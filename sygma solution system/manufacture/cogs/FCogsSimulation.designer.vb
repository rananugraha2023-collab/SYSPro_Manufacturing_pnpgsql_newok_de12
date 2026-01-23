<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCogsSimulation
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
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.cogsc_yield = New DevExpress.XtraEditors.TextEdit
        Me.BtGenerate = New DevExpress.XtraEditors.SimpleButton
        Me.cogsc_unit = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_indirect_non_material_ttl = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_indirect_material_ttl = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_qty_prod = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_qty = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_pt_id = New DevExpress.XtraEditors.ButtonEdit
        Me.cogsc_total = New DevExpress.XtraEditors.TextEdit
        Me.gc_edit_indir_nonmat = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_indir_nonmat = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cogsc_direct_material_ttl = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_cs_id = New DevExpress.XtraEditors.LookUpEdit
        Me.cogsc_direct_non_material_ttl = New DevExpress.XtraEditors.TextEdit
        Me.gc_edit_dir_mat = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_dir_mat = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gc_edit_dir_nonmat = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_dir_nonmat = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gc_edit_indir_mat = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_indir_mat = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.cogsc_remarks = New DevExpress.XtraEditors.TextEdit
        Me.cogsc_date = New DevExpress.XtraEditors.DateEdit
        Me.cogsc_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_header = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup11 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup12 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_detail = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lcg_data = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup13 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_item = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_item = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_item = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_route = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail3 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_route = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_route = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_raw = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_raw = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_raw = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_acc = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail_acc = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_acc = New DevExpress.XtraGrid.Views.Grid.GridView
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
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.cogsc_yield.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_unit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_indirect_non_material_ttl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_indirect_material_ttl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_qty_prod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_qty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_total.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_indir_nonmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_indir_nonmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_direct_material_ttl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_cs_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_direct_non_material_ttl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_dir_mat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_dir_mat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_dir_nonmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_dir_nonmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_indir_mat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_indir_mat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cogsc_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcg_data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_item.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail_item, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_route.SuspendLayout()
        CType(Me.scc_detail3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail3.SuspendLayout()
        CType(Me.gc_detail_route, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_route, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_raw.SuspendLayout()
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail2.SuspendLayout()
        CType(Me.gc_detail_raw, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_raw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_acc.SuspendLayout()
        CType(Me.gc_detail_acc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_acc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(992, 494)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(994, 548)
        Me.scc_master.SplitterPosition = 27
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(992, 494)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(982, 449)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(994, 515)
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(212, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 19
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(163, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(52, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 17
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(2, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "First Date"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(982, 484)
        Me.gc_master.TabIndex = 4
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.cogsc_yield)
        Me.lci_master.Controls.Add(Me.BtGenerate)
        Me.lci_master.Controls.Add(Me.cogsc_unit)
        Me.lci_master.Controls.Add(Me.cogsc_indirect_non_material_ttl)
        Me.lci_master.Controls.Add(Me.cogsc_indirect_material_ttl)
        Me.lci_master.Controls.Add(Me.cogsc_qty_prod)
        Me.lci_master.Controls.Add(Me.cogsc_qty)
        Me.lci_master.Controls.Add(Me.cogsc_pt_id)
        Me.lci_master.Controls.Add(Me.cogsc_total)
        Me.lci_master.Controls.Add(Me.gc_edit_indir_nonmat)
        Me.lci_master.Controls.Add(Me.cogsc_direct_material_ttl)
        Me.lci_master.Controls.Add(Me.cogsc_cs_id)
        Me.lci_master.Controls.Add(Me.cogsc_direct_non_material_ttl)
        Me.lci_master.Controls.Add(Me.gc_edit_dir_mat)
        Me.lci_master.Controls.Add(Me.gc_edit_dir_nonmat)
        Me.lci_master.Controls.Add(Me.gc_edit_indir_mat)
        Me.lci_master.Controls.Add(Me.cogsc_remarks)
        Me.lci_master.Controls.Add(Me.cogsc_date)
        Me.lci_master.Controls.Add(Me.cogsc_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(982, 449)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 3
        Me.lci_master.Text = "LayoutControl1"
        '
        'cogsc_yield
        '
        Me.cogsc_yield.Location = New System.Drawing.Point(598, 130)
        Me.cogsc_yield.Name = "cogsc_yield"
        Me.cogsc_yield.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_yield.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_yield.Properties.Mask.EditMask = "p"
        Me.cogsc_yield.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_yield.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_yield.Properties.ReadOnly = True
        Me.cogsc_yield.Size = New System.Drawing.Size(348, 20)
        Me.cogsc_yield.StyleController = Me.lci_master
        Me.cogsc_yield.TabIndex = 76
        '
        'BtGenerate
        '
        Me.BtGenerate.Location = New System.Drawing.Point(257, 190)
        Me.BtGenerate.Name = "BtGenerate"
        Me.BtGenerate.Size = New System.Drawing.Size(231, 22)
        Me.BtGenerate.StyleController = Me.lci_master
        Me.BtGenerate.TabIndex = 75
        Me.BtGenerate.Text = "Generate"
        '
        'cogsc_unit
        '
        Me.cogsc_unit.Location = New System.Drawing.Point(140, 348)
        Me.cogsc_unit.Name = "cogsc_unit"
        Me.cogsc_unit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_unit.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_unit.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_unit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_unit.Properties.Mask.EditMask = "n"
        Me.cogsc_unit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_unit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_unit.Properties.ReadOnly = True
        Me.cogsc_unit.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_unit.StyleController = Me.lci_master
        Me.cogsc_unit.TabIndex = 74
        '
        'cogsc_indirect_non_material_ttl
        '
        Me.cogsc_indirect_non_material_ttl.Location = New System.Drawing.Point(140, 300)
        Me.cogsc_indirect_non_material_ttl.Name = "cogsc_indirect_non_material_ttl"
        Me.cogsc_indirect_non_material_ttl.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_indirect_non_material_ttl.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_indirect_non_material_ttl.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_indirect_non_material_ttl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_indirect_non_material_ttl.Properties.Mask.EditMask = "n"
        Me.cogsc_indirect_non_material_ttl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_indirect_non_material_ttl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_indirect_non_material_ttl.Properties.ReadOnly = True
        Me.cogsc_indirect_non_material_ttl.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_indirect_non_material_ttl.StyleController = Me.lci_master
        Me.cogsc_indirect_non_material_ttl.TabIndex = 73
        '
        'cogsc_indirect_material_ttl
        '
        Me.cogsc_indirect_material_ttl.Location = New System.Drawing.Point(140, 276)
        Me.cogsc_indirect_material_ttl.Name = "cogsc_indirect_material_ttl"
        Me.cogsc_indirect_material_ttl.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_indirect_material_ttl.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_indirect_material_ttl.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_indirect_material_ttl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_indirect_material_ttl.Properties.Mask.EditMask = "n"
        Me.cogsc_indirect_material_ttl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_indirect_material_ttl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_indirect_material_ttl.Properties.ReadOnly = True
        Me.cogsc_indirect_material_ttl.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_indirect_material_ttl.StyleController = Me.lci_master
        Me.cogsc_indirect_material_ttl.TabIndex = 72
        '
        'cogsc_qty_prod
        '
        Me.cogsc_qty_prod.Location = New System.Drawing.Point(598, 154)
        Me.cogsc_qty_prod.Name = "cogsc_qty_prod"
        Me.cogsc_qty_prod.Properties.Mask.EditMask = "n"
        Me.cogsc_qty_prod.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_qty_prod.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_qty_prod.Properties.ReadOnly = True
        Me.cogsc_qty_prod.Properties.ValidateOnEnterKey = True
        Me.cogsc_qty_prod.Size = New System.Drawing.Size(348, 20)
        Me.cogsc_qty_prod.StyleController = Me.lci_master
        Me.cogsc_qty_prod.TabIndex = 71
        '
        'cogsc_qty
        '
        Me.cogsc_qty.Location = New System.Drawing.Point(140, 154)
        Me.cogsc_qty.Name = "cogsc_qty"
        Me.cogsc_qty.Properties.Mask.EditMask = "n"
        Me.cogsc_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_qty.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_qty.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_qty.StyleController = Me.lci_master
        Me.cogsc_qty.TabIndex = 70
        '
        'cogsc_pt_id
        '
        Me.cogsc_pt_id.Location = New System.Drawing.Point(140, 130)
        Me.cogsc_pt_id.Name = "cogsc_pt_id"
        Me.cogsc_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.cogsc_pt_id.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_pt_id.StyleController = Me.lci_master
        Me.cogsc_pt_id.TabIndex = 69
        '
        'cogsc_total
        '
        Me.cogsc_total.Location = New System.Drawing.Point(140, 324)
        Me.cogsc_total.Name = "cogsc_total"
        Me.cogsc_total.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_total.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_total.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_total.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_total.Properties.Mask.EditMask = "n"
        Me.cogsc_total.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_total.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_total.Properties.ReadOnly = True
        Me.cogsc_total.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_total.StyleController = Me.lci_master
        Me.cogsc_total.TabIndex = 66
        '
        'gc_edit_indir_nonmat
        '
        Me.gc_edit_indir_nonmat.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit_indir_nonmat.MainView = Me.gv_edit_indir_nonmat
        Me.gc_edit_indir_nonmat.Name = "gc_edit_indir_nonmat"
        Me.gc_edit_indir_nonmat.Size = New System.Drawing.Size(910, 333)
        Me.gc_edit_indir_nonmat.TabIndex = 65
        Me.gc_edit_indir_nonmat.UseEmbeddedNavigator = True
        Me.gc_edit_indir_nonmat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_indir_nonmat})
        '
        'gv_edit_indir_nonmat
        '
        Me.gv_edit_indir_nonmat.GridControl = Me.gc_edit_indir_nonmat
        Me.gv_edit_indir_nonmat.Name = "gv_edit_indir_nonmat"
        Me.gv_edit_indir_nonmat.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_indir_nonmat.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_indir_nonmat.OptionsView.ShowFooter = True
        Me.gv_edit_indir_nonmat.OptionsView.ShowGroupPanel = False
        '
        'cogsc_direct_material_ttl
        '
        Me.cogsc_direct_material_ttl.Location = New System.Drawing.Point(140, 228)
        Me.cogsc_direct_material_ttl.Name = "cogsc_direct_material_ttl"
        Me.cogsc_direct_material_ttl.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_direct_material_ttl.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_direct_material_ttl.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_direct_material_ttl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_direct_material_ttl.Properties.DisplayFormat.FormatString = "n"
        Me.cogsc_direct_material_ttl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cogsc_direct_material_ttl.Properties.EditFormat.FormatString = "n"
        Me.cogsc_direct_material_ttl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cogsc_direct_material_ttl.Properties.Mask.EditMask = "n"
        Me.cogsc_direct_material_ttl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_direct_material_ttl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_direct_material_ttl.Properties.ReadOnly = True
        Me.cogsc_direct_material_ttl.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_direct_material_ttl.StyleController = Me.lci_master
        Me.cogsc_direct_material_ttl.TabIndex = 58
        '
        'cogsc_cs_id
        '
        Me.cogsc_cs_id.Location = New System.Drawing.Point(596, 58)
        Me.cogsc_cs_id.Name = "cogsc_cs_id"
        Me.cogsc_cs_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cogsc_cs_id.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_cs_id.StyleController = Me.lci_master
        Me.cogsc_cs_id.TabIndex = 55
        '
        'cogsc_direct_non_material_ttl
        '
        Me.cogsc_direct_non_material_ttl.Location = New System.Drawing.Point(140, 252)
        Me.cogsc_direct_non_material_ttl.Name = "cogsc_direct_non_material_ttl"
        Me.cogsc_direct_non_material_ttl.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.cogsc_direct_non_material_ttl.Properties.Appearance.Options.UseBackColor = True
        Me.cogsc_direct_non_material_ttl.Properties.Appearance.Options.UseTextOptions = True
        Me.cogsc_direct_non_material_ttl.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.cogsc_direct_non_material_ttl.Properties.DisplayFormat.FormatString = "n"
        Me.cogsc_direct_non_material_ttl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cogsc_direct_non_material_ttl.Properties.EditFormat.FormatString = "n"
        Me.cogsc_direct_non_material_ttl.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cogsc_direct_non_material_ttl.Properties.Mask.EditMask = "n"
        Me.cogsc_direct_non_material_ttl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.cogsc_direct_non_material_ttl.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.cogsc_direct_non_material_ttl.Properties.ReadOnly = True
        Me.cogsc_direct_non_material_ttl.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_direct_non_material_ttl.StyleController = Me.lci_master
        Me.cogsc_direct_non_material_ttl.TabIndex = 54
        '
        'gc_edit_dir_mat
        '
        Me.gc_edit_dir_mat.DataMember = "pod_det"
        Me.gc_edit_dir_mat.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit_dir_mat.MainView = Me.gv_edit_dir_mat
        Me.gc_edit_dir_mat.Name = "gc_edit_dir_mat"
        Me.gc_edit_dir_mat.Size = New System.Drawing.Size(910, 333)
        Me.gc_edit_dir_mat.TabIndex = 40
        Me.gc_edit_dir_mat.UseEmbeddedNavigator = True
        Me.gc_edit_dir_mat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_dir_mat})
        '
        'gv_edit_dir_mat
        '
        Me.gv_edit_dir_mat.GridControl = Me.gc_edit_dir_mat
        Me.gv_edit_dir_mat.Name = "gv_edit_dir_mat"
        Me.gv_edit_dir_mat.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_dir_mat.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_dir_mat.OptionsView.ShowFooter = True
        Me.gv_edit_dir_mat.OptionsView.ShowGroupPanel = False
        '
        'gc_edit_dir_nonmat
        '
        Me.gc_edit_dir_nonmat.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit_dir_nonmat.MainView = Me.gv_edit_dir_nonmat
        Me.gc_edit_dir_nonmat.Name = "gc_edit_dir_nonmat"
        Me.gc_edit_dir_nonmat.Size = New System.Drawing.Size(910, 333)
        Me.gc_edit_dir_nonmat.TabIndex = 52
        Me.gc_edit_dir_nonmat.UseEmbeddedNavigator = True
        Me.gc_edit_dir_nonmat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_dir_nonmat})
        '
        'gv_edit_dir_nonmat
        '
        Me.gv_edit_dir_nonmat.GridControl = Me.gc_edit_dir_nonmat
        Me.gv_edit_dir_nonmat.Name = "gv_edit_dir_nonmat"
        Me.gv_edit_dir_nonmat.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_dir_nonmat.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_dir_nonmat.OptionsView.ShowFooter = True
        Me.gv_edit_dir_nonmat.OptionsView.ShowGroupPanel = False
        '
        'gc_edit_indir_mat
        '
        Me.gc_edit_indir_mat.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.gc_edit_indir_mat.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.gc_edit_indir_mat.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit_indir_mat.MainView = Me.gv_edit_indir_mat
        Me.gc_edit_indir_mat.Name = "gc_edit_indir_mat"
        Me.gc_edit_indir_mat.Size = New System.Drawing.Size(910, 333)
        Me.gc_edit_indir_mat.TabIndex = 50
        Me.gc_edit_indir_mat.UseEmbeddedNavigator = True
        Me.gc_edit_indir_mat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_indir_mat})
        '
        'gv_edit_indir_mat
        '
        Me.gv_edit_indir_mat.GridControl = Me.gc_edit_indir_mat
        Me.gv_edit_indir_mat.Name = "gv_edit_indir_mat"
        Me.gv_edit_indir_mat.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_indir_mat.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_indir_mat.OptionsView.ShowFooter = True
        Me.gv_edit_indir_mat.OptionsView.ShowGroupPanel = False
        '
        'cogsc_remarks
        '
        Me.cogsc_remarks.Location = New System.Drawing.Point(140, 106)
        Me.cogsc_remarks.Name = "cogsc_remarks"
        Me.cogsc_remarks.Size = New System.Drawing.Size(806, 20)
        Me.cogsc_remarks.StyleController = Me.lci_master
        Me.cogsc_remarks.TabIndex = 12
        '
        'cogsc_date
        '
        Me.cogsc_date.EditValue = Nothing
        Me.cogsc_date.Location = New System.Drawing.Point(140, 82)
        Me.cogsc_date.Name = "cogsc_date"
        Me.cogsc_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cogsc_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.cogsc_date.Size = New System.Drawing.Size(350, 20)
        Me.cogsc_date.StyleController = Me.lci_master
        Me.cogsc_date.TabIndex = 9
        '
        'cogsc_en_id
        '
        Me.cogsc_en_id.Location = New System.Drawing.Point(140, 58)
        Me.cogsc_en_id.Name = "cogsc_en_id"
        Me.cogsc_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cogsc_en_id.Size = New System.Drawing.Size(348, 20)
        Me.cogsc_en_id.StyleController = Me.lci_master
        Me.cogsc_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_header})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(982, 449)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'tcg_header
        '
        Me.tcg_header.CustomizationFormText = "TabbedControlGroup2"
        Me.tcg_header.HeaderOrientation = DevExpress.XtraTab.TabOrientation.Horizontal
        Me.tcg_header.Location = New System.Drawing.Point(0, 0)
        Me.tcg_header.Name = "tcg_header"
        Me.tcg_header.SelectedTabPage = Me.LayoutControlGroup2
        Me.tcg_header.SelectedTabPageIndex = 0
        Me.tcg_header.Size = New System.Drawing.Size(962, 429)
        Me.tcg_header.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup5})
        Me.tcg_header.Text = "tcg_header"
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Header"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup11, Me.LayoutControlGroup12, Me.EmptySpaceItem6, Me.LayoutControlItem13, Me.EmptySpaceItem4, Me.EmptySpaceItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(938, 383)
        Me.LayoutControlGroup2.Text = "Header"
        '
        'LayoutControlGroup11
        '
        Me.LayoutControlGroup11.CustomizationFormText = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem6, Me.LayoutControlItem24, Me.LayoutControlItem5, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.EmptySpaceItem1, Me.LayoutControlItem14})
        Me.LayoutControlGroup11.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup11.Name = "LayoutControlGroup11"
        Me.LayoutControlGroup11.Size = New System.Drawing.Size(938, 144)
        Me.LayoutControlGroup11.Text = "LayoutControlGroup11"
        Me.LayoutControlGroup11.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cogsc_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(456, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.cogsc_date
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem6.Text = "Effective Date"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.cogsc_cs_id
        Me.LayoutControlItem24.CustomizationFormText = "Account Prepayment"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(456, 0)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem24.Text = "Cost Set"
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem24.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.cogsc_pt_id
        Me.LayoutControlItem5.CustomizationFormText = "Barang Jadi"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem5.Text = "Part Number"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.cogsc_qty
        Me.LayoutControlItem7.CustomizationFormText = "QTY"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem7.Text = "Qty Order"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.cogsc_qty_prod
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(458, 96)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(456, 24)
        Me.LayoutControlItem8.Text = "Qty Production"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.cogsc_remarks
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(914, 24)
        Me.LayoutControlItem9.Text = "Remarks"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(100, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(458, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(456, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.cogsc_yield
        Me.LayoutControlItem14.CustomizationFormText = "Yield"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(458, 72)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(456, 24)
        Me.LayoutControlItem14.Text = "Yield"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlGroup12
        '
        Me.LayoutControlGroup12.CustomizationFormText = "LayoutControlGroup12"
        Me.LayoutControlGroup12.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.LayoutControlItem23, Me.EmptySpaceItem3, Me.EmptySpaceItem5, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem10, Me.LayoutControlItem11})
        Me.LayoutControlGroup12.Location = New System.Drawing.Point(0, 170)
        Me.LayoutControlGroup12.Name = "LayoutControlGroup12"
        Me.LayoutControlGroup12.Size = New System.Drawing.Size(938, 168)
        Me.LayoutControlGroup12.Text = "LayoutControlGroup12"
        Me.LayoutControlGroup12.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.cogsc_direct_material_ttl
        Me.LayoutControlItem4.CustomizationFormText = "Prepayment Balance"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem4.Text = "Direct Material"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.cogsc_direct_non_material_ttl
        Me.LayoutControlItem23.CustomizationFormText = "Prepayment"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem23.Text = "Direct Non Material"
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(100, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(458, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(456, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(458, 24)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(456, 120)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.cogsc_total
        Me.LayoutControlItem2.CustomizationFormText = "Total"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem2.Text = "Total Cost"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.cogsc_indirect_material_ttl
        Me.LayoutControlItem3.CustomizationFormText = "Indirect Material"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem3.Text = "Indirect Material"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.cogsc_indirect_non_material_ttl
        Me.LayoutControlItem10.CustomizationFormText = "Indirect Non Material"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem10.Text = "Indirect Non Material"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(100, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.cogsc_unit
        Me.LayoutControlItem11.CustomizationFormText = "Unit Cost"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(458, 24)
        Me.LayoutControlItem11.Text = "Unit Cost"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(100, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(0, 338)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(938, 45)
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.BtGenerate
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(233, 144)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(235, 26)
        Me.LayoutControlItem13.Text = "LayoutControlItem13"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextToControlDistance = 0
        Me.LayoutControlItem13.TextVisible = False
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(468, 144)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(470, 26)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 144)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(233, 26)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "Detail"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_detail})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(938, 383)
        Me.LayoutControlGroup5.Text = "Detail"
        '
        'tcg_detail
        '
        Me.tcg_detail.CustomizationFormText = "TabbedControlGroup1"
        Me.tcg_detail.Location = New System.Drawing.Point(0, 0)
        Me.tcg_detail.Name = "tcg_detail"
        Me.tcg_detail.SelectedTabPage = Me.LayoutControlGroup3
        Me.tcg_detail.SelectedTabPageIndex = 1
        Me.tcg_detail.Size = New System.Drawing.Size(938, 383)
        Me.tcg_detail.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lcg_data, Me.LayoutControlGroup3, Me.LayoutControlGroup4, Me.LayoutControlGroup13})
        Me.tcg_detail.Text = "tcg_detail"
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Distribution Line"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem21})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlGroup3.Text = "Direct Non Material"
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.gc_edit_dir_nonmat
        Me.LayoutControlItem21.CustomizationFormText = "LayoutControlItem21"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlItem21.Text = "LayoutControlItem21"
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem21.TextToControlDistance = 0
        Me.LayoutControlItem21.TextVisible = False
        '
        'lcg_data
        '
        Me.lcg_data.CustomizationFormText = "Data"
        Me.lcg_data.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem15})
        Me.lcg_data.Location = New System.Drawing.Point(0, 0)
        Me.lcg_data.Name = "lcg_data"
        Me.lcg_data.Size = New System.Drawing.Size(914, 337)
        Me.lcg_data.Text = "Direct Raw Material"
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.gc_edit_dir_mat
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlItem15.Text = "LayoutControlItem15"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "List Receive Item"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlGroup4.Text = "Indirect Material"
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.gc_edit_indir_mat
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlItem12.Text = "LayoutControlItem12"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlGroup13
        '
        Me.LayoutControlGroup13.CustomizationFormText = "Account"
        Me.LayoutControlGroup13.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem31})
        Me.LayoutControlGroup13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup13.Name = "LayoutControlGroup13"
        Me.LayoutControlGroup13.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlGroup13.Text = "Indirect Non Material"
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.Control = Me.gc_edit_indir_nonmat
        Me.LayoutControlItem31.CustomizationFormText = "LayoutControlItem31"
        Me.LayoutControlItem31.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Size = New System.Drawing.Size(914, 337)
        Me.LayoutControlItem31.Text = "LayoutControlItem31"
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem31.TextToControlDistance = 0
        Me.LayoutControlItem31.TextVisible = False
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dp_detail})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("65f162b6-5b70-479e-8496-bb8c125024ef")
        Me.dp_detail.Location = New System.Drawing.Point(0, 548)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(994, 200)
        Me.dp_detail.Size = New System.Drawing.Size(994, 200)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(988, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_item
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_detail.Size = New System.Drawing.Size(988, 172)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_item, Me.xtp_route, Me.xtp_raw, Me.xtp_acc})
        '
        'xtp_item
        '
        Me.xtp_item.Controls.Add(Me.scc_detail)
        Me.xtp_item.Name = "xtp_item"
        Me.xtp_item.Size = New System.Drawing.Size(986, 151)
        Me.xtp_item.Text = "Direct Raw Material"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_detail_item)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(986, 151)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail_item
        '
        Me.gc_detail_item.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_item.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_item.MainView = Me.gv_detail_item
        Me.gc_detail_item.Name = "gc_detail_item"
        Me.gc_detail_item.Size = New System.Drawing.Size(986, 151)
        Me.gc_detail_item.TabIndex = 0
        Me.gc_detail_item.UseEmbeddedNavigator = True
        Me.gc_detail_item.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_item})
        '
        'gv_detail_item
        '
        Me.gv_detail_item.GridControl = Me.gc_detail_item
        Me.gv_detail_item.Name = "gv_detail_item"
        Me.gv_detail_item.OptionsView.ShowGroupPanel = False
        '
        'xtp_route
        '
        Me.xtp_route.Controls.Add(Me.scc_detail3)
        Me.xtp_route.Name = "xtp_route"
        Me.xtp_route.Size = New System.Drawing.Size(986, 151)
        Me.xtp_route.Text = "Direct Non Material"
        '
        'scc_detail3
        '
        Me.scc_detail3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail3.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail3.Name = "scc_detail3"
        Me.scc_detail3.Panel1.Controls.Add(Me.gc_detail_route)
        Me.scc_detail3.Panel1.Text = "Panel1"
        Me.scc_detail3.Panel2.Text = "Panel2"
        Me.scc_detail3.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail3.Size = New System.Drawing.Size(986, 151)
        Me.scc_detail3.TabIndex = 2
        Me.scc_detail3.Text = "SplitContainerControl1"
        '
        'gc_detail_route
        '
        Me.gc_detail_route.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_route.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_route.MainView = Me.gv_detail_route
        Me.gc_detail_route.Name = "gc_detail_route"
        Me.gc_detail_route.Size = New System.Drawing.Size(986, 151)
        Me.gc_detail_route.TabIndex = 0
        Me.gc_detail_route.UseEmbeddedNavigator = True
        Me.gc_detail_route.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_route})
        '
        'gv_detail_route
        '
        Me.gv_detail_route.GridControl = Me.gc_detail_route
        Me.gv_detail_route.Name = "gv_detail_route"
        Me.gv_detail_route.OptionsView.ShowGroupPanel = False
        '
        'xtp_raw
        '
        Me.xtp_raw.Controls.Add(Me.scc_detail2)
        Me.xtp_raw.Name = "xtp_raw"
        Me.xtp_raw.Size = New System.Drawing.Size(986, 151)
        Me.xtp_raw.Text = "Indirect Material"
        '
        'scc_detail2
        '
        Me.scc_detail2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail2.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail2.Name = "scc_detail2"
        Me.scc_detail2.Panel1.Controls.Add(Me.gc_detail_raw)
        Me.scc_detail2.Panel1.Text = "Panel1"
        Me.scc_detail2.Panel2.Text = "Panel2"
        Me.scc_detail2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail2.Size = New System.Drawing.Size(986, 151)
        Me.scc_detail2.TabIndex = 1
        Me.scc_detail2.Text = "SplitContainerControl1"
        '
        'gc_detail_raw
        '
        Me.gc_detail_raw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_raw.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_raw.MainView = Me.gv_detail_raw
        Me.gc_detail_raw.Name = "gc_detail_raw"
        Me.gc_detail_raw.Size = New System.Drawing.Size(986, 151)
        Me.gc_detail_raw.TabIndex = 0
        Me.gc_detail_raw.UseEmbeddedNavigator = True
        Me.gc_detail_raw.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_raw})
        '
        'gv_detail_raw
        '
        Me.gv_detail_raw.GridControl = Me.gc_detail_raw
        Me.gv_detail_raw.Name = "gv_detail_raw"
        Me.gv_detail_raw.OptionsView.ShowGroupPanel = False
        '
        'xtp_acc
        '
        Me.xtp_acc.Controls.Add(Me.gc_detail_acc)
        Me.xtp_acc.Name = "xtp_acc"
        Me.xtp_acc.Size = New System.Drawing.Size(986, 151)
        Me.xtp_acc.Text = "Indirect Non Material"
        '
        'gc_detail_acc
        '
        Me.gc_detail_acc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_acc.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_acc.MainView = Me.gv_detail_acc
        Me.gc_detail_acc.Name = "gc_detail_acc"
        Me.gc_detail_acc.Size = New System.Drawing.Size(986, 151)
        Me.gc_detail_acc.TabIndex = 1
        Me.gc_detail_acc.UseEmbeddedNavigator = True
        Me.gc_detail_acc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_acc})
        '
        'gv_detail_acc
        '
        Me.gv_detail_acc.GridControl = Me.gc_detail_acc
        Me.gv_detail_acc.Name = "gv_detail_acc"
        Me.gv_detail_acc.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_detail_acc.OptionsView.ColumnAutoWidth = False
        Me.gv_detail_acc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail_acc.OptionsView.ShowGroupPanel = False
        '
        'FCogsSimulation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(994, 748)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FCogsSimulation"
        Me.Text = "COGS Simulation"
        Me.Controls.SetChildIndex(Me.dp_detail, 0)
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
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.cogsc_yield.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_unit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_indirect_non_material_ttl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_indirect_material_ttl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_qty_prod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_qty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_total.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_indir_nonmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_indir_nonmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_direct_material_ttl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_cs_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_direct_non_material_ttl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_dir_mat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_dir_mat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_dir_nonmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_dir_nonmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_indir_mat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_indir_mat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cogsc_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcg_data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_item.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail_item, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_route.ResumeLayout(False)
        CType(Me.scc_detail3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail3.ResumeLayout(False)
        CType(Me.gc_detail_route, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_route, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_raw.ResumeLayout(False)
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail2.ResumeLayout(False)
        CType(Me.gc_detail_raw, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_raw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_acc.ResumeLayout(False)
        CType(Me.gc_detail_acc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_acc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents gc_edit_dir_mat As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_dir_mat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cogsc_cs_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents cogsc_direct_non_material_ttl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gc_edit_dir_nonmat As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_dir_nonmat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cogsc_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cogsc_date As DevExpress.XtraEditors.DateEdit
    Public WithEvents cogsc_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tcg_header As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup11 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup12 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tcg_detail As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents lcg_data As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_item As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_item As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_item As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_raw As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_raw As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_raw As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_route As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail3 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_route As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_route As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cogsc_direct_material_ttl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents cogsc_total As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents xtp_acc As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail_acc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_acc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents gc_edit_indir_nonmat As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_indir_nonmat As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents gc_edit_indir_mat As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_indir_mat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cogsc_qty_prod As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cogsc_qty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cogsc_indirect_non_material_ttl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cogsc_indirect_material_ttl As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cogsc_unit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup13 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents cogsc_pt_id As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents cogsc_yield As DevExpress.XtraEditors.TextEdit

End Class
