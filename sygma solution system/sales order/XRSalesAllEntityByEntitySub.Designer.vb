<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSalesAllEntityByEntitySub
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
        Dim PointSeriesLabel1 As DevExpress.XtraCharts.PointSeriesLabel = New DevExpress.XtraCharts.PointSeriesLabel
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrChart1 = New DevExpress.XtraReports.UI.XRChart
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.XrEntity = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageBreak1 = New DevExpress.XtraReports.UI.XRPageBreak
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrSalesAllEntitySub1 = New sygma_solution_system.XRSalesAllEntitySub
        Me.XrPageBreak2 = New DevExpress.XtraReports.UI.XRPageBreak
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PointSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrSalesAllEntitySub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrChart1, Me.XrPivotGrid1, Me.XrLabel1, Me.XrPeriode, Me.XrEntity, Me.XrPageBreak1})
        Me.Detail.Height = 877
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("en_desc", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrChart1
        '
        Me.XrChart1.AppearanceName = "Northern Lights"
        Me.XrChart1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.XrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrChart1.Location = New System.Drawing.Point(0, 92)
        Me.XrChart1.Name = "XrChart1"
        Me.XrChart1.PaletteName = "Nature Colors"
        Me.XrChart1.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        PointSeriesLabel1.LineVisible = True
        Me.XrChart1.SeriesTemplate.Label = PointSeriesLabel1
        Me.XrChart1.SeriesTemplate.View = LineSeriesView1
        Me.XrChart1.Size = New System.Drawing.Size(1067, 442)
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField1, Me.XrPivotGridField2, Me.XrPivotGridField3})
        Me.XrPivotGrid1.Location = New System.Drawing.Point(0, 542)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.Size = New System.Drawing.Size(1067, 325)
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "Sales"
        Me.XrPivotGridField1.FieldName = "ptnr_name"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        Me.XrPivotGridField1.Width = 170
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Periode"
        Me.XrPivotGridField2.FieldName = "ngroup"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.Width = 60
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField3.AreaIndex = 0
        Me.XrPivotGridField3.Caption = "Amount"
        Me.XrPivotGridField3.CellFormat.FormatString = "n1"
        Me.XrPivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.FieldName = "nvalue"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.ValueFormat.FormatString = "n1"
        Me.XrPivotGridField3.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(117, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(833, 25)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "SALES REPORT BY SALES"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPeriode
        '
        Me.XrPeriode.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.XrPeriode.Location = New System.Drawing.Point(117, 50)
        Me.XrPeriode.Name = "XrPeriode"
        Me.XrPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPeriode.Size = New System.Drawing.Size(833, 25)
        Me.XrPeriode.StylePriority.UseFont = False
        Me.XrPeriode.StylePriority.UseTextAlignment = False
        Me.XrPeriode.Text = "PERIODE"
        Me.XrPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrEntity
        '
        Me.XrEntity.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.en_desc", "")})
        Me.XrEntity.Font = New System.Drawing.Font("Times New Roman", 12.0!)
        Me.XrEntity.Location = New System.Drawing.Point(117, 25)
        Me.XrEntity.Name = "XrEntity"
        Me.XrEntity.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrEntity.Size = New System.Drawing.Size(833, 25)
        Me.XrEntity.StylePriority.UseFont = False
        Me.XrEntity.StylePriority.UseTextAlignment = False
        Me.XrEntity.Text = "ENTITY"
        Me.XrEntity.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrPageBreak1
        '
        Me.XrPageBreak1.Location = New System.Drawing.Point(0, 875)
        Me.XrPageBreak1.Name = "XrPageBreak1"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrSubreport1, Me.XrPageBreak2})
        Me.ReportHeader.Height = 27
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Location = New System.Drawing.Point(0, 0)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ReportSource = Me.XrSalesAllEntitySub1
        Me.XrSubreport1.Size = New System.Drawing.Size(1067, 17)
        '
        'XrSalesAllEntitySub1
        '
        Me.XrSalesAllEntitySub1.Landscape = True
        Me.XrSalesAllEntitySub1.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.XrSalesAllEntitySub1.Name = "XrSalesAllEntitySub1"
        Me.XrSalesAllEntitySub1.PageColor = System.Drawing.Color.White
        Me.XrSalesAllEntitySub1.PageHeight = 827
        Me.XrSalesAllEntitySub1.PageWidth = 1169
        Me.XrSalesAllEntitySub1.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.XrSalesAllEntitySub1.Version = "9.2"
        '
        'XrPageBreak2
        '
        Me.XrPageBreak2.Location = New System.Drawing.Point(0, 25)
        Me.XrPageBreak2.Name = "XrPageBreak2"
        '
        'XRSalesAllEntityByEntitySub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.ReportHeader})
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 827
        Me.PageWidth = 1169
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Version = "9.2"
        CType(PointSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrChart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrSalesAllEntitySub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Public WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrPeriode As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrEntity As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrChart1 As DevExpress.XtraReports.UI.XRChart
    Friend WithEvents XrPageBreak1 As DevExpress.XtraReports.UI.XRPageBreak
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Private WithEvents XrSalesAllEntitySub1 As sygma_solution_system.XRSalesAllEntitySub
    Friend WithEvents XrPageBreak2 As DevExpress.XtraReports.UI.XRPageBreak
End Class
