Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraExport

Public Class FSalesQuotationVerification
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit, ds_edit_shipment, ds_edit_transfer, ds_edit_dist As New DataSet
    Dim ds_ptnr As New DataSet
    Dim _now As DateTime
    Dim _sv_oid_mstr, _sqa_oid_mstr As String
    Dim ssqls As New ArrayList
    Dim func_bill As New Cls_Bilangan
    Public _sv_ptnr_id_sold_mstr As Integer
    Public _sv_ref_po_oid As String
    Public _oid_ptnr As String
    Dim _conf_value As String
    Dim _verification_min_point As Double
    Dim _total_point As Double = 0
    Dim _point_status_rumah As Double
    Dim _point_income As Double
    Dim _point_lama_kerja As Double
    Dim _point_lama_tinggal As Double
    Dim _point_jaminan As Double
    Dim _point_kepribadian As Double
    Dim _point_tanggungan As Double
    Public _sq_oid As String
    Dim mf As New master_new.ModFunction
    Dim ds_attr As New DataSet
    Dim _sv_status As String = "A"


    Private Sub FSalesQuotation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_sales_verification")
        _verification_min_point = func_coll.get_conf_file("Verification_min_point")

        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _sv_ptnr_id_sold_mstr = -1 'set diawal agar tau kalau -1 artinya datanya belum ada...

        If _conf_value = "0" Then
            xtc_detail.TabPages(1).PageVisible = False
            xtc_detail.TabPages(2).PageVisible = False
            xtc_detail.TabPages(4).PageVisible = False
            xtc_detail.TabPages(5).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(1).PageVisible = False
            xtc_detail.TabPages(2).PageVisible = False
            xtc_detail.TabPages(3).PageVisible = True
            xtc_detail.TabPages(4).PageVisible = True
            'xtc_detail.TabPages(5).PageVisible = True
        End If

        xtc_detail.SelectedTabPageIndex = 0
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        gc_edit.EmbeddedNavigator.Buttons.Append.Enabled = False
        gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
    End Sub

    Public Overrides Sub load_cb()
        init_le(sv_en_id, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sq_type())
        sv_type.Properties.DataSource = dt_bantu
        sv_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        sv_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        sv_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        sv_cu_id.Properties.DataSource = dt_bantu
        sv_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        sv_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        sv_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        sv_ar_ac_id.Properties.DataSource = dt_bantu
        sv_ar_ac_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        sv_ar_ac_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        sv_ar_ac_id.ItemIndex = 0


        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            sv_tran_id.Properties.DataSource = dt_bantu
            sv_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            sv_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            sv_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            sv_tran_id.Properties.DataSource = dt_bantu
            sv_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            sv_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            sv_tran_id.ItemIndex = 0
        End If


        init_le(le_en_id, "en_mstr")

    End Sub

    Public Overrides Sub load_cb_en()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sales(sv_en_id.EditValue))
        sv_sales_person.Properties.DataSource = dt_bantu
        sv_sales_person.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        sv_sales_person.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        sv_sales_person.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(sv_en_id.EditValue))
        sv_si_id.Properties.DataSource = dt_bantu
        sv_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        sv_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        sv_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pay_type(sv_en_id.EditValue))
        sv_pay_type.Properties.DataSource = dt_bantu
        sv_pay_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sv_pay_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sv_pay_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sv_en_id.EditValue, "payment_methode"))
        sv_pay_method.Properties.DataSource = dt_bantu
        sv_pay_method.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sv_pay_method.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sv_pay_method.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_status_rumah())
        sqa_status_rumah_id.Properties.DataSource = dt_bantu
        sqa_status_rumah_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_status_rumah_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_status_rumah_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pi_mstr(sv_en_id.EditValue, sv_type.EditValue, sv_cu_id.EditValue, sv_date.DateTime))
        sv_pi_id.Properties.DataSource = dt_bantu
        sv_pi_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
        sv_pi_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
        sv_pi_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sv_en_id.EditValue))
        sv_ar_sb_id.Properties.DataSource = dt_bantu
        sv_ar_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        sv_ar_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        sv_ar_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sv_en_id.EditValue))
        sv_ar_cc_id.Properties.DataSource = dt_bantu
        sv_ar_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        sv_ar_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        sv_ar_cc_id.ItemIndex = 0

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

        dt_bantu = New DataTable
        dt_bantu = (func_data.kepribadian())
        sqa_kepribadian_id.Properties.DataSource = dt_bantu
        sqa_kepribadian_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        sqa_kepribadian_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqa_kepribadian_id.ItemIndex = 0

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

    Private Sub sv_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sv_en_id.EditValueChanged
        load_cb_en()
    End Sub


    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "sv_oid", False)
        add_column_copy(gv_master, "SV Status", "sv_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "SV Number", "sv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "sv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "SV Type", "sv_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Referensi SQ No.", "sv_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "sv_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Consignment", "sv_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Prepayment", "sv_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Discount", "sv_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_master, "Payment", "sv_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Payment Date", "sv_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Close Date", "sv_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Approval Type", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "sv_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "sv_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sales Unit", "sv_sales_unit", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Total", "sv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "sv_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPH", "sv_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "After Tax", "sv_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Ext. Total", "sv_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPN", "sv_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. PPH", "sv_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Ext. After Tax", "sv_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_master, "User Create", "sv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "svd_sv_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Additional Charge", "svd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "svd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "svd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Process", "svd_qty_transfer_issue", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Complete", "svd_qty_transfer_receipt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Reject", "svd_qty_transfer_return", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Invoice  ", "svd_qty_invoice", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "svd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Price", "svd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "svd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_detail, "UM Conversion", "svd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "svd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Taxable", "svd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Include", "svd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tax Class", "svd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN Type", "svd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Prepayment", "svd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment", "svd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Unit", "svd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "svd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_attr, "sqa_sv_oid", False)
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
        add_column_copy(gv_detail_attr, "Kepribadian", "kepribadian", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Tanggungan", "tanggungan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_attr, "Jaminan", "jaminan", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_kp, "sokp_sv_oid", False)
        add_column_copy(gv_detail_kp, "Amount", "sokp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Due Date", "sokp_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Amount Pay", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Date Payment", "sokp_date_payment", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_kp, "Description", "sokp_description", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "svd_oid", False)
        add_column(gv_edit, "svd_sv_oid", False)
        add_column(gv_edit, "svd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Additional Charge", "svd_is_additional_charge", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pt_type", False)
        add_column(gv_edit, "pt_ls", False)
        add_column_edit(gv_edit, "Remarks", "svd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "svd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "svd_qty_transfer", False)
        add_column(gv_edit, "svd_um", False)
        add_column(gv_edit, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Cost", "svd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Price", "svd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Discount", "svd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_edit, "svd_sales_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_sales_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_sales_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_disc_ac_id", False)
        add_column(gv_edit, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "UM Conversion", "svd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "svd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Taxable", "svd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Tax Include", "svd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "svd_tax_class", False)
        add_column(gv_edit, "Tax Class", "svd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "PPN Type", "svd_ppn_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Prepayment", "svd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Payment", "svd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sales Unit", "svd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "svd_pod_oid", False)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "sv_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "SO Number", "sv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Effective Date", "sv_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Exchange Rate", "sv_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Consignment", "sv_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "sv_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "sv_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Payment Date", "sv_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Close Date", "sv_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Remarks", "sv_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "sv_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPN", "sv_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "PPH", "sv_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "After Tax", "sv_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Ext. Total", "sv_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPN", "sv_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. PPH", "sv_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Ext. After Tax", "sv_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "User Create", "sv_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "sv_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "sv_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "sv_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "svd_sv_oid", False)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "svd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "svd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost", "svd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Price", "svd_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Discount", "svd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_email, "Account Name", "ac_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Code", "ac_code_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Account Disc. Name", "ac_name_disc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "UM Conversion", "svd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty Real", "svd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Taxable", "svd_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Include", "svd_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Tax Class", "svd_tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Prepayment", "svd_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Payment", "svd_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Sales Unit", "svd_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Status", "svd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_smart, "Code", "sv_code", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sv_oid, " _
                    & "  sv_sq_ref_oid, " _
                    & "  sv_dom_id, " _
                    & "  sv_en_id, " _
                    & "  sv_add_by, " _
                    & "  sv_add_date, " _
                    & "  sv_upd_by, " _
                    & "  sv_upd_date, " _
                    & "  sv_code, " _
                    & "  sv_ptnr_id_sold, " _
                    & "  sv_ptnr_id_bill, " _
                    & "  sv_ref_po_code, " _
                    & "  sv_ref_po_oid, " _
                    & "  sv_date, " _
                    & "  sv_si_id, " _
                    & "  sv_type, " _
                    & "  sv_sales_person, " _
                    & "  sv_pi_id, " _
                    & "  sv_pay_type, " _
                    & "  sv_pay_method, " _
                    & "  sv_ar_ac_id, " _
                    & "  sv_ar_sb_id, " _
                    & "  sv_ar_cc_id, " _
                    & "  sv_dp, " _
                    & "  sv_disc_header, " _
                    & "  sv_total, " _
                    & "  sv_print_count, " _
                    & "  sv_need_date, " _
                    & "  sv_cons, " _
                    & "  sv_close_date, " _
                    & "  sv_tran_id, " _
                    & "  sv_trans_id, " _
                    & "  sv_trans_rmks, " _
                    & "  sv_current_route, " _
                    & "  sv_next_route, " _
                    & "  sv_dt, " _
                    & "  sv_cu_id, " _
                    & "  sv_total_ppn, " _
                    & "  sv_total_pph, " _
                    & "  sv_payment, " _
                    & "  sv_exc_rate, " _
                    & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                    & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                    & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
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
                    & "  cu_name, " _
                    & "  tran_name,sv_sq_ref_oid,sv_status, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  ((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_status_rumah_id)) + " _
                    & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_lama_tinggal_id))+ " _
                    & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_masa_kerja_id))+ " _
                    & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_kepribadian_id)),0)+ " _
                    & "  coalesce((select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_jaminan_id)),0)+ " _
                    & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_income_id))- " _
                    & "  (select sqr_value from sqr_rate where sqr_code_id=(select code_id from code_mstr where code_id=ptnratt_tanggungan_id)))as total_point " _
                    & "FROM  " _
                    & "  public.sv_mstr " _
                    & "  inner join en_mstr on en_id = sv_en_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                    & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                    & "  left outer join ptnra_addr on ptnra_ptnr_oid = ptnr_mstr_sold.ptnr_oid " _
                    & "  inner join si_mstr on si_id = sv_si_id " _
                    & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                    & "  inner join pi_mstr on pi_id = sv_pi_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                    & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                    & "  inner join ac_mstr ac_mstr_ar on ac_mstr_ar.ac_id = sv_ar_ac_id " _
                    & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                    & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                    & "  inner join cu_mstr on cu_id = sv_cu_id " _
                    & "  left outer join tran_mstr on tran_id = sv_tran_id" _
                    & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_mstr_bill.ptnr_oid " _
                        & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                        & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                        & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                        & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                        & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                        & "  LEFT OUTER JOIN public.code_mstr kp on tg.code_id = ptnratt_kepribadian_id " _
                        & "  LEFT OUTER JOIN public.code_mstr ja on tg.code_id = ptnratt_jaminan_id " _
                    & " where sv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and sv_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        If ce_en_id.EditValue = True Then
            get_sequel += " and sv_en_id =" & le_en_id.EditValue
        Else
            get_sequel += " and sv_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        End If
        If ce_cus.EditValue = True Then
            get_sequel += " and sv_ptnr_id_sold =" & le_cus_id.EditValue
        End If
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

    End Sub

    Public Overrides Sub relation_detail()

        Try
            'load semua data detail diawal trus di filter yg tdk efisien sana sekali
            'berattttt booo

            'gv_detail.Columns("sqd_sq_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sq_oid").ToString)
            'gv_detail.BestFitColumns()

            gv_detail_attr.Columns("sqa_sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString)
            gv_detail.BestFitColumns()

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

    Public Overrides Sub insert_data_awal()
        _sv_ptnr_id_sold_mstr = -1
        sv_en_id.Focus()
        sv_en_id.ItemIndex = 0
        sv_date.DateTime = _now
        sv_type.ItemIndex = 0
        sv_sales_person.ItemIndex = 0
        sv_ar_ac_id.ItemIndex = 0
        sv_cu_id.ItemIndex = 0
        sv_ar_cc_id.ItemIndex = 0
        sv_ar_sb_id.ItemIndex = 0
        sv_type.ItemIndex = 0
        sv_pi_id.ItemIndex = 0
        sv_pay_type.ItemIndex = 0
        sv_pay_method.ItemIndex = 0
        sv_trans_rmks.Text = ""
        sv_tran_id.ItemIndex = 0
        sv_ptnr_id_sold.Text = ""
        sv_bantu_address.Text = ""
        sv_cons.EditValue = False

        sv_type.Enabled = True
        sv_pay_type.Enabled = True
        sv_pi_id.Enabled = True
        sv_ref_sq_code.Enabled = True
        sv_ref_sq_code.Text = ""
        _sv_ref_po_oid = ""
        sv_ref_sq_code.Enabled = True
        sv_need_date.DateTime = CekTanggal.Date
        sv_due_date.DateTime = CekTanggal.Date
        sv_trans_rmks.EditValue = ""
        sv_total_point.EditValue = 0.0
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
        sqa_kepribadian_id.ItemIndex = 0
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
                        & "  svd_oid, " _
                        & "  svd_dom_id, " _
                        & "  svd_en_id, " _
                        & "  svd_add_by, " _
                        & "  svd_add_date, " _
                        & "  svd_upd_by, " _
                        & "  svd_upd_date, " _
                        & "  svd_sv_oid, " _
                        & "  svd_seq, " _
                        & "  svd_is_additional_charge, " _
                        & "  svd_si_id, " _
                        & "  svd_pt_id, " _
                        & "  svd_rmks, " _
                        & "  svd_qty, " _
                        & "  svd_qty_allocated, " _
                        & "  svd_qty_picked, " _
                        & "  svd_qty_shipment, " _
                        & "  svd_qty_transfer_issue, " _
                        & "  svd_qty_transfer_receipt, " _
                        & "  svd_qty_pending_inv, " _
                        & "  svd_qty_invoice, " _
                        & "  svd_um, " _
                        & "  svd_cost, " _
                        & "  svd_price, " _
                        & "  svd_total_amount_price, " _
                        & "  svd_disc, " _
                        & "  svd_sales_ac_id, " _
                        & "  svd_sales_sb_id, " _
                        & "  svd_sales_cc_id, " _
                        & "  svd_disc_ac_id, " _
                        & "  svd_um_conv, " _
                        & "  svd_qty_real, " _
                        & "  svd_taxable, " _
                        & "  svd_tax_inc, " _
                        & "  svd_tax_class, " _
                        & "  svd_ppn_type, " _
                        & "  svd_status, " _
                        & "  svd_dt, " _
                        & "  svd_payment, " _
                        & "  svd_dp, " _
                        & "  svd_sales_unit, " _
                        & "  svd_loc_id, " _
                        & "  svd_serial, " _
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
                        & "  tax_class.code_name as svd_tax_class_name, " _
                        & "  loc_desc, " _
                        & "  svd_pod_oid " _
                        & "FROM  " _
                        & "  public.svd_det " _
                        & "  inner join sv_mstr on sv_oid = svd_sv_oid " _
                        & "  inner join en_mstr on en_id = svd_en_id " _
                        & "  inner join si_mstr on si_id = svd_si_id " _
                        & "  inner join pt_mstr on pt_id = svd_pt_id " _
                        & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = svd_sales_cc_id " _
                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                        & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                        & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                        & " where svd_det.svd_seq = -99"
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
        ElseIf sqa_kepribadian_id.Text = "[EditValue is null]" Then
            MessageBox.Show("Kepribadian Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            sqa_kepribadian_id.Focus()
            before_save = False
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

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()


        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim sSql As String
        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("svd_loc_id")) = True Then
                MessageBox.Show("Location Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else
                sSql = "SELECT loc_id " _
                       & "FROM " _
                       & "  public.loc_mstr a " _
                       & "WHERE " _
                       & "  a.loc_id=" & ds_edit.Tables(0).Rows(i).Item("svd_loc_id") & " AND  " _
                       & "  a.loc_en_id=" & sv_en_id.EditValue

                If master_new.PGSqlConn.CekRowSelect(sSql) = 0 Then
                    MessageBox.Show("Location error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    BindingContext(ds_edit.Tables(0)).Position = i
                    Return False
                End If
            End If
        Next

        If sv_type.GetColumnValue("value") = "D" Then
            If sv_need_date.Text = "" Then
                MessageBox.Show("Need Date Can't Empty...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
        End If

        Return before_save
    End Function

#Region "GridView"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _svd_qty, _svd_qty_real, _svd_um_conv, _svd_qty_cost, _svd_cost, _svd_disc, _svd_qty_shipment, _svd_qty_transfer, _svd_payment, _svd_dp As Double
        Dim _svd_pt_id As Integer

        _svd_um_conv = 1
        _svd_qty = 1
        _svd_cost = 0
        _svd_disc = 0
        _svd_payment = 0
        _svd_dp = 0
        _svd_pt_id = -1

        If e.Column.Name = "svd_qty" Then
            '********* Cek Qty Processed
            'Try
            '    _svd_qty_shipment = (gv_edit.GetRowCellValue(e.RowHandle, "svd_qty_shipment"))
            'Catch ex As Exception
            'End Try

            'If e.Value < _svd_qty_shipment Then
            '    MessageBox.Show("Qty SV Can't Lower Than Qty shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    gv_edit.CancelUpdateCurrentRow()
            '    Exit Sub
            'End If
            '********************************
            Try
                _svd_pt_id = (gv_edit.GetRowCellValue(e.RowHandle, "svd_pt_id"))
            Catch ex As Exception
            End Try

            Try
                _svd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "svd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _svd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "svd_cost"))
            Catch ex As Exception
            End Try

            Try
                _svd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "svd_disc"))
            Catch ex As Exception
            End Try

            _svd_qty_real = e.Value * _svd_um_conv
            _svd_qty_cost = (e.Value * _svd_cost) - (e.Value * _svd_cost * _svd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "svd_qty_real", _svd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "svd_qty_cost", _svd_qty_cost)

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sv_pi_id.EditValue, _svd_pt_id, sv_pay_type.EditValue, e.Value))

            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "svd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", _svd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", _svd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", dt_bantu.Rows(0).Item("pidd_payment"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", dt_bantu.Rows(0).Item("pidd_dp"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "svd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", 0)
            End If

            footer()
        ElseIf e.Column.Name = "svd_cost" Then
            Try
                _svd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_qty")))
            Catch ex As Exception
            End Try

            Try
                _svd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_disc")))
            Catch ex As Exception
            End Try

            _svd_qty_cost = (e.Value * _svd_qty) - (e.Value * _svd_qty * _svd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "svd_qty_cost", _svd_qty_cost)
            footer()
        ElseIf e.Column.Name = "svd_disc" Then
            Try
                _svd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_qty")))
            Catch ex As Exception
            End Try

            Try
                _svd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_cost")))
            Catch ex As Exception
            End Try

            _svd_qty_cost = (_svd_cost * _svd_qty) - (_svd_cost * _svd_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "svd_qty_cost", _svd_qty_cost)
            footer()
        ElseIf e.Column.Name = "svd_um_conv" Then
            Try
                _svd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_qty")))
            Catch ex As Exception
            End Try

            _svd_qty_real = e.Value * _svd_qty

            gv_edit.SetRowCellValue(e.RowHandle, "svd_qty_real", _svd_qty_real)
            footer()
        ElseIf e.Column.Name = "svd_taxable" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "svd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "svd_tax_inc", "N")
                gv_edit.SetRowCellValue(e.RowHandle, "svd_tax_class", func_coll.get_id_tax_class("NON-TAX"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_tax_class_name", "NON-TAX")
            End If
            footer()
        ElseIf e.Column.Name = "svd_tax_inc" Then
            If gv_edit.GetRowCellValue(e.RowHandle, "svd_tax_inc").ToString.ToUpper = "Y" And gv_edit.GetRowCellValue(e.RowHandle, "svd_taxable").ToString.ToUpper = "N" Then
                gv_edit.SetRowCellValue(e.RowHandle, "svd_tax_inc", "N")
            End If
            footer()
        ElseIf e.Column.Name = "svd_pt_id" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            _svd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "svd_qty_real")))

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sv_pi_id.EditValue, e.Value, sv_pay_type.EditValue, _svd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "svd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "svd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _svd_payment = 0 * _svd_qty
                _svd_dp = 0 * _svd_qty
            Else
                _svd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _svd_qty
                _svd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _svd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "svd_tax_inc").ToString.ToUpper = "N" Then
                _svd_payment = _svd_payment + (_svd_payment * _tax_rate)
                _svd_dp = _svd_dp + (_svd_dp * _tax_rate)
            End If

            'If sv_type.EditValue = "D" Then
            If dt_bantu.Rows.Count > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "svd_price", dt_bantu.Rows(0).Item("pidd_price"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", _svd_payment)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", _svd_dp)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
            Else
                gv_edit.SetRowCellValue(e.RowHandle, "svd_price", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", 0)
                gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", 0)
            End If
            'End If
            footer()
        ElseIf e.Column.Name = "svd_qty_real" Then
            'Seting Data Berdasar Kepada Price List
            Dim _tax_rate As Double = 0
            Dim _pt_id As Integer
            Try
                _pt_id = gv_edit.GetRowCellValue(e.RowHandle, "svd_pt_id")
            Catch ex As Exception
                Exit Sub
            End Try

            _svd_qty = e.Value

            dt_bantu = New DataTable
            dt_bantu = (load_price_list(sv_pi_id.EditValue, _pt_id, sv_pay_type.EditValue, _svd_qty))

            If gv_edit.GetRowCellValue(e.RowHandle, "svd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(gv_edit.GetRowCellValue(e.RowHandle, "svd_tax_class"))
            End If

            If dt_bantu.Rows.Count = 0 Then
                _svd_payment = 0 * _svd_qty
                _svd_dp = 0 * _svd_qty
            Else
                _svd_payment = dt_bantu.Rows(0).Item("pidd_payment") * _svd_qty
                _svd_dp = dt_bantu.Rows(0).Item("pidd_dp") * _svd_qty
            End If

            If gv_edit.GetRowCellValue(e.RowHandle, "svd_tax_inc").ToString.ToUpper = "N" Then
                _svd_payment = _svd_payment + (_svd_payment * _tax_rate)
                _svd_dp = _svd_dp + (_svd_dp * _tax_rate)
            End If

            If sv_type.EditValue = "D" Then
                If dt_bantu.Rows.Count > 0 Then
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_price", dt_bantu.Rows(0).Item("pidd_price"))
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", dt_bantu.Rows(0).Item("pidd_disc"))
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", _svd_payment)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", _svd_dp)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", dt_bantu.Rows(0).Item("pidd_sales_unit"))
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_price", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_disc", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_payment", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_dp", 0)
                    gv_edit.SetRowCellValue(e.RowHandle, "svd_sales_unit", 0)
                End If
            End If
            footer()
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

    End Sub



    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "svd_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "svd_en_id", sv_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", sv_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "svd_si_id", sv_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", sv_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "svd_is_additional_charge", "N")
            .SetRowCellValue(e.RowHandle, "svd_qty", 1)
            .SetRowCellValue(e.RowHandle, "svd_cost", 0)
            .SetRowCellValue(e.RowHandle, "svd_price", 0)
            .SetRowCellValue(e.RowHandle, "svd_disc", 0)
            .SetRowCellValue(e.RowHandle, "svd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "svd_qty_real", 1)
            .SetRowCellValue(e.RowHandle, "svd_qty_cost", 0)

            .SetRowCellValue(e.RowHandle, "svd_payment", 0)
            .SetRowCellValue(e.RowHandle, "svd_dp", 0)
            .SetRowCellValue(e.RowHandle, "svd_sales_unit", 0)
            .BestFitColumns()
        End With

        sv_type.Enabled = False
        sv_pi_id.Enabled = False
        sv_pay_type.Enabled = False
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

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _sv_oid As Guid
        _sv_oid = Guid.NewGuid
        Dim _sv_code, _sv_terbilang As String
        Dim _sv_total, _sv_total_ppn, _sv_total_pph, _svd_qty, _svd_price, _svd_disc, _sv_total_temp, _tax_rate As Double
        Dim i As Integer


        If _total_point < _verification_min_point Then
            _sv_status = "R"
        ElseIf _total_point >= _verification_min_point Then
            _sv_status = "A"
        End If


        If cek_duplikat_pt_id(gv_edit, "svd_pt_id") = False Then
            Return False
        End If

        _sv_code = func_coll.get_transaction_number("SV", sv_en_id.GetColumnValue("en_code"), "sv_mstr", "sv_code")
        ssqls.Clear()

        Dim _sv_trn_status As String
        Dim ds_bantu As New DataSet

        If sv_pay_type.GetColumnValue("code_usr_1") = 0 Then
            '_sv_trn_status = "C" 'Lansung Default Ke Close
            _sv_trn_status = "D"
        Else
            _sv_trn_status = "D" 'Lansung Default Ke Draft
        End If


        ds_bantu = func_data.load_aprv_mstr(sv_tran_id.EditValue)

        '******* Mencari Total so, Total PPN, Total PPH
        _sv_total = 0
        _sv_total_ppn = 0
        _sv_total_pph = 0
        _sv_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            ''disini hanya line ppn saja
            ' _cost kalau disini diganti jadi price..karena dihitung dari price.....
            If ds_edit.Tables(0).Rows(i).Item("svd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("svd_tax_class"))
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("svd_price") / (1 + _tax_rate)))
            Else
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price")
            End If

            _svd_qty = ds_edit.Tables(0).Rows(i).Item("svd_qty")
            _svd_disc = ds_edit.Tables(0).Rows(i).Item("svd_disc")

            _sv_total = _sv_total + ((_svd_qty * _svd_price) - (_svd_qty * _svd_price * _svd_disc))
        Next

        'disini dihitung ppn dan pph nya
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("svd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("svd_tax_inc").ToString.ToUpper = "Y" Then
                    _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("svd_price") / (1 + _tax_rate)))
                Else
                    _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price")
                End If

                _svd_qty = ds_edit.Tables(0).Rows(i).Item("svd_qty")
                _svd_disc = ds_edit.Tables(0).Rows(i).Item("svd_disc")

                _sv_total_temp = ((_svd_qty * _svd_price) - (_svd_qty * _svd_price * _svd_disc))
                _sv_total_ppn = _sv_total_ppn + (_sv_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")))
                _sv_total_pph = _sv_total_pph + (_sv_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")))
            End If
        Next
        '*******************************************************

        _sv_terbilang = func_bill.TERBILANG_FIX(_sv_total + _sv_total_ppn - _sv_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("svd_dp") * ds_edit.Tables(0).Rows(i).Item("svd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("svd_payment") * ds_edit.Tables(0).Rows(i).Item("svd_qty"))
        Next
        '*************************

        'Deklarasi varible untuk customer baru
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid

        Dim _ptnr_id As Integer
        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sv_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sv_en_id.EditValue.ToString))

        _ptnr_id = SetInteger(GetID_Local(sv_en_id.GetColumnValue("en_code")))

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

        _ptnr_code = _ptnr_code + IIf(sv_en_id.GetColumnValue("en_code") = 0, "99", sv_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        '*************************************

        Dim _id_ptnr_sq As Integer

        If _sv_ptnr_id_sold_mstr = -1 Then
            _id_ptnr_sq = _ptnr_id
        Else
            _id_ptnr_sq = _sv_ptnr_id_sold_mstr
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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sv_mstr " _
                                            & "( " _
                                            & "  sv_oid, " _
                                            & "  sv_dom_id, " _
                                            & "  sv_en_id, " _
                                            & "  sv_add_by, " _
                                            & "  sv_add_date, " _
                                            & "  sv_sq_ref_oid, " _
                                            & "  sv_code, " _
                                            & "  sv_ptnr_id_sold, " _
                                            & "  sv_ptnr_id_bill, " _
                                            & "  sv_ref_po_code, " _
                                            & "  sv_ref_po_oid, " _
                                            & "  sv_date, " _
                                            & "  sv_si_id, " _
                                            & "  sv_type, " _
                                            & "  sv_sales_person, " _
                                            & "  sv_pi_id, " _
                                            & "  sv_pay_type, " _
                                            & "  sv_pay_method, " _
                                            & "  sv_cons, " _
                                            & "  sv_ar_ac_id, " _
                                            & "  sv_ar_sb_id, " _
                                            & "  sv_ar_cc_id, " _
                                            & "  sv_cu_id, " _
                                            & "  sv_dp, " _
                                            & "  sv_disc_header, " _
                                            & "  sv_total, " _
                                            & "  sv_need_date, " _
                                            & "  sv_tran_id, " _
                                            & "  sv_trans_id, " _
                                            & "  sv_trans_rmks, " _
                                            & "  sv_dt, " _
                                            & "  sv_total_ppn, " _
                                            & "  sv_total_pph, " _
                                            & "  sv_payment, " _
                                            & "  sv_interval, " _
                                            & "  sv_terbilang, " _
                                            & "  sv_status " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sv_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sv_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_sq_oid.ToString) & ",  " _
                                            & SetSetring(_sv_code) & ",  " _
                                            & SetInteger(_id_ptnr_sq) & ",  " _
                                            & SetInteger(_id_ptnr_sq) & ",  " _
                                            & SetSetring(sv_ref_sq_code.Text) & ",  " _
                                            & SetSetring(_sv_ref_po_oid) & ",  " _
                                            & SetDate(sv_date.DateTime) & ",  " _
                                            & SetInteger(sv_si_id.EditValue) & ",  " _
                                            & SetSetring(sv_type.EditValue) & ",  " _
                                            & SetInteger(sv_sales_person.EditValue) & ",  " _
                                            & SetInteger(sv_pi_id.EditValue) & ",  " _
                                            & SetInteger(sv_pay_type.EditValue) & ",  " _
                                            & SetInteger(sv_pay_method.EditValue) & ",  " _
                                            & SetBitYN(sv_cons.EditValue) & ",  " _
                                            & SetInteger(sv_ar_ac_id.EditValue) & ",  " _
                                            & SetInteger(sv_ar_sb_id.EditValue) & ",  " _
                                            & SetInteger(sv_ar_cc_id.EditValue) & ",  " _
                                            & SetInteger(sv_cu_id.EditValue) & ",  " _
                                            & SetDbl(_total_dp) & ",  " _
                                            & SetDbl(0) & ",  " _
                                            & SetDbl(_sv_total) & ",  " _
                                            & SetDate(sv_need_date.DateTime) & ",  " _
                                            & SetInteger(sv_tran_id.EditValue) & ",  " _
                                            & SetSetring(_sv_trn_status) & ",  " _
                                            & SetSetring(sv_trans_rmks.Text) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDbl(_sv_total_ppn) & ",  " _
                                            & SetDbl(_sv_total_pph) & ",  " _
                                            & SetDbl(_total_payment) & ",  " _
                                            & SetInteger(sv_pay_type.GetColumnValue("code_usr_1")) & ",  " _
                                            & SetSetring(_sv_terbilang) & ",  " _
                                            & SetSetring(_sv_status) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update sq_mstr set sq_trans_id = 'W' " + _
                                               " where sq_oid = " & SetSetring(_sq_oid) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                               " where wf_ref_oid = '" + _sq_oid + "'" + _
                                               " and wf_seq = 0"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


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
                                            & "  sqa_kepribadian_id = " & SetInteger(sqa_kepribadian_id.EditValue) & " ,  " _
                                            & "  sqa_jaminan_id = " & SetInteger(sqa_jaminan_id.EditValue) & " ,  " _
                                            & "  sqa_bank = " & SetSetring(sqa_bank.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sqa_sq_oid = " & SetSetring(_sq_oid) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from ptnratt_attr where ptnratt_ptnr_oid = " & SetSetring(_oid_ptnr) & ""
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
                                            & "  ptnratt_kepribadian_id, " _
                                            & "  ptnratt_jaminan_id, " _
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
                                            & SetSetring(_oid_ptnr) & ",  " _
                                            & SetInteger(sqa_status_rumah_id.EditValue) & ",  " _
                                            & SetInteger(sqa_lama_tinggal_id.EditValue) & ",  " _
                                            & SetInteger(sqa_masa_kerja_id.EditValue) & ",  " _
                                            & SetInteger(sqa_income_id.EditValue) & ",  " _
                                            & SetInteger(sqa_kepribadian_id.EditValue) & ",  " _
                                            & SetInteger(sqa_jaminan_id.EditValue) & ",  " _
                                            & SetInteger(sqa_tanggungan_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()



                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.svd_det " _
                                                & "( " _
                                                & "  svd_oid, " _
                                                & "  svd_dom_id, " _
                                                & "  svd_en_id, " _
                                                & "  svd_add_by, " _
                                                & "  svd_add_date, " _
                                                & "  svd_sv_oid, " _
                                                & "  svd_seq, " _
                                                & "  svd_is_additional_charge, " _
                                                & "  svd_si_id, " _
                                                & "  svd_pt_id, " _
                                                & "  svd_rmks, " _
                                                & "  svd_qty, " _
                                                & "  svd_um, " _
                                                & "  svd_cost, " _
                                                & "  svd_price, " _
                                                & "  svd_disc, " _
                                                & "  svd_sales_ac_id, " _
                                                & "  svd_sales_sb_id, " _
                                                & "  svd_sales_cc_id, " _
                                                & "  svd_um_conv, " _
                                                & "  svd_qty_real, " _
                                                & "  svd_taxable, " _
                                                & "  svd_tax_inc, " _
                                                & "  svd_tax_class, " _
                                                & "  svd_ppn_type, " _
                                                & "  svd_dt, " _
                                                & "  svd_payment, " _
                                                & "  svd_dp, " _
                                                & "  svd_loc_id, " _
                                                & "  svd_sales_unit, " _
                                                & "  svd_pod_oid " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_oid")) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_sv_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_is_additional_charge")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("svd_rmks")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_price")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_disc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_ac_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_sb_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_cc_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty_real")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_taxable")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_tax_inc")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_ppn_type")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("svd_payment")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("svd_dp")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_loc_id")) & ",  " _
                                                & SetDblDB(ds_edit.Tables(0).Rows(i).Item("svd_sales_unit")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_pod_oid").ToString) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'If SetString(ds_edit.Tables(0).Rows(i).Item("svd_pod_oid").ToString) <> "" Then
                            '    'update karena ada hubungan antara so dan po antar group perusahaan
                            '    .Command.CommandType = CommandType.Text
                            '    .Command.CommandText = "update pod_det set pod_qty_sq = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty")) _
                            '                         & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_pod_oid"))
                            '    ssqls.Add(.Command.CommandText)
                            '    .Command.ExecuteNonQuery()
                            '    .Command.Parameters.Clear()
                            'End If
                        Next




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
                                                        & SetInteger(sv_en_id.EditValue) & ",  " _
                                                        & SetSetring(sv_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_sv_oid.ToString) & ",  " _
                                                        & SetSetring(_sv_code) & ",  " _
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


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert SV " & _sv_code)
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
                        set_row(_sv_oid.ToString, "sv_oid")
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

    Private Function insert_sokp_piutang(ByVal par_obj As Object, ByVal par_sv_oid As String, ByVal par_ref As String, ByVal par_dp As Double, ByVal par_amount As Double) As Boolean
        'insert_sokp_piutang = True
        'Dim i, _interval As Integer

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select code_usr_1 From code_mstr " + _
        '                                   " where code_field = 'payment_type' " + _
        '                                   " and code_id = " + sv_pay_type.EditValue.ToString
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _interval = .DataReader("code_usr_1").ToString
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If sv_need_date.Text = "" Then
        '    MessageBox.Show("Need Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim _date As Date = sv_need_date.DateTime

        'With par_obj
        '    Try
        '        'Untuk Insert Yang DP
        '        'DP harus masuk juga ke kartu piutang....agar pada saat ar jadi pas..
        '        .Command.CommandType = CommandType.Text
        '        .Command.CommandText = "INSERT INTO  " _
        '                            & "  public.sokp_piutang " _
        '                            & "( " _
        '                            & "  sokp_oid, " _
        '                            & "  sokp_sv_oid, " _
        '                            & "  sokp_seq, " _
        '                            & "  sokp_ref, " _
        '                            & "  sokp_amount, " _
        '                            & "  sokp_due_date, " _
        '                            & "  sokp_amount_pay, " _
        '                            & "  sokp_description " _
        '                            & ")  " _
        '                            & "VALUES ( " _
        '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                            & SetSetring(par_sv_oid) & ",  " _
        '                            & SetInteger(0) & ",  " _
        '                            & SetSetring(par_ref) & ",  " _
        '                            & SetDbl(par_dp) & ",  " _
        '                            & SetDate(_date) & "," _
        '                            & SetDbl(0) & ",  " _
        '                            & SetSetring("-") & "  " _
        '                            & ")"
        '        ssqls.Add(.Command.CommandText)
        '        .Command.ExecuteNonQuery()
        '        .Command.Parameters.Clear()

        '        For i = 0 To _interval - 1
        '            _date = _date.AddMonths(1)

        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "INSERT INTO  " _
        '                                & "  public.sokp_piutang " _
        '                                & "( " _
        '                                & "  sokp_oid, " _
        '                                & "  sokp_sv_oid, " _
        '                                & "  sokp_seq, " _
        '                                & "  sokp_ref, " _
        '                                & "  sokp_amount, " _
        '                                & "  sokp_due_date, " _
        '                                & "  sokp_amount_pay, " _
        '                                & "  sokp_description " _
        '                                & ")  " _
        '                                & "VALUES ( " _
        '                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
        '                                & SetSetring(par_sv_oid) & ",  " _
        '                                & SetInteger(i + 1) & ",  " _
        '                                & SetSetring(par_ref) & ",  " _
        '                                & SetDbl(par_amount) & ",  " _
        '                                & SetDate(_date) & ",  " _
        '                                & SetDbl(0) & ",  " _
        '                                & SetSetring("-") & "  " _
        '                                & ")"
        '            ssqls.Add(.Command.CommandText)
        '            .Command.ExecuteNonQuery()
        '            .Command.Parameters.Clear()
        '        Next
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message)
        '        Return False
        '    End Try
        'End With

        'Return insert_sokp_piutang
    End Function

    Public Overrides Function edit_data() As Boolean
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_trans_id") <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been Processed...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        'MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Dim i As Integer
        For i = 0 To ds.Tables("detail").Rows.Count - 1
            If Not IsDBNull(ds.Tables("detail").Rows(i).Item("svd_qty_shipment")) = True Then
                If ds.Tables("detail").Rows(i).Item("svd_qty_shipment") > 0 Then
                    MessageBox.Show("Data already transfer..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next



        ' gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sv_oid_mstr = .Item("sv_oid").ToString
                sv_ar_sb_id.EditValue = .Item("sv_ar_sb_id")
                sv_ar_ac_id.EditValue = .Item("sv_ar_ac_id")
                sv_ar_cc_id.EditValue = .Item("sv_ar_cc_id")
                sv_en_id.EditValue = .Item("sv_en_id")
                sv_date.DateTime = .Item("sv_date")
                sv_pay_method.EditValue = .Item("sv_pay_method")
                sv_cons.EditValue = SetBitYNB(.Item("sv_cons"))
                sv_pay_type.EditValue = .Item("sv_pay_type")
                sv_need_date.DateTime = .Item("sv_need_date")
                sv_pi_id.EditValue = .Item("sv_pi_id")
                _sv_ptnr_id_sold_mstr = SetInteger(.Item("sv_ptnr_id_sold"))
                sv_ptnr_id_sold.Text = SetString(.Item("ptnr_name_sold"))
                sv_ref_sq_code.Text = SetString(.Item("sv_ref_po_code"))
                _sv_ref_po_oid = SetString(.Item("sv_ref_po_oid"))
                If _sv_ref_po_oid <> "" Then
                    sv_ref_sq_code.Enabled = False
                Else
                    sv_ref_sq_code.Enabled = True
                End If
                sv_bantu_address.Text = Trim(.Item("ptnra_line_1") + .Item("ptnra_line_2") + .Item("ptnra_line_3"))
                sv_sales_person.EditValue = .Item("sv_sales_person")
                sv_si_id.EditValue = .Item("sv_si_id")
                'sv_tax_inc.EditValue = SetBitYNB(.Item("sv_tax_inc"))


                sv_tran_id.EditValue = .Item("sv_tran_id")
                sv_trans_rmks.Text = SetString(.Item("sv_trans_rmks"))
                sv_type.EditValue = .Item("sv_type")
                sv_total_point.EditValue = .Item("total_point")
            End With
            sv_en_id.Focus()
            sv_ref_sq_code.Enabled = False
            sv_ptnr_id_sold.Enabled = False
            sv_bantu_address.Enabled = False
            sv_pay_type.Enabled = False
            sv_type.Enabled = False
            sv_pi_id.Enabled = False

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
                            & "  svd_oid, " _
                            & "  svd_dom_id, " _
                            & "  svd_en_id, " _
                            & "  svd_add_by, " _
                            & "  svd_add_date, " _
                            & "  svd_upd_by, " _
                            & "  svd_upd_date, " _
                            & "  svd_sv_oid, " _
                            & "  svd_seq, " _
                            & "  svd_is_additional_charge, " _
                            & "  svd_si_id, " _
                            & "  svd_pt_id, " _
                            & "  svd_rmks, " _
                            & "  svd_qty, " _
                            & "  svd_qty_allocated, " _
                            & "  svd_qty_picked, " _
                            & "  svd_qty_shipment, " _
                            & "  svd_qty_pending_inv, " _
                            & "  svd_qty_invoice, " _
                            & "  svd_um, " _
                            & "  svd_cost, " _
                            & "  svd_price, " _
                            & "  svd_disc, " _
                            & "  svd_sales_ac_id, " _
                            & "  svd_sales_sb_id, " _
                            & "  svd_sales_cc_id, " _
                            & "  svd_disc_ac_id, " _
                            & "  svd_um_conv, " _
                            & "  svd_qty_real, " _
                            & "  svd_taxable, " _
                            & "  svd_tax_inc, " _
                            & "  svd_tax_class, " _
                            & "  svd_ppn_type, " _
                            & "  svd_status, " _
                            & "  svd_dt, " _
                            & "  svd_payment, " _
                            & "  svd_dp, " _
                            & "  svd_sales_unit, " _
                            & "  svd_loc_id, " _
                            & "  svd_serial, " _
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
                            & "  tax_class.code_name as svd_tax_class_name, " _
                            & "  loc_desc, svd_pod_oid " _
                            & "FROM  " _
                            & "  public.svd_det " _
                            & "  inner join sv_mstr on sv_oid = svd_sv_oid " _
                            & "  inner join en_mstr on en_id = svd_en_id " _
                            & "  inner join si_mstr on si_id = svd_si_id " _
                            & "  inner join pt_mstr on pt_id = svd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                            & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = svd_sales_cc_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                            & " where public.svd_det.svd_sv_oid = '" + ds.Tables(0).Rows(row).Item("sv_oid").ToString + "'"

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
                sqa_kepribadian_id.EditValue = SetInteger(.Item("sqa_kepribadian_id"))
                sqa_tanggungan_id.EditValue = SetInteger(.Item("sqa_tanggungan_id"))
                sqa_jaminan_id.EditValue = SetInteger(.Item("sqa_status_rumah_id"))

                sqa_status_rumah_id.Text = SetString(.Item("status_kepemilikan"))
                sqa_lama_tinggal_id.Text = SetString(.Item("lama_tinggal"))
                sqa_masa_kerja_id.Text = SetString(.Item("masa_kerja"))
                sqa_income_id.Text = SetString(.Item("income"))
                sqa_kepribadian_id.Text = SetString(.Item("kepribadian"))
                sqa_tanggungan_id.Text = SetString(.Item("tanggungan"))
                sqa_jaminan_id.Text = SetString(.Item("jaminan"))

            End With
        End If
        sqa_status_rumah_id.ClosePopup()
        sqa_lama_tinggal_id.ClosePopup()
        sqa_masa_kerja_id.ClosePopup()
        sqa_income_id.ClosePopup()
        sqa_kepribadian_id.ClosePopup()
        sqa_tanggungan_id.ClosePopup()
        sqa_jaminan_id.ClosePopup()
        'cek_shipment_row()
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _sv_total, _svd_qty, _svd_price, _svd_disc, _sv_total_ppn, _sv_total_pph, _sv_total_temp, _tax_rate As Double
        Dim _svd_qty_shipment As Double = 0
        Dim i As Integer
        Dim _sv_terbilang As String

        Dim _sv_trn_status As String
        Dim ds_bantu As New DataSet
        _sv_trn_status = "D" 'set default langsung ke D
        ds_bantu = func_data.load_aprv_mstr(sv_tran_id.EditValue)

        ssqls.Clear()

        '******* Mencari Total so, Total PPN, Total PPH
        _sv_total = 0
        _sv_total_ppn = 0
        _sv_total_pph = 0
        _sv_total_temp = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
            'Item Price - Tax Amount = Taxable Base                            
            '100.00 - 9.09 = 90.91 
            If ds_edit.Tables(0).Rows(i).Item("svd_tax_inc").ToString.ToUpper = "Y" Then
                _tax_rate = func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("svd_tax_class"))
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("svd_price") / (1 + _tax_rate)))
            Else
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price")
            End If

            _svd_qty = ds_edit.Tables(0).Rows(i).Item("svd_qty")
            _svd_disc = ds_edit.Tables(0).Rows(i).Item("svd_disc")

            _sv_total = _sv_total + ((_svd_qty * _svd_price) - (_svd_qty * _svd_price * _svd_disc))
        Next

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("svd_ppn_type").ToString.ToUpper = "A" Then
                If ds_edit.Tables(0).Rows(i).Item("svd_tax_inc").ToString.ToUpper = "Y" Then
                    _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("svd_price") / (1 + _tax_rate)))
                Else
                    _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price")
                End If

                _svd_qty = ds_edit.Tables(0).Rows(i).Item("svd_qty")
                _svd_disc = ds_edit.Tables(0).Rows(i).Item("svd_disc")

                _sv_total_temp = ((_svd_qty * _svd_price) - (_svd_qty * _svd_price * _svd_disc))
                _sv_total_ppn = _sv_total_ppn + (_sv_total_temp * func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")))
                _sv_total_pph = _sv_total_pph + (_sv_total_temp * func_coll.get_pph(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")))
            End If
        Next
        '*******************************************************

        _sv_terbilang = func_bill.TERBILANG_FIX(_sv_total + _sv_total_ppn - _sv_total_pph)

        'Menghitung total dp, discount, dan payment
        Dim _total_dp, _total_payment As Double
        _total_dp = 0
        _total_payment = 0

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _total_dp = _total_dp + (ds_edit.Tables(0).Rows(i).Item("svd_dp") * ds_edit.Tables(0).Rows(i).Item("svd_qty"))
            _total_payment = _total_payment + (ds_edit.Tables(0).Rows(i).Item("svd_payment") * ds_edit.Tables(0).Rows(i).Item("svd_qty"))
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
                                            & "  public.sv_mstr   " _
                                            & "SET  " _
                                            & "  sv_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  sv_en_id = " & SetInteger(sv_en_id.EditValue) & ",  " _
                                            & "  sv_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sv_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sv_date = " & SetDate(sv_date.DateTime) & ",  " _
                                            & "  sv_ptnr_id_sold = " & SetInteger(_sv_ptnr_id_sold_mstr) & ",  " _
                                            & "  sv_ref_po_code = " & SetSetring(sv_ref_sq_code.Text) & ",  " _
                                            & "  sv_ref_po_oid = " & SetSetring(_sv_ref_po_oid) & ",  " _
                                            & "  sv_si_id = " & SetInteger(sv_si_id.EditValue) & ",  " _
                                            & "  sv_sales_person = " & SetInteger(sv_sales_person.EditValue) & ",  " _
                                            & "  sv_pay_method = " & SetInteger(sv_pay_method.EditValue) & ",  " _
                                            & "  sv_cons = " & SetBitYN(sv_cons.EditValue) & ",  " _
                                            & "  sv_dp = " & SetDbl(_total_dp) & ",  " _
                                            & "  sv_total = " & SetDbl(_sv_total) & ",  " _
                                            & "  sv_need_date = " & SetDate(sv_need_date.DateTime) & ",  " _
                                            & "  sv_tran_id = " & SetInteger(sv_tran_id.EditValue) & ",  " _
                                            & "  sv_trans_id = " & SetSetring(_sv_trn_status) & ",  " _
                                            & "  sv_trans_rmks = " & SetSetring(sv_trans_rmks.Text) & ",  " _
                                            & "  sv_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sv_total_ppn = " & SetDbl(_sv_total_ppn) & ",  " _
                                            & "  sv_total_pph = " & SetDbl(_sv_total_pph) & ",  " _
                                            & "  sv_payment = " & SetDbl(_total_payment) & ",  " _
                                            & "  sv_terbilang = " & SetSetring(_sv_terbilang) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sv_oid = " & SetSetring(_sv_oid_mstr) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        'Ini untuk update pod_qty_so dikarenakan ada link antara so dan po yang satu group perusahaan
                        ' ''.Command.CommandType = CommandType.Text
                        ' ''.Command.CommandText = "update pod_det set pod_qty_sq = 0 where pod_oid in (select svd_pod_oid from svd_det where svd_sv_oid = " + SetSetring(_sv_oid_mstr) + ")"
                        ' ''ssqls.Add(.Command.CommandText)
                        ' ''.Command.ExecuteNonQuery()
                        ' ''.Command.Parameters.Clear()
                        '******************************************************

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table transfer
                        'kalau sudah relasi ke table transfer jadi nya error...dan harusnya tidak bisa didelete
                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from svd_det where coalesce((select count(*) as jml from ptsfrd_det where ptsfrd_svd_oid=svd_oid),0) = 0 and svd_sv_oid = '" + _sv_oid_mstr + "'"
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _svd_qty_shipment = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("svd_qty_shipment")) = True, 0, ds_edit.Tables(0).Rows(i).Item("svd_qty_shipment"))
                            ' ''If SetNumber(ds_edit.Tables(0).Rows(i).Item("status_shipment")) = 0 Then
                            ' ''    .Command.CommandType = CommandType.Text
                            ' ''    .Command.CommandText = "INSERT INTO  " _
                            ' ''                    & "  public.svd_det " _
                            ' ''                    & "( " _
                            ' ''                    & "  svd_oid, " _
                            ' ''                    & "  svd_dom_id, " _
                            ' ''                    & "  svd_en_id, " _
                            ' ''                    & "  svd_add_by, " _
                            ' ''                    & "  svd_add_date, " _
                            ' ''                    & "  svd_sv_oid, " _
                            ' ''                    & "  svd_seq, " _
                            ' ''                    & "  svd_is_additional_charge, " _
                            ' ''                    & "  svd_si_id, " _
                            ' ''                    & "  svd_pt_id, " _
                            ' ''                    & "  svd_rmks, " _
                            ' ''                    & "  svd_qty, " _
                            ' ''                    & "  svd_um, " _
                            ' ''                    & "  svd_cost, " _
                            ' ''                    & "  svd_price, " _
                            ' ''                    & "  svd_disc, " _
                            ' ''                    & "  svd_sales_ac_id, " _
                            ' ''                    & "  svd_sales_sb_id, " _
                            ' ''                    & "  svd_sales_cc_id, " _
                            ' ''                    & "  svd_um_conv, " _
                            ' ''                    & "  svd_qty_real, " _
                            ' ''                    & "  svd_taxable, " _
                            ' ''                    & "  svd_tax_inc, " _
                            ' ''                    & "  svd_tax_class, " _
                            ' ''                    & "  svd_ppn_type, " _
                            ' ''                    & "  svd_dt, " _
                            ' ''                    & "  svd_payment, " _
                            ' ''                    & "  svd_dp, " _
                            ' ''                    & "  svd_loc_id, " _
                            ' ''                    & "  svd_sales_unit " _
                            ' ''                    & ")  " _
                            ' ''                    & "VALUES ( " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_oid")) & ",  " _
                            ' ''                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_en_id")) & ",  " _
                            ' ''                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            ' ''                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                            ' ''                    & SetSetring(_sv_oid_mstr.ToString) & ",  " _
                            ' ''                    & SetInteger(i) & ",  " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_is_additional_charge")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_si_id")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_pt_id")) & ",  " _
                            ' ''                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("svd_rmks")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_cost")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_price")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_disc")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_ac_id")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_sb_id")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_cc_id")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um_conv")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty_real")) & ",  " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_taxable")) & ",  " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_tax_inc")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")) & ",  " _
                            ' ''                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_ppn_type")) & ",  " _
                            ' ''                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_payment")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_dp")) & ",  " _
                            ' ''                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_loc_id")) & ",  " _
                            ' ''                    & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_sales_unit")) & "  " _
                            ' ''                    & ")"
                            ' ''    ssqls.Add(.Command.CommandText)
                            ' ''    .Command.ExecuteNonQuery()
                            ' ''    .Command.Parameters.Clear()
                            ' ''Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.svd_det   " _
                                                & "SET  " _
                                                & "  svd_en_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_en_id")) & ",  " _
                                                & "  svd_is_additional_charge = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_is_additional_charge")) & ",  " _
                                                & "  svd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_si_id")) & ",  " _
                                                & "  svd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("svd_rmks")) & ",  " _
                                                & "  svd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty")) & ",  " _
                                                & "  svd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um")) & ",  " _
                                                & "  svd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_cost")) & ",  " _
                                                & "  svd_price = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_price")) & ",  " _
                                                & "  svd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_disc")) & ",  " _
                                                & "  svd_sales_ac_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_ac_id")) & ",  " _
                                                & "  svd_sales_sb_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_sb_id")) & ",  " _
                                                & "  svd_sales_cc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_sales_cc_id")) & ",  " _
                                                & "  svd_um_conv = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_um_conv")) & ",  " _
                                                & "  svd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty_real")) & ",  " _
                                                & "  svd_taxable = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_taxable")) & ",  " _
                                                & "  svd_tax_inc = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_tax_inc")) & ",  " _
                                                & "  svd_tax_class = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")) & ",  " _
                                                & "  svd_ppn_type = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_ppn_type")) & ",  " _
                                                & "  svd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                                & "  svd_payment = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_payment")) & ",  " _
                                                & "  svd_dp = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_dp")) & ",  " _
                                                & "  svd_loc_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("svd_loc_id")) & ",  " _
                                                & "  svd_sales_unit = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_sales_unit")) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  svd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_oid")) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
                                                & "  sqa_kepribadian_id = " & SetInteger(sqa_kepribadian_id.EditValue) & " ,  " _
                                                & "  sqa_tanggungan_id = " & SetInteger(sqa_tanggungan_id.EditValue) & " ,  " _
                                                & "  sqa_jaminan_id = " & SetInteger(sqa_jaminan_id.EditValue) & " ,  " _
                                                & "  sqa_bank = " & SetSetring(sqa_bank.Text) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  sqa_oid = " & SetSetring(_sqa_oid_mstr) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("svd_pod_oid").ToString <> "" Then
                                'update karena ada hubungan antara so dan po antar group perusahaan
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pod_det set pod_qty_so = " + SetDbl(ds_edit.Tables(0).Rows(i).Item("svd_qty")) _
                                                     & " where pod_oid  = " + SetSetring(ds_edit.Tables(0).Rows(i).Item("svd_pod_oid"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next


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
                                                        & SetInteger(sv_en_id.EditValue) & ",  " _
                                                        & SetSetring(sv_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_sv_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code")) & ",  " _
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

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, sv_en_id.EditValue, 10, _sv_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code"), sv_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit SV " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code"))
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
                        set_row(_sv_oid_mstr, "sv_oid")
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
                    .SQL = "select coalesce(svd_qty_shipment,0) as svd_qty_shipment from svd_det " + _
                           " where svd_sv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "svd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("svd_qty_shipment") > 0 Then
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
                            .Command.CommandText = "update pod_det set pod_qty_so = 0 where pod_oid in (select svd_pod_oid from svd_det where svd_sv_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString) + ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            '******************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from sv_mstr where sv_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete SV " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update sq_mstr set sq_trans_id = 'D' " + _
                                                   " where sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_sq_ref_oid").ToString + "'"

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

    Private Sub sv_ptnr_id_sold_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sv_ptnr_id_sold.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = sv_en_id.EditValue
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
        Dim _svd_price As Double = 0

        If ds_edit.Tables.Count = 0 Then
            Exit Sub
        End If

        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("svd_taxable").ToString.ToUpper = "Y" Then
                _tax_rate = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")) = True, 0, func_coll.get_ppn(ds_edit.Tables(0).Rows(i).Item("svd_tax_class")))
            Else
                _tax_rate = 0
            End If

            If ds_edit.Tables(0).Rows(i).Item("svd_tax_inc").ToString.ToUpper = "Y" Then
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("svd_price") / (1 + _tax_rate)))
            Else
                _svd_price = ds_edit.Tables(0).Rows(i).Item("svd_price")
            End If

            _dpp = _dpp + (ds_edit.Tables(0).Rows(i).Item("svd_qty_real") * _svd_price)
            _dpp_line = (ds_edit.Tables(0).Rows(i).Item("svd_qty_real") * _svd_price)
            _discount = _discount + (ds_edit.Tables(0).Rows(i).Item("svd_qty_real") * _svd_price * ds_edit.Tables(0).Rows(i).Item("svd_disc"))
            _discount_line = (ds_edit.Tables(0).Rows(i).Item("svd_qty_real") * _svd_price * ds_edit.Tables(0).Rows(i).Item("svd_disc"))
            _ppn = _ppn + (_tax_rate * (_dpp_line - _discount_line))
        Next

        te_dpp.EditValue = _dpp
        te_discount.EditValue = _discount
        te_ppn.EditValue = _ppn
        te_total.EditValue = _dpp - _discount + _ppn
    End Sub


    Public Overrides Sub preview()


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
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString
        _colom = "sv_trans_id"
        _table = "sv_mstr"
        _criteria = "sv_code"
        _initial = "sv"
        _type = "sv"

        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update sq_mstr set sq_trans_id = 'D' " + _
                                               " where sq_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_sq_ref_oid").ToString + "'"

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

                        'help_load_data(True)
                        'MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                        .Command.CommandText = "select count(sv_code) as jml " _
                                             & " from sv_mstr " _
                                             & " inner join svd_det on svd_sv_oid = sv_oid " _
                                             & " where sv_code ~~* '" + par_code + "' " _
                                             & " and svd_qty_shipment >= 1"

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
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code")
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
                    gv_email.Columns("sv_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("sv_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("sv_code"), 0)
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
                                .Command.CommandText = "update sv_mstr set sv_trans_id = '" + _trans_id + "'," + _
                                               " sv_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " sv_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where sv_oid = '" + ds.Tables("smart").Rows(i).Item("sv_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("sv_code") + "'" + _
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

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("sv_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("sv_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Sales Order", ds.Tables("smart").Rows(i).Item("sv_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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

        _conf_value = func_coll.get_conf_file("wf_sales_verification")

        If _conf_value = "0" Then
            Exit Sub
        End If

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_status") = "R" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString
        _colom = "sv_trans_id"
        _table = "sv_mstr"
        _criteria = "sv_code"
        _initial = "sv"
        _type = "sv"
        _title = "Sales Verification"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)


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
                & "  svd_oid, " _
                & "  svd_dom_id, " _
                & "  svd_en_id, " _
                & "  svd_add_by, " _
                & "  svd_add_date, " _
                & "  svd_upd_by, " _
                & "  svd_upd_date, " _
                & "  svd_sv_oid, " _
                & "  svd_seq, " _
                & "  svd_is_additional_charge, " _
                & "  svd_si_id, " _
                & "  svd_pt_id, " _
                & "  svd_rmks, " _
                & "  svd_qty, " _
                & "  svd_qty_allocated, " _
                & "  svd_qty_picked, " _
                & "  svd_qty_shipment, " _
                & "  svd_qty_pending_inv, " _
                & "  svd_qty_invoice, " _
                & "  svd_um, " _
                & "  svd_cost, " _
                & "  svd_price, " _
                & "  svd_disc, " _
                & "  svd_sales_ac_id, " _
                & "  svd_sales_sb_id, " _
                & "  svd_sales_cc_id, " _
                & "  svd_disc_ac_id, " _
                & "  svd_um_conv, " _
                & "  svd_qty_real, " _
                & "  svd_taxable, " _
                & "  svd_tax_inc, " _
                & "  svd_tax_class, " _
                & "  svd_status, " _
                & "  svd_dt, " _
                & "  svd_payment, " _
                & "  svd_dp, " _
                & "  svd_sales_unit, " _
                & "  svd_loc_id, " _
                & "  svd_serial, " _
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
                & "  tax_class.code_name as svd_tax_class_name, " _
                & "  svd_ppn_type, " _
                & "  loc_desc " _
                & "FROM  " _
                & "  public.svd_det " _
                & "  inner join sv_mstr on sv_oid = svd_sv_oid " _
                & "  inner join en_mstr on en_id = svd_en_id " _
                & "  inner join si_mstr on si_id = svd_si_id " _
                & "  inner join pt_mstr on pt_id = svd_pt_id " _
                & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = svd_sales_cc_id " _
                & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                & "  where svd_sv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString & "'"

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
                & "  sqa_kepribadian_id, " _
                & "  sqa_tanggungan_id, " _
                & "  sqa_jaminan_id, " _
                & "  sr.code_name as status_kepemilikan, " _
                & "  lt.code_name as lama_tinggal, " _
                & "  mk.code_name as masa_kerja, " _
                & "  ic.code_name as income, " _
                & "  kp.code_name as kepribadian, " _
                & "  tg.code_name as tanggungan, " _
                & "  ja.code_name as jaminan, " _
                & "  sqa_bank " _
                & "FROM  " _
                & "  public.sqa_attr" _
                & "  inner join sq_mstr on sq_oid = sqa_sq_oid " _
                & "  inner join sv_mstr on sv_sq_ref_oid = sq_mstr.sq_oid " _
                & "  left outer join code_mstr sr on sr.code_id = sqa_status_rumah_id " _
                & "  left outer join code_mstr lt on lt.code_id = sqa_lama_tinggal_id " _
                & "  left outer join code_mstr mk on mk.code_id = sqa_masa_kerja_id " _
                & "  left outer join code_mstr ic on ic.code_id = sqa_income_id " _
                & "  left outer join code_mstr kp on kp.code_id = sqa_kepribadian_id " _
                & "  left outer join code_mstr tg on tg.code_id = sqa_tanggungan_id " _
                & "  left outer join code_mstr ja on ja.code_id = sqa_jaminan_id " _
                & "  where sv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString & "'"



            load_data_detail(sql, gc_detail_attr, "detail_attr")

            'Try
            '    ds.Tables("detail_kp").Clear()
            'Catch ex As Exception
            'End Try

            'sql = "SELECT  " _
            '    & "  sokp_oid, " _
            '    & "  sokp_sv_oid, " _
            '    & "  sokp_seq, " _
            '    & "  sokp_ref, " _
            '    & "  sokp_amount, " _
            '    & "  sokp_due_date, " _
            '    & "  sokp_amount_pay, " _
            '    & "  sokp_description " _
            '    & "FROM  " _
            '    & "  public.sokp_piutang " _
            '    & "  inner join public.sv_mstr on sv_oid = sokp_sv_oid " _
            '    & " where sokp_sv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString & "'"


            'load_data_detail(sql, gc_detail_kp, "detail_kp")

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
                      " inner join sv_mstr on sv_code = wf_ref_code " + _
                      " inner join svd_det dt on dt.svd_sv_oid = sv_oid " _
                      & "  where sv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString & "' " _
                      & " order by wf_ref_code, wf_seq "


                load_data_detail(sql, gc_wf, "wf")
                gv_wf.BestFitColumns()

                Try
                    ds.Tables("email").Clear()
                Catch ex As Exception
                End Try

                sql = "SELECT  " _
                           & "  sv_oid, " _
                            & "  sv_en_id, " _
                            & "  sv_add_by, " _
                            & "  sv_add_date, " _
                            & "  sv_upd_by, " _
                            & "  sv_upd_date, " _
                            & "  sv_code, " _
                            & "  sv_date, " _
                            & "  sv_dp, " _
                            & "  sv_disc_header, " _
                            & "  sv_total, " _
                            & "  sv_need_date, " _
                            & "  sv_cons, " _
                            & "  sv_close_date, " _
                            & "  sv_tran_id, " _
                            & "  sv_trans_id, " _
                            & "  sv_trans_rmks, " _
                            & "  sv_cu_id, " _
                            & "  sv_total_ppn, " _
                            & "  sv_total_pph, " _
                            & "  sv_payment, " _
                            & "  sv_exc_rate, " _
                            & "  (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax, " _
                            & "  sv_exc_rate * sv_total as sv_total_ext,  sv_exc_rate * sv_total_ppn as sv_total_ppn_ext, " _
                            & "  sv_exc_rate * sv_total_pph as sv_total_pph_ext,  sv_exc_rate * (sv_total + sv_total_ppn + sv_total_pph) as sv_total_after_tax_ext, " _
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
                            & "  svd_oid, " _
                            & "  svd_sv_oid, " _
                            & "  svd_pt_id, " _
                            & "  svd_rmks, " _
                            & "  svd_qty, " _
                            & "  svd_um, " _
                            & "  svd_cost, " _
                            & "  svd_price, " _
                            & "  svd_disc, " _
                            & "  svd_sales_ac_id, " _
                            & "  svd_sales_sb_id, " _
                            & "  svd_sales_cc_id, " _
                            & "  svd_disc_ac_id, " _
                            & "  svd_um_conv, " _
                            & "  svd_qty_real, " _
                            & "  svd_taxable, " _
                            & "  svd_tax_inc, " _
                            & "  svd_tax_class, " _
                            & "  svd_status, " _
                            & "  svd_dt, " _
                            & "  svd_payment, " _
                            & "  svd_dp, " _
                            & "  svd_sales_unit, " _
                            & "  svd_loc_id, " _
                            & "  svd_serial, " _
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
                            & "  tax_class.code_name as svd_tax_class_name, " _
                            & "  loc_desc " _
                            & "FROM  " _
                            & "  public.sv_mstr " _
                            & "  inner join en_mstr on en_id = sv_en_id " _
                            & "  inner join ptnr_mstr ptnr_mstr_sold on ptnr_mstr_sold.ptnr_id = sv_ptnr_id_sold " _
                            & "  inner join ptnr_mstr ptnr_mstr_bill on ptnr_mstr_bill.ptnr_id = sv_ptnr_id_bill " _
                            & "  inner join ptnr_mstr ptnr_mstr_sales on ptnr_mstr_sales.ptnr_id = sv_sales_person " _
                            & "  inner join pi_mstr on pi_id = sv_pi_id " _
                            & "  inner join code_mstr pay_type on pay_type.code_id = sv_pay_type " _
                            & "  inner join code_mstr pay_method on pay_method.code_id = sv_pay_method " _
                            & "  inner join sb_mstr sb_mstr_ar on sb_mstr_ar.sb_id = sv_ar_sb_id " _
                            & "  inner join cc_mstr cc_mstr_ar on cc_mstr_ar.cc_id = sv_ar_cc_id " _
                            & "  inner join cu_mstr on cu_id = sv_cu_id " _
                            & "  inner join tran_mstr on tran_id = sv_tran_id" _
                            & "  inner join svd_det on sv_oid = svd_sv_oid " _
                            & "  inner join si_mstr on si_id = svd_si_id " _
                            & "  inner join pt_mstr on pt_id = svd_pt_id " _
                            & "  inner join code_mstr um_mstr on um_mstr.code_id = svd_um	 " _
                            & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = svd_sales_ac_id " _
                            & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = svd_sales_sb_id " _
                            & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = svd_sales_ac_id " _
                            & "  inner join code_mstr tax_class on tax_class.code_id = svd_tax_class	 " _
                            & "  left outer join loc_mstr on loc_id = svd_loc_id" _
                            & "  where sv_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_oid").ToString & "' "


                load_data_detail(sql, gc_email, "email")
                gv_email.BestFitColumns()

                ' belum(diaktifkan)

                Try
                    ds.Tables("smart").Clear()
                Catch ex As Exception
                End Try

                sql = "select sv_oid, sv_code, sv_trans_id, false as status from sv_mstr " _
                    & " where sv_trans_id ~~* 'd' "
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
                & "  a.sv_oid, " _
                & "  a.sv_code, " _
                & "  a.sv_date, " _
                & "  b.ptnr_name " _
                & "FROM " _
                & "  public.sv_mstr a " _
                & "  INNER JOIN public.ptnr_mstr b ON (a.sv_ptnr_id_sold = b.ptnr_id) " _
                & "WHERE " _
                & "  a.sv_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code").ToString & "'"


            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "sv_master")

            sSQL = "SELECT  " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  c.code_code AS um_desc, " _
                & "  a.svd_price, " _
                & "  a.svd_qty, " _
                & "  b.pt_isbn,a.svd_seq " _
                & "FROM " _
                & "  public.pt_mstr b " _
                & "  INNER JOIN public.svd_det a ON (b.pt_id = a.svd_pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
                & "  INNER JOIN public.sv_mstr d ON (a.svd_sv_oid = d.sv_oid) " _
                & "WHERE " _
                & "  d.sv_code = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code").ToString & "' " _
                & "order by a.svd_seq"

            daT1 = New nPgSqlDataAdapter(sSQL, objConn)
            daT1.Fill(Ds_export, "sv_detail")

            Dim objSaveFileDialog As New SaveFileDialog
            Dim filePath As String

            'Set the Save dialog properties

            With objSaveFileDialog
                .DefaultExt = "xml"
                .FileName = "Export_sv_" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sv_code").ToString & Now().ToString("yyyyMMdd-HHmmss")
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
                    ds_edit.Tables(0).Rows(i).Item("svd_loc_id") = gv_edit.GetRowCellValue(_row, "svd_loc_id")
                Next
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sv_ref_sq_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sv_ref_sq_code.ButtonClick
        Dim frm As New FSalesQuotationSearch
        frm.set_win(Me)
        frm._en_id = sv_en_id.EditValue
        frm._date = sv_date.DateTime
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub sv_ref_sq_code_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sv_ref_sq_code.EditValueChanged

    End Sub

    Private Sub gc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gc_master.Click

    End Sub

    Private Sub sv_total_point_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sv_total_point.EditValueChanged

    End Sub

    Private Sub sqa_jaminan_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sqa_jaminan_id.EditValueChanged
        _point_status_rumah = 0
        _point_income = 0
        _point_lama_kerja = 0
        _point_lama_tinggal = 0
        _point_jaminan = 0
        _point_kepribadian = 0
        _point_tanggungan = 0
        _point_jaminan = func_data.get_value(sqa_jaminan_id.EditValue)
        _point_kepribadian = func_data.get_value(sqa_kepribadian_id.EditValue)
        _point_status_rumah = func_data.get_value(sqa_status_rumah_id.EditValue)
        _point_income = func_data.get_value(sqa_income_id.EditValue)
        _point_lama_kerja = func_data.get_value(sqa_masa_kerja_id.EditValue)
        _point_lama_tinggal = func_data.get_value(sqa_lama_tinggal_id.EditValue)
        _point_tanggungan = func_data.get_value(sqa_tanggungan_id.EditValue)
        sv_total_point.EditValue = _point_jaminan + _point_kepribadian + _point_status_rumah + _point_income + _point_lama_kerja + _
        _point_lama_tinggal - _point_tanggungan
    End Sub

    Private Sub sqa_kepribadian_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sqa_kepribadian_id.EditValueChanged
        _point_status_rumah = 0
        _point_income = 0
        _point_lama_kerja = 0
        _point_lama_tinggal = 0
        _point_jaminan = 0
        _point_kepribadian = 0
        _point_tanggungan = 0
        _point_jaminan = func_data.get_value(sqa_jaminan_id.EditValue)
        _point_kepribadian = func_data.get_value(sqa_kepribadian_id.EditValue)
        _point_status_rumah = func_data.get_value(sqa_status_rumah_id.EditValue)
        _point_income = func_data.get_value(sqa_income_id.EditValue)
        _point_lama_kerja = func_data.get_value(sqa_masa_kerja_id.EditValue)
        _point_lama_tinggal = func_data.get_value(sqa_lama_tinggal_id.EditValue)
        _point_tanggungan = SetNumber(func_data.get_value(SetNumber(sqa_tanggungan_id.EditValue)))
        sv_total_point.EditValue = _point_jaminan + _point_kepribadian + _point_status_rumah + _point_income + _point_lama_kerja + _
        _point_lama_tinggal - _point_tanggungan
    End Sub
End Class
