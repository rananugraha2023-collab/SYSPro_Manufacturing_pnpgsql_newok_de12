Imports npgsql
Imports master_new.ModFunction

Public Class FVoucher
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ap_oid_mstr As String
    Public ds_edit_po, ds_edit_receive, ds_edit_dist As DataSet
    Dim _now As DateTime
    Public _par_vendor_id As String
    Public _par_po_oid As String

#Region "Seting Awal"
    Private Sub FVoucher_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ap_en_id.Properties.DataSource = dt_bantu
        ap_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ap_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ap_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ap_cu_id.Properties.DataSource = dt_bantu
        ap_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ap_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ap_cu_id.ItemIndex = 0

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
               & "  enacc_ac_id " _
               & "FROM  " _
               & "  public.enacc_mstr  " _
               & "Where enacc_en_id=" & SetInteger(ap_en_id.EditValue) & " and enacc_code='voucher_account')"

        dt_bantu = New DataTable
        If limit_account(ap_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If

        ap_ap_ac_id.Properties.DataSource = dt_bantu
        ap_ap_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ap_ap_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ap_ap_ac_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        ap_ac_prepaid.Properties.DataSource = dt_bantu
        ap_ac_prepaid.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ap_ac_prepaid.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ap_ac_prepaid.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        ' dt_bantu = (func_data.load_supplier(ap_en_id.EditValue))
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", ap_en_id.EditValue))

        ap_ptnr_id.Properties.DataSource = dt_bantu
        ap_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        ap_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        ap_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(ap_en_id.EditValue))
        ap_credit_term.Properties.DataSource = dt_bantu
        ap_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ap_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ap_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(ap_en_id.EditValue))
        ap_ap_sb_id.Properties.DataSource = dt_bantu
        ap_ap_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ap_ap_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ap_ap_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(ap_en_id.EditValue))
        ap_ap_cc_id.Properties.DataSource = dt_bantu
        ap_ap_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ap_ap_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ap_ap_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr(ap_en_id.EditValue))
        ap_tax_class_id.Properties.DataSource = dt_bantu
        ap_tax_class_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ap_tax_class_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ap_tax_class_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ap_type(ap_en_id.EditValue))
        ap_type.Properties.DataSource = dt_bantu
        ap_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ap_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ap_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(ap_en_id.EditValue, ap_cu_id.EditValue))
        ap_bk_id.Properties.DataSource = dt_bantu
        ap_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        ap_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        ap_bk_id.ItemIndex = 0

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
               & "  enacc_ac_id " _
               & "FROM  " _
               & "  public.enacc_mstr  " _
               & "Where enacc_en_id=" & SetInteger(ap_en_id.EditValue) & " and enacc_code='voucher_account')"

        dt_bantu = New DataTable
        If limit_account(ap_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If
        'dt_bantu = (func_data.load_ac_mstr(_filter))
        ap_ap_ac_id.Properties.DataSource = dt_bantu
        ap_ap_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ap_ap_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ap_ap_ac_id.ItemIndex = 0

        ' ap_ap_ac_id.EditValue = ap_ptnr_id.GetColumnValue("ptnr_ac_ap_id")
    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ap_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Sub ap_cu_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ap_cu_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(ap_en_id.EditValue, ap_cu_id.EditValue))
        ap_bk_id.Properties.DataSource = dt_bantu
        ap_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        ap_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        ap_bk_id.ItemIndex = 0

        If ap_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            ap_exc_rate.EditValue = func_data.get_exchange_rate(ap_cu_id.EditValue, ap_date.DateTime)
        Else
            ap_exc_rate.EditValue = 1
        End If
    End Sub

    Private Sub ap_ptnr_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ap_ptnr_id.EditValueChanged
        ap_ap_ac_id.EditValue = ap_ptnr_id.GetColumnValue("ptnr_ac_ap_id")

        Dim _ptnr_prepaid_balance As Double = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(ptnr_prepaid_balance,0) as ptnr_prepaid_balance from ptnr_mstr " + _
                                           " where ptnr_id = " + ap_ptnr_id.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ptnr_prepaid_balance = .DataReader("ptnr_prepaid_balance")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        te_prepayment_balance.EditValue = _ptnr_prepaid_balance
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        'ap_ptnr_id
        add_column_copy(gv_master, "Supplier ID", "ap_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exc. Rate", "ap_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Number", "ap_invoice", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Date", "ap_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Faktur Pajak Number", "ap_fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Date", "ap_fp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Expected Date", "ap_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "ap_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "ap_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "ap_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "ap_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Prepayment IDR", "ap_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Account Code Prepayment", "ac_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name Prepayment", "ac_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks1", "ap_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks2", "ap_remarks2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "DPP", "ap_dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "ap_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "ap_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Exclude PPH", "ap_amount_exc_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Include PPH", "ap_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Payment", "ap_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Outstanding Payment", "ap_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total IDR", "ap_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Payment IDR", "ap_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Outstanding Payment IDR", "ap_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Status", "ap_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Remark", "ap_tax_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Approval Date", "ap_tax_apr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "ap_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ap_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ap_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ap_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_po, "app_ap_oid", False)
        add_column_copy(gv_detail_po, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_receive, "apr_ap_oid", False)
        add_column_copy(gv_detail_receive, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Description1", "pod_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Description2", "pod_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Taxable", "apr_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Tax Include", "apr_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receive, "Qty Open", "apr_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_receive, "Qty Invoice", "apr_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_receive, "PO Cost", "apr_po_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_receive, "Invoice Cost", "apr_invoice_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_receive, "Tot Cost", "tot_inv_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_receive, "Close Line", "apr_close_line", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_dist, "apd_ap_oid", False)
        add_column_copy(gv_detail_dist, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Taxable", "apd_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Tax Include", "apd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        ''kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column_copy(gv_detail_dist, "Remarks", "apd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Amount", "apd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_dist, "Status", "apd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_po, "app_oid", False)
        add_column(gv_edit_po, "app_po_oid", False)
        add_column(gv_edit_po, "PO Number", "po_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_receive, "apr_oid", False)
        add_column(gv_edit_receive, "apr_rcvd_oid", False)
        add_column_edit(gv_edit_receive, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_receive, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "pt_id", False)
        add_column(gv_edit_receive, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "Taxable", "apr_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "Tax Include", "apr_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "apr_tax_class_id", False)
        add_column(gv_edit_receive, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_receive, "Exc. Rate", "rcv_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_receive, "Qty Open", "apr_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_receive, "Qty Invoice", "apr_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_receive, "PO Cost", "apr_po_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_receive, "Invoice Cost", "apr_invoice_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_receive, "Total Cost", "tot_inv_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_edit(gv_edit_receive, "Close Line", "apr_close_line", DevExpress.Utils.HorzAlignment.Default)
        
        add_column(gv_edit_dist, "apd_oid", False)
        add_column(gv_edit_dist, "apd_ac_id", False)
        add_column(gv_edit_dist, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "apd_sb_id", False)
        add_column(gv_edit_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "apd_cc_id", False)
        add_column(gv_edit_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Taxable", "apd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Tax Include", "apd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "apd_tax_class_id", False)
        add_column(gv_edit_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        'kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column(gv_edit_dist, "Remarks", "apd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Amount", "apd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_edit_dist, "Status", "apd_tax_distribution", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_dist, "Recalculate", "apd_recalculate", DevExpress.Utils.HorzAlignment.Default) 'untuk membantu proses recalculate tax u/ yang input ap manual tanpa po
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.ap_mstr.ap_oid, " _
                & "  public.ap_mstr.ap_dom_id, " _
                & "  public.ap_mstr.ap_en_id, " _
                & "  public.ap_mstr.ap_add_by, " _
                & "  public.ap_mstr.ap_add_date, " _
                & "  public.ap_mstr.ap_upd_by, " _
                & "  public.ap_mstr.ap_upd_date, " _
                & "  public.ap_mstr.ap_code, " _
                & "  public.ap_mstr.ap_date, " _
                & "  public.ap_mstr.ap_tax_date, " _
                & "  public.ap_mstr.ap_ptnr_id, " _
                & "  public.ap_mstr.ap_cu_id, " _
                & "  public.ap_mstr.ap_exc_rate, " _
                & "  public.ap_mstr.ap_bk_id, " _
                & "  public.ap_mstr.ap_credit_term, " _
                & "  public.ap_mstr.ap_eff_date, " _
                & "  public.ap_mstr.ap_disc_date, " _
                & "  public.ap_mstr.ap_expt_date, " _
                & "  public.ap_mstr.ap_ap_ac_id, " _
                & "  public.ap_mstr.ap_ap_sb_id, " _
                & "  public.ap_mstr.ap_ap_cc_id, " _
                & "  public.ap_mstr.ap_disc_ac_id, " _
                & "  public.ap_mstr.ap_disc_sb_id, " _
                & "  public.ap_mstr.ap_disc_cc_id, " _
                & "  public.ap_mstr.ap_type, " _
                & "  public.ap_mstr.ap_taxable, " _
                & "  public.ap_mstr.ap_tax_inc, " _
                & "  public.ap_mstr.ap_tax_class_id, " _
                & "  public.ap_mstr.ap_pay_prepaid, " _
                & "  public.ap_mstr.ap_pay_prepaid * ap_exc_rate as ap_pay_prepaid_idr, " _
                & "  public.ap_mstr.ap_ac_prepaid, " _
                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                & "  public.ap_mstr.ap_amount, " _
                & "  public.ap_mstr.ap_pay_amount, " _
                & "  public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount as ap_outstanding, " _
                & "  public.ap_mstr.ap_amount * ap_exc_rate as ap_amount_idr, " _
                & "  public.ap_mstr.ap_pay_amount * ap_exc_rate as ap_pay_amount_idr, " _
                & "  (public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount) * ap_exc_rate as ap_outstanding_idr, " _
                & "  public.ap_mstr.ap_remarks, ap_remarks2, " _
                & "  public.ap_mstr.ap_status, " _
                & "  public.ap_mstr.ap_dt, " _
                & "  public.ap_mstr.ap_invoice, " _
                & "  public.ap_mstr.ap_due_date, " _
                & "  public.ap_mstr.ap_fp_code, " _
                & "  public.ap_mstr.ap_fp_date, " _
                & "  public.ap_mstr.ap_trans_id, " _
                & "  public.ap_mstr.ap_tax_remarks, " _
                & "  public.ap_mstr.ap_tax_apr_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name,ap_ptnr_id, " _
                & "  public.cu_mstr.cu_name, " _
                & "  public.bk_mstr.bk_name, " _
                & "  credit_term_mstr.code_name as credit_terms_name, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  ap_type.code_name as ap_type_name, " _
                & "  taxclass_mstr.code_name as taxclass_name, " _
                & "  ap_dpp, ap_ppn, ap_pph, ap_dpp + ap_ppn as ap_amount_exc_pph " _
                & "FROM " _
                & "  public.ap_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.ap_mstr.ap_bk_id = public.bk_mstr.bk_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ap_mstr.ap_cu_id = public.cu_mstr.cu_id) " _
                & "  INNER JOIN public.code_mstr credit_term_mstr ON (public.ap_mstr.ap_credit_term = credit_term_mstr.code_id) " _
                & "  INNER JOIN public.ac_mstr ON (public.ap_mstr.ap_ap_ac_id = public.ac_mstr.ac_id) " _
                & "  INNER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ap_mstr.ap_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                & "  INNER JOIN public.sb_mstr ON (public.ap_mstr.ap_ap_sb_id = public.sb_mstr.sb_id) " _
                & "  INNER JOIN public.cc_mstr ON (public.ap_mstr.ap_ap_cc_id = public.cc_mstr.cc_id) " _
                & "  INNER JOIN public.code_mstr ap_type ON (public.ap_mstr.ap_type = ap_type.code_id) " _
                & "  INNER JOIN public.code_mstr taxclass_mstr ON (public.ap_mstr.ap_tax_class_id = taxclass_mstr.code_id) "


        If par_vend.Text <> "" Then

            If par_po.Text <> "" Then
                get_sequel += "  INNER JOIN public.app_po ON app_ap_oid = ap_oid " _
                           & "  where  ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & " and ap_en_id in (select user_en_id from tconfuserentity " _
                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                        & " and app_po_oid='" & _par_po_oid & "'"
            Else
                get_sequel += "  where  ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & " and ap_en_id in (select user_en_id from tconfuserentity " _
                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                        & " and ap_ptnr_id=" & _par_vendor_id

            End If
        Else
            get_sequel += "  where  ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
               & " and ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
               & " and ap_en_id in (select user_en_id from tconfuserentity " _
               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If

       

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail_po").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  app_oid, " _
            & "  app_ap_oid, " _
            & "  app_po_oid, " _
            & "  po_code " _
            & "FROM  " _
            & "  public.app_po  " _
            & "  inner join public.ap_mstr on ap_mstr.ap_oid = app_ap_oid" _
            & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid" _
            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_po, "detail_po")

        Try
            ds.Tables("detail_receive").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  apr_oid, " _
            & "  apr_ap_oid, True as ceklist," _
            & "  apr_seq, " _
            & "  apr_rcvd_oid, " _
            & "  rcv_code, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pod_pt_desc1, " _
            & "  pod_pt_desc2, " _
            & "  apr_taxable, " _
            & "  apr_tax_class_id, " _
            & "  code_name as taxclass_name, " _
            & "  apr_tax_inc, " _
            & "  apr_open, " _
            & "  apr_invoice, " _
            & "  apr_po_cost, " _
            & "  apr_gl_cost, " _
            & "  apr_invoice_cost, " _
            & "  apr_invoice_cost * apr_invoice as tot_inv_cost, " _
            & "  apr_close_line, " _
            & "  apr_dt, rcv_exc_rate " _
            & "FROM  " _
            & "  public.apr_rcv " _
            & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
            & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
            & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
            & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
            & "  inner join public.code_mstr on public.apr_rcv.apr_tax_class_id = public.code_mstr.code_id" _
            & "  inner join public.ap_mstr on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid" _
            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_receive, "detail_receive")

        Try
            ds.Tables("detail_dist").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.apd_dist.apd_oid, " _
            & "  public.apd_dist.apd_ap_oid, " _
            & "  public.apd_dist.apd_tax_distribution, " _
            & "  public.apd_dist.apd_taxable, " _
            & "  public.apd_dist.apd_tax_inc, " _
            & "  public.apd_dist.apd_tax_class_id, " _
            & "  public.apd_dist.apd_ac_id, " _
            & "  public.apd_dist.apd_sb_id, " _
            & "  public.apd_dist.apd_cc_id, " _
            & "  public.apd_dist.apd_amount, " _
            & "  public.apd_dist.apd_remarks, " _
            & "  public.apd_dist.apd_dt, " _
            & "  public.ac_mstr.ac_code, " _
            & "  public.ac_mstr.ac_name, " _
            & "  public.sb_mstr.sb_desc, " _
            & "  public.code_mstr.code_name, " _
            & "  public.cc_mstr.cc_desc " _
            & "FROM " _
            & "  public.apd_dist " _
            & "  INNER JOIN public.ap_mstr ON (public.apd_dist.apd_ap_oid = public.ap_mstr.ap_oid) " _
            & "  INNER JOIN public.ac_mstr ON (public.apd_dist.apd_ac_id = public.ac_mstr.ac_id) " _
            & "  left outer join public.sb_mstr ON (public.apd_dist.apd_sb_id = public.sb_mstr.sb_id) " _
            & "  left outer join public.cc_mstr ON (public.apd_dist.apd_cc_id = public.cc_mstr.cc_id) " _
            & "  left outer join public.code_mstr ON (public.apd_dist.apd_tax_class_id = public.code_mstr.code_id)" _
            & "  where ap_mstr.ap_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ap_mstr.ap_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail_dist, "detail_dist")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_po.Columns("app_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("app_ap_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString & "'")
            gv_detail_po.BestFitColumns()

            gv_detail_receive.Columns("apr_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apr_ap_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString & "'")
            gv_detail_receive.BestFitColumns()

            gv_detail_dist.Columns("apd_ap_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("apd_ap_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString & "'")
            gv_detail_dist.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
        
        ap_en_id.Focus()
        ap_en_id.ItemIndex = 0
        ap_ap_ac_id.ItemIndex = 0
        ap_cu_id.ItemIndex = 0
        ap_eff_date.DateTime = _now
        ap_invoice.Text = ""
        ap_date.DateTime = _now
        ap_due_date.DateTime = _now
        ap_expt_date.DateTime = _now
        ap_taxable.EditValue = False
        ap_tax_inc.EditValue = False
        ap_remarks.Text = ""
        ap_remarks2.Text = ""
        ap_exc_rate.EditValue = 1
        ap_fp_code.Text = ""
        ap_fp_date.Text = ""
        ap_en_id.Enabled = True
        ap_ptnr_id.Enabled = True
        ap_ptnr_id.ItemIndex = 0
        ap_cu_id.Enabled = True
        ap_ap_ac_id.Enabled = True
        ap_ap_sb_id.Enabled = True
        ap_ap_cc_id.Enabled = True
        ap_pay_prepaid.EditValue = 0.0
        ap_ac_prepaid.ItemIndex = 0
        gc_edit_po.Enabled = True
        gc_edit_receive.Enabled = True
        gc_edit_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = True
        sb_retrieve_dist.Enabled = True
        'sb_generate_tax.Enabled = False

        ap_exc_rate.Enabled = True
        ap_pay_prepaid.Enabled = True
        ap_ac_prepaid.Enabled = True
        ap_eff_date.Enabled = True

        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = True

        gv_edit_dist.Columns("apd_taxable").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("apd_tax_inc").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("apd_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("apd_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("apd_amount").OptionsColumn.AllowEdit = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_po = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  app_oid, " _
                        & "  app_ap_oid, " _
                        & "  app_po_oid, " _
                        & "  po_code " _
                        & "FROM  " _
                        & "  public.app_po  " _
                        & "  inner join public.ap_mstr on ap_mstr.ap_oid = app_ap_oid" _
                        & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid" _
                        & "  where po_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_po, "list_po")
                    gc_edit_po.DataSource = ds_edit_po.Tables(0)
                    gv_edit_po.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_receive = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  apr_oid, True as ceklist," _
                        & "  apr_ap_oid, " _
                        & "  apr_seq, " _
                        & "  apr_rcvd_oid, " _
                        & "  rcv_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  apr_taxable, " _
                        & "  apr_tax_class_id, " _
                        & "  code_name as taxclass_name, " _
                        & "  apr_tax_inc, " _
                        & "  apr_open, " _
                        & "  apr_invoice, " _
                        & "  apr_po_cost, " _
                        & "  apr_gl_cost, " _
                        & "  apr_invoice_cost, " _
                        & "  apr_invoice_cost * apr_invoice as tot_inv_cost, " _
                        & "  apr_close_line, " _
                        & "  apr_dt, rcv_exc_rate " _
                        & "FROM  " _
                        & "  public.apr_rcv " _
                        & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
                        & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
                        & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
                        & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.code_mstr on public.apr_rcv.apr_tax_class_id = public.code_mstr.code_id" _
                        & "  inner join public.ap_mstr on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid" _
                        & " where apr_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_receive, "receive")
                    gc_edit_receive.DataSource = ds_edit_receive.Tables(0)
                    gv_edit_receive.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_dist = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.apd_dist.apd_oid, " _
                        & "  public.apd_dist.apd_ap_oid, " _
                        & "  public.apd_dist.apd_tax_distribution, " _
                        & "  public.apd_dist.apd_taxable, " _
                        & "  public.apd_dist.apd_tax_inc, " _
                        & "  public.apd_dist.apd_tax_class_id, " _
                        & "  public.apd_dist.apd_ac_id, " _
                        & "  public.apd_dist.apd_sb_id, " _
                        & "  public.apd_dist.apd_cc_id, " _
                        & "  public.apd_dist.apd_amount, " _
                        & "  public.apd_dist.apd_remarks, " _
                        & "  public.apd_dist.apd_dt, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.sb_mstr.sb_desc, " _
                        & "  public.code_mstr.code_name as taxclass_name, " _
                        & "  public.cc_mstr.cc_desc " _
                        & "FROM " _
                        & "  public.apd_dist " _
                        & "  INNER JOIN public.ap_mstr ON (public.apd_dist.apd_ap_oid = public.ap_mstr.ap_oid) " _
                        & "  INNER JOIN public.ac_mstr ON (public.apd_dist.apd_ac_id = public.ac_mstr.ac_id) " _
                        & "  left outer join public.sb_mstr ON (public.apd_dist.apd_sb_id = public.sb_mstr.sb_id) " _
                        & "  left outer join public.cc_mstr ON (public.apd_dist.apd_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.code_mstr ON (public.apd_dist.apd_tax_class_id = public.code_mstr.code_id)" _
                        & " where apd_amount = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_dist, "dist")
                    gc_edit_dist.DataSource = ds_edit_dist.Tables(0)
                    gv_edit_dist.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_po.UpdateCurrentRow()
        gv_edit_receive.UpdateCurrentRow()
        gv_edit_dist.UpdateCurrentRow()

        ds_edit_po.AcceptChanges()
        ds_edit_receive.AcceptChanges()
        ds_edit_dist.AcceptChanges()

        If ap_ap_ac_id.ItemIndex = -1 Then
            MessageBox.Show("Account can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ap_en_id.EditValue, "gcald_ap", ap_eff_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + ap_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + ap_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        '*********************
        'Cek close line di tab receive
        Dim i As Integer
        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            With ds_edit_receive.Tables(0).Rows(i)
                If (.Item("apr_open") = .Item("apr_invoice")) And (.Item("apr_po_cost") = .Item("apr_invoice_cost")) Then
                    .Item("apr_close_line") = "Y"
                End If
            End With
        Next
        '*********************

        If ds_edit_dist.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        If te_prepayment_balance.EditValue > 0 And ap_pay_prepaid.EditValue = 0 Then
            If MessageBox.Show("Do Not Use Prepaid Balance, Continue Saving Data ?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
                Return False
            End If
        End If
        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ap_oid_mstr = .Item("ap_oid").ToString
                ap_en_id.EditValue = .Item("ap_en_id")
                ap_ptnr_id.EditValue = .Item("ap_ptnr_id")
                ap_eff_date.DateTime = .Item("ap_eff_date")
                ap_cu_id.EditValue = .Item("ap_cu_id")
                ap_exc_rate.EditValue = .Item("ap_exc_rate")
                ap_bk_id.EditValue = .Item("ap_bk_id")
                ap_invoice.Text = SetString(.Item("ap_invoice"))

                If IsDBNull(.Item("ap_date")) = True Then
                    ap_date.Text = ""
                Else
                    ap_date.DateTime = .Item("ap_date")
                End If

                ap_fp_code.Text = SetString(.Item("ap_fp_code"))

                If IsDBNull(.Item("ap_fp_date")) = True Then
                    ap_fp_date.Text = ""
                Else
                    ap_fp_date.DateTime = .Item("ap_fp_date")
                End If

                ap_credit_term.EditValue = .Item("ap_credit_term")
                ap_due_date.DateTime = .Item("ap_due_date")
                ap_expt_date.DateTime = .Item("ap_expt_date")
                ap_ap_ac_id.EditValue = .Item("ap_ap_ac_id")
                ap_ap_sb_id.EditValue = .Item("ap_ap_sb_id")
                ap_ap_cc_id.EditValue = .Item("ap_ap_cc_id")
                ap_taxable.EditValue = SetBitYNB(.Item("ap_taxable"))
                ap_tax_inc.EditValue = SetBitYNB(.Item("ap_tax_inc"))
                ap_tax_class_id.EditValue = .Item("ap_tax_class_id")
                ap_type.EditValue = .Item("ap_type")
                ap_remarks.Text = SetString(.Item("ap_remarks"))
                ap_remarks2.Text = SetString(.Item("ap_remarks2"))
                ap_pay_prepaid.EditValue = .Item("ap_pay_prepaid")
                ap_ac_prepaid.EditValue = .Item("ap_ac_prepaid")
            End With
            ap_en_id.Focus()
            ap_en_id.Enabled = False
            ap_ptnr_id.Enabled = False
            ap_cu_id.Enabled = False
            ap_ap_ac_id.Enabled = False
            ap_exc_rate.Enabled = False
            ap_ap_sb_id.Enabled = False
            ap_ap_cc_id.Enabled = False
            ap_pay_prepaid.Enabled = False
            ap_ac_prepaid.Enabled = False
            gc_edit_po.Enabled = False
            gc_edit_receive.Enabled = False
            gc_edit_dist.Enabled = False
            ap_eff_date.Enabled = False
            sb_retrieve_receive_item.Enabled = False
            sb_retrieve_dist.Enabled = False
            'sb_generate_tax.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_po = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  app_oid, " _
                            & "  app_ap_oid, " _
                            & "  app_po_oid, " _
                            & "  po_code " _
                            & "FROM  " _
                            & "  public.app_po  " _
                            & "  inner join public.ap_mstr on ap_mstr.ap_oid = app_ap_oid" _
                            & "  inner join public.po_mstr on po_mstr.po_oid = app_po_oid" _
                            & "  where app_ap_oid = '" + _ap_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_po, "list_po")
                        gc_edit_po.DataSource = ds_edit_po.Tables(0)
                        gv_edit_po.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_receive = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  apr_oid, " _
                            & "  apr_ap_oid, " _
                            & "  apr_seq, " _
                            & "  apr_rcvd_oid, " _
                            & "  rcv_code, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  apr_taxable, " _
                            & "  apr_tax_class_id, " _
                            & "  code_name as taxclass_name, " _
                            & "  apr_tax_inc, " _
                            & "  apr_open, " _
                            & "  apr_invoice, " _
                            & "  apr_po_cost, " _
                            & "  apr_gl_cost, " _
                            & "  apr_invoice_cost, " _
                            & "  apr_close_line, " _
                            & "  apr_dt, rcv_exc_rate " _
                            & "FROM  " _
                            & "  public.apr_rcv " _
                            & "  inner join public.rcvd_det on public.apr_rcv.apr_rcvd_oid = public.rcvd_det.rcvd_oid " _
                            & "  inner join public.rcv_mstr on public.rcvd_det.rcvd_rcv_oid = public.rcv_mstr.rcv_oid " _
                            & "  inner join public.pod_det on public.rcvd_det.rcvd_pod_oid = public.pod_det.pod_oid " _
                            & "  inner join public.pt_mstr on public.pod_det.pod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.code_mstr on public.apr_rcv.apr_tax_class_id = public.code_mstr.code_id" _
                            & "  inner join public.ap_mstr on public.apr_rcv.apr_ap_oid = public.ap_mstr.ap_oid" _
                            & " where apr_ap_oid = '" + _ap_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_receive, "receive")
                        gc_edit_receive.DataSource = ds_edit_receive.Tables(0)
                        gv_edit_receive.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_dist = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  public.apd_dist.apd_oid, " _
                                & "  public.apd_dist.apd_ap_oid, " _
                                & "  public.apd_dist.apd_tax_distribution, " _
                                & "  public.apd_dist.apd_taxable, " _
                                & "  public.apd_dist.apd_tax_class_id, " _
                                & "  public.apd_dist.apd_ac_id, " _
                                & "  public.apd_dist.apd_sb_id, " _
                                & "  public.apd_dist.apd_cc_id, " _
                                & "  public.apd_dist.apd_amount, " _
                                & "  public.apd_dist.apd_remarks, " _
                                & "  public.apd_dist.apd_dt, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.code_mstr.code_name, " _
                                & "  public.cc_mstr.cc_desc " _
                                & "FROM " _
                                & "  public.apd_dist " _
                                & "  INNER JOIN public.ap_mstr ON (public.apd_dist.apd_ap_oid = public.ap_mstr.ap_oid) " _
                                & "  INNER JOIN public.ac_mstr ON (public.apd_dist.apd_ac_id = public.ac_mstr.ac_id) " _
                                & "  left outer join public.sb_mstr ON (public.apd_dist.apd_sb_id = public.sb_mstr.sb_id) " _
                                & "  left outer join public.cc_mstr ON (public.apd_dist.apd_cc_id = public.cc_mstr.cc_id) " _
                                & "  left outer join public.code_mstr ON (public.apd_dist.apd_tax_class_id = public.code_mstr.code_id)" _
                                & " where apd_ap_oid = '" + _ap_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_dist, "dist")
                        gc_edit_dist.DataSource = ds_edit_dist.Tables(0)
                        gv_edit_dist.BestFitColumns()
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
        Dim ssqls As New ArrayList

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
                                            & "  public.ap_mstr   " _
                                            & "SET  " _
                                            & "  ap_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  ap_en_id = " & SetInteger(ap_en_id.EditValue) & ",  " _
                                            & "  ap_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ap_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ap_date = " & SetDate(ap_date.DateTime) & ",  " _
                                            & "  ap_bk_id = " & SetInteger(ap_bk_id.EditValue) & ",  " _
                                            & "  ap_credit_term = " & SetSetring(ap_credit_term.EditValue) & ",  " _
                                            & "  ap_eff_date = " & SetDate(ap_eff_date.DateTime) & ",  " _
                                            & "  ap_expt_date = " & SetDate(ap_expt_date.DateTime) & ",  " _
                                            & "  ap_type = " & SetSetring(ap_type.EditValue) & ",  " _
                                            & "  ap_remarks = " & SetSetring(ap_remarks.Text) & ",  " _
                                            & "  ap_remarks2 = " & SetSetring(ap_remarks2.Text) & ",  " _
                                            & "  ap_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ap_invoice = " & SetSetring(ap_invoice.Text) & ",  " _
                                            & "  ap_due_date = " & SetDate(ap_due_date.DateTime) & ",  " _
                                            & "  ap_fp_code = " & SetSetring(ap_fp_code.Text) & ",  " _
                                            & "  ap_fp_date = " & SetDate(ap_fp_date.DateTime) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ap_oid = " & SetSetring(_ap_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Yang diperbolehkan edit hanya data master saja,
                        'Untuk yang detail terlalu rumit karena sudah generate jurnal
                        'Apabila salah untuk detail, lakukan delete aja, maka akan terjadi jurnal balik
                        'Lalu lakukan insert data lagi....


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Voucher " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_code"))
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
                        set_row(_ap_oid_mstr, "ap_oid")
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

        Dim _ap_eff_date As Date = func_coll.get_tanggal_sistem() ' ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_eff_date")
        Dim _ap_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_ap_en_id, "gcald_ap", _ap_eff_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _ap_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _ap_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
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

        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
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

                            For i = 0 To ds.Tables("detail_receive").Rows.Count - 1
                                If ds.Tables("detail_receive").Rows(i).Item("apr_ap_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString Then
                                    'Update Table Receive Detail untuk kolom rcvd_qty_inv dan rcvd_close_line nya
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update rcvd_det set rcvd_qty_inv = rcvd_qty_inv - " + SetDec(ds.Tables("detail_receive").Rows(i).Item("apr_invoice")) + _
                                                           ", rcvd_close_line = 'N'" + _
                                                           " where rcvd_oid = '" + ds.Tables("detail_receive").Rows(i).Item("apr_rcvd_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Table PO Detail untuk kolom pod_qty_invoice
                                    If ds.Tables("detail_receive").Rows(i).Item("apr_invoice") > 0 Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update pod_det set pod_qty_invoice = pod_qty_invoice - " + SetDec(ds.Tables("detail_receive").Rows(i).Item("apr_invoice")) + _
                                                               " where pod_oid = (select rcvd_pod_oid from rcvd_det where rcvd_oid = '" + ds.Tables("detail_receive").Rows(i).Item("apr_rcvd_oid").ToString + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                End If
                            Next

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) - " + SetDec(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_pay_prepaid")) + _
                                                   " where ptnr_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_ptnr_id").ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ap_mstr where ap_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If _create_jurnal = True Then
                                Dim _ap_eff_date As Date = func_coll.get_tanggal_sistem() 'karena delete harus menjurnal ditanggal dia delete
                                Dim _ap_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_en_id")
                                Dim _gcald_det_status As String = func_data.get_gcald_det_status(_ap_en_id, "gcald_ap", _ap_eff_date)

                                If _gcald_det_status = "" Then
                                    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _ap_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Return False
                                ElseIf _gcald_det_status.ToUpper = "Y" Then
                                    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _ap_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    sqlTran.Rollback()
                                    Return False
                                End If

                                If func_coll.delete_glt_det_ap(ssqls, objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid").ToString, _
                                                           ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_code")) = False Then
                                    sqlTran.Rollback()
                                    Exit Function
                                End If
                            End If



                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Voucher " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_code"))
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

    Private Sub gv_edit_po_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_po.DoubleClick
        browse_po()
    End Sub

    Private Sub browse_po()
        Dim _col As String = gv_edit_po.FocusedColumn.Name
        Dim _row As Integer = gv_edit_po.FocusedRowHandle

        'Browse PO berdasar kepada entity, patner, currency......

        If _col = "po_code" Then
            Dim frm As New FPurchaseOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ap_en_id.EditValue
            frm._ptnr_id = ap_ptnr_id.EditValue
            frm._cu_id = ap_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub sb_retrieve_receive_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click
        If ds_edit_po.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_po.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim _po_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_po.Tables(0).Rows.Count - 1
            _po_code = _po_code + "'" + ds_edit_po.Tables(0).Rows(i).Item("po_code") + "',"
        Next

        _po_code = _po_code.Substring(0, _po_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  rcvd_oid, True as ceklist, " _
                        & "  rcvd_rcv_oid, " _
                        & "  rcvd_pod_oid, " _
                        & "  rcv_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  pod_pt_desc1, " _
                        & "  pod_pt_desc2, " _
                        & "  pod_cost - (pod_cost * coalesce(pod_disc,0)) as pod_cost, " _
                        & "  coalesce(pod_taxable,'N') as pod_taxable, " _
                        & "  pod_tax_inc, " _
                        & "  pod_tax_class, " _
                        & "  code_name as pod_tax_class_name, " _
                        & "  rcvd_qty - coalesce(rcvd_qty_inv,0) as qty_open, rcv_exc_rate " _
                        & "FROM  " _
                        & "  public.rcvd_det " _
                        & "  inner join rcv_mstr on rcv_oid = rcvd_rcv_oid " _
                        & "  inner join pod_det on pod_oid = rcvd_pod_oid " _
                        & "  inner join po_mstr on po_oid = pod_po_oid " _
                        & "  inner join pt_mstr on pt_id = pod_pt_id " _
                        & "  inner join code_mstr on code_id = pod_tax_class " _
                        & "  where coalesce(rcvd_close_line,'N') = 'N' " _
                        & "  and po_code in (" + _po_code + ")"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "rcv_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_receive.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_receive.Tables(0).NewRow
            _dtrow("apr_oid") = Guid.NewGuid.ToString
            _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
            _dtrow("apr_rcvd_oid") = ds_bantu.Tables(0).Rows(i).Item("rcvd_oid").ToString
            _dtrow("rcv_code") = ds_bantu.Tables(0).Rows(i).Item("rcv_code")
            _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
            _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc1")
            _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pod_pt_desc2")
            _dtrow("apr_taxable") = ds_bantu.Tables(0).Rows(i).Item("pod_taxable")
            _dtrow("apr_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("pod_tax_class")
            _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("pod_tax_class_name")
            _dtrow("apr_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("pod_tax_inc")
            _dtrow("rcv_exc_rate") = ds_bantu.Tables(0).Rows(i).Item("rcv_exc_rate")
            _dtrow("apr_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
            _dtrow("apr_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
            _dtrow("apr_po_cost") = ds_bantu.Tables(0).Rows(i).Item("pod_cost")
            _dtrow("apr_invoice_cost") = ds_bantu.Tables(0).Rows(i).Item("pod_cost")
            _dtrow("tot_inv_cost") = _dtrow("apr_invoice_cost") * _dtrow("apr_invoice")
            _dtrow("apr_close_line") = "N"
            ds_edit_receive.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit_receive.Tables(0).AcceptChanges()

        gv_edit_receive.BestFitColumns()

    End Sub

    Private Sub retrieve_from_receive()

        If ds_edit_receive.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_receive.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim i, j As Integer

        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = False 'False
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = False 'False
        gv_edit_dist.Columns("taxclass_name").Visible = False
        gv_edit_dist.Columns("apd_tax_inc").Visible = False
        gv_edit_dist.Columns("apd_taxable").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("apd_tax_inc").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("apd_remarks").OptionsColumn.AllowEdit = False


        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
        Dim _dtrow As DataRow
        Dim _invoice_cost, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
        _invoice_cost = 0
        _line_tr_pph = 0
        _line_tr_ppn = 0
        _tax_rate = 0

        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 Then
                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                    'Mencari prodline account untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_prodline_account_ap(ds_edit_receive.Tables(0).Rows(i).Item("pt_id")))
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        'If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
                        '(ds_edit_dist.Tables(0).Rows(j).Item("apd_tax_class_id") = ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")) Then
                        If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya line ppn saja
                            _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _tax_rate = func_coll.get_ppn(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_cost / (1 + _tax_rate))
                            _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * (_invoice_cost - _line_tr_ppn)
                            _invoice_cost = _invoice_cost * ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") / ap_exc_rate.EditValue ' untuk dikurangi dari selisih kurs
                            ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _invoice_cost
                        Else
                            _invoice_cost = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _
                                           (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                            ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _invoice_cost = _invoice_cost * ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") / ap_exc_rate.EditValue ' untuk dikurangi dari selisih kurs
                            ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = _invoice_cost
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("apd_oid") = Guid.NewGuid.ToString

                        _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                        _dtrow("apd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("apd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("apd_taxable") = ds_edit_receive.Tables(0).Rows(i).Item("apr_taxable")
                        _dtrow("apd_tax_class_id") = ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")
                        _dtrow("taxclass_name") = ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name")
                        _dtrow("apd_remarks") = Trim(ap_remarks.Text) 'dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya dicari ppn nya saja
                            _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _tax_rate = func_coll.get_ppn(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_cost / (1 + _tax_rate))
                            _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * (_invoice_cost - _line_tr_ppn)
                            _invoice_cost = _invoice_cost * ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") / ap_exc_rate.EditValue ' untuk menghitung u/ keperluan selisih kurs
                            _dtrow("apd_amount") = _invoice_cost
                        Else
                            _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _invoice_cost = _invoice_cost * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                            _invoice_cost = _invoice_cost * ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") / ap_exc_rate.EditValue ' untuk menghitung u/ keperluan selisih kurs
                            _dtrow("apd_amount") = _invoice_cost
                        End If


                        _dtrow("apd_tax_distribution") = "Y"

                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False
                End If
            End If
        Next

        'Untuk PPN dan PPH
        Dim _ppn, _pph As Double
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 And ds_edit_receive.Tables(0).Rows(i).Item("apr_taxable").ToString.ToUpper = "Y" Then
                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                    'Mencari taxrate account ap untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_ap_account(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")))

                    '1. PPN
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")) Then 'rows(0) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _ppn = _ppn * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                        Else
                            _ppn = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _ppn
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("apd_oid") = Guid.NewGuid.ToString

                        If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                            Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                        End If

                        _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                        _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                        _dtrow("apd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("apd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("apd_taxable") = "N"
                        _dtrow("apd_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("apd_remarks") = Trim(ap_remarks.Text) 'dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _ppn = _ppn * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                        Else
                            _ppn = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                        End If

                        _dtrow("apd_amount") = _ppn
                        _dtrow("apd_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False
                    '1. PPH
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")) Then 'rows(1) karena PPH
                            'Exit Sub
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                            _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - _ppn) * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")  'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _pph
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("apd_oid") = Guid.NewGuid.ToString

                        If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                            Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                        End If

                        _dtrow("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")
                        _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                        _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                        _dtrow("apd_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("apd_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("apd_taxable") = "N"
                        _dtrow("apd_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("apd_remarks") = Trim(ap_remarks.Text) ' dt_bantu.Rows(1).Item("ac_name")
                        If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - _ppn) * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                        End If

                        _dtrow("apd_amount") = _pph
                        _dtrow("apd_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                    _search = False
                End If
            End If
        Next


        'Ini untuk ap rate variance
        _search = False
        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 Then
                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") > ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") Then
                        'Mencari prodline account untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_prodline_account_ap_rate(ds_edit_receive.Tables(0).Rows(i).Item("pt_id")))
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya line ppn saja
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost")  'harus selisih antara po_cost dan invoice cost
                                _invoice_cost = _invoice_cost * -1 'harus kali -1 agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_cost / (1 + _tax_rate))
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * (_invoice_cost - _line_tr_ppn)
                                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _invoice_cost
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost")  'harus selisih antara po_cost dan invoice cost
                                _invoice_cost = _invoice_cost * -1 'harus kali -1 agar mengurangi

                                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _
                                                                                (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                                                                 _invoice_cost)
                                ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("apd_oid") = Guid.NewGuid.ToString

                            _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("apd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("apd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("apd_taxable") = ds_edit_receive.Tables(0).Rows(i).Item("apr_taxable")
                            _dtrow("apd_tax_class_id") = ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")
                            _dtrow("taxclass_name") = ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name")
                            _dtrow("apd_remarks") = Trim(ap_remarks.Text) 'dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya dicari ppn nya saja
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisihnya karena ini ap_rate variance
                                _invoice_cost = _invoice_cost * -1 'agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_cost / (1 + _tax_rate))
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * (_invoice_cost - _line_tr_ppn)
                                _dtrow("apd_amount") = _invoice_cost
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisihnya karena ini ap_rate variance
                                _invoice_cost = _invoice_cost * -1
                                _dtrow("apd_amount") = ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _invoice_cost
                            End If


                            _dtrow("apd_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH yang ap rate
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 And ds_edit_receive.Tables(0).Rows(i).Item("apr_taxable").ToString.ToUpper = "Y" Then
                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") > ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") Then
                        'Mencari taxrate account ap untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_taxrate_ap_account(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")))

                        '1. PPN
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")) Then 'rows(0) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali satu agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_cost / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali satu agar mengurangi
                                _ppn = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _invoice_cost) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _ppn
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("apd_oid") = Guid.NewGuid.ToString

                            If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                                Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                            End If

                            _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("apd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("apd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("apd_taxable") = "N"
                            _dtrow("apd_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("apd_remarks") = Trim(ap_remarks.Text) 'dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_cost / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 agar mengurangi
                                _ppn = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _invoice_cost) * (dt_bantu.Rows(0).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("apd_amount") = _ppn
                            _dtrow("apd_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False
                        '1. PPH
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")) Then 'rows(1) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                                '_pph = _pph * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")

                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 karena ini mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_cost / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                _pph = (_invoice_cost - _ppn) * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _invoice_cost) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _pph
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("apd_oid") = Guid.NewGuid.ToString

                            If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                                Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                            End If
                            _dtrow("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")
                            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                            _dtrow("apd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("apd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("apd_taxable") = "N"
                            _dtrow("apd_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("apd_remarks") = Trim(ap_remarks.Text) 'dt_bantu.Rows(1).Item("ac_name")
                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc").ToString.ToUpper = "Y" Then
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                '_pph = _pph * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 karena ini mengurangi

                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_cost / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                _pph = (_invoice_cost - _ppn) * ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_cost = ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") - ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost") 'harus selisih karena ini ap_rate
                                _invoice_cost = _invoice_cost * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _invoice_cost) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("apd_amount") = _pph
                            _dtrow("apd_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                        _search = False
                    End If
                End If
            End If
        Next
        '**************************************************

        Dim _unreal_type As String = ""
        Dim _nilai_awal, _nilai_akhir As Double

        'Untuk Mencari Unreal Gain atau Unreal loss apabila ada...
        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 Then
                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") <> ap_exc_rate.EditValue Then

                        If ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") > ap_exc_rate.EditValue Then
                            _unreal_type = "gain"
                        ElseIf ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate") < ap_exc_rate.EditValue Then
                            _unreal_type = "loss"
                        End If

                        dt_bantu = New DataTable
                        dt_bantu = func_data.get_unreal_account(ap_cu_id.EditValue, _unreal_type)

                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            _nilai_awal = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate"))
                            _nilai_akhir = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") * _
                                           ap_exc_rate.EditValue)
                            _invoice_cost = (_nilai_akhir - _nilai_awal) / ap_exc_rate.EditValue

                            ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _invoice_cost
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("apd_oid") = Guid.NewGuid.ToString

                            _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("apd_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("apd_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("apd_taxable") = "N"
                            _dtrow("apd_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("apd_remarks") = Trim(ap_remarks.Text) ' dt_bantu.Rows(0).Item("ac_name")

                            _nilai_awal = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("rcv_exc_rate"))
                            _nilai_akhir = (ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") * _
                                           ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost") * _
                                           ap_exc_rate.EditValue)
                            _invoice_cost = (_nilai_akhir - _nilai_awal) / ap_exc_rate.EditValue
                            _dtrow("apd_amount") = _invoice_cost

                            _dtrow("apd_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next
        '*********************************************************

        'Insert ke ds untuk yang prepayment kalau ada
        'If ap_pay_prepaid.EditValue < 0 Then
        '    _dtrow = ds_edit_dist.Tables(0).NewRow
        '    _dtrow("apd_oid") = Guid.NewGuid.ToString

        '    _dtrow("apd_ac_id") = ap_ac_prepaid.EditValue
        '    _dtrow("ac_code") = ap_ac_prepaid.GetColumnValue("ac_code")
        '    _dtrow("ac_name") = ap_ac_prepaid.GetColumnValue("ac_name")

        '    _dtrow("apd_sb_id") = 0
        '    _dtrow("sb_desc") = "-"
        '    _dtrow("apd_cc_id") = 0
        '    _dtrow("cc_desc") = "-"
        '    _dtrow("apd_taxable") = "N"
        '    _dtrow("apd_tax_class_id") = DBNull.Value
        '    _dtrow("taxclass_name") = DBNull.Value
        '    _dtrow("apd_remarks") = ap_ac_prepaid.GetColumnValue("ac_name")
        '    _dtrow("apd_amount") = ap_pay_prepaid.EditValue
        '    _dtrow("apd_tax_distribution") = "Y"
        '    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
        '    ds_edit_dist.Tables(0).AcceptChanges()
        '    '**********************************************************
        'End If

        If ap_pay_prepaid.EditValue <> 0 Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_ap_account(ap_tax_class_id.EditValue))

            If ap_taxable.EditValue = True Then
                If ap_tax_inc.EditValue = True Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ap_pay_prepaid.EditValue / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ap_pay_prepaid.EditValue - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ap_pay_prepaid.EditValue) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                    _ppn = (ap_pay_prepaid.EditValue) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If
            End If

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("apd_oid") = Guid.NewGuid.ToString

            _dtrow("apd_ac_id") = ap_ac_prepaid.EditValue
            _dtrow("ac_code") = ap_ac_prepaid.GetColumnValue("ac_code")
            _dtrow("ac_name") = ap_ac_prepaid.GetColumnValue("ac_name")

            _dtrow("apd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("apd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("apd_taxable") = IIf(ap_taxable.EditValue = True, "Y", "N")
            _dtrow("apd_tax_inc") = IIf(ap_tax_inc.EditValue = True, "Y", "N")
            _dtrow("apd_tax_class_id") = IIf(ap_taxable.EditValue = True, ap_tax_class_id.EditValue, DBNull.Value)
            _dtrow("taxclass_name") = IIf(ap_taxable.EditValue = True, ap_tax_class_id.GetColumnValue("code_name"), DBNull.Value)
            _dtrow("apd_remarks") = ap_ac_prepaid.GetColumnValue("ac_name")

            If ap_tax_inc.EditValue = True Then
                _dtrow("apd_amount") = ap_pay_prepaid.EditValue - _ppn
            Else
                _dtrow("apd_amount") = ap_pay_prepaid.EditValue
            End If


            _dtrow("apd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()


            '1. PPN
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")) Then
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _ppn
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("apd_oid") = Guid.NewGuid.ToString

                If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                    Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(0).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                End If

                _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")
                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                _dtrow("apd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("apd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("apd_taxable") = "N"
                _dtrow("apd_tax_inc") = "N"
                _dtrow("apd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("apd_remarks") = Trim(ap_remarks.Text) ' dt_bantu.Rows(0).Item("ac_name")

                _dtrow("apd_amount") = _ppn
                _dtrow("apd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If


            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")) Then
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _pph
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("apd_oid") = Guid.NewGuid.ToString

                If dt_bantu.Rows(0).Item("taxr_ac_ap_id") = 0 Then
                    Box("Tax Class " & ds_edit_receive.Tables(0).Rows(i).Item("taxclass_name") & " dengan Tax Type " & dt_bantu.Rows(1).Item("tax_type_name") & ", belum diseting account AP nya (taxr_ac_ap_id) " & vbNewLine & "Silahkan seting di Master Data >> Address and Tax >> Tax Rate")

                End If

                _dtrow("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")
                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                _dtrow("apd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("apd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("apd_taxable") = "N"
                _dtrow("apd_tax_inc") = "N"
                _dtrow("apd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("apd_remarks") = Trim(ap_remarks.Text) ' dt_bantu.Rows(1).Item("ac_name")

                _dtrow("apd_amount") = _pph
                _dtrow("apd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
        End If

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        gv_edit_dist.BestFitColumns()
    End Sub

    Private Sub recalculate()
        Dim _dtrow As DataRow
        Dim i, j As Integer
        Dim ds_copy As New DataSet
        Dim _search As Boolean
        Dim _ppn, _pph, _tax_rate As Double

        ds_edit_dist.AcceptChanges()
        ds_copy = ds_edit_dist.Copy
        _ppn = 0
        _pph = 0

        'ini untuk update line paling awal
        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            If ds_edit_dist.Tables(0).Rows(i).Item("apd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit_dist.Tables(0).Rows(i).Item("apd_tax_class_id"))
                If ds_edit_dist.Tables(0).Rows(i).Item("apd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = _tax_rate * (ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") / (1 + _tax_rate))
                    ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") - _ppn
                Else
                    _ppn = ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") * _tax_rate
                End If

                ds_edit_dist.AcceptChanges()
            End If
        Next

        For i = 0 To ds_copy.Tables(0).Rows.Count - 1
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_ap_account(ds_copy.Tables(0).Rows(i).Item("apd_tax_class_id")))

            '1. PPN
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")) Then 'rows(0) karena PPN
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("apd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("apd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("apd_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _ppn
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("apd_oid") = Guid.NewGuid.ToString

                _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")
                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                _dtrow("apd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("apd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("apd_taxable") = "N"
                _dtrow("apd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("apd_remarks") = dt_bantu.Rows(0).Item("ac_name")

                If ds_copy.Tables(0).Rows(i).Item("apd_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("apd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("apd_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                _dtrow("apd_amount") = _ppn
                _dtrow("apd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If

            '2. PPH
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")) Then 'rows(1) karena PPH
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("apd_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("apd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("apd_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("apd_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'row(1) artinya mengacu ke pph
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") = ds_edit_dist.Tables(0).Rows(j).Item("apd_amount") + _pph
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("apd_oid") = Guid.NewGuid.ToString

                _dtrow("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")
                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                _dtrow("apd_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("apd_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("apd_taxable") = "N"
                _dtrow("apd_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("apd_remarks") = dt_bantu.Rows(1).Item("ac_name")
                If ds_copy.Tables(0).Rows(i).Item("apd_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("apd_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("apd_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("apd_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                End If

                _dtrow("apd_amount") = _pph
                _dtrow("apd_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
        Next

        'Insert ke ds untuk yang prepayment kalau ada
        _ppn = 0

        If ap_pay_prepaid.EditValue <> 0 Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_ap_account(ap_tax_class_id.EditValue))

            If ap_taxable.EditValue = True Then
                If ap_tax_inc.EditValue = True Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ap_pay_prepaid.EditValue / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ap_pay_prepaid.EditValue - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ap_pay_prepaid.EditValue) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                    _ppn = (ap_pay_prepaid.EditValue) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If
            End If

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("apd_oid") = Guid.NewGuid.ToString

            _dtrow("apd_ac_id") = ap_ac_prepaid.EditValue
            _dtrow("ac_code") = ap_ac_prepaid.GetColumnValue("ac_code")
            _dtrow("ac_name") = ap_ac_prepaid.GetColumnValue("ac_name")

            _dtrow("apd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("apd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("apd_taxable") = IIf(ap_taxable.EditValue = True, "Y", "N")
            _dtrow("apd_tax_inc") = IIf(ap_tax_inc.EditValue = True, "Y", "N")
            _dtrow("apd_tax_class_id") = IIf(ap_taxable.EditValue = True, ap_tax_class_id.EditValue, DBNull.Value)
            _dtrow("taxclass_name") = IIf(ap_taxable.EditValue = True, ap_tax_class_id.GetColumnValue("code_name"), DBNull.Value)
            _dtrow("apd_remarks") = ap_ac_prepaid.GetColumnValue("ac_name")
            _dtrow("apd_amount") = ap_pay_prepaid.EditValue - _ppn
            _dtrow("apd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()


            '1. PPN
            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("apd_oid") = Guid.NewGuid.ToString

            _dtrow("apd_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_ap_id")
            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

            _dtrow("apd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("apd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("apd_taxable") = "N"
            _dtrow("apd_tax_inc") = "N"
            _dtrow("apd_tax_class_id") = DBNull.Value
            _dtrow("taxclass_name") = DBNull.Value
            _dtrow("apd_remarks") = dt_bantu.Rows(0).Item("ac_name")

            _dtrow("apd_amount") = _ppn
            _dtrow("apd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()

            '2. PPH

            _dtrow = ds_edit_dist.Tables(0).NewRow
            _dtrow("apd_oid") = Guid.NewGuid.ToString

            _dtrow("apd_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_ap_id")
            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

            _dtrow("apd_sb_id") = 0
            _dtrow("sb_desc") = "-"
            _dtrow("apd_cc_id") = 0
            _dtrow("cc_desc") = "-"
            _dtrow("apd_taxable") = "N"
            _dtrow("apd_tax_inc") = "N"
            _dtrow("apd_tax_class_id") = DBNull.Value
            _dtrow("taxclass_name") = DBNull.Value
            _dtrow("apd_remarks") = dt_bantu.Rows(1).Item("ac_name")

            _dtrow("apd_amount") = _pph
            _dtrow("apd_tax_distribution") = "Y"
            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
            ds_edit_dist.Tables(0).AcceptChanges()
        End If

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("apd_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()
        gv_edit_dist.BestFitColumns()

        'gc_edit_dist.Enabled = False
        'sb_retrieve_dist.Enabled = False
        'setelah di recalculate tidak boleh lagi diubah karena sudah hitung tax dan lain sebagainya...
        'kalau salah di cancel aja terus buat lagi dari awal
    End Sub

    Private Sub sb_retrieve_dist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_dist.Click
        If ds_edit_receive.Tables(0).Rows.Count = 0 Then
            recalculate()
        Else
            retrieve_from_receive()
        End If
    End Sub

    Private Sub gv_edit_dist_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_dist.CellValueChanged
        If e.Column.Name = "apd_taxable" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "apd_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_tax_inc", "N")
                gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_tax_class_id", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit_dist.SetRowCellValue(e.RowHandle, "taxclass_name", "NON-TAX")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_recalculate", False)
            ElseIf gv_edit_dist.GetRowCellValue(e.RowHandle, "apd_taxable").ToString.ToUpper = "Y" Then
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_recalculate", True)
            End If
        ElseIf e.Column.Name = "apd_tax_inc" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "apd_tax_inc").ToString.ToUpper = "Y" And gv_edit_dist.GetRowCellValue(e.RowHandle, "apd_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_tax_inc", "N")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_recalculate", False)
            End If
        ElseIf e.Column.Name = "apd_amount" Then
            'gv_edit_dist.SetRowCellValue(e.RowHandle, "apd_recalculate", True)
        End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_dist.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_dist.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_dist.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_dist.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_dist.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit_dist.FocusedColumn.Name
        Dim _row As Integer = gv_edit_dist.FocusedRowHandle

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = ap_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = ap_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "taxclass_name" Then
            If gv_edit_dist.GetRowCellValue(_row, "apd_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ap_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_dist_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_dist.InitNewRow
        gv_edit_dist.Columns("taxclass_name").Visible = True
        gv_edit_dist.Columns("apd_tax_inc").Visible = True
        'sb_retrieve_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = False

        With gv_edit_dist
            .SetRowCellValue(e.RowHandle, "apd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "apd_taxable", IIf(ap_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "apd_tax_class_id", ap_tax_class_id.EditValue)
            .SetRowCellValue(e.RowHandle, "taxclass_name", ap_tax_class_id.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "apd_tax_inc", IIf(ap_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "apd_amount", 0)
            .SetRowCellValue(e.RowHandle, "apd_tax_distribution", "N")

            .SetRowCellValue(e.RowHandle, "apd_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "apd_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")


            'by sys 20110611
            .SetRowCellValue(e.RowHandle, "apd_remarks", Trim(ap_remarks.Text))

            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_po_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_po.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_po.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_po.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_po_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_po.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_po()
        End If
    End Sub

    Private Sub ap_date_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ap_date.EditValueChanged
        ap_fp_date.EditValue = ap_date.EditValue
    End Sub

    Private Sub ap_due_date_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ap_due_date.EditValueChanged
        ap_expt_date.EditValue = ap_due_date.EditValue
    End Sub

    Private Sub gv_edit_receive_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_receive.CellValueChanged
        Dim _apr_po_cost As Double
        _apr_po_cost = gv_edit_receive.GetRowCellValue(e.RowHandle, "apr_po_cost")

        If e.Column.Name = "apr_invoice" Then
            Dim _apr_invoice_cost As Double

            _apr_invoice_cost = gv_edit_receive.GetRowCellValue(e.RowHandle, "apr_invoice_cost")
            gv_edit_receive.SetRowCellValue(e.RowHandle, "tot_inv_cost", e.Value * _apr_invoice_cost)
        ElseIf e.Column.Name = "apr_invoice_cost" And e.Value <= _apr_po_cost Then
            Dim _apr_invoice As Double

            _apr_invoice = gv_edit_receive.GetRowCellValue(e.RowHandle, "apr_invoice")
            gv_edit_receive.SetRowCellValue(e.RowHandle, "tot_inv_cost", e.Value * _apr_invoice)
        ElseIf e.Column.Name = "apr_invoice_cost" And e.Value > _apr_po_cost Then
            gv_edit_receive.SetRowCellValue(e.RowHandle, "apr_invoice_cost", _apr_po_cost)
        End If
    End Sub

#Region "DML"
    Public Overrides Function insert() As Boolean
        Dim _ap_oid As Guid
        _ap_oid = Guid.NewGuid

        Dim _ap_code As String
        Dim _ap_amount As Double = 0
        Dim _ap_dpp As Double = 0
        Dim _ap_ppn As Double = 0
        Dim _ap_pph As Double = 0
        Dim _prepaid As Double = 0
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        _ap_code = func_coll.get_transaction_number("AP", ap_en_id.GetColumnValue("en_code"), "ap_mstr", "ap_code")

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _ap_amount = _ap_amount + ds_edit_dist.Tables(0).Rows(i).Item("apd_amount")
            If ds_edit_dist.Tables(0).Rows(i).Item("ac_name").ToString.Substring(0, 3).ToUpper = "PPN" Then
                _ap_ppn = _ap_ppn + ds_edit_dist.Tables(0).Rows(i).Item("apd_amount")
            ElseIf ds_edit_dist.Tables(0).Rows(i).Item("ac_name").ToString.Substring(0, 3).ToUpper = "PPH" Then
                _ap_pph = _ap_pph + ds_edit_dist.Tables(0).Rows(i).Item("apd_amount")
            End If
            'MessageBox.Show(ds_edit_dist.Tables(0).Rows(i).Item("ac_name"))
            'Return False
        Next

        _ap_dpp = (_ap_amount + (_ap_pph * -1)) - _ap_ppn

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
                                            & "  public.ap_mstr " _
                                            & "( " _
                                            & "  ap_oid, " _
                                            & "  ap_dom_id, " _
                                            & "  ap_en_id, " _
                                            & "  ap_add_by, " _
                                            & "  ap_add_date, " _
                                            & "  ap_code, " _
                                            & "  ap_date, " _
                                            & "  ap_tax_date, " _
                                            & "  ap_ptnr_id, " _
                                            & "  ap_cu_id, " _
                                            & "  ap_exc_rate, " _
                                            & "  ap_bk_id, " _
                                            & "  ap_credit_term, " _
                                            & "  ap_eff_date, " _
                                            & "  ap_disc_date, " _
                                            & "  ap_expt_date, " _
                                            & "  ap_ap_ac_id, " _
                                            & "  ap_ap_sb_id, " _
                                            & "  ap_ap_cc_id, " _
                                            & "  ap_disc_ac_id, " _
                                            & "  ap_disc_sb_id, " _
                                            & "  ap_disc_cc_id, " _
                                            & "  ap_type, " _
                                            & "  ap_taxable, " _
                                            & "  ap_tax_inc, " _
                                            & "  ap_tax_class_id, " _
                                            & "  ap_ac_prepaid, " _
                                            & "  ap_pay_prepaid, " _
                                            & "  ap_amount, " _
                                            & "  ap_pay_amount, " _
                                            & "  ap_remarks, " _
                                            & "  ap_remarks2, " _
                                            & "  ap_status, " _
                                            & "  ap_dt, " _
                                            & "  ap_invoice, " _
                                            & "  ap_due_date, " _
                                            & "  ap_fp_code, " _
                                            & "  ap_fp_date, " _
                                            & "  ap_dpp, " _
                                            & "  ap_ppn, " _
                                            & "  ap_pph " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ap_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ap_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_ap_code) & ",  " _
                                            & SetDate(ap_date.DateTime) & ",  " _
                                            & "null" & ",  " _
                                            & SetInteger(ap_ptnr_id.EditValue) & ",  " _
                                            & SetInteger(ap_cu_id.EditValue) & ",  " _
                                            & SetDbl(ap_exc_rate.EditValue) & ",  " _
                                            & SetInteger(ap_bk_id.EditValue) & ",  " _
                                            & SetInteger(ap_credit_term.EditValue) & ",  " _
                                            & SetDate(ap_eff_date.DateTime) & ",  " _
                                            & "null" & ",  " _
                                            & SetDate(ap_expt_date.DateTime) & ",  " _
                                            & SetInteger(ap_ap_ac_id.EditValue) & ",  " _
                                            & SetInteger(ap_ap_sb_id.EditValue) & ",  " _
                                            & SetInteger(ap_ap_cc_id.EditValue) & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & SetInteger(ap_type.EditValue) & ",  " _
                                            & SetBitYN(ap_taxable.EditValue) & ",  " _
                                            & SetBitYN(ap_tax_inc.EditValue) & ",  " _
                                            & SetInteger(ap_tax_class_id.EditValue) & ",  " _
                                            & SetInteger(ap_ac_prepaid.EditValue) & ",  " _
                                            & SetDbl(ap_pay_prepaid.EditValue) & ",  " _
                                            & SetDbl(_ap_amount) & ",  " _
                                            & "0" & ",  " _
                                            & SetSetring(ap_remarks.Text) & ",  " _
                                            & SetSetring(ap_remarks2.Text) & ",  " _
                                            & "null" & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(ap_invoice.Text) & ",  " _
                                            & SetDate(ap_due_date.DateTime) & ",  " _
                                            & SetSetring(ap_fp_code.Text) & ",  " _
                                            & SetDate(ap_fp_date.DateTime) & ",  " _
                                            & SetDbl(_ap_dpp) & ",  " _
                                            & SetDbl(_ap_ppn) & ",  " _
                                            & SetDbl(_ap_pph) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Insert Data List PO
                        For i = 0 To ds_edit_po.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.app_po " _
                                                & "( " _
                                                & "  app_oid, " _
                                                & "  app_ap_oid, " _
                                                & "  app_po_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_po.Tables(0).Rows(i).Item("app_oid").ToString) & ",  " _
                                                & SetSetring(_ap_oid.ToString) & ",  " _
                                                & SetSetring(ds_edit_po.Tables(0).Rows(i).Item("app_po_oid").ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        'Untuk Insert Data List Receive
                        For i = 0 To ds_edit_receive.Tables(0).Rows.Count - 1
                            If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 Then
                                If ds_edit_receive.Tables(0).Rows(i).Item("ceklist") = True Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.apr_rcv " _
                                                        & "( " _
                                                        & "  apr_oid, " _
                                                        & "  apr_ap_oid, " _
                                                        & "  apr_seq, " _
                                                        & "  apr_rcvd_oid, " _
                                                        & "  apr_taxable, " _
                                                        & "  apr_tax_class_id, " _
                                                        & "  apr_tax_inc, " _
                                                        & "  apr_open, " _
                                                        & "  apr_invoice, " _
                                                        & "  apr_po_cost, " _
                                                        & "  apr_invoice_cost, " _
                                                        & "  apr_close_line, " _
                                                        & "  apr_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(ds_edit_receive.Tables(0).Rows(i).Item("apr_oid").ToString) & ",  " _
                                                        & SetSetring(_ap_oid.ToString) & ",  " _
                                                        & SetInteger(i) & ",  " _
                                                        & SetSetring(ds_edit_receive.Tables(0).Rows(i).Item("apr_rcvd_oid").ToString) & ",  " _
                                                        & SetSetring(ds_edit_receive.Tables(0).Rows(i).Item("apr_taxable")) & ",  " _
                                                        & SetInteger(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_class_id")) & ",  " _
                                                        & SetSetring(ds_edit_receive.Tables(0).Rows(i).Item("apr_tax_inc")) & ",  " _
                                                        & SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_open")) & ",  " _
                                                        & SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice")) & ",  " _
                                                        & SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_po_cost")) & ",  " _
                                                        & SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice_cost")) & ",  " _
                                                        & SetSetring(ds_edit_receive.Tables(0).Rows(i).Item("apr_close_line")) & ",  " _
                                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                        & ")"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Table Receive Detail untuk kolom rcvd_qty_inv dan rcvd_close_line nya
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update rcvd_det set rcvd_qty_inv = coalesce(rcvd_qty_inv,0) + " + SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice").ToString) + _
                                                           ", rcvd_close_line = '" + ds_edit_receive.Tables(0).Rows(i).Item("apr_close_line") + "'" + _
                                                           " where rcvd_oid = '" + ds_edit_receive.Tables(0).Rows(i).Item("apr_rcvd_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    If ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice") <> 0 Then
                                        'Update Table PO Detail untuk kolom pod_qty_invoice
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update pod_det set pod_qty_invoice = coalesce(pod_qty_invoice,0) + " + SetDbl(ds_edit_receive.Tables(0).Rows(i).Item("apr_invoice").ToString) + _
                                                               " where pod_oid = (select rcvd_pod_oid from rcvd_det where rcvd_oid = '" + ds_edit_receive.Tables(0).Rows(i).Item("apr_rcvd_oid").ToString + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                End If
                            End If
                        Next

                        'Untuk Insert Distribution Line nya
                        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.apd_dist " _
                                                & "( " _
                                                & "  apd_oid, " _
                                                & "  apd_ap_oid, " _
                                                & "  apd_tax_distribution, " _
                                                & "  apd_taxable, " _
                                                & "  apd_tax_class_id, " _
                                                & "  apd_ac_id, " _
                                                & "  apd_sb_id, " _
                                                & "  apd_cc_id, " _
                                                & "  apd_amount, " _
                                                & "  apd_remarks, " _
                                                & "  apd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("apd_oid").ToString) & ",  " _
                                                & SetSetring(_ap_oid.ToString) & ",  " _
                                                & SetSetringDB(ds_edit_dist.Tables(0).Rows(i).Item("apd_tax_distribution")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("apd_taxable")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("apd_tax_class_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("apd_ac_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("apd_sb_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("apd_cc_id")) & ",  " _
                                                & SetDbl(ds_edit_dist.Tables(0).Rows(i).Item("apd_amount")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("apd_remarks")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        'update prepaid balance
                        If ap_pay_prepaid.EditValue <> 0 Then
                            For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                                If ap_ac_prepaid.EditValue = ds_edit_dist.Tables(0).Rows(i).Item("apd_ac_id") Then
                                    _prepaid = ds_edit_dist.Tables(0).Rows(i).Item("apd_amount")
                                End If
                            Next

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) + " + SetDbl(_prepaid.ToString) + _
                                                   " where ptnr_id = " + ap_ptnr_id.EditValue.ToString

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        'Insert Jurnal
                        If _create_jurnal = True Then
                            If insert_glt_det_ap(ssqls, objinsert, ds_edit_dist, _
                                                       ap_en_id.EditValue, ap_en_id.GetColumnValue("en_code"), _
                                                       _ap_oid.ToString, _ap_code, _
                                                       ap_eff_date.DateTime, _
                                                       ap_cu_id.EditValue, ap_exc_rate.EditValue, _
                                                       "AP", "AP-VOC", _
                                                       ap_ap_ac_id.EditValue, ap_ap_sb_id.EditValue, ap_ap_cc_id.EditValue, _
                                                       ap_ap_ac_id.GetColumnValue("ac_name"), _
                                                       _ap_amount) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If

                        '************************************************
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Voucher " & _ap_code)
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
                        set_row(_ap_oid.ToString, "ap_oid")
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

    Private Function insert_glt_det_ap(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        insert_glt_det_ap = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            If par_ds.Tables(0).Rows(i).Item("apd_amount") > 0 Then
                With par_obj
                    Try
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
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("apd_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("apd_remarks")) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("apd_amount")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("apd_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("apd_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("apd_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("apd_amount"), "D") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf par_ds.Tables(0).Rows(i).Item("apd_amount") < 0 Then
                With par_obj
                    Try
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
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("apd_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("apd_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("apd_amount") * -1) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("apd_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("apd_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("apd_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("apd_amount") * -1, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        _seq = _seq + 1

        With par_obj
            Try
                'Insert untuk yang credit yang account hutang nya
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
                                    & SetInteger(par_en_id) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & SetDate(par_date) & ",  " _
                                    & SetSetring(par_type) & ",  " _
                                    & SetInteger(par_cu_id) & ",  " _
                                    & SetDbl(par_exc_rate) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(par_ac_id) & ",  " _
                                    & SetIntegerDB(par_sb_id) & ",  " _
                                    & SetIntegerDB(par_cc_id) & ",  " _
                                    & SetSetringDB(par_desc) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetDblDB(par_amount) & ",  " _
                                    & SetSetring(par_oid) & ",  " _
                                    & SetSetring(par_trans_code) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(par_daybook) & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                 par_ac_id, _
                                                 par_sb_id, _
                                                 par_cc_id, _
                                                 par_en_id, par_cu_id, _
                                                 par_exc_rate, par_amount, "C") = False Then

                    Return False
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function
#End Region

    Private Sub par_cus_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_vend.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = par_vend
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_so_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_po.ButtonClick
        Try
            
            Dim frm As New FPurchaseOrderSearch
            frm.set_win(Me)
            frm._obj = par_po
            frm._ptnr_id = _par_vendor_id
            frm.type_form = True
            frm.ShowDialog()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
