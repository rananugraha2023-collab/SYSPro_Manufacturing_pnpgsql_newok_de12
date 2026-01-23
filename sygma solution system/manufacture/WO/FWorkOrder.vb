Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls

Public Class FWorkOrder

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wo_oid_mstr As String
    Dim ds_edit As DataSet
    Dim _qty_wo_before As Double = 0
    Dim status_insert As Boolean = True
    Public _wod_related_oid As String = ""
    Public dt_edit As New DataTable
    Dim ssqls As New ArrayList
    Public _wo_sod_oid As String

    Private Sub FWorkOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        gv_detail.OptionsView.ShowFooter = True
        tcg_header.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        tcg_header.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        Dim sSQL As String
        Dim dt As New DataTable

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        wo_en_id.Properties.DataSource = dt_bantu
        wo_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        wo_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        wo_en_id.ItemIndex = 0

        sSQL = "select '' as code, 'Normal' as name " _
            & "union " _
            & "select 'R' as code, 'Rework' as name "

        dt = master_new.PGSqlConn.GetTableData(sSQL)
        With wo_type
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("code", "Code", 10))
                .Properties.Columns.Add(New LookUpColumnInfo("name", "Name", 20))
            End If

            .Properties.DataSource = dt
            .Properties.DisplayMember = dt.Columns("name").ToString
            .Properties.ValueMember = dt.Columns("code").ToString
            .ItemIndex = 0
            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 2
        End With



        sSQL = "select wog_code , wog_name  from wog_group " _
          & "order by wog_name "

        dt = master_new.PGSqlConn.GetTableData(sSQL)
        With wo_wog_code
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("wog_code", "Code", 10))
                .Properties.Columns.Add(New LookUpColumnInfo("wog_name", "Name", 20))
            End If

            .Properties.DataSource = dt
            .Properties.DisplayMember = dt.Columns("wog_name").ToString
            .Properties.ValueMember = dt.Columns("wog_code").ToString
            .ItemIndex = 0
            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 6
        End With
    End Sub

    Public Overrides Sub approve_line()
        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Resume WO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If

        If MessageBox.Show("Are you " + master_new.ClsVar.sNama + " sure to resume..?", "Resume..", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If


        row = BindingContext(ds.Tables(0)).Position

        If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
            row = row - 1
        ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
            row = 0
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wo_mstr set wo_status='R' where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Resume data WO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        help_load_data(True)
                        MessageBox.Show("Data have been hold..", "Hold..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)

                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Overrides Sub cancel_line()
        'walaupun
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid")
        '_colom = "so_trans_id"
        '_table = "so_mstr"
        '_criteria = "so_code"
        '_initial = "so"
        '_type = "so"

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Hold WO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If

        If MessageBox.Show("Are you " + master_new.ClsVar.sNama + " sure to hold..?", "Hold..", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If


        row = BindingContext(ds.Tables(0)).Position

        If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
            row = row - 1
        ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
            row = 0
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wo_mstr set wo_status='H' where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Hold data WO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        help_load_data(True)
                        MessageBox.Show("Data have been hold..", "Hold..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)

                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub
    Public Overrides Sub format_grid()
        add_column(gv_master, "wo_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WO Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Wo Type", "wo_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Wo Group", "wog_name", DevExpress.Utils.HorzAlignment.Default)
        'wog_name
        add_column_copy(gv_master, "Reference Rework", "wo_ref_rework_wo", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number Project", "pt_code_prj", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1_prj", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number WO", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Product Structure", "ps_par", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Qty", "wo_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Proccess", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Complete", "wo_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "wo_qty_rjc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost", "wo_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Insheet Percent", "wo_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Qty Prod", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Start Date", "wo_start_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "wo_end_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "d")

        add_column_copy(gv_master, "Sales Order Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Order Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "wo_ro_id", False)
        add_column_copy(gv_master, "Routing", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Routing", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wo_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column_copy(gv_detail, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number Description", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Indirect", "wod_indirect", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "psd_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Qty", "wod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Qty PR", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_detail, "Insheet", "wod_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Qty Insheet", "wod_qty_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n4")

        add_column_copy(gv_detail, "Sequence", "wod_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

        add_column_copy(gv_routing, "Sequence", "wodr_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column_copy(gv_routing, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_routing, "Description", "wodr_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_routing, "Insheet", "wodr_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_routing, "Qty In", "wodr_qty_in", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Qty Complete", "wodr_qty_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Qty Reject", "wodr_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Qty Outstanding", "wodr_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Qty Out", "wodr_qty_out", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Qty Out Conversion", "wodr_qty_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_routing, "Total Setup", "wodr_setup", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Total Run", "wodr_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_routing, "Total Down", "wodr_down", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub

    Public Overrides Sub preview()

        Dim ssql As String
        ssql = "SELECT  " _
                & "  a.wo_oid, " _
                & "  a.wo_dom_id, " _
                & "  a.wo_en_id, " _
                & "  a.wo_si_id, " _
                & "  a.wo_id, " _
                & "  a.wo_code, " _
                & "  a.wo_type, " _
                & "  a.wo_pt_id, a.wo_pt_id_prj, " _
                & "  a.wo_qty_ord, " _
                & "  a.wo_qty_comp, " _
                & "  a.wo_qty_rjc, " _
                & "  a.wo_ord_date, " _
                & "  a.wo_rel_date, " _
                & "  a.wo_due_date, " _
                & "  a.wo_insheet_pct, " _
                & "  a.wo_ro_id, " _
                & "  a.wo_status, " _
                & "  a.wo_remarks, " _
                & "  a.wo_dt, " _
                & "  a.wo_date_close, " _
                & "  a.wo_pjc_id, " _
                & "  a.wo_ref_rework, " _
                & "  a.wo_qty, " _
                & "  b.en_desc, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  k.pt_code as pt_code_prj, " _
                & "  k.pt_desc1 as pt_desc1_prj, " _
                & "  c.pt_desc2,a.wo_ps_id,ps_par, " _
                & "  e.ro_code, " _
                & "  e.ro_desc, a.wo_so_oid, " _
                & "  g.so_code, " _
                & "  g.so_date, " _
                & "  g.so_ptnr_id_sold, " _
                & "  h.ptnr_name, " _
                & "  i.wo_code AS wo_ref_rework,a.wo_cost, " _
                & "  j.si_desc, " _
                & "  l.wod_oid, " _
                & "  l.wod_pt_bom_id, " _
                & "  m.pt_code, " _
                & "  m.pt_desc1, " _
                & "  m.pt_desc2, " _
                & "  l.wod_op, " _
                & "  n.op_name, " _
                & "  l.wod_qty,wod_qty_req, " _
                & "  l.wod_indirect, " _
                & "  l.wod_insheet_pct, " _
                & "  l.wod_seq, " _
                & "  l.wod_qty_insheet, " _
                & "  o.code_name AS um_desc " _
                & "FROM " _
                & "  public.wo_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.wo_en_id = b.en_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.wo_pt_id = c.pt_id) " _
                & "  INNER JOIN public.ps_mstr d ON (a.wo_ps_id = d.ps_id) " _
                & "  INNER JOIN public.ro_mstr e ON (a.wo_ro_id = e.ro_id) " _
                & "  INNER JOIN public.so_mstr g ON (a.wo_so_oid = g.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr h ON (g.so_ptnr_id_sold = h.ptnr_id) " _
                & "  LEFT OUTER JOIN public.wo_mstr i ON (a.wo_ref_rework = i.wo_oid) " _
                & "  LEFT OUTER JOIN public.si_mstr j ON (a.wo_si_id = j.si_id) " _
                & "  LEFT OUTER JOIN public.pt_mstr k ON (a.wo_pt_id_prj = k.pt_id) " _
                & "  INNER JOIN public.wod_det l ON (a.wo_oid = l.wod_wo_oid) " _
                & "  INNER JOIN public.pt_mstr m ON (l.wod_pt_bom_id = m.pt_id) " _
                & "  left outer JOIN public.op_mstr n ON (l.wod_op = n.op_code) " _
                & "  left outer JOIN public.code_mstr o ON (m.pt_um = o.code_id) " _
                & "  where a.wo_code = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code")) _
                & " Order by wo_code"

        Dim ds_bantu As New DataSet

        Dim rpt As New XR_WO
        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = ssql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                '._pjc_code = be_to.Text
                .DataSource = ds_bantu
                .DataMember = "data"
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

                'printTool.PreviewForm.Text = "Personal Selling SDI Report"
                printTool.ShowPreview()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try



    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

        get_sequel = "SELECT  " _
                & "  a.wo_oid,a.wo_wog_code,wog_name, " _
                & "  a.wo_dom_id, " _
                & "  a.wo_en_id, " _
                & "  a.wo_si_id, " _
                & "  a.wo_id, " _
                & "  a.wo_code, " _
                & "  a.wo_type, " _
                & "  a.wo_pt_id, a.wo_pt_id_prj, " _
                & "  a.wo_qty_ord, " _
                & "  a.wo_qty_comp, " _
                & "  a.wo_qty_rjc, " _
                & "  a.wo_ord_date, " _
                & "  a.wo_rel_date, " _
                & "  a.wo_due_date, " _
                & "  a.wo_insheet_pct, " _
                & "  a.wo_ro_id, " _
                & "  a.wo_status, " _
                & "  a.wo_remarks, " _
                & "  a.wo_dt, " _
                & "  a.wo_date_close, " _
                & "  a.wo_pjc_id, " _
                & "  a.wo_ref_rework, " _
                & "  a.wo_qty, " _
                & "  b.en_desc, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  k.pt_code as pt_code_prj, " _
                & "  k.pt_desc1 as pt_desc1_prj, " _
                & "  c.pt_desc2,a.wo_ps_id,ps_par, " _
                & "  e.ro_code, " _
                & "  e.ro_desc, a.wo_so_oid, " _
                & "  g.so_code, " _
                & "  g.so_date, " _
                & "  g.so_ptnr_id_sold, " _
                & "  h.ptnr_name, " _
                & "  i.wo_code AS wo_ref_rework_wo,a.wo_cost, " _
                & "  j.si_desc " _
                & "FROM " _
                & "  public.wo_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.wo_en_id = b.en_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.wo_pt_id = c.pt_id) " _
                & "  INNER JOIN public.ps_mstr d ON (a.wo_ps_id = d.ps_id) " _
                & "  INNER JOIN public.ro_mstr e ON (a.wo_ro_id = e.ro_id) " _
                & "  INNER JOIN public.so_mstr g ON (a.wo_so_oid = g.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr h ON (g.so_ptnr_id_sold = h.ptnr_id) " _
                & "  LEFT OUTER JOIN public.wo_mstr i ON (a.wo_ref_rework = i.wo_oid) " _
                & "  LEFT OUTER JOIN public.si_mstr j ON (a.wo_si_id = j.si_id) " _
                & "  LEFT OUTER JOIN public.pt_mstr k ON (a.wo_pt_id_prj = k.pt_id) " _
                 & "  LEFT OUTER JOIN public.wog_group l ON (a.wo_wog_code = l.wog_code) " _
                & "  where a.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and a.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & "  and a.wo_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " Order by wo_code"


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()


        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub ' If the table exists but has no rows, exit
            End If
        Else
            Exit Sub ' If the DataSet has no tables, exit
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try


        sql = "SELECT  " _
          & "  a.wod_oid, " _
          & "  a.wod_pt_bom_id, " _
          & "  b.pt_code, " _
          & "  b.pt_desc1, " _
          & "  b.pt_desc2, " _
          & "  a.wod_op, " _
          & "  c.op_name, " _
          & "  a.wod_qty,wod_qty_req, " _
          & "  a.wod_indirect, " _
          & "  a.wod_insheet_pct, " _
          & "  a.wod_seq, " _
          & "  a.wod_qty_insheet, " _
          & "  d.code_name AS um_desc " _
          & "FROM " _
          & "  public.wod_det a " _
          & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
          & "  left outer JOIN public.op_mstr c ON (a.wod_op = c.op_code) " _
          & "  left outer JOIN public.code_mstr d ON (b.pt_um = d.code_id) " _
          & "WHERE " _
          & "  a.wod_wo_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'"

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("routing").Clear()
        Catch ex As Exception
        End Try
        Try

            sql = "SELECT  " _
               & "  a.wodr_uid, " _
               & "  a.wodr_wo_oid, " _
               & "  a.wodr_op, " _
               & "  b.op_name, " _
               & "  a.wodr_start_date, " _
               & "  a.wodr_end_date, " _
               & "  a.wodr_desc, " _
               & "  a.wodr_wc_id, " _
               & "  c.wc_desc, " _
               & "  a.wodr_yield_pct, " _
               & "  a.wodr_seq, " _
               & "  a.wodr_qty_in, " _
               & "  a.wodr_qty_complete, " _
               & "  a.wodr_qty_reject,coalesce(wodr_qty_in,0) - coalesce(wodr_qty_complete,0) -  coalesce(wodr_qty_reject,0) as  wodr_qty_outstanding, " _
               & "  a.wodr_qty_out,wodr_run,wodr_setup,wodr_down, wodr_qty_conversion " _
               & "FROM " _
               & "  public.wodr_routing a " _
               & "  left outer JOIN public.op_mstr b ON (a.wodr_op = b.op_code) " _
               & "  left outer JOIN public.wc_mstr c ON (a.wodr_wc_id = c.wc_id) " _
               & "WHERE " _
               & "  a.wodr_wo_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "' " _
               & " ORDER By wodr_seq"

            load_data_detail(sql, gc_routing, "routing")

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        'Try
        'gv_detail.Columns("wod_wo_oid").FilterInfo = _
        'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wod_wo_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'")
        'gv_detail.BestFitColumns()

        'gv_routing.Columns("wr_wo_oid").FilterInfo = _
        'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wr_wo_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'")
        'gv_routing.BestFitColumns()


    End Sub
#End Region

#Region "valuechanged"
    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wo_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(wo_en_id.EditValue))
        wo_si_id.Properties.DataSource = dt_bantu
        wo_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        wo_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        wo_si_id.ItemIndex = 0



        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ro_mstr(wo_en_id.EditValue))
        'wo_ro_id.Properties.DataSource = dt_bantu
        'wo_ro_id.Properties.DisplayMember = dt_bantu.Columns("ro_desc").ToString
        'wo_ro_id.Properties.ValueMember = dt_bantu.Columns("ro_id").ToString
        'wo_ro_id.ItemIndex = 0


    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        wo_en_id.Focus()
        wo_en_id.ItemIndex = 0
        wo_si_id.ItemIndex = 0
        wo_pt_id.EditValue = ""
        wo_pt_id.Tag = ""

        wo_pt_id_prj.EditValue = ""
        wo_pt_id_prj.Tag = ""

        wo_qty_ord.Text = 0
        wo_insheet_pct.Text = 0
        wo_ord_date.DateTime = Now
        wo_due_date.DateTime = Now

        wo_ps_id.Tag = ""
        wo_ps_id.Text = ""
        wo_ro_id.Tag = ""
        wo_ro_id.EditValue = ""
        wo_remarks.Text = ""
        wo_so_oid.Text = ""
        wo_so_oid.Tag = ""
        pjc_desc.Text = ""
        wo_type.EditValue = ""
        wo_ref_rework.Tag = ""
        wo_ref_rework.Text = ""
        wo_wog_code.ItemIndex = 0
        wo_start_date.DateTime = CekTanggal.Date
        wo_end_date.DateTime = CekTanggal.Date
        _wo_sod_oid = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit.AcceptChanges()

        'dt_edit()

        If wo_ro_id.Tag.ToString = "" Then
            Box("Routing can't null")
            Return False
        End If

        'If SetString(wo_type.EditValue) = "" Then
        '    If wo_ro_id.Tag.ToString = "" Then
        '        Box("Routing can't null")
        '        Return False
        '    End If
        '    'Else
        '    '    If dt_edit.Rows.Count = 0 Then
        '    '        Box("Routing detail can't empty")
        '    '        Return False
        '    '    End If
        'End If

        If wo_so_oid.Text = "" Then
            MessageBox.Show("Sales Order Code Can't Empty ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If IsDBNull(wo_pt_id.EditValue) Then
            MessageBox.Show("Part Code Cannot Null...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        Dim sql As String
        Try
            sql = "SELECT  " _
              & "  a.wr_op, " _
              & "  a.wr_wc_id, " _
              & "  c.wc_desc, " _
              & "  a.wr_setup_men, " _
              & "  a.wr_setup_rate, " _
              & "  a.wr_setup, " _
              & "  a.wr_setup_real, " _
              & "  a.wr_lbr_rate, " _
              & "  a.wr_men_mch, " _
              & "  a.wr_run, " _
              & "  a.wr_run_real, " _
              & "  a.wr_sub_cost, " _
              & "  a.wr_sub_cost_real, " _
              & "  a.wr_mch_op, " _
              & "  a.wr_mch_bdn_rate, " _
              & "  a.wr_trans_id, " _
              & "  a.wr_qty_wo, " _
              & "  a.wr_qty_feedback, " _
              & "  a.wr_wo_oid, " _
              & "  a.wr_oid " _
              & "FROM " _
              & "  public.wr_route a " _
              & "  INNER JOIN public.wc_mstr c ON (a.wr_wc_id = c.wc_id) " _
              & "WHERE " _
              & " wr_oid is null"

            dt_edit = master_new.PGSqlConn.GetTableData(sql)

            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Function

    Public Overrides Function insert() As Boolean
        Dim _wo_oid As Guid
        _wo_oid = Guid.NewGuid

        If wo_pt_id_prj.Tag <> wo_pt_id.Tag Then
            wo_cost.EditValue = 0.0
        End If

        gv_edit.UpdateCurrentRow()
        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit.AcceptChanges()
        ssqls.Clear()

        Dim _wo_code As String

        _wo_code = func_coll.get_transaction_number("WO", wo_en_id.GetColumnValue("en_code"), "wo_mstr", "wo_code", func_coll.get_tanggal_sistem)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.wo_mstr " _
                                            & "( " _
                                            & "  wo_oid, " _
                                            & "  wo_dom_id, " _
                                            & "  wo_en_id, " _
                                            & "  wo_si_id, " _
                                            & "  wo_id, " _
                                            & "  wo_code, " _
                                            & "  wo_pt_id,wo_ps_id,wo_pt_id_prj, " _
                                            & "  wo_qty_ord,wo_qty, " _
                                            & "  wo_qty_comp, " _
                                            & "  wo_qty_rjc, " _
                                            & "  wo_ord_date, " _
                                            & "  wo_due_date, " _
                                            & "  wo_insheet_pct, " _
                                            & "  wo_so_oid, " _
                                            & "  wo_ro_id, " _
                                            & "  wo_status,wo_cost,wo_sod_oid, " _
                                            & "  wo_remarks,wo_type,wo_ref_rework,wo_wog_code, wo_start_date,wo_end_date," _
                                            & "  wo_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_wo_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetIntegerDB(wo_en_id.EditValue) & ",  " _
                                            & SetIntegerDB(wo_si_id.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("wo_mstr", wo_en_id.GetColumnValue("en_code"), "wo_id", "wo_en_id", wo_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(_wo_code.ToString) & ",  " _
                                            & SetIntegerDB(wo_pt_id.Tag) & ",  " _
                                            & SetIntegerDB(wo_ps_id.Tag) & ",  " _
                                            & SetIntegerDB(wo_pt_id_prj.Tag) & ",  " _
                                            & SetDec(wo_qty_ord.EditValue) & ",  " _
                                             & SetDec(wo_qty.EditValue) & ",  " _
                                            & "0,  " _
                                            & "0,  " _
                                            & SetDate(wo_ord_date.EditValue) & ",  " _
                                            & SetDate(wo_due_date.EditValue) & ",  " _
                                            & SetDec(wo_insheet_pct.EditValue) & ",  " _
                                            & SetSetring(wo_so_oid.Tag) & ",  " _
                                            & SetIntegerDB(wo_ro_id.Tag) & ",  " _
                                            & "'F' ,  " _
                                            & SetDec(wo_cost.EditValue) & ",  " _
                                            & SetSetring(_wo_sod_oid) & ",  " _
                                            & SetSetring(wo_remarks.Text) & ",  " _
                                            & SetSetring(wo_type.EditValue) & ",  " _
                                            & SetSetring(wo_ref_rework.Tag) & ",  " _
                                             & SetSetring(wo_wog_code.EditValue) & ",  " _
                                             & SetDate(wo_start_date.EditValue) & ",  " _
                                             & SetDate(wo_end_date.EditValue) & ",  " _
                                            & "current_timestamp " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'cek jika partnumber utama
                        If SetString(wo_so_oid.Tag) <> "" Then

                            Dim sql As String = ""
                            sql = "SELECT  " _
                                & "  b.sod_pt_id,b.sod_oid " _
                                & "FROM " _
                                & "  public.so_mstr a " _
                                & "  INNER JOIN public.sod_det b ON (a.so_oid = b.sod_so_oid) " _
                                & "WHERE " _
                                & "  a.so_oid = '" & wo_so_oid.Tag & "'"

                            Dim dt_so As New DataTable
                            dt_so = master_new.PGSqlConn.GetTableData(sql)

                            For Each dr_so As DataRow In dt_so.Rows
                                If SetString(dr_so(0)) = SetString(wo_pt_id.Tag) Then
                                    '.Command.CommandType = CommandType.Text
                                    '.Command.CommandText = "update so_mstr set so_wo_status = 'Y' " _
                                    '                       & " where so_oid = " + SetSetring(wo_so_oid.Tag) & "  "

                                    'ssqls.Add(.Command.CommandText)
                                    '.Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update sod_det set sod_wo_status = 'Y' " _
                                                           & " where sod_oid = " + SetSetring(dr_so("sod_oid")) & "  "

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    Exit For
                                End If
                            Next

                        End If


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If


                        sqlTran.Commit()
                        after_success()
                        set_row(_wo_oid.ToString, "wo_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        Dim _sql As String

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
            Box("WO has been released")
            Exit Function
        End If
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                _wo_oid_mstr = .Item("wo_oid").ToString
                wo_en_id.EditValue = .Item("wo_en_id")
                wo_si_id.EditValue = .Item("wo_si_id")
                wo_pt_id.Text = .Item("pt_code")
                wo_pt_id.Tag = .Item("wo_pt_id")

                wo_pt_id_prj.EditValue = .Item("pt_code_prj")
                wo_pt_id_prj.Tag = .Item("wo_pt_id_prj")

                wo_ps_id.Text = .Item("ps_par")
                wo_ps_id.Tag = .Item("wo_ps_id")
                wo_qty.EditValue = .Item("wo_qty")
                wo_qty_ord.EditValue = .Item("wo_qty_ord")

                wo_ord_date.DateTime = .Item("wo_ord_date")
                wo_due_date.DateTime = .Item("wo_due_date")
                wo_insheet_pct.EditValue = .Item("wo_insheet_pct")
                wo_ro_id.Text = SetString(.Item("ro_desc"))
                wo_ro_id.Tag = SetString(.Item("wo_ro_id"))
                wo_remarks.Text = SetString(.Item("wo_remarks"))
                wo_so_oid.Text = SetString(.Item("so_code"))
                wo_so_oid.Tag = .Item("wo_so_oid").ToString

                wo_cost.EditValue = .Item("wo_cost")
                wo_type.EditValue = .Item("wo_type")
                wo_ref_rework.Tag = SetString(.Item("wo_ref_rework"))
                ' wo_ref_rework.Text = SetString(.Item("wo_ref_code"))
                wo_wog_code.EditValue = .Item("wo_wog_code")

                wo_start_date.EditValue = .Item("wo_start_date")
                wo_end_date.EditValue = .Item("wo_end_date")

            End With
            wo_en_id.Focus()



            Try
                dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        ssqls.Clear()
        If SetString(wo_type.EditValue) = "" Then
            If wo_so_oid.Text <> "" Then

            End If
        End If

        If wo_pt_id_prj.Tag <> wo_pt_id.Tag Then
            wo_cost.EditValue = 0.0
        End If

        'Return False
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wo_mstr   " _
                                            & "SET  " _
                                            & "  wo_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  wo_en_id = " & SetInteger(wo_en_id.EditValue) & ",  " _
                                            & "  wo_si_id = " & SetInteger(wo_si_id.EditValue) & ",  " _
                                            & "  wo_pt_id = " & SetInteger(wo_pt_id.Tag) & ",  " _
                                            & "  wo_pt_id_prj = " & SetInteger(wo_pt_id_prj.Tag) & ",  " _
                                            & "  wo_qty_ord = " & SetDec(wo_qty_ord.EditValue) & ",  " _
                                            & "  wo_ord_date = " & SetDate(wo_ord_date.DateTime) & ",  " _
                                            & "  wo_start_date = " & SetDate(wo_start_date.DateTime) & ",  " _
                                            & "  wo_end_date = " & SetDate(wo_end_date.DateTime) & ",  " _
                                            & "  wo_due_date = " & SetDate(wo_due_date.DateTime) & ",  " _
                                            & "  wo_insheet_pct = " & SetDec(wo_insheet_pct.EditValue) & ",  " _
                                            & "  wo_qty = " & SetDec(wo_qty.EditValue) & ",  " _
                                            & "  wo_ro_id = " & SetIntegerDB(wo_ro_id.Tag) & ",  " _
                                            & "  wo_so_oid = " & SetSetring(wo_so_oid.Tag) & ",  " _
                                            & "  wo_sod_oid = " & SetSetring(_wo_sod_oid) & ",  " _
                                             & "  wo_wog_code = " & SetSetring(wo_wog_code.EditValue) & ",  " _
                                            & "  wo_remarks = " & SetSetringDB(wo_remarks.Text) & ",  " _
                                            & "  wo_type = " & SetSetringDB(wo_type.EditValue) & ",  " _
                                            & "  wo_ref_rework = " & IIf(wo_ref_rework.Tag = "", "null", SetSetringDB(wo_ref_rework.Tag)) & ",  " _
                                            & "  wo_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  wo_oid = " & SetSetring(_wo_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If SetString(wo_type.EditValue) = "R" Then
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "delete from wr_route where wr_wo_oid ='" & _wo_oid_mstr.ToString & "'"
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        '    For i As Integer = 0 To dt_edit.Rows.Count - 1
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = "INSERT INTO  " _
                        '                            & "  public.wr_route " _
                        '                            & "( " _
                        '                            & "  wr_oid, " _
                        '                            & "  wr_dom_id, " _
                        '                            & "  wr_en_id, " _
                        '                            & "  wr_add_by, " _
                        '                            & "  wr_add_date, " _
                        '                            & "  wr_upd_by, " _
                        '                            & "  wr_upd_date, " _
                        '                            & "  wr_dt, " _
                        '                            & "  wr_wo_oid, " _
                        '                            & "  wr_op, " _
                        '                            & "  wr_wc_id, " _
                        '                            & "  wr_setup_men, " _
                        '                            & "  wr_setup_rate, " _
                        '                            & "  wr_setup, " _
                        '                            & "  wr_setup_real, " _
                        '                            & "  wr_lbr_rate, " _
                        '                            & "  wr_men_mch, " _
                        '                            & "  wr_run, " _
                        '                            & "  wr_run_real, " _
                        '                            & "  wr_sub_cost, " _
                        '                            & "  wr_sub_cost_real, " _
                        '                            & "  wr_mch_op, " _
                        '                            & "  wr_mch_bdn_rate, " _
                        '                            & "  wr_trans_id, " _
                        '                            & "  wr_qty_wo, " _
                        '                            & "  wr_qty_feedback " _
                        '                            & ")  " _
                        '                            & "VALUES ( " _
                        '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                        '                            & SetInteger(wo_en_id.EditValue) & ",  " _
                        '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                            & "current_timestamp" & ",  " _
                        '                            & "null" & ",  " _
                        '                            & "null" & ",  " _
                        '                            & "current_timestamp" & ",  " _
                        '                            & SetSetring(_wo_oid_mstr.ToString) & ",  " _
                        '                            & SetInteger(dt_edit.Rows(i).Item("wr_op")) & ",  " _
                        '                            & SetInteger(dt_edit.Rows(i).Item("wr_wc_id")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_setup_men")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_setup_rate")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_setup")) & ",  " _
                        '                            & SetDec(0) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_lbr_rate")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_men_mch")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_run")) & ",  " _
                        '                            & SetDec(0) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_sub_cost")) & ",  " _
                        '                            & SetDec(0) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_mch_op")) & ",  " _
                        '                            & SetDec(dt_edit.Rows(i).Item("wr_mch_bdn_rate")) & ",  " _
                        '                            & SetSetring("D") & ",  " _
                        '                            & SetDec(wo_qty_ord.EditValue) & ",  " _
                        '                            & SetDec(0) & "  " _
                        '                            & ")"

                        '        ssqls.Add(.Command.CommandText)

                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'Else
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) - " + SetIntegerDB(_qty_wo_before) + _
                        '                           " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_so_oid.Text + "'))" + _
                        '                           " and boqd_pt_id = " + SetInteger(wo_pt_id.Tag)
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) + " + SetIntegerDB(wo_qty_ord.EditValue) + _
                        '                           " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + wo_so_oid.Text + "'))" + _
                        '                           " and boqd_pt_id = " + SetInteger(wo_pt_id.Tag)
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        'End If

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_wo_oid_mstr, "wo_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        edit = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If
        ssqls.Clear()
        Dim sSQL As String
        sSQL = "select coalesce(wod_qty_issued,0) as wod_qty_issued from wod_det where wod_wo_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)

        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("wod_qty_issued") > 0 Then
                MessageBox.Show("This WO has been issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
                Exit Function
            End If
        Next

        'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_status") = "R" Then
        '    Box("WO has been released")
        '    Exit Function
        'End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "update boqd_det set boqd_qty_wo = coalesce(boqd_qty_wo,0) - " + SetIntegerDB(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_qty_ord")) + _
                            '                       " where boqd_boq_oid = (select boq_oid from boq_mstr where boq_sopj_oid = (select prj_oid from prj_mstr where prj_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pjc_code") + "'))" + _
                            '                       " and boqd_pt_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_pt_id"))
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            Dim _wo_so_oid As String = ""
                            Dim _wo_pt_id As String = ""
                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_so_oid")) <> "" Then
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update so_mstr set so_wo_status = 'N' " _
                                '                       & " where so_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_so_oid")) & "  "


                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                _wo_so_oid = SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_so_oid"))
                                _wo_pt_id = SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_pt_id"))

                                Dim sql As String = ""
                                sql = "SELECT  " _
                                    & "  b.sod_pt_id,b.sod_oid " _
                                    & "FROM " _
                                    & "  public.so_mstr a " _
                                    & "  INNER JOIN public.sod_det b ON (a.so_oid = b.sod_so_oid) " _
                                    & "WHERE " _
                                    & "  a.so_oid = '" & _wo_so_oid & "'"

                                Dim dt_so As New DataTable
                                dt_so = master_new.PGSqlConn.GetTableData(sql)

                                For Each dr_so As DataRow In dt_so.Rows
                                    If SetString(dr_so(0)) = SetString(_wo_pt_id) Then
                                        '.Command.CommandType = CommandType.Text
                                        '.Command.CommandText = "update so_mstr set so_wo_status = 'Y' " _
                                        '                       & " where so_oid = " + SetSetring(wo_so_oid.Tag) & "  "

                                        'ssqls.Add(.Command.CommandText)
                                        '.Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()


                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sod_det set sod_wo_status = 'N' " _
                                                               & " where sod_oid = " + SetSetring(dr_so("sod_oid")) & "  "

                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                        Exit For
                                    End If
                                Next

                            End If



                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

    Private Sub wo_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_pt_id.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._obj = wo_pt_id
        frm._en_id = wo_en_id.EditValue
        frm._prj_code = wo_so_oid.Text
        frm._pt_id = wo_pt_id_prj.Tag
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub wo_ro_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_ro_id.ButtonClick
        Dim frm As New FRoutingSearch
        frm.set_win(Me)
        frm._en_id = wo_en_id.EditValue
        frm._pt_id = wo_pt_id.Tag
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub wo_pjc_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_so_oid.ButtonClick
        Dim frm As New FSalesOrderSearch 'FProjectAccountSearch()
        frm.set_win(Me)
        frm._obj = wo_so_oid
        frm._en_id = wo_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub wo_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wo_type.EditValueChanged
        Try
            If SetString(wo_type.EditValue) = "R" Then
                tcg_header.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True
                tcg_header.SelectedTabPageIndex = 0
                wo_ref_rework.Enabled = True
                wo_ref_rework.Tag = ""
                wo_ref_rework.Text = ""
            Else
                tcg_header.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
                tcg_header.SelectedTabPageIndex = 0
                wo_ref_rework.Enabled = False
                wo_ref_rework.Tag = ""
                wo_ref_rework.Text = ""
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub wo_ref_rework_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_ref_rework.ButtonClick
        Try
            Dim frm As New FWOSearch()
            frm.set_win(Me)
            frm._en_id = wo_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _en_id As Integer = wo_en_id.EditValue

        If _col = "wc_desc" Then
            Dim frm As New FWCSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub wo_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_ps_id.ButtonClick
        Dim frm As New FProdStrucSearch()
        frm.set_win(Me)
        frm._obj = wo_ps_id
        frm._en_id = wo_en_id.EditValue
        frm._prj_code = wo_so_oid.Text
        frm._pt_id = wo_pt_id.Tag
        frm.type_form = True
        frm.ShowDialog()

    End Sub

    Private Sub wo_insheet_pct_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wo_insheet_pct.EditValueChanged
        Try
            Dim _qty As Double = 0
            Dim _yield As Double = 0

            _qty = SetNumber(wo_qty.EditValue)
            _yield = SetNumber(wo_insheet_pct.EditValue)
            wo_qty_ord.EditValue = Math.Round(_qty * _yield, 0)

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub wo_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles wo_qty.EditValueChanged
        Try
            Dim _qty As Double = 0
            Dim _yield As Double = 0

            _qty = SetNumber(wo_qty.EditValue)
            _yield = SetNumber(wo_insheet_pct.EditValue)
            wo_qty_ord.EditValue = Math.Round(_qty * _yield, 0)

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub wo_pt_id_prj_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles wo_pt_id_prj.ButtonClick
        Try
            Dim frm As New FProdStrucSearch()
            frm.set_win(Me)
            frm._obj = wo_pt_id_prj
            frm._en_id = wo_en_id.EditValue
            frm._prj_code = wo_so_oid.Text
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Sub LabelControl3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelControl3.DoubleClick
        Try
            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code") + " unrelease Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE wo_mstr set wo_status='F' where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "DELETE from wod_det where wod_wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "DELETE from wodr_routing where wodr_wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di unrelease..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub UpdateRoutingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateRoutingToolStripMenuItem.Click
        Try
            Dim ssql As String = ""
            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code") + " update routing ..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
            Dim _code1, _code2 As String
            Dim dt_lbr As New DataTable
            Dim dt_routing As New DataTable
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran


                            _code1 = ""
                            _code2 = ""

                            Dim x As Integer = 0

                            For Each dr As DataRow In ds.Tables("routing").Rows
                                If x = 0 Then
                                    ssql = "UPDATE  " _
                                            & "  public.wodr_routing   " _
                                            & "SET  " _
                                            & "  wodr_qty_complete = 0 ,wodr_qty_out=0,  " _
                                            & "  wodr_qty_reject = 0,  " _
                                            & "  wodr_run = 0,  " _
                                            & "  wodr_setup = 0,  " _
                                            & "  wodr_down =0 " _
                                            & "WHERE  " _
                                            & "  wodr_uid = " & SetSetring(dr("wodr_uid")) & " "

                                    DbRun(ssql)
                                Else
                                    ssql = "UPDATE  " _
                                           & "  public.wodr_routing   " _
                                           & "SET  " _
                                           & "  wodr_qty_complete = 0 , wodr_qty_in=0, wodr_qty_out=0," _
                                           & "  wodr_qty_reject = 0,  " _
                                           & "  wodr_run = 0,  " _
                                           & "  wodr_setup = 0,  " _
                                           & "  wodr_down =0 " _
                                           & "WHERE  " _
                                           & "  wodr_uid = " & SetSetring(dr("wodr_uid")) & " "
                                    DbRun(ssql)
                                End If
                                x = x + 1
                            Next
                            x = 0
                            For Each dr As DataRow In ds.Tables("routing").Rows
                               

                                ssql = "SELECT lbrf_code, " _
                                   & "  x.lbrf_qty_complete, " _
                                   & "  x.lbrf_qty_reject, " _
                                   & "  x.lbrf_elapsed_run, " _
                                   & "  x.lbrf_elapsed_down, " _
                                   & "  x.lbrf_elapsed_setup " _
                                   & "FROM " _
                                   & "  public.lbrf_mstr x " _
                                   & "WHERE " _
                                   & "  x.lbrf_wodr_uid = '" & dr("wodr_uid").ToString & "' " _
                                   & "ORDER BY " _
                                   & "  x.lbrf_date"

                                dt_lbr = GetTableData(ssql)

                                For Each dr_lbr As DataRow In dt_lbr.Rows
                                    _code1 = dr_lbr("lbrf_code")
                                    ssql = "UPDATE  " _
                                        & "  public.wodr_routing   " _
                                        & "SET  " _
                                        & "  wodr_qty_complete = coalesce(wodr_qty_complete,0) + " & SetDec(dr_lbr("lbrf_qty_complete")) & ",  " _
                                        & "  wodr_qty_reject = coalesce(wodr_qty_reject,0) + " & SetDec(dr_lbr("lbrf_qty_reject")) & ",  " _
                                        & "  wodr_run = coalesce(wodr_run,0) + " & SetDec(dr_lbr("lbrf_elapsed_run")) & ",  " _
                                        & "  wodr_setup = coalesce(wodr_setup,0) + " & SetDec(dr_lbr("lbrf_elapsed_setup")) & ",  " _
                                        & "  wodr_down = coalesce(wodr_down,0) + " & SetDec(dr_lbr("lbrf_elapsed_down")) & "  " _
                                        & "WHERE  " _
                                        & "  wodr_uid = " & SetSetring(dr("wodr_uid")) & " "

                                    'ssqls.Add(.Command.CommandText)
                                    '.Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()

                                    DbRun(ssql)

                                Next

                                ssql = "SELECT  trans_code," _
                                    & "  x.trans_wodr_uid_from, " _
                                    & "  x.trans_wodr_uid_to, " _
                                    & "  x.trans_qty, " _
                                    & "  x.trans_qty_conversion, " _
                                    & "  x.trans_qty_routing_conversion_from " _
                                    & "FROM " _
                                    & "  public.transrouting_mstr x " _
                                    & "WHERE " _
                                    & "  x.trans_wodr_uid_from = '" & dr("wodr_uid").ToString & "' order by x.trans_date"

                                dt_routing = GetTableData(ssql)

                                For Each dr_routing As DataRow In dt_routing.Rows
                                    _code2 = dr_routing("trans_code")
                                    ssql = "UPDATE  " _
                                        & "  public.wodr_routing   " _
                                        & "SET  " _
                                        & "  wodr_qty_out = coalesce(wodr_qty_out,0) + " & SetDec(dr_routing("trans_qty")) & "  " _
                                        & "    " _
                                        & "WHERE  " _
                                        & "  wodr_uid = " & SetSetring(dr("wodr_uid")) & " "

                                    DbRun(ssql)

                                    ssql = "UPDATE  " _
                                     & "  public.wodr_routing   " _
                                     & "SET  " _
                                     & "  wodr_qty_in = coalesce(wodr_qty_in,0) + " & SetDec(dr("wodr_qty_conversion") * dr_routing("trans_qty")) & "  " _
                                     & "    " _
                                     & "WHERE  " _
                                     & "  wodr_uid = " & SetSetring(dr_routing("trans_wodr_uid_to")) & " "

                                    DbRun(ssql)
                                Next
                              


                                x = x + 1

                            Next



                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Update routing success", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message & " No " & _code1 & " " & _code2 & " : " & ssql)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RegenerateRoutingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegenerateRoutingToolStripMenuItem.Click
        Try

            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_code") + " regenerate routing ..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim sSQL As String = ""
            sSQL = "SELECT  " _
                   & "  rod_oid, " _
                   & "  rod_ro_oid, " _
                   & "  rod_add_by, " _
                   & "  rod_add_date, " _
                   & "  rod_upd_by, " _
                   & "  rod_upd_date, " _
                   & "  rod_op, " _
                   & "  rod_start_date, " _
                   & "  rod_end_date, " _
                   & "  rod_wc_id, " _
                   & "  rod_desc, " _
                   & "  rod_mch_op, " _
                   & "  rod_tran_qty, " _
                   & "  rod_queue, " _
                   & "  rod_wait, " _
                   & "  rod_move, " _
                   & "  rod_run, " _
                   & "  rod_setup, " _
                   & "  rod_insheet_pct, " _
                   & "  rod_milestone, " _
                   & "  rod_sub_lead, " _
                   & "  rod_setup_men, " _
                   & "  rod_men_mch, " _
                   & "  rod_tool_code, " _
                   & "  rod_ptnr_id, " _
                   & "  rod_sub_cost,rod_seq,rod_conversion, " _
                   & "  wc_desc, " _
                   & "  code_name, " _
                   & "  ptnr_name, " _
                   & "  rod_dt " _
                   & "FROM  " _
                   & "  public.rod_det " _
                   & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
                   & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
                   & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
                   & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
                   & " where ro_id=" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_ro_id").ToString _
                   & "  order by rod_seq "

            Dim _wo_oid As String = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString

            Dim dt_routing As New DataTable
            dt_routing = GetTableData(sSQL)

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from wodr_routing where wodr_wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString + "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        Dim x As Integer = 1
                        For Each dr As DataRow In dt_routing.Rows
                            .Command.CommandType = CommandType.Text
                            If x = 1 Then
                                .Command.CommandText = "INSERT INTO  " _
                                   & "  public.wodr_routing " _
                                   & "( " _
                                   & "  wodr_uid, " _
                                   & "  wodr_wo_oid, " _
                                   & "  wodr_op, " _
                                   & "  wodr_start_date, " _
                                   & "  wodr_end_date, " _
                                   & "  wodr_desc, " _
                                   & "  wodr_wc_id, " _
                                   & "  wodr_yield_pct,wodr_qty_conversion,wodr_qty_in,wodr_qty_complete,wodr_qty_reject,wodr_qty_out, " _
                                   & "  wodr_seq " _
                                   & ")  " _
                                   & "VALUES ( " _
                                   & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                   & SetSetring(_wo_oid) & ",  " _
                                   & SetInteger(dr("rod_op")) & ",  " _
                                   & SetDateNTime00(dr("rod_start_date")) & ",  " _
                                   & SetDateNTime00(dr("rod_end_date")) & ",  " _
                                   & SetSetring(dr("rod_desc")) & ",  " _
                                   & SetInteger(dr("rod_wc_id")) & ",  " _
                                   & SetDec(dr("rod_insheet_pct")) & ",  " _
                                    & SetDec(dr("rod_conversion")) & ",  " _
                                   & SetDec(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_qty_ord")) & ",0,0,0, " _
                                   & SetInteger(x) & "  " _
                                   & ")"

                            Else
                                .Command.CommandText = "INSERT INTO  " _
                                   & "  public.wodr_routing " _
                                   & "( " _
                                   & "  wodr_uid, " _
                                   & "  wodr_wo_oid, " _
                                   & "  wodr_op, " _
                                   & "  wodr_start_date, " _
                                   & "  wodr_end_date, " _
                                   & "  wodr_desc, " _
                                   & "  wodr_wc_id, " _
                                   & "  wodr_yield_pct,wodr_qty_conversion, " _
                                   & "  wodr_seq " _
                                   & ")  " _
                                   & "VALUES ( " _
                                   & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                   & SetSetring(_wo_oid) & ",  " _
                                   & SetInteger(dr("rod_op")) & ",  " _
                                   & SetDateNTime00(dr("rod_start_date")) & ",  " _
                                   & SetDateNTime00(dr("rod_end_date")) & ",  " _
                                   & SetSetring(dr("rod_desc")) & ",  " _
                                   & SetInteger(dr("rod_wc_id")) & ",  " _
                                   & SetDec(dr("rod_insheet_pct")) & ",  " _
                                   & SetDec(dr("rod_conversion")) & ",  " _
                                   & SetInteger(x) & "  " _
                                   & ")"

                            End If


                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            x = x + 1
                        Next

                        sqlTran.Commit()
                        MsgBox("Release success")
                        help_load_data(True)
                    Catch ex As Exception
                        MsgBox(ex.Message)
                        sqlTran.Rollback()
                    End Try
                End With
            End Using


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtUpdateSod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtUpdateSod.Click
        Try
            Dim ssql As String

            ssql = "select * from wo_mstr where wo_so_oid is not null and wo_sod_oid is null order by wo_code"
            Dim dt_wo As New DataTable
            Dim dt_sod As New DataTable
            Dim _sod_oid As String = ""
            Dim x As Integer = 1
            dt_wo = GetTableData(ssql)
            For Each dr_wo As DataRow In dt_wo.Rows
                ssql = "select sod_oid from sod_det where sod_so_oid=" & SetSetring(dr_wo("wo_so_oid")) & " and sod_pt_id=" & SetInteger(dr_wo("wo_pt_id_prj"))
                dt_sod = GetTableData(ssql)
                _sod_oid = ""
                For Each dr_sod As DataRow In dt_sod.Rows
                    _sod_oid = dr_sod(0)
                Next
                If _sod_oid <> "" Then
                    ssql = "update wo_mstr set wo_sod_oid=" & SetSetring(_sod_oid) & " where wo_oid=" & SetSetring(dr_wo("wo_oid"))
                    DbRun(ssql)
                End If
                LabelControl4.Text = x & " of " & dt_wo.Rows.Count
                System.Windows.Forms.Application.DoEvents()
                x = x + 1
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class