Imports DevExpress.XtraPrinting.BarCode

Public Class rptLabelHarga108b
    Dim _data As String

    Private Sub XrBarCode1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs)
        '_data = GetCurrentColumnValue("kode_barcode").ToString.Replace("*", "").Replace("PT", "")

        XrLabel3.Text = GetCurrentColumnValue("kode_barcode")
        'XrBarCode1.Text = GetCurrentColumnValue("kode_barcode").ToString.Replace("*", "")


        'XrBarCode1.Symbology = symb
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'XrBarCode1.Symbology = New Code128Generator()
        'CType(XrBarCode1.Symbology, Code128Generator).CharacterSet = Code128Charset.CharsetB
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class