Imports MessagingToolkit.QRCode

Public Class rptLabelQRCode
    Dim _data As String
    Dim QR_code As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Private Sub XrBarCode1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
        '_data = GetCurrentColumnValue("kode_barcode").ToString.Replace("*", "").Replace("PT", "")
        'XrBarCode1.Text = _data & "00"

    End Sub

    Private Sub XrPictureBox1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles XrPictureBox1.BeforePrint
        Try
            Dim _data As String = GetCurrentColumnValue("qrcode_oid").ToString
            XrPictureBox1.Image = QR_code.Encode(_data)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rptLabelLocationSub_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

    End Sub
End Class