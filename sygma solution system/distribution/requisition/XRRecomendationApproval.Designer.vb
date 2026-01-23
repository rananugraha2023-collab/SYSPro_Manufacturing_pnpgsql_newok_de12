<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class XRRecomendationApproval
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(XRRecomendationApproval))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel20 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel
        Me.rec_date = New DevExpress.XtraReports.UI.XRLabel
        Me.xrpb_logo = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine
        Me.XrLabel45 = New DevExpress.XtraReports.UI.XRLabel
        Me.cf_pt_description = New DevExpress.XtraReports.UI.CalculatedField
        'Me.Dsrec1 = New sygma_solution_system.dsrec
        Me.cf_reqd_cost_ext = New DevExpress.XtraReports.UI.CalculatedField
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand
        Me.rec_code = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel12 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrPageInfo4 = New DevExpress.XtraReports.UI.XRPageInfo
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.cf_user = New DevExpress.XtraReports.UI.CalculatedField
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand
        Me.XrPictureBox1 = New DevExpress.XtraReports.UI.XRPictureBox
        Me.XrSubreport1 = New DevExpress.XtraReports.UI.XRSubreport
        Me.XrRequisitionApprovalSub1 = New sygma_solution_system.XRRequisitionApprovalSub
        'CType(Me.Dsrec1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrRequisitionApprovalSub1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.Height = 0
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("req_code", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel22
        '
        Me.XrLabel22.Dpi = 254.0!
        Me.XrLabel22.Location = New System.Drawing.Point(169, 278)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel22.Size = New System.Drawing.Size(20, 43)
        Me.XrLabel22.Text = ":"
        '
        'XrLabel21
        '
        Me.XrLabel21.Dpi = 254.0!
        Me.XrLabel21.Location = New System.Drawing.Point(169, 235)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel21.Size = New System.Drawing.Size(20, 43)
        Me.XrLabel21.Text = ":"
        '
        'XrLabel20
        '
        Me.XrLabel20.Dpi = 254.0!
        Me.XrLabel20.Location = New System.Drawing.Point(169, 190)
        Me.XrLabel20.Name = "XrLabel20"
        Me.XrLabel20.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel20.Size = New System.Drawing.Size(20, 43)
        Me.XrLabel20.Text = ":"
        '
        'XrLabel19
        '
        Me.XrLabel19.Dpi = 254.0!
        Me.XrLabel19.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel19.Location = New System.Drawing.Point(402, 0)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel19.Size = New System.Drawing.Size(742, 64)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "RECOMENDATION"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel5
        '
        Me.XrLabel5.Dpi = 254.0!
        Me.XrLabel5.Location = New System.Drawing.Point(21, 278)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel5.Size = New System.Drawing.Size(147, 43)
        Me.XrLabel5.Text = "User"
        '
        'XrLabel4
        '
        Me.XrLabel4.Dpi = 254.0!
        Me.XrLabel4.Location = New System.Drawing.Point(21, 235)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel4.Size = New System.Drawing.Size(147, 43)
        Me.XrLabel4.Text = "Entity"
        '
        'XrLabel3
        '
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Location = New System.Drawing.Point(21, 190)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.Size = New System.Drawing.Size(147, 43)
        Me.XrLabel3.Text = "Date"
        '
        'XrLabel2
        '
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.en_desc", "{0:dd/MM/yyyy}")})
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Location = New System.Drawing.Point(190, 235)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.Size = New System.Drawing.Size(720, 42)
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "XrLabel2"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'rec_date
        '
        Me.rec_date.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.rec_date", "{0:MM/dd/yyyy}")})
        Me.rec_date.Dpi = 254.0!
        Me.rec_date.Location = New System.Drawing.Point(190, 190)
        Me.rec_date.Name = "rec_date"
        Me.rec_date.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.rec_date.Size = New System.Drawing.Size(720, 42)
        Me.rec_date.StylePriority.UseTextAlignment = False
        Me.rec_date.Text = "rec_date"
        Me.rec_date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'xrpb_logo
        '
        Me.xrpb_logo.Dpi = 254.0!
        Me.xrpb_logo.Location = New System.Drawing.Point(21, 0)
        Me.xrpb_logo.Name = "xrpb_logo"
        Me.xrpb_logo.Size = New System.Drawing.Size(296, 169)
        Me.xrpb_logo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'XrLine1
        '
        Me.XrLine1.Dpi = 254.0!
        Me.XrLine1.LineWidth = 3
        Me.XrLine1.Location = New System.Drawing.Point(21, 0)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.Size = New System.Drawing.Size(1270, 24)
        '
        'XrLabel45
        '
        Me.XrLabel45.Dpi = 254.0!
        Me.XrLabel45.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.XrLabel45.Location = New System.Drawing.Point(21, 42)
        Me.XrLabel45.Name = "XrLabel45"
        Me.XrLabel45.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel45.Size = New System.Drawing.Size(423, 42)
        Me.XrLabel45.StylePriority.UseFont = False
        Me.XrLabel45.Text = "Approved by :"
        '
        'cf_pt_description
        '
        Me.cf_pt_description.DataMember = "DataTable1"
        'Me.cf_pt_description.DataSource = Me.Dsrec1
        Me.cf_pt_description.Expression = "trim([pt_desc1] + ' ' + [pt_desc2])"
        Me.cf_pt_description.Name = "cf_pt_description"
        '
        'Dsrec1
        '
        'Me.Dsrec1.DataSetName = "dsrec"
        'Me.Dsrec1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cf_reqd_cost_ext
        '
        Me.cf_reqd_cost_ext.DataMember = "DataTable1"
        'Me.cf_reqd_cost_ext.DataSource = Me.Dsrec1
        Me.cf_reqd_cost_ext.Expression = "([reqd_cost] - ([reqd_cost]*[reqd_disc])) * [reqd_qty]"
        Me.cf_reqd_cost_ext.Name = "cf_reqd_cost_ext"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrpb_logo, Me.rec_date, Me.XrLabel2, Me.XrLabel3, Me.XrLabel4, Me.XrLabel5, Me.XrLabel19, Me.XrLabel20, Me.XrLabel21, Me.XrLabel22, Me.rec_code, Me.XrLabel11, Me.XrLabel12, Me.XrPageInfo4, Me.XrLabel7, Me.XrLabel8, Me.XrLabel1, Me.XrLabel6, Me.XrPictureBox1})
        Me.ReportHeader.Dpi = 254.0!
        Me.ReportHeader.Height = 1685
        Me.ReportHeader.Name = "ReportHeader"
        '
        'rec_code
        '
        Me.rec_code.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.rec_code", "")})
        Me.rec_code.Dpi = 254.0!
        Me.rec_code.Location = New System.Drawing.Point(402, 85)
        Me.rec_code.Name = "rec_code"
        Me.rec_code.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.rec_code.Size = New System.Drawing.Size(741, 42)
        '
        'XrLabel11
        '
        Me.XrLabel11.Dpi = 254.0!
        Me.XrLabel11.Location = New System.Drawing.Point(169, 323)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel11.Size = New System.Drawing.Size(20, 43)
        Me.XrLabel11.Text = ":"
        '
        'XrLabel12
        '
        Me.XrLabel12.Dpi = 254.0!
        Me.XrLabel12.Location = New System.Drawing.Point(21, 323)
        Me.XrLabel12.Name = "XrLabel12"
        Me.XrLabel12.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel12.Size = New System.Drawing.Size(147, 43)
        Me.XrLabel12.Text = "PR"
        '
        'XrPageInfo4
        '
        Me.XrPageInfo4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Tag", Nothing, "Table.rec_remarks", "")})
        Me.XrPageInfo4.Dpi = 254.0!
        Me.XrPageInfo4.Location = New System.Drawing.Point(190, 368)
        Me.XrPageInfo4.Name = "XrPageInfo4"
        Me.XrPageInfo4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrPageInfo4.PageInfo = DevExpress.XtraPrinting.PageInfo.None
        Me.XrPageInfo4.Size = New System.Drawing.Size(1122, 119)
        Me.XrPageInfo4.StylePriority.UseTextAlignment = False
        Me.XrPageInfo4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel7
        '
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Location = New System.Drawing.Point(21, 368)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.Size = New System.Drawing.Size(147, 43)
        Me.XrLabel7.Text = "Remarks"
        '
        'XrLabel8
        '
        Me.XrLabel8.Dpi = 254.0!
        Me.XrLabel8.Location = New System.Drawing.Point(169, 368)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel8.Size = New System.Drawing.Size(20, 43)
        Me.XrLabel8.Text = ":"
        '
        'XrLabel1
        '
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.emp_fname", "{0:dd/MM/yyyy}")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Location = New System.Drawing.Point(190, 278)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.Size = New System.Drawing.Size(720, 42)
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "XrLabel2"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "Table.rec_reff_code", "{0:dd/MM/yyyy}")})
        Me.XrLabel6.Dpi = 254.0!
        Me.XrLabel6.Location = New System.Drawing.Point(190, 323)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel6.Size = New System.Drawing.Size(720, 42)
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel2"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=visitama"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("rec_oid", "rec_oid"), New System.Data.Common.DataColumnMapping("rec_code", "rec_code"), New System.Data.Common.DataColumnMapping("rec_en_id", "rec_en_id"), New System.Data.Common.DataColumnMapping("en_desc", "en_desc"), New System.Data.Common.DataColumnMapping("rec_emp_id", "rec_emp_id"), New System.Data.Common.DataColumnMapping("emp_fname", "emp_fname"), New System.Data.Common.DataColumnMapping("emp_nik_old", "emp_nik_old"), New System.Data.Common.DataColumnMapping("rec_date", "rec_date"), New System.Data.Common.DataColumnMapping("rec_reff_code", "rec_reff_code"), New System.Data.Common.DataColumnMapping("rec_reff_oid", "rec_reff_oid"), New System.Data.Common.DataColumnMapping("rec_detail", "rec_detail"), New System.Data.Common.DataColumnMapping("rec_remarks", "rec_remarks"), New System.Data.Common.DataColumnMapping("rec_db_code", "rec_db_code")})})
        '
        'cf_user
        '
        Me.cf_user.DataMember = "Table"
        'Me.cf_user.DataSource = Me.Dsrec1
        Me.cf_user.Expression = "Concat([emp_nik_old],' ', [emp_fname] )"
        Me.cf_user.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.cf_user.Name = "cf_user"
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel45, Me.XrSubreport1})
        Me.ReportFooter.Dpi = 254.0!
        Me.ReportFooter.Height = 153
        Me.ReportFooter.Name = "ReportFooter"
        '
        'XrPictureBox1
        '
        Me.XrPictureBox1.Dpi = 254.0!
        Me.XrPictureBox1.Location = New System.Drawing.Point(21, 508)
        Me.XrPictureBox1.Name = "XrPictureBox1"
        Me.XrPictureBox1.Size = New System.Drawing.Size(1291, 1164)
        '
        'XrSubreport1
        '
        Me.XrSubreport1.Dpi = 254.0!
        Me.XrSubreport1.Location = New System.Drawing.Point(21, 85)
        Me.XrSubreport1.Name = "XrSubreport1"
        Me.XrSubreport1.ReportSource = Me.XrRequisitionApprovalSub1
        Me.XrSubreport1.Size = New System.Drawing.Size(1291, 21)
        '
        'XrRequisitionApprovalSub1
        '
        Me.XrRequisitionApprovalSub1.DataMember = "Table"
        Me.XrRequisitionApprovalSub1.Name = "XrRequisitionApprovalSub1"
        Me.XrRequisitionApprovalSub1.PageColor = System.Drawing.Color.White
        Me.XrRequisitionApprovalSub1.PageHeight = 1100
        Me.XrRequisitionApprovalSub1.PageWidth = 850
        Me.XrRequisitionApprovalSub1.Version = "9.2"
        '
        'XRRecomendationApproval
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.ReportHeader, Me.ReportFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.cf_pt_description, Me.cf_reqd_cost_ext, Me.cf_user})
        Me.DataAdapter = Me.OdbcDataAdapter1
        Me.DataMember = "Table"
        'Me.DataSource = Me.Dsrec1
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(76, 76, 75, 75)
        Me.PageHeight = 2101
        Me.PageWidth = 1481
        Me.PaperKind = System.Drawing.Printing.PaperKind.A5
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.Version = "9.1"
        'CType(Me.Dsrec1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrRequisitionApprovalSub1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents xrpb_logo As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel20 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents rec_date As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents cf_pt_description As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents cf_reqd_cost_ext As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrLabel45 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrSubreport1 As DevExpress.XtraReports.UI.XRSubreport
    Private WithEvents XrRequisitionApprovalSub1 As sygma_solution_system.XRRequisitionApprovalSub
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents rec_code As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel12 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo4 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    'Friend WithEvents Dsrec1 As sygma_solution_system.dsrec
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents cf_user As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Public WithEvents XrPictureBox1 As DevExpress.XtraReports.UI.XRPictureBox
End Class
