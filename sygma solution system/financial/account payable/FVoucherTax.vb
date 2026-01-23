Imports npgsql
Imports master_new.ModFunction

Public Class FVoucherTax
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_po, ds_rcv, ds_dist As DataSet
    Dim _now As DateTime

    Private Sub FVoucherTax_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_all.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_all.ColumnFilterChanged, AddressOf relation_detail

        AddHandler gv_os.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_os.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wfs_status())
        le_status.Properties.DataSource = dt_bantu
        le_status.Properties.DisplayMember = dt_bantu.Columns("wfs_desc").ToString
        le_status.Properties.ValueMember = dt_bantu.Columns("wfs_id").ToString
        le_status.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Effective Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Exc. Rate", "ap_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Invoice Date", "ap_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Faktur Pajak Number", "ap_fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Faktur Pajak Date", "ap_fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Expected Date", "ap_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Taxable", "ap_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Type", "ap_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Prepayment", "ap_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "Prepayment IDR", "ap_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_os, "Account Code Prepayment", "ac_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Account Name Prepayment", "ac_code_prepaid", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_os, "Remarks1", "ap_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Remarks2", "ap_remarks2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "DPP", "ap_dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "PPN", "ap_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "PPH", "ap_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_os, "DPP IDR", "ap_dpp_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "PPN IDR", "ap_ppn_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "PPH IDR", "ap_pph_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_os, "Total Exclude PPH", "ap_amount_exc_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "Total Include PPH", "ap_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_os, "Total Payment", "ap_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "Eff. Date Payment", "appay_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "Outstanding Payment", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_os, "Total IDR", "ap_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_os, "Total Payment IDR", "ap_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_os, "Outstanding Payment IDR", "ap_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_os, "Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Tax Status", "ap_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Tax Remark", "ap_tax_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Tax Approval Date", "ap_tax_apr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_os, "User Create", "ap_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Date Create", "ap_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_os, "User Update", "ap_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_os, "Date Update", "ap_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Effective Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Exc. Rate", "ap_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Invoice Date", "ap_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Faktur Pajak Number", "ap_fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Faktur Pajak Date", "ap_fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Expected Date", "ap_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Taxable", "ap_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Type", "ap_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Prepayment", "ap_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "Prepayment IDR", "ap_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_all, "Account Code Prepayment", "ac_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Account Name Prepayment", "ac_code_prepaid", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_all, "Remarks1", "ap_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Remarks2", "ap_remarks2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "DPP", "ap_dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "PPN", "ap_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "PPH", "ap_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_all, "DPP IDR", "ap_dpp_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "PPN IDR", "ap_ppn_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "PPH IDR", "ap_pph_exc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_all, "Total Exclude PPH", "ap_amount_exc_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "Total Include PPH", "ap_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_all, "Total Payment", "ap_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "Eff. Date Payment", "appay_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "Outstanding Payment", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_all, "Total IDR", "ap_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_all, "Total Payment IDR", "ap_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_all, "Outstanding Payment IDR", "ap_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_all, "Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Tax Status", "ap_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Tax Remark", "ap_tax_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Tax Approval Date", "ap_tax_apr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_all, "User Create", "ap_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Date Create", "ap_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_all, "User Update", "ap_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_all, "Date Update", "ap_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_po, "app_ap_oid", False)
        add_column(gv_detail_po, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_receive, "apr_ap_oid", False)
        add_column(gv_detail_receive, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Taxable", "apr_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Tax Include", "apr_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_receive, "Qty Open", "apr_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_receive, "Qty Invoice", "apr_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_receive, "PO Cost", "apr_po_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_receive, "Invoice Cost", "apr_invoice_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_receive, "Close Line", "apr_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_dist, "apd_ap_oid", False)
        add_column(gv_detail_dist, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_dist, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_dist, "Remarks", "apd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_dist, "Amount", "apd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_dist, "Status", "apd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            .SQL = "SELECT  false as status, " _
                            & "  public.ap_mstr.ap_oid, " _
                            & "  public.ap_mstr.ap_dom_id, " _
                            & "  public.ap_mstr.ap_en_id, " _
                            & "  public.ap_mstr.ap_add_by, " _
                            & "  public.ap_mstr.ap_add_date, " _
                            & "  public.ap_mstr.ap_upd_by, " _
                            & "  public.ap_mstr.ap_upd_date, " _
                            & "  public.ap_mstr.ap_code, " _
                            & "  public.ap_mstr.ap_date, " _
                            & "  public.ap_mstr.ap_tax_date, " _
                            & "  public.ap_mstr.ap_ptnr_id, " _
                            & "  public.ap_mstr.ap_cu_id, " _
                            & "  public.ap_mstr.ap_exc_rate, " _
                            & "  public.ap_mstr.ap_bk_id, " _
                            & "  public.ap_mstr.ap_credit_term, " _
                            & "  public.ap_mstr.ap_eff_date, " _
                            & "  public.ap_mstr.ap_disc_date, " _
                            & "  public.ap_mstr.ap_expt_date, " _
                            & "  public.ap_mstr.ap_ap_ac_id, " _
                            & "  public.ap_mstr.ap_ap_sb_id, " _
                            & "  public.ap_mstr.ap_ap_cc_id, " _
                            & "  public.ap_mstr.ap_disc_ac_id, " _
                            & "  public.ap_mstr.ap_disc_sb_id, " _
                            & "  public.ap_mstr.ap_disc_cc_id, " _
                            & "  public.ap_mstr.ap_type, " _
                            & "  public.ap_mstr.ap_taxable, " _
                            & "  public.ap_mstr.ap_tax_class_id, " _
                            & "  public.ap_mstr.ap_pay_prepaid, " _
                            & "  public.ap_mstr.ap_pay_prepaid * ap_exc_rate as ap_pay_prepaid_idr, " _
                            & "  public.ap_mstr.ap_ac_prepaid, " _
                            & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                            & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                            & "  public.ap_mstr.ap_amount, " _
                            & "  public.ap_mstr.ap_pay_amount, " _
                            & "  public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount as ap_outstanding, " _
                            & "  public.ap_mstr.ap_amount * ap_exc_rate as ap_amount_idr, " _
                            & "  public.ap_mstr.ap_pay_amount * ap_exc_rate as ap_pay_amount_idr, " _
                            & "  (public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount) * ap_exc_rate as ap_outstanding_idr, " _
                            & "  public.ap_mstr.ap_remarks, ap_remarks2, " _
                            & "  public.ap_mstr.ap_status, " _
                            & "  public.ap_mstr.ap_dt, " _
                            & "  public.ap_mstr.ap_invoice, " _
                            & "  public.ap_mstr.ap_due_date, " _
                            & "  public.ap_mstr.ap_fp_code, " _
                            & "  public.ap_mstr.ap_fp_date, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.cu_mstr.cu_name, " _
                            & "  public.bk_mstr.bk_name, " _
                            & "  credit_term_mstr.code_name as credit_terms_name, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.cc_mstr.cc_desc, " _
                            & "  ap_type.code_name as ap_type_name, " _
                            & "  appay_eff_date, " _
                            & "  taxclass_mstr.code_name as taxclass_name, ap_tax_remarks, ap_tax_apr_date, " _
                            & "  ap_dpp, ap_ppn, ap_pph, ap_dpp + ap_ppn as ap_amount_exc_pph , ap_trans_id, " _
                            & "  ap_exc_rate * ap_dpp as ap_dpp_exc, ap_exc_rate * ap_ppn as ap_ppn_exc, ap_exc_rate * ap_pph as ap_pph_exc " _
                            & "FROM " _
                            & "  public.ap_mstr " _
                            & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.bk_mstr ON (public.ap_mstr.ap_bk_id = public.bk_mstr.bk_id) " _
                            & "  INNER JOIN public.cu_mstr ON (public.ap_mstr.ap_cu_id = public.cu_mstr.cu_id) " _
                            & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.ap_mstr.ap_credit_term = credit_term_mstr.code_id) " _
                            & "  INNER JOIN public.ac_mstr ON (public.ap_mstr.ap_ap_ac_id = public.ac_mstr.ac_id) " _
                            & "  INNER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ap_mstr.ap_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.ap_mstr.ap_ap_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.ap_mstr.ap_ap_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.code_mstr ap_type ON (public.ap_mstr.ap_type = ap_type.code_id) " _
                            & "  INNER JOIN public.code_mstr taxclass_mstr ON (public.ap_mstr.ap_tax_class_id = taxclass_mstr.code_id) " _
                            & "  left outer join appayd_det on appayd_ap_oid = ap_oid " _
                            & "  left outer join appay_payment on appay_oid = appayd_appay_oid " _
                            & " where  (coalesce(ap_trans_id,'') = '' or ap_trans_id = 'H') " _
                            & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        Else
                            .SQL = "SELECT  false as status, " _
                                & "  public.ap_mstr.ap_oid, " _
                                & "  public.ap_mstr.ap_dom_id, " _
                                & "  public.ap_mstr.ap_en_id, " _
                                & "  public.ap_mstr.ap_add_by, " _
                                & "  public.ap_mstr.ap_add_date, " _
                                & "  public.ap_mstr.ap_upd_by, " _
                                & "  public.ap_mstr.ap_upd_date, " _
                                & "  public.ap_mstr.ap_code, " _
                                & "  public.ap_mstr.ap_date, " _
                                & "  public.ap_mstr.ap_tax_date, " _
                                & "  public.ap_mstr.ap_ptnr_id, " _
                                & "  public.ap_mstr.ap_cu_id, " _
                                & "  public.ap_mstr.ap_exc_rate, " _
                                & "  public.ap_mstr.ap_bk_id, " _
                                & "  public.ap_mstr.ap_credit_term, " _
                                & "  public.ap_mstr.ap_eff_date, " _
                                & "  public.ap_mstr.ap_disc_date, " _
                                & "  public.ap_mstr.ap_expt_date, " _
                                & "  public.ap_mstr.ap_ap_ac_id, " _
                                & "  public.ap_mstr.ap_ap_sb_id, " _
                                & "  public.ap_mstr.ap_ap_cc_id, " _
                                & "  public.ap_mstr.ap_disc_ac_id, " _
                                & "  public.ap_mstr.ap_disc_sb_id, " _
                                & "  public.ap_mstr.ap_disc_cc_id, " _
                                & "  public.ap_mstr.ap_type, " _
                                & "  public.ap_mstr.ap_taxable, " _
                                & "  public.ap_mstr.ap_tax_class_id, " _
                                & "  public.ap_mstr.ap_pay_prepaid, " _
                                & "  public.ap_mstr.ap_pay_prepaid * ap_exc_rate as ap_pay_prepaid_idr, " _
                                & "  public.ap_mstr.ap_ac_prepaid, " _
                                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                                & "  public.ap_mstr.ap_amount, " _
                                & "  public.ap_mstr.ap_pay_amount, " _
                                & "  public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount as ap_outstanding, " _
                                & "  public.ap_mstr.ap_amount * ap_exc_rate as ap_amount_idr, " _
                                & "  public.ap_mstr.ap_pay_amount * ap_exc_rate as ap_pay_amount_idr, " _
                                & "  (public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount) * ap_exc_rate as ap_outstanding_idr, " _
                                & "  public.ap_mstr.ap_remarks, ap_remarks2, " _
                                & "  public.ap_mstr.ap_status, " _
                                & "  public.ap_mstr.ap_dt, " _
                                & "  public.ap_mstr.ap_invoice, " _
                                & "  public.ap_mstr.ap_due_date, " _
                                & "  public.ap_mstr.ap_fp_code, " _
                                & "  public.ap_mstr.ap_fp_date, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.cu_mstr.cu_name, " _
                                & "  public.bk_mstr.bk_name, " _
                                & "  credit_term_mstr.code_name as credit_terms_name, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.cc_mstr.cc_desc, " _
                                & "  ap_type.code_name as ap_type_name, " _
                                & "  appay_eff_date, " _
                                & "  taxclass_mstr.code_name as taxclass_name, ap_trans_id, ap_tax_remarks , " _
                                & "  ap_dpp, ap_ppn, ap_pph, ap_dpp + ap_ppn as ap_amount_exc_pph, " _
                                & "  ap_exc_rate * ap_dpp as ap_dpp_exc, ap_exc_rate * ap_ppn as ap_ppn_exc, ap_exc_rate * ap_pph as ap_pph_exc " _
                                & "FROM " _
                                & "  public.ap_mstr " _
                                & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.bk_mstr ON (public.ap_mstr.ap_bk_id = public.bk_mstr.bk_id) " _
                                & "  INNER JOIN public.cu_mstr ON (public.ap_mstr.ap_cu_id = public.cu_mstr.cu_id) " _
                                & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.ap_mstr.ap_credit_term = credit_term_mstr.code_id) " _
                                & "  INNER JOIN public.ac_mstr ON (public.ap_mstr.ap_ap_ac_id = public.ac_mstr.ac_id) " _
                                & "  INNER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ap_mstr.ap_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                                & "  INNER JOIN public.sb_mstr ON (public.ap_mstr.ap_ap_sb_id = public.sb_mstr.sb_id) " _
                                & "  INNER JOIN public.cc_mstr ON (public.ap_mstr.ap_ap_cc_id = public.cc_mstr.cc_id) " _
                                & "  INNER JOIN public.code_mstr ap_type ON (public.ap_mstr.ap_type = ap_type.code_id) " _
                                & "  INNER JOIN public.code_mstr taxclass_mstr ON (public.ap_mstr.ap_tax_class_id = taxclass_mstr.code_id) " _
                                & "  left outer join appayd_det on appayd_ap_oid = ap_oid " _
                                & "  left outer join appay_payment on appay_oid = appayd_appay_oid " _
                                & " where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                        'untuk ambil wf_mstr table
                        load_data_grid_detail()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub load_data_grid_detail()
        ds_po = New DataSet
        ds_rcv = New DataSet
        ds_dist = New DataSet

        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If
            Try
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "  app_oid, " _
                            & "  app_ap_oid, " _
                            & "  app_po_oid, " _
                            & "  po_code " _
                            & "FROM  " _
                            & "  public.app_po  " _
                            & "  inner join public.ap_mstr on ap_mstr.ap_oid = app_ap_oid" _
                            & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid" _
                            & "  where coalesce(ap_trans_id,'') = '' " _
                            & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_po, "po")
                        gc_detail_po.DataSource = ds_po.Tables("po")
                        gv_detail_po.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  apr_oid, " _
                            & "  apr_ap_oid, " _
                            & "  apr_seq, " _
                            & "  apr_rcvd_oid, " _
                            & "  rcv_code, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pod_pt_desc1, " _
                            & "  pod_pt_desc2, " _
                            & "  apr_taxable, " _
                            & "  apr_tax_class_id, " _
                            & "  code_name as taxclass_name, " _
                            & "  apr_tax_inc, " _
                            & "  apr_open, " _
                            & "  apr_invoice, " _
                            & "  apr_po_cost, " _
                            & "  apr_gl_cost, " _
                            & "  apr_invoice_cost, " _
                            & "  apr_close_line, " _
                            & "  apr_dt " _
                            & "FROM  " _
                            & "  public.apr_rcv " _
                            & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
                            & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
                            & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
                            & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.code_mstr on public.apr_rcv.apr_tax_class_id = public.code_mstr.code_id" _
                            & "  inner join public.ap_mstr on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid" _
                            & "  where coalesce(ap_trans_id,'') = '' " _
                            & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_rcv, "rcv")
                        gc_detail_receive.DataSource = ds_rcv.Tables("rcv")
                        gv_detail_receive.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.apd_dist.apd_oid, " _
                            & "  public.apd_dist.apd_ap_oid, " _
                            & "  public.apd_dist.apd_tax_distribution, " _
                            & "  public.apd_dist.apd_taxable, " _
                            & "  public.apd_dist.apd_tax_inc, " _
                            & "  public.apd_dist.apd_tax_class_id, " _
                            & "  public.apd_dist.apd_ac_id, " _
                            & "  public.apd_dist.apd_sb_id, " _
                            & "  public.apd_dist.apd_cc_id, " _
                            & "  public.apd_dist.apd_amount, " _
                            & "  public.apd_dist.apd_remarks, " _
                            & "  public.apd_dist.apd_dt, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.cc_mstr.cc_desc " _
                            & "FROM " _
                            & "  public.apd_dist " _
                            & "  INNER JOIN public.ap_mstr ON (public.apd_dist.apd_ap_oid = public.ap_mstr.ap_oid) " _
                            & "  INNER JOIN public.ac_mstr ON (public.apd_dist.apd_ac_id = public.ac_mstr.ac_id) " _
                            & "  left outer join public.sb_mstr ON (public.apd_dist.apd_sb_id = public.sb_mstr.sb_id) " _
                            & "  left outer join public.cc_mstr ON (public.apd_dist.apd_cc_id = public.cc_mstr.cc_id) " _
                            & "  left outer join public.code_mstr ON (public.apd_dist.apd_tax_class_id = public.code_mstr.code_id)" _
                            & "  where coalesce(ap_trans_id,'') = '' " _
                            & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_dist, "dist")
                        gc_detail_dist.DataSource = ds_dist.Tables("dist")
                        gv_detail_dist.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            If ds.Tables("all").Rows.Count = 0 Then
                Exit Sub
            End If
            Try
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "SELECT  " _
                            & "  app_oid, " _
                            & "  app_ap_oid, " _
                            & "  app_po_oid, " _
                            & "  po_code " _
                            & "FROM  " _
                            & "  public.app_po  " _
                            & "  inner join public.ap_mstr on ap_mstr.ap_oid = app_ap_oid" _
                            & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid" _
                            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_po, "po")
                        gc_detail_po.DataSource = ds_po.Tables("po")
                        gv_detail_po.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  apr_oid, " _
                            & "  apr_ap_oid, " _
                            & "  apr_seq, " _
                            & "  apr_rcvd_oid, " _
                            & "  rcv_code, " _
                            & "  pt_code, " _
                            & "  pod_pt_desc1, " _
                            & "  pod_pt_desc2, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  apr_taxable, " _
                            & "  apr_tax_class_id, " _
                            & "  code_name as taxclass_name, " _
                            & "  apr_tax_inc, " _
                            & "  apr_open, " _
                            & "  apr_invoice, " _
                            & "  apr_po_cost, " _
                            & "  apr_gl_cost, " _
                            & "  apr_invoice_cost, " _
                            & "  apr_close_line, " _
                            & "  apr_dt " _
                            & "FROM  " _
                            & "  public.apr_rcv " _
                            & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
                            & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
                            & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
                            & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.code_mstr on public.apr_rcv.apr_tax_class_id = public.code_mstr.code_id" _
                            & "  inner join public.ap_mstr on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid" _
                            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_rcv, "rcv")
                        gc_detail_receive.DataSource = ds_rcv.Tables("rcv")
                        gv_detail_receive.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  public.apd_dist.apd_oid, " _
                            & "  public.apd_dist.apd_ap_oid, " _
                            & "  public.apd_dist.apd_tax_distribution, " _
                            & "  public.apd_dist.apd_taxable, " _
                            & "  public.apd_dist.apd_tax_inc, " _
                            & "  public.apd_dist.apd_tax_class_id, " _
                            & "  public.apd_dist.apd_ac_id, " _
                            & "  public.apd_dist.apd_sb_id, " _
                            & "  public.apd_dist.apd_cc_id, " _
                            & "  public.apd_dist.apd_amount, " _
                            & "  public.apd_dist.apd_remarks, " _
                            & "  public.apd_dist.apd_dt, " _
                            & "  public.ac_mstr.ac_code, " _
                            & "  public.ac_mstr.ac_name, " _
                            & "  public.sb_mstr.sb_desc, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.cc_mstr.cc_desc " _
                            & "FROM " _
                            & "  public.apd_dist " _
                            & "  INNER JOIN public.ap_mstr ON (public.apd_dist.apd_ap_oid = public.ap_mstr.ap_oid) " _
                            & "  INNER JOIN public.ac_mstr ON (public.apd_dist.apd_ac_id = public.ac_mstr.ac_id) " _
                            & "  left outer join public.sb_mstr ON (public.apd_dist.apd_sb_id = public.sb_mstr.sb_id) " _
                            & "  left outer join public.cc_mstr ON (public.apd_dist.apd_cc_id = public.cc_mstr.cc_id) " _
                            & "  left outer join public.code_mstr ON (public.apd_dist.apd_tax_class_id = public.code_mstr.code_id)" _
                            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & " and ap_en_id in (select user_en_id from tconfuserentity " _
                                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_dist, "dist")
                        gc_detail_dist.DataSource = ds_dist.Tables("dist")
                        gv_detail_dist.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Public Overrides Sub relation_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_detail_po.Columns("app_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("app_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                gv_detail_po.BestFitColumns()

                gv_detail_receive.Columns("apr_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apr_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                gv_detail_receive.BestFitColumns()

                gv_detail_dist.Columns("apd_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apd_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                gv_detail_dist.BestFitColumns()

                'gv_detail_po.Columns("app_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("app_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                'gv_detail_po.BestFitColumns()

                'gv_detail_receive.Columns("apr_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apr_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                'gv_detail_receive.BestFitColumns()

                'gv_detail_dist.Columns("apd_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apd_ap_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid").ToString & "'")
                'gv_detail_dist.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail_po.Columns("app_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("app_ap_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ap_oid").ToString & "'")
                gv_detail_po.BestFitColumns()

                gv_detail_receive.Columns("apr_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apr_ap_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ap_oid").ToString & "'")
                gv_detail_receive.BestFitColumns()

                gv_detail_dist.Columns("apd_ap_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apd_ap_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ap_oid").ToString & "'")
                gv_detail_dist.BestFitColumns()

                'gv_detail_po.Columns("app_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ap_oid"))
                'gv_detail_po.BestFitColumns()

                'gv_detail_receive.Columns("apr_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("ap_oid"))
                'gv_detail_receive.BestFitColumns()

                'gv_detail_dist.Columns("apd_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("ap_oid"))
                'gv_detail_dist.BestFitColumns()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub xtc_master_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles xtc_master.SelectedPageChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
            sb_process.Enabled = True
        Else
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
            sb_process.Enabled = False
        End If
        relation_detail()
    End Sub

    Private Sub sb_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_process.Click
        Dim i, j As Integer
        j = 0

        If xtc_master.SelectedTabPageIndex = 0 Then
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

        If le_status.EditValue = 1 Then
            process_approve()
        ElseIf le_status.EditValue = 2 Then
            process_hold()
        End If
    End Sub

    Private Sub process_approve()
        If MessageBox.Show("Approve Data Selection..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
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

                        ds.Tables("os").AcceptChanges()
                        For i = 0 To ds.Tables("os").Rows.Count - 1
                            If ds.Tables("os").Rows(i).Item("status") = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update ap_mstr set ap_trans_id = 'A', " + _
                                                       " ap_tax_remarks = " + SetSetring(txt_remarks_next.Text) + _
                                                       " where ap_oid = '" + ds.Tables("os").Rows(i).Item("ap_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                ssqls.Add(.Command.CommandText)
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = insert_log("Approve Voucher Tax " & ds.Tables("os").Rows(i).Item("ap_code").ToString)
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
                        MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub process_hold()
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

                        ds.Tables("os").AcceptChanges()
                        For i = 0 To ds.Tables("os").Rows.Count - 1
                            If ds.Tables("os").Rows(i).Item("status") = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update ap_mstr set ap_trans_id = 'H', " + _
                                                       " ap_tax_remarks = " + SetSetring(txt_remarks_next.Text) + ", " + _
                                                       " ap_tax_apr_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " + _
                                                       " where ap_oid = '" + ds.Tables("os").Rows(i).Item("ap_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()



                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = insert_log("Hold Voucher Tax " & ds.Tables("os").Rows(i).Item("ap_code").ToString)
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
                        MessageBox.Show("Selamat " + master_new.ClsVar.sNama + ", Data Telah Diprocess..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

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
