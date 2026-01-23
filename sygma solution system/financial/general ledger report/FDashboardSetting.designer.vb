<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDashboardSetting
    Inherits master_new.MasterWITwo

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
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CheckAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.GC_Edit = New DevExpress.XtraGrid.GridControl
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.dash_type_number = New DevExpress.XtraEditors.TextEdit
        Me.dash_type_filter = New DevExpress.XtraEditors.TextEdit
        Me.dash_type_id = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_cu_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_cu_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        'Me.DataTable1TableAdapter = New sygma_solution_system.ds_invoiceTableAdapters.DataTable1TableAdapter
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.GC_Edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dash_type_number.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dash_type_filter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dash_type_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(765, 348)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(767, 369)
        Me.scc_master.SplitterPosition = 313
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(767, 369)
        '
        'gc_master
        '
        Me.gc_master.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(755, 338)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master, Me.GridView2})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheckAccountToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(156, 26)
        '
        'CheckAccountToolStripMenuItem
        '
        Me.CheckAccountToolStripMenuItem.Name = "CheckAccountToolStripMenuItem"
        Me.CheckAccountToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.CheckAccountToolStripMenuItem.Text = "Account Check"
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_master
        Me.GridView2.Name = "GridView2"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.GC_Edit)
        Me.lci_master.Controls.Add(Me.dash_type_number)
        Me.lci_master.Controls.Add(Me.dash_type_filter)
        Me.lci_master.Controls.Add(Me.dash_type_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'GC_Edit
        '
        Me.GC_Edit.Location = New System.Drawing.Point(12, 84)
        Me.GC_Edit.MainView = Me.gv_edit
        Me.GC_Edit.Name = "GC_Edit"
        Me.GC_Edit.Size = New System.Drawing.Size(519, 204)
        Me.GC_Edit.TabIndex = 5
        Me.GC_Edit.UseEmbeddedNavigator = True
        Me.GC_Edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.GC_Edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'dash_type_number
        '
        Me.dash_type_number.Enabled = False
        Me.dash_type_number.Location = New System.Drawing.Point(53, 60)
        Me.dash_type_number.Name = "dash_type_number"
        Me.dash_type_number.Size = New System.Drawing.Size(216, 20)
        Me.dash_type_number.StyleController = Me.lci_master
        Me.dash_type_number.TabIndex = 2
        '
        'dash_type_filter
        '
        Me.dash_type_filter.Enabled = False
        Me.dash_type_filter.Location = New System.Drawing.Point(53, 12)
        Me.dash_type_filter.Name = "dash_type_filter"
        Me.dash_type_filter.Size = New System.Drawing.Size(216, 20)
        Me.dash_type_filter.StyleController = Me.lci_master
        Me.dash_type_filter.TabIndex = 0
        '
        'dash_type_id
        '
        Me.dash_type_id.Enabled = False
        Me.dash_type_id.Location = New System.Drawing.Point(53, 36)
        Me.dash_type_id.Name = "dash_type_id"
        Me.dash_type_id.Size = New System.Drawing.Size(216, 20)
        Me.dash_type_id.StyleController = Me.lci_master
        Me.dash_type_id.TabIndex = 1
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_cu_code, Me.LayoutControlItem1, Me.lci_cu_desc, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.EmptySpaceItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 300)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_cu_code
        '
        Me.lci_cu_code.Control = Me.dash_type_filter
        Me.lci_cu_code.CustomizationFormText = "Currency Code"
        Me.lci_cu_code.Location = New System.Drawing.Point(0, 0)
        Me.lci_cu_code.Name = "lci_cu_code"
        Me.lci_cu_code.Size = New System.Drawing.Size(261, 24)
        Me.lci_cu_code.Text = "Type"
        Me.lci_cu_code.TextSize = New System.Drawing.Size(37, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.dash_type_number
        Me.LayoutControlItem1.CustomizationFormText = "Sort Number"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem1.Text = "Number"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(37, 13)
        '
        'lci_cu_desc
        '
        Me.lci_cu_desc.Control = Me.dash_type_id
        Me.lci_cu_desc.CustomizationFormText = "Description"
        Me.lci_cu_desc.Location = New System.Drawing.Point(0, 24)
        Me.lci_cu_desc.Name = "lci_cu_desc"
        Me.lci_cu_desc.Size = New System.Drawing.Size(261, 24)
        Me.lci_cu_desc.Text = "ID"
        Me.lci_cu_desc.TextSize = New System.Drawing.Size(37, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.GC_Edit
        Me.LayoutControlItem4.CustomizationFormText = " "
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(523, 208)
        Me.LayoutControlItem4.Text = " "
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(261, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(262, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(261, 24)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(262, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(261, 48)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(262, 24)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("667bec5c-0802-4a5b-a433-ff3dfa92dab1")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 369)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(767, 200)
        Me.DockPanel1.Text = "Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.gc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(761, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(761, 172)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'DataTable1TableAdapter
        '
        'Me.DataTable1TableAdapter.ClearBeforeFill = True
        '
        'FDashboardSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(767, 569)
        Me.Controls.Add(Me.DockPanel1)
        Me.Name = "FDashboardSetting"
        Me.Text = "Dashboard Setting"
        Me.Controls.SetChildIndex(Me.DockPanel1, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.GC_Edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dash_type_number.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dash_type_filter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dash_type_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents dash_type_filter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_cu_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents dash_type_id As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_cu_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents dash_type_number As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents GC_Edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CheckAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    'Friend WithEvents DataTable1TableAdapter As sygma_solution_system.ds_invoiceTableAdapters.DataTable1TableAdapter

End Class
