<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FOrganizationStructureDetail
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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.sc_le_orgsd_parent_org = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_orgsd_org_id = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_orgsd_org_type = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_le_orgsd_orgs_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_orgsd_orgs_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgsd_org_type = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgsd_org_id = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_orgsd_parent_org = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.sc_le_orgsd_parent_org.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_orgsd_org_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_orgsd_org_type.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_orgsd_orgs_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgsd_orgs_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgsd_org_type, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgsd_org_id, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_orgsd_parent_org, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.xtp_edit.Size = New System.Drawing.Size(549, 408)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
        Me.Panel1.Size = New System.Drawing.Size(539, 348)
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
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = True
        Me.lci_master.Appearance.DisabledLayoutItem.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lci_master.Appearance.DisabledLayoutItem.Options.UseForeColor = True
        Me.lci_master.Controls.Add(Me.sc_le_orgsd_parent_org)
        Me.lci_master.Controls.Add(Me.sc_le_orgsd_org_id)
        Me.lci_master.Controls.Add(Me.sc_le_orgsd_org_type)
        Me.lci_master.Controls.Add(Me.sc_le_orgsd_orgs_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(539, 348)
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'sc_le_orgsd_parent_org
        '
        Me.sc_le_orgsd_parent_org.Location = New System.Drawing.Point(157, 100)
        Me.sc_le_orgsd_parent_org.Name = "sc_le_orgsd_parent_org"
        Me.sc_le_orgsd_parent_org.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_orgsd_parent_org.Size = New System.Drawing.Size(376, 20)
        Me.sc_le_orgsd_parent_org.StyleController = Me.lci_master
        Me.sc_le_orgsd_parent_org.TabIndex = 7
        '
        'sc_le_orgsd_org_id
        '
        Me.sc_le_orgsd_org_id.Location = New System.Drawing.Point(157, 69)
        Me.sc_le_orgsd_org_id.Name = "sc_le_orgsd_org_id"
        Me.sc_le_orgsd_org_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_orgsd_org_id.Size = New System.Drawing.Size(376, 20)
        Me.sc_le_orgsd_org_id.StyleController = Me.lci_master
        Me.sc_le_orgsd_org_id.TabIndex = 6
        '
        'sc_le_orgsd_org_type
        '
        Me.sc_le_orgsd_org_type.Location = New System.Drawing.Point(157, 38)
        Me.sc_le_orgsd_org_type.Name = "sc_le_orgsd_org_type"
        Me.sc_le_orgsd_org_type.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_orgsd_org_type.Size = New System.Drawing.Size(376, 20)
        Me.sc_le_orgsd_org_type.StyleController = Me.lci_master
        Me.sc_le_orgsd_org_type.TabIndex = 5
        '
        'sc_le_orgsd_orgs_id
        '
        Me.sc_le_orgsd_orgs_id.Location = New System.Drawing.Point(157, 7)
        Me.sc_le_orgsd_orgs_id.Name = "sc_le_orgsd_orgs_id"
        Me.sc_le_orgsd_orgs_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_orgsd_orgs_id.Size = New System.Drawing.Size(376, 20)
        Me.sc_le_orgsd_orgs_id.StyleController = Me.lci_master
        Me.sc_le_orgsd_orgs_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_orgsd_orgs_id, Me.lci_orgsd_org_type, Me.lci_orgsd_org_id, Me.lci_orgsd_parent_org})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(539, 348)
        Me.LayoutControlGroup1.Spacing = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_orgsd_orgs_id
        '
        Me.lci_orgsd_orgs_id.Control = Me.sc_le_orgsd_orgs_id
        Me.lci_orgsd_orgs_id.CustomizationFormText = "Organization Structure Master"
        Me.lci_orgsd_orgs_id.Location = New System.Drawing.Point(0, 0)
        Me.lci_orgsd_orgs_id.Name = "lci_orgsd_orgs_id"
        Me.lci_orgsd_orgs_id.Size = New System.Drawing.Size(537, 31)
        Me.lci_orgsd_orgs_id.Text = "Organization Structure Master"
        Me.lci_orgsd_orgs_id.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgsd_orgs_id.TextSize = New System.Drawing.Size(145, 20)
        '
        'lci_orgsd_org_type
        '
        Me.lci_orgsd_org_type.Control = Me.sc_le_orgsd_org_type
        Me.lci_orgsd_org_type.CustomizationFormText = "Organization Type"
        Me.lci_orgsd_org_type.Location = New System.Drawing.Point(0, 31)
        Me.lci_orgsd_org_type.Name = "lci_orgsd_org_type"
        Me.lci_orgsd_org_type.Size = New System.Drawing.Size(537, 31)
        Me.lci_orgsd_org_type.Text = "Organization Type"
        Me.lci_orgsd_org_type.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgsd_org_type.TextSize = New System.Drawing.Size(145, 20)
        '
        'lci_orgsd_org_id
        '
        Me.lci_orgsd_org_id.Control = Me.sc_le_orgsd_org_id
        Me.lci_orgsd_org_id.CustomizationFormText = "Organization Name"
        Me.lci_orgsd_org_id.Location = New System.Drawing.Point(0, 62)
        Me.lci_orgsd_org_id.Name = "lci_orgsd_org_id"
        Me.lci_orgsd_org_id.Size = New System.Drawing.Size(537, 31)
        Me.lci_orgsd_org_id.Text = "Organization Name"
        Me.lci_orgsd_org_id.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgsd_org_id.TextSize = New System.Drawing.Size(145, 20)
        '
        'lci_orgsd_parent_org
        '
        Me.lci_orgsd_parent_org.Control = Me.sc_le_orgsd_parent_org
        Me.lci_orgsd_parent_org.CustomizationFormText = "Parent Organization"
        Me.lci_orgsd_parent_org.Location = New System.Drawing.Point(0, 93)
        Me.lci_orgsd_parent_org.Name = "lci_orgsd_parent_org"
        Me.lci_orgsd_parent_org.Size = New System.Drawing.Size(537, 253)
        Me.lci_orgsd_parent_org.Text = "Parent Organization"
        Me.lci_orgsd_parent_org.TextLocation = DevExpress.Utils.Locations.Left
        Me.lci_orgsd_parent_org.TextSize = New System.Drawing.Size(145, 20)
        '
        'FOrganizationStructureDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FOrganizationStructureDetail"
        Me.Text = "Organization Structure Detail"
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
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.sc_le_orgsd_parent_org.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_orgsd_org_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_orgsd_org_type.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_orgsd_orgs_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgsd_orgs_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgsd_org_type, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgsd_org_id, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_orgsd_parent_org, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents sc_le_orgsd_org_type As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_orgsd_orgs_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lci_orgsd_orgs_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_orgsd_org_type As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_le_orgsd_parent_org As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents sc_le_orgsd_org_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents lci_orgsd_org_id As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_orgsd_parent_org As DevExpress.XtraLayout.LayoutControlItem

End Class
