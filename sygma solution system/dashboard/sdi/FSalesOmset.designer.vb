<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FSalesOmset
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
        Me.omz_date = New DevExpress.XtraEditors.DateEdit
        Me.omz_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.omz_target = New DevExpress.XtraEditors.TextEdit
        Me.omz_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_dom_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.Target = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lci_master.SuspendLayout()
        CType(Me.omz_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.omz_date.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.omz_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.omz_target.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.omz_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Target, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(543, 352)
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
        Me.lci_master.Controls.Add(Me.omz_date)
        Me.lci_master.Controls.Add(Me.omz_en_id)
        Me.lci_master.Controls.Add(Me.omz_target)
        Me.lci_master.Controls.Add(Me.omz_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 352)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'omz_date
        '
        Me.omz_date.EditValue = Nothing
        Me.omz_date.Location = New System.Drawing.Point(48, 60)
        Me.omz_date.Name = "omz_date"
        Me.omz_date.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.omz_date.Properties.DisplayFormat.FormatString = "y"
        Me.omz_date.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.omz_date.Properties.Mask.EditMask = "y"
        Me.omz_date.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.omz_date.Size = New System.Drawing.Size(483, 20)
        Me.omz_date.StyleController = Me.lci_master
        Me.omz_date.TabIndex = 3
        '
        'omz_en_id
        '
        Me.omz_en_id.Location = New System.Drawing.Point(48, 12)
        Me.omz_en_id.Name = "omz_en_id"
        Me.omz_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.omz_en_id.Size = New System.Drawing.Size(483, 20)
        Me.omz_en_id.StyleController = Me.lci_master
        Me.omz_en_id.TabIndex = 1
        '
        'omz_target
        '
        Me.omz_target.Location = New System.Drawing.Point(48, 84)
        Me.omz_target.Name = "omz_target"
        Me.omz_target.Properties.Mask.BeepOnError = True
        Me.omz_target.Properties.Mask.EditMask = "n2"
        Me.omz_target.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.omz_target.Size = New System.Drawing.Size(483, 20)
        Me.omz_target.StyleController = Me.lci_master
        Me.omz_target.TabIndex = 4
        '
        'omz_code
        '
        Me.omz_code.Location = New System.Drawing.Point(48, 36)
        Me.omz_code.Name = "omz_code"
        Me.omz_code.Size = New System.Drawing.Size(483, 20)
        Me.omz_code.StyleController = Me.lci_master
        Me.omz_code.TabIndex = 2
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_dom_code, Me.Target, Me.LayoutControlItem10, Me.EmptySpaceItem1, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 352)
        Me.LayoutControlGroup1.Text = "LayoutControlGroup1"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_dom_code
        '
        Me.lci_dom_code.Control = Me.omz_code
        Me.lci_dom_code.CustomizationFormText = "Domain Code"
        Me.lci_dom_code.Location = New System.Drawing.Point(0, 24)
        Me.lci_dom_code.Name = "lci_dom_code"
        Me.lci_dom_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_code.Text = "Code"
        Me.lci_dom_code.TextSize = New System.Drawing.Size(32, 13)
        '
        'Target
        '
        Me.Target.Control = Me.omz_target
        Me.Target.CustomizationFormText = "Domain Name"
        Me.Target.Location = New System.Drawing.Point(0, 72)
        Me.Target.Name = "Target"
        Me.Target.Size = New System.Drawing.Size(523, 24)
        Me.Target.Text = "Target"
        Me.Target.TextSize = New System.Drawing.Size(32, 13)
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.omz_en_id
        Me.LayoutControlItem10.CustomizationFormText = "Entity ID"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem10.Text = "Entity"
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(32, 13)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 96)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 236)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.omz_date
        Me.LayoutControlItem3.CustomizationFormText = "Month"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Month"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(32, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.omz_target
        Me.LayoutControlItem2.CustomizationFormText = "Domain Name"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 31)
        Me.LayoutControlItem2.Name = "lci_dom_name"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(537, 31)
        Me.LayoutControlItem2.Text = "Account Name"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(95, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(256, 24)
        Me.LayoutControlItem1.Text = "Start Date"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(50, 13)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'FSalesOmset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FSalesOmset"
        Me.Text = "Sales Omset"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lci_master.ResumeLayout(False)
        CType(Me.omz_date.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.omz_date.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.omz_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.omz_target.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.omz_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Target, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents omz_target As DevExpress.XtraEditors.TextEdit
    Friend WithEvents omz_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_dom_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Target As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents omz_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents omz_date As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem

End Class
