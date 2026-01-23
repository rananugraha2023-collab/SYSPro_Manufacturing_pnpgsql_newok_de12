<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRPSReport
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
        Me.WinControlContainer1 = New DevExpress.XtraReports.UI.WinControlContainer
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.ptnr_name = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.lvl_name = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_sales_amount = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_group_total = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_bonus_recruitment = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_sales_bonus = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_group_bonus = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_thp_total = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_sales_amount_recruitment = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabelPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.WinControlContainer1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 148
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'WinControlContainer1
        '
        Me.WinControlContainer1.Dpi = 254.0!
        Me.WinControlContainer1.Location = New System.Drawing.Point(0, 0)
        Me.WinControlContainer1.Name = "WinControlContainer1"
        Me.WinControlContainer1.Size = New System.Drawing.Size(16, 16)
        Me.WinControlContainer1.WinControl = Me.TreeList1
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.ptnr_name, Me.lvl_name, Me.psgend_sales_amount, Me.psgend_group_total, Me.psgend_bonus_recruitment, Me.psgend_sales_bonus, Me.psgend_group_bonus, Me.psgend_thp_total, Me.psgend_sales_amount_recruitment})
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "ptnr_id"
        Me.TreeList1.Location = New System.Drawing.Point(0, 0)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.PopulateServiceColumns = True
        Me.TreeList1.OptionsPrint.AutoWidth = False
        Me.TreeList1.OptionsPrint.PrintAllNodes = True
        Me.TreeList1.OptionsPrint.UsePrintStyles = True
        Me.TreeList1.OptionsView.AutoWidth = False
        Me.TreeList1.ParentFieldName = "ptnr_parent"
        Me.TreeList1.Size = New System.Drawing.Size(6, 6)
        Me.TreeList1.TabIndex = 0
        '
        'ptnr_name
        '
        Me.ptnr_name.Caption = "Name"
        Me.ptnr_name.FieldName = "ptnr_name"
        Me.ptnr_name.Name = "ptnr_name"
        Me.ptnr_name.SortOrder = System.Windows.Forms.SortOrder.Ascending
        Me.ptnr_name.Visible = True
        Me.ptnr_name.VisibleIndex = 0
        Me.ptnr_name.Width = 251
        '
        'lvl_name
        '
        Me.lvl_name.Caption = "Level"
        Me.lvl_name.FieldName = "lvl_name"
        Me.lvl_name.Name = "lvl_name"
        Me.lvl_name.Visible = True
        Me.lvl_name.VisibleIndex = 1
        Me.lvl_name.Width = 89
        '
        'psgend_sales_amount
        '
        Me.psgend_sales_amount.AllNodesSummary = True
        Me.psgend_sales_amount.Caption = "Sales Amount"
        Me.psgend_sales_amount.FieldName = "psgend_sales_amount"
        Me.psgend_sales_amount.Format.FormatString = "n0"
        Me.psgend_sales_amount.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_sales_amount.Name = "psgend_sales_amount"
        Me.psgend_sales_amount.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_sales_amount.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_sales_amount.Visible = True
        Me.psgend_sales_amount.VisibleIndex = 2
        Me.psgend_sales_amount.Width = 89
        '
        'psgend_group_total
        '
        Me.psgend_group_total.AllNodesSummary = True
        Me.psgend_group_total.Caption = "Sales Group"
        Me.psgend_group_total.FieldName = "psgend_group_total"
        Me.psgend_group_total.Format.FormatString = "n0"
        Me.psgend_group_total.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_group_total.Name = "psgend_group_total"
        Me.psgend_group_total.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_group_total.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_group_total.Visible = True
        Me.psgend_group_total.VisibleIndex = 3
        Me.psgend_group_total.Width = 80
        '
        'psgend_bonus_recruitment
        '
        Me.psgend_bonus_recruitment.AllNodesSummary = True
        Me.psgend_bonus_recruitment.Caption = "Recruitment Fee"
        Me.psgend_bonus_recruitment.FieldName = "psgend_bonus_recruitment"
        Me.psgend_bonus_recruitment.Format.FormatString = "n0"
        Me.psgend_bonus_recruitment.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_bonus_recruitment.Name = "psgend_bonus_recruitment"
        Me.psgend_bonus_recruitment.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_bonus_recruitment.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_bonus_recruitment.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_bonus_recruitment.Visible = True
        Me.psgend_bonus_recruitment.VisibleIndex = 5
        Me.psgend_bonus_recruitment.Width = 90
        '
        'psgend_sales_bonus
        '
        Me.psgend_sales_bonus.AllNodesSummary = True
        Me.psgend_sales_bonus.Caption = "Sales Fee"
        Me.psgend_sales_bonus.FieldName = "psgend_sales_bonus"
        Me.psgend_sales_bonus.Format.FormatString = "n0"
        Me.psgend_sales_bonus.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_sales_bonus.Name = "psgend_sales_bonus"
        Me.psgend_sales_bonus.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_sales_bonus.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_sales_bonus.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_sales_bonus.Visible = True
        Me.psgend_sales_bonus.VisibleIndex = 6
        Me.psgend_sales_bonus.Width = 69
        '
        'psgend_group_bonus
        '
        Me.psgend_group_bonus.AllNodesSummary = True
        Me.psgend_group_bonus.Caption = "Group Fee"
        Me.psgend_group_bonus.FieldName = "psgend_group_bonus"
        Me.psgend_group_bonus.Format.FormatString = "n0"
        Me.psgend_group_bonus.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_group_bonus.Name = "psgend_group_bonus"
        Me.psgend_group_bonus.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_group_bonus.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_group_bonus.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_group_bonus.Visible = True
        Me.psgend_group_bonus.VisibleIndex = 7
        Me.psgend_group_bonus.Width = 72
        '
        'psgend_thp_total
        '
        Me.psgend_thp_total.AllNodesSummary = True
        Me.psgend_thp_total.Caption = "Total Fee"
        Me.psgend_thp_total.FieldName = "psgend_thp_total"
        Me.psgend_thp_total.Format.FormatString = "n0"
        Me.psgend_thp_total.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_thp_total.Name = "psgend_thp_total"
        Me.psgend_thp_total.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_thp_total.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_thp_total.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_thp_total.Visible = True
        Me.psgend_thp_total.VisibleIndex = 8
        Me.psgend_thp_total.Width = 86
        '
        'psgend_sales_amount_recruitment
        '
        Me.psgend_sales_amount_recruitment.AllNodesSummary = True
        Me.psgend_sales_amount_recruitment.Caption = "Amount Sales Recruitment"
        Me.psgend_sales_amount_recruitment.FieldName = "psgend_sales_amount_recruitment"
        Me.psgend_sales_amount_recruitment.Format.FormatString = "n0"
        Me.psgend_sales_amount_recruitment.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_sales_amount_recruitment.Name = "psgend_sales_amount_recruitment"
        Me.psgend_sales_amount_recruitment.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_sales_amount_recruitment.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_sales_amount_recruitment.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_sales_amount_recruitment.Visible = True
        Me.psgend_sales_amount_recruitment.VisibleIndex = 4
        Me.psgend_sales_amount_recruitment.Width = 140
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.Location = New System.Drawing.Point(466, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(1778, 64)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "PERSONAL SELLING REPORT"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabelPeriode
        '
        Me.XrLabelPeriode.Dpi = 254.0!
        Me.XrLabelPeriode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabelPeriode.Location = New System.Drawing.Point(466, 64)
        Me.XrLabelPeriode.Name = "XrLabelPeriode"
        Me.XrLabelPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabelPeriode.Size = New System.Drawing.Size(1778, 64)
        Me.XrLabelPeriode.StylePriority.UseFont = False
        Me.XrLabelPeriode.StylePriority.UseTextAlignment = False
        Me.XrLabelPeriode.Text = "PERIODE"
        Me.XrLabelPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 77
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabelPeriode})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 183
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XRPSReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents ptnr_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_sales_amount As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_group_total As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_bonus_recruitment As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_sales_bonus As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_group_bonus As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_thp_total As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLabelPeriode As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents lvl_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_sales_amount_recruitment As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
