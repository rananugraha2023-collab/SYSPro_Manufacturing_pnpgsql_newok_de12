

Public Class XR_routing


    Private Sub XR_routing_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        'If IsDBNull(GetCurrentColumnValue("sign1")) = True Then
        '    lbl_authorized_1.Visible = False
        '    xlabel_sign1.Visible = False
        '    xline_sign1.Visible = False
        '    xl_by.Visible = False
        'Else
        '    lbl_authorized_1.Visible = True
        '    xlabel_sign1.Visible = True
        '    xline_sign1.Visible = True
        '    xl_by.Visible = True
        'End If

        'If IsDBNull(GetCurrentColumnValue("post1")) = True Then
        '    xlabel_post1.Visible = False
        'Else
        '    xlabel_post1.Visible = True
        'End If

        'If IsDBNull(GetCurrentColumnValue("sign2")) = True Then
        '    lbl_authorized_2.Visible = False
        '    xlabel_sign2.Visible = False
        '    xline_sign2.Visible = False
        'Else
        '    lbl_authorized_2.Visible = True
        '    xlabel_sign2.Visible = True
        '    xline_sign2.Visible = True
        'End If

        'If IsDBNull(GetCurrentColumnValue("post2")) = True Then
        '    xlabel_post2.Visible = False
        'Else
        '    xlabel_post2.Visible = True
        'End If

        'If IsDBNull(GetCurrentColumnValue("sign3")) = True Then
        '    lbl_authorized_3.Visible = False
        '    xlabel_sign3.Visible = False
        '    xline_sign3.Visible = False
        'Else
        '    lbl_authorized_3.Visible = True
        '    xlabel_sign3.Visible = True
        '    xline_sign3.Visible = True
        'End If

        'If IsDBNull(GetCurrentColumnValue("post3")) = True Then
        '    xlabel_post3.Visible = False
        'Else
        '    xlabel_post3.Visible = True
        'End If
        Try
            xrpb_logo.Image = New Bitmap(master_new.ModFunction.appbase() + "\zpendukung\logo.jpg")

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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class