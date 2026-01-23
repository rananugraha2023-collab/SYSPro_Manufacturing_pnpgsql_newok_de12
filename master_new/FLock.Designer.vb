<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLock
    Inherits master_new.FPass

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
        CType(Me.txt_password.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt_username.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.le_domain.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(159, 36)
        '
        'txt_username
        '
        Me.txt_username.Location = New System.Drawing.Point(177, 230)
        Me.txt_username.Visible = False
        '
        'sb_cancel
        '
        Me.sb_cancel.Location = New System.Drawing.Point(200, 211)
        Me.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = False
        Me.sb_cancel.Visible = False
        '
        'sb_ok
        '
        Me.sb_ok.Location = New System.Drawing.Point(287, 130)
        Me.sb_ok.LookAndFeel.UseDefaultLookAndFeel = False
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(159, 13)
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(175, 208)
        Me.UsernameLabel.Visible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.Size = New System.Drawing.Size(388, 177)
        '
        'le_domain
        '
        Me.le_domain.EditValue = CType(1, Short)
        Me.le_domain.Visible = False
        '
        'Label1
        '
        Me.Label1.Visible = False
        '
        'FLock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(406, 227)
        Me.KeyPreview = True
        Me.Name = "FLock"
        Me.ShowInTaskbar = False
        Me.TopMost = True
        CType(Me.txt_password.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt_username.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.le_domain.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

End Class
