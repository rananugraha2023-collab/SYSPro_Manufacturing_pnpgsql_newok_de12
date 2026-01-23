<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FBudgetDetail
    Inherits master_new.MasterInfTwo

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FBudgetDetail))
        Me.DstestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.pr_budget_code = New DevExpress.XtraEditors.ButtonEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.bdgtd_bdgtp_id = New DevExpress.XtraEditors.LookUpEdit
        Me.btn_add_update = New DevExpress.XtraEditors.SimpleButton
        Me.bdgtd_budget = New DevExpress.XtraEditors.TextEdit
        Me.bdgtd_ac_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pgc_detail = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.fieldbdgtpcode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldaccode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldacname = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdbudget = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdalokasi = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldbdgtdrealisasi = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DstestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_budget_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.bdgtd_bdgtp_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtd_budget.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bdgtd_ac_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_budget_code)
        Me.scc_master.Panel2.Controls.Add(Me.LayoutControl1)
        Me.scc_master.Size = New System.Drawing.Size(866, 500)
        Me.scc_master.SplitterPosition = 35
        '
        'pr_budget_code
        '
        Me.pr_budget_code.Location = New System.Drawing.Point(89, 9)
        Me.pr_budget_code.Name = "pr_budget_code"
        Me.pr_budget_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_budget_code.Properties.ReadOnly = True
        Me.pr_budget_code.Size = New System.Drawing.Size(129, 20)
        Me.pr_budget_code.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(13, 13)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Budget Code  :"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.bdgtd_bdgtp_id)
        Me.LayoutControl1.Controls.Add(Me.btn_add_update)
        Me.LayoutControl1.Controls.Add(Me.bdgtd_budget)
        Me.LayoutControl1.Controls.Add(Me.bdgtd_ac_id)
        Me.LayoutControl1.Controls.Add(Me.pgc_detail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(866, 459)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'bdgtd_bdgtp_id
        '
        Me.bdgtd_bdgtp_id.Location = New System.Drawing.Point(95, 365)
        Me.bdgtd_bdgtp_id.Name = "bdgtd_bdgtp_id"
        Me.bdgtd_bdgtp_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgtd_bdgtp_id.Size = New System.Drawing.Size(337, 20)
        Me.bdgtd_bdgtp_id.StyleController = Me.LayoutControl1
        Me.bdgtd_bdgtp_id.TabIndex = 10
        '
        'btn_add_update
        '
        Me.btn_add_update.Image = CType(resources.GetObject("btn_add_update.Image"), System.Drawing.Image)
        Me.btn_add_update.Location = New System.Drawing.Point(436, 413)
        Me.btn_add_update.Name = "btn_add_update"
        Me.btn_add_update.Size = New System.Drawing.Size(144, 22)
        Me.btn_add_update.StyleController = Me.LayoutControl1
        Me.btn_add_update.TabIndex = 9
        Me.btn_add_update.Text = "Add/Update"
        '
        'bdgtd_budget
        '
        Me.bdgtd_budget.Location = New System.Drawing.Point(95, 413)
        Me.bdgtd_budget.Name = "bdgtd_budget"
        Me.bdgtd_budget.Properties.DisplayFormat.FormatString = "n2"
        Me.bdgtd_budget.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.bdgtd_budget.Properties.EditFormat.FormatString = "n2"
        Me.bdgtd_budget.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.bdgtd_budget.Properties.Mask.EditMask = "n2"
        Me.bdgtd_budget.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.bdgtd_budget.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.bdgtd_budget.Size = New System.Drawing.Size(337, 20)
        Me.bdgtd_budget.StyleController = Me.LayoutControl1
        Me.bdgtd_budget.TabIndex = 8
        '
        'bdgtd_ac_id
        '
        Me.bdgtd_ac_id.Location = New System.Drawing.Point(95, 389)
        Me.bdgtd_ac_id.Name = "bdgtd_ac_id"
        Me.bdgtd_ac_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.bdgtd_ac_id.Size = New System.Drawing.Size(337, 20)
        Me.bdgtd_ac_id.StyleController = Me.LayoutControl1
        Me.bdgtd_ac_id.TabIndex = 7
        '
        'pgc_detail
        '
        Me.pgc_detail.AppearancePrint.FieldValueGrandTotal.Options.UseTextOptions = True
        Me.pgc_detail.AppearancePrint.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_detail.AppearancePrint.GrandTotalCell.Options.UseTextOptions = True
        Me.pgc_detail.AppearancePrint.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_detail.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_detail.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldbdgtpcode, Me.fieldaccode, Me.fieldacname, Me.fieldbdgtdbudget, Me.fieldbdgtdalokasi, Me.fieldbdgtdrealisasi, Me.PivotGridField1})
        Me.pgc_detail.Location = New System.Drawing.Point(12, 12)
        Me.pgc_detail.Name = "pgc_detail"
        Me.pgc_detail.OptionsCustomization.AllowFilter = False
        Me.pgc_detail.OptionsPrint.UsePrintAppearance = True
        Me.pgc_detail.Size = New System.Drawing.Size(842, 337)
        Me.pgc_detail.TabIndex = 5
        '
        'fieldbdgtpcode
        '
        Me.fieldbdgtpcode.Appearance.Cell.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.CellGrandTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.CellTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtpcode.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtpcode.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtpcode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldbdgtpcode.AreaIndex = 1
        Me.fieldbdgtpcode.Caption = "Periode"
        Me.fieldbdgtpcode.FieldName = "bdgtp_code"
        Me.fieldbdgtpcode.Name = "fieldbdgtpcode"
        '
        'fieldaccode
        '
        Me.fieldaccode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldaccode.AreaIndex = 0
        Me.fieldaccode.Caption = "Account Code"
        Me.fieldaccode.FieldName = "ac_code"
        Me.fieldaccode.Name = "fieldaccode"
        '
        'fieldacname
        '
        Me.fieldacname.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldacname.AreaIndex = 1
        Me.fieldacname.Caption = "Account Name"
        Me.fieldacname.FieldName = "ac_name"
        Me.fieldacname.Name = "fieldacname"
        '
        'fieldbdgtdbudget
        '
        Me.fieldbdgtdbudget.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdbudget.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdbudget.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdbudget.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdbudget.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdbudget.AreaIndex = 0
        Me.fieldbdgtdbudget.Caption = "Budget"
        Me.fieldbdgtdbudget.CellFormat.FormatString = "n2"
        Me.fieldbdgtdbudget.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.FieldName = "bdgtd_budget"
        Me.fieldbdgtdbudget.GrandTotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdbudget.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.Name = "fieldbdgtdbudget"
        Me.fieldbdgtdbudget.TotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdbudget.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.TotalValueFormat.FormatString = "n2"
        Me.fieldbdgtdbudget.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.ValueFormat.FormatString = "n2"
        Me.fieldbdgtdbudget.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdbudget.Width = 118
        '
        'fieldbdgtdalokasi
        '
        Me.fieldbdgtdalokasi.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdalokasi.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdalokasi.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdalokasi.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdalokasi.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdalokasi.AreaIndex = 1
        Me.fieldbdgtdalokasi.Caption = "Alokasi"
        Me.fieldbdgtdalokasi.CellFormat.FormatString = "n2"
        Me.fieldbdgtdalokasi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.FieldName = "bdgtd_alokasi"
        Me.fieldbdgtdalokasi.GrandTotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdalokasi.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.Name = "fieldbdgtdalokasi"
        Me.fieldbdgtdalokasi.TotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdalokasi.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.TotalValueFormat.FormatString = "n2"
        Me.fieldbdgtdalokasi.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.ValueFormat.FormatString = "n2"
        Me.fieldbdgtdalokasi.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdalokasi.Width = 135
        '
        'fieldbdgtdrealisasi
        '
        Me.fieldbdgtdrealisasi.Appearance.Header.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldbdgtdrealisasi.Appearance.Value.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdrealisasi.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldbdgtdrealisasi.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldbdgtdrealisasi.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldbdgtdrealisasi.AreaIndex = 2
        Me.fieldbdgtdrealisasi.Caption = "Realisasi"
        Me.fieldbdgtdrealisasi.CellFormat.FormatString = "n2"
        Me.fieldbdgtdrealisasi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.FieldName = "bdgtd_realisasi"
        Me.fieldbdgtdrealisasi.GrandTotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdrealisasi.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.Name = "fieldbdgtdrealisasi"
        Me.fieldbdgtdrealisasi.TotalCellFormat.FormatString = "n2"
        Me.fieldbdgtdrealisasi.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.TotalValueFormat.FormatString = "n2"
        Me.fieldbdgtdrealisasi.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.ValueFormat.FormatString = "n2"
        Me.fieldbdgtdrealisasi.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldbdgtdrealisasi.Width = 122
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "ID"
        Me.PivotGridField1.FieldName = "bdgtd_bdgtp_id"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(866, 459)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem4, Me.EmptySpaceItem3, Me.EmptySpaceItem4, Me.EmptySpaceItem5, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 341)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(846, 98)
        Me.LayoutControlGroup3.Text = "LayoutControlGroup3"
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.bdgtd_ac_id
        Me.LayoutControlItem3.CustomizationFormText = "Account Code"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem3.Text = "Account Code"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.bdgtd_budget
        Me.LayoutControlItem4.CustomizationFormText = "Budget"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(412, 26)
        Me.LayoutControlItem4.Text = "Budget"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(67, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(412, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(410, 32)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(412, 32)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(410, 16)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem5
        '
        Me.EmptySpaceItem5.CustomizationFormText = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Location = New System.Drawing.Point(560, 48)
        Me.EmptySpaceItem5.Name = "EmptySpaceItem5"
        Me.EmptySpaceItem5.Size = New System.Drawing.Size(262, 26)
        Me.EmptySpaceItem5.Text = "EmptySpaceItem5"
        Me.EmptySpaceItem5.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btn_add_update
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(412, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(148, 26)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.bdgtd_bdgtp_id
        Me.LayoutControlItem6.CustomizationFormText = "Periode"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(412, 24)
        Me.LayoutControlItem6.Text = "Periode"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(67, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pgc_detail
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(846, 341)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FBudgetDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(866, 500)
        Me.Name = "FBudgetDetail"
        Me.Text = "Budget Detail"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DstestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_budget_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.bdgtd_bdgtp_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtd_budget.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bdgtd_ac_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DstestBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Public WithEvents pr_budget_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents pgc_detail As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents fieldbdgtpcode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldaccode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldacname As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdbudget As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdalokasi As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldbdgtdrealisasi As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents bdgtd_ac_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents bdgtd_budget As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btn_add_update As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents bdgtd_bdgtp_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField

End Class
