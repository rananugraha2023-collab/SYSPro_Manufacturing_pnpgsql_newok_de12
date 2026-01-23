Imports master_new.ModFunction

Public Class rptWOPickListOld
    Dim flag2 As String
    Dim wod_num As Integer

    'Private Sub XRWorkOrder_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
    '    'xrpb_logo.Image = New Bitmap(appbase() + "\zpendukung\logo.jpg")
    '    wod_num = 0
    'End Sub

    'Private Sub GroupHeader1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
    '    If flag2 <> GetCurrentColumnValue("wo_oid").ToString Then
    '        wod_num = 0
    '    End If
    '    flag2 = GetCurrentColumnValue("wo_oid").ToString

    'End Sub

    'Private Sub GroupHeader2_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint

    '    wod_num = wod_num + 1
    '    XrLabel16.Text = wod_num

    'End Sub
End Class