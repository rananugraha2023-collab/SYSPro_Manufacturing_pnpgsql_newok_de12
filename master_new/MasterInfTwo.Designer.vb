<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterInfTwo
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
        Me.scc_master = New DevExpress.XtraEditors.SplitContainerControl
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
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
        Me.scc_master.Size = New System.Drawing.Size(555, 433)
        Me.scc_master.SplitterPosition = 69
        Me.scc_master.TabIndex = 1
        '
        'MasterInfTwo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Controls.Add(Me.scc_master)
        Me.Name = "MasterInfTwo"
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents scc_master As DevExpress.XtraEditors.SplitContainerControl

End Class
