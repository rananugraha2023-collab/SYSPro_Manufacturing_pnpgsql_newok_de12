<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FUnvoucheredPOReceipt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FUnvoucheredPOReceipt))
        Me.Label2 = New System.Windows.Forms.Label
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.cu_id = New DevExpress.XtraEditors.LookUpEdit
        Me.aaa = New DevExpress.XtraLayout.LayoutControlItem
        Me.RBBaseCU = New System.Windows.Forms.RadioButton
        Me.RBFilterCU = New System.Windows.Forms.RadioButton
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cu_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aaa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.pgc_master)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.RBFilterCU)
        Me.scc_master.Panel1.Controls.Add(Me.RBBaseCU)
        Me.scc_master.Panel1.Controls.Add(Me.cu_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Size = New System.Drawing.Size(872, 491)
        Me.scc_master.SplitterPosition = 35
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(870, 429)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(860, 369)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(209, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Currency"
        '
        'OdbcSelectCommand1
        '
        Me.OdbcSelectCommand1.CommandText = resources.GetString("OdbcSelectCommand1.CommandText")
        Me.OdbcSelectCommand1.Connection = Me.OdbcConnection1
        '
        'OdbcConnection1
        '
        Me.OdbcConnection1.ConnectionString = "Dsn=syspro"
        '
        'OdbcDataAdapter1
        '
        Me.OdbcDataAdapter1.SelectCommand = Me.OdbcSelectCommand1
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("rcvd_qty", "rcvd_qty"), New System.Data.Common.DataColumnMapping("rcvd_qty_inv", "rcvd_qty_inv"), New System.Data.Common.DataColumnMapping("qty_accrue", "qty_accrue"), New System.Data.Common.DataColumnMapping("ap_accrue", "ap_accrue"), New System.Data.Common.DataColumnMapping("rcv_eff_date", "rcv_eff_date"), New System.Data.Common.DataColumnMapping("pod_cost", "pod_cost"), New System.Data.Common.DataColumnMapping("po_exc_rate", "po_exc_rate"), New System.Data.Common.DataColumnMapping("po_code", "po_code"), New System.Data.Common.DataColumnMapping("po_cu_id", "po_cu_id"), New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name")})})
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField5, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField1, Me.PivotGridField4})
        Me.pgc_master.Location = New System.Drawing.Point(5, 5)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.Size = New System.Drawing.Size(543, 335)
        Me.pgc_master.TabIndex = 0
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField5.AreaIndex = 1
        Me.PivotGridField5.Caption = "Entity"
        Me.PivotGridField5.FieldName = "en_desc"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 0
        Me.PivotGridField2.Caption = "Partner"
        Me.PivotGridField2.FieldName = "ptnr_name"
        Me.PivotGridField2.Name = "PivotGridField2"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Amount"
        Me.PivotGridField3.CellFormat.FormatString = "n"
        Me.PivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField3.FieldName = "ap_accrue"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Year"
        Me.PivotGridField1.FieldName = "rcv_eff_date"
        Me.PivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.UnboundFieldName = "PivotGridField1"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "Month"
        Me.PivotGridField4.FieldName = "rcv_eff_date"
        Me.PivotGridField4.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.UnboundFieldName = "PivotGridField4"
        '
        'cu_id
        '
        Me.cu_id.Location = New System.Drawing.Point(264, 7)
        Me.cu_id.Name = "cu_id"
        Me.cu_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cu_id.Size = New System.Drawing.Size(150, 20)
        Me.cu_id.TabIndex = 15
        '
        'aaa
        '
        Me.aaa.Control = Me.cu_id
        Me.aaa.CustomizationFormText = "Currency"
        Me.aaa.Location = New System.Drawing.Point(0, 93)
        Me.aaa.Name = "aaa"
        Me.aaa.Size = New System.Drawing.Size(380, 31)
        Me.aaa.Text = "Currency"
        Me.aaa.TextSize = New System.Drawing.Size(95, 13)
        Me.aaa.TextToControlDistance = 5
        '
        'RBBaseCU
        '
        Me.RBBaseCU.AutoSize = True
        Me.RBBaseCU.Location = New System.Drawing.Point(13, 8)
        Me.RBBaseCU.Name = "RBBaseCU"
        Me.RBBaseCU.Size = New System.Drawing.Size(91, 17)
        Me.RBBaseCU.TabIndex = 16
        Me.RBBaseCU.Text = "Base Curency"
        Me.RBBaseCU.UseVisualStyleBackColor = True
        '
        'RBFilterCU
        '
        Me.RBFilterCU.AutoSize = True
        Me.RBFilterCU.Checked = True
        Me.RBFilterCU.Location = New System.Drawing.Point(109, 8)
        Me.RBFilterCU.Name = "RBFilterCU"
        Me.RBFilterCU.Size = New System.Drawing.Size(89, 17)
        Me.RBFilterCU.TabIndex = 16
        Me.RBFilterCU.TabStop = True
        Me.RBFilterCU.Text = "Filter Curency"
        Me.RBFilterCU.UseVisualStyleBackColor = True
        '
        'FUnvoucheredPOReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(872, 491)
        Me.Name = "FUnvoucheredPOReceipt"
        Me.Text = "Unvouchered PO Receipt"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cu_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aaa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Public WithEvents cu_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents aaa As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RBFilterCU As System.Windows.Forms.RadioButton
    Friend WithEvents RBBaseCU As System.Windows.Forms.RadioButton
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField

End Class
