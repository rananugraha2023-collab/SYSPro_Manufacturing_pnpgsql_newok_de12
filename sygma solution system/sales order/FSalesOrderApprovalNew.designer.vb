<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSalesOrderApprovalNew
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
        Me.pr_txttglakhir = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.pr_txttglawal = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_os = New DevExpress.XtraTab.XtraTabPage
        Me.gc_os = New DevExpress.XtraGrid.GridControl
        Me.gv_os = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.col_select = New DevExpress.XtraGrid.Columns.GridColumn
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
        Me.xtp_all = New DevExpress.XtraTab.XtraTabPage
        Me.gc_all = New DevExpress.XtraGrid.GridControl
        Me.gv_all = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_data_detail = New DevExpress.XtraTab.XtraTabPage
        Me.scc_detail = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_detail = New DevExpress.XtraGrid.GridControl
        Me.gv_detail = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.BtApprove = New DevExpress.XtraEditors.SimpleButton
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
        Me.xtp_os.SuspendLayout()
        CType(Me.gc_os, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_os, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_all.SuspendLayout()
        CType(Me.gc_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_all, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_data_detail.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.BtApprove)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglakhir)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txttglawal)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(901, 304)
        Me.scc_master.SplitterPosition = 28
        '
        'pr_txttglakhir
        '
        Me.pr_txttglakhir.EditValue = Nothing
        Me.pr_txttglakhir.Enabled = False
        Me.pr_txttglakhir.Location = New System.Drawing.Point(217, 4)
        Me.pr_txttglakhir.Name = "pr_txttglakhir"
        Me.pr_txttglakhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglakhir.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglakhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglakhir.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglakhir.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(167, 7)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(46, 13)
        Me.LabelControl2.TabIndex = 14
        Me.LabelControl2.Text = "Last Date"
        '
        'pr_txttglawal
        '
        Me.pr_txttglawal.EditValue = Nothing
        Me.pr_txttglawal.Enabled = False
        Me.pr_txttglawal.Location = New System.Drawing.Point(57, 4)
        Me.pr_txttglawal.Name = "pr_txttglawal"
        Me.pr_txttglawal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txttglawal.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txttglawal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txttglawal.Size = New System.Drawing.Size(100, 20)
        Me.pr_txttglawal.TabIndex = 13
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(6, 7)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(47, 13)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "First Date"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_os
        Me.xtc_master.Size = New System.Drawing.Size(901, 270)
        Me.xtc_master.TabIndex = 2
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_os, Me.xtp_all})
        '
        'xtp_os
        '
        Me.xtp_os.Controls.Add(Me.gc_os)
        Me.xtp_os.Name = "xtp_os"
        Me.xtp_os.Size = New System.Drawing.Size(899, 249)
        Me.xtp_os.Text = "Out Standing Data"
        '
        'gc_os
        '
        Me.gc_os.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_os.Location = New System.Drawing.Point(0, 0)
        Me.gc_os.MainView = Me.gv_os
        Me.gc_os.Name = "gc_os"
        Me.gc_os.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.gc_os.Size = New System.Drawing.Size(899, 249)
        Me.gc_os.TabIndex = 1
        Me.gc_os.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_os})
        '
        'gv_os
        '
        Me.gv_os.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.col_select})
        Me.gv_os.GridControl = Me.gc_os
        Me.gv_os.Name = "gv_os"
        '
        'col_select
        '
        Me.col_select.Caption = "#"
        Me.col_select.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.col_select.FieldName = "status"
        Me.col_select.Name = "col_select"
        Me.col_select.Visible = True
        Me.col_select.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'xtp_all
        '
        Me.xtp_all.Controls.Add(Me.gc_all)
        Me.xtp_all.Name = "xtp_all"
        Me.xtp_all.Size = New System.Drawing.Size(899, 249)
        Me.xtp_all.Text = "All Data"
        '
        'gc_all
        '
        Me.gc_all.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_all.Location = New System.Drawing.Point(0, 0)
        Me.gc_all.MainView = Me.gv_all
        Me.gc_all.Name = "gc_all"
        Me.gc_all.Size = New System.Drawing.Size(899, 249)
        Me.gc_all.TabIndex = 1
        Me.gc_all.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_all})
        '
        'gv_all
        '
        Me.gv_all.GridControl = Me.gc_all
        Me.gv_all.Name = "gv_all"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.dp_detail})
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'dp_detail
        '
        Me.dp_detail.Controls.Add(Me.DockPanel1_Container)
        Me.dp_detail.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom
        Me.dp_detail.ID = New System.Guid("60ab2a42-a6b3-4662-b496-769724990f49")
        Me.dp_detail.Location = New System.Drawing.Point(0, 304)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 200)
        Me.dp_detail.Size = New System.Drawing.Size(901, 200)
        Me.dp_detail.Text = "Data Detail"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(895, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_data_detail
        Me.xtc_detail.Size = New System.Drawing.Size(895, 172)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_data_detail})
        '
        'xtp_data_detail
        '
        Me.xtp_data_detail.Controls.Add(Me.scc_detail)
        Me.xtp_data_detail.Name = "xtp_data_detail"
        Me.xtp_data_detail.Size = New System.Drawing.Size(893, 151)
        Me.xtp_data_detail.Text = "Data Detail"
        '
        'scc_detail
        '
        Me.scc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_detail.Location = New System.Drawing.Point(0, 0)
        Me.scc_detail.Name = "scc_detail"
        Me.scc_detail.Panel1.Controls.Add(Me.gc_detail)
        Me.scc_detail.Panel1.Text = "Panel1"
        Me.scc_detail.Panel2.Text = "Panel2"
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(893, 151)
        Me.scc_detail.TabIndex = 1
        Me.scc_detail.Text = "SplitContainerControl1"
        '
        'gc_detail
        '
        Me.gc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_detail.Location = New System.Drawing.Point(0, 0)
        Me.gc_detail.MainView = Me.gv_detail
        Me.gc_detail.Name = "gc_detail"
        Me.gc_detail.Size = New System.Drawing.Size(893, 151)
        Me.gc_detail.TabIndex = 1
        Me.gc_detail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_detail})
        '
        'gv_detail
        '
        Me.gv_detail.GridControl = Me.gc_detail
        Me.gv_detail.Name = "gv_detail"
        Me.gv_detail.OptionsView.ColumnAutoWidth = False
        Me.gv_detail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.gv_detail.OptionsView.ShowGroupPanel = False
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'BtApprove
        '
        Me.BtApprove.Location = New System.Drawing.Point(332, 3)
        Me.BtApprove.Name = "BtApprove"
        Me.BtApprove.Size = New System.Drawing.Size(75, 23)
        Me.BtApprove.TabIndex = 16
        Me.BtApprove.Text = "Approve"
        '
        'FSalesOrderApprovalNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(901, 504)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FSalesOrderApprovalNew"
        Me.Text = "Sales Order Approval"
        Me.Controls.SetChildIndex(Me.dp_detail, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
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
        Me.xtp_os.ResumeLayout(False)
        CType(Me.gc_os, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_os, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_all.ResumeLayout(False)
        CType(Me.gc_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_all, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_data_detail.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.gc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_detail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_txttglakhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_txttglawal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_os As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents xtp_all As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_all As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_all As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gc_os As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_os As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents col_select As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents xtp_data_detail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_detail As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_detail As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_detail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtApprove As DevExpress.XtraEditors.SimpleButton

End Class
