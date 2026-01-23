<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FARReportByTop
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
        Me.CBoTop = New System.Windows.Forms.ComboBox
        Me.CBPaid = New System.Windows.Forms.CheckBox
        Me.RBFilterCU = New System.Windows.Forms.RadioButton
        Me.RBBaseCU = New System.Windows.Forms.RadioButton
        Me.cu_id = New DevExpress.XtraEditors.LookUpEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_1 = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
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
        Me.scc_master.Panel1.Controls.Add(Me.CBoTop)
        Me.scc_master.Panel1.Controls.Add(Me.CBPaid)
        Me.scc_master.Panel1.Controls.Add(Me.RBFilterCU)
        Me.scc_master.Panel1.Controls.Add(Me.RBBaseCU)
        Me.scc_master.Panel1.Controls.Add(Me.cu_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 60
        '
        'CBoTop
        '
        Me.CBoTop.FormattingEnabled = True
        Me.CBoTop.Items.AddRange(New Object() {"5", "10", "15", "20", "25", "30", "50", "100"})
        Me.CBoTop.Location = New System.Drawing.Point(258, 34)
        Me.CBoTop.Name = "CBoTop"
        Me.CBoTop.Size = New System.Drawing.Size(148, 21)
        Me.CBoTop.TabIndex = 27
        Me.CBoTop.Text = "5"
        '
        'CBPaid
        '
        Me.CBPaid.AutoSize = True
        Me.CBPaid.Location = New System.Drawing.Point(9, 34)
        Me.CBPaid.Name = "CBPaid"
        Me.CBPaid.Size = New System.Drawing.Size(52, 17)
        Me.CBPaid.TabIndex = 26
        Me.CBPaid.Text = "Close"
        Me.CBPaid.UseVisualStyleBackColor = True
        '
        'RBFilterCU
        '
        Me.RBFilterCU.AutoSize = True
        Me.RBFilterCU.Checked = True
        Me.RBFilterCU.Location = New System.Drawing.Point(103, 6)
        Me.RBFilterCU.Name = "RBFilterCU"
        Me.RBFilterCU.Size = New System.Drawing.Size(89, 17)
        Me.RBFilterCU.TabIndex = 25
        Me.RBFilterCU.TabStop = True
        Me.RBFilterCU.Text = "Filter Curency"
        Me.RBFilterCU.UseVisualStyleBackColor = True
        '
        'RBBaseCU
        '
        Me.RBBaseCU.AutoSize = True
        Me.RBBaseCU.Location = New System.Drawing.Point(7, 6)
        Me.RBBaseCU.Name = "RBBaseCU"
        Me.RBBaseCU.Size = New System.Drawing.Size(91, 17)
        Me.RBBaseCU.TabIndex = 24
        Me.RBBaseCU.Text = "Base Curency"
        Me.RBBaseCU.UseVisualStyleBackColor = True
        '
        'cu_id
        '
        Me.cu_id.Location = New System.Drawing.Point(258, 5)
        Me.cu_id.Name = "cu_id"
        Me.cu_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cu_id.Size = New System.Drawing.Size(148, 20)
        Me.cu_id.TabIndex = 23
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(203, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Top"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(203, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 22
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
        Me.xtc_master.Size = New System.Drawing.Size(555, 367)
        Me.xtc_master.TabIndex = 4
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.pgc_master)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(553, 366)
        Me.xtp_1.Text = "Data"
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField6, Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5})
        Me.pgc_master.Location = New System.Drawing.Point(0, 0)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.Size = New System.Drawing.Size(553, 366)
        Me.pgc_master.TabIndex = 1
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField6.AreaIndex = 0
        Me.PivotGridField6.Caption = "Entity"
        Me.PivotGridField6.FieldName = "en_desc"
        Me.PivotGridField6.Name = "PivotGridField6"
        '
        'PivotGridField1
        '
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Type"
        Me.PivotGridField1.FieldName = "ar_type_name"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "Partner"
        Me.PivotGridField2.FieldName = "ptnr_name"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.SortBySummaryInfo.Field = Me.PivotGridField3
        Me.PivotGridField2.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Amount"
        Me.PivotGridField3.CellFormat.FormatString = "n"
        Me.PivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField3.FieldName = "ar_amount"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Value
        Me.PivotGridField3.SortOrder = DevExpress.XtraPivotGrid.PivotSortOrder.Descending
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "Pay Amount"
        Me.PivotGridField4.CellFormat.FormatString = "n"
        Me.PivotGridField4.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField4.FieldName = "ar_pay_amount"
        Me.PivotGridField4.Name = "PivotGridField4"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField5.AreaIndex = 2
        Me.PivotGridField5.Caption = "Outstanding"
        Me.PivotGridField5.CellFormat.FormatString = "n"
        Me.PivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.FieldName = "ar_outstanding"
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.ValueFormat.FormatString = "n"
        '
        'FARReportByTop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FARReportByTop"
        Me.Text = "AR Report By TOP"
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
    Friend WithEvents CBoTop As System.Windows.Forms.ComboBox
    Friend WithEvents CBPaid As System.Windows.Forms.CheckBox
    Friend WithEvents RBFilterCU As System.Windows.Forms.RadioButton
    Friend WithEvents RBBaseCU As System.Windows.Forms.RadioButton
    Public WithEvents cu_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField

End Class
