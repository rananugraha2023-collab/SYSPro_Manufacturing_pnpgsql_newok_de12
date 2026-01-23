Imports npgsql
Imports master_new.ModFunction

Public Class FPerson

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssql As String
    Dim dt_edit As New DataTable

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_invoice1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter1.Fill(Me.Ds_invoice1.DataTable1)
        'TODO: This line of code loads data into the 'Ds_invoic_konsiyasi1.DataTable1' table. You can move, or remove it, as needed.
        ' Me.DataTable1TableAdapter.Fill(Me.Ds_invoic_konsiyasi1.DataTable1)
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "ID", "lbrfp_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit, "ID", "lbrfp_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.lbrfp_id, " _
                    & "  a.lbrfp_name, " _
                    & "  a.lbrfp_group " _
                    & "FROM " _
                    & "  public.lbrfp_person a " _
                    & "ORDER BY " _
                    & "  a.lbrfp_id"

        Return get_sequel
    End Function

    'Public Overrides Function insert_data() As Boolean
    '    'MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '    ssql = "SELECT  " _
    '               & "  a.lbrfp_id, " _
    '               & "  a.lbrfp_name, " _
    '               & "  a.lbrfp_group " _
    '               & "FROM " _
    '               & "  public.lbrfp_person a " _
    '               & "where lbrfp_id is null"


    '    dt_edit = master_new.PGSqlConn.GetTableData(ssql)

    '    gc_edit.DataSource = dt_edit
    '    gv_edit.BestFitColumns()
    '    insert_data = True
    'End Function
    Public Overrides Function insert() As Boolean
        insert = True
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

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "DELETE from lbrfp_person "
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                        & "  public.lbrfp_person " _
                                        & "( " _
                                        & "  lbrfp_id, " _
                                        & "  lbrfp_name, " _
                                        & "  lbrfp_group" _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetInteger(dr("lbrfp_id")) & ",  " _
                                        & SetSetring(dr("lbrfp_name")) & ",  " _
                                        & SetSetring(dr("lbrfp_group")) & "  " _
                                        & ")"

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Person")
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
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert()
    End Function

    Public Overrides Sub insert_data_awal()
        ssql = "SELECT  " _
                   & "  a.lbrfp_id, " _
                   & "  a.lbrfp_name, " _
                   & "  a.lbrfp_group " _
                   & "FROM " _
                   & "  public.lbrfp_person a " _
                   & "where lbrfp_id is null"


        dt_edit = master_new.PGSqlConn.GetTableData(ssql)

        gc_edit.DataSource = dt_edit
        gv_edit.BestFitColumns()
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            ssql = "SELECT  " _
                    & "  a.lbrfp_id, " _
                    & "  a.lbrfp_name, " _
                    & "  a.lbrfp_group " _
                    & "FROM " _
                    & "  public.lbrfp_person a " _
                    & "ORDER BY " _
                    & "  a.lbrfp_id"


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

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "DELETE from lbrfp_person "
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit.Rows

                            '                            ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pay_interval")
                            Dim _ada As Boolean = False
                            For Each dr_master As DataRow In ds.Tables(0).Rows
                                If SetNumber(dr("lbrfp_id")) = SetNumber(dr_master("lbrfp_id")) Then
                                    _ada = True
                                    Exit For
                                End If
                            Next

                            If _ada Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update  " _
                                            & "  public.lbrfp_person " _
                                            & "set " _
                                            & "  lbrfp_name = " & SetSetring(dr("lbrfp_name")) & ",  " _
                                            & "  lbrfp_group = " & SetSetring(dr("lbrfp_group")) & "  " _
                                            & "where  lbrfp_id= " & SetInteger(dr("lbrfp_id")) & " "

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                            & "  public.lbrfp_person " _
                                            & "( " _
                                            & "  lbrfp_id, " _
                                            & "  lbrfp_name, " _
                                            & "  lbrfp_group" _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(dr("lbrfp_id")) & ",  " _
                                            & SetSetring(dr("lbrfp_name")) & ",  " _
                                            & SetSetring(dr("lbrfp_group")) & "  " _
                                            & ")"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If



                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Person")
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

                _row_new("lbrfp_id") = dr("ID")
                _row_new("lbrfp_name") = dr("Name")
                _row_new("lbrfp_group") = dr("Group")
               

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
