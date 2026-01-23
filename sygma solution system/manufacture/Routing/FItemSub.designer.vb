<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FItemSub
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
        Me.pts_desc = New DevExpress.XtraEditors.TextEdit
        Me.pts_qty = New DevExpress.XtraEditors.TextEdit
        Me.pts_pt_sub_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pts_pt_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pts_active = New DevExpress.XtraEditors.CheckEdit
        Me.pts_ps_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pts_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
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
        CType(Me.pts_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_qty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_pt_sub_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_ps_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(553, 412)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
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
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
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
        Me.lci_master.Controls.Add(Me.pts_desc)
        Me.lci_master.Controls.Add(Me.pts_qty)
        Me.lci_master.Controls.Add(Me.pts_pt_sub_id)
        Me.lci_master.Controls.Add(Me.pts_pt_id)
        Me.lci_master.Controls.Add(Me.pts_active)
        Me.lci_master.Controls.Add(Me.pts_ps_id)
        Me.lci_master.Controls.Add(Me.pts_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'pts_desc
        '
        Me.pts_desc.Location = New System.Drawing.Point(101, 108)
        Me.pts_desc.Name = "pts_desc"
        Me.pts_desc.Size = New System.Drawing.Size(430, 20)
        Me.pts_desc.StyleController = Me.lci_master
        Me.pts_desc.TabIndex = 9
        '
        'pts_qty
        '
        Me.pts_qty.Location = New System.Drawing.Point(101, 84)
        Me.pts_qty.Name = "pts_qty"
        Me.pts_qty.Properties.DisplayFormat.FormatString = "n"
        Me.pts_qty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pts_qty.Properties.EditFormat.FormatString = "n"
        Me.pts_qty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.pts_qty.Size = New System.Drawing.Size(68, 20)
        Me.pts_qty.StyleController = Me.lci_master
        Me.pts_qty.TabIndex = 8
        '
        'pts_pt_sub_id
        '
        Me.pts_pt_sub_id.Location = New System.Drawing.Point(385, 60)
        Me.pts_pt_sub_id.Name = "pts_pt_sub_id"
        Me.pts_pt_sub_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pts_pt_sub_id.Size = New System.Drawing.Size(146, 20)
        Me.pts_pt_sub_id.StyleController = Me.lci_master
        Me.pts_pt_sub_id.TabIndex = 7
        '
        'pts_pt_id
        '
        Me.pts_pt_id.Location = New System.Drawing.Point(101, 60)
        Me.pts_pt_id.Name = "pts_pt_id"
        Me.pts_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pts_pt_id.Size = New System.Drawing.Size(191, 20)
        Me.pts_pt_id.StyleController = Me.lci_master
        Me.pts_pt_id.TabIndex = 6
        '
        'pts_active
        '
        Me.pts_active.Location = New System.Drawing.Point(12, 132)
        Me.pts_active.Name = "pts_active"
        Me.pts_active.Properties.Caption = "Is Active"
        Me.pts_active.Size = New System.Drawing.Size(519, 19)
        Me.pts_active.StyleController = Me.lci_master
        Me.pts_active.TabIndex = 10
        '
        'pts_ps_id
        '
        Me.pts_ps_id.Location = New System.Drawing.Point(101, 36)
        Me.pts_ps_id.Name = "pts_ps_id"
        Me.pts_ps_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pts_ps_id.Size = New System.Drawing.Size(430, 20)
        Me.pts_ps_id.StyleController = Me.lci_master
        Me.pts_ps_id.TabIndex = 5
        '
        'pts_en_id
        '
        Me.pts_en_id.Location = New System.Drawing.Point(101, 12)
        Me.pts_en_id.Name = "pts_en_id"
        Me.pts_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pts_en_id.Size = New System.Drawing.Size(168, 20)
        Me.pts_en_id.StyleController = Me.lci_master
        Me.pts_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem4, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.EmptySpaceItem2, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.pts_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pts_ps_id
        Me.LayoutControlItem2.CustomizationFormText = "Product Structure"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Product Structure"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.pts_pt_id
        Me.LayoutControlItem3.CustomizationFormText = "Part Number"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(284, 24)
        Me.LayoutControlItem3.Text = "Part Number"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.pts_qty
        Me.LayoutControlItem5.CustomizationFormText = "Quantity"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(161, 24)
        Me.LayoutControlItem5.Text = "Quantity"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.pts_pt_sub_id
        Me.LayoutControlItem4.CustomizationFormText = "Part Substitution"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(284, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(239, 24)
        Me.LayoutControlItem4.Text = "Part Substitution"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.pts_desc
        Me.LayoutControlItem6.CustomizationFormText = "Description"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem6.Text = "Description"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(85, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.pts_active
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(523, 212)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(261, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(262, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(161, 72)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(362, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FItemSub
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FItemSub"
        Me.Text = "Item Substitution"
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
        CType(Me.pts_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_qty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_pt_sub_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_ps_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents pts_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_ps_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_pt_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_pt_sub_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents pts_qty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
