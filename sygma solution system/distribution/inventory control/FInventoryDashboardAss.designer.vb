<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryDashboardAss
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
        Me.xtc_ida = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_ar = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_ar = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.DsaragingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.Ds_ar_aging = New sygma_solution_system.ds_ar_aging
        Me.xtp_ars = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_ars = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        'Me.DataTable1TableAdapter = New sygma_solution_system.ds_general_ledger_printTableAdapters.DataTable1TableAdapter
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LookUpEdit2 = New DevExpress.XtraEditors.LookUpEdit
        Me.CBChild = New DevExpress.XtraEditors.CheckEdit
        Me.CheckEdit2 = New DevExpress.XtraEditors.CheckEdit
        'Me.DataTable1TableAdapter1 = New sygma_solution_system.ds_soTableAdapters.DataTable1TableAdapter
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_1.SuspendLayout()
        CType(Me.xtc_ida, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_ida.SuspendLayout()
        Me.xtp_ar.SuspendLayout()
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_ars.SuspendLayout()
        CType(Me.pgc_ars, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CBChild.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CheckEdit2)
        Me.scc_master.Panel1.Controls.Add(Me.CBChild)
        Me.scc_master.Panel1.Controls.Add(Me.LookUpEdit2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1013, 433)
        Me.scc_master.SplitterPosition = 53
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_1
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(1013, 374)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.xtc_ida)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(1011, 373)
        Me.xtp_1.Text = "Data"
        '
        'xtc_ida
        '
        Me.xtc_ida.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_ida.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_ida.Location = New System.Drawing.Point(0, 0)
        Me.xtc_ida.Name = "xtc_ida"
        Me.xtc_ida.SelectedTabPage = Me.xtp_ar
        Me.xtc_ida.Size = New System.Drawing.Size(1011, 373)
        Me.xtc_ida.TabIndex = 3
        Me.xtc_ida.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_ar, Me.xtp_ars})
        '
        'xtp_ar
        '
        Me.xtp_ar.Controls.Add(Me.pgc_ar)
        Me.xtp_ar.Name = "xtp_ar"
        Me.xtp_ar.Size = New System.Drawing.Size(1004, 344)
        Me.xtp_ar.Text = "Stock Cover W/O PS"
        '
        'pgc_ar
        '
        Me.pgc_ar.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_ar.DataMember = "DataTable1"
        Me.pgc_ar.DataSource = Me.DsaragingBindingSource
        Me.pgc_ar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_ar.Location = New System.Drawing.Point(0, 0)
        Me.pgc_ar.Name = "pgc_ar"
        Me.pgc_ar.Size = New System.Drawing.Size(1004, 344)
        Me.pgc_ar.TabIndex = 6
        '
        'DsaragingBindingSource
        '
        'Me.DsaragingBindingSource.DataSource = Me.Ds_ar_aging
        Me.DsaragingBindingSource.Position = 0
        '
        'Ds_ar_aging
        '
        'Me.Ds_ar_aging.DataSetName = "ds_ar_aging"
        'Me.Ds_ar_aging.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'xtp_ars
        '
        Me.xtp_ars.Controls.Add(Me.pgc_ars)
        Me.xtp_ars.Name = "xtp_ars"
        Me.xtp_ars.Size = New System.Drawing.Size(1004, 344)
        Me.xtp_ars.Text = "Stock Cover PS"
        '
        'pgc_ars
        '
        Me.pgc_ars.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_ars.DataMember = "DataTable1"
        Me.pgc_ars.DataSource = Me.DsaragingBindingSource
        Me.pgc_ars.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_ars.Location = New System.Drawing.Point(0, 0)
        Me.pgc_ars.Name = "pgc_ars"
        Me.pgc_ars.Size = New System.Drawing.Size(1004, 344)
        Me.pgc_ars.TabIndex = 3
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(222, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 27
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(170, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 26
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(59, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 25
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(3, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(50, 13)
        Me.LabelControl1.TabIndex = 24
        Me.LabelControl1.Text = "Start Date"
        '
        'DataTable1TableAdapter
        '
        'Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(396, 3)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window
        Me.pr_entity.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.pr_entity.Properties.Appearance.Options.UseBackColor = True
        Me.pr_entity.Properties.Appearance.Options.UseForeColor = True
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(193, 20)
        Me.pr_entity.TabIndex = 28
        '
        'LookUpEdit2
        '
        Me.LookUpEdit2.Location = New System.Drawing.Point(396, 29)
        Me.LookUpEdit2.Name = "LookUpEdit2"
        Me.LookUpEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LookUpEdit2.Size = New System.Drawing.Size(193, 20)
        Me.LookUpEdit2.TabIndex = 29
        '
        'CBChild
        '
        Me.CBChild.Location = New System.Drawing.Point(595, 4)
        Me.CBChild.Name = "CBChild"
        Me.CBChild.Properties.Caption = "With All Child"
        Me.CBChild.Size = New System.Drawing.Size(111, 19)
        Me.CBChild.TabIndex = 30
        '
        'CheckEdit2
        '
        Me.CheckEdit2.Location = New System.Drawing.Point(595, 30)
        Me.CheckEdit2.Name = "CheckEdit2"
        Me.CheckEdit2.Properties.Caption = "With Location"
        Me.CheckEdit2.Size = New System.Drawing.Size(111, 19)
        Me.CheckEdit2.TabIndex = 31
        '
        'DataTable1TableAdapter1
        '
        'Me.DataTable1TableAdapter1.ClearBeforeFill = True
        '
        'FInventoryDashboardAss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1013, 433)
        Me.Name = "FInventoryDashboardAss"
        Me.Text = "Stock Cover Analisys"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_1.ResumeLayout(False)
        CType(Me.xtc_ida, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_ida.ResumeLayout(False)
        Me.xtp_ar.ResumeLayout(False)
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_ars.ResumeLayout(False)
        CType(Me.pgc_ars, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LookUpEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CBChild.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents DsaragingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Ds_ar_aging As sygma_solution_system.ds_ar_aging
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_general_ledger_printTableAdapters.DataTable1TableAdapter
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LookUpEdit2 As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents CheckEdit2 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CBChild As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents xtc_ida As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_ar As DevExpress.XtraTab.XtraTabPage
    'Friend WithEvents DataTable1TableAdapter1 As sygma_solution_system.ds_soTableAdapters.DataTable1TableAdapter
    Friend WithEvents xtp_ars As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pgc_ars As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents pgc_ar As DevExpress.XtraPivotGrid.PivotGridControl

End Class
