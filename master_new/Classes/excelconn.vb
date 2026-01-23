Public Class excelconn

    Public Shared Function ImportExcel(ByVal PrmPathExcelFile As String) As DataSet

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        MyConnection = Nothing
        Try

            ''''''' Fetch Data from Excel
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                            "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")

            ' Select the data from Sheet1 of the workbook.
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [sheet1$]", MyConnection)

            MyCommand.TableMappings.Add("Table", "result")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
            Return Nothing
        End Try

    End Function
    Public Shared Function ImportCsv(ByVal PrmPathExcelFile As String) As DataSet

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        MyConnection = Nothing
        Try

            ''''''' Fetch Data from Excel
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                            "data source='" & IO.Path.GetDirectoryName(PrmPathExcelFile) & " '; " & "Extended Properties=""text;HDR=No;FMT=Delimited"";")

            ' Select the data from Sheet1 of the workbook.
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & IO.Path.GetFileName(PrmPathExcelFile) & "]", MyConnection)

            'MyCommand.TableMappings.Add("Table", "result")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
            Return Nothing
        End Try

    End Function


    Public Shared Function ImportExcel(ByVal PrmPathExcelFile As String, ByVal par_sheet As String) As DataSet

        Dim MyConnection As System.Data.OleDb.OleDbConnection
        MyConnection = Nothing
        Try

            ''''''' Fetch Data from Excel
            Dim DtSet As System.Data.DataSet

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                            "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")

            ' Select the data from Sheet1 of the workbook.
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & par_sheet & "$]", MyConnection)

            MyCommand.TableMappings.Add("Table", "result")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            MyConnection.Close()
            Return DtSet
        Catch ex As Exception
            MyConnection.Close()
            Return Nothing
        End Try

    End Function
End Class
