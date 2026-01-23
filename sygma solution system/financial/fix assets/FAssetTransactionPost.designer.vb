<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FAssetTransactionPost
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
        Me.StyleController1 = New DevExpress.XtraEditors.StyleController(Me.components)
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.date_of_depr = New DevExpress.XtraEditors.DateEdit
        Me.btn_update = New DevExpress.XtraEditors.SimpleButton
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.gc_master = New DevExpress.XtraGrid.GridControl
        Me.gv_master = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.ce_select = New DevExpress.XtraEditors.CheckEdit
        Me.xtp_data.SuspendLayout()
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_edit.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_of_depr.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_of_depr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ce_select.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtp_data
        '
        Me.xtp_data.Controls.Add(Me.gc_master)
        Me.xtp_data.Size = New System.Drawing.Size(670, 374)
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.ce_select)
        Me.scc_master.Panel1.Controls.Add(Me.LabelControl1)
        Me.scc_master.Panel1.Controls.Add(Me.date_of_depr)
        Me.scc_master.Panel1.Controls.Add(Me.btn_update)
        Me.scc_master.Size = New System.Drawing.Size(672, 457)
        Me.scc_master.SplitterPosition = 56
        '
        'xtc_master
        '
        Me.xtc_master.Size = New System.Drawing.Size(672, 395)
        '
        'xtp_edit
        '
        Me.xtp_edit.PageVisible = False
        Me.xtp_edit.Size = New System.Drawing.Size(553, 345)
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(543, 285)
        '
        'StyleController1
        '
        Me.StyleController1.AppearanceFocused.BackColor = System.Drawing.Color.SkyBlue
        Me.StyleController1.AppearanceFocused.Options.UseBackColor = True
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'date_of_depr
        '
        Me.date_of_depr.EditValue = Nothing
        Me.date_of_depr.Location = New System.Drawing.Point(132, 10)
        Me.date_of_depr.Name = "date_of_depr"
        Me.date_of_depr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.date_of_depr.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.date_of_depr.Size = New System.Drawing.Size(110, 20)
        Me.date_of_depr.TabIndex = 7
        '
        'btn_update
        '
        Me.btn_update.Location = New System.Drawing.Point(487, 8)
        Me.btn_update.Name = "btn_update"
        Me.btn_update.Size = New System.Drawing.Size(171, 22)
        Me.btn_update.TabIndex = 6
        Me.btn_update.Text = "<< Update Depresiasi >>"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(26, 13)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(100, 13)
        Me.LabelControl1.TabIndex = 8
        Me.LabelControl1.Text = "Date Of Depresiasi  :"
        '
        'gc_master
        '
        Me.gc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_master.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.gc_master.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.gc_master.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.gc_master.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.gc_master.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.gc_master.Location = New System.Drawing.Point(5, 5)
        Me.gc_master.MainView = Me.gv_master
        Me.gc_master.Name = "gc_master"
        Me.gc_master.Size = New System.Drawing.Size(660, 364)
        Me.gc_master.TabIndex = 6
        Me.gc_master.UseEmbeddedNavigator = True
        Me.gc_master.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_master})
        '
        'gv_master
        '
        Me.gv_master.GridControl = Me.gc_master
        Me.gv_master.Name = "gv_master"
        Me.gv_master.OptionsBehavior.AutoExpandAllGroups = True
        Me.gv_master.OptionsView.ColumnAutoWidth = False
        Me.gv_master.OptionsView.ShowAutoFilterRow = True
        Me.gv_master.OptionsView.ShowFooter = True
        Me.gv_master.OptionsView.ShowGroupedColumns = True
        '
        'ce_select
        '
        Me.ce_select.Location = New System.Drawing.Point(20, 33)
        Me.ce_select.Name = "ce_select"
        Me.ce_select.Properties.Caption = "Select All"
        Me.ce_select.Size = New System.Drawing.Size(75, 19)
        Me.ce_select.TabIndex = 9
        '
        'FAssetTransactionPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(672, 457)
        Me.Name = "FAssetTransactionPost"
        Me.Text = "Asset Transaction Post"
        Me.xtp_data.ResumeLayout(False)
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_edit.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StyleController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_of_depr.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_of_depr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gc_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_master, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ce_select.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents StyleController1 As DevExpress.XtraEditors.StyleController
    Friend WithEvents date_of_depr As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btn_update As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents gc_master As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_master As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ce_select As DevExpress.XtraEditors.CheckEdit

End Class
