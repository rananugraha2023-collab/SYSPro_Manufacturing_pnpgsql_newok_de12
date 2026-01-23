<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterWITwo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasterWITwo))
        Me.scc_master = New DevExpress.XtraEditors.SplitContainerControl
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_data = New DevExpress.XtraTab.XtraTabPage
        Me.xtp_edit = New DevExpress.XtraTab.XtraTabPage
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scc_master.Horizontal = False
        Me.scc_master.Location = New System.Drawing.Point(0, 0)
        Me.scc_master.Name = "scc_master"
        Me.scc_master.Panel1.Text = "Retrieve Data By : "
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(555, 433)
        Me.scc_master.SplitterPosition = 61
        Me.scc_master.TabIndex = 1
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_data
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[True]
        Me.xtc_master.Size = New System.Drawing.Size(555, 366)
        Me.xtc_master.TabIndex = 0
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_data, Me.xtp_edit})
        '
        'xtp_data
        '
        Me.xtp_data.Name = "xtp_data"
        Me.xtp_data.Padding = New System.Windows.Forms.Padding(5)
        Me.xtp_data.Size = New System.Drawing.Size(553, 345)
        Me.xtp_data.Text = "Data"
        '
        'xtp_edit
        '
        Me.xtp_edit.Controls.Add(Me.Panel1)
        Me.xtp_edit.Controls.Add(Me.PanelControl2)
        Me.xtp_edit.Controls.Add(Me.PictureBox1)
        Me.xtp_edit.Controls.Add(Me.PanelControl1)
        Me.xtp_edit.Name = "xtp_edit"
        Me.xtp_edit.Padding = New System.Windows.Forms.Padding(5)
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        Me.xtp_edit.Text = "Edit Data"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(5, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(543, 300)
        Me.Panel1.TabIndex = 15
        '
        'PanelControl2
        '
        Me.PanelControl2.Appearance.BackColor = System.Drawing.Color.Orange
        Me.PanelControl2.Appearance.BackColor2 = System.Drawing.Color.FloralWhite
        Me.PanelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.PanelControl2.Appearance.Options.UseBackColor = True
        Me.PanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(5, 330)
        Me.PanelControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(543, 10)
        Me.PanelControl2.TabIndex = 2
        Me.PanelControl2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 32)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Orange
        Me.PanelControl1.Appearance.BackColor2 = System.Drawing.Color.FloralWhite
        Me.PanelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(5, 5)
        Me.PanelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(543, 25)
        Me.PanelControl1.TabIndex = 0
        Me.PanelControl1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(25, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Editable Mode"
        '
        'MasterWITwo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Controls.Add(Me.scc_master)
        Me.Name = "MasterWITwo"
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents xtp_data As DevExpress.XtraTab.XtraTabPage
    Protected WithEvents scc_master As DevExpress.XtraEditors.SplitContainerControl
    Protected WithEvents xtp_edit As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl

End Class
