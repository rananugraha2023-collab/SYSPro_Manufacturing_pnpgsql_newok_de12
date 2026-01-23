<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FEntityGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FEntityGroup))
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.eng_name = New DevExpress.XtraEditors.TextEdit
        Me.eng_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_en_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_en_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_group = New DevExpress.XtraTab.XtraTabPage
        Me.scc_group = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_en = New DevExpress.XtraGrid.GridControl
        Me.gv_en = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.sb_delete = New DevExpress.XtraEditors.SimpleButton
        Me.sb_add_save = New DevExpress.XtraEditors.SimpleButton
        Me.engd_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.eng_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.eng_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_en_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_en_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_group.SuspendLayout()
        CType(Me.scc_group, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_group.SuspendLayout()
        CType(Me.gc_en, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_en, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.engd_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(827, 212)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(829, 233)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(829, 233)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 412)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 352)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(817, 202)
        Me.gc_master.TabIndex = 1
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
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.lci_master.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.lci_master.Controls.Add(Me.eng_name)
        Me.lci_master.Controls.Add(Me.eng_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'eng_name
        '
        Me.eng_name.Location = New System.Drawing.Point(39, 38)
        Me.eng_name.Name = "eng_name"
        Me.eng_name.Size = New System.Drawing.Size(227, 20)
        Me.eng_name.StyleController = Me.lci_master
        Me.eng_name.TabIndex = 5
        '
        'eng_code
        '
        Me.eng_code.Location = New System.Drawing.Point(39, 7)
        Me.eng_code.Name = "eng_code"
        Me.eng_code.Size = New System.Drawing.Size(227, 20)
        Me.eng_code.StyleController = Me.lci_master
        Me.eng_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_en_code, Me.lci_en_desc, Me.EmptySpaceItem1, Me.EmptySpaceItem2, Me.EmptySpaceItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_en_code
        '
        Me.lci_en_code.Control = Me.eng_code
        Me.lci_en_code.CustomizationFormText = "Code"
        Me.lci_en_code.Location = New System.Drawing.Point(0, 0)
        Me.lci_en_code.Name = "lci_en_code"
        Me.lci_en_code.Size = New System.Drawing.Size(270, 31)
        Me.lci_en_code.Text = "Code"
        Me.lci_en_code.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_en_code.TextSize = New System.Drawing.Size(27, 13)
        '
        'lci_en_desc
        '
        Me.lci_en_desc.Control = Me.eng_name
        Me.lci_en_desc.CustomizationFormText = "Description"
        Me.lci_en_desc.Location = New System.Drawing.Point(0, 31)
        Me.lci_en_desc.Name = "lci_en_desc"
        Me.lci_en_desc.Size = New System.Drawing.Size(270, 31)
        Me.lci_en_desc.Text = "Name"
        Me.lci_en_desc.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_en_desc.TextSize = New System.Drawing.Size(27, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(270, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(271, 31)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(270, 31)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(271, 31)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 62)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(541, 288)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
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
        Me.dp_detail.ID = New System.Guid("ca94f48d-02d1-42ab-99a9-4549d4e7df94")
        Me.dp_detail.Location = New System.Drawing.Point(0, 233)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.Size = New System.Drawing.Size(829, 200)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(823, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_group
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_detail.Size = New System.Drawing.Size(823, 172)
        Me.xtc_detail.TabIndex = 1
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_group})
        '
        'xtp_group
        '
        Me.xtp_group.Controls.Add(Me.scc_group)
        Me.xtp_group.Name = "xtp_group"
        Me.xtp_group.Size = New System.Drawing.Size(821, 171)
        Me.xtp_group.Text = "Group"
        '
        'scc_group
        '
        Me.scc_group.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_group.Location = New System.Drawing.Point(0, 0)
        Me.scc_group.Name = "scc_group"
        Me.scc_group.Panel1.Controls.Add(Me.gc_en)
        Me.scc_group.Panel1.Text = "SplitContainerControl1_Panel1"
        Me.scc_group.Panel2.Controls.Add(Me.sb_delete)
        Me.scc_group.Panel2.Controls.Add(Me.sb_add_save)
        Me.scc_group.Panel2.Controls.Add(Me.engd_en_id)
        Me.scc_group.Panel2.Controls.Add(Me.Label5)
        Me.scc_group.Panel2.Text = "SplitContainerControl1_Panel2"
        Me.scc_group.Size = New System.Drawing.Size(821, 171)
        Me.scc_group.SplitterPosition = 491
        Me.scc_group.TabIndex = 0
        Me.scc_group.Text = "SplitContainerControl1"
        '
        'gc_en
        '
        Me.gc_en.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_en.Location = New System.Drawing.Point(0, 0)
        Me.gc_en.MainView = Me.gv_en
        Me.gc_en.Name = "gc_en"
        Me.gc_en.Size = New System.Drawing.Size(491, 171)
        Me.gc_en.TabIndex = 0
        Me.gc_en.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_en})
        '
        'gv_en
        '
        Me.gv_en.GridControl = Me.gc_en
        Me.gv_en.Name = "gv_en"
        '
        'sb_delete
        '
        Me.sb_delete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_delete.Image = CType(resources.GetObject("sb_delete.Image"), System.Drawing.Image)
        Me.sb_delete.Location = New System.Drawing.Point(191, 69)
        Me.sb_delete.Name = "sb_delete"
        Me.sb_delete.Size = New System.Drawing.Size(36, 30)
        Me.sb_delete.TabIndex = 36
        '
        'sb_add_save
        '
        Me.sb_add_save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_add_save.Image = CType(resources.GetObject("sb_add_save.Image"), System.Drawing.Image)
        Me.sb_add_save.Location = New System.Drawing.Point(191, 33)
        Me.sb_add_save.Name = "sb_add_save"
        Me.sb_add_save.Size = New System.Drawing.Size(36, 30)
        Me.sb_add_save.TabIndex = 35
        '
        'engd_en_id
        '
        Me.engd_en_id.Location = New System.Drawing.Point(49, 7)
        Me.engd_en_id.Name = "engd_en_id"
        Me.engd_en_id.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.engd_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.engd_en_id.Properties.PopupWidth = 500
        Me.engd_en_id.Size = New System.Drawing.Size(178, 20)
        Me.engd_en_id.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Entity"
        '
        'FEntityGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(829, 433)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FEntityGroup"
        Me.Text = "Entity Group"
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
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.eng_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.eng_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_en_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_en_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_group.ResumeLayout(False)
        CType(Me.scc_group, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_group.ResumeLayout(False)
        CType(Me.gc_en, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_en, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.engd_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents eng_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents eng_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_en_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_en_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_group As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_group As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_en As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_en As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_delete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_add_save As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents engd_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
