Imports master_new.ModFunction

Public Class rptWOReceipts_1
    Dim flag2 As String
    Dim wod_num As Integer

    Private Sub rptWOReceipts_1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'XrPictureBox1.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")

        If IsDBNull(GetCurrentColumnValue("sign1")) = True Then
            lbl_authorized_1.Visible = False
            xlabel_sign1.Visible = False
            xline_sign1.Visible = False
            xl_by.Visible = False
        Else
            lbl_authorized_1.Visible = True
            xlabel_sign1.Visible = True
            xline_sign1.Visible = True
            xl_by.Visible = True
        End If

        If IsDBNull(GetCurrentColumnValue("post1")) = True Then
            xlabel_post1.Visible = False
        Else
            xlabel_post1.Visible = True
        End If

        If IsDBNull(GetCurrentColumnValue("sign2")) = True Then
            lbl_authorized_2.Visible = False
            xlabel_sign2.Visible = False
            xline_sign2.Visible = False
        Else
            lbl_authorized_2.Visible = True
            xlabel_sign2.Visible = True
            xline_sign2.Visible = True
        End If

        If IsDBNull(GetCurrentColumnValue("post2")) = True Then
            xlabel_post2.Visible = False
        Else
            xlabel_post2.Visible = True
        End If

        If IsDBNull(GetCurrentColumnValue("sign3")) = True Then
            lbl_authorized_3.Visible = False
            xlabel_sign3.Visible = False
            xline_sign3.Visible = False
        Else
            lbl_authorized_3.Visible = True
            xlabel_sign3.Visible = True
            xline_sign3.Visible = True
        End If

        If IsDBNull(GetCurrentColumnValue("post3")) = True Then
            xlabel_post3.Visible = False
        Else
            xlabel_post3.Visible = True
        End If
    End Sub

End Class