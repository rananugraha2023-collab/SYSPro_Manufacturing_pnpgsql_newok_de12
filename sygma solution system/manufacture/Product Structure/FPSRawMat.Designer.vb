<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPSRawMat
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
        Me.components = New System.ComponentModel.Container
        Dim PivotGridCustomTotal1 As DevExpress.XtraPivotGrid.PivotGridCustomTotal = New DevExpress.XtraPivotGrid.PivotGridCustomTotal
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPSRawMat))
        Me.PivotGridField2 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.pgc_master = New DevExpress.XtraPivotGrid.PivotGridControl
        Me.PivotGridField1 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField3 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField4 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField5 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField6 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.PivotGridField7 = New DevExpress.XtraPivotGrid.PivotGridField
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_account = New DevExpress.XtraTab.XtraTabPage
        Me.scc_account = New DevExpress.XtraEditors.SplitContainerControl
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.be_to = New DevExpress.XtraEditors.ButtonEdit
        Me.be_from = New DevExpress.XtraEditors.ButtonEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.sb_delete = New DevExpress.XtraEditors.SimpleButton
        Me.sb_add_save = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.te_quan = New DevExpress.XtraEditors.TextEdit
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_account.SuspendLayout()
        CType(Me.scc_account, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_account.SuspendLayout()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_quan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.pgc_master)
        Me.xtp_data.Size = New System.Drawing.Size(860, 240)
        '
        'scc_master
        '
        Me.scc_master.Size = New System.Drawing.Size(862, 267)
        Me.scc_master.SplitterPosition = 0
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(862, 261)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'PivotGridField2
        '
        Me.PivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField2.AreaIndex = 1
        Me.PivotGridField2.Caption = "Standard Cost"
        Me.PivotGridField2.CellFormat.FormatString = "n"
        Me.PivotGridField2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        PivotGridCustomTotal1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count
        Me.PivotGridField2.CustomTotals.AddRange(New DevExpress.XtraPivotGrid.PivotGridCustomTotal() {PivotGridCustomTotal1})
        Me.PivotGridField2.FieldName = "psd_cost"
        Me.PivotGridField2.GrandTotalCellFormat.FormatString = "-"
        Me.PivotGridField2.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.PivotGridField2.GrandTotalText = "-"
        Me.PivotGridField2.Name = "PivotGridField2"
        Me.PivotGridField2.RunningTotal = True
        Me.PivotGridField2.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Average
        '
        'pgc_master
        '
        Me.pgc_master.Cursor = System.Windows.Forms.Cursors.Default
        Me.pgc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pgc_master.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.PivotGridField1, Me.PivotGridField2, Me.PivotGridField3, Me.PivotGridField4, Me.PivotGridField5, Me.PivotGridField6, Me.PivotGridField7})
        Me.pgc_master.Location = New System.Drawing.Point(5, 5)
        Me.pgc_master.Name = "pgc_master"
        Me.pgc_master.Size = New System.Drawing.Size(850, 230)
        Me.pgc_master.TabIndex = 0
        '
        'PivotGridField1
        '
        Me.PivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField1.AreaIndex = 0
        Me.PivotGridField1.Caption = "Qty"
        Me.PivotGridField1.CellFormat.FormatString = "n"
        Me.PivotGridField1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField1.FieldName = "psd_qty"
        Me.PivotGridField1.Name = "PivotGridField1"
        '
        'PivotGridField3
        '
        Me.PivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.PivotGridField3.AreaIndex = 2
        Me.PivotGridField3.Caption = "Ext. Std Cost"
        Me.PivotGridField3.CellFormat.FormatString = "n"
        Me.PivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.PivotGridField3.FieldName = "tot_cost"
        Me.PivotGridField3.Name = "PivotGridField3"
        '
        'PivotGridField4
        '
        Me.PivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField4.AreaIndex = 0
        Me.PivotGridField4.Caption = "Raw Material"
        Me.PivotGridField4.FieldName = "psd_comp"
        Me.PivotGridField4.Name = "PivotGridField4"
        '
        'PivotGridField5
        '
        Me.PivotGridField5.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField5.AreaIndex = 1
        Me.PivotGridField5.Caption = "Description1"
        Me.PivotGridField5.FieldName = "pt_desc1"
        Me.PivotGridField5.Name = "PivotGridField5"
        '
        'PivotGridField6
        '
        Me.PivotGridField6.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.PivotGridField6.AreaIndex = 2
        Me.PivotGridField6.Caption = "Description2"
        Me.PivotGridField6.FieldName = "pt_desc2"
        Me.PivotGridField6.Name = "PivotGridField6"
        '
        'PivotGridField7
        '
        Me.PivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.PivotGridField7.AreaIndex = 0
        Me.PivotGridField7.Caption = "Finish Good Subsystem"
        Me.PivotGridField7.FieldName = "ps_par"
        Me.PivotGridField7.Name = "PivotGridField7"
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
        Me.dp_detail.ID = New System.Guid("321e9940-0d71-4277-b541-7ee338e14617")
        Me.dp_detail.Location = New System.Drawing.Point(0, 267)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.OriginalSize = New System.Drawing.Size(200, 200)
        Me.dp_detail.Size = New System.Drawing.Size(862, 200)
        Me.dp_detail.Text = "Data"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(856, 172)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_account
        Me.xtc_detail.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_detail.Size = New System.Drawing.Size(856, 172)
        Me.xtc_detail.TabIndex = 2
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_account})
        '
        'xtp_account
        '
        Me.xtp_account.Controls.Add(Me.scc_account)
        Me.xtp_account.Name = "xtp_account"
        Me.xtp_account.Size = New System.Drawing.Size(854, 171)
        Me.xtp_account.Text = "Account"
        '
        'scc_account
        '
        Me.scc_account.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_account.Location = New System.Drawing.Point(0, 0)
        Me.scc_account.Name = "scc_account"
        Me.scc_account.Panel1.Controls.Add(Me.gc_master)
        Me.scc_account.Panel1.Text = "SplitContainerControl1_Panel1"
        Me.scc_account.Panel2.Controls.Add(Me.be_to)
        Me.scc_account.Panel2.Controls.Add(Me.be_from)
        Me.scc_account.Panel2.Controls.Add(Me.LabelControl2)
        Me.scc_account.Panel2.Controls.Add(Me.sb_delete)
        Me.scc_account.Panel2.Controls.Add(Me.sb_add_save)
        Me.scc_account.Panel2.Controls.Add(Me.LabelControl4)
        Me.scc_account.Panel2.Controls.Add(Me.LabelControl3)
        Me.scc_account.Panel2.Controls.Add(Me.LabelControl1)
        Me.scc_account.Panel2.Controls.Add(Me.te_quan)
        Me.scc_account.Panel2.Text = "SplitContainerControl1_Panel2"
        Me.scc_account.Size = New System.Drawing.Size(854, 171)
        Me.scc_account.SplitterPosition = 438
        Me.scc_account.TabIndex = 0
        Me.scc_account.Text = "SplitContainerControl1"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(0, 0)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(438, 171)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'be_to
        '
        Me.be_to.Location = New System.Drawing.Point(192, 54)
        Me.be_to.Name = "be_to"
        Me.be_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_to.Size = New System.Drawing.Size(150, 20)
        Me.be_to.TabIndex = 56
        '
        'be_from
        '
        Me.be_from.Location = New System.Drawing.Point(13, 54)
        Me.be_from.Name = "be_from"
        Me.be_from.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_from.Size = New System.Drawing.Size(150, 20)
        Me.be_from.TabIndex = 55
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(13, 16)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(30, 14)
        Me.LabelControl2.TabIndex = 54
        Me.LabelControl2.Text = "FGSS"
        '
        'sb_delete
        '
        Me.sb_delete.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_delete.Image = CType(resources.GetObject("sb_delete.Image"), System.Drawing.Image)
        Me.sb_delete.Location = New System.Drawing.Point(254, 132)
        Me.sb_delete.Name = "sb_delete"
        Me.sb_delete.Size = New System.Drawing.Size(36, 30)
        Me.sb_delete.TabIndex = 53
        '
        'sb_add_save
        '
        Me.sb_add_save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_add_save.Image = CType(resources.GetObject("sb_add_save.Image"), System.Drawing.Image)
        Me.sb_add_save.Location = New System.Drawing.Point(254, 91)
        Me.sb_add_save.Name = "sb_add_save"
        Me.sb_add_save.Size = New System.Drawing.Size(36, 30)
        Me.sb_add_save.TabIndex = 52
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(198, 35)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(16, 13)
        Me.LabelControl4.TabIndex = 51
        Me.LabelControl4.Text = "To."
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 91)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(42, 13)
        Me.LabelControl3.TabIndex = 50
        Me.LabelControl3.Text = "Quantity"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 35)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 48
        Me.LabelControl1.Text = "From."
        '
        'te_quan
        '
        Me.te_quan.EditValue = "1"
        Me.te_quan.Location = New System.Drawing.Point(13, 110)
        Me.te_quan.Name = "te_quan"
        Me.te_quan.Properties.DisplayFormat.FormatString = "n"
        Me.te_quan.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_quan.Properties.EditFormat.FormatString = "n"
        Me.te_quan.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.te_quan.Properties.MaxLength = 5
        Me.te_quan.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.te_quan.Size = New System.Drawing.Size(130, 20)
        Me.te_quan.TabIndex = 45
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FPSRawMat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 467)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "FPSRawMat"
        Me.Text = "Product Structure Raw Material"
        Me.Controls.SetChildIndex(Me.dp_detail, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_account.ResumeLayout(False)
        CType(Me.scc_account, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_account.ResumeLayout(False)
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_quan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pgc_master As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_account As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents scc_account As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents te_quan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents sb_delete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_add_save As DevExpress.XtraEditors.SimpleButton
    Public WithEvents be_to As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents be_from As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents PivotGridField1 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField3 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField4 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField5 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField6 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField7 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PivotGridField2 As DevExpress.XtraPivotGrid.PivotGridField

End Class
