<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRShipAddressSQ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRShipAddressSQ))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        'Me.DsShipAddressSQ1 = New sygma_solution_system.DSShipAddressSQ
        Me.cf_address = New DevExpress.XtraReports.UI.CalculatedField
        Me.cf_ptnr_address = New DevExpress.XtraReports.UI.CalculatedField
        'CType(Me.DsShipAddressSQ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4, Me.XrLabel5, Me.XrLabel6, Me.XrLine1, Me.XrLine2})
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 918
        Me.Detail.MultiColumn.ColumnCount = 2
        Me.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cmaddr_name", "")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.KeepTogether = True
        Me.XrLabel1.Location = New System.Drawing.Point(0, 37)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(1355, 64)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "XrLabel1"
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_address", "")})
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.KeepTogether = True
        Me.XrLabel2.Location = New System.Drawing.Point(0, 106)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(1355, 127)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "XrLabel2"
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cmaddr_phone_1", "")})
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.KeepTogether = True
        Me.XrLabel3.Location = New System.Drawing.Point(0, 233)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(508, 64)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "XrLabel3"
        '
        'XrLabel4
        '
        Me.XrLabel4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cf_ptnr_address", "")})
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.KeepTogether = True
        Me.XrLabel4.Location = New System.Drawing.Point(0, 339)
        Me.XrLabel4.Multiline = True
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(1347, 402)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "XrLabel4"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.KeepTogether = True
        Me.XrLabel5.Location = New System.Drawing.Point(148, 783)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(1037, 64)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "Al Qur’an Jangan di Injak/Banting!"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.sq_code", "")})
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.KeepTogether = True
        Me.XrLabel6.Location = New System.Drawing.Point(698, 233)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(656, 63)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.Location = New System.Drawing.Point(1363, 21)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(19, 860)
        '
        'XrLine2
        '
        Me.XrLine2.Dpi = 254.0!
        Me.XrLine2.LineWidth = 3
        Me.XrLine2.Location = New System.Drawing.Point(0, 868)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(1363, 29)
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
        Me.PageFooter.Height = 0
        Me.PageFooter.Name = "PageFooter"
        Me.PageFooter.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=sea_pub"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("sqd_sq_oid", "sqd_sq_oid"), New System.Data.Common.DataColumnMapping("um_name", "um_name"), New System.Data.Common.DataColumnMapping("sq_dropship_address", "sq_dropship_address"), New System.Data.Common.DataColumnMapping("sq_total_aft_tax", "sq_total_aft_tax"), New System.Data.Common.DataColumnMapping("tranaprvd_name_1", "tranaprvd_name_1"), New System.Data.Common.DataColumnMapping("tranaprvd_name_2", "tranaprvd_name_2"), New System.Data.Common.DataColumnMapping("tranaprvd_name_3", "tranaprvd_name_3"), New System.Data.Common.DataColumnMapping("tranaprvd_name_4", "tranaprvd_name_4"), New System.Data.Common.DataColumnMapping("sq_code", "sq_code"), New System.Data.Common.DataColumnMapping("sq_date", "sq_date"), New System.Data.Common.DataColumnMapping("sq_terbilang", "sq_terbilang"), New System.Data.Common.DataColumnMapping("sq_total", "sq_total"), New System.Data.Common.DataColumnMapping("sq_total_ppn", "sq_total_ppn"), New System.Data.Common.DataColumnMapping("sq_total_pph", "sq_total_pph"), New System.Data.Common.DataColumnMapping("sqd_oid", "sqd_oid"), New System.Data.Common.DataColumnMapping("sqd_dom_id", "sqd_dom_id"), New System.Data.Common.DataColumnMapping("sqd_en_id", "sqd_en_id"), New System.Data.Common.DataColumnMapping("sqd_add_by", "sqd_add_by"), New System.Data.Common.DataColumnMapping("sqd_add_date", "sqd_add_date"), New System.Data.Common.DataColumnMapping("sqd_upd_by", "sqd_upd_by"), New System.Data.Common.DataColumnMapping("sqd_upd_date", "sqd_upd_date"), New System.Data.Common.DataColumnMapping("sqd_seq", "sqd_seq"), New System.Data.Common.DataColumnMapping("sqd_is_additional_charge", "sqd_is_additional_charge"), New System.Data.Common.DataColumnMapping("sqd_si_id", "sqd_si_id"), New System.Data.Common.DataColumnMapping("sqd_pt_id", "sqd_pt_id"), New System.Data.Common.DataColumnMapping("sqd_rmks", "sqd_rmks"), New System.Data.Common.DataColumnMapping("sqd_qty", "sqd_qty"), New System.Data.Common.DataColumnMapping("sqd_qty_allocated", "sqd_qty_allocated"), New System.Data.Common.DataColumnMapping("sqd_qty_picked", "sqd_qty_picked"), New System.Data.Common.DataColumnMapping("sqd_qty_shipment", "sqd_qty_shipment"), New System.Data.Common.DataColumnMapping("sqd_qty_pending_inv", "sqd_qty_pending_inv"), New System.Data.Common.DataColumnMapping("sqd_qty_invoice", "sqd_qty_invoice"), New System.Data.Common.DataColumnMapping("sqd_um", "sqd_um"), New System.Data.Common.DataColumnMapping("sqd_cost", "sqd_cost"), New System.Data.Common.DataColumnMapping("sqd_price", "sqd_price"), New System.Data.Common.DataColumnMapping("sqd_disc", "sqd_disc"), New System.Data.Common.DataColumnMapping("sqd_sales_ac_id", "sqd_sales_ac_id"), New System.Data.Common.DataColumnMapping("sqd_sales_sb_id", "sqd_sales_sb_id"), New System.Data.Common.DataColumnMapping("sqd_sales_cc_id", "sqd_sales_cc_id"), New System.Data.Common.DataColumnMapping("sqd_disc_ac_id", "sqd_disc_ac_id"), New System.Data.Common.DataColumnMapping("sqd_um_conv", "sqd_um_conv"), New System.Data.Common.DataColumnMapping("sqd_qty_real", "sqd_qty_real"), New System.Data.Common.DataColumnMapping("sqd_taxable", "sqd_taxable"), New System.Data.Common.DataColumnMapping("sqd_tax_inc", "sqd_tax_inc"), New System.Data.Common.DataColumnMapping("sqd_tax_class", "sqd_tax_class"), New System.Data.Common.DataColumnMapping("sqd_status", "sqd_status"), New System.Data.Common.DataColumnMapping("sqd_dt", "sqd_dt"), New System.Data.Common.DataColumnMapping("sqd_payment", "sqd_payment"), New System.Data.Common.DataColumnMapping("sqd_dp", "sqd_dp"), New System.Data.Common.DataColumnMapping("sqd_sales_unit", "sqd_sales_unit"), New System.Data.Common.DataColumnMapping("sqd_loc_id", "sqd_loc_id"), New System.Data.Common.DataColumnMapping("sqd_serial", "sqd_serial"), New System.Data.Common.DataColumnMapping("sqd_qty_return", "sqd_qty_return"), New System.Data.Common.DataColumnMapping("sqd_ppn_type", "sqd_ppn_type"), New System.Data.Common.DataColumnMapping("cmaddr_code", "cmaddr_code"), New System.Data.Common.DataColumnMapping("cmaddr_name", "cmaddr_name"), New System.Data.Common.DataColumnMapping("cmaddr_line_1", "cmaddr_line_1"), New System.Data.Common.DataColumnMapping("cmaddr_line_2", "cmaddr_line_2"), New System.Data.Common.DataColumnMapping("cmaddr_line_3", "cmaddr_line_3"), New System.Data.Common.DataColumnMapping("cmaddr_phone_1", "cmaddr_phone_1"), New System.Data.Common.DataColumnMapping("cmaddr_phone_2", "cmaddr_phone_2"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ptnra_line_1", "ptnra_line_1"), New System.Data.Common.DataColumnMapping("ptnra_line_2", "ptnra_line_2"), New System.Data.Common.DataColumnMapping("ptnra_line_3", "ptnra_line_3"), New System.Data.Common.DataColumnMapping("ptnra_zip", "ptnra_zip"), New System.Data.Common.DataColumnMapping("cu_name", "cu_name"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_1", "tranaprvd_pos_1"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_2", "tranaprvd_pos_2"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_3", "tranaprvd_pos_3"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_4", "tranaprvd_pos_4")})})
        '
        'DsShipAddressSQ1
        '
        'Me.DsShipAddressSQ1.DataSetName = "DSShipAddressSQ"
        'Me.DsShipAddressSQ1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cf_address
        '
        Me.cf_address.DataMember = "Table"
        'Me.cf_address.DataSource = Me.DsShipAddressSQ1
        Me.cf_address.Expression = "[cmaddr_line_1] + ' ' + [cmaddr_line_2] + ' ' + [cmaddr_line_3]"
        Me.cf_address.Name = "cf_address"
        '
        'cf_ptnr_address
        '
        Me.cf_ptnr_address.DataMember = "Table"
        'Me.cf_ptnr_address.DataSource = Me.DsShipAddressSQ1
        Me.cf_ptnr_address.Expression = "Iif([sq_dropship_address] == '',[ptnr_name]  + [line]+ [ptnra_line_1] + ' ' + [pt" & _
            "nra_line_2] + ' '  + [ptnra_line_3] + [line] + '(' + [ptnra_phone_1] + ')' ,[sq_" & _
            "dropship_address] )" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.cf_ptnr_address.Name = "cf_ptnr_address"
        '
        'XRShipAddressSQ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.PageHeader, Me.PageFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_address, Me.cf_ptnr_address})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.DsShipAddressSQ1
        Me.Dpi = 254.0!
        Me.Landscape = True
        Me.PageHeight = 2101
        Me.PageWidth = 2969
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.2"
        'CType(Me.DsShipAddressSQ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    'Friend WithEvents DsShipAddressSQ1 As sygma_solution_system.DSShipAddressSQ
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents cf_address As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents cf_ptnr_address As DevExpress.XtraReports.UI.CalculatedField
End Class
