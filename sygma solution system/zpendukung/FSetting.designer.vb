<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSetting
    Inherits DevExpress.XtraEditors.XtraForm

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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.BtUpdate = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl
        Me.version_id = New DevExpress.XtraEditors.TextEdit
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.version_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.BtUpdate)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.version_id)
        Me.GroupControl1.Location = New System.Drawing.Point(28, 29)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(505, 223)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Setting"
        '
        'BtUpdate
        '
        Me.BtUpdate.Location = New System.Drawing.Point(127, 78)
        Me.BtUpdate.Name = "BtUpdate"
        Me.BtUpdate.Size = New System.Drawing.Size(75, 23)
        Me.BtUpdate.TabIndex = 2
        Me.BtUpdate.Text = "Update"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(27, 49)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Version"
        '
        'version_id
        '
        Me.version_id.Location = New System.Drawing.Point(126, 44)
        Me.version_id.Name = "version_id"
        Me.version_id.Size = New System.Drawing.Size(182, 20)
        Me.version_id.TabIndex = 0
        '
        'FSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(603, 321)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "FSetting"
        Me.Text = "Setting"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.version_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents BtUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents version_id As DevExpress.XtraEditors.TextEdit
End Class
