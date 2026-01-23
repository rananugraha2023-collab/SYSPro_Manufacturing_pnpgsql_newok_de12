<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMainMenu
    Inherits master_new.MasterMDI

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FMainMenu))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer5 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.menudesc = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.menuseq = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.btFindNext = New DevExpress.XtraEditors.SimpleButton
        Me.btSearch = New DevExpress.XtraEditors.SimpleButton
        Me.txtSearchMenu = New DevExpress.XtraEditors.TextEdit
        Me.ceExpandCollaps = New DevExpress.XtraEditors.CheckEdit
        Me.panelContainer1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.dp_financial = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer2 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.nbc_financial = New DevExpress.XtraNavBar.NavBarControl
        Me.nbg_taxation = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_so_ar_fp_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_tax = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_faktur_pajak_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_faktur_pajak_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_faktur_pajak = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_faktur_pajak_sign = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_faktur_pajak_transaction_code = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_ar = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_ar_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_payment = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo_detail = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo_detail_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_payment_detail = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_report_by_aging = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_report_by_aging_sdi = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_report_by_top = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_report_pending_invoice = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_cash_in_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ar_dbcr_balance_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_payment_ar_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_ap = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_ap_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_payment = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_report_by_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_report_by_aging = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_report_by_top = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_report_by_unvouchered = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_ap_voucher_balance_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_gl = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_gl_entity = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_entity_group = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_glcalendar = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_project = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_account = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_subaccount = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_cost_center = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_cost_center_user = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_cost_center_account = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_opening_balance = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_standard_transaction = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_unposted_transaction = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_transaction_post = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_month_end = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_year_end = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_forex = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_add_account_glbal = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_multiple_currency = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_mc_currency = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_mc_exchange_rate = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_mc_bank = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_fin_gl_report = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_gl_general_ledger_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_plSetting = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_profit_loss_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_cfSetting = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_cash_flow = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_trial_balance_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bsSetting = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_gl_balance_sheet = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_budget = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_fin_bgt_periode = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bgt_generate = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bgt_maintenance = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bgt_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bgt_cross = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_fin_bgt_cross_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_cash_management = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_cash_transfer = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_cash_in = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_cash_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_cash_bank_reconciliation = New DevExpress.XtraNavBar.NavBarItem
        Me.ImageList5 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList4 = New System.Windows.Forms.ImageList(Me.components)
        Me.dp_master_data = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.nbc_master_data = New DevExpress.XtraNavBar.NavBarControl
        Me.nbg_master_data_company = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_master_data_domain = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_comp_company_address = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_comp_employee = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_master_data_agama = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_org_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_organization = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_organization_structure = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_org_struc_detail = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_organizationtree = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_comp_tran_status = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_emp_transaction = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_company_position = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_company_dok_aprv = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_company_routing_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_company_conf = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_item_site = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_mst_is_partnumber = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_approval_tax = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_approval_accounting = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_part_number_group = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_inventorystatus = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_site = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_itemstatus = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_item_site_cost = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_warehouse = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_warehouse_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_warehouse_category = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_location = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_location_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_location_category = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_productline = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_prodline_location = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_unitmeasure = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_is_unit_conversion = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_address_taxes = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_mst_at_partner = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_partner_address = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_partner_cp = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_partner_all = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_cp_function = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_partner_bank = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_partner_group = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_address_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_taxclass = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_tax_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_at_tax_rate = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_mst_fin_credit_terms = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_trn_req = New DevExpress.XtraNavBar.NavBarItem
        Me.dp_distribution = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.nbc_distribution = New DevExpress.XtraNavBar.NavBarControl
        Me.nbg_requistion = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_dist_req_mstr = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_mstr_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_mstr_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_transfer_issue = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_transfer_issue_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_transfer_issue_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_transfer_receipt = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_req_transfer_receipt_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_pr_boq = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_purchase_order = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_dist_purchase_order = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_order_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_order_print_no_cost = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_order_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_receipt = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_receipt_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_receipt_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_return = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_purchase_return_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_reason_code_return = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_purchase_order_filem = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_purchase_order_filem_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_inventory_control = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_inv_inventory_begining_balance = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_inventory_detail = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_inventory_detail_2 = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_dist_inv_history = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_inventory_by_eff_date = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_request = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_request_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_request_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_receipts = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_receipts_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_issues = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_issues_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_issues = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_issues_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_issues_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_receipts = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_receipts_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_adjusment_plus = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_adjustment_minus = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_cycle_count = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_transfer_issues_return = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_isues_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_receipts_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_adjusment_plus_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_adjustment_minus_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_inv_report_detail_wip = New DevExpress.XtraNavBar.NavBarItem
        Me.dp_sales_order = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer3 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.nbc_sales_order = New DevExpress.XtraNavBar.NavBarControl
        Me.nbg_sales_order = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_sales_order_sales_program = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_pay_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_payment_methode = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_reason_code = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_so_promo = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_pricelist = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_pricelist_header = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_pricelist_detail = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_pricelist_copy = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_so = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_so_credit_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_so_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_so_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_so_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_faktur_penjualan_print = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_shipments = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_shipments_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_returns = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_returns_print_out = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_manual_allocation = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_so_retur_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_so_ship_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_so_faktur_pajak_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_so_insentif_royalti = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_sales_order_area = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_sales_struktur = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_royalti_rule = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_royalti_periode = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_royalti = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_periode_intensif_ds = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_rule_ds = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_intensif_ds = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_periode_intensif_rs = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_rule_rs = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_cf_rs = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_sales_order_intensif_rs = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_project = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_prj_project_type = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_prj_project_area = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_prj_po_customer = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_prj_project = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_prj_boq = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_prj_boq_approval = New DevExpress.XtraNavBar.NavBarItem
        Me.dp_manufacturing = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer4 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.NavBarControl1 = New DevExpress.XtraNavBar.NavBarControl
        Me.nbg_product_structure = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_manu_prod_structure = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_prod_structure_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_where_in_used = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_materials_summary_report = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_simulated_picklist = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_item_subtitute = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_man_master = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_man_bom = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_producstructure = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_pt_subtitute = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_department = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_workcenter = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_machine = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_routing = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_workcenter_machine = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_CostSet = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_cost_element = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_man_transaksi = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_man_wo_project = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_work_order = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_release = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_maintenance = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_component_issue = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_receipt = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_wo_CostElementRealProject = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_labour_feedback = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_reason_reject = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_man_reason_rework = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_routing_work_center = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_manu_departement = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_work_center = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_tools = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_routing = New DevExpress.XtraNavBar.NavBarItem
        Me.nbg_work_order = New DevExpress.XtraNavBar.NavBarGroup
        Me.nbi_manu_work_order = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_wo_release = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_wo_comp_issue = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_wo_bill = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_wo_receipt = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_wip = New DevExpress.XtraNavBar.NavBarItem
        Me.nbi_manu_trans_issue_wip = New DevExpress.XtraNavBar.NavBarItem
        Me.bbi_manufacturing = New DevExpress.XtraBars.BarButtonItem
        Me.bci_manufacturing = New DevExpress.XtraBars.BarCheckItem
        Me.bci_manufacture = New DevExpress.XtraBars.BarCheckItem
        Me.bbi_user_history = New DevExpress.XtraBars.BarButtonItem
        'Me.DataTable1TableAdapter = New sygma_solution_system.ds_cash_outTableAdapters.DataTable1TableAdapter
        'Me.DataTable1TableAdapter1 = New sygma_solution_system.ds_cash_inTableAdapters.DataTable1TableAdapter
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.BarStaticItem1 = New DevExpress.XtraBars.BarStaticItem
        Me.BarLargeButtonItem1 = New DevExpress.XtraBars.BarLargeButtonItem
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.ControlContainer5.SuspendLayout()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceExpandCollaps.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelContainer1.SuspendLayout()
        Me.dp_financial.SuspendLayout()
        Me.ControlContainer2.SuspendLayout()
        CType(Me.nbc_financial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_master_data.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.nbc_master_data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_distribution.SuspendLayout()
        Me.ControlContainer1.SuspendLayout()
        CType(Me.nbc_distribution, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_sales_order.SuspendLayout()
        Me.ControlContainer3.SuspendLayout()
        CType(Me.nbc_sales_order, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_manufacturing.SuspendLayout()
        Me.ControlContainer4.SuspendLayout()
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BarAndDockingController1
        '
        Me.BarAndDockingController1.PropertiesBar.AllowLinkLighting = False
        '
        'DockManager1
        '
        Me.DockManager1.Controller = Me.BarAndDockingController1
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1, Me.panelContainer1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.ControlContainer5)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left
        Me.DockPanel1.ID = New System.Guid("56bd63aa-d1e2-49cc-943e-e2f2b0767f63")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 87)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(281, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(281, 553)
        Me.DockPanel1.Text = "Menu"
        '
        'ControlContainer5
        '
        Me.ControlContainer5.Controls.Add(Me.TreeList1)
        Me.ControlContainer5.Controls.Add(Me.PanelControl1)
        Me.ControlContainer5.Location = New System.Drawing.Point(3, 25)
        Me.ControlContainer5.Name = "ControlContainer5"
        Me.ControlContainer5.Size = New System.Drawing.Size(275, 525)
        Me.ControlContainer5.TabIndex = 0
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.menudesc, Me.menuseq})
        Me.TreeList1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "menuid"
        Me.TreeList1.Location = New System.Drawing.Point(0, 26)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.Editable = False
        Me.TreeList1.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeList1.OptionsView.ShowColumns = False
        Me.TreeList1.ParentFieldName = "menuid_parent"
        Me.TreeList1.SelectImageList = Me.ImageList1
        Me.TreeList1.Size = New System.Drawing.Size(275, 499)
        Me.TreeList1.TabIndex = 28
        '
        'menudesc
        '
        Me.menudesc.Caption = "Menu"
        Me.menudesc.FieldName = "menudesc"
        Me.menudesc.Name = "menudesc"
        Me.menudesc.Visible = True
        Me.menudesc.VisibleIndex = 0
        '
        'menuseq
        '
        Me.menuseq.Caption = "menuseq"
        Me.menuseq.FieldName = "menuseq"
        Me.menuseq.Name = "menuseq"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "shell32 005.ico")
        Me.ImageList1.Images.SetKeyName(1, "001_20.ico")
        Me.ImageList1.Images.SetKeyName(2, "001_43.ico")
        Me.ImageList1.Images.SetKeyName(3, "001_60.ico")
        Me.ImageList1.Images.SetKeyName(4, "001_59.ico")
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btFindNext)
        Me.PanelControl1.Controls.Add(Me.btSearch)
        Me.PanelControl1.Controls.Add(Me.txtSearchMenu)
        Me.PanelControl1.Controls.Add(Me.ceExpandCollaps)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(275, 26)
        Me.PanelControl1.TabIndex = 29
        '
        'btFindNext
        '
        Me.btFindNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btFindNext.Location = New System.Drawing.Point(236, 3)
        Me.btFindNext.Name = "btFindNext"
        Me.btFindNext.Size = New System.Drawing.Size(31, 20)
        Me.btFindNext.TabIndex = 3
        Me.btFindNext.Text = "Next"
        '
        'btSearch
        '
        Me.btSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btSearch.Image = Global.sygma_solution_system.My.Resources.Resources.search_icon2
        Me.btSearch.Location = New System.Drawing.Point(201, 3)
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(31, 20)
        Me.btSearch.TabIndex = 2
        Me.btSearch.Text = " "
        '
        'txtSearchMenu
        '
        Me.txtSearchMenu.Location = New System.Drawing.Point(86, 3)
        Me.txtSearchMenu.MenuManager = Me.BarManager1
        Me.txtSearchMenu.Name = "txtSearchMenu"
        Me.txtSearchMenu.Size = New System.Drawing.Size(111, 20)
        Me.txtSearchMenu.TabIndex = 1
        '
        'ceExpandCollaps
        '
        Me.ceExpandCollaps.EditValue = True
        Me.ceExpandCollaps.Location = New System.Drawing.Point(6, 3)
        Me.ceExpandCollaps.MenuManager = Me.BarManager1
        Me.ceExpandCollaps.Name = "ceExpandCollaps"
        Me.ceExpandCollaps.Properties.Caption = "Expand All"
        Me.ceExpandCollaps.Size = New System.Drawing.Size(76, 19)
        Me.ceExpandCollaps.TabIndex = 0
        '
        'panelContainer1
        '
        Me.panelContainer1.ActiveChild = Me.dp_financial
        Me.panelContainer1.Controls.Add(Me.dp_master_data)
        Me.panelContainer1.Controls.Add(Me.dp_distribution)
        Me.panelContainer1.Controls.Add(Me.dp_sales_order)
        Me.panelContainer1.Controls.Add(Me.dp_financial)
        Me.panelContainer1.Controls.Add(Me.dp_manufacturing)
        Me.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right
        Me.panelContainer1.FloatSize = New System.Drawing.Size(200, 314)
        Me.panelContainer1.ID = New System.Guid("e51c8083-bea4-4222-bfb5-d3cd65343d50")
        Me.panelContainer1.Location = New System.Drawing.Point(1034, 87)
        Me.panelContainer1.Name = "panelContainer1"
        Me.panelContainer1.OriginalSize = New System.Drawing.Size(224, 577)
        Me.panelContainer1.Size = New System.Drawing.Size(224, 553)
        Me.panelContainer1.Tabbed = True
        Me.panelContainer1.Text = "panelContainer1"
        '
        'dp_financial
        '
        Me.dp_financial.Controls.Add(Me.ControlContainer2)
        Me.dp_financial.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.dp_financial.FloatSize = New System.Drawing.Size(200, 314)
        Me.dp_financial.FloatVertical = True
        Me.dp_financial.ID = New System.Guid("ac94eff9-6328-49b7-b01d-c32f959d9f13")
        Me.dp_financial.Location = New System.Drawing.Point(3, 25)
        Me.dp_financial.Name = "dp_financial"
        Me.dp_financial.OriginalSize = New System.Drawing.Size(218, 485)
        Me.dp_financial.Size = New System.Drawing.Size(218, 503)
        Me.dp_financial.Text = "Financial"
        '
        'ControlContainer2
        '
        Me.ControlContainer2.Controls.Add(Me.nbc_financial)
        Me.ControlContainer2.Location = New System.Drawing.Point(0, 0)
        Me.ControlContainer2.Name = "ControlContainer2"
        Me.ControlContainer2.Size = New System.Drawing.Size(218, 503)
        Me.ControlContainer2.TabIndex = 0
        '
        'nbc_financial
        '
        Me.nbc_financial.ActiveGroup = Me.nbg_taxation
        Me.nbc_financial.ContentButtonHint = Nothing
        Me.nbc_financial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nbc_financial.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nbg_ar, Me.nbg_ap, Me.nbg_gl, Me.nbg_multiple_currency, Me.nbg_fin_gl_report, Me.nbg_budget, Me.nbg_cash_management, Me.nbg_taxation})
        Me.nbc_financial.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nbi_fin_gl_standard_transaction, Me.nbi_fin_gl_unposted_transaction, Me.nbi_fin_gl_transaction_post, Me.nbi_fin_ap_voucher, Me.nbi_fin_ap_payment, Me.nbi_fin_mc_exchange_rate, Me.nbi_fin_mc_currency, Me.nbi_fin_mc_bank, Me.nbi_fin_gl_entity, Me.nbi_fin_gl_glcalendar, Me.nbi_fin_gl_account, Me.nbi_fin_gl_subaccount, Me.nbi_fin_gl_cost_center, Me.nbi_fin_gl_project, Me.nbi_fin_ap_type, Me.nbi_fin_gl_year_end, Me.nbi_fin_gl_forex, Me.nbi_fin_gl_general_ledger_report, Me.nbi_fin_gl_profit_loss_report, Me.nbi_fin_gl_trial_balance_report, Me.nbi_fin_gl_balance_sheet, Me.nbi_fin_gl_cash_flow, Me.nbi_fin_gl_opening_balance, Me.nbi_fin_gl_month_end, Me.nbi_fin_ar_dc_memo, Me.nbi_fin_ar_payment, Me.nbi_fin_ar_type, Me.nbi_fin_ar_dc_memo_print_out, Me.nbi_fin_ar_dc_memo_konsiyasi_print_out, Me.nbi_fin_ar_faktur_pajak, Me.nbi_fin_ar_faktur_pajak_print, Me.nbi_fin_ar_faktur_pajak_transaction_code, Me.nbi_fin_gl_entity_group, Me.nbi_fin_ap_voucher_tax, Me.nbi_fin_ap_voucher_report_by_type, Me.nbi_fin_ap_voucher_report_by_aging, Me.nbi_fin_ap_voucher_report_by_top, Me.nbi_fin_ap_voucher_report_by_unvouchered, Me.nbi_fin_ar_dc_memo_detail, Me.nbi_fin_ar_payment_detail, Me.nbi_fin_ar_dc_memo_report, Me.nbi_fin_ar_report_by_aging, Me.nbi_fin_ar_report_by_top, Me.nbi_fin_ar_report_pending_invoice, Me.nbi_fin_bgt_periode, Me.nbi_fin_bgt_generate, Me.nbi_fin_bgt_maintenance, Me.nbi_fin_bgt_approval, Me.nbi_fin_bgt_cross, Me.nbi_fin_bgt_cross_approval, Me.nbi_fin_gl_cost_center_user, Me.nbi_fin_gl_cost_center_account, Me.nbi_fin_ar_cash_in_report, Me.nbi_fin_plSetting, Me.nbi_fin_bsSetting, Me.nbi_fin_cfSetting, Me.nbi_cash_transfer, Me.nbi_cash_in, Me.nbi_cash_out, Me.nbi_cash_bank_reconciliation, Me.nbi_fin_ar_dbcr_balance_report, Me.nbi_fin_ap_voucher_balance_report, Me.nbi_fin_ar_faktur_pajak_sign, Me.nbi_fin_ar_faktur_pajak_approval, Me.nbi_fin_ar_report_by_aging_sdi, Me.nbi_fin_add_account_glbal, Me.nbi_fin_ar_dc_memo_detail_report, Me.nbi_fin_payment_ar_report, Me.nbi_so_ar_fp_report})
        Me.nbc_financial.LargeImages = Me.ImageList5
        Me.nbc_financial.Location = New System.Drawing.Point(0, 0)
        Me.nbc_financial.Name = "nbc_financial"
        Me.nbc_financial.OptionsNavPane.ExpandedWidth = 194
        Me.nbc_financial.Size = New System.Drawing.Size(218, 503)
        Me.nbc_financial.SmallImages = Me.ImageList4
        Me.nbc_financial.TabIndex = 0
        Me.nbc_financial.Text = "NavBarControl1"
        '
        'nbg_taxation
        '
        Me.nbg_taxation.Caption = "Taxation"
        Me.nbg_taxation.Expanded = True
        Me.nbg_taxation.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_so_ar_fp_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_tax), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_faktur_pajak_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_faktur_pajak_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_faktur_pajak), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_faktur_pajak_sign), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_faktur_pajak_transaction_code)})
        Me.nbg_taxation.Name = "nbg_taxation"
        '
        'nbi_so_ar_fp_report
        '
        Me.nbi_so_ar_fp_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_so_ar_fp_report.Appearance.Options.UseFont = True
        Me.nbi_so_ar_fp_report.Caption = "SO Related With Shipment & Return & AR & FP Report"
        Me.nbi_so_ar_fp_report.Enabled = False
        Me.nbi_so_ar_fp_report.Hint = "SO Related With Shipment & Return & AR & FP Report"
        Me.nbi_so_ar_fp_report.Name = "nbi_so_ar_fp_report"
        Me.nbi_so_ar_fp_report.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_tax
        '
        Me.nbi_fin_ap_voucher_tax.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher_tax.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher_tax.Caption = "Voucher - Tax"
        Me.nbi_fin_ap_voucher_tax.Enabled = False
        Me.nbi_fin_ap_voucher_tax.Hint = "Voucher - Tax"
        Me.nbi_fin_ap_voucher_tax.Name = "nbi_fin_ap_voucher_tax"
        Me.nbi_fin_ap_voucher_tax.SmallImageIndex = 1
        '
        'nbi_fin_ar_faktur_pajak_print
        '
        Me.nbi_fin_ar_faktur_pajak_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_faktur_pajak_print.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_faktur_pajak_print.Caption = "Tax invoice Print"
        Me.nbi_fin_ar_faktur_pajak_print.Enabled = False
        Me.nbi_fin_ar_faktur_pajak_print.Hint = "Faktur Pajak Print"
        Me.nbi_fin_ar_faktur_pajak_print.Name = "nbi_fin_ar_faktur_pajak_print"
        Me.nbi_fin_ar_faktur_pajak_print.SmallImageIndex = 1
        '
        'nbi_fin_ar_faktur_pajak_approval
        '
        Me.nbi_fin_ar_faktur_pajak_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_faktur_pajak_approval.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_faktur_pajak_approval.Caption = "Tax invoice Approval"
        Me.nbi_fin_ar_faktur_pajak_approval.Enabled = False
        Me.nbi_fin_ar_faktur_pajak_approval.Hint = "Faktur Pajak Approval"
        Me.nbi_fin_ar_faktur_pajak_approval.Name = "nbi_fin_ar_faktur_pajak_approval"
        Me.nbi_fin_ar_faktur_pajak_approval.SmallImageIndex = 1
        '
        'nbi_fin_ar_faktur_pajak
        '
        Me.nbi_fin_ar_faktur_pajak.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_faktur_pajak.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_faktur_pajak.Caption = "Tax invoice"
        Me.nbi_fin_ar_faktur_pajak.Enabled = False
        Me.nbi_fin_ar_faktur_pajak.Hint = "Faktur Pajak"
        Me.nbi_fin_ar_faktur_pajak.Name = "nbi_fin_ar_faktur_pajak"
        Me.nbi_fin_ar_faktur_pajak.SmallImageIndex = 1
        '
        'nbi_fin_ar_faktur_pajak_sign
        '
        Me.nbi_fin_ar_faktur_pajak_sign.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_faktur_pajak_sign.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_faktur_pajak_sign.Caption = "Tax invoice Sign User"
        Me.nbi_fin_ar_faktur_pajak_sign.Enabled = False
        Me.nbi_fin_ar_faktur_pajak_sign.Hint = "Faktur Pajak Sign User"
        Me.nbi_fin_ar_faktur_pajak_sign.Name = "nbi_fin_ar_faktur_pajak_sign"
        Me.nbi_fin_ar_faktur_pajak_sign.SmallImageIndex = 1
        '
        'nbi_fin_ar_faktur_pajak_transaction_code
        '
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Caption = "Tax invoices Transaction Code"
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Enabled = False
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Hint = "Faktur Pajak Transaction Code"
        Me.nbi_fin_ar_faktur_pajak_transaction_code.Name = "nbi_fin_ar_faktur_pajak_transaction_code"
        Me.nbi_fin_ar_faktur_pajak_transaction_code.SmallImageIndex = 1
        '
        'nbg_ar
        '
        Me.nbg_ar.Caption = "Account Receiveable"
        Me.nbg_ar.Hint = "Account Receiveable"
        Me.nbg_ar.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_payment), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo_detail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo_detail_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_payment_detail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dc_memo_konsiyasi_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_report_by_aging), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_report_by_aging_sdi), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_report_by_top), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_report_pending_invoice), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_cash_in_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ar_dbcr_balance_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_payment_ar_report)})
        Me.nbg_ar.Name = "nbg_ar"
        '
        'nbi_fin_ar_type
        '
        Me.nbi_fin_ar_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_type.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_type.Caption = "AR Type"
        Me.nbi_fin_ar_type.Enabled = False
        Me.nbi_fin_ar_type.Hint = "AR Type"
        Me.nbi_fin_ar_type.Name = "nbi_fin_ar_type"
        Me.nbi_fin_ar_type.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo
        '
        Me.nbi_fin_ar_dc_memo.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo.Caption = "Debit / Credit Memo"
        Me.nbi_fin_ar_dc_memo.Enabled = False
        Me.nbi_fin_ar_dc_memo.Hint = "Debit / Credit Memo"
        Me.nbi_fin_ar_dc_memo.Name = "nbi_fin_ar_dc_memo"
        Me.nbi_fin_ar_dc_memo.SmallImageIndex = 1
        '
        'nbi_fin_ar_payment
        '
        Me.nbi_fin_ar_payment.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_payment.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_payment.Caption = "AR Payment"
        Me.nbi_fin_ar_payment.Enabled = False
        Me.nbi_fin_ar_payment.Hint = "AR Payment"
        Me.nbi_fin_ar_payment.Name = "nbi_fin_ar_payment"
        Me.nbi_fin_ar_payment.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo_detail
        '
        Me.nbi_fin_ar_dc_memo_detail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo_detail.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo_detail.Caption = "Debit / Credit Memo SDI"
        Me.nbi_fin_ar_dc_memo_detail.Enabled = False
        Me.nbi_fin_ar_dc_memo_detail.Hint = "Debit / Credit Memo SDI"
        Me.nbi_fin_ar_dc_memo_detail.Name = "nbi_fin_ar_dc_memo_detail"
        Me.nbi_fin_ar_dc_memo_detail.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo_report
        '
        Me.nbi_fin_ar_dc_memo_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo_report.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo_report.Caption = "Debit / Credit Memo Report"
        Me.nbi_fin_ar_dc_memo_report.Enabled = False
        Me.nbi_fin_ar_dc_memo_report.Hint = "Debit / Credit Memo Report"
        Me.nbi_fin_ar_dc_memo_report.Name = "nbi_fin_ar_dc_memo_report"
        Me.nbi_fin_ar_dc_memo_report.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo_detail_report
        '
        Me.nbi_fin_ar_dc_memo_detail_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo_detail_report.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo_detail_report.Caption = "Debit / Credit Memo Report SDI"
        Me.nbi_fin_ar_dc_memo_detail_report.Enabled = False
        Me.nbi_fin_ar_dc_memo_detail_report.Name = "nbi_fin_ar_dc_memo_detail_report"
        Me.nbi_fin_ar_dc_memo_detail_report.SmallImageIndex = 1
        '
        'nbi_fin_ar_payment_detail
        '
        Me.nbi_fin_ar_payment_detail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_payment_detail.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_payment_detail.Caption = "AR Payment SDI"
        Me.nbi_fin_ar_payment_detail.Enabled = False
        Me.nbi_fin_ar_payment_detail.Hint = "AR Payment SDI"
        Me.nbi_fin_ar_payment_detail.Name = "nbi_fin_ar_payment_detail"
        Me.nbi_fin_ar_payment_detail.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo_print_out
        '
        Me.nbi_fin_ar_dc_memo_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo_print_out.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo_print_out.Caption = "Invoice Print"
        Me.nbi_fin_ar_dc_memo_print_out.Enabled = False
        Me.nbi_fin_ar_dc_memo_print_out.Hint = "Invoice Print"
        Me.nbi_fin_ar_dc_memo_print_out.Name = "nbi_fin_ar_dc_memo_print_out"
        Me.nbi_fin_ar_dc_memo_print_out.SmallImageIndex = 1
        '
        'nbi_fin_ar_dc_memo_konsiyasi_print_out
        '
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Caption = "Invoice Konsiyasi Print"
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Enabled = False
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Hint = "Invoice Konsiyasi Print"
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.Name = "nbi_fin_ar_dc_memo_konsiyasi_print_out"
        Me.nbi_fin_ar_dc_memo_konsiyasi_print_out.SmallImageIndex = 1
        '
        'nbi_fin_ar_report_by_aging
        '
        Me.nbi_fin_ar_report_by_aging.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_report_by_aging.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_report_by_aging.Caption = "AR Report By Aging"
        Me.nbi_fin_ar_report_by_aging.Enabled = False
        Me.nbi_fin_ar_report_by_aging.Hint = "AR Report By Aging"
        Me.nbi_fin_ar_report_by_aging.Name = "nbi_fin_ar_report_by_aging"
        Me.nbi_fin_ar_report_by_aging.SmallImageIndex = 1
        '
        'nbi_fin_ar_report_by_aging_sdi
        '
        Me.nbi_fin_ar_report_by_aging_sdi.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_report_by_aging_sdi.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_report_by_aging_sdi.Caption = "AR Report By Aging SDI"
        Me.nbi_fin_ar_report_by_aging_sdi.Enabled = False
        Me.nbi_fin_ar_report_by_aging_sdi.Hint = "AR Report By Aging SDI"
        Me.nbi_fin_ar_report_by_aging_sdi.Name = "nbi_fin_ar_report_by_aging_sdi"
        Me.nbi_fin_ar_report_by_aging_sdi.SmallImageIndex = 1
        '
        'nbi_fin_ar_report_by_top
        '
        Me.nbi_fin_ar_report_by_top.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_report_by_top.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_report_by_top.Caption = "AR Report By TOP"
        Me.nbi_fin_ar_report_by_top.Enabled = False
        Me.nbi_fin_ar_report_by_top.Hint = "AR Report By TOP"
        Me.nbi_fin_ar_report_by_top.Name = "nbi_fin_ar_report_by_top"
        Me.nbi_fin_ar_report_by_top.SmallImageIndex = 1
        '
        'nbi_fin_ar_report_pending_invoice
        '
        Me.nbi_fin_ar_report_pending_invoice.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_report_pending_invoice.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_report_pending_invoice.Caption = "Pending Invoice"
        Me.nbi_fin_ar_report_pending_invoice.Enabled = False
        Me.nbi_fin_ar_report_pending_invoice.Hint = "Pending Invoice"
        Me.nbi_fin_ar_report_pending_invoice.Name = "nbi_fin_ar_report_pending_invoice"
        Me.nbi_fin_ar_report_pending_invoice.SmallImageIndex = 1
        '
        'nbi_fin_ar_cash_in_report
        '
        Me.nbi_fin_ar_cash_in_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_cash_in_report.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_cash_in_report.Caption = "Cash In Report"
        Me.nbi_fin_ar_cash_in_report.Enabled = False
        Me.nbi_fin_ar_cash_in_report.Hint = "Cash In Report"
        Me.nbi_fin_ar_cash_in_report.Name = "nbi_fin_ar_cash_in_report"
        Me.nbi_fin_ar_cash_in_report.SmallImageIndex = 1
        '
        'nbi_fin_ar_dbcr_balance_report
        '
        Me.nbi_fin_ar_dbcr_balance_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ar_dbcr_balance_report.Appearance.Options.UseFont = True
        Me.nbi_fin_ar_dbcr_balance_report.Caption = "Debit Credit Memo Balance Report"
        Me.nbi_fin_ar_dbcr_balance_report.Enabled = False
        Me.nbi_fin_ar_dbcr_balance_report.Name = "nbi_fin_ar_dbcr_balance_report"
        Me.nbi_fin_ar_dbcr_balance_report.SmallImageIndex = 1
        '
        'nbi_fin_payment_ar_report
        '
        Me.nbi_fin_payment_ar_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_payment_ar_report.Appearance.Options.UseFont = True
        Me.nbi_fin_payment_ar_report.Caption = "AR Payment Report"
        Me.nbi_fin_payment_ar_report.Enabled = False
        Me.nbi_fin_payment_ar_report.Name = "nbi_fin_payment_ar_report"
        Me.nbi_fin_payment_ar_report.SmallImageIndex = 1
        '
        'nbg_ap
        '
        Me.nbg_ap.Caption = "Account Payable"
        Me.nbg_ap.Hint = "Account Payable"
        Me.nbg_ap.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_payment), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_report_by_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_report_by_aging), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_report_by_top), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_report_by_unvouchered), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_ap_voucher_balance_report)})
        Me.nbg_ap.Name = "nbg_ap"
        '
        'nbi_fin_ap_type
        '
        Me.nbi_fin_ap_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_type.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_type.Caption = "Voucher Type"
        Me.nbi_fin_ap_type.Enabled = False
        Me.nbi_fin_ap_type.Hint = "Voucher Type"
        Me.nbi_fin_ap_type.Name = "nbi_fin_ap_type"
        Me.nbi_fin_ap_type.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher
        '
        Me.nbi_fin_ap_voucher.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher.Caption = "Voucher"
        Me.nbi_fin_ap_voucher.Enabled = False
        Me.nbi_fin_ap_voucher.Hint = "Voucher"
        Me.nbi_fin_ap_voucher.Name = "nbi_fin_ap_voucher"
        Me.nbi_fin_ap_voucher.SmallImageIndex = 1
        '
        'nbi_fin_ap_payment
        '
        Me.nbi_fin_ap_payment.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_payment.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_payment.Caption = "Payment Manual Checks"
        Me.nbi_fin_ap_payment.Enabled = False
        Me.nbi_fin_ap_payment.Hint = "Payment Manual Checks"
        Me.nbi_fin_ap_payment.Name = "nbi_fin_ap_payment"
        Me.nbi_fin_ap_payment.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_report_by_type
        '
        Me.nbi_fin_ap_voucher_report_by_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher_report_by_type.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher_report_by_type.Caption = "Voucher Report By Type"
        Me.nbi_fin_ap_voucher_report_by_type.Enabled = False
        Me.nbi_fin_ap_voucher_report_by_type.Hint = "Voucher Report By Type"
        Me.nbi_fin_ap_voucher_report_by_type.Name = "nbi_fin_ap_voucher_report_by_type"
        Me.nbi_fin_ap_voucher_report_by_type.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_report_by_aging
        '
        Me.nbi_fin_ap_voucher_report_by_aging.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher_report_by_aging.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher_report_by_aging.Caption = "Voucher Report By Aging"
        Me.nbi_fin_ap_voucher_report_by_aging.Enabled = False
        Me.nbi_fin_ap_voucher_report_by_aging.Hint = "Voucher Report By Aging"
        Me.nbi_fin_ap_voucher_report_by_aging.Name = "nbi_fin_ap_voucher_report_by_aging"
        Me.nbi_fin_ap_voucher_report_by_aging.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_report_by_top
        '
        Me.nbi_fin_ap_voucher_report_by_top.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher_report_by_top.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher_report_by_top.Caption = "Voucher Report By TOP"
        Me.nbi_fin_ap_voucher_report_by_top.Enabled = False
        Me.nbi_fin_ap_voucher_report_by_top.Hint = "Voucher Report By TOP"
        Me.nbi_fin_ap_voucher_report_by_top.Name = "nbi_fin_ap_voucher_report_by_top"
        Me.nbi_fin_ap_voucher_report_by_top.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_report_by_unvouchered
        '
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Appearance.Options.UseFont = True
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Caption = "Unvouchered PO Receipt"
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Enabled = False
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Hint = "Unvouchered PO Receipt"
        Me.nbi_fin_ap_voucher_report_by_unvouchered.Name = "nbi_fin_ap_voucher_report_by_unvouchered"
        Me.nbi_fin_ap_voucher_report_by_unvouchered.SmallImageIndex = 1
        '
        'nbi_fin_ap_voucher_balance_report
        '
        Me.nbi_fin_ap_voucher_balance_report.Caption = "Voucher Balance Report"
        Me.nbi_fin_ap_voucher_balance_report.Name = "nbi_fin_ap_voucher_balance_report"
        Me.nbi_fin_ap_voucher_balance_report.SmallImageIndex = 1
        '
        'nbg_gl
        '
        Me.nbg_gl.Caption = "General Ledger"
        Me.nbg_gl.Hint = "General Ledger"
        Me.nbg_gl.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_entity), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_entity_group), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_glcalendar), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_project), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_account), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_subaccount), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_cost_center), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_cost_center_user), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_cost_center_account), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_opening_balance), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_standard_transaction), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_unposted_transaction), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_transaction_post), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_month_end), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_year_end), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_forex), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_add_account_glbal)})
        Me.nbg_gl.Name = "nbg_gl"
        '
        'nbi_fin_gl_entity
        '
        Me.nbi_fin_gl_entity.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_entity.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_entity.Caption = "Entity"
        Me.nbi_fin_gl_entity.Enabled = False
        Me.nbi_fin_gl_entity.Hint = "Entity"
        Me.nbi_fin_gl_entity.Name = "nbi_fin_gl_entity"
        Me.nbi_fin_gl_entity.SmallImageIndex = 1
        '
        'nbi_fin_gl_entity_group
        '
        Me.nbi_fin_gl_entity_group.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_entity_group.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_entity_group.Caption = "Entity Group"
        Me.nbi_fin_gl_entity_group.Enabled = False
        Me.nbi_fin_gl_entity_group.Hint = "Entity Group"
        Me.nbi_fin_gl_entity_group.Name = "nbi_fin_gl_entity_group"
        Me.nbi_fin_gl_entity_group.SmallImageIndex = 1
        '
        'nbi_fin_gl_glcalendar
        '
        Me.nbi_fin_gl_glcalendar.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_glcalendar.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_glcalendar.Caption = "GL Calendar"
        Me.nbi_fin_gl_glcalendar.Enabled = False
        Me.nbi_fin_gl_glcalendar.Hint = "GL Calendar"
        Me.nbi_fin_gl_glcalendar.Name = "nbi_fin_gl_glcalendar"
        Me.nbi_fin_gl_glcalendar.SmallImageIndex = 1
        '
        'nbi_fin_gl_project
        '
        Me.nbi_fin_gl_project.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_project.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_project.Caption = "Project"
        Me.nbi_fin_gl_project.Enabled = False
        Me.nbi_fin_gl_project.Hint = "Project"
        Me.nbi_fin_gl_project.Name = "nbi_fin_gl_project"
        Me.nbi_fin_gl_project.SmallImageIndex = 1
        '
        'nbi_fin_gl_account
        '
        Me.nbi_fin_gl_account.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_account.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_account.Caption = "Account"
        Me.nbi_fin_gl_account.Enabled = False
        Me.nbi_fin_gl_account.Hint = "Account"
        Me.nbi_fin_gl_account.Name = "nbi_fin_gl_account"
        Me.nbi_fin_gl_account.SmallImageIndex = 1
        '
        'nbi_fin_gl_subaccount
        '
        Me.nbi_fin_gl_subaccount.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_subaccount.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_subaccount.Caption = "Sub Account"
        Me.nbi_fin_gl_subaccount.Enabled = False
        Me.nbi_fin_gl_subaccount.Hint = "Sub Account"
        Me.nbi_fin_gl_subaccount.Name = "nbi_fin_gl_subaccount"
        Me.nbi_fin_gl_subaccount.SmallImageIndex = 1
        '
        'nbi_fin_gl_cost_center
        '
        Me.nbi_fin_gl_cost_center.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_cost_center.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_cost_center.Caption = "Cost Center"
        Me.nbi_fin_gl_cost_center.Enabled = False
        Me.nbi_fin_gl_cost_center.Hint = "Cost Center"
        Me.nbi_fin_gl_cost_center.Name = "nbi_fin_gl_cost_center"
        Me.nbi_fin_gl_cost_center.SmallImageIndex = 1
        '
        'nbi_fin_gl_cost_center_user
        '
        Me.nbi_fin_gl_cost_center_user.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_cost_center_user.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_cost_center_user.Caption = "Cost Center User"
        Me.nbi_fin_gl_cost_center_user.Enabled = False
        Me.nbi_fin_gl_cost_center_user.Hint = "Cost Center User"
        Me.nbi_fin_gl_cost_center_user.Name = "nbi_fin_gl_cost_center_user"
        Me.nbi_fin_gl_cost_center_user.SmallImageIndex = 1
        '
        'nbi_fin_gl_cost_center_account
        '
        Me.nbi_fin_gl_cost_center_account.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_cost_center_account.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_cost_center_account.Caption = "Cost Center Account"
        Me.nbi_fin_gl_cost_center_account.Enabled = False
        Me.nbi_fin_gl_cost_center_account.Hint = "Cost Center Account"
        Me.nbi_fin_gl_cost_center_account.Name = "nbi_fin_gl_cost_center_account"
        Me.nbi_fin_gl_cost_center_account.SmallImageIndex = 1
        '
        'nbi_fin_gl_opening_balance
        '
        Me.nbi_fin_gl_opening_balance.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_opening_balance.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_opening_balance.Caption = "Opening Balance"
        Me.nbi_fin_gl_opening_balance.Enabled = False
        Me.nbi_fin_gl_opening_balance.Hint = "Opening Balance"
        Me.nbi_fin_gl_opening_balance.Name = "nbi_fin_gl_opening_balance"
        Me.nbi_fin_gl_opening_balance.SmallImageIndex = 1
        '
        'nbi_fin_gl_standard_transaction
        '
        Me.nbi_fin_gl_standard_transaction.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_standard_transaction.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_standard_transaction.Caption = "Standard Transaction"
        Me.nbi_fin_gl_standard_transaction.Enabled = False
        Me.nbi_fin_gl_standard_transaction.Hint = "Standard Transaction"
        Me.nbi_fin_gl_standard_transaction.Name = "nbi_fin_gl_standard_transaction"
        Me.nbi_fin_gl_standard_transaction.SmallImageIndex = 1
        '
        'nbi_fin_gl_unposted_transaction
        '
        Me.nbi_fin_gl_unposted_transaction.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_unposted_transaction.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_unposted_transaction.Caption = "Unposted Transaction"
        Me.nbi_fin_gl_unposted_transaction.Enabled = False
        Me.nbi_fin_gl_unposted_transaction.Hint = "Unposted Transaction"
        Me.nbi_fin_gl_unposted_transaction.Name = "nbi_fin_gl_unposted_transaction"
        Me.nbi_fin_gl_unposted_transaction.SmallImageIndex = 1
        '
        'nbi_fin_gl_transaction_post
        '
        Me.nbi_fin_gl_transaction_post.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_transaction_post.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_transaction_post.Caption = "Transaction Post"
        Me.nbi_fin_gl_transaction_post.Enabled = False
        Me.nbi_fin_gl_transaction_post.Hint = "Transaction Post"
        Me.nbi_fin_gl_transaction_post.Name = "nbi_fin_gl_transaction_post"
        Me.nbi_fin_gl_transaction_post.SmallImageIndex = 1
        '
        'nbi_fin_gl_month_end
        '
        Me.nbi_fin_gl_month_end.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_month_end.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_month_end.Caption = "Month End Adjustment Transaction"
        Me.nbi_fin_gl_month_end.Enabled = False
        Me.nbi_fin_gl_month_end.Hint = "Month End Adjustment Transaction"
        Me.nbi_fin_gl_month_end.Name = "nbi_fin_gl_month_end"
        Me.nbi_fin_gl_month_end.SmallImageIndex = 1
        '
        'nbi_fin_gl_year_end
        '
        Me.nbi_fin_gl_year_end.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_year_end.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_year_end.Caption = "Year End Adjustment Transaction"
        Me.nbi_fin_gl_year_end.Enabled = False
        Me.nbi_fin_gl_year_end.Hint = "Year End Adjustment Transaction"
        Me.nbi_fin_gl_year_end.Name = "nbi_fin_gl_year_end"
        Me.nbi_fin_gl_year_end.SmallImageIndex = 1
        '
        'nbi_fin_gl_forex
        '
        Me.nbi_fin_gl_forex.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_forex.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_forex.Caption = "Foreign Exchange Revaluation"
        Me.nbi_fin_gl_forex.Enabled = False
        Me.nbi_fin_gl_forex.Hint = "Foreign Exchange Revaluation"
        Me.nbi_fin_gl_forex.Name = "nbi_fin_gl_forex"
        Me.nbi_fin_gl_forex.SmallImageIndex = 1
        '
        'nbi_fin_add_account_glbal
        '
        Me.nbi_fin_add_account_glbal.Caption = "Add Account to GL Balance"
        Me.nbi_fin_add_account_glbal.Name = "nbi_fin_add_account_glbal"
        Me.nbi_fin_add_account_glbal.SmallImageIndex = 1
        '
        'nbg_multiple_currency
        '
        Me.nbg_multiple_currency.Caption = "Multiple Currency"
        Me.nbg_multiple_currency.Hint = "Multiple Currency"
        Me.nbg_multiple_currency.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_mc_currency), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_mc_exchange_rate), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_mc_bank)})
        Me.nbg_multiple_currency.Name = "nbg_multiple_currency"
        '
        'nbi_fin_mc_currency
        '
        Me.nbi_fin_mc_currency.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_mc_currency.Appearance.Options.UseFont = True
        Me.nbi_fin_mc_currency.Caption = "Currency"
        Me.nbi_fin_mc_currency.Enabled = False
        Me.nbi_fin_mc_currency.Hint = "Currency"
        Me.nbi_fin_mc_currency.Name = "nbi_fin_mc_currency"
        Me.nbi_fin_mc_currency.SmallImageIndex = 1
        '
        'nbi_fin_mc_exchange_rate
        '
        Me.nbi_fin_mc_exchange_rate.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_mc_exchange_rate.Appearance.Options.UseFont = True
        Me.nbi_fin_mc_exchange_rate.Caption = "Exchange Rate"
        Me.nbi_fin_mc_exchange_rate.Enabled = False
        Me.nbi_fin_mc_exchange_rate.Hint = "Exchange Rate"
        Me.nbi_fin_mc_exchange_rate.Name = "nbi_fin_mc_exchange_rate"
        Me.nbi_fin_mc_exchange_rate.SmallImageIndex = 1
        '
        'nbi_fin_mc_bank
        '
        Me.nbi_fin_mc_bank.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_mc_bank.Appearance.Options.UseFont = True
        Me.nbi_fin_mc_bank.Caption = "Bank"
        Me.nbi_fin_mc_bank.Enabled = False
        Me.nbi_fin_mc_bank.Hint = "Bank"
        Me.nbi_fin_mc_bank.Name = "nbi_fin_mc_bank"
        Me.nbi_fin_mc_bank.SmallImageIndex = 1
        '
        'nbg_fin_gl_report
        '
        Me.nbg_fin_gl_report.Caption = "General Ledger Report"
        Me.nbg_fin_gl_report.Hint = "General Ledger Report"
        Me.nbg_fin_gl_report.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_general_ledger_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_plSetting), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_profit_loss_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_cfSetting), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_cash_flow), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_trial_balance_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bsSetting), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_gl_balance_sheet)})
        Me.nbg_fin_gl_report.Name = "nbg_fin_gl_report"
        '
        'nbi_fin_gl_general_ledger_report
        '
        Me.nbi_fin_gl_general_ledger_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_general_ledger_report.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_general_ledger_report.Caption = "General Ledger Report"
        Me.nbi_fin_gl_general_ledger_report.Enabled = False
        Me.nbi_fin_gl_general_ledger_report.Hint = "General Ledger Report"
        Me.nbi_fin_gl_general_ledger_report.Name = "nbi_fin_gl_general_ledger_report"
        Me.nbi_fin_gl_general_ledger_report.SmallImageIndex = 1
        '
        'nbi_fin_plSetting
        '
        Me.nbi_fin_plSetting.Caption = "Profit Loss Setting"
        Me.nbi_fin_plSetting.Enabled = False
        Me.nbi_fin_plSetting.Name = "nbi_fin_plSetting"
        Me.nbi_fin_plSetting.SmallImageIndex = 1
        '
        'nbi_fin_gl_profit_loss_report
        '
        Me.nbi_fin_gl_profit_loss_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_profit_loss_report.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_profit_loss_report.Caption = "Profit & Loss Report"
        Me.nbi_fin_gl_profit_loss_report.Enabled = False
        Me.nbi_fin_gl_profit_loss_report.Hint = "Profit & Loss Report"
        Me.nbi_fin_gl_profit_loss_report.Name = "nbi_fin_gl_profit_loss_report"
        Me.nbi_fin_gl_profit_loss_report.SmallImageIndex = 1
        '
        'nbi_fin_cfSetting
        '
        Me.nbi_fin_cfSetting.Caption = "Cash Flow Setting"
        Me.nbi_fin_cfSetting.Enabled = False
        Me.nbi_fin_cfSetting.Name = "nbi_fin_cfSetting"
        Me.nbi_fin_cfSetting.SmallImageIndex = 1
        '
        'nbi_fin_gl_cash_flow
        '
        Me.nbi_fin_gl_cash_flow.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_cash_flow.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_cash_flow.Caption = "Cash Flow Report"
        Me.nbi_fin_gl_cash_flow.Enabled = False
        Me.nbi_fin_gl_cash_flow.Hint = "Cash Flow"
        Me.nbi_fin_gl_cash_flow.Name = "nbi_fin_gl_cash_flow"
        Me.nbi_fin_gl_cash_flow.SmallImageIndex = 1
        '
        'nbi_fin_gl_trial_balance_report
        '
        Me.nbi_fin_gl_trial_balance_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_trial_balance_report.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_trial_balance_report.Caption = "Trial Balance Report"
        Me.nbi_fin_gl_trial_balance_report.Enabled = False
        Me.nbi_fin_gl_trial_balance_report.Hint = "Trial Balance Report"
        Me.nbi_fin_gl_trial_balance_report.Name = "nbi_fin_gl_trial_balance_report"
        Me.nbi_fin_gl_trial_balance_report.SmallImageIndex = 1
        '
        'nbi_fin_bsSetting
        '
        Me.nbi_fin_bsSetting.Caption = "Balance Sheet Setting"
        Me.nbi_fin_bsSetting.Enabled = False
        Me.nbi_fin_bsSetting.Name = "nbi_fin_bsSetting"
        Me.nbi_fin_bsSetting.SmallImageIndex = 1
        '
        'nbi_fin_gl_balance_sheet
        '
        Me.nbi_fin_gl_balance_sheet.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_gl_balance_sheet.Appearance.Options.UseFont = True
        Me.nbi_fin_gl_balance_sheet.Caption = "Balance Sheet Report"
        Me.nbi_fin_gl_balance_sheet.Enabled = False
        Me.nbi_fin_gl_balance_sheet.Hint = "Balance Sheet Report"
        Me.nbi_fin_gl_balance_sheet.Name = "nbi_fin_gl_balance_sheet"
        Me.nbi_fin_gl_balance_sheet.SmallImageIndex = 1
        '
        'nbg_budget
        '
        Me.nbg_budget.Caption = "Budget"
        Me.nbg_budget.Hint = "Budget"
        Me.nbg_budget.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_periode), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_generate), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_maintenance), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_cross), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_fin_bgt_cross_approval)})
        Me.nbg_budget.Name = "nbg_budget"
        '
        'nbi_fin_bgt_periode
        '
        Me.nbi_fin_bgt_periode.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_periode.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_periode.Caption = "Budget Periode"
        Me.nbi_fin_bgt_periode.Enabled = False
        Me.nbi_fin_bgt_periode.Hint = "Budget Periode"
        Me.nbi_fin_bgt_periode.Name = "nbi_fin_bgt_periode"
        Me.nbi_fin_bgt_periode.SmallImageIndex = 1
        '
        'nbi_fin_bgt_generate
        '
        Me.nbi_fin_bgt_generate.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_generate.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_generate.Caption = "Budget Generate"
        Me.nbi_fin_bgt_generate.Enabled = False
        Me.nbi_fin_bgt_generate.Hint = "Budget Generate"
        Me.nbi_fin_bgt_generate.Name = "nbi_fin_bgt_generate"
        Me.nbi_fin_bgt_generate.SmallImageIndex = 1
        '
        'nbi_fin_bgt_maintenance
        '
        Me.nbi_fin_bgt_maintenance.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_maintenance.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_maintenance.Caption = "Budget Detail"
        Me.nbi_fin_bgt_maintenance.Enabled = False
        Me.nbi_fin_bgt_maintenance.Hint = "Budget Detail"
        Me.nbi_fin_bgt_maintenance.Name = "nbi_fin_bgt_maintenance"
        Me.nbi_fin_bgt_maintenance.SmallImageIndex = 1
        '
        'nbi_fin_bgt_approval
        '
        Me.nbi_fin_bgt_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_approval.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_approval.Caption = "Budget Approval"
        Me.nbi_fin_bgt_approval.Enabled = False
        Me.nbi_fin_bgt_approval.Hint = "Budget Approval"
        Me.nbi_fin_bgt_approval.Name = "nbi_fin_bgt_approval"
        Me.nbi_fin_bgt_approval.SmallImageIndex = 1
        '
        'nbi_fin_bgt_cross
        '
        Me.nbi_fin_bgt_cross.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_cross.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_cross.Caption = "Budget Cross"
        Me.nbi_fin_bgt_cross.Enabled = False
        Me.nbi_fin_bgt_cross.Hint = "Budget Cross"
        Me.nbi_fin_bgt_cross.Name = "nbi_fin_bgt_cross"
        Me.nbi_fin_bgt_cross.SmallImageIndex = 1
        '
        'nbi_fin_bgt_cross_approval
        '
        Me.nbi_fin_bgt_cross_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_fin_bgt_cross_approval.Appearance.Options.UseFont = True
        Me.nbi_fin_bgt_cross_approval.Caption = "Budget Cross Approval"
        Me.nbi_fin_bgt_cross_approval.Enabled = False
        Me.nbi_fin_bgt_cross_approval.Hint = "Budget Cross Approval"
        Me.nbi_fin_bgt_cross_approval.Name = "nbi_fin_bgt_cross_approval"
        Me.nbi_fin_bgt_cross_approval.SmallImageIndex = 1
        '
        'nbg_cash_management
        '
        Me.nbg_cash_management.Caption = "Cash Management"
        Me.nbg_cash_management.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_cash_transfer), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_cash_in), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_cash_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_cash_bank_reconciliation)})
        Me.nbg_cash_management.Name = "nbg_cash_management"
        '
        'nbi_cash_transfer
        '
        Me.nbi_cash_transfer.Caption = "Transfer Bank"
        Me.nbi_cash_transfer.Name = "nbi_cash_transfer"
        Me.nbi_cash_transfer.SmallImageIndex = 1
        '
        'nbi_cash_in
        '
        Me.nbi_cash_in.Caption = "Cash In"
        Me.nbi_cash_in.Name = "nbi_cash_in"
        Me.nbi_cash_in.SmallImageIndex = 1
        '
        'nbi_cash_out
        '
        Me.nbi_cash_out.Caption = "Cash Out"
        Me.nbi_cash_out.Name = "nbi_cash_out"
        Me.nbi_cash_out.SmallImageIndex = 1
        '
        'nbi_cash_bank_reconciliation
        '
        Me.nbi_cash_bank_reconciliation.Caption = "Bank Reconciliation"
        Me.nbi_cash_bank_reconciliation.Name = "nbi_cash_bank_reconciliation"
        Me.nbi_cash_bank_reconciliation.SmallImageIndex = 1
        '
        'ImageList5
        '
        Me.ImageList5.ImageStream = CType(resources.GetObject("ImageList5.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList5.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList5.Images.SetKeyName(0, "home.gif")
        '
        'ImageList4
        '
        Me.ImageList4.ImageStream = CType(resources.GetObject("ImageList4.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList4.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList4.Images.SetKeyName(0, "locker.bmp")
        Me.ImageList4.Images.SetKeyName(1, "001_32c.png")
        Me.ImageList4.Images.SetKeyName(2, "001_32b.png")
        Me.ImageList4.Images.SetKeyName(3, "control_repeat_blue.png")
        Me.ImageList4.Images.SetKeyName(4, "001_32.png")
        Me.ImageList4.Images.SetKeyName(5, "001_09.png")
        Me.ImageList4.Images.SetKeyName(6, "001_52.png")
        Me.ImageList4.Images.SetKeyName(7, "go-lv.jpg")
        '
        'dp_master_data
        '
        Me.dp_master_data.Controls.Add(Me.DockPanel1_Container)
        Me.dp_master_data.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.dp_master_data.FloatSize = New System.Drawing.Size(200, 314)
        Me.dp_master_data.FloatVertical = True
        Me.dp_master_data.ID = New System.Guid("4e5d5631-142d-4c22-9af4-53c94426e32a")
        Me.dp_master_data.Location = New System.Drawing.Point(3, 25)
        Me.dp_master_data.Name = "dp_master_data"
        Me.dp_master_data.OriginalSize = New System.Drawing.Size(218, 485)
        Me.dp_master_data.Size = New System.Drawing.Size(218, 503)
        Me.dp_master_data.Text = "Master Data"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.nbc_master_data)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(218, 503)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'nbc_master_data
        '
        Me.nbc_master_data.ActiveGroup = Me.nbg_master_data_company
        Me.nbc_master_data.ContentButtonHint = Nothing
        Me.nbc_master_data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nbc_master_data.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nbg_master_data_company, Me.nbg_item_site, Me.nbg_address_taxes})
        Me.nbc_master_data.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nbi_master_data_domain, Me.nbi_master_data_agama, Me.nbi_mst_org_type, Me.nbi_mst_organization, Me.nbi_mst_organization_structure, Me.nbi_mst_org_struc_detail, Me.nbi_trn_req, Me.nbi_mst_organizationtree, Me.nbi_mst_at_partner, Me.nbi_mst_is_partnumber, Me.nbi_mst_is_inventorystatus, Me.nbi_mst_is_itemstatus, Me.nbi_mst_is_location, Me.nbi_mst_is_productline, Me.nbi_mst_is_unitmeasure, Me.nbi_mst_is_location_type, Me.nbi_mst_is_location_category, Me.nbi_mst_is_warehouse, Me.nbi_mst_is_warehouse_type, Me.nbi_mst_is_warehouse_category, Me.nbi_mst_is_part_number_group, Me.nbi_mst_at_taxclass, Me.nbi_mst_is_site, Me.nbi_mst_at_partner_group, Me.nbi_mst_comp_company_address, Me.nbi_mst_comp_employee, Me.nbi_mst_comp_tran_status, Me.nbi_mst_emp_transaction, Me.nbi_mst_at_partner_bank, Me.nbi_mst_at_partner_address, Me.nbi_mst_at_partner_cp, Me.nbi_mst_at_address_type, Me.nbi_mst_at_cp_function, Me.nbi_mst_fin_credit_terms, Me.nbi_mst_at_tax_type, Me.nbi_mst_at_tax_rate, Me.nbi_mst_prodline_location, Me.nbi_company_position, Me.nbi_mst_is_unit_conversion, Me.nbi_mst_at_partner_all, Me.nbi_company_dok_aprv, Me.nbi_company_conf, Me.nbi_company_routing_approval, Me.nbi_mst_is_item_site_cost, Me.nbi_mst_is_approval_tax, Me.nbi_mst_is_approval_accounting})
        Me.nbc_master_data.LargeImages = Me.ImageList5
        Me.nbc_master_data.Location = New System.Drawing.Point(0, 0)
        Me.nbc_master_data.Name = "nbc_master_data"
        Me.nbc_master_data.OptionsNavPane.ExpandedWidth = 194
        Me.nbc_master_data.Size = New System.Drawing.Size(218, 503)
        Me.nbc_master_data.SmallImages = Me.ImageList4
        Me.nbc_master_data.TabIndex = 0
        Me.nbc_master_data.Text = "NavBarControl1"
        '
        'nbg_master_data_company
        '
        Me.nbg_master_data_company.Caption = "Company"
        Me.nbg_master_data_company.Expanded = True
        Me.nbg_master_data_company.Hint = "Company"
        Me.nbg_master_data_company.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_master_data_domain), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_comp_company_address), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_comp_employee), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_master_data_agama), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_org_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_organization), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_organization_structure), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_org_struc_detail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_organizationtree), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_comp_tran_status), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_emp_transaction), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_company_position), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_company_dok_aprv), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_company_routing_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_company_conf)})
        Me.nbg_master_data_company.Name = "nbg_master_data_company"
        '
        'nbi_master_data_domain
        '
        Me.nbi_master_data_domain.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_master_data_domain.Appearance.Options.UseFont = True
        Me.nbi_master_data_domain.Caption = "Domain"
        Me.nbi_master_data_domain.Enabled = False
        Me.nbi_master_data_domain.Hint = "Domain"
        Me.nbi_master_data_domain.Name = "nbi_master_data_domain"
        Me.nbi_master_data_domain.SmallImageIndex = 1
        '
        'nbi_mst_comp_company_address
        '
        Me.nbi_mst_comp_company_address.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_comp_company_address.Appearance.Options.UseFont = True
        Me.nbi_mst_comp_company_address.Caption = "Company Address"
        Me.nbi_mst_comp_company_address.Enabled = False
        Me.nbi_mst_comp_company_address.Hint = "Company Address"
        Me.nbi_mst_comp_company_address.Name = "nbi_mst_comp_company_address"
        Me.nbi_mst_comp_company_address.SmallImageIndex = 1
        '
        'nbi_mst_comp_employee
        '
        Me.nbi_mst_comp_employee.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_comp_employee.Appearance.Options.UseFont = True
        Me.nbi_mst_comp_employee.Caption = "Employee"
        Me.nbi_mst_comp_employee.Enabled = False
        Me.nbi_mst_comp_employee.Hint = "Employee"
        Me.nbi_mst_comp_employee.Name = "nbi_mst_comp_employee"
        Me.nbi_mst_comp_employee.SmallImageIndex = 1
        '
        'nbi_master_data_agama
        '
        Me.nbi_master_data_agama.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_master_data_agama.Appearance.Options.UseFont = True
        Me.nbi_master_data_agama.Caption = "Agama"
        Me.nbi_master_data_agama.Enabled = False
        Me.nbi_master_data_agama.Hint = "Agama"
        Me.nbi_master_data_agama.Name = "nbi_master_data_agama"
        Me.nbi_master_data_agama.SmallImageIndex = 1
        '
        'nbi_mst_org_type
        '
        Me.nbi_mst_org_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_org_type.Appearance.Options.UseFont = True
        Me.nbi_mst_org_type.Caption = "Organization Type"
        Me.nbi_mst_org_type.Enabled = False
        Me.nbi_mst_org_type.Hint = "Organization Type"
        Me.nbi_mst_org_type.Name = "nbi_mst_org_type"
        Me.nbi_mst_org_type.SmallImageIndex = 1
        '
        'nbi_mst_organization
        '
        Me.nbi_mst_organization.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_organization.Appearance.Options.UseFont = True
        Me.nbi_mst_organization.Caption = "Organization"
        Me.nbi_mst_organization.Enabled = False
        Me.nbi_mst_organization.Hint = "Organization"
        Me.nbi_mst_organization.Name = "nbi_mst_organization"
        Me.nbi_mst_organization.SmallImageIndex = 1
        '
        'nbi_mst_organization_structure
        '
        Me.nbi_mst_organization_structure.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_organization_structure.Appearance.Options.UseFont = True
        Me.nbi_mst_organization_structure.Caption = "Organization Structure"
        Me.nbi_mst_organization_structure.Enabled = False
        Me.nbi_mst_organization_structure.Hint = "Organization Structure"
        Me.nbi_mst_organization_structure.Name = "nbi_mst_organization_structure"
        Me.nbi_mst_organization_structure.SmallImageIndex = 1
        '
        'nbi_mst_org_struc_detail
        '
        Me.nbi_mst_org_struc_detail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_org_struc_detail.Appearance.Options.UseFont = True
        Me.nbi_mst_org_struc_detail.Caption = "Organization Struc. Detail"
        Me.nbi_mst_org_struc_detail.Enabled = False
        Me.nbi_mst_org_struc_detail.Hint = "Organization Struc. Detail"
        Me.nbi_mst_org_struc_detail.Name = "nbi_mst_org_struc_detail"
        Me.nbi_mst_org_struc_detail.SmallImageIndex = 1
        '
        'nbi_mst_organizationtree
        '
        Me.nbi_mst_organizationtree.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_organizationtree.Appearance.Options.UseFont = True
        Me.nbi_mst_organizationtree.Caption = "Organization Tree"
        Me.nbi_mst_organizationtree.Enabled = False
        Me.nbi_mst_organizationtree.Hint = "Organization Tree"
        Me.nbi_mst_organizationtree.Name = "nbi_mst_organizationtree"
        Me.nbi_mst_organizationtree.SmallImageIndex = 1
        '
        'nbi_mst_comp_tran_status
        '
        Me.nbi_mst_comp_tran_status.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_comp_tran_status.Appearance.Options.UseFont = True
        Me.nbi_mst_comp_tran_status.Caption = "Transaction Status"
        Me.nbi_mst_comp_tran_status.Enabled = False
        Me.nbi_mst_comp_tran_status.Hint = "Transaction Status"
        Me.nbi_mst_comp_tran_status.Name = "nbi_mst_comp_tran_status"
        Me.nbi_mst_comp_tran_status.SmallImageIndex = 1
        '
        'nbi_mst_emp_transaction
        '
        Me.nbi_mst_emp_transaction.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_emp_transaction.Appearance.Options.UseFont = True
        Me.nbi_mst_emp_transaction.Caption = "Transaction"
        Me.nbi_mst_emp_transaction.Enabled = False
        Me.nbi_mst_emp_transaction.Hint = "Transaction"
        Me.nbi_mst_emp_transaction.Name = "nbi_mst_emp_transaction"
        Me.nbi_mst_emp_transaction.SmallImageIndex = 1
        '
        'nbi_company_position
        '
        Me.nbi_company_position.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_company_position.Appearance.Options.UseFont = True
        Me.nbi_company_position.Caption = "Position"
        Me.nbi_company_position.Enabled = False
        Me.nbi_company_position.Hint = "Position"
        Me.nbi_company_position.Name = "nbi_company_position"
        Me.nbi_company_position.SmallImageIndex = 1
        '
        'nbi_company_dok_aprv
        '
        Me.nbi_company_dok_aprv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_company_dok_aprv.Appearance.Options.UseFont = True
        Me.nbi_company_dok_aprv.Caption = "Document Approval"
        Me.nbi_company_dok_aprv.Enabled = False
        Me.nbi_company_dok_aprv.Hint = "Document Approval"
        Me.nbi_company_dok_aprv.Name = "nbi_company_dok_aprv"
        Me.nbi_company_dok_aprv.SmallImageIndex = 1
        '
        'nbi_company_routing_approval
        '
        Me.nbi_company_routing_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_company_routing_approval.Appearance.Options.UseFont = True
        Me.nbi_company_routing_approval.Caption = "Routing Approval"
        Me.nbi_company_routing_approval.Enabled = False
        Me.nbi_company_routing_approval.Hint = "Routing Approval"
        Me.nbi_company_routing_approval.Name = "nbi_company_routing_approval"
        Me.nbi_company_routing_approval.SmallImageIndex = 1
        '
        'nbi_company_conf
        '
        Me.nbi_company_conf.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_company_conf.Appearance.Options.UseFont = True
        Me.nbi_company_conf.Caption = "Control File"
        Me.nbi_company_conf.Enabled = False
        Me.nbi_company_conf.Hint = "Control File"
        Me.nbi_company_conf.Name = "nbi_company_conf"
        Me.nbi_company_conf.SmallImageIndex = 1
        '
        'nbg_item_site
        '
        Me.nbg_item_site.Caption = "Items / Sites"
        Me.nbg_item_site.Expanded = True
        Me.nbg_item_site.Hint = "Items / Sites"
        Me.nbg_item_site.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_partnumber), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_approval_tax), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_approval_accounting), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_part_number_group), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_inventorystatus), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_site), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_itemstatus), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_item_site_cost), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_warehouse), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_warehouse_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_warehouse_category), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_location), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_location_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_location_category), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_productline), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_prodline_location), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_unitmeasure), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_is_unit_conversion)})
        Me.nbg_item_site.Name = "nbg_item_site"
        '
        'nbi_mst_is_partnumber
        '
        Me.nbi_mst_is_partnumber.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_partnumber.Appearance.Options.UseFont = True
        Me.nbi_mst_is_partnumber.Caption = "Part Number"
        Me.nbi_mst_is_partnumber.Enabled = False
        Me.nbi_mst_is_partnumber.Hint = "Part Number"
        Me.nbi_mst_is_partnumber.Name = "nbi_mst_is_partnumber"
        Me.nbi_mst_is_partnumber.SmallImageIndex = 1
        '
        'nbi_mst_is_approval_tax
        '
        Me.nbi_mst_is_approval_tax.Caption = "Part Number Approval Tax"
        Me.nbi_mst_is_approval_tax.Name = "nbi_mst_is_approval_tax"
        Me.nbi_mst_is_approval_tax.SmallImageIndex = 1
        '
        'nbi_mst_is_approval_accounting
        '
        Me.nbi_mst_is_approval_accounting.Caption = "Part Number Aproval Accounting"
        Me.nbi_mst_is_approval_accounting.Name = "nbi_mst_is_approval_accounting"
        Me.nbi_mst_is_approval_accounting.SmallImageIndex = 1
        '
        'nbi_mst_is_part_number_group
        '
        Me.nbi_mst_is_part_number_group.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_part_number_group.Appearance.Options.UseFont = True
        Me.nbi_mst_is_part_number_group.Caption = "Part Number Group"
        Me.nbi_mst_is_part_number_group.Enabled = False
        Me.nbi_mst_is_part_number_group.Hint = "Part Number Group"
        Me.nbi_mst_is_part_number_group.Name = "nbi_mst_is_part_number_group"
        Me.nbi_mst_is_part_number_group.SmallImageIndex = 1
        '
        'nbi_mst_is_inventorystatus
        '
        Me.nbi_mst_is_inventorystatus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_inventorystatus.Appearance.Options.UseFont = True
        Me.nbi_mst_is_inventorystatus.Caption = "Inventory Status"
        Me.nbi_mst_is_inventorystatus.Enabled = False
        Me.nbi_mst_is_inventorystatus.Hint = "Inventory Status"
        Me.nbi_mst_is_inventorystatus.Name = "nbi_mst_is_inventorystatus"
        Me.nbi_mst_is_inventorystatus.SmallImageIndex = 1
        '
        'nbi_mst_is_site
        '
        Me.nbi_mst_is_site.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_site.Appearance.Options.UseFont = True
        Me.nbi_mst_is_site.Caption = "Site"
        Me.nbi_mst_is_site.Enabled = False
        Me.nbi_mst_is_site.Hint = "Site"
        Me.nbi_mst_is_site.Name = "nbi_mst_is_site"
        Me.nbi_mst_is_site.SmallImageIndex = 1
        '
        'nbi_mst_is_itemstatus
        '
        Me.nbi_mst_is_itemstatus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_itemstatus.Appearance.Options.UseFont = True
        Me.nbi_mst_is_itemstatus.Caption = "Item Status"
        Me.nbi_mst_is_itemstatus.Enabled = False
        Me.nbi_mst_is_itemstatus.Hint = "Item Status"
        Me.nbi_mst_is_itemstatus.Name = "nbi_mst_is_itemstatus"
        Me.nbi_mst_is_itemstatus.SmallImageIndex = 1
        '
        'nbi_mst_is_item_site_cost
        '
        Me.nbi_mst_is_item_site_cost.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_item_site_cost.Appearance.Options.UseFont = True
        Me.nbi_mst_is_item_site_cost.Caption = "Item Site Cost"
        Me.nbi_mst_is_item_site_cost.Enabled = False
        Me.nbi_mst_is_item_site_cost.Hint = "Item Site Cost"
        Me.nbi_mst_is_item_site_cost.Name = "nbi_mst_is_item_site_cost"
        Me.nbi_mst_is_item_site_cost.SmallImageIndex = 1
        '
        'nbi_mst_is_warehouse
        '
        Me.nbi_mst_is_warehouse.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_warehouse.Appearance.Options.UseFont = True
        Me.nbi_mst_is_warehouse.Caption = "Warehouse"
        Me.nbi_mst_is_warehouse.Enabled = False
        Me.nbi_mst_is_warehouse.Hint = "Warehouse"
        Me.nbi_mst_is_warehouse.Name = "nbi_mst_is_warehouse"
        Me.nbi_mst_is_warehouse.SmallImageIndex = 1
        '
        'nbi_mst_is_warehouse_type
        '
        Me.nbi_mst_is_warehouse_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_warehouse_type.Appearance.Options.UseFont = True
        Me.nbi_mst_is_warehouse_type.Caption = "Warehouse Type"
        Me.nbi_mst_is_warehouse_type.Enabled = False
        Me.nbi_mst_is_warehouse_type.Hint = "Warehouse Type"
        Me.nbi_mst_is_warehouse_type.Name = "nbi_mst_is_warehouse_type"
        Me.nbi_mst_is_warehouse_type.SmallImageIndex = 1
        '
        'nbi_mst_is_warehouse_category
        '
        Me.nbi_mst_is_warehouse_category.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_warehouse_category.Appearance.Options.UseFont = True
        Me.nbi_mst_is_warehouse_category.Caption = "Warehouse Category"
        Me.nbi_mst_is_warehouse_category.Enabled = False
        Me.nbi_mst_is_warehouse_category.Hint = "Warehouse Category"
        Me.nbi_mst_is_warehouse_category.Name = "nbi_mst_is_warehouse_category"
        Me.nbi_mst_is_warehouse_category.SmallImageIndex = 1
        '
        'nbi_mst_is_location
        '
        Me.nbi_mst_is_location.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_location.Appearance.Options.UseFont = True
        Me.nbi_mst_is_location.Caption = "Location"
        Me.nbi_mst_is_location.Enabled = False
        Me.nbi_mst_is_location.Hint = "Location"
        Me.nbi_mst_is_location.Name = "nbi_mst_is_location"
        Me.nbi_mst_is_location.SmallImageIndex = 1
        '
        'nbi_mst_is_location_type
        '
        Me.nbi_mst_is_location_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_location_type.Appearance.Options.UseFont = True
        Me.nbi_mst_is_location_type.Caption = "Location Type"
        Me.nbi_mst_is_location_type.Enabled = False
        Me.nbi_mst_is_location_type.Hint = "Location Type"
        Me.nbi_mst_is_location_type.Name = "nbi_mst_is_location_type"
        Me.nbi_mst_is_location_type.SmallImageIndex = 1
        '
        'nbi_mst_is_location_category
        '
        Me.nbi_mst_is_location_category.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_location_category.Appearance.Options.UseFont = True
        Me.nbi_mst_is_location_category.Caption = "Location Category"
        Me.nbi_mst_is_location_category.Enabled = False
        Me.nbi_mst_is_location_category.Hint = "Location Category"
        Me.nbi_mst_is_location_category.Name = "nbi_mst_is_location_category"
        Me.nbi_mst_is_location_category.SmallImageIndex = 1
        '
        'nbi_mst_is_productline
        '
        Me.nbi_mst_is_productline.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_productline.Appearance.Options.UseFont = True
        Me.nbi_mst_is_productline.Caption = "Product Line"
        Me.nbi_mst_is_productline.Enabled = False
        Me.nbi_mst_is_productline.Hint = "Product Line"
        Me.nbi_mst_is_productline.Name = "nbi_mst_is_productline"
        Me.nbi_mst_is_productline.SmallImageIndex = 1
        '
        'nbi_mst_prodline_location
        '
        Me.nbi_mst_prodline_location.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_prodline_location.Appearance.Options.UseFont = True
        Me.nbi_mst_prodline_location.Caption = "Inventory Account"
        Me.nbi_mst_prodline_location.Enabled = False
        Me.nbi_mst_prodline_location.Hint = "Inventory Account"
        Me.nbi_mst_prodline_location.Name = "nbi_mst_prodline_location"
        Me.nbi_mst_prodline_location.SmallImageIndex = 1
        '
        'nbi_mst_is_unitmeasure
        '
        Me.nbi_mst_is_unitmeasure.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_unitmeasure.Appearance.Options.UseFont = True
        Me.nbi_mst_is_unitmeasure.Caption = "Unit Measure"
        Me.nbi_mst_is_unitmeasure.Enabled = False
        Me.nbi_mst_is_unitmeasure.Hint = "Unit Measure"
        Me.nbi_mst_is_unitmeasure.Name = "nbi_mst_is_unitmeasure"
        Me.nbi_mst_is_unitmeasure.SmallImageIndex = 1
        '
        'nbi_mst_is_unit_conversion
        '
        Me.nbi_mst_is_unit_conversion.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_is_unit_conversion.Appearance.Options.UseFont = True
        Me.nbi_mst_is_unit_conversion.Caption = "Unit Measure Conversion"
        Me.nbi_mst_is_unit_conversion.Enabled = False
        Me.nbi_mst_is_unit_conversion.Hint = "Unit Measure Conversion"
        Me.nbi_mst_is_unit_conversion.Name = "nbi_mst_is_unit_conversion"
        Me.nbi_mst_is_unit_conversion.SmallImageIndex = 1
        '
        'nbg_address_taxes
        '
        Me.nbg_address_taxes.Caption = "Addreses & Taxes"
        Me.nbg_address_taxes.Expanded = True
        Me.nbg_address_taxes.Hint = "Addreses & Taxes"
        Me.nbg_address_taxes.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner_address), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner_cp), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner_all), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_cp_function), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner_bank), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_partner_group), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_address_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_taxclass), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_tax_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_at_tax_rate), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_mst_fin_credit_terms)})
        Me.nbg_address_taxes.Name = "nbg_address_taxes"
        '
        'nbi_mst_at_partner
        '
        Me.nbi_mst_at_partner.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner.Caption = "Partner"
        Me.nbi_mst_at_partner.Enabled = False
        Me.nbi_mst_at_partner.Hint = "Partner"
        Me.nbi_mst_at_partner.Name = "nbi_mst_at_partner"
        Me.nbi_mst_at_partner.SmallImageIndex = 1
        '
        'nbi_mst_at_partner_address
        '
        Me.nbi_mst_at_partner_address.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner_address.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner_address.Caption = "Partner Address"
        Me.nbi_mst_at_partner_address.Enabled = False
        Me.nbi_mst_at_partner_address.Hint = "Partner Address"
        Me.nbi_mst_at_partner_address.Name = "nbi_mst_at_partner_address"
        Me.nbi_mst_at_partner_address.SmallImageIndex = 1
        '
        'nbi_mst_at_partner_cp
        '
        Me.nbi_mst_at_partner_cp.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner_cp.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner_cp.Caption = "Partner Contact Person"
        Me.nbi_mst_at_partner_cp.Enabled = False
        Me.nbi_mst_at_partner_cp.Hint = "Partner Contact Person"
        Me.nbi_mst_at_partner_cp.Name = "nbi_mst_at_partner_cp"
        Me.nbi_mst_at_partner_cp.SmallImageIndex = 1
        '
        'nbi_mst_at_partner_all
        '
        Me.nbi_mst_at_partner_all.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner_all.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner_all.Caption = "Partner Complete"
        Me.nbi_mst_at_partner_all.Enabled = False
        Me.nbi_mst_at_partner_all.Hint = "Partner Complete"
        Me.nbi_mst_at_partner_all.Name = "nbi_mst_at_partner_all"
        Me.nbi_mst_at_partner_all.SmallImageIndex = 1
        '
        'nbi_mst_at_cp_function
        '
        Me.nbi_mst_at_cp_function.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_cp_function.Appearance.Options.UseFont = True
        Me.nbi_mst_at_cp_function.Caption = "Contact Person Function"
        Me.nbi_mst_at_cp_function.Enabled = False
        Me.nbi_mst_at_cp_function.Hint = "Contact Person Function"
        Me.nbi_mst_at_cp_function.Name = "nbi_mst_at_cp_function"
        Me.nbi_mst_at_cp_function.SmallImageIndex = 1
        '
        'nbi_mst_at_partner_bank
        '
        Me.nbi_mst_at_partner_bank.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner_bank.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner_bank.Caption = "Partner Bank"
        Me.nbi_mst_at_partner_bank.Enabled = False
        Me.nbi_mst_at_partner_bank.Hint = "Partner Bank"
        Me.nbi_mst_at_partner_bank.Name = "nbi_mst_at_partner_bank"
        Me.nbi_mst_at_partner_bank.SmallImageIndex = 1
        '
        'nbi_mst_at_partner_group
        '
        Me.nbi_mst_at_partner_group.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_partner_group.Appearance.Options.UseFont = True
        Me.nbi_mst_at_partner_group.Caption = "Partner Group"
        Me.nbi_mst_at_partner_group.Enabled = False
        Me.nbi_mst_at_partner_group.Hint = "Partner Group"
        Me.nbi_mst_at_partner_group.Name = "nbi_mst_at_partner_group"
        Me.nbi_mst_at_partner_group.SmallImageIndex = 1
        '
        'nbi_mst_at_address_type
        '
        Me.nbi_mst_at_address_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_address_type.Appearance.Options.UseFont = True
        Me.nbi_mst_at_address_type.Caption = "Address Type"
        Me.nbi_mst_at_address_type.Enabled = False
        Me.nbi_mst_at_address_type.Hint = "Address Type"
        Me.nbi_mst_at_address_type.Name = "nbi_mst_at_address_type"
        Me.nbi_mst_at_address_type.SmallImageIndex = 1
        '
        'nbi_mst_at_taxclass
        '
        Me.nbi_mst_at_taxclass.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_taxclass.Appearance.Options.UseFont = True
        Me.nbi_mst_at_taxclass.Caption = "Tax Class"
        Me.nbi_mst_at_taxclass.Enabled = False
        Me.nbi_mst_at_taxclass.Hint = "Tax Class"
        Me.nbi_mst_at_taxclass.Name = "nbi_mst_at_taxclass"
        Me.nbi_mst_at_taxclass.SmallImageIndex = 1
        '
        'nbi_mst_at_tax_type
        '
        Me.nbi_mst_at_tax_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_tax_type.Appearance.Options.UseFont = True
        Me.nbi_mst_at_tax_type.Caption = "Tax Type"
        Me.nbi_mst_at_tax_type.Enabled = False
        Me.nbi_mst_at_tax_type.Hint = "Tax Type"
        Me.nbi_mst_at_tax_type.Name = "nbi_mst_at_tax_type"
        Me.nbi_mst_at_tax_type.SmallImageIndex = 1
        '
        'nbi_mst_at_tax_rate
        '
        Me.nbi_mst_at_tax_rate.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_at_tax_rate.Appearance.Options.UseFont = True
        Me.nbi_mst_at_tax_rate.Caption = "Tax Rate"
        Me.nbi_mst_at_tax_rate.Enabled = False
        Me.nbi_mst_at_tax_rate.Hint = "Tax Rate"
        Me.nbi_mst_at_tax_rate.Name = "nbi_mst_at_tax_rate"
        Me.nbi_mst_at_tax_rate.SmallImageIndex = 1
        '
        'nbi_mst_fin_credit_terms
        '
        Me.nbi_mst_fin_credit_terms.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_mst_fin_credit_terms.Appearance.Options.UseFont = True
        Me.nbi_mst_fin_credit_terms.Caption = "Credit Terms"
        Me.nbi_mst_fin_credit_terms.Enabled = False
        Me.nbi_mst_fin_credit_terms.Hint = "Credit Terms"
        Me.nbi_mst_fin_credit_terms.Name = "nbi_mst_fin_credit_terms"
        Me.nbi_mst_fin_credit_terms.SmallImageIndex = 1
        '
        'nbi_trn_req
        '
        Me.nbi_trn_req.Caption = "Requisition"
        Me.nbi_trn_req.Hint = "Requisition"
        Me.nbi_trn_req.Name = "nbi_trn_req"
        Me.nbi_trn_req.SmallImageIndex = 1
        '
        'dp_distribution
        '
        Me.dp_distribution.Controls.Add(Me.ControlContainer1)
        Me.dp_distribution.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.dp_distribution.FloatSize = New System.Drawing.Size(200, 314)
        Me.dp_distribution.ID = New System.Guid("cdae2749-379c-4345-8f6a-339e230f1b3c")
        Me.dp_distribution.Location = New System.Drawing.Point(3, 25)
        Me.dp_distribution.Name = "dp_distribution"
        Me.dp_distribution.OriginalSize = New System.Drawing.Size(218, 485)
        Me.dp_distribution.Size = New System.Drawing.Size(218, 503)
        Me.dp_distribution.Text = "Distribution"
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Controls.Add(Me.nbc_distribution)
        Me.ControlContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ControlContainer1.Name = "ControlContainer1"
        Me.ControlContainer1.Size = New System.Drawing.Size(218, 503)
        Me.ControlContainer1.TabIndex = 0
        '
        'nbc_distribution
        '
        Me.nbc_distribution.ActiveGroup = Me.nbg_requistion
        Me.nbc_distribution.ContentButtonHint = Nothing
        Me.nbc_distribution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nbc_distribution.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nbg_requistion, Me.nbg_purchase_order, Me.nbg_inventory_control})
        Me.nbc_distribution.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nbi_dist_req_mstr, Me.nbi_dist_purchase_order, Me.nbi_dist_purchase_receipt, Me.nbi_dist_purchase_return, Me.nbi_inv_inventory_detail, Me.nbi_dist_reason_code_return, Me.nbi_inv_receipts, Me.nbi_inv_issues, Me.nbi_inv_transfer_issues, Me.nbi_inv_transfer_receipts, Me.nbi_inv_inventory_begining_balance, Me.nbi_inv_adjusment_plus, Me.nbi_inv_adjustment_minus, Me.nbi_dist_purchase_order_print, Me.nbi_dist_purchase_receipt_print, Me.nbi_dist_purchase_return_print, Me.nbi_dist_req_mstr_print, Me.nbi_inv_transfer_issues_print_out, Me.nbi_inv_transfer_receipts_print_out, Me.nbi_inv_cycle_count, Me.nbi_dist_req_transfer_issue, Me.nbi_dist_req_transfer_issue_print, Me.nbi_dist_req_transfer_receipt, Me.nbi_dist_req_transfer_receipt_print, Me.nbi_inv_request, Me.nbi_inv_request_print, Me.nbi_inv_inventory_detail_2, Me.nbi_dist_purchase_order_report, Me.nbi_dist_purchase_receipt_report, Me.nbi_inv_inventory_by_eff_date, Me.nbi_dist_purchase_order_print_no_cost, Me.nbi_dist_req_mstr_approval, Me.nbi_dist_req_transfer_issue_approval, Me.nbi_inv_request_approval, Me.nbi_inv_transfer_issues_approval, Me.nbi_inv_receipts_print, Me.nbi_inv_issues_print, Me.nbi_inv_transfer_issues_return, Me.nbi_dist_inv_history, Me.nbi_inv_isues_report, Me.nbi_inv_receipts_report, Me.nbi_purchase_order_filem, Me.nbi_purchase_order_filem_print, Me.nbi_inv_adjusment_plus_report, Me.nbi_inv_adjustment_minus_report, Me.nbi_pr_boq, Me.nbi_inv_report_detail_wip})
        Me.nbc_distribution.LargeImages = Me.ImageList5
        Me.nbc_distribution.Location = New System.Drawing.Point(0, 0)
        Me.nbc_distribution.Name = "nbc_distribution"
        Me.nbc_distribution.OptionsNavPane.ExpandedWidth = 194
        Me.nbc_distribution.Size = New System.Drawing.Size(218, 503)
        Me.nbc_distribution.SmallImages = Me.ImageList4
        Me.nbc_distribution.TabIndex = 0
        Me.nbc_distribution.Text = "NavBarControl1"
        '
        'nbg_requistion
        '
        Me.nbg_requistion.Caption = "Purchase Requisition Menu"
        Me.nbg_requistion.Hint = "Purchase Requisition Menu"
        Me.nbg_requistion.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_mstr), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_mstr_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_mstr_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_transfer_issue), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_transfer_issue_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_transfer_issue_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_transfer_receipt), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_req_transfer_receipt_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_pr_boq)})
        Me.nbg_requistion.Name = "nbg_requistion"
        '
        'nbi_dist_req_mstr
        '
        Me.nbi_dist_req_mstr.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_mstr.Appearance.Options.UseFont = True
        Me.nbi_dist_req_mstr.Caption = "Purchase Requisition"
        Me.nbi_dist_req_mstr.Enabled = False
        Me.nbi_dist_req_mstr.Hint = "Purchase Requisition"
        Me.nbi_dist_req_mstr.Name = "nbi_dist_req_mstr"
        Me.nbi_dist_req_mstr.SmallImageIndex = 1
        '
        'nbi_dist_req_mstr_approval
        '
        Me.nbi_dist_req_mstr_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_mstr_approval.Appearance.Options.UseFont = True
        Me.nbi_dist_req_mstr_approval.Caption = "Requisition Approval"
        Me.nbi_dist_req_mstr_approval.Enabled = False
        Me.nbi_dist_req_mstr_approval.Hint = "Requisition Approval"
        Me.nbi_dist_req_mstr_approval.Name = "nbi_dist_req_mstr_approval"
        Me.nbi_dist_req_mstr_approval.SmallImageIndex = 1
        '
        'nbi_dist_req_mstr_print
        '
        Me.nbi_dist_req_mstr_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_mstr_print.Appearance.Options.UseFont = True
        Me.nbi_dist_req_mstr_print.Caption = "Purchase Requistion Print"
        Me.nbi_dist_req_mstr_print.Enabled = False
        Me.nbi_dist_req_mstr_print.Hint = "Purchase Requistion Print"
        Me.nbi_dist_req_mstr_print.Name = "nbi_dist_req_mstr_print"
        Me.nbi_dist_req_mstr_print.SmallImageIndex = 1
        '
        'nbi_dist_req_transfer_issue
        '
        Me.nbi_dist_req_transfer_issue.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_transfer_issue.Appearance.Options.UseFont = True
        Me.nbi_dist_req_transfer_issue.Caption = "Req. Transfer Issue"
        Me.nbi_dist_req_transfer_issue.Enabled = False
        Me.nbi_dist_req_transfer_issue.Hint = "Req. Transfer Issue"
        Me.nbi_dist_req_transfer_issue.Name = "nbi_dist_req_transfer_issue"
        Me.nbi_dist_req_transfer_issue.SmallImageIndex = 1
        '
        'nbi_dist_req_transfer_issue_approval
        '
        Me.nbi_dist_req_transfer_issue_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_transfer_issue_approval.Appearance.Options.UseFont = True
        Me.nbi_dist_req_transfer_issue_approval.Caption = "Requisition Issue Approval"
        Me.nbi_dist_req_transfer_issue_approval.Enabled = False
        Me.nbi_dist_req_transfer_issue_approval.Hint = "Requisition Issue Approval"
        Me.nbi_dist_req_transfer_issue_approval.Name = "nbi_dist_req_transfer_issue_approval"
        Me.nbi_dist_req_transfer_issue_approval.SmallImageIndex = 1
        '
        'nbi_dist_req_transfer_issue_print
        '
        Me.nbi_dist_req_transfer_issue_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_transfer_issue_print.Appearance.Options.UseFont = True
        Me.nbi_dist_req_transfer_issue_print.Caption = "Req. Transfer Issue Print"
        Me.nbi_dist_req_transfer_issue_print.Enabled = False
        Me.nbi_dist_req_transfer_issue_print.Hint = "Req. Transfer Issue Print"
        Me.nbi_dist_req_transfer_issue_print.Name = "nbi_dist_req_transfer_issue_print"
        Me.nbi_dist_req_transfer_issue_print.SmallImageIndex = 1
        '
        'nbi_dist_req_transfer_receipt
        '
        Me.nbi_dist_req_transfer_receipt.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_transfer_receipt.Appearance.Options.UseFont = True
        Me.nbi_dist_req_transfer_receipt.Caption = "Req. Transfer Receipt"
        Me.nbi_dist_req_transfer_receipt.Enabled = False
        Me.nbi_dist_req_transfer_receipt.Hint = "Req. Transfer Receipt"
        Me.nbi_dist_req_transfer_receipt.Name = "nbi_dist_req_transfer_receipt"
        Me.nbi_dist_req_transfer_receipt.SmallImageIndex = 1
        '
        'nbi_dist_req_transfer_receipt_print
        '
        Me.nbi_dist_req_transfer_receipt_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_req_transfer_receipt_print.Appearance.Options.UseFont = True
        Me.nbi_dist_req_transfer_receipt_print.Caption = "Req. Transfer Receipt Print"
        Me.nbi_dist_req_transfer_receipt_print.Enabled = False
        Me.nbi_dist_req_transfer_receipt_print.Hint = "Req. Transfer Receipt Print"
        Me.nbi_dist_req_transfer_receipt_print.Name = "nbi_dist_req_transfer_receipt_print"
        Me.nbi_dist_req_transfer_receipt_print.SmallImageIndex = 1
        '
        'nbi_pr_boq
        '
        Me.nbi_pr_boq.Caption = "BOQ Generate PR"
        Me.nbi_pr_boq.Hint = "BOQ Generate PR"
        Me.nbi_pr_boq.Name = "nbi_pr_boq"
        Me.nbi_pr_boq.SmallImageIndex = 1
        '
        'nbg_purchase_order
        '
        Me.nbg_purchase_order.Caption = "Purchase Order Menu"
        Me.nbg_purchase_order.Hint = "Purchase Order Menu"
        Me.nbg_purchase_order.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_order), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_order_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_order_print_no_cost), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_order_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_receipt), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_receipt_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_receipt_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_return), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_purchase_return_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_reason_code_return), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_purchase_order_filem), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_purchase_order_filem_print)})
        Me.nbg_purchase_order.Name = "nbg_purchase_order"
        '
        'nbi_dist_purchase_order
        '
        Me.nbi_dist_purchase_order.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_order.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_order.Caption = "Purchase Order"
        Me.nbi_dist_purchase_order.Enabled = False
        Me.nbi_dist_purchase_order.Hint = "Purchase Order"
        Me.nbi_dist_purchase_order.Name = "nbi_dist_purchase_order"
        Me.nbi_dist_purchase_order.SmallImageIndex = 1
        '
        'nbi_dist_purchase_order_print
        '
        Me.nbi_dist_purchase_order_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_order_print.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_order_print.Caption = "Purchase Order Print"
        Me.nbi_dist_purchase_order_print.Enabled = False
        Me.nbi_dist_purchase_order_print.Hint = "Purchase Order Print"
        Me.nbi_dist_purchase_order_print.Name = "nbi_dist_purchase_order_print"
        Me.nbi_dist_purchase_order_print.SmallImageIndex = 1
        '
        'nbi_dist_purchase_order_print_no_cost
        '
        Me.nbi_dist_purchase_order_print_no_cost.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_order_print_no_cost.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_order_print_no_cost.Caption = "Purchase Order Print No Cost"
        Me.nbi_dist_purchase_order_print_no_cost.Enabled = False
        Me.nbi_dist_purchase_order_print_no_cost.Hint = "Purchase Order Print No Cost"
        Me.nbi_dist_purchase_order_print_no_cost.Name = "nbi_dist_purchase_order_print_no_cost"
        Me.nbi_dist_purchase_order_print_no_cost.SmallImageIndex = 1
        '
        'nbi_dist_purchase_order_report
        '
        Me.nbi_dist_purchase_order_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_order_report.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_order_report.Caption = "Purchase Order Report"
        Me.nbi_dist_purchase_order_report.Enabled = False
        Me.nbi_dist_purchase_order_report.Hint = "Purchase Order Report"
        Me.nbi_dist_purchase_order_report.Name = "nbi_dist_purchase_order_report"
        Me.nbi_dist_purchase_order_report.SmallImageIndex = 1
        '
        'nbi_dist_purchase_receipt
        '
        Me.nbi_dist_purchase_receipt.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_receipt.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_receipt.Caption = "Purchase Receipt"
        Me.nbi_dist_purchase_receipt.Enabled = False
        Me.nbi_dist_purchase_receipt.Hint = "Purchase Receipt"
        Me.nbi_dist_purchase_receipt.Name = "nbi_dist_purchase_receipt"
        Me.nbi_dist_purchase_receipt.SmallImageIndex = 1
        '
        'nbi_dist_purchase_receipt_print
        '
        Me.nbi_dist_purchase_receipt_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_receipt_print.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_receipt_print.Caption = "Purchase Receipt Print"
        Me.nbi_dist_purchase_receipt_print.Enabled = False
        Me.nbi_dist_purchase_receipt_print.Hint = "Purchase Receipt Print"
        Me.nbi_dist_purchase_receipt_print.Name = "nbi_dist_purchase_receipt_print"
        Me.nbi_dist_purchase_receipt_print.SmallImageIndex = 1
        '
        'nbi_dist_purchase_receipt_report
        '
        Me.nbi_dist_purchase_receipt_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_receipt_report.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_receipt_report.Caption = "Purchase Receipt Report"
        Me.nbi_dist_purchase_receipt_report.Enabled = False
        Me.nbi_dist_purchase_receipt_report.Hint = "Purchase Receipt Report"
        Me.nbi_dist_purchase_receipt_report.Name = "nbi_dist_purchase_receipt_report"
        Me.nbi_dist_purchase_receipt_report.SmallImageIndex = 1
        '
        'nbi_dist_purchase_return
        '
        Me.nbi_dist_purchase_return.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_return.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_return.Caption = "Purchase Return"
        Me.nbi_dist_purchase_return.Enabled = False
        Me.nbi_dist_purchase_return.Hint = "Purchase Return"
        Me.nbi_dist_purchase_return.Name = "nbi_dist_purchase_return"
        Me.nbi_dist_purchase_return.SmallImageIndex = 1
        '
        'nbi_dist_purchase_return_print
        '
        Me.nbi_dist_purchase_return_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_purchase_return_print.Appearance.Options.UseFont = True
        Me.nbi_dist_purchase_return_print.Caption = "Purchase Return Print"
        Me.nbi_dist_purchase_return_print.Enabled = False
        Me.nbi_dist_purchase_return_print.Hint = "Purchase Return Print"
        Me.nbi_dist_purchase_return_print.Name = "nbi_dist_purchase_return_print"
        Me.nbi_dist_purchase_return_print.SmallImageIndex = 1
        '
        'nbi_dist_reason_code_return
        '
        Me.nbi_dist_reason_code_return.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_dist_reason_code_return.Appearance.Options.UseFont = True
        Me.nbi_dist_reason_code_return.Caption = "Reason Code Return"
        Me.nbi_dist_reason_code_return.Enabled = False
        Me.nbi_dist_reason_code_return.Hint = "Reason Code Return"
        Me.nbi_dist_reason_code_return.Name = "nbi_dist_reason_code_return"
        Me.nbi_dist_reason_code_return.SmallImageIndex = 1
        '
        'nbi_purchase_order_filem
        '
        Me.nbi_purchase_order_filem.Caption = "Purchase Order Film"
        Me.nbi_purchase_order_filem.Name = "nbi_purchase_order_filem"
        Me.nbi_purchase_order_filem.SmallImageIndex = 1
        '
        'nbi_purchase_order_filem_print
        '
        Me.nbi_purchase_order_filem_print.Caption = "Purchase Order Print Film"
        Me.nbi_purchase_order_filem_print.Name = "nbi_purchase_order_filem_print"
        Me.nbi_purchase_order_filem_print.SmallImageIndex = 1
        '
        'nbg_inventory_control
        '
        Me.nbg_inventory_control.Caption = "Inventory Control"
        Me.nbg_inventory_control.Expanded = True
        Me.nbg_inventory_control.Hint = "Inventory Control"
        Me.nbg_inventory_control.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_inventory_begining_balance), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_inventory_detail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_inventory_detail_2), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_dist_inv_history), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_inventory_by_eff_date), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_request), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_request_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_request_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_receipts), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_receipts_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_issues), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_issues_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_issues), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_issues_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_issues_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_receipts), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_receipts_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_adjusment_plus), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_adjustment_minus), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_cycle_count), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_transfer_issues_return), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_isues_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_receipts_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_adjusment_plus_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_adjustment_minus_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_inv_report_detail_wip)})
        Me.nbg_inventory_control.Name = "nbg_inventory_control"
        '
        'nbi_inv_inventory_begining_balance
        '
        Me.nbi_inv_inventory_begining_balance.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_inventory_begining_balance.Appearance.Options.UseFont = True
        Me.nbi_inv_inventory_begining_balance.Caption = "Inventory Begining Balance"
        Me.nbi_inv_inventory_begining_balance.Enabled = False
        Me.nbi_inv_inventory_begining_balance.Hint = "Inventory Begining Balance"
        Me.nbi_inv_inventory_begining_balance.Name = "nbi_inv_inventory_begining_balance"
        Me.nbi_inv_inventory_begining_balance.SmallImageIndex = 1
        '
        'nbi_inv_inventory_detail
        '
        Me.nbi_inv_inventory_detail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_inventory_detail.Appearance.Options.UseFont = True
        Me.nbi_inv_inventory_detail.Caption = "Inventory Report Detail"
        Me.nbi_inv_inventory_detail.Enabled = False
        Me.nbi_inv_inventory_detail.Hint = "Inventory Report Detail"
        Me.nbi_inv_inventory_detail.Name = "nbi_inv_inventory_detail"
        Me.nbi_inv_inventory_detail.SmallImageIndex = 1
        '
        'nbi_inv_inventory_detail_2
        '
        Me.nbi_inv_inventory_detail_2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_inventory_detail_2.Appearance.Options.UseFont = True
        Me.nbi_inv_inventory_detail_2.Caption = "Inventory Report Detail Log"
        Me.nbi_inv_inventory_detail_2.Enabled = False
        Me.nbi_inv_inventory_detail_2.Hint = "Inventory Report Detail Log"
        Me.nbi_inv_inventory_detail_2.Name = "nbi_inv_inventory_detail_2"
        Me.nbi_inv_inventory_detail_2.SmallImageIndex = 1
        '
        'nbi_dist_inv_history
        '
        Me.nbi_dist_inv_history.Caption = "Inventory Historical"
        Me.nbi_dist_inv_history.Name = "nbi_dist_inv_history"
        Me.nbi_dist_inv_history.SmallImageIndex = 1
        '
        'nbi_inv_inventory_by_eff_date
        '
        Me.nbi_inv_inventory_by_eff_date.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_inventory_by_eff_date.Appearance.Options.UseFont = True
        Me.nbi_inv_inventory_by_eff_date.Caption = "Inventory Report By Effective Date"
        Me.nbi_inv_inventory_by_eff_date.Enabled = False
        Me.nbi_inv_inventory_by_eff_date.Hint = "Inventory Report By Effective Date"
        Me.nbi_inv_inventory_by_eff_date.Name = "nbi_inv_inventory_by_eff_date"
        Me.nbi_inv_inventory_by_eff_date.SmallImageIndex = 1
        '
        'nbi_inv_request
        '
        Me.nbi_inv_request.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_request.Appearance.Options.UseFont = True
        Me.nbi_inv_request.Caption = "Inventory Request"
        Me.nbi_inv_request.Enabled = False
        Me.nbi_inv_request.Hint = "Inventory Request"
        Me.nbi_inv_request.Name = "nbi_inv_request"
        Me.nbi_inv_request.SmallImageIndex = 1
        '
        'nbi_inv_request_approval
        '
        Me.nbi_inv_request_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_request_approval.Appearance.Options.UseFont = True
        Me.nbi_inv_request_approval.Caption = "Inventory Request Approval"
        Me.nbi_inv_request_approval.Enabled = False
        Me.nbi_inv_request_approval.Hint = "Inventory Request Approval"
        Me.nbi_inv_request_approval.Name = "nbi_inv_request_approval"
        Me.nbi_inv_request_approval.SmallImageIndex = 1
        '
        'nbi_inv_request_print
        '
        Me.nbi_inv_request_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_request_print.Appearance.Options.UseFont = True
        Me.nbi_inv_request_print.Caption = "Inventory Request Print"
        Me.nbi_inv_request_print.Enabled = False
        Me.nbi_inv_request_print.Hint = "Inventory Request Print"
        Me.nbi_inv_request_print.Name = "nbi_inv_request_print"
        Me.nbi_inv_request_print.SmallImageIndex = 1
        '
        'nbi_inv_receipts
        '
        Me.nbi_inv_receipts.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_receipts.Appearance.Options.UseFont = True
        Me.nbi_inv_receipts.Caption = "Inventory Receipts"
        Me.nbi_inv_receipts.Enabled = False
        Me.nbi_inv_receipts.Hint = "Inventory Receipts"
        Me.nbi_inv_receipts.Name = "nbi_inv_receipts"
        Me.nbi_inv_receipts.SmallImageIndex = 1
        '
        'nbi_inv_receipts_print
        '
        Me.nbi_inv_receipts_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_receipts_print.Appearance.Options.UseFont = True
        Me.nbi_inv_receipts_print.Caption = "Inventory Receipt Print"
        Me.nbi_inv_receipts_print.Enabled = False
        Me.nbi_inv_receipts_print.Hint = "Inventory Receipt Print"
        Me.nbi_inv_receipts_print.Name = "nbi_inv_receipts_print"
        Me.nbi_inv_receipts_print.SmallImageIndex = 1
        '
        'nbi_inv_issues
        '
        Me.nbi_inv_issues.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_issues.Appearance.Options.UseFont = True
        Me.nbi_inv_issues.Caption = "Inventory Issues"
        Me.nbi_inv_issues.Enabled = False
        Me.nbi_inv_issues.Hint = "Inventory Issues"
        Me.nbi_inv_issues.Name = "nbi_inv_issues"
        Me.nbi_inv_issues.SmallImageIndex = 1
        '
        'nbi_inv_issues_print
        '
        Me.nbi_inv_issues_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_issues_print.Appearance.Options.UseFont = True
        Me.nbi_inv_issues_print.Caption = "Inventory Issue Print"
        Me.nbi_inv_issues_print.Enabled = False
        Me.nbi_inv_issues_print.Hint = "Inventory Issue Print"
        Me.nbi_inv_issues_print.Name = "nbi_inv_issues_print"
        Me.nbi_inv_issues_print.SmallImageIndex = 1
        '
        'nbi_inv_transfer_issues
        '
        Me.nbi_inv_transfer_issues.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_transfer_issues.Appearance.Options.UseFont = True
        Me.nbi_inv_transfer_issues.Caption = "Inventory Transfer Issues"
        Me.nbi_inv_transfer_issues.Enabled = False
        Me.nbi_inv_transfer_issues.Hint = "Inventory Transfer Issues"
        Me.nbi_inv_transfer_issues.Name = "nbi_inv_transfer_issues"
        Me.nbi_inv_transfer_issues.SmallImageIndex = 1
        '
        'nbi_inv_transfer_issues_approval
        '
        Me.nbi_inv_transfer_issues_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_transfer_issues_approval.Appearance.Options.UseFont = True
        Me.nbi_inv_transfer_issues_approval.Caption = "Inv. Transfer Issue Approval"
        Me.nbi_inv_transfer_issues_approval.Enabled = False
        Me.nbi_inv_transfer_issues_approval.Hint = "Inv. Transfer Issue Approval"
        Me.nbi_inv_transfer_issues_approval.Name = "nbi_inv_transfer_issues_approval"
        Me.nbi_inv_transfer_issues_approval.SmallImageIndex = 1
        '
        'nbi_inv_transfer_issues_print_out
        '
        Me.nbi_inv_transfer_issues_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_transfer_issues_print_out.Appearance.Options.UseFont = True
        Me.nbi_inv_transfer_issues_print_out.Caption = "Inv. Transfer Issue Print"
        Me.nbi_inv_transfer_issues_print_out.Enabled = False
        Me.nbi_inv_transfer_issues_print_out.Hint = "Inv. Transfer Issue Print"
        Me.nbi_inv_transfer_issues_print_out.Name = "nbi_inv_transfer_issues_print_out"
        Me.nbi_inv_transfer_issues_print_out.SmallImageIndex = 1
        '
        'nbi_inv_transfer_receipts
        '
        Me.nbi_inv_transfer_receipts.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_transfer_receipts.Appearance.Options.UseFont = True
        Me.nbi_inv_transfer_receipts.Caption = "Inventory Transfer Receipts"
        Me.nbi_inv_transfer_receipts.Enabled = False
        Me.nbi_inv_transfer_receipts.Hint = "Inventory Transfer Receipts"
        Me.nbi_inv_transfer_receipts.Name = "nbi_inv_transfer_receipts"
        Me.nbi_inv_transfer_receipts.SmallImageIndex = 1
        '
        'nbi_inv_transfer_receipts_print_out
        '
        Me.nbi_inv_transfer_receipts_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_transfer_receipts_print_out.Appearance.Options.UseFont = True
        Me.nbi_inv_transfer_receipts_print_out.Caption = "Inv. Transfer Receipt Print"
        Me.nbi_inv_transfer_receipts_print_out.Enabled = False
        Me.nbi_inv_transfer_receipts_print_out.Hint = "Inv. Transfer Receipt Print"
        Me.nbi_inv_transfer_receipts_print_out.Name = "nbi_inv_transfer_receipts_print_out"
        Me.nbi_inv_transfer_receipts_print_out.SmallImageIndex = 1
        '
        'nbi_inv_adjusment_plus
        '
        Me.nbi_inv_adjusment_plus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_adjusment_plus.Appearance.Options.UseFont = True
        Me.nbi_inv_adjusment_plus.Caption = "Inventory Adjustment Plus"
        Me.nbi_inv_adjusment_plus.Enabled = False
        Me.nbi_inv_adjusment_plus.Hint = "Inventory Adjustment Plus"
        Me.nbi_inv_adjusment_plus.Name = "nbi_inv_adjusment_plus"
        Me.nbi_inv_adjusment_plus.SmallImageIndex = 1
        '
        'nbi_inv_adjustment_minus
        '
        Me.nbi_inv_adjustment_minus.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_adjustment_minus.Appearance.Options.UseFont = True
        Me.nbi_inv_adjustment_minus.Caption = "Inventory Adjustment Minus"
        Me.nbi_inv_adjustment_minus.Enabled = False
        Me.nbi_inv_adjustment_minus.Hint = "Inventory Adjustment Minus"
        Me.nbi_inv_adjustment_minus.Name = "nbi_inv_adjustment_minus"
        Me.nbi_inv_adjustment_minus.SmallImageIndex = 1
        '
        'nbi_inv_cycle_count
        '
        Me.nbi_inv_cycle_count.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_cycle_count.Appearance.Options.UseFont = True
        Me.nbi_inv_cycle_count.Caption = "Inventory Cycle Count"
        Me.nbi_inv_cycle_count.Enabled = False
        Me.nbi_inv_cycle_count.Hint = "Inventory Cycle Count"
        Me.nbi_inv_cycle_count.Name = "nbi_inv_cycle_count"
        Me.nbi_inv_cycle_count.SmallImageIndex = 1
        '
        'nbi_inv_transfer_issues_return
        '
        Me.nbi_inv_transfer_issues_return.Caption = "Inventory Transfer Issues Return"
        Me.nbi_inv_transfer_issues_return.Name = "nbi_inv_transfer_issues_return"
        Me.nbi_inv_transfer_issues_return.SmallImageIndex = 1
        '
        'nbi_inv_isues_report
        '
        Me.nbi_inv_isues_report.Caption = "Inventory Isues Report"
        Me.nbi_inv_isues_report.Name = "nbi_inv_isues_report"
        Me.nbi_inv_isues_report.SmallImageIndex = 1
        '
        'nbi_inv_receipts_report
        '
        Me.nbi_inv_receipts_report.Caption = "Inventory Receipts Report"
        Me.nbi_inv_receipts_report.Name = "nbi_inv_receipts_report"
        Me.nbi_inv_receipts_report.SmallImageIndex = 1
        '
        'nbi_inv_adjusment_plus_report
        '
        Me.nbi_inv_adjusment_plus_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_adjusment_plus_report.Appearance.Options.UseFont = True
        Me.nbi_inv_adjusment_plus_report.Caption = "Inventory Adjustment Plus Report"
        Me.nbi_inv_adjusment_plus_report.Enabled = False
        Me.nbi_inv_adjusment_plus_report.Hint = "Inventory Adjustment Plus Report"
        Me.nbi_inv_adjusment_plus_report.Name = "nbi_inv_adjusment_plus_report"
        Me.nbi_inv_adjusment_plus_report.SmallImageIndex = 1
        '
        'nbi_inv_adjustment_minus_report
        '
        Me.nbi_inv_adjustment_minus_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_inv_adjustment_minus_report.Appearance.Options.UseFont = True
        Me.nbi_inv_adjustment_minus_report.Caption = "Inventory Adjustment Minus Report"
        Me.nbi_inv_adjustment_minus_report.Enabled = False
        Me.nbi_inv_adjustment_minus_report.Hint = "Inventory Adjustment Minus Report"
        Me.nbi_inv_adjustment_minus_report.Name = "nbi_inv_adjustment_minus_report"
        Me.nbi_inv_adjustment_minus_report.SmallImageIndex = 1
        '
        'nbi_inv_report_detail_wip
        '
        Me.nbi_inv_report_detail_wip.Caption = "Report Detail Log WIP"
        Me.nbi_inv_report_detail_wip.Name = "nbi_inv_report_detail_wip"
        Me.nbi_inv_report_detail_wip.SmallImageIndex = 1
        '
        'dp_sales_order
        '
        Me.dp_sales_order.Controls.Add(Me.ControlContainer3)
        Me.dp_sales_order.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.dp_sales_order.FloatSize = New System.Drawing.Size(200, 314)
        Me.dp_sales_order.FloatVertical = True
        Me.dp_sales_order.Hint = "Sales Order"
        Me.dp_sales_order.ID = New System.Guid("ee3ca6ab-d118-42a9-9df6-49976542f528")
        Me.dp_sales_order.Location = New System.Drawing.Point(3, 25)
        Me.dp_sales_order.Name = "dp_sales_order"
        Me.dp_sales_order.OriginalSize = New System.Drawing.Size(218, 485)
        Me.dp_sales_order.Size = New System.Drawing.Size(218, 503)
        Me.dp_sales_order.Text = "Sales Order"
        '
        'ControlContainer3
        '
        Me.ControlContainer3.Controls.Add(Me.nbc_sales_order)
        Me.ControlContainer3.Location = New System.Drawing.Point(0, 0)
        Me.ControlContainer3.Name = "ControlContainer3"
        Me.ControlContainer3.Size = New System.Drawing.Size(218, 503)
        Me.ControlContainer3.TabIndex = 0
        '
        'nbc_sales_order
        '
        Me.nbc_sales_order.ActiveGroup = Me.nbg_sales_order
        Me.nbc_sales_order.ContentButtonHint = Nothing
        Me.nbc_sales_order.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nbc_sales_order.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nbg_sales_order, Me.nbg_so_insentif_royalti, Me.nbg_project})
        Me.nbc_sales_order.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nbi_so_promo, Me.nbi_sales_order_pricelist, Me.nbi_sales_order_so, Me.nbi_sales_order_shipments, Me.nbi_sales_order_returns, Me.nbi_sales_order_sales_program, Me.nbi_sales_order_pay_type, Me.nbi_sales_order_payment_methode, Me.nbi_sales_order_reason_code, Me.nbi_sales_order_shipments_print_out, Me.nbi_sales_order_returns_print_out, Me.nbi_sales_order_intensif_ds, Me.nbi_sales_order_periode_intensif_ds, Me.nbi_sales_order_rule_ds, Me.nbi_sales_order_area, Me.nbi_sales_order_royalti_rule, Me.nbi_sales_order_sales_struktur, Me.nbi_sales_order_periode_intensif_rs, Me.nbi_sales_order_rule_rs, Me.nbi_sales_order_cf_rs, Me.nbi_sales_order_intensif_rs, Me.nbi_sales_order_royalti_periode, Me.nbi_sales_order_royalti, Me.nbi_sales_order_so_print, Me.nbi_sales_order_faktur_penjualan_print, Me.nbi_sales_order_manual_allocation, Me.nbi_sales_order_so_report, Me.nbi_sales_order_so_approval, Me.nbi_sales_order_pricelist_header, Me.nbi_sales_order_pricelist_detail, Me.nbi_sales_order_pricelist_copy, Me.nbi_sales_order_so_credit_report, Me.nbi_so_retur_report, Me.nbi_so_ship_report, Me.nbi_so_faktur_pajak_report, Me.nbi_prj_project, Me.nbi_prj_po_customer, Me.nbi_prj_project_type, Me.nbi_prj_project_area, Me.nbi_prj_boq, Me.nbi_prj_boq_approval})
        Me.nbc_sales_order.LargeImages = Me.ImageList4
        Me.nbc_sales_order.Location = New System.Drawing.Point(0, 0)
        Me.nbc_sales_order.Name = "nbc_sales_order"
        Me.nbc_sales_order.OptionsNavPane.ExpandedWidth = 194
        Me.nbc_sales_order.Size = New System.Drawing.Size(218, 503)
        Me.nbc_sales_order.SmallImages = Me.ImageList4
        Me.nbc_sales_order.TabIndex = 0
        Me.nbc_sales_order.Text = "NavBarControl1"
        '
        'nbg_sales_order
        '
        Me.nbg_sales_order.Caption = "Sales Order"
        Me.nbg_sales_order.Expanded = True
        Me.nbg_sales_order.Hint = "Sales Order"
        Me.nbg_sales_order.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_sales_program), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_pay_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_payment_methode), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_reason_code), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_so_promo), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_pricelist), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_pricelist_header), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_pricelist_detail), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_pricelist_copy), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_so), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_so_credit_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_so_approval), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_so_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_so_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_faktur_penjualan_print), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_shipments), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_shipments_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_returns), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_returns_print_out), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_manual_allocation), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_so_retur_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_so_ship_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_so_faktur_pajak_report)})
        Me.nbg_sales_order.Name = "nbg_sales_order"
        '
        'nbi_sales_order_sales_program
        '
        Me.nbi_sales_order_sales_program.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_sales_program.Appearance.Options.UseFont = True
        Me.nbi_sales_order_sales_program.Caption = "Sales Program"
        Me.nbi_sales_order_sales_program.Enabled = False
        Me.nbi_sales_order_sales_program.Hint = "Sales Program"
        Me.nbi_sales_order_sales_program.Name = "nbi_sales_order_sales_program"
        Me.nbi_sales_order_sales_program.SmallImageIndex = 1
        '
        'nbi_sales_order_pay_type
        '
        Me.nbi_sales_order_pay_type.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_pay_type.Appearance.Options.UseFont = True
        Me.nbi_sales_order_pay_type.Caption = "Payment Type"
        Me.nbi_sales_order_pay_type.Enabled = False
        Me.nbi_sales_order_pay_type.Hint = "Payment Type"
        Me.nbi_sales_order_pay_type.Name = "nbi_sales_order_pay_type"
        Me.nbi_sales_order_pay_type.SmallImageIndex = 1
        '
        'nbi_sales_order_payment_methode
        '
        Me.nbi_sales_order_payment_methode.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_payment_methode.Appearance.Options.UseFont = True
        Me.nbi_sales_order_payment_methode.Caption = "Payment Methode"
        Me.nbi_sales_order_payment_methode.Enabled = False
        Me.nbi_sales_order_payment_methode.Hint = "Payment Methode"
        Me.nbi_sales_order_payment_methode.Name = "nbi_sales_order_payment_methode"
        Me.nbi_sales_order_payment_methode.SmallImageIndex = 1
        '
        'nbi_sales_order_reason_code
        '
        Me.nbi_sales_order_reason_code.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_reason_code.Appearance.Options.UseFont = True
        Me.nbi_sales_order_reason_code.Caption = "Reason Code Return"
        Me.nbi_sales_order_reason_code.Enabled = False
        Me.nbi_sales_order_reason_code.Hint = "Reason Code Return"
        Me.nbi_sales_order_reason_code.Name = "nbi_sales_order_reason_code"
        Me.nbi_sales_order_reason_code.SmallImageIndex = 1
        '
        'nbi_so_promo
        '
        Me.nbi_so_promo.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_so_promo.Appearance.Options.UseFont = True
        Me.nbi_so_promo.Caption = "Promotion"
        Me.nbi_so_promo.Enabled = False
        Me.nbi_so_promo.Hint = "Promotion"
        Me.nbi_so_promo.Name = "nbi_so_promo"
        Me.nbi_so_promo.SmallImageIndex = 1
        '
        'nbi_sales_order_pricelist
        '
        Me.nbi_sales_order_pricelist.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_pricelist.Appearance.Options.UseFont = True
        Me.nbi_sales_order_pricelist.Caption = "Price List"
        Me.nbi_sales_order_pricelist.Enabled = False
        Me.nbi_sales_order_pricelist.Hint = "Price List"
        Me.nbi_sales_order_pricelist.Name = "nbi_sales_order_pricelist"
        Me.nbi_sales_order_pricelist.SmallImageIndex = 1
        '
        'nbi_sales_order_pricelist_header
        '
        Me.nbi_sales_order_pricelist_header.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_pricelist_header.Appearance.Options.UseFont = True
        Me.nbi_sales_order_pricelist_header.Caption = "Price List Name"
        Me.nbi_sales_order_pricelist_header.Enabled = False
        Me.nbi_sales_order_pricelist_header.Hint = "Price List Name"
        Me.nbi_sales_order_pricelist_header.Name = "nbi_sales_order_pricelist_header"
        Me.nbi_sales_order_pricelist_header.SmallImageIndex = 1
        '
        'nbi_sales_order_pricelist_detail
        '
        Me.nbi_sales_order_pricelist_detail.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_pricelist_detail.Appearance.Options.UseFont = True
        Me.nbi_sales_order_pricelist_detail.Caption = "Price List Detail"
        Me.nbi_sales_order_pricelist_detail.Enabled = False
        Me.nbi_sales_order_pricelist_detail.Hint = "Price List Detail"
        Me.nbi_sales_order_pricelist_detail.Name = "nbi_sales_order_pricelist_detail"
        Me.nbi_sales_order_pricelist_detail.SmallImageIndex = 1
        '
        'nbi_sales_order_pricelist_copy
        '
        Me.nbi_sales_order_pricelist_copy.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_pricelist_copy.Appearance.Options.UseFont = True
        Me.nbi_sales_order_pricelist_copy.Caption = "Price List Copy"
        Me.nbi_sales_order_pricelist_copy.Enabled = False
        Me.nbi_sales_order_pricelist_copy.Hint = "Price List Copy"
        Me.nbi_sales_order_pricelist_copy.Name = "nbi_sales_order_pricelist_copy"
        Me.nbi_sales_order_pricelist_copy.SmallImageIndex = 1
        '
        'nbi_sales_order_so
        '
        Me.nbi_sales_order_so.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_so.Appearance.Options.UseFont = True
        Me.nbi_sales_order_so.Caption = "Sales Order"
        Me.nbi_sales_order_so.Enabled = False
        Me.nbi_sales_order_so.Hint = "Sales Order"
        Me.nbi_sales_order_so.Name = "nbi_sales_order_so"
        Me.nbi_sales_order_so.SmallImageIndex = 1
        '
        'nbi_sales_order_so_credit_report
        '
        Me.nbi_sales_order_so_credit_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_so_credit_report.Appearance.Options.UseFont = True
        Me.nbi_sales_order_so_credit_report.Caption = "Sales Order Credit Report"
        Me.nbi_sales_order_so_credit_report.Enabled = False
        Me.nbi_sales_order_so_credit_report.Hint = "Sales Order Credit Report"
        Me.nbi_sales_order_so_credit_report.Name = "nbi_sales_order_so_credit_report"
        Me.nbi_sales_order_so_credit_report.SmallImageIndex = 1
        '
        'nbi_sales_order_so_approval
        '
        Me.nbi_sales_order_so_approval.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_so_approval.Appearance.Options.UseFont = True
        Me.nbi_sales_order_so_approval.Caption = "Sales Order Approval"
        Me.nbi_sales_order_so_approval.Enabled = False
        Me.nbi_sales_order_so_approval.Hint = "Sales Order Approval"
        Me.nbi_sales_order_so_approval.Name = "nbi_sales_order_so_approval"
        Me.nbi_sales_order_so_approval.SmallImageIndex = 1
        '
        'nbi_sales_order_so_print
        '
        Me.nbi_sales_order_so_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_so_print.Appearance.Options.UseFont = True
        Me.nbi_sales_order_so_print.Caption = "Sales Order Print"
        Me.nbi_sales_order_so_print.Enabled = False
        Me.nbi_sales_order_so_print.Hint = "Sales Order Print"
        Me.nbi_sales_order_so_print.Name = "nbi_sales_order_so_print"
        Me.nbi_sales_order_so_print.SmallImageIndex = 1
        '
        'nbi_sales_order_so_report
        '
        Me.nbi_sales_order_so_report.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_so_report.Appearance.Options.UseFont = True
        Me.nbi_sales_order_so_report.Caption = "Sales Order Report"
        Me.nbi_sales_order_so_report.Enabled = False
        Me.nbi_sales_order_so_report.Hint = "Sales Order Report"
        Me.nbi_sales_order_so_report.Name = "nbi_sales_order_so_report"
        Me.nbi_sales_order_so_report.SmallImageIndex = 1
        '
        'nbi_sales_order_faktur_penjualan_print
        '
        Me.nbi_sales_order_faktur_penjualan_print.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_faktur_penjualan_print.Appearance.Options.UseFont = True
        Me.nbi_sales_order_faktur_penjualan_print.Caption = "Sales Order Faktur Penjualan Print"
        Me.nbi_sales_order_faktur_penjualan_print.Enabled = False
        Me.nbi_sales_order_faktur_penjualan_print.Hint = "Sales Order Faktur Penjualan Print"
        Me.nbi_sales_order_faktur_penjualan_print.Name = "nbi_sales_order_faktur_penjualan_print"
        Me.nbi_sales_order_faktur_penjualan_print.SmallImageIndex = 1
        '
        'nbi_sales_order_shipments
        '
        Me.nbi_sales_order_shipments.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_shipments.Appearance.Options.UseFont = True
        Me.nbi_sales_order_shipments.Caption = "Sales Order Shipments"
        Me.nbi_sales_order_shipments.Enabled = False
        Me.nbi_sales_order_shipments.Hint = "Sales Order Shipments"
        Me.nbi_sales_order_shipments.Name = "nbi_sales_order_shipments"
        Me.nbi_sales_order_shipments.SmallImageIndex = 1
        '
        'nbi_sales_order_shipments_print_out
        '
        Me.nbi_sales_order_shipments_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_shipments_print_out.Appearance.Options.UseFont = True
        Me.nbi_sales_order_shipments_print_out.Caption = "Sales Order Shipments Print"
        Me.nbi_sales_order_shipments_print_out.Enabled = False
        Me.nbi_sales_order_shipments_print_out.Hint = "Sales Order Shipments Print"
        Me.nbi_sales_order_shipments_print_out.Name = "nbi_sales_order_shipments_print_out"
        Me.nbi_sales_order_shipments_print_out.SmallImageIndex = 1
        '
        'nbi_sales_order_returns
        '
        Me.nbi_sales_order_returns.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_returns.Appearance.Options.UseFont = True
        Me.nbi_sales_order_returns.Caption = "Sales Order Returns"
        Me.nbi_sales_order_returns.Enabled = False
        Me.nbi_sales_order_returns.Hint = "Sales Order Returns"
        Me.nbi_sales_order_returns.Name = "nbi_sales_order_returns"
        Me.nbi_sales_order_returns.SmallImageIndex = 1
        '
        'nbi_sales_order_returns_print_out
        '
        Me.nbi_sales_order_returns_print_out.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_returns_print_out.Appearance.Options.UseFont = True
        Me.nbi_sales_order_returns_print_out.Caption = "Sales Order Retuns Print"
        Me.nbi_sales_order_returns_print_out.Enabled = False
        Me.nbi_sales_order_returns_print_out.Hint = "Sales Order Retuns Print"
        Me.nbi_sales_order_returns_print_out.Name = "nbi_sales_order_returns_print_out"
        Me.nbi_sales_order_returns_print_out.SmallImageIndex = 1
        '
        'nbi_sales_order_manual_allocation
        '
        Me.nbi_sales_order_manual_allocation.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_manual_allocation.Appearance.Options.UseFont = True
        Me.nbi_sales_order_manual_allocation.Caption = "Sales Order Manual Allocation"
        Me.nbi_sales_order_manual_allocation.Enabled = False
        Me.nbi_sales_order_manual_allocation.Hint = "Sales Order Manual Allocation"
        Me.nbi_sales_order_manual_allocation.Name = "nbi_sales_order_manual_allocation"
        Me.nbi_sales_order_manual_allocation.SmallImageIndex = 1
        '
        'nbi_so_retur_report
        '
        Me.nbi_so_retur_report.Caption = "Sales Order Retur Report"
        Me.nbi_so_retur_report.Name = "nbi_so_retur_report"
        Me.nbi_so_retur_report.SmallImageIndex = 1
        '
        'nbi_so_ship_report
        '
        Me.nbi_so_ship_report.Caption = "Sales Order Shipment Report"
        Me.nbi_so_ship_report.Name = "nbi_so_ship_report"
        Me.nbi_so_ship_report.SmallImageIndex = 1
        '
        'nbi_so_faktur_pajak_report
        '
        Me.nbi_so_faktur_pajak_report.Caption = "Sales Order Faktur Pajak Report"
        Me.nbi_so_faktur_pajak_report.Name = "nbi_so_faktur_pajak_report"
        Me.nbi_so_faktur_pajak_report.SmallImageIndex = 1
        '
        'nbg_so_insentif_royalti
        '
        Me.nbg_so_insentif_royalti.Caption = "Insentif & Royalti"
        Me.nbg_so_insentif_royalti.Hint = "Insentif & Royalti"
        Me.nbg_so_insentif_royalti.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_area), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_sales_struktur), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_royalti_rule), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_royalti_periode), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_royalti), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_periode_intensif_ds), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_rule_ds), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_intensif_ds), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_periode_intensif_rs), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_rule_rs), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_cf_rs), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_sales_order_intensif_rs)})
        Me.nbg_so_insentif_royalti.Name = "nbg_so_insentif_royalti"
        '
        'nbi_sales_order_area
        '
        Me.nbi_sales_order_area.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_area.Appearance.Options.UseFont = True
        Me.nbi_sales_order_area.Caption = "Area"
        Me.nbi_sales_order_area.Enabled = False
        Me.nbi_sales_order_area.Hint = "Area"
        Me.nbi_sales_order_area.Name = "nbi_sales_order_area"
        Me.nbi_sales_order_area.SmallImageIndex = 1
        '
        'nbi_sales_order_sales_struktur
        '
        Me.nbi_sales_order_sales_struktur.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_sales_struktur.Appearance.Options.UseFont = True
        Me.nbi_sales_order_sales_struktur.Caption = "Regular Selling Sales Structure"
        Me.nbi_sales_order_sales_struktur.Enabled = False
        Me.nbi_sales_order_sales_struktur.Hint = "Regular Selling Sales Structure"
        Me.nbi_sales_order_sales_struktur.Name = "nbi_sales_order_sales_struktur"
        Me.nbi_sales_order_sales_struktur.SmallImageIndex = 1
        '
        'nbi_sales_order_royalti_rule
        '
        Me.nbi_sales_order_royalti_rule.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_royalti_rule.Appearance.Options.UseFont = True
        Me.nbi_sales_order_royalti_rule.Caption = "Royalti Rule"
        Me.nbi_sales_order_royalti_rule.Enabled = False
        Me.nbi_sales_order_royalti_rule.Hint = "Royalti Rule"
        Me.nbi_sales_order_royalti_rule.Name = "nbi_sales_order_royalti_rule"
        Me.nbi_sales_order_royalti_rule.SmallImageIndex = 1
        '
        'nbi_sales_order_royalti_periode
        '
        Me.nbi_sales_order_royalti_periode.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_royalti_periode.Appearance.Options.UseFont = True
        Me.nbi_sales_order_royalti_periode.Caption = "Royalti Periode"
        Me.nbi_sales_order_royalti_periode.Enabled = False
        Me.nbi_sales_order_royalti_periode.Hint = "Royalti Periode"
        Me.nbi_sales_order_royalti_periode.Name = "nbi_sales_order_royalti_periode"
        Me.nbi_sales_order_royalti_periode.SmallImageIndex = 1
        '
        'nbi_sales_order_royalti
        '
        Me.nbi_sales_order_royalti.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_royalti.Appearance.Options.UseFont = True
        Me.nbi_sales_order_royalti.Caption = "Royalti"
        Me.nbi_sales_order_royalti.Enabled = False
        Me.nbi_sales_order_royalti.Hint = "Royalti"
        Me.nbi_sales_order_royalti.Name = "nbi_sales_order_royalti"
        Me.nbi_sales_order_royalti.SmallImageIndex = 1
        '
        'nbi_sales_order_periode_intensif_ds
        '
        Me.nbi_sales_order_periode_intensif_ds.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_periode_intensif_ds.Appearance.Options.UseFont = True
        Me.nbi_sales_order_periode_intensif_ds.Caption = "Direct Selling Periode Insentif"
        Me.nbi_sales_order_periode_intensif_ds.Enabled = False
        Me.nbi_sales_order_periode_intensif_ds.Hint = "Direct Selling Periode Insentif"
        Me.nbi_sales_order_periode_intensif_ds.Name = "nbi_sales_order_periode_intensif_ds"
        Me.nbi_sales_order_periode_intensif_ds.SmallImageIndex = 1
        '
        'nbi_sales_order_rule_ds
        '
        Me.nbi_sales_order_rule_ds.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_rule_ds.Appearance.Options.UseFont = True
        Me.nbi_sales_order_rule_ds.Caption = "Direct Selling Rule"
        Me.nbi_sales_order_rule_ds.Enabled = False
        Me.nbi_sales_order_rule_ds.Hint = "Direct Selling Rule"
        Me.nbi_sales_order_rule_ds.Name = "nbi_sales_order_rule_ds"
        Me.nbi_sales_order_rule_ds.SmallImageIndex = 1
        '
        'nbi_sales_order_intensif_ds
        '
        Me.nbi_sales_order_intensif_ds.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_intensif_ds.Appearance.Options.UseFont = True
        Me.nbi_sales_order_intensif_ds.Caption = "Direct Selling Insentif"
        Me.nbi_sales_order_intensif_ds.Enabled = False
        Me.nbi_sales_order_intensif_ds.Hint = "Direct Selling Insentif"
        Me.nbi_sales_order_intensif_ds.Name = "nbi_sales_order_intensif_ds"
        Me.nbi_sales_order_intensif_ds.SmallImageIndex = 1
        '
        'nbi_sales_order_periode_intensif_rs
        '
        Me.nbi_sales_order_periode_intensif_rs.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_periode_intensif_rs.Appearance.Options.UseFont = True
        Me.nbi_sales_order_periode_intensif_rs.Caption = "Regular Selling Periode Insentif"
        Me.nbi_sales_order_periode_intensif_rs.Enabled = False
        Me.nbi_sales_order_periode_intensif_rs.Hint = "Regular Selling Periode Insentif"
        Me.nbi_sales_order_periode_intensif_rs.Name = "nbi_sales_order_periode_intensif_rs"
        Me.nbi_sales_order_periode_intensif_rs.SmallImageIndex = 1
        '
        'nbi_sales_order_rule_rs
        '
        Me.nbi_sales_order_rule_rs.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_rule_rs.Appearance.Options.UseFont = True
        Me.nbi_sales_order_rule_rs.Caption = "Regular Selling Rule"
        Me.nbi_sales_order_rule_rs.Enabled = False
        Me.nbi_sales_order_rule_rs.Hint = "Regular Selling Rule"
        Me.nbi_sales_order_rule_rs.Name = "nbi_sales_order_rule_rs"
        Me.nbi_sales_order_rule_rs.SmallImageIndex = 1
        '
        'nbi_sales_order_cf_rs
        '
        Me.nbi_sales_order_cf_rs.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_cf_rs.Appearance.Options.UseFont = True
        Me.nbi_sales_order_cf_rs.Caption = "Regular Selling Control File"
        Me.nbi_sales_order_cf_rs.Enabled = False
        Me.nbi_sales_order_cf_rs.Hint = "Regular Selling Control File"
        Me.nbi_sales_order_cf_rs.Name = "nbi_sales_order_cf_rs"
        Me.nbi_sales_order_cf_rs.SmallImageIndex = 1
        '
        'nbi_sales_order_intensif_rs
        '
        Me.nbi_sales_order_intensif_rs.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Strikeout)
        Me.nbi_sales_order_intensif_rs.Appearance.Options.UseFont = True
        Me.nbi_sales_order_intensif_rs.Caption = "Regular Selling Insetif"
        Me.nbi_sales_order_intensif_rs.Enabled = False
        Me.nbi_sales_order_intensif_rs.Hint = "Regular Selling Insetif"
        Me.nbi_sales_order_intensif_rs.Name = "nbi_sales_order_intensif_rs"
        Me.nbi_sales_order_intensif_rs.SmallImageIndex = 1
        '
        'nbg_project
        '
        Me.nbg_project.Caption = "Project"
        Me.nbg_project.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_project_type), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_project_area), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_po_customer), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_project), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_boq), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_prj_boq_approval)})
        Me.nbg_project.Name = "nbg_project"
        '
        'nbi_prj_project_type
        '
        Me.nbi_prj_project_type.Caption = "Project Type"
        Me.nbi_prj_project_type.Hint = "Project Type"
        Me.nbi_prj_project_type.Name = "nbi_prj_project_type"
        Me.nbi_prj_project_type.SmallImageIndex = 1
        '
        'nbi_prj_project_area
        '
        Me.nbi_prj_project_area.Caption = "Project Area"
        Me.nbi_prj_project_area.Hint = "Project Area"
        Me.nbi_prj_project_area.Name = "nbi_prj_project_area"
        Me.nbi_prj_project_area.SmallImageIndex = 1
        '
        'nbi_prj_po_customer
        '
        Me.nbi_prj_po_customer.Caption = "PO Customer"
        Me.nbi_prj_po_customer.Hint = "PO Customer"
        Me.nbi_prj_po_customer.Name = "nbi_prj_po_customer"
        Me.nbi_prj_po_customer.SmallImageIndex = 1
        '
        'nbi_prj_project
        '
        Me.nbi_prj_project.Caption = "Project Maintenance"
        Me.nbi_prj_project.Hint = "Project Maintenance"
        Me.nbi_prj_project.Name = "nbi_prj_project"
        Me.nbi_prj_project.SmallImageIndex = 1
        '
        'nbi_prj_boq
        '
        Me.nbi_prj_boq.Caption = "Bill Of Quantity"
        Me.nbi_prj_boq.Hint = "Bill Of Quantity"
        Me.nbi_prj_boq.Name = "nbi_prj_boq"
        Me.nbi_prj_boq.SmallImageIndex = 1
        '
        'nbi_prj_boq_approval
        '
        Me.nbi_prj_boq_approval.Caption = "Bill Of Quantity Approval"
        Me.nbi_prj_boq_approval.Hint = "Bill Of Quantity Approval"
        Me.nbi_prj_boq_approval.Name = "nbi_prj_boq_approval"
        Me.nbi_prj_boq_approval.SmallImageIndex = 1
        '
        'dp_manufacturing
        '
        Me.dp_manufacturing.Controls.Add(Me.ControlContainer4)
        Me.dp_manufacturing.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill
        Me.dp_manufacturing.FloatSize = New System.Drawing.Size(200, 314)
        Me.dp_manufacturing.ID = New System.Guid("957dcbe6-6eaa-45a5-9cda-2edb02d4f27c")
        Me.dp_manufacturing.Location = New System.Drawing.Point(3, 25)
        Me.dp_manufacturing.Name = "dp_manufacturing"
        Me.dp_manufacturing.OriginalSize = New System.Drawing.Size(218, 485)
        Me.dp_manufacturing.Size = New System.Drawing.Size(218, 503)
        Me.dp_manufacturing.Text = "Manufacturing"
        '
        'ControlContainer4
        '
        Me.ControlContainer4.Controls.Add(Me.NavBarControl1)
        Me.ControlContainer4.Location = New System.Drawing.Point(0, 0)
        Me.ControlContainer4.Name = "ControlContainer4"
        Me.ControlContainer4.Size = New System.Drawing.Size(218, 503)
        Me.ControlContainer4.TabIndex = 0
        '
        'NavBarControl1
        '
        Me.NavBarControl1.ActiveGroup = Me.nbg_product_structure
        Me.NavBarControl1.ContentButtonHint = Nothing
        Me.NavBarControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NavBarControl1.Groups.AddRange(New DevExpress.XtraNavBar.NavBarGroup() {Me.nbg_man_master, Me.nbg_man_transaksi, Me.nbg_product_structure, Me.nbg_routing_work_center, Me.nbg_work_order})
        Me.NavBarControl1.Items.AddRange(New DevExpress.XtraNavBar.NavBarItem() {Me.nbi_man_bom, Me.nbi_man_producstructure, Me.nbi_man_pt_subtitute, Me.nbi_man_department, Me.nbi_man_workcenter, Me.nbi_man_work_order, Me.nbi_man_wo_component_issue, Me.nbi_man_wo_receipt, Me.nbi_man_machine, Me.nbi_man_routing, Me.nbi_man_workcenter_machine, Me.nbi_man_wo_release, Me.nbi_man_wo_maintenance, Me.nbi_man_wo_project, Me.nbi_man_wo_cost_element, Me.nbi_man_wo_CostSet, Me.nbi_man_wo_CostElementRealProject, Me.nbi_man_labour_feedback, Me.nbi_man_reason_reject, Me.nbi_man_reason_rework, Me.nbi_manu_prod_structure, Me.nbi_manu_prod_structure_report, Me.nbi_manu_where_in_used, Me.nbi_manu_materials_summary_report, Me.nbi_manu_simulated_picklist, Me.nbi_manu_item_subtitute, Me.nbi_manu_departement, Me.nbi_manu_work_center, Me.nbi_manu_tools, Me.nbi_manu_routing, Me.nbi_manu_work_order, Me.nbi_manu_wo_release, Me.nbi_manu_wo_comp_issue, Me.nbi_manu_wo_bill, Me.nbi_manu_wo_receipt, Me.nbi_manu_wip, Me.nbi_manu_trans_issue_wip})
        Me.NavBarControl1.Location = New System.Drawing.Point(0, 0)
        Me.NavBarControl1.Name = "NavBarControl1"
        Me.NavBarControl1.OptionsNavPane.ExpandedWidth = 194
        Me.NavBarControl1.Size = New System.Drawing.Size(218, 503)
        Me.NavBarControl1.SmallImages = Me.ImageList4
        Me.NavBarControl1.TabIndex = 0
        Me.NavBarControl1.Text = "NavBarControl1"
        '
        'nbg_product_structure
        '
        Me.nbg_product_structure.Caption = "Product Structure"
        Me.nbg_product_structure.Expanded = True
        Me.nbg_product_structure.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_prod_structure), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_prod_structure_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_where_in_used), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_materials_summary_report), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_simulated_picklist), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_item_subtitute)})
        Me.nbg_product_structure.Name = "nbg_product_structure"
        '
        'nbi_manu_prod_structure
        '
        Me.nbi_manu_prod_structure.Caption = "Product Structure"
        Me.nbi_manu_prod_structure.Hint = "Product Structure"
        Me.nbi_manu_prod_structure.Name = "nbi_manu_prod_structure"
        Me.nbi_manu_prod_structure.SmallImageIndex = 1
        '
        'nbi_manu_prod_structure_report
        '
        Me.nbi_manu_prod_structure_report.Caption = "Product Structure Report"
        Me.nbi_manu_prod_structure_report.Hint = "Product Structure Report"
        Me.nbi_manu_prod_structure_report.Name = "nbi_manu_prod_structure_report"
        Me.nbi_manu_prod_structure_report.SmallImageIndex = 1
        '
        'nbi_manu_where_in_used
        '
        Me.nbi_manu_where_in_used.Caption = "Where In Used Report"
        Me.nbi_manu_where_in_used.Hint = "Where In Used Report"
        Me.nbi_manu_where_in_used.Name = "nbi_manu_where_in_used"
        Me.nbi_manu_where_in_used.SmallImageIndex = 1
        '
        'nbi_manu_materials_summary_report
        '
        Me.nbi_manu_materials_summary_report.Caption = "Materials Summary Report"
        Me.nbi_manu_materials_summary_report.Hint = "Materials Summary Report"
        Me.nbi_manu_materials_summary_report.Name = "nbi_manu_materials_summary_report"
        Me.nbi_manu_materials_summary_report.SmallImageIndex = 1
        '
        'nbi_manu_simulated_picklist
        '
        Me.nbi_manu_simulated_picklist.Caption = "Simulated Picklist Report"
        Me.nbi_manu_simulated_picklist.Hint = "Simulated Picklist Report"
        Me.nbi_manu_simulated_picklist.Name = "nbi_manu_simulated_picklist"
        Me.nbi_manu_simulated_picklist.SmallImageIndex = 1
        '
        'nbi_manu_item_subtitute
        '
        Me.nbi_manu_item_subtitute.Caption = "Item Subtitute"
        Me.nbi_manu_item_subtitute.Hint = "Item Subtitute"
        Me.nbi_manu_item_subtitute.Name = "nbi_manu_item_subtitute"
        Me.nbi_manu_item_subtitute.SmallImageIndex = 1
        '
        'nbg_man_master
        '
        Me.nbg_man_master.Caption = "Master"
        Me.nbg_man_master.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_bom), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_producstructure), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_pt_subtitute), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_department), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_workcenter), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_machine), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_routing), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_workcenter_machine), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_CostSet), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_cost_element)})
        Me.nbg_man_master.Name = "nbg_man_master"
        Me.nbg_man_master.Visible = False
        '
        'nbi_man_bom
        '
        Me.nbi_man_bom.Caption = "Bill Of Material"
        Me.nbi_man_bom.Enabled = False
        Me.nbi_man_bom.Name = "nbi_man_bom"
        Me.nbi_man_bom.SmallImageIndex = 1
        '
        'nbi_man_producstructure
        '
        Me.nbi_man_producstructure.Caption = "Product Structure"
        Me.nbi_man_producstructure.Enabled = False
        Me.nbi_man_producstructure.Name = "nbi_man_producstructure"
        Me.nbi_man_producstructure.SmallImageIndex = 1
        '
        'nbi_man_pt_subtitute
        '
        Me.nbi_man_pt_subtitute.Caption = "Part Number Subtitute"
        Me.nbi_man_pt_subtitute.Enabled = False
        Me.nbi_man_pt_subtitute.Name = "nbi_man_pt_subtitute"
        Me.nbi_man_pt_subtitute.SmallImageIndex = 1
        '
        'nbi_man_department
        '
        Me.nbi_man_department.Caption = "Department"
        Me.nbi_man_department.Enabled = False
        Me.nbi_man_department.Name = "nbi_man_department"
        Me.nbi_man_department.SmallImageIndex = 1
        '
        'nbi_man_workcenter
        '
        Me.nbi_man_workcenter.Caption = "Work Center"
        Me.nbi_man_workcenter.Enabled = False
        Me.nbi_man_workcenter.Name = "nbi_man_workcenter"
        Me.nbi_man_workcenter.SmallImageIndex = 1
        '
        'nbi_man_machine
        '
        Me.nbi_man_machine.Caption = "Machine"
        Me.nbi_man_machine.Enabled = False
        Me.nbi_man_machine.Name = "nbi_man_machine"
        Me.nbi_man_machine.SmallImageIndex = 1
        '
        'nbi_man_routing
        '
        Me.nbi_man_routing.Caption = "Routing"
        Me.nbi_man_routing.Enabled = False
        Me.nbi_man_routing.Name = "nbi_man_routing"
        Me.nbi_man_routing.SmallImageIndex = 1
        '
        'nbi_man_workcenter_machine
        '
        Me.nbi_man_workcenter_machine.Caption = "Workcenter Machine"
        Me.nbi_man_workcenter_machine.Enabled = False
        Me.nbi_man_workcenter_machine.Name = "nbi_man_workcenter_machine"
        Me.nbi_man_workcenter_machine.SmallImageIndex = 1
        '
        'nbi_man_wo_CostSet
        '
        Me.nbi_man_wo_CostSet.Caption = "Cost Set"
        Me.nbi_man_wo_CostSet.Name = "nbi_man_wo_CostSet"
        Me.nbi_man_wo_CostSet.SmallImageIndex = 1
        '
        'nbi_man_wo_cost_element
        '
        Me.nbi_man_wo_cost_element.Caption = "Cost Element"
        Me.nbi_man_wo_cost_element.Name = "nbi_man_wo_cost_element"
        Me.nbi_man_wo_cost_element.SmallImageIndex = 1
        '
        'nbg_man_transaksi
        '
        Me.nbg_man_transaksi.Caption = "Work Order"
        Me.nbg_man_transaksi.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_project), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_work_order), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_release), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_maintenance), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_component_issue), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_receipt), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_wo_CostElementRealProject), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_labour_feedback), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_reason_reject), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_man_reason_rework)})
        Me.nbg_man_transaksi.Name = "nbg_man_transaksi"
        Me.nbg_man_transaksi.Visible = False
        '
        'nbi_man_wo_project
        '
        Me.nbi_man_wo_project.Caption = "Project"
        Me.nbi_man_wo_project.Name = "nbi_man_wo_project"
        Me.nbi_man_wo_project.SmallImageIndex = 1
        '
        'nbi_man_work_order
        '
        Me.nbi_man_work_order.Caption = "Work Order"
        Me.nbi_man_work_order.Enabled = False
        Me.nbi_man_work_order.Name = "nbi_man_work_order"
        Me.nbi_man_work_order.SmallImageIndex = 1
        '
        'nbi_man_wo_release
        '
        Me.nbi_man_wo_release.Caption = "Work Order Release"
        Me.nbi_man_wo_release.Enabled = False
        Me.nbi_man_wo_release.Name = "nbi_man_wo_release"
        Me.nbi_man_wo_release.SmallImageIndex = 1
        '
        'nbi_man_wo_maintenance
        '
        Me.nbi_man_wo_maintenance.Caption = "Work Order Maintenance"
        Me.nbi_man_wo_maintenance.Enabled = False
        Me.nbi_man_wo_maintenance.Name = "nbi_man_wo_maintenance"
        Me.nbi_man_wo_maintenance.SmallImageIndex = 1
        '
        'nbi_man_wo_component_issue
        '
        Me.nbi_man_wo_component_issue.Caption = "Component Issue"
        Me.nbi_man_wo_component_issue.Enabled = False
        Me.nbi_man_wo_component_issue.Name = "nbi_man_wo_component_issue"
        Me.nbi_man_wo_component_issue.SmallImageIndex = 1
        '
        'nbi_man_wo_receipt
        '
        Me.nbi_man_wo_receipt.Caption = "WO Receipt"
        Me.nbi_man_wo_receipt.Enabled = False
        Me.nbi_man_wo_receipt.Name = "nbi_man_wo_receipt"
        Me.nbi_man_wo_receipt.SmallImageIndex = 1
        '
        'nbi_man_wo_CostElementRealProject
        '
        Me.nbi_man_wo_CostElementRealProject.Caption = "Cost Element Real Project"
        Me.nbi_man_wo_CostElementRealProject.Name = "nbi_man_wo_CostElementRealProject"
        Me.nbi_man_wo_CostElementRealProject.SmallImageIndex = 1
        '
        'nbi_man_labour_feedback
        '
        Me.nbi_man_labour_feedback.Caption = "Labour Feedback"
        Me.nbi_man_labour_feedback.Name = "nbi_man_labour_feedback"
        Me.nbi_man_labour_feedback.SmallImageIndex = 1
        '
        'nbi_man_reason_reject
        '
        Me.nbi_man_reason_reject.Caption = "Reason Reject"
        Me.nbi_man_reason_reject.Name = "nbi_man_reason_reject"
        Me.nbi_man_reason_reject.SmallImageIndex = 1
        '
        'nbi_man_reason_rework
        '
        Me.nbi_man_reason_rework.Caption = "Reason Rework"
        Me.nbi_man_reason_rework.Name = "nbi_man_reason_rework"
        Me.nbi_man_reason_rework.SmallImageIndex = 1
        '
        'nbg_routing_work_center
        '
        Me.nbg_routing_work_center.Caption = "Routing / Work Center"
        Me.nbg_routing_work_center.Expanded = True
        Me.nbg_routing_work_center.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_departement), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_work_center), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_tools), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_routing)})
        Me.nbg_routing_work_center.Name = "nbg_routing_work_center"
        '
        'nbi_manu_departement
        '
        Me.nbi_manu_departement.Caption = "Departement"
        Me.nbi_manu_departement.Hint = "Departement"
        Me.nbi_manu_departement.Name = "nbi_manu_departement"
        Me.nbi_manu_departement.SmallImageIndex = 1
        '
        'nbi_manu_work_center
        '
        Me.nbi_manu_work_center.Caption = "Work Center"
        Me.nbi_manu_work_center.Hint = "Work Center"
        Me.nbi_manu_work_center.Name = "nbi_manu_work_center"
        Me.nbi_manu_work_center.SmallImageIndex = 1
        '
        'nbi_manu_tools
        '
        Me.nbi_manu_tools.Caption = "Tools"
        Me.nbi_manu_tools.Hint = "Tools"
        Me.nbi_manu_tools.Name = "nbi_manu_tools"
        Me.nbi_manu_tools.SmallImageIndex = 1
        '
        'nbi_manu_routing
        '
        Me.nbi_manu_routing.Caption = "Routing"
        Me.nbi_manu_routing.Hint = "Routing"
        Me.nbi_manu_routing.Name = "nbi_manu_routing"
        Me.nbi_manu_routing.SmallImageIndex = 1
        '
        'nbg_work_order
        '
        Me.nbg_work_order.Caption = "Work Order"
        Me.nbg_work_order.Expanded = True
        Me.nbg_work_order.ItemLinks.AddRange(New DevExpress.XtraNavBar.NavBarItemLink() {New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_work_order), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_wo_release), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_wo_comp_issue), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_wo_bill), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_wo_receipt), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_wip), New DevExpress.XtraNavBar.NavBarItemLink(Me.nbi_manu_trans_issue_wip)})
        Me.nbg_work_order.Name = "nbg_work_order"
        '
        'nbi_manu_work_order
        '
        Me.nbi_manu_work_order.Caption = "Work Order"
        Me.nbi_manu_work_order.Hint = "Work Order"
        Me.nbi_manu_work_order.Name = "nbi_manu_work_order"
        Me.nbi_manu_work_order.SmallImageIndex = 1
        '
        'nbi_manu_wo_release
        '
        Me.nbi_manu_wo_release.Caption = "Work Order Release"
        Me.nbi_manu_wo_release.Hint = "Work Order Release"
        Me.nbi_manu_wo_release.Name = "nbi_manu_wo_release"
        Me.nbi_manu_wo_release.SmallImageIndex = 1
        '
        'nbi_manu_wo_comp_issue
        '
        Me.nbi_manu_wo_comp_issue.Caption = "Work Order Component Issue"
        Me.nbi_manu_wo_comp_issue.Hint = "Work Order Component Issue"
        Me.nbi_manu_wo_comp_issue.Name = "nbi_manu_wo_comp_issue"
        Me.nbi_manu_wo_comp_issue.SmallImageIndex = 1
        '
        'nbi_manu_wo_bill
        '
        Me.nbi_manu_wo_bill.Caption = "Work Order Bill Maintenance"
        Me.nbi_manu_wo_bill.Hint = "Work Order Bill Maintenance"
        Me.nbi_manu_wo_bill.Name = "nbi_manu_wo_bill"
        Me.nbi_manu_wo_bill.SmallImageIndex = 1
        '
        'nbi_manu_wo_receipt
        '
        Me.nbi_manu_wo_receipt.Caption = "Work Order Receipt"
        Me.nbi_manu_wo_receipt.Hint = "Work Order Receipt"
        Me.nbi_manu_wo_receipt.Name = "nbi_manu_wo_receipt"
        Me.nbi_manu_wo_receipt.SmallImageIndex = 1
        '
        'nbi_manu_wip
        '
        Me.nbi_manu_wip.Caption = "Work In Progress"
        Me.nbi_manu_wip.Name = "nbi_manu_wip"
        Me.nbi_manu_wip.SmallImageIndex = 1
        '
        'nbi_manu_trans_issue_wip
        '
        Me.nbi_manu_trans_issue_wip.Caption = "Inv. Transfer Issue WIP"
        Me.nbi_manu_trans_issue_wip.Name = "nbi_manu_trans_issue_wip"
        '
        'bbi_manufacturing
        '
        Me.bbi_manufacturing.Caption = "Manufacturing"
        Me.bbi_manufacturing.Id = 174
        Me.bbi_manufacturing.Name = "bbi_manufacturing"
        '
        'bci_manufacturing
        '
        Me.bci_manufacturing.Caption = "Manufacturing"
        Me.bci_manufacturing.Id = 174
        Me.bci_manufacturing.Name = "bci_manufacturing"
        '
        'bci_manufacture
        '
        Me.bci_manufacture.Caption = "Manufacturing"
        Me.bci_manufacture.Id = 174
        Me.bci_manufacture.Name = "bci_manufacture"
        '
        'bbi_user_history
        '
        Me.bbi_user_history.Caption = "User History"
        Me.bbi_user_history.Id = 175
        Me.bbi_user_history.ImageIndex = 12
        Me.bbi_user_history.Name = "bbi_user_history"
        '
        'DataTable1TableAdapter
        '
        'Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'DataTable1TableAdapter1
        '
        'Me.DataTable1TableAdapter1.ClearBeforeFill = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'BarStaticItem1
        '
        Me.BarStaticItem1.Caption = "BarStaticItem1"
        Me.BarStaticItem1.Id = 182
        Me.BarStaticItem1.Name = "BarStaticItem1"
        Me.BarStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near
        '
        'BarLargeButtonItem1
        '
        Me.BarLargeButtonItem1.Caption = "Update"
        Me.BarLargeButtonItem1.Id = 185
        Me.BarLargeButtonItem1.LargeImageIndex = 10
        Me.BarLargeButtonItem1.Name = "BarLargeButtonItem1"
        '
        'FMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1258, 691)
        Me.Controls.Add(Me.panelContainer1)
        Me.Controls.Add(Me.DockPanel1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FMainMenu"
        Me.Controls.SetChildIndex(Me.DockPanel1, 0)
        Me.Controls.SetChildIndex(Me.panelContainer1, 0)
        CType(Me.BarAndDockingController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabbedMdiManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.ControlContainer5.ResumeLayout(False)
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceExpandCollaps.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelContainer1.ResumeLayout(False)
        Me.dp_financial.ResumeLayout(False)
        Me.ControlContainer2.ResumeLayout(False)
        CType(Me.nbc_financial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_master_data.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.nbc_master_data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_distribution.ResumeLayout(False)
        Me.ControlContainer1.ResumeLayout(False)
        CType(Me.nbc_distribution, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_sales_order.ResumeLayout(False)
        Me.ControlContainer3.ResumeLayout(False)
        CType(Me.nbc_sales_order, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_manufacturing.ResumeLayout(False)
        Me.ControlContainer4.ResumeLayout(False)
        CType(Me.NavBarControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_master_data As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents nbc_master_data As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nbg_master_data_company As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents ImageList5 As System.Windows.Forms.ImageList
    Friend WithEvents ImageList4 As System.Windows.Forms.ImageList
    Friend WithEvents nbi_master_data_domain As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_master_data_agama As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_org_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_organization As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_organization_structure As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_org_struc_detail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_trn_req As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents panelContainer1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents dp_distribution As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents nbc_distribution As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nbg_requistion As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_dist_req_mstr As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_organizationtree As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_purchase_order As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_dist_purchase_order As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_item_site As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbg_address_taxes As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_mst_is_partnumber As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_inventorystatus As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_itemstatus As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_location As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_productline As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_unitmeasure As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_location_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_location_category As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_warehouse As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_warehouse_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_warehouse_category As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_part_number_group As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_taxclass As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_site As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner_group As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_comp_company_address As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_comp_employee As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_comp_tran_status As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_emp_transaction As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner_bank As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner_cp As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner_address As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_address_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_cp_function As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_fin_credit_terms As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_tax_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_tax_rate As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_receipt As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents dp_financial As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer2 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents nbc_financial As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nbg_ap As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbg_gl As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_fin_gl_standard_transaction As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_unposted_transaction As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_transaction_post As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_return As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_inventory_control As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_inv_inventory_detail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_reason_code_return As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_payment As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_multiple_currency As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_fin_mc_exchange_rate As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_mc_currency As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_entity As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_glcalendar As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_account As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_subaccount As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_cost_center As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_mc_bank As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_project As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents dp_sales_order As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer3 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents nbc_sales_order As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nbg_sales_order As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_so_promo As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_pricelist As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_prodline_location As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_year_end As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_forex As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_so As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_receipts As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_issues As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_issues As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_shipments As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_receipts As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_returns As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_sales_program As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_pay_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_payment_methode As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_general_ledger_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_profit_loss_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_trial_balance_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_balance_sheet As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_cash_flow As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_reason_code As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_inventory_begining_balance As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_adjusment_plus As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_adjustment_minus As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_opening_balance As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_month_end As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_fin_gl_report As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbg_ar As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_fin_ar_dc_memo As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_payment As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_order_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_receipt_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_mstr_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_return_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_issues_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_receipts_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_shipments_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_returns_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dc_memo_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_intensif_ds As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_cycle_count As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_periode_intensif_ds As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_rule_ds As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_area As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_royalti_rule As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_sales_struktur As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_company_position As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_unit_conversion As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_so_insentif_royalti As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_sales_order_periode_intensif_rs As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_rule_rs As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_cf_rs As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_intensif_rs As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_transfer_issue As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_transfer_issue_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_transfer_receipt As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_transfer_receipt_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dc_memo_konsiyasi_print_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_faktur_pajak As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_faktur_pajak_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_faktur_pajak_transaction_code As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_request As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_royalti_periode As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_royalti As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_request_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_entity_group As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_tax As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_inventory_detail_2 As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_at_partner_all As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_so_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_faktur_penjualan_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_company_dok_aprv As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_company_conf As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_manual_allocation As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_order_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_receipt_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_inventory_by_eff_date As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_purchase_order_print_no_cost As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_so_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_company_routing_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_mstr_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_so_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_req_transfer_issue_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_item_site_cost As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_request_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_issues_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_pricelist_header As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_pricelist_detail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_pricelist_copy As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_sales_order_so_credit_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_report_by_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_report_by_aging As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_report_by_top As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_report_by_unvouchered As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dc_memo_detail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_payment_detail As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dc_memo_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_report_by_aging As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_report_by_top As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_report_pending_invoice As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_receipts_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_issues_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_budget As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_fin_bgt_periode As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bgt_generate As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bgt_maintenance As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bgt_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bgt_cross As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bgt_cross_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_cost_center_user As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_gl_cost_center_account As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_cash_in_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_plSetting As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_bsSetting As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_cfSetting As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_transfer_issues_return As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents dp_manufacturing As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer4 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents NavBarControl1 As DevExpress.XtraNavBar.NavBarControl
    Friend WithEvents nbg_man_master As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbg_man_transaksi As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_man_bom As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_producstructure As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_pt_subtitute As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_department As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_workcenter As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_work_order As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_component_issue As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_receipt As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_machine As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_routing As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents bbi_manufacturing As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bci_manufacturing As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents bci_manufacture As DevExpress.XtraBars.BarCheckItem
    Friend WithEvents nbi_man_workcenter_machine As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_release As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_maintenance As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_approval_tax As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_mst_is_approval_accounting As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_project As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_cost_element As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_CostSet As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_wo_CostElementRealProject As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents bbi_user_history As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents nbi_man_labour_feedback As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_reason_reject As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_man_reason_rework As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_dist_inv_history As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_cash_management As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_cash_transfer As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_cash_in As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_cash_out As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_cash_bank_reconciliation As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dbcr_balance_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ap_voucher_balance_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_isues_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_receipts_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_so_retur_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_so_ship_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_faktur_pajak_sign As DevExpress.XtraNavBar.NavBarItem
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_cash_outTableAdapters.DataTable1TableAdapter
    'Friend WithEvents DataTable1TableAdapter1 As sygma_solution_system.ds_cash_inTableAdapters.DataTable1TableAdapter
    Friend WithEvents nbi_fin_ar_faktur_pajak_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_report_by_aging_sdi As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_add_account_glbal As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents nbi_so_faktur_pajak_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_ar_dc_memo_detail_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_fin_payment_ar_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_purchase_order_filem As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_purchase_order_filem_print As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_so_ar_fp_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_adjusment_plus_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_inv_adjustment_minus_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_product_structure As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_manu_prod_structure As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_routing_work_center As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbg_work_order As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_manu_prod_structure_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_where_in_used As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_materials_summary_report As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_simulated_picklist As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_item_subtitute As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_departement As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_work_center As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_tools As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_routing As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_work_order As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_wo_release As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_wo_comp_issue As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_wo_bill As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_wo_receipt As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_project As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_prj_project As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_prj_po_customer As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_prj_project_type As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_prj_project_area As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_prj_boq As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_prj_boq_approval As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_pr_boq As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbg_taxation As DevExpress.XtraNavBar.NavBarGroup
    Friend WithEvents nbi_inv_report_detail_wip As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_wip As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents nbi_manu_trans_issue_wip As DevExpress.XtraNavBar.NavBarItem
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer5 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents menudesc As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ceExpandCollaps As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents btSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchMenu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btFindNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents menuseq As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents BarStaticItem1 As DevExpress.XtraBars.BarStaticItem
    Friend WithEvents BarLargeButtonItem1 As DevExpress.XtraBars.BarLargeButtonItem

End Class
