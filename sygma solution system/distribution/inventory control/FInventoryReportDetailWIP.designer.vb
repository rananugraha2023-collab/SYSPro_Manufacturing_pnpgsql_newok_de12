<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryReportDetailWIP
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
        Me.xtp_inv = New DevExpress.XtraTab.XtraTabPage
        Me.gc_inv = New DevExpress.XtraGrid.GridControl
        Me.gv_inv = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_inv.SuspendLayout()
        CType(Me.gc_inv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_inv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 28
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_inv
        Me.xtc_master.Size = New System.Drawing.Size(555, 358)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_inv})
        '
        'xtp_inv
        '
        Me.xtp_inv.Controls.Add(Me.gc_inv)
        Me.xtp_inv.Name = "xtp_inv"
        Me.xtp_inv.Size = New System.Drawing.Size(553, 337)
        Me.xtp_inv.Text = "By Warehouse"
        '
        'gc_inv
        '
        Me.gc_inv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_inv.Location = New System.Drawing.Point(0, 0)
        Me.gc_inv.MainView = Me.gv_inv
        Me.gc_inv.Name = "gc_inv"
        Me.gc_inv.Size = New System.Drawing.Size(553, 337)
        Me.gc_inv.TabIndex = 0
        Me.gc_inv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_inv})
        '
        'gv_inv
        '
        Me.gv_inv.GridControl = Me.gc_inv
        Me.gv_inv.Name = "gv_inv"
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(44, 3)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(193, 20)
        Me.pr_entity.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Entity"
        '
        'FInventoryReportDetailWIP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FInventoryReportDetailWIP"
        Me.Text = "Inventory Report Detail WIP"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_inv.ResumeLayout(False)
        CType(Me.gc_inv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_inv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_inv As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_inv As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_inv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
