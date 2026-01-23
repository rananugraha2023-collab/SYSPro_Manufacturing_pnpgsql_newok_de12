<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRSQ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRSQ))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel
        Me.PgSqlDataAdapter1 = New npgsql.nPgSqlDataAdapter
        Me.pgSqlSelectCommand1 = New npgsql.nPgSqlCommand
        Me.PgSqlConnection1 = New npgsql.nPgSqlConnection
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow10 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell19 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell20 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.XrLabel45 = New DevExpress.XtraReports.UI.XRLabel
        Me.tranaprvd_line_1 = New DevExpress.XtraReports.UI.XRLine
        Me.tranaprvd_line_2 = New DevExpress.XtraReports.UI.XRLine
        Me.tranaprvd_line_3 = New DevExpress.XtraReports.UI.XRLine
        Me.tranaprvd_line_4 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLine2 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel
        'Me.PgSqlDataTable1 = New CoreLab.PostgreSql.PgSqlDataTable
        'Me.pgSqlSelectCommand2 = New npgsql.nPgSqlCommand
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow4 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow
        Me.XrTableCell11 = New DevExpress.XtraReports.UI.XRTableCell
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.PgSqlDataTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel12, Me.XrLabel13, Me.XrLabel14, Me.XrLabel15, Me.XrLabel16})
        Me.Detail.Height = 17
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel12
        '
        Me.XrLabel12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.sqd_qty", "")})
        Me.XrLabel12.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel12.Location = New System.Drawing.Point(567, 0)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(83, 17)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.StylePriority.UseTextAlignment = False
        Me.XrLabel12.Text = "XrLabel12"
        Me.XrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.pt_code", "")})
        Me.XrLabel13.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel13.Location = New System.Drawing.Point(33, 0)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(142, 17)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel14
        '
        Me.XrLabel14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.pt_desc1", "")})
        Me.XrLabel14.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel14.Location = New System.Drawing.Point(175, 0)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.Size = New System.Drawing.Size(392, 17)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "XrLabel14"
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.um_name", "")})
        Me.XrLabel15.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel15.Location = New System.Drawing.Point(650, 0)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.Size = New System.Drawing.Size(92, 17)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "XrLabel15"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel16
        '
        Me.XrLabel16.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.sqd_seq", "")})
        Me.XrLabel16.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel16.Location = New System.Drawing.Point(0, 0)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.Size = New System.Drawing.Size(33, 17)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n0}"
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel16.Summary = XrSummary1
        Me.XrLabel16.Text = "XrLabel16"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'PgSqlDataAdapter1
        '
        Me.PgSqlDataAdapter1.SelectCommand = Me.pgSqlSelectCommand1
        Me.PgSqlDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SQ", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("um_name", "um_name"), New System.Data.Common.DataColumnMapping("ac_code_sales", "ac_code_sales"), New System.Data.Common.DataColumnMapping("ac_name_sales", "ac_name_sales"), New System.Data.Common.DataColumnMapping("ac_code_disc", "ac_code_disc"), New System.Data.Common.DataColumnMapping("ac_name_disc", "ac_name_disc"), New System.Data.Common.DataColumnMapping("sqd_tax_class_name", "sqd_tax_class_name"), New System.Data.Common.DataColumnMapping("sqd_oid", "sqd_oid"), New System.Data.Common.DataColumnMapping("sqd_dom_id", "sqd_dom_id"), New System.Data.Common.DataColumnMapping("sqd_en_id", "sqd_en_id"), New System.Data.Common.DataColumnMapping("sqd_add_by", "sqd_add_by"), New System.Data.Common.DataColumnMapping("sqd_add_date", "sqd_add_date"), New System.Data.Common.DataColumnMapping("sqd_upd_by", "sqd_upd_by"), New System.Data.Common.DataColumnMapping("sqd_upd_date", "sqd_upd_date"), New System.Data.Common.DataColumnMapping("sqd_sq_oid", "sqd_sq_oid"), New System.Data.Common.DataColumnMapping("sqd_seq", "sqd_seq"), New System.Data.Common.DataColumnMapping("sqd_is_additional_charge", "sqd_is_additional_charge"), New System.Data.Common.DataColumnMapping("sqd_si_id", "sqd_si_id"), New System.Data.Common.DataColumnMapping("sqd_pt_id", "sqd_pt_id"), New System.Data.Common.DataColumnMapping("sqd_rmks", "sqd_rmks"), New System.Data.Common.DataColumnMapping("sqd_qty", "sqd_qty"), New System.Data.Common.DataColumnMapping("sqd_qty_allocated", "sqd_qty_allocated"), New System.Data.Common.DataColumnMapping("sqd_qty_picked", "sqd_qty_picked"), New System.Data.Common.DataColumnMapping("sqd_qty_shipment", "sqd_qty_shipment"), New System.Data.Common.DataColumnMapping("sqd_qty_transfer_issue", "sqd_qty_transfer_issue"), New System.Data.Common.DataColumnMapping("sqd_qty_transfer_receipt", "sqd_qty_transfer_receipt"), New System.Data.Common.DataColumnMapping("sqd_qty_pending_inv", "sqd_qty_pending_inv"), New System.Data.Common.DataColumnMapping("sqd_qty_invoice", "sqd_qty_invoice"), New System.Data.Common.DataColumnMapping("sqd_um", "sqd_um"), New System.Data.Common.DataColumnMapping("sqd_cost", "sqd_cost"), New System.Data.Common.DataColumnMapping("sqd_price", "sqd_price"), New System.Data.Common.DataColumnMapping("sqd_total_amount_price", "sqd_total_amount_price"), New System.Data.Common.DataColumnMapping("sqd_disc", "sqd_disc"), New System.Data.Common.DataColumnMapping("sqd_sales_ac_id", "sqd_sales_ac_id"), New System.Data.Common.DataColumnMapping("sqd_sales_sb_id", "sqd_sales_sb_id"), New System.Data.Common.DataColumnMapping("sqd_sales_cc_id", "sqd_sales_cc_id"), New System.Data.Common.DataColumnMapping("sqd_disc_ac_id", "sqd_disc_ac_id"), New System.Data.Common.DataColumnMapping("sqd_um_conv", "sqd_um_conv"), New System.Data.Common.DataColumnMapping("sqd_qty_real", "sqd_qty_real"), New System.Data.Common.DataColumnMapping("sqd_taxable", "sqd_taxable"), New System.Data.Common.DataColumnMapping("sqd_tax_inc", "sqd_tax_inc"), New System.Data.Common.DataColumnMapping("sqd_tax_class", "sqd_tax_class"), New System.Data.Common.DataColumnMapping("sqd_ppn_type", "sqd_ppn_type"), New System.Data.Common.DataColumnMapping("sqd_status", "sqd_status"), New System.Data.Common.DataColumnMapping("sqd_dt", "sqd_dt"), New System.Data.Common.DataColumnMapping("sqd_payment", "sqd_payment"), New System.Data.Common.DataColumnMapping("sqd_dp", "sqd_dp"), New System.Data.Common.DataColumnMapping("sqd_sales_unit", "sqd_sales_unit"), New System.Data.Common.DataColumnMapping("sqd_loc_id", "sqd_loc_id"), New System.Data.Common.DataColumnMapping("sqd_serial", "sqd_serial"), New System.Data.Common.DataColumnMapping("sqd_pod_oid", "sqd_pod_oid"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("si_desc", "si_desc"), New System.Data.Common.DataColumnMapping("pt_code", "pt_code"), New System.Data.Common.DataColumnMapping("pt_desc1", "pt_desc1"), New System.Data.Common.DataColumnMapping("pt_desc2", "pt_desc2"), New System.Data.Common.DataColumnMapping("pt_type", "pt_type"), New System.Data.Common.DataColumnMapping("pt_ls", "pt_ls"), New System.Data.Common.DataColumnMapping("sb_desc", "sb_desc"), New System.Data.Common.DataColumnMapping("cc_desc", "cc_desc"), New System.Data.Common.DataColumnMapping("loc_desc", "loc_desc"), New System.Data.Common.DataColumnMapping("sq_code", "sq_code"), New System.Data.Common.DataColumnMapping("en_desc1", "en_desc1"), New System.Data.Common.DataColumnMapping("sq_date", "sq_date"), New System.Data.Common.DataColumnMapping("tranaprvd_tran_oid", "tranaprvd_tran_oid"), New System.Data.Common.DataColumnMapping("tranaprvd_tran_code", "tranaprvd_tran_code"), New System.Data.Common.DataColumnMapping("tranaprvd_name_1", "tranaprvd_name_1"), New System.Data.Common.DataColumnMapping("tranaprvd_name_2", "tranaprvd_name_2"), New System.Data.Common.DataColumnMapping("tranaprvd_name_3", "tranaprvd_name_3"), New System.Data.Common.DataColumnMapping("tranaprvd_name_4", "tranaprvd_name_4"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_1", "tranaprvd_pos_1"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_2", "tranaprvd_pos_2"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_3", "tranaprvd_pos_3"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_4", "tranaprvd_pos_4"), New System.Data.Common.DataColumnMapping("tranaprvd_name_5", "tranaprvd_name_5"), New System.Data.Common.DataColumnMapping("tranaprvd_name_6", "tranaprvd_name_6"), New System.Data.Common.DataColumnMapping("tranaprvd_name_7", "tranaprvd_name_7"), New System.Data.Common.DataColumnMapping("tranaprvd_name_8", "tranaprvd_name_8"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_6", "tranaprvd_pos_6"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_5", "tranaprvd_pos_5"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_7", "tranaprvd_pos_7"), New System.Data.Common.DataColumnMapping("tranaprvd_pos_8", "tranaprvd_pos_8")})})
        '
        'pgSqlSelectCommand1
        '
        Me.pgSqlSelectCommand1.CommandText = resources.GetString("pgSqlSelectCommand1.CommandText")
        Me.pgSqlSelectCommand1.Connection = Me.PgSqlConnection1
        'Me.pgSqlSelectCommand1.Name = "pgSqlSelectCommand1"
        'Me.pgSqlSelectCommand1.Owner = Me
        '
        'PgSqlConnection1
        '
        Me.PgSqlConnection1.ConnectionString = "User Id=postgres;Password=bangkar;Host=192.168.1.157;Database=sygma_cmds_firman;"
        'Me.PgSqlConnection1.Name = "PgSqlConnection1"
        'Me.PgSqlConnection1.Owner = Me
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrpb_logo, Me.XrLabel4, Me.XrLabel5, Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrTable4, Me.XrLabel8, Me.XrLabel9, Me.XrLabel10, Me.XrLabel11, Me.XrPageInfo1, Me.XrPageInfo2, Me.XrLabel6, Me.XrLabel7, Me.XrLabel25, Me.XrTable1})
        Me.GroupHeader1.Height = 217
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'xrpb_logo
        '
        Me.xrpb_logo.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Top
        Me.xrpb_logo.Image = CType(resources.GetObject("xrpb_logo.Image"), System.Drawing.Image)
        Me.xrpb_logo.Location = New System.Drawing.Point(0, 0)
        Me.xrpb_logo.Name = "xrpb_logo"
        Me.xrpb_logo.Size = New System.Drawing.Size(175, 75)
        Me.xrpb_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLabel4
        '
        Me.XrLabel4.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel4.BorderWidth = 2
        Me.XrLabel4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(525, 0)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(217, 17)
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseBorderWidth = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "DELIVERY ORDER"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.Location = New System.Drawing.Point(542, 25)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(42, 17)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "Nomor"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(542, 42)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(42, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "Tanggal"
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(592, 25)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = ":"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.Location = New System.Drawing.Point(592, 42)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = ":"
        '
        'XrTable4
        '
        Me.XrTable4.Location = New System.Drawing.Point(0, 200)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow10})
        Me.XrTable4.Size = New System.Drawing.Size(742, 17)
        '
        'XrTableRow10
        '
        Me.XrTableRow10.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.XrTableRow10.BorderWidth = 2
        Me.XrTableRow10.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell18, Me.XrTableCell19, Me.XrTableCell20, Me.XrTableCell21, Me.XrTableCell22})
        Me.XrTableRow10.Name = "XrTableRow10"
        Me.XrTableRow10.StylePriority.UseBorders = False
        Me.XrTableRow10.StylePriority.UseBorderWidth = False
        Me.XrTableRow10.Weight = 1
        '
        'XrTableCell18
        '
        Me.XrTableCell18.BorderWidth = 1
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.StylePriority.UseBorderWidth = False
        Me.XrTableCell18.Text = "No."
        Me.XrTableCell18.Weight = 0.31
        '
        'XrTableCell19
        '
        Me.XrTableCell19.BorderWidth = 1
        Me.XrTableCell19.Name = "XrTableCell19"
        Me.XrTableCell19.StylePriority.UseBorderWidth = False
        Me.XrTableCell19.Text = "Code"
        Me.XrTableCell19.Weight = 1.2261333333333333
        '
        'XrTableCell20
        '
        Me.XrTableCell20.BorderWidth = 1
        Me.XrTableCell20.Name = "XrTableCell20"
        Me.XrTableCell20.StylePriority.UseBorderWidth = False
        Me.XrTableCell20.Text = "Description"
        Me.XrTableCell20.Weight = 3.4638666666666666
        '
        'XrTableCell21
        '
        Me.XrTableCell21.BorderWidth = 1
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.StylePriority.UseBorderWidth = False
        Me.XrTableCell21.StylePriority.UseTextAlignment = False
        Me.XrTableCell21.Text = "Qty"
        Me.XrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell21.Weight = 0.75
        '
        'XrTableCell22
        '
        Me.XrTableCell22.BorderWidth = 1
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.StylePriority.UseBorderWidth = False
        Me.XrTableCell22.StylePriority.UseTextAlignment = False
        Me.XrTableCell22.Text = "UM"
        Me.XrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrTableCell22.Weight = 0.83
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel8.Location = New System.Drawing.Point(542, 58)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(50, 17)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = "Print Date"
        '
        'XrLabel9
        '
        Me.XrLabel9.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel9.Location = New System.Drawing.Point(542, 75)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(50, 17)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "Print Page"
        '
        'XrLabel10
        '
        Me.XrLabel10.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.Location = New System.Drawing.Point(592, 58)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = ":"
        '
        'XrLabel11
        '
        Me.XrLabel11.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel11.Location = New System.Drawing.Point(592, 75)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = ":"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Location = New System.Drawing.Point(608, 75)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.Size = New System.Drawing.Size(133, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Format = "{0:dd/MM/yyyy hh:mm:sss}"
        Me.XrPageInfo2.Location = New System.Drawing.Point(608, 58)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.Size = New System.Drawing.Size(133, 17)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.sq_code", "")})
        Me.XrLabel6.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.Location = New System.Drawing.Point(600, 25)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(142, 17)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel6"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel7
        '
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.sq_date", "{0:dd/MM/yyyy}")})
        Me.XrLabel7.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel7.Location = New System.Drawing.Point(600, 42)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(142, 17)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.Text = "XrLabel7"
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrTableCell5
        '
        Me.XrTableCell5.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseFont = False
        Me.XrTableCell5.Text = "Nomor"
        Me.XrTableCell5.Weight = 1
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel45, Me.tranaprvd_line_1, Me.tranaprvd_line_2, Me.tranaprvd_line_3, Me.tranaprvd_line_4, Me.XrLine2, Me.XrLabel17, Me.XrLabel18, Me.XrLabel19, Me.XrLabel20, Me.XrLabel21, Me.XrLabel22, Me.XrLabel23, Me.XrLabel24})
        Me.GroupFooter1.Height = 142
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLabel45
        '
        Me.XrLabel45.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.XrLabel45.Location = New System.Drawing.Point(0, 100)
        Me.XrLabel45.Name = "XrLabel45"
        Me.XrLabel45.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel45.Size = New System.Drawing.Size(25, 17)
        Me.XrLabel45.StylePriority.UseFont = False
        Me.XrLabel45.Text = "By :"
        '
        'tranaprvd_line_1
        '
        Me.tranaprvd_line_1.Location = New System.Drawing.Point(25, 100)
        Me.tranaprvd_line_1.Name = "tranaprvd_line_1"
        Me.tranaprvd_line_1.Size = New System.Drawing.Size(167, 9)
        '
        'tranaprvd_line_2
        '
        Me.tranaprvd_line_2.Location = New System.Drawing.Point(208, 100)
        Me.tranaprvd_line_2.Name = "tranaprvd_line_2"
        Me.tranaprvd_line_2.Size = New System.Drawing.Size(167, 9)
        '
        'tranaprvd_line_3
        '
        Me.tranaprvd_line_3.Location = New System.Drawing.Point(392, 100)
        Me.tranaprvd_line_3.Name = "tranaprvd_line_3"
        Me.tranaprvd_line_3.Size = New System.Drawing.Size(167, 9)
        '
        'tranaprvd_line_4
        '
        Me.tranaprvd_line_4.Location = New System.Drawing.Point(575, 100)
        Me.tranaprvd_line_4.Name = "tranaprvd_line_4"
        Me.tranaprvd_line_4.Size = New System.Drawing.Size(167, 9)
        '
        'XrLine2
        '
        Me.XrLine2.BorderWidth = 2
        Me.XrLine2.Location = New System.Drawing.Point(0, 0)
        Me.XrLine2.Name = "XrLine2"
        Me.XrLine2.Size = New System.Drawing.Size(742, 8)
        Me.XrLine2.StylePriority.UseBorderWidth = False
        '
        'XrLabel17
        '
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_name_1", "")})
        Me.XrLabel17.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel17.Location = New System.Drawing.Point(25, 83)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "XrLabel17"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_name_2", "")})
        Me.XrLabel18.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel18.Location = New System.Drawing.Point(208, 83)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel19
        '
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_name_3", "")})
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel19.Location = New System.Drawing.Point(392, 83)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel20
        '
        Me.XrLabel20.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_name_4", "")})
        Me.XrLabel20.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(575, 83)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.StylePriority.UseTextAlignment = False
        Me.XrLabel20.Text = "XrLabel20"
        Me.XrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel21
        '
        Me.XrLabel21.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_pos_1", "")})
        Me.XrLabel21.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel21.Location = New System.Drawing.Point(25, 108)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "XrLabel21"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel22
        '
        Me.XrLabel22.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_pos_2", "")})
        Me.XrLabel22.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel22.Location = New System.Drawing.Point(208, 108)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.StylePriority.UseTextAlignment = False
        Me.XrLabel22.Text = "XrLabel22"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel23
        '
        Me.XrLabel23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_pos_3", "")})
        Me.XrLabel23.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel23.Location = New System.Drawing.Point(392, 108)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "XrLabel23"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel24
        '
        Me.XrLabel24.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SQ.tranaprvd_pos_4", "")})
        Me.XrLabel24.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel24.Location = New System.Drawing.Point(575, 108)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.StylePriority.UseTextAlignment = False
        Me.XrLabel24.Text = "XrLabel24"
        Me.XrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'PgSqlDataTable1
        '
        'Me.PgSqlDataTable1.Connection = Me.PgSqlConnection1
        'Me.PgSqlDataTable1.Name = "PgSqlDataTable1"
        'Me.PgSqlDataTable1.SelectCommand = Me.pgSqlSelectCommand2
        'Me.PgSqlDataTable1.TableName = "PgSqlDataTable1"
        'Me.PgSqlDataTable1.Owner = Me
        '
        'pgSqlSelectCommand2
        '
        'Me.pgSqlSelectCommand2.CommandText = resources.GetString("pgSqlSelectCommand2.CommandText")
        'Me.pgSqlSelectCommand2.Connection = Me.PgSqlConnection1
        'Me.pgSqlSelectCommand2.Name = "pgSqlSelectCommand2"
        'Me.pgSqlSelectCommand2.Owner = Me
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel25.Location = New System.Drawing.Point(0, 83)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(83, 17)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = "Order From :"
        '
        'XrTable1
        '
        Me.XrTable1.Location = New System.Drawing.Point(0, 108)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1, Me.XrTableRow4, Me.XrTableRow3, Me.XrTableRow2, Me.XrTableRow7})
        Me.XrTable1.Size = New System.Drawing.Size(300, 82)
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Data Table 1.ptnr_name", "")})
        Me.XrTableCell1.Font = New System.Drawing.Font("Times New Roman", 6.75!)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.Text = "XrTableCell1"
        Me.XrTableCell1.Weight = 3
        '
        'XrTableRow4
        '
        Me.XrTableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4})
        Me.XrTableRow4.Name = "XrTableRow4"
        Me.XrTableRow4.Weight = 1
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Data Table 1.ptnra_line_1", "")})
        Me.XrTableCell4.Font = New System.Drawing.Font("Times New Roman", 6.75!)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.Text = "XrTableCell4"
        Me.XrTableCell4.Weight = 3
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Data Table 1.ptnra_line_2", "")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Times New Roman", 6.75!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.Text = "XrTableCell3"
        Me.XrTableCell3.Weight = 3
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell2})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Data Table 1.ptnra_line_3", "")})
        Me.XrTableCell2.Font = New System.Drawing.Font("Times New Roman", 6.75!)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.Text = "XrTableCell2"
        Me.XrTableCell2.Weight = 3
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell11})
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.Weight = 1
        '
        'XrTableCell11
        '
        Me.XrTableCell11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Data Table 1.ptnrac_contact_name", "")})
        Me.XrTableCell11.Font = New System.Drawing.Font("Times New Roman", 6.75!)
        Me.XrTableCell11.Name = "XrTableCell11"
        Me.XrTableCell11.StylePriority.UseFont = False
        Me.XrTableCell11.Text = "XrTableCell11"
        Me.XrTableCell11.Weight = 3
        '
        'XRSQ
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.GroupFooter1, Me.Detail, Me.GroupHeader1})
        Me.DataAdapter = Me.PgSqlDataAdapter1
        Me.DataMember = "SQ"
        Me.Margins = New System.Drawing.Printing.Margins(40, 40, 60, 60)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5Rotated
        Me.Version = "9.2"
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.PgSqlDataTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PgSqlDataAdapter1 As npgsql.nPgSqlDataAdapter
    Friend WithEvents pgSqlSelectCommand1 As npgsql.nPgSqlCommand
    Friend WithEvents PgSqlConnection1 As npgsql.nPgSqlConnection
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow10 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents XrLabel45 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents tranaprvd_line_1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents tranaprvd_line_2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents tranaprvd_line_3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents tranaprvd_line_4 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLine2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents PgSqlDataTable1 As CoreLab.PostgreSql.PgSqlDataTable
    'Friend WithEvents pgSqlSelectCommand2 As CoreLab.PostgreSql.PgSqlCommand
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell11 As DevExpress.XtraReports.UI.XRTableCell
    'Friend WithEvents DataSet31 As sygma_solution_system.DataSet3
End Class
