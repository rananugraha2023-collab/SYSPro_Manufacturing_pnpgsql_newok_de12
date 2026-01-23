<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XR_sub_routing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XR_sub_routing))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell13 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell14 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell16 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PgSqlDataAdapter1 = New npgsql.nPgSqlDataAdapter
        Me.pgSqlSelectCommand1 = New npgsql.nPgSqlCommand
        Me.FormattingRule1 = New DevExpress.XtraReports.UI.FormattingRule
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel6, Me.XrLabel11, Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4})
        Me.Detail.Height = 17
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.rod_op", "")})
        Me.XrLabel6.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.Location = New System.Drawing.Point(33, 0)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(33, 17)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel11
        '
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.wc_desc", "")})
        Me.XrLabel11.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel11.Location = New System.Drawing.Point(67, 0)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(233, 17)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = "XrLabel11"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.wodr_seq", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(33, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.wo_qty_comp", "")})
        Me.XrLabel2.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(500, 0)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel2.Visible = False
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.wodr_qty_in", "")})
        Me.XrLabel3.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.FormattingRules.Add(Me.FormattingRule1)
        Me.XrLabel3.Location = New System.Drawing.Point(300, 0)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "ro_mstr.wo_qty_rjc", "")})
        Me.XrLabel4.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(367, 0)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "XrLabel4"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrLabel4.Visible = False
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.PageHeader.Height = 35
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
                    Or DevExpress.XtraPrinting.BorderSide.Right) _
                    Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable1.Location = New System.Drawing.Point(0, 17)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(650, 18)
        Me.XrTable1.StylePriority.UseBorders = False
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell4, Me.XrTableCell3, Me.XrTableCell7, Me.XrTableCell6, Me.XrTableCell8, Me.XrTableCell9, Me.XrTableCell10})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "Line"
        Me.XrTableCell1.Weight = 0.15384615384615385
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "Proc"
        Me.XrTableCell4.Weight = 0.15384615384615385
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Text = "Workstation"
        Me.XrTableCell3.Weight = 1.083076923076923
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "Qty Order"
        Me.XrTableCell7.Weight = 0.31
        '
        'XrTableCell6
        '
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Text = "Reject"
        Me.XrTableCell6.Weight = 0.30730769230769228
        '
        'XrTableCell8
        '
        Me.XrTableCell8.Name = "XrTableCell8"
        Me.XrTableCell8.Text = "Split"
        Me.XrTableCell8.Weight = 0.3026923076923077
        '
        'XrTableCell9
        '
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.Text = "Qty Comp"
        Me.XrTableCell9.Weight = 0.30999999999999994
        '
        'XrTableCell10
        '
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.Text = "TTD"
        Me.XrTableCell10.Weight = 0.37923076923076926
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.PageFooter.Height = 46
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable2
        '
        Me.XrTable2.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
                    Or DevExpress.XtraPrinting.BorderSide.Right) _
                    Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable2.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTable2.Location = New System.Drawing.Point(0, 0)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(650, 18)
        Me.XrTable2.StylePriority.UseBorders = False
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell13, Me.XrTableCell14, Me.XrTableCell15, Me.XrTableCell16, Me.XrTableCell17, Me.XrTableCell18})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell13
        '
        Me.XrTableCell13.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell13.Name = "XrTableCell13"
        Me.XrTableCell13.StylePriority.UseFont = False
        Me.XrTableCell13.StylePriority.UseTextAlignment = False
        Me.XrTableCell13.Text = "Total"
        Me.XrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell13.Weight = 1.3907692307692305
        '
        'XrTableCell14
        '
        Me.XrTableCell14.Name = "XrTableCell14"
        Me.XrTableCell14.Weight = 0.31
        '
        'XrTableCell15
        '
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.Weight = 0.30730769230769228
        '
        'XrTableCell16
        '
        Me.XrTableCell16.Name = "XrTableCell16"
        Me.XrTableCell16.Weight = 0.3026923076923077
        '
        'XrTableCell17
        '
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.Weight = 0.30999999999999994
        '
        'XrTableCell18
        '
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.Weight = 0.37923076923076926
        '
        'PgSqlDataAdapter1
        '
        Me.PgSqlDataAdapter1.SelectCommand = Me.pgSqlSelectCommand1
        Me.PgSqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "ro_mstr", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ro_oid", "ro_oid"), New System.Data.Common.DataColumnMapping("ro_dom_id", "ro_dom_id"), New System.Data.Common.DataColumnMapping("ro_en_id", "ro_en_id"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("ro_add_by", "ro_add_by"), New System.Data.Common.DataColumnMapping("ro_add_date", "ro_add_date"), New System.Data.Common.DataColumnMapping("ro_upd_by", "ro_upd_by"), New System.Data.Common.DataColumnMapping("ro_upd_date", "ro_upd_date"), New System.Data.Common.DataColumnMapping("ro_id", "ro_id"), New System.Data.Common.DataColumnMapping("ro_code", "ro_code"), New System.Data.Common.DataColumnMapping("ro_desc", "ro_desc"), New System.Data.Common.DataColumnMapping("ro_active", "ro_active"), New System.Data.Common.DataColumnMapping("ro_dt", "ro_dt"), New System.Data.Common.DataColumnMapping("ro_cs_id", "ro_cs_id"), New System.Data.Common.DataColumnMapping("cs_name", "cs_name"), New System.Data.Common.DataColumnMapping("ro_pt_id", "ro_pt_id"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("ro_yield", "ro_yield"), New System.Data.Common.DataColumnMapping("ro_total", "ro_total"), New System.Data.Common.DataColumnMapping("ro_is_default", "ro_is_default"), New System.Data.Common.DataColumnMapping("rod_oid", "rod_oid"), New System.Data.Common.DataColumnMapping("rod_ro_oid", "rod_ro_oid"), New System.Data.Common.DataColumnMapping("rod_add_by", "rod_add_by"), New System.Data.Common.DataColumnMapping("rod_add_date", "rod_add_date"), New System.Data.Common.DataColumnMapping("rod_upd_by", "rod_upd_by"), New System.Data.Common.DataColumnMapping("rod_upd_date", "rod_upd_date"), New System.Data.Common.DataColumnMapping("rod_op", "rod_op"), New System.Data.Common.DataColumnMapping("op_name", "op_name"), New System.Data.Common.DataColumnMapping("rod_start_date", "rod_start_date"), New System.Data.Common.DataColumnMapping("rod_end_date", "rod_end_date"), New System.Data.Common.DataColumnMapping("rod_wc_id", "rod_wc_id"), New System.Data.Common.DataColumnMapping("rod_desc", "rod_desc"), New System.Data.Common.DataColumnMapping("rod_mch_op", "rod_mch_op"), New System.Data.Common.DataColumnMapping("rod_tran_qty", "rod_tran_qty"), New System.Data.Common.DataColumnMapping("rod_queue", "rod_queue"), New System.Data.Common.DataColumnMapping("rod_wait", "rod_wait"), New System.Data.Common.DataColumnMapping("rod_move", "rod_move"), New System.Data.Common.DataColumnMapping("rod_run", "rod_run"), New System.Data.Common.DataColumnMapping("rod_setup", "rod_setup"), New System.Data.Common.DataColumnMapping("rod_yield_pct", "rod_yield_pct"), New System.Data.Common.DataColumnMapping("rod_milestone", "rod_milestone"), New System.Data.Common.DataColumnMapping("rod_sub_lead", "rod_sub_lead"), New System.Data.Common.DataColumnMapping("rod_setup_men", "rod_setup_men"), New System.Data.Common.DataColumnMapping("rod_men_mch", "rod_men_mch"), New System.Data.Common.DataColumnMapping("rod_tool_code", "rod_tool_code"), New System.Data.Common.DataColumnMapping("rod_ptnr_id", "rod_ptnr_id"), New System.Data.Common.DataColumnMapping("rod_sub_cost", "rod_sub_cost"), New System.Data.Common.DataColumnMapping("rod_dt", "rod_dt"), New System.Data.Common.DataColumnMapping("rod_lbr_ac_id", "rod_lbr_ac_id"), New System.Data.Common.DataColumnMapping("rod_lbr_amount", "rod_lbr_amount"), New System.Data.Common.DataColumnMapping("rod_bdn_ac_id", "rod_bdn_ac_id"), New System.Data.Common.DataColumnMapping("rod_bdn_amount", "rod_bdn_amount"), New System.Data.Common.DataColumnMapping("rod_sbc_ac_id", "rod_sbc_ac_id"), New System.Data.Common.DataColumnMapping("rod_sbc_amount", "rod_sbc_amount"), New System.Data.Common.DataColumnMapping("wodr_seq", "wodr_seq"), New System.Data.Common.DataColumnMapping("wc_desc", "wc_desc"), New System.Data.Common.DataColumnMapping("code_name", "code_name"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ac_code_lbr", "ac_code_lbr"), New System.Data.Common.DataColumnMapping("ac_name_lbr", "ac_name_lbr"), New System.Data.Common.DataColumnMapping("ac_code_bdn", "ac_code_bdn"), New System.Data.Common.DataColumnMapping("ac_name_bdn", "ac_name_bdn"), New System.Data.Common.DataColumnMapping("ac_code_sbc", "ac_code_sbc"), New System.Data.Common.DataColumnMapping("ac_name_sbc", "ac_name_sbc"), New System.Data.Common.DataColumnMapping("wo_qty", "wo_qty"), New System.Data.Common.DataColumnMapping("wo_ref_rework", "wo_ref_rework"), New System.Data.Common.DataColumnMapping("wo_bom_id", "wo_bom_id"), New System.Data.Common.DataColumnMapping("wodr_qty_in", "wodr_qty_in"), New System.Data.Common.DataColumnMapping("wo_qty_comp", "wo_qty_comp"), New System.Data.Common.DataColumnMapping("wo_qty_rjc", "wo_qty_rjc")})})
        '
        'pgSqlSelectCommand1
        '
        Me.pgSqlSelectCommand1.CommandText = resources.GetString("pgSqlSelectCommand1.CommandText")
        'Me.pgSqlSelectCommand1.Name = "pgSqlSelectCommand1"
        'Me.pgSqlSelectCommand1.Owner = Me
        '
        'FormattingRule1
        '
        Me.FormattingRule1.Condition = "wodr_qty_in=0"
        '
        '
        '
        Me.FormattingRule1.Formatting.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.FormattingRule1.Name = "FormattingRule1"
        '
        'XR_sub_routing
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.DataAdapter = Me.PgSqlDataAdapter1
        Me.DataMember = "ro_mstr"
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {Me.FormattingRule1})
        Me.Version = "9.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents PgSqlDataAdapter1 As npgsql.nPgSqlDataAdapter
    Friend WithEvents pgSqlSelectCommand1 As npgsql.nPgSqlCommand
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents FormattingRule1 As DevExpress.XtraReports.UI.FormattingRule
    'Friend WithEvents DS_SUB_ruting1 As syspro_ver2.DS_SUB_ruting
End Class
