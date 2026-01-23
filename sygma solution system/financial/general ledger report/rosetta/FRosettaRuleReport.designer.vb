<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRosettaRuleReport
    Inherits master_new.MasterInfTwo

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
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_view1 = New DevExpress.XtraTab.XtraTabPage
        Me.gc_view1 = New DevExpress.XtraGrid.GridControl
        Me.gv_view1 = New DevExpress.XtraGrid.Views.Grid.GridView
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_view1.SuspendLayout()
        CType(Me.gc_view1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_view1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(788, 433)
        Me.scc_master.SplitterPosition = 0
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_view1
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(788, 427)
        Me.xtc_master.TabIndex = 8
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_view1})
        '
        'xtp_view1
        '
        Me.xtp_view1.Controls.Add(Me.gc_view1)
        Me.xtp_view1.Name = "xtp_view1"
        Me.xtp_view1.Size = New System.Drawing.Size(786, 426)
        Me.xtp_view1.Text = "View1"
        '
        'gc_view1
        '
        Me.gc_view1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_view1.Location = New System.Drawing.Point(0, 0)
        Me.gc_view1.MainView = Me.gv_view1
        Me.gc_view1.Name = "gc_view1"
        Me.gc_view1.Size = New System.Drawing.Size(786, 426)
        Me.gc_view1.TabIndex = 0
        Me.gc_view1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_view1})
        '
        'gv_view1
        '
        Me.gv_view1.GridControl = Me.gc_view1
        Me.gv_view1.Name = "gv_view1"
        Me.gv_view1.OptionsView.ShowAutoFilterRow = True
        '
        'FRosettaRuleReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(788, 433)
        Me.Name = "FRosettaRuleReport"
        Me.Text = "Rosetta Rules Report"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_view1.ResumeLayout(False)
        CType(Me.gc_view1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_view1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_view1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_view1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_view1 As DevExpress.XtraGrid.Views.Grid.GridView

End Class
