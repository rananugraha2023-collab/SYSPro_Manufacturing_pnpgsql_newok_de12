Imports npgsql
Imports master_new.ModFunction

Public Class FBOPBTKL

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Dim dt_edit As New DataTable

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WS", "ws", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WS Sub", "ws_sub", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Item", "item", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KD", "kd", DevExpress.Utils.HorzAlignment.Default)



        add_column_copy(gv_master, "Material Unit", "material_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Insheet", "insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total Material Unit", "total_material_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bop Umum", "bop_umum", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Adjust BOP", "adjust_bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust BOP Nilai", "adjust_bop_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Sewa Gedung", "sewa_gedung", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust Sewa", "adjust_sewa", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Adjust Sewa Nilai", "adjust_sewa_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Penyusutan", "penyusutan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust Penyusutan", "adjust_penyusutan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust Penyusutan Nilai", "adjust_penyusutan_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Listrik", "listrik", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust Listrik Listrik", "adjust_listrik", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Adjust Listrik Nilai", "adjust_listrik_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Total", "total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Index BOP", "index_bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Pilihan", "pilihan", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Unit", "unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Index Unit", "index_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "BOP", "bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "K", "k", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "HL", "hl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "K Adjust", "k_adjust", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "HL Nilai", "hl_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "K Total", "k_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "HL Total", "hl_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "BTKL Total", "bktl_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Index BTKL", "index_bktl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column_copy(gv_master, "BTKL Unit", "bktl_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Grand Total", "grand_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")





        add_column_edit(gv_edit, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "WS", "ws", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "WS Sub", "ws_sub", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Item", "item", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "KD", "kd", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "Material Unit", "material_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Insheet", "insheet", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Total Material Unit", "total_material_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Bop Umum", "bop_umum", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "Adjust BOP", "adjust_bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust BOP Nilai", "adjust_bop_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Sewa Gedung", "sewa_gedung", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust Sewa", "adjust_sewa", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "Adjust Sewa Nilai", "adjust_sewa_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Penyusutan", "penyusutan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust Penyusutan", "adjust_penyusutan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust Penyusutan Nilai", "adjust_penyusutan_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "Listrik", "listrik", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust Listrik Listrik", "adjust_listrik", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Adjust Listrik Nilai", "adjust_listrik_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Total", "total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "Index BOP", "index_bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Pilihan", "pilihan", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Unit", "unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Index Unit", "index_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "BOP", "bop", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "K", "k", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "HL", "hl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "K Adjust", "k_adjust", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "HL Nilai", "hl_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_edit(gv_edit, "K Total", "k_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "HL Total", "hl_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "BTKL Total", "bktl_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Index BTKL", "index_bktl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")


        add_column_edit(gv_edit, "BTKL Unit", "bktl_unit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Grand Total", "grand_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")




        'add_column_edit(gv_edit, "Jenis", "adjust_bop", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Ukuran", "adjust_bop_nilai", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Panjang", "sewa_gedung", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Lebar", "adjust_sewa", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Gramasi", "adjust_sewa_nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "P", "penyusutan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_edit(gv_edit, "Kode", "kode", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Nilai", "nilai", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Matras Foil", "matras_foil", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Matras Emboss", "matras_emboss", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_edit(gv_edit, "Matras Deboss", "matras_deboss", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")



    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.ws, " _
                    & "  a.ws_sub, " _
                    & "  a.item, " _
                    & "  a.kd, " _
                    & "  a.material_unit, " _
                    & "  a.insheet, " _
                    & "  a.total_material_unit, " _
                    & "  a.bop_umum, " _
                    & "  a.adjust_bop, " _
                    & "  a.adjust_bop_nilai, " _
                    & "  a.sewa_gedung, " _
                    & "  a.adjust_sewa, " _
                    & "  a.adjust_sewa_nilai, " _
                    & "  a.penyusutan, " _
                    & "  a.adjust_penyusutan, " _
                    & "  a.adjust_penyusutan_nilai, " _
                    & "  a.listrik, " _
                    & "  a.adjust_listrik, " _
                    & "  a.adjust_listrik_nilai, " _
                    & "  a.total, " _
                    & "  a.index_bop, " _
                    & "  a.pilihan, " _
                    & "  a.unit, " _
                    & "  a.index_unit, " _
                    & "  a.bop, " _
                    & "  a.k, " _
                    & "  a.hl, " _
                    & "  a.k_adjust, " _
                    & "  a.hl_nilai, " _
                    & "  a.k_total, " _
                    & "  a.hl_total, " _
                    & "  a.bktl_total, " _
                    & "  a.index_bktl, " _
                    & "  a.bktl_unit, " _
                    & "  a.grand_total " _
                    & "FROM " _
                    & "  public.cetak_bop_btkl a " _
                    & "ORDER BY " _
                    & "  a.kode"



        Return get_sequel
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            ssql = "SELECT  " _
                    & "  a.kode, " _
                    & "  a.ws, " _
                    & "  a.ws_sub, " _
                    & "  a.item, " _
                    & "  a.kd, " _
                    & "  a.material_unit, " _
                    & "  a.insheet, " _
                    & "  a.total_material_unit, " _
                    & "  a.bop_umum, " _
                    & "  a.adjust_bop, " _
                    & "  a.adjust_bop_nilai, " _
                    & "  a.sewa_gedung, " _
                    & "  a.adjust_sewa, " _
                    & "  a.adjust_sewa_nilai, " _
                    & "  a.penyusutan, " _
                    & "  a.adjust_penyusutan, " _
                    & "  a.adjust_penyusutan_nilai, " _
                    & "  a.listrik, " _
                    & "  a.adjust_listrik, " _
                    & "  a.adjust_listrik_nilai, " _
                    & "  a.total, " _
                    & "  a.index_bop, " _
                    & "  a.pilihan, " _
                    & "  a.unit, " _
                    & "  a.index_unit, " _
                    & "  a.bop, " _
                    & "  a.k, " _
                    & "  a.hl, " _
                    & "  a.k_adjust, " _
                    & "  a.hl_nilai, " _
                    & "  a.k_total, " _
                    & "  a.hl_total, " _
                    & "  a.bktl_total, " _
                    & "  a.index_bktl, " _
                    & "  a.bktl_unit, " _
                    & "  a.grand_total " _
                    & "FROM " _
                    & "  public.cetak_bop_btkl a " _
                    & "ORDER BY " _
                    & "  a.kode"


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
                        .Command.CommandText = "DELETE from cetak_bop_btkl  "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.cetak_bop_btkl " _
                                            & "( " _
                                            & "  kode, " _
                                            & "  ws, " _
                                            & "  ws_sub, " _
                                            & "  item, " _
                                            & "  kd, " _
                                            & "  material_unit, " _
                                            & "  insheet, " _
                                            & "  total_material_unit, " _
                                            & "  bop_umum, " _
                                            & "  adjust_bop, " _
                                            & "  adjust_bop_nilai, " _
                                            & "  sewa_gedung, " _
                                            & "  adjust_sewa, " _
                                            & "  adjust_sewa_nilai, " _
                                            & "  penyusutan, " _
                                            & "  adjust_penyusutan, " _
                                            & "  adjust_penyusutan_nilai, " _
                                            & "  listrik, " _
                                            & "  adjust_listrik, " _
                                            & "  adjust_listrik_nilai, " _
                                            & "  total, " _
                                            & "  index_bop, " _
                                            & "  pilihan, " _
                                            & "  unit, " _
                                            & "  index_unit, " _
                                            & "  bop, " _
                                            & "  k, " _
                                            & "  hl, " _
                                            & "  k_adjust, " _
                                            & "  hl_nilai, " _
                                            & "  k_total, " _
                                            & "  hl_total, " _
                                            & "  bktl_total, " _
                                            & "  index_bktl, " _
                                            & "  bktl_unit, " _
                                            & "  grand_total " _
                                            & ") " _
                                            & "VALUES ( " _
                                            & SetSetring(dr("kode")) & ",  " _
                                            & SetSetring(dr("ws")) & ",  " _
                                            & SetSetring(dr("ws_sub")) & ",  " _
                                            & SetSetring(dr("item")) & ",  " _
                                            & SetSetring(dr("kd")) & ",  " _
                                            & SetDec(dr("material_unit")) & ",  " _
                                            & SetDec(dr("insheet")) & ",  " _
                                            & SetDec(dr("total_material_unit")) & ",  " _
                                            & SetDec(dr("bop_umum")) & ",  " _
                                            & SetDec(dr("adjust_bop")) & ",  " _
                                            & SetDec(dr("adjust_bop_nilai")) & ",  " _
                                            & SetDec(dr("sewa_gedung")) & ",  " _
                                            & SetDec(dr("adjust_sewa")) & ",  " _
                                            & SetDec(dr("adjust_sewa_nilai")) & ",  " _
                                            & SetDec(dr("penyusutan")) & ",  " _
                                            & SetDec(dr("adjust_penyusutan")) & ",  " _
                                            & SetDec(dr("adjust_penyusutan_nilai")) & ",  " _
                                            & SetDec(dr("listrik")) & ",  " _
                                            & SetDec(dr("adjust_listrik")) & ",  " _
                                            & SetDec(dr("adjust_listrik_nilai")) & ",  " _
                                            & SetDec(dr("total")) & ",  " _
                                            & SetDec(dr("index_bop")) & ",  " _
                                            & SetSetring(dr("pilihan")) & ",  " _
                                            & SetDec(dr("unit")) & ",  " _
                                            & SetDec(dr("index_unit")) & ",  " _
                                            & SetDec(dr("bop")) & ",  " _
                                            & SetDec(dr("k")) & ",  " _
                                            & SetDec(dr("hl")) & ",  " _
                                            & SetDec(dr("k_adjust")) & ",  " _
                                            & SetDec(dr("hl_nilai")) & ",  " _
                                            & SetDec(dr("k_total")) & ",  " _
                                            & SetDec(dr("hl_total")) & ",  " _
                                            & SetDec(dr("bktl_total")) & ",  " _
                                            & SetDec(dr("index_bktl")) & ",  " _
                                            & SetDec(dr("bktl_unit")) & ",  " _
                                            & SetDec(dr("grand_total")) & "  " _
                                            & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit BOP BTKL")
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

                _row_new("kode") = dr("kode")
                _row_new("ws") = dr("ws")
                _row_new("ws_sub") = dr("ws_sub")
                _row_new("item") = dr("item")
                _row_new("kd") = dr("kd")
                _row_new("material_unit") = dr("material_unit")
                _row_new("insheet") = dr("insheet")
                _row_new("total_material_unit") = dr("total_material_unit")

                _row_new("bop_umum") = dr("bop_umum")
                _row_new("adjust_bop") = dr("adjust_bop")
                _row_new("adjust_bop_nilai") = dr("adjust_bop_nilai")
                _row_new("sewa_gedung") = dr("sewa_gedung")
                _row_new("adjust_sewa") = dr("adjust_sewa")
                _row_new("adjust_sewa_nilai") = dr("adjust_sewa_nilai")
                _row_new("penyusutan") = dr("penyusutan")
                _row_new("adjust_penyusutan") = dr("adjust_penyusutan")
                _row_new("adjust_penyusutan_nilai") = dr("adjust_penyusutan_nilai")
                _row_new("listrik") = dr("listrik")
                _row_new("adjust_listrik") = dr("adjust_listrik")
                _row_new("adjust_listrik_nilai") = dr("adjust_listrik_nilai")
                _row_new("total") = dr("total")
                _row_new("index_bop") = dr("index_bop")

                _row_new("pilihan") = dr("pilihan")
                _row_new("unit") = dr("unit")
                _row_new("index_unit") = dr("index_unit")
                _row_new("bop") = dr("bop")
                _row_new("k") = dr("k")
                _row_new("hl") = dr("hl")
                _row_new("k_adjust") = dr("k_adjust")
                _row_new("hl_nilai") = dr("hl_nilai")
                _row_new("k_total") = dr("k_total")
                _row_new("hl_total") = dr("hl_total")
                _row_new("bktl_total") = dr("bktl_total")
                _row_new("index_bktl") = dr("index_bktl")
                _row_new("bktl_unit") = dr("bktl_unit")
                _row_new("grand_total") = dr("grand_total")

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
