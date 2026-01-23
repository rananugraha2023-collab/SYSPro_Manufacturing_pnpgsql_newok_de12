<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryReportDatePivot
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
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.ce_child = New DevExpress.XtraEditors.CheckEdit
        Me.be_loc = New DevExpress.XtraEditors.ButtonEdit
        Me.de_from = New DevExpress.XtraEditors.DateEdit
        Me.le_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldloc_desc = New DevExpress.XtraPivotGrid.PivotGridField
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
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.ce_child.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_loc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.de_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.pgc_master)
        Me.xtp_data.Size = New System.Drawing.Size(813, 349)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LayoutControl1)
        Me.scc_master.Size = New System.Drawing.Size(815, 480)
        Me.scc_master.SplitterPosition = 104
        '
        'xtc_master
        '
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[Default]
        Me.xtc_master.Size = New System.Drawing.Size(815, 370)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.ce_child)
        Me.LayoutControl1.Controls.Add(Me.be_loc)
        Me.LayoutControl1.Controls.Add(Me.de_from)
        Me.LayoutControl1.Controls.Add(Me.le_en_id)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(815, 104)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'ce_child
        '
        Me.ce_child.Location = New System.Drawing.Point(409, 36)
        Me.ce_child.Name = "ce_child"
        Me.ce_child.Properties.Caption = "With Child"
        Me.ce_child.Size = New System.Drawing.Size(194, 19)
        Me.ce_child.StyleController = Me.LayoutControl1
        Me.ce_child.TabIndex = 8
        '
        'be_loc
        '
        Me.be_loc.Location = New System.Drawing.Point(56, 60)
        Me.be_loc.Name = "be_loc"
        Me.be_loc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_loc.Size = New System.Drawing.Size(349, 20)
        Me.be_loc.StyleController = Me.LayoutControl1
        Me.be_loc.TabIndex = 6
        '
        'de_from
        '
        Me.de_from.EditValue = Nothing
        Me.de_from.Location = New System.Drawing.Point(56, 36)
        Me.de_from.Name = "de_from"
        Me.de_from.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.de_from.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.de_from.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.de_from.Size = New System.Drawing.Size(349, 20)
        Me.de_from.StyleController = Me.LayoutControl1
        Me.de_from.TabIndex = 5
        '
        'le_en_id
        '
        Me.le_en_id.Location = New System.Drawing.Point(56, 12)
        Me.le_en_id.Name = "le_en_id"
        Me.le_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_en_id.Size = New System.Drawing.Size(349, 20)
        Me.le_en_id.StyleController = Me.LayoutControl1
        Me.le_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.EmptySpaceItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.LayoutControlItem5, Me.EmptySpaceItem3, Me.EmptySpaceItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(815, 104)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.le_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(397, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(40, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.de_from
        Me.LayoutControlItem2.CustomizationFormText = "Date"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(397, 24)
        Me.LayoutControlItem2.Text = "Date"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(40, 13)
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(397, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(398, 24)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.be_loc
        Me.LayoutControlItem3.CustomizationFormText = "Location"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(397, 24)
        Me.LayoutControlItem3.Text = "Location"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(40, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(595, 24)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(200, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.ce_child
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(397, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(198, 24)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(397, 48)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(398, 24)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'EmptySpaceItem4
        '
        Me.EmptySpaceItem4.CustomizationFormText = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem4.Name = "EmptySpaceItem4"
        Me.EmptySpaceItem4.Size = New System.Drawing.Size(795, 12)
        Me.EmptySpaceItem4.Text = "EmptySpaceItem4"
        Me.EmptySpaceItem4.TextSize = New System.Drawing.Size(0, 0)
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.fieldloc_desc})
        Me.pgc_master.Location = New System.Drawing.Point(5, 5)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.OptionsCustomization.AllowFilter = False
        Me.pgc_master.Size = New System.Drawing.Size(803, 339)
        Me.pgc_master.TabIndex = 2
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Qty"
        Me.PivotGridField1.CellFormat.FormatString = "n"
        Me.PivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField1.FieldName = "inv_qty"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 0
        Me.PivotGridField2.Caption = "Part Code"
        Me.PivotGridField2.FieldName = "pt_code"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField3.AreaIndex = 1
        Me.PivotGridField3.Caption = "Description 1"
        Me.PivotGridField3.FieldName = "pt_desc1"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'PivotGridField4
        '
        Me.PivotGridField4.AreaIndex = 0
        Me.PivotGridField4.Caption = "Description 2"
        Me.PivotGridField4.FieldName = "pt_desc2"
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Group"
        Me.PivotGridField5.FieldName = "invh_group"
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'fieldloc_desc
        '
        Me.fieldloc_desc.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldloc_desc.AreaIndex = 2
        Me.fieldloc_desc.Caption = "Location"
        Me.fieldloc_desc.CellFormat.FormatString = "n0"
        Me.fieldloc_desc.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldloc_desc.FieldName = "loc_desc"
        Me.fieldloc_desc.Name = "fieldloc_desc"
        Me.fieldloc_desc.TotalCellFormat.FormatString = "n0"
        Me.fieldloc_desc.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldloc_desc.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        Me.fieldloc_desc.TotalValueFormat.FormatString = "n0"
        Me.fieldloc_desc.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldloc_desc.ValueFormat.FormatString = "n0"
        Me.fieldloc_desc.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'FInventoryReportDatePivot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(815, 480)
        Me.Name = "FInventoryReportDatePivot"
        Me.Text = "Inventory Report by Date Pivot"
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
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.ce_child.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_loc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.de_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents de_from As DevExpress.XtraEditors.DateEdit
    Friend WithEvents le_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ce_child As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
    Public WithEvents be_loc As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldloc_desc As DevExpress.XtraPivotGrid.PivotGridField

End Class
