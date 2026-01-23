<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptProfitLossReport
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptProfitLossReport))
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Dim XrSummary3 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.hide_rows_empty = New DevExpress.XtraReports.UI.FormattingRule
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        'Me.DsProfitLoss1 = New sygma_solution_system.DSProfitLoss
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.show_lr_lain2 = New DevExpress.XtraReports.UI.FormattingRule
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.hide_lr_lain2 = New DevExpress.XtraReports.UI.FormattingRule
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.LblPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabelTitle = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.LblStatusPosting = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand
        'CType(Me.DsProfitLoss1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.FormattingRules.Add(Me.hide_rows_empty)
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'hide_rows_empty
        '
        Me.hide_rows_empty.Condition = "[pls_item]   ==  ''"
        '
        '
        '
        Me.hide_rows_empty.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        Me.hide_rows_empty.Name = "hide_rows_empty"
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pls_item", "")})
        Me.XrLabel2.Location = New System.Drawing.Point(4, 0)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(338, 21)
        Me.XrLabel2.Text = "XrLabel2"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.v_nilai", "")})
        Me.XrLabel3.Location = New System.Drawing.Point(463, 0)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(133, 21)
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n2}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel3.Summary = XrSummary1
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(0, 21)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(604, 5)
        '
        'PageHeader
        '
        Me.PageHeader.Height = 0
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrLabel8})
        Me.PageFooter.Height = 30
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(421, 4)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(227, 17)
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel8
        '
        Me.XrLabel8.Location = New System.Drawing.Point(358, 4)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(58, 19)
        Me.XrLabel8.Text = "Print on : "
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = resources.GetString("OdbcConnection1.ConnectionString")
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("pl_oid", "pl_oid"), New System.Data.Common.DataColumnMapping("pl_footer", "pl_footer"), New System.Data.Common.DataColumnMapping("pl_sign", "pl_sign"), New System.Data.Common.DataColumnMapping("pl_number", "pl_number"), New System.Data.Common.DataColumnMapping("pls_oid", "pls_oid"), New System.Data.Common.DataColumnMapping("pls_item", "pls_item"), New System.Data.Common.DataColumnMapping("pls_number", "pls_number"), New System.Data.Common.DataColumnMapping("pla_ac_id", "pla_ac_id"), New System.Data.Common.DataColumnMapping("ac_code", "ac_code"), New System.Data.Common.DataColumnMapping("ac_name", "ac_name"), New System.Data.Common.DataColumnMapping("pla_ac_hirarki", "pla_ac_hirarki"), New System.Data.Common.DataColumnMapping("v_nilai", "v_nilai")})})
        '
        'DsProfitLoss1
        '
        'Me.DsProfitLoss1.DataSetName = "DSProfitLoss"
        'Me.DsProfitLoss1.EnforceConstraints = False
        '
        'GroupHeader1
        '
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("pl_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("pl_footer", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 0
        Me.GroupHeader1.Level = 1
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel5, Me.XrLabel4})
        Me.GroupFooter1.Height = 21
        Me.GroupFooter1.Level = 1
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pl_footer", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel1.Location = New System.Drawing.Point(100, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(208, 21)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrLabel5
        '
        Me.XrLabel5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.v_nilai", "")})
        Me.XrLabel5.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.FormattingRules.Add(Me.show_lr_lain2)
        Me.XrLabel5.Location = New System.Drawing.Point(467, 0)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(133, 21)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel5.Summary = XrSummary2
        Me.XrLabel5.Text = "XrLabel5"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel5.Visible = False
        '
        'show_lr_lain2
        '
        Me.show_lr_lain2.Condition = "[pl_sign]='G'"
        '
        '
        '
        Me.show_lr_lain2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.show_lr_lain2.Name = "show_lr_lain2"
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.v_nilai", "")})
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.FormattingRules.Add(Me.hide_lr_lain2)
        Me.XrLabel4.Location = New System.Drawing.Point(467, 0)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(133, 21)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        XrSummary3.FormatString = "{0:n}"
        XrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.RunningSum
        XrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel4.Summary = XrSummary3
        Me.XrLabel4.Text = "XrLabel4"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'hide_lr_lain2
        '
        Me.hide_lr_lain2.Condition = "[pl_sign]='G'"
        '
        '
        '
        Me.hide_lr_lain2.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.hide_lr_lain2.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[True]
        Me.hide_lr_lain2.Name = "hide_lr_lain2"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.LblPeriode, Me.XrLabelTitle, Me.XrLabel20, Me.LblStatusPosting})
        Me.ReportHeader.Height = 63
        Me.ReportHeader.Name = "ReportHeader"
        '
        'LblPeriode
        '
        Me.LblPeriode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPeriode.ForeColor = System.Drawing.Color.Black
        Me.LblPeriode.Location = New System.Drawing.Point(79, 42)
        Me.LblPeriode.Name = "LblPeriode"
        Me.LblPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LblPeriode.Size = New System.Drawing.Size(492, 21)
        Me.LblPeriode.StylePriority.UseFont = False
        Me.LblPeriode.StylePriority.UseForeColor = False
        Me.LblPeriode.StylePriority.UseTextAlignment = False
        Me.LblPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabelTitle
        '
        Me.XrLabelTitle.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabelTitle.ForeColor = System.Drawing.Color.Black
        Me.XrLabelTitle.Location = New System.Drawing.Point(79, 0)
        Me.XrLabelTitle.Name = "XrLabelTitle"
        Me.XrLabelTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabelTitle.Size = New System.Drawing.Size(492, 21)
        Me.XrLabelTitle.StylePriority.UseFont = False
        Me.XrLabelTitle.StylePriority.UseForeColor = False
        Me.XrLabelTitle.StylePriority.UseTextAlignment = False
        Me.XrLabelTitle.Text = "PT. SYGMA EXAMEDIA ARKANLEEMA"
        Me.XrLabelTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.ForeColor = System.Drawing.Color.Black
        Me.XrLabel20.Location = New System.Drawing.Point(79, 21)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(492, 21)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseForeColor = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "PROFIT AND LOSS REPORT"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LblStatusPosting
        '
        Me.LblStatusPosting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblStatusPosting.ForeColor = System.Drawing.Color.Red
        Me.LblStatusPosting.Location = New System.Drawing.Point(604, 0)
        Me.LblStatusPosting.Name = "LblStatusPosting"
        Me.LblStatusPosting.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.LblStatusPosting.Size = New System.Drawing.Size(104, 19)
        Me.LblStatusPosting.StylePriority.UseFont = False
        Me.LblStatusPosting.StylePriority.UseForeColor = False
        Me.LblStatusPosting.StylePriority.UseTextAlignment = False
        Me.LblStatusPosting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrLine1, Me.XrLabel2})
        Me.GroupHeader2.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("pls_number", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("pls_item", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader2.Height = 26
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'rptProfitLossReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupFooter1, Me.PageHeader, Me.PageFooter, Me.GroupHeader1, Me.ReportHeader, Me.GroupHeader2})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.DsProfitLoss1
        Me.DrawGrid = False
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.show_lr_lain2, Me.hide_lr_lain2, Me.hide_rows_empty})
        Me.GridSize = New System.Drawing.Size(4, 4)
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 100, 100)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Version = "9.2"
        'CType(Me.DsProfitLoss1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    ''Friend WithEvents DsProfitLoss1 As sygma_solution_system.DSProfitLoss
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents show_lr_lain2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents hide_lr_lain2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents hide_rows_empty As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents LblPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LblStatusPosting As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Public WithEvents XrLabelTitle As DevExpress.XtraReports.UI.XRLabel
End Class
