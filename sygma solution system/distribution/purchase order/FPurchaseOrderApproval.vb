Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FPurchaseOrderApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FPurchaseOrderApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        gv_email.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways

        xtp_mail.PageVisible = False

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

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Order Date", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Need Date", "po_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Due Date", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Remarks", "po_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Credit Terms", "po_credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Exchange Rate", "po_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Taxable", "po_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Include", "po_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Class", "po_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Close Date", "po_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Total", "po_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "PPN", "po_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "PPH", "po_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "After Tax", "po_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Ext. Total", "po_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. PPN", "po_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. PPH", "po_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. After Tax", "po_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cash/Credit", "po_status_cash", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Bank", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Status", "po_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Order Date", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Need Date", "po_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Due Date", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Remarks", "po_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Credit Terms", "po_credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Exchange Rate", "po_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Taxable", "po_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Include", "po_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Class", "po_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Close Date", "po_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Total", "po_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "PPN", "po_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "PPH", "po_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "After Tax", "po_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Ext. Total", "po_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. PPN", "po_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. PPH", "po_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. After Tax", "po_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cash/Credit", "po_status_cash", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Bank", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Status", "po_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail, "pod_po_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Requisition", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Is Memo", "pod_memo", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Receive", "pod_qty_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Outstanding", "pod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "pod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Taxable", "pod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "pod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "pod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Need Date", "pod_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "pod_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "pod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty * Cost", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Status", "pod_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN", "pod_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "PPH", "pod_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column(gv_wf1, "wf_ref_code", False)
        add_column_copy(gv_wf1, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf1, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf1, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf1, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

       
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            Try
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  false as status," _
                                    & "  public.en_mstr.en_desc, " _
                                    & "  public.po_mstr.po_oid, " _
                                    & "  public.po_mstr.po_dom_id, " _
                                    & "  public.po_mstr.po_en_id, " _
                                    & "  public.po_mstr.po_upd_date, " _
                                    & "  public.po_mstr.po_upd_by, " _
                                    & "  public.po_mstr.po_add_date, " _
                                    & "  public.po_mstr.po_add_by, " _
                                    & "  public.po_mstr.po_code, " _
                                    & "  public.po_mstr.po_ptnr_id, " _
                                    & "  public.po_mstr.po_cmaddr_id, " _
                                    & "  public.po_mstr.po_date, " _
                                    & "  public.po_mstr.po_need_date, " _
                                    & "  public.po_mstr.po_due_date, " _
                                    & "  public.po_mstr.po_rmks, " _
                                    & "  public.po_mstr.po_sb_id, " _
                                    & "  public.po_mstr.po_cc_id, " _
                                    & "  public.po_mstr.po_si_id, " _
                                    & "  public.po_mstr.po_pjc_id, " _
                                    & "  public.po_mstr.po_close_date, " _
                                    & "  public.po_mstr.po_total, " _
                                    & "  public.po_mstr.po_tran_id, " _
                                    & "  public.po_mstr.po_trans_id, " _
                                    & "  public.po_mstr.po_trans_rmks, " _
                                    & "  public.po_mstr.po_current_route, " _
                                    & "  public.po_mstr.po_next_route, " _
                                    & "  public.po_mstr.po_dt, " _
                                    & "  public.ptnr_mstr.ptnr_name, " _
                                    & "  cmaddr_name, " _
                                    & "  tran_name, " _
                                    & "  po_status_cash,user_id_telegram, " _
                                    & "  public.pjc_mstr.pjc_desc, " _
                                    & "  public.si_mstr.si_desc, " _
                                    & "  public.po_mstr.po_bk_id, " _
                                    & "  public.bk_mstr.bk_code,wf_seq,  " _
                                    & "  public.sb_mstr.sb_desc, po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                                    & "  po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name , " _
                                    & "  public.cc_mstr.cc_desc, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                                    & "  po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                                    & "  po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext " _
                                    & "FROM " _
                                    & "  public.po_mstr " _
                                    & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id) " _
                                    & "  LEFT OUTER JOIN public.bk_mstr ON (public.po_mstr.po_bk_id = public.bk_mstr.bk_id) " _
                                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                    & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                                    & "  INNER JOIN public.sb_mstr ON (public.po_mstr.po_sb_id = public.sb_mstr.sb_id) " _
                                    & "  INNER JOIN public.cc_mstr ON (public.po_mstr.po_cc_id = public.cc_mstr.cc_id) " _
                                    & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                                    & "  INNER JOIN public.pjc_mstr ON (public.po_mstr.po_pjc_id = public.pjc_mstr.pjc_id) " _
                                    & "  INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                                    & "  INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                                    & "  INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                                    & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class " _
                                    & " inner join wf_mstr wm on wf_ref_oid = po_oid " _
                                    & " inner join tconfuser on lower(usernama) = lower(po_add_by) " _
                                     & " where  lower(wf_user_id) in (" + _user_approval.ToLower + ")" _
                                    & " and wf_iscurrent = 'Y'" _
                                    & " and po_trans_id = 'W' "

                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                            gv_os.BestFitColumns()
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  false as status," _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.po_mstr.po_oid, " _
                                & "  public.po_mstr.po_dom_id, " _
                                & "  public.po_mstr.po_en_id, " _
                                & "  public.po_mstr.po_upd_date, " _
                                & "  public.po_mstr.po_upd_by, " _
                                & "  public.po_mstr.po_add_date, " _
                                & "  public.po_mstr.po_add_by, " _
                                & "  public.po_mstr.po_code, " _
                                & "  public.po_mstr.po_ptnr_id, " _
                                & "  public.po_mstr.po_cmaddr_id, " _
                                & "  public.po_mstr.po_date, " _
                                & "  public.po_mstr.po_need_date, " _
                                & "  public.po_mstr.po_due_date, " _
                                & "  public.po_mstr.po_rmks, " _
                                & "  public.po_mstr.po_sb_id, " _
                                & "  public.po_mstr.po_cc_id, " _
                                & "  public.po_mstr.po_si_id, " _
                                & "  public.po_mstr.po_pjc_id, " _
                                & "  public.po_mstr.po_close_date, " _
                                & "  public.po_mstr.po_total, " _
                                & "  public.po_mstr.po_tran_id, " _
                                & "  public.po_mstr.po_trans_id, " _
                                & "  public.po_mstr.po_trans_rmks, " _
                                & "  public.po_mstr.po_current_route, " _
                                & "  public.po_mstr.po_next_route, " _
                                & "  public.po_mstr.po_dt, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  cmaddr_name, " _
                                & "  tran_name, " _
                                & "  po_status_cash, " _
                                & "  public.pjc_mstr.pjc_desc, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.po_mstr.po_bk_id, " _
                                & "  public.bk_mstr.bk_code, " _
                                & "  public.sb_mstr.sb_desc, po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                                & "  po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name , " _
                                & "  public.cc_mstr.cc_desc, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                                & "  po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                                & "  po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext " _
                                & "FROM " _
                                & "  public.po_mstr " _
                                & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id) " _
                                & "  LEFT OUTER JOIN public.bk_mstr ON (public.po_mstr.po_bk_id = public.bk_mstr.bk_id) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                                & "  INNER JOIN public.sb_mstr ON (public.po_mstr.po_sb_id = public.sb_mstr.sb_id) " _
                                & "  INNER JOIN public.cc_mstr ON (public.po_mstr.po_cc_id = public.cc_mstr.cc_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pjc_mstr ON (public.po_mstr.po_pjc_id = public.pjc_mstr.pjc_id) " _
                                & "  INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                                & "  INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                                & "  INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                                & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class " _
                                  & " inner join wf_mstr wm on wf_ref_oid = po_oid " _
                               & " where   po_date >= " + SetDate(pr_txttglawal.DateTime) _
                                    & " and po_date <= " + SetDate(pr_txttglakhir.DateTime) _
                               & " and lower(wf_user_id) in (" + _user_approval.ToLower + ")"

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
                            & "  public.pod_det.pod_oid, " _
                            & "  public.pod_det.pod_dom_id, " _
                            & "  public.pod_det.pod_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pod_det.pod_add_by, " _
                            & "  public.pod_det.pod_add_date, " _
                            & "  public.pod_det.pod_upd_by, " _
                            & "  public.pod_det.pod_upd_date, " _
                            & "  public.pod_det.pod_po_oid, " _
                            & "  public.pod_det.pod_seq, " _
                            & "  public.pod_det.pod_reqd_oid, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.pod_det.pod_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.pod_det.pod_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pod_det.pod_pt_desc1, " _
                            & "  public.pod_det.pod_pt_desc2, " _
                            & "  public.pod_det.pod_memo, " _
                            & "  public.pod_det.pod_rmks, " _
                            & "  public.pod_det.pod_end_user, " _
                            & "  public.pod_det.pod_qty, " _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost, " _
                            & "  public.pod_det.pod_disc, " _
                            & "  public.pod_det.pod_need_date, " _
                            & "  public.pod_det.pod_due_date, " _
                            & "  public.pod_det.pod_um_conv, " _
                            & "  public.pod_det.pod_qty_real, " _
                            & "  public.pod_det.pod_pt_class, " _
                            & "  public.pod_det.pod_status, " _
                            & "  public.pod_det.pod_dt, " _
                            & "  public.pod_det.pod_sb_id,pod_ppn,pod_pph, " _
                            & "  sb_desc, " _
                            & "  public.pod_det.pod_cc_id, " _
                            & "  cc_desc, " _
                            & "  loc_desc, " _
                            & "  public.pod_det.pod_pjc_id, " _
                            & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
                            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                            & "  taxclass_mstr.code_name as pod_tax_class_name " _
                            & "  FROM " _
                            & "  public.pod_det " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                            & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = po_oid " _
                            & " where  lower(wf_user_id) in (" + _user_approval.ToLower + ")" _
                            & " and wf_iscurrent = 'Y'"


                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join po_mstr on po_code = wf_ref_code " _
                              & " where wf_ref_code in (select po_code from po_mstr inner join wf_mstr on wf_ref_code = po_code " _
                                                  & " where  lower(wf_user_id) in (" + _user_approval.ToLower + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf1.DataSource = ds_detail.Tables("wf")
                        gv_wf1.BestFitColumns()


                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                       

                        .SQL = "SELECT  " _
                            & "  public.pod_det.pod_oid, " _
                            & "  public.pod_det.pod_dom_id, " _
                            & "  public.pod_det.pod_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pod_det.pod_add_by, " _
                            & "  public.pod_det.pod_add_date, " _
                            & "  public.pod_det.pod_upd_by, " _
                            & "  public.pod_det.pod_upd_date, " _
                            & "  public.pod_det.pod_po_oid, " _
                            & "  public.pod_det.pod_seq, " _
                            & "  public.pod_det.pod_reqd_oid, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.pod_det.pod_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.pod_det.pod_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pod_det.pod_pt_desc1, " _
                            & "  public.pod_det.pod_pt_desc2, " _
                            & "  public.pod_det.pod_memo, " _
                            & "  public.pod_det.pod_rmks, " _
                            & "  public.pod_det.pod_end_user, " _
                            & "  public.pod_det.pod_qty, " _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost, " _
                            & "  public.pod_det.pod_disc, " _
                            & "  public.pod_det.pod_need_date, " _
                            & "  public.pod_det.pod_due_date, " _
                            & "  public.pod_det.pod_um_conv, " _
                            & "  public.pod_det.pod_qty_real, " _
                            & "  public.pod_det.pod_pt_class, " _
                            & "  public.pod_det.pod_status, " _
                            & "  public.pod_det.pod_dt, " _
                            & "  public.pod_det.pod_sb_id,pod_ppn,pod_pph, " _
                            & "  sb_desc, " _
                            & "  public.pod_det.pod_cc_id, " _
                            & "  cc_desc, " _
                            & "  loc_desc, " _
                            & "  public.pod_det.pod_pjc_id, " _
                            & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
                            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                            & "  taxclass_mstr.code_name as pod_tax_class_name " _
                            & "  FROM " _
                            & "  public.pod_det " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                            & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " inner join wf_mstr wm on wf_ref_oid = po_oid " _
                            & " where  lower(wf_user_id) in (" + _user_approval.ToLower + ") and " _
                            & " po_mstr.po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & "  and po_mstr.po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""


                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join po_mstr on po_code = wf_ref_code " _
                            & " where wf_ref_code in (select po_code from po_mstr inner join wf_mstr on wf_ref_code = po_code " _
                                                  & "  where  lower(wf_user_id) in (" + _user_approval.ToLower + "))" _
                            & " and po_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and po_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "

                       
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
                gv_detail.Columns("pod_po_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[pod_po_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("po_oid").ToString & "'")
                gv_detail.BestFitColumns()
                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never

                gv_wf1.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("po_code").ToString & "'")
                gv_wf1.BestFitColumns()

                'gv_email.Columns("pod_po_oid").FilterInfo = _
                'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[pod_po_oid] = '" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("po_oid").ToString & "'")
                'gv_email.BestFitColumns()


            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("pod_po_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[pod_po_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("po_oid").ToString & "'")
                gv_detail.BestFitColumns()
                gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never

                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("po_code").ToString & "'")
                gv_wf.BestFitColumns()

                'gv_email.Columns("pod_po_oid").FilterInfo = _
                'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[pod_po_oid] = '" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("po_oid").ToString & "'")
                'gv_email.BestFitColumns()
            Catch ex As Exception
                'MsgBox(ex.Message)
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

        _colom = "po_trans_id"
        _table = "po_mstr"
        _initial = "po"
        _type = "po"
        _title = "Purchase Order"

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
                                           " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                           " and lower(wf_user_id) in (" + par_user_approval.ToLower + ")" + _
                                           " and wf_iscurrent ~~* 'Y'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and wf_seq = " + (par_ds.Tables("os").Rows(i).Item("wf_seq") + 1).ToString
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If func_coll.get_conf_file("send_telegram") = "1" Then
                                    Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval").Replace("approval_telegram.php", "update_approval.php") & "?number_transaction=" & par_ds.Tables("os").Rows(i).Item(par_initial + "_code") _
                                    & "&syspro=" & func_coll.get_conf_file("syspro_approval_code") & "&user_id_telegram=" & par_ds.Tables("os").Rows(i).Item("user_id_telegram")

                                    Dim result As String
                                    result = mf.run_get_to_api(_alamat_api & "")

                                    If SetString(result).Contains("success") Then
                                        '_MakeReport("Data empty")

                                    Else
                                        Box("Gagal update approval")
                                        'Exit Sub
                                    End If
                                End If


                                _max_seq = mf.get_max_seq(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"))

                                If _max_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") Then

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'I'" + _
                                                   " where " + par_initial + "_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pod_det set pod_status = 'I' " _
                                                           & " where pod_po_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()


                                    

                                End If

                                wf_seq = par_ds.Tables("os").Rows(i).Item("wf_seq") + 1
                                user_wf = mf.get_user_wf(par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), wf_seq)
                                'user_wf_email = mf.get_email_address(user_wf)

                                'If user_wf_email <> "" Then
                                '    filename = "c:\syspro\" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + ".xls"
                                '    ExportTo(par_gv, New ExportXlsProvider(filename))

                                '    format_email_bantu = mf.format_email(user_wf, par_ds.Tables("os").Rows(i).Item(par_initial + "_code"), par_type)
                                '    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                'End If
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

    Public Overrides Sub cancel_wf(ByVal par_table As String, ByVal par_colom As String, ByVal par_initial As String, ByVal par_type As String, ByVal par_status_id As Integer, ByVal par_desc As String, _
                          ByVal par_user_approval As String, ByVal par_ds As DataSet, ByVal par_title As String)
        If MessageBox.Show("Cancel Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim i_2 As Integer = 0

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
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_wfs_id = '" + par_status_id.ToString + "'," + _
                                                       " wf_aprv_date = current_timestamp, " + _
                                                       " wf_aprv_user = '" + master_new.ClsVar.sNama + "'," + _
                                                       " wf_desc = '" + Trim(par_desc) + "'," + _
                                                       " wf_iscurrent = 'N'," + _
                                                       " wf_dt = current_timestamp, " + _
                                                       " wf_date_to = null " + _
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and lower(wf_user_id) in (" + par_user_approval.ToLower + ")" + _
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

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_status = 'X' " _
                                                       & " where pod_po_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                '    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Cancel Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                'End If


                                If func_coll.get_conf_file("send_telegram") = "1" Then
                                    Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval").Replace("approval_telegram.php", "update_approval.php") & "?number_transaction=" & par_ds.Tables("os").Rows(i).Item(par_initial + "_code") _
                                    & "&syspro=" & func_coll.get_conf_file("syspro_approval_code") & "&user_id_telegram=" & par_ds.Tables("os").Rows(i).Item("user_id_telegram")

                                    Dim result As String
                                    result = mf.run_get_to_api(_alamat_api & "")

                                    If SetString(result).Contains("success") Then
                                        '_MakeReport("Data empty")

                                    Else
                                        Box("Gagal update approval")
                                        'Exit Sub
                                    End If
                                End If

                                For n As Integer = 0 To ds_detail.Tables("detail").Rows.Count - 1
                                    If ds_detail.Tables("detail").Rows(n).Item("pod_po_oid").ToString = par_ds.Tables("os").Rows(i).Item("po_oid").ToString Then
                                        'Update relation PR apabila terdapat relasi pr
                                        If IsDBNull(ds_detail.Tables("detail").Rows(n).Item("pod_reqd_oid")) = False Then
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) - " + SetDec(ds_detail.Tables("detail").Rows(n).Item("pod_qty").ToString) _
                                                                 & " where reqd_oid = '" + ds_detail.Tables("detail").Rows(n).Item("pod_reqd_oid").ToString + "'"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        End If

                                    End If
                                Next

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
                                                       " where wf_ref_code ~~* '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_code") + "'" + _
                                                       " and lower(wf_user_id) in (" + par_user_approval.ToLower + ")" + _
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

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_status = 'D' " _
                                                       & " where pod_po_oid = '" + par_ds.Tables("os").Rows(i).Item(par_initial + "_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'If par_ds.Tables("os").Rows(i).Item("useremail") <> "" Then
                                '    mf.sent_email(par_ds.Tables("os").Rows(i).Item("useremail"), "", mf.title_email(par_title, par_ds.Tables("os").Rows(i).Item(par_initial + "_code")), "Transaction Number : " + ds.Tables("os").Rows(i).Item(par_initial + "_code") + " Rollback Transaction By : " + master_new.ClsVar.sNama, master_new.ClsVar.sEmailSyspro, "")
                                'End If

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

  

    Private Sub CeAll_CheckedChanged(sender As Object, e As EventArgs) Handles CeAll.CheckedChanged
        Try
            For Each dr As DataRow In ds.Tables("os").Rows
                dr("status") = CeAll.Checked
            Next
            ds.AcceptChanges()
            gv_os.BestFitColumns()

        Catch ex As Exception

        End Try
    End Sub
End Class
