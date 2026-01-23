<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FOrganizationStructure
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
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.sc_ce_orgs_active = New DevExpress.XtraEditors.CheckEdit
        Me.sc_cbe_orgs_type = New DevExpress.XtraEditors.ComboBoxEdit
        Me.sc_te_orgs_desc = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_orgs_code = New DevExpress.XtraEditors.TextEdit
        Me.sc_le_orgs_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_orgs_en_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgs_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgs_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgs_type = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgs_active = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_detail1 = New DevExpress.XtraTab.XtraTabPage
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.sc_ce_orgs_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_cbe_orgs_type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_orgs_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_orgs_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_orgs_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgs_en_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgs_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgs_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgs_type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgs_active, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_detail1.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(899, 208)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(905, 233)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(901, 229)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(899, 208)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(889, 148)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(889, 198)
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
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.lci_master.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.lci_master.Controls.Add(Me.sc_ce_orgs_active)
        Me.lci_master.Controls.Add(Me.sc_cbe_orgs_type)
        Me.lci_master.Controls.Add(Me.sc_te_orgs_desc)
        Me.lci_master.Controls.Add(Me.sc_te_orgs_code)
        Me.lci_master.Controls.Add(Me.sc_le_orgs_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(889, 148)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'sc_ce_orgs_active
        '
        Me.sc_ce_orgs_active.Location = New System.Drawing.Point(7, 131)
        Me.sc_ce_orgs_active.Name = "sc_ce_orgs_active"
        Me.sc_ce_orgs_active.Properties.Caption = "Is Active"
        Me.sc_ce_orgs_active.Size = New System.Drawing.Size(876, 19)
        Me.sc_ce_orgs_active.StyleController = Me.lci_master
        Me.sc_ce_orgs_active.TabIndex = 8
        '
        'sc_cbe_orgs_type
        '
        Me.sc_cbe_orgs_type.Location = New System.Drawing.Point(65, 100)
        Me.sc_cbe_orgs_type.Name = "sc_cbe_orgs_type"
        Me.sc_cbe_orgs_type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_cbe_orgs_type.Properties.Items.AddRange(New Object() {"O", "A"})
        Me.sc_cbe_orgs_type.Size = New System.Drawing.Size(818, 20)
        Me.sc_cbe_orgs_type.StyleController = Me.lci_master
        Me.sc_cbe_orgs_type.TabIndex = 7
        '
        'sc_te_orgs_desc
        '
        Me.sc_te_orgs_desc.Location = New System.Drawing.Point(65, 69)
        Me.sc_te_orgs_desc.Name = "sc_te_orgs_desc"
        Me.sc_te_orgs_desc.Size = New System.Drawing.Size(818, 20)
        Me.sc_te_orgs_desc.StyleController = Me.lci_master
        Me.sc_te_orgs_desc.TabIndex = 6
        '
        'sc_te_orgs_code
        '
        Me.sc_te_orgs_code.Location = New System.Drawing.Point(65, 38)
        Me.sc_te_orgs_code.Name = "sc_te_orgs_code"
        Me.sc_te_orgs_code.Size = New System.Drawing.Size(818, 20)
        Me.sc_te_orgs_code.StyleController = Me.lci_master
        Me.sc_te_orgs_code.TabIndex = 5
        '
        'sc_le_orgs_en_id
        '
        Me.sc_le_orgs_en_id.Location = New System.Drawing.Point(65, 7)
        Me.sc_le_orgs_en_id.Name = "sc_le_orgs_en_id"
        Me.sc_le_orgs_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_orgs_en_id.Size = New System.Drawing.Size(818, 20)
        Me.sc_le_orgs_en_id.StyleController = Me.lci_master
        Me.sc_le_orgs_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_orgs_en_id, Me.lci_orgs_code, Me.lci_orgs_desc, Me.lci_orgs_type, Me.lci_orgs_active, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(889, 166)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_orgs_en_id
        '
        Me.lci_orgs_en_id.Control = Me.sc_le_orgs_en_id
        Me.lci_orgs_en_id.CustomizationFormText = "Entity"
        Me.lci_orgs_en_id.Location = New System.Drawing.Point(0, 0)
        Me.lci_orgs_en_id.Name = "lci_orgs_en_id"
        Me.lci_orgs_en_id.Size = New System.Drawing.Size(887, 31)
        Me.lci_orgs_en_id.Text = "Entity"
        Me.lci_orgs_en_id.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgs_en_id.TextSize = New System.Drawing.Size(53, 20)
        '
        'lci_orgs_code
        '
        Me.lci_orgs_code.Control = Me.sc_te_orgs_code
        Me.lci_orgs_code.CustomizationFormText = "Code"
        Me.lci_orgs_code.Location = New System.Drawing.Point(0, 31)
        Me.lci_orgs_code.Name = "lci_orgs_code"
        Me.lci_orgs_code.Size = New System.Drawing.Size(887, 31)
        Me.lci_orgs_code.Text = "Code"
        Me.lci_orgs_code.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgs_code.TextSize = New System.Drawing.Size(53, 20)
        '
        'lci_orgs_desc
        '
        Me.lci_orgs_desc.Control = Me.sc_te_orgs_desc
        Me.lci_orgs_desc.CustomizationFormText = "Description"
        Me.lci_orgs_desc.Location = New System.Drawing.Point(0, 62)
        Me.lci_orgs_desc.Name = "lci_orgs_desc"
        Me.lci_orgs_desc.Size = New System.Drawing.Size(887, 31)
        Me.lci_orgs_desc.Text = "Description"
        Me.lci_orgs_desc.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgs_desc.TextSize = New System.Drawing.Size(53, 20)
        '
        'lci_orgs_type
        '
        Me.lci_orgs_type.Control = Me.sc_cbe_orgs_type
        Me.lci_orgs_type.CustomizationFormText = "Type"
        Me.lci_orgs_type.Location = New System.Drawing.Point(0, 93)
        Me.lci_orgs_type.Name = "lci_orgs_type"
        Me.lci_orgs_type.Size = New System.Drawing.Size(887, 31)
        Me.lci_orgs_type.Text = "Type"
        Me.lci_orgs_type.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgs_type.TextSize = New System.Drawing.Size(53, 20)
        '
        'lci_orgs_active
        '
        Me.lci_orgs_active.Control = Me.sc_ce_orgs_active
        Me.lci_orgs_active.CustomizationFormText = "lci_orgs_active"
        Me.lci_orgs_active.Location = New System.Drawing.Point(0, 124)
        Me.lci_orgs_active.Name = "lci_orgs_active"
        Me.lci_orgs_active.Size = New System.Drawing.Size(887, 30)
        Me.lci_orgs_active.Text = "lci_orgs_active"
        Me.lci_orgs_active.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgs_active.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_orgs_active.TextToControlDistance = 0
        Me.lci_orgs_active.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 154)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(887, 10)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
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
        Me.dp_detail.ID = New System.Guid("90923fcb-e686-4681-9a0b-4ef1d09cbb15")
        Me.dp_detail.Location = New System.Drawing.Point(0, 233)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.Size = New System.Drawing.Size(905, 200)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(899, 172)
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
        Me.xtc_detail.Size = New System.Drawing.Size(899, 172)
        Me.xtc_detail.TabIndex = 0
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_detail1})
        '
        'xtp_detail1
        '
        Me.xtp_detail1.Controls.Add(Me.SplitContainerControl1)
        Me.xtp_detail1.Name = "xtp_detail1"
        Me.xtp_detail1.Size = New System.Drawing.Size(897, 151)
        Me.xtp_detail1.Text = "Organization Struktur Detail"
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.gc_detail)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.SplitContainerControl1.Size = New System.Drawing.Size(897, 151)
        Me.SplitContainerControl1.SplitterPosition = 523
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(893, 147)
        Me.gc_detail.TabIndex = 0
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        '
        'FOrganizationStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(905, 433)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FOrganizationStructure"
        Me.Text = "Organization Structure"
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
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.sc_ce_orgs_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_cbe_orgs_type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_orgs_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_orgs_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_orgs_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgs_en_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgs_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgs_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgs_type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgs_active, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_detail1.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents sc_le_orgs_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lci_orgs_en_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents sc_ce_orgs_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_cbe_orgs_type As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents sc_te_orgs_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_te_orgs_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_orgs_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_orgs_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_orgs_type As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_orgs_active As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_detail1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView

End Class
