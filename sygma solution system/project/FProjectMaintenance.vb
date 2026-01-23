Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FProjectMaintenance
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _pocust_oid As String
    Public ds_edit, ds_edit_cust As DataSet
    Dim _now As DateTime
    Dim _conf_budget, _conf_value As String
    Public _prj_oid_master, _prj_code_master As String
    Dim _prj_code_pre_edit As String
    Dim _prj_date_pre_edit As Date
    Dim mf As New master_new.ModFunction
    Dim ds_get As DataSet
    Dim _prj_tran_id_pre_edit As Integer

#Region "SettingAwal"
    Private Sub FProjectMaintenance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        _conf_value = func_coll.get_conf_file("wf_project_maintenance")
        tcg_detail.TabPages(1).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    'rev by hendrik 2011-03-09
    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        prj_en_id.Properties.DataSource = dt_bantu
        prj_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        prj_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        prj_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        prj_cu_id.Properties.DataSource = dt_bantu
        prj_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        prj_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        prj_cu_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_tran_mstr())
        'prj_tran_id.Properties.DataSource = dt_bantu
        'prj_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'prj_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        'prj_tran_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            prj_tran_id.Properties.DataSource = dt_bantu
            prj_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            prj_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            prj_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            prj_tran_id.Properties.DataSource = dt_bantu
            prj_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            prj_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            prj_tran_id.ItemIndex = 0
        End If
    End Sub

    Private Sub prj_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prj_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", prj_en_id.EditValue))
        prj_si_id.Properties.DataSource = dt_bantu
        prj_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        prj_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        prj_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_cust", prj_en_id.EditValue))
        prj_ptnr_id_sold.Properties.DataSource = dt_bantu
        prj_ptnr_id_sold.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        prj_ptnr_id_sold.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        prj_ptnr_id_sold.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_cust", prj_en_id.EditValue))
        prj_ptnr_id_bill.Properties.DataSource = dt_bantu
        prj_ptnr_id_bill.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        prj_ptnr_id_bill.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        prj_ptnr_id_bill.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_sal", prj_en_id.EditValue))
        prj_sales_person_id.Properties.DataSource = dt_bantu
        prj_sales_person_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        prj_sales_person_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        prj_sales_person_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(prj_en_id.EditValue, "project_type"))
        prj_pjt_code_id.Properties.DataSource = dt_bantu
        prj_pjt_code_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prj_pjt_code_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prj_pjt_code_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(prj_en_id.EditValue, "sopj_area_id"))
        prj_area_id.Properties.DataSource = dt_bantu
        prj_area_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prj_area_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prj_area_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(prj_en_id.EditValue))
        prj_credit_term.Properties.DataSource = dt_bantu
        prj_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prj_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prj_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr(prj_en_id.EditValue))
        prj_tax_class.Properties.DataSource = dt_bantu
        prj_tax_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prj_tax_class.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prj_tax_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccr_restrc(prj_en_id.EditValue))
        prj_ar_cc_id.Properties.DataSource = dt_bantu
        prj_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_code").ToString
        prj_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        prj_ar_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        prj_ar_ac_id.Properties.DataSource = dt_bantu
        prj_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_code").ToString
        prj_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        prj_ar_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("sb_mstr", prj_en_id.EditValue))
        prj_ar_sb_id.Properties.DataSource = dt_bantu
        prj_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        prj_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        prj_ar_sb_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Number", "prj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "sold_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bill To", "bill_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "sall_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ref. PO", "pocust_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Type", "project_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Area", "prj_area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Order Date", "prj_ord_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Start Date", "prj_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "prj_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Site", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "prj_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Taxable", "prj_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "prj_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total", "prj_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "prj_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "prj_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "After Tax", "prj_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ext. Total", "prj_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPN", "prj_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPH", "prj_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. After Tax", "prj_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "prj_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "prj_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "prj_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "prj_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "prjd_oid", False)
        add_column(gv_detail, "prjd_prj_oid", False)
        add_column(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "prjd_type", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Qty PAO", "prjd_qty_pao", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Open PAO", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty MO", "prjd_qty_mo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty DO", "prjd_qty_do", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Qty Closing", "prjd_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice", "prjd_qty_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Progress Closing", "prjd_progress_pay", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Progress Closing Invoice", "prjd_progress_pay_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Trans ID", "prjd_trans_id", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_email, "prjd_oid", False)
        add_column(gv_email, "prjd_prj_oid", False)
        add_column(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "prjd_type", False)
        add_column(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_email, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_email, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column(gv_email, "Qty PAO", "prjd_qty_pao", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Qty MO", "prjd_qty_mo", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_email, "Trans ID", "prjd_trans_id", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "prjd_oid", False)
        add_column(gv_edit, "prjd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_prj_oid", False)
        add_column(gv_edit, "prjd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Type", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_type", False)
        add_column(gv_edit, "prjd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description1", "prjd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "prjd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "prjd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "prjd_qty_pao", False)
        add_column(gv_edit, "prjd_um", False)
        add_column(gv_edit, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "prjd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Price", "prjd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Discount", "prjd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "UM Conversion", "prjd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "prjd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty * Cost", "prjd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_edit(gv_edit, "Taxable", "prjd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "prjd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "prjd_tax_class", False)
        add_column(gv_edit, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)

        '=====================================================================================================
        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_cust, "prjc_oid", False)
        add_column(gv_detail_cust, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_cust, "prjc_prj_oid", False)
        add_column(gv_detail_cust, "prjc_seq", False)
        add_column(gv_detail_cust, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_cust, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_cust, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_cust, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_cust, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_cust, "Qty", "prjc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_cust, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_cust, "Cost", "prjc_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_cust, "Price", "prjc_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_cust, "Discount", "prjc_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_detail_cust, "UM Conversion", "prjc_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_cust, "Qty Real", "prjc_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_cust, "Qty * Cost", "prjc_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_edit(gv_detail_cust, "Taxable", "prjc_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_cust, "Tax Include", "prjc_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_cust, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_cust, "prjc_oid", False)
        add_column(gv_edit_cust, "prjc_en_id", False)
        add_column(gv_edit_cust, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_cust, "prjc_prj_oid", False)
        add_column(gv_edit_cust, "prjc_seq", False)
        add_column(gv_edit_cust, "prjc_si_id", False)
        add_column(gv_edit_cust, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_cust, "prjc_cp_id", False)
        add_column(gv_edit_cust, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Description1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Description2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_cust, "prjc_loc_id", False)
        add_column(gv_edit_cust, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Qty", "prjc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_cust, "prjc_um", False)
        add_column(gv_edit_cust, "UM", "unit_measure", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Cost", "prjc_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cust, "Price", "prjc_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cust, "Discount", "prjc_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit_cust, "UM Conversion", "prjc_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_cust, "Qty Real", "prjc_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_cust, "Qty * Cost", "prjc_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_edit(gv_edit_cust, "Taxable", "prjc_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cust, "Tax Include", "prjc_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_cust, "prjc_tax_class", False)
        add_column(gv_edit_cust, "Tax Class", "tax_class", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "prj_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  prj_oid, " _
                    & "  prj_dom_id, " _
                    & "  prj_en_id,en_desc, " _
                    & "  prj_add_by, " _
                    & "  prj_add_date, " _
                    & "  prj_upd_by, " _
                    & "  prj_upd_date, " _
                    & "  prj_dt, " _
                    & "  prj_code, pocust_code, " _
                    & "  prj_ptnr_id_sold,sold.ptnr_name as sold_ptnr_name, " _
                    & "  prj_ptnr_id_bill,bill.ptnr_name as bill_ptnr_name, " _
                    & "  prj_sales_person_id,sal.ptnr_name as sall_ptnr_name, " _
                    & "  prj_pjt_code_id,prj_type.code_name as project_type, " _
                    & "  prj_ord_date, " _
                    & "  prj_start_date, " _
                    & "  prj_end_date, " _
                    & "  prj_si_id,si_code, " _
                    & "  prj_cu_id,cu_code, " _
                    & "  prj_exc_rate, " _
                    & "  prj_credit_term,ct.code_name as credit_term, " _
                    & "  prj_taxable, " _
                    & "  prj_tax_inc, " _
                    & "  prj_tax_class,tclass.code_name as tax_class, " _
                    & "  prj_area_id, " _
                    & "  prj_area.code_name as prj_area_name, " _
                    & "  prj_total, " _
                    & "  prj_total_ppn, " _
                    & "  prj_total_pph, " _
                    & "  (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax," _
                    & "  prj_exc_rate * prj_total as prj_total_ext, " _
                    & "  prj_exc_rate * prj_total_ppn as prj_total_ppn_ext, " _
                    & "  prj_exc_rate * prj_total_pph as prj_total_pph_ext, " _
                    & "  prj_exc_rate * (prj_total + prj_total_ppn + prj_total_pph) as prj_total_after_tax_ext, " _
                    & "  prj_tran_id, tran_name, " _
                    & "  prj_trans_id, " _
                    & "  prj_pocust_oid, " _
                    & "  pocust_code, " _
                    & "  prj_ar_ac_id,ac_code,ac_name, " _
                    & "  prj_ar_sb_id,sb_code,sb_desc, " _
                    & "  prj_ar_cc_id,cc_code,cc_desc " _
                    & "FROM  " _
                    & "  public.prj_mstr  " _
                    & "  inner join en_mstr on en_id = prj_en_id " _
                    & "  inner join ptnr_mstr sold on sold.ptnr_id = prj_ptnr_id_sold " _
                    & "  inner join ptnr_mstr bill on bill.ptnr_id = prj_ptnr_id_bill " _
                    & "  inner join ptnr_mstr sal on sal.ptnr_id = prj_sales_person_id " _
                    & "  inner join code_mstr prj_type on prj_type.code_id = prj_pjt_code_id " _
                    & "  left outer join code_mstr prj_area on prj_area.code_id = prj_area_id " _
                    & "  inner join si_mstr on si_id = prj_si_id " _
                    & "  inner join cu_mstr on cu_id = prj_cu_id " _
                    & "  inner join code_mstr ct on ct.code_id = prj_credit_term " _
                    & "  inner join code_mstr tclass on tclass.code_id = prj_tax_class " _
                    & "  inner join ac_mstr on ac_id = prj_ar_ac_id " _
                    & "  inner join sb_mstr on sb_id = prj_ar_sb_id " _
                    & "  inner join cc_mstr on cc_id = prj_ar_cc_id " _
                    & "  inner join tran_mstr on tran_id = prj_tran_id " _
                    & "  left outer join pocust_mstr on pocust_oid = prj_pocust_oid " _
                    & " where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  prjd_oid, " _
            & "  prjd_dom_id, " _
            & "  prjd_en_id, en_desc, " _
            & "  prjd_add_by, " _
            & "  prjd_add_date, " _
            & "  prjd_upd_by, " _
            & "  prjd_upd_date, " _
            & "  prjd_dt, " _
            & "  prjd_prj_oid, " _
            & "  prjd_seq, " _
            & "  prjd_si_id,si_desc, " _
            & "  prjd_pt_id,pt_code, " _
            & "  prjd_pt_desc1, " _
            & "  prjd_pt_desc2, " _
            & "  prjd_loc_id,loc_code,loc_desc, " _
            & "  prjd_qty, " _
            & "  prjd_qty_full, " _
            & "  prjd_um,um.code_name as unit_measure, " _
            & "  prjd_cost, " _
            & "  prjd_price, " _
            & "  prjd_disc, " _
            & "  prjd_um_conv, " _
            & "  prjd_qty_real, " _
            & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
            & "  prjd_taxable, " _
            & "  prjd_tax_inc, " _
            & "  prjd_tax_class,tclass.code_name as tax_class, " _
            & "  prjd_trans_id, " _
            & "  prjd_qty_pao, " _
            & "  prjd_qty - prjd_qty_pao as qty_open, " _
            & "  prjd_qty_mo, " _
            & "  coalesce(prjd_qty_shipment,0) as prjd_qty_shipment, " _
            & "  coalesce(prjd_qty_inv,0) as prjd_qty_inv, " _
            & "  coalesce(prjd_progress_pay,0) as prjd_progress_pay, " _
            & "  coalesce(prjd_progress_pay_inv,0) as prjd_progress_pay_inv, " _
            & "  coalesce(prjd_qty_do,0) as prjd_qty_do, " _
            & "  prjd_type,type.code_code as code_code " _
            & "FROM  " _
            & "  public.prjd_det " _
            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
            & "  inner join en_mstr on en_id = prjd_en_id " _
            & "  inner join si_mstr on si_id = prjd_si_id " _
            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
            & "  inner join code_mstr um on um.code_id =  prjd_um " _
            & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
            & "  inner join code_mstr type on type.code_id = prjd_type " _
            & "  where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_cust").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  prjc_oid, " _
            & "  prjc_dom_id, " _
            & "  prjc_en_id,en_desc, " _
            & "  prjc_add_by, " _
            & "  prjc_add_date, " _
            & "  prjc_upd_by, " _
            & "  prjc_upd_date, " _
            & "  prjc_dt, " _
            & "  prjc_prj_oid, " _
            & "  prjc_seq, " _
            & "  prjc_si_id,si_desc, " _
            & "  prjc_cp_id,cp_code, " _
            & "  prjc_pt_desc1, " _
            & "  prjc_pt_desc2, " _
            & "  prjc_loc_id,loc_code,loc_desc, " _
            & "  prjc_qty, " _
            & "  prjc_qty_full, " _
            & "  prjc_um,um.code_name as unit_measure, " _
            & "  prjc_cost, " _
            & "  prjc_price, " _
            & "  prjc_disc, " _
            & "  prjc_um_conv, " _
            & "  prjc_qty_real, " _
            & "  ((prjc_qty * prjc_cost) - (prjc_qty * prjc_cost * prjc_disc)) as prjc_qty_cost, " _
            & "  prjc_taxable, " _
            & "  prjc_tax_inc, " _
            & "  prjc_tax_class,tclass.code_name as tax_class, " _
            & "  prjc_trans_id, " _
            & "  prjc_qty_inv " _
            & "FROM  " _
            & "  public.prjc_cust  " _
            & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
            & "  inner join en_mstr on en_id = prjc_en_id " _
            & "  inner join si_mstr on si_id = prjc_si_id " _
            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
            & "  inner join loc_mstr on loc_id = prjc_loc_id " _
            & "  inner join code_mstr um on um.code_id =  prjc_um " _
            & "  inner join code_mstr tclass on tclass.code_id = prjc_tax_class " _
            & "  where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        load_data_detail(sql, gc_detail_cust, "detail_cust")

        '========================================================================================
        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join prj_mstr on prj_code = wf_ref_code " + _
                  " inner join prjd_det dt on dt.prjd_prj_oid = prj_oid " _
                & " where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " and prj_ar_cc_id in (select ccr_cc_id from ccr_restrc " _
                                      & " where ccr_user_id = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  prjc_oid, " _
                & "  prjc_dom_id, " _
                & "  prjc_en_id,en_desc, " _
                & "  prjc_add_by, " _
                & "  prjc_add_date, " _
                & "  prjc_upd_by, " _
                & "  prjc_upd_date, " _
                & "  prjc_dt, " _
                & "  prjc_prj_oid, " _
                & "  prjc_seq, " _
                & "  prjc_si_id,si_desc, " _
                & "  prjc_cp_id,cp_code, " _
                & "  prjc_pt_desc1, " _
                & "  prjc_pt_desc2, " _
                & "  prjc_loc_id,loc_code,loc_desc, " _
                & "  prjc_qty, " _
                & "  prjc_qty_full, " _
                & "  prjc_um,um.code_name as unit_measure, " _
                & "  prjc_cost, " _
                & "  prjc_price, " _
                & "  prjc_disc, " _
                & "  prjc_um_conv, " _
                & "  prjc_qty_real, " _
                & "  ((prjc_qty * prjc_cost) - (prjc_qty * prjc_cost * prjc_disc)) as prjc_qty_cost, " _
                & "  prjc_taxable, " _
                & "  prjc_tax_inc, " _
                & "  prjc_tax_class,tclass.code_name as tax_class, " _
                & "  prjc_trans_id, " _
                & "  prjc_qty_inv " _
                & "FROM  " _
                & "  public.prjc_cust  " _
                & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
                & "  inner join en_mstr on en_id = prjc_en_id " _
                & "  inner join si_mstr on si_id = prjc_si_id " _
                & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                & "  inner join code_mstr um on um.code_id =  prjc_um " _
                & "  inner join code_mstr tclass on tclass.code_id = prjc_tax_class " _
                & "  where prj_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and prj_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and prj_en_id in (select user_en_id from tconfuserentity " _
                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select prj_oid, prj_code, prj_trans_id, false as status from prj_mstr " _
                & " where prj_trans_id ~~* 'd' " _
                & " and prj_ar_cc_id in (select ccr_cc_id from ccr_restrc " _
                                     & " where ccr_user_id = " + master_new.ClsVar.sUserID.ToString + ")"
            load_data_detail(sql, gc_smart, "smart")
        End If

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("prjd_prj_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjd_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_cust.Columns("prjc_prj_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjc_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString & "'")
            gv_detail_cust.BestFitColumns()

            gv_wf.Columns("wf_ref_code").FilterInfo = _
           New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wf_ref_code] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code").ToString & "'")
            gv_wf.BestFitColumns()

            gv_email.Columns("prjc_prj_oid").FilterInfo = _
           New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjc_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString & "'")
            gv_email.BestFitColumns()

            gv_smart.Columns("prjc_prj_oid").FilterInfo = _
          New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[prjc_prj_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString & "'")
            gv_smart.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        prj_en_id.Focus()
        prj_en_id.ItemIndex = 0
        prj_si_id.ItemIndex = 0
        'prj_code.Text = ""
        prj_ptnr_id_sold.ItemIndex = 0
        prj_ptnr_id_bill.ItemIndex = 0
        prj_pocust_oid.Text = ""
        _pocust_oid = ""
        prj_sales_person_id.ItemIndex = 0
        prj_pjt_code_id.ItemIndex = 0
        prj_ord_date.EditValue = _now
        prj_start_date.EditValue = _now
        prj_end_date.EditValue = _now
        prj_cu_id.ItemIndex = 0
        prj_exc_rate.Text = "1"
        prj_cu_id.ItemIndex = 0
        prj_credit_term.ItemIndex = 0
        prj_tax_class.ItemIndex = 0
        prj_ar_cc_id.ItemIndex = 0
        prj_ar_ac_id.ItemIndex = 0
        prj_ar_sb_id.ItemIndex = 0
        prj_tran_id.ItemIndex = 0

        prj_taxable.Enabled = True
        prj_tax_class.Enabled = False
        prj_tax_inc.Enabled = False

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  prjd_oid, " _
                        & "  prjd_dom_id, " _
                        & "  prjd_en_id,en_desc, " _
                        & "  prjd_add_by, " _
                        & "  prjd_add_date, " _
                        & "  prjd_upd_by, " _
                        & "  prjd_upd_date, " _
                        & "  prjd_dt, " _
                        & "  prjd_prj_oid, " _
                        & "  prjd_seq, " _
                        & "  prjd_si_id,si_desc, " _
                        & "  prjd_pt_id,pt_code, " _
                        & "  prjd_pt_desc1, " _
                        & "  prjd_pt_desc2, " _
                        & "  prjd_loc_id,loc_code,loc_desc, " _
                        & "  prjd_qty, " _
                        & "  prjd_qty_full, " _
                        & "  prjd_um,um.code_name as unit_measure, " _
                        & "  prjd_cost, " _
                        & "  prjd_price, " _
                        & "  prjd_disc, " _
                        & "  prjd_um_conv, " _
                        & "  prjd_qty_real, " _
                        & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
                        & "  prjd_taxable, " _
                        & "  prjd_tax_inc, " _
                        & "  prjd_tax_class,tclass.code_name as tax_class, " _
                        & "  prjd_trans_id, " _
                        & "  prjd_qty_pao, " _
                        & "  prjd_qty_mo, " _
                        & "  prjd_type,type.code_code as code_code " _
                        & "FROM  " _
                        & "  public.prjd_det " _
                        & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                        & "  inner join en_mstr on en_id = prjd_en_id " _
                        & "  inner join si_mstr on si_id = prjd_si_id " _
                        & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                        & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                        & "  inner join code_mstr um on um.code_id =  prjd_um " _
                        & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
                        & "  inner join code_mstr type on type.code_id = prjd_type " _
                        & "  where prjd_seq = -99 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_cust = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  prjc_oid, " _
                        & "  prjc_dom_id, " _
                        & "  prjc_en_id,en_desc, " _
                        & "  prjc_add_by, " _
                        & "  prjc_add_date, " _
                        & "  prjc_upd_by, " _
                        & "  prjc_upd_date, " _
                        & "  prjc_dt, " _
                        & "  prjc_prj_oid, " _
                        & "  prjc_seq, " _
                        & "  prjc_si_id,si_desc, " _
                        & "  prjc_cp_id,cp_code, " _
                        & "  prjc_pt_desc1, " _
                        & "  prjc_pt_desc2, " _
                        & "  prjc_loc_id,loc_code,loc_desc, " _
                        & "  prjc_qty, " _
                        & "  prjc_qty_full, " _
                        & "  prjc_um,um.code_name as unit_measure, " _
                        & "  prjc_cost, " _
                        & "  prjc_price, " _
                        & "  prjc_disc, " _
                        & "  prjc_um_conv, " _
                        & "  prjc_qty_real, " _
                        & "  ((prjc_qty * prjc_cost) - (prjc_qty * prjc_cost * prjc_disc)) as prjc_qty_cost, " _
                        & "  prjc_taxable, " _
                        & "  prjc_tax_inc, " _
                        & "  prjc_tax_class,tclass.code_name as tax_class, " _
                        & "  prjc_trans_id, " _
                        & "  prjc_qty_inv " _
                        & "FROM  " _
                        & "  public.prjc_cust  " _
                        & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
                        & "  inner join en_mstr on en_id = prjc_en_id " _
                        & "  inner join si_mstr on si_id = prjc_si_id " _
                        & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                        & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                        & "  inner join code_mstr um on um.code_id =  prjc_um " _
                        & "  inner join code_mstr tclass on tclass.code_id = prjc_tax_class " _
                        & " where prjc_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_cust, "insert_edit_cust")
                    gc_edit_cust.DataSource = ds_edit_cust.Tables(0)
                    gv_edit_cust.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Private Function get_status_project(ByVal par_oid As String) As String
        get_status_project = "W"
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select prj_trans_id from prj_mstr where prj_oid = " + SetSetring(par_oid)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_status_project = .DataReader("prj_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return get_status_project
    End Function

    Public Overrides Function delete_data() As Boolean
        _conf_value = func_coll.get_conf_file("wf_project_maintenance")

        row = BindingContext(ds.Tables(0)).Position
        If get_status_project(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
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

        'Dim i As Integer

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            'ds_get = New DataSet
            'Try
            '    Using objcb As New master_new.WDABasepgsql("", "")
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  prjd_oid, " _
            '                & "  prjd_dom_id, " _
            '                & "  prjd_en_id,en_desc, " _
            '                & "  prjd_add_by, " _
            '                & "  prjd_add_date, " _
            '                & "  prjd_upd_by, " _
            '                & "  prjd_upd_date, " _
            '                & "  prjd_dt, " _
            '                & "  prjd_prj_oid, " _
            '                & "  prjd_seq, " _
            '                & "  prjd_si_id,si_desc, " _
            '                & "  prjd_pt_id,pt_code, " _
            '                & "  prjd_pt_desc1, " _
            '                & "  prjd_pt_desc2, " _
            '                & "  prjd_loc_id,loc_code,loc_desc, " _
            '                & "  prjd_qty, " _
            '                & "  prjd_qty_full, " _
            '                & "  prjd_um,um.code_name as unit_measure, " _
            '                & "  prjd_cost, " _
            '                & "  prjd_price, " _
            '                & "  prjd_disc, " _
            '                & "  prjd_um_conv, " _
            '                & "  prjd_qty_real, " _
            '                & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
            '                & "  prjd_taxable, " _
            '                & "  prjd_tax_inc, " _
            '                & "  prjd_tax_class,tclass.code_name as tax_class, " _
            '                & "  prjd_trans_id, " _
            '                & "  prjd_qty_pao, " _
            '                & "  prjd_qty_mo " _
            '                & "FROM  " _
            '                & "  public.prjd_det " _
            '                & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
            '                & "  inner join en_mstr on en_id = prjd_en_id " _
            '                & "  inner join si_mstr on si_id = prjd_si_id " _
            '                & "  inner join pt_mstr on pt_id = prjd_pt_id " _
            '                & "  inner join loc_mstr on loc_id = prjd_loc_id " _
            '                & "  inner join code_mstr um on um.code_id =  prjd_um " _
            '                & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
            '                & "  where prjd_prj_oid = " & SetSetring(ds.Tables(0).Rows(row).Item("prj_oid")) _
            '                & "  order by prjd_seq asc "
            '            .InitializeCommand()
            '            .FillDataSet(ds_get, "get")
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

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

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from prj_mstr where prj_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from pjc_mstr where pjc_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()
                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Function get_status_wf_local(ByVal par_oid As String) As String
        get_status_wf_local = "-1"
        Try
            Using objkalendar As New master_new.WDABasepgsql("", "")
                With objkalendar
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select wf_wfs_id from wf_mstr  " + _
                                           " where wf_ref_oid = '" + par_oid + "'" + _
                                           " and wf_seq = 0"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_status_wf_local = .DataReader.Item("wf_wfs_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_status_wf_local
    End Function

    Public Overrides Function edit_data() As Boolean
        'If _conf_value = "0" Then
        '    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Function
        'ElseIf _conf_value = "1" Then
        '    For Each _dr As DataRow In ds.Tables(0).Rows
        '        For Each _drdet As DataRow In ds.Tables("detail").Rows
        '            If _dr("prj_oid") = _drdet("prjd_prj_oid") Then
        '                'MessageBox.Show(_drdet("prjd_trans_id").ToString())
        '                If _drdet("prjd_trans_id") <> "D" Then
        '                    If get_status_wf_local(_drdet("prjd_oid")) > 0 Then
        '                        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                        Exit Function
        '                    End If
        '                End If

        '            End If
        '        Next
        '    Next

        ''If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_trans_id") <> "D" Then
        'If get_status_wf_local(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prjd_code")) > 0 Then
        '    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Function
        'End If
        'End If
        'End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position
            With ds.Tables(0).Rows(row)
                _prj_oid_master = .Item("prj_oid").ToString
                _prj_code_master = .Item("prj_code")
                'prj_code.Text = .Item("prj_code")
                'prj_code.Enabled = False
                prj_en_id.EditValue = .Item("prj_en_id")
                prj_ptnr_id_sold.EditValue = .Item("prj_ptnr_id_sold")
                prj_ptnr_id_bill.EditValue = .Item("prj_ptnr_id_bill")
                prj_sales_person_id.EditValue = .Item("prj_sales_person_id")
                prj_pjt_code_id.EditValue = .Item("prj_pjt_code_id")
                prj_area_id.EditValue = .Item("prj_area_id")
                prj_ord_date.DateTime = .Item("prj_ord_date")
                prj_start_date.DateTime = .Item("prj_start_date")
                prj_end_date.DateTime = .Item("prj_end_date")
                prj_si_id.EditValue = .Item("prj_si_id")
                prj_cu_id.EditValue = .Item("prj_cu_id")
                prj_exc_rate.Text = .Item("prj_exc_rate")
                prj_credit_term.EditValue = .Item("prj_credit_term")
                prj_taxable.EditValue = SetBitYNB(.Item("prj_taxable"))
                prj_tax_inc.EditValue = SetBitYNB(.Item("prj_tax_inc"))
                prj_tax_class.EditValue = .Item("prj_tax_class")
                prj_ar_ac_id.EditValue = .Item("prj_ar_ac_id")
                prj_ar_sb_id.EditValue = .Item("prj_ar_sb_id")
                prj_ar_cc_id.EditValue = .Item("prj_ar_cc_id")
                prj_tran_id.EditValue = .Item("prj_tran_id")
                _prj_tran_id_pre_edit = .Item("prj_tran_id")

                _prj_code_pre_edit = .Item("prj_code")
                _pocust_oid = IIf(IsDBNull(.Item("prj_pocust_oid")) = True, "", .Item("prj_pocust_oid").ToString)
                prj_pocust_oid.Text = IIf(IsDBNull(.Item("prj_pocust_oid")) = True, "", .Item("pocust_code").ToString)

                If prj_taxable.EditValue = False Then
                    prj_tax_class.Enabled = False
                Else : prj_tax_class.Enabled = True
                End If

            End With
            prj_en_id.Focus()
            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjd_oid, " _
                            & "  prjd_dom_id, " _
                            & "  prjd_en_id,en_desc, " _
                            & "  prjd_add_by, " _
                            & "  prjd_add_date, " _
                            & "  prjd_upd_by, " _
                            & "  prjd_upd_date, " _
                            & "  prjd_dt, " _
                            & "  prjd_prj_oid, " _
                            & "  prjd_seq, " _
                            & "  prjd_si_id,si_desc, " _
                            & "  prjd_pt_id,pt_code, " _
                            & "  prjd_pt_desc1, " _
                            & "  prjd_pt_desc2, " _
                            & "  prjd_loc_id,loc_code,loc_desc, " _
                            & "  prjd_qty, " _
                            & "  prjd_qty_full, " _
                            & "  prjd_um,um.code_name as unit_measure, " _
                            & "  prjd_cost, " _
                            & "  prjd_price, " _
                            & "  prjd_disc, " _
                            & "  prjd_um_conv, " _
                            & "  prjd_qty_real, " _
                            & "  ((prjd_qty * prjd_cost) - (prjd_qty * prjd_cost * prjd_disc)) as prjd_qty_cost, " _
                            & "  prjd_taxable, " _
                            & "  prjd_tax_inc, " _
                            & "  prjd_tax_class,tclass.code_name as tax_class, " _
                            & "  prjd_trans_id, " _
                            & "  coalesce(prjd_qty_pao,0) as prjd_qty_pao, " _
                            & "  prjd_qty_mo, " _
                            & "  prjd_type,type.code_code as code_code " _
                            & "FROM  " _
                            & "  public.prjd_det " _
                            & "  inner join prj_mstr on prj_oid = prjd_prj_oid " _
                            & "  inner join en_mstr on en_id = prjd_en_id " _
                            & "  inner join si_mstr on si_id = prjd_si_id " _
                            & "  inner join pt_mstr on pt_id = prjd_pt_id " _
                            & "  inner join loc_mstr on loc_id = prjd_loc_id " _
                            & "  inner join code_mstr um on um.code_id =  prjd_um " _
                            & "  inner join code_mstr tclass on tclass.code_id = prjd_tax_class " _
                            & "  inner join code_mstr type on type.code_id = prjd_type " _
                            & "  where prjd_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) _
                            & "  order by prjd_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_cust = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  prjc_oid, " _
                            & "  prjc_dom_id, " _
                            & "  prjc_en_id,en_desc, " _
                            & "  prjc_add_by, " _
                            & "  prjc_add_date, " _
                            & "  prjc_upd_by, " _
                            & "  prjc_upd_date, " _
                            & "  prjc_dt, " _
                            & "  prjc_prj_oid, " _
                            & "  prjc_seq, " _
                            & "  prjc_si_id,si_desc, " _
                            & "  prjc_cp_id,cp_code, " _
                            & "  prjc_pt_desc1, " _
                            & "  prjc_pt_desc2, " _
                            & "  prjc_loc_id,loc_code,loc_desc, " _
                            & "  prjc_qty, " _
                            & "  prjc_qty_full, " _
                            & "  prjc_um,um.code_name as unit_measure, " _
                            & "  prjc_cost, " _
                            & "  prjc_price, " _
                            & "  prjc_disc, " _
                            & "  prjc_um_conv, " _
                            & "  prjc_qty_real, " _
                            & "  ((prjc_qty * prjc_cost) - (prjc_qty * prjc_cost * prjc_disc)) as prjc_qty_cost, " _
                            & "  prjc_taxable, " _
                            & "  prjc_tax_inc, " _
                            & "  prjc_tax_class,tclass.code_name as tax_class, " _
                            & "  prjc_trans_id, " _
                            & "  prjc_qty_inv, prjgd_oid " _
                            & "FROM  " _
                            & "  public.prjc_cust  " _
                            & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
                            & "  inner join en_mstr on en_id = prjc_en_id " _
                            & "  inner join si_mstr on si_id = prjc_si_id " _
                            & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                            & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                            & "  inner join code_mstr um on um.code_id =  prjc_um " _
                            & "  inner join code_mstr tclass on tclass.code_id = prjc_tax_class " _
                            & "  left outer join prjgd_det on prjgd_det.prjgd_prjc_oid = prjc_oid " _
                            & "  where prjc_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) _
                            & "  order by prjc_seq asc "
                        .InitializeCommand()
                        .FillDataSet(ds_edit_cust, "insert_edit_cust")
                        gc_edit_cust.DataSource = ds_edit_cust.Tables(0)
                        gv_edit_cust.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    'rev by hendrik 2011-06-05 ===============================================================
    Private Function ProjectGroup(ByVal par_prj_oid As String) As Boolean
        ProjectGroup = False
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select prjg_prj_oid from prjg_group " + _
                                           " where prjg_prj_oid = " & SetSetring(par_prj_oid.ToString())
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        ProjectGroup = True
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ProjectGroup
    End Function
    '=============================================================================================

    Public Overrides Function edit()
        edit = True
        _conf_value = func_coll.get_conf_file("wf_project_maintenance")
        Dim ssqls As New ArrayList
        Dim _prj_oid As Guid
        _prj_oid = Guid.NewGuid

        Dim ds_bantu As New DataSet
        Dim _prj_total, _prj_total_ppn, _prj_total_pph, _prjd_qty, _prjd_cost, _prjd_disc, _prj_total_temp, _tax_rate As Double
        'Dim _prjd_cost_real, _prjd_ppn, _prjd_pph, _cost_real As Double
        Dim _cost_real As Double
        Dim i, j As Integer
        Dim _prj_trans_id As String = ""

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(prj_tran_id.EditValue)
            _prj_trans_id = "D"
        Else
            _prj_trans_id = "I"
        End If

        '******* Total PRJ, Total PPN, Total PPH
        _prj_total = 0
        _prj_total_ppn = 0
        _prj_total_pph = 0
        _prj_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("prjd_cost") / (1 + _tax_rate)))
                Else
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                End If
            Else : _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
            End If

            _prjd_qty = ds_edit.Tables(0).Rows(i).Item("prjd_qty")
            _prjd_disc = ds_edit.Tables(0).Rows(i).Item("prjd_disc")

            _prj_total = _prj_total + ((_prjd_qty * _prjd_cost) - (_prjd_qty * _prjd_cost * _prjd_disc))
        Next

        'ppn dan pph
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("prjd_cost") / (1 + _tax_rate)))
                Else
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                End If

                _prjd_qty = ds_edit.Tables(0).Rows(i).Item("prjd_qty")
                _prjd_disc = ds_edit.Tables(0).Rows(i).Item("prjd_disc")

                _prj_total_temp = ((_prjd_qty * _prjd_cost) - (_prjd_qty * _prjd_cost * _prjd_disc))
                _prj_total_ppn = _prj_total_ppn + (_prj_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")))
                _prj_total_pph = _prj_total_pph + (_prj_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")))
            Else
                _prj_total_ppn = _prj_total_ppn + 0
                _prj_total_pph = _prj_total_pph + 0
            End If
        Next
        '*******************************************************

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM prjd_det   " _
                                            & " WHERE coalesce(prjd_qty_pao,0) = 0 " _
                                            & " and prjd_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'rev by hendrik 2011-06-05 ===============================================================
                        If ProjectGroup(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) = False Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "DELETE FROM prjc_cust   " _
                                                & " WHERE " _
                                                & " prjc_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) & "  " _
                                                & ";"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '===============================================================================================

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.prj_mstr   " _
                                            & "SET  " _
                                            & "  prj_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  prj_en_id = " & SetInteger(prj_en_id.EditValue) & ",  " _
                                            & "  prj_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  prj_upd_date = current_timestamp, " _
                                            & "  prj_dt = current_timestamp, " _
                                            & "  prj_ptnr_id_sold = " & SetInteger(prj_ptnr_id_sold.EditValue) & ",  " _
                                            & "  prj_ptnr_id_bill = " & SetInteger(prj_ptnr_id_bill.EditValue) & ",  " _
                                            & "  prj_sales_person_id = " & SetInteger(prj_sales_person_id.EditValue) & ",  " _
                                            & "  prj_pjt_code_id = " & SetInteger(prj_pjt_code_id.EditValue) & ",  " _
                                            & "  prj_ord_date = " & SetDate(prj_ord_date.DateTime.Date) & ",  " _
                                            & "  prj_si_id = " & SetInteger(prj_si_id.EditValue) & ",  " _
                                            & "  prj_cu_id = " & SetInteger(prj_cu_id.EditValue) & ",  " _
                                            & "  prj_exc_rate = " & SetDbl(prj_exc_rate.Text) & ",  " _
                                            & "  prj_credit_term = " & SetInteger(prj_credit_term.EditValue) & ",  " _
                                            & "  prj_taxable = " & SetBitYN(prj_taxable.EditValue) & ",  " _
                                            & "  prj_tax_inc = " & SetBitYN(prj_tax_inc.EditValue) & ",  " _
                                            & "  prj_tax_class = " & SetInteger(prj_tax_class.EditValue) & ",  " _
                                            & "  prj_total = " & SetDbl(_prj_total) & ",  " _
                                            & "  prj_total_ppn = " & SetDbl(_prj_total_ppn) & ",  " _
                                            & "  prj_total_pph = " & SetDbl(_prj_total_pph) & ",  " _
                                            & "  prj_tran_id = " & SetInteger(prj_tran_id.EditValue) & ",  " _
                                            & "  prj_trans_id = " & SetSetring(_prj_trans_id) & ",  " _
                                            & "  prj_ar_ac_id = " & SetInteger(prj_ar_ac_id.EditValue) & ",  " _
                                            & "  prj_ar_sb_id = " & SetInteger(prj_ar_sb_id.EditValue) & ",  " _
                                            & "  prj_ar_cc_id = " & SetInteger(prj_ar_cc_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _cost_real = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (ds_edit.Tables(0).Rows(i).Item("prjd_cost") * ds_edit.Tables(0).Rows(i).Item("prjd_disc"))

                            'If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                            '    If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                            '        _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_cost_real = _cost_real - (_tax_rate * (_cost_real / (1 + _tax_rate)))
                            '        _prjd_ppn = _prjd_cost_real * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_pph = _prjd_cost_real * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '    Else
                            '        _prjd_cost_real = _cost_real
                            '        _prjd_ppn = _cost_real * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_pph = _cost_real * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '    End If
                            'Else
                            '    _prjd_cost_real = _cost_real
                            '    _prjd_ppn = 0
                            '    _prjd_pph = 0
                            'End If

                            If ds_edit.Tables(0).Rows(i).Item("prjd_qty_pao") = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.prjd_det " _
                                                    & "( " _
                                                    & "  prjd_oid, " _
                                                    & "  prjd_dom_id, " _
                                                    & "  prjd_en_id, " _
                                                    & "  prjd_add_by, " _
                                                    & "  prjd_add_date, " _
                                                    & "  prjd_dt, " _
                                                    & "  prjd_prj_oid, " _
                                                    & "  prjd_seq, " _
                                                    & "  prjd_si_id, " _
                                                    & "  prjd_pt_id, " _
                                                    & "  prjd_pt_desc1, " _
                                                    & "  prjd_pt_desc2, " _
                                                    & "  prjd_loc_id, " _
                                                    & "  prjd_qty, " _
                                                    & "  prjd_qty_full, " _
                                                    & "  prjd_um, " _
                                                    & "  prjd_cost, " _
                                                    & "  prjd_price, " _
                                                    & "  prjd_disc, " _
                                                    & "  prjd_um_conv, " _
                                                    & "  prjd_qty_real, " _
                                                    & "  prjd_taxable, " _
                                                    & "  prjd_tax_inc, " _
                                                    & "  prjd_tax_class, " _
                                                    & "  prjd_trans_id, " _
                                                    & "  prjd_qty_pao, " _
                                                    & "  prjd_qty_mo, " _
                                                    & "  prjd_type " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid")) & ",  " _
                                                     & master_new.ClsVar.sdom_id & ",  " _
                                                    & prj_en_id.EditValue & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & SetSetring(_prj_oid_master.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_pt_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc1")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc2")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_loc_id")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty_full")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_cost")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_price")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_disc")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty_real")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_taxable")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")) & ",  " _
                                                    & SetSetring(_prj_trans_id) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_type")) & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.prjd_det   " _
                                                    & "SET  " _
                                                    & "  prjd_en_id = " & SetInteger(prj_en_id.EditValue) & ",  " _
                                                    & "  prjd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  prjd_upd_date = current_timestamp, " _
                                                    & "  prjd_dt = current_timestamp,  " _
                                                    & "  prjd_seq = " & SetInteger(i) & ",  " _
                                                    & "  prjd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_si_id")) & ",  " _
                                                    & "  prjd_pt_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_pt_id")) & ",  " _
                                                    & "  prjd_pt_desc1 = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc1")) & ",  " _
                                                    & "  prjd_pt_desc2 = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc2")) & ",  " _
                                                    & "  prjd_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_loc_id")) & ",  " _
                                                    & "  prjd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty")) & ",  " _
                                                    & "  prjd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_um")) & ",  " _
                                                    & "  prjd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_cost")) & ",  " _
                                                    & "  prjd_price = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_price")) & ",  " _
                                                    & "  prjd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_disc")) & ",  " _
                                                    & "  prjd_um_conv = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_um_conv")) & ",  " _
                                                    & "  prjd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty_real")) & ",  " _
                                                    & "  prjd_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_taxable")) & ",  " _
                                                    & "  prjd_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc")) & ",  " _
                                                    & "  prjd_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")) & ",  " _
                                                    & "  prjd_trans_id = " & SetSetring(_prj_trans_id) & ",  " _
                                                    & "  prjd_type = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_type")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  prjd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid").ToString)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If



                            'Untuk insert wf_mstr ini relation nya ke detail project dan bukan ke header
                            If _conf_value = "1" Then
                                If _prj_tran_id_pre_edit <> prj_tran_id.EditValue Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "DELETE FROM wf_mstr   " _
                                                        & " WHERE " _
                                                        & "  wf_ref_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid").ToString()) & "  " _
                                                        & ";"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                        Dim _trans_id As String
                                        If ds_bantu.Tables(0).Rows(j).Item("aprv_seq") = 0 Then
                                            _trans_id = "Y"
                                        Else
                                            _trans_id = "N"
                                        End If

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                                                & "  public.wf_mstr " _
                                                                & "( " _
                                                                & "  wf_oid, " _
                                                                & "  wf_dom_id, " _
                                                                & "  wf_en_id, " _
                                                                & "  wf_tran_id, " _
                                                                & "  wf_ref_oid, " _
                                                                & "  wf_ref_code, " _
                                                                & "  wf_ref_desc, " _
                                                                & "  wf_seq, " _
                                                                & "  wf_user_id, " _
                                                                & "  wf_wfs_id, " _
                                                                & "  wf_iscurrent, " _
                                                                & "  wf_dt " _
                                                                & ")  " _
                                                                & "VALUES ( " _
                                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                                & SetInteger(prj_en_id.EditValue) & ",  " _
                                                                & SetSetring(prj_tran_id.EditValue) & ",  " _
                                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid").ToString()) & ",  " _
                                                                & SetSetring(_prj_code_master) & ",  " _
                                                                & SetSetring("Project") & ",  " _
                                                                & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                                & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                                & SetInteger(0) & ",  " _
                                                                & SetSetring(_trans_id) & ",  " _
                                                                & " current_timestamp " & "  " _
                                                                & ")"
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    Next

                                End If

                            End If
                        Next

                        'rev by hendrik 2011-06-05 ==========================================================================================
                        For i = 0 To ds_edit_cust.Tables(0).Rows.Count - 1
                            If ProjectGroup(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.prjc_cust " _
                                                    & "( " _
                                                    & "  prjc_oid, " _
                                                    & "  prjc_dom_id, " _
                                                    & "  prjc_en_id, " _
                                                    & "  prjc_add_by, " _
                                                    & "  prjc_add_date, " _
                                                    & "  prjc_dt, " _
                                                    & "  prjc_prj_oid, " _
                                                    & "  prjc_seq, " _
                                                    & "  prjc_si_id, " _
                                                    & "  prjc_cp_id, " _
                                                    & "  prjc_pt_desc1, " _
                                                    & "  prjc_pt_desc2, " _
                                                    & "  prjc_loc_id, " _
                                                    & "  prjc_qty, " _
                                                    & "  prjc_qty_full, " _
                                                    & "  prjc_um, " _
                                                    & "  prjc_cost, " _
                                                    & "  prjc_price, " _
                                                    & "  prjc_disc, " _
                                                    & "  prjc_um_conv, " _
                                                    & "  prjc_qty_real, " _
                                                    & "  prjc_taxable, " _
                                                    & "  prjc_tax_inc, " _
                                                    & "  prjc_tax_class, " _
                                                    & "  prjc_trans_id, " _
                                                    & "  prjc_qty_inv " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_oid")) & ",  " _
                                                    & master_new.ClsVar.sdom_id & ",  " _
                                                    & prj_en_id.EditValue & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & " current_timestamp " & ",  " _
                                                    & SetSetring(_prj_oid_master.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_si_id")) & ",  " _
                                                    & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cp_id")) & ",  " _
                                                    & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc1")) & ",  " _
                                                    & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc2")) & ",  " _
                                                    & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_loc_id")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_full")) & ",  " _
                                                    & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_price")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_disc")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_real")) & ",  " _
                                                    & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_taxable")) & ",  " _
                                                    & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_inc")) & ",  " _
                                                    & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_class")) & ",  " _
                                                    & SetSetring(_prj_trans_id) & ",  " _
                                                    & SetDbl(0) & "  " _
                                                    & ");"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE " _
                                                    & " prjc_cust " _
                                                    & " SET " _
                                                    & "  prjc_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                                    & "  prjc_en_id = " & prj_en_id.EditValue & ",  " _
                                                    & "  prjc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "  prjc_upd_date = current_timestamp, " _
                                                    & "  prjc_dt = current_timestamp, " _
                                                    & "  prjc_prj_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString()) & ", " _
                                                    & "  prjc_seq =  " & SetInteger(i) & ",  " _
                                                    & "  prjc_si_id = " & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_si_id")) & ",  " _
                                                    & "  prjc_cp_id =  " & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cp_id")) & ",  " _
                                                    & "  prjc_pt_desc1 = " & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc1")) & ",  " _
                                                    & "  prjc_pt_desc2 = " & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc2")) & ",  " _
                                                    & "  prjc_loc_id = " & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_loc_id")) & ",  " _
                                                    & "  prjc_qty = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty")) & ",  " _
                                                    & "  prjc_qty_full = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_full")) & ",  " _
                                                    & "  prjc_um = " & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um")) & ",  " _
                                                    & "  prjc_cost = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost")) & ",  " _
                                                    & "  prjc_price = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_price")) & ",  " _
                                                    & "  prjc_disc = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_disc")) & ",  " _
                                                    & "  prjc_um_conv = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um_conv")) & ",  " _
                                                    & "  prjc_qty_real = " & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_real")) & ",  " _
                                                    & "  prjc_taxable = " & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_taxable")) & ",  " _
                                                    & "  prjc_tax_inc = " & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_inc")) & ",  " _
                                                    & "  prjc_tax_class = " & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_class")) & "  " _
                                                    & "  WHERE prjc_oid = " & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_oid").ToString())
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next
                        '==========================================================================================

                        'update pjc_mstr
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.pjc_mstr   " _
                                                & "SET  " _
                                                & "  pjc_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & "  pjc_en_id = " & SetInteger(prj_en_id.EditValue) & ",  " _
                                                & "  pjc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  pjc_upd_date = current_timestamp, " _
                                                & "  pjc_date = " & SetDate(prj_ord_date.DateTime.Date) & ",  " _
                                                & "  pjc_desc = " & SetSetring("-") & ",  " _
                                                & "  pjc_active = " & SetSetring("Y") & ",  " _
                                                & "  pjc_dt = current_timestamp " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  pjc_code = " & SetSetring(_prj_code_pre_edit) & "  " _
                                                & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_prj_oid_master.ToString(), "prj_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        edit = False
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        gv_edit_cust.UpdateCurrentRow()
        ds_edit_cust.AcceptChanges()

        'Cek UM
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("prjd_um")) = True Then
                MessageBox.Show("Unit Measure Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("prjd_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next

        For i = 0 To ds_edit_cust.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit_cust.Tables(0).Rows(i).Item("prjc_loc_id")) = True Then
                MessageBox.Show("Location in customer Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit_cust.Tables(0)).Position = i
                Return False
            End If
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("prjd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If Trim(prj_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If prj_ar_cc_id.Text = "[EditValue is null]" Then
            MessageBox.Show("Cost Center Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            prj_ar_cc_id.Focus()
            Return False
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        'If ds_edit_cust.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Cust Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        '=====================================================================================
        If ds_edit_cust.Tables(0).Rows.Count > 0 Then
            Dim _amount, _amount_cust As Double
            Dim _tax_rate, _prjd_cost, _prjd_qty, _prjd_disc As Double
            Dim _tax_rate_cust, _prjc_cost, _prjc_qty, _prjc_disc As Double

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                        _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                        _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("prjd_cost") / (1 + _tax_rate)))
                    Else
                        _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                    End If
                Else : _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                End If

                _prjd_qty = ds_edit.Tables(0).Rows(i).Item("prjd_qty")
                _prjd_disc = ds_edit.Tables(0).Rows(i).Item("prjd_disc")

                _amount = _amount + ((_prjd_qty * _prjd_cost) - (_prjd_qty * _prjd_cost * _prjd_disc))
            Next

            For i = 0 To ds_edit_cust.Tables(0).Rows.Count - 1
                If ds_edit_cust.Tables(0).Rows(i).Item("prjc_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_inc").ToString.ToUpper = "Y" Then
                        _tax_rate_cust = func_coll.get_ppn(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_class"))
                        _prjc_cost = ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost") - (_tax_rate_cust * (ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost") / (1 + _tax_rate_cust)))
                    Else
                        _prjc_cost = ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost")
                    End If
                Else : _prjc_cost = ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost")
                End If

                _prjc_qty = ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty")
                _prjc_disc = ds_edit_cust.Tables(0).Rows(i).Item("prjc_disc")

                _amount_cust = _amount_cust + ((_prjc_qty * _prjc_cost) - (_prjc_qty * _prjc_cost * _prjc_disc))
            Next

            If _amount <> _amount_cust Then
                If MessageBox.Show("Total Amount tidak sama..!, Continue Save Data..?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
                    Return False
                End If
            End If
        End If

        Return before_save
    End Function

    Public Shared Function GetIDMax() As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_id),0) as max_col from ass_mstr "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetIDMax = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetIDMax
    End Function

    Public Shared Function GetMaxLine(ByVal _par_pt_id As Integer) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(ass_line),0) as max_col from ass_mstr " + _
                                           "where ass_pt_id = " + _par_pt_id.ToString()
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetMaxLine = .DataReader("max_col")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetMaxLine
    End Function

    Public Overrides Function insert() As Boolean
        _conf_value = func_coll.get_conf_file("wf_project_maintenance")

        Dim ssqls As New ArrayList
        Dim _prj_oid As Guid
        _prj_oid = Guid.NewGuid

        Dim _prj_code As String
        Dim ds_bantu As New DataSet
        Dim _prj_total, _prj_total_ppn, _prj_total_pph, _prjd_qty, _prjd_cost, _prjd_disc, _prj_total_temp, _tax_rate As Double
        'Dim _prjd_cost_real, _prjd_ppn, _prjd_pph, _cost_real As Double
        Dim _cost_real As Double
        Dim i, j As Integer
        Dim _prj_trans_id As String = ""

        _prj_code = func_coll.get_transaction_number("PJ", prj_en_id.GetColumnValue("en_code"), "prj_mstr", "prj_code")
        '_prj_code = prj_code.Text

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(prj_tran_id.EditValue)
            _prj_trans_id = "D"
        Else
            _prj_trans_id = "I"
        End If

        '******* Total PRJ, Total PPN, Total PPH
        _prj_total = 0
        _prj_total_ppn = 0
        _prj_total_pph = 0
        _prj_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("prjd_cost") / (1 + _tax_rate)))
                Else
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                End If
            Else : _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
            End If

            _prjd_qty = ds_edit.Tables(0).Rows(i).Item("prjd_qty")
            _prjd_disc = ds_edit.Tables(0).Rows(i).Item("prjd_disc")

            _prj_total = _prj_total + ((_prjd_qty * _prjd_cost) - (_prjd_qty * _prjd_cost * _prjd_disc))
        Next

        'ppn dan pph
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("prjd_cost") / (1 + _tax_rate)))
                Else
                    _prjd_cost = ds_edit.Tables(0).Rows(i).Item("prjd_cost")
                End If

                _prjd_qty = ds_edit.Tables(0).Rows(i).Item("prjd_qty")
                _prjd_disc = ds_edit.Tables(0).Rows(i).Item("prjd_disc")

                _prj_total_temp = ((_prjd_qty * _prjd_cost) - (_prjd_qty * _prjd_cost * _prjd_disc))
                _prj_total_ppn = _prj_total_ppn + (_prj_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")))
                _prj_total_pph = _prj_total_pph + (_prj_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")))
            Else
                _prj_total_ppn = _prj_total_ppn + 0
                _prj_total_pph = _prj_total_pph + 0
            End If
        Next
        '*******************************************************

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
                                            & "  public.prj_mstr " _
                                            & "( " _
                                            & "  prj_oid, " _
                                            & "  prj_dom_id, " _
                                            & "  prj_en_id, " _
                                            & "  prj_add_by, " _
                                            & "  prj_add_date, " _
                                            & "  prj_dt, " _
                                            & "  prj_code, " _
                                            & "  prj_ptnr_id_sold, " _
                                            & "  prj_ptnr_id_bill, " _
                                            & "  prj_sales_person_id, " _
                                            & "  prj_pjt_code_id, " _
                                            & "  prj_area_id, " _
                                            & "  prj_ord_date, " _
                                            & "  prj_start_date, " _
                                            & "  prj_end_date, " _
                                            & "  prj_si_id, " _
                                            & "  prj_cu_id, " _
                                            & "  prj_exc_rate, " _
                                            & "  prj_credit_term, " _
                                            & "  prj_taxable, " _
                                            & "  prj_tax_inc, " _
                                            & "  prj_tax_class, " _
                                            & "  prj_total, " _
                                            & "  prj_total_ppn, " _
                                            & "  prj_total_pph, " _
                                            & "  prj_tran_id, " _
                                            & "  prj_trans_id, " _
                                            & "  prj_pocust_oid, " _
                                            & "  prj_ar_ac_id, " _
                                            & "  prj_ar_sb_id, " _
                                            & "  prj_ar_cc_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_prj_oid.ToString()) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(prj_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " current_timestamp " & ", " _
                                            & " current_timestamp " & ", " _
                                            & SetSetring(_prj_code) & ",  " _
                                            & SetInteger(prj_ptnr_id_sold.EditValue) & ",  " _
                                            & SetInteger(prj_ptnr_id_bill.EditValue) & ",  " _
                                            & SetInteger(prj_sales_person_id.EditValue) & ",  " _
                                            & SetInteger(prj_pjt_code_id.EditValue) & ",  " _
                                            & SetInteger(prj_area_id.EditValue) & ",  " _
                                            & SetDate(prj_ord_date.DateTime.Date) & ",  " _
                                            & SetDate(prj_start_date.DateTime) & ",  " _
                                            & SetDate(prj_end_date.DateTime) & ",  " _
                                            & SetInteger(prj_si_id.EditValue) & ",  " _
                                            & SetInteger(prj_cu_id.EditValue) & ",  " _
                                            & SetDbl(prj_exc_rate.Text) & ",  " _
                                            & SetInteger(prj_credit_term.EditValue) & ",  " _
                                            & SetBitYN(prj_taxable.EditValue) & ",  " _
                                            & SetBitYN(prj_tax_inc.EditValue) & ",  " _
                                            & SetInteger(prj_tax_class.EditValue) & ",  " _
                                            & SetDbl(_prj_total) & ",  " _
                                            & SetDbl(_prj_total_ppn) & ",  " _
                                            & SetDbl(_prj_total_pph) & ",  " _
                                            & SetInteger(prj_tran_id.EditValue) & ",  " _
                                            & SetSetring(_prj_trans_id) & ",  " _
                                            & IIf(Trim(prj_pocust_oid.Text = ""), "null", SetSetring(_pocust_oid)) & ",  " _
                                            & SetInteger(prj_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(prj_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(prj_ar_cc_id.EditValue) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _cost_real = ds_edit.Tables(0).Rows(i).Item("prjd_cost") - (ds_edit.Tables(0).Rows(i).Item("prjd_cost") * ds_edit.Tables(0).Rows(i).Item("prjd_disc"))

                            'If ds_edit.Tables(0).Rows(i).Item("prjd_taxable").ToString.ToUpper = "Y" Then
                            '    If ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc").ToString.ToUpper = "Y" Then
                            '        _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_cost_real = _cost_real - (_tax_rate * (_cost_real / (1 + _tax_rate)))
                            '        _prjd_ppn = _prjd_cost_real * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_pph = _prjd_cost_real * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '    Else
                            '        _prjd_cost_real = _cost_real
                            '        _prjd_ppn = _cost_real * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '        _prjd_pph = _cost_real * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class"))
                            '    End If
                            'Else
                            '    _prjd_cost_real = _cost_real
                            '    _prjd_ppn = 0
                            '    _prjd_pph = 0
                            'End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjd_det " _
                                                & "( " _
                                                & "  prjd_oid, " _
                                                & "  prjd_dom_id, " _
                                                & "  prjd_en_id, " _
                                                & "  prjd_add_by, " _
                                                & "  prjd_add_date, " _
                                                & "  prjd_dt, " _
                                                & "  prjd_prj_oid, " _
                                                & "  prjd_seq, " _
                                                & "  prjd_si_id, " _
                                                & "  prjd_pt_id, " _
                                                & "  prjd_pt_desc1, " _
                                                & "  prjd_pt_desc2, " _
                                                & "  prjd_loc_id, " _
                                                & "  prjd_qty, " _
                                                & "  prjd_qty_full, " _
                                                & "  prjd_um, " _
                                                & "  prjd_cost, " _
                                                & "  prjd_price, " _
                                                & "  prjd_disc, " _
                                                & "  prjd_um_conv, " _
                                                & "  prjd_qty_real, " _
                                                & "  prjd_taxable, " _
                                                & "  prjd_tax_inc, " _
                                                & "  prjd_tax_class, " _
                                                & "  prjd_trans_id, " _
                                                & "  prjd_qty_pao, " _
                                                & "  prjd_qty_mo, " _
                                                & "  prjd_type " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid")) & ",  " _
                                                 & master_new.ClsVar.sdom_id & ",  " _
                                                & prj_en_id.EditValue & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(_prj_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc1")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("prjd_pt_desc2")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_loc_id")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty_full")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_disc")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("prjd_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_tax_class")) & ",  " _
                                                & SetSetring(_prj_trans_id) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("prjd_type")) & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Untuk insert wf_mstr ini relation nya ke detail project dan bukan ke header
                            If _conf_value = "1" Then
                                For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                    Dim _trans_id As String
                                    If ds_bantu.Tables(0).Rows(j).Item("aprv_seq") = 0 Then
                                        _trans_id = "Y"
                                    Else
                                        _trans_id = "N"
                                    End If

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.wf_mstr " _
                                                            & "( " _
                                                            & "  wf_oid, " _
                                                            & "  wf_dom_id, " _
                                                            & "  wf_en_id, " _
                                                            & "  wf_tran_id, " _
                                                            & "  wf_ref_oid, " _
                                                            & "  wf_ref_code, " _
                                                            & "  wf_ref_desc, " _
                                                            & "  wf_seq, " _
                                                            & "  wf_user_id, " _
                                                            & "  wf_wfs_id, " _
                                                            & "  wf_iscurrent, " _
                                                            & "  wf_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                            & SetInteger(prj_en_id.EditValue) & ",  " _
                                                            & SetSetring(prj_tran_id.EditValue) & ",  " _
                                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("prjd_oid")) & ",  " _
                                                            & SetSetring(_prj_code) & ",  " _
                                                            & SetSetring("Project") & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                            & SetInteger(0) & ",  " _
                                                            & SetSetring(_trans_id) & ",  " _
                                                            & " current_timestamp " & "  " _
                                                            & ")"
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If
                        Next

                        For i = 0 To ds_edit_cust.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjc_cust " _
                                                & "( " _
                                                & "  prjc_oid, " _
                                                & "  prjc_dom_id, " _
                                                & "  prjc_en_id, " _
                                                & "  prjc_add_by, " _
                                                & "  prjc_add_date, " _
                                                & "  prjc_dt, " _
                                                & "  prjc_prj_oid, " _
                                                & "  prjc_seq, " _
                                                & "  prjc_si_id, " _
                                                & "  prjc_cp_id, " _
                                                & "  prjc_pt_desc1, " _
                                                & "  prjc_pt_desc2, " _
                                                & "  prjc_loc_id, " _
                                                & "  prjc_qty, " _
                                                & "  prjc_qty_full, " _
                                                & "  prjc_um, " _
                                                & "  prjc_cost, " _
                                                & "  prjc_price, " _
                                                & "  prjc_disc, " _
                                                & "  prjc_um_conv, " _
                                                & "  prjc_qty_real, " _
                                                & "  prjc_taxable, " _
                                                & "  prjc_tax_inc, " _
                                                & "  prjc_tax_class, " _
                                                & "  prjc_trans_id, " _
                                                & "  prjc_qty_inv " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_oid")) & ",  " _
                                                & master_new.ClsVar.sdom_id & ",  " _
                                                & prj_en_id.EditValue & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(_prj_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_si_id")) & ",  " _
                                                & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cp_id")) & ",  " _
                                                & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc1")) & ",  " _
                                                & SetSetringDB(ds_edit_cust.Tables(0).Rows(i).Item("prjc_pt_desc2")) & ",  " _
                                                & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_loc_id")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_full")) & ",  " _
                                                & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_cost")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_price")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_disc")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_um_conv")) & ",  " _
                                                & SetDbl(ds_edit_cust.Tables(0).Rows(i).Item("prjc_qty_real")) & ",  " _
                                                & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_taxable")) & ",  " _
                                                & SetSetring(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit_cust.Tables(0).Rows(i).Item("prjc_tax_class")) & ",  " _
                                                & SetSetring(_prj_trans_id) & ",  " _
                                                & SetDbl(0) & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'insert pjc_mstr
                        Dim _pjc_id As Integer
                        _pjc_id = SetNewID_OLD("pjc_mstr", "pjc_id")
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pjc_mstr " _
                                                & "( " _
                                                & "  pjc_oid, " _
                                                & "  pjc_dom_id, " _
                                                & "  pjc_en_id, " _
                                                & "  pjc_add_by, " _
                                                & "  pjc_add_date, " _
                                                & "  pjc_id, " _
                                                & "  pjc_code, " _
                                                & "  pjc_date, " _
                                                & "  pjc_desc, " _
                                                & "  pjc_active, " _
                                                & "  pjc_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(prj_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(_pjc_id) & ",  " _
                                                & SetSetring(_prj_code) & ",  " _
                                                & SetDate(prj_ord_date.DateTime.Date) & ",  " _
                                                & SetSetring(_prj_code) & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_prj_code.ToString, "prj_code")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

#End Region

#Region "gv_edit"
    'Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
    '    Dim _rcvd_um_conv As Double = 1
    '    Dim _rcvd_qty As Double = 1
    '    Dim _rcvd_qty_real As Double

    '    If e.Column.Name = "rcvd_qty" Then
    '        Try
    '            _rcvd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "rcvd_um_conv"))
    '        Catch ex As Exception
    '        End Try

    '        _rcvd_qty_real = e.Value * _rcvd_um_conv
    '        gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
    '    ElseIf e.Column.Name = "rcvd_um_conv" Then
    '        Try
    '            _rcvd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "rcvd_qty")))
    '        Catch ex As Exception
    '        End Try

    '        _rcvd_qty_real = e.Value * _rcvd_qty
    '        gv_edit.SetRowCellValue(e.RowHandle, "rcvd_qty_real", _rcvd_qty_real)
    '    End If
    'End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub gv_edit_cust_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit_cust.DoubleClick
        browse_data_cust()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _prjd_en_id As Integer = gv_edit.GetRowCellValue(_row, "prjd_en_id")
        Dim _grid_name As String = ""

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.grid_call = "gv_edit"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjd_en_id
            frm.grid_call = "gv_edit"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm.grid_call = "gv_edit"
            frm._en_id = prj_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjd_en_id
            frm.grid_call = "gv_edit"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "tax_class" Then
            If gv_edit.GetRowCellValue(_row, "prjd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjd_en_id
            frm.grid_call = "gv_edit"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_code" Then
            Dim frm As New FProjectTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjd_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub browse_data_cust()
        Dim _col As String = gv_edit_cust.FocusedColumn.Name
        Dim _row As Integer = gv_edit_cust.FocusedRowHandle
        Dim _prjc_en_id As Integer = gv_edit_cust.GetRowCellValue(_row, "prjc_en_id")
        Dim _grid_name As String = ""

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.grid_call = "gv_edit_cust"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjc_en_id
            frm.grid_call = "gv_edit_cust"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "cp_code" Then
            Dim frm As New FPartNumberCustomerSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjc_en_id
            frm._ptnr_id = prj_ptnr_id_sold.EditValue
            'MessageBox.Show(prj_ptnr_id_sold.EditValue.ToString())
            'frm.grid_call = "gv_edit_cust"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjc_en_id
            frm.grid_call = "gv_edit_cust"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "tax_class" Then
            If gv_edit.GetRowCellValue(_row, "prjd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _prjc_en_id
            frm.grid_call = "gv_edit_cust"
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    'Private Sub gv_serial_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_serial.InitNewRow
    '    'Dim _row_edit As Integer
    '    '_row_edit = BindingContext(ds_edit.Tables(0)).Position

    '    'If IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_loc_id")) = True Then
    '    '    MessageBox.Show("Location Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '    Exit Sub
    '    'End If

    '    'With gv_serial
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_oid", Guid.NewGuid.ToString)
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_rcvd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_oid"))
    '    '    .SetRowCellValue(e.RowHandle, "rcvd_pod_oid", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_pod_oid"))
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_qty", 1)
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_um", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_um"))
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_si_id", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_si_id"))
    '    '    .SetRowCellValue(e.RowHandle, "rcvds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("rcvd_loc_id"))

    '    '    .SetRowCellValue(e.RowHandle, "pod_pt_id", ds_edit.Tables(0).Rows(_row_edit).Item("pod_pt_id"))

    '    '    .SetRowCellValue(e.RowHandle, "pod_pt_desc1", ds_edit.Tables(0).Rows(_row_edit).Item("pod_pt_desc1"))
    '    '    .SetRowCellValue(e.RowHandle, "pod_pt_desc2", ds_edit.Tables(0).Rows(_row_edit).Item("pod_pt_desc2"))
    '    '    .SetRowCellValue(e.RowHandle, "pt_type", ds_edit.Tables(0).Rows(_row_edit).Item("pt_type"))
    '    '    .SetRowCellValue(e.RowHandle, "pt_pl_id", ds_edit.Tables(0).Rows(_row_edit).Item("pt_pl_id"))
    '    '    .SetRowCellValue(e.RowHandle, "pl_fa_depr", ds_edit.Tables(0).Rows(_row_edit).Item("pl_fa_depr"))

    '    '    .SetRowCellValue(e.RowHandle, "pod_pt_class", ds_edit.Tables(0).Rows(_row_edit).Item("pod_pt_class"))
    '    '    '.SetRowCellValue(e.RowHandle, "pod_emp_id", ds_edit.Tables(0).Rows(_row_edit).Item("pod_emp_id"))
    '    '    .SetRowCellValue(e.RowHandle, "po_ptnr_id", ds_edit.Tables(0).Rows(_row_edit).Item("po_ptnr_id"))

    '    '    .BestFitColumns()
    '    'End With
    'End Sub

    'Private Sub gv_serial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_serial.KeyDown
    '    If e.Control And e.KeyCode = Keys.I Then
    '        gv_serial.AddNewRow()
    '    ElseIf e.Control And e.KeyCode = Keys.D Then
    '        gv_serial.DeleteSelectedRows()
    '    End If
    'End Sub

#End Region

    Public Function get_tanggal_sistem() As Date
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select current_date as tanggal"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_tanggal_sistem = .DataReader.Item("tanggal")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return get_tanggal_sistem
    End Function

    'rev by hendrik 2011-03-09
    Private Function GetIdProjectType() As Integer
        GetIdProjectType = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_id from code_mstr " _
                                         & " where code_field = 'prjd_type' and code_default = 'Y' " _
                                         & " limit 1 "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetIdProjectType = .DataReader.Item("code_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetIdProjectType
    End Function

    'rev by hendrik 2011-03-09
    Private Function GetCodeProjectType() As String
        GetCodeProjectType = ""
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_code from code_mstr " _
                                         & " where code_field = 'prjd_type' and code_default = 'Y' " _
                                         & " limit 1 "
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetCodeProjectType = .DataReader.Item("code_code")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetCodeProjectType
    End Function

    Private Sub gv_edit_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "prjd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "prjd_en_id", prj_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", prj_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "prjd_si_id", prj_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", prj_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "prjd_qty", 0)
            .SetRowCellValue(e.RowHandle, "prjd_cost", 0)
            .SetRowCellValue(e.RowHandle, "prjd_price", 0)
            .SetRowCellValue(e.RowHandle, "prjd_disc", 0)
            .SetRowCellValue(e.RowHandle, "prjd_taxable", IIf(prj_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "prjd_tax_inc", IIf(prj_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "prjd_tax_class", prj_tax_class.EditValue)
            .SetRowCellValue(e.RowHandle, "tax_class", prj_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "prjd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "prjd_qty_real", 0)
            .SetRowCellValue(e.RowHandle, "prjd_qty_pao", 0)
            .SetRowCellValue(e.RowHandle, "prjd_cost", 0)
            .SetRowCellValue(e.RowHandle, "prjd_type", GetIdProjectType())
            .SetRowCellValue(e.RowHandle, "code_code", GetCodeProjectType())
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_cust_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_cust.InitNewRow
        With gv_edit_cust
            .SetRowCellValue(e.RowHandle, "prjc_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "prjc_en_id", prj_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", prj_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "prjc_si_id", prj_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", prj_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "prjc_qty", 0)
            .SetRowCellValue(e.RowHandle, "prjc_cost", 0)
            .SetRowCellValue(e.RowHandle, "prjc_price", 0)
            .SetRowCellValue(e.RowHandle, "prjc_disc", 0)
            .SetRowCellValue(e.RowHandle, "prjc_taxable", IIf(prj_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "prjc_tax_inc", IIf(prj_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "prjc_tax_class", prj_tax_class.EditValue)
            .SetRowCellValue(e.RowHandle, "tax_class", prj_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "prjc_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "prjc_qty_real", 0)
            .BestFitColumns()
        End With
    End Sub


    Private Sub gv_edit_cust_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_cust.CellValueChanged
        Dim _prjc_qty, _prjc_qty_real, _prjc_um_conv, _prjc_qty_cost, _prjc_cost, _prjc_disc As Double
        _prjc_um_conv = 1
        _prjc_qty = 1
        _prjc_qty_cost = 0
        _prjc_disc = 0

        If e.Column.Name = "prjc_qty" Then
            Try
                _prjc_um_conv = (gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _prjc_cost = (gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_cost"))
            Catch ex As Exception
            End Try

            Try
                _prjc_disc = (gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_disc"))
            Catch ex As Exception
            End Try

            _prjc_qty_real = e.Value * _prjc_um_conv
            _prjc_qty_cost = (e.Value * _prjc_cost) - (e.Value * _prjc_cost * _prjc_disc)

            gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_qty_real", _prjc_qty_real)
            gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_qty_cost", _prjc_qty_cost)
        ElseIf e.Column.Name = "prjc_cost" Then
            Try
                _prjc_qty = ((gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_qty")))
            Catch ex As Exception
            End Try

            Try
                _prjc_disc = ((gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_disc")))
            Catch ex As Exception
            End Try

            _prjc_qty_cost = (e.Value * _prjc_qty) - (e.Value * _prjc_qty * _prjc_disc)
            gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_qty_cost", _prjc_qty_cost)
        ElseIf e.Column.Name = "prjc_disc" Then
            Try
                _prjc_qty = ((gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_qty")))
            Catch ex As Exception
            End Try

            Try
                _prjc_cost = ((gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_cost")))
            Catch ex As Exception
            End Try

            _prjc_qty_cost = (_prjc_cost * _prjc_qty) - (_prjc_cost * _prjc_qty * e.Value)
            gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_qty_cost", _prjc_qty_cost)
        ElseIf e.Column.Name = "prjc_um_conv" Then
            Try
                _prjc_qty = ((gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_qty")))
            Catch ex As Exception
            End Try

            _prjc_qty_real = e.Value * _prjc_qty

            gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_qty_real", _prjc_qty_real)
        ElseIf e.Column.Name = "prjc_taxable" Then
            If gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_taxable").ToString.ToUpper = "N" Then
                gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_tax_inc", "N")
                gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit_cust.SetRowCellValue(e.RowHandle, "tax_class", "NON-TAX")
            End If
        ElseIf e.Column.Name = "prjc_tax_inc" Then
            If gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_tax_inc").ToString.ToUpper = "Y" And gv_edit_cust.GetRowCellValue(e.RowHandle, "prjc_taxable").ToString.ToUpper = "N" Then
                gv_edit_cust.SetRowCellValue(e.RowHandle, "prjc_tax_inc", "N")
            End If
        End If
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _prjd_qty, _prjd_qty_real, _prjd_um_conv, _prjd_qty_cost, _prjd_cost, _prjd_disc As Double
        _prjd_um_conv = 1
        _prjd_qty = 1
        _prjd_qty_cost = 0
        _prjd_disc = 0

        If e.Column.Name = "prjd_qty" Then
            Try
                _prjd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _prjd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_cost"))
            Catch ex As Exception
            End Try

            Try
                _prjd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "prjd_disc"))
            Catch ex As Exception
            End Try

            _prjd_qty_real = e.Value * _prjd_um_conv
            _prjd_qty_cost = (e.Value * _prjd_cost) - (e.Value * _prjd_cost * _prjd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_real", _prjd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        ElseIf e.Column.Name = "prjd_cost" Then
            Try
                _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
            Catch ex As Exception
            End Try

            Try
                _prjd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_disc")))
            Catch ex As Exception
            End Try

            _prjd_qty_cost = (e.Value * _prjd_qty) - (e.Value * _prjd_qty * _prjd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        ElseIf e.Column.Name = "prjd_disc" Then
            Try
                _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
            Catch ex As Exception
            End Try

            Try
                _prjd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_cost")))
            Catch ex As Exception
            End Try

            _prjd_qty_cost = (_prjd_cost * _prjd_qty) - (_prjd_cost * _prjd_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_cost", _prjd_qty_cost)
        ElseIf e.Column.Name = "prjd_um_conv" Then
            Try
                _prjd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "prjd_qty")))
            Catch ex As Exception
            End Try

            _prjd_qty_real = e.Value * _prjd_qty

            gv_edit.SetRowCellValue(e.RowHandle, "prjd_qty_real", _prjd_qty_real)
        ElseIf e.Column.Name = "prjd_taxable" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "prjd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_inc", "N")
                gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit.SetRowCellValue(e.RowHandle, "tax_class", "NON-TAX")
            End If
        ElseIf e.Column.Name = "prjd_tax_inc" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "prjd_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "prjd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "prjd_tax_inc", "N")
            End If
        End If
    End Sub

#Region "Approval"

    'Public Overrides Sub approve_line()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
    '    _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code")
    '    _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid")
    '    _colom = "prj_trans_id"
    '    _table = "prj_mstr"
    '    _criteria = "prj_code"
    '    _initial = "prj"
    '    _type = "pj"
    '    _title = "Project"
    '    approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    'End Sub

    'Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
    '                               ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
    '    Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String

    '    If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
    '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    _pby_code = par_code
    '    _trn_status = "W"
    '    user_wf = mf.get_user_wf(par_code, 0)
    '    user_wf_email = mf.get_email_address(user_wf)

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
    '                                           " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
    '                                           " " + par_initial + "_upd_date = current_timestamp " + _
    '                                           " where " + par_initial + "_oid = '" + par_oid + "'"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    For Each _dr As DataRow In ds.Tables("detail").Rows
    '                        If _dr("prjd_prj_oid") = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid") Then
    '                            .Command.CommandType = CommandType.Text
    '                            .Command.CommandText = "update prjd_det set prjd_trans_id = '" + _trn_status + "' " + _
    '                                                   " where prjd_prj_oid = '" + par_oid + "'"
    '                            .Command.ExecuteNonQuery()
    '                            .Command.Parameters.Clear()
    '                        End If
    '                    Next

    '                    '============================================================================
    '                    If get_status_wf(par_code.ToString()) = 0 Then
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'" + _
    '                                               " and wf_seq = 0"

    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'"

    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                    ElseIf get_status_wf(par_code.ToString()) > 0 Then
    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "update wf_mstr set " + _
    '                                               " wf_iscurrent = 'Y', " + _
    '                                               " wf_wfs_id = '0', " + _
    '                                               " wf_desc = '', " + _
    '                                               " wf_date_to = null, " + _
    '                                               " wf_aprv_user = '', " + _
    '                                               " wf_aprv_date = null " + _
    '                                               " where wf_ref_code ~~* '" + par_code + "'" + _
    '                                               " and wf_wfs_id = '4' "
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()
    '                    End If
    '                    '============================================================================

    '                    sqlTran.Commit()

    '                    format_email_bantu = mf.format_email(user_wf, par_code, par_type)

    '                    filename = "c:\syspro\" + par_code + ".xls"
    '                    ExportTo(par_gv, New ExportXlsProvider(filename))

    '                    If user_wf_email <> "" Then
    '                        mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                    Else
    '                        'MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    End If

    '                    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    help_load_data(True)
    '                    set_row(Trim(par_oid), par_initial + "_oid")
    '                Catch ex As nPgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Public Overrides Sub cancel_line()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
    '    _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code")
    '    _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid")
    '    _colom = "prj_trans_id"
    '    _table = "prj_mstr"
    '    _criteria = "prj_code"
    '    _initial = "prj"
    '    _type = "pj"

    '    cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    'End Sub

    'Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
    '                               ByVal par_initial As String, ByVal par_type As String)
    '    _conf_value = func_coll.get_conf_file("wf_project_maintenance")

    '    Dim _prj_trans_id As String = ""
    '    Dim ssqls As New ArrayList

    '    If _conf_value = "1" Then
    '        Try
    '            Using objcek As New master_new.WDABasepgsql("", "")
    '                With objcek
    '                    .Connection.Open()
    '                    .Command = .Connection.CreateCommand
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "select prj_trans_id from prj_mstr where prj_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_oid").ToString)
    '                    .InitializeCommand()
    '                    .DataReader = .Command.ExecuteReader
    '                    While .DataReader.Read
    '                        _prj_trans_id = .DataReader("prj_trans_id").ToString
    '                    End While
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If

    '    If _prj_trans_id.ToUpper = "D" Then
    '        MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
    '        Exit Sub
    '    ElseIf _prj_trans_id.ToUpper = "C" Then
    '        MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
    '        Exit Sub
    '    ElseIf _prj_trans_id.ToUpper = "X" Then
    '        MessageBox.Show("Can't Cancel For Cancel Data..", "Conf", MessageBoxButtons.OK)
    '        Exit Sub
    '    Else
    '        If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
    '            Exit Sub
    '        End If
    '    End If

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
    '                                           " where " + par_criteria + " = '" + par_code + "'"
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
    '                                           " where wf_ref_code = '" + par_code + "'"
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    sqlTran.Commit()
    '                    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    row = BindingContext(ds.Tables(0)).Position
    '                    help_load_data(True)
    '                    set_row(Trim(par_oid), par_initial + "_oid")
    '                Catch ex As nPgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    'Public Overrides Sub reminder_mail()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _code, _type, _user, _title As String
    '    _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("prj_code")
    '    _type = "pj"
    '    _user = func_coll.get_wf_iscurrent(_code)
    '    _title = "Project"

    '    If _user = "" Then
    '        MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    reminder_by_mail(_code, _type, _user, gv_email, _title)
    'End Sub

    'Public Overrides Sub smart_approve()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
    '    Dim i As Integer
    '    Dim _po_is_budget As String = ""

    '    If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
    '        Exit Sub
    '    End If

    '    ds.Tables("smart").AcceptChanges()

    '    For i = 0 To ds.Tables("smart").Rows.Count - 1
    '        If ds.Tables("smart").Rows(i).Item("status") = True Then

    '            Try
    '                gv_email.Columns("prj_oid").FilterInfo = _
    '                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[po_oid] = '" & ds.Tables("smart").Rows(i).Item("prj_oid").ToString & "'")
    '                gv_email.BestFitColumns()

    '                'gv_email.Columns("po_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("po_oid"))
    '            Catch ex As Exception
    '            End Try

    '            _trans_id = "W" 'default langsung ke W 
    '            user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("prj_code"), 0)
    '            user_wf_email = mf.get_email_address(user_wf)

    '            Try
    '                Using objinsert As New master_new.WDABasepgsql("", "")
    '                    With objinsert
    '                        .Connection.Open()
    '                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                        Try
    '                            .Command = .Connection.CreateCommand
    '                            .Command.Transaction = sqlTran
    '                            .Command.CommandType = CommandType.Text
    '                            .Command.CommandText = "update prj_mstr set prj_trans_id = '" + _trans_id + "'," + _
    '                                           " prj_upd_by = '" + master_new.ClsVar.sNama + "'," + _
    '                                           " prj_upd_date = current_timestamp " + _
    '                                           " where prj_oid = '" + ds.Tables("smart").Rows(i).Item("prj_oid") + "'"

    '                            .Command.ExecuteNonQuery()
    '                            .Command.Parameters.Clear()

    '                            '============================================================================
    '                            If get_status_wf(ds.Tables("smart").Rows(i).Item("prj_code")) = 0 Then
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("prj_code") + "'" + _
    '                                                       " and wf_seq = 0"

    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()

    '                                'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("prj_code") + "'"

    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()

    '                            ElseIf get_status_wf(ds.Tables("smart").Rows(i).Item("prj_code")) > 0 Then
    '                                .Command.CommandType = CommandType.Text
    '                                .Command.CommandText = "update wf_mstr set " + _
    '                                                       " wf_iscurrent = 'Y', " + _
    '                                                       " wf_wfs_id = '0', " + _
    '                                                       " wf_desc = '', " + _
    '                                                       " wf_date_to = null, " + _
    '                                                       " wf_aprv_user = '', " + _
    '                                                       " wf_aprv_date = null " + _
    '                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("prj_code") + "'" + _
    '                                                       " and wf_wfs_id = '4' "
    '                                .Command.ExecuteNonQuery()
    '                                .Command.Parameters.Clear()
    '                            End If
    '                            '============================================================================

    '                            sqlTran.Commit()

    '                            format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("prj_code"), "prj")

    '                            filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("prj_code") + ".xls"
    '                            ExportTo(gv_email, New ExportXlsProvider(filename))

    '                            If user_wf_email <> "" Then
    '                                mf.sent_email(user_wf_email, "", mf.title_email("Project", ds.Tables("smart").Rows(i).Item("prj_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
    '                            Else
    '                                MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                            End If

    '                        Catch ex As nPgSqlException
    '                            sqlTran.Rollback()
    '                            MessageBox.Show(ex.Message)
    '                        End Try
    '                    End With
    '                End Using
    '            Catch ex As Exception
    '                MessageBox.Show(ex.Message)
    '            End Try
    '        End If
    '    Next

    '    help_load_data(True)
    '    MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub

#End Region

    Private Sub gv_edit_KeyDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_cust_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_cust.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_cust.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_cust.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_cust_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_cust.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data_cust()
        End If
    End Sub

    Private Sub prj_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prj_taxable.CheckedChanged
        If prj_taxable.EditValue = False Then
            prj_tax_class.Enabled = False
            prj_tax_class.ItemIndex = 0
            prj_tax_inc.Enabled = False
            prj_tax_inc.Checked = False
        Else
            prj_tax_class.Enabled = True
            prj_tax_inc.Enabled = True
            prj_tax_inc.Checked = False
        End If
    End Sub

    Private Sub prj_pocust_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles prj_pocust_oid.ButtonClick
        Dim frm As New FPOCustomerSearch
        frm.set_win(Me)
        frm._en_id = prj_en_id.EditValue
        frm._obj = prj_pocust_oid
        frm._ptnr_id = prj_ptnr_id_bill.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
