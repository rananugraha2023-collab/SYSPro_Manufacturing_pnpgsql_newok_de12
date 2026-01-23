<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FImportRouting
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
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_erp = New DevExpress.XtraTab.XtraTabPage
        Me.gc_excel = New DevExpress.XtraGrid.GridControl
        Me.gv_excel = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.xtp_sys = New DevExpress.XtraTab.XtraTabPage
        Me.gc_sys = New DevExpress.XtraGrid.GridControl
        Me.gv_sys = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.sb_migrate = New DevExpress.XtraEditors.SimpleButton
        Me.be_import_xls = New DevExpress.XtraEditors.ButtonEdit
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_erp.SuspendLayout()
        CType(Me.gc_excel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_excel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_sys.SuspendLayout()
        CType(Me.gc_sys, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_sys, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.le_entity)
        Me.scc_master.Panel1.Controls.Add(Me.be_import_xls)
        Me.scc_master.Panel1.Controls.Add(Me.sb_migrate)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.SplitterPosition = 54
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 28)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(85, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Import from Excel"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_erp
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(555, 373)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_erp, Me.xtp_sys})
        '
        'xtp_erp
        '
        Me.xtp_erp.Controls.Add(Me.gc_excel)
        Me.xtp_erp.Name = "xtp_erp"
        Me.xtp_erp.Size = New System.Drawing.Size(553, 372)
        Me.xtp_erp.Text = "ERP"
        '
        'gc_excel
        '
        Me.gc_excel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_excel.Location = New System.Drawing.Point(0, 0)
        Me.gc_excel.MainView = Me.gv_excel
        Me.gc_excel.Name = "gc_excel"
        Me.gc_excel.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gc_excel.Size = New System.Drawing.Size(553, 372)
        Me.gc_excel.TabIndex = 0
        Me.gc_excel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_excel})
        '
        'gv_excel
        '
        Me.gv_excel.GridControl = Me.gc_excel
        Me.gv_excel.Name = "gv_excel"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'xtp_sys
        '
        Me.xtp_sys.Controls.Add(Me.gc_sys)
        Me.xtp_sys.Name = "xtp_sys"
        Me.xtp_sys.Size = New System.Drawing.Size(553, 372)
        Me.xtp_sys.Text = "Syspro"
        '
        'gc_sys
        '
        Me.gc_sys.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_sys.Location = New System.Drawing.Point(0, 0)
        Me.gc_sys.MainView = Me.gv_sys
        Me.gc_sys.Name = "gc_sys"
        Me.gc_sys.Size = New System.Drawing.Size(553, 372)
        Me.gc_sys.TabIndex = 0
        Me.gc_sys.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_sys})
        '
        'gv_sys
        '
        Me.gv_sys.GridControl = Me.gc_sys
        Me.gv_sys.Name = "gv_sys"
        '
        'sb_migrate
        '
        Me.sb_migrate.Location = New System.Drawing.Point(325, 22)
        Me.sb_migrate.Name = "sb_migrate"
        Me.sb_migrate.Size = New System.Drawing.Size(108, 23)
        Me.sb_migrate.TabIndex = 2
        Me.sb_migrate.Text = "Migrate To Syspro"
        '
        'be_import_xls
        '
        Me.be_import_xls.Location = New System.Drawing.Point(103, 25)
        Me.be_import_xls.Name = "be_import_xls"
        Me.be_import_xls.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_import_xls.Size = New System.Drawing.Size(205, 20)
        Me.be_import_xls.TabIndex = 3
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(103, 3)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(128, 20)
        Me.le_entity.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(12, 7)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Entity"
        '
        'FImportRouting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FImportRouting"
        Me.Text = "Import Routing"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_erp.ResumeLayout(False)
        CType(Me.gc_excel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_excel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_sys.ResumeLayout(False)
        CType(Me.gc_sys, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_sys, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_import_xls.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_erp As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_excel As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_excel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents xtp_sys As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_sys As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_sys As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sb_migrate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents be_import_xls As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit

End Class
