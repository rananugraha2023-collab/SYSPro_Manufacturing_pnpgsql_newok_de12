<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSalesOrderShipmentPrint
    Inherits master_new.MasterInfTwo

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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl()
        Me.CheckEditNew = New DevExpress.XtraEditors.CheckEdit()
        Me.soship_ce_lbl = New DevExpress.XtraEditors.CheckEdit()
        Me.be_to = New DevExpress.XtraEditors.ButtonEdit()
        Me.be_first = New DevExpress.XtraEditors.ButtonEdit()
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.CheckEditNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.soship_ce_lbl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.lci_master)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.CheckEditNew)
        Me.lci_master.Controls.Add(Me.soship_ce_lbl)
        Me.lci_master.Controls.Add(Me.be_to)
        Me.lci_master.Controls.Add(Me.be_first)
        Me.lci_master.Controls.Add(Me.le_entity)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(555, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 6
        Me.lci_master.Text = "LayoutControl1"
        '
        'CheckEditNew
        '
        Me.CheckEditNew.Location = New System.Drawing.Point(430, 43)
        Me.CheckEditNew.Name = "CheckEditNew"
        Me.CheckEditNew.Properties.Caption = "New"
        Me.CheckEditNew.Size = New System.Drawing.Size(101, 19)
        Me.CheckEditNew.StyleController = Me.lci_master
        Me.CheckEditNew.TabIndex = 11
        '
        'soship_ce_lbl
        '
        Me.soship_ce_lbl.Location = New System.Drawing.Point(326, 43)
        Me.soship_ce_lbl.Name = "soship_ce_lbl"
        Me.soship_ce_lbl.Properties.Caption = "Package Label"
        Me.soship_ce_lbl.Size = New System.Drawing.Size(100, 19)
        Me.soship_ce_lbl.StyleController = Me.lci_master
        Me.soship_ce_lbl.TabIndex = 10
        '
        'be_to
        '
        Me.be_to.Location = New System.Drawing.Point(432, 67)
        Me.be_to.Name = "be_to"
        Me.be_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.be_to.Size = New System.Drawing.Size(99, 20)
        Me.be_to.StyleController = Me.lci_master
        Me.be_to.TabIndex = 9
        '
        'be_first
        '
        Me.be_first.Location = New System.Drawing.Point(130, 67)
        Me.be_first.Name = "be_first"
        Me.be_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.be_first.Size = New System.Drawing.Size(192, 20)
        Me.be_first.StyleController = Me.lci_master
        Me.be_first.TabIndex = 8
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(130, 43)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(192, 20)
        Me.le_entity.StyleController = Me.lci_master
        Me.le_entity.TabIndex = 7
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem8, Me.LayoutControlGroup6})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(555, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'EmptySpaceItem8
        '
        Me.EmptySpaceItem8.AllowHotTrack = False
        Me.EmptySpaceItem8.CustomizationFormText = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Location = New System.Drawing.Point(0, 91)
        Me.EmptySpaceItem8.Name = "EmptySpaceItem8"
        Me.EmptySpaceItem8.Size = New System.Drawing.Size(535, 322)
        Me.EmptySpaceItem8.Text = "EmptySpaceItem8"
        Me.EmptySpaceItem8.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(535, 91)
        Me.LayoutControlGroup6.Text = "Position"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.le_entity
        Me.LayoutControlItem10.CustomizationFormText = "Entity"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(302, 24)
        Me.LayoutControlItem10.Text = "Entity"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(103, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.be_first
        Me.LayoutControlItem1.CustomizationFormText = "Purchase Order"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(302, 24)
        Me.LayoutControlItem1.Text = "Sales Order Shipment"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(103, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.be_to
        Me.LayoutControlItem2.CustomizationFormText = "To"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(302, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(209, 24)
        Me.LayoutControlItem2.Text = "To"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(103, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.soship_ce_lbl
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(302, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.CheckEditNew
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(406, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(105, 24)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'FSalesOrderShipmentPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FSalesOrderShipmentPrint"
        Me.Text = "Sales Order Shipment Print"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.CheckEditNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.soship_ce_lbl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents be_to As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents be_first As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents soship_ce_lbl As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CheckEditNew As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem

End Class
