Imports npgsql
Imports master_new.ModFunction

Public Class FSalesQuotationReportAlocated
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesQuotationReportAlocated_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SQ Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi Po No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Booking", "sq_booking", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Payment", "sq_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "sq_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Package", "sq_is_package", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price", "sq_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total", "sq_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "sq_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "sq_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "After Tax", "sq_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ext. Total", "sq_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPN", "sq_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPH", "sq_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. After Tax", "sq_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "User Create", "sq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sq_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sq_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_master, "sqd_sq_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Booking", "sqd_qty_booking", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Transfer", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  sq_oid, " _
            & "  sq_dom_id, " _
            & "  sq_en_id, " _
            & "  sq_add_by, " _
            & "  sq_add_date, " _
            & "  sq_upd_by, " _
            & "  sq_upd_date, " _
            & "  sq_code, " _
            & "  sq_ptnr_id_sold, " _
            & "  sq_ptnr_id_bill, " _
            & "  sq_ref_po_code, " _
            & "  sq_ref_po_oid, " _
            & "  sq_date, " _
            & "  sq_si_id, " _
            & "  sq_type, " _
            & "  sq_sales_person, " _
            & "  sq_pi_id, " _
            & "  sq_pay_type, " _
            & "  sq_pay_method, " _
            & "  sq_ar_ac_id, " _
            & "  sq_ar_sb_id, " _
            & "  sq_ar_cc_id, " _
            & "  sq_dp, " _
            & "  sq_disc_header, " _
            & "  sq_total, " _
            & "  sq_print_count, " _
            & "  sq_need_date, " _
            & "  sq_cons, " _
            & "  sq_booking, " _
            & "  sq_close_date, " _
            & "  sq_tran_id, " _
            & "  sq_trans_id, " _
            & "  sq_trans_rmks, " _
            & "  sq_current_route, " _
            & "  sq_next_route, " _
            & "  sq_dt, " _
            & "  sq_cu_id, " _
            & "  sq_total_ppn, " _
            & "  sq_total_pph, " _
            & "  sq_payment, " _
            & "  sq_exc_rate, " _
            & "  (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax, " _
            & "  sq_exc_rate * sq_total as sq_total_ext,  sq_exc_rate * sq_total_ppn as sq_total_ppn_ext, " _
            & "  sq_exc_rate * sq_total_pph as sq_total_pph_ext,  sq_exc_rate * (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax_ext, " _
            & "  en_desc, " _
            & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
            & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
            & "  si_desc, " _
            & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
            & "  pi_desc, " _
            & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
            & "  pay_method.code_name as pay_method_name, " _
            & "  ac_mstr_ar.ac_code, " _
            & "  ac_mstr_ar.ac_name, " _
            & "  sb_mstr_ar.sb_desc, " _
            & "  cc_mstr_ar.cc_desc,sq_pt_id,coalesce(sq_is_package,'N') as sq_is_package,pt_code,pt_desc1,pt_desc2,sq_price, " _
            & "  cu_name, " _
            & "  tran_name, " _
            & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3, " _
            & "  sqd_oid, " _
            & "  sqd_dom_id, " _
            & "  sqd_en_id, " _
            & "  sqd_add_by, " _
            & "  sqd_add_date, " _
            & "  sqd_upd_by, " _
            & "  sqd_upd_date, " _
            & "  sqd_sq_oid, " _
            & "  sqd_seq, " _
            & "  sqd_is_additional_charge, " _
            & "  sqd_si_id, " _
            & "  sqd_pt_id, " _
            & "  sqd_rmks, " _
            & "  sqd_qty, " _
            & "  sqd_qty_transfer, " _
            & "  sqd_qty_so, " _
            & "  sqd_um, " _
            & "  sqd_cost, " _
            & "  sqd_price, " _
            & "  sqd_disc, " _
            & "  sqd_sales_ac_id, " _
            & "  sqd_sales_sb_id, " _
            & "  sqd_sales_cc_id, " _
            & "  sqd_disc_ac_id, " _
            & "  sqd_um_conv, " _
            & "  sqd_qty_real, " _
            & "  sqd_taxable, " _
            & "  sqd_tax_inc, " _
            & "  sqd_tax_class, " _
            & "  sqd_status, " _
            & "  sqd_dt, " _
            & "  sqd_payment, " _
            & "  sqd_dp, " _
            & "  sqd_sales_unit, " _
            & "  sqd_loc_id, " _
            & "  sqd_serial, " _
            & "  en_desc, " _
            & "  si_desc, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_mstr.code_name as um_name, " _
            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
            & "  sb_mstr_sales.sb_desc, " _
            & "  cc_mstr_sales.cc_desc, " _
            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
            & "  tax_class.code_name as sqd_tax_class_name, " _
            & "  sqd_ppn_type, " _
            & "  loc_desc " _
            & "FROM  " _
            & "  public.sq_mstr " _
            & "  inner join en_mstr on en_id = sq_en_id " _
            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
            & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
            & "  inner join si_mstr on si_id = sq_si_id " _
            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
            & "  inner join pi_mstr on pi_id = sq_pi_id " _
            & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
            & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
            & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
            & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
            & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
            & "  inner join cu_mstr on cu_id = sq_cu_id " _
            & "  left outer join tran_mstr on tran_id = sq_tran_id" _
            & "  inner join sqd_det on sq_oid = sqd_sq_oid " _
            & "  inner join pt_mstr on pt_id = sqd_pt_id " _
            & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
            & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
            & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
            & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date)

        Return get_sequel
    End Function
    '& " where sq_cons='Y' and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
End Class
