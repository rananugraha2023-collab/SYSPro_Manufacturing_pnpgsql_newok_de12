Public Class rptLabelHarga108
    Dim _data As String
    Private Sub XrBarCode1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles XrBarCode1.BeforePrint
        _data = GetCurrentColumnValue("kode_barcode").ToString.Replace("*", "").Replace("PT", "")
        XrBarCode1.Text = _data & "00"
    End Sub
End Class