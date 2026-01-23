Imports npgsql
Imports master_new.ModFunction

Public Class FGenPOCostMargin
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList


    Private Sub FRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        genpocst_en_id.Properties.DataSource = dt_bantu
        genpocst_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        genpocst_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        genpocst_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", genpocst_en_id.EditValue))
        genpocst_ptnr_id.Properties.DataSource = dt_bantu
        genpocst_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        genpocst_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        genpocst_ptnr_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "cstmg_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Quarter", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Year", "cstmg_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "cstmg_remarks", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "All Child", "cstmg_all_child", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "cstmg_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cstmg_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "cstmg_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cstmg_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "cstmgd_cstmg_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Receipt", "cstmgd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "cstmgd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "cstmgd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "cstmgd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Lot Number", "cstmgd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "cstmgd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "cstmgd_oid", False)
        add_column(gv_edit, "cstmgd_pt_id", False)
        add_column(gv_edit, "cstmgd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Lot/Serial", "pt_ls", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "", False)
        add_column(gv_edit, "cstmgd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty Receipts", "cstmgd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "cstmgd_um", False)
        add_column(gv_edit, "UM", "cstmgd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "cstmgd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "cstmgd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Lot Number", "cstmgd_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "cstmgd_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "cstmgd_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "cstmgd_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "cstmgd_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "cstmgd_sod_oid", False)

    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.cstmg_oid, " _
                & "  a.cstmg_dom_id, " _
                & "  a.cstmg_en_id, " _
                & "  c.en_desc, " _
                & "  a.cstmg_qrtr_id, " _
                & "  b.qrtr_name, " _
                & "  a.cstmg_year, " _
                & "  a.cstmg_remarks, " _
                & "  a.cstmg_all_child, " _
                & "  a.cstmg_add_by, " _
                & "  a.cstmg_add_date, " _
                & "  a.cstmg_upd_by, " _
                & "  a.cstmg_upd_date, " _
                & "  a.cstmg_code " _
                & "FROM " _
                & "  public.cstmg_mstr a " _
                & "  INNER JOIN public.qrtr_mstr b ON (a.cstmg_qrtr_id = b.qrtr_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.cstmg_en_id = c.en_id) " _
                   & "   where cstmg_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by cstmg_code"

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
            & "  a.cstmgd_oid, " _
            & "  a.cstmgd_cstmg_oid, " _
            & "  a.cstmgd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.cstmgd_01_amount, " _
            & "  a.cstmgd_02_amount, " _
            & "  a.cstmgd_03_amount, " _
            & "  a.cstmgd_total_amount, " _
            & "  a.cstmgd_buffer_amount, " _
            & "  a.cstmgd_po, " _
            & "  a.cstmgd_stock, " _
            & "  a.cstmgd_fs_amount, " _
            & "  a.cstmgd_fs_amount_round, " _
            & "  a.cstmgd_fs_amount_round_bal, " _
            & "  a.cstmgd_pr_amount, " _
            & "  a.cstmgd_01_amount_min, " _
            & "  a.cstmgd_02_amount_min, " _
            & "  a.cstmgd_03_amount_min " _
            & "FROM " _
            & "  public.cstmgd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.cstmgd_pt_id = b.pt_id) " _
            & "WHERE " _
            & "  a.cstmgd_cstmg_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cstmg_oid").ToString & "' " _
            & "ORDER BY " _
            & "  cstmgd_seq"


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
        genpocst_en_id.ItemIndex = 0
        'genpocst_year.EditValue = ""
        genpocst_remarks.Text = ""
        genpocst_en_id.Focus()
        genpocst_internal.EditValue = True

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
                        & "  a.cstmgd_oid, " _
                        & "  a.cstmgd_cstmg_oid, " _
                        & "  a.cstmgd_pt_id, " _
                        & "  b.pt_code, " _
                        & "  b.pt_desc1, " _
                        & "  b.pt_desc2, " _
                        & "  a.cstmgd_01_amount, " _
                        & "  a.cstmgd_02_amount, " _
                        & "  a.cstmgd_03_amount, " _
                        & "  a.cstmgd_total_amount, " _
                        & "  a.cstmgd_buffer_amount, " _
                        & "  a.cstmgd_po, " _
                        & "  a.cstmgd_stock, " _
                        & "  a.cstmgd_fs_amount, " _
                        & "  a.cstmgd_fs_amount_round, " _
                        & "  a.cstmgd_fs_amount_round_bal, " _
                        & "  a.cstmgd_pr_amount, " _
                        & "  a.cstmgd_01_amount_min, " _
                        & "  a.cstmgd_02_amount_min, " _
                        & "  a.cstmgd_03_amount_min, " _
                        & "  a.cstmgd_seq " _
                        & "FROM " _
                        & "  public.cstmgd_det a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.cstmgd_pt_id = b.pt_id) " _
                        & "WHERE " _
                        & "  a.cstmgd_cstmg_oid is null"


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
        _code = func_coll.get_transaction_number("FC", genpocst_en_id.GetColumnValue("en_code"), "cstmg_mstr", "cstmg_code")

        'Dim _ro_id As Integer
        '_ro_id = SetInteger(func_coll.GetID("fcs_mstr", fcs_en_id.GetColumnValue("en_code"), "ro_id", "ro_en_id", fcs_en_id.EditValue.ToString))

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
                                & "  public.cstmg_mstr " _
                                & "( " _
                                & "  cstmg_oid, " _
                                & "  cstmg_dom_id, " _
                                & "  cstmg_en_id, " _
                                & "  cstmg_qrtr_id, " _
                                & "  cstmg_remarks, " _
                                & "  cstmg_add_by, " _
                                & "  cstmg_add_date, " _
                                & "  cstmg_code " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_ro_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(genpocst_en_id.EditValue) & ",  " _
                                & SetInteger(genpocst_ptnr_id.EditValue) & ",  " _
                                & SetSetring(genpocst_remarks.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetSetring(_code) & ",  " _
                                & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.cstmgd_det " _
                                        & "( " _
                                        & "  cstmgd_oid, " _
                                        & "  cstmgd_cstmg_oid, " _
                                        & "  cstmgd_pt_id, " _
                                        & "  cstmgd_01_amount, " _
                                        & "  cstmgd_02_amount, " _
                                        & "  cstmgd_03_amount, " _
                                        & "  cstmgd_total_amount, " _
                                        & "  cstmgd_buffer_amount, " _
                                        & "  cstmgd_po, " _
                                        & "  cstmgd_stock, " _
                                        & "  cstmgd_fs_amount, " _
                                        & "  cstmgd_fs_amount_round, " _
                                        & "  cstmgd_fs_amount_round_bal, " _
                                        & "  cstmgd_pr_amount, " _
                                        & "  cstmgd_01_amount_min, " _
                                        & "  cstmgd_02_amount_min, " _
                                        & "  cstmgd_03_amount_min, " _
                                        & "  cstmgd_seq " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_pt_id")) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_01_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_02_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_03_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_total_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_buffer_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_po"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_stock"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_fs_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_fs_amount_round"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_fs_amount_round_bal"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_pr_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_01_amount_min"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_02_amount_min"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("cstmgd_03_amount_min"))) & ",  " _
                                        & SetInteger(i) & "  " _
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
                        set_row(Trim(_ro_oid.ToString), "cstmg_oid")
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

        'If SetString(genpocst_year.EditValue) = "" Then
        '    Box("Year can't empt")
        '    Return False
        '    Exit Function
        'End If
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
        '    cstmg_en_id.Focus()

        '    row = BindingContext(ds.Tables(0)).Position

        '    With ds.Tables(0).Rows(row)
        '        _ro_oid_mstr = .Item("fcs_oid")
        '        cstmg_en_id.EditValue = .Item("fcs_en_id")
        '        cstmg_qrtr_id.EditValue = .Item("fcs_qrtr_id")
        '        cstmg_remarks.EditValue = .Item("fcs_remarks")
        '        cstmg_year.EditValue = .Item("fcs_year")
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
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.fcs_mstr   " _
                                    & "SET  " _
                                    & "  fcs_en_id = " & SetInteger(genpocst_en_id.EditValue) & ",  " _
                                    & "  fcs_remarks = " & SetSetring(genpocst_remarks.EditValue) & ",  " _
                                    & "  fcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  fcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  fcs_qrtr_id = " & SetInteger(genpocst_ptnr_id.EditValue) & "  " _
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
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
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
                    Catch ex As nPgSqlException
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
                            .Command.CommandText = "delete from cstmg_mstr where cstmg_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cstmg_oid").ToString + "'"
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

    'Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        browse_data()
    '    End If
    'End Sub

    'Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
    '    browse_data()
    'End Sub

    'Private Sub browse_data()
    '    Dim _col As String = gv_edit.FocusedColumn.Name
    '    Dim _row As Integer = gv_edit.FocusedRowHandle
    '    Dim _rod_en_id As Integer = genpocst_en_id.EditValue

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

    'End Sub

    'Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
    '    'Dim _now As DateTime
    '    '_now = func_coll.get_now
    '    'With gv_edit
    '    '    .SetRowCellValue(e.RowHandle, "rod_op", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_start_date", _now)
    '    '    .SetRowCellValue(e.RowHandle, "rod_end_date", _now)
    '    '    .SetRowCellValue(e.RowHandle, "rod_mch_op", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_tran_qty", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_queue", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_wait", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_move", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_run", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_setup", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_yield_pct", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_milestone", "N")
    '    '    .SetRowCellValue(e.RowHandle, "rod_sub_lead", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_setup_men", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_men_mch", 0)
    '    '    .SetRowCellValue(e.RowHandle, "rod_sub_cost", 0)
    '    '    .BestFitColumns()
    '    'End With
    'End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            'If SetString(genpocst_year.EditValue) = "" Then
            '    Box("Year can't empty")
            '    Exit Sub
            'End If

            Dim _en_id_all As String

            Dim sSQL As String
            sSQL = "SELECT  " _
                & "  a.fcsd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  sum(a.fcsd_01_amount) as fcsd_01_amount, " _
                & "  sum(a.fcsd_02_amount) as fcsd_02_amount, " _
                & "  sum(a.fcsd_03_amount) as fcsd_03_amount, " _
                & "  sum(a.fcsd_total_amount) as fcsd_total_amount, " _
                & "  sum(a.fcsd_buffer_amount) as fcsd_buffer_amount,coalesce((SELECT  sum(x.pod_qty - coalesce(x.pod_qty_receive, 0)) AS jml " _
                & "FROM  public.pod_det x  INNER JOIN public.po_mstr y ON (x.pod_po_oid = y.po_oid) " _
                & "WHERE  x.pod_pt_id = a.fcsd_pt_id AND   y.po_en_id in (" & _en_id_all & ") and coalesce(po_trans_id,'') <> 'X'),0) as qty_po, " _
                & " coalesce((SELECT   sum(x.invc_qty) as jml " _
                & "FROM  public.invc_mstr x " _
                & "WHERE  x.invc_pt_id = a.fcsd_pt_id AND   x.invc_en_id in (" & _en_id_all & ") and x.invc_loc_id in (SELECT   z.locgr_loc_id FROM  public.locgr_mstr z)),0) as  qty_stock " _
                & "FROM " _
                & "  public.fcsd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.fcsd_pt_id = b.pt_id) " _
                & "  INNER JOIN public.fcs_mstr c ON (a.fcsd_fcs_oid = c.fcs_oid) " _
                & "WHERE " _
                & "  c.fcs_en_id IN (" & _en_id_all & ") " _
                & "  and c.fcs_qrtr_id=" & genpocst_ptnr_id.EditValue & " " _
                & "  group by  " _
                & "  a.fcsd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2"

            Dim dt_fs As New DataTable
            dt_fs = master_new.PGSqlConn.GetTableData(sSQL)

            ds_edit.Tables(0).Rows.Clear()

            For Each dr As DataRow In dt_fs.Rows
                Dim _row As DataRow
                _row = ds_edit.Tables(0).NewRow

                _row("cstmgd_pt_id") = dr("fcsd_pt_id")
                _row("pt_code") = dr("pt_code")
                _row("pt_desc1") = dr("pt_desc1")
                _row("pt_desc2") = dr("pt_desc2")
                _row("cstmgd_01_amount") = dr("fcsd_01_amount")
                _row("cstmgd_02_amount") = dr("fcsd_02_amount")
                _row("cstmgd_03_amount") = dr("fcsd_03_amount")
                _row("cstmgd_total_amount") = dr("fcsd_total_amount")
                _row("cstmgd_buffer_amount") = System.Math.Round(dr("fcsd_buffer_amount"), 0)

                _row("cstmgd_fs_amount") = SetNumber(dr("fcsd_total_amount")) + SetNumber(_row("cstmgd_buffer_amount"))
                _row("cstmgd_fs_amount_round") = pembulatan(_row("cstmgd_fs_amount"))
                _row("cstmgd_fs_amount_round_bal") = _row("cstmgd_fs_amount_round") - _row("cstmgd_fs_amount")

                _row("cstmgd_po") = dr("qty_po")
                _row("cstmgd_stock") = dr("qty_stock")
                _row("cstmgd_pr_amount") = IIf(_row("cstmgd_fs_amount_round") - _row("cstmgd_po") - _row("cstmgd_stock") < 0, 0, _row("cstmgd_fs_amount_round") - _row("cstmgd_po") - _row("cstmgd_stock"))

                _row("cstmgd_01_amount_min") = _row("cstmgd_01_amount") + _row("cstmgd_buffer_amount")
                _row("cstmgd_02_amount_min") = _row("cstmgd_02_amount")
                _row("cstmgd_03_amount_min") = _row("cstmgd_03_amount")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub genpocst_internal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles genpocst_internal.EditValueChanged
        'Dim _po_exc_rate As Double
        'If po_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
        '    _po_exc_rate = func_data.get_exchange_rate(po_cu_id.EditValue, po_date.DateTime)
        '    If _po_exc_rate = 1 Then
        '        po_exc_rate.EditValue = 0
        '    Else
        '        po_exc_rate.EditValue = _po_exc_rate
        '    End If
        'Else
        '    po_exc_rate.EditValue = 1
        'End If

        If genpocst_internal.Checked = True Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_data("ptnr_mstr_vend_internal", genpocst_en_id.EditValue))
            genpocst_ptnr_id.Properties.DataSource = dt_bantu
            genpocst_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
            genpocst_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
            genpocst_ptnr_id.ItemIndex = 0
        Else

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_data("ptnr_mstr_vend", genpocst_en_id.EditValue))
            genpocst_ptnr_id.Properties.DataSource = dt_bantu
            genpocst_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
            genpocst_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
            genpocst_ptnr_id.ItemIndex = 0
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
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "pod_en_id")
        Dim _pod_si_id As Integer = gv_edit.GetRowCellValue(_row, "pod_si_id")
        'Dim _pod_pt_id As Integer = gv_edit.GetRowCellValue(_row, "pod_pt_id")

        If _col = "req_code_relation" Then
            Dim frm As New FRequisitionSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._si_id = _pod_si_id
            'frm._ptnr_id = po_ptnr_id.EditValue
            'frm._is_internal = po_is_internal.EditValue
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "sb_desc" Then
            Dim frm As New FSubAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "cc_desc" Then
            Dim frm As New FCostCenterSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pjc_desc" Then
            Dim frm As New FProjectAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._pt_id = gv_edit.GetRowCellValue(_row, "pod_pt_id")
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pod_tax_class_name" Then
            If gv_edit.GetRowCellValue(_row, "pod_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "cstmgd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "cstmgd_qty", 1)
            .SetRowCellValue(e.RowHandle, "cstmgd_si_id", 1)
            '.SetRowCellValue(e.RowHandle, "cstmgd_cost", 0)
            '.SetRowCellValue(e.RowHandle, "cstmgd_sb_id", 0)
            '.SetRowCellValue(e.RowHandle, "sb_desc", "-")
            '.SetRowCellValue(e.RowHandle, "cstmgd_cc_id", 0)
            '.SetRowCellValue(e.RowHandle, "cc_desc", "-")
            .BestFitColumns()
        End With
    End Sub
End Class
