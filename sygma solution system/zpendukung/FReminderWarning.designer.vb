<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FReminderWarning
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
        Me.xtp_wf = New DevExpress.XtraTab.XtraTabPage
        Me.gc_wf = New DevExpress.XtraGrid.GridControl
        Me.gv_wf = New DevExpress.XtraGrid.Views.Grid.GridView
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_wf.SuspendLayout()
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_wf
        Me.xtc_master.Size = New System.Drawing.Size(555, 358)
        Me.xtc_master.TabIndex = 0
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_wf})
        '
        'xtp_wf
        '
        Me.xtp_wf.Controls.Add(Me.gc_wf)
        Me.xtp_wf.Name = "xtp_wf"
        Me.xtp_wf.Size = New System.Drawing.Size(553, 337)
        Me.xtp_wf.Text = "Data"
        '
        'gc_wf
        '
        Me.gc_wf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_wf.Location = New System.Drawing.Point(0, 0)
        Me.gc_wf.MainView = Me.gv_wf
        Me.gc_wf.Name = "gc_wf"
        Me.gc_wf.Size = New System.Drawing.Size(553, 337)
        Me.gc_wf.TabIndex = 2
        Me.gc_wf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_wf})
        '
        'gv_wf
        '
        Me.gv_wf.GridControl = Me.gc_wf
        Me.gv_wf.Name = "gv_wf"
        '
        'FReminderWarning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FReminderWarning"
        Me.Text = "Reminder Page"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_wf.ResumeLayout(False)
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_wf As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_wf As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_wf As DevExpress.XtraGrid.Views.Grid.GridView

End Class
