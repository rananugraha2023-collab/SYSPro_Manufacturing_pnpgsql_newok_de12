<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FWOLaborFeedbackBackup
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
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl()
        Me.gc_downtime_edit = New DevExpress.XtraGrid.GridControl()
        Me.gv_downtime_edit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gc_reject_edit = New DevExpress.XtraGrid.GridControl()
        Me.gv_reject_edit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lbrf_mch_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.gc_person_edit = New DevExpress.XtraGrid.GridControl()
        Me.gv_person_edit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.lbrf_qty_conversion = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_shift_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbrf_activity_type_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbrf_qc_out_reason_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbrf_qc_in_reason_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbrf_down_stop = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_down_start = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_qty_reject = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_date = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_down_reason_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.lbrf_elapsed_down = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_elapsed_run = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_stop_run = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_start_run = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_elapsed_setup = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_stop_setup = New DevExpress.XtraEditors.DateEdit()
        Me.lbrf_start_setup = New DevExpress.XtraEditors.DateEdit()
        Me.wc_desc = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_wodr_uid = New DevExpress.XtraEditors.ButtonEdit()
        Me.lbrf_remarks = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_qty_complete = New DevExpress.XtraEditors.TextEdit()
        Me.lbrf_en_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem24 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl()
        Me.xtp_downtime = New DevExpress.XtraTab.XtraTabPage()
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gc_downtime = New DevExpress.XtraGrid.GridControl()
        Me.gv_downtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtp_reject = New DevExpress.XtraTab.XtraTabPage()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gc_reject = New DevExpress.XtraGrid.GridControl()
        Me.gv_reject = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xtp_person = New DevExpress.XtraTab.XtraTabPage()
        Me.gc_person_detail = New DevExpress.XtraGrid.GridControl()
        Me.gv_person_detail = New DevExpress.XtraGrid.Views.Grid.GridView()
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.gc_downtime_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_downtime_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_reject_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_reject_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_mch_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_person_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_person_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_qty_conversion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_shift_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_activity_type_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_qc_out_reason_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_qc_in_reason_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_down_stop.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_down_stop.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_down_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_down_start.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_qty_reject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_down_reason_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_elapsed_down.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_elapsed_run.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_stop_run.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_stop_run.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_start_run.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_start_run.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_elapsed_setup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_stop_setup.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_stop_setup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_start_setup.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_start_setup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_wodr_uid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_qty_complete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbrf_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_downtime.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_downtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_downtime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_reject.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_reject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_reject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_person.SuspendLayout()
        CType(Me.gc_person_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_person_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(923, 553)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Size = New System.Drawing.Size(925, 611)
        Me.scc_master.SplitterPosition = 32
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(923, 553)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(913, 508)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(925, 574)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(913, 543)
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
        Me.lci_master.Controls.Add(Me.gc_downtime_edit)
        Me.lci_master.Controls.Add(Me.gc_reject_edit)
        Me.lci_master.Controls.Add(Me.lbrf_mch_id)
        Me.lci_master.Controls.Add(Me.gc_person_edit)
        Me.lci_master.Controls.Add(Me.lbrf_qty_conversion)
        Me.lci_master.Controls.Add(Me.lbrf_shift_id)
        Me.lci_master.Controls.Add(Me.lbrf_activity_type_id)
        Me.lci_master.Controls.Add(Me.lbrf_qc_out_reason_id)
        Me.lci_master.Controls.Add(Me.lbrf_qc_in_reason_id)
        Me.lci_master.Controls.Add(Me.lbrf_down_stop)
        Me.lci_master.Controls.Add(Me.lbrf_down_start)
        Me.lci_master.Controls.Add(Me.lbrf_qty_reject)
        Me.lci_master.Controls.Add(Me.lbrf_date)
        Me.lci_master.Controls.Add(Me.lbrf_down_reason_id)
        Me.lci_master.Controls.Add(Me.lbrf_elapsed_down)
        Me.lci_master.Controls.Add(Me.lbrf_elapsed_run)
        Me.lci_master.Controls.Add(Me.lbrf_stop_run)
        Me.lci_master.Controls.Add(Me.lbrf_start_run)
        Me.lci_master.Controls.Add(Me.lbrf_elapsed_setup)
        Me.lci_master.Controls.Add(Me.lbrf_stop_setup)
        Me.lci_master.Controls.Add(Me.lbrf_start_setup)
        Me.lci_master.Controls.Add(Me.wc_desc)
        Me.lci_master.Controls.Add(Me.lbrf_wodr_uid)
        Me.lci_master.Controls.Add(Me.lbrf_remarks)
        Me.lci_master.Controls.Add(Me.lbrf_qty_complete)
        Me.lci_master.Controls.Add(Me.lbrf_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(913, 508)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'gc_downtime_edit
        '
        Me.gc_downtime_edit.Location = New System.Drawing.Point(430, 348)
        Me.gc_downtime_edit.MainView = Me.gv_downtime_edit
        Me.gc_downtime_edit.Name = "gc_downtime_edit"
        Me.gc_downtime_edit.Size = New System.Drawing.Size(174, 148)
        Me.gc_downtime_edit.TabIndex = 36
        Me.gc_downtime_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_downtime_edit})
        '
        'gv_downtime_edit
        '
        Me.gv_downtime_edit.GridControl = Me.gc_downtime_edit
        Me.gv_downtime_edit.Name = "gv_downtime_edit"
        '
        'gc_reject_edit
        '
        Me.gc_reject_edit.Location = New System.Drawing.Point(728, 348)
        Me.gc_reject_edit.MainView = Me.gv_reject_edit
        Me.gc_reject_edit.Name = "gc_reject_edit"
        Me.gc_reject_edit.Size = New System.Drawing.Size(173, 148)
        Me.gc_reject_edit.TabIndex = 35
        Me.gc_reject_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_reject_edit})
        '
        'gv_reject_edit
        '
        Me.gv_reject_edit.GridControl = Me.gc_reject_edit
        Me.gv_reject_edit.Name = "gv_reject_edit"
        '
        'lbrf_mch_id
        '
        Me.lbrf_mch_id.Location = New System.Drawing.Point(578, 60)
        Me.lbrf_mch_id.Name = "lbrf_mch_id"
        Me.lbrf_mch_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_mch_id.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_mch_id.StyleController = Me.lci_master
        Me.lbrf_mch_id.TabIndex = 34
        '
        'gc_person_edit
        '
        Me.gc_person_edit.Location = New System.Drawing.Point(132, 348)
        Me.gc_person_edit.MainView = Me.gv_person_edit
        Me.gc_person_edit.Name = "gc_person_edit"
        Me.gc_person_edit.Size = New System.Drawing.Size(174, 148)
        Me.gc_person_edit.TabIndex = 33
        Me.gc_person_edit.UseEmbeddedNavigator = True
        Me.gc_person_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_person_edit})
        '
        'gv_person_edit
        '
        Me.gv_person_edit.GridControl = Me.gc_person_edit
        Me.gv_person_edit.Name = "gv_person_edit"
        Me.gv_person_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_person_edit.OptionsView.ShowGroupPanel = False
        '
        'lbrf_qty_conversion
        '
        Me.lbrf_qty_conversion.Enabled = False
        Me.lbrf_qty_conversion.Location = New System.Drawing.Point(578, 84)
        Me.lbrf_qty_conversion.Name = "lbrf_qty_conversion"
        Me.lbrf_qty_conversion.Size = New System.Drawing.Size(183, 20)
        Me.lbrf_qty_conversion.StyleController = Me.lci_master
        Me.lbrf_qty_conversion.TabIndex = 32
        '
        'lbrf_shift_id
        '
        Me.lbrf_shift_id.Location = New System.Drawing.Point(132, 132)
        Me.lbrf_shift_id.Name = "lbrf_shift_id"
        Me.lbrf_shift_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_shift_id.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_shift_id.StyleController = Me.lci_master
        Me.lbrf_shift_id.TabIndex = 31
        '
        'lbrf_activity_type_id
        '
        Me.lbrf_activity_type_id.Location = New System.Drawing.Point(132, 84)
        Me.lbrf_activity_type_id.Name = "lbrf_activity_type_id"
        Me.lbrf_activity_type_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_activity_type_id.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_activity_type_id.StyleController = Me.lci_master
        Me.lbrf_activity_type_id.TabIndex = 30
        '
        'lbrf_qc_out_reason_id
        '
        Me.lbrf_qc_out_reason_id.Location = New System.Drawing.Point(132, 156)
        Me.lbrf_qc_out_reason_id.Name = "lbrf_qc_out_reason_id"
        Me.lbrf_qc_out_reason_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_qc_out_reason_id.Size = New System.Drawing.Size(769, 20)
        Me.lbrf_qc_out_reason_id.StyleController = Me.lci_master
        Me.lbrf_qc_out_reason_id.TabIndex = 28
        '
        'lbrf_qc_in_reason_id
        '
        Me.lbrf_qc_in_reason_id.Location = New System.Drawing.Point(578, 132)
        Me.lbrf_qc_in_reason_id.Name = "lbrf_qc_in_reason_id"
        Me.lbrf_qc_in_reason_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_qc_in_reason_id.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_qc_in_reason_id.StyleController = Me.lci_master
        Me.lbrf_qc_in_reason_id.TabIndex = 27
        '
        'lbrf_down_stop
        '
        Me.lbrf_down_stop.EditValue = Nothing
        Me.lbrf_down_stop.Location = New System.Drawing.Point(578, 276)
        Me.lbrf_down_stop.Name = "lbrf_down_stop"
        Me.lbrf_down_stop.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_down_stop.Properties.Mask.EditMask = "G"
        Me.lbrf_down_stop.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_down_stop.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_down_stop.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_down_stop.StyleController = Me.lci_master
        Me.lbrf_down_stop.TabIndex = 26
        '
        'lbrf_down_start
        '
        Me.lbrf_down_start.EditValue = Nothing
        Me.lbrf_down_start.Location = New System.Drawing.Point(132, 276)
        Me.lbrf_down_start.Name = "lbrf_down_start"
        Me.lbrf_down_start.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_down_start.Properties.Mask.EditMask = "G"
        Me.lbrf_down_start.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_down_start.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_down_start.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_down_start.StyleController = Me.lci_master
        Me.lbrf_down_start.TabIndex = 25
        '
        'lbrf_qty_reject
        '
        Me.lbrf_qty_reject.Location = New System.Drawing.Point(578, 108)
        Me.lbrf_qty_reject.Name = "lbrf_qty_reject"
        Me.lbrf_qty_reject.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_qty_reject.StyleController = Me.lci_master
        Me.lbrf_qty_reject.TabIndex = 24
        '
        'lbrf_date
        '
        Me.lbrf_date.EditValue = Nothing
        Me.lbrf_date.Location = New System.Drawing.Point(578, 12)
        Me.lbrf_date.Name = "lbrf_date"
        Me.lbrf_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_date.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_date.StyleController = Me.lci_master
        Me.lbrf_date.TabIndex = 22
        '
        'lbrf_down_reason_id
        '
        Me.lbrf_down_reason_id.Location = New System.Drawing.Point(578, 300)
        Me.lbrf_down_reason_id.Name = "lbrf_down_reason_id"
        Me.lbrf_down_reason_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_down_reason_id.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_down_reason_id.StyleController = Me.lci_master
        Me.lbrf_down_reason_id.TabIndex = 20
        '
        'lbrf_elapsed_down
        '
        Me.lbrf_elapsed_down.Enabled = False
        Me.lbrf_elapsed_down.Location = New System.Drawing.Point(132, 300)
        Me.lbrf_elapsed_down.Name = "lbrf_elapsed_down"
        Me.lbrf_elapsed_down.Properties.Mask.EditMask = "n"
        Me.lbrf_elapsed_down.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lbrf_elapsed_down.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_elapsed_down.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_elapsed_down.StyleController = Me.lci_master
        Me.lbrf_elapsed_down.TabIndex = 19
        '
        'lbrf_elapsed_run
        '
        Me.lbrf_elapsed_run.Enabled = False
        Me.lbrf_elapsed_run.Location = New System.Drawing.Point(132, 252)
        Me.lbrf_elapsed_run.Name = "lbrf_elapsed_run"
        Me.lbrf_elapsed_run.Properties.Mask.EditMask = "n"
        Me.lbrf_elapsed_run.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lbrf_elapsed_run.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_elapsed_run.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_elapsed_run.StyleController = Me.lci_master
        Me.lbrf_elapsed_run.TabIndex = 18
        '
        'lbrf_stop_run
        '
        Me.lbrf_stop_run.EditValue = Nothing
        Me.lbrf_stop_run.Location = New System.Drawing.Point(578, 228)
        Me.lbrf_stop_run.Name = "lbrf_stop_run"
        Me.lbrf_stop_run.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_stop_run.Properties.DisplayFormat.FormatString = "G"
        Me.lbrf_stop_run.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_stop_run.Properties.EditFormat.FormatString = "G"
        Me.lbrf_stop_run.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_stop_run.Properties.Mask.EditMask = "G"
        Me.lbrf_stop_run.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_stop_run.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_stop_run.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_stop_run.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_stop_run.StyleController = Me.lci_master
        Me.lbrf_stop_run.TabIndex = 17
        '
        'lbrf_start_run
        '
        Me.lbrf_start_run.EditValue = Nothing
        Me.lbrf_start_run.Location = New System.Drawing.Point(132, 228)
        Me.lbrf_start_run.Name = "lbrf_start_run"
        Me.lbrf_start_run.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_start_run.Properties.DisplayFormat.FormatString = "G"
        Me.lbrf_start_run.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_start_run.Properties.EditFormat.FormatString = "G"
        Me.lbrf_start_run.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_start_run.Properties.Mask.EditMask = "G"
        Me.lbrf_start_run.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_start_run.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_start_run.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_start_run.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_start_run.StyleController = Me.lci_master
        Me.lbrf_start_run.TabIndex = 16
        '
        'lbrf_elapsed_setup
        '
        Me.lbrf_elapsed_setup.Enabled = False
        Me.lbrf_elapsed_setup.Location = New System.Drawing.Point(132, 204)
        Me.lbrf_elapsed_setup.Name = "lbrf_elapsed_setup"
        Me.lbrf_elapsed_setup.Properties.Mask.EditMask = "n"
        Me.lbrf_elapsed_setup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lbrf_elapsed_setup.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_elapsed_setup.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_elapsed_setup.StyleController = Me.lci_master
        Me.lbrf_elapsed_setup.TabIndex = 15
        '
        'lbrf_stop_setup
        '
        Me.lbrf_stop_setup.EditValue = Nothing
        Me.lbrf_stop_setup.Location = New System.Drawing.Point(578, 180)
        Me.lbrf_stop_setup.Name = "lbrf_stop_setup"
        Me.lbrf_stop_setup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_stop_setup.Properties.DisplayFormat.FormatString = "G"
        Me.lbrf_stop_setup.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_stop_setup.Properties.EditFormat.FormatString = "G"
        Me.lbrf_stop_setup.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.lbrf_stop_setup.Properties.Mask.EditMask = "G"
        Me.lbrf_stop_setup.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_stop_setup.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_stop_setup.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_stop_setup.Size = New System.Drawing.Size(323, 20)
        Me.lbrf_stop_setup.StyleController = Me.lci_master
        Me.lbrf_stop_setup.TabIndex = 14
        '
        'lbrf_start_setup
        '
        Me.lbrf_start_setup.EditValue = Nothing
        Me.lbrf_start_setup.Location = New System.Drawing.Point(132, 180)
        Me.lbrf_start_setup.Name = "lbrf_start_setup"
        Me.lbrf_start_setup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_start_setup.Properties.DisplayFormat.FormatString = "G"
        Me.lbrf_start_setup.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_start_setup.Properties.EditFormat.FormatString = "G"
        Me.lbrf_start_setup.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.lbrf_start_setup.Properties.Mask.EditMask = "G"
        Me.lbrf_start_setup.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_start_setup.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.[True]
        Me.lbrf_start_setup.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_start_setup.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_start_setup.StyleController = Me.lci_master
        Me.lbrf_start_setup.TabIndex = 13
        '
        'wc_desc
        '
        Me.wc_desc.Location = New System.Drawing.Point(578, 36)
        Me.wc_desc.Name = "wc_desc"
        Me.wc_desc.Properties.ReadOnly = True
        Me.wc_desc.Size = New System.Drawing.Size(323, 20)
        Me.wc_desc.StyleController = Me.lci_master
        Me.wc_desc.TabIndex = 12
        '
        'lbrf_wodr_uid
        '
        Me.lbrf_wodr_uid.Location = New System.Drawing.Point(132, 36)
        Me.lbrf_wodr_uid.Name = "lbrf_wodr_uid"
        Me.lbrf_wodr_uid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.lbrf_wodr_uid.Properties.ReadOnly = True
        Me.lbrf_wodr_uid.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_wodr_uid.StyleController = Me.lci_master
        Me.lbrf_wodr_uid.TabIndex = 11
        '
        'lbrf_remarks
        '
        Me.lbrf_remarks.Location = New System.Drawing.Point(132, 324)
        Me.lbrf_remarks.Name = "lbrf_remarks"
        Me.lbrf_remarks.Size = New System.Drawing.Size(769, 20)
        Me.lbrf_remarks.StyleController = Me.lci_master
        Me.lbrf_remarks.TabIndex = 9
        '
        'lbrf_qty_complete
        '
        Me.lbrf_qty_complete.Location = New System.Drawing.Point(132, 108)
        Me.lbrf_qty_complete.Name = "lbrf_qty_complete"
        Me.lbrf_qty_complete.Properties.DisplayFormat.FormatString = "n"
        Me.lbrf_qty_complete.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lbrf_qty_complete.Properties.EditFormat.FormatString = "n"
        Me.lbrf_qty_complete.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lbrf_qty_complete.Properties.Mask.EditMask = "n"
        Me.lbrf_qty_complete.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.lbrf_qty_complete.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.lbrf_qty_complete.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_qty_complete.StyleController = Me.lci_master
        Me.lbrf_qty_complete.TabIndex = 8
        '
        'lbrf_en_id
        '
        Me.lbrf_en_id.Location = New System.Drawing.Point(132, 12)
        Me.lbrf_en_id.Name = "lbrf_en_id"
        Me.lbrf_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.lbrf_en_id.Size = New System.Drawing.Size(322, 20)
        Me.lbrf_en_id.StyleController = Me.lci_master
        Me.lbrf_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem5, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem6, Me.EmptySpaceItem1, Me.LayoutControlItem10, Me.LayoutControlItem11, Me.LayoutControlItem12, Me.LayoutControlItem13, Me.EmptySpaceItem4, Me.LayoutControlItem14, Me.LayoutControlItem7, Me.LayoutControlItem17, Me.LayoutControlItem15, Me.LayoutControlItem16, Me.LayoutControlItem18, Me.LayoutControlItem19, Me.LayoutControlItem21, Me.EmptySpaceItem2, Me.LayoutControlItem22, Me.LayoutControlItem23, Me.LayoutControlItem24, Me.LayoutControlItem20, Me.EmptySpaceItem3, Me.LayoutControlItem25, Me.LayoutControlItem26})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(913, 508)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.lbrf_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.lbrf_wodr_uid
        Me.LayoutControlItem8.CustomizationFormText = "Work Order"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem8.Text = "Work Order"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.wc_desc
        Me.LayoutControlItem9.CustomizationFormText = "Operation"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(446, 24)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem9.Text = "Work Center"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.lbrf_qty_complete
        Me.LayoutControlItem5.CustomizationFormText = "Quantity Complete"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem5.Text = "Quantity Complete"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.lbrf_start_setup
        Me.LayoutControlItem2.CustomizationFormText = "Setup Start"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 168)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem2.Text = "Setup Start"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.lbrf_stop_setup
        Me.LayoutControlItem3.CustomizationFormText = "Setup End"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(446, 168)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem3.Text = "Setup Stop"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.lbrf_elapsed_setup
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 192)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem4.Text = "Setup Elapsed"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.lbrf_remarks
        Me.LayoutControlItem6.CustomizationFormText = "Description"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 312)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(893, 24)
        Me.LayoutControlItem6.Text = "Remark"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(117, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(336, 192)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(336, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.lbrf_start_run
        Me.LayoutControlItem10.CustomizationFormText = "Run Start"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 216)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem10.Text = "Run Start"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.lbrf_stop_run
        Me.LayoutControlItem11.CustomizationFormText = "Run Stop"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(446, 216)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem11.Text = "Run Stop"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.lbrf_elapsed_run
        Me.LayoutControlItem12.CustomizationFormText = "Run Elapsed"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem12.Text = "Run Elapsed"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.lbrf_elapsed_down
        Me.LayoutControlItem13.CustomizationFormText = "Down Time"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 288)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem13.Text = "Down Time"
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(117, 13)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.AllowHotTrack = False
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(336, 240)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(336, 24)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.lbrf_down_reason_id
        Me.LayoutControlItem14.CustomizationFormText = "Down Reason"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(446, 288)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem14.Text = "Down Reason"
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.lbrf_date
        Me.LayoutControlItem7.CustomizationFormText = "Date"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(446, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem7.Text = "Date"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.Control = Me.lbrf_qty_reject
        Me.LayoutControlItem17.CustomizationFormText = "Quantity Reject"
        Me.LayoutControlItem17.Location = New System.Drawing.Point(446, 96)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem17.Text = "Quantity Reject"
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.lbrf_down_start
        Me.LayoutControlItem15.CustomizationFormText = "Down Start"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 264)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem15.Text = "Down Start"
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.lbrf_down_stop
        Me.LayoutControlItem16.CustomizationFormText = "Down Stop"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(446, 264)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem16.Text = "Down Stop"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.lbrf_qc_in_reason_id
        Me.LayoutControlItem18.CustomizationFormText = "Reject Reason Incoming"
        Me.LayoutControlItem18.Location = New System.Drawing.Point(446, 120)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem18.Text = "Reject Reason Incoming"
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.lbrf_qc_out_reason_id
        Me.LayoutControlItem19.CustomizationFormText = "Reject Reason Outgoing"
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(893, 24)
        Me.LayoutControlItem19.Text = "Reject Reason Outgoing"
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.Control = Me.lbrf_activity_type_id
        Me.LayoutControlItem21.CustomizationFormText = "Activity"
        Me.LayoutControlItem21.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem21.Text = "Activity"
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(117, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(567, 72)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(105, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.lbrf_shift_id
        Me.LayoutControlItem22.CustomizationFormText = "Shift"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(446, 24)
        Me.LayoutControlItem22.Text = "Shift"
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.lbrf_qty_conversion
        Me.LayoutControlItem23.CustomizationFormText = "Conversion"
        Me.LayoutControlItem23.Location = New System.Drawing.Point(446, 72)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(307, 24)
        Me.LayoutControlItem23.Text = "Conversion"
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem24
        '
        Me.LayoutControlItem24.Control = Me.gc_person_edit
        Me.LayoutControlItem24.CustomizationFormText = "Person"
        Me.LayoutControlItem24.Location = New System.Drawing.Point(0, 336)
        Me.LayoutControlItem24.Name = "LayoutControlItem24"
        Me.LayoutControlItem24.Size = New System.Drawing.Size(298, 152)
        Me.LayoutControlItem24.Text = "Person"
        Me.LayoutControlItem24.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.lbrf_mch_id
        Me.LayoutControlItem20.CustomizationFormText = "Machine"
        Me.LayoutControlItem20.Location = New System.Drawing.Point(446, 48)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(447, 24)
        Me.LayoutControlItem20.Text = "Machine"
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(117, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 48)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(336, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.Control = Me.gc_reject_edit
        Me.LayoutControlItem25.CustomizationFormText = "Reject"
        Me.LayoutControlItem25.Location = New System.Drawing.Point(596, 336)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(297, 152)
        Me.LayoutControlItem25.Text = "Reject"
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(117, 13)
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.Control = Me.gc_downtime_edit
        Me.LayoutControlItem26.CustomizationFormText = "Downtime"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(298, 336)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(298, 152)
        Me.LayoutControlItem26.Text = "Downtime"
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(117, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(477, 6)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 41
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(428, 9)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 40
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(317, 6)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 39
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(265, 9)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl1.TabIndex = 38
        Me.LabelControl1.Text = "WO Date"
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(42, 5)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(193, 20)
        Me.pr_entity.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 36
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
        Me.hideContainerBottom.BackColor = System.Drawing.SystemColors.Control
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 611)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(925, 19)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("7df89328-79f8-4b66-9427-14e3c4500237")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(925, 200)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(925, 200)
        Me.DockPanel1.Text = "Detail"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(917, 173)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_downtime
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_detail.Size = New System.Drawing.Size(917, 173)
        Me.xtc_detail.TabIndex = 4
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_downtime, Me.xtp_reject, Me.xtp_person})
        '
        'xtp_downtime
        '
        Me.xtp_downtime.Controls.Add(Me.scc_detail)
        Me.xtp_downtime.Name = "xtp_downtime"
        Me.xtp_downtime.Size = New System.Drawing.Size(915, 152)
        Me.xtp_downtime.Text = "Down Time"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_downtime)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(915, 152)
        Me.scc_detail.TabIndex = 0
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_downtime
        '
        Me.gc_downtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_downtime.Location = New System.Drawing.Point(0, 0)
        Me.gc_downtime.MainView = Me.gv_downtime
        Me.gc_downtime.Name = "gc_downtime"
        Me.gc_downtime.Size = New System.Drawing.Size(915, 152)
        Me.gc_downtime.TabIndex = 0
        Me.gc_downtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_downtime})
        '
        'gv_downtime
        '
        Me.gv_downtime.GridControl = Me.gc_downtime
        Me.gv_downtime.Name = "gv_downtime"
        Me.gv_downtime.OptionsView.ColumnAutoWidth = False
        Me.gv_downtime.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_downtime.OptionsView.ShowFooter = True
        '
        'xtp_reject
        '
        Me.xtp_reject.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_reject.Name = "xtp_reject"
        Me.xtp_reject.Size = New System.Drawing.Size(915, 152)
        Me.xtp_reject.Text = "Reject"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gc_reject)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl1.Size = New System.Drawing.Size(915, 152)
        Me.SplitContainerControl1.TabIndex = 1
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_reject
        '
        Me.gc_reject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_reject.Location = New System.Drawing.Point(0, 0)
        Me.gc_reject.MainView = Me.gv_reject
        Me.gc_reject.Name = "gc_reject"
        Me.gc_reject.Size = New System.Drawing.Size(915, 152)
        Me.gc_reject.TabIndex = 0
        Me.gc_reject.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_reject})
        '
        'gv_reject
        '
        Me.gv_reject.GridControl = Me.gc_reject
        Me.gv_reject.Name = "gv_reject"
        Me.gv_reject.OptionsView.ColumnAutoWidth = False
        Me.gv_reject.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_reject.OptionsView.ShowFooter = True
        '
        'xtp_person
        '
        Me.xtp_person.Controls.Add(Me.gc_person_detail)
        Me.xtp_person.Name = "xtp_person"
        Me.xtp_person.Size = New System.Drawing.Size(915, 152)
        Me.xtp_person.Text = "Person"
        '
        'gc_person_detail
        '
        Me.gc_person_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_person_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_person_detail.MainView = Me.gv_person_detail
        Me.gc_person_detail.Name = "gc_person_detail"
        Me.gc_person_detail.Size = New System.Drawing.Size(915, 152)
        Me.gc_person_detail.TabIndex = 0
        Me.gc_person_detail.UseEmbeddedNavigator = True
        Me.gc_person_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_person_detail})
        '
        'gv_person_detail
        '
        Me.gv_person_detail.GridControl = Me.gc_person_detail
        Me.gv_person_detail.Name = "gv_person_detail"
        Me.gv_person_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_person_detail.OptionsView.ShowGroupPanel = False
        '
        'FWOLaborFeedback
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(925, 630)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FWOLaborFeedback"
        Me.Text = "Work Order Labor Feedback"
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
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.gc_downtime_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_downtime_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_reject_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_reject_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_mch_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_person_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_person_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_qty_conversion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_shift_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_activity_type_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_qc_out_reason_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_qc_in_reason_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_down_stop.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_down_stop.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_down_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_down_start.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_qty_reject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_down_reason_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_elapsed_down.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_elapsed_run.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_stop_run.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_stop_run.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_start_run.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_start_run.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_elapsed_setup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_stop_setup.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_stop_setup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_start_setup.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_start_setup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_wodr_uid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_qty_complete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbrf_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_downtime.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_downtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_downtime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_reject.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_reject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_reject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_person.ResumeLayout(False)
        CType(Me.gc_person_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_person_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lbrf_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbrf_qty_complete As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_stop_setup As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lbrf_start_setup As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_elapsed_setup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents lbrf_elapsed_run As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lbrf_stop_run As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lbrf_start_run As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_elapsed_down As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents lbrf_down_reason_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents wc_desc As DevExpress.XtraEditors.TextEdit
    Public WithEvents lbrf_wodr_uid As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents lbrf_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_qty_reject As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_down_stop As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lbrf_down_start As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_qc_out_reason_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lbrf_qc_in_reason_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lbrf_activity_type_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents lbrf_shift_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents lbrf_qty_conversion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem24 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents gc_person_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_person_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Public WithEvents gc_person_edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_person_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Public WithEvents lbrf_mch_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents gc_reject_edit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_reject_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_downtime As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_downtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_downtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_reject As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_reject As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_reject As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_person As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents gc_downtime_edit As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_downtime_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem

End Class
