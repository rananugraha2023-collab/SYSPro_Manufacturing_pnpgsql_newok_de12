Imports npgsql
Imports master_new.ModFunction

Public Class FPurchaseOrderReportNoCost
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPurchaseOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity Header", "en_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Order Date Header", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Need Date Header", "po_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Due Date Header", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Remarks Header", "po_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Site Header", "si_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Project Header", "pjc_desc_header", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Credit Terms", "po_credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Exchange Rate", "po_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_view1, "Taxable Header", "po_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Include Header", "po_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Class Header", "po_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Close Date", "po_close_date", DevExpress.Utils.HorzAlignment.Center)

        'add_column_copy(gv_view1, "Total", "po_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "PPN", "po_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "PPH", "po_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "After Tax", "po_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_view1, "Ext. Total", "po_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Ext. PPN", "po_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Ext. PPH", "po_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Ext. After Tax", "po_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_view1, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cash/Credit", "po_status_cash", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "Revision Count", "po_rev", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_view1, "pod_po_oid", False)
        add_column_copy(gv_view1, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Requisition", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Remarks Detail", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "End User Detail", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Receive", "pod_qty_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Outstanding", "pod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Invoice  ", "pod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_view1, "Sub Account Detail", "sb_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Cost Center Detail", "cc_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Project Detail", "pjc_desc_detail", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Taxable Detail", "pod_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Include Detail", "pod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Tax Class Detail", "pod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "Need Date Detail", "pod_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Due Date Detail", "pod_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Real", "pod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty * Cost", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_view1, "Status", "pod_status", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_view2, "Entity Header", "en_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Order Date Header", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Need Date Header", "po_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Due Date Header", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Remarks Header", "po_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Site Header", "si_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Project Header", "pjc_desc_header", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Credit Terms", "po_credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Exchange Rate", "po_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_view2, "Taxable Header", "po_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Include Header", "po_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Class Header", "po_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Close Date", "po_close_date", DevExpress.Utils.HorzAlignment.Center)

        'add_column_copy(gv_view2, "Total", "po_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "PPN", "po_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "PPH", "po_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "After Tax", "po_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_view2, "Ext. Total", "po_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view2, "Ext. PPN", "po_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view2, "Ext. PPH", "po_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view2, "Ext. After Tax", "po_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_view2, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Cash/Credit", "po_status_cash", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Revision Count", "po_rev", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_view2, "pod_po_oid", False)
        add_column_copy(gv_view2, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Requisition", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Remarks Detail", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "End User Detail", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view2, "Qty Receive", "pod_qty_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view2, "Qty Outstanding", "pod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view2, "Qty Invoice  ", "pod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view2, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_view2, "Sub Account Detail", "sb_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Cost Center Detail", "cc_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Project Detail", "pjc_desc_detail", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Taxable Detail", "pod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Include Detail", "pod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Tax Class Detail", "pod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Need Date Detail", "pod_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "Due Date Detail", "pod_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view2, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view2, "Qty Real", "pod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty * Cost", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_view2, "Status", "pod_status", DevExpress.Utils.HorzAlignment.Default)
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
                            .SQL = "SELECT  " _
                            & "en_mstr_header.en_desc as en_desc_header, " _
                            & "public.po_mstr.po_oid, " _
                            & "public.po_mstr.po_dom_id, " _
                            & "public.po_mstr.po_en_id, " _
                            & "public.po_mstr.po_upd_date, " _
                            & "public.po_mstr.po_upd_by, " _
                            & "public.po_mstr.po_add_date, " _
                            & "public.po_mstr.po_add_by, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.po_mstr.po_cmaddr_id, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_need_date, " _
                            & "public.po_mstr.po_due_date, " _
                            & "public.po_mstr.po_rmks, " _
                            & "public.po_mstr.po_sb_id, " _
                            & "public.po_mstr.po_cc_id, " _
                            & "public.po_mstr.po_si_id, " _
                            & "public.po_mstr.po_pjc_id, " _
                            & "public.po_mstr.po_close_date, " _
                            & "public.po_mstr.po_total, " _
                            & "public.po_mstr.po_tran_id, " _
                            & "public.po_mstr.po_trans_id, " _
                            & "public.po_mstr.po_trans_rmks, " _
                            & "public.po_mstr.po_current_route, " _
                            & "public.po_mstr.po_next_route, " _
                            & "public.po_mstr.po_dt, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "cmaddr_name, " _
                            & "tran_name, " _
                            & "po_status_cash, " _
                            & "pjc_mstr_header.pjc_desc as pjc_desc_header, " _
                            & "si_mstr_header.si_desc as si_desc_header, " _
                            & "sb_mstr_header.sb_desc as sb_desc_header,  " _
                            & "cc_mstr_header.cc_desc as cc_desc_header, " _
                            & "po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                            & "po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                            & "po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                            & "po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext, " _
                            & "en_mstr_detail.en_desc as en_desc_detail, " _
                            & "pod_seq, " _
                            & "si_mstr_detail.si_desc as si_desc_detail, " _
                            & "pt_code, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "pod_rmks, " _
                            & "pod_end_user, " _
                            & "pod_qty, " _
                            & "pod_qty_receive, " _
                            & "pod_qty_invoice, " _
                            & "pod_qty - coalesce(pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "pod_um, " _
                            & "um_master.code_name as um_name, " _
                            & "pod_cost, " _
                            & "pod_disc, " _
                            & "pod_sb_id, pod_cc_id, pod_pjc_id, " _
                            & "pod_need_date, " _
                            & "pod_due_date, " _
                            & "pod_um_conv, " _
                            & "pod_qty_real, " _
                            & "pod_pt_class, " _
                            & "pod_taxable, " _
                            & "pod_tax_inc, " _
                            & "pod_tax_class, " _
                            & "pod_status, " _
                            & "pod_qty_return, " _
                            & "pjc_mstr_detail.pjc_desc as pjc_desc_detail, " _
                            & "si_mstr_detail.si_desc as si_desc_detailr, " _
                            & "sb_mstr_detail.sb_desc as sb_desc_detail,  " _
                            & " taxclass_mstr_detail.code_name as pod_tax_class_name, " _
                            & "cc_mstr_detail.cc_desc as cc_desc_detail, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost " _
                            & "FROM " _
                            & "public.po_mstr " _
                            & "INNER JOIN public.en_mstr en_mstr_header ON (public.po_mstr.po_en_id = en_mstr_header.en_id) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "INNER JOIN public.sb_mstr sb_mstr_header ON (public.po_mstr.po_sb_id = sb_mstr_header.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_header ON (public.po_mstr.po_cc_id = cc_mstr_header .cc_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_header ON (public.po_mstr.po_si_id = si_mstr_header.si_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_header ON (public.po_mstr.po_pjc_id = pjc_mstr_header.pjc_id) " _
                            & "INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class   " _
                            & "inner join pod_det on pod_po_oid = po_oid " _
                            & "INNER JOIN public.en_mstr en_mstr_detail ON (pod_en_id = en_mstr_detail.en_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_detail ON (pod_si_id = si_mstr_detail.si_id) " _
                            & "inner join pt_mstr on pt_id = pod_pt_id " _
                            & "INNER JOIN public.sb_mstr sb_mstr_detail ON (pod_sb_id = sb_mstr_detail.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_detail ON (pod_cc_id = cc_mstr_detail .cc_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_detail ON (pod_pjc_id = pjc_mstr_detail.pjc_id) " _
                            & "INNER JOIN public.code_mstr as um_master ON um_master.code_id  = pod_um " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr_detail ON taxclass_mstr_detail.code_id  = pod_tax_class   " _
                            & " where po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            .SQL = "SELECT  " _
                            & "en_mstr_header.en_desc as en_desc_header, " _
                            & "public.po_mstr.po_oid, " _
                            & "public.po_mstr.po_dom_id, " _
                            & "public.po_mstr.po_en_id, " _
                            & "public.po_mstr.po_upd_date, " _
                            & "public.po_mstr.po_upd_by, " _
                            & "public.po_mstr.po_add_date, " _
                            & "public.po_mstr.po_add_by, " _
                            & "public.po_mstr.po_code, " _
                            & "public.po_mstr.po_ptnr_id, " _
                            & "public.po_mstr.po_cmaddr_id, " _
                            & "public.po_mstr.po_date, " _
                            & "public.po_mstr.po_need_date, " _
                            & "public.po_mstr.po_due_date, " _
                            & "public.po_mstr.po_rmks, " _
                            & "public.po_mstr.po_sb_id, " _
                            & "public.po_mstr.po_cc_id, " _
                            & "public.po_mstr.po_si_id, " _
                            & "public.po_mstr.po_pjc_id, " _
                            & "public.po_mstr.po_close_date, " _
                            & "public.po_mstr.po_total, " _
                            & "public.po_mstr.po_tran_id, " _
                            & "public.po_mstr.po_trans_id, " _
                            & "public.po_mstr.po_trans_rmks, " _
                            & "public.po_mstr.po_current_route, " _
                            & "public.po_mstr.po_next_route, " _
                            & "public.po_mstr.po_dt, " _
                            & "public.ptnr_mstr.ptnr_name, " _
                            & "cmaddr_name, " _
                            & "tran_name, " _
                            & "po_status_cash, " _
                            & "pjc_mstr_header.pjc_desc as pjc_desc_header, " _
                            & "si_mstr_header.si_desc as si_desc_header, " _
                            & "sb_mstr_header.sb_desc as sb_desc_header,  " _
                            & "cc_mstr_header.cc_desc as cc_desc_header, " _
                            & "po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                            & "po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                            & "po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                            & "po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext, " _
                            & "en_mstr_detail.en_desc as en_desc_detail, " _
                            & "pod_seq, " _
                            & "si_mstr_detail.si_desc as si_desc_detail, " _
                            & "pt_code, " _
                            & "pod_pt_desc1, " _
                            & "pod_pt_desc2, " _
                            & "pod_rmks, " _
                            & "pod_end_user, " _
                            & "pod_qty, " _
                            & "pod_qty_receive, " _
                            & "pod_qty_invoice, " _
                            & "pod_qty - coalesce(pod_qty_receive,0) as pod_qty_outstanding, " _
                            & "pod_um, " _
                            & "um_master.code_name as um_name, " _
                            & "pod_cost, " _
                            & "pod_disc, " _
                            & "pod_sb_id, pod_cc_id, pod_pjc_id, " _
                            & "pod_need_date, " _
                            & "pod_due_date, " _
                            & "pod_um_conv, " _
                            & "pod_qty_real, " _
                            & "pod_pt_class, " _
                            & "pod_taxable, " _
                            & "pod_tax_inc, " _
                            & "pod_tax_class, " _
                            & "pod_status, " _
                            & "pod_qty_return, " _
                            & "pjc_mstr_detail.pjc_desc as pjc_desc_detail, " _
                            & "si_mstr_detail.si_desc as si_desc_detailr, " _
                            & "sb_mstr_detail.sb_desc as sb_desc_detail,  " _
                            & " taxclass_mstr_detail.code_name as pod_tax_class_name, " _
                            & "cc_mstr_detail.cc_desc as cc_desc_detail, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost " _
                            & "FROM " _
                            & "public.po_mstr " _
                            & "INNER JOIN public.en_mstr en_mstr_header ON (public.po_mstr.po_en_id = en_mstr_header.en_id) " _
                            & "INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                            & "INNER JOIN public.sb_mstr sb_mstr_header ON (public.po_mstr.po_sb_id = sb_mstr_header.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_header ON (public.po_mstr.po_cc_id = cc_mstr_header .cc_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_header ON (public.po_mstr.po_si_id = si_mstr_header.si_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_header ON (public.po_mstr.po_pjc_id = pjc_mstr_header.pjc_id) " _
                            & "INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                            & "INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                            & "INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class   " _
                            & "inner join pod_det on pod_po_oid = po_oid " _
                            & "INNER JOIN public.en_mstr en_mstr_detail ON (pod_en_id = en_mstr_detail.en_id) " _
                            & "INNER JOIN public.si_mstr si_mstr_detail ON (pod_si_id = si_mstr_detail.si_id) " _
                            & "inner join pt_mstr on pt_id = pod_pt_id " _
                            & "INNER JOIN public.sb_mstr sb_mstr_detail ON (pod_sb_id = sb_mstr_detail.sb_id) " _
                            & "INNER JOIN public.cc_mstr cc_mstr_detail ON (pod_cc_id = cc_mstr_detail .cc_id) " _
                            & "INNER JOIN public.pjc_mstr pjc_mstr_detail ON (pod_pjc_id = pjc_mstr_detail.pjc_id) " _
                            & "INNER JOIN public.code_mstr as um_master ON um_master.code_id  = pod_um " _
                            & "INNER JOIN public.code_mstr as taxclass_mstr_detail ON taxclass_mstr_detail.code_id  = pod_tax_class   " _
                            & " where coalesce(pod_qty_receive,0) < pod_qty and coalesce(po_trans_id,'I') <> 'X' " _
                            & " and po_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                            .InitializeCommand()
                            .FillDataSet(ds, "view2")
                            gc_view2.DataSource = ds.Tables("view2")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub xtc_master_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xtc_master.TabIndexChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        End If
    End Sub
End Class
