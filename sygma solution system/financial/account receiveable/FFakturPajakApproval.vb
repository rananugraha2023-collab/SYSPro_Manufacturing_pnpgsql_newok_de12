Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FFakturPajakApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet
    Public Shared PageNumAproval As String

    Private Sub FFakturPajakApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        ce_page_number.checked=True
        ce_page_number.checked=False

       
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
        add_column_copy(gv_os, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "fp_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Faktur Pajak Replacement", "fp_code_pengganti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status Approval", "fp_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Revisi", "fp_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Unstrikeout", "fp_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "fp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "fp_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "User Update", "fp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "fp_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "fp_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Faktur Pajak Replacement", "fp_code_pengganti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status Approval", "fp_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Revisi", "fp_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Unstrikeout", "fp_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "fp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "fp_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "User Update", "fp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "fp_upd_date", DevExpress.Utils.HorzAlignment.Center)

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

        add_column(gv_email, "fp_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Faktur Pajak Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Faktur Pajak Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Faktur Pajak Sign", "fp_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status", "fp_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Status", "fp_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Revisi", "fp_rev", DevExpress.Utils.HorzAlignment.Default)

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
                                & "  fp_mstr.fp_oid, " _
                                & "  fp_mstr.fp_dom_id, " _
                                & "  fp_mstr.fp_en_id, " _
                                & "  fp_mstr.fp_add_by, " _
                                & "  fp_mstr.fp_add_date, " _
                                & "  fp_mstr.fp_upd_by, " _
                                & "  fp_mstr.fp_upd_date, " _
                                & "  fp_mstr.fp_code, " _
                                & "  fp_mstr.fp_pengali_tax, " _
                                & "  fp_mstr.fp_dt, " _
                                & "  fp_mstr.fp_date, " _
                                & "  fp_mstr.fp_sign, " _
                                & "  fp_mstr.fp_status, " _
                                & "  CASE WHEN fp_mstr.fp_status = '0' " _
                                & "      THEN 'Normal' " _
                                & "      ELSE 'Penggantian' " _
                                & "  END AS fp_status_desc , " _
                                & "  fp_mstr.fp_customer_type, " _
                                & "  fp_mstr.fp_area, " _
                                & "  fp_mstr.fp_ppn_type, " _
                                & "  fp_mstr.fp_tax_inc, " _
                                & "  fp_mstr.fp_ptnr_id, " _
                                & "  fp_mstr.fp_tran_id, " _
                                & "  fp_mstr.fp_trans_id, " _
                                & "  fp_mstr.fp_ar_oid, " _
                                & "  fp_mstr.fp_rev, " _
                                & "  fp_mstr.fp_unstrikeout, " _
                                & "  fp_mstr.fp_fp_oid, " _
                                & "  fm.fp_code as fp_code_pengganti, " _
                                & "  en_desc, " _
                                & "  ar_code, " _
                                & "  fp_mstr.fp_ptnr_addr_oid, " _
                                & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                                & "  ptnr_name, " _
                                & "  ptnr_npwp, " _
                                & "  ptnr_nppkp,wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM  " _
                                & "  public.fp_mstr " _
                                & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                                & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                                & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                                & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                                & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                                & " inner join wf_mstr wm on wf_ref_oid = fp_mstr.fp_oid " _
                                & " inner join tconfuser on usernama = fp_mstr.fp_add_by " _
                                & " where fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and fp_mstr.fp_trans_id = 'W' " _
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
                                & "  fp_mstr.fp_oid, " _
                                & "  fp_mstr.fp_dom_id, " _
                                & "  fp_mstr.fp_en_id, " _
                                & "  fp_mstr.fp_add_by, " _
                                & "  fp_mstr.fp_add_date, " _
                                & "  fp_mstr.fp_upd_by, " _
                                & "  fp_mstr.fp_upd_date, " _
                                & "  fp_mstr.fp_code, " _
                                & "  fp_mstr.fp_pengali_tax, " _
                                & "  fp_mstr.fp_dt, " _
                                & "  fp_mstr.fp_date, " _
                                & "  fp_mstr.fp_sign, " _
                                & "  fp_mstr.fp_status, " _
                                & "  CASE WHEN fp_mstr.fp_status = '0' " _
                                & "      THEN 'Normal' " _
                                & "      ELSE 'Penggantian' " _
                                & "  END AS fp_status_desc , " _
                                & "  fp_mstr.fp_customer_type, " _
                                & "  fp_mstr.fp_area, " _
                                & "  fp_mstr.fp_ppn_type, " _
                                & "  fp_mstr.fp_tax_inc, " _
                                & "  fp_mstr.fp_ptnr_id, " _
                                & "  fp_mstr.fp_tran_id, " _
                                & "  fp_mstr.fp_trans_id, " _
                                & "  fp_mstr.fp_ar_oid, " _
                                & "  fp_mstr.fp_rev, " _
                                & "  fp_mstr.fp_unstrikeout, " _
                                & "  fp_mstr.fp_fp_oid, " _
                                & "  fm.fp_code as fp_code_pengganti, " _
                                & "  en_desc, " _
                                & "  ar_code, " _
                                & "  fp_mstr.fp_ptnr_addr_oid, " _
                                & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                                & "  ptnr_name, " _
                                & "  ptnr_npwp, " _
                                & "  ptnr_nppkp " _
                                & "FROM  " _
                                & "  public.fp_mstr " _
                                & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                                & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                                & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                                & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                                & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                                & " inner join wf_mstr wm on wf_ref_oid = fp_mstr.fp_oid " _
                                & " where fp_mstr.fp_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and fp_mstr.fp_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")"
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

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join fp_mstr on fp_oid = wf_ref_oid " + _
                              " where wf_ref_oid in (select fp_oid from fp_mstr inner join wf_mstr on wf_ref_oid = fp_oid " _
                                                  & " where fp_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  fp_mstr.fp_oid, " _
                            & "  fp_mstr.fp_dom_id, " _
                            & "  fp_mstr.fp_en_id, " _
                            & "  fp_mstr.fp_add_by, " _
                            & "  fp_mstr.fp_add_date, " _
                            & "  fp_mstr.fp_upd_by, " _
                            & "  fp_mstr.fp_upd_date, " _
                            & "  fp_mstr.fp_code, " _
                            & "  fp_mstr.fp_pengali_tax, " _
                            & "  fp_mstr.fp_dt, " _
                            & "  fp_mstr.fp_date, " _
                            & "  fp_mstr.fp_sign, " _
                            & "  fp_mstr.fp_status, " _
                            & "  CASE WHEN fp_mstr.fp_status = '0' " _
                            & "      THEN 'Normal' " _
                            & "      ELSE 'Penggantian' " _
                            & "  END AS fp_status_desc , " _
                            & "  fp_mstr.fp_customer_type, " _
                            & "  fp_mstr.fp_area, " _
                            & "  fp_mstr.fp_ppn_type, " _
                            & "  fp_mstr.fp_tax_inc, " _
                            & "  fp_mstr.fp_ptnr_id, " _
                            & "  fp_mstr.fp_tran_id, " _
                            & "  fp_mstr.fp_trans_id, " _
                            & "  fp_mstr.fp_ar_oid, " _
                            & "  fp_mstr.fp_rev, " _
                            & "  fp_mstr.fp_unstrikeout, " _
                            & "  fp_mstr.fp_fp_oid, " _
                            & "  fm.fp_code as fp_code_pengganti, " _
                            & "  wf_ref_oid, " _
                            & "  en_desc, " _
                            & "  ar_code, " _
                            & "  fp_mstr.fp_ptnr_addr_oid, " _
                            & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                            & "  ptnr_name, " _
                            & "  ptnr_npwp, " _
                            & "  ptnr_nppkp " _
                            & "FROM  " _
                            & "  public.fp_mstr " _
                            & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                            & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                            & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                            & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                            & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = fp_mstr.fp_oid " _
                            & " where fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then


                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join fp_mstr on fp_oid = wf_ref_oid " + _
                              " where wf_ref_oid in (select fp_oid from fp_mstr inner join wf_mstr on wf_ref_oid = fp_oid " _
                                                  & " where fp_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and fp_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and fp_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  fp_mstr.fp_oid, " _
                            & "  fp_mstr.fp_dom_id, " _
                            & "  fp_mstr.fp_en_id, " _
                            & "  fp_mstr.fp_add_by, " _
                            & "  fp_mstr.fp_add_date, " _
                            & "  fp_mstr.fp_upd_by, " _
                            & "  fp_mstr.fp_upd_date, " _
                            & "  fp_mstr.fp_code, " _
                            & "  fp_mstr.fp_pengali_tax, " _
                            & "  fp_mstr.fp_dt, " _
                            & "  fp_mstr.fp_date, " _
                            & "  fp_mstr.fp_sign, " _
                            & "  fp_mstr.fp_status, " _
                            & "  CASE WHEN fp_mstr.fp_status = '0' " _
                            & "      THEN 'Normal' " _
                            & "      ELSE 'Penggantian' " _
                            & "  END AS fp_status_desc , " _
                            & "  fp_mstr.fp_customer_type, " _
                            & "  fp_mstr.fp_area, " _
                            & "  fp_mstr.fp_ppn_type, " _
                            & "  fp_mstr.fp_tax_inc, " _
                            & "  fp_mstr.fp_ptnr_id, " _
                            & "  fp_mstr.fp_tran_id, " _
                            & "  fp_mstr.fp_trans_id, " _
                            & "  fp_mstr.fp_ar_oid, " _
                            & "  fp_mstr.fp_rev, " _
                            & "  fp_mstr.fp_unstrikeout, " _
                            & "  fp_mstr.fp_fp_oid, " _
                            & "  fm.fp_code as fp_code_pengganti, " _
                            & "  wf_ref_oid, " _
                            & "  en_desc, " _
                            & "  ar_code, " _
                            & "  fp_mstr.fp_ptnr_addr_oid, " _
                            & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                            & "  ptnr_name, " _
                            & "  ptnr_npwp, " _
                            & "  ptnr_nppkp " _
                            & "FROM  " _
                            & "  public.fp_mstr " _
                            & "  inner join ptnr_mstr on ptnr_id = fp_mstr.fp_ptnr_id " _
                            & "  inner join en_mstr on en_id = fp_mstr.fp_en_id " _
                            & "  inner join ar_mstr on fp_mstr.fp_ar_oid = ar_oid " _
                            & "  inner join ptnra_addr pa on pa.ptnra_oid = fp_mstr.fp_ptnr_addr_oid " _
                            & "  left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = fp_mstr.fp_oid " _
                            & " where fp_mstr.fp_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and fp_mstr.fp_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and fp_mstr.fp_date <= " + SetDate(pr_txttglakhir.DateTime) _
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

                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("fp_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("fp_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[fp_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("fp_oid").ToString & "'")
                gv_email.BestFitColumns()


            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try

                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("fp_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("fp_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[fp_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("fp_oid").ToString & "'")
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

        _colom = "fp_trans_id"
        _table = "fp_mstr"
        _initial = "fp"
        _type = "fp"
        _title = "Faktur Pajak"

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
                                    par_gv.Columns(par_initial + "_oid").FilterInfo = _
                                    New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" + par_initial + "_oid] = '" & ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString & "'")
                                    par_gv.BestFitColumns()
                                Catch ex As Exception
                                End Try

                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                           " wf_aprv_date = current_timestamp, " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "', " + _
                                           " wf_desc = '" + Trim(par_desc) + "', " + _
                                           " wf_iscurrent = 'N'," + _
                                           " wf_dt = current_timestamp, " + _
                                           " wf_date_to = null " + _
                                           " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                           " and wf_user_id in (" + par_user_approval + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                                       " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
                                                   " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
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

    Public Overrides Sub hold_wf(ByVal par_initial As String, ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_hold As Integer, _
                                      ByVal par_user_approval As String, ByVal par_ds As DataSet)
        If MessageBox.Show("Hold Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer

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

                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                           " wf_aprv_date = current_timestamp, " + _
                                           " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                           " wf_desc = '" + Trim(par_desc) + "'," + _
                                           " wf_iscurrent = 'Y'," + _
                                           " wf_dt = current_timestamp, " + _
                                           " wf_date_to = current_date + " + par_hold.ToString + _
                                           " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                           " and wf_user_id in (" + par_user_approval + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Holded..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Overrides Sub cancel_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Cancel Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer

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

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Cancel Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
                        Next

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Canceled..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Overrides Sub rollback_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Rollback Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim i As Integer

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

                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                                       " and wf_user_id in (" + par_user_approval + ")" + _
                                                       " and wf_iscurrent ~~* 'Y'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'D' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Rollback Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
                        Next

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Rollbacked..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "true as print_comma, " _
            & "true as print_detail, " _
            & "fp_mstr.fp_oid, " _
            & "fp_mstr.fp_dom_id, " _
            & "fp_mstr.fp_en_id, " _
            & "fp_mstr.fp_add_by, " _
            & "fp_mstr.fp_add_date, " _
            & "fp_mstr.fp_upd_by, " _
            & "fp_mstr.fp_upd_date, " _
            & "fp_mstr.fp_code, " _
            & "fp_mstr.fp_pengali_tax, " _
            & "fp_mstr.fp_dt, " _
            & "fp_mstr.fp_date, " _
            & "fp_mstr.fp_sign, " _
            & "fp_mstr.fp_status, " _
            & "fp_mstr.fp_customer_type, " _
            & "fp_mstr.fp_area, " _
            & "fp_mstr.fp_ppn_type, " _
            & "fp_mstr.fp_ptnr_id, " _
            & "fp_mstr.fp_tax_inc, " _
            & "fp_mstr.fp_ar_oid , " _
            & "fp_mstr.fp_unstrikeout , " _
            & "fm.fp_code as fp_code_pengganti, " _
            & "fm.fp_date as fp_date_pengganti, " _
            & "fp_mstr.fp_trans_id, " _
            & "ar_code, " _
            & "ar_date,  " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_npwp, " _
            & "cmaddr_pkp_date, " _
            & "coalesce(ptnr_name_alt,ptnr_name) as ptnr_name, " _
            & "ptnr_npwp, " _
            & "ptnr_nppkp, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ar_cu_id, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "ars_tax_class_id, " _
            & "sod_disc, " _
            & "ar_exc_rate, " _
            & "ar_credit_term, " _
            & "cu_code, " _
            & "credit_terms_mstr.code_name as top_name, " _
            & "pt_code, " _
            & "pt_desc1, " _
            & "pt_desc2, " _
            & "sod_seq, " _
            & "(select conf_value from conf_file where conf_name = 'faktur_pajak_city') as faktur_pajak_city, " _
            & "ars_tax_inc, " _
            & "ars_invoice, " _
            & "ars_invoice_price, " _
            & "sod_disc, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (ars_invoice_price * ars_invoice) " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) " _
            & "END AS price_ext_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_idr,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ars_invoice * ars_invoice_price * sod_disc " _
            & "WHEN 'Y' THEN ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc) " _
            & "END AS disc_value_usd,  " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN ((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate " _
            & "WHEN 'Y' THEN (ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc)) " _
            & "END AS dpp_value_usd, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_idr, " _
            & "CASE upper(ars_tax_inc) " _
            & "WHEN 'N' THEN (((ars_invoice_price * ars_invoice) - (ars_invoice * ars_invoice_price * sod_disc)) * ar_exc_rate) * 0.1 " _
            & "WHEN 'Y' THEN ((ars_invoice_price * ars_invoice * 100 / 110) - (ars_invoice * ((ars_invoice_price * 100 / 110) * sod_disc))) * 0.1 " _
            & "END AS ppn_usd " _
            & "FROM  " _
            & "fp_mstr " _
            & "inner join ar_mstr on ar_oid = fp_mstr.fp_ar_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "inner join ptnra_addr on ptnra_oid = fp_ptnr_addr_oid " _
            & "inner join ars_ship on ars_ar_oid = ar_oid " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join code_mstr credit_terms_mstr on credit_terms_mstr.code_id = ar_credit_term  " _
            & "left outer join fp_mstr fm on fm.fp_oid = fp_mstr.fp_fp_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "where ars_taxable ~~* 'Y'" _
            & "  and fp_mstr.fp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fp_oid").ToString + "'" _
            & "  order by fp_mstr.fp_code, sod_seq "

        Dim rpt As Object = Nothing

        'If ce_blank.Checked = True Then
        rpt = New XRFakturPajakFormPlain
        'Else
        'rpt = New XRFakturPajakForm
        'End If

        Try
            With rpt
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Exit Sub
                End Try

                If ds_bantu.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                .DataSource = ds_bantu
                .DataMember = "data"
                .ShowPreview()

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ce_page_number_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_page_number.CheckedChanged
        XRFakturPajakFormPlain.StatusForm = "3"
        'XRFakturPajakForm.StatusFormPajak = "3"
        If ce_page_number.Checked = True Then
            PageNumAproval = "Y"
        Else
            PageNumAproval = "N"
        End If
    End Sub
End Class
