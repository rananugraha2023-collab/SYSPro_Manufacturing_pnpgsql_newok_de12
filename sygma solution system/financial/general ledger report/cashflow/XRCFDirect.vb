Public Class XRCFDirect

    Private Sub XRCFDirect_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        xpg_data.BestFit()
    End Sub
End Class