<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuBrowse
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TreeList1 = New DevExpress.XtraTreeList.TreeList
        Me.menudesc = New DevExpress.XtraTreeList.Columns.TreeListColumn
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl
        Me.btFindNext = New DevExpress.XtraEditors.SimpleButton
        Me.btSearch = New DevExpress.XtraEditors.SimpleButton
        Me.txtSearchMenu = New DevExpress.XtraEditors.TextEdit
        Me.ceExpandCollaps = New DevExpress.XtraEditors.CheckEdit
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl
        Me.BtCancel = New DevExpress.XtraEditors.SimpleButton
        Me.BtOK = New DevExpress.XtraEditors.SimpleButton
        Me.menuseq = New DevExpress.XtraTreeList.Columns.TreeListColumn
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceExpandCollaps.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeList1
        '
        Me.TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {Me.menudesc, Me.menuseq})
        Me.TreeList1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TreeList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeList1.KeyFieldName = "menuid"
        Me.TreeList1.Location = New System.Drawing.Point(0, 26)
        Me.TreeList1.Name = "TreeList1"
        Me.TreeList1.OptionsBehavior.Editable = False
        Me.TreeList1.OptionsView.EnableAppearanceEvenRow = True
        Me.TreeList1.OptionsView.ShowColumns = False
        Me.TreeList1.ParentFieldName = "menuid_parent"
        Me.TreeList1.Size = New System.Drawing.Size(284, 433)
        Me.TreeList1.TabIndex = 29
        '
        'menudesc
        '
        Me.menudesc.Caption = "Menu"
        Me.menudesc.FieldName = "menudesc"
        Me.menudesc.Name = "menudesc"
        Me.menudesc.Visible = True
        Me.menudesc.VisibleIndex = 0
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btFindNext)
        Me.PanelControl1.Controls.Add(Me.btSearch)
        Me.PanelControl1.Controls.Add(Me.txtSearchMenu)
        Me.PanelControl1.Controls.Add(Me.ceExpandCollaps)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(284, 26)
        Me.PanelControl1.TabIndex = 30
        '
        'btFindNext
        '
        Me.btFindNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btFindNext.Location = New System.Drawing.Point(236, 3)
        Me.btFindNext.Name = "btFindNext"
        Me.btFindNext.Size = New System.Drawing.Size(31, 20)
        Me.btFindNext.TabIndex = 3
        Me.btFindNext.Text = "Next"
        '
        'btSearch
        '
        Me.btSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btSearch.Location = New System.Drawing.Point(201, 3)
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(31, 20)
        Me.btSearch.TabIndex = 2
        Me.btSearch.Text = "Find"
        '
        'txtSearchMenu
        '
        Me.txtSearchMenu.Location = New System.Drawing.Point(86, 3)
        Me.txtSearchMenu.Name = "txtSearchMenu"
        Me.txtSearchMenu.Size = New System.Drawing.Size(111, 20)
        Me.txtSearchMenu.TabIndex = 1
        '
        'ceExpandCollaps
        '
        Me.ceExpandCollaps.EditValue = True
        Me.ceExpandCollaps.Location = New System.Drawing.Point(6, 3)
        Me.ceExpandCollaps.Name = "ceExpandCollaps"
        Me.ceExpandCollaps.Properties.Caption = "Expand All"
        Me.ceExpandCollaps.Size = New System.Drawing.Size(76, 19)
        Me.ceExpandCollaps.TabIndex = 0
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.BtCancel)
        Me.PanelControl2.Controls.Add(Me.BtOK)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl2.Location = New System.Drawing.Point(0, 459)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(284, 36)
        Me.PanelControl2.TabIndex = 31
        '
        'BtCancel
        '
        Me.BtCancel.Location = New System.Drawing.Point(149, 7)
        Me.BtCancel.Name = "BtCancel"
        Me.BtCancel.Size = New System.Drawing.Size(76, 22)
        Me.BtCancel.TabIndex = 0
        Me.BtCancel.Text = "Cancel"
        '
        'BtOK
        '
        Me.BtOK.Location = New System.Drawing.Point(59, 7)
        Me.BtOK.Name = "BtOK"
        Me.BtOK.Size = New System.Drawing.Size(76, 22)
        Me.BtOK.TabIndex = 0
        Me.BtOK.Text = "OK"
        '
        'menuseq
        '
        Me.menuseq.Caption = "menuseq"
        Me.menuseq.FieldName = "menuseq"
        Me.menuseq.Name = "menuseq"
        '
        'frmMenuBrowse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 495)
        Me.Controls.Add(Me.TreeList1)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.PanelControl2)
        Me.Name = "frmMenuBrowse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu"
        CType(Me.TreeList1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.txtSearchMenu.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceExpandCollaps.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeList1 As DevExpress.XtraTreeList.TreeList
    Friend WithEvents menudesc As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btFindNext As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtSearchMenu As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ceExpandCollaps As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BtCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents menuseq As DevExpress.XtraTreeList.Columns.TreeListColumn
End Class
