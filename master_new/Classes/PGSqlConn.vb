'Imports Npgsql
Imports System.IO
Imports master_new.ModFunction
Imports Npgsql

Public Class PGSqlConn
    '===Ini dll untuk koneksi ke postgresql
    Public Shared Function DbConString() As String

        Dim constring As String = master_new.WDABasepgsql.DbConString

        DbConString = constring

    End Function
    Public Shared Function DbConString(ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As String


        Dim constring As String = "Server= " & par_ip & " ;" & _
                        "Database=" & par_db & ";Port=" & par_port & ";User ID=" & par_user & ";Password=bangkar;"


        DbConString = constring

    End Function

    Public Shared Function DbConString(ByVal server As String) As String

        If server.ToUpper = "SYNC" Then
            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserversync.txt"), "server") & " ;" _
                    & "Database=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                    & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;"
            DbConString = constring

        ElseIf server.ToUpper = "SVR1" Then

            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver_1.txt"), "server") & " ;" & _
                      "Database=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_1.txt"), "user") & ";Password=bangkar;Pooling=false;"

            DbConString = constring

        ElseIf server.ToUpper = "SVR2" Then

            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver_2.txt"), "server") & " ;" & _
                      "Database=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_2.txt"), "user") & ";Password=bangkar;Pooling=false;"

            DbConString = constring

        ElseIf server.ToUpper = "SVR_Visitama".ToUpper Then

            Dim constring As String = "Server= " & konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver_visitama.txt"), "server") & " ;" & _
                      "Database=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_visitama.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_visitama.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                      & "\filekonfigurasi\pgserver_visitama.txt"), "user") & ";Password=bangkar;Pooling=false;"

            DbConString = constring

        ElseIf server.Length > 0 Then
            If server.Contains(";") Then
                Dim i As Integer
                Dim svr, port As String
                i = 0
                i = server.IndexOf(";")

                svr = server.Substring(0, i)
                port = server.Substring(i + 1, Len(server) - i - 1)

                Dim constring As String = "Server= " & svr & " ;" & _
                   "Database=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & port & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;Connection Lifetime=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\conf_system.txt"), "limit_connection_timeout") & ";"

                DbConString = constring
            Else
                Dim constring As String = "Server= " & server & " ;" & _
                   "Database=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "db") & ";Port=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "port") & ";User ID=" & konfigurasi(GetFileContents(appbase() _
                   & "\filekonfigurasi\pgserversync.txt"), "user") & ";Password=bangkar;Pooling=false;"
                DbConString = constring
            End If

        Else
            DbConString = DbConString()
        End If

    End Function

    Public Shared Function CekStyeTanggal() As String
        Try
            Dim sSQL As String

            sSQL = "SHOW DATESTYLE"

            CekStyeTanggal = GetRowInfo(sSQL)(0).ToString
        Catch ex As Exception
            Pesan(Err)
            CekStyeTanggal = ""
        End Try

    End Function

    Public Shared Function get_conf_file(ByVal par_type As String) As String
        get_conf_file = ""
        Try
            Dim dr As DataRow
            Dim ssql As String
            ssql = "select conf_value from conf_file " + _
                    " where conf_name = '" + par_type + "'"

            dr = master_new.PGSqlConn.GetRowInfo(ssql)
            If dr Is Nothing Then
                'Box("Sorry, configuration " & par_type & " doesn't exist")
                ' get_conf_file
            ElseIf dr(0) Is System.DBNull.Value Then
                Box("Sorry, configuration " & par_type & " is null")
            Else
                get_conf_file = dr(0).ToString
            End If

            'Using objkalendar As New master_new.WDABasepgsql("", "")
            '    With objkalendar
            '        .Connection.Open()
            '        .Command = .Connection.CreateCommand
            '        .Command.CommandType = CommandType.Text
            '        .Command.CommandText = "select conf_value from conf_file " + _
            '                               " where conf_name = '" + par_type + "'"
            '        .InitializeCommand()
            '        .DataReader = .Command.ExecuteReader
            '        While .DataReader.Read
            '            get_conf_file = .DataReader.Item("conf_value")
            '        End While
            '    End With
            'End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return get_conf_file
    End Function
    Public Shared Function DbRun(ByVal sSql As String) As Boolean
        Dim DbConn As nPgSqlConnection
        Dim CmdSql As nPgSqlCommand

        DbConn = New nPgSqlConnection(DbConString)
        CmdSql = New nPgSqlCommand(sSql, DbConn)

        Try
            With CmdSql
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
            Return True
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
            Return False
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Function

    Public Shared Function DbRun(ByVal sSql As String, ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As Boolean
        Dim DbConn As nPgSqlConnection
        Dim CmdSql As nPgSqlCommand

        DbConn = New nPgSqlConnection(DbConString(par_ip, par_db, par_port, par_user))
        CmdSql = New nPgSqlCommand(sSql, DbConn)

        Try
            With CmdSql
                .Connection.Open()
                .ExecuteNonQuery()
                .Connection.Close()
                .Dispose()
            End With
            Return True
        Catch ex As Exception
            Throw ex 'New Exception("Data Entry Error")
            'MessageBox.Show(sSql & " >> desc :" & Err.Description & " >> err.number :" & Err.Number)
            Return False
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Function


    Public Shared Sub DbRunParameter(ByVal sSql As String, ByVal sSQLs As ArrayList)
        Dim DbConn As nPgSqlConnection
        Dim CmdSql As nPgSqlCommand
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim Prmtr, Isi As String

        DbConn = New nPgSqlConnection(DbConString)
        CmdSql = New nPgSqlCommand(sSql, DbConn)
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
                    '.Parameters.
                    '.Parameters.AddWithValue(Prmtr, Isi)
                    Dim Param = New NpgsqlParameter(Prmtr, DbType.String)
                    Param.Value = Isi
                    .Parameters.Add(Param)
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

    Public Shared Function DbRunTran(ByVal sSQLs As ArrayList, ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As Boolean
        Dim DbConn As New nPgSqlConnection(DbConString(par_ip, par_db, par_port, par_user))
        Dim SqlTrans As nPgSqlTransaction
        Dim sql As String = ""

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try
            For Each sSQL As String In sSQLs
                Dim CmdSql As New nPgSqlCommand(sSQL, DbConn)
                sql = sSQL

                With CmdSql
                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                    'System.Windows.Forms.Application.DoEvents()
                End With

            Next
            SqlTrans.Commit()
            Return True
        Catch ex As Exception
            SqlTrans.Rollback()
            'MsgBox(sql, MsgBoxStyle.Information, "Query error...")
            Throw ex 'New Exception("Data Entry Failed")
            Return False
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Function

    Public Shared Function DbRunTran(ByVal sSQLs As ArrayList, Optional ByVal debug As Boolean = False) As Boolean
        Dim DbConn As New nPgSqlConnection(DbConString)
        Dim SqlTrans As nPgSqlTransaction
        Dim sql As String = ""

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try
            For Each sSQL As String In sSQLs
                Dim CmdSql As New nPgSqlCommand(sSQL, DbConn)
                sql = sSQL
                If debug = True Then
                    Box(sql)
                End If

                With CmdSql
                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                    'System.Windows.Forms.Application.DoEvents()
                End With

            Next
            SqlTrans.Commit()
            Return True
        Catch ex As Exception
            SqlTrans.Rollback()
            'MsgBox(sql, MsgBoxStyle.Information, "Query error...")
            Throw ex 'New Exception("Data Entry Failed")
            Return False
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Function


    Public Shared Function DbRunTran(ByVal sSQLs As ArrayList, ByVal server As String) As Boolean
        Dim DbConn As New nPgSqlConnection(DbConString(server))
        Dim SqlTrans As nPgSqlTransaction

        DbConn.Open()
        SqlTrans = DbConn.BeginTransaction()

        Try

            For Each sSQL As String In sSQLs
                Dim CmdSql As New nPgSqlCommand(sSQL, DbConn)

                With CmdSql
                    .Transaction = SqlTrans
                    .ExecuteNonQuery()
                    'System.Windows.Forms.Application.DoEvents()
                End With
            Next
            SqlTrans.Commit()
            Return True
        Catch ex As Exception

            SqlTrans.Rollback()

            'Throw ex 'New Exception("Data Entry Failed")
            MsgBox(ex.Message)
            Return False
        Finally
            DbConn.Close()
            DbConn.Dispose()
        End Try

    End Function
    Public Shared Function DbRunTran(ByVal sSQLs1 As ArrayList, ByVal server1 As String, _
                                ByVal sSQLs2 As ArrayList, ByVal server2 As String) As Boolean

        Dim DbConn1 As New nPgSqlConnection(DbConString(server1))
        Dim SqlTrans1 As nPgSqlTransaction

        DbConn1.Open()
        SqlTrans1 = DbConn1.BeginTransaction()

        Dim DbConn2 As New nPgSqlConnection(DbConString(server2))
        Dim SqlTrans2 As nPgSqlTransaction

        DbConn2.Open()
        SqlTrans2 = DbConn2.BeginTransaction()

        Try

            For Each sSQL1 As String In sSQLs1
                Dim CmdSql1 As New nPgSqlCommand(sSQL1, DbConn1)
                With CmdSql1
                    .Transaction = SqlTrans1
                    .ExecuteNonQuery()
                End With
            Next

            For Each sSQL2 As String In sSQLs2
                Dim CmdSql2 As New nPgSqlCommand(sSQL2, DbConn2)

                With CmdSql2
                    .Transaction = SqlTrans2
                    .ExecuteNonQuery()
                End With
            Next

            SqlTrans1.Commit()
            SqlTrans2.Commit()
            Return True
        Catch ex As Exception

            SqlTrans1.Rollback()
            SqlTrans2.Rollback()

            MsgBox(ex.Message)
            Return False
        Finally
            DbConn1.Close()
            DbConn1.Dispose()
            DbConn2.Close()
            DbConn2.Dispose()
        End Try
    End Function
    Public Shared Function DbRunTran(ByVal sSQLs1 As ArrayList, ByVal server1 As String, _
                                ByVal sSQLs2 As ArrayList, ByVal server2 As String, _
                                ByVal sSQLs3 As ArrayList, ByVal server3 As String) As Boolean

        Dim DbConn1 As New nPgSqlConnection(DbConString(server1))
        Dim SqlTrans1 As nPgSqlTransaction


        DbConn1.Open()
        SqlTrans1 = DbConn1.BeginTransaction()

        Dim DbConn2 As New nPgSqlConnection(DbConString(server2))
        Dim SqlTrans2 As nPgSqlTransaction


        DbConn2.Open()
        SqlTrans2 = DbConn2.BeginTransaction()

        Dim DbConn3 As New nPgSqlConnection(DbConString(server3))
        Dim SqlTrans3 As nPgSqlTransaction


        DbConn3.Open()
        SqlTrans3 = DbConn3.BeginTransaction()

        Try

            For Each sSQL1 As String In sSQLs1
                Dim CmdSql1 As New nPgSqlCommand(sSQL1, DbConn1)

                With CmdSql1
                    .Transaction = SqlTrans1
                    .ExecuteNonQuery()
                End With
            Next

            For Each sSQL2 As String In sSQLs2
                Dim CmdSql2 As New nPgSqlCommand(sSQL2, DbConn2)

                With CmdSql2
                    .Transaction = SqlTrans2
                    .ExecuteNonQuery()
                End With
            Next


            For Each sSQL3 As String In sSQLs3
                Dim CmdSql3 As New nPgSqlCommand(sSQL3, DbConn3)

                With CmdSql3
                    .Transaction = SqlTrans3
                    .ExecuteNonQuery()
                End With
            Next

            SqlTrans1.Commit()
            SqlTrans2.Commit()
            SqlTrans3.Commit()
            Return True
        Catch ex As Exception

            SqlTrans1.Rollback()
            SqlTrans2.Rollback()
            SqlTrans3.Rollback()

            Throw ex 'New Exception("Data Entry Failed")
            Return False
        Finally
            DbConn1.Close()
            DbConn1.Dispose()

            DbConn2.Close()
            DbConn2.Dispose()

            DbConn3.Close()
            DbConn3.Dispose()
        End Try

    End Function
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


        Dim DbConn As New nPgSqlConnection(DbConString)
        Dim SqlTrans As nPgSqlTransaction
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

                Dim CmdSql As New nPgSqlCommand(Sql, DbConn)
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
                            '.Parameters.AddWithValue(Prmtr, Isi)
                            Dim Param = New NpgsqlParameter(Prmtr, DbType.String)
                            Param.Value = Isi
                            .Parameters.Add(Param)
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
        Dim sDa As New nPgSqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable

        sDa.SelectCommand.CommandTimeout = 240
        sDa.Fill(Dt)
        sDa.Dispose()
        If Dt.Rows.Count = 0 Then
            Return Nothing
        ElseIf Dt.Rows.Count > 1 Then
            'Throw New Exception("Multiple rows effected")
            Return Dt.Rows(0)
        Else
            Return Dt.Rows(0)
        End If
    End Function

    Public Shared Function GetRowInfo(ByVal sSql As String, ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As DataRow
        Dim sDa As New nPgSqlDataAdapter(sSql, DbConString(par_ip, par_db, par_port, par_user))
        Dim Dt As New DataTable

        sDa.SelectCommand.CommandTimeout = 240
        sDa.Fill(Dt)
        sDa.Dispose()
        If Dt.Rows.Count = 0 Then
            Return Nothing
        ElseIf Dt.Rows.Count > 1 Then
            'Throw New Exception("Multiple rows effected")
            Return Dt.Rows(0)
        Else
            Return Dt.Rows(0)
        End If
    End Function

    Public Shared Function GetDataColumn(ByVal sSql As String) As Object
        Try
            Dim sDa As New nPgSqlDataAdapter(sSql, DbConString)
            Dim Dt As New DataTable

            sDa.SelectCommand.CommandTimeout = 240
            sDa.Fill(Dt)
            sDa.Dispose()
            If Dt.Rows.Count > 0 Then
                Return Dt.Rows(0).Item(0)
            Else
                Return DBNull.Value
            End If
        Catch ex As Exception
            Return DBNull.Value
        End Try

    End Function
    Public Shared Function GetTableData(ByVal sSql As String) As DataTable
        Try
            Dim sDa As New nPgSqlDataAdapter(sSql, DbConString)
            Dim Dt As New DataTable

            sDa.SelectCommand.CommandTimeout = 480
            'sDa.SelectCommand.Connection.AutoCommit = False
            sDa.Fill(Dt)
            Return Dt
            Dt.Dispose()
            sDa.Dispose()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetTableData(ByVal sSql As String, ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As DataTable
        Try
            Dim sDa As New nPgSqlDataAdapter(sSql, DbConString(par_ip, par_db, par_port, par_user))
            Dim Dt As New DataTable

            sDa.SelectCommand.CommandTimeout = 480
            sDa.Fill(Dt)
            Return Dt
            Dt.Dispose()
            sDa.Dispose()
        Catch ex As Exception

            MsgBox(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Shared Function GetTableData(ByVal sSql As String, ByVal par_none As String, ByVal par_constring As String) As DataTable

        Dim sDa As New nPgSqlDataAdapter(sSql, par_constring)
        Dim Dt As New DataTable

        sDa.SelectCommand.CommandTimeout = 240
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()
    End Function

    Public Shared Function GetTableData(ByVal sSql As String, ByVal server As String) As DataTable

        Dim DbConn As nPgSqlConnection
        DbConn = New nPgSqlConnection(DbConString(server))
        Dim cmd As nPgSqlCommand
        Dim adapter As New nPgSqlDataAdapter

        Try
            'open the command objects connection
            Dim Dt As New DataTable

            DbConn.Open()

            cmd = New nPgSqlCommand
            cmd.Connection = DbConn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sSql

            adapter.SelectCommand = cmd
            adapter.SelectCommand.CommandTimeout = 240
            adapter.Fill(Dt)

            Return Dt

            DbConn.Close()
            adapter.Dispose()
        Catch ex As NpgsqlException
            'Throw ex
            Throw ex
            Return Nothing
        Catch ex As Exception
            ' Throw New Exception(ex.Message)
            Throw ex
            Return Nothing
        Finally
            DbConn.Close()
            adapter.Dispose()
        End Try
    End Function

    Public Shared Function GetTableDataReader(ByVal sSql As String) As NpgsqlDataReader
        Dim DbConn As NpgsqlConnection
        DbConn = New NpgsqlConnection(DbConString)

        DbConn.Open()

        Dim cmd As NpgsqlCommand
        Dim rs As NpgsqlDataReader

        cmd = New NpgsqlCommand
        cmd.Connection = DbConn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sSql
        rs = cmd.ExecuteReader(CommandBehavior.SingleResult)

        Return rs
        'Label1.Text = rs.GetString(0) & vbNewLine & rs.GetString(1)

        rs.Close()
        cmd.Dispose()
        DbConn.Close()
        DbConn.Dispose()
    End Function


    '=================fungsi tambahan ====================
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
    Public Shared Function GetNewID(ByVal Tbl As String, ByVal Fld As String, ByVal fldKey As String, ByVal filter As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewID = -1
        sSQL = "SELECT COUNT(" & Fld & ") AS Co, Max(" & Fld & ") + 1 AS ID FROM " & Tbl _
                & " where " & fldKey & "='" & filter & "'"

        NewRow = GetRowInfo(sSQL)

        If (NewRow("Co")) = 0 Then
            GetNewID = 1
        Else
            GetNewID = (NewRow("ID"))
        End If

    End Function

    Public Shared Function GetNewNumber(ByVal Tbl As String, ByVal Fld As String, ByVal digit As String) As Integer
        Dim NewRow As DataRow
        Dim sSQL As String

        GetNewNumber = -1
        sSQL = "SELECT COUNT (" & Fld & ") AS Co, to_number(MAX(RIGHT(" & Fld & "," & digit & ")),'999999999999999') + 1 AS ID FROM " & Tbl
        NewRow = GetRowInfo(sSQL)

        If CType(NewRow("Co"), Integer) = 0 Then
            GetNewNumber = 1
        Else
            GetNewNumber = CType(NewRow("ID"), Integer)
        End If

    End Function
    Public Shared Function GetNewNumberYM(ByVal Tbl As String, ByVal Fld As String, _
               ByVal digit As Integer, ByVal Filter As String, _
               Optional ByVal ModeFull As Boolean = True) As String


        Dim NewRow As DataRow
        Dim sSQL As String
        Dim FormatHasil As String = ""

        For i As Integer = 1 To digit
            FormatHasil = FormatHasil & "0"
        Next

        GetNewNumberYM = -1
        sSQL = "SELECT coalesce(cast(RIGHT(MAX(" & Fld _
        & ")," & digit & ") as integer),0) + 1 AS ID FROM " & Tbl _
        & " Where left(" & Fld & "," & Len(Filter) & ")='" & Filter & "'"
        NewRow = GetRowInfo(sSQL)
        If ModeFull = True Then
            GetNewNumberYM = Filter & CType(NewRow("ID"), Integer).ToString(FormatHasil)
        Else
            GetNewNumberYM = CType(NewRow("ID"), Integer)
        End If
    End Function
    Public Shared Function GetNewNumberYMChild(ByVal Tbl As String, ByVal Fld As String, _
       ByVal FldKunci As String, ByVal digit As Integer, ByVal Filter As String, _
       Optional ByVal ModeFull As Boolean = True) As String

        Dim NewRow As DataRow
        Dim sSQL As String
        Dim FormatHasil As String = ""

        For i As Integer = 1 To digit
            FormatHasil = FormatHasil & "0"
        Next

        GetNewNumberYMChild = -1
        sSQL = "SELECT COUNT (" & Fld & ") AS Co, MAX(RIGHT(" & Fld & "," & digit & ")) + 1 AS ID FROM " & Tbl _
        & " Where (" & FldKunci & ")='" & Filter & "'"

        NewRow = GetRowInfo(sSQL)

        If ModeFull = True Then
            If CType(NewRow("Co"), Integer) = 0 Then
                GetNewNumberYMChild = Filter & CType(1, Integer).ToString(FormatHasil)
            Else
                GetNewNumberYMChild = Filter & CType(NewRow("ID"), Integer).ToString(FormatHasil)
            End If
        Else
            If CType(NewRow("Co"), Integer) = 0 Then
                GetNewNumberYMChild = CType(1, Integer)
            Else
                GetNewNumberYMChild = CType(NewRow("ID"), Integer)
            End If
        End If


    End Function


    Public Shared Function GetIDByName(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName & " = '" & fldVal & "'"

        NewRow = GetRowInfo(sSQL)

        If NewRow Is Nothing Then
            'ini handle eror
            GetIDByName = ""
        Else
            GetIDByName = CType(SetString(NewRow(fld)), String)
        End If

    End Function

    Public Shared Function GetIDByName2(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName2 = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName & " = " & fldVal

        NewRow = GetRowInfo(sSQL)

        If NewRow Is Nothing Then
            'ini handle eror
            GetIDByName2 = ""
        Else
            GetIDByName2 = CType(SetString(NewRow(fld)), String)
        End If

    End Function

    Public Shared Function GetIDByName2Prmtr(ByVal Tbl As String, ByVal fld As String, ByVal fldName1 As String, ByVal fldVal1 As String, ByVal fldName2 As String, ByVal fldVal2 As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetIDByName2Prmtr = "0"
        sSQL = "SELECT " & fld & " FROM " & Tbl & " WHERE " & fldName1 & " = '" & fldVal1 & "' and " & fldName2 & " = " & fldVal2 & ""

        NewRow = GetRowInfo(sSQL)
        If NewRow Is Nothing Then
            GetIDByName2Prmtr = ""
        Else
            GetIDByName2Prmtr = CType(SetString(NewRow(fld)), String)
        End If


    End Function

    Public Shared Function GetNameByID(ByVal Tbl As String, ByVal fld As String, ByVal fldName As String, ByVal fldVal As String) As String

        Dim sSQL As String
        Dim NewRow As DataRow

        GetNameByID = ""
        sSQL = "SELECT " & fldName & " FROM " & Tbl & " WHERE " & fld & " = " & fldVal
        NewRow = GetRowInfo(sSQL)

        If NewRow Is Nothing Then
            'ini handle eror
            GetNameByID = ""
        Else
            GetNameByID = CType(SetString(NewRow(fldName)), String)
        End If
    End Function

    Public Shared Function CekTanggal() As Date
        Try
            Dim sSQL As String
            Dim NewRow As DataRow

            'GetIDByName = "0"
            sSQL = "SELECT LOCALTIMESTAMP as Tgl"

            NewRow = GetRowInfo(sSQL)

            CekTanggal = NewRow("Tgl")
        Catch ex As Exception
            CekTanggal = Nothing
        End Try
    End Function

    Public Shared Function CekJam() As String
        Try
            Dim sSQL As String
            Dim NewRow As DataRow

            'GetIDByName = "0"
            sSQL = "SELECT LOCALTIME as Jam"
            NewRow = GetRowInfo(sSQL)

            CekJam = NewRow("Jam")
        Catch ex As Exception
            CekJam = ""
        End Try

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

    Public Shared Sub InsertCombo(ByVal sql As String, ByVal Cbo As DevExpress.XtraEditors.ComboBoxEdit)
        Try

            Dim dt As New DataTable
            dt = GetTableData(sql)

            Cbo.Properties.Items.Clear()
            For Each row As DataRow In dt.Rows
                Cbo.Properties.Items.Add(row(0))
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Function GetTableDataPaging(ByVal Sql As String, ByVal Mulai As Integer, _
   ByVal JmlPerPage As Integer, ByVal OrderBy As String) As DataTable

        Dim sSql As String

        sSql = Sql & " Order By " & OrderBy & " limit " & JmlPerPage & " OFFSET " & Mulai
        'MsgBox(sSql)
        Dim sDa As New nPgSqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()

    End Function

    Public Shared Function CekRowSelect(ByVal Sql As String) As Integer
        Dim NewSql As String
        NewSql = "Select count (*) from (" & Sql & ") a"

        Dim sDa As New nPgSqlDataAdapter(NewSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)

        Return CType(Dt.Rows(0).Item(0), Integer)

        Dt.Dispose()
        sDa.Dispose()

    End Function

    Public Shared Function GetTableDataTop(ByVal Sql As String, ByVal Jml As Integer, ByVal OrderBy As String) As DataTable
        Dim sSql As String

        sSql = "Select  * From ( " & Sql & " ) a Order By " & OrderBy & " limit " & Jml

        Dim sDa As New nPgSqlDataAdapter(sSql, DbConString)
        Dim Dt As New DataTable
        sDa.Fill(Dt)
        Return Dt
        Dt.Dispose()
        sDa.Dispose()
    End Function

    Public Shared Function ReportDataset(ByVal sSql As String) As DataSet
        Try
            ' Create a connection.
            Dim Connection As New nPgSqlConnection(DbConString())

            ' Create a data adapter and a dataset.
            Dim Adapter As New nPgSqlDataAdapter(sSql, Connection)
            Dim DataSet1 As New DataSet()

            ' Specify the data adapter and the data source for the report.
            ' Note that you must fill the datasource with data because it is not bound directly to the specified data adapter.
            'report.DataAdapter = Adapter
            Adapter.SelectCommand.CommandTimeout = 240
            Adapter.Fill(DataSet1, "Table")


            Return DataSet1

        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function

    Public Shared Function ReportDataset(ByVal sSql As String, ByVal par_ip As String, ByVal par_db As String, ByVal par_port As String, ByVal par_user As String) As DataSet
        Try
            ' Create a connection.
            Dim Connection As New nPgSqlConnection(DbConString(par_ip, par_db, par_port, par_user))

            ' Create a data adapter and a dataset.
            Dim Adapter As New nPgSqlDataAdapter(sSql, Connection)
            Dim DataSet1 As New DataSet()

            ' Specify the data adapter and the data source for the report.
            ' Note that you must fill the datasource with data because it is not bound directly to the specified data adapter.
            'report.DataAdapter = Adapter
            Adapter.SelectCommand.CommandTimeout = 240
            Adapter.Fill(DataSet1, "Table")


            Return DataSet1

        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function

    Public Shared Function FinsertSQL2Array(ByVal List As ArrayList) As ArrayList
        Dim sSqls As New ArrayList
        Dim sSql As String
        Dim i As Integer = 1
        Try
            For Each Sql As String In List

                sSql = "INSERT INTO  " _
                    & "  public.t_sql_out " _
                    & "( " _
                    & "  sql_uid, " _
                    & "  seq, " _
                    & "  sql_command, " _
                    & "  mili_second, " _
                    & "  waktu " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                    & i & ",  " _
                    & SetSetring(Sql) & ",  " _
                    & "cast(to_char(now(),'MS') as integer)" & ",  " _
                    & "now()" & "  " _
                    & ")"

                sSqls.Add(sSql)
                sSql = ""

                i = i + 1
            Next
            Return sSqls
        Catch ex As Exception
            Pesan(Err)
            Return sSqls
        End Try
    End Function

    Public Shared Function status_sync() As Boolean
        Try
            Dim ssql As String = "select conf_value from conf_file  where conf_name ='sync_status'"

            Dim dr As DataRow
            dr = GetRowInfo(ssql)

            If dr Is Nothing Then
                'Box("Sorry, configuration " & par_type & " doesn't exist")
                ' get_conf_file
                Return True
            ElseIf dr(0) Is System.DBNull.Value Then
                Return True

            ElseIf dr(0).ToString = "0" Then
                Return False

            ElseIf dr(0).ToString = "1" Then
                Return True
            Else
                Return True
            End If

        Catch ex As Exception
            Return True
        End Try
    End Function

End Class

