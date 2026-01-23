Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls

Public Class FCalculateCost
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ro_oid_mstr As String
    Dim _calc_code As String
    Dim ds_edit As DataSet
    Dim sSQLs As New ArrayList

    Private Sub FRouting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible

    End Sub
    Public Sub get_data(ByVal par_code As String)
        Try
            Dim _oid As String = ""
            Dim sql As String = "SELECT  " _
                & "  a.calc_oid, " _
                & "  a.calc_code, " _
                & "  a.calc_en_id, " _
                & "  b.en_desc, " _
                & "  a.calc_date, " _
                & "  a.calc_judul, " _
                & "  a.calc_jns_buku, " _
                & "  a.calc_remarks, " _
                & "  a.calc_oplah, " _
                & "  a.calc_ukuran, " _
                & "  a.calc_biaya_produksi, " _
                & "  a.calc_biaya_per_buku, " _
                & "  a.calc_panjang, " _
                & "  a.calc_lebar, " _
                & "  a.calc_berat, " _
                & "  a.calc_isi_jml, " _
                & "  a.calc_isi_opsi, " _
                & "  a.calc_isi_bahan, " _
                & "  a.calc_isi_bhn_qty, " _
                & "  a.calc_isi_panjang, " _
                & "  a.calc_isi_lebar, " _
                & "  a.calc_isi_jml_warna, " _
                & "  a.calc_isi_insheet, " _
                & "  a.calc_isi_naik_cetak, " _
                & "  a.calc_jns_mesin, " _
                & "  a.calc_isi_kbthn_bahan, " _
                & "  a.calc_isi_nilai, " _
                & "  a.calc_isi_outsource_opsi, " _
                & "  a.calc_isi_outsource_nilai, " _
                & "  a.calc_sb_jml_design, " _
                & "  a.calc_sb_opsi, " _
                & "  a.calc_sb_panjang, " _
                & "  a.calc_sb_lebar, " _
                & "  a.calc_sb_jns_bahan, " _
                & "  a.calc_sb_bahan_qty, " _
                & "  a.calc_sb_bhn_panjang, " _
                & "  a.calc_sb_bhn_lebar, " _
                & "  a.calc_sb_jml_warna, " _
                & "  a.calc_sb_insheet, " _
                & "  a.calc_sb_naik_cetak, " _
                & "  a.calc_sb_berat, " _
                & "  a.calc_sb_bhn_qty, " _
                & "  a.calc_sb_jns_mesin, " _
                & "  a.calc_sb_nilai, " _
                & "  a.calc_sb_outsource_opsi, " _
                & "  a.calc_sb_outsource_nilai, " _
                & "  a.calc_cv_jml_design, " _
                & "  a.calc_cv_opsi, " _
                & "  a.calc_cv_panjang, " _
                & "  a.calc_cv_lebar, " _
                & "  a.calc_cv_jns_bahan, " _
                & "  a.calc_cv_bahan_qty, " _
                & "  a.calc_cv_bhn_panjang, " _
                & "  a.calc_cv_bhn_lebar, " _
                & "  a.calc_cv_jml_warna, " _
                & "  a.calc_cv_insheet, " _
                & "  a.calc_cv_naik_cetak, " _
                & "  a.calc_cv_berat, " _
                & "  a.calc_cv_kbthn_bhn_qty, " _
                & "  a.calc_cv_jns_mesin, " _
                & "  a.calc_cv_nilai, " _
                & "  a.calc_cv_outsource_opsi, " _
                & "  a.calc_cv_outsource_nilai, " _
                & "  a.calc_kr_jns_bahan, " _
                & "  a.calc_kr_jns_bhn_qty, " _
                & "  a.calc_kr_opsi, " _
                & "  a.calc_kr_bhn_panjang, " _
                & "  a.calc_kr_bhn_lebar, " _
                & "  a.calc_kr_insheet, " _
                & "  a.calc_kr_bhn_qty, " _
                & "  a.calc_kr_nilai, " _
                & "  a.calc_kr_outsource_opsi, " _
                & "  a.calc_kr_outsource_nilai, " _
                & "  a.calc_pra_opsi, " _
                & "  a.calc_opsi_isi, " _
                & "  a.calc_biaya_produksi2, " _
                & "  a.calc_margin, " _
                & "  a.calc_nilai_margin, " _
                & "  a.calc_biaya_produksi_nilai_margin, " _
                & "  a.calc_ppn, " _
                & "  a.calc_nilai_ppn, " _
                & "  a.calc_biaya_produksi_nilai_margin_nilai_ppn, " _
                & "  a.calc_biaya_cetak_pcs, " _
                & "  a.calc_harga_jaket, " _
                & "  a.calc_margin_jaket, " _
                & "  a.calc_nilai_jaket, " _
                & "  a.calc_biaya_cetak_final,calc_add_by,calc_add_date,calc_upd_by,calc_upd_date  " _
                & "FROM " _
                & "  public.calc_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.calc_en_id = b.en_id) " _
                & "WHERE " _
                & "  a.calc_code = " & SetSetring(par_code) & " "

            Dim dt_get As New DataTable
            dt_get = GetTableData(sql)

            For Each dr As DataRow In dt_get.Rows

                _oid = dr("calc_oid").ToString
                calc_judul.EditValue = dr("calc_judul")
                calc_jns_buku.EditValue = dr("calc_jns_buku")
                calc_remarks.EditValue = dr("calc_remarks")
                calc_oplah.EditValue = dr("calc_oplah")
                calc_ukuran.EditValue = dr("calc_ukuran")

                calc_panjang.EditValue = dr("calc_panjang")
                calc_lebar.EditValue = dr("calc_lebar")
                calc_berat.EditValue = dr("calc_berat")



                calc_isi_jml.EditValue = dr("calc_isi_jml")
                calc_isi_opsi.EditValue = dr("calc_isi_opsi")
                calc_isi_bahan.EditValue = dr("calc_isi_bahan")
                calc_isi_bhn_qty.EditValue = dr("calc_isi_bhn_qty")
                'Dim xx As Double = 0.0
                'xx = dr("calc_isi_panjang")
                calc_isi_panjang.EditValue = dr("calc_isi_panjang")
                calc_isi_lebar.EditValue = dr("calc_isi_lebar")
                calc_isi_naik_cetak.EditValue = dr("calc_isi_naik_cetak")
                calc_isi_jml_warna.EditValue = dr("calc_isi_jml_warna")
                calc_isi_insheet.EditValue = dr("calc_isi_insheet")
                calc_jns_mesin.EditValue = dr("calc_jns_mesin")
                calc_isi_outsource_opsi.EditValue = dr("calc_isi_outsource_opsi")


                calc_sb_jml_design.EditValue = dr("calc_sb_jml_design")
                calc_sb_opsi.EditValue = dr("calc_sb_opsi")
                calc_sb_panjang.EditValue = dr("calc_sb_panjang")
                calc_sb_lebar.EditValue = dr("calc_sb_lebar")
                calc_sb_jns_bahan.EditValue = dr("calc_sb_jns_bahan")
                calc_sb_bahan_qty.EditValue = dr("calc_sb_bahan_qty")
                calc_sb_bhn_panjang.EditValue = dr("calc_sb_bhn_panjang")
                calc_sb_bhn_lebar.EditValue = dr("calc_sb_bhn_lebar")
                calc_sb_jml_warna.EditValue = dr("calc_sb_jml_warna")
                calc_sb_insheet.EditValue = dr("calc_sb_insheet")
                calc_sb_naik_cetak.EditValue = dr("calc_sb_naik_cetak")
                calc_sb_berat.EditValue = dr("calc_sb_berat")
                'calc_sb_bhn_qty.EditValue = dr("calc_sb_bhn_qty")
                calc_sb_jns_mesin.EditValue = dr("calc_sb_jns_mesin")


                calc_cv_jml_design.EditValue = dr("calc_cv_jml_design")
                calc_cv_opsi.EditValue = dr("calc_cv_opsi")
                calc_cv_panjang.EditValue = dr("calc_cv_panjang")
                calc_cv_lebar.EditValue = dr("calc_cv_lebar")
                calc_cv_jns_bahan.EditValue = dr("calc_cv_jns_bahan")

                calc_cv_bahan_qty.EditValue = dr("calc_cv_bahan_qty")
                calc_cv_bhn_panjang.EditValue = dr("calc_cv_bhn_panjang")
                calc_cv_bhn_lebar.EditValue = dr("calc_cv_bhn_lebar")
                calc_cv_jml_warna.EditValue = dr("calc_cv_jml_warna")
                calc_cv_insheet.EditValue = dr("calc_cv_insheet")
                calc_cv_naik_cetak.EditValue = dr("calc_cv_naik_cetak")
                calc_cv_jns_mesin.EditValue = dr("calc_cv_jns_mesin")

                calc_kr_jns_bahan.EditValue = dr("calc_kr_jns_bahan")
                calc_kr_jns_bhn_qty.EditValue = dr("calc_kr_jns_bhn_qty")
                calc_kr_opsi.EditValue = dr("calc_kr_opsi")
                calc_kr_bhn_panjang.EditValue = dr("calc_kr_bhn_panjang")
                calc_kr_bhn_lebar.EditValue = dr("calc_kr_bhn_lebar")
                calc_kr_insheet.EditValue = dr("calc_kr_insheet")
                calc_kr_bhn_qty.EditValue = dr("calc_kr_bhn_qty")
                calc_kr_nilai.EditValue = dr("calc_kr_nilai")


                calc_kr_outsource_opsi.EditValue = dr("calc_kr_outsource_opsi")
                calc_kr_outsource_nilai.EditValue = dr("calc_kr_outsource_nilai")
                calc_pra_opsi.EditValue = dr("calc_pra_opsi")
                calc_opsi_isi.EditValue = dr("calc_opsi_isi")
                calc_biaya_produksi2.EditValue = dr("calc_biaya_produksi2")
                calc_margin.EditValue = dr("calc_margin")
                calc_nilai_margin.EditValue = dr("calc_nilai_margin")
                calc_biaya_produksi_nilai_margin.EditValue = dr("calc_biaya_produksi_nilai_margin")
                calc_ppn.EditValue = dr("calc_ppn")
                calc_nilai_ppn.EditValue = dr("calc_nilai_ppn")
                calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue = dr("calc_biaya_produksi_nilai_margin_nilai_ppn")
                calc_biaya_cetak_pcs.EditValue = dr("calc_biaya_cetak_pcs")
                calc_harga_jaket.EditValue = dr("calc_harga_jaket")
                calc_margin_jaket.EditValue = dr("calc_margin_jaket")
                calc_nilai_jaket.EditValue = dr("calc_nilai_jaket")
                calc_biaya_cetak_final.EditValue = dr("calc_biaya_cetak_final")
                calc_judul.EditValue = dr("calc_judul")





            Next


            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  a.cald_oid, " _
                                & "  a.calcd_calc_oid , " _
                                & "  a.calcd_cetak_oid as cetak_oid, " _
                                & "  a.calcd_cetak_item as cetak_item, " _
                                & "  a.calcd_cetak_group as cetak_group, " _
                                & "  a.calcd_cetak_order as cetak_order, " _
                                & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                & "  a.calcd_qty as qty, " _
                                & "  a.calcd_harga as harga, " _
                                & "  a.calcd_harga_kg as harga_kg, " _
                                & "  a.calcd_lebar as lebar, " _
                                & "  a.calcd_panjang as panjang, " _
                                & "  a.calcd_jml_potong as jml_potong, " _
                                & "  a.calcd_kg as kg, " _
                                & "  a.calcd_bop_btkl as bop_btkl, " _
                                & "  a.calcd_nilai as nilai, " _
                                & "  a.calcd_warna as warna, " _
                                & "  a.calcd_velt as velt, " _
                                & "  a.calcd_insheet as insheet, " _
                                & "  a.calcd_biaya_potong as biaya_potong, " _
                                & "  a.calcd_opsi as opsi, " _
                                & "  a.calcd_outsource as outsource, " _
                                & "  a.calcd_jenis as jenis, " _
                                & "  a.calcd_tipe  " _
                                & "FROM " _
                                & "  public.calcd_det a " _
                                & "WHERE " _
                                & "  a.calcd_calc_oid = '" & _oid & "'  AND  " _
                                & "  a.calcd_tipe = 'insert_edit' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")


                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        .SQL = "SELECT  " _
                                   & "  a.cald_oid, " _
                                    & "  a.calcd_calc_oid, " _
                                    & "  a.calcd_cetak_oid as cetak_oid, " _
                                    & "  a.calcd_cetak_item as cetak_item, " _
                                    & "  a.calcd_cetak_group as cetak_group, " _
                                    & "  a.calcd_cetak_order as cetak_order, " _
                                    & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                    & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                    & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                    & "  a.calcd_qty as qty, " _
                                    & "  a.calcd_harga as harga, " _
                                    & "  a.calcd_harga_kg as harga_kg, " _
                                    & "  a.calcd_lebar as lebar, " _
                                    & "  a.calcd_panjang as panjang, " _
                                    & "  a.calcd_jml_potong as jml_potong, " _
                                    & "  a.calcd_kg as kg, " _
                                    & "  a.calcd_bop_btkl as bop_btkl, " _
                                    & "  a.calcd_nilai as nilai, " _
                                    & "  a.calcd_warna as warna, " _
                                    & "  a.calcd_velt as velt, " _
                                    & "  a.calcd_insheet as insheet, " _
                                    & "  a.calcd_biaya_potong as biaya_potong, " _
                                    & "  a.calcd_opsi as opsi, " _
                                    & "  a.calcd_outsource as outsource, " _
                                    & "  a.calcd_jenis as jenis, " _
                                 & "  a.calcd_tipe " _
                                 & "FROM " _
                                 & "  public.calcd_det a " _
                                 & "WHERE " _
                                 & "  a.calcd_calc_oid = '" & _oid & "'  AND  " _
                                 & "  a.calcd_tipe = 'insert_edit_detail' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_detail")
                        gc_edit_cetak.DataSource = ds_edit.Tables(1)
                        gv_edit_cetak.BestFitColumns()


                        .SQL = "SELECT  " _
                              & "  a.cald_oid, " _
                                & "  a.calcd_calc_oid as calc_oid, " _
                                & "  a.calcd_cetak_oid as cetak_oid, " _
                                & "  a.calcd_cetak_item as cetak_item, " _
                                & "  a.calcd_cetak_group as cetak_group, " _
                                & "  a.calcd_cetak_order as cetak_order, " _
                                & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                & "  a.calcd_qty as qty, " _
                                & "  a.calcd_harga as harga, " _
                                & "  a.calcd_harga_kg as harga_kg, " _
                                & "  a.calcd_lebar as lebar, " _
                                & "  a.calcd_panjang as panjang, " _
                                & "  a.calcd_jml_potong as jml_potong, " _
                                & "  a.calcd_kg as kg, " _
                                & "  a.calcd_bop_btkl as bop_btkl, " _
                                & "  a.calcd_nilai as nilai, " _
                                & "  a.calcd_warna as warna, " _
                                & "  a.calcd_velt as velt, " _
                                & "  a.calcd_insheet as insheet, " _
                                & "  a.calcd_biaya_potong as biaya_potong, " _
                                & "  a.calcd_opsi as opsi, " _
                                & "  a.calcd_outsource as outsource, " _
                                & "  a.calcd_jenis as jenis, " _
                             & "  a.calcd_tipe " _
                             & "FROM " _
                             & "  public.calcd_det a " _
                             & "WHERE " _
                             & "  a.calcd_calc_oid = '" & _oid & "'  AND  " _
                             & "  a.calcd_tipe = 'insert_edit_calc' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_calc")
                        gc_edit_pasca.DataSource = ds_edit.Tables(2)
                        gv_edit_pasca.BestFitColumns()


                        .SQL = "SELECT  " _
                            & "  a.calcdt_oid, " _
                            & "  a.calcdt_calc_oid, " _
                            & "  a.calcdt_tambahan_kode tambahan_kode, " _
                            & "  a.calcdt_tambahan_desc tambahan_desc, " _
                            & "  a.calcdt_tambahan_value tambahan_value, " _
                            & "  a.calcdt_qty qty, " _
                            & "  a.calcdt_harga harga, " _
                            & "  a.calcdt_insheet insheet, " _
                            & "  a.calcdt_nilai nilai " _
                            & "FROM " _
                            & "  public.calcdt_tambahan a " _
                            & "WHERE " _
                            & "  a.calcdt_calc_oid = '" & _oid & "' " _
                            & "ORDER BY " _
                            & "  a.calcdt_tambahan_kode"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_tambahan")
                        gc_edit_tambahan.DataSource = ds_edit.Tables(3)
                        gv_edit_tambahan.BestFitColumns()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            hitung()
            'gc_edit_cetak.Update()
            ds_edit.AcceptChanges()
            'gv_edit_cetak_CellValueChanged(Nothing, Nothing)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Overrides Sub load_cb()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_en_mstr_mstr())
            calc_en_id.Properties.DataSource = dt_bantu
            calc_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            calc_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            calc_en_id.ItemIndex = 0

            'dt_bantu = New DataTable
            'dt_bantu = (func_data.load_periode_mstr_se())
            'With calc_jns_buku
            '    If .Properties.Columns.VisibleCount = 0 Then
            '        .Properties.Columns.Add(New LookUpColumnInfo("seperiode_code", "Code", 20))
            '        .Properties.Columns.Add(New LookUpColumnInfo("seperiode_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            '        .Properties.Columns.Add(New LookUpColumnInfo("seperiode_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            '        '.Properties.Columns.Add(New LookUpColumnInfo("seperiode_bonus_gen", "Generate Bonus", 20))
            '        '.Properties.Columns.Add(New LookUpColumnInfo("seperiode_payment_date", "Payment Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            '        .Properties.Columns.Add(New LookUpColumnInfo("seperiode_remarks", "Remarks", 20))
            '    End If

            '    .Properties.DataSource = dt_bantu
            '    .Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
            '    .Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
            '    If dt_bantu.Rows.Count > 0 Then
            '        .EditValue = dt_bantu.Rows(0).Item(.Properties.ValueMember)
            '        'segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
            '        'segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
            '        calc_remarks.EditValue = calc_jns_buku.GetColumnValue("seperiode_remarks")
            '    End If

            '    .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            '    .Properties.BestFit()
            '    .Properties.DropDownRows = 12
            '    .Properties.PopupWidth = 600
            '    .ItemIndex = 0
            'End With



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tanggal", "calc_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Nomor", "calc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Judul", "calc_judul", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jenis Buku", "calc_jns_buku", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Keterangan", "calc_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Oplah", "calc_oplah", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ukuran", "calc_ukuran", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Biaya Produksi", "calc_biaya_produksi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Biaya Per Buku", "calc_biaya_per_buku", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Panjang", "calc_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Lebar", "calc_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Berat", "calc_berat", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Jumlah Isi", "calc_isi_jml", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Opsi Isi", "calc_isi_opsi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bahan Isi", "calc_isi_bahan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jumlah Bahan Isi", "calc_isi_bhn_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Panjang", "calc_isi_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Lebar", "calc_isi_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Jml Warna", "calc_isi_jml_warna", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Insheet", "calc_isi_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Naik Cetak", "calc_isi_naik_cetak", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Jenis Mesin", "calc_jns_mesin", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Isi Kebutuhan Bahan", "calc_isi_kbthn_bahan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Isi Opsi Outsource", "calc_isi_outsource_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Isi Outsource Nilai", "calc_isi_outsource_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Nilai", "calc_isi_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Jml Design", "calc_sb_jml_design", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Opsi", "calc_sb_opsi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Panjang", "calc_sb_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Lebar", "calc_sb_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Jenis Bahan", "calc_sb_jns_bahan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Skiblat Jumlah Bahan", "calc_sb_bahan_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Panjang Bahan", "calc_sb_bhn_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Lebar Bahan", "calc_sb_bhn_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Jml Warna", "calc_sb_jml_warna", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Insheet", "calc_sb_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Naik Cetak", "calc_sb_naik_cetak", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Berat", "calc_sb_berat", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Qty Bahan", "calc_sb_bhn_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Jenis Mesin", "calc_sb_jns_mesin", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Skiblat Opsi Outsource", "calc_sb_outsource_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Skiblat Outsource", "calc_sb_outsource_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Skiblat Nilai", "calc_sb_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Jml Design", "calc_cv_jml_design", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Opsi", "calc_cv_opsi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_copy(gv_master, "Cover Panjang", "calc_cv_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Lebar", "calc_cv_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Jenis Bahan", "calc_cv_jns_bahan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cover Qty Bahan", "calc_cv_bahan_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Panjang Bahan", "calc_cv_bhn_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Lebar Bahan", "calc_cv_bhn_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Jml Warna", "calc_cv_jml_warna", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Insheet", "calc_cv_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Naik Cetak", "calc_cv_naik_cetak", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Berat", "calc_cv_berat", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Kebutuhan Bahan", "calc_cv_kbthn_bhn_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Jns Mesin", "calc_cv_jns_mesin", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cover Opsi Outsource", "calc_cv_outsource_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cover Outsource", "calc_cv_outsource_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cover Nilai", "calc_cv_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Karton Jenis Bahan", "calc_kr_jns_bahan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Karton Jml Bahan", "calc_kr_jns_bhn_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Karton Opsi", "calc_kr_opsi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Karton Bahan Panjang", "calc_kr_bhn_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Karton Bahan Lebar", "calc_kr_bhn_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Karton Opsi Outsource", "calc_kr_outsource_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Karton Outsource", "calc_kr_outsource_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Karton Nilai", "calc_kr_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Opsi Pracetak", "calc_pra_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Opsi Isi", "calc_opsi_isi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Biaya Produksi", "calc_biaya_produksi2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Margin", "calc_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Nilai Margin", "calc_nilai_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Biaya Produksi + Margin", "calc_biaya_produksi_nilai_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "PPN", "calc_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Nilai PPN", "calc_nilai_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Biaya Produksi + Margin + PPN", "calc_biaya_produksi_nilai_margin_nilai_ppn", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Biaya Cetal / Pcs", "calc_biaya_cetak_pcs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Harga Jaket", "calc_harga_jaket", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Margin Jaket", "calc_margin_jaket", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Nilai Jaket", "calc_nilai_jaket", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Biaya Cetak Final", "calc_biaya_cetak_final", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "calc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "calc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "calc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "calc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_edit, "cetak_oid", False)
        add_column(gv_edit, "cetak_group", False)
        add_column(gv_edit, "cetak_sub_group", False)
        add_column_copy(gv_edit, "Kelompok", "cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Item", "cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Opsi", "opsi", DevExpress.Utils.HorzAlignment.Default, init_le_repo("ya_tidak"))
        add_column_copy(gv_edit, "Qty", "qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Harga", "harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Panjang", "panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Lebar", "lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Outsource", "outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Total", "nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_cetak, "Kelompok", "cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_cetak, "Item", "cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cetak, "Opsi", "opsi", DevExpress.Utils.HorzAlignment.Default, init_le_repo("ya_tidak"))
        add_column_edit(gv_edit_cetak, "Jenis", "jenis", DevExpress.Utils.HorzAlignment.Default, init_le_repo("opsi_detail_cetak"))
        add_column_copy(gv_edit_cetak, "Qty", "qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cetak, "Velt", "velt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_cetak, "Jml Potong", "jml_potong", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_cetak, "Biaya Potong", "biaya_potong", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cetak, "Insheet", "insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cetak, "Warna %", "warna", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_cetak, "Kg", "kg", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cetak, "Harga /kg", "harga_kg", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_cetak, "Outsource", "outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_cetak, "Total", "nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_pasca, "Kelompok", "cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_pasca, "Item", "cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_pasca, "Opsi", "opsi", DevExpress.Utils.HorzAlignment.Default, init_le_repo("ya_tidak"))
        add_column_edit(gv_edit_pasca, "Jenis", "jenis", DevExpress.Utils.HorzAlignment.Default, init_le_repo("opsi_detail_pasca"))
        add_column_copy(gv_edit_pasca, "Qty", "qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_pasca, "Velt", "velt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_pasca, "Harga", "harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_pasca, "Ukuran", "lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_pasca, "BOP BTKL", "bop_btkl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pasca, "Insheet", "insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_pasca, "Outsource", "outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_pasca, "Total", "nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_edit_tambahan, "Item", "tambahan_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit_tambahan, "%", "tambahan_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_tambahan, "Qty", "qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_tambahan, "Harga", "harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_tambahan, "Insheet", "insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit_tambahan, "Total", "nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column(gv_detail, "calcd_cetak_oid", False)
        add_column(gv_detail, "calcd_cetak_group", False)
        add_column(gv_detail, "calcd_cetak_sub_group", False)
        add_column_copy(gv_detail, "Kelompok", "calcd_cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Item", "calcd_cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Opsi", "calcd_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "calcd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Harga", "calcd_harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Panjang", "calcd_panjang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Lebar", "calcd_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Outsource", "calcd_outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Total", "calcd_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


        add_column_copy(gv_detail_cetak, "Kelompok", "calcd_cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cetak, "Item", "calcd_cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cetak, "Opsi", "calcd_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cetak, "Jenis", "jenis", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cetak, "Qty", "calcd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Velt", "calcd_velt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Jml Potong", "calcd_jml_potong", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Biaya Potong", "calcd_biaya_potong", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Insheet", "calcd_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Warna %", "calcd_warna", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Kg", "calcd_kg", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_cetak, "Harga /kg", "calcd_harga_kg", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_cetak, "Outsource", "calcd_outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_cetak, "Total", "calcd_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        'gv_detail_cetak
        'gv_detail_pasca

        add_column_copy(gv_detail_pasca, "Kelompok", "calcd_cetak_sub_group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pasca, "Item", "calcd_cetak_item", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_pasca, "Opsi", "calcd_opsi", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_detail_pasca, "Jenis", "calcd_jenis", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_pasca, "Qty", "calcd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pasca, "Velt", "calcd_velt", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pasca, "Harga", "calcd_harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pasca, "Ukuran", "calcd_lebar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pasca, "BOP BTKL", "calcd_bop_btkl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_pasca, "Insheet", "calcd_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_pasca, "Outsource", "calcd_outsource", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_pasca, "Total", "calcd_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_detail_tambahan, "Item", "calcdt_tambahan_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_tambahan, "%", "calcdt_tambahan_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_tambahan, "Qty", "calcdt_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_tambahan, "Harga", "calcdt_harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_detail_tambahan, "Insheet", "calcdt_insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_tambahan, "Total", "calcdt_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")



        'init_le(calc_jns_buku, "SELECT  " _
        '            & "  a.jns_buku " _
        '            & "FROM " _
        '            & "  public.cetak_jns_buku a", "jns_buku", "Pilihan", True)
        init_le(calc_jns_buku, "jenis_buku")
        'init_le(calc_ukuran, "SELECT  " _
        '            & "  a.spek_ukuran " _
        '            & "FROM " _
        '            & "  public.cetak_spec_buku a " _
        '            & "ORDER BY " _
        '            & "  a.spek_number", "spek_ukuran", "Pilihan", True)
        init_le(calc_opsi_isi, "opsi_isi")
        init_le(calc_ukuran, "spek_buku")

        init_le(calc_isi_opsi, "ya_tidak")
        init_le(calc_sb_opsi, "ya_tidak")
        init_le(calc_cv_opsi, "ya_tidak")
        init_le(calc_kr_opsi, "ya_tidak")


        init_le(calc_isi_bahan, "SELECT  " _
                    & "  a.isi_jenis " _
                    & "FROM " _
                    & "  public.cetak_spec_isi a where a.isi_jenis IS NOT NULL order by isi_jenis", "isi_jenis", "Pilihan", True)

        init_le(calc_isi_bhn_qty, "SELECT  " _
                   & "  a.isi_gramasi " _
                   & "FROM " _
                   & "  public.cetak_spec_isi a where a.isi_gramasi IS NOT NULL order by isi_gramasi", "isi_gramasi", "Pilihan", True)

        init_le(calc_isi_panjang, "SELECT  " _
                  & "  a.isi_panjang " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a  where a.isi_panjang IS NOT NULL order by isi_panjang", "isi_panjang", "Pilihan", True)

        init_le(calc_isi_lebar, "SELECT  " _
                  & "  a.isi_lebar " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a   where a.isi_lebar IS NOT NULL order by isi_lebar", "isi_lebar", "Pilihan", True)

        init_le(calc_jns_mesin, "SELECT  " _
                & "  a.plate " _
                & "FROM " _
                & "  public.cetak_plate a where a.plate IS NOT NULL order by plate", "plate", "Pilihan", True)

        '==========
        init_le(calc_sb_jns_bahan, "SELECT  " _
                  & "  a.isi_jenis " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_jenis IS NOT NULL order by isi_jenis", "isi_jenis", "Pilihan", True)

        init_le(calc_sb_bahan_qty, "SELECT  " _
                  & "  a.isi_gramasi " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_gramasi IS NOT NULL order by isi_gramasi", "isi_gramasi", "Pilihan", True)

        init_le(calc_sb_bhn_panjang, "SELECT  " _
                  & "  a.isi_panjang " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_panjang IS NOT NULL order by isi_panjang", "isi_panjang", "Pilihan", True)

        init_le(calc_sb_bhn_lebar, "SELECT  " _
                  & "  a.isi_lebar " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_lebar IS NOT NULL order by isi_lebar", "isi_lebar", "Pilihan", True)

        init_le(calc_sb_jns_mesin, "SELECT  " _
                & "  a.plate " _
                & "FROM " _
                & "  public.cetak_plate a where a.plate IS NOT NULL order by plate", "plate", "Pilihan", True)



        init_le(calc_cv_jns_bahan, "SELECT  " _
                 & "  a.isi_jenis " _
                 & "FROM " _
                 & "  public.cetak_spec_isi a where a.isi_jenis IS NOT NULL order by isi_jenis", "isi_jenis", "Pilihan", True)

        init_le(calc_cv_bahan_qty, "SELECT  " _
                  & "  a.isi_gramasi " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_gramasi IS NOT NULL order by isi_gramasi", "isi_gramasi", "Pilihan", True)

        init_le(calc_cv_bhn_panjang, "SELECT  " _
                  & "  a.isi_panjang " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_panjang IS NOT NULL order by isi_panjang", "isi_panjang", "Pilihan", True)

        init_le(calc_cv_bhn_lebar, "SELECT  " _
                  & "  a.isi_lebar " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_lebar IS NOT NULL order by isi_lebar", "isi_lebar", "Pilihan", True)

        init_le(calc_cv_jns_mesin, "SELECT  " _
                & "  a.plate " _
                & "FROM " _
                & "  public.cetak_plate a where a.plate IS NOT NULL order by plate", "plate", "Pilihan", True)



        init_le(calc_kr_jns_bahan, "SELECT  " _
                & "  a.isi_jenis " _
                & "FROM " _
                & "  public.cetak_spec_isi a where a.isi_jenis IS NOT NULL order by isi_jenis", "isi_jenis", "Pilihan", True)

        init_le(calc_kr_jns_bhn_qty, "SELECT  " _
                  & "  a.isi_gramasi " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_gramasi IS NOT NULL order by isi_gramasi", "isi_gramasi", "Pilihan", True)

        init_le(calc_kr_bhn_panjang, "SELECT  " _
                  & "  a.isi_panjang " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_panjang IS NOT NULL order by isi_panjang", "isi_panjang", "Pilihan", True)

        init_le(calc_kr_bhn_lebar, "SELECT  " _
                  & "  a.isi_lebar " _
                  & "FROM " _
                  & "  public.cetak_spec_isi a where a.isi_lebar IS NOT NULL order by isi_lebar", "isi_lebar", "Pilihan", True)

        init_le(calc_pra_opsi, "select opsi from public.cetak_opsi", "opsi", "Pilihan", True)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.calc_oid, " _
                & "  a.calc_code, " _
                & "  a.calc_en_id, " _
                & "  b.en_desc, " _
                & "  a.calc_date, " _
                & "  a.calc_judul, " _
                & "  a.calc_jns_buku, " _
                & "  a.calc_remarks, " _
                & "  a.calc_oplah, " _
                & "  a.calc_ukuran, " _
                & "  a.calc_biaya_produksi, " _
                & "  a.calc_biaya_per_buku, " _
                & "  a.calc_panjang, " _
                & "  a.calc_lebar, " _
                & "  a.calc_berat, " _
                & "  a.calc_isi_jml, " _
                & "  a.calc_isi_opsi, " _
                & "  a.calc_isi_bahan, " _
                & "  a.calc_isi_bhn_qty, " _
                & "  a.calc_isi_panjang, " _
                & "  a.calc_isi_lebar, " _
                & "  a.calc_isi_jml_warna, " _
                & "  a.calc_isi_insheet, " _
                & "  a.calc_isi_naik_cetak, " _
                & "  a.calc_jns_mesin, " _
                & "  a.calc_isi_kbthn_bahan, " _
                & "  a.calc_isi_nilai, " _
                & "  a.calc_isi_outsource_opsi, " _
                & "  a.calc_isi_outsource_nilai, " _
                & "  a.calc_sb_jml_design, " _
                & "  a.calc_sb_opsi, " _
                & "  a.calc_sb_panjang, " _
                & "  a.calc_sb_lebar, " _
                & "  a.calc_sb_jns_bahan, " _
                & "  a.calc_sb_bahan_qty, " _
                & "  a.calc_sb_bhn_panjang, " _
                & "  a.calc_sb_bhn_lebar, " _
                & "  a.calc_sb_jml_warna, " _
                & "  a.calc_sb_insheet, " _
                & "  a.calc_sb_naik_cetak, " _
                & "  a.calc_sb_berat, " _
                & "  a.calc_sb_bhn_qty, " _
                & "  a.calc_sb_jns_mesin, " _
                & "  a.calc_sb_nilai, " _
                & "  a.calc_sb_outsource_opsi, " _
                & "  a.calc_sb_outsource_nilai, " _
                & "  a.calc_cv_jml_design, " _
                & "  a.calc_cv_opsi, " _
                & "  a.calc_cv_panjang, " _
                & "  a.calc_cv_lebar, " _
                & "  a.calc_cv_jns_bahan, " _
                & "  a.calc_cv_bahan_qty, " _
                & "  a.calc_cv_bhn_panjang, " _
                & "  a.calc_cv_bhn_lebar, " _
                & "  a.calc_cv_jml_warna, " _
                & "  a.calc_cv_insheet, " _
                & "  a.calc_cv_naik_cetak, " _
                & "  a.calc_cv_berat, " _
                & "  a.calc_cv_kbthn_bhn_qty, " _
                & "  a.calc_cv_jns_mesin, " _
                & "  a.calc_cv_nilai, " _
                & "  a.calc_cv_outsource_opsi, " _
                & "  a.calc_cv_outsource_nilai, " _
                & "  a.calc_kr_jns_bahan, " _
                & "  a.calc_kr_jns_bhn_qty, " _
                & "  a.calc_kr_opsi, " _
                & "  a.calc_kr_bhn_panjang, " _
                & "  a.calc_kr_bhn_lebar, " _
                & "  a.calc_kr_insheet, " _
                & "  a.calc_kr_bhn_qty, " _
                & "  a.calc_kr_nilai, " _
                & "  a.calc_kr_outsource_opsi, " _
                & "  a.calc_kr_outsource_nilai, " _
                & "  a.calc_pra_opsi, " _
                & "  a.calc_opsi_isi, " _
                & "  a.calc_biaya_produksi2, " _
                & "  a.calc_margin, " _
                & "  a.calc_nilai_margin, " _
                & "  a.calc_biaya_produksi_nilai_margin, " _
                & "  a.calc_ppn, " _
                & "  a.calc_nilai_ppn, " _
                & "  a.calc_biaya_produksi_nilai_margin_nilai_ppn, " _
                & "  a.calc_biaya_cetak_pcs, " _
                & "  a.calc_harga_jaket, " _
                & "  a.calc_margin_jaket, " _
                & "  a.calc_nilai_jaket,pt_code,pt_desc1,calc_pt_id, " _
                & "  a.calc_biaya_cetak_final,calc_add_by,calc_add_date,calc_upd_by,calc_upd_date  " _
                & "FROM " _
                & "  public.calc_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.calc_en_id = b.en_id) " _
                & "  left outer JOIN public.pt_mstr c ON (a.calc_pt_id = c.pt_id) " _
                & "WHERE " _
                & "  a.calc_date BETWEEN " & SetDate(pr_txttglawal.Text) & " AND " & SetDate(pr_txttglakhir.Text) & "  " _
                & " and calc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "ORDER BY " _
                & "  a.calc_code"



        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables.Count = 0 Then
            Exit Sub
        End If

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If


        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                & "  a.cald_oid, " _
                & "  a.calcd_calc_oid, " _
                & "  a.calcd_cetak_oid, " _
                & "  a.calcd_cetak_item, " _
                & "  a.calcd_cetak_group, " _
                & "  a.calcd_cetak_order, " _
                & "  a.calcd_cetak_sub_group, " _
                & "  a.calcd_cetak_sub_order, " _
                & "  a.calcd_cetak_sub_group_name, " _
                & "  a.calcd_qty, " _
                & "  a.calcd_harga, " _
                & "  a.calcd_harga_kg, " _
                & "  a.calcd_lebar, " _
                & "  a.calcd_panjang, " _
                & "  a.calcd_jml_potong, " _
                & "  a.calcd_kg, " _
                & "  a.calcd_bop_btkl, " _
                & "  a.calcd_nilai, " _
                & "  a.calcd_warna, " _
                & "  a.calcd_velt, " _
                & "  a.calcd_insheet, " _
                & "  a.calcd_biaya_potong, " _
                & "  a.calcd_opsi, " _
                & "  a.calcd_outsource, " _
                & "  a.calcd_jenis, " _
                & "  a.calcd_tipe " _
                & "FROM " _
                & "  public.calcd_det a " _
                & "WHERE " _
                & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                & "  a.calcd_tipe = 'insert_edit' order by calcd_cetak_order, calcd_cetak_sub_order "

        load_data_detail(sql, gc_detail, "detail")
        gv_detail.BestFitColumns()

        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible



        Try
            ds.Tables("detail_trx").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
                 & "  a.cald_oid, " _
                 & "  a.calcd_calc_oid, " _
                 & "  a.calcd_cetak_oid, " _
                 & "  a.calcd_cetak_item, " _
                 & "  a.calcd_cetak_group, " _
                 & "  a.calcd_cetak_order, " _
                 & "  a.calcd_cetak_sub_group, " _
                 & "  a.calcd_cetak_sub_order, " _
                 & "  a.calcd_cetak_sub_group_name, " _
                 & "  a.calcd_qty, " _
                 & "  a.calcd_harga, " _
                 & "  a.calcd_harga_kg, " _
                 & "  a.calcd_lebar, " _
                 & "  a.calcd_panjang, " _
                 & "  a.calcd_jml_potong, " _
                 & "  a.calcd_kg, " _
                 & "  a.calcd_bop_btkl, " _
                 & "  a.calcd_nilai, " _
                 & "  a.calcd_warna, " _
                 & "  a.calcd_velt, " _
                 & "  a.calcd_insheet, " _
                 & "  a.calcd_biaya_potong, " _
                 & "  a.calcd_opsi, " _
                 & "  a.calcd_outsource, " _
                 & "  a.calcd_jenis, " _
                 & "  a.calcd_tipe " _
                 & "FROM " _
                 & "  public.calcd_det a " _
                 & "WHERE " _
                 & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                 & "  a.calcd_tipe = 'insert_edit_detail' order by calcd_cetak_order, calcd_cetak_sub_order "

        load_data_detail(sql, gc_detail_pasca, "detail_trx")
        gv_detail_pasca.BestFitColumns()



        Try
            ds.Tables("detail_calc").Clear()
        Catch ex As Exception
        End Try
        sql = "SELECT  " _
                 & "  a.cald_oid, " _
                 & "  a.calcd_calc_oid, " _
                 & "  a.calcd_cetak_oid, " _
                 & "  a.calcd_cetak_item, " _
                 & "  a.calcd_cetak_group, " _
                 & "  a.calcd_cetak_order, " _
                 & "  a.calcd_cetak_sub_group, " _
                 & "  a.calcd_cetak_sub_order, " _
                 & "  a.calcd_cetak_sub_group_name, " _
                 & "  a.calcd_qty, " _
                 & "  a.calcd_harga, " _
                 & "  a.calcd_harga_kg, " _
                 & "  a.calcd_lebar, " _
                 & "  a.calcd_panjang, " _
                 & "  a.calcd_jml_potong, " _
                 & "  a.calcd_kg, " _
                 & "  a.calcd_bop_btkl, " _
                 & "  a.calcd_nilai, " _
                 & "  a.calcd_warna, " _
                 & "  a.calcd_velt, " _
                 & "  a.calcd_insheet, " _
                 & "  a.calcd_biaya_potong, " _
                 & "  a.calcd_opsi, " _
                 & "  a.calcd_outsource, " _
                 & "  a.calcd_jenis, " _
                 & "  a.calcd_tipe " _
                 & "FROM " _
                 & "  public.calcd_det a " _
                 & "WHERE " _
                 & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                 & "  a.calcd_tipe = 'insert_edit_calc' order by calcd_cetak_order, calcd_cetak_sub_order "

        load_data_detail(sql, gc_detail_cetak, "insert_edit_detail")
        gv_detail_cetak.BestFitColumns()

        Try
            sql = "SELECT  " _
                & "  a.calcdt_oid, " _
                & "  a.calcdt_calc_oid, " _
                & "  a.calcdt_tambahan_kode, " _
                & "  a.calcdt_tambahan_desc, " _
                & "  a.calcdt_tambahan_value, " _
                & "  a.calcdt_qty, " _
                & "  a.calcdt_harga, " _
                & "  a.calcdt_insheet, " _
                & "  a.calcdt_nilai " _
                & "FROM " _
                & "  public.calcdt_tambahan a " _
                & "WHERE " _
                & "  a.calcdt_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.calcdt_tambahan_kode"

            load_data_detail(sql, gc_detail_tambahan, "detail_tambahan")
            gv_detail_cetak.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub relation_detail()
        Try
            'load_data_grid_detail()
            'gv_detail.Columns("rod_ro_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rod_ro_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ro_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        calc_en_id.ItemIndex = 0
        calc_jns_buku.ItemIndex = 0
        calc_en_id.Focus()
        calc_date.EditValue = master_new.PGSqlConn.CekTanggal
        calc_remarks.EditValue = ""


        calc_margin.EditValue = 15.0
        calc_ppn.EditValue = 10.0
        calc_margin_jaket.EditValue = 5.0
        calc_sb_insheet.EditValue = 0.0
        calc_isi_insheet.EditValue = 0.0
        calc_cv_insheet.EditValue = 0.0
        calc_kr_insheet.EditValue = 0.0

        calc_oplah.EditValue = 1.0
        calc_isi_jml.EditValue = 1.0
        calc_isi_bahan.EditValue = "QPP IK"
        calc_isi_bhn_qty.EditValue = 1D
        calc_isi_panjang.EditValue = 61D
        calc_isi_lebar.EditValue = 86D
        calc_isi_jml_warna.EditValue = 2D
        calc_isi_insheet.EditValue = 104D
        calc_isi_naik_cetak.EditValue = 1D
        calc_jns_mesin.EditValue = "Plano"
        calc_sb_jns_bahan.EditValue = "Art Carton"
        calc_sb_jml_design.EditValue = 1D
        calc_sb_bahan_qty.EditValue = 260D
        calc_sb_bhn_panjang.EditValue = 65D
        calc_sb_bhn_lebar.EditValue = 100D
        calc_sb_jml_warna.EditValue = 1D
        calc_sb_insheet.EditValue = 104D
        calc_sb_naik_cetak.EditValue = 1D
        calc_sb_jns_mesin.EditValue = "1/2 Plano"

        calc_cv_jml_design.EditValue = 1D

        calc_cv_jns_bahan.EditValue = "Art Paper"
        calc_cv_bahan_qty.EditValue = 150D
        calc_cv_bhn_panjang.EditValue = 79D
        calc_cv_bhn_lebar.EditValue = 109D
        calc_cv_jml_warna.EditValue = 4D
        calc_cv_insheet.EditValue = 104D
        calc_cv_naik_cetak.EditValue = 1D
        calc_cv_jns_mesin.EditValue = "1/4 Plano"


        calc_kr_jns_bahan.EditValue = "Karton 30 grey Nore"
        calc_kr_jns_bhn_qty.EditValue = 1500D
        calc_kr_bhn_panjang.EditValue = 65D
        calc_kr_bhn_lebar.EditValue = 77D
        calc_kr_insheet.EditValue = 104D

        Try
            XtraTabControl1.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean

        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "SELECT  " _
                        & "  a.cetak_oid, " _
                        & "  a.cetak_item, " _
                        & "  a.cetak_group, " _
                        & "  a.cetak_order,0.0 as kg,0.0 as warna,0.0 as harga_kg, 0.0 as insheet,0.0 as bop_btkl, " _
                        & "  a.cetak_sub_group,'' as jenis,0.0 as velt,  0.0 as jml_potong,0.0 as biaya_potong, " _
                        & "  a.cetak_sub_order,cetak_sub_group_name,0.0 as qty, 0.0 as harga, 0.0 as lebar, 0.0 as panjang, 0.0 as nilai,1 as opsi, 0.0 as outsource " _
                        & "FROM " _
                        & "  public.cetak_mstr a " _
                        & "WHERE " _
                        & "  a.cetak_group = 'pracetak' " _
                        & "ORDER BY " _
                        & "  a.cetak_order, " _
                        & "  a.cetak_sub_order"


                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")

                    .SQL = "SELECT  " _
                        & "  a.cetak_oid, " _
                        & "  a.cetak_item, " _
                        & "  a.cetak_group, " _
                        & "  a.cetak_order, " _
                        & "  a.cetak_sub_group,0.0 as harga, 0.0 as lebar, 0.0 as panjang,0.0 as bop_btkl, " _
                        & "  a.cetak_sub_order,cetak_sub_group_name,'' as jenis,0.0 as qty,0.0 as velt," _
                        & " 0.0 as jml_potong, 0.0 as biaya_potong, 0.0 as kg,0.0 as warna,0.0 as harga_kg, 0.0 as insheet, 0.0 as nilai,1 as opsi, 0.0 as outsource " _
                        & "FROM " _
                        & "  public.cetak_mstr a " _
                        & "WHERE " _
                        & "  a.cetak_group = 'cetak' " _
                        & "ORDER BY " _
                        & "  a.cetak_order, " _
                        & "  a.cetak_sub_order"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_detail")


                    .SQL = "SELECT  " _
                        & "  a.cetak_oid, " _
                        & "  a.cetak_item, " _
                        & "  a.cetak_group, " _
                        & "  a.cetak_order, " _
                        & "  a.cetak_sub_group,0.0 as jml_potong, 0.0 as biaya_potong, 0.0 as kg,0.0 as warna,0.0 as harga_kg, " _
                        & "  a.cetak_sub_order,cetak_sub_group_name,'' as jenis,0.0 as qty,0.0 as velt,0.0 as bop_btkl, " _
                        & " 0.0 as harga, 0.0 as lebar, 0.0 as panjang, 0.0 as nilai , 0.0 as insheet ,1 as opsi , 0.0 as outsource " _
                        & "FROM " _
                        & "  public.cetak_mstr a " _
                        & "WHERE " _
                        & "  a.cetak_group = 'pasca_cetak' " _
                        & "ORDER BY " _
                        & "  a.cetak_order, " _
                        & "  a.cetak_sub_order"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_calc")

                    .SQL = "SELECT  " _
                        & "  a.tambahan_kode, " _
                        & "  a.tambahan_desc, " _
                        & "  a.tambahan_value, 0.0 as qty, 0.0 as harga, 0.0 as insheet, 0.0 as nilai " _
                        & "FROM " _
                        & "  public.cetak_tambahan_pasca a " _
                        & "ORDER BY " _
                        & "  a.tambahan_kode"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit_tambahan")

                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                    gc_edit_cetak.DataSource = ds_edit.Tables(1)
                    gv_edit_cetak.BestFitColumns()
                    gc_edit_pasca.DataSource = ds_edit.Tables(2)
                    gv_edit_pasca.BestFitColumns()
                    gc_edit_tambahan.DataSource = ds_edit.Tables(3)
                    gv_edit_tambahan.BestFitColumns()

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function insert() As Boolean
        Dim i As Integer

        Dim _ro_oid As String
        _ro_oid = Guid.NewGuid.ToString

        sSQLs.Clear()

        Dim _code As String
        _code = func_coll.get_transaction_number("KH", calc_en_id.GetColumnValue("en_code"), "calc_mstr", "calc_code")
        Dim _segen_commision_bruto As Double
        Dim _segen_bonus_recruitment As Double = 0
        Dim _segen_pph21 As Double = 0
        Dim _segen_commission_nett As Double
        Dim _total As Double

        'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
        '    _segen_commision_bruto += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_bruto"))
        '    _segen_bonus_recruitment += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_recruiter"))
        '    _segen_pph21 += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_pph_21"))
        '    _segen_commission_nett += SetNumber(ds_edit.Tables("insert_edit").Rows(i).Item("segend_komisi_total"))
        'Next
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
                                            & "  public.calc_mstr " _
                                            & "( " _
                                            & "  calc_oid, " _
                                            & "  calc_code, " _
                                            & "  calc_en_id, " _
                                            & "  calc_date, " _
                                            & "  calc_jns_buku, " _
                                            & "  calc_remarks, " _
                                            & "  calc_oplah, " _
                                            & "  calc_ukuran, " _
                                            & "  calc_biaya_produksi, " _
                                            & "  calc_biaya_per_buku, " _
                                            & "  calc_panjang, " _
                                            & "  calc_lebar, " _
                                            & "  calc_berat, " _
                                            & "  calc_isi_jml, " _
                                            & "  calc_isi_opsi, " _
                                            & "  calc_isi_bahan, " _
                                            & "  calc_isi_bhn_qty, " _
                                            & "  calc_isi_panjang, " _
                                            & "  calc_isi_lebar, " _
                                            & "  calc_isi_jml_warna, " _
                                            & "  calc_isi_insheet, " _
                                            & "  calc_isi_naik_cetak, " _
                                            & "  calc_jns_mesin, " _
                                            & "  calc_isi_kbthn_bahan, " _
                                            & "  calc_isi_nilai, " _
                                            & "  calc_isi_outsource_opsi, " _
                                            & "  calc_isi_outsource_nilai, " _
                                            & "  calc_sb_jml_design, " _
                                            & "  calc_sb_opsi, " _
                                            & "  calc_sb_panjang, " _
                                            & "  calc_sb_lebar, " _
                                            & "  calc_sb_jns_bahan, " _
                                            & "  calc_sb_bahan_qty, " _
                                            & "  calc_sb_bhn_panjang, " _
                                            & "  calc_sb_bhn_lebar, " _
                                            & "  calc_sb_jml_warna, " _
                                            & "  calc_sb_insheet, " _
                                            & "  calc_sb_naik_cetak, " _
                                            & "  calc_sb_berat, " _
                                            & "  calc_sb_bhn_qty, " _
                                            & "  calc_sb_jns_mesin, " _
                                            & "  calc_sb_nilai, " _
                                            & "  calc_sb_outsource_opsi, " _
                                            & "  calc_sb_outsource_nilai, " _
                                            & "  calc_cv_jml_design, " _
                                            & "  calc_cv_opsi, " _
                                            & "  calc_cv_panjang, " _
                                            & "  calc_cv_lebar, " _
                                            & "  calc_cv_jns_bahan, " _
                                            & "  calc_cv_bahan_qty, " _
                                            & "  calc_cv_bhn_panjang, " _
                                            & "  calc_cv_bhn_lebar, " _
                                            & "  calc_cv_jml_warna, " _
                                            & "  calc_cv_insheet, " _
                                            & "  calc_cv_naik_cetak, " _
                                            & "  calc_cv_berat, " _
                                            & "  calc_cv_kbthn_bhn_qty, " _
                                            & "  calc_cv_jns_mesin, " _
                                            & "  calc_cv_nilai, " _
                                            & "  calc_cv_outsource_opsi, " _
                                            & "  calc_cv_outsource_nilai, " _
                                            & "  calc_kr_jns_bahan, " _
                                            & "  calc_kr_jns_bhn_qty, " _
                                            & "  calc_kr_opsi, " _
                                            & "  calc_kr_bhn_panjang, " _
                                            & "  calc_kr_bhn_lebar, " _
                                            & "  calc_kr_insheet, " _
                                            & "  calc_kr_bhn_qty, " _
                                            & "  calc_kr_nilai, " _
                                            & "  calc_kr_outsource_opsi, " _
                                            & "  calc_kr_outsource_nilai, " _
                                            & "  calc_pra_opsi, " _
                                            & "  calc_opsi_isi, " _
                                            & "  calc_biaya_produksi2, " _
                                            & "  calc_margin, " _
                                            & "  calc_nilai_margin, " _
                                            & "  calc_biaya_produksi_nilai_margin, " _
                                            & "  calc_ppn, " _
                                            & "  calc_nilai_ppn, " _
                                            & "  calc_biaya_produksi_nilai_margin_nilai_ppn, " _
                                            & "  calc_biaya_cetak_pcs, " _
                                            & "  calc_harga_jaket, " _
                                            & "  calc_margin_jaket, " _
                                            & "  calc_nilai_jaket, " _
                                            & "  calc_biaya_cetak_final, " _
                                            & "  calc_judul, " _
                                            & "  calc_add_by,calc_pt_id, " _
                                            & "  calc_add_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ro_oid) & ",  " _
                                            & SetSetring(_code) & ",  " _
                                            & SetInteger(calc_en_id.EditValue) & ",  " _
                                            & SetDateNTime00(calc_date.EditValue) & ",  " _
                                            & SetSetring(calc_jns_buku.EditValue) & ",  " _
                                            & SetSetring(calc_remarks.EditValue) & ",  " _
                                            & SetDec(calc_oplah.EditValue) & ",  " _
                                            & SetSetring(calc_ukuran.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi.EditValue) & ",  " _
                                            & SetDec(calc_biaya_per_buku.EditValue) & ",  " _
                                            & SetDec(calc_panjang.EditValue) & ",  " _
                                            & SetDec(calc_lebar.EditValue) & ",  " _
                                            & SetDec(calc_berat.EditValue) & ",  " _
                                            & SetDec(calc_isi_jml.EditValue) & ",  " _
                                            & SetDec(calc_isi_opsi.EditValue) & ",  " _
                                            & SetSetring(calc_isi_bahan.EditValue) & ",  " _
                                            & SetDec(calc_isi_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_isi_panjang.EditValue) & ",  " _
                                            & SetDec(calc_isi_lebar.EditValue) & ",  " _
                                            & SetDec(calc_isi_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_isi_insheet.EditValue) & ",  " _
                                            & SetDec(calc_isi_naik_cetak.EditValue) & ",  " _
                                            & SetSetring(calc_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_isi_kbthn_bahan.EditValue) & ",  " _
                                            & SetDec(calc_isi_nilai.EditValue) & ",  " _
                                            & SetDec(calc_isi_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_isi_outsource_nilai.EditValue) & ",  " _
                                            & SetDec(calc_sb_jml_design.EditValue) & ",  " _
                                            & SetDec(calc_sb_opsi.EditValue) & ",  " _
                                            & SetDec(calc_sb_panjang.EditValue) & ",  " _
                                            & SetDec(calc_sb_lebar.EditValue) & ",  " _
                                            & SetSetring(calc_sb_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_sb_bahan_qty.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_sb_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_sb_insheet.EditValue) & ",  " _
                                            & SetDec(calc_sb_naik_cetak.EditValue) & ",  " _
                                            & SetDec(calc_sb_berat.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_qty.EditValue) & ",  " _
                                            & SetSetring(calc_sb_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_sb_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_sb_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_sb_outsource_nilai.EditValue) & ",  " _
                                            & SetDec(calc_cv_jml_design.EditValue) & ",  " _
                                            & SetDec(calc_cv_opsi.EditValue) & ",  " _
                                            & SetDec(calc_cv_panjang.EditValue) & ",  " _
                                            & SetDec(calc_cv_lebar.EditValue) & ",  " _
                                            & SetSetring(calc_cv_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_cv_bahan_qty.EditValue) & ",  " _
                                            & SetDec(calc_cv_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_cv_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_cv_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_cv_insheet.EditValue) & ",  " _
                                            & SetDec(calc_cv_naik_cetak.EditValue) & ",  " _
                                            & SetDec(calc_cv_berat.EditValue) & ",  " _
                                            & SetDec(calc_cv_kbthn_bhn_qty.EditValue) & ",  " _
                                            & SetSetring(calc_cv_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_cv_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_cv_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_cv_outsource_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_kr_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_kr_jns_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_kr_opsi.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_kr_insheet.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_kr_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_kr_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_kr_outsource_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_pra_opsi.EditValue) & ",  " _
                                            & SetSetring(calc_opsi_isi.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi2.EditValue) & ",  " _
                                            & SetDec(calc_margin.EditValue) & ",  " _
                                            & SetDec(calc_nilai_margin.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi_nilai_margin.EditValue) & ",  " _
                                            & SetDec(calc_ppn.EditValue) & ",  " _
                                            & SetDec(calc_nilai_ppn.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue) & ",  " _
                                            & SetDec(calc_biaya_cetak_pcs.EditValue) & ",  " _
                                            & SetDec(calc_harga_jaket.EditValue) & ",  " _
                                            & SetDec(calc_margin_jaket.EditValue) & ",  " _
                                            & SetDec(calc_nilai_jaket.EditValue) & ",  " _
                                            & SetDec(calc_biaya_cetak_final.EditValue) & ",  " _
                                            & SetSetring(calc_judul.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetInteger(calc_pt_id.Tag) & ",  " _
                                            & SetSetring(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcd_det " _
                                    & "( " _
                                    & "  cald_oid, " _
                                    & "  calcd_calc_oid, " _
                                    & "  calcd_cetak_oid, " _
                                    & "  calcd_cetak_item, " _
                                    & "  calcd_cetak_group, " _
                                    & "  calcd_cetak_order, " _
                                    & "  calcd_cetak_sub_group, " _
                                    & "  calcd_cetak_sub_order, " _
                                    & "  calcd_cetak_sub_group_name, " _
                                    & "  calcd_qty, " _
                                    & "  calcd_harga, " _
                                    & "  calcd_harga_kg, " _
                                    & "  calcd_lebar, " _
                                    & "  calcd_panjang, " _
                                    & "  calcd_jml_potong, " _
                                    & "  calcd_kg, " _
                                    & "  calcd_bop_btkl, " _
                                    & "  calcd_nilai, " _
                                    & "  calcd_warna, " _
                                    & "  calcd_velt, " _
                                    & "  calcd_insheet, " _
                                    & "  calcd_biaya_potong, " _
                                    & "  calcd_opsi, " _
                                    & "  calcd_outsource, " _
                                    & "  calcd_jenis,calcd_tipe " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_item")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("harga_kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("lebar")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("panjang")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("jml_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("bop_btkl")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("nilai")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("warna")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("velt")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("biaya_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("opsi")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("outsource")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("jenis")) & ",'insert_edit'  " _
                                    & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        For i = 0 To ds_edit.Tables("insert_edit_detail").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                     & "  public.calcd_det " _
                                     & "( " _
                                     & "  cald_oid, " _
                                     & "  calcd_calc_oid, " _
                                     & "  calcd_cetak_oid, " _
                                     & "  calcd_cetak_item, " _
                                     & "  calcd_cetak_group, " _
                                     & "  calcd_cetak_order, " _
                                     & "  calcd_cetak_sub_group, " _
                                     & "  calcd_cetak_sub_order, " _
                                     & "  calcd_cetak_sub_group_name, " _
                                     & "  calcd_qty, " _
                                     & "  calcd_harga, " _
                                     & "  calcd_harga_kg, " _
                                     & "  calcd_lebar, " _
                                     & "  calcd_panjang, " _
                                     & "  calcd_jml_potong, " _
                                     & "  calcd_kg, " _
                                     & "  calcd_bop_btkl, " _
                                     & "  calcd_nilai, " _
                                     & "  calcd_warna, " _
                                     & "  calcd_velt, " _
                                     & "  calcd_insheet, " _
                                     & "  calcd_biaya_potong, " _
                                     & "  calcd_opsi, " _
                                     & "  calcd_outsource, " _
                                     & "  calcd_jenis,calcd_tipe " _
                                     & ")  " _
                                     & "VALUES ( " _
                                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                     & SetSetring(_ro_oid) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_oid")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_item")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_group")) & ",  " _
                                     & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_order")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_group")) & ",  " _
                                     & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_order")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("qty")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("harga")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("harga_kg")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("lebar")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("panjang")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("jml_potong")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("kg")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("bop_btkl")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("nilai")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("warna")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("velt")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("insheet")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("biaya_potong")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("opsi")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("outsource")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("jenis")) & ",'insert_edit_detail'  " _
                                     & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_calc").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcd_det " _
                                    & "( " _
                                    & "  cald_oid, " _
                                    & "  calcd_calc_oid, " _
                                    & "  calcd_cetak_oid, " _
                                    & "  calcd_cetak_item, " _
                                    & "  calcd_cetak_group, " _
                                    & "  calcd_cetak_order, " _
                                    & "  calcd_cetak_sub_group, " _
                                    & "  calcd_cetak_sub_order, " _
                                    & "  calcd_cetak_sub_group_name, " _
                                    & "  calcd_qty, " _
                                    & "  calcd_harga, " _
                                    & "  calcd_harga_kg, " _
                                    & "  calcd_lebar, " _
                                    & "  calcd_panjang, " _
                                    & "  calcd_jml_potong, " _
                                    & "  calcd_kg, " _
                                    & "  calcd_bop_btkl, " _
                                    & "  calcd_nilai, " _
                                    & "  calcd_warna, " _
                                    & "  calcd_velt, " _
                                    & "  calcd_insheet, " _
                                    & "  calcd_biaya_potong, " _
                                    & "  calcd_opsi, " _
                                    & "  calcd_outsource, " _
                                    & "  calcd_jenis,calcd_tipe " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_item")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("harga_kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("lebar")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("panjang")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("jml_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("bop_btkl")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("nilai")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("warna")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("velt")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("biaya_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("opsi")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("outsource")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("jenis")) & ",'insert_edit_calc'  " _
                                    & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_tambahan").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcdt_tambahan " _
                                    & "( " _
                                    & "  calcdt_oid, " _
                                    & "  calcdt_calc_oid, " _
                                    & "  calcdt_tambahan_kode, " _
                                    & "  calcdt_tambahan_desc, " _
                                    & "  calcdt_tambahan_value, " _
                                    & "  calcdt_qty, " _
                                    & "  calcdt_harga, " _
                                    & "  calcdt_insheet, " _
                                    & "  calcdt_nilai " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_kode")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_desc")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_value")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("nilai")) & "  " _
                                    & ")"


                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
                        after_success()
                        set_row(Trim(_ro_oid.ToString), "calc_oid")
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        'If SetString(genpr_year.EditValue) = "" Then
        '    Box("Year can't empt")
        '    Return False
        '    Exit Function
        'End If
        '*********************
        'Cek UM
        'Dim i As Integer
        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_wc_id")) = True Then
        '        MessageBox.Show("Workstation Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next
        '*********************

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_tool_code")) = True Then
        '        MessageBox.Show("Tool Code Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_ptnr_id")) = True Then
        '        MessageBox.Show("Partner Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    If IsDBNull(ds_edit.Tables(0).Rows(i).Item("rod_milestone")) = True Then
        '        MessageBox.Show("Milestone Can't Empty.. Fill with (Y/N)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_edit.Tables(0)).Position = i
        '        Return False
        '    End If
        'Next

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        'MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Return False

        'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pay_interval") = 0 Then
        '    MessageBox.Show("Can't Edit Sales Order Cash Transaction...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Return False
        'End If
        'Dim i As Integer
        'For i = 0 To ds.Tables("detail").Rows.Count - 1
        '    If Not IsDBNull(ds.Tables("detail").Rows(i).Item("sod_qty_shipment")) = True Then
        '        If ds.Tables("detail").Rows(i).Item("sod_qty_shipment") > 0 Then
        '            MessageBox.Show("Data already shipment..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Return False
        '        End If
        '    End If
        'Next

        ' gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ro_oid_mstr = .Item("calc_oid").ToString
                _calc_code = .Item("calc_code")
                calc_en_id.EditValue = .Item("calc_en_id")
                calc_date.EditValue = .Item("calc_date")
                calc_jns_buku.EditValue = .Item("calc_jns_buku")
                calc_remarks.EditValue = .Item("calc_remarks")
                calc_oplah.EditValue = .Item("calc_oplah")
                calc_ukuran.EditValue = .Item("calc_ukuran")
                calc_biaya_produksi.EditValue = .Item("calc_biaya_produksi")
                calc_biaya_per_buku.EditValue = .Item("calc_biaya_per_buku")
                calc_panjang.EditValue = .Item("calc_panjang")
                calc_lebar.EditValue = .Item("calc_lebar")
                calc_berat.EditValue = .Item("calc_berat")
                calc_isi_jml.EditValue = .Item("calc_isi_jml")
                calc_isi_opsi.EditValue = .Item("calc_isi_opsi")
                calc_isi_bahan.EditValue = .Item("calc_isi_bahan")
                calc_isi_bhn_qty.EditValue = .Item("calc_isi_bhn_qty")
                calc_isi_panjang.EditValue = .Item("calc_isi_panjang")
                calc_isi_lebar.EditValue = .Item("calc_isi_lebar")
                calc_isi_jml_warna.EditValue = .Item("calc_isi_jml_warna")
                calc_isi_insheet.EditValue = .Item("calc_isi_insheet")
                calc_isi_naik_cetak.EditValue = .Item("calc_isi_naik_cetak")
                calc_jns_mesin.EditValue = .Item("calc_jns_mesin")
                calc_isi_kbthn_bahan.EditValue = .Item("calc_isi_kbthn_bahan")
                calc_isi_nilai.EditValue = .Item("calc_isi_nilai")
                calc_isi_outsource_opsi.EditValue = .Item("calc_isi_outsource_opsi")
                calc_isi_outsource_nilai.EditValue = .Item("calc_isi_outsource_nilai")
                calc_sb_jml_design.EditValue = .Item("calc_sb_jml_design")
                calc_sb_opsi.EditValue = .Item("calc_sb_opsi")
                calc_sb_panjang.EditValue = .Item("calc_sb_panjang")
                calc_sb_lebar.EditValue = .Item("calc_sb_lebar")
                calc_sb_jns_bahan.EditValue = .Item("calc_sb_jns_bahan")
                calc_sb_bahan_qty.EditValue = .Item("calc_sb_bahan_qty")
                calc_sb_bhn_panjang.EditValue = .Item("calc_sb_bhn_panjang")
                calc_sb_bhn_lebar.EditValue = .Item("calc_sb_bhn_lebar")
                calc_sb_jml_warna.EditValue = .Item("calc_sb_jml_warna")
                calc_sb_insheet.EditValue = .Item("calc_sb_insheet")
                calc_sb_naik_cetak.EditValue = .Item("calc_sb_naik_cetak")
                calc_sb_berat.EditValue = .Item("calc_sb_berat")
                calc_sb_bhn_qty.EditValue = .Item("calc_sb_bhn_qty")
                calc_sb_jns_mesin.EditValue = .Item("calc_sb_jns_mesin")
                calc_sb_nilai.EditValue = .Item("calc_sb_nilai")
                calc_sb_outsource_opsi.EditValue = .Item("calc_sb_outsource_opsi")
                calc_sb_outsource_nilai.EditValue = .Item("calc_sb_outsource_nilai")
                calc_cv_jml_design.EditValue = .Item("calc_cv_jml_design")
                calc_cv_opsi.EditValue = .Item("calc_cv_opsi")
                calc_cv_panjang.EditValue = .Item("calc_cv_panjang")
                calc_cv_lebar.EditValue = .Item("calc_cv_lebar")
                calc_cv_jns_bahan.EditValue = .Item("calc_cv_jns_bahan")
                calc_cv_bahan_qty.EditValue = .Item("calc_cv_bahan_qty")
                calc_cv_bhn_panjang.EditValue = .Item("calc_cv_bhn_panjang")
                calc_cv_bhn_lebar.EditValue = .Item("calc_cv_bhn_lebar")
                calc_cv_jml_warna.EditValue = .Item("calc_cv_jml_warna")
                calc_cv_insheet.EditValue = .Item("calc_cv_insheet")
                calc_cv_naik_cetak.EditValue = .Item("calc_cv_naik_cetak")
                calc_cv_berat.EditValue = .Item("calc_cv_berat")
                calc_cv_kbthn_bhn_qty.EditValue = .Item("calc_cv_kbthn_bhn_qty")
                calc_cv_jns_mesin.EditValue = .Item("calc_cv_jns_mesin")
                calc_cv_nilai.EditValue = .Item("calc_cv_nilai")
                calc_cv_outsource_opsi.EditValue = .Item("calc_cv_outsource_opsi")
                calc_cv_outsource_nilai.EditValue = .Item("calc_cv_outsource_nilai")
                calc_kr_jns_bahan.EditValue = .Item("calc_kr_jns_bahan")
                calc_kr_jns_bhn_qty.EditValue = .Item("calc_kr_jns_bhn_qty")
                calc_kr_opsi.EditValue = .Item("calc_kr_opsi")
                calc_kr_bhn_panjang.EditValue = .Item("calc_kr_bhn_panjang")
                calc_kr_bhn_lebar.EditValue = .Item("calc_kr_bhn_lebar")
                calc_kr_insheet.EditValue = .Item("calc_kr_insheet")
                calc_kr_bhn_qty.EditValue = .Item("calc_kr_bhn_qty")
                calc_kr_nilai.EditValue = .Item("calc_kr_nilai")
                calc_kr_outsource_opsi.EditValue = .Item("calc_kr_outsource_opsi")
                calc_kr_outsource_nilai.EditValue = .Item("calc_kr_outsource_nilai")
                calc_pra_opsi.EditValue = .Item("calc_pra_opsi")
                calc_opsi_isi.EditValue = .Item("calc_opsi_isi")
                calc_biaya_produksi2.EditValue = .Item("calc_biaya_produksi2")
                calc_margin.EditValue = .Item("calc_margin")
                calc_nilai_margin.EditValue = .Item("calc_nilai_margin")
                calc_biaya_produksi_nilai_margin.EditValue = .Item("calc_biaya_produksi_nilai_margin")
                calc_ppn.EditValue = .Item("calc_ppn")
                calc_nilai_ppn.EditValue = .Item("calc_nilai_ppn")
                calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue = .Item("calc_biaya_produksi_nilai_margin_nilai_ppn")
                calc_biaya_cetak_pcs.EditValue = .Item("calc_biaya_cetak_pcs")
                calc_harga_jaket.EditValue = .Item("calc_harga_jaket")
                calc_margin_jaket.EditValue = .Item("calc_margin_jaket")
                calc_nilai_jaket.EditValue = .Item("calc_nilai_jaket")
                calc_biaya_cetak_final.EditValue = .Item("calc_biaya_cetak_final")
                calc_judul.EditValue = .Item("calc_judul")
                calc_pt_id.Tag = SetString(.Item("calc_pt_id"))
                calc_pt_id.EditValue = .Item("pt_code")
                pt_desc.EditValue = .Item("pt_desc1")

            End With
            'so_en_id.Focus()
            'so_cu_id.Enabled = False
            'so_ptnr_id_sold.Enabled = False
            'so_bantu_address.Enabled = False
            'so_pay_type.Enabled = False
            'so_type.Enabled = False
            'so_pi_id.Enabled = False
            'so_ppn_type.Enabled = False

            Try
                'tcg_header.SelectedTabPageIndex = 0
                'tcg_detail.SelectedTabPageIndex = 0
                'tcg_customer.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  a.cald_oid, " _
                                & "  a.calcd_calc_oid , " _
                                & "  a.calcd_cetak_oid as cetak_oid, " _
                                & "  a.calcd_cetak_item as cetak_item, " _
                                & "  a.calcd_cetak_group as cetak_group, " _
                                & "  a.calcd_cetak_order as cetak_order, " _
                                & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                & "  a.calcd_qty as qty, " _
                                & "  a.calcd_harga as harga, " _
                                & "  a.calcd_harga_kg as harga_kg, " _
                                & "  a.calcd_lebar as lebar, " _
                                & "  a.calcd_panjang as panjang, " _
                                & "  a.calcd_jml_potong as jml_potong, " _
                                & "  a.calcd_kg as kg, " _
                                & "  a.calcd_bop_btkl as bop_btkl, " _
                                & "  a.calcd_nilai as nilai, " _
                                & "  a.calcd_warna as warna, " _
                                & "  a.calcd_velt as velt, " _
                                & "  a.calcd_insheet as insheet, " _
                                & "  a.calcd_biaya_potong as biaya_potong, " _
                                & "  a.calcd_opsi as opsi, " _
                                & "  a.calcd_outsource as outsource, " _
                                & "  a.calcd_jenis as jenis, " _
                                & "  a.calcd_tipe  " _
                                & "FROM " _
                                & "  public.calcd_det a " _
                                & "WHERE " _
                                & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                                & "  a.calcd_tipe = 'insert_edit' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")


                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        .SQL = "SELECT  " _
                                   & "  a.cald_oid, " _
                                    & "  a.calcd_calc_oid, " _
                                    & "  a.calcd_cetak_oid as cetak_oid, " _
                                    & "  a.calcd_cetak_item as cetak_item, " _
                                    & "  a.calcd_cetak_group as cetak_group, " _
                                    & "  a.calcd_cetak_order as cetak_order, " _
                                    & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                    & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                    & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                    & "  a.calcd_qty as qty, " _
                                    & "  a.calcd_harga as harga, " _
                                    & "  a.calcd_harga_kg as harga_kg, " _
                                    & "  a.calcd_lebar as lebar, " _
                                    & "  a.calcd_panjang as panjang, " _
                                    & "  a.calcd_jml_potong as jml_potong, " _
                                    & "  a.calcd_kg as kg, " _
                                    & "  a.calcd_bop_btkl as bop_btkl, " _
                                    & "  a.calcd_nilai as nilai, " _
                                    & "  a.calcd_warna as warna, " _
                                    & "  a.calcd_velt as velt, " _
                                    & "  a.calcd_insheet as insheet, " _
                                    & "  a.calcd_biaya_potong as biaya_potong, " _
                                    & "  a.calcd_opsi as opsi, " _
                                    & "  a.calcd_outsource as outsource, " _
                                    & "  a.calcd_jenis as jenis, " _
                                 & "  a.calcd_tipe " _
                                 & "FROM " _
                                 & "  public.calcd_det a " _
                                 & "WHERE " _
                                 & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                                 & "  a.calcd_tipe = 'insert_edit_detail' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_detail")
                        gc_edit_cetak.DataSource = ds_edit.Tables(1)
                        gv_edit_cetak.BestFitColumns()


                        .SQL = "SELECT  " _
                              & "  a.cald_oid, " _
                                & "  a.calcd_calc_oid as calc_oid, " _
                                & "  a.calcd_cetak_oid as cetak_oid, " _
                                & "  a.calcd_cetak_item as cetak_item, " _
                                & "  a.calcd_cetak_group as cetak_group, " _
                                & "  a.calcd_cetak_order as cetak_order, " _
                                & "  a.calcd_cetak_sub_group as cetak_sub_group, " _
                                & "  a.calcd_cetak_sub_order as cetak_sub_order, " _
                                & "  a.calcd_cetak_sub_group_name as cetak_sub_group_name, " _
                                & "  a.calcd_qty as qty, " _
                                & "  a.calcd_harga as harga, " _
                                & "  a.calcd_harga_kg as harga_kg, " _
                                & "  a.calcd_lebar as lebar, " _
                                & "  a.calcd_panjang as panjang, " _
                                & "  a.calcd_jml_potong as jml_potong, " _
                                & "  a.calcd_kg as kg, " _
                                & "  a.calcd_bop_btkl as bop_btkl, " _
                                & "  a.calcd_nilai as nilai, " _
                                & "  a.calcd_warna as warna, " _
                                & "  a.calcd_velt as velt, " _
                                & "  a.calcd_insheet as insheet, " _
                                & "  a.calcd_biaya_potong as biaya_potong, " _
                                & "  a.calcd_opsi as opsi, " _
                                & "  a.calcd_outsource as outsource, " _
                                & "  a.calcd_jenis as jenis, " _
                             & "  a.calcd_tipe " _
                             & "FROM " _
                             & "  public.calcd_det a " _
                             & "WHERE " _
                             & "  a.calcd_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "'  AND  " _
                             & "  a.calcd_tipe = 'insert_edit_calc' order by calcd_cetak_order, calcd_cetak_sub_order "

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_calc")
                        gc_edit_pasca.DataSource = ds_edit.Tables(2)
                        gv_edit_pasca.BestFitColumns()


                        .SQL = "SELECT  " _
                            & "  a.calcdt_oid, " _
                            & "  a.calcdt_calc_oid, " _
                            & "  a.calcdt_tambahan_kode tambahan_kode, " _
                            & "  a.calcdt_tambahan_desc tambahan_desc, " _
                            & "  a.calcdt_tambahan_value tambahan_value, " _
                            & "  a.calcdt_qty qty, " _
                            & "  a.calcdt_harga harga, " _
                            & "  a.calcdt_insheet insheet, " _
                            & "  a.calcdt_nilai nilai " _
                            & "FROM " _
                            & "  public.calcdt_tambahan a " _
                            & "WHERE " _
                            & "  a.calcdt_calc_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString & "' " _
                            & "ORDER BY " _
                            & "  a.calcdt_tambahan_kode"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit_tambahan")
                        gc_edit_tambahan.DataSource = ds_edit.Tables(3)
                        gv_edit_tambahan.BestFitColumns()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If

    End Function

    Public Overrides Function edit()
        Dim i As Integer

        edit = True
        sSQLs.Clear()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "Delete from calc_mstr where calc_oid='" & _ro_oid_mstr & "'"
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.calc_mstr " _
                                            & "( " _
                                            & "  calc_oid, " _
                                            & "  calc_code, " _
                                            & "  calc_en_id, " _
                                            & "  calc_date, " _
                                            & "  calc_jns_buku, " _
                                            & "  calc_remarks, " _
                                            & "  calc_oplah, " _
                                            & "  calc_ukuran, " _
                                            & "  calc_biaya_produksi, " _
                                            & "  calc_biaya_per_buku, " _
                                            & "  calc_panjang, " _
                                            & "  calc_lebar, " _
                                            & "  calc_berat, " _
                                            & "  calc_isi_jml, " _
                                            & "  calc_isi_opsi, " _
                                            & "  calc_isi_bahan, " _
                                            & "  calc_isi_bhn_qty, " _
                                            & "  calc_isi_panjang, " _
                                            & "  calc_isi_lebar, " _
                                            & "  calc_isi_jml_warna, " _
                                            & "  calc_isi_insheet, " _
                                            & "  calc_isi_naik_cetak, " _
                                            & "  calc_jns_mesin, " _
                                            & "  calc_isi_kbthn_bahan, " _
                                            & "  calc_isi_nilai, " _
                                            & "  calc_isi_outsource_opsi, " _
                                            & "  calc_isi_outsource_nilai, " _
                                            & "  calc_sb_jml_design, " _
                                            & "  calc_sb_opsi, " _
                                            & "  calc_sb_panjang, " _
                                            & "  calc_sb_lebar, " _
                                            & "  calc_sb_jns_bahan, " _
                                            & "  calc_sb_bahan_qty, " _
                                            & "  calc_sb_bhn_panjang, " _
                                            & "  calc_sb_bhn_lebar, " _
                                            & "  calc_sb_jml_warna, " _
                                            & "  calc_sb_insheet, " _
                                            & "  calc_sb_naik_cetak, " _
                                            & "  calc_sb_berat, " _
                                            & "  calc_sb_bhn_qty, " _
                                            & "  calc_sb_jns_mesin, " _
                                            & "  calc_sb_nilai, " _
                                            & "  calc_sb_outsource_opsi, " _
                                            & "  calc_sb_outsource_nilai, " _
                                            & "  calc_cv_jml_design, " _
                                            & "  calc_cv_opsi, " _
                                            & "  calc_cv_panjang, " _
                                            & "  calc_cv_lebar, " _
                                            & "  calc_cv_jns_bahan, " _
                                            & "  calc_cv_bahan_qty, " _
                                            & "  calc_cv_bhn_panjang, " _
                                            & "  calc_cv_bhn_lebar, " _
                                            & "  calc_cv_jml_warna, " _
                                            & "  calc_cv_insheet, " _
                                            & "  calc_cv_naik_cetak, " _
                                            & "  calc_cv_berat, " _
                                            & "  calc_cv_kbthn_bhn_qty, " _
                                            & "  calc_cv_jns_mesin, " _
                                            & "  calc_cv_nilai, " _
                                            & "  calc_cv_outsource_opsi, " _
                                            & "  calc_cv_outsource_nilai, " _
                                            & "  calc_kr_jns_bahan, " _
                                            & "  calc_kr_jns_bhn_qty, " _
                                            & "  calc_kr_opsi, " _
                                            & "  calc_kr_bhn_panjang, " _
                                            & "  calc_kr_bhn_lebar, " _
                                            & "  calc_kr_insheet, " _
                                            & "  calc_kr_bhn_qty, " _
                                            & "  calc_kr_nilai, " _
                                            & "  calc_kr_outsource_opsi, " _
                                            & "  calc_kr_outsource_nilai, " _
                                            & "  calc_pra_opsi, " _
                                            & "  calc_opsi_isi, " _
                                            & "  calc_biaya_produksi2, " _
                                            & "  calc_margin, " _
                                            & "  calc_nilai_margin, " _
                                            & "  calc_biaya_produksi_nilai_margin, " _
                                            & "  calc_ppn, " _
                                            & "  calc_nilai_ppn, " _
                                            & "  calc_biaya_produksi_nilai_margin_nilai_ppn, " _
                                            & "  calc_biaya_cetak_pcs, " _
                                            & "  calc_harga_jaket, " _
                                            & "  calc_margin_jaket, " _
                                            & "  calc_nilai_jaket, " _
                                            & "  calc_biaya_cetak_final, " _
                                            & "  calc_judul, " _
                                            & "  calc_add_by,calc_pt_id, " _
                                            & "  calc_add_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ro_oid_mstr) & ",  " _
                                            & SetSetring(_calc_code) & ",  " _
                                            & SetInteger(calc_en_id.EditValue) & ",  " _
                                            & SetDateNTime00(calc_date.EditValue) & ",  " _
                                            & SetSetring(calc_jns_buku.EditValue) & ",  " _
                                            & SetSetring(calc_remarks.EditValue) & ",  " _
                                            & SetDec(calc_oplah.EditValue) & ",  " _
                                            & SetSetring(calc_ukuran.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi.EditValue) & ",  " _
                                            & SetDec(calc_biaya_per_buku.EditValue) & ",  " _
                                            & SetDec(calc_panjang.EditValue) & ",  " _
                                            & SetDec(calc_lebar.EditValue) & ",  " _
                                            & SetDec(calc_berat.EditValue) & ",  " _
                                            & SetDec(calc_isi_jml.EditValue) & ",  " _
                                            & SetDec(calc_isi_opsi.EditValue) & ",  " _
                                            & SetSetring(calc_isi_bahan.EditValue) & ",  " _
                                            & SetDec(calc_isi_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_isi_panjang.EditValue) & ",  " _
                                            & SetDec(calc_isi_lebar.EditValue) & ",  " _
                                            & SetDec(calc_isi_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_isi_insheet.EditValue) & ",  " _
                                            & SetDec(calc_isi_naik_cetak.EditValue) & ",  " _
                                            & SetSetring(calc_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_isi_kbthn_bahan.EditValue) & ",  " _
                                            & SetDec(calc_isi_nilai.EditValue) & ",  " _
                                            & SetDec(calc_isi_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_isi_outsource_nilai.EditValue) & ",  " _
                                            & SetDec(calc_sb_jml_design.EditValue) & ",  " _
                                            & SetDec(calc_sb_opsi.EditValue) & ",  " _
                                            & SetDec(calc_sb_panjang.EditValue) & ",  " _
                                            & SetDec(calc_sb_lebar.EditValue) & ",  " _
                                            & SetSetring(calc_sb_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_sb_bahan_qty.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_sb_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_sb_insheet.EditValue) & ",  " _
                                            & SetDec(calc_sb_naik_cetak.EditValue) & ",  " _
                                            & SetDec(calc_sb_berat.EditValue) & ",  " _
                                            & SetDec(calc_sb_bhn_qty.EditValue) & ",  " _
                                            & SetSetring(calc_sb_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_sb_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_sb_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_sb_outsource_nilai.EditValue) & ",  " _
                                            & SetDec(calc_cv_jml_design.EditValue) & ",  " _
                                            & SetDec(calc_cv_opsi.EditValue) & ",  " _
                                            & SetDec(calc_cv_panjang.EditValue) & ",  " _
                                            & SetDec(calc_cv_lebar.EditValue) & ",  " _
                                            & SetSetring(calc_cv_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_cv_bahan_qty.EditValue) & ",  " _
                                            & SetDec(calc_cv_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_cv_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_cv_jml_warna.EditValue) & ",  " _
                                            & SetDec(calc_cv_insheet.EditValue) & ",  " _
                                            & SetDec(calc_cv_naik_cetak.EditValue) & ",  " _
                                            & SetDec(calc_cv_berat.EditValue) & ",  " _
                                            & SetDec(calc_cv_kbthn_bhn_qty.EditValue) & ",  " _
                                            & SetSetring(calc_cv_jns_mesin.EditValue) & ",  " _
                                            & SetDec(calc_cv_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_cv_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_cv_outsource_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_kr_jns_bahan.EditValue) & ",  " _
                                            & SetDec(calc_kr_jns_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_kr_opsi.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_panjang.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_lebar.EditValue) & ",  " _
                                            & SetDec(calc_kr_insheet.EditValue) & ",  " _
                                            & SetDec(calc_kr_bhn_qty.EditValue) & ",  " _
                                            & SetDec(calc_kr_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_kr_outsource_opsi.EditValue) & ",  " _
                                            & SetDec(calc_kr_outsource_nilai.EditValue) & ",  " _
                                            & SetSetring(calc_pra_opsi.EditValue) & ",  " _
                                            & SetSetring(calc_opsi_isi.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi2.EditValue) & ",  " _
                                            & SetDec(calc_margin.EditValue) & ",  " _
                                            & SetDec(calc_nilai_margin.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi_nilai_margin.EditValue) & ",  " _
                                            & SetDec(calc_ppn.EditValue) & ",  " _
                                            & SetDec(calc_nilai_ppn.EditValue) & ",  " _
                                            & SetDec(calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue) & ",  " _
                                            & SetDec(calc_biaya_cetak_pcs.EditValue) & ",  " _
                                            & SetDec(calc_harga_jaket.EditValue) & ",  " _
                                            & SetDec(calc_margin_jaket.EditValue) & ",  " _
                                            & SetDec(calc_nilai_jaket.EditValue) & ",  " _
                                            & SetDec(calc_biaya_cetak_final.EditValue) & ",  " _
                                            & SetSetring(calc_judul.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetInteger(calc_pt_id.Tag) & ",  " _
                                            & SetSetring(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"


                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcd_det " _
                                    & "( " _
                                    & "  cald_oid, " _
                                    & "  calcd_calc_oid, " _
                                    & "  calcd_cetak_oid, " _
                                    & "  calcd_cetak_item, " _
                                    & "  calcd_cetak_group, " _
                                    & "  calcd_cetak_order, " _
                                    & "  calcd_cetak_sub_group, " _
                                    & "  calcd_cetak_sub_order, " _
                                    & "  calcd_cetak_sub_group_name, " _
                                    & "  calcd_qty, " _
                                    & "  calcd_harga, " _
                                    & "  calcd_harga_kg, " _
                                    & "  calcd_lebar, " _
                                    & "  calcd_panjang, " _
                                    & "  calcd_jml_potong, " _
                                    & "  calcd_kg, " _
                                    & "  calcd_bop_btkl, " _
                                    & "  calcd_nilai, " _
                                    & "  calcd_warna, " _
                                    & "  calcd_velt, " _
                                    & "  calcd_insheet, " _
                                    & "  calcd_biaya_potong, " _
                                    & "  calcd_opsi, " _
                                    & "  calcd_outsource, " _
                                    & "  calcd_jenis,calcd_tipe " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid_mstr) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_item")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("harga_kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("lebar")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("panjang")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("jml_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("bop_btkl")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("nilai")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("warna")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("velt")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("biaya_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("opsi")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit").Rows(i).Item("outsource")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit").Rows(i).Item("jenis")) & ",'insert_edit'  " _
                                    & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        For i = 0 To ds_edit.Tables("insert_edit_detail").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                     & "  public.calcd_det " _
                                     & "( " _
                                     & "  cald_oid, " _
                                     & "  calcd_calc_oid, " _
                                     & "  calcd_cetak_oid, " _
                                     & "  calcd_cetak_item, " _
                                     & "  calcd_cetak_group, " _
                                     & "  calcd_cetak_order, " _
                                     & "  calcd_cetak_sub_group, " _
                                     & "  calcd_cetak_sub_order, " _
                                     & "  calcd_cetak_sub_group_name, " _
                                     & "  calcd_qty, " _
                                     & "  calcd_harga, " _
                                     & "  calcd_harga_kg, " _
                                     & "  calcd_lebar, " _
                                     & "  calcd_panjang, " _
                                     & "  calcd_jml_potong, " _
                                     & "  calcd_kg, " _
                                     & "  calcd_bop_btkl, " _
                                     & "  calcd_nilai, " _
                                     & "  calcd_warna, " _
                                     & "  calcd_velt, " _
                                     & "  calcd_insheet, " _
                                     & "  calcd_biaya_potong, " _
                                     & "  calcd_opsi, " _
                                     & "  calcd_outsource, " _
                                     & "  calcd_jenis,calcd_tipe " _
                                     & ")  " _
                                     & "VALUES ( " _
                                     & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                     & SetSetring(_ro_oid_mstr) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_oid")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_item")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_group")) & ",  " _
                                     & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_order")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_group")) & ",  " _
                                     & SetInteger(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_order")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("qty")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("harga")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("harga_kg")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("lebar")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("panjang")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("jml_potong")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("kg")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("bop_btkl")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("nilai")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("warna")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("velt")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("insheet")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("biaya_potong")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("opsi")) & ",  " _
                                     & SetDec(ds_edit.Tables("insert_edit_detail").Rows(i).Item("outsource")) & ",  " _
                                     & SetSetring(ds_edit.Tables("insert_edit_detail").Rows(i).Item("jenis")) & ",'insert_edit_detail'  " _
                                     & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_calc").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcd_det " _
                                    & "( " _
                                    & "  cald_oid, " _
                                    & "  calcd_calc_oid, " _
                                    & "  calcd_cetak_oid, " _
                                    & "  calcd_cetak_item, " _
                                    & "  calcd_cetak_group, " _
                                    & "  calcd_cetak_order, " _
                                    & "  calcd_cetak_sub_group, " _
                                    & "  calcd_cetak_sub_order, " _
                                    & "  calcd_cetak_sub_group_name, " _
                                    & "  calcd_qty, " _
                                    & "  calcd_harga, " _
                                    & "  calcd_harga_kg, " _
                                    & "  calcd_lebar, " _
                                    & "  calcd_panjang, " _
                                    & "  calcd_jml_potong, " _
                                    & "  calcd_kg, " _
                                    & "  calcd_bop_btkl, " _
                                    & "  calcd_nilai, " _
                                    & "  calcd_warna, " _
                                    & "  calcd_velt, " _
                                    & "  calcd_insheet, " _
                                    & "  calcd_biaya_potong, " _
                                    & "  calcd_opsi, " _
                                    & "  calcd_outsource, " _
                                    & "  calcd_jenis,calcd_tipe " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid_mstr) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_oid")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_item")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_group")) & ",  " _
                                    & SetInteger(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_order")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("cetak_sub_group_name")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("harga_kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("lebar")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("panjang")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("jml_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("kg")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("bop_btkl")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("nilai")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("warna")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("velt")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("biaya_potong")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("opsi")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_calc").Rows(i).Item("outsource")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_calc").Rows(i).Item("jenis")) & ",'insert_edit_calc'  " _
                                    & ")"



                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        For i = 0 To ds_edit.Tables("insert_edit_tambahan").Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                    & "  public.calcdt_tambahan " _
                                    & "( " _
                                    & "  calcdt_oid, " _
                                    & "  calcdt_calc_oid, " _
                                    & "  calcdt_tambahan_kode, " _
                                    & "  calcdt_tambahan_desc, " _
                                    & "  calcdt_tambahan_value, " _
                                    & "  calcdt_qty, " _
                                    & "  calcdt_harga, " _
                                    & "  calcdt_insheet, " _
                                    & "  calcdt_nilai " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_ro_oid_mstr) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_kode")) & ",  " _
                                    & SetSetring(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_desc")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("tambahan_value")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("qty")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("harga")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("insheet")) & ",  " _
                                    & SetDec(ds_edit.Tables("insert_edit_tambahan").Rows(i).Item("nilai")) & "  " _
                                    & ")"


                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
                        after_success()
                        set_row(Trim(_ro_oid_mstr.ToString), "calc_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
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

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If
            sSQLs.Clear()

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from calc_mstr where calc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
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


    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _rod_en_id As Integer = calc_en_id.EditValue

        'If _col = "wc_desc" Then
        '    Dim frm As New FWCSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "code_name" Then
        '    Dim frm As New FToolSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ptnr_name" Then
        '    Dim frm As New FPartnerSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _rod_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If

    End Sub


    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_data_grid_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_data_grid_detail()
    End Sub


    Private Function cek_periode(ByVal pr_periode As String) As String
        Try
            Dim ssql As String
            Dim hasil As String = ""
            ssql = "select  seperiode_code  from seperiode_mstr where seperiode_code>=" & SetSetring(pr_periode) & " order by seperiode_code limit 3 "

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                hasil = dr(0)
            Next
            Return hasil
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub psgen_periode_code_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calc_jns_buku.EditValueChanged
        Try
            'segen_start_date.EditValue = segen_periode.GetColumnValue("seperiode_start_date")
            'segen_end_date.EditValue = segen_periode.GetColumnValue("seperiode_end_date")
            calc_remarks.EditValue = calc_jns_buku.GetColumnValue("seperiode_remarks")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_detail.Click
        Try
            gv_detail_SelectionChanged(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub gv_detail_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_detail.SelectionChanged
        Try
            If ds.Tables("detail").Rows.Count = 0 Then
                Exit Sub
            End If

            'Dim sql As String
            'sql = "select ptnr_id,ptnr_parent,ptnr_name,  segend_poin_total, segend_poin_pengali,  segend_komisi_bruto,  segend_komisi_recruiter,    segend_komisi_total,  segend_pph_21,  segend_komisi_netto  from (select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
            '  & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
            '  & " where ptnr_id in " _
            '  & " ( select menu_id from get_all_child(" _
            '  & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString _
            '  & ")) or ptnr_id in (" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("segend_ptnr_id").ToString & ")) as temp  " _
            '   & " left outer join public.segend_det  ON (segend_ptnr_id = ptnr_id) where segend_segen_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("segen_oid").ToString & "'"

            'Dim dt_tree As New DataTable
            'dt_tree = master_new.PGSqlConn.GetTableData(sql)

            'Try
            '    TreeList1.DataSource = dt_tree
            '    TreeList1.ExpandAll()
            'Catch ex As Exception
            'End Try


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        Try
            Dim _file As String = ""
            _file = AskSaveAsFile("Xls Files | *.xls")
            If _file = "" Then
                Box("Please select file name to export")
                Exit Sub
            End If

            'TreeList1.ExportToXls(_file)
            OpenFile(_file)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub preview()

        Dim _sql As String

        _sql = "SELECT  " _
                & "  a.calc_oid, " _
                & "  a.calc_code, " _
                & "  a.calc_en_id, " _
                & "  b.en_desc, " _
                & "  a.calc_date, " _
                & "  a.calc_judul, " _
                & "  a.calc_jns_buku, " _
                & "  a.calc_remarks, " _
                & "  a.calc_oplah, " _
                & "  a.calc_ukuran, " _
                & "  a.calc_biaya_produksi, " _
                & "  a.calc_biaya_per_buku, " _
                & "  a.calc_panjang, " _
                & "  a.calc_lebar, " _
                & "  a.calc_berat, " _
                & "  a.calc_isi_jml, " _
                & "  a.calc_isi_opsi, " _
                & "  a.calc_isi_bahan, " _
                & "  a.calc_isi_bhn_qty, " _
                & "  a.calc_isi_panjang, " _
                & "  a.calc_isi_lebar, " _
                & "  a.calc_isi_jml_warna, " _
                & "  a.calc_isi_insheet, " _
                & "  a.calc_isi_naik_cetak, " _
                & "  a.calc_jns_mesin, " _
                & "  a.calc_isi_kbthn_bahan, " _
                & "  coalesce(a.calc_isi_nilai,0) calc_isi_nilai, " _
                & "  a.calc_isi_outsource_opsi, " _
                & "  a.calc_isi_outsource_nilai, " _
                & "  a.calc_sb_jml_design, " _
                & "  a.calc_sb_opsi, " _
                & "  a.calc_sb_panjang, " _
                & "  a.calc_sb_lebar, " _
                & "  a.calc_sb_jns_bahan, " _
                & "  a.calc_sb_bahan_qty, " _
                & "  a.calc_sb_bhn_panjang, " _
                & "  a.calc_sb_bhn_lebar, " _
                & "  a.calc_sb_jml_warna, " _
                & "  a.calc_sb_insheet, " _
                & "  a.calc_sb_naik_cetak, " _
                & "  a.calc_sb_berat, " _
                & "  a.calc_sb_bhn_qty, " _
                & "  a.calc_sb_jns_mesin, " _
                & " coalesce( a.calc_sb_nilai,0) calc_sb_nilai, " _
                & "  a.calc_sb_outsource_opsi, " _
                & "  a.calc_sb_outsource_nilai, " _
                & "  a.calc_cv_jml_design, " _
                & "  a.calc_cv_opsi, " _
                & "  a.calc_cv_panjang, " _
                & "  a.calc_cv_lebar, " _
                & "  a.calc_cv_jns_bahan, " _
                & "  a.calc_cv_bahan_qty, " _
                & "  a.calc_cv_bhn_panjang, " _
                & "  a.calc_cv_bhn_lebar, " _
                & "  a.calc_cv_jml_warna, " _
                & "  a.calc_cv_insheet, " _
                & "  a.calc_cv_naik_cetak, " _
                & "  a.calc_cv_berat, " _
                & "  a.calc_cv_kbthn_bhn_qty, " _
                & "  a.calc_cv_jns_mesin, " _
                & "  coalesce(a.calc_cv_nilai,0) calc_cv_nilai, " _
                & "  a.calc_cv_outsource_opsi, " _
                & "  a.calc_cv_outsource_nilai, " _
                & "  a.calc_kr_jns_bahan, " _
                & "  a.calc_kr_jns_bhn_qty, " _
                & "  a.calc_kr_opsi, " _
                & "  a.calc_kr_bhn_panjang, " _
                & "  a.calc_kr_bhn_lebar, " _
                & "  a.calc_kr_insheet, " _
                & "  a.calc_kr_bhn_qty, " _
                & "  coalesce(a.calc_kr_nilai,0) calc_kr_nilai, " _
                & "  a.calc_kr_outsource_opsi, " _
                & "  a.calc_kr_outsource_nilai, " _
                & "  a.calc_pra_opsi, " _
                & "  a.calc_opsi_isi, " _
                & "  a.calc_biaya_produksi2, " _
                & "  a.calc_margin, " _
                & "  a.calc_nilai_margin, " _
                & "  a.calc_biaya_produksi_nilai_margin, " _
                & "  a.calc_ppn, " _
                & "  a.calc_nilai_ppn, " _
                & "  a.calc_biaya_produksi_nilai_margin_nilai_ppn, " _
                & "  a.calc_biaya_cetak_pcs, " _
                & "  a.calc_harga_jaket, " _
                & "  a.calc_margin_jaket, " _
                & "  a.calc_nilai_jaket, " _
                & "  a.calc_biaya_cetak_final,calc_add_by,calc_add_date,calc_upd_by,calc_upd_date  " _
                & "FROM " _
                & "  public.calc_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.calc_en_id = b.en_id) " _
                & "WHERE " _
                & "  calc_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString) & " " _
                & " "


        'Dim rpt As New rptCalculateCost
        'With rpt
        '    Dim ds As New DataSet
        '    ds = master_new.PGSqlConn.ReportDataset(_sql)
        '    If ds.Tables(0).Rows.Count < 1 Then
        '        Box("Maaf data kosong")
        '        Exit Sub
        '    End If


        '    ._oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_oid").ToString
        '    .DataSource = ds
        '    .DataMember = "Table"

        '     Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
  
        '    printTool.PreviewForm.Text = "Personal Selling Progressio Report"
        '    '.PrintingSystem = ps
        '    printTool.ShowPreview()


        'End With

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "rptCalculateCost"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("calc_code")
        frm.ShowDialog()


    End Sub

    Private Sub calc_ukuran_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calc_ukuran.EditValueChanged
        Try
            Dim ssql As String
            ssql = "SELECT  " _
                    & "  spek_lebar, spek_panjang  " _
                    & "FROM " _
                    & "  public.cetak_spec_buku a " _
                    & "where a.spek_ukuran=" & SetSetring(calc_ukuran.EditValue)

            Dim dt As New DataTable
            dt = GetTableData(ssql)

            For Each dr As DataRow In dt.Rows
                calc_panjang.EditValue = dr("spek_panjang")
                calc_lebar.EditValue = dr("spek_lebar")
            Next

            hitung("isi")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Sub hitung()
        hitung("isi")
        hitung("skipblat")
        hitung("cover")
        hitung("karton")
        hitung("pracetak")
    End Sub
    Public Sub hitung(ByVal par_opsi As String)
        Try
            If par_opsi = "isi" Then
                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_isi_jml.EditValue) > 0 And SetNumber(calc_isi_insheet.EditValue) > 0 Then

                    calc_isi_kbthn_bahan.EditValue = SetNumber(calc_oplah.EditValue) * SetNumber(calc_isi_jml.EditValue) / _
                        SetNumber(calc_ukuran.GetColumnValue("spek_hal_lembar_plano")) * SetNumber(calc_isi_insheet.EditValue) / 100 / 500

                    Dim _key As String = calc_isi_bahan.EditValue & calc_isi_bhn_qty.EditValue & calc_isi_panjang.EditValue & calc_isi_lebar.EditValue
                    Dim ssql As String
                    ssql = "select harga_rim from cetak_harga_kertas where kode=" & SetSetring(_key)

                    calc_isi_nilai.EditValue = IIf(SetNumber(calc_isi_outsource_nilai.EditValue) > 0, SetNumber(calc_isi_outsource_nilai.EditValue), SetNumber(GetDataColumn(ssql)) * SetNumber(calc_isi_kbthn_bahan.EditValue) * SetNumber(calc_isi_opsi.EditValue))
                End If

            ElseIf par_opsi = "skipblat" Then
                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_sb_insheet.EditValue) > 0 And SetNumber(calc_sb_naik_cetak.EditValue) > 0  Then

                    calc_sb_bhn_qty.EditValue = SetNumber(calc_oplah.EditValue) * 2 / SetNumber(calc_ukuran.GetColumnValue("spek_pot_skiblat_plano")) _
                        * SetNumber(calc_sb_insheet.EditValue) / 100 / 500 / SetNumber(calc_sb_naik_cetak.EditValue)

                    Dim _key As String = calc_sb_jns_bahan.EditValue & calc_sb_bahan_qty.EditValue & calc_sb_bhn_panjang.EditValue & calc_sb_bhn_lebar.EditValue
                    Dim ssql As String
                    ssql = "select harga_rim from cetak_harga_kertas where kode=" & SetSetring(_key)

                    calc_sb_nilai.EditValue = IIf(SetNumber(calc_sb_outsource_nilai.EditValue) > 0, SetNumber(calc_sb_outsource_nilai.EditValue), SetNumber(calc_sb_bhn_qty.EditValue) * SetNumber(GetDataColumn(ssql)) * SetNumber(calc_jns_buku.GetColumnValue("finishing_sb")) * SetNumber(calc_sb_opsi.EditValue))

                End If
               
            ElseIf par_opsi = "cover" Then
                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_cv_insheet.EditValue) > 0 And SetNumber(calc_cv_naik_cetak.EditValue) > 0 Then
                    calc_cv_kbthn_bhn_qty.EditValue = SetNumber(calc_oplah.EditValue) / SetNumber(calc_ukuran.GetColumnValue("spek_pot_cover_plano")) _
                        * SetNumber(calc_cv_insheet.EditValue) / 100 / 500 / SetNumber(calc_cv_naik_cetak.EditValue)

                    Dim _key As String = calc_cv_jns_bahan.EditValue & calc_cv_bahan_qty.EditValue & calc_cv_bhn_panjang.EditValue & calc_cv_bhn_lebar.EditValue
                    Dim ssql As String
                    ssql = "select harga_rim from cetak_harga_kertas where kode=" & SetSetring(_key)

                    calc_cv_nilai.EditValue = IIf(SetNumber(calc_cv_outsource_nilai.EditValue) > 0, SetNumber(calc_cv_outsource_nilai.EditValue), SetNumber(calc_cv_kbthn_bhn_qty.EditValue) * SetNumber(GetDataColumn(ssql)) * SetNumber(calc_jns_buku.GetColumnValue("finishing_cv")) * SetNumber(calc_cv_opsi.EditValue))
                End If
               

            ElseIf par_opsi = "karton" Then
                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_kr_insheet.EditValue) > 0 Then
                    Dim _key As String = calc_ukuran.EditValue & calc_kr_bhn_panjang.EditValue & calc_kr_bhn_lebar.EditValue
                    Dim ssql As String
                    ssql = "select nilai from cetak_pot_board where lower(kode)=" & SetSetring(_key.ToLower)

                    calc_kr_bhn_qty.EditValue = SetNumber(calc_oplah.EditValue) / SetNumber(GetDataColumn(ssql)) _
                        * SetNumber(calc_kr_insheet.EditValue) / 100 / 500

                    _key = calc_kr_jns_bahan.EditValue & calc_kr_jns_bhn_qty.EditValue & calc_kr_bhn_panjang.EditValue & calc_kr_bhn_lebar.EditValue
                    ssql = "select harga_rim from cetak_harga_kertas where kode=" & SetSetring(_key)

                    calc_kr_nilai.EditValue = IIf(SetNumber(calc_kr_outsource_nilai.EditValue) > 0, SetNumber(calc_kr_outsource_nilai.EditValue), SetNumber(calc_kr_bhn_qty.EditValue) * SetNumber(GetDataColumn(ssql)) * SetNumber(calc_jns_buku.GetColumnValue("finishing_brd")) * SetNumber(calc_kr_opsi.EditValue))
                End If

            ElseIf par_opsi = "pracetak" Then
                Dim _qty_isi_plate As Double = 0.0
                If Not ds_edit Is Nothing Then

                    Dim _yes_no As Double = 1
                    For Each dr As DataRow In ds_edit.Tables("insert_edit").Rows
                        If dr("cetak_sub_group") = "imposisi" Then
                            If dr("cetak_sub_order") = 1 Then
                                If SetNumber(calc_isi_jml.EditValue) > 0 And SetNumber(calc_isi_jml_warna.EditValue) > 0 Then
                                    dr("qty") = SetNumber(calc_isi_jml.EditValue) / _
                                    SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue))) * _
                                    SetNumber(calc_isi_jml_warna.EditValue) * 2
                                    _qty_isi_plate = dr("qty")
                                End If

                            ElseIf dr("cetak_sub_order") = 2 Then
                                dr("qty") = SetNumber(calc_sb_jml_warna.EditValue) * SetNumber(calc_sb_jml_design.EditValue)
                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("qty") = SetNumber(calc_cv_jml_design.EditValue) * SetNumber(calc_cv_jml_warna.EditValue)
                            End If
                            dr("harga") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Pracetak-plano-Isi")))

                            dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("harga")) * SetNumber(dr("opsi"))
                        ElseIf dr("cetak_sub_group") = "plat" Then
                            Dim _opsi As Double = 1
                            If dr("cetak_sub_order") = 1 Then
                                If SetNumber(calc_isi_jml.EditValue) > 0 Then
                                    dr("qty") = SetNumber(calc_isi_jml.EditValue) / SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue))) * _
                               SetNumber(calc_isi_jml_warna.EditValue) * 2
                                    dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_plate where plate=" & SetSetring(calc_jns_mesin.EditValue)))
                                    _yes_no = calc_isi_opsi.EditValue
                                End If

                            ElseIf dr("cetak_sub_order") = 2 Then

                                dr("qty") = SetNumber(calc_sb_jml_warna.EditValue) * SetNumber(calc_sb_jml_design.EditValue)
                                dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_plate where plate=" & SetSetring(calc_sb_jns_mesin.EditValue)))

                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("qty") = SetNumber(calc_cv_jml_design.EditValue) * SetNumber(calc_cv_jml_warna.EditValue)
                                dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_plate where plate=" & SetSetring(calc_cv_jns_mesin.EditValue)))
                            ElseIf dr("cetak_sub_order") = 4 Then
                                dr("qty") = 1.0
                                dr("harga") = SetNumber(GetDataColumn("select matras_foil from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue)))
                            ElseIf dr("cetak_sub_order") = 5 Then
                                dr("qty") = 1.0
                                dr("harga") = SetNumber(GetDataColumn("select matras_deboss from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue)))
                                _opsi = IIf(calc_jns_buku.EditValue = "Hardcover", 1, 0)
                            ElseIf dr("cetak_sub_order") = 6 Then
                                dr("qty") = 1.0
                                dr("harga") = SetNumber(GetDataColumn("select matras_emboss from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue)))
                                _opsi = IIf(calc_jns_buku.EditValue = "Hardcover", 0, 1)
                            End If
                            dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("harga")) * _yes_no * SetNumber(dr("opsi")) * _opsi
                            _yes_no = 1
                        End If
                        dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), dr("nilai"))
                    Next

                    ds_edit.AcceptChanges()
                    gv_edit.BestFitColumns()
                    Dim _velt_isi, _velt_skiblat, _velt_cover, _qty_isi, _qty_skiblat, _qty_cover As Double
                    _velt_isi = 0
                    _velt_skiblat = 0

                    For Each dr As DataRow In ds_edit.Tables("insert_edit_detail").Rows
                        If dr("cetak_sub_group") = "potong_kertas" Then
                            If dr("cetak_sub_order") = 1 Then
                                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_isi_jml_warna.EditValue) > 0 And SetNumber(calc_isi_insheet.EditValue) > 0 Then
                                    dr("qty") = calc_isi_kbthn_bahan.EditValue * 500.0 / (SetNumber(_qty_isi_plate) / SetNumber(calc_isi_jml_warna.EditValue) / 2) ' SetNumber(calc_oplah.EditValue) * SetNumber(calc_isi_insheet.EditValue) / 100
                                    _qty_isi = dr("qty")
                                    dr("velt") = SetNumber(_qty_isi_plate) / SetNumber(calc_isi_jml_warna.EditValue) / 2
                                    _velt_isi = SetNumber(dr("velt"))
                                    dr("jml_potong") = SetNumber(GetDataColumn("select cut from cetak_plate where plate=" & SetSetring(calc_jns_mesin.EditValue)))
                                    dr("biaya_potong") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where upper(kode)=" & SetSetring("Cutting velt-plano-Isi").ToUpper))
                                    dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                                    _yes_no = calc_isi_opsi.EditValue
                                End If

                            ElseIf dr("cetak_sub_order") = 2 Then
                                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_sb_jml_design.EditValue) > 0 And SetNumber(calc_sb_insheet.EditValue) > 0 Then
                                    dr("qty") = calc_sb_bhn_qty.EditValue * 500 ' SetNumber(calc_oplah.EditValue) / SetNumber(calc_sb_jml_design.EditValue) * SetNumber(calc_isi_insheet.EditValue) / 100
                                    _qty_skiblat = dr("qty")
                                    dr("velt") = SetNumber(calc_sb_jml_design.EditValue)
                                    _velt_skiblat = dr("velt")
                                    dr("jml_potong") = SetNumber(GetDataColumn("select cut from cetak_plate where plate=" & SetSetring(calc_sb_jns_mesin.EditValue)))
                                    dr("biaya_potong") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where upper(kode)=" & SetSetring("Cutting velt-1/2 plano-Isi").ToUpper))
                                    dr("insheet") = SetNumber(calc_sb_insheet.EditValue)
                                    _yes_no = calc_sb_opsi.EditValue
                                End If

                            ElseIf dr("cetak_sub_order") = 3 Then
                                If SetNumber(calc_oplah.EditValue) > 0 And SetNumber(calc_cv_jml_design.EditValue) > 0 And SetNumber(calc_cv_insheet.EditValue) > 0 Then
                                    dr("qty") = calc_cv_kbthn_bhn_qty.EditValue * 500 'SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_jml_design.EditValue) * SetNumber(calc_isi_insheet.EditValue) / 100
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    _qty_cover = dr("qty")
                                    _velt_cover = dr("velt")
                                    dr("jml_potong") = SetNumber(GetDataColumn("select cut from cetak_plate where plate=" & SetSetring(calc_cv_jns_mesin.EditValue)))
                                    dr("biaya_potong") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where upper(kode)=" & SetSetring("Cutting velt-1/4 plano-Isi").ToUpper))
                                    dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    _yes_no = calc_cv_opsi.EditValue
                                End If

                            End If
                            dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("biaya_potong")) * SetNumber(dr("jml_potong")) * _yes_no * SetNumber(dr("opsi"))
                            _yes_no = 1
                        ElseIf dr("cetak_sub_group") = "tinta_isi" Then
                            If SetNumber(calc_isi_panjang.EditValue) > 0 And SetNumber(calc_isi_lebar.EditValue) > 0 And SetNumber(calc_isi_jml_warna.EditValue) > 0 Then
                                Dim _o, _opsi, _v, _p, _i As Double

                                If dr("cetak_sub_order") < 5 Then
                                    If dr("jenis") = "" Then
                                        dr("jenis") = "Teks"
                                    End If
                                Else
                                    If dr("jenis") = "" Then
                                        dr("jenis") = "Blok"
                                    End If
                                End If
                                _yes_no = calc_isi_opsi.EditValue

                                dr("qty") = SetNumber(_qty_isi)
                                dr("velt") = SetNumber(_velt_isi)
                                _o = SetNumber(calc_isi_panjang.EditValue) * SetNumber(calc_isi_lebar.EditValue) / 100000
                                _opsi = SetNumber(calc_opsi_isi.GetColumnValue("nilai"))
                                _v = SetNumber(GetDataColumn("select nilai from cetak_opsi_detail_cetak where kode=" & SetSetring(dr("jenis"))))
                                _p = SetNumber(GetDataColumn("select isi_p from cetak_spec_isi where isi_jenis=" & SetSetring(calc_isi_bahan.EditValue)))
                                _i = SetNumber(GetDataColumn("select nilai from cetak_warna_tinta where warna=" & SetSetring(dr("cetak_item"))))
                                dr("kg") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * _o * _opsi * _v * _p * _i / 1000 * SetNumber(dr("warna")) / 100 * SetNumber(calc_isi_jml_warna.EditValue)
                                dr("nilai") = SetNumber(dr("kg")) * SetNumber(dr("harga_kg")) * _yes_no * SetNumber(dr("opsi"))

                                'Dim d, e, f, g, h, i As Double
                                'd = SetNumber(dr("qty"))
                                'e = SetNumber(dr("velt"))
                                'f = SetNumber(dr("kg"))
                                'g = SetNumber(dr("harga_kg"))
                                'h = SetNumber(dr("warna"))
                                'i = SetNumber(dr("opsi"))


                                _yes_no = 1
                            End If
                           
                        ElseIf dr("cetak_sub_group") = "tinta_skiblat" Then
                            If SetNumber(calc_sb_jml_warna.EditValue) > 0 And SetNumber(calc_sb_bhn_panjang.EditValue) > 0 And SetNumber(calc_sb_bhn_lebar.EditValue) > 0 Then
                                Dim _o, _opsi, _v, _p, _i As Double
                                If dr("jenis") = "" Then
                                    dr("jenis") = "Blok"
                                End If
                                dr("qty") = SetNumber(_qty_skiblat)
                                dr("velt") = SetNumber(_velt_skiblat)
                                _yes_no = calc_sb_opsi.EditValue
                                _o = SetNumber(calc_sb_bhn_panjang.EditValue) * SetNumber(calc_sb_bhn_lebar.EditValue) / 100000
                                _opsi = SetNumber(calc_opsi_isi.GetColumnValue("nilai"))
                                _v = SetNumber(GetDataColumn("select nilai from cetak_opsi_detail_cetak where kode=" & SetSetring(dr("jenis"))))
                                _p = SetNumber(GetDataColumn("select isi_p from cetak_spec_isi where isi_jenis=" & SetSetring(calc_sb_jns_bahan.EditValue)))
                                _i = SetNumber(GetDataColumn("select nilai from cetak_warna_tinta where warna=" & SetSetring(dr("cetak_item"))))
                                dr("kg") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * _o * _opsi * _v * _p * _i / 1000 * SetNumber(dr("warna")) / 100 * SetNumber(calc_sb_jml_warna.EditValue)
                                dr("nilai") = SetNumber(dr("kg")) * SetNumber(dr("harga_kg")) * _yes_no * SetNumber(dr("opsi"))
                                _yes_no = 1
                            End If
                        ElseIf dr("cetak_sub_group") = "tinta_cover" Then
                            If SetNumber(calc_cv_jml_warna.EditValue) > 0 And SetNumber(calc_cv_bhn_panjang.EditValue) > 0 And SetNumber(calc_cv_bhn_lebar.EditValue) > 0 Then
                                Dim _o, _opsi, _v, _p, _i As Double
                                If dr("jenis") = "" Then
                                    dr("jenis") = "Blok"
                                End If
                                dr("qty") = SetNumber(_qty_cover)
                                dr("velt") = SetNumber(_velt_cover)
                                _yes_no = calc_cv_opsi.EditValue
                                _o = SetNumber(calc_cv_bhn_panjang.EditValue) * SetNumber(calc_cv_bhn_lebar.EditValue) / 100000
                                _opsi = SetNumber(calc_opsi_isi.GetColumnValue("nilai"))
                                _v = SetNumber(GetDataColumn("select nilai from cetak_opsi_detail_cetak where kode=" & SetSetring(dr("jenis"))))
                                _p = SetNumber(GetDataColumn("select isi_p from cetak_spec_isi where isi_jenis=" & SetSetring(calc_cv_jns_bahan.EditValue)))
                                _i = SetNumber(GetDataColumn("select nilai from cetak_warna_tinta where warna=" & SetSetring(dr("cetak_item"))))
                                dr("kg") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * _o * _opsi * _v * _p * _i / 1000 * SetNumber(dr("warna")) / 100 * SetNumber(calc_cv_jml_warna.EditValue)
                                dr("nilai") = SetNumber(dr("kg")) * SetNumber(dr("harga_kg")) * _yes_no * SetNumber(dr("opsi"))
                                _yes_no = 1
                            End If
                           
                        ElseIf dr("cetak_sub_group") = "cetak" Then
                            Dim _v, _naik As Double
                            If dr("cetak_sub_order") = 1 Then
                                If dr("jenis") = "" Then
                                    dr("jenis") = "Perfecting"
                                End If
                            Else
                                If dr("jenis") = "" Then
                                    dr("jenis") = "Multi"
                                End If
                            End If
                            If dr("cetak_sub_order") = 1 Then
                                dr("qty") = SetNumber(_qty_isi)
                                dr("velt") = SetNumber(_velt_isi)
                                dr("kg") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Printing' and  lower(ws_sub)=" & SetSetring(dr("jenis").ToString.ToLower))) * SetNumber(calc_isi_jml_warna.EditValue) * 2
                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                                End If

                                _naik = SetNumber(calc_isi_naik_cetak.EditValue)
                                _yes_no = calc_isi_opsi.EditValue

                                _v = SetNumber(GetDataColumn("select v_biaya_cetak from cetak_plate where plate=" & SetSetring(calc_jns_mesin.EditValue)))

                            ElseIf dr("cetak_sub_order") = 2 Then
                                dr("qty") = SetNumber(_qty_isi)
                                dr("velt") = SetNumber(0)
                                dr("kg") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Printing' and  lower(ws_sub)=" & SetSetring(dr("jenis").ToString.ToLower))) * 2
                                'dr("insheet") = SetNumber(calc_isi_insheet.EditValue)

                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                                End If

                                _naik = SetNumber(calc_isi_naik_cetak.EditValue)
                                _yes_no = calc_isi_opsi.EditValue

                                _v = SetNumber(GetDataColumn("select v_biaya_cetak from cetak_plate where plate=" & SetSetring(calc_jns_mesin.EditValue)))

                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("qty") = SetNumber(_qty_skiblat)
                                dr("velt") = SetNumber(_velt_skiblat)
                                dr("kg") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Printing' and  lower(ws_sub)=" & SetSetring(dr("jenis").ToString.ToLower))) * SetNumber(calc_sb_jml_warna.EditValue)
                                'dr("insheet") = SetNumber(calc_sb_insheet.EditValue)

                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_sb_insheet.EditValue)
                                End If

                                _naik = SetNumber(calc_sb_naik_cetak.EditValue)
                                _yes_no = calc_sb_opsi.EditValue

                                _v = SetNumber(GetDataColumn("select v_biaya_cetak from cetak_plate where plate=" & SetSetring(calc_sb_jns_mesin.EditValue)))

                            ElseIf dr("cetak_sub_order") = 4 Then
                                dr("qty") = SetNumber(_qty_cover)
                                dr("velt") = SetNumber(_velt_cover)
                                dr("kg") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Printing' and  lower(ws_sub)=" & SetSetring(dr("jenis").ToString.ToLower))) * SetNumber(calc_cv_jml_warna.EditValue)
                                'dr("insheet") = SetNumber(calc_cv_insheet.EditValue)

                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                End If

                                _naik = SetNumber(calc_cv_naik_cetak.EditValue)
                                _yes_no = calc_cv_opsi.EditValue

                                _v = SetNumber(GetDataColumn("select v_biaya_cetak from cetak_plate where plate=" & SetSetring(calc_cv_jns_mesin.EditValue)))

                            End If



                            If _naik > 0 Then
                                dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("kg")) * SetNumber(dr("insheet")) * SetNumber(_v) / SetNumber(_naik) / 100 * _yes_no * SetNumber(dr("opsi"))
                                _yes_no = 1
                            End If

                        End If
                        dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), dr("nilai"))
                    Next
                    ds_edit.AcceptChanges()
                    gv_edit_cetak.BestFitColumns()

                    Dim _velt_lipat As Double = 0

                    For Each dr As DataRow In ds_edit.Tables("insert_edit_calc").Rows
                        If dr("cetak_sub_group") = "isi" Then

                            dr("qty") = SetNumber(calc_oplah.EditValue)

                            If dr("cetak_sub_order") = 1 Then 'sortir

                                dr("velt") = SetNumber(calc_isi_jml.EditValue) / _
                                    SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue)))

                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inline-" & calc_ukuran.EditValue & "-Isi"))) ' QC Inline--Isi 

                            ElseIf dr("cetak_sub_order") = 2 Then 'lipat
                                Dim _opsi_lipat As Double
                                _opsi_lipat = SetNumber(GetDataColumn("select nilai from cetak_opsi_lipat where kode=" & SetSetring(calc_ukuran.EditValue)))

                                dr("velt") = SetNumber(calc_isi_jml.EditValue) / _
                                   SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue))) * _opsi_lipat
                                '_velt_lipat = SetNumber(calc_isi_jml.EditValue) / _
                                '   SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue))) * _opsi_lipat
                                _velt_lipat = dr("velt")

                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Lipat-" & calc_ukuran.EditValue & "-Isi")))
                            ElseIf dr("cetak_sub_order") = 3 Then 'sisip
                                Dim _opsi_lipat As Double
                                _opsi_lipat = SetNumber(GetDataColumn("select nilai from cetak_opsi_lipat where kode=" & SetSetring(calc_ukuran.EditValue)))


                                dr("velt") = IIf(calc_ukuran.EditValue.ToString.ToLower = "a4", SetNumber(calc_isi_jml.EditValue) / _
                                   SetNumber(GetDataColumn("select nilai from cetak_imposisi_isi where kode=" & SetSetring(calc_ukuran.EditValue))) * _opsi_lipat / 2, 1)

                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where lower(kode)=" & SetSetring("Sisip-" & calc_ukuran.EditValue & "-Isi").ToLower))
                            ElseIf dr("cetak_sub_order") = 4 Then 'susun
                                dr("velt") = IIf(calc_ukuran.EditValue.ToString.ToLower = "a4", _velt_lipat / 2, _velt_lipat) ' SetNumber(1)

                                If SetNumber(dr("velt")) <> 0 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Susun (" _
                                              & dr("jenis") & ")-" & calc_ukuran.EditValue & "-Isi"))) / SetNumber(dr("velt"))
                                End If
                              


                            ElseIf dr("cetak_sub_order") = 5 Then
                                dr("velt") = SetNumber(1)
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Cutting Final' and  lower(ws_sub)=" _
                                                 & SetSetring(calc_ukuran.EditValue.ToString.ToLower)))
                            ElseIf dr("cetak_sub_order") = 6 Then
                                dr("velt") = SetNumber(1)
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inc-" & calc_ukuran.EditValue & "-Isi")))
                            ElseIf dr("cetak_sub_order") = 7 Then
                                dr("velt") = SetNumber(1)
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Outgoing-" & calc_ukuran.EditValue & "-Isi")))
                            End If
                            If SetNumber(dr("insheet")) = 0 Then
                                dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                            End If

                            dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _yes_no * SetNumber(dr("opsi")))
                            _yes_no = 1
                        ElseIf dr("cetak_sub_group") = "proses_isi" Then
                            Dim _opsi As Double = 1
                            dr("qty") = SetNumber(calc_oplah.EditValue)
                            dr("velt") = SetNumber(1)
                            If dr("cetak_sub_order") = 1 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Jahit (" & dr("jenis") & ")-" & calc_ukuran.EditValue & "-Isi")))
                                If calc_jns_buku.EditValue.ToString.ToLower = "Softcover Stitching".ToLower Then
                                    _opsi = 0
                                Else
                                    _opsi = 1
                                End If
                            ElseIf dr("cetak_sub_order") = 2 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Jahit (Kawat)-" & calc_ukuran.EditValue & "-Isi")))
                                If calc_jns_buku.EditValue.ToString.ToLower = "Softcover Stitching".ToLower Then
                                    _opsi = 1
                                Else
                                    _opsi = 0
                                End If
                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Bloklem-" & calc_ukuran.EditValue & "-Isi")))
                                '_opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                            ElseIf dr("cetak_sub_order") = 4 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Psg Siblat-" & calc_ukuran.EditValue & "-Isi")))
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                            ElseIf dr("cetak_sub_order") = 5 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Roundback-" & calc_ukuran.EditValue & "-Isi")))
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                            ElseIf dr("cetak_sub_order") = 6 Then
                                dr("harga") = SetNumber(GetDataColumn("select harga_bahan_accessories from cetak_harga_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Accessories-" & calc_ukuran.EditValue & "-Isi")))
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                            ElseIf dr("cetak_sub_order") = 7 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Necking-" & calc_ukuran.EditValue & "-Isi")))
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                            End If
                            If SetNumber(dr("insheet")) = 0 Then
                                dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                            End If

                            dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + SetNumber(dr("harga"))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi")))
                            'If dr("harga") > 0 Then
                            '    dr("nilai") = dr("nilai") * dr("harga")
                            'End If
                            _yes_no = 1
                        ElseIf dr("cetak_sub_group") = "proses_packing" Then
                            dr("qty") = SetNumber(calc_oplah.EditValue)
                            dr("velt") = SetNumber(1)
                            If dr("cetak_sub_order") = 1 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("PC-" & SetString(dr("jenis")) & "-Isi"))) ' calc_ukuran.EditValue
                            ElseIf dr("cetak_sub_order") = 2 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Welding-" & calc_ukuran.EditValue & "-Isi")))
                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Shrink-" & calc_ukuran.EditValue & "-Isi")))
                            ElseIf dr("cetak_sub_order") = 4 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Packing-" & SetString(dr("jenis")) & "-Isi"))) 'calc_ukuran.EditValue
                            End If
                            If SetNumber(dr("insheet")) = 0 Then
                                dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                            End If

                            dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _yes_no * SetNumber(dr("opsi")))
                            _yes_no = 1
                        ElseIf dr("cetak_sub_group") = "assembly" Then
                            Dim _opsi As Double = 1
                            dr("qty") = SetNumber(calc_oplah.EditValue)
                            dr("velt") = SetNumber(1)
                            If dr("cetak_sub_order") = 1 Then
                                dr("harga") = SetNumber(GetDataColumn("select harga_assembly_hc from cetak_harga_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Assembly (HC)-" & calc_ukuran.EditValue & "-Isi")))
                                If calc_jns_buku.EditValue.ToString.ToLower = "Hardcover".ToLower Then
                                    _opsi = 1
                                Else
                                    _opsi = 0
                                End If
                            ElseIf dr("cetak_sub_order") = 2 Then
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Assembly (SC)-" & calc_ukuran.EditValue & "-Isi")))
                                If calc_jns_buku.EditValue.ToString.ToLower = "Softcover Binding".ToLower Then
                                    _opsi = 1
                                Else
                                    _opsi = 0
                                End If
                            ElseIf dr("cetak_sub_order") = 3 Then
                                dr("harga") = SetNumber(GetDataColumn("select harga_assembly_jaket from cetak_harga_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Assembly (Jaket)-" & calc_ukuran.EditValue & "-Isi")))
                                If calc_jns_buku.EditValue.ToString.ToLower = "Jaket".ToLower Then
                                    _opsi = 1
                                Else
                                    _opsi = 0
                                End If
                            End If
                            If SetNumber(dr("insheet")) = 0 Then
                                dr("insheet") = SetNumber(calc_isi_insheet.EditValue)
                            End If

                            dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + SetNumber(dr("harga"))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi")))
                            'If dr("harga") > 0 Then
                            '    dr("nilai") = dr("nilai") * dr("harga")
                            'End If
                            _yes_no = 1
                        ElseIf dr("cetak_sub_group") = "skiblat" Then
                            If SetNumber(calc_sb_naik_cetak.EditValue) > 0 And SetNumber(calc_sb_jml_design.EditValue) > 0 Then
                                Dim _opsi As Double = 1
                                Dim _pembagi As Double = 1
                                Dim _pengali As Double = 1
                                dr("velt") = SetNumber(1)
                                If dr("cetak_sub_order") = 1 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inline-" & calc_ukuran.EditValue & "-Isi")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_sb_naik_cetak.EditValue)
                                    _pembagi = SetNumber(calc_sb_naik_cetak.EditValue)
                                ElseIf dr("cetak_sub_order") = 2 Then
                                    dr("velt") = SetNumber(calc_sb_jml_design.EditValue)
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode='Cutting velt-plano-Isi'"))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_sb_naik_cetak.EditValue) / SetNumber(calc_sb_jml_design.EditValue)
                                    _pembagi = 1
                                    _pengali = 4
                                ElseIf dr("cetak_sub_order") = 3 Then
                                    dr("velt") = SetNumber(calc_sb_jml_design.EditValue)
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Rel-" & calc_ukuran.EditValue & "-Cover")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_sb_naik_cetak.EditValue) / SetNumber(calc_sb_jml_design.EditValue)
                                ElseIf dr("cetak_sub_order") = 4 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inline-" & calc_ukuran.EditValue & "-Isi")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_sb_naik_cetak.EditValue) / SetNumber(calc_sb_jml_design.EditValue)
                                End If

                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_sb_insheet.EditValue)
                                End If

                                dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi")))
                                _yes_no = 1
                            End If

                        ElseIf dr("cetak_sub_group") = "cover" Then
                            If SetNumber(calc_cv_naik_cetak.EditValue) > 0 And SetNumber(calc_cv_jml_design.EditValue) > 0 Then
                                Dim _opsi As Double = 1
                                If SetNumber(dr("insheet")) = 0 Then
                                    dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                End If

                                Dim _pembagi As Double = 1
                                Dim _pengali As Double = 1
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca_cover where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))

                                If dr("cetak_sub_order") = 1 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inline-" & calc_ukuran.EditValue & "-Isi")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_naik_cetak.EditValue)
                                    dr("velt") = SetNumber(1)
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi")) / SetNumber(calc_cv_naik_cetak.EditValue)

                                ElseIf dr("cetak_sub_order") = 2 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode='Cutting velt-plano-Isi'"))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_naik_cetak.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    _pembagi = 1
                                    _pengali = 4
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi"))

                                ElseIf dr("cetak_sub_order") = 3 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Rel-" & calc_ukuran.EditValue & "-Cover")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_naik_cetak.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))

                                   
                                ElseIf dr("cetak_sub_order") = 4 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("QC Inline-" & calc_ukuran.EditValue & "-Isi")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_naik_cetak.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi"))
                                ElseIf dr("cetak_sub_order") = 5 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Cutting velt-plano-Isi")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue)
                                    dr("velt") = SetNumber(1)
                                    If SetNumber(dr("insheet")) = 0 Then
                                        dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    End If

                                    _pembagi = 648
                                    _pengali = 4
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi"))
                                ElseIf dr("cetak_sub_order") = 6 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where ws='Cutting Karton' and  lower(ws_sub)=" _
                                                    & SetSetring(calc_ukuran.EditValue.ToString.ToLower)))
                                    dr("qty") = SetNumber(calc_oplah.EditValue)
                                    dr("velt") = SetNumber(1)
                                    If SetNumber(dr("insheet")) = 0 Then
                                        dr("insheet") = SetNumber(calc_kr_insheet.EditValue)
                                    End If

                                 
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * SetNumber(dr("bop_btkl")) * SetNumber(dr("insheet")) / 100 * _opsi / _pembagi * _pengali * _yes_no * SetNumber(dr("opsi"))
                                End If

                                _yes_no = 1
                            End If
                        ElseIf dr("cetak_sub_group") = "laminasi" Then
                            If SetNumber(calc_cv_jml_design.EditValue) > 0 Then
                                If dr("jenis") = "" Then
                                    dr("jenis") = "Doff 18"
                                End If

                                dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                Dim _pembagi As Double = SetNumber(GetDataColumn("select naik_cetak from cetak_laminasi_naik_cetak where kode=" & SetSetring(calc_ukuran.EditValue)))
                                If _pembagi <> 0 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Laminasi Doff-" & calc_ukuran.EditValue & "-Cover"))) / _pembagi
                                Else
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Laminasi Doff-" & calc_ukuran.EditValue & "-Cover")))
                                End If

                                dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_opsi_pasca where kode=" & SetSetring(dr("jenis"))))
                                dr("lebar") = SetNumber(GetDataColumn("select laminasi from cetak_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                If dr("insheet") = 0 Then
                                    dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                End If


                                'Dim _opsi_laminasi As Double
                                Dim _opsi As Double = 1
                                '_opsi_laminasi = SetNumber(GetDataColumn("select nilai from cetak_opsi_laminasi where kode=" & SetSetring(calc_ukuran.EditValue)))
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca_cover where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))

                                dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")) * SetNumber(dr("lebar")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                _yes_no = 1


                            End If
                        ElseIf dr("cetak_sub_group") = "proses_cover" Then
                            If SetNumber(calc_cv_jml_design.EditValue) > 0 Then
                                'Dim _opsi_laminasi As Double = 1
                                Dim _opsi As Double = 1
                                If dr("cetak_sub_order") = 1 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Foil-" & calc_ukuran.EditValue & "-Cover")))
                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_opsi_pasca where kode=" & SetSetring("Foil")))
                                    '_opsi_laminasi = SetNumber(GetDataColumn("select nilai from cetak_opsi_laminasi where kode=" & SetSetring(calc_ukuran.EditValue)))
                                    dr("lebar") = SetNumber(GetDataColumn("select foil from cetak_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                ElseIf dr("cetak_sub_order") = 2 Then
                                    If dr("jenis") = "" Then
                                        dr("jenis") = "UV"
                                    End If

                                    Dim _pembagi As Double = SetNumber(GetDataColumn("select naik_cetak from cetak_laminasi_naik_cetak where kode=" & SetSetring(calc_ukuran.EditValue)))

                                    If _pembagi <> 0 Then
                                        dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Spot UV-" & calc_ukuran.EditValue & "-Cover"))) / _pembagi
                                    Else
                                        dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Spot UV-" & calc_ukuran.EditValue & "-Cover")))
                                    End If


                                    dr("qty") = SetNumber(calc_oplah.EditValue) / SetNumber(calc_cv_jml_design.EditValue)
                                    dr("velt") = SetNumber(calc_cv_jml_design.EditValue)
                                    dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_opsi_spot where kode=" & SetSetring(calc_ukuran.EditValue)))
                                    dr("lebar") = SetNumber(GetDataColumn("select spot from cetak_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                End If

                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca_cover where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))

                                'dr("insheet") = SetNumber(calc_cv_insheet.EditValue)

                                If dr("insheet") = 0 Then
                                    dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                End If

                                dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")) * SetNumber(dr("lebar")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                _yes_no = 1
                            End If

                        ElseIf dr("cetak_sub_group") = "casemaker" Then
                            If SetNumber(calc_cv_insheet.EditValue) > 0 Then
                                Dim _opsi_laminasi As Double = 1
                                dr("qty") = SetNumber(calc_oplah.EditValue)
                                dr("velt") = SetNumber(1)

                                Dim _opsi As Double = 1
                                _opsi = SetNumber(GetDataColumn("select nilai from cetak_proses_pasca_cover where kode=" & SetSetring(dr("cetak_item") & calc_jns_buku.EditValue)))
                                If dr("cetak_sub_order") = 1 Then
                                    If dr("jenis") = "" Then
                                        dr("jenis") = "Mesin"
                                    End If
                                    If dr("insheet") = 0 Then
                                        dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    End If
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode='Casemaker (" & (dr("jenis")) & ")-" & calc_ukuran.EditValue & "-Cover'"))
                                    dr("harga") = SetNumber(GetDataColumn("select nilai from cetak_opsi_pasca where kode=" & SetSetring("Casemaker")))

                                    ' _opsi_laminasi = SetNumber(GetDataColumn("select nilai from cetak_opsi_laminasi where kode=" & SetSetring(calc_ukuran.EditValue)))
                                    dr("lebar") = SetNumber(GetDataColumn("select casemaker from cetak_bahan where kode=" & SetSetring(calc_ukuran.EditValue)))
                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")) * SetNumber(dr("lebar")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                ElseIf dr("cetak_sub_order") = 2 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Debbos-" & calc_ukuran.EditValue & "-Cover")))
                                    If dr("insheet") = 0 Then
                                        dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    End If

                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                ElseIf dr("cetak_sub_order") = 3 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Emboss-" & calc_ukuran.EditValue & "-Cover")))
                                    If dr("insheet") = 0 Then
                                        dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    End If

                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                ElseIf dr("cetak_sub_order") = 4 Then
                                    dr("bop_btkl") = SetNumber(GetDataColumn("select grand_total from cetak_bop_btkl where kode=" & SetSetring("Pilung-" & calc_ukuran.EditValue & "-Cover")))
                                    If dr("insheet") = 0 Then
                                        dr("insheet") = SetNumber(calc_cv_insheet.EditValue)
                                    End If

                                    dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("velt")) * (SetNumber(dr("bop_btkl")) + (SetNumber(dr("harga")))) * SetNumber(dr("insheet")) / 100 * _opsi * _yes_no * SetNumber(dr("opsi"))
                                End If
                                _yes_no = 1
                            End If

                        End If
                        dr("nilai") = IIf(SetNumber(dr("outsource")) > 0, SetNumber(dr("outsource")), dr("nilai"))
                    Next
                    ds_edit.AcceptChanges()
                    gv_edit_pasca.BestFitColumns()

                    For Each dr As DataRow In ds_edit.Tables("insert_edit_tambahan").Rows
                        If SetNumber(dr("qty")) = 0 Then
                            dr("qty") = SetNumber(calc_oplah.EditValue) * SetNumber(dr("tambahan_value")) / 100
                        End If
                        dr("nilai") = SetNumber(dr("qty")) * SetNumber(dr("harga")) * SetNumber(dr("insheet")) / 100
                    Next
                    ds_edit.AcceptChanges()
                    gv_edit_tambahan.BestFitColumns()

                    Dim _nilai_tabel As Double = 0
                    For Each dr As DataRow In ds_edit.Tables("insert_edit").Rows
                        _nilai_tabel += SetNumber(dr("nilai"))
                    Next
                    For Each dr As DataRow In ds_edit.Tables("insert_edit_detail").Rows
                        _nilai_tabel += SetNumber(dr("nilai"))
                    Next
                    For Each dr As DataRow In ds_edit.Tables("insert_edit_calc").Rows
                        _nilai_tabel += SetNumber(dr("nilai"))
                    Next
                    For Each dr As DataRow In ds_edit.Tables("insert_edit_tambahan").Rows
                        _nilai_tabel += SetNumber(dr("nilai"))
                    Next

                    calc_biaya_produksi.EditValue = SetNumber(calc_kr_nilai.EditValue) + SetNumber(calc_cv_nilai.EditValue) + SetNumber(calc_sb_nilai.EditValue) _
                            + SetNumber(calc_isi_nilai.EditValue) + SetNumber(_nilai_tabel)



                    calc_nilai_margin.EditValue = SetNumber(calc_biaya_produksi_nilai_margin.EditValue) - SetNumber(calc_biaya_produksi2.EditValue) 'calc_margin.EditValue * calc_biaya_produksi.EditValue / 100

                    calc_biaya_produksi2.EditValue = calc_biaya_produksi.EditValue
                    'calc_nilai_margin.EditValue = calc_nilai_margin.EditValue + calc_biaya_produksi.EditValue

                    calc_biaya_produksi_nilai_margin.EditValue = SetNumber(calc_biaya_produksi2.EditValue) / (1 - (SetNumber(calc_margin.EditValue) / 100)) 'calc_biaya_produksi2.EditValue + calc_nilai_margin.EditValue
                    calc_nilai_ppn.EditValue = SetNumber(calc_ppn.EditValue) * SetNumber(calc_biaya_produksi_nilai_margin.EditValue) / 100
                    calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue = SetNumber(calc_biaya_produksi_nilai_margin.EditValue) + SetNumber(calc_nilai_ppn.EditValue)
                    If SetNumber(calc_oplah.EditValue) > 0 Then
                        calc_biaya_cetak_pcs.EditValue = SetNumber(calc_biaya_produksi_nilai_margin_nilai_ppn.EditValue) / SetNumber(calc_oplah.EditValue)
                    End If
                    If SetNumber(calc_harga_jaket.EditValue) > 0 And calc_opsi_isi.EditValue = "Jaket" Then
                        calc_nilai_jaket.EditValue = SetNumber(calc_oplah.EditValue) * SetNumber(calc_harga_jaket.EditValue) / SetNumber(calc_oplah.EditValue) * (1 + (SetNumber(calc_margin_jaket.EditValue) / 100))
                    Else
                        calc_nilai_jaket.EditValue = 0.0
                    End If
                    calc_biaya_cetak_final.EditValue = SetNumber(calc_biaya_cetak_pcs.EditValue) + SetNumber(calc_nilai_jaket.EditValue)
                    calc_biaya_per_buku.EditValue = calc_biaya_cetak_final.EditValue
                End If
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub calc_oplah_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_oplah.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_jml_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_jml.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_bahan_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_bahan.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_bhn_qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_bhn_qty.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_panjang_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_panjang.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_lebar_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_lebar.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_insheet_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_insheet.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_insheet_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calc_sb_insheet.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_naik_cetak_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_naik_cetak.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_jns_bahan_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_jns_bahan.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_bahan_qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_bahan_qty.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_bhn_panjang_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_bhn_panjang.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_bhn_lebar_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_bhn_lebar.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_insheet_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_insheet.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_naik_cetak_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_naik_cetak.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_jns_bahan_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_jns_bahan.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_bahan_qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_bahan_qty.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_bhn_panjang_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_bhn_panjang.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_bhn_lebar_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_bhn_lebar.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_insheet_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_insheet.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_jns_bahan_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_jns_bahan.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_jns_bhn_qty_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_jns_bhn_qty.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_bhn_panjang_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_bhn_panjang.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_bhn_lebar_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_bhn_lebar.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_jml_warna_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_jml_warna.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_jml_warna_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_jml_warna.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_jml_design_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_jml_design.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_jml_warna_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_jml_warna.LostFocus
        hitung()
    End Sub

    Private Sub gv_edit_cetak_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_cetak.CellValueChanged
        Try
            hitung()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_pasca_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_pasca.CellValueChanged
        hitung()
    End Sub

    Private Sub gv_edit_CellValueChanged1(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        hitung()
    End Sub

    Private Sub calc_opsi_isi_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_opsi_isi.LostFocus
        hitung()
    End Sub

    Private Sub calc_isi_opsi_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_opsi.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_opsi_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_opsi.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_opsi_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_opsi.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_opsi_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_opsi.LostFocus
        hitung()
    End Sub

    Private Sub calc_ppn_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_ppn.LostFocus
        hitung()
    End Sub

    Private Sub gv_edit_pasca_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_edit_pasca.SelectionChanged
        'If ds_edit.Tables("insert_edit_calc").Rows(BindingContext(ds_edit.Tables("insert_edit_calc")).Position).Item("cetak_sub_group") = "proses_isi" Then
        '    If ds_edit.Tables("insert_edit_calc").Rows(BindingContext(ds_edit.Tables("insert_edit_calc")).Position).Item("cetak_sub_order") = 1 Then
        '        gv_edit_pasca.Columns("jenis").ColumnEdit = init_le_repo("opsi_detail_pasca", "tipe ~~* '%J%' ") 'or tipe ~~* '%A%'
        '    End If
        'ElseIf ds_edit.Tables("insert_edit_calc").Rows(BindingContext(ds_edit.Tables("insert_edit_calc")).Position).Item("cetak_sub_group") = "proses_packing" Then
        '    If ds_edit.Tables("insert_edit_calc").Rows(BindingContext(ds_edit.Tables("insert_edit_calc")).Position).Item("cetak_sub_order") = 1 Then
        '        gv_edit_pasca.Columns("jenis").ColumnEdit = init_le_repo("opsi_detail_pasca", "tipe ~~* '%P%' ") 'or tipe ~~* '%A%'
        '    ElseIf ds_edit.Tables("insert_edit_calc").Rows(BindingContext(ds_edit.Tables("insert_edit_calc")).Position).Item("cetak_sub_order") = 4 Then
        '        gv_edit_pasca.Columns("jenis").ColumnEdit = init_le_repo("opsi_detail_pasca", "tipe ~~* '%P%' ") 'or tipe ~~* '%A%'
        '    End If
        'End If
    End Sub

    Private Sub gv_edit_tambahan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit_tambahan.CellValueChanged
        hitung()
    End Sub

    Private Sub calc_isi_outsource_nilai_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_isi_outsource_nilai.LostFocus
        hitung()
    End Sub

    Private Sub calc_sb_outsource_nilai_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_sb_outsource_nilai.LostFocus
        hitung()
    End Sub

    Private Sub calc_cv_outsource_nilai_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_cv_outsource_nilai.LostFocus
        hitung()
    End Sub

    Private Sub calc_kr_outsource_nilai_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calc_kr_outsource_nilai.LostFocus
        hitung()
    End Sub

    Private Sub be_calc_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_calc_code.ButtonClick
        Try
            'XtraTabControl3.SelectedTabPageIndex = 2
            'XtraTabControl3.SelectedTabPageIndex = 3
            'XtraTabControl3.SelectedTabPageIndex = 0
            calc_berat.Focus()
            Dim frm As New FCalculateCostSearch
            frm.set_win(Me)
            frm._obj = be_calc_code
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub calc_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles calc_pt_id.ButtonClick
        Try
          
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._obj = calc_pt_id
            frm._en_id = calc_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtTest.Click
        Try
            calc_oplah.EditValue = 10000.0
            calc_isi_jml.EditValue = 656.0
            calc_isi_bahan.EditValue = "QPP IK"
            calc_isi_bhn_qty.EditValue = CDbl(50)
            calc_isi_panjang.EditValue = 61D
            calc_isi_lebar.EditValue = 86D
            calc_isi_jml_warna.EditValue = 2D
            calc_isi_insheet.EditValue = 104D
            calc_isi_naik_cetak.EditValue = 1D
            calc_jns_mesin.EditValue = "Plano"
            calc_sb_jns_bahan.EditValue = "Art Carton"
            calc_sb_jml_design.EditValue = 1D
            calc_sb_bahan_qty.EditValue = 260D
            calc_sb_bhn_panjang.EditValue = 65D
            calc_sb_bhn_lebar.EditValue = 100D
            calc_sb_jml_warna.EditValue = 1D
            calc_sb_insheet.EditValue = 104D
            calc_sb_naik_cetak.EditValue = 1D
            calc_sb_jns_mesin.EditValue = "1/2 Plano"

            calc_cv_jml_design.EditValue = 1D
           
            calc_cv_jns_bahan.EditValue = "Art Paper"
            calc_cv_bahan_qty.EditValue = 150D
            calc_cv_bhn_panjang.EditValue=79d
            calc_cv_bhn_lebar.EditValue = 109D
            calc_cv_jml_warna.EditValue = 4D
            calc_cv_insheet.EditValue = 104D
            calc_cv_naik_cetak.EditValue = 1D
            calc_cv_jns_mesin.EditValue = "1/4 Plano"


            calc_kr_jns_bahan.EditValue = "Karton 30 grey Nore"
            calc_kr_jns_bhn_qty.EditValue = 1500D
            calc_kr_bhn_panjang.EditValue = 65D
            calc_kr_bhn_lebar.EditValue = 77D
            calc_kr_insheet.EditValue = 104D



            'Dim x As Object
            'x = calc_isi_panjang.EditValue
            'Dim y As Object
            'y = calc_isi_lebar.EditValue
        Catch ex As Exception

        End Try
    End Sub
End Class
