<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTaxExportReport
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
        Me.components = New System.ComponentModel.Container()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl()
        Me.xtp_view1 = New DevExpress.XtraTab.XtraTabPage()
        Me.gc_header = New DevExpress.XtraGrid.GridControl()
        Me.gv_header = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtp_view_det = New DevExpress.XtraTab.XtraTabPage()
        Me.gc_detail = New DevExpress.XtraGrid.GridControl()
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit()
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit()
        Me.le_syspro = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_view1.SuspendLayout()
        CType(Me.gc_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_header, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_view_det.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_syspro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.le_syspro)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(630, 433)
        Me.scc_master.SplitterPosition = 26
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(191, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 26
        Me.LabelControl2.Text = "Last Date"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(9, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(61, 13)
        Me.LabelControl1.TabIndex = 24
        Me.LabelControl1.Text = "AR Eff. Date"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_view1
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_master.Size = New System.Drawing.Size(630, 402)
        Me.xtc_master.TabIndex = 8
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_view1, Me.xtp_view_det})
        '
        'xtp_view1
        '
        Me.xtp_view1.Controls.Add(Me.gc_header)
        Me.xtp_view1.Name = "xtp_view1"
        Me.xtp_view1.Size = New System.Drawing.Size(628, 381)
        Me.xtp_view1.Text = "Faktur"
        '
        'gc_header
        '
        Me.gc_header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_header.Location = New System.Drawing.Point(0, 0)
        Me.gc_header.MainView = Me.gv_header
        Me.gc_header.Name = "gc_header"
        Me.gc_header.Size = New System.Drawing.Size(628, 381)
        Me.gc_header.TabIndex = 0
        Me.gc_header.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_header})
        '
        'gv_header
        '
        Me.gv_header.GridControl = Me.gc_header
        Me.gv_header.Name = "gv_header"
        '
        'xtp_view_det
        '
        Me.xtp_view_det.Controls.Add(Me.gc_detail)
        Me.xtp_view_det.Name = "xtp_view_det"
        Me.xtp_view_det.Size = New System.Drawing.Size(628, 381)
        Me.xtp_view_det.Text = "DetailFaktur"
        '
        'gc_detail
        '
        Me.gc_detail.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(628, 381)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(253, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 28
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(76, 4)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 29
        '
        'le_syspro
        '
        Me.le_syspro.Location = New System.Drawing.Point(452, 3)
        Me.le_syspro.Name = "le_syspro"
        Me.le_syspro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_syspro.Size = New System.Drawing.Size(100, 20)
        Me.le_syspro.TabIndex = 30
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(399, 6)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(33, 13)
        Me.LabelControl3.TabIndex = 31
        Me.LabelControl3.Text = "Syspro"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 48)
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExportToolStripMenuItem.Text = "Export Detail"
        '
        'FTaxExportReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(630, 433)
        Me.Name = "FTaxExportReport"
        Me.Text = "Tax Export Report"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_view1.ResumeLayout(False)
        CType(Me.gc_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_header, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_view_det.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_syspro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_view1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_header As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_header As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_view_det As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents le_syspro As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
