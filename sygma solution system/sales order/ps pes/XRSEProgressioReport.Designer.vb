<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSEProgressioReport
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
        Me.segend_nama = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_entity = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_level = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.ptnr_code = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_su_sales = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_komisi = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_su_group = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_add_income = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_coaching_income = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_total_income = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_komisi_bulanan = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.segend_komisi_telah_dibayar = New DevExpress.XtraTreeList.Columns.TreeListColumn
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
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.segend_nama, Me.segend_entity, Me.segend_level, Me.ptnr_code, Me.segend_su_sales, Me.segend_komisi, Me.segend_su_group, Me.segend_add_income, Me.segend_coaching_income, Me.segend_total_income, Me.segend_komisi_bulanan, Me.segend_komisi_telah_dibayar})
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "segend_ptnr_id"
        Me.TreeList1.Location = New System.Drawing.Point(0, 0)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.PopulateServiceColumns = True
        Me.TreeList1.OptionsPrint.AutoWidth = False
        Me.TreeList1.OptionsPrint.PrintAllNodes = True
        Me.TreeList1.OptionsPrint.UsePrintStyles = True
        Me.TreeList1.OptionsView.AutoWidth = False
        Me.TreeList1.ParentFieldName = "segend_parent"
        Me.TreeList1.Size = New System.Drawing.Size(6, 6)
        Me.TreeList1.TabIndex = 0
        '
        'segend_nama
        '
        Me.segend_nama.Caption = "Name"
        Me.segend_nama.FieldName = "segend_nama"
        Me.segend_nama.Name = "segend_nama"
        Me.segend_nama.Visible = True
        Me.segend_nama.VisibleIndex = 0
        Me.segend_nama.Width = 242
        '
        'segend_entity
        '
        Me.segend_entity.Caption = "Entity"
        Me.segend_entity.FieldName = "segend_entity"
        Me.segend_entity.Name = "segend_entity"
        Me.segend_entity.Visible = True
        Me.segend_entity.VisibleIndex = 1
        Me.segend_entity.Width = 100
        '
        'segend_level
        '
        Me.segend_level.Caption = "NPWP"
        Me.segend_level.FieldName = "segend_npwp"
        Me.segend_level.Name = "segend_level"
        Me.segend_level.Visible = True
        Me.segend_level.VisibleIndex = 3
        Me.segend_level.Width = 117
        '
        'ptnr_code
        '
        Me.ptnr_code.Caption = "Start Periode"
        Me.ptnr_code.FieldName = "segend_ptnr_start_periode"
        Me.ptnr_code.Name = "ptnr_code"
        Me.ptnr_code.Visible = True
        Me.ptnr_code.VisibleIndex = 2
        Me.ptnr_code.Width = 82
        '
        'segend_su_sales
        '
        Me.segend_su_sales.AllNodesSummary = True
        Me.segend_su_sales.Caption = "Point Total"
        Me.segend_su_sales.FieldName = "segend_poin_total"
        Me.segend_su_sales.Format.FormatString = "n2"
        Me.segend_su_sales.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_su_sales.Name = "segend_su_sales"
        Me.segend_su_sales.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_su_sales.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_su_sales.Visible = True
        Me.segend_su_sales.VisibleIndex = 4
        Me.segend_su_sales.Width = 64
        '
        'segend_komisi
        '
        Me.segend_komisi.AllNodesSummary = True
        Me.segend_komisi.Caption = "Multiplier"
        Me.segend_komisi.FieldName = "segend_poin_pengali"
        Me.segend_komisi.Format.FormatString = "n0"
        Me.segend_komisi.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_komisi.Name = "segend_komisi"
        Me.segend_komisi.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_komisi.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_komisi.Visible = True
        Me.segend_komisi.VisibleIndex = 5
        Me.segend_komisi.Width = 66
        '
        'segend_su_group
        '
        Me.segend_su_group.AllNodesSummary = True
        Me.segend_su_group.Caption = "Gross Commission"
        Me.segend_su_group.FieldName = "segend_komisi_bruto"
        Me.segend_su_group.Format.FormatString = "n2"
        Me.segend_su_group.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_su_group.Name = "segend_su_group"
        Me.segend_su_group.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_su_group.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_su_group.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_su_group.Visible = True
        Me.segend_su_group.VisibleIndex = 6
        Me.segend_su_group.Width = 103
        '
        'segend_add_income
        '
        Me.segend_add_income.AllNodesSummary = True
        Me.segend_add_income.Caption = "Point Recruiter"
        Me.segend_add_income.FieldName = "segend_point_rekruter"
        Me.segend_add_income.Format.FormatString = "n0"
        Me.segend_add_income.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_add_income.Name = "segend_add_income"
        Me.segend_add_income.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_add_income.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_add_income.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_add_income.Visible = True
        Me.segend_add_income.VisibleIndex = 7
        Me.segend_add_income.Width = 99
        '
        'segend_coaching_income
        '
        Me.segend_coaching_income.AllNodesSummary = True
        Me.segend_coaching_income.Caption = "Multiplier Recruiter"
        Me.segend_coaching_income.FieldName = "segend_point_pengali_rekrut"
        Me.segend_coaching_income.Format.FormatString = "n0"
        Me.segend_coaching_income.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_coaching_income.Name = "segend_coaching_income"
        Me.segend_coaching_income.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_coaching_income.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_coaching_income.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_coaching_income.Visible = True
        Me.segend_coaching_income.VisibleIndex = 8
        Me.segend_coaching_income.Width = 109
        '
        'segend_total_income
        '
        Me.segend_total_income.AllNodesSummary = True
        Me.segend_total_income.Caption = "Total Commission"
        Me.segend_total_income.FieldName = "segend_komisi_total"
        Me.segend_total_income.Format.FormatString = "n0"
        Me.segend_total_income.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_total_income.Name = "segend_total_income"
        Me.segend_total_income.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_total_income.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_total_income.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_total_income.Visible = True
        Me.segend_total_income.VisibleIndex = 9
        Me.segend_total_income.Width = 92
        '
        'segend_komisi_bulanan
        '
        Me.segend_komisi_bulanan.Caption = "PPH21"
        Me.segend_komisi_bulanan.FieldName = "segend_pph_21"
        Me.segend_komisi_bulanan.Format.FormatString = "n0"
        Me.segend_komisi_bulanan.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_komisi_bulanan.Name = "segend_komisi_bulanan"
        Me.segend_komisi_bulanan.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_komisi_bulanan.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_komisi_bulanan.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_komisi_bulanan.Visible = True
        Me.segend_komisi_bulanan.VisibleIndex = 10
        Me.segend_komisi_bulanan.Width = 67
        '
        'segend_komisi_telah_dibayar
        '
        Me.segend_komisi_telah_dibayar.Caption = "Nett Commission"
        Me.segend_komisi_telah_dibayar.FieldName = "segend_komisi_netto"
        Me.segend_komisi_telah_dibayar.Format.FormatString = "n0"
        Me.segend_komisi_telah_dibayar.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.segend_komisi_telah_dibayar.Name = "segend_komisi_telah_dibayar"
        Me.segend_komisi_telah_dibayar.RowFooterSummaryStrFormat = "{0:n0}"
        Me.segend_komisi_telah_dibayar.SummaryFooter = DevExpress.XtraTreeList.SummaryItemType.Sum
        Me.segend_komisi_telah_dibayar.SummaryFooterStrFormat = "{0:n0}"
        Me.segend_komisi_telah_dibayar.Visible = True
        Me.segend_komisi_telah_dibayar.VisibleIndex = 11
        Me.segend_komisi_telah_dibayar.Width = 103
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
        Me.XrLabel1.Text = "PERSONAL SELLING PROGRESSIO REPORT"
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
        'XRSEProgressioReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter, Me.ReportHeader})
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.PageHeight = 2159
        Me.PageWidth = 3556
        Me.PaperKind = System.Drawing.Printing.PaperKind.Legal
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents WinControlContainer1 As DevExpress.XtraReports.UI.WinControlContainer
    Friend WithEvents segend_nama As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_su_sales As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_komisi As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_su_group As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_add_income As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_coaching_income As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_total_income As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents XrLabelPeriode As DevExpress.XtraReports.UI.XRLabel
    Public WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents segend_level As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_entity As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents ptnr_code As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_komisi_bulanan As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents segend_komisi_telah_dibayar As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
