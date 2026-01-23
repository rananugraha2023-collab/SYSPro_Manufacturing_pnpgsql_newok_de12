<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XtraForm1
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
        Me.nPgSqlConnection1 = New Npgsql.NpgsqlConnection
        Me.SuspendLayout()
        '
        'nPgSqlConnection1
        '
        'Me.PgSqlConnection1.Name = "nPgSqlConnection1"
        'Me.nPgSqlConnection1.Owner = Me
        '
        'XtraForm1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "XtraForm1"
        Me.Text = "XtraForm1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents nPgSqlConnection1 As Npgsql.NpgsqlConnection
End Class
