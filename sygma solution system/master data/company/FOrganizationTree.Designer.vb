<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FOrganizationTree
    Inherits master_new.MasterInfOne

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
        Me.OrgsddetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.colcode_name = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.colorg_name_child = New DevExpress.XtraTreeList.Columns.TreeListColumn
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_detail.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OrgsddetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        '
        'scc_detail
        '
        Me.scc_detail.Panel1.Controls.Add(Me.TreeList1)
        Me.scc_detail.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1
        Me.scc_detail.Size = New System.Drawing.Size(837, 429)
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.colcode_name, Me.colorg_name_child})
        Me.TreeList1.DataSource = Me.OrgsddetBindingSource
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "orgsd_org_id"
        Me.TreeList1.Location = New System.Drawing.Point(5, 4)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.ParentFieldName = "orgsd_parent_org"
        Me.TreeList1.Size = New System.Drawing.Size(827, 420)
        Me.TreeList1.TabIndex = 0
        '
        'colcode_name
        '
        Me.colcode_name.Caption = "Parent"
        Me.colcode_name.FieldName = "code_name"
        Me.colcode_name.Name = "colcode_name"
        Me.colcode_name.OptionsColumn.ReadOnly = True
        Me.colcode_name.Visible = True
        Me.colcode_name.VisibleIndex = 0
        Me.colcode_name.Width = 402
        '
        'colorg_name_child
        '
        Me.colorg_name_child.Caption = "Child"
        Me.colorg_name_child.FieldName = "org_name_child"
        Me.colorg_name_child.Name = "colorg_name_child"
        Me.colorg_name_child.OptionsColumn.ReadOnly = True
        Me.colorg_name_child.Visible = True
        Me.colorg_name_child.VisibleIndex = 1
        Me.colorg_name_child.Width = 404
        '
        'FOrganizationTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(841, 433)
        Me.Name = "FOrganizationTree"
        Me.Text = "Organization Tree"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.scc_detail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_detail.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OrgsddetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OrgsddetBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents colcode_name As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents colorg_name_child As DevExpress.XtraTreeList.Columns.TreeListColumn

End Class
