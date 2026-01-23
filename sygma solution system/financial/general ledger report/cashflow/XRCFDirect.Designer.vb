<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRCFDirect
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
        Me.xpg_data = New DevExpress.XtraReports.UI.XRPivotGrid
        'Me.Ds_cf_direct1 = New syspro_ver2.ds_cf_direct
        Me.XrPivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField2 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField3 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.XrPivotGridField4 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        'Me.Cfd_tempTableAdapter = New syspro_ver2.ds_cf_directTableAdapters.cfd_tempTableAdapter
        'CType(Me.Ds_cf_direct1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xpg_data})
        Me.Detail.Height = 271
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'xpg_data
        '
        Me.xpg_data.DataMember = "cfd_temp"
        'Me.xpg_data.DataSource = Me.Ds_cf_direct1
        Me.xpg_data.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.XrPivotGridField1, Me.XrPivotGridField2, Me.XrPivotGridField3, Me.XrPivotGridField4})
        Me.xpg_data.Location = New System.Drawing.Point(8, 8)
        Me.xpg_data.Name = "xpg_data"
        Me.xpg_data.OptionsView.ShowColumnHeaders = False
        Me.xpg_data.OptionsView.ShowDataHeaders = False
        Me.xpg_data.OptionsView.ShowRowGrandTotals = False
        Me.xpg_data.OptionsView.ShowRowTotals = False
        Me.xpg_data.Size = New System.Drawing.Size(950, 250)
        '
        'Ds_cf_direct1
        '
        'Me.Ds_cf_direct1.DataSetName = "ds_cf_direct"
        'Me.Ds_cf_direct1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XrPivotGridField1
        '
        Me.XrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField1.AreaIndex = 0
        Me.XrPivotGridField1.Caption = "Group"
        Me.XrPivotGridField1.FieldName = "group_name"
        Me.XrPivotGridField1.Name = "XrPivotGridField1"
        '
        'XrPivotGridField2
        '
        Me.XrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.XrPivotGridField2.AreaIndex = 0
        Me.XrPivotGridField2.Caption = "Month"
        Me.XrPivotGridField2.FieldName = "_month"
        Me.XrPivotGridField2.Name = "XrPivotGridField2"
        '
        'XrPivotGridField3
        '
        Me.XrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.XrPivotGridField3.AreaIndex = 0
        Me.XrPivotGridField3.Caption = "Amount"
        Me.XrPivotGridField3.FieldName = "cfd_amount"
        Me.XrPivotGridField3.Name = "XrPivotGridField3"
        '
        'XrPivotGridField4
        '
        Me.XrPivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.XrPivotGridField4.AreaIndex = 1
        Me.XrPivotGridField4.Caption = "Cashflow Trans."
        Me.XrPivotGridField4.FieldName = "tranline_name"
        Me.XrPivotGridField4.Name = "XrPivotGridField4"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel13, Me.XrLabel20})
        Me.PageHeader.Height = 81
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel13
        '
        Me.XrLabel13.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel13.Location = New System.Drawing.Point(233, 8)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(492, 21)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "PT. SYGMA EXAMEDIA ARKANLEEMA"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(233, 32)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(492, 21)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "CASHFLOW REPORT"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Height = 0
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'Cfd_tempTableAdapter
        '
        'Me.Cfd_tempTableAdapter.ClearBeforeFill = True
        '
        'XRCFDirect
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        'Me.DataAdapter = Me.Cfd_tempTableAdapter
        Me.DataMember = "cfd_temp"
        'Me.DataSource = Me.Ds_cf_direct1
        Me.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.Landscape = True
        Me.PageHeight = 827
        Me.PageWidth = 1169
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Version = "9.2"
        'CType(Me.Ds_cf_direct1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    'Friend WithEvents Ds_cf_direct1 As syspro_ver2.ds_cf_direct
    'Friend WithEvents Cfd_tempTableAdapter As syspro_ver2.ds_cf_directTableAdapters.cfd_tempTableAdapter
    Friend WithEvents XrPivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField2 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField3 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrPivotGridField4 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents xpg_data As DevExpress.XtraReports.UI.XRPivotGrid
End Class
