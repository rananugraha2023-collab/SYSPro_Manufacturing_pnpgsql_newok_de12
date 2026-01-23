<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FBudgeting
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
        Dim PivotGridGroup1 As DevExpress.XtraPivotGrid.PivotGridGroup = New DevExpress.XtraPivotGrid.PivotGridGroup
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.bdgt_year_periode = New DevExpress.XtraEditors.LookUpEdit
        Me.ce_rev = New DevExpress.XtraEditors.CheckEdit
        Me.bdgt_rev = New DevExpress.XtraEditors.SpinEdit
        Me.bdgt_cc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.bdgt_year = New DevExpress.XtraEditors.TextEdit
        Me.bdgt_tran_id = New DevExpress.XtraEditors.LookUpEdit
        Me.bdgt_remarks = New DevExpress.XtraEditors.TextEdit
        Me.bdgt_date = New DevExpress.XtraEditors.DateEdit
        Me.bdgt_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.pgc_detail = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.fieldbdgtpcode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldaccode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldacname = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdbudget = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdalokasi = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdrealisasi = New DevExpress.XtraPivotGrid.PivotGridField
        Me.xtp_work_flow = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_wf = New DevExpress.XtraGrid.GridControl
        Me.gv_wf = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_email = New DevExpress.XtraTab.XtraTabPage
        Me.gc_email = New DevExpress.XtraGrid.GridControl
        Me.gv_email = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_smart_approve = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_smart = New DevExpress.XtraGrid.GridControl
        Me.gv_smart = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.col_select = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.DsinvoicehariffBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DsreqBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.Ds_req = New sygma_solution_system.ds_req
        'Me.Ds_po_printout = New sygma_solution_system.ds_po_printout
        Me.DspoprintoutBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.bdgt_year_periode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_rev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_rev.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_cc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_year.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_tran_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgt_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_work_flow.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_email.SuspendLayout()
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_smart_approve.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsinvoicehariffBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsreqBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.Ds_req, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.Ds_po_printout, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DspoprintoutBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(880, 236)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(882, 573)
        Me.scc_master.SplitterPosition = 27
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(880, 519)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(870, 474)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(882, 540)
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "ID"
        Me.PivotGridField1.FieldName = "bdgtd_bdgtp_id"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(215, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 7
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(197, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "To"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(89, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 5
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(28, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(57, 13)
        Me.LabelControl1.TabIndex = 4
        Me.LabelControl1.Text = "Date From :"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(870, 226)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.bdgt_year_periode)
        Me.lci_master.Controls.Add(Me.ce_rev)
        Me.lci_master.Controls.Add(Me.bdgt_rev)
        Me.lci_master.Controls.Add(Me.bdgt_cc_id)
        Me.lci_master.Controls.Add(Me.bdgt_year)
        Me.lci_master.Controls.Add(Me.bdgt_tran_id)
        Me.lci_master.Controls.Add(Me.bdgt_remarks)
        Me.lci_master.Controls.Add(Me.bdgt_date)
        Me.lci_master.Controls.Add(Me.bdgt_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(870, 474)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'bdgt_year_periode
        '
        Me.bdgt_year_periode.Location = New System.Drawing.Point(101, 144)
        Me.bdgt_year_periode.Name = "bdgt_year_periode"
        Me.bdgt_year_periode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgt_year_periode.Size = New System.Drawing.Size(333, 20)
        Me.bdgt_year_periode.StyleController = Me.lci_master
        Me.bdgt_year_periode.TabIndex = 45
        '
        'ce_rev
        '
        Me.ce_rev.Location = New System.Drawing.Point(101, 24)
        Me.ce_rev.Name = "ce_rev"
        Me.ce_rev.Properties.Caption = ""
        Me.ce_rev.Size = New System.Drawing.Size(91, 19)
        Me.ce_rev.StyleController = Me.lci_master
        Me.ce_rev.TabIndex = 44
        '
        'bdgt_rev
        '
        Me.bdgt_rev.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.bdgt_rev.Enabled = False
        Me.bdgt_rev.Location = New System.Drawing.Point(196, 24)
        Me.bdgt_rev.Name = "bdgt_rev"
        Me.bdgt_rev.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.bdgt_rev.Size = New System.Drawing.Size(85, 20)
        Me.bdgt_rev.StyleController = Me.lci_master
        Me.bdgt_rev.TabIndex = 43
        '
        'bdgt_cc_id
        '
        Me.bdgt_cc_id.Location = New System.Drawing.Point(101, 168)
        Me.bdgt_cc_id.Name = "bdgt_cc_id"
        Me.bdgt_cc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgt_cc_id.Size = New System.Drawing.Size(745, 20)
        Me.bdgt_cc_id.StyleController = Me.lci_master
        Me.bdgt_cc_id.TabIndex = 42
        '
        'bdgt_year
        '
        Me.bdgt_year.Location = New System.Drawing.Point(515, 144)
        Me.bdgt_year.Name = "bdgt_year"
        Me.bdgt_year.Properties.Appearance.Options.UseTextOptions = True
        Me.bdgt_year.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.bdgt_year.Properties.DisplayFormat.FormatString = "f0"
        Me.bdgt_year.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.bdgt_year.Properties.EditFormat.FormatString = "f0"
        Me.bdgt_year.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.bdgt_year.Properties.Mask.EditMask = "f0"
        Me.bdgt_year.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.bdgt_year.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.bdgt_year.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.bdgt_year.Size = New System.Drawing.Size(331, 20)
        Me.bdgt_year.StyleController = Me.lci_master
        Me.bdgt_year.TabIndex = 41
        '
        'bdgt_tran_id
        '
        Me.bdgt_tran_id.Location = New System.Drawing.Point(101, 240)
        Me.bdgt_tran_id.Name = "bdgt_tran_id"
        Me.bdgt_tran_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgt_tran_id.Size = New System.Drawing.Size(331, 20)
        Me.bdgt_tran_id.StyleController = Me.lci_master
        Me.bdgt_tran_id.TabIndex = 17
        '
        'bdgt_remarks
        '
        Me.bdgt_remarks.Location = New System.Drawing.Point(101, 192)
        Me.bdgt_remarks.Name = "bdgt_remarks"
        Me.bdgt_remarks.Size = New System.Drawing.Size(745, 20)
        Me.bdgt_remarks.StyleController = Me.lci_master
        Me.bdgt_remarks.TabIndex = 12
        '
        'bdgt_date
        '
        Me.bdgt_date.EditValue = Nothing
        Me.bdgt_date.Location = New System.Drawing.Point(101, 96)
        Me.bdgt_date.Name = "bdgt_date"
        Me.bdgt_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgt_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.bdgt_date.Size = New System.Drawing.Size(331, 20)
        Me.bdgt_date.StyleController = Me.lci_master
        Me.bdgt_date.TabIndex = 7
        '
        'bdgt_en_id
        '
        Me.bdgt_en_id.Location = New System.Drawing.Point(101, 72)
        Me.bdgt_en_id.Name = "bdgt_en_id"
        Me.bdgt_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgt_en_id.Size = New System.Drawing.Size(331, 20)
        Me.bdgt_en_id.StyleController = Me.lci_master
        Me.bdgt_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.LayoutControlGroup3, Me.LayoutControlGroup5, Me.LayoutControlGroup9, Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(870, 474)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 264)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(850, 190)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem4, Me.EmptySpaceItem2, Me.EmptySpaceItem3})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(850, 72)
        Me.LayoutControlGroup3.Text = "LayoutControlGroup3"
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.bdgt_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.bdgt_date
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem4.Text = "Date"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(412, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(414, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(412, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(414, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem7})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(850, 96)
        Me.LayoutControlGroup5.Text = "LayoutControlGroup5"
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.bdgt_remarks
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(826, 24)
        Me.LayoutControlItem9.Text = "Remarks"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.bdgt_year
        Me.LayoutControlItem2.CustomizationFormText = "Yearh"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(414, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem2.Text = "Year"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.bdgt_cc_id
        Me.LayoutControlItem3.CustomizationFormText = "Cost Centre"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(826, 24)
        Me.LayoutControlItem3.Text = "Cost Centre"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.bdgt_year_periode
        Me.LayoutControlItem7.CustomizationFormText = "Budget Periode"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(414, 24)
        Me.LayoutControlItem7.Text = "Budget Periode"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(73, 13)
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.CustomizationFormText = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14, Me.EmptySpaceItem4})
        Me.LayoutControlGroup9.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlGroup9.Name = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(850, 48)
        Me.LayoutControlGroup9.Text = "LayoutControlGroup9"
        Me.LayoutControlGroup9.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.bdgt_tran_id
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem14.Text = "Transaction"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(412, 0)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(414, 24)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.LayoutControlItem6, Me.EmptySpaceItem5})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(850, 48)
        Me.LayoutControlGroup2.Text = "LayoutControlGroup2"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.bdgt_rev
        Me.LayoutControlItem5.CustomizationFormText = "Revisi"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(172, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(89, 24)
        Me.LayoutControlItem5.Text = "Revisi"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ce_rev
        Me.LayoutControlItem6.CustomizationFormText = "Revisi"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(172, 24)
        Me.LayoutControlItem6.Text = "Revisi"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(73, 13)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(261, 0)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(565, 24)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
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
        Me.dp_detail.ID = New System.Guid("59c0b7c1-361b-4c25-857e-9630e652d81e")
        Me.dp_detail.Location = New System.Drawing.Point(0, 0)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 302)
        Me.dp_detail.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.SavedIndex = 0
        Me.dp_detail.Size = New System.Drawing.Size(882, 302)
        Me.dp_detail.Text = "Data Detail"
        Me.dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(876, 274)
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
        Me.xtc_detail.Size = New System.Drawing.Size(876, 274)
        Me.xtc_detail.TabIndex = 0
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_work_flow, Me.xtp_email, Me.xtp_smart_approve})
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(874, 253)
        Me.xtp_detail.Text = "Data Detail"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.pgc_detail)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(874, 253)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'pgc_detail
        '
        Me.pgc_detail.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_detail.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldbdgtpcode, Me.fieldaccode, Me.fieldacname, Me.fieldbdgtdbudget, Me.fieldbdgtdalokasi, Me.fieldbdgtdrealisasi, Me.PivotGridField1})
        PivotGridGroup1.Fields.Add(Me.PivotGridField1)
        PivotGridGroup1.Hierarchy = Nothing
        Me.pgc_detail.Groups.AddRange(New DevExpress.XtraPivotGrid.PivotGridGroup() {PivotGridGroup1})
        Me.pgc_detail.Location = New System.Drawing.Point(0, 0)
        Me.pgc_detail.Name = "pgc_detail"
        Me.pgc_detail.OptionsCustomization.AllowFilter = False
        Me.pgc_detail.OptionsView.ShowFilterHeaders = False
        Me.pgc_detail.Size = New System.Drawing.Size(874, 253)
        Me.pgc_detail.TabIndex = 1
        '
        'fieldbdgtpcode
        '
        Me.fieldbdgtpcode.Appearance.Cell.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.CellGrandTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.CellTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtpcode.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldbdgtpcode.AreaIndex = 1
        Me.fieldbdgtpcode.Caption = "Periode"
        Me.fieldbdgtpcode.FieldName = "bdgtp_code"
        Me.fieldbdgtpcode.Name = "fieldbdgtpcode"
        '
        'fieldaccode
        '
        Me.fieldaccode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldaccode.AreaIndex = 0
        Me.fieldaccode.Caption = "Account Code"
        Me.fieldaccode.FieldName = "ac_code"
        Me.fieldaccode.Name = "fieldaccode"
        '
        'fieldacname
        '
        Me.fieldacname.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldacname.AreaIndex = 1
        Me.fieldacname.Caption = "Account Name"
        Me.fieldacname.FieldName = "ac_name"
        Me.fieldacname.Name = "fieldacname"
        '
        'fieldbdgtdbudget
        '
        Me.fieldbdgtdbudget.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdbudget.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdbudget.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdbudget.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdbudget.AreaIndex = 0
        Me.fieldbdgtdbudget.Caption = "Budget"
        Me.fieldbdgtdbudget.CellFormat.FormatString = "n0"
        Me.fieldbdgtdbudget.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.FieldName = "bdgtd_budget"
        Me.fieldbdgtdbudget.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdbudget.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.Name = "fieldbdgtdbudget"
        Me.fieldbdgtdbudget.TotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdbudget.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.TotalValueFormat.FormatString = "n0"
        Me.fieldbdgtdbudget.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.ValueFormat.FormatString = "n0"
        Me.fieldbdgtdbudget.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.Width = 118
        '
        'fieldbdgtdalokasi
        '
        Me.fieldbdgtdalokasi.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdalokasi.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdalokasi.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdalokasi.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdalokasi.AreaIndex = 1
        Me.fieldbdgtdalokasi.Caption = "Alokasi"
        Me.fieldbdgtdalokasi.CellFormat.FormatString = "n0"
        Me.fieldbdgtdalokasi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.FieldName = "bdgtd_alokasi"
        Me.fieldbdgtdalokasi.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdalokasi.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.Name = "fieldbdgtdalokasi"
        Me.fieldbdgtdalokasi.TotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdalokasi.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.TotalValueFormat.FormatString = "n0"
        Me.fieldbdgtdalokasi.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.ValueFormat.FormatString = "n0"
        Me.fieldbdgtdalokasi.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.Width = 135
        '
        'fieldbdgtdrealisasi
        '
        Me.fieldbdgtdrealisasi.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdrealisasi.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdrealisasi.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdrealisasi.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdrealisasi.AreaIndex = 2
        Me.fieldbdgtdrealisasi.Caption = "Realisasi"
        Me.fieldbdgtdrealisasi.CellFormat.FormatString = "n0"
        Me.fieldbdgtdrealisasi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.FieldName = "bdgtd_realisasi"
        Me.fieldbdgtdrealisasi.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdrealisasi.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.Name = "fieldbdgtdrealisasi"
        Me.fieldbdgtdrealisasi.TotalCellFormat.FormatString = "n0"
        Me.fieldbdgtdrealisasi.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.TotalValueFormat.FormatString = "n0"
        Me.fieldbdgtdrealisasi.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.ValueFormat.FormatString = "n0"
        Me.fieldbdgtdrealisasi.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.Width = 122
        '
        'xtp_work_flow
        '
        Me.xtp_work_flow.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_work_flow.Name = "xtp_work_flow"
        Me.xtp_work_flow.Size = New System.Drawing.Size(874, 253)
        Me.xtp_work_flow.Text = "Work Flow"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gc_wf)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl1.Size = New System.Drawing.Size(874, 253)
        Me.SplitContainerControl1.SplitterPosition = 554
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_wf
        '
        Me.gc_wf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_wf.Location = New System.Drawing.Point(0, 0)
        Me.gc_wf.MainView = Me.gv_wf
        Me.gc_wf.Name = "gc_wf"
        Me.gc_wf.Size = New System.Drawing.Size(874, 253)
        Me.gc_wf.TabIndex = 2
        Me.gc_wf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_wf})
        '
        'gv_wf
        '
        Me.gv_wf.GridControl = Me.gc_wf
        Me.gv_wf.Name = "gv_wf"
        Me.gv_wf.OptionsView.ColumnAutoWidth = False
        Me.gv_wf.OptionsView.ShowGroupPanel = False
        '
        'xtp_email
        '
        Me.xtp_email.Controls.Add(Me.gc_email)
        Me.xtp_email.Name = "xtp_email"
        Me.xtp_email.PageVisible = False
        Me.xtp_email.Size = New System.Drawing.Size(874, 253)
        Me.xtp_email.Text = "For Email"
        '
        'gc_email
        '
        Me.gc_email.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_email.Location = New System.Drawing.Point(0, 0)
        Me.gc_email.MainView = Me.gv_email
        Me.gc_email.Name = "gc_email"
        Me.gc_email.Size = New System.Drawing.Size(874, 253)
        Me.gc_email.TabIndex = 1
        Me.gc_email.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_email})
        '
        'gv_email
        '
        Me.gv_email.GridControl = Me.gc_email
        Me.gv_email.Name = "gv_email"
        Me.gv_email.OptionsView.ColumnAutoWidth = False
        Me.gv_email.OptionsView.ShowGroupPanel = False
        '
        'xtp_smart_approve
        '
        Me.xtp_smart_approve.Controls.Add(Me.SplitContainerControl2)
        Me.xtp_smart_approve.Name = "xtp_smart_approve"
        Me.xtp_smart_approve.Size = New System.Drawing.Size(874, 253)
        Me.xtp_smart_approve.Text = "Smart Approve"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.gc_smart)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl2.Size = New System.Drawing.Size(874, 253)
        Me.SplitContainerControl2.SplitterPosition = 524
        Me.SplitContainerControl2.TabIndex = 0
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'gc_smart
        '
        Me.gc_smart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_smart.Location = New System.Drawing.Point(0, 0)
        Me.gc_smart.MainView = Me.gv_smart
        Me.gc_smart.Name = "gc_smart"
        Me.gc_smart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.gc_smart.Size = New System.Drawing.Size(874, 253)
        Me.gc_smart.TabIndex = 4
        Me.gc_smart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_smart})
        '
        'gv_smart
        '
        Me.gv_smart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_select})
        Me.gv_smart.GridControl = Me.gc_smart
        Me.gv_smart.Name = "gv_smart"
        Me.gv_smart.OptionsView.ColumnAutoWidth = False
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
        'DsreqBindingSource
        '
        'Me.DsreqBindingSource.DataSource = Me.Ds_req
        Me.DsreqBindingSource.Position = 0
        '
        'Ds_req
        '
        'Me.Ds_req.DataSetName = "ds_req"
        'Me.Ds_req.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        ''
        ''Ds_po_printout
        ''
        'Me.Ds_po_printout.DataSetName = "ds_po_printout"
        'Me.Ds_po_printout.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DspoprintoutBindingSource
        '
        'Me.DspoprintoutBindingSource.DataSource = Me.Ds_po_printout
        Me.DspoprintoutBindingSource.Position = 0
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.Controls.Add(Me.dp_detail)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 573)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(882, 19)
        '
        'FBudgeting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(882, 592)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FBudgeting"
        Me.Text = "Generate Budget"
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
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.bdgt_year_periode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_rev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_rev.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_cc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_year.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_tran_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgt_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_work_flow.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_email.ResumeLayout(False)
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_smart_approve.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsinvoicehariffBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsreqBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.Ds_req, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.Ds_po_printout, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DspoprintoutBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents bdgt_tran_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents bdgt_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bdgt_date As DevExpress.XtraEditors.DateEdit
    Public WithEvents bdgt_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents xtp_work_flow As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtp_email As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtp_smart_approve As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_email As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_email As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents bdgt_cc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents bdgt_year As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DsreqBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Ds_req As sygma_solution_system.ds_req
    Friend WithEvents DsinvoicehariffBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DspoprintoutBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Ds_po_printout As sygma_solution_system.ds_po_printout
    Friend WithEvents pgc_detail As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents fieldbdgtpcode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldaccode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldacname As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdbudget As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdalokasi As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdrealisasi As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents bdgt_rev As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ce_rev As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents bdgt_year_periode As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_wf As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_wf As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_smart As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_smart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents col_select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer

End Class
