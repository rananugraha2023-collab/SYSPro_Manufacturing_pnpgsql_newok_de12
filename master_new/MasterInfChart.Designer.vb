<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterInfChart
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
        Me.xtp_grid = New DevExpress.XtraTab.XtraTabPage
        Me.xtp_chart = New DevExpress.XtraTab.XtraTabPage
        Me.scc_chart = New DevExpress.XtraEditors.SplitContainerControl
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
        Me.xtp_chart.SuspendLayout()
        CType(Me.scc_chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_chart.SuspendLayout()
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
        Me.scc_master.Size = New System.Drawing.Size(892, 565)
        Me.scc_master.SplitterPosition = 69
        Me.scc_master.TabIndex = 2
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_grid
        Me.xtc_master.Size = New System.Drawing.Size(888, 486)
        Me.xtc_master.TabIndex = 0
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_grid, Me.xtp_chart})
        '
        'xtp_grid
        '
        Me.xtp_grid.Name = "xtp_grid"
        Me.xtp_grid.Size = New System.Drawing.Size(886, 465)
        Me.xtp_grid.Text = "Grid View"
        '
        'xtp_chart
        '
        Me.xtp_chart.Controls.Add(Me.scc_chart)
        Me.xtp_chart.Name = "xtp_chart"
        Me.xtp_chart.Size = New System.Drawing.Size(886, 465)
        Me.xtp_chart.Text = "Chart View"
        '
        'scc_chart
        '
        Me.scc_chart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_chart.Horizontal = False
        Me.scc_chart.Location = New System.Drawing.Point(0, 0)
        Me.scc_chart.Name = "scc_chart"
        Me.scc_chart.Panel1.Text = "Panel1"
        Me.scc_chart.Panel2.Controls.Add(Me.sb_default)
        Me.scc_chart.Panel2.Controls.Add(Me.LabelControl4)
        Me.scc_chart.Panel2.Controls.Add(Me.cb_lbl_position)
        Me.scc_chart.Panel2.Controls.Add(Me.ce_value_percent)
        Me.scc_chart.Panel2.Controls.Add(Me.LabelControl3)
        Me.scc_chart.Panel2.Controls.Add(Me.cb_explode)
        Me.scc_chart.Panel2.Controls.Add(Me.ce_view_label)
        Me.scc_chart.Panel2.Controls.Add(Me.LabelControl2)
        Me.scc_chart.Panel2.Controls.Add(Me.cb_bar_appearance)
        Me.scc_chart.Panel2.Controls.Add(Me.LabelControl1)
        Me.scc_chart.Panel2.Controls.Add(Me.cb_chart_appearance)
        Me.scc_chart.Panel2.Controls.Add(Me.lbl_chart_view)
        Me.scc_chart.Panel2.Controls.Add(Me.cb_chart_view)
        Me.scc_chart.Panel2.ShowCaption = True
        Me.scc_chart.Panel2.Text = "Chart Configuration"
        Me.scc_chart.Size = New System.Drawing.Size(886, 465)
        Me.scc_chart.SplitterPosition = 362
        Me.scc_chart.TabIndex = 0
        Me.scc_chart.Text = "SplitContainerControl1"
        '
        'sb_default
        '
        Me.sb_default.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_default.Location = New System.Drawing.Point(556, 49)
        Me.sb_default.Name = "sb_default"
        Me.sb_default.Size = New System.Drawing.Size(100, 23)
        Me.sb_default.TabIndex = 21
        Me.sb_default.Text = "Set As Default"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(332, 32)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(65, 13)
        Me.LabelControl4.TabIndex = 20
        Me.LabelControl4.Text = "Label Position"
        '
        'cb_lbl_position
        '
        Me.cb_lbl_position.Enabled = False
        Me.cb_lbl_position.Location = New System.Drawing.Point(451, 27)
        Me.cb_lbl_position.Name = "cb_lbl_position"
        Me.cb_lbl_position.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_lbl_position.Properties.Items.AddRange(New Object() {"Inside", "Outside", "Two Columns", "Radial"})
        Me.cb_lbl_position.Size = New System.Drawing.Size(205, 20)
        Me.cb_lbl_position.TabIndex = 19
        '
        'ce_value_percent
        '
        Me.ce_value_percent.Location = New System.Drawing.Point(410, 53)
        Me.ce_value_percent.Name = "ce_value_percent"
        Me.ce_value_percent.Properties.Caption = "Value As Percent"
        Me.ce_value_percent.Size = New System.Drawing.Size(107, 19)
        Me.ce_value_percent.TabIndex = 18
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(332, 9)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(112, 13)
        Me.LabelControl3.TabIndex = 15
        Me.LabelControl3.Text = "Explode Pie / Doughnut"
        '
        'cb_explode
        '
        Me.cb_explode.Enabled = False
        Me.cb_explode.Location = New System.Drawing.Point(451, 4)
        Me.cb_explode.Name = "cb_explode"
        Me.cb_explode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_explode.Properties.Items.AddRange(New Object() {"None", "All", "MinValue", "MaxValue"})
        Me.cb_explode.Size = New System.Drawing.Size(205, 20)
        Me.cb_explode.TabIndex = 14
        '
        'ce_view_label
        '
        Me.ce_view_label.EditValue = True
        Me.ce_view_label.Location = New System.Drawing.Point(329, 53)
        Me.ce_view_label.Name = "ce_view_label"
        Me.ce_view_label.Properties.Caption = "View Label"
        Me.ce_view_label.Size = New System.Drawing.Size(75, 19)
        Me.ce_view_label.TabIndex = 13
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(8, 57)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(77, 13)
        Me.LabelControl2.TabIndex = 12
        Me.LabelControl2.Text = "Bar Appearance"
        '
        'cb_bar_appearance
        '
        Me.cb_bar_appearance.Location = New System.Drawing.Point(102, 50)
        Me.cb_bar_appearance.Name = "cb_bar_appearance"
        Me.cb_bar_appearance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_bar_appearance.Size = New System.Drawing.Size(205, 20)
        Me.cb_bar_appearance.TabIndex = 11
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(8, 32)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(88, 13)
        Me.LabelControl1.TabIndex = 10
        Me.LabelControl1.Text = "Chart Appearance"
        '
        'cb_chart_appearance
        '
        Me.cb_chart_appearance.Location = New System.Drawing.Point(102, 27)
        Me.cb_chart_appearance.Name = "cb_chart_appearance"
        Me.cb_chart_appearance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_chart_appearance.Size = New System.Drawing.Size(205, 20)
        Me.cb_chart_appearance.TabIndex = 9
        '
        'lbl_chart_view
        '
        Me.lbl_chart_view.Location = New System.Drawing.Point(8, 9)
        Me.lbl_chart_view.Name = "lbl_chart_view"
        Me.lbl_chart_view.Size = New System.Drawing.Size(52, 13)
        Me.lbl_chart_view.TabIndex = 8
        Me.lbl_chart_view.Text = "Chart View"
        '
        'cb_chart_view
        '
        Me.cb_chart_view.Location = New System.Drawing.Point(102, 4)
        Me.cb_chart_view.Name = "cb_chart_view"
        Me.cb_chart_view.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cb_chart_view.Properties.Items.AddRange(New Object() {"Bar", "Bar Stacked", "Bar Stacked 100%", "Bar 3D", "Bar 3D Stacked", "Bar 3D Stacked 100%", "Manhattan Bar", "Point", "Bubble", "Line", "Step Line", "Spline", "Line 3D", "Step Line 3D", "Spline 3D", "Area", "Area Stacked", "Area Stacked 100%", "Spline Area", "Spline Area Stacked", "Spline Area Stacked 100%", "Area 3D", "Area 3D Stacked", "Area 3D Stacked 100%", "Spline Area 3D", "Spline Area 3D Stacked", "Spline Area 3D Stacked 100%", "Pie", "Doughnut", "Pie 3D", "Doughnut 3D", "Stock", "Candle Stick", "Range Bar", "Side By Side Range Bar", "Gantt", "Side By Side Gantt", "Radar Point", "Radar Line", "Radar Area"})
        Me.cb_chart_view.Size = New System.Drawing.Size(205, 20)
        Me.cb_chart_view.TabIndex = 7
        '
        'MasterInfChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(892, 565)
        Me.Controls.Add(Me.scc_master)
        Me.Name = "MasterInfChart"
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_chart.ResumeLayout(False)
        CType(Me.scc_chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_chart.ResumeLayout(False)
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
    Friend WithEvents xtp_chart As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ce_view_label As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_bar_appearance As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_chart_appearance As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lbl_chart_view As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_chart_view As DevExpress.XtraEditors.ComboBoxEdit
    Protected WithEvents scc_chart As DevExpress.XtraEditors.SplitContainerControl
    Protected WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Protected WithEvents xtp_grid As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_explode As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ce_value_percent As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Protected WithEvents cb_lbl_position As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents sb_default As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ToolTipController1 As DevExpress.Utils.ToolTipController

End Class
