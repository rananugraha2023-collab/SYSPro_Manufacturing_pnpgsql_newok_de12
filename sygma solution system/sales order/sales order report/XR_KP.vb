Imports master_new.ModFunction
Imports DevExpress.XtraPrinting
Imports npgsql

Public Class XR_KP

    Private Sub XR_KP_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
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