<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPSTreeReport
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
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_view1 = New DevExpress.XtraTab.XtraTabPage
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.ptnr_id = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.ptnr_name = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.lvl_name = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.lvl_active = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.en_desc = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.ptnr_code = New DevExpress.XtraTreeList.Columns.TreeListColumn
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_view1.SuspendLayout()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1066, 433)
        Me.scc_master.SplitterPosition = 0
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Location = New System.Drawing.Point(214, 3)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 23
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(165, 6)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 22
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Location = New System.Drawing.Point(54, 3)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 21
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(4, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(40, 13)
        Me.LabelControl1.TabIndex = 20
        Me.LabelControl1.Text = "SO Date"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_view1
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(1066, 427)
        Me.xtc_master.TabIndex = 7
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_view1})
        '
        'xtp_view1
        '
        Me.xtp_view1.Controls.Add(Me.TreeList1)
        Me.xtp_view1.Name = "xtp_view1"
        Me.xtp_view1.Size = New System.Drawing.Size(1064, 426)
        Me.xtp_view1.Text = "View1"
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.ptnr_id, Me.ptnr_name, Me.lvl_name, Me.lvl_active, Me.en_desc, Me.ptnr_code})
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "ptnr_id"
        Me.TreeList1.Location = New System.Drawing.Point(0, 0)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.PopulateServiceColumns = True
        Me.TreeList1.OptionsSelection.MultiSelect = True
        Me.TreeList1.OptionsSelection.UseIndicatorForSelection = True
        Me.TreeList1.OptionsView.AutoWidth = False
        Me.TreeList1.ParentFieldName = "ptnr_parent"
        Me.TreeList1.Size = New System.Drawing.Size(1064, 426)
        Me.TreeList1.TabIndex = 1
        '
        'ptnr_id
        '
        Me.ptnr_id.Caption = "ID"
        Me.ptnr_id.FieldName = "ptnr_id"
        Me.ptnr_id.Name = "ptnr_id"
        Me.ptnr_id.Visible = True
        Me.ptnr_id.VisibleIndex = 0
        Me.ptnr_id.Width = 118
        '
        'ptnr_name
        '
        Me.ptnr_name.Caption = "Name"
        Me.ptnr_name.FieldName = "ptnr_name"
        Me.ptnr_name.Name = "ptnr_name"
        Me.ptnr_name.Visible = True
        Me.ptnr_name.VisibleIndex = 1
        Me.ptnr_name.Width = 284
        '
        'lvl_name
        '
        Me.lvl_name.Caption = "Level"
        Me.lvl_name.FieldName = "lvl_name"
        Me.lvl_name.Name = "lvl_name"
        Me.lvl_name.Visible = True
        Me.lvl_name.VisibleIndex = 2
        Me.lvl_name.Width = 169
        '
        'lvl_active
        '
        Me.lvl_active.Caption = "Is Active"
        Me.lvl_active.FieldName = "ptnr_active"
        Me.lvl_active.Name = "lvl_active"
        Me.lvl_active.Visible = True
        Me.lvl_active.VisibleIndex = 3
        '
        'en_desc
        '
        Me.en_desc.Caption = "Entity"
        Me.en_desc.FieldName = "en_desc"
        Me.en_desc.Name = "en_desc"
        Me.en_desc.Visible = True
        Me.en_desc.VisibleIndex = 4
        Me.en_desc.Width = 190
        '
        'ptnr_code
        '
        Me.ptnr_code.Caption = "Sales Code"
        Me.ptnr_code.FieldName = "ptnr_code"
        Me.ptnr_code.Name = "ptnr_code"
        Me.ptnr_code.Visible = True
        Me.ptnr_code.VisibleIndex = 5
        Me.ptnr_code.Width = 118
        '
        'FPSTreeReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1066, 433)
        Me.Name = "FPSTreeReport"
        Me.Text = "Personal Sales Tree Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglakhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txttglawal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_view1.ResumeLayout(False)
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_view1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents ptnr_id As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents ptnr_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents lvl_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents lvl_active As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents en_desc As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents ptnr_code As DevExpress.XtraTreeList.Columns.TreeListColumn

End Class
