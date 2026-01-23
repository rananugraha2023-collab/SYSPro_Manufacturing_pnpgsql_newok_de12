Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FSalesQuotationConsigment
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit As New DataSet
    Dim _now As DateTime
    Dim _sq_oid_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Dim mf As New master_new.ModFunction

    Private Sub FSalesQuotation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        init_le(sq_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sq_type())
        sq_type.Properties.DataSource = dt_bantu
        sq_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        sq_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        sq_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        sq_cu_id.Properties.DataSource = dt_bantu
        sq_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        sq_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        sq_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        sq_ar_ac_id.Properties.DataSource = dt_bantu
        sq_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        sq_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        sq_ar_ac_id.ItemIndex = 0

        init_le(le_en_id, "en_mstr")

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales(sq_en_id.EditValue))
        sq_sales_person.Properties.DataSource = dt_bantu
        sq_sales_person.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        sq_sales_person.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        sq_sales_person.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(sq_en_id.EditValue))
        sq_si_id.Properties.DataSource = dt_bantu
        sq_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        sq_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        sq_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pay_type(sq_en_id.EditValue))
        sq_pay_type.Properties.DataSource = dt_bantu
        sq_pay_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sq_pay_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sq_pay_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sq_en_id.EditValue, "payment_methode"))
        sq_pay_method.Properties.DataSource = dt_bantu
        sq_pay_method.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sq_pay_method.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sq_pay_method.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sq_en_id.EditValue))
        sq_ar_sb_id.Properties.DataSource = dt_bantu
        sq_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        sq_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        sq_ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sq_en_id.EditValue))
        sq_ar_cc_id.Properties.DataSource = dt_bantu
        sq_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        sq_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        sq_ar_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        sq_pi_id.Properties.DataSource = dt_bantu
        sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        sq_pi_id.ItemIndex = 0

        


    End Sub

    Private Sub sq_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub sq_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_type.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        sq_pi_id.Properties.DataSource = dt_bantu
        sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        sq_pi_id.ItemIndex = 0
    End Sub

    Private Sub sq_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_cu_id.EditValueChanged
        Dim _sq_exc_rate As Double
        If sq_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _sq_exc_rate = func_data.get_exchange_rate(sq_cu_id.EditValue, sq_date.DateTime)
            If _sq_exc_rate = 1 Then
                sq_exc_rate.EditValue = 0
            Else
                sq_exc_rate.EditValue = _sq_exc_rate
            End If

        Else
            sq_exc_rate.EditValue = 1
        End If

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(sq_en_id.EditValue, sq_type.EditValue, sq_cu_id.EditValue, sq_date.DateTime))
        sq_pi_id.Properties.DataSource = dt_bantu
        sq_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        sq_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        sq_pi_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SQ Type", "sq_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Dropship Address", "sq_dropship_address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi Po No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        '
        add_column_copy(gv_master, "Preorder Number", "sq_preorder_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Payment", "sq_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "sq_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sales Unit", "sq_sales_unit", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Is Package", "sq_is_package", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

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

        add_column(gv_detail, "sqd_sq_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Additional Charge", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Transfer", "sqd_qty_transfer", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty SO", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "sqd_oid", False)
        add_column(gv_edit, "sqd_sq_oid", False)
        add_column(gv_edit, "sqd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_is_additional_charge", False)
        add_column(gv_edit, "sqd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sqd_um", False)
        add_column(gv_edit, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        If func_coll.get_conf_file("editable_sq_price") = "0" Then
            add_column(gv_edit, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        Else
            add_column_edit(gv_edit, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If
        add_column_edit(gv_edit, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "sqd_sales_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_sales_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_sales_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_disc_ac_id", False)
        add_column(gv_edit, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_tax_class", False)
        add_column(gv_edit, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "PPN Type", "sqd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sqd_pod_oid", False)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sq_oid, sq_preorder_code,sq_dropship_address," _
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
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM  " _
                    & "  public.sq_mstr " _
                    & "  inner join en_mstr on en_id = sq_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
                    & "  inner join si_mstr on si_id = sq_si_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                    & "  left outer join ptnra_addr on ptnra_id = sq_ptnra_id " _
                    & "  inner join pi_mstr on pi_id = sq_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
                    & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
                    & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
                    & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
                    & "  inner join cu_mstr on cu_id = sq_cu_id " _
                    & "  left outer join tran_mstr on tran_id = sq_tran_id" _
                    & "  left outer join pt_mstr on sq_pt_id = pt_id " _
                    & " where sq_cons='Y' and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)


        'get_sequel = "SELECT  " _
        '                    & "  sq_oid, sq_preorder_code,sq_dropship_address," _
        '                    & "  sq_dom_id, " _
        '                    & "  sq_en_id, " _
        '                    & "  sq_add_by, " _
        '                    & "  sq_add_date, " _
        '                    & "  sq_upd_by, " _
        '                    & "  sq_upd_date, " _
        '                    & "  sq_code, " _
        '                    & "  sq_ptnr_id_sold, " _
        '                    & "  sq_ptnr_id_bill, " _
        '                    & "  sq_ref_po_code, " _
        '                    & "  sq_ref_po_oid, " _
        '                    & "  sq_date, " _
        '                    & "  sq_si_id, " _
        '                    & "  sq_type, " _
        '                    & "  sq_sales_person, " _
        '                    & "  sq_pi_id, " _
        '                    & "  sq_pay_type, " _
        '                    & "  sq_pay_method, " _
        '                    & "  sq_ar_ac_id, " _
        '                    & "  sq_ar_sb_id, " _
        '                    & "  sq_ar_cc_id, " _
        '                    & "  sq_dp, " _
        '                    & "  sq_disc_header, " _
        '                    & "  sq_total, " _
        '                    & "  sq_print_count, " _
        '                    & "  sq_need_date, " _
        '                    & "  sq_cons, " _
        '                    & "  sq_close_date, " _
        '                    & "  sq_tran_id, " _
        '                    & "  sq_trans_id, " _
        '                    & "  sq_trans_rmks, " _
        '                    & "  sq_current_route, " _
        '                    & "  sq_next_route, " _
        '                    & "  sq_dt, " _
        '                    & "  sq_cu_id, " _
        '                    & "  sq_total_ppn, " _
        '                    & "  sq_total_pph, " _
        '                    & "  sq_payment, " _
        '                    & "  sq_exc_rate, " _
        '                    & "  (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax, " _
        '                    & "  sq_exc_rate * sq_total as sq_total_ext,  sq_exc_rate * sq_total_ppn as sq_total_ppn_ext, " _
        '                    & "  sq_exc_rate * sq_total_pph as sq_total_pph_ext,  sq_exc_rate * (sq_total + sq_total_ppn + sq_total_pph) as sq_total_after_tax_ext, " _
        '                    & "  en_desc, " _
        '                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
        '                    & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
        '                    & "  si_desc, " _
        '                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
        '                    & "  pi_desc, " _
        '                    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
        '                    & "  pay_method.code_name as pay_method_name, " _
        '                    & "  ac_mstr_ar.ac_code, " _
        '                    & "  ac_mstr_ar.ac_name, " _
        '                    & "  sb_mstr_ar.sb_desc, " _
        '                    & "  cc_mstr_ar.cc_desc,sq_pt_id,coalesce(sq_is_package,'N') as sq_is_package,pt_code,pt_desc1,pt_desc2,sq_price, " _
        '                    & "  cu_name, " _
        '                    & "  tran_name, " _
        '                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3 " _
        '                    & "FROM  " _
        '                    & "  public.sq_mstr " _
        '                    & "  inner join en_mstr on en_id = sq_en_id " _
        '                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
        '                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
        '                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
        '                    & "  inner join si_mstr on si_id = sq_si_id " _
        '                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
        '                    & "  inner join pi_mstr on pi_id = sq_pi_id " _
        '                    & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
        '                    & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
        '                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
        '                    & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
        '                    & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
        '                    & "  inner join cu_mstr on cu_id = sq_cu_id " _
        '                    & "  left outer join tran_mstr on tran_id = sq_tran_id" _
        '                    & "  left outer join pt_mstr on sq_pt_id = pt_id " _
        '                    & " where sq_cons='Y' and sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)


        If ce_en_id.EditValue = True Then
            get_sequel += " and sq_en_id =" & le_en_id.EditValue
        Else
            get_sequel += " and sq_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If
        If ce_cus.EditValue = True Then
            get_sequel += " and sq_ptnr_id_sold =" & le_cus_id.EditValue
        End If
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

    End Sub

    Public Overrides Sub relation_detail()
        'Dim ssql As String
        Try
            'load semua data detail diawal trus di filter yg tdk efisien sana sekali
            'berattttt booo

            'gv_detail.Columns("sqd_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            'gv_detail.BestFitColumns()

            'gv_detail_attr.Columns("sqa_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            'gv_detail.BestFitColumns()

            ''gv_detail_kp.Columns("sokp_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid"))
            ''gv_detail_kp.BestFitColumns()

            'If _conf_value = "1" Then
            '    gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code").ToString)
            '    gv_wf.BestFitColumns()

            '    gv_email.Columns("sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            '    gv_email.BestFitColumns()
            'End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If sq_taxable.EditValue = False Then
        '    sq_tax_class.Enabled = False
        '    sq_tax_class.ItemIndex = 0
        '    sq_tax_inc.Enabled = False
        '    sq_tax_inc.Checked = False
        'Else
        '    sq_tax_class.Enabled = True
        '    sq_tax_inc.Enabled = True
        '    sq_tax_inc.Checked = False
        'End If
    End Sub

    Public Overrides Sub insert_data_awal()
        sq_ptnr_id_sold.Tag = ""
        sq_en_id.Focus()
        sq_en_id.ItemIndex = 0
        sq_date.DateTime = _now
        sq_type.ItemIndex = 0
        sq_ar_ac_id.ItemIndex = 0
        sq_cu_id.ItemIndex = 0
        sq_exc_rate.Text = 1
        sq_trans_rmks.Text = ""
        sq_ptnr_id_sold.Text = ""
        sq_bantu_address.Text = ""
        sq_cu_id.Enabled = True
        sq_type.Enabled = True
        sq_pay_type.Enabled = True
        sq_pi_id.Enabled = True
        sq_ptnr_id_sold.Enabled = True
        sq_bantu_address.Enabled = True
        sq_ref_po_code.Text = ""
        sq_ref_po_code.Enabled = True

        sq_is_package.EditValue = False
        sq_pt_id.Tag = ""
        sq_pt_id.EditValue = ""
        pt_desc1.EditValue = ""
        pt_desc2.EditValue = ""
        sq_price.EditValue = ""
        sq_preorder_code.EditValue = ""
        sq_dropship_address.EditValue = ""

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try


        te_dpp.EditValue = 0.0
        te_ppn.EditValue = 0.0
        te_discount.EditValue = 0.0
        te_total.EditValue = 0.0
    End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()
        gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
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
                        & "  sqd_total_amount_price, " _
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
                        & "  sqd_ppn_type, " _
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
                        & "  pt_type, " _
                        & "  pt_ls, " _
                        & "  um_mstr.code_name as um_name, " _
                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                        & "  tax_class.code_name as sqd_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  sqd_pod_oid " _
                        & "FROM  " _
                        & "  public.sqd_det " _
                        & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                        & "  inner join en_mstr on en_id = sqd_en_id " _
                        & "  inner join si_mstr on si_id = sqd_si_id " _
                        & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                        & " where sqd_det.sqd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        'If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
        '    Dim _date As Date = sq_date.DateTime
        '    Dim _gcald_det_status As String = func_data.get_gcald_det_status(sq_en_id.EditValue, "gcald_so", _date)

        '    If _gcald_det_status = "" Then
        '        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    ElseIf _gcald_det_status.ToUpper = "Y" Then
        '        MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If
        footer()
        If SetString(sq_ptnr_id_sold.Tag) = "" Then
            MessageBox.Show("Customer can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If Trim(sq_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sSql As String
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                sSql = "SELECT loc_id " _
                       & "FROM " _
                       & "  public.loc_mstr a " _
                       & "WHERE " _
                       & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("sqd_loc_id") & " AND  " _
                       & "  a.loc_en_id=" & sq_en_id.EditValue

                If master_new.PGSqlConn.CekRowSelect(sSql) = 0 Then
                    MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If

                If SetString(sq_preorder_code.EditValue) = "" Then
                    sSql = "select count (*) as jml from preorder_mstr where preorder_active='Y' and preorder_pt_id=" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"))
                    If master_new.PGSqlConn.GetRowInfo(sSql)(0) > 0 Then
                        Box("Preorder can't blank")
                        sq_preorder_code.Focus()
                        Return False
                    End If
                Else
                    sSql = "select count (*) as jml from preorder_mstr where preorder_code=" & SetSetring(sq_preorder_code.EditValue) & " and preorder_active='Y' and preorder_pt_id=" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"))
                    If master_new.PGSqlConn.GetRowInfo(sSql)(0) = 0 Then
                        Box("Please check preorder number")
                        sq_preorder_code.Focus()
                        Return False
                    End If
                End If


            End If
        Next


        'cek inventory kalau terjadi penjualan cash
        'If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
        '    For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '        If func_coll.cek_inventory_allocation(ds_edit.Tables(0).Rows(i).Item("sqd_en_id"), ds_edit.Tables(0).Rows(i).Item("sqd_si_id"), _
        '                                            ds_edit.Tables(0).Rows(i).Item("sqd_loc_id"), ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"), _
        '                                            ds_edit.Tables(0).Rows(i).Item("pt_code"), ds_edit.Tables(0).Rows(i).Item("sqd_qty"), "''") = False Then
        '            Return False
        '        End If
        '    Next
        'End If

        If sq_type.GetColumnValue("value") = "D" Then
            If sq_need_date.Text = "" Then
                MessageBox.Show("Need Date Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If


        End If

        Return before_save
    End Function

#Region "GridView"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _sqd_qty, _sqd_qty_real, _sqd_um_conv, _sqd_qty_cost, _sqd_cost, _sqd_disc, _sqd_qty_shipment, _sqd_qty_transfer, _sqd_payment, _sqd_dp As Double
        Dim _sqd_pt_id As Integer

        _sqd_um_conv = 1
        _sqd_qty = 1
        _sqd_cost = 0
        _sqd_disc = 0
        _sqd_payment = 0
        _sqd_dp = 0
        _sqd_pt_id = -1

        If e.Column.Name = "sqd_qty" Then
            '********* Cek Qty Processed
            'Try
            '    _sqd_qty_shipment = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty_shipment"))
            'Catch ex As Exception
            'End Try

            'If e.Value < _sqd_qty_shipment Then
            '    MessageBox.Show("Qty SO Can't Lower Than Qty shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    gv_edit.CancelUpdateCurrentRow()
            '    Exit Sub
            'End If
            '********************************
            Try
                _sqd_pt_id = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_pt_id"))
            Catch ex As Exception
            End Try

            Try
                _sqd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _sqd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_cost"))
            Catch ex As Exception
            End Try

            Try
                _sqd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_disc"))
            Catch ex As Exception
            End Try

            _sqd_qty_real = e.Value * _sqd_um_conv
            _sqd_qty_cost = (e.Value * _sqd_cost) - (e.Value * _sqd_cost * _sqd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_real", _sqd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sq_pi_id.EditValue, _sqd_pt_id, sq_pay_type.EditValue, e.Value))

            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", dt_bantu.Rows(0).Item("pidd_payment"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", dt_bantu.Rows(0).Item("pidd_dp"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            End If

            'footer()
        ElseIf e.Column.Name = "sqd_cost" Then
            Try
                _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _sqd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_disc")))
            Catch ex As Exception
            End Try

            _sqd_qty_cost = (e.Value * _sqd_qty) - (e.Value * _sqd_qty * _sqd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            'footer()
        ElseIf e.Column.Name = "sqd_disc" Then
            Try
                _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _sqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_cost")))
            Catch ex As Exception
            End Try

            _sqd_qty_cost = (_sqd_cost * _sqd_qty) - (_sqd_cost * _sqd_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            footer()
        ElseIf e.Column.Name = "sqd_um_conv" Then
            'Try
            '    _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            'Catch ex As Exception
            'End Try

            '_sqd_qty_real = e.Value * _sqd_qty

            'gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_real", _sqd_qty_real)
            ''footer()
        ElseIf e.Column.Name = "sqd_taxable" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_inc", "N")
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_class_name", "NON-TAX")
            End If
            'footer()
        ElseIf e.Column.Name = "sqd_tax_inc" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_tax_inc", "N")
            End If
            'footer()
        ElseIf e.Column.Name = "sqd_pt_id" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty_real")))

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sq_pi_id.EditValue, e.Value, sq_pay_type.EditValue, _sqd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sqd_payment = 0 * _sqd_qty
                _sqd_dp = 0 * _sqd_qty
            Else
                _sqd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sqd_qty
                _sqd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sqd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "N" Then
                _sqd_payment = _sqd_payment + (_sqd_payment * _tax_rate)
                _sqd_dp = _sqd_dp + (_sqd_dp * _tax_rate)
            End If

            'If sq_type.EditValue = "D" Then
            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            End If
            'End If
            'footer()
        ElseIf e.Column.Name = "sqd_qty_real" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            Dim _pt_id As Integer
            Try
                _pt_id = gv_edit.GetRowCellValue(e.RowHandle, "sqd_pt_id")
            Catch ex As Exception
                Exit Sub
            End Try

            _sqd_qty = e.Value

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sq_pi_id.EditValue, _pt_id, sq_pay_type.EditValue, _sqd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sqd_payment = 0 * _sqd_qty
                _sqd_dp = 0 * _sqd_qty
            Else
                _sqd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sqd_qty
                _sqd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sqd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sqd_tax_inc").ToString.ToUpper = "N" Then
                _sqd_payment = _sqd_payment + (_sqd_payment * _tax_rate)
                _sqd_dp = _sqd_dp + (_sqd_dp * _tax_rate)
            End If

            If sq_type.EditValue = "D" Then
                If dt_bantu.Rows.Count > 0 Then
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", _sqd_payment)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", _sqd_dp)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_price", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_disc", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_payment", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_dp", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
                End If
            End If
            'footer()
        End If

    End Sub

    Private Function load_price_list(ByVal par_pi_id As Integer, ByVal par_pt_id As Integer, ByVal par_payment_type As Integer, ByVal par_min_qty As Double) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  pi_id, " _
                            & "  pidd_oid, " _
                            & "  pidd_pid_oid, " _
                            & "  pidd_payment_type, " _
                            & "  pidd_price, " _
                            & "  pidd_disc, " _
                            & "  pidd_dp, " _
                            & "  pidd_interval, " _
                            & "  coalesce(pidd_payment,0) as pidd_payment, " _
                            & "  pidd_min_qty, " _
                            & "  pidd_sales_unit, " _
                            & "  pid_pt_id " _
                            & "FROM  " _
                            & "  public.pidd_det " _
                            & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                            & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                            & "  where pi_id = " + par_pi_id.ToString _
                            & "  and pidd_payment_type = " + par_payment_type.ToString _
                            & "  and pid_pt_id = " + par_pt_id.ToString _
                            & "  and pidd_min_qty <= " + par_min_qty.ToString _
                            & "  order by pidd_min_qty desc "
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "pi_mstr")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _sqd_en_id As Integer = SetNumber(gv_edit.GetRowCellValue(_row, "sqd_en_id"))
        Dim _sqd_si_id As Integer = SetNumber(gv_edit.GetRowCellValue(_row, "sqd_si_id"))

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            If sq_is_package.EditValue = True Then
                Exit Sub
            End If
            Dim frm As New FSearchPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm._si_id = _sqd_si_id
            frm._sq_type = sq_type.EditValue
            'frm._tran_oid = _sq_ref_po_oid
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "um_name" Then
            'Dim frm As New FUMSearch
            'frm.set_win(Me)
            'frm._row = _row
            'frm._en_id = _sqd_en_id
            'frm.type_form = True
            'frm.ShowDialog()
        ElseIf _col = "sqd_tax_class_name" Then
            If gv_edit.GetRowCellValue(_row, "sqd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "sqd_ppn_type" Then
            Dim frm As New FPPNTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Dim _sqd_qty_shipment As Double = 0

        'Try
        '    _sqd_qty_shipment = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "sqd_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sqd_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        'cek_shipment_row()
        footer()
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        'Dim _sqd_qty_shipment As Double = 0

        'Try
        '    _sqd_qty_shipment = ((gv_edit.GetRowCellValue(0, "sqd_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sqd_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        'cek_shipment_row()
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "sqd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "sqd_en_id", sq_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", sq_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "sqd_si_id", sq_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", sq_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "sqd_is_additional_charge", "N")
            .SetRowCellValue(e.RowHandle, "sqd_qty", 1)
            .SetRowCellValue(e.RowHandle, "sqd_cost", 0)
            .SetRowCellValue(e.RowHandle, "sqd_price", 0)
            .SetRowCellValue(e.RowHandle, "sqd_disc", 0)
            '.SetRowCellValue(e.RowHandle, "sqd_taxable", IIf(sq_taxable.EditValue = True, "Y", "N"))
            '.SetRowCellValue(e.RowHandle, "sqd_tax_inc", IIf(sq_tax_inc.EditValue = True, "Y", "N"))
            '.SetRowCellValue(e.RowHandle, "sqd_tax_class", sq_tax_class.EditValue)
            '.SetRowCellValue(e.RowHandle, "sqd_tax_class_name", sq_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "sqd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "sqd_qty_real", 1)
            .SetRowCellValue(e.RowHandle, "sqd_qty_cost", 0)

            .SetRowCellValue(e.RowHandle, "sqd_payment", 0)
            .SetRowCellValue(e.RowHandle, "sqd_dp", 0)
            .SetRowCellValue(e.RowHandle, "sqd_sales_unit", 0)
            .BestFitColumns()
        End With

        sq_cu_id.Enabled = False
        sq_type.Enabled = False
        sq_pi_id.Enabled = False
        sq_pay_type.Enabled = False
    End Sub

#End Region

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Sub cancel_line()
        'walaupun
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString
        _colom = "sq_trans_id"
        _table = "sq_mstr"
        _criteria = "sq_code"
        _initial = "sq"
        _type = "sq"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Cancel Line SQ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(sq_code) as jml " _
                                         & " from sq_mstr " _
                                         & " inner join sqd_det on sqd_sq_oid = sq_oid " _
                                         & " where sq_code ~~* '" + par_code + "' " _
                                         & " and sqd_qty_transfer >= 1"

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _jml = .DataReader("jml").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _jml > 0 Then
            MessageBox.Show("Can't Cancel For Shipment SQ which has been transfered...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                               " where " + par_criteria + " = '" + par_code + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        row = BindingContext(ds.Tables(0)).Position
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
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
    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
                                           " where substring(cast(ptnr_id as varchar),5,100) <> ''"
                    .InitializeCommand()

                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _sq_oid As Guid
        _sq_oid = Guid.NewGuid
        Dim _sq_code, _sq_terbilang As String
        Dim _sq_total, _sq_total_ppn, _sq_total_pph, _sqd_qty, _sqd_price, _sqd_disc, _sq_total_temp, _tax_rate As Double
        Dim i As Integer


        If cek_duplikat_pt_id(gv_edit, "sqd_pt_id") = False Then
            Return False
        End If

        _sq_code = func_coll.get_transaction_number("SQ", sq_en_id.GetColumnValue("en_code"), "sq_mstr", "sq_code")

        ssqls.Clear()

        Dim _sq_trn_status As String
        Dim ds_bantu As New DataSet

        If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
            '_sq_trn_status = "C" 'Lansung Default Ke Close
            _sq_trn_status = "D"
        Else
            _sq_trn_status = "D" 'Lansung Default Ke Draft
        End If


        '******* Mencari Total so, Total PPN, Total PPH
        _sq_total = 0
        _sq_total_ppn = 0
        _sq_total_pph = 0
        _sq_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            ''disini hanya line ppn saja
            ' _cost kalau disini diganti jadi price..karena dihitung dari price.....
            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class"))
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
            _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

            _sq_total = _sq_total + ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
        Next

        'disini dihitung ppn dan pph nya
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
                Else
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
                End If

                _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
                _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

                _sq_total_temp = ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                _sq_total_ppn = _sq_total_ppn + (_sq_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
                _sq_total_pph = _sq_total_pph + (_sq_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            End If
        Next
        '*******************************************************

        _sq_terbilang = func_bill.TERBILANG_FIX(_sq_total + _sq_total_ppn - _sq_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sqd_dp") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sqd_payment") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
        Next
        '*************************

        'Deklarasi varible untuk customer baru
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid


        Dim _ptnr_id As Integer

        _ptnr_id = SetInteger(GetID_Local(sq_en_id.GetColumnValue("en_code")))

        Dim _ptnr_code As String
        _ptnr_code = "CU" + "00"
        'Return False
        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sq_en_id.GetColumnValue("en_code") = 0, "99", sq_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        '*************************************

        Dim _id_ptnr_sq As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sq_mstr " _
                                            & "( " _
                                            & "  sq_oid,sq_preorder_code,sq_dropship_address, " _
                                            & "  sq_dom_id, " _
                                            & "  sq_en_id, " _
                                            & "  sq_add_by, " _
                                            & "  sq_add_date, " _
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
                                            & "  sq_cons, " _
                                            & "  sq_ar_ac_id, " _
                                            & "  sq_ar_sb_id, " _
                                            & "  sq_ar_cc_id, " _
                                            & "  sq_dp, " _
                                            & "  sq_disc_header, " _
                                            & "  sq_total, " _
                                            & "  sq_need_date, " _
                                            & "  sq_tran_id, " _
                                            & "  sq_trans_id, " _
                                            & "  sq_trans_rmks, " _
                                            & "  sq_dt, " _
                                            & "  sq_cu_id, " _
                                            & "  sq_total_ppn, " _
                                            & "  sq_total_pph, " _
                                            & "  sq_payment, " _
                                            & "  sq_exc_rate, " _
                                            & "  sq_interval,sq_is_package,sq_pt_id,sq_price, " _
                                            & "  sq_terbilang " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sq_oid.ToString) & ",  " _
                                            & SetSetring(sq_preorder_code.EditValue) & ", " _
                                            & SetSetring(sq_dropship_address.EditValue) & ", " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sq_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_sq_code) & ",  " _
                                            & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                            & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                            & SetSetring(sq_ref_po_code.Text) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetDate(sq_date.DateTime) & ",  " _
                                            & SetInteger(sq_si_id.EditValue) & ",  " _
                                            & SetSetring(sq_type.EditValue) & ",  " _
                                            & SetInteger(sq_sales_person.EditValue) & ",  " _
                                            & SetInteger(sq_pi_id.EditValue) & ",  " _
                                            & SetInteger(sq_pay_type.EditValue) & ",  " _
                                            & SetInteger(sq_pay_method.EditValue) & ",  " _
                                            & SetSetring("Y") & ",  " _
                                            & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                            & SetDbl(_total_dp) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(_sq_total) & ",  " _
                                            & SetDate(sq_need_date.DateTime) & ",  " _
                                            & SetInteger("") & ",  " _
                                            & SetSetring(_sq_trn_status) & ",  " _
                                            & SetSetring(sq_trans_rmks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(sq_cu_id.EditValue) & ",  " _
                                            & SetDbl(_sq_total_ppn) & ",  " _
                                            & SetDbl(_sq_total_pph) & ",  " _
                                            & SetDbl(_total_payment) & ",  " _
                                            & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                            & SetInteger(sq_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                            & SetBitYN(sq_is_package.EditValue) & ",  " _
                                            & SetInteger(sq_pt_id.Tag) & ",  " _
                                            & SetDec(sq_price.EditValue) & ",  " _
                                            & SetSetring(_sq_terbilang) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sqd_det " _
                                                & "( " _
                                                & "  sqd_oid, " _
                                                & "  sqd_dom_id, " _
                                                & "  sqd_en_id, " _
                                                & "  sqd_add_by, " _
                                                & "  sqd_add_date, " _
                                                & "  sqd_sq_oid, " _
                                                & "  sqd_seq, " _
                                                & "  sqd_is_additional_charge, " _
                                                & "  sqd_si_id, " _
                                                & "  sqd_pt_id, " _
                                                & "  sqd_rmks, " _
                                                & "  sqd_qty, " _
                                                & "  sqd_um, " _
                                                & "  sqd_cost, " _
                                                & "  sqd_price, " _
                                                & "  sqd_disc, " _
                                                & "  sqd_sales_ac_id, " _
                                                & "  sqd_sales_sb_id, " _
                                                & "  sqd_sales_cc_id, " _
                                                & "  sqd_um_conv, " _
                                                & "  sqd_qty_real, " _
                                                & "  sqd_taxable, " _
                                                & "  sqd_tax_inc, " _
                                                & "  sqd_tax_class, " _
                                                & "  sqd_ppn_type, " _
                                                & "  sqd_dt, " _
                                                & "  sqd_payment, " _
                                                & "  sqd_dp, " _
                                                & "  sqd_loc_id, " _
                                                & "  sqd_sales_unit, " _
                                                & "  sqd_pod_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_sq_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid").ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(sq_preorder_code.EditValue) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE   " _
                                                    & "  public.preorder_mstr " _
                                                    & "set preorder_qty_sq=coalesce(preorder_qty_sq,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                    & "Where preorder_code=" & SetSetring(sq_preorder_code.EditValue) & " and preorder_pt_id=" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next



                        'Dim _oid_ptnr As String



                        'If sq_type.EditValue.ToString.ToUpper = "D" Then
                        '    If insert_sokp_piutang(objinsert, _sq_oid.ToString, _sq_code, _total_dp, _total_payment) = False Then
                        '        sqlTran.Rollback()
                        '        insert = False
                        '        Exit Function
                        '    End If
                        'End If


                        'apabila chash maka langsung bisa generate sales order shipment....
                        'If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
                        '    If insert_shipment(objinsert, _sq_oid.ToString, _sq_code) = False Then
                        '        sqlTran.Rollback()
                        '        insert = False
                        '        Exit Function
                        '    End If

                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "update sq_mstr set sq_close_date = current_date " _
                        '                         & " where sq_code = " + SetSetring(_sq_code)
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'End If

                        '' ''Update so mstr sq_status = null apabila terjadi perubahan qty atau edit data so nambah line
                        ' ''.Command.CommandType = CommandType.Text
                        ' ''.Command.CommandText = "update sq_mstr set sq_close_date = null " _
                        ' ''                     & " where sq_code = coalesce((select distinct sq_code from sqd_det " _
                        ' ''                     & " inner join sq_mstr on sq_oid = sqd_sq_oid " _
                        ' ''                     & " where sqd_sq_oid = '" + _sq_oid.ToString + "'" _
                        ' ''                     & " and  sqd_qty <> coalesce(sqd_qty_shipment,0)),'') "
                        ' ''ssqls.Add(.Command.CommandText)
                        ' ''.Command.ExecuteNonQuery()
                        ' ''.Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SQ " & _sq_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        set_row(_sq_oid.ToString, "sq_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                    .Dispose()
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Private Function insert_sokp_piutang(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_ref As String, ByVal par_dp As Double, ByVal par_amount As Double) As Boolean
        insert_sokp_piutang = True
        Dim i, _interval As Integer

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_usr_1 From code_mstr " + _
                                           " where code_field = 'payment_type' " + _
                                           " and code_id = " + sq_pay_type.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _interval = .DataReader("code_usr_1").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If sq_need_date.Text = "" Then
            MessageBox.Show("Need Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _date As Date = sq_need_date.DateTime

        With par_obj
            Try
                'Untuk Insert Yang DP
                'DP harus masuk juga ke kartu piutang....agar pada saat ar jadi pas..
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.sokp_piutang " _
                                    & "( " _
                                    & "  sokp_oid, " _
                                    & "  sokp_sq_oid, " _
                                    & "  sokp_seq, " _
                                    & "  sokp_ref, " _
                                    & "  sokp_amount, " _
                                    & "  sokp_due_date, " _
                                    & "  sokp_amount_pay, " _
                                    & "  sokp_description " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(par_sq_oid) & ",  " _
                                    & SetInteger(0) & ",  " _
                                    & SetSetring(par_ref) & ",  " _
                                    & SetDbl(par_dp) & ",  " _
                                    & SetDate(_date) & "," _
                                    & SetDbl(0) & ",  " _
                                    & SetSetring("-") & "  " _
                                    & ")"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                For i = 0 To _interval - 1
                    _date = _date.AddMonths(1)

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.sokp_piutang " _
                                        & "( " _
                                        & "  sokp_oid, " _
                                        & "  sokp_sq_oid, " _
                                        & "  sokp_seq, " _
                                        & "  sokp_ref, " _
                                        & "  sokp_amount, " _
                                        & "  sokp_due_date, " _
                                        & "  sokp_amount_pay, " _
                                        & "  sokp_description " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(par_sq_oid) & ",  " _
                                        & SetInteger(i + 1) & ",  " _
                                        & SetSetring(par_ref) & ",  " _
                                        & SetDbl(par_amount) & ",  " _
                                        & SetDate(_date) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetSetring("-") & "  " _
                                        & ")"
                    ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        Return insert_sokp_piutang
    End Function

    Public Overrides Function edit_data() As Boolean
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_trans_id") <> "D" Then
            MessageBox.Show("Can't Edit Sales Quotation That Has Been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Dim i As Integer
        For i = 0 To ds.Tables("detail").Rows.Count - 1
            If SetNumber(ds.Tables("detail").Rows(i).Item("sqd_qty_transfer")) > 0 Then
                MessageBox.Show("Data already transfer..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sq_oid_mstr = .Item("sq_oid").ToString
                sq_ar_sb_id.EditValue = CInt(.Item("sq_ar_sb_id"))
                sq_ar_ac_id.EditValue = CInt(.Item("sq_ar_ac_id"))
                sq_ar_cc_id.EditValue = CInt(.Item("sq_ar_cc_id"))
                sq_cu_id.EditValue = .Item("sq_cu_id")
                sq_en_id.EditValue = .Item("sq_en_id")
                sq_date.DateTime = .Item("sq_date")
                sq_exc_rate.EditValue = .Item("sq_exc_rate")
                sq_pay_method.EditValue = .Item("sq_pay_method")
                sq_pay_type.EditValue = .Item("sq_pay_type")
                sq_need_date.DateTime = .Item("sq_need_date")
                sq_pi_id.EditValue = .Item("sq_pi_id")
                sq_ptnr_id_sold.Tag = SetInteger(.Item("sq_ptnr_id_sold"))
                sq_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
                sq_ref_po_code.Text = SetString(.Item("sq_ref_po_code"))
                sq_ref_po_code.Enabled = True
                sq_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
                sq_sales_person.EditValue = .Item("sq_sales_person")
                sq_si_id.EditValue = .Item("sq_si_id")
                sq_trans_rmks.Text = SetString(.Item("sq_trans_rmks"))
                sq_type.EditValue = .Item("sq_type")

                sq_is_package.EditValue = SetBitYNB(.Item("sq_is_package"))
                sq_dropship_address.EditValue = .Item("sq_dropship_address")
                sq_preorder_code.EditValue = .Item("sq_preorder_code")

                sq_pt_id.Tag = SetString(.Item("sq_pt_id"))
                sq_pt_id.EditValue = .Item("pt_code")
                pt_desc1.EditValue = .Item("pt_desc1")
                pt_desc2.EditValue = .Item("pt_desc2")
                sq_price.EditValue = .Item("sq_price")

                If sq_is_package.EditValue = True Then
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                    gv_edit.BestFitColumns()
                Else
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = True
                    gv_edit.BestFitColumns()
                End If
            End With
            sq_en_id.Focus()
            sq_cu_id.Enabled = False
            sq_ptnr_id_sold.Enabled = False
            sq_bantu_address.Enabled = False
            sq_pay_type.Enabled = False
            sq_type.Enabled = False
            sq_pi_id.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
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
                            & "  sqd_ppn_type, " _
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
                            & "  pt_type, " _
                            & "  pt_ls, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sqd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sqd_det " _
                            & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                            & "  inner join en_mstr on en_id = sqd_en_id " _
                            & "  inner join si_mstr on si_id = sqd_si_id " _
                            & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                            & " where public.sqd_det.sqd_sq_oid = '" + ds.Tables(0).Rows(row).Item("sq_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If

    End Function

    Public Overrides Function edit()
        edit = True
        Dim _sq_total, _sqd_qty, _sqd_price, _sqd_disc, _sq_total_ppn, _sq_total_pph, _sq_total_temp, _tax_rate As Double
        Dim _sqd_qty_shipment As Double = 0
        Dim i As Integer
        Dim _sq_terbilang As String

        Dim _sq_trn_status As String
        Dim ds_bantu As New DataSet
        _sq_trn_status = "D" 'set default langsung ke D


        ssqls.Clear()

        '******* Mencari Total so, Total PPN, Total PPH
        _sq_total = 0
        _sq_total_ppn = 0
        _sq_total_pph = 0
        _sq_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class"))
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
            _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

            _sq_total = _sq_total + ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
                Else
                    _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
                End If

                _sqd_qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
                _sqd_disc = ds_edit.Tables(0).Rows(i).Item("sqd_disc")

                _sq_total_temp = ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                _sq_total_ppn = _sq_total_ppn + (_sq_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
                _sq_total_pph = _sq_total_pph + (_sq_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            End If
        Next
        '*******************************************************

        _sq_terbilang = func_bill.TERBILANG_FIX(_sq_total + _sq_total_ppn - _sq_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sqd_dp") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sqd_payment") * ds_edit.Tables(0).Rows(i).Item("sqd_qty"))
        Next
        '*************************

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        For i = 0 To ds.Tables("detail").Rows.Count - 1
                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_preorder_code")) <> "" Then
                                .Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sqd_pod_oid from sqd_det where sqd_sq_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString) + ")"


                                .Command.CommandText = "UPDATE   " _
                                               & "  public.preorder_mstr " _
                                               & "set preorder_qty_sq=coalesce(preorder_qty_sq,0) - " & SetDbl(ds.Tables("detail").Rows(i).Item("sqd_qty")) _
                                               & "Where preorder_code=" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_preorder_code")) & " and preorder_pt_id=" & SetInteger(ds.Tables("detail").Rows(i).Item("sqd_pt_id"))


                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.sq_mstr   " _
                                            & "SET  " _
                                            & "  sq_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  sq_en_id = " & SetInteger(sq_en_id.EditValue) & ",  " _
                                            & "  sq_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sq_preorder_code = " & SetSetring(sq_preorder_code.EditValue) & ",  " _
                                            & "  sq_dropship_address = " & SetSetring(sq_dropship_address.EditValue) & ",  " _
                                            & "  sq_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_date = " & SetDate(sq_date.DateTime) & ",  " _
                                            & "  sq_ptnr_id_sold = " & SetInteger(sq_ptnr_id_sold.Tag) & ",  " _
                                            & "  sq_ref_po_code = " & SetSetring(sq_ref_po_code.Text) & ",  " _
                                            & "  sq_ref_po_oid = " & SetSetring("") & ",  " _
                                            & "  sq_si_id = " & SetInteger(sq_si_id.EditValue) & ",  " _
                                            & "  sq_sales_person = " & SetInteger(sq_sales_person.EditValue) & ",  " _
                                            & "  sq_pay_method = " & SetInteger(sq_pay_method.EditValue) & ",  " _
                                            & "  sq_cons = " & SetSetring("Y") & ",  " _
                                            & "  sq_ar_ac_id = " & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                            & "  sq_ar_sb_id = " & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                            & "  sq_ar_cc_id = " & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                            & "  sq_dp = " & SetDbl(_total_dp) & ",  " _
                                            & "  sq_total = " & SetDbl(_sq_total) & ",  " _
                                            & "  sq_need_date = " & SetDate(sq_need_date.DateTime) & ",  " _
                                            & "  sq_tran_id = " & SetInteger("") & ",  " _
                                            & "  sq_trans_id = " & SetSetring(_sq_trn_status) & ",  " _
                                            & "  sq_trans_rmks = " & SetSetring(sq_trans_rmks.Text) & ",  " _
                                            & "  sq_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_total_ppn = " & SetDbl(_sq_total_ppn) & ",  " _
                                            & "  sq_total_pph = " & SetDbl(_sq_total_pph) & ",  " _
                                            & "  sq_payment = " & SetDbl(_total_payment) & ",  " _
                                            & "  sq_exc_rate = " & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                            & "  sq_is_package = " & SetBitYN(sq_is_package.EditValue) & ",  " _
                                            & "  sq_pt_id = " & SetInteger(sq_pt_id.Tag) & ",  " _
                                            & "  sq_price = " & SetDec(sq_price.EditValue) & ",  " _
                                            & "  sq_terbilang = " & SetSetring(_sq_terbilang) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sq_oid = " & SetSetring(_sq_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from sqd_det  " _
                                            & "WHERE  " _
                                            & "  sqd_sq_oid = " & SetSetring(_sq_oid_mstr) & " and sqd_qty_transfer is null "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            ' _sqd_qty_shipment = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment")) = True, 0, ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment"))

                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_qty_transfer")) = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.sqd_det " _
                                                    & "( " _
                                                    & "  sqd_oid, " _
                                                    & "  sqd_dom_id, " _
                                                    & "  sqd_en_id, " _
                                                    & "  sqd_add_by, " _
                                                    & "  sqd_add_date, " _
                                                    & "  sqd_sq_oid, " _
                                                    & "  sqd_seq, " _
                                                    & "  sqd_is_additional_charge, " _
                                                    & "  sqd_si_id, " _
                                                    & "  sqd_pt_id, " _
                                                    & "  sqd_rmks, " _
                                                    & "  sqd_qty, " _
                                                    & "  sqd_um, " _
                                                    & "  sqd_cost, " _
                                                    & "  sqd_price, " _
                                                    & "  sqd_disc, " _
                                                    & "  sqd_sales_ac_id, " _
                                                    & "  sqd_sales_sb_id, " _
                                                    & "  sqd_sales_cc_id, " _
                                                    & "  sqd_um_conv, " _
                                                    & "  sqd_qty_real, " _
                                                    & "  sqd_taxable, " _
                                                    & "  sqd_tax_inc, " _
                                                    & "  sqd_tax_class, " _
                                                    & "  sqd_ppn_type, " _
                                                    & "  sqd_dt, " _
                                                    & "  sqd_payment, " _
                                                    & "  sqd_dp, " _
                                                    & "  sqd_loc_id, " _
                                                    & "  sqd_sales_unit " _
                                                    & "   " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_sq_oid_mstr.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.sqd_det   " _
                                                    & "SET  " _
                                                    & "  sqd_en_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                    & "  sqd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                                                    & "  sqd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                                                    & "  sqd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                                                    & "  sqd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                                                    & "  sqd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                                                    & "  sqd_price = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                                                    & "  sqd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                                                    & "  sqd_sales_ac_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                                                    & "  sqd_sales_sb_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                                                    & "  sqd_sales_cc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                                                    & "  sqd_um_conv = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                                                    & "  sqd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                                                    & "  sqd_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                                                    & "  sqd_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                                                    & "  sqd_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                                                    & "  sqd_ppn_type = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                                                    & "  sqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  sqd_payment = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                                                    & "  sqd_dp = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                                                    & "  sqd_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                                                    & "  sqd_sales_unit = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  sqd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If SetString(sq_preorder_code.EditValue) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE   " _
                                                    & "  public.preorder_mstr " _
                                                    & "set preorder_qty_sq=coalesce(preorder_qty_sq,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                    & "Where preorder_code=" & SetSetring(sq_preorder_code.EditValue) & " and preorder_pt_id=" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next


                        'If sq_type.EditValue.ToString.ToUpper = "D" Then
                        '    If edit_sokp_piutang(objinsert, _sq_oid_mstr.ToString, _total_payment) = False Then
                        '        sqlTran.Rollback()
                        '        edit = False
                        '        Exit Function
                        '    End If
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit SQ " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_sq_oid_mstr, "sq_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                    .Dispose()
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Private Function edit_sokp_piutang(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_amount As Double) As Boolean
        edit_sokp_piutang = True

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.sokp_piutang   " _
                                    & "SET  " _
                                    & "  sokp_amount = " & SetDbl(par_amount) & "  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  sokp_sq_oid = " & SetSetring(par_sq_oid) & " "
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        Return edit_sokp_piutang
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select coalesce(sqd_qty_shipment,0) as sqd_qty_shipment from sqd_det " + _
                           " where sqd_sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sqd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("sqd_qty_shipment") > 0 Then
                MessageBox.Show("Can't Delete Shipment Sales Order...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
    End Function

    Public Overrides Function delete_data() As Boolean
        help_load_data(True)

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_trans_id") <> "D" Then
            MessageBox.Show("Can't Delete Sales Quotation That Has Been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        ssqls.Clear()

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                                If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_preorder_code")) <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "UPDATE   " _
                                                   & "  public.preorder_mstr " _
                                                   & "set preorder_qty_sq=coalesce(preorder_qty_sq,0) - " & SetDbl(ds.Tables("detail").Rows(i).Item("sqd_qty")) _
                                                   & "Where preorder_code=" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_preorder_code")) & " and preorder_pt_id=" & SetInteger(ds.Tables("detail").Rows(i).Item("sqd_pt_id"))


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            Next
                            'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sqd_pod_oid from sqd_det where sqd_sq_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString) + ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '******************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from sq_mstr where sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete SO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_preorder_code")) <> "" Then

                            End If

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                        .Dispose()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Sub sq_ptnr_id_sold_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_ptnr_id_sold.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = sq_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        footer()
    End Sub

    Private Sub gv_edit_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.RowCountChanged
        footer()
    End Sub

    Private Sub footer()
        Dim i As Integer
        Dim _dpp As Double = 0
        Dim _dpp_line As Double = 0
        Dim _discount As Double = 0
        Dim _discount_line As Double = 0
        Dim _ppn As Double = 0
        Dim _tax_rate As Double = 0
        Dim _sqd_price As Double = 0

        If ds_edit.Tables.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sqd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) = True, 0, func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")))
            Else
                _tax_rate = 0
            End If

            If ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc").ToString.ToUpper = "Y" Then
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_price") / (1 + _tax_rate)))
            Else
                _sqd_price = ds_edit.Tables(0).Rows(i).Item("sqd_price")
            End If

            _dpp = _dpp + (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price)
            _dpp_line = (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price)
            _discount = _discount + (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
            _discount_line = (ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * _sqd_price * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
            _ppn = _ppn + (_tax_rate * (_dpp_line - _discount_line))
        Next

        te_dpp.EditValue = _dpp
        te_discount.EditValue = _discount
        te_ppn.EditValue = _ppn
        te_total.EditValue = _dpp - _discount + _ppn
    End Sub

    Private Sub sq_ref_po_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If func_coll.get_conf_file("ref_po_sales_order") = "1" Then
            If sq_ptnr_id_sold.Tag = "" Then
                MessageBox.Show("Please Specipy Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim frm As New FPurchaseOrderSearch()
                frm.set_win(Me)
                frm._obj = sq_ref_po_code
                frm._sq_ptnr_id_sold_mstr = sq_ptnr_id_sold.Tag
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_en_id")
        '_type = 10
        '_table = "sq_mstr"
        '_initial = "so"
        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        'Dim ds_bantu As New DataSet
        'Dim _sql As String

        '_sql = "SELECT  " _
        '    & "  sq_mstr.sq_code, " _
        '    & "  sq_mstr.sq_ptnr_id_sold, " _
        '    & "  sq_mstr.sq_date, " _
        '    & "  sq_mstr.sq_ptnr_id_bill, " _
        '    & "  sq_mstr.sq_pay_type, " _
        '    & "  sq_mstr.sq_pay_method, " _
        '    & "  sqd_det.sqd_pt_id, " _
        '    & "  sqd_det.sqd_qty, " _
        '    & "  sqd_det.sqd_sq_oid, " _
        '    & "  sqd_det.sqd_um, " _
        '    & "  ptnr_mstr.ptnr_code, " _
        '    & "  ptnr_mstr.ptnr_name, " _
        '    & "  ptnr_mstr.ptnr_id, " _
        '    & "  ptnra_addr.ptnra_id, " _
        '    & "  ptnra_addr.ptnra_line, " _
        '    & "  ptnra_addr.ptnra_line_1, " _
        '    & "  ptnra_addr.ptnra_line_2, " _
        '    & "  ptnra_addr.ptnra_line_3, " _
        '    & "  pt_mstr.pt_id, " _
        '    & "  pt_mstr.pt_code, " _
        '    & "  pt_mstr.pt_desc1, " _
        '    & "  pt_mstr.pt_um, " _
        '    & "  code_mstr.code_id, " _
        '    & "  code_mstr.code_code, " _
        '    & "  code_mstr.code_field, " _
        '    & "  sq_mstr.sq_oid, " _
        '    & "  ptnra_addr.ptnra_oid, " _
        '    & "  ptnrac_cntc.ptnrac_oid, " _
        '    & "  ptnrac_cntc.ptnrac_contact_name, " _
        '    & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        '    & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        '    & "FROM " _
        '    & "  sq_mstr " _
        '    & "  INNER JOIN sqd_det ON (sq_mstr.sq_oid = sqd_det.sqd_sq_oid) " _
        '    & "  INNER JOIN ptnr_mstr ON (sq_mstr.sq_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
        '    & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
        '    & "  INNER JOIN pt_mstr ON (sqd_det.sqd_pt_id = pt_mstr.pt_id) " _
        '    & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
        '    & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
        '    & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = sq_oid " _
        '    & "WHERE " _
        '    & "sq_mstr.sq_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code") + "'"

        'Dim frm As New frmPrintDialog
        'frm._ssql = _sql
        'frm._report = "XRSO"
        'frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        'frm.ShowDialog()

        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_en_id")
        _type = 10
        _table = "sq_mstr"
        _initial = "sq"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  sq_mstr.sq_code, " _
            & "  sq_mstr.sq_ptnr_id_sold, " _
            & "  sq_mstr.sq_date, " _
            & "  sq_mstr.sq_ptnr_id_bill, " _
            & "  sq_mstr.sq_pay_type, " _
            & "  sq_mstr.sq_pay_method, " _
            & "  sqd_det.sqd_pt_id, " _
            & "  sqd_det.sqd_qty, " _
            & "  sqd_det.sqd_sq_oid, " _
            & "  sqd_det.sqd_um, " _
            & "  ptnr_mstr.ptnr_code, " _
            & "  ptnr_mstr.ptnr_name, " _
            & "  ptnr_mstr.ptnr_id, " _
            & "  ptnra_addr.ptnra_id, " _
            & "  ptnra_addr.ptnra_line, " _
            & "  ptnra_addr.ptnra_line_1, " _
            & "  ptnra_addr.ptnra_line_2, " _
            & "  ptnra_addr.ptnra_line_3, " _
            & "  pt_mstr.pt_id, " _
            & "  pt_mstr.pt_code, " _
            & "  pt_mstr.pt_desc1, " _
            & "  pt_mstr.pt_um, " _
            & "  code_mstr.code_id, " _
            & "  code_mstr.code_code as um_name, " _
            & "  code_mstr.code_field, " _
            & "  sq_mstr.sq_oid, " _
            & "  ptnra_addr.ptnra_oid, " _
            & "  ptnrac_cntc.ptnrac_oid, " _
            & "  ptnrac_cntc.ptnrac_contact_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM " _
            & "  sq_mstr " _
            & "  INNER JOIN sqd_det ON (sq_mstr.sq_oid = sqd_det.sqd_sq_oid) " _
            & "  INNER JOIN ptnr_mstr ON (sq_mstr.sq_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
            & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
            & "  INNER JOIN pt_mstr ON (sqd_det.sqd_pt_id = pt_mstr.pt_id) " _
            & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
            & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = sq_oid " _
            & "WHERE " _
            & "sq_mstr.sq_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRSQ"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        frm.ShowDialog()

    End Sub


    Private Sub le_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_en_id.EditValueChanged
        Try
            init_le(le_cus_id, "cus_mstr", le_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        Try
            'load_data_to_detail()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
   
    Private Sub load_data_to_detail()
        Try
            Try
                If ds.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                End If
            Catch ex As Exception
                Exit Sub
            End Try

            Dim sql As String
            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
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
                & "  sb_desc, " _
                & "  cc_desc, " _
                & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                & "  tax_class.code_name as sqd_tax_class_name, " _
                & "  sqd_ppn_type, " _
                & "  loc_desc " _
                & "FROM  " _
                & "  public.sqd_det " _
                & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                & "  inner join en_mstr on en_id = sqd_en_id " _
                & "  inner join si_mstr on si_id = sqd_si_id " _
                & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "'"

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            load_data_to_detail()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        MyBase.help_load_data(par)
        gc_detail.DataSource = Nothing
        load_data_to_detail()
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "loc_desc" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
                    ds_edit.Tables(0).Rows(i).Item("sqd_loc_id") = gv_edit.GetRowCellValue(_row, "sqd_loc_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub file_excel_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_excel.ButtonClick
        Try
            Dim dt_temp As New DataTable
            Dim _row As Integer = 0

            Dim ssql As String
            Dim ds As New DataSet
            Dim _file As String = AskOpenFile("Format import data Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            file_excel.EditValue = _file
            ds = master_new.excelconn.ImportExcel(_file)

            ds_edit.Tables(0).Rows.Clear()
            ds_edit.AcceptChanges()

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

            For Each dr As DataRow In ds.Tables(0).Rows
                ssql = "SELECT  distinct " _
                   & "  en_id, " _
                   & "  en_desc, " _
                   & "  si_desc, " _
                   & "  pt_id, " _
                   & "  pt_code, " _
                   & "  pt_desc1, " _
                   & "  pt_desc2, " _
                   & "  pt_cost, " _
                   & "  invct_cost, " _
                   & "  pt_price, " _
                   & "  pt_type, " _
                   & "  pt_um, " _
                   & "  pt_pl_id, " _
                   & "  pt_ls, " _
                   & "  pt_loc_id, " _
                   & "  loc_desc, " _
                   & "  pt_taxable, " _
                   & "  pt_tax_inc, " _
                   & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
                   & "  tax_class_mstr.code_name as tax_class_name, " _
                   & "  pt_ppn_type, " _
                   & "  um_mstr.code_name as um_name " _
                   & "FROM  " _
                   & "  public.pt_mstr" _
                   & " inner join en_mstr on en_id = pt_en_id " _
                   & " inner join loc_mstr on loc_id = pt_loc_id " _
                   & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
                   & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
                   & " inner join invct_table on invct_pt_id = pt_id " _
                   & " inner join si_mstr on si_id = invct_si_id " _
                   & " where pt_code ='" & dr("pt_code") & "' "

                dt_temp = master_new.PGSqlConn.GetTableData(ssql)

                For Each dr_temp As DataRow In dt_temp.Rows

                    Dim ds_bantu As New DataSet

                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                    & "From pla_mstr  " _
                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                    & "where pla_pl_id = " + dr_temp("pt_pl_id").ToString _
                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                .InitializeCommand()
                                .FillDataSet(ds_bantu, "prodline")

                                If ds_bantu.Tables(0).Rows.Count = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                    Exit Sub
                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                    Exit Sub
                                End If

                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                    gv_edit.AddNewRow()
                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    gv_edit.SetRowCellValue(_row, "sqd_pt_id", dr_temp("pt_id"))
                    gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                    gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                    gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                    gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                    gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                    gv_edit.SetRowCellValue(_row, "sqd_loc_id", dr_temp("pt_loc_id"))
                    gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                    gv_edit.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
                    gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                    gv_edit.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

                    'If _so_type <> "D" Then
                    '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                    'End If

                    gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                    gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                    gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                    gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                    gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                    gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                    gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                    gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                    gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                    gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                    gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
                    gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

                    gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
                    gv_edit.SetRowCellValue(_row, "sqd_qty", dr("qty"))
                    gv_edit.SetRowCellValue(_row, "sqd_disc", dr("disc"))

                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                    _row = _row + 1
                    System.Windows.Forms.Application.DoEvents()
                    'Exit Sub
                Next
            Next
        Catch ex As Exception
            Pesan(Err)
        End Try
       
    End Sub

    Private Sub sq_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_pt_id.ButtonClick
        Try
            If sq_is_package.EditValue = False Then
                Box("Please check is package")
                Exit Sub
            End If

            Dim frm As New FSearchPartNumberSearch
            frm.set_win(Me)
            'frm._pt_id = so_pt_id.Tag
            frm._en_id = sq_en_id.EditValue
            frm._si_id = sq_si_id.EditValue
            frm._sq_type = sq_type.EditValue
            frm.grid_call = "header"
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_is_package_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_is_package.EditValueChanged
        Try
            If sq_is_package.EditValue = True Then
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                gv_edit.BestFitColumns()
            Else
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = True
                gv_edit.BestFitColumns()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_preorder_code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_preorder_code.ButtonClick
        Try
            Dim frm As New FPreOrderSearch
            frm.set_win(Me)
            'frm._pt_id = so_pt_id.Tag
            frm._obj = sq_preorder_code
            'frm._si_id = sq_si_id.EditValue
            'frm._sq_type = sq_type.EditValue
            'frm.grid_call = "header"
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sq_dropship_address_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles sq_dropship_address.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then
                e.Handled = True
                e.SuppressKeyPress = True
                sq_dropship_address.EditValue = sq_dropship_address.EditValue & vbNewLine
                sq_dropship_address.Focus()


            End If

        Catch ex As Exception

        End Try
    End Sub
End Class

