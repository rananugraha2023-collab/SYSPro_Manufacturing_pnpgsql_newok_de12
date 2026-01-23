<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryReportDetailLog
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
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_warehouse = New DevExpress.XtraTab.XtraTabPage
        Me.gc_loc = New DevExpress.XtraGrid.GridControl
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MergeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gv_loc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_serial = New DevExpress.XtraTab.XtraTabPage
        Me.gc_serial = New DevExpress.XtraGrid.GridControl
        Me.gv_serial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.CBChild = New DevExpress.XtraEditors.CheckEdit
        Me.BtEItem = New DevExpress.XtraEditors.ButtonEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.BtCek = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.CeNo0 = New DevExpress.XtraEditors.CheckEdit
        Me.CEGroupByWh = New DevExpress.XtraEditors.CheckEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_warehouse.SuspendLayout()
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_serial.SuspendLayout()
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CBChild.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BtEItem.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CeNo0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CEGroupByWh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CEGroupByWh)
        Me.scc_master.Panel1.Controls.Add(Me.CeNo0)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.BtCek)
        Me.scc_master.Panel1.Controls.Add(Me.BtEItem)
        Me.scc_master.Panel1.Controls.Add(Me.CBChild)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(806, 586)
        Me.scc_master.SplitterPosition = 52
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(39, 3)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(193, 20)
        Me.pr_entity.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Entity"
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
        Me.xtc_master.Size = New System.Drawing.Size(806, 528)
        Me.xtc_master.TabIndex = 2
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_warehouse, Me.xtp_serial})
        '
        'xtp_warehouse
        '
        Me.xtp_warehouse.Controls.Add(Me.gc_loc)
        Me.xtp_warehouse.Name = "xtp_warehouse"
        Me.xtp_warehouse.Size = New System.Drawing.Size(804, 527)
        Me.xtp_warehouse.Text = "By Warehouse"
        '
        'gc_loc
        '
        Me.gc_loc.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_loc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_loc.Location = New System.Drawing.Point(0, 0)
        Me.gc_loc.MainView = Me.gv_loc
        Me.gc_loc.Name = "gc_loc"
        Me.gc_loc.Size = New System.Drawing.Size(804, 527)
        Me.gc_loc.TabIndex = 0
        Me.gc_loc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_loc})
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
        'gv_loc
        '
        Me.gv_loc.GridControl = Me.gc_loc
        Me.gv_loc.Name = "gv_loc"
        '
        'xtp_serial
        '
        Me.xtp_serial.Controls.Add(Me.gc_serial)
        Me.xtp_serial.Name = "xtp_serial"
        Me.xtp_serial.Size = New System.Drawing.Size(804, 527)
        Me.xtp_serial.Text = "Warehouse With Serial"
        '
        'gc_serial
        '
        Me.gc_serial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_serial.Location = New System.Drawing.Point(0, 0)
        Me.gc_serial.MainView = Me.gv_serial
        Me.gc_serial.Name = "gc_serial"
        Me.gc_serial.Size = New System.Drawing.Size(804, 527)
        Me.gc_serial.TabIndex = 1
        Me.gc_serial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_serial})
        '
        'gv_serial
        '
        Me.gv_serial.GridControl = Me.gc_serial
        Me.gv_serial.Name = "gv_serial"
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.Controls.Add(Me.dp_detail)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 586)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(806, 19)
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("ef939ee7-41a1-41b6-af09-62a15d3fe0ad")
        Me.dp_detail.Location = New System.Drawing.Point(0, 0)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(806, 200)
        Me.dp_detail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.SavedIndex = 0
        Me.dp_detail.Size = New System.Drawing.Size(806, 200)
        Me.dp_detail.Text = "Data Detail"
        Me.dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(800, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_detail
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_detail.Size = New System.Drawing.Size(800, 172)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(798, 171)
        Me.xtp_detail.Text = "Data Detail"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_detail)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(798, 171)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(798, 171)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'CBChild
        '
        Me.CBChild.Location = New System.Drawing.Point(37, 29)
        Me.CBChild.Name = "CBChild"
        Me.CBChild.Properties.Caption = "With All Child"
        Me.CBChild.Size = New System.Drawing.Size(111, 19)
        Me.CBChild.TabIndex = 8
        '
        'BtEItem
        '
        Me.BtEItem.Location = New System.Drawing.Point(322, 4)
        Me.BtEItem.Name = "BtEItem"
        Me.BtEItem.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.BtEItem.Size = New System.Drawing.Size(150, 20)
        Me.BtEItem.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(251, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Part Number"
        '
        'BtCek
        '
        Me.BtCek.Location = New System.Drawing.Point(478, 3)
        Me.BtCek.Name = "BtCek"
        Me.BtCek.Size = New System.Drawing.Size(104, 23)
        Me.BtCek.TabIndex = 10
        Me.BtCek.Text = "Cek Dobel"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(734, 3)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl1.TabIndex = 11
        Me.LabelControl1.Text = "-"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(704, 3)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl2.TabIndex = 12
        Me.LabelControl2.Text = "."
        '
        'CeNo0
        '
        Me.CeNo0.Location = New System.Drawing.Point(138, 29)
        Me.CeNo0.Name = "CeNo0"
        Me.CeNo0.Properties.Caption = "Hide 0"
        Me.CeNo0.Size = New System.Drawing.Size(75, 19)
        Me.CeNo0.TabIndex = 13
        '
        'CEGroupByWh
        '
        Me.CEGroupByWh.Location = New System.Drawing.Point(219, 29)
        Me.CEGroupByWh.Name = "CEGroupByWh"
        Me.CEGroupByWh.Properties.Caption = "Group By Warehouse"
        Me.CEGroupByWh.Size = New System.Drawing.Size(136, 19)
        Me.CEGroupByWh.TabIndex = 13
        '
        'FInventoryReportDetailLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(806, 605)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FInventoryReportDetailLog"
        Me.Text = "Inventory Report Detail Log"
        Me.Controls.SetChildIndex(Me.hideContainerBottom, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_warehouse.ResumeLayout(False)
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_serial.ResumeLayout(False)
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CBChild.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BtEItem.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CeNo0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CEGroupByWh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_warehouse As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_loc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_loc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_serial As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_serial As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_serial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MergeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CBChild As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents BtEItem As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtCek As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CeNo0 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CEGroupByWh As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer

End Class
