<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInsentifDS
    Inherits master_new.MasterWITwo

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
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.pr_periode = New DevExpress.XtraEditors.LookUpEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.sb_generate = New DevExpress.XtraEditors.SimpleButton
        Me.sb_generate2 = New DevExpress.XtraEditors.SimpleButton
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_periode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(553, 374)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.sb_generate2)
        Me.scc_master.Panel1.Controls.Add(Me.sb_generate)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.pr_periode)
        Me.scc_master.SplitterPosition = 32
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 395)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 364)
        Me.gc_master.TabIndex = 2
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'pr_periode
        '
        Me.pr_periode.Location = New System.Drawing.Point(47, 7)
        Me.pr_periode.Name = "pr_periode"
        Me.pr_periode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_periode.Properties.DisplayFormat.FormatString = "d"
        Me.pr_periode.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.pr_periode.Properties.EditFormat.FormatString = "d"
        Me.pr_periode.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.pr_periode.Properties.PopupWidth = 500
        Me.pr_periode.Size = New System.Drawing.Size(158, 20)
        Me.pr_periode.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(5, 11)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(36, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Periode"
        '
        'sb_generate
        '
        Me.sb_generate.Location = New System.Drawing.Point(233, 4)
        Me.sb_generate.Name = "sb_generate"
        Me.sb_generate.Size = New System.Drawing.Size(105, 23)
        Me.sb_generate.TabIndex = 2
        Me.sb_generate.Text = "Generate"
        '
        'sb_generate2
        '
        Me.sb_generate2.Location = New System.Drawing.Point(344, 4)
        Me.sb_generate2.Name = "sb_generate2"
        Me.sb_generate2.Size = New System.Drawing.Size(105, 23)
        Me.sb_generate2.TabIndex = 2
        Me.sb_generate2.Text = "Generate Periode 2"
        '
        'FInsentifDS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FInsentifDS"
        Me.Text = "Insentif Direct Seling"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_periode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pr_periode As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sb_generate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_generate2 As DevExpress.XtraEditors.SimpleButton

End Class
