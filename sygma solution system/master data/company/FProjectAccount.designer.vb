<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FProjectAccount
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
        Me.sc_le_dt_pjc = New DevExpress.XtraEditors.DateEdit
        Me.sc_le_pjc = New DevExpress.XtraEditors.LookUpEdit
        Me.sc_ce_pjc_active = New DevExpress.XtraEditors.CheckEdit
        Me.sc_te_pjc_code = New DevExpress.XtraEditors.TextEdit
        Me.sc_te_pjc_desc = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_cu_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_cu_desc = New DevExpress.XtraLayout.LayoutControlItem
        Me.Lci_entity = New DevExpress.XtraLayout.LayoutControlItem
        Me.Lci_date = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.sc_le_dt_pjc.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_dt_pjc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_le_pjc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_ce_pjc_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_pjc_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sc_te_pjc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lci_date, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(555, 433)
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
        Me.lci_master.Controls.Add(Me.sc_le_dt_pjc)
        Me.lci_master.Controls.Add(Me.sc_le_pjc)
        Me.lci_master.Controls.Add(Me.sc_ce_pjc_active)
        Me.lci_master.Controls.Add(Me.sc_te_pjc_code)
        Me.lci_master.Controls.Add(Me.sc_te_pjc_desc)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'sc_le_dt_pjc
        '
        Me.sc_le_dt_pjc.EditValue = Nothing
        Me.sc_le_dt_pjc.Location = New System.Drawing.Point(69, 60)
        Me.sc_le_dt_pjc.Name = "sc_le_dt_pjc"
        Me.sc_le_dt_pjc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_dt_pjc.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.sc_le_dt_pjc.Size = New System.Drawing.Size(462, 20)
        Me.sc_le_dt_pjc.StyleController = Me.lci_master
        Me.sc_le_dt_pjc.TabIndex = 10
        '
        'sc_le_pjc
        '
        Me.sc_le_pjc.Location = New System.Drawing.Point(69, 12)
        Me.sc_le_pjc.Name = "sc_le_pjc"
        Me.sc_le_pjc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.sc_le_pjc.Size = New System.Drawing.Size(462, 20)
        Me.sc_le_pjc.StyleController = Me.lci_master
        Me.sc_le_pjc.TabIndex = 9
        '
        'sc_ce_pjc_active
        '
        Me.sc_ce_pjc_active.Location = New System.Drawing.Point(12, 108)
        Me.sc_ce_pjc_active.Name = "sc_ce_pjc_active"
        Me.sc_ce_pjc_active.Properties.Caption = "Is Active"
        Me.sc_ce_pjc_active.Size = New System.Drawing.Size(519, 19)
        Me.sc_ce_pjc_active.StyleController = Me.lci_master
        Me.sc_ce_pjc_active.TabIndex = 6
        '
        'sc_te_pjc_code
        '
        Me.sc_te_pjc_code.Location = New System.Drawing.Point(69, 36)
        Me.sc_te_pjc_code.Name = "sc_te_pjc_code"
        Me.sc_te_pjc_code.Size = New System.Drawing.Size(462, 20)
        Me.sc_te_pjc_code.StyleController = Me.lci_master
        Me.sc_te_pjc_code.TabIndex = 4
        '
        'sc_te_pjc_desc
        '
        Me.sc_te_pjc_desc.Location = New System.Drawing.Point(69, 84)
        Me.sc_te_pjc_desc.Name = "sc_te_pjc_desc"
        Me.sc_te_pjc_desc.Size = New System.Drawing.Size(462, 20)
        Me.sc_te_pjc_desc.StyleController = Me.lci_master
        Me.sc_te_pjc_desc.TabIndex = 7
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_cu_code, Me.LayoutControlItem5, Me.lci_cu_desc, Me.Lci_entity, Me.Lci_date})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_cu_code
        '
        Me.lci_cu_code.Control = Me.sc_te_pjc_code
        Me.lci_cu_code.CustomizationFormText = "Currency Code"
        Me.lci_cu_code.Location = New System.Drawing.Point(0, 24)
        Me.lci_cu_code.Name = "lci_cu_code"
        Me.lci_cu_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_cu_code.Text = "Code"
        Me.lci_cu_code.TextSize = New System.Drawing.Size(53, 13)
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.sc_ce_pjc_active
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 236)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'lci_cu_desc
        '
        Me.lci_cu_desc.Control = Me.sc_te_pjc_desc
        Me.lci_cu_desc.CustomizationFormText = "Description"
        Me.lci_cu_desc.Location = New System.Drawing.Point(0, 72)
        Me.lci_cu_desc.Name = "lci_cu_desc"
        Me.lci_cu_desc.Size = New System.Drawing.Size(523, 24)
        Me.lci_cu_desc.Text = "Description"
        Me.lci_cu_desc.TextSize = New System.Drawing.Size(53, 13)
        '
        'Lci_entity
        '
        Me.Lci_entity.Control = Me.sc_le_pjc
        Me.Lci_entity.CustomizationFormText = "Entity"
        Me.Lci_entity.Location = New System.Drawing.Point(0, 0)
        Me.Lci_entity.Name = "Lci_entity"
        Me.Lci_entity.Size = New System.Drawing.Size(523, 24)
        Me.Lci_entity.Text = "Entity"
        Me.Lci_entity.TextSize = New System.Drawing.Size(53, 13)
        '
        'Lci_date
        '
        Me.Lci_date.Control = Me.sc_le_dt_pjc
        Me.Lci_date.CustomizationFormText = "LayoutControlItem1"
        Me.Lci_date.Location = New System.Drawing.Point(0, 48)
        Me.Lci_date.Name = "Lci_date"
        Me.Lci_date.Size = New System.Drawing.Size(523, 24)
        Me.Lci_date.Text = "Date"
        Me.Lci_date.TextSize = New System.Drawing.Size(53, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FProjectAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FProjectAccount"
        Me.Text = "Project"
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
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.sc_le_dt_pjc.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_dt_pjc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_le_pjc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_ce_pjc_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_pjc_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sc_te_pjc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_cu_desc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lci_entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lci_date, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents sc_ce_pjc_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents sc_te_pjc_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_cu_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents sc_te_pjc_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lci_cu_desc As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_le_pjc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Lci_entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents sc_le_dt_pjc As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Lci_date As DevExpress.XtraLayout.LayoutControlItem

End Class
