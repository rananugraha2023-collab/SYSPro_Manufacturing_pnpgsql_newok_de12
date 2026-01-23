Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls


Public Class FTaxExportReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARPaymentReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim _file As String = AskSaveAsFile("Excel Files | *.xls")
        If _file <> "" Then
            gv_header.ExportToXls(_file)
            Box("Export data sucess")
            OpenFile(_file)
        End If
       
    End Function
    Public Overrides Sub format_grid()
        add_column_copy(gv_header, "Baris", "tax_baris", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Tanggal Faktur", "tax_ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_header, "Jenis Faktur", "tax_jenis_faktur", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_header, "Kode Transaksi", "tax_kode_transaksi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Keterangan Tambahan", "tax_ket_tambahan", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_header, "Dokumen Pendukung", "tax_dok_pendukung", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Referensi", "tax_reff", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_header, "Cap Fasilitas", "tax_cap_fasilitas", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_header, "ID TKU Penjual", "tax_id_tku_penjual", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "NPWP/NIK Pembeli", "tax_npwp_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Jenis ID Pembeli", "tax_jenis_id_pembeli", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_header, "Negara Pembeli", "tax_negara_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Nomor Dokumen Pembeli", "tax_no_dok_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Nama Pembeli", "tax_nama_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Alamat Pembeli", "tax_alamat_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "Email Pembeli", "tax_email_pembeli", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_header, "ID TKU Pembeli", "tax_id_tku_pembeli", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "Baris", "taxd_baris", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Referensi", "tax_reff", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Barang/Jasa", "taxd_barang_jasa", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Kode Barang Jasa", "taxd_kode_barang_jasa", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Nama Barang/Jasa", "taxd_nama_barang_jasa", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Nama Satuan Ukur", "taxd_nama_satuan_ukur", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Harga Satuan", "taxd_harga_satuan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Jumlah Barang Jasa", "taxd_jumlah", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Total Diskon", "taxd_diskon", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_detail, "DPP", "taxd_dpp_total", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "DPP Nilai Lain", "taxd_dpp_nilai_lain_total", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tarif PPN", "taxd_tarif_ppn_total", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPN", "taxd_ppn_total", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Tarif PPnBM", "taxd_tarif_ppnbm", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "PPnBM", "taxd_ppnbm", DevExpress.Utils.HorzAlignment.Default)

        Dim dt_bantu As DataTable = New DataTable
        Dim sql As String = "Select db_code, db_host, db_database, db_port, db_user from public.telegram_db order by db_code"
        dt_bantu = GetTableData(sql, "SVR_Visitama")

        With le_syspro
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("db_code", "Kode", 20))
                .Properties.Columns.Add(New LookUpColumnInfo("db_host", 0))
                .Properties.Columns.Add(New LookUpColumnInfo("db_database", 0))
                .Properties.Columns.Add(New LookUpColumnInfo("db_port", 0))
                .Properties.Columns.Add(New LookUpColumnInfo("db_user", 0))


            End If
        End With


        le_syspro.Properties.DataSource = dt_bantu
        le_syspro.Properties.DisplayMember = dt_bantu.Columns("db_code").ToString
        le_syspro.Properties.ValueMember = dt_bantu.Columns("db_code").ToString
        le_syspro.ItemIndex = 0

        le_syspro.Properties.DropDownRows = 14
        le_syspro.Properties.PopupWidth = 500
        le_syspro.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
        le_syspro.Properties.BestFit()

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                Dim ssql As String = "SELECT    " _
                              & "  en_desc, " _
                              & "  so_code, " _
                              & "  so_date, " _
                              & "  ptnr_mstr.ptnr_name, COALESCE(ptnr_mstr.ptnr_npwp,'000000000000000') as ptnr_npwp, ptnr_mstr.ptnr_address_tax,ptnr_mstr.ptnr_email, " _
                              & "  ptnr_mstr.ptnr_code, " _
                              & "  soship_code, " _
                              & "  soship_date, " _
                              & "  si_desc, " _
                              & "  soship_is_shipment, " _
                              & "  soshipd_seq, " _
                              & "  cu_name, " _
                              & "  so_exc_rate, " _
                              & "  pt_code, sod_cost, " _
                              & "  pt_desc1, " _
                              & "  pt_desc2, " _
                              & "  sod_taxable, sod_tax_class, " _
                              & "  sod_tax_inc, sod_sales_unit * sod_qty as so_sales_unit, " _
                              & "  tax_mstr.code_name as tax_name, " _
                              & "  soshipd_qty * -1 as soshipd_qty, " _
                              & "  sod_price, " _
                              & " soshipd_qty * -1 * sod_price as sales_ttl, " _
                              & "  sod_disc, " _
                              & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                              & "   " _
                              & " case upper(sod_tax_inc) " _
                              & "  when 'N' then soshipd_qty * -1 * sod_price " _
                              & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp, " _
                              & "   " _
                              & "  case upper(sod_tax_inc) " _
                              & "  when 'N' then sod_price " _
                              & "  when 'Y' then sod_price * cast((100.0/110.0) as numeric(26,8)) " _
                              & "  end as price_fp1, " _
                              & "   " _
                              & "  case upper(sod_tax_inc) " _
                              & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                              & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc " _
                              & "  end as disc_fp, " _
                              & "   " _
                              & "  case upper(sod_tax_inc) " _
                              & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                              & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc) " _
                              & "  end as dpp, " _
                              & "   " _
                              & "  pl_desc,  " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000001' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))) * 0.11 " _
                              & "                    end " _
                              & "  end,0) as ppn_bayar, " _
                              & "   " _
                              & "  COALESCE(case pl_code " _
                              & "  when '990000000002' then " _
                              & "  					case upper(sod_tax_inc) " _
                              & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.11 " _
                              & "                    when 'Y' then (((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric(26,8)) * sod_disc))  " _
                              & "                    end " _
                              & "  end,0) as ppn_bebas, " _
                              & "               " _
                              & "  um_mstr.code_name as um_name, " _
                              & "  loc_desc, " _
                              & "  reason_mstr.code_name as reason_name, " _
                              & "  ar_code, " _
                              & "  ar_date,ar_eff_date, " _
                              & "  ti_code, " _
                              & "  sales_mstr.ptnr_name as sales_name, cmaddr_name, cmaddr_npwp, cmaddr_tax_line_1, cmaddr_tax_line_2, " _
                              & "  ti_date,pay_type.code_name as pay_type_desc, pi_desc, so_pay_type, trunc(so_total) as so_total, so_total_ppn " _
                              & "FROM  " _
                              & "  public.soship_mstr " _
                              & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                              & "  inner join so_mstr on so_oid = soship_so_oid " _
                              & "  inner join pi_mstr on so_pi_id = pi_id " _
                              & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                              & "  inner join ptnr_mstr sales_mstr on sales_mstr.ptnr_id = so_sales_person " _
                              & "  inner join en_mstr on en_id = soship_en_id " _
                              & "  inner join si_mstr on si_id = soship_si_id " _
                              & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                              & "  inner join pt_mstr on pt_id = sod_pt_id " _
                              & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                              & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                              & "  inner join cu_mstr on cu_id = so_cu_id " _
                              & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                              & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                              & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                              & "  left outer join ars_ship on ars_soshipd_oid = soshipd_oid " _
                              & "  inner join ar_mstr on ar_oid = ars_ar_oid " _
                              & "  left outer join tia_ar on tia_ar_oid = ar_oid " _
                              & "  left outer join ti_mstr on ti_oid = tia_ti_oid " _
                              & "  inner join pl_mstr on pl_id = pt_pl_id " _
                              & "  left outer join cmaddr_mstr on cmaddr_en_id = en_id " _
                              & "  where ar_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                              & "  and ar_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                              & "  AND  pay_type.code_usr_1 <> '0' and coalesce(so_trans_id,'') <> 'X'  " _
                              & "  order by ar_eff_date,ar_code,sod_seq"


                Dim dt_data As New DataTable
                dt_data = GetTableData(ssql, le_syspro.GetColumnValue("db_host"), le_syspro.GetColumnValue("db_database"), le_syspro.GetColumnValue("db_port"), le_syspro.GetColumnValue("db_user"))

                ssql = "select 0 as tax_baris, " _
                    & "  current_date as tax_ar_date, " _
                    & "  ''  as tax_jenis_faktur, " _
                    & "  ''  as tax_kode_transaksi, " _
                    & "  ''  as tax_ket_tambahan, " _
                    & "  ''  as tax_dok_pendukung, " _
                    & "  ''  as tax_reff, " _
                    & "  ''  as tax_cap_fasilitas, " _
                    & "  ''  as tax_id_tku_penjual, " _
                    & "  ''  as tax_npwp_pembeli, " _
                    & "  ''  as tax_jenis_id_pembeli, " _
                    & "  ''  as tax_negara_pembeli, " _
                    & "  ''  as tax_no_dok_pembeli, " _
                    & "  ''  as tax_nama_pembeli, " _
                    & "  ''  as tax_alamat_pembeli, " _
                    & "  ''  as tax_email_pembeli, " _
                    & "  '' as tax_id_tku_pembeli"

                Dim dt_header As New DataTable
                dt_header = GetTableData(ssql)
                dt_header.Rows.Clear()

                ssql = "select 0 as taxd_baris, " _
                   & "  ''  as taxd_barang_jasa, " _
                   & "  ''  as taxd_kode_barang_jasa, " _
                   & "  ''  as taxd_nama_barang_jasa, " _
                   & "  ''  as taxd_nama_satuan_ukur, " _
                   & "  ''  as tax_reff, " _
                   & "  0  as taxd_harga_satuan, " _
                   & "  0  as taxd_jumlah, " _
                   & "  0  as taxd_diskon, " _
                   & "  0  as taxd_dpp_total, " _
                   & "  0  as taxd_dpp_nilai_lain_total, " _
                   & "  0  as taxd_tarif_ppn_total, " _
                   & "  0  as taxd_ppn_total, " _
                   & "  0  as taxd_tarif_ppnbm, " _
                   & "  0  as taxd_ppnbm"

                Dim dt_detail As New DataTable
                dt_detail = GetTableData(ssql)
                dt_detail.Rows.Clear()


                Dim x As Integer = 0
                Dim _no_ar As String = ""
                For Each dr As DataRow In dt_data.Rows
                    If _no_ar <> dr("ar_code") Then
                        x = x + 1
                        _no_ar = dr("ar_code")
                        Dim _row As DataRow = dt_header.NewRow

                        _row("tax_baris") = x
                        _row("tax_ar_date") = dr("ar_eff_date")
                        _row("tax_reff") = dr("ar_code")
                        _row("tax_npwp_pembeli") = dr("ptnr_npwp").ToString.Replace(".", "").Replace("-", "")
                        _row("tax_jenis_id_pembeli") = IIf(dr("ptnr_npwp").ToString.StartsWith("32"), "NIK", IIf(dr("ptnr_npwp") = "000000000000000", "", "TIN"))
                        _row("tax_jenis_faktur") = "Normal"
                        _row("tax_negara_pembeli") = "IDN"
                        _row("tax_nama_pembeli") = dr("ptnr_name")
                        _row("tax_email_pembeli") = dr("ptnr_email")
                        _row("tax_alamat_pembeli") = dr("ptnr_address_tax")
                        _row("tax_id_tku_penjual") = dr("cmaddr_npwp").ToString.Replace(".", "").Replace("-", "") & "000000"
                        _row("tax_id_tku_pembeli") = dr("ptnr_npwp").ToString.Replace(".", "").Replace("-", "") & "000000"
                        _row("tax_kode_transaksi") = IIf(dr("ppn_bebas") > 0.0, "08", IIf(dr("ppn_bayar") > 0, "01", ""))
                        _row("tax_dok_pendukung") = dr("so_code")
                        _row("tax_ket_tambahan") = IIf(_row("tax_kode_transaksi") = "08", "TD.00510", "")
                        _row("tax_cap_fasilitas") = IIf(_row("tax_kode_transaksi") = "08", "TD.01110", "")

                        dt_header.Rows.Add(_row)


                    End If

                    Dim _rowd As DataRow = dt_detail.NewRow

                    _rowd("taxd_baris") = x
                    _rowd("tax_reff") = dr("ar_code")
                    _rowd("taxd_barang_jasa") = IIf(dr("ppn_bebas") > 0.0, "A", "")
                    _rowd("taxd_kode_barang_jasa") = IIf(dr("ppn_bebas") > 0.0, "490000", "")
                    _rowd("taxd_nama_barang_jasa") = dr("pt_desc1")
                    _rowd("taxd_nama_satuan_ukur") = IIf(dr("ppn_bebas") > 0.0, "UM.0021", "")
                    _rowd("taxd_harga_satuan") = dr("sod_price")
                    _rowd("taxd_diskon") = dr("disc_value")
                    _rowd("taxd_jumlah") = dr("soshipd_qty")
                    _rowd("taxd_dpp_total") = dr("dpp")
                    _rowd("taxd_dpp_nilai_lain_total") = dr("dpp") * (11.0 / 12.0)
                    _rowd("taxd_tarif_ppn_total") = 12
                    _rowd("taxd_ppn_total") = dr("dpp") * (11.0 / 12.0) * (12.0 / 100.0)
                    _rowd("taxd_tarif_ppnbm") = 0
                    _rowd("taxd_ppnbm") = 0
                    'UM.0021

                    dt_detail.Rows.Add(_rowd)
                  
                Next
                gc_header.DataSource = dt_header
                dt_header.AcceptChanges()
                gv_header.BestFitColumns()

                gc_detail.DataSource = dt_detail
                dt_detail.AcceptChanges()
                gv_detail.BestFitColumns()


                Box("Success")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
        Try
            Dim _file As String = AskSaveAsFile("Excel Files | *.xls")
            If _file <> "" Then
                gv_detail.ExportToXls(_file)
                Box("Export data sucess")
                OpenFile(_file)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
