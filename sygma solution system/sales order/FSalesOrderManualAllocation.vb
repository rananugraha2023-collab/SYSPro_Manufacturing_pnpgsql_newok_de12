Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FSalesOrderManualAllocation
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit, ds_edit_shipment, ds_edit_dist, ds_check As New DataSet
    Dim _now As DateTime
    Dim _so_oid_mstr, _soa_oid_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Public _so_ptnr_id_sold_mstr As Integer
    Public _so_ref_po_oid As String
    Dim _conf_value As String
    Dim mf As New master_new.ModFunction
    Dim _sod_oid_first(300) As String
    Dim _sod_invc_oid_first(300) As String
    Dim _sod_qty_allocated_first(300) As Integer
    Dim long_array As Integer

    Private Sub FSalesOrderManualAllocation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_order")

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _so_ptnr_id_sold_mstr = -1 'set diawal agar tau kalau -1 artinya datanya belum ada...



        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        so_en_id.Properties.DataSource = dt_bantu
        so_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        so_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        so_en_id.ItemIndex = 0

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

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        so_ar_ac_id.Properties.DataSource = dt_bantu
        so_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        so_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        so_ar_ac_id.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales(so_en_id.EditValue))
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
        so_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_code").ToString
        so_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        so_bk_id.ItemIndex = 0
    End Sub

    Private Sub so_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub so_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles so_type.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
        so_pi_id.Properties.DataSource = dt_bantu
        so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        so_pi_id.ItemIndex = 0
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
        dt_bantu = (func_data.load_pi_mstr(so_en_id.EditValue, so_type.EditValue, so_cu_id.EditValue, so_date.DateTime))
        so_pi_id.Properties.DataSource = dt_bantu
        so_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        so_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        so_pi_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_bk_mstr(so_en_id.EditValue, so_cu_id.EditValue))
        so_bk_id.Properties.DataSource = dt_bantu
        so_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_code").ToString
        so_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        so_bk_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SO Type", "so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi Po No.", "so_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Taxable", "so_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Include", "so_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_master, "Payment", "so_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sales Unit", "so_sales_unit", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        ' add_column_copy(gv_master, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_master, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_master, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_master, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_master, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

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
        'add_column_copy(gv_detail, "Qty Pending Invoice", "sod_qty_pending_inv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Invoice  ", "sod_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        'add_column_copy(gv_detail, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "UM Conversion", "sod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "sod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Allocated", "sod_qty_allocated", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Allocated at", "loc_desc_invc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "sod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN Type", "sod_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Serial", "sod_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)

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
        add_column(gv_edit, "Remarks", "sod_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_qty_shipment", False)
        add_column(gv_edit, "sod_um", False)
        add_column(gv_edit, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "sod_sales_ac_id", False)
        'add_column(gv_edit, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_sales_sb_id", False)
        'add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "sod_sales_cc_id", False)
        'add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_disc_ac_id", False)
        'add_column(gv_edit, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "UM Conversion", "sod_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "sod_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_invc_oid", False)
        add_column(gv_edit, "sod_invc_loc_id", False)
        add_column(gv_edit, "Allocated at", "loc_desc_invc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty Allocated", "sod_qty_allocated", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sod_tax_class", False)
        add_column(gv_edit, "Tax Class", "sod_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "PPN Type", "sod_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column(gv_edit, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "sod_pod_oid", False)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
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
                    & "  ptnr_mstr_sales.ptnr_name as ptnr_name_sales, " _
                    & "  pi_desc, " _
                    & "  pay_type.code_name as pay_type_name, coalesce(pay_type.code_usr_1,'-1') as pay_interval, " _
                    & "  pay_method.code_name as pay_method_name, " _
                    & "  ac_mstr_ar.ac_code, " _
                    & "  ac_mstr_ar.ac_name, " _
                    & "  sb_mstr_ar.sb_desc, " _
                    & "  cc_mstr_ar.cc_desc, " _
                    & "  cu_name, " _
                    & "  tran_name, " _
                    & "  so_bk_id, bk_code, bk_name, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM  " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = so_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = so_ptnr_id_bill " _
                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
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
                    & " where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and so_en_id in (select user_en_id from tconfuserentity " _
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
            & "  sod_status, " _
            & "  sod_dt, " _
            & "  sod_payment, " _
            & "  sod_dp, " _
            & "  sod_sales_unit, " _
            & "  sod_loc_id, " _
            & "  sod_serial, " _
            & "  sod_invc_oid, " _
            & "  sod_invc_loc_id, " _
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
            & "  tax_class.code_name as sod_tax_class_name, " _
            & "  sod_ppn_type, " _
            & "  loc_mstr_invc.loc_desc as loc_desc_invc, " _
            & "  loc_mstr.loc_desc " _
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
            & "  left outer join loc_mstr on loc_mstr.loc_id = sod_loc_id" _
            & "  left outer join invc_mstr on sod_invc_oid = invc_oid" _
            & "  left outer join loc_mstr loc_mstr_invc on invc_loc_id = loc_mstr_invc.loc_id" _
            & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and so_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

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
            & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & " and so_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        load_data_detail(sql, gc_detail_attr, "detail_attr")
      
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("sod_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sod_so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_attr.Columns("soa_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soa_so_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
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
        
    End Sub

    Public Overrides Function insert_data() As Boolean
        MsgBox("Cannot Insert Data, Use Edit Button to Allocating a unit..", MsgBoxStyle.Critical, "Insert Disabled")

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        Dim _qty_alloc, _qty_open, _qty_alloc_ship As Integer

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("sod_invc_oid")) = False Then
                ds_check = New DataSet
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                                & "  coalesce(public.invc_mstr.invc_qty,0) - coalesce(public.invc_mstr.invc_qty_alloc,0) as invc_qty_open " _
                               & "FROM  " _
                                & "  public.invc_mstr " _
                                & " where invc_oid = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_invc_oid"))

                            .InitializeCommand()
                            .FillDataSet(ds_check, "to_invc_check")
                            
                        End With
                    End Using
                    _qty_alloc = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("sod_qty_allocated"))
                    _qty_open = SetIntegerDB(ds_check.Tables("to_invc_check").Rows(0).Item("invc_qty_open"))

                    If _qty_alloc > _qty_open Then
                        MsgBox(ds_edit.Tables(0).Rows(i).Item("pt_desc1") + " Qty Which Allocated are More Than Availability on " + ds_edit.Tables(0).Rows(i).Item("loc_desc_invc") + ".......", MsgBoxStyle.Critical, "Unable to Save..")
                        before_save = False
                    End If

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Next

        Dim j As Integer
        For j = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(j).Item("sod_invc_oid")) = False Then
                Dim ds_allocated_ship As New DataSet
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = "SELECT  " _
                                & "  sum(soshipd_qty_allocated) as tot_alloc_ship " _
                                & "FROM  " _
                                & "  public.soshipd_det " _
                                & "  where soshipd_sod_oid = " + SetSetring(ds_edit.Tables(0).Rows(j).Item("sod_oid"))

                            .InitializeCommand()
                            .FillDataSet(ds_allocated_ship, "allocated_ship")
                        End With
                    End Using

                    If IsDBNull(ds_allocated_ship.Tables(0).Rows(j).Item("tot_alloc_ship")) Then
                    Else
                        _qty_alloc = SetIntegerDB(ds_edit.Tables(0).Rows(j).Item("sod_qty_allocated"))
                        _qty_alloc_ship = SetIntegerDB(ds_allocated_ship.Tables("allocated_ship").Rows(0).Item("tot_alloc_ship"))

                        If _qty_alloc < _qty_alloc_ship Then
                            MsgBox(ds_edit.Tables(0).Rows(j).Item("pt_desc1") + " Qty Which Allocated Are Shipped " + _qty_alloc_ship.ToString + ".......", MsgBoxStyle.Critical, "Unable to Save..")
                            before_save = False
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Next

        Return before_save
    End Function

#Region "GridView"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        
        If e.Column.Name = "sod_qty_allocated" Then
            If e.Value > gv_edit.GetRowCellValue(e.RowHandle, "sod_qty") Then
                MessageBox.Show("Qty Allocated Can't More Than Sales Order Qty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.SetRowCellValue(e.RowHandle, "sod_qty_allocated", gv_edit.GetRowCellValue(e.RowHandle, "sod_qty"))
                Exit Sub
            End If

            If IsDBNull(gv_edit.GetRowCellValue(e.RowHandle, "sod_invc_oid")) Then
                MessageBox.Show("Location Unknown..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If

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
        Dim _sod_pt_id As Integer = gv_edit.GetRowCellValue(_row, "sod_pt_id")
        Dim _sod_qty As Integer = gv_edit.GetRowCellValue(_row, "sod_qty")

        If _col = "loc_desc_invc" Then
            Dim frm As New FInventorySearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _sod_en_id
            frm._si_id = _sod_si_id
            frm._pt_id = _sod_pt_id
            frm._qty = _sod_qty
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

#End Region

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

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
        
        Return insert
    End Function


    Public Overrides Function edit_data() As Boolean

        Dim x As Integer

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _so_oid_mstr = .Item("so_oid").ToString
                so_ar_ac_id.EditValue = .Item("so_ar_ac_id")
                so_ar_cc_id.EditValue = .Item("so_ar_cc_id")
                so_credit_term.EditValue = .Item("so_credit_term")
                so_cu_id.EditValue = .Item("so_cu_id")

                If IsDBNull(.Item("so_bk_id")) = True Then
                    so_bk_id.ItemIndex = 0
                Else
                    so_bk_id.EditValue = .Item("so_bk_id")
                End If

                so_en_id.EditValue = .Item("so_en_id")
                so_date.DateTime = .Item("so_date")
                so_exc_rate.EditValue = .Item("so_exc_rate")
                so_pay_method.EditValue = .Item("so_pay_method")
                so_cons.EditValue = SetBitYNB(.Item("so_cons"))
                so_pay_type.EditValue = .Item("so_pay_type")
                so_payment_date.DateTime = .Item("so_payment_date")
                so_pi_id.EditValue = .Item("so_pi_id")
                _so_ptnr_id_sold_mstr = .Item("so_ptnr_id_sold")
                so_ptnr_id_sold.Text = .Item("ptnr_name_sold")
                so_ref_po_code.Text = SetString(.Item("so_ref_po_code"))
                _so_ref_po_oid = SetString(.Item("so_ref_po_oid"))
                If _so_ref_po_oid <> "" Then
                    so_ref_po_code.Enabled = False
                Else
                    so_ref_po_code.Enabled = True
                End If
                so_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
                so_sales_person.EditValue = .Item("so_sales_person")
                so_si_id.EditValue = .Item("so_si_id")
                so_tax_class.EditValue = .Item("so_tax_class")
                so_tax_inc.EditValue = SetBitYNB(.Item("so_tax_inc"))
                so_taxable.EditValue = SetBitYNB(.Item("so_taxable"))
                so_tran_id.EditValue = .Item("so_tran_id")
                so_trans_rmks.Text = SetString(.Item("so_trans_rmks"))
                so_type.EditValue = .Item("so_type")
            End With
            so_en_id.Focus()
            so_cu_id.Enabled = False
            'so_ptnr_id_sold.Enabled = False
            'so_bantu_address.Enabled = False
            so_pay_type.Enabled = False
            so_type.Enabled = False
            so_pi_id.Enabled = False

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
                            & "  coalesce(sod_qty_allocated,0) as sod_qty_allocated, " _
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
                            & "  sod_invc_oid, " _
                            & "  sod_invc_loc_id, " _
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
                            & "  loc_mstr_invc.loc_desc as loc_desc_invc, " _
                            & "  loc_mstr.loc_desc, sod_pod_oid " _
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
                            & "  left outer join loc_mstr on loc_mstr.loc_id = sod_loc_id" _
                            & "  left outer join invc_mstr on sod_invc_oid = invc_oid" _
                            & "  left outer join loc_mstr loc_mstr_invc on invc_loc_id = loc_mstr_invc.loc_id" _
                            & " where public.sod_det.sod_so_oid = '" + ds.Tables(0).Rows(row).Item("so_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")

                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            long_array = 0
            _sod_oid_first.Clear(_sod_oid_first, 0, _sod_oid_first.Length - 1)
            _sod_invc_oid_first.Clear(_sod_invc_oid_first, 0, _sod_invc_oid_first.Length - 1)
            _sod_qty_allocated_first.Clear(_sod_qty_allocated_first, 0, _sod_qty_allocated_first.Length - 1)

            For x = 0 To ds_edit.Tables("detail").Rows.Count - 1
                If IsDBNull(ds_edit.Tables("detail").Rows(x).Item("sod_qty_allocated")) = False Then
                    If ds_edit.Tables("detail").Rows(x).Item("sod_qty_allocated") > 0 Then
                        _sod_oid_first(x) = ds_edit.Tables("detail").Rows(x).Item("sod_oid").ToString
                        _sod_invc_oid_first(x) = SetSetringDB(ds_edit.Tables("detail").Rows(x).Item("sod_invc_oid"))
                        _sod_qty_allocated_first(x) = SetIntegerDB(ds_edit.Tables("detail").Rows(x).Item("sod_qty_allocated"))
                        long_array = long_array + 1
                    End If
                End If
            Next

            edit_data = True

        End If

    End Function

    Public Overrides Function edit()
        edit = True
        'Dim _so_total, _sod_qty, _sod_price, _sod_disc, _so_total_ppn, _so_total_pph, _so_total_temp, _tax_rate As Double
        Dim _sod_qty_shipment As Double = 0
        Dim i, z As Integer
        Dim _sod_qty_allocated As Double = 0


        'Dim _so_trn_status As String
        Dim ds_bantu As New DataSet

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("sod_qty_allocated") > 0 Then
                                _sod_qty_allocated = 0

                                For z = 0 To long_array - 1
                                    If _sod_oid_first(z).ToString = ds_edit.Tables(0).Rows(i).Item("sod_oid").ToString Then
                                        _sod_qty_allocated = _sod_qty_allocated_first(z)
                                    End If
                                Next


                                If _sod_qty_allocated > 0 Then

                                    For z = 0 To long_array - 1
                                        If _sod_oid_first(z).ToString = ds_edit.Tables(0).Rows(i).Item("sod_oid").ToString Then
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = "UPDATE  " _
                                                                & "  public.invc_mstr   " _
                                                                & "SET  " _
                                                                & "  invc_qty_alloc = invc_qty_alloc - " & SetIntegerDB(_sod_qty_allocated_first(z)) & "  " _
                                                                & "  " _
                                                                & "WHERE  " _
                                                                & "  invc_oid = " & _sod_invc_oid_first(z) & " "

                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        End If
                                    Next
                                End If

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.sod_det   " _
                                                    & "SET  " _
                                                    & "  sod_invc_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_invc_oid")) & ",  " _
                                                    & "  sod_invc_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_invc_loc_id")) & ",  " _
                                                    & "  sod_qty_allocated = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("sod_qty_allocated")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  sod_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("sod_oid")) & " "

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.invc_mstr   " _
                                                    & "SET  " _
                                                    & "  invc_qty_alloc = coalesce(invc_qty_alloc,0) + " & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("sod_qty_allocated")) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  invc_oid = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("sod_invc_oid")) & " "

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_so_oid_mstr, "so_oid")
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
        before_delete = False

        MsgBox("Data Can't Be Deleted...", MsgBoxStyle.Critical, "Delete Disabled")
    End Function

    Public Overrides Function delete_data() As Boolean
        MsgBox("Data Can't Be Deleted...", MsgBoxStyle.Critical, "Delete Disabled")
    End Function

    
End Class
