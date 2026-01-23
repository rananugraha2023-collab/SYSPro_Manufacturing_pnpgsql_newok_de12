<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCFDRetrievePeriod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FCFDRetrievePeriod))
        Me.OdbcSelectCommand1 = New System.Data.Odbc.OdbcCommand
        Me.OdbcConnection1 = New System.Data.Odbc.OdbcConnection
        Me.OdbcDataAdapter1 = New System.Data.Odbc.OdbcDataAdapter
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.le_periode_from = New DevExpress.XtraEditors.LookUpEdit
        Me.le_periode_to = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_periode_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_periode_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.pgc_master)
        Me.xtp_data.Size = New System.Drawing.Size(870, 420)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl3)
        Me.scc_master.Panel1.Controls.Add(Me.le_periode_to)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.le_periode_from)
        Me.scc_master.Panel1.Controls.Add(Me.le_entity)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(872, 491)
        Me.scc_master.SplitterPosition = 44
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(872, 441)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
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
        Me.OdbcDataAdapter1.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "Table", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ptnr_name", "ptnr_name"), New System.Data.Common.DataColumnMapping("ap_type_name", "ap_type_name"), New System.Data.Common.DataColumnMapping("ap_amount", "ap_amount"), New System.Data.Common.DataColumnMapping("ap_pay_amount", "ap_pay_amount"), New System.Data.Common.DataColumnMapping("ap_outstanding", "ap_outstanding")})})
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(46, 15)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Size = New System.Drawing.Size(193, 20)
        Me.le_entity.TabIndex = 18
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 18)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 17
        Me.LabelControl1.Text = "Entity"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(255, 19)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl2.TabIndex = 20
        Me.LabelControl2.Text = "GL Periode"
        '
        'le_periode_from
        '
        Me.le_periode_from.Location = New System.Drawing.Point(314, 15)
        Me.le_periode_from.Name = "le_periode_from"
        Me.le_periode_from.Properties.DisplayFormat.FormatString = "d"
        Me.le_periode_from.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode_from.Properties.EditFormat.FormatString = "d"
        Me.le_periode_from.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode_from.Properties.PopupWidth = 500
        Me.le_periode_from.Size = New System.Drawing.Size(133, 20)
        Me.le_periode_from.TabIndex = 19
        '
        'le_periode_to
        '
        Me.le_periode_to.Location = New System.Drawing.Point(473, 15)
        Me.le_periode_to.Name = "le_periode_to"
        Me.le_periode_to.Properties.DisplayFormat.FormatString = "d"
        Me.le_periode_to.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode_to.Properties.EditFormat.FormatString = "d"
        Me.le_periode_to.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.le_periode_to.Properties.PopupWidth = 500
        Me.le_periode_to.Size = New System.Drawing.Size(133, 20)
        Me.le_periode_to.TabIndex = 21
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(458, 18)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(4, 13)
        Me.LabelControl3.TabIndex = 22
        Me.LabelControl3.Text = "-"
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField5, Me.PivotGridField3})
        Me.pgc_master.Location = New System.Drawing.Point(5, 5)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.OptionsView.ShowRowGrandTotals = False
        Me.pgc_master.Size = New System.Drawing.Size(860, 410)
        Me.pgc_master.TabIndex = 1
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Group"
        Me.PivotGridField1.FieldName = "group_name"
        Me.PivotGridField1.Name = "PivotGridField1"
        Me.PivotGridField1.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "Cashflow Trans."
        Me.PivotGridField2.FieldName = "tranline_name"
        Me.PivotGridField2.Name = "PivotGridField2"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField5.AreaIndex = 0
        Me.PivotGridField5.Caption = "Amount"
        Me.PivotGridField5.CellFormat.FormatString = "n"
        Me.PivotGridField5.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField5.FieldName = "cfd_amount"
        Me.PivotGridField5.Name = "PivotGridField5"
        Me.PivotGridField5.ValueFormat.FormatString = "n"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField3.AreaIndex = 0
        Me.PivotGridField3.Caption = "Month"
        Me.PivotGridField3.FieldName = "_month"
        Me.PivotGridField3.Name = "PivotGridField3"
        Me.PivotGridField3.UnboundFieldName = "PivotGridField3"
        '
        'FCFDRetrievePeriod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(872, 491)
        Me.Name = "FCFDRetrievePeriod"
        Me.Text = "Cashflow Direct Retrieve With Period"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_periode_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_periode_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OdbcSelectCommand1 As System.Data.Odbc.OdbcCommand
    Friend WithEvents OdbcDataAdapter1 As System.Data.Odbc.OdbcDataAdapter
    Friend WithEvents OdbcConnection1 As System.Data.Odbc.OdbcConnection
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents le_periode_from As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents le_periode_to As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField

End Class
