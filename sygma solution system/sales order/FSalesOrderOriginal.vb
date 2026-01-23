Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FSalesOrderOriginal
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit, ds_edit_shipment, ds_edit_dist As New DataSet
    Dim _now As DateTime
    Dim _so_oid_mstr, _soa_oid_mstr, _so_code_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Public _so_ptnr_id_sold_mstr As Integer
    Public _so_ref_po_oid As String
    Dim _conf_value As String
    Dim _conf_so_price As String
    Dim _conf_limit_partner_group_ws As String
    Dim mf As New master_new.ModFunction
    Dim _id_ptnr_so As Integer

    'Dim _term As Double = 0
    'Dim _credit_limit As Double = 0

    Private Sub FSalesOrderOriginal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_order")
        _conf_so_price = func_coll.get_conf_file("so_price_from_calculate")
        _conf_limit_partner_group_ws = func_coll.get_conf_file("limit_partner_group_ws")

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _so_ptnr_id_sold_mstr = -1 'set diawal agar tau kalau -1 artinya datanya belum ada...

        If _conf_value = "0" Then
            xtc_detail.TabPages(3).PageVisible = False
            xtc_detail.TabPages(5).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(3).PageVisible = True
            xtc_detail.TabPages(5).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub


    '    Private Sub GridView1_RowStyle(ByVal sender As Object, _
    'ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gv_master.RowStyle

    '        Dim View As GridView = sender
    '        If (e.RowHandle >= 0) Then
    '            Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("so_trans_id"))
    '            If category = "X" Then

    '                e.Appearance.BackColor = Color.Salmon
    '                e.Appearance.BackColor2 = Color.SeaShell
    '            End If
    '        End If
    '    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'so_en_id.Properties.DataSource = dt_bantu
        'so_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'so_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'so_en_id.ItemIndex = 0

        init_le(so_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_so_type())
        so_type.Properties.DataSource = dt_bantu
        so_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        so_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        so_type.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            so_tran_id.Properties.DataSource = dt_bantu
            so_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            so_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            so_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            so_tran_id.Properties.DataSource = dt_bantu
            so_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            so_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            so_tran_id.ItemIndex = 0
        End If


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        so_cu_id.Properties.DataSource = dt_bantu
        so_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        so_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        so_cu_id.ItemIndex = 0

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(so_en_id.EditValue)

        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            _filter += " and enacc_code='so_cash')"
        Else
            _filter += " and enacc_code='so_credit')"
        End If


        dt_bantu = New DataTable
        If limit_account(so_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If

        so_ar_ac_id.Properties.DataSource = dt_bantu
        so_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        so_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        so_ar_ac_id.ItemIndex = 0


        init_le(le_en_id, "en_mstr")

        'sys 20110613
        dt_bantu = New DataTable
        dt_bantu = func_data.load_ppn_type()

        With so_ppn_type
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("display").ToString
            .Properties.ValueMember = dt_bantu.Columns("value").ToString
            .ItemIndex = 0
        End With
        'end 20110613

        dt_bantu = New DataTable

        dt_bantu = func_data.load_sales_program()

        With so_sales_program
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("sls_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("sls_code").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Function load_sales(ByVal par_en_id As String) As DataTable
        Dim ds_bantu As New DataSet
        Dim dt_bantu As New DataTable
        Try


            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select ptnr_id, ptnr_name,en_desc, ptnr_ac_ap_id from ptnr_mstr inner join en_mstr on ptnr_en_id=en_id where ptnr_active ~~* 'Y'" + _
                           " and ptnr_dom_id = " + master_new.ClsVar.sdom_id.ToString + _
                           " and ptnr_is_member ~~* 'Y' " + _
                           " order by ptnr_name "
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "ptnr_mstr")
                    dt_bantu = ds_bantu.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return dt_bantu
    End Function

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", so_en_id.EditValue))
        ptnrg_id.Properties.DataSource = dt_bantu
        ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        If SetString(func_coll.get_conf_file("penjualan_lintas_entitas")) = "1" Then
            dt_bantu = (load_sales(so_en_id.EditValue))
        Else
            dt_bantu = (func_data.load_sales(so_en_id.EditValue))
        End If

        so_sales_person.Properties.DataSource = dt_bantu
        so_sales_person.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        so_sales_person.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        so_sales_person.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_creditterms_mstr(so_en_id.EditValue))
        so_credit_term.Properties.DataSource = dt_bantu
        so_credit_term.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        so_credit_term.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        so_credit_term.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_taxclass_mstr(so_en_id.EditValue))
        so_tax_class.Properties.DataSource = dt_bantu
        so_tax_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        so_tax_class.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        so_tax_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(so_en_id.EditValue))
        so_si_id.Properties.DataSource = dt_bantu
        so_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        so_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        so_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pay_type(so_en_id.EditValue))
        so_pay_type.Properties.DataSource = dt_bantu
        so_pay_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        so_pay_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        so_pay_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(so_en_id.EditValue, "payment_methode"))
        so_pay_method.Properties.DataSource = dt_bantu
        so_pay_method.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        so_pay_method.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        so_pay_method.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(so_en_id.EditValue))
        so_ar_sb_id.Properties.DataSource = dt_bantu
        so_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        so_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        so_ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(so_en_id.EditValue))
        so_ar_cc_id.Properties.DataSource = dt_bantu
        so_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        so_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        so_ar_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(so_en_id.EditValue))
        so_bk_id.Properties.DataSource = dt_bantu
        so_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        so_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        so_bk_id.ItemIndex = 0

        dt_bantu = New DataTable

        If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime, SetNumber(ptnrg_id.EditValue)))
        Else
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
        End If

        Try
            so_pi_id.Properties.DataSource = dt_bantu
            so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            so_pi_id.ItemIndex = 0

        Catch ex As Exception

        End Try

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(so_en_id.EditValue)

        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            _filter += " and enacc_code='so_cash')"
        Else
            _filter += " and enacc_code='so_credit')"
        End If


        dt_bantu = New DataTable
        If limit_account(so_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If

        so_ar_ac_id.Properties.DataSource = dt_bantu
        so_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        so_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        so_ar_ac_id.ItemIndex = 0
    End Sub

    Private Sub so_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub so_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_type.EditValueChanged
        dt_bantu = New DataTable
        If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime, ptnrg_id.EditValue))
        Else
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
        End If

        Try
            so_pi_id.Properties.DataSource = dt_bantu
            so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            so_pi_id.ItemIndex = 0
        Catch ex As Exception

        End Try


        If so_type.EditValue.ToString.ToUpper = "D" Then
            gv_edit.Columns("sod_disc").OptionsColumn.AllowEdit = False
        Else
            gv_edit.Columns("sod_disc").OptionsColumn.AllowEdit = True
        End If

    End Sub

    Private Sub so_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_cu_id.EditValueChanged
        Dim _so_exc_rate As Double
        If so_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _so_exc_rate = func_data.get_exchange_rate(so_cu_id.EditValue, so_date.DateTime)
            If _so_exc_rate = 1 Then
                so_exc_rate.EditValue = 0
            Else
                so_exc_rate.EditValue = _so_exc_rate
            End If

        Else
            so_exc_rate.EditValue = 1
        End If

        dt_bantu = New DataTable
        If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime, ptnrg_id.EditValue))
        Else
            dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
        End If

        Try
            so_pi_id.Properties.DataSource = dt_bantu
            so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            so_pi_id.ItemIndex = 0
        Catch ex As Exception

        End Try


        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(so_en_id.EditValue, so_cu_id.EditValue))
        so_bk_id.Properties.DataSource = dt_bantu
        so_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_code").ToString
        so_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        so_bk_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "so_oid", False)
        add_column_edit(gv_master, "Select", "pilih", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Parent", "so_parent_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SO Type", "so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Indent", "so_manufacture", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Program", "sls_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Entity Customer", "en_desc_cust", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID Customer", "so_ptnr_id_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cust ID AJB", "ptnr_customer_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shipping Address", "so_shipping_address", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PS Status", "ps_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO No.", "so_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Take Over", "so_ref_po_code_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SQ No.", "so_sq_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Req No.", "so_psn_ref_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "so_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "so_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Type", "so_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Payment", "so_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sales Unit", "so_sales_unit", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Customer Address ID", "ptnra_customer_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receiver Name", "ptnra_receiver_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receiver Address", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Is Package", "so_is_package", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Price", "so_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_master, "Entity Shipment", "en_desc_shipment", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Indent Status", "so_indent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project Status", "so_project", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Packing Status", "so_status_packing", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Packing By", "so_packing_by", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "sod_so_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Additional Charge", "sod_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Shipment", "sod_qty_shipment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Child", "sod_qty_child", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "sod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
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
        add_column_copy(gv_detail, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_detail, "Commision", "sod_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Commision Total", "sod_commision_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        'add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Serial", "sod_serial", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "PPN", "sod_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "PPH", "sod_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_detail, "Status WO", "sod_wo_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_attr, "soa_so_oid", False)
        add_column_copy(gv_detail_attr, "KJB Number", "soa_kjb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Tempat Bekerja", "soa_bekerja_pada", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jabatan", "soa_jabatan_bagian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat1", "soa_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat2", "soa_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Lantai Bekerja", "soa_kantor_lantai", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Telephone", "soa_kantor_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "KTP", "soa_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Email", "soa_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat1", "soa_rumah_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat2", "soa_rumah_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Kode Pos", "soa_rumah_kode_pos", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Telephone", "soa_rumah_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Telephone HP", "soa_rumah_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Kirim", "soa_status_alamat_kirim", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Tagih", "soa_status_alamat_tagih", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Nama", "soa_suami_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Tempat Bekerja", "soa_suami_bekerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Jabatan", "soa_suami_jabatan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat1", "soa_suami_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat2", "soa_suami_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Telephone", "soa_suami_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri HP", "soa_suami_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Nama", "soa_anak_nama_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Tgl Lahir", "soa_anak_tgl_lahir_1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak Pertama Sekolah", "soa_anak_sekolah_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Nama", "soa_anak_nama_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Tgl Lahir", "soa_anak_tgl_lahir_2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak kedua Sekolah", "soa_anak_sekolah_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Nama", "soa_anak_nama_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Tgl Lahir", "soa_anak_tgl_lahir_3", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak ketiga Sekolah", "soa_anak_sekolah_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Nama", "soa_keluarga_dekat_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat1", "soa_keluarga_dekat_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat2", "soa_keluarga_dekat_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Telephone", "soa_keluarga_dekat_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat HP", "soa_keluarga_dekat_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Tempat Tinggal", "soa_status_tempat_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jenis Kartu Kredit", "soa_jenis_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "No Kartu Kredit", "soa_no_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Berlaku s/d", "soa_berlaku_sd", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Bank", "soa_bank", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_kp, "sokp_so_oid", False)
        add_column_copy(gv_detail_kp, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Amount Pay", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Date Payment", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Description", "sokp_description", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "sod_oid", False)
        add_column(gv_edit, "sod_so_oid", False)
        add_column(gv_edit, "sod_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Additional Charge", "sod_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Remarks", "sod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_qty_shipment", False)
        add_column(gv_edit, "sod_um", False)
        add_column(gv_edit, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_cost", False)

        If func_coll.get_conf_file("editable_so_price") = "0" Then
            add_column(gv_edit, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        Else
            add_column_edit(gv_edit, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        End If

        add_column_edit(gv_edit, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "Price Net", "sod_price_net", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Ext Total Before Tax", "sod_price_ori_aft_disc_before_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Ext Total After Tax", "sod_price_ori_aft_disc_aft_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_sales_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_sales_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_sales_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_disc_ac_id", False)
        add_column(gv_edit, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "sod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "sod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_tax_class", False)
        add_column(gv_edit, "Tax Class", "sod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "PPN Type", "sod_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Commision", "sod_commision", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Commision Total", "sod_commision_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "sod_pod_oid", False)
        add_column(gv_edit_assembly, "sodas_pt_id_sod", False)
        add_column(gv_edit_assembly, "sodas_pt_id", False)
        add_column(gv_edit_assembly, "sodas_sod_oid", False)

        add_column(gv_edit_assembly, "Partnumber Parent", "pt_code_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "PT Desc1 Parent", "pt_desc1_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "PT Desc2 Parent", "pt_desc2_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "Partnumber", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "PT Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "PT Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_assembly, "Qty", "sodas_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_assembly, "Qty Sold", "sodas_qty_sold", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_assembly, "Partnumber Parent", "pt_code_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "PT Desc1 Parent", "pt_desc1_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "PT Desc2 Parent", "pt_desc2_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "Partnumber", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "PT Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "PT Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_assembly, "Qty", "sodas_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail_assembly, "Qty Sold", "sodas_qty_sold", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "so_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "sod_so_oid", False)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "sod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "UM Conversion", "sod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty Real", "sod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Class", "sod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & " false as pilih, so_oid,so_status_packing,so_packing_by, " _
                    & "  so_dom_id,so_ref_po_code_2,so_parent_oid,so_parent_code, " _
                    & "  so_en_id,coalesce(so_project,'N') as so_project,so_sq_ref_oid,so_sq_ref_code, so_psn_ref_code," _
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
                    & "  so_sales_person, " _
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
                    & "  so_tax_inc, so_price, " _
                    & "  (so_total + so_total_ppn + so_total_pph) as so_total_after_tax, " _
                    & "  so_exc_rate * so_total as so_total_ext,  so_exc_rate * so_total_ppn as so_total_ppn_ext, " _
                    & "  so_exc_rate * so_total_pph as so_total_pph_ext,  so_exc_rate * (so_total + so_total_ppn + so_total_pph) as so_total_after_tax_ext, " _
                    & "  en_mstr.en_desc, en_mstr_cust.en_desc as en_desc_cust, en_mstr_shipment.en_desc as en_desc_shipment, " _
                    & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                    & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                    & "  credit_term.code_name as credit_term_name, " _
                    & "  tax_class.code_name as tax_class_name, " _
                    & "  si_desc, " _
                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                    & "  pi_desc, " _
                    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  ac_mstr_ar.ac_code, " _
                    & "  ac_mstr_ar.ac_name, " _
                    & "  sb_mstr_ar.sb_desc, " _
                    & "  cc_mstr_ar.cc_desc,so_sales_program,sls_name, " _
                    & "  cu_name, so_indent,so_shipping_address,ptnr_mstr_sold.ptnr_customer_id as ptnr_customer_id, " _
                    & "  tran_name,coalesce(so_manufacture,'N') as so_manufacture, " _
                    & "  case so_ppn_type " _
                    & "  when 'E' then 'PPN Bebas' " _
                    & "  when 'A' then 'PPN Bayar' " _
                    & "  end as so_ppn_type, case when ptnr_mstr_sold.ptnr_is_ps='Y' then 'PS' else 'NON PS' end as ps_status,ptnrg_name, ptnra_receiver_name,ptnra_customer_id, " _
                    & "  so_bk_id, bk_code, bk_name,coalesce(so_is_package,'N') as so_is_package,pt_code,pt_desc1,pt_desc2,so_pt_id, " _
                    & "   ptnra_line_1,  ptnra_line_2,  ptnra_line_3 " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_mstr.en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = so_ptnr_id_bill " _
                    & "  inner join code_mstr credit_term on credit_term.code_id = so_credit_term " _
                    & "  inner join code_mstr tax_class on tax_class.code_id = so_tax_class " _
                    & "  inner join si_mstr on si_id = so_si_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
                    & "  inner join pi_mstr on pi_id = so_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "  inner join code_mstr pay_method on pay_method.code_id = so_pay_method " _
                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = so_ar_ac_id " _
                    & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
                    & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id " _
                    & "  left outer join bk_mstr on bk_mstr.bk_id = so_bk_id " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  left outer join tran_mstr on tran_id = so_tran_id" _
                    & "  left outer join pt_mstr on so_pt_id = pt_id " _
                    & "  left outer join ptnrg_grp on ptnrg_grp.ptnrg_id = ptnr_mstr_sold.ptnr_ptnrg_id " _
                    & "  left outer join sls_program on so_sales_program = sls_code " _
                    & "  left outer join en_mstr as en_mstr_cust on en_mstr_cust.en_id = ptnr_mstr_sold.ptnr_en_id " _
                    & "  left outer join en_mstr en_mstr_shipment on en_mstr_shipment.en_id  = so_en_id_shipment  " _
                     & "  left outer join ptnra_addr on  ( so_ptnra_id=ptnra_id  and ptnra_ptnr_oid=ptnr_mstr_sold.ptnr_oid ) " _
                    & " where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        '                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _

        If ce_en_id.EditValue = True Then
            get_sequel += " and so_en_id =" & le_en_id.EditValue
        Else
            get_sequel += " and so_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If
        If ce_cus.EditValue = True Then
            get_sequel += " and so_ptnr_id_sold =" & le_cus_id.EditValue
        End If
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        'load_data_to_detail()

        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  sod_oid, " _
        '    & "  sod_dom_id, " _
        '    & "  sod_en_id, " _
        '    & "  sod_add_by, " _
        '    & "  sod_add_date, " _
        '    & "  sod_upd_by, " _
        '    & "  sod_upd_date, " _
        '    & "  sod_so_oid, " _
        '    & "  sod_seq, " _
        '    & "  sod_is_additional_charge, " _
        '    & "  sod_si_id, " _
        '    & "  sod_pt_id, " _
        '    & "  sod_rmks, " _
        '    & "  sod_qty, " _
        '    & "  sod_qty_allocated, " _
        '    & "  sod_qty_picked, " _
        '    & "  sod_qty_shipment, " _
        '    & "  sod_qty_pending_inv, " _
        '    & "  sod_qty_invoice, " _
        '    & "  sod_um, " _
        '    & "  sod_cost, " _
        '    & "  sod_price, " _
        '    & "  sod_disc, " _
        '    & "  sod_sales_ac_id, " _
        '    & "  sod_sales_sb_id, " _
        '    & "  sod_sales_cc_id, " _
        '    & "  sod_disc_ac_id, " _
        '    & "  sod_um_conv, " _
        '    & "  sod_qty_real, " _
        '    & "  sod_taxable, " _
        '    & "  sod_tax_inc, " _
        '    & "  sod_tax_class, " _
        '    & "  sod_status, " _
        '    & "  sod_dt, " _
        '    & "  sod_payment, " _
        '    & "  sod_dp, " _
        '    & "  sod_sales_unit, " _
        '    & "  sod_loc_id, " _
        '    & "  sod_serial, " _
        '    & "  en_desc, " _
        '    & "  si_desc, " _
        '    & "  pt_code, " _
        '    & "  pt_desc1, " _
        '    & "  pt_desc2, " _
        '    & "  um_mstr.code_name as um_name, " _
        '    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
        '    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
        '    & "  sb_desc, " _
        '    & "  cc_desc, " _
        '    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
        '    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
        '    & "  tax_class.code_name as sod_tax_class_name, " _
        '    & "  sod_ppn_type, " _
        '    & "  loc_desc " _
        '    & "FROM  " _
        '    & "  public.sod_det " _
        '    & "  inner join so_mstr on so_oid = sod_so_oid " _
        '    & "  inner join en_mstr on en_id = sod_en_id " _
        '    & "  inner join si_mstr on si_id = sod_si_id " _
        '    & "  inner join pt_mstr on pt_id = sod_pt_id " _
        '    & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
        '    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
        '    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
        '    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
        '    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
        '    & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class	 " _
        '    & "  left outer join loc_mstr on loc_id = sod_loc_id" _
        '    & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '    & " and so_en_id in (select user_en_id from tconfuserentity " _
        '                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        'load_data_detail(sql, gc_detail, "detail")

        'Try
        '    ds.Tables("detail_attr").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  soa_oid, " _
        '    & "  soa_kjb_code, " _
        '    & "  soa_bekerja_pada, " _
        '    & "  soa_jabatan_bagian, " _
        '    & "  soa_kantor_alamat_1, " _
        '    & "  soa_kantor_alamat_2, " _
        '    & "  soa_kantor_lantai, " _
        '    & "  soa_kantor_telp, " _
        '    & "  soa_ktp, " _
        '    & "  soa_email, " _
        '    & "  soa_rumah_alamat_1, " _
        '    & "  soa_rumah_alamat_2, " _
        '    & "  soa_rumah_kode_pos, " _
        '    & "  soa_rumah_telp, " _
        '    & "  soa_rumah_hp, " _
        '    & "  soa_status_alamat_kirim, " _
        '    & "  soa_status_alamat_tagih, " _
        '    & "  soa_suami_nama, " _
        '    & "  soa_suami_bekerja, " _
        '    & "  soa_suami_jabatan, " _
        '    & "  soa_suami_kantor_alamat_1, " _
        '    & "  soa_suami_kantor_alamat_2, " _
        '    & "  soa_suami_telp, " _
        '    & "  soa_suami_hp, " _
        '    & "  soa_anak_nama_1, " _
        '    & "  soa_anak_tgl_lahir_1, " _
        '    & "  soa_anak_sekolah_1, " _
        '    & "  soa_anak_nama_2, " _
        '    & "  soa_anak_tgl_lahir_2, " _
        '    & "  soa_anak_sekolah_2, " _
        '    & "  soa_anak_nama_3, " _
        '    & "  soa_anak_tgl_lahir_3, " _
        '    & "  soa_anak_sekolah_3, " _
        '    & "  soa_keluarga_dekat_nama, " _
        '    & "  soa_keluarga_dekat_alamat_1, " _
        '    & "  soa_keluarga_dekat_alamat_2, " _
        '    & "  soa_keluarga_dekat_telp, " _
        '    & "  soa_keluarga_dekat_hp, " _
        '    & "  soa_status_tempat_tinggal, " _
        '    & "  soa_jenis_kartu_kredit, " _
        '    & "  soa_no_kartu_kredit, " _
        '    & "  soa_berlaku_sd, " _
        '    & "  soa_dt, " _
        '    & "  soa_bank " _
        '    & "FROM  " _
        '    & "  public.soa_attr" _
        '    & "  inner join so_mstr on so_oid = soa_so_oid " _
        '    & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '    & " and so_en_id in (select user_en_id from tconfuserentity " _
        '                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        'load_data_detail(sql, gc_detail_attr, "detail_attr")

        'Try
        '    ds.Tables("detail_kp").Clear()
        'Catch ex As Exception
        'End Try

        ''sql = "SELECT  " _
        ''    & "  sokp_oid, " _
        ''    & "  sokp_so_oid, " _
        ''    & "  sokp_seq, " _
        ''    & "  sokp_ref, " _
        ''    & "  sokp_amount, " _
        ''    & "  sokp_due_date, " _
        ''    & "  sokp_amount_pay, " _
        ''    & "  sokp_description " _
        ''    & "FROM  " _
        ''    & "  public.sokp_piutang " _
        ''    & "  inner join public.so_mstr on so_oid = sokp_so_oid " _
        ''    & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        ''    & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        ''    & " and so_en_id in (select user_en_id from tconfuserentity " _
        ''                         & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
        ''    & " order by sokp_seq"

        ''load_data_detail(sql, gc_detail_kp, "detail_kp")

        'If _conf_value = "1" Then
        '    Try
        '        ds.Tables("wf").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
        '          " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
        '          " wf_iscurrent, wf_seq " + _
        '          " from wf_mstr w " + _
        '          " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
        '          " inner join so_mstr on so_code = wf_ref_code " + _
        '          " inner join sod_det dt on dt.sod_so_oid = so_oid " _
        '        & " where so_date >= " + SetDate(pr_txttglawal.DateTime) _
        '        & " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
        '        & " and so_en_id in (select user_en_id from tconfuserentity " _
        '                              & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
        '        & " order by wf_ref_code, wf_seq "
        '    load_data_detail(sql, gc_wf, "wf")
        '    gv_wf.BestFitColumns()

        '    Try
        '        ds.Tables("email").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "SELECT  " _
        '               & "  so_oid, " _
        '                & "  so_en_id, " _
        '                & "  so_add_by, " _
        '                & "  so_add_date, " _
        '                & "  so_upd_by, " _
        '                & "  so_upd_date, " _
        '                & "  so_code, " _
        '                & "  so_date, " _
        '                & "  so_dp, " _
        '                & "  so_disc_header, " _
        '                & "  so_total, " _
        '                & "  so_payment_date, " _
        '                & "  so_cons, " _
        '                & "  so_close_date, " _
        '                & "  so_tran_id, " _
        '                & "  so_trans_id, " _
        '                & "  so_trans_rmks, " _
        '                & "  so_cu_id, " _
        '                & "  so_total_ppn, " _
        '                & "  so_total_pph, " _
        '                & "  so_payment, " _
        '                & "  so_exc_rate, " _
        '                & "  so_tax_inc, " _
        '                & "  (so_total + so_total_ppn + so_total_pph) as so_total_after_tax, " _
        '                & "  so_exc_rate * so_total as so_total_ext,  so_exc_rate * so_total_ppn as so_total_ppn_ext, " _
        '                & "  so_exc_rate * so_total_pph as so_total_pph_ext,  so_exc_rate * (so_total + so_total_ppn + so_total_pph) as so_total_after_tax_ext, " _
        '                & "  en_desc, " _
        '                & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
        '                & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
        '                & "  credit_term.code_name as credit_term_name, " _
        '                & "  tax_class.code_name as tax_class_name, " _
        '                & "  si_desc, " _
        '                & "  pi_desc, " _
        '                & "  pay_type.code_name as pay_type_name, " _
        '                & "  pay_method.code_name as pay_method_name, " _
        '                & "  cc_mstr_ar.cc_desc, " _
        '                & "  cu_name, " _
        '                & "  tran_name, " _
        '                & "  sod_oid, " _
        '                & "  sod_so_oid, " _
        '                & "  sod_pt_id, " _
        '                & "  sod_rmks, " _
        '                & "  sod_qty, " _
        '                & "  sod_um, " _
        '                & "  sod_cost, " _
        '                & "  sod_price, " _
        '                & "  sod_disc, " _
        '                & "  sod_sales_ac_id, " _
        '                & "  sod_sales_sb_id, " _
        '                & "  sod_sales_cc_id, " _
        '                & "  sod_disc_ac_id, " _
        '                & "  sod_um_conv, " _
        '                & "  sod_qty_real, " _
        '                & "  sod_taxable, " _
        '                & "  sod_tax_inc, " _
        '                & "  sod_tax_class, " _
        '                & "  sod_status, " _
        '                & "  sod_dt, " _
        '                & "  sod_payment, " _
        '                & "  sod_dp, " _
        '                & "  sod_sales_unit, " _
        '                & "  sod_loc_id, " _
        '                & "  sod_serial, " _
        '                & "  en_desc, " _
        '                & "  si_desc, " _
        '                & "  pt_code, " _
        '                & "  pt_desc1, " _
        '                & "  pt_desc2, " _
        '                & "  um_mstr.code_name as um_name, " _
        '                & "  ac_mstr_sales.ac_code as ac_code_sales, " _
        '                & "  ac_mstr_sales.ac_name as ac_name_sales, " _
        '                & "  sb_mstr_ar.sb_desc, " _
        '                & "  ac_mstr_disc.ac_code as ac_code_disc, " _
        '                & "  ac_mstr_disc.ac_name as ac_name_disc, " _
        '                & "  tax_class.code_name as sod_tax_class_name, " _
        '                & "  loc_desc " _
        '                & "FROM  " _
        '                & "  public.so_mstr " _
        '                & "  inner join en_mstr on en_id = so_en_id " _
        '                & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
        '                & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = so_ptnr_id_bill " _
        '                & "  inner join code_mstr credit_term on credit_term.code_id = so_credit_term " _
        '                & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
        '                & "  inner join pi_mstr on pi_id = so_pi_id " _
        '                & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
        '                & "  inner join code_mstr pay_method on pay_method.code_id = so_pay_method " _
        '                & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
        '                & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id " _
        '                & "  inner join cu_mstr on cu_id = so_cu_id " _
        '                & "  inner join tran_mstr on tran_id = so_tran_id" _
        '                & "  inner join sod_det on so_oid = sod_so_oid " _
        '                & "  inner join si_mstr on si_id = sod_si_id " _
        '                & "  inner join pt_mstr on pt_id = sod_pt_id " _
        '                & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
        '                & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
        '                & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
        '                & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
        '                & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class	 " _
        '                & "  left outer join loc_mstr on loc_id = sod_loc_id" _
        '                & " where so_date >= " + SetDate(pr_txttglawal.DateTime) _
        '                & " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
        '                & " and so_en_id in (select user_en_id from tconfuserentity " _
        '                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        '    load_data_detail(sql, gc_email, "email")
        '    gv_email.BestFitColumns()

        '    Try
        '        ds.Tables("smart").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select so_oid, so_code, so_trans_id, false as status from so_mstr " _
        '        & " where so_trans_id ~~* 'd' "
        '    load_data_detail(sql, gc_smart, "smart")
        'End If
    End Sub

    Public Overrides Sub relation_detail()
        'Dim ssql As String
        Try
            'load semua data detail diawal trus di filter yg tdk efisien sana sekali
            'berattttt booo

            'gv_detail.Columns("sod_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString)
            'gv_detail.BestFitColumns()

            'gv_detail_attr.Columns("soa_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString)
            'gv_detail.BestFitColumns()

            ''gv_detail_kp.Columns("sokp_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid"))
            ''gv_detail_kp.BestFitColumns()

            'If _conf_value = "1" Then
            '    gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code").ToString)
            '    gv_wf.BestFitColumns()

            '    gv_email.Columns("so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString)
            '    gv_email.BestFitColumns()
            'End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub so_taxable_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles so_taxable.CheckedChanged
        If so_taxable.EditValue = False Then
            so_tax_class.Enabled = False
            so_tax_class.ItemIndex = 0
            so_tax_inc.Enabled = False
            so_tax_inc.Checked = False
        Else
            so_tax_class.Enabled = True
            so_tax_inc.Enabled = True
            so_tax_inc.Checked = False
        End If
    End Sub

    Public Overrides Sub insert_data_awal()
        _so_ptnr_id_sold_mstr = -1
        so_en_id.Focus()
        so_en_id.ItemIndex = 0
        so_date.DateTime = _now
        so_type.ItemIndex = 0
        so_ar_ac_id.ItemIndex = 0
        so_cu_id.ItemIndex = 0
        so_exc_rate.Text = 1
        so_trans_rmks.Text = ""
        so_tran_id.ItemIndex = 0
        so_taxable.EditValue = False
        so_tax_inc.EditValue = False
        so_tax_inc.Enabled = False
        so_tax_class.Enabled = False
        so_ppn_type.ItemIndex = 0
        so_ptnr_id_sold.Text = ""
        so_shipping_address.Text = ""
        so_cons.EditValue = False

        so_cu_id.Enabled = True
        so_type.Enabled = True
        so_pay_type.Enabled = True
        so_pi_id.Enabled = True
        so_ptnr_id_sold.Enabled = True
        so_shipping_address.Enabled = True
        so_ppn_type.Enabled = True
        so_bk_id.ItemIndex = 0
        so_ref_po_code.Text = ""
        _so_ref_po_oid = ""
        so_ref_po_code.Enabled = True

        so_is_package.EditValue = False
        so_pt_id.Tag = ""
        so_pt_id.EditValue = ""
        pt_desc1.EditValue = ""
        pt_desc2.EditValue = ""
        so_manufacture.EditValue = False
        so_price.EditValue = ""
        so_sales_program.ItemIndex = 0
        so_sq_ref_oid.Tag = ""
        so_sq_ref_oid.Text = ""
        so_project.EditValue = False
        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
            tcg_customer.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
        so_psn_ref_oid.Tag = ""

        so_psn_ref_oid.EditValue = ""
        soa_kjb_code.Text = ""
        soa_anak_nama_1.Text = ""
        soa_anak_nama_2.Text = ""
        soa_anak_nama_3.Text = ""
        soa_anak_sekolah_1.Text = ""
        soa_anak_sekolah_2.Text = ""
        soa_anak_sekolah_3.Text = ""
        soa_anak_tgl_lahir_1.Text = ""
        soa_anak_tgl_lahir_2.Text = ""
        soa_anak_tgl_lahir_3.Text = ""
        soa_bank.Text = ""
        soa_bekerja_pada.Text = ""
        soa_berlaku_sd.Text = ""
        soa_email.Text = ""
        soa_jabatan_bagian.Text = ""
        soa_jenis_kartu_kredit.Text = ""
        soa_kantor_alamat_1.Text = ""
        soa_kantor_alamat_2.Text = ""
        soa_kantor_lantai.Text = ""
        soa_kantor_telp.Text = ""
        soa_keluarga_dekat_alamat_1.Text = ""
        soa_keluarga_dekat_alamat_2.Text = ""
        soa_keluarga_dekat_hp.Text = ""
        soa_keluarga_dekat_nama.Text = ""
        soa_keluarga_dekat_telp.Text = ""
        soa_ktp.Text = ""
        soa_no_kartu_kredit.Text = ""
        soa_rumah_alamat_1.Text = ""
        soa_rumah_alamat_2.Text = ""
        soa_rumah_hp.Text = ""
        soa_rumah_kode_pos.Text = ""
        soa_rumah_telp.Text = ""
        soa_status_alamat_kirim.EditValue = False
        soa_status_alamat_tagih.EditValue = False
        soa_status_tempat_tinggal.EditValue = False
        soa_suami_bekerja.Text = ""
        soa_suami_hp.Text = ""
        soa_suami_jabatan.Text = ""
        soa_suami_kantor_alamat_1.Text = ""
        soa_suami_kantor_alamat_2.Text = ""
        soa_suami_nama.Text = ""
        soa_suami_telp.Text = ""

        so_ref_po_code_2.Text = ""
        so_ref_po_code.Text = ""

        te_dpp.EditValue = 0.0
        te_ppn.EditValue = 0.0
        te_discount.EditValue = 0.0
        te_total.EditValue = 0.0
        so_parent_oid.Tag = ""
        so_parent_oid.Text = ""
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
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
                        & "  sod_qty_allocated, " _
                        & "  sod_qty_picked, " _
                        & "  sod_qty_shipment, " _
                        & "  sod_qty_pending_inv, " _
                        & "  sod_qty_invoice, " _
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
                        & "  sod_ppn_type, " _
                        & "  sod_status, " _
                        & "  sod_dt, " _
                        & "  sod_payment, " _
                        & "  sod_dp, " _
                        & "  sod_sales_unit, sod_commision, sod_commision_total, " _
                        & "  sod_loc_id, " _
                        & "  sod_sales_unit_total,0.0 as sod_price_ori_aft_disc_aft_tax_ext,0.0 as sod_price_ori_aft_disc_before_tax_ext,0.0 as sod_price_net, " _
                        & "  sod_serial, " _
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
                        & "  tax_class.code_name as sod_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  sod_pod_oid,sod_sqd_oid,'' as sod_sod_parent_oid " _
                        & "FROM  " _
                        & "  public.sod_det " _
                        & "  inner join so_mstr on so_oid = sod_so_oid " _
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
                        & " where sod_det.sod_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()



                    .SQL = "SELECT  " _
                        & "  a.sodas_oid, " _
                        & "  a.sodas_so_oid, " _
                        & "  a.sodas_pt_id_sod, " _
                        & "  b.pt_code as pt_code_parent, " _
                        & "  b.pt_desc1 as pt_desc1_parent, " _
                        & "  b.pt_desc2 as pt_desc2_parent, " _
                        & "  a.sodas_pt_id, " _
                        & "  c.pt_code, " _
                        & "  c.pt_desc1, " _
                        & "  c.pt_desc2, " _
                        & "  a.sodas_qty,sodas_qty_sold, " _
                        & "  a.sodas_sod_oid " _
                        & "FROM " _
                        & "  public.sodas_assembly a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.sodas_pt_id_sod = b.pt_id) " _
                        & "  INNER JOIN public.pt_mstr c ON (a.sodas_pt_id = c.pt_id) " _
                        & "WHERE " _
                        & "  a.sodas_so_oid is null " _
                        & "ORDER BY " _
                        & "  a.sodas_pt_id_sod, " _
                        & "  a.sodas_pt_id"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_assembly")
                    gc_edit_assembly.DataSource = ds_edit.Tables(1)
                    gv_edit_assembly.BestFitColumns()
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

        If so_ar_ac_id.ItemIndex = -1 Then
            MessageBox.Show("Account can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            Dim _date As Date = so_date.DateTime
            Dim _gcald_det_status As String = func_data.get_gcald_det_status(so_en_id.EditValue, "gcald_so", _date)

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

        If Trim(so_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim sSql As String
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_sales_ac_id")) Then
                MessageBox.Show("Sales Account Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_ppn_type")) Then
                MessageBox.Show("PPN Type Detail Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_cost")) = 0.0 Then
                MessageBox.Show("Cost Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                sSql = "SELECT loc_id " _
                       & "FROM " _
                       & "  public.loc_mstr a " _
                       & "WHERE " _
                       & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("sod_loc_id") & " AND  " _
                       & "  a.loc_en_id=" & so_en_id.EditValue

                If master_new.PGSqlConn.CekRowSelect(sSql) = 0 Then
                    MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next


        'cek inventory kalau terjadi penjualan cash
        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If func_coll.cek_inventory_allocation(ds_edit.Tables(0).Rows(i).Item("sod_en_id"), ds_edit.Tables(0).Rows(i).Item("sod_si_id"), _
                                                    ds_edit.Tables(0).Rows(i).Item("sod_loc_id"), ds_edit.Tables(0).Rows(i).Item("sod_pt_id"), _
                                                    ds_edit.Tables(0).Rows(i).Item("pt_code"), ds_edit.Tables(0).Rows(i).Item("sod_qty"), "''") = False Then
                    Return False
                End If
            Next
        End If

        If so_type.GetColumnValue("value") = "D" Then
            If so_payment_date.Text = "" Then
                MessageBox.Show("Payment Date Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If

        If so_type.EditValue = "D" Then
            If so_pay_type.GetColumnValue("code_usr_1") <> 0 Then
                Dim _nilai_price, _nilai_payment, _nilai_beda As Double
                _nilai_price = 0
                _nilai_payment = 0
                _nilai_beda = 0
                For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                    _nilai_price = ds_edit.Tables(0).Rows(i).Item("sod_price") * (1 - (ds_edit.Tables(0).Rows(i).Item("sod_disc")))
                    _nilai_payment = (ds_edit.Tables(0).Rows(i).Item("sod_payment") * so_pay_type.GetColumnValue("code_usr_1")) + ds_edit.Tables(0).Rows(i).Item("sod_dp")
                    _nilai_beda = _nilai_price - _nilai_payment

                    If _nilai_beda > 500 Or _nilai_beda < -500 Then

                        Box("Column payment error : price :" & _nilai_price & ", payment : " & _nilai_payment)
                        Return False
                    End If
                Next
            End If
        End If
        If so_is_package.EditValue = True Then
            Dim _nilai_diskon As Double = 0
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If i > 0 Then
                    If _nilai_diskon <> ds_edit.Tables(0).Rows(i).Item("sod_disc") Then
                        Box("Discount rate should be the same")
                        Return False
                    End If
                End If
                _nilai_diskon = ds_edit.Tables(0).Rows(i).Item("sod_disc")

            Next
        End If


        If func_coll.get_conf_file("limit_so_by_dbcr") = "1" And func_coll.get_conf_file("limit_so_by_dbcr_date") <> "" Then

            If so_project.EditValue = False Then
                sSql = "SELECT  " _
                  & "  sum(ar_amount - coalesce(ar_pay_amount, 0)) as jml " _
                  & "FROM " _
                  & "  public.ar_mstr " _
                  & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                  & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
                  & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                  & "WHERE " _
                  & "  public.ar_mstr.ar_status  is null and  ptnr_is_ps='Y' and public.so_mstr.so_sales_person = " & so_sales_person.EditValue _
                  & " and ar_date > '" & func_coll.get_conf_file("limit_so_by_dbcr_date") _
                  & "' and    datediff(CURRENT_DATE,ar_date)>3 and coalesce(so_project,'N')='N' "

                'sSql = "SELECT  " _
                '  & "  distinct(public.ar_mstr.ar_oid) as ar_oid " _
                '  & "FROM " _
                '  & "  public.ar_mstr " _
                '  & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
                '  & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
                '  & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                '  & "WHERE " _
                '  & "  public.ar_mstr.ar_status  is null and  ptnr_is_ps='Y' and public.so_mstr.so_sales_person = " & so_sales_person.EditValue _
                '  & " and ar_date > '" & func_coll.get_conf_file("limit_so_by_dbcr_date") _
                '  & "' and    datediff(CURRENT_DATE,ar_date)>3 and coalesce(so_project,'N')='N' "


                Dim dt As New DataTable
                dt = GetTableData(sSql)

                If dt.Rows.Count > 0 Then
                    If SetNumber(dt.Rows(0).Item(0)) > 0.0 Then
                        Box("Check Debit Credit Memo for this sales")
                        Return False
                    End If
                End If
            End If

            'sSql = "SELECT  " _
            '   & "  distinct(public.ar_mstr.ar_oid) as ar_oid " _
            '   & "FROM " _
            '   & "  public.ar_mstr " _
            '   & "  INNER JOIN public.arso_so ON (public.ar_mstr.ar_oid = public.arso_so.arso_ar_oid) " _
            '   & "  INNER JOIN public.so_mstr ON (public.arso_so.arso_so_oid = public.so_mstr.so_oid) " _
            '   & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
            '   & "WHERE " _
            '   & "  public.ar_mstr.ar_status  is null and  ptnr_is_ps='Y' and public.so_mstr.so_sales_person = " & so_sales_person.EditValue _
            '   & "  "

            'Dim dt As New DataTable
            'dt = GetTableData(sSql)

            'If dt.Rows.Count > 3 Then
            '    Box("Check Debit Credit Memo for this sales")
            '    Return False
            'End If


        End If

        If _conf_limit_partner_group_ws = "1" Then
            If Not SetInteger(ptnrg_id.EditValue) = "Null" Then
                sSql = "select ptnrg_credit_limit,ptnrg_term_of_payment from ptnrg_grp where ptnrg_id=" & SetInteger(ptnrg_id.EditValue)

                Dim dt_ptnrg As New DataTable
                dt_ptnrg = GetTableData(sSql)

                Dim _term As Double = 0.0
                For Each dr_ptnrg As DataRow In dt_ptnrg.Rows
                    If Not dr_ptnrg("ptnrg_credit_limit") Is DBNull.Value Then
                        _term = dr_ptnrg("ptnrg_credit_limit")
                    End If

                Next

                If _term > 0.0 Then


                    Dim _so_total, _so_total_ppn, _so_total_pph, _sod_qty, _sod_price, _sod_disc, _so_total_temp, _tax_rate As Double


                    '******* Mencari Total so, Total PPN, Total PPH

                    _so_total = 0
                    _so_total_ppn = 0
                    _so_total_pph = 0

                    For i = 0 To ds_edit.Tables(0).Rows.Count - 1


                        If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                            _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                            _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
                        Else
                            _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
                        End If

                        _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
                        _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

                        _so_total = _so_total + ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
                    Next

                    sSql = "SELECT  " _
                     & "  sum(ar_amount - coalesce(ar_pay_amount,0)) as jml " _
                     & "FROM " _
                     & "  public.ar_mstr " _
                     & "WHERE " _
                     & "  public.ar_mstr.ar_status  is null and  ar_bill_to = " & _so_ptnr_id_sold_mstr _
                     & "  "


                    Dim dt As New DataTable
                    dt = GetTableData(sSql)

                    'If dt.Rows.Count > 0 Then
                    '    Box("Check Debit Credit Memo for this sales")
                    '    Return False
                    'End If
                    Dim _credit As Double = 0.0
                    For Each dr_ptnrg As DataRow In dt.Rows
                        If Not dr_ptnrg("jml") Is DBNull.Value Then
                            _credit = dr_ptnrg(0)
                        End If
                    Next

                    _credit = _credit + _so_total

                    If _credit > _term Then
                        Box("AR Amount : " & MaskDec(_credit - _so_total) & " & current SO " & MaskDec(_so_total) & " over than credit limit : " & MaskDec(_term))
                        Return False
                    End If
                End If

            End If

        End If

        If SetString(so_parent_oid.Tag) <> "" Then
            sSql = "SELECT  " _
                & "  sod_oid, " _
                & "  sod_dom_id, " _
                & "  sod_en_id, " _
                & "  sod_add_by, " _
                & "  sod_add_date, " _
                & "  sod_upd_by, " _
                & "  sod_upd_date, " _
                & "  sod_so_oid, " _
                & "  sod_seq, " _
                & "  sod_is_additional_charge,sod_wo_status, " _
                & "  sod_si_id, " _
                & "  sod_pt_id, " _
                & "  sod_rmks, " _
                & "  sod_qty, " _
                & "  sod_qty_allocated, " _
                & "  sod_qty_picked, " _
                & "  sod_qty_shipment,sod_qty_child, " _
                & "  sod_qty_pending_inv, " _
                & "  sod_qty_invoice, " _
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
                & "  sod_loc_id, " _
                & "  sod_serial, " _
                & "  en_desc, " _
                & "  si_desc, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2,sod_ppn,sod_pph, " _
                & "  um_mstr.code_name as um_name, " _
                & "  sod_sales_unit_total, " _
                & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                & "  sb_desc, " _
                & "  cc_desc, " _
                & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                & "  ac_mstr_disc.ac_name as ac_name_disc,sod_sqd_oid, " _
                & "  tax_class.code_name as sod_tax_class_name, sod_commision, sod_commision_total,  " _
                & "  sod_ppn_type, " _
                & "  loc_desc " _
                & "FROM  " _
                & "  public.sod_det " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
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
                & "  where so_oid='" & so_parent_oid.Tag & "'"

            Dim dt_so_parent As New DataTable
            dt_so_parent = GetTableData(sSql)

            Dim _status_row As Boolean = False
            If dt_so_parent.Rows.Count = 0 Then
                Box("SO Parent Error")
                Return False
            End If

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                _status_row = False
                For Each dr_rows As DataRow In dt_so_parent.Rows
                    If SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) = SetNumber(dr_rows("sod_pt_id")) Then
                        ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid") = dr_rows("sod_oid").ToString

                        _status_row = True
                    End If
                Next

                If _status_row = False Then
                    Box("SO Parent Error")
                    Return False
                    Exit For
                End If

            Next
            ds_edit.AcceptChanges()

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                For Each dr_rows As DataRow In dt_so_parent.Rows
                    If SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) = SetNumber(dr_rows("sod_pt_id")) Then
                        If (SetNumber(dr_rows("sod_qty_shipment")) - SetNumber(dr_rows("sod_qty_child"))) < SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_qty")) Then
                            Box("SO Qty shipment parent lower than qty")
                            Return False
                            Exit For
                        End If
                    End If
                Next
            Next
        End If

        Return before_save
    End Function

#Region "GridView"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _sod_qty, _sod_qty_real, _sod_um_conv, _sod_qty_cost, _sod_cost, _sod_disc, _sod_qty_shipment, _sod_payment, _sod_dp As Double
        Dim _sod_pt_id As Integer

        _sod_um_conv = 1.0
        _sod_qty = 1.0
        _sod_cost = 0.0
        _sod_disc = 0.0
        _sod_payment = 0.0
        _sod_dp = 0.0
        _sod_pt_id = -1

        If e.Column.Name = "sod_qty" Then
            '********* Cek Qty Processed
            Try
                _sod_qty_shipment = (gv_edit.GetRowCellValue(e.RowHandle, "sod_qty_shipment"))
            Catch ex As Exception
            End Try

            If e.Value < _sod_qty_shipment Then
                MessageBox.Show("Qty SO Can't Lower Than Qty shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            '********************************
            Try
                _sod_pt_id = (gv_edit.GetRowCellValue(e.RowHandle, "sod_pt_id"))
            Catch ex As Exception
            End Try

            Try
                _sod_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "sod_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _sod_cost = (gv_edit.GetRowCellValue(e.RowHandle, "sod_cost"))
            Catch ex As Exception
            End Try

            Try
                _sod_disc = (gv_edit.GetRowCellValue(e.RowHandle, "sod_disc"))
            Catch ex As Exception
            End Try

            _sod_qty_real = e.Value * _sod_um_conv
            _sod_qty_cost = (e.Value * _sod_cost) - (e.Value * _sod_cost * _sod_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_real", _sod_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_cost", _sod_qty_cost)

            dt_bantu = New DataTable

            Dim _price As Double = 0.0
            If _conf_so_price = "1" Then
                Dim ssql As String

                ssql = "select calc_biaya_per_buku from calc_mstr where calc_pt_id=" _
                & SetInteger(_sod_pt_id) & " order by calc_date desc limit 1"
                Dim dt_price As New DataTable
                dt_price = GetTableData(ssql)
                For Each dr As DataRow In dt_price.Rows
                    _price = SetNumber(dr(0))
                Next

            Else
                dt_bantu = (load_price_list(so_pi_id.EditValue, _sod_pt_id, so_pay_type.EditValue, e.Value))

            End If

            If dt_bantu.Rows.Count > 0 Then

                gv_edit.SetRowCellValue(e.RowHandle, "sod_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                'gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", _sod_payment)
                'gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", _sod_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", dt_bantu.Rows(0).Item("pidd_payment"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", dt_bantu.Rows(0).Item("pidd_dp"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", dt_bantu.Rows(0).Item("pidd_commision"))
            Else
                If _conf_so_price = "1" Then
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", _price)
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", 0.0)
                End If

                gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", 0.0)
            End If

            Dim _sod_price_ori_aft_disc_before_tax_ext As Double = 0.0
            Dim _sod_price_ori_aft_disc_aft_tax_ext As Double = 0.0
            Dim _sod_price_net As Double = 0.0

            Dim sod_price As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_price"))
            Dim sod_disc As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_disc"))
            Dim sod_qty As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_qty"))
            Dim _so_exc_rate As Double = SetNumber(so_exc_rate.EditValue)
            Dim _tax_class As Integer = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_class"))
            Dim rate_ppn As Double = func_coll.get_ppn(_tax_class) * 100
            Dim rate_pph As Double = func_coll.get_pph(_tax_class) * 100

            If SetString(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_inc")) = "N" Then
                _sod_price_ori_aft_disc_aft_tax_ext = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
                 * (rate_ppn / 100.0)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph / 100.0))) * sod_qty), 2) _
                 * _so_exc_rate
            Else
                _sod_price_ori_aft_disc_aft_tax_ext = System.Math.Round(((((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - _
                ((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) + _
                (((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - ((sod_price - _
                ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) * (rate_ppn / 100.0)) - _
                (((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - ((sod_price - ((rate_ppn / 100.0) * _
                (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) * (rate_pph / 100.0))) * sod_qty), 2) * _so_exc_rate
            End If

            _sod_price_ori_aft_disc_before_tax_ext = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
               * (1)) - ((sod_price - (sod_price * sod_disc)) * (1))) * sod_qty), 2) _
               * _so_exc_rate

            _sod_price_net = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
               * (1)) - ((sod_price - (sod_price * sod_disc)) * (1)))), 2) _
               * _so_exc_rate

            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_net", _sod_price_net)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_ori_aft_disc_before_tax_ext", _sod_price_ori_aft_disc_aft_tax_ext)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_ori_aft_disc_aft_tax_ext", _sod_price_ori_aft_disc_aft_tax_ext)

            'footer()
        ElseIf e.Column.Name = "sod_cost" Then
            Try
                _sod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_qty")))
            Catch ex As Exception
            End Try
            Try
                If e.Value Is System.DBNull.Value Then
                    Box("Cost null")
                    Exit Sub
                End If
            Catch ex As Exception

            End Try
            Try
                _sod_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_disc")))
            Catch ex As Exception
            End Try

            _sod_qty_cost = (e.Value * _sod_qty) - (e.Value * _sod_qty * _sod_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_cost", _sod_qty_cost)
            'footer()
        ElseIf e.Column.Name = "sod_disc" Then
            Try
                _sod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_qty")))
            Catch ex As Exception
            End Try

            Try
                _sod_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_cost")))
            Catch ex As Exception
            End Try

            _sod_qty_cost = (_sod_cost * _sod_qty) - (_sod_cost * _sod_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_cost", _sod_qty_cost)
            'footer()
            Dim _sod_price_ori_aft_disc_before_tax_ext As Double = 0.0
            Dim _sod_price_ori_aft_disc_aft_tax_ext As Double = 0.0
            Dim _sod_price_net As Double = 0.0

            Dim sod_price As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_price"))
            Dim sod_disc As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_disc"))
            Dim sod_qty As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_qty"))
            Dim _so_exc_rate As Double = SetNumber(so_exc_rate.EditValue)
            Dim _tax_class As Integer = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_class"))
            Dim rate_ppn As Double = func_coll.get_ppn(_tax_class) * 100
            Dim rate_pph As Double = func_coll.get_pph(_tax_class) * 100

            If SetString(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_inc")) = "N" Then
                _sod_price_ori_aft_disc_aft_tax_ext = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
                 * (rate_ppn / 100.0)) - ((sod_price - (sod_price * sod_disc)) * (rate_pph / 100.0))) * sod_qty), 2) _
                 * _so_exc_rate
            Else
                _sod_price_ori_aft_disc_aft_tax_ext = System.Math.Round(((((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - _
                ((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) + _
                (((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - ((sod_price - _
                ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) * (rate_ppn / 100.0)) - _
                (((sod_price - ((rate_ppn / 100.0) * (sod_price / (1.0 + (rate_ppn / 100.0))))) - ((sod_price - ((rate_ppn / 100.0) * _
                (sod_price / (1.0 + (rate_ppn / 100.0))))) * sod_disc)) * (rate_pph / 100.0))) * sod_qty), 2) * _so_exc_rate
            End If

            _sod_price_ori_aft_disc_before_tax_ext = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
               * (1)) - ((sod_price - (sod_price * sod_disc)) * (1))) * sod_qty), 2) _
               * _so_exc_rate

            _sod_price_net = System.Math.Round((((sod_price - (sod_price * sod_disc)) + ((sod_price - (sod_price * sod_disc)) _
               * (1)) - ((sod_price - (sod_price * sod_disc)) * (1)))), 2) _
               * _so_exc_rate

            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_net", _sod_price_net)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_ori_aft_disc_before_tax_ext", _sod_price_ori_aft_disc_aft_tax_ext)
            gv_edit.SetRowCellValue(e.RowHandle, "sod_price_ori_aft_disc_aft_tax_ext", _sod_price_ori_aft_disc_aft_tax_ext)

        ElseIf e.Column.Name = "sod_um_conv" Then
            Try
                _sod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_qty")))
            Catch ex As Exception
            End Try

            _sod_qty_real = e.Value * _sod_qty

            gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_real", _sod_qty_real)
            'footer()
        ElseIf e.Column.Name = "sod_taxable" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "sod_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "sod_tax_inc", "N")
                gv_edit.SetRowCellValue(e.RowHandle, "sod_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_tax_class_name", "NON-TAX")
            End If
            'footer()
        ElseIf e.Column.Name = "sod_tax_inc" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "sod_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "sod_tax_inc", "N")
            End If
            'footer()
        ElseIf e.Column.Name = "sod_pt_id" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0.0
            _sod_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sod_qty_real")))

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(so_pi_id.EditValue, e.Value, so_pay_type.EditValue, _sod_qty))


            Try
                _sod_pt_id = e.Value
            Catch ex As Exception
            End Try

            If gv_edit.GetRowCellValue(e.RowHandle, "sod_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sod_payment = 0.0 * _sod_qty
                _sod_dp = 0.0 * _sod_qty
            Else
                _sod_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sod_qty
                _sod_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sod_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_inc").ToString.ToUpper = "N" Then
                _sod_payment = _sod_payment + (_sod_payment * _tax_rate)
                _sod_dp = _sod_dp + (_sod_dp * _tax_rate)
            End If

            Dim _price As Double = 0
            If _conf_so_price = "1" Then
                Dim ssql As String

                ssql = "select calc_biaya_per_buku from calc_mstr where calc_pt_id=" & SetInteger(_sod_pt_id) & " order by calc_date desc limit 1"
                Dim dt_price As New DataTable
                dt_price = GetTableData(ssql)
                For Each dr As DataRow In dt_price.Rows
                    _price = SetNumber(dr(0))
                Next


            End If

            'If so_type.EditValue = "D" Then
            If dt_bantu.Rows.Count > 0 Then
                If _conf_so_price = "1" Then
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", _price)
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", dt_bantu.Rows(0).Item("pidd_price"))
                End If

                gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", _sod_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", _sod_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", dt_bantu.Rows(0).Item("pidd_commision"))
            Else
                If _conf_so_price = "1" Then
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", _price)
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_price", 0.0)
                End If
                gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", 0.0)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", 0.0)
            End If
            'End If
            'footer()
        ElseIf e.Column.Name = "sod_qty_real" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0.0
            Dim _pt_id As Integer
            Try
                _pt_id = gv_edit.GetRowCellValue(e.RowHandle, "sod_pt_id")
            Catch ex As Exception
                Exit Sub
            End Try

            _sod_qty = e.Value

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(so_pi_id.EditValue, _pt_id, so_pay_type.EditValue, _sod_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "sod_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _sod_payment = 0 * _sod_qty
                _sod_dp = 0 * _sod_qty
            Else
                _sod_payment = dt_bantu.Rows(0).Item("pidd_payment") * _sod_qty
                _sod_dp = dt_bantu.Rows(0).Item("pidd_dp") * _sod_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "sod_tax_inc").ToString.ToUpper = "N" Then
                _sod_payment = _sod_payment + (_sod_payment * _tax_rate)
                _sod_dp = _sod_dp + (_sod_dp * _tax_rate)
            End If

            Dim _price As Double = 0
            If _conf_so_price = "1" Then
                Dim ssql As String

                ssql = "select calc_biaya_per_buku from calc_mstr where calc_pt_id=" & SetInteger(_sod_pt_id) & " order by calc_date desc limit 1"
                Dim dt_price As New DataTable
                dt_price = GetTableData(ssql)
                For Each dr As DataRow In dt_price.Rows
                    _price = SetNumber(dr(0))
                Next


            End If

            If so_type.EditValue = "D" Then
                If dt_bantu.Rows.Count > 0 Then
                    If _conf_so_price = "1" Then
                        gv_edit.SetRowCellValue(e.RowHandle, "sod_price", _price)
                    Else
                        gv_edit.SetRowCellValue(e.RowHandle, "sod_price", dt_bantu.Rows(0).Item("pidd_price"))
                    End If
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", _sod_payment)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", _sod_dp)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", dt_bantu.Rows(0).Item("pidd_commision"))
                Else
                    If _conf_so_price = "1" Then
                        gv_edit.SetRowCellValue(e.RowHandle, "sod_price", _price)
                    Else
                        gv_edit.SetRowCellValue(e.RowHandle, "sod_price", 0.0)
                    End If
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_disc", 0.0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_payment", 0.0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_dp", 0.0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit", 0.0)
                    gv_edit.SetRowCellValue(e.RowHandle, "sod_commision", 0.0)
                End If
            End If

            gv_edit.SetRowCellValue(e.RowHandle, "sod_sales_unit_total", 0)
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
                            & "  coalesce(pidd_payment,0) as pidd_payment,coalesce(pidd_commision,0) as pidd_commision, " _
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
        Dim _sod_en_id As Integer = gv_edit.GetRowCellValue(_row, "sod_en_id")
        Dim _sod_si_id As Integer = gv_edit.GetRowCellValue(_row, "sod_si_id")

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
            frm._en_id = _sod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            If so_is_package.EditValue = True Then
                Exit Sub
            End If


            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sod_en_id
            frm._si_id = _sod_si_id
            frm._so_type = so_type.EditValue
            frm._tran_oid = _so_ref_po_oid
            If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
                frm._so_cash = True
            Else
                frm._so_cash = False
            End If
            frm._ppn_type = so_ppn_type.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'Dimatikan karena dianggap belum perlu rubah data ini dan ini diambil otomatis dari prodline
            'ElseIf _col = "ac_code_sales" Then
            '    Dim frm As New FAccountSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm.type_form = True
            '    frm.ShowDialog
            'ElseIf _col = "ac_name_sales" Then
            '    Dim frm As New FAccountSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm.type_form = True
            '    frm.ShowDialog
            'ElseIf _col = "sb_desc" Then
            '    Dim frm As New FSubAccountSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = _sod_en_id
            '    frm.type_form = True
            '    frm.ShowDialog
            'ElseIf _col = "cc_desc" Then
            '    Dim frm As New FCostCenterSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = _sod_en_id
            '    frm.type_form = True
            '    frm.ShowDialog
        ElseIf _col = "um_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "sod_tax_class_name" Then
            If gv_edit.GetRowCellValue(_row, "sod_taxable") = "N" Then
                Exit Sub
            End If

            Dim frm As New FTaxClassSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "sod_ppn_type" Then
            '    Dim frm As New FPPNTypeSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm.type_form = True
            '    frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sod_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        'Dim _sod_qty_shipment As Double = 0

        'Try
        '    _sod_qty_shipment = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "sod_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sod_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        cek_shipment_row()
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        'Dim _sod_qty_shipment As Double = 0

        'Try
        '    _sod_qty_shipment = ((gv_edit.GetRowCellValue(0, "sod_qty_shipment")))
        'Catch ex As Exception
        'End Try

        'If _sod_qty_shipment <> 0 Then
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        'Else
        '    gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        'End If
        cek_shipment_row()
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "sod_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "sod_en_id", so_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", so_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "sod_si_id", so_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", so_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "sod_is_additional_charge", "N")
            .SetRowCellValue(e.RowHandle, "sod_qty", 1.0)
            .SetRowCellValue(e.RowHandle, "sod_cost", 0.0)
            .SetRowCellValue(e.RowHandle, "sod_price", 0.0)
            .SetRowCellValue(e.RowHandle, "sod_disc", 0.0)
            .SetRowCellValue(e.RowHandle, "sod_taxable", IIf(so_taxable.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "sod_tax_inc", IIf(so_tax_inc.EditValue = True, "Y", "N"))
            .SetRowCellValue(e.RowHandle, "sod_tax_class", so_tax_class.EditValue)
            .SetRowCellValue(e.RowHandle, "sod_tax_class_name", so_tax_class.GetColumnValue("code_name"))
            .SetRowCellValue(e.RowHandle, "sod_um_conv", 1.0)
            .SetRowCellValue(e.RowHandle, "sod_qty_real", 1.0)
            .SetRowCellValue(e.RowHandle, "sod_qty_cost", 0.0)

            .SetRowCellValue(e.RowHandle, "sod_payment", 0.0)
            .SetRowCellValue(e.RowHandle, "sod_dp", 0.0)
            .SetRowCellValue(e.RowHandle, "sod_sales_unit", 0.0)
            .BestFitColumns()
        End With

        so_cu_id.Enabled = False
        so_type.Enabled = False
        so_pi_id.Enabled = True
        so_pay_type.Enabled = False
        so_ppn_type.Enabled = False
    End Sub

#End Region

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Function GetID_Local(ByVal par_en_code As String) As Long
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
                                           " where substring(cast(ptnr_id as varchar),5,100) <> '' and ptnr_en_id=(select en_id from en_mstr where en_code='" _
                                           & par_en_code & "') and substring(cast(ptnr_id as varchar),3,2)='" & master_new.ClsVar.sServerCode & "'"
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

        GetID_Local = CLng(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _so_oid As Guid
        _so_oid = Guid.NewGuid
        Dim _so_code, _so_terbilang As String
        Dim _so_total, _so_total_ppn, _so_total_pph, _sod_qty, _sod_price, _sod_disc, _so_total_temp, _tax_rate As Double
        Dim i As Integer


        If cek_duplikat_pt_id(gv_edit, "sod_pt_id") = False Then
            Return False
        End If

        _so_code = func_coll.get_transaction_number("SO", so_en_id.GetColumnValue("en_code"), "so_mstr", "so_code")
        ssqls.Clear()

        Dim _so_trn_status As String
        Dim ds_bantu As New DataSet

        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            _so_trn_status = "C" 'Lansung Default Ke Close
        Else
            _so_trn_status = "D" 'Lansung Default Ke Draft
        End If


        ds_bantu = func_data.load_aprv_mstr(so_tran_id.EditValue)

        '******* Mencari Total so, Total PPN, Total PPH

        _so_total = 0
        _so_total_ppn = 0
        _so_total_pph = 0
        _so_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            ''disini hanya line ppn saja
            ' _cost kalau disini diganti jadi price..karena dihitung dari price.....

            If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
            Else
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
            End If

            _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
            _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

            _so_total = _so_total + ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
        Next

        'disini dihitung ppn dan pph nya
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
                Else
                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
                End If

                _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
                _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

                _so_total_temp = ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
                _so_total_ppn = _so_total_ppn + (_so_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                _so_total_pph = _so_total_pph + (_so_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
            End If
        Next
        '*******************************************************

        _so_terbilang = func_bill.TERBILANG_FIX(_so_total + _so_total_ppn - _so_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sod_dp") * ds_edit.Tables(0).Rows(i).Item("sod_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sod_payment") * ds_edit.Tables(0).Rows(i).Item("sod_qty"))
        Next
        '*************************

        'Deklarasi varible untuk customer baru
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid





        '*************************************

        ds_edit.Tables(1).Rows.Clear()

        Dim dt_pt As New DataTable
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            Dim ssql As String

            sSql = "SELECT  " _
             & "  b.psd_pt_bom_id, " _
             & "  c.pt_code, " _
             & "  c.pt_desc1, " _
             & "  c.pt_desc2, " _
             & "  sum(b.psd_qty) as psd_qty " _
             & "FROM " _
             & "  public.psd_det b " _
             & "  INNER JOIN public.pt_mstr c ON (b.psd_pt_bom_id = c.pt_id) " _
             & "  INNER JOIN public.ps_mstr a ON (b.psd_ps_oid = a.ps_oid) " _
             & "WHERE " _
             & "  a.ps_pt_bom_id =" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) _
             & " group by psd_pt_bom_id,pt_code,pt_desc1,pt_desc2"

            dt_pt = master_new.PGSqlConn.GetTableData(ssql)

            Dim _dtrow As DataRow
            For Each dr As DataRow In dt_pt.Rows
                _dtrow = ds_edit.Tables(1).NewRow
                _dtrow("sodas_so_oid") = _so_oid.ToString
                _dtrow("sodas_sod_oid") = ds_edit.Tables(0).Rows(i).Item("sod_oid")
                _dtrow("sodas_pt_id_sod") = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code_parent") = ds_edit.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1_parent") = ds_edit.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2_parent") = ds_edit.Tables(0).Rows(i).Item("pt_desc2")

                _dtrow("sodas_pt_id") = dr("psd_pt_bom_id")
                _dtrow("pt_code") = dr("pt_code")
                _dtrow("pt_desc1") = dr("pt_desc1")
                _dtrow("pt_desc2") = dr("pt_desc2")
                _dtrow("sodas_qty") = dr("psd_qty")
                _dtrow("sodas_qty_sold") = dr("psd_qty") * SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_qty"))

                ds_edit.Tables(1).Rows.Add(_dtrow)
            Next

            ds_edit.Tables(1).AcceptChanges()
            gv_edit_assembly.BestFitColumns()

        Next



        If _so_ptnr_id_sold_mstr > -1 Then
            _id_ptnr_so = _so_ptnr_id_sold_mstr
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'Insert Data Customer Baru
                        If _so_ptnr_id_sold_mstr = -1 Then

                            Dim _ptnr_id As Long
                            '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", so_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", so_en_id.EditValue.ToString))

                            _ptnr_id = SetInteger(GetID_Local(so_en_id.GetColumnValue("en_code")))

                            Dim _ptnr_code As String
                            _ptnr_code = "CU" + "00"
                            'Return False
                            Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)

                            'If Len(_ptnr_id_s) = 1 Then
                            '    _ptnr_id_s = "000000" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 2 Then
                            '    _ptnr_id_s = "00000" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 3 Then
                            '    _ptnr_id_s = "0000" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 4 Then
                            '    _ptnr_id_s = "000" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 5 Then
                            '    _ptnr_id_s = "00" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 6 Then
                            '    _ptnr_id_s = "0" + _ptnr_id_s.ToString
                            'ElseIf Len(_ptnr_id_s) = 7 Then
                            '    _ptnr_id_s = _ptnr_id_s.ToString
                            'End If

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

                            _ptnr_code = _ptnr_code + IIf(so_en_id.GetColumnValue("en_code") = 0, "99", so_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

                            _id_ptnr_so = _ptnr_id


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnr_mstr " _
                                                & "( " _
                                                & "  ptnr_oid, " _
                                                & "  ptnr_dom_id, " _
                                                & "  ptnr_en_id, " _
                                                & "  ptnr_add_by, " _
                                                & "  ptnr_add_date, " _
                                                & "  ptnr_id, " _
                                                & "  ptnr_code, " _
                                                & "  ptnr_name, " _
                                                & "  ptnr_ptnrg_id, " _
                                                & "  ptnr_url, " _
                                                & "  ptnr_email, " _
                                                & "  ptnr_npwp, " _
                                                & "  ptnr_nppkp, " _
                                                & "  ptnr_remarks, " _
                                                & "  ptnr_is_cust, " _
                                                & "  ptnr_is_vend, " _
                                                & "  ptnr_is_member, " _
                                                & "  ptnr_is_emp, " _
                                                & "  ptnr_is_writer, " _
                                                & "  ptnr_ac_ar_id, " _
                                                & "  ptnr_sb_ar_id, " _
                                                & "  ptnr_cc_ar_id, " _
                                                & "  ptnr_ac_ap_id, " _
                                                & "  ptnr_sb_ap_id, " _
                                                & "  ptnr_cc_ap_id, " _
                                                & "  ptnr_cu_id, " _
                                                & "  ptnr_limit_credit, " _
                                                & "  ptnr_active, " _
                                                & "  ptnr_transaction_code_id, " _
                                                & "  ptnr_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(so_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & _ptnr_id & ",  " _
                                                & SetSetring(_ptnr_code) & ",  " _
                                                & SetSetring(so_ptnr_id_sold.Text) & ",  " _
                                                & 0 & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetBitYN(True) & ",  " _
                                                & SetBitYN(False) & ",  " _
                                                & SetBitYN(False) & ",  " _
                                                & SetBitYN(False) & ",  " _
                                                & SetBitYN(False) & ",  " _
                                                & SetInteger(so_ar_ac_id.EditValue) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(0) & ",  " _
                                                & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                                & SetDbl(0) & ",  " _
                                                & SetBitYN(True) & ",  " _
                                                & " null " & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnra_addr " _
                                                & "( " _
                                                & "  ptnra_oid, " _
                                                & "  ptnra_id, " _
                                                & "  ptnra_dom_id, " _
                                                & "  ptnra_en_id, " _
                                                & "  ptnra_add_by, " _
                                                & "  ptnra_add_date, " _
                                                & "  ptnra_line, " _
                                                & "  ptnra_line_1, " _
                                                & "  ptnra_line_2, " _
                                                & "  ptnra_line_3, " _
                                                & "  ptnra_phone_1, " _
                                                & "  ptnra_phone_2, " _
                                                & "  ptnra_fax_1, " _
                                                & "  ptnra_fax_2, " _
                                                & "  ptnra_zip, " _
                                                & "  ptnra_ptnr_oid, " _
                                                & "  ptnra_addr_type, " _
                                                & "  ptnra_comment, " _
                                                & "  ptnra_active, " _
                                                & "  ptnra_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", so_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", so_en_id.EditValue.ToString)) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(so_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", so_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                & SetSetring(so_shipping_address.Text) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & " (select code_id from code_mstr where code_field ~~* 'addr_type_mstr' and code_name ~~* 'bill to') " & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetBitYN(True) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '******************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.so_mstr " _
                                            & "( " _
                                            & "  so_oid,so_project,so_sales_program, " _
                                            & "  so_dom_id, " _
                                            & "  so_en_id, " _
                                            & "  so_add_by, " _
                                            & "  so_add_date, " _
                                            & "  so_code, " _
                                            & "  so_ptnr_id_sold, " _
                                            & "  so_ptnr_id_bill, " _
                                            & "  so_ref_po_code,so_ref_po_code_2, " _
                                            & "  so_ref_po_oid, " _
                                            & "  so_date, " _
                                            & "  so_credit_term, " _
                                            & "  so_taxable, " _
                                            & "  so_tax_class, " _
                                            & "  so_ppn_type, " _
                                            & "  so_si_id, " _
                                            & "  so_type, " _
                                            & "  so_sales_person, " _
                                            & "  so_pi_id, " _
                                            & "  so_pay_type, " _
                                            & "  so_pay_method, " _
                                            & "  so_cons, " _
                                            & "  so_ar_ac_id, " _
                                            & "  so_ar_sb_id, " _
                                            & "  so_ar_cc_id, " _
                                            & "  so_dp, " _
                                            & "  so_disc_header, " _
                                            & "  so_total, " _
                                            & "  so_payment_date, " _
                                            & "  so_tran_id, " _
                                            & "  so_trans_id, " _
                                            & "  so_trans_rmks, " _
                                            & "  so_dt, " _
                                            & "  so_cu_id, " _
                                            & "  so_bk_id, " _
                                            & "  so_total_ppn, " _
                                            & "  so_total_pph, " _
                                            & "  so_payment, " _
                                            & "  so_exc_rate, " _
                                            & "  so_tax_inc,so_manufacture,so_parent_oid,so_parent_code, " _
                                            & "  so_interval,so_is_package,so_pt_id,so_price,so_shipping_address, so_sq_ref_oid,so_sq_ref_code,so_psn_ref_code," _
                                            & "  so_terbilang " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_so_oid.ToString) & ",  " _
                                            & SetBitYN(so_project.EditValue) & ",  " _
                                            & SetSetring(so_sales_program.EditValue) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(so_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_so_code) & ",  " _
                                            & SetInteger(_id_ptnr_so) & ",  " _
                                            & SetInteger(_id_ptnr_so) & ",  " _
                                            & SetSetring(so_ref_po_code.Text) & ",  " _
                                            & SetSetring(so_ref_po_code_2.Text) & ",  " _
                                            & SetSetring(_so_ref_po_oid) & ",  " _
                                            & SetDate(so_date.DateTime) & ",  " _
                                            & SetInteger(so_credit_term.EditValue) & ",  " _
                                            & SetBitYN(so_taxable.EditValue) & ",  " _
                                            & SetInteger(so_tax_class.EditValue) & ",  " _
                                            & SetSetring(so_ppn_type.EditValue) & ",  " _
                                            & SetInteger(so_si_id.EditValue) & ",  " _
                                            & SetSetring(so_type.EditValue) & ",  " _
                                            & SetInteger(so_sales_person.EditValue) & ",  " _
                                            & SetInteger(so_pi_id.EditValue) & ",  " _
                                            & SetInteger(so_pay_type.EditValue) & ",  " _
                                            & SetInteger(so_pay_method.EditValue) & ",  " _
                                            & SetBitYN(so_cons.EditValue) & ",  " _
                                            & SetInteger(so_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(so_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(so_ar_cc_id.EditValue) & ",  " _
                                            & SetDbl(_total_dp) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(_so_total) & ",  " _
                                            & SetDate(so_payment_date.DateTime) & ",  " _
                                            & SetInteger(so_tran_id.EditValue) & ",  " _
                                            & SetSetring(_so_trn_status) & ",  " _
                                            & SetSetring(so_trans_rmks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(so_cu_id.EditValue) & ",  " _
                                            & SetInteger(so_bk_id.EditValue) & ",  " _
                                            & SetDbl(_so_total_ppn) & ",  " _
                                            & SetDbl(_so_total_pph) & ",  " _
                                            & SetDbl(_total_payment) & ",  " _
                                            & SetDbl(so_exc_rate.EditValue) & ",  " _
                                            & SetBitYN(so_tax_inc.EditValue) & ",  " _
                                            & SetBitYN(so_manufacture.EditValue) & ",  " _
                                            & SetSetring(so_parent_oid.Tag) & " ," _
                                            & SetSetring(so_parent_oid.Text) & " ," _
                                            & SetInteger(so_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                            & SetBitYN(so_is_package.EditValue) & ",  " _
                                            & SetInteger(so_pt_id.Tag) & ",  " _
                                            & SetDec(so_price.EditValue) & ",  " _
                                            & SetSetring(so_shipping_address.Text) & ",  " _
                                            & SetSetring(so_sq_ref_oid.Tag) & " ," _
                                            & SetSetring(so_sq_ref_oid.Text) & " ," _
                                            & SetSetring(so_psn_ref_oid.Text) & " ," _
                                            & SetSetring(_so_terbilang) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        If SetString(so_psn_ref_oid.Text) <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update  " _
                                                & "  public.pesanan " _
                                                & "set pesan_status='Sudah Proses',pesan_so_code=" & SetSetring(_so_code) & " where pesan_code=" & SetSetring(so_psn_ref_oid.Text)

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If


                        If func_coll.get_conf_file("wf_sales_quotation") = "1" Then
                            If SetString(so_sq_ref_oid.Text) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sq_mstr set sq_trans_id = 'C', sq_close_date = " & SetDateNTime00(so_date.DateTime) & " " + _
                                                       " where sq_oid = " & SetSetring(so_sq_ref_oid.Tag) & " "

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        End If


                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            Dim _so_ppn, _so_pph As Double
                            _so_ppn = 0
                            _so_pph = 0

                            If ds_edit.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                                If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                                    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
                                Else
                                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
                                End If

                                _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
                                _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

                                _so_total_temp = ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
                                _so_ppn = (_so_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                                _so_pph = (_so_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                            End If


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sod_det " _
                                                & "( " _
                                                & "  sod_oid, " _
                                                & "  sod_dom_id, " _
                                                & "  sod_en_id, " _
                                                & "  sod_add_by, " _
                                                & "  sod_add_date, " _
                                                & "  sod_so_oid, " _
                                                & "  sod_seq, " _
                                                & "  sod_is_additional_charge, " _
                                                & "  sod_si_id, " _
                                                & "  sod_pt_id, " _
                                                & "  sod_rmks, " _
                                                & "  sod_qty, " _
                                                & "  sod_um, " _
                                                & "  sod_cost, " _
                                                & "  sod_price, " _
                                                & "  sod_disc, " _
                                                & "  sod_sales_ac_id, " _
                                                & "  sod_sales_sb_id, " _
                                                & "  sod_sales_cc_id, " _
                                                & "  sod_um_conv, " _
                                                & "  sod_qty_real, " _
                                                & "  sod_taxable, " _
                                                & "  sod_tax_inc, " _
                                                & "  sod_tax_class, " _
                                                & "  sod_ppn_type, " _
                                                & "  sod_dt, " _
                                                & "  sod_payment, " _
                                                & "  sod_dp, " _
                                                & "  sod_loc_id, " _
                                                & "  sod_sales_unit,sod_ppn,sod_pph,sod_commision,sod_commision_total,sod_sod_parent_oid, " _
                                                & "  sod_pod_oid,sod_sqd_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_oid")) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_so_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_is_additional_charge")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sod_rmks")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_disc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_ppn_type")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sod_payment")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sod_dp")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_loc_id")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sod_sales_unit")) & ",  " _
                                                & SetDbl(_so_ppn) & "," _
                                                & SetDbl(_so_pph) & "," _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sod_commision")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("sod_commision_total")) & ",  " _
                                                 & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid").ToString) & " , " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_pod_oid").ToString) & " , " _
                                                 & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sqd_oid").ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '

                            If SetString(so_parent_oid.Tag) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  sod_det " _
                                                    & "set sod_qty_child= coalesce(sod_qty_child,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) _
                                                    & " where sod_oid=" & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If


                            If SetString(ds_edit.Tables(0).Rows(i).Item("sod_pod_oid").ToString) <> "" Then
                                'update karena ada hubungan antara so dan po antar group perusahaan
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_so = coalesce(pod_qty_so,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) _
                                                     & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_pod_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If SetString(ds_edit.Tables(0).Rows(i).Item("sod_sqd_oid").ToString) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sqd_det set sqd_qty_so = coalesce(sqd_qty_so,0) + " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) _
                                                     & " where sqd_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sqd_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()



                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sq_mstr set sq_close_date = " & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & ", sq_trans_id = 'C' " + _
                                                     " where coalesce((select count(sqd_sq_oid) as jml From sqd_det " + _
                                                     " where sqd_qty <> coalesce(sqd_qty_so,0) " + _
                                                     " and sqd_sq_oid = " & SetSetring(so_sq_ref_oid.Tag) & " " & _
                                                     " group by sqd_sq_oid),0) = 0 " & _
                                                     " and sq_oid = " & SetSetring(so_sq_ref_oid.Tag) & " "

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        For i = 0 To ds_edit.Tables(1).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.sodas_assembly " _
                                & "( " _
                                & "  sodas_oid, " _
                                & "  sodas_so_oid, " _
                                & "  sodas_pt_id_sod, " _
                                & "  sodas_pt_id, " _
                                & "  sodas_qty, " _
                                & "  sodas_sod_oid, " _
                                & "  sodas_qty_sold " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(ds_edit.Tables(1).Rows(i).Item("sodas_so_oid")) & ",  " _
                                & SetInteger(ds_edit.Tables(1).Rows(i).Item("sodas_pt_id_sod")) & ",  " _
                                & SetInteger(ds_edit.Tables(1).Rows(i).Item("sodas_pt_id")) & ",  " _
                                & SetDec(ds_edit.Tables(1).Rows(i).Item("sodas_qty")) & ",  " _
                                & SetSetring(ds_edit.Tables(1).Rows(i).Item("sodas_sod_oid")) & ",  " _
                                & SetDec(ds_edit.Tables(1).Rows(i).Item("sodas_qty_sold")) & "  " _
                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If Trim(soa_bekerja_pada.Text) <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.soa_attr " _
                                                & "( " _
                                                & "  soa_oid, " _
                                                & "  soa_kjb_code, " _
                                                & "  soa_bekerja_pada, " _
                                                & "  soa_jabatan_bagian, " _
                                                & "  soa_kantor_alamat_1, " _
                                                & "  soa_kantor_alamat_2, " _
                                                & "  soa_kantor_lantai, " _
                                                & "  soa_kantor_telp, " _
                                                & "  soa_ktp, " _
                                                & "  soa_email, " _
                                                & "  soa_rumah_alamat_1, " _
                                                & "  soa_rumah_alamat_2, " _
                                                & "  soa_rumah_kode_pos, " _
                                                & "  soa_rumah_telp, " _
                                                & "  soa_rumah_hp, " _
                                                & "  soa_status_alamat_kirim, " _
                                                & "  soa_status_alamat_tagih, " _
                                                & "  soa_suami_nama, " _
                                                & "  soa_suami_bekerja, " _
                                                & "  soa_suami_jabatan, " _
                                                & "  soa_suami_kantor_alamat_1, " _
                                                & "  soa_suami_kantor_alamat_2, " _
                                                & "  soa_suami_telp, " _
                                                & "  soa_suami_hp, " _
                                                & "  soa_anak_nama_1, " _
                                                & "  soa_anak_tgl_lahir_1, " _
                                                & "  soa_anak_sekolah_1, " _
                                                & "  soa_anak_nama_2, " _
                                                & "  soa_anak_tgl_lahir_2, " _
                                                & "  soa_anak_sekolah_2, " _
                                                & "  soa_anak_nama_3, " _
                                                & "  soa_anak_tgl_lahir_3, " _
                                                & "  soa_anak_sekolah_3, " _
                                                & "  soa_keluarga_dekat_nama, " _
                                                & "  soa_keluarga_dekat_alamat_1, " _
                                                & "  soa_keluarga_dekat_alamat_2, " _
                                                & "  soa_keluarga_dekat_telp, " _
                                                & "  soa_keluarga_dekat_hp, " _
                                                & "  soa_status_tempat_tinggal, " _
                                                & "  soa_jenis_kartu_kredit, " _
                                                & "  soa_no_kartu_kredit, " _
                                                & "  soa_berlaku_sd, " _
                                                & "  soa_dt, " _
                                                & "  soa_bank, " _
                                                & "  soa_so_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(soa_kjb_code.Text) & ",  " _
                                                & SetSetring(soa_bekerja_pada.Text) & ",  " _
                                                & SetSetring(soa_jabatan_bagian.Text) & ",  " _
                                                & SetSetring(soa_kantor_alamat_1.Text) & ",  " _
                                                & SetSetring(soa_kantor_alamat_2.Text) & ",  " _
                                                & SetSetring(soa_kantor_lantai.Text) & ",  " _
                                                & SetSetring(soa_kantor_telp.Text) & ",  " _
                                                & SetSetring(soa_ktp.Text) & ",  " _
                                                & SetSetring(soa_email.Text) & ",  " _
                                                & SetSetring(soa_rumah_alamat_1.Text) & ",  " _
                                                & SetSetring(soa_rumah_alamat_2.Text) & ",  " _
                                                & SetSetring(soa_rumah_kode_pos.Text) & ",  " _
                                                & SetSetring(soa_rumah_telp.Text) & ",  " _
                                                & SetSetring(soa_rumah_hp.Text) & ",  " _
                                                & SetBitYN(soa_status_alamat_kirim.EditValue) & ",  " _
                                                & SetBitYN(soa_status_alamat_tagih.EditValue) & ",  " _
                                                & SetSetring(soa_suami_nama.Text) & ",  " _
                                                & SetSetring(soa_suami_bekerja.Text) & ",  " _
                                                & SetSetring(soa_suami_jabatan.Text) & ",  " _
                                                & SetSetring(soa_suami_kantor_alamat_1.Text) & ",  " _
                                                & SetSetring(soa_suami_kantor_alamat_2.Text) & ",  " _
                                                & SetSetring(soa_suami_telp.Text) & ",  " _
                                                & SetSetring(soa_suami_hp.Text) & ",  " _
                                                & SetSetring(soa_anak_nama_1.Text) & ",  " _
                                                & SetDate(soa_anak_tgl_lahir_1.DateTime) & ",  " _
                                                & SetSetring(soa_anak_sekolah_1.Text) & ",  " _
                                                & SetSetring(soa_anak_nama_2.Text) & ",  " _
                                                & SetDate(soa_anak_tgl_lahir_2.DateTime) & ",  " _
                                                & SetSetring(soa_anak_sekolah_2.Text) & ",  " _
                                                & SetSetring(soa_anak_nama_3.Text) & ",  " _
                                                & SetDate(soa_anak_tgl_lahir_3.DateTime) & ",  " _
                                                & SetSetring(soa_anak_sekolah_3.Text) & ",  " _
                                                & SetSetring(soa_keluarga_dekat_nama.Text) & ",  " _
                                                & SetSetring(soa_keluarga_dekat_alamat_1.Text) & ",  " _
                                                & SetSetring(soa_keluarga_dekat_alamat_2.Text) & ",  " _
                                                & SetSetring(soa_keluarga_dekat_telp.Text) & ",  " _
                                                & SetSetring(soa_keluarga_dekat_hp.Text) & ",  " _
                                                & SetBitYN(soa_status_tempat_tinggal.EditValue) & ",  " _
                                                & SetSetring(soa_jenis_kartu_kredit.Text) & ",  " _
                                                & SetSetring(soa_no_kartu_kredit.Text) & ",  " _
                                                & SetSetring(soa_berlaku_sd.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(soa_bank.Text) & ",  " _
                                                & SetSetring(_so_oid.ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        'If so_type.EditValue.ToString.ToUpper = "D" Then
                        '    If insert_sokp_piutang(objinsert, _so_oid.ToString, _so_code, _total_dp, _total_payment) = False Then
                        '        sqlTran.Rollback()
                        '        insert = False
                        '        Exit Function
                        '    End If
                        'End If


                        'apabila chash maka langsung bisa generate sales order shipment....
                        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
                            If insert_shipment(objinsert, _so_oid.ToString, _so_code) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update so_mstr set so_close_date = current_date " _
                                                 & " where so_code = " + SetSetring(_so_code)
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        '' ''Update so mstr so_status = null apabila terjadi perubahan qty atau edit data so nambah line
                        ' ''.Command.CommandType = CommandType.Text
                        ' ''.Command.CommandText = "update so_mstr set so_close_date = null " _
                        ' ''                     & " where so_code = coalesce((select distinct so_code from sod_det " _
                        ' ''                     & " inner join so_mstr on so_oid = sod_so_oid " _
                        ' ''                     & " where sod_so_oid = '" + _so_oid.ToString + "'" _
                        ' ''                     & " and  sod_qty <> coalesce(sod_qty_shipment,0)),'') "
                        ' ''ssqls.Add(.Command.CommandText)
                        ' ''.Command.ExecuteNonQuery()
                        ' ''.Command.Parameters.Clear()

                        If _conf_value = "1" Then
                            If ds_bantu.Tables(0).Rows.Count = 0 Then
                                Box("Data Aprove empty")
                            End If
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
                                                        & SetInteger(so_en_id.EditValue) & ",  " _
                                                        & SetSetring(so_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_so_oid.ToString) & ",  " _
                                                        & SetSetring(_so_code) & ",  " _
                                                        & SetSetring("Sales Order") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, so_en_id.EditValue, 10, _so_oid.ToString, _so_code, so_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        If so_manufacture.EditValue = True Then

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
                                                & "  pjc_active,pjc_so_oid,pjc_wo_process,pjc_cancel, " _
                                                & "  pjc_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(so_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(func_coll.GetID("pjc_mstr", so_en_id.GetColumnValue("en_code"), "pjc_id", "pjc_en_id", so_en_id.EditValue.ToString)) & ",  " _
                                                & SetSetring(_so_code) & ",  " _
                                                & "current_date" & ",  " _
                                                & SetSetring(_so_code) & ",  " _
                                                & SetSetring("Y") & ",  " _
                                                & SetSetring(_so_oid.ToString) & ",'N','N',  " _
                                                & "current_timestamp" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If



                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SO " & _so_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
                            'update rekonsiliasi kas masuk
                            If update_rec(objinsert, ssqls, so_en_id.EditValue, so_bk_id.EditValue, so_cu_id.EditValue, _
                                so_exc_rate.EditValue, _so_total + _so_total_ppn + _so_total_pph, so_date.DateTime, _so_code, _
                                so_ptnr_id_sold.Text, "SO CASH") = False Then
                                Return False
                                Exit Function
                            End If
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

                        after_success()
                        set_row(_so_oid.ToString, "so_oid")
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

    Private Function insert_shipment(ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_so_code As String) As Boolean
        insert_shipment = True
        Dim _soship_oid As Guid
        Dim _soship_code, _serial, _pt_code As String
        'Dim _cost_methode As String 
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ds_bantu_ry As New DataSet

        _soship_oid = Guid.NewGuid

        _soship_code = func_coll.get_transaction_number("SS", so_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-so")
        If _tran_id = -1 Then
            MessageBox.Show("Sales Order Shipment In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        With par_obj
            .Command.CommandType = CommandType.Text
            .Command.CommandText = "INSERT INTO  " _
                                & "  public.soship_mstr " _
                                & "( " _
                                & "  soship_oid, " _
                                & "  soship_dom_id, " _
                                & "  soship_en_id, " _
                                & "  soship_add_by, " _
                                & "  soship_add_date, " _
                                & "  soship_code, " _
                                & "  soship_date, " _
                                & "  soship_so_oid, " _
                                & "  soship_si_id, " _
                                & "  soship_is_shipment, " _
                                & "  soship_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_soship_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(so_en_id.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                & SetSetring(_soship_code) & ",  " _
                                & SetDate(so_date.DateTime) & ",  " _
                                & SetSetring(par_so_oid.ToString) & ",  " _
                                & SetInteger(so_si_id.EditValue) & ",  " _
                                & SetSetring("Y") & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                & ")"
            ssqls.Add(.Command.CommandText)
            .Command.ExecuteNonQuery()
            .Command.Parameters.Clear()

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.soshipd_det " _
                                    & "( " _
                                    & "  soshipd_oid, " _
                                    & "  soshipd_soship_oid, " _
                                    & "  soshipd_sod_oid, " _
                                    & "  soshipd_seq, " _
                                    & "  soshipd_qty, " _
                                    & "  soshipd_um, " _
                                    & "  soshipd_um_conv, " _
                                    & "  soshipd_qty_real, " _
                                    & "  soshipd_si_id, " _
                                    & "  soshipd_loc_id, " _
                                    & "  soshipd_lot_serial, " _
                                    & "  soshipd_rea_code_id, " _
                                    & "  soshipd_dt " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_soship_oid.ToString) & ",  " _
                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_oid").ToString) & ",  " _
                                    & SetInteger(i) & ",  " _
                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty") * -1) & ",  " _
                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um")) & ",  " _
                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_um_conv")) & ",  " _
                                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty_real") * -1) & ",  " _
                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_si_id")) & ",  " _
                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_loc_id")) & ",  " _
                                    & SetSetringDB("") & ",  " _
                                    & " null " & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                    & ")"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                'Update sod_det
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "update sod_det set sod_qty_shipment = coalesce(sod_qty_shipment,0) + " + ds_edit.Tables(0).Rows(i).Item("sod_qty").ToString _
                                     & " where sod_oid = '" + ds_edit.Tables(0).Rows(i).Item("sod_oid").ToString + "'"
                ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
            Next

            'Update Table Inventory Dan Cost Inventory Dan History Inventory
            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
            i_2 = 0
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                    If ds_edit.Tables(0).Rows(i).Item("sod_qty_real") > 0 Then
                        If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                            i_2 += 1

                            _en_id = ds_edit.Tables(0).Rows(i).Item("sod_en_id")
                            _si_id = ds_edit.Tables(0).Rows(i).Item("sod_si_id")
                            _loc_id = ds_edit.Tables(0).Rows(i).Item("sod_loc_id")
                            _pt_id = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
                            _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                            _serial = "''"
                            _qty = ds_edit.Tables(0).Rows(i).Item("sod_qty_real")
                            If func_coll.update_invc_mstr_minus(ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                Return False
                            End If

                            'Update History Inventory                        
                            _qty = _qty * -1
                            _cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (ds_edit.Tables(0).Rows(i).Item("sod_cost") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
                            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                            If func_coll.update_invh_mstr(ssqls, par_obj, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Shipment", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", so_date.DateTime) = False Then
                                Return False
                            End If
                        Else
                            MessageBox.Show("Error Serial..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        End If
                    End If
                End If
            Next

            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
            'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            '    If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
            '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("sod_pt_id").ToString.ToUpper)
            '        _en_id = ds_edit.Tables(0).Rows(i).Item("sod_en_id")
            '        _si_id = ds_edit.Tables(0).Rows(i).Item("sod_si_id")
            '        _pt_id = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
            '        _qty = ds_edit.Tables(0).Rows(i).Item("sod_qty_real")
            '        _cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (ds_edit.Tables(0).Rows(i).Item("sod_cost") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))

            '        If _cost_methode = "F" Or _cost_methode = "L" Then
            '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
            '            Return False
            '            'If func_coll.update_invct_table_minus(ssqls, par_obj, _en_id, _pt_id, _qty, _cost_methode) = False Then
            '            '    Return False
            '            'End If
            '        ElseIf _cost_methode = "A" Then
            '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
            '            If func_coll.update_item_cost_avg(ssqls, par_obj, _si_id, _pt_id, _cost_avg) = False Then
            '                Return False
            '            End If
            '        End If
            '    End If
            'Next

            ' TIdak jadi karena sudah ada di menu khusus untuk penghitungan royalti
            ' ''Update Ke Table Royalti 'soshipd_qty
            ''For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            ''    _soshipd_qty_real = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")

            ''    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
            ''        Try
            ''            Using objcb As New master_new.WDABasepgsql("", "")
            ''                With objcb
            ''                    .SQL = "select royt_oid, royt_pt_id, royt_seq, royt_qty_royalti - royt_qty_so as royt_qty_open " + _
            ''                       " from royt_table " + _
            ''                       " where royt_qty_royalti > royt_qty_so " + _
            ''                       " and royt_pt_id  = " + ds_edit.Tables(0).Rows(i).Item("pt_id").ToString + _
            ''                       " order by royt_seq "
            ''                    .InitializeCommand()
            ''                    .FillDataSet(ds_bantu_ry, "royalti")
            ''                End With
            ''            End Using
            ''        Catch ex As Exception
            ''            MessageBox.Show(ex.Message)
            ''        End Try

            ''        For j = 0 To ds_bantu_ry.Tables(0).Rows.Count - 1
            ''            If _soshipd_qty_real > ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open") Then
            ''                .Command.CommandType = CommandType.Text
            ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open").ToString _
            ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
            ''                ssqls.Add(.Command.CommandText)
            ''                .Command.ExecuteNonQuery()
            ''                .Command.Parameters.Clear()

            ''                _soshipd_qty_real = _soshipd_qty_real - ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open")
            ''            Else
            ''                .Command.CommandType = CommandType.Text
            ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + _soshipd_qty_real.ToString _
            ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
            ''                ssqls.Add(.Command.CommandText)
            ''                .Command.ExecuteNonQuery()
            ''                .Command.Parameters.Clear()
            ''                Exit For 'karena nilai _shosipd_qty_real sudah habis...
            ''            End If
            ''        Next
            ''    End If
            ''Next
            ' ''**********************************************************
            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

            If _create_jurnal = True Then
                If glt_det_so_shipment(ssqls, par_obj, ds_edit, _soship_oid.ToString, _soship_code) = False Then
                    Return False
                End If

                If jurnal_payment(par_obj, par_so_oid, par_so_code) = False Then
                    Return False
                End If
            End If
        End With
    End Function

    Private Function glt_det_so_shipment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, ByVal par_soship_oid As String, ByVal par_soship_code As String) As Boolean
        glt_det_so_shipment = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _date As Date = so_date.DateTime
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number("IC", so_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("sod_qty") > 0 Then
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("sod_pt_id"))
                        '_cost = par_ds.Tables(0).Rows(i).Item("sod_qty") * (par_ds.Tables(0).Rows(i).Item("sod_cost") - (par_ds.Tables(0).Rows(i).Item("sod_cost") * par_ds.Tables(0).Rows(i).Item("sod_disc")))
                        _cost = par_ds.Tables(0).Rows(i).Item("sod_qty") * (par_ds.Tables(0).Rows(i).Item("sod_cost"))

                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "SL_CMACC"))

                        'Insert Untuk Yang Debet

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
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("sod_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(so_cu_id.EditValue) & ",  " _
                                            & SetDbl(so_exc_rate.EditValue) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("SO Shipment") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_soship_oid) & ",  " _
                                            & SetSetring(par_soship_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("IC-SOS") & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("sod_en_id"), so_cu_id.EditValue, _
                                                         so_exc_rate.EditValue, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    End If

                    _seq = _seq + 1

                    dt_bantu = New DataTable
                    dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))

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
                                        & SetInteger(par_ds.Tables(0).Rows(i).Item("sod_en_id")) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(so_cu_id.EditValue) & ",  " _
                                        & SetDbl(so_exc_rate.EditValue) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("SO Shipment") & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetSetring(par_soship_oid) & ",  " _
                                        & SetSetring(par_soship_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("IC-SOS") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     par_ds.Tables(0).Rows(i).Item("sod_en_id"), so_cu_id.EditValue, _
                                                     so_exc_rate.EditValue, _cost, "C") = False Then

                        Return False
                        Exit Function
                    End If
                    '********************** finish untuk yang credit
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Private Function jurnal_payment(ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_so_code As String) As Boolean
        jurnal_payment = True
        'buat struktur dulu datasetnya...dengan data yang kosong
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
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer

        Dim _dtrow As DataRow
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _dtrow = ds_edit_shipment.Tables(0).NewRow
            _dtrow("ars_oid") = Guid.NewGuid.ToString
            _dtrow("ceklist") = True
            _dtrow("ars_soshipd_oid") = par_so_oid 'ini hanya formalitas saja
            _dtrow("soship_code") = par_so_code ''ini hanya formalitas saja
            _dtrow("pt_id") = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
            _dtrow("pt_code") = ds_edit.Tables(0).Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = ds_edit.Tables(0).Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = ds_edit.Tables(0).Rows(i).Item("pt_desc2")
            _dtrow("ars_taxable") = ds_edit.Tables(0).Rows(i).Item("sod_taxable")
            _dtrow("ars_tax_class_id") = ds_edit.Tables(0).Rows(i).Item("sod_tax_class")
            _dtrow("taxclass_name") = ds_edit.Tables(0).Rows(i).Item("sod_tax_class_name")
            _dtrow("ars_tax_inc") = ds_edit.Tables(0).Rows(i).Item("sod_tax_inc")
            _dtrow("ars_open") = ds_edit.Tables(0).Rows(i).Item("sod_qty")
            _dtrow("ars_invoice") = ds_edit.Tables(0).Rows(i).Item("sod_qty")
            _dtrow("ars_so_price") = ds_edit.Tables(0).Rows(i).Item("sod_price")
            _dtrow("ars_so_disc_value") = ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc")
            '_dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sod_price") - (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sod_price") '- (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_close_line") = "Y"
            ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit_shipment.Tables(0).AcceptChanges()

        'pindah kan ke table distribution
        Dim j As Integer

        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
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
                    _search = False
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
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                        Else
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                            (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                             ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price"))
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
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            _dtrow("ard_amount") = _invoice_price
                        Else
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
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
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    'Mencari taxrate account ar untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                    '1. PPN
                    _search = False
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
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
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
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _ppn
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If

                    '1. PPH
                    _search = False
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
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
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
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
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
                        _search = False
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
                        _search = False
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
                        _search = False
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
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
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
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
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

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        Dim _bk_ac_id As Integer
        Dim _bk_ac_name As String = ""
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_ac_id, ac_name from bk_mstr inner join ac_mstr on ac_id = bk_ac_id where bk_id = " + so_bk_id.EditValue.ToString
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bk_ac_id = .DataReader("bk_ac_id").ToString
                        _bk_ac_name = .DataReader("ac_name").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Dim _ard_total_amount As Double = 0

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _ard_total_amount = _ard_total_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        Next

        If insert_glt_det_ar(ssqls, par_obj, ds_edit_dist, _
                                                       so_en_id.EditValue, so_en_id.GetColumnValue("en_code"), _
                                                       par_so_oid.ToString, par_so_code, _
                                                       so_date.DateTime, _
                                                       so_cu_id.EditValue, so_exc_rate.EditValue, _
                                                       "AY", "AR-PAY", _
                                                       _bk_ac_id, 0, 0, _
                                                       _bk_ac_name, _ard_total_amount) = False Then

            Return False
        End If

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

        'Return False
        'Exit Function
        insert_glt_det_ar = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

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
                                            & SetInteger(_seq) & ",  " _
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
                                            & SetInteger(_seq) & ",  " _
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

    Private Function insert_sokp_piutang(ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_ref As String, ByVal par_dp As Double, ByVal par_amount As Double) As Boolean
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
                                           " and code_id = " + so_pay_type.EditValue.ToString
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

        If so_payment_date.Text = "" Then
            MessageBox.Show("Payment Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _date As Date = so_payment_date.DateTime

        With par_obj
            Try
                'Untuk Insert Yang DP
                'DP harus masuk juga ke kartu piutang....agar pada saat ar jadi pas..
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.sokp_piutang " _
                                    & "( " _
                                    & "  sokp_oid, " _
                                    & "  sokp_so_oid, " _
                                    & "  sokp_seq, " _
                                    & "  sokp_ref, " _
                                    & "  sokp_amount, " _
                                    & "  sokp_due_date, " _
                                    & "  sokp_amount_pay, " _
                                    & "  sokp_description " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(par_so_oid) & ",  " _
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
                                        & "  sokp_so_oid, " _
                                        & "  sokp_seq, " _
                                        & "  sokp_ref, " _
                                        & "  sokp_amount, " _
                                        & "  sokp_due_date, " _
                                        & "  sokp_amount_pay, " _
                                        & "  sokp_description " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(par_so_oid) & ",  " _
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
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pay_interval") = 0 Then
            MessageBox.Show("Can't Edit Sales Order Cash Transaction...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim i As Integer
        For i = 0 To ds.Tables("detail").Rows.Count - 1
            If Not IsDBNull(ds.Tables("detail").Rows(i).Item("sod_qty_shipment")) = True Then
                If ds.Tables("detail").Rows(i).Item("sod_qty_shipment") > 0 Then
                    MessageBox.Show("Data already shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next

        ' gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _so_oid_mstr = .Item("so_oid").ToString
                _so_code_mstr = .Item("so_code")
                so_project.EditValue = SetBitYNB(.Item("so_project"))
                so_ar_sb_id.EditValue = .Item("so_ar_sb_id")
                so_ar_ac_id.EditValue = .Item("so_ar_ac_id")
                so_ar_cc_id.EditValue = .Item("so_ar_cc_id")
                so_credit_term.EditValue = .Item("so_credit_term")
                so_cu_id.EditValue = .Item("so_cu_id")

                If IsDBNull(.Item("so_bk_id")) = True Then
                    so_bk_id.ItemIndex = 0
                Else
                    so_bk_id.EditValue = .Item("so_bk_id")
                End If

                so_sales_program.EditValue = .Item("so_sales_program")
                so_en_id.EditValue = .Item("so_en_id")
                so_date.DateTime = .Item("so_date")
                so_exc_rate.EditValue = .Item("so_exc_rate")
                so_pay_method.EditValue = .Item("so_pay_method")
                so_cons.EditValue = SetBitYNB(.Item("so_cons"))
                so_pay_type.EditValue = .Item("so_pay_type")
                so_payment_date.DateTime = .Item("so_payment_date")

                _so_ptnr_id_sold_mstr = SetString(.Item("so_ptnr_id_sold"))
                so_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
                so_ref_po_code.Text = SetString(.Item("so_ref_po_code"))
                _so_ref_po_oid = SetString(.Item("so_ref_po_oid"))
                so_psn_ref_oid.Text = SetString(.Item("so_psn_ref_code"))
                so_parent_oid.Tag = SetString(.Item("so_parent_oid"))
                so_parent_oid.EditValue = .Item("so_parent_code")

                If _so_ref_po_oid <> "" Then
                    so_ref_po_code.Enabled = False
                Else
                    so_ref_po_code.Enabled = True
                End If

                so_shipping_address.Text = Trim(.Item("ptnra_line_1") + " " + .Item("ptnra_line_2") + " " + .Item("ptnra_line_3"))
                so_sales_person.EditValue = .Item("so_sales_person")
                so_si_id.EditValue = .Item("so_si_id")
                so_tax_class.EditValue = .Item("so_tax_class")
                so_tax_inc.EditValue = SetBitYNB(.Item("so_tax_inc"))

                If IsDBNull(.Item("so_ppn_type")) = True Then
                    so_ppn_type.Text = ""
                ElseIf .Item("so_ppn_type") = "PPN Bebas" Then
                    so_ppn_type.EditValue = "E"
                ElseIf .Item("so_ppn_type") = "PPN Bayar" Then
                    so_ppn_type.EditValue = "A"
                End If

                so_taxable.EditValue = SetBitYNB(.Item("so_taxable"))
                so_tran_id.EditValue = .Item("so_tran_id")
                so_trans_rmks.Text = SetString(.Item("so_trans_rmks"))
                so_type.EditValue = .Item("so_type")

                so_is_package.EditValue = SetBitYNB(.Item("so_is_package"))
                so_pt_id.Tag = SetString(.Item("so_pt_id"))
                so_pt_id.EditValue = .Item("pt_code")
                pt_desc1.EditValue = .Item("pt_desc1")
                pt_desc2.EditValue = .Item("pt_desc2")
                so_price.EditValue = .Item("so_price")
                so_manufacture.EditValue = SetBitYNB(.Item("so_manufacture"))

                If so_is_package.EditValue = True Then
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
                    gv_edit.BestFitColumns()
                Else
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                    gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = True
                    gv_edit.BestFitColumns()
                End If
                so_pi_id.EditValue = .Item("so_pi_id")
            End With
            so_en_id.Focus()
            so_cu_id.Enabled = False
            so_ptnr_id_sold.Enabled = False
            so_shipping_address.Enabled = True
            so_pay_type.Enabled = False
            so_type.Enabled = False
            so_pi_id.Enabled = True
            so_ppn_type.Enabled = False

            Try
                tcg_header.SelectedTabPageIndex = 0
                tcg_detail.SelectedTabPageIndex = 0
                tcg_customer.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
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
                            & "  sod_qty_allocated, " _
                            & "  sod_qty_picked, " _
                            & "  sod_qty_shipment, " _
                            & "  sod_qty_pending_inv, " _
                            & "  sod_qty_invoice, " _
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
                            & "  sod_ppn_type, " _
                            & "  sod_status, " _
                            & "  sod_dt, " _
                            & "  sod_payment, " _
                            & "  sod_dp, " _
                            & "  sod_sales_unit, " _
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_type, " _
                            & "  sod_sales_unit_total, sod_commision, sod_commision_total, sod_sod_parent_oid, " _
                            & "  pt_ls, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  loc_desc, sod_pod_oid,coalesce((select count(*) as jml from soshipd_det where soshipd_sod_oid=sod_oid),0) as status_shipment " _
                            & "FROM  " _
                            & "  public.sod_det " _
                            & "  inner join so_mstr on so_oid = sod_so_oid " _
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
                            & " where public.sod_det.sod_so_oid = '" + ds.Tables(0).Rows(row).Item("so_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")


                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        .SQL = "SELECT  " _
                        & "  a.sodas_oid, " _
                        & "  a.sodas_so_oid, " _
                        & "  a.sodas_pt_id_sod, " _
                        & "  b.pt_code as pt_code_parent, " _
                        & "  b.pt_desc1 as pt_desc1_parent, " _
                        & "  b.pt_desc2 as pt_desc2_parent, " _
                        & "  a.sodas_pt_id, " _
                        & "  c.pt_code, " _
                        & "  c.pt_desc1, " _
                        & "  c.pt_desc2, " _
                        & "  a.sodas_qty,sodas_qty_sold, " _
                        & "  a.sodas_sod_oid " _
                        & "FROM " _
                        & "  public.sodas_assembly a " _
                        & "  INNER JOIN public.pt_mstr b ON (a.sodas_pt_id_sod = b.pt_id) " _
                        & "  INNER JOIN public.pt_mstr c ON (a.sodas_pt_id = c.pt_id) " _
                        & "WHERE " _
                        & "  a.sodas_so_oid is null " _
                        & "ORDER BY " _
                        & "  a.sodas_pt_id_sod, " _
                        & "  a.sodas_pt_id"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_assembly")
                        gc_edit_assembly.DataSource = ds_edit.Tables(1)
                        gv_edit_assembly.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If

        If ds.Tables("detail_attr").Rows.Count = 0 Then
            _soa_oid_mstr = ""
        Else
            row = BindingContext(ds.Tables("detail_attr")).Position

            With ds.Tables("detail_attr").Rows(row)
                _soa_oid_mstr = .Item("soa_oid").ToString
                soa_kjb_code.Text = SetString(.Item("soa_kjb_code"))
                soa_anak_nama_1.Text = SetString(.Item("soa_anak_nama_1"))
                soa_anak_nama_2.Text = SetString(.Item("soa_anak_nama_2"))
                soa_anak_nama_3.Text = SetString(.Item("soa_anak_nama_3"))
                soa_anak_sekolah_1.Text = SetString(.Item("soa_anak_sekolah_1"))
                soa_anak_sekolah_2.Text = SetString(.Item("soa_anak_sekolah_2"))
                soa_anak_sekolah_3.Text = SetString(.Item("soa_anak_sekolah_3"))
                soa_anak_tgl_lahir_1.DateTime = SetString(.Item("soa_anak_tgl_lahir_1"))
                soa_anak_tgl_lahir_2.DateTime = SetString(.Item("soa_anak_tgl_lahir_2"))
                soa_anak_tgl_lahir_3.DateTime = SetString(.Item("soa_anak_tgl_lahir_3"))
                soa_bank.Text = SetString(.Item("soa_bank"))
                soa_bekerja_pada.Text = SetString(.Item("soa_bekerja_pada"))
                soa_berlaku_sd.Text = SetString(.Item("soa_berlaku_sd"))
                soa_email.Text = SetString(.Item("soa_email"))
                soa_jabatan_bagian.Text = SetString(.Item("soa_jabatan_bagian"))
                soa_jenis_kartu_kredit.Text = SetString(.Item("soa_jenis_kartu_kredit"))
                soa_kantor_alamat_1.Text = SetString(.Item("soa_kantor_alamat_1"))
                soa_kantor_alamat_2.Text = SetString(.Item("soa_kantor_alamat_2"))
                soa_kantor_lantai.Text = SetString(.Item("soa_kantor_lantai"))
                soa_kantor_telp.Text = SetString(.Item("soa_kantor_telp"))
                soa_keluarga_dekat_alamat_1.Text = SetString(.Item("soa_keluarga_dekat_alamat_1"))
                soa_keluarga_dekat_alamat_2.Text = SetString(.Item("soa_keluarga_dekat_alamat_2"))
                soa_keluarga_dekat_hp.Text = SetString(.Item("soa_keluarga_dekat_hp"))
                soa_keluarga_dekat_nama.Text = SetString(.Item("soa_keluarga_dekat_nama"))
                soa_keluarga_dekat_telp.Text = SetString(.Item("soa_keluarga_dekat_telp"))
                soa_ktp.Text = SetString(.Item("soa_ktp"))
                soa_no_kartu_kredit.Text = SetString(.Item("soa_no_kartu_kredit"))
                soa_rumah_alamat_1.Text = SetString(.Item("soa_rumah_alamat_1"))
                soa_rumah_alamat_2.Text = SetString(.Item("soa_rumah_alamat_2"))
                soa_rumah_hp.Text = SetString(.Item("soa_rumah_hp"))
                soa_rumah_kode_pos.Text = SetString(.Item("soa_rumah_kode_pos"))
                soa_rumah_telp.Text = SetString(.Item("soa_rumah_telp"))
                soa_status_alamat_kirim.EditValue = SetBitYNB(.Item("soa_status_alamat_kirim"))
                soa_status_alamat_tagih.EditValue = SetBitYNB(.Item("soa_status_alamat_tagih"))
                soa_status_tempat_tinggal.EditValue = SetBitYNB(.Item("soa_status_tempat_tinggal"))
                soa_suami_bekerja.Text = SetString(.Item("soa_suami_bekerja"))
                soa_suami_hp.Text = SetString(.Item("soa_suami_hp"))
                soa_suami_jabatan.Text = SetString(.Item("soa_suami_jabatan"))
                soa_suami_kantor_alamat_1.Text = SetString(.Item("soa_suami_kantor_alamat_1"))
                soa_suami_kantor_alamat_2.Text = SetString(.Item("soa_suami_kantor_alamat_2"))
                soa_suami_nama.Text = SetString(.Item("soa_suami_nama"))
                soa_suami_telp.Text = SetString(.Item("soa_suami_telp"))
            End With
        End If
        cek_shipment_row()
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _so_total, _sod_qty, _sod_price, _sod_disc, _so_total_ppn, _so_total_pph, _so_total_temp, _tax_rate As Double
        Dim _sod_qty_shipment As Double = 0
        Dim i As Integer
        Dim _so_terbilang As String

        Dim _so_trn_status As String
        Dim ds_bantu As New DataSet
        _so_trn_status = "D" 'set default langsung ke D
        ds_bantu = func_data.load_aprv_mstr(so_tran_id.EditValue)

        ssqls.Clear()

        '******* Mencari Total so, Total PPN, Total PPH
        _so_total = 0
        _so_total_ppn = 0
        _so_total_pph = 0
        _so_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
            Else
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
            End If

            _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
            _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

            _so_total = _so_total + ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
                Else
                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
                End If

                _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
                _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

                _so_total_temp = ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
                _so_total_ppn = _so_total_ppn + (_so_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                _so_total_pph = _so_total_pph + (_so_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
            End If
        Next
        '*******************************************************

        _so_terbilang = func_bill.TERBILANG_FIX(_so_total + _so_total_ppn - _so_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("sod_dp") * ds_edit.Tables(0).Rows(i).Item("sod_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("sod_payment") * ds_edit.Tables(0).Rows(i).Item("sod_qty"))
        Next
        '*************************

        ds_edit.Tables(1).Rows.Clear()

        Dim dt_pt As New DataTable
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            Dim ssql As String

            ssql = "SELECT  " _
             & "  b.psd_pt_bom_id, " _
             & "  c.pt_code, " _
             & "  c.pt_desc1, " _
             & "  c.pt_desc2, " _
             & "  sum(b.psd_qty) as psd_qty " _
             & "FROM " _
             & "  public.psd_det b " _
             & "  INNER JOIN public.pt_mstr c ON (b.psd_pt_bom_id = c.pt_id) " _
             & "  INNER JOIN public.ps_mstr a ON (b.psd_ps_oid = a.ps_oid) " _
             & "WHERE " _
             & "  a.ps_pt_bom_id =" & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) _
             & " group by psd_pt_bom_id,pt_code,pt_desc1,pt_desc2"

            dt_pt = master_new.PGSqlConn.GetTableData(ssql)

            Dim _dtrow As DataRow
            For Each dr As DataRow In dt_pt.Rows
                _dtrow = ds_edit.Tables(1).NewRow
                _dtrow("sodas_so_oid") = _so_oid_mstr.ToString
                _dtrow("sodas_sod_oid") = ds_edit.Tables(0).Rows(i).Item("sod_oid").ToString
                _dtrow("sodas_pt_id_sod") = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
                _dtrow("pt_code_parent") = ds_edit.Tables(0).Rows(i).Item("pt_code")
                _dtrow("pt_desc1_parent") = ds_edit.Tables(0).Rows(i).Item("pt_desc1")
                _dtrow("pt_desc2_parent") = ds_edit.Tables(0).Rows(i).Item("pt_desc2")

                _dtrow("sodas_pt_id") = dr("psd_pt_bom_id")
                _dtrow("pt_code") = dr("pt_code")
                _dtrow("pt_desc1") = dr("pt_desc1")
                _dtrow("pt_desc2") = dr("pt_desc2")
                _dtrow("sodas_qty") = dr("psd_qty")
                _dtrow("sodas_qty_sold") = dr("psd_qty") * SetNumber(ds_edit.Tables(0).Rows(i).Item("sod_qty"))

                ds_edit.Tables(1).Rows.Add(_dtrow)
            Next

            ds_edit.Tables(1).AcceptChanges()
            gv_edit_assembly.BestFitColumns()

        Next

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
                                            & "  public.so_mstr   " _
                                            & "SET  " _
                                            & "  so_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  so_en_id = " & SetInteger(so_en_id.EditValue) & ",  " _
                                            & "  so_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  so_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  so_date = " & SetDate(so_date.DateTime) & ",  " _
                                            & "  so_ptnr_id_sold = " & SetInteger(_so_ptnr_id_sold_mstr) & ",  " _
                                            & "  so_ref_po_code = " & SetSetring(so_ref_po_code.Text) & ",  " _
                                            & "  so_ref_po_oid = " & SetSetring(_so_ref_po_oid) & ",  " _
                                            & "  so_psn_ref_code = " & SetSetring(so_psn_ref_oid.Text) & ",  " _
                                            & "  so_credit_term = " & SetInteger(so_credit_term.EditValue) & ",  " _
                                            & "  so_taxable = " & SetBitYN(so_taxable.EditValue) & ",  " _
                                            & "  so_project = " & SetBitYN(so_project.EditValue) & ",  " _
                                            & "  so_tax_class = " & SetInteger(so_tax_class.EditValue) & ",  " _
                                            & "  so_ppn_type = " & SetSetring(so_ppn_type.EditValue) & ",  " _
                                            & "  so_si_id = " & SetInteger(so_si_id.EditValue) & ",  " _
                                            & "  so_sales_person = " & SetInteger(so_sales_person.EditValue) & ",  " _
                                            & "  so_pay_method = " & SetInteger(so_pay_method.EditValue) & ",  " _
                                            & "  so_cons = " & SetBitYN(so_cons.EditValue) & ",  " _
                                            & "  so_ar_ac_id = " & SetInteger(so_ar_ac_id.EditValue) & ",  " _
                                            & "  so_ar_sb_id = " & SetInteger(so_ar_sb_id.EditValue) & ",  " _
                                            & "  so_ar_cc_id = " & SetInteger(so_ar_cc_id.EditValue) & ",  " _
                                            & "  so_dp = " & SetDbl(_total_dp) & ",  " _
                                            & "  so_total = " & SetDbl(_so_total) & ",  " _
                                            & "  so_payment_date = " & SetDate(so_payment_date.DateTime) & ",  " _
                                            & "  so_tran_id = " & SetInteger(so_tran_id.EditValue) & ",  " _
                                            & "  so_trans_id = " & SetSetring(_so_trn_status) & ",  " _
                                            & "  so_bk_id = " & SetInteger(so_bk_id.EditValue) & ",  " _
                                            & "  so_trans_rmks = " & SetSetring(so_trans_rmks.Text) & ",  " _
                                            & "  so_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  so_total_ppn = " & SetDbl(_so_total_ppn) & ",  " _
                                            & "  so_total_pph = " & SetDbl(_so_total_pph) & ",  " _
                                            & "  so_payment = " & SetDbl(_total_payment) & ",  " _
                                            & "  so_exc_rate = " & SetDbl(so_exc_rate.EditValue) & ",  " _
                                            & "  so_tax_inc = " & SetBitYN(so_tax_inc.EditValue) & ",  " _
                                            & "  so_manufacture = " & SetBitYN(so_manufacture.EditValue) & ",  " _
                                            & "  so_is_package = " & SetBitYN(so_is_package.EditValue) & ",  " _
                                            & "  so_pt_id = " & SetInteger(so_pt_id.Tag) & ",  " _
                                            & "  so_price = " & SetDec(so_price.EditValue) & ",  " _
                                            & "  so_sales_program = " & SetSetring(so_sales_program.EditValue) & ",  " _
                                            & "  so_parent_oid = " & SetSetring(so_parent_oid.Tag) & ",  " _
                                            & "  so_parent_code = " & SetSetring(so_parent_oid.EditValue) & ",  " _
                                            & "  so_shipping_address = " & SetSetring(so_shipping_address.EditValue) & ",  " _
                                            & "  so_terbilang = " & SetSetring(_so_terbilang) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  so_oid = " & SetSetring(_so_oid_mstr) & " "



                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_parent_oid")) <> "" Then
                            For Each dr_det As DataRow In ds.Tables("detail").Rows
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  sod_det " _
                                                    & "set sod_qty_child= coalesce(sod_qty_child,0) - " & SetDbl(dr_det("sod_qty")) _
                                                    & " where sod_oid=" & SetSetring(dr_det("sod_sod_parent_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                        End If

                        If SetString(so_psn_ref_oid.Text) <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update  " _
                                                & "  public.pesanan " _
                                                & "set pesan_status='Sudah Proses',pesan_so_code=" & SetSetring(_so_code_mstr) & "  where pesan_code=" & SetSetring(so_psn_ref_oid.Text)

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If


                        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_manufacture") <> IIf(so_manufacture.EditValue = True, "Y", "N") Then
                            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_manufacture") = "Y" Then
                                'cancel
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  public.pjc_mstr " _
                                                    & "set pjc_cancel='Y' where pjc_so_oid=" & SetSetring(_so_oid_mstr.ToString)

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                'insert
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
                                                    & "  pjc_active,pjc_so_oid,pjc_wo_process,pjc_cancel, " _
                                                    & "  pjc_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(so_en_id.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetInteger(func_coll.GetID("pjc_mstr", so_en_id.GetColumnValue("en_code"), "pjc_id", "pjc_en_id", so_en_id.EditValue.ToString)) & ",  " _
                                                    & SetSetring(_so_code_mstr) & ",  " _
                                                    & "current_date" & ",  " _
                                                    & SetSetring(_so_code_mstr) & ",  " _
                                                    & SetSetring("Y") & ",  " _
                                                    & SetSetring(_so_oid_mstr.ToString) & ",'N','N',  " _
                                                    & "current_timestamp" & "  " _
                                                    & ")"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            End If
                        End If

                        'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sod_pod_oid from sod_det where sod_so_oid = " + SetSetring(_so_oid_mstr.ToString) + ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table shipment
                        'kalau sudah relasi ke table shipment jadi nya error...dan harusnya tidak didelete
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from sod_det where coalesce((select count(*) as jml from soshipd_det where soshipd_sod_oid=sod_oid),0) = 0 and sod_so_oid = '" + _so_oid_mstr.ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

                            Dim _so_ppn, _so_pph As Double
                            _so_ppn = 0
                            _so_pph = 0

                            If ds_edit.Tables(0).Rows(i).Item("sod_ppn_type").ToString.ToUpper = "A" Then
                                If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                                    _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class"))
                                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
                                Else
                                    _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
                                End If

                                _sod_qty = ds_edit.Tables(0).Rows(i).Item("sod_qty")
                                _sod_disc = ds_edit.Tables(0).Rows(i).Item("sod_disc")

                                _so_total_temp = ((_sod_qty * _sod_price) - (_sod_qty * _sod_price * _sod_disc))
                                _so_ppn = (_so_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                                _so_pph = (_so_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
                            End If


                            _sod_qty_shipment = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_qty_shipment")) = True, 0, ds_edit.Tables(0).Rows(i).Item("sod_qty_shipment"))
                            If SetNumber(ds_edit.Tables(0).Rows(i).Item("status_shipment")) = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.sod_det " _
                                                & "( " _
                                                & "  sod_oid, " _
                                                & "  sod_dom_id, " _
                                                & "  sod_en_id, " _
                                                & "  sod_add_by, " _
                                                & "  sod_add_date, " _
                                                & "  sod_so_oid, " _
                                                & "  sod_seq, " _
                                                & "  sod_is_additional_charge, " _
                                                & "  sod_si_id, " _
                                                & "  sod_pt_id, " _
                                                & "  sod_rmks, " _
                                                & "  sod_qty, " _
                                                & "  sod_um, " _
                                                & "  sod_cost, " _
                                                & "  sod_price, " _
                                                & "  sod_disc, " _
                                                & "  sod_sales_ac_id, " _
                                                & "  sod_sales_sb_id, " _
                                                & "  sod_sales_cc_id, " _
                                                & "  sod_um_conv, " _
                                                & "  sod_qty_real, " _
                                                & "  sod_taxable, " _
                                                & "  sod_tax_inc, " _
                                                & "  sod_tax_class, " _
                                                & "  sod_ppn_type, " _
                                                & "  sod_dt, " _
                                                & "  sod_payment, " _
                                                & "  sod_dp, " _
                                                & "  sod_loc_id,sod_ppn,sod_pph, sod_commision, sod_commision_total,sod_sod_parent_oid,  " _
                                                & "  sod_sales_unit " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_oid")) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_so_oid_mstr.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_is_additional_charge")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sod_rmks")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_disc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_ppn_type")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_payment")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_dp")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_loc_id")) & ",  " _
                                                & SetDbl(_so_ppn) & ", " _
                                                & SetDbl(_so_pph) & ", " _
                                                 & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_commision")) & ",  " _
                                                  & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_commision_total")) & ",  " _
                                                  & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_sales_unit")) & "  " _
                                                & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.sod_det   " _
                                                    & "SET  " _
                                                    & "  sod_en_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_en_id")) & ",  " _
                                                    & "  sod_is_additional_charge = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_is_additional_charge")) & ",  " _
                                                    & "  sod_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_si_id")) & ",  " _
                                                    & "  sod_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sod_rmks")) & ",  " _
                                                    & "  sod_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) & ",  " _
                                                    & "  sod_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um")) & ",  " _
                                                    & "  sod_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_cost")) & ",  " _
                                                    & "  sod_price = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_price")) & ",  " _
                                                    & "  sod_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_disc")) & ",  " _
                                                    & "  sod_sales_ac_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_ac_id")) & ",  " _
                                                    & "  sod_sales_sb_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_sb_id")) & ",  " _
                                                    & "  sod_sales_cc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_sales_cc_id")) & ",  " _
                                                    & "  sod_um_conv = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_um_conv")) & ",  " _
                                                    & "  sod_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty_real")) & ",  " _
                                                    & "  sod_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_taxable")) & ",  " _
                                                    & "  sod_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_tax_inc")) & ",  " _
                                                    & "  sod_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")) & ",  " _
                                                    & "  sod_ppn_type = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_ppn_type")) & ",  " _
                                                    & "  sod_sod_parent_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid")) & ",  " _
                                                    & "  sod_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                    & "  sod_payment = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_payment")) & ",  " _
                                                     & "  sod_commision = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_commision")) & ",  " _
                                                      & "  sod_commision_total = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_commision_total")) & ",  " _
                                                    & "  sod_dp = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_dp")) & ",  " _
                                                    & "  sod_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_loc_id")) & ",  " _
                                                    & "  sod_ppn= " & SetDbl(_so_ppn) & ", " _
                                                    & "  sod_pph= " & SetDbl(_so_pph) & ", " _
                                                    & "  sod_sales_unit = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_sales_unit")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  sod_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If SetString(so_parent_oid.Tag) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  sod_det " _
                                                    & "set sod_qty_child= coalesce(sod_qty_child,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) _
                                                    & " where sod_oid=" & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_sod_parent_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from sodas_assembly where sodas_so_oid='" & _so_oid_mstr & "'"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(1).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.sodas_assembly " _
                                & "( " _
                                & "  sodas_oid, " _
                                & "  sodas_so_oid, " _
                                & "  sodas_pt_id_sod, " _
                                & "  sodas_pt_id, " _
                                & "  sodas_qty, " _
                                & "  sodas_sod_oid, " _
                                & "  sodas_qty_sold " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(ds_edit.Tables(1).Rows(i).Item("sodas_so_oid")) & ",  " _
                                & SetInteger(ds_edit.Tables(1).Rows(i).Item("sodas_pt_id_sod")) & ",  " _
                                & SetInteger(ds_edit.Tables(1).Rows(i).Item("sodas_pt_id")) & ",  " _
                                & SetDec(ds_edit.Tables(1).Rows(i).Item("sodas_qty")) & ",  " _
                                & SetSetring(ds_edit.Tables(1).Rows(i).Item("sodas_sod_oid")) & ",  " _
                                & SetDec(ds_edit.Tables(1).Rows(i).Item("sodas_qty_sold")) & "  " _
                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If _soa_oid_mstr <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.soa_attr   " _
                                                & "SET  " _
                                                & "  soa_kjb_code = " & SetSetring(soa_kjb_code.Text) & ",  " _
                                                & "  soa_bekerja_pada = " & SetSetring(soa_bekerja_pada.Text) & ",  " _
                                                & "  soa_jabatan_bagian = " & SetSetring(soa_jabatan_bagian.Text) & ",  " _
                                                & "  soa_kantor_alamat_1 = " & SetSetring(soa_kantor_alamat_1.Text) & ",  " _
                                                & "  soa_kantor_alamat_2 = " & SetSetring(soa_kantor_alamat_2.Text) & ",  " _
                                                & "  soa_kantor_lantai = " & SetSetring(soa_kantor_lantai.Text) & ",  " _
                                                & "  soa_kantor_telp = " & SetSetring(soa_kantor_telp.Text) & ",  " _
                                                & "  soa_ktp = " & SetSetring(soa_ktp.Text) & ",  " _
                                                & "  soa_email = " & SetSetring(soa_email.Text) & ",  " _
                                                & "  soa_rumah_alamat_1 = " & SetSetring(soa_rumah_alamat_1.Text) & ",  " _
                                                & "  soa_rumah_alamat_2 = " & SetSetring(soa_rumah_alamat_2.Text) & ",  " _
                                                & "  soa_rumah_kode_pos = " & SetSetring(soa_rumah_kode_pos.Text) & ",  " _
                                                & "  soa_rumah_telp = " & SetSetring(soa_rumah_telp.Text) & ",  " _
                                                & "  soa_rumah_hp = " & SetSetring(soa_rumah_hp.Text) & ",  " _
                                                & "  soa_status_alamat_kirim = " & SetBitYN(soa_status_alamat_kirim.EditValue) & ",  " _
                                                & "  soa_status_alamat_tagih = " & SetBitYN(soa_status_alamat_tagih.EditValue) & ",  " _
                                                & "  soa_suami_nama = " & SetSetring(soa_suami_nama.Text) & ",  " _
                                                & "  soa_suami_bekerja = " & SetSetring(soa_suami_bekerja.Text) & ",  " _
                                                & "  soa_suami_jabatan = " & SetSetring(soa_suami_jabatan.Text) & ",  " _
                                                & "  soa_suami_kantor_alamat_1 = " & SetSetring(soa_suami_kantor_alamat_1.Text) & ",  " _
                                                & "  soa_suami_kantor_alamat_2 = " & SetSetring(soa_suami_kantor_alamat_2.Text) & ",  " _
                                                & "  soa_suami_telp = " & SetSetring(soa_suami_telp.Text) & ",  " _
                                                & "  soa_suami_hp = " & SetSetring(soa_suami_hp.Text) & ",  " _
                                                & "  soa_anak_nama_1 = " & SetSetring(soa_anak_nama_1.Text) & ",  " _
                                                & "  soa_anak_tgl_lahir_1 = " & SetDate(soa_anak_tgl_lahir_1.DateTime) & ",  " _
                                                & "  soa_anak_sekolah_1 = " & SetSetring(soa_anak_sekolah_1.Text) & ",  " _
                                                & "  soa_anak_nama_2 = " & SetSetring(soa_anak_nama_2.Text) & ",  " _
                                                & "  soa_anak_tgl_lahir_2 = " & SetDate(soa_anak_tgl_lahir_2.DateTime) & ",  " _
                                                & "  soa_anak_sekolah_2 = " & SetSetring(soa_anak_sekolah_2.Text) & ",  " _
                                                & "  soa_anak_nama_3 = " & SetSetring(soa_anak_nama_3.Text) & ",  " _
                                                & "  soa_anak_tgl_lahir_3 = " & SetDate(soa_anak_tgl_lahir_3.DateTime) & ",  " _
                                                & "  soa_anak_sekolah_3 = " & SetSetring(soa_anak_sekolah_3.Text) & ",  " _
                                                & "  soa_keluarga_dekat_nama = " & SetSetring(soa_keluarga_dekat_nama.Text) & ",  " _
                                                & "  soa_keluarga_dekat_alamat_1 = " & SetSetring(soa_keluarga_dekat_alamat_1.Text) & ",  " _
                                                & "  soa_keluarga_dekat_alamat_2 = " & SetSetring(soa_keluarga_dekat_alamat_2.Text) & ",  " _
                                                & "  soa_keluarga_dekat_telp = " & SetSetring(soa_keluarga_dekat_telp.Text) & ",  " _
                                                & "  soa_keluarga_dekat_hp = " & SetSetring(soa_keluarga_dekat_hp.Text) & ",  " _
                                                & "  soa_status_tempat_tinggal = " & SetBitYN(soa_status_tempat_tinggal.EditValue) & ",  " _
                                                & "  soa_jenis_kartu_kredit = " & SetSetring(soa_jenis_kartu_kredit.Text) & ",  " _
                                                & "  soa_no_kartu_kredit = " & SetSetring(soa_no_kartu_kredit.Text) & ",  " _
                                                & "  soa_berlaku_sd = " & SetSetring(soa_berlaku_sd.Text) & ",  " _
                                                & "  soa_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                                & "  soa_bank = " & SetSetring(soa_bank.Text) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  soa_oid = " & SetSetring(_soa_oid_mstr) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("sod_pod_oid").ToString <> "" Then
                                'update karena ada hubungan antara so dan po antar group perusahaan
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_so = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sod_qty")) _
                                                     & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_pod_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If so_type.EditValue.ToString.ToUpper = "D" Then
                            If edit_sokp_piutang(objinsert, _so_oid_mstr.ToString, _total_payment) = False Then
                                sqlTran.Rollback()
                                edit = False
                                Exit Function
                            End If
                        End If

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
                                                        & SetInteger(so_en_id.EditValue) & ",  " _
                                                        & SetSetring(so_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_so_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")) & ",  " _
                                                        & SetSetring("Sales Order") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, so_en_id.EditValue, 10, _so_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code"), so_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit SO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code"))
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
                        set_row(_so_oid_mstr.ToString, "so_oid")
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

    Private Function edit_sokp_piutang(ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_amount As Double) As Boolean
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
                                    & "  sokp_so_oid = " & SetSetring(par_so_oid) & " "
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
                    .SQL = "select coalesce(sod_qty_shipment,0) as sod_qty_shipment from sod_det " + _
                           " where sod_so_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "sod_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("sod_qty_shipment") > 0 Then
                MessageBox.Show("Can't Delete Shipment Sales Order...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        ssqls.Clear()

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            load_data_to_detail()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select sod_pod_oid from sod_det where sod_so_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString) + ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '******************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from so_mstr where so_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_psn_ref_code")) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                                    & "  public.pesanan " _
                                                    & "set pesan_status='Belum Proses',pesan_so_code=null  where pesan_code=" & _
                                                    SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_psn_ref_code"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If


                            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_parent_oid")) <> "" Then
                                For Each dr_det As DataRow In ds.Tables("detail").Rows
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update  " _
                                                        & "  sod_det " _
                                                        & "set sod_qty_child= coalesce(sod_qty_child,0) - " & SetDbl(dr_det("sod_qty")) _
                                                        & " where sod_oid=" & SetSetring(dr_det("sod_sod_parent_oid"))

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next

                            End If






                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete SO " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code"))
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
                        .Dispose()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Sub so_ptnr_id_sold_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_ptnr_id_sold.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = so_en_id.EditValue
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
        Dim _sod_price As Double = 0

        If ds_edit.Tables.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1

            If ds_edit.Tables(0).Rows(i).Item("sod_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")) = True, 0, func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("sod_tax_class")))
            Else
                _tax_rate = 0
            End If

            'Exit Sub

            If ds_edit.Tables(0).Rows(i).Item("sod_tax_inc").ToString.ToUpper = "Y" Then
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_price") / (1 + _tax_rate)))
            Else
                _sod_price = ds_edit.Tables(0).Rows(i).Item("sod_price")
            End If

            _dpp = _dpp + (ds_edit.Tables(0).Rows(i).Item("sod_qty_real") * _sod_price)
            _dpp_line = (ds_edit.Tables(0).Rows(i).Item("sod_qty_real") * _sod_price)
            _discount = _discount + (ds_edit.Tables(0).Rows(i).Item("sod_qty_real") * _sod_price * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _discount_line = (ds_edit.Tables(0).Rows(i).Item("sod_qty_real") * _sod_price * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _ppn = _ppn + (_tax_rate * (_dpp_line - _discount_line))
        Next



        te_dpp.EditValue = _dpp
        te_discount.EditValue = _discount
        te_ppn.EditValue = _ppn
        te_total.EditValue = _dpp - _discount + _ppn
    End Sub

    Private Sub so_ref_po_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_ref_po_code.ButtonClick

        'Dim frm As New FPesananSearch()
        'frm.set_win(Me)
        'frm._obj = so_ref_po_code
        'frm._en_id = so_en_id.EditValue
        'frm.type_form = True
        'frm.ShowDialog()

        If func_coll.get_conf_file("ref_po_sales_order") = "1" Then
            If _so_ptnr_id_sold_mstr = -1 Then
                MessageBox.Show("Please Specify Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim frm As New FPurchaseOrderSearch()
                frm.set_win(Me)
                frm._obj = so_ref_po_code
                frm._so_ptnr_id_sold_mstr = _so_ptnr_id_sold_mstr
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If

    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String



        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_en_id")
        _type = 10
        _table = "so_mstr"
        _initial = "so"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  so_mstr.so_code, " _
            & "  so_mstr.so_ptnr_id_sold, " _
            & "  so_mstr.so_date, " _
            & "  so_mstr.so_ptnr_id_bill, " _
            & "  so_mstr.so_credit_term, " _
            & "  so_mstr.so_pay_type, " _
            & "  so_mstr.so_pay_method, " _
            & "  sod_det.sod_pt_id, " _
            & "  sod_det.sod_qty, " _
            & "  sod_det.sod_so_oid, " _
            & "  sod_det.sod_um, " _
            & "  ptnr_mstr.ptnr_code, " _
            & "  ptnr_mstr.ptnr_name, " _
            & "  ptnr_mstr.ptnr_id, " _
            & "  ptnra_addr.ptnra_id, " _
            & "  ptnra_addr.ptnra_line, " _
            & "  ptnra_addr.ptnra_line_1 as ptnra_line_1a, " _
            & "  ptnra_addr.ptnra_line_2 as ptnra_line_1b, " _
            & "  ptnra_addr.ptnra_line_3 as ptnra_line_1c, " _
            & "  pt_mstr.pt_id, " _
            & "  pt_mstr.pt_code, " _
            & "  pt_mstr.pt_desc1, " _
            & "  pt_mstr.pt_um, " _
            & "  code_mstr.code_id, " _
            & "  code_mstr.code_code, " _
            & "  code_mstr.code_field, " _
            & "  so_mstr.so_oid, " _
            & "  ptnra_addr.ptnra_oid, " _
            & "  ptnrac_cntc.ptnrac_oid,so_shipping_address as ptnra_line_1, " _
            & "  ptnrac_cntc.ptnrac_contact_name, " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & " FROM " _
            & "  so_mstr " _
            & "  INNER JOIN sod_det ON (so_mstr.so_oid = sod_det.sod_so_oid) " _
            & "  INNER JOIN ptnr_mstr ON (so_mstr.so_ptnr_id_sold = ptnr_mstr.ptnr_id) " _
            & "  INNER JOIN ptnra_addr ON (ptnra_addr.ptnra_ptnr_oid = ptnr_mstr.ptnr_oid) " _
            & "  INNER JOIN pt_mstr ON (sod_det.sod_pt_id = pt_mstr.pt_id) " _
            & "  INNER JOIN code_mstr ON (pt_mstr.pt_um = code_mstr.code_id) " _
            & "  LEFT OUTER JOIN ptnrac_cntc ON (ptnrac_cntc.addrc_ptnra_oid = ptnra_addr.ptnra_ptnr_oid) " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = so_oid " _
            & "WHERE " _
            & "so_mstr.so_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRSO"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        frm.ShowDialog()

    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString
        _colom = "so_trans_id"
        _table = "so_mstr"
        _criteria = "so_code"
        _initial = "so"
        _type = "so"
        _title = "Sales Order"

        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        'par_initial contohnya pby
        'par_type contohnya dr
        Dim _trn_status, user_wf, user_wf_email, filename, format_email_bantu, _pby_code As String
        Dim ssqls As New ArrayList
        If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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

                        filename = "c:\syspro\" + par_code + ".xls"
                        ExportTo(par_gv, New ExportXlsProvider(filename))

                        If user_wf_email <> "" Then
                            mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        Else
                            MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If

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

    Public Overrides Sub cancel_line()
        'walaupun
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString
        _colom = "so_trans_id"
        _table = "so_mstr"
        _criteria = "so_code"
        _initial = "so"
        _type = "so"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim _jml As Integer = 0
        Dim ssqls As New ArrayList

        If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            MessageBox.Show("Disable Authorization Cancel Line SO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        load_data_to_detail()

        If func_coll.get_conf_file("wf_sales_order") = "1" Then
            If _trans_id = "D" Then
                MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
                Exit Sub
            Else
                If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
        ElseIf func_coll.get_conf_file("wf_sales_order") = "0" Then
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select count(so_code) as jml " _
                                             & " from so_mstr " _
                                             & " inner join sod_det on sod_so_oid = so_oid " _
                                             & " where so_code ~~* '" + par_code + "' " _
                                             & " and sod_qty_shipment >= 1"

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
                MessageBox.Show("Can't Cancel For Shipment SO...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

                        For Each dr_det As DataRow In ds.Tables("detail").Rows
                            If SetString(dr_det("sod_sqd_oid").ToString) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sqd_det set sqd_qty_so = coalesce(sqd_qty_so,0) - " + SetDbl(dr_det("sod_qty")) _
                                      & " where sqd_oid  = " + SetSetring(dr_det("sod_sqd_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sq_mstr set sq_close_date = null, sq_trans_id = 'D' " + _
                                      " where  sq_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_sq_ref_oid")) & " "

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next


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
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _type, _user, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        _type = "so"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Sales Order"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        'Dim i As Integer
        'Dim ssqls As New ArrayList

        'If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
        '    Exit Sub
        'End If

        'ds.Tables("smart").AcceptChanges()

        'For i = 0 To ds.Tables("smart").Rows.Count - 1
        '    If ds.Tables("smart").Rows(i).Item("status") = True Then

        '        Try
        '            gv_email.Columns("so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("so_oid").ToString)
        '        Catch ex As Exception
        '        End Try

        '        _trans_id = "W" 'default langsung ke W 
        '        user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("so_code"), 0)
        '        user_wf_email = mf.get_email_address(user_wf)

        '        Try
        '            Using objinsert As New master_new.WDABasepgsql("", "")
        '                With objinsert
        '                    .Connection.Open()
        '                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
        '                    Try
        '                        .Command = .Connection.CreateCommand
        '                        .Command.Transaction = sqlTran
        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = "update so_mstr set so_trans_id = '" + _trans_id + "'," + _
        '                                       " so_upd_by = '" + master_new.ClsVar.sNama + "'," + _
        '                                       " so_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
        '                                       " where so_oid = '" + ds.Tables("smart").Rows(i).Item("so_oid") + "'"
        '                        ssqls.Add(.Command.CommandText)
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()

        '                        .Command.CommandType = CommandType.Text
        '                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
        '                                               " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("so_code") + "'" + _
        '                                               " and wf_seq = 0"
        '                        ssqls.Add(.Command.CommandText)
        '                        .Command.ExecuteNonQuery()
        '                        .Command.Parameters.Clear()

        '                        If master_new.PGSqlConn.status_sync = True Then
        '                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
        '                                .Command.CommandType = CommandType.Text
        '                                .Command.CommandText = Data
        '                                .Command.ExecuteNonQuery()
        '                                .Command.Parameters.Clear()
        '                            Next
        '                        End If

        '                        sqlTran.Commit()

        '                        format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("so_code"), "dr")

        '                        filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("so_code") + ".xls"
        '                        ExportTo(gv_email, New ExportXlsProvider(filename))

        '                        If user_wf_email <> "" Then
        '                            mf.sent_email(user_wf_email, "", mf.title_email("Sales Order", ds.Tables("smart").Rows(i).Item("so_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
        '                        Else
        '                            MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '                        End If

        '                    Catch ex As nPgSqlException
        '                        sqlTran.Rollback()
        '                        MessageBox.Show(ex.Message)
        '                    End Try
        '                End With
        '            End Using
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try
        '    End If
        'Next

        'help_load_data(True)
        'MessageBox.Show("Welldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)


        ' MyBase.smart_approve()

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
                & "  sod_oid, " _
                & "  sod_dom_id, " _
                & "  sod_en_id, " _
                & "  sod_add_by, " _
                & "  sod_add_date, " _
                & "  sod_upd_by, " _
                & "  sod_upd_date, " _
                & "  sod_so_oid, " _
                & "  sod_seq, " _
                & "  sod_is_additional_charge,sod_wo_status,sod_sod_parent_oid, " _
                & "  sod_si_id, " _
                & "  sod_pt_id, " _
                & "  sod_rmks, " _
                & "  sod_qty, " _
                & "  sod_qty_allocated,sod_qty_child, " _
                & "  sod_qty_picked, " _
                & "  sod_qty_shipment, " _
                & "  sod_qty_pending_inv, " _
                & "  sod_qty_invoice, " _
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
                & "  sod_loc_id, " _
                & "  sod_serial, " _
                & "  en_desc, " _
                & "  si_desc, " _
                & "  pt_code, " _
                & "  pt_desc1, " _
                & "  pt_desc2,sod_ppn,sod_pph, " _
                & "  um_mstr.code_name as um_name, " _
                & "  sod_sales_unit_total, " _
                & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                & "  sb_desc, " _
                & "  cc_desc, " _
                & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                & "  ac_mstr_disc.ac_name as ac_name_disc,sod_sqd_oid, " _
                & "  tax_class.code_name as sod_tax_class_name, sod_commision, sod_commision_total,  " _
                & "  sod_ppn_type, " _
                & "  loc_desc " _
                & "FROM  " _
                & "  public.sod_det " _
                & "  inner join so_mstr on so_oid = sod_so_oid " _
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
                & "  where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

            load_data_detail(sql, gc_detail, "detail")

            Try
                ds.Tables("detail_attr").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  soa_oid, " _
                & "  soa_kjb_code, " _
                & "  soa_bekerja_pada, " _
                & "  soa_jabatan_bagian, " _
                & "  soa_kantor_alamat_1, " _
                & "  soa_kantor_alamat_2, " _
                & "  soa_kantor_lantai, " _
                & "  soa_kantor_telp, " _
                & "  soa_ktp, " _
                & "  soa_email, " _
                & "  soa_rumah_alamat_1, " _
                & "  soa_rumah_alamat_2, " _
                & "  soa_rumah_kode_pos, " _
                & "  soa_rumah_telp, " _
                & "  soa_rumah_hp, " _
                & "  soa_status_alamat_kirim, " _
                & "  soa_status_alamat_tagih, " _
                & "  soa_suami_nama, " _
                & "  soa_suami_bekerja, " _
                & "  soa_suami_jabatan, " _
                & "  soa_suami_kantor_alamat_1, " _
                & "  soa_suami_kantor_alamat_2, " _
                & "  soa_suami_telp, " _
                & "  soa_suami_hp, " _
                & "  soa_anak_nama_1, " _
                & "  soa_anak_tgl_lahir_1, " _
                & "  soa_anak_sekolah_1, " _
                & "  soa_anak_nama_2, " _
                & "  soa_anak_tgl_lahir_2, " _
                & "  soa_anak_sekolah_2, " _
                & "  soa_anak_nama_3, " _
                & "  soa_anak_tgl_lahir_3, " _
                & "  soa_anak_sekolah_3, " _
                & "  soa_keluarga_dekat_nama, " _
                & "  soa_keluarga_dekat_alamat_1, " _
                & "  soa_keluarga_dekat_alamat_2, " _
                & "  soa_keluarga_dekat_telp, " _
                & "  soa_keluarga_dekat_hp, " _
                & "  soa_status_tempat_tinggal, " _
                & "  soa_jenis_kartu_kredit, " _
                & "  soa_no_kartu_kredit, " _
                & "  soa_berlaku_sd, " _
                & "  soa_dt, " _
                & "  soa_bank " _
                & "FROM  " _
                & "  public.soa_attr" _
                & "  inner join so_mstr on so_oid = soa_so_oid " _
                & "  where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

            '& "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '& " and so_en_id in (select user_en_id from tconfuserentity " _
            '& " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_detail_attr, "detail_attr")

            Try
                ds.Tables("detail_kp").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  sokp_oid, " _
                & "  sokp_so_oid, " _
                & "  sokp_seq, " _
                & "  sokp_ref, " _
                & "  sokp_amount, " _
                & "  sokp_due_date, " _
                & "  sokp_amount_pay, " _
                & "  sokp_description " _
                & "FROM  " _
                & "  public.sokp_piutang " _
                & "  inner join public.so_mstr on so_oid = sokp_so_oid " _
                & " where sokp_so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"
            '& "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '& " and so_en_id in (select user_en_id from tconfuserentity " _
            '                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            '& " order by sokp_seq"

            load_data_detail(sql, gc_detail_kp, "detail_kp")


            '.SQL = "SELECT  " _
            '          & "  a.sodas_oid, " _
            '          & "  a.sodas_so_oid, " _
            '          & "  a.sodas_pt_id_sod, " _
            '          & "  b.pt_code as pt_code_parent, " _
            '          & "  b.pt_desc1 as pt_desc1_parent, " _
            '          & "  b.pt_desc2 as pt_desc2_parent, " _
            '          & "  a.sodas_pt_id, " _
            '          & "  c.pt_code, " _
            '          & "  c.pt_desc1, " _
            '          & "  c.pt_desc2, " _
            '          & "  a.sodas_qty,sodas_qty_sold, " _
            '          & "  a.sodas_sod_oid " _
            '          & "FROM " _
            '          & "  public.sodas_assembly a " _
            '          & "  INNER JOIN public.pt_mstr b ON (a.sodas_pt_id_sod = b.pt_id) " _
            '          & "  INNER JOIN public.pt_mstr c ON (a.sodas_pt_id = c.pt_id) " _
            '          & "WHERE " _
            '          & "  a.sodas_so_oid is null " _
            '          & "ORDER BY " _
            '          & "  a.sodas_pt_id_sod, " _
            '          & "  a.sodas_pt_id"

            '.InitializeCommand()
            '.FillDataSet(ds_edit, "insert_edit_assembly")
            'gc_edit_assembly.DataSource = ds_edit.Tables(1)
            'gv_edit_assembly.BestFitColumns()


            Try
                ds.Tables("detail_assembly").Clear()
            Catch ex As Exception
            End Try


            sql = "SELECT  " _
                      & "  a.sodas_oid, " _
                      & "  a.sodas_so_oid, " _
                      & "  a.sodas_pt_id_sod, " _
                      & "  b.pt_code as pt_code_parent, " _
                      & "  b.pt_desc1 as pt_desc1_parent, " _
                      & "  b.pt_desc2 as pt_desc2_parent, " _
                      & "  a.sodas_pt_id, " _
                      & "  c.pt_code, " _
                      & "  c.pt_desc1, " _
                      & "  c.pt_desc2, " _
                      & "  a.sodas_qty,sodas_qty_sold, " _
                      & "  a.sodas_sod_oid " _
                      & "FROM " _
                      & "  public.sodas_assembly a " _
                      & "  INNER JOIN public.pt_mstr b ON (a.sodas_pt_id_sod = b.pt_id) " _
                      & "  INNER JOIN public.pt_mstr c ON (a.sodas_pt_id = c.pt_id) " _
                      & "WHERE " _
                      & "  a.sodas_so_oid  ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "' " _
                      & "ORDER BY " _
                      & "  a.sodas_pt_id_sod, " _
                      & "  a.sodas_pt_id"

            load_data_detail(sql, gc_detail_assembly, "detail_assembly")

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
                      " inner join so_mstr on so_code = wf_ref_code " + _
                      " inner join sod_det dt on dt.sod_so_oid = so_oid " _
                      & "  where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "' " _
                      & " order by wf_ref_code, wf_seq "

                '& " where so_date >= " + SetDate(pr_txttglawal.DateTime) _
                '& " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                '& " and so_en_id in (select user_en_id from tconfuserentity " _
                '                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                '& " order by wf_ref_code, wf_seq "


                load_data_detail(sql, gc_wf, "wf")
                gv_wf.BestFitColumns()

                Try
                    ds.Tables("email").Clear()
                Catch ex As Exception
                End Try

                sql = "SELECT  " _
                           & "  so_oid, " _
                            & "  so_en_id, " _
                            & "  so_add_by, " _
                            & "  so_add_date, " _
                            & "  so_upd_by, " _
                            & "  so_upd_date, " _
                            & "  so_code, " _
                            & "  so_date, " _
                            & "  so_dp, " _
                            & "  so_disc_header, " _
                            & "  so_total, " _
                            & "  so_payment_date, " _
                            & "  so_cons, " _
                            & "  so_close_date, " _
                            & "  so_tran_id, " _
                            & "  so_trans_id, " _
                            & "  so_trans_rmks, " _
                            & "  so_cu_id, " _
                            & "  so_total_ppn, " _
                            & "  so_total_pph, " _
                            & "  so_payment, " _
                            & "  so_exc_rate, " _
                            & "  so_tax_inc, " _
                            & "  (so_total + so_total_ppn + so_total_pph) as so_total_after_tax, " _
                            & "  so_exc_rate * so_total as so_total_ext,  so_exc_rate * so_total_ppn as so_total_ppn_ext, " _
                            & "  so_exc_rate * so_total_pph as so_total_pph_ext,  so_exc_rate * (so_total + so_total_ppn + so_total_pph) as so_total_after_tax_ext, " _
                            & "  en_desc, " _
                            & "  ptnr_mstr_sold.ptnr_name as ptnr_name_sold, " _
                            & "  ptnr_mstr_bill.ptnr_name as ptnr_name_bill, " _
                            & "  credit_term.code_name as credit_term_name, " _
                            & "  tax_class.code_name as tax_class_name, " _
                            & "  si_desc, " _
                            & "  pi_desc, " _
                            & "  pay_type.code_name as pay_type_name, " _
                            & "  pay_method.code_name as pay_method_name, " _
                            & "  cc_mstr_ar.cc_desc, " _
                            & "  cu_name, " _
                            & "  tran_name, " _
                            & "  sod_oid, " _
                            & "  sod_so_oid, " _
                            & "  sod_pt_id, " _
                            & "  sod_rmks, " _
                            & "  sod_qty, " _
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
                            & "  sod_loc_id, " _
                            & "  sod_serial, " _
                            & "  en_desc, " _
                            & "  si_desc, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  um_mstr.code_name as um_name, " _
                            & "  sod_sales_unit_total, " _
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_mstr_ar.sb_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.so_mstr " _
                            & "  inner join en_mstr on en_id = so_en_id " _
                            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = so_ptnr_id_bill " _
                            & "  inner join code_mstr credit_term on credit_term.code_id = so_credit_term " _
                            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = so_sales_person " _
                            & "  inner join pi_mstr on pi_id = so_pi_id " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                            & "  inner join code_mstr pay_method on pay_method.code_id = so_pay_method " _
                            & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = so_ar_sb_id " _
                            & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = so_ar_cc_id " _
                            & "  inner join cu_mstr on cu_id = so_cu_id " _
                            & "  inner join tran_mstr on tran_id = so_tran_id" _
                            & "  inner join sod_det on so_oid = sod_so_oid " _
                            & "  inner join si_mstr on si_id = sod_si_id " _
                            & "  inner join pt_mstr on pt_id = sod_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = sod_loc_id" _
                            & "  where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "' "

                '& " where so_date >= " + SetDate(pr_txttglawal.DateTime) _
                '& " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                '& " and so_en_id in (select user_en_id from tconfuserentity " _
                '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                load_data_detail(sql, gc_email, "email")
                gv_email.BestFitColumns()

                ' belum(diaktifkan)

                Try
                    ds.Tables("smart").Clear()
                Catch ex As Exception
                End Try

                sql = "select so_oid, so_code, so_trans_id, false as status from so_mstr " _
                    & " where so_trans_id ~~* 'd' "
                load_data_detail(sql, gc_smart, "smart")
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gv_master.RowStyle
        Try
            'Dim view As GridView = TryCast(sender, GridView)

            'Dim _mark As Boolean = If(view.GetRowCellValue(e.RowHandle, "en_desc") = view.GetRowCellValue(e.RowHandle, "en_desc_cust"), False, True)

            'If _mark = True Then
            '    e.Appearance.BackColor = Color.Red
            'End If

        Catch ex As Exception

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
        gc_detail_attr.DataSource = Nothing
        gc_wf.DataSource = Nothing
        gc_email.DataSource = Nothing
        gc_smart.DataSource = Nothing
        load_data_to_detail()
    End Sub

    Private Sub ExportToPameranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sSQL As String
        Try
            Dim objConn As nPgSqlConnection
            Dim daT1 As nPgSqlDataAdapter


            objConn = New nPgSqlConnection(master_new.PGSqlConn.DbConString)
            objConn.Open()

            Dim Ds_export As DataSet
            Ds_export = New DataSet

            sSQL = "SELECT  " _
                & "  a.so_oid, " _
                & "  a.so_code, " _
                & "  a.so_date, " _
                & "  b.ptnr_name " _
                & "FROM " _
                & "  public.so_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.so_ptnr_id_sold = b.ptnr_id) " _
                & "WHERE " _
                & "  a.so_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code").ToString & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "so_master")

            sSQL = "SELECT  " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  c.code_code AS um_desc, " _
                & "  a.sod_price, " _
                & "  a.sod_qty, " _
                & "  b.pt_isbn,a.sod_seq " _
                & "FROM " _
                & "  public.pt_mstr b " _
                & "  INNER JOIN public.sod_det a ON (b.pt_id = a.sod_pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "  INNER JOIN public.so_mstr d ON (a.sod_so_oid = d.so_oid) " _
                & "WHERE " _
                & "  d.so_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code").ToString & "' " _
                & "order by a.sod_seq"

            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "so_detail")

            Dim objSaveFileDialog As New SaveFileDialog
            Dim filePath As String

            'Set the Save dialog properties

            With objSaveFileDialog
                .DefaultExt = "xml"
                .FileName = "Export_SO_" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code").ToString & Now().ToString("yyyyMMdd-HHmmss")
                .Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*"
                .FilterIndex = 1
                .OverwritePrompt = True
                .Title = .FileName
            End With

            If objSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

                filePath = System.IO.Path.Combine( _
                    My.Computer.FileSystem.SpecialDirectories.MyDocuments, _
                    objSaveFileDialog.FileName)

                Ds_export.WriteXml(filePath, XmlWriteMode.WriteSchema)
            Else
                Exit Sub
            End If

            objConn.Close()
            objSaveFileDialog.Dispose()
            objSaveFileDialog = Nothing
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            gv_edit.UpdateCurrentRow()
            Dim _col As String = gv_edit.FocusedColumn.Name
            Dim _row As Integer = gv_edit.FocusedRowHandle

            If _col = "loc_desc" Then
                For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
                    ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
                    ds_edit.Tables(0).Rows(i).Item("sod_loc_id") = gv_edit.GetRowCellValue(_row, "sod_loc_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_edit.SelectionChanged
        Try
            cek_shipment_row()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub cek_shipment_row()
        Try
            If so_is_package.EditValue = False Then
                Dim _nilai As Object
                _nilai = SetNumber(gv_edit.GetFocusedRowCellValue("status_shipment"))

                If _nilai > 0 Then
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
                Else
                    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
                End If
            End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.Click
        cek_shipment_row()
    End Sub

    Private Sub file_xml_pameran_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_xml_pameran.ButtonClick
        Dim sSQL As String
        Dim dt_temp As New DataTable
        Dim _row As Integer = 0
        Try

            Dim filex As String = ""
            filex = AskOpenFile("Xml Files | *.xml")

            If filex = "" Then
                Exit Sub
            End If

            file_xml_pameran.Text = filex

            Dim ds_import As New DataSet
            ds_import.ReadXml(filex)
            'For i As Integer = 0 To gv_edit.RowCount - 1
            '    gv_edit.DeleteRow(i)

            'Next
            ds_edit.Tables(0).Rows.Clear()
            ds_edit.AcceptChanges()

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

            For Each dr As DataRow In ds_import.Tables(0).Rows
                If SetString(dr("status_pajak")).ToUpper = so_ppn_type.EditValue.ToString.ToUpper Then

                    sSQL = "SELECT  distinct " _
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
                    & " where pt_code ='" & dr("kode_barang") & "' "

                    dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

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

                        gv_edit.SetRowCellValue(_row, "sod_pt_id", dr_temp("pt_id"))
                        gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                        gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                        gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                        gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                        gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                        gv_edit.SetRowCellValue(_row, "sod_loc_id", dr_temp("pt_loc_id"))
                        gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                        gv_edit.SetRowCellValue(_row, "sod_um", dr_temp("pt_um"))
                        gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                        gv_edit.SetRowCellValue(_row, "sod_cost", dr_temp("invct_cost"))

                        'If _so_type <> "D" Then
                        '    fobject.gv_edit.SetRowCellValue(_row, "sod_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
                        'End If

                        gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                        gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                        gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                        gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                        gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                        gv_edit.SetRowCellValue(_row, "sod_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                        gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                        gv_edit.SetRowCellValue(_row, "sod_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                        gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                        gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                        gv_edit.SetRowCellValue(_row, "sod_taxable", dr_temp("pt_taxable"))
                        gv_edit.SetRowCellValue(_row, "sod_tax_inc", dr_temp("pt_tax_inc"))
                        gv_edit.SetRowCellValue(_row, "sod_tax_class", dr_temp("pt_tax_class"))
                        gv_edit.SetRowCellValue(_row, "sod_tax_class_name", dr_temp("tax_class_name"))

                        gv_edit.SetRowCellValue(_row, "sod_ppn_type", dr_temp("pt_ppn_type"))
                        gv_edit.SetRowCellValue(_row, "sod_qty", dr("qty_all"))
                        gv_edit.SetRowCellValue(_row, "sod_disc", dr("disc_avg") / 100)

                        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                        _row = _row + 1
                        System.Windows.Forms.Application.DoEvents()
                        'Exit Sub
                    Next
                End If
            Next

            gv_edit.Columns("sod_disc").OptionsColumn.AllowEdit = False
            gv_edit.Columns("sod_qty").OptionsColumn.AllowEdit = False

            gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
            Box("Import Finish")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub SetPPNTypeEBebasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPPNTypeEBebasToolStripMenuItem.Click
        Try
            ssqls.Clear()

            If ask("Are you sure to set PPN Type E for this SO ?", "Confirm..", MessageBoxDefaultButton.Button1) = True Then
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE so_mstr set so_ppn_type='E' where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE sod_det set sod_ppn_type='E' where sod_so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

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
                            Box("Success")
                        Catch ex As npgSql.nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SetPPNTypeABayarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPPNTypeABayarToolStripMenuItem.Click
        Try
            ssqls.Clear()

            If ask("Are you sure to set PPN Type A for this SO ?", "Confirm..", MessageBoxDefaultButton.Button1) = True Then
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE so_mstr set so_ppn_type='A' where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE sod_det set sod_ppn_type='A' where sod_so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'"

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

                            Box("Success")
                        Catch ex As npgSql.nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub so_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_pt_id.ButtonClick
        Try
            If so_is_package.EditValue = False Then
                Box("Please check is package")
                Exit Sub
            End If

            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            'frm._pt_id = so_pt_id.Tag
            frm._en_id = so_en_id.EditValue
            frm._si_id = so_si_id.EditValue
            frm._so_type = so_type.EditValue
            frm.grid_call = "header"
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub so_pay_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_pay_type.EditValueChanged

        Dim _filter As String
        _filter = " and ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(so_en_id.EditValue)

        If so_pay_type.GetColumnValue("code_usr_1") = 0 Then
            _filter += " and enacc_code='so_cash')"
        Else
            _filter += " and enacc_code='so_credit')"
        End If


        dt_bantu = New DataTable
        If limit_account(so_en_id.EditValue) = True Then
            dt_bantu = (func_data.load_ac_mstr(_filter))
        Else
            dt_bantu = (func_data.load_ac_mstr())
        End If
        'dt_bantu = (func_data.load_ac_mstr(_filter))
        so_ar_ac_id.Properties.DataSource = dt_bantu
        so_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        so_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        so_ar_ac_id.ItemIndex = 0
    End Sub

    Private Sub UpdateTerbilangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTerbilangToolStripMenuItem.Click
        Try
            Dim ssql As String
            Dim _terbilang As String
            Dim sSQLs As New ArrayList

            For n As Integer = 0 To gv_master.RowCount - 1

                '_terbilang = func_bill.TERBILANG_FIX(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_total_after_tax"))
                'ssql = "update so_mstr set so_terbilang=" & SetSetring(_terbilang) & " where so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid") & "'"

                _terbilang = func_bill.TERBILANG_FIX(gv_master.GetRowCellValue(n, "so_total_after_tax"))

                ssql = "update so_mstr set so_terbilang=" & SetSetring(_terbilang) & " where so_oid='" & gv_master.GetRowCellValue(n, "so_oid").ToString & "'"

                sSQLs.Add(ssql)
            Next


            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            Box("Update success, please refresh data")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub so_ptnr_id_sold_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_ptnr_id_sold.EditValueChanged
        Try



            Dim ssql As String = ""

            ssql = "select ptnr_ptnrg_id from ptnr_mstr where ptnr_id=" & SetInteger(_so_ptnr_id_sold_mstr)

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                ptnrg_id.EditValue = dr(0)
            Next

            dt_bantu = New DataTable
            If func_coll.get_conf_file("so_price_list_limit_ptnrg_id") = "1" Then
                dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime, ptnrg_id.EditValue))
            Else
                dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
            End If

            Try

                so_pi_id.Properties.DataSource = dt_bantu
                so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
                so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
                so_pi_id.ItemIndex = 0

            Catch ex As Exception

            End Try

            If _conf_limit_partner_group_ws = "1" Then
                If Not SetInteger(ptnrg_id.EditValue) = "Null" Then
                    ssql = "select ptnrg_credit_limit,ptnrg_term_of_payment from ptnrg_grp where ptnrg_id=" & SetInteger(ptnrg_id.EditValue)

                    Dim dt_ptnrg As New DataTable
                    dt_ptnrg = GetTableData(ssql)

                    Dim _term As Double
                    For Each dr_ptnrg As DataRow In dt_ptnrg.Rows
                        If Not dr_ptnrg("ptnrg_term_of_payment") Is DBNull.Value Then
                            _term = dr_ptnrg("ptnrg_term_of_payment")
                        End If

                    Next

                    ssql = "select code_id, code_name, code_usr_1 from code_mstr where code_field ~~* 'creditterms_mstr' and code_active ~~* 'Y'" _
                                     & " AND code_en_id in (0," & SetInteger(so_en_id.EditValue) & ")" _
                                     & " AND code_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & " and code_usr_1='" & CInt(_term) & "' " _
                                     & " order by code_default desc, code_desc"
                    Dim dt_term_payment As New DataTable
                    dt_term_payment = GetTableData(ssql)

                    For Each dr_term As DataRow In dt_term_payment.Rows
                        If Not dr_term("code_id") Is DBNull.Value Then
                            so_credit_term.EditValue = dr_term("code_id")
                        End If

                    Next
                End If

            End If

            If _conf_limit_partner_group_ws = "1" Then
                If Not SetInteger(ptnrg_id.EditValue) = "Null" Then
                    ssql = "select ptnrg_credit_limit,ptnrg_term_of_payment from ptnrg_grp where ptnrg_id=" & SetInteger(ptnrg_id.EditValue)

                    Dim dt_ptnrg As New DataTable
                    dt_ptnrg = GetTableData(ssql)

                    Dim _term As Double = 0.0
                    For Each dr_ptnrg As DataRow In dt_ptnrg.Rows
                        If Not dr_ptnrg("ptnrg_credit_limit") Is DBNull.Value Then
                            _term = dr_ptnrg("ptnrg_credit_limit")
                        End If
                    Next

                    If _term > 0.0 Then
                        ssql = "SELECT  " _
                         & "  sum(ar_amount - coalesce(ar_pay_amount,0)) as jml " _
                         & "FROM " _
                         & "  public.ar_mstr " _
                         & "WHERE " _
                         & "  public.ar_mstr.ar_status  is null and  ar_bill_to = " & _so_ptnr_id_sold_mstr _
                         & "  "


                        Dim dt_test As New DataTable
                        dt_test = GetTableData(ssql)

                        'If dt.Rows.Count > 0 Then
                        '    Box("Check Debit Credit Memo for this sales")
                        '    Return False
                        'End If

                        Dim _credit As Double = 0.0
                        For Each dr_ptnrg As DataRow In dt_test.Rows
                            If Not dr_ptnrg("jml") Is DBNull.Value Then
                                _credit = dr_ptnrg(0)
                            End If
                        Next

                        If _credit > _term Then
                            Box("AR Amount : " & MaskDec(_credit) & " over than credit limit : " & MaskDec(_term))
                            'Return False
                        End If
                    End If

                End If

            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub so_sq_ref_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_sq_ref_oid.ButtonClick
        Dim frm As New FSalesQuotationSearch
        frm.set_win(Me)
        frm._en_id = so_en_id.EditValue
        frm._date = so_date.DateTime
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub so_pi_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_pi_id.EditValueChanged
        Try

            ds_edit.Tables("insert_edit").Rows.Clear()
            ds_edit.Tables("insert_edit").AcceptChanges()
            gv_edit.RefreshData()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub so_psn_ref_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_psn_ref_oid.ButtonClick
        Try

            Dim frm As New FPesananSearch()
            frm.set_win(Me)
            frm._obj = so_psn_ref_oid
            frm._en_id = so_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtLoadItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtLoadItem.Click
        Try
            Dim ssql As String
            ssql = "SELECT  " _
            & "  a.pesan_oid,en_desc, " _
            & "  a.pesan_code, " _
            & "  a.pesan_date, " _
            & "  a.pesan_sales_id,  b.ptnr_code,  b.ptnr_name,  " _
            & "  a.pesan_en_id, " _
            & "  a.pesan_add_date, " _
            & "  a.pesan_status_customer_lama, " _
            & "  a.pesan_kode_costumer, " _
            & "  a.pesan_customer, " _
            & "  a.pesan_costumer_alamat, " _
            & "  a.pesan_costumer_hp, " _
            & "  a.pesan_kode_barang1, " _
            & "  a.pesan_nama_barang1, " _
            & "  a.pesan_qty1, " _
            & "  a.pesan_ket1, " _
            & "  a.pesan_kode_barang2, " _
            & "  a.pesan_nama_barang2, " _
            & "  a.pesan_qty2, " _
            & "  a.pesan_ket2, " _
            & "  a.pesan_kode_barang3, " _
            & "  a.pesan_nama_barang3, " _
            & "  a.pesan_qty3, " _
            & "  a.pesan_ket3, " _
            & "  a.pesan_kode_barang4, " _
            & "  a.pesan_nama_barang4, " _
            & "  a.pesan_qty4, " _
            & "  a.pesan_ket4, coalesce(pesan_status,'') as pesan_status " _
            & "FROM " _
            & "  public.pesanan a " _
            & "   INNER JOIN public.ptnr_mstr b ON (a.pesan_sales_id = b.ptnr_id) " _
            & "   INNER JOIN public.en_mstr c ON  (a.pesan_en_id = c.en_id) " _
            & "WHERE " _
            & "  a.pesan_code = '" & so_psn_ref_oid.Text & "' "
            Dim dt_pt As New DataTable
            dt_pt = GetTableData(ssql)

            ds_edit.Tables(0).Rows.Clear()

            For Each dr As DataRow In dt_pt.Rows
                If SetString(dr("pesan_kode_barang1")) <> "" Then
                    add_item(dr("pesan_kode_barang1"), 0, SetNumber(dr("pesan_qty1")))
                End If
                If SetString(dr("pesan_kode_barang2")) <> "" Then
                    add_item(dr("pesan_kode_barang2"), 1, SetNumber(dr("pesan_qty2")))
                End If
                If SetString(dr("pesan_kode_barang3")) <> "" Then
                    add_item(dr("pesan_kode_barang3"), 2, SetNumber(dr("pesan_qty3")))
                End If
                If SetString(dr("pesan_kode_barang4")) <> "" Then
                    add_item(dr("pesan_kode_barang4"), 3, SetNumber(dr("pesan_qty4")))
                End If
            Next



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function add_item(ByVal par_pt_code As String, ByVal _row As Integer, ByVal par_qty As Double) As Boolean
        Try
            Dim ssql As String
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
                  & " where pt_code =" & SetSetring(par_pt_code) & " "

            Dim dt_temp As New DataTable
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
                                Exit Function
                            ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                Exit Function
                            ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
                                Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                    dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
                                Exit Function
                            End If

                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try


                gv_edit.AddNewRow()
                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                gv_edit.SetRowCellValue(_row, "sod_pt_id", dr_temp("pt_id"))
                gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
                gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
                gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
                gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
                gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
                gv_edit.SetRowCellValue(_row, "sod_loc_id", dr_temp("pt_loc_id"))
                gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
                gv_edit.SetRowCellValue(_row, "sod_um", dr_temp("pt_um"))
                gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
                gv_edit.SetRowCellValue(_row, "sod_cost", dr_temp("invct_cost"))

                gv_edit.SetRowCellValue(_row, "sod_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
                gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
                gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
                gv_edit.SetRowCellValue(_row, "sod_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
                gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
                gv_edit.SetRowCellValue(_row, "sod_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
                gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

                gv_edit.SetRowCellValue(_row, "sod_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
                gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
                gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

                gv_edit.SetRowCellValue(_row, "sod_taxable", dr_temp("pt_taxable"))
                gv_edit.SetRowCellValue(_row, "sod_tax_inc", dr_temp("pt_tax_inc"))
                gv_edit.SetRowCellValue(_row, "sod_tax_class", dr_temp("pt_tax_class"))
                gv_edit.SetRowCellValue(_row, "sod_tax_class_name", dr_temp("tax_class_name"))

                gv_edit.SetRowCellValue(_row, "sod_ppn_type", dr_temp("pt_ppn_type"))
                gv_edit.SetRowCellValue(_row, "sod_qty", par_qty)
                gv_edit.SetRowCellValue(_row, "sod_disc", 0)

                Dim dt_bantu_new As New DataTable()

                Using ds_bantu_new As New DataSet()

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
                                    & "  coalesce(pidd_payment,0) as pidd_payment,coalesce(pidd_commision,0) as pidd_commision, " _
                                    & "  pidd_min_qty, " _
                                    & "  pidd_sales_unit, " _
                                    & "  pid_pt_id " _
                                    & "FROM  " _
                                    & "  public.pidd_det " _
                                    & "  inner join public.pid_det on pid_oid = pidd_pid_oid " _
                                    & "  inner join public.pi_mstr on pi_oid = pid_pi_oid " _
                                    & "  where pi_id = " & so_pi_id.EditValue _
                                    & "  and pidd_payment_type = " & so_pay_type.EditValue _
                                    & "  and pid_pt_id = " & dr_temp("pt_id") _
                                    & "  order by pidd_min_qty desc "
                                .InitializeCommand()
                                .FillDataSet(ds_bantu_new, "pi_mstr")
                                dt_bantu_new = ds_bantu_new.Tables(0)
                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                If dt_bantu_new.Rows.Count > 0 Then
                    gv_edit.SetRowCellValue(_row, "sod_price", dt_bantu_new.Rows(0).Item("pidd_price"))
                    gv_edit.SetRowCellValue(_row, "sod_disc", dt_bantu_new.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(_row, "sod_payment", dt_bantu_new.Rows(0).Item("pidd_payment"))
                    gv_edit.SetRowCellValue(_row, "sod_dp", dt_bantu_new.Rows(0).Item("pidd_dp"))
                    gv_edit.SetRowCellValue(_row, "sod_sales_unit", dt_bantu_new.Rows(0).Item("pidd_sales_unit"))
                    gv_edit.SetRowCellValue(_row, "sod_commision", dt_bantu_new.Rows(0).Item("pidd_commision"))
                Else
                    gv_edit.SetRowCellValue(_row, "sod_price", 0)
                    gv_edit.SetRowCellValue(_row, "sod_disc", 0)
                    gv_edit.SetRowCellValue(_row, "sod_payment", 0)
                    gv_edit.SetRowCellValue(_row, "sod_dp", 0)
                    gv_edit.SetRowCellValue(_row, "sod_sales_unit", 0)
                    gv_edit.SetRowCellValue(_row, "sod_commision", 0)
                End If


                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                _row = _row + 1
                System.Windows.Forms.Application.DoEvents()
                gv_edit.BestFitColumns()
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub so_ref_po_code_2_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_ref_po_code_2.ButtonClick
        If func_coll.get_conf_file("ref_po_sales_order") = "1" Then
            If _so_ptnr_id_sold_mstr = -1 Then
                MessageBox.Show("Please Specify Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim frm As New FPurchaseOrderSearch()
                frm.set_win(Me)
                frm._obj = so_ref_po_code_2
                frm._so_ptnr_id_sold_mstr = _so_ptnr_id_sold_mstr
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub so_parent_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles so_parent_oid.ButtonClick

        Try
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)
            frm._obj = so_parent_oid
            frm._en_id = so_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()


            'Dim frm As New FSalesQuotationSearch
            'frm.set_win(Me)
            'frm._en_id = ptsfr_en_id.EditValue
            'frm._obj = ptsfr_sq_oid
            'frm.type_form = True
            'frm.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPacking.Click
        Try
            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pay_interval") = 0 Then

            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Proses Data Ini..?", "Packing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssql As String

            Dim sSQLs As New ArrayList

            For Each dr_master As DataRow In ds.Tables(0).Rows
                If dr_master("pilih") = True Then
                    ssql = "update so_mstr set so_status_packing='D' where so_oid='" & dr_master("so_oid").ToString & "'"

                    sSQLs.Add(ssql)

                End If
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            after_success()
            Box("Update success")


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
