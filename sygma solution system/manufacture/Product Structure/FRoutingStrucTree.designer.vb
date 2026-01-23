<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRoutingStrucTree
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
        Me.TreeProdStruc = New DevExpress.XtraTreeList.TreeList()
        Me.psd_comp = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.psd_desc = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.psd_qty = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.psd_scrp_pct = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.invct_cost = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.unit_measure = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.TreeListColumn2 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.use_routing = New DevExpress.XtraEditors.CheckEdit()
        Me.te_level = New DevExpress.XtraEditors.TextEdit()
        Me.pr_ondate = New DevExpress.XtraEditors.DateEdit()
        Me.te_quan = New DevExpress.XtraEditors.TextEdit()
        Me.be_first = New DevExpress.XtraEditors.ButtonEdit()
        Me.use_date = New DevExpress.XtraEditors.CheckEdit()
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeProdStruc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.use_routing.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_level.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_ondate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_ondate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_quan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.use_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Appearance.PageClient.BackColor = System.Drawing.SystemColors.Window
        Me.xtp_data.Appearance.PageClient.Options.UseBackColor = True
        Me.xtp_data.Controls.Add(Me.TreeProdStruc)
        Me.xtp_data.Size = New System.Drawing.Size(853, 287)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LayoutControl1)
        Me.scc_master.Size = New System.Drawing.Size(855, 433)
        Me.scc_master.SplitterPosition = 119
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(855, 308)
        '
        'TreeProdStruc
        '
        Me.TreeProdStruc.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.psd_comp, Me.psd_desc, Me.psd_qty, Me.psd_scrp_pct, Me.invct_cost, Me.unit_measure, Me.TreeListColumn1, Me.TreeListColumn2})
        Me.TreeProdStruc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeProdStruc.Location = New System.Drawing.Point(5, 5)
        Me.TreeProdStruc.Name = "TreeProdStruc"
        Me.TreeProdStruc.OptionsBehavior.PopulateServiceColumns = True
        Me.TreeProdStruc.OptionsSelection.MultiSelect = True
        Me.TreeProdStruc.Size = New System.Drawing.Size(843, 277)
        Me.TreeProdStruc.TabIndex = 0
        '
        'psd_comp
        '
        Me.psd_comp.Caption = "Product Structure Code"
        Me.psd_comp.FieldName = "pt_code"
        Me.psd_comp.Name = "psd_comp"
        Me.psd_comp.Visible = True
        Me.psd_comp.VisibleIndex = 0
        '
        'psd_desc
        '
        Me.psd_desc.Caption = "Description"
        Me.psd_desc.FieldName = "pt_desc1"
        Me.psd_desc.Name = "psd_desc"
        Me.psd_desc.Visible = True
        Me.psd_desc.VisibleIndex = 1
        '
        'psd_qty
        '
        Me.psd_qty.Caption = "Qty"
        Me.psd_qty.FieldName = "psd_qty"
        Me.psd_qty.Name = "psd_qty"
        Me.psd_qty.Visible = True
        Me.psd_qty.VisibleIndex = 2
        '
        'psd_scrp_pct
        '
        Me.psd_scrp_pct.Caption = "Insheet"
        Me.psd_scrp_pct.FieldName = "psd_yield_pct"
        Me.psd_scrp_pct.Format.FormatString = "p"
        Me.psd_scrp_pct.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.psd_scrp_pct.Name = "psd_scrp_pct"
        Me.psd_scrp_pct.Visible = True
        Me.psd_scrp_pct.VisibleIndex = 4
        '
        'invct_cost
        '
        Me.invct_cost.AppearanceCell.Options.UseTextOptions = True
        Me.invct_cost.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invct_cost.Caption = "Cost"
        Me.invct_cost.FieldName = "invct_cost"
        Me.invct_cost.Format.FormatString = "n"
        Me.invct_cost.Format.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invct_cost.Name = "invct_cost"
        Me.invct_cost.Visible = True
        Me.invct_cost.VisibleIndex = 5
        '
        'unit_measure
        '
        Me.unit_measure.Caption = "UM"
        Me.unit_measure.FieldName = "code_name"
        Me.unit_measure.Name = "unit_measure"
        Me.unit_measure.Visible = True
        Me.unit_measure.VisibleIndex = 3
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Indirect"
        Me.TreeListColumn1.FieldName = "psd_indirect"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 6
        '
        'TreeListColumn2
        '
        Me.TreeListColumn2.Caption = "Is Phantom"
        Me.TreeListColumn2.FieldName = "variable_1"
        Me.TreeListColumn2.Name = "TreeListColumn2"
        Me.TreeListColumn2.Visible = True
        Me.TreeListColumn2.VisibleIndex = 7
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.use_routing)
        Me.LayoutControl1.Controls.Add(Me.te_level)
        Me.LayoutControl1.Controls.Add(Me.pr_ondate)
        Me.LayoutControl1.Controls.Add(Me.te_quan)
        Me.LayoutControl1.Controls.Add(Me.be_first)
        Me.LayoutControl1.Controls.Add(Me.use_date)
        Me.LayoutControl1.Controls.Add(Me.le_entity)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem6})
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(855, 119)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'use_routing
        '
        Me.use_routing.Location = New System.Drawing.Point(432, 36)
        Me.use_routing.Name = "use_routing"
        Me.use_routing.Properties.Caption = "Routing"
        Me.use_routing.Size = New System.Drawing.Size(203, 19)
        Me.use_routing.StyleController = Me.LayoutControl1
        Me.use_routing.TabIndex = 15
        '
        'te_level
        '
        Me.te_level.EditValue = "10"
        Me.te_level.Location = New System.Drawing.Point(140, 84)
        Me.te_level.Name = "te_level"
        Me.te_level.Properties.DisplayFormat.FormatString = "n"
        Me.te_level.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_level.Properties.EditFormat.FormatString = "n"
        Me.te_level.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_level.Properties.Mask.EditMask = "n0"
        Me.te_level.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.te_level.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.te_level.Properties.NullValuePrompt = "Cannot Null"
        Me.te_level.Size = New System.Drawing.Size(288, 20)
        Me.te_level.StyleController = Me.LayoutControl1
        Me.te_level.TabIndex = 12
        '
        'pr_ondate
        '
        Me.pr_ondate.EditValue = Nothing
        Me.pr_ondate.Enabled = False
        Me.pr_ondate.Location = New System.Drawing.Point(140, 60)
        Me.pr_ondate.Name = "pr_ondate"
        Me.pr_ondate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_ondate.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.pr_ondate.Size = New System.Drawing.Size(288, 20)
        Me.pr_ondate.StyleController = Me.LayoutControl1
        Me.pr_ondate.TabIndex = 10
        '
        'te_quan
        '
        Me.te_quan.EditValue = "1"
        Me.te_quan.Location = New System.Drawing.Point(141, 84)
        Me.te_quan.Name = "te_quan"
        Me.te_quan.Properties.DisplayFormat.FormatString = "n"
        Me.te_quan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_quan.Properties.EditFormat.FormatString = "n"
        Me.te_quan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_quan.Properties.Mask.EditMask = "n"
        Me.te_quan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.te_quan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.te_quan.Properties.NullValuePrompt = "Cannot Null"
        Me.te_quan.Size = New System.Drawing.Size(287, 20)
        Me.te_quan.StyleController = Me.LayoutControl1
        Me.te_quan.TabIndex = 9
        '
        'be_first
        '
        Me.be_first.Location = New System.Drawing.Point(140, 36)
        Me.be_first.Name = "be_first"
        Me.be_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.be_first.Properties.ReadOnly = True
        Me.be_first.Size = New System.Drawing.Size(288, 20)
        Me.be_first.StyleController = Me.LayoutControl1
        Me.be_first.TabIndex = 5
        '
        'use_date
        '
        Me.use_date.Location = New System.Drawing.Point(432, 59)
        Me.use_date.Name = "use_date"
        Me.use_date.Properties.Caption = "Use Date"
        Me.use_date.Size = New System.Drawing.Size(411, 19)
        Me.use_date.StyleController = Me.LayoutControl1
        Me.use_date.TabIndex = 14
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(140, 12)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(288, 20)
        Me.le_entity.StyleController = Me.LayoutControl1
        Me.le_entity.TabIndex = 4
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.te_quan
        Me.LayoutControlItem6.CustomizationFormText = "Quantity"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(420, 27)
        Me.LayoutControlItem6.Text = "Quantity"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(125, 13)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem1, Me.EmptySpaceItem2, Me.LayoutControlItem4, Me.LayoutControlItem7, Me.EmptySpaceItem1, Me.LayoutControlItem8, Me.EmptySpaceItem3, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(855, 119)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.be_first
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(420, 24)
        Me.LayoutControlItem2.Text = "Product Structure Number"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(125, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.le_entity
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(420, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(125, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(420, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(415, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.pr_ondate
        Me.LayoutControlItem4.CustomizationFormText = "First Date"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(420, 24)
        Me.LayoutControlItem4.Text = "On Date"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(125, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.te_level
        Me.LayoutControlItem7.CustomizationFormText = "Level"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(420, 27)
        Me.LayoutControlItem7.Text = "Level"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(125, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(420, 72)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(415, 27)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.use_date
        Me.LayoutControlItem8.CustomizationFormText = "LayoutControlItem8"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(420, 47)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(415, 25)
        Me.LayoutControlItem8.Text = "LayoutControlItem8"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextToControlDistance = 0
        Me.LayoutControlItem8.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(627, 24)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(208, 23)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.use_routing
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(420, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(207, 23)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'FRoutingStrucTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(855, 433)
        Me.Name = "FRoutingStrucTree"
        Me.Text = "Product Structure Tree"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeProdStruc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.use_routing.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_level.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_ondate.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_ondate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_quan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.use_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeProdStruc As DevExpress.XtraTreeList.TreeList
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents be_first As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents te_quan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents pr_ondate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents te_level As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents use_date As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents psd_comp As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psd_desc As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psd_qty As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents psd_scrp_pct As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents invct_cost As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents unit_measure As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents TreeListColumn2 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents use_routing As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem

End Class
