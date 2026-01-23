Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FBudgetApprovalBackup
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FBudgetApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_os, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Date", "bdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Year", "bdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Remarks", "bdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "bdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "bdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "bdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "bdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "bdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Date", "bdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Year", "bdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Remarks", "bdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "bdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "bdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "bdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "bdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "bdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_detail, "bdgtd_oid", False)
        'add_column(gv_detail, "bdgtd_bdgt_oid", False)
        'add_column_copy(gv_detail, "Month", "bdgtd_month", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Alokasi", "bdgtd_alokasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Realisasi", "bdgtd_realisasi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf, "wf_ref_oid", False)
        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "bdgtd_oid", False)
        add_column(gv_email, "bdgtd_bdgt_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Year", "bdgt_year", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Periode", "bdgtp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Budget", "bdgtd_budget", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Remarks", "bdgt_remarks", DevExpress.Utils.HorzAlignment.Default)

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
                                & "  bdgt_oid, " _
                                & "  bdgt_dom_id, " _
                                & "  bdgt_en_id,en_desc, " _
                                & "  bdgt_add_by, " _
                                & "  bdgt_add_date, " _
                                & "  bdgt_upd_by, " _
                                & "  bdgt_upd_date, " _
                                & "  bdgt_date, " _
                                & "  bdgt_year, " _
                                & "  bdgt_remarks, " _
                                & "  bdgt_trans_id, " _
                                & "  bdgt_dt, " _
                                & "  bdgt_tran_id, wf_seq, coalesce(useremail,'') as useremail, " _
                                & "  bdgt_cc_id, " _
                                & "  bdgt_code, " _
                                & "  bdgt_rev, " _
                                & "  bdgt_active " _
                                & "FROM  " _
                                & "  public.bdgt_mstr " _
                                & "  inner join en_mstr on en_id = bdgt_en_id " _
                                & " inner join wf_mstr wm on wf_ref_oid = bdgt_oid " _
                                & " inner join tconfuser on usernama = bdgt_add_by " _
                                & " where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and wf_iscurrent ~~* 'Y'" _
                                & " and bdgt_active = 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  false as status, " _
                                & "  bdgt_oid, " _
                                & "  bdgt_dom_id, " _
                                & "  bdgt_en_id,en_desc, " _
                                & "  bdgt_add_by, " _
                                & "  bdgt_add_date, " _
                                & "  bdgt_upd_by, " _
                                & "  bdgt_upd_date, " _
                                & "  bdgt_date, " _
                                & "  bdgt_year, " _
                                & "  bdgt_remarks, " _
                                & "  bdgt_trans_id, " _
                                & "  bdgt_dt, " _
                                & "  bdgt_tran_id, wf_seq, coalesce(useremail,'') as useremail, " _
                                & "  bdgt_cc_id, " _
                                & "  bdgt_code, " _
                                & "  bdgt_rev, " _
                                & "  bdgt_active " _
                                & "FROM  " _
                                & "  public.bdgt_mstr " _
                                & "  inner join en_mstr on en_id = bdgt_en_id " _
                                & " inner join wf_mstr wm on wf_ref_oid = bdgt_oid " _
                                & " inner join tconfuser on usernama = bdgt_add_by " _
                                & " where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and bdgt_trans_id <> 'D' " _
                                & " and bdgt_active = 'Y'"
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

    Public Sub LoadDetail()
        Dim _ds_detail As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    If xtc_master.SelectedTabPageIndex = 0 Then
                        .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                            & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and wf_user_id in (" + _user_approval + ")" _
                            & "  and wf_iscurrent ~~* 'Y' " _
                            & "  and bdgtd_bdgt_oid = " + SetSetring(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid")).ToString() _
                            & " order by bdgtd_bdgtp_id asc "
                        .InitializeCommand()
                        .FillDataSet(_ds_detail, "detail")
                        pgc_detail.DataSource = _ds_detail
                        pgc_detail.DataMember = "detail"
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                            & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and bdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")" _
                            & "  and bdgtd_bdgt_oid = " + SetSetring(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid")).ToString() _
                            & " order by bdgtd_bdgtp_id asc "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        pgc_detail.DataSource = ds_detail
                        pgc_detail.DataMember = "detail"
                    End If

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

        LoadDetail()

        ds_detail = New DataSet
        Try
            Using objload As New master_new.WDABasepgsql("", "")
                With objload

                    If xtc_master.SelectedTabPageIndex = 0 Then
                        '.SQL = "SELECT  " _
                        '    & "  bdgtd_oid, " _
                        '    & "  bdgtd_bdgt_oid, " _
                        '    & "  bdgtd_add_by, " _
                        '    & "  bdgtd_add_date, " _
                        '    & "  bdgtd_upd_by, " _
                        '    & "  bdgtd_upd_date, " _
                        '    & "  bdgtd_month, " _
                        '    & "  bdgtd_ac_id,ac_code,ac_name, " _
                        '    & "  bdgtd_sb_id, " _
                        '    & "  bdgtd_budget, " _
                        '    & "  bdgtd_alokasi, " _
                        '    & "  bdgtd_realisasi, " _
                        '    & "  bdgtd_dt " _
                        '    & "FROM  " _
                        '    & "  public.bdgtd_det " _
                        '    & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                        '    & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                        '    & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                        '    & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                        '                         & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        '    & "  and wf_user_id in (" + _user_approval + ")" _
                        '    & "  and wf_iscurrent ~~* 'Y'"
                        '.InitializeCommand()
                        '.FillDataSet(ds_detail, "detail")
                        ''gc_detail.DataSource = ds_detail.Tables("detail")
                        ''gv_detail.BestFitColumns()
                        'pgc_detail.DataSource = ds_detail
                        'pgc_detail.DataMember = "detail"

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join bdgt_mstr on bdgt_oid = wf_ref_oid " + _
                              " inner join bdgtd_det dt on dt.bdgtd_bdgt_oid = bdgt_oid " _
                            & " where wf_ref_oid in (select bdgt_oid from bdgt_mstr inner join wf_mstr on wf_ref_oid = bdgt_oid " _
                                                  & " where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid,bdgt_year,bdgt_code,cc_desc,bdgt_remarks,en_desc, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                            & "  inner join en_mstr on en_id = bdgt_en_id " _
                            & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                            & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and wf_user_id in (" + _user_approval + ")" _
                            & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        '.SQL = "SELECT  " _
                        '    & "  bdgtd_oid, " _
                        '    & "  bdgtd_bdgt_oid, " _
                        '    & "  bdgtd_add_by, " _
                        '    & "  bdgtd_add_date, " _
                        '    & "  bdgtd_upd_by, " _
                        '    & "  bdgtd_upd_date, " _
                        '    & "  bdgtd_month, " _
                        '    & "  bdgtd_ac_id,ac_code,ac_name, " _
                        '    & "  bdgtd_sb_id, " _
                        '    & "  bdgtd_budget, " _
                        '    & "  bdgtd_alokasi, " _
                        '    & "  bdgtd_realisasi, " _
                        '    & "  bdgtd_dt " _
                        '    & "FROM  " _
                        '    & "  public.bdgtd_det " _
                        '    & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                        '    & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                        '    & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                        '    & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                        '                         & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        '    & "  and bdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                        '    & "  and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        '    & "  and wf_user_id in (" + _user_approval + ")"
                        '.InitializeCommand()
                        '.FillDataSet(ds_detail, "detail")
                        ''gc_detail.DataSource = ds_detail.Tables("detail")
                        ''gv_detail.BestFitColumns()
                        'pgc_detail.DataSource = ds_detail
                        'pgc_detail.DataMember = "detail"

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join bdgt_mstr on bdgt_oid = wf_ref_oid " + _
                              " inner join bdgtd_det dt on dt.bdgtd_bdgt_oid = bdgt_oid " _
                            & " where wf_ref_oid in (select bdgt_oid from bdgt_mstr inner join wf_mstr on wf_ref_oid = bdgt_oid " _
                                                  & " where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and bdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  bdgtd_oid, " _
                            & "  bdgtd_bdgt_oid,bdgt_year,bdgt_code,cc_desc,bdgt_remarks,en_desc, " _
                            & "  bdgtd_add_by, " _
                            & "  bdgtd_add_date, " _
                            & "  bdgtd_upd_by, " _
                            & "  bdgtd_upd_date, " _
                            & "  bdgtd_bdgtp_id, " _
                            & "  bdgtp_code,bdgtp_remarks,bdgtp_start_date,bdgtp_end_date, " _
                            & "  bdgtd_ac_id,ac_code,ac_name, " _
                            & "  bdgtd_sb_id, " _
                            & "  bdgtd_budget, " _
                            & "  bdgtd_alokasi, " _
                            & "  bdgtd_realisasi, " _
                            & "  bdgtd_dt " _
                            & "FROM  " _
                            & "  public.bdgtd_det " _
                            & "  inner join bdgt_mstr on bdgt_oid = bdgtd_bdgt_oid " _
                            & "  inner join bdgtp_periode on bdgtp_id = bdgtd_bdgtp_id " _
                            & "  inner join ac_mstr on ac_id = bdgtd_ac_id " _
                            & "  inner join cc_mstr on cc_id = bdgt_cc_id " _
                            & "  inner join en_mstr on en_id = bdgt_en_id " _
                            & "  inner join wf_mstr wm on wf_ref_oid = bdgtd_bdgt_oid " _
                            & "  where bdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and bdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and bdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"
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
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("bdgtd_bdgt_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("bdgt_oid"))
                'gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("bdgtd_bdgt_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_detail.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid"))
                'gv_detail.BestFitColumns()

                'gv_wf.Columns("wf_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("bdgtd_bdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("bdgt_oid"))
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

        _colom = "bdgt_trans_id"
        _table = "bdgt_mstr"
        _initial = "bdgt"
        _type = "bg"
        _title = "Budget"

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

    'Public Function UpdateAsset() As Boolean
    '    'UpdateAsset = True

    '    'Dim _oid_master As String
    '    '_oid_master = ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("asrtr_oid")
    '    'Dim _max_seq As Integer
    '    '_max_seq = mf.get_max_seq(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("asrtr_code").ToString())

    '    'Try
    '    '    Using objinsert As New master_new.WDABasepgsql("", "")
    '    '        With objinsert
    '    '            .Connection.Open()
    '    '            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '    '            Try
    '    '                .Command = .Connection.CreateCommand
    '    '                .Command.Transaction = sqlTran

    '    '                For Each _dr As DataRow In ds_detail.Tables("detail").Rows
    '    '                    If _dr("asrtrd_asrtr_oid") = _oid_master Then
    '    '                        If _max_seq = _dr("wf_seq") Then
    '    '                            .Command.CommandType = CommandType.Text
    '    '                            .Command.CommandText = "UPDATE ass_mstr  " _
    '    '                                                & "  set ass_qty_del = 1, " _
    '    '                                                & "  ass_confirm = 'N', " _
    '    '                                                & "  ass_its_id =  2, " _
    '    '                                                & "  ass_emp_id = 0, " _
    '    '                                                & "  ass_emp_dept = 0, " _
    '    '                                                & "  ass_emp_rg = 0, " _
    '    '                                                & "  ass_disp_amount = " + SetDbl(_dr("asrtrd_dispose_cost")) + "," _
    '    '                                                & "  ass_disp_date = " + SetDate(_dr("asrtrd_dispose_date")) _
    '    '                                                & "  where ass_id = " + SetInteger(_dr("asrtrd_ass_id"))
    '    '                            .Command.ExecuteNonQuery()
    '    '                            .Command.Parameters.Clear()
    '    '                        End If
    '    '                    End If
    '    '                Next

    '    '                sqlTran.Commit()
    '    '                load_data_many(True)
    '    '            Catch ex As nPgSqlException
    '    '                sqlTran.Rollback()
    '    '                MessageBox.Show(ex.Message)
    '    '            End Try
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    UpdateAsset = False
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    'End Function

    'Public Overrides Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
    '                               ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
    '                               ByVal par_ds As DataSet, ByVal par_gv As Object)
    '    If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    Dim wf_seq, i, _max_seq As Integer
    '    Dim filename, user_wf, user_wf_email, format_email_bantu As String

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran
    '                    .Command.CommandType = CommandType.Text

    '                    par_ds.Tables("os").AcceptChanges()
    '                    For i = 0 To par_ds.Tables("os").Rows.Count - 1
    '                        If par_ds.Tables("os").Rows(i).Item("status") = True Then

    '                            Try
    '                                par_gv.Columns(par_initial + "_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(i).Item(par_initial + "_oid"))
    '                            Catch ex As Exception
    '                            End Try

    '                            .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
    '                                       " wf_aprv_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " + _
    '                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
    '                                       " wf_desc = '" + Trim(par_desc) + "', " + _
    '                                       " wf_iscurrent = 'N'," + _
    '                                       " wf_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " + _
    '                                       " wf_date_to = null " + _
    '                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
    '                                       " and wf_user_id in (" + par_user_approval + ")" + _
    '                                       " and wf_iscurrent ~~* 'Y'"

    '                            .Command.ExecuteNonQuery()
    '                            .Command.Parameters.Clear()

    '                            .Command.CommandType = CommandType.Text
    '                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                                   " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
    '                                                   " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString

    '                            .Command.ExecuteNonQuery()
    '                            .Command.Parameters.Clear()

    '                            _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

    '                            If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
    '                                               " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid") + "'"
    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()
    '                            End If

    '                            wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") + 1
    '                            user_wf = mf.get_user_wf(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), wf_seq)
    '                            user_wf_email = mf.get_email_address(user_wf)

    '                            If user_wf_email <> "" Then
    '                                filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + ".xls"
    '                                ExportTo(par_gv, New ExportXlsProvider(filename))

    '                                format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), par_type)
    '                                mf.sent_email(user_wf_email, "", mf.title_email(par_type, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                            End If
    '                        End If

    '                    Next

    '                    If UpdateAsset() = False Then
    '                        sqlTran.Rollback()
    '                        Exit Sub
    '                    End If

    '                    sqlTran.Commit()
    '                    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '                    load_data_many(True)
    '                Catch ex As nPgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub gv_os_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_os.FocusedRowChanged
        Try
            LoadDetail()
        Catch ex As Exception
        End Try
    End Sub
End Class
