Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports System.Drawing.Printing
Imports System.IO

Public Class frmPrintDialogReportNew
    Public _report As DevExpress.XtraReports.UI.XtraReport
    Public _remarks As String
    Dim filename As String

    Private Sub RibbonForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            PrintControl1.PrintingSystem = _report.PrintingSystem

            ' Generate the document for preview. 
            If _report.GetType().Name.ToString = "rptDistribusi" Then
                '_report.LblTgl.text = _remarks
                Dim my As rptDistribusi = CType(_report, rptDistribusi)
                my.LblTgl.Text = _remarks.Replace(">>", "to")
            End If

            _report.CreateDocument()
            Me.Text = "Report Preview - " & _report.GetType().Name.ToString
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class