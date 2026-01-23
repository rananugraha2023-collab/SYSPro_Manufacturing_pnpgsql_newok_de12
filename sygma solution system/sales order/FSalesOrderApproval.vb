Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FSalesOrderApproval
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet

    Private Sub FSalesOrderApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        gv_os.Columns("status").VisibleIndex = 0

        AddHandler gv_os.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_os.ColumnFilterChanged, AddressOf relation_detail

        AddHandler gv_all.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_all.ColumnFilterChanged, AddressOf relation_detail

        _user_approval = get_user_approval()
    End Sub

    Private Function get_user_approval() As String
        get_user_approval = "'" + master_new.ClsVar.sNama + "',"
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupnama from tconfusergroup ug " + _
                           " inner join tconfgroup g on g.groupid = ug.groupid " + _
                           " where userid = " + master_new.ClsVar.sUserID.ToString
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "wfs_status")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            get_user_approval = get_user_approval + "'" + ds_bantu.Tables(0).Rows(i).Item("groupnama") + "',"
        Next
        get_user_approval = get_user_approval.Substring(0, Len(get_user_approval) - 1)
        Return get_user_approval
    End Function

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_wfs_status())
        le_wfs_status.Properties.DataSource = dt_bantu
        le_wfs_status.Properties.DisplayMember = dt_bantu.Columns("wfs_desc").ToString
        le_wfs_status.Properties.ValueMember = dt_bantu.Columns("wfs_id").ToString
        le_wfs_status.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_os, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "SO Type", "so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Taxable", "so_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Include", "so_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_os, "Payment", "so_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_os, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Sales Unit", "so_sales_unit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_os, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_os, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_os, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_os, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_all, "Status", "so_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "SO Type", "so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Bill To", "ptnr_name_bill", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Taxable", "so_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Include", "so_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Tax Class", "tax_class_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Credit Terms", "credit_term_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Payment Type", "pay_type_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Payment Method", "pay_method_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Consignment", "so_cons", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Prepayment", "so_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Discount", "so_disc_header", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_all, "Payment", "so_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Payment Date", "so_payment_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Close Date", "so_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_all, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Remarks", "so_trans_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Sales Unit", "so_sales_unit", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Total", "so_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "PPN", "so_total_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "PPH", "so_total_pph", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "After Tax", "so_total_after_tax", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_all, "Ext. Total", "so_total_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. PPN", "so_total_ppn_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. PPH", "so_total_pph_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "Ext. After Tax", "so_total_after_tax_ext", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_all, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_all, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_all, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail, "sod_oid", False)
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
        add_column_copy(gv_detail, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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
        add_column_copy(gv_detail, "Prepayment", "sod_dp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Payment", "sod_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Serial", "sod_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_attr, "soa_so_oid", False)
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
        add_column_copy(gv_detail_kp, "Amount Pay", "sokp_amount_pay", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_kp, "Description", "sokp_description", DevExpress.Utils.HorzAlignment.Default)

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
        add_column_copy(gv_email, "Status", "sod_status", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                'ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            Try
                                ds.Tables("os").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
                                & "  false as status, " _
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
                                & "  pay_type.code_name as pay_type_name, " _
                                & "  pay_method.code_name as pay_method_name, " _
                                & "  ac_mstr_ar.ac_code, " _
                                & "  ac_mstr_ar.ac_name, " _
                                & "  sb_mstr_ar.sb_desc, " _
                                & "  cc_mstr_ar.cc_desc, " _
                                & "  cu_name, " _
                                & "  tran_name ,wf_seq, coalesce(useremail,'') as useremail " _
                                & "FROM  " _
                                & "  public.so_mstr " _
                                & "  inner join en_mstr on en_id = so_en_id " _
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
                                & "  inner join cu_mstr on cu_id = so_cu_id " _
                                & "  inner join tran_mstr on tran_id = so_tran_id" _
                                & " inner join wf_mstr wm on wf_ref_oid = so_oid " _
                                & " inner join tconfuser on usernama = so_add_by " _
                                & " where so_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and wf_iscurrent ~~* 'Y'"
                            .InitializeCommand()
                            .FillDataSet(ds, "os")
                            gc_os.DataSource = ds.Tables("os")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            Try
                                ds.Tables("all").Clear()
                            Catch ex As Exception
                            End Try

                            .SQL = "SELECT  " _
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
                                & "  pay_type.code_name as pay_type_name, " _
                                & "  pay_method.code_name as pay_method_name, " _
                                & "  ac_mstr_ar.ac_code, " _
                                & "  ac_mstr_ar.ac_name, " _
                                & "  sb_mstr_ar.sb_desc, " _
                                & "  cc_mstr_ar.cc_desc, " _
                                & "  cu_name, " _
                                & "  tran_name " _
                                & "FROM  " _
                                & "  public.so_mstr " _
                                & "  inner join en_mstr on en_id = so_en_id " _
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
                                & "  inner join cu_mstr on cu_id = so_cu_id " _
                                & "  inner join tran_mstr on tran_id = so_tran_id" _
                                & " inner join wf_mstr wm on wf_ref_oid = so_oid " _
                                & " where so_date >= " + SetDate(pr_txttglawal.DateTime) _
                                & " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                                & " and so_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                & " and wf_user_id in (" + _user_approval + ")" _
                                & " and so_trans_id <> 'D' "
                            .InitializeCommand()
                            .FillDataSet(ds, "all")
                            gc_all.DataSource = ds.Tables("all")
                        End If
                        
                        bestfit_column()
                        load_data_grid_detail()
                        relation_detail()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Overrides Sub load_data_grid_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            If ds.Tables("all").Rows.Count = 0 Then
                Exit Sub
            End If
        End If

        ds_detail = New DataSet
        Try
            Using objload As New master_new.WDABasepgsql("", "")
                With objload
                    If xtc_master.SelectedTabPageIndex = 0 Then
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
                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                        & "  sb_desc, " _
                        & "  cc_desc, " _
                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                        & "  tax_class.code_name as sod_tax_class_name, " _
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
                        & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                        & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & "  and wf_user_id in (" + _user_approval + ")" _
                        & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join so_mstr on so_code = wf_ref_code " + _
                              " inner join sod_det dt on dt.sod_so_oid = so_oid " _
                            & " where wf_ref_code in (select so_code from so_mstr inner join wf_mstr on wf_ref_code = so_code " _
                                                  & " where so_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + ")" _
                                                  & " and wf_iscurrent ~~* 'Y')" _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                        & "  soa_oid, " _
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
                        & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                        & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                             & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                        & "  and wf_user_id in (" + _user_approval + ")" _
                        & "  and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "customer")
                        gc_detail_attr.DataSource = ds_detail.Tables("customer")
                        'gv_detail_attr.BestFitColumns()

                        .SQL = "SELECT  " _
                                 & "  sokp_oid, " _
                                 & "  sokp_so_oid, " _
                                 & "  sokp_seq, " _
                                 & "  sokp_ref, " _
                                 & "  sokp_amount, " _
                                 & "  sokp_amount_pay, " _
                                 & "  sokp_description " _
                                 & "FROM  " _
                                 & "  public.sokp_piutang " _
                                 & "  inner join public.so_mstr on so_oid = sokp_so_oid " _
                                 & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                                 & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                                     & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                 & "  and wf_user_id in (" + _user_approval + ")" _
                                 & "  and wf_iscurrent ~~* 'Y'"

                        .InitializeCommand()
                        .FillDataSet(ds_detail, "kartu_piutang")
                        gc_detail_kp.DataSource = ds_detail.Tables("kartu_piutang")
                        gv_detail_kp.BestFitColumns()

                        .SQL = "SELECT  " _
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
                            & " and so_en_id in (select user_en_id from tconfuserentity " _
                                               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and so_ar_cc_id in (select ccr_cc_id from ccr_restrc " _
                                               & " where ccr_user_id = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " inner join wf_mstr wm on wf_ref_oid = so_oid " _
                            & " where so_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and wf_user_id in (" + _user_approval + ")" _
                            & " and wf_iscurrent ~~* 'Y'"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()

                    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
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
                            & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                            & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                            & "  sb_desc, " _
                            & "  cc_desc, " _
                            & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                            & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                            & "  tax_class.code_name as sod_tax_class_name, " _
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
                            & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                            & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and so_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "detail")
                        gc_detail.DataSource = ds_detail.Tables("detail")
                        gv_detail.BestFitColumns()

                        .SQL = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                              " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                              " wf_iscurrent, wf_seq " + _
                              " from wf_mstr w " + _
                              " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                              " inner join so_mstr on so_code = wf_ref_code " + _
                              " inner join sod_det dt on dt.sod_so_oid = so_oid " _
                            & " where wf_ref_code in (select so_code from so_mstr inner join wf_mstr on wf_ref_code = so_code " _
                                                  & " where so_en_id in (select user_en_id from tconfuserentity " _
                                                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                                                  & " and wf_user_id in (" + _user_approval + "))" _
                            & " and so_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " order by wf_ref_code, wf_seq "
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "wf")
                        gc_wf.DataSource = ds_detail.Tables("wf")
                        gv_wf.BestFitColumns()

                        .SQL = "SELECT  " _
                            & "  soa_oid, " _
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
                            & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                            & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and so_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_detail, "customer")
                        gc_detail_attr.DataSource = ds_detail.Tables("customer")
                        gv_detail_attr.BestFitColumns()



                        .SQL = "SELECT  " _
                             & "  sokp_oid, " _
                             & "  sokp_so_oid, " _
                             & "  sokp_seq, " _
                             & "  sokp_ref, " _
                             & "  sokp_amount, " _
                             & "  sokp_amount_pay, " _
                             & "  sokp_description " _
                             & "FROM  " _
                             & "  public.sokp_piutang " _
                             & "  inner join public.so_mstr on so_oid = sokp_so_oid " _
                            & "  inner join wf_mstr wm on wf_ref_oid = so_oid " _
                            & "  where so_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and so_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & "  and wf_user_id in (" + _user_approval + ")"

                        .InitializeCommand()
                        .FillDataSet(ds_detail, "kartu_piutang")
                        gc_detail_kp.DataSource = ds_detail.Tables("kartu_piutang")
                        gv_detail_kp.BestFitColumns()



                        .SQL = "SELECT  " _
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
                            & " inner join wf_mstr wm on wf_ref_oid = so_oid " _
                            & " where so_en_id in (select user_en_id from tconfuserentity " _
                                                 & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & " and so_date >= " + SetDate(pr_txttglawal.DateTime) _
                            & " and so_date <= " + SetDate(pr_txttglakhir.DateTime) _
                            & " and wf_user_id in (" + _user_approval + ")"
                        .InitializeCommand()
                        .FillDataSet(ds_detail, "email")
                        gc_email.DataSource = ds_detail.Tables("email")
                        gv_email.BestFitColumns()
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        If xtc_master.SelectedTabPageIndex = 0 Then
            Try
                gv_detail.Columns("sod_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sod_so_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("so_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_detail_attr.Columns("soa_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soa_so_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("so_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_detail_kp.Columns("sokp_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sokp_so_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("so_oid").ToString & "'")
                gv_detail_kp.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("so_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("so_oid='" & ds.Tables("os").Rows(BindingContext(ds.Tables("os")).Position).Item("so_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            Try
                gv_detail.Columns("sod_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sod_so_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("so_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_detail_attr.Columns("soa_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("soa_so_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("so_oid").ToString & "'")
                gv_detail.BestFitColumns()

                gv_detail_kp.Columns("sokp_so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("sokp_so_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("so_oid").ToString & "'")
                gv_detail_kp.BestFitColumns()

                gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("so_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("so_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("so_oid='" & ds.Tables("all").Rows(BindingContext(ds.Tables("all")).Position).Item("so_oid").ToString & "'")
                gv_email.BestFitColumns()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub xtc_master_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xtc_master.Click
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        End If
    End Sub

    Private Sub sb_process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_process.Click
        Dim i, j As Integer
        j = 0

        If xtc_master.SelectedTabPageIndex = 0 Then
            If ds.Tables.Count = 0 Then
                Exit Sub
            ElseIf ds.Tables("os").Rows.Count = 0 Then
                Exit Sub
            End If

            For i = 0 To ds.Tables("os").Rows.Count - 1
                If ds.Tables("os").Rows(i).Item("status") = True Then
                    j += 1
                End If
            Next

            If j = 0 Then
                MessageBox.Show("Please Select Data, First...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        Dim _colom, _table, _initial, _type, _title As String

        _colom = "so_trans_id"
        _table = "so_mstr"
        _initial = "so"
        _type = "so"
        _title = "Sales Order"

        If le_wfs_status.EditValue = 1 And xtc_master.SelectedTabPageIndex = 0 Then
            approve_wf(_colom, _table, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, gv_email, _title)
        ElseIf le_wfs_status.EditValue = 2 And xtc_master.SelectedTabPageIndex = 0 Then
            hold_wf(_initial, le_wfs_status.EditValue, Trim(te_remark.Text), nud_hold_day.Value, _user_approval, ds)
        ElseIf le_wfs_status.EditValue = 3 And xtc_master.SelectedTabPageIndex = 0 Then
            cancel_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        ElseIf le_wfs_status.EditValue = 4 And xtc_master.SelectedTabPageIndex = 0 Then
            rollback_wf(_table, _colom, _initial, _type, le_wfs_status.EditValue, Trim(te_remark.Text), _user_approval, ds, _title)
        End If
    End Sub

    Private Sub le_wfs_status_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles le_wfs_status.EditValueChanged
        If le_wfs_status.EditValue = 2 Then
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lci_hold_day.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

   
End Class
