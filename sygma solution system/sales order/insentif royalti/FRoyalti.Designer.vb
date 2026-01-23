<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRoyalti
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
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_sum = New DevExpress.XtraTab.XtraTabPage
        Me.gc_sum = New DevExpress.XtraGrid.GridControl
        Me.gv_sum = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.sb_generate = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.pr_periode = New DevExpress.XtraEditors.LookUpEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_sum.SuspendLayout()
        CType(Me.gc_sum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_sum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_periode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_generate)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_periode)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 29
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_detail
        Me.xtc_master.Size = New System.Drawing.Size(555, 358)
        Me.xtc_master.TabIndex = 2
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_sum})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.gc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(553, 337)
        Me.xtp_detail.Text = "Detail"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(553, 337)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'xtp_sum
        '
        Me.xtp_sum.Controls.Add(Me.gc_sum)
        Me.xtp_sum.Name = "xtp_sum"
        Me.xtp_sum.Size = New System.Drawing.Size(553, 377)
        Me.xtp_sum.Text = "Summary"
        '
        'gc_sum
        '
        Me.gc_sum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_sum.Location = New System.Drawing.Point(0, 0)
        Me.gc_sum.MainView = Me.gv_sum
        Me.gc_sum.Name = "gc_sum"
        Me.gc_sum.Size = New System.Drawing.Size(553, 377)
        Me.gc_sum.TabIndex = 1
        Me.gc_sum.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_sum})
        '
        'gv_sum
        '
        Me.gv_sum.GridControl = Me.gc_sum
        Me.gv_sum.Name = "gv_sum"
        '
        'sb_generate
        '
        Me.sb_generate.Location = New System.Drawing.Point(253, 3)
        Me.sb_generate.Name = "sb_generate"
        Me.sb_generate.Size = New System.Drawing.Size(75, 23)
        Me.sb_generate.TabIndex = 5
        Me.sb_generate.Text = "Generate"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(4, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Periode"
        '
        'pr_periode
        '
        Me.pr_periode.Location = New System.Drawing.Point(46, 5)
        Me.pr_periode.Name = "pr_periode"
        Me.pr_periode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_periode.Properties.PopupWidth = 500
        Me.pr_periode.Size = New System.Drawing.Size(195, 20)
        Me.pr_periode.TabIndex = 3
        '
        'FRoyalti
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FRoyalti"
        Me.Text = "Royalti"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_sum.ResumeLayout(False)
        CType(Me.gc_sum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_sum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_periode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_sum As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_sum As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_sum As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_generate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_periode As DevExpress.XtraEditors.LookUpEdit

End Class
