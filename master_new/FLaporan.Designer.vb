<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FLaporan
    Inherits master_new.MasterReport

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
        Me.pr_lblparam = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pr_lblparam)
        '
        'pr_lblparam
        '
        Me.pr_lblparam.AutoSize = True
        Me.pr_lblparam.Location = New System.Drawing.Point(95, 33)
        Me.pr_lblparam.Name = "pr_lblparam"
        Me.pr_lblparam.Size = New System.Drawing.Size(39, 13)
        Me.pr_lblparam.TabIndex = 1
        Me.pr_lblparam.Text = "Label1"
        '
        'FLaporan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(868, 433)
        Me.Name = "FLaporan"
        Me.Text = "Preview Mode"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents pr_lblparam As System.Windows.Forms.Label

End Class
