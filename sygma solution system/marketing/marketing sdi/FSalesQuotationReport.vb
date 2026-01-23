Imports npgsql
Imports master_new.ModFunction

Public Class FSalesQuotationReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()

        '    add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        '    add_column_copy(gv_master, "SQ Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Referensi Po No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        '    add_column_copy(gv_master, "Payment", "sq_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Payment Date", "sq_need_date", DevExpress.Utils.HorzAlignment.Center)
        '    add_column_copy(gv_master, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        '    add_column_copy(gv_master, "Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Is Package", "sq_is_package", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Price", "sq_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Total", "sq_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "PPN", "sq_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "PPH", "sq_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "After Tax", "sq_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Ext. Total", "sq_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "Ext. PPN", "sq_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "Ext. PPH", "sq_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "Ext. After Tax", "sq_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "User Create", "sq_add_by", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Date Create", "sq_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        '    add_column_copy(gv_master, "User Update", "sq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Date Update", "sq_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        '    add_column(gv_master, "sqd_sq_oid", False)
        '    add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "Qty Transfer", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        '    add_column_copy(gv_master, "Qty Outstanding", "sqd_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        '    add_column_copy(gv_master, "Qty Outstanding Price", "sqd_qty_outstanding_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        '    add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        '    add_column_copy(gv_master, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        '    add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        '    add_column_copy(gv_master, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)

        'Private Sub gv_master()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "oid", "sqd_invc_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Booking", "sq_booking", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Booking", "sqd_qty_booking", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "SQ Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "SO Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sq Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Transfer Code", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Status", "ptsfr_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Transfer  ", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Shipment", "sqd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "SO Return", "so_return", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Qty Allocated  ", "sqd_qty_allocated", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Referensi PO No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Total Before Disc", "sqd_total_before", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total After Disc", "sqd_total_after", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Code Header", "ac_code_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name Header", "ac_name_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Taxable", "sq_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Tax Include", "sq_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Payment", "sq_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "sq_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_detail, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Sales Unit", "sq_sales_unit", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Total", "sq_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "PPN", "sq_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "PPH", "sq_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "After Tax", "sq_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail, "Ext. Total", "sq_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Ext. PPN", "sq_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Ext. PPH", "sq_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Ext. After Tax", "sq_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail, "Additional Charge", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "Account Code Detail", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name Detail", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account Detail", "sb_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center Detail", "cc_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Sales Comission", "sq_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_detail, "User Create", "sq_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Date Create", "sq_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_detail, "User Update", "sq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Date Update", "sq_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        'get_sequel = "SELECT  " _
        '    & "  sq_oid, " _
        '    & "  sq_dom_id, " _
        '    & "  sq_en_id, " _
        '    & "  sq_add_by, " _
        '    & "  sq_add_date, " _
        '    & "  sq_upd_by, " _
        '    & "  sq_upd_date, " _
        '    & "  sq_code, " _
        '    & "  sq_ptnr_id_sold, " _
        '    & "  sq_ptnr_id_bill, " _
        '    & "  sq_ref_po_code, " _
        '    & "  sq_ref_po_oid, " _
        '    & "  sq_date, " _
        '    & "  sq_si_id, " _
        '    & "  sq_type, " _
        '    & "  sq_sales_person, " _
        '    & "  sq_pi_id, " _
        '    & "  sq_pay_type, " _
        '    & "  sq_pay_method, " _
        '    & "  sq_ar_ac_id, " _
        '    & "  sq_ar_sb_id, " _
        '    & "  sq_ar_cc_id, " _
        '    & "  sq_dp, " _
        '    & "  sq_disc_header, " _
        '    & "  sq_total, " _
        '    & "  sq_print_count, " _
        '    & "  sq_need_date, " _
        '    & "  sq_cons, " _
        '    & "  sq_close_date, " _
        '    & "  sq_tran_id, " _
        '    & "  sq_trans_id, " _
        '    & "  sq_trans_rmks, " _
        '    & "  sq_current_route, " _
        '    & "  sq_next_route, " _
        '    & "  sq_dt, " _
        '    & "  sq_cu_id, " _
        '    & "  sq_total_ppn, " _
        '    & "  sq_total_pph, " _
        '    & "  sq_payment, " _
        '    & "  sq_exc_rate, " _
        '    & "  (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax, " _
        '    & "  sq_exc_rate * sq_total as sq_total_ext,  sq_exc_rate * sq_total_ppn as sq_total_ppn_ext, " _
        '    & "  sq_exc_rate * sq_total_pph as sq_total_pph_ext,  sq_exc_rate * (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax_ext, " _
        '    & "  en_desc, " _
        '    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
        '    & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
        '    & "  si_desc, " _
        '    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
        '    & "  pi_desc, " _
        '    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
        '    & "  pay_method.code_name as pay_method_name, " _
        '    & "  ac_mstr_ar.ac_code, " _
        '    & "  ac_mstr_ar.ac_name, " _
        '    & "  sb_mstr_ar.sb_desc, " _
        '    & "  cc_mstr_ar.cc_desc,sq_pt_id,coalesce(sq_is_package,'N') as sq_is_package,pt_code,pt_desc1,pt_desc2,sq_price, " _
        '    & "  cu_name, " _
        '    & "  tran_name, " _
        '    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3, " _
        '    & "  sqd_oid, " _
        '    & "  sqd_dom_id, " _
        '    & "  sqd_en_id, " _
        '    & "  sqd_add_by, " _
        '    & "  sqd_add_date, " _
        '    & "  sqd_upd_by, " _
        '    & "  sqd_upd_date, " _
        '    & "  sqd_sq_oid, " _
        '    & "  sqd_seq, " _
        '    & "  sqd_is_additional_charge, " _
        '    & "  sqd_si_id, " _
        '    & "  sqd_pt_id, " _
        '    & "  sqd_rmks, " _
        '    & "  sqd_qty, " _
        '    & "  sqd_qty_transfer, " _
        '    & "  sqd_qty_so,sqd_qty - coalesce(sqd_qty_transfer,0) - coalesce(sqd_qty_so,0) as sqd_qty_outstanding, " _
        '    & "  sqd_um,(sqd_qty - coalesce(sqd_qty_transfer,0) - coalesce(sqd_qty_so,0))* sqd_price as sqd_qty_outstanding_price, " _
        '    & "  sqd_cost, " _
        '    & "  sqd_price, " _
        '    & "  sqd_disc, " _
        '    & "  sqd_sales_ac_id, " _
        '    & "  sqd_sales_sb_id, " _
        '    & "  sqd_sales_cc_id, " _
        '    & "  sqd_disc_ac_id, " _
        '    & "  sqd_um_conv, " _
        '    & "  sqd_qty_real, " _
        '    & "  sqd_taxable, " _
        '    & "  sqd_tax_inc, " _
        '    & "  sqd_tax_class, " _
        '    & "  sqd_status, " _
        '    & "  sqd_dt, " _
        '    & "  sqd_payment, " _
        '    & "  sqd_dp, " _
        '    & "  sqd_sales_unit, " _
        '    & "  sqd_loc_id, " _
        '    & "  sqd_serial, " _
        '    & "  en_desc, " _
        '    & "  si_desc, " _
        '    & "  pt_code, " _
        '    & "  pt_desc1, " _
        '    & "  pt_desc2, " _
        '    & "  um_mstr.code_name as um_name, " _
        '    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
        '    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
        '    & "  sb_mstr_sales.sb_desc, " _
        '    & "  cc_mstr_sales.cc_desc, " _
        '    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
        '    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
        '    & "  tax_class.code_name as sqd_tax_class_name, " _
        '    & "  sqd_ppn_type, " _
        '    & "  loc_desc " _
        '    & "FROM  " _
        '    & "  public.sq_mstr " _
        '    & "  inner join en_mstr on en_id = sq_en_id " _
        '    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
        '    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
        '    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
        '    & "  inner join si_mstr on si_id = sq_si_id " _
        '    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
        '    & "  inner join pi_mstr on pi_id = sq_pi_id " _
        '    & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
        '    & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
        '    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
        '    & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
        '    & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
        '    & "  inner join cu_mstr on cu_id = sq_cu_id " _
        '    & "  left outer join tran_mstr on tran_id = sq_tran_id" _
        '    & "  inner join sqd_det on sq_oid = sqd_sq_oid " _
        '    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
        '    & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
        '    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
        '    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
        '    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
        '    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
        '    & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
        '    & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
        '    & " where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        get_sequel = "SELECT DISTINCT " _
                    & "public.sq_mstr.sq_oid, " _
                    & "       public.sq_mstr.sq_dom_id, " _
                    & "       public.sq_mstr.sq_en_id, " _
                    & "       public.en_mstr.en_desc, " _
                    & "       public.sq_mstr.sq_add_by, " _
                    & "       public.sq_mstr.sq_add_date, " _
                    & "       public.sq_mstr.sq_upd_by, " _
                    & "       public.sq_mstr.sq_upd_date, " _
                    & "       public.sq_mstr.sq_code, " _
                    & "       public.sq_mstr.sq_date, " _
                    & "       public.sq_mstr.sq_si_id, " _
                    & "       public.si_mstr.si_desc, " _
                    & "       public.sq_mstr.sq_ptnr_id_sold, " _
                    & "       ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "       public.sq_mstr.sq_ptnr_id_bill, " _
                    & "       ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                    & "       public.ptnrg_grp.ptnrg_name, " _
                    & "       public.sq_mstr.sq_ref_po_code, " _
                    & "       public.sq_mstr.sq_ref_po_oid, " _
                    & "       public.sq_mstr.sq_sales_person, " _
                    & "       ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                    & "       public.sq_mstr.sq_type, " _
                    & "       public.sq_mstr.sq_taxable, " _
                    & "       public.sq_mstr.sq_tax_class, " _
                    & "       public.sq_mstr.sq_pi_id, " _
                    & "       public.pi_mstr.pi_desc, " _
                    & "       public.sq_mstr.sq_credit_term, " _
                    & "       credit_term.code_name as credit_term_name, " _
                    & "       public.sq_mstr.sq_cu_id, " _
                    & "       cu_name, " _
                    & "       public.sq_mstr.sq_pay_type, " _
                    & "       pay_type.code_name as pay_type_name, " _
                    & "       coalesce(pay_type.code_usr_1, '-1') as pay_interval, " _
                    & "       public.sq_mstr.sq_pay_method, " _
                    & "       pay_method.code_name as pay_method_name, " _
                    & "       public.sq_mstr.sq_dp, " _
                    & "       public.sq_mstr.sq_disc_header, " _
                    & "       public.sq_mstr.sq_total, " _
                    & "       public.sq_mstr.sq_print_count, " _
                    & "       public.sq_mstr.sq_due_date, " _
                    & "       public.sq_mstr.sq_close_date, " _
                    & "       public.sq_mstr.sq_tran_id, " _
                    & "       public.sq_mstr.sq_trans_id, " _
                    & "       public.sq_mstr.sq_trans_rmks, " _
                    & "       public.sq_mstr.sq_current_route, " _
                    & "       public.sq_mstr.sq_next_route, " _
                    & "       public.sq_mstr.sq_dt, " _
                    & "       public.sq_mstr.sq_bk_appr, " _
                    & "       public.sq_mstr.sq_total_ppn, " _
                    & "       public.sq_mstr.sq_total_pph, " _
                    & "       public.sq_mstr.sq_payment, " _
                    & "       public.sq_mstr.sq_exc_rate, " _
                    & "       (sqd_qty_booking * sqd_price) as sqd_total_before, " _
                    & "       (sqd_qty_booking * sqd_price * sqd_disc) as sqd_total_after, " _
                    & "       public.sq_mstr.sq_tax_inc, " _
                    & "       public.sq_mstr.sq_cons, " _
                    & "       public.sq_mstr.sq_booking, " _
                    & "       public.sq_mstr.sq_book_start_date, " _
                    & "       public.sq_mstr.sq_book_end_date, " _
                    & "       public.sq_mstr.sq_terbilang, " _
                    & "       public.sq_mstr.sq_bk_id, " _
                    & "       public.sq_mstr.sq_interval, " _
                    & "       public.sq_mstr.sq_ppn_type, " _
                    & "       public.sq_mstr.sq_ac_prepaid, " _
                    & "       public.sq_mstr.sq_pay_prepaod, " _
                    & "       public.sq_mstr.sq_ar_ac_id, " _
                    & "       public.sq_mstr.sq_ar_sb_id, " _
                    & "       public.sq_mstr.sq_ar_cc_id, " _
                    & "       public.sq_mstr.sq_need_date, " _
                    & "       public.sq_mstr.sq_payment_date, " _
                    & "       public.sq_mstr.sq_last_transaction, " _
                    & "       public.sq_mstr.sq_is_package, " _
                    & "       public.sq_mstr.sq_pt_id, " _
                    & "       public.sq_mstr.sq_price, " _
                    & "       public.sq_mstr.sq_sales_program, " _
                    & "       public.sq_mstr.sq_status_produk, " _
                    & "       public.sq_mstr.sq_diskon_produk, " _
                    & "       public.sq_mstr.sq_unique_code, " _
                    & "       public.sq_mstr.sq_dp_unique, " _
                    & "       public.sq_mstr.sq_payment_unique, " _
                    & "       public.sq_mstr.sq_alocated, " _
                    & "       public.sq_mstr.sq_book_status, " _
                    & "       public.sq_mstr.sq_quo_type, " _
                    & "       public.sq_mstr.sq_project, " _
                    & "       public.sq_mstr.sq_shipping_charges, " _
                    & "       public.sq_mstr.sq_total_final, " _
                    & "       public.sq_mstr.sq_indent, " _
                    & "       public.sq_mstr.sq_manufacture, " _
                    & "       public.sq_mstr.sq_ptsfr_loc_id, " _
                    & "       public.sq_mstr.sq_ptsfr_loc_to_id, " _
                    & "       public.sq_mstr.sq_ptsfr_loc_git, " _
                    & "       public.sq_mstr.sq_en_to_id, " _
                    & "       public.sq_mstr.sq_si_to_id, " _
                    & "       public.sqd_det.sqd_oid, " _
                    & "       public.sqd_det.sqd_dom_id, " _
                    & "       public.sqd_det.sqd_en_id, " _
                    & "       public.sqd_det.sqd_add_by, " _
                    & "       public.sqd_det.sqd_add_date, " _
                    & "       public.sqd_det.sqd_upd_by, " _
                    & "       public.sqd_det.sqd_upd_date, " _
                    & "       public.sqd_det.sqd_sq_oid, " _
                    & "       public.sqd_det.sqd_seq, " _
                    & "       public.sqd_det.sqd_is_additional_charge, " _
                    & "       public.sqd_det.sqd_si_id, " _
                    & "       public.sqd_det.sqd_pt_id, " _
                    & "       public.pt_mstr.pt_code, " _
                    & "       public.pt_mstr.pt_desc1, " _
                    & "       public.pt_mstr.pt_desc2, " _
                    & "       public.sqd_det.sqd_rmks, " _
                    & "       public.sqd_det.sqd_qty, " _
                    & "       public.sqd_det.sqd_qty_booking, " _
                    & "       public.sqd_det.sqd_qty_transfer, " _
                    & "       public.sqd_det.sqd_qty_allocated, " _
                    & "       public.sqd_det.sqd_qty_so, " _
                    & "       public.sqd_det.sqd_um, " _
                    & "       um_mstr.code_name as um_name, " _
                    & "       public.sqd_det.sqd_qty_picked, " _
                    & "       public.sqd_det.sqd_qty_shipment, " _
                    & "       public.sqd_det.sqd_qty_pending_inv, " _
                    & "       public.sqd_det.sqd_qty_invoice, " _
                    & "       public.sqd_det.sqd_cost, " _
                    & "       public.sqd_det.sqd_price, " _
                    & "       public.sqd_det.sqd_disc, " _
                    & "       public.sqd_det.sqd_sales_ac_id, " _
                    & "       public.sqd_det.sqd_sales_sb_id, " _
                    & "       public.sqd_det.sqd_sales_cc_id, " _
                    & "       public.sqd_det.sqd_disc_ac_id, " _
                    & "       public.sqd_det.sqd_um_conv, " _
                    & "       public.sqd_det.sqd_qty_real, " _
                    & "       public.sqd_det.sqd_taxable, " _
                    & "       public.sqd_det.sqd_tax_inc, " _
                    & "       public.sqd_det.sqd_tax_class, " _
                    & "       public.sqd_det.sqd_status, " _
                    & "       public.sqd_det.sqd_dt, " _
                    & "       public.sqd_det.sqd_payment, " _
                    & "       public.sqd_det.sqd_dp, " _
                    & "       public.sqd_det.sqd_sales_unit, " _
                    & "       public.sqd_det.sqd_loc_id, " _
                    & "       public.sqd_det.sqd_serial, " _
                    & "       public.sqd_det.sqd_qty_return, " _
                    & "       public.sqd_det.sqd_ppn_type, " _
                    & "       public.sqd_det.sqd_pod_oid, " _
                    & "       public.sqd_det.sqd_qty_ir, " _
                    & "       public.sqd_det.sqd_invc_oid, " _
                    & "       public.sqd_det.sqd_invc_loc_id, " _
                    & "       public.sqd_det.sqd_need_date, " _
                    & "       public.sqd_det.sqd_qty_transfer_receipt, " _
                    & "       public.sqd_det.sqd_qty_transfer_issue, " _
                    & "       public.sqd_det.sqd_total_amount_price, " _
                    & "       public.sqd_det.sbd_qty_riud, " _
                    & "       public.sqd_det.sbd_qty_processed, " _
                    & "       public.sqd_det.sbd_qty_completed, " _
                    & "       public.sqd_det.sqd_qty_maxorder, " _
                    & "       public.sqd_det.sqd_commision, " _
                    & "       public.sqd_det.sqd_commision_total, " _
                    & "       public.sqd_det.sqd_sales_unit_total, " _
                    & "       public.sqd_det.sqd_sqd_oid, " _
                    & "       public.sqd_det.sodas_sq_oid, " _
                    & "       public.so_mstr.so_sq_ref_oid, " _
                    & "       public.ptsfr_mstr.ptsfr_sq_oid, " _
                    & "       public.ptsfr_mstr.ptsfr_code, " _
                    & "       public.ptsfr_mstr.ptsfr_date, " _
                    & "       public.ptsfr_mstr.ptsfr_trans_id, " _
                    & "       public.so_mstr.so_code, " _
                    & "       public.so_mstr.so_date, " _
                    & "       public.so_mstr.so_trans_id, " _
                    & "       public.so_mstr.so_return, " _
                    & "       public.loc_mstr.loc_desc " _
                    & "FROM public.sq_mstr " _
                    & "     INNER JOIN public.sqd_det ON (public.sq_mstr.sq_oid = public.sqd_det.sqd_sq_oid) " _
                    & "     INNER JOIN public.pi_mstr ON (public.sq_mstr.sq_pi_id = public.pi_mstr.pi_id) " _
                    & "     INNER JOIN public.en_mstr ON (public.sq_mstr.sq_en_id = public.en_mstr.en_id) " _
                    & "     inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                    & "     inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
                    & "     inner join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr_sold.ptnr_ptnrg_id " _
                    & "     left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                    & "     inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                    & "     left outer join code_mstr credit_term on credit_term.code_id = sq_credit_term " _
                    & "     inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
                    & "     inner join cu_mstr on cu_id = sq_cu_id " _
                    & "     inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
                    & "     inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
                    & "     inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
                    & "     inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
                    & "     left outer join bk_mstr on bk_mstr.bk_id = sq_bk_id " _
                    & "     left outer join tran_mstr on tran_id = sq_tran_id " _
                    & "     LEFT OUTER JOIN public.so_mstr ON (public.sq_mstr.sq_oid = public.so_mstr.so_sq_ref_oid) " _
                    & "     LEFT OUTER JOIN public.ptsfr_mstr ON (public.sq_mstr.sq_oid = public.ptsfr_mstr.ptsfr_sq_oid) " _
                    & "     inner join si_mstr on si_id = sqd_si_id " _
                    & "     inner join pt_mstr on pt_id = sqd_pt_id " _
                    & "     inner join code_mstr um_mstr on um_mstr.code_id = sqd_um " _
                    & "     left outer join loc_mstr on loc_id = sqd_loc_id " _
                    & " where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and sq_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

End Class
