<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FMenuAuthorization
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
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.sc_txtgroup_1 = New DevExpress.XtraEditors.TextEdit
        Me.sc_txtmenu = New DevExpress.XtraEditors.TextEdit
        Me.sc_cbenable_status = New System.Windows.Forms.ComboBox
        Me.sc_cbvisible_status = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.sc_cbeditable_status = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.sc_cb_delete = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.sc_cb_insert = New System.Windows.Forms.ComboBox
        Me.cancelable_status = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
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
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtgroup_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_txtmenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(767, 386)
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(765, 365)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(765, 365)
        '
        'scc_detail
        '
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(767, 386)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cancelable_status)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.sc_cb_insert)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.sc_cb_delete)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.sc_cbeditable_status)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.sc_cbvisible_status)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.sc_cbenable_status)
        Me.Panel1.Controls.Add(Me.sc_txtmenu)
        Me.Panel1.Controls.Add(Me.sc_txtgroup_1)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Size = New System.Drawing.Size(745, 295)
        Me.Panel1.TabIndex = 0
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(755, 355)
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
        Me.Label2.Location = New System.Drawing.Point(18, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Group"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Menu"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Enable Status"
        '
        'sc_txtgroup_1
        '
        Me.sc_txtgroup_1.Location = New System.Drawing.Point(118, 19)
        Me.sc_txtgroup_1.Name = "sc_txtgroup_1"
        Me.sc_txtgroup_1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtgroup_1.Size = New System.Drawing.Size(174, 20)
        Me.sc_txtgroup_1.TabIndex = 1
        '
        'sc_txtmenu
        '
        Me.sc_txtmenu.Location = New System.Drawing.Point(118, 46)
        Me.sc_txtmenu.Name = "sc_txtmenu"
        Me.sc_txtmenu.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003
        Me.sc_txtmenu.Size = New System.Drawing.Size(174, 20)
        Me.sc_txtmenu.TabIndex = 3
        '
        'sc_cbenable_status
        '
        Me.sc_cbenable_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sc_cbenable_status.FormattingEnabled = True
        Me.sc_cbenable_status.Location = New System.Drawing.Point(118, 73)
        Me.sc_cbenable_status.Name = "sc_cbenable_status"
        Me.sc_cbenable_status.Size = New System.Drawing.Size(121, 21)
        Me.sc_cbenable_status.TabIndex = 5
        '
        'sc_cbvisible_status
        '
        Me.sc_cbvisible_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sc_cbvisible_status.FormattingEnabled = True
        Me.sc_cbvisible_status.Location = New System.Drawing.Point(118, 100)
        Me.sc_cbvisible_status.Name = "sc_cbvisible_status"
        Me.sc_cbvisible_status.Size = New System.Drawing.Size(121, 21)
        Me.sc_cbvisible_status.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Visible Status"
        '
        'sc_cbeditable_status
        '
        Me.sc_cbeditable_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sc_cbeditable_status.FormattingEnabled = True
        Me.sc_cbeditable_status.Location = New System.Drawing.Point(118, 154)
        Me.sc_cbeditable_status.Name = "sc_cbeditable_status"
        Me.sc_cbeditable_status.Size = New System.Drawing.Size(121, 21)
        Me.sc_cbeditable_status.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 159)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Editable Status"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 186)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Deleteable Status"
        '
        'sc_cb_delete
        '
        Me.sc_cb_delete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sc_cb_delete.FormattingEnabled = True
        Me.sc_cb_delete.Location = New System.Drawing.Point(118, 181)
        Me.sc_cb_delete.Name = "sc_cb_delete"
        Me.sc_cb_delete.Size = New System.Drawing.Size(121, 21)
        Me.sc_cb_delete.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 131)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Insertable Status"
        '
        'sc_cb_insert
        '
        Me.sc_cb_insert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.sc_cb_insert.FormattingEnabled = True
        Me.sc_cb_insert.Location = New System.Drawing.Point(118, 127)
        Me.sc_cb_insert.Name = "sc_cb_insert"
        Me.sc_cb_insert.Size = New System.Drawing.Size(121, 21)
        Me.sc_cb_insert.TabIndex = 9
        '
        'cancelable_status
        '
        Me.cancelable_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cancelable_status.FormattingEnabled = True
        Me.cancelable_status.Location = New System.Drawing.Point(118, 207)
        Me.cancelable_status.Name = "cancelable_status"
        Me.cancelable_status.Size = New System.Drawing.Size(121, 21)
        Me.cancelable_status.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 212)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Cancelable Status"
        '
        'FMenuAuthorization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(767, 386)
        Me.Name = "FMenuAuthorization"
        Me.Text = "Menu Authorization"
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
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtgroup_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_txtmenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sc_txtmenu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_txtgroup_1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents sc_cbenable_status As System.Windows.Forms.ComboBox
    Friend WithEvents sc_cbeditable_status As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents sc_cbvisible_status As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents sc_cb_delete As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents sc_cb_insert As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cancelable_status As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
