<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCashInReport
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
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail_ar = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail_ar = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_ar = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_detail_so_cash = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail_so_cash = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_so_cash = New DevExpress.XtraGrid.Views.Grid.GridView
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_detail_ar.SuspendLayout()
        CType(Me.gc_detail_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_detail_so_cash.SuspendLayout()
        CType(Me.gc_detail_so_cash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_so_cash, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 26
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(215, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 35
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(166, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 34
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(55, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 33
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(2, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 32
        Me.LabelControl1.Text = "First Date"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_detail_ar
        Me.xtc_master.Size = New System.Drawing.Size(555, 358)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail_ar, Me.xtp_detail_so_cash})
        '
        'xtp_detail_ar
        '
        Me.xtp_detail_ar.Controls.Add(Me.gc_detail_ar)
        Me.xtp_detail_ar.Name = "xtp_detail_ar"
        Me.xtp_detail_ar.Size = New System.Drawing.Size(553, 337)
        Me.xtp_detail_ar.Text = "AR"
        '
        'gc_detail_ar
        '
        Me.gc_detail_ar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_ar.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_ar.MainView = Me.gv_detail_ar
        Me.gc_detail_ar.Name = "gc_detail_ar"
        Me.gc_detail_ar.Size = New System.Drawing.Size(553, 337)
        Me.gc_detail_ar.TabIndex = 0
        Me.gc_detail_ar.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_ar})
        '
        'gv_detail_ar
        '
        Me.gv_detail_ar.GridControl = Me.gc_detail_ar
        Me.gv_detail_ar.Name = "gv_detail_ar"
        '
        'xtp_detail_so_cash
        '
        Me.xtp_detail_so_cash.Controls.Add(Me.gc_detail_so_cash)
        Me.xtp_detail_so_cash.Name = "xtp_detail_so_cash"
        Me.xtp_detail_so_cash.Size = New System.Drawing.Size(553, 380)
        Me.xtp_detail_so_cash.Text = "SO Cash"
        '
        'gc_detail_so_cash
        '
        Me.gc_detail_so_cash.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_so_cash.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_so_cash.MainView = Me.gv_detail_so_cash
        Me.gc_detail_so_cash.Name = "gc_detail_so_cash"
        Me.gc_detail_so_cash.Size = New System.Drawing.Size(553, 380)
        Me.gc_detail_so_cash.TabIndex = 1
        Me.gc_detail_so_cash.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_so_cash})
        '
        'gv_detail_so_cash
        '
        Me.gv_detail_so_cash.GridControl = Me.gc_detail_so_cash
        Me.gv_detail_so_cash.Name = "gv_detail_so_cash"
        '
        'FCashInReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FCashInReport"
        Me.Text = "Cash In Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_detail_ar.ResumeLayout(False)
        CType(Me.gc_detail_ar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_ar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_detail_so_cash.ResumeLayout(False)
        CType(Me.gc_detail_so_cash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_so_cash, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail_ar As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail_ar As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_ar As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_detail_so_cash As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail_so_cash As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_so_cash As DevExpress.XtraGrid.Views.Grid.GridView

End Class
