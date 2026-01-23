<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FInventoryStatus))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail1 = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gcc_detail1 = New DevExpress.XtraGrid.GridControl
        Me.gv_detail1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.sb_delete = New DevExpress.XtraEditors.SimpleButton
        Me.sb_add_save = New DevExpress.XtraEditors.SimpleButton
        Me.le_transaction = New DevExpress.XtraEditors.LookUpEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.sc_ce_is_overissue = New DevExpress.XtraEditors.CheckEdit
        Me.sc_ce_is_nettable = New DevExpress.XtraEditors.CheckEdit
        Me.sc_ce_is_avail = New DevExpress.XtraEditors.CheckEdit
        Me.sc_te_is_desc = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_is_code = New DevExpress.XtraEditors.TextEdit
        Me.sc_le_is_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail1.SuspendLayout()
        CType(Me.scc_detail1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail1.SuspendLayout()
        CType(Me.gcc_detail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_transaction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.sc_ce_is_overissue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_is_nettable.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_is_avail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_is_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_is_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_is_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(697, 239)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(703, 264)
        Me.scc_master.SplitterPosition = 25
        '
        'xtc_master
        '
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[Default]
        Me.xtc_master.Size = New System.Drawing.Size(699, 260)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(697, 239)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LayoutControl1)
        Me.Panel1.Size = New System.Drawing.Size(687, 179)
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
        Me.dp_detail.ID = New System.Guid("1fd0dd30-2a4a-4372-b569-7c9c6f06fcc6")
        Me.dp_detail.Location = New System.Drawing.Point(0, 264)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.Size = New System.Drawing.Size(703, 200)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(697, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_detail1
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_detail.Size = New System.Drawing.Size(697, 172)
        Me.xtc_detail.TabIndex = 0
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail1, Me.XtraTabPage2})
        '
        'xtp_detail1
        '
        Me.xtp_detail1.Controls.Add(Me.scc_detail1)
        Me.xtp_detail1.Name = "xtp_detail1"
        Me.xtp_detail1.Size = New System.Drawing.Size(695, 171)
        Me.xtp_detail1.Text = "XtraTabPage1"
        '
        'scc_detail1
        '
        Me.scc_detail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail1.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail1.Name = "scc_detail1"
        Me.scc_detail1.Panel1.Controls.Add(Me.gcc_detail1)
        Me.scc_detail1.Panel1.Text = "Panel1"
        Me.scc_detail1.Panel2.Controls.Add(Me.sb_delete)
        Me.scc_detail1.Panel2.Controls.Add(Me.sb_add_save)
        Me.scc_detail1.Panel2.Controls.Add(Me.le_transaction)
        Me.scc_detail1.Panel2.Controls.Add(Me.Label5)
        Me.scc_detail1.Panel2.Text = "Panel2"
        Me.scc_detail1.Size = New System.Drawing.Size(695, 171)
        Me.scc_detail1.SplitterPosition = 376
        Me.scc_detail1.TabIndex = 0
        Me.scc_detail1.Text = "SplitContainerControl1"
        '
        'gcc_detail1
        '
        Me.gcc_detail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcc_detail1.Location = New System.Drawing.Point(0, 0)
        Me.gcc_detail1.MainView = Me.gv_detail1
        Me.gcc_detail1.Name = "gcc_detail1"
        Me.gcc_detail1.Size = New System.Drawing.Size(372, 167)
        Me.gcc_detail1.TabIndex = 0
        Me.gcc_detail1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail1})
        '
        'gv_detail1
        '
        Me.gv_detail1.GridControl = Me.gcc_detail1
        Me.gv_detail1.Name = "gv_detail1"
        '
        'sb_delete
        '
        Me.sb_delete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_delete.Image = CType(resources.GetObject("sb_delete.Image"), System.Drawing.Image)
        Me.sb_delete.Location = New System.Drawing.Point(255, 99)
        Me.sb_delete.Name = "sb_delete"
        Me.sb_delete.Size = New System.Drawing.Size(36, 30)
        Me.sb_delete.TabIndex = 40
        '
        'sb_add_save
        '
        Me.sb_add_save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_add_save.Image = CType(resources.GetObject("sb_add_save.Image"), System.Drawing.Image)
        Me.sb_add_save.Location = New System.Drawing.Point(255, 63)
        Me.sb_add_save.Name = "sb_add_save"
        Me.sb_add_save.Size = New System.Drawing.Size(36, 30)
        Me.sb_add_save.TabIndex = 39
        '
        'le_transaction
        '
        Me.le_transaction.Location = New System.Drawing.Point(128, 37)
        Me.le_transaction.Name = "le_transaction"
        Me.le_transaction.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.le_transaction.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_transaction.Properties.PopupWidth = 500
        Me.le_transaction.Size = New System.Drawing.Size(178, 20)
        Me.le_transaction.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 13)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Transaction Name"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(695, 171)
        Me.XtraTabPage2.Text = "XtraTabPage2"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(687, 229)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.LayoutControl1.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.LayoutControl1.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.LayoutControl1.Controls.Add(Me.sc_ce_is_overissue)
        Me.LayoutControl1.Controls.Add(Me.sc_ce_is_nettable)
        Me.LayoutControl1.Controls.Add(Me.sc_ce_is_avail)
        Me.LayoutControl1.Controls.Add(Me.sc_te_is_desc)
        Me.LayoutControl1.Controls.Add(Me.sc_te_is_code)
        Me.LayoutControl1.Controls.Add(Me.sc_le_is_en_id)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(687, 179)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'sc_ce_is_overissue
        '
        Me.sc_ce_is_overissue.Location = New System.Drawing.Point(472, 100)
        Me.sc_ce_is_overissue.Name = "sc_ce_is_overissue"
        Me.sc_ce_is_overissue.Properties.Caption = "Is Overissue"
        Me.sc_ce_is_overissue.Size = New System.Drawing.Size(209, 19)
        Me.sc_ce_is_overissue.StyleController = Me.LayoutControl1
        Me.sc_ce_is_overissue.TabIndex = 8
        '
        'sc_ce_is_nettable
        '
        Me.sc_ce_is_nettable.Location = New System.Drawing.Point(246, 100)
        Me.sc_ce_is_nettable.Name = "sc_ce_is_nettable"
        Me.sc_ce_is_nettable.Properties.Caption = "Is Nettable"
        Me.sc_ce_is_nettable.Size = New System.Drawing.Size(215, 19)
        Me.sc_ce_is_nettable.StyleController = Me.LayoutControl1
        Me.sc_ce_is_nettable.TabIndex = 7
        '
        'sc_ce_is_avail
        '
        Me.sc_ce_is_avail.Location = New System.Drawing.Point(7, 100)
        Me.sc_ce_is_avail.Name = "sc_ce_is_avail"
        Me.sc_ce_is_avail.Properties.Caption = "Is Available"
        Me.sc_ce_is_avail.Size = New System.Drawing.Size(228, 19)
        Me.sc_ce_is_avail.StyleController = Me.LayoutControl1
        Me.sc_ce_is_avail.TabIndex = 6
        '
        'sc_te_is_desc
        '
        Me.sc_te_is_desc.Location = New System.Drawing.Point(65, 69)
        Me.sc_te_is_desc.Name = "sc_te_is_desc"
        Me.sc_te_is_desc.Size = New System.Drawing.Size(616, 20)
        Me.sc_te_is_desc.StyleController = Me.LayoutControl1
        Me.sc_te_is_desc.TabIndex = 5
        '
        'sc_te_is_code
        '
        Me.sc_te_is_code.Location = New System.Drawing.Point(65, 38)
        Me.sc_te_is_code.Name = "sc_te_is_code"
        Me.sc_te_is_code.Size = New System.Drawing.Size(616, 20)
        Me.sc_te_is_code.StyleController = Me.LayoutControl1
        Me.sc_te_is_code.TabIndex = 4
        '
        'sc_le_is_en_id
        '
        Me.sc_le_is_en_id.Location = New System.Drawing.Point(65, 7)
        Me.sc_le_is_en_id.Name = "sc_le_is_en_id"
        Me.sc_le_is_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_is_en_id.Size = New System.Drawing.Size(616, 20)
        Me.sc_le_is_en_id.StyleController = Me.LayoutControl1
        Me.sc_le_is_en_id.TabIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem4, Me.LayoutControlItem6, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(687, 179)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.sc_te_is_code
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(685, 31)
        Me.LayoutControlItem2.Text = "Code"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(53, 20)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.sc_te_is_desc
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 62)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(685, 31)
        Me.LayoutControlItem3.Text = "Description"
        Me.LayoutControlItem3.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(53, 20)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.sc_ce_is_nettable
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(239, 93)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(226, 84)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.sc_ce_is_avail
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 93)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(239, 84)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.sc_ce_is_overissue
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(465, 93)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(220, 84)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.sc_le_is_en_id
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(685, 31)
        Me.LayoutControlItem1.Text = "Entity ID"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(53, 20)
        '
        'FInventoryStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(703, 464)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FInventoryStatus"
        Me.Text = "Inventory Status"
        Me.Controls.SetChildIndex(Me.dp_detail, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail1.ResumeLayout(False)
        CType(Me.scc_detail1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail1.ResumeLayout(False)
        CType(Me.gcc_detail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_transaction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.sc_ce_is_overissue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_is_nettable.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_is_avail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_is_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_is_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_is_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gcc_detail1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sb_delete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_add_save As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents le_transaction As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents sc_ce_is_overissue As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_ce_is_nettable As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_ce_is_avail As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_te_is_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_te_is_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_le_is_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController

End Class
