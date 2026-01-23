<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FViewDepresiasi
    Inherits master_new.FSearch

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
        Me.sc_tct_salv_cost = New DevExpress.XtraEditors.TextEdit
        Me.sc_txt_cost = New DevExpress.XtraEditors.TextEdit
        Me.v_asbk = New DevExpress.XtraEditors.TextEdit
        Me.v_asmt = New DevExpress.XtraEditors.TextEdit
        Me.v_serial = New DevExpress.XtraEditors.TextEdit
        Me.v_part_number = New DevExpress.XtraEditors.TextEdit
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.sc_tct_salv_cost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txt_cost.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.v_asbk.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.v_asmt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.v_serial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.v_part_number.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(860, 543)
        Me.scc_master.SplitterPosition = 96
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(860, 543)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.LayoutControl1)
        Me.xtp_data.Size = New System.Drawing.Size(858, 542)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.sc_tct_salv_cost)
        Me.LayoutControl1.Controls.Add(Me.sc_txt_cost)
        Me.LayoutControl1.Controls.Add(Me.v_asbk)
        Me.LayoutControl1.Controls.Add(Me.v_asmt)
        Me.LayoutControl1.Controls.Add(Me.v_serial)
        Me.LayoutControl1.Controls.Add(Me.v_part_number)
        Me.LayoutControl1.Controls.Add(Me.gc_master)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(5, 5)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(848, 532)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'sc_tct_salv_cost
        '
        Me.sc_tct_salv_cost.Location = New System.Drawing.Point(510, 72)
        Me.sc_tct_salv_cost.Name = "sc_tct_salv_cost"
        Me.sc_tct_salv_cost.Properties.DisplayFormat.FormatString = "n2"
        Me.sc_tct_salv_cost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sc_tct_salv_cost.Properties.Mask.EditMask = "n2"
        Me.sc_tct_salv_cost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.sc_tct_salv_cost.Size = New System.Drawing.Size(314, 20)
        Me.sc_tct_salv_cost.StyleController = Me.LayoutControl1
        Me.sc_tct_salv_cost.TabIndex = 11
        '
        'sc_txt_cost
        '
        Me.sc_txt_cost.Location = New System.Drawing.Point(117, 72)
        Me.sc_txt_cost.Name = "sc_txt_cost"
        Me.sc_txt_cost.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sc_txt_cost.Properties.Appearance.Options.UseFont = True
        Me.sc_txt_cost.Properties.DisplayFormat.FormatString = "n2"
        Me.sc_txt_cost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sc_txt_cost.Properties.Mask.EditMask = "n2"
        Me.sc_txt_cost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.sc_txt_cost.Size = New System.Drawing.Size(296, 20)
        Me.sc_txt_cost.StyleController = Me.LayoutControl1
        Me.sc_txt_cost.TabIndex = 10
        '
        'v_asbk
        '
        Me.v_asbk.Location = New System.Drawing.Point(117, 48)
        Me.v_asbk.Name = "v_asbk"
        Me.v_asbk.Size = New System.Drawing.Size(296, 20)
        Me.v_asbk.StyleController = Me.LayoutControl1
        Me.v_asbk.TabIndex = 9
        '
        'v_asmt
        '
        Me.v_asmt.Location = New System.Drawing.Point(510, 48)
        Me.v_asmt.Name = "v_asmt"
        Me.v_asmt.Size = New System.Drawing.Size(314, 20)
        Me.v_asmt.StyleController = Me.LayoutControl1
        Me.v_asmt.TabIndex = 8
        '
        'v_serial
        '
        Me.v_serial.Location = New System.Drawing.Point(510, 24)
        Me.v_serial.Name = "v_serial"
        Me.v_serial.Size = New System.Drawing.Size(314, 20)
        Me.v_serial.StyleController = Me.LayoutControl1
        Me.v_serial.TabIndex = 7
        '
        'v_part_number
        '
        Me.v_part_number.Location = New System.Drawing.Point(117, 24)
        Me.v_part_number.Name = "v_part_number"
        Me.v_part_number.Size = New System.Drawing.Size(296, 20)
        Me.v_part_number.StyleController = Me.LayoutControl1
        Me.v_part_number.TabIndex = 6
        '
        'gc_master
        '
        Me.gc_master.Location = New System.Drawing.Point(24, 120)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(800, 388)
        Me.gc_master.TabIndex = 5
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.GroupCount = 1
        Me.gv_master.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "v_depr", Me.GridColumn4, "{0:n}")})
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsBehavior.AutoExpandAllGroups = True
        Me.gv_master.OptionsView.ShowFooter = True
        Me.gv_master.OptionsView.ShowGroupedColumns = True
        Me.gv_master.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.GridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Yearh"
        Me.GridColumn1.FieldName = "v_year"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Month"
        Me.GridColumn2.FieldName = "v_month"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 204
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Periode"
        Me.GridColumn3.FieldName = "v_periode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 158
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn4.Caption = "Depresiasi"
        Me.GridColumn4.DisplayFormat.FormatString = "n0"
        Me.GridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.FieldName = "v_depr"
        Me.GridColumn4.GroupFormat.FormatString = "n0"
        Me.GridColumn4.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.SummaryItem.DisplayFormat = "{0:n}"
        Me.GridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 270
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceCell.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.GridColumn5.Caption = "Sisa"
        Me.GridColumn5.DisplayFormat.FormatString = "n0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "v_sisa"
        Me.GridColumn5.GroupFormat.FormatString = "n0"
        Me.GridColumn5.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 325
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2, Me.LayoutControlGroup3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(848, 532)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(828, 96)
        Me.LayoutControlGroup2.Text = "LayoutControlGroup2"
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.v_part_number
        Me.LayoutControlItem1.CustomizationFormText = "Part Number"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem1.Text = "Part Number"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.v_serial
        Me.LayoutControlItem3.CustomizationFormText = "Serial Number"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(393, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(411, 24)
        Me.LayoutControlItem3.Text = "Serial Number"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.v_asmt
        Me.LayoutControlItem4.CustomizationFormText = "Fix Asset Methode"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(393, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(411, 24)
        Me.LayoutControlItem4.Text = "Fix Asset Methode"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.v_asbk
        Me.LayoutControlItem5.CustomizationFormText = "Fix Asset Book"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem5.Text = "Fix Asset Book"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.sc_txt_cost
        Me.LayoutControlItem6.CustomizationFormText = "Cost"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(393, 24)
        Me.LayoutControlItem6.Text = "Cost"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.sc_tct_salv_cost
        Me.LayoutControlItem7.CustomizationFormText = "Salvage Cost"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(393, 48)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(411, 24)
        Me.LayoutControlItem7.Text = "Salvage Cost"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(89, 13)
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(828, 416)
        Me.LayoutControlGroup3.Text = "LayoutControlGroup3"
        Me.LayoutControlGroup3.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.gc_master
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(804, 392)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FViewDepresiasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(860, 543)
        Me.Name = "FViewDepresiasi"
        Me.Text = "View Depresiasi"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.sc_tct_salv_cost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txt_cost.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.v_asbk.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.v_asmt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.v_serial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.v_part_number.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents v_asmt As DevExpress.XtraEditors.TextEdit
    Public WithEvents v_serial As DevExpress.XtraEditors.TextEdit
    Public WithEvents v_part_number As DevExpress.XtraEditors.TextEdit
    Public WithEvents v_asbk As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_txt_cost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents sc_tct_salv_cost As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn

End Class
