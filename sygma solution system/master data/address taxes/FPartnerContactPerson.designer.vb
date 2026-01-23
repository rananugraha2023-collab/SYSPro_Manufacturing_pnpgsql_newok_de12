<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartnerContactPerson
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
        Me.components = New System.ComponentModel.Container
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.ptnrac_email = New DevExpress.XtraEditors.TextEdit
        Me.ptnrac_phone_2 = New DevExpress.XtraEditors.TextEdit
        Me.ptnrac_phone_1 = New DevExpress.XtraEditors.TextEdit
        Me.ptnrac_contact_name = New DevExpress.XtraEditors.TextEdit
        Me.ptnrac_function = New DevExpress.XtraEditors.LookUpEdit
        Me.addrc_ptnra_oid = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.ptnrac_email.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrac_phone_2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrac_phone_1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrac_contact_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrac_function.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.addrc_ptnra_oid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(553, 412)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(553, 412)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(543, 352)
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(543, 402)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master, Me.GridView2})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.gc_master
        Me.GridView2.Name = "GridView2"
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.ptnrac_email)
        Me.lci_master.Controls.Add(Me.ptnrac_phone_2)
        Me.lci_master.Controls.Add(Me.ptnrac_phone_1)
        Me.lci_master.Controls.Add(Me.ptnrac_contact_name)
        Me.lci_master.Controls.Add(Me.ptnrac_function)
        Me.lci_master.Controls.Add(Me.addrc_ptnra_oid)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'ptnrac_email
        '
        Me.ptnrac_email.Location = New System.Drawing.Point(94, 132)
        Me.ptnrac_email.Name = "ptnrac_email"
        Me.ptnrac_email.Size = New System.Drawing.Size(437, 20)
        Me.ptnrac_email.StyleController = Me.lci_master
        Me.ptnrac_email.TabIndex = 18
        '
        'ptnrac_phone_2
        '
        Me.ptnrac_phone_2.Location = New System.Drawing.Point(94, 108)
        Me.ptnrac_phone_2.Name = "ptnrac_phone_2"
        Me.ptnrac_phone_2.Size = New System.Drawing.Size(437, 20)
        Me.ptnrac_phone_2.StyleController = Me.lci_master
        Me.ptnrac_phone_2.TabIndex = 17
        '
        'ptnrac_phone_1
        '
        Me.ptnrac_phone_1.Location = New System.Drawing.Point(94, 84)
        Me.ptnrac_phone_1.Name = "ptnrac_phone_1"
        Me.ptnrac_phone_1.Size = New System.Drawing.Size(437, 20)
        Me.ptnrac_phone_1.StyleController = Me.lci_master
        Me.ptnrac_phone_1.TabIndex = 16
        '
        'ptnrac_contact_name
        '
        Me.ptnrac_contact_name.Location = New System.Drawing.Point(94, 60)
        Me.ptnrac_contact_name.Name = "ptnrac_contact_name"
        Me.ptnrac_contact_name.Size = New System.Drawing.Size(437, 20)
        Me.ptnrac_contact_name.StyleController = Me.lci_master
        Me.ptnrac_contact_name.TabIndex = 15
        '
        'ptnrac_function
        '
        Me.ptnrac_function.Location = New System.Drawing.Point(94, 36)
        Me.ptnrac_function.Name = "ptnrac_function"
        Me.ptnrac_function.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ptnrac_function.Size = New System.Drawing.Size(437, 20)
        Me.ptnrac_function.StyleController = Me.lci_master
        Me.ptnrac_function.TabIndex = 14
        '
        'addrc_ptnra_oid
        '
        Me.addrc_ptnra_oid.Location = New System.Drawing.Point(94, 12)
        Me.addrc_ptnra_oid.Name = "addrc_ptnra_oid"
        Me.addrc_ptnra_oid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.addrc_ptnra_oid.Size = New System.Drawing.Size(437, 20)
        Me.addrc_ptnra_oid.StyleController = Me.lci_master
        Me.addrc_ptnra_oid.TabIndex = 13
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem6, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.addrc_ptnra_oid
        Me.LayoutControlItem3.CustomizationFormText = "Entity"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Partner Address"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.ptnrac_contact_name
        Me.LayoutControlItem1.CustomizationFormText = "Org Code"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Contact Person"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.ptnrac_phone_1
        Me.LayoutControlItem2.CustomizationFormText = "Org Name"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Phone1"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.ptnrac_phone_2
        Me.LayoutControlItem8.CustomizationFormText = "Org Approver ID"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem8.Text = "Phone2"
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.ptnrac_email
        Me.LayoutControlItem9.CustomizationFormText = "Remarks"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem9.Text = "Email"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ptnrac_function
        Me.LayoutControlItem6.CustomizationFormText = "Type ID"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem6.Text = "Function"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(78, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 144)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 188)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FPartnerContactPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FPartnerContactPerson"
        Me.Text = "Partner Contact Person"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me._dt_lang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.ptnrac_email.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrac_phone_2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrac_phone_1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrac_contact_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrac_function.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.addrc_ptnra_oid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents ptnrac_phone_2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrac_phone_1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrac_contact_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrac_function As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents addrc_ptnra_oid As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ptnrac_email As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
