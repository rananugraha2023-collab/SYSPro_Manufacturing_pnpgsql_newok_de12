Imports Npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraGrid.Views.Base

Public Class FGenFSN
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList
    Public _genfsn_qrtr_ids As String

    Private Property inv_fsn_end_date As Object


    Private Sub FGenFSN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()


        genfsn_year.EditValue = DateTime.Now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        genfsn_en_id.Properties.DataSource = dt_bantu
        genfsn_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        genfsn_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        genfsn_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_qrtr_code_id())
        'genfsn_qrtr_code_id.Properties.DataSource = dt_bantu
        'genfsn_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_code_name").ToString
        'genfsn_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("qrtr_code_id").ToString
        'genfsn_qrtr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "genfsn_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Name", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Type", "qrtr_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Interval", "qrtr_period_count", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Year", "genfsn_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "genfsn_start_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date", "genfsn_end_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "genfsn_remarks", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "All Child", "genfsn_all_child", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Final/Used", "genfsn_ce_final", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "genfsn_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "genfsn_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "genfsn_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "genfsn_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "genfsnd_pt_id", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Group", "ptgroup_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Methode", "pt_methode_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Total Qty", "genfsnd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Clasification", "genfsnd_fsn_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "genfsnd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "code_id", False)
        add_column_copy(gv_edit, "Group", "code_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "genfsnd_pt_id", False)
        add_column_copy(gv_edit, "Methode", "pt_methode_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Total Qty", "genfsnd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Clasification", "stock_status", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.genfsn_oid, " _
                & "  a.genfsn_dom_id, " _
                & "  a.genfsn_en_id, " _
                & "  c.en_desc, " _
                & "  a.genfsn_qrtr_id, " _
                & "  b.qrtr_code, " _
                & "  b.qrtr_name, " _
                & "  a.genfsn_year, " _
                & "  a.genfsn_remarks, " _
                & "  a.genfsn_add_by, " _
                & "  a.genfsn_add_date, " _
                & "  a.genfsn_upd_by, " _
                & "  a.genfsn_upd_date, " _
                & "  a.genfsn_code, " _
                & "  a.genfsn_all_child, " _
                & "  a.genfsn_ce_final, " _
                & "  a.genfsn_qrtr_code_id, " _
                & "  a.genfsn_qrtr_code_name, " _
                & "  b.qrtr_code_id, " _
                & "  b.qrtr_code_name, " _
                & "  a.genfsn_start_dt, " _
                & "  a.genfsn_end_dt, " _
                & "  a.genfsn_fsncat_id, " _
                & "  b.qrtr_period_count " _
                & "FROM " _
                & "  public.genfsn_mstr a " _
                & "  INNER JOIN public.qrtr_mstr b ON (a.genfsn_qrtr_id = b.qrtr_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.genfsn_en_id = c.en_id) " _
                & "   where genfsn_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by genfsn_code"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try


        sql = "SELECT  " _
            & "  a.genfsnd_oid, " _
            & "  a.genfsnd_genfsn_oid, " _
            & "  a.genfsnd_seq, " _
            & "  a.genfsnd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.genfsnd_ptgroup, " _
            & "  a.genfsnd_fsn_cat, " _
            & "  a.genfsnd_fsn_name, " _
            & "  a.genfsnd_methode_id, " _
            & "  a.genfsnd_qty, " _
            & "  c.code_name AS ptgroup_code_name, " _
            & "  ptnr_man_methode_mstr.code_name AS pt_methode_name " _
            & "FROM " _
            & "  public.genfsnd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.genfsnd_pt_id = b.pt_id) " _
            & "  INNER JOIN public.code_mstr c ON (a.genfsnd_ptgroup = c.code_id) " _
            & "  LEFT OUTER JOIN public.code_mstr ptnr_man_methode_mstr ON (b.pt_methode_id = ptnr_man_methode_mstr.code_id) " _
            & "WHERE " _
            & "  a.genfsnd_genfsn_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("genfsn_oid").ToString & "' " _
            & "ORDER BY " _
            & "  genfsnd_seq"


        load_data_detail(sql, gc_detail, "detail")
        gv_detail.BestFitColumns()
    End Sub

    Public Overrides Sub relation_detail()
        Try
            'load_data_grid_detail()
            'gv_detail.Columns("rod_ro_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        genfsn_en_id.ItemIndex = 0
        'genfsn_qrtr_id.ItemIndex = 0
        genfsn_year.EditValue = ""
        genfsn_remarks.Text = ""
        genfsn_en_id.Focus()
        genfsn_all_child.EditValue = True

        genfsn_start_dt.EditValue = ""
        genfsn_end_dt.EditValue = ""

        genfsn_qrtr_id.Text = ""
        _genfsn_qrtr_ids = ""

        '_genfsn_qrtr_ids = -1
        'genfsn_qrtr_id.Text = ""
        genfsn_ce_final.EditValue = False

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
                    .SQL = "SELECT  " _
                        & "  a.genfsnd_oid, " _
                        & "  a.genfsnd_genfsn_oid, " _
                        & "  a.genfsnd_seq, " _
                        & "  a.genfsnd_pt_id, " _
                        & "  b.pt_id, " _
                        & "  b.pt_code, " _
                        & "  b.pt_desc1, " _
                        & "  b.pt_desc2, " _
                        & "  a.genfsnd_ptgroup, " _
                        & "  a.genfsnd_fsn_cat, " _
                        & "  a.genfsnd_fsn_name, " _
                        & "  a.genfsnd_methode_id, " _
                        & "  a.genfsnd_qty, " _
                        & "  '' as stock_status, " _
                        & "  c.code_name, " _
                        & "  ptnr_man_methode_mstr.code_name AS pt_methode_name " _
                        & "FROM " _
                        & "  public.genfsnd_det a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.genfsnd_pt_id = b.pt_id) " _
                        & "  INNER JOIN public.code_mstr c ON (a.genfsnd_ptgroup = c.code_id) " _
                        & "  LEFT OUTER JOIN public.code_mstr ptnr_man_methode_mstr ON (b.pt_methode_id = ptnr_man_methode_mstr.code_id) " _
                        & "WHERE " _
                        & "  a.genfsnd_genfsn_oid is null"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
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

        sSQLs.Clear()

        Dim _code As String
        _code = func_coll.get_transaction_number("FSN", genfsn_en_id.GetColumnValue("en_code"), "genfsn_mstr", "genfsn_code")

        'Dim _ro_id As Integer
        '_ro_id = SetInteger(func_coll.GetID("fcs_mstr", fcs_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", fcs_en_id.EditValue.ToString))
        Dim _genfsn_year As Integer = Convert.ToDateTime(genfsn_year.EditValue).Year

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO public.genfsn_mstr " _
                                            & "( " _
                                            & "  genfsn_oid, " _
                                            & "  genfsn_dom_id, " _
                                            & "  genfsn_en_id, " _
                                            & "  genfsn_qrtr_id, " _
                                            & "  genfsn_qrtr_code_id, " _
                                            & "  genfsn_year, " _
                                            & "  genfsn_remarks, " _
                                            & "  genfsn_add_by, " _
                                            & "  genfsn_add_date, " _
                                            & "  genfsn_code, " _
                                            & "  genfsn_all_child, " _
                                            & "  genfsn_ce_final, " _
                                            & "  genfsn_start_dt, " _
                                            & "  genfsn_end_dt, " _
                                            & "  genfsn_fsncat_id " _
                                            & ") " _
                                            & "VALUES ( " _
                                            & SetSetring(_ro_oid.ToString) & ", " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ", " _
                                            & SetInteger(genfsn_en_id.EditValue) & ", " _
                                            & SetSetring(_genfsn_qrtr_ids) & ", " _
                                            & SetInteger(genfsn_qrtr_code_id.Tag) & ", " _
                                            & SetInteger(_genfsn_year) & ", " _
                                            & SetSetring(genfsn_remarks.EditValue) & ", " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                                            & SetSetring(_code) & ", " _
                                            & SetBitYN(genfsn_all_child.EditValue) & ", " _
                                            & SetBitYN(genfsn_ce_final.EditValue) & ", " _
                                            & SetDate(genfsn_start_dt.DateTime) & ", " _
                                            & SetDate(genfsn_end_dt.DateTime) & ", " _
                                            & SetInteger(genfsn_fsncat_id.EditValue) _
                                            & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.genfsnd_det " _
                                        & "( " _
                                        & "  genfsnd_oid, " _
                                        & "  genfsnd_genfsn_oid, " _
                                        & "  genfsnd_pt_id, " _
                                        & "  genfsnd_ptgroup, " _
                                        & "  genfsnd_fsn_cat, " _
                                        & "  genfsnd_fsn_name, " _
                                        & "  genfsnd_methode_id, " _
                                        & "  genfsnd_qty, " _
                                        & "  genfsnd_seq " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_pt_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_ptgroup")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_fsn_cat")) & ",  " _
                                        & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_fsn_name")) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_methode_id")) & ",  " _
                                        & SetDbl(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_qty")) & ",  " _
                                        & SetInteger(i) & "  " _
                                        & ")"

                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("pt_id"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_ptgroup"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_fsn_cat"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_fsn_name"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_methode_id"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_po"))) & ",  " _
                            '& SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genfsnd_qty"))) & ",  " _



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_ro_oid.ToString), "genfsn_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As NpgsqlException
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

        If SetString(genfsn_year.EditValue) = "" Then
            Box("Year can't empt")
            Return False
            Exit Function
        End If
        '*********************
        'Cek UM
        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
        '        MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next
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
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return False
        'If MyBase.edit_data = True Then
        '    genpr_en_id.Focus()

        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _ro_oid_mstr = .Item("fcs_oid")
        '        genpr_en_id.EditValue = .Item("fcs_en_id")
        '        genpr_qrtr_id.EditValue = .Item("fcs_qrtr_id")
        '        genpr_remarks.EditValue = .Item("fcs_remarks")
        '        genpr_year.EditValue = .Item("fcs_year")
        '    End With

        '    ds_edit = New DataSet
        '    'ds_update_related = New DataSet
        '    Try
        '        Using objcb As New master_new.WDABasepgsql("", "")
        '            With objcb
        '                .SQL = "SELECT  " _
        '                    & "  a.fcsd_oid, " _
        '                    & "  a.fcsd_fcs_oid, " _
        '                    & "  a.fcsd_pt_id, " _
        '                    & "  b.pt_code, " _
        '                    & "  b.pt_desc1, " _
        '                    & "  b.pt_desc2, " _
        '                    & "  d.gr_name, " _
        '                    & "  a.fcsd_01_amount, " _
        '                    & "  a.fcsd_02_amount, " _
        '                    & "  a.fcsd_03_amount, " _
        '                    & "  a.fcsd_total_amount, " _
        '                    & "  a.fcsd_buffer_amount, " _
        '                    & "  a.fcsd_seq " _
        '                    & "FROM " _
        '                    & "  public.fcsd_det a " _
        '                    & "  INNER JOIN public.pt_mstr b ON (a.fcsd_pt_id = b.pt_id) " _
        '                    & "  INNER JOIN public.ptgr_mstr c ON (b.pt_id = c.ptgr_pt_id) " _
        '                    & "  INNER JOIN public.gr_mstr d ON (c.ptgr_gr_id = d.gr_id) " _
        '                    & "WHERE " _
        '                    & "  a.fcsd_fcs_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fcs_oid").ToString & "' " _
        '                    & "ORDER BY " _
        '                    & "  a.fcsd_seq"


        '                .InitializeCommand()
        '                .FillDataSet(ds_edit, "detail_upd")

        '                gc_edit.DataSource = ds_edit.Tables("detail_upd")
        '                gv_edit.BestFitColumns()
        '            End With
        '        End Using
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '    End Try

        '    edit_data = True
        'End If
    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        sSQLs.Clear()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.fcs_mstr   " _
                                    & "SET  " _
                                    & "  fcs_en_id = " & SetInteger(genfsn_en_id.EditValue) & ",  " _
                                    & "  fcs_remarks = " & SetSetring(genfsn_remarks.EditValue) & ",  " _
                                    & "  fcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  fcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  fcs_qrtr_id = " & SetInteger(genfsn_qrtr_id.EditValue) & ",  " _
                                    & "  fcs_year = " & SetInteger(genfsn_year.EditValue) & "  " _
                                    & "WHERE  " _
                                    & "  fcs_oid = " & SetSetring(_ro_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from fcsd_det where fcsd_fcs_oid = '" + _ro_oid_mstr + "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables("detail_upd").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.fcsd_det " _
                                            & "( " _
                                            & "  fcsd_oid, " _
                                            & "  fcsd_fcs_oid, " _
                                            & "  fcsd_pt_id, " _
                                            & "  fcsd_01_amount, " _
                                            & "  fcsd_02_amount, " _
                                            & "  fcsd_03_amount, " _
                                            & "  fcsd_total_amount, " _
                                            & "  fcsd_buffer_amount, " _
                                            & "  fcsd_seq " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_ro_oid_mstr.ToString) & ",  " _
                                            & SetInteger(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_pt_id")) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_01_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_02_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_03_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_total_amount"))) & ",  " _
                                            & SetDec(SetNumber(ds_edit.Tables("detail_upd").Rows(i).Item("fcsd_buffer_amount"))) & ",  " _
                                            & SetInteger(i) & "  " _
                                            & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_ro_oid_mstr.ToString), "fcs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As NpgsqlException
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT genfsn_ce_final FROM genfsn_mstr " & _
                           "WHERE genfsn_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("genfsn_oid").ToString & "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "genfsn_mstr")
                End With
            End Using

            ' Cek apakah ada hasil dari query
            If ds_bantu.Tables(0).Rows.Count > 0 Then
                ' Ambil nilai field
                Dim status As String = ds_bantu.Tables(0).Rows(0).Item("genfsn_ce_final").ToString().Trim()

                ' Jika status 'Y', batalkan penghapusan
                If status = "Y" Then
                    MessageBox.Show("Can't Delete Used Data...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Return True
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
                        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from genfsn_mstr where genfsn_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("genfsn_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As NpgsqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        'End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged

        Try
            'If e.Column.Name = "fcsd_01_amount" Or e.Column.Name = "fcsd_02_amount" Or e.Column.Name = "fcsd_03_amount" Then
            '    Dim _buffer_persen As Double = 0
            '    Dim _buffer As Double = 0
            '    If gv_edit.GetRowCellValue(e.RowHandle, "gr_code") = "TOP10" Then
            '        _buffer_persen = 0.5
            '    Else
            '        _buffer_persen = 0.2
            '    End If
            '    Dim _total As Double
            '    _total = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_01_amount")) + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_02_amount")) _
            '    + SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "fcsd_03_amount"))

            '    _buffer = _total / 3 * _buffer_persen
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_total_amount", _total)
            '    gv_edit.SetRowCellValue(e.RowHandle, "fcsd_buffer_amount", _buffer)

            'End If
            'gv_edit.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
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
        Dim _rod_en_id As Integer = genfsn_en_id.EditValue

        'If _col = "wc_desc" Then
        '    Dim frm As New FWCSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FToolSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ptnr_name" Then
        '    Dim frm As New FPartnerSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub

    Private Sub genfsn_qrtr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles genfsn_qrtr_id.ButtonClick
        Dim frm As New FPeriodeSearch
        frm.set_win(Me)
        'frm._en_id = genfsn_en_id.EditValue
        frm._obj = genfsn_qrtr_id
        frm._code = genfsn_qrtr_code_id.Tag
        'frm._year = genfsn_year.EditValue
        frm._years = genfsn_year.DateTime.Year.ToString()
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub genfsn_qrtr_code_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles genfsn_qrtr_code_id.ButtonClick
        Dim frm As New FPeriodeTypeSearch
        frm.set_win(Me)
        frm._obj = genfsn_qrtr_code_id
        'frm._code = qrtr_period_type_id.EditValue
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try

            If SetString(genfsn_year.EditValue) = "" Then
                Box("Year can't empty")
                Exit Sub
            End If

            Dim _en_id_all As String

            If genfsn_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(genfsn_en_id.EditValue)
            Else
                _en_id_all = genfsn_en_id.EditValue
            End If

            Dim sSQL As String
            sSQL = "SELECT DISTINCT  " _
                    & "  public.sod_det.sod_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_group, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.pt_mstr.pt_methode_id, " _
                    & "  ptnr_man_methode_mstr.code_name AS pt_methode_name, " _
                    & "  SUM(public.soshipd_det.soshipd_qty *-1) AS soshipd_qty, " _
                    & "  CASE  " _
                    & "    WHEN SUM(public.soshipd_det.soshipd_qty *-1) > 1500 THEN 'Fast Moving' " _
                    & "    WHEN SUM(public.soshipd_det.soshipd_qty *-1) BETWEEN 500 AND 1500 THEN 'Slow Moving' " _
                    & "    WHEN SUM(public.soshipd_det.soshipd_qty *-1) BETWEEN 200 AND 499 THEN 'Moving' " _
                    & "    WHEN SUM(coalesce(public.soshipd_det.soshipd_qty *-1, 0)) < 200 THEN 'Death Stock' " _
                    & "  END AS stock_status " _
                    & "FROM " _
                    & "  public.soship_mstr " _
                    & "  INNER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                    & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.pt_mstr.pt_group = public.code_mstr.code_id) " _
                    & "  LEFT OUTER JOIN code_mstr ptnr_man_methode_mstr ON (ptnr_man_methode_mstr.code_id = public.pt_mstr.pt_methode_id) " _
                    & "WHERE " _
                    & "  public.soship_mstr.soship_date >= " + SetDate(genfsn_start_dt.DateTime.Date) _
                    & "  and public.soship_mstr.soship_date <= " + SetDate(genfsn_end_dt.DateTime.Date) _
                    & "  and public.soship_mstr.soship_en_id in (" & _en_id_all & ") " _
                    & "GROUP BY " _
                    & "  public.sod_det.sod_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pt_mstr.pt_group, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.pt_mstr.pt_methode_id, " _
                    & "  ptnr_man_methode_mstr.code_name " _
                    & "ORDER BY " _
                    & "  soshipd_qty DESC"

            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

            ds_edit.Tables(0).Rows.Clear()

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("genfsnd_pt_id") = dr("sod_pt_id")
                _row("pt_code") = dr("pt_code")
                _row("pt_desc1") = dr("pt_desc1")
                _row("pt_desc2") = dr("pt_desc2")
                _row("genfsnd_ptgroup") = dr("pt_group")
                _row("code_name") = dr("code_name")
                _row("genfsnd_fsn_name") = dr("stock_status")
                _row("genfsnd_methode_id") = dr("pt_methode_id")
                _row("pt_methode_name") = dr("pt_methode_name")
                _row("stock_status") = dr("stock_status")
                _row("genfsnd_qty") = dr("soshipd_qty")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
