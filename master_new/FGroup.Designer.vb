<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGroup
    Inherits master_new.MasterWIOne
    'Inherits master_new.MasterWITwo
    'MasterWITwo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGroup))
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.sc_txtkode_1 = New DevExpress.XtraEditors.TextEdit
        Me.sc_txtgroup = New DevExpress.XtraEditors.TextEdit
        Me.sc_cbdefault = New DevExpress.XtraEditors.ComboBoxEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.groupactive = New DevExpress.XtraEditors.CheckEdit
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl
        Me.btFindNext = New DevExpress.XtraEditors.SimpleButton
        Me.btSearch = New DevExpress.XtraEditors.SimpleButton
        Me.txtSearchMenu = New DevExpress.XtraEditors.TextEdit
        Me.bt_copy = New DevExpress.XtraEditors.SimpleButton
        Me.cb_group = New DevExpress.XtraEditors.LookUpEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.DockPanel1 = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label6 = New System.Windows.Forms.Label
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtkode_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtgroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_cbdefault.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupactive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_group.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel1.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(767, 378)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(767, 378)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(765, 357)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(765, 357)
        '
        'scc_detail
        '
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(767, 378)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelControl4)
        Me.Panel1.Controls.Add(Me.PanelControl3)
        Me.Panel1.Size = New System.Drawing.Size(745, 287)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(755, 347)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Kode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Group"
        '
        'sc_txtkode_1
        '
        Me.sc_txtkode_1.Location = New System.Drawing.Point(125, 5)
        Me.sc_txtkode_1.Name = "sc_txtkode_1"
        Me.sc_txtkode_1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtkode_1.Properties.MaxLength = 5
        Me.sc_txtkode_1.Size = New System.Drawing.Size(74, 20)
        Me.sc_txtkode_1.TabIndex = 1
        '
        'sc_txtgroup
        '
        Me.sc_txtgroup.Location = New System.Drawing.Point(125, 32)
        Me.sc_txtgroup.Name = "sc_txtgroup"
        Me.sc_txtgroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtgroup.Size = New System.Drawing.Size(169, 20)
        Me.sc_txtgroup.TabIndex = 3
        '
        'sc_cbdefault
        '
        Me.sc_cbdefault.EditValue = "<<Please Choose>>"
        Me.sc_cbdefault.Location = New System.Drawing.Point(125, 60)
        Me.sc_cbdefault.Name = "sc_cbdefault"
        Me.sc_cbdefault.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_cbdefault.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_cbdefault.Properties.Items.AddRange(New Object() {"<<Please Choose>>", "True", "False"})
        Me.sc_cbdefault.Size = New System.Drawing.Size(129, 20)
        Me.sc_cbdefault.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Group Default"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(28, 89)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Is Active"
        '
        'groupactive
        '
        Me.groupactive.Location = New System.Drawing.Point(125, 86)
        Me.groupactive.Name = "groupactive"
        Me.groupactive.Properties.Caption = ""
        Me.groupactive.Size = New System.Drawing.Size(75, 19)
        Me.groupactive.TabIndex = 24
        '
        'TreeList1
        '
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "menuid"
        Me.TreeList1.Location = New System.Drawing.Point(2, 2)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeList1.ParentFieldName = "menuid_parent"
        Me.TreeList1.SelectImageList = Me.imageList1
        Me.TreeList1.Size = New System.Drawing.Size(741, 158)
        Me.TreeList1.TabIndex = 27
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Magenta
        Me.imageList1.Images.SetKeyName(0, "")
        Me.imageList1.Images.SetKeyName(1, "")
        Me.imageList1.Images.SetKeyName(2, "")
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.btFindNext)
        Me.PanelControl3.Controls.Add(Me.btSearch)
        Me.PanelControl3.Controls.Add(Me.txtSearchMenu)
        Me.PanelControl3.Controls.Add(Me.bt_copy)
        Me.PanelControl3.Controls.Add(Me.cb_group)
        Me.PanelControl3.Controls.Add(Me.Label6)
        Me.PanelControl3.Controls.Add(Me.Label5)
        Me.PanelControl3.Controls.Add(Me.sc_txtkode_1)
        Me.PanelControl3.Controls.Add(Me.Label11)
        Me.PanelControl3.Controls.Add(Me.Label3)
        Me.PanelControl3.Controls.Add(Me.sc_txtgroup)
        Me.PanelControl3.Controls.Add(Me.groupactive)
        Me.PanelControl3.Controls.Add(Me.Label2)
        Me.PanelControl3.Controls.Add(Me.Label4)
        Me.PanelControl3.Controls.Add(Me.sc_cbdefault)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(745, 125)
        Me.PanelControl3.TabIndex = 28
        '
        'btFindNext
        '
        Me.btFindNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btFindNext.Location = New System.Drawing.Point(532, 33)
        Me.btFindNext.Name = "btFindNext"
        Me.btFindNext.Size = New System.Drawing.Size(31, 20)
        Me.btFindNext.TabIndex = 31
        Me.btFindNext.Text = "Next"
        '
        'btSearch
        '
        Me.btSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btSearch.Location = New System.Drawing.Point(497, 33)
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(31, 20)
        Me.btSearch.TabIndex = 30
        Me.btSearch.Text = "Find"
        '
        'txtSearchMenu
        '
        Me.txtSearchMenu.Location = New System.Drawing.Point(382, 33)
        Me.txtSearchMenu.Name = "txtSearchMenu"
        Me.txtSearchMenu.Size = New System.Drawing.Size(111, 20)
        Me.txtSearchMenu.TabIndex = 29
        '
        'bt_copy
        '
        Me.bt_copy.Location = New System.Drawing.Point(573, 6)
        Me.bt_copy.Name = "bt_copy"
        Me.bt_copy.Size = New System.Drawing.Size(91, 22)
        Me.bt_copy.TabIndex = 28
        Me.bt_copy.Text = "Copy"
        '
        'cb_group
        '
        Me.cb_group.Location = New System.Drawing.Point(382, 6)
        Me.cb_group.Name = "cb_group"
        Me.cb_group.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.cb_group.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_group.Properties.PopupWidth = 500
        Me.cb_group.Size = New System.Drawing.Size(178, 20)
        Me.cb_group.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(336, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Group"
        '
        'PanelControl4
        '
        Me.PanelControl4.Controls.Add(Me.TreeList1)
        Me.PanelControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl4.Location = New System.Drawing.Point(0, 125)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(745, 162)
        Me.PanelControl4.TabIndex = 29
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel1})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'DockPanel1
        '
        Me.DockPanel1.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.DockPanel1.ID = New System.Guid("d74b4d1e-9f7d-4a92-ab6e-8ed4eb13210b")
        Me.DockPanel1.Location = New System.Drawing.Point(0, 378)
        Me.DockPanel1.Name = "DockPanel1"
        Me.DockPanel1.OriginalSize = New System.Drawing.Size(200, 200)
        Me.DockPanel1.Size = New System.Drawing.Size(767, 200)
        Me.DockPanel1.Text = "DockPanel1"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.gc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(761, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(761, 172)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.EnableAppearanceEvenRow = True
        Me.gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(336, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Search"
        '
        'FGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(767, 578)
        Me.Controls.Add(Me.DockPanel1)
        Me.Name = "FGroup"
        Me.Text = "Group Configuration"
        Me.Controls.SetChildIndex(Me.DockPanel1, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtkode_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtgroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_cbdefault.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupactive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_group.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel1.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sc_txtgroup As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_txtkode_1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents sc_cbdefault As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents groupactive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel1 As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cb_group As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents bt_copy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btFindNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchMenu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
