<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FEntity
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
        Me.en_warehouse_id = New DevExpress.XtraEditors.TextEdit
        Me.en_city = New DevExpress.XtraEditors.TextEdit
        Me.en_limit_account = New DevExpress.XtraEditors.CheckEdit
        Me.gc_edit = New DevExpress.XtraGrid.GridControl
        Me.gv_edit = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.en_parent = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_ce_en_active = New DevExpress.XtraEditors.CheckEdit
        Me.sc_te_en_desc = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_en_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_en_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_en_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_en_active = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerBottom = New DevExpress.XtraBars.Docking.AutoHideContainer
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
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
        CType(Me.en_warehouse_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_city.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_limit_account.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_parent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_en_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_en_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_en_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_en_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_en_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_en_active, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerBottom.SuspendLayout()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(574, 393)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(576, 414)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(574, 393)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(564, 348)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(576, 414)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(564, 383)
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
        Me.lci_master.Controls.Add(Me.en_warehouse_id)
        Me.lci_master.Controls.Add(Me.en_city)
        Me.lci_master.Controls.Add(Me.en_limit_account)
        Me.lci_master.Controls.Add(Me.gc_edit)
        Me.lci_master.Controls.Add(Me.en_parent)
        Me.lci_master.Controls.Add(Me.sc_ce_en_active)
        Me.lci_master.Controls.Add(Me.sc_te_en_desc)
        Me.lci_master.Controls.Add(Me.sc_te_en_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(564, 348)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'en_warehouse_id
        '
        Me.en_warehouse_id.Location = New System.Drawing.Point(356, 60)
        Me.en_warehouse_id.Name = "en_warehouse_id"
        Me.en_warehouse_id.Size = New System.Drawing.Size(196, 20)
        Me.en_warehouse_id.StyleController = Me.lci_master
        Me.en_warehouse_id.TabIndex = 14
        '
        'en_city
        '
        Me.en_city.Location = New System.Drawing.Point(356, 36)
        Me.en_city.Name = "en_city"
        Me.en_city.Size = New System.Drawing.Size(196, 20)
        Me.en_city.StyleController = Me.lci_master
        Me.en_city.TabIndex = 13
        '
        'en_limit_account
        '
        Me.en_limit_account.Location = New System.Drawing.Point(283, 84)
        Me.en_limit_account.Name = "en_limit_account"
        Me.en_limit_account.Properties.Caption = "Limit Account"
        Me.en_limit_account.Size = New System.Drawing.Size(269, 19)
        Me.en_limit_account.StyleController = Me.lci_master
        Me.en_limit_account.TabIndex = 12
        '
        'gc_edit
        '
        Me.gc_edit.Location = New System.Drawing.Point(12, 107)
        Me.gc_edit.MainView = Me.gv_edit
        Me.gc_edit.Name = "gc_edit"
        Me.gc_edit.Size = New System.Drawing.Size(540, 229)
        Me.gc_edit.TabIndex = 11
        Me.gc_edit.UseEmbeddedNavigator = True
        Me.gc_edit.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_edit})
        '
        'gv_edit
        '
        Me.gv_edit.GridControl = Me.gc_edit
        Me.gv_edit.Name = "gv_edit"
        Me.gv_edit.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_edit.OptionsView.ColumnAutoWidth = False
        Me.gv_edit.OptionsView.ShowGroupPanel = False
        '
        'en_parent
        '
        Me.en_parent.Location = New System.Drawing.Point(85, 60)
        Me.en_parent.Name = "en_parent"
        Me.en_parent.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.en_parent.Size = New System.Drawing.Size(194, 20)
        Me.en_parent.StyleController = Me.lci_master
        Me.en_parent.TabIndex = 7
        '
        'sc_ce_en_active
        '
        Me.sc_ce_en_active.Location = New System.Drawing.Point(12, 84)
        Me.sc_ce_en_active.Name = "sc_ce_en_active"
        Me.sc_ce_en_active.Properties.Caption = "Is Active"
        Me.sc_ce_en_active.Size = New System.Drawing.Size(267, 19)
        Me.sc_ce_en_active.StyleController = Me.lci_master
        Me.sc_ce_en_active.TabIndex = 6
        '
        'sc_te_en_desc
        '
        Me.sc_te_en_desc.Location = New System.Drawing.Point(85, 36)
        Me.sc_te_en_desc.Name = "sc_te_en_desc"
        Me.sc_te_en_desc.Size = New System.Drawing.Size(194, 20)
        Me.sc_te_en_desc.StyleController = Me.lci_master
        Me.sc_te_en_desc.TabIndex = 5
        '
        'sc_te_en_code
        '
        Me.sc_te_en_code.Location = New System.Drawing.Point(85, 12)
        Me.sc_te_en_code.Name = "sc_te_en_code"
        Me.sc_te_en_code.Properties.Appearance.Options.UseTextOptions = True
        Me.sc_te_en_code.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.sc_te_en_code.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sc_te_en_code.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sc_te_en_code.Properties.Mask.EditMask = "n0"
        Me.sc_te_en_code.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.sc_te_en_code.Properties.MaxLength = 2
        Me.sc_te_en_code.Size = New System.Drawing.Size(194, 20)
        Me.sc_te_en_code.StyleController = Me.lci_master
        Me.sc_te_en_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_en_code, Me.lci_en_desc, Me.lci_en_active, Me.EmptySpaceItem1, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(564, 348)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_en_code
        '
        Me.lci_en_code.Control = Me.sc_te_en_code
        Me.lci_en_code.CustomizationFormText = "Code"
        Me.lci_en_code.Location = New System.Drawing.Point(0, 0)
        Me.lci_en_code.Name = "lci_en_code"
        Me.lci_en_code.Size = New System.Drawing.Size(271, 24)
        Me.lci_en_code.Text = "Code"
        Me.lci_en_code.TextSize = New System.Drawing.Size(69, 13)
        '
        'lci_en_desc
        '
        Me.lci_en_desc.Control = Me.sc_te_en_desc
        Me.lci_en_desc.CustomizationFormText = "Description"
        Me.lci_en_desc.Location = New System.Drawing.Point(0, 24)
        Me.lci_en_desc.Name = "lci_en_desc"
        Me.lci_en_desc.Size = New System.Drawing.Size(271, 24)
        Me.lci_en_desc.Text = "Description"
        Me.lci_en_desc.TextSize = New System.Drawing.Size(69, 13)
        '
        'lci_en_active
        '
        Me.lci_en_active.Control = Me.sc_ce_en_active
        Me.lci_en_active.CustomizationFormText = "Is Active"
        Me.lci_en_active.Location = New System.Drawing.Point(0, 72)
        Me.lci_en_active.Name = "lci_en_active"
        Me.lci_en_active.Size = New System.Drawing.Size(271, 23)
        Me.lci_en_active.Text = "Is Active"
        Me.lci_en_active.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_en_active.TextToControlDistance = 0
        Me.lci_en_active.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(271, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(273, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.en_parent
        Me.LayoutControlItem1.CustomizationFormText = "Parent"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(271, 24)
        Me.LayoutControlItem1.Text = "Parent"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.gc_edit
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 95)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(544, 233)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.en_limit_account
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(271, 72)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(273, 23)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.en_city
        Me.LayoutControlItem4.CustomizationFormText = "City"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(271, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(273, 24)
        Me.LayoutControlItem4.Text = "City"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(69, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.en_warehouse_id
        Me.LayoutControlItem5.CustomizationFormText = "Branch ID"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(271, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(273, 24)
        Me.LayoutControlItem5.Text = "Warehouse ID"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(69, 13)
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
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerBottom
        '
        Me.hideContainerBottom.Controls.Add(Me.DockPanel1)
        Me.hideContainerBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.hideContainerBottom.Location = New System.Drawing.Point(0, 414)
        Me.hideContainerBottom.Name = "hideContainerBottom"
        Me.hideContainerBottom.Size = New System.Drawing.Size(576, 19)
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("be28187d-fe9e-44d4-ad41-8532704740a9")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(576, 200)
        Me.DockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.SavedIndex = 0
        Me.DockPanel1.Size = New System.Drawing.Size(576, 200)
        Me.DockPanel1.Text = "Data Detail"
        Me.DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.gc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(570, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(570, 172)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'FEntity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(576, 433)
        Me.Controls.Add(Me.hideContainerBottom)
        Me.Name = "FEntity"
        Me.Text = "Entity"
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
        CType(Me.en_warehouse_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_city.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_limit_account.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_edit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_parent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_en_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_en_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_en_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_en_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_en_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_en_active, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerBottom.ResumeLayout(False)
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents sc_ce_en_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_te_en_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_te_en_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_en_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_en_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_en_active As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents en_parent As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents hideContainerBottom As DevExpress.XtraBars.Docking.AutoHideContainer
    Public WithEvents gc_edit As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_edit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents en_limit_account As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents en_city As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents en_warehouse_id As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem

End Class
