<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSyncMonitoring
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
        Me.xtp_warehouse = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_synci_summ = New DevExpress.XtraGrid.GridControl
        Me.gv_synci_summ = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gc_jobi_summ = New DevExpress.XtraGrid.GridControl
        Me.gv_jobi_summ = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.SplitContainerControl3 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_synco_summ = New DevExpress.XtraGrid.GridControl
        Me.gv_synco_summ = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gc_jobo_summ = New DevExpress.XtraGrid.GridControl
        Me.gv_jobo_summ = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_serial = New DevExpress.XtraTab.XtraTabPage
        Me.gc_serial = New DevExpress.XtraGrid.GridControl
        Me.gv_serial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MergeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_warehouse.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.gc_synci_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_synci_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_jobi_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_jobi_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl3.SuspendLayout()
        CType(Me.gc_synco_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_synco_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_jobo_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_jobo_summ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_serial.SuspendLayout()
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(806, 605)
        Me.scc_master.SplitterPosition = 0
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_warehouse
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(806, 599)
        Me.xtc_master.TabIndex = 2
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_warehouse, Me.xtp_serial})
        '
        'xtp_warehouse
        '
        Me.xtp_warehouse.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_warehouse.Name = "xtp_warehouse"
        Me.xtp_warehouse.Size = New System.Drawing.Size(804, 598)
        Me.xtp_warehouse.Text = "By Warehouse"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.SplitContainerControl2)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.SplitContainerControl3)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(804, 598)
        Me.SplitContainerControl1.SplitterPosition = 360
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Horizontal = False
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.gc_synci_summ)
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.gc_jobi_summ)
        Me.SplitContainerControl2.Panel2.Controls.Add(Me.PanelControl2)
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.Size = New System.Drawing.Size(360, 598)
        Me.SplitContainerControl2.SplitterPosition = 314
        Me.SplitContainerControl2.TabIndex = 0
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'gc_synci_summ
        '
        Me.gc_synci_summ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_synci_summ.Location = New System.Drawing.Point(0, 19)
        Me.gc_synci_summ.MainView = Me.gv_synci_summ
        Me.gc_synci_summ.Name = "gc_synci_summ"
        Me.gc_synci_summ.Size = New System.Drawing.Size(360, 295)
        Me.gc_synci_summ.TabIndex = 1
        Me.gc_synci_summ.UseEmbeddedNavigator = True
        Me.gc_synci_summ.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_synci_summ})
        '
        'gv_synci_summ
        '
        Me.gv_synci_summ.GridControl = Me.gc_synci_summ
        Me.gv_synci_summ.Name = "gv_synci_summ"
        Me.gv_synci_summ.OptionsView.ColumnAutoWidth = False
        Me.gv_synci_summ.OptionsView.ShowGroupPanel = False
        '
        'gc_jobi_summ
        '
        Me.gc_jobi_summ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_jobi_summ.Location = New System.Drawing.Point(0, 19)
        Me.gc_jobi_summ.MainView = Me.gv_jobi_summ
        Me.gc_jobi_summ.Name = "gc_jobi_summ"
        Me.gc_jobi_summ.Size = New System.Drawing.Size(360, 259)
        Me.gc_jobi_summ.TabIndex = 1
        Me.gc_jobi_summ.UseEmbeddedNavigator = True
        Me.gc_jobi_summ.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_jobi_summ})
        '
        'gv_jobi_summ
        '
        Me.gv_jobi_summ.GridControl = Me.gc_jobi_summ
        Me.gv_jobi_summ.Name = "gv_jobi_summ"
        Me.gv_jobi_summ.OptionsView.ColumnAutoWidth = False
        Me.gv_jobi_summ.OptionsView.ShowGroupPanel = False
        '
        'SplitContainerControl3
        '
        Me.SplitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl3.Horizontal = False
        Me.SplitContainerControl3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl3.Name = "SplitContainerControl3"
        Me.SplitContainerControl3.Panel1.Controls.Add(Me.gc_synco_summ)
        Me.SplitContainerControl3.Panel1.Controls.Add(Me.PanelControl3)
        Me.SplitContainerControl3.Panel1.Text = "Panel1"
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.gc_jobo_summ)
        Me.SplitContainerControl3.Panel2.Controls.Add(Me.PanelControl4)
        Me.SplitContainerControl3.Panel2.Text = "Panel2"
        Me.SplitContainerControl3.Size = New System.Drawing.Size(438, 598)
        Me.SplitContainerControl3.SplitterPosition = 314
        Me.SplitContainerControl3.TabIndex = 0
        Me.SplitContainerControl3.Text = "SplitContainerControl3"
        '
        'gc_synco_summ
        '
        Me.gc_synco_summ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_synco_summ.Location = New System.Drawing.Point(0, 19)
        Me.gc_synco_summ.MainView = Me.gv_synco_summ
        Me.gc_synco_summ.Name = "gc_synco_summ"
        Me.gc_synco_summ.Size = New System.Drawing.Size(438, 295)
        Me.gc_synco_summ.TabIndex = 2
        Me.gc_synco_summ.UseEmbeddedNavigator = True
        Me.gc_synco_summ.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_synco_summ})
        '
        'gv_synco_summ
        '
        Me.gv_synco_summ.GridControl = Me.gc_synco_summ
        Me.gv_synco_summ.Name = "gv_synco_summ"
        Me.gv_synco_summ.OptionsView.ColumnAutoWidth = False
        Me.gv_synco_summ.OptionsView.ShowGroupPanel = False
        '
        'gc_jobo_summ
        '
        Me.gc_jobo_summ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_jobo_summ.Location = New System.Drawing.Point(0, 19)
        Me.gc_jobo_summ.MainView = Me.gv_jobo_summ
        Me.gc_jobo_summ.Name = "gc_jobo_summ"
        Me.gc_jobo_summ.Size = New System.Drawing.Size(438, 259)
        Me.gc_jobo_summ.TabIndex = 2
        Me.gc_jobo_summ.UseEmbeddedNavigator = True
        Me.gc_jobo_summ.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_jobo_summ})
        '
        'gv_jobo_summ
        '
        Me.gv_jobo_summ.GridControl = Me.gc_jobo_summ
        Me.gv_jobo_summ.Name = "gv_jobo_summ"
        Me.gv_jobo_summ.OptionsView.ColumnAutoWidth = False
        Me.gv_jobo_summ.OptionsView.ShowGroupPanel = False
        '
        'xtp_serial
        '
        Me.xtp_serial.Controls.Add(Me.gc_serial)
        Me.xtp_serial.Name = "xtp_serial"
        Me.xtp_serial.Size = New System.Drawing.Size(804, 598)
        Me.xtp_serial.Text = "Warehouse With Serial"
        '
        'gc_serial
        '
        Me.gc_serial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_serial.Location = New System.Drawing.Point(0, 0)
        Me.gc_serial.MainView = Me.gv_serial
        Me.gc_serial.Name = "gc_serial"
        Me.gc_serial.Size = New System.Drawing.Size(804, 598)
        Me.gc_serial.TabIndex = 1
        Me.gc_serial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_serial})
        '
        'gv_serial
        '
        Me.gv_serial.GridControl = Me.gc_serial
        Me.gv_serial.Name = "gv_serial"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MergeToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(109, 48)
        '
        'MergeToolStripMenuItem
        '
        Me.MergeToolStripMenuItem.Name = "MergeToolStripMenuItem"
        Me.MergeToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.MergeToolStripMenuItem.Text = "Merge"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(360, 19)
        Me.PanelControl1.TabIndex = 2
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.LabelControl3)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(360, 19)
        Me.PanelControl2.TabIndex = 3
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.LabelControl2)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(438, 19)
        Me.PanelControl3.TabIndex = 4
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.LabelControl4)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl4.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(438, 19)
        Me.PanelControl4.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Sync In"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Sync Out"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(10, 2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(90, 13)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Sync In Processing"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(12, 1)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(98, 13)
        Me.LabelControl4.TabIndex = 2
        Me.LabelControl4.Text = "Sync Out Processing"
        '
        'FSyncMonitoring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(806, 605)
        Me.Name = "FSyncMonitoring"
        Me.Text = "Data Sync Monitoring"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_warehouse.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.gc_synci_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_synci_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_jobi_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_jobi_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitContainerControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl3.ResumeLayout(False)
        CType(Me.gc_synco_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_synco_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_jobo_summ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_jobo_summ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_serial.ResumeLayout(False)
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        Me.PanelControl4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_warehouse As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtp_serial As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_serial As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_serial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MergeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents SplitContainerControl3 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_synci_summ As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_synci_summ As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gc_jobi_summ As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_jobi_summ As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gc_synco_summ As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_synco_summ As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gc_jobo_summ As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_jobo_summ As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl

End Class
