Public Class XRSEBankReport

    Private Sub XRPSReport_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            'TreeList1.ExpandAll()
        Catch ex As Exception
        End Try
    End Sub
End Class