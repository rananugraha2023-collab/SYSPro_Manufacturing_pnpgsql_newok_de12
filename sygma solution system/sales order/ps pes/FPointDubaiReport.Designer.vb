<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPointDubaiReport
    Inherits master_new.MasterInfTwo

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_1 = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_ar = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.DsaragingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.Ds_ar_aging = New sygma_solution_system.ds_ar_aging
        Me.fieldpointdubai = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label1 = New System.Windows.Forms.Label
        Me.en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_1.SuspendLayout()
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.en_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1013, 433)
        Me.scc_master.SplitterPosition = 33
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_1
        Me.xtc_master.Size = New System.Drawing.Size(1013, 394)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1, Me.XtraTabPage1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.pgc_ar)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(1011, 373)
        Me.xtp_1.Text = "Data"
        '
        'pgc_ar
        '
        Me.pgc_ar.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_ar.DataMember = "DataTable1"
        Me.pgc_ar.DataSource = Me.DsaragingBindingSource
        Me.pgc_ar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_ar.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldpointdubai, Me.PivotGridField1, Me.PivotGridField5, Me.PivotGridField2, Me.PivotGridField3})
        Me.pgc_ar.Location = New System.Drawing.Point(0, 0)
        Me.pgc_ar.Name = "pgc_ar"
        Me.pgc_ar.Size = New System.Drawing.Size(1011, 373)
        Me.pgc_ar.TabIndex = 0
        '
        'DsaragingBindingSource
        '
        Me.DsaragingBindingSource.DataSource = Nothing 'Me.Ds_ar_aging
        Me.DsaragingBindingSource.Position = 0
        '
        'Ds_ar_aging
        '
        'Me.Ds_ar_aging.DataSetName = "ds_ar_aging"
        'Me.Ds_ar_aging.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'fieldpointdubai
        '
        Me.fieldpointdubai.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldpointdubai.AreaIndex = 0
        Me.fieldpointdubai.Caption = "Point"
        Me.fieldpointdubai.CellFormat.FormatString = "n"
        Me.fieldpointdubai.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldpointdubai.FieldName = "point_dubai"
        Me.fieldpointdubai.GrandTotalCellFormat.FormatString = "n"
        Me.fieldpointdubai.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldpointdubai.Name = "fieldpointdubai"
        Me.fieldpointdubai.TotalCellFormat.FormatString = "n"
        Me.fieldpointdubai.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldpointdubai.TotalValueFormat.FormatString = "n"
        Me.fieldpointdubai.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldpointdubai.ValueFormat.FormatString = "n"
        Me.fieldpointdubai.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 1
        Me.PivotGridField1.Caption = "Month"
        Me.PivotGridField1.FieldName = "so_date"
        Me.PivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.UnboundFieldName = "PivotGridField1"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Product"
        Me.PivotGridField5.FieldName = "pt_desc1"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField2.AreaIndex = 0
        Me.PivotGridField2.Caption = "Year"
        Me.PivotGridField2.FieldName = "so_date"
        Me.PivotGridField2.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.UnboundFieldName = "PivotGridField2"
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.gc_detail)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1011, 373)
        Me.XtraTabPage1.Text = "Top Point"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(1011, 373)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Entity"
        '
        'en_id
        '
        Me.en_id.Location = New System.Drawing.Point(52, 6)
        Me.en_id.Name = "en_id"
        Me.en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.en_id.Size = New System.Drawing.Size(164, 20)
        Me.en_id.TabIndex = 29
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(467, 6)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 33
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(413, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 32
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(289, 6)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 31
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(234, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 30
        Me.LabelControl1.Text = "First Date"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Sales"
        Me.PivotGridField3.FieldName = "so_sales"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'FPointDubaiReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1013, 433)
        Me.Name = "FPointDubaiReport"
        Me.Text = "Point Dubai Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_1.ResumeLayout(False)
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pgc_ar As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents DsaragingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Ds_ar_aging As sygma_solution_system.ds_ar_aging
    Friend WithEvents fieldpointdubai As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Public WithEvents en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField

End Class
