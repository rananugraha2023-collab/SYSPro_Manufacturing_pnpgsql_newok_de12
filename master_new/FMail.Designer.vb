<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FMail))
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl
        Me.Label2 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.sb_cancel = New DevExpress.XtraEditors.SimpleButton
        Me.sb_sent = New DevExpress.XtraEditors.SimpleButton
        Me.sc_txtfrom = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.sc_txtfile = New DevExpress.XtraEditors.TextEdit
        Me.Label6 = New System.Windows.Forms.Label
        Me.sc_txtbody = New DevExpress.XtraEditors.TextEdit
        Me.Label5 = New System.Windows.Forms.Label
        Me.sc_txtsubject = New DevExpress.XtraEditors.TextEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.sc_txtcc = New DevExpress.XtraEditors.TextEdit
        Me.Label3 = New System.Windows.Forms.Label
        Me.sc_txtto = New DevExpress.XtraEditors.TextEdit
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.sc_txtfile.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtbody.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtsubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl3
        '
        Me.PanelControl3.Appearance.BackColor = System.Drawing.Color.Orange
        Me.PanelControl3.Appearance.BackColor2 = System.Drawing.Color.FloralWhite
        Me.PanelControl3.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.PanelControl3.Appearance.Options.UseBackColor = True
        Me.PanelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl3.Controls.Add(Me.Label2)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl3.Location = New System.Drawing.Point(9, 9)
        Me.PanelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(388, 25)
        Me.PanelControl3.TabIndex = 18
        Me.PanelControl3.Text = "PanelControl3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 17)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "SYSPRO - Mail Form"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(21, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 21
        Me.PictureBox1.TabStop = False
        '
        'PanelControl2
        '
        Me.PanelControl2.Appearance.BackColor = System.Drawing.Color.White
        Me.PanelControl2.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.PanelControl2.Appearance.Options.UseBackColor = True
        Me.PanelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(9, 34)
        Me.PanelControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(388, 7)
        Me.PanelControl2.TabIndex = 34
        Me.PanelControl2.Text = "PanelControl2"
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.PanelControl1.Controls.Add(Me.sb_cancel)
        Me.PanelControl1.Controls.Add(Me.sb_sent)
        Me.PanelControl1.Controls.Add(Me.sc_txtfrom)
        Me.PanelControl1.Controls.Add(Me.Label7)
        Me.PanelControl1.Controls.Add(Me.sc_txtfile)
        Me.PanelControl1.Controls.Add(Me.Label6)
        Me.PanelControl1.Controls.Add(Me.sc_txtbody)
        Me.PanelControl1.Controls.Add(Me.Label5)
        Me.PanelControl1.Controls.Add(Me.sc_txtsubject)
        Me.PanelControl1.Controls.Add(Me.Label4)
        Me.PanelControl1.Controls.Add(Me.sc_txtcc)
        Me.PanelControl1.Controls.Add(Me.Label3)
        Me.PanelControl1.Controls.Add(Me.sc_txtto)
        Me.PanelControl1.Controls.Add(Me.Label1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(9, 41)
        Me.PanelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(388, 209)
        Me.PanelControl1.TabIndex = 35
        Me.PanelControl1.Text = "PanelControl1"
        '
        'sb_cancel
        '
        Me.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_cancel.Location = New System.Drawing.Point(305, 178)
        Me.sb_cancel.Name = "sb_cancel"
        Me.sb_cancel.Size = New System.Drawing.Size(75, 23)
        Me.sb_cancel.TabIndex = 55
        Me.sb_cancel.Text = "Cancel"
        '
        'sb_sent
        '
        Me.sb_sent.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sb_sent.Location = New System.Drawing.Point(217, 178)
        Me.sb_sent.Name = "sb_sent"
        Me.sb_sent.Size = New System.Drawing.Size(75, 23)
        Me.sb_sent.TabIndex = 54
        Me.sb_sent.Text = "Sent"
        '
        'sc_txtfrom
        '
        Me.sc_txtfrom.AutoSize = True
        Me.sc_txtfrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sc_txtfrom.Location = New System.Drawing.Point(56, 151)
        Me.sc_txtfrom.Name = "sc_txtfrom"
        Me.sc_txtfrom.Size = New System.Drawing.Size(0, 13)
        Me.sc_txtfrom.TabIndex = 53
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(30, 13)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "From"
        '
        'sc_txtfile
        '
        Me.sc_txtfile.Enabled = False
        Me.sc_txtfile.Location = New System.Drawing.Point(56, 110)
        Me.sc_txtfile.Name = "sc_txtfile"
        Me.sc_txtfile.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtfile.Size = New System.Drawing.Size(325, 20)
        Me.sc_txtfile.TabIndex = 43
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 13)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "File"
        '
        'sc_txtbody
        '
        Me.sc_txtbody.Location = New System.Drawing.Point(56, 84)
        Me.sc_txtbody.Name = "sc_txtbody"
        Me.sc_txtbody.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtbody.Size = New System.Drawing.Size(325, 20)
        Me.sc_txtbody.TabIndex = 41
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Body"
        '
        'sc_txtsubject
        '
        Me.sc_txtsubject.Location = New System.Drawing.Point(56, 59)
        Me.sc_txtsubject.Name = "sc_txtsubject"
        Me.sc_txtsubject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtsubject.Size = New System.Drawing.Size(325, 20)
        Me.sc_txtsubject.TabIndex = 39
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Subject"
        '
        'sc_txtcc
        '
        Me.sc_txtcc.Location = New System.Drawing.Point(56, 33)
        Me.sc_txtcc.Name = "sc_txtcc"
        Me.sc_txtcc.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtcc.Size = New System.Drawing.Size(325, 20)
        Me.sc_txtcc.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Cc"
        '
        'sc_txtto
        '
        Me.sc_txtto.Location = New System.Drawing.Point(56, 8)
        Me.sc_txtto.Name = "sc_txtto"
        Me.sc_txtto.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtto.Size = New System.Drawing.Size(325, 20)
        Me.sc_txtto.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "To "
        '
        'FMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(406, 259)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PanelControl3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FMail"
        Me.Padding = New System.Windows.Forms.Padding(9)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sent Email"
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.PanelControl3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.sc_txtfile.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtbody.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtsubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents sc_txtbody As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents sc_txtsubject As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents sc_txtcc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents sc_txtto As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents sb_cancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_sent As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sc_txtfrom As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents sc_txtfile As DevExpress.XtraEditors.TextEdit
End Class
