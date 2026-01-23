Imports master_new.ModFunction
Imports npgsql
Imports System.Windows.Forms

Public Class FChangePassword

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_ok.Click
        If Trim(txt_new_password.Text) <> Trim(txt_retype.Text) Then
            MessageBox.Show("ReType Password Berbeda Dengan New Password ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_retype.Focus()
            Exit Sub
        ElseIf Trim(txt_old_password.Text.ToLower) = Trim(txt_new_password.Text.ToLower) Then
            MessageBox.Show("Password baru harus berbeda dengan Password lama ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_retype.Focus()
            Exit Sub
        End If

        Dim ssqls As New ArrayList

        If before_login() = False Then
            MessageBox.Show("Maaf Old Password Yang Anda Masukan Salah..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            '.SQL = "update tconfuser set password = md5('" + Trim(txt_new_password.Text) + "')" _
                            '     + " where userid = " + master_new.ClsVar.sUserID.ToString
                            .Command.CommandText = "update tconfuser set password ='" + Trim(txt_new_password.Text) + "'" _
                                                 + " where userid = " + master_new.ClsVar.sUserID.ToString
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Edit Password " & master_new.ClsVar.sNama)
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
                            MessageBox.Show("Selamat Data Telah TerUpdate..", "Update Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            master_new.ClsVar.sPassword = MD5Encrypt(Trim(txt_new_password.Text))

                            Me.DialogResult = System.Windows.Forms.DialogResult.OK
                            Me.Close()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_cancel.Click
        'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        'Me.Close()
        Try
            Global.System.Windows.Forms.Application.Exit()
        Catch ex As Exception
        End Try

    End Sub

    Private Function before_login() As Boolean
        before_login = True
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select userid from tconfuser " + _
                                           " where userid = " + master_new.ClsVar.sUserID.ToString + _
                                           " and password = '" + Trim(txt_old_password.Text) + "'"
                    '" and password = md5('" + Trim(txt_old_password.Text) + "')"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    If .DataReader.HasRows = False Then
                        before_login = False
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return before_login
    End Function
End Class
