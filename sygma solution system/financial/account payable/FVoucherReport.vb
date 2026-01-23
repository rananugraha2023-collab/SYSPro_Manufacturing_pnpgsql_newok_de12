Imports npgsql
Imports master_new.ModFunction

Public Class FVoucherReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FDRCRMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier ID", "ap_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exc. Rate", "ap_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Date", "ap_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Faktur Pajak Number", "ap_fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Date", "ap_fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expected Date", "ap_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "ap_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "ap_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "ap_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "ap_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Prepayment IDR", "ap_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Account Code Prepayment", "ac_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name Prepayment", "ac_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks1", "ap_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks2", "ap_remarks2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "DPP", "ap_dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "ap_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "ap_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Exclude PPH", "ap_amount_exc_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Include PPH", "ap_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Payment", "ap_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Outstanding Payment", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total IDR", "ap_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Payment IDR", "ap_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Outstanding Payment IDR", "ap_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Status", "ap_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Remark", "ap_tax_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Approval Date", "ap_tax_apr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "ap_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ap_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ap_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ap_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_master, "app_ap_oid", False)
        add_column_copy(gv_master, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "apr_ap_oid", False)
        add_column_copy(gv_master, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "apr_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "apr_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Open", "apr_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Invoice", "apr_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PO Cost", "apr_po_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Invoice Cost", "apr_invoice_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Tot Cost", "tot_inv_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Close Line", "apr_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_master, "apd_ap_oid", False)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "apd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "apd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Status", "apd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
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
                & "  public.ap_mstr.ap_tax_inc, " _
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
                & "  public.ap_mstr.ap_trans_id, " _
                & "  public.ap_mstr.ap_tax_remarks, " _
                & "  public.ap_mstr.ap_tax_apr_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name,ap_ptnr_id, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.bk_mstr.bk_name, " _
                & "  credit_term_mstr.code_name as credit_terms_name, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  ap_type.code_name as ap_type_name, " _
                & "  taxclass_mstr.code_name as taxclass_name, " _
                & "  ap_dpp, ap_ppn, ap_pph, ap_dpp + ap_ppn as ap_amount_exc_pph,po_code, " _
                 & "  apr_oid, " _
            & "  apr_ap_oid, True as ceklist," _
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
            & "  taxclass_mstrd.code_name as taxclass_name, " _
            & "  apr_tax_inc, " _
            & "  apr_open, " _
            & "  apr_invoice, " _
            & "  apr_po_cost, " _
            & "  apr_gl_cost, " _
            & "  apr_invoice_cost, " _
            & "  apr_invoice_cost * apr_invoice as tot_inv_cost, " _
            & "  apr_close_line, " _
            & "  apr_dt, rcv_exc_rate " _
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
                & "  inner join public.app_po on ap_mstr.ap_oid = app_ap_oid " _
                & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid " _
                & "  inner join public.apr_rcv on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid " _
                & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
                & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
                & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
                & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                & "  inner join public.code_mstr taxclass_mstrd on public.apr_rcv.apr_tax_class_id = taxclass_mstrd.code_id "


        get_sequel += "  where  ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
              & " and ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
              & " and ap_en_id in (select user_en_id from tconfuserentity " _
              & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"



        Return get_sequel
    End Function

End Class
