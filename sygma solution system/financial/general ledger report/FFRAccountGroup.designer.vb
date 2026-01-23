<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FFRAccountGroup
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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldperiode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldaccode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldacname = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldglbalbalanceend = New DevExpress.XtraPivotGrid.PivotGridField
        Me.de_to_end = New DevExpress.XtraEditors.DateEdit
        Me.de_to_first = New DevExpress.XtraEditors.DateEdit
        Me.le_to_gcal = New DevExpress.XtraEditors.LookUpEdit
        Me.le_report = New DevExpress.XtraEditors.LookUpEdit
        Me.le_from_gcal = New DevExpress.XtraEditors.LookUpEdit
        Me.de_from_first = New DevExpress.XtraEditors.DateEdit
        Me.de_from_end = New DevExpress.XtraEditors.DateEdit
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_to_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_to_end.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_to_first.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_to_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_to_gcal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_report.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_from_gcal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from_first.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from_end.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_master.Size = New System.Drawing.Size(838, 433)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.pgc_master)
        Me.lci_master.Controls.Add(Me.de_to_end)
        Me.lci_master.Controls.Add(Me.de_to_first)
        Me.lci_master.Controls.Add(Me.le_to_gcal)
        Me.lci_master.Controls.Add(Me.le_report)
        Me.lci_master.Controls.Add(Me.le_from_gcal)
        Me.lci_master.Controls.Add(Me.de_from_first)
        Me.lci_master.Controls.Add(Me.de_from_end)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(838, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 3
        Me.lci_master.Text = "LayoutControl1"
        '
        'pgc_master
        '
        Me.pgc_master.AppearancePrint.FieldValueGrandTotal.Options.UseTextOptions = True
        Me.pgc_master.AppearancePrint.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_master.AppearancePrint.GrandTotalCell.Options.UseTextOptions = True
        Me.pgc_master.AppearancePrint.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.fieldperiode, Me.fieldaccode, Me.fieldacname, Me.fieldglbalbalanceend})
        Me.pgc_master.Location = New System.Drawing.Point(12, 148)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.OptionsPrint.UsePrintAppearance = True
        Me.pgc_master.OptionsView.ShowGrandTotalsForSingleValues = True
        Me.pgc_master.Size = New System.Drawing.Size(814, 273)
        Me.pgc_master.TabIndex = 19
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "YEAR"
        Me.PivotGridField1.FieldName = "gcal_year"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'fieldperiode
        '
        Me.fieldperiode.Appearance.Cell.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldperiode.Appearance.CellGrandTotal.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldperiode.Appearance.CellTotal.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldperiode.Appearance.Header.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldperiode.Appearance.Value.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldperiode.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldperiode.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldperiode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldperiode.AreaIndex = 1
        Me.fieldperiode.Caption = "Periode"
        Me.fieldperiode.FieldName = "periode"
        Me.fieldperiode.Name = "fieldperiode"
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
        'fieldglbalbalanceend
        '
        Me.fieldglbalbalanceend.Appearance.Header.Options.UseTextOptions = True
        Me.fieldglbalbalanceend.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldglbalbalanceend.Appearance.Value.Options.UseTextOptions = True
        Me.fieldglbalbalanceend.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldglbalbalanceend.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldglbalbalanceend.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldglbalbalanceend.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldglbalbalanceend.AreaIndex = 0
        Me.fieldglbalbalanceend.Caption = "Amount"
        Me.fieldglbalbalanceend.CellFormat.FormatString = "n2"
        Me.fieldglbalbalanceend.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldglbalbalanceend.FieldName = "glbal_balance_end"
        Me.fieldglbalbalanceend.GrandTotalCellFormat.FormatString = "n2"
        Me.fieldglbalbalanceend.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldglbalbalanceend.Name = "fieldglbalbalanceend"
        Me.fieldglbalbalanceend.TotalCellFormat.FormatString = "n2"
        Me.fieldglbalbalanceend.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldglbalbalanceend.TotalValueFormat.FormatString = "n2"
        Me.fieldglbalbalanceend.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldglbalbalanceend.ValueFormat.FormatString = "n2"
        Me.fieldglbalbalanceend.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldglbalbalanceend.Width = 118
        '
        'de_to_end
        '
        Me.de_to_end.EditValue = Nothing
        Me.de_to_end.Location = New System.Drawing.Point(731, 100)
        Me.de_to_end.Name = "de_to_end"
        Me.de_to_end.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_to_end.Properties.ReadOnly = True
        Me.de_to_end.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_to_end.Size = New System.Drawing.Size(71, 20)
        Me.de_to_end.StyleController = Me.lci_master
        Me.de_to_end.TabIndex = 18
        '
        'de_to_first
        '
        Me.de_to_first.EditValue = Nothing
        Me.de_to_first.Location = New System.Drawing.Point(616, 100)
        Me.de_to_first.Name = "de_to_first"
        Me.de_to_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_to_first.Properties.ReadOnly = True
        Me.de_to_first.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_to_first.Size = New System.Drawing.Size(71, 20)
        Me.de_to_first.StyleController = Me.lci_master
        Me.de_to_first.TabIndex = 17
        '
        'le_to_gcal
        '
        Me.le_to_gcal.Location = New System.Drawing.Point(616, 76)
        Me.le_to_gcal.Name = "le_to_gcal"
        Me.le_to_gcal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_to_gcal.Size = New System.Drawing.Size(186, 20)
        Me.le_to_gcal.StyleController = Me.lci_master
        Me.le_to_gcal.TabIndex = 16
        '
        'le_report
        '
        Me.le_report.Location = New System.Drawing.Point(64, 44)
        Me.le_report.Name = "le_report"
        Me.le_report.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_report.Size = New System.Drawing.Size(209, 20)
        Me.le_report.StyleController = Me.lci_master
        Me.le_report.TabIndex = 14
        '
        'le_from_gcal
        '
        Me.le_from_gcal.Location = New System.Drawing.Point(353, 76)
        Me.le_from_gcal.Name = "le_from_gcal"
        Me.le_from_gcal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_from_gcal.Size = New System.Drawing.Size(195, 20)
        Me.le_from_gcal.StyleController = Me.lci_master
        Me.le_from_gcal.TabIndex = 10
        '
        'de_from_first
        '
        Me.de_from_first.EditValue = Nothing
        Me.de_from_first.Location = New System.Drawing.Point(353, 100)
        Me.de_from_first.Name = "de_from_first"
        Me.de_from_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_from_first.Properties.Mask.EditMask = ""
        Me.de_from_first.Properties.ReadOnly = True
        Me.de_from_first.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_from_first.Size = New System.Drawing.Size(75, 20)
        Me.de_from_first.StyleController = Me.lci_master
        Me.de_from_first.TabIndex = 5
        '
        'de_from_end
        '
        Me.de_from_end.EditValue = Nothing
        Me.de_from_end.Location = New System.Drawing.Point(472, 100)
        Me.de_from_end.Name = "de_from_end"
        Me.de_from_end.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_from_end.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.de_from_end.Properties.Mask.EditMask = ""
        Me.de_from_end.Properties.ReadOnly = True
        Me.de_from_end.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_from_end.Size = New System.Drawing.Size(76, 20)
        Me.de_from_end.StyleController = Me.lci_master
        Me.de_from_end.TabIndex = 4
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup5, Me.LayoutControlGroup6, Me.LayoutControlItem12})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(838, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "Periode"
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup1, Me.LayoutControlGroup2})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(277, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(541, 136)
        Me.LayoutControlGroup5.Text = "Periode"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "From"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem8, Me.LayoutControlItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(263, 92)
        Me.LayoutControlGroup1.Text = "From"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.le_from_gcal
        Me.LayoutControlItem1.CustomizationFormText = "Periode"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(239, 24)
        Me.LayoutControlItem1.Text = "Periode"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.de_from_first
        Me.LayoutControlItem8.CustomizationFormText = "First"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(119, 24)
        Me.LayoutControlItem8.Text = "First"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.de_from_end
        Me.LayoutControlItem7.CustomizationFormText = "End"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(119, 24)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(120, 24)
        Me.LayoutControlItem7.Text = "End"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "To"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem11})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(263, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(254, 92)
        Me.LayoutControlGroup2.Text = "To"
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.le_to_gcal
        Me.LayoutControlItem5.CustomizationFormText = "Periode"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(230, 24)
        Me.LayoutControlItem5.Text = "Periode"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.de_to_first
        Me.LayoutControlItem6.CustomizationFormText = "First"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(115, 24)
        Me.LayoutControlItem6.Text = "First"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.de_to_end
        Me.LayoutControlItem11.CustomizationFormText = "End"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(115, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(115, 24)
        Me.LayoutControlItem11.Text = "End"
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(36, 13)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.EmptySpaceItem2})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(277, 136)
        Me.LayoutControlGroup6.Text = "Position"
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.le_report
        Me.LayoutControlItem2.CustomizationFormText = "Account Name"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(253, 24)
        Me.LayoutControlItem2.Text = "Report"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(36, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 24)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(253, 68)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.Control = Me.pgc_master
        Me.LayoutControlItem12.CustomizationFormText = "LayoutControlItem12"
        Me.LayoutControlItem12.Location = New System.Drawing.Point(0, 136)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(818, 277)
        Me.LayoutControlItem12.Text = "LayoutControlItem12"
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'FFRAccountGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(838, 433)
        Me.Controls.Add(Me.lci_master)
        Me.Name = "FFRAccountGroup"
        Me.Text = "Financial Report Account Group"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.Controls.SetChildIndex(Me.lci_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_to_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_to_end.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_to_first.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_to_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_to_gcal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_report.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_from_gcal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from_first.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from_end.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents de_from_first As DevExpress.XtraEditors.DateEdit
    Public WithEvents de_from_end As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents le_from_gcal As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents le_report As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents de_to_end As DevExpress.XtraEditors.DateEdit
    Friend WithEvents de_to_first As DevExpress.XtraEditors.DateEdit
    Friend WithEvents le_to_gcal As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents fieldperiode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldaccode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldacname As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldglbalbalanceend As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem

End Class
