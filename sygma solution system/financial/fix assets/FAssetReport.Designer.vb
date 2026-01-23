<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAssetReport
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
        Me.DstestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl
        Me.pgc_detail = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldptcode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldasscode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldassrepdepresiasi = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldassrepdepracum = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldassrepnilaibuku = New DevExpress.XtraPivotGrid.PivotGridField
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.pr_date = New DevExpress.XtraEditors.DateEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DstestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.pr_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.GroupControl1)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel2.Controls.Add(Me.LayoutControl1)
        Me.scc_master.Size = New System.Drawing.Size(719, 500)
        Me.scc_master.SplitterPosition = 53
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(17, 51)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Budget Code  :"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.pgc_detail)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(719, 441)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'pgc_detail
        '
        Me.pgc_detail.AppearancePrint.FieldValueGrandTotal.Options.UseTextOptions = True
        Me.pgc_detail.AppearancePrint.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_detail.AppearancePrint.GrandTotalCell.Options.UseTextOptions = True
        Me.pgc_detail.AppearancePrint.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_detail.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_detail.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField3, Me.PivotGridField6, Me.PivotGridField5, Me.fieldptcode, Me.fieldasscode, Me.PivotGridField2, Me.PivotGridField4, Me.fieldassrepdepresiasi, Me.fieldassrepdepracum, Me.fieldassrepnilaibuku})
        Me.pgc_detail.Location = New System.Drawing.Point(12, 12)
        Me.pgc_detail.Name = "pgc_detail"
        Me.pgc_detail.OptionsCustomization.AllowFilter = False
        Me.pgc_detail.OptionsPrint.UsePrintAppearance = True
        Me.pgc_detail.Size = New System.Drawing.Size(695, 417)
        Me.pgc_detail.TabIndex = 5
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Tahun"
        Me.PivotGridField3.FieldName = "assrep_tahun"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField6.AreaIndex = 1
        Me.PivotGridField6.Caption = "Bulan"
        Me.PivotGridField6.FieldName = "assrep_bulan"
        Me.PivotGridField6.Name = "PivotGridField6"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField5.AreaIndex = 2
        Me.PivotGridField5.Caption = "Bulan Ke"
        Me.PivotGridField5.FieldName = "assrep_bulan_ke"
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.Visible = False
        '
        'fieldptcode
        '
        Me.fieldptcode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldptcode.AreaIndex = 0
        Me.fieldptcode.Caption = "Part Number"
        Me.fieldptcode.FieldName = "pt_code"
        Me.fieldptcode.Name = "fieldptcode"
        '
        'fieldasscode
        '
        Me.fieldasscode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldasscode.AreaIndex = 1
        Me.fieldasscode.Caption = "Asset Code"
        Me.fieldasscode.FieldName = "ass_code"
        Me.fieldasscode.Name = "fieldasscode"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 2
        Me.PivotGridField2.Caption = "Service Date"
        Me.PivotGridField2.CellFormat.FormatString = "dd/MM/yyyy"
        Me.PivotGridField2.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.PivotGridField2.FieldName = "ass_service_date"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.ValueFormat.FormatString = "dd/MM/yyyy"
        Me.PivotGridField2.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField4.AreaIndex = 3
        Me.PivotGridField4.Caption = "Basis Cost"
        Me.PivotGridField4.CellFormat.FormatString = "n2"
        Me.PivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField4.FieldName = "basis_cost"
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.ValueFormat.FormatString = "n2"
        Me.PivotGridField4.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'fieldassrepdepresiasi
        '
        Me.fieldassrepdepresiasi.Appearance.Header.Options.UseTextOptions = True
        Me.fieldassrepdepresiasi.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldassrepdepresiasi.Appearance.Value.Options.UseTextOptions = True
        Me.fieldassrepdepresiasi.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepdepresiasi.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldassrepdepresiasi.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepdepresiasi.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldassrepdepresiasi.AreaIndex = 0
        Me.fieldassrepdepresiasi.Caption = "Depresiasi"
        Me.fieldassrepdepresiasi.CellFormat.FormatString = "n0"
        Me.fieldassrepdepresiasi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepresiasi.FieldName = "assrep_depresiasi"
        Me.fieldassrepdepresiasi.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldassrepdepresiasi.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepresiasi.Name = "fieldassrepdepresiasi"
        Me.fieldassrepdepresiasi.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepdepresiasi.Options.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepdepresiasi.Options.ReadOnly = True
        Me.fieldassrepdepresiasi.Options.ShowCustomTotals = False
        Me.fieldassrepdepresiasi.Options.ShowGrandTotal = False
        Me.fieldassrepdepresiasi.Options.ShowTotals = False
        Me.fieldassrepdepresiasi.TotalCellFormat.FormatString = "n0"
        Me.fieldassrepdepresiasi.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepresiasi.TotalValueFormat.FormatString = "n0"
        Me.fieldassrepdepresiasi.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepresiasi.ValueFormat.FormatString = "n0"
        Me.fieldassrepdepresiasi.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepresiasi.Width = 91
        '
        'fieldassrepdepracum
        '
        Me.fieldassrepdepracum.Appearance.Header.Options.UseTextOptions = True
        Me.fieldassrepdepracum.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldassrepdepracum.Appearance.Value.Options.UseTextOptions = True
        Me.fieldassrepdepracum.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepdepracum.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldassrepdepracum.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepdepracum.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldassrepdepracum.AreaIndex = 1
        Me.fieldassrepdepracum.Caption = "depr Akum"
        Me.fieldassrepdepracum.CellFormat.FormatString = "n0"
        Me.fieldassrepdepracum.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepracum.FieldName = "assrep_depr_acum"
        Me.fieldassrepdepracum.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldassrepdepracum.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepracum.Name = "fieldassrepdepracum"
        Me.fieldassrepdepracum.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepdepracum.Options.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepdepracum.Options.ReadOnly = True
        Me.fieldassrepdepracum.Options.ShowCustomTotals = False
        Me.fieldassrepdepracum.Options.ShowGrandTotal = False
        Me.fieldassrepdepracum.Options.ShowTotals = False
        Me.fieldassrepdepracum.TotalCellFormat.FormatString = "n0"
        Me.fieldassrepdepracum.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepracum.TotalValueFormat.FormatString = "n0"
        Me.fieldassrepdepracum.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepracum.ValueFormat.FormatString = "n0"
        Me.fieldassrepdepracum.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepdepracum.Width = 90
        '
        'fieldassrepnilaibuku
        '
        Me.fieldassrepnilaibuku.Appearance.Header.Options.UseTextOptions = True
        Me.fieldassrepnilaibuku.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldassrepnilaibuku.Appearance.Value.Options.UseTextOptions = True
        Me.fieldassrepnilaibuku.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepnilaibuku.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldassrepnilaibuku.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldassrepnilaibuku.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldassrepnilaibuku.AreaIndex = 2
        Me.fieldassrepnilaibuku.Caption = "Nilai Buku"
        Me.fieldassrepnilaibuku.CellFormat.FormatString = "n0"
        Me.fieldassrepnilaibuku.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepnilaibuku.FieldName = "assrep_nilai_buku"
        Me.fieldassrepnilaibuku.GrandTotalCellFormat.FormatString = "n0"
        Me.fieldassrepnilaibuku.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepnilaibuku.Name = "fieldassrepnilaibuku"
        Me.fieldassrepnilaibuku.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepnilaibuku.Options.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.fieldassrepnilaibuku.Options.ReadOnly = True
        Me.fieldassrepnilaibuku.Options.ShowCustomTotals = False
        Me.fieldassrepnilaibuku.Options.ShowGrandTotal = False
        Me.fieldassrepnilaibuku.Options.ShowTotals = False
        Me.fieldassrepnilaibuku.TotalCellFormat.FormatString = "n0"
        Me.fieldassrepnilaibuku.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepnilaibuku.TotalValueFormat.FormatString = "n0"
        Me.fieldassrepnilaibuku.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepnilaibuku.ValueFormat.FormatString = "n0"
        Me.fieldassrepnilaibuku.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldassrepnilaibuku.Width = 82
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(719, 441)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.pgc_detail
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(699, 421)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.pr_date)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(719, 53)
        Me.GroupControl1.TabIndex = 6
        Me.GroupControl1.Text = "Filter By ||"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(17, 29)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(45, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "To Date :"
        '
        'pr_date
        '
        Me.pr_date.EditValue = Nothing
        Me.pr_date.Location = New System.Drawing.Point(64, 27)
        Me.pr_date.Name = "pr_date"
        Me.pr_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_date.Size = New System.Drawing.Size(100, 20)
        Me.pr_date.TabIndex = 0
        '
        'FAssetReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(719, 500)
        Me.Name = "FAssetReport"
        Me.Text = "Asset Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DstestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.pgc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.pr_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DstestBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents pgc_detail As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents fieldptcode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldasscode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldassrepdepresiasi As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldassrepdepracum As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldassrepnilaibuku As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField

End Class
