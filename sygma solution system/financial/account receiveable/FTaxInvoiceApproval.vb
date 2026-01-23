Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FTaxInvoiceApproval
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
        xtc_detail.TabPages(5).PageVisible = False
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
        add_column_copy(gv_os, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Revisi", "ti_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "ti_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Invoice Replacement", "ti_code_pengganti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Signature User", "sign_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Type", "ti_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Unstrikeout", "ti_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Approval Status", "ti_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "ti_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "ti_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "User Update", "ti_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "ti_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Revisi", "ti_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "ti_status_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Invoice Replacement", "ti_code_pengganti", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Signature User", "sign_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Type", "ti_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Unstrikeout", "ti_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Approval Status", "ti_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "ti_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "ti_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "User Update", "ti_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "ti_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail_soship, "tis_ti_oid", False)
        add_column_copy(gv_detail_soship, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_soship, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_soship, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_soship, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_ar, "tia_ti_oid", False)
        add_column_copy(gv_detail_ar, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "AR Code", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "AR Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_ar, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_pt, "tip_ti_oid", False)
        add_column_copy(gv_detail_pt, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pt, "Qty", "tip_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Price", "tip_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Discount", "tip_disc", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Tax Rate", "tip_tax_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_pt, "Tax", "tip_ppn", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pt, "Total", "tip_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

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

        add_column(gv_email, "ti_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Invoice Number", "ti_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Invoice Date", "ti_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_email, "Revisi", "ti_rev", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Signature User", "sign_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Address", "address", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Tax Type", "ti_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Unstrikeout", "ti_unstrikeout", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Routing Approval", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Approval Status", "ti_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "User Create", "ti_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Date Create", "ti_add_date", DevExpress.Utils.HorzAlignment.Center)
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

                            .SQL = "SELECT  false as status, " _
                                & "  ti_mstr.ti_oid, " _
                                & "  ti_mstr.ti_dom_id, " _
                                & "  ti_mstr.ti_en_id, " _
                                & "  en_desc, " _
                                & "  ti_mstr.ti_add_by, " _
                                & "  ti_mstr.ti_add_date, " _
                                & "  ti_mstr.ti_upd_by, " _
                                & "  ti_mstr.ti_upd_date, " _
                                & "  ti_mstr.ti_dt, " _
                                & "  ti_mstr.ti_code, " _
                                & "  ti_mstr.ti_date, " _
                                & "  ti_mstr.ti_sign_id, " _
                                & "  code_name as sign_name, " _
                                & "  ti_mstr.ti_status, " _
                                & "  CASE WHEN ti_mstr.ti_status = '0' " _
                                & "      THEN 'Normal' " _
                                & "      ELSE 'Penggantian' " _
                                & "  END AS ti_status_desc, " _
                                & "  ti_mstr.ti_ptnr_id, " _
                                & "  ptnr_name, " _
                                & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                                & "  ptnr_nppkp, " _
                                & "  ti_mstr.ti_customer_type, " _
                                & "  ti_mstr.ti_area, " _
                                & "  ti_mstr.ti_ppn_type, " _
                                & "  ti_mstr.ti_ptnr_addr_oid, " _
                                & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                                & "  ti_mstr.ti_tran_id, " _
                                & "  tran_name, " _
                                & "  ti_mstr.ti_trans_id, " _
                                & "  ti_mstr.ti_rev, " _
                                & "  ti_mstr.ti_unstrikeout, " _
                                & "  ti_mstr.ti_ti_oid, " _
                                & "  tm.ti_code as ti_code_pengganti, coalesce(useremail,'') as useremail,wf_seq " _
                                & "FROM  " _
                                & "  public.ti_mstr " _
                                & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
                                & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
                                & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
                                & "  inner join code_mstr on code_id = ti_mstr.ti_sign_id " _
                                & "  inner join tran_mstr on tran_id = ti_mstr.ti_tran_id " _
                                & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
                                & "  inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                                & "  inner join tconfuser on usernama = ti_mstr.ti_add_by " _
                                & "  where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & "  and wf_user_id in (" + _user_approval + ")" _
                                & "  and ti_mstr.ti_trans_id = 'W' " _
                                & "  and wf_iscurrent ~~* 'Y' order by ti_mstr.ti_code "
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  ti_mstr.ti_oid, " _
                                & "  ti_mstr.ti_dom_id, " _
                                & "  ti_mstr.ti_en_id, " _
                                & "  en_desc, " _
                                & "  ti_mstr.ti_add_by, " _
                                & "  ti_mstr.ti_add_date, " _
                                & "  ti_mstr.ti_upd_by, " _
                                & "  ti_mstr.ti_upd_date, " _
                                & "  ti_mstr.ti_dt, " _
                                & "  ti_mstr.ti_code, " _
                                & "  ti_mstr.ti_date, " _
                                & "  ti_mstr.ti_sign_id, " _
                                & "  code_name as sign_name, " _
                                & "  ti_mstr.ti_status, " _
                                & "  CASE WHEN ti_mstr.ti_status = '0' " _
                                & "      THEN 'Normal' " _
                                & "      ELSE 'Penggantian' " _
                                & "  END AS ti_status_desc, " _
                                & "  ti_mstr.ti_ptnr_id, " _
                                & "  ptnr_name, " _
                                & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                                & "  ptnr_nppkp, " _
                                & "  ti_mstr.ti_customer_type, " _
                                & "  ti_mstr.ti_area, " _
                                & "  ti_mstr.ti_ppn_type, " _
                                & "  ti_mstr.ti_ptnr_addr_oid, " _
                                & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                                & "  ti_mstr.ti_tran_id, " _
                                & "  tran_name, " _
                                & "  ti_mstr.ti_trans_id, " _
                                & "  ti_mstr.ti_rev, " _
                                & "  ti_mstr.ti_unstrikeout, " _
                                & "  ti_mstr.ti_ti_oid, " _
                                & "  tm.ti_code as ti_code_pengganti " _
                                & "FROM  " _
                                & "  public.ti_mstr " _
                                & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
                                & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
                                & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
                                & "  inner join code_mstr on code_id = ti_mstr.ti_sign_id " _
                                & "  inner join tran_mstr on tran_id = ti_mstr.ti_tran_id " _
                                & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
                                & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                                & " where ti_mstr.ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and ti_mstr.ti_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
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
                              " inner join ti_mstr on ti_oid = wf_ref_oid " + _
                              " where wf_ref_oid in (select ti_oid from ti_mstr inner join wf_mstr on wf_ref_oid = ti_oid " _
                                                  & " where ti_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tia_oid, " _
                            & "  tia_ti_oid, " _
                            & "  tia_seq, " _
                            & "  tia_ar_oid, " _
                            & "  en_desc, " _
                            & "  ar_code, " _
                            & "  ar_eff_date, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tia_ar  " _
                            & "  inner join ti_mstr on ti_oid = tia_ti_oid " _
                            & "  inner join ar_mstr on ar_oid = tia_ar_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                            & "  inner join en_mstr on en_id = ar_en_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'" 
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "ar")
                        gc_detail_ar.DataSource = ds_detail.Tables("ar")
                        gv_detail_ar.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tis_oid, " _
                            & "  tis_ti_oid, " _
                            & "  tis_seq, " _
                            & "  tis_soship_oid, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  en_desc, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tis_soship  " _
                            & "  inner join ti_mstr on ti_oid = tis_ti_oid " _
                            & "  inner join soship_mstr on soship_oid = tis_soship_oid " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
                            & "  inner join en_mstr on en_id = so_en_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'" 
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "soship")
                        gc_detail_soship.DataSource = ds_detail.Tables("soship")
                        gv_detail_soship.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tip_oid, " _
                            & "  tip_ti_oid, " _
                            & "  tip_seq, " _
                            & "  tip_pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  tip_qty, " _
                            & "  tip_price, " _
                            & "  tip_disc, " _
                            & "  tip_tax_rate, " _
                            & "  tip_ppn, " _
                            & "  tip_pph, " _
                            & "  tip_total " _
                            & "FROM  " _
                            & "  public.tip_pt  " _
                            & "  inner join ti_mstr on ti_oid = tip_ti_oid " _
                            & "  inner join pt_mstr on pt_Id = tip_pt_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'" 
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "item")
                        gc_detail_pt.DataSource = ds_detail.Tables("item")
                        gv_detail_pt.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  ti_mstr.ti_oid, " _
                            & "  ti_mstr.ti_dom_id, " _
                            & "  ti_mstr.ti_en_id, " _
                            & "  en_desc, " _
                            & "  ti_mstr.ti_add_by, " _
                            & "  ti_mstr.ti_add_date, " _
                            & "  ti_mstr.ti_upd_by, " _
                            & "  ti_mstr.ti_upd_date, " _
                            & "  ti_mstr.ti_dt, " _
                            & "  ti_mstr.ti_code, " _
                            & "  ti_mstr.ti_date, " _
                            & "  ti_mstr.ti_sign_id, " _
                            & "  code_name as sign_name, " _
                            & "  ti_mstr.ti_status, " _
                            & "  CASE WHEN ti_mstr.ti_status = '0' " _
                            & "      THEN 'Normal' " _
                            & "      ELSE 'Penggantian' " _
                            & "  END AS ti_status_desc, " _
                            & "  ti_mstr.ti_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                            & "  ptnr_nppkp, " _
                            & "  ti_mstr.ti_customer_type, " _
                            & "  ti_mstr.ti_area, " _
                            & "  ti_mstr.ti_ppn_type, " _
                            & "  ti_mstr.ti_ptnr_addr_oid, " _
                            & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                            & "  ti_mstr.ti_tran_id, " _
                            & "  tran_name, " _
                            & "  ti_mstr.ti_trans_id, " _
                            & "  ti_mstr.ti_rev, " _
                            & "  ti_mstr.ti_unstrikeout, " _
                            & "  ti_mstr.ti_ti_oid, " _
                            & "  tm.ti_code as ti_code_pengganti " _
                            & "FROM  " _
                            & "  public.ti_mstr " _
                            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
                            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
                            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
                            & "  inner join code_mstr on code_id = ti_mstr.ti_sign_id " _
                            & "  inner join tran_mstr on tran_id = ti_mstr.ti_tran_id " _
                            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
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
                              " inner join ti_mstr on ti_oid = wf_ref_oid " + _
                              " where wf_ref_oid in (select ti_oid from ti_mstr inner join wf_mstr on wf_ref_oid = ti_oid " _
                                                  & " where ti_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and ti_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tia_oid, " _
                            & "  tia_ti_oid, " _
                            & "  tia_seq, " _
                            & "  tia_ar_oid, " _
                            & "  en_desc, " _
                            & "  ar_code, " _
                            & "  ar_eff_date, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tia_ar  " _
                            & "  inner join ti_mstr on ti_oid = tia_ti_oid " _
                            & "  inner join ar_mstr on ar_oid = tia_ar_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = ar_bill_to " _
                            & "  inner join en_mstr on en_id = ar_en_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and ti_date <= " + SetDate(pr_txttglakhir.DateTime)
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "ar")
                        gc_detail_ar.DataSource = ds_detail.Tables("ar")
                        gv_detail_ar.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tis_oid, " _
                            & "  tis_ti_oid, " _
                            & "  tis_seq, " _
                            & "  tis_soship_oid, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  soship_code, " _
                            & "  soship_date, " _
                            & "  en_desc, " _
                            & "  ptnr_name " _
                            & "FROM  " _
                            & "  public.tis_soship  " _
                            & "  inner join ti_mstr on ti_oid = tis_ti_oid " _
                            & "  inner join soship_mstr on soship_oid = tis_soship_oid " _
                            & "  inner join so_mstr on so_oid = soship_so_oid " _
                            & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_bill " _
                            & "  inner join en_mstr on en_id = so_en_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and ti_date <= " + SetDate(pr_txttglakhir.DateTime)
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "soship")
                        gc_detail_soship.DataSource = ds_detail.Tables("soship")
                        gv_detail_soship.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  tip_oid, " _
                            & "  tip_ti_oid, " _
                            & "  tip_seq, " _
                            & "  tip_pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  tip_qty, " _
                            & "  tip_price, " _
                            & "  tip_disc, " _
                            & "  tip_tax_rate, " _
                            & "  tip_ppn, " _
                            & "  tip_pph, " _
                            & "  tip_total " _
                            & "FROM  " _
                            & "  public.tip_pt  " _
                            & "  inner join ti_mstr on ti_oid = tip_ti_oid " _
                            & "  inner join pt_mstr on pt_Id = tip_pt_id " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and ti_date <= " + SetDate(pr_txttglakhir.DateTime)
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "item")
                        gc_detail_pt.DataSource = ds_detail.Tables("item")
                        gv_detail_pt.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  ti_mstr.ti_oid, " _
                            & "  ti_mstr.ti_dom_id, " _
                            & "  ti_mstr.ti_en_id, " _
                            & "  en_desc, " _
                            & "  ti_mstr.ti_add_by, " _
                            & "  ti_mstr.ti_add_date, " _
                            & "  ti_mstr.ti_upd_by, " _
                            & "  ti_mstr.ti_upd_date, " _
                            & "  ti_mstr.ti_dt, " _
                            & "  ti_mstr.ti_code, " _
                            & "  ti_mstr.ti_date, " _
                            & "  ti_mstr.ti_sign_id, " _
                            & "  code_name as sign_name, " _
                            & "  ti_mstr.ti_status, " _
                            & "  CASE WHEN ti_mstr.ti_status = '0' " _
                            & "      THEN 'Normal' " _
                            & "      ELSE 'Penggantian' " _
                            & "  END AS ti_status_desc, " _
                            & "  ti_mstr.ti_ptnr_id, " _
                            & "  ptnr_name, " _
                            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
                            & "  ptnr_nppkp, " _
                            & "  ti_mstr.ti_customer_type, " _
                            & "  ti_mstr.ti_area, " _
                            & "  ti_mstr.ti_ppn_type, " _
                            & "  ti_mstr.ti_ptnr_addr_oid, " _
                            & "  (coalesce(ptnra_line_1,'') || ' ' || coalesce(ptnra_line_2,'') || ' ' || coalesce(ptnra_line_3,'')) as address, " _
                            & "  ti_mstr.ti_tran_id, " _
                            & "  tran_name, " _
                            & "  ti_mstr.ti_trans_id, " _
                            & "  ti_mstr.ti_rev, " _
                            & "  ti_mstr.ti_unstrikeout, " _
                            & "  ti_mstr.ti_ti_oid, " _
                            & "  tm.ti_code as ti_code_pengganti " _
                            & "FROM  " _
                            & "  public.ti_mstr " _
                            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
                            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
                            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
                            & "  inner join code_mstr on code_id = ti_mstr.ti_sign_id " _
                            & "  inner join tran_mstr on tran_id = ti_mstr.ti_tran_id " _
                            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = ti_mstr.ti_oid " _
                            & " where ti_mstr.ti_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and ti_mstr.ti_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and ti_mstr.ti_date <= " + SetDate(pr_txttglakhir.DateTime) _
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
                gv_detail_soship.Columns("tis_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tis_ti_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ti_oid").ToString & "'")
                gv_detail_soship.BestFitColumns()

                gv_detail_ar.Columns("tia_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tia_ti_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ti_oid").ToString & "'")
                gv_detail_ar.BestFitColumns()

                gv_detail_pt.Columns("tip_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tip_ti_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ti_oid").ToString & "'")
                gv_detail_pt.BestFitColumns()

                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ti_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("ti_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[ti_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ti_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail_soship.Columns("tis_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tis_ti_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ti_oid").ToString & "'")
                gv_detail_soship.BestFitColumns()

                gv_detail_ar.Columns("tia_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tia_ti_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ti_oid").ToString & "'")
                gv_detail_ar.BestFitColumns()

                gv_detail_pt.Columns("tip_ti_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("tip_ti_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ti_oid").ToString & "'")
                gv_detail_pt.BestFitColumns()

                gv_wf.Columns("wf_ref_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ti_oid").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("ti_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[ti_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ti_oid").ToString & "'")
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

    Private Sub le_wfs_status_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles le_wfs_status.EditValueChanged
        If le_wfs_status.EditValue = 2 Then
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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

        _colom = "ti_trans_id"
        _table = "ti_mstr"
        _initial = "ti"
        _type = "ti"
        _title = "Tax Invoice"

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

    Public Overrides Sub approve_wf(ByVal par_colom As String, ByVal par_table As String, ByVal par_initial As String, ByVal par_type As String, _
                                  ByVal par_status_id As Integer, ByVal par_desc As String, ByVal par_user_approval As String, _
                                  ByVal par_ds As DataSet, ByVal par_gv As Object, ByVal par_title As String)
        If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim wf_seq, i, _max_seq As Integer
        Dim filename, user_wf, user_wf_email, format_email_bantu As String
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
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'" + _
                                                       " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
                                                   " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
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

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

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
        Dim ssqls As New ArrayList
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
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
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
        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim _dt_ar, _dt_soship As DataTable

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
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                '---------------------------------------------------------------------------------------------
                                'Update ar dan soship
                                _dt_ar = New DataTable
                                _dt_ar = get_ar(par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString)
                                If _dt_ar.Rows.Count = 0 Then
                                    MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

                                For Each _dr As DataRow In _dt_ar.Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update ar_mstr set ar_ti_in_use = 'N' " + _
                                                           " where ar_oid = " + SetSetring(_dr("tia_ar_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next

                                _dt_soship = New DataTable
                                _dt_soship = get_soship(par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString)
                                If _dt_soship.Rows.Count = 0 Then
                                    MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

                                For Each _dr As DataRow In _dt_soship.Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update soship_mstr set soship_ti_in_use = 'N' " + _
                                                           " where soship_oid = " + SetSetring(_dr("tis_soship_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                                '---------------------------------------------------------------------------------------------

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Cancel Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
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
        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim _dt_ar, _dt_soship As DataTable

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
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'D' " + _
                                                       " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                '---------------------------------------------------------------------------------------------
                                'Update ar dan soship
                                _dt_ar = New DataTable
                                _dt_ar = get_ar(par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString)
                                If _dt_ar.Rows.Count = 0 Then
                                    MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

                                For Each _dr As DataRow In _dt_ar.Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update ar_mstr set ar_ti_in_use = 'N' " + _
                                                           " where ar_oid = " + SetSetring(_dr("tia_ar_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next

                                _dt_soship = New DataTable
                                _dt_soship = get_soship(par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString)
                                If _dt_soship.Rows.Count = 0 Then
                                    MessageBox.Show("Error Connection...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Exit Sub
                                End If

                                For Each _dr As DataRow In _dt_soship.Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update soship_mstr set soship_ti_in_use = 'N' " + _
                                                           " where soship_oid = " + SetSetring(_dr("tis_soship_oid"))
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                                '---------------------------------------------------------------------------------------------

                                If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Rollback Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                End If
                            End If
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

    Private Function get_ar(ByVal par_oid As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tia_oid, " _
                            & "  tia_ar_oid " _
                            & "FROM  " _
                            & "  public.tia_ar  " _
                            & " where tia_ti_oid = " + SetSetring(par_oid).ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "bantu")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Function get_soship(ByVal par_oid As String) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  tis_oid, " _
                            & "  tis_soship_oid " _
                            & "FROM  " _
                            & "  public.tis_soship  " _
                            & "  where tis_ti_oid = " + SetSetring(par_oid).ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "bantu")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Function set_sql_detail_gabungan() As String
        set_sql_detail_gabungan = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  tip_seq, " _
            & "  tip_pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  trim(pt_desc1 || ' ' || pt_desc2) as pt_desc, " _
            & "  tip_qty, " _
            & "  tip_price, " _
            & "  tip_ppn, " _
            & "  tip_total, " _
            & "  tip_disc, " _
            & "  tip_qty * tip_price as qty_price, " _
            & "  tip_qty * tip_price as qty_price_usd, " _
            & "  tip_qty * tip_disc as qty_disc, " _
            & "  tip_qty * tip_disc as qty_disc_usd, " _
            & "  tip_qty * tip_ppn as qty_ppn, " _
            & "  tip_qty * tip_ppn as qty_ppn_usd, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc_usd, " _
            & "  tip_tax_rate, " _
            & "  'Reference : ' || coalesce(ti_mstr.ti_ref_ar,ti_mstr.ti_ref_so) as ar_code, " _
            & "  (select count(tia_oid) as jml from tia_ar where tia_ti_oid = ti_mstr.ti_oid) as jml_ar " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  order by ti_mstr.ti_code, tip_seq "

        Return set_sql_detail_gabungan
    End Function

    Private Function set_sql_detail_single() As String
        set_sql_detail_single = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  tip_seq, " _
            & "  tip_pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  trim(pt_desc1 || ' ' || pt_desc2) as pt_desc, " _
            & "  tip_qty, " _
            & "  tip_price, " _
            & "  tip_ppn, " _
            & "  tip_total, " _
            & "  tip_disc, " _
            & "  tip_qty * tip_price as qty_price, " _
            & "  tip_qty * tip_price as qty_price_usd, " _
            & "  tip_qty * tip_disc as qty_disc, " _
            & "  tip_qty * tip_disc as qty_disc_usd, " _
            & "  tip_qty * tip_ppn as qty_ppn, " _
            & "  tip_qty * tip_ppn as qty_ppn_usd, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc, " _
            & "  (tip_qty * tip_price) - (tip_qty * tip_disc) as price_kurang_disc_usd, " _
            & "  tip_tax_rate, " _
            & "  (select count(tia_oid) as jml from tia_ar where tia_ti_oid = ti_mstr.ti_oid) as jml_ar, " _
            & "  (select count(tis_oid) as jml from tis_soship where tis_ti_oid = ti_mstr.ti_oid) as jml_soship, " _
            & "  'Reference : ' || coalesce(ti_mstr.ti_ref_ar,ti_mstr.ti_ref_so) as ar_code, " _
            & "  term_mstr.code_name as top_name, " _
            & "  tip_exc_rate " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  left outer join tia_ar on tia_ti_oid = ti_mstr.ti_oid " _
            & "  left outer join ar_mstr on ar_oid = tia_ar_oid " _
            & "  left outer join code_mstr term_mstr on term_mstr.code_id = ar_credit_term " _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  order by ti_mstr.ti_code, tip_seq "

        Return set_sql_detail_single
    End Function

    Private Function set_sql_group() As String
        set_sql_group = "SELECT  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  case ti_mstr.ti_ppn_type " _
            & "  when 'E' then 'Penjualan Buku' " _
            & "  when 'A' then 'PPN Dibayar' " _
            & "  end as pt_desc1, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name as sign_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  coalesce(ptnr_npwp,'00.000.000.0-000.000') as ptnr_npwp, " _
            & "  trim(ptnra_line_2 || ' ' || ptnra_line_3) as ptnra_line, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code as ti_code_pengganti,  " _
            & "  tm.ti_date as ti_date_pengganti, " _
            & "  sum(tip_qty * tip_price) as qty_price, " _
            & "  sum(tip_qty * tip_price) as qty_price_usd, " _
            & "  sum(tip_qty * tip_disc) as qty_disc, " _
            & "  sum(tip_qty * tip_disc) as qty_disc_usd, " _
            & "  sum(tip_qty * tip_ppn) as qty_ppn, " _
            & "  sum(tip_qty * tip_ppn) as qty_ppn_usd, " _
            & "   sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc, " _
            & "  sum((tip_qty * tip_price) - (tip_qty * tip_disc)) as price_kurang_disc_usd " _
            & "FROM  " _
            & "  ti_mstr " _
            & "  inner join en_mstr on en_id = ti_mstr.ti_en_id " _
            & "  inner join cmaddr_mstr on cmaddr_en_id = en_id " _
            & "  inner join code_mstr sign_mstr on sign_mstr.code_id = ti_mstr.ti_sign_id " _
            & "  inner join ptnr_mstr on ptnr_id = ti_mstr.ti_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_oid = ti_mstr.ti_ptnr_addr_oid " _
            & "  inner join cu_mstr on cu_id = ti_mstr.ti_cu_id " _
            & "  left outer join ti_mstr tm on tm.ti_oid = ti_mstr.ti_ti_oid " _
            & "  inner join tip_pt on tip_ti_oid = ti_mstr.ti_oid " _
            & "  inner join pt_mstr on pt_id = tip_pt_id" _
            & "  where ti_mstr.ti_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid").ToString + "'" _
            & "  group by  " _
            & "  ti_mstr.ti_oid, " _
            & "  ti_mstr.ti_dom_id, " _
            & "  ti_mstr.ti_en_id, " _
            & "  ti_mstr.ti_add_by, " _
            & "  ti_mstr.ti_add_date, " _
            & "  ti_mstr.ti_upd_by, " _
            & "  ti_mstr.ti_upd_date, " _
            & "  ti_mstr.ti_dt, " _
            & "  ti_mstr.ti_code, " _
            & "  ti_mstr.ti_date, " _
            & "  ti_mstr.ti_sign_id, " _
            & "  ti_mstr.ti_ptnr_id, " _
            & "  ti_mstr.ti_status, " _
            & "  ti_mstr.ti_customer_type, " _
            & "  ti_mstr.ti_area, " _
            & "  ti_mstr.ti_ppn_type, " _
            & "  ti_mstr.ti_ptnr_addr_oid, " _
            & "  ti_mstr.ti_tran_id, " _
            & "  ti_mstr.ti_trans_id, " _
            & "  ti_mstr.ti_rev, " _
            & "  ti_mstr.ti_unstrikeout, " _
            & "  ti_mstr.ti_ti_oid, " _
            & "  cmaddr_name,  " _
            & "  cmaddr_npwp, " _
            & "  cmaddr_tax_line_1, " _
            & "  cmaddr_tax_line_2, " _
            & "  cmaddr_tax_line_3, " _
            & "  sign_mstr.code_name, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  ptnr_npwp, " _
            & "  ti_mstr.ti_cu_id, " _
            & "  cu_code, " _
            & "  tm.ti_code,  " _
            & "  tm.ti_date "

        Return set_sql_group
    End Function

    Private Function get_ar_count() As Integer
        get_ar_count = 0

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select count(tia_oid) as jml from tia_ar where tia_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "tia")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_ar_count = ds_bantu.Tables(0).Rows(0).Item("jml")

        Return get_ar_count
    End Function

    Private Function get_soship_count() As Integer
        get_soship_count = 0

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select count(tis_oid) as jml from tis_soship where tis_ti_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ti_oid"))
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "tis")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        get_soship_count = ds_bantu.Tables(0).Rows(0).Item("jml")

        Return get_soship_count
    End Function

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String
        Dim _ar_count As Integer = get_ar_count()
        Dim _soship_count As Integer = get_soship_count()

        If _ar_count = 0 And _soship_count = 0 Then
            MessageBox.Show("Data Doens't Exist.., Contact Your Admin Program..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If ce_show_detail.Checked = True Then
            If _ar_count > 1 Then
                _sql = set_sql_detail_gabungan()
            Else : _sql = set_sql_detail_single()
            End If
        Else
            _sql = set_sql_group()
        End If


        Dim rpt As Object = Nothing

        rpt = New XRTaxInvoice

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

    Private Sub CEAll_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CEAll.EditValueChanged
        Try
            For i As Integer = 0 To ds.Tables("os").Rows.Count - 1
                ds.Tables("os").Rows(i).Item("status") = CEAll.EditValue
            Next


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
