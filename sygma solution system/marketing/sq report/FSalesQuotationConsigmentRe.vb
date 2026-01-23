Imports master_new.ModFunction
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraBars.Docking
Imports System.Collections.Generic

Public Class FSalesQuotationConsigmentRe
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSalesOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        grid_detail()
    End Sub

    Private Sub grid_detail()
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Sq Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_detail, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_detail, "SQD OID", "sqd_oid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "SQD OID", "sod_so_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Booking", "sq_booking", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Booking", "sqd_qty_booking", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "SQ Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "SOD SQD", "sod_sqd_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "SO Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Transfer Code", "ptsfr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Transfer Date", "ptsfr_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Transfer  ", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_detail, "SS OID", "soship_oid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "SOD OID", "sod_oid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "SHOD OID", "soshipd_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Shipment Code", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Shipment", "sqd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Shipment", "soshipd_sqd_oid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Qty Allocated  ", "sqd_qty_allocated", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        

        'add_column_copy(gv_detail, "Referensi PO No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Total Before Disc", "sqd_total_before", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Total After Disc", "sqd_total_after", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
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
        add_column_copy(gv_detail, "Payment Date", "sq_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_detail, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Sales Unit", "sq_sales_unit", DevExpress.Utils.HorzAlignment.Default)

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

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            load_detail(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            '    load_book_total(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                            '    load_sales_person(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 3 Then
                            '    load_sales_by_so(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 4 Then
                            '    load_cash_credit(objload)
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub load_detail(ByVal par_obj As Object)
        With par_obj
            .SQL = "SELECT DISTINCT " _
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
                    & "       public.so_mstr.so_code, " _
                    & "       public.so_mstr.so_date, " _
                    & "       public.so_mstr.so_trans_id, " _
                    & "       public.soship_mstr.soship_oid, " _
                    & "       public.soship_mstr.soship_code, " _
                    & "       public.soship_mstr.soship_date, " _
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
                    & "     LEFT OUTER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                    & "     LEFT OUTER JOIN public.ptsfr_mstr ON (public.sq_mstr.sq_oid = public.ptsfr_mstr.ptsfr_sq_oid) " _
                    & "     LEFT OUTER JOIN public.soship_mstr ON (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid) " _
                    & "     LEFT OUTER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                    & "     inner join si_mstr on si_id = sqd_si_id " _
                    & "     inner join pt_mstr on pt_id = sqd_pt_id " _
                    & "     inner join code_mstr um_mstr on um_mstr.code_id = sqd_um " _
                    & "     left outer join loc_mstr on loc_id = sqd_loc_id " _
                    & " where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and sq_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            .InitializeCommand()
            .FillDataSet(ds, "detail")
            gc_detail.DataSource = ds.Tables("detail")
            gv_detail.BestFitColumns()
        End With
    End Sub

    Private Sub load_book_total(ByVal par_obj As Object)
        pgc_book_total.DataSource = Nothing
        pgc_book_total.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "book_total")
            pgc_book_total.DataSource = ds.Tables("book_total")
            pgc_book_total.BestFit()
        End With
    End Sub

    Private Sub load_sales_person(ByVal par_obj As Object)
        pgc_sales_person.DataSource = Nothing
        pgc_sales_person.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "sales_person")
            pgc_sales_person.DataSource = ds.Tables("sales_person")
            pgc_sales_person.BestFit()
        End With
    End Sub

    Private Sub load_sales_by_so(ByVal par_obj As Object)
        pgc_sls_by_so.DataSource = Nothing
        pgc_sls_by_so.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "sls_by_so")
            pgc_sls_by_so.DataSource = ds.Tables("sls_by_so")
            pgc_sls_by_so.BestFit()
        End With
    End Sub

    Private Sub load_cash_credit(ByVal par_obj As Object)
        pgc_cash_credit.DataSource = Nothing
        pgc_cash_credit.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "cash_credit")
            pgc_cash_credit.DataSource = ds.Tables("cash_credit")
            pgc_cash_credit.BestFit()
        End With
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim _file As String = AskSaveAsFile("Excel Files | *.xls*")

        If xtc_master.SelectedTabPageIndex = 0 Then

        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pgc_book_total.OptionsPrint.MergeColumnFieldValues = False
            pgc_book_total.OptionsPrint.MergeRowFieldValues = False
            pgc_book_total.ExportToXls(_file & ".xls")
        ElseIf xtc_master.SelectedTabPageIndex = 2 Then
            pgc_sales_person.OptionsPrint.MergeColumnFieldValues = False
            pgc_sales_person.OptionsPrint.MergeRowFieldValues = False
            pgc_sales_person.ExportToXls(_file & ".xls")
        ElseIf xtc_master.SelectedTabPageIndex = 3 Then
            pgc_sls_by_so.OptionsPrint.MergeColumnFieldValues = False
            pgc_sls_by_so.OptionsPrint.MergeRowFieldValues = False
            pgc_sls_by_so.ExportToXls(_file & ".xls")
        ElseIf xtc_master.SelectedTabPageIndex = 4 Then
            pgc_cash_credit.OptionsPrint.MergeColumnFieldValues = False
            pgc_cash_credit.OptionsPrint.MergeRowFieldValues = False
            pgc_cash_credit.ExportToXls(_file & ".xls")
        End If
        Box("Success")
    End Function

    Private Function set_sql() As String
        set_sql = "SELECT  " _
                    & "soshipd_oid, " _
                    & "soshipd_soship_oid, " _
                    & "soshipd_sod_oid, " _
                    & "soshipd_seq, " _
                    & "soshipd_qty, " _
                    & "soshipd_um, " _
                    & "soshipd_um_conv, " _
                    & "soshipd_cancel_bo, " _
                    & "soshipd_qty_real * - 1 as soshipd_qty_real, " _
                    & "soshipd_si_id, " _
                    & "soshipd_loc_id, " _
                    & "soshipd_lot_serial, " _
                    & "soshipd_rea_code_id, " _
                    & "soshipd_dt, " _
                    & "soshipd_qty_inv, " _
                    & "soshipd_close_line, " _
                    & "sod_pt_id, " _
                    & "sod_cost, " _
                    & "sod_tax_class, " _
                    & "pt_code, " _
                    & "pt_desc1, " _
                    & "pt_desc2, " _
                    & "pt_syslog_code, " _
                    & "so_code, " _
                    & "so_oid, " _
                    & "sales_mstr.ptnr_name as sales_name, " _
                    & "bill_mstr.ptnr_name as bill_name, ptnrg_name, " _
                    & "so_date, " _
                    & "en_desc, " _
                    & "rate_ppn, " _
                    & "rate_pph, " _
                    & "sod_tax_inc, " _
                    & "sod_price * so_exc_rate as sod_price ,x.code_name as payment_type, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(sod_price,2) * so_exc_rate  " _
                    & " when sod_tax_inc ~~* 'Y' then round((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))),2) * so_exc_rate " _
                    & "end sod_price_ori, " _
                    & " " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((sod_price * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end sod_price_ori_ext, " _
                    & "        " _
                    & "sod_disc, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((sod_price * sod_disc),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc),2) * so_exc_rate " _
                    & "end sod_disc_value, " _
                    & " " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(((sod_price * sod_disc) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end sod_disc_value_ext, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((sod_price - (sod_price * sod_disc)),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)),2) * so_exc_rate " _
                    & "end sod_price_ori_aft_disc, " _
                    & " " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(((sod_price - (sod_price * sod_disc)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end sod_price_ori_aft_disc_ext, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)),2) * so_exc_rate " _
                    & "end ppn_value, " _
                    & " " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end ppn_value_ext, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00)),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00)),2) * so_exc_rate " _
                    & "end pph_value, " _
                    & " " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00)) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end pph_value_ext, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round(((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))),2) * so_exc_rate " _
                    & "end sod_price_ori_aft_disc_aft_tax, " _
                    & "        " _
                    & "case " _
                    & " when sod_tax_inc ~~* 'N' then round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) * (rate_ppn/100.00)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & " when sod_tax_inc ~~* 'Y' then round(((((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) + (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_ppn/100.00)) - (((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) - ((sod_price - ((rate_ppn/100.00) * (sod_price / (1.00 + (rate_ppn/100.00))))) * sod_disc)) * (rate_pph/100.00))) * soshipd_qty_real * -1.00),2) * so_exc_rate " _
                    & "end sod_price_ori_aft_disc_aft_tax_ext, " _
                    & "  case upper(sod_tax_inc) " _
                    & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                    & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                    & "  end as dpp " _
                    & "        " _
                    & "FROM soshipd_det " _
                    & "inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                    & "inner join sod_det on sod_oid = soshipd_sod_oid " _
                    & "inner join so_mstr on so_oid = sod_so_oid " _
                    & "inner join pt_mstr on pt_id = sod_pt_id " _
                    & "inner join en_mstr on en_id = soship_en_id " _
                    & "inner join code_mstr x on x.code_id = so_pay_type " _
                    & "inner join  " _
                    & "(SELECT taxr_tax_class, taxr_rate as rate_ppn FROM taxr_mstr " _
                    & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                    & " code_name ~~* 'ppn') taxr_ppn on taxr_ppn.taxr_tax_class = sod_tax_class " _
                    & "inner join  " _
                    & "(SELECT taxr_tax_class, taxr_rate as rate_pph FROM taxr_mstr " _
                    & "inner join code_mstr tax_type on tax_type.code_id = taxr_tax_type where " _
                    & " code_name ~~* 'pph') taxr_pph on taxr_pph.taxr_tax_class = sod_tax_class " _
                    & "inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                    & "inner join ptnr_mstr bill_mstr on bill_mstr.ptnr_id = so_ptnr_id_bill " _
                    & "inner join ptnrg_grp on ptnrg_grp.ptnrg_id = bill_mstr.ptnr_ptnrg_id " _
                    & "where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and so_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"





        '& "where soship_is_shipment ~~* 'Y'" _
        '& " and soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '& " and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '& " and so_en_id in (select user_en_id from tconfuserentity " _
        '& " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return set_sql
    End Function

    Private Sub pgc_book_total_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pgc_book_total.DoubleClick

        Try
            Dim _pt_code As String
            _pt_code = pgc_book_total.GetFieldValue(pgc_book_total.Fields("pt_code"), cekrow_pivot(pgc_book_total))

            Dim frm As New FSOReportCheck
            frm.par_pt_code = _pt_code
            frm.par_tgl_awal = pr_txttglawal.DateTime
            frm.par_tgl_akhir = pr_txttglakhir.DateTime
            frm.Show()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub pgc_sales_person_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pgc_sales_person.DoubleClick

        Try
            Dim _pt_code As String
            _pt_code = pgc_sales_person.GetFieldValue(pgc_sales_person.Fields("pt_code"), cekrow_pivot(pgc_sales_person))

            Dim frm As New FSOReportCheck
            frm.par_pt_code = _pt_code
            frm.par_tgl_awal = pr_txttglawal.DateTime
            frm.par_tgl_akhir = pr_txttglakhir.DateTime
            frm.Show()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub pgc_sls_by_so_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pgc_sls_by_so.DoubleClick

        Try
            Dim _pt_code As String
            _pt_code = pgc_sls_by_so.GetFieldValue(pgc_sls_by_so.Fields("pt_code"), cekrow_pivot(pgc_sls_by_so))

            Dim frm As New FSOReportCheck
            frm.par_pt_code = _pt_code
            frm.par_tgl_awal = pr_txttglawal.DateTime
            frm.par_tgl_akhir = pr_txttglakhir.DateTime
            frm.Show()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Const CellDelimiter As String = vbTab
    Const LineDelimiter As String = vbCrLf
    Function cekrow_pivot(ByVal PivotGrid As PivotGridControl) As Integer
        Dim cells As PivotGridCells = PivotGrid.Cells
        ' Get the coordinates of the selected cells.
        Dim cellSelection As Rectangle = cells.Selection
        Dim result As String = ""
        ' Get the index of the bottommost selected row.
        Dim maxRow As Integer = cellSelection.Y + cellSelection.Height - 1
        ' Get the index of the rightmost selected column.
        Dim maxColumn As Integer = cellSelection.X + cellSelection.Width - 1
        ' Iterate through the selected cells.
        Dim rowIndex, colIndex As Integer
        For rowIndex = cellSelection.Y To maxRow
            For colIndex = cellSelection.X To maxColumn
                ' Get the current cell's display text.
                result += cells.GetCellInfo(colIndex, rowIndex).DisplayText
                If (colIndex < maxColumn) Then result += CellDelimiter
            Next
            If (rowIndex < maxRow) Then result += LineDelimiter
        Next


        Return rowIndex - 1

        ' Copy the resulting text to the clipboard.
        'Clipboard.SetDataObject(result)
    End Function

End Class
