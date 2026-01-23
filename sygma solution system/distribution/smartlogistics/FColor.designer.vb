<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FColor
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
        Me.color_active = New DevExpress.XtraEditors.CheckEdit
        Me.color_name = New DevExpress.XtraEditors.TextEdit
        Me.color_code = New DevExpress.XtraEditors.TextEdit
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup
        Me.lci_dom_code = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_dom_name = New DevExpress.XtraLayout.LayoutControlItem
        Me.lci_dom_active = New DevExpress.XtraLayout.LayoutControlItem
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem
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
        CType(Me.color_active.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.color_name.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.color_code.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_name, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lci_dom_active, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lci_master.Controls.Add(Me.color_active)
        Me.lci_master.Controls.Add(Me.color_name)
        Me.lci_master.Controls.Add(Me.color_code)
        Me.lci_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lci_master.Location = New System.Drawing.Point(0, 0)
        Me.lci_master.Name = "lci_master"
        Me.lci_master.Root = Me.LayoutControlGroup1
        Me.lci_master.Size = New System.Drawing.Size(543, 367)
        Me.lci_master.StyleController = Me.StyleController1
        Me.lci_master.TabIndex = 0
        Me.lci_master.Text = "LayoutControl1"
        '
        'color_active
        '
        Me.color_active.Location = New System.Drawing.Point(12, 60)
        Me.color_active.Name = "color_active"
        Me.color_active.Properties.Caption = "Is Active"
        Me.color_active.Size = New System.Drawing.Size(519, 19)
        Me.color_active.StyleController = Me.lci_master
        Me.color_active.TabIndex = 6
        '
        'color_name
        '
        Me.color_name.Location = New System.Drawing.Point(81, 36)
        Me.color_name.Name = "color_name"
        Me.color_name.Size = New System.Drawing.Size(450, 20)
        Me.color_name.StyleController = Me.lci_master
        Me.color_name.TabIndex = 5
        '
        'color_code
        '
        Me.color_code.Location = New System.Drawing.Point(81, 12)
        Me.color_code.Name = "color_code"
        Me.color_code.Size = New System.Drawing.Size(450, 20)
        Me.color_code.StyleController = Me.lci_master
        Me.color_code.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lci_dom_code, Me.lci_dom_name, Me.lci_dom_active, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(543, 367)
        Me.LayoutControlGroup1.Text = "Root"
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lci_dom_code
        '
        Me.lci_dom_code.Control = Me.color_code
        Me.lci_dom_code.CustomizationFormText = "Domain Code"
        Me.lci_dom_code.Location = New System.Drawing.Point(0, 0)
        Me.lci_dom_code.Name = "lci_dom_code"
        Me.lci_dom_code.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_code.Text = "Domain Code"
        Me.lci_dom_code.TextSize = New System.Drawing.Size(65, 13)
        '
        'lci_dom_name
        '
        Me.lci_dom_name.Control = Me.color_name
        Me.lci_dom_name.CustomizationFormText = "Domain Name"
        Me.lci_dom_name.Location = New System.Drawing.Point(0, 24)
        Me.lci_dom_name.Name = "lci_dom_name"
        Me.lci_dom_name.Size = New System.Drawing.Size(523, 24)
        Me.lci_dom_name.Text = "Domain Name"
        Me.lci_dom_name.TextSize = New System.Drawing.Size(65, 13)
        '
        'lci_dom_active
        '
        Me.lci_dom_active.Control = Me.color_active
        Me.lci_dom_active.CustomizationFormText = "lci_dom_active"
        Me.lci_dom_active.Location = New System.Drawing.Point(0, 48)
        Me.lci_dom_active.Name = "lci_dom_active"
        Me.lci_dom_active.Size = New System.Drawing.Size(523, 23)
        Me.lci_dom_active.Text = "lci_dom_active"
        Me.lci_dom_active.TextSize = New System.Drawing.Size(0, 0)
        Me.lci_dom_active.TextToControlDistance = 0
        Me.lci_dom_active.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 71)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(523, 276)
        Me.EmptySpaceItem1.Text = "EmptySpaceItem1"
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'FColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(555, 433)
        Me.Name = "FColor"
        Me.Text = "Color"
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
        CType(Me.color_active.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.color_name.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.color_code.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_code, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_name, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lci_dom_active, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lci_master As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents color_active As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents color_name As DevExpress.XtraEditors.TextEdit
    Friend WithEvents color_code As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lci_dom_code As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_dom_name As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lci_dom_active As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem

End Class
