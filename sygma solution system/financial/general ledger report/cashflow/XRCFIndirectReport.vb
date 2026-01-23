Public Class XRCFIndirectReport

    Private Sub XRCFIndirectReport_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
        xpg_data.BestFit()
    End Sub
End Class