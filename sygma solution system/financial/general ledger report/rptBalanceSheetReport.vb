Public Class rptBalanceSheetReport
    Public Posting_Option As Boolean
    Public periode As String
    Private Sub rptProfitLoss_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
        Try
            If Posting_Option = True Then
                LblStatusPosting.Text = ""
            Else
                LblStatusPosting.Text = "Unposting Data"
            End If

            LblPeriode.Text = periode
        Catch ex As Exception

        End Try
    End Sub
End Class