<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDPackingSheetPrintOutbySO
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
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateTerbilangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.pcs_so_shipment = New DevExpress.XtraEditors.CheckEdit
        Me.pcksl_remarks = New DevExpress.XtraEditors.TextEdit
        Me.be_so_code = New DevExpress.XtraEditors.ButtonEdit
        Me.gc_edit_so = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_so = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.ce_close_transaction = New DevExpress.XtraEditors.CheckEdit
        Me.sb_retrieve_receive_item = New DevExpress.XtraEditors.SimpleButton
        Me.gc_edit_sales_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_shipment = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pcksl_sold_to = New DevExpress.XtraEditors.LookUpEdit
        Me.pcksl_due_date = New DevExpress.XtraEditors.DateEdit
        Me.pcksl_eff_date = New DevExpress.XtraEditors.DateEdit
        Me.pcksl_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pcksl_expt_date = New DevExpress.XtraEditors.DateEdit
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lcg_ar = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem
        Me.Remarks = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup13 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_detail = New DevExpress.XtraLayout.TabbedControlGroup
        Me.lcg_data = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.ce_pcksl_doc = New DevExpress.XtraEditors.CheckEdit
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.ControlContainer1 = New DevExpress.XtraBars.Docking.ControlContainer
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_po = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_so = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_so = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_receive = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_shipment = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_shipment = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.par_so = New DevExpress.XtraEditors.ButtonEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.par_cus = New DevExpress.XtraEditors.ButtonEdit
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.CheckEdit1 = New DevExpress.XtraEditors.CheckEdit
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
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.pcs_so_shipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_so_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_so, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_so, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_close_transaction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_sales_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_shipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_sold_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_due_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_eff_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_eff_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_expt_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcksl_expt_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcg_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcg_data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_pcksl_doc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_po.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail_so, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_so, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_receive.SuspendLayout()
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail2.SuspendLayout()
        CType(Me.gc_detail_shipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_shipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.par_so.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.par_cus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(992, 489)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CheckEdit1)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl4)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.par_cus)
        Me.scc_master.Panel1.Controls.Add(Me.par_so)
        Me.scc_master.Panel1.Controls.Add(Me.ce_pcksl_doc)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(994, 543)
        Me.scc_master.SplitterPosition = 27
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(994, 510)
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(212, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
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
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
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
        Me.gc_master.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(982, 479)
        Me.gc_master.TabIndex = 4
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateTerbilangToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(164, 26)
        '
        'UpdateTerbilangToolStripMenuItem
        '
        Me.UpdateTerbilangToolStripMenuItem.Name = "UpdateTerbilangToolStripMenuItem"
        Me.UpdateTerbilangToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.UpdateTerbilangToolStripMenuItem.Text = "Update Terbilang"
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
        Me.lci_master.Controls.Add(Me.pcs_so_shipment)
        Me.lci_master.Controls.Add(Me.pcksl_remarks)
        Me.lci_master.Controls.Add(Me.be_so_code)
        Me.lci_master.Controls.Add(Me.gc_edit_so)
        Me.lci_master.Controls.Add(Me.ce_close_transaction)
        Me.lci_master.Controls.Add(Me.sb_retrieve_receive_item)
        Me.lci_master.Controls.Add(Me.gc_edit_sales_detail)
        Me.lci_master.Controls.Add(Me.pcksl_sold_to)
        Me.lci_master.Controls.Add(Me.pcksl_due_date)
        Me.lci_master.Controls.Add(Me.pcksl_eff_date)
        Me.lci_master.Controls.Add(Me.pcksl_en_id)
        Me.lci_master.Controls.Add(Me.pcksl_expt_date)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem26, Me.lcg_ar, Me.LayoutControlItem2})
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 3
        Me.lci_master.Text = "LayoutControl1"
        '
        'pcs_so_shipment
        '
        Me.pcs_so_shipment.Location = New System.Drawing.Point(593, 104)
        Me.pcs_so_shipment.Name = "pcs_so_shipment"
        Me.pcs_so_shipment.Properties.Caption = "By Shipment"
        Me.pcs_so_shipment.Size = New System.Drawing.Size(124, 19)
        Me.pcs_so_shipment.StyleController = Me.lci_master
        Me.pcs_so_shipment.TabIndex = 63
        '
        'pcksl_remarks
        '
        Me.pcksl_remarks.Location = New System.Drawing.Point(111, 152)
        Me.pcksl_remarks.Name = "pcksl_remarks"
        Me.pcksl_remarks.Size = New System.Drawing.Size(396, 20)
        Me.pcksl_remarks.StyleController = Me.lci_master
        Me.pcksl_remarks.TabIndex = 61
        '
        'be_so_code
        '
        Me.be_so_code.Location = New System.Drawing.Point(124, 72)
        Me.be_so_code.Name = "be_so_code"
        Me.be_so_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_so_code.Properties.ReadOnly = True
        Me.be_so_code.Size = New System.Drawing.Size(360, 20)
        Me.be_so_code.StyleController = Me.lci_master
        Me.be_so_code.TabIndex = 59
        '
        'gc_edit_so
        '
        Me.gc_edit_so.DataMember = "pod_det"
        Me.gc_edit_so.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.gc_edit_so.Location = New System.Drawing.Point(36, 268)
        Me.gc_edit_so.MainView = Me.gv_edit_so
        Me.gc_edit_so.Name = "gc_edit_so"
        Me.gc_edit_so.Size = New System.Drawing.Size(471, 46)
        Me.gc_edit_so.TabIndex = 40
        Me.gc_edit_so.UseEmbeddedNavigator = True
        Me.gc_edit_so.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_so})
        '
        'gv_edit_so
        '
        Me.gv_edit_so.GridControl = Me.gc_edit_so
        Me.gv_edit_so.Name = "gv_edit_so"
        Me.gv_edit_so.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_so.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_so.OptionsView.ShowFooter = True
        Me.gv_edit_so.OptionsView.ShowGroupPanel = False
        '
        'ce_close_transaction
        '
        Me.ce_close_transaction.Location = New System.Drawing.Point(19, 72)
        Me.ce_close_transaction.Name = "ce_close_transaction"
        Me.ce_close_transaction.Properties.Caption = "Close Transaction"
        Me.ce_close_transaction.Size = New System.Drawing.Size(942, 19)
        Me.ce_close_transaction.StyleController = Me.lci_master
        Me.ce_close_transaction.TabIndex = 60
        '
        'sb_retrieve_receive_item
        '
        Me.sb_retrieve_receive_item.Location = New System.Drawing.Point(36, 268)
        Me.sb_retrieve_receive_item.Name = "sb_retrieve_receive_item"
        Me.sb_retrieve_receive_item.Size = New System.Drawing.Size(56, 22)
        Me.sb_retrieve_receive_item.StyleController = Me.lci_master
        Me.sb_retrieve_receive_item.TabIndex = 51
        Me.sb_retrieve_receive_item.Text = "Retrieve"
        '
        'gc_edit_sales_detail
        '
        Me.gc_edit_sales_detail.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.gc_edit_sales_detail.Location = New System.Drawing.Point(36, 294)
        Me.gc_edit_sales_detail.MainView = Me.gv_edit_shipment
        Me.gc_edit_sales_detail.Name = "gc_edit_sales_detail"
        Me.gc_edit_sales_detail.Size = New System.Drawing.Size(471, 20)
        Me.gc_edit_sales_detail.TabIndex = 50
        Me.gc_edit_sales_detail.UseEmbeddedNavigator = True
        Me.gc_edit_sales_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_shipment})
        '
        'gv_edit_shipment
        '
        Me.gv_edit_shipment.GridControl = Me.gc_edit_sales_detail
        Me.gv_edit_shipment.Name = "gv_edit_shipment"
        Me.gv_edit_shipment.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_shipment.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_shipment.OptionsView.ShowFooter = True
        Me.gv_edit_shipment.OptionsView.ShowGroupPanel = False
        '
        'pcksl_sold_to
        '
        Me.pcksl_sold_to.Location = New System.Drawing.Point(348, 56)
        Me.pcksl_sold_to.Name = "pcksl_sold_to"
        Me.pcksl_sold_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pcksl_sold_to.Size = New System.Drawing.Size(159, 20)
        Me.pcksl_sold_to.StyleController = Me.lci_master
        Me.pcksl_sold_to.TabIndex = 48
        '
        'pcksl_due_date
        '
        Me.pcksl_due_date.EditValue = Nothing
        Me.pcksl_due_date.Location = New System.Drawing.Point(111, 128)
        Me.pcksl_due_date.Name = "pcksl_due_date"
        Me.pcksl_due_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pcksl_due_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pcksl_due_date.Size = New System.Drawing.Size(159, 20)
        Me.pcksl_due_date.StyleController = Me.lci_master
        Me.pcksl_due_date.TabIndex = 47
        '
        'pcksl_eff_date
        '
        Me.pcksl_eff_date.Cursor = System.Windows.Forms.Cursors.Default
        Me.pcksl_eff_date.EditValue = Nothing
        Me.pcksl_eff_date.Location = New System.Drawing.Point(111, 104)
        Me.pcksl_eff_date.Name = "pcksl_eff_date"
        Me.pcksl_eff_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pcksl_eff_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pcksl_eff_date.Size = New System.Drawing.Size(159, 20)
        Me.pcksl_eff_date.StyleController = Me.lci_master
        Me.pcksl_eff_date.TabIndex = 9
        '
        'pcksl_en_id
        '
        Me.pcksl_en_id.Location = New System.Drawing.Point(111, 56)
        Me.pcksl_en_id.Name = "pcksl_en_id"
        Me.pcksl_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pcksl_en_id.Size = New System.Drawing.Size(158, 20)
        Me.pcksl_en_id.StyleController = Me.lci_master
        Me.pcksl_en_id.TabIndex = 4
        '
        'pcksl_expt_date
        '
        Me.pcksl_expt_date.EditValue = Nothing
        Me.pcksl_expt_date.Location = New System.Drawing.Point(349, 104)
        Me.pcksl_expt_date.Name = "pcksl_expt_date"
        Me.pcksl_expt_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pcksl_expt_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pcksl_expt_date.Size = New System.Drawing.Size(158, 20)
        Me.pcksl_expt_date.StyleController = Me.lci_master
        Me.pcksl_expt_date.TabIndex = 49
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.be_so_code
        Me.LayoutControlItem16.CustomizationFormText = "SO Number"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(476, 31)
        Me.LayoutControlItem16.Text = "SO Number"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem16.TextToControlDistance = 5
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.Control = Me.ce_close_transaction
        Me.LayoutControlItem26.CustomizationFormText = "LayoutControlItem26"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(953, 30)
        Me.LayoutControlItem26.Text = "LayoutControlItem26"
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem26.TextToControlDistance = 0
        Me.LayoutControlItem26.TextVisible = False
        '
        'lcg_ar
        '
        Me.lcg_ar.CustomizationFormText = "List AR"
        Me.lcg_ar.Location = New System.Drawing.Point(0, 0)
        Me.lcg_ar.Name = "lcg_ar"
        Me.lcg_ar.Size = New System.Drawing.Size(914, 149)
        Me.lcg_ar.Text = "List AR"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pcs_so_shipment
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(457, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(228, 24)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(50, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.TabbedControlGroup1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 350)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Header"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6, Me.LayoutControlGroup13})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(523, 188)
        Me.LayoutControlGroup2.Text = "Header"
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6, Me.LayoutControlItem19, Me.Remarks, Me.LayoutControlItem5, Me.EmptySpaceItem3})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(499, 96)
        Me.LayoutControlGroup6.Text = "LayoutControlGroup6"
        Me.LayoutControlGroup6.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.pcksl_eff_date
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(238, 24)
        Me.LayoutControlItem6.Text = "Effective Date"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(71, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.pcksl_due_date
        Me.LayoutControlItem19.CustomizationFormText = "Due Date"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(238, 24)
        Me.LayoutControlItem19.Text = "Due Date"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(71, 13)
        '
        'Remarks
        '
        Me.Remarks.Control = Me.pcksl_remarks
        Me.Remarks.CustomizationFormText = "Remarks"
        Me.Remarks.Location = New System.Drawing.Point(0, 48)
        Me.Remarks.Name = "Remarks"
        Me.Remarks.Size = New System.Drawing.Size(475, 24)
        Me.Remarks.Text = "Remarks"
        Me.Remarks.TextSize = New System.Drawing.Size(71, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.pcksl_expt_date
        Me.LayoutControlItem5.CustomizationFormText = "Expected Date"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(238, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(237, 24)
        Me.LayoutControlItem5.Text = "Expected Date"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(71, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(238, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(237, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup13
        '
        Me.LayoutControlGroup13.CustomizationFormText = "LayoutControlGroup13"
        Me.LayoutControlGroup13.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3})
        Me.LayoutControlGroup13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup13.Name = "LayoutControlGroup13"
        Me.LayoutControlGroup13.Size = New System.Drawing.Size(499, 48)
        Me.LayoutControlGroup13.Text = "LayoutControlGroup13"
        Me.LayoutControlGroup13.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.pcksl_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(237, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(71, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.pcksl_sold_to
        Me.LayoutControlItem3.CustomizationFormText = "Supplier"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(237, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(238, 24)
        Me.LayoutControlItem3.Text = "Customer"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(71, 13)
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 188)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup5
        Me.TabbedControlGroup1.SelectedTabPageIndex = 0
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(523, 142)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5})
        Me.TabbedControlGroup1.Text = "TabbedControlGroup1"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "Detail"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_detail})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(499, 96)
        Me.LayoutControlGroup5.Text = "Detail"
        '
        'tcg_detail
        '
        Me.tcg_detail.CustomizationFormText = "TabbedControlGroup1"
        Me.tcg_detail.Location = New System.Drawing.Point(0, 0)
        Me.tcg_detail.Name = "tcg_detail"
        Me.tcg_detail.SelectedTabPage = Me.lcg_data
        Me.tcg_detail.SelectedTabPageIndex = 0
        Me.tcg_detail.Size = New System.Drawing.Size(499, 96)
        Me.tcg_detail.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lcg_data, Me.LayoutControlGroup4})
        Me.tcg_detail.Text = "tcg_detail"
        '
        'lcg_data
        '
        Me.lcg_data.CustomizationFormText = "Data"
        Me.lcg_data.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem15})
        Me.lcg_data.Location = New System.Drawing.Point(0, 0)
        Me.lcg_data.Name = "lcg_data"
        Me.lcg_data.Size = New System.Drawing.Size(475, 50)
        Me.lcg_data.Text = "List Sales Order"
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.gc_edit_so
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(475, 50)
        Me.LayoutControlItem15.Text = "LayoutControlItem15"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "List Item"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem20, Me.EmptySpaceItem2})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(475, 50)
        Me.LayoutControlGroup4.Text = "List Item"
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.gc_edit_sales_detail
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 26)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(475, 24)
        Me.LayoutControlItem12.Text = "LayoutControlItem12"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.sb_retrieve_receive_item
        Me.LayoutControlItem20.CustomizationFormText = "LayoutControlItem20"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(60, 26)
        Me.LayoutControlItem20.Text = "LayoutControlItem20"
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextToControlDistance = 0
        Me.LayoutControlItem20.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(60, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(415, 26)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'ce_pcksl_doc
        '
        Me.ce_pcksl_doc.Location = New System.Drawing.Point(776, 3)
        Me.ce_pcksl_doc.Name = "ce_pcksl_doc"
        Me.ce_pcksl_doc.Properties.Caption = "Print Lable"
        Me.ce_pcksl_doc.Size = New System.Drawing.Size(97, 19)
        Me.ce_pcksl_doc.TabIndex = 64
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.HiddenPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dp_detail})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.ControlContainer1)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Float
        Me.DockPanel1.FloatLocation = New System.Drawing.Point(1266, 537)
        Me.DockPanel1.ID = New System.Guid("d2a62746-56d7-4bcc-823c-7f21e5bcdb7d")
        Me.DockPanel1.Location = New System.Drawing.Point(-32768, -32768)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(200, 200)
        Me.DockPanel1.Text = "DockPanel1"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        '
        'ControlContainer1
        '
        Me.ControlContainer1.Location = New System.Drawing.Point(2, 24)
        Me.ControlContainer1.Name = "ControlContainer1"
        Me.ControlContainer1.Size = New System.Drawing.Size(196, 174)
        Me.ControlContainer1.TabIndex = 0
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("65f162b6-5b70-479e-8496-bb8c125024ef")
        Me.dp_detail.Location = New System.Drawing.Point(0, 543)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(994, 205)
        Me.dp_detail.Size = New System.Drawing.Size(994, 205)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(988, 177)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_po
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_detail.Size = New System.Drawing.Size(988, 177)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_po, Me.xtp_receive})
        '
        'xtp_po
        '
        Me.xtp_po.Controls.Add(Me.scc_detail)
        Me.xtp_po.Name = "xtp_po"
        Me.xtp_po.Size = New System.Drawing.Size(986, 156)
        Me.xtp_po.Text = "List SO"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_detail_so)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(986, 156)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail_so
        '
        Me.gc_detail_so.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_so.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_so.MainView = Me.gv_detail_so
        Me.gc_detail_so.Name = "gc_detail_so"
        Me.gc_detail_so.Size = New System.Drawing.Size(986, 156)
        Me.gc_detail_so.TabIndex = 0
        Me.gc_detail_so.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_so, Me.GridView3})
        '
        'gv_detail_so
        '
        Me.gv_detail_so.GridControl = Me.gc_detail_so
        Me.gv_detail_so.Name = "gv_detail_so"
        Me.gv_detail_so.OptionsView.ColumnAutoWidth = False
        Me.gv_detail_so.OptionsView.ShowFooter = True
        Me.gv_detail_so.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gc_detail_so
        Me.GridView3.Name = "GridView3"
        '
        'xtp_receive
        '
        Me.xtp_receive.Controls.Add(Me.scc_detail2)
        Me.xtp_receive.Name = "xtp_receive"
        Me.xtp_receive.Size = New System.Drawing.Size(986, 156)
        Me.xtp_receive.Text = "List Shipment Item"
        '
        'scc_detail2
        '
        Me.scc_detail2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail2.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail2.Name = "scc_detail2"
        Me.scc_detail2.Panel1.Controls.Add(Me.gc_detail_shipment)
        Me.scc_detail2.Panel1.Text = "Panel1"
        Me.scc_detail2.Panel2.Text = "Panel2"
        Me.scc_detail2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail2.Size = New System.Drawing.Size(986, 156)
        Me.scc_detail2.TabIndex = 1
        Me.scc_detail2.Text = "SplitContainerControl1"
        '
        'gc_detail_shipment
        '
        Me.gc_detail_shipment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_shipment.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_shipment.MainView = Me.gv_detail_shipment
        Me.gc_detail_shipment.Name = "gc_detail_shipment"
        Me.gc_detail_shipment.Size = New System.Drawing.Size(986, 156)
        Me.gc_detail_shipment.TabIndex = 0
        Me.gc_detail_shipment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_shipment, Me.GridView2})
        '
        'gv_detail_shipment
        '
        Me.gv_detail_shipment.GridControl = Me.gc_detail_shipment
        Me.gv_detail_shipment.Name = "gv_detail_shipment"
        Me.gv_detail_shipment.OptionsView.ColumnAutoWidth = False
        Me.gv_detail_shipment.OptionsView.ShowFooter = True
        Me.gv_detail_shipment.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_detail_shipment
        Me.GridView2.Name = "GridView2"
        '
        'par_so
        '
        Me.par_so.Location = New System.Drawing.Point(582, 3)
        Me.par_so.Name = "par_so"
        Me.par_so.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.par_so.Size = New System.Drawing.Size(188, 20)
        Me.par_so.TabIndex = 20
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(520, 6)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(56, 13)
        Me.LabelControl3.TabIndex = 21
        Me.LabelControl3.Text = "Sales Order"
        '
        'par_cus
        '
        Me.par_cus.Location = New System.Drawing.Point(391, 3)
        Me.par_cus.Name = "par_cus"
        Me.par_cus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.par_cus.Size = New System.Drawing.Size(112, 20)
        Me.par_cus.TabIndex = 20
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(333, 6)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl4.TabIndex = 21
        Me.LabelControl4.Text = "Customer"
        '
        'CheckEdit1
        '
        Me.CheckEdit1.Location = New System.Drawing.Point(861, 3)
        Me.CheckEdit1.Name = "CheckEdit1"
        Me.CheckEdit1.Properties.Caption = "Shipping Addr"
        Me.CheckEdit1.Size = New System.Drawing.Size(121, 19)
        Me.CheckEdit1.TabIndex = 65
        '
        'FDPackingSheetPrintOutbySO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(994, 748)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FDPackingSheetPrintOutbySO"
        Me.Text = "Packing Sheet Document"
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
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.pcs_so_shipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_so_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_so, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_so, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_close_transaction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_sales_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_shipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_sold_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_due_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_eff_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_eff_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_expt_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcksl_expt_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcg_ar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcg_data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_pcksl_doc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_po.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail_so, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_so, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_receive.ResumeLayout(False)
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail2.ResumeLayout(False)
        CType(Me.gc_detail_shipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_shipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.par_so.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.par_cus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Public WithEvents gc_edit_so As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_so As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_retrieve_receive_item As DevExpress.XtraEditors.SimpleButton
    Public WithEvents pcksl_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ce_close_transaction As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents be_so_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents pcksl_expt_date As DevExpress.XtraEditors.DateEdit
    Public WithEvents pcksl_sold_to As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents pcksl_due_date As DevExpress.XtraEditors.DateEdit
    Public WithEvents pcksl_eff_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents par_so As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents par_cus As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UpdateTerbilangToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup13 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tcg_detail As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_po As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_so As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_so As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_receive As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_shipment As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_shipment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents ControlContainer1 As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents pcksl_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Remarks As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents lcg_ar As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lcg_data As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pcs_so_shipment As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Public WithEvents ce_pcksl_doc As DevExpress.XtraEditors.CheckEdit
    Public WithEvents gc_edit_sales_detail As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_shipment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CheckEdit1 As DevExpress.XtraEditors.CheckEdit

End Class
