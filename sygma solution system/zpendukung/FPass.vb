Imports npgsql
Imports master_new.ModFunction

Public Class FPass
    Public Overrides Sub login()
        Dim frm As New FMainMenu
        frm.set_window(Me)
        frm.Show()
        Me.Hide()
        master_new.ClsVar.sPass = False

        Dim fp As New master_new.FPrintScreen
        fp.Show()
        fp.Visible = False
        frm.set_window_print_screen(fp)


        If _sama = True Then
            Dim frm2 As New master_new.FChangePassword
            frm2.ShowDialog()
            _sama = False
        End If

        'If System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "," Then
        '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
        'End If

        'bantu_menu()
        'bantu_sync()
    End Sub

    Private Sub bantu_sync()
        Dim i As Integer = 0
        Dim ssqls As New ArrayList
        Dim ds_bantu As New DataSet

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "select pt_id from pt_mstr where coalesce(pt_id,-1) <> -1"
        '            .InitializeCommand()
        '            .FillDataSet(ds_bantu, "bantu")
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                            & "  public.table1 " _
                            & "( " _
                            & "  field1 " _
                            & ")  " _
                            & "VALUES ( 1  " _
                            & "); INSERT INTO  " _
                            & "  public.table1 " _
                            & "( " _
                            & "  field1 " _
                            & ")  " _
                            & "VALUES ( 2  " _
                            & "); "

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
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub bantu_menu()
        Dim ds_bantu As New DataSet
        Dim i, j As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select groupid from tconfgroup"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssqls As New ArrayList
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenucollection " _
                                            & "( " _
                                            & "  menuid, " _
                                            & "  menuname " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(143) & ",  " _
                                            & SetSetring("PriceListHeader") & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenucollection " _
                                            & "( " _
                                            & "  menuid, " _
                                            & "  menuname " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(144) & ",  " _
                                            & SetSetring("PriceListDetail") & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tconfmenucollection " _
                                            & "( " _
                                            & "  menuid, " _
                                            & "  menuname " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetInteger(145) & ",  " _
                                            & SetSetring("PriceListCopy") & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                            For j = 143 To 145
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.tconfmenu " _
                                                    & "( " _
                                                    & "  groupid, " _
                                                    & "  menuid, " _
                                                    & "  enablestatus, " _
                                                    & "  visiblestatus, " _
                                                    & "  editablestatus " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetInteger(ds_bantu.Tables(0).Rows(i).Item("groupid")) & ",  " _
                                                    & SetInteger(j) & ",  False , False, False)"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        Next


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
