<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FUpdateStock
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
        Me.components = New System.ComponentModel.Container
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.lci_master = New DevExpress.XtraLayout.LayoutControl
        Me.BtSOShipment = New DevExpress.XtraEditors.SimpleButton
        Me.BtTransfer = New DevExpress.XtraEditors.SimpleButton
        Me.BtLoad = New DevExpress.XtraEditors.SimpleButton
        Me.gc_loc = New DevExpress.XtraGrid.GridControl
        Me.gv_loc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.BtUpdate = New DevExpress.XtraEditors.SimpleButton
        Me.par_loc = New DevExpress.XtraEditors.LookUpEdit
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit
        Me.LookUpEdit1 = New DevExpress.XtraEditors.LookUpEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.par_loc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel2.Controls.Add(Me.lci_master)
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        Me.scc_master.Size = New System.Drawing.Size(970, 433)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.BtSOShipment)
        Me.lci_master.Controls.Add(Me.BtTransfer)
        Me.lci_master.Controls.Add(Me.BtLoad)
        Me.lci_master.Controls.Add(Me.gc_loc)
        Me.lci_master.Controls.Add(Me.BtUpdate)
        Me.lci_master.Controls.Add(Me.par_loc)
        Me.lci_master.Controls.Add(Me.le_entity)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(970, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 6
        Me.lci_master.Text = "LayoutControl1"
        '
        'BtSOShipment
        '
        Me.BtSOShipment.Location = New System.Drawing.Point(718, 70)
        Me.BtSOShipment.Name = "BtSOShipment"
        Me.BtSOShipment.Size = New System.Drawing.Size(112, 22)
        Me.BtSOShipment.StyleController = Me.lci_master
        Me.BtSOShipment.TabIndex = 11
        Me.BtSOShipment.Text = "SO Shipment"
        '
        'BtTransfer
        '
        Me.BtTransfer.Location = New System.Drawing.Point(486, 70)
        Me.BtTransfer.Name = "BtTransfer"
        Me.BtTransfer.Size = New System.Drawing.Size(228, 22)
        Me.BtTransfer.StyleController = Me.lci_master
        Me.BtTransfer.TabIndex = 10
        Me.BtTransfer.Text = "Transfer Issue"
        '
        'BtLoad
        '
        Me.BtLoad.Location = New System.Drawing.Point(486, 44)
        Me.BtLoad.Name = "BtLoad"
        Me.BtLoad.Size = New System.Drawing.Size(228, 22)
        Me.BtLoad.StyleController = Me.lci_master
        Me.BtLoad.TabIndex = 9
        Me.BtLoad.Text = "Load Data"
        '
        'gc_loc
        '
        Me.gc_loc.Location = New System.Drawing.Point(12, 108)
        Me.gc_loc.MainView = Me.gv_loc
        Me.gc_loc.Name = "gc_loc"
        Me.gc_loc.Size = New System.Drawing.Size(946, 313)
        Me.gc_loc.TabIndex = 4
        Me.gc_loc.UseEmbeddedNavigator = True
        Me.gc_loc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_loc})
        '
        'gv_loc
        '
        Me.gv_loc.GridControl = Me.gc_loc
        Me.gv_loc.Name = "gv_loc"
        '
        'BtUpdate
        '
        Me.BtUpdate.Location = New System.Drawing.Point(718, 44)
        Me.BtUpdate.Name = "BtUpdate"
        Me.BtUpdate.Size = New System.Drawing.Size(228, 22)
        Me.BtUpdate.StyleController = Me.lci_master
        Me.BtUpdate.TabIndex = 8
        Me.BtUpdate.Text = "Update"
        '
        'par_loc
        '
        Me.par_loc.Location = New System.Drawing.Point(68, 68)
        Me.par_loc.Name = "par_loc"
        Me.par_loc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.par_loc.Size = New System.Drawing.Size(414, 20)
        Me.par_loc.StyleController = Me.lci_master
        Me.par_loc.TabIndex = 5
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(68, 44)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(414, 20)
        Me.le_entity.StyleController = Me.lci_master
        Me.le_entity.TabIndex = 7
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6, Me.LayoutControlItem2})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(970, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem4, Me.EmptySpaceItem3, Me.LayoutControlItem1, Me.LayoutControlItem3, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(950, 96)
        Me.LayoutControlGroup6.Text = "Position"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.le_entity
        Me.LayoutControlItem10.CustomizationFormText = "Entity"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(462, 24)
        Me.LayoutControlItem10.Text = "Entity"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(40, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.par_loc
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(462, 28)
        Me.LayoutControlItem4.Text = "Location"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(40, 13)
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.CustomizationFormText = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(810, 26)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(116, 26)
        Me.EmptySpaceItem3.Text = "EmptySpaceItem3"
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.BtUpdate
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(694, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(232, 26)
        Me.LayoutControlItem1.Text = "LayoutControlItem1"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.BtLoad
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(462, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(232, 26)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.BtTransfer
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(462, 26)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(232, 26)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.BtSOShipment
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(694, 26)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(116, 26)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.gc_loc
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(950, 317)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.[Default]
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Location = New System.Drawing.Point(0, 0)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.DateEdit1.Size = New System.Drawing.Size(100, 20)
        Me.DateEdit1.TabIndex = 2
        '
        'LookUpEdit1
        '
        Me.LookUpEdit1.Location = New System.Drawing.Point(0, 0)
        Me.LookUpEdit1.Name = "LookUpEdit1"
        Me.LookUpEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LookUpEdit1.Size = New System.Drawing.Size(100, 20)
        Me.LookUpEdit1.TabIndex = 3
        '
        'FUpdateStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(970, 433)
        Me.Controls.Add(Me.LookUpEdit1)
        Me.Controls.Add(Me.DateEdit1)
        Me.Name = "FUpdateStock"
        Me.Text = "Update Stock"
        Me.Controls.SetChildIndex(Me.DateEdit1, 0)
        Me.Controls.SetChildIndex(Me.LookUpEdit1, 0)
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.par_loc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LookUpEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LookUpEdit1 As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents par_loc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtUpdate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gc_loc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_loc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BtSOShipment As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtTransfer As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem

End Class
