<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRecieveQRCodePrint
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
        Me.invqr_is_req = New DevExpress.XtraEditors.CheckEdit()
        Me.invqr_req_id = New DevExpress.XtraEditors.ButtonEdit()
        Me.invqr_seq_to = New DevExpress.XtraEditors.TextEdit()
        Me.invqr_en_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.invqr_seq_from = New DevExpress.XtraEditors.TextEdit()
        Me.invqr_si_id = New DevExpress.XtraEditors.LookUpEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ColliyNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.Entity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LuarJawa = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ReceiveNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.invqr_is_req.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_req_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_seq_to.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_seq_from.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.invqr_si_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColliyNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LuarJawa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReceiveNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.xtc_master.SelectedTabPage = Me.xtp_data
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
        Me.lci_master.Controls.Add(Me.invqr_is_req)
        Me.lci_master.Controls.Add(Me.invqr_req_id)
        Me.lci_master.Controls.Add(Me.invqr_seq_to)
        Me.lci_master.Controls.Add(Me.invqr_en_id)
        Me.lci_master.Controls.Add(Me.invqr_seq_from)
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
        'invqr_is_req
        '
        Me.invqr_is_req.Location = New System.Drawing.Point(273, 36)
        Me.invqr_is_req.Name = "invqr_is_req"
        Me.invqr_is_req.Properties.Caption = "Requsition"
        Me.invqr_is_req.Size = New System.Drawing.Size(258, 19)
        Me.invqr_is_req.StyleController = Me.lci_master
        Me.invqr_is_req.TabIndex = 13
        '
        'invqr_req_id
        '
        Me.invqr_req_id.Location = New System.Drawing.Point(93, 36)
        Me.invqr_req_id.Name = "invqr_req_id"
        Me.invqr_req_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.invqr_req_id.Size = New System.Drawing.Size(176, 20)
        Me.invqr_req_id.StyleController = Me.lci_master
        Me.invqr_req_id.TabIndex = 12
        '
        'invqr_seq_to
        '
        Me.invqr_seq_to.Location = New System.Drawing.Point(354, 60)
        Me.invqr_seq_to.Name = "invqr_seq_to"
        Me.invqr_seq_to.Properties.Appearance.Options.UseTextOptions = True
        Me.invqr_seq_to.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invqr_seq_to.Properties.DisplayFormat.FormatString = "n"
        Me.invqr_seq_to.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_seq_to.Properties.EditFormat.FormatString = "n"
        Me.invqr_seq_to.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_seq_to.Properties.Mask.EditMask = "n"
        Me.invqr_seq_to.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invqr_seq_to.Size = New System.Drawing.Size(177, 20)
        Me.invqr_seq_to.StyleController = Me.lci_master
        Me.invqr_seq_to.TabIndex = 10
        '
        'invqr_en_id
        '
        Me.invqr_en_id.Location = New System.Drawing.Point(354, 12)
        Me.invqr_en_id.Name = "invqr_en_id"
        Me.invqr_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invqr_en_id.Size = New System.Drawing.Size(177, 20)
        Me.invqr_en_id.StyleController = Me.lci_master
        Me.invqr_en_id.TabIndex = 9
        '
        'invqr_seq_from
        '
        Me.invqr_seq_from.Location = New System.Drawing.Point(93, 60)
        Me.invqr_seq_from.Name = "invqr_seq_from"
        Me.invqr_seq_from.Properties.Appearance.Options.UseTextOptions = True
        Me.invqr_seq_from.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.invqr_seq_from.Properties.DisplayFormat.FormatString = "n"
        Me.invqr_seq_from.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_seq_from.Properties.EditFormat.FormatString = "n"
        Me.invqr_seq_from.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.invqr_seq_from.Properties.Mask.EditMask = "n"
        Me.invqr_seq_from.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.invqr_seq_from.Size = New System.Drawing.Size(176, 20)
        Me.invqr_seq_from.StyleController = Me.lci_master
        Me.invqr_seq_from.TabIndex = 5
        '
        'invqr_si_id
        '
        Me.invqr_si_id.Location = New System.Drawing.Point(93, 12)
        Me.invqr_si_id.Name = "invqr_si_id"
        Me.invqr_si_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.invqr_si_id.Size = New System.Drawing.Size(176, 20)
        Me.invqr_si_id.StyleController = Me.lci_master
        Me.invqr_si_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.ColliyNumber, Me.EmptySpaceItem1, Me.Entity, Me.LuarJawa, Me.ReceiveNumber, Me.LayoutControlItem2})
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
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(78, 13)
        '
        'ColliyNumber
        '
        Me.ColliyNumber.Control = Me.invqr_seq_from
        Me.ColliyNumber.CustomizationFormText = "Colliy Number"
        Me.ColliyNumber.Location = New System.Drawing.Point(0, 48)
        Me.ColliyNumber.Name = "ColliyNumber"
        Me.ColliyNumber.Size = New System.Drawing.Size(261, 24)
        Me.ColliyNumber.Text = "Colliy Number"
        Me.ColliyNumber.TextSize = New System.Drawing.Size(78, 13)
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
        Me.Entity.TextSize = New System.Drawing.Size(78, 13)
        '
        'LuarJawa
        '
        Me.LuarJawa.Control = Me.invqr_seq_to
        Me.LuarJawa.CustomizationFormText = "To"
        Me.LuarJawa.Location = New System.Drawing.Point(261, 48)
        Me.LuarJawa.Name = "LuarJawa"
        Me.LuarJawa.Size = New System.Drawing.Size(262, 24)
        Me.LuarJawa.Text = "To"
        Me.LuarJawa.TextSize = New System.Drawing.Size(78, 13)
        '
        'ReceiveNumber
        '
        Me.ReceiveNumber.Control = Me.invqr_req_id
        Me.ReceiveNumber.CustomizationFormText = "Receive Number"
        Me.ReceiveNumber.Location = New System.Drawing.Point(0, 24)
        Me.ReceiveNumber.Name = "ReceiveNumber"
        Me.ReceiveNumber.Size = New System.Drawing.Size(261, 24)
        Me.ReceiveNumber.Text = "Receive Number"
        Me.ReceiveNumber.TextSize = New System.Drawing.Size(78, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.invqr_is_req
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(261, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(262, 24)
        Me.LayoutControlItem2.Text = "LayoutControlItem2"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'FRecieveQRCodePrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FRecieveQRCodePrint"
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
        CType(Me.invqr_is_req.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_req_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_seq_to.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_seq_from.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.invqr_si_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColliyNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Entity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LuarJawa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReceiveNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents invqr_si_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ColliyNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents invqr_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Entity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LuarJawa As DevExpress.XtraLayout.LayoutControlItem
    Public WithEvents invqr_seq_from As DevExpress.XtraEditors.TextEdit
    Public WithEvents invqr_seq_to As DevExpress.XtraEditors.TextEdit
    Public WithEvents invqr_req_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ReceiveNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents invqr_is_req As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem

End Class
