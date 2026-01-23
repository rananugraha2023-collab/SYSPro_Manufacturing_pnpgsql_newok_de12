Imports master_new.ModFunction

Public Class rptProfitLossDetailReportNew
    Public Posting_Option As Boolean
    Public periode As String
    Dim v_nilai_1 As Double = 0
    Dim v_nilai_2 As Double = 0
    Dim v_nilai_3 As Double = 0
    Dim v_nilai_4 As Double = 0
    Dim v_nilai_5 As Double = 0
    Dim v_nilai_6 As Double = 0
    Dim v_nilai_7 As Double = 0
    Dim v_nilai_8 As Double = 0
    Dim v_nilai_9 As Double = 0
    Dim v_nilai_10 As Double = 0
    Dim v_nilai_11 As Double = 0
    Dim v_nilai_12 As Double = 0

    Private Sub rptProfitLoss_BeforePrint(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.BeforePrint
        Try
            'If Posting_Option = True Then
            '    LblStatusPosting.Text = ""
            'Else
            '    LblStatusPosting.Text = "Unposting Data"
            'End If

            LblPeriode.Text = periode
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        v_nilai_1 = v_nilai_1 + (SetNumber(GetCurrentColumnValue("v_nilai1")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_2 = v_nilai_2 + (SetNumber(GetCurrentColumnValue("v_nilai2")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_3 = v_nilai_3 + (SetNumber(GetCurrentColumnValue("v_nilai3")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_4 = v_nilai_4 + (SetNumber(GetCurrentColumnValue("v_nilai4")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_5 = v_nilai_5 + (SetNumber(GetCurrentColumnValue("v_nilai5")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_6 = v_nilai_6 + (SetNumber(GetCurrentColumnValue("v_nilai6")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_7 = v_nilai_7 + (SetNumber(GetCurrentColumnValue("v_nilai7")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_8 = v_nilai_8 + (SetNumber(GetCurrentColumnValue("v_nilai8")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_9 = v_nilai_9 + (SetNumber(GetCurrentColumnValue("v_nilai9")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_10 = v_nilai_10 + (SetNumber(GetCurrentColumnValue("v_nilai10")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_11 = v_nilai_11 + (SetNumber(GetCurrentColumnValue("v_nilai11")) * SetNumber(GetCurrentColumnValue("value")))
        v_nilai_12 = v_nilai_12 + (SetNumber(GetCurrentColumnValue("v_nilai12")) * SetNumber(GetCurrentColumnValue("value")))

    End Sub

    Private Sub GroupFooter1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        footer_01.Text = master_new.ModFunction.MaskDec(v_nilai_1, 0)
        footer_02.Text = master_new.ModFunction.MaskDec(v_nilai_2, 0)
        footer_03.Text = master_new.ModFunction.MaskDec(v_nilai_3, 0)
        footer_04.Text = master_new.ModFunction.MaskDec(v_nilai_4, 0)
        footer_05.Text = master_new.ModFunction.MaskDec(v_nilai_5, 0)
        footer_06.Text = master_new.ModFunction.MaskDec(v_nilai_6, 0)
        footer_07.Text = master_new.ModFunction.MaskDec(v_nilai_7, 0)
        footer_08.Text = master_new.ModFunction.MaskDec(v_nilai_8, 0)
        footer_09.Text = master_new.ModFunction.MaskDec(v_nilai_9, 0)
        footer_10.Text = master_new.ModFunction.MaskDec(v_nilai_10, 0)
        footer_11.Text = master_new.ModFunction.MaskDec(v_nilai_11, 0)
        footer_12.Text = master_new.ModFunction.MaskDec(v_nilai_12, 0)


    End Sub
End Class