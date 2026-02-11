Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports System.IO

Public Class FPurchaseOrder
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr, _pod_oid As String
    Dim ds_edit As DataSet
    Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim _conf_budget As String
    Dim _conf_value As String
    Dim _cost_center_pr_po As String = ""
    Dim dt_edit_doc As New DataTable
    Dim mf As New master_new.ModFunction

#Region "Seting Awal"
    Private Sub FPurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_purchase_order")
        _cost_center_pr_po = func_coll.get_conf_file("cost_center_pr_po")

        If _conf_value = "1" Then
            xtp_wf.PageVisible = True
        Else
            xtp_wf.PageVisible = False
        End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        po_en_id.Properties.DataSource = dt_bantu
        po_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        po_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        po_en_id.ItemIndex = 0

        If func_coll.get_conf_file("wf_purchase_order") = 0 Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            po_tran_id.Properties.DataSource = dt_bantu
            po_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            po_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            po_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            po_tran_id.Properties.DataSource = dt_bantu
            po_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            po_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            po_tran_id.ItemIndex = 0
        End If

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_tran_mstr("po_mstr"))
        'po_tran_id.Properties.DataSource = dt_bantu
        'po_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'po_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        'po_tran_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        po_cu_id.Properties.DataSource = dt_bantu
        po_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        po_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        po_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_po_status_cash())
        po_status_cash.Properties.DataSource = dt_bantu
        po_status_cash.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        po_status_cash.Properties.ValueMember = dt_bantu.Columns("value").ToString
        po_status_cash.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", po_en_id.EditValue))
        po_ptnr_id.Properties.DataSource = dt_bantu
        po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        po_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cmaddr_mstr", po_en_id.EditValue))
        po_cmaddr_id.Properties.DataSource = dt_bantu
        po_cmaddr_id.Properties.DisplayMember = dt_bantu.Columns("cmaddr_name").ToString
        po_cmaddr_id.Properties.ValueMember = dt_bantu.Columns("cmaddr_id").ToString
        po_cmaddr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("sb_mstr", po_en_id.EditValue))
        po_sb_id.Properties.DataSource = dt_bantu
        po_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        po_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        po_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_mstr", po_en_id.EditValue))
        po_cc_id.Properties.DataSource = dt_bantu
        po_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        po_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        po_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", po_en_id.EditValue))
        po_si_id.Properties.DataSource = dt_bantu
        po_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        po_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        po_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pjc_mstr", po_en_id.EditValue))
        po_pjc_id.Properties.DataSource = dt_bantu
        po_pjc_id.Properties.DisplayMember = dt_bantu.Columns("pjc_desc").ToString
        po_pjc_id.Properties.ValueMember = dt_bantu.Columns("pjc_id").ToString
        po_pjc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(po_en_id.EditValue))
        po_credit_term.Properties.DataSource = dt_bantu
        po_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        po_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        po_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr(po_en_id.EditValue))
        po_tax_class.Properties.DataSource = dt_bantu
        po_tax_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        po_tax_class.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        po_tax_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(po_en_id.EditValue, po_cu_id.EditValue))
        po_bk_id.Properties.DataSource = dt_bantu
        po_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        po_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        po_bk_id.ItemIndex = 0
    End Sub

    Private Sub po_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles po_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub po_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles po_cu_id.EditValueChanged
        Dim _po_exc_rate As Double
        If po_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _po_exc_rate = func_data.get_exchange_rate(po_cu_id.EditValue, po_date.DateTime)
            If _po_exc_rate = 1 Then
                po_exc_rate.EditValue = 0
            Else
                po_exc_rate.EditValue = _po_exc_rate
            End If
        Else
            po_exc_rate.EditValue = 1
        End If

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(po_en_id.EditValue, po_cu_id.EditValue))
        po_bk_id.Properties.DataSource = dt_bantu
        po_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        po_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        po_bk_id.ItemIndex = 0

    End Sub

    Private Sub po_is_internal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles po_is_internal.EditValueChanged
        'Dim _po_exc_rate As Double
        'If po_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
        '    _po_exc_rate = func_data.get_exchange_rate(po_cu_id.EditValue, po_date.DateTime)
        '    If _po_exc_rate = 1 Then
        '        po_exc_rate.EditValue = 0
        '    Else
        '        po_exc_rate.EditValue = _po_exc_rate
        '    End If
        'Else
        '    po_exc_rate.EditValue = 1
        'End If

        If po_is_internal.Checked = True Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_data("ptnr_mstr_vend_internal", po_en_id.EditValue))
            po_ptnr_id.Properties.DataSource = dt_bantu
            po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
            po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
            po_ptnr_id.ItemIndex = 0
        Else

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_data("ptnr_mstr_vend", po_en_id.EditValue))
            po_ptnr_id.Properties.DataSource = dt_bantu
            po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
            po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
            po_ptnr_id.ItemIndex = 0
        End If

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "po_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Parent", "po_parent_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier ID", "po_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Order Date", "po_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Need Date", "po_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "po_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "po_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Internal Use", "po_is_internal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "po_credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "po_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Taxable", "po_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "po_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "po_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Date", "po_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Total", "po_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "po_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "po_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "After Tax", "po_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ext. Total", "po_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPN", "po_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPH", "po_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. After Tax", "po_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cash/Credit", "po_status_cash", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "po_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "pod_oid", False)
        add_column(gv_detail, "pod_po_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Requisition", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Is Memo", "pod_memo", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Receive", "pod_qty_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Child", "pod_qty_child", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Outstanding", "pod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "pod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Taxable", "pod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "pod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "pod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Need Date", "pod_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "pod_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "pod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty * Cost", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Status", "pod_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN", "pod_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "PPH", "pod_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column(gv_detail_comment, "cmt_oid", False)
        add_column(gv_detail_comment, "cmt_dom_id", False)
        add_column(gv_detail_comment, "cmt_table", False)
        add_column(gv_detail_comment, "cmt_code", False)
        add_column(gv_detail_comment, "cmt_seq", False)
        add_column(gv_detail_comment, "Type", False)
        add_column_copy(gv_detail_comment, "Part Description", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_comment, "Comment", "cmt_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_comment, "cmt_ref_oid", False)
        add_column(gv_detail_comment, "cmt_ref_code", False)

        add_column(gv_edit, "pod_oid", False)
        add_column(gv_edit, "pod_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_reqd_oid", False)
        add_column(gv_edit, "Requisition", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Is Memo", "pod_memo", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "End User", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "pod_qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "pod_um", False)
        add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column(gv_edit, "pod_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_pjc_id", False)
        add_column(gv_edit, "Project", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "Taxable", "pod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "pod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_tax_class", False)
        add_column(gv_edit, "Tax Class", "pod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "Need Date", "pod_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_edit(gv_edit, "Due Date", "pod_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "pod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty * Cost", "pod_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "pt_type", False)

        add_column(gv_comment, "cmt_oid", False)
        add_column(gv_comment, "cmt_dom_id", False)
        add_column(gv_comment, "cmt_table", False)
        add_column(gv_comment, "cmt_code", False)
        add_column(gv_comment, "cmt_seq", False)
        add_column(gv_comment, "Type", False)
        add_column_edit(gv_comment, "Comment", "cmt_comment", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_comment, "Ref Detail", "cmt_ref_oid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_comment, "cmt_ref_oid", False)
        add_column(gv_comment, "cmt_ref_code", False)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "dd/MM/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_receive, "Date", "podtr_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_edit(gv_receive, "Percent", "podtr_qty_percent", DevExpress.Utils.HorzAlignment.Far, init_te_repo("percent"))
        add_column_edit(gv_receive, "Qty", "podtr_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_payment, "Date", "podtp_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_edit(gv_payment, "Percent", "podtp_amount_percent", DevExpress.Utils.HorzAlignment.Far, init_te_repo("percent"))
        add_column_edit(gv_payment, "Amount", "podtp_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_detail_receive, "podtr_po_oid", False)
        add_column_copy(gv_detail_receive, "Date", "podtr_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail_receive, "Percent", "podtr_qty_percent", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_receive, "Qty", "podtr_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_payment, "podtp_po_oid", False)
        add_column_copy(gv_detail_payment, "Date", "podtp_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_detail_payment, "Percent", "podtp_amount_percent", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail_payment, "Amount", "podtp_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_ap, "app_po_oid", False)
        add_column_copy(gv_ap, "Voucher Date", "ap_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_ap, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_ap, "Payment Date", "appay_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_ap, "Payment Number", "appay_code", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_edit_doc, "File", "podd_file", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_doc, "File", "podd_file", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_doc, "Seq", "podd_seq", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.po_mstr.po_oid, " _
                    & "  public.po_mstr.po_dom_id, " _
                    & "  public.po_mstr.po_en_id, " _
                    & "  public.po_mstr.po_upd_date, " _
                    & "  public.po_mstr.po_upd_by, " _
                    & "  public.po_mstr.po_add_date, " _
                    & "  public.po_mstr.po_add_by, " _
                    & "  public.po_mstr.po_code, " _
                    & "  public.po_mstr.po_ptnr_id, " _
                    & "  public.po_mstr.po_cmaddr_id, " _
                    & "  public.po_mstr.po_date, " _
                    & "  public.po_mstr.po_need_date, " _
                    & "  public.po_mstr.po_due_date, " _
                    & "  public.po_mstr.po_rmks, " _
                    & "  public.po_mstr.po_sb_id, " _
                    & "  public.po_mstr.po_cc_id, " _
                    & "  public.po_mstr.po_si_id, " _
                    & "  public.po_mstr.po_pjc_id, " _
                    & "  public.po_mstr.po_is_internal, " _
                    & "  public.po_mstr.po_close_date, " _
                    & "  public.po_mstr.po_total, " _
                    & "  public.po_mstr.po_tran_id, " _
                    & "  public.po_mstr.po_trans_id, " _
                    & "  public.po_mstr.po_trans_rmks, " _
                    & "  public.po_mstr.po_current_route, " _
                    & "  public.po_mstr.po_next_route, " _
                    & "  public.po_mstr.po_dt, " _
                    & "  public.ptnr_mstr.ptnr_name, public.ptnr_mstr.ptnr_code, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  po_status_cash, " _
                    & "  public.pjc_mstr.pjc_desc,po_parent_oid,po_parent_code, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.po_mstr.po_bk_id, " _
                    & "  public.bk_mstr.bk_code, " _
                    & "  public.sb_mstr.sb_desc, po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                    & "  po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name , " _
                    & "  public.cc_mstr.cc_desc, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                    & "  po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                    & "  po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext " _
                    & "FROM " _
                    & "  public.po_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.bk_mstr ON (public.po_mstr.po_bk_id = public.bk_mstr.bk_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.po_mstr.po_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.po_mstr.po_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.po_mstr.po_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                    & "  INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                    & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class " _
                    & " where coalesce(po_film,'N')='N' and po_is_internal <> 'Y' and po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and po_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If TxtPRDetail.Text.Length > 0 Then
            get_sequel = "SELECT distinct  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.po_mstr.po_oid, " _
                    & "  public.po_mstr.po_dom_id, " _
                    & "  public.po_mstr.po_en_id, " _
                    & "  public.po_mstr.po_upd_date, " _
                    & "  public.po_mstr.po_upd_by, " _
                    & "  public.po_mstr.po_add_date, " _
                    & "  public.po_mstr.po_add_by, " _
                    & "  public.po_mstr.po_code, " _
                    & "  public.po_mstr.po_ptnr_id, " _
                    & "  public.po_mstr.po_cmaddr_id, " _
                    & "  public.po_mstr.po_date, " _
                    & "  public.po_mstr.po_need_date, " _
                    & "  public.po_mstr.po_due_date, " _
                    & "  public.po_mstr.po_rmks, " _
                    & "  public.po_mstr.po_sb_id, " _
                    & "  public.po_mstr.po_cc_id, " _
                    & "  public.po_mstr.po_si_id, " _
                    & "  public.po_mstr.po_pjc_id, " _
                    & "  public.po_mstr.po_is_internal, " _
                    & "  public.po_mstr.po_close_date, " _
                    & "  public.po_mstr.po_total, " _
                    & "  public.po_mstr.po_tran_id, " _
                    & "  public.po_mstr.po_trans_id, " _
                    & "  public.po_mstr.po_trans_rmks, " _
                    & "  public.po_mstr.po_current_route, " _
                    & "  public.po_mstr.po_next_route, " _
                    & "  public.po_mstr.po_dt, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  po_status_cash, " _
                    & "  public.pjc_mstr.pjc_desc, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.po_mstr.po_bk_id, " _
                    & "  public.bk_mstr.bk_code, " _
                    & "  public.sb_mstr.sb_desc, po_credit_term, po_taxable, po_tax_class, po_tax_inc, po_total_ppn, po_total_pph, " _
                    & "  po_cu_id, po_exc_rate, cu_name, creditterm_mstr.code_name as po_credit_term_name, taxclass_mstr.code_name as po_tax_class_name , " _
                    & "  public.cc_mstr.cc_desc, (po_total + po_total_ppn + po_total_pph) as po_total_after_tax, " _
                    & "  po_exc_rate * po_total as po_total_ext,  po_exc_rate * po_total_ppn as po_total_ppn_ext, " _
                    & "  po_exc_rate * po_total_pph as po_total_pph_ext,  po_exc_rate * (po_total + po_total_ppn + po_total_pph) as po_total_after_tax_ext " _
                    & "FROM " _
                    & "  public.po_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.po_mstr.po_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.bk_mstr ON (public.po_mstr.po_bk_id = public.bk_mstr.bk_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.po_mstr.po_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.po_mstr.po_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.po_mstr.po_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.po_mstr.po_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.po_mstr.po_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.po_mstr.po_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.tran_mstr ON (public.po_mstr.po_tran_id = public.tran_mstr.tran_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.po_mstr.po_cu_id = public.cu_mstr.cu_id) " _
                    & "  INNER JOIN public.code_mstr as creditterm_mstr ON (public.po_mstr.po_credit_term = creditterm_mstr.code_id) " _
                    & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.po_mstr.po_tax_class " _
                    & "  INNER JOIN public.pod_det ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                    & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
                    & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                    & " where  coalesce(po_film,'N')='N' and po_is_internal <> 'Y' and po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and po_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and  req_mstr_relation.req_code ='" & TxtPRDetail.Text & "'"
        End If

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
            & "  public.pod_det.pod_oid, " _
            & "  public.pod_det.pod_dom_id, " _
            & "  public.pod_det.pod_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.pod_det.pod_add_by, " _
            & "  public.pod_det.pod_add_date, " _
            & "  public.pod_det.pod_upd_by, " _
            & "  public.pod_det.pod_upd_date, " _
            & "  public.pod_det.pod_po_oid, " _
            & "  public.pod_det.pod_seq, " _
            & "  public.pod_det.pod_reqd_oid, " _
            & "  req_mstr_relation.req_code as req_code_relation, " _
            & "  public.pod_det.pod_si_id, " _
            & "  public.si_mstr.si_desc,pod_qty_child, pod_pod_parent_oid," _
            & "  public.pod_det.pod_pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  public.pod_det.pod_pt_desc1, " _
            & "  public.pod_det.pod_pt_desc2, " _
            & "  public.pod_det.pod_memo, " _
            & "  public.pod_det.pod_rmks, " _
            & "  public.pod_det.pod_end_user, " _
            & "  public.pod_det.pod_qty, " _
            & "  public.pod_det.pod_qty_receive, " _
            & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
            & "  public.pod_det.pod_qty_invoice, " _
            & "  public.pod_det.pod_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.pod_det.pod_cost, " _
            & "  public.pod_det.pod_disc, " _
            & "  public.pod_det.pod_need_date, " _
            & "  public.pod_det.pod_due_date, " _
            & "  public.pod_det.pod_um_conv, " _
            & "  public.pod_det.pod_qty_real, " _
            & "  public.pod_det.pod_pt_class, " _
            & "  public.pod_det.pod_status, " _
            & "  public.pod_det.pod_dt, " _
            & "  public.pod_det.pod_sb_id,pod_ppn,pod_pph, " _
            & "  sb_desc, " _
            & "  public.pod_det.pod_cc_id, " _
            & "  cc_desc, " _
            & "  loc_desc, " _
            & "  public.pod_det.pod_pjc_id, " _
            & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
            & "  taxclass_mstr.code_name as pod_tax_class_name " _
            & "  FROM " _
            & "  public.pod_det " _
            & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
            & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
            & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
            & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
            & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
            & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
            & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
            & "  where po_mstr.po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and po_mstr.po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail, "detail")

        'load_data_detail(sql, gc_detail, "detail")

        Dim row As Integer = BindingContext(ds.Tables(0)).Position
        Dim ssql As String
        ssql = "SELECT  " _
                & "  podd_file, " _
                & "  podd_seq " _
                & "FROM  " _
                & "  public.podd_doc where podd_po_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("po_oid").ToString) _
                & " order by podd_seq"


        gc_detail_doc.DataSource = master_new.PGSqlConn.GetTableData(ssql)
        gv_detail_doc.BestFitColumns()


        Try
            ds.Tables("comment").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cmt_oid, " _
            & "  cmt_dom_id, " _
            & "  cmt_table, " _
            & "  cmt_code, " _
            & "  cmt_seq, " _
            & "  cmt_type, " _
            & "  cmt_ref_oid, " _
            & "  cmt_ref_code, " _
            & "  cmt_comment, " _
            & "  pod_pt_desc1, " _
            & "  cmt_dt " _
            & "FROM  " _
            & "  public.cmt_mstr " _
            & "  INNER JOIN public.pod_det ON (public.pod_det.pod_oid = public.cmt_mstr.cmt_ref_oid) " _
            & "  where cmt_mstr.cmt_table = 'Purchase Order'"

        load_data_detail(sql, gc_detail_comment, "detail_comment")

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
                  " inner join po_mstr on po_code = wf_ref_code " _
                & " where po_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and po_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and po_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "

            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

        End If

        Try
            ds.Tables("detail_receive").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.podtr_oid, " _
            & "  a.podtr_po_oid, " _
            & "  a.podtr_seq, " _
            & "  a.podtr_date, " _
            & "  a.podtr_qty,podtr_qty_percent " _
            & "FROM " _
            & "  public.pod_term_receive a " _
            & "  INNER JOIN public.po_mstr ON (podtr_po_oid = po_oid) " _
            & "WHERE " _
            & " po_mstr.po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and po_mstr.po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
            & "ORDER BY " _
            & "  a.podtr_seq"

        load_data_detail(sql, gc_detail_receive, "detail_receive")



        Try
            ds.Tables("detail_payment").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.podtp_oid, " _
            & "  a.podtp_po_oid, " _
            & "  a.podtp_seq, " _
            & "  a.podtp_date, " _
            & "  a.podtp_amount,podtp_amount_percent " _
            & "FROM " _
            & "  public.pod_term_payment a " _
             & "  INNER JOIN public.po_mstr ON (podtp_po_oid = po_oid) " _
            & "WHERE " _
             & " po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
            & "ORDER BY " _
            & "  a.podtp_seq"


        load_data_detail(sql, gc_detail_payment, "detail_payment")

        sql = "SELECT  " _
                & "  a.app_oid, " _
                & "  a.app_ap_oid, " _
                & "  a.app_po_oid, " _
                & "  b.ap_code,b.ap_date, " _
                & "  b.ap_amount, " _
                & "  b.ap_pay_amount, " _
                & "  d.appay_code, appay_date " _
                & "FROM " _
                & "  public.app_po a " _
                & "  INNER JOIN public.ap_mstr b ON (a.app_ap_oid = b.ap_oid) " _
                & "  left outer JOIN public.appayd_det c ON (b.ap_oid = c.appayd_ap_oid) " _
                & "  left outer JOIN public.appay_payment d ON (c.appayd_appay_oid = d.appay_oid) " _
                & "  INNER JOIN public.po_mstr ON (a.app_po_oid = public.po_mstr.po_oid) " _
                & "  where po_mstr.po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & "  and po_mstr.po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " "

        load_data_detail(sql, gc_ap, "ap")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("pod_po_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pod_po_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_comment.Columns("cmt_ref_code").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cmt_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code").ToString & "'")
            gv_detail_comment.BestFitColumns()

            gv_wf.Columns("wf_ref_code").FilterInfo = _
          New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code").ToString & "'")
            gv_wf.BestFitColumns()

            gv_detail_payment.Columns("podtp_po_oid").FilterInfo = _
           New DevExpress.XtraGrid.Columns.ColumnFilterInfo("podtp_po_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString & "'")
            gv_detail_payment.BestFitColumns()

            gv_detail_receive.Columns("podtr_po_oid").FilterInfo = _
           New DevExpress.XtraGrid.Columns.ColumnFilterInfo("podtr_po_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString & "'")
            gv_detail_receive.BestFitColumns()

            gv_ap.Columns("app_po_oid").FilterInfo = _
         New DevExpress.XtraGrid.Columns.ColumnFilterInfo("app_po_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString & "'")
            gv_ap.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"
    Private Sub po_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles po_taxable.CheckedChanged
        If po_taxable.EditValue = False Then
            po_tax_class.Enabled = False
            po_tax_class.ItemIndex = 0
            po_tax_inc.Enabled = False
            po_tax_inc.Checked = False
        Else
            po_tax_class.Enabled = True
            po_tax_inc.Enabled = True
            po_tax_inc.Checked = False
        End If
    End Sub


#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        po_en_id.Focus()
        po_en_id.ItemIndex = 0
        po_cmaddr_id.ItemIndex = 0
        po_si_id.ItemIndex = 0
        po_sb_id.ItemIndex = 0
        po_cc_id.ItemIndex = 0
        po_pjc_id.ItemIndex = 0
        po_ptnr_id.ItemIndex = 0
        po_is_internal.EditValue = False
        po_date.DateTime = _now
        po_due_date.DateTime = _now
        po_need_date.DateTime = _now
        po_rmks.Text = ""
        po_tran_id.ItemIndex = 0
        po_cu_id.ItemIndex = 0
        po_cu_id.Enabled = True
        po_tax_class.ItemIndex = 0
        po_exc_rate.Text = 1
        po_tax_inc.EditValue = False
        po_taxable.EditValue = False
        po_tax_inc.Enabled = False
        po_tax_class.Enabled = True
        po_ptnr_id.Enabled = True
        po_tax_class.Enabled = True
        po_status_cash.Enabled = True
        'po_status_cash.EditValue = "-"
        po_status_cash.ItemIndex = 0
        'po_bk_id.ItemIndex = 0

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        po_parent_oid.Tag = ""
        po_parent_oid.Text = ""

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
                            & "  public.pod_det.pod_oid, " _
                            & "  public.pod_det.pod_dom_id, " _
                            & "  public.pod_det.pod_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pod_det.pod_add_by, " _
                            & "  public.pod_det.pod_add_date, " _
                            & "  public.pod_det.pod_upd_by, " _
                            & "  public.pod_det.pod_upd_date, " _
                            & "  public.pod_det.pod_po_oid, " _
                            & "  public.pod_det.pod_seq, " _
                            & "  public.pod_det.pod_reqd_oid, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.pod_det.pod_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.pod_det.pod_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pod_det.pod_pt_desc1, " _
                            & "  public.pod_det.pod_pt_desc2, " _
                            & "  public.pt_mstr.pt_type, " _
                            & "  public.pod_det.pod_memo, " _
                            & "  public.pod_det.pod_rmks, " _
                            & "  public.pod_det.pod_end_user, " _
                            & "  public.pod_det.pod_qty,0.0 as pod_qty_open, " _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost, " _
                            & "  public.pod_det.pod_disc, " _
                            & "  public.pod_det.pod_need_date, " _
                            & "  public.pod_det.pod_due_date, " _
                            & "  public.pod_det.pod_um_conv, " _
                            & "  public.pod_det.pod_qty_real, " _
                            & "  public.pod_det.pod_pt_class, " _
                            & "  public.pod_det.pod_status, " _
                            & "  public.pod_det.pod_dt, " _
                            & "  public.pod_det.pod_sb_id, " _
                            & "  sb_desc, " _
                            & "  public.pod_det.pod_cc_id, " _
                            & "  cc_desc, " _
                            & "  pod_loc_id, " _
                            & "  loc_desc, " _
                            & "  pt_type, " _
                            & "  pt_ls, " _
                            & "  public.pod_det.pod_pjc_id, " _
                            & "  pjc_desc, 0.0 as pod_qty_cost, " _
                            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                            & "  taxclass_mstr.code_name as pod_tax_class_name,'' as pod_pod_parent_oid " _
                            & "  FROM " _
                            & "  public.pod_det " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                            & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " where public.pod_det.pod_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'ds_comment = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  cmt_oid, " _
                        & "  cmt_dom_id, " _
                        & "  cmt_table, " _
                        & "  cmt_code, " _
                        & "  cmt_seq, " _
                        & "  cmt_type, " _
                        & "  cmt_ref_oid, " _
                        & "  cmt_ref_code, " _
                        & "  cmt_comment, " _
                        & "  cmt_dt " _
                        & "FROM  " _
                        & "  public.cmt_mstr " _
                        & "  where cmt_mstr.cmt_table = 'Purchase Order'" _
                        & " and cmt_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail_comment")
                    gc_comment.DataSource = ds_edit.Tables("detail_comment")
                    gv_comment.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.podtr_oid, " _
                        & "  a.podtr_po_oid, " _
                        & "  a.podtr_seq, " _
                        & "  a.podtr_date, " _
                        & "  a.podtr_qty,podtr_qty_percent " _
                        & "FROM " _
                        & "  public.pod_term_receive a " _
                        & "WHERE " _
                        & "  a.podtr_oid is null " _
                        & "ORDER BY " _
                        & "  a.podtr_seq"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail_receive")
                    gc_receive.DataSource = ds_edit.Tables("detail_receive")
                    gv_receive.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.podtp_oid, " _
                        & "  a.podtp_po_oid, " _
                        & "  a.podtp_seq, " _
                        & "  a.podtp_date, " _
                        & "  a.podtp_amount,podtp_amount_percent " _
                        & "FROM " _
                        & "  public.pod_term_payment a " _
                        & "WHERE " _
                        & "  a.podtp_oid is null " _
                        & "ORDER BY " _
                        & "  a.podtp_seq"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail_payment")
                    gc_payment.DataSource = ds_edit.Tables("detail_payment")
                    gv_payment.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssql As String

        ssql = "SELECT  " _
        & "  podd_file, " _
        & "  podd_seq " _
        & "FROM  " _
        & "  public.podd_doc where podd_oid is null " _
        & " order by podd_seq"

        dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
        gc_edit_doc.DataSource = dt_edit_doc
        gv_edit_doc.BestFitColumns()

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        gc_payment.EmbeddedNavigator.Buttons.DoClick(gc_payment.EmbeddedNavigator.Buttons.EndEdit)
        gc_receive.EmbeddedNavigator.Buttons.DoClick(gc_receive.EmbeddedNavigator.Buttons.EndEdit)
        'ds_edit.Tables(0).AcceptChanges()
        'gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If po_status_cash.EditValue = "C" Then
            Dim _date As Date = po_date.DateTime
            Dim _gcald_det_status As String = func_data.get_gcald_det_status(po_en_id.EditValue, "gcald_ic", _date)

            If _gcald_det_status = "" Then
                MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf _gcald_det_status.ToUpper = "Y" Then
                MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        'If ds_edit.Tables("detail_receive").Rows.Count = 0 Then
        '    MessageBox.Show("Data Receive Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        If ds_edit.Tables("detail_payment").Rows.Count = 0 Then
            MessageBox.Show("Data Payment Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim ssql As String
        ssql = "select count(*) as jml  from ptnr_mstr    " _
                & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid    " _
                & "inner join code_mstr addr_type on addr_type.code_id = ptnra_addr_type   " _
                & "where addr_type.code_name ~~* 'ship to' and ptnr_id=" & SetInteger(po_ptnr_id.EditValue)


        If GetRowInfo(ssql)(0) = 0 Then
            MessageBox.Show("Please Check address type Ship To", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            before_save = False
        End If



        '*********************
        'Cek UM
        Dim i As Integer

        Dim _persentase As Double = 0
        For i = 0 To ds_edit.Tables("detail_payment").Rows.Count - 1
            _persentase += SetNumber(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_amount_percent"))
        Next

        If _persentase <> 1 Then
            MessageBox.Show("Term Payment error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_um")) = True Then
                MessageBox.Show("Unit Measure Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************

        Dim _taxable, _tax_include As String
        Dim _tax_class As Integer

        _taxable = SetBitYN(po_taxable.EditValue)
        _tax_include = SetBitYN(po_tax_inc.EditValue)
        _tax_class = po_tax_class.EditValue

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            With ds_edit.Tables(0).Rows(i)
                If SetString(.Item("pod_taxable")) <> _taxable Or SetString(.Item("pod_tax_inc")) <> _tax_include Then
                    If CInt(SetNumber(.Item("pod_tax_class"))) <> _tax_class Then
                        MessageBox.Show("Taxable status, Tax Include Status or Tax Class detail are not equal with header", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End If
            End With
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                If func_coll.get_conf_file("restrict_po_by_pr") = "1" Then
                    If ds_edit.Tables(0).Rows(i).Item("pod_qty") > ds_edit.Tables(0).Rows(i).Item("pod_qty_open") Then
                        MessageBox.Show("Qty PO over than qty PR", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End If
                End If

            End If
        Next

        If _cost_center_pr_po = "1" Then
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If CInt(SetNumber(ds_edit.Tables(0).Rows(i).Item("pod_cc_id"))) = 0 Then
                    MessageBox.Show("Cost center can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next
        End If


        If Trim(po_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If po_status_cash.EditValue = "-" Then
            MessageBox.Show("Cash / Credit Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        'by sys 20110412
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_cost") = 0 Then
                MessageBox.Show("Can't Save For Cost <= 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
        '---------------------------------

        'by sys 20110418
        If func_coll.get_conf_file("restrict_po_by_pr") = "1" Then
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = True Then
                    MessageBox.Show("Requisiton Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next
        End If

        'Dim ssql As String
        If SetString(po_parent_oid.Tag) <> "" Then


            ssql = "SELECT  " _
                & "  public.pod_det.pod_oid, " _
                & "  public.pod_det.pod_dom_id, " _
                & "  public.pod_det.pod_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pod_det.pod_add_by, " _
                & "  public.pod_det.pod_add_date, " _
                & "  public.pod_det.pod_upd_by, " _
                & "  public.pod_det.pod_upd_date, " _
                & "  public.pod_det.pod_po_oid, " _
                & "  public.pod_det.pod_seq, " _
                & "  public.pod_det.pod_reqd_oid,pod_qty_child, " _
                & "  req_mstr_relation.req_code as req_code_relation, " _
                & "  public.pod_det.pod_si_id, " _
                & "  public.si_mstr.si_desc, " _
                & "  public.pod_det.pod_pt_id, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.pt_mstr.pt_desc2, " _
                & "  public.pod_det.pod_pt_desc1, " _
                & "  public.pod_det.pod_pt_desc2, " _
                & "  public.pod_det.pod_memo, " _
                & "  public.pod_det.pod_rmks, " _
                & "  public.pod_det.pod_end_user, " _
                & "  public.pod_det.pod_qty, " _
                & "  public.pod_det.pod_qty_receive, " _
                & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
                & "  public.pod_det.pod_qty_invoice, " _
                & "  public.pod_det.pod_um, " _
                & "  public.code_mstr.code_name, " _
                & "  public.pod_det.pod_cost, " _
                & "  public.pod_det.pod_disc, " _
                & "  public.pod_det.pod_need_date, " _
                & "  public.pod_det.pod_due_date, " _
                & "  public.pod_det.pod_um_conv, " _
                & "  public.pod_det.pod_qty_real, " _
                & "  public.pod_det.pod_pt_class, " _
                & "  public.pod_det.pod_status, " _
                & "  public.pod_det.pod_dt, " _
                & "  public.pod_det.pod_sb_id,pod_ppn,pod_pph, " _
                & "  sb_desc, " _
                & "  public.pod_det.pod_cc_id, " _
                & "  cc_desc, " _
                & "  loc_desc, " _
                & "  public.pod_det.pod_pjc_id, " _
                & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
                & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                & "  taxclass_mstr.code_name as pod_tax_class_name " _
                & "  FROM " _
                & "  public.pod_det " _
                & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
                & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
                & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
                & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
                & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
                & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                & "  where po_oid='" & po_parent_oid.Tag & "'"

            Dim dt_so_parent As New DataTable
            dt_so_parent = GetTableData(sSql)

            Dim _status_row As Boolean = False
            If dt_so_parent.Rows.Count = 0 Then
                Box("PO Parent Error")
                Return False
            End If

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                _status_row = False
                For Each dr_rows As DataRow In dt_so_parent.Rows
                    If SetNumber(ds_edit.Tables(0).Rows(i).Item("pod_pt_id")) = SetNumber(dr_rows("pod_pt_id")) Then
                        ds_edit.Tables(0).Rows(i).Item("pod_pod_parent_oid") = dr_rows("pod_oid").ToString

                        _status_row = True
                    End If
                Next

                If _status_row = False Then
                    Box("PO Parent Error")
                    Return False
                    Exit For
                End If

            Next
            ds_edit.AcceptChanges()

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                For Each dr_rows As DataRow In dt_so_parent.Rows
                    If SetNumber(ds_edit.Tables(0).Rows(i).Item("pod_pt_id")) = SetNumber(dr_rows("pod_pt_id")) Then
                        If (SetNumber(dr_rows("pod_qty_receive")) - SetNumber(dr_rows("pod_qty_child"))) < SetNumber(ds_edit.Tables(0).Rows(i).Item("pod_qty")) Then
                            Box("PO Qty receive parent lower than qty")
                            Return False
                            Exit For
                        End If
                    End If
                Next
            Next
        End If

        '---------------------------------

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        _conf_budget = func_coll.get_conf_file("budget_base")
        Dim _po_oid As Guid
        _po_oid = Guid.NewGuid

        Dim ssqls As New ArrayList
        Dim _po_code As String
        Dim ds_bantu As New DataSet
        Dim _po_total, _po_total_ppn, _po_total_pph, _pod_qty, _pod_cost, _pod_disc, _po_total_temp, _tax_rate As Double
        Dim i, k, j As Integer


        If cek_duplikat_pt_id(gv_edit, "pod_pt_id") = False Then
            Return False
        End If

        _conf_value = func_coll.get_conf_file("wf_purchase_order")
        Dim _po_trans_id As String = ""

        If _conf_value = "1" Then
            _po_trans_id = "D"
            ds_bantu = func_data.load_aprv_mstr(po_tran_id.EditValue)
        Else
            _po_trans_id = "I"
        End If

        If _conf_value = "1" Then
            If ds_bantu.Tables(0).Rows.Count = 0 Then
                Box("This transaction  haven't user work flow")
                Exit Function
            End If
        End If


        If _conf_value = "1" And po_status_cash.EditValue = "C" Then
            Box("Transaction cash for approval mode is not allowed")
            Exit Function
        End If

        _po_code = func_coll.get_transaction_number("PO", po_en_id.GetColumnValue("en_code"), "po_mstr", "po_code")

        '******* Mencari Total PO, Total PPN, Total PPH
        _po_total = 0
        _po_total_ppn = 0
        _po_total_pph = 0
        _po_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            ''disini hanya line ppn saja
            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

            _po_total = _po_total + ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
        Next

        'disini dihitung ppn dan pph nya
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

            _po_total_temp = ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
            _po_total_ppn = _po_total_ppn + (_po_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
            _po_total_pph = _po_total_pph + (_po_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
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
                                            & "  public.po_mstr " _
                                            & "( " _
                                            & "  po_oid, " _
                                            & "  po_dom_id, " _
                                            & "  po_en_id, " _
                                            & "  po_add_by, " _
                                            & "  po_add_date, " _
                                            & "  po_code, " _
                                            & "  po_ptnr_id, " _
                                            & "  po_cmaddr_id, " _
                                            & "  po_date, " _
                                            & "  po_need_date, " _
                                            & "  po_due_date, " _
                                            & "  po_rmks, " _
                                            & "  po_sb_id, " _
                                            & "  po_cc_id, " _
                                            & "  po_si_id, " _
                                            & "  po_pjc_id, " _
                                            & "  po_is_internal, " _
                                            & "  po_total, " _
                                            & "  po_tran_id,po_trans_id, " _
                                            & "  po_credit_term, " _
                                            & "  po_taxable, " _
                                            & "  po_tax_inc, " _
                                            & "  po_tax_class, " _
                                            & "  po_cu_id, " _
                                            & "  po_exc_rate, " _
                                            & "  po_total_ppn, " _
                                            & "  po_total_pph, " _
                                            & "  po_status_cash, " _
                                            & "  po_bk_id,po_parent_oid,po_parent_code, " _
                                            & "  po_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(po_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ", " _
                                            & SetSetring(_po_code) & ",  " _
                                            & po_ptnr_id.EditValue & ",  " _
                                            & po_cmaddr_id.EditValue & ",  " _
                                            & "" & SetDateNTime(po_date.DateTime) & "" & ",  " _
                                            & SetDate(po_need_date.DateTime) & ",  " _
                                            & SetDate(po_due_date.DateTime) & ",  " _
                                            & SetSetring(po_rmks.Text) & ",  " _
                                            & po_sb_id.EditValue & ",  " _
                                            & po_cc_id.EditValue & ",  " _
                                            & po_si_id.EditValue & ",  " _
                                            & po_pjc_id.EditValue & ",  " _
                                            & SetBitYN(po_is_internal.EditValue) & ",  " _
                                            & SetDbl(_po_total) & ",  " _
                                            & po_tran_id.EditValue & ",  " _
                                            & SetSetring(_po_trans_id) & "," _
                                            & po_credit_term.EditValue & ",  " _
                                            & SetBitYN(po_taxable.EditValue) & ",  " _
                                            & SetBitYN(po_tax_inc.EditValue) & ",  " _
                                            & po_tax_class.EditValue & ",  " _
                                            & po_cu_id.EditValue & ",  " _
                                            & SetDbl(po_exc_rate.EditValue) & ",  " _
                                            & SetDbl(_po_total_ppn) & ",  " _
                                            & SetDbl(_po_total_pph) & ",  " _
                                            & SetSetring(po_status_cash.EditValue) & ",  " _
                                            & SetInteger(po_bk_id.EditValue) & ",  " _
                                            & SetSetring(po_parent_oid.Tag) & ",  " _
                                            & SetSetring(po_parent_oid.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

                            Dim _po_ppn, _po_pph As Double

                            _po_ppn = 0
                            _po_pph = 0

                            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                            Else
                                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
                            End If

                            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
                            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

                            _po_total_temp = ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
                            _po_ppn = (_po_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
                            _po_pph = (_po_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))


                            '_pod_oid = Guid.NewGuid.ToString
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pod_det " _
                                                & "( " _
                                                & "  pod_oid, " _
                                                & "  pod_dom_id, " _
                                                & "  pod_en_id, " _
                                                & "  pod_add_by, " _
                                                & "  pod_add_date, " _
                                                & "  pod_po_oid, " _
                                                & "  pod_seq, " _
                                                & "  pod_reqd_oid, " _
                                                & "  pod_si_id, " _
                                                & "  pod_pt_id, " _
                                                & "  pod_pt_desc1, " _
                                                & "  pod_pt_desc2, " _
                                                & "  pod_memo, " _
                                                & "  pod_rmks, " _
                                                & "  pod_end_user, " _
                                                & "  pod_qty, " _
                                                & "  pod_um, " _
                                                & "  pod_cost, " _
                                                & "  pod_disc, " _
                                                & "  pod_sb_id, " _
                                                & "  pod_cc_id, " _
                                                & "  pod_pjc_id, " _
                                                & "  pod_need_date, " _
                                                & "  pod_due_date, " _
                                                & "  pod_um_conv, " _
                                                & "  pod_qty_real, " _
                                                & "  pod_taxable, " _
                                                & "  pod_tax_inc, " _
                                                & "  pod_tax_class, " _
                                                & "  pod_loc_id,pod_ppn,pod_pph,pod_pod_parent_oid, " _
                                                & "  pod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid")) & ",  " _
                                                & master_new.ClsVar.sdom_id & ",  " _
                                                & po_en_id.EditValue & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_po_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString)) & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_si_id") & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_pt_id") & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc2")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_memo")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_rmks")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_end_user")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_disc")) & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_sb_id") & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_cc_id") & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_pjc_id") & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("pod_need_date")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("pod_due_date")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_tax_inc")) & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("pod_tax_class") & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_loc_id")) & ",  " _
                                                & SetDbl(_po_ppn) & "," _
                                                & SetDbl(_po_pph) & "," _
                                                 & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_pod_parent_oid")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(po_parent_oid.Tag) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  pod_det " _
                                                    & "set pod_qty_child= coalesce(pod_qty_child,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) _
                                                    & " where pod_oid=" & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_pod_parent_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If


                            k = 0
                            For j = 0 To ds_edit.Tables("detail_comment").Rows.Count - 1
                                If ds_edit.Tables("insert_edit").Rows(i).Item("pod_oid").ToString = ds_edit.Tables("detail_comment").Rows(j).Item("cmt_ref_oid").ToString Then
                                    If IsDBNull(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_comment")) = False Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.cmt_mstr " _
                                                            & "( " _
                                                            & "  cmt_oid, " _
                                                            & "  cmt_dom_id, " _
                                                            & "  cmt_table, " _
                                                            & "  cmt_code, " _
                                                            & "  cmt_seq, " _
                                                            & "  cmt_type, " _
                                                            & "  cmt_ref_oid, " _
                                                            & "  cmt_ref_code, " _
                                                            & "  cmt_comment, " _
                                                            & "  cmt_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_oid").ToString) & ",  " _
                                                            & SetInteger(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_dom_id")) & ",  " _
                                                            & "'Purchase Order',  " _
                                                            & SetSetring(func_coll.get_transaction_number("CM", po_en_id.GetColumnValue("en_code"), "cmt_mstr", "cmt_code")) & ",  " _
                                                            & k & ",  " _
                                                            & SetSetringDB(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_type")) & ",  " _
                                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid")) & ",  " _
                                                            & SetSetring(_po_code) & ",  " _
                                                            & SetSetringDB(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_comment")) & ",  " _
                                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                            & ")"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                        k = k + 1
                                    End If
                                End If
                            Next

                            'Update relation PR apabila terdapat relasi pr
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Ini tidak dilakukan karena close pada saat user sudah terima barang dan ada menu khusus
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update reqd_det set reqd_status = 'c'" _
                                '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'" _
                                '                     & " and reqd_qty_processed = reqd_qty "
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If

                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                'har 20110617
                                Dim _boqs_oid As String
                                _boqs_oid = ""
                                Dim sSql As String
                                sSql = "SELECT  " _
                                        & "  reqd_boqs_oid " _
                                        & "FROM " _
                                        & "   public.reqd_det  " _
                                        & "WHERE " _
                                        & "  reqd_oid='" & ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString & "'"
                                Dim dt As New DataTable

                                dt = master_new.PGSqlConn.GetTableData(sSql)


                                For Each dr As DataRow In dt.Rows
                                    _boqs_oid = SetString(dr(0))
                                Next

                                If _boqs_oid <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update boqs_stand set boqs_qty_po=coalesce(boqs_qty_po,0) + " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            End If
                        Next


                        For i = 0 To ds_edit.Tables("detail_receive").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pod_term_receive " _
                                            & "( " _
                                            & "  podtr_oid, " _
                                            & "  podtr_po_oid, " _
                                            & "  podtr_seq, " _
                                            & "  podtr_date, " _
                                            & "  podtr_qty, " _
                                            & "  podtr_qty_percent " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetDateNTime00(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_date")) & ",  " _
                                            & SetDec(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_qty")) & ",  " _
                                            & SetDec(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_qty_percent")) & "  " _
                                            & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        For i = 0 To ds_edit.Tables("detail_payment").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                               & "  public.pod_term_payment " _
                                               & "( " _
                                               & "  podtp_oid, " _
                                               & "  podtp_po_oid, " _
                                               & "  podtp_seq, " _
                                               & "  podtp_date, " _
                                               & "  podtp_amount, " _
                                               & "  podtp_amount_percent " _
                                               & ")  " _
                                               & "VALUES ( " _
                                               & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                               & SetSetring(_po_oid.ToString) & ",  " _
                                               & SetInteger(i) & ",  " _
                                               & SetDateNTime00(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_date")) & ",  " _
                                               & SetDec(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_amount")) & ",  " _
                                               & SetDec(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_amount_percent")) & "  " _
                                               & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
                        Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
                        Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")

                        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                            _ip_server = func_coll.get_conf_file("ftp_doc_server_online")

                        End If

                        Dim r As Integer = 0
                        For Each dr As DataRow In dt_edit_doc.Rows

                            If SetString(dr("podd_file")) <> "" Then
                                If SetString(dr("podd_file")).Contains("\") Then

                                    Dim _nama_file As String = ""
                                    _nama_file = IO.Path.GetFileName(dr("podd_file")).Replace(" ", "_")
                                    _nama_file = _po_code & "_" & _nama_file


                                    'Dim _error As New ArrayList

                                    'Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                                    'If Upload(dr("podd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                    '    sqlTran.Rollback()
                                    '    'MessageBox.Show(ex.Message)
                                    '    Box("Upload error : " & _error.Item(0))
                                    '    insert = False
                                    '    '    'Return False
                                    '    '    'Exit Function
                                    'End If



                                    Dim _sukses As Boolean = False

                                    If func_coll.get_conf_file("doc_upload_mode") = "http" Then
                                        Try
                                            Dim _url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & "upload.php?nama_file=" & _nama_file
                                            'Box(_url)
                                            HttpUploadFile(_url, dr("podd_file"), "files", "multipart/form-data")

                                            _sukses = True
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try
                                    Else
                                        Dim _error As New ArrayList

                                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                                        If Upload(dr("podd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                            sqlTran.Rollback()
                                            'MessageBox.Show(ex.Message)
                                            Box("Upload error : " & _error.Item(0))
                                            insert = False
                                            '    'Return False
                                            '    'Exit Function
                                        End If
                                    End If



                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                & "  public.podd_doc " _
                                                & "( " _
                                                & "  podd_oid, " _
                                                & "  podd_po_oid, " _
                                                & "  podd_file, " _
                                                & "  podd_seq " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_po_oid.ToString) & ",  " _
                                                & SetSetring(_nama_file) & ",  " _
                                                & SetInteger(r) & "  " _
                                                & ")"


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                Else

                                End If
                            End If


                            r = r + 1
                        Next

                        'apabila chash maka langsung bisa generate po receive....
                        If po_status_cash.EditValue = "C" Then
                            If insert_receipt(ssqls, objinsert, _po_oid.ToString, _po_code) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                            'update rekonsiliasi po cash
                            If update_rec(objinsert, ssqls, po_en_id.EditValue, po_bk_id.EditValue, po_cu_id.EditValue, _
                                         po_exc_rate.EditValue, (_po_total + _po_total_pph + _po_total_ppn) * -1, po_date.DateTime, _po_code, _
                                         po_ptnr_id.Text, "PO CASH") = False Then
                                sqlTran.Rollback()
                                Return False
                                Exit Function
                            End If

                        End If

                        If _conf_budget = "1" Then
                            If func_coll.update_budget(ssqls, objinsert, ds_edit, po_en_id.EditValue, po_date.EditValue) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, po_en_id.EditValue, 2, _po_oid.ToString, _po_code, po_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(po_en_id.EditValue) & ",  " _
                                                        & SetInteger(po_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_po_oid.ToString) & ",  " _
                                                        & SetSetring(_po_code) & ",  " _
                                                        & SetSetring("Purchase Order") & ",  " _
                                                        & SetInteger(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert PO " & _po_code)
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
                        set_row(_po_oid.ToString, "po_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
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

    Private Function insert_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_po_oid As String, ByVal par_po_code As String) As Boolean
        insert_receipt = True
        Dim _rcv_oid As Guid
        _rcv_oid = Guid.NewGuid

        Dim _rcv_code, _serial, _cost_methode As String
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        _rcv_code = func_coll.get_transaction_number("RC", po_en_id.GetColumnValue("en_code"), "rcv_mstr", "rcv_code")

        _tran_id = func_coll.get_id_tran_mstr("rct_po")
        If _tran_id = -1 Then
            MessageBox.Show("Receipt PO In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        With par_obj
            .Command.CommandType = CommandType.Text
            .Command.CommandText = "INSERT INTO  " _
                                & "  public.rcv_mstr " _
                                & "( " _
                                & "  rcv_oid, " _
                                & "  rcv_dom_id, " _
                                & "  rcv_en_id, " _
                                & "  rcv_add_by, " _
                                & "  rcv_add_date, " _
                                & "  rcv_code, " _
                                & "  rcv_date, " _
                                & "  rcv_eff_date, " _
                                & "  rcv_po_oid, " _
                                & "  rcv_packing_slip, " _
                                & "  rcv_cu_id, " _
                                & "  rcv_exc_rate, " _
                                & "  rcv_is_receive, " _
                                & "  rcv_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_rcv_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(po_en_id.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                & SetSetring(_rcv_code) & ",  " _
                                & SetDate(po_date.DateTime) & ",  " _
                                & SetDate(po_date.DateTime) & ",  " _
                                & SetSetring(par_po_oid.ToString) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetInteger(po_cu_id.EditValue) & ",  " _
                                & SetDbl(po_exc_rate.EditValue) & ",  " _
                                & SetSetring("Y") & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                & ");"
            par_ssqls.Add(.Command.CommandText)
            .Command.ExecuteNonQuery()
            .Command.Parameters.Clear()

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("pod_qty") > 0 Then
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.rcvd_det " _
                                        & "( " _
                                        & "  rcvd_oid, " _
                                        & "  rcvd_rcv_oid, " _
                                        & "  rcvd_pod_oid, " _
                                        & "  rcvd_qty, " _
                                        & "  rcvd_um, " _
                                        & "  rcvd_packing_qty, " _
                                        & "  rcvd_um_conv, " _
                                        & "  rcvd_qty_real, " _
                                        & "  rcvd_si_id, " _
                                        & "  rcvd_loc_id, " _
                                        & "  rcvd_supp_lot, " _
                                        & "  rcvd_dt " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_rcv_oid.ToString) & ",  " _
                                        & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString) & ",  " _
                                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_um_conv")) & ",  " _
                                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty_real")) & ",  " _
                                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_si_id")) & ",  " _
                                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_loc_id")) & ",  " _
                                        & SetSetringDB(0) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                        & ");"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    'Update pod_det
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "update pod_det set pod_qty_receive = coalesce(pod_qty_receive,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                         & ", pod_qty_invoice = coalesce(pod_qty_invoice,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                         & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString + "'"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "update pod_det set pod_status = 'c'" _
                                         & " where pod_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString + "'" _
                                         & " and pod_qty = pod_qty_receive "
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    'har 20110707
                    'update boqs================================================
                    Dim _boqs_oid As String
                    _boqs_oid = ""
                    Dim sSql As String
                    sSql = "SELECT  " _
                      & "  b.reqd_boqs_oid " _
                      & "FROM " _
                      & "  public.pod_det a " _
                      & "  INNER JOIN public.reqd_det b ON (a.pod_reqd_oid = b.reqd_oid) " _
                      & "WHERE " _
                      & " a.pod_oid='" & ds_edit.Tables(0).Rows(i).Item("rcvd_pod_oid").ToString & "'"
                    Dim dt As New DataTable
                    dt = master_new.PGSqlConn.GetTableData(sSql)

                    For Each dr As DataRow In dt.Rows
                        _boqs_oid = SetString(dr(0))
                    Next

                    If _boqs_oid <> "" Then
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update boqs_stand set boqs_qty_receipt=coalesce(boqs_qty_receipt,0) + " _
                           & SetDbl(ds_edit.Tables(0).Rows(i).Item("rcvd_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    End If
                    '================================================
                End If
            Next

            'ds_serial tidak ada...jadi pembelian barang cash yang serial tidak bisa dilakukan

            'Untuk update po_close_date apabile semua line sudah terpenuhi qty_receivenya...
            .Command.CommandType = CommandType.Text
            .Command.CommandText = "update po_mstr set po_close_date = " & SetDateNTime00(po_date.DateTime) & "" + _
                                   " where coalesce((select count(pod_po_oid) as jml From pod_det " + _
                                   " where pod_qty <> coalesce(pod_qty_receive,0) " + _
                                   " and pod_po_oid = '" + par_po_oid.ToString + "'" + _
                                   " group by pod_po_oid),0) = 0 " + _
                                   " and po_oid = '" + par_po_oid.ToString + "'"
            par_ssqls.Add(.Command.CommandText)
            .Command.ExecuteNonQuery()
            .Command.Parameters.Clear()

            'Update Table Inventory Dan Cost Inventory Dan History Inventory
            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
            i_2 = 0
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                    If ds_edit.Tables(0).Rows(i).Item("pod_qty_real") > 0 Then
                        If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                            i_2 += 1

                            _en_id = po_en_id.EditValue
                            _si_id = ds_edit.Tables(0).Rows(i).Item("pod_si_id")
                            _loc_id = ds_edit.Tables(0).Rows(i).Item("pod_loc_id")
                            _pt_id = ds_edit.Tables(0).Rows(i).Item("pod_pt_id")
                            _serial = "''"
                            _qty = ds_edit.Tables(0).Rows(i).Item("pod_qty_real")
                            If func_coll.update_invc_mstr_plus(par_ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                Return False
                            End If

                            'Update History Inventory                                    
                            _cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
                            _cost = _cost * po_exc_rate.EditValue
                            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                            If func_coll.update_invh_mstr(par_ssqls, par_obj, _tran_id, i_2, _en_id, _rcv_code, _rcv_oid.ToString, "PO Receipt (PO Cash)", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", po_date.DateTime) = False Then
                                Return False
                            End If
                        Else
                            If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena Lot/Serial", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then
                                Return False
                                Exit Function
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = insert_log("Insert PO Cash tidak masuk ke stok karena Lot/Serial " & _rcv_code)
                                par_ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        End If
                    Else
                        If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena QTY Real < 1", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then
                            Return False
                            Exit Function
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Insert PO Cash tidak masuk ke stok karena QTY Real < 1" & _rcv_code)
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                    End If
                Else
                    If ask(ds_edit.Tables(0).Rows(i).Item("pt_code") & " " & ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1") & " tidak akan masuk ke stok karena tipenya bukan Inventory.", "Konfirmasi ...", MessageBoxDefaultButton.Button1) = False Then

                        Return False
                        Exit Function
                    Else
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert PO Cash tidak masuk ke stok karena tipenya bukan Inventory" & _rcv_code)
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                    End If
                    'pod_pt_desc1
                End If
            Next

            'ds_serial tidak ada...jadi pembelian barang cash yang serial tidak bisa dilakukan

            '3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                    _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pod_pt_id").ToString.ToUpper)
                    _en_id = po_en_id.EditValue
                    _si_id = ds_edit.Tables(0).Rows(i).Item("pod_si_id")
                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pod_pt_id")
                    _qty = ds_edit.Tables(0).Rows(i).Item("pod_qty_real")
                    _cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
                    _cost = _cost * po_exc_rate.EditValue
                    If _cost_methode = "F" Or _cost_methode = "L" Then
                        MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        Return False
                        'If func_coll.update_invct_table_plus(ssqls, par_obj, _en_id, _pt_id, _qty, _cost) = False Then
                        '    Return False
                        'End If
                    ElseIf _cost_methode = "A" Then
                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        If func_coll.update_item_cost_avg(par_ssqls, par_obj, _si_id, _pt_id, _cost_avg) = False Then
                            Return False
                        End If
                    End If
                End If
            Next
        End With

        'Insert ke table glt_det
        'Di proses ini langsung ke prodline account tidak ke prodline location account...
        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja

        If _create_jurnal = True Then
            If glt_det_purchase_receipt(par_ssqls, par_obj, ds_edit, _rcv_oid.ToString, _rcv_code, "IC-RPO") = False Then
                Return False
            End If
        End If

        Return insert_receipt
    End Function

    Private Function glt_det_purchase_receipt(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, ByVal par_rcv_oid As String, ByVal par_rcv_code As String, ByVal par_daybook As String) As Boolean
        glt_det_purchase_receipt = True
        Dim i, _pl_id As Integer
        Dim _glt_code, _glt_desc As String
        Dim dt_bantu As DataTable
        Dim _cost As Double
        Dim _ppn As Double
        Dim _kas_bank As Double

        _glt_desc = ""
        _glt_code = func_coll.get_transaction_number("IC", po_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")
        Dim _bk_ac_id As Integer = get_bk_ac_id(po_bk_id.EditValue) ' ini unttuk account creditnya...karena po yang bersifat cash / tunai
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("pod_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("pod_pt_id"))

                        If par_daybook.ToUpper = "IC-RPO" Then
                            'MessageBox.Show(par_ds.Tables(0).Rows(i).Item("pod_memo"))
                            If Trim(par_ds.Tables(0).Rows(i).Item("pod_memo")) = "" Then
                                dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))
                            Else
                                dt_bantu = (func_coll.get_prodline_account(_pl_id, "PRC_PACC"))
                            End If
                            _glt_desc = "PO Receipt"
                        ElseIf par_daybook.ToUpper = "IC-PRS" Then
                            dt_bantu = (func_coll.get_prodline_account(_pl_id, "PRC_PORACC"))
                            _glt_desc = "PO Return"
                        End If

                        '_tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                        '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                        Dim _tax_rate As Double

                        If par_ds.Tables(0).Rows(i).Item("pod_taxable").ToString.ToUpper = "Y" Then
                            _tax_rate = func_coll.get_ppn(par_ds.Tables(0).Rows(i).Item("pod_tax_class"))
                            If par_ds.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then

                                _ppn = _tax_rate * (par_ds.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate))

                                _cost = (par_ds.Tables(0).Rows(i).Item("pod_cost") - (_ppn)) _
                                        * par_ds.Tables(0).Rows(i).Item("pod_qty")

                                _kas_bank = par_ds.Tables(0).Rows(i).Item("pod_cost") * par_ds.Tables(0).Rows(i).Item("pod_qty")
                            Else
                                _ppn = _tax_rate * par_ds.Tables(0).Rows(i).Item("pod_cost")
                                _cost = par_ds.Tables(0).Rows(i).Item("pod_cost") * par_ds.Tables(0).Rows(i).Item("pod_qty")
                                _kas_bank = (par_ds.Tables(0).Rows(i).Item("pod_cost") + _ppn) * par_ds.Tables(0).Rows(i).Item("pod_qty")
                            End If
                        Else
                            _ppn = 0
                            _cost = par_ds.Tables(0).Rows(i).Item("pod_cost") * par_ds.Tables(0).Rows(i).Item("pod_qty")
                            _kas_bank = par_ds.Tables(0).Rows(i).Item("pod_cost") * par_ds.Tables(0).Rows(i).Item("pod_qty")
                        End If

                        _seq = _seq + 1

                        'Insert Untuk Yang Debet Dulu
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(po_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(po_date.DateTime) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(po_cu_id.EditValue) & ",  " _
                                            & SetDbl(po_exc_rate.EditValue) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring(_glt_desc) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_rcv_oid.ToString) & ",  " _
                                            & SetSetring(par_rcv_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, po_date.DateTime, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         po_en_id.EditValue, po_cu_id.EditValue, _
                                                         po_exc_rate.EditValue, _cost, "D") = False Then

                            Return False
                        End If
                        '********************** finish untuk yang debet

                        _seq = _seq + 1
                        If par_ds.Tables(0).Rows(i).Item("pod_taxable").ToString.ToUpper = "Y" Then
                            '********* mulai untuk ppn ***********
                            dt_bantu = New DataTable
                            dt_bantu = (func_data.get_taxrate_ap_account(par_ds.Tables(0).Rows(i).Item("pod_tax_class")))

                            'Insert Untuk Yang Debet Dulu
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.glt_det " _
                                                & "( " _
                                                & "  glt_oid, " _
                                                & "  glt_dom_id, " _
                                                & "  glt_en_id, " _
                                                & "  glt_add_by, " _
                                                & "  glt_add_date, " _
                                                & "  glt_code, " _
                                                & "  glt_date, " _
                                                & "  glt_type, " _
                                                & "  glt_cu_id, " _
                                                & "  glt_exc_rate, " _
                                                & "  glt_seq, " _
                                                & "  glt_ac_id, " _
                                                & "  glt_sb_id, " _
                                                & "  glt_cc_id, " _
                                                & "  glt_desc, " _
                                                & "  glt_debit, " _
                                                & "  glt_credit, " _
                                                & "  glt_ref_oid, " _
                                                & "  glt_ref_trans_code, " _
                                                & "  glt_posted, " _
                                                & "  glt_dt, " _
                                                & "  glt_daybook " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(po_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_glt_code) & ",  " _
                                                & SetDate(po_date.DateTime) & ",  " _
                                                & SetSetring("IC") & ",  " _
                                                & SetInteger(po_cu_id.EditValue) & ",  " _
                                                & SetDbl(po_exc_rate.EditValue) & ",  " _
                                                & SetInteger(_seq) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("taxr_ac_ap_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("taxr_sb_ap_id")) & ",  " _
                                                & SetInteger(dt_bantu.Rows(0).Item("taxr_cc_ap_id")) & ",  " _
                                                & SetSetring(_glt_desc) & ",  " _
                                                & SetDblDB(_ppn * par_ds.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                & SetDblDB(0) & ",  " _
                                                & SetSetring(par_rcv_oid.ToString) & ",  " _
                                                & SetSetring(par_rcv_code) & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(par_daybook) & "  " _
                                                & ")"
                            par_ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, po_date.DateTime, _
                                                             dt_bantu.Rows(0).Item("taxr_ac_ap_id"), _
                                                             dt_bantu.Rows(0).Item("taxr_sb_ap_id"), _
                                                             dt_bantu.Rows(0).Item("taxr_cc_ap_id"), _
                                                             po_en_id.EditValue, po_cu_id.EditValue, _
                                                             po_exc_rate.EditValue, _ppn * par_ds.Tables(0).Rows(i).Item("pod_qty"), "D") = False Then

                                Return False
                            End If

                        End If


                        _seq = _seq + 1
                        'untuk yang credit
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(po_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(po_date.DateTime) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(po_cu_id.EditValue) & ",  " _
                                            & SetDbl(po_exc_rate.EditValue) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(_bk_ac_id) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetInteger(0) & ",  " _
                                            & SetSetringDB(_glt_desc) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_kas_bank) & ",  " _
                                            & SetSetring(par_rcv_oid) & ",  " _
                                            & SetSetring(par_rcv_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, po_date.DateTime, _
                                                         _bk_ac_id, _
                                                         0, _
                                                         0, _
                                                         po_en_id.EditValue, po_cu_id.EditValue, _
                                                         po_exc_rate.EditValue, _cost, "C") = False Then

                            Return False
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Private Function get_bk_ac_id(ByVal par_bk_id As Integer) As Integer
        get_bk_ac_id = -1
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_ac_id from bk_mstr where bk_id = " + par_bk_id.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_bk_ac_id = .DataReader("bk_ac_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
        Return get_bk_ac_id
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_status_cash").ToString.ToUpper = "C" Then
            MessageBox.Show("Can't Edit Purchase Order Cash Transaction...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        For i As Integer = 0 To gv_detail.RowCount - 1
            If Not gv_detail.GetRowCellValue(i, "pod_qty_receive") Is System.DBNull.Value Then
                If gv_detail.GetRowCellValue(i, "pod_qty_receive") > 0 Then
                    MessageBox.Show("Data already receive..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                    Exit Function
                End If
            End If
        Next

        If _conf_value = 1 Then
            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "X" Then
                MessageBox.Show("Can't Edit Data That Has Been cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "W" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
                    MessageBox.Show("Can't Edit Data That Has Been Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        Else
            'If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            '    MessageBox.Show("Disable Authorization Edit PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End If
            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id") <> "D" Then
            '    If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")) > 0 Then
            '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Function
            '    End If
            'End If
        End If

        'gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then
            load_data_grid_detail()
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _po_oid_mstr = .Item("po_oid").ToString
                po_en_id.EditValue = .Item("po_en_id")
                po_ptnr_id.EditValue = .Item("po_ptnr_id")
                po_cmaddr_id.EditValue = .Item("po_cmaddr_id")
                po_date.DateTime = .Item("po_date")
                po_need_date.DateTime = .Item("po_need_date")
                po_due_date.DateTime = .Item("po_due_date")
                po_rmks.Text = SetString(.Item("po_rmks"))
                po_sb_id.EditValue = .Item("po_sb_id")
                po_cc_id.EditValue = .Item("po_cc_id")
                po_si_id.EditValue = .Item("po_si_id")
                po_pjc_id.EditValue = .Item("po_pjc_id")
                po_is_internal.EditValue = SetBitYNB(.Item("po_is_internal"))
                po_tran_id.EditValue = .Item("po_tran_id")
                po_credit_term.EditValue = .Item("po_credit_term")
                po_taxable.EditValue = SetBitYNB(.Item("po_taxable"))
                po_tax_inc.EditValue = SetBitYNB(.Item("po_tax_inc"))
                po_tax_class.EditValue = .Item("po_tax_class")
                po_cu_id.EditValue = .Item("po_cu_id")
                po_exc_rate.EditValue = .Item("po_exc_rate")
                po_status_cash.EditValue = .Item("po_status_cash")
                po_bk_id.EditValue = .Item("po_bk_id")

                po_parent_oid.Tag = SetString(.Item("po_parent_oid"))
                po_parent_oid.EditValue = .Item("po_parent_code")

                If po_taxable.EditValue = False Then
                    po_tax_class.Enabled = False
                Else
                    po_tax_class.Enabled = True
                End If
            End With
            po_en_id.Focus()
            po_cu_id.Enabled = False
            po_ptnr_id.Enabled = True
            po_status_cash.Enabled = False
            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  public.pod_det.pod_oid, " _
                            & "  public.pod_det.pod_dom_id, " _
                            & "  public.pod_det.pod_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.pod_det.pod_add_by, " _
                            & "  public.pod_det.pod_add_date, " _
                            & "  public.pod_det.pod_upd_by, " _
                            & "  public.pod_det.pod_upd_date, " _
                            & "  public.pod_det.pod_po_oid, " _
                            & "  public.pod_det.pod_seq, " _
                            & "  public.pod_det.pod_reqd_oid, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.pod_det.pod_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.pod_det.pod_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.pod_det.pod_pt_desc1, " _
                            & "  public.pod_det.pod_pt_desc2, " _
                            & "  public.pt_mstr.pt_type, " _
                            & "  public.pod_det.pod_memo, " _
                            & "  public.pod_det.pod_rmks, " _
                            & "  public.pod_det.pod_end_user, " _
                            & "  public.pod_det.pod_qty,reqd_qty_real - coalesce(reqd_qty_processed,0) + coalesce(pod_qty,0) as pod_qty_open ," _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost, " _
                            & "  public.pod_det.pod_disc, " _
                            & "  public.pod_det.pod_need_date, " _
                            & "  public.pod_det.pod_due_date, " _
                            & "  public.pod_det.pod_um_conv, " _
                            & "  public.pod_det.pod_qty_real, " _
                            & "  public.pod_det.pod_pt_class, " _
                            & "  public.pod_det.pod_status, " _
                            & "  public.pod_det.pod_dt, " _
                            & "  public.pod_det.pod_sb_id, " _
                            & "  sb_desc, " _
                            & "  public.pod_det.pod_cc_id,pod_pod_parent_oid,pod_qty_child, " _
                            & "  cc_desc, " _
                            & "  pod_loc_id, " _
                            & "  loc_desc, " _
                            & "  public.pod_det.pod_pjc_id, " _
                            & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
                            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                            & "  taxclass_mstr.code_name as pod_tax_class_name, " _
                            & " coalesce((select count(*) as jml from rcvd_det where rcvd_pod_oid=pod_oid),0) as status_receive " _
                            & "  FROM " _
                            & "  public.pod_det " _
                            & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
                            & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
                            & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
                            & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                            & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " where public.pod_det.pod_po_oid = '" + ds.Tables(0).Rows(row).Item("po_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
                        .FillDataSet(ds_update_related, "update_related")
                        .SQL = "SELECT  " _
                            & "  cmt_oid, " _
                            & "  cmt_dom_id, " _
                            & "  cmt_table, " _
                            & "  cmt_code, " _
                            & "  cmt_seq, " _
                            & "  cmt_type, " _
                            & "  cmt_ref_oid, " _
                            & "  cmt_ref_code, " _
                            & "  cmt_comment, " _
                            & "  cmt_dt " _
                            & "FROM  " _
                            & "  public.cmt_mstr " _
                            & "  where cmt_mstr.cmt_table = 'Purchase Order'" _
                            & " AND public.cmt_mstr.cmt_ref_code = '" + ds.Tables(0).Rows(row).Item("po_code").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail_comment")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()

                        gc_comment.DataSource = ds_edit.Tables("detail_comment")
                        gv_comment.BestFitColumns()

                        gv_edit_Click(Nothing, Nothing)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.podtr_oid, " _
                            & "  a.podtr_po_oid, " _
                            & "  a.podtr_seq, " _
                            & "  a.podtr_date, " _
                            & "  a.podtr_qty,podtr_qty_percent " _
                            & "FROM " _
                            & "  public.pod_term_receive a " _
                            & "WHERE " _
                            & "  a.podtr_po_oid = '" + ds.Tables(0).Rows(row).Item("po_oid").ToString + "' " _
                            & "ORDER BY " _
                            & "  a.podtr_seq"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail_receive")
                        gc_receive.DataSource = ds_edit.Tables("detail_receive")
                        gv_receive.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.podtp_oid, " _
                            & "  a.podtp_po_oid, " _
                            & "  a.podtp_seq, " _
                            & "  a.podtp_date, " _
                            & "  a.podtp_amount,podtp_amount_percent " _
                            & "FROM " _
                            & "  public.pod_term_payment a " _
                            & "WHERE " _
                            & "  a.podtp_po_oid = '" + ds.Tables(0).Rows(row).Item("po_oid").ToString + "' " _
                            & "ORDER BY " _
                            & "  a.podtp_seq"
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail_payment")
                        gc_payment.DataSource = ds_edit.Tables("detail_payment")
                        gv_payment.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            'Dim row As Integer = BindingContext(ds.Tables(0)).Position
            Dim ssql As String
            ssql = "SELECT  " _
                    & "  podd_file, " _
                    & "  podd_seq " _
                    & "FROM  " _
                    & "  public.podd_doc where podd_po_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("po_oid").ToString) _
                    & " order by podd_seq"


            dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
            gc_edit_doc.DataSource = dt_edit_doc
            gv_edit_doc.BestFitColumns()



            edit_data = True
        End If
        cek_receive_row()
    End Function

    Public Overrides Function edit()


        edit = True
        Dim _po_total, _pod_qty, _pod_cost, _pod_disc, _po_total_ppn, _po_total_pph, _po_total_temp, _tax_rate As Double
        Dim _pod_qty_receive As Double = 0
        Dim i, j, k As Integer
        Dim ssqls As New ArrayList
        '******* Mencari Total PO, Total PPN, Total PPH
        _po_total = 0
        _po_total_ppn = 0
        _po_total_pph = 0
        _po_total_temp = 0

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(po_tran_id.EditValue)
        End If


        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

            _po_total = _po_total + ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

            _po_total_temp = ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
            _po_total_ppn = _po_total_ppn + (_po_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
            _po_total_pph = _po_total_pph + (_po_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
        Next
        '*******************************************************

        dt_bantu.Clear()
        dt_bantu = (func_data.load_list_aprvd_dok(po_en_id.EditValue, 1, po_date.DateTime))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.po_mstr   " _
                                            & "SET  " _
                                            & "  po_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  po_en_id = " & po_en_id.EditValue & ",  " _
                                            & "  po_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  po_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  po_ptnr_id = " & po_ptnr_id.EditValue & ",  " _
                                            & "  po_cmaddr_id = " & po_cmaddr_id.EditValue & ",  " _
                                            & "  po_date = " & SetDate(po_date.DateTime) & ",  " _
                                            & "  po_need_date = " & SetDate(po_need_date.DateTime) & ",  " _
                                            & "  po_due_date = " & SetDate(po_due_date.DateTime) & ",  " _
                                            & "  po_rmks = " & SetSetring(po_rmks.Text) & ",  " _
                                             & "  po_trans_id = " & SetSetring("D") & ",  " _
                                            & "  po_sb_id = " & SetInteger(po_sb_id.EditValue) & ",  " _
                                            & "  po_cc_id = " & SetInteger(po_cc_id.EditValue) & ",  " _
                                            & "  po_si_id = " & SetInteger(po_si_id.EditValue) & ",  " _
                                            & "  po_pjc_id = " & SetInteger(po_pjc_id.EditValue) & ",  " _
                                            & "  po_is_internal = " & SetBitYN(po_is_internal.EditValue) & ",  " _
                                            & "  po_total = " & SetDbl(_po_total.ToString) & ",  " _
                                            & "  po_tran_id = " & po_tran_id.EditValue & ",  " _
                                            & "  po_credit_term = " & po_credit_term.EditValue & ",  " _
                                            & "  po_taxable = " & SetBitYN(po_taxable.EditValue) & ",  " _
                                            & "  po_tax_inc = " & SetBitYN(po_tax_inc.EditValue) & ",  " _
                                            & "  po_tax_class = " & po_tax_class.EditValue & ",  " _
                                            & "  po_cu_id = " & SetInteger(po_cu_id.EditValue) & ",  " _
                                            & "  po_exc_rate = " & SetDbl(po_exc_rate.EditValue) & ",  " _
                                            & "  po_total_ppn = " & SetDbl(_po_total_ppn) & ",  " _
                                            & "  po_total_pph = " & SetDbl(_po_total_pph) & ",  " _
                                            & "  po_status_cash = " & SetSetring(po_status_cash.EditValue) & ",  " _
                                            & "  po_bk_id = " & SetInteger(po_bk_id.EditValue) & ",  " _
                                            & "  po_parent_oid = " & SetSetring(po_parent_oid.Tag) & ",  " _
                                            & "  po_parent_code = " & SetSetring(po_parent_oid.EditValue) & ",  " _
                                            & "  po_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  po_oid = " & SetSetring(_po_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("pod_po_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString Then

                                If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_parent_oid")) <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update  " _
                                                        & "  pod_det " _
                                                        & "set pod_qty_child= coalesce(pod_qty_child,0) - " & SetDbl(ds.Tables("detail").Rows(i).Item("pod_qty")) _
                                                        & " where pod_oid=" & SetSetring(ds.Tables("detail").Rows(i).Item("pod_pod_parent_oid"))

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If


                            End If
                        Next

                        'by sys 20110418 perbaikan update ke requisition
                        'kurangi dulu qty processed pr nya
                        For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + SetDbl(ds_update_related.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                                     & " where reqd_oid = '" + ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        'tambahkan lagi qty processed pr nya
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next
                        '------------------------------------------------
                        ''Sebelum delete data detail maka balikan dulu requistion detail ke semula
                        'For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                        '    If IsDBNull(ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + ds_update_related.Tables(0).Rows(i).Item("pod_qty").ToString + ", " + _
                        '                               " reqd_status = null" _
                        '                             & " where reqd_oid = '" + ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    End If
                        'Next
                        ''******************************************************

                        For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then

                                'update boqs================================================
                                'kurangi dulu dengan kondisi semula dan set kembali ke kondisi berikutnya
                                'har 20110617
                                Dim _boqs_oid As String
                                _boqs_oid = ""
                                Dim sSql As String

                                sSql = "SELECT  " _
                                       & "  reqd_boqs_oid " _
                                       & "FROM " _
                                       & "   public.reqd_det  " _
                                       & "WHERE " _
                                       & "  reqd_oid='" & ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid").ToString & "'"

                                Dim dt As New DataTable
                                dt = master_new.PGSqlConn.GetTableData(sSql)

                                For Each dr As DataRow In dt.Rows
                                    _boqs_oid = SetString(dr(0))
                                Next

                                If _boqs_oid <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update boqs_stand set boqs_qty_po=coalesce(boqs_qty_po,0) - " _
                                                & SetDbl(ds_update_related.Tables(0).Rows(i).Item("pod_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            End If
                        Next
                        '================================================

                        'update boqs================================================
                        'krn kemungkinan bisa dobel maka update dr seq paling atas
                        'har 20110617
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                Dim _boqs_oid As String
                                _boqs_oid = ""
                                Dim sSql As String

                                sSql = "SELECT  " _
                                    & "  reqd_boqs_oid " _
                                    & "FROM " _
                                    & "   public.reqd_det  " _
                                    & "WHERE " _
                                    & "  reqd_oid='" & ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString & "'"


                                Dim dt As New DataTable
                                dt = master_new.PGSqlConn.GetTableData(sSql)

                                For Each dr As DataRow In dt.Rows
                                    _boqs_oid = SetString(dr(0))
                                Next

                                If _boqs_oid <> "" Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update boqs_stand set boqs_qty_po=coalesce(boqs_qty_po,0) + " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            End If
                        Next
                        '================================================


                        'dicoment agar bs di edit
                        'har 20110623
                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table receive
                        'kalau sudah relasi ke table receive jadi nya error...dan harusnya tidak didelete

                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from pod_det where pod_po_oid = '" + _po_oid_mstr + "'"
                        .Command.CommandText = "delete from pod_det where coalesce((select count(*) as jml from rcvd_det where rcvd_pod_oid=pod_oid),0) = 0 and pod_po_oid = '" + _po_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cmt_mstr where cmt_ref_code = '" + ds.Tables(0).Rows(row).Item("po_code").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pod_term_receive where podtr_po_oid = '" + ds.Tables(0).Rows(row).Item("po_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pod_term_payment where podtp_po_oid = '" + ds.Tables(0).Rows(row).Item("po_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from podd_doc where podd_po_oid = '" + _po_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

                            Dim _po_ppn, _po_pph As Double

                            _po_ppn = 0
                            _po_pph = 0


                            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                            Else
                                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
                            End If

                            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
                            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

                            _po_total_temp = ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
                            _po_ppn = (_po_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
                            _po_pph = (_po_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))


                            _pod_qty_receive = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_qty_receive")) = True, 0, ds_edit.Tables(0).Rows(i).Item("pod_qty_receive"))
                            '_pod_oid = Guid.NewGuid.ToString
                            If SetNumber(ds_edit.Tables(0).Rows(i).Item("status_receive")) = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.pod_det " _
                                                    & "( " _
                                                    & "  pod_oid, " _
                                                    & "  pod_dom_id, " _
                                                    & "  pod_en_id, " _
                                                    & "  pod_add_by, " _
                                                    & "  pod_add_date, " _
                                                    & "  pod_po_oid, " _
                                                    & "  pod_seq, " _
                                                    & "  pod_reqd_oid, " _
                                                    & "  pod_si_id, " _
                                                    & "  pod_pt_id, " _
                                                    & "  pod_pt_desc1, " _
                                                    & "  pod_pt_desc2, " _
                                                    & "  pod_memo, " _
                                                    & "  pod_rmks, " _
                                                    & "  pod_end_user, " _
                                                    & "  pod_qty, " _
                                                    & "  pod_um, " _
                                                    & "  pod_cost, " _
                                                    & "  pod_disc, " _
                                                    & "  pod_sb_id, " _
                                                    & "  pod_cc_id, " _
                                                    & "  pod_pjc_id, " _
                                                    & "  pod_need_date, " _
                                                    & "  pod_due_date, " _
                                                    & "  pod_um_conv, " _
                                                    & "  pod_qty_real, " _
                                                    & "  pod_taxable, " _
                                                    & "  pod_tax_inc, " _
                                                    & "  pod_tax_class, " _
                                                    & "  pod_loc_id,pod_ppn,pod_pph,pod_pod_parent_oid, " _
                                                    & "  pod_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid")) & ",  " _
                                                    & master_new.ClsVar.sdom_id & ",  " _
                                                    & po_en_id.EditValue & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString)) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_pt_id")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc2")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_memo")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_rmks")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_end_user")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_disc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_sb_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_cc_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_pjc_id")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("pod_need_date")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("pod_due_date")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_um_conv")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty_real")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_taxable")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_tax_inc")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_loc_id")) & ",  " _
                                                    & SetDbl(_po_ppn) & ", " _
                                                    & SetDbl(_po_pph) & ", " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_pod_parent_oid")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update relation PR apabila terdapat relasi pr
                                'by sys 20110418 di non active-kan karena sudah terdapat revisi codingan diatas...
                                'If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then

                                '    .Command.CommandType = CommandType.Text

                                '    .Command.CommandText = "update reqd_det set reqd_qty_processed =  " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                '                         & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"

                                '    '.Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString) _
                                '    '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"

                                '    ssqls.Add(.Command.CommandText)
                                '    .Command.ExecuteNonQuery()
                                '    .Command.Parameters.Clear()
                                'End If

                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.pod_det   " _
                                                    & "SET  " _
                                                    & "  pod_pt_desc1 = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc1")) & ",  " _
                                                    & "  pod_pt_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_pt_id")) & ",  " _
                                                    & "  pod_pt_desc2 = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc2")) & ",  " _
                                                    & "  pod_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_si_id")) & ",  " _
                                                    & "  pod_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_rmks")) & ",  " _
                                                    & "  pod_end_user = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_end_user")) & ",  " _
                                                    & "  pod_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & "  pod_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                    & "  pod_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                    & "  pod_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_disc")) & ",  " _
                                                    & "  pod_sb_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_sb_id")) & ",  " _
                                                    & "  pod_cc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_cc_id")) & ",  " _
                                                    & "  pod_pjc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_pjc_id")) & ",  " _
                                                    & "  pod_need_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_need_date")) & ",  " _
                                                    & "  pod_due_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_due_date")) & ",  " _
                                                    & "  pod_um_conv = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_um_conv")) & ",  " _
                                                    & "  pod_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty_real")) & ",  " _
                                                    & "  pod_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_taxable")) & ",  " _
                                                    & "  pod_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_tax_inc")) & ",  " _
                                                    & "  pod_memo = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_memo")) & ",  " _
                                                    & "  pod_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")) & ",  " _
                                                    & "  pod_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_loc_id")) & ",  " _
                                                    & "  pod_ppn=" & SetDbl(_po_ppn) & ", " _
                                                    & "  pod_pph=" & SetDbl(_po_pph) & ", " _
                                                    & "  pod_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  pod_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If SetString(po_parent_oid.Tag) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  pod_det " _
                                                    & "set pod_qty_child= coalesce(pod_qty_child,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) _
                                                    & " where pod_oid=" & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_pod_parent_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next

                        k = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            For j = 0 To ds_edit.Tables("detail_comment").Rows.Count - 1
                                If ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString = SetString(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_ref_oid")) Then
                                    If IsDBNull(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_comment")) = False Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                                            & "  public.cmt_mstr " _
                                                            & "( " _
                                                            & "  cmt_oid, " _
                                                            & "  cmt_dom_id, " _
                                                            & "  cmt_table, " _
                                                            & "  cmt_code, " _
                                                            & "  cmt_seq, " _
                                                            & "  cmt_type, " _
                                                            & "  cmt_ref_oid, " _
                                                            & "  cmt_ref_code, " _
                                                            & "  cmt_comment, " _
                                                            & "  cmt_dt " _
                                                            & ")  " _
                                                            & "VALUES ( " _
                                                            & SetSetring(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_oid").ToString) & ",  " _
                                                            & SetInteger(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_dom_id")) & ",  " _
                                                            & "'Purchase Order',  " _
                                                            & SetSetring(func_coll.get_transaction_number("CM", po_en_id.GetColumnValue("en_code"), "cmt_mstr", "cmt_code")) & ",  " _
                                                            & k & ",  " _
                                                            & SetSetringDB(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_type")) & ",  " _
                                                            & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString) & ",  " _
                                                            & SetSetring(ds.Tables(0).Rows(row).Item("po_code").ToString) & ",  " _
                                                            & SetSetringDB(ds_edit.Tables("detail_comment").Rows(j).Item("cmt_comment")) & ",  " _
                                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                            & ");"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                        k = k + 1
                                    End If
                                End If
                            Next
                        Next

                        For i = 0 To ds_edit.Tables("detail_receive").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pod_term_receive " _
                                            & "( " _
                                            & "  podtr_oid, " _
                                            & "  podtr_po_oid, " _
                                            & "  podtr_seq, " _
                                            & "  podtr_date, " _
                                            & "  podtr_qty, " _
                                            & "  podtr_qty_percent " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetDateNTime00(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_date")) & ",  " _
                                            & SetDec(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_qty")) & ",  " _
                                            & SetDec(ds_edit.Tables("detail_receive").Rows(i).Item("podtr_qty_percent")) & "  " _
                                            & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        For i = 0 To ds_edit.Tables("detail_payment").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                               & "  public.pod_term_payment " _
                                               & "( " _
                                               & "  podtp_oid, " _
                                               & "  podtp_po_oid, " _
                                               & "  podtp_seq, " _
                                               & "  podtp_date, " _
                                               & "  podtp_amount, " _
                                               & "  podtp_amount_percent " _
                                               & ")  " _
                                               & "VALUES ( " _
                                               & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                               & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                               & SetInteger(i) & ",  " _
                                               & SetDateNTime00(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_date")) & ",  " _
                                               & SetDec(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_amount")) & ",  " _
                                               & SetDec(ds_edit.Tables("detail_payment").Rows(i).Item("podtp_amount_percent")) & "  " _
                                               & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next


                        'Update Po mstr po_close_date = null apabila terjadi perubahan qty atau edit data po nambah line
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update po_mstr set po_close_date = null " _
                                             & " where po_code = coalesce((select distinct po_code from pod_det " _
                                             & " inner join po_mstr on po_oid = pod_po_oid " _
                                             & " where pod_po_oid = '" + _po_oid_mstr + "'" _
                                             & " and  pod_qty <> coalesce(pod_qty_receive,0)),'') "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
                        Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
                        Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")

                        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                            _ip_server = func_coll.get_conf_file("ftp_doc_server_online")

                        End If

                        Dim r As Integer = 0
                        For Each dr As DataRow In dt_edit_doc.Rows

                            If SetString(dr("podd_file")) <> "" Then
                                If SetString(dr("podd_file")).Contains("\") Then

                                    Dim _nama_file As String = ""
                                    _nama_file = IO.Path.GetFileName(dr("podd_file")).Replace(" ", "_")
                                    _nama_file = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") & "_" & _nama_file

                                    'Dim _error As New ArrayList

                                    'Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                                    'If Upload(dr("podd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                    '    sqlTran.Rollback()
                                    '    Box("Upload error : " & _error.Item(0))
                                    '    edit = False

                                    'End If

                                    Dim _sukses As Boolean = False

                                    If func_coll.get_conf_file("doc_upload_mode") = "http" Then
                                        Try
                                            Dim _url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & "upload.php?nama_file=" & _nama_file
                                            'Box(_url)
                                            HttpUploadFile(_url, dr("podd_file"), "files", "multipart/form-data")

                                            _sukses = True
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try
                                    Else
                                        Dim _error As New ArrayList
                                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)

                                        If Upload(dr("podd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                            sqlTran.Rollback()
                                            'MessageBox.Show(ex.Message)
                                            Box("Upload error : " & _error.Item(0))
                                            _sukses = False
                                            '    'Return False
                                            '    'Exit Function
                                        Else
                                            _sukses = True
                                        End If
                                    End If


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                            & "  public.podd_doc " _
                                            & "( " _
                                            & "  podd_oid, " _
                                            & "  podd_po_oid, " _
                                            & "  podd_file, " _
                                            & "  podd_seq " _
                                            & ") " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                            & SetSetring(_nama_file) & ",  " _
                                            & SetInteger(r) & "  " _
                                            & ")"


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                Else

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                            & "  public.podd_doc " _
                                            & "( " _
                                            & "  podd_oid, " _
                                            & "  podd_po_oid, " _
                                            & "  podd_file, " _
                                            & "  podd_seq " _
                                            & ") " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                            & SetSetring(dr("podd_file")) & ",  " _
                                            & SetInteger(r) & "  " _
                                            & ")"

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If
                            End If


                            r = r + 1
                        Next

                        If _conf_value = "1" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(po_en_id.EditValue) & ",  " _
                                                        & SetSetring(po_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_po_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")) & ",  " _
                                                        & SetSetring("Purchase Order") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'Return False
                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, po_en_id.EditValue, 2, _po_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code"), po_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit PO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code"))
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
                        set_row(_po_oid_mstr, "po_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select coalesce(pod_qty_receive,0) as pod_qty_receive from pod_det " + _
                           " where pod_po_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "pod_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("pod_qty_receive") > 0 Then
                MessageBox.Show("Can't Delete Receive Purchase Order...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
    End Function

    Public Overrides Function delete_data() As Boolean
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

        If _conf_value = 1 Then
            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) <> "D" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        End If

        Dim i As Integer
        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            load_data_grid_detail()
            Dim ssql As String
            'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_parent_oid")) <> "" Then


            '    ssql = "SELECT  " _
            '        & "  public.pod_det.pod_oid, " _
            '        & "  public.pod_det.pod_dom_id, " _
            '        & "  public.pod_det.pod_en_id, " _
            '        & "  public.en_mstr.en_desc, " _
            '        & "  public.pod_det.pod_add_by, " _
            '        & "  public.pod_det.pod_add_date, " _
            '        & "  public.pod_det.pod_upd_by, " _
            '        & "  public.pod_det.pod_upd_date, " _
            '        & "  public.pod_det.pod_po_oid, " _
            '        & "  public.pod_det.pod_seq, " _
            '        & "  public.pod_det.pod_reqd_oid,pod_qty_child, " _
            '        & "  req_mstr_relation.req_code as req_code_relation, " _
            '        & "  public.pod_det.pod_si_id, " _
            '        & "  public.si_mstr.si_desc, " _
            '        & "  public.pod_det.pod_pt_id, " _
            '        & "  public.pt_mstr.pt_code, " _
            '        & "  public.pt_mstr.pt_desc1, " _
            '        & "  public.pt_mstr.pt_desc2, " _
            '        & "  public.pod_det.pod_pt_desc1, " _
            '        & "  public.pod_det.pod_pt_desc2, " _
            '        & "  public.pod_det.pod_memo, " _
            '        & "  public.pod_det.pod_rmks, " _
            '        & "  public.pod_det.pod_end_user, " _
            '        & "  public.pod_det.pod_qty, " _
            '        & "  public.pod_det.pod_qty_receive, " _
            '        & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
            '        & "  public.pod_det.pod_qty_invoice, " _
            '        & "  public.pod_det.pod_um, " _
            '        & "  public.code_mstr.code_name, " _
            '        & "  public.pod_det.pod_cost, " _
            '        & "  public.pod_det.pod_disc, " _
            '        & "  public.pod_det.pod_need_date, " _
            '        & "  public.pod_det.pod_due_date, " _
            '        & "  public.pod_det.pod_um_conv, " _
            '        & "  public.pod_det.pod_qty_real, " _
            '        & "  public.pod_det.pod_pt_class, " _
            '        & "  public.pod_det.pod_status, " _
            '        & "  public.pod_det.pod_dt, " _
            '        & "  public.pod_det.pod_sb_id,pod_ppn,pod_pph,pod_pod_parent_oid, " _
            '        & "  sb_desc, " _
            '        & "  public.pod_det.pod_cc_id, " _
            '        & "  cc_desc, " _
            '        & "  loc_desc, " _
            '        & "  public.pod_det.pod_pjc_id, " _
            '        & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
            '        & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
            '        & "  taxclass_mstr.code_name as pod_tax_class_name " _
            '        & "  FROM " _
            '        & "  public.pod_det " _
            '        & "  LEFT OUTER JOIN public.loc_mstr ON (public.pod_det.pod_loc_id = public.loc_mstr.loc_id) " _
            '        & "  INNER JOIN public.sb_mstr ON (public.pod_det.pod_sb_id = public.sb_mstr.sb_id) " _
            '        & "  INNER JOIN public.cc_mstr ON (public.pod_det.pod_cc_id = public.cc_mstr.cc_id) " _
            '        & "  INNER JOIN public.pjc_mstr ON (public.pod_det.pod_pjc_id = public.pjc_mstr.pjc_id) " _
            '        & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
            '        & "  INNER JOIN public.si_mstr ON (public.pod_det.pod_si_id = public.si_mstr.si_id) " _
            '        & "  INNER JOIN public.pt_mstr ON (public.pod_det.pod_pt_id = public.pt_mstr.pt_id) " _
            '        & "  INNER JOIN public.code_mstr ON (public.pod_det.pod_um = public.code_mstr.code_id) " _
            '        & "  INNER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
            '        & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.pod_det.pod_tax_class " _
            '        & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.pod_det.pod_reqd_oid               " _
            '        & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
            '        & "  where po_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_parent_oid").ToString & "'"

            '    Dim dt_so_parent As New DataTable
            '    dt_so_parent = GetTableData(ssql)
            'End If
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("pod_po_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString Then

                                    If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_parent_oid").ToString) <> "" Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update  " _
                                                            & "  pod_det " _
                                                            & "set pod_qty_child= coalesce(pod_qty_child,0) - " & SetDbl(ds.Tables("detail").Rows(i).Item("pod_qty")) _
                                                            & " where pod_oid=" & SetSetring(ds.Tables("detail").Rows(i).Item("pod_pod_parent_oid").ToString)

                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If


                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("pod_reqd_oid").ToString) = False Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + SetDec(ds.Tables("detail").Rows(i).Item("pod_qty").ToString) + ", " + _
                                                               " reqd_status = null" _
                                                             & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("pod_reqd_oid").ToString + "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("pod_reqd_oid").ToString + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If

                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("pod_reqd_oid").ToString) = False Then
                                        'update boqs================================================
                                        'krn kemungkinan bisa dobel maka update dr seq paling atas
                                        'har 20110608

                                        Dim _boqs_oid As String
                                        _boqs_oid = ""
                                        'Dim sSql As String
                                        ssql = "SELECT  " _
                                                    & "  b.reqd_boqs_oid " _
                                                    & "FROM " _
                                                    & "  public.pod_det a " _
                                                    & "  INNER JOIN public.reqd_det b ON (a.pod_reqd_oid = b.reqd_oid) " _
                                                    & "WHERE " _
                                                    & "  a.pod_oid='" & ds.Tables("detail").Rows(i).Item("pod_oid").ToString & "'"
                                        Dim dt As New DataTable
                                        dt = master_new.PGSqlConn.GetTableData(ssql)

                                        For Each dr As DataRow In dt.Rows
                                            _boqs_oid = SetString(dr(0))
                                        Next

                                        If _boqs_oid <> "" Then
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update boqs_stand set boqs_qty_po=coalesce(boqs_qty_po,0) - " _
                                                        & SetDbl(ds.Tables("detail").Rows(i).Item("pod_qty")) & " where boqs_oid = '" & _boqs_oid & "'"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        End If
                                        '================================================
                                    End If
                                End If
                            Next

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from po_mstr where po_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from cmt_mstr where cmt_ref_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete PO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code"))
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
#End Region

#Region "gv_edit"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _pod_qty, _pod_qty_real, _pod_um_conv, _pod_qty_cost, _pod_cost, _pod_disc, _pod_qty_receive As Double
        _pod_um_conv = 1
        _pod_qty = 1
        _pod_cost = 0
        _pod_disc = 0

        If e.Column.Name = "pod_qty" Then
            '********* Cek Qty Processed
            Try
                _pod_qty_receive = (gv_edit.GetRowCellValue(e.RowHandle, "pod_qty_receive"))
            Catch ex As Exception
            End Try

            If e.Value < _pod_qty_receive Then
                MessageBox.Show("Qty PO Can't Lower Than Qty Receive..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            '********************************

            Try
                _pod_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "pod_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _pod_cost = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost"))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = (gv_edit.GetRowCellValue(e.RowHandle, "pod_disc"))
            Catch ex As Exception
            End Try

            _pod_qty_real = e.Value * _pod_um_conv
            _pod_qty_cost = (e.Value * _pod_cost) - (e.Value * _pod_cost * _pod_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_real", _pod_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_cost" Then
            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_disc")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (e.Value * _pod_qty) - (e.Value * _pod_qty * _pod_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_disc" Then
            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_cost")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (_pod_cost * _pod_qty) - (_pod_cost * _pod_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_um_conv" Then
            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            _pod_qty_real = e.Value * _pod_qty

            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_real", _pod_qty_real)
        ElseIf e.Column.Name = "pod_taxable" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "pod_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "pod_tax_inc", "N")
                gv_edit.SetRowCellValue(e.RowHandle, "pod_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit.SetRowCellValue(e.RowHandle, "pod_tax_class_name", "NON-TAX")
            End If
        ElseIf e.Column.Name = "pod_tax_inc" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "pod_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "pod_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "pod_tax_inc", "N")
            End If
        ElseIf e.Column.Name = "pod_memo" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "pt_type").ToString.ToUpper = "E" And e.Value <> "M" Then
                gv_edit.SetRowCellValue(e.RowHandle, "pod_memo", "M")
                Exit Sub
            End If
        End If
    End Sub

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
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "pod_en_id")
        Dim _pod_si_id As Integer = gv_edit.GetRowCellValue(_row, "pod_si_id")
        'Dim _pod_pt_id As Integer = gv_edit.GetRowCellValue(_row, "pod_pt_id")

        If _col = "req_code_relation" Then
            Dim frm As New FRequisitionSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._si_id = _pod_si_id
            frm._ptnr_id = po_ptnr_id.EditValue
            frm._is_internal = po_is_internal.EditValue
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "sb_desc" Then
            Dim frm As New FSubAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "cc_desc" Then
            Dim frm As New FCostCenterSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pjc_desc" Then
            Dim frm As New FProjectAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._pt_id = gv_edit.GetRowCellValue(_row, "pod_pt_id")
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch()
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pod_tax_class_name" Then
            If gv_edit.GetRowCellValue(_row, "pod_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Dim _pod_qty_receive As Double = 0

        'Try
        '    _pod_qty_receive = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pod_qty_receive")))
        'Catch ex As Exception
        'End Try

        'If _pod_qty_receive <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If

        cek_receive_row()
        Try
            gv_comment.Columns("cmt_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cmt_ref_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("pod_oid").ToString & "'")
            gv_comment.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        'Dim _pod_qty_receive As Double = 0

        'Try
        '    _pod_qty_receive = ((gv_edit.GetRowCellValue(0, "pod_qty_receive")))
        'Catch ex As Exception
        'End Try

        'If _pod_qty_receive <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        cek_receive_row()
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        Dim _new_uuid As Guid = Guid.NewGuid
        With gv_edit
            .SetRowCellValue(e.RowHandle, "pod_oid", _new_uuid.ToString)
            .SetRowCellValue(e.RowHandle, "pod_en_id", po_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", po_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "pod_si_id", po_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", po_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "pod_qty", 0)
            .SetRowCellValue(e.RowHandle, "pod_cost", 0)
            .SetRowCellValue(e.RowHandle, "pod_disc", 0)
            .SetRowCellValue(e.RowHandle, "pod_sb_id", po_sb_id.EditValue)
            .SetRowCellValue(e.RowHandle, "sb_desc", po_sb_id.GetColumnValue("sb_desc"))
            .SetRowCellValue(e.RowHandle, "pod_cc_id", po_cc_id.EditValue)
            .SetRowCellValue(e.RowHandle, "cc_desc", po_cc_id.GetColumnValue("cc_desc"))
            .SetRowCellValue(e.RowHandle, "pod_pjc_id", po_pjc_id.EditValue)
            .SetRowCellValue(e.RowHandle, "pjc_desc", po_pjc_id.GetColumnValue("pjc_desc"))
            .SetRowCellValue(e.RowHandle, "pod_taxable", IIf(po_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "pod_tax_inc", IIf(po_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "pod_tax_class", po_tax_class.EditValue)
            .SetRowCellValue(e.RowHandle, "pod_tax_class_name", po_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "pod_need_date", po_need_date.DateTime)
            .SetRowCellValue(e.RowHandle, "pod_due_date", po_due_date.DateTime)
            .SetRowCellValue(e.RowHandle, "pod_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "pod_qty_real", 0)
            .SetRowCellValue(e.RowHandle, "pod_qty_cost", 0)
            .BestFitColumns()
        End With
    End Sub
#End Region

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
        Dim _pod_cost As Double = 0

        ds_edit.Tables(0).AcceptChanges()
        gv_edit.UpdateCurrentRow()

        If ds_edit.Tables.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")) = True, 0, func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
            Else
                _tax_rate = 0
            End If

            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _dpp = _dpp + (ds_edit.Tables(0).Rows(i).Item("pod_qty") * _pod_cost)
            _dpp_line = (ds_edit.Tables(0).Rows(i).Item("pod_qty") * _pod_cost)
            _discount = _discount + (ds_edit.Tables(0).Rows(i).Item("pod_qty") * _pod_cost * ds_edit.Tables(0).Rows(i).Item("pod_disc"))
            _discount_line = (ds_edit.Tables(0).Rows(i).Item("pod_qty") * _pod_cost * ds_edit.Tables(0).Rows(i).Item("pod_disc"))
            _ppn = _ppn + (_tax_rate * (_dpp_line - _discount_line))

        Next

        te_dpp.EditValue = _dpp
        te_discount.EditValue = _discount
        te_ppn.EditValue = _ppn
        te_total.EditValue = _dpp - _discount + _ppn
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_en_id")
        _type = 2
        _table = "po_mstr"
        _initial = "po"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  po_oid, " _
            & "  po_dom_id, " _
            & "  po_en_id, " _
            & "  po_upd_date, " _
            & "  po_upd_by, " _
            & "  po_add_date, " _
            & "  po_add_by, " _
            & "  po_code, " _
            & "  po_ptnr_id, " _
            & "  po_cmaddr_id, " _
            & "  po_date, " _
            & "  po_need_date, " _
            & "  po_due_date, " _
            & "  po_rmks, " _
            & "  po_sb_id, " _
            & "  po_cc_id, " _
            & "  po_si_id, " _
            & "  po_pjc_id, " _
            & "  po_is_internal, " _
            & "  po_close_date, " _
            & "  po_total, " _
            & "  po_tran_id, " _
            & "  coalesce(po_trans_id,'') as po_trans_id, " _
            & "  po_credit_term, " _
            & "  po_taxable, " _
            & "  po_tax_inc, " _
            & "  po_tax_class, " _
            & "  po_cu_id, " _
            & "  po_exc_rate, " _
            & "  po_trans_rmks, " _
            & "  po_total_ppn, " _
            & "  po_freight, " _
            & "  po_total_pph, " _
            & "  po_status_cash, " _
            & "  ptnr_name, " _
            & "  ptnra_line_1, " _
            & "  ptnra_line_2, " _
            & "  ptnra_line_3, " _
            & "  ptnra_line, ptnrac_contact_name, " _
            & "  ptnra_phone_1, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  tax_class_mstr.code_name as tax_class_name, " _
            & "  cu_name, " _
            & "  pod_oid," _
            & "  pod_pt_id, " _
            & "  pod_qty, " _
            & "  pod_cost, " _
            & "  pod_disc, " _
            & "  pod_rmks,pod_seq, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  cc_desc, " _
            & "  um_master.code_code as um_name, " _
            & "  creditterm_mstr.code_name as creditterms_name,  " _
            & "  cmt_ref_oid, cmt_comment, cmt_type , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.po_mstr " _
            & "  inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "  left outer join ptnrac_cntc on addrc_ptnra_oid  = ptnra_oid " _
            & "  inner join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
            & "  inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = po_tax_class " _
            & "  inner join cu_mstr on cu_id = po_cu_id " _
            & "  inner join cc_mstr on cc_id = po_cc_id " _
            & "  inner join pod_det on pod_po_oid = po_oid " _
            & "  inner join pt_mstr on pt_id = pod_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = pod_um  " _
            & "  inner join code_mstr as creditterm_mstr on po_mstr.po_credit_term = creditterm_mstr.code_id" _
            & "  left outer join code_mstr addr_type on (addr_type.code_id = ptnra_addr_type and addr_type.code_name ~~* 'ship to')    " _
            & "  left outer join cmt_mstr on pod_oid = cmt_ref_oid  " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = po_oid  " _
            & "  where po_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") + "'" _
            & "  " _
            & "  order by po_code, pod_seq, cmt_seq "

        '& "  and ptnra_line = 1 and ptnrac_seq = 1 " _
        '& "  order by po_code "
        ' & "  and ptnrac_seq = 1 " _


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        'frm._report = "XRPurchaseOrderPrintOut"

        If func_coll.get_conf_file("wf_purchase_order") = "1" And ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_name") <> "*" Then
            frm._report = "XRPurchaseOrderPrintOut"
        Else
            frm._report = "XRPurchaseOrderPrintOut"
        End If

        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        frm.ShowDialog()



    End Sub
    Public Function preview_export(ByVal par_filename As String) As Boolean
        Try
            Dim _sql As String

            _sql = "SELECT  " _
                & "  po_oid, " _
                & "  po_dom_id, " _
                & "  po_en_id, " _
                & "  po_upd_date, " _
                & "  po_upd_by, " _
                & "  po_add_date, " _
                & "  po_add_by, " _
                & "  po_code, " _
                & "  po_ptnr_id, " _
                & "  po_cmaddr_id, " _
                & "  po_date, " _
                & "  po_need_date, " _
                & "  po_due_date, " _
                & "  po_rmks, " _
                & "  po_sb_id, " _
                & "  po_cc_id, " _
                & "  po_si_id, " _
                & "  po_pjc_id, " _
                & "  po_is_internal, " _
                & "  po_close_date, " _
                & "  po_total, " _
                & "  po_tran_id, " _
                & "  coalesce(po_trans_id,'') as po_trans_id, " _
                & "  po_credit_term, " _
                & "  po_taxable, " _
                & "  po_tax_inc, " _
                & "  po_tax_class, " _
                & "  po_cu_id, " _
                & "  po_exc_rate, " _
                & "  po_trans_rmks, " _
                & "  po_total_ppn, " _
                & "  po_freight, " _
                & "  po_total_pph, " _
                & "  po_status_cash, " _
                & "  ptnr_name, " _
                & "  ptnra_line_1, " _
                & "  ptnra_line_2, " _
                & "  ptnra_line_3, " _
                & "  ptnra_line, ptnrac_contact_name, " _
                & "  ptnra_phone_1, " _
                & "  cmaddr_name, " _
                & "  cmaddr_line_1, " _
                & "  cmaddr_line_2, " _
                & "  cmaddr_line_3, " _
                & "  tax_class_mstr.code_name as tax_class_name, " _
                & "  cu_name, " _
                & "  pod_oid," _
                & "  pod_pt_id, " _
                & "  pod_qty, " _
                & "  pod_cost, " _
                & "  pod_disc, " _
                & "  pod_rmks,pod_seq, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2, " _
                & "  pod_pt_desc1, " _
                & "  coalesce(pod_pt_desc2,'') || ' (' || coalesce(pod_end_user,'') || ')' as pod_pt_desc2, " _
                & "  cc_desc, " _
                & "  um_master.code_code as um_name, " _
                & "  creditterm_mstr.code_name as creditterms_name,  " _
                & "  cmt_ref_oid, cmt_comment, cmt_type , " _
                & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM  " _
                & "  public.po_mstr " _
                & "  inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
                & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                & "  left outer join ptnrac_cntc on addrc_ptnra_oid  = ptnra_oid " _
                & "  inner join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
                & "  inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = po_tax_class " _
                & "  inner join cu_mstr on cu_id = po_cu_id " _
                & "  inner join cc_mstr on cc_id = po_cc_id " _
                & "  inner join pod_det on pod_po_oid = po_oid " _
                & "  inner join pt_mstr on pt_id = pod_pt_id " _
                & "  inner join code_mstr um_master on um_master.code_id = pod_um  " _
                & "  inner join code_mstr as creditterm_mstr on po_mstr.po_credit_term = creditterm_mstr.code_id" _
                & "  left outer join code_mstr addr_type on (addr_type.code_id = ptnra_addr_type and addr_type.code_name ~~* 'ship to')    " _
                & "  left outer join cmt_mstr on pod_oid = cmt_ref_oid  " _
                & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = po_oid  " _
                & "  where po_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") + "'" _
                           & "  " _
                & "  order by po_code, pod_seq, cmt_seq "


            Dim rpt As New XRPurchaseOrderPrintOut
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong. Gagal email silahkan perbaiki data kemudian klik remainder email")
                    'Exit Function
                    Return False
                    Exit Function
                End If

                .DataSource = ds
                .DataMember = "Table"

                If IO.Directory.Exists(appbase() & "\export") = False Then
                    IO.Directory.CreateDirectory(appbase() & "\export")
                End If

                'export image
                Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = rpt.ExportOptions.Image

                ' Set Image-specific export options.
                imageOptions.Resolution = 72
                imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
                imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


                .ExportToImage(par_filename, imageOptions)


                'rubah ke export xls

                '.ExportToXls(par_filename)
            End With
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function

    'Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
    '    Try
    '        gv_comment.Columns("cmt_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("pod_oid"))
    '        gv_comment.BestFitColumns()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub gv_comment_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_comment.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position
        If _row_edit = -1 Then
            MsgBox("Detail is null.....")
        Else

            With gv_comment
                .SetRowCellValue(e.RowHandle, "cmt_oid", Guid.NewGuid.ToString)
                .SetRowCellValue(e.RowHandle, "cmt_comment", "")
                .SetRowCellValue(e.RowHandle, "cmt_dom_id", master_new.ClsVar.sdom_id.ToString)
                .SetRowCellValue(e.RowHandle, "cmt_table", "Purchase Order")
                .SetRowCellValue(e.RowHandle, "cmt_ref_oid", ds_edit.Tables(0).Rows(_row_edit).Item("pod_oid").ToString)
                .BestFitColumns()
            End With
        End If
    End Sub

    Private Sub gv_comment_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_comment.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_comment.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_comment.DeleteSelectedRows()
        End If
    End Sub

    Private Sub PasteCommentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteCommentToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim _row As Integer
        Dim max_row As Integer
        Try
            i = data.Replace(vbNewLine, "~")

            a = i.Split("~")
            max_row = a.GetUpperBound(0)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                If j <= max_row Then
                    _row = ds_edit.Tables("detail_comment").Rows.Count - 1
                    If j <= _row Then
                        ds_edit.Tables("detail_comment").Rows(j).Item("cmt_comment") = Trim(a(j).ToString)
                        'ds_edit.Tables(0).Rows(j).Item("cmt_comment") = a(j).ToString
                    End If
                End If
            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub cek_receive_row()
        Try

            Dim _nilai As Object
            _nilai = SetNumber(gv_edit.GetFocusedRowCellValue("status_receive"))

            If _nilai > 0 Then
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
            Else
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.Click
        cek_receive_row()

        Try
            gv_comment.Columns("cmt_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cmt_ref_oid='" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("pod_oid").ToString & "'")
            gv_comment.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub cancel_line()
        'walaupun
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString
        _colom = "po_trans_id"
        _table = "po_mstr"
        _criteria = "po_code"
        _initial = "po"
        _type = "po"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Cancel Line PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'Else
            '    If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
            '        MessageBox.Show("Can't Cancel Data That Has Been Approved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End If
        End If

        If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "X" Then
            MessageBox.Show("Can't Cancel Data That Has Been cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        If func_coll.get_conf_file("wf_purchase_order") = "1" Then
            If _trans_id = "D" Then
                MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
                Exit Sub
            Else
                Try
                    Using objcek As New master_new.WDABasepgsql("", "")
                        With objcek
                            .Connection.Open()
                            .Command = .Connection.CreateCommand
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "select count(po_code) as jml " _
                                                 & " from po_mstr " _
                                                 & " inner join pod_det on pod_po_oid = po_oid " _
                                                 & " where po_code ~~* '" + par_code + "' " _
                                                 & " and pod_qty_receive >= 1"

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
                    MessageBox.Show("Can't Cancel For Receipt PO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
        ElseIf func_coll.get_conf_file("wf_purchase_order") = "0" Then
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select count(po_code) as jml " _
                                             & " from po_mstr " _
                                             & " inner join pod_det on pod_po_oid = po_oid " _
                                             & " where po_code ~~* '" + par_code + "' " _
                                             & " and pod_qty_receive >= 1"

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
                MessageBox.Show("Can't Cancel For Receipt PO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        Else
            MessageBox.Show("Please Configure Your Setup Workflow...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                               " where wf_ref_code = '" + par_code + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update qty processed di pr
                        For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("pod_po_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString Then
                                'Update relation PR apabila terdapat relasi pr
                                If IsDBNull(ds.Tables("detail").Rows(i).Item("pod_reqd_oid")) = False Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) - " + ds.Tables("detail").Rows(i).Item("pod_qty").ToString _
                                                         & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("pod_reqd_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If

                            End If
                        Next
                        '-----------------------------------------------------

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Cancel PO " & par_code)
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

    Public Overrides Sub approve_line()
        _conf_value = func_coll.get_conf_file("wf_purchase_order")

        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString
        _colom = "po_trans_id"
        _table = "po_mstr"
        _criteria = "po_code"
        _initial = "po"
        _type = "po"
        _title = "Purchase Order"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_wf, _title)
    End Sub
    Public Function get_pod_det(ByVal par_po_oid As String) As DataSet
        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT pod_oid, pod_qty,pod_cost,pod_disc from pod_det " + _
                           " where pod_po_oid = " + SetSetring(par_po_oid.ToString())
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "aprv_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return ds_bantu
    End Function

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        Dim _trn_status, _pby_code, _sql As String


        If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ssqls As New ArrayList
        _pby_code = par_code
        _trn_status = "W"

        Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu, user_id_telegram As String

        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)
        user_phone = mf.get_phone(user_wf)
        user_id_telegram = mf.get_id_telegram(user_wf)

        Dim _ds_pod_det As New DataSet
        _ds_pod_det = get_pod_det(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString)

        Try
            Using objappove As New master_new.WDABasepgsql("", "")
                With objappove
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                                   " and wf_seq = 0"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        ElseIf get_status_wf(par_code.ToString()) > 0 Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            _sql = "update wf_mstr set " + _
                                   " wf_iscurrent = 'Y', " + _
                                   " wf_wfs_id = '0', " + _
                                   " wf_desc = '', " + _
                                   " wf_date_to = null, " + _
                                   " wf_aprv_user = '', " + _
                                   " wf_aprv_date = null " + _
                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                   " and wf_seq = 0 "
                            ssqls.Add(.Command.CommandText)
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = _sql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)
                        filename = appbase() & "\export\" + par_code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"

                        'filename = appbase() & "\export\" + par_code + Now.ToString("_yyyyMMdd_HHmmss") + ".xls"
                        'ExportTo(par_gv, New ExportXlsProvider(filename))

                        If func_coll.get_conf_file("send_email") = "1" Then
                            If preview_export(filename) = True Then

                                If user_wf_email <> "" Then
                                    If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then

                                    Else
                                        Box("Email Failed, Use remainder mail to send email again")
                                    End If

                                    Try
                                        IO.File.Delete(filename)
                                    Catch ex As Exception
                                    End Try
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If
                            End If
                        End If

                        If func_coll.get_conf_file("send_sms") = "1" Then
                            If sent_sms(user_phone, "Mohon di approve PO : " & par_code & " entity : " & master_new.PGSqlConn.GetRowInfo("select en_desc from en_mstr where en_id = (select po_en_id from po_mstr where po_oid='" & par_oid & "') ")(0).ToString & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
                                Box("Gagal mengirim pesan")
                                'Exit Sub
                            Else

                            End If
                        End If


                        If func_coll.get_conf_file("send_telegram") = "1" Then
                            Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval") & "?pesan=approve@PO@" & func_coll.get_conf_file("syspro_approval_code") & "@" & par_code _
                            & "@" & user_id_telegram & "@" & user_wf

                            Dim result As String
                            result = mf.run_get_to_api(_alamat_api & "")

                            If SetString(result).Contains("success") Then
                                '_MakeReport("Data empty")

                            Else
                                Box("Gagal notif telegram")
                                Exit Sub
                            End If
                        End If





                        'type_user_wf = mf.get_type_user_wf(par_code, 0)
                        'If type_user_wf = 0 Then 'Jika typenya user
                        '    user_wf = mf.get_user_wf(par_code, 0)
                        '    user_wf_email = mf.get_email_address(user_wf)

                        '    If user_wf_email <> "" Then
                        '        filename = "c:\syspro\" + par_code + ".xls"
                        '        ExportTo(par_gv, New ExportXlsProvider(filename))

                        '        format_email_bantu = mf.format_email(user_wf, par_code, par_type)
                        '        mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        '    Else
                        '        MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        '    End If
                        'ElseIf type_user_wf = 1 Then
                        '    Dim ds_bantu As New DataSet
                        '    Dim a As Integer
                        '    Dim _user_group_name As String

                        '    _user_group_name = mf.get_user_wf(par_code, 0)
                        '    ds_bantu = mf.load_user_in_group(_user_group_name)

                        '    filename = "c:\syspro\" + par_code + ".xls"
                        '    ExportTo(par_gv, New ExportXlsProvider(filename))

                        '    For a = 0 To ds_bantu.Tables(0).Rows.Count - 1
                        '        user_wf_email = mf.get_email_address(ds_bantu.Tables(0).Rows(a).Item("wf_user_id"))

                        '        If user_wf_email <> "" Then
                        '            format_email_bantu = mf.format_email(ds_bantu.Tables(0).Rows(a).Item("wf_user_id"), par_code, par_type)
                        '            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        '        End If
                        '    Next
                        'End If

                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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


    Public Overrides Sub reminder_mail()
        'Dim user_wf, user_wf_email, filename, format_email_bantu As String

        '_conf_value = func_coll.get_conf_file("wf_purchase_order")

        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title, user_phone As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid")
        '_colom = "po_trans_id"
        '_table = "po_mstr"
        '_criteria = "po_code"
        '_initial = "po"
        '_type = "po"
        '_title = "Purchase Order"
        ''approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_wf, _title)

        'user_wf = mf.get_user_wf(_code, 0)
        'user_wf_email = mf.get_email_address(user_wf)

        'format_email_bantu = mf.format_email(user_wf, _code, _type)
        'user_phone = mf.get_phone(user_wf)

        ''filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".xls"
        'filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"
        ''ExportTo(par_gv, New ExportXlsProvider(filename))

        'If preview_export(filename) = True Then

        '    If user_wf_email <> "" Then

        '        If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(_title, _code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then
        '            If sent_sms(user_phone, "Mohon di approve PO : " & _code & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
        '                Box("Gagal mengirim pesan")
        '                'Exit Sub
        '            End If
        '            Box("Email success")

        '        Else
        '            Box("Email Failed")
        '        End If

        '        Try
        '            IO.File.Delete(filename)
        '        Catch ex As Exception

        '        End Try

        '    Else
        '        MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    End If
        'End If


        'ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        Try
            Dim selectedRowHandles As Integer() = gv_master.GetSelectedRows()
            If selectedRowHandles.Length > 0 Then
                For i As Integer = 0 To selectedRowHandles.Length - 1
                    If SetString(gv_master.GetRowCellValue(selectedRowHandles(i), "po_trans_id")) <> "W" Then
                        Box("Transaksi harus dalam status workflow (W)")
                        set_row(gv_master.GetRowCellValue(selectedRowHandles(i), "po_oid"), "po_oid")
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception

        End Try




        Try
            Dim selectedRowHandles As Integer() = gv_master.GetSelectedRows()

            If selectedRowHandles.Length > 0 Then
                'Move focus to the first selected row.
                'View.FocusedRowHandle = selectedRowHandles(0)
                'Copy the selection to the clipboard
                'View.CopyToClipboard()
                'Copy the selected company names to a Memo editor.
                'MemoEdit1.Text = ""
                For i As Integer = 0 To selectedRowHandles.Length - 1
                    'MsgBox(gv_master.GetRowCellValue(selectedRowHandles(i), "po_code"))
                    'MemoEdit1.Text += View.GetRowCellDisplayText(selectedRowHandles(i), colCompany) & vbCrLf

                    Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu, user_id_telegram As String
                    Dim par_code As String = gv_master.GetRowCellValue(selectedRowHandles(i), "po_code")

                    Dim _seq_id As Integer = 0
                    _seq_id = mf.get_user_wf_seq(par_code)

                    user_wf = mf.get_user_wf(par_code, _seq_id)

                    'user_wf = mf.get_user_wf(par_code, 0)
                    'user_wf_email = mf.get_email_address(user_wf)
                    'user_phone = mf.get_phone(user_wf)
                    user_id_telegram = mf.get_id_telegram(user_wf)

                    If func_coll.get_conf_file("send_telegram") = "1" Then
                        Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval") & "?pesan=approve@PO@" & func_coll.get_conf_file("syspro_approval_code") & "@" & par_code _
                        & "@" & user_id_telegram & "@" & user_wf

                        Dim result As String
                        result = mf.run_get_to_api(_alamat_api & "")

                        If SetString(result).Contains("success") Then
                            '_MakeReport("Data empty")

                        Else
                            Box("Gagal notif telegram")
                            Exit Sub
                        End If
                    End If

                Next
            End If

            Box("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_payment_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_payment.CellValueChanged
        Try
            If e.Column.Name = "podtp_amount_percent" Then
                gv_payment.SetRowCellValue(e.RowHandle, "podtp_amount", SetNumber(te_total.EditValue) * SetNumber(e.Value))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_detail_payment_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_payment.RowUpdated
        gv_payment.BestFitColumns()
    End Sub

    Private Sub gv_detail_receive_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_receive.RowUpdated
        gv_receive.BestFitColumns()
    End Sub

    Private Sub ClosePODetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosePODetailToolStripMenuItem.Click
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_close_date") Is System.DBNull.Value Then


            Close_PO_det(gv_detail.GetRowCellValue(gv_detail.FocusedRowHandle(), "pod_oid").ToString, gv_detail.GetRowCellValue(gv_detail.FocusedRowHandle(), "pod_status").ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString.ToUpper, _
                    ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code").ToString.ToUpper)



        Else
            MessageBox.Show("Can't Close data that PO has been closed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Public Sub Close_PO(ByVal par_colom As String, ByVal par_table As String, ByVal par_oid As String, ByVal par_code As String)

        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Close PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If

        If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "X" Then
            MessageBox.Show("Can't Close Data That Has Been cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        .Command.CommandText = "update " + par_table + " set  " _
                                            & "  po_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  po_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  po_close_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & " where po_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update pod_det set  " _
                                            & "  pod_status = 'C' " _
                                            & " where pod_po_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '-----------------------------------------------------

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Close PO " & par_code)
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
                        'set_row(Trim(par_oid), par_initial + "_oid")
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

    Public Sub Close_PO_det(ByVal par_oid_det As String, ByVal par_status As String, ByVal par_oid As String, ByVal par_code As String)

        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Close PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'Else
            '    If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
            '        MessageBox.Show("Can't Cancel Data That Has Been Approved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End If
        End If

        If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "X" Then
            MessageBox.Show("Can't Close Data That Has Been cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If SetString(par_status) = "C" Or SetString(par_status) = "c" Then
            MessageBox.Show("Can't Close Data That Has Been closed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(pod_status) as jml " _
                                         & " from pod_det " _
                                         & "  " _
                                         & " where pod_po_oid = '" + par_oid + "' AND upper(pod_status)='C' "

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

        'If _jml > 1 Then
        '    MessageBox.Show("Can't Cancel For Receipt PO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        If gv_detail.RowCount - _jml <= 1 Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update po_mstr set  " _
                                                & "  po_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  po_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                & "  po_close_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                & " where po_oid = '" + par_oid + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                        Else

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update po_mstr set  " _
                                                & "  po_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  po_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                & " where po_oid = '" + par_oid + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        End If



                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update pod_det set  " _
                                            & "  pod_status = 'C' " _
                                            & " where pod_oid = '" + par_oid_det + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        '-----------------------------------------------------

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Close PO " & par_code)
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
                        'set_row(Trim(par_oid), par_initial + "_oid")
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

    Private Sub po_parent_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles po_parent_oid.ButtonClick
        Try
            Dim frm As New FPurchaseOrderSearch
            frm.set_win(Me)
            frm._obj = po_parent_oid
            frm._en_id = po_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SendApprovalTelegramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendApprovalTelegramToolStripMenuItem.Click
        Try
            Dim selectedRowHandles As Integer() = gv_master.GetSelectedRows()

            If selectedRowHandles.Length > 0 Then
                'Move focus to the first selected row.
                'View.FocusedRowHandle = selectedRowHandles(0)
                'Copy the selection to the clipboard
                'View.CopyToClipboard()
                'Copy the selected company names to a Memo editor.
                'MemoEdit1.Text = ""
                For i As Integer = 0 To selectedRowHandles.Length - 1
                    'MsgBox(gv_master.GetRowCellValue(selectedRowHandles(i), "po_code"))
                    'MemoEdit1.Text += View.GetRowCellDisplayText(selectedRowHandles(i), colCompany) & vbCrLf

                    Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu, user_id_telegram As String
                    Dim par_code As String = gv_master.GetRowCellValue(selectedRowHandles(i), "po_code")

                    user_wf = mf.get_user_wf(par_code, 0)
                    'user_wf_email = mf.get_email_address(user_wf)
                    'user_phone = mf.get_phone(user_wf)
                    user_id_telegram = mf.get_id_telegram(user_wf)

                    If func_coll.get_conf_file("send_telegram") = "1" Then
                        Dim _alamat_api As String = "" & func_coll.get_conf_file("http_approval") & "?pesan=approve@PO@" & func_coll.get_conf_file("syspro_approval_code") & "@" & par_code _
                        & "@" & user_id_telegram & "@" & user_wf

                        Dim result As String
                        result = mf.run_get_to_api(_alamat_api & "")

                        If SetString(result).Contains("success") Then
                            '_MakeReport("Data empty")

                        Else
                            Box("Gagal notif telegram")
                            Exit Sub
                        End If
                    End If

                Next
            End If

            Box("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bt_tambah_dokumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_tambah_dokumen.Click
        Try

            If be_document.Text <> "" Then
                Dim _row As DataRow
                _row = dt_edit_doc.NewRow

                _row.Item("podd_file") = be_document.Text

                dt_edit_doc.Rows.Add(_row)
                dt_edit_doc.AcceptChanges()
                gv_edit_doc.BestFitColumns()
                be_document.Text = ""
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bt_hapus_dokumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_hapus_dokumen.Click
        Try

            Dim row As Integer = BindingContext(dt_edit_doc).Position
            dt_edit_doc.Rows.RemoveAt(row)
            dt_edit_doc.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try

            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim row As Integer = BindingContext(ds.Tables(0)).Position
            Dim ssql As String
            ssql = "SELECT  " _
                     & "  podd_file, " _
                     & "  podd_seq " _
                     & "FROM  " _
                     & "  public.podd_doc where podd_po_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("po_oid").ToString) _
                     & " order by podd_seq"


            'dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
            gc_detail_doc.DataSource = master_new.PGSqlConn.GetTableData(ssql)
            gv_detail_doc.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub be_document_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_document.ButtonClick
        Dim stream As FileStream
        Dim reader As BinaryReader
        Try
            Dim dlg As New OpenFileDialog
            dlg.Filter = "Image Files(*.jpeg;*.jpg;*.png)|*.JPEG;*.JPG;*.PNG|PDF files (*.pdf)|*.PDF"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                be_document.Text = dlg.FileName

                stream = New FileStream(be_document.Text, FileMode.Open)
                reader = New BinaryReader(stream)

                If stream.Length > 4000000 Then
                    Box("File max 4MB")
                    be_document.Text = ""
                    stream.Dispose()
                    Exit Sub
                End If
                stream.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub be_document_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles be_document.EditValueChanged

    End Sub

    Private Sub AddDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDocumentToolStripMenuItem.Click
        Dim stream As FileStream
        Dim reader As BinaryReader
        Try

            If master_new.ClsVar.sNama <> ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_add_by") Then
                Box("Can't proccess this menu, check permission..")
                Exit Sub
            End If


            Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
            Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
            Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")

            If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                _ip_server = func_coll.get_conf_file("ftp_doc_server_online")

            End If
            Dim _file_name As String = ""

            Dim dlg As New OpenFileDialog
            dlg.Filter = "Image Files(*.jpeg;*.jpg;*.png)|*.JPEG;*.JPG;*.PNG|PDF files (*.pdf)|*.PDF"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                _file_name = dlg.FileName

                stream = New FileStream(_file_name, FileMode.Open)
                reader = New BinaryReader(stream)

                If stream.Length > 4000000 Then
                    Box("File max 4MB")
                    be_document.Text = ""
                    stream.Dispose()
                    Exit Sub
                Else
                    stream.Dispose()
                    Dim _nama_file As String = ""
                    _nama_file = IO.Path.GetFileName(_file_name.Replace(" ", "_"))
                    _nama_file = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") & "_" & _nama_file

                    Dim _sukses As Boolean = False

                    If func_coll.get_conf_file("doc_upload_mode") = "http" Then
                        Try
                            Dim _url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & "upload.php?nama_file=" & _nama_file
                            'Box(_url)
                            HttpUploadFile(_url, _file_name, "files", "multipart/form-data")

                            _sukses = True
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Else
                        'Dim _error As New ArrayList
                        Dim _error As New ArrayList
                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                        'Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                        'If Upload(dr("reqdd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                        '    sqlTran.Rollback()
                        '    'MessageBox.Show(ex.Message)
                        '    Box("Upload error : " & _error.Item(0))
                        '    insert = False
                        '    '    'Return False
                        '    '    'Exit Function
                        'End If
                        If Upload(_file_name, "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                            _sukses = False
                        Else
                            _sukses = True
                        End If
                    End If

                   

                    If _sukses = False Then 'If Upload(_file_name, "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                        'sqlTran.Rollback()
                        'MessageBox.Show(ex.Message)
                        'Box("Upload error : " & _error.Item(0))
                        'insert = False
                        '    'Return False
                        '    'Exit Function
                        Box("Error upload")
                    Else
                        Dim sSQL As String
                        Dim ssqls As New ArrayList

                        sSQL = "INSERT INTO  " _
                                & "  public.podd_doc " _
                                & "( " _
                                 & "  podd_oid, " _
                                & "  podd_po_oid, " _
                                & "  podd_file, " _
                                & "  podd_seq " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid")) & ",  " _
                                & SetSetring(_nama_file) & ",  " _
                                & SetInteger(1) & "  " _
                                & ")"

                        ssqls.Add(sSQL)

                        If master_new.PGSqlConn.status_sync = True Then
                            'Dim _data As String = arr_to_str(ssqls)
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then

                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If

                        after_success()

                    End If

                End If

            End If
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RemoveDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveDocumentToolStripMenuItem.Click
        Try

            Dim sSQL As String
            Dim ssqls As New ArrayList

            sSQL = "DELETE from  " _
                    & "  public.podd_doc " _
                    & "where podd_oid='" & gv_detail_doc.GetRowCellValue(gv_detail_doc.FocusedRowHandle, "podd_oid").ToString & "'"

            ssqls.Add(sSQL)

            If master_new.PGSqlConn.status_sync = True Then
                'Dim _data As String = arr_to_str(ssqls)
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If
            after_success()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtReload.Click
        Try
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
                '    MessageBox.Show("Can't Edit Data That Has Been Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If
                MessageBox.Show("No access to this menu..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If MessageBox.Show("Are you sure to reset ?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssqls As New ArrayList

            Dim ssql As String

            ssql = "UPDATE public.po_mstr set po_trans_id='D' where po_oid='" _
                    & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString & "'"

            ssqls.Add(ssql)

            ssqls.Add(insert_log("Reset Purchase Order " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")))

            Dim ds_bantu As New DataSet
            If _conf_value = "1" Then
                ds_bantu = func_data.load_aprv_mstr(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_tran_id"))
            End If

            If _conf_value = "1" Then
                ssql = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString + "'"
                ssqls.Add(ssql)

                Dim j As Integer
                For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    ssql = "INSERT INTO  " _
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
                            & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_en_id")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_tran_id")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")) & ",  " _
                            & SetSetring("Purchase Order") & ",  " _
                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                            & SetInteger(0) & ",  " _
                            & IIf(j = 0, SetSetring("Y"), SetSetring("N")) & ",  " _
                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                            & ")"
                    ssqls.Add(ssql)

                Next
            End If


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            Box("Reload success")
            after_success()
            set_row(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString, "po_oid")

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ShowFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFileToolStripMenuItem.Click
        Try
            Dim Url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & gv_detail_doc.GetRowCellValue(gv_detail_doc.FocusedRowHandle, "podd_file")

            Process.Start(Url)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class

