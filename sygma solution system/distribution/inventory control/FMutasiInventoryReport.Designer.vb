<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMutasiInventoryReport
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
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_mutasi_inventory = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_mutasi = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.fieldtempmtransdesc = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldptcode = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldlocdesc = New DevExpress.XtraPivotGrid.PivotGridField
        Me.bdgtd_budget_sisa1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.pr_date_1 = New DevExpress.XtraEditors.DateEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.pr_date_2 = New DevExpress.XtraEditors.DateEdit
        Me.Label3 = New System.Windows.Forms.Label
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_mutasi_inventory.SuspendLayout()
        CType(Me.pgc_mutasi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_date_1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_date_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_date_2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_date_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.pr_date_2)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_date_1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(846, 514)
        Me.scc_master.SplitterPosition = 28
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_mutasi_inventory
        Me.xtc_master.Size = New System.Drawing.Size(846, 480)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_mutasi_inventory})
        '
        'xtp_mutasi_inventory
        '
        Me.xtp_mutasi_inventory.Controls.Add(Me.pgc_mutasi)
        Me.xtp_mutasi_inventory.Name = "xtp_mutasi_inventory"
        Me.xtp_mutasi_inventory.Size = New System.Drawing.Size(844, 459)
        Me.xtp_mutasi_inventory.Text = "Mutasi Inventory"
        '
        'pgc_mutasi
        '
        Me.pgc_mutasi.AppearancePrint.FieldValueGrandTotal.Options.UseTextOptions = True
        Me.pgc_mutasi.AppearancePrint.FieldValueGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_mutasi.AppearancePrint.GrandTotalCell.Options.UseTextOptions = True
        Me.pgc_mutasi.AppearancePrint.GrandTotalCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.pgc_mutasi.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_mutasi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_mutasi.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldtempmtransdesc, Me.fieldptcode, Me.fieldlocdesc, Me.bdgtd_budget_sisa1})
        Me.pgc_mutasi.Location = New System.Drawing.Point(0, 0)
        Me.pgc_mutasi.Name = "pgc_mutasi"
        Me.pgc_mutasi.OptionsPrint.UsePrintAppearance = True
        Me.pgc_mutasi.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button
        Me.pgc_mutasi.OptionsView.ShowDataHeaders = False
        Me.pgc_mutasi.OptionsView.ShowFilterHeaders = False
        Me.pgc_mutasi.OptionsView.ShowRowTotals = False
        Me.pgc_mutasi.Size = New System.Drawing.Size(844, 459)
        Me.pgc_mutasi.TabIndex = 6
        '
        'fieldtempmtransdesc
        '
        Me.fieldtempmtransdesc.Appearance.Cell.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.Cell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldtempmtransdesc.Appearance.CellGrandTotal.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.CellGrandTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldtempmtransdesc.Appearance.CellTotal.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.CellTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.fieldtempmtransdesc.Appearance.Header.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldtempmtransdesc.Appearance.Value.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.Value.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldtempmtransdesc.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.fieldtempmtransdesc.Appearance.ValueTotal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.fieldtempmtransdesc.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldtempmtransdesc.AreaIndex = 0
        Me.fieldtempmtransdesc.Caption = "Transaction"
        Me.fieldtempmtransdesc.FieldName = "tempm_trans_desc"
        Me.fieldtempmtransdesc.Name = "fieldtempmtransdesc"
        '
        'fieldptcode
        '
        Me.fieldptcode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldptcode.AreaIndex = 0
        Me.fieldptcode.Caption = "Part Number"
        Me.fieldptcode.FieldName = "pt_code"
        Me.fieldptcode.Name = "fieldptcode"
        '
        'fieldlocdesc
        '
        Me.fieldlocdesc.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.fieldlocdesc.AreaIndex = 1
        Me.fieldlocdesc.Caption = "Location"
        Me.fieldlocdesc.FieldName = "loc_desc"
        Me.fieldlocdesc.Name = "fieldlocdesc"
        '
        'bdgtd_budget_sisa1
        '
        Me.bdgtd_budget_sisa1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.bdgtd_budget_sisa1.AreaIndex = 0
        Me.bdgtd_budget_sisa1.Caption = "Quantity"
        Me.bdgtd_budget_sisa1.CellFormat.FormatString = "n2"
        Me.bdgtd_budget_sisa1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.bdgtd_budget_sisa1.FieldName = "tempm_trans_qty"
        Me.bdgtd_budget_sisa1.Name = "bdgtd_budget_sisa1"
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(49, 5)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(122, 20)
        Me.pr_entity.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Entity :"
        '
        'pr_date_1
        '
        Me.pr_date_1.EditValue = Nothing
        Me.pr_date_1.Location = New System.Drawing.Point(256, 5)
        Me.pr_date_1.Name = "pr_date_1"
        Me.pr_date_1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_date_1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_date_1.Size = New System.Drawing.Size(102, 20)
        Me.pr_date_1.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(193, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "From Date :"
        '
        'pr_date_2
        '
        Me.pr_date_2.EditValue = Nothing
        Me.pr_date_2.Location = New System.Drawing.Point(378, 5)
        Me.pr_date_2.Name = "pr_date_2"
        Me.pr_date_2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_date_2.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_date_2.Size = New System.Drawing.Size(102, 20)
        Me.pr_date_2.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(362, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "to"
        '
        'FMutasiInventoryReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(846, 514)
        Me.Name = "FMutasiInventoryReport"
        Me.Text = "Mutasi Inventory Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_mutasi_inventory.ResumeLayout(False)
        CType(Me.pgc_mutasi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_date_1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_date_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_date_2.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_date_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_mutasi_inventory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pr_date_1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pr_date_2 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents pgc_mutasi As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents fieldtempmtransdesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldptcode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldlocdesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents bdgtd_budget_sisa1 As DevExpress.XtraPivotGrid.PivotGridField

End Class
