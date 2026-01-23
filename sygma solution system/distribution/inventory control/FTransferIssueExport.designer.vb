<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FTransferIssueExport
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
        Me.xtp_header = New DevExpress.XtraTab.XtraTabPage
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.ptsfr_code = New DevExpress.XtraEditors.ButtonEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pr_price_list = New DevExpress.XtraEditors.LookUpEdit
        Me.BtExport = New DevExpress.XtraEditors.SimpleButton
        Me.BtPrintBarcode = New DevExpress.XtraEditors.SimpleButton
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_header.SuspendLayout()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptsfr_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_price_list.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.SimpleButton1)
        Me.scc_master.Panel1.Controls.Add(Me.BtPrintBarcode)
        Me.scc_master.Panel1.Controls.Add(Me.BtExport)
        Me.scc_master.Panel1.Controls.Add(Me.ptsfr_code)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_price_list)
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(935, 433)
        Me.scc_master.SplitterPosition = 59
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.SelectedTabPage = Me.xtp_header
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(935, 368)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_header})
        '
        'xtp_header
        '
        Me.xtp_header.Controls.Add(Me.gc_master)
        Me.xtp_header.Name = "xtp_header"
        Me.xtp_header.Size = New System.Drawing.Size(928, 361)
        Me.xtp_header.Text = "Header"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(0, 0)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(928, 361)
        Me.gc_master.TabIndex = 2
        Me.gc_master.UseEmbeddedNavigator = True
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsView.ShowAutoFilterRow = True
        Me.gv_master.OptionsView.ShowFooter = True
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(67, 5)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(155, 20)
        Me.pr_entity.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Entity"
        '
        'ptsfr_code
        '
        Me.ptsfr_code.Location = New System.Drawing.Point(379, 6)
        Me.ptsfr_code.Name = "ptsfr_code"
        Me.ptsfr_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.ptsfr_code.Size = New System.Drawing.Size(189, 20)
        Me.ptsfr_code.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(256, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Transfer Issue Number"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Price List"
        '
        'pr_price_list
        '
        Me.pr_price_list.Location = New System.Drawing.Point(67, 31)
        Me.pr_price_list.Name = "pr_price_list"
        Me.pr_price_list.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_price_list.Size = New System.Drawing.Size(155, 20)
        Me.pr_price_list.TabIndex = 9
        '
        'BtExport
        '
        Me.BtExport.Location = New System.Drawing.Point(379, 32)
        Me.BtExport.Name = "BtExport"
        Me.BtExport.Size = New System.Drawing.Size(82, 23)
        Me.BtExport.TabIndex = 12
        Me.BtExport.Text = "Export"
        '
        'BtPrintBarcode
        '
        Me.BtPrintBarcode.Location = New System.Drawing.Point(467, 32)
        Me.BtPrintBarcode.Name = "BtPrintBarcode"
        Me.BtPrintBarcode.Size = New System.Drawing.Size(82, 23)
        Me.BtPrintBarcode.TabIndex = 29
        Me.BtPrintBarcode.Text = "Print Barcode"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(555, 32)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(84, 23)
        Me.SimpleButton1.TabIndex = 29
        Me.SimpleButton1.Text = "Print Barcode 2"
        '
        'FTransferIssueExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(935, 433)
        Me.Name = "FTransferIssueExport"
        Me.Text = "Transfer Issue Export"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_header.ResumeLayout(False)
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptsfr_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_price_list.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_header As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ptsfr_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pr_price_list As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BtExport As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtPrintBarcode As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton

End Class
