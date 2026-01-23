<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FItemLeadTime
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
        Me.le_invld_en = New DevExpress.XtraEditors.LookUpEdit
        Me.invld_pt_id = New DevExpress.XtraEditors.ButtonEdit
        Me.invld_lead = New DevExpress.XtraEditors.TextEdit
        Me.invld_si_id = New DevExpress.XtraEditors.LookUpEdit
        Me.invld_monitored = New DevExpress.XtraEditors.CheckEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.Entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.invld_weight = New DevExpress.XtraEditors.TextEdit
        Me.Weight = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.le_invld_en.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invld_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invld_lead.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invld_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invld_monitored.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invld_weight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Weight, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(543, 367)
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
        Me.gc_master.TabIndex = 1
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
        Me.lci_master.Controls.Add(Me.invld_weight)
        Me.lci_master.Controls.Add(Me.le_invld_en)
        Me.lci_master.Controls.Add(Me.invld_pt_id)
        Me.lci_master.Controls.Add(Me.invld_lead)
        Me.lci_master.Controls.Add(Me.invld_si_id)
        Me.lci_master.Controls.Add(Me.invld_monitored)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 367)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'le_invld_en
        '
        Me.le_invld_en.Location = New System.Drawing.Point(337, 12)
        Me.le_invld_en.Name = "le_invld_en"
        Me.le_invld_en.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_invld_en.Size = New System.Drawing.Size(194, 20)
        Me.le_invld_en.StyleController = Me.lci_master
        Me.le_invld_en.TabIndex = 9
        '
        'invld_pt_id
        '
        Me.invld_pt_id.Location = New System.Drawing.Point(76, 36)
        Me.invld_pt_id.Name = "invld_pt_id"
        Me.invld_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.invld_pt_id.Size = New System.Drawing.Size(455, 20)
        Me.invld_pt_id.StyleController = Me.lci_master
        Me.invld_pt_id.TabIndex = 6
        '
        'invld_lead
        '
        Me.invld_lead.Location = New System.Drawing.Point(76, 60)
        Me.invld_lead.Name = "invld_lead"
        Me.invld_lead.Properties.Appearance.Options.UseTextOptions = True
        Me.invld_lead.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invld_lead.Properties.DisplayFormat.FormatString = "n"
        Me.invld_lead.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invld_lead.Properties.EditFormat.FormatString = "n"
        Me.invld_lead.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invld_lead.Properties.Mask.EditMask = "n"
        Me.invld_lead.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invld_lead.Size = New System.Drawing.Size(193, 20)
        Me.invld_lead.StyleController = Me.lci_master
        Me.invld_lead.TabIndex = 5
        '
        'invld_si_id
        '
        Me.invld_si_id.Location = New System.Drawing.Point(76, 12)
        Me.invld_si_id.Name = "invld_si_id"
        Me.invld_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invld_si_id.Size = New System.Drawing.Size(193, 20)
        Me.invld_si_id.StyleController = Me.lci_master
        Me.invld_si_id.TabIndex = 4
        '
        'invld_monitored
        '
        Me.invld_monitored.Location = New System.Drawing.Point(12, 84)
        Me.invld_monitored.Name = "invld_monitored"
        Me.invld_monitored.Properties.Caption = "Monitored"
        Me.invld_monitored.Size = New System.Drawing.Size(519, 19)
        Me.invld_monitored.StyleController = Me.lci_master
        Me.invld_monitored.TabIndex = 8
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.EmptySpaceItem1, Me.Entity, Me.Weight})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 367)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.invld_si_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem1.Text = "Site"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.invld_lead
        Me.LayoutControlItem2.CustomizationFormText = "Lead"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem2.Text = "Lead Time"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.invld_pt_id
        Me.LayoutControlItem3.CustomizationFormText = "Part Number"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Part Number"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.invld_monitored
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 23)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 95)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 252)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'Entity
        '
        Me.Entity.Control = Me.le_invld_en
        Me.Entity.CustomizationFormText = "Entity"
        Me.Entity.Location = New System.Drawing.Point(261, 0)
        Me.Entity.Name = "Entity"
        Me.Entity.Size = New System.Drawing.Size(262, 24)
        Me.Entity.Text = "Entity"
        Me.Entity.TextSize = New System.Drawing.Size(60, 13)
        '
        'invld_weight
        '
        Me.invld_weight.Location = New System.Drawing.Point(337, 60)
        Me.invld_weight.Name = "invld_weight"
        Me.invld_weight.Size = New System.Drawing.Size(194, 20)
        Me.invld_weight.StyleController = Me.lci_master
        Me.invld_weight.TabIndex = 10
        '
        'Weight
        '
        Me.Weight.Control = Me.invld_weight
        Me.Weight.CustomizationFormText = "Weight"
        Me.Weight.Location = New System.Drawing.Point(261, 48)
        Me.Weight.Name = "Weight"
        Me.Weight.Size = New System.Drawing.Size(262, 24)
        Me.Weight.Text = "Weight"
        Me.Weight.TextSize = New System.Drawing.Size(60, 13)
        '
        'FItemLeadTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FItemLeadTime"
        Me.Text = "Item Lead Time"
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
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.le_invld_en.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invld_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invld_lead.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invld_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invld_monitored.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invld_weight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Weight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents invld_lead As DevExpress.XtraEditors.TextEdit
    Friend WithEvents invld_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invld_pt_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents invld_monitored As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents le_invld_en As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents invld_weight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Weight As DevExpress.XtraLayout.LayoutControlItem

End Class
