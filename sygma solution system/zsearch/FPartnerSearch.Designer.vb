<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartnerSearch
    Inherits master_new.FSearch

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
        Me.gc_master = New DevExpress.XtraGrid.GridControl()
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.par_entity = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.sb_fill = New DevExpress.XtraEditors.SimpleButton()
        Me.sb_retrieve = New DevExpress.XtraEditors.SimpleButton()
        Me.CeAllEntity = New DevExpress.XtraEditors.CheckEdit()
        Me.te_name = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.par_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CeAllEntity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.te_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.te_name)
        Me.scc_master.Panel1.Controls.Add(Me.CeAllEntity)
        Me.scc_master.Panel1.Controls.Add(Me.sb_fill)
        Me.scc_master.Panel1.Controls.Add(Me.sb_retrieve)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl2)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.par_entity)
        Me.scc_master.SplitterPosition = 36
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(878, 349)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(876, 348)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(876, 348)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(866, 288)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(866, 338)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'par_entity
        '
        Me.par_entity.Location = New System.Drawing.Point(47, 8)
        Me.par_entity.Name = "par_entity"
        Me.par_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.par_entity.Size = New System.Drawing.Size(236, 20)
        Me.par_entity.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 11)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Entity"
        '
        'sb_fill
        '
        Me.sb_fill.Location = New System.Drawing.Point(699, 7)
        Me.sb_fill.Name = "sb_fill"
        Me.sb_fill.Size = New System.Drawing.Size(75, 23)
        Me.sb_fill.TabIndex = 4
        Me.sb_fill.Text = "Select Data"
        Me.sb_fill.Visible = False
        '
        'sb_retrieve
        '
        Me.sb_retrieve.Location = New System.Drawing.Point(614, 7)
        Me.sb_retrieve.Name = "sb_retrieve"
        Me.sb_retrieve.Size = New System.Drawing.Size(75, 23)
        Me.sb_retrieve.TabIndex = 3
        Me.sb_retrieve.Text = "Retrieve"
        '
        'CeAllEntity
        '
        Me.CeAllEntity.Location = New System.Drawing.Point(289, 9)
        Me.CeAllEntity.Name = "CeAllEntity"
        Me.CeAllEntity.Properties.Caption = "All Entity"
        Me.CeAllEntity.Size = New System.Drawing.Size(75, 19)
        Me.CeAllEntity.TabIndex = 1
        '
        'te_name
        '
        Me.te_name.Location = New System.Drawing.Point(448, 8)
        Me.te_name.Name = "te_name"
        Me.te_name.Size = New System.Drawing.Size(147, 20)
        Me.te_name.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(398, 11)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Name"
        '
        'FPartnerSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(878, 390)
        Me.Name = "FPartnerSearch"
        Me.Text = "Partner Search"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.par_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CeAllEntity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.te_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents par_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sb_fill As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sb_retrieve As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CeAllEntity As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents te_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl

End Class
