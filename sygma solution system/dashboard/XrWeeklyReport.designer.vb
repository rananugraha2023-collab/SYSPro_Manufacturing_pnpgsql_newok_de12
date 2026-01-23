<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XrWeeklyReport
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
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.XrPgWeekly = New DevExpress.XtraReports.UI.XRPivotGrid
        Me.XrPivotGridField16 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField5 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField6 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField7 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField8 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField9 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField10 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField11 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField12 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField13 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField14 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField15 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
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
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 76
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPgWeekly})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 1008
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrPgWeekly
        '
        'Me.XrPgWeekly.Dpi = 254.0!
        'Me.XrPgWeekly.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField16, Me.XrPivotGridField1, Me.XrPivotGridField2, Me.XrPivotGridField3, Me.XrPivotGridField4, Me.XrPivotGridField5, Me.XrPivotGridField6, Me.XrPivotGridField7, Me.XrPivotGridField8, Me.XrPivotGridField9, Me.XrPivotGridField10, Me.XrPivotGridField11, Me.XrPivotGridField12, Me.XrPivotGridField13, Me.XrPivotGridField14, Me.XrPivotGridField15})
        'Me.XrPgWeekly.Location = New System.Drawing.Point(21, 21)
        'Me.XrPgWeekly.Name = "XrPgWeekly"
        'Me.XrPgWeekly.OptionsView.ShowColumnGrandTotals = False
        'Me.XrPgWeekly.OptionsView.ShowColumnTotals = False
        'Me.XrPgWeekly.OptionsView.ShowGrandTotalHeaderIfNoColumnFields = False
        'Me.XrPgWeekly.OptionsView.ShowRowGrandTotals = False
        'Me.XrPgWeekly.OptionsView.ShowRowTotals = False
        'Me.XrPgWeekly.Size = New System.Drawing.Size(2604, 931)
        '
        'XrPivotGridField16
        '
        Me.XrPivotGridField16.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField16.AreaIndex = 2
        Me.XrPivotGridField16.Caption = "Contract"
        Me.XrPivotGridField16.FieldName = "contr_code"
        Me.XrPivotGridField16.Name = "XrPivotGridField16"
        Me.XrPivotGridField16.Width = 130
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "Carage"
        Me.XrPivotGridField1.FieldName = "loc_desc"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        Me.XrPivotGridField1.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField2.AreaIndex = 1
        Me.XrPivotGridField2.Caption = "PPL"
        Me.XrPivotGridField2.FieldName = "ptnr_name"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        Me.XrPivotGridField2.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField3.AreaIndex = 3
        Me.XrPivotGridField3.Caption = "Umur Data"
        Me.XrPivotGridField3.CellFormat.FormatString = "n0"
        Me.XrPivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField3.FieldName = "umur_data"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        Me.XrPivotGridField3.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField3.Width = 60
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField4.AreaIndex = 4
        Me.XrPivotGridField4.Caption = "Umur Actual"
        Me.XrPivotGridField4.CellFormat.FormatString = "n0"
        Me.XrPivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField4.FieldName = "umur_actual"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        Me.XrPivotGridField4.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField4.Width = 60
        '
        'XrPivotGridField5
        '
        Me.XrPivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField5.AreaIndex = 5
        Me.XrPivotGridField5.Caption = "Tgl Chickin"
        Me.XrPivotGridField5.CellFormat.FormatString = "d"
        Me.XrPivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.XrPivotGridField5.FieldName = "tgl_chikin"
        Me.XrPivotGridField5.Name = "XrPivotGridField5"
        Me.XrPivotGridField5.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField5.ValueFormat.FormatString = "d"
        Me.XrPivotGridField5.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'XrPivotGridField6
        '
        Me.XrPivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField6.AreaIndex = 6
        Me.XrPivotGridField6.Caption = "DOC"
        Me.XrPivotGridField6.FieldName = "doc"
        Me.XrPivotGridField6.Name = "XrPivotGridField6"
        Me.XrPivotGridField6.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'XrPivotGridField7
        '
        Me.XrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField7.AreaIndex = 7
        Me.XrPivotGridField7.Caption = "Population"
        Me.XrPivotGridField7.CellFormat.FormatString = "n0"
        Me.XrPivotGridField7.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.FieldName = "population"
        Me.XrPivotGridField7.Name = "XrPivotGridField7"
        Me.XrPivotGridField7.TotalCellFormat.FormatString = "n0"
        Me.XrPivotGridField7.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField7.TotalValueFormat.FormatString = "n0"
        Me.XrPivotGridField7.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField7.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField7.Width = 70
        '
        'XrPivotGridField8
        '
        Me.XrPivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField8.AreaIndex = 0
        Me.XrPivotGridField8.Caption = "Umur"
        Me.XrPivotGridField8.CellFormat.FormatString = "n0"
        Me.XrPivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField8.FieldName = "umur"
        Me.XrPivotGridField8.Name = "XrPivotGridField8"
        Me.XrPivotGridField8.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField8.ValueFormat.FormatString = "n0"
        Me.XrPivotGridField8.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField8.Width = 44
        '
        'XrPivotGridField9
        '
        Me.XrPivotGridField9.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField9.AreaIndex = 0
        Me.XrPivotGridField9.Caption = "Minggu"
        Me.XrPivotGridField9.FieldName = "minggu"
        Me.XrPivotGridField9.Name = "XrPivotGridField9"
        Me.XrPivotGridField9.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'XrPivotGridField10
        '
        Me.XrPivotGridField10.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField10.AreaIndex = 1
        Me.XrPivotGridField10.Caption = "BW Std"
        Me.XrPivotGridField10.CellFormat.FormatString = "n3"
        Me.XrPivotGridField10.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField10.FieldName = "bw_std"
        Me.XrPivotGridField10.Name = "XrPivotGridField10"
        Me.XrPivotGridField10.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField10.ValueFormat.FormatString = "n3"
        Me.XrPivotGridField10.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField10.Width = 56
        '
        'XrPivotGridField11
        '
        Me.XrPivotGridField11.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField11.AreaIndex = 2
        Me.XrPivotGridField11.Caption = "BW Act"
        Me.XrPivotGridField11.CellFormat.FormatString = "n3"
        Me.XrPivotGridField11.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField11.FieldName = "bw_act"
        Me.XrPivotGridField11.Name = "XrPivotGridField11"
        Me.XrPivotGridField11.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField11.ValueFormat.FormatString = "n3"
        Me.XrPivotGridField11.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField11.Width = 53
        '
        'XrPivotGridField12
        '
        Me.XrPivotGridField12.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField12.AreaIndex = 3
        Me.XrPivotGridField12.Caption = "FCR Std"
        Me.XrPivotGridField12.CellFormat.FormatString = "n4"
        Me.XrPivotGridField12.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField12.FieldName = "fcr_std"
        Me.XrPivotGridField12.Name = "XrPivotGridField12"
        Me.XrPivotGridField12.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField12.ValueFormat.FormatString = "n4"
        Me.XrPivotGridField12.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField12.Width = 52
        '
        'XrPivotGridField13
        '
        Me.XrPivotGridField13.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField13.AreaIndex = 4
        Me.XrPivotGridField13.Caption = "FCR Act"
        Me.XrPivotGridField13.CellFormat.FormatString = "n4"
        Me.XrPivotGridField13.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField13.FieldName = "fcr_act"
        Me.XrPivotGridField13.Name = "XrPivotGridField13"
        Me.XrPivotGridField13.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField13.ValueFormat.FormatString = "n4"
        Me.XrPivotGridField13.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField13.Width = 49
        '
        'XrPivotGridField14
        '
        Me.XrPivotGridField14.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField14.AreaIndex = 5
        Me.XrPivotGridField14.Caption = "Dpl Std"
        Me.XrPivotGridField14.CellFormat.FormatString = "n2"
        Me.XrPivotGridField14.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField14.FieldName = "dpl_std"
        Me.XrPivotGridField14.Name = "XrPivotGridField14"
        Me.XrPivotGridField14.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField14.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField14.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField14.Width = 50
        '
        'XrPivotGridField15
        '
        Me.XrPivotGridField15.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField15.AreaIndex = 6
        Me.XrPivotGridField15.Caption = "Dpl Act"
        Me.XrPivotGridField15.CellFormat.FormatString = "n2"
        Me.XrPivotGridField15.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField15.FieldName = "dpl_act"
        Me.XrPivotGridField15.Name = "XrPivotGridField15"
        Me.XrPivotGridField15.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.XrPivotGridField15.ValueFormat.FormatString = "n2"
        Me.XrPivotGridField15.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.XrPivotGridField15.Width = 54
        '
        'XrWeeklyReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(150, 150, 150, 150)
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Public WithEvents XrPgWeekly As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField5 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField6 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField7 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField8 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField9 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField10 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField11 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField12 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField13 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField14 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField15 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField16 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
End Class
