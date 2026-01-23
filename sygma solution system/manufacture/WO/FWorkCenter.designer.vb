<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FWorkCenter
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
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.wc_name = New DevExpress.XtraEditors.TextEdit
        Me.wc_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.wc_dpt = New DevExpress.XtraEditors.LookUpEdit
        Me.wc_desc = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.TabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup8 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.Lci_entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_wc_dpt = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.lci_wc_name = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_wc_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.wc_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wc_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wc_dpt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_wc_dpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_wc_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_wc_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(736, 727)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(738, 748)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(738, 748)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(736, 727)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(726, 667)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(726, 717)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master, Me.GridView2})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_master
        Me.GridView2.Name = "GridView2"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.wc_name)
        Me.lci_master.Controls.Add(Me.wc_en_id)
        Me.lci_master.Controls.Add(Me.wc_dpt)
        Me.lci_master.Controls.Add(Me.wc_desc)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(726, 667)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'wc_name
        '
        Me.wc_name.Location = New System.Drawing.Point(103, 82)
        Me.wc_name.Name = "wc_name"
        Me.wc_name.Size = New System.Drawing.Size(259, 20)
        Me.wc_name.StyleController = Me.lci_master
        Me.wc_name.TabIndex = 11
        '
        'wc_en_id
        '
        Me.wc_en_id.Location = New System.Drawing.Point(103, 58)
        Me.wc_en_id.Name = "wc_en_id"
        Me.wc_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wc_en_id.Size = New System.Drawing.Size(259, 20)
        Me.wc_en_id.StyleController = Me.lci_master
        Me.wc_en_id.TabIndex = 9
        '
        'wc_dpt
        '
        Me.wc_dpt.Location = New System.Drawing.Point(103, 130)
        Me.wc_dpt.Name = "wc_dpt"
        Me.wc_dpt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.wc_dpt.Size = New System.Drawing.Size(259, 20)
        Me.wc_dpt.StyleController = Me.lci_master
        Me.wc_dpt.TabIndex = 10
        '
        'wc_desc
        '
        Me.wc_desc.Location = New System.Drawing.Point(103, 106)
        Me.wc_desc.Name = "wc_desc"
        Me.wc_desc.Size = New System.Drawing.Size(587, 20)
        Me.wc_desc.StyleController = Me.lci_master
        Me.wc_desc.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem3, Me.TabbedControlGroup1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(726, 667)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 166)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(706, 481)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'TabbedControlGroup1
        '
        Me.TabbedControlGroup1.CustomizationFormText = "TabbedControlGroup1"
        Me.TabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.TabbedControlGroup1.Name = "TabbedControlGroup1"
        Me.TabbedControlGroup1.SelectedTabPage = Me.LayoutControlGroup6
        Me.TabbedControlGroup1.SelectedTabPageIndex = 0
        Me.TabbedControlGroup1.Size = New System.Drawing.Size(706, 166)
        Me.TabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6})
        Me.TabbedControlGroup1.Text = "TabbedControlGroup1"
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup8})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(499, 120)
        Me.LayoutControlGroup6.Text = "Work Center Data"
        '
        'LayoutControlGroup8
        '
        Me.LayoutControlGroup8.CustomizationFormText = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.Lci_entity, Me.lci_wc_dpt, Me.EmptySpaceItem6, Me.EmptySpaceItem8, Me.lci_wc_name, Me.lci_wc_desc, Me.EmptySpaceItem7})
        Me.LayoutControlGroup8.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup8.Name = "LayoutControlGroup8"
        Me.LayoutControlGroup8.Size = New System.Drawing.Size(682, 120)
        Me.LayoutControlGroup8.Text = "LayoutControlGroup8"
        Me.LayoutControlGroup8.TextVisible = False
        '
        'Lci_entity
        '
        Me.Lci_entity.Control = Me.wc_en_id
        Me.Lci_entity.CustomizationFormText = "Entity"
        Me.Lci_entity.Location = New System.Drawing.Point(0, 0)
        Me.Lci_entity.Name = "Lci_entity"
        Me.Lci_entity.Size = New System.Drawing.Size(330, 24)
        Me.Lci_entity.Text = "Entity"
        Me.Lci_entity.TextSize = New System.Drawing.Size(63, 13)
        '
        'lci_wc_dpt
        '
        Me.lci_wc_dpt.Control = Me.wc_dpt
        Me.lci_wc_dpt.CustomizationFormText = "Unit Measure"
        Me.lci_wc_dpt.Location = New System.Drawing.Point(0, 72)
        Me.lci_wc_dpt.Name = "lci_wc_dpt"
        Me.lci_wc_dpt.Size = New System.Drawing.Size(330, 24)
        Me.lci_wc_dpt.Text = "Departement"
        Me.lci_wc_dpt.TextSize = New System.Drawing.Size(63, 13)
        '
        'EmptySpaceItem6
        '
        Me.EmptySpaceItem6.CustomizationFormText = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Location = New System.Drawing.Point(330, 0)
        Me.EmptySpaceItem6.Name = "EmptySpaceItem6"
        Me.EmptySpaceItem6.Size = New System.Drawing.Size(328, 24)
        Me.EmptySpaceItem6.Text = "EmptySpaceItem6"
        Me.EmptySpaceItem6.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem8
        '
        Me.EmptySpaceItem8.CustomizationFormText = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Location = New System.Drawing.Point(330, 72)
        Me.EmptySpaceItem8.Name = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Size = New System.Drawing.Size(328, 24)
        Me.EmptySpaceItem8.Text = "EmptySpaceItem8"
        Me.EmptySpaceItem8.TextSize = New System.Drawing.Size(0, 0)
        '
        'lci_wc_name
        '
        Me.lci_wc_name.Control = Me.wc_name
        Me.lci_wc_name.CustomizationFormText = "Name"
        Me.lci_wc_name.Location = New System.Drawing.Point(0, 24)
        Me.lci_wc_name.Name = "lci_wc_name"
        Me.lci_wc_name.Size = New System.Drawing.Size(330, 24)
        Me.lci_wc_name.Text = "Name"
        Me.lci_wc_name.TextSize = New System.Drawing.Size(63, 13)
        '
        'lci_wc_desc
        '
        Me.lci_wc_desc.Control = Me.wc_desc
        Me.lci_wc_desc.CustomizationFormText = "Description"
        Me.lci_wc_desc.Location = New System.Drawing.Point(0, 48)
        Me.lci_wc_desc.Name = "lci_wc_desc"
        Me.lci_wc_desc.Size = New System.Drawing.Size(658, 24)
        Me.lci_wc_desc.Text = "Description"
        Me.lci_wc_desc.TextSize = New System.Drawing.Size(63, 13)
        '
        'EmptySpaceItem7
        '
        Me.EmptySpaceItem7.CustomizationFormText = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Location = New System.Drawing.Point(330, 24)
        Me.EmptySpaceItem7.Name = "EmptySpaceItem7"
        Me.EmptySpaceItem7.Size = New System.Drawing.Size(328, 24)
        Me.EmptySpaceItem7.Text = "EmptySpaceItem7"
        Me.EmptySpaceItem7.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(328, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(330, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FWorkCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(738, 748)
        Me.Name = "FWorkCenter"
        Me.Text = "Work Center"
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.wc_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wc_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wc_dpt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabbedControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_wc_dpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_wc_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_wc_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents wc_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents wc_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents wc_dpt As DevExpress.XtraEditors.LookUpEdit
    Public WithEvents lci_wc_dpt As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents TabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_wc_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Lci_entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup8 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents wc_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_wc_name As DevExpress.XtraLayout.LayoutControlItem

End Class
