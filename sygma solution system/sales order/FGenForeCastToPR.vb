Imports npgsql
Imports master_new.ModFunction

Public Class FGenForeCastToPR
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
        genpr_en_id.Properties.DataSource = dt_bantu
        genpr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        genpr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        genpr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_quarter_mstr())
        genpr_qrtr_id.Properties.DataSource = dt_bantu
        genpr_qrtr_id.Properties.DisplayMember = dt_bantu.Columns("qrtr_name").ToString
        genpr_qrtr_id.Properties.ValueMember = dt_bantu.Columns("qrtr_id").ToString
        genpr_qrtr_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "genpr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Quarter", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Year", "genpr_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "genpr_remarks", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "All Child", "genpr_all_child", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "genpr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "genpr_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "genpr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "genpr_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "genprd_pt_id", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Month 1 Amount", "genprd_01_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Month 2 Amount", "genprd_02_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Month 3 Amount", "genprd_03_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Total Amount", "genprd_total_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Buffer Amount", "genprd_buffer_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "FS Amount", "genprd_fs_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "FS Round Amount", "genprd_fs_amount_round", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "FS Bal Amount", "genprd_fs_amount_round_bal", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "PO", "genprd_po", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Stock", "genprd_stock", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "PR", "genprd_pr_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Month 1 Min Amount", "genprd_01_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Month 2 Min Amount", "genprd_02_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Month 3 Min Amount", "genprd_03_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "genprd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Description 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Month 1 Amount", "genprd_01_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Month 2 Amount", "genprd_02_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Month 3 Amount", "genprd_03_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Total Amount", "genprd_total_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Buffer Amount", "genprd_buffer_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "FS Amount", "genprd_fs_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "FS Round Amount", "genprd_fs_amount_round", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "FS Bal Amount", "genprd_fs_amount_round_bal", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "PO", "genprd_po", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Stock", "genprd_stock", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "PR", "genprd_pr_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Month 1 Min Amount", "genprd_01_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Month 2 Min Amount", "genprd_02_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Month 3 Min Amount", "genprd_03_amount_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  a.genpr_oid, " _
                & "  a.genpr_dom_id, " _
                & "  a.genpr_en_id, " _
                & "  c.en_desc, " _
                & "  a.genpr_qrtr_id, " _
                & "  b.qrtr_name, " _
                & "  a.genpr_year, " _
                & "  a.genpr_remarks, " _
                & "  a.genpr_all_child, " _
                & "  a.genpr_add_by, " _
                & "  a.genpr_add_date, " _
                & "  a.genpr_upd_by, " _
                & "  a.genpr_upd_date, " _
                & "  a.genpr_code " _
                & "FROM " _
                & "  public.genpr_mstr a " _
                & "  INNER JOIN public.qrtr_mstr b ON (a.genpr_qrtr_id = b.qrtr_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.genpr_en_id = c.en_id) " _
                   & "   where genpr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by genpr_code"

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
            & "  a.genprd_oid, " _
            & "  a.genprd_genpr_oid, " _
            & "  a.genprd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  a.genprd_01_amount, " _
            & "  a.genprd_02_amount, " _
            & "  a.genprd_03_amount, " _
            & "  a.genprd_total_amount, " _
            & "  a.genprd_buffer_amount, " _
            & "  a.genprd_po, " _
            & "  a.genprd_stock, " _
            & "  a.genprd_fs_amount, " _
            & "  a.genprd_fs_amount_round, " _
            & "  a.genprd_fs_amount_round_bal, " _
            & "  a.genprd_pr_amount, " _
            & "  a.genprd_01_amount_min, " _
            & "  a.genprd_02_amount_min, " _
            & "  a.genprd_03_amount_min " _
            & "FROM " _
            & "  public.genprd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.genprd_pt_id = b.pt_id) " _
            & "WHERE " _
            & "  a.genprd_genpr_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("genpr_oid").ToString & "' " _
            & "ORDER BY " _
            & "  genprd_seq"


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
        genpr_en_id.ItemIndex = 0
        genpr_qrtr_id.ItemIndex = 0
        genpr_year.EditValue = ""
        genpr_remarks.Text = ""
        genpr_en_id.Focus()

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
                        & "  a.genprd_oid, " _
                        & "  a.genprd_genpr_oid, " _
                        & "  a.genprd_pt_id, " _
                        & "  b.pt_code, " _
                        & "  b.pt_desc1, " _
                        & "  b.pt_desc2, " _
                        & "  a.genprd_01_amount, " _
                        & "  a.genprd_02_amount, " _
                        & "  a.genprd_03_amount, " _
                        & "  a.genprd_total_amount, " _
                        & "  a.genprd_buffer_amount, " _
                        & "  a.genprd_po, " _
                        & "  a.genprd_stock, " _
                        & "  a.genprd_fs_amount, " _
                        & "  a.genprd_fs_amount_round, " _
                        & "  a.genprd_fs_amount_round_bal, " _
                        & "  a.genprd_pr_amount, " _
                        & "  a.genprd_01_amount_min, " _
                        & "  a.genprd_02_amount_min, " _
                        & "  a.genprd_03_amount_min, " _
                        & "  a.genprd_seq " _
                        & "FROM " _
                        & "  public.genprd_det a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.genprd_pt_id = b.pt_id) " _
                        & "WHERE " _
                        & "  a.genprd_genpr_oid is null"


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
        _code = func_coll.get_transaction_number("FC", genpr_en_id.GetColumnValue("en_code"), "genpr_mstr", "genpr_code")

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
                                & "  public.genpr_mstr " _
                                & "( " _
                                & "  genpr_oid, " _
                                & "  genpr_dom_id, " _
                                & "  genpr_en_id, " _
                                & "  genpr_qrtr_id, " _
                                & "  genpr_year, " _
                                & "  genpr_remarks, " _
                                & "  genpr_add_by, " _
                                & "  genpr_add_date, " _
                                & "  genpr_code, " _
                                & "  genpr_all_child " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_ro_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(genpr_en_id.EditValue) & ",  " _
                                & SetInteger(genpr_qrtr_id.EditValue) & ",  " _
                                & SetInteger(genpr_year.EditValue) & ",  " _
                                & SetSetring(genpr_remarks.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetSetring(_code) & ",  " _
                                & SetBitYN(genpr_all_child.EditValue) & "  " _
                                & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.genprd_det " _
                                        & "( " _
                                        & "  genprd_oid, " _
                                        & "  genprd_genpr_oid, " _
                                        & "  genprd_pt_id, " _
                                        & "  genprd_01_amount, " _
                                        & "  genprd_02_amount, " _
                                        & "  genprd_03_amount, " _
                                        & "  genprd_total_amount, " _
                                        & "  genprd_buffer_amount, " _
                                        & "  genprd_po, " _
                                        & "  genprd_stock, " _
                                        & "  genprd_fs_amount, " _
                                        & "  genprd_fs_amount_round, " _
                                        & "  genprd_fs_amount_round_bal, " _
                                        & "  genprd_pr_amount, " _
                                        & "  genprd_01_amount_min, " _
                                        & "  genprd_02_amount_min, " _
                                        & "  genprd_03_amount_min, " _
                                        & "  genprd_seq " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_ro_oid.ToString) & ",  " _
                                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_pt_id")) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_01_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_02_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_03_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_total_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_buffer_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_po"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_stock"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_fs_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_fs_amount_round"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_fs_amount_round_bal"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_pr_amount"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_01_amount_min"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_02_amount_min"))) & ",  " _
                                        & SetDec(SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("genprd_03_amount_min"))) & ",  " _
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
                        set_row(Trim(_ro_oid.ToString), "genpr_oid")
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

        If SetString(genpr_year.EditValue) = "" Then
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
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                    & "  public.fcs_mstr   " _
                                    & "SET  " _
                                    & "  fcs_en_id = " & SetInteger(genpr_en_id.EditValue) & ",  " _
                                    & "  fcs_remarks = " & SetSetring(genpr_remarks.EditValue) & ",  " _
                                    & "  fcs_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  fcs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                    & "  fcs_qrtr_id = " & SetInteger(genpr_qrtr_id.EditValue) & ",  " _
                                    & "  fcs_year = " & SetInteger(genpr_year.EditValue) & "  " _
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
                            .Command.CommandText = "delete from genpr_mstr where genpr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("genpr_oid").ToString + "'"
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
        Dim _rod_en_id As Integer = genpr_en_id.EditValue

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

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'Dim _now As DateTime
        '_now = func_coll.get_now
        'With gv_edit
        '    .SetRowCellValue(e.RowHandle, "rod_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_start_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_end_date", _now)
        '    .SetRowCellValue(e.RowHandle, "rod_mch_op", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_tran_qty", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_queue", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_wait", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_move", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_run", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_yield_pct", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_milestone", "N")
        '    .SetRowCellValue(e.RowHandle, "rod_sub_lead", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_setup_men", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_men_mch", 0)
        '    .SetRowCellValue(e.RowHandle, "rod_sub_cost", 0)
        '    .BestFitColumns()
        'End With
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub

    Private Sub BtGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGen.Click
        Try

            If SetString(genpr_year.EditValue) = "" Then
                Box("Year can't empty")
                Exit Sub
            End If

            Dim _en_id_all As String

            If genpr_all_child.EditValue = True Then
                _en_id_all = get_en_id_child(genpr_en_id.EditValue)
            Else
                _en_id_all = genpr_en_id.EditValue
            End If

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
                & "  and c.fcs_qrtr_id=" & genpr_qrtr_id.EditValue & " " _
                & "  and c.fcs_year=" & genpr_year.EditValue & " " _
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

                _row("genprd_pt_id") = dr("fcsd_pt_id")
                _row("pt_code") = dr("pt_code")
                _row("pt_desc1") = dr("pt_desc1")
                _row("pt_desc2") = dr("pt_desc2")
                _row("genprd_01_amount") = dr("fcsd_01_amount")
                _row("genprd_02_amount") = dr("fcsd_02_amount")
                _row("genprd_03_amount") = dr("fcsd_03_amount")
                _row("genprd_total_amount") = dr("fcsd_total_amount")
                _row("genprd_buffer_amount") = System.Math.Round(dr("fcsd_buffer_amount"), 0)

                _row("genprd_fs_amount") = SetNumber(dr("fcsd_total_amount")) + SetNumber(_row("genprd_buffer_amount"))
                _row("genprd_fs_amount_round") = pembulatan(_row("genprd_fs_amount"))
                _row("genprd_fs_amount_round_bal") = _row("genprd_fs_amount_round") - _row("genprd_fs_amount")

                _row("genprd_po") = dr("qty_po")
                _row("genprd_stock") = dr("qty_stock")
                _row("genprd_pr_amount") = IIf(_row("genprd_fs_amount_round") - _row("genprd_po") - _row("genprd_stock") < 0, 0, _row("genprd_fs_amount_round") - _row("genprd_po") - _row("genprd_stock"))

                _row("genprd_01_amount_min") = _row("genprd_01_amount") + _row("genprd_buffer_amount")
                _row("genprd_02_amount_min") = _row("genprd_02_amount")
                _row("genprd_03_amount_min") = _row("genprd_03_amount")

                ds_edit.Tables(0).Rows.Add(_row)
            Next
            ds_edit.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
