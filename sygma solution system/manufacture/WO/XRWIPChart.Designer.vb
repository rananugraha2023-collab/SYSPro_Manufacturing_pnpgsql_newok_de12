<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRWIPChart
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
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim PointOptions1 As DevExpress.XtraCharts.PointOptions = New DevExpress.XtraCharts.PointOptions
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series
        Dim SideBySideBarSeriesLabel2 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim PointOptions2 As DevExpress.XtraCharts.PointOptions = New DevExpress.XtraCharts.PointOptions
        Dim Series3 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series
        Dim SideBySideBarSeriesLabel3 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim PointOptions3 As DevExpress.XtraCharts.PointOptions = New DevExpress.XtraCharts.PointOptions
        Dim Series4 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series
        Dim SideBySideBarSeriesLabel4 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim PointOptions4 As DevExpress.XtraCharts.PointOptions = New DevExpress.XtraCharts.PointOptions
        Dim SideBySideBarSeriesLabel5 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrChart1 = New DevExpress.XtraReports.UI.XRChart
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 64
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrChart1, Me.XrPivotGrid1, Me.XrLabel1, Me.XrPeriode})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 2193
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrChart1
        '
        Me.XrChart1.AppearanceName = "Gray"
        Me.XrChart1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.XrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None
        XyDiagram1.AxisX.Label.Angle = 45
        XyDiagram1.AxisX.Range.SideMarginsEnabled = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.NumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        XyDiagram1.AxisY.NumericOptions.Precision = 0
        XyDiagram1.AxisY.Range.SideMarginsEnabled = True
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.XrChart1.Diagram = XyDiagram1
        Me.XrChart1.Dpi = 254.0!
        Me.XrChart1.Location = New System.Drawing.Point(21, 169)
        Me.XrChart1.Name = "XrChart1"
        Me.XrChart1.PaletteBaseColorNumber = 5
        Me.XrChart1.PaletteName = "Verve"
        Series1.ArgumentDataMember = "ngroup"
        SideBySideBarSeriesLabel1.Border.Visible = False
        SideBySideBarSeriesLabel1.LineVisible = True
        SideBySideBarSeriesLabel1.Visible = False
        Series1.Label = SideBySideBarSeriesLabel1
        Series1.Name = "Series 1"
        PointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        PointOptions1.ValueNumericOptions.Precision = 0
        Series1.PointOptions = PointOptions1
        Series1.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending
        Series1.ValueDataMembersSerializable = "nvalue"
        Series2.ArgumentDataMember = "ngroup"
        SideBySideBarSeriesLabel2.Border.Visible = False
        SideBySideBarSeriesLabel2.LineVisible = True
        SideBySideBarSeriesLabel2.Visible = False
        Series2.Label = SideBySideBarSeriesLabel2
        Series2.Name = "Series 2"
        PointOptions2.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        PointOptions2.ValueNumericOptions.Precision = 0
        Series2.PointOptions = PointOptions2
        Series2.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending
        Series2.ValueDataMembersSerializable = "nvalue"
        Series3.ArgumentDataMember = "ngroup"
        SideBySideBarSeriesLabel3.LineVisible = True
        SideBySideBarSeriesLabel3.Visible = False
        Series3.Label = SideBySideBarSeriesLabel3
        Series3.Name = "Series 3"
        PointOptions3.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        PointOptions3.ValueNumericOptions.Precision = 0
        Series3.PointOptions = PointOptions3
        Series3.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending
        Series3.ValueDataMembersSerializable = "nvalue"
        Series4.ArgumentDataMember = "ngroup"
        SideBySideBarSeriesLabel4.LineVisible = True
        SideBySideBarSeriesLabel4.Visible = False
        Series4.Label = SideBySideBarSeriesLabel4
        Series4.Name = "Series 4"
        PointOptions4.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Number
        PointOptions4.ValueNumericOptions.Precision = 0
        Series4.PointOptions = PointOptions4
        Series4.SeriesPointsSorting = DevExpress.XtraCharts.SortingMode.Ascending
        Series4.ValueDataMembersSerializable = "nvalue"
        Me.XrChart1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1, Series2, Series3, Series4}
        Me.XrChart1.SeriesSorting = DevExpress.XtraCharts.SortingMode.Ascending
        SideBySideBarSeriesLabel5.LineVisible = True
        Me.XrChart1.SeriesTemplate.Label = SideBySideBarSeriesLabel5
        Me.XrChart1.Size = New System.Drawing.Size(2730, 1101)
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.Appearance.FieldHeader.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.XrPivotGrid1.Dpi = 254.0!
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField1, Me.XrPivotGridField2, Me.XrPivotGridField3, Me.XrPivotGridField5, Me.XrPivotGridField4})
        Me.XrPivotGrid1.Location = New System.Drawing.Point(21, 1291)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.Size = New System.Drawing.Size(2730, 804)
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "Work Center"
        Me.XrPivotGridField1.FieldName = "ngroup"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        Me.XrPivotGridField1.Width = 200
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Standar Quran"
        Me.XrPivotGridField2.CellFormat.FormatString = "n0"
        Me.XrPivotGridField2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField2.FieldName = "nvalue_standard"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField2.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField2.Width = 110
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField3.AreaIndex = 1
        Me.XrPivotGridField3.Caption = "WIP Quran"
        Me.XrPivotGridField3.CellFormat.FormatString = "n0"
        Me.XrPivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.FieldName = "nvalue_wip"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.TotalCellFormat.FormatString = "n0"
        Me.XrPivotGridField3.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.TotalValueFormat.FormatString = "n0"
        Me.XrPivotGridField3.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField3.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.Width = 117
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField5.AreaIndex = 2
        Me.XrPivotGridField5.Caption = "Standard Book"
        Me.XrPivotGridField5.CellFormat.FormatString = "n0"
        Me.XrPivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField5.FieldName = "nvalue_standard_book"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        Me.XrPivotGridField5.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField5.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField5.Width = 110
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField4.AreaIndex = 3
        Me.XrPivotGridField4.Caption = "WIP Book"
        Me.XrPivotGridField4.CellFormat.FormatString = "n0"
        Me.XrPivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.FieldName = "nvalue_wip_book"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.Width = 117
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(339, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(2011, 64)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "WORK CENTER OUTSTANDING REPORT"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPeriode
        '
        Me.XrPeriode.Dpi = 254.0!
        Me.XrPeriode.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPeriode.ForeColor = System.Drawing.Color.White
        Me.XrPeriode.Location = New System.Drawing.Point(826, 64)
        Me.XrPeriode.Name = "XrPeriode"
        Me.XrPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPeriode.Size = New System.Drawing.Size(1101, 64)
        Me.XrPeriode.StylePriority.UseFont = False
        Me.XrPeriode.StylePriority.UseForeColor = False
        Me.XrPeriode.StylePriority.UseTextAlignment = False
        Me.XrPeriode.Text = "PERIODE"
        Me.XrPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(2011, 0)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(741, 64)
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XRWIPChart
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(100, 100, 125, 125)
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Public WithEvents XrChart1 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Public WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
End Class
