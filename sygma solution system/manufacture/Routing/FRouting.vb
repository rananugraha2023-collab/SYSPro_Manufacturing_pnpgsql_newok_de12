Imports npgsql
Imports master_new.ModFunction

Public Class FRouting
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet

    Dim sSQLs As New ArrayList

    Private Sub FRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        ro_en_id.Properties.DataSource = dt_bantu
        ro_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ro_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ro_en_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ro_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Description", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "ro_active", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Is Default", "ro_is_default", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Cost Set", "cs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Indirect Total", "ro_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Insheet Total", "ro_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_master, "User Create", "ro_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ro_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "ro_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ro_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "rod_oid", False)
        add_column(gv_detail, "rod_ro_oid", False)
        add_column_copy(gv_detail, "Sequence", "rod_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail, "Operation", "rod_op", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Start Date", "rod_start_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "End Date", "rod_end_date", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Workcenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Insheet", "rod_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Run (Hours)", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        add_column_copy(gv_detail, "Qty Out Conversion", "rod_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "rod_oid", False)
        add_column(gv_edit, "rod_ro_oid", False)
        add_column_edit(gv_edit, "Sequence", "rod_seq", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "Operation Code", "rod_op", DevExpress.Utils.HorzAlignment.Far)
        'add_column(gv_edit, "Operation Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Start Date", "rod_start_date", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.DateTime, "d")
        'add_column_edit(gv_edit, "End Date", "rod_end_date", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.DateTime, "d")

        add_column(gv_edit, "rod_wc_id", False)
        add_column(gv_edit, "WorkCenter", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "rod_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Insheet", "rod_insheet_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Run (Hours)", "rod_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n5")
        add_column_edit(gv_edit, "Qty Out Conversion", "rod_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'rod_run

        'init_le(ro_cs_id, "cost_set", ro_en_id.EditValue)

        add_column(gv_indirect_edit, "rodi_type", False)
        add_column(gv_indirect_edit, "Indirect Type", "rodit_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_indirect_edit, "Remarks", "rodi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_indirect_edit, "Qty", "rodi_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_indirect_edit, "Amount", "rodi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_indirect_edit, "Amount Total", "rodi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_indirect_detail, "rodi_ro_oid", False)
        add_column(gv_indirect_detail, "rodi_type", False)
        add_column_copy(gv_indirect_detail, "Indirect Type", "rodit_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_indirect_detail, "Remarks", "rodi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_indirect_detail, "Qty", "rodi_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_indirect_detail, "Amount", "rodi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_indirect_detail, "Amount Total", "rodi_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        gv_edit.Columns("rod_seq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_detail.Columns("rod_seq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        gv_edit.OptionsCustomization.AllowSort = False
        gv_detail.OptionsCustomization.AllowSort = False
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ro_oid, " _
                & "  a.ro_dom_id, " _
                & "  a.ro_en_id, " _
                & "  b.en_desc, " _
                & "  a.ro_add_by, " _
                & "  a.ro_add_date, " _
                & "  a.ro_upd_by, " _
                & "  a.ro_upd_date, " _
                & "  a.ro_id, " _
                & "  a.ro_code, " _
                & "  a.ro_desc, " _
                & "  a.ro_active, " _
                & "  a.ro_dt, " _
                & "  a.ro_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1,ro_insheet, " _
                & "  d.pt_desc2,ro_total,ro_is_default " _
                & "FROM " _
                & "  public.en_mstr b " _
                & "  INNER JOIN public.ro_mstr a ON (b.en_id = a.ro_en_id) " _
                & "  LEFT OUTER JOIN public.pt_mstr d ON (a.ro_pt_id = d.pt_id) " _
                & "ORDER BY " _
                & "  a.ro_code"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
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
            & "  rod_sub_cost, " _
            & "  wc_desc,rod_seq,rod_conversion, " _
            & "  code_name, " _
            & "  ptnr_name, " _
            & "  rod_dt  " _
            & "FROM  " _
            & "  public.rod_det " _
            & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
            & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
            & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
            & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
            & " order by rod_seq "

        load_data_detail(sql, gc_detail, "detail")

        sql = "SELECT  " _
            & "  a.rodi_oid, " _
            & "  a.rodi_ro_oid, " _
            & "  a.rodi_type, " _
            & "  a.rodi_remarks, " _
            & "  a.rodi_qty, " _
            & "  a.rodi_amount, " _
            & "  a.rodi_total, " _
            & "  b.rodit_name " _
            & "FROM " _
            & "  public.rodi_indirect a " _
            & "  INNER JOIN public.rodi_type_mstr b ON (a.rodi_type = b.rodit_code) " _
            & "WHERE " _
            & "  a.rodi_ro_oid is not null " _
            & "ORDER BY " _
            & "  a.rodi_seq"

        load_data_detail(sql, gc_indirect_detail, "detail_indirect")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("rod_ro_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")

            gv_indirect_detail.Columns("rodi_ro_oid").FilterInfo = _
           New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rodi_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            gv_indirect_detail.BestFitColumns()



        Catch ex As Exception
        End Try
    End Sub

    'Public Overrides Function export_data() As Boolean
    '    Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
    '    Dim obj As Object
    '    If fileName <> "" Then
    '        obj = get_gv()
    '        If TypeOf obj Is DevExpress.XtraGrid.Views.Grid.GridView Then
    '            ExportTo(obj, New ExportXlsProvider(fileName))
    '        ElseIf TypeOf obj Is DevExpress.XtraPivotGrid.PivotGridControl Then
    '            obj.ExportToXls(fileName)
    '        End If
    '        OpenFile(fileName)
    '    End If
    'End Function

    Public Overrides Sub insert_data_awal()
        ro_en_id.ItemIndex = 0
        ro_active.EditValue = True
        ro_is_default.EditValue = True
        ro_code.Text = ""
        ro_desc.Text = ""
        ro_pt_id.Tag = ""
        ro_pt_id.Text = ""
        pt_desc1.Text = ""
        pt_desc2.Text = ""
        'ro_cs_id.ItemIndex = 0
        ro_total.EditValue = 0
        ro_en_id.Focus()

        Try
            XtraTabControl1.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                            & "  rod_oid, " _
                            & "  rod_ro_oid, " _
                            & "  rod_add_by, " _
                            & "  rod_add_date, " _
                            & "  rod_upd_by, " _
                            & "  rod_upd_date, " _
                            & "  rod_op,'' as op_name, " _
                            & "  rod_start_date, " _
                            & "  rod_end_date, " _
                            & "  rod_wc_id, " _
                            & "  rod_desc, " _
                            & "  rod_mch_op, " _
                            & "  rod_tran_qty,rod_seq, " _
                            & "  rod_queue, " _
                            & "  rod_wait, " _
                            & "  rod_move, " _
                            & "  rod_run, " _
                            & "  rod_setup, " _
                            & "  rod_insheet_pct,rod_conversion, " _
                            & "  rod_milestone, " _
                            & "  rod_sub_lead, " _
                            & "  rod_setup_men, " _
                            & "  rod_men_mch, " _
                            & "  rod_tool_code, " _
                            & "  rod_ptnr_id, " _
                            & "  rod_sub_cost, " _
                            & "  wc_desc, " _
                            & "  code_name, " _
                            & "  ptnr_name, " _
                            & "  rod_dt " _
                            & "FROM  " _
                            & "  public.rod_det " _
                            & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid " _
                            & " INNER JOIN wc_mstr on rod_wc_id= wc_id " _
                            & " INNER JOIN code_mstr on rod_tool_code= code_id " _
                            & " INNER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id " _
                            & " where public.rod_det.rod_add_by = 'm/-n'"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.rodi_oid, " _
                        & "  a.rodi_ro_oid, " _
                        & "  a.rodi_type, " _
                        & "  a.rodi_remarks, " _
                        & "  a.rodi_qty, " _
                        & "  a.rodi_amount, " _
                        & "  a.rodi_total, " _
                        & "  b.rodit_name " _
                        & "FROM " _
                        & "  public.rodi_indirect a " _
                        & "  INNER JOIN public.rodi_type_mstr b ON (a.rodi_type = b.rodit_code) " _
                        & "WHERE " _
                        & "  a.rodi_ro_oid is null " _
                        & "ORDER BY " _
                        & "  a.rodi_seq"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_indirect")
                    gc_indirect_edit.DataSource = ds_edit.Tables(1)
                    gv_indirect_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean
        Dim i As Integer

        Dim _ro_oid As Guid
        _ro_oid = Guid.NewGuid

        Dim _ro_id As Integer
        _ro_id = SetInteger(func_coll.GetID("ro_mstr", ro_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", ro_en_id.EditValue.ToString))

        Dim _total_yield As Double = 0.0
        Dim _total As Double = 0

        'For x As Integer = 0 To gv_edit.RowCount - 1
        '    'set_amount(x)
        '    _total += SetNumber(gv_edit.GetRowCellValue(x, "rod_lbr_amount")) + _
        '            SetNumber(gv_edit.GetRowCellValue(x, "rod_bdn_amount")) + _
        '            SetNumber(gv_edit.GetRowCellValue(x, "rod_sbc_amount"))
        'Next

        For i = 0 To ds_edit.Tables("insert_edit_indirect").Rows.Count - 1
            _total += SetNumber(ds_edit.Tables(1).Rows(i).Item("rodi_total"))
        Next
        ro_total.EditValue = _total

        'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
        '    _total_yield = _total_yield * SetNumber(ds_edit.Tables(0).Rows(i).Item("rod_insheet_pct"))
        'Next

        calc_yield()
        sSQLs.Clear()

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
                                            & "  public.ro_mstr " _
                                            & "( " _
                                            & "  ro_oid, " _
                                            & "  ro_dom_id, " _
                                            & "  ro_en_id, " _
                                            & "  ro_add_by, " _
                                            & "  ro_add_date, " _
                                            & "  ro_id, " _
                                            & "  ro_code,ro_insheet, " _
                                            & "  ro_desc,ro_pt_id, " _
                                            & "  ro_active,ro_total,ro_is_default, " _
                                            & "  ro_dt " _
                                            & ") " _
                                            & "VALUES (" _
                                            & SetSetring(_ro_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(ro_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetInteger(_ro_id) & ",  " _
                                            & SetSetring(ro_code.Text) & ",  " _
                                            & SetDec(ro_insheet.EditValue) & ", " _
                                            & SetSetring(ro_desc.Text) & ",  " _
                                            & SetInteger(ro_pt_id.Tag) & ", " _
                                            & SetBitYN(ro_active.EditValue) & ",  " _
                                            & SetDbl(ro_total.EditValue) & ",  " _
                                            & SetBitYN(ro_is_default.EditValue) & ",  " _
                                            & " current_timestamp " & "  " _
                                            & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If ro_is_default.EditValue = True Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                    & "  public.ro_mstr   " _
                                    & "SET  " _
                                    & "  ro_is_default = 'N'  " _
                                    & "WHERE  " _
                                    & "  ro_en_id = " & SetInteger(ro_en_id.EditValue) & " AND  " _
                                    & "  ro_pt_id = " & SetInteger(ro_pt_id.Tag) & " AND " _
                                    & "  ro_oid <> " & SetSetring(_ro_oid.ToString) & "  "

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.rod_det " _
                                            & "( " _
                                            & "  rod_oid, " _
                                            & "  rod_ro_oid, " _
                                            & "  rod_add_by, " _
                                            & "  rod_add_date, " _
                                            & "  rod_op, " _
                                            & "  rod_start_date, " _
                                            & "  rod_end_date, " _
                                            & "  rod_wc_id, " _
                                            & "  rod_desc, " _
                                            & "  rod_mch_op, " _
                                            & "  rod_tran_qty,rod_conversion, " _
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
                                            & "  rod_sub_cost,rod_seq, " _
                                            & "  rod_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_op")) & ",  " _
                                            & SetDate(ds_edit.Tables("insert_edit").Rows(i).Item("rod_start_date")) & ",  " _
                                            & SetDate(ds_edit.Tables("insert_edit").Rows(i).Item("rod_end_date")) & ",  " _
                                            & ds_edit.Tables("insert_edit").Rows(i).Item("rod_wc_id") & ",  " _
                                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_desc")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_mch_op")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_tran_qty")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_conversion")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_queue")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_wait")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_move")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_run")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_setup")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_insheet_pct")) & ",  " _
                                            & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("rod_milestone").ToString.ToUpper) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_sub_lead")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_setup_men")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_men_mch")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_tool_code")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_ptnr_id")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_sub_cost")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("insert_edit").Rows(i).Item("rod_seq")) & ",  " _
                                            & "current_timestamp)"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_indirect").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.rodi_indirect " _
                                        & "( " _
                                        & "  rodi_oid, " _
                                        & "  rodi_ro_oid, " _
                                        & "  rodi_type, " _
                                        & "  rodi_remarks, " _
                                        & "  rodi_qty, " _
                                        & "  rodi_amount, " _
                                        & "  rodi_total, " _
                                        & "  rodi_seq " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetSetring(ds_edit.Tables(1).Rows(i).Item("rodi_type")) & ",  " _
                                        & SetSetring(ds_edit.Tables(1).Rows(i).Item("rodi_remarks")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_qty")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_amount")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_total")) & ",  " _
                                        & SetDec(i) & "  " _
                                        & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


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
                        set_row(Trim(_ro_oid.ToString), "ro_oid")
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        '*********************
        'Cek UM
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
                MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
            If SetNumber(ds_edit.Tables(0).Rows(i).Item("rod_conversion")) = 0 Then
                MessageBox.Show("Conversion Can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_tool_code")) = True Then
        '        MessageBox.Show("Tool Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_ptnr_id")) = True Then
        '        MessageBox.Show("Partner Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_milestone")) = True Then
        '        MessageBox.Show("Milestone Can't Empty.. Fill with (Y/N)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            ro_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ro_oid_mstr = .Item("ro_oid").ToString
                ro_en_id.EditValue = .Item("ro_en_id")
                ro_active.EditValue = SetBitYNB(.Item("ro_active"))
                ro_is_default.EditValue = SetBitYNB(.Item("ro_is_default"))
                ro_code.Text = SetString(.Item("ro_code"))
                ro_desc.Text = SetString(.Item("ro_desc"))
                ro_pt_id.Tag = SetNumber(.Item("ro_pt_id"))
                ro_pt_id.Text = SetString(.Item("pt_code"))
                pt_desc1.Text = SetString(.Item("pt_desc1"))
                pt_desc2.Text = SetString(.Item("pt_desc2"))
                ro_total.EditValue = .Item("ro_total")
                ro_insheet.EditValue = .Item("ro_insheet")
            End With

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT " _
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
                            & "  rod_insheet_pct,rod_conversion, " _
                            & "  rod_milestone, " _
                            & "  rod_sub_lead, " _
                            & "  rod_setup_men, " _
                            & "  rod_men_mch, " _
                            & "  rod_tool_code, " _
                            & "  rod_ptnr_id, " _
                            & "  rod_sub_cost, " _
                            & "  wc_desc,rod_seq, " _
                            & "  code_name, " _
                            & "  ptnr_name, " _
                            & "  rod_dt " _
                            & "FROM  " _
                            & "  public.rod_det " _
                            & " INNER JOIN ro_mstr on ro_oid= rod_ro_oid" _
                            & " INNER JOIN wc_mstr on rod_wc_id= wc_id" _
                            & " LEFT OUTER JOIN code_mstr on rod_tool_code= code_id" _
                            & " LEFT OUTER JOIN ptnr_mstr on rod_ptnr_id= ptnr_id" _
                            & " where public.rod_det.rod_ro_oid = '" + ds.Tables(0).Rows(row).Item("ro_oid").ToString + "'" _
                            & " order by rod_seq "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail_upd")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.rodi_oid, " _
                            & "  a.rodi_ro_oid, " _
                            & "  a.rodi_type, " _
                            & "  a.rodi_remarks, " _
                            & "  a.rodi_qty, " _
                            & "  a.rodi_amount, " _
                            & "  a.rodi_total, " _
                            & "  b.rodit_name " _
                            & "FROM " _
                            & "  public.rodi_indirect a " _
                            & "  INNER JOIN public.rodi_type_mstr b ON (a.rodi_type = b.rodit_code) " _
                            & "WHERE " _
                            & "  a.rodi_ro_oid = '" + ds.Tables(0).Rows(row).Item("ro_oid").ToString + "' " _
                            & "ORDER BY " _
                            & "  a.rodi_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_indirect")

                        gc_indirect_edit.DataSource = ds_edit.Tables(1)
                        gv_indirect_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        'Dim _total As Double = 0
        'For x As Integer = 0 To gv_edit.RowCount - 1
        '    set_amount(x)
        '    _total += SetNumber(gv_edit.GetRowCellValue(x, "rod_lbr_amount")) + _
        '            SetNumber(gv_edit.GetRowCellValue(x, "rod_bdn_amount")) + _
        '            SetNumber(gv_edit.GetRowCellValue(x, "rod_sbc_amount"))
        'Next
        'ro_total.EditValue = _total

        Dim _total_yield As Double = 0.0
        Dim _total As Double = 0


        For i = 0 To ds_edit.Tables("insert_edit_indirect").Rows.Count - 1
            _total += SetNumber(ds_edit.Tables(1).Rows(i).Item("rodi_total"))
        Next
        ro_total.EditValue = _total

        calc_yield()
        sSQLs.Clear()
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
                                & "  public.ro_mstr   " _
                                & "SET  " _
                                & "  ro_en_id = " & SetInteger(ro_en_id.EditValue) & ",  " _
                                & "  ro_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  ro_upd_date = current_timestamp,  " _
                                & "  ro_code = " & SetSetring(ro_code.Text) & ",  " _
                                & "  ro_pt_id = " & SetInteger(ro_pt_id.Tag) & ",  " _
                                & "  ro_desc = " & SetSetring(ro_desc.Text) & ",  " _
                                & "  ro_active = " & SetBitYN(ro_active.EditValue) & ",  " _
                                & "  ro_is_default = " & SetBitYN(ro_is_default.EditValue) & ",  " _
                                & "  ro_total = " & SetDbl(ro_total.EditValue) & ",  " _
                                & "  ro_insheet = " & SetDbl(ro_insheet.EditValue) & ",  " _
                                & "  ro_dt = current_timestamp  " _
                                & "  " _
                                & "WHERE  " _
                                & "  ro_oid = " & SetSetring(_ro_oid_mstr.ToString) & "  "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If ro_is_default.EditValue = True Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                    & "  public.ro_mstr   " _
                                    & "SET  " _
                                    & "  ro_is_default = 'N'  " _
                                    & "WHERE  " _
                                    & "  ro_en_id = " & SetInteger(ro_en_id.EditValue) & " AND  " _
                                    & "  ro_pt_id = " & SetInteger(ro_pt_id.Tag) & "  and  " _
                                    & "  ro_oid <> " & SetSetring(_ro_oid_mstr.ToString) & "  "
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rod_det where rod_ro_oid = '" + _ro_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rodi_indirect where rodi_ro_oid = '" + _ro_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.rod_det " _
                                            & "( " _
                                            & "  rod_oid, " _
                                            & "  rod_ro_oid, " _
                                            & "  rod_upd_by, " _
                                            & "  rod_upd_date, " _
                                            & "  rod_op, " _
                                            & "  rod_start_date, " _
                                            & "  rod_end_date, " _
                                            & "  rod_wc_id, " _
                                            & "  rod_desc, " _
                                            & "  rod_mch_op, " _
                                            & "  rod_tran_qty,rod_conversion, " _
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
                                            & "  rod_sub_cost,rod_seq, " _
                                            & "  rod_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid_mstr) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " current_timestamp " & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_op")) & ",  " _
                                            & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("rod_start_date")) & ",  " _
                                            & SetDate(ds_edit.Tables("detail_upd").Rows(i).Item("rod_end_date")) & ",  " _
                                            & ds_edit.Tables("detail_upd").Rows(i).Item("rod_wc_id") & ",  " _
                                            & SetSetringDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_desc")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_mch_op")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_tran_qty")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_conversion")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_queue")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_wait")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_move")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_run")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_setup")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_insheet_pct")) & ",  " _
                                            & SetSetringDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_milestone").ToString.ToUpper) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_sub_lead")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_setup_men")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_men_mch")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_tool_code")) & ",  " _
                                            & SetIntegerDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_ptnr_id")) & ",  " _
                                            & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_sub_cost")) & ",  " _
                                             & SetDblDB(ds_edit.Tables("detail_upd").Rows(i).Item("rod_seq")) & ",  " _
                                            & "current_timestamp" & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_indirect").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.rodi_indirect " _
                                        & "( " _
                                        & "  rodi_oid, " _
                                        & "  rodi_ro_oid, " _
                                        & "  rodi_type, " _
                                        & "  rodi_remarks, " _
                                        & "  rodi_qty, " _
                                        & "  rodi_amount, " _
                                        & "  rodi_total, " _
                                        & "  rodi_seq " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid_mstr) & ",  " _
                                        & SetSetring(ds_edit.Tables(1).Rows(i).Item("rodi_type")) & ",  " _
                                        & SetSetring(ds_edit.Tables(1).Rows(i).Item("rodi_remarks")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_qty")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_amount")) & ",  " _
                                        & SetDec(ds_edit.Tables(1).Rows(i).Item("rodi_total")) & ",  " _
                                        & SetDec(i) & "  " _
                                        & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

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
                        set_row(Trim(_ro_oid_mstr.ToString), "ro_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
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
            sSQLs.Clear()
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ro_mstr where ro_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from rod_det where rod_ro_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
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
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
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

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        'If e.Column.Name = "rod_milestone" Then
        '    If (gv_edit.GetRowCellValue(e.RowHandle, "rod_milestone") <> "N") And (gv_edit.GetRowCellValue(e.RowHandle, "rod_milestone") <> "Y") Then
        '        If gv_edit.GetRowCellValue(e.RowHandle, "rod_milestone").ToString.ToUpper = "Y" Then
        '            gv_edit.SetRowCellValue(e.RowHandle, "rod_milestone", "Y")
        '        Else
        '            gv_edit.SetRowCellValue(e.RowHandle, "rod_milestone", "N")
        '        End If
        '    End If
        'End If
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
        Dim _rod_en_id As Integer = ro_en_id.EditValue

        If _col = "wc_desc" Then
            Dim frm As New FWCSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _rod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FToolSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _rod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ptnr_name" Then
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _rod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_code_lbr" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._obj = _col
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_code_bdn" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._obj = _col
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_code_sbc" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._obj = _col
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "rod_op" Or _col = "op_name" Then
            Dim frm As New FOpSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        Dim _now As DateTime
        Dim sSQL As String
        _now = func_coll.get_now
        With gv_edit
            '.SetRowCellValue(e.RowHandle, "rod_op", 0)
            '.SetRowCellValue(e.RowHandle, "rod_start_date", CDate("01/01/2000"))
            '.SetRowCellValue(e.RowHandle, "rod_end_date", CDate("31/12/9999"))
            .SetRowCellValue(e.RowHandle, "rod_mch_op", 0)
            .SetRowCellValue(e.RowHandle, "rod_tran_qty", 0)
            .SetRowCellValue(e.RowHandle, "rod_queue", 0)
            .SetRowCellValue(e.RowHandle, "rod_wait", 0)
            .SetRowCellValue(e.RowHandle, "rod_move", 0)
            .SetRowCellValue(e.RowHandle, "rod_run", 0)
            .SetRowCellValue(e.RowHandle, "rod_conversion", 1)
            .SetRowCellValue(e.RowHandle, "rod_setup", 0)
            .SetRowCellValue(e.RowHandle, "rod_insheet_pct", 0.0)
            .SetRowCellValue(e.RowHandle, "rod_milestone", "N")
            .SetRowCellValue(e.RowHandle, "rod_sub_lead", 0)
            .SetRowCellValue(e.RowHandle, "rod_setup_men", 0)
            .SetRowCellValue(e.RowHandle, "rod_men_mch", 0)
            .SetRowCellValue(e.RowHandle, "rod_sub_cost", 0)
            .SetRowCellValue(e.RowHandle, "rod_seq", gv_edit.RowCount)

            'sSQL = "select dom_oid, dom_id, dom_code, dom_desc, dom_active, dom_dt, " + _
            '         " dom_base_cur_id, cu_name, " + _
            '         " ac_pl.ac_code as ac_pl_code, ac_pl.ac_name as ac_pl_name, " + _
            '         " ac_re.ac_code as ac_re_code, ac_re.ac_name as ac_re_name, " + _
            '         " ac_la.ac_code as ac_la_code, ac_la.ac_name as ac_la_name, " + _
            '         " ac_las.ac_code as ac_las_code, ac_las.ac_name as ac_las_name, " + _
            '         " ac_psa.ac_code as ac_psa_code, ac_psa.ac_name as ac_psa_name, " + _
            '         " ac_lbr.ac_code as ac_lbr_code, ac_lbr.ac_name as ac_lbr_name, " + _
            '         " ac_ovh.ac_code as ac_ovh_code, ac_ovh.ac_name as ac_ovh_name, " + _
            '         " ac_bdn.ac_code as ac_bdn_code, ac_bdn.ac_name as ac_bdn_name, " + _
            '         " ac_mtl.ac_code as ac_mtl_code, ac_mtl.ac_name as ac_mtl_name, " + _
            '         " ac_sbc.ac_code as ac_sbc_code, ac_sbc.ac_name as ac_sbc_name, " + _
            '         " dom_base_cur_id, cu_name, dom_pl_ac, dom_re_ac, dom_la_ac, dom_las_ac, dom_psa_ac,dom_lbr_ac_id,dom_ovh_ac_id,dom_bdn_ac_id,dom_mtl_ac_id,dom_sbc_ac_id " + _
            '         " from dom_mstr " + _
            '         " left outer join cu_mstr on cu_id = dom_base_cur_id " + _
            '         " left outer join ac_mstr ac_pl on ac_pl.ac_id = dom_pl_ac " + _
            '         " left outer join ac_mstr ac_re on ac_re.ac_id = dom_re_ac " + _
            '         " left outer join ac_mstr ac_la on ac_la.ac_id = dom_la_ac " + _
            '         " left outer join ac_mstr ac_las on ac_las.ac_id = dom_las_ac " + _
            '         " left outer join ac_mstr ac_psa on ac_psa.ac_id = dom_psa_ac " + _
            '          " left outer join ac_mstr ac_lbr on ac_lbr.ac_id = dom_lbr_ac_id " + _
            '         " left outer join ac_mstr ac_ovh on ac_ovh.ac_id = dom_ovh_ac_id " + _
            '         " left outer join ac_mstr ac_bdn on ac_bdn.ac_id = dom_bdn_ac_id " + _
            '         " left outer join ac_mstr ac_mtl on ac_mtl.ac_id = dom_mtl_ac_id " + _
            '         " left outer join ac_mstr ac_sbc on ac_sbc.ac_id = dom_sbc_ac_id " _
            '         & "Where dom_id=" & master_new.ClsVar.sdom_id

            'Dim dt As New DataTable
            'dt = master_new.PGSqlConn.GetTableData(sSQL)


            '.SetRowCellValue(e.RowHandle, "rod_lbr_ac_id", dt.Rows(0).Item("dom_lbr_ac_id"))
            '.SetRowCellValue(e.RowHandle, "ac_code_lbr", dt.Rows(0).Item("ac_lbr_code"))
            '.SetRowCellValue(e.RowHandle, "ac_name_lbr", dt.Rows(0).Item("ac_lbr_name"))

            '.SetRowCellValue(e.RowHandle, "rod_bdn_ac_id", dt.Rows(0).Item("dom_bdn_ac_id"))
            '.SetRowCellValue(e.RowHandle, "ac_code_bdn", dt.Rows(0).Item("ac_bdn_code"))
            '.SetRowCellValue(e.RowHandle, "ac_name_bdn", dt.Rows(0).Item("ac_bdn_name"))

            '.SetRowCellValue(e.RowHandle, "rod_sbc_ac_id", dt.Rows(0).Item("dom_sbc_ac_id"))
            '.SetRowCellValue(e.RowHandle, "ac_code_sbc", dt.Rows(0).Item("ac_sbc_code"))
            '.SetRowCellValue(e.RowHandle, "ac_name_sbc", dt.Rows(0).Item("ac_sbc_name"))


            .BestFitColumns()
        End With
    End Sub

    Private Sub ro_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ro_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch
        frm.set_win(Me)
        frm._en_id = ro_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub ro_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ro_en_id.EditValueChanged
        'init_le(ro_cs_id, "cost_set", ro_en_id.EditValue)
    End Sub

    Private Sub set_amount(ByVal par_row As Integer)
        Dim sSQL As String
        Try
            'Dim _rod_lbr_amount, _rod_bdn_amount As Double

            '_rod_bdn_amount = 0
            '_rod_lbr_amount = 0

            'sSQL = "SELECT  " _
            '    & "  a.wc_mch_bdn_rate, " _
            '    & "  a.wc_mch_op, " _
            '    & "  a.wc_setup_men, " _
            '    & "  a.wc_setup_rate, " _
            '    & "  a.wc_men_mch, " _
            '    & "  a.wc_lbr_rate " _
            '    & "FROM " _
            '    & "  public.wc_mstr a " _
            '    & "WHERE " _
            '    & "  a.wc_id =" & gv_edit.GetRowCellValue(par_row, "rod_wc_id")
            'Dim dr As DataRow
            'dr = master_new.PGSqlConn.GetRowInfo(sSQL)

            '_rod_lbr_amount = (SetNumber(dr("wc_setup_men")) * SetNumber(dr("wc_setup_rate")) * _
            '                   SetNumber(gv_edit.GetRowCellValue(par_row, "rod_setup"))) + _
            '                   (SetNumber(dr("wc_men_mch")) * SetNumber(dr("wc_lbr_rate")) * _
            '                    SetNumber(gv_edit.GetRowCellValue(par_row, "rod_run")))

            '_rod_bdn_amount = SetNumber(dr("wc_mch_bdn_rate")) * SetNumber(dr("wc_mch_op")) * SetNumber(gv_edit.GetRowCellValue(par_row, "rod_run"))

            'gv_edit.SetRowCellValue(par_row, "rod_lbr_amount", _rod_lbr_amount)
            'gv_edit.SetRowCellValue(par_row, "rod_bdn_amount", _rod_bdn_amount)
            'gv_edit.SetRowCellValue(par_row, "rod_sbc_amount", SetNumber(gv_edit.GetRowCellValue(par_row, "rod_sub_cost")))
            'gv_edit.BestFitColumns()
        Catch ex As Exception
            'Pesan(Err)
        End Try
    End Sub
    Private Sub calc_yield()
        Try
            'Dim _total_yield As Double = 0.0
            ds_edit.AcceptChanges()
            Dim _insheet As Double = 0.0
            For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                If _insheet < SetNumber(ds_edit.Tables(0).Rows(i).Item("rod_insheet_pct")) Then
                    _insheet = SetNumber(ds_edit.Tables(0).Rows(i).Item("rod_insheet_pct"))
                End If
            Next

            'If _total_yield > 0.0 Then
            '    If ds_edit.Tables(0).Rows.Count > 1 Then
            '        _total_yield = _total_yield / ds_edit.Tables(0).Rows.Count
            '    End If

            'End If


            ro_insheet.EditValue = _insheet
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    'Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
    '    'gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
    '    'set_amount(e.RowHandle)
    'End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        'set_amount(e.RowHandle)
        calc_yield()
    End Sub

    Private Sub gv_indirect_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_indirect_edit.CellValueChanged
        Try

            If e.Column.Name = "rodi_qty" Then

                gv_indirect_edit.SetRowCellValue(e.RowHandle, "rodi_total", SetNumber(gv_indirect_edit.GetRowCellValue(e.RowHandle, "rodi_qty")) _
                                                    * SetNumber(gv_indirect_edit.GetRowCellValue(e.RowHandle, "rodi_amount")))
            ElseIf e.Column.Name = "rodi_amount" Then
                gv_indirect_edit.SetRowCellValue(e.RowHandle, "rodi_total", SetNumber(gv_indirect_edit.GetRowCellValue(e.RowHandle, "rodi_qty")) _
                                                   * SetNumber(gv_indirect_edit.GetRowCellValue(e.RowHandle, "rodi_amount")))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_indirect_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_indirect_edit.DoubleClick
        Try
            Dim _col As String = gv_indirect_edit.FocusedColumn.Name
            Dim _row As Integer = gv_indirect_edit.FocusedRowHandle

            If _col = "rodit_name" Then
                Dim frm As New FRodiTypeSearch
                frm.set_win(Me)
                frm._row = _row
                frm.type_form = True
                frm.ShowDialog()

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_indirect_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_indirect_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "rodi_qty", 1.0)
            .SetRowCellValue(e.RowHandle, "rodi_amount", 0.0)
        End With
    End Sub

    Private Sub gv_indirect_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_indirect_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            gv_indirect_edit_DoubleClick(sender, e)
        End If
    End Sub

    Private Sub ro_copy_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ro_copy.ButtonClick
        Dim frm As New FRoutingSearch
        frm.set_win(Me)
        frm._en_id = ro_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub ro_copy_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ro_copy.EditValueChanged

    End Sub
End Class
