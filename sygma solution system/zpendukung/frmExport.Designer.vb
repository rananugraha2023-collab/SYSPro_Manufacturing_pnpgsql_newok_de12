<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExport
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
        Me.gc_export = New DevExpress.XtraGrid.GridControl
        Me.gv_export = New DevExpress.XtraGrid.Views.Grid.GridView
        CType(Me.gc_export, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_export, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gc_export
        '
        Me.gc_export.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_export.Location = New System.Drawing.Point(0, 0)
        Me.gc_export.MainView = Me.gv_export
        Me.gc_export.Name = "gc_export"
        Me.gc_export.Size = New System.Drawing.Size(1043, 337)
        Me.gc_export.TabIndex = 0
        Me.gc_export.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_export})
        '
        'gv_export
        '
        Me.gv_export.GridControl = Me.gc_export
        Me.gv_export.Name = "gv_export"
        Me.gv_export.OptionsSelection.MultiSelect = True
        Me.gv_export.OptionsView.ColumnAutoWidth = False
        Me.gv_export.OptionsView.ShowAutoFilterRow = True
        '
        'frmExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 337)
        Me.Controls.Add(Me.gc_export)
        Me.Name = "frmExport"
        Me.Text = "frmExport"
        CType(Me.gc_export, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_export, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents gc_export As DevExpress.XtraGrid.GridControl
    Public WithEvents gv_export As DevExpress.XtraGrid.Views.Grid.GridView
End Class
