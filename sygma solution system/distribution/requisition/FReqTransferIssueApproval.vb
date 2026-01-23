Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FReqTransferIssueApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FReqTransferIssueApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        gv_os.Columns("status").VisibleIndex = 0

        AddHandler gv_os.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_os.ColumnFilterChanged, AddressOf relation_detail

        AddHandler gv_all.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_all.ColumnFilterChanged, AddressOf relation_detail

        _user_approval = get_user_approval()
    End Sub

    Private Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next
        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wfs_status())
        le_wfs_status.Properties.DataSource = dt_bantu
        le_wfs_status.Properties.DisplayMember = dt_bantu.Columns("wfs_desc").ToString
        le_wfs_status.Properties.ValueMember = dt_bantu.Columns("wfs_id").ToString
        le_wfs_status.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_os, "Requisition Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Transfer Issue Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Transfer Issue Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "reqs_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Requisition Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Transfer Issue Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Transfer Issue Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "reqs_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqds_oid", False)
        add_column(gv_detail, "reqds_reqs_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
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
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                'ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  false as status, " _
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
                                & "  reqs_dt, wf_seq, coalesce(useremail,'') as useremail  " _
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
                                & " inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                                & " inner join tconfuser on usernama = reqs_add_by " _
                                & " where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and wf_iscurrent ~~* 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
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
                                & " inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                                & " where reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and reqs_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and reqs_trans_id <> 'D' "
                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If
                        
                        bestfit_column()
                        load_data_grid_detail()
                        relation_detail()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            If ds.Tables("all").Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        ds_detail = New DataSet
        Try
            Using objload As New master_new.WDABasepgsql("", "")
                With objload
                    If xtc_master.SelectedTabPageIndex = 0 Then
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
                        & "  reqds_qty, " _
                        & "  reqds_um, " _
                        & "  code_name as reqds_um_name, " _
                        & "  reqds_qty_real, " _
                        & "  reqds_dt " _
                        & "FROM  " _
                        & "  public.reqsd_det " _
                        & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
                        & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                        & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                        & "  inner join code_mstr on code_id = reqds_um " _
                        & "  inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                        & "  where reqs_en_id in (select user_en_id from tconfuserentity " _
                                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & "  and wf_user_id in (" + _user_approval + ")" _
                        & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join reqs_mstr on reqs_code = wf_ref_code " + _
                              " inner join reqsd_det dt on dt.reqds_reqs_oid = reqs_oid " _
                            & " where wf_ref_code in (select reqs_code from reqs_mstr inner join wf_mstr on wf_ref_code = reqs_code " _
                                                  & " where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
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
                            & " inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                            & " where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
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
                            & "  reqds_qty, " _
                            & "  reqds_um, " _
                            & "  code_name as reqds_um_name, " _
                            & "  reqds_qty_real, " _
                            & "  reqds_dt " _
                            & "FROM  " _
                            & "  public.reqsd_det " _
                            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
                            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join code_mstr on code_id = reqds_um " _
                            & "  inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                            & "  where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join reqs_mstr on reqs_code = wf_ref_code " + _
                              " inner join reqsd_det dt on dt.reqds_reqs_oid = reqs_oid " _
                            & " where wf_ref_code in (select reqs_code from reqs_mstr inner join wf_mstr on wf_ref_code = reqs_code " _
                                                  & " where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
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
                            & " inner join wf_mstr wm on wf_ref_oid = reqs_oid " _
                            & " where reqs_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and reqs_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqds_reqs_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("reqs_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqs_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("reqds_reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("reqs_oid"))
                'gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqds_reqs_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("reqs_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqs_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("reqds_reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("reqs_oid"))
                'gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub xtc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtc_master.Click
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        End If
    End Sub

    Private Sub sb_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_process.Click
        Dim i, j As Integer
        j = 0

        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables.Count = 0 Then
                Exit Sub
            ElseIf ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If

            For i = 0 To ds.Tables("os").Rows.Count - 1
                If ds.Tables("os").Rows(i).Item("status") = True Then
                    j += 1
                End If
            Next

            If j = 0 Then
                MessageBox.Show("Please Select Data, First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        Dim _colom, _table, _initial, _type, _title As String

        _colom = "reqs_trans_id"
        _table = "reqs_mstr"
        _initial = "reqs"
        _type = "ir"
        _title = "Requisition Transfer Issue"

        If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
            approve_wf(_colom, _table, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, gv_email, _title)
        ElseIf le_wfs_status.EditValue = 2 And xtc_master.SelectedTabPageIndex = 0 Then
            hold_wf(_initial, le_wfs_status.EditValue, Trim(te_remark.Text), nud_hold_day.Value, _user_approval, ds)
        ElseIf le_wfs_status.EditValue = 3 And xtc_master.SelectedTabPageIndex = 0 Then
            cancel_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        ElseIf le_wfs_status.EditValue = 4 And xtc_master.SelectedTabPageIndex = 0 Then
            rollback_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        End If
    End Sub

    Private Sub le_wfs_status_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles le_wfs_status.EditValueChanged
        If le_wfs_status.EditValue = 2 Then
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

   
End Class
