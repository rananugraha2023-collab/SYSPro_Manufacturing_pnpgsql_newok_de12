<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FUser
    Inherits master_new.MasterWIOne

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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.sc_txtkode_1 = New DevExpress.XtraEditors.TextEdit()
        Me.sc_txtuser = New DevExpress.XtraEditors.TextEdit()
        Me.sc_txtpassword = New DevExpress.XtraEditors.TextEdit()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.sc_cbnama = New DevExpress.XtraEditors.LookUpEdit()
        Me.sc_cbgroup = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_data.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtkode_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtpassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_cbnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_cbgroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(767, 433)
        '
        'xtc_master
        '
        Me.xtc_master.SelectedTabPage = Me.xtp_data
        Me.xtc_master.Size = New System.Drawing.Size(767, 433)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(765, 412)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(765, 412)
        '
        'scc_detail
        '
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(767, 433)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.sc_cbgroup)
        Me.Panel1.Controls.Add(Me.sc_cbnama)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.sc_txtpassword)
        Me.Panel1.Controls.Add(Me.sc_txtuser)
        Me.Panel1.Controls.Add(Me.sc_txtkode_1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Size = New System.Drawing.Size(745, 342)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(755, 402)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Kode"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "User"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Password"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Group"
        '
        'sc_txtkode_1
        '
        Me.sc_txtkode_1.Location = New System.Drawing.Point(116, 10)
        Me.sc_txtkode_1.Name = "sc_txtkode_1"
        Me.sc_txtkode_1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtkode_1.Properties.MaxLength = 5
        Me.sc_txtkode_1.Size = New System.Drawing.Size(67, 20)
        Me.sc_txtkode_1.TabIndex = 1
        Me.sc_txtkode_1.Visible = False
        '
        'sc_txtuser
        '
        Me.sc_txtuser.Location = New System.Drawing.Point(116, 35)
        Me.sc_txtuser.Name = "sc_txtuser"
        Me.sc_txtuser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtuser.Size = New System.Drawing.Size(180, 20)
        Me.sc_txtuser.TabIndex = 3
        '
        'sc_txtpassword
        '
        Me.sc_txtpassword.Location = New System.Drawing.Point(117, 61)
        Me.sc_txtpassword.Name = "sc_txtpassword"
        Me.sc_txtpassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtpassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.sc_txtpassword.Size = New System.Drawing.Size(100, 20)
        Me.sc_txtpassword.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Nama Karyawan"
        '
        'sc_cbnama
        '
        Me.sc_cbnama.Location = New System.Drawing.Point(117, 117)
        Me.sc_cbnama.Name = "sc_cbnama"
        Me.sc_cbnama.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_cbnama.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_cbnama.Properties.PopupWidth = 500
        Me.sc_cbnama.Size = New System.Drawing.Size(178, 20)
        Me.sc_cbnama.TabIndex = 9
        '
        'sc_cbgroup
        '
        Me.sc_cbgroup.Location = New System.Drawing.Point(117, 89)
        Me.sc_cbgroup.Name = "sc_cbgroup"
        Me.sc_cbgroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_cbgroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_cbgroup.Properties.PopupWidth = 300
        Me.sc_cbgroup.Size = New System.Drawing.Size(178, 20)
        Me.sc_cbgroup.TabIndex = 7
        '
        'FUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(767, 433)
        Me.Name = "FUser"
        Me.Text = "User Authentication"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_data.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtkode_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtpassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_cbnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_cbgroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sc_txtpassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_txtuser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_txtkode_1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents sc_cbgroup As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_cbnama As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label6 As System.Windows.Forms.Label

End Class
