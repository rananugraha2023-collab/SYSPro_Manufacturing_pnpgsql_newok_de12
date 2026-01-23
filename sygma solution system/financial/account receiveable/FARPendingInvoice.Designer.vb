<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FARPendingInvoice
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
        Me.RBFilterCU = New System.Windows.Forms.RadioButton
        Me.RBBaseCU = New System.Windows.Forms.RadioButton
        Me.cu_id = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_1 = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cu_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_1.SuspendLayout()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.RBFilterCU)
        Me.scc_master.Panel1.Controls.Add(Me.RBBaseCU)
        Me.scc_master.Panel1.Controls.Add(Me.cu_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 28
        '
        'RBFilterCU
        '
        Me.RBFilterCU.AutoSize = True
        Me.RBFilterCU.Checked = True
        Me.RBFilterCU.Location = New System.Drawing.Point(100, 5)
        Me.RBFilterCU.Name = "RBFilterCU"
        Me.RBFilterCU.Size = New System.Drawing.Size(89, 17)
        Me.RBFilterCU.TabIndex = 19
        Me.RBFilterCU.TabStop = True
        Me.RBFilterCU.Text = "Filter Curency"
        Me.RBFilterCU.UseVisualStyleBackColor = True
        '
        'RBBaseCU
        '
        Me.RBBaseCU.AutoSize = True
        Me.RBBaseCU.Location = New System.Drawing.Point(4, 5)
        Me.RBBaseCU.Name = "RBBaseCU"
        Me.RBBaseCU.Size = New System.Drawing.Size(91, 17)
        Me.RBBaseCU.TabIndex = 20
        Me.RBBaseCU.Text = "Base Curency"
        Me.RBBaseCU.UseVisualStyleBackColor = True
        '
        'cu_id
        '
        Me.cu_id.Location = New System.Drawing.Point(255, 4)
        Me.cu_id.Name = "cu_id"
        Me.cu_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cu_id.Size = New System.Drawing.Size(150, 20)
        Me.cu_id.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(200, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Currency"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_1
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(555, 358)
        Me.xtc_master.TabIndex = 4
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.pgc_master)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(553, 357)
        Me.xtp_1.Text = "Data"
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField5, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField1, Me.PivotGridField4})
        Me.pgc_master.Location = New System.Drawing.Point(0, 0)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.Size = New System.Drawing.Size(553, 357)
        Me.pgc_master.TabIndex = 1
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
        Me.PivotGridField3.FieldName = "ar_accrue"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Year"
        Me.PivotGridField1.FieldName = "soship_date"
        Me.PivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.UnboundFieldName = "PivotGridField1"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "Month"
        Me.PivotGridField4.FieldName = "soship_date"
        Me.PivotGridField4.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.PivotGridField4.Name = "PivotGridField4"
        Me.PivotGridField4.UnboundFieldName = "PivotGridField4"
        '
        'FARPendingInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FARPendingInvoice"
        Me.Text = "Pending Invoice"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cu_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_1.ResumeLayout(False)
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RBFilterCU As System.Windows.Forms.RadioButton
    Friend WithEvents RBBaseCU As System.Windows.Forms.RadioButton
    Public WithEvents cu_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField

End Class
