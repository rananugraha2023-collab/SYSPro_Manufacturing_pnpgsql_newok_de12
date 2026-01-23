Imports DevExpress.XtraPrinting
Imports npgsql
Imports master_new.ModFunction

Public Class XRtransferSQ
    Dim ds_paper As DataSet
    Dim _paper_oid As String
    Dim _available, _landscape_def As Boolean
    Dim _paperkind_def, _topmargin_def, _bottommargin_def, _leftmargin_def, _rightmargin_def As Integer
    Dim _paper_name As String

    Private Sub XRtransferSQ_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        If GetCurrentColumnValue("tranaprvd_name_1") = "" Then
            tranaprvd_line_1.Visible = False
        Else
            tranaprvd_line_1.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_2") = "" Then
            tranaprvd_line_2.Visible = False
        Else
            tranaprvd_line_2.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_3") = "" Then
            tranaprvd_line_3.Visible = False
        Else
            tranaprvd_line_3.Visible = True
        End If

        If GetCurrentColumnValue("tranaprvd_name_4") = "" Then
            tranaprvd_line_4.Visible = False
        Else
            tranaprvd_line_4.Visible = True
        End If
        'cek_paper_available()
        'set_paper()
    End Sub
End Class