Imports npgsql
Imports master_new.ModFunction
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraPivotGrid.Data


Public Class FRosView
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_glt As DataSet
    Dim ds_group_glt As DataSet
    Dim ds_rosetta As DataSet

    Private Sub FRosView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        'help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())


        If le_periode.Properties.Columns.VisibleCount = 0 Then
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_start_date", "Start Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
            le_periode.Properties.Columns.Add(New DevExpress.XtraEditors.Controls.LookUpColumnInfo("gcal_end_date", "End Date", 20, DevExpress.Utils.FormatType.DateTime, "d", True, DevExpress.Utils.HorzAlignment.Center))
        End If

        le_periode.Properties.DropDownRows = 12

        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0

    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        ds_rosetta = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  rstbal_oid, " _
                    & "  rstbal_dom_id, " _
                    & "  rstbal_en_id, " _
                    & "  rstbal_add_by, " _
                    & "  rstbal_add_date, " _
                    & "  rstbal_upd_by, " _
                    & "  rstbal_upd_date, " _
                    & "  rstbal_rstrule_oid, " _
                    & "  rstbal_gcal_oid, " _
                    & "  coalesce(rstbal_openbal_amount,0) + coalesce(rstbal_amount,0) as rstbal_amount, " _
                    & "  rstbal_dt, " _
                    & "  en_desc, " _
                    & "  gcal_year, " _
                    & "  gcal_periode, " _
                    & "  gcal_start_date, " _
                    & "  gcal_end_date, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  account_mstr.code_name as account_name, " _
                    & "  tranline_mstr.code_name as line_name, " _
                    & "  cashflow_mstr.code_name as cashflow_name " _
                    & " FROM  " _
                    & "  public.rstbal_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
                    & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                    & "  INNER JOIN public.rstrule_mstr ON (public.rstrule_mstr.rstrule_oid = public.rstbal_mstr.rstbal_rstrule_oid)" _
                    & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                    & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                    & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                    & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                    & " where rstbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                    & " and rstbal_en_id =1 " _
                    & " ORDER BY group_mstr.code_seq,account_mstr.code_seq,tranline_mstr.code_seq,cashflow_mstr.code_seq "

                    .InitializeCommand()
                    .FillDataSet(ds_rosetta, "rosseta")
                    pgc_master.DataSource = ds_rosetta.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
  

    Private Sub ShowDrilldown(ByVal ds As DevExpress.XtraPivotGrid.PivotDrillDownDataSource)
        Dim form As XtraForm = New XtraForm()
        form.Text = "Drill Down Form"
        form.StartPosition = FormStartPosition.CenterParent
        Dim grid As DataGridView = New DataGridView()
        grid.Parent = form
        AddHandler grid.DataBindingComplete, AddressOf grid_DataBindingComplete
        grid.Dock = DockStyle.Fill
        grid.DataSource = ds
        form.Width = 520
        form.Height = 300

        Dim s As String = ""
        For Each r As DataGridViewRow In grid.Rows
            'For Each c As DataGridViewCell In r.Cells
            s = r.Cells(0).Value
            'Next
            ' s = s & Environment.NewLine
        Next

        'MsgBox(s)
        form.Dispose()

        Dim sSQL As String
        sSQL = "SELECT  " _
                & "  a.rsthistory_oid, " _
                & "  a.rsthistory_rstbal_oid, " _
                & "  a.rsthistory_glt_code, " _
                & "  a.rsthistory_amount, " _
                & "  a.rsthistory_ac_id1, " _
                & "  b.ac_code AS ac_code1, " _
                & "  b.ac_name AS ac_name1, " _
                & "  a.rsthistory_ac_sign1, " _
                & "  a.rsthistory_ac_id2, " _
                & "  c.ac_code AS ac_code2, " _
                & "  c.ac_name AS ac_name2, " _
                & "  a.rsthistory_ac_sign2 " _
                & "FROM " _
                & "  public.ac_mstr b " _
                & "  INNER JOIN public.rsthistory_mstr a ON (b.ac_id = a.rsthistory_ac_id1) " _
                & "  INNER JOIN public.ac_mstr c ON (a.rsthistory_ac_id2 = c.ac_id) " _
                & " Where rsthistory_rstbal_oid=" & SetSetring(s) & " order by rsthistory_glt_code"

        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(sSQL)


        Dim frm As New FShow
        frm.fobject = Me
        frm.par_dt = dt
        frm.ShowDialog()


        'Dim grid As DataGridView = New DataGridView()
        'AddHandler grid.DataBindingComplete, AddressOf grid_DataBindingComplete
        'grid.DataSource = ds

        'MsgBox(grid.Rows(0).Cells(0).Value)
        ' grid.Columns("rstbal_oid").
    End Sub

    Private Sub grid_DataBindingComplete(ByVal sender As Object, ByVal e As DataGridViewBindingCompleteEventArgs)
        Dim grid As DataGridView = CType(sender, DataGridView)
        For i As Integer = 0 To grid.ColumnCount - 1
            Dim column As DataGridViewColumn = grid.Columns(i)
            column.HeaderText = GetHeaderText(column.Name)
        Next i
    End Sub
    Private Function GetHeaderText(ByVal drilldownColumnName As String) As String
        For i As Integer = 0 To pgc_master.Fields.Count - 1
            Dim field As DevExpress.XtraPivotGrid.PivotGridField = pgc_master.Fields(i)
            If field.OLAPDrillDownColumnName = drilldownColumnName Then
                Return field.Caption
            End If
        Next i
        Return drilldownColumnName
    End Function

    Private Sub pgc_master_CellDoubleClick(ByVal sender As Object, ByVal e As DevExpress.XtraPivotGrid.PivotCellEventArgs) Handles pgc_master.CellDoubleClick
        ShowDrilldown(e.CreateDrillDownDataSource())
        'MsgBox(ds_rosetta.Tables(0).Rows(BindingContext(ds_rosetta.Tables(0)).Position).Item("rstbal_oid"))
    End Sub

    Public ReadOnly Property ViewOptionsControl() As DevExpress.XtraPivotGrid.PivotGridControl
        Get
            Return pgc_master
        End Get
    End Property
    Public ReadOnly Property ExportControl() As DevExpress.XtraPivotGrid.PivotGridControl
        Get
            Return pgc_master
        End Get
    End Property
End Class
