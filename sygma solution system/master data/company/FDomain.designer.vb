<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDomain
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
        Me.dom_company = New DevExpress.XtraEditors.TextEdit
        Me.dom_re_ac = New DevExpress.XtraEditors.LookUpEdit
        Me.dom_pl_ac = New DevExpress.XtraEditors.LookUpEdit
        Me.dom_base_cur_id = New DevExpress.XtraEditors.LookUpEdit
        Me.dom_active = New DevExpress.XtraEditors.CheckEdit
        Me.dom_desc = New DevExpress.XtraEditors.TextEdit
        Me.dom_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_dom_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_dom_name = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_dom_active = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.dom_company.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_re_ac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_pl_ac.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_base_cur_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dom_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_active, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lci_master.Controls.Add(Me.dom_company)
        Me.lci_master.Controls.Add(Me.dom_re_ac)
        Me.lci_master.Controls.Add(Me.dom_pl_ac)
        Me.lci_master.Controls.Add(Me.dom_base_cur_id)
        Me.lci_master.Controls.Add(Me.dom_active)
        Me.lci_master.Controls.Add(Me.dom_desc)
        Me.lci_master.Controls.Add(Me.dom_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 367)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'dom_company
        '
        Me.dom_company.Location = New System.Drawing.Point(145, 60)
        Me.dom_company.Name = "dom_company"
        Me.dom_company.Size = New System.Drawing.Size(386, 20)
        Me.dom_company.StyleController = Me.lci_master
        Me.dom_company.TabIndex = 10
        '
        'dom_re_ac
        '
        Me.dom_re_ac.Location = New System.Drawing.Point(145, 132)
        Me.dom_re_ac.Name = "dom_re_ac"
        Me.dom_re_ac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dom_re_ac.Size = New System.Drawing.Size(386, 20)
        Me.dom_re_ac.StyleController = Me.lci_master
        Me.dom_re_ac.TabIndex = 9
        '
        'dom_pl_ac
        '
        Me.dom_pl_ac.Location = New System.Drawing.Point(145, 108)
        Me.dom_pl_ac.Name = "dom_pl_ac"
        Me.dom_pl_ac.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dom_pl_ac.Size = New System.Drawing.Size(386, 20)
        Me.dom_pl_ac.StyleController = Me.lci_master
        Me.dom_pl_ac.TabIndex = 8
        '
        'dom_base_cur_id
        '
        Me.dom_base_cur_id.Location = New System.Drawing.Point(145, 84)
        Me.dom_base_cur_id.Name = "dom_base_cur_id"
        Me.dom_base_cur_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dom_base_cur_id.Size = New System.Drawing.Size(386, 20)
        Me.dom_base_cur_id.StyleController = Me.lci_master
        Me.dom_base_cur_id.TabIndex = 7
        '
        'dom_active
        '
        Me.dom_active.Location = New System.Drawing.Point(12, 156)
        Me.dom_active.Name = "dom_active"
        Me.dom_active.Properties.Caption = "Is Active"
        Me.dom_active.Size = New System.Drawing.Size(519, 19)
        Me.dom_active.StyleController = Me.lci_master
        Me.dom_active.TabIndex = 6
        '
        'dom_desc
        '
        Me.dom_desc.Location = New System.Drawing.Point(145, 36)
        Me.dom_desc.Name = "dom_desc"
        Me.dom_desc.Size = New System.Drawing.Size(386, 20)
        Me.dom_desc.StyleController = Me.lci_master
        Me.dom_desc.TabIndex = 5
        '
        'dom_code
        '
        Me.dom_code.Location = New System.Drawing.Point(145, 12)
        Me.dom_code.Name = "dom_code"
        Me.dom_code.Size = New System.Drawing.Size(386, 20)
        Me.dom_code.StyleController = Me.lci_master
        Me.dom_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_dom_code, Me.lci_dom_name, Me.lci_dom_active, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 367)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_dom_code
        '
        Me.lci_dom_code.Control = Me.dom_code
        Me.lci_dom_code.CustomizationFormText = "Domain Code"
        Me.lci_dom_code.Location = New System.Drawing.Point(0, 0)
        Me.lci_dom_code.Name = "lci_dom_code"
        Me.lci_dom_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_code.Text = "Domain Code"
        Me.lci_dom_code.TextSize = New System.Drawing.Size(129, 13)
        '
        'lci_dom_name
        '
        Me.lci_dom_name.Control = Me.dom_desc
        Me.lci_dom_name.CustomizationFormText = "Domain Name"
        Me.lci_dom_name.Location = New System.Drawing.Point(0, 24)
        Me.lci_dom_name.Name = "lci_dom_name"
        Me.lci_dom_name.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_name.Text = "Domain Name"
        Me.lci_dom_name.TextSize = New System.Drawing.Size(129, 13)
        '
        'lci_dom_active
        '
        Me.lci_dom_active.Control = Me.dom_active
        Me.lci_dom_active.CustomizationFormText = "lci_dom_active"
        Me.lci_dom_active.Location = New System.Drawing.Point(0, 144)
        Me.lci_dom_active.Name = "lci_dom_active"
        Me.lci_dom_active.Size = New System.Drawing.Size(523, 23)
        Me.lci_dom_active.Text = "lci_dom_active"
        Me.lci_dom_active.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_dom_active.TextToControlDistance = 0
        Me.lci_dom_active.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.dom_base_cur_id
        Me.LayoutControlItem1.CustomizationFormText = "Currency"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Currency"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(129, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.dom_pl_ac
        Me.LayoutControlItem2.CustomizationFormText = "Profit / Loss Account"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Profit / Loss Account"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(129, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.dom_re_ac
        Me.LayoutControlItem3.CustomizationFormText = "Retained Earnings Account"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Retained Earnings Account"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(129, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 167)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 180)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.dom_company
        Me.LayoutControlItem4.CustomizationFormText = "Company Name"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem4.Text = "Company Name"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(129, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FDomain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FDomain"
        Me.Text = "Data Domain"
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
        CType(Me.dom_company.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_re_ac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_pl_ac.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_base_cur_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dom_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_active, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents dom_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents dom_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents dom_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_dom_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_dom_name As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_dom_active As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents dom_re_ac As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dom_pl_ac As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents dom_base_cur_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents dom_company As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem

End Class
