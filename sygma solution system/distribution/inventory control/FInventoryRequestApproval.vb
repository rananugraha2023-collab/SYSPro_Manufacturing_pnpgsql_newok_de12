Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FInventoryRequestApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FInventoryRequestApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "pbd_oid", False)
        add_column(gv_detail, "pbd_pb_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Processed", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Complete", "pbd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "pb_oid", False)
        add_column(gv_email, "pbd_pb_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)
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
                                & "  public.en_mstr.en_desc, " _
                                & "  public.pb_mstr.pb_oid, " _
                                & "  public.pb_mstr.pb_dom_id, " _
                                & "  public.pb_mstr.pb_en_id, " _
                                & "  public.pb_mstr.pb_upd_date, " _
                                & "  public.pb_mstr.pb_upd_by, " _
                                & "  public.pb_mstr.pb_add_date, " _
                                & "  public.pb_mstr.pb_add_by, " _
                                & "  public.pb_mstr.pb_code, " _
                                & "  public.pb_mstr.pb_date, " _
                                & "  public.pb_mstr.pb_due_date, " _
                                & "  public.pb_mstr.pb_requested, " _
                                & "  public.pb_mstr.pb_end_user, " _
                                & "  public.pb_mstr.pb_rmks, " _
                                & "  public.pb_mstr.pb_status, " _
                                & "  public.pb_mstr.pb_tran_id, " _
                                & "  public.pb_mstr.pb_trans_id, " _
                                & "  public.pb_mstr.pb_close_date, " _
                                & "  public.pb_mstr.pb_dt, wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM " _
                                & "  public.pb_mstr " _
                                & "  INNER JOIN public.en_mstr ON (public.pb_mstr.pb_en_id = public.en_mstr.en_id) " _
                                & " inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                                & " inner join tconfuser on usernama = pb_add_by " _
                                & " where pb_en_id in (select user_en_id from tconfuserentity " _
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
                                & "  public.en_mstr.en_desc, " _
                                & "  public.pb_mstr.pb_oid, " _
                                & "  public.pb_mstr.pb_dom_id, " _
                                & "  public.pb_mstr.pb_en_id, " _
                                & "  public.pb_mstr.pb_upd_date, " _
                                & "  public.pb_mstr.pb_upd_by, " _
                                & "  public.pb_mstr.pb_add_date, " _
                                & "  public.pb_mstr.pb_add_by, " _
                                & "  public.pb_mstr.pb_code, " _
                                & "  public.pb_mstr.pb_date, " _
                                & "  public.pb_mstr.pb_due_date, " _
                                & "  public.pb_mstr.pb_requested, " _
                                & "  public.pb_mstr.pb_end_user, " _
                                & "  public.pb_mstr.pb_rmks, " _
                                & "  public.pb_mstr.pb_status, " _
                                & "  public.pb_mstr.pb_tran_id, " _
                                & "  public.pb_mstr.pb_trans_id, " _
                                & "  public.pb_mstr.pb_close_date, " _
                                & "  public.pb_mstr.pb_dt " _
                                & "FROM " _
                                & "  public.pb_mstr " _
                                & "  INNER JOIN public.en_mstr ON (public.pb_mstr.pb_en_id = public.en_mstr.en_id) " _
                                & " inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                                & " where pb_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and pb_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and pb_trans_id <> 'D' "
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
                        & "  public.pbd_det.pbd_oid, " _
                        & "  public.pbd_det.pbd_dom_id, " _
                        & "  public.pbd_det.pbd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.pbd_det.pbd_add_by, " _
                        & "  public.pbd_det.pbd_add_date, " _
                        & "  public.pbd_det.pbd_upd_by, " _
                        & "  public.pbd_det.pbd_upd_date, " _
                        & "  public.pbd_det.pbd_pb_oid, " _
                        & "  public.pbd_det.pbd_seq, " _
                        & "  public.pbd_det.pbd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  public.pbd_det.pbd_rmks, " _
                        & "  public.pbd_det.pbd_end_user, " _
                        & "  public.pbd_det.pbd_due_date, " _
                        & "  public.pbd_det.pbd_qty, " _
                        & "  public.pbd_det.pbd_qty_processed, " _
                        & "  public.pbd_det.pbd_qty_completed, " _
                        & "  public.pbd_det.pbd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  public.pbd_det.pbd_status, " _
                        & "  public.pbd_det.pbd_dt " _
                        & "  FROM " _
                        & "  public.pbd_det " _
                        & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.pb_mstr ON (public.pbd_det.pbd_pb_oid = public.pb_mstr.pb_oid) " _
                        & "  inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                        & "  where pb_en_id in (select user_en_id from tconfuserentity " _
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
                              " inner join pb_mstr on pb_code = wf_ref_code " + _
                              " inner join pbd_det dt on dt.pbd_pb_oid = pb_oid " _
                            & " where wf_ref_code in (select pb_code from pb_mstr inner join wf_mstr on wf_ref_code = pb_code " _
                                                  & " where pb_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pb_mstr.pb_oid, " _
                            & "  public.pb_mstr.pb_dom_id, " _
                            & "  public.pb_mstr.pb_en_id, " _
                            & "  public.pb_mstr.pb_upd_date, " _
                            & "  public.pb_mstr.pb_upd_by, " _
                            & "  public.pb_mstr.pb_add_date, " _
                            & "  public.pb_mstr.pb_add_by, " _
                            & "  public.pb_mstr.pb_code, " _
                            & "  public.pb_mstr.pb_date, " _
                            & "  public.pb_mstr.pb_due_date, " _
                            & "  public.pb_mstr.pb_requested, " _
                            & "  public.pb_mstr.pb_end_user, " _
                            & "  public.pb_mstr.pb_rmks, " _
                            & "  public.pb_mstr.pb_status, " _
                            & "  public.pb_mstr.pb_close_date, " _
                            & "  public.pb_mstr.pb_dt, " _
                            & "  public.pbd_det.pbd_oid, " _
                            & "  public.pbd_det.pbd_dom_id, " _
                            & "  public.pbd_det.pbd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pbd_det.pbd_add_by, " _
                            & "  public.pbd_det.pbd_add_date, " _
                            & "  public.pbd_det.pbd_upd_by, " _
                            & "  public.pbd_det.pbd_upd_date, " _
                            & "  public.pbd_det.pbd_pb_oid, " _
                            & "  public.pbd_det.pbd_seq, " _
                            & "  public.pbd_det.pbd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pbd_det.pbd_rmks, " _
                            & "  public.pbd_det.pbd_end_user, " _
                            & "  public.pbd_det.pbd_due_date, " _
                            & "  public.pbd_det.pbd_qty, " _
                            & "  public.pbd_det.pbd_qty_processed, " _
                            & "  public.pbd_det.pbd_qty_completed, " _
                            & "  public.pbd_det.pbd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pbd_det.pbd_status, " _
                            & "  public.pbd_det.pbd_dt " _
                            & "FROM " _
                            & "  public.pb_mstr " _
                            & " inner join pbd_det on pbd_pb_oid = pb_oid " _
                            & " inner join en_mstr on en_id = pb_en_id  " _
                            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                            & " inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                            & " where pb_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        .SQL = "SELECT  " _
                            & "  public.pbd_det.pbd_oid, " _
                            & "  public.pbd_det.pbd_dom_id, " _
                            & "  public.pbd_det.pbd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pbd_det.pbd_add_by, " _
                            & "  public.pbd_det.pbd_add_date, " _
                            & "  public.pbd_det.pbd_upd_by, " _
                            & "  public.pbd_det.pbd_upd_date, " _
                            & "  public.pbd_det.pbd_pb_oid, " _
                            & "  public.pbd_det.pbd_seq, " _
                            & "  public.pbd_det.pbd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pbd_det.pbd_rmks, " _
                            & "  public.pbd_det.pbd_end_user, " _
                            & "  public.pbd_det.pbd_due_date, " _
                            & "  public.pbd_det.pbd_qty, " _
                            & "  public.pbd_det.pbd_qty_processed, " _
                            & "  public.pbd_det.pbd_qty_completed, " _
                            & "  public.pbd_det.pbd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pbd_det.pbd_status, " _
                            & "  public.pbd_det.pbd_dt " _
                            & "  FROM " _
                            & "  public.pbd_det " _
                            & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.pb_mstr ON (public.pbd_det.pbd_pb_oid = public.pb_mstr.pb_oid) " _
                            & "  inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                            & "  where pb_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and pb_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
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
                              " inner join pb_mstr on pb_code = wf_ref_code " + _
                              " inner join pbd_det dt on dt.pbd_pb_oid = pb_oid " _
                            & " where wf_ref_code in (select pb_code from pb_mstr inner join wf_mstr on wf_ref_code = pb_code " _
                                                  & " where pb_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and pb_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pb_mstr.pb_oid, " _
                            & "  public.pb_mstr.pb_dom_id, " _
                            & "  public.pb_mstr.pb_en_id, " _
                            & "  public.pb_mstr.pb_upd_date, " _
                            & "  public.pb_mstr.pb_upd_by, " _
                            & "  public.pb_mstr.pb_add_date, " _
                            & "  public.pb_mstr.pb_add_by, " _
                            & "  public.pb_mstr.pb_code, " _
                            & "  public.pb_mstr.pb_date, " _
                            & "  public.pb_mstr.pb_due_date, " _
                            & "  public.pb_mstr.pb_requested, " _
                            & "  public.pb_mstr.pb_end_user, " _
                            & "  public.pb_mstr.pb_rmks, " _
                            & "  public.pb_mstr.pb_status, " _
                            & "  public.pb_mstr.pb_close_date, " _
                            & "  public.pb_mstr.pb_dt, " _
                            & "  public.pbd_det.pbd_oid, " _
                            & "  public.pbd_det.pbd_dom_id, " _
                            & "  public.pbd_det.pbd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pbd_det.pbd_add_by, " _
                            & "  public.pbd_det.pbd_add_date, " _
                            & "  public.pbd_det.pbd_upd_by, " _
                            & "  public.pbd_det.pbd_upd_date, " _
                            & "  public.pbd_det.pbd_pb_oid, " _
                            & "  public.pbd_det.pbd_seq, " _
                            & "  public.pbd_det.pbd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pbd_det.pbd_rmks, " _
                            & "  public.pbd_det.pbd_end_user, " _
                            & "  public.pbd_det.pbd_due_date, " _
                            & "  public.pbd_det.pbd_qty, " _
                            & "  public.pbd_det.pbd_qty_processed, " _
                            & "  public.pbd_det.pbd_qty_completed, " _
                            & "  public.pbd_det.pbd_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pbd_det.pbd_status, " _
                            & "  public.pbd_det.pbd_dt " _
                            & "FROM " _
                            & "  public.pb_mstr " _
                            & " inner join pbd_det on pbd_pb_oid = pb_oid " _
                            & " inner join en_mstr on en_id = pb_en_id  " _
                            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                            & " inner join wf_mstr wm on wf_ref_oid = pb_oid " _
                            & " where pb_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and pb_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
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
                gv_detail.Columns("pbd_pb_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pbd_pb_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("pb_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("pb_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("pb_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("pb_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("pbd_pb_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pbd_pb_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("pb_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("pb_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("pb_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("pb_oid").ToString & "'")
                gv_email.BestFitColumns()
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

        _colom = "pb_trans_id"
        _table = "pb_mstr"
        _initial = "pb"
        _type = "ir"
        _title = "Inventory Request"

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
