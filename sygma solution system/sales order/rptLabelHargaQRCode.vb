Public Class rptLabelHargaQRCode
    Dim _data As String
    Dim QR_code As New MessagingToolkit.QRCode.Codec.QRCodeEncoder

    Private Sub XrBarCode1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles XrBarCode1.BeforePrint
        _data = GetCurrentColumnValue("kode_barcode").ToString.Replace("*", "").Replace("PT", "")
        XrBarCode1.Text = _data & "00"

        Try
            XrPictureBox1.Image = QR_code.Encode(GetCurrentColumnValue("pt_code"))
        Catch ex As Exception
        End Try

    End Sub
End Class