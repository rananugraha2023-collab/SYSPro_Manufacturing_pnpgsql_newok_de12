Imports master_new.ModFunction

Public Class rptNeracaFix
    Public Posting_Option As Boolean
    Public periode As String
    Private Sub rptNeracaFix_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
        Try
            If Posting_Option = True Then
                LblStatusPosting.Text = ""
            Else
                LblStatusPosting.Text = "Unposting Data"
            End If

            LblPeriode.Text = periode
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class