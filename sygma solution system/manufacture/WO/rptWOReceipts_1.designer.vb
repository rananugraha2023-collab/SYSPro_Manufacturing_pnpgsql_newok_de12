<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptWOReceipts_1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptWOReceipts_1))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand
        Me.xlabel_post1 = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign1 = New DevExpress.XtraReports.UI.XRLine
        Me.xl_by = New DevExpress.XtraReports.UI.XRLabel
        Me.xlabel_sign1 = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign2 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_sign2 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_1 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_2 = New DevExpress.XtraReports.UI.XRLabel
        Me.xlabel_post2 = New DevExpress.XtraReports.UI.XRLabel
        Me.cf_pt_description = New DevExpress.XtraReports.UI.CalculatedField
        Me.Ds_wo_receipt1 = New syspro_ver2.ds_wo_receipt
        Me.cf_det_pt_desc = New DevExpress.XtraReports.UI.CalculatedField
        Me.qty_open = New DevExpress.XtraReports.UI.CalculatedField
        Me.XrLabel25 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel26 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel24 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel28 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel30 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel32 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel33 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_post3 = New DevExpress.XtraReports.UI.XRLabel
        Me.xline_sign3 = New DevExpress.XtraReports.UI.XRLine
        Me.xlabel_sign3 = New DevExpress.XtraReports.UI.XRLabel
        Me.lbl_authorized_3 = New DevExpress.XtraReports.UI.XRLabel
        Me.DataTable1TableAdapter = New syspro_ver2.ds_wo_receiptTableAdapters.DataTable1TableAdapter
        CType(Me.Ds_wo_receipt1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Height = 0
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.PrintOnEmptyDataSource = False
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("wod_pt_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 0
        Me.GroupFooter1.KeepTogether = True
        Me.GroupFooter1.Name = "GroupFooter1"
        Me.GroupFooter1.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand
        '
        'xlabel_post1
        '
        Me.xlabel_post1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post1", "")})
        Me.xlabel_post1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post1.Location = New System.Drawing.Point(17, 423)
        Me.xlabel_post1.Name = "xlabel_post1"
        Me.xlabel_post1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post1.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post1.StylePriority.UseFont = False
        Me.xlabel_post1.StylePriority.UseTextAlignment = False
        Me.xlabel_post1.Text = "xlabel_post1"
        Me.xlabel_post1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xline_sign1
        '
        Me.xline_sign1.Location = New System.Drawing.Point(17, 417)
        Me.xline_sign1.Name = "xline_sign1"
        Me.xline_sign1.Size = New System.Drawing.Size(142, 9)
        '
        'xl_by
        '
        Me.xl_by.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xl_by.Location = New System.Drawing.Point(6, 404)
        Me.xl_by.Name = "xl_by"
        Me.xl_by.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xl_by.Size = New System.Drawing.Size(42, 17)
        Me.xl_by.StylePriority.UseFont = False
        Me.xl_by.Text = "By :"
        '
        'xlabel_sign1
        '
        Me.xlabel_sign1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign1", "")})
        Me.xlabel_sign1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign1.Location = New System.Drawing.Point(17, 404)
        Me.xlabel_sign1.Name = "xlabel_sign1"
        Me.xlabel_sign1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign1.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign1.StylePriority.UseFont = False
        Me.xlabel_sign1.StylePriority.UseTextAlignment = False
        Me.xlabel_sign1.Text = "xlabel_sign1"
        Me.xlabel_sign1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xline_sign2
        '
        Me.xline_sign2.Location = New System.Drawing.Point(200, 417)
        Me.xline_sign2.Name = "xline_sign2"
        Me.xline_sign2.Size = New System.Drawing.Size(142, 9)
        '
        'xlabel_sign2
        '
        Me.xlabel_sign2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign2", "")})
        Me.xlabel_sign2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign2.Location = New System.Drawing.Point(200, 404)
        Me.xlabel_sign2.Name = "xlabel_sign2"
        Me.xlabel_sign2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign2.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign2.StylePriority.UseFont = False
        Me.xlabel_sign2.StylePriority.UseTextAlignment = False
        Me.xlabel_sign2.Text = "[sign2]"
        Me.xlabel_sign2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_1
        '
        Me.lbl_authorized_1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_1.Location = New System.Drawing.Point(25, 338)
        Me.lbl_authorized_1.Name = "lbl_authorized_1"
        Me.lbl_authorized_1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_1.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_1.StylePriority.UseFont = False
        Me.lbl_authorized_1.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_1.Text = "Authorized Signature"
        Me.lbl_authorized_1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_2
        '
        Me.lbl_authorized_2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_2.Location = New System.Drawing.Point(208, 338)
        Me.lbl_authorized_2.Name = "lbl_authorized_2"
        Me.lbl_authorized_2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_2.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_2.StylePriority.UseFont = False
        Me.lbl_authorized_2.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_2.Text = "Authorized Signature"
        Me.lbl_authorized_2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xlabel_post2
        '
        Me.xlabel_post2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post2", "")})
        Me.xlabel_post2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post2.Location = New System.Drawing.Point(200, 423)
        Me.xlabel_post2.Name = "xlabel_post2"
        Me.xlabel_post2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post2.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post2.StylePriority.UseFont = False
        Me.xlabel_post2.StylePriority.UseTextAlignment = False
        Me.xlabel_post2.Text = "xlabel_post2"
        Me.xlabel_post2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'cf_pt_description
        '
        Me.cf_pt_description.DataMember = "DataTable1"
        Me.cf_pt_description.DataSource = Me.Ds_wo_receipt1
        Me.cf_pt_description.Expression = "trim([pt_desc1] + ' ' + [pt_desc2])"
        Me.cf_pt_description.Name = "cf_pt_description"
        '
        'Ds_wo_receipt1
        '
        Me.Ds_wo_receipt1.DataSetName = "ds_wo_receipt"
        Me.Ds_wo_receipt1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cf_det_pt_desc
        '
        Me.cf_det_pt_desc.DataMember = "DataTable1"
        Me.cf_det_pt_desc.DataSource = Me.Ds_wo_receipt1
        Me.cf_det_pt_desc.Expression = "trim([ptbomdesc] + ' ' + [ptbomdesc2])"
        Me.cf_det_pt_desc.Name = "cf_det_pt_desc"
        '
        'qty_open
        '
        Me.qty_open.DataMember = "DataTable1"
        Me.qty_open.DataSource = Me.Ds_wo_receipt1
        Me.qty_open.Expression = "[wod_qty_req] - [wod_qty_issued]"
        Me.qty_open.FieldType = DevExpress.XtraReports.UI.FieldType.[Double]
        Me.qty_open.Name = "qty_open"
        '
        'XrLabel25
        '
        Me.XrLabel25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel25.Location = New System.Drawing.Point(617, 59)
        Me.XrLabel25.Name = "XrLabel25"
        Me.XrLabel25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel25.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel25.StylePriority.UseFont = False
        Me.XrLabel25.Text = ":"
        '
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel22.Location = New System.Drawing.Point(500, 59)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(114, 17)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "Efective Date"
        '
        'XrLabel18
        '
        Me.XrLabel18.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wor_date_eff", "{0:dd/MM/yyyy}")})
        Me.XrLabel18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel18.Location = New System.Drawing.Point(627, 59)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "XrLabel18"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel16
        '
        Me.XrLabel16.CanShrink = True
        Me.XrLabel16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel16.Location = New System.Drawing.Point(12, 227)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.Size = New System.Drawing.Size(77, 17)
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.Text = "UM"
        '
        'XrLabel12
        '
        Me.XrLabel12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel12.Location = New System.Drawing.Point(96, 227)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(9, 17)
        Me.XrLabel12.StylePriority.UseFont = False
        Me.XrLabel12.Text = ":"
        '
        'XrLabel11
        '
        Me.XrLabel11.CanShrink = True
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.unit_measure", "{0:n}")})
        Me.XrLabel11.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel11.Location = New System.Drawing.Point(110, 227)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(76, 17)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "XrLabel11"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel8.Location = New System.Drawing.Point(96, 207)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(9, 17)
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.Text = ":"
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel7.Location = New System.Drawing.Point(96, 246)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = ":"
        '
        'XrLabel27
        '
        Me.XrLabel27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel27.Location = New System.Drawing.Point(500, 93)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel27.Size = New System.Drawing.Size(114, 17)
        Me.XrLabel27.StylePriority.UseFont = False
        Me.XrLabel27.Text = "Page"
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel6.Location = New System.Drawing.Point(617, 93)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = ":"
        Me.XrLabel6.Visible = False
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo2.Location = New System.Drawing.Point(627, 93)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.Size = New System.Drawing.Size(116, 17)
        Me.XrPageInfo2.StylePriority.UseFont = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel26
        '
        Me.XrLabel26.CanShrink = True
        Me.XrLabel26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wor_qty_comp", "{0:n}")})
        Me.XrLabel26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel26.Location = New System.Drawing.Point(110, 207)
        Me.XrLabel26.Name = "XrLabel26"
        Me.XrLabel26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel26.Size = New System.Drawing.Size(76, 17)
        Me.XrLabel26.StylePriority.UseFont = False
        Me.XrLabel26.StylePriority.UseTextAlignment = False
        Me.XrLabel26.Text = "XrLabel26"
        Me.XrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel14
        '
        Me.XrLabel14.CanShrink = True
        Me.XrLabel14.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel14.Location = New System.Drawing.Point(12, 207)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.Size = New System.Drawing.Size(77, 17)
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.Text = "Qty Receipt"
        '
        'XrLabel13
        '
        Me.XrLabel13.CanShrink = True
        Me.XrLabel13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel13.Location = New System.Drawing.Point(12, 246)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.Size = New System.Drawing.Size(77, 17)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.Text = "WO Number "
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Image = CType(resources.GetObject("XrPictureBox1.Image"), System.Drawing.Image)
        Me.XrPictureBox1.Location = New System.Drawing.Point(8, 0)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.Size = New System.Drawing.Size(133, 117)
        Me.XrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        Me.XrPictureBox1.Visible = False
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wor_code", "")})
        Me.XrLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.Location = New System.Drawing.Point(627, 25)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wor_date", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.Location = New System.Drawing.Point(627, 42)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(116, 17)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrPageInfo1.Format = "{0:dd/MM/yyyy}"
        Me.XrPageInfo1.Location = New System.Drawing.Point(627, 76)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo1.Size = New System.Drawing.Size(116, 17)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel3.Location = New System.Drawing.Point(500, 25)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(114, 17)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "WO Receipt Number"
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel4.Location = New System.Drawing.Point(500, 42)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(114, 17)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "WO Receipt Date"
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel5.Location = New System.Drawing.Point(500, 76)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(114, 17)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = "Print Date"
        '
        'XrLabel19
        '
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel19.Location = New System.Drawing.Point(542, 0)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(201, 25)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "WORK ORDER RECEIPTS"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel20
        '
        Me.XrLabel20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel20.Location = New System.Drawing.Point(617, 25)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel20.StylePriority.UseFont = False
        Me.XrLabel20.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel21.Location = New System.Drawing.Point(617, 42)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.Text = ":"
        '
        'XrLabel23
        '
        Me.XrLabel23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel23.Location = New System.Drawing.Point(617, 76)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.Text = ":"
        Me.XrLabel23.Visible = False
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wo_code", "")})
        Me.XrLabel9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel9.Location = New System.Drawing.Point(110, 246)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.Size = New System.Drawing.Size(175, 17)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "XrLabel9"
        '
        'XrLabel15
        '
        Me.XrLabel15.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_desc1", "")})
        Me.XrLabel15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel15.Location = New System.Drawing.Point(12, 180)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.Size = New System.Drawing.Size(283, 17)
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.Text = "XrLabel15"
        '
        'XrLabel24
        '
        Me.XrLabel24.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel24.Location = New System.Drawing.Point(12, 143)
        Me.XrLabel24.Name = "XrLabel24"
        Me.XrLabel24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel24.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel24.StylePriority.UseFont = False
        Me.XrLabel24.Text = "Part Description :"
        '
        'XrLabel17
        '
        Me.XrLabel17.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wor_remarks", "")})
        Me.XrLabel17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel17.Location = New System.Drawing.Point(110, 266)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.Size = New System.Drawing.Size(633, 17)
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.Text = "XrLabel17"
        '
        'XrLabel28
        '
        Me.XrLabel28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel28.Location = New System.Drawing.Point(12, 266)
        Me.XrLabel28.Name = "XrLabel28"
        Me.XrLabel28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel28.Size = New System.Drawing.Size(77, 17)
        Me.XrLabel28.StylePriority.UseFont = False
        Me.XrLabel28.Text = "Remarks "
        '
        'XrLabel30
        '
        Me.XrLabel30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XrLabel30.Location = New System.Drawing.Point(96, 266)
        Me.XrLabel30.Name = "XrLabel30"
        Me.XrLabel30.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel30.Size = New System.Drawing.Size(8, 17)
        Me.XrLabel30.StylePriority.UseFont = False
        Me.XrLabel30.Text = ":"
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.pt_code", "")})
        Me.XrLabel10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel10.Location = New System.Drawing.Point(12, 164)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.Size = New System.Drawing.Size(283, 17)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "XrLabel10"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel10, Me.XrLabel30, Me.XrLabel28, Me.XrLabel17, Me.XrLabel24, Me.XrLabel15, Me.XrLabel9, Me.XrLabel23, Me.XrLabel21, Me.XrLabel20, Me.XrLabel19, Me.XrLabel5, Me.XrLabel4, Me.XrLabel3, Me.XrPageInfo1, Me.XrLabel2, Me.XrLabel1, Me.XrPictureBox1, Me.XrLabel13, Me.XrLabel14, Me.XrLabel26, Me.XrPageInfo2, Me.XrLabel6, Me.XrLabel27, Me.XrLabel7, Me.XrLabel8, Me.XrLabel11, Me.XrLabel12, Me.XrLabel16, Me.XrLabel18, Me.XrLabel22, Me.XrLabel25, Me.XrLabel29, Me.XrLabel31, Me.XrLabel32, Me.XrLabel33, Me.xlabel_post1, Me.lbl_authorized_2, Me.lbl_authorized_1, Me.xlabel_sign2, Me.xline_sign2, Me.xlabel_sign1, Me.xl_by, Me.xline_sign1, Me.xlabel_post2, Me.XrLine1, Me.xlabel_post3, Me.xline_sign3, Me.xlabel_sign3, Me.lbl_authorized_3})
        Me.GroupHeader1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("wor_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.Height = 468
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
        Me.GroupHeader1.StylePriority.UseFont = False
        '
        'XrLabel29
        '
        Me.XrLabel29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel29.Location = New System.Drawing.Point(330, 143)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel29.Size = New System.Drawing.Size(167, 17)
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.Text = "Location :"
        '
        'XrLabel31
        '
        Me.XrLabel31.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel31.Location = New System.Drawing.Point(559, 143)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel31.Size = New System.Drawing.Size(180, 17)
        Me.XrLabel31.StylePriority.UseFont = False
        Me.XrLabel31.Text = "Work Center :"
        '
        'XrLabel32
        '
        Me.XrLabel32.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.loc_desc", "")})
        Me.XrLabel32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel32.Location = New System.Drawing.Point(330, 164)
        Me.XrLabel32.Name = "XrLabel32"
        Me.XrLabel32.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel32.Size = New System.Drawing.Size(210, 17)
        Me.XrLabel32.StylePriority.UseFont = False
        Me.XrLabel32.Text = "XrLabel32"
        '
        'XrLabel33
        '
        Me.XrLabel33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.wc_desc", "")})
        Me.XrLabel33.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel33.Location = New System.Drawing.Point(559, 164)
        Me.XrLabel33.Name = "XrLabel33"
        Me.XrLabel33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel33.Size = New System.Drawing.Size(180, 17)
        Me.XrLabel33.StylePriority.UseFont = False
        Me.XrLabel33.Text = "XrLabel33"
        '
        'XrLine1
        '
        Me.XrLine1.Location = New System.Drawing.Point(12, 294)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(735, 9)
        '
        'xlabel_post3
        '
        Me.xlabel_post3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.post3", "")})
        Me.xlabel_post3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_post3.Location = New System.Drawing.Point(392, 425)
        Me.xlabel_post3.Name = "xlabel_post3"
        Me.xlabel_post3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_post3.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_post3.StylePriority.UseFont = False
        Me.xlabel_post3.StylePriority.UseTextAlignment = False
        Me.xlabel_post3.Text = "xlabel_post3"
        Me.xlabel_post3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xline_sign3
        '
        Me.xline_sign3.Location = New System.Drawing.Point(392, 419)
        Me.xline_sign3.Name = "xline_sign3"
        Me.xline_sign3.Size = New System.Drawing.Size(142, 9)
        '
        'xlabel_sign3
        '
        Me.xlabel_sign3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DataTable1.sign3", "")})
        Me.xlabel_sign3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.xlabel_sign3.Location = New System.Drawing.Point(392, 406)
        Me.xlabel_sign3.Name = "xlabel_sign3"
        Me.xlabel_sign3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xlabel_sign3.Size = New System.Drawing.Size(142, 17)
        Me.xlabel_sign3.StylePriority.UseFont = False
        Me.xlabel_sign3.StylePriority.UseTextAlignment = False
        Me.xlabel_sign3.Text = "xlabel_sign3"
        Me.xlabel_sign3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lbl_authorized_3
        '
        Me.lbl_authorized_3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_authorized_3.Location = New System.Drawing.Point(400, 340)
        Me.lbl_authorized_3.Name = "lbl_authorized_3"
        Me.lbl_authorized_3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lbl_authorized_3.Size = New System.Drawing.Size(133, 17)
        Me.lbl_authorized_3.StylePriority.UseFont = False
        Me.lbl_authorized_3.StylePriority.UseTextAlignment = False
        Me.lbl_authorized_3.Text = "Authorized Signature"
        Me.lbl_authorized_3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'DataTable1TableAdapter
        '
        Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'rptWOReceipts_1
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupHeader1, Me.GroupFooter1})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_pt_description, Me.cf_det_pt_desc, Me.qty_open})
        Me.DataAdapter = Me.DataTable1TableAdapter
        Me.DataMember = "DataTable1"
        Me.DataSource = Me.Ds_wo_receipt1
        Me.DrawGrid = False
        Me.GridSize = New System.Drawing.Size(2, 2)
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(30, 30, 50, 60)
        Me.PageHeight = 583
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.Version = "9.1"
        CType(Me.Ds_wo_receipt1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents cf_pt_description As DevExpress.XtraReports.UI.CalculatedField
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_woTableAdapters.DataTable1TableAdapter
    'Friend WithEvents Ds_wo1 As sygma_solution_system.ds_wo
    Friend WithEvents cf_det_pt_desc As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents qty_open As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents xlabel_post2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xlabel_post1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xl_by As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xlabel_sign1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign2 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_sign2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_2 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents Ds_wo_receipt_print1 As syspro_ver2.ds_wo_receipt_print
    'Friend WithEvents DataTable1TableAdapter As syspro_ver2.ds_wo_receipt_printTableAdapters.DataTable1TableAdapter
    Friend WithEvents XrLabel25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel30 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel32 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents Ds_wo_receipt1 As syspro_ver2.ds_wo_receipt
    Friend WithEvents xlabel_post3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xline_sign3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents xlabel_sign3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lbl_authorized_3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DataTable1TableAdapter As syspro_ver2.ds_wo_receiptTableAdapters.DataTable1TableAdapter
    'Friend WithEvents DsWOPickList1 As syspro_ver2.dsWOPickList
End Class
