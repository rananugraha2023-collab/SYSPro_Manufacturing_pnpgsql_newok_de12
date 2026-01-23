<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRequisition
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.be_document = New DevExpress.XtraEditors.ButtonEdit
        Me.bt_download = New DevExpress.XtraEditors.SimpleButton
        Me.bt_preview = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl69 = New DevExpress.XtraEditors.LabelControl
        Me.bt_hapus_dokumen = New DevExpress.XtraEditors.SimpleButton
        Me.bt_tambah_dokumen = New DevExpress.XtraEditors.SimpleButton
        Me.gc_edit_doc = New DevExpress.XtraGrid.GridControl
        Me.gv_edit_doc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.req_type = New DevExpress.XtraEditors.ComboBoxEdit
        Me.gc_edit = New DevExpress.XtraGrid.GridControl
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.req_tran_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_pjc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_si_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_cc_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_sb_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_rmks = New DevExpress.XtraEditors.TextEdit
        Me.req_end_user = New DevExpress.XtraEditors.TextEdit
        Me.req_requested = New DevExpress.XtraEditors.TextEdit
        Me.req_due_date = New DevExpress.XtraEditors.DateEdit
        Me.req_need_date = New DevExpress.XtraEditors.DateEdit
        Me.req_date = New DevExpress.XtraEditors.DateEdit
        Me.req_ptnr_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_cmaddr_id = New DevExpress.XtraEditors.LookUpEdit
        Me.req_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.tcg_header = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup9 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlGroup7 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_work_flow = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_wf = New DevExpress.XtraGrid.GridControl
        Me.gv_wf = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_email = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl2 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_email = New DevExpress.XtraGrid.GridControl
        Me.gv_email = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_smart_approve = New DevExpress.XtraTab.XtraTabPage
        Me.scc_smart_approve = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_smart = New DevExpress.XtraGrid.GridControl
        Me.gv_smart = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.col_select = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_file = New DevExpress.XtraTab.XtraTabPage
        Me.gc_detail_doc = New DevExpress.XtraGrid.GridControl
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RemoveDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gv_detail_doc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.TxtPRDetail = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.BtPacking = New DevExpress.XtraEditors.SimpleButton
        Me.BtReload = New DevExpress.XtraEditors.SimpleButton
        Me.BtResetApproval = New DevExpress.XtraEditors.SimpleButton
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
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.be_document.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit_doc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit_doc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.req_type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_tran_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_pjc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_cc_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_sb_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_rmks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_end_user.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_requested.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_due_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_need_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_need_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_cmaddr_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.req_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_work_flow.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_email.SuspendLayout()
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl2.SuspendLayout()
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_smart_approve.SuspendLayout()
        CType(Me.scc_smart_approve, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_smart_approve.SuspendLayout()
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_file.SuspendLayout()
        CType(Me.gc_detail_doc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gv_detail_doc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPRDetail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(880, 492)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.BtResetApproval)
        Me.scc_master.Panel1.Controls.Add(Me.BtReload)
        Me.scc_master.Panel1.Controls.Add(Me.BtPacking)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl4)
        Me.scc_master.Panel1.Controls.Add(Me.TxtPRDetail)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(882, 548)
        Me.scc_master.SplitterPosition = 29
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(880, 492)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(870, 447)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(882, 513)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(870, 482)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "First Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(62, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 1
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(173, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 2
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(225, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 3
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.SplitContainer2)
        Me.lci_master.Controls.Add(Me.SplitContainer1)
        Me.lci_master.Controls.Add(Me.req_type)
        Me.lci_master.Controls.Add(Me.gc_edit)
        Me.lci_master.Controls.Add(Me.req_tran_id)
        Me.lci_master.Controls.Add(Me.req_pjc_id)
        Me.lci_master.Controls.Add(Me.req_si_id)
        Me.lci_master.Controls.Add(Me.req_cc_id)
        Me.lci_master.Controls.Add(Me.req_sb_id)
        Me.lci_master.Controls.Add(Me.req_rmks)
        Me.lci_master.Controls.Add(Me.req_end_user)
        Me.lci_master.Controls.Add(Me.req_requested)
        Me.lci_master.Controls.Add(Me.req_due_date)
        Me.lci_master.Controls.Add(Me.req_need_date)
        Me.lci_master.Controls.Add(Me.req_date)
        Me.lci_master.Controls.Add(Me.req_ptnr_id)
        Me.lci_master.Controls.Add(Me.req_cmaddr_id)
        Me.lci_master.Controls.Add(Me.req_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(870, 447)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Location = New System.Drawing.Point(24, 46)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.be_document)
        Me.SplitContainer2.Panel1.Controls.Add(Me.bt_download)
        Me.SplitContainer2.Panel1.Controls.Add(Me.bt_preview)
        Me.SplitContainer2.Panel1.Controls.Add(Me.LabelControl69)
        Me.SplitContainer2.Panel1.Controls.Add(Me.bt_hapus_dokumen)
        Me.SplitContainer2.Panel1.Controls.Add(Me.bt_tambah_dokumen)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gc_edit_doc)
        Me.SplitContainer2.Size = New System.Drawing.Size(805, 390)
        Me.SplitContainer2.SplitterDistance = 51
        Me.SplitContainer2.TabIndex = 21
        '
        'be_document
        '
        Me.be_document.Location = New System.Drawing.Point(75, 13)
        Me.be_document.Name = "be_document"
        Me.be_document.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_document.Size = New System.Drawing.Size(331, 20)
        Me.be_document.TabIndex = 165
        '
        'bt_download
        '
        Me.bt_download.Location = New System.Drawing.Point(604, 12)
        Me.bt_download.Name = "bt_download"
        Me.bt_download.Size = New System.Drawing.Size(86, 23)
        Me.bt_download.TabIndex = 164
        Me.bt_download.Text = "Download"
        Me.bt_download.Visible = False
        '
        'bt_preview
        '
        Me.bt_preview.Location = New System.Drawing.Point(512, 12)
        Me.bt_preview.Name = "bt_preview"
        Me.bt_preview.Size = New System.Drawing.Size(86, 23)
        Me.bt_preview.TabIndex = 163
        Me.bt_preview.Text = "Preview"
        Me.bt_preview.Visible = False
        '
        'LabelControl69
        '
        Me.LabelControl69.Location = New System.Drawing.Point(12, 17)
        Me.LabelControl69.Name = "LabelControl69"
        Me.LabelControl69.Size = New System.Drawing.Size(48, 13)
        Me.LabelControl69.TabIndex = 162
        Me.LabelControl69.Text = "Document"
        '
        'bt_hapus_dokumen
        '
        Me.bt_hapus_dokumen.Location = New System.Drawing.Point(465, 14)
        Me.bt_hapus_dokumen.Name = "bt_hapus_dokumen"
        Me.bt_hapus_dokumen.Size = New System.Drawing.Size(41, 20)
        Me.bt_hapus_dokumen.TabIndex = 160
        Me.bt_hapus_dokumen.Text = "-"
        '
        'bt_tambah_dokumen
        '
        Me.bt_tambah_dokumen.Location = New System.Drawing.Point(418, 14)
        Me.bt_tambah_dokumen.Name = "bt_tambah_dokumen"
        Me.bt_tambah_dokumen.Size = New System.Drawing.Size(41, 20)
        Me.bt_tambah_dokumen.TabIndex = 159
        Me.bt_tambah_dokumen.Text = "+"
        '
        'gc_edit_doc
        '
        Me.gc_edit_doc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_edit_doc.Location = New System.Drawing.Point(0, 0)
        Me.gc_edit_doc.MainView = Me.gv_edit_doc
        Me.gc_edit_doc.Name = "gc_edit_doc"
        Me.gc_edit_doc.Size = New System.Drawing.Size(805, 335)
        Me.gc_edit_doc.TabIndex = 1
        Me.gc_edit_doc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit_doc, Me.GridView6})
        '
        'gv_edit_doc
        '
        Me.gv_edit_doc.GridControl = Me.gc_edit_doc
        Me.gv_edit_doc.Name = "gv_edit_doc"
        Me.gv_edit_doc.OptionsView.ColumnAutoWidth = False
        Me.gv_edit_doc.OptionsView.ShowGroupPanel = False
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.gc_edit_doc
        Me.GridView6.Name = "GridView6"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(118, 452)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Size = New System.Drawing.Size(723, 20)
        Me.SplitContainer1.SplitterDistance = 232
        Me.SplitContainer1.TabIndex = 20
        '
        'req_type
        '
        Me.req_type.Location = New System.Drawing.Point(142, 346)
        Me.req_type.Name = "req_type"
        Me.req_type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_type.Properties.Items.AddRange(New Object() {"Bugdet", "Non Bugdet", "Reimburse"})
        Me.req_type.Size = New System.Drawing.Size(283, 20)
        Me.req_type.StyleController = Me.lci_master
        Me.req_type.TabIndex = 19
        '
        'gc_edit
        '
        Me.gc_edit.Location = New System.Drawing.Point(24, 46)
        Me.gc_edit.MainView = Me.gv_edit
        Me.gc_edit.Name = "gc_edit"
        Me.gc_edit.Size = New System.Drawing.Size(805, 390)
        Me.gc_edit.TabIndex = 18
        Me.gc_edit.UseEmbeddedNavigator = True
        Me.gc_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.gc_edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsBehavior.AllowIncrementalSearch = True
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowFooter = True
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'req_tran_id
        '
        Me.req_tran_id.Location = New System.Drawing.Point(142, 394)
        Me.req_tran_id.Name = "req_tran_id"
        Me.req_tran_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_tran_id.Size = New System.Drawing.Size(283, 20)
        Me.req_tran_id.StyleController = Me.lci_master
        Me.req_tran_id.TabIndex = 17
        '
        'req_pjc_id
        '
        Me.req_pjc_id.Location = New System.Drawing.Point(142, 298)
        Me.req_pjc_id.Name = "req_pjc_id"
        Me.req_pjc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_pjc_id.Size = New System.Drawing.Size(283, 20)
        Me.req_pjc_id.StyleController = Me.lci_master
        Me.req_pjc_id.TabIndex = 16
        '
        'req_si_id
        '
        Me.req_si_id.Location = New System.Drawing.Point(535, 58)
        Me.req_si_id.Name = "req_si_id"
        Me.req_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_si_id.Size = New System.Drawing.Size(282, 20)
        Me.req_si_id.StyleController = Me.lci_master
        Me.req_si_id.TabIndex = 15
        '
        'req_cc_id
        '
        Me.req_cc_id.Location = New System.Drawing.Point(142, 274)
        Me.req_cc_id.Name = "req_cc_id"
        Me.req_cc_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_cc_id.Size = New System.Drawing.Size(283, 20)
        Me.req_cc_id.StyleController = Me.lci_master
        Me.req_cc_id.TabIndex = 14
        '
        'req_sb_id
        '
        Me.req_sb_id.Location = New System.Drawing.Point(142, 250)
        Me.req_sb_id.Name = "req_sb_id"
        Me.req_sb_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_sb_id.Size = New System.Drawing.Size(283, 20)
        Me.req_sb_id.StyleController = Me.lci_master
        Me.req_sb_id.TabIndex = 13
        '
        'req_rmks
        '
        Me.req_rmks.Location = New System.Drawing.Point(142, 370)
        Me.req_rmks.Name = "req_rmks"
        Me.req_rmks.Size = New System.Drawing.Size(283, 20)
        Me.req_rmks.StyleController = Me.lci_master
        Me.req_rmks.TabIndex = 12
        '
        'req_end_user
        '
        Me.req_end_user.Location = New System.Drawing.Point(535, 202)
        Me.req_end_user.Name = "req_end_user"
        Me.req_end_user.Size = New System.Drawing.Size(282, 20)
        Me.req_end_user.StyleController = Me.lci_master
        Me.req_end_user.TabIndex = 11
        '
        'req_requested
        '
        Me.req_requested.Location = New System.Drawing.Point(142, 202)
        Me.req_requested.Name = "req_requested"
        Me.req_requested.Size = New System.Drawing.Size(283, 20)
        Me.req_requested.StyleController = Me.lci_master
        Me.req_requested.TabIndex = 10
        '
        'req_due_date
        '
        Me.req_due_date.EditValue = Nothing
        Me.req_due_date.Location = New System.Drawing.Point(535, 154)
        Me.req_due_date.Name = "req_due_date"
        Me.req_due_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_due_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.req_due_date.Size = New System.Drawing.Size(282, 20)
        Me.req_due_date.StyleController = Me.lci_master
        Me.req_due_date.TabIndex = 9
        '
        'req_need_date
        '
        Me.req_need_date.EditValue = Nothing
        Me.req_need_date.Location = New System.Drawing.Point(142, 154)
        Me.req_need_date.Name = "req_need_date"
        Me.req_need_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_need_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.req_need_date.Size = New System.Drawing.Size(283, 20)
        Me.req_need_date.StyleController = Me.lci_master
        Me.req_need_date.TabIndex = 8
        '
        'req_date
        '
        Me.req_date.EditValue = Nothing
        Me.req_date.Location = New System.Drawing.Point(142, 130)
        Me.req_date.Name = "req_date"
        Me.req_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.req_date.Size = New System.Drawing.Size(283, 20)
        Me.req_date.StyleController = Me.lci_master
        Me.req_date.TabIndex = 7
        '
        'req_ptnr_id
        '
        Me.req_ptnr_id.Location = New System.Drawing.Point(535, 82)
        Me.req_ptnr_id.Name = "req_ptnr_id"
        Me.req_ptnr_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_ptnr_id.Size = New System.Drawing.Size(282, 20)
        Me.req_ptnr_id.StyleController = Me.lci_master
        Me.req_ptnr_id.TabIndex = 6
        '
        'req_cmaddr_id
        '
        Me.req_cmaddr_id.Location = New System.Drawing.Point(142, 82)
        Me.req_cmaddr_id.Name = "req_cmaddr_id"
        Me.req_cmaddr_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_cmaddr_id.Size = New System.Drawing.Size(283, 20)
        Me.req_cmaddr_id.StyleController = Me.lci_master
        Me.req_cmaddr_id.TabIndex = 5
        '
        'req_en_id
        '
        Me.req_en_id.Location = New System.Drawing.Point(142, 58)
        Me.req_en_id.Name = "req_en_id"
        Me.req_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.req_en_id.Size = New System.Drawing.Size(283, 20)
        Me.req_en_id.StyleController = Me.lci_master
        Me.req_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tcg_header, Me.LayoutControlItem17})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(853, 484)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'tcg_header
        '
        Me.tcg_header.CustomizationFormText = "tcg_header"
        Me.tcg_header.Location = New System.Drawing.Point(0, 0)
        Me.tcg_header.Name = "tcg_header"
        Me.tcg_header.SelectedTabPage = Me.LayoutControlGroup9
        Me.tcg_header.SelectedTabPageIndex = 2
        Me.tcg_header.Size = New System.Drawing.Size(833, 440)
        Me.tcg_header.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup4, Me.LayoutControlGroup9})
        Me.tcg_header.Text = "tcg_header"
        '
        'LayoutControlGroup9
        '
        Me.LayoutControlGroup9.CustomizationFormText = "Document"
        Me.LayoutControlGroup9.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem18})
        Me.LayoutControlGroup9.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup9.Name = "LayoutControlGroup9"
        Me.LayoutControlGroup9.Size = New System.Drawing.Size(809, 394)
        Me.LayoutControlGroup9.Text = "Document"
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.SplitContainer2
        Me.LayoutControlItem18.CustomizationFormText = "LayoutControlItem18"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(809, 394)
        Me.LayoutControlItem18.Text = "LayoutControlItem18"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextToControlDistance = 0
        Me.LayoutControlItem18.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "Header"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.LayoutControlGroup3, Me.LayoutControlGroup5, Me.LayoutControlGroup6, Me.LayoutControlGroup8, Me.LayoutControlGroup7})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(809, 394)
        Me.LayoutControlGroup2.Text = "Header"
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 384)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(809, 10)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem12})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(809, 72)
        Me.LayoutControlGroup3.Text = "LayoutControlGroup3"
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.req_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.req_cmaddr_id
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem2.Text = "Company Address"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.req_ptnr_id
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(393, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(392, 24)
        Me.LayoutControlItem3.Text = "Supplier"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.req_si_id
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(393, 0)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(392, 24)
        Me.LayoutControlItem12.Text = "Site"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem8, Me.LayoutControlItem6, Me.LayoutControlItem5, Me.LayoutControlItem4})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(809, 72)
        Me.LayoutControlGroup5.Text = "LayoutControlGroup5"
        Me.LayoutControlGroup5.TextVisible = False
        '
        'EmptySpaceItem8
        '
        Me.EmptySpaceItem8.CustomizationFormText = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Location = New System.Drawing.Point(393, 0)
        Me.EmptySpaceItem8.Name = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem8.Text = "EmptySpaceItem8"
        Me.EmptySpaceItem8.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.req_due_date
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(393, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(392, 24)
        Me.LayoutControlItem6.Text = "Due Date"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.req_need_date
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem5.Text = "Need Date"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.req_date
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem4.Text = "Date"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem8})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(809, 48)
        Me.LayoutControlGroup6.Text = "LayoutControlGroup6"
        Me.LayoutControlGroup6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.req_requested
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem7.Text = "Requested By"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.req_end_user
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(393, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(392, 24)
        Me.LayoutControlItem8.Text = "End User"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.CustomizationFormText = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem9, Me.LayoutControlItem14, Me.EmptySpaceItem4, Me.EmptySpaceItem3, Me.EmptySpaceItem2})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 288)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(809, 96)
        Me.LayoutControlGroup8.Text = "LayoutControlGroup8"
        Me.LayoutControlGroup8.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.req_type
        Me.LayoutControlItem16.CustomizationFormText = "Type"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem16.Text = "Type"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.req_rmks
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem9.Text = "Remarks"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.req_tran_id
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem14.Text = "Transaction"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(102, 13)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(393, 48)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(393, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(393, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup7
        '
        Me.LayoutControlGroup7.CustomizationFormText = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem7, Me.EmptySpaceItem6, Me.EmptySpaceItem5, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem13})
        Me.LayoutControlGroup7.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlGroup7.Name = "LayoutControlGroup7"
        Me.LayoutControlGroup7.Size = New System.Drawing.Size(809, 96)
        Me.LayoutControlGroup7.Text = "LayoutControlGroup7"
        Me.LayoutControlGroup7.TextVisible = False
        '
        'EmptySpaceItem7
        '
        Me.EmptySpaceItem7.CustomizationFormText = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Location = New System.Drawing.Point(393, 48)
        Me.EmptySpaceItem7.Name = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem7.Text = "EmptySpaceItem7"
        Me.EmptySpaceItem7.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(393, 24)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(393, 0)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(392, 24)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.req_sb_id
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem10.Text = "Sub Account"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.req_cc_id
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem11.Text = "Cost Center"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.req_pjc_id
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem13.Text = "Project Name"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(102, 13)
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Detail"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem15})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(809, 394)
        Me.LayoutControlGroup4.Text = "Detail"
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.gc_edit
        Me.LayoutControlItem15.CustomizationFormText = "s"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(111, 31)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(809, 394)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.Text = "s"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.SplitContainer1
        Me.LayoutControlItem17.CustomizationFormText = "LayoutControlItem17"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 440)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(833, 24)
        Me.LayoutControlItem17.Text = "LayoutControlItem17"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(102, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
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
        Me.dp_detail.ID = New System.Guid("525bedab-0583-4df5-bbe3-28bd20f577a8")
        Me.dp_detail.Location = New System.Drawing.Point(0, 548)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 200)
        Me.dp_detail.Size = New System.Drawing.Size(882, 200)
        Me.dp_detail.Text = "Data Detail"
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
        Me.xtc_detail.SelectedTabPage = Me.xtp_work_flow
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_detail.Size = New System.Drawing.Size(876, 172)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail, Me.xtp_work_flow, Me.xtp_email, Me.xtp_smart_approve, Me.xtp_file})
        '
        'xtp_work_flow
        '
        Me.xtp_work_flow.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_work_flow.Name = "xtp_work_flow"
        Me.xtp_work_flow.Size = New System.Drawing.Size(874, 151)
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
        Me.SplitContainerControl1.Size = New System.Drawing.Size(874, 151)
        Me.SplitContainerControl1.SplitterPosition = 467
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_wf
        '
        Me.gc_wf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_wf.Location = New System.Drawing.Point(0, 0)
        Me.gc_wf.MainView = Me.gv_wf
        Me.gc_wf.Name = "gc_wf"
        Me.gc_wf.Size = New System.Drawing.Size(874, 151)
        Me.gc_wf.TabIndex = 0
        Me.gc_wf.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_wf, Me.GridView3})
        '
        'gv_wf
        '
        Me.gv_wf.GridControl = Me.gc_wf
        Me.gv_wf.Name = "gv_wf"
        Me.gv_wf.OptionsView.ColumnAutoWidth = False
        Me.gv_wf.OptionsView.ShowGroupPanel = False
        '
        'GridView3
        '
        Me.GridView3.GridControl = Me.gc_wf
        Me.GridView3.Name = "GridView3"
        '
        'xtp_detail
        '
        Me.xtp_detail.Controls.Add(Me.scc_detail)
        Me.xtp_detail.Name = "xtp_detail"
        Me.xtp_detail.Size = New System.Drawing.Size(874, 151)
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
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail, Me.GridView4})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.gc_detail
        Me.GridView4.Name = "GridView4"
        '
        'xtp_email
        '
        Me.xtp_email.Controls.Add(Me.SplitContainerControl2)
        Me.xtp_email.Name = "xtp_email"
        Me.xtp_email.PageVisible = False
        Me.xtp_email.Size = New System.Drawing.Size(874, 151)
        Me.xtp_email.Text = "For Email"
        '
        'SplitContainerControl2
        '
        Me.SplitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl2.Name = "SplitContainerControl2"
        Me.SplitContainerControl2.Panel1.Controls.Add(Me.gc_email)
        Me.SplitContainerControl2.Panel1.Text = "Panel1"
        Me.SplitContainerControl2.Panel2.Text = "Panel2"
        Me.SplitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl2.Size = New System.Drawing.Size(874, 151)
        Me.SplitContainerControl2.SplitterPosition = 467
        Me.SplitContainerControl2.TabIndex = 1
        Me.SplitContainerControl2.Text = "SplitContainerControl2"
        '
        'gc_email
        '
        Me.gc_email.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_email.Location = New System.Drawing.Point(0, 0)
        Me.gc_email.MainView = Me.gv_email
        Me.gc_email.Name = "gc_email"
        Me.gc_email.Size = New System.Drawing.Size(874, 151)
        Me.gc_email.TabIndex = 0
        Me.gc_email.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_email, Me.GridView2})
        '
        'gv_email
        '
        Me.gv_email.GridControl = Me.gc_email
        Me.gv_email.Name = "gv_email"
        Me.gv_email.OptionsView.ColumnAutoWidth = False
        Me.gv_email.OptionsView.ShowGroupPanel = False
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_email
        Me.GridView2.Name = "GridView2"
        '
        'xtp_smart_approve
        '
        Me.xtp_smart_approve.Controls.Add(Me.scc_smart_approve)
        Me.xtp_smart_approve.Name = "xtp_smart_approve"
        Me.xtp_smart_approve.Size = New System.Drawing.Size(874, 151)
        Me.xtp_smart_approve.Text = "Smart Approve"
        '
        'scc_smart_approve
        '
        Me.scc_smart_approve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_smart_approve.Location = New System.Drawing.Point(0, 0)
        Me.scc_smart_approve.Name = "scc_smart_approve"
        Me.scc_smart_approve.Panel1.Controls.Add(Me.gc_smart)
        Me.scc_smart_approve.Panel1.Text = "Panel1"
        Me.scc_smart_approve.Panel2.Text = "Panel2"
        Me.scc_smart_approve.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_smart_approve.Size = New System.Drawing.Size(874, 151)
        Me.scc_smart_approve.SplitterPosition = 608
        Me.scc_smart_approve.TabIndex = 1
        Me.scc_smart_approve.Text = "SplitContainerControl1"
        '
        'gc_smart
        '
        Me.gc_smart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_smart.Location = New System.Drawing.Point(0, 0)
        Me.gc_smart.MainView = Me.gv_smart
        Me.gc_smart.Name = "gc_smart"
        Me.gc_smart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.gc_smart.Size = New System.Drawing.Size(874, 151)
        Me.gc_smart.TabIndex = 2
        Me.gc_smart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_smart, Me.GridView1})
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
        'GridView1
        '
        Me.GridView1.GridControl = Me.gc_smart
        Me.GridView1.Name = "GridView1"
        '
        'xtp_file
        '
        Me.xtp_file.Controls.Add(Me.gc_detail_doc)
        Me.xtp_file.Name = "xtp_file"
        Me.xtp_file.Size = New System.Drawing.Size(874, 151)
        Me.xtp_file.Text = "Document"
        '
        'gc_detail_doc
        '
        Me.gc_detail_doc.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gc_detail_doc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail_doc.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail_doc.MainView = Me.gv_detail_doc
        Me.gc_detail_doc.Name = "gc_detail_doc"
        Me.gc_detail_doc.Size = New System.Drawing.Size(874, 151)
        Me.gc_detail_doc.TabIndex = 1
        Me.gc_detail_doc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail_doc, Me.GridView7})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddDocumentToolStripMenuItem, Me.RemoveDocumentToolStripMenuItem, Me.ShowFileToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(177, 70)
        '
        'AddDocumentToolStripMenuItem
        '
        Me.AddDocumentToolStripMenuItem.Name = "AddDocumentToolStripMenuItem"
        Me.AddDocumentToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.AddDocumentToolStripMenuItem.Text = "Add Document"
        '
        'RemoveDocumentToolStripMenuItem
        '
        Me.RemoveDocumentToolStripMenuItem.Name = "RemoveDocumentToolStripMenuItem"
        Me.RemoveDocumentToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.RemoveDocumentToolStripMenuItem.Text = "Remove Document"
        '
        'ShowFileToolStripMenuItem
        '
        Me.ShowFileToolStripMenuItem.Name = "ShowFileToolStripMenuItem"
        Me.ShowFileToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ShowFileToolStripMenuItem.Text = "Show File"
        '
        'gv_detail_doc
        '
        Me.gv_detail_doc.GridControl = Me.gc_detail_doc
        Me.gv_detail_doc.Name = "gv_detail_doc"
        Me.gv_detail_doc.OptionsView.ColumnAutoWidth = False
        Me.gv_detail_doc.OptionsView.ShowGroupPanel = False
        '
        'GridView7
        '
        Me.GridView7.GridControl = Me.gc_detail_doc
        Me.GridView7.Name = "GridView7"
        '
        'TxtPRDetail
        '
        Me.TxtPRDetail.Location = New System.Drawing.Point(440, 3)
        Me.TxtPRDetail.Name = "TxtPRDetail"
        Me.TxtPRDetail.Size = New System.Drawing.Size(149, 20)
        Me.TxtPRDetail.TabIndex = 4
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(377, 6)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(43, 13)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "PR Detail"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(624, 6)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl4.TabIndex = 5
        Me.LabelControl4.Text = "."
        '
        'BtPacking
        '
        Me.BtPacking.Location = New System.Drawing.Point(807, 3)
        Me.BtPacking.Name = "BtPacking"
        Me.BtPacking.Size = New System.Drawing.Size(75, 23)
        Me.BtPacking.TabIndex = 6
        Me.BtPacking.Text = "Packing"
        Me.BtPacking.Visible = False
        '
        'BtReload
        '
        Me.BtReload.Location = New System.Drawing.Point(611, 2)
        Me.BtReload.Name = "BtReload"
        Me.BtReload.Size = New System.Drawing.Size(84, 23)
        Me.BtReload.TabIndex = 7
        Me.BtReload.Text = "Reload Routing"
        '
        'BtResetApproval
        '
        Me.BtResetApproval.Location = New System.Drawing.Point(701, 2)
        Me.BtResetApproval.Name = "BtResetApproval"
        Me.BtResetApproval.Size = New System.Drawing.Size(84, 23)
        Me.BtResetApproval.TabIndex = 7
        Me.BtResetApproval.Text = "Reset Approval"
        '
        'FRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(882, 748)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FRequisition"
        Me.Text = "Purchase Requisition"
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
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.be_document.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit_doc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit_doc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.req_type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_tran_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_pjc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_cc_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_sb_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_rmks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_end_user.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_requested.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_due_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_due_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_need_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_need_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_cmaddr_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.req_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tcg_header, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_work_flow.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_wf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_email.ResumeLayout(False)
        CType(Me.SplitContainerControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl2.ResumeLayout(False)
        CType(Me.gc_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_email, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_smart_approve.ResumeLayout(False)
        CType(Me.scc_smart_approve, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_smart_approve.ResumeLayout(False)
        CType(Me.gc_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_smart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_file.ResumeLayout(False)
        CType(Me.gc_detail_doc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gv_detail_doc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPRDetail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents req_rmks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents req_end_user As DevExpress.XtraEditors.TextEdit
    Friend WithEvents req_requested As DevExpress.XtraEditors.TextEdit
    Friend WithEvents req_due_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents req_need_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents req_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents req_tran_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents req_pjc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents req_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents req_cc_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents req_sb_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Public WithEvents req_en_id As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents req_ptnr_id As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents req_cmaddr_id As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents gc_edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents req_type As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents tcg_header As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup7 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_work_flow As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_wf As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_wf As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_email As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl2 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_email As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_email As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_smart_approve As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_smart_approve As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_smart As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_smart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents col_select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents TxtPRDetail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlGroup9 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents gc_edit_doc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_edit_doc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl69 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents bt_hapus_dokumen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_tambah_dokumen As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_download As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_preview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents be_document As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents xtp_file As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_detail_doc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail_doc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtPacking As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtReload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtResetApproval As DevExpress.XtraEditors.SimpleButton

End Class
