Imports System.Net.Mail

Public Class FMail
    Dim nama_depan As String

    Private Sub FMail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ClsVar.sKaryawanID <> -1 Then
            Try
                Using objperiode As New master_new.WDABasepgsql("", "")
                    With objperiode
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select nama_depan, email_perusahaan from tkaryawan where id_karyawan = " + ClsVar.sKaryawanID.ToString
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            sc_txtfrom.Text = .DataReader.Item("email_perusahaan")
                            nama_depan = .DataReader.Item("nama_depan")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else
            sc_txtfrom.Text = ""
        End If
    End Sub

    Private Sub sb_sent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_sent.Click
        If sc_txtto.Text = "" Then
            MessageBox.Show("Daftarkan Terlebih Dahulu Email Anda Di Data Karyawan..", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Try
                Dim message As New MailMessage
                message.From = New MailAddress(sc_txtfrom.Text)
                message.To.Add(Trim(sc_txtto.Text))

                If Trim(sc_txtcc.Text) <> "" Then
                    message.CC.Add(Trim(sc_txtcc.Text))
                End If

                message.Subject = Trim(sc_txtsubject.Text)
                message.Body = Trim(sc_txtbody.Text)

                Dim attachment = New Attachment(sc_txtfile.Text)
                message.Attachments.Add(Attachment)


                Dim emailClient As New SmtpClient("mail.hariff.com")
                Dim SMTPUserInfo As New System.Net.NetworkCredential("setiadi.sudrajat@hariff.com", "sudrajat")
                emailClient.UseDefaultCredentials = False
                emailClient.Credentials = SMTPUserInfo
                emailClient.Send(message)

                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End Try
        End If
    End Sub

    Private Sub sb_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_cancel.Click
        Close()
    End Sub
End Class