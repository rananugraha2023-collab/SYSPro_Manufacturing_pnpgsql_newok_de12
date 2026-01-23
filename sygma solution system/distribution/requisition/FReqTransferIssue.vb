Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FReqTransferIssue
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit As DataSet
    Dim _now As DateTime
    Public _req_oid As String
    Dim mf As New master_new.ModFunction
    Dim _conf_value As String

    Private Sub FReqTransferIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_req_transfer_issue")
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
        reqs_en_id.Properties.DataSource = dt_bantu
        reqs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        reqs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        reqs_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran_global())
        reqs_en_id_to.Properties.DataSource = dt_bantu
        reqs_en_id_to.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        reqs_en_id_to.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        reqs_en_id_to.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            reqs_tran_id.Properties.DataSource = dt_bantu
            reqs_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            reqs_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            reqs_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            reqs_tran_id.Properties.DataSource = dt_bantu
            reqs_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            reqs_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            reqs_tran_id.ItemIndex = 0
        End If

    End Sub

    Private Sub reqs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reqs_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(reqs_en_id.EditValue))
        reqs_si_id.Properties.DataSource = dt_bantu
        reqs_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        reqs_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        reqs_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id.EditValue))
        reqs_loc_id_from.Properties.DataSource = dt_bantu
        reqs_loc_id_from.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_from.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_from.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id.EditValue))
        reqs_loc_id_git.Properties.DataSource = dt_bantu
        reqs_loc_id_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_git.ItemIndex = 0
    End Sub

    Private Sub reqs_en_id_to_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reqs_en_id_to.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(reqs_en_id_to.EditValue))
        reqs_si_to_id.Properties.DataSource = dt_bantu
        reqs_si_to_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        reqs_si_to_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        reqs_si_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id_to.EditValue))
        reqs_loc_id_to.Properties.DataSource = dt_bantu
        reqs_loc_id_to.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_to.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_to.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Requisition Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "reqs_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqds_reqs_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "reqds_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "reqds_qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "reqds_um", False)
        add_column(gv_edit, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqds_cost", False)

        add_column(gv_wf, "wf_ref_oid", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "reqs_oid", False)
        add_column_copy(gv_email, "Requisition Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Transfer Issue Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Transfer Issue Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  reqs_oid, " _
                    & "  reqs_dom_id, " _
                    & "  reqs_add_by, " _
                    & "  reqs_add_date, " _
                    & "  reqs_upd_by, " _
                    & "  reqs_upd_date, " _
                    & "  reqs_req_oid, " _
                    & "  req_code, " _
                    & "  reqs_code, " _
                    & "  reqs_date, " _
                    & "  reqs_receive_date, " _
                    & "  reqs_en_id, " _
                    & "  en_mstr_from.en_desc as en_desc_from, " _
                    & "  reqs_si_id, " _
                    & "  si_mstr_from.si_desc as si_desc_from, " _
                    & "  reqs_loc_id_from, " _
                    & "  loc_mstr_from.loc_desc as loc_desc_from, " _
                    & "  reqs_loc_id_git, " _
                    & "  loc_mstr_git.loc_desc as loc_desc_git, " _
                    & "  reqs_en_id_to, " _
                    & "  en_mstr_to.en_desc as en_desc_to, " _
                    & "  reqs_si_to_id, " _
                    & "  si_mstr_to.si_desc as si_desc_to, " _
                    & "  reqs_loc_id_to, " _
                    & "  loc_mstr_to.loc_desc as loc_desc_to, " _
                    & "  tran_name, " _
                    & "  reqs_remarks, " _
                    & "  reqs_trans_id, " _
                    & "  reqs_tran_id, " _
                    & "  reqs_dt   " _
                    & "FROM  " _
                    & "  public.reqs_mstr " _
                    & "  inner join req_mstr on req_oid = reqs_req_oid " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = reqs_si_id " _
                    & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
                    & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = reqs_loc_id_git " _
                    & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
                    & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = reqs_si_to_id " _
                    & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
                    & "  left outer join public.tran_mstr ON (public.reqs_mstr.reqs_tran_id = public.tran_mstr.tran_id) " _
                    & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and reqs_en_id in (select user_en_id from tconfuserentity " _
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
            & "  reqds_oid, " _
            & "  reqds_add_by, " _
            & "  reqds_add_date, " _
            & "  reqds_upd_by, " _
            & "  reqds_upd_date, " _
            & "  reqds_reqs_oid, " _
            & "  reqds_reqd_oid, " _
            & "  reqds_seq, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_type, " _
            & "  pt_cost, " _
            & "  reqds_qty, " _
            & "  reqds_um, " _
            & "  code_name as reqds_um_name, " _
            & "  reqds_qty_real, reqds_cost,  " _
            & "  reqds_dt " _
            & "FROM  " _
            & "  public.reqsd_det " _
            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr on code_id = reqds_um " _
            & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""
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
                  " inner join reqs_mstr on reqs_code = wf_ref_code " + _
                  " inner join reqsd_det dt on dt.reqds_reqs_oid = reqs_oid " _
                & " where reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and reqs_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                       & "  reqs_oid, " _
                        & "  reqs_dom_id, " _
                        & "  reqs_add_by, " _
                        & "  reqs_add_date, " _
                        & "  reqs_upd_by, " _
                        & "  reqs_upd_date, " _
                        & "  reqs_req_oid, " _
                        & "  req_code, " _
                        & "  reqs_code, " _
                        & "  reqs_date, " _
                        & "  reqs_receive_date, " _
                        & "  reqs_en_id, " _
                        & "  en_mstr_from.en_desc as en_desc_from, " _
                        & "  reqs_si_id, " _
                        & "  si_mstr_from.si_desc as si_desc_from, " _
                        & "  reqs_loc_id_from, " _
                        & "  loc_mstr_from.loc_desc as loc_desc_from, " _
                        & "  reqs_loc_id_git, " _
                        & "  loc_mstr_git.loc_desc as loc_desc_git, " _
                        & "  reqs_en_id_to, " _
                        & "  en_mstr_to.en_desc as en_desc_to, " _
                        & "  reqs_si_to_id, " _
                        & "  si_mstr_to.si_desc as si_desc_to, " _
                        & "  reqs_loc_id_to, " _
                        & "  loc_mstr_to.loc_desc as loc_desc_to, " _
                        & "  reqs_remarks, " _
                        & "  reqs_tran_id, " _
                        & "  reqs_trans_id, " _
                        & "  reqs_dt,   " _
                        & "  reqds_oid, " _
                        & "  reqds_add_by, " _
                        & "  reqds_add_date, " _
                        & "  reqds_upd_by, " _
                        & "  reqds_upd_date, " _
                        & "  reqds_reqs_oid, " _
                        & "  reqds_reqd_oid, " _
                        & "  reqds_seq, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pt_cost, " _
                        & "  reqds_qty, " _
                        & "  reqds_um, " _
                        & "  code_name as reqds_um_name, " _
                        & "  reqds_qty_real, " _
                        & "  reqds_dt " _
                        & "FROM  " _
                        & "  public.reqs_mstr " _
                        & "  inner join req_mstr on req_oid = reqs_req_oid " _
                        & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
                        & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = reqs_si_id " _
                        & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
                        & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = reqs_loc_id_git " _
                        & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
                        & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = reqs_si_to_id " _
                        & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
                        & "  inner join reqsd_det on reqs_oid = reqds_reqs_oid " _
                        & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                        & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                        & "  inner join code_mstr on code_id = reqds_um " _
                        & " where reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                        & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        & " and reqs_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select reqs_oid, reqs_code, reqs_trans_id, false as status from reqs_mstr " _
                & " where reqs_trans_id ~~* 'd' "

            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqds_reqs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid"))
            'gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("reqs_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_oid").FilterInfo = _
                'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("reqs_oid").FilterInfo = _
                'New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid"))
                'gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        reqs_en_id.ItemIndex = 0
        reqs_en_id_to.ItemIndex = 0
        reqs_remarks.Text = ""
        reqs_req_oid.Text = ""
        reqs_tran_id.ItemIndex = 0
        _req_oid = ""
        reqs_date.DateTime = _now
        reqs_date.Focus()
        'reqs_en_id.Focus()

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
                            & "  reqds_oid, " _
                            & "  reqds_add_by, " _
                            & "  reqds_add_date, " _
                            & "  reqds_upd_by, " _
                            & "  reqds_upd_date, " _
                            & "  reqds_reqs_oid, " _
                            & "  reqds_reqd_oid, " _
                            & "  reqds_seq, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_cost, " _
                            & "  pt_type, " _
                            & "  reqds_qty, " _
                            & "  reqds_um, " _
                            & "  code_name as reqds_um_name, " _
                            & "  reqds_qty_real, " _
                            & "  0 as reqds_qty_open, reqds_cost, " _
                            & "  reqds_dt " _
                            & "FROM  " _
                            & "  public.reqsd_det " _
                            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
                            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join code_mstr on code_id = reqds_um " _
                            & "  where pt_id = -99"
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

    Private Sub reqs_req_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles reqs_req_oid.ButtonClick
        Dim frm As New FRequisitionSearch()
        frm.set_win(Me)
        frm._en_id = reqs_en_id_to.EditValue
        frm.type_form = True
        frm._obj = reqs_req_oid
        frm.ShowDialog()
    End Sub

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()

        ds_edit.AcceptChanges()

        Dim _date As Date = reqs_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(reqs_en_id.EditValue, "gcald_ic", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i As Integer

        '*********************
        'Cek part number, Location, site , account
        If reqs_loc_id_from.EditValue = 0 Then
            MessageBox.Show("Location From Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reqs_loc_id_from.Focus()
            Return False
        ElseIf reqs_loc_id_git.EditValue = 0 Then
            MessageBox.Show("Location GIT Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reqs_loc_id_git.Focus()
            Return False
        ElseIf reqs_loc_id_to.EditValue = 0 Then
            MessageBox.Show("Location To Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reqs_loc_id_to.Focus()
            Return False
        ElseIf reqs_si_id.EditValue = 0 Then
            MessageBox.Show("Site From Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reqs_si_id.Focus()
            Return False
        ElseIf reqs_si_to_id.EditValue = 0 Then
            MessageBox.Show("Site To Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reqs_si_to_id.Focus()
            Return False
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pt_id")) = True Then
                MessageBox.Show("Part Number Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _reqs_oid As Guid = Guid.NewGuid

        Dim _serial, _pt_code, _reqs_code As String
        'Dim  _cost_methode As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList

        _reqs_code = func_coll.get_transaction_number("RI", reqs_en_id.GetColumnValue("en_code"), "reqs_mstr", "reqs_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-tr")
        If _tran_id = -1 Then
            MessageBox.Show("Transfer Issue In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Dim _reqs_trn_status As String
        Dim ds_bantu As New DataSet
        _reqs_trn_status = "D" 'Lansung Default Ke Draft
        ds_bantu = func_data.load_aprv_mstr(reqs_tran_id.EditValue)

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
                                            & "  public.reqs_mstr " _
                                            & "( " _
                                            & "  reqs_oid, " _
                                            & "  reqs_dom_id, " _
                                            & "  reqs_en_id, " _
                                            & "  reqs_add_by, " _
                                            & "  reqs_add_date, " _
                                            & "  reqs_code, " _
                                            & "  reqs_date, " _
                                            & "  reqs_req_oid, " _
                                            & "  reqs_en_id_to, " _
                                            & "  reqs_loc_id_from, " _
                                            & "  reqs_loc_id_git, " _
                                            & "  reqs_loc_id_to, " _
                                            & "  reqs_remarks, " _
                                            & "  reqs_dt, " _
                                            & "  reqs_si_id, " _
                                            & "  reqs_tran_id, " _
                                            & "  reqs_trans_id, " _
                                            & "  reqs_si_to_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_reqs_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(reqs_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(_reqs_code) & ",  " _
                                            & SetDate(reqs_date.DateTime) & ",  " _
                                            & SetSetring(_req_oid.ToString) & ",  " _
                                            & SetInteger(reqs_en_id_to.EditValue) & ",  " _
                                            & SetInteger(reqs_loc_id_from.EditValue) & ",  " _
                                            & SetInteger(reqs_loc_id_git.EditValue) & ",  " _
                                            & SetInteger(reqs_loc_id_to.EditValue) & ",  " _
                                            & SetSetring(reqs_remarks.Text) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetInteger(reqs_si_id.EditValue) & ",  " _
                                            & SetInteger(reqs_tran_id.EditValue) & ",  " _
                                            & SetSetring(_reqs_trn_status) & ",  " _
                                            & SetInteger(reqs_si_to_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.reqsd_det " _
                                                    & "( " _
                                                    & "  reqds_oid, " _
                                                    & "  reqds_add_by, " _
                                                    & "  reqds_add_date, " _
                                                    & "  reqds_reqs_oid, " _
                                                    & "  reqds_reqd_oid, " _
                                                    & "  reqds_seq, " _
                                                    & "  reqds_qty, " _
                                                    & "  reqds_um, " _
                                                    & "  reqds_qty_real, " _
                                                    & "  reqds_cost, " _
                                                    & "  reqds_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqds_oid")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                    & SetSetring(_reqs_oid.ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqds_reqd_oid")) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqds_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqds_cost")) & ",  " _
                                                    & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                    & ")"
                                'ini karena didatabase tidak ada um conversion..jadi disamin aja 
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update qty reqd_qty_completed di reqd_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = " update reqd_det set reqd_qty_completed = coalesce(reqd_qty_completed,0) + " + ds_edit.Tables(0).Rows(i).Item("reqds_qty").ToString + _
                                                       " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqds_reqd_oid") + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next


                        '*********************************************************************************
                        'Proses Pengurangan untuk lokasi asal
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                    'If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = reqs_en_id.EditValue
                                    _si_id = reqs_si_id.EditValue
                                    _loc_id = reqs_loc_id_from.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                    'End If
                                End If
                            End If
                        Next

                        '' Tidak ada barang serial untuk requisition transfer issue
                        ' ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        ''For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        ''    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty") > 0 Then
                        ''        i_2 += 1

                        ''        _en_id = ptsfr_en_id.EditValue
                        ''        _si_id = ptsfr_si_id.EditValue
                        ''        _loc_id = ptsfr_loc_id.EditValue
                        ''        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        ''        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        ''        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        ''        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty")
                        ''        'If func_coll.update_invc_mstr_plus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        ''        If func_coll.update_invc_mstr_minus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        ''            sqlTran.Rollback()
                        ''            insert = False
                        ''            Exit Function
                        ''        End If

                        ''        'Update History Inventory
                        ''        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        ''        _cost = _cost * -1
                        ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                        ''            sqlTran.Rollback()
                        ''            insert = False
                        ''            Exit Function
                        ''        End If
                        ''    End If
                        ''Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '        _en_id = reqs_en_id.EditValue
                        '        _si_id = reqs_si_id.EditValue
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                        '        _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                        '        If _cost_methode = "F" Or _cost_methode = "L" Then
                        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '            Return False
                        '            'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    insert = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                insert = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        '*********************************************************************************
                        'Proses penambahan (+) untuk lokasi git
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                    'If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = reqs_en_id.EditValue
                                    _si_id = reqs_si_id.EditValue
                                    _loc_id = reqs_loc_id_git.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                    'End If
                                End If
                            End If
                        Next

                        '' Tidak ada serial untuk requisition
                        '2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        ' ''For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        ' ''    If ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty") > 0 Then
                        ' ''        i_2 += 1

                        ' ''        _en_id = ptsfr_en_id.EditValue
                        ' ''        _si_id = ptsfr_si_id.EditValue
                        ' ''        _loc_id = ptsfr_loc_git.EditValue
                        ' ''        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        ' ''        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        ' ''        _serial = ds_serial.Tables(0).Rows(i).Item("ptsfrds_lot_serial")
                        ' ''        _qty = ds_serial.Tables(0).Rows(i).Item("ptsfrds_qty")
                        ' ''        If func_coll.update_invc_mstr_plus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        ' ''            sqlTran.Rollback()
                        ' ''            insert = False
                        ' ''            Exit Function
                        ' ''        End If

                        ' ''        'Update History Inventory
                        ' ''        _cost = ds_serial.Tables(0).Rows(i).Item("ptsfrds_cost")
                        ' ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _ptsfr_code, _ptsfr_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                        ' ''            sqlTran.Rollback()
                        ' ''            insert = False
                        ' ''            Exit Function
                        ' ''        End If
                        ' ''    End If
                        ' ''Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '        _en_id = reqs_en_id.EditValue
                        '        _si_id = reqs_si_id.EditValue
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                        '        _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                        '        If _cost_methode = "F" Or _cost_methode = "L" Then
                        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '            Return False
                        '            'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    insert = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                insert = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                        '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                        '                         _rcv_oid.ToString, _rcv_code, _
                        '                         func_coll.get_tanggal_sistem, _
                        '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                        '                         "IC", "IC-RPO") = False Then
                        '    sqlTran.Rollback()
                        '    insert = False
                        '    Exit Function
                        'End If

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
                                                        & SetInteger(reqs_en_id.EditValue) & ",  " _
                                                        & SetSetring(reqs_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_reqs_oid.ToString) & ",  " _
                                                        & SetSetring(_reqs_code) & ",  " _
                                                        & SetSetring("Requisition Transfer Issue") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, reqs_en_id.EditValue, 3, _reqs_oid.ToString, _reqs_code, _now.Date) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Req Transfer Isue " & _reqs_code)
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
                        after_success()
                        set_row(_reqs_oid.ToString, "reqs_oid")
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim _reqs_trans_id As String = ""
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(reqs_trans_id,'') as reqs_trans_id from reqs_mstr where reqs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _reqs_trans_id = .DataReader("reqs_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _reqs_trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Delete Closed Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
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

        Dim ssqls As New ArrayList

        If before_delete() = True Then
            Dim _serial, _pt_code, _reqs_code, _reqs_oid As String
            'Dim _cost_methode As String
            Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id, _qty, _row As Integer
            Dim _cost, _cost_avg As Double
            Dim i, i_2 As Integer

            row = BindingContext(ds.Tables(0)).Position
            _row = row

            _reqs_oid = ds.Tables(0).Rows(_row).Item("reqs_oid").ToString
            _reqs_code = ds.Tables(0).Rows(_row).Item("reqs_code")

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

                            '*********************************************************************************
                            'Proses Pengurangan untuk lokasi git
                            'Update Table Inventory Dan Cost Inventory Dan History Inventory
                            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                            i_2 = 0
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid").ToString = _reqs_oid Then
                                    If ds.Tables("detail").Rows(i).Item("pt_type") = "I" Then
                                        If ds.Tables("detail").Rows(i).Item("reqds_qty") > 0 Then
                                            'If ds.Tables("detail").Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                            i_2 += 1

                                            _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                                            _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                                            _loc_id = ds.Tables(0).Rows(_row).Item("reqs_loc_id_git")
                                            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                                            _pt_code = ds.Tables("detail").Rows(i).Item("pt_code")
                                            _serial = "''"
                                            _qty = ds.Tables("detail").Rows(i).Item("reqds_qty")
                                            If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            'Update History Inventory                                    
                                            _qty = _qty * -1
                                            _cost = ds.Tables("detail").Rows(i).Item("reqds_cost")
                                            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_date.DateTime) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If
                                            'End If
                                        End If
                                    End If
                                End If
                            Next

                            ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                            '' tidak ada serial ceunah....tablenya juga gak ada...
                            ''For i = 0 To ds.Tables("detail_serial").Rows.Count - 1
                            '' ini belum dicek ke headernya nanti bisa2 ke proses semua line detailnya...
                            ''    If ds.Tables("detail_serial").Rows(i).Item("reqsds_qty") > 0 Then
                            ''        i_2 += 1

                            ''        _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                            ''        _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                            ''        _loc_id = ds.Tables(0).Rows(_row).Item("reqs_loc_git")
                            ''        _pt_id = ds.Tables("detail_serial").Rows(i).Item("pt_id")
                            ''        _pt_code = ds.Tables("detail_serial").Rows(i).Item("pt_code")
                            ''        _serial = ds.Tables("detail_serial").Rows(i).Item("reqsds_lot_serial")
                            ''        _qty = ds.Tables("detail_serial").Rows(i).Item("reqsds_qty")

                            ''        If func_coll.update_invc_mstr_minus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                            ''            sqlTran.Rollback()
                            ''            delete_data = False
                            ''            Exit Function
                            ''        End If

                            ''        'Update History Inventory
                            ''        _cost = ds.Tables("detail_serial").Rows(i).Item("reqsds_cost")
                            ''        _cost = _cost * -1
                            ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                            ''            sqlTran.Rollback()
                            ''            delete_data = False
                            ''            Exit Function
                            ''        End If
                            ''    End If
                            ''Next

                            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                            'For i = 0 To ds.Tables("detail").Rows.Count - 1
                            '    If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid") = _reqs_oid Then
                            '        If ds.Tables("detail").Rows(i).Item("pt_type") = "I" Then
                            '            _cost_methode = func_coll.get_pt_cost_method(ds.Tables("detail").Rows(i).Item("pt_id").ToString.ToUpper)
                            '            _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                            '            _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                            '            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                            '            _qty = ds.Tables("detail").Rows(i).Item("reqds_qty")
                            '            _cost = ds.Tables("detail").Rows(i).Item("reqds_cost")
                            '            If _cost_methode = "F" Or _cost_methode = "L" Then
                            '                MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                            '                Return False
                            '                'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                            '                '    sqlTran.Rollback()
                            '                '    delete_data = False
                            '                '    Exit Function
                            '                'End If
                            '            ElseIf _cost_methode = "A" Then
                            '                _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                            '                If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                            '                    sqlTran.Rollback()
                            '                    delete_data = False
                            '                    Exit Function
                            '                End If
                            '            End If
                            '        End If
                            '    End If
                            'Next
                            '*****************************************************************************************

                            '*********************************************************************************
                            'Proses penambahan (+) untuk lokasi asal
                            'Update Table Inventory Dan Cost Inventory Dan History Inventory
                            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                            i_2 = 0
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid").ToString = _reqs_oid Then
                                    If ds.Tables("detail").Rows(i).Item("pt_type") = "I" Then
                                        If ds.Tables("detail").Rows(i).Item("reqds_qty") > 0 Then
                                            'If ds.Tables("detail").Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                            i_2 += 1

                                            _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                                            _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                                            _loc_id = ds.Tables(0).Rows(_row).Item("reqs_loc_id_from")
                                            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                                            _pt_code = ds.Tables("detail").Rows(i).Item("pt_code")
                                            _serial = "''"
                                            _qty = ds.Tables("detail").Rows(i).Item("reqds_qty")
                                            If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            'Update History Inventory                                    
                                            _cost = ds.Tables("detail").Rows(i).Item("reqds_cost")
                                            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_date.DateTime) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If
                                            'End If
                                        End If
                                    End If
                                End If
                            Next

                            ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                            '' didatabase nya ga ada tabel serial jadi ga dibuatkan
                            ''For i = 0 To ds.Tables("detail_serial").Rows.Count - 1
                            '' ini belum di cek relasi ke headernya..
                            ''    If ds.Tables("detail_serial").Rows(i).Item("reqsds_qty") > 0 Then
                            ''        i_2 += 1

                            ''        _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                            ''        _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                            ''        _loc_id = ds.Tables(0).Rows(_row).Item("reqs_loc_id")
                            ''        _pt_id = ds.Tables("detail_serial").Rows(i).Item("pt_id")
                            ''        _pt_code = ds.Tables("detail_serial").Rows(i).Item("pt_code")
                            ''        _serial = ds.Tables("detail_serial").Rows(i).Item("reqsds_lot_serial")
                            ''        _qty = ds.Tables("detail_serial").Rows(i).Item("reqsds_qty")
                            ''        If func_coll.update_invc_mstr_plus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                            ''            sqlTran.Rollback()
                            ''            delete_data = False
                            ''            Exit Function
                            ''        End If

                            ''        'Update History Inventory
                            ''        _cost = ds.Tables("detail_serial").Rows(i).Item("reqsds_cost")
                            ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _reqs_code, _reqs_oid.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                            ''            sqlTran.Rollback()
                            ''            delete_data = False
                            ''            Exit Function
                            ''        End If
                            ''    End If
                            ''Next

                            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                            'For i = 0 To ds.Tables("detail").Rows.Count - 1
                            '    If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid") = _reqs_oid Then
                            '        If ds.Tables("detail").Rows(i).Item("pt_type") = "I" Then
                            '            _cost_methode = func_coll.get_pt_cost_method(ds.Tables("detail").Rows(i).Item("pt_id").ToString.ToUpper)
                            '            _en_id = ds.Tables(0).Rows(_row).Item("reqs_en_id")
                            '            _si_id = ds.Tables(0).Rows(_row).Item("reqs_si_id")
                            '            _pt_id = ds.Tables("detail").Rows(i).Item("pt_id")
                            '            _qty = ds.Tables("detail").Rows(i).Item("reqds_qty")
                            '            _cost = ds.Tables("detail").Rows(i).Item("reqds_cost")
                            '            If _cost_methode = "F" Or _cost_methode = "L" Then
                            '                MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                            '                Return False
                            '                'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                            '                '    sqlTran.Rollback()
                            '                '    delete_data = False
                            '                '    Exit Function
                            '                'End If
                            '            ElseIf _cost_methode = "A" Then
                            '                _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                            '                If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                            '                    sqlTran.Rollback()
                            '                    delete_data = False
                            '                    Exit Function
                            '                End If
                            '            End If
                            '        End If
                            '    End If
                            'Next

                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid").ToString = _reqs_oid Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update reqd_det set reqd_qty_completed = reqd_qty_completed - " + ds.Tables("detail").Rows(i).Item("reqds_qty").ToString + _
                                                           " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqds_reqd_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            Next
                            '*****************************************************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from reqs_mstr where reqs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'If func_coll.delete_glt_det_ap(objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid"), _
                            '                               ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_code")) = False Then
                            '    sqlTran.Rollback()
                            '    Exit Function
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

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_en_id")
        _type = 3
        _table = "reqs_mstr"
        _initial = "reqs"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  reqs_oid, " _
            & "  reqs_dom_id, " _
            & "  reqs_en_id, " _
            & "  reqs_add_by, " _
            & "  reqs_add_date, " _
            & "  reqs_upd_by, " _
            & "  reqs_upd_date, " _
            & "  reqs_code, " _
            & "  reqs_date, " _
            & "  reqs_req_oid, " _
            & "  reqs_en_id_to, " _
            & "  reqs_loc_id_from, " _
            & "  reqs_loc_id_git, " _
            & "  reqs_loc_id_to, " _
            & "  reqs_trans_id, " _
            & "  reqs_receive_date, " _
            & "  reqs_remarks, " _
            & "  reqs_dt, " _
            & "  reqs_si_id, " _
            & "  reqs_si_to_id, " _
            & "  req_code, " _
            & "  en_mstr_from.en_desc as en_desc_from, " _
            & "  en_mstr_to.en_desc as en_desc_to, " _
            & "  loc_mstr_from.loc_desc as loc_desc_from, " _
            & "  loc_mstr_to.loc_desc as loc_desc_to, " _
            & "  cmaddr_mstr_from.cmaddr_name as cmaddr_name_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_1 as cmaddr_line_1_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_2 as cmaddr_line_2_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_3 as cmaddr_line_3_from, " _
            & "  cmaddr_mstr_to.cmaddr_name as cmaddr_name_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_1 as cmaddr_line_1_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_2 as cmaddr_line_2_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_3 as cmaddr_line_3_to, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  reqds_qty, " _
            & "  code_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.reqs_mstr " _
            & "  inner join req_mstr on req_oid = reqs_req_oid " _
            & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
            & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
            & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
            & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
            & "  inner join cmaddr_mstr cmaddr_mstr_from on cmaddr_mstr_from.cmaddr_en_id = reqs_en_id " _
            & "  inner join cmaddr_mstr cmaddr_mstr_to on cmaddr_mstr_to.cmaddr_en_id = reqs_en_id_to " _
            & "  inner join reqsd_det on reqsd_det.reqds_reqs_oid = reqs_oid " _
            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr on code_id = reqds_um " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = reqs_oid " _
            & "  where reqs_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRReqTransferIssue"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        frm.ShowDialog()


    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString
        _colom = "reqs_trans_id"
        _table = "reqs_mstr"
        _criteria = "reqs_code"
        _initial = "reqs"
        _type = "ri"
        _title = "Requisition Transfer Issue"

        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString
        _colom = "reqs_trans_id"
        _table = "reqs_mstr"
        _criteria = "reqs_code"
        _initial = "reqs"
        _type = "ri"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String

        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        _type = "ri"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Requisition Transfer Issue"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

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
                    gv_email.Columns("reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("reqs_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("reqs_code"), 0)
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
                                .Command.CommandText = "update reqs_mstr set reqs_trans_id = '" + _trans_id + "'," + _
                                               " reqs_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " reqs_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where reqs_oid = '" + ds.Tables("smart").Rows(i).Item("reqs_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("reqs_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("reqs_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("reqs_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Requisition Transfer Issue", ds.Tables("smart").Rows(i).Item("reqs_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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
        MessageBox.Show("Welldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
