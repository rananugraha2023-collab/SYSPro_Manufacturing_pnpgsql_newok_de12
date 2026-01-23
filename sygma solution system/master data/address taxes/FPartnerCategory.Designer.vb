<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPartnerCategory
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
        Me.ptnrc_term_of_payment = New DevExpress.XtraEditors.TextEdit
        Me.ptnrc_credit_limit = New DevExpress.XtraEditors.TextEdit
        Me.ptnrc_name = New DevExpress.XtraEditors.TextEdit
        Me.ptnrc_active = New DevExpress.XtraEditors.CheckEdit
        Me.ptnrc_desc = New DevExpress.XtraEditors.TextEdit
        Me.ptnrc_code = New DevExpress.XtraEditors.TextEdit
        Me.ptnrc_en_id = New DevExpress.XtraEditors.LookUpEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem
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
        CType(Me.ptnrc_term_of_payment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_credit_limit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_desc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ptnrc_en_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lci_master.Controls.Add(Me.ptnrc_term_of_payment)
        Me.lci_master.Controls.Add(Me.ptnrc_credit_limit)
        Me.lci_master.Controls.Add(Me.ptnrc_name)
        Me.lci_master.Controls.Add(Me.ptnrc_active)
        Me.lci_master.Controls.Add(Me.ptnrc_desc)
        Me.lci_master.Controls.Add(Me.ptnrc_code)
        Me.lci_master.Controls.Add(Me.ptnrc_en_id)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 367)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 1
        Me.lci_master.Text = "LayoutControl1"
        '
        'ptnrc_term_of_payment
        '
        Me.ptnrc_term_of_payment.Location = New System.Drawing.Point(135, 132)
        Me.ptnrc_term_of_payment.Name = "ptnrc_term_of_payment"
        Me.ptnrc_term_of_payment.Properties.Mask.EditMask = "n"
        Me.ptnrc_term_of_payment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ptnrc_term_of_payment.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.ptnrc_term_of_payment.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_term_of_payment.StyleController = Me.lci_master
        Me.ptnrc_term_of_payment.TabIndex = 10
        '
        'ptnrc_credit_limit
        '
        Me.ptnrc_credit_limit.Location = New System.Drawing.Point(135, 108)
        Me.ptnrc_credit_limit.Name = "ptnrc_credit_limit"
        Me.ptnrc_credit_limit.Properties.Mask.EditMask = "n"
        Me.ptnrc_credit_limit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.ptnrc_credit_limit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.ptnrc_credit_limit.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_credit_limit.StyleController = Me.lci_master
        Me.ptnrc_credit_limit.TabIndex = 9
        '
        'ptnrc_name
        '
        Me.ptnrc_name.Location = New System.Drawing.Point(135, 60)
        Me.ptnrc_name.Name = "ptnrc_name"
        Me.ptnrc_name.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_name.StyleController = Me.lci_master
        Me.ptnrc_name.TabIndex = 8
        '
        'ptnrc_active
        '
        Me.ptnrc_active.Location = New System.Drawing.Point(12, 156)
        Me.ptnrc_active.Name = "ptnrc_active"
        Me.ptnrc_active.Properties.Caption = "Is Active"
        Me.ptnrc_active.Size = New System.Drawing.Size(519, 19)
        Me.ptnrc_active.StyleController = Me.lci_master
        Me.ptnrc_active.TabIndex = 7
        '
        'ptnrc_desc
        '
        Me.ptnrc_desc.Location = New System.Drawing.Point(135, 84)
        Me.ptnrc_desc.Name = "ptnrc_desc"
        Me.ptnrc_desc.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_desc.StyleController = Me.lci_master
        Me.ptnrc_desc.TabIndex = 6
        '
        'ptnrc_code
        '
        Me.ptnrc_code.Location = New System.Drawing.Point(135, 36)
        Me.ptnrc_code.Name = "ptnrc_code"
        Me.ptnrc_code.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_code.StyleController = Me.lci_master
        Me.ptnrc_code.TabIndex = 5
        '
        'ptnrc_en_id
        '
        Me.ptnrc_en_id.Location = New System.Drawing.Point(135, 12)
        Me.ptnrc_en_id.Name = "ptnrc_en_id"
        Me.ptnrc_en_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ptnrc_en_id.Size = New System.Drawing.Size(396, 20)
        Me.ptnrc_en_id.StyleController = Me.lci_master
        Me.ptnrc_en_id.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "Root"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.EmptySpaceItem1, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 367)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.ptnrc_en_id
        Me.LayoutControlItem1.CustomizationFormText = "Entity"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem1.Text = "Entity"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(119, 13)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.ptnrc_code
        Me.LayoutControlItem2.CustomizationFormText = "Code"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem2.Text = "Code"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(119, 13)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.ptnrc_desc
        Me.LayoutControlItem3.CustomizationFormText = "Description"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem3.Text = "Description"
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(119, 13)
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.ptnrc_active
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 144)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(523, 23)
        Me.LayoutControlItem4.Text = "LayoutControlItem4"
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextToControlDistance = 0
        Me.LayoutControlItem4.TextVisible = False
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
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.ptnrc_name
        Me.LayoutControlItem5.CustomizationFormText = "Name"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem5.Text = "Name"
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(119, 13)
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.ptnrc_credit_limit
        Me.LayoutControlItem6.CustomizationFormText = "Credit Limit"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem6.Text = "Credit Limit"
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(119, 13)
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.ptnrc_term_of_payment
        Me.LayoutControlItem7.CustomizationFormText = "Term Of Payment (Days)"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(523, 24)
        Me.LayoutControlItem7.Text = "Term Of Payment (Days)"
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(119, 13)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FPartnerCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FPartnerCategory"
        Me.Text = "Partner Category"
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
        CType(Me.ptnrc_term_of_payment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_credit_limit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_desc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ptnrc_en_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents ptnrc_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ptnrc_desc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrc_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrc_en_id As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents ptnrc_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents ptnrc_term_of_payment As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ptnrc_credit_limit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem

End Class
