<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRoyaltiRule
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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.royt_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.royt_pt_id = New DevExpress.XtraEditors.LookUpEdit
        Me.royt_royalti_amt = New DevExpress.XtraEditors.TextEdit
        Me.royt_remarks = New DevExpress.XtraEditors.TextEdit
        Me.royt_qty_royalti = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.royt_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.royt_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.royt_royalti_amt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.royt_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.royt_qty_royalti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
        Me.gc_master.TabIndex = 1
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
        Me.lci_master.Controls.Add(Me.royt_en_id)
        Me.lci_master.Controls.Add(Me.royt_pt_id)
        Me.lci_master.Controls.Add(Me.royt_royalti_amt)
        Me.lci_master.Controls.Add(Me.royt_remarks)
        Me.lci_master.Controls.Add(Me.royt_qty_royalti)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 285)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 2
        Me.lci_master.Text = "LayoutControl1"
        '
        'royt_en_id
        '
        Me.royt_en_id.Location = New System.Drawing.Point(72, 7)
        Me.royt_en_id.Name = "royt_en_id"
        Me.royt_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.royt_en_id.Size = New System.Drawing.Size(465, 20)
        Me.royt_en_id.StyleController = Me.lci_master
        Me.royt_en_id.TabIndex = 13
        '
        'royt_pt_id
        '
        Me.royt_pt_id.Location = New System.Drawing.Point(72, 38)
        Me.royt_pt_id.Name = "royt_pt_id"
        Me.royt_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.royt_pt_id.Size = New System.Drawing.Size(465, 20)
        Me.royt_pt_id.StyleController = Me.lci_master
        Me.royt_pt_id.TabIndex = 12
        '
        'royt_royalti_amt
        '
        Me.royt_royalti_amt.Location = New System.Drawing.Point(72, 100)
        Me.royt_royalti_amt.Name = "royt_royalti_amt"
        Me.royt_royalti_amt.Properties.Appearance.Options.UseTextOptions = True
        Me.royt_royalti_amt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.royt_royalti_amt.Properties.DisplayFormat.FormatString = "p"
        Me.royt_royalti_amt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.royt_royalti_amt.Properties.EditFormat.FormatString = "p"
        Me.royt_royalti_amt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.royt_royalti_amt.Properties.Mask.EditMask = "p"
        Me.royt_royalti_amt.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.royt_royalti_amt.Size = New System.Drawing.Size(465, 20)
        Me.royt_royalti_amt.StyleController = Me.lci_master
        Me.royt_royalti_amt.TabIndex = 9
        '
        'royt_remarks
        '
        Me.royt_remarks.Location = New System.Drawing.Point(72, 131)
        Me.royt_remarks.Name = "royt_remarks"
        Me.royt_remarks.Size = New System.Drawing.Size(465, 20)
        Me.royt_remarks.StyleController = Me.lci_master
        Me.royt_remarks.TabIndex = 8
        '
        'royt_qty_royalti
        '
        Me.royt_qty_royalti.Location = New System.Drawing.Point(72, 69)
        Me.royt_qty_royalti.Name = "royt_qty_royalti"
        Me.royt_qty_royalti.Properties.Appearance.Options.UseTextOptions = True
        Me.royt_qty_royalti.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.royt_qty_royalti.Properties.DisplayFormat.FormatString = "n"
        Me.royt_qty_royalti.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.royt_qty_royalti.Properties.EditFormat.FormatString = "n"
        Me.royt_qty_royalti.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.royt_qty_royalti.Properties.Mask.EditMask = "n"
        Me.royt_qty_royalti.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.royt_qty_royalti.Size = New System.Drawing.Size(465, 20)
        Me.royt_qty_royalti.StyleController = Me.lci_master
        Me.royt_qty_royalti.TabIndex = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1, Me.LayoutControlItem8, Me.LayoutControlItem2, Me.LayoutControlItem6, Me.LayoutControlItem5, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 285)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 155)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(541, 128)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.royt_pt_id
        Me.LayoutControlItem8.CustomizationFormText = "Gender"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(541, 31)
        Me.LayoutControlItem8.Text = "Part Number"
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.royt_qty_royalti
        Me.LayoutControlItem2.CustomizationFormText = "Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 62)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(541, 31)
        Me.LayoutControlItem2.Text = "Qty"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.royt_royalti_amt
        Me.LayoutControlItem6.CustomizationFormText = "Last Name"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 93)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(541, 31)
        Me.LayoutControlItem6.Text = "Prosentase"
        Me.LayoutControlItem6.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.royt_remarks
        Me.LayoutControlItem5.CustomizationFormText = "Middle Name"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 124)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(541, 31)
        Me.LayoutControlItem5.Text = "Remarks"
        Me.LayoutControlItem5.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.royt_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(541, 31)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(60, 13)
        '
        'FRoyaltiRule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FRoyaltiRule"
        Me.Text = "Royalti Rule"
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
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.royt_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.royt_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.royt_royalti_amt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.royt_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.royt_qty_royalti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents royt_pt_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents royt_royalti_amt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents royt_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents royt_qty_royalti As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents royt_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem

End Class
