Imports npgsql
Imports master_new.ModFunction

Public Class FPurchaseOrderFilm
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr, _pod_oid As String
    Dim ds_edit, ds_comment As DataSet
    Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim _conf_budget As String

    Dim _conf_value As String
    Dim mf As New master_new.ModFunction

#Region "Seting Awal"
    Private Sub FPurchaseOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        _conf_value = func_coll.get_conf_file("wf_purchase_order")

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

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_tran_mstr("po_mstr"))
        'po_tran_id.Properties.DataSource = dt_bantu
        'po_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        'po_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        'po_tran_id.ItemIndex = 0

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

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)
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

        add_column_copy(gv_master, "PO Film", "po_film", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "po_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "po_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "po_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "po_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "po_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

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
        'add_column_copy(gv_detail, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Height", "pod_height", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Width", "pod_width", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Receive", "pod_qty_receive", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Outstanding", "pod_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "pod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Film", "pod_cost_film", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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
        add_column_edit(gv_edit, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Is Memo", "pod_memo", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "pod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "End User", "pod_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pod_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "pod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Height", "pod_height", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Width", "pod_width", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "pod_um", False)
        add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Cost Film", "pod_cost_film", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Cost", "pod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Discount", "pod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

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
        add_column_edit(gv_edit, "UM Conversion", "pod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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
                    & "  public.bk_mstr.bk_code,po_film, " _
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
                    & " where po_film='Y' and po_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and po_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and po_en_id in (select user_en_id from tconfuserentity " _
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
            & "  public.pod_det.pod_memo, " _
            & "  public.pod_det.pod_rmks, " _
            & "  public.pod_det.pod_end_user, " _
            & "  public.pod_det.pod_height, " _
            & "  public.pod_det.pod_width, " _
            & "  public.pod_det.pod_qty, " _
            & "  public.pod_det.pod_qty_receive, " _
            & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
            & "  public.pod_det.pod_qty_invoice, " _
            & "  public.pod_det.pod_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.pod_det.pod_cost, pod_cost_film," _
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
            & "  loc_desc, " _
            & "  public.pod_det.pod_pjc_id, " _
            & "  pjc_desc, ((pod_qty * pod_cost ) - (pod_qty * pod_cost * pod_disc )) as pod_qty_cost, " _
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

        te_dpp.EditValue = 0.0
        te_ppn.EditValue = 0.0
        te_discount.EditValue = 0.0
        te_total.EditValue = 0.0
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

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
                            & "  public.pod_det.pod_qty, " _
                            & "  public.pod_det.pod_height, " _
                            & "  public.pod_det.pod_width, " _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost,pod_cost_film, " _
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

        ds_comment = New DataSet
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
                    .FillDataSet(ds_comment, "comment")
                    gc_comment.DataSource = ds_comment.Tables(0)
                    gv_comment.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True


        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
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

        '*********************
        'Cek UM
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_um")) = True Then
                MessageBox.Show("Unit Measure Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '*********************

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pod_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

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
        Dim _po_trans_id As String = ""
        Dim _po_total, _po_total_ppn, _po_total_pph, _pod_qty, _pod_cost, _pod_disc, _po_total_temp, _tax_rate As Double
        '_pod_height, _pod_width, lgsg ke pod_cost saja
        Dim i, k, j As Integer

        _po_code = func_coll.get_transaction_number("PO", po_en_id.GetColumnValue("en_code"), "po_mstr", "po_code")

        '******* Mencari Total PO, Total PPN, Total PPH
        _po_total = 0
        _po_total_ppn = 0
        _po_total_pph = 0
        _po_total_temp = 0
        _conf_value = func_coll.get_conf_file("wf_purchase_order")

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
            '_pod_height = ds_edit.Tables(0).Rows(i).Item("pod_height")
            '_pod_width = ds_edit.Tables(0).Rows(i).Item("pod_width")

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
            '_pod_height = ds_edit.Tables(0).Rows(i).Item("pod_height")
            '_pod_width = ds_edit.Tables(0).Rows(i).Item("pod_width")

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
                                            & "  po_bk_id, " _
                                            & "  po_dt,po_film " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_po_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & po_en_id.EditValue & ",  " _
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
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",'Y'" & "  " _
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


                            'If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                            '    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                            '    _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                            'Else
                            '    _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
                            'End If

                            '_pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
                            '_pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")

                            '_po_total_temp = ((_pod_qty * _pod_cost) - (_pod_qty * _pod_cost * _pod_disc))
                            '_po_total_ppn = _po_total_ppn + (_po_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))
                            '_po_total_pph = _po_total_pph + (_po_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("pod_tax_class")))


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
                                                & "  pod_height, " _
                                                & "  pod_width, " _
                                                & "  pod_qty, " _
                                                & "  pod_um, " _
                                                & "  pod_cost,pod_cost_film, " _
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
                                                & "  pod_loc_id,pod_ppn,pod_pph, " _
                                                & "  pod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString) & ",  " _
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
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_height")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_width")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost_film")) & ",  " _
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
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            k = 0
                            For j = 0 To ds_comment.Tables(0).Rows.Count - 1
                                If ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString = ds_comment.Tables(0).Rows(j).Item("cmt_ref_oid").ToString Then

                                    If IsDBNull(ds_comment.Tables(0).Rows(j).Item("cmt_comment")) = False Then
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
                                                            & SetSetring(ds_comment.Tables(0).Rows(j).Item("cmt_oid").ToString) & ",  " _
                                                            & SetInteger(ds_comment.Tables(0).Rows(j).Item("cmt_dom_id")) & ",  " _
                                                            & "'Purchase Order',  " _
                                                            & SetSetring(func_coll.get_transaction_number("CM", po_en_id.GetColumnValue("en_code"), "cmt_mstr", "cmt_code")) & ",  " _
                                                            & k & ",  " _
                                                            & SetSetringDB(ds_comment.Tables(0).Rows(j).Item("cmt_type")) & ",  " _
                                                            & SetSetring(ds_comment.Tables(0).Rows(j).Item("cmt_ref_oid")) & ",  " _
                                                            & SetSetring(_po_code) & ",  " _
                                                            & SetSetringDB(ds_comment.Tables(0).Rows(j).Item("cmt_comment")) & ",  " _
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
                                                        & SetSetring(po_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_po_oid.ToString) & ",  " _
                                                        & SetSetring(_po_code) & ",  " _
                                                        & SetSetring("Purchase Order") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
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
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                MessageBox.Show("Disable Authorization Edit PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        End If


        If MyBase.edit_data = True Then

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
                po_tran_id.EditValue = .Item("po_tran_id")
                po_credit_term.EditValue = .Item("po_credit_term")
                po_taxable.EditValue = SetBitYNB(.Item("po_taxable"))
                po_tax_inc.EditValue = SetBitYNB(.Item("po_tax_inc"))
                po_tax_class.EditValue = .Item("po_tax_class")
                po_cu_id.EditValue = .Item("po_cu_id")
                po_exc_rate.EditValue = .Item("po_exc_rate")
                po_status_cash.EditValue = .Item("po_status_cash")
                po_bk_id.EditValue = .Item("po_bk_id")

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
                            & "  public.pod_det.pod_height, " _
                            & "  public.pod_det.pod_width, " _
                            & "  public.pod_det.pod_qty, " _
                            & "  public.pod_det.pod_qty_receive, " _
                            & "  public.pod_det.pod_qty_invoice, " _
                            & "  public.pod_det.pod_um, " _
                            & "  public.code_mstr.code_name, " _
                            & "  public.pod_det.pod_cost,pod_cost_film, " _
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
                            & "  public.pod_det.pod_pjc_id, " _
                            & "  pjc_desc, ((pod_qty * pod_cost) - (pod_qty * pod_cost * pod_disc)) as pod_qty_cost, " _
                            & "  pod_taxable, pod_tax_inc, pod_tax_class, " _
                            & "  taxclass_mstr.code_name as pod_tax_class_name, " _
                            & "  coalesce((select count(*) as jml from rcvd_det where rcvd_pod_oid=pod_oid),0) as status_receive " _
                            & "  " _
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
        Dim _po_total, _pod_qty, _pod_cost, _pod_disc, _po_total_ppn, _po_total_pph, _po_total_temp, _tax_rate, _pod_height, _pod_width As Double
        Dim _pod_qty_receive As Double = 0
        Dim i, j, k As Integer
        Dim ssqls As New ArrayList
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
            If ds_edit.Tables(0).Rows(i).Item("pod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("pod_tax_class"))
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
            Else
                _pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost")
            End If

            _pod_qty = ds_edit.Tables(0).Rows(i).Item("pod_qty")
            _pod_disc = ds_edit.Tables(0).Rows(i).Item("pod_disc")
            '_pod_height = ds_edit.Tables(0).Rows(i).Item("pod_height")
            '_pod_width = ds_edit.Tables(0).Rows(i).Item("pod_width")

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
            '_pod_height = ds_edit.Tables(0).Rows(i).Item("pod_height")
            '_pod_width = ds_edit.Tables(0).Rows(i).Item("pod_width")

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
                                            & "  po_sb_id = " & SetInteger(po_sb_id.EditValue) & ",  " _
                                            & "  po_cc_id = " & SetInteger(po_cc_id.EditValue) & ",  " _
                                            & "  po_si_id = " & SetInteger(po_si_id.EditValue) & ",  " _
                                            & "  po_pjc_id = " & SetInteger(po_pjc_id.EditValue) & ",  " _
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
                                            & "  po_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  po_oid = " & SetSetring(_po_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'by sys 20110418 perbaikan update ke requisition
                        'kurangi dulu qty processed pr nya
                        For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + ds_update_related.Tables(0).Rows(i).Item("pod_qty").ToString _
                                                     & " where reqd_oid = '" + ds_update_related.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        'tambahkan lagi qty processed pr nya
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed + " + ds_edit.Tables(0).Rows(i).Item("pod_qty").ToString _
                                                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("pod_reqd_oid").ToString + "'"
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

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table receive
                        'kalau sudah relasi ke table receive jadi nya error...dan harusnya tidak didelete

                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from pod_det where pod_po_oid = '" + _po_oid_mstr + "'"
                        '.Command.CommandText = "delete from pod_det where coalesce(pod_qty_receive,0) = 0 and pod_po_oid = '" + _po_oid_mstr + "'"
                        .Command.CommandText = "delete from pod_det where coalesce((select count(*) as jml from rcvd_det where rcvd_pod_oid=pod_oid),0) = 0 and pod_po_oid = '" + _po_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cmt_mstr where cmt_ref_code = '" + ds.Tables(0).Rows(row).Item("po_code").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

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
                            _pod_oid = Guid.NewGuid.ToString

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
                                                    & "  pod_height, " _
                                                    & "  pod_width, " _
                                                    & "  pod_qty, " _
                                                    & "  pod_um, " _
                                                    & "  pod_cost,pod_cost_film, " _
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
                                                    & "  pod_loc_id,pod_ppn,pod_pph, " _
                                                    & "  pod_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(_pod_oid) & ",  " _
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
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_height")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_width")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost_film")) & ",  " _
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
                                                    & SetDbl(_po_ppn) & "," _
                                                    & SetDbl(_po_pph) & "," _
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
                                                    & "  pod_pt_desc2 = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_pt_desc2")) & ",  " _
                                                    & "  pod_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_si_id")) & ",  " _
                                                    & "  pod_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_rmks")) & ",  " _
                                                    & "  pod_end_user = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pod_end_user")) & ",  " _
                                                    & "  pod_height = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_height")) & ",  " _
                                                    & "  pod_width = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_width")) & ",  " _
                                                    & "  pod_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_qty")) & ",  " _
                                                    & "  pod_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("pod_um")) & ",  " _
                                                    & "  pod_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost")) & ",  " _
                                                     & "  pod_cost_film = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pod_cost_film")) & ",  " _
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
                        Next

                        k = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            For j = 0 To ds_edit.Tables("detail_comment").Rows.Count - 1
                                If ds_edit.Tables(0).Rows(i).Item("pod_oid").ToString = ds_edit.Tables("detail_comment").Rows(j).Item("cmt_ref_oid").ToString Then
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
                                                            & SetSetring(_pod_oid) & ",  " _
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
                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("pod_reqd_oid")) = False Then
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
        Dim _pod_qty, _pod_qty_real, _pod_um_conv, _pod_qty_cost, _pod_cost, _pod_cost_film, _pod_disc, _pod_qty_receive, _pod_height, _pod_width As Double
        _pod_um_conv = 1
        _pod_qty = 1
        _pod_cost = 0
        _pod_disc = 0
        _pod_height = 0
        _pod_width = 0
        _pod_cost_film = 0

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
                _pod_height = (gv_edit.GetRowCellValue(e.RowHandle, "pod_height"))
            Catch ex As Exception
            End Try

            Try
                _pod_width = (gv_edit.GetRowCellValue(e.RowHandle, "pod_width"))
            Catch ex As Exception
            End Try

            Try
                _pod_cost_film = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost_film"))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = (gv_edit.GetRowCellValue(e.RowHandle, "pod_disc"))
            Catch ex As Exception
            End Try

            _pod_qty_real = e.Value * _pod_um_conv
            _pod_qty_cost = (e.Value * _pod_cost_film * _pod_height * _pod_width) - (e.Value * _pod_cost_film * _pod_disc * _pod_height * _pod_width)

            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_real", _pod_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_cost_film" Then
            Try
                _pod_height = (gv_edit.GetRowCellValue(e.RowHandle, "pod_height"))
            Catch ex As Exception
            End Try

            Try
                _pod_width = (gv_edit.GetRowCellValue(e.RowHandle, "pod_width"))
            Catch ex As Exception
            End Try

            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_disc")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (e.Value * _pod_qty * _pod_height * _pod_width) - (e.Value * _pod_qty * _pod_height * _pod_width * _pod_disc)
            _pod_cost = e.Value * _pod_height * _pod_width

            gv_edit.SetRowCellValue(e.RowHandle, "pod_cost", _pod_cost)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_height" Then
            Try
                _pod_cost_film = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost_film"))
            Catch ex As Exception
            End Try

            Try
                _pod_width = (gv_edit.GetRowCellValue(e.RowHandle, "pod_width"))
            Catch ex As Exception
            End Try

            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_disc")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (e.Value * _pod_qty * _pod_cost_film * _pod_width) - (e.Value * _pod_qty * _pod_cost_film * _pod_width * _pod_disc)

            _pod_cost = e.Value * _pod_cost_film * _pod_width

            gv_edit.SetRowCellValue(e.RowHandle, "pod_cost", _pod_cost)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_width" Then
            Try
                _pod_cost_film = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost_film"))
            Catch ex As Exception
            End Try

            Try
                _pod_height = (gv_edit.GetRowCellValue(e.RowHandle, "pod_height"))
            Catch ex As Exception
            End Try

            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_disc")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (e.Value * _pod_qty * _pod_cost_film * _pod_height) - (e.Value * _pod_qty * _pod_cost_film * _pod_height * _pod_disc)

            _pod_cost = e.Value * _pod_cost_film * _pod_height
            gv_edit.SetRowCellValue(e.RowHandle, "pod_cost", _pod_cost)
            gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        ElseIf e.Column.Name = "pod_disc" Then
            Try
                _pod_height = (gv_edit.GetRowCellValue(e.RowHandle, "pod_height"))
            Catch ex As Exception
            End Try

            Try
                _pod_width = (gv_edit.GetRowCellValue(e.RowHandle, "pod_width"))
            Catch ex As Exception
            End Try
            Try
                _pod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_qty")))
            Catch ex As Exception
            End Try

            Try
                _pod_cost_film = ((gv_edit.GetRowCellValue(e.RowHandle, "pod_cost_film")))
            Catch ex As Exception
            End Try

            _pod_qty_cost = (_pod_cost_film * _pod_qty * _pod_height * _pod_width) - (_pod_cost_film * _pod_qty * _pod_height * _pod_width * e.Value)
            _pod_cost = _pod_width * _pod_cost_film * _pod_height
            gv_edit.SetRowCellValue(e.RowHandle, "pod_cost", _pod_cost)
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
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If


        Dim _oid As String
        Try
            _oid = ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("pod_oid").ToString
        Catch ex As Exception
        End Try

        Try
            gv_comment.Columns("cmt_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cmt_ref_oid='" & _oid & "'")
            gv_comment.BestFitColumns()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(0, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "pod_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "pod_en_id", po_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", po_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "pod_si_id", po_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", po_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "pod_height", 0)
            .SetRowCellValue(e.RowHandle, "pod_width", 0)
            .SetRowCellValue(e.RowHandle, "pod_qty", 0)
            .SetRowCellValue(e.RowHandle, "pod_cost", 0)
            .SetRowCellValue(e.RowHandle, "pod_cost_film", 0)
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

        '====================================================================

        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_en_id")
        _type = 2
        _table = "po_mstr"
        _initial = "po"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

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
            & "  po_close_date, " _
            & "  po_total, " _
            & "  po_tran_id, " _
            & "  po_trans_id, " _
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
            & " pod_oid," _
            & "  pod_pt_id, " _
            & "  pod_height, " _
            & "  pod_width, " _
            & "  pod_qty, " _
            & "  pod_cost_film as pod_cost, " _
            & "  pod_disc, " _
            & "  pod_rmks,pod_seq, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  pod_pt_desc1 || pod_pt_desc2 as pod_desc, " _
            & "  ((pod_qty * pod_cost_film * pod_height * pod_width) - (pod_qty * pod_cost_film * pod_disc * pod_height * pod_width)) as pod_cost_ext, " _
            & "  cc_desc, " _
            & "  um_master.code_code as um_name, " _
            & "  creditterm_mstr.code_name as creditterms_name,  " _
            & "  cmt_ref_oid, cmt_comment, cmt_type , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  public.po_mstr " _
            & "  inner join ptnr_mstr on ptnr_id = po_ptnr_id " _
            & "  inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "  inner join ptnrac_cntc on addrc_ptnra_oid  = ptnra_oid " _
            & "  inner join cmaddr_mstr on cmaddr_id = po_cmaddr_id " _
            & "  inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = po_tax_class " _
            & "  inner join cu_mstr on cu_id = po_cu_id " _
            & "  inner join cc_mstr on cc_id = po_cc_id " _
            & "  inner join pod_det on pod_po_oid = po_oid " _
            & "  inner join pt_mstr on pt_id = pod_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = pod_um  " _
            & "  inner join code_mstr as creditterm_mstr on po_mstr.po_credit_term = creditterm_mstr.code_id" _
            & "  inner join code_mstr addr_type on addr_type.code_id = ptnra_addr_type  " _
            & "  left outer join cmt_mstr on pod_oid = cmt_ref_oid  " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = po_oid  " _
            & "  where po_code >= '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") + "'" _
            & "  and po_code <= '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code") + "'" _
                       & "  and addr_type.code_name ~~* 'ship to' " _
            & "  order by po_code, pod_seq, cmt_seq "




        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRPurchaseOrderPrintOutFilm"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        frm.ShowDialog()

    End Sub

    'Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
    '    Try
    '        gv_comment.Columns("cmt_ref_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("pod_oid"))
    '        gv_comment.BestFitColumns()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub gv_comment_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_comment.InitNewRow
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

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
        Dim max_row As Integer
        Try
            'Box(data)
            i = data.Replace(vbNewLine, "~")

            a = i.Split("~")
            max_row = a.GetUpperBound(0)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= max_row Then
                    If j <= ds_edit.Tables("detail_comment").Rows.Count - 1 Then
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


    Private Sub RepairPOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RepairPOToolStripMenuItem.Click
        Try
            Dim sSQLs As New ArrayList
            Dim sSql As String
            sSql = "SELECT  " _
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
            & "  public.pod_det.pod_memo, " _
            & "  public.pod_det.pod_rmks, " _
            & "  public.pod_det.pod_end_user, " _
            & "  public.pod_det.pod_height, " _
            & "  public.pod_det.pod_width, " _
            & "  public.pod_det.pod_qty, " _
            & "  coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_receive, " _
            & "  public.pod_det.pod_qty - coalesce(public.pod_det.pod_qty_receive,0) as pod_qty_outstanding, " _
            & "  public.pod_det.pod_qty_invoice, " _
            & "  public.pod_det.pod_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.pod_det.pod_cost, coalesce(pod_cost_film,0) as pod_cost_film," _
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
            & "  loc_desc, " _
            & "  public.pod_det.pod_pjc_id, " _
            & "  pjc_desc, ((pod_qty * pod_cost ) - (pod_qty * pod_cost * pod_disc )) as pod_qty_cost, " _
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
            & "  where po_mstr.po_code = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code"))

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(sSql)
            Dim _stts_receive, _stts_process As Boolean
            _stts_process = False
            _stts_receive = False
            For Each dr As DataRow In dt.Rows
                If dr("pod_qty_receive") > 0 Then
                    _stts_receive = True
                    Exit For
                End If
                If dr("pod_cost_film") > 0 Then
                    _stts_process = True
                    Exit For
                End If
            Next

            If _stts_receive = True Then
                Box("PO allready recieved")
                Exit Sub
            End If

            If _stts_process = True Then
                Box("PO allready processed")
                Exit Sub
            End If

            For Each dr As DataRow In dt.Rows
                sSql = "update pod_det set pod_cost_film=" & SetDec(dr("pod_cost")) & ",pod_cost=" & SetDec(dr("pod_cost") * dr("pod_height") * dr("pod_width")) & " where pod_oid='" & dr("pod_oid").ToString & "'"
                sSQLs.Add(sSql)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If master_new.PGSqlConn.DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If master_new.PGSqlConn.DbRunTran(sSQLs, "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            Box("Success, Please retrieve to load data")

        Catch ex As Exception
            Pesan(Err)
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

        Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu As String

        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)
        user_phone = mf.get_phone(user_wf)

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
                                               " where " + par_initial + "_oid = '" + par_oid.ToString + "'"
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

                        If preview_export(filename) = True Then

                            If user_wf_email <> "" Then
                                If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then
                                    If sent_sms(user_phone, "Mohon di approve PO : " & par_code & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
                                        Box("Gagal mengirim pesan")
                                        'Exit Sub
                                    End If
                                    Box("Email success")
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

    Public Overrides Sub reminder_mail()
        Dim user_wf, user_wf_email, filename, format_email_bantu As String

        _conf_value = func_coll.get_conf_file("wf_purchase_order")

        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title, user_phone As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid").ToString
        _colom = "po_trans_id"
        _table = "po_mstr"
        _criteria = "po_code"
        _initial = "po"
        _type = "po"
        _title = "Purchase Order"
        'approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_wf, _title)

        user_wf = mf.get_user_wf(_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        format_email_bantu = mf.format_email(user_wf, _code, _type)
        user_phone = mf.get_phone(user_wf)

        'filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".xls"
        filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"
        'ExportTo(par_gv, New ExportXlsProvider(filename))

        If preview_export(filename) = True Then

            If user_wf_email <> "" Then

                If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(_title, _code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then
                    If sent_sms(user_phone, "Mohon di approve PO : " & _code & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
                        Box("Gagal mengirim pesan")
                        'Exit Sub
                    End If
                    Box("Email success")

                Else
                    Box("Email Failed")
                End If

                Try
                    IO.File.Delete(filename)
                Catch ex As Exception

                End Try

            Else
                MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

    End Sub
End Class

