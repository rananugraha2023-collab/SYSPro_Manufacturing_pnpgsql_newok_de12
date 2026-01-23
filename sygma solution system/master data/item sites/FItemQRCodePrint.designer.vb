<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FItemQRCodePrint
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
        Me.components = New System.ComponentModel.Container()
        Me.gc_master = New DevExpress.XtraGrid.GridControl()
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl()
        Me.invqr_pi_desc = New DevExpress.XtraEditors.ButtonEdit()
        Me.invqr_price_ljawa = New DevExpress.XtraEditors.TextEdit()
        Me.invqr_en_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.invqr_pt_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.invqr_price_jawa = New DevExpress.XtraEditors.TextEdit()
        Me.invqr_si_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PulauJawa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.Entity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LuarJawa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PriceLists = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.invqr_pi_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_price_ljawa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_price_jawa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PulauJawa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LuarJawa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PriceLists, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lci_master.Controls.Add(Me.invqr_pi_desc)
        Me.lci_master.Controls.Add(Me.invqr_price_ljawa)
        Me.lci_master.Controls.Add(Me.invqr_en_id)
        Me.lci_master.Controls.Add(Me.invqr_pt_id)
        Me.lci_master.Controls.Add(Me.invqr_price_jawa)
        Me.lci_master.Controls.Add(Me.invqr_si_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'invqr_pi_desc
        '
        Me.invqr_pi_desc.Location = New System.Drawing.Point(75, 36)
        Me.invqr_pi_desc.Name = "invqr_pi_desc"
        Me.invqr_pi_desc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.invqr_pi_desc.Size = New System.Drawing.Size(194, 20)
        Me.invqr_pi_desc.StyleController = Me.lci_master
        Me.invqr_pi_desc.TabIndex = 12
        '
        'invqr_price_ljawa
        '
        Me.invqr_price_ljawa.Location = New System.Drawing.Point(336, 60)
        Me.invqr_price_ljawa.Name = "invqr_price_ljawa"
        Me.invqr_price_ljawa.Properties.Appearance.Options.UseTextOptions = True
        Me.invqr_price_ljawa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invqr_price_ljawa.Properties.DisplayFormat.FormatString = "n"
        Me.invqr_price_ljawa.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_ljawa.Properties.EditFormat.FormatString = "n"
        Me.invqr_price_ljawa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_ljawa.Properties.Mask.EditMask = "n"
        Me.invqr_price_ljawa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invqr_price_ljawa.Size = New System.Drawing.Size(195, 20)
        Me.invqr_price_ljawa.StyleController = Me.lci_master
        Me.invqr_price_ljawa.TabIndex = 10
        '
        'invqr_en_id
        '
        Me.invqr_en_id.Location = New System.Drawing.Point(336, 12)
        Me.invqr_en_id.Name = "invqr_en_id"
        Me.invqr_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invqr_en_id.Size = New System.Drawing.Size(195, 20)
        Me.invqr_en_id.StyleController = Me.lci_master
        Me.invqr_en_id.TabIndex = 9
        '
        'invqr_pt_id
        '
        Me.invqr_pt_id.Location = New System.Drawing.Point(336, 36)
        Me.invqr_pt_id.Name = "invqr_pt_id"
        Me.invqr_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.invqr_pt_id.Size = New System.Drawing.Size(195, 20)
        Me.invqr_pt_id.StyleController = Me.lci_master
        Me.invqr_pt_id.TabIndex = 6
        '
        'invqr_price_jawa
        '
        Me.invqr_price_jawa.Location = New System.Drawing.Point(75, 60)
        Me.invqr_price_jawa.Name = "invqr_price_jawa"
        Me.invqr_price_jawa.Properties.Appearance.Options.UseTextOptions = True
        Me.invqr_price_jawa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invqr_price_jawa.Properties.DisplayFormat.FormatString = "n"
        Me.invqr_price_jawa.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_jawa.Properties.EditFormat.FormatString = "n"
        Me.invqr_price_jawa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_jawa.Properties.Mask.EditMask = "n"
        Me.invqr_price_jawa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invqr_price_jawa.Size = New System.Drawing.Size(194, 20)
        Me.invqr_price_jawa.StyleController = Me.lci_master
        Me.invqr_price_jawa.TabIndex = 5
        '
        'invqr_si_id
        '
        Me.invqr_si_id.Location = New System.Drawing.Point(75, 12)
        Me.invqr_si_id.Name = "invqr_si_id"
        Me.invqr_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invqr_si_id.Size = New System.Drawing.Size(194, 20)
        Me.invqr_si_id.StyleController = Me.lci_master
        Me.invqr_si_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.PulauJawa, Me.EmptySpaceItem1, Me.Entity, Me.LuarJawa, Me.LayoutControlItem3, Me.PriceLists})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 300)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.invqr_si_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem1.Text = "Site"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(60, 13)
        '
        'PulauJawa
        '
        Me.PulauJawa.Control = Me.invqr_price_jawa
        Me.PulauJawa.CustomizationFormText = "Pulau Jawa"
        Me.PulauJawa.Location = New System.Drawing.Point(0, 48)
        Me.PulauJawa.Name = "PulauJawa"
        Me.PulauJawa.Size = New System.Drawing.Size(261, 24)
        Me.PulauJawa.Text = "Pulau Jawa"
        Me.PulauJawa.TextSize = New System.Drawing.Size(60, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 208)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'Entity
        '
        Me.Entity.Control = Me.invqr_en_id
        Me.Entity.CustomizationFormText = "Entity"
        Me.Entity.Location = New System.Drawing.Point(261, 0)
        Me.Entity.Name = "Entity"
        Me.Entity.Size = New System.Drawing.Size(262, 24)
        Me.Entity.Text = "Entity"
        Me.Entity.TextSize = New System.Drawing.Size(60, 13)
        '
        'LuarJawa
        '
        Me.LuarJawa.Control = Me.invqr_price_ljawa
        Me.LuarJawa.CustomizationFormText = "LuarJawa"
        Me.LuarJawa.Location = New System.Drawing.Point(261, 48)
        Me.LuarJawa.Name = "LuarJawa"
        Me.LuarJawa.Size = New System.Drawing.Size(262, 24)
        Me.LuarJawa.Text = "LuarJawa"
        Me.LuarJawa.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.invqr_pt_id
        Me.LayoutControlItem3.CustomizationFormText = "Part Number"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(261, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(262, 24)
        Me.LayoutControlItem3.Text = "Part Number"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(60, 13)
        '
        'PriceLists
        '
        Me.PriceLists.Control = Me.invqr_pi_desc
        Me.PriceLists.CustomizationFormText = "Price Lists"
        Me.PriceLists.Location = New System.Drawing.Point(0, 24)
        Me.PriceLists.Name = "PriceLists"
        Me.PriceLists.Size = New System.Drawing.Size(261, 24)
        Me.PriceLists.Text = "Price Lists"
        Me.PriceLists.TextSize = New System.Drawing.Size(60, 13)
        '
        'FItemQRCodePrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FItemQRCodePrint"
        Me.Text = "Item QR Code Print"
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
        CType(Me.invqr_pi_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_price_ljawa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_price_jawa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PulauJawa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LuarJawa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PriceLists, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents invqr_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PulauJawa As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invqr_pt_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents invqr_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LuarJawa As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invqr_price_jawa As DevExpress.XtraEditors.TextEdit
    Public WithEvents invqr_price_ljawa As DevExpress.XtraEditors.TextEdit
    Public WithEvents invqr_pi_desc As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents PriceLists As DevExpress.XtraLayout.LayoutControlItem

End Class
