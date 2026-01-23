<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectMaintenance
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
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.prj_area_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_pocust_oid = New DevExpress.XtraEditors.ButtonEdit
        Me.prj_end_date = New DevExpress.XtraEditors.DateEdit
        Me.prj_start_date = New DevExpress.XtraEditors.DateEdit
        Me.gc_edit_cust = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_cust = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gc_edit = New DevExpress.XtraGrid.GridControl
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.prj_tran_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_ar_cc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_ar_sb_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_ar_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_tax_class = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_tax_inc = New DevExpress.XtraEditors.CheckEdit
        Me.prj_taxable = New DevExpress.XtraEditors.CheckEdit
        Me.prj_credit_term = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_pjt_code_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_sales_person_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_ptnr_id_sold = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_si_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_ord_date = New DevExpress.XtraEditors.DateEdit
        Me.prj_ptnr_id_bill = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_cu_id = New DevExpress.XtraEditors.LookUpEdit
        Me.prj_exc_rate = New DevExpress.XtraEditors.TextEdit
        Me.prj_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_header = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.aaa = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_detail = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_detail_serial = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail_cust = New DevExpress.XtraGrid.GridControl
        Me.gv_detail_cust = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_work_flow = New DevExpress.XtraTab.XtraTabPage
        Me.gc_wf = New DevExpress.XtraGrid.GridControl
        Me.gv_wf = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_email = New DevExpress.XtraTab.XtraTabPage
        Me.gc_email = New DevExpress.XtraGrid.GridControl
        Me.gv_email = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_smart_approve = New DevExpress.XtraTab.XtraTabPage
        Me.gc_smart = New DevExpress.XtraGrid.GridControl
        Me.gv_smart = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.col_select = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.prj_area_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_pocust_oid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_end_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_start_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_cust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_cust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_tran_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ar_cc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ar_sb_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ar_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_tax_class.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_tax_inc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_taxable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_credit_term.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_pjt_code_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_sales_person_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ptnr_id_sold.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ord_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ord_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_ptnr_id_bill.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_cu_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_exc_rate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.prj_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aaa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_detail_serial.SuspendLayout()
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail2.SuspendLayout()
        CType(Me.gc_detail_cust, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail_cust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_work_flow.SuspendLayout()
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_email.SuspendLayout()
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_smart_approve.SuspendLayout()
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(820, 400)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(822, 608)
        Me.scc_master.SplitterPosition = 27
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(820, 554)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(810, 494)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(822, 575)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(810, 390)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(215, 4)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 11
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(167, 7)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 10
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(55, 4)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 9
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(4, 7)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "First Date"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.prj_area_id)
        Me.lci_master.Controls.Add(Me.prj_pocust_oid)
        Me.lci_master.Controls.Add(Me.prj_end_date)
        Me.lci_master.Controls.Add(Me.prj_start_date)
        Me.lci_master.Controls.Add(Me.gc_edit_cust)
        Me.lci_master.Controls.Add(Me.gc_edit)
        Me.lci_master.Controls.Add(Me.prj_tran_id)
        Me.lci_master.Controls.Add(Me.prj_ar_cc_id)
        Me.lci_master.Controls.Add(Me.prj_ar_sb_id)
        Me.lci_master.Controls.Add(Me.prj_ar_ac_id)
        Me.lci_master.Controls.Add(Me.prj_tax_class)
        Me.lci_master.Controls.Add(Me.prj_tax_inc)
        Me.lci_master.Controls.Add(Me.prj_taxable)
        Me.lci_master.Controls.Add(Me.prj_credit_term)
        Me.lci_master.Controls.Add(Me.prj_pjt_code_id)
        Me.lci_master.Controls.Add(Me.prj_sales_person_id)
        Me.lci_master.Controls.Add(Me.prj_ptnr_id_sold)
        Me.lci_master.Controls.Add(Me.prj_si_id)
        Me.lci_master.Controls.Add(Me.prj_ord_date)
        Me.lci_master.Controls.Add(Me.prj_ptnr_id_bill)
        Me.lci_master.Controls.Add(Me.prj_cu_id)
        Me.lci_master.Controls.Add(Me.prj_exc_rate)
        Me.lci_master.Controls.Add(Me.prj_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(810, 494)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'prj_area_id
        '
        Me.prj_area_id.Location = New System.Drawing.Point(485, 154)
        Me.prj_area_id.Name = "prj_area_id"
        Me.prj_area_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_area_id.Size = New System.Drawing.Size(289, 20)
        Me.prj_area_id.StyleController = Me.lci_master
        Me.prj_area_id.TabIndex = 48
        '
        'prj_pocust_oid
        '
        Me.prj_pocust_oid.Location = New System.Drawing.Point(113, 106)
        Me.prj_pocust_oid.Name = "prj_pocust_oid"
        Me.prj_pocust_oid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.prj_pocust_oid.Properties.ReadOnly = True
        Me.prj_pocust_oid.Size = New System.Drawing.Size(291, 20)
        Me.prj_pocust_oid.StyleController = Me.lci_master
        Me.prj_pocust_oid.TabIndex = 47
        '
        'prj_end_date
        '
        Me.prj_end_date.EditValue = Nothing
        Me.prj_end_date.Location = New System.Drawing.Point(485, 202)
        Me.prj_end_date.Name = "prj_end_date"
        Me.prj_end_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_end_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.prj_end_date.Size = New System.Drawing.Size(289, 20)
        Me.prj_end_date.StyleController = Me.lci_master
        Me.prj_end_date.TabIndex = 46
        '
        'prj_start_date
        '
        Me.prj_start_date.EditValue = Nothing
        Me.prj_start_date.Location = New System.Drawing.Point(113, 202)
        Me.prj_start_date.Name = "prj_start_date"
        Me.prj_start_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_start_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.prj_start_date.Size = New System.Drawing.Size(291, 20)
        Me.prj_start_date.StyleController = Me.lci_master
        Me.prj_start_date.TabIndex = 45
        '
        'gc_edit_cust
        '
        Me.gc_edit_cust.DataMember = "pod_det"
        Me.gc_edit_cust.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit_cust.MainView = Me.gv_edit_cust
        Me.gc_edit_cust.Name = "gc_edit_cust"
        Me.gc_edit_cust.Size = New System.Drawing.Size(738, 378)
        Me.gc_edit_cust.TabIndex = 43
        Me.gc_edit_cust.UseEmbeddedNavigator = True
        Me.gc_edit_cust.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_cust})
        '
        'gv_edit_cust
        '
        Me.gv_edit_cust.GridControl = Me.gc_edit_cust
        Me.gv_edit_cust.Name = "gv_edit_cust"
        Me.gv_edit_cust.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit_cust.OptionsCustomization.AllowSort = False
        Me.gv_edit_cust.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_cust.OptionsView.ShowFooter = True
        Me.gv_edit_cust.OptionsView.ShowGroupPanel = False
        '
        'gc_edit
        '
        Me.gc_edit.DataMember = "pod_det"
        Me.gc_edit.Location = New System.Drawing.Point(36, 80)
        Me.gc_edit.MainView = Me.gv_edit
        Me.gc_edit.Name = "gc_edit"
        Me.gc_edit.Size = New System.Drawing.Size(738, 378)
        Me.gc_edit.TabIndex = 42
        Me.gc_edit.UseEmbeddedNavigator = True
        Me.gc_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.gc_edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit.OptionsCustomization.AllowSort = False
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowFooter = True
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'prj_tran_id
        '
        Me.prj_tran_id.Location = New System.Drawing.Point(113, 417)
        Me.prj_tran_id.Name = "prj_tran_id"
        Me.prj_tran_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_tran_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_tran_id.StyleController = Me.lci_master
        Me.prj_tran_id.TabIndex = 33
        '
        'prj_ar_cc_id
        '
        Me.prj_ar_cc_id.Location = New System.Drawing.Point(113, 393)
        Me.prj_ar_cc_id.Name = "prj_ar_cc_id"
        Me.prj_ar_cc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ar_cc_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_ar_cc_id.StyleController = Me.lci_master
        Me.prj_ar_cc_id.TabIndex = 32
        '
        'prj_ar_sb_id
        '
        Me.prj_ar_sb_id.Location = New System.Drawing.Point(485, 369)
        Me.prj_ar_sb_id.Name = "prj_ar_sb_id"
        Me.prj_ar_sb_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ar_sb_id.Size = New System.Drawing.Size(289, 20)
        Me.prj_ar_sb_id.StyleController = Me.lci_master
        Me.prj_ar_sb_id.TabIndex = 31
        '
        'prj_ar_ac_id
        '
        Me.prj_ar_ac_id.Location = New System.Drawing.Point(113, 369)
        Me.prj_ar_ac_id.Name = "prj_ar_ac_id"
        Me.prj_ar_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ar_ac_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_ar_ac_id.StyleController = Me.lci_master
        Me.prj_ar_ac_id.TabIndex = 30
        '
        'prj_tax_class
        '
        Me.prj_tax_class.Location = New System.Drawing.Point(113, 321)
        Me.prj_tax_class.Name = "prj_tax_class"
        Me.prj_tax_class.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_tax_class.Size = New System.Drawing.Size(291, 20)
        Me.prj_tax_class.StyleController = Me.lci_master
        Me.prj_tax_class.TabIndex = 29
        '
        'prj_tax_inc
        '
        Me.prj_tax_inc.Location = New System.Drawing.Point(485, 298)
        Me.prj_tax_inc.Name = "prj_tax_inc"
        Me.prj_tax_inc.Properties.Caption = ""
        Me.prj_tax_inc.Size = New System.Drawing.Size(289, 19)
        Me.prj_tax_inc.StyleController = Me.lci_master
        Me.prj_tax_inc.TabIndex = 28
        '
        'prj_taxable
        '
        Me.prj_taxable.Location = New System.Drawing.Point(113, 298)
        Me.prj_taxable.Name = "prj_taxable"
        Me.prj_taxable.Properties.Caption = ""
        Me.prj_taxable.Size = New System.Drawing.Size(291, 19)
        Me.prj_taxable.StyleController = Me.lci_master
        Me.prj_taxable.TabIndex = 27
        '
        'prj_credit_term
        '
        Me.prj_credit_term.Location = New System.Drawing.Point(113, 274)
        Me.prj_credit_term.Name = "prj_credit_term"
        Me.prj_credit_term.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_credit_term.Size = New System.Drawing.Size(291, 20)
        Me.prj_credit_term.StyleController = Me.lci_master
        Me.prj_credit_term.TabIndex = 26
        '
        'prj_pjt_code_id
        '
        Me.prj_pjt_code_id.Location = New System.Drawing.Point(113, 154)
        Me.prj_pjt_code_id.Name = "prj_pjt_code_id"
        Me.prj_pjt_code_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_pjt_code_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_pjt_code_id.StyleController = Me.lci_master
        Me.prj_pjt_code_id.TabIndex = 25
        '
        'prj_sales_person_id
        '
        Me.prj_sales_person_id.Location = New System.Drawing.Point(485, 106)
        Me.prj_sales_person_id.Name = "prj_sales_person_id"
        Me.prj_sales_person_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_sales_person_id.Size = New System.Drawing.Size(289, 20)
        Me.prj_sales_person_id.StyleController = Me.lci_master
        Me.prj_sales_person_id.TabIndex = 24
        '
        'prj_ptnr_id_sold
        '
        Me.prj_ptnr_id_sold.Location = New System.Drawing.Point(485, 82)
        Me.prj_ptnr_id_sold.Name = "prj_ptnr_id_sold"
        Me.prj_ptnr_id_sold.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ptnr_id_sold.Size = New System.Drawing.Size(289, 20)
        Me.prj_ptnr_id_sold.StyleController = Me.lci_master
        Me.prj_ptnr_id_sold.TabIndex = 23
        '
        'prj_si_id
        '
        Me.prj_si_id.Location = New System.Drawing.Point(485, 58)
        Me.prj_si_id.Name = "prj_si_id"
        Me.prj_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_si_id.Size = New System.Drawing.Size(289, 20)
        Me.prj_si_id.StyleController = Me.lci_master
        Me.prj_si_id.TabIndex = 22
        '
        'prj_ord_date
        '
        Me.prj_ord_date.EditValue = Nothing
        Me.prj_ord_date.Location = New System.Drawing.Point(113, 178)
        Me.prj_ord_date.Name = "prj_ord_date"
        Me.prj_ord_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ord_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.prj_ord_date.Size = New System.Drawing.Size(291, 20)
        Me.prj_ord_date.StyleController = Me.lci_master
        Me.prj_ord_date.TabIndex = 21
        '
        'prj_ptnr_id_bill
        '
        Me.prj_ptnr_id_bill.Location = New System.Drawing.Point(113, 82)
        Me.prj_ptnr_id_bill.Name = "prj_ptnr_id_bill"
        Me.prj_ptnr_id_bill.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_ptnr_id_bill.Size = New System.Drawing.Size(291, 20)
        Me.prj_ptnr_id_bill.StyleController = Me.lci_master
        Me.prj_ptnr_id_bill.TabIndex = 18
        '
        'prj_cu_id
        '
        Me.prj_cu_id.Location = New System.Drawing.Point(113, 250)
        Me.prj_cu_id.Name = "prj_cu_id"
        Me.prj_cu_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_cu_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_cu_id.StyleController = Me.lci_master
        Me.prj_cu_id.TabIndex = 14
        '
        'prj_exc_rate
        '
        Me.prj_exc_rate.Location = New System.Drawing.Point(485, 250)
        Me.prj_exc_rate.Name = "prj_exc_rate"
        Me.prj_exc_rate.Properties.Appearance.Options.UseTextOptions = True
        Me.prj_exc_rate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.prj_exc_rate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.prj_exc_rate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.prj_exc_rate.Properties.Mask.EditMask = "n"
        Me.prj_exc_rate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.prj_exc_rate.Size = New System.Drawing.Size(289, 20)
        Me.prj_exc_rate.StyleController = Me.lci_master
        Me.prj_exc_rate.TabIndex = 13
        '
        'prj_en_id
        '
        Me.prj_en_id.Location = New System.Drawing.Point(113, 58)
        Me.prj_en_id.Name = "prj_en_id"
        Me.prj_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.prj_en_id.Size = New System.Drawing.Size(291, 20)
        Me.prj_en_id.StyleController = Me.lci_master
        Me.prj_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_header})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(810, 494)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'tcg_header
        '
        Me.tcg_header.CustomizationFormText = "tcg_header"
        Me.tcg_header.Location = New System.Drawing.Point(0, 0)
        Me.tcg_header.Name = "tcg_header"
        Me.tcg_header.SelectedTabPage = Me.LayoutControlGroup5
        Me.tcg_header.SelectedTabPageIndex = 0
        Me.tcg_header.Size = New System.Drawing.Size(790, 474)
        Me.tcg_header.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5, Me.LayoutControlGroup6})
        Me.tcg_header.Text = "tcg_header"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "Header"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup4, Me.LayoutControlGroup7, Me.LayoutControlGroup8, Me.LayoutControlGroup9})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(766, 428)
        Me.LayoutControlGroup5.Text = "Header"
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem13, Me.LayoutControlItem7, Me.LayoutControlItem22, Me.LayoutControlItem11, Me.LayoutControlItem14})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(766, 96)
        Me.LayoutControlGroup4.Text = "LayoutControlGroup4"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.prj_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.prj_ptnr_id_sold
        Me.LayoutControlItem13.CustomizationFormText = "Parner Sold To"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(372, 24)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem13.Text = "Parner Sold To"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.prj_si_id
        Me.LayoutControlItem7.CustomizationFormText = "Site"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(372, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem7.Text = "Site"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.prj_pocust_oid
        Me.LayoutControlItem22.CustomizationFormText = "Referensi PO"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem22.Text = "Referensi PO"
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.prj_ptnr_id_bill
        Me.LayoutControlItem11.CustomizationFormText = "Partner Bill To"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem11.Text = "Partner Bill To"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.prj_sales_person_id
        Me.LayoutControlItem14.CustomizationFormText = "Sales Person"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(372, 48)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem14.Text = "Sales Person"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.CustomizationFormText = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem8, Me.LayoutControlItem6, Me.LayoutControlItem21, Me.EmptySpaceItem1, Me.LayoutControlItem15, Me.LayoutControlItem23})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(766, 96)
        Me.LayoutControlGroup7.Text = "LayoutControlGroup7"
        Me.LayoutControlGroup7.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.prj_ord_date
        Me.LayoutControlItem8.CustomizationFormText = "Order Date"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem8.Text = "Order Date"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.prj_start_date
        Me.LayoutControlItem6.CustomizationFormText = "Start Date"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem6.Text = "Start Date"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.prj_end_date
        Me.LayoutControlItem21.CustomizationFormText = "End Date"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(372, 48)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem21.Text = "End Date"
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(372, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(370, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.prj_pjt_code_id
        Me.LayoutControlItem15.CustomizationFormText = "Project Type"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem15.Text = "Project Type"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.prj_area_id
        Me.LayoutControlItem23.CustomizationFormText = "Project Area"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(372, 0)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem23.Text = "Project Area"
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.CustomizationFormText = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.aaa, Me.LayoutControlItem16, Me.LayoutControlItem17, Me.LayoutControlItem19, Me.EmptySpaceItem2, Me.LayoutControlItem18, Me.EmptySpaceItem5, Me.LayoutControlItem3})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(766, 119)
        Me.LayoutControlGroup8.Text = "LayoutControlGroup8"
        Me.LayoutControlGroup8.TextVisible = False
        '
        'aaa
        '
        Me.aaa.Control = Me.prj_cu_id
        Me.aaa.CustomizationFormText = "Currency"
        Me.aaa.Location = New System.Drawing.Point(0, 0)
        Me.aaa.Name = "aaa"
        Me.aaa.Size = New System.Drawing.Size(372, 24)
        Me.aaa.Text = "Currency"
        Me.aaa.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.prj_credit_term
        Me.LayoutControlItem16.CustomizationFormText = "Credit Term"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem16.Text = "Credit Term"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.prj_taxable
        Me.LayoutControlItem17.CustomizationFormText = "Taxable"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(372, 23)
        Me.LayoutControlItem17.Text = "Taxable"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.prj_tax_class
        Me.LayoutControlItem19.CustomizationFormText = "Tax Class"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 71)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem19.Text = "Tax Class"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(372, 71)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(370, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.prj_tax_inc
        Me.LayoutControlItem18.CustomizationFormText = "Tax Include"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(372, 48)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(370, 23)
        Me.LayoutControlItem18.Text = "Tax Include"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(372, 24)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(370, 24)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.prj_exc_rate
        Me.LayoutControlItem3.CustomizationFormText = "Exchange Rate"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(372, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem3.Text = "Exchange Rate"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.CustomizationFormText = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem12, Me.LayoutControlItem10, Me.LayoutControlItem5, Me.LayoutControlItem9, Me.EmptySpaceItem6, Me.EmptySpaceItem4})
        Me.LayoutControlGroup9.Location = New System.Drawing.Point(0, 311)
        Me.LayoutControlGroup9.Name = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(766, 117)
        Me.LayoutControlGroup9.Text = "LayoutControlGroup9"
        Me.LayoutControlGroup9.TextVisible = False
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.prj_tran_id
        Me.LayoutControlItem12.CustomizationFormText = "Transaction ID"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(372, 45)
        Me.LayoutControlItem12.Text = "Transaction ID"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.prj_ar_cc_id
        Me.LayoutControlItem10.CustomizationFormText = "Cost Centre"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem10.Text = "Cost Centre"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.prj_ar_ac_id
        Me.LayoutControlItem5.CustomizationFormText = "Account"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(372, 24)
        Me.LayoutControlItem5.Text = "Account"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.prj_ar_sb_id
        Me.LayoutControlItem9.CustomizationFormText = "Sub Account"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(372, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(370, 24)
        Me.LayoutControlItem9.Text = "Sub Account"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(372, 24)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(370, 24)
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(372, 48)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(370, 45)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Detail"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_detail})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(766, 428)
        Me.LayoutControlGroup6.Text = "Detail"
        '
        'tcg_detail
        '
        Me.tcg_detail.CustomizationFormText = "TabbedControlGroup1"
        Me.tcg_detail.Location = New System.Drawing.Point(0, 0)
        Me.tcg_detail.Name = "tcg_detail"
        Me.tcg_detail.SelectedTabPage = Me.LayoutControlGroup2
        Me.tcg_detail.SelectedTabPageIndex = 0
        Me.tcg_detail.Size = New System.Drawing.Size(766, 428)
        Me.tcg_detail.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.tcg_detail.Text = "tcg_detail"
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Detail Receipt"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem20})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(742, 382)
        Me.LayoutControlGroup2.Text = "Project Detail"
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.gc_edit
        Me.LayoutControlItem20.CustomizationFormText = "LayoutControlItem20"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(742, 382)
        Me.LayoutControlItem20.Text = "LayoutControlItem20"
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextToControlDistance = 0
        Me.LayoutControlItem20.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "Detail Serial Number"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(742, 382)
        Me.LayoutControlGroup3.Text = "Project Detail Customer"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.gc_edit_cust
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(742, 382)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
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
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("860e7497-8bf2-4bdd-9c5e-b4a122e884f9")
        Me.dp_detail.Location = New System.Drawing.Point(0, 0)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 173)
        Me.dp_detail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.SavedIndex = 0
        Me.dp_detail.Size = New System.Drawing.Size(822, 173)
        Me.dp_detail.Text = "Data Detail"
        Me.dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(816, 145)
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
        Me.xtc_detail.Size = New System.Drawing.Size(816, 145)
        Me.xtc_detail.TabIndex = 0
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_detail_serial, Me.xtp_work_flow, Me.xtp_email, Me.xtp_smart_approve})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(814, 124)
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
        Me.scc_detail.Size = New System.Drawing.Size(814, 124)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(814, 124)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'xtp_detail_serial
        '
        Me.xtp_detail_serial.Controls.Add(Me.scc_detail2)
        Me.xtp_detail_serial.Name = "xtp_detail_serial"
        Me.xtp_detail_serial.PageVisible = False
        Me.xtp_detail_serial.Size = New System.Drawing.Size(814, 124)
        Me.xtp_detail_serial.Text = "Data Detail Customer"
        '
        'scc_detail2
        '
        Me.scc_detail2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail2.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail2.Name = "scc_detail2"
        Me.scc_detail2.Panel1.Controls.Add(Me.gc_detail_cust)
        Me.scc_detail2.Panel1.Text = "Panel1"
        Me.scc_detail2.Panel2.Text = "Panel2"
        Me.scc_detail2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail2.Size = New System.Drawing.Size(814, 124)
        Me.scc_detail2.TabIndex = 1
        Me.scc_detail2.Text = "SplitContainerControl1"
        '
        'gc_detail_cust
        '
        Me.gc_detail_cust.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_cust.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_cust.MainView = Me.gv_detail_cust
        Me.gc_detail_cust.Name = "gc_detail_cust"
        Me.gc_detail_cust.Size = New System.Drawing.Size(814, 124)
        Me.gc_detail_cust.TabIndex = 0
        Me.gc_detail_cust.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_cust})
        '
        'gv_detail_cust
        '
        Me.gv_detail_cust.GridControl = Me.gc_detail_cust
        Me.gv_detail_cust.Name = "gv_detail_cust"
        Me.gv_detail_cust.OptionsView.ShowGroupPanel = False
        '
        'xtp_work_flow
        '
        Me.xtp_work_flow.Controls.Add(Me.gc_wf)
        Me.xtp_work_flow.Name = "xtp_work_flow"
        Me.xtp_work_flow.PageVisible = False
        Me.xtp_work_flow.Size = New System.Drawing.Size(814, 124)
        Me.xtp_work_flow.Text = "Work Flow"
        '
        'gc_wf
        '
        Me.gc_wf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_wf.Location = New System.Drawing.Point(0, 0)
        Me.gc_wf.MainView = Me.gv_wf
        Me.gc_wf.Name = "gc_wf"
        Me.gc_wf.Size = New System.Drawing.Size(814, 124)
        Me.gc_wf.TabIndex = 2
        Me.gc_wf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_wf})
        '
        'gv_wf
        '
        Me.gv_wf.GridControl = Me.gc_wf
        Me.gv_wf.Name = "gv_wf"
        Me.gv_wf.OptionsView.ColumnAutoWidth = False
        Me.gv_wf.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_wf.OptionsView.ShowGroupPanel = False
        '
        'xtp_email
        '
        Me.xtp_email.Controls.Add(Me.gc_email)
        Me.xtp_email.Name = "xtp_email"
        Me.xtp_email.PageVisible = False
        Me.xtp_email.Size = New System.Drawing.Size(814, 124)
        Me.xtp_email.Text = "For Email"
        '
        'gc_email
        '
        Me.gc_email.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_email.Location = New System.Drawing.Point(0, 0)
        Me.gc_email.MainView = Me.gv_email
        Me.gc_email.Name = "gc_email"
        Me.gc_email.Size = New System.Drawing.Size(814, 124)
        Me.gc_email.TabIndex = 2
        Me.gc_email.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_email})
        '
        'gv_email
        '
        Me.gv_email.GridControl = Me.gc_email
        Me.gv_email.Name = "gv_email"
        Me.gv_email.OptionsView.ColumnAutoWidth = False
        Me.gv_email.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_email.OptionsView.ShowGroupPanel = False
        '
        'xtp_smart_approve
        '
        Me.xtp_smart_approve.Controls.Add(Me.gc_smart)
        Me.xtp_smart_approve.Name = "xtp_smart_approve"
        Me.xtp_smart_approve.PageVisible = False
        Me.xtp_smart_approve.Size = New System.Drawing.Size(814, 124)
        Me.xtp_smart_approve.Text = "Smart Approve"
        '
        'gc_smart
        '
        Me.gc_smart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_smart.Location = New System.Drawing.Point(0, 0)
        Me.gc_smart.MainView = Me.gv_smart
        Me.gc_smart.Name = "gc_smart"
        Me.gc_smart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.gc_smart.Size = New System.Drawing.Size(814, 124)
        Me.gc_smart.TabIndex = 4
        Me.gc_smart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_smart})
        '
        'gv_smart
        '
        Me.gv_smart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_select})
        Me.gv_smart.GridControl = Me.gc_smart
        Me.gv_smart.Name = "gv_smart"
        Me.gv_smart.OptionsView.ColumnAutoWidth = False
        Me.gv_smart.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_smart.OptionsView.ShowGroupPanel = False
        '
        'col_select
        '
        Me.col_select.Caption = "#"
        Me.col_select.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.col_select.FieldName = "status"
        Me.col_select.Name = "col_select"
        Me.col_select.Visible = True
        Me.col_select.VisibleIndex = 0
        Me.col_select.Width = 64
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.Controls.Add(Me.dp_detail)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 608)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(822, 19)
        '
        'FProjectMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(822, 627)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FProjectMaintenance"
        Me.Text = "Project Maintenance"
        Me.Controls.SetChildIndex(Me.hideContainerBottom, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.prj_area_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_pocust_oid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_end_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_end_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_start_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_start_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_cust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_cust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_tran_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ar_cc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ar_sb_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ar_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_tax_class.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_tax_inc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_taxable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_credit_term.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_pjt_code_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_sales_person_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ptnr_id_sold.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ord_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ord_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_ptnr_id_bill.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_cu_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_exc_rate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.prj_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aaa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_detail_serial.ResumeLayout(False)
        CType(Me.scc_detail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail2.ResumeLayout(False)
        CType(Me.gc_detail_cust, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail_cust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_work_flow.ResumeLayout(False)
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_email.ResumeLayout(False)
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_smart_approve.ResumeLayout(False)
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents prj_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents xtp_detail_serial As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail_cust As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_cust As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tcg_header As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents tcg_detail As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents aaa As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents prj_exc_rate As DevExpress.XtraEditors.TextEdit
    Public WithEvents prj_cu_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents prj_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_ord_date As DevExpress.XtraEditors.DateEdit
    Public WithEvents prj_ptnr_id_bill As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents prj_pjt_code_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_sales_person_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_ptnr_id_sold As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents prj_tax_class As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_tax_inc As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents prj_taxable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents prj_credit_term As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents prj_ar_cc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_ar_sb_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents prj_ar_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents prj_tran_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Public WithEvents gc_edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents gc_edit_cust As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit_cust As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents xtp_work_flow As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_wf As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_wf As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_email As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_email As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_email As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_smart_approve As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_smart As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_smart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents col_select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents prj_end_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents prj_start_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents prj_pocust_oid As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents prj_area_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer

End Class
