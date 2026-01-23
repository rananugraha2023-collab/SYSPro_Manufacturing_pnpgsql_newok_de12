<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FVirtualAccount
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
        Me.va_ptnr_id = New DevExpress.XtraEditors.LookUpEdit
        Me.va_bk_id = New DevExpress.XtraEditors.LookUpEdit
        Me.va_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.va_active = New DevExpress.XtraEditors.CheckEdit
        Me.va_name = New DevExpress.XtraEditors.TextEdit
        Me.va_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_dom_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.Description = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.va_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.va_bk_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.va_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.va_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.va_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.va_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Description, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lci_master)
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
        Me.lci_master.Controls.Add(Me.va_ptnr_id)
        Me.lci_master.Controls.Add(Me.va_bk_id)
        Me.lci_master.Controls.Add(Me.va_en_id)
        Me.lci_master.Controls.Add(Me.va_active)
        Me.lci_master.Controls.Add(Me.va_name)
        Me.lci_master.Controls.Add(Me.va_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'va_ptnr_id
        '
        Me.va_ptnr_id.Location = New System.Drawing.Point(69, 108)
        Me.va_ptnr_id.Name = "va_ptnr_id"
        Me.va_ptnr_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.va_ptnr_id.Size = New System.Drawing.Size(462, 20)
        Me.va_ptnr_id.StyleController = Me.lci_master
        Me.va_ptnr_id.TabIndex = 15
        '
        'va_bk_id
        '
        Me.va_bk_id.Location = New System.Drawing.Point(69, 84)
        Me.va_bk_id.Name = "va_bk_id"
        Me.va_bk_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.va_bk_id.Size = New System.Drawing.Size(462, 20)
        Me.va_bk_id.StyleController = Me.lci_master
        Me.va_bk_id.TabIndex = 13
        '
        'va_en_id
        '
        Me.va_en_id.Location = New System.Drawing.Point(69, 12)
        Me.va_en_id.Name = "va_en_id"
        Me.va_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.va_en_id.Size = New System.Drawing.Size(462, 20)
        Me.va_en_id.StyleController = Me.lci_master
        Me.va_en_id.TabIndex = 14
        '
        'va_active
        '
        Me.va_active.Location = New System.Drawing.Point(12, 132)
        Me.va_active.Name = "va_active"
        Me.va_active.Properties.Caption = "Is Active"
        Me.va_active.Size = New System.Drawing.Size(519, 19)
        Me.va_active.StyleController = Me.lci_master
        Me.va_active.TabIndex = 6
        '
        'va_name
        '
        Me.va_name.Location = New System.Drawing.Point(69, 60)
        Me.va_name.Name = "va_name"
        Me.va_name.Size = New System.Drawing.Size(462, 20)
        Me.va_name.StyleController = Me.lci_master
        Me.va_name.TabIndex = 5
        '
        'va_code
        '
        Me.va_code.Location = New System.Drawing.Point(69, 36)
        Me.va_code.Name = "va_code"
        Me.va_code.Size = New System.Drawing.Size(462, 20)
        Me.va_code.StyleController = Me.lci_master
        Me.va_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7, Me.LayoutControlItem3, Me.lci_dom_code, Me.Description, Me.LayoutControlItem10, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 300)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.va_active
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(523, 160)
        Me.LayoutControlItem7.Text = "LayoutControlItem7"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextToControlDistance = 0
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.va_bk_id
        Me.LayoutControlItem3.CustomizationFormText = "Account Type"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Bank Id"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(53, 13)
        '
        'lci_dom_code
        '
        Me.lci_dom_code.Control = Me.va_code
        Me.lci_dom_code.CustomizationFormText = "Domain Code"
        Me.lci_dom_code.Location = New System.Drawing.Point(0, 24)
        Me.lci_dom_code.Name = "lci_dom_code"
        Me.lci_dom_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_code.Text = "VA Code"
        Me.lci_dom_code.TextSize = New System.Drawing.Size(53, 13)
        '
        'Description
        '
        Me.Description.Control = Me.va_name
        Me.Description.CustomizationFormText = "Domain Name"
        Me.Description.Location = New System.Drawing.Point(0, 48)
        Me.Description.Name = "Description"
        Me.Description.Size = New System.Drawing.Size(523, 24)
        Me.Description.Text = "Description"
        Me.Description.TextSize = New System.Drawing.Size(53, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.va_en_id
        Me.LayoutControlItem10.CustomizationFormText = "Entity ID"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem10.Text = "Entity ID"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(53, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.va_ptnr_id
        Me.LayoutControlItem1.CustomizationFormText = "Account Id"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Partner Id"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(53, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.va_name
        Me.LayoutControlItem2.CustomizationFormText = "Domain Name"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem2.Name = "lci_dom_name"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(537, 31)
        Me.LayoutControlItem2.Text = "Account Name"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(95, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.va_bk_id
        Me.LayoutControlItem6.CustomizationFormText = "Account Type"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 124)
        Me.LayoutControlItem6.Name = "LayoutControlItem3"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(537, 31)
        Me.LayoutControlItem6.Text = "Account Type"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(95, 20)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.va_bk_id
        Me.LayoutControlItem9.CustomizationFormText = "Account Type"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 124)
        Me.LayoutControlItem9.Name = "LayoutControlItem3"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(537, 31)
        Me.LayoutControlItem9.Text = "Account Type"
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(95, 20)
        Me.LayoutControlItem9.TextToControlDistance = 5
        '
        'FVirtualAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FVirtualAccount"
        Me.Text = "Virtual Account"
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
        CType(Me.va_ptnr_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.va_bk_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.va_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.va_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.va_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.va_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Description, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents va_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents va_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents va_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_dom_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Description As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents va_bk_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents va_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents va_ptnr_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem

End Class
