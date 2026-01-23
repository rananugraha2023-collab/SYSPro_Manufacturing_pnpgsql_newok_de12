Imports DevExpress.XtraPrinting
Imports npgsql
Imports master_new.ModFunction

Public Class XRProducttop10diskon

    Private Sub XRProducttop10_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try

            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        Catch ex As Exception

        End Try
    End Sub
End Class