<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XR_LabourfeedbackSummary
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid()
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrPivotGridField7 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPeriode = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.TopMarginBand1 = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMarginBand1 = New DevExpress.XtraReports.UI.BottomMarginBand()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 0.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.HeightF = 0.0!
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrPageInfo2})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.HeightF = 85.0!
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(1714.0!, 0.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(254.0!, 64.0!)
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 254.0!
        Me.XrPageInfo2.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(487.0!, 64.0!)
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPivotGrid1, Me.XrLabel1, Me.XrPeriode})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.HeightF = 1059.0!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.XrPivotGrid1.CellStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.CustomTotalCellStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.Dpi = 254.0!
        Me.XrPivotGrid1.FieldHeaderStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField1, Me.XrPivotGridField2, Me.XrPivotGridField3, Me.XrPivotGridField4, Me.XrPivotGridField5, Me.XrPivotGridField6, Me.XrPivotGridField7})
        Me.XrPivotGrid1.FieldValueGrandTotalStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.FieldValueStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.FieldValueTotalStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.FilterSeparatorStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.GrandTotalCellStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.HeaderGroupLineStyleName = "XrControlStyle1"
        Me.XrPivotGrid1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 233.0!)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3
        Me.XrPivotGrid1.OptionsView.ShowRowGrandTotals = False
        Me.XrPivotGrid1.SizeF = New System.Drawing.SizeF(1968.0!, 826.0!)
        Me.XrPivotGrid1.TotalCellStyleName = "XrControlStyle1"
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "Work Center"
        Me.XrPivotGridField1.FieldName = "wc_desc"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        Me.XrPivotGridField1.Width = 140
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Shift"
        Me.XrPivotGridField2.FieldName = "shift_name"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.Width = 74
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField3.AreaIndex = 0
        Me.XrPivotGridField3.Caption = "Output"
        Me.XrPivotGridField3.CellFormat.FormatString = "n2"
        Me.XrPivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.FieldName = "lbrf_qty_complete"
        Me.XrPivotGridField3.GrandTotalCellFormat.FormatString = "n2"
        Me.XrPivotGridField3.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.TotalCellFormat.FormatString = "n2"
        Me.XrPivotGridField3.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.TotalValueFormat.FormatString = "n2"
        Me.XrPivotGridField3.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField3.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.Width = 81
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField4.AreaIndex = 1
        Me.XrPivotGridField4.Caption = "Qty Reject"
        Me.XrPivotGridField4.CellFormat.FormatString = "n2"
        Me.XrPivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.FieldName = "lbrf_qty_reject"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.TotalCellFormat.FormatString = "n2"
        Me.XrPivotGridField4.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.TotalValueFormat.FormatString = "n2"
        Me.XrPivotGridField4.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.Visible = False
        Me.XrPivotGridField4.Width = 68
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField5.AreaIndex = 3
        Me.XrPivotGridField5.Caption = "Machine"
        Me.XrPivotGridField5.FieldName = "mch_name"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        Me.XrPivotGridField5.Width = 80
        '
        'XrPivotGridField6
        '
        Me.XrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField6.AreaIndex = 1
        Me.XrPivotGridField6.Caption = "Standart Book"
        Me.XrPivotGridField6.FieldName = "dpt_lbr_cap_book"
        Me.XrPivotGridField6.Name = "XrPivotGridField6"
        Me.XrPivotGridField6.TotalValueFormat.FormatString = "n2"
        Me.XrPivotGridField6.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField6.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField6.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField6.Width = 80
        '
        'XrPivotGridField7
        '
        Me.XrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField7.AreaIndex = 2
        Me.XrPivotGridField7.Caption = "Standart Quran"
        Me.XrPivotGridField7.FieldName = "dpt_lbr_cap"
        Me.XrPivotGridField7.Name = "XrPivotGridField7"
        Me.XrPivotGridField7.TotalValueFormat.FormatString = "n2"
        Me.XrPivotGridField7.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField7.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.Width = 80
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(444.0!, 0.0!)
        Me.XrLabel1.Multiline = True
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(1101.0!, 148.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "WORK CENTER OUTPUT" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DAILY REPORT"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPeriode
        '
        Me.XrPeriode.Dpi = 254.0!
        Me.XrPeriode.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPeriode.LocationFloat = New DevExpress.Utils.PointFloat(444.0!, 151.0!)
        Me.XrPeriode.Name = "XrPeriode"
        Me.XrPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPeriode.SizeF = New System.Drawing.SizeF(1101.0!, 64.0!)
        Me.XrPeriode.StylePriority.UseFont = False
        Me.XrPeriode.StylePriority.UseTextAlignment = False
        Me.XrPeriode.Text = "PERIODE"
        Me.XrPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.HeightF = 53.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrControlStyle1
        '
        Me.XrControlStyle1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrControlStyle1.Name = "XrControlStyle1"
        '
        'TopMarginBand1
        '
        Me.TopMarginBand1.Dpi = 254.0!
        Me.TopMarginBand1.HeightF = 150.0!
        Me.TopMarginBand1.Name = "TopMarginBand1"
        '
        'BottomMarginBand1
        '
        Me.BottomMarginBand1.Dpi = 254.0!
        Me.BottomMarginBand1.HeightF = 150.0!
        Me.BottomMarginBand1.Name = "BottomMarginBand1"
        '
        'XR_LabourfeedbackSummary
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader, Me.ReportFooter, Me.TopMarginBand1, Me.BottomMarginBand1})
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(60, 60, 150, 150)
        Me.PageHeight = 2970
        Me.PageWidth = 2100
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.XrControlStyle1})
        Me.Version = "12.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Public WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrControlStyle1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents XrPivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField7 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents TopMarginBand1 As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMarginBand1 As DevExpress.XtraReports.UI.BottomMarginBand
End Class
