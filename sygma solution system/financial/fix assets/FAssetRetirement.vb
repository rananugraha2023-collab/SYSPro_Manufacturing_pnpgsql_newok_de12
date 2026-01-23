Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FAssetRetirement
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr As String
    Dim ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim mf As New master_new.ModFunction
    Dim _conf_value As String

#Region "Seting Awal"
    Private Sub FAssetRetirement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_asset_retirement")

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
        asrtr_en_id.Properties.DataSource = dt_bantu
        asrtr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        asrtr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        asrtr_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_transaction())
        'asrtr_tran_id.Properties.DataSource = dt_bantu
        'asrtr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'asrtr_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
        'asrtr_tran_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_tran_mstr())
        'asrtr_tran_id.Properties.DataSource = dt_bantu
        'asrtr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'asrtr_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        'asrtr_tran_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            asrtr_tran_id.Properties.DataSource = dt_bantu
            asrtr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            asrtr_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            asrtr_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            asrtr_tran_id.Properties.DataSource = dt_bantu
            asrtr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            asrtr_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            asrtr_tran_id.ItemIndex = 0
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "asrtr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "asrtr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Dispose Date", "asrtr_dispose_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "asrtr_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "asrtr_trans_id", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "asrtr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "asrtr_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "asrtr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "asrtr_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "asrtrd_oid", False)
        add_column(gv_edit, "asrtrd_asrtr_oid", False)
        add_column(gv_edit, "asrtrd_ass_id", False)
        add_column(gv_edit, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Dispose Date", "asrtrd_dispose_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "asrtrd_type", False)
        add_column(gv_edit, "Type", "type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "asrtrd_reason", False)
        add_column(gv_edit, "Dispose Reson", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Dispose Cost", "asrtrd_dispose_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Remarks", "asrtrd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "asrtrd_oid", False)
        add_column(gv_detail, "asrtrd_asrtr_oid", False)
        add_column_copy(gv_detail, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Dispose Date", "asrtrd_dispose_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Type", "type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Dispose Reson", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Dispose Cost", "asrtrd_dispose_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Remarks", "asrtrd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "asrtrd_oid", False)
        add_column(gv_email, "asrtrd_asrtr_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Retirement Code", "asrtr_code", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Dispose Date", "asrtrd_dispose_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Type", "type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Dispose Reson", "reason", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Dispose Cost", "asrtrd_dispose_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Remarks", "asrtrd_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Retirement Code", "asrtr_code", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  asrtr_oid, " _
                    & "  asrtr_dom_id, " _
                    & "  asrtr_en_id,en_desc, " _
                    & "  asrtr_add_by, " _
                    & "  asrtr_add_date, " _
                    & "  asrtr_upd_by, " _
                    & "  asrtr_upd_date, " _
                    & "  asrtr_code, " _
                    & "  asrtr_date, " _
                    & "  asrtr_dispose_date, " _
                    & "  asrtr_rmks, " _
                    & "  asrtr_tran_id,tran_name, " _
                    & "  asrtr_trans_id, " _
                    & "  asrtr_trans_rmks, " _
                    & "  asrtr_dt " _
                    & "FROM  " _
                    & "  public.asrtr_mstr " _
                    & "  inner join en_mstr on en_id = asrtr_en_id " _
                    & "  inner join tran_mstr on tran_id = asrtr_tran_id " _
                    & " where asrtr_dispose_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and asrtr_dispose_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and asrtr_en_id in (select user_en_id from tconfuserentity " _
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
            & "  asrtrd_oid, " _
            & "  asrtrd_asrtr_oid, " _
            & "  asrtrd_seq, " _
            & "  asrtrd_ass_id,ass_code,ass_desc,pt_code, " _
            & "  asrtrd_dispose_date, " _
            & "  asrtrd_type,type.code_name as type, " _
            & "  asrtrd_reason,rs.code_name as reason, " _
            & "  asrtrd_dispose_cost, " _
            & "  asrtrd_rmks, " _
            & "  asrtrd_dt " _
            & "FROM  " _
            & "  public.asrtrd_det " _
            & "  inner join asrtr_mstr on asrtr_oid = asrtrd_asrtr_oid " _
            & "  inner join ass_mstr on ass_id = asrtrd_ass_id " _
            & "  inner join pt_mstr on pt_id = ass_pt_id " _
            & "  inner join code_mstr rs on rs.code_id = asrtrd_reason " _
            & "  inner join code_mstr type on type.code_id = asrtrd_type " _
            & " where asrtr_dispose_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & " and asrtr_dispose_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

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
                  " inner join asrtr_mstr on asrtr_code = wf_ref_code " + _
                  " inner join asrtrd_det dt on dt.asrtrd_asrtr_oid = asrtr_oid " _
                & " where asrtr_dispose_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and asrtr_dispose_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and asrtr_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  asrtrd_oid, " _
                & "  asrtrd_asrtr_oid,en_desc, " _
                & "  asrtr_code,asrtr_dispose_date,asrtr_rmks, " _
                & "  asrtrd_seq, " _
                & "  asrtrd_ass_id,ass_code,ass_desc,pt_code, " _
                & "  asrtrd_dispose_date, " _
                & "  asrtrd_type,type.code_name as type, " _
                & "  asrtrd_reason,rs.code_name as reason, " _
                & "  asrtrd_dispose_cost, " _
                & "  asrtrd_rmks, " _
                & "  asrtrd_dt " _
                & "FROM  " _
                & "  public.asrtrd_det " _
                & "  inner join asrtr_mstr on asrtr_oid = asrtrd_asrtr_oid " _
                & "  inner join en_mstr on en_id = asrtr_en_id " _
                & "  inner join ass_mstr on ass_id = asrtrd_ass_id " _
                & "  inner join pt_mstr on pt_id = ass_pt_id " _
                & "  inner join code_mstr rs on rs.code_id = asrtrd_reason " _
                & "  inner join code_mstr type on type.code_id = asrtrd_type " _
                & " where asrtr_dispose_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and asrtr_dispose_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select asrtr_oid, asrtr_code, asrtr_trans_id, false as status from asrtr_mstr " _
                & " where asrtr_trans_id ~~* 'd' "
            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("asrtrd_asrtr_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asrtrd_asrtr_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("asrtrd_asrtr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid"))
            'gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("asrtrd_asrtr_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asrtrd_asrtr_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("asrtrd_asrtr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid"))
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
        asrtr_en_id.Focus()
        asrtr_en_id.ItemIndex = 0
        asrtr_date.DateTime = _now
        asrtr_dispose_date.DateTime = _now
        asrtr_rmks.Text = ""
        asrtr_tran_id.ItemIndex = 0

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
                            & "  asrtrd_oid, " _
                            & "  asrtrd_asrtr_oid, " _
                            & "  asrtrd_seq, " _
                            & "  asrtrd_ass_id,ass_code,ass_desc,pt_code, " _
                            & "  asrtrd_dispose_date, " _
                            & "  asrtrd_type,type.code_name as type, " _
                            & "  asrtrd_reason,rs.code_name as reason, " _
                            & "  asrtrd_dispose_cost, " _
                            & "  asrtrd_rmks, " _
                            & "  asrtrd_dt " _
                            & "FROM  " _
                            & "  public.asrtrd_det " _
                            & "  inner join asrtr_mstr on asrtr_oid = asrtrd_asrtr_oid " _
                            & "  inner join ass_mstr on ass_id = asrtrd_ass_id " _
                            & "  inner join pt_mstr on pt_id = ass_pt_id " _
                            & "  inner join code_mstr rs on rs.code_id = asrtrd_reason " _
                            & "  inner join code_mstr type on type.code_id = asrtrd_type " _
                            & " where asrtrd_seq = -99"
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
        Dim _po_oid As Guid
        _po_oid = Guid.NewGuid

        Dim _asrtr_code As String
        Dim i As Integer
        Dim ssqls As New ArrayList

        _asrtr_code = func_coll.get_transaction_number("RT", asrtr_en_id.GetColumnValue("en_code"), "asrtr_mstr", "asrtr_code")

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(asrtr_tran_id.EditValue)
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
                                            & "  public.asrtr_mstr " _
                                            & "( " _
                                            & "  asrtr_oid, " _
                                            & "  asrtr_dom_id, " _
                                            & "  asrtr_en_id, " _
                                            & "  asrtr_add_by, " _
                                            & "  asrtr_add_date, " _
                                            & "  asrtr_code, " _
                                            & "  asrtr_date, " _
                                            & "  asrtr_dispose_date, " _
                                            & "  asrtr_rmks, " _
                                            & "  asrtr_tran_id, " _
                                            & "  asrtr_trans_id, " _
                                            & "  asrtr_trans_rmks, " _
                                            & "  asrtr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(asrtr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetSetring(_asrtr_code) & ",  " _
                                            & SetDate(asrtr_date.DateTime) & ",  " _
                                            & SetDate(asrtr_dispose_date.DateTime) & ",  " _
                                            & SetSetring(asrtr_rmks.Text) & ",  " _
                                            & SetInteger(asrtr_tran_id.EditValue) & ",  " _
                                            & "'D'" & ",  " _
                                            & "'-'" & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.asrtrd_det " _
                                                & "( " _
                                                & "  asrtrd_oid, " _
                                                & "  asrtrd_asrtr_oid, " _
                                                & "  asrtrd_seq, " _
                                                & "  asrtrd_ass_id, " _
                                                & "  asrtrd_dispose_date, " _
                                                & "  asrtrd_type, " _
                                                & "  asrtrd_reason, " _
                                                & "  asrtrd_dispose_cost, " _
                                                & "  asrtrd_rmks, " _
                                                & "  asrtrd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_po_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_ass_id")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_date")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_type")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_reason")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_cost")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("asrtrd_rmks")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If _conf_value = "0" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE ass_mstr  " _
                                                        & "  set ass_qty_del = 1, " _
                                                        & "  ass_its_id =  2, " _
                                                        & "  ass_emp_id = 0, " _
                                                        & "  ass_emp_dept = 0, " _
                                                        & "  ass_emp_rg = 0, " _
                                                        & "  ass_disp_amount = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_cost")) + "," _
                                                        & "  ass_disp_date = " + SetDate(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_date")) _
                                                        & "  where ass_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_ass_id"))
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                ssqls.Add(.Command.CommandText)
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
                                                        & SetInteger(asrtr_en_id.EditValue) & ",  " _
                                                        & SetInteger(asrtr_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_po_oid.ToString) & ",  " _
                                                        & SetSetring(_asrtr_code) & ",  " _
                                                        & SetSetring("Asset Retirement") & ",  " _
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
                        set_row(_po_oid.ToString, "asrtr_oid")
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

    Public Overrides Function edit_data() As Boolean
        If _conf_value = "0" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")) > 0 Then
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
                _po_oid_mstr = .Item("asrtr_oid").ToString
                asrtr_en_id.EditValue = .Item("asrtr_en_id")
                asrtr_date.DateTime = .Item("asrtr_date")
                asrtr_dispose_date.DateTime = .Item("asrtr_dispose_date")
                asrtr_rmks.Text = SetString(.Item("asrtr_rmks"))
                asrtr_tran_id.EditValue = .Item("asrtr_tran_id")
            End With
            asrtr_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  asrtrd_oid, " _
                            & "  asrtrd_asrtr_oid, " _
                            & "  asrtrd_seq, " _
                            & "  asrtrd_ass_id,ass_code,ass_desc,pt_code, " _
                            & "  asrtrd_dispose_date, " _
                            & "  asrtrd_type,type.code_name as type, " _
                            & "  asrtrd_reason,rs.code_name as reason, " _
                            & "  asrtrd_dispose_cost, " _
                            & "  asrtrd_rmks, " _
                            & "  asrtrd_dt " _
                            & "FROM  " _
                            & "  public.asrtrd_det " _
                            & "  inner join asrtr_mstr on asrtr_oid = asrtrd_asrtr_oid " _
                            & "  inner join ass_mstr on ass_id = asrtrd_ass_id " _
                            & "  inner join pt_mstr on pt_id = ass_pt_id " _
                            & "  inner join code_mstr rs on rs.code_id = asrtrd_reason " _
                            & "  inner join code_mstr type on type.code_id = asrtrd_type " _
                            & " where asrtrd_asrtr_oid = '" + ds.Tables(0).Rows(row).Item("asrtr_oid").ToString + "'"

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

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _pod_qty_receive As Double = 0
        Dim i As Integer
        Dim ds_bantu As New DataSet

        ds_bantu = func_data.load_aprv_mstr(asrtr_tran_id.EditValue)
        Dim ssqls As New ArrayList

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
                                            & "  public.asrtr_mstr   " _
                                            & "SET  " _
                                            & "  asrtr_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  asrtr_en_id = " & asrtr_en_id.EditValue & ",  " _
                                            & "  asrtr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  asrtr_upd_date = current_timestamp " & ",  " _
                                            & "  asrtr_code = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")) & ",  " _
                                            & "  asrtr_date = " & SetDate(asrtr_date.DateTime) & ",  " _
                                            & "  asrtr_dispose_date = " & SetDate(asrtr_dispose_date.DateTime) & ",  " _
                                            & "  asrtr_rmks = " & SetSetring(asrtr_rmks.Text) & ",  " _
                                            & "  asrtr_tran_id = " & SetInteger(asrtr_tran_id.EditValue) & ",  " _
                                            & "  asrtr_trans_id = 'D' " & ",  " _
                                            & "  asrtr_trans_rmks = '-' " & ",  " _
                                            & "  asrtr_dt = current_timestamp " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  asrtr_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid")) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from asrtrd_det where asrtrd_asrtr_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.asrtrd_det " _
                                            & "( " _
                                            & "  asrtrd_oid, " _
                                            & "  asrtrd_asrtr_oid, " _
                                            & "  asrtrd_seq, " _
                                            & "  asrtrd_ass_id, " _
                                            & "  asrtrd_dispose_date, " _
                                            & "  asrtrd_type, " _
                                            & "  asrtrd_reason, " _
                                            & "  asrtrd_dispose_cost, " _
                                            & "  asrtrd_rmks, " _
                                            & "  asrtrd_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("asrtrd_asrtr_oid")) & ",  " _
                                            & i & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_ass_id")) & ",  " _
                                            & SetDate(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_date")) & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_type")) & ",  " _
                                            & SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_reason")) & ",  " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("asrtrd_dispose_cost")) & ",  " _
                                            & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("asrtrd_rmks")) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

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
                                                    & SetInteger(asrtr_en_id.EditValue) & ",  " _
                                                    & SetSetring(asrtr_tran_id.EditValue) & ",  " _
                                                    & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                                    & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")) & ",  " _
                                                    & SetSetring("Asset Retirement") & ",  " _
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
                        set_row(_po_oid_mstr, "asrtr_oid")
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
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    'Public Overrides Function before_delete() As Boolean
    '    before_delete = True

    '    Return before_delete
    'End Function

    'Public Overrides Function delete_data() As Boolean
    '    row = BindingContext(ds.Tables(0)).Position
    '    Dim func_coll As New function_collection
    '    If func_coll.get_status_wf(ds.Tables(0).Rows(row).Item("asrtr_code")) > 0 Then
    '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Function
    '    End If

    '    Dim _asrtr_oid As String
    '    _asrtr_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid")

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
    '                        .Command.CommandText = "delete from asrtr_mstr where asrtr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid") + "'"
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        'For i = 0 To ds.Tables("detail").Rows.Count - 1
    '                        '    If ds.Tables("detail").Rows(i).Item("asrtrd_asrtr_oid") = _asrtr_oid Then
    '                        '        .Command.CommandType = CommandType.Text
    '                        '        .Command.CommandText = "UPDATE ass_mstr  " _
    '                        '                            & "  set ass_qty_del = 0, " _
    '                        '                            & "  ass_confirm = 'Y', " _
    '                        '                            & "  ass_its_id =  1 " _
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
        Dim _asrtr_en_id As Integer = asrtr_en_id.EditValue

        If _col = "ass_code" Then
            Dim frm As New FAssetSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "type" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm._type = "retirement_type"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "reason" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _asrtr_en_id
            frm._type = "retirement_reason"
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
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid").ToString
        _colom = "asrtr_trans_id"
        _table = "asrtr_mstr"
        _criteria = "asrtr_code"
        _initial = "asrtr"
        _type = "rt"
        _title = "Asset Retirement"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_oid").ToString
        _colom = "asrtr_trans_id"
        _table = "asrtr_mstr"
        _criteria = "asrtr_code"
        _initial = "asrtr"
        _type = "rt"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("asrtr_code")
        _type = "rt"
        _user = func_coll.get_wf_iscurrent(_code)

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        _title = "Asset Retirement"
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
                    gv_email.Columns("asrtr_oid").FilterInfo = _
                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[asrtr_oid] = '" & ds.Tables("smart").Rows(i).Item("asrtr_oid").ToString & "'")
                    gv_email.BestFitColumns()

                    'gv_email.Columns("asrtr_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("asrtr_oid"))
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("asrtr_code"), 0)
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
                                .Command.CommandText = "update asrtr_mstr set asrtr_trans_id = '" + _trans_id + "'," + _
                                               " asrtr_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " asrtr_upd_date = current_timestamp " + _
                                               " where asrtr_oid = '" + ds.Tables("smart").Rows(i).Item("asrtr_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("asrtr_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("asrtr_code"), "rt")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("asrtr_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Asset Retirement", ds.Tables("smart").Rows(i).Item("asrtr_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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
