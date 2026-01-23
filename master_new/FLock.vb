Imports master_new.ModFunction

Public Class FLock

    Public fmm As FOpacity

    Public Overrides Sub FPass_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim status_file As String = DevExpress.Utils.FilesHelper.FindingFileName("c:\syspro", "logo.jpg", False)
        If status_file = "" Then
            LogoPictureBox.ImageLocation = "\\192.50.1.3\syspro\logo.jpg"
        Else
            LogoPictureBox.ImageLocation = "C:\syspro\logo.jpg"
        End If
    End Sub

    Public Overridable Sub set_window(ByVal arg As FOpacity)
        fmm = arg
    End Sub

    Private Sub FLock_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txt_password.Focus()
    End Sub

    Private Sub FLock_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.F4 Then
            Dim frm As New FLock
            frm.ShowDialog()
        End If
    End Sub

    Public Overrides Sub sb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MD5Encrypt(Trim(txt_password.Text)) <> master_new.ClsVar.sPassword Then
            txt_password.Text = ""
            txt_password.Focus()
            MessageBox.Show("Maaf Password Yang Anda Masukan Salah..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Me.Close()
            fmm.Close()
        End If
    End Sub
End Class
