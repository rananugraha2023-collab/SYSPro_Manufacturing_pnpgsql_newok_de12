<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FChartSalesYearMultiSeries
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
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim PointOptions1 As DevExpress.XtraCharts.PointOptions = New DevExpress.XtraCharts.PointOptions
        Dim SideBySideBarSeriesLabel2 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel
        Dim ChartTitle1 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle
        Me.pr_txtyear = New DevExpress.XtraEditors.DateEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.be_so_code = New DevExpress.XtraEditors.ButtonEdit
        Me.ce_close_transaction = New DevExpress.XtraEditors.CheckEdit
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ChartControl1 = New DevExpress.XtraCharts.ChartControl
        Me.checkEditShowLabels = New DevExpress.XtraEditors.CheckEdit
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl
        Me.be_server = New DevExpress.XtraEditors.ButtonEdit
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txtyear.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_txtyear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.be_so_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_close_transaction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkEditShowLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_server.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.ChartControl1)
        Me.xtp_data.Size = New System.Drawing.Size(992, 714)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.be_server)
        Me.scc_master.Panel1.Controls.Add(Me.checkEditShowLabels)
        Me.scc_master.Panel1.Controls.Add(Me.pr_txtyear)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Size = New System.Drawing.Size(994, 748)
        Me.scc_master.SplitterPosition = 27
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 365)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 305)
        '
        'xtc_master
        '
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(994, 715)
        '
        'pr_txtyear
        '
        Me.pr_txtyear.EditValue = Nothing
        Me.pr_txtyear.Location = New System.Drawing.Point(52, 3)
        Me.pr_txtyear.Name = "pr_txtyear"
        Me.pr_txtyear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_txtyear.Properties.DisplayFormat.FormatString = "yyyy"
        Me.pr_txtyear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.pr_txtyear.Properties.EditFormat.FormatString = "yyyy"
        Me.pr_txtyear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.pr_txtyear.Properties.Mask.EditMask = "yyyy"
        Me.pr_txtyear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.pr_txtyear.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pr_txtyear.Size = New System.Drawing.Size(100, 20)
        Me.pr_txtyear.TabIndex = 17
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(10, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl1.TabIndex = 16
        Me.LabelControl1.Text = "Year"
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.be_so_code)
        Me.lci_master.Controls.Add(Me.ce_close_transaction)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem16, Me.LayoutControlItem26})
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 305)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 3
        Me.lci_master.Text = "LayoutControl1"
        '
        'be_so_code
        '
        Me.be_so_code.Location = New System.Drawing.Point(124, 72)
        Me.be_so_code.Name = "be_so_code"
        Me.be_so_code.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_so_code.Properties.ReadOnly = True
        Me.be_so_code.Size = New System.Drawing.Size(360, 20)
        Me.be_so_code.StyleController = Me.lci_master
        Me.be_so_code.TabIndex = 59
        '
        'ce_close_transaction
        '
        Me.ce_close_transaction.Location = New System.Drawing.Point(19, 72)
        Me.ce_close_transaction.Name = "ce_close_transaction"
        Me.ce_close_transaction.Properties.Caption = "Close Transaction"
        Me.ce_close_transaction.Size = New System.Drawing.Size(942, 19)
        Me.ce_close_transaction.StyleController = Me.lci_master
        Me.ce_close_transaction.TabIndex = 60
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.be_so_code
        Me.LayoutControlItem16.CustomizationFormText = "SO Number"
        Me.LayoutControlItem16.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(476, 31)
        Me.LayoutControlItem16.Text = "SO Number"
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem16.TextToControlDistance = 5
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.Control = Me.ce_close_transaction
        Me.LayoutControlItem26.CustomizationFormText = "LayoutControlItem26"
        Me.LayoutControlItem26.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(953, 30)
        Me.LayoutControlItem26.Text = "LayoutControlItem26"
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem26.TextToControlDistance = 0
        Me.LayoutControlItem26.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 305)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ChartControl1
        '
        XyDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = True
        XyDiagram1.AxisX.Range.SideMarginsEnabled = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = True
        XyDiagram1.AxisY.Range.SideMarginsEnabled = True
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.ChartControl1.Diagram = XyDiagram1
        Me.ChartControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChartControl1.Location = New System.Drawing.Point(5, 5)
        Me.ChartControl1.Name = "ChartControl1"
        SideBySideBarSeriesLabel1.LineVisible = True
        Series1.Label = SideBySideBarSeriesLabel1
        PointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Currency
        Series1.LegendPointOptions = PointOptions1
        Series1.Name = "Series 1"
        Series1.SynchronizePointOptions = False
        Me.ChartControl1.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1}
        SideBySideBarSeriesLabel2.LineVisible = True
        Me.ChartControl1.SeriesTemplate.Label = SideBySideBarSeriesLabel2
        Me.ChartControl1.Size = New System.Drawing.Size(982, 704)
        Me.ChartControl1.TabIndex = 0
        ChartTitle1.Text = "Sales By Month (Million)"
        Me.ChartControl1.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle1})
        '
        'checkEditShowLabels
        '
        Me.checkEditShowLabels.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.checkEditShowLabels.EditValue = True
        Me.checkEditShowLabels.Location = New System.Drawing.Point(6062, 4)
        Me.checkEditShowLabels.Margin = New System.Windows.Forms.Padding(10, 10, 0, 0)
        Me.checkEditShowLabels.Name = "checkEditShowLabels"
        Me.checkEditShowLabels.Properties.Caption = "Show Labels"
        Me.checkEditShowLabels.Size = New System.Drawing.Size(82, 19)
        Me.checkEditShowLabels.TabIndex = 23
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(184, 5)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(32, 13)
        Me.LabelControl5.TabIndex = 41
        Me.LabelControl5.Text = "Server"
        '
        'be_server
        '
        Me.be_server.Location = New System.Drawing.Point(237, 3)
        Me.be_server.Name = "be_server"
        Me.be_server.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_server.Size = New System.Drawing.Size(198, 20)
        Me.be_server.TabIndex = 24
        '
        'FChartSalesYearMultiSeries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(994, 748)
        Me.Controls.Add(Me.LabelControl5)
        Me.Name = "FChartSalesYearMultiSeries"
        Me.Text = "Sales By Month (Multi Series)"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.Controls.SetChildIndex(Me.LabelControl5, 0)
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txtyear.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_txtyear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.be_so_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_close_transaction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkEditShowLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_server.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pr_txtyear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ce_close_transaction As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents be_so_code As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ChartControl1 As DevExpress.XtraCharts.ChartControl
    Protected WithEvents checkEditShowLabels As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents be_server As DevExpress.XtraEditors.ButtonEdit

End Class
