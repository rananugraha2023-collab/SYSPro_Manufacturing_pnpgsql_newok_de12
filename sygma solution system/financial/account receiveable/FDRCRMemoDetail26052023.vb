Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDRCRMemoDetail26052023
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr, _ar_so_ref_codes As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime
    Dim _total_piutang As Double
    Public _par_cus_id As String

    Private Sub FDRCRMemoDetail26052023_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ar_en_id.Properties.DataSource = dt_bantu
        ar_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ar_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ar_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ar_cu_id.Properties.DataSource = dt_bantu
        ar_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ar_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ar_cu_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ac_mstr())
        'ar_ac_id.Properties.DataSource = dt_bantu
        'ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        'ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        'ar_ac_id.ItemIndex = 0

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(ar_en_id.EditValue) & " and enacc_code='dbcr_account')"

        dt_bantu = New DataTable
        If limit_account(ar_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If
        ' dt_bantu = (func_data.load_ac_mstr(_filter))
        ar_ac_id.Properties.DataSource = dt_bantu
        ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ar_ac_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ac_mstr())
        'ar_ac_prepaid.Properties.DataSource = dt_bantu
        'ar_ac_prepaid.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        'ar_ac_prepaid.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        'ar_ac_prepaid.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_ppn_type()
        ar_ppn_type.Properties.DataSource = dt_bantu
        ar_ppn_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ar_ppn_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ar_ppn_type.ItemIndex = 0

    End Sub



    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_customer(ar_en_id.EditValue))
        'ar_bill_to.Properties.DataSource = dt_bantu
        'ar_bill_to.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        'ar_bill_to.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        'ar_bill_to.ItemIndex = 0

        init_le(ar_bill_to, "cus_mstr_parent", ar_en_id.EditValue)

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(ar_en_id.EditValue))
        ar_credit_term.Properties.DataSource = dt_bantu
        ar_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ar_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ar_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(ar_en_id.EditValue))
        ar_credit_term.Properties.DataSource = dt_bantu
        ar_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ar_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ar_credit_term.ItemIndex = 0

        'ar_due_date.DateTime = ar_eff_date.DateTime.AddDays(ar_credit_term.GetColumnValue("code_usr_1"))
        'ar_expt_date.DateTime = ar_eff_date.DateTime.AddDays(ar_credit_term.GetColumnValue("code_usr_1"))

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_so_mstr(ar_en_id.EditValue))
        'ar_sb_id.Properties.DataSource = dt_bantu
        'ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("so_code").ToString
        'ar_sb_id.Properties.ValueMember = dt_bantu.Columns("so_id").ToString
        'ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(ar_en_id.EditValue))
        ar_cc_id.Properties.DataSource = dt_bantu
        ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ar_cc_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_taxclass_mstr(ar_en_id.EditValue))
        'ar_tax_class_id.Properties.DataSource = dt_bantu
        'ar_tax_class_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'ar_tax_class_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'ar_tax_class_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ar_en_id.EditValue, "ar_type"))
        ar_type.Properties.DataSource = dt_bantu
        ar_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ar_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ar_type.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_bk_mstr(ar_en_id.EditValue))
        'ar_bk_id.Properties.DataSource = dt_bantu
        'ar_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        'ar_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        'ar_bk_id.ItemIndex = 0

        'ar_ac_id.EditValue = ar_bill_to.GetColumnValue("ptnr_ac_ar_id")

        Dim _filter As String

        _filter = "and bk_id in (SELECT  " _
            & "  bk_id " _
            & "FROM  " _
            & "  public.bk_mstr  " _
            & "WHERE  " _
            & "bk_ac_id in (SELECT  " _
            & "  enacc_ac_id " _
            & "FROM  " _
            & "  public.enacc_mstr  " _
            & "Where enacc_en_id=" & SetInteger(ar_en_id.EditValue) & " and enacc_code='dbcr_bank'))"

        dt_bantu = New DataTable
        If limit_account(ar_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_bk_mstr(ar_en_id.EditValue, _filter, True))
        Else
            dt_bantu = (func_data.load_bk_mstr(ar_en_id.EditValue))
        End If

        'dt_bantu = (func_data.load_bk_mstr(ar_en_id.EditValue, _filter))
        ar_bk_id.Properties.DataSource = dt_bantu
        ar_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        ar_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        ar_bk_id.ItemIndex = 0


        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(ar_en_id.EditValue) & " and enacc_code='dbcr_account')"

        dt_bantu = New DataTable
        If limit_account(ar_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If
        'dt_bantu = (func_data.load_ac_mstr(_filter))
        ar_ac_id.Properties.DataSource = dt_bantu
        ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ar_ac_id.ItemIndex = 0


    End Sub

    Private Sub ap_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Sub ap_cu_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_cu_id.EditValueChanged
        If ar_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            ar_exc_rate.EditValue = func_data.get_exchange_rate(ar_cu_id.EditValue, ar_eff_date.DateTime) 'func_data.get_exchange_rate(ar_cu_id.EditValue)
        Else
            ar_exc_rate.EditValue = 1
        End If
    End Sub

    Private Sub ap_ptnr_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_bill_to.EditValueChanged
        ar_ac_id.EditValue = ar_bill_to.GetColumnValue("ptnr_ac_ar_id")

        Dim _ptnr_prepaid_balance As Double = 0
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(ptnr_prepaid_balance,0) as ptnr_prepaid_balance from ptnr_mstr " + _
                                           " where ptnr_id = " + ar_bill_to.EditValue.ToString
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

        'te_prepayment_balance.EditValue = _ptnr_prepaid_balance
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exc. Rate", "ar_exc_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Credit Terms", "credit_terms_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Due Date", "ar_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Expected Date", "ar_expt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Taxable", "ar_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Tax Include", "ar_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Type", "ar_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "ar_type_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Prepayment", "ar_pay_prepaid", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Prepayment IDR", "ar_pay_prepaid_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_master, "Account Code Prepayment", "ar_name_prepaid", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Name Prepayment", "ar_code_prepaid", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ar_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Payment", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Outstanding Payment", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total IDR", "ar_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Payment IDR", "ar_pay_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Outstanding Payment IDR", "ar_outstanding_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Status", "ar_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ar_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ar_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ar_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ar_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_so, "arso_ar_oid", False)
        add_column_copy(gv_detail_so, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_shipment, "ars_ar_oid", False)
        add_column_copy(gv_detail_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Taxable", "ars_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Tax Include", "ars_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Qty Open", "ars_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Qty Invoice", "ars_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "SO Price", "ars_so_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "SO Discount", "ars_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_shipment, "Close Line", "ars_close_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_shipment, "Faktur Pajak Status", "ars_fp_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_piutang, "arso_ar_oid", False)
        add_column(gv_detail_piutang, "sokp_ar_oid", False)
        add_column_copy(gv_detail_piutang, "Seq", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_piutang, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_piutang, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_piutang, "Payment Amount", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_piutang, "Payment Date", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_piutang, "Status", "sokp_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_piutang, "User Create", "sokp_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_piutang, "Date Create", "sokp_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail_piutang, "User Update", "sokp_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_piutang, "Date Update", "sokp_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_dist, "ard_ar_oid", False)
        add_column_copy(gv_detail_dist, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Taxable", "apd_taxable", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "Tax Include", "apd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        ''kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column_copy(gv_detail_dist, "Remarks", "ard_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_dist, "Amount", "ard_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_dist, "Status", "ard_tax_distribution", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_so, "arso_oid", False)
        add_column(gv_edit_so, "arso_ar_oid", False)
        add_column(gv_edit_so, "arso_so_oid", False)
        add_column(gv_edit_so, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_so, "arso_so_date", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_shipment, "ars_oid", False)
        add_column(gv_edit_shipment, "ars_ar_oid", False)
        add_column_edit(gv_edit_shipment, "#", "ceklist", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit_shipment, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "pt_id", False)
        add_column(gv_edit_shipment, "sod_ppn_type", False)
        add_column(gv_edit_shipment, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Taxable", "ars_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Tax Include", "ars_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "ars_tax_class_id", False)
        add_column(gv_edit_shipment, "Tax Class", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_shipment, "Qty Open", "ars_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_shipment, "Qty Invoice", "ars_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_shipment, "SO Price", "ars_so_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_shipment, "SO Discount", "ars_so_disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        If func_coll.get_conf_file("editable_invoice_price") = "1" Then
            add_column_edit(gv_edit_shipment, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        Else
            add_column(gv_edit_shipment, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If

        'add_column_edit(gv_edit_shipment, "Invoice Price", "ars_invoice_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_shipment, "Close Line", "ars_close_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_shipment, "Faktur Pajak Status", "ars_fp_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_dist, "ard_oid", False)
        add_column(gv_edit_dist, "ard_ac_id", False)
        add_column(gv_edit_dist, "Account", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "ard_sb_id", False)
        add_column(gv_edit_dist, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "ard_cc_id", False)
        add_column(gv_edit_dist, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Taxable", "ard_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Tax Include", "ard_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "ard_tax_class_id", False)
        add_column(gv_edit_dist, "TaxClass", "taxclass_name", DevExpress.Utils.HorzAlignment.Default)
        'kalau dari po tax_class_name dan tax_include akan kosong, karena datanya di group berdasar account...dan bisa dari taxclass yang berbeda
        add_column(gv_edit_dist, "Remarks", "ard_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_dist, "Amount", "ard_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column(gv_edit_dist, "Status", "ard_tax_distribution", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit_dist, "Recalculate", "apd_recalculate", DevExpress.Utils.HorzAlignment.Default) 'untuk membantu proses recalculate tax u/ yang input ap manual tanpa po

        add_column(gv_edit_piutang, "sokp_oid", False)
        add_column(gv_edit_piutang, "Seq", "sokp_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_piutang, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_piutang, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_dom_id, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_add_by, " _
                & "  public.ar_mstr.ar_add_date, " _
                & "  public.ar_mstr.ar_upd_by, " _
                & "  public.ar_mstr.ar_upd_date, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_exc_rate, " _
                & "  public.ar_mstr.ar_credit_term, " _
                & "  public.ar_mstr.ar_eff_date, " _
                & "  public.ar_mstr.ar_disc_date, " _
                & "  public.ar_mstr.ar_expt_date, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_taxable, " _
                & "  public.ar_mstr.ar_tax_inc, " _
                & "  public.ar_mstr.ar_tax_class_id, " _
                & "  public.ar_mstr.ar_pay_prepaid, " _
                & "  public.ar_mstr.ar_pay_prepaid * ar_exc_rate as ar_pay_prepaid_idr, " _
                & "  public.ar_mstr.ar_ac_prepaid, " _
                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_amount * ar_exc_rate as ar_amount_idr, " _
                & "  public.ar_mstr.ar_pay_amount * ar_exc_rate as ar_pay_amount_idr, " _
                & "  (public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount) * ar_exc_rate as ar_outstanding_idr, " _
                & "  public.ar_mstr.ar_remarks, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_dt, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  credit_term_mstr.code_name as credit_terms_name, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  public.ar_mstr.ar_bk_id, " _
                & "  public.bk_mstr.bk_name, " _
                & "  ar_type.code_name as ar_type_name, " _
                & "  taxclass_mstr.code_name as taxclass_name,coalesce(ar_ppn_type,'E') as ar_ppn_type " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  LEFT OUTER JOIN public.code_mstr credit_term_mstr ON (public.ar_mstr.ar_credit_term = credit_term_mstr.code_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ar_mstr.ar_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                & "  LEFT OUTER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  LEFT OUTER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  LEFT OUTER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = bk_mstr.bk_id) " _
                & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.ar_mstr.ar_tax_class_id = taxclass_mstr.code_id) " _
                & " where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & "   "

        If par_cus.Text <> "" Then
            If par_so.Text <> "" Then

                get_sequel = "SELECT  " _
                & "  public.ar_mstr.ar_oid, " _
                & "  public.ar_mstr.ar_dom_id, " _
                & "  public.ar_mstr.ar_en_id, " _
                & "  public.ar_mstr.ar_add_by, " _
                & "  public.ar_mstr.ar_add_date, " _
                & "  public.ar_mstr.ar_upd_by, " _
                & "  public.ar_mstr.ar_upd_date, " _
                & "  public.ar_mstr.ar_code, " _
                & "  public.ar_mstr.ar_date, " _
                & "  public.ar_mstr.ar_bill_to, " _
                & "  public.ar_mstr.ar_cu_id, " _
                & "  public.ar_mstr.ar_exc_rate, " _
                & "  public.ar_mstr.ar_credit_term, " _
                & "  public.ar_mstr.ar_eff_date, " _
                & "  public.ar_mstr.ar_disc_date, " _
                & "  public.ar_mstr.ar_expt_date, " _
                & "  public.ar_mstr.ar_ac_id, " _
                & "  public.ar_mstr.ar_sb_id, " _
                & "  public.ar_mstr.ar_cc_id, " _
                & "  public.ar_mstr.ar_type, " _
                & "  public.ar_mstr.ar_taxable, " _
                & "  public.ar_mstr.ar_tax_inc, " _
                & "  public.ar_mstr.ar_tax_class_id, " _
                & "  public.ar_mstr.ar_pay_prepaid, " _
                & "  public.ar_mstr.ar_pay_prepaid * ar_exc_rate as ar_pay_prepaid_idr, " _
                & "  public.ar_mstr.ar_ac_prepaid, " _
                & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
                & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
                & "  public.ar_mstr.ar_amount, " _
                & "  public.ar_mstr.ar_pay_amount, " _
                & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
                & "  public.ar_mstr.ar_amount * ar_exc_rate as ar_amount_idr, " _
                & "  public.ar_mstr.ar_pay_amount * ar_exc_rate as ar_pay_amount_idr, " _
                & "  (public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount) * ar_exc_rate as ar_outstanding_idr, " _
                & "  public.ar_mstr.ar_remarks, " _
                & "  public.ar_mstr.ar_status, " _
                & "  public.ar_mstr.ar_dt, " _
                & "  public.ar_mstr.ar_due_date, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.cu_mstr.cu_name, " _
                & "  credit_term_mstr.code_name as credit_terms_name, " _
                & "  public.ac_mstr.ac_code, " _
                & "  public.ac_mstr.ac_name, " _
                & "  public.sb_mstr.sb_desc, " _
                & "  public.cc_mstr.cc_desc, " _
                & "  public.ar_mstr.ar_bk_id, " _
                & "  public.bk_mstr.bk_name, " _
                & "  ar_type.code_name as ar_type_name, " _
                & "  taxclass_mstr.code_name as taxclass_name,coalesce(ar_ppn_type,'E') as ar_ppn_type " _
                & "FROM " _
                & "  public.ar_mstr " _
                & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
                & "  LEFT OUTER JOIN public.code_mstr credit_term_mstr ON (public.ar_mstr.ar_credit_term = credit_term_mstr.code_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
                & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ar_mstr.ar_ac_prepaid = ac_mstr_prepaid.ac_id) " _
                & "  LEFT OUTER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
                & "  LEFT OUTER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
                & "  LEFT OUTER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
                & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = bk_mstr.bk_id) " _
                & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.ar_mstr.ar_tax_class_id = taxclass_mstr.code_id) " _
                & "  INNER JOIN public.arso_so ON arso_ar_oid = ar_oid " _
                & " where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " and arso_so_code='" & par_so.Text & "'"

            Else
                get_sequel = "SELECT  " _
               & "  public.ar_mstr.ar_oid, " _
               & "  public.ar_mstr.ar_dom_id, " _
               & "  public.ar_mstr.ar_en_id, " _
               & "  public.ar_mstr.ar_add_by, " _
               & "  public.ar_mstr.ar_add_date, " _
               & "  public.ar_mstr.ar_upd_by, " _
               & "  public.ar_mstr.ar_upd_date, " _
               & "  public.ar_mstr.ar_code, " _
               & "  public.ar_mstr.ar_date, " _
               & "  public.ar_mstr.ar_bill_to, " _
               & "  public.ar_mstr.ar_cu_id, " _
               & "  public.ar_mstr.ar_exc_rate, " _
               & "  public.ar_mstr.ar_credit_term, " _
               & "  public.ar_mstr.ar_eff_date, " _
               & "  public.ar_mstr.ar_disc_date, " _
               & "  public.ar_mstr.ar_expt_date, " _
               & "  public.ar_mstr.ar_ac_id, " _
               & "  public.ar_mstr.ar_sb_id, " _
               & "  public.ar_mstr.ar_cc_id, " _
               & "  public.ar_mstr.ar_type, " _
               & "  public.ar_mstr.ar_taxable, " _
               & "  public.ar_mstr.ar_tax_inc, " _
               & "  public.ar_mstr.ar_tax_class_id, " _
               & "  public.ar_mstr.ar_pay_prepaid, " _
               & "  public.ar_mstr.ar_pay_prepaid * ar_exc_rate as ar_pay_prepaid_idr, " _
               & "  public.ar_mstr.ar_ac_prepaid, " _
               & "  ac_mstr_prepaid.ac_code as ac_code_prepaid, " _
               & "  ac_mstr_prepaid.ac_name as ac_name_prepaid, " _
               & "  public.ar_mstr.ar_amount, " _
               & "  public.ar_mstr.ar_pay_amount, " _
               & "  public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount as ar_outstanding, " _
               & "  public.ar_mstr.ar_amount * ar_exc_rate as ar_amount_idr, " _
               & "  public.ar_mstr.ar_pay_amount * ar_exc_rate as ar_pay_amount_idr, " _
               & "  (public.ar_mstr.ar_amount - public.ar_mstr.ar_pay_amount) * ar_exc_rate as ar_outstanding_idr, " _
               & "  public.ar_mstr.ar_remarks, " _
               & "  public.ar_mstr.ar_status, " _
               & "  public.ar_mstr.ar_dt, " _
               & "  public.ar_mstr.ar_due_date, " _
               & "  public.en_mstr.en_desc, " _
               & "  public.ptnr_mstr.ptnr_name, " _
               & "  public.cu_mstr.cu_name, " _
               & "  credit_term_mstr.code_name as credit_terms_name, " _
               & "  public.ac_mstr.ac_code, " _
               & "  public.ac_mstr.ac_name, " _
               & "  public.sb_mstr.sb_desc, " _
               & "  public.cc_mstr.cc_desc, " _
               & "  public.ar_mstr.ar_bk_id, " _
               & "  public.bk_mstr.bk_name, " _
               & "  ar_type.code_name as ar_type_name, " _
               & "  taxclass_mstr.code_name as taxclass_name,coalesce(ar_ppn_type,'E') as ar_ppn_type " _
               & "FROM " _
               & "  public.ar_mstr " _
               & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
               & "  INNER JOIN public.cu_mstr ON (public.ar_mstr.ar_cu_id = public.cu_mstr.cu_id) " _
               & "  LEFT OUTER JOIN public.code_mstr credit_term_mstr ON (public.ar_mstr.ar_credit_term = credit_term_mstr.code_id) " _
               & "  LEFT OUTER JOIN public.ac_mstr ON (public.ar_mstr.ar_ac_id = public.ac_mstr.ac_id) " _
               & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_prepaid ON (public.ar_mstr.ar_ac_prepaid = ac_mstr_prepaid.ac_id) " _
               & "  LEFT OUTER JOIN public.sb_mstr ON (public.ar_mstr.ar_sb_id = public.sb_mstr.sb_id) " _
               & "  LEFT OUTER JOIN public.cc_mstr ON (public.ar_mstr.ar_cc_id = public.cc_mstr.cc_id) " _
               & "  LEFT OUTER JOIN public.code_mstr ar_type ON (public.ar_mstr.ar_type = ar_type.code_id) " _
               & "  INNER JOIN public.bk_mstr ON (public.ar_mstr.ar_bk_id = bk_mstr.bk_id) " _
               & "  LEFT OUTER JOIN public.code_mstr taxclass_mstr ON (public.ar_mstr.ar_tax_class_id = taxclass_mstr.code_id) " _
               & " where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
               & " and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
               & " and ar_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
               & " and ar_bill_to=" & _par_cus_id

            End If
        End If
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail_so").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  arso_oid, " _
            & "  arso_ar_oid, " _
            & "  arso_seq, " _
            & "  arso_so_oid, " _
            & "  arso_so_code, " _
            & "  arso_so_date, " _
            & "  arso_dt, " _
            & "  arso_amount " _
            & "FROM  " _
            & "  public.arso_so  " _
            & "  inner join public.ar_mstr on ar_mstr.ar_oid = arso_ar_oid" _
            & "  inner join public.so_mstr on so_mstr.so_oid = arso_so_oid" _
            & "  where ar_mstr.ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ar_mstr.ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail_so, "detail_so")

        Try
            ds.Tables("detail_shipment").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  ars_oid, " _
            & "  ars_ar_oid, " _
            & "  ars_seq, " _
            & "  ars_soshipd_oid, " _
            & "  soship_code, " _
            & "  sod_oid, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  ars_taxable, " _
            & "  ars_tax_class_id, " _
            & "  code_name as taxclass_name, " _
            & "  ars_tax_inc, " _
            & "  ars_open, " _
            & "  ars_invoice, " _
            & "  ars_so_price, " _
            & "  ars_so_disc_value, " _
            & "  ars_invoice_price, " _
            & "  ars_close_line, " _
            & "  ars_fp_status, " _
            & "  ars_dt " _
            & "FROM  " _
            & "  public.ars_ship " _
            & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
            & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
            & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
            & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
            & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
            & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
            & "  where ar_mstr.ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ar_mstr.ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail_shipment, "detail_shipment")

        Try
            ds.Tables("detail_dist").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.ard_dist.ard_oid, " _
            & "  public.ard_dist.ard_ar_oid, " _
            & "  public.ard_dist.ard_tax_distribution, " _
            & "  public.ard_dist.ard_taxable, " _
            & "  public.ard_dist.ard_tax_inc, " _
            & "  public.ard_dist.ard_tax_class_id, " _
            & "  public.ard_dist.ard_ac_id, " _
            & "  public.ard_dist.ard_sb_id, " _
            & "  public.ard_dist.ard_cc_id, " _
            & "  public.ard_dist.ard_amount, " _
            & "  public.ard_dist.ard_remarks, " _
            & "  public.ard_dist.ard_dt, " _
            & "  public.ac_mstr.ac_code, " _
            & "  public.ac_mstr.ac_name, " _
            & "  public.sb_mstr.sb_desc, " _
            & "  public.code_mstr.code_name, " _
            & "  public.cc_mstr.cc_desc " _
            & "FROM " _
            & "  public.ard_dist " _
            & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
            & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
            & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
            & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
            & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
            & "  where ar_mstr.ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ar_mstr.ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail_dist, "detail_dist")

        Try
            ds.Tables("detail_piutang").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  sokp_oid, " _
            & "  arso_ar_oid, " _
            & "  sokp_so_oid, " _
            & "  sokp_seq, " _
            & "  sokp_amount, " _
            & "  sokp_due_date, " _
            & "  sokp_amount_pay, " _
            & "  sokp_description,sokp_date_payment,sokp_status,sokp_add_by,sokp_add_date,sokp_upd_by,sokp_upd_date " _
            & "FROM  " _
            & "  public.sokp_piutang " _
            & "  inner join public.arso_so on arso_so_oid = sokp_so_oid " _
            & "  inner join public.ar_mstr on ar_mstr.ar_oid = arso_ar_oid" _
            & "  where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and ar_en_id in (select user_en_id from tconfuserentity " _
                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by sokp_seq"

        load_data_detail(sql, gc_detail_piutang, "detail_kp")


    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_so.Columns("arso_ar_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("arso_ar_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString & "'")
            gv_detail_so.BestFitColumns()

            gv_detail_shipment.Columns("ars_ar_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ars_ar_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString & "'")
            gv_detail_shipment.BestFitColumns()

            gv_detail_dist.Columns("ard_ar_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("ard_ar_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString & "'")
            gv_detail_dist.BestFitColumns()

            gv_detail_piutang.Columns("arso_ar_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("arso_ar_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString & "'")
            gv_detail_piutang.BestFitColumns()

            'gv_detail_so.Columns("arso_ar_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid"))
            'gv_detail_so.BestFitColumns()

            'gv_detail_shipment.Columns("ars_ar_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid"))
            'gv_detail_shipment.BestFitColumns()

            'gv_detail_dist.Columns("ard_ar_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid"))
            'gv_detail_dist.BestFitColumns()

            'gv_detail_piutang.Columns("arso_ar_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid"))
            'gv_detail_piutang.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        ar_en_id.Focus()
        ar_en_id.ItemIndex = 0
        ar_bill_to.ItemIndex = 0
        ar_ac_id.ItemIndex = 0
        ar_cu_id.ItemIndex = 0
        ar_bk_id.ItemIndex = 0
        ar_eff_date.DateTime = _now
        'ar_due_date.DateTime = _now
        'ar_expt_date.DateTime = _now
        'ar_taxable.EditValue = False
        'ar_tax_inc.EditValue = False
        ar_remarks.Text = ""
        ar_exc_rate.EditValue = 1

        ar_en_id.Enabled = True
        ar_bill_to.Enabled = True
        ar_cu_id.Enabled = True
        ar_bk_id.Enabled = True
        ar_ac_id.Enabled = True
        ar_sb_id.Enabled = True
        ar_cc_id.Enabled = True
        ar_exc_rate.Enabled = True
        ar_ppn_type.ItemIndex = 0
        ar_ppn_type.Enabled = True

        ar_so_ref_code.Text = ""
        _ar_so_ref_codes = ""
        ar_so_ref_code.Enabled = True

        'ar_tax_inc.Enabled = True
        'ar_tax_class_id.Enabled = True
        'ar_pay_prepaid.EditValue = 0
        'ar_ac_prepaid.ItemIndex = 0
        gc_edit_so.Enabled = True
        gc_edit_shipment.Enabled = True
        gc_edit_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = True
        sb_retrieve_dist.Enabled = True
        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = True
        gc_edit_piutang.EmbeddedNavigator.Buttons.Append.Visible = True
        gc_edit_piutang.EmbeddedNavigator.Buttons.Remove.Visible = True

        gv_edit_dist.Columns("ard_taxable").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("ard_tax_inc").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("ard_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("ard_remarks").OptionsColumn.AllowEdit = True
        gv_edit_dist.Columns("ard_amount").OptionsColumn.AllowEdit = True
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_so = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  arso_oid, " _
                        & "  arso_ar_oid, " _
                        & "  arso_seq, " _
                        & "  arso_so_oid, " _
                        & "  arso_so_code, " _
                        & "  arso_so_date, " _
                        & "  arso_dt, " _
                        & "  arso_amount " _
                        & "FROM  " _
                        & "  public.arso_so  " _
                        & "  inner join public.ar_mstr on ar_mstr.ar_oid = arso_ar_oid" _
                        & "  inner join public.so_mstr on so_mstr.so_oid = arso_so_oid" _
                        & "  where arso_so_code ~~* 'asdfad'"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_so, "list_so")
                    gc_edit_so.DataSource = ds_edit_so.Tables(0)
                    gv_edit_so.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  ars_oid, True as ceklist, " _
                        & "  ars_ar_oid, " _
                        & "  ars_seq, " _
                        & "  ars_soshipd_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  sod_ppn_type, " _
                        & "  ars_taxable, " _
                        & "  ars_tax_class_id, " _
                        & "  code_name as taxclass_name, " _
                        & "  ars_tax_inc, " _
                        & "  ars_open, " _
                        & "  ars_invoice, " _
                        & "  ars_so_price, " _
                        & "  ars_so_disc_value, " _
                        & "  ars_invoice_price, " _
                        & "  ars_close_line, " _
                        & "  ars_dt " _
                        & "FROM  " _
                        & "  public.ars_ship " _
                        & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
                        & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
                        & " where ars_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                    gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                    gv_edit_shipment.BestFitColumns()
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
                        & "  public.ard_dist.ard_oid, " _
                        & "  public.ard_dist.ard_ar_oid, " _
                        & "  public.ard_dist.ard_tax_distribution, " _
                        & "  public.ard_dist.ard_taxable, " _
                        & "  public.ard_dist.ard_tax_inc, " _
                        & "  public.ard_dist.ard_tax_class_id, " _
                        & "  public.ard_dist.ard_ac_id, " _
                        & "  public.ard_dist.ard_sb_id, " _
                        & "  public.ard_dist.ard_cc_id, " _
                        & "  public.ard_dist.ard_amount, " _
                        & "  public.ard_dist.ard_remarks, " _
                        & "  public.ard_dist.ard_dt, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.sb_mstr.sb_desc, " _
                        & "  public.code_mstr.code_name as taxclass_name, " _
                        & "  public.cc_mstr.cc_desc " _
                        & "FROM " _
                        & "  public.ard_dist " _
                        & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
                        & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
                        & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
                        & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
                        & " where ard_amount = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_dist, "dist")
                    gc_edit_dist.DataSource = ds_edit_dist.Tables(0)
                    gv_edit_dist.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_piutang = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  sokp_oid, " _
                        & "  sokp_so_oid, " _
                        & "  sokp_seq, " _
                        & "  sokp_amount, " _
                        & "  sokp_due_date, " _
                        & "  sokp_amount_pay, " _
                        & "  sokp_description " _
                        & "FROM  " _
                        & "  public.sokp_piutang " _
                        & "  inner join public.so_mstr on so_oid = sokp_so_oid " _
                        & "  where sokp_seq = -99 "

                    .InitializeCommand()
                    .FillDataSet(ds_edit_piutang, "piutang")
                    gc_edit_piutang.DataSource = ds_edit_piutang.Tables(0)
                    gv_edit_piutang.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit_so.UpdateCurrentRow()
        gv_edit_shipment.UpdateCurrentRow()
        gv_edit_dist.UpdateCurrentRow()

        ds_edit_so.AcceptChanges()
        ds_edit_shipment.AcceptChanges()
        ds_edit_dist.AcceptChanges()

        If ar_exc_rate.EditValue = 0 Then
            MessageBox.Show("Exc Rate can't 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ar_en_id.EditValue, "gcald_ar", ar_eff_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + ar_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + ar_eff_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        '*********************
        'Cek close line di tab shipment
        Dim i As Integer
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            With ds_edit_shipment.Tables(0).Rows(i)
                If (.Item("ars_open") = .Item("ars_invoice")) And (.Item("ars_so_price") = .Item("ars_invoice_price")) Then
                    .Item("ars_close_line") = "Y"
                End If
            End With
        Next
        '*********************

        If ds_edit_dist.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        'If te_prepayment_balance.EditValue > 0 And ar_pay_prepaid.EditValue = 0 Then
        '    If MessageBox.Show("Do Not Use Prepaid Balance, Continue Saving Data ?", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
        '        Return False
        '    End If
        'End If

        Dim _total As Double = 0
        For i = 0 To ds_edit_piutang.Tables(0).Rows.Count - 1
            With ds_edit_piutang.Tables(0).Rows(i)
                _total = _total + SetNumber(.Item("sokp_amount"))

            End With
        Next

        If System.Math.Round(_total, 0) <> System.Math.Round(_total_piutang, 0) Then
            MessageBox.Show("Total Piutang not balance total " & MaskDec(_total) & ", total piutang " & MaskDec(_total_piutang), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ar_oid_mstr = .Item("ar_oid").ToString
                ar_en_id.EditValue = .Item("ar_en_id")
                ar_bill_to.EditValue = .Item("ar_bill_to")
                ar_cu_id.EditValue = .Item("ar_cu_id")
                ar_bk_id.EditValue = .Item("ar_bk_id")
                ar_eff_date.DateTime = .Item("ar_eff_date")
                'ar_due_date.DateTime = .Item("ar_due_date")
                'ar_expt_date.DateTime = .Item("ar_expt_date")
                ar_exc_rate.EditValue = .Item("ar_exc_rate")
                'ar_taxable.EditValue = SetBitYNB(.Item("ar_taxable"))
                'ar_tax_inc.EditValue = SetBitYNB(.Item("ar_tax_inc"))
                'ar_tax_class_id.EditValue = .Item("ar_tax_class_id")
                ar_ppn_type.EditValue = .Item("ar_ppn_type")
                ar_ac_id.EditValue = .Item("ar_ac_id")
                ar_sb_id.EditValue = .Item("ar_sb_id")
                ar_cc_id.EditValue = .Item("ar_cc_id")
                ar_remarks.Text = SetString(.Item("ar_remarks"))
                ar_type.EditValue = .Item("ar_type")
                ar_credit_term.EditValue = .Item("ar_credit_term")
                'ar_pay_prepaid.EditValue = .Item("ar_pay_prepaid")
                'ar_ac_prepaid.EditValue = .Item("ar_ac_prepaid")
            End With

            ar_en_id.Focus()
            ar_en_id.Enabled = False
            ar_bill_to.Enabled = False
            ar_cu_id.Enabled = False
            ar_bk_id.Enabled = False
            ar_ac_id.Enabled = False
            ar_exc_rate.Enabled = False
            'ar_tax_inc.Enabled = False
            'ar_tax_class_id.Enabled = False
            'ar_taxable.Enabled = False
            ar_ppn_type.Enabled = False
            ar_sb_id.Enabled = False
            ar_cc_id.Enabled = False
            'ar_pay_prepaid.Enabled = False
            'ar_ac_prepaid.Enabled = False
            gc_edit_so.Enabled = False
            gc_edit_shipment.Enabled = False
            gc_edit_dist.Enabled = False
            ar_eff_date.Enabled = False
            sb_retrieve_receive_item.Enabled = False
            sb_retrieve_dist.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_so = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  arso_oid, " _
                            & "  arso_ar_oid, " _
                            & "  arso_seq, " _
                            & "  arso_so_oid, " _
                            & "  arso_so_code, " _
                            & "  arso_so_date, " _
                            & "  arso_dt, " _
                            & "  arso_amount " _
                            & "FROM  " _
                            & "  public.arso_so  " _
                            & "  inner join public.ar_mstr on ar_mstr.ar_oid = arso_ar_oid" _
                            & "  inner join public.so_mstr on so_mstr.so_oid = arso_so_oid" _
                            & "  where arso_ar_oid = '" + _ar_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_so, "list_so")
                        gc_edit_so.DataSource = ds_edit_so.Tables(0)
                        gv_edit_so.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_shipment = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  ars_oid, " _
                            & "  ars_ar_oid, " _
                            & "  ars_seq, " _
                            & "  ars_soshipd_oid, " _
                            & "  soship_code, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  sod_ppn_type, " _
                            & "  ars_taxable, " _
                            & "  ars_tax_class_id, " _
                            & "  code_name as taxclass_name, " _
                            & "  ars_tax_inc, " _
                            & "  ars_open, " _
                            & "  ars_invoice, " _
                            & "  ars_so_price, " _
                            & "  ars_so_disc_value, " _
                            & "  ars_invoice_price, " _
                            & "  ars_close_line, " _
                            & "  ars_dt " _
                            & "FROM  " _
                            & "  public.ars_ship " _
                            & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
                            & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                            & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                            & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                            & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
                            & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
                            & " where ars_ar_oid = '" + _ar_oid_mstr + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit_shipment, "shipment")
                        gc_edit_shipment.DataSource = ds_edit_shipment.Tables(0)
                        gv_edit_shipment.BestFitColumns()
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
                                & "  public.ard_dist.ard_oid, " _
                                & "  public.ard_dist.ard_ar_oid, " _
                                & "  public.ard_dist.ard_tax_distribution, " _
                                & "  public.ard_dist.ard_taxable, " _
                                & "  public.ard_dist.ard_tax_inc, " _
                                & "  public.ard_dist.ard_tax_class_id, " _
                                & "  public.ard_dist.ard_ac_id, " _
                                & "  public.ard_dist.ard_sb_id, " _
                                & "  public.ard_dist.ard_cc_id, " _
                                & "  public.ard_dist.ard_amount, " _
                                & "  public.ard_dist.ard_remarks, " _
                                & "  public.ard_dist.ard_dt, " _
                                & "  public.ac_mstr.ac_code, " _
                                & "  public.ac_mstr.ac_name, " _
                                & "  public.sb_mstr.sb_desc, " _
                                & "  public.code_mstr.code_name, " _
                                & "  public.cc_mstr.cc_desc " _
                                & "FROM " _
                                & "  public.ard_dist " _
                                & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
                                & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
                                & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
                                & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
                                & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
                                & " where ard_ar_oid = '" + _ar_oid_mstr + "'"

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
                                            & "  public.ar_mstr   " _
                                            & "SET  " _
                                            & "  ar_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  ar_en_id = " & SetInteger(ar_en_id.EditValue) & ",  " _
                                            & "  ar_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ar_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ar_credit_term = " & SetSetring(ar_credit_term.EditValue) & ",  " _
                                            & "  ar_eff_date = " & SetDate(ar_eff_date.DateTime) & ",  " _
                                            & "  ar_type = " & SetSetring(ar_type.EditValue) & ",  " _
                                            & "  ar_remarks = " & SetSetring(ar_remarks.Text) & ",  " _
                                            & "  ar_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ar_oid = " & SetSetring(_ar_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, ar_en_id.EditValue, 13, _ar_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code"), ar_eff_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        'Yang diperbolehkan edit hanya data master saja,
                        'Untuk yang detail terlalu rumit karena sudah generate jurnal
                        'arabila salah untuk detail, lakukan delete aja, maka akan terjadi jurnal balik
                        'Lalu lakukan insert data lagi....

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
                        set_row(_ar_oid_mstr, "ar_oid")
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

        Dim _ar_eff_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_eff_date")
        Dim _ar_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_ar_en_id, "gcald_ar", _ar_eff_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _ar_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _ar_eff_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Harus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

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

                            For i = 0 To ds.Tables("detail_shipment").Rows.Count - 1
                                If ds.Tables("detail_shipment").Rows(i).Item("ars_ar_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString Then
                                    'Update Table shipment Detail untuk kolom soshipd_qty_inv dan soshipd_close_line nya
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update soshipd_det set soshipd_qty_inv = soshipd_qty_inv - " + SetDec(ds.Tables("detail_shipment").Rows(i).Item("ars_invoice").ToString) + _
                                                           ", soshipd_close_line = 'N'" + _
                                                           " where soshipd_oid = '" + ds.Tables("detail_shipment").Rows(i).Item("ars_soshipd_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    'Update Table so Detail untuk kolom sod_qty_invoice
                                    If ds.Tables("detail_shipment").Rows(i).Item("ars_invoice") > 0 Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update sod_det set sod_qty_invoice = sod_qty_invoice - " + SetDec(ds.Tables("detail_shipment").Rows(i).Item("ars_invoice").ToString) + _
                                                               " where sod_oid = " + SetSetring(ds.Tables("detail_shipment").Rows(i).Item("sod_oid").ToString)
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                End If
                            Next

                            'sqlTran.Rollback()
                            'Return False

                            'Update Table shipment Detail untuk kolom soshipd_qty_inv dan soshipd_close_line nya
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) - " + SetDbl(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_pay_prepaid")) + _
                                                   " where ptnr_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_bill_to").ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from sokp_piutang  " _
                                                 & "where sokp_so_oid in (select so_oid  " _
                                                   & "from so_mstr " _
                                                   & "inner join arso_so on arso_so_oid = so_oid " _
                                                   & "inner join ar_mstr on ar_oid = arso_ar_oid " _
                                                   & "where ar_code = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code").ToString) + ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ar_mstr where ar_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If _create_jurnal = True Then
                                If func_coll.delete_glt_det_ar(ssqls, objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString, _
                                                           ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code")) = False Then
                                    sqlTran.Rollback()
                                    Exit Function
                                End If
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Debit Credit Memo SDI " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code"))
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

    Private Sub gv_edit_so_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.DoubleClick
        browse_so()
    End Sub

    Private Sub ar_so_ref_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ar_so_ref_code.ButtonClick
        Dim frm As New FSalesOrderSearch
        frm.set_win(Me)
        frm._en_id = ar_en_id.EditValue
        frm._date = ar_eff_date.DateTime
        frm._ar_credit_term_id = ar_credit_term.EditValue
        frm.type_form = True
        'frm._tran_oid = _so_sq_ref_oid
        frm.ShowDialog()
    End Sub


    Private Sub gv_edit_so_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_so.RowCountChanged
        If ds_edit_so.Tables(0).Rows.Count > 1 Then
            'ds_edit_so.Tables(0).RejectChanges()
            ds_edit_so.Tables(0).Rows(1).Delete()
            ds_edit_so.Tables(0).AcceptChanges()
            MessageBox.Show("AR SDI Just For One SO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub gv_edit_so_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_so.InitNewRow
        'If ds_edit_so.Tables(0).Rows.Count > 1 Then
        '    ds_edit_so.Tables(0).Rows(1).Delete()
        '    ds_edit_so.Tables(0).AcceptChanges()
        '    MessageBox.Show("AR SDI Just For One SO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub

    Private Sub gv_edit_so_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit_so.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit_so.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit_so.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_so_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit_so.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_so()
        End If
    End Sub

    Private Sub browse_so()
        Dim _col As String = gv_edit_so.FocusedColumn.Name
        Dim _row As Integer = gv_edit_so.FocusedRowHandle

        'Browse PO berdasar kepada entity, patner, currency......
        If _col = "arso_so_code" Then
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ar_en_id.EditValue
            frm._ptnr_id = ar_bill_to.EditValue
            frm._cu_id = ar_cu_id.EditValue
            frm._obj = gv_edit_so
            frm._ppn_type = ar_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub sb_retrieve_shipment_item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_receive_item.Click
        If ds_edit_so.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_so.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim _so_code As String = ""
        Dim i As Integer

        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            _so_code = _so_code + "'" + ds_edit_so.Tables(0).Rows(i).Item("arso_so_code") + "',"
        Next

        _so_code = _so_code.Substring(0, _so_code.Length - 1)

        Dim ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  soshipd_oid, True as ceklist, " _
                        & "  soshipd_soship_oid, " _
                        & "  soshipd_sod_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  sod_ppn_type, " _
                        & "  sod_price, " _
                        & "  (sod_price * coalesce(sod_disc,0)) as sod_disc, " _
                        & "  sod_taxable, " _
                        & "  sod_tax_inc, " _
                        & "  sod_tax_class, " _
                        & "  code_name as sod_tax_class_name, " _
                        & "  (soshipd_qty - coalesce(soshipd_qty_inv,0)) * -1 as qty_open " _
                        & "FROM  " _
                        & "  public.soshipd_det " _
                        & "  inner join soship_mstr on soship_oid = soshipd_soship_oid " _
                        & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                        & "  inner join code_mstr on code_id = sod_tax_class " _
                        & "  where coalesce(soshipd_close_line,'N') = 'N' " _
                        & "  and so_code in (" + _so_code + ") and sod_ppn_type=" & SetSetring(ar_ppn_type.EditValue) & " " _
                        & "  order by soship_date "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "soship_mstr")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_shipment.Tables(0).Clear()
        Dim _dtrow As DataRow
        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_shipment.Tables(0).NewRow
            _dtrow("ars_oid") = Guid.NewGuid.ToString
            _dtrow("ceklist") = ds_bantu.Tables(0).Rows(i).Item("ceklist")
            _dtrow("ars_soshipd_oid") = ds_bantu.Tables(0).Rows(i).Item("soshipd_oid").ToString
            _dtrow("soship_code") = ds_bantu.Tables(0).Rows(i).Item("soship_code")
            _dtrow("pt_id") = ds_bantu.Tables(0).Rows(i).Item("pt_id")
            _dtrow("pt_code") = ds_bantu.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_bantu.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_bantu.Tables(0).Rows(i).Item("pt_desc2")
            _dtrow("sod_ppn_type") = ds_bantu.Tables(0).Rows(i).Item("sod_ppn_type")
            _dtrow("ars_taxable") = ds_bantu.Tables(0).Rows(i).Item("sod_taxable")
            _dtrow("ars_tax_class_id") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class")
            _dtrow("taxclass_name") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_class_name")
            _dtrow("ars_tax_inc") = ds_bantu.Tables(0).Rows(i).Item("sod_tax_inc")
            _dtrow("ars_open") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
            _dtrow("ars_invoice") = ds_bantu.Tables(0).Rows(i).Item("qty_open")
            _dtrow("ars_so_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price")
            _dtrow("ars_so_disc_value") = ds_bantu.Tables(0).Rows(i).Item("sod_disc")
            _dtrow("ars_invoice_price") = ds_bantu.Tables(0).Rows(i).Item("sod_price") - ds_bantu.Tables(0).Rows(i).Item("sod_disc")
            _dtrow("ars_close_line") = "Y"
            ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit_shipment.Tables(0).AcceptChanges()

        gv_edit_shipment.BestFitColumns()

    End Sub

    Private Sub retrieve_from_shipment()
        If ds_edit_shipment.Tables.Count = 0 Then
            Exit Sub
        ElseIf ds_edit_shipment.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If
        Dim i, j As Integer

        gc_edit_dist.EmbeddedNavigator.Buttons.Append.Visible = False
        gc_edit_dist.EmbeddedNavigator.Buttons.Remove.Visible = False
        gv_edit_dist.Columns("taxclass_name").Visible = False
        gv_edit_dist.Columns("ard_tax_inc").Visible = False
        gv_edit_dist.Columns("ard_taxable").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("ard_tax_inc").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("ard_remarks").OptionsColumn.AllowEdit = False
        gv_edit_dist.Columns("ard_amount").OptionsColumn.AllowEdit = False

        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
        Dim _dtrow As DataRow
        Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
        _invoice_price = 0
        _line_tr_pph = 0
        _line_tr_ppn = 0
        _tax_rate = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                    'Mencari prodline account untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_prodline_account_ar(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        'If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
                        '(ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya line ppn saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                        Else
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                            (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                             ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price"))
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                        _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                        _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya dicari ppn nya saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            _dtrow("ard_amount") = _invoice_price
                        Else
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")
                            _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                        End If


                        _dtrow("ard_tax_distribution") = "Y"

                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH
        Dim _ppn, _pph As Double
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" And ds_edit_shipment.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                    'Mencari taxrate account ar untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                    '1. PPN
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next
                    'Exit Sub
                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_sod_cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _ppn
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If

                    '1. PPH
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _pph
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Ini untuk ar discount
        _search = False
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari prodline account untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_prodline_account_ar_discount(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya line ppn saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                 _invoice_price)
                                ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                            _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                            _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya dicari ppn nya saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                _dtrow("ard_amount") = _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1
                                _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                            End If


                            _dtrow("ard_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH yang ar discount
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari taxrate account ap untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                        '1. PPN
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            _dtrow("ard_amount") = _ppn
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If

                        '1. PPH
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("ard_amount") = _pph
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next
        '**************************************************

        ''Insert ke ds untuk yang prepayment kalau ada
        'If ar_pay_prepaid.EditValue < 0 Then
        '    _dtrow = ds_edit_dist.Tables(0).NewRow
        '    _dtrow("ard_oid") = Guid.NewGuid.ToString

        '    _dtrow("ard_ac_id") = ar_ac_prepaid.EditValue
        '    _dtrow("ac_code") = ar_ac_prepaid.GetColumnValue("ac_code")

        '    _dtrow("ard_sb_id") = 0
        '    _dtrow("sb_desc") = "-"
        '    _dtrow("ard_cc_id") = 0
        '    _dtrow("cc_desc") = "-"
        '    _dtrow("ard_taxable") = "N"
        '    _dtrow("ard_tax_class_id") = DBNull.Value
        '    _dtrow("taxclass_name") = DBNull.Value
        '    _dtrow("ard_remarks") = ar_ac_prepaid.GetColumnValue("ac_name")
        '    _dtrow("ard_amount") = ar_pay_prepaid.EditValue
        '    _dtrow("ard_tax_distribution") = "Y"
        '    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
        '    ds_edit_dist.Tables(0).AcceptChanges()
        '    '**********************************************************
        'End If

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
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
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit_dist.Tables(0).Rows(i).Item("ard_tax_class_id"))
                If ds_edit_dist.Tables(0).Rows(i).Item("ard_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = _tax_rate * (ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") / (1 + _tax_rate))
                Else
                    _ppn = ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") * _tax_rate
                End If


                ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") - _ppn
                ds_edit_dist.AcceptChanges()
            End If
        Next

        For i = 0 To ds_copy.Tables(0).Rows.Count - 1
            dt_bantu = New DataTable
            dt_bantu = (func_data.get_taxrate_ar_account(ds_copy.Tables(0).Rows(i).Item("ard_tax_class_id")))

            '1. PPN
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPN
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("ard_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    '_pod_cost = ds_edit.Tables(0).Rows(i).Item("pod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("pod_cost") / (1 + _tax_rate)))
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("ard_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("ard_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("ard_oid") = Guid.NewGuid.ToString

                _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                _dtrow("ard_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("ard_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("ard_taxable") = "N"
                _dtrow("ard_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                If ds_copy.Tables(0).Rows(i).Item("ard_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("ard_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                Else
                    _ppn = (ds_copy.Tables(0).Rows(i).Item("ard_amount")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                End If

                _dtrow("ard_amount") = _ppn
                _dtrow("ard_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If

            '2. PPH
            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                    _search = True
                    Exit For
                End If
            Next

            If _search = True Then
                If ds_copy.Tables(0).Rows(i).Item("ard_tax_inc").ToString.ToUpper = "Y" Then
                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                    'Item Price - Tax Amount = Taxable Base                            
                    '100.00 - 9.09 = 90.91 
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("ard_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("ard_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("ard_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'row(1) artinya mengacu ke pph
                End If

                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                ds_edit_dist.Tables(0).AcceptChanges()
            Else
                _dtrow = ds_edit_dist.Tables(0).NewRow
                _dtrow("ard_oid") = Guid.NewGuid.ToString

                _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                _dtrow("ard_sb_id") = 0
                _dtrow("sb_desc") = "-"
                _dtrow("ard_cc_id") = 0
                _dtrow("cc_desc") = "-"
                _dtrow("ard_taxable") = "N"
                _dtrow("ard_tax_class_id") = DBNull.Value
                _dtrow("taxclass_name") = DBNull.Value
                _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                If ds_copy.Tables(0).Rows(i).Item("ard_tax_inc").ToString.ToUpper = "Y" Then
                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_copy.Tables(0).Rows(i).Item("ard_amount") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                    _pph = ds_copy.Tables(0).Rows(i).Item("ard_amount") - _ppn
                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                Else
                    _pph = (ds_copy.Tables(0).Rows(i).Item("ard_amount")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'ini mengacu ke pph
                End If

                _dtrow("ard_amount") = _pph
                _dtrow("ard_tax_distribution") = "Y"
                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                ds_edit_dist.Tables(0).AcceptChanges()
            End If
        Next

        'Insert ke ds untuk yang prepayment kalau ada
        'If ar_pay_prepaid.EditValue <> 0 Then
        '    _dtrow = ds_edit_dist.Tables(0).NewRow
        '    _dtrow("ard_oid") = Guid.NewGuid.ToString

        '    _dtrow("ard_ac_id") = ar_ac_prepaid.EditValue
        '    _dtrow("ac_code") = ar_ac_prepaid.GetColumnValue("ac_code")

        '    _dtrow("ard_sb_id") = 0
        '    _dtrow("sb_desc") = "-"
        '    _dtrow("ard_cc_id") = 0
        '    _dtrow("cc_desc") = "-"
        '    _dtrow("ard_taxable") = "N"
        '    _dtrow("ard_tax_class_id") = DBNull.Value
        '    _dtrow("taxclass_name") = DBNull.Value
        '    _dtrow("ard_remarks") = ar_ac_prepaid.GetColumnValue("ac_name")
        '    _dtrow("ard_amount") = ar_pay_prepaid.EditValue
        '    _dtrow("ard_tax_distribution") = "Y"
        '    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
        '    ds_edit_dist.Tables(0).AcceptChanges()
        '    '**********************************************************
        'End If

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        'gc_edit_dist.Enabled = False
        'sb_retrieve_dist.Enabled = False
        'setelah di recalculate tidak boleh lagi diubah karena sudah hitung tax dan lain sebagainya...
        'kalau salah di cancel aja terus buat lagi dari awal
    End Sub

    Private Sub sb_retrieve_dist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve_dist.Click
        If ds_edit_so.Tables(0).Rows.Count = 0 Then
            recalculate()
        Else
            retrieve_from_shipment()
        End If
    End Sub

    Private Sub gv_edit_dist_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_dist.CellValueChanged
        If e.Column.Name = "ard_taxable" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "ard_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_tax_inc", "N")
                gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_tax_class_id", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit_dist.SetRowCellValue(e.RowHandle, "taxclass_name", "NON-TAX")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_recalculate", False)
            ElseIf gv_edit_dist.GetRowCellValue(e.RowHandle, "ard_taxable").ToString.ToUpper = "Y" Then
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_recalculate", True)
            End If
        ElseIf e.Column.Name = "ard_tax_inc" Then
            If gv_edit_dist.GetRowCellValue(e.RowHandle, "ard_tax_inc").ToString.ToUpper = "Y" And gv_edit_dist.GetRowCellValue(e.RowHandle, "ard_taxable").ToString.ToUpper = "N" Then
                gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_tax_inc", "N")
                'gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_recalculate", False)
            End If
        ElseIf e.Column.Name = "ard_amount" Then
            'gv_edit_dist.SetRowCellValue(e.RowHandle, "ard_recalculate", True)
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
            frm._cu_id = ar_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = ar_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "taxclass_name" Then
            If gv_edit_dist.GetRowCellValue(_row, "ard_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ar_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_dist_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_dist.InitNewRow
        gv_edit_dist.Columns("taxclass_name").Visible = True
        gv_edit_dist.Columns("ard_tax_inc").Visible = True
        'sb_retrieve_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = False

        With gv_edit_dist
            .SetRowCellValue(e.RowHandle, "ard_oid", Guid.NewGuid.ToString)
            '.SetRowCellValue(e.RowHandle, "ard_taxable", IIf(ar_taxable.EditValue = True, "Y", "N"))
            '.SetRowCellValue(e.RowHandle, "ard_tax_class_id", ar_tax_class_id.EditValue)
            '.SetRowCellValue(e.RowHandle, "taxclass_name", ar_tax_class_id.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "ard_tax_inc", "N")
            .SetRowCellValue(e.RowHandle, "ard_amount", 0)
            .SetRowCellValue(e.RowHandle, "ard_tax_distribution", "N")

            .SetRowCellValue(e.RowHandle, "ard_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "ard_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")
            .BestFitColumns()
        End With

        gv_edit_dist.Columns("taxclass_name").Visible = True
        gv_edit_dist.Columns("ard_tax_inc").Visible = True
        'sb_retrieve_dist.Enabled = True
        sb_retrieve_receive_item.Enabled = False
    End Sub

    'Private Sub ar_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If ar_taxable.EditValue = False Then
    '        ar_tax_class_id.Enabled = False
    '        ar_tax_class_id.ItemIndex = 0
    '        ar_tax_inc.Enabled = False
    '        ar_tax_inc.Checked = False
    '    Else
    '        ar_tax_class_id.Enabled = True
    '        ar_tax_inc.Enabled = True
    '        ar_tax_inc.Checked = False
    '    End If
    'End Sub

    Private Sub be_so_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_so_code.ButtonClick
        'Dim frm As New FSalesOrderSearch
        'frm.set_win(Me)
        'frm._en_id = ar_en_id.EditValue
        'frm._obj = be_so_code
        'frm.type_form = True
        'frm.ShowDialog()
    End Sub

    'Private Sub ar_credit_term_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_credit_term.EditValueChanged
    '    ar_due_date.DateTime = ar_eff_date.DateTime.AddDays(ar_credit_term.GetColumnValue("code_usr_1"))
    '    ar_expt_date.DateTime = ar_eff_date.DateTime.AddDays(ar_credit_term.GetColumnValue("code_usr_1"))
    'End Sub

    Public Overrides Function insert() As Boolean
        Dim _ar_oid As Guid
        _ar_oid = Guid.NewGuid

        Dim _ar_code, _ar_terbilang As String
        Dim _ar_amount As Double = 0
        Dim i, j As Integer
        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        _ar_code = func_coll.get_transaction_number("AR", ar_en_id.GetColumnValue("en_code"), "ar_mstr", "ar_code")

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _ar_amount = _ar_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        Next
        _ar_terbilang = func_bill.TERBILANG_FIX(_ar_amount)

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
                                            & "  public.ar_mstr " _
                                            & "( " _
                                            & "  ar_oid, " _
                                            & "  ar_dom_id, " _
                                            & "  ar_en_id, " _
                                            & "  ar_add_by, " _
                                            & "  ar_add_date, " _
                                            & "  ar_code, " _
                                            & "  ar_date, " _
                                            & "  ar_bill_to, " _
                                            & "  ar_cu_id, " _
                                            & "  ar_exc_rate, " _
                                            & "  ar_credit_term, " _
                                            & "  ar_eff_date, " _
                                            & "  ar_disc_date, " _
                                            & "  ar_expt_date, " _
                                            & "  ar_ac_id, " _
                                            & "  ar_sb_id, " _
                                            & "  ar_cc_id, " _
                                            & "  ar_type,ar_ppn_type, " _
                                            & "  ar_taxable, " _
                                            & "  ar_tax_inc, " _
                                            & "  ar_tax_class_id, " _
                                            & "  ar_ac_prepaid, " _
                                            & "  ar_pay_prepaid, " _
                                            & "  ar_amount, " _
                                            & "  ar_pay_amount, " _
                                            & "  ar_remarks, " _
                                            & "  ar_status, " _
                                            & "  ar_dt, " _
                                            & "  ar_bk_id, " _
                                            & "  ar_due_date, " _
                                            & "  ar_terbilang " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ar_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ar_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_ar_code) & ",  " _
                                            & " " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetInteger(ar_bill_to.EditValue) & ",  " _
                                            & SetInteger(ar_cu_id.EditValue) & ",  " _
                                            & SetDbl(ar_exc_rate.EditValue) & ",  " _
                                            & SetInteger(ar_credit_term.EditValue) & ",  " _
                                            & SetDate(ar_eff_date.DateTime) & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & SetInteger(ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(ar_cc_id.EditValue) & ",  " _
                                            & SetInteger(ar_type.EditValue) & ",  " _
                                             & SetSetring(ar_ppn_type.EditValue) & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & "null" & ",  " _
                                            & SetDbl(_ar_amount) & ",  " _
                                            & "0" & ",  " _
                                            & SetSetring(ar_remarks.Text) & ",  " _
                                            & "null" & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(ar_bk_id.EditValue) & ",  " _
                                            & "null" & ",  " _
                                            & SetSetring(_ar_terbilang) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk Insert Data List so
                        For i = 0 To ds_edit_so.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.arso_so " _
                                                & "( " _
                                                & "  arso_oid, " _
                                                & "  arso_ar_oid, " _
                                                & "  arso_seq, " _
                                                & "  arso_so_oid, " _
                                                & "  arso_so_code, " _
                                                & "  arso_so_date, " _
                                                & "  arso_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("arso_oid").ToString) & ",  " _
                                                & SetSetring(_ar_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("arso_so_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("arso_so_code").ToString) & ",  " _
                                                & SetDate(ds_edit_so.Tables(0).Rows(i).Item("arso_so_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For j = 0 To ds_edit_piutang.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.sokp_piutang " _
                                                    & "( " _
                                                    & "  sokp_oid, " _
                                                    & "  sokp_so_oid, " _
                                                    & "  sokp_seq, " _
                                                    & "  sokp_amount, " _
                                                    & "  sokp_due_date, " _
                                                    & "  sokp_amount_pay, " _
                                                    & "  sokp_description " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetSetring(ds_edit_so.Tables(0).Rows(i).Item("arso_so_oid").ToString) & ",  " _
                                                    & SetInteger(ds_edit_piutang.Tables(0).Rows(j).Item("sokp_seq").ToString) & ",  " _
                                                    & SetDbl(ds_edit_piutang.Tables(0).Rows(j).Item("sokp_amount").ToString) & ",  " _
                                                    & SetDate(ds_edit_piutang.Tables(0).Rows(j).Item("sokp_due_date").ToString) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetSetring("-") & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        Next

                        'Untuk Insert Data List shipment
                        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ars_ship " _
                                                    & "( " _
                                                    & "  ars_oid, " _
                                                    & "  ars_ar_oid, " _
                                                    & "  ars_seq, " _
                                                    & "  ars_soshipd_oid, " _
                                                    & "  ars_taxable, " _
                                                    & "  ars_tax_class_id, " _
                                                    & "  ars_tax_inc, " _
                                                    & "  ars_open, " _
                                                    & "  ars_invoice, " _
                                                    & "  ars_so_price, " _
                                                    & "  ars_so_disc_value, " _
                                                    & "  ars_invoice_price, " _
                                                    & "  ars_close_line, " _
                                                    & "  ars_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("ars_oid").ToString) & ",  " _
                                                    & SetSetring(_ar_oid.ToString) & ",  " _
                                                    & SetInteger(i) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("ars_soshipd_oid").ToString) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")) & ",  " _
                                                    & SetInteger(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc")) & ",  " _
                                                    & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_open")) & ",  " _
                                                    & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")) & ",  " _
                                                    & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_price")) & ",  " _
                                                    & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")) & ",  " _
                                                    & SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) & ",  " _
                                                    & SetSetring(ds_edit_shipment.Tables(0).Rows(i).Item("ars_close_line")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update Table shipment Detail untuk kolom shipd_qty_inv dan shipd_close_line nya
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update soshipd_det set soshipd_qty_inv = coalesce(soshipd_qty_inv,0) + " + SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice").ToString) + _
                                                       ", soshipd_close_line = '" + ds_edit_shipment.Tables(0).Rows(i).Item("ars_close_line") + "'" + _
                                                       " where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("ars_soshipd_oid") + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                                    'Update Table so Detail untuk kolom sod_qty_invoice
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update sod_det set sod_qty_invoice = coalesce(sod_qty_invoice,0) + " + SetDbl(ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice").ToString) + _
                                                           " where sod_oid = (select soshipd_sod_oid from soshipd_det where soshipd_oid = '" + ds_edit_shipment.Tables(0).Rows(i).Item("ars_soshipd_oid") + "')"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            End If
                        Next

                        'sqlTran.Rollback()
                        'Return False

                        'Untuk Insert Distribution Line nya
                        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ard_dist " _
                                                & "( " _
                                                & "  ard_oid, " _
                                                & "  ard_ar_oid, " _
                                                & "  ard_tax_distribution, " _
                                                & "  ard_taxable, " _
                                                & "  ard_tax_class_id, " _
                                                & "  ard_ac_id, " _
                                                & "  ard_sb_id, " _
                                                & "  ard_cc_id, " _
                                                & "  ard_amount, " _
                                                & "  ard_remarks, " _
                                                & "  ard_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("ard_oid").ToString) & ",  " _
                                                & SetSetring(_ar_oid.ToString) & ",  " _
                                                & SetSetringDB(ds_edit_dist.Tables(0).Rows(i).Item("ard_tax_distribution")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("ard_taxable")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("ard_tax_class_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("ard_sb_id")) & ",  " _
                                                & SetIntegerDB(ds_edit_dist.Tables(0).Rows(i).Item("ard_cc_id")) & ",  " _
                                                & SetDbl(ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")) & ",  " _
                                                & SetSetring(ds_edit_dist.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        ''Update Table shipment Detail untuk kolom shipd_qty_inv dan shipd_close_line nya
                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "update ptnr_mstr set ptnr_prepaid_balance = coalesce(ptnr_prepaid_balance,0) + " + ar_pay_prepaid.EditValue.ToString + _
                        '                       " where ptnr_id = " + ar_bill_to.EditValue.ToString
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        'Insert Jurnal
                        If _create_jurnal = True Then
                            If insert_glt_det_ar(ssqls, objinsert, ds_edit_dist, _
                                                       ar_en_id.EditValue, ar_en_id.GetColumnValue("en_code"), _
                                                       _ar_oid.ToString, _ar_code, _
                                                       ar_eff_date.DateTime, _
                                                       ar_cu_id.EditValue, ar_exc_rate.EditValue, _
                                                       "AR", "AR-INV", _
                                                       ar_ac_id.EditValue, ar_sb_id.EditValue, ar_cc_id.EditValue, _
                                                       ar_ac_id.GetColumnValue("ac_name"), _
                                                       _ar_amount) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                        '************************************************

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, ar_en_id.EditValue, 13, _ar_oid.ToString, _ar_code, ar_eff_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Debit Credit Memo SDI " & _ar_code)
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
                        set_row(_ar_oid.ToString, "ar_oid")
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

    Private Function insert_glt_det_ar(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        insert_glt_det_ar = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            If par_ds.Tables(0).Rows(i).Item("ard_amount") > 0 Then
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
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount")) & ",  " _
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
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount"), "C") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf par_ds.Tables(0).Rows(i).Item("ard_amount") < 0 Then
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
                                            & SetInteger(i) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount") * -1) & ",  " _
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
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount") * -1, "D") = False Then

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
                                    & SetInteger(i) & ",  " _
                                    & SetInteger(par_ac_id) & ",  " _
                                    & SetIntegerDB(par_sb_id) & ",  " _
                                    & SetIntegerDB(par_cc_id) & ",  " _
                                    & SetSetringDB(par_desc) & ",  " _
                                    & SetDblDB(par_amount) & ",  " _
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
                                                 par_ac_id, _
                                                 par_sb_id, _
                                                 par_cc_id, _
                                                 par_en_id, par_cu_id, _
                                                 par_exc_rate, par_amount, "D") = False Then

                    Return False
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_en_id")
        _type = 13
        _table = "ar_mstr"
        _initial = "ar"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "select  " _
            & "ar_oid, " _
            & "ar_code, " _
            & "ar_bill_to, " _
            & "ptnr_name, " _
            & "ptnra_line_1, " _
            & "ptnra_line_2, " _
            & "ptnra_line_3, " _
            & "ptnra_zip, " _
            & "ar_cu_id, " _
            & "cu_name, " _
            & "ar_date, " _
            & "ar_eff_date, " _
            & "ar_amount, " _
            & "ar_pay_amount, " _
            & "ar_due_date, " _
            & "ar_expt_date, " _
            & "ar_exc_rate, " _
            & "ar_remarks, " _
            & "ar_status, " _
            & "ar_type, " _
            & "ar_credit_term, " _
            & "credit_term_mstr.code_name as credit_term_name, " _
            & "cu_symbol, " _
            & "so_code, " _
            & "um_master.code_name as um_name , " _
            & "pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "sod_disc,sod_tax_inc,  " _
            & "tax_class_mstr.code_name tax_class_name, " _
            & "tax_type_mstr.code_name tax_type_name,  " _
            & "taxr_rate,  " _
            & "ars_invoice,  " _
            & "ars_invoice_price, " _
            & "ars_invoice_price + (sod_price * sod_disc) as ars_invoice_price2, " _
            & "(ars_invoice * (ars_invoice_price * taxr_rate / 100)) as ars_invoice_price_ppn, " _
            & "cmaddr_code, " _
            & "cmaddr_name, " _
            & "cmaddr_line_1, " _
            & "cmaddr_line_2, " _
            & "cmaddr_line_3, " _
            & "cmaddr_phone_1, " _
            & "cmaddr_phone_2, " _
            & "bk_name, " _
            & "ac_name, " _
            & "ar_terbilang, " _
            & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "from ars_ship " _
            & "inner join soshipd_det on soshipd_oid = ars_soshipd_oid  " _
            & "inner join ar_mstr on ar_oid = ars_ar_oid " _
            & "inner join sod_det on sod_oid = soshipd_sod_oid " _
            & "inner join so_mstr on so_oid = sod_so_oid " _
            & "inner join pt_mstr on pt_id = sod_pt_id " _
            & "inner join code_mstr tax_class_mstr on tax_class_mstr.code_id = ars_tax_class_id " _
            & "inner join taxr_mstr on taxr_mstr.taxr_tax_class = ars_tax_class_id " _
            & "inner join code_mstr tax_type_mstr on tax_type_mstr.code_id = taxr_mstr.taxr_tax_type " _
            & "inner join ptnr_mstr on ptnr_id = ar_bill_to " _
            & "inner join ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            & "inner join cu_mstr on cu_id = ar_cu_id " _
            & "inner join code_mstr um_master on um_master.code_id = sod_um " _
            & "inner join code_mstr credit_term_mstr on credit_term_mstr.code_id = ar_credit_term " _
            & "inner join cmaddr_mstr on cmaddr_en_id = ar_en_id " _
            & "inner join bk_mstr on bk_id = ar_bk_id " _
            & "inner join ac_mstr on ac_id = bk_ac_id " _
            & "left outer join tranaprvd_dok on tranaprvd_tran_oid = ar_oid " _
            & "where tax_type_mstr.code_name = 'PPN'" _
            & "and ar_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInvoice"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_code")
        frm.ShowDialog()


    End Sub

    Private Sub sb_retrieve_piutang_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve_piutang.Click
        If ds_edit_so.Tables(0).Rows.Count > 0 Then
            retrieve_piutang()
        Else
            MsgBox("SO not available...", MsgBoxStyle.Critical, "Unable to Create Kartu Piutang")
        End If
    End Sub

    Private Sub retrieve_piutang()
        'cari barang yang sudah dikirim untuk dibuatkan kartu piutang
        Dim x, y, z As Integer
        Dim _total_dp_all, _total_dp_item, _total_payment_all, _total_payment_item As Double
        Dim _tot_qty As Integer = 0
        Dim _dtrow As DataRow

        _total_dp_all = 0
        _total_payment_all = 0
        ds_edit_piutang.Tables(0).Clear()

        For x = 0 To ds_edit_so.Tables(0).Rows.Count - 1
            ds_sod_piutang = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  public.so_mstr.so_oid, " _
                            & "  public.so_mstr.so_code, " _
                            & "  public.so_mstr.so_payment_date, " _
                            & "  public.so_mstr.so_pay_type, " _
                            & "  public.sod_det.sod_qty, " _
                            & "  public.sod_det.sod_oid, " _
                            & "  public.sod_det.sod_so_oid, " _
                            & "  public.sod_det.sod_seq, " _
                            & "  public.sod_det.sod_pt_id, " _
                            & "  public.sod_det.sod_qty_allocated, " _
                            & "  public.sod_det.sod_qty_picked, " _
                            & "  public.sod_det.sod_qty_shipment, " _
                            & "  public.sod_det.sod_qty_pending_inv, " _
                            & "  public.sod_det.sod_qty_invoice, " _
                            & "  public.sod_det.sod_cost, " _
                            & "  public.sod_det.sod_price, " _
                            & "  public.sod_det.sod_disc, " _
                            & "  public.sod_det.sod_qty_real, " _
                            & "  public.sod_det.sod_status, " _
                            & "  public.sod_det.sod_dt, " _
                            & "  public.sod_det.sod_payment, " _
                            & "  public.sod_det.sod_dp, " _
                            & "  public.sod_det.sod_sales_unit, " _
                            & "  public.sod_det.sod_serial, " _
                            & "  public.sod_det.sod_qty_return, " _
                            & "  public.sod_det.sod_qty_ir " _
                            & "FROM " _
                            & "  public.sod_det " _
                            & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid)" _
                            & "  where so_code = " & SetSetring(ds_edit_so.Tables(0).Rows(x).Item("arso_so_code").ToString)
                        .InitializeCommand()
                        .FillDataSet(ds_sod_piutang, "list_sod")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            For y = 0 To ds_sod_piutang.Tables(0).Rows.Count - 1
                _total_dp_item = 0
                _total_payment_item = 0
                For z = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
                    If ds_sod_piutang.Tables(0).Rows(y).Item("sod_pt_id").ToString = ds_edit_shipment.Tables(0).Rows(z).Item("pt_id") Then
                        If ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice") < 0 Then
                            _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp") * -1.0)
                            _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment") * -1.0) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                        Else
                            _total_dp_item = _total_dp_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_dp").ToString) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)
                            _total_payment_item = _total_payment_item + (ds_sod_piutang.Tables(0).Rows(y).Item("sod_payment")) '* ds_edit_shipment.Tables(0).Rows(z).Item("ars_invoice").ToString)

                        End If
                    End If
                Next
                _total_dp_all = _total_dp_all + _total_dp_item
                _total_payment_all = _total_payment_all + _total_payment_item
            Next

            Dim i, _interval As Integer

            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select code_usr_1 From code_mstr " + _
                                               " where code_field = 'payment_type' " + _
                                               " and code_id = " + ds_sod_piutang.Tables(0).Rows(0).Item("so_pay_type").ToString
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _interval = .DataReader("code_usr_1").ToString
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            Dim _date As Date = ds_sod_piutang.Tables(0).Rows(0).Item("so_payment_date").ToString

            _dtrow = ds_edit_piutang.Tables(0).NewRow
            _dtrow("sokp_oid") = Guid.NewGuid.ToString

            _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
            _dtrow("sokp_seq") = 0

            _dtrow("sokp_amount") = _total_dp_all
            _dtrow("sokp_due_date") = _date
            _dtrow("sokp_amount_pay") = 0
            _dtrow("sokp_description") = "-"

            ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
            ds_edit_piutang.Tables(0).AcceptChanges()


            _total_piutang = _total_dp_all
            For i = 0 To _interval - 1
                _date = _date.AddMonths(1)

                _dtrow = ds_edit_piutang.Tables(0).NewRow
                _dtrow("sokp_oid") = Guid.NewGuid.ToString

                _dtrow("sokp_so_oid") = ds_sod_piutang.Tables(0).Rows(0).Item("so_oid").ToString
                _dtrow("sokp_seq") = i + 1

                _dtrow("sokp_amount") = _total_payment_all
                _total_piutang += _total_payment_all

                _dtrow("sokp_due_date") = _date
                _dtrow("sokp_amount_pay") = 0
                _dtrow("sokp_description") = "-"

                ds_edit_piutang.Tables(0).Rows.Add(_dtrow)
                ds_edit_piutang.Tables(0).AcceptChanges()

            Next
            gv_edit_piutang.BestFitColumns()
        Next
    End Sub

    Private Sub par_so_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_so.ButtonClick
        Try

            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._obj = par_so
            frm._ptnr_id = _par_cus_id
            frm.type_form = True
            frm.ShowDialog()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_cus_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_cus.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = par_cus
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub UpdateTerbilangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTerbilangToolStripMenuItem.Click
        Try
            Dim ssql As String
            Dim _terbilang As String

            _terbilang = func_bill.TERBILANG_FIX(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_amount"))
            ssql = "update ar_mstr set ar_terbilang=" & SetSetring(_terbilang) & " where ar_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_oid").ToString & "'"

            Dim ssqls As New ArrayList
            ssqls.Add(ssql)

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
            Box("Update success, please refresh data")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    'Private Sub ar_so_ref_code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_so_ref_code.EditValueChanged
    'Private Sub so_sq_ref_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_sq_ref_code.ButtonClick
    Private Sub ar_so_ref_code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ar_so_ref_code.EditValueChanged
        Dim frm As New FSalesOrderSearch
        frm.set_win(Me)
        frm._en_id = ar_en_id.EditValue
        frm._ptnr_id = ar_bill_to.EditValue
        frm._objk = ar_so_ref_code
        'frm._date = ar_date.DateTime
        frm.type_form = True
        'frm._tran_oid = _so_sq_ref_oid
        frm.ShowDialog()
    End Sub
    'End Sub
End Class
