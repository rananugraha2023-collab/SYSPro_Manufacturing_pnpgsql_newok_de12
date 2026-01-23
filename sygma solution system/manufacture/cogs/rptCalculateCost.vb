Imports MyPGDll.ClassReportDev


Public Class rptCalculateCost
    Public _oid As String
    Dim ssql As String
    Private Sub rptCalculateCost_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            ssql = "SELECT  " _
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
                & "  a.calcd_calc_oid = '" & GetCurrentColumnValue("calc_oid").ToString & "'  AND  " _
                & "  a.calcd_tipe = 'insert_edit' order by calcd_cetak_order, calcd_cetak_sub_order "

            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rpt1 As New rptCalculateCostSub1

            XrSubreport1.ReportSource = rpt1
            rpt1.DataSource = ds_sub_report
            rpt1.DataMember = "Table"

        Catch ex As Exception

        End Try

        Try
            ssql = "SELECT  " _
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
                & "  a.calcd_calc_oid = '" & GetCurrentColumnValue("calc_oid").ToString & "'  AND  " _
                & "  a.calcd_tipe = 'insert_edit_detail' order by calcd_cetak_order, calcd_cetak_sub_order "

            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rpt2 As New rptCalculateCostSub2

            XrSubreport2.ReportSource = rpt2
            rpt2.DataSource = ds_sub_report
            rpt2.DataMember = "Table"

        Catch ex As Exception

        End Try

        Try
            ssql = "SELECT  " _
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
                & "  a.calcd_calc_oid = '" & GetCurrentColumnValue("calc_oid").ToString & "'  AND  " _
                & "  a.calcd_tipe = 'insert_edit_calc' order by calcd_cetak_order, calcd_cetak_sub_order "

            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rpt2 As New rptCalculateCostSub3

            XrSubreport3.ReportSource = rpt2
            rpt2.DataSource = ds_sub_report
            rpt2.DataMember = "Table"

        Catch ex As Exception

        End Try


        Try
          

            ssql = "SELECT  " _
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
                & "  a.calcdt_calc_oid = '" & GetCurrentColumnValue("calc_oid").ToString & "' " _
                & "ORDER BY " _
                & "  a.calcdt_tambahan_kode"

            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rpt2 As New rptCalculateCostSub4

            XrSubreport4.ReportSource = rpt2
            rpt2.DataSource = ds_sub_report
            rpt2.DataMember = "Table"

        Catch ex As Exception

        End Try

    End Sub
End Class