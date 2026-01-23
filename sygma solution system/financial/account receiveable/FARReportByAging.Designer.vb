<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FARReportByAging
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
        Me.CBOutstandingType = New System.Windows.Forms.ComboBox
        Me.outstanding_date = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.RBFilterCU = New System.Windows.Forms.RadioButton
        Me.RBBaseCU = New System.Windows.Forms.RadioButton
        Me.cu_id = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_1 = New DevExpress.XtraTab.XtraTabPage
        Me.pgc_ar = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.DsaragingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.Ds_ar_aging = New sygma_solution_system.ds_ar_aging
        Me.fieldptnrname = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldaroutstanding = New DevExpress.XtraPivotGrid.PivotGridField
        Me.fieldarduedate = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.Label1 = New System.Windows.Forms.Label
        Me.en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.Pr_Type = New DevExpress.XtraEditors.ComboBoxEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.CeAllEntity = New DevExpress.XtraEditors.CheckEdit
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
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pr_Type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CeAllEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.CeAllEntity)
        Me.scc_master.Panel1.Controls.Add(Me.Pr_Type)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.en_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel1.Controls.Add(Me.CBOutstandingType)
        Me.scc_master.Panel1.Controls.Add(Me.outstanding_date)
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.Label7)
        Me.scc_master.Panel1.Controls.Add(Me.RBFilterCU)
        Me.scc_master.Panel1.Controls.Add(Me.RBBaseCU)
        Me.scc_master.Panel1.Controls.Add(Me.cu_id)
        Me.scc_master.Panel1.Controls.Add(Me.Label4)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1013, 433)
        Me.scc_master.SplitterPosition = 56
        '
        'CBOutstandingType
        '
        Me.CBOutstandingType.FormattingEnabled = True
        Me.CBOutstandingType.Items.AddRange(New Object() {"Before Outstanding Date", "After Outstanding Date", "All Data", "Between Date"})
        Me.CBOutstandingType.Location = New System.Drawing.Point(538, 29)
        Me.CBOutstandingType.Name = "CBOutstandingType"
        Me.CBOutstandingType.Size = New System.Drawing.Size(100, 21)
        Me.CBOutstandingType.TabIndex = 27
        Me.CBOutstandingType.Text = "All Data"
        '
        'outstanding_date
        '
        Me.outstanding_date.CustomFormat = ""
        Me.outstanding_date.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.outstanding_date.Location = New System.Drawing.Point(330, 30)
        Me.outstanding_date.Name = "outstanding_date"
        Me.outstanding_date.Size = New System.Drawing.Size(100, 20)
        Me.outstanding_date.TabIndex = 26
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(440, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Outstanding Type"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(234, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Outstanding Date"
        '
        'RBFilterCU
        '
        Me.RBFilterCU.AutoSize = True
        Me.RBFilterCU.Checked = True
        Me.RBFilterCU.Location = New System.Drawing.Point(330, 7)
        Me.RBFilterCU.Name = "RBFilterCU"
        Me.RBFilterCU.Size = New System.Drawing.Size(89, 17)
        Me.RBFilterCU.TabIndex = 22
        Me.RBFilterCU.TabStop = True
        Me.RBFilterCU.Text = "Filter Curency"
        Me.RBFilterCU.UseVisualStyleBackColor = True
        '
        'RBBaseCU
        '
        Me.RBBaseCU.AutoSize = True
        Me.RBBaseCU.Location = New System.Drawing.Point(234, 7)
        Me.RBBaseCU.Name = "RBBaseCU"
        Me.RBBaseCU.Size = New System.Drawing.Size(91, 17)
        Me.RBBaseCU.TabIndex = 23
        Me.RBBaseCU.Text = "Base Curency"
        Me.RBBaseCU.UseVisualStyleBackColor = True
        '
        'cu_id
        '
        Me.cu_id.Location = New System.Drawing.Point(538, 6)
        Me.cu_id.Name = "cu_id"
        Me.cu_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cu_id.Size = New System.Drawing.Size(100, 20)
        Me.cu_id.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(440, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 20
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
        Me.xtc_master.Size = New System.Drawing.Size(1013, 371)
        Me.xtc_master.TabIndex = 3
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_1})
        '
        'xtp_1
        '
        Me.xtp_1.Controls.Add(Me.pgc_ar)
        Me.xtp_1.Name = "xtp_1"
        Me.xtp_1.Size = New System.Drawing.Size(1011, 370)
        Me.xtp_1.Text = "Data"
        '
        'pgc_ar
        '
        Me.pgc_ar.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_ar.DataMember = "DataTable1"
        Me.pgc_ar.DataSource = Me.DsaragingBindingSource
        Me.pgc_ar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_ar.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.fieldptnrname, Me.fieldaroutstanding, Me.fieldarduedate, Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5})
        Me.pgc_ar.Location = New System.Drawing.Point(0, 0)
        Me.pgc_ar.Name = "pgc_ar"
        Me.pgc_ar.Size = New System.Drawing.Size(1011, 370)
        Me.pgc_ar.TabIndex = 0
        '
        'DsaragingBindingSource
        '
        'Me.DsaragingBindingSource.DataSource = Me.Ds_ar_aging
        Me.DsaragingBindingSource.Position = 0
        '
        'Ds_ar_aging
        '
        'Me.Ds_ar_aging.DataSetName = "ds_ar_aging"
        'Me.Ds_ar_aging.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        ''
        'fieldptnrname
        '
        Me.fieldptnrname.AreaIndex = 3
        Me.fieldptnrname.Caption = "Customer"
        Me.fieldptnrname.FieldName = "ptnr_name"
        Me.fieldptnrname.Name = "fieldptnrname"
        '
        'fieldaroutstanding
        '
        Me.fieldaroutstanding.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.fieldaroutstanding.AreaIndex = 0
        Me.fieldaroutstanding.Caption = "Outstanding"
        Me.fieldaroutstanding.CellFormat.FormatString = "n"
        Me.fieldaroutstanding.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldaroutstanding.FieldName = "ar_outstanding"
        Me.fieldaroutstanding.GrandTotalCellFormat.FormatString = "n"
        Me.fieldaroutstanding.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldaroutstanding.Name = "fieldaroutstanding"
        Me.fieldaroutstanding.TotalCellFormat.FormatString = "n"
        Me.fieldaroutstanding.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldaroutstanding.TotalValueFormat.FormatString = "n"
        Me.fieldaroutstanding.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.fieldaroutstanding.ValueFormat.FormatString = "n"
        Me.fieldaroutstanding.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '
        'fieldarduedate
        '
        Me.fieldarduedate.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.fieldarduedate.AreaIndex = 0
        Me.fieldarduedate.Caption = "Year"
        Me.fieldarduedate.FieldName = "ar_due_date"
        Me.fieldarduedate.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
        Me.fieldarduedate.Name = "fieldarduedate"
        Me.fieldarduedate.UnboundFieldName = "fieldarduedate"
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField1.AreaIndex = 1
        Me.PivotGridField1.Caption = "Month"
        Me.PivotGridField1.FieldName = "ar_due_date"
        Me.PivotGridField1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.UnboundFieldName = "PivotGridField1"
        '
        'PivotGridField2
        '
        Me.PivotGridField2.AreaIndex = 2
        Me.PivotGridField2.Caption = "Week"
        Me.PivotGridField2.FieldName = "ar_due_date"
        Me.PivotGridField2.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateWeekOfMonth
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.UnboundFieldName = "PivotGridField2"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "AR Code"
        Me.PivotGridField3.FieldName = "ar_code"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.AreaIndex = 1
        Me.PivotGridField4.Caption = "AR Date"
        Me.PivotGridField4.FieldName = "ar_date"
        Me.PivotGridField4.Name = "PivotGridField4"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Entity"
        Me.PivotGridField5.FieldName = "en_desc"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Entity"
        '
        'en_id
        '
        Me.en_id.Location = New System.Drawing.Point(54, 24)
        Me.en_id.Name = "en_id"
        Me.en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.en_id.Size = New System.Drawing.Size(164, 20)
        Me.en_id.TabIndex = 29
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(879, 28)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 33
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(825, 31)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 32
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(879, 5)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 31
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(824, 8)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 30
        Me.LabelControl1.Text = "First Date"
        '
        'Pr_Type
        '
        Me.Pr_Type.EditValue = "Effective Date"
        Me.Pr_Type.Location = New System.Drawing.Point(707, 6)
        Me.Pr_Type.Name = "Pr_Type"
        Me.Pr_Type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.Pr_Type.Properties.Items.AddRange(New Object() {"Effective Date", "Due Date"})
        Me.Pr_Type.Size = New System.Drawing.Size(100, 20)
        Me.Pr_Type.TabIndex = 34
        Me.Pr_Type.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(650, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Date Type"
        '
        'CeAllEntity
        '
        Me.CeAllEntity.EditValue = True
        Me.CeAllEntity.Location = New System.Drawing.Point(52, 3)
        Me.CeAllEntity.Name = "CeAllEntity"
        Me.CeAllEntity.Properties.Caption = "All Entity"
        Me.CeAllEntity.Size = New System.Drawing.Size(75, 19)
        Me.CeAllEntity.TabIndex = 35
        '
        'FARReportByAging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1013, 433)
        Me.Name = "FARReportByAging"
        Me.Text = "AR Report By Aging"
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
        CType(Me.pgc_ar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsaragingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.Ds_ar_aging, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pr_Type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CeAllEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CBOutstandingType As System.Windows.Forms.ComboBox
    Friend WithEvents outstanding_date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents RBFilterCU As System.Windows.Forms.RadioButton
    Friend WithEvents RBBaseCU As System.Windows.Forms.RadioButton
    Public WithEvents cu_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents pgc_ar As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents DsaragingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Ds_ar_aging As sygma_solution_system.ds_ar_aging
    Friend WithEvents fieldptnrname As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldaroutstanding As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents fieldarduedate As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Public WithEvents en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Pr_Type As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CeAllEntity As DevExpress.XtraEditors.CheckEdit

End Class
