<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FItemStatus
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FItemStatus))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl()
        Me.xtp_detail1 = New DevExpress.XtraTab.XtraTabPage()
        Me.scc_detail1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.gcc_detail1 = New DevExpress.XtraGrid.GridControl()
        Me.gv_detail1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.sb_delete = New DevExpress.XtraEditors.SimpleButton()
        Me.sb_add_save = New DevExpress.XtraEditors.SimpleButton()
        Me.le_transaction = New DevExpress.XtraEditors.LookUpEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.gc_master = New DevExpress.XtraGrid.GridControl()
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.sc_te_its_desc = New DevExpress.XtraEditors.TextEdit()
        Me.sc_te_its_code = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.sc_te_its_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_its_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(701, 243)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(703, 264)
        Me.scc_master.SplitterPosition = 25
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(701, 243)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LayoutControl1)
        Me.Panel1.Size = New System.Drawing.Size(691, 198)
        '
        'xtc_master
        '
        Me.xtc_master.SelectedTabPage = Me.xtp_data
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[Default]
        Me.xtc_master.Size = New System.Drawing.Size(703, 264)
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
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 200)
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
        Me.gcc_detail1.Size = New System.Drawing.Size(376, 171)
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
        Me.gc_master.Size = New System.Drawing.Size(691, 233)
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
        Me.LayoutControl1.Controls.Add(Me.sc_te_its_desc)
        Me.LayoutControl1.Controls.Add(Me.sc_te_its_code)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(691, 198)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'sc_te_its_desc
        '
        Me.sc_te_its_desc.Location = New System.Drawing.Point(68, 36)
        Me.sc_te_its_desc.Name = "sc_te_its_desc"
        Me.sc_te_its_desc.Size = New System.Drawing.Size(611, 20)
        Me.sc_te_its_desc.StyleController = Me.LayoutControl1
        Me.sc_te_its_desc.TabIndex = 5
        '
        'sc_te_its_code
        '
        Me.sc_te_its_code.Location = New System.Drawing.Point(68, 12)
        Me.sc_te_its_code.Name = "sc_te_its_code"
        Me.sc_te_its_code.Size = New System.Drawing.Size(611, 20)
        Me.sc_te_its_code.StyleController = Me.LayoutControl1
        Me.sc_te_its_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(691, 198)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.sc_te_its_code
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(671, 24)
        Me.LayoutControlItem2.Text = "Code"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(53, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.sc_te_its_desc
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(671, 154)
        Me.LayoutControlItem3.Text = "Description"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(53, 13)
        '
        'FItemStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(703, 464)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FItemStatus"
        Me.Text = "Item Status"
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
        CType(Me.sc_te_its_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_its_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents sc_te_its_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_te_its_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController

End Class
