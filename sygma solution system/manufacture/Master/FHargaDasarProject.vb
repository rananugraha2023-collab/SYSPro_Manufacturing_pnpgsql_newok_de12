Imports npgsql
Imports master_new.ModFunction

Public Class FHargaDasarProject

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Dim dt_edit As New DataTable

    Private Sub FHargaDasarProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Harga Accessories", "harga_bahan_accessories", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Harga Assembly HC", "harga_assembly_hc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Harga Assembly Jaket", "harga_assembly_jaket", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        'add_column_edit(gv_edit, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Harga Accessories", "harga_bahan_accessories", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Harga Assembly HC", "harga_assembly_hc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Harga Assembly Jaket", "harga_assembly_jaket", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Size", "ukuran", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Oplagh", "oplagh", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Barang Jadi", "isi_barang_jadi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Bj Bongkar", "isi_barang_jadi_bongkar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi Bloklem", "isi_boklem", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi aksesoris bongkar", "isi_aksesoris_bongkar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Isi aksesoris", "isi_aksesoris", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "End Sheet 1/0 Reg", "endsheet_1_0_reguler", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "End Sheet 4/0 Cust", "endsheet_4_0_costum", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "End Sheet 4/4", "endsheet_4_4", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "End Sheet Dbl", "double_endsheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cov Laminasi Doff-Glossy", "cover_laminasi_doff_glossy", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Harga Gramasi", "cover_laminasi_doff_spotuv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cov Laminasi Doff-Glossy", "cover_laminasi_doff_spotuv_foil", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Harga Gramasi", "cover_laminasi_doff_spotuv_emboss", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cov Laminasi Doff-Glossy", "cover_finishing_cover", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Stiker Bentang", "stiker_bentang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Stiker 1/2 Bentang", "stiker_setengah_bentang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sisipan 2 Hal Qpp", "sisipan_2hal_qpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sisipan 4 Hal Qppi", "sisipan_4hal_qpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sisipan 2 Hal Hvs", "sisipan_2hal_hvs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sisipan 4 Hal Hvs", "sisipan_4hal_hvs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Pembatas 1 Lbr", "pembatas_1lembar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Pembatas 2 Lbr", "pembatas_2lembar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Packing", "packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "Size", "ukuran", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Oplagh", "oplagh", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Isi Barang Jadi", "isi_barang_jadi", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Isi Bj Bongkar", "isi_barang_jadi_bongkar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Isi Bloklem", "isi_boklem", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Isi aksesoris bongkar", "isi_aksesoris_bongkar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Isi aksesoris", "isi_aksesoris", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "End Sheet 1/0 Reg", "endsheet_1_0_reguler", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "End Sheet 4/0 Cust", "endsheet_4_0_costum", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "End Sheet 4/4", "endsheet_4_4", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "End Sheet Dbl", "double_endsheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Cov Laminasi Doff-Glossy", "cover_laminasi_doff_glossy", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Harga Gramasi", "cover_laminasi_doff_spotuv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Cov Laminasi Doff-Glossy", "cover_laminasi_doff_spotuv_foil", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Harga Gramasi", "cover_laminasi_doff_spotuv_emboss", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Cov Laminasi Doff-Glossy", "cover_finishing_cover", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Stiker Bentang", "stiker_bentang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Stiker 1/2 Bentang", "stiker_setengah_bentang", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sisipan 2 Hal Qpp", "sisipan_2hal_qpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sisipan 4 Hal Qppi", "sisipan_4hal_qpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sisipan 2 Hal Hvs", "sisipan_2hal_hvs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sisipan 4 Hal Hvs", "sisipan_4hal_hvs", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Pembatas 1 Lbr", "pembatas_1lembar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Pembatas 2 Lbr", "pembatas_2lembar", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Packing", "packing", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")



    End Sub

    Public Overrides Function get_sequel() As String

        'get_sequel = "SELECT  " _
        '            & "  a.kode, " _
        '            & "  a.harga_bahan_accessories, " _
        '            & "  a.harga_assembly_hc, " _
        '            & "  a.harga_assembly_jaket " _
        '            & "FROM " _
        '            & "  public.cetak_harga_bahan a " _
        '            & "ORDER BY " _
        '            & "  a.kode"

        get_sequel = "SELECT  " _
            & "  public.costum_or_mstr.no, " _
            & "  public.costum_or_mstr.ukuran, " _
            & "  public.costum_or_mstr.oplagh, " _
            & "  public.costum_or_mstr.isi_barang_jadi, " _
            & "  public.costum_or_mstr.isi_barang_jadi_bongkar, " _
            & "  public.costum_or_mstr.isi_boklem, " _
            & "  public.costum_or_mstr.isi_aksesoris_bongkar, " _
            & "  public.costum_or_mstr.isi_aksesoris, " _
            & "  public.costum_or_mstr.endsheet_1_0_reguler, " _
            & "  public.costum_or_mstr.endsheet_4_0_costum, " _
            & "  public.costum_or_mstr.endsheet_4_4, " _
            & "  public.costum_or_mstr.double_endsheet, " _
            & "  public.costum_or_mstr.cover_laminasi_doff_glossy, " _
            & "  public.costum_or_mstr.cover_laminasi_doff_spotuv, " _
            & "  public.costum_or_mstr.cover_laminasi_doff_spotuv_foil, " _
            & "  public.costum_or_mstr.cover_laminasi_doff_spotuv_emboss, " _
            & "  public.costum_or_mstr.cover_finishing_cover, " _
            & "  public.costum_or_mstr.stiker_bentang, " _
            & "  public.costum_or_mstr.stiker_setengah_bentang, " _
            & "  public.costum_or_mstr.sisipan_2hal_qpp, " _
            & "  public.costum_or_mstr.sisipan_4hal_qpp, " _
            & "  public.costum_or_mstr.sisipan_2hal_hvs, " _
            & "  public.costum_or_mstr.sisipan_4hal_hvs, " _
            & "  public.costum_or_mstr.pembatas_1lembar, " _
            & "  public.costum_or_mstr.pembatas_2lembar, " _
            & "  public.costum_or_mstr.packing " _
            & "FROM " _
            & "  public.costum_or_mstr " _
            & "ORDER BY " _
            & "  public.costum_or_mstr.no"


        Return get_sequel
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Inaert Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            ssql = "SELECT  " _
                    & "  public.costum_or_mstr.no, " _
                    & "  public.costum_or_mstr.ukuran, " _
                    & "  public.costum_or_mstr.oplagh, " _
                    & "  public.costum_or_mstr.isi_barang_jadi, " _
                    & "  public.costum_or_mstr.isi_barang_jadi_bongkar, " _
                    & "  public.costum_or_mstr.isi_boklem, " _
                    & "  public.costum_or_mstr.isi_aksesoris_bongkar, " _
                    & "  public.costum_or_mstr.isi_aksesoris, " _
                    & "  public.costum_or_mstr.endsheet_1_0_reguler, " _
                    & "  public.costum_or_mstr.endsheet_4_0_costum, " _
                    & "  public.costum_or_mstr.endsheet_4_4, " _
                    & "  public.costum_or_mstr.double_endsheet, " _
                    & "  public.costum_or_mstr.cover_laminasi_doff_glossy, " _
                    & "  public.costum_or_mstr.cover_laminasi_doff_spotuv, " _
                    & "  public.costum_or_mstr.cover_laminasi_doff_spotuv_foil, " _
                    & "  public.costum_or_mstr.cover_laminasi_doff_spotuv_emboss, " _
                    & "  public.costum_or_mstr.cover_finishing_cover, " _
                    & "  public.costum_or_mstr.stiker_bentang, " _
                    & "  public.costum_or_mstr.stiker_setengah_bentang, " _
                    & "  public.costum_or_mstr.sisipan_2hal_qpp, " _
                    & "  public.costum_or_mstr.sisipan_4hal_qpp, " _
                    & "  public.costum_or_mstr.sisipan_2hal_hvs, " _
                    & "  public.costum_or_mstr.sisipan_4hal_hvs, " _
                    & "  public.costum_or_mstr.pembatas_1lembar, " _
                    & "  public.costum_or_mstr.pembatas_2lembar, " _
                    & "  public.costum_or_mstr.packing " _
                    & "FROM " _
                    & "  public.costum_or_mstr " _
                    & "ORDER BY " _
                    & "  public.costum_or_mstr.no"

            dt_edit = master_new.PGSqlConn.GetTableData(ssql)

            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList
        dt_edit.AcceptChanges()

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE from costum_or_mstr  "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.costum_or_mstr " _
                                                                & "( " _
                                                                & "  no, " _
                                                                & "  ukuran, " _
                                                                & "  oplagh, " _
                                                                & "  isi_barang_jadi, " _
                                                                & "  isi_barang_jadi_bongkar, " _
                                                                & "  isi_boklem, " _
                                                                & "  isi_aksesoris_bongkar, " _
                                                                & "  isi_aksesoris, " _
                                                                & "  endsheet_1_0_reguler, " _
                                                                & "  endsheet_4_0_costum, " _
                                                                & "  endsheet_4_4, " _
                                                                & "  double_endsheet, " _
                                                                & "  cover_laminasi_doff_glossy, " _
                                                                & "  cover_laminasi_doff_spotuv, " _
                                                                & "  cover_laminasi_doff_spotuv_foil, " _
                                                                & "  cover_laminasi_doff_spotuv_emboss, " _
                                                                & "  cover_finishing_cover, " _
                                                                & "  stiker_bentang, " _
                                                                & "  stiker_setengah_bentang, " _
                                                                & "  sisipan_2hal_qpp, " _
                                                                & "  sisipan_4hal_qpp, " _
                                                                & "  sisipan_2hal_hvs, " _
                                                                & "  sisipan_4hal_hvs, " _
                                                                & "  pembatas_1lembar, " _
                                                                & "  pembatas_2lembar " _
                                                                & ")  " _
                                                                & "VALUES ( " _
                                                                & SetDbl(dr("no")) & ",  " _
                                                                & SetString(dr("ukuran")) & ",  " _
                                                                & SetDbl(dr("oplagh")) & ",  " _
                                                                & SetDbl(dr("isi_barang_jadi")) & ", " _
                                                                & SetDbl(dr("isi_barang_jadi_bongkar")) & ",  " _
                                                                & SetDbl(dr("isi_boklem")) & ",  " _
                                                                & SetDbl(dr("isi_aksesoris_bongkar")) & ",  " _
                                                                & SetDbl(dr("isi_aksesoris")) & ",  " _
                                                                & SetDbl(dr("endsheet_1_0_reguler")) & ",  " _
                                                                & SetDbl(dr("endsheet_4_0_costum")) & ",  " _
                                                                & SetDbl(dr("endsheet_4_4")) & ",  " _
                                                                & SetDbl(dr("double_endsheet")) & ",  " _
                                                                & SetDbl(dr("cover_laminasi_doff_glossy")) & ",  " _
                                                                & SetDbl(dr("cover_laminasi_doff_spotuv")) & ",  " _
                                                                & SetDbl(dr("cover_laminasi_doff_spotuv_foil")) & ",  " _
                                                                & SetDbl(dr("cover_laminasi_doff_spotuv_emboss")) & ",  " _
                                                                & SetDbl(dr("cover_finishing_cover")) & ",  " _
                                                                & SetDbl(dr("sisipan_2hal_qpp")) & ",  " _
                                                                & SetDbl(dr("stiker_setengah_bentang")) & ",  " _
                                                                & SetDbl(dr("isi_aksesoris")) & ",  " _
                                                                & SetDbl(dr("sisipan_2hal_qpp")) & ",  " _
                                                                & SetDbl(dr("sisipan_4hal_qpp")) & ",  " _
                                                                & SetDbl(dr("sisipan_2hal_hvs")) & ",  " _
                                                                & SetDbl(dr("sisipan_4hal_hvs")) & ",  " _
                                                                & SetDbl(dr("pembatas_1lembar")) & ",  " _
                                                                & SetDbl(dr("pembatas_2lembar")) & "  " _
                                                                & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Harga Dasar ")
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
                        ' set_row(Trim(op_code.Text), "op_code")
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
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Private Sub BtImportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImportExcel.Click
        Try

            Dim filex As String = ""
            filex = AskOpenFile("Xls Files | *.xls")

            If filex = "" Then
                Exit Sub
            End If

            gc_edit.DataSource = Nothing

            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(filex)

            dt_edit.Rows.Clear()

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim _row_new As DataRow
                _row_new = dt_edit.NewRow

                _row_new("kode") = dr("Kode")
                _row_new("harga_jns_kertas") = dr("Jenis")
                _row_new("harga_ukuran_plano") = dr("Ukuran Plano")
                _row_new("harga_panjang") = dr("Panjang")
                _row_new("harga_lebar") = dr("Lebar")
                _row_new("harga_kg") = dr("Harga Kg")
                _row_new("harga_rim") = dr("Harga Rim")
                _row_new("harga_gramasi") = dr("Harga Gramasi")

                dt_edit.Rows.Add(_row_new)
            Next
            dt_edit.AcceptChanges()


            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
