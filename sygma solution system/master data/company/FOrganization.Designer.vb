<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FOrganization
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
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.sc_ce_org_active = New DevExpress.XtraEditors.CheckEdit
        Me.sc_ce_org_default = New DevExpress.XtraEditors.CheckEdit
        Me.sc_te_org_remarks = New DevExpress.XtraEditors.TextEdit
        Me.sc_le_org_approver_id = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_te_org_name = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_org_code = New DevExpress.XtraEditors.TextEdit
        Me.sc_le_org_type_id = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_org_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_type_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_name = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_approver_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_remarks = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_default = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_org_active = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.sc_ce_org_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_org_default.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_org_remarks.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_org_approver_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_org_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_org_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_org_type_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_org_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_type_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_approver_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_remarks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_default, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_org_active, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(549, 408)
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(551, 429)
        '
        'xtp_edit
        '
        Me.xtp_edit.Size = New System.Drawing.Size(549, 341)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(539, 281)
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(539, 398)
        Me.gc_master.TabIndex = 0
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        '
        'lci_master
        '
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.lci_master.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.lci_master.Controls.Add(Me.sc_ce_org_active)
        Me.lci_master.Controls.Add(Me.sc_ce_org_default)
        Me.lci_master.Controls.Add(Me.sc_te_org_remarks)
        Me.lci_master.Controls.Add(Me.sc_le_org_approver_id)
        Me.lci_master.Controls.Add(Me.sc_te_org_name)
        Me.lci_master.Controls.Add(Me.sc_te_org_code)
        Me.lci_master.Controls.Add(Me.sc_le_org_type_id)
        Me.lci_master.Controls.Add(Me.sc_le_org_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(539, 281)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'sc_ce_org_active
        '
        Me.sc_ce_org_active.Location = New System.Drawing.Point(7, 223)
        Me.sc_ce_org_active.Name = "sc_ce_org_active"
        Me.sc_ce_org_active.Properties.Caption = "Is Active"
        Me.sc_ce_org_active.Size = New System.Drawing.Size(526, 19)
        Me.sc_ce_org_active.StyleController = Me.lci_master
        Me.sc_ce_org_active.TabIndex = 11
        '
        'sc_ce_org_default
        '
        Me.sc_ce_org_default.Location = New System.Drawing.Point(7, 193)
        Me.sc_ce_org_default.Name = "sc_ce_org_default"
        Me.sc_ce_org_default.Properties.Caption = "Is Default"
        Me.sc_ce_org_default.Size = New System.Drawing.Size(526, 19)
        Me.sc_ce_org_default.StyleController = Me.lci_master
        Me.sc_ce_org_default.TabIndex = 10
        '
        'sc_te_org_remarks
        '
        Me.sc_te_org_remarks.Location = New System.Drawing.Point(100, 162)
        Me.sc_te_org_remarks.Name = "sc_te_org_remarks"
        Me.sc_te_org_remarks.Size = New System.Drawing.Size(433, 20)
        Me.sc_te_org_remarks.StyleController = Me.lci_master
        Me.sc_te_org_remarks.TabIndex = 9
        '
        'sc_le_org_approver_id
        '
        Me.sc_le_org_approver_id.Location = New System.Drawing.Point(100, 131)
        Me.sc_le_org_approver_id.Name = "sc_le_org_approver_id"
        Me.sc_le_org_approver_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_org_approver_id.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_org_approver_id.StyleController = Me.lci_master
        Me.sc_le_org_approver_id.TabIndex = 8
        '
        'sc_te_org_name
        '
        Me.sc_te_org_name.Location = New System.Drawing.Point(100, 100)
        Me.sc_te_org_name.Name = "sc_te_org_name"
        Me.sc_te_org_name.Size = New System.Drawing.Size(433, 20)
        Me.sc_te_org_name.StyleController = Me.lci_master
        Me.sc_te_org_name.TabIndex = 7
        '
        'sc_te_org_code
        '
        Me.sc_te_org_code.Location = New System.Drawing.Point(100, 69)
        Me.sc_te_org_code.Name = "sc_te_org_code"
        Me.sc_te_org_code.Size = New System.Drawing.Size(433, 20)
        Me.sc_te_org_code.StyleController = Me.lci_master
        Me.sc_te_org_code.TabIndex = 6
        '
        'sc_le_org_type_id
        '
        Me.sc_le_org_type_id.Location = New System.Drawing.Point(100, 38)
        Me.sc_le_org_type_id.Name = "sc_le_org_type_id"
        Me.sc_le_org_type_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_org_type_id.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_org_type_id.StyleController = Me.lci_master
        Me.sc_le_org_type_id.TabIndex = 5
        '
        'sc_le_org_en_id
        '
        Me.sc_le_org_en_id.Location = New System.Drawing.Point(100, 7)
        Me.sc_le_org_en_id.Name = "sc_le_org_en_id"
        Me.sc_le_org_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_org_en_id.Size = New System.Drawing.Size(433, 20)
        Me.sc_le_org_en_id.StyleController = Me.lci_master
        Me.sc_le_org_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_entity, Me.lci_org_type_id, Me.lci_org_code, Me.lci_org_name, Me.lci_org_approver_id, Me.lci_org_remarks, Me.lci_org_default, Me.lci_org_active})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(539, 281)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_entity
        '
        Me.lci_entity.Control = Me.sc_le_org_en_id
        Me.lci_entity.CustomizationFormText = "Entity"
        Me.lci_entity.Location = New System.Drawing.Point(0, 0)
        Me.lci_entity.Name = "lci_entity"
        Me.lci_entity.Size = New System.Drawing.Size(537, 31)
        Me.lci_entity.Text = "Entity"
        Me.lci_entity.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_entity.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_type_id
        '
        Me.lci_org_type_id.Control = Me.sc_le_org_type_id
        Me.lci_org_type_id.CustomizationFormText = "Organization Type"
        Me.lci_org_type_id.Location = New System.Drawing.Point(0, 31)
        Me.lci_org_type_id.Name = "lci_org_type_id"
        Me.lci_org_type_id.Size = New System.Drawing.Size(537, 31)
        Me.lci_org_type_id.Text = "Organization Type"
        Me.lci_org_type_id.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_type_id.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_code
        '
        Me.lci_org_code.Control = Me.sc_te_org_code
        Me.lci_org_code.CustomizationFormText = "Code"
        Me.lci_org_code.Location = New System.Drawing.Point(0, 62)
        Me.lci_org_code.Name = "lci_org_code"
        Me.lci_org_code.Size = New System.Drawing.Size(537, 31)
        Me.lci_org_code.Text = "Code"
        Me.lci_org_code.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_code.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_name
        '
        Me.lci_org_name.Control = Me.sc_te_org_name
        Me.lci_org_name.CustomizationFormText = "Name"
        Me.lci_org_name.Location = New System.Drawing.Point(0, 93)
        Me.lci_org_name.Name = "lci_org_name"
        Me.lci_org_name.Size = New System.Drawing.Size(537, 31)
        Me.lci_org_name.Text = "Name"
        Me.lci_org_name.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_name.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_approver_id
        '
        Me.lci_org_approver_id.Control = Me.sc_le_org_approver_id
        Me.lci_org_approver_id.CustomizationFormText = "Approver"
        Me.lci_org_approver_id.Location = New System.Drawing.Point(0, 124)
        Me.lci_org_approver_id.Name = "lci_org_approver_id"
        Me.lci_org_approver_id.Size = New System.Drawing.Size(537, 31)
        Me.lci_org_approver_id.Text = "Approver"
        Me.lci_org_approver_id.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_approver_id.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_remarks
        '
        Me.lci_org_remarks.Control = Me.sc_te_org_remarks
        Me.lci_org_remarks.CustomizationFormText = "Remarks"
        Me.lci_org_remarks.Location = New System.Drawing.Point(0, 155)
        Me.lci_org_remarks.Name = "lci_org_remarks"
        Me.lci_org_remarks.Size = New System.Drawing.Size(537, 31)
        Me.lci_org_remarks.Text = "Remarks"
        Me.lci_org_remarks.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_remarks.TextSize = New System.Drawing.Size(88, 20)
        '
        'lci_org_default
        '
        Me.lci_org_default.Control = Me.sc_ce_org_default
        Me.lci_org_default.CustomizationFormText = "lci_org_default"
        Me.lci_org_default.Location = New System.Drawing.Point(0, 186)
        Me.lci_org_default.Name = "lci_org_default"
        Me.lci_org_default.Size = New System.Drawing.Size(537, 30)
        Me.lci_org_default.Text = "lci_org_default"
        Me.lci_org_default.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_default.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_org_default.TextToControlDistance = 0
        Me.lci_org_default.TextVisible = False
        '
        'lci_org_active
        '
        Me.lci_org_active.Control = Me.sc_ce_org_active
        Me.lci_org_active.CustomizationFormText = "Is Active"
        Me.lci_org_active.Location = New System.Drawing.Point(0, 216)
        Me.lci_org_active.Name = "lci_org_active"
        Me.lci_org_active.Size = New System.Drawing.Size(537, 63)
        Me.lci_org_active.Text = "Is Active"
        Me.lci_org_active.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_org_active.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_org_active.TextToControlDistance = 0
        Me.lci_org_active.TextVisible = False
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FOrganization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FOrganization"
        Me.Text = "Organization"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.sc_ce_org_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_org_default.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_org_remarks.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_org_approver_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_org_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_org_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_org_type_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_org_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_type_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_approver_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_remarks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_default, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_org_active, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents sc_te_org_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_te_org_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_le_org_type_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_org_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lci_entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_type_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_name As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_ce_org_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_ce_org_default As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_te_org_remarks As DevExpress.XtraEditors.TextEdit
    Friend WithEvents sc_le_org_approver_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lci_org_approver_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_remarks As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_default As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_org_active As DevExpress.XtraLayout.LayoutControlItem

End Class
