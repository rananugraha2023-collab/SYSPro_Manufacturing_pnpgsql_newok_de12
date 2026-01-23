Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FAssetBack
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _asback_oid_mstr As String
    Dim ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim ds_before As New DataSet
    Dim mf As New master_new.ModFunction
    Dim _conf_value As String

#Region "Seting Awal"
    Private Sub FAssetBack_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_asset_return")

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If _conf_value = "0" Then
            xtc_detail.TabPages(1).PageVisible = False
            xtc_detail.TabPages(3).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(1).PageVisible = True
            xtc_detail.TabPages(3).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        asback_en_id.Properties.DataSource = dt_bantu
        asback_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        asback_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        asback_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_transaction())
        'asback_tran_id.Properties.DataSource = dt_bantu
        'asback_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'asback_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        'asback_tran_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_tran_mstr())
        'asback_tran_id.Properties.DataSource = dt_bantu
        'asback_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'asback_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        'asback_tran_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            asback_tran_id.Properties.DataSource = dt_bantu
            asback_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            asback_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            asback_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            asback_tran_id.Properties.DataSource = dt_bantu
            asback_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            asback_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            asback_tran_id.ItemIndex = 0
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Code", "asback_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Return Date", "asback_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "asback_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "asback_trans_id", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "asback_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "asback_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "asback_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "asback_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "asbackd_oid", False)
        add_column(gv_edit, "asbackd_asback_oid", False)
        add_column(gv_edit, "asbackd_ass_id", False)
        add_column(gv_edit, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asbackd_loc_id", False)
        add_column(gv_edit, "Back To Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asbackd_reason", False)
        add_column(gv_edit, "Back Reson", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asbackd_its_id", False)
        add_column(gv_edit, "Item Status", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "asbackd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "asbackd_oid", False)
        add_column(gv_detail, "asbackd_asback_oid", False)
        add_column_copy(gv_detail, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Back To Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Back Reason", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Item Status", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "asbackd_rmks", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "asbackd_oid", False)
        add_column(gv_email, "asbackd_asback_oid", False)
        'add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Return Code", "asback_code", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Return Date", "asback_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Return Reason", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Remarks", "asbackd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Return Code", "asback_code", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  asback_oid, " _
                    & "  asback_dom_id, " _
                    & "  asback_en_id,en_desc, " _
                    & "  asback_add_by, " _
                    & "  asback_add_date, " _
                    & "  asback_upd_by, " _
                    & "  asback_upd_date, " _
                    & "  asback_code, " _
                    & "  asback_date, " _
                    & "  asback_rmks, " _
                    & "  asback_tran_id,tran_name, " _
                    & "  asback_trans_id, " _
                    & "  asback_trans_rmks, " _
                    & "  asback_dt " _
                    & "FROM  " _
                    & "  public.asback_mstr " _
                    & "  inner join en_mstr on en_id = asback_en_id " _
                    & "  inner join tran_mstr on tran_id = asback_tran_id " _
                    & " where asback_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and asback_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and asback_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
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
            & "  asbackd_oid, " _
            & "  asbackd_asback_oid, " _
            & "  asbackd_seq, " _
            & "  asbackd_ass_id,ass_code,ass_desc,pt_code, " _
            & "  asbackd_reason,rs.code_name as reason, " _
            & "  asbackd_its_id,its_desc, " _
            & "  asbackd_loc_id,loc_desc, " _
            & "  asbackd_rmks, " _
            & "  asbackd_dt " _
            & "FROM  " _
            & "  public.asbackd_det " _
            & "  inner join asback_mstr on asback_oid = asbackd_asback_oid " _
            & "  inner join ass_mstr on ass_id = asbackd_ass_id " _
            & "  inner join pt_mstr on pt_id = ass_pt_id " _
            & "  inner join code_mstr rs on rs.code_id = asbackd_reason " _
            & "  inner join its_mstr on its_id = asbackd_its_id " _
            & "  LEFT OUTER JOIN public.loc_mstr ON (public.loc_mstr.loc_id = asbackd_loc_id) " _
            & " where asback_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & " and asback_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
        load_data_detail(sql, gc_detail, "detail")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join asback_mstr on asback_code = wf_ref_code " + _
                  " inner join asbackd_det dt on dt.asbackd_asback_oid = asback_oid " _
                & " where asback_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and asback_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and asback_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  asbackd_oid, " _
                & "  asbackd_asback_oid,asback_code,asback_date, " _
                & "  asbackd_seq, " _
                & "  asbackd_ass_id,ass_code,ass_desc,pt_code, " _
                & "  asbackd_reason,rs.code_name as reason, " _
                & "  asbackd_its_id,its_desc, " _
                & "  asbackd_rmks, " _
                & "  asbackd_dt " _
                & "FROM  " _
                & "  public.asbackd_det " _
                & "  inner join asback_mstr on asback_oid = asbackd_asback_oid " _
                & "  inner join ass_mstr on ass_id = asbackd_ass_id " _
                & "  inner join pt_mstr on pt_id = ass_pt_id " _
                & "  inner join code_mstr rs on rs.code_id = asbackd_reason " _
                & "  inner join its_mstr on its_id = asbackd_its_id " _
                & " where asback_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and asback_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()
        End If
        
        Try
            ds.Tables("smart").Clear()
        Catch ex As Exception
        End Try

        sql = "select asback_oid, asback_code, asback_trans_id, false as status from asback_mstr " _
            & " where asback_trans_id ~~* 'd' and asback_add_by ~~* '" + master_new.ClsVar.sNama + "'"
        load_data_detail(sql, gc_smart, "smart")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("asbackd_asback_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asbackd_asback_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("asbackd_asback_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid"))
            'gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("asbackd_asback_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asbackd_asback_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("asbackd_asback_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid"))
                'gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"
    'Public Overrides Sub load_cb_en()
    '    dt_bantu = New DataTable
    '    dt_bantu = (func_data.load_data("ptnr_mstr_vend", asrtr_en_id.EditValue))
    '    po_ptnr_id.Properties.DataSource = dt_bantu
    '    po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
    '    po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
    '    po_ptnr_id.ItemIndex = 0
    'End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        asback_en_id.Focus()
        asback_en_id.ItemIndex = 0
        asback_date.DateTime = _now
        asback_rmks.Text = ""
        asback_tran_id.ItemIndex = 0

        Try
            tcg_header.SelectedTabPageIndex = 0
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
                            & "  asbackd_oid, " _
                            & "  asbackd_asback_oid, " _
                            & "  asbackd_seq, " _
                            & "  asbackd_ass_id,ass_code,ass_desc,pt_code, " _
                            & "  asbackd_reason,rs.code_name as reason, " _
                            & "  asbackd_its_id,its_desc, " _
                            & "  asbackd_rmks, " _
                            & "  asbackd_loc_id,loc_desc, " _
                            & "  asbackd_dt " _
                            & "FROM  " _
                            & "  public.asbackd_det " _
                            & "  inner join asback_mstr on asback_oid = asbackd_asback_oid " _
                            & "  inner join ass_mstr on ass_id = asbackd_ass_id " _
                            & "  inner join pt_mstr on pt_id = ass_pt_id " _
                            & "  inner join code_mstr rs on rs.code_id = asbackd_reason " _
                            & "  inner join its_mstr on its_id = asbackd_its_id " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.loc_mstr.loc_id = asbackd_loc_id) " _
                            & " where asbackd_seq = -99"
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim ds_bantu As New DataSet
        Dim _asback_oid As Guid
        _asback_oid = Guid.NewGuid

        Dim _asback_code As String
        Dim i As Integer
        Dim ssqls As New ArrayList

        _asback_code = func_coll.get_transaction_number("BK", asback_en_id.GetColumnValue("en_code"), "asback_mstr", "asback_code")

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(asback_tran_id.EditValue)
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.asback_mstr " _
                                            & "( " _
                                            & "  asback_oid, " _
                                            & "  asback_dom_id, " _
                                            & "  asback_en_id, " _
                                            & "  asback_add_by, " _
                                            & "  asback_add_date, " _
                                            & "  asback_code, " _
                                            & "  asback_date, " _
                                            & "  asback_rmks, " _
                                            & "  asback_tran_id, " _
                                            & "  asback_trans_id, " _
                                            & "  asback_trans_rmks, " _
                                            & "  asback_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_asback_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(asback_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_asback_code) & ",  " _
                                            & SetDate(asback_date.DateTime) & ",  " _
                                            & SetSetring(asback_rmks.Text) & ",  " _
                                            & SetInteger(asback_tran_id.EditValue) & ",  " _
                                            & "'D'" & ",  " _
                                            & "'-'" & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            'Ant 22 feb 2011
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.asbackd_det " _
                                                & "( " _
                                                & "  asbackd_oid, " _
                                                & "  asbackd_asback_oid, " _
                                                & "  asbackd_seq, " _
                                                & "  asbackd_ass_id, " _
                                                & "  asbackd_reason, " _
                                                & "  asbackd_its_id, " _
                                                & "  asbackd_loc_id, " _
                                                & "  asbackd_rmks, " _
                                                & "  asbackd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_asback_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_ass_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_reason")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_its_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_loc_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("asbackd_rmks")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            '--------------------------
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If _conf_value = "0" Then
                                .Command.CommandType = CommandType.Text
                                'Ant 22 feb 2011
                                .Command.CommandText = "UPDATE ass_mstr  " _
                                                    & "  set ass_qty_assgn = 0, " _
                                                    & "  ass_emp_id = 0, " _
                                                    & "  ass_emp_dept = 0, " _
                                                    & "  ass_emp_rg = 0 ," _
                                                    & "  ass_its_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_its_id")) & ",  " _
                                                    & "  ass_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_loc_id")) & "  " _
                                                    & "  where ass_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_ass_id"))
                                '-------------------------------
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(asback_en_id.EditValue) & ",  " _
                                                        & SetInteger(asback_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_asback_oid.ToString) & ",  " _
                                                        & SetSetring(_asback_code) & ",  " _
                                                        & SetSetring("Asset Return") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                ssqls.Add(.Command.CommandText)
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
                        set_row(_asback_oid.ToString, "asback_oid")
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function delete_data() As Boolean
        'ant 22 feb 2011
        Dim ssqls As New ArrayList

        If _conf_value = "0" Then
            MessageBox.Show("Can't Delete, Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")) > 0 Then
                    MessageBox.Show("Can't Delete, Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            Else
                If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    Exit Function
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
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                '******************************************************
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from asbackd_det where asbackd_asback_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                '******************************************************
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from asback_mstr where asback_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                '******************************************************

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
        End If
        '---------------------------------------------------------
    End Function

    Public Overrides Function edit_data() As Boolean
        If _conf_value = "0" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If

        If ds.Tables.Count = 0 Then
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            Exit Function
        End If

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _asback_oid_mstr = .Item("asback_oid").ToString
                asback_en_id.EditValue = .Item("asback_en_id")
                asback_date.DateTime = .Item("asback_date")
                asback_rmks.Text = SetString(.Item("asback_rmks"))
                asback_tran_id.EditValue = .Item("asback_tran_id")
            End With
            asback_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        'ant 22 feb 2011
                        .SQL = "SELECT  " _
                            & "  asbackd_oid, " _
                            & "  asbackd_asback_oid, " _
                            & "  asbackd_seq, " _
                            & "  asbackd_ass_id,ass_code,ass_desc,pt_code, " _
                            & "  asbackd_reason,rs.code_name as reason, " _
                            & "  asbackd_its_id,its_desc, " _
                            & "  asbackd_loc_id,loc_desc, " _
                            & "  asbackd_rmks, " _
                            & "  asbackd_dt " _
                            & "FROM  " _
                            & "  public.asbackd_det " _
                            & "  inner join asback_mstr on asback_oid = asbackd_asback_oid " _
                            & "  inner join ass_mstr on ass_id = asbackd_ass_id " _
                            & "  inner join pt_mstr on pt_id = ass_pt_id " _
                            & "  inner join code_mstr rs on rs.code_id = asbackd_reason " _
                            & "  inner join its_mstr on its_id = asbackd_its_id " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.loc_mstr.loc_id = asbackd_loc_id) " _
                            & " where asbackd_asback_oid = '" + ds.Tables(0).Rows(row).Item("asback_oid").ToString + "'"
                        '------------------------------
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
                        '.FillDataSet(ds_update_related, "update_related")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_before = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        'ant 22 feb 2011
                        .SQL = "SELECT  " _
                            & "  asbackd_oid, " _
                            & "  asbackd_asback_oid, " _
                            & "  asbackd_seq, " _
                            & "  asbackd_ass_id,ass_code,ass_desc,pt_code, " _
                            & "  asbackd_reason,rs.code_name as reason, " _
                            & "  asbackd_its_id,its_desc, " _
                            & "  asbackd_loc_id,loc_desc, " _
                            & "  asbackd_rmks, " _
                            & "  asbackd_dt " _
                            & "FROM  " _
                            & "  public.asbackd_det " _
                            & "  inner join asback_mstr on asback_oid = asbackd_asback_oid " _
                            & "  inner join ass_mstr on ass_id = asbackd_ass_id " _
                            & "  inner join pt_mstr on pt_id = ass_pt_id " _
                            & "  inner join code_mstr rs on rs.code_id = asbackd_reason " _
                            & "  inner join its_mstr on its_id = asbackd_its_id " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.loc_mstr.loc_id = asbackd_loc_id) " _
                            & " where asbackd_asback_oid = '" + ds.Tables(0).Rows(row).Item("asback_oid").ToString + "'"
                        '-----------------------------------------
                        .InitializeCommand()
                        .FillDataSet(ds_before, "before_edit")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _pod_qty_receive As Double = 0
        Dim i As Integer
        Dim ds_bantu As New DataSet
        Dim ssqls As New ArrayList

        ds_bantu = func_data.load_aprv_mstr(asback_tran_id.EditValue)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'For Each _dr As DataRow In ds_before.Tables(0).Rows
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "UPDATE ass_mstr  " _
                        '                        & "  set ass_qty_assgn = 1, " _
                        '                        & "  ass_confirm = 'Y', " _
                        '                        & "  ass_its_id = 1 " _
                        '                        & "  where ass_id = " + SetInteger(_dr("asrtrd_ass_id"))
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from asbackd_det where asbackd_asback_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.asback_mstr   " _
                                            & "SET  " _
                                            & "  asback_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  asback_en_id = " & asback_en_id.EditValue & ",  " _
                                            & "  asback_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  asback_upd_date = current_timestamp " & ",  " _
                                            & "  asback_code = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")) & ",  " _
                                            & "  asback_date = " & SetDate(asback_date.DateTime) & ",  " _
                                            & "  asback_rmks = " & SetSetring(asback_rmks.Text) & ",  " _
                                            & "  asback_tran_id = " & SetInteger(asback_tran_id.EditValue) & ",  " _
                                            & "  asback_trans_id = 'D' " & ",  " _
                                            & "  asback_trans_rmks = '-' " & ",  " _
                                            & "  asback_dt = current_timestamp " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  asback_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid")) & " " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            'ant 22 feb 2011
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.asbackd_det " _
                                                & "( " _
                                                & "  asbackd_oid, " _
                                                & "  asbackd_asback_oid, " _
                                                & "  asbackd_seq, " _
                                                & "  asbackd_ass_id, " _
                                                & "  asbackd_reason, " _
                                                & "  asbackd_its_id, " _
                                                & "  asbackd_loc_id, " _
                                                & "  asbackd_rmks, " _
                                                & "  asbackd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("asbackd_asback_oid")) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_ass_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_reason")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_its_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_loc_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("asbackd_rmks")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            '--------------------------------
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "UPDATE ass_mstr  " _
                            '                    & "  set ass_qty_assgn = 0, " _
                            '                    & "  ass_its_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("asbackd_its_id")) & "  " _
                            '                    & "  where ass_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_ass_id"))
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        'Ant 22 feb 2011
                        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_tran_id") <> asback_tran_id.EditValue Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _asback_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(asback_en_id.EditValue) & ",  " _
                                                        & SetSetring(asback_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_asback_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")) & ",  " _
                                                        & SetSetring("Asset Return") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If
                        '---------------------------------

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
                        set_row(_asback_oid_mstr, "asback_oid")
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

    'Public Overrides Function before_delete() As Boolean
    '    before_delete = True

    '    Return before_delete
    'End Function

    'Public Overrides Function delete_data() As Boolean
    '    row = BindingContext(ds.Tables(0)).Position
    '    Dim func_coll As New function_collection
    '    If func_coll.get_status_wf(ds.Tables(0).Rows(row).Item("asback_code")) > 0 Then
    '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Function
    '    End If


    '    Dim _asback_oid As String
    '    _asback_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid")

    '    delete_data = True
    '    If ds.Tables.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    ElseIf ds.Tables(0).Rows.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    End If

    '    If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
    '        Exit Function
    '    End If

    '    Dim i As Integer

    '    If before_delete() = True Then
    '        row = BindingContext(ds.Tables(0)).Position

    '        If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
    '            row = row - 1
    '        ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
    '            row = 0
    '        End If

    '        Try
    '            Using objinsert As New master_new.WDABasepgsql("", "")
    '                With objinsert
    '                    .Connection.Open()
    '                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                    Try
    '                        .Command = .Connection.CreateCommand
    '                        .Command.Transaction = sqlTran

    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "delete from asback_mstr where asback_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid") + "'"
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        'For i = 0 To ds.Tables("detail").Rows.Count - 1
    '                        '    If ds.Tables("detail").Rows(i).Item("asbackd_asback_oid") = _asrtr_oid Then
    '                        '        .Command.CommandType = CommandType.Text
    '                        '        .Command.CommandText = "UPDATE ass_mstr  " _
    '                        '                            & "  set ass_qty_assgn = 0, " _
    '                        '                            & "  ass_its_id = 1 " _
    '                        '                            & "  where ass_id = " + SetInteger(ds.Tables("detail").Rows(i).Item("asrtrd_ass_id"))
    '                        '        .Command.ExecuteNonQuery()
    '                        '        .Command.Parameters.Clear()
    '                        '    End If
    '                        'Next

    '                        sqlTran.Commit()

    '                        help_load_data(True)
    '                        MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Catch ex As nPgSqlException
    '                        sqlTran.Rollback()
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If

    '    Return delete_data
    'End Function
#End Region

#Region "gv_edit"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        'Dim _pod_qty, _pod_qty_real, _pod_um_conv, _pod_qty_cost, _pod_cost, _pod_disc, _pod_qty_receive As Double
        '_pod_um_conv = 1
        '_pod_qty = 1
        '_pod_cost = 0
        '_pod_disc = 0

        'If e.Column.Name = "pod_qty" Then
        '    '********* Cek Qty Processed
        '    Try
        '        _pod_qty_receive = (gv_edit.GetRowCellValue(e.RowHandle, "pod_qty_receive"))
        '    Catch ex As Exception
        '    End Try

        '    If e.Value < _pod_qty_receive Then
        '        MessageBox.Show("Qty PO Can't Lower Than Qty Receive..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        gv_edit.CancelUpdateCurrentRow()
        '        Exit Sub
        '    End If
        '    '********************************

        '    Try
        '        _pod_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "pod_um_conv"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _pod_cost = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _pod_disc = (gv_edit.GetRowCellValue(e.RowHandle, "pod_disc"))
        '    Catch ex As Exception
        '    End Try

        '    _pod_qty_real = e.Value * _pod_um_conv
        '    _pod_qty_cost = (e.Value * _pod_cost) - (e.Value * _pod_cost * _pod_disc)

        '    gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_real", _pod_qty_real)
        '    gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
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
        Dim _asrtr_en_id As Integer = asback_en_id.EditValue

        If _col = "ass_code" Then
            Dim frm As New FAssetSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "reason" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm._type = "asbackd_reason"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "its_desc" Then
            Dim frm As New FItemStatusSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(0, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'With gv_edit
        '    '.SetRowCellValue(e.RowHandle, "pod_en_id", asrtr_en_id.EditValue)
        '    .BestFitColumns()
        'End With
    End Sub
#End Region

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid").ToString
        _colom = "asback_trans_id"
        _table = "asback_mstr"
        _criteria = "asback_code"
        _initial = "asback"
        _type = "ret"
        _title = "Asset Return"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        'ant 22 feb 2011
        Dim _asback_trans_id As String = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_trans_id").ToString

        If _conf_value = "0" Then
            Exit Sub
        End If

        If _asback_trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _asback_trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _asback_trans_id.ToUpper = "I" Then
            MessageBox.Show("Can't Cancel For Approved Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _asback_trans_id.ToUpper = "X" Then
            MessageBox.Show("Data Has Been Canceled..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_oid").ToString
        _colom = "asback_trans_id"
        _table = "asback_mstr"
        _criteria = "asback_code"
        _initial = "asback"
        _type = "ret"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
        '--------------------------------------------------------
    End Sub

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asback_code")
        _type = "ret"
        _user = func_coll.get_wf_iscurrent(_code)

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        _title = "Asset Return"
        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then
                Try
                    gv_email.Columns("asback_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asback_oid] = '" & ds.Tables("smart").Rows(i).Item("asback_oid").ToString & "'")
                    gv_email.BestFitColumns()

                    'gv_email.Columns("asback_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("asback_oid"))
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("asback_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update asback_mstr set asback_trans_id = '" + _trans_id + "'," + _
                                               " asback_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " asback_upd_date = current_timestamp " + _
                                               " where asback_oid = '" + ds.Tables("smart").Rows(i).Item("asback_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("asback_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("asback_code"), "ret")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("asback_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Asset Return", ds.Tables("smart").Rows(i).Item("asback_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

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
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
