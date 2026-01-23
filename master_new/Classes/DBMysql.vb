Imports System.IO
Imports MySql.Data.MySqlClient
Imports master_new.ModFunctionMy

Public Class DBMysql
    Public Shared Function DbConString() As String
        Dim constring As String
        constring = "Data Source= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\myserver_sms.txt"), "server") & " ;" & _
                    "Database=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\myserver_sms.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\myserver_sms.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\myserver_sms.txt"), "user") & ";Password=bangkar;"

        DbConString = constring
    End Function

    Public Shared Sub DbRun(ByVal sSql As String)
        Dim DbConn As MySqlConnection
        Dim CmdSql As MySqlCommand

        DbConn = New MySqlConnection(DbConString)
        CmdSql = New MySqlCommand(sSql, DbConn)

        Try
            With CmdSql
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Sub DbRunParameter(ByVal sSql As String, ByVal sSQLs As ArrayList)
        Dim DbConn As MySqlConnection
        Dim CmdSql As MySqlCommand
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim Prmtr, Isi As String

        DbConn = New MySqlConnection(DbConString)
        CmdSql = New MySqlCommand(sSql, DbConn)
        Prmtr = ""
        Isi = ""

        Try
            With CmdSql
                For Each Parameter As String In sSQLs
                    'MsgBox(Parameter)
                    i = Parameter
                    a = i.Split(";")
                    For j = 0 To a.GetUpperBound(0)
                        If j = 0 Then
                            If Len(a(j)) > 0 Then
                                Prmtr = a(j)
                            End If
                        End If

                        If j = 1 Then
                            If Len(a(j)) > 0 Then
                                Isi = a(j)
                            End If
                        End If
                    Next
                    'MsgBox(Prmtr & " >> " & Isi & " <<")
                    .Parameters.AddWithValue(Prmtr, Isi)
                Next
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Sub DbRunTran(ByVal sSQLs As ArrayList, Optional ByVal debug As Boolean = False)
        Dim DbConn As New MySqlConnection(DbConString)
        Dim SqlTrans As MySqlTransaction
        Dim sql As String = ""

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try
            For Each sSQL As String In sSQLs
                Dim CmdSql As New MySqlCommand(sSQL, DbConn)
                sql = sSQL
                If debug = True Then
                    Box(sql)
                End If

                With CmdSql
                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                End With
            Next
            SqlTrans.Commit()
        Catch ex As Exception
            SqlTrans.Rollback()
            MsgBox(sql, MsgBoxStyle.Information, "Query error...")
            Throw ex 'New Exception("Data Entry Failed")
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Sub DbRunTranParameter(ByVal sSQLs As ArrayList)
        'contoh penggunaan procedure eksekusi query menggunakan parameter 
        'dengan fasilitas rollback jika terjadi kegagalan
        'penggunaan parameter ini berfungsi untuk melewatkan karakter2 tertentu seperti petik satu
        'misal nama Nu'man. jika kata Nu'man di insert menggunakan sql biasa akan error

        'dim sSql as string
        'dim sSqls as new ArrayList

        'sSql="Insert Into Tabel1 (Item1,Item2,Item3) Values " _
        '& "(Parameter1,Parameter2,Parameter3)~Parameter1;Value1#Parameter2;Value2#Parameter3;Value4"
        'sSqls.Add(sSql)

        'sSql="Insert Into Tabel2 (Item1,Item2,Item3) Values " _
        '& "(Parameter1,Parameter2,Parameter3)~Parameter1;Value1#Parameter2;Value2#Parameter3;Value4"
        'sSqls.Add(sSql)

        'DbRunTranParameter(sSqls)

        'silahkan deklarasikan dbconstring dengan koneksi string yang dipakai
        'jika anda menggunakan mode System.Data.SqlClient  maka rubahlah oleDb menjadi sql
        'contoh Dim SqlTrans As OleDbTransaction menjadi Dim SqlTrans As SqlTransaction dst, dst


        Dim DbConn As New MySqlConnection(DbConString)
        Dim SqlTrans As MySqlTransaction
        Dim i, h As String
        Dim a(), b(), c() As String
        Dim j, k, l As Integer
        Dim Prmtr, Isi As String
        Dim Sql, Parameter As String

        Parameter = ""
        Sql = ""
        Prmtr = ""
        Isi = ""

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try
            For Each sSQL As String In sSQLs
                'Pisahkan antara Query dgn parameter
                i = sSQL
                a = i.Split("~")
                For j = 0 To a.GetUpperBound(0)

                    If j = 0 Then
                        If Len(a(j)) > 0 Then
                            Sql = a(j)
                        End If
                    End If

                    If j = 1 Then
                        If Len(a(j)) > 0 Then
                            Parameter = a(j)
                        End If
                    End If
                Next

                Dim CmdSql As New MySqlCommand(Sql, DbConn)
                With CmdSql
                    'Cek apakah ada parameternya
                    If i.Contains("~") <> False Then
                        'Pisahkan parameter satu dgn parameter lainya
                        b = Parameter.Split("#")
                        For k = 0 To b.GetUpperBound(0)
                            h = b(k)
                            'Pisahkan antara parameter dan value dr parameter
                            c = h.Split(";")
                            For l = 0 To c.GetUpperBound(0)
                                If l = 0 Then
                                    If Len(c(l)) > 0 Then
                                        Prmtr = c(l)
                                    End If
                                End If

                                If l = 1 Then
                                    If Len(c(l)) > 0 Then
                                        Isi = c(l)
                                    End If
                                End If
                            Next
                            'parameter disini
                            .Parameters.AddWithValue(Prmtr, Isi)
                        Next
                    End If

                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                End With
            Next
            SqlTrans.Commit()
        Catch ex As Exception
            SqlTrans.Rollback()
            Throw ex 'New Exception("Data Entry Failed")
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Sub

    Public Shared Function GetRowInfo(ByVal sSql As String) As DataRow
        Dim sDa As New MySqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        sDa.Dispose()
        If Dt.Rows.Count = 0 Then
            Return Nothing
        ElseIf Dt.Rows.Count > 1 Then
            Throw New Exception("Multiple rows effected")
        Else
            Return Dt.Rows(0)
        End If
    End Function

    Public Shared Function GetTableData(ByVal sSql As String) As DataTable

        Dim sDa As New MySqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()
    End Function
    Public Shared Function GetNewID(ByVal Tbl As String, ByVal Fld As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewID = -1
        sSQL = "SELECT COUNT(" & Fld & ") AS Co, Max(" & Fld & ") + 1 AS ID FROM " & Tbl
        NewRow = GetRowInfo(sSQL)

        If CType(NewRow("Co"), Integer) = 0 Then
            GetNewID = 1
        Else
            GetNewID = CType(NewRow("ID"), Integer)
        End If

    End Function
    Public Shared Function GetTableDataPaging(ByVal Sql As String, ByVal Mulai As Integer, _
   ByVal JmlPerPage As Integer, ByVal OrderBy As String) As DataTable

        Dim sSql As String
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Dim OrderBySql1 As String = ""

        i = OrderBy
        a = i.Split("#")
        For j = 0 To a.GetUpperBound(0)
            If OrderBySql1 = "" Then
                OrderBySql1 = OrderBySql1 & a(j)
            Else
                OrderBySql1 = OrderBySql1 & "," & a(j)
            End If
        Next

        sSql = Sql & " Order By " & OrderBySql1 & " limit " & Mulai & "," & JmlPerPage

        Dim sDa As New MySqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()

    End Function

    Public Shared Function CekRowSelect(ByVal Sql As String) As Integer
        Dim NewSql As String
        NewSql = "Select count(*) from (" & Sql & ") a"

        Dim sDa As New MySqlDataAdapter(NewSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        If Dt.Rows.Count > 0 Then
            Return CType(Dt.Rows(0).Item(0), Integer)
        Else
            Return 0
        End If

        Dt.Dispose()
        sDa.Dispose()

    End Function
    Public Shared Sub InsertCombo(ByVal sql As String, ByVal Cbo As Windows.Forms.ComboBox)
        Try
            Dim dt As New DataTable
            dt = GetTableData(sql)

            Cbo.Items.Clear()
            For Each row As DataRow In dt.Rows
                Cbo.Items.Add(row(0))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
