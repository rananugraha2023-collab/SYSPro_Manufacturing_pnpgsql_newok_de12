
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Public Class frmRunFromServer
    Dim sSQL As String
    Delegate Sub FunctionCall(ByVal param)

    Private Sub BtAmbilServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtAmbilServer.Click
        Try
            Dim _query_original As String = ""
            Dim _temp As String = ""
            Dim _table As String = ""
            Dim dt_structure As New DataTable
            Dim dt_data As New DataTable

            'sSQL = "SELECT  " _
            '          & "  a.serv_code,a.serv_name,serv_ip,serv_port " _
            '          & "FROM " _
            '          & "  public.serv_server a " _
            '          & "WHERE " _
            '          & "  a.serv_location = 'foreign' " _
            '          & "ORDER BY " _
            '          & "  a.serv_code"

            Dim dt As New DataTable
            'dt_serv = GetTableData(sSQL, "SYNC")

            '_serv_pusat_ip = dt_serv.Rows(0).Item("serv_ip")
            '_serv_pusat_port = dt_serv.Rows(0).Item("serv_port")


            If me_sql.Text.Contains(";") Then
                Dim delimiter() As Char = ";".ToCharArray
                Dim SQL() As String = me_sql.Text.Split(delimiter)
                For I As Integer = 0 To UBound(SQL) - 1
                    If Len(SQL(I)) > 0 Then

                        sSQL = SQL(I)
                        _query_original = sSQL
                        Try
                            dt_data = GetTableData(sSQL, "SVR2")
                        Catch ex As Exception
                            Make_Report(sSQL & vbNewLine & ex.Message)
                        End Try


                        _temp = sSQL.Substring(sSQL.ToLower.IndexOf("from") + 5, sSQL.Length - sSQL.ToLower.IndexOf("from") - 5)

                        _table = Microsoft.VisualBasic.Left(_temp, _temp.IndexOf(" ") + 1).Trim

                        sSQL = "select column_name, data_type,udt_name " _
                            & "from INFORMATION_SCHEMA.COLUMNS where table_name = '" & _table & "' order by ordinal_position"
                        Make_Report(sSQL)

                        dt_structure = GetTableData(sSQL)

                        For Each dr_data As DataRow In dt_data.Rows
                            sSQL = "Insert into " & _table & " ("
                            For Each dr_struc As DataRow In dt_structure.Rows
                                sSQL = sSQL & " " & dr_struc("column_name") & ","
                            Next

                            sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 1)

                            sSQL = sSQL & ") values ( "
                            Make_Report(sSQL)

                            Dim x As Integer = 0
                            For Each dr_struc As DataRow In dt_structure.Rows
                                If dr_struc("udt_name") = "int2" Or dr_struc("udt_name") = "int4" Or dr_struc("udt_name") = "numeric" Or dr_struc("udt_name") = "int8" Then
                                    sSQL = sSQL & " " & SetDec(dr_data(x)) & ","
                                ElseIf dr_struc("udt_name") = "timestamp" Or dr_struc("udt_name") = "date" Then
                                    sSQL = sSQL & " " & SetDateNTime(dr_data(x)) & ","
                                Else
                                    sSQL = sSQL & " " & SetSetring(dr_data(x)) & ","
                                End If

                                x = x + 1
                            Next

                            sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 1)

                            sSQL = sSQL & ")"

                            Make_Report(sSQL)

                            Try
                                If DbRun(sSQL) = True Then
                                    Make_Report("Execute " & _query_original & vbNewLine & sSQL & " Success" & vbNewLine)
                                Else
                                    Make_Report("Execute " & _query_original & vbNewLine & vbNewLine & sSQL & vbNewLine & vbNewLine & " failed " & "" & vbNewLine)
                                End If

                            Catch ex As Exception
                                Make_Report("Execute " & _query_original & vbNewLine & sSQL & vbNewLine & ex.Message & vbNewLine)
                            End Try

                        Next

                        LblStatus.Text = "SQL to Array " & I
                    End If
                Next
            Else
                sSQL = me_sql.Text
                _query_original = sSQL
                Try
                    dt_data = GetTableData(sSQL, "SVR2")
                Catch ex As Exception
                    Make_Report(sSQL & vbNewLine & ex.Message)
                End Try


                _temp = sSQL.Substring(sSQL.ToLower.IndexOf("from") + 5, sSQL.Length - sSQL.ToLower.IndexOf("from") - 5)

                _table = Microsoft.VisualBasic.Left(_temp, _temp.IndexOf(" ") + 1).Trim

                sSQL = "select column_name, data_type,udt_name " _
                    & "from INFORMATION_SCHEMA.COLUMNS where table_name = '" & _table & "' order by ordinal_position"

                dt_structure = GetTableData(sSQL)

                For Each dr_data As DataRow In dt_data.Rows
                    sSQL = "Insert into " & _table & " ("
                    For Each dr_struc As DataRow In dt_structure.Rows
                        sSQL = sSQL & " " & dr_struc("column_name") & ","
                    Next

                    sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 1)

                    sSQL = sSQL & ") values ( "


                    Dim x As Integer = 0
                    For Each dr_struc As DataRow In dt_structure.Rows
                        If dr_struc("udt_name") = "int2" Or dr_struc("udt_name") = "int4" Or dr_struc("udt_name") = "numeric" Or dr_struc("udt_name") = "int8" Then
                            sSQL = sSQL & " " & SetDec(dr_data(x)) & ","
                        ElseIf dr_struc("udt_name") = "timestamp" Or dr_struc("udt_name") = "date" Then
                            sSQL = sSQL & " " & SetDateNTime(dr_data(x)) & ","
                        Else
                            sSQL = sSQL & " " & SetSetring(dr_data(x)) & ","
                        End If

                        x = x + 1
                    Next

                    sSQL = Microsoft.VisualBasic.Left(sSQL, sSQL.Length - 1)

                    sSQL = sSQL & ")"

                    Try
                        If DbRun(sSQL) = True Then
                            Make_Report("Execute " & _query_original & vbNewLine & sSQL & " Success" & vbNewLine)
                        Else
                            Make_Report("Execute " & _query_original & vbNewLine & vbNewLine & sSQL & vbNewLine & vbNewLine & " failed " & "" & vbNewLine)
                        End If

                    Catch ex As Exception
                        Make_Report("Execute " & _query_original & vbNewLine & sSQL & vbNewLine & ex.Message & vbNewLine)
                    End Try

                Next

            End If
            XtraTabControl1.SelectedTabPageIndex = 1
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Make_Report(ByVal status)
        Invoke(New FunctionCall(AddressOf _MakeReport), status)
    End Sub
    Private Sub _MakeReport(ByVal status)
        Try
            me_log.EditValue = me_log.EditValue & Now.ToString & " >> " & status + Chr(13) + Chr(10)
            'WriteToErrorLog(status.ToString)
            System.Windows.Forms.Application.DoEvents()

        Catch ex As Exception
        End Try
    End Sub
End Class