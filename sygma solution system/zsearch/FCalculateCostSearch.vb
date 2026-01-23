Imports master_new.ModFunction

Public Class FCalculateCostSearch
    Public _row, _en_id As Integer
    Public _col As String
    Public _obj As Object


    Private Sub FCodeSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 600
        Me.Height = 360
        'te_search.Focus()
        pr_txttglawal.DateTime = master_new.PGSqlConn.CekTanggal
        pr_txttglakhir.DateTime = master_new.PGSqlConn.CekTanggal
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Tanggal", "calc_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column(gv_master, "Nomor", "calc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Judul", "calc_judul", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Oplah", "calc_oplah", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_master, "Jenis Buku", "calc_jns_buku", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "calc_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Biaya Per Buku", "calc_biaya_per_buku", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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
            & "  a.calc_nilai_jaket, " _
            & "  a.calc_biaya_cetak_final,calc_add_by,calc_add_date,calc_upd_by,calc_upd_date  " _
            & "FROM " _
            & "  public.calc_mstr a " _
            & "  INNER JOIN public.en_mstr b ON (a.calc_en_id = b.en_id) " _
            & "WHERE " _
            & "  a.calc_date BETWEEN " & SetDate(pr_txttglawal.Text) & " AND " & SetDate(pr_txttglakhir.Text) & "  " _
            & " and calc_en_id in (select user_en_id from tconfuserentity " _
                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & "ORDER BY " _
            & "  a.calc_code"

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = FCalculateCost.Name Then

            'fobject.gv_edit.SetRowCellValue(_row, "psd_group", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "code_group_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            'fobject.gv_edit.BestFitColumns()
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("calc_code")
            fobject.get_data(_obj.text)
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub
End Class
