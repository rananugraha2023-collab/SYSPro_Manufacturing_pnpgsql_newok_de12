Imports master_new.ModFunction
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraBars.Docking
Imports System.Collections.Generic

Public Class FSalesOrderReport
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
        add_column_copy(gv_detail, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Parent", "so_parent_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "SO Type", "so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Customer Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Referensi Po No.", "so_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Referensi Po Take Over", "so_ref_po_code_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code Header", "ac_code_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name Header", "ac_name_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Taxable", "so_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "so_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Payment", "so_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "SO Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sales Unit", "so_sales_unit", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Additional Charge", "sod_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Child", "sod_qty_child", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Shipment", "sod_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Outstanding", "sod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "sod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Account Code Detail", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name Detail", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account Detail", "sb_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center Detail", "cc_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "sod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "sod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "sod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN Type", "sod_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Comission", "sod_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Comission", "so_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
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
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            load_book_total(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                            load_sales_person(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 3 Then
                            load_sales_by_so(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 4 Then
                            load_cash_credit(objload)
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
            .SQL = "SELECT  " _
                    & "  so_oid, " _
                    & "  so_dom_id, " _
                    & "  so_en_id, " _
                    & "  so_add_by, " _
                    & "  so_add_date, " _
                    & "  so_upd_by, " _
                    & "  so_upd_date, " _
                    & "  so_code, " _
                    & "  so_ptnr_id_sold, " _
                    & "  so_ptnr_id_bill, " _
                    & "  so_ref_po_code, " _
                    & "  so_ref_po_oid, " _
                    & "  so_date, " _
                    & "  so_credit_term, " _
                    & "  so_taxable, " _
                    & "  so_tax_class, " _
                    & "  so_si_id, " _
                    & "  so_type, " _
                    & "  so_sales_person,so_ref_po_code_2,sod_qty_child,so_parent_code, " _
                    & "  so_pi_id, " _
                    & "  so_pay_type, " _
                    & "  so_pay_method, " _
                    & "  so_ar_ac_id, " _
                    & "  so_ar_sb_id, " _
                    & "  so_ar_cc_id, " _
                    & "  so_dp, " _
                    & "  so_disc_header, " _
                    & "  so_total, " _
                    & "  so_print_count, " _
                    & "  so_payment_date, " _
                    & "  so_cons, " _
                    & "  so_close_date, " _
                    & "  so_tran_id, " _
                    & "  so_trans_id, " _
                    & "  so_trans_rmks, " _
                    & "  so_current_route, " _
                    & "  so_next_route, " _
                    & "  so_dt, " _
                    & "  so_cu_id, " _
                    & "  so_total_ppn, " _
                    & "  so_total_pph, " _
                    & "  so_payment, " _
                    & "  so_exc_rate, " _
                    & "  so_tax_inc, " _
                    & "  (so_total + so_total_ppn + so_total_pph) as so_total_after_tax, " _
                    & "  so_exc_rate * so_total as so_total_ext,  so_exc_rate * so_total_ppn as so_total_ppn_ext, " _
                    & "  so_exc_rate * so_total_pph as so_total_pph_ext,  so_exc_rate * (so_total + so_total_ppn + so_total_pph) as so_total_after_tax_ext, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, ptnrg_name, ptnrc_name," _
                    & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                    & "  credit_term.code_name as credit_term_name, " _
                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                    & "  pi_desc, " _
                    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  ac_mstr_ar.ac_code as ac_code_header, " _
                    & "  ac_mstr_ar.ac_name as ac_name_header, " _
                    & "  sb_mstr_ar.sb_desc as sb_desc_header, " _
                    & "  cc_mstr_ar.cc_desc as cc_desc_header, " _
                    & "  cu_name, " _
                    & "  tran_name, " _
                    & "  so_bk_id, bk_code, bk_name, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  sod_oid, " _
                    & "  sod_dom_id, " _
                    & "  sod_en_id, " _
                    & "  sod_add_by, " _
                    & "  sod_add_date, " _
                    & "  sod_upd_by, " _
                    & "  sod_upd_date, " _
                    & "  sod_so_oid, " _
                    & "  sod_seq, " _
                    & "  sod_is_additional_charge, " _
                    & "  sod_si_id, " _
                    & "  sod_pt_id, " _
                    & "  sod_rmks, " _
                    & "  sod_qty, " _
                    & "  coalesce (sod_qty_allocated,0) as sod_qty_allocated, " _
                    & "  (sod_qty - coalesce (sod_qty_shipment,0)) as sod_qty_outstanding, " _
                    & "  coalesce (sod_qty_picked,0) as sod_qty_picked, " _
                    & "  coalesce (sod_qty_shipment,0) as sod_qty_shipment," _
                    & "  coalesce (sod_qty_pending_inv,0) as sod_qty_pending_inv," _
                    & "  coalesce (sod_qty_invoice,0) as sod_qty_invoice," _
                    & "  sod_um, " _
                    & "  sod_cost, " _
                    & "  sod_price, " _
                    & "  sod_disc, " _
                    & "  sod_sales_ac_id, " _
                    & "  sod_sales_sb_id, " _
                    & "  sod_sales_cc_id, " _
                    & "  sod_disc_ac_id, " _
                    & "  sod_um_conv, " _
                    & "  sod_qty_real, " _
                    & "  sod_taxable, " _
                    & "  sod_tax_inc, " _
                    & "  sod_tax_class, " _
                    & "  sod_status, " _
                    & "  sod_dt, " _
                    & "  sod_payment, " _
                    & "  sod_dp, " _
                    & "  sod_sales_unit, " _
                    & "  sod_sales_unit * sod_qty as so_sales_unit, " _
                    & "  sod_commision, " _
                    & "  sod_commision * sod_qty as so_commision, " _
                    & "  sod_loc_id, " _
                    & "  sod_serial, " _
                    & "  en_desc, " _
                    & "  si_desc, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  um_mstr.code_name as um_name, " _
                    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                    & "  sb_mstr_sales.sb_desc as sb_desc_detail, " _
                    & "  cc_mstr_sales.cc_desc as cc_desc_detail, " _
                    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                    & "  tax_class.code_name as sod_tax_class_name, " _
                    & "  sod_ppn_type, " _
                    & "  loc_desc " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = so_ptnr_id_bill " _
                    & "  left outer join ptnrc_cat on ptnrc_cat.ptnrc_id =  ptnr_mstr_sold.ptnr_ptnrc_id " _
                    & "  inner join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr_sold.ptnr_ptnrg_id " _
                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                    & "  inner join code_mstr credit_term on credit_term.code_id = so_credit_term " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
                    & "  inner join pi_mstr on pi_id = so_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "  inner join code_mstr pay_method on pay_method.code_id = so_pay_method " _
                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = so_ar_ac_id " _
                    & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
                    & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id " _
                    & "  inner join bk_mstr on bk_mstr.bk_id = so_bk_id " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  inner join tran_mstr on tran_id = so_tran_id " _
                    & "  inner join sod_det on sod_so_oid = so_oid  " _
                    & "  inner join en_mstr on en_id = sod_en_id " _
                    & "  inner join si_mstr on si_id = sod_si_id " _
                    & "  inner join pt_mstr on pt_id = sod_pt_id " _
                    & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                    & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class	 " _
                    & "  left outer join loc_mstr on loc_id = sod_loc_id" _
                    & " where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and so_en_id in (select user_en_id from tconfuserentity " _
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
