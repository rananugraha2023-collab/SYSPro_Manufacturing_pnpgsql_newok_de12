Imports MyPGDll.ModFunction
Public Class rptTransJurnal

    Private Sub rptTransJurnal_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            LPeriode.Text = Parameters("PPeriode").Value
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class