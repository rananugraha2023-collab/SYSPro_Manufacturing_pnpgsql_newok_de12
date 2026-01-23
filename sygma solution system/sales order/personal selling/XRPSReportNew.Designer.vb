<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRPSReportNew
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
        Me.psgend_ps_vol = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_pt_vol = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_network_child = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_child_level = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_sales_amount = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_commision = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_network_bonus = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.psgend_thp_total = New DevExpress.XtraTreeList.Columns.TreeListColumn
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
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.ptnr_name, Me.lvl_name, Me.psgend_ps_vol, Me.psgend_pt_vol, Me.psgend_network_child, Me.psgend_child_level, Me.psgend_sales_amount, Me.psgend_commision, Me.psgend_network_bonus, Me.psgend_thp_total})
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
        'psgend_ps_vol
        '
        Me.psgend_ps_vol.AllNodesSummary = True
        Me.psgend_ps_vol.Caption = "PS Vol Amount"
        Me.psgend_ps_vol.FieldName = "psgend_ps_vol"
        Me.psgend_ps_vol.Format.FormatString = "n0"
        Me.psgend_ps_vol.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_ps_vol.Name = "psgend_ps_vol"
        Me.psgend_ps_vol.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_ps_vol.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_ps_vol.Visible = True
        Me.psgend_ps_vol.VisibleIndex = 2
        Me.psgend_ps_vol.Width = 89
        '
        'psgend_pt_vol
        '
        Me.psgend_pt_vol.AllNodesSummary = True
        Me.psgend_pt_vol.Caption = "PT Vol Amount"
        Me.psgend_pt_vol.FieldName = "psgend_pt_vol"
        Me.psgend_pt_vol.Format.FormatString = "n0"
        Me.psgend_pt_vol.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_pt_vol.Name = "psgend_pt_vol"
        Me.psgend_pt_vol.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_pt_vol.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_pt_vol.Visible = True
        Me.psgend_pt_vol.VisibleIndex = 3
        Me.psgend_pt_vol.Width = 80
        '
        'psgend_network_child
        '
        Me.psgend_network_child.AllNodesSummary = True
        Me.psgend_network_child.Caption = "Child Count"
        Me.psgend_network_child.FieldName = "psgend_network_child"
        Me.psgend_network_child.Format.FormatString = "n0"
        Me.psgend_network_child.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_network_child.Name = "psgend_network_child"
        Me.psgend_network_child.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_network_child.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_network_child.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_network_child.Visible = True
        Me.psgend_network_child.VisibleIndex = 4
        Me.psgend_network_child.Width = 90
        '
        'psgend_child_level
        '
        Me.psgend_child_level.AllNodesSummary = True
        Me.psgend_child_level.Caption = "Child Level"
        Me.psgend_child_level.FieldName = "psgend_child_level"
        Me.psgend_child_level.Name = "psgend_child_level"
        Me.psgend_child_level.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_child_level.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_child_level.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_child_level.Visible = True
        Me.psgend_child_level.VisibleIndex = 5
        Me.psgend_child_level.Width = 69
        '
        'psgend_sales_amount
        '
        Me.psgend_sales_amount.AllNodesSummary = True
        Me.psgend_sales_amount.Caption = "Sales Amount"
        Me.psgend_sales_amount.FieldName = "psgend_sales_amount"
        Me.psgend_sales_amount.Format.FormatString = "n0"
        Me.psgend_sales_amount.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_sales_amount.Name = "psgend_sales_amount"
        Me.psgend_sales_amount.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_sales_amount.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_sales_amount.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_sales_amount.Visible = True
        Me.psgend_sales_amount.VisibleIndex = 6
        Me.psgend_sales_amount.Width = 72
        '
        'psgend_commision
        '
        Me.psgend_commision.AllNodesSummary = True
        Me.psgend_commision.Caption = "Commision"
        Me.psgend_commision.FieldName = "psgend_commision"
        Me.psgend_commision.Format.FormatString = "n0"
        Me.psgend_commision.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_commision.Name = "psgend_commision"
        Me.psgend_commision.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_commision.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_commision.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_commision.Visible = True
        Me.psgend_commision.VisibleIndex = 7
        Me.psgend_commision.Width = 86
        '
        'psgend_network_bonus
        '
        Me.psgend_network_bonus.AllNodesSummary = True
        Me.psgend_network_bonus.Caption = "Network Bonus"
        Me.psgend_network_bonus.FieldName = "psgend_network_bonus"
        Me.psgend_network_bonus.Format.FormatString = "n0"
        Me.psgend_network_bonus.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psgend_network_bonus.Name = "psgend_network_bonus"
        Me.psgend_network_bonus.RowFooterSummaryStrFormat = "{0:n0}"
        Me.psgend_network_bonus.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.psgend_network_bonus.SummaryFooterStrFormat = "{0:n0}"
        Me.psgend_network_bonus.Visible = True
        Me.psgend_network_bonus.VisibleIndex = 8
        Me.psgend_network_bonus.Width = 140
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
        Me.psgend_thp_total.VisibleIndex = 9
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
        'XRPSReportNew
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
    Friend WithEvents psgend_ps_vol As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_pt_vol As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_network_child As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_child_level As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_sales_amount As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_commision As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLabelPeriode As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents lvl_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_network_bonus As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psgend_thp_total As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
