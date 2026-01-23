<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintDialog
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintDialog))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl
        Me.gridControl1 = New DevExpress.XtraGrid.GridControl
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.gridColumnName = New DevExpress.XtraGrid.Columns.GridColumn
        Me.repositoryItemImageComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
        Me.bt_design = New DevExpress.XtraEditors.SimpleButton
        Me.bt_preview = New DevExpress.XtraEditors.SimpleButton
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.gridControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(25, 27)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(505, 211)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "List Setting"
        '
        'gridControl1
        '
        Me.gridControl1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.gridControl1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridControl1.Location = New System.Drawing.Point(2, 22)
        Me.gridControl1.MainView = Me.gridView1
        Me.gridControl1.Name = "gridControl1"
        Me.gridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repositoryItemImageComboBox1})
        Me.gridControl1.Size = New System.Drawing.Size(501, 187)
        Me.gridControl1.TabIndex = 0
        Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gridView1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 26)
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ExportToolStripMenuItem.Text = "Delete"
        '
        'gridView1
        '
        Me.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gridView1.Appearance.EvenRow.Options.UseBackColor = True
        Me.gridView1.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gridView1.Appearance.FooterPanel.Options.UseFont = True
        Me.gridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.gridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gridColumnName})
        Me.gridView1.GridControl = Me.gridControl1
        Me.gridView1.Name = "gridView1"
        Me.gridView1.OptionsBehavior.AllowIncrementalSearch = True
        Me.gridView1.OptionsBehavior.Editable = False
        Me.gridView1.OptionsMenu.EnableFooterMenu = False
        Me.gridView1.OptionsMenu.EnableGroupPanelMenu = False
        Me.gridView1.OptionsView.AutoCalcPreviewLineCount = True
        Me.gridView1.OptionsView.ShowFooter = True
        Me.gridView1.OptionsView.ShowGroupPanel = False
        Me.gridView1.OptionsView.ShowHorzLines = False
        Me.gridView1.OptionsView.ShowVertLines = False
        Me.gridView1.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.gridColumnName, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'gridColumnName
        '
        Me.gridColumnName.Caption = "File Setting"
        Me.gridColumnName.FieldName = "name"
        Me.gridColumnName.Name = "gridColumnName"
        Me.gridColumnName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.gridColumnName.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        Me.gridColumnName.SummaryItem.DisplayFormat = "Files And Folders ({0})"
        Me.gridColumnName.SummaryItem.FieldName = "Name"
        Me.gridColumnName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
        Me.gridColumnName.Visible = True
        Me.gridColumnName.VisibleIndex = 0
        Me.gridColumnName.Width = 348
        '
        'repositoryItemImageComboBox1
        '
        Me.repositoryItemImageComboBox1.AutoHeight = False
        Me.repositoryItemImageComboBox1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.repositoryItemImageComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repositoryItemImageComboBox1.CaseSensitiveSearch = True
        Me.repositoryItemImageComboBox1.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)})
        Me.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1"
        '
        'bt_design
        '
        Me.bt_design.Location = New System.Drawing.Point(172, 243)
        Me.bt_design.Name = "bt_design"
        Me.bt_design.Size = New System.Drawing.Size(100, 28)
        Me.bt_design.TabIndex = 1
        Me.bt_design.Text = "Design"
        '
        'bt_preview
        '
        Me.bt_preview.Location = New System.Drawing.Point(282, 242)
        Me.bt_preview.Name = "bt_preview"
        Me.bt_preview.Size = New System.Drawing.Size(100, 28)
        Me.bt_preview.TabIndex = 0
        Me.bt_preview.Text = "Preview"
        '
        'frmPrintDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 287)
        Me.Controls.Add(Me.bt_preview)
        Me.Controls.Add(Me.bt_design)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Dialog"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryItemImageComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Private WithEvents gridControl1 As DevExpress.XtraGrid.GridControl
    Private WithEvents gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents repositoryItemImageComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Private WithEvents gridColumnName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bt_design As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bt_preview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
