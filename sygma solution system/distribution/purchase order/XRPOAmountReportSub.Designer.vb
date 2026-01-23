<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRPOAmountReportSub
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRPOAmountReportSub))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell8 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell10 = New DevExpress.XtraReports.UI.XRTableCell
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell9 = New DevExpress.XtraReports.UI.XRTableCell
        'Me.DataTable1TableAdapter = New sygma_solution_system.ds_general_ledger_printTableAdapters.DataTable1TableAdapter
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        'Me.DspoAmountSub1 = New sygma_solution_system.DSPOAmountSub
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.DspoAmountSub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 59
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable2
        '
        Me.XrTable2.BorderColor = System.Drawing.Color.Silver
        Me.XrTable2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTable2.Dpi = 254.0!
        Me.XrTable2.Location = New System.Drawing.Point(64, 3)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(1820, 56)
        Me.XrTable2.StylePriority.UseBorderColor = False
        Me.XrTable2.StylePriority.UseBorders = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell8, Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell12, Me.XrTableCell6, Me.XrTableCell10})
        Me.XrTableRow2.Dpi = 254.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell8
        '
        Me.XrTableCell8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.po_code", "")})
        Me.XrTableCell8.Dpi = 254.0!
        Me.XrTableCell8.Name = "XrTableCell8"
        XrSummary1.FormatString = "{0:n0}"
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrTableCell8.Summary = XrSummary1
        Me.XrTableCell8.Text = "XrTableCell8"
        Me.XrTableCell8.Weight = 0.17032967032967034
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.po_code", "")})
        Me.XrTableCell4.Dpi = 254.0!
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 0.59395604395604384
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.po_date", "{0:dd/MM/yyyy}")})
        Me.XrTableCell5.Dpi = 254.0!
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell5.Weight = 0.38791208791208781
        '
        'XrTableCell12
        '
        Me.XrTableCell12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.ptnr_name", "")})
        Me.XrTableCell12.Dpi = 254.0!
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.Text = "XrTableCell12"
        Me.XrTableCell12.Weight = 0.83791208791208793
        '
        'XrTableCell6
        '
        Me.XrTableCell6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.pod_qty_cost", "{0:n0}")})
        Me.XrTableCell6.Dpi = 254.0!
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.StylePriority.UseTextAlignment = False
        Me.XrTableCell6.Text = "XrTableCell6"
        Me.XrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell6.Weight = 0.48846153846153856
        '
        'XrTableCell10
        '
        Me.XrTableCell10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.amount_receive", "{0:n0}")})
        Me.XrTableCell10.Dpi = 254.0!
        Me.XrTableCell10.Name = "XrTableCell10"
        Me.XrTableCell10.StylePriority.UseTextAlignment = False
        Me.XrTableCell10.Text = "XrTableCell10"
        Me.XrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell10.Weight = 0.52142857142857146
        '
        'PageFooter
        '
        Me.PageFooter.Dpi = 254.0!
        Me.PageFooter.Height = 0
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.PageHeader.Dpi = 254.0!
        Me.PageHeader.Height = 45
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom
        Me.XrTable1.BorderColor = System.Drawing.Color.Silver
        Me.XrTable1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTable1.Dpi = 254.0!
        Me.XrTable1.Location = New System.Drawing.Point(64, 3)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(1820, 42)
        Me.XrTable1.StylePriority.UseBorderColor = False
        Me.XrTable1.StylePriority.UseBorders = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell7, Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell11, Me.XrTableCell3, Me.XrTableCell9})
        Me.XrTableRow1.Dpi = 254.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell7
        '
        Me.XrTableCell7.CanGrow = False
        Me.XrTableCell7.Dpi = 254.0!
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Text = "NO"
        Me.XrTableCell7.Weight = 0.17032967032967034
        '
        'XrTableCell1
        '
        Me.XrTableCell1.CanGrow = False
        Me.XrTableCell1.Dpi = 254.0!
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Text = "PO"
        Me.XrTableCell1.Weight = 0.59395604395604384
        '
        'XrTableCell2
        '
        Me.XrTableCell2.CanGrow = False
        Me.XrTableCell2.Dpi = 254.0!
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.Text = "Date"
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell2.Weight = 0.38791208791208781
        '
        'XrTableCell11
        '
        Me.XrTableCell11.CanGrow = False
        Me.XrTableCell11.Dpi = 254.0!
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.Text = "Suplier"
        Me.XrTableCell11.Weight = 0.83791208791208793
        '
        'XrTableCell3
        '
        Me.XrTableCell3.CanGrow = False
        Me.XrTableCell3.Dpi = 254.0!
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "Amount"
        Me.XrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell3.Weight = 0.48846153846153856
        '
        'XrTableCell9
        '
        Me.XrTableCell9.CanGrow = False
        Me.XrTableCell9.Dpi = 254.0!
        Me.XrTableCell9.Name = "XrTableCell9"
        Me.XrTableCell9.StylePriority.UseTextAlignment = False
        Me.XrTableCell9.Text = "Receive Amount"
        Me.XrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell9.Weight = 0.52142857142857146
        '
        'DataTable1TableAdapter
        '
        'Me.DataTable1TableAdapter.ClearBeforeFill = True
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("en_desc_header", "en_desc_header"), New System.Data.Common.DataColumnMapping("po_oid", "po_oid"), New System.Data.Common.DataColumnMapping("po_dom_id", "po_dom_id"), New System.Data.Common.DataColumnMapping("po_en_id", "po_en_id"), New System.Data.Common.DataColumnMapping("po_upd_date", "po_upd_date"), New System.Data.Common.DataColumnMapping("po_upd_by", "po_upd_by"), New System.Data.Common.DataColumnMapping("po_add_date", "po_add_date"), New System.Data.Common.DataColumnMapping("po_add_by", "po_add_by"), New System.Data.Common.DataColumnMapping("po_code", "po_code"), New System.Data.Common.DataColumnMapping("po_ptnr_id", "po_ptnr_id"), New System.Data.Common.DataColumnMapping("po_cmaddr_id", "po_cmaddr_id"), New System.Data.Common.DataColumnMapping("po_date", "po_date"), New System.Data.Common.DataColumnMapping("po_need_date", "po_need_date"), New System.Data.Common.DataColumnMapping("po_due_date", "po_due_date"), New System.Data.Common.DataColumnMapping("po_rmks", "po_rmks"), New System.Data.Common.DataColumnMapping("po_sb_id", "po_sb_id"), New System.Data.Common.DataColumnMapping("po_cc_id", "po_cc_id"), New System.Data.Common.DataColumnMapping("po_si_id", "po_si_id"), New System.Data.Common.DataColumnMapping("po_pjc_id", "po_pjc_id"), New System.Data.Common.DataColumnMapping("po_close_date", "po_close_date"), New System.Data.Common.DataColumnMapping("po_total", "po_total"), New System.Data.Common.DataColumnMapping("po_tran_id", "po_tran_id"), New System.Data.Common.DataColumnMapping("po_trans_id", "po_trans_id"), New System.Data.Common.DataColumnMapping("po_trans_rmks", "po_trans_rmks"), New System.Data.Common.DataColumnMapping("po_current_route", "po_current_route"), New System.Data.Common.DataColumnMapping("po_next_route", "po_next_route"), New System.Data.Common.DataColumnMapping("po_dt", "po_dt"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("cmaddr_name", "cmaddr_name"), New System.Data.Common.DataColumnMapping("tran_name", "tran_name"), New System.Data.Common.DataColumnMapping("po_status_cash", "po_status_cash"), New System.Data.Common.DataColumnMapping("pjc_desc_header", "pjc_desc_header"), New System.Data.Common.DataColumnMapping("si_desc_header", "si_desc_header"), New System.Data.Common.DataColumnMapping("sb_desc_header", "sb_desc_header"), New System.Data.Common.DataColumnMapping("cc_desc_header", "cc_desc_header"), New System.Data.Common.DataColumnMapping("po_credit_term", "po_credit_term"), New System.Data.Common.DataColumnMapping("po_taxable", "po_taxable"), New System.Data.Common.DataColumnMapping("po_tax_class", "po_tax_class"), New System.Data.Common.DataColumnMapping("po_tax_inc", "po_tax_inc"), New System.Data.Common.DataColumnMapping("po_total_ppn", "po_total_ppn"), New System.Data.Common.DataColumnMapping("po_total_pph", "po_total_pph"), New System.Data.Common.DataColumnMapping("po_cu_id", "po_cu_id"), New System.Data.Common.DataColumnMapping("po_exc_rate", "po_exc_rate"), New System.Data.Common.DataColumnMapping("cu_name", "cu_name"), New System.Data.Common.DataColumnMapping("po_credit_term_name", "po_credit_term_name"), New System.Data.Common.DataColumnMapping("po_tax_class_name", "po_tax_class_name"), New System.Data.Common.DataColumnMapping("po_total_after_tax", "po_total_after_tax"), New System.Data.Common.DataColumnMapping("po_total_ext", "po_total_ext"), New System.Data.Common.DataColumnMapping("po_total_ppn_ext", "po_total_ppn_ext"), New System.Data.Common.DataColumnMapping("po_total_pph_ext", "po_total_pph_ext"), New System.Data.Common.DataColumnMapping("po_total_after_tax_ext", "po_total_after_tax_ext"), New System.Data.Common.DataColumnMapping("en_desc_detail", "en_desc_detail"), New System.Data.Common.DataColumnMapping("pod_seq", "pod_seq"), New System.Data.Common.DataColumnMapping("si_desc_detail", "si_desc_detail"), New System.Data.Common.DataColumnMapping("pod_pt_id", "pod_pt_id"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pod_pt_desc1", "pod_pt_desc1"), New System.Data.Common.DataColumnMapping("pod_pt_desc2", "pod_pt_desc2"), New System.Data.Common.DataColumnMapping("pod_rmks", "pod_rmks"), New System.Data.Common.DataColumnMapping("pod_end_user", "pod_end_user"), New System.Data.Common.DataColumnMapping("pod_qty", "pod_qty"), New System.Data.Common.DataColumnMapping("pod_qty_receive", "pod_qty_receive"), New System.Data.Common.DataColumnMapping("pod_qty_invoice", "pod_qty_invoice"), New System.Data.Common.DataColumnMapping("pod_qty_outstanding", "pod_qty_outstanding"), New System.Data.Common.DataColumnMapping("pod_um", "pod_um"), New System.Data.Common.DataColumnMapping("um_name", "um_name"), New System.Data.Common.DataColumnMapping("pod_cost", "pod_cost"), New System.Data.Common.DataColumnMapping("pod_disc", "pod_disc"), New System.Data.Common.DataColumnMapping("pod_sb_id", "pod_sb_id"), New System.Data.Common.DataColumnMapping("pod_cc_id", "pod_cc_id"), New System.Data.Common.DataColumnMapping("pod_pjc_id", "pod_pjc_id"), New System.Data.Common.DataColumnMapping("pod_need_date", "pod_need_date"), New System.Data.Common.DataColumnMapping("pod_due_date", "pod_due_date"), New System.Data.Common.DataColumnMapping("pod_um_conv", "pod_um_conv"), New System.Data.Common.DataColumnMapping("pod_qty_real", "pod_qty_real"), New System.Data.Common.DataColumnMapping("pod_pt_class", "pod_pt_class"), New System.Data.Common.DataColumnMapping("pod_taxable", "pod_taxable"), New System.Data.Common.DataColumnMapping("pod_tax_inc", "pod_tax_inc"), New System.Data.Common.DataColumnMapping("pod_tax_class", "pod_tax_class"), New System.Data.Common.DataColumnMapping("pod_status", "pod_status"), New System.Data.Common.DataColumnMapping("pod_qty_return", "pod_qty_return"), New System.Data.Common.DataColumnMapping("pjc_desc_detail", "pjc_desc_detail"), New System.Data.Common.DataColumnMapping("si_desc_detailr", "si_desc_detailr"), New System.Data.Common.DataColumnMapping("sb_desc_detail", "sb_desc_detail"), New System.Data.Common.DataColumnMapping("pod_tax_class_name", "pod_tax_class_name"), New System.Data.Common.DataColumnMapping("req_code", "req_code"), New System.Data.Common.DataColumnMapping("cc_desc_detail", "cc_desc_detail"), New System.Data.Common.DataColumnMapping("pod_qty_cost", "pod_qty_cost"), New System.Data.Common.DataColumnMapping("amount_receive", "amount_receive")})})
        '
        'DspoAmountSub1
        '
        'Me.DspoAmountSub1.DataSetName = "DSPOAmountSub"
        'Me.DspoAmountSub1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'XRPOAmountReportSub
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.DspoAmountSub1
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(100, 100, 0, 0)
        Me.PageHeight = 2969
        Me.PageWidth = 2101
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.DspoAmountSub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_general_ledger_printTableAdapters.DataTable1TableAdapter
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    'Friend WithEvents DspoAmountSub1 As sygma_solution_system.DSPOAmountSub
End Class
