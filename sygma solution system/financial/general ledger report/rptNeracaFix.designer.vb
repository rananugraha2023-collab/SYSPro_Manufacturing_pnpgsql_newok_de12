<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptNeracaFix
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptNeracaFix))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.Level4 = New DevExpress.XtraReports.UI.FormattingRule
        'Me.Ds_neraca1 = New sygma_solution_system.ds_neraca
        Me.Level3 = New DevExpress.XtraReports.UI.FormattingRule
        Me.Level2 = New DevExpress.XtraReports.UI.FormattingRule
        Me.Level1 = New DevExpress.XtraReports.UI.FormattingRule
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.LblStatusPosting = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.LblPeriode = New DevExpress.XtraReports.UI.XRLabel
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.fac_code = New DevExpress.XtraReports.UI.CalculatedField
        'CType(Me.Ds_neraca1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel3, Me.XrLine1})
        Me.Detail.Dpi = 254.0!
        Me.Detail.FormattingRules.Add(Me.Level4)
        Me.Detail.FormattingRules.Add(Me.Level3)
        Me.Detail.FormattingRules.Add(Me.Level2)
        Me.Detail.FormattingRules.Add(Me.Level1)
        Me.Detail.Height = 59
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("ac_code_sort", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.fac_code", "")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.FormattingRules.Add(Me.Level4)
        Me.XrLabel1.FormattingRules.Add(Me.Level3)
        Me.XrLabel1.FormattingRules.Add(Me.Level2)
        Me.XrLabel1.FormattingRules.Add(Me.Level1)
        Me.XrLabel1.Location = New System.Drawing.Point(11, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(1117, 48)
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ac_value", "{0:n2}")})
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Location = New System.Drawing.Point(1286, 0)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(561, 48)
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.ForeColor = System.Drawing.Color.DarkGray
        Me.XrLine1.Location = New System.Drawing.Point(5, 48)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(1852, 11)
        Me.XrLine1.StylePriority.UseForeColor = False
        '
        'Level4
        '
        Me.Level4.Condition = "[ac_level]=4"
        'Me.Level4.DataSource = Me.Ds_neraca1
        '
        '
        '
        Me.Level4.Formatting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Level4.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Level4.Formatting.Padding = New DevExpress.XtraPrinting.PaddingInfo(75, 0, 0, 0, 254.0!)
        Me.Level4.Name = "Level4"
        '
        'Ds_neraca1
        '
        'Me.Ds_neraca1.DataSetName = "ds_neraca"
        'Me.Ds_neraca1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Level3
        '
        Me.Level3.Condition = "[ac_level]=3"
        'Me.Level3.DataSource = Me.Ds_neraca1
        '
        '
        '
        Me.Level3.Formatting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Level3.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Level3.Formatting.Padding = New DevExpress.XtraPrinting.PaddingInfo(50, 0, 0, 0, 254.0!)
        Me.Level3.Name = "Level3"
        '
        'Level2
        '
        Me.Level2.Condition = "[ac_level]=2"
        'Me.Level2.DataSource = Me.Ds_neraca1
        '
        '
        '
        Me.Level2.Formatting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Level2.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Level2.Formatting.Padding = New DevExpress.XtraPrinting.PaddingInfo(25, 0, 0, 0, 254.0!)
        Me.Level2.Name = "Level2"
        '
        'Level1
        '
        Me.Level1.Condition = "[ac_level]=1"
        'Me.Level1.DataSource = Me.Ds_neraca1
        '
        '
        '
        Me.Level1.Formatting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Level1.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Level1.Name = "Level1"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel20, Me.LblStatusPosting, Me.XrLabel4, Me.LblPeriode})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 164
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(328, 58)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(1249, 53)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "BALANCE SHEET REPORT"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LblStatusPosting
        '
        Me.LblStatusPosting.Dpi = 254.0!
        Me.LblStatusPosting.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblStatusPosting.ForeColor = System.Drawing.Color.Red
        Me.LblStatusPosting.Location = New System.Drawing.Point(1630, 26)
        Me.LblStatusPosting.Name = "LblStatusPosting"
        Me.LblStatusPosting.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LblStatusPosting.Size = New System.Drawing.Size(265, 48)
        Me.LblStatusPosting.StylePriority.UseFont = False
        Me.LblStatusPosting.StylePriority.UseForeColor = False
        Me.LblStatusPosting.StylePriority.UseTextAlignment = False
        Me.LblStatusPosting.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(328, 5)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(1249, 53)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "PT. SYGMA EXAMEDIA ARKANLEEMA"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'LblPeriode
        '
        Me.LblPeriode.Dpi = 254.0!
        Me.LblPeriode.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPeriode.Location = New System.Drawing.Point(328, 111)
        Me.LblPeriode.Name = "LblPeriode"
        Me.LblPeriode.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.LblPeriode.Size = New System.Drawing.Size(1249, 53)
        Me.LblPeriode.StylePriority.UseFont = False
        Me.LblPeriode.StylePriority.UseTextAlignment = False
        Me.LblPeriode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrLabel5})
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 48
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 254.0!
        Me.XrPageInfo1.Format = "{0:dddd, dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(1270, 0)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(577, 42)
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Location = New System.Drawing.Point(1117, 0)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(148, 48)
        Me.XrLabel5.Text = "Print on : "
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ac_id", "ac_id"), New System.Data.Common.DataColumnMapping("ac_code", "ac_code"), New System.Data.Common.DataColumnMapping("ac_code_hirarki", "ac_code_hirarki"), New System.Data.Common.DataColumnMapping("ac_name", "ac_name"), New System.Data.Common.DataColumnMapping("ac_desc", "ac_desc"), New System.Data.Common.DataColumnMapping("ac_parent", "ac_parent"), New System.Data.Common.DataColumnMapping("ac_type", "ac_type"), New System.Data.Common.DataColumnMapping("ac_is_sumlevel", "ac_is_sumlevel"), New System.Data.Common.DataColumnMapping("ac_sign", "ac_sign"), New System.Data.Common.DataColumnMapping("ac_active", "ac_active"), New System.Data.Common.DataColumnMapping("ac_cu_id", "ac_cu_id"), New System.Data.Common.DataColumnMapping("ac_cash_flow", "ac_cash_flow"), New System.Data.Common.DataColumnMapping("ac_level", "ac_level"), New System.Data.Common.DataColumnMapping("ac_value", "ac_value")})})
        '
        'fac_code
        '
        Me.fac_code.DataMember = "Table"
        'Me.fac_code.DataSource = Me.Ds_neraca1
        Me.fac_code.Expression = "Iif([ac_level]<>4,[ac_name]  ,Concat( [ac_code] ,' ', [ac_name] ) )"
        Me.fac_code.Name = "fac_code"
        '
        'rptNeracaFix
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.fac_code})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.Ds_neraca1
        Me.Dpi = 254.0!
        Me.DrawGrid = False
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.Level4, Me.Level3, Me.Level2, Me.Level1})
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Margins = New System.Drawing.Printing.Margins(99, 99, 150, 150)
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "9.2"
        'CType(Me.Ds_neraca1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    'Friend WithEvents Ds_neraca1 As sygma_solution_system.ds_neraca
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents Level4 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Level3 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Level2 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents Level1 As DevExpress.XtraReports.UI.FormattingRule
    Friend WithEvents LblStatusPosting As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents LblPeriode As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents Ds_neraca1 As sygma_solution_system.ds_neraca
    Friend WithEvents fac_code As DevExpress.XtraReports.UI.CalculatedField
End Class
