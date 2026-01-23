<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryReportDetail
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
        Me.gc_loc = New DevExpress.XtraGrid.GridControl
        Me.gv_loc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_serial = New DevExpress.XtraTab.XtraTabPage
        Me.gc_serial = New DevExpress.XtraGrid.GridControl
        Me.gv_serial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
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
        Me.pr_price_list = New DevExpress.XtraEditors.LookUpEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.CeNo0 = New DevExpress.XtraEditors.CheckEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_warehouse.SuspendLayout()
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_serial.SuspendLayout()
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.pr_price_list.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CeNo0.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CeNo0)
        Me.scc_master.Panel1.Controls.Add(Me.pr_price_list)
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.CBChild)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(778, 494)
        Me.scc_master.SplitterPosition = 28
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
        Me.xtc_master.Size = New System.Drawing.Size(778, 460)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_warehouse, Me.xtp_serial})
        '
        'xtp_warehouse
        '
        Me.xtp_warehouse.Controls.Add(Me.gc_loc)
        Me.xtp_warehouse.Name = "xtp_warehouse"
        Me.xtp_warehouse.Size = New System.Drawing.Size(776, 459)
        Me.xtp_warehouse.Text = "By Warehouse"
        '
        'gc_loc
        '
        Me.gc_loc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_loc.Location = New System.Drawing.Point(0, 0)
        Me.gc_loc.MainView = Me.gv_loc
        Me.gc_loc.Name = "gc_loc"
        Me.gc_loc.Size = New System.Drawing.Size(776, 459)
        Me.gc_loc.TabIndex = 0
        Me.gc_loc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_loc})
        '
        'gv_loc
        '
        Me.gv_loc.GridControl = Me.gc_loc
        Me.gv_loc.Name = "gv_loc"
        Me.gv_loc.OptionsSelection.MultiSelect = True
        '
        'xtp_serial
        '
        Me.xtp_serial.Controls.Add(Me.gc_serial)
        Me.xtp_serial.Name = "xtp_serial"
        Me.xtp_serial.Size = New System.Drawing.Size(773, 459)
        Me.xtp_serial.Text = "Warehouse With Serial"
        '
        'gc_serial
        '
        Me.gc_serial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_serial.Location = New System.Drawing.Point(0, 0)
        Me.gc_serial.MainView = Me.gv_serial
        Me.gc_serial.Name = "gc_serial"
        Me.gc_serial.Size = New System.Drawing.Size(773, 459)
        Me.gc_serial.TabIndex = 1
        Me.gc_serial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_serial})
        '
        'gv_serial
        '
        Me.gv_serial.GridControl = Me.gc_serial
        Me.gv_serial.Name = "gv_serial"
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
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 494)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(778, 19)
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("719f8947-c6f6-44da-b1b8-aa7d12f4553f")
        Me.dp_detail.Location = New System.Drawing.Point(0, 0)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 200)
        Me.dp_detail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.SavedIndex = 0
        Me.dp_detail.Size = New System.Drawing.Size(775, 200)
        Me.dp_detail.Text = "Data Detail"
        Me.dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(769, 172)
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
        Me.xtc_detail.Size = New System.Drawing.Size(769, 172)
        Me.xtc_detail.TabIndex = 1
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(767, 171)
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
        Me.scc_detail.Size = New System.Drawing.Size(767, 171)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(767, 171)
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
        Me.CBChild.Location = New System.Drawing.Point(264, 5)
        Me.CBChild.Name = "CBChild"
        Me.CBChild.Properties.Caption = "With All Child"
        Me.CBChild.Size = New System.Drawing.Size(111, 19)
        Me.CBChild.TabIndex = 9
        '
        'pr_price_list
        '
        Me.pr_price_list.Location = New System.Drawing.Point(422, 4)
        Me.pr_price_list.Name = "pr_price_list"
        Me.pr_price_list.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_price_list.Size = New System.Drawing.Size(155, 20)
        Me.pr_price_list.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(364, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Price List"
        '
        'CeNo0
        '
        Me.CeNo0.Location = New System.Drawing.Point(596, 5)
        Me.CeNo0.Name = "CeNo0"
        Me.CeNo0.Properties.Caption = "Hide 0"
        Me.CeNo0.Size = New System.Drawing.Size(75, 19)
        Me.CeNo0.TabIndex = 14
        '
        'FInventoryReportDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(778, 513)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FInventoryReportDetail"
        Me.Text = "Inventory Report Detail"
        Me.Controls.SetChildIndex(Me.hideContainerBottom, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_warehouse.ResumeLayout(False)
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_serial.ResumeLayout(False)
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.pr_price_list.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CeNo0.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_warehouse As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_loc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_loc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_serial As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_serial As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_serial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CBChild As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents pr_price_list As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents CeNo0 As DevExpress.XtraEditors.CheckEdit

End Class
