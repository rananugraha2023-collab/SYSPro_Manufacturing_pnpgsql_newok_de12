Imports master_new.ModFunction
Public Class FWhatsNew

    Private Sub FWhatsNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadRTF(appbase() & "\zpendukung\WhatsNew.rtf", rtbWhatsNew)
    End Sub
End Class