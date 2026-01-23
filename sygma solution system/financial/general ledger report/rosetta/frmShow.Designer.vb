<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShow
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
        Me.gc_view = New DevExpress.XtraGrid.GridControl
        Me.gv_view = New DevExpress.XtraGrid.Views.Grid.GridView
        CType(Me.gc_view, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_view, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gc_view
        '
        Me.gc_view.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_view.Location = New System.Drawing.Point(0, 0)
        Me.gc_view.MainView = Me.gv_view
        Me.gc_view.Name = "gc_view"
        Me.gc_view.Size = New System.Drawing.Size(495, 413)
        Me.gc_view.TabIndex = 0
        Me.gc_view.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_view})
        '
        'gv_view
        '
        Me.gv_view.GridControl = Me.gc_view
        Me.gv_view.Name = "gv_view"
        Me.gv_view.OptionsBehavior.AutoPopulateColumns = False
        Me.gv_view.OptionsSelection.MultiSelect = True
        Me.gv_view.OptionsView.ColumnAutoWidth = False
        Me.gv_view.OptionsView.EnableAppearanceEvenRow = True
        Me.gv_view.OptionsView.ShowAutoFilterRow = True
        '
        'frmShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 413)
        Me.Controls.Add(Me.gc_view)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmShow"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Data"
        CType(Me.gc_view, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_view, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_view As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_view As DevExpress.XtraGrid.Views.Grid.GridView
End Class
