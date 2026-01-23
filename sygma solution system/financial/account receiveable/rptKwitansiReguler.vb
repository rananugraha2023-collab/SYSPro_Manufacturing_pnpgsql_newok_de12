Imports master_new.ModFunction
Public Class rptKwitansiReguler
    Dim _reff As String = ""

    Private Sub rptKwitansiReguler_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            XRTerbilang.Text = Terbilang(SetNumber(GetCurrentColumnValue("arpay_total_amount"))) & " Rupiah"
            xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        _reff = _reff & ", " & GetCurrentColumnValue("arpayd_ar_ref").ToString
    End Sub


    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        If _reff = "" Then
        Else
            _reff = Microsoft.VisualBasic.Right(_reff, _reff.Length - 2)
        End If
        XrLabelReff.Text = _reff
    End Sub


    Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        _reff = ""
    End Sub
End Class