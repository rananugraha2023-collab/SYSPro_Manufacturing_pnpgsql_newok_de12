<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPrintScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FPrintScreen))
        Me.notifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.contextMenu1 = New System.Windows.Forms.ContextMenu
        Me.menuItem1 = New System.Windows.Forms.MenuItem
        Me.menuItem3 = New System.Windows.Forms.MenuItem
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PrintSystem1 = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        CType(Me.PrintSystem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'notifyIcon1
        '
        Me.notifyIcon1.ContextMenu = Me.contextMenu1
        Me.notifyIcon1.Icon = CType(resources.GetObject("notifyIcon1.Icon"), System.Drawing.Icon)
        Me.notifyIcon1.Text = "Print Screen"
        Me.notifyIcon1.Visible = True
        '
        'contextMenu1
        '
        Me.contextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem1, Me.menuItem3})
        '
        'menuItem1
        '
        Me.menuItem1.Index = 0
        Me.menuItem1.Text = "&PrintScreen"
        '
        'menuItem3
        '
        Me.menuItem3.Index = 1
        Me.menuItem3.Text = "Page&Setup"
        '
        'timer1
        '
        Me.timer1.Interval = 200
        '
        'FPrintScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "FPrintScreen"
        Me.Opacity = 0
        Me.ShowInTaskbar = False
        Me.Text = "FPrintScreen"
        CType(Me.PrintSystem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents notifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents contextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents timer1 As System.Windows.Forms.Timer
    Friend WithEvents PrintSystem1 As DevExpress.XtraPrinting.PrintingSystem
End Class
