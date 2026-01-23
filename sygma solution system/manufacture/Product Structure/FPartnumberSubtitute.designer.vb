<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartnumberSubtitute
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
        Me.pts_qty = New DevExpress.XtraEditors.TextEdit
        Me.pts_pt_sub_id = New DevExpress.XtraEditors.ButtonEdit
        Me.pts_pt_id = New DevExpress.XtraEditors.ButtonEdit
        Me.pts_ps_id = New DevExpress.XtraEditors.ButtonEdit
        Me.pts_active = New DevExpress.XtraEditors.CheckEdit
        Me.pts_desc = New DevExpress.XtraEditors.TextEdit
        Me.pts_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
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
        CType(Me.pts_qty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_pt_sub_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_ps_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pts_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(779, 406)
        '
        'scc_master
        '
        Me.scc_master.Size = New System.Drawing.Size(781, 433)
        Me.scc_master.SplitterPosition = 0
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(781, 427)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(779, 406)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(769, 346)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(769, 396)
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
        Me.lci_master.Controls.Add(Me.pts_qty)
        Me.lci_master.Controls.Add(Me.pts_pt_sub_id)
        Me.lci_master.Controls.Add(Me.pts_pt_id)
        Me.lci_master.Controls.Add(Me.pts_ps_id)
        Me.lci_master.Controls.Add(Me.pts_active)
        Me.lci_master.Controls.Add(Me.pts_desc)
        Me.lci_master.Controls.Add(Me.pts_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(769, 346)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'pts_qty
        '
        Me.pts_qty.Location = New System.Drawing.Point(497, 60)
        Me.pts_qty.Name = "pts_qty"
        Me.pts_qty.Properties.Mask.EditMask = "n"
        Me.pts_qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.pts_qty.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.pts_qty.Size = New System.Drawing.Size(260, 20)
        Me.pts_qty.StyleController = Me.lci_master
        Me.pts_qty.TabIndex = 13
        '
        'pts_pt_sub_id
        '
        Me.pts_pt_sub_id.Location = New System.Drawing.Point(123, 60)
        Me.pts_pt_sub_id.Name = "pts_pt_sub_id"
        Me.pts_pt_sub_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pts_pt_sub_id.Properties.ReadOnly = True
        Me.pts_pt_sub_id.Size = New System.Drawing.Size(259, 20)
        Me.pts_pt_sub_id.StyleController = Me.lci_master
        Me.pts_pt_sub_id.TabIndex = 12
        '
        'pts_pt_id
        '
        Me.pts_pt_id.Location = New System.Drawing.Point(497, 36)
        Me.pts_pt_id.Name = "pts_pt_id"
        Me.pts_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pts_pt_id.Properties.ReadOnly = True
        Me.pts_pt_id.Size = New System.Drawing.Size(260, 20)
        Me.pts_pt_id.StyleController = Me.lci_master
        Me.pts_pt_id.TabIndex = 11
        '
        'pts_ps_id
        '
        Me.pts_ps_id.Location = New System.Drawing.Point(123, 36)
        Me.pts_ps_id.Name = "pts_ps_id"
        Me.pts_ps_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pts_ps_id.Properties.ReadOnly = True
        Me.pts_ps_id.Size = New System.Drawing.Size(259, 20)
        Me.pts_ps_id.StyleController = Me.lci_master
        Me.pts_ps_id.TabIndex = 10
        '
        'pts_active
        '
        Me.pts_active.Location = New System.Drawing.Point(12, 108)
        Me.pts_active.Name = "pts_active"
        Me.pts_active.Properties.Caption = "Is Active"
        Me.pts_active.Size = New System.Drawing.Size(745, 19)
        Me.pts_active.StyleController = Me.lci_master
        Me.pts_active.TabIndex = 8
        '
        'pts_desc
        '
        Me.pts_desc.Location = New System.Drawing.Point(123, 84)
        Me.pts_desc.Name = "pts_desc"
        Me.pts_desc.Size = New System.Drawing.Size(634, 20)
        Me.pts_desc.StyleController = Me.lci_master
        Me.pts_desc.TabIndex = 7
        '
        'pts_en_id
        '
        Me.pts_en_id.Location = New System.Drawing.Point(123, 12)
        Me.pts_en_id.Name = "pts_en_id"
        Me.pts_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pts_en_id.Size = New System.Drawing.Size(259, 20)
        Me.pts_en_id.StyleController = Me.lci_master
        Me.pts_en_id.TabIndex = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.EmptySpaceItem2, Me.EmptySpaceItem3, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem10, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(769, 346)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pts_en_id
        Me.LayoutControlItem2.CustomizationFormText = "Entity ID"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(374, 24)
        Me.LayoutControlItem2.Text = "Entity"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(107, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.pts_desc
        Me.LayoutControlItem4.CustomizationFormText = "Reference"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(749, 24)
        Me.LayoutControlItem4.Text = "Description"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(107, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.pts_active
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(749, 23)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 119)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(749, 195)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(0, 314)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(749, 12)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.pts_ps_id
        Me.LayoutControlItem7.CustomizationFormText = "Product Structure"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(374, 24)
        Me.LayoutControlItem7.Text = "Product Structure"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(107, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.pts_pt_id
        Me.LayoutControlItem8.CustomizationFormText = "Part Number"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(374, 24)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(375, 24)
        Me.LayoutControlItem8.Text = "Part Number"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(107, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.pts_pt_sub_id
        Me.LayoutControlItem9.CustomizationFormText = "Part Number Subtitute"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(374, 24)
        Me.LayoutControlItem9.Text = "Part Number Subtitute"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(107, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.pts_qty
        Me.LayoutControlItem10.CustomizationFormText = "Qty"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(374, 48)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(375, 24)
        Me.LayoutControlItem10.Text = "Qty"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(107, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(374, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(375, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FPartnumberSubtitute
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(781, 433)
        Me.Name = "FPartnumberSubtitute"
        Me.Text = "Part Number Subtitute"
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
        CType(Me.pts_qty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_pt_sub_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_ps_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pts_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pts_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents pts_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents pts_qty As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents pts_pt_sub_id As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents pts_pt_id As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents pts_ps_id As DevExpress.XtraEditors.ButtonEdit

End Class
