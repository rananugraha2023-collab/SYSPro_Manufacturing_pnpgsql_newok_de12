Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Imports DevExpress.XtraExport
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Net

Public Class FSalesQuotationGenerate
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit, ds_edit_shipment, ds_edit_transfer, ds_edit_dist As New DataSet
    Dim _now As DateTime
    Dim _sq_oid_mstr, _sqa_oid_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Public _sq_ptnr_id_sold_mstr As Integer
    Public _sq_ref_po_oid As String
    Public _ptnratt_ptnr_oid As String
    Dim _conf_value As String
    Dim _url_api As String = ""
    Dim mf As New master_new.ModFunction
    Dim _kjb_code As String
    Dim _id_proses As Integer = 0
    Dim dt As New DataTable

    Private Sub FSalesQuotation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_quotation")
        _url_api = func_coll.get_conf_file("url_api_sdi")

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _sq_ptnr_id_sold_mstr = -1

        'set diawal agar tau kalau -1 artinya datanya belum ada...

        If _conf_value = "0" Then
            xtc_detail.TabPages(3).PageVisible = False
            xtc_detail.TabPages(5).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(3).PageVisible = True
            xtc_detail.TabPages(4).PageVisible = False
            xtc_detail.TabPages(5).PageVisible = True
        End If
        _ptnratt_ptnr_oid = ""
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

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            sq_tran_id.Properties.DataSource = dt_bantu
            sq_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            sq_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            sq_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            sq_tran_id.Properties.DataSource = dt_bantu
            sq_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            sq_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            sq_tran_id.ItemIndex = 0
        End If


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

        dt_bantu = New DataTable
        dt_bantu = func_data.load_sales_program()

        With sq_sales_program
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

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_sales(sq_en_id.EditValue))
        'sq_sales_person.Properties.DataSource = dt_bantu
        'sq_sales_person.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        'sq_sales_person.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        'sq_sales_person.ItemIndex = 0



        dt_bantu = New DataTable
        If SetString(func_coll.get_conf_file("penjualan_lintas_entitas")) = "1" Then
            dt_bantu = (load_sales(sq_en_id.EditValue))
        Else
            dt_bantu = (func_data.load_sales(sq_en_id.EditValue))
        End If

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

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_status_rumah())
        sqa_status_rumah_id.Properties.DataSource = dt_bantu
        sqa_status_rumah_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_status_rumah_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_status_rumah_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_lama_tinggal())
        sqa_lama_tinggal_id.Properties.DataSource = dt_bantu
        sqa_lama_tinggal_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_lama_tinggal_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_lama_tinggal_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.masa_kerja())
        sqa_masa_kerja_id.Properties.DataSource = dt_bantu
        sqa_masa_kerja_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_masa_kerja_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_masa_kerja_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.income())
        sqa_income_id.Properties.DataSource = dt_bantu
        sqa_income_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_income_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_income_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.kepribadian())
        'sqa_kepribadian_id.Properties.DataSource = dt_bantu
        'sqa_kepribadian_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'sqa_kepribadian_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'sqa_kepribadian_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.tanggungan())
        sqa_tanggungan_id.Properties.DataSource = dt_bantu
        sqa_tanggungan_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_tanggungan_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_tanggungan_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.jaminan())
        sqa_jaminan_id.Properties.DataSource = dt_bantu
        sqa_jaminan_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_jaminan_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_jaminan_id.ItemIndex = 0


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
        add_column_copy(gv_master, "Cust ID", "sq_ptnr_id_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi Po No.", "sq_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
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
        add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Sales Program", "sq_sales_program", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status Produk", "sq_status_produk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Diskon Produk", "sq_diskon_produk", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Unique Number", "sq_unique_code", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Prepayment Unique", "sq_dp_unique", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Payment Unique", "sq_payment_unique", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")

        'sq_sales_program,sq_status_produk,sq_diskon_produk,sq_unique_code, sq_dp_unique,sq_payment_unique,
        add_column_copy(gv_master, "Status", "sq_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Unit", "sq_sales_unit", DevExpress.Utils.HorzAlignment.Default)

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
        add_column_copy(gv_detail, "Additional Charge", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Process", "sqd_qty_transfer_issue", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Complete", "sqd_qty_transfer_receipt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Reject", "sqd_qty_transfer_return", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty SO  ", "sqd_qty_so", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
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

        add_column(gv_detail_attr, "sqa_sq_oid", False)
        add_column_copy(gv_detail_attr, "KJB Number", "sqa_kjb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Tempat Bekerja", "sqa_bekerja_pada", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jabatan", "sqa_jabatan_bagian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat1", "sqa_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Alamat2", "sqa_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Lantai Bekerja", "sqa_kantor_lantai", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Kantor Telephone", "sqa_kantor_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "KTP", "sqa_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Email", "sqa_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat1", "sqa_rumah_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Alamat2", "sqa_rumah_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Kode Pos", "sqa_rumah_kode_pos", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Rumah Telephone", "sqa_rumah_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Telephone HP", "sqa_rumah_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Kirim", "sqa_status_alamat_kirim", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Alamat Tagih", "sqa_status_alamat_tagih", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Nama", "sqa_suami_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Tempat Bekerja", "sqa_suami_bekerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Jabatan", "sqa_suami_jabatan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat1", "sqa_suami_kantor_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Kantor Alamat2", "sqa_suami_kantor_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri Telephone", "sqa_suami_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Suami/Istri HP", "sqa_suami_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Nama", "sqa_anak_nama_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak Pertama Tgl Lahir", "sqa_anak_tgl_lahir_1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak Pertama Sekolah", "sqa_anak_sekolah_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Nama", "sqa_anak_nama_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak kedua Tgl Lahir", "sqa_anak_tgl_lahir_2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak kedua Sekolah", "sqa_anak_sekolah_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Nama", "sqa_anak_nama_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Anak ketiga Tgl Lahir", "sqa_anak_tgl_lahir_3", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Anak ketiga Sekolah", "sqa_anak_sekolah_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Nama", "sqa_keluarga_dekat_nama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat1", "sqa_keluarga_dekat_alamat_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Alamat2", "sqa_keluarga_dekat_alamat_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat Telephone", "sqa_keluarga_dekat_telp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Keluarga Dekat HP", "sqa_keluarga_dekat_hp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Tempat Tinggal", "sqa_status_tempat_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jenis Kartu Kredit", "sqa_jenis_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "No Kartu Kredit", "sqa_no_kartu_kredit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Berlaku s/d", "sqa_berlaku_sd", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_attr, "Bank", "sqa_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Status Rumah Tinggal", "status_kepemilikan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Lama Tinggal", "lama_tinggal", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Masa Kerja", "masa_kerja", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Income", "income", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_attr, "Kepribadian", "kepribadian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Tanggungan", "tanggungan", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail_attr, "Jaminan", "jaminan", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_kp, "sokp_sq_oid", False)
        add_column_copy(gv_detail_kp, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Amount Pay", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Date Payment", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Description", "sokp_description", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "sqd_oid", False)
        add_column(gv_edit, "sqd_sq_oid", False)
        add_column(gv_edit, "sqd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Additional Charge", "sqd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sqd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sqd_qty_transfer", False)
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

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "sq_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "SO Number", "sq_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Effective Date", "sq_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Exchange Rate", "sq_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Consignment", "sq_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "sq_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "sq_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Payment Date", "sq_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Close Date", "sq_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Remarks", "sq_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "sq_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPN", "sq_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPH", "sq_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "After Tax", "sq_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Ext. Total", "sq_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPN", "sq_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPH", "sq_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. After Tax", "sq_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "User Create", "sq_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "sq_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "sq_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "sq_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "sqd_sq_oid", False)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "sqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "sqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost", "sqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price", "sqd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "sqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "UM Conversion", "sqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty Real", "sqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Taxable", "sqd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Include", "sqd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Class", "sqd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "sqd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Payment", "sqd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Sales Unit", "sqd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Status", "sqd_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_smart, "Code", "sq_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sq_oid, " _
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
                    & "  cc_mstr_ar.cc_desc, " _
                    & "  cu_name,sq_ajb_code, " _
                    & "  tran_name,sq_sales_program,sq_status_produk,sq_diskon_produk,sq_unique_code, sq_dp_unique,sq_payment_unique, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM  " _
                    & "  public.sq_mstr " _
                    & "  inner join en_mstr on en_id = sq_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                    & "  inner join si_mstr on si_id = sq_si_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                    & "  left outer join pi_mstr on pi_id = sq_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
                    & "  left outer join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
                    & "  left outer join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sq_ar_ac_id " _
                    & "  left outer join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
                    & "  left outer join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
                    & "  inner join cu_mstr on cu_id = sq_cu_id " _
                    & "  left outer join tran_mstr on tran_id = sq_tran_id" _
                    & " where sq_cons='N' and  sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

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
        _sq_ptnr_id_sold_mstr = -1
        sq_en_id.Focus()
        sq_en_id.ItemIndex = 0
        sq_date.DateTime = _now
        sq_type.ItemIndex = 0
        sq_sales_person.ItemIndex = 0
        sq_ar_ac_id.ItemIndex = 0
        sq_cu_id.ItemIndex = 0
        sq_ar_cc_id.ItemIndex = 0
        sq_ar_sb_id.ItemIndex = 0
        sq_type.ItemIndex = 0
        sq_pi_id.ItemIndex = 0
        sq_pay_type.ItemIndex = 0
        sq_pay_method.ItemIndex = 0
        sq_ar_ac_id.ItemIndex = 0
        sq_cu_id.ItemIndex = 0
        sq_exc_rate.Text = 1
        sq_trans_rmks.Text = ""
        sq_tran_id.ItemIndex = 0
        sq_ptnr_id_sold.Text = ""
        sq_bantu_address.Text = ""
        sq_cons.EditValue = False

        sq_cu_id.Enabled = True
        sq_type.Enabled = True
        sq_pay_type.Enabled = True
        sq_pi_id.Enabled = True
        sq_ptnr_id_sold.Enabled = True
        sq_bantu_address.Enabled = True
        sq_ref_po_code.Text = ""
        _sq_ref_po_oid = ""
        sq_ref_po_code.Enabled = True

        sq_status_produk.Text = "Tidak Indent"
        sq_diskon_produk.Text = ""
        sq_sales_program.ItemIndex = 0


        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
            tcg_customer.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        sqa_kjb_code.Text = ""
        sqa_anak_nama_1.Text = ""
        sqa_anak_nama_2.Text = ""
        sqa_anak_nama_3.Text = ""
        sqa_anak_sekolah_1.Text = ""
        sqa_anak_sekolah_2.Text = ""
        sqa_anak_sekolah_3.Text = ""
        sqa_anak_tgl_lahir_1.Text = ""
        sqa_anak_tgl_lahir_2.Text = ""
        sqa_anak_tgl_lahir_3.Text = ""
        sqa_bank.Text = ""
        sqa_bekerja_pada.Text = ""
        sqa_berlaku_sd.Text = ""
        sqa_email.Text = ""
        sqa_jabatan_bagian.Text = ""
        sqa_jenis_kartu_kredit.Text = ""
        sqa_kantor_alamat_1.Text = ""
        sqa_kantor_alamat_2.Text = ""
        sqa_kantor_lantai.Text = ""
        sqa_kantor_telp.Text = ""
        sqa_keluarga_dekat_alamat_1.Text = ""
        sqa_keluarga_dekat_alamat_2.Text = ""
        sqa_keluarga_dekat_hp.Text = ""
        sqa_keluarga_dekat_nama.Text = ""
        sqa_keluarga_dekat_telp.Text = ""
        sqa_ktp.Text = ""
        sqa_no_kartu_kredit.Text = ""
        sqa_rumah_alamat_1.Text = ""
        sqa_rumah_alamat_2.Text = ""
        sqa_rumah_hp.Text = ""
        sqa_rumah_kode_pos.Text = ""
        sqa_rumah_telp.Text = ""
        sqa_status_alamat_kirim.EditValue = False
        sqa_status_alamat_tagih.EditValue = False
        sqa_status_tempat_tinggal.EditValue = False
        sqa_suami_bekerja.Text = ""
        sqa_suami_hp.Text = ""
        sqa_suami_jabatan.Text = ""
        sqa_suami_kantor_alamat_1.Text = ""
        sqa_suami_kantor_alamat_2.Text = ""
        sqa_suami_nama.Text = ""
        sqa_suami_telp.Text = ""
        sqa_status_rumah_id.ItemIndex = 0
        sqa_lama_tinggal_id.ItemIndex = 0
        sqa_masa_kerja_id.ItemIndex = 0
        sqa_income_id.ItemIndex = 0
        'sqa_kepribadian_id.ItemIndex = 0
        sqa_tanggungan_id.ItemIndex = 0
        sqa_jaminan_id.ItemIndex = 0


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
                        & "  sqd_qty_allocated, " _
                        & "  sqd_qty_picked, " _
                        & "  sqd_qty_shipment, " _
                        & "  sqd_qty_transfer_issue, " _
                        & "  sqd_qty_transfer_receipt, " _
                        & "  sqd_qty_pending_inv, " _
                        & "  sqd_qty_invoice, " _
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


        If Not sq_pay_type.GetColumnValue("code_name").ToString.ToLower.Contains("cash tempo") Then
            If sqa_bekerja_pada.Text.Trim = "" Then
                MessageBox.Show("Tempat Bekerja Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_bekerja_pada.Focus()
                before_save = False
            ElseIf sqa_masa_kerja_id.Text = "[EditValue is null]" Then
                MessageBox.Show("Masa Kerja Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_masa_kerja_id.Focus()
                before_save = False
            ElseIf sqa_income_id.Text = "[EditValue is null]" Then
                MessageBox.Show("Income Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_income_id.Focus()
                before_save = False
                'ElseIf sqa_kepribadian_id.Text = "[EditValue is null]" Then
                '    MessageBox.Show("Kepribadian Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    sqa_kepribadian_id.Focus()
                '    before_save = False
            ElseIf sqa_status_rumah_id.Text = "[EditValue is null]" Then
                MessageBox.Show("Status Rumah Tinggal Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_status_rumah_id.Focus()
                before_save = False
            ElseIf sqa_lama_tinggal_id.Text = "[EditValue is null]" Then
                MessageBox.Show("Lama Tinggal Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_lama_tinggal_id.Focus()
                before_save = False

            ElseIf sqa_tanggungan_id.Text = "[EditValue is null]" Then
                MessageBox.Show("Jumlah Tanggungan Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
                sqa_tanggungan_id.Focus()
                before_save = False
            End If
        End If


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

        If sq_pay_type.GetColumnValue("code_name").ToString.ToLower.Contains("cash tempo") Then
            If sqa_rumah_hp.EditValue.ToString.Length < 8 Then
                MessageBox.Show("Handphone number for customer cash can't empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            Try
                _sqd_qty_shipment = (gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty_shipment"))
            Catch ex As Exception
            End Try

            If e.Value < _sqd_qty_shipment Then
                MessageBox.Show("Qty SO Can't Lower Than Qty shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
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

            _sqd_qty_cost = (SetNumber(e.Value) * _sqd_qty) - (SetNumber(e.Value) * _sqd_qty * _sqd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            ' footer()
        ElseIf e.Column.Name = "sqd_disc" Then
            Try
                _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _sqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_cost")))
            Catch ex As Exception
            End Try

            _sqd_qty_cost = (_sqd_cost * _sqd_qty) - (_sqd_cost * _sqd_qty * SetNumber(e.Value))
            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_cost", _sqd_qty_cost)
            'footer()
        ElseIf e.Column.Name = "sqd_um_conv" Then
            Try
                _sqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "sqd_qty")))
            Catch ex As Exception
            End Try

            _sqd_qty_real = SetNumber(e.Value) * _sqd_qty

            gv_edit.SetRowCellValue(e.RowHandle, "sqd_qty_real", _sqd_qty_real)
            'footer()
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
            dt_bantu = (load_price_list(sq_pi_id.EditValue, SetNumber(e.Value), sq_pay_type.EditValue, _sqd_qty))

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

            _sqd_qty = SetNumber(e.Value)

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
        Dim _sqd_en_id As Integer = gv_edit.GetRowCellValue(_row, "sqd_en_id")
        Dim _sqd_si_id As Integer = gv_edit.GetRowCellValue(_row, "sqd_si_id")

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
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sqd_en_id
            frm.type_form = True
            frm.ShowDialog()
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
        'sq_pi_id.Enabled = False
        sq_pay_type.Enabled = False
    End Sub

#End Region

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    'Private Function GetID_Local(ByVal par_en_code As String) As Integer
    '    Try
    '        Using objcb As New master_new.WDABasepgsql("", "")
    '            With objcb
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text

    '                .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
    '                                       " where substring(cast(ptnr_id as varchar),5,100) <> '' and ptnr_en_id=(select en_id from en_mstr where en_code='" _
    '                                       & par_en_code & "') and substring(cast(ptnr_id as varchar),3,2)='" & master_new.ClsVar.sServerCode & "'"



    '                .InitializeCommand()

    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read

    '                    GetID_Local = .DataReader("max_col") + 1
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    If par_en_code = "0" Then
    '        par_en_code = "99"
    '    End If

    '    GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

    '    Return GetID_Local
    'End Function

    Private Function GetuniqueID_Local(ByVal par_en_id As String) As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(sq_unique_code as varchar),3,100) as integer)),0) as max_col  from sq_mstr " + _
                                           " where substring(cast(sq_unique_code as varchar),3,100) <> '' and sq_en_id=" & par_en_id _
                                           & " and sq_oid=(select sq_oid from sq_mstr where sq_add_date=(select max(sq_add_date) as dt from sq_mstr where sq_en_id=" & par_en_id _
                                           & "  ) order by sq_unique_code desc limit 1 )"
                    .InitializeCommand()

                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read

                        GetuniqueID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If GetuniqueID_Local = "999" Then
            GetuniqueID_Local = "1"
        End If

        If par_en_id = "0" Then
            par_en_id = "99"
        End If

        GetuniqueID_Local = CInt(par_en_id + GetuniqueID_Local.ToString)

        Return GetuniqueID_Local
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
        _kjb_code = func_coll.get_transaction_number("KJ", sq_en_id.GetColumnValue("en_code"), "sq_mstr", "sq_code")
        sqa_kjb_code.Text = _kjb_code

        ssqls.Clear()

        Dim _sq_trn_status As String
        Dim ds_bantu As New DataSet

        If sq_pay_type.GetColumnValue("code_usr_1") = 0 Then
            '_sq_trn_status = "C" 'Lansung Default Ke Close
            _sq_trn_status = "D"
        Else
            _sq_trn_status = "D" 'Lansung Default Ke Draft
        End If


        ds_bantu = func_data.load_aprv_mstr(sq_tran_id.EditValue)

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
        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sq_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sq_en_id.EditValue.ToString))

        _ptnr_id = SetInteger(GetID_Local(sq_en_id.GetColumnValue("en_code")))
        Dim _unique_id As String = "0"

        If sq_pay_type.GetColumnValue("code_name").ToString.ToLower.Contains("cash tempo") Then
            _unique_id = SetNumber(GetuniqueID_Local(sq_en_id.EditValue))
        End If

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
        Dim _barang As String = ""
        Dim _id_ptnr_sq As Integer

        If _sq_ptnr_id_sold_mstr = -1 Then
            _id_ptnr_sq = _ptnr_id
        Else
            _id_ptnr_sq = _sq_ptnr_id_sold_mstr
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
                        If _sq_ptnr_id_sold_mstr = -1 Then
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
                                                & "  ptnr_transaction_code_id," _
                                                & "  ptnr_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(sq_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & _ptnr_id & ",  " _
                                                & SetSetring(_ptnr_code) & ",  " _
                                                & SetSetring(sq_ptnr_id_sold.Text) & ",  " _
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
                                                & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
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
                                                & SetInteger(func_coll.GetID("ptnra_addr", sq_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", sq_en_id.EditValue.ToString)) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(sq_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", sq_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                & SetSetring(sq_bantu_address.Text) & ",  " _
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

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnratt_attr " _
                                                & "( " _
                                                & "  ptnratt_oid, " _
                                                & "  ptnratt_kjb_code, " _
                                                & "  ptnratt_bekerja_pada, " _
                                                & "  ptnratt_jabatan_bagian, " _
                                                & "  ptnratt_kantor_alamat_1, " _
                                                & "  ptnratt_kantor_alamat_2, " _
                                                & "  ptnratt_kantor_lantai, " _
                                                & "  ptnratt_kantor_telp, " _
                                                & "  ptnratt_ktp, " _
                                                & "  ptnratt_email, " _
                                                & "  ptnratt_rumah_alamat_1, " _
                                                & "  ptnratt_rumah_alamat_2, " _
                                                & "  ptnratt_rumah_kode_pos, " _
                                                & "  ptnratt_rumah_telp, " _
                                                & "  ptnratt_rumah_hp, " _
                                                & "  ptnratt_status_alamat_kirim, " _
                                                & "  ptnratt_status_alamat_tagih, " _
                                                & "  ptnratt_suami_nama, " _
                                                & "  ptnratt_suami_bekerja, " _
                                                & "  ptnratt_suami_jabatan, " _
                                                & "  ptnratt_suami_kantor_alamat_1, " _
                                                & "  ptnratt_suami_kantor_alamat_2, " _
                                                & "  ptnratt_suami_telp, " _
                                                & "  ptnratt_suami_hp, " _
                                                & "  ptnratt_anak_nama_1, " _
                                                & "  ptnratt_anak_tgl_lahir_1, " _
                                                & "  ptnratt_anak_sekolah_1, " _
                                                & "  ptnratt_anak_nama_2, " _
                                                & "  ptnratt_anak_tgl_lahir_2, " _
                                                & "  ptnratt_anak_sekolah_2, " _
                                                & "  ptnratt_anak_nama_3, " _
                                                & "  ptnratt_anak_tgl_lahir_3, " _
                                                & "  ptnratt_anak_sekolah_3, " _
                                                & "  ptnratt_keluarga_dekat_nama, " _
                                                & "  ptnratt_keluarga_dekat_alamat_1, " _
                                                & "  ptnratt_keluarga_dekat_alamat_2, " _
                                                & "  ptnratt_keluarga_dekat_telp, " _
                                                & "  ptnratt_keluarga_dekat_hp, " _
                                                & "  ptnratt_status_tempat_tinggal, " _
                                                & "  ptnratt_jenis_kartu_kredit, " _
                                                & "  ptnratt_no_kartu_kredit, " _
                                                & "  ptnratt_berlaku_sd, " _
                                                & "  ptnratt_dt, " _
                                                & "  ptnratt_bank, " _
                                                & "  ptnratt_ptnr_oid, " _
                                                & "  ptnratt_status_rumah_id, " _
                                                & "  ptnratt_lama_tinggal_id, " _
                                                & "  ptnratt_masa_kerja_id, " _
                                                & "  ptnratt_income_id, " _
                                                & "  ptnratt_tanggungan_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(sqa_kjb_code.Text) & ",  " _
                                                & SetSetring(sqa_bekerja_pada.Text) & ",  " _
                                                & SetSetring(sqa_jabatan_bagian.Text) & ",  " _
                                                & SetSetring(sqa_kantor_alamat_1.Text) & ",  " _
                                                & SetSetring(sqa_kantor_alamat_2.Text) & ",  " _
                                                & SetSetring(sqa_kantor_lantai.Text) & ",  " _
                                                & SetSetring(sqa_kantor_telp.Text) & ",  " _
                                                & SetSetring(sqa_ktp.Text) & ",  " _
                                                & SetSetring(sqa_email.Text) & ",  " _
                                                & SetSetring(sqa_rumah_alamat_1.Text) & ",  " _
                                                & SetSetring(sqa_rumah_alamat_2.Text) & ",  " _
                                                & SetSetring(sqa_rumah_kode_pos.Text) & ",  " _
                                                & SetSetring(sqa_rumah_telp.Text) & ",  " _
                                                & SetSetring(sqa_rumah_hp.Text) & ",  " _
                                                & SetBitYN(sqa_status_alamat_kirim.EditValue) & ",  " _
                                                & SetBitYN(sqa_status_alamat_tagih.EditValue) & ",  " _
                                                & SetSetring(sqa_suami_nama.Text) & ",  " _
                                                & SetSetring(sqa_suami_bekerja.Text) & ",  " _
                                                & SetSetring(sqa_suami_jabatan.Text) & ",  " _
                                                & SetSetring(sqa_suami_kantor_alamat_1.Text) & ",  " _
                                                & SetSetring(sqa_suami_kantor_alamat_2.Text) & ",  " _
                                                & SetSetring(sqa_suami_telp.Text) & ",  " _
                                                & SetSetring(sqa_suami_hp.Text) & ",  " _
                                                & SetSetring(sqa_anak_nama_1.Text) & ",  " _
                                                & SetDate(sqa_anak_tgl_lahir_1.DateTime) & ",  " _
                                                & SetSetring(sqa_anak_sekolah_1.Text) & ",  " _
                                                & SetSetring(sqa_anak_nama_2.Text) & ",  " _
                                                & SetDate(sqa_anak_tgl_lahir_2.DateTime) & ",  " _
                                                & SetSetring(sqa_anak_sekolah_2.Text) & ",  " _
                                                & SetSetring(sqa_anak_nama_3.Text) & ",  " _
                                                & SetDate(sqa_anak_tgl_lahir_3.DateTime) & ",  " _
                                                & SetSetring(sqa_anak_sekolah_3.Text) & ",  " _
                                                & SetSetring(sqa_keluarga_dekat_nama.Text) & ",  " _
                                                & SetSetring(sqa_keluarga_dekat_alamat_1.Text) & ",  " _
                                                & SetSetring(sqa_keluarga_dekat_alamat_2.Text) & ",  " _
                                                & SetSetring(sqa_keluarga_dekat_telp.Text) & ",  " _
                                                & SetSetring(sqa_keluarga_dekat_hp.Text) & ",  " _
                                                & SetBitYN(sqa_status_tempat_tinggal.EditValue) & ",  " _
                                                & SetSetring(sqa_jenis_kartu_kredit.Text) & ",  " _
                                                & SetSetring(sqa_no_kartu_kredit.Text) & ",  " _
                                                & SetSetring(sqa_berlaku_sd.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(sqa_bank.Text) & ",  " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & SetInteger(sqa_status_rumah_id.EditValue) & ",  " _
                                                & SetInteger(sqa_lama_tinggal_id.EditValue) & ",  " _
                                                & SetInteger(sqa_masa_kerja_id.EditValue) & ",  " _
                                                & SetInteger(sqa_income_id.EditValue) & ",  " _
                                                & SetInteger(sqa_tanggungan_id.EditValue) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If
                        '******************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sq_mstr " _
                                            & "( " _
                                            & "  sq_oid, " _
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
                                            & "  sq_need_date,sq_due_date, " _
                                            & "  sq_tran_id, " _
                                            & "  sq_trans_id, " _
                                            & "  sq_trans_rmks, " _
                                            & "  sq_dt, " _
                                            & "  sq_cu_id, " _
                                            & "  sq_total_ppn, " _
                                            & "  sq_total_pph, " _
                                            & "  sq_payment, " _
                                            & "  sq_exc_rate, " _
                                            & "  sq_interval,sq_sales_program,sq_status_produk,sq_diskon_produk,sq_unique_code, sq_dp_unique,sq_payment_unique, " _
                                            & "  sq_terbilang " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sq_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sq_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_sq_code) & ",  " _
                                            & SetInteger(_id_ptnr_sq) & ",  " _
                                            & SetInteger(_id_ptnr_sq) & ",  " _
                                            & SetSetring(sq_ref_po_code.Text) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetDate(sq_date.DateTime) & ",  " _
                                            & SetInteger(sq_si_id.EditValue) & ",  " _
                                            & SetSetring(sq_type.EditValue) & ",  " _
                                            & SetInteger(sq_sales_person.EditValue) & ",  " _
                                            & SetInteger(sq_pi_id.EditValue) & ",  " _
                                            & SetInteger(sq_pay_type.EditValue) & ",  " _
                                            & SetInteger(sq_pay_method.EditValue) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                            & SetDbl(_total_dp) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(_sq_total) & ",  " _
                                            & SetDate(sq_need_date.DateTime) & ",  " _
                                             & SetDate(sq_due_date.DateTime) & ",  " _
                                            & SetInteger(sq_tran_id.EditValue) & ",  " _
                                            & SetSetring(_sq_trn_status) & ",  " _
                                            & SetSetring(sq_trans_rmks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(sq_cu_id.EditValue) & ",  " _
                                            & SetDbl(_sq_total_ppn) & ",  " _
                                            & SetDbl(_sq_total_pph) & ",  " _
                                            & SetDbl(_total_payment) & ",  " _
                                            & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                            & SetInteger(sq_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                             & SetSetring(sq_sales_program.EditValue) & ",  " _
                                                & SetSetring(sq_status_produk.EditValue) & ",  " _
                                                & SetSetring(sq_diskon_produk.EditValue) & ",  " _
                                                 & SetInteger(_unique_id) & ",  " _
                                            & SetDbl(IIf(_unique_id <> "0", IIf(_total_dp > 0, _total_dp - CDbl(_unique_id), 0), _total_dp)) & ",  " _
                                            & SetDbl(IIf(_unique_id <> "0", _total_payment - CDbl(_unique_id), _total_payment)) & ",  " _
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

                            _barang = _barang & ds_edit.Tables(0).Rows(i).Item("pt_desc1") & " "

                            ''If SetString(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid").ToString) <> "" Then
                            ''    'update karena ada hubungan antara so dan po antar group perusahaan
                            ''    .Command.CommandType = CommandType.Text
                            ''    .Command.CommandText = "update pod_det set pod_qty_sq = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                            ''                         & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid"))
                            ''    ssqls.Add(.Command.CommandText)
                            ''    .Command.ExecuteNonQuery()
                            ''    .Command.Parameters.Clear()
                            ''End If
                        Next

                        'If Trim(sqa_bekerja_pada.Text) <> "" Then
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sqa_attr " _
                                            & "( " _
                                            & "  sqa_oid, " _
                                            & "  sqa_kjb_code, " _
                                            & "  sqa_bekerja_pada, " _
                                            & "  sqa_jabatan_bagian, " _
                                            & "  sqa_kantor_alamat_1, " _
                                            & "  sqa_kantor_alamat_2, " _
                                            & "  sqa_kantor_lantai, " _
                                            & "  sqa_kantor_telp, " _
                                            & "  sqa_ktp, " _
                                            & "  sqa_email, " _
                                            & "  sqa_rumah_alamat_1, " _
                                            & "  sqa_rumah_alamat_2, " _
                                            & "  sqa_rumah_kode_pos, " _
                                            & "  sqa_rumah_telp, " _
                                            & "  sqa_rumah_hp, " _
                                            & "  sqa_status_alamat_kirim, " _
                                            & "  sqa_status_alamat_tagih, " _
                                            & "  sqa_suami_nama, " _
                                            & "  sqa_suami_bekerja, " _
                                            & "  sqa_suami_jabatan, " _
                                            & "  sqa_suami_kantor_alamat_1, " _
                                            & "  sqa_suami_kantor_alamat_2, " _
                                            & "  sqa_suami_telp, " _
                                            & "  sqa_suami_hp, " _
                                            & "  sqa_anak_nama_1, " _
                                            & "  sqa_anak_tgl_lahir_1, " _
                                            & "  sqa_anak_sekolah_1, " _
                                            & "  sqa_anak_nama_2, " _
                                            & "  sqa_anak_tgl_lahir_2, " _
                                            & "  sqa_anak_sekolah_2, " _
                                            & "  sqa_anak_nama_3, " _
                                            & "  sqa_anak_tgl_lahir_3, " _
                                            & "  sqa_anak_sekolah_3, " _
                                            & "  sqa_keluarga_dekat_nama, " _
                                            & "  sqa_keluarga_dekat_alamat_1, " _
                                            & "  sqa_keluarga_dekat_alamat_2, " _
                                            & "  sqa_keluarga_dekat_telp, " _
                                            & "  sqa_keluarga_dekat_hp, " _
                                            & "  sqa_status_tempat_tinggal, " _
                                            & "  sqa_jenis_kartu_kredit, " _
                                            & "  sqa_no_kartu_kredit, " _
                                            & "  sqa_berlaku_sd, " _
                                            & "  sqa_dt, " _
                                            & "  sqa_bank, " _
                                            & "  sqa_sq_oid, " _
                                            & "  sqa_status_rumah_id, " _
                                            & "  sqa_lama_tinggal_id, " _
                                            & "  sqa_masa_kerja_id, " _
                                            & "  sqa_income_id, " _
                                            & "  sqa_tanggungan_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(sqa_kjb_code.Text) & ",  " _
                                            & SetSetring(sqa_bekerja_pada.Text) & ",  " _
                                            & SetSetring(sqa_jabatan_bagian.Text) & ",  " _
                                            & SetSetring(sqa_kantor_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_kantor_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_kantor_lantai.Text) & ",  " _
                                            & SetSetring(sqa_kantor_telp.Text) & ",  " _
                                            & SetSetring(sqa_ktp.Text) & ",  " _
                                            & SetSetring(sqa_email.Text) & ",  " _
                                            & SetSetring(sqa_rumah_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_rumah_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_rumah_kode_pos.Text) & ",  " _
                                            & SetSetring(sqa_rumah_telp.Text) & ",  " _
                                            & SetSetring(sqa_rumah_hp.Text) & ",  " _
                                            & SetBitYN(sqa_status_alamat_kirim.EditValue) & ",  " _
                                            & SetBitYN(sqa_status_alamat_tagih.EditValue) & ",  " _
                                            & SetSetring(sqa_suami_nama.Text) & ",  " _
                                            & SetSetring(sqa_suami_bekerja.Text) & ",  " _
                                            & SetSetring(sqa_suami_jabatan.Text) & ",  " _
                                            & SetSetring(sqa_suami_kantor_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_suami_kantor_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_suami_telp.Text) & ",  " _
                                            & SetSetring(sqa_suami_hp.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_1.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_1.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_1.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_2.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_2.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_2.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_3.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_3.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_3.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_nama.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_telp.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_hp.Text) & ",  " _
                                            & SetBitYN(sqa_status_tempat_tinggal.EditValue) & ",  " _
                                            & SetSetring(sqa_jenis_kartu_kredit.Text) & ",  " _
                                            & SetSetring(sqa_no_kartu_kredit.Text) & ",  " _
                                            & SetSetring(sqa_berlaku_sd.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(sqa_bank.Text) & ",  " _
                                            & SetSetring(_sq_oid.ToString) & ",  " _
                                            & SetInteger(sqa_status_rumah_id.EditValue) & ",  " _
                                            & SetInteger(sqa_lama_tinggal_id.EditValue) & ",  " _
                                            & SetInteger(sqa_masa_kerja_id.EditValue) & ",  " _
                                            & SetInteger(sqa_income_id.EditValue) & ",  " _
                                            & SetInteger(sqa_tanggungan_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _oid_ptnr As String
                        If _ptnratt_ptnr_oid.ToString.Trim = "" Then
                            _oid_ptnr = SetSetring(_ptnr_oid.ToString)

                        Else
                            _oid_ptnr = SetSetring(_ptnratt_ptnr_oid)
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from ptnratt_attr where ptnratt_ptnr_oid = " & _oid_ptnr & ""
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.ptnratt_attr " _
                                            & "( " _
                                            & "  ptnratt_oid, " _
                                            & "  ptnratt_kjb_code, " _
                                            & "  ptnratt_bekerja_pada, " _
                                            & "  ptnratt_jabatan_bagian, " _
                                            & "  ptnratt_kantor_alamat_1, " _
                                            & "  ptnratt_kantor_alamat_2, " _
                                            & "  ptnratt_kantor_lantai, " _
                                            & "  ptnratt_kantor_telp, " _
                                            & "  ptnratt_ktp, " _
                                            & "  ptnratt_email, " _
                                            & "  ptnratt_rumah_alamat_1, " _
                                            & "  ptnratt_rumah_alamat_2, " _
                                            & "  ptnratt_rumah_kode_pos, " _
                                            & "  ptnratt_rumah_telp, " _
                                            & "  ptnratt_rumah_hp, " _
                                            & "  ptnratt_status_alamat_kirim, " _
                                            & "  ptnratt_status_alamat_tagih, " _
                                            & "  ptnratt_suami_nama, " _
                                            & "  ptnratt_suami_bekerja, " _
                                            & "  ptnratt_suami_jabatan, " _
                                            & "  ptnratt_suami_kantor_alamat_1, " _
                                            & "  ptnratt_suami_kantor_alamat_2, " _
                                            & "  ptnratt_suami_telp, " _
                                            & "  ptnratt_suami_hp, " _
                                            & "  ptnratt_anak_nama_1, " _
                                            & "  ptnratt_anak_tgl_lahir_1, " _
                                            & "  ptnratt_anak_sekolah_1, " _
                                            & "  ptnratt_anak_nama_2, " _
                                            & "  ptnratt_anak_tgl_lahir_2, " _
                                            & "  ptnratt_anak_sekolah_2, " _
                                            & "  ptnratt_anak_nama_3, " _
                                            & "  ptnratt_anak_tgl_lahir_3, " _
                                            & "  ptnratt_anak_sekolah_3, " _
                                            & "  ptnratt_keluarga_dekat_nama, " _
                                            & "  ptnratt_keluarga_dekat_alamat_1, " _
                                            & "  ptnratt_keluarga_dekat_alamat_2, " _
                                            & "  ptnratt_keluarga_dekat_telp, " _
                                            & "  ptnratt_keluarga_dekat_hp, " _
                                            & "  ptnratt_status_tempat_tinggal, " _
                                            & "  ptnratt_jenis_kartu_kredit, " _
                                            & "  ptnratt_no_kartu_kredit, " _
                                            & "  ptnratt_berlaku_sd, " _
                                            & "  ptnratt_dt, " _
                                            & "  ptnratt_bank, " _
                                            & "  ptnratt_ptnr_oid, " _
                                            & "  ptnratt_status_rumah_id, " _
                                            & "  ptnratt_lama_tinggal_id, " _
                                            & "  ptnratt_masa_kerja_id, " _
                                            & "  ptnratt_income_id, " _
                                            & "  ptnratt_tanggungan_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(sqa_kjb_code.Text) & ",  " _
                                            & SetSetring(sqa_bekerja_pada.Text) & ",  " _
                                            & SetSetring(sqa_jabatan_bagian.Text) & ",  " _
                                            & SetSetring(sqa_kantor_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_kantor_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_kantor_lantai.Text) & ",  " _
                                            & SetSetring(sqa_kantor_telp.Text) & ",  " _
                                            & SetSetring(sqa_ktp.Text) & ",  " _
                                            & SetSetring(sqa_email.Text) & ",  " _
                                            & SetSetring(sqa_rumah_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_rumah_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_rumah_kode_pos.Text) & ",  " _
                                            & SetSetring(sqa_rumah_telp.Text) & ",  " _
                                            & SetSetring(sqa_rumah_hp.Text) & ",  " _
                                            & SetBitYN(sqa_status_alamat_kirim.EditValue) & ",  " _
                                            & SetBitYN(sqa_status_alamat_tagih.EditValue) & ",  " _
                                            & SetSetring(sqa_suami_nama.Text) & ",  " _
                                            & SetSetring(sqa_suami_bekerja.Text) & ",  " _
                                            & SetSetring(sqa_suami_jabatan.Text) & ",  " _
                                            & SetSetring(sqa_suami_kantor_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_suami_kantor_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_suami_telp.Text) & ",  " _
                                            & SetSetring(sqa_suami_hp.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_1.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_1.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_1.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_2.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_2.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_2.Text) & ",  " _
                                            & SetSetring(sqa_anak_nama_3.Text) & ",  " _
                                            & SetDate(sqa_anak_tgl_lahir_3.DateTime) & ",  " _
                                            & SetSetring(sqa_anak_sekolah_3.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_nama.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_alamat_1.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_alamat_2.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_telp.Text) & ",  " _
                                            & SetSetring(sqa_keluarga_dekat_hp.Text) & ",  " _
                                            & SetBitYN(sqa_status_tempat_tinggal.EditValue) & ",  " _
                                            & SetSetring(sqa_jenis_kartu_kredit.Text) & ",  " _
                                            & SetSetring(sqa_no_kartu_kredit.Text) & ",  " _
                                            & SetSetring(sqa_berlaku_sd.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(sqa_bank.Text) & ",  " _
                                            & _oid_ptnr & ",  " _
                                            & SetInteger(sqa_status_rumah_id.EditValue) & ",  " _
                                            & SetInteger(sqa_lama_tinggal_id.EditValue) & ",  " _
                                            & SetInteger(sqa_masa_kerja_id.EditValue) & ",  " _
                                            & SetInteger(sqa_income_id.EditValue) & ",  " _
                                            & SetInteger(sqa_tanggungan_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'End If

                        If sq_type.EditValue.ToString.ToUpper = "D" Then
                            If insert_sokp_piutang(objinsert, _sq_oid.ToString, _sq_code, _total_dp, _total_payment) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If


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

                        If _conf_value = "1" Then
                            'If ds_bantu.Tables(0).Rows.Count = 0 Then
                            '    Box("Data Aprove empty")
                            'End If
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
                                                        & SetInteger(sq_en_id.EditValue) & ",  " _
                                                        & SetSetring(sq_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_sq_oid.ToString) & ",  " _
                                                        & SetSetring(_sq_code) & ",  " _
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

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, sq_en_id.EditValue, 10, _sq_oid.ToString, _sq_code, sq_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SO " & _sq_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If sq_pay_type.GetColumnValue("code_name").ToString.ToLower.Contains("cash tempo") Then

                        '    Dim _kata As String = ""

                        '    Dim _pembayaran As String = ""
                        '    If _total_dp > 0 Then
                        '        _pembayaran = " DP Rp. " & MaskDec(_total_dp - CDbl(_unique_id), 0) & " dan pelunasan Rp. " & MaskDec(_total_payment - CDbl(_unique_id), 0)
                        '    Else
                        '        _pembayaran = "  pembayaran Rp. " & MaskDec(_total_payment - CDbl(_unique_id), 0)
                        '    End If


                        '    _kata = "Terimakasih Bpk/Ibu " & sq_ptnr_id_sold.Text & " telah membeli buku " & _barang & " dengan No AJB " & sqa_kjb_code.Text _
                        '            & ", silahkan untuk segera mentransfer " & _pembayaran & " ke rekening Bank Muamalat a/c " _
                        '            & master_new.PGSqlConn.GetRowInfo("select en_bank_number_sms from en_mstr where en_id=" & sq_en_id.EditValue)(0).ToString _
                        '            & " a/n PT SYGMA DAYA INSANI, Terimakasih (PT SDI)"

                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                & "  public.sms_outbox " _
                        '                & "( " _
                        '                & "  sms_oid, " _
                        '                & "  sms_reff_no, " _
                        '                & "  sms_type, " _
                        '                & "  sms_phone_number, " _
                        '                & "  sms_text, " _
                        '                & "  sms_status, " _
                        '                & "  sms_add_date, " _
                        '                & "  sms_add_by " _
                        '                & ")  " _
                        '                & "VALUES ( " _
                        '                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                & SetSetring(_sq_code) & ",  " _
                        '                & "  'sq', " _
                        '                & SetSetring(sqa_rumah_hp.EditValue) & ",  " _
                        '                & SetSetring(_kata) & ",  " _
                        '                & "  'N', " _
                        '                & "  current_timestamp, " _
                        '                & SetSetring(master_new.ClsVar.sNama) & "  " _
                        '                & ")"

                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()

                        '    'update rekonsiliasi kas masuk
                        '    'If update_rec(objinsert, ssqls, sq_en_id.EditValue, sq_bk_id.EditValue, sq_cu_id.EditValue, _
                        '    '    sq_exc_rate.EditValue, _sq_total + _sq_total_ppn + _sq_total_pph, sq_date.DateTime, _sq_code, _
                        '    '    sq_ptnr_id_sold.Text, "SO CASH") = False Then
                        '    '    Return False
                        '    '    Exit Function
                        '    'End If
                        'End If


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
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    'Private Function insert_shipment(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_sq_code As String) As Boolean
    '    '    insert_shipment = True
    '    '    Dim _soship_oid As Guid
    '    '    Dim _soship_code, _serial, _pt_code As String
    '    '    'Dim _cost_methode As String 
    '    '    Dim _en_id, _si_id, _loc_id, _pt_id, _qty As Integer
    '    '    Dim _tran_id As Integer
    '    '    Dim _cost, _cost_avg As Double
    '    '    Dim i, i_2 As Integer
    '    '    Dim ds_bantu_ry As New DataSet

    '    '    _soship_oid = Guid.NewGuid

    '    '    _soship_code = func_coll.get_transaction_number("SS", sq_en_id.GetColumnValue("en_code"), "soship_mstr", "soship_code")

    '    '    _tran_id = func_coll.get_id_tran_mstr("iss-so")
    '    '    If _tran_id = -1 Then
    '    '        MessageBox.Show("Sales Order Shipment In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '        Return False
    '    '    End If

    '    '    With par_obj
    '    '        .Command.CommandType = CommandType.Text
    '    '        .Command.CommandText = "INSERT INTO  " _
    '    '                            & "  public.soship_mstr " _
    '    '                            & "( " _
    '    '                            & "  soship_oid, " _
    '    '                            & "  soship_dom_id, " _
    '    '                            & "  soship_en_id, " _
    '    '                            & "  soship_add_by, " _
    '    '                            & "  soship_add_date, " _
    '    '                            & "  soship_code, " _
    '    '                            & "  soship_date, " _
    '    '                            & "  soship_sq_oid, " _
    '    '                            & "  soship_si_id, " _
    '    '                            & "  soship_is_shipment, " _
    '    '                            & "  soship_dt " _
    '    '                            & ")  " _
    '    '                            & "VALUES ( " _
    '    '                            & SetSetring(_soship_oid.ToString) & ",  " _
    '    '                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '    '                            & SetInteger(sq_en_id.EditValue) & ",  " _
    '    '                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '    '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '    '                            & SetSetring(_soship_code) & ",  " _
    '    '                            & SetDate(sq_date.DateTime) & ",  " _
    '    '                            & SetSetring(par_sq_oid.ToString) & ",  " _
    '    '                            & SetInteger(sq_si_id.EditValue) & ",  " _
    '    '                            & SetSetring("Y") & ",  " _
    '    '                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
    '    '                            & ")"
    '    '        ssqls.Add(.Command.CommandText)
    '    '        .Command.ExecuteNonQuery()
    '    '        .Command.Parameters.Clear()

    '    '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '    '            .Command.CommandType = CommandType.Text
    '    '            .Command.CommandText = "INSERT INTO  " _
    '    '                                & "  public.soshipd_det " _
    '    '                                & "( " _
    '    '                                & "  soshipd_oid, " _
    '    '                                & "  soshipd_soship_oid, " _
    '    '                                & "  soshipd_sqd_oid, " _
    '    '                                & "  soshipd_seq, " _
    '    '                                & "  soshipd_qty, " _
    '    '                                & "  soshipd_um, " _
    '    '                                & "  soshipd_um_conv, " _
    '    '                                & "  soshipd_qty_real, " _
    '    '                                & "  soshipd_si_id, " _
    '    '                                & "  soshipd_loc_id, " _
    '    '                                & "  soshipd_lot_serial, " _
    '    '                                & "  soshipd_rea_code_id, " _
    '    '                                & "  soshipd_dt " _
    '    '                                & ")  " _
    '    '                                & "VALUES ( " _
    '    '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '    '                                & SetSetring(_soship_oid.ToString) & ",  " _
    '    '                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString) & ",  " _
    '    '                                & SetInteger(i) & ",  " _
    '    '                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty") * -1) & ",  " _
    '    '                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
    '    '                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
    '    '                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") * -1) & ",  " _
    '    '                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
    '    '                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
    '    '                                & SetSetringDB("") & ",  " _
    '    '                                & " null " & ",  " _
    '    '                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
    '    '                                & ")"
    '    '            ssqls.Add(.Command.CommandText)
    '    '            .Command.ExecuteNonQuery()
    '    '            .Command.Parameters.Clear()

    '    '            'Update sqd_det
    '    '            .Command.CommandType = CommandType.Text
    '    '            .Command.CommandText = "update sqd_det set sqd_qty_shipment = coalesce(sqd_qty_shipment,0) + " + ds_edit.Tables(0).Rows(i).Item("sqd_qty").ToString _
    '    '                                 & " where sqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("sqd_oid").ToString + "'"
    '    '            ssqls.Add(.Command.CommandText)
    '    '            .Command.ExecuteNonQuery()
    '    '            .Command.Parameters.Clear()
    '    '        Next

    '    '        'Update Table Inventory Dan Cost Inventory Dan History Inventory
    '    '        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
    '    '        i_2 = 0
    '    '        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '    '            If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
    '    '                If ds_edit.Tables(0).Rows(i).Item("sqd_qty_real") > 0 Then
    '    '                    If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
    '    '                        i_2 += 1

    '    '                        _en_id = ds_edit.Tables(0).Rows(i).Item("sqd_en_id")
    '    '                        _si_id = ds_edit.Tables(0).Rows(i).Item("sqd_si_id")
    '    '                        _loc_id = ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")
    '    '                        _pt_id = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
    '    '                        _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
    '    '                        _serial = "''"
    '    '                        _qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")
    '    '                        If func_coll.update_invc_mstr_minus(ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
    '    '                            Return False
    '    '                        End If

    '    '                        'Update History Inventory                        
    '    '                        _qty = _qty * -1
    '    '                        _cost = ds_edit.Tables(0).Rows(i).Item("sqd_cost") - (ds_edit.Tables(0).Rows(i).Item("sqd_cost") * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
    '    '                        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
    '    '                        If func_coll.update_invh_mstr(ssqls, par_obj, _tran_id, i_2, _en_id, _soship_code, _soship_oid.ToString, "SO Shipment", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", sq_date.DateTime) = False Then
    '    '                            Return False
    '    '                        End If
    '    '                    Else
    '    '                        MessageBox.Show("Error Serial..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '                        Return False
    '    '                    End If
    '    '                End If
    '    '            End If
    '    '        Next

    '    '        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
    '    '        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '    '        '    If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
    '    '        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id").ToString.ToUpper)
    '    '        '        _en_id = ds_edit.Tables(0).Rows(i).Item("sqd_en_id")
    '    '        '        _si_id = ds_edit.Tables(0).Rows(i).Item("sqd_si_id")
    '    '        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
    '    '        '        _qty = ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")
    '    '        '        _cost = ds_edit.Tables(0).Rows(i).Item("sqd_cost") - (ds_edit.Tables(0).Rows(i).Item("sqd_cost") * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))

    '    '        '        If _cost_methode = "F" Or _cost_methode = "L" Then
    '    '        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
    '    '        '            Return False
    '    '        '            'If func_coll.update_invct_table_minus(ssqls, par_obj, _en_id, _pt_id, _qty, _cost_methode) = False Then
    '    '        '            '    Return False
    '    '        '            'End If
    '    '        '        ElseIf _cost_methode = "A" Then
    '    '        '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
    '    '        '            If func_coll.update_item_cost_avg(ssqls, par_obj, _si_id, _pt_id, _cost_avg) = False Then
    '    '        '                Return False
    '    '        '            End If
    '    '        '        End If
    '    '        '    End If
    '    '        'Next

    '    '        ' TIdak jadi karena sudah ada di menu khusus untuk penghitungan royalti
    '    '        ' ''Update Ke Table Royalti 'soshipd_qty
    '    '        ''For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '    '        ''    _soshipd_qty_real = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")

    '    '        ''    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
    '    '        ''        Try
    '    '        ''            Using objcb As New master_new.WDABasepgsql("", "")
    '    '        ''                With objcb
    '    '        ''                    .SQL = "select royt_oid, royt_pt_id, royt_seq, royt_qty_royalti - royt_qty_so as royt_qty_open " + _
    '    '        ''                       " from royt_table " + _
    '    '        ''                       " where royt_qty_royalti > royt_qty_so " + _
    '    '        ''                       " and royt_pt_id  = " + ds_edit.Tables(0).Rows(i).Item("pt_id").ToString + _
    '    '        ''                       " order by royt_seq "
    '    '        ''                    .InitializeCommand()
    '    '        ''                    .FillDataSet(ds_bantu_ry, "royalti")
    '    '        ''                End With
    '    '        ''            End Using
    '    '        ''        Catch ex As Exception
    '    '        ''            MessageBox.Show(ex.Message)
    '    '        ''        End Try

    '    '        ''        For j = 0 To ds_bantu_ry.Tables(0).Rows.Count - 1
    '    '        ''            If _soshipd_qty_real > ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open") Then
    '    '        ''                .Command.CommandType = CommandType.Text
    '    '        ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open").ToString _
    '    '        ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
    '    '        ''                ssqls.Add(.Command.CommandText)
    '    '        ''                .Command.ExecuteNonQuery()
    '    '        ''                .Command.Parameters.Clear()

    '    '        ''                _soshipd_qty_real = _soshipd_qty_real - ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open")
    '    '        ''            Else
    '    '        ''                .Command.CommandType = CommandType.Text
    '    '        ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + _soshipd_qty_real.ToString _
    '    '        ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
    '    '        ''                ssqls.Add(.Command.CommandText)
    '    '        ''                .Command.ExecuteNonQuery()
    '    '        ''                .Command.Parameters.Clear()
    '    '        ''                Exit For 'karena nilai _shosipd_qty_real sudah habis...
    '    '        ''            End If
    '    '        ''        Next
    '    '        ''    End If
    '    '        ''Next
    '    '        ' ''**********************************************************
    '    '        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

    '    '        If _create_jurnal = True Then
    '    '            If glt_det_sq_shipment(ssqls, par_obj, ds_edit, _soship_oid.ToString, _soship_code) = False Then
    '    '                Return False
    '    '            End If

    '    '            If jurnal_payment(par_obj, par_sq_oid, par_sq_code) = False Then
    '    '                Return False
    '    '            End If
    '    '        End If
    '    '    End With
    'End Function

    'Private Function glt_det_sq_shipment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, ByVal par_soship_oid As String, ByVal par_soship_code As String) As Boolean
    '    'glt_det_sq_shipment = True
    '    'Dim i, _pl_id As Integer
    '    'Dim _glt_code As String
    '    'Dim dt_bantu As DataTable
    '    'Dim _date As Date = sq_date.DateTime
    '    'Dim _cost As Double
    '    '_glt_code = func_coll.get_transaction_number("IC", sq_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")
    '    'Dim _seq As Integer = -1

    '    'For i = 0 To par_ds.Tables(0).Rows.Count - 1
    '    '    _seq = _seq + 1

    '    '    With par_obj
    '    '        Try
    '    '            If par_ds.Tables(0).Rows(i).Item("sqd_qty") > 0 Then
    '    '                _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("sqd_pt_id"))
    '    '                '_cost = par_ds.Tables(0).Rows(i).Item("sqd_qty") * (par_ds.Tables(0).Rows(i).Item("sqd_cost") - (par_ds.Tables(0).Rows(i).Item("sqd_cost") * par_ds.Tables(0).Rows(i).Item("sqd_disc")))
    '    '                _cost = par_ds.Tables(0).Rows(i).Item("sqd_qty") * (par_ds.Tables(0).Rows(i).Item("sqd_cost"))

    '    '                dt_bantu = New DataTable
    '    '                dt_bantu = (func_coll.get_prodline_account(_pl_id, "SL_CMACC"))

    '    '                'Insert Untuk Yang Debet

    '    '                .Command.CommandType = CommandType.Text
    '    '                .Command.CommandText = "INSERT INTO  " _
    '    '                                    & "  public.glt_det " _
    '    '                                    & "( " _
    '    '                                    & "  glt_oid, " _
    '    '                                    & "  glt_dom_id, " _
    '    '                                    & "  glt_en_id, " _
    '    '                                    & "  glt_add_by, " _
    '    '                                    & "  glt_add_date, " _
    '    '                                    & "  glt_code, " _
    '    '                                    & "  glt_date, " _
    '    '                                    & "  glt_type, " _
    '    '                                    & "  glt_cu_id, " _
    '    '                                    & "  glt_exc_rate, " _
    '    '                                    & "  glt_seq, " _
    '    '                                    & "  glt_ac_id, " _
    '    '                                    & "  glt_sb_id, " _
    '    '                                    & "  glt_cc_id, " _
    '    '                                    & "  glt_desc, " _
    '    '                                    & "  glt_debit, " _
    '    '                                    & "  glt_credit, " _
    '    '                                    & "  glt_ref_oid, " _
    '    '                                    & "  glt_ref_trans_code, " _
    '    '                                    & "  glt_posted, " _
    '    '                                    & "  glt_dt, " _
    '    '                                    & "  glt_daybook " _
    '    '                                    & ")  " _
    '    '                                    & "VALUES ( " _
    '    '                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '    '                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '    '                                    & SetInteger(par_ds.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
    '    '                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '    '                                    & SetSetring(_glt_code) & ",  " _
    '    '                                    & SetDate(_date) & ",  " _
    '    '                                    & SetSetring("IC") & ",  " _
    '    '                                    & SetInteger(sq_cu_id.EditValue) & ",  " _
    '    '                                    & SetDbl(sq_exc_rate.EditValue) & ",  " _
    '    '                                    & SetInteger(_seq) & ",  " _
    '    '                                    & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
    '    '                                    & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
    '    '                                    & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
    '    '                                    & SetSetringDB("SO Shipment") & ",  " _
    '    '                                    & SetDblDB(_cost) & ",  " _
    '    '                                    & SetDblDB(0) & ",  " _
    '    '                                    & SetSetring(par_soship_oid) & ",  " _
    '    '                                    & SetSetring(par_soship_code) & ",  " _
    '    '                                    & SetSetring("N") & ",  " _
    '    '                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '    '                                    & SetSetring("IC-SOS") & "  " _
    '    '                                    & ")"
    '    '                par_ssqls.Add(.Command.CommandText)
    '    '                .Command.ExecuteNonQuery()
    '    '                .Command.Parameters.Clear()

    '    '                If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
    '    '                                                 dt_bantu.Rows(0).Item("pla_ac_id"), _
    '    '                                                 dt_bantu.Rows(0).Item("pla_sb_id"), _
    '    '                                                 dt_bantu.Rows(0).Item("pla_cc_id"), _
    '    '                                                 par_ds.Tables(0).Rows(i).Item("sqd_en_id"), sq_cu_id.EditValue, _
    '    '                                                 sq_exc_rate.EditValue, _cost, "D") = False Then

    '    '                    Return False
    '    '                    Exit Function
    '    '                End If
    '    '            End If

    '    '            _seq = _seq + 1

    '    '            dt_bantu = New DataTable
    '    '            dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))

    '    '            .Command.CommandType = CommandType.Text
    '    '            .Command.CommandText = "INSERT INTO  " _
    '    '                                & "  public.glt_det " _
    '    '                                & "( " _
    '    '                                & "  glt_oid, " _
    '    '                                & "  glt_dom_id, " _
    '    '                                & "  glt_en_id, " _
    '    '                                & "  glt_add_by, " _
    '    '                                & "  glt_add_date, " _
    '    '                                & "  glt_code, " _
    '    '                                & "  glt_date, " _
    '    '                                & "  glt_type, " _
    '    '                                & "  glt_cu_id, " _
    '    '                                & "  glt_exc_rate, " _
    '    '                                & "  glt_seq, " _
    '    '                                & "  glt_ac_id, " _
    '    '                                & "  glt_sb_id, " _
    '    '                                & "  glt_cc_id, " _
    '    '                                & "  glt_desc, " _
    '    '                                & "  glt_debit, " _
    '    '                                & "  glt_credit, " _
    '    '                                & "  glt_ref_oid, " _
    '    '                                & "  glt_ref_trans_code, " _
    '    '                                & "  glt_posted, " _
    '    '                                & "  glt_dt, " _
    '    '                                & "  glt_daybook " _
    '    '                                & ")  " _
    '    '                                & "VALUES ( " _
    '    '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '    '                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '    '                                & SetInteger(par_ds.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
    '    '                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '    '                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '    '                                & SetSetring(_glt_code) & ",  " _
    '    '                                & SetDate(_date) & ",  " _
    '    '                                & SetSetring("IC") & ",  " _
    '    '                                & SetInteger(sq_cu_id.EditValue) & ",  " _
    '    '                                & SetDbl(sq_exc_rate.EditValue) & ",  " _
    '    '                                & SetInteger(_seq) & ",  " _
    '    '                                & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
    '    '                                & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
    '    '                                & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
    '    '                                & SetSetring("SO Shipment") & ",  " _
    '    '                                & SetDblDB(0) & ",  " _
    '    '                                & SetDblDB(_cost) & ",  " _
    '    '                                & SetSetring(par_soship_oid) & ",  " _
    '    '                                & SetSetring(par_soship_code) & ",  " _
    '    '                                & SetSetring("N") & ",  " _
    '    '                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '    '                                & SetSetring("IC-SOS") & "  " _
    '    '                                & ")"
    '    '            par_ssqls.Add(.Command.CommandText)
    '    '            .Command.ExecuteNonQuery()
    '    '            .Command.Parameters.Clear()

    '    '            If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
    '    '                                             dt_bantu.Rows(0).Item("pla_ac_id"), _
    '    '                                             dt_bantu.Rows(0).Item("pla_sb_id"), _
    '    '                                             dt_bantu.Rows(0).Item("pla_cc_id"), _
    '    '                                             par_ds.Tables(0).Rows(i).Item("sqd_en_id"), sq_cu_id.EditValue, _
    '    '                                             sq_exc_rate.EditValue, _cost, "C") = False Then

    '    '                Return False
    '    '                Exit Function
    '    '            End If
    '    '            '********************** finish untuk yang credit
    '    '        Catch ex As Exception
    '    '            MessageBox.Show(ex.Message)
    '    '            Return False
    '    '        End Try
    '    '    End With
    '    'Next
    'End Function

    'Private Function jurnal_payment(ByVal par_obj As Object, ByVal par_sq_oid As String, ByVal par_sq_code As String) As Boolean
    '    'jurnal_payment = True
    '    ''buat struktur dulu datasetnya...dengan data yang kosong
    '    'ds_edit_shipment = New DataSet
    '    'Try
    '    '    Using objcb As New master_new.WDABasepgsql("", "")
    '    '        With objcb
    '    '            .SQL = "SELECT  " _
    '    '                & "  ars_oid, True as ceklist, " _
    '    '                & "  ars_ar_oid, " _
    '    '                & "  ars_seq, " _
    '    '                & "  ars_soshipd_oid, " _
    '    '                & "  soship_code, " _
    '    '                & "  pt_id, " _
    '    '                & "  pt_code, " _
    '    '                & "  pt_desc1, " _
    '    '                & "  pt_desc2, " _
    '    '                & "  ars_taxable, " _
    '    '                & "  ars_tax_class_id, " _
    '    '                & "  code_name as taxclass_name, " _
    '    '                & "  ars_tax_inc, " _
    '    '                & "  ars_open, " _
    '    '                & "  ars_invoice, " _
    '    '                & "  ars_sq_price, " _
    '    '                & "  ars_sq_disc_value, " _
    '    '                & "  ars_invoice_price, " _
    '    '                & "  ars_close_line, " _
    '    '                & "  ars_dt " _
    '    '                & "FROM  " _
    '    '                & "  public.ars_ship " _
    '    '                & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
    '    '                & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
    '    '                & "  inner join public.sqd_det on public.soshipd_det.soshipd_sqd_oid = public.sqd_det.sqd_oid " _
    '    '                & "  inner join public.pt_mstr on public.sqd_det.sqd_pt_id = public.pt_mstr.pt_id " _
    '    '                & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
    '    '                & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
    '    '                & " where ars_seq = -99"
    '    '            .InitializeCommand()
    '    '            .FillDataSet(ds_edit_shipment, "shipment")
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    '    'ds_edit_dist = New DataSet
    '    'Try
    '    '    Using objcb As New master_new.WDABasepgsql("", "")
    '    '        With objcb
    '    '            .SQL = "SELECT  " _
    '    '                & "  public.ard_dist.ard_oid, " _
    '    '                & "  public.ard_dist.ard_ar_oid, " _
    '    '                & "  public.ard_dist.ard_tax_distribution, " _
    '    '                & "  public.ard_dist.ard_taxable, " _
    '    '                & "  public.ard_dist.ard_tax_inc, " _
    '    '                & "  public.ard_dist.ard_tax_class_id, " _
    '    '                & "  public.ard_dist.ard_ac_id, " _
    '    '                & "  public.ard_dist.ard_sb_id, " _
    '    '                & "  public.ard_dist.ard_cc_id, " _
    '    '                & "  public.ard_dist.ard_amount, " _
    '    '                & "  public.ard_dist.ard_remarks, " _
    '    '                & "  public.ard_dist.ard_dt, " _
    '    '                & "  public.ac_mstr.ac_code, " _
    '    '                & "  public.ac_mstr.ac_name, " _
    '    '                & "  public.sb_mstr.sb_desc, " _
    '    '                & "  public.code_mstr.code_name as taxclass_name, " _
    '    '                & "  public.cc_mstr.cc_desc " _
    '    '                & "FROM " _
    '    '                & "  public.ard_dist " _
    '    '                & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
    '    '                & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
    '    '                & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
    '    '                & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
    '    '                & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
    '    '                & " where ard_amount = -99"
    '    '            .InitializeCommand()
    '    '            .FillDataSet(ds_edit_dist, "dist")
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    '    'Dim i As Integer

    '    'Dim _dtrow As DataRow
    '    'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
    '    '    _dtrow = ds_edit_shipment.Tables(0).NewRow
    '    '    _dtrow("ars_oid") = Guid.NewGuid.ToString
    '    '    _dtrow("ceklist") = True
    '    '    _dtrow("ars_soshipd_oid") = par_sq_oid 'ini hanya formalitas saja
    '    '    _dtrow("soship_code") = par_sq_code ''ini hanya formalitas saja
    '    '    _dtrow("pt_id") = ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")
    '    '    _dtrow("pt_code") = ds_edit.Tables(0).Rows(i).Item("pt_code")
    '    '    _dtrow("pt_desc1") = ds_edit.Tables(0).Rows(i).Item("pt_desc1")
    '    '    _dtrow("pt_desc2") = ds_edit.Tables(0).Rows(i).Item("pt_desc2")
    '    '    _dtrow("ars_taxable") = ds_edit.Tables(0).Rows(i).Item("sqd_taxable")
    '    '    _dtrow("ars_tax_class_id") = ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")
    '    '    _dtrow("taxclass_name") = ds_edit.Tables(0).Rows(i).Item("sqd_tax_class_name")
    '    '    _dtrow("ars_tax_inc") = ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")
    '    '    _dtrow("ars_open") = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
    '    '    _dtrow("ars_invoice") = ds_edit.Tables(0).Rows(i).Item("sqd_qty")
    '    '    _dtrow("ars_sq_price") = ds_edit.Tables(0).Rows(i).Item("sqd_price")
    '    '    _dtrow("ars_sq_disc_value") = ds_edit.Tables(0).Rows(i).Item("sqd_price") * ds_edit.Tables(0).Rows(i).Item("sqd_disc")
    '    '    '_dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sqd_price") - (ds_edit.Tables(0).Rows(i).Item("sqd_price") * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
    '    '    _dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sqd_price") '- (ds_edit.Tables(0).Rows(i).Item("sqd_price") * ds_edit.Tables(0).Rows(i).Item("sqd_disc"))
    '    '    _dtrow("ars_close_line") = "Y"
    '    '    ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
    '    'Next
    '    'ds_edit_shipment.Tables(0).AcceptChanges()

    '    ''pindah kan ke table distribution
    '    'Dim j As Integer

    '    'ds_edit_dist.Tables(0).Clear()
    '    'Dim _search As Boolean = False
    '    'Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
    '    '_invoice_price = 0
    '    '_line_tr_pph = 0
    '    '_line_tr_ppn = 0
    '    '_tax_rate = 0

    '    'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
    '    '    If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
    '    '        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
    '    '            'Mencari prodline account untuk masing2 line receipt
    '    '            dt_bantu = New DataTable
    '    '            dt_bantu = (func_data.get_prodline_account_ar(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
    '    '            _search = False
    '    '            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                'If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
    '    '                '(ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
    '    '                If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
    '    '                    _search = True
    '    '                    Exit For
    '    '                End If
    '    '            Next

    '    '            If _search = True Then
    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                    'Item Price - Tax Amount = Taxable Base                            
    '    '                    '100.00 - 9.09 = 90.91 
    '    '                    'disini hanya line ppn saja
    '    '                    _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
    '    '                    _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
    '    '                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
    '    '                    _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
    '    '                    ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
    '    '                Else
    '    '                    ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
    '    '                                                                    (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
    '    '                                                                     ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price"))
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                End If
    '    '            Else
    '    '                _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
    '    '                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

    '    '                _dtrow("ard_sb_id") = 0
    '    '                _dtrow("sb_desc") = "-"
    '    '                _dtrow("ard_cc_id") = 0
    '    '                _dtrow("cc_desc") = "-"
    '    '                _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
    '    '                _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
    '    '                _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
    '    '                _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                    'Item Price - Tax Amount = Taxable Base                            
    '    '                    '100.00 - 9.09 = 90.91 
    '    '                    'disini hanya dicari ppn nya saja
    '    '                    _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
    '    '                    _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
    '    '                    _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
    '    '                    _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
    '    '                    _dtrow("ard_amount") = _invoice_price
    '    '                Else
    '    '                    _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
    '    '                    _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
    '    '                End If


    '    '                _dtrow("ard_tax_distribution") = "Y"

    '    '                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                ds_edit_dist.Tables(0).AcceptChanges()
    '    '            End If
    '    '        End If
    '    '    End If
    '    'Next

    '    ''Untuk PPN dan PPH
    '    'Dim _ppn, _pph As Double
    '    '_ppn = 0
    '    '_pph = 0

    '    'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
    '    '    If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

    '    '        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
    '    '            'Mencari taxrate account ar untuk masing2 line receipt
    '    '            dt_bantu = New DataTable
    '    '            dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

    '    '            '1. PPN
    '    '            _search = False
    '    '            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
    '    '                    _search = True
    '    '                    Exit For
    '    '                End If
    '    '            Next
    '    '            'Exit Sub
    '    '            If _search = True Then
    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                    'Item Price - Tax Amount = Taxable Base                            
    '    '                    '100.00 - 9.09 = 90.91 
    '    '                    '_sqd_cost = ds_edit.Tables(0).Rows(i).Item("sqd_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sqd_cost") / (1 + _tax_rate)))
    '    '                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
    '    '                    _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                Else
    '    '                    _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
    '    '                End If

    '    '                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
    '    '                ds_edit_dist.Tables(0).AcceptChanges()
    '    '            Else
    '    '                _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
    '    '                _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

    '    '                _dtrow("ard_sb_id") = 0
    '    '                _dtrow("sb_desc") = "-"
    '    '                _dtrow("ard_cc_id") = 0
    '    '                _dtrow("cc_desc") = "-"
    '    '                _dtrow("ard_taxable") = "N"
    '    '                _dtrow("ard_tax_class_id") = DBNull.Value
    '    '                _dtrow("taxclass_name") = DBNull.Value
    '    '                _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
    '    '                    _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                Else
    '    '                    _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
    '    '                End If

    '    '                _dtrow("ard_amount") = _ppn
    '    '                _dtrow("ard_tax_distribution") = "Y"
    '    '                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                ds_edit_dist.Tables(0).AcceptChanges()
    '    '            End If

    '    '            '1. PPH
    '    '            _search = False
    '    '            For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
    '    '                    _search = True
    '    '                    Exit For
    '    '                End If
    '    '            Next

    '    '            If _search = True Then
    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                    '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                    'Item Price - Tax Amount = Taxable Base                            
    '    '                    '100.00 - 9.09 = 90.91 
    '    '                    '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
    '    '                    '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

    '    '                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
    '    '                    _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
    '    '                Else
    '    '                    _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
    '    '                End If

    '    '                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
    '    '                ds_edit_dist.Tables(0).AcceptChanges()
    '    '            Else
    '    '                _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
    '    '                _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

    '    '                _dtrow("ard_sb_id") = 0
    '    '                _dtrow("sb_desc") = "-"
    '    '                _dtrow("ard_cc_id") = 0
    '    '                _dtrow("cc_desc") = "-"
    '    '                _dtrow("ard_taxable") = "N"
    '    '                _dtrow("ard_tax_class_id") = DBNull.Value
    '    '                _dtrow("taxclass_name") = DBNull.Value
    '    '                _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
    '    '                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                    '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
    '    '                    '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

    '    '                    _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
    '    '                    _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                    _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
    '    '                Else
    '    '                    _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
    '    '                End If

    '    '                _dtrow("ard_amount") = _pph
    '    '                _dtrow("ard_tax_distribution") = "Y"
    '    '                ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                ds_edit_dist.Tables(0).AcceptChanges()
    '    '            End If
    '    '        End If
    '    '    End If
    '    'Next

    '    ''Ini untuk ar discount
    '    '_search = False
    '    'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
    '    '    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
    '    '        If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
    '    '            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value") > 0 Then
    '    '                'Mencari prodline account untuk masing2 line receipt
    '    '                dt_bantu = New DataTable
    '    '                dt_bantu = (func_data.get_prodline_account_ar_discount(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
    '    '                _search = False
    '    '                For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                    If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
    '    '                        _search = True
    '    '                        Exit For
    '    '                    End If
    '    '                Next

    '    '                If _search = True Then
    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                        'Item Price - Tax Amount = Taxable Base                            
    '    '                        '100.00 - 9.09 = 90.91 
    '    '                        'disini hanya line ppn saja
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
    '    '                        _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
    '    '                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
    '    '                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

    '    '                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
    '    '                                                                        (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
    '    '                                                                         _invoice_price)
    '    '                        ds_edit_dist.Tables(0).AcceptChanges()
    '    '                    End If
    '    '                Else
    '    '                    _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                    _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                    _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
    '    '                    _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
    '    '                    _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

    '    '                    _dtrow("ard_sb_id") = 0
    '    '                    _dtrow("sb_desc") = "-"
    '    '                    _dtrow("ard_cc_id") = 0
    '    '                    _dtrow("cc_desc") = "-"
    '    '                    _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
    '    '                    _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
    '    '                    _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
    '    '                    _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                        'Item Price - Tax Amount = Taxable Base                            
    '    '                        '100.00 - 9.09 = 90.91 
    '    '                        'disini hanya dicari ppn nya saja
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'agar mengurangi
    '    '                        _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
    '    '                        _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
    '    '                        _dtrow("ard_amount") = _invoice_price
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1
    '    '                        _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
    '    '                    End If


    '    '                    _dtrow("ard_tax_distribution") = "Y"

    '    '                    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                End If
    '    '            End If
    '    '        End If
    '    '    End If
    '    'Next

    '    ''Untuk PPN dan PPH yang ar discount
    '    '_search = False
    '    '_ppn = 0
    '    '_pph = 0

    '    'For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
    '    '    If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
    '    '        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
    '    '            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value") > 0 Then
    '    '                'Mencari taxrate account ap untuk masing2 line receipt
    '    '                dt_bantu = New DataTable
    '    '                dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

    '    '                '1. PPN
    '    '                _search = False
    '    '                For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                    If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
    '    '                        _search = True
    '    '                        Exit For
    '    '                    End If
    '    '                Next

    '    '                If _search = True Then
    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                        'Item Price - Tax Amount = Taxable Base                            
    '    '                        '100.00 - 9.09 = 90.91 
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
    '    '                        _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
    '    '                        _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
    '    '                        _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
    '    '                    End If

    '    '                    ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                Else
    '    '                    _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                    _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                    _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
    '    '                    _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
    '    '                    _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

    '    '                    _dtrow("ard_sb_id") = 0
    '    '                    _dtrow("sb_desc") = "-"
    '    '                    _dtrow("ard_cc_id") = 0
    '    '                    _dtrow("cc_desc") = "-"
    '    '                    _dtrow("ard_taxable") = "N"
    '    '                    _dtrow("ard_tax_class_id") = DBNull.Value
    '    '                    _dtrow("taxclass_name") = DBNull.Value
    '    '                    _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
    '    '                        _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
    '    '                        _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
    '    '                        _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
    '    '                    End If

    '    '                    _dtrow("ard_amount") = _ppn
    '    '                    _dtrow("ard_tax_distribution") = "Y"
    '    '                    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                End If

    '    '                '1. PPH
    '    '                _search = False
    '    '                For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '                    If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
    '    '                        _search = True
    '    '                        Exit For
    '    '                    End If
    '    '                Next

    '    '                If _search = True Then
    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
    '    '                        '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
    '    '                        'Item Price - Tax Amount = Taxable Base                            
    '    '                        '100.00 - 9.09 = 90.91 
    '    '                        '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
    '    '                        '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
    '    '                        _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
    '    '                        _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                        _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
    '    '                        _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
    '    '                    End If

    '    '                    ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                Else
    '    '                    _dtrow = ds_edit_dist.Tables(0).NewRow
    '    '                    _dtrow("ard_oid") = Guid.NewGuid.ToString

    '    '                    _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
    '    '                    _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
    '    '                    _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

    '    '                    _dtrow("ard_sb_id") = 0
    '    '                    _dtrow("sb_desc") = "-"
    '    '                    _dtrow("ard_cc_id") = 0
    '    '                    _dtrow("cc_desc") = "-"
    '    '                    _dtrow("ard_taxable") = "N"
    '    '                    _dtrow("ard_tax_class_id") = DBNull.Value
    '    '                    _dtrow("taxclass_name") = DBNull.Value
    '    '                    _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
    '    '                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
    '    '                        '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
    '    '                        '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

    '    '                        _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
    '    '                        _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
    '    '                        _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
    '    '                    Else
    '    '                        _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_sq_disc_value")
    '    '                        _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
    '    '                        _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
    '    '                    End If

    '    '                    _dtrow("ard_amount") = _pph
    '    '                    _dtrow("ard_tax_distribution") = "Y"
    '    '                    ds_edit_dist.Tables(0).Rows.Add(_dtrow)
    '    '                    ds_edit_dist.Tables(0).AcceptChanges()
    '    '                End If
    '    '            End If
    '    '        End If
    '    '    End If
    '    'Next
    '    ''**************************************************

    '    'For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
    '    '    If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
    '    '        ds_edit_dist.Tables(0).Rows(i).Delete()
    '    '    End If
    '    'Next
    '    'ds_edit_dist.Tables(0).AcceptChanges()

    '    'Dim _bk_ac_id As Integer
    '    'Dim _bk_ac_name As String = ""
    '    'Try
    '    '    Using objcb As New master_new.WDABasepgsql("", "")
    '    '        With objcb
    '    '            .Connection.Open()
    '    '            .Command = .Connection.CreateCommand
    '    '            .Command.CommandType = CommandType.Text
    '    '            .Command.CommandText = "select bk_ac_id, ac_name from bk_mstr inner join ac_mstr on ac_id = bk_ac_id where bk_id = " + sq_bk_id.EditValue.ToString
    '    '            .InitializeCommand()
    '    '            .DataReader = .Command.ExecuteReader
    '    '            While .DataReader.Read
    '    '                _bk_ac_id = .DataReader("bk_ac_id").ToString
    '    '                _bk_ac_name = .DataReader("ac_name").ToString
    '    '            End While
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    '    Return False
    '    'End Try

    '    'Dim _ard_total_amount As Double = 0

    '    'For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
    '    '    _ard_total_amount = _ard_total_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
    '    'Next

    '    'If insert_glt_det_ar(ssqls, par_obj, ds_edit_dist, _
    '    '                                               sq_en_id.EditValue, sq_en_id.GetColumnValue("en_code"), _
    '    '                                               par_sq_oid.ToString, par_sq_code, _
    '    '                                               sq_date.DateTime, _
    '    '                                               sq_cu_id.EditValue, sq_exc_rate.EditValue, _
    '    '                                               "AY", "AR-PAY", _
    '    '                                               _bk_ac_id, 0, 0, _
    '    '                                               _bk_ac_name, _ard_total_amount) = False Then

    '    '    Return False
    '    'End If

    'End Function

    'Private Function insert_glt_det_ar(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
    '                               ByVal par_en_id As Integer, ByVal par_en_code As String, _
    '                               ByVal par_oid As String, ByVal par_trans_code As String, _
    '                               ByVal par_date As Date, ByVal par_cu_id As Integer, _
    '                               ByVal par_exc_rate As Double, _
    '                               ByVal par_type As String, ByVal par_daybook As String, _
    '                               ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
    '                               ByVal par_cc_id As Integer, _
    '                               ByVal par_desc As String, ByVal par_amount As Double) As Boolean

    '    'Return False
    '    'Exit Function
    '    insert_glt_det_ar = True
    '    Dim i As Integer
    '    Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
    '    Dim _seq As Integer = -1

    '    'Insert Untuk Yang Debet dan Credit, Looping dari dataset
    '    For i = 0 To par_ds.Tables(0).Rows.Count - 1
    '        _seq = _seq + 1

    '        If par_ds.Tables(0).Rows(i).Item("ard_amount") > 0 Then
    '            With par_obj
    '                Try
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(par_exc_rate) & ",  " _
    '                                        & SetInteger(_seq) & ",  " _
    '                                        & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount")) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                         par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
    '                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
    '                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
    '                                                         par_en_id, par_cu_id, _
    '                                                         par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount"), "C") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                    Return False
    '                End Try
    '            End With
    '        ElseIf par_ds.Tables(0).Rows(i).Item("ard_amount") < 0 Then
    '            With par_obj
    '                Try
    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.glt_det " _
    '                                        & "( " _
    '                                        & "  glt_oid, " _
    '                                        & "  glt_dom_id, " _
    '                                        & "  glt_en_id, " _
    '                                        & "  glt_add_by, " _
    '                                        & "  glt_add_date, " _
    '                                        & "  glt_code, " _
    '                                        & "  glt_date, " _
    '                                        & "  glt_type, " _
    '                                        & "  glt_cu_id, " _
    '                                        & "  glt_exc_rate, " _
    '                                        & "  glt_seq, " _
    '                                        & "  glt_ac_id, " _
    '                                        & "  glt_sb_id, " _
    '                                        & "  glt_cc_id, " _
    '                                        & "  glt_desc, " _
    '                                        & "  glt_debit, " _
    '                                        & "  glt_credit, " _
    '                                        & "  glt_ref_oid, " _
    '                                        & "  glt_ref_trans_code, " _
    '                                        & "  glt_posted, " _
    '                                        & "  glt_dt, " _
    '                                        & "  glt_daybook " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetInteger(par_en_id) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(_glt_code) & ",  " _
    '                                        & SetDate(par_date) & ",  " _
    '                                        & SetSetring(par_type) & ",  " _
    '                                        & SetInteger(par_cu_id) & ",  " _
    '                                        & SetDbl(par_exc_rate) & ",  " _
    '                                        & SetInteger(_seq) & ",  " _
    '                                        & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetIntegerDB(0) & ",  " _
    '                                        & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
    '                                        & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount") * -1) & ",  " _
    '                                        & SetDblDB(0) & ",  " _
    '                                        & SetSetring(par_oid) & ",  " _
    '                                        & SetSetring(par_trans_code) & ",  " _
    '                                        & SetSetring("N") & ",  " _
    '                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                        & SetSetring(par_daybook) & "  " _
    '                                        & ")"
    '                    par_ssqls.Add(.Command.CommandText)
    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                                         par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
    '                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
    '                                                         SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
    '                                                         par_en_id, par_cu_id, _
    '                                                         par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount") * -1, "D") = False Then

    '                        Return False
    '                        Exit Function
    '                    End If
    '                Catch ex As Exception
    '                    MessageBox.Show(ex.Message)
    '                    Return False
    '                End Try
    '            End With
    '        End If
    '    Next

    '    _seq = _seq + 1

    '    With par_obj
    '        Try
    '            'Insert untuk yang credit yang account hutang nya
    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "INSERT INTO  " _
    '                                & "  public.glt_det " _
    '                                & "( " _
    '                                & "  glt_oid, " _
    '                                & "  glt_dom_id, " _
    '                                & "  glt_en_id, " _
    '                                & "  glt_add_by, " _
    '                                & "  glt_add_date, " _
    '                                & "  glt_code, " _
    '                                & "  glt_date, " _
    '                                & "  glt_type, " _
    '                                & "  glt_cu_id, " _
    '                                & "  glt_exc_rate, " _
    '                                & "  glt_seq, " _
    '                                & "  glt_ac_id, " _
    '                                & "  glt_sb_id, " _
    '                                & "  glt_cc_id, " _
    '                                & "  glt_desc, " _
    '                                & "  glt_debit, " _
    '                                & "  glt_credit, " _
    '                                & "  glt_ref_oid, " _
    '                                & "  glt_ref_trans_code, " _
    '                                & "  glt_posted, " _
    '                                & "  glt_dt, " _
    '                                & "  glt_daybook " _
    '                                & ")  " _
    '                                & "VALUES ( " _
    '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
    '                                & SetInteger(par_en_id) & ",  " _
    '                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                & SetSetring(_glt_code) & ",  " _
    '                                & SetDate(par_date) & ",  " _
    '                                & SetSetring(par_type) & ",  " _
    '                                & SetInteger(par_cu_id) & ",  " _
    '                                & SetDbl(par_exc_rate) & ",  " _
    '                                & SetInteger(_seq) & ",  " _
    '                                & SetInteger(par_ac_id) & ",  " _
    '                                & SetIntegerDB(par_sb_id) & ",  " _
    '                                & SetIntegerDB(par_cc_id) & ",  " _
    '                                & SetSetringDB(par_desc) & ",  " _
    '                                & SetDblDB(par_amount) & ",  " _
    '                                & SetDblDB(0) & ",  " _
    '                                & SetSetring(par_oid) & ",  " _
    '                                & SetSetring(par_trans_code) & ",  " _
    '                                & SetSetring("N") & ",  " _
    '                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
    '                                & SetSetring(par_daybook) & "  " _
    '                                & ")"
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()

    '            If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
    '                                             par_ac_id, _
    '                                             par_sb_id, _
    '                                             par_cc_id, _
    '                                             par_en_id, par_cu_id, _
    '                                             par_exc_rate, par_amount, "D") = False Then

    '                Return False
    '                Exit Function
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '            Return False
    '        End Try
    '    End With
    'End Function

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
            If Not IsDBNull(ds.Tables("detail").Rows(i).Item("sqd_qty_shipment")) = True Then
                If ds.Tables("detail").Rows(i).Item("sqd_qty_shipment") > 0 Then
                    MessageBox.Show("Data already transfer..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next

        ' gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sq_oid_mstr = .Item("sq_oid").ToString
                sq_ar_sb_id.EditValue = .Item("sq_ar_sb_id")
                sq_ar_ac_id.EditValue = .Item("sq_ar_ac_id")
                sq_ar_cc_id.EditValue = .Item("sq_ar_cc_id")
                'sq_credit_term.EditValue = .Item("sq_credit_term")
                sq_cu_id.EditValue = .Item("sq_cu_id")

                'If IsDBNull(.Item("sq_bk_id")) = True Then
                '    sq_bk_id.ItemIndex = 0
                'Else
                '    sq_bk_id.EditValue = .Item("sq_bk_id")
                'End If

                sq_en_id.EditValue = .Item("sq_en_id")
                sq_date.DateTime = .Item("sq_date")
                sq_exc_rate.EditValue = .Item("sq_exc_rate")
                sq_pay_method.EditValue = .Item("sq_pay_method")
                sq_cons.EditValue = SetBitYNB(.Item("sq_cons"))
                sq_pay_type.EditValue = .Item("sq_pay_type")
                sq_need_date.DateTime = .Item("sq_need_date")
                sq_pi_id.EditValue = .Item("sq_pi_id")
                _sq_ptnr_id_sold_mstr = SetInteger(.Item("sq_ptnr_id_sold"))
                sq_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
                sq_ref_po_code.Text = SetString(.Item("sq_ref_po_code"))
                _sq_ref_po_oid = SetString(.Item("sq_ref_po_oid"))

                sq_sales_program.EditValue = .Item("sq_sales_program")
                sq_status_produk.EditValue = .Item("sq_status_produk")
                sq_diskon_produk.EditValue = .Item("sq_diskon_produk")

                If _sq_ref_po_oid <> "" Then
                    sq_ref_po_code.Enabled = False
                Else
                    sq_ref_po_code.Enabled = True
                End If
                sq_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
                sq_sales_person.EditValue = .Item("sq_sales_person")
                sq_si_id.EditValue = .Item("sq_si_id")
                'sq_tax_inc.EditValue = SetBitYNB(.Item("sq_tax_inc"))

                'If IsDBNull(.Item("sq_ppn_type")) = True Then
                '    sq_ppn_type.Text = ""
                'ElseIf .Item("sq_ppn_type") = "PPN Bebas" Then
                '    sq_ppn_type.EditValue = "E"
                'ElseIf .Item("sq_ppn_type") = "PPN Bayar" Then
                '    sq_ppn_type.EditValue = "A"
                'End If
                'sq_taxable.EditValue = SetBitYNB(.Item("sq_taxable"))
                sq_tran_id.EditValue = .Item("sq_tran_id")
                sq_trans_rmks.Text = SetString(.Item("sq_trans_rmks"))
                sq_type.EditValue = .Item("sq_type")

            End With
            sq_en_id.Focus()
            sq_cu_id.Enabled = False
            sq_ptnr_id_sold.Enabled = False
            sq_bantu_address.Enabled = False
            sq_pay_type.Enabled = False
            sq_type.Enabled = False
            'sq_pi_id.Enabled = False

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
                            & "  sqd_qty_allocated, " _
                            & "  sqd_qty_picked, " _
                            & "  sqd_qty_shipment, " _
                            & "  sqd_qty_pending_inv, " _
                            & "  sqd_qty_invoice, " _
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
                            & "  loc_desc, sqd_pod_oid,0 as status_shipment " _
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

        If ds.Tables("detail_attr").Rows.Count = 0 Then
            _sqa_oid_mstr = ""
        Else
            row = BindingContext(ds.Tables("detail_attr")).Position

            With ds.Tables("detail_attr").Rows(row)
                _sqa_oid_mstr = .Item("sqa_oid").ToString
                sqa_kjb_code.Text = SetString(.Item("sqa_kjb_code"))
                sqa_anak_nama_1.Text = SetString(.Item("sqa_anak_nama_1"))
                sqa_anak_nama_2.Text = SetString(.Item("sqa_anak_nama_2"))
                sqa_anak_nama_3.Text = SetString(.Item("sqa_anak_nama_3"))
                sqa_anak_sekolah_1.Text = SetString(.Item("sqa_anak_sekolah_1"))
                sqa_anak_sekolah_2.Text = SetString(.Item("sqa_anak_sekolah_2"))
                sqa_anak_sekolah_3.Text = SetString(.Item("sqa_anak_sekolah_3"))
                sqa_anak_tgl_lahir_1.DateTime = SetString(.Item("sqa_anak_tgl_lahir_1"))
                sqa_anak_tgl_lahir_2.DateTime = SetString(.Item("sqa_anak_tgl_lahir_2"))
                sqa_anak_tgl_lahir_3.DateTime = SetString(.Item("sqa_anak_tgl_lahir_3"))
                sqa_bank.Text = SetString(.Item("sqa_bank"))
                sqa_bekerja_pada.Text = SetString(.Item("sqa_bekerja_pada"))
                sqa_berlaku_sd.Text = SetString(.Item("sqa_berlaku_sd"))
                sqa_email.Text = SetString(.Item("sqa_email"))
                sqa_jabatan_bagian.Text = SetString(.Item("sqa_jabatan_bagian"))
                sqa_jenis_kartu_kredit.Text = SetString(.Item("sqa_jenis_kartu_kredit"))
                sqa_kantor_alamat_1.Text = SetString(.Item("sqa_kantor_alamat_1"))
                sqa_kantor_alamat_2.Text = SetString(.Item("sqa_kantor_alamat_2"))
                sqa_kantor_lantai.Text = SetString(.Item("sqa_kantor_lantai"))
                sqa_kantor_telp.Text = SetString(.Item("sqa_kantor_telp"))
                sqa_keluarga_dekat_alamat_1.Text = SetString(.Item("sqa_keluarga_dekat_alamat_1"))
                sqa_keluarga_dekat_alamat_2.Text = SetString(.Item("sqa_keluarga_dekat_alamat_2"))
                sqa_keluarga_dekat_hp.Text = SetString(.Item("sqa_keluarga_dekat_hp"))
                sqa_keluarga_dekat_nama.Text = SetString(.Item("sqa_keluarga_dekat_nama"))
                sqa_keluarga_dekat_telp.Text = SetString(.Item("sqa_keluarga_dekat_telp"))
                sqa_ktp.Text = SetString(.Item("sqa_ktp"))
                sqa_no_kartu_kredit.Text = SetString(.Item("sqa_no_kartu_kredit"))
                sqa_rumah_alamat_1.Text = SetString(.Item("sqa_rumah_alamat_1"))
                sqa_rumah_alamat_2.Text = SetString(.Item("sqa_rumah_alamat_2"))
                sqa_rumah_hp.Text = SetString(.Item("sqa_rumah_hp"))
                sqa_rumah_kode_pos.Text = SetString(.Item("sqa_rumah_kode_pos"))
                sqa_rumah_telp.Text = SetString(.Item("sqa_rumah_telp"))
                sqa_status_alamat_kirim.EditValue = SetBitYNB(.Item("sqa_status_alamat_kirim"))
                sqa_status_alamat_tagih.EditValue = SetBitYNB(.Item("sqa_status_alamat_tagih"))
                sqa_status_tempat_tinggal.EditValue = SetBitYNB(.Item("sqa_status_tempat_tinggal"))
                sqa_suami_bekerja.Text = SetString(.Item("sqa_suami_bekerja"))
                sqa_suami_hp.Text = SetString(.Item("sqa_suami_hp"))
                sqa_suami_jabatan.Text = SetString(.Item("sqa_suami_jabatan"))
                sqa_suami_kantor_alamat_1.Text = SetString(.Item("sqa_suami_kantor_alamat_1"))
                sqa_suami_kantor_alamat_2.Text = SetString(.Item("sqa_suami_kantor_alamat_2"))
                sqa_suami_nama.Text = SetString(.Item("sqa_suami_nama"))
                sqa_suami_telp.Text = SetString(.Item("sqa_suami_telp"))
                sqa_status_rumah_id.EditValue = SetInteger(.Item("sqa_status_rumah_id"))
                sqa_lama_tinggal_id.EditValue = SetInteger(.Item("sqa_lama_tinggal_id"))
                sqa_masa_kerja_id.EditValue = SetInteger(.Item("sqa_masa_kerja_id"))
                sqa_income_id.EditValue = SetInteger(.Item("sqa_income_id"))
                'sqa_kepribadian_id.EditValue = SetInteger(.Item("sqa_kepribadian_id"))
                sqa_tanggungan_id.EditValue = SetInteger(.Item("sqa_tanggungan_id"))
                'sqa_jaminan_id.EditValue = SetInteger(.Item("sqa_status_rumah_id"))

                sqa_status_rumah_id.Text = SetString(.Item("status_kepemilikan"))
                sqa_lama_tinggal_id.Text = SetString(.Item("lama_tinggal"))
                sqa_masa_kerja_id.Text = SetString(.Item("masa_kerja"))
                sqa_income_id.Text = SetString(.Item("income"))
                'sqa_kepribadian_id.Text = SetString(.Item("kepribadian"))
                sqa_tanggungan_id.Text = SetString(.Item("tanggungan"))
                'sqa_jaminan_id.Text = SetString(.Item("jaminan"))

            End With
        End If
        sqa_status_rumah_id.ClosePopup()
        sqa_lama_tinggal_id.ClosePopup()
        sqa_masa_kerja_id.ClosePopup()
        sqa_income_id.ClosePopup()
        'sqa_kepribadian_id.ClosePopup()
        sqa_tanggungan_id.ClosePopup()
        'sqa_jaminan_id.ClosePopup()
        'cek_shipment_row()
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
        ds_bantu = func_data.load_aprv_mstr(sq_tran_id.EditValue)

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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.sq_mstr   " _
                                            & "SET  " _
                                            & "  sq_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  sq_en_id = " & SetInteger(sq_en_id.EditValue) & ",  " _
                                            & "  sq_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sq_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_date = " & SetDate(sq_date.DateTime) & ",  " _
                                            & "  sq_ptnr_id_sold = " & SetInteger(_sq_ptnr_id_sold_mstr) & ",  " _
                                            & "  sq_ref_po_code = " & SetSetring(sq_ref_po_code.Text) & ",  " _
                                             & "  sq_sales_program = " & SetSetring(sq_sales_program.EditValue) & ",  " _
                                              & "  sq_status_produk = " & SetSetring(sq_status_produk.Text) & ",  " _
                                               & "  sq_diskon_produk = " & SetSetring(sq_diskon_produk.Text) & ",  " _
                                            & "  sq_ref_po_oid = " & SetSetring(_sq_ref_po_oid) & ",  " _
                                            & "  sq_si_id = " & SetInteger(sq_si_id.EditValue) & ",  " _
                                            & "  sq_sales_person = " & SetInteger(sq_sales_person.EditValue) & ",  " _
                                            & "  sq_pay_method = " & SetInteger(sq_pay_method.EditValue) & ",  " _
                                            & "  sq_cons = " & SetSetring("N") & ",  " _
                                            & "  sq_ar_ac_id = " & SetInteger(sq_ar_ac_id.EditValue) & ",  " _
                                            & "  sq_ar_sb_id = " & SetInteger(sq_ar_sb_id.EditValue) & ",  " _
                                            & "  sq_ar_cc_id = " & SetInteger(sq_ar_cc_id.EditValue) & ",  " _
                                            & "  sq_dp = " & SetDbl(_total_dp) & ",  " _
                                            & "  sq_total = " & SetDbl(_sq_total) & ",  " _
                                            & "  sq_need_date = " & SetDate(sq_need_date.DateTime) & ",  " _
                                            & "  sq_tran_id = " & SetInteger(sq_tran_id.EditValue) & ",  " _
                                            & "  sq_trans_id = " & SetSetring(_sq_trn_status) & ",  " _
                                            & "  sq_trans_rmks = " & SetSetring(sq_trans_rmks.Text) & ",  " _
                                            & "  sq_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sq_total_ppn = " & SetDbl(_sq_total_ppn) & ",  " _
                                            & "  sq_total_pph = " & SetDbl(_sq_total_pph) & ",  " _
                                            & "  sq_payment = " & SetDbl(_total_payment) & ",  " _
                                            & "  sq_exc_rate = " & SetDbl(sq_exc_rate.EditValue) & ",  " _
                                            & "  sq_terbilang = " & SetSetring(_sq_terbilang) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sq_oid = " & SetSetring(_sq_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        ' ''Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                        ''.Command.CommandType = CommandType.Text
                        ''.Command.CommandText = "update pod_det set pod_qty_sq = 0 where pod_oid in (select sqd_pod_oid from sqd_det where sqd_sq_oid = " + SetSetring(_sq_oid_mstr) + ")"
                        ''ssqls.Add(.Command.CommandText)
                        ''.Command.ExecuteNonQuery()
                        ''.Command.Parameters.Clear()
                        '******************************************************

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table transfer
                        'kalau sudah relasi ke table transfer jadi nya error...dan harusnya tidak bisa didelete
                        ' ''.Command.CommandType = CommandType.Text
                        ' ''.Command.CommandText = "delete from sqd_det where coalesce((select count(*) as jml from ptsfrd_det where ptsfrd_sqd_oid=sqd_oid),0) = 0 and sqd_sq_oid = '" + _sq_oid_mstr + "'"
                        ' ''ssqls.Add(.Command.CommandText)
                        ' ''.Command.ExecuteNonQuery()
                        ' ''.Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _sqd_qty_shipment = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment")) = True, 0, ds_edit.Tables(0).Rows(i).Item("sqd_qty_shipment"))
                            'If SetNumber(ds_edit.Tables(0).Rows(i).Item("status_shipment")) = 0 Then
                            '    .Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "INSERT INTO  " _
                            '                    & "  public.sqd_det " _
                            '                    & "( " _
                            '                    & "  sqd_oid, " _
                            '                    & "  sqd_dom_id, " _
                            '                    & "  sqd_en_id, " _
                            '                    & "  sqd_add_by, " _
                            '                    & "  sqd_add_date, " _
                            '                    & "  sqd_sq_oid, " _
                            '                    & "  sqd_seq, " _
                            '                    & "  sqd_is_additional_charge, " _
                            '                    & "  sqd_si_id, " _
                            '                    & "  sqd_pt_id, " _
                            '                    & "  sqd_rmks, " _
                            '                    & "  sqd_qty, " _
                            '                    & "  sqd_um, " _
                            '                    & "  sqd_cost, " _
                            '                    & "  sqd_price, " _
                            '                    & "  sqd_disc, " _
                            '                    & "  sqd_sales_ac_id, " _
                            '                    & "  sqd_sales_sb_id, " _
                            '                    & "  sqd_sales_cc_id, " _
                            '                    & "  sqd_um_conv, " _
                            '                    & "  sqd_qty_real, " _
                            '                    & "  sqd_taxable, " _
                            '                    & "  sqd_tax_inc, " _
                            '                    & "  sqd_tax_class, " _
                            '                    & "  sqd_ppn_type, " _
                            '                    & "  sqd_dt, " _
                            '                    & "  sqd_payment, " _
                            '                    & "  sqd_dp, " _
                            '                    & "  sqd_loc_id, " _
                            '                    & "  sqd_sales_unit " _
                            '                    & ")  " _
                            '                    & "VALUES ( " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_oid")) & ",  " _
                            '                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                            '                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            '                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                            '                    & SetSetring(_sq_oid_mstr.ToString) & ",  " _
                            '                    & SetInteger(i) & ",  " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_si_id")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_pt_id")) & ",  " _
                            '                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sqd_rmks")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_cost")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_price")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_disc")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_ac_id")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_sb_id")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_sales_cc_id")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_um_conv")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty_real")) & ",  " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_taxable")) & ",  " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_tax_inc")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_tax_class")) & ",  " _
                            '                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_ppn_type")) & ",  " _
                            '                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_payment")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_dp")) & ",  " _
                            '                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_loc_id")) & ",  " _
                            '                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_sales_unit")) & "  " _
                            '                    & ")"
                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    .Command.Parameters.Clear()
                            'Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.sqd_det   " _
                                                & "SET  " _
                                                & "  sqd_en_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sqd_en_id")) & ",  " _
                                                & "  sqd_is_additional_charge = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_is_additional_charge")) & ",  " _
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
                            'End If
                        Next

                        If _sqa_oid_mstr <> "" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.sqa_attr   " _
                                                & "SET  " _
                                                & "  sqa_kjb_code = " & SetSetring(sqa_kjb_code.Text) & ",  " _
                                                & "  sqa_bekerja_pada = " & SetSetring(sqa_bekerja_pada.Text) & ",  " _
                                                & "  sqa_jabatan_bagian = " & SetSetring(sqa_jabatan_bagian.Text) & ",  " _
                                                & "  sqa_kantor_alamat_1 = " & SetSetring(sqa_kantor_alamat_1.Text) & ",  " _
                                                & "  sqa_kantor_alamat_2 = " & SetSetring(sqa_kantor_alamat_2.Text) & ",  " _
                                                & "  sqa_kantor_lantai = " & SetSetring(sqa_kantor_lantai.Text) & ",  " _
                                                & "  sqa_kantor_telp = " & SetSetring(sqa_kantor_telp.Text) & ",  " _
                                                & "  sqa_ktp = " & SetSetring(sqa_ktp.Text) & ",  " _
                                                & "  sqa_email = " & SetSetring(sqa_email.Text) & ",  " _
                                                & "  sqa_rumah_alamat_1 = " & SetSetring(sqa_rumah_alamat_1.Text) & ",  " _
                                                & "  sqa_rumah_alamat_2 = " & SetSetring(sqa_rumah_alamat_2.Text) & ",  " _
                                                & "  sqa_rumah_kode_pos = " & SetSetring(sqa_rumah_kode_pos.Text) & ",  " _
                                                & "  sqa_rumah_telp = " & SetSetring(sqa_rumah_telp.Text) & ",  " _
                                                & "  sqa_rumah_hp = " & SetSetring(sqa_rumah_hp.Text) & ",  " _
                                                & "  sqa_status_alamat_kirim = " & SetBitYN(sqa_status_alamat_kirim.EditValue) & ",  " _
                                                & "  sqa_status_alamat_tagih = " & SetBitYN(sqa_status_alamat_tagih.EditValue) & ",  " _
                                                & "  sqa_suami_nama = " & SetSetring(sqa_suami_nama.Text) & ",  " _
                                                & "  sqa_suami_bekerja = " & SetSetring(sqa_suami_bekerja.Text) & ",  " _
                                                & "  sqa_suami_jabatan = " & SetSetring(sqa_suami_jabatan.Text) & ",  " _
                                                & "  sqa_suami_kantor_alamat_1 = " & SetSetring(sqa_suami_kantor_alamat_1.Text) & ",  " _
                                                & "  sqa_suami_kantor_alamat_2 = " & SetSetring(sqa_suami_kantor_alamat_2.Text) & ",  " _
                                                & "  sqa_suami_telp = " & SetSetring(sqa_suami_telp.Text) & ",  " _
                                                & "  sqa_suami_hp = " & SetSetring(sqa_suami_hp.Text) & ",  " _
                                                & "  sqa_anak_nama_1 = " & SetSetring(sqa_anak_nama_1.Text) & ",  " _
                                                & "  sqa_anak_tgl_lahir_1 = " & SetDate(sqa_anak_tgl_lahir_1.DateTime) & ",  " _
                                                & "  sqa_anak_sekolah_1 = " & SetSetring(sqa_anak_sekolah_1.Text) & ",  " _
                                                & "  sqa_anak_nama_2 = " & SetSetring(sqa_anak_nama_2.Text) & ",  " _
                                                & "  sqa_anak_tgl_lahir_2 = " & SetDate(sqa_anak_tgl_lahir_2.DateTime) & ",  " _
                                                & "  sqa_anak_sekolah_2 = " & SetSetring(sqa_anak_sekolah_2.Text) & ",  " _
                                                & "  sqa_anak_nama_3 = " & SetSetring(sqa_anak_nama_3.Text) & ",  " _
                                                & "  sqa_anak_tgl_lahir_3 = " & SetDate(sqa_anak_tgl_lahir_3.DateTime) & ",  " _
                                                & "  sqa_anak_sekolah_3 = " & SetSetring(sqa_anak_sekolah_3.Text) & ",  " _
                                                & "  sqa_keluarga_dekat_nama = " & SetSetring(sqa_keluarga_dekat_nama.Text) & ",  " _
                                                & "  sqa_keluarga_dekat_alamat_1 = " & SetSetring(sqa_keluarga_dekat_alamat_1.Text) & ",  " _
                                                & "  sqa_keluarga_dekat_alamat_2 = " & SetSetring(sqa_keluarga_dekat_alamat_2.Text) & ",  " _
                                                & "  sqa_keluarga_dekat_telp = " & SetSetring(sqa_keluarga_dekat_telp.Text) & ",  " _
                                                & "  sqa_keluarga_dekat_hp = " & SetSetring(sqa_keluarga_dekat_hp.Text) & ",  " _
                                                & "  sqa_status_tempat_tinggal = " & SetBitYN(sqa_status_tempat_tinggal.EditValue) & ",  " _
                                                & "  sqa_jenis_kartu_kredit = " & SetSetring(sqa_jenis_kartu_kredit.Text) & ",  " _
                                                & "  sqa_no_kartu_kredit = " & SetSetring(sqa_no_kartu_kredit.Text) & ",  " _
                                                & "  sqa_berlaku_sd = " & SetSetring(sqa_berlaku_sd.Text) & ",  " _
                                                & "  sqa_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                                & "  sqa_status_rumah_id = " & SetInteger(sqa_status_rumah_id.EditValue) & " ,  " _
                                                & "  sqa_lama_tinggal_id = " & SetInteger(sqa_lama_tinggal_id.EditValue) & " ,  " _
                                                & "  sqa_income_id = " & SetInteger(sqa_income_id.EditValue) & " ,  " _
                                                & "  sqa_masa_kerja_id = " & SetInteger(sqa_masa_kerja_id.EditValue) & " ,  " _
                                                & "  sqa_tanggungan_id = " & SetInteger(sqa_tanggungan_id.EditValue) & " ,  " _
                                                & "  sqa_bank = " & SetSetring(sqa_bank.Text) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  sqa_oid = " & SetSetring(_sqa_oid_mstr) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid").ToString <> "" Then
                                'update karena ada hubungan antara so dan po antar group perusahaan
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_so = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("sqd_qty")) _
                                                     & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sqd_pod_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If sq_type.EditValue.ToString.ToUpper = "D" Then
                            If edit_sokp_piutang(objinsert, _sq_oid_mstr.ToString, _total_payment) = False Then
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
                                                        & SetInteger(sq_en_id.EditValue) & ",  " _
                                                        & SetSetring(sq_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_sq_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")) & ",  " _
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

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, sq_en_id.EditValue, 10, _sq_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code"), sq_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
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

    Private Sub sq_ref_po_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sq_ref_po_code.ButtonClick
        If func_coll.get_conf_file("ref_po_sales_order") = "1" Then
            If _sq_ptnr_id_sold_mstr = -1 Then
                MessageBox.Show("Please Specipy Customer Data Before..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim frm As New FPurchaseOrderSearch()
                frm.set_win(Me)
                frm._obj = sq_ref_po_code
                frm._sq_ptnr_id_sold_mstr = _sq_ptnr_id_sold_mstr
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_en_id")
        _type = 10
        _table = "sq_mstr"
        _initial = "so"
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
            & "  code_mstr.code_code, " _
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
        frm._report = "XRSO"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        frm.ShowDialog()

    End Sub

    'Public Overrides Sub approve_line()
    '    If _conf_value = "0" Then
    '        Exit Sub
    '    End If

    '    Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
    '    _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
    '    _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid")
    '    _colom = "sq_trans_id"
    '    _table = "sq_mstr"
    '    _criteria = "sq_code"
    '    _initial = "so"
    '    _type = "so"
    '    _title = "Sales Order"

    '    approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    'End Sub

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
        'user_wf_email = mf.get_email_address(user_wf)

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

                        'format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        'filename = "c:\syspro\" + par_code + ".xls"
                        'ExportTo(par_gv, New ExportXlsProvider(filename))

                        'If user_wf_email <> "" Then
                        '    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        'Else
                        '    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Public Overrides Sub cancel_line()
        help_load_data(True)
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
        'Exit Sub
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

        If func_coll.get_conf_file("wf_sales_quotation") = "1" Then
            If _trans_id = "D" Then
                MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
                Exit Sub
            Else
                If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
        ElseIf func_coll.get_conf_file("wf_sales_quotation") = "0" Then
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
                                             & " and sqd_qty_shipment >= 1"

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
                MessageBox.Show("Can't Cancel For Shipment SQ...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        _type = "sQ"
        _user = func_coll.get_wf_iscurrent(_code)
        _title = "Sales Quotation"

        If _user = "" Then
            MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        reminder_by_mail(_code, _type, _user, gv_email, _title)
    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer
        Dim ssqls As New ArrayList

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("sq_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("sq_code"), 0)
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
                                .Command.CommandText = "update sq_mstr set sq_trans_id = '" + _trans_id + "'," + _
                                               " sq_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " sq_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where sq_oid = '" + ds.Tables("smart").Rows(i).Item("sq_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("sq_code") + "'" + _
                                                       " and wf_seq = 0"
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

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("sq_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("sq_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Sales Order", ds.Tables("smart").Rows(i).Item("sq_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

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
        Next

        help_load_data(True)
        MessageBox.Show("Welldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    Public Overrides Sub approve_line()

        '_conf_value = func_coll.get_conf_file("wf_sales_quotation")

        'If _conf_value = "0" Then
        '    Exit Sub
        'End If

        'Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code")
        '_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid")
        '_colom = "sq_trans_id"
        '_table = "sq_mstr"
        '_criteria = "sq_code"
        '_initial = "sq"
        '_type = "sq"
        '_title = "Sales Quotation"
        'approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)


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
                & "  sqd_qty_allocated, " _
                & "  sqd_qty_picked, " _
                & "  sqd_qty_shipment, " _
                & "  sqd_qty_pending_inv, " _
                & "  sqd_qty_invoice,sqd_qty_so, " _
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
                & "  left outer join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                & "  left outer join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                & "  left outer join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "'"

            load_data_detail(sql, gc_detail, "detail")

            Try
                ds.Tables("detail_attr").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  sqa_oid, " _
                & "  sqa_kjb_code, " _
                & "  sqa_bekerja_pada, " _
                & "  sqa_jabatan_bagian, " _
                & "  sqa_kantor_alamat_1, " _
                & "  sqa_kantor_alamat_2, " _
                & "  sqa_kantor_lantai, " _
                & "  sqa_kantor_telp, " _
                & "  sqa_ktp, " _
                & "  sqa_email, " _
                & "  sqa_rumah_alamat_1, " _
                & "  sqa_rumah_alamat_2, " _
                & "  sqa_rumah_kode_pos, " _
                & "  sqa_rumah_telp, " _
                & "  sqa_rumah_hp, " _
                & "  sqa_status_alamat_kirim, " _
                & "  sqa_status_alamat_tagih, " _
                & "  sqa_suami_nama, " _
                & "  sqa_suami_bekerja, " _
                & "  sqa_suami_jabatan, " _
                & "  sqa_suami_kantor_alamat_1, " _
                & "  sqa_suami_kantor_alamat_2, " _
                & "  sqa_suami_telp, " _
                & "  sqa_suami_hp, " _
                & "  sqa_anak_nama_1, " _
                & "  sqa_anak_tgl_lahir_1, " _
                & "  sqa_anak_sekolah_1, " _
                & "  sqa_anak_nama_2, " _
                & "  sqa_anak_tgl_lahir_2, " _
                & "  sqa_anak_sekolah_2, " _
                & "  sqa_anak_nama_3, " _
                & "  sqa_anak_tgl_lahir_3, " _
                & "  sqa_anak_sekolah_3, " _
                & "  sqa_keluarga_dekat_nama, " _
                & "  sqa_keluarga_dekat_alamat_1, " _
                & "  sqa_keluarga_dekat_alamat_2, " _
                & "  sqa_keluarga_dekat_telp, " _
                & "  sqa_keluarga_dekat_hp, " _
                & "  sqa_status_tempat_tinggal, " _
                & "  sqa_jenis_kartu_kredit, " _
                & "  sqa_no_kartu_kredit, " _
                & "  sqa_berlaku_sd, " _
                & "  sqa_dt, " _
                & "  sqa_status_rumah_id, " _
                & "  sqa_lama_tinggal_id, " _
                & "  sqa_masa_kerja_id, " _
                & "  sqa_income_id, " _
                & "  sqa_tanggungan_id, " _
                & "  sr.code_name as status_kepemilikan, " _
                & "  lt.code_name as lama_tinggal, " _
                & "  mk.code_name as masa_kerja, " _
                & "  ic.code_name as income, " _
                & "  tg.code_name as tanggungan, " _
                & "  sqa_bank " _
                & "FROM  " _
                & "  public.sqa_attr" _
                & "  inner join sq_mstr on sq_oid = sqa_sq_oid " _
                & "  left outer join code_mstr sr on sr.code_id = sqa_status_rumah_id " _
                & "   left outer join code_mstr lt on lt.code_id = sqa_lama_tinggal_id " _
                & "   left outer join code_mstr mk on mk.code_id = sqa_masa_kerja_id " _
                & "   left outer join code_mstr ic on ic.code_id = sqa_income_id " _
                & "   left outer join code_mstr tg on tg.code_id = sqa_tanggungan_id " _
                & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "'"

            '& "  where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '& " and sq_en_id in (select user_en_id from tconfuserentity " _
            '& " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_detail_attr, "detail_attr")

            Try
                ds.Tables("detail_kp").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  sokp_oid, " _
                & "  sokp_sq_oid, " _
                & "  sokp_seq, " _
                & "  sokp_ref, " _
                & "  sokp_amount, " _
                & "  sokp_due_date, " _
                & "  sokp_amount_pay, " _
                & "  sokp_description " _
                & "FROM  " _
                & "  public.sokp_piutang " _
                & "  inner join public.sq_mstr on sq_oid = sokp_sq_oid " _
                & " where sokp_sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "'"



            '& "  where sq_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and sq_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            '& " and sq_en_id in (select user_en_id from tconfuserentity " _
            '                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            '& " order by sokp_seq"

            load_data_detail(sql, gc_detail_kp, "detail_kp")

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
                      " inner join sq_mstr on sq_code = wf_ref_code " + _
                      " inner join sqd_det dt on dt.sqd_sq_oid = sq_oid " _
                      & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "' " _
                      & " order by wf_ref_code, wf_seq "

                '& " where sq_date >= " + SetDate(pr_txttglawal.DateTime) _
                '& " and sq_date <= " + SetDate(pr_txttglakhir.DateTime) _
                '& " and sq_en_id in (select user_en_id from tconfuserentity " _
                '                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                '& " order by wf_ref_code, wf_seq "


                load_data_detail(sql, gc_wf, "wf")
                gv_wf.BestFitColumns()

                Try
                    ds.Tables("email").Clear()
                Catch ex As Exception
                End Try

                sql = "SELECT  " _
                           & "  sq_oid, " _
                            & "  sq_en_id, " _
                            & "  sq_add_by, " _
                            & "  sq_add_date, " _
                            & "  sq_upd_by, " _
                            & "  sq_upd_date, " _
                            & "  sq_code, " _
                            & "  sq_date, " _
                            & "  sq_dp, " _
                            & "  sq_disc_header, " _
                            & "  sq_total, " _
                            & "  sq_need_date, " _
                            & "  sq_cons, " _
                            & "  sq_close_date, " _
                            & "  sq_tran_id, " _
                            & "  sq_trans_id, " _
                            & "  sq_trans_rmks, " _
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
                            & "  tax_class.code_name as tax_class_name, " _
                            & "  si_desc, " _
                            & "  pi_desc, " _
                            & "  pay_type.code_name as pay_type_name, " _
                            & "  pay_method.code_name as pay_method_name, " _
                            & "  cc_mstr_ar.cc_desc, " _
                            & "  cu_name, " _
                            & "  tran_name, " _
                            & "  sqd_oid, " _
                            & "  sqd_sq_oid, " _
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
                            & "  sb_mstr_ar.sb_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sqd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sq_mstr " _
                            & "  inner join en_mstr on en_id = sq_en_id " _
                            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sq_ptnr_id_sold " _
                            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sq_ptnr_id_bill " _
                            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sq_sales_person " _
                            & "  inner join pi_mstr on pi_id = sq_pi_id " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = sq_pay_type " _
                            & "  inner join code_mstr pay_method on pay_method.code_id = sq_pay_method " _
                            & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sq_ar_sb_id " _
                            & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sq_ar_cc_id " _
                            & "  inner join cu_mstr on cu_id = sq_cu_id " _
                            & "  inner join tran_mstr on tran_id = sq_tran_id" _
                            & "  inner join sqd_det on sq_oid = sqd_sq_oid " _
                            & "  inner join si_mstr on si_id = sqd_si_id " _
                            & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                            & "  where sq_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString & "' "

                '& " where sq_date >= " + SetDate(pr_txttglawal.DateTime) _
                '& " and sq_date <= " + SetDate(pr_txttglakhir.DateTime) _
                '& " and sq_en_id in (select user_en_id from tconfuserentity " _
                '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                load_data_detail(sql, gc_email, "email")
                gv_email.BestFitColumns()

                ' belum(diaktifkan)

                Try
                    ds.Tables("smart").Clear()
                Catch ex As Exception
                End Try

                sql = "select sq_oid, sq_code, sq_trans_id, false as status from sq_mstr " _
                    & " where sq_trans_id ~~* 'd' "
                load_data_detail(sql, gc_smart, "smart")
            End If
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
                & "  a.sq_oid, " _
                & "  a.sq_code, " _
                & "  a.sq_date, " _
                & "  b.ptnr_name " _
                & "FROM " _
                & "  public.sq_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.sq_ptnr_id_sold = b.ptnr_id) " _
                & "WHERE " _
                & "  a.sq_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code").ToString & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "sq_master")

            sSQL = "SELECT  " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  c.code_code AS um_desc, " _
                & "  a.sqd_price, " _
                & "  a.sqd_qty, " _
                & "  b.pt_isbn,a.sqd_seq " _
                & "FROM " _
                & "  public.pt_mstr b " _
                & "  INNER JOIN public.sqd_det a ON (b.pt_id = a.sqd_pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "  INNER JOIN public.sq_mstr d ON (a.sqd_sq_oid = d.sq_oid) " _
                & "WHERE " _
                & "  d.sq_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code").ToString & "' " _
                & "order by a.sqd_seq"

            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "sq_detail")

            Dim objSaveFileDialog As New SaveFileDialog
            Dim filePath As String

            'Set the Save dialog properties

            With objSaveFileDialog
                .DefaultExt = "xml"
                .FileName = "Export_sq_" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_code").ToString & Now().ToString("yyyyMMdd-HHmmss")
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
                    ds_edit.Tables(0).Rows(i).Item("sqd_loc_id") = gv_edit.GetRowCellValue(_row, "sqd_loc_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    'Private Sub gv_edit_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_edit.SelectionChanged
    '    Try
    '        cek_shipment_row()
    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'comment untuk digantikan dengan pengecekan pada transfer
    'Private Sub cek_shipment_row()
    '    Try

    '        Dim _nilai As Object
    '        _nilai = SetNumber(gv_edit.GetFocusedRowCellValue("status_shipment"))

    '        If _nilai > 0 Then
    '            gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
    '        Else
    '            gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
    '        End If

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    'comment untuk digantikan dengan pengecekan pada transfer
    'Private Sub gv_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.Click
    '    cek_shipment_row()
    'End Sub

    'Private Sub file_xml_pameran_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles file_xml_pameran.ButtonClick
    '    Dim sSQL As String
    '    Dim dt_temp As New DataTable
    '    Dim _row As Integer = 0
    '    Try

    '        Dim filex As String = ""
    '        filex = AskOpenFile("Xml Files | *.xml")

    '        If filex = "" Then
    '            Exit Sub
    '        End If

    '        file_xml_pameran.Text = filex

    '        Dim ds_import As New DataSet
    '        ds_import.ReadXml(filex)
    '        For i As Integer = 0 To gv_edit.RowCount - 1
    '            gv_edit.DeleteRow(i)

    '        Next
    '        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

    '        For Each dr As DataRow In ds_import.Tables(0).Rows
    '            If SetString(dr("status_pajak")).ToUpper = sq_ppn_type.EditValue.ToString.ToUpper Then

    '                sSQL = "SELECT  distinct " _
    '                & "  en_id, " _
    '                & "  en_desc, " _
    '                & "  si_desc, " _
    '                & "  pt_id, " _
    '                & "  pt_code, " _
    '                & "  pt_desc1, " _
    '                & "  pt_desc2, " _
    '                & "  pt_cost, " _
    '                & "  invct_cost, " _
    '                & "  pt_price, " _
    '                & "  pt_type, " _
    '                & "  pt_um, " _
    '                & "  pt_pl_id, " _
    '                & "  pt_ls, " _
    '                & "  pt_loc_id, " _
    '                & "  loc_desc, " _
    '                & "  pt_taxable, " _
    '                & "  pt_tax_inc, " _
    '                & "  pt_tax_class,coalesce(pt_approval_status,'A') as pt_approval_status, " _
    '                & "  tax_class_mstr.code_name as tax_class_name, " _
    '                & "  pt_ppn_type, " _
    '                & "  um_mstr.code_name as um_name " _
    '                & "FROM  " _
    '                & "  public.pt_mstr" _
    '                & " inner join en_mstr on en_id = pt_en_id " _
    '                & " inner join loc_mstr on loc_id = pt_loc_id " _
    '                & " inner join code_mstr um_mstr on pt_um = um_mstr.code_id " _
    '                & " left outer join code_mstr tax_class_mstr on tax_class_mstr.code_id = pt_tax_class " _
    '                & " inner join invct_table on invct_pt_id = pt_id " _
    '                & " inner join si_mstr on si_id = invct_si_id " _
    '                & " where pt_code ='" & dr("kode_barang") & "' "

    '                dt_temp = master_new.PGSqlConn.GetTableData(sSQL)

    '                For Each dr_temp As DataRow In dt_temp.Rows

    '                    Dim ds_bantu As New DataSet

    '                    Try
    '                        Using objcb As New master_new.WDABasepgsql("", "")
    '                            With objcb
    '                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
    '                                                    & "From pla_mstr  " _
    '                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
    '                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
    '                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
    '                                                    & "where pla_pl_id = " + dr_temp("pt_pl_id").ToString _
    '                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
    '                                .InitializeCommand()
    '                                .FillDataSet(ds_bantu, "prodline")

    '                                If ds_bantu.Tables(0).Rows.Count = 0 Then
    '                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
    '                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
    '                                    Exit Sub
    '                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
    '                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
    '                                    dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
    '                                    Exit Sub
    '                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then
    '                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
    '                                        dr_temp("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")
    '                                    Exit Sub
    '                                End If

    '                            End With
    '                        End Using
    '                    Catch ex As Exception
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                    gv_edit.AddNewRow()
    '                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

    '                    gv_edit.SetRowCellValue(_row, "sqd_pt_id", dr_temp("pt_id"))
    '                    gv_edit.SetRowCellValue(_row, "pt_code", dr_temp("pt_code"))
    '                    gv_edit.SetRowCellValue(_row, "pt_desc1", dr_temp("pt_desc1"))
    '                    gv_edit.SetRowCellValue(_row, "pt_desc2", dr_temp("pt_desc2"))
    '                    gv_edit.SetRowCellValue(_row, "pt_type", dr_temp("pt_type"))
    '                    gv_edit.SetRowCellValue(_row, "pt_ls", dr_temp("pt_ls"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_loc_id", dr_temp("pt_loc_id"))
    '                    gv_edit.SetRowCellValue(_row, "loc_desc", dr_temp("loc_desc"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_um", dr_temp("pt_um"))
    '                    gv_edit.SetRowCellValue(_row, "um_name", dr_temp("um_name"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_cost", dr_temp("invct_cost"))

    '                    'If _sq_type <> "D" Then
    '                    '    fobject.gv_edit.SetRowCellValue(_row, "sqd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
    '                    'End If

    '                    gv_edit.SetRowCellValue(_row, "sqd_sales_ac_id", ds_bantu.Tables(0).Rows(0).Item("pla_ac_id"))
    '                    gv_edit.SetRowCellValue(_row, "ac_code_sales", ds_bantu.Tables(0).Rows(0).Item("ac_code"))
    '                    gv_edit.SetRowCellValue(_row, "ac_name_sales", ds_bantu.Tables(0).Rows(0).Item("ac_name"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_sales_sb_id", ds_bantu.Tables(0).Rows(0).Item("pla_sb_id"))
    '                    gv_edit.SetRowCellValue(_row, "sb_desc", ds_bantu.Tables(0).Rows(0).Item("sb_desc"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_sales_cc_id", ds_bantu.Tables(0).Rows(0).Item("pla_cc_id"))
    '                    gv_edit.SetRowCellValue(_row, "cc_desc", ds_bantu.Tables(0).Rows(0).Item("cc_desc"))

    '                    gv_edit.SetRowCellValue(_row, "sqd_disc_ac_id", ds_bantu.Tables(0).Rows(1).Item("pla_ac_id"))
    '                    gv_edit.SetRowCellValue(_row, "ac_code_disc", ds_bantu.Tables(0).Rows(1).Item("ac_code"))
    '                    gv_edit.SetRowCellValue(_row, "ac_name_disc", ds_bantu.Tables(0).Rows(1).Item("ac_name"))

    '                    gv_edit.SetRowCellValue(_row, "sqd_taxable", dr_temp("pt_taxable"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_tax_inc", dr_temp("pt_tax_inc"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_tax_class", dr_temp("pt_tax_class"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_tax_class_name", dr_temp("tax_class_name"))

    '                    gv_edit.SetRowCellValue(_row, "sqd_ppn_type", dr_temp("pt_ppn_type"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_qty", dr("qty_all"))
    '                    gv_edit.SetRowCellValue(_row, "sqd_disc", dr("disc_avg") / 100)

    '                    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

    '                    _row = _row + 1
    '                Next


    '            End If
    '        Next

    '        gv_edit.Columns("sqd_disc").OptionsColumn.AllowEdit = False
    '        gv_edit.Columns("sqd_qty").OptionsColumn.AllowEdit = False

    '        gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
    '        gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub


    Private Sub sq_ptnr_id_sold_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sq_ptnr_id_sold.EditValueChanged

    End Sub

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub
    Private Function rq(ByVal par_str As Object) As String
        Try
            Return SetString(par_str).ToString.Replace("""", "")

        Catch ex As Exception
            Return ""
        End Try
    End Function
    Private Sub btImportSQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btImportSQ.Click
        Dim sSQL As String
        Dim dt_temp As New DataTable
        Dim _row As Integer = 0
        Try

            Dim filex As String = ""
            filex = AskOpenFile("Excel Files | *.xlsx; *.csv")

            If filex = "" Then
                Exit Sub
            End If

            Try
                For i As Integer = 0 To gv_edit.RowCount - 1
                    gv_edit.DeleteRow(i)
                Next
            Catch ex As Exception
            End Try

            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt = New DataTable
            Dim ds_import As New DataSet
            Dim _ext As String
            _ext = IO.Path.GetExtension(filex).ToLower
            If IO.Path.GetExtension(filex).ToLower = ".csv" Then
                ds_import = master_new.excelconn.ImportCsv(filex)

                'Dim dt As New System.Data.DataTable
                LblStatus.Text = "Total Data : " & ds_import.Tables(0).Rows.Count
                Dim i As Integer = 0
                Dim n As Integer = 0
                For Each dr As DataRow In ds_import.Tables(0).Rows
                    If dr.ItemArray.Length = 1 Then
                        If i = 0 Then
                            Dim cols = dr(0).Split(",")
                            For Each col In cols
                                'If rq(col) <> "" Then
                                dt.Columns.Add(New DataColumn(rq(col), GetType(String)))
                                'Else
                                '    dt.Columns.Add(New DataColumn(n.ToString, GetType(String)))
                                'End If
                                n += 1
                            Next
                        Else

                            Dim data = dr(0).Split(",")
                            'dt.Rows.Add()

                            Dim myDRow As DataRow = dt.NewRow
                            myDRow.ItemArray = data
                            dt.Rows.Add(myDRow)

                        End If
                    Else
                        If i = 0 Then
                            Dim cols = dr.ItemArray
                            For Each col In cols
                                'If rq(col) <> "" Then
                                dt.Columns.Add(New DataColumn(col.ToString, GetType(String)))
                                'Else
                                '    dt.Columns.Add(New DataColumn(n.ToString, GetType(String)))
                                'End If
                                n += 1
                            Next
                        Else

                            Dim data = dr.ItemArray
                            'dt.Rows.Add()

                            Dim myDRow As DataRow = dt.NewRow
                            myDRow.ItemArray = data
                            dt.Rows.Add(myDRow)

                        End If
                    End If

                    i += 1

                Next



                Dim _en_id As String = ""
                Dim _sales_id As String = ""
                Dim _customer_id As String = ""
                Dim _pay_type As String = ""
                Dim _price_list As String = ""


                For Each dr_a As DataRow In dt.Rows

                    _en_id = get_id("en_mstr", "lower(en_desc)", rq(dr_a("Wilayah")).ToLower, "en_id")
                    sq_en_id.EditValue = CInt(_en_id)

                    If rq(dr_a("Tanggal")) = "" Then
                        Box("Kolom tanggal kosong")
                    Else
                        sq_date.EditValue = CDate(rq(dr_a("Tanggal")))
                    End If

                    _sales_id = SetNumber(get_id("ptnr_mstr", "lower(ptnr_code)", rq(dr_a("Kode Sales")).ToLower, "ptnr_id"))
                    If _sales_id = 0 Then
                        Box("Kode sales tidak dikenali")
                        Exit Sub
                    End If

                    sq_sales_person.EditValue = CInt(_sales_id)

                    _pay_type = get_id("code_mstr", "lower(code_name)", rq(dr_a("Jenis Transaksi")).ToLower, "code_id", "payment_type")
                    sq_pay_type.EditValue = CInt(_pay_type)

                    If SetString(rq(dr_a("Kode Konsumen"))) <> "" Then
                        _customer_id = SetNumber(get_id("ptnr_mstr", "lower(ptnr_code)", rq(dr_a("Kode Konsumen")).ToLower, "ptnr_id"))
                        If _customer_id = 0 Then
                            Box("Kode Customer tidak dikenali")
                            Exit Sub
                        End If
                        _sq_ptnr_id_sold_mstr = CInt(_customer_id)
                    End If

                    sq_type.EditValue = "D"
                    sq_ptnr_id_sold.Text = SetString(rq(dr_a("Nama Konsumen")))

                    sq_sales_program.EditValue = get_id("sls_program", "lower(sls_desc)", rq(dr_a("Program Penjualan")).ToLower, "sls_code")

                    sq_status_produk.EditValue = rq(dr_a("Status Produk Pembelian"))
                    sq_diskon_produk.EditValue = rq(dr_a("Status Diskon Produk Pembelian"))


                    '_price_list = get_id("pi_mstr", "lower(pi_desc)", rq(dr_a("Price list")).ToLower, "pi_id")
                    'sq_pi_id.EditValue = CInt(_price_list)

                    For x As Integer = 1 To 4


                        Dim _pt_code As String = ""
                        If SetString(rq(dr_a("Nama Barang " & x))) <> "" Then
                            _pt_code = get_id("pt_mstr", "lower(pt_desc1)", rq(dr_a("Nama Barang " & x)).ToString.ToLower, "pt_code")

                            'Quantity 1

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
                               & " where pt_code ='" & _pt_code & "' "

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

                                'If _sq_type <> "D" Then
                                '    fobject.gv_edit.SetRowCellValue(_row, "sqd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
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
                                gv_edit.SetRowCellValue(_row, "sqd_qty", SetNumber(rq(dr_a("Quantity " & x))))
                                'gv_edit.SetRowCellValue(_row, "sqd_disc", SetNumber(dr("Diskon %")) / 100)

                                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                                _row = _row + 1
                            Next
                        End If
                    Next

                    gv_edit.BestFitColumns()


                    'If SetString(dr(0)) = "KJB Number" Then
                    sqa_kjb_code.Text = SetString(rq(dr_a("AJB Number")))
                    'End If

                    'If SetString(dr(0)) = "Tempat Bekerja" Then
                    sqa_bekerja_pada.Text = SetString(rq(dr_a("Tempat Bekerja")))
                    'End If

                    'If SetString(dr(0)) = "Jabatan" Then
                    sqa_jabatan_bagian.Text = SetString(rq(dr_a("Jabatan")))
                    'End If

                    'If SetString(dr(0)) = "Kantor Alamat1" Then
                    sqa_kantor_alamat_1.Text = SetString(rq(dr_a("Kantor Alamat 1")))

                    'End If

                    'If SetString(dr(0)) = "Kantor Alamat2" Then
                    sqa_kantor_alamat_2.Text = SetString(rq(dr_a("Kantor Alamat 2")))
                    'End If

                    'If SetString(dr(0)) = "Lantai Bekerja" Then
                    sqa_kantor_lantai.Text = SetString(rq(dr_a("Lantai Bekerja")))
                    'End If

                    'If SetString(dr(0)) = "Kantor Telephone" Then
                    sqa_kantor_telp.Text = SetString(rq(dr_a("Kantor Telepon")))
                    'End If

                    'If SetString(dr(0)) = "KTP" Then
                    sqa_ktp.Text = SetString(rq(dr_a("KTP")))
                    'End If

                    'If SetString(dr(0)) = "Email" Then
                    sqa_email.Text = SetString(rq(dr_a("Email")))
                    'End If

                    'If SetString(dr(0)) = "Rumah Alamat1" Then
                    sqa_rumah_alamat_1.Text = SetString(rq(dr_a("Alamat Rumah 1")))
                    sq_bantu_address.Text = SetString(rq(dr_a("Alamat Rumah 1")))
                    'End If

                    'If SetString(dr(0)) = "Rumah Alamat2" Then
                    sqa_rumah_alamat_2.Text = SetString(rq(dr_a("Alamat Rumah 2")))
                    'End If
                    'If SetString(dr(0)) = "Rumah Kode Pos" Then
                    sqa_rumah_kode_pos.Text = SetString(rq(dr_a("Kode pos rumah")))
                    'End If
                    'If SetString(dr(0)) = "Rumah Telephone" Then
                    sqa_rumah_telp.Text = SetString(rq(dr_a("Telepon rumah")))
                    'End If
                    'If SetString(dr(0)) = "Telephone HP" Then
                    sqa_rumah_hp.Text = SetString(rq(dr_a("Telepon Hp")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Nama" Then
                    sqa_suami_nama.Text = SetString(rq(dr_a("Nama suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Tempat Bekerja" Then
                    sqa_suami_bekerja.Text = SetString(rq(dr_a("Tempat suami/istri bekerja")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Jabatan" Then
                    sqa_suami_jabatan.Text = SetString(rq(dr_a("Jabatan suami/istri bekerja")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Kantor Alamat1" Then
                    sqa_suami_kantor_alamat_1.Text = SetString(rq(dr_a("Kantor alamat suami/istri bekerja (1)")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Kantor Alamat2" Then
                    sqa_suami_kantor_alamat_2.Text = SetString(rq(dr_a("Kantor alamat suami/istri bekerja (2)")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Telephone" Then
                    sqa_suami_telp.Text = SetString(rq(dr_a("Nomor Telepon suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri HP" Then
                    sqa_suami_hp.Text = SetString(rq(dr_a("Nomor Telepon HP suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Nama" Then
                    sqa_anak_nama_1.Text = SetString(rq(dr_a("Nama anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Tgl Lahir" Then
                    sqa_anak_tgl_lahir_1.EditValue = SetString(rq(dr_a("Tanggal lahir anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Sekolah" Then
                    sqa_anak_sekolah_1.Text = SetString(rq(dr_a("Tempat Sekolah anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Nama" Then
                    sqa_anak_nama_2.Text = SetString(rq(dr_a("Nama anak kedua")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Tgl Lahir" Then
                    sqa_anak_tgl_lahir_2.EditValue = SetString(rq(dr_a("Tanggal lahir anak kedua")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Sekolah" Then
                    sqa_anak_sekolah_2.Text = SetString(rq(dr_a("Tempat Sekolah anak kedua")))
                    'End If

                    'If SetString(dr(0)) = "Anak ketiga Nama" Then
                    sqa_anak_nama_3.Text = SetString(rq(dr_a("Nama anak ketiga")))
                    'End If
                    'If SetString(dr(0)) = "Anak ketiga Tgl Lahir" Then
                    sqa_anak_tgl_lahir_3.EditValue = SetString(rq(dr_a("Tanggal lahir anak ketiga")))
                    'End If
                    'If SetString(dr(0)) = "Anak ketiga Sekolah" Then
                    sqa_anak_sekolah_3.Text = SetString(rq(dr_a("Tempat Sekolah anak ketiga")))
                    'End If

                    'If SetString(dr(0)) = "Keluarga Dekat Nama" Then
                    sqa_keluarga_dekat_nama.Text = SetString(rq(dr_a("Nama keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat Alamat1" Then
                    sqa_keluarga_dekat_alamat_1.EditValue = SetString(rq(dr_a("Alamat 1 keluarga dekat")))
                    ' End If
                    'If SetString(dr(0)) = "Keluarga Dekat Alamat2" Then
                    sqa_keluarga_dekat_alamat_2.Text = SetString(rq(dr_a("Alamat 2 keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat Telephone" Then
                    sqa_keluarga_dekat_telp.Text = SetString(rq(dr_a("Nomor telepon rumah keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat HP" Then
                    sqa_keluarga_dekat_hp.EditValue = SetString(rq(dr_a("Nomor telepon HP keluarga dekat")))
                    'End If

                    'If SetString(dr(0)) = "Jenis Kartu Kredit" Then
                    sqa_jenis_kartu_kredit.Text = SetString(rq(dr_a("Jenis Kartu Kredit")))
                    'End If
                    'If SetString(dr(0)) = "No Kartu Kredit" Then
                    sqa_no_kartu_kredit.Text = SetString(rq(dr_a("Nomor kartu kredit")))
                    'End If
                    'If SetString(dr(0)) = "Berlaku s/d" Then
                    sqa_berlaku_sd.Text = SetString(rq(dr_a("Kartu Kredit berlaku sampai dengan")))
                    'End If
                    'If SetString(dr(0)) = "Bank" Then
                    sqa_bank.Text = SetString(rq(dr_a("Nama bank yang digunakan")))
                    'End If

                    'If SetString(dr(0)) = "Status Rumah Tinggal" Then
                    sqa_status_rumah_id.EditValue = SetNumberNull(get_id("code_mstr", " code_field =  'resident_status_mstr' and  lower(code_desc)", SetString(rq(dr_a("Status tempat tinggal"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Lama Tinggal" Then
                    sqa_lama_tinggal_id.EditValue = SetNumberNull(get_id("code_mstr", " code_field =  'resident_stay_mstr' and  lower(code_desc)", SetString(rq(dr_a("Lama tinggal"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Masa Kerja" Then
                    sqa_masa_kerja_id.EditValue = SetNumberNull(get_id("code_mstr", " code_field =  'work_age_mstr' and  lower(code_desc)", SetString(rq(dr_a("Masa Kerja"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Income" Then
                    sqa_income_id.EditValue = SetNumberNull(get_id("code_mstr", " code_field =  'income_mstr' and  lower(code_desc)", SetString(rq(dr_a("Income perbulan"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Tanggungan" Then
                    sqa_tanggungan_id.EditValue = SetNumberNull(get_id("code_mstr", " code_field =  'mortage_mstr' and  lower(code_desc)", SetString(rq(dr_a("Tanggungan"))).ToString.ToLower, "code_id"))
                    'End If
                    _id_proses = 1
                    LblStatus.Text = "Data processed : 1 of " & dt.Rows.Count
                    Exit Sub
                Next
            Else
                ds_import = master_new.excelconn.ImportExcel(filex, "Data SQ")


                Dim _en_id As String = ""
                Dim _sales_id As String = ""
                Dim _customer_id As String = ""
                Dim _pay_type As String = ""
                Dim _price_list As String = ""



                For Each dr As DataRow In ds_import.Tables(0).Rows

                    If SetString(dr(0)) = "Wilayah" Then
                        _en_id = get_id("en_mstr", "lower(en_desc)", dr(1).ToString.ToLower, "en_id")
                        sq_en_id.EditValue = CInt(_en_id)
                    End If

                    If SetString(dr(0)) = "Tanggal" Then
                        If dr(1).ToString = "" Then
                            Box("Kolom tanggal kosong")
                        Else
                            sq_date.EditValue = CDate(dr(1).ToString)
                        End If

                    End If

                    If SetString(dr(0)) = "Kode Sales" Then
                        _sales_id = SetNumber(get_id("ptnr_mstr", "lower(ptnr_code)", dr(1).ToString.ToLower, "ptnr_id"))

                        If _sales_id = 0 Then
                            Box("Kode sales tidak dikenali")
                            Exit Sub
                        End If
                        sq_sales_person.EditValue = CInt(_sales_id)
                    End If

                    If SetString(dr(0)) = "Jenis Transaksi" Then
                        _pay_type = get_id("code_mstr", "lower(code_name)", dr(1).ToString.ToLower, "code_id", "payment_type")
                        sq_pay_type.EditValue = CInt(_pay_type)
                    End If

                    If SetString(dr(0)) = "Kode Konsumen" Then
                        If SetString(dr(1)) <> "" Then
                            _customer_id = SetNumber(get_id("ptnr_mstr", "lower(ptnr_code)", dr(1).ToString.ToLower, "ptnr_id"))
                            If _customer_id = 0 Then
                                Box("Kode Customer tidak dikenali")
                                Exit Sub
                            End If
                            _sq_ptnr_id_sold_mstr = CInt(_customer_id)
                        End If
                    End If

                    If SetString(dr(0)) = "Nama Konsumen" Then
                        sq_ptnr_id_sold.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Program Penjualan" Then
                        sq_sales_program.EditValue = get_id("sls_program", "lower(sls_desc)", rq(dr(1)).ToLower, "sls_code")
                    End If

                    If SetString(dr(0)) = "Status Produk Pembelian" Then
                        sq_status_produk.EditValue = rq(dr(1))
                    End If

                    If SetString(dr(0)) = "Status Diskon Produk Pembelian" Then
                        sq_diskon_produk.EditValue = rq(dr(1))

                    End If


                    sq_type.EditValue = "D"

                    'If SetString(dr(0)) = "Price List" Then
                    '    _price_list = get_id("pi_mstr", "lower(pi_desc)", dr(2).ToString.ToLower, "pi_id")
                    '    sq_pi_id.EditValue = CInt(_price_list)
                    'End If

                    If SetString(dr(0)) = "Nama Barang 1" Or SetString(dr(0)) = "Nama Barang 2" _
                    Or SetString(dr(0)) = "Nama Barang 3" Or SetString(dr(0)) = "Nama Barang 4" Then

                        Dim _pt_code As String = ""
                        If SetString(dr(1)) <> "" Then
                            _pt_code = get_id("pt_mstr", "lower(pt_desc1)", dr(1).ToString.ToLower, "pt_code")


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
                               & " where pt_code ='" & _pt_code & "' "

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

                                'If _sq_type <> "D" Then
                                '    fobject.gv_edit.SetRowCellValue(_row, "sqd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
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
                                gv_edit.SetRowCellValue(_row, "sqd_qty", SetNumber(1))
                                'gv_edit.SetRowCellValue(_row, "sqd_disc", SetNumber(dr("Diskon %")) / 100)

                                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                                _row = _row + 1
                            Next
                        End If
                        gv_edit.BestFitColumns()

                    End If

                    If SetString(dr(0)) = "Quantity 1" Then
                        Try
                            If SetNumber(dr(1)) > 0 Then
                                gv_edit.SetRowCellValue(0, "sqd_qty", SetNumber(dr(1)))
                            End If

                        Catch ex As Exception

                        End Try

                    End If

                    If SetString(dr(0)) = "Quantity 2" Then
                        Try
                            If SetNumber(dr(1)) > 0 Then
                                gv_edit.SetRowCellValue(1, "sqd_qty", SetNumber(dr(1)))
                            End If

                        Catch ex As Exception

                        End Try
                    End If

                    If SetString(dr(0)) = "Quantity 3" Then
                        Try
                            If SetNumber(dr(1)) > 0 Then
                                gv_edit.SetRowCellValue(2, "sqd_qty", SetNumber(dr(1)))
                            End If


                        Catch ex As Exception

                        End Try
                    End If

                    If SetString(dr(0)) = "Quantity 4" Then
                        Try
                            If SetNumber(dr(1)) > 0 Then
                                gv_edit.SetRowCellValue(3, "sqd_qty", SetNumber(dr(1)))
                            End If


                        Catch ex As Exception

                        End Try
                    End If

                    If SetString(dr(0)) = "KJB Number" Then
                        sqa_kjb_code.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Tempat Bekerja" Then
                        sqa_bekerja_pada.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Jabatan" Then
                        sqa_jabatan_bagian.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Kantor Alamat1" Then
                        sqa_kantor_alamat_1.Text = SetString(dr(1))

                    End If

                    If SetString(dr(0)) = "Kantor Alamat2" Then
                        sqa_kantor_alamat_2.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Lantai Bekerja" Then
                        sqa_kantor_lantai.Text = SetString(dr(1))
                    End If


                    If SetString(dr(0)) = "Kantor Telephone" Then
                        sqa_kantor_telp.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "KTP" Then
                        sqa_ktp.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Email" Then
                        sqa_email.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Rumah Alamat1" Then
                        sqa_rumah_alamat_1.Text = SetString(dr(1))
                        sq_bantu_address.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Rumah Alamat2" Then
                        sqa_rumah_alamat_2.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Rumah Kode Pos" Then
                        sqa_rumah_kode_pos.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Rumah Telephone" Then
                        sqa_rumah_telp.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Telephone HP" Then
                        sqa_rumah_hp.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Nama" Then
                        sqa_suami_nama.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Tempat Bekerja" Then
                        sqa_suami_bekerja.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Jabatan" Then
                        sqa_suami_jabatan.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Kantor Alamat1" Then
                        sqa_suami_kantor_alamat_1.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Kantor Alamat2" Then
                        sqa_suami_kantor_alamat_2.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri Telephone" Then
                        sqa_suami_telp.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Suami/Istri HP" Then
                        sqa_suami_hp.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak Pertama Nama" Then
                        sqa_anak_nama_1.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak Pertama Tgl Lahir" Then
                        sqa_anak_tgl_lahir_1.EditValue = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak Pertama Sekolah" Then
                        sqa_anak_sekolah_1.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak kedua Nama" Then
                        sqa_anak_nama_2.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak kedua Tgl Lahir" Then
                        sqa_anak_tgl_lahir_2.EditValue = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak kedua Sekolah" Then
                        sqa_anak_sekolah_2.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Anak ketiga Nama" Then
                        sqa_anak_nama_3.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak ketiga Tgl Lahir" Then
                        sqa_anak_tgl_lahir_3.EditValue = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Anak ketiga Sekolah" Then
                        sqa_anak_sekolah_3.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Keluarga Dekat Nama" Then
                        sqa_keluarga_dekat_nama.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Keluarga Dekat Alamat1" Then
                        sqa_keluarga_dekat_alamat_1.EditValue = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Keluarga Dekat Alamat2" Then
                        sqa_keluarga_dekat_alamat_2.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Keluarga Dekat Telephone" Then
                        sqa_keluarga_dekat_telp.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Keluarga Dekat HP" Then
                        sqa_keluarga_dekat_hp.EditValue = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Jenis Kartu Kredit" Then
                        sqa_jenis_kartu_kredit.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "No Kartu Kredit" Then
                        sqa_no_kartu_kredit.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Berlaku s/d" Then
                        sqa_berlaku_sd.Text = SetString(dr(1))
                    End If
                    If SetString(dr(0)) = "Bank" Then
                        sqa_bank.Text = SetString(dr(1))
                    End If

                    If SetString(dr(0)) = "Status Rumah Tinggal" Then
                        sqa_status_rumah_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'resident_status_mstr' and  lower(code_desc)", dr(1).ToString.ToLower, "code_id"))
                    End If
                    If SetString(dr(0)) = "Lama Tinggal" Then
                        sqa_lama_tinggal_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'resident_stay_mstr' and  lower(code_desc)", dr(1).ToString.ToLower, "code_id"))
                    End If
                    If SetString(dr(0)) = "Masa Kerja" Then
                        sqa_masa_kerja_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'work_age_mstr' and  lower(code_desc)", dr(1).ToString.ToLower, "code_id"))
                    End If
                    If SetString(dr(0)) = "Income" Then
                        sqa_income_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'income_mstr' and  lower(code_desc)", dr(1).ToString.ToLower, "code_id"))
                    End If
                    If SetString(dr(0)) = "Tanggungan" Then
                        sqa_tanggungan_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'mortage_mstr' and  lower(code_desc)", dr(1).ToString.ToLower, "code_id"))
                    End If

                Next
            End If





            ' sq_bantu_address.Text = SetString(dr(1))



            'gv_edit.Columns("sqd_disc").OptionsColumn.AllowEdit = False
            'gv_edit.Columns("sqd_qty").OptionsColumn.AllowEdit = False

            'gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
            'gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Function SetNumberNull(ByVal Str As Object) As Object
        SetNumberNull = Nothing

        If Str Is System.DBNull.Value Or Str Is Nothing Then
            Return SetNumberNull
        End If

        If CStr(Str) = "" Then
            Return SetNumberNull
        End If
        Return CType(Str, Double)
    End Function

    Public Function ImportExcel(ByVal PrmPathExcelFile As String, ByVal par_sheet As String) As DataSet

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        MyConnection = Nothing
        Try

            ''''''' Fetch Data from Excel
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                            "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")

            ' Select the data from Sheet1 of the workbook.
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & par_sheet & "$]", MyConnection)

            MyCommand.TableMappings.Add("Table", "result")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
            Return Nothing
        End Try

    End Function

    Private Sub sq_pi_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sq_pi_id.EditValueChanged
        Try
            For i As Integer = 0 To gv_edit.RowCount - 1
                dt_bantu = New DataTable
                dt_bantu = (load_price_list(sq_pi_id.EditValue, gv_edit.GetRowCellValue(i, "sqd_pt_id"), sq_pay_type.EditValue, gv_edit.GetRowCellValue(i, "sqd_qty")))

                If dt_bantu.Rows.Count > 0 Then
                    gv_edit.SetRowCellValue(i, "sqd_price", dt_bantu.Rows(0).Item("pidd_price"))
                    gv_edit.SetRowCellValue(i, "sqd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(i, "sqd_payment", 0)
                    gv_edit.SetRowCellValue(i, "sqd_dp", 0)
                    gv_edit.SetRowCellValue(i, "sqd_payment", dt_bantu.Rows(0).Item("pidd_payment"))
                    gv_edit.SetRowCellValue(i, "sqd_dp", dt_bantu.Rows(0).Item("pidd_dp"))
                    gv_edit.SetRowCellValue(i, "sqd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                Else
                    gv_edit.SetRowCellValue(i, "sqd_price", 0)
                    gv_edit.SetRowCellValue(i, "sqd_disc", 0)
                    gv_edit.SetRowCellValue(i, "sqd_payment", 0)
                    gv_edit.SetRowCellValue(i, "sqd_dp", 0)
                    gv_edit.SetRowCellValue(i, "sqd_sales_unit", 0)
                End If
            Next
            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            gv_edit.BestFitColumns()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtNext.Click
        Try
            Dim _en_id As String = ""
            Dim _sales_id As String = ""
            Dim _customer_id As String = ""
            Dim _pay_type As String = ""
            Dim _price_list As String = ""

            Dim y As Integer = 1

            For Each dr_a As DataRow In dt.Rows

                If y = _id_proses + 1 Then
                    _en_id = get_id("en_mstr", "lower(en_desc)", rq(dr_a("Wilayah")).ToLower, "en_id")
                    sq_en_id.EditValue = CInt(_en_id)

                    If rq(dr_a("Tanggal")) = "" Then
                        Box("Kolom tanggal kosong")
                    Else
                        sq_date.EditValue = CDate(rq(dr_a("Tanggal")))
                    End If

                    _sales_id = get_id("ptnr_mstr", "ptnr_code", rq(dr_a("Kode Sales")), "ptnr_id")
                    sq_sales_person.EditValue = CInt(_sales_id)

                    _pay_type = get_id("code_mstr", "lower(code_name)", rq(dr_a("Jenis Transaksi")).ToLower, "code_id")
                    sq_pay_type.EditValue = CInt(_pay_type)

                    If SetString(rq(dr_a("Kode Konsumen"))) <> "" Then
                        _customer_id = get_id("ptnr_mstr", "ptnr_code", rq(dr_a("Kode Konsumen")), "ptnr_id")
                        _sq_ptnr_id_sold_mstr = CInt(_customer_id)
                    End If

                    sq_type.EditValue = "D"
                    sq_ptnr_id_sold.Text = SetString(rq(dr_a("Nama Konsumen")))

                    '_price_list = get_id("pi_mstr", "lower(pi_desc)", rq(dr_a("Price list")).ToLower, "pi_id")
                    'sq_pi_id.EditValue = CInt(_price_list)
                    Dim _row As Integer = 0

                    For x As Integer = 1 To 4


                        Dim _pt_code As String = ""
                        If SetString(rq(dr_a("Nama Barang " & x))) <> "" Then
                            _pt_code = get_id("pt_mstr", "lower(pt_desc1)", rq(dr_a("Nama Barang " & x)).ToString.ToLower, "pt_code")

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
                               & " where pt_code ='" & _pt_code & "' "

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

                                'If _sq_type <> "D" Then
                                '    fobject.gv_edit.SetRowCellValue(_row, "sqd_price", ds.Tables(0).Rows(_row_gv).Item("pt_price"))
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
                                gv_edit.SetRowCellValue(_row, "sqd_qty", SetNumber(1))
                                'gv_edit.SetRowCellValue(_row, "sqd_disc", SetNumber(dr("Diskon %")) / 100)

                                gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)

                                _row = _row + 1
                            Next
                        End If
                    Next

                    gv_edit.BestFitColumns()


                    'If SetString(dr(0)) = "KJB Number" Then
                    sqa_kjb_code.Text = SetString(rq(dr_a("AJB Number")))
                    'End If

                    'If SetString(dr(0)) = "Tempat Bekerja" Then
                    sqa_bekerja_pada.Text = SetString(rq(dr_a("Tempat Bekerja")))
                    'End If

                    'If SetString(dr(0)) = "Jabatan" Then
                    sqa_jabatan_bagian.Text = SetString(rq(dr_a("Jabatan")))
                    'End If

                    'If SetString(dr(0)) = "Kantor Alamat1" Then
                    sqa_kantor_alamat_1.Text = SetString(rq(dr_a("Kantor Alamat 1")))

                    'End If

                    'If SetString(dr(0)) = "Kantor Alamat2" Then
                    sqa_kantor_alamat_2.Text = SetString(rq(dr_a("Kantor Alamat 2")))
                    'End If

                    'If SetString(dr(0)) = "Lantai Bekerja" Then
                    sqa_kantor_lantai.Text = SetString(rq(dr_a("Lantai Bekerja")))
                    'End If

                    'If SetString(dr(0)) = "Kantor Telephone" Then
                    sqa_kantor_telp.Text = SetString(rq(dr_a("Kantor Telepon")))
                    'End If

                    'If SetString(dr(0)) = "KTP" Then
                    sqa_ktp.Text = SetString(rq(dr_a("KTP")))
                    'End If

                    'If SetString(dr(0)) = "Email" Then
                    sqa_email.Text = SetString(rq(dr_a("Email")))
                    'End If

                    'If SetString(dr(0)) = "Rumah Alamat1" Then
                    sqa_rumah_alamat_1.Text = SetString(rq(dr_a("Alamat Rumah 1")))
                    sq_bantu_address.Text = SetString(rq(dr_a("Alamat Rumah 1")))
                    'End If

                    'If SetString(dr(0)) = "Rumah Alamat2" Then
                    sqa_rumah_alamat_2.Text = SetString(rq(dr_a("Alamat Rumah 2")))
                    'End If
                    'If SetString(dr(0)) = "Rumah Kode Pos" Then
                    sqa_rumah_kode_pos.Text = SetString(rq(dr_a("Kode pos rumah")))
                    'End If
                    'If SetString(dr(0)) = "Rumah Telephone" Then
                    sqa_rumah_telp.Text = SetString(rq(dr_a("Telepon rumah")))
                    'End If
                    'If SetString(dr(0)) = "Telephone HP" Then
                    sqa_rumah_hp.Text = SetString(rq(dr_a("Telepon Hp")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Nama" Then
                    sqa_suami_nama.Text = SetString(rq(dr_a("Nama suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Tempat Bekerja" Then
                    sqa_suami_bekerja.Text = SetString(rq(dr_a("Tempat suami/istri bekerja")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Jabatan" Then
                    sqa_suami_jabatan.Text = SetString(rq(dr_a("Jabatan suami/istri bekerja")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Kantor Alamat1" Then
                    sqa_suami_kantor_alamat_1.Text = SetString(rq(dr_a("Kantor alamat suami/istri bekerja (1)")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Kantor Alamat2" Then
                    sqa_suami_kantor_alamat_2.Text = SetString(rq(dr_a("Kantor alamat suami/istri bekerja (2)")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri Telephone" Then
                    sqa_suami_telp.Text = SetString(rq(dr_a("Nomor Telepon suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Suami/Istri HP" Then
                    sqa_suami_hp.Text = SetString(rq(dr_a("Nomor Telepon HP suami/istri")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Nama" Then
                    sqa_anak_nama_1.Text = SetString(rq(dr_a("Nama anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Tgl Lahir" Then
                    sqa_anak_tgl_lahir_1.EditValue = SetString(rq(dr_a("Tanggal lahir anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak Pertama Sekolah" Then
                    sqa_anak_sekolah_1.Text = SetString(rq(dr_a("Tempat Sekolah anak pertama")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Nama" Then
                    sqa_anak_nama_2.Text = SetString(rq(dr_a("Nama anak kedua")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Tgl Lahir" Then
                    sqa_anak_tgl_lahir_2.EditValue = SetString(rq(dr_a("Tanggal lahir anak kedua")))
                    'End If
                    'If SetString(dr(0)) = "Anak kedua Sekolah" Then
                    sqa_anak_sekolah_2.Text = SetString(rq(dr_a("Tempat Sekolah anak kedua")))
                    'End If

                    'If SetString(dr(0)) = "Anak ketiga Nama" Then
                    sqa_anak_nama_3.Text = SetString(rq(dr_a("Nama anak ketiga")))
                    'End If
                    'If SetString(dr(0)) = "Anak ketiga Tgl Lahir" Then
                    sqa_anak_tgl_lahir_3.EditValue = SetString(rq(dr_a("Tanggal lahir anak ketiga")))
                    'End If
                    'If SetString(dr(0)) = "Anak ketiga Sekolah" Then
                    sqa_anak_sekolah_3.Text = SetString(rq(dr_a("Tempat Sekolah anak ketiga")))
                    'End If

                    'If SetString(dr(0)) = "Keluarga Dekat Nama" Then
                    sqa_keluarga_dekat_nama.Text = SetString(rq(dr_a("Nama keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat Alamat1" Then
                    sqa_keluarga_dekat_alamat_1.EditValue = SetString(rq(dr_a("Alamat 1 keluarga dekat")))
                    ' End If
                    'If SetString(dr(0)) = "Keluarga Dekat Alamat2" Then
                    sqa_keluarga_dekat_alamat_2.Text = SetString(rq(dr_a("Alamat 2 keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat Telephone" Then
                    sqa_keluarga_dekat_telp.Text = SetString(rq(dr_a("Nomor telepon rumah keluarga dekat")))
                    'End If
                    'If SetString(dr(0)) = "Keluarga Dekat HP" Then
                    sqa_keluarga_dekat_hp.EditValue = SetString(rq(dr_a("Nomor telepon HP keluarga dekat")))
                    'End If

                    'If SetString(dr(0)) = "Jenis Kartu Kredit" Then
                    sqa_jenis_kartu_kredit.Text = SetString(rq(dr_a("Jenis Kartu Kredit")))
                    'End If
                    'If SetString(dr(0)) = "No Kartu Kredit" Then
                    sqa_no_kartu_kredit.Text = SetString(rq(dr_a("Nomor kartu kredit")))
                    'End If
                    'If SetString(dr(0)) = "Berlaku s/d" Then
                    sqa_berlaku_sd.Text = SetString(rq(dr_a("Kartu Kredit berlaku sampai dengan")))
                    'End If
                    'If SetString(dr(0)) = "Bank" Then
                    sqa_bank.Text = SetString(rq(dr_a("Nama bank yang digunakan")))
                    'End If

                    'If SetString(dr(0)) = "Status Rumah Tinggal" Then
                    sqa_status_rumah_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'resident_status_mstr' and  lower(code_desc)", SetString(rq(dr_a("Status tempat tinggal"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Lama Tinggal" Then
                    sqa_lama_tinggal_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'resident_stay_mstr' and  lower(code_desc)", SetString(rq(dr_a("Lama tinggal"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Masa Kerja" Then
                    sqa_masa_kerja_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'work_age_mstr' and  lower(code_desc)", SetString(rq(dr_a("Masa Kerja"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Income" Then
                    sqa_income_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'income_mstr' and  lower(code_desc)", SetString(rq(dr_a("Income perbulan"))).ToString.ToLower, "code_id"))
                    'End If
                    'If SetString(dr(0)) = "Tanggungan" Then
                    sqa_tanggungan_id.EditValue = SetNumber(get_id("code_mstr", " code_field =  'mortage_mstr' and  lower(code_desc)", SetString(rq(dr_a("Tanggungan"))).ToString.ToLower, "code_id"))
                    'End If
                    _id_proses = _id_proses + 1
                    LblStatus.Text = "Data processed : " & _id_proses & " of " & dt.Rows.Count
                    Exit Sub
                End If

                y = y + 1

            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try

            Dim ssql As String
            Dim result As Object
            Dim _url As String
            Dim _err As New ArrayList

            '_url = "https://testapi.sdi.id/api/orders?filter=%7B%22where%22%3A%7B%22status%22%3A1%2C%22created_at%22%3A%7B%22between%22%3A%5B%222020-08-03%2004%3A46%3A00%22%2C%222021-05-07%2016%3A59%3A59%22%5D%7D%7D%2C%22include%22%3A%5B%22invoice%22%2C%7B%22order_items%22%3A%22product_set%22%7D%5D%7D&access_token=NOL9vyqrF7pupPXLVlUpfGAEbQNHOHEkvWAmFmBzbSMtU4yRGiiZmRHxSbAVGRLr"
            '_url = "https://testapi.sdi.id/api/orders?filter=" _
            '        & {"where":{"status":1,"created_at":{"between":["2020-08-03 04:46:00","2021-05-07 16:59:59"]}},"include":["invoice",{"order_items":"product_set"}]}&access_token=NOL9vyqrF7pupPXLVlUpfGAEbQNHOHEkvWAmFmBzbSMtU4yRGiiZmRHxSbAVGRLr"

            Dim tgl_awal, tgl_akhir As String


            tgl_awal = Format(pr_txttglawal.DateTime.Date, "yyyy-MM-dd HH:mm:ss")
            tgl_akhir = Format(pr_txttglakhir.DateTime.Date, "yyyy-MM-dd HH:mm:ss")

            'tgl_awal = "2020-08-03 04:46:00"
            'tgl_akhir = "2021-04-07 16:59:59"



            _url = "http://192.168.1.140/php56/json/curl_api_sdi_order.php?" & "tgl_awal=" & tgl_awal & "&tgl_akhir=" & tgl_akhir

            '_url = _url_api & "orders?filter=" _
            '        & "{""where"":{""status"":1,""created_at"":{""between"":[""2020-08-03 04:46:00"",""2021-05-07 16:59:59""]}}," _
            '        & """include"":[""customer"",""shipping_address"",""sales_program"",""warehouse"",{""slc"":{""userProfiles"":[""district"",""city"",""province"",""communities""]}},""payment_method"",""invoice"",{""order_items"":{""product_set"":{""skus"":""product""}}}]}" _
            '        & "&access_token=NOL9vyqrF7pupPXLVlUpfGAEbQNHOHEkvWAmFmBzbSMtU4yRGiiZmRHxSbAVGRLr"


            result = get_data_from_api(_url, _err)
            If result Is Nothing Then
                If _err.Count > 0 Then
                    LblStatusGen.Text = _err.Item(0).ToString
                End If

                '_MakeReport("Data empty")
                Exit Sub
            End If

            Dim result2 As JArray = JArray.Parse(result.ToString) 'JsonConvert.DeserializeObject(Of List(Of ArrayList))(result.ToString)

            Dim _order_number As String = ""
            Dim _slc_id As String = ""
            Dim _slc_ptnr_id As String = ""
            Dim _slc_code As String = ""
            Dim _customer_id As String = ""
            Dim _customer_name As String = ""
            Dim _customer_email As String = ""
            Dim _customer_mobile As String = ""
            Dim _customer_zip_code As String = ""
            Dim _shipping_address_id As String = ""
            Dim _customer_shipping_address As String = ""
            Dim _created_at As Date
            Dim _warehouse_id As String = ""
            Dim _payment_method_id As String = ""
            Dim _sales_program_id As String = ""
            Dim _sales_program As String = ""
            Dim _order_id As String = ""
            Dim obj_product_set As String = ""
            Dim _discount As Double = 0.0

            Dim _ptnr_id As String = ""
            Dim _en_id As String = ""
            Dim _en_code As String = ""

            Dim dt_temp As New DataTable
            'Dim result_user_profile As JArray


            ssqls.Clear()
            For Each item As JObject In result2

                _en_id = ""
                _en_code = ""
                _customer_name = ""
                _customer_email = ""
                _customer_mobile = ""
                _customer_zip_code = ""
                _customer_shipping_address = ""
                _slc_code = ""
                _sales_program = ""
                _discount = 0.0

                _order_number = item.GetValue("order_number").ToString()
                _slc_id = item.GetValue("slc_id").ToString()
                _customer_id = item.GetValue("customer_id").ToString()
                _shipping_address_id = item.GetValue("shipping_address_id").ToString()
                _warehouse_id = item.GetValue("warehouse_id").ToString()
                _payment_method_id = item.GetValue("payment_method_id").ToString()
                _sales_program_id = item.GetValue("sales_program_id").ToString()
                _order_id = item.GetValue("id").ToString()


                ssql = "select sq_code from sq_mstr where sq_ajb_code=" & SetSetring(_order_number)

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    Continue For
                End If


                Try
                    'dt = JsonConvert.DeserializeObject<DateTime>(sa);
                    '_created_at = CDate("") 'JsonSerializer.Deserialize(Of DateTime)(item.GetValue("created_at").ToString()) 'JsonConvert.DeserializeObject(Of DateTime)(item.GetValue("created_at")) 'Date.ParseExact(item.GetValue("created_at").ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture)


                    Dim iString As String = item.GetValue("created_at").ToString()
                    Dim oDate As DateTime = DateTime.ParseExact(iString, "dd/MM/yyyy HH:mm:ss", Nothing)
                    _created_at = oDate
                Catch ex As Exception

                End Try

                Try
                    ssql = "select * from en_mstr where en_warehouse_id=" & SetSetring(_warehouse_id)

                    dt_temp = GetTableData(ssql)

                    If dt_temp.Rows.Count > 0 Then

                        _en_id = dt_temp.Rows(0).Item("en_id")
                        _en_code = dt_temp.Rows(0).Item("en_code")
                    Else
                        'Resume Next
                        Continue For
                    End If


                Catch ex As Exception

                End Try

                Try
                    Dim obj_sales_program As JObject = JObject.Parse(item.GetValue("sales_program").ToString())
                    _sales_program = obj_sales_program.GetValue("title").ToString()

                Catch ex As Exception

                End Try


                Dim obj_customer As String = ""
                Try
                    obj_customer = item.GetValue("customer").ToString()
                Catch ex As Exception

                End Try

                If obj_customer <> "" Then
                    Try
                        Dim _obj_customer As JObject = JObject.Parse(obj_customer)

                        _customer_name = _obj_customer.GetValue("full_name").ToString()
                        _customer_email = _obj_customer.GetValue("email").ToString()
                        _customer_mobile = _obj_customer.GetValue("mobile").ToString()

                        '
                        'Dim y As String = _obj_invoice.SelectToken("invoice_no").ToString()
                        Dim z As Integer = 0
                    Catch ex As Exception

                    End Try
                End If

                Try

                    Dim _obj_customer_shipping As JObject = JObject.Parse(item.GetValue("shipping_address").ToString())
                    _customer_shipping_address = _obj_customer_shipping.GetValue("address").ToString().Replace("\n", "")
                    _customer_zip_code = _obj_customer_shipping.GetValue("zip_code").ToString()
                Catch ex As Exception

                End Try


                Try
                    Dim _obj_slc As JObject = JObject.Parse(item.GetValue("slc").ToString())
                    Dim _obj_userProfiles As JObject = JObject.Parse(_obj_slc.GetValue("userProfiles").ToString())
                    _slc_code = _obj_userProfiles.GetValue("sales_code").ToString

                Catch ex As Exception

                End Try



                Dim obj_invoice As String = ""
                Try
                    obj_invoice = item.GetValue("invoice").ToString()
                Catch ex As Exception

                End Try

                If obj_invoice <> "" Then

                    Try
                        Dim _obj_invoice As JObject = JObject.Parse(obj_invoice)
                        'For Each pair As Object In _obj

                        'Next

                        'Dim x As String = _obj_invoice.SelectToken("invoice_no")

                        Dim x As String = _obj_invoice.GetValue("invoice_no").ToString()
                        Dim y As String = _obj_invoice.SelectToken("invoice_no").ToString()
                        Dim z As Integer = 0
                    Catch ex As Exception

                    End Try

                End If

                'Try
                '    _url = _url_api & "UserProfiles?filter={""where"":{""user_id"":" & _slc_id & "}}" _
                '            & "&access_token=NOL9vyqrF7pupPXLVlUpfGAEbQNHOHEkvWAmFmBzbSMtU4yRGiiZmRHxSbAVGRLr"


                '    result = get_data_from_api(_url, _err)
                '    If result Is Nothing Then
                '        If _err.Count > 0 Then
                '            LblStatusGen.Text = _err.Item(0).ToString
                '        End If

                '        '_MakeReport("Data empty")
                '        Exit Sub
                '    End If

                '    result_user_profile = JArray.Parse(result.ToString) 'JsonConvert.DeserializeObject(Of List(Of ArrayList))(result.ToString)

                '    For Each item_profile As JObject In result_user_profile
                '        _slc_code = item_profile.GetValue("sales_code").ToString()

                '    Next
                'Catch ex As Exception

                'End Try

                'cari sales person

                Try
                    ssql = "select * from ptnr_mstr where ptnr_sales_code=" & SetSetring(_slc_code)

                    dt_temp = GetTableData(ssql)

                    If dt_temp.Rows.Count > 0 Then
                        _slc_ptnr_id = dt_temp.Rows(0).Item("ptnr_id")
                        '_en_id = dt_temp.Rows(0).Item("en_id")
                        '_en_code = dt_temp.Rows(0).Item("en_code")
                    Else
                        'Resume Next
                        Continue For
                    End If

                Catch ex As Exception

                End Try

                Try
                    ssql = "select * from ptnr_mstr where ptnr_customer_id=" & SetSetring(_customer_id)
                    Dim dt_ptnr As New DataTable
                    dt_ptnr = GetTableData(ssql)

                    If dt_ptnr.Rows.Count = 0 Then
                        'insert ptnr

                        Dim _ptnr_oid As Guid
                        _ptnr_oid = Guid.NewGuid


                        'Dim _ptnr_id As Integer
                        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", _en_code, "ptnr_id", "ptnr_en_id", SetNumber(_en_id)))

                        _ptnr_id = SetInteger(GetID_Local(_en_code))

                        Dim _cu_id As Integer
                        _cu_id = GetRowInfo("select dom_base_cur_id from dom_mstr where dom_id=1")(0)


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

                        _ptnr_code = _ptnr_code + IIf(_en_code = 0, "99", _en_code) + _ptnr_id_s.ToString



                        ssql = "INSERT INTO  " _
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
                                & "  ptnr_transaction_code_id,ptnr_customer_id," _
                                & "  ptnr_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                & SetSetring(1) & ",  " _
                                & SetSetring(_en_id) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & " " & SetDateNTime(_created_at) & " " & ",  " _
                                & _ptnr_id & ",  " _
                                & SetSetring(_ptnr_code) & ",  " _
                                & SetSetring(_customer_name) & ",  " _
                                & 0 & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring(_customer_email) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("Import AJB Online") & ",  " _
                                & SetBitYN(True) & ",  " _
                                & SetBitYN(False) & ",  " _
                                & SetBitYN(False) & ",  " _
                                & SetBitYN(False) & ",  " _
                                & SetBitYN(False) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(0) & ",  " _
                                & SetInteger(_cu_id) & ",  " _
                                & SetDbl(0) & ",  " _
                                & SetBitYN(True) & ",  " _
                                & " null " & ",  " _
                                & SetSetring(_customer_id) & ",  " _
                                & "" & SetDateNTime(_created_at) & " " & "  " _
                                & ")"

                        ssqls.Add(ssql)


                        ssql = "INSERT INTO  " _
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
                                & SetInteger(func_coll.GetID("ptnra_addr", _en_code, "ptnra_id", "ptnra_en_id", _en_id)) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(_en_id) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "" & SetDateNTime(_created_at) & "" & ",  " _
                                & SetInteger(1) & ",  " _
                                & SetSetring(_customer_shipping_address) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring(_customer_mobile) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring(_customer_zip_code) & ",  " _
                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                & " (select code_id from code_mstr where code_field ~~* 'addr_type_mstr' and code_name ~~* 'bill to') " & ",  " _
                                & SetSetring("") & ",  " _
                                & SetBitYN(True) & ",  " _
                                & "" & SetDateNTime(_created_at) & "" & "  " _
                                & ")"

                        ssqls.Add(ssql)
                    Else
                        For Each dr_ptnr As DataRow In dt_ptnr.Rows
                            _ptnr_id = dr_ptnr("ptnr_id")

                        Next
                    End If


                Catch ex As Exception

                End Try


                'ssql = "SELECT  " _
                '    & "  sqd_oid, " _
                '    & "  sqd_dom_id, " _
                '    & "  sqd_en_id, " _
                '    & "  sqd_add_by, " _
                '    & "  sqd_add_date, " _
                '    & "  sqd_upd_by, " _
                '    & "  sqd_upd_date, " _
                '    & "  sqd_sq_oid, " _
                '    & "  sqd_seq, " _
                '    & "  sqd_is_additional_charge, " _
                '    & "  sqd_si_id, " _
                '    & "  sqd_pt_id, " _
                '    & "  sqd_rmks, " _
                '    & "  sqd_qty, " _
                '    & "  sqd_qty_allocated, " _
                '    & "  sqd_qty_picked, " _
                '    & "  sqd_qty_shipment, " _
                '    & "  sqd_qty_transfer_issue, " _
                '    & "  sqd_qty_transfer_receipt, " _
                '    & "  sqd_qty_pending_inv, " _
                '    & "  sqd_qty_invoice, " _
                '    & "  sqd_um, " _
                '    & "  sqd_cost, " _
                '    & "  sqd_price, " _
                '    & "  sqd_total_amount_price, " _
                '    & "  sqd_disc, " _
                '    & "  sqd_sales_ac_id, " _
                '    & "  sqd_sales_sb_id, " _
                '    & "  sqd_sales_cc_id, " _
                '    & "  sqd_disc_ac_id, " _
                '    & "  sqd_um_conv, " _
                '    & "  sqd_qty_real, " _
                '    & "  sqd_taxable, " _
                '    & "  sqd_tax_inc, " _
                '    & "  sqd_tax_class, " _
                '    & "  sqd_ppn_type, " _
                '    & "  sqd_status, " _
                '    & "  sqd_dt, " _
                '    & "  sqd_payment, " _
                '    & "  sqd_dp, " _
                '    & "  sqd_sales_unit, " _
                '    & "  sqd_loc_id, " _
                '    & "  sqd_serial, " _
                '    & "  en_desc, " _
                '    & "  si_desc, " _
                '    & "  pt_code, " _
                '    & "  pt_desc1, " _
                '    & "  pt_desc2, " _
                '    & "  pt_type, " _
                '    & "  pt_ls, " _
                '    & "  um_mstr.code_name as um_name, " _
                '    & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                '    & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                '    & "  sb_desc, " _
                '    & "  cc_desc, " _
                '    & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                '    & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                '    & "  tax_class.code_name as sqd_tax_class_name, " _
                '    & "  loc_desc, " _
                '    & "  sqd_pod_oid " _
                '    & "FROM  " _
                '    & "  public.sqd_det " _
                '    & "  inner join sq_mstr on sq_oid = sqd_sq_oid " _
                '    & "  inner join en_mstr on en_id = sqd_en_id " _
                '    & "  inner join si_mstr on si_id = sqd_si_id " _
                '    & "  inner join pt_mstr on pt_id = sqd_pt_id " _
                '    & "  inner join code_mstr um_mstr on um_mstr.code_id = sqd_um	 " _
                '    & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sqd_sales_ac_id " _
                '    & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sqd_sales_sb_id " _
                '    & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sqd_sales_cc_id " _
                '    & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sqd_sales_ac_id " _
                '    & "  inner join code_mstr tax_class on tax_class.code_id = sqd_tax_class	 " _
                '    & "  left outer join loc_mstr on loc_id = sqd_loc_id" _
                '    & " where sqd_det.sqd_seq = -99"

                'Dim dt_det As New DataTable
                'dt_det = GetTableData(ssql)




                Dim _sq_code, _sq_terbilang As String
                Dim _sq_total, _sq_total_ppn, _sq_total_pph, _sqd_qty, _sqd_price, _sqd_disc, _sq_total_temp, _tax_rate As Double

                _sq_code = func_coll.get_transaction_number("SQ", _en_code, "sq_mstr", "sq_code")
                '_kjb_code = func_coll.get_transaction_number("KJ", _en_code, "sq_mstr", "sq_code")




                Dim _sq_oid As String = Guid.NewGuid.ToString

                Dim array_order_items As String = ""

                Try
                    array_order_items = item.GetValue("order_items").ToString()
                Catch ex As Exception

                End Try

                Dim dt_pt As New DataTable
                Dim _ppn_type, _tax_inc, _tax_class, _pt_taxable As String
                Dim _price, _qty, _subtotal, _su As Double


                _sq_total = 0
                _sq_total_ppn = 0
                _sq_total_pph = 0
                _sq_total_temp = 0

                Dim ssqls_det As New ArrayList

                Dim i As Integer = 0

                If array_order_items <> "" Then
                    Dim jarray_order_items As JArray = JArray.Parse(array_order_items)

                    For Each item_order_items As JObject In jarray_order_items

                        _pt_taxable = ""
                        _tax_class = ""
                        _ppn_type = ""
                        _tax_inc = ""
                        _price = 0.0
                        _qty = 0.0
                        _subtotal = 0.0
                        _su = 0.0

                        _order_id = item_order_items.GetValue("order_id").ToString()
                        obj_product_set = item_order_items.GetValue("product_set").ToString()
                        _price = item_order_items.GetValue("price")
                        _qty = item_order_items.GetValue("quantity")
                        _subtotal = item_order_items.GetValue("subtotal")

                        Dim _obj_rule As JObject = JObject.Parse(item_order_items.GetValue("rule").ToString())

                        _su = _obj_rule.GetValue("sales_unit")
                        _discount = _obj_rule.GetValue("discount")

                        Dim _obj_product_set As JObject = JObject.Parse(obj_product_set)
                        Dim _title As String = _obj_product_set.GetValue("title").ToString()

                        Dim jarray_skus As JArray = JArray.Parse(_obj_product_set.GetValue("skus").ToString())

                        Dim obj_product As String = ""
                        Try
                            obj_product = jarray_skus.Item(0).SelectToken("product").ToString()
                        Catch ex As Exception

                        End Try


                        If obj_product <> "" Then
                            Dim _obj_product As JObject = JObject.Parse(obj_product)

                            Dim _abbr As String = _obj_product.GetValue("abbr").ToString()

                            Dim gg As Integer = 0


                            ssql = "select pt_id,pt_code,pt_desc1, pt_tax_class,pt_tax_inc,pt_taxable,pt_ppn_type, pt_si_id, pt_um,pt_loc_id from pt_mstr where pt_syslog_code=" & SetSetring(_abbr)
                            dt_pt = GetTableData(ssql)

                            If dt_pt.Rows.Count = 0 Then
                                '_status_berhasil = False
                                'Exit For

                                'Continue For
                                ssqls.Clear()
                                Exit For
                            End If

                            Dim _pt_id As String = ""
                            Dim _si_id As String = ""
                            Dim _pt_um As String = ""
                            Dim _pt_loc_id As String = ""

                            For Each dr_tax As DataRow In dt_pt.Rows
                                _pt_taxable = dr_tax("pt_taxable")
                                _tax_class = dr_tax("pt_tax_class")
                                _ppn_type = dr_tax("pt_ppn_type")
                                _tax_inc = dr_tax("pt_tax_inc")

                                _pt_id = dr_tax("pt_id")
                                _si_id = dr_tax("pt_si_id")
                                ' _tax_inc = dr_tax("pt_tax_inc")
                                _pt_um = dr_tax("pt_um")
                                _pt_loc_id = dr_tax("pt_loc_id")

                            Next

                            If _tax_inc.ToUpper = "Y" Then
                                _tax_rate = func_coll.get_ppn(_tax_class)
                                _sqd_price = _price - (_tax_rate * (_price / (1 + _tax_rate)))
                            Else
                                _sqd_price = _price
                            End If


                            _sqd_qty = _qty
                            _sqd_disc = _discount

                            If _ppn_type.ToUpper = "A" Then
                                'If _tax_inc.ToUpper = "Y" Then
                                '    _tax_rate = func_coll.get_ppn(_tax_class)
                                '    _sqd_price = dr_det("harga") - (_tax_rate * (dr_det("harga") / (1 + _tax_rate)))
                                'Else
                                '    _sqd_price = dr_det("harga")
                                'End If

                                '_sqd_qty = dr_det("qty")
                                '_sqd_disc = 0.0

                                _sq_total_temp = ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                                _sq_total_ppn = _sq_total_ppn + (_sq_total_temp * func_coll.get_ppn(_tax_class))
                                _sq_total_pph = _sq_total_pph + (_sq_total_temp * func_coll.get_pph(_tax_class))
                            End If


                            Dim _cost As Double
                            _cost = SetNumber(GetRowInfo("select invct_cost from invct_table where invct_pt_id=" & _pt_id)(0))


                            ssql = "INSERT INTO  " _
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
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(_en_id) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "" & SetDateNTime(_created_at) & "" & ",  " _
                                    & SetSetring(_sq_oid.ToString) & ",  " _
                                    & SetInteger(i) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & SetInteger(_si_id) & ",  " _
                                    & SetInteger(_pt_id) & ",  " _
                                    & SetSetringDB("") & ",  " _
                                    & SetDbl(_qty) & ",  " _
                                    & SetInteger(_pt_um) & ",  " _
                                    & SetDbl(_cost) & ",  " _
                                    & SetDbl(_price) & ",  " _
                                    & SetDbl(_sqd_disc) & ",  " _
                                    & "null" & ",  " _
                                    & "null" & ",  " _
                                    & "null" & ",  " _
                                    & SetInteger(1) & ",  " _
                                    & SetDbl(_qty) & ",  " _
                                    & SetSetring(_pt_taxable) & ",  " _
                                    & SetSetring(_tax_inc) & ",  " _
                                    & SetInteger(_tax_class) & ",  " _
                                    & SetSetring(_ppn_type) & ",  " _
                                    & "" & SetDateNTime(_created_at) & "" & ",  " _
                                    & SetDblDB(_qty * _price) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetInteger(_pt_loc_id) & ",  " _
                                    & SetDblDB(_su) & ",  " _
                                    & SetSetring("") & "  " _
                                    & ")"

                            ssqls_det.Add(ssql)

                            _sq_total = _sq_total + ((_sqd_qty * _sqd_price) - (_sqd_qty * _sqd_price * _sqd_disc))
                        End If

                        i = i + 1
                    Next
                End If

                _sq_terbilang = func_bill.TERBILANG_FIX(_sq_total + _sq_total_ppn - _sq_total_pph)

                Dim _total_dp, _total_payment As Double
                _total_dp = 0
                _total_payment = 0


                Dim _pi_id As String = ""

                ssql = "select pi_id from pi_mstr where pi_code='-'"
                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _pi_id = dt_temp.Rows(0).Item("pi_id").ToString
                End If


                Dim _sls_code As String = ""

                ssql = "select sls_code from sls_program where sls_desc='" & _sales_program & "'"

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _sls_code = dt_temp.Rows(0).Item("sls_code").ToString
                End If


                Dim _payment_methode As String = ""
                ssql = "select code_id from code_mstr where code_name='Transfer Via Bank' and code_field='payment_methode'"

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _payment_methode = dt_temp.Rows(0).Item("code_id").ToString
                End If

                Dim _payment_type As String = ""
                Dim payment_type As String = ""

                Try
                    Dim obj_payment_type As JObject = JObject.Parse(item.GetValue("payment_method").ToString())

                    payment_type = obj_payment_type.GetValue("title").ToString()
                Catch ex As Exception

                End Try

                ssql = "select code_id from code_mstr where code_usr_2='" & payment_type & "' and code_field='payment_type'"

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _payment_type = dt_temp.Rows(0).Item("code_id").ToString
                End If

                Dim _bk_id As String = ""
                ssql = "select bk_id from bk_mstr where bk_name='IPAYMU' and bk_en_id =" & _en_id & ""

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _bk_id = dt_temp.Rows(0).Item("bk_id").ToString
                End If


                Dim _si_id_new As String = ""
                ssql = "     "

                dt_temp = GetTableData(ssql)

                If dt_temp.Rows.Count > 0 Then
                    _si_id_new = dt_temp.Rows(0).Item("si_id").ToString
                End If


                ssql = "INSERT INTO  " _
                        & "  public.sq_mstr " _
                        & "( " _
                        & "  sq_oid, " _
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
                        & "  sq_si_id," _
                        & "  sq_type, " _
                        & "  sq_sales_person, sq_bk_id, " _
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
                        & "  sq_need_date,sq_due_date, " _
                        & "  sq_tran_id, " _
                        & "  sq_trans_id, " _
                        & "  sq_trans_rmks, " _
                        & "  sq_dt, " _
                        & "  sq_cu_id, " _
                        & "  sq_total_ppn, " _
                        & "  sq_total_pph, " _
                        & "  sq_payment, " _
                        & "  sq_exc_rate, " _
                        & "  sq_interval,sq_sales_program,sq_status_produk,sq_diskon_produk,sq_unique_code, sq_dp_unique,sq_payment_unique,sq_ajb_code, " _
                        & "  sq_terbilang " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(_sq_oid.ToString) & ",  " _
                        & SetInteger(1) & ",  " _
                        & SetInteger(_en_id) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & "" & SetDateNTime(_created_at) & "" & ",  " _
                        & SetSetring(_sq_code) & ",  " _
                        & SetInteger(_ptnr_id) & ",  " _
                        & SetInteger(_ptnr_id) & ",  " _
                        & SetSetring("") & ",  " _
                        & SetSetring("") & ",  " _
                        & SetDate(_created_at) & ",  " _
                        & SetInteger(_si_id_new) & ",  " _
                        & SetSetring("D") & ",  " _
                        & SetInteger(_slc_ptnr_id) & ",  " _
                        & SetInteger(_bk_id) & ",  " _
                        & SetInteger(_pi_id) & ",  " _
                        & SetInteger(_payment_type) & ",  " _
                        & SetInteger(_payment_methode) & ",  " _
                        & SetSetring("N") & ",  " _
                        & SetInteger("0") & ",  " _
                        & SetInteger("0") & ",  " _
                        & SetInteger("0") & ",  " _
                        & SetDbl(_total_dp) & ",  " _
                        & SetDbl(0) & ",  " _
                        & SetDbl(_sq_total) & ",  " _
                        & SetDate(CekTanggal) & ",  " _
                         & SetDate(CekTanggal) & ",  " _
                        & SetInteger("") & ",  " _
                        & SetSetring("I") & ",  " _
                        & SetSetring("AJB Online") & ",  " _
                        & "" & SetDateNTime(_created_at) & "" & ",  " _
                        & SetInteger("1") & ",  " _
                        & SetDbl(_sq_total_ppn) & ",  " _
                        & SetDbl(_sq_total_pph) & ",  " _
                        & SetDbl(_total_payment) & ",  " _
                        & SetDbl("1") & ",  " _
                        & SetInteger(1) & ",  " _
                        & SetSetring(_sls_code) & ",  " _
                        & SetSetring("") & ",  " _
                        & SetSetring(0) & ",  " _
                        & SetInteger("") & ",  " _
                        & SetDbl(0) & ",  " _
                        & SetDbl(0) & ",  " _
                        & SetSetring(_order_number) & " , " _
                        & SetSetring(_sq_terbilang) & "  " _
                        & ")"

                ssqls.Add(ssql)

                For Each it As String In ssqls_det
                    ssqls.Add(it)
                Next

                If ssqls_det.Count = 0 Then
                    ssqls.Clear()
                    Continue For
                End If
                'handler:
                '                'MessageBox.Show("Caught exception " + Err.GetException().Message)
                '                Continue For


                DbRunTran(ssqls)
                ssqls.Clear()
                Box("Success")
            Next


            'JsonConvert.DeserializeObject(Of List(Of userdetails))(strmsg)
            'Dim jsonreader = New JsonTextReader(New StringReader(result))
            'Dim serializer = New JsonSerializer()
            'Dim document As JObject = CType(serializer.Deserialize(jsonreader), JObject)
            'For Each result In result2
            '    LblStatusGen.Text = result.Key
            '    For Each channelgroup In result.Value
            '        LblStatusGen.Text = channelgroup.Item("order_number").ToString
            '        'For Each channel In channelgroup.Item("invoice")

            '        'Next
            '    Next
            'Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetJSON(ByVal JsonData As String) As HttpWebRequest
        Dim objhttpWebRequest As HttpWebRequest
        Try
            Dim httpWebRequest = DirectCast(WebRequest.Create("https://testapi.sdi.id/api/orders?"), HttpWebRequest)
            httpWebRequest.ContentType = "application/x-www-form-urlencoded"
            httpWebRequest.Method = "GET"
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows CE)"
            httpWebRequest.Accept = "*/*"
            httpWebRequest.Headers.Add("Accept-Language", "en-us\r\n")
            httpWebRequest.Headers.Add("UA-CPU", "x86 \r\n")
            httpWebRequest.Headers.Add("Cache-Control", "no-cache\r\n")
            httpWebRequest.KeepAlive = True

            Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())


                streamWriter.Write(JsonData)
                streamWriter.Flush()
                streamWriter.Close()
            End Using

           

            objhttpWebRequest = httpWebRequest

        Catch ex As Exception
            Console.WriteLine("Send Request Error[{0}]", ex.Message)

            Return Nothing
        End Try

        Return objhttpWebRequest

    End Function
    Private Function PostJSON(ByVal JsonData As String) As HttpWebRequest
        Dim objhttpWebRequest As HttpWebRequest
        Try
            Dim httpWebRequest = DirectCast(WebRequest.Create("http://geojsonlint.com/"), HttpWebRequest)
            httpWebRequest.ContentType = "text/json"
            httpWebRequest.Method = "POST"

            Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())


                streamWriter.Write(JsonData)
                streamWriter.Flush()
                streamWriter.Close()
            End Using

            objhttpWebRequest = httpWebRequest

        Catch ex As Exception
            Console.WriteLine("Send Request Error[{0}]", ex.Message)

            Return Nothing
        End Try

        Return objhttpWebRequest

    End Function

    Private Function GetResponse(ByVal httpWebRequest As HttpWebRequest) As String
        Dim strResponse As String = "Bad Request:400"
        Try
            Dim httpResponse = DirectCast(httpWebRequest.GetResponse(), HttpWebResponse)
            Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                Dim result = streamReader.ReadToEnd()

                strResponse = result.ToString()
            End Using
        Catch ex As Exception
            Console.WriteLine("GetResponse Error[{0}]", ex.Message)

            Return ex.Message
        End Try

        Return strResponse

    End Function

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimpleButton1.Click
        'Try
        '    'Dim JsonData As String = "{""filter"":{""where"":{""status"":1,""created_at"":{""between"":[""2021-01-29 15:00:00"",""2021-04-07 16:59:59""]}""}}}"

        '    'Dim myRequest As HttpWebRequest = GetJSON(JsonData)
        '    'GetResponse(myRequest)

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Try
            'Dim fr As System.Net.HttpWebRequest
            'Dim targetURI As New Uri("https://testapi.sdi.id/api/orders?filter={%22where%22:{%22status%22:1,%22created_at%22:{%22between%22:[%222020-08-03%2004:46:00%22,%222021-05-07%2016:59:59%22]}},%22include%22:[%22customer%22,%22shipping_address%22,%22sales_program%22,%22warehouse%22,{%22slc%22:{%22userProfiles%22:[%22district%22,%22city%22,%22province%22,%22communities%22]}},%22payment_method%22,%22invoice%22,{%22order_items%22:{%22product_set%22:{%22skus%22:%22product%22}}}]}&access_token=NOL9vyqrF7pupPXLVlUpfGAEbQNHOHEkvWAmFmBzbSMtU4yRGiiZmRHxSbAVGRLr")

            'fr = DirectCast(HttpWebRequest.Create(targetURI), System.Net.HttpWebRequest)

            'Dim responseFromServer As String = ""
            'If (fr.GetResponse().ContentLength > 0) Then
            '    Dim str As New System.IO.StreamReader(fr.GetResponse().GetResponseStream())
            '    responseFromServer = str.ReadToEnd()
            '    str.Close()
            'End If
            'Dim data As String = responseFromServer


            '            Dim url As String = "https://jsonplaceholder.typicode.com/users"

            '            Dim options As New Utils.WebRequest.Options



            '            Dim options = New Utils.WebRequest.Options With {
            '    .UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0",
            '    .ContentType = Utils.WebRequest.ContentType.ApplicationJson,
            '    .Headers = New System.Net.WebHeaderCollection From {
            '        {"key", "value"}
            '    }
            '}
            Dim myReq As HttpWebRequest
            Dim myResp As HttpWebResponse

            myReq = HttpWebRequest.Create("https://testapi.sdi.id/api/orders?" & "filter"":{""where"":{""status"":1,""created_at"":{""between"":[""2021-01-29 15:00:00"",""2021-04-07 16:59:59""]}""}}")
            myReq.Method = "GET"
            myReq.ContentType = "application/json"
            myReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0"
            'myReq.Headers.Add("Authorization", "Basic " & Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("myusername:mypassword")))

            Dim myData As String = "{""filter"":{""where"":{""status"":1,""created_at"":{""between"":[""2021-01-29 15:00:00"",""2021-04-07 16:59:59""]}""}}}"

            ' myReq.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(myData), 0, System.Text.Encoding.UTF8.GetBytes(myData).Length)
            myResp = myReq.GetResponse

            Dim myreader As New System.IO.StreamReader(myResp.GetResponseStream)
            Dim myText As String

            myText = myreader.ReadToEnd

        Catch ex As System.Net.WebException
            'Error in accessing the resource, handle it
        End Try
    End Sub

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
End Class
