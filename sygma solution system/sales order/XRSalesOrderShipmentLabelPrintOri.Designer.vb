<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSalesOrderShipmentLabelPrintOri
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRSalesOrderShipmentLabelPrintOri))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell
        Me.cf_pt_description = New DevExpress.XtraReports.UI.CalculatedField
        'Me.DsSoship1 = New sygma_solution_system.DSSoship
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        'Me.TableAdapterManager1 = New sygma_solution_system.DataSet1TableAdapters.TableAdapterManager
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.DsSoship1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.Detail.Height = 20
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrTable1
        '
        Me.XrTable1.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.XrTable1.Location = New System.Drawing.Point(0, 0)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.Size = New System.Drawing.Size(491, 17)
        Me.XrTable1.StylePriority.UseFont = False
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2, Me.XrTableCell3, Me.XrTableCell4, Me.XrTableCell5})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 0.67999999999999994
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Font = New System.Drawing.Font("Square721 Cn BT", 7.0!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell1.Summary = XrSummary1
        Me.XrTableCell1.Text = "rec_no"
        Me.XrTableCell1.Weight = 0.13052151238591914
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_code", "")})
        Me.XrTableCell2.Font = New System.Drawing.Font("Square721 Cn BT", 7.0!)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.Weight = 0.31971751412429378
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.cf_pt_description", "")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Square721 Cn BT", 7.0!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 1.0186300808743822
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soshipd_qty", "{0:n}")})
        Me.XrTableCell4.Font = New System.Drawing.Font("Square721 Cn BT", 7.0!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell4.Weight = 0.16147776828665164
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.um_name", "")})
        Me.XrTableCell5.Font = New System.Drawing.Font("Square721 Cn BT", 7.0!)
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        Me.XrTableCell5.Text = "XrTableCell5"
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell5.Weight = 0.25087917796859971
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel8, Me.XrLabel9, Me.XrLabel15, Me.XrLabel24, Me.XrLabel22, Me.XrLabel5, Me.XrPageInfo1, Me.XrLabel7})
        Me.GroupFooter1.Height = 134
        Me.GroupFooter1.KeepTogether = True
        Me.GroupFooter1.Name = "GroupFooter1"
        Me.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        Me.GroupFooter1.PrintAtBottom = True
        Me.GroupFooter1.RepeatEveryPage = True
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(0, 0)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(542, 9)
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2, Me.xrpb_logo, Me.XrLabel23, Me.XrLabel21, Me.XrLabel20, Me.XrLabel3, Me.XrLabel6, Me.XrLabel4, Me.XrPageInfo2, Me.XrLabel2, Me.XrLabel1, Me.XrLabel33, Me.XrLabel18, Me.XrLabel26, Me.XrLabel27, Me.XrLabel28, Me.XrLabel29, Me.XrLabel30, Me.XrLabel31, Me.XrLabel32, Me.XrLabel25, Me.XrLabel10, Me.XrLabel11, Me.XrLabel12, Me.XrLabel13, Me.XrLine2})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("soship_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 193
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.RepeatEveryPage = True
        '
        'XrTable2
        '
        Me.XrTable2.Location = New System.Drawing.Point(0, 158)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.Size = New System.Drawing.Size(542, 17)
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell7})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.StylePriority.UseBorders = False
        Me.XrTableRow2.Weight = 0.67999999999999994
        '
        'XrTableCell7
        '
        Me.XrTableCell7.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.StylePriority.UseFont = False
        Me.XrTableCell7.Text = "Item"
        Me.XrTableCell7.Weight = 2.0766283524904217
        '
        'cf_pt_description
        '
        Me.cf_pt_description.DataMember = "DataTable1"
        'Me.cf_pt_description.DataSource = Me.DsSoship1
        Me.cf_pt_description.Expression = "trim([pt_desc1] + ' ' + [pt_desc2])"
        Me.cf_pt_description.Name = "cf_pt_description"
        '
        'DsSoship1
        '
        'Me.DsSoship1.DataSetName = "DSSoship"
        'Me.DsSoship1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("soship_oid", "soship_oid"), New System.Data.Common.DataColumnMapping("soship_dom_id", "soship_dom_id"), New System.Data.Common.DataColumnMapping("soship_en_id", "soship_en_id"), New System.Data.Common.DataColumnMapping("soship_add_by", "soship_add_by"), New System.Data.Common.DataColumnMapping("soship_add_date", "soship_add_date"), New System.Data.Common.DataColumnMapping("soship_upd_by", "soship_upd_by"), New System.Data.Common.DataColumnMapping("soship_upd_date", "soship_upd_date"), New System.Data.Common.DataColumnMapping("soship_code", "soship_code"), New System.Data.Common.DataColumnMapping("soship_date", "soship_date"), New System.Data.Common.DataColumnMapping("soship_so_oid", "soship_so_oid"), New System.Data.Common.DataColumnMapping("soship_si_id", "soship_si_id"), New System.Data.Common.DataColumnMapping("soship_is_shipment", "soship_is_shipment"), New System.Data.Common.DataColumnMapping("soship_dt", "soship_dt"), New System.Data.Common.DataColumnMapping("soshipd_qty", "soshipd_qty"), New System.Data.Common.DataColumnMapping("soshipd_um", "soshipd_um"), New System.Data.Common.DataColumnMapping("soshipd_um_conv", "soshipd_um_conv"), New System.Data.Common.DataColumnMapping("soshipd_cancel_bo", "soshipd_cancel_bo"), New System.Data.Common.DataColumnMapping("soshipd_qty_real", "soshipd_qty_real"), New System.Data.Common.DataColumnMapping("soshipd_si_id", "soshipd_si_id"), New System.Data.Common.DataColumnMapping("soshipd_loc_id", "soshipd_loc_id"), New System.Data.Common.DataColumnMapping("soshipd_lot_serial", "soshipd_lot_serial"), New System.Data.Common.DataColumnMapping("soshipd_rea_code_id", "soshipd_rea_code_id"), New System.Data.Common.DataColumnMapping("soshipd_dt", "soshipd_dt"), New System.Data.Common.DataColumnMapping("soshipd_qty_inv", "soshipd_qty_inv"), New System.Data.Common.DataColumnMapping("soshipd_close_line", "soshipd_close_line"), New System.Data.Common.DataColumnMapping("so_code", "so_code"), New System.Data.Common.DataColumnMapping("so_date", "so_date"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ptnra_line_1", "ptnra_line_1"), New System.Data.Common.DataColumnMapping("ptnra_line_2", "ptnra_line_2"), New System.Data.Common.DataColumnMapping("ptnra_line_3", "ptnra_line_3"), New System.Data.Common.DataColumnMapping("credit_term_name", "credit_term_name"), New System.Data.Common.DataColumnMapping("cu_name", "cu_name"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("um_name", "um_name"), New System.Data.Common.DataColumnMapping("cmaddr_name", "cmaddr_name"), New System.Data.Common.DataColumnMapping("cmaddr_line_1", "cmaddr_line_1"), New System.Data.Common.DataColumnMapping("cmaddr_line_2", "cmaddr_line_2"), New System.Data.Common.DataColumnMapping("cmaddr_line_3", "cmaddr_line_3"), New System.Data.Common.DataColumnMapping("so_delivery_receipt_number", "so_delivery_receipt_number"), New System.Data.Common.DataColumnMapping("so_delivery_courier", "so_delivery_courier"), New System.Data.Common.DataColumnMapping("tranaprvd_name_1", "tranaprvd_name_1"), New System.Data.Common.DataColumnMapping("tranaprvd_name_2", "tranaprvd_name_2"), New System.Data.Common.DataColumnMapping("tranaprvd_name_3", "tranaprvd_name_3"), New System.Data.Common.DataColumnMapping("tranaprvd_name_4", "tranaprvd_name_4"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_1", "tranaprvd_pos_1"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_2", "tranaprvd_pos_2"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_3", "tranaprvd_pos_3"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_4", "tranaprvd_pos_4"), New System.Data.Common.DataColumnMapping("so_ref_po_code", "so_ref_po_code")})})
        '
        'XrLabel23
        '
        Me.XrLabel23.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel23.Location = New System.Drawing.Point(191, 134)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel21.Location = New System.Drawing.Point(191, 116)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.Text = ":"
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel20.Location = New System.Drawing.Point(191, 100)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.Text = ":"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.Location = New System.Drawing.Point(125, 100)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(74, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "DO Number"
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel6.Location = New System.Drawing.Point(125, 134)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = "PACKAGE"
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.Location = New System.Drawing.Point(125, 116)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "DO Date"
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrPageInfo2.Location = New System.Drawing.Point(208, 134)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.Size = New System.Drawing.Size(117, 17)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soship_date", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel2.Location = New System.Drawing.Point(208, 116)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.soship_code", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel1.Location = New System.Drawing.Point(208, 100)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel33
        '
        Me.XrLabel33.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel33.Location = New System.Drawing.Point(333, 117)
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel33.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel33.StylePriority.UseFont = False
        Me.XrLabel33.Text = "RESI Number"
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.so_code", "")})
        Me.XrLabel18.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel18.Location = New System.Drawing.Point(417, 100)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel26
        '
        Me.XrLabel26.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel26.Location = New System.Drawing.Point(400, 100)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel26.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.Text = ":"
        '
        'XrLabel27
        '
        Me.XrLabel27.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel27.Location = New System.Drawing.Point(333, 100)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel27.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.Text = "SO Number"
        '
        'XrLabel28
        '
        Me.XrLabel28.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel28.Location = New System.Drawing.Point(333, 134)
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel28.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel28.StylePriority.UseFont = False
        Me.XrLabel28.Text = "Courier"
        '
        'XrLabel29
        '
        Me.XrLabel29.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel29.Location = New System.Drawing.Point(400, 134)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel29.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.Text = ":"
        '
        'XrLabel30
        '
        Me.XrLabel30.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.so_delivery_courier", "")})
        Me.XrLabel30.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel30.Location = New System.Drawing.Point(417, 134)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel30.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.StylePriority.UseTextAlignment = False
        Me.XrLabel30.Text = "XrLabel18"
        Me.XrLabel30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel31
        '
        Me.XrLabel31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.so_delivery_receipt_number", "")})
        Me.XrLabel31.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel31.Location = New System.Drawing.Point(417, 117)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel31.Size = New System.Drawing.Size(117, 17)
        Me.XrLabel31.StylePriority.UseFont = False
        Me.XrLabel31.StylePriority.UseTextAlignment = False
        Me.XrLabel31.Text = "XrLabel2"
        Me.XrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel32
        '
        Me.XrLabel32.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel32.Location = New System.Drawing.Point(400, 117)
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel32.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.Text = ":"
        '
        'XrLabel8
        '
        Me.XrLabel8.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cmaddr_line_2", "")})
        Me.XrLabel8.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel8.Location = New System.Drawing.Point(0, 67)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(467, 17)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "XrLabel8"
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cmaddr_line_3", "")})
        Me.XrLabel9.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel9.Location = New System.Drawing.Point(0, 84)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(467, 17)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "XrLabel9"
        '
        'XrLabel15
        '
        Me.XrLabel15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.cmaddr_name", "")})
        Me.XrLabel15.Font = New System.Drawing.Font("Square721 Cn BT", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel15.Location = New System.Drawing.Point(0, 33)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.Size = New System.Drawing.Size(467, 17)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.Text = "XrLabel15"
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New System.Drawing.Font("Square721 Cn BT", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel24.Location = New System.Drawing.Point(0, 17)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.Size = New System.Drawing.Size(55, 17)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.Text = "From :"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Square721 Cn BT", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel25.Location = New System.Drawing.Point(125, 8)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(55, 16)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = "Ship To :"
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.ptnr_name", "")})
        Me.XrLabel10.Font = New System.Drawing.Font("Square721 Cn BT", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel10.Location = New System.Drawing.Point(125, 25)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(409, 17)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "XrLabel10"
        '
        'XrLabel11
        '
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.ptnra_line_1", "")})
        Me.XrLabel11.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel11.Location = New System.Drawing.Point(125, 42)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(409, 17)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = "XrLabel11"
        '
        'XrLabel12
        '
        Me.XrLabel12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.ptnra_line_2", "")})
        Me.XrLabel12.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel12.Location = New System.Drawing.Point(125, 59)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(409, 16)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = "XrLabel12"
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.ptnra_line_3", "")})
        Me.XrLabel13.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel13.Location = New System.Drawing.Point(125, 75)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(409, 17)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.Text = "XrLabel13"
        '
        'TableAdapterManager1
        '
        'Me.TableAdapterManager1.BackupDataSetBeforeUpdate = False
        'Me.TableAdapterManager1.Connection = Nothing
        'e.TableAdapterManager1.UpdateOrder = sygma_solution_system.DataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'xrpb_logo
        '
        Me.xrpb_logo.Image = CType(resources.GetObject("xrpb_logo.Image"), System.Drawing.Image)
        Me.xrpb_logo.Location = New System.Drawing.Point(8, 8)
        Me.xrpb_logo.Name = "xrpb_logo"
        Me.xrpb_logo.Size = New System.Drawing.Size(100, 75)
        Me.xrpb_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel22.Location = New System.Drawing.Point(409, 108)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = ":"
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.Location = New System.Drawing.Point(342, 108)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(67, 17)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "Print Date"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrPageInfo1.Format = "{0:dd/MM/yyyy}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(425, 108)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(117, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLine2
        '
        Me.XrLine2.Location = New System.Drawing.Point(0, 92)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(542, 9)
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.cmaddr_line_1", "")})
        Me.XrLabel7.Font = New System.Drawing.Font("Square721 Cn BT", 8.0!)
        Me.XrLabel7.Location = New System.Drawing.Point(0, 50)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(467, 17)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = "XrLabel7"
        '
        'XRSalesOrderShipmentLabelPrint
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupFooter1, Me.GroupHeader1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_pt_description})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.DsSoship1
        Me.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.Margins = New System.Drawing.Printing.Margins(20, 20, 25, 30)
        Me.PageHeight = 413
        Me.PageWidth = 583
        Me.PaperKind = System.Drawing.Printing.PaperKind.A6Rotated
        Me.Version = "9.1"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.DsSoship1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents cf_pt_description As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    'Friend WithEvents DsSoship1 As sygma_solution_system.DSSoship
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents TableAdapterManager1 As sygma_solution_system.DataSet1TableAdapters.TableAdapterManager
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
End Class
