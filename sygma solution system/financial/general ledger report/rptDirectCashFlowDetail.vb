Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class rptDirectCashFlowDetail
    Public Posting_Option As Boolean
    Public periode As String
    Public glt_periode As String
    Private Sub rptProfitLoss_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
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

    Private Sub ReportFooter_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles ReportFooter.BeforePrint

    End Sub

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Try
            Dim ssql As String
            
            ssql = "SELECT  " _
                & "  public.cf_save.glt_periode, " _
                & "  public.cf_save.glt_code, " _
                & "  public.cf_save.glt_date, " _
                & "  public.cf_save.glt_ac_id, " _
                & "  public.cf_save.ac_code, " _
                & "  public.cf_save.ac_name, " _
                & "  public.cf_save.glt_desc, " _
                & "  public.cf_save.glt_debit, " _
                & "  public.cf_save.glt_credit, " _
                & "  public.cf_save.glt_ref_trans_code, " _
                & "  public.cf_save.glt_oid " _
                & "FROM " _
                & "  public.cf_save WHERE glt_code in (SELECT  " _
                & "  public.cf_save.glt_code " _
                & "FROM " _
                & "  public.cf_save " _
                & "WHERE " _
                & "  public.cf_save.glt_periode = '" & glt_periode _
                & "' and  public.cf_save.ac_code= " & SetSetring(GetCurrentColumnValue("ac_code"))

            If GetCurrentColumnValue("ac_sign") = "C" Then
                ssql = ssql & " and public.cf_save.glt_credit <> 0.0 "
            Else
                ssql = ssql & " and public.cf_save.glt_debit <> 0.0 "
            End If

            ssql = ssql & " )"



            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rpt2 As New XtraReportCFDirectSub

            XrSubreport1.ReportSource = rpt2
            rpt2.DataSource = ds_sub_report
            rpt2.DataMember = "Table"

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
End Class