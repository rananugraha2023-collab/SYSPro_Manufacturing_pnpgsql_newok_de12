<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FInventoryHistory
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
        Me.pr_entity = New DevExpress.XtraEditors.LookUpEdit
        Me.Label2 = New System.Windows.Forms.Label
        Me.xtc_master = New DevExpress.XtraTab.XtraTabControl
        Me.xtp_warehouse = New DevExpress.XtraTab.XtraTabPage
        Me.gc_loc = New DevExpress.XtraGrid.GridControl
        Me.gv_loc = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.xtp_serial = New DevExpress.XtraTab.XtraTabPage
        Me.gc_serial = New DevExpress.XtraGrid.GridControl
        Me.gv_serial = New DevExpress.XtraGrid.Views.Grid.GridView
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.CBLoc = New DevExpress.XtraEditors.CheckEdit
        Me.date_start = New DevExpress.XtraEditors.DateEdit
        Me.date_end = New DevExpress.XtraEditors.DateEdit
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pt_id = New DevExpress.XtraEditors.ButtonEdit
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.par_loc = New DevExpress.XtraEditors.LookUpEdit
        Me.cbChild = New DevExpress.XtraEditors.CheckEdit
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scc_master.SuspendLayout()
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtc_master.SuspendLayout()
        Me.xtp_warehouse.SuspendLayout()
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.xtp_serial.SuspendLayout()
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CBLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_start.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.date_end.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pt_id.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.par_loc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbChild.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'scc_master
        '
        Me.scc_master.Panel1.Controls.Add(Me.pt_id)
        Me.scc_master.Panel1.Controls.Add(Me.date_end)
        Me.scc_master.Panel1.Controls.Add(Me.date_start)
        Me.scc_master.Panel1.Controls.Add(Me.cbChild)
        Me.scc_master.Panel1.Controls.Add(Me.CBLoc)
        Me.scc_master.Panel1.Controls.Add(Me.par_loc)
        Me.scc_master.Panel1.Controls.Add(Me.pr_entity)
        Me.scc_master.Panel1.Controls.Add(Me.Label3)
        Me.scc_master.Panel1.Controls.Add(Me.Label4)
        Me.scc_master.Panel1.Controls.Add(Me.Label5)
        Me.scc_master.Panel1.Controls.Add(Me.Label1)
        Me.scc_master.Panel1.Controls.Add(Me.Label2)
        Me.scc_master.Panel2.Controls.Add(Me.xtc_master)
        Me.scc_master.Size = New System.Drawing.Size(1152, 605)
        Me.scc_master.SplitterPosition = 60
        '
        'pr_entity
        '
        Me.pr_entity.Location = New System.Drawing.Point(562, 5)
        Me.pr_entity.Name = "pr_entity"
        Me.pr_entity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.pr_entity.Size = New System.Drawing.Size(193, 20)
        Me.pr_entity.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(500, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Entity"
        '
        'xtc_master
        '
        Me.xtc_master.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xtc_master.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom
        Me.xtc_master.Location = New System.Drawing.Point(0, 0)
        Me.xtc_master.Name = "xtc_master"
        Me.xtc_master.PaintStyleName = "PropertyView"
        Me.xtc_master.SelectedTabPage = Me.xtp_warehouse
        Me.xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.xtc_master.Size = New System.Drawing.Size(1152, 539)
        Me.xtc_master.TabIndex = 2
        Me.xtc_master.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.xtp_warehouse, Me.xtp_serial})
        '
        'xtp_warehouse
        '
        Me.xtp_warehouse.Controls.Add(Me.gc_loc)
        Me.xtp_warehouse.Name = "xtp_warehouse"
        Me.xtp_warehouse.Size = New System.Drawing.Size(1150, 538)
        Me.xtp_warehouse.Text = "By Warehouse"
        '
        'gc_loc
        '
        Me.gc_loc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_loc.Location = New System.Drawing.Point(0, 0)
        Me.gc_loc.MainView = Me.gv_loc
        Me.gc_loc.Name = "gc_loc"
        Me.gc_loc.Size = New System.Drawing.Size(1150, 538)
        Me.gc_loc.TabIndex = 0
        Me.gc_loc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_loc})
        '
        'gv_loc
        '
        Me.gv_loc.GridControl = Me.gc_loc
        Me.gv_loc.Name = "gv_loc"
        Me.gv_loc.OptionsSelection.MultiSelect = True
        '
        'xtp_serial
        '
        Me.xtp_serial.Controls.Add(Me.gc_serial)
        Me.xtp_serial.Name = "xtp_serial"
        Me.xtp_serial.Size = New System.Drawing.Size(1150, 538)
        Me.xtp_serial.Text = "Warehouse With Serial"
        '
        'gc_serial
        '
        Me.gc_serial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gc_serial.Location = New System.Drawing.Point(0, 0)
        Me.gc_serial.MainView = Me.gv_serial
        Me.gc_serial.Name = "gc_serial"
        Me.gc_serial.Size = New System.Drawing.Size(1150, 538)
        Me.gc_serial.TabIndex = 1
        Me.gc_serial.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gv_serial})
        '
        'gv_serial
        '
        Me.gv_serial.GridControl = Me.gc_serial
        Me.gv_serial.Name = "gv_serial"
        '
        'DockManager1
        '
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'CBLoc
        '
        Me.CBLoc.Location = New System.Drawing.Point(774, 31)
        Me.CBLoc.Name = "CBLoc"
        Me.CBLoc.Properties.Caption = "With Location"
        Me.CBLoc.Size = New System.Drawing.Size(111, 19)
        Me.CBLoc.TabIndex = 5
        '
        'date_start
        '
        Me.date_start.EditValue = Nothing
        Me.date_start.Location = New System.Drawing.Point(98, 31)
        Me.date_start.Name = "date_start"
        Me.date_start.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.date_start.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.date_start.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.date_start.Size = New System.Drawing.Size(127, 20)
        Me.date_start.TabIndex = 1
        '
        'date_end
        '
        Me.date_end.EditValue = Nothing
        Me.date_end.Location = New System.Drawing.Point(302, 32)
        Me.date_end.Name = "date_end"
        Me.date_end.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.date_end.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.date_end.Properties.VistaTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.date_end.Size = New System.Drawing.Size(127, 20)
        Me.date_end.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date Start"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(235, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Date End"
        '
        'pt_id
        '
        Me.pt_id.Location = New System.Drawing.Point(98, 5)
        Me.pt_id.Name = "pt_id"
        Me.pt_id.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
        Me.pt_id.Size = New System.Drawing.Size(331, 20)
        Me.pt_id.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Part Number"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(500, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Location"
        '
        'par_loc
        '
        Me.par_loc.Location = New System.Drawing.Point(562, 32)
        Me.par_loc.Name = "par_loc"
        Me.par_loc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.par_loc.Size = New System.Drawing.Size(193, 20)
        Me.par_loc.TabIndex = 4
        '
        'cbChild
        '
        Me.cbChild.Location = New System.Drawing.Point(774, 4)
        Me.cbChild.Name = "cbChild"
        Me.cbChild.Properties.Caption = "With All Child"
        Me.cbChild.Size = New System.Drawing.Size(111, 19)
        Me.cbChild.TabIndex = 5
        '
        'FInventoryHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1152, 605)
        Me.Name = "FInventoryHistory"
        Me.Text = "Inventory History"
        CType(Me.scc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scc_master.ResumeLayout(False)
        CType(Me.xtcd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pr_entity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.xtc_master, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtc_master.ResumeLayout(False)
        Me.xtp_warehouse.ResumeLayout(False)
        CType(Me.gc_loc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_loc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.xtp_serial.ResumeLayout(False)
        CType(Me.gc_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv_serial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CBLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_start.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_start.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_end.Properties.VistaTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.date_end.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pt_id.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.par_loc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbChild.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pr_entity As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents xtc_master As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents xtp_warehouse As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_loc As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_loc As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xtp_serial As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents gc_serial As DevExpress.XtraGrid.GridControl
    Friend WithEvents gv_serial As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents CBLoc As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents date_end As DevExpress.XtraEditors.DateEdit
    Friend WithEvents date_start As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pt_id As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents par_loc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbChild As DevExpress.XtraEditors.CheckEdit

End Class
