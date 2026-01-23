<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FReminder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FReminder))
        Me.timer_awal = New System.Windows.Forms.Timer(Me.components)
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbl_reminder = New System.Windows.Forms.Label
        Me.sb_go = New DevExpress.XtraEditors.SimpleButton
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.timer_akhir = New System.Windows.Forms.Timer(Me.components)
        Me.timer_ring = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timer_awal
        '
        '
        'PanelControl3
        '
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.Orange
        Me.PanelControl3.Appearance.BackColor2 = System.Drawing.Color.FloralWhite
        Me.PanelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.PanelControl3.Appearance.Image = CType(resources.GetObject("PanelControl3.Appearance.Image"), System.Drawing.Image)
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.Appearance.Options.UseImage = True
        Me.PanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.PanelControl3.Controls.Add(Me.Label2)
        Me.PanelControl3.Controls.Add(Me.lbl_reminder)
        Me.PanelControl3.Controls.Add(Me.sb_go)
        Me.PanelControl3.Controls.Add(Me.PictureBox1)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(5, 5)
        Me.PanelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(125, 166)
        Me.PanelControl3.TabIndex = 18
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Alerting System"
        '
        'lbl_reminder
        '
        Me.lbl_reminder.AutoSize = True
        Me.lbl_reminder.BackColor = System.Drawing.Color.Transparent
        Me.lbl_reminder.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_reminder.Location = New System.Drawing.Point(5, 117)
        Me.lbl_reminder.Name = "lbl_reminder"
        Me.lbl_reminder.Size = New System.Drawing.Size(82, 12)
        Me.lbl_reminder.TabIndex = 1
        Me.lbl_reminder.Text = "You Have Alert"
        '
        'sb_go
        '
        Me.sb_go.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_go.Cursor = System.Windows.Forms.Cursors.Hand
        Me.sb_go.Image = CType(resources.GetObject("sb_go.Image"), System.Drawing.Image)
        Me.sb_go.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.sb_go.Location = New System.Drawing.Point(6, 137)
        Me.sb_go.Name = "sb_go"
        Me.sb_go.Size = New System.Drawing.Size(47, 23)
        Me.sb_go.TabIndex = 0
        Me.sb_go.Text = "Go.."
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(121, 162)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'timer_akhir
        '
        '
        'timer_ring
        '
        Me.timer_ring.Interval = 3000
        '
        'FReminder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkOrange
        Me.ClientSize = New System.Drawing.Size(135, 176)
        Me.Controls.Add(Me.PanelControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FReminder"
        Me.Opacity = 0
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FReminder"
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timer_awal As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_reminder As System.Windows.Forms.Label
    Friend WithEvents timer_akhir As System.Windows.Forms.Timer
    Friend WithEvents timer_ring As System.Windows.Forms.Timer
    Protected WithEvents sb_go As DevExpress.XtraEditors.SimpleButton
    Protected WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
