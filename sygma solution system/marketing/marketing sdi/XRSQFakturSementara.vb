Imports master_new.ModFunction
Imports DevExpress.XtraPrinting
Imports npgsql

Public Class XRSQFakturSementara

    Private Sub Detail_AfterPrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Detail.AfterPrint
        Try
            If GetCurrentColumnValue("sqd_tax_inc") = "Y" Then
                XrTableCell59.Visible = False
                XrTableCell60.Visible = False
                XrTableCell61.Visible = False
                XrTableCell65.Visible = False
                XrTableCell66.Visible = False
                XrTableCell67.Visible = False
                XrLabel1.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub XRInvoice_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        If GetCurrentColumnValue("tranaprvd_name_1") = "" Then
            tranaprvd_line1.Visible = False
        Else
            tranaprvd_line1.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_2") = "" Then
            tranaprvd_line2.Visible = False
        Else
            tranaprvd_line2.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_3") = "" Then
            tranaprvd_line3.Visible = False
        Else
            tranaprvd_line3.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_4") = "" Then
            tranaprvd_line4.Visible = False
        Else
            tranaprvd_line4.Visible = True
        End If

    End Sub
End Class