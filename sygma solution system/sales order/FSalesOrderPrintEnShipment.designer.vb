<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSalesOrderPrintEnShipment
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
        Me.TxtSONumber = New DevExpress.XtraEditors.MemoEdit
        Me.so_tglcutting_akhir = New DevExpress.XtraEditors.DateEdit
        Me.so_tglcutting_awal = New DevExpress.XtraEditors.DateEdit
        Me.ce_indent = New DevExpress.XtraEditors.CheckEdit
        Me.ce_prn_dt = New DevExpress.XtraEditors.CheckEdit
        Me.ce_invoice = New DevExpress.XtraEditors.CheckEdit
        Me.le_entity_shipment = New DevExpress.XtraEditors.LookUpEdit
        Me.be_to = New DevExpress.XtraEditors.ButtonEdit
        Me.be_first = New DevExpress.XtraEditors.ButtonEdit
        Me.ce_excel = New DevExpress.XtraEditors.CheckEdit
        Me.le_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.EntityShipment = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.FirstDate = New DevExpress.XtraLayout.LayoutControlItem
        Me.LastDate = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.TxtSONumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.so_tglcutting_akhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.so_tglcutting_akhir.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.so_tglcutting_awal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.so_tglcutting_awal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_indent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_prn_dt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_invoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity_shipment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_excel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EntityShipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FirstDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LastDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'lci_master
        '
        Me.lci_master.Controls.Add(Me.TxtSONumber)
        Me.lci_master.Controls.Add(Me.so_tglcutting_akhir)
        Me.lci_master.Controls.Add(Me.so_tglcutting_awal)
        Me.lci_master.Controls.Add(Me.ce_indent)
        Me.lci_master.Controls.Add(Me.ce_prn_dt)
        Me.lci_master.Controls.Add(Me.ce_invoice)
        Me.lci_master.Controls.Add(Me.le_entity_shipment)
        Me.lci_master.Controls.Add(Me.be_to)
        Me.lci_master.Controls.Add(Me.be_first)
        Me.lci_master.Controls.Add(Me.ce_excel)
        Me.lci_master.Controls.Add(Me.le_entity)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EntityShipment})
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup4
        Me.lci_master.Size = New System.Drawing.Size(555, 433)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 5
        Me.lci_master.Text = "LayoutControl1"
        '
        'TxtSONumber
        '
        Me.TxtSONumber.Location = New System.Drawing.Point(124, 162)
        Me.TxtSONumber.Name = "TxtSONumber"
        Me.TxtSONumber.Size = New System.Drawing.Size(407, 247)
        Me.TxtSONumber.StyleController = Me.lci_master
        Me.TxtSONumber.TabIndex = 17
        '
        'so_tglcutting_akhir
        '
        Me.so_tglcutting_akhir.EditValue = Nothing
        Me.so_tglcutting_akhir.Location = New System.Drawing.Point(379, 91)
        Me.so_tglcutting_akhir.Name = "so_tglcutting_akhir"
        Me.so_tglcutting_akhir.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.so_tglcutting_akhir.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.so_tglcutting_akhir.Size = New System.Drawing.Size(152, 20)
        Me.so_tglcutting_akhir.StyleController = Me.lci_master
        Me.so_tglcutting_akhir.TabIndex = 16
        '
        'so_tglcutting_awal
        '
        Me.so_tglcutting_awal.EditValue = Nothing
        Me.so_tglcutting_awal.Location = New System.Drawing.Point(124, 91)
        Me.so_tglcutting_awal.Name = "so_tglcutting_awal"
        Me.so_tglcutting_awal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.so_tglcutting_awal.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.so_tglcutting_awal.Size = New System.Drawing.Size(151, 20)
        Me.so_tglcutting_awal.StyleController = Me.lci_master
        Me.so_tglcutting_awal.TabIndex = 15
        '
        'ce_indent
        '
        Me.ce_indent.Location = New System.Drawing.Point(152, 68)
        Me.ce_indent.Name = "ce_indent"
        Me.ce_indent.Properties.Caption = "Indent"
        Me.ce_indent.Size = New System.Drawing.Size(124, 19)
        Me.ce_indent.StyleController = Me.lci_master
        Me.ce_indent.TabIndex = 13
        '
        'ce_prn_dt
        '
        Me.ce_prn_dt.Location = New System.Drawing.Point(280, 68)
        Me.ce_prn_dt.Name = "ce_prn_dt"
        Me.ce_prn_dt.Properties.Caption = "By Date"
        Me.ce_prn_dt.Size = New System.Drawing.Size(251, 19)
        Me.ce_prn_dt.StyleController = Me.lci_master
        Me.ce_prn_dt.TabIndex = 12
        '
        'ce_invoice
        '
        Me.ce_invoice.Location = New System.Drawing.Point(24, 68)
        Me.ce_invoice.Name = "ce_invoice"
        Me.ce_invoice.Properties.Caption = "Invoice"
        Me.ce_invoice.Size = New System.Drawing.Size(124, 19)
        Me.ce_invoice.StyleController = Me.lci_master
        Me.ce_invoice.TabIndex = 11
        '
        'le_entity_shipment
        '
        Me.le_entity_shipment.Location = New System.Drawing.Point(380, 44)
        Me.le_entity_shipment.Name = "le_entity_shipment"
        Me.le_entity_shipment.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity_shipment.Size = New System.Drawing.Size(151, 20)
        Me.le_entity_shipment.StyleController = Me.lci_master
        Me.le_entity_shipment.TabIndex = 10
        '
        'be_to
        '
        Me.be_to.Location = New System.Drawing.Point(380, 115)
        Me.be_to.Name = "be_to"
        Me.be_to.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_to.Size = New System.Drawing.Size(151, 20)
        Me.be_to.StyleController = Me.lci_master
        Me.be_to.TabIndex = 9
        '
        'be_first
        '
        Me.be_first.Location = New System.Drawing.Point(124, 115)
        Me.be_first.Name = "be_first"
        Me.be_first.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.be_first.Size = New System.Drawing.Size(152, 20)
        Me.be_first.StyleController = Me.lci_master
        Me.be_first.TabIndex = 8
        '
        'ce_excel
        '
        Me.ce_excel.Location = New System.Drawing.Point(24, 139)
        Me.ce_excel.Name = "ce_excel"
        Me.ce_excel.Properties.Caption = "By Filter SO Number"
        Me.ce_excel.Size = New System.Drawing.Size(507, 19)
        Me.ce_excel.StyleController = Me.lci_master
        Me.ce_excel.TabIndex = 14
        '
        'le_entity
        '
        Me.le_entity.Location = New System.Drawing.Point(124, 44)
        Me.le_entity.Name = "le_entity"
        Me.le_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.le_entity.Size = New System.Drawing.Size(152, 20)
        Me.le_entity.StyleController = Me.lci_master
        Me.le_entity.TabIndex = 7
        '
        'EntityShipment
        '
        Me.EntityShipment.Control = Me.le_entity_shipment
        Me.EntityShipment.CustomizationFormText = "Entity Shipment"
        Me.EntityShipment.Location = New System.Drawing.Point(256, 0)
        Me.EntityShipment.Name = "EntityShipment"
        Me.EntityShipment.Size = New System.Drawing.Size(255, 24)
        Me.EntityShipment.Text = "Entity Shipment"
        Me.EntityShipment.TextSize = New System.Drawing.Size(96, 13)
        Me.EntityShipment.TextToControlDistance = 5
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.CustomizationFormText = "Root"
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6})
        Me.LayoutControlGroup4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup4.Name = "Root"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(555, 433)
        Me.LayoutControlGroup4.Text = "Root"
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.CustomizationFormText = "Position"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem10, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.FirstDate, Me.LastDate, Me.LayoutControlItem7, Me.LayoutControlItem6, Me.EmptySpaceItem1})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(535, 413)
        Me.LayoutControlGroup6.Text = "Data"
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.le_entity
        Me.LayoutControlItem10.CustomizationFormText = "Entity"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem10.Text = "Entity"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.be_first
        Me.LayoutControlItem1.CustomizationFormText = "Purchase Order"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 71)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem1.Text = "Sales Order Number"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.be_to
        Me.LayoutControlItem2.CustomizationFormText = "To"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(256, 71)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(255, 24)
        Me.LayoutControlItem2.Text = "To"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.ce_invoice
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(128, 23)
        Me.LayoutControlItem3.Text = "LayoutControlItem3"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextToControlDistance = 0
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.ce_prn_dt
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(256, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(255, 23)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.ce_indent
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(128, 24)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(128, 23)
        Me.LayoutControlItem5.Text = "LayoutControlItem5"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'FirstDate
        '
        Me.FirstDate.Control = Me.so_tglcutting_awal
        Me.FirstDate.CustomizationFormText = "First Date"
        Me.FirstDate.Location = New System.Drawing.Point(0, 47)
        Me.FirstDate.Name = "FirstDate"
        Me.FirstDate.Size = New System.Drawing.Size(255, 24)
        Me.FirstDate.Text = "First Date"
        Me.FirstDate.TextSize = New System.Drawing.Size(96, 13)
        '
        'LastDate
        '
        Me.LastDate.Control = Me.so_tglcutting_akhir
        Me.LastDate.CustomizationFormText = "Last Date"
        Me.LastDate.Location = New System.Drawing.Point(255, 47)
        Me.LastDate.Name = "LastDate"
        Me.LastDate.Size = New System.Drawing.Size(256, 24)
        Me.LastDate.Text = "Last Date"
        Me.LastDate.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.TxtSONumber
        Me.LayoutControlItem7.CustomizationFormText = "SO Number (Paste)"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 118)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(511, 251)
        Me.LayoutControlItem7.Text = "SO Number (Paste)"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(96, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ce_excel
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 95)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(511, 23)
        Me.LayoutControlItem6.Text = "LayoutControlItem6"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(256, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(255, 24)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FSalesOrderPrintEnShipment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Controls.Add(Me.lci_master)
        Me.Name = "FSalesOrderPrintEnShipment"
        Me.Text = "Sales Order Print"
        Me.Controls.SetChildIndex(Me.scc_master, 0)
        Me.Controls.SetChildIndex(Me.lci_master, 0)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me._rpt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dt_control, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.TxtSONumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.so_tglcutting_akhir.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.so_tglcutting_akhir.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.so_tglcutting_awal.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.so_tglcutting_awal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_indent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_prn_dt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_invoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity_shipment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.be_first.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_excel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.le_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EntityShipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FirstDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LastDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub


    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Public WithEvents be_to As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents be_first As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents le_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents le_entity_shipment As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents EntityShipment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ce_excel As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ce_indent As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ce_prn_dt As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ce_invoice As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents so_tglcutting_akhir As DevExpress.XtraEditors.DateEdit
    Friend WithEvents so_tglcutting_awal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FirstDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LastDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TxtSONumber As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
End Class

