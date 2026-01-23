<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterInfChart2
    Inherits master_new.MasterWork

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
        Me.scc_master = New DevExpress.XtraEditors.SplitContainerControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_data = New DevExpress.XtraTab.XtraTabPage
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.dp_detail = New DevExpress.XtraBars.Docking.DockPanel
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer
        Me.xtc_detail = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_chart = New DevExpress.XtraTab.XtraTabPage
        Me.xtp_configuration = New DevExpress.XtraTab.XtraTabPage
        Me.sb_default = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl
        Me.cb_lbl_position = New DevExpress.XtraEditors.ComboBoxEdit
        Me.ce_value_percent = New DevExpress.XtraEditors.CheckEdit
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl
        Me.cb_explode = New DevExpress.XtraEditors.ComboBoxEdit
        Me.ce_view_label = New DevExpress.XtraEditors.CheckEdit
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.cb_bar_appearance = New DevExpress.XtraEditors.ComboBoxEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.cb_chart_appearance = New DevExpress.XtraEditors.ComboBoxEdit
        Me.lbl_chart_view = New DevExpress.XtraEditors.LabelControl
        Me.cb_chart_view = New DevExpress.XtraEditors.ComboBoxEdit
        Me.ToolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dp_detail.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_detail.SuspendLayout()
        Me.xtp_configuration.SuspendLayout()
        CType(Me.cb_lbl_position.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_value_percent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_explode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_view_label.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_bar_appearance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_chart_appearance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cb_chart_view.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_master.Horizontal = False
        Me.scc_master.Location = New System.Drawing.Point(0, 0)
        Me.scc_master.Name = "scc_master"
        Me.scc_master.Panel1.ShowCaption = True
        Me.scc_master.Panel1.Text = "Retrieve Data By : "
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(771, 239)
        Me.scc_master.SplitterPosition = 69
        Me.scc_master.TabIndex = 3
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_data
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(767, 160)
        Me.xtc_master.TabIndex = 1
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_data})
        '
        'xtp_data
        '
        Me.xtp_data.Name = "xtp_data"
        Me.xtp_data.Padding = New System.Windows.Forms.Padding(5)
        Me.xtp_data.Size = New System.Drawing.Size(765, 159)
        Me.xtp_data.Text = "Data"
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
        Me.dp_detail.ID = New System.Guid("8cd47d92-d651-49c6-a83d-77fbc71ea49c")
        Me.dp_detail.Location = New System.Drawing.Point(0, 239)
        Me.dp_detail.Name = "dp_detail"
        Me.dp_detail.Size = New System.Drawing.Size(771, 194)
        Me.dp_detail.Text = "Chart"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.xtc_detail)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(765, 166)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'xtc_detail
        '
        Me.xtc_detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_detail.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_detail.Location = New System.Drawing.Point(0, 0)
        Me.xtc_detail.Name = "xtc_detail"
        Me.xtc_detail.PaintStyleName = "PropertyView"
        Me.xtc_detail.SelectedTabPage = Me.xtp_chart
        Me.xtc_detail.Size = New System.Drawing.Size(765, 166)
        Me.xtc_detail.TabIndex = 0
        Me.xtc_detail.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_chart, Me.xtp_configuration})
        '
        'xtp_chart
        '
        Me.xtp_chart.Name = "xtp_chart"
        Me.xtp_chart.Size = New System.Drawing.Size(763, 145)
        Me.xtp_chart.Text = "Chart View"
        '
        'xtp_configuration
        '
        Me.xtp_configuration.Controls.Add(Me.sb_default)
        Me.xtp_configuration.Controls.Add(Me.LabelControl4)
        Me.xtp_configuration.Controls.Add(Me.cb_lbl_position)
        Me.xtp_configuration.Controls.Add(Me.ce_value_percent)
        Me.xtp_configuration.Controls.Add(Me.LabelControl3)
        Me.xtp_configuration.Controls.Add(Me.cb_explode)
        Me.xtp_configuration.Controls.Add(Me.ce_view_label)
        Me.xtp_configuration.Controls.Add(Me.LabelControl2)
        Me.xtp_configuration.Controls.Add(Me.cb_bar_appearance)
        Me.xtp_configuration.Controls.Add(Me.LabelControl1)
        Me.xtp_configuration.Controls.Add(Me.cb_chart_appearance)
        Me.xtp_configuration.Controls.Add(Me.lbl_chart_view)
        Me.xtp_configuration.Controls.Add(Me.cb_chart_view)
        Me.xtp_configuration.Name = "xtp_configuration"
        Me.xtp_configuration.Size = New System.Drawing.Size(763, 145)
        Me.xtp_configuration.Text = "Chart Configuration"
        '
        'sb_default
        '
        Me.sb_default.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_default.Location = New System.Drawing.Point(555, 48)
        Me.sb_default.Name = "sb_default"
        Me.sb_default.Size = New System.Drawing.Size(100, 23)
        Me.sb_default.TabIndex = 34
        Me.sb_default.Text = "Set As Default"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(331, 31)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl4.TabIndex = 33
        Me.LabelControl4.Text = "Label Position"
        '
        'cb_lbl_position
        '
        Me.cb_lbl_position.Enabled = False
        Me.cb_lbl_position.Location = New System.Drawing.Point(450, 26)
        Me.cb_lbl_position.Name = "cb_lbl_position"
        Me.cb_lbl_position.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_lbl_position.Properties.Items.AddRange(New Object() {"Inside", "Outside", "Two Columns", "Radial"})
        Me.cb_lbl_position.Size = New System.Drawing.Size(205, 20)
        Me.cb_lbl_position.TabIndex = 32
        '
        'ce_value_percent
        '
        Me.ce_value_percent.Location = New System.Drawing.Point(409, 52)
        Me.ce_value_percent.Name = "ce_value_percent"
        Me.ce_value_percent.Properties.Caption = "Value As Percent"
        Me.ce_value_percent.Size = New System.Drawing.Size(107, 19)
        Me.ce_value_percent.TabIndex = 31
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(331, 8)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(112, 13)
        Me.LabelControl3.TabIndex = 30
        Me.LabelControl3.Text = "Explode Pie / Doughnut"
        '
        'cb_explode
        '
        Me.cb_explode.Enabled = False
        Me.cb_explode.Location = New System.Drawing.Point(450, 3)
        Me.cb_explode.Name = "cb_explode"
        Me.cb_explode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_explode.Properties.Items.AddRange(New Object() {"None", "All", "MinValue", "MaxValue"})
        Me.cb_explode.Size = New System.Drawing.Size(205, 20)
        Me.cb_explode.TabIndex = 29
        '
        'ce_view_label
        '
        Me.ce_view_label.EditValue = True
        Me.ce_view_label.Location = New System.Drawing.Point(328, 52)
        Me.ce_view_label.Name = "ce_view_label"
        Me.ce_view_label.Properties.Caption = "View Label"
        Me.ce_view_label.Size = New System.Drawing.Size(75, 19)
        Me.ce_view_label.TabIndex = 28
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(7, 56)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(77, 13)
        Me.LabelControl2.TabIndex = 27
        Me.LabelControl2.Text = "Bar Appearance"
        '
        'cb_bar_appearance
        '
        Me.cb_bar_appearance.Location = New System.Drawing.Point(101, 49)
        Me.cb_bar_appearance.Name = "cb_bar_appearance"
        Me.cb_bar_appearance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_bar_appearance.Size = New System.Drawing.Size(205, 20)
        Me.cb_bar_appearance.TabIndex = 26
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(7, 31)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(88, 13)
        Me.LabelControl1.TabIndex = 25
        Me.LabelControl1.Text = "Chart Appearance"
        '
        'cb_chart_appearance
        '
        Me.cb_chart_appearance.Location = New System.Drawing.Point(101, 26)
        Me.cb_chart_appearance.Name = "cb_chart_appearance"
        Me.cb_chart_appearance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_chart_appearance.Size = New System.Drawing.Size(205, 20)
        Me.cb_chart_appearance.TabIndex = 24
        '
        'lbl_chart_view
        '
        Me.lbl_chart_view.Location = New System.Drawing.Point(7, 8)
        Me.lbl_chart_view.Name = "lbl_chart_view"
        Me.lbl_chart_view.Size = New System.Drawing.Size(52, 13)
        Me.lbl_chart_view.TabIndex = 23
        Me.lbl_chart_view.Text = "Chart View"
        '
        'cb_chart_view
        '
        Me.cb_chart_view.Location = New System.Drawing.Point(101, 3)
        Me.cb_chart_view.Name = "cb_chart_view"
        Me.cb_chart_view.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_chart_view.Properties.Items.AddRange(New Object() {"Bar", "Bar Stacked", "Bar Stacked 100%", "Bar 3D", "Bar 3D Stacked", "Bar 3D Stacked 100%", "Manhattan Bar", "Point", "Bubble", "Line", "Step Line", "Spline", "Line 3D", "Step Line 3D", "Spline 3D", "Area", "Area Stacked", "Area Stacked 100%", "Spline Area", "Spline Area Stacked", "Spline Area Stacked 100%", "Area 3D", "Area 3D Stacked", "Area 3D Stacked 100%", "Spline Area 3D", "Spline Area 3D Stacked", "Spline Area 3D Stacked 100%", "Pie", "Doughnut", "Pie 3D", "Doughnut 3D", "Stock", "Candle Stick", "Range Bar", "Side By Side Range Bar", "Gantt", "Side By Side Gantt", "Radar Point", "Radar Line", "Radar Area"})
        Me.cb_chart_view.Size = New System.Drawing.Size(205, 20)
        Me.cb_chart_view.TabIndex = 22
        '
        'MasterInfChart2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(771, 433)
        Me.Controls.Add(Me.scc_master)
        Me.Controls.Add(Me.dp_detail)
        Me.Name = "MasterInfChart2"
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dp_detail.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.xtc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_detail.ResumeLayout(False)
        Me.xtp_configuration.ResumeLayout(False)
        Me.xtp_configuration.PerformLayout()
        CType(Me.cb_lbl_position.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_value_percent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_explode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_view_label.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_bar_appearance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_chart_appearance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cb_chart_view.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents scc_master As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents dp_detail As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Protected WithEvents xtp_chart As DevExpress.XtraTab.XtraTabPage
    Protected WithEvents xtc_detail As DevExpress.XtraTab.XtraTabControl
    Protected WithEvents xtp_configuration As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents sb_default As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_lbl_position As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ce_value_percent As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_explode As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ce_view_label As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_bar_appearance As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_chart_appearance As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lbl_chart_view As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_chart_view As DevExpress.XtraEditors.ComboBoxEdit
    Protected WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Protected WithEvents xtp_data As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController

End Class
