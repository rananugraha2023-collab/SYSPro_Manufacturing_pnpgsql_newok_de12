<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FQRSmartLogPrint
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
        Me.invcd_ce_data = New DevExpress.XtraEditors.CheckEdit()
        Me.invqr_gen_qr = New DevExpress.XtraEditors.SimpleButton()
        Me.invcd_en_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.invcd_pt_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.invqr_price_jawa = New DevExpress.XtraEditors.TextEdit()
        Me.invcd_si_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.Entity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.Location = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
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
        CType(Me.invcd_ce_data.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invcd_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invcd_pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_price_jawa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invcd_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Location, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lci_master.Controls.Add(Me.invcd_ce_data)
        Me.lci_master.Controls.Add(Me.invqr_gen_qr)
        Me.lci_master.Controls.Add(Me.invcd_en_id)
        Me.lci_master.Controls.Add(Me.invcd_pt_id)
        Me.lci_master.Controls.Add(Me.invqr_price_jawa)
        Me.lci_master.Controls.Add(Me.invcd_si_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 300)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'invcd_ce_data
        '
        Me.invcd_ce_data.Location = New System.Drawing.Point(12, 36)
        Me.invcd_ce_data.Name = "invcd_ce_data"
        Me.invcd_ce_data.Properties.Caption = "With Data"
        Me.invcd_ce_data.Size = New System.Drawing.Size(257, 19)
        Me.invcd_ce_data.StyleController = Me.lci_master
        Me.invcd_ce_data.TabIndex = 14
        '
        'invqr_gen_qr
        '
        Me.invqr_gen_qr.Location = New System.Drawing.Point(12, 83)
        Me.invqr_gen_qr.Name = "invqr_gen_qr"
        Me.invqr_gen_qr.Size = New System.Drawing.Size(519, 22)
        Me.invqr_gen_qr.StyleController = Me.lci_master
        Me.invqr_gen_qr.TabIndex = 13
        Me.invqr_gen_qr.Text = "Generate"
        '
        'invcd_en_id
        '
        Me.invcd_en_id.Location = New System.Drawing.Point(336, 12)
        Me.invcd_en_id.Name = "invcd_en_id"
        Me.invcd_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invcd_en_id.Size = New System.Drawing.Size(195, 20)
        Me.invcd_en_id.StyleController = Me.lci_master
        Me.invcd_en_id.TabIndex = 9
        '
        'invcd_pt_id
        '
        Me.invcd_pt_id.Location = New System.Drawing.Point(75, 59)
        Me.invcd_pt_id.Name = "invcd_pt_id"
        Me.invcd_pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.invcd_pt_id.Size = New System.Drawing.Size(194, 20)
        Me.invcd_pt_id.StyleController = Me.lci_master
        Me.invcd_pt_id.TabIndex = 6
        '
        'invqr_price_jawa
        '
        Me.invqr_price_jawa.Location = New System.Drawing.Point(336, 59)
        Me.invqr_price_jawa.Name = "invqr_price_jawa"
        Me.invqr_price_jawa.Properties.Appearance.Options.UseTextOptions = True
        Me.invqr_price_jawa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invqr_price_jawa.Properties.DisplayFormat.FormatString = "n"
        Me.invqr_price_jawa.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_jawa.Properties.EditFormat.FormatString = "n"
        Me.invqr_price_jawa.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_price_jawa.Properties.Mask.EditMask = "n"
        Me.invqr_price_jawa.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invqr_price_jawa.Size = New System.Drawing.Size(195, 20)
        Me.invqr_price_jawa.StyleController = Me.lci_master
        Me.invqr_price_jawa.TabIndex = 5
        '
        'invcd_si_id
        '
        Me.invcd_si_id.Location = New System.Drawing.Point(75, 12)
        Me.invcd_si_id.Name = "invcd_si_id"
        Me.invcd_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invcd_si_id.Size = New System.Drawing.Size(194, 20)
        Me.invcd_si_id.StyleController = Me.lci_master
        Me.invcd_si_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.EmptySpaceItem1, Me.Entity, Me.LayoutControlItem3, Me.LayoutControlItem2, Me.Location, Me.LayoutControlItem4, Me.EmptySpaceItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 300)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.invcd_si_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem1.Text = "Site"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(60, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 97)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 183)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'Entity
        '
        Me.Entity.Control = Me.invcd_en_id
        Me.Entity.CustomizationFormText = "Entity"
        Me.Entity.Location = New System.Drawing.Point(261, 0)
        Me.Entity.Name = "Entity"
        Me.Entity.Size = New System.Drawing.Size(262, 24)
        Me.Entity.Text = "Entity"
        Me.Entity.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.invcd_pt_id
        Me.LayoutControlItem3.CustomizationFormText = "Part Number"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 47)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(261, 24)
        Me.LayoutControlItem3.Text = "Part Number"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.invqr_gen_qr
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 71)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 26)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'Location
        '
        Me.Location.Control = Me.invqr_price_jawa
        Me.Location.CustomizationFormText = "Location"
        Me.Location.Location = New System.Drawing.Point(261, 47)
        Me.Location.Name = "Location"
        Me.Location.Size = New System.Drawing.Size(262, 24)
        Me.Location.Text = "Location"
        Me.Location.TextSize = New System.Drawing.Size(60, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.invcd_ce_data
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(261, 23)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(261, 24)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(262, 23)
        Me.EmptySpaceItem2.Text = "EmptySpaceItem2"
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'FQRSmartLogPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FQRSmartLogPrint"
        Me.Text = "QR Code Print"
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
        CType(Me.invcd_ce_data.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invcd_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invcd_pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_price_jawa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invcd_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Location, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents invcd_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Location As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invcd_pt_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents invcd_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Entity As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invqr_price_jawa As DevExpress.XtraEditors.TextEdit
    Friend WithEvents invqr_gen_qr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents invcd_ce_data As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem

End Class
