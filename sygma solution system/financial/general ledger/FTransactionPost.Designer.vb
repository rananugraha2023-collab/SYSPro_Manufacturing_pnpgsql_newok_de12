<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTransactionPost
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
        Me.xtp_unposted = New DevExpress.XtraTab.XtraTabPage
        Me.gc_unposted = New DevExpress.XtraGrid.GridControl
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CheckTheTransactionIsNotBalancedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ApproveCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gv_unposted = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_posted = New DevExpress.XtraTab.XtraTabPage
        Me.gc_posted = New DevExpress.XtraGrid.GridControl
        Me.gv_posted = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_all = New DevExpress.XtraTab.XtraTabPage
        Me.gc_all = New DevExpress.XtraGrid.GridControl
        Me.gv_all = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.sb_posting = New DevExpress.XtraEditors.SimpleButton
        Me.ce_all = New DevExpress.XtraEditors.CheckEdit
        Me.LblStatus = New DevExpress.XtraEditors.LabelControl
        Me.CeMassive = New DevExpress.XtraEditors.CheckEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_unposted.SuspendLayout()
        CType(Me.gc_unposted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gv_unposted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_posted.SuspendLayout()
        CType(Me.gc_posted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_posted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_all.SuspendLayout()
        CType(Me.gc_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_all.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CeMassive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CeMassive)
        Me.scc_master.Panel1.Controls.Add(Me.LblStatus)
        Me.scc_master.Panel1.Controls.Add(Me.ce_all)
        Me.scc_master.Panel1.Controls.Add(Me.sb_posting)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1087, 479)
        Me.scc_master.SplitterPosition = 60
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_unposted
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_master.Size = New System.Drawing.Size(1087, 413)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_unposted, Me.xtp_posted, Me.xtp_all})
        '
        'xtp_unposted
        '
        Me.xtp_unposted.Controls.Add(Me.gc_unposted)
        Me.xtp_unposted.Name = "xtp_unposted"
        Me.xtp_unposted.Size = New System.Drawing.Size(1085, 392)
        Me.xtp_unposted.Text = "Unposted"
        '
        'gc_unposted
        '
        Me.gc_unposted.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_unposted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_unposted.Location = New System.Drawing.Point(0, 0)
        Me.gc_unposted.MainView = Me.gv_unposted
        Me.gc_unposted.Name = "gc_unposted"
        Me.gc_unposted.Size = New System.Drawing.Size(1085, 392)
        Me.gc_unposted.TabIndex = 2
        Me.gc_unposted.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_unposted})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckTheTransactionIsNotBalancedToolStripMenuItem, Me.ApproveCheckToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(273, 70)
        '
        'CheckTheTransactionIsNotBalancedToolStripMenuItem
        '
        Me.CheckTheTransactionIsNotBalancedToolStripMenuItem.Name = "CheckTheTransactionIsNotBalancedToolStripMenuItem"
        Me.CheckTheTransactionIsNotBalancedToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.CheckTheTransactionIsNotBalancedToolStripMenuItem.Text = "Check the transaction is not balanced"
        '
        'ApproveCheckToolStripMenuItem
        '
        Me.ApproveCheckToolStripMenuItem.Name = "ApproveCheckToolStripMenuItem"
        Me.ApproveCheckToolStripMenuItem.Size = New System.Drawing.Size(272, 22)
        Me.ApproveCheckToolStripMenuItem.Text = "Approve Check"
        '
        'gv_unposted
        '
        Me.gv_unposted.GridControl = Me.gc_unposted
        Me.gv_unposted.Name = "gv_unposted"
        '
        'xtp_posted
        '
        Me.xtp_posted.Controls.Add(Me.gc_posted)
        Me.xtp_posted.Name = "xtp_posted"
        Me.xtp_posted.Size = New System.Drawing.Size(1085, 392)
        Me.xtp_posted.Text = "Posted"
        '
        'gc_posted
        '
        Me.gc_posted.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_posted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_posted.Location = New System.Drawing.Point(0, 0)
        Me.gc_posted.MainView = Me.gv_posted
        Me.gc_posted.Name = "gc_posted"
        Me.gc_posted.Size = New System.Drawing.Size(1085, 392)
        Me.gc_posted.TabIndex = 3
        Me.gc_posted.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_posted})
        '
        'gv_posted
        '
        Me.gv_posted.GridControl = Me.gc_posted
        Me.gv_posted.Name = "gv_posted"
        '
        'xtp_all
        '
        Me.xtp_all.Controls.Add(Me.gc_all)
        Me.xtp_all.Name = "xtp_all"
        Me.xtp_all.Size = New System.Drawing.Size(1085, 392)
        Me.xtp_all.Text = "All Data"
        '
        'gc_all
        '
        Me.gc_all.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_all.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_all.Location = New System.Drawing.Point(0, 0)
        Me.gc_all.MainView = Me.gv_all
        Me.gc_all.Name = "gc_all"
        Me.gc_all.Size = New System.Drawing.Size(1085, 392)
        Me.gc_all.TabIndex = 4
        Me.gc_all.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_all})
        '
        'gv_all
        '
        Me.gv_all.GridControl = Me.gc_all
        Me.gv_all.Name = "gv_all"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(219, 4)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(167, 7)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 14
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(56, 4)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(4, 7)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "First Date"
        '
        'sb_posting
        '
        Me.sb_posting.Location = New System.Drawing.Point(340, 1)
        Me.sb_posting.Name = "sb_posting"
        Me.sb_posting.Size = New System.Drawing.Size(75, 23)
        Me.sb_posting.TabIndex = 16
        Me.sb_posting.Text = "Posting"
        '
        'ce_all
        '
        Me.ce_all.Location = New System.Drawing.Point(54, 31)
        Me.ce_all.Name = "ce_all"
        Me.ce_all.Properties.Caption = "Check All"
        Me.ce_all.Size = New System.Drawing.Size(75, 19)
        Me.ce_all.TabIndex = 17
        '
        'LblStatus
        '
        Me.LblStatus.Location = New System.Drawing.Point(219, 34)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(4, 13)
        Me.LblStatus.TabIndex = 18
        Me.LblStatus.Text = "-"
        '
        'CeMassive
        '
        Me.CeMassive.Location = New System.Drawing.Point(446, 3)
        Me.CeMassive.Name = "CeMassive"
        Me.CeMassive.Properties.Caption = "Massive"
        Me.CeMassive.Size = New System.Drawing.Size(75, 19)
        Me.CeMassive.TabIndex = 19
        '
        'FTransactionPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1087, 479)
        Me.Name = "FTransactionPost"
        Me.Text = "Transaction Post"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_unposted.ResumeLayout(False)
        CType(Me.gc_unposted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gv_unposted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_posted.ResumeLayout(False)
        CType(Me.gc_posted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_posted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_all.ResumeLayout(False)
        CType(Me.gc_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_all.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CeMassive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_unposted As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_unposted As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_unposted As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_posted As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_posted As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_posted As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sb_posting As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ce_all As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents xtp_all As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_all As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_all As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CheckTheTransactionIsNotBalancedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LblStatus As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ApproveCheckToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CeMassive As DevExpress.XtraEditors.CheckEdit

End Class
