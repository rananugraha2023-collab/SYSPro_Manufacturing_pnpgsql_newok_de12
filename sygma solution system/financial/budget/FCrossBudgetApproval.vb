Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FCrossBudgetApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FCrossBudgetApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        add_column_copy(gv_os, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Date", "cbdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Year", "cbdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center From", "cc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center To", "cc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account From", "ac_code_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account To", "ac_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Amount", "cbdgt_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_os, "Remarks", "cbdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "cbdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "cbdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "cbdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "cbdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "cbdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Date", "cbdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Year", "cbdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center From", "cc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center To", "cc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account From", "ac_code_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account To", "ac_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Amount", "cbdgt_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_all, "Remarks", "cbdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "cbdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "cbdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "cbdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "cbdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "cbdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_detail, "pbyd_oid", False)
        'add_column(gv_detail, "pbyd_pby_oid", False)
        'add_column_copy(gv_detail, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Description", "pbyd_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Amount", "pbyd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "cbdgt_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Code", "cbdgt_code", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Date", "cbdgt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Year", "cbdgt_year", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Budget Code", "bdgt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost Center From", "cc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost Center To", "cc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account From", "ac_code_from", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Account To", "ac_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Amount", "cbdgt_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column(gv_email, "Remarks", "cbdgt_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Status", "cbdgt_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "User Create", "cbdgt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Date Create", "cbdgt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "User Update", "cbdgt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Date Update", "cbdgt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
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
                                & "  cbdgt_oid, " _
                                & "  cbdgt_dom_id, " _
                                & "  cbdgt_en_id,en_desc, " _
                                & "  cbdgt_add_by, " _
                                & "  cbdgt_add_date, " _
                                & "  cbdgt_upd_by, " _
                                & "  cbdgt_upd_date, " _
                                & "  cbdgt_date, " _
                                & "  cbdgt_year, " _
                                & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
                                & "  cbdgt_sb_from_id,  " _
                                & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
                                & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
                                & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
                                & "  cbdgt_sb_to_id, " _
                                & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
                                & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
                                & "  cbdgt_remarks, " _
                                & "  cbdgt_dt,cbdgt_code, " _
                                & "  cbdgt_bdgt_oid,bdgt_code, " _
                                & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id,wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM  " _
                                & "  public.cbdgt_mstr " _
                                & "  inner join en_mstr on en_id = cbdgt_en_id " _
                                & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
                                & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
                                & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
                                & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
                                & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
                                & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
                                & "  inner join wf_mstr wm on wf_ref_oid = cbdgt_oid " _
                                & "  inner join tconfuser on usernama = cbdgt_add_by " _
                                & "  inner join bdgt_mstr on bdgt_oid = cbdgt_bdgt_oid " _
                                & " where cbdgt_en_id in (select user_en_id from tconfuserentity " _
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
                                & "  cbdgt_oid, " _
                                & "  cbdgt_dom_id, " _
                                & "  cbdgt_en_id,en_desc, " _
                                & "  cbdgt_add_by, " _
                                & "  cbdgt_add_date, " _
                                & "  cbdgt_upd_by, " _
                                & "  cbdgt_upd_date, " _
                                & "  cbdgt_date, " _
                                & "  cbdgt_year, " _
                                & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
                                & "  cbdgt_sb_from_id,  " _
                                & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
                                & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
                                & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
                                & "  cbdgt_sb_to_id, " _
                                & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
                                & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
                                & "  cbdgt_remarks, " _
                                & "  cbdgt_dt,cbdgt_code, " _
                                & "  cbdgt_bdgt_oid,bdgt_code, " _
                                & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id " _
                                & "FROM  " _
                                & "  public.cbdgt_mstr " _
                                & "  inner join en_mstr on en_id = cbdgt_en_id " _
                                & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
                                & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
                                & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
                                & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
                                & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
                                & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
                                & "  inner join wf_mstr wm on wf_ref_oid = cbdgt_oid " _
                                & "  inner join bdgt_mstr on bdgt_oid = cbdgt_bdgt_oid " _
                                & " where cbdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and cbdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and cbdgt_trans_id <> 'D' "
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
                        '.SQL = "SELECT  " _
                        '& "  pbyd_oid, " _
                        '& "  pbyd_pby_oid, " _
                        '& "  pbyd_seq, " _
                        '& "  pbyd_ac_id, ac_code, " _
                        '& "  pbyd_sb_id,sb_desc, " _
                        '& "  pbyd_cc_id,cc_desc, " _
                        '& "  pbyd_desc, " _
                        '& "  pbyd_amount, " _
                        '& "  pbyd_dt " _
                        '& "FROM  " _
                        '& "  public.pbyd_det " _
                        '& "  inner join pby_mstr on pby_oid = pbyd_pby_oid " _
                        '& "  inner join cc_mstr on cc_id = pbyd_cc_id " _
                        '& "  inner join sb_mstr on sb_id = pbyd_sb_id " _
                        '& "  inner join ac_mstr on ac_id = pbyd_ac_id " _
                        '& "  inner join wf_mstr wm on wf_ref_oid = pby_oid " _
                        '& "  where pby_en_id in (select user_en_id from tconfuserentity " _
                        '                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        '& "  and wf_user_id in (" + _user_approval + ")" _
                        '& "  and wf_iscurrent ~~* 'Y'"
                        '.InitializeCommand()
                        '.FillDataSet(ds_detail, "detail")
                        'gc_detail.DataSource = ds_detail.Tables("detail")
                        'gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join cbdgt_mstr on cbdgt_code = wf_ref_code " + _
                              " where wf_ref_code in (select cbdgt_code from cbdgt_mstr inner join wf_mstr on wf_ref_code = cbdgt_code " _
                                                  & " where cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                                & "  cbdgt_oid, " _
                                & "  cbdgt_dom_id, " _
                                & "  cbdgt_en_id,en_desc, " _
                                & "  cbdgt_add_by, " _
                                & "  cbdgt_add_date, " _
                                & "  cbdgt_upd_by, " _
                                & "  cbdgt_upd_date, " _
                                & "  cbdgt_date, " _
                                & "  cbdgt_year, " _
                                & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
                                & "  cbdgt_sb_from_id,  " _
                                & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
                                & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
                                & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
                                & "  cbdgt_sb_to_id, " _
                                & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
                                & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
                                & "  cbdgt_remarks, " _
                                & "  cbdgt_dt,cbdgt_code, " _
                                & "  cbdgt_bdgt_oid,bdgt_code, " _
                                & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id " _
                                & "FROM  " _
                                & "  public.cbdgt_mstr " _
                                & "  inner join en_mstr on en_id = cbdgt_en_id " _
                                & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
                                & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
                                & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
                                & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
                                & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
                                & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
                                & "  inner join wf_mstr wm on wf_ref_oid = cbdgt_oid " _
                                & "  inner join bdgt_mstr on bdgt_oid = cbdgt_bdgt_oid " _
                            & " where cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        '.SQL = "SELECT  " _
                        '    & "  pbyd_oid, " _
                        '    & "  pbyd_pby_oid, " _
                        '    & "  pbyd_seq, " _
                        '    & "  pbyd_ac_id, ac_code, " _
                        '    & "  pbyd_sb_id,sb_desc, " _
                        '    & "  pbyd_cc_id,cc_desc, " _
                        '    & "  pbyd_desc, " _
                        '    & "  pbyd_amount, " _
                        '    & "  pbyd_dt " _
                        '    & "FROM  " _
                        '    & "  public.pbyd_det " _
                        '    & "  inner join pby_mstr on pby_oid = pbyd_pby_oid " _
                        '    & "  inner join cc_mstr on cc_id = pbyd_cc_id " _
                        '    & "  inner join sb_mstr on sb_id = pbyd_sb_id " _
                        '    & "  inner join ac_mstr on ac_id = pbyd_ac_id " _
                        '    & "  inner join wf_mstr wm on wf_ref_oid = pby_oid " _
                        '    & "  where pby_en_id in (select user_en_id from tconfuserentity " _
                        '                         & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        '    & "  and pby_date >= " + SetDate(pr_txttglawal.DateTime) _
                        '    & "  and pby_date <= " + SetDate(pr_txttglakhir.DateTime) _
                        '    & "  and wf_user_id in (" + _user_approval + ")"
                        '.InitializeCommand()
                        '.FillDataSet(ds_detail, "detail")
                        'gc_detail.DataSource = ds_detail.Tables("detail")
                        'gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join cbdgt_mstr on cbdgt_code = wf_ref_code " _
                            & " where wf_ref_code in (select cbdgt_code from cbdgt_mstr inner join wf_mstr on wf_ref_code = cbdgt_code " _
                                                  & " where cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and cbdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and cbdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                                & "  cbdgt_oid, " _
                                & "  cbdgt_dom_id, " _
                                & "  cbdgt_en_id,en_desc, " _
                                & "  cbdgt_add_by, " _
                                & "  cbdgt_add_date, " _
                                & "  cbdgt_upd_by, " _
                                & "  cbdgt_upd_date, " _
                                & "  cbdgt_date, " _
                                & "  cbdgt_year, " _
                                & "  cbdgt_ac_from_id,acf.ac_code as ac_code_from,acf.ac_name as ac_name_from, " _
                                & "  cbdgt_sb_from_id,  " _
                                & "  cbdgt_cc_from_id,ccf.cc_desc as cc_desc_from, " _
                                & "  cbdgt_periode_from,pfrom.bdgtp_code as periode_from, " _
                                & "  cbdgt_ac_to_id,acto.ac_code as ac_code_to,acto.ac_name as ac_name_to, " _
                                & "  cbdgt_sb_to_id, " _
                                & "  cbdgt_cc_to_id,ccto.cc_desc as cc_desc_to, " _
                                & "  cbdgt_periode_to,pto.bdgtp_code as periode_to, " _
                                & "  cbdgt_remarks, " _
                                & "  cbdgt_dt,cbdgt_code, " _
                                & "  cbdgt_bdgt_oid,bdgt_code, " _
                                & "  cbdgt_amount,cbdgt_trans_id,cbdgt_tran_id " _
                                & "FROM  " _
                                & "  public.cbdgt_mstr " _
                                & "  inner join en_mstr on en_id = cbdgt_en_id " _
                                & "  inner join ac_mstr acf on acf.ac_id = cbdgt_ac_from_id " _
                                & "  inner join ac_mstr acto on acto.ac_id = cbdgt_ac_to_id " _
                                & "  inner join cc_mstr ccf on ccf.cc_id = cbdgt_cc_from_id " _
                                & "  inner join cc_mstr ccto on ccto.cc_id = cbdgt_cc_to_id " _
                                & "  inner join bdgtp_periode pfrom on pfrom.bdgtp_id = cbdgt_periode_from " _
                                & "  inner join bdgtp_periode pto on pto.bdgtp_id = cbdgt_periode_to " _
                                & "  inner join wf_mstr wm on wf_ref_oid = cbdgt_oid " _
                                & "  inner join bdgt_mstr on bdgt_oid = cbdgt_bdgt_oid " _
                            & " where cbdgt_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and cbdgt_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and cbdgt_date <= " + SetDate(pr_txttglakhir.DateTime) _
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
                'gv_detail.Columns("pbyd_pby_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("pby_oid"))
                'gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("cbdgt_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("cbdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cbdgt_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("cbdgt_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                'gv_detail.Columns("pbyd_pby_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("pby_oid"))
                'gv_detail.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("cbdgt_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("cbdgt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cbdgt_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("cbdgt_oid").ToString & "'")
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

        _colom = "cbdgt_trans_id"
        _table = "cbdgt_mstr"
        _initial = "cbdgt"
        _type = "cb"
        _title = "CrossBudget"

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

    Public Overrides Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
                                   ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
                                   ByVal par_ds As DataSet, ByVal par_gv As Object, ByVal par_title As String)
        If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim wf_seq, i, _max_seq As Integer
        Dim filename, user_wf, user_wf_email, format_email_bantu As String

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text

                        par_ds.Tables("os").AcceptChanges()
                        For i = 0 To par_ds.Tables("os").Rows.Count - 1
                            If par_ds.Tables("os").Rows(i).Item("status") = True Then

                                Try
                                    par_gv.Columns("cbdgt_oid").FilterInfo = _
                                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cbdgt_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cbdgt_oid").ToString & "'")
                                    par_gv.BestFitColumns()

                                    'par_gv.Columns(par_initial + "_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(i).Item(par_initial + "_oid"))
                                Catch ex As Exception
                                End Try

                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                           " wf_aprv_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                           " wf_desc = '" + Trim(par_desc) + "', " + _
                                           " wf_iscurrent = 'N'," + _
                                           " wf_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " + _
                                           " wf_date_to = null " + _
                                           " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                           " and wf_user_id in (" + par_user_approval + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    'Cek
                                    Dim ds_cek As New DataSet
                                    Try
                                        Using objcb As New master_new.WDABasepgsql("", "")
                                            With objcb
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
                                                    & " where bdgtd_bdgt_oid = " + SetSetring(ds.Tables("os").Rows(i).Item("cbdgt_bdgt_oid").ToString()) _
                                                    & "  and bdgtd_bdgtp_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_periode_from")) _
                                                    & "  and bdgtd_ac_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_ac_from_id"))
                                                .InitializeCommand()
                                                .FillDataSet(ds_cek, "cek")
                                            End With
                                        End Using
                                    Catch ex As Exception
                                        MessageBox.Show(ex.Message)
                                    End Try

                                    Dim _budget_from As Double = 0
                                    For Each _dr As DataRow In ds_cek.Tables(0).Rows
                                        _budget_from = _budget_from + (_dr("bdgtd_budget") - (_dr("bdgtd_alokasi") + _dr("bdgtd_realisasi")))
                                    Next

                                    'Update cross Budget
                                    Dim _ammount As Double = 0
                                    _ammount = ds.Tables("os").Rows(i).Item("cbdgt_amount")

                                    If _budget_from < _ammount Then
                                        MessageBox.Show("Budget Lebih Kecil Dari pada Nominal Cross Budget,,!", "Err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
                                                   " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Pengurangan (From)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE bdgtd_det  " _
                                                        & "  set bdgtd_budget = bdgtd_budget - " + SetDbl(_ammount) _
                                                        & "  where bdgtd_bdgt_oid = " + SetSetring(ds.Tables("os").Rows(i).Item("cbdgt_bdgt_oid").ToString()) _
                                                        & "  and bdgtd_bdgtp_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_periode_from")) _
                                                        & "  and bdgtd_ac_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_ac_from_id"))
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Penambahan (To)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE bdgtd_det  " _
                                                        & "  set bdgtd_budget = bdgtd_budget + " + SetDbl(_ammount) _
                                                        & "  where bdgtd_bdgt_oid = " + SetSetring(ds.Tables("os").Rows(i).Item("cbdgt_bdgt_oid").ToString()) _
                                                        & "  and bdgtd_bdgtp_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_periode_to")) _
                                                        & "  and bdgtd_ac_id = " + SetInteger(ds.Tables("os").Rows(i).Item("cbdgt_ac_to_id"))
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If

                                wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") + 1
                                user_wf = mf.get_user_wf(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), wf_seq)
                                user_wf_email = mf.get_email_address(user_wf)

                                If user_wf_email <> "" Then
                                    filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + ".xls"
                                    ExportTo(par_gv, New ExportXlsProvider(filename))

                                    format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), par_type)
                                    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                End If
                            End If
                        Next

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        load_data_many(True)
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
