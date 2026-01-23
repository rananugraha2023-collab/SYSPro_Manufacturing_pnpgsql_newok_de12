<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FWorkOrder
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
        Me.components = New System.ComponentModel.Container()
        Me.gc_master = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateRoutingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegenerateRoutingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl()
        Me.wo_end_date = New DevExpress.XtraEditors.DateEdit()
        Me.wo_start_date = New DevExpress.XtraEditors.DateEdit()
        Me.wo_wog_code = New DevExpress.XtraEditors.LookUpEdit()
        Me.wo_cost = New DevExpress.XtraEditors.TextEdit()
        Me.wo_pt_id_prj = New DevExpress.XtraEditors.ButtonEdit()
        Me.wo_insheet_pct = New DevExpress.XtraEditors.TextEdit()
        Me.wo_qty = New DevExpress.XtraEditors.TextEdit()
        Me.wo_ps_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.wo_ref_rework = New DevExpress.XtraEditors.ButtonEdit()
        Me.wo_type = New DevExpress.XtraEditors.LookUpEdit()
        Me.pjc_desc = New DevExpress.XtraEditors.TextEdit()
        Me.wo_so_oid = New DevExpress.XtraEditors.ButtonEdit()
        Me.wo_ro_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.gc_edit = New DevExpress.XtraGrid.GridControl()
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.wo_ord_date = New DevExpress.XtraEditors.DateEdit()
        Me.wo_remarks = New DevExpress.XtraEditors.TextEdit()
        Me.wo_pt_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.wo_due_date = New DevExpress.XtraEditors.DateEdit()
        Me.wo_qty_ord = New DevExpress.XtraEditors.TextEdit()
        Me.wo_en_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.wo_si_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.tcg_header = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl()
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage()
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gc_detail = New DevExpress.XtraGrid.GridControl()
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtp_woroute = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gc_routing = New DevExpress.XtraGrid.GridControl()
        Me.gv_routing = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.EmptySpaceItem9 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.BtUpdateSod = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.wo_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_end_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_start_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_wog_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_cost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_pt_id_prj.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_insheet_pct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_qty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_ps_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_ref_rework.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pjc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_so_oid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_ro_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_ord_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_ord_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_due_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_qty_ord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wo_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.xtp_woroute.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_routing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_routing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(880, 583)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl4)
        Me.scc_master.Panel1.Controls.Add(Me.BtUpdateSod)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(882, 641)
        Me.scc_master.SplitterPosition = 32
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 375)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 330)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(882, 604)
        '
        'gc_master
        '
        Me.gc_master.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(870, 573)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateRoutingToolStripMenuItem, Me.RegenerateRoutingToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(179, 48)
        '
        'UpdateRoutingToolStripMenuItem
        '
        Me.UpdateRoutingToolStripMenuItem.Name = "UpdateRoutingToolStripMenuItem"
        Me.UpdateRoutingToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.UpdateRoutingToolStripMenuItem.Text = "Update Routing"
        '
        'RegenerateRoutingToolStripMenuItem
        '
        Me.RegenerateRoutingToolStripMenuItem.Name = "RegenerateRoutingToolStripMenuItem"
        Me.RegenerateRoutingToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.RegenerateRoutingToolStripMenuItem.Text = "Regenerate Routing"
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.wo_end_date)
        Me.lci_master.Controls.Add(Me.wo_start_date)
        Me.lci_master.Controls.Add(Me.wo_wog_code)
        Me.lci_master.Controls.Add(Me.wo_cost)
        Me.lci_master.Controls.Add(Me.wo_pt_id_prj)
        Me.lci_master.Controls.Add(Me.wo_insheet_pct)
        Me.lci_master.Controls.Add(Me.wo_qty)
        Me.lci_master.Controls.Add(Me.wo_ps_id)
        Me.lci_master.Controls.Add(Me.wo_ref_rework)
        Me.lci_master.Controls.Add(Me.wo_type)
        Me.lci_master.Controls.Add(Me.pjc_desc)
        Me.lci_master.Controls.Add(Me.wo_so_oid)
        Me.lci_master.Controls.Add(Me.wo_ro_id)
        Me.lci_master.Controls.Add(Me.gc_edit)
        Me.lci_master.Controls.Add(Me.wo_ord_date)
        Me.lci_master.Controls.Add(Me.wo_remarks)
        Me.lci_master.Controls.Add(Me.wo_pt_id)
        Me.lci_master.Controls.Add(Me.wo_due_date)
        Me.lci_master.Controls.Add(Me.wo_qty_ord)
        Me.lci_master.Controls.Add(Me.wo_en_id)
        Me.lci_master.Controls.Add(Me.wo_si_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 330)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'wo_end_date
        '
        Me.wo_end_date.EditValue = Nothing
        Me.wo_end_date.Location = New System.Drawing.Point(374, 430)
        Me.wo_end_date.Name = "wo_end_date"
        Me.wo_end_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_end_date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.wo_end_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_end_date.Size = New System.Drawing.Size(121, 20)
        Me.wo_end_date.StyleController = Me.lci_master
        Me.wo_end_date.TabIndex = 45
        '
        'wo_start_date
        '
        Me.wo_start_date.EditValue = Nothing
        Me.wo_start_date.Location = New System.Drawing.Point(148, 430)
        Me.wo_start_date.Name = "wo_start_date"
        Me.wo_start_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_start_date.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.wo_start_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_start_date.Size = New System.Drawing.Size(122, 20)
        Me.wo_start_date.StyleController = Me.lci_master
        Me.wo_start_date.TabIndex = 44
        '
        'wo_wog_code
        '
        Me.wo_wog_code.Location = New System.Drawing.Point(374, 142)
        Me.wo_wog_code.Name = "wo_wog_code"
        Me.wo_wog_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_wog_code.Size = New System.Drawing.Size(121, 20)
        Me.wo_wog_code.StyleController = Me.lci_master
        Me.wo_wog_code.TabIndex = 43
        '
        'wo_cost
        '
        Me.wo_cost.Location = New System.Drawing.Point(148, 238)
        Me.wo_cost.Name = "wo_cost"
        Me.wo_cost.Properties.ReadOnly = True
        Me.wo_cost.Size = New System.Drawing.Size(122, 20)
        Me.wo_cost.StyleController = Me.lci_master
        Me.wo_cost.TabIndex = 42
        '
        'wo_pt_id_prj
        '
        Me.wo_pt_id_prj.Location = New System.Drawing.Point(148, 166)
        Me.wo_pt_id_prj.Name = "wo_pt_id_prj"
        Me.wo_pt_id_prj.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_pt_id_prj.Properties.ReadOnly = True
        Me.wo_pt_id_prj.Size = New System.Drawing.Size(122, 20)
        Me.wo_pt_id_prj.StyleController = Me.lci_master
        Me.wo_pt_id_prj.TabIndex = 41
        '
        'wo_insheet_pct
        '
        Me.wo_insheet_pct.Location = New System.Drawing.Point(374, 286)
        Me.wo_insheet_pct.Name = "wo_insheet_pct"
        Me.wo_insheet_pct.Properties.Mask.EditMask = "p"
        Me.wo_insheet_pct.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.wo_insheet_pct.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.wo_insheet_pct.Properties.ReadOnly = True
        Me.wo_insheet_pct.Size = New System.Drawing.Size(56, 20)
        Me.wo_insheet_pct.StyleController = Me.lci_master
        Me.wo_insheet_pct.TabIndex = 40
        '
        'wo_qty
        '
        Me.wo_qty.Location = New System.Drawing.Point(148, 358)
        Me.wo_qty.Name = "wo_qty"
        Me.wo_qty.Properties.Mask.EditMask = "n2"
        Me.wo_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.wo_qty.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.wo_qty.Size = New System.Drawing.Size(122, 20)
        Me.wo_qty.StyleController = Me.lci_master
        Me.wo_qty.TabIndex = 39
        '
        'wo_ps_id
        '
        Me.wo_ps_id.Location = New System.Drawing.Point(374, 190)
        Me.wo_ps_id.Name = "wo_ps_id"
        Me.wo_ps_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_ps_id.Properties.ReadOnly = True
        Me.wo_ps_id.Size = New System.Drawing.Size(121, 20)
        Me.wo_ps_id.StyleController = Me.lci_master
        Me.wo_ps_id.TabIndex = 38
        '
        'wo_ref_rework
        '
        Me.wo_ref_rework.Location = New System.Drawing.Point(374, 94)
        Me.wo_ref_rework.Name = "wo_ref_rework"
        Me.wo_ref_rework.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_ref_rework.Properties.ReadOnly = True
        Me.wo_ref_rework.Size = New System.Drawing.Size(121, 20)
        Me.wo_ref_rework.StyleController = Me.lci_master
        Me.wo_ref_rework.TabIndex = 37
        '
        'wo_type
        '
        Me.wo_type.Location = New System.Drawing.Point(374, 70)
        Me.wo_type.Name = "wo_type"
        Me.wo_type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_type.Size = New System.Drawing.Size(121, 20)
        Me.wo_type.StyleController = Me.lci_master
        Me.wo_type.TabIndex = 36
        '
        'pjc_desc
        '
        Me.pjc_desc.Location = New System.Drawing.Point(148, 214)
        Me.pjc_desc.Name = "pjc_desc"
        Me.pjc_desc.Properties.ReadOnly = True
        Me.pjc_desc.Size = New System.Drawing.Size(347, 20)
        Me.pjc_desc.StyleController = Me.lci_master
        Me.pjc_desc.TabIndex = 34
        '
        'wo_so_oid
        '
        Me.wo_so_oid.Location = New System.Drawing.Point(148, 142)
        Me.wo_so_oid.Name = "wo_so_oid"
        Me.wo_so_oid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_so_oid.Properties.ReadOnly = True
        Me.wo_so_oid.Size = New System.Drawing.Size(122, 20)
        Me.wo_so_oid.StyleController = Me.lci_master
        Me.wo_so_oid.TabIndex = 33
        '
        'wo_ro_id
        '
        Me.wo_ro_id.Location = New System.Drawing.Point(148, 286)
        Me.wo_ro_id.Name = "wo_ro_id"
        Me.wo_ro_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_ro_id.Properties.ReadOnly = True
        Me.wo_ro_id.Size = New System.Drawing.Size(122, 20)
        Me.wo_ro_id.StyleController = Me.lci_master
        Me.wo_ro_id.TabIndex = 31
        '
        'gc_edit
        '
        Me.gc_edit.Location = New System.Drawing.Point(36, 58)
        Me.gc_edit.MainView = Me.gv_edit
        Me.gc_edit.Name = "gc_edit"
        Me.gc_edit.Size = New System.Drawing.Size(471, 414)
        Me.gc_edit.TabIndex = 35
        Me.gc_edit.UseEmbeddedNavigator = True
        Me.gc_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.gc_edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowFooter = True
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'wo_ord_date
        '
        Me.wo_ord_date.EditValue = New Date(2010, 4, 27, 0, 0, 0, 0)
        Me.wo_ord_date.Location = New System.Drawing.Point(148, 406)
        Me.wo_ord_date.Name = "wo_ord_date"
        Me.wo_ord_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_ord_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_ord_date.Size = New System.Drawing.Size(122, 20)
        Me.wo_ord_date.StyleController = Me.lci_master
        Me.wo_ord_date.TabIndex = 28
        '
        'wo_remarks
        '
        Me.wo_remarks.Location = New System.Drawing.Point(148, 310)
        Me.wo_remarks.Name = "wo_remarks"
        Me.wo_remarks.Size = New System.Drawing.Size(347, 20)
        Me.wo_remarks.StyleController = Me.lci_master
        Me.wo_remarks.TabIndex = 27
        '
        'wo_pt_id
        '
        Me.wo_pt_id.Location = New System.Drawing.Point(148, 190)
        Me.wo_pt_id.Name = "wo_pt_id"
        Me.wo_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_pt_id.Properties.ReadOnly = True
        Me.wo_pt_id.Size = New System.Drawing.Size(122, 20)
        Me.wo_pt_id.StyleController = Me.lci_master
        Me.wo_pt_id.TabIndex = 29
        '
        'wo_due_date
        '
        Me.wo_due_date.EditValue = Nothing
        Me.wo_due_date.Location = New System.Drawing.Point(374, 406)
        Me.wo_due_date.Name = "wo_due_date"
        Me.wo_due_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_due_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.wo_due_date.Size = New System.Drawing.Size(121, 20)
        Me.wo_due_date.StyleController = Me.lci_master
        Me.wo_due_date.TabIndex = 23
        '
        'wo_qty_ord
        '
        Me.wo_qty_ord.Location = New System.Drawing.Point(374, 358)
        Me.wo_qty_ord.Name = "wo_qty_ord"
        Me.wo_qty_ord.Properties.DisplayFormat.FormatString = "n"
        Me.wo_qty_ord.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.wo_qty_ord.Properties.EditFormat.FormatString = "n"
        Me.wo_qty_ord.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.wo_qty_ord.Properties.Mask.EditMask = "n"
        Me.wo_qty_ord.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.wo_qty_ord.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.wo_qty_ord.Properties.ReadOnly = True
        Me.wo_qty_ord.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.wo_qty_ord.Size = New System.Drawing.Size(121, 20)
        Me.wo_qty_ord.StyleController = Me.lci_master
        Me.wo_qty_ord.TabIndex = 20
        '
        'wo_en_id
        '
        Me.wo_en_id.Location = New System.Drawing.Point(148, 70)
        Me.wo_en_id.Name = "wo_en_id"
        Me.wo_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_en_id.Size = New System.Drawing.Size(122, 20)
        Me.wo_en_id.StyleController = Me.lci_master
        Me.wo_en_id.TabIndex = 4
        '
        'wo_si_id
        '
        Me.wo_si_id.Location = New System.Drawing.Point(148, 94)
        Me.wo_si_id.Name = "wo_si_id"
        Me.wo_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wo_si_id.Size = New System.Drawing.Size(122, 20)
        Me.wo_si_id.StyleController = Me.lci_master
        Me.wo_si_id.TabIndex = 15
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 508)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Header"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_header})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(523, 488)
        Me.LayoutControlGroup2.Text = "Header"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'tcg_header
        '
        Me.tcg_header.CustomizationFormText = "tcg_header"
        Me.tcg_header.Location = New System.Drawing.Point(0, 0)
        Me.tcg_header.Name = "tcg_header"
        Me.tcg_header.SelectedTabPage = Me.LayoutControlGroup3
        Me.tcg_header.SelectedTabPageIndex = 0
        Me.tcg_header.Size = New System.Drawing.Size(499, 464)
        Me.tcg_header.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3, Me.LayoutControlGroup9})
        Me.tcg_header.Text = "tcg_header"
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Header"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup4, Me.LayoutControlGroup5, Me.LayoutControlGroup6, Me.LayoutControlGroup7, Me.LayoutControlGroup8})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(475, 418)
        Me.LayoutControlGroup3.Text = "Header"
        Me.LayoutControlGroup3.TextLocation = DevExpress.Utils.Locations.Bottom
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem12, Me.LayoutControlItem17, Me.LayoutControlItem18})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(475, 72)
        Me.LayoutControlGroup4.Text = "LayoutControlGroup4"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.wo_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.wo_si_id
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem12.Text = "Site"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.wo_type
        Me.LayoutControlItem17.CustomizationFormText = "Type"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(226, 0)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem17.Text = "Type"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.wo_ref_rework
        Me.LayoutControlItem18.CustomizationFormText = "Reference Rework"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(226, 24)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem18.Text = "Reference Rework"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem11, Me.LayoutControlItem19, Me.LayoutControlItem13, Me.LayoutControlItem20, Me.LayoutControlItem21, Me.EmptySpaceItem6, Me.LayoutControlItem22, Me.EmptySpaceItem3})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(475, 144)
        Me.LayoutControlGroup5.Text = "LayoutControlGroup5"
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.wo_pt_id
        Me.LayoutControlItem2.CustomizationFormText = "Part Number"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem2.Text = "Part Number WO"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.wo_so_oid
        Me.LayoutControlItem11.CustomizationFormText = "Project"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem11.Text = "Sales Order"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.wo_ps_id
        Me.LayoutControlItem19.CustomizationFormText = "Part Number WO"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(226, 48)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem19.Text = "Product Structure"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.pjc_desc
        Me.LayoutControlItem13.CustomizationFormText = "Project Desc"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(451, 24)
        Me.LayoutControlItem13.Text = "Project Desc"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(97, 13)
        Me.LayoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.wo_pt_id_prj
        Me.LayoutControlItem20.CustomizationFormText = "Part Number Project"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem20.Text = "Part Number Project"
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.wo_cost
        Me.LayoutControlItem21.CustomizationFormText = "Cost"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem21.Text = "Cost"
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(97, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.AllowHotTrack = False
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(226, 96)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(225, 24)
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.wo_wog_code
        Me.LayoutControlItem22.CustomizationFormText = "Work Order Group"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(226, 0)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem22.Text = "Work Order Group"
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(97, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(226, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(225, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem5})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 288)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(475, 48)
        Me.LayoutControlGroup6.Text = "LayoutControlGroup6"
        Me.LayoutControlGroup6.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.wo_qty_ord
        Me.LayoutControlItem3.CustomizationFormText = "Qty Order"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(226, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem3.Text = "Qty Create"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.wo_qty
        Me.LayoutControlItem5.CustomizationFormText = "Qty Order"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem5.Text = "Qty"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.CustomizationFormText = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem23, Me.LayoutControlItem24})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(0, 336)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(475, 82)
        Me.LayoutControlGroup7.Text = "LayoutControlGroup7"
        Me.LayoutControlGroup7.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.wo_due_date
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(226, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem6.Text = "Due Date"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.wo_ord_date
        Me.LayoutControlItem4.CustomizationFormText = "Order Date"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem4.Text = "Order Date"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(97, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 48)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(451, 10)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.wo_start_date
        Me.LayoutControlItem23.CustomizationFormText = "Start Date"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem23.Text = "Start Date"
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.wo_end_date
        Me.LayoutControlItem24.CustomizationFormText = "End Date"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(226, 24)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(225, 24)
        Me.LayoutControlItem24.Text = "End Date"
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.CustomizationFormText = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem8, Me.EmptySpaceItem2, Me.LayoutControlItem9})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(475, 72)
        Me.LayoutControlGroup8.Text = "LayoutControlGroup8"
        Me.LayoutControlGroup8.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.wo_remarks
        Me.LayoutControlItem10.CustomizationFormText = "Remarks"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(451, 24)
        Me.LayoutControlItem10.Text = "Remarks"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.wo_ro_id
        Me.LayoutControlItem8.CustomizationFormText = "Routing"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem8.Text = "Routing"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(97, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(386, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(65, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.wo_insheet_pct
        Me.LayoutControlItem9.CustomizationFormText = "Yield"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(226, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(160, 24)
        Me.LayoutControlItem9.Text = "Insheet"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(97, 13)
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.CustomizationFormText = "Routing"
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem15})
        Me.LayoutControlGroup9.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup9.Name = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(475, 418)
        Me.LayoutControlGroup9.Text = "Routing"
        Me.LayoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.gc_edit
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(475, 418)
        Me.LayoutControlItem15.Text = "LayoutControlItem15"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerBottom})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.BackColor = System.Drawing.SystemColors.Control
        Me.hideContainerBottom.Controls.Add(Me.dp_detail)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 641)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(882, 19)
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("7e1d1e4a-87f8-4b89-8a0c-ee59cc8ee030")
        Me.dp_detail.Location = New System.Drawing.Point(0, 0)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(882, 200)
        Me.dp_detail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.SavedIndex = 0
        Me.dp_detail.Size = New System.Drawing.Size(882, 200)
        Me.dp_detail.Text = "Data Detail"
        Me.dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(876, 172)
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
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_detail.Size = New System.Drawing.Size(876, 172)
        Me.xtc_detail.TabIndex = 3
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_woroute})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(874, 151)
        Me.xtp_detail.Text = "Data Material"
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
        Me.scc_detail.Size = New System.Drawing.Size(874, 151)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(874, 151)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail.OptionsView.ShowFooter = True
        '
        'xtp_woroute
        '
        Me.xtp_woroute.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_woroute.Name = "xtp_woroute"
        Me.xtp_woroute.Size = New System.Drawing.Size(874, 151)
        Me.xtp_woroute.Text = "Routing"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gc_routing)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl1.Size = New System.Drawing.Size(874, 151)
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_routing
        '
        Me.gc_routing.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_routing.Location = New System.Drawing.Point(0, 0)
        Me.gc_routing.MainView = Me.gv_routing
        Me.gc_routing.Name = "gc_routing"
        Me.gc_routing.Size = New System.Drawing.Size(874, 151)
        Me.gc_routing.TabIndex = 0
        Me.gc_routing.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_routing})
        '
        'gv_routing
        '
        Me.gv_routing.GridControl = Me.gc_routing
        Me.gv_routing.Name = "gv_routing"
        Me.gv_routing.OptionsView.ColumnAutoWidth = False
        Me.gv_routing.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_routing.OptionsView.ShowFooter = True
        '
        'EmptySpaceItem9
        '
        Me.EmptySpaceItem9.AllowHotTrack = False
        Me.EmptySpaceItem9.CustomizationFormText = "EmptySpaceItem9"
        Me.EmptySpaceItem9.Location = New System.Drawing.Point(0, 48)
        Me.EmptySpaceItem9.Name = "EmptySpaceItem9"
        Me.EmptySpaceItem9.Size = New System.Drawing.Size(802, 10)
        Me.EmptySpaceItem9.Text = "EmptySpaceItem9"
        Me.EmptySpaceItem9.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.AllowHotTrack = False
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(0, 24)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(802, 10)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(218, 9)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 31
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(169, 12)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 30
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(58, 9)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 29
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(6, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl1.TabIndex = 28
        Me.LabelControl1.Text = "WO Date"
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(809, 336)
        Me.LayoutControlItem16.Text = "LayoutControlItem16"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextToControlDistance = 0
        Me.LayoutControlItem16.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem16"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem14.Name = "LayoutControlItem16"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(809, 336)
        Me.LayoutControlItem14.Text = "LayoutControlItem16"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextToControlDistance = 0
        Me.LayoutControlItem14.TextVisible = False
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(775, 12)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl3.TabIndex = 32
        Me.LabelControl3.Text = "."
        '
        'BtUpdateSod
        '
        Me.BtUpdateSod.Location = New System.Drawing.Point(466, 5)
        Me.BtUpdateSod.Name = "BtUpdateSod"
        Me.BtUpdateSod.Size = New System.Drawing.Size(96, 23)
        Me.BtUpdateSod.TabIndex = 33
        Me.BtUpdateSod.Text = "Update Sod_Oid"
        Me.BtUpdateSod.Visible = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(587, 10)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl4.TabIndex = 34
        Me.LabelControl4.Text = "-"
        '
        'FWorkOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(882, 660)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FWorkOrder"
        Me.Text = "Work Order"
        Me.Controls.SetChildIndex(Me.hideContainerBottom, 0)
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.wo_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_end_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_start_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_wog_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_cost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_pt_id_prj.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_insheet_pct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_qty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_ps_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_ref_rework.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pjc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_so_oid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_ro_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_ord_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_ord_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_due_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_qty_ord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wo_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.xtp_woroute.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_routing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_routing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents wo_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Public WithEvents wo_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents wo_qty_ord As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmptySpaceItem9 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents wo_due_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem

    Friend WithEvents wo_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents wo_ord_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents wo_pt_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Public WithEvents wo_ro_id As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents pjc_desc As DevExpress.XtraEditors.TextEdit
    Public WithEvents wo_so_oid As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents xtp_woroute As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_routing As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_routing As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents gc_edit As DevExpress.XtraGrid.GridControl
    Friend WithEvents wo_type As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tcg_header As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents wo_ref_rework As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents wo_ps_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents wo_insheet_pct As DevExpress.XtraEditors.TextEdit
    Public WithEvents wo_qty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Public WithEvents wo_cost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UpdateRoutingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegenerateRoutingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents wo_wog_code As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents wo_end_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents wo_start_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Public WithEvents wo_pt_id_prj As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents BtUpdateSod As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl

End Class
