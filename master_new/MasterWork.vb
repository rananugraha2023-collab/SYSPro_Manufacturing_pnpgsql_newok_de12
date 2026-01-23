Imports npgsql
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export
Imports master_new.ModFunction
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls

Public Class MasterWork

    Public spc As DevExpress.XtraEditors.SplitContainerControl
    Public spcd As DevExpress.XtraEditors.SplitContainerControl
    Public spcd2 As DevExpress.XtraEditors.SplitContainerControl
    Public sgp As DevExpress.XtraEditors.SplitGroupPanel
    Public sgpd As DevExpress.XtraEditors.SplitGroupPanel
    Public sgpd2 As DevExpress.XtraEditors.SplitGroupPanel
    Public xtc As DevExpress.XtraTab.XtraTabControl
    Public xtd As DevExpress.XtraTab.XtraTabControl
    Public xtp As DevExpress.XtraTab.XtraTabPage
    Public gc As DevExpress.XtraGrid.GridControl
    Public gv As DevExpress.XtraGrid.Views.Grid.GridView
    Public xvg As DevExpress.XtraPivotGrid.PivotGridControl
    Public ow As Windows.Forms.Control
    Public spv_master, spv_detail As New DevExpress.XtraEditors.SplitPanelVisibility
    Public dp As New DevExpress.XtraBars.Docking.DockPanel
    Public cc As New DevExpress.XtraBars.Docking.ControlContainer
    Public xtcd As New DevExpress.XtraTab.XtraTabControl
    Public xtpd As New DevExpress.XtraTab.XtraTabPage

    Public ds As New DataSet
    Public column As DevExpress.XtraGrid.Columns.GridColumn
    Public row As Integer
    Public row_detail As Integer
    Public get_param, get_path As String
    Public pnl As System.Windows.Forms.Panel
    Public fmm As MasterMDI
    Public type_form As Boolean
    Dim visible_index As Integer = 0
    Dim gv_cek As String = ""
    Public dt_control As New DataTable

    Public Sub NumericValuesOnly(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case AscW(ControlChars.Cr) 'Enter key
                e.Handled = True

            Case AscW(ControlChars.Back) 'Backspace

            Case 44, 45, 46, 48 To 57 ', Negative sign, Decimal and Numbers

            Case Else ' Everything else
                e.Handled = True
        End Select
    End Sub

    Public Overridable Sub form_first_load()

    End Sub

    Public Overridable Sub on_load()

    End Sub

    Public Overridable Sub ConditionsAdjustment()

    End Sub

    Public Overridable Sub load_data(ByVal arg As Boolean, ByVal gc As DevExpress.XtraGrid.GridControl)

    End Sub

    Public Overridable Sub load_data_detail(ByVal sql As String, ByVal gc As DevExpress.XtraGrid.GridControl, ByVal tn As String)

    End Sub

    Public Overridable Sub load_data_grid_detail()

    End Sub

    Public Overridable Sub load_cb()

    End Sub

    Public Overridable Sub format_grid()

    End Sub

    Public Overridable Sub format_embeddednavigator(ByVal gc As DevExpress.XtraGrid.GridControl)
        gc.EmbeddedNavigator.Buttons.Append.Visible = False
        gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        gc.EmbeddedNavigator.Buttons.Edit.Visible = False
        gc.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        gc.EmbeddedNavigator.Buttons.Remove.Visible = False
    End Sub

    Public Overridable Sub set_param_textbox()

    End Sub

    Public Overridable Sub help_load_data(ByVal par As Boolean)

    End Sub

    Public Overridable Sub load_data_many(ByVal par As Boolean)

    End Sub

    Public Overrides Sub find()
        help_load_data(True)
    End Sub

    Public Overridable Function get_sequel() As String
        get_sequel = ""
        Return get_sequel
    End Function

    Public Overridable Sub add_groupsummary()

    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_fn As String, ByVal par_visible As Boolean)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_fn
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = par_visible
        conf_col(gv.Name, par_fn, par_visible)
    End Sub

    Public Overridable Sub add_column_edit(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, _
                                           ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column_edit(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, _
                                           ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, _
                                           ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column_edit(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, _
                                           ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, _
                                           ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String, _
                                           ByVal par_summarytype As DevExpress.Data.SummaryItemType, ByVal par_displayformat As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        column.SummaryItem.SummaryType = par_summarytype
        column.SummaryItem.DisplayFormat = par_displayformat
        gv.GroupSummary.Add(par_summarytype, par_fn, column, par_displayformat)
        conf_col(gv.Name, par_fn, True)
    End Sub


    Public Sub add_column_fix(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, _
                                   ByVal par_caption As String, ByVal par_fn As String, _
                                   ByVal par_align As DevExpress.Utils.HorzAlignment)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        'column.OptionsColumn.AllowEdit = True
        'column.OptionsColumn.ReadOnly = True
        column.OptionsColumn.AllowEdit = True 'false
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
        column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        'conf_col(gv.Name, par_fn, True)
    End Sub

    Public Sub add_column_fix(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, _
                                     ByVal par_caption As String, ByVal par_fn As String, _
                                     ByVal par_align As DevExpress.Utils.HorzAlignment, _
                                     ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.ReadOnly = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        column.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        'conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String, ByVal par_summarytype As DevExpress.Data.SummaryItemType, ByVal par_displayformat As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        column.SummaryItem.SummaryType = par_summarytype
        column.SummaryItem.DisplayFormat = par_displayformat
        gv.GroupSummary.Add(par_summarytype, par_fn, column, par_displayformat)
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column_copy(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.ReadOnly = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub add_column_copy(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.ReadOnly = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        conf_col(gv.Name, par_fn, True)
    End Sub
    Public Sub add_column_edit(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, _
   ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, _
   ByVal par_repo As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.ColumnEdit = par_repo
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
    End Sub


    'Public Function init_le_repo(ByVal par_tipe As String) As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    '    Dim sSQL As String
    '    Try

    '        Dim RepositoryItemLookUpEdit1 As New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit

    '        If par_tipe = "city" Then
    '            sSQL = "SELECT  " _
    '               & "  a.city_name " _
    '               & "FROM " _
    '               & "  city_mstr a " _
    '               & "   " _
    '               & "ORDER BY " _
    '               & "  a.city_name"

    '            Dim dt As New DataTable
    '            dt = master_new.PGSqlConn.GetTableData(sSQL)

    '            With RepositoryItemLookUpEdit1

    '                If .Columns.VisibleCount = 0 Then
    '                    .Columns.Add(New LookUpColumnInfo("city_name", "City", 20))

    '                End If

    '                .DataSource = dt
    '                .DisplayMember = dt.Columns("city_name").ToString
    '                .ValueMember = dt.Columns("city_name").ToString

    '                .BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup
    '                .BestFit()
    '                .DropDownRows = 12
    '                .PopupWidth = 300

    '            End With
    '        End If

    '        Return RepositoryItemLookUpEdit1
    '    Catch ex As Exception
    '        Pesan(Err)
    '        Return Nothing
    '    End Try

    'End Function

    Public Sub add_column_edit(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, _
  ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, _
  ByVal par_repo As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.ColumnEdit = par_repo
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align
    End Sub
    Public Overridable Sub add_column_copy(ByVal gv As DevExpress.XtraGrid.Views.Grid.GridView, ByVal par_caption As String, ByVal par_fn As String, ByVal par_align As DevExpress.Utils.HorzAlignment, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String, ByVal par_summarytype As DevExpress.Data.SummaryItemType, ByVal par_displayformat As String)
        column = New DevExpress.XtraGrid.Columns.GridColumn

        gv.Columns.Add(column)
        column.Caption = par_caption
        column.FieldName = par_fn
        column.Name = par_fn
        column.Visible = True
        column.OptionsColumn.AllowEdit = True
        column.OptionsColumn.ReadOnly = True
        column.OptionsColumn.AllowIncrementalSearch = True
        column.AppearanceCell.TextOptions.HAlignment = par_align

        column.DisplayFormat.FormatType = formatType
        column.DisplayFormat.FormatString = formatString
        column.SummaryItem.SummaryType = par_summarytype
        column.SummaryItem.DisplayFormat = par_displayformat
        gv.GroupSummary.Add(par_summarytype, par_fn, column, par_displayformat)
        conf_col(gv.Name, par_fn, True)
    End Sub

    Public Overridable Sub AllowIncrementalSearch()

    End Sub

    Public Overridable Sub bestfit_column()

    End Sub

    Public Overridable Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String
        Dim dlg As SaveFileDialog = New SaveFileDialog()
        Dim name As String = Application.ProductName
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " + title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = DialogResult.OK Then

            Return dlg.FileName
        End If
        Return ""
    End Function

    Public Overridable Sub OpenFile(ByVal fileName As String)
        If MessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                MessageBox.Show(Me, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Public Overridable Sub Export_Progress(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Export.ProgressEventArgs)
        If e.Phase = DevExpress.XtraGrid.Export.ExportPhase.Link Then
            progressForm.SetProgressValue(e.Position)
        End If
    End Sub

    Private progressForm As FProgress = Nothing
    Public Overridable Sub ExportTo(ByVal gv_exp As DevExpress.XtraGrid.Views.Grid.GridView, ByVal provider As IExportProvider)
        progressForm = New FProgress(Me)
        progressForm.Show()

        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor
        Dim link As BaseExportLink
        link = Nothing
        link = gv_exp.CreateExportLink(provider)
        CType(link, GridViewExportLink).ExpandAll = True
        AddHandler link.Progress, New DevExpress.XtraGrid.Export.ProgressEventHandler(AddressOf Export_Progress)

        link.ExportTo(True)
        provider.Dispose()
        RemoveHandler link.Progress, New DevExpress.XtraGrid.Export.ProgressEventHandler(AddressOf Export_Progress)
        Cursor.Current = currentCursor

        progressForm.Dispose()
        progressForm = Nothing
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        Dim obj As Object
        If fileName <> "" Then
            obj = get_gv()
            If TypeOf obj Is DevExpress.XtraGrid.Views.Grid.GridView Then
                ExportTo(obj, New ExportXlsProvider(fileName))
            ElseIf TypeOf obj Is DevExpress.XtraPivotGrid.PivotGridControl Then
                obj.ExportToXls(fileName)
            End If
            OpenFile(fileName)
        End If
    End Function

    Public Overrides Sub email_data()
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            ExportTo(get_gv(), New ExportXlsProvider(fileName))
            Dim frm As New FMail
            frm.sc_txtfile.Text = fileName
            frm.ShowDialog()
        End If
    End Sub

    'Public Overrides Sub preview()
    'get_param_and_path()
    'Dim frm As New FLaporan
    'frm.MdiParent = fmm
    'frm.pr_lblparam.Text = get_param
    'frm.preview_report_ds(get_path, ds.Tables(0))
    'frm.Show()
    'End Sub

    Public Overridable Sub get_param_and_path()

    End Sub

    Public Overridable Sub relation_detail()

    End Sub

    Public Overridable Sub set_component()

    End Sub

    Public Overridable Sub panel_visibility()

    End Sub

    Public Overrides ReadOnly Property ExportView() As DevExpress.XtraGrid.Views.Base.BaseView
        Get
            Return get_gv()
        End Get
    End Property

    Public Overridable Sub conf_col(ByVal gv_name As String, ByVal par_fn As String, ByVal par_visible As Boolean)
        'Try
        '    Using objsearch As New master_new.WDABasepgsql("", "")
        '        With objsearch
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select count(column_name) as jml from tconf_columns" + _
        '                                   " where form_name = '" + Me.Name.ToLower + "'" + _
        '                                   " and gv_name = '" + gv_name + "'" + _
        '                                   " and userid = " + ClsVar.sUserID.ToString + _
        '                                   " and column_name = '" + par_fn + "'"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                If .DataReader.Item("jml") = 1 Then
        '                    Exit Sub
        '                End If
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

        'Try
        '    Using objtable As New master_new.WDABasepgsql("", "")
        '        With objtable
        '            .Connection.Open()
        '            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
        '            Try
        '                If gv_cek <> gv_name Then
        '                    visible_index = 0
        '                    gv_cek = gv_name
        '                End If

        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran
        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "insert into tconf_columns values ('" + Me.Name.ToLower + "','" + _
        '                                       gv_name + "'," + ClsVar.sUserID.ToString + ",'" + _
        '                                       par_fn + "'," + par_visible.ToString + "," + _
        '                                       visible_index.ToString + "," + _
        '                                       par_visible.ToString + "," + _
        '                                       visible_index.ToString + ")"
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()
        '                visible_index += 1

        '                sqlTran.Commit()
        '            Catch ex As nPgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As nPgSqlException
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Overridable Sub load_Columns()
        Dim nama_file, path, status_file As String
        path = "c:\syspro\layout\"

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                For Each ctrl_spcd2 In spcd2.Controls
                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView

                                                                nama_file = Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
                                                                status_file = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
                                                                If status_file <> "" Then
                                                                    gv_detail.RestoreLayoutFromXml(path + nama_file)
                                                                End If
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView

                                                                                nama_file = Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
                                                                                status_file = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
                                                                                If status_file <> "" Then
                                                                                    gv_detail.RestoreLayoutFromXml(path + nama_file)
                                                                                End If
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView

                                                nama_file = Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"
                                                status_file = DevExpress.Utils.FilesHelper.FindingFileName(path, nama_file, False)
                                                If status_file <> "" Then
                                                    gv.RestoreLayoutFromXml(path + nama_file)
                                                End If
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overridable Sub cek_folder()
        Dim filesys, folder
        Dim path As String
        path = "c:\syspro"
        filesys = CreateObject("Scripting.FileSystemObject")
        If Not filesys.FolderExists(path) Then
            folder = filesys.CreateFolder(path)
        End If
        path = "c:\syspro\layout"
        filesys = CreateObject("Scripting.FileSystemObject")
        If Not filesys.FolderExists(path) Then
            folder = filesys.CreateFolder(path)
        End If
    End Sub

    Public Overridable Sub save_setting_columns()
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)

                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)

                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)

                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)

                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                For Each ctrl_spcd2 In spcd2.Controls
                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView
                                                                gv_detail.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                gv_detail.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + gv_detail.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                gv.SaveLayoutToXml("c:\syspro\layout\" + Me.Name + "_" + gv.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml")
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub reset()
        If MessageBox.Show("Yakin Untuk Mereset Setingan Ini..", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try
            Using objtable As New master_new.WDABasepgsql("", "")
                With objtable
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        gv = get_gv()

                        .Command.CommandText = "update tconf_columns " + _
                                                   " set status_visible = status_visible_awal, " + _
                                                   " visible_index = visible_index_awal " + _
                                                   " where form_name = '" + Me.Name.ToLower + "'" + _
                                                   " and gv_name = '" + gv.Name + "'" + _
                                                   " and userid = " + ClsVar.sUserID.ToString
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        load_Columns()
                        gv.BestFitColumns()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As nPgSqlException
            MessageBox.Show(ex.Message)
        End Try

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                For Each ctrl_spcd2 In spcd2.Controls
                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView
                                                                Try
                                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                                        With objtable
                                                                            .Connection.Open()
                                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                                            Try
                                                                                .Command = .Connection.CreateCommand
                                                                                .Command.Transaction = sqlTran
                                                                                .Command.CommandType = CommandType.Text

                                                                                .Command.CommandText = "update tconf_columns " + _
                                                                                                       " set status_visible = status_visible_awal, " + _
                                                                                                       " visible_index = visible_index_awal " + _
                                                                                                       " where form_name = '" + Me.Name.ToLower + "'" + _
                                                                                                       " and gv_name = '" + gv_detail.Name + "'" + _
                                                                                                       " and userid = " + ClsVar.sUserID.ToString
                                                                                .Command.ExecuteNonQuery()
                                                                                .Command.Parameters.Clear()

                                                                                sqlTran.Commit()
                                                                                load_Columns()
                                                                                gv_detail.BestFitColumns()
                                                                                Exit Sub
                                                                            Catch ex As nPgSqlException
                                                                                sqlTran.Rollback()
                                                                                MessageBox.Show(ex.Message)
                                                                            End Try
                                                                        End With
                                                                    End Using
                                                                Catch ex As nPgSqlException
                                                                    MessageBox.Show(ex.Message)
                                                                End Try
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                Try
                                                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                                                        With objtable
                                                                                            .Connection.Open()
                                                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                                                            Try
                                                                                                .Command = .Connection.CreateCommand
                                                                                                .Command.Transaction = sqlTran
                                                                                                .Command.CommandType = CommandType.Text

                                                                                                .Command.CommandText = "update tconf_columns " + _
                                                                                                                       " set status_visible = status_visible_awal, " + _
                                                                                                                       " visible_index = visible_index_awal " + _
                                                                                                                       " where form_name = '" + Me.Name.ToLower + "'" + _
                                                                                                                       " and gv_name = '" + gv_detail.Name + "'" + _
                                                                                                                       " and userid = " + ClsVar.sUserID.ToString
                                                                                                .Command.ExecuteNonQuery()
                                                                                                .Command.Parameters.Clear()

                                                                                                sqlTran.Commit()
                                                                                                load_Columns()
                                                                                                gv_detail.BestFitColumns()
                                                                                                Exit Sub
                                                                                            Catch ex As nPgSqlException
                                                                                                sqlTran.Rollback()
                                                                                                MessageBox.Show(ex.Message)
                                                                                            End Try
                                                                                        End With
                                                                                    End Using
                                                                                Catch ex As nPgSqlException
                                                                                    MessageBox.Show(ex.Message)
                                                                                End Try
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                Try
                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                        With objtable
                                                            .Connection.Open()
                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                            Try
                                                                .Command = .Connection.CreateCommand
                                                                .Command.Transaction = sqlTran
                                                                .Command.CommandType = CommandType.Text

                                                                .Command.CommandText = "update tconf_columns " + _
                                                                                       " set status_visible = status_visible_awal, " + _
                                                                                       " visible_index = visible_index_awal " + _
                                                                                       " where form_name = '" + Me.Name.ToLower + "'" + _
                                                                                       " and gv_name = '" + gv.Name + "'" + _
                                                                                       " and userid = " + ClsVar.sUserID.ToString
                                                                .Command.ExecuteNonQuery()
                                                                .Command.Parameters.Clear()

                                                                sqlTran.Commit()
                                                                load_Columns()
                                                                gv.BestFitColumns()
                                                                Exit Sub
                                                            Catch ex As nPgSqlException
                                                                sqlTran.Rollback()
                                                                MessageBox.Show(ex.Message)
                                                            End Try
                                                        End With
                                                    End Using
                                                Catch ex As nPgSqlException
                                                    MessageBox.Show(ex.Message)
                                                End Try
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub favorite()
        If MessageBox.Show("Add This Form To Favorite ...?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "insert into tconffavorite values ('" + Me.Name.ToLower + "'," + _
                           ClsVar.sUserID.ToString + ",'" + Me.Text + "')"
                    .InitializeCommand()
                    .ExecuteStoredProcedure()
                    fmm.load_myfavorite()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show("This Form Already Save at Favorite Menu..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Overrides Sub save_as()
        If MessageBox.Show("Save As This Form ...?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim form_alias As String = "", form_name As String = "", form_ori As String = ""
        Dim fixed As Boolean = False
        Dim fixed_alignment As String = ""
        form_alias = InputBox("Your New Form Name..", "Customize Form", "")

        If Trim(form_alias) = "" Then
            Exit Sub
        End If

        If type_form = True Then
            form_ori = Me.Name.ToLower
        Else
            Try
                Using objsearch As New master_new.WDABasepgsql("", "")
                    With objsearch
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select form_name_asal from tconfmycustom" + _
                                               " where form_name_baru = '" + Me.Name.ToLower + "'"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            form_ori = .DataReader.Item("form_name_asal")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Try
            Using objsearch As New master_new.WDABasepgsql("", "")
                With objsearch
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select count(form_alias) as jml from tconfmycustom" + _
                                           " where form_name_asal = '" + form_ori + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        form_name = form_ori + "_" + (.DataReader("jml") + 1).ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer
        Try
            Using objtable As New master_new.WDABasepgsql("", "")
                With objtable
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        gv = get_gv()
                        For i = 0 To gv.Columns.Count - 1
                            If gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                                fixed = True
                                fixed_alignment = "left"
                            ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None Then
                                fixed = False
                                fixed_alignment = "none"
                            ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right Then
                                fixed = True
                                fixed_alignment = "right"
                            End If
                            .Command.CommandText = "insert into tconf_columns values ('" + form_name.ToLower + "','" + _
                                                   gv.Name + "'," + ClsVar.sUserID.ToString + ",'" + _
                                                   gv.Columns(i).Name + "'," + gv.Columns(i).Visible.ToString + "," + _
                                                   gv.Columns(i).VisibleIndex.ToString + "," + _
                                                   gv.Columns(i).Visible.ToString + "," + _
                                                   gv.Columns(i).VisibleIndex.ToString + "," + _
                                                   fixed.ToString + ",'" + fixed_alignment + "')"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandText = "insert into tconfmycustom values ('" + form_alias + "'," + _
                                               ClsVar.sUserID.ToString + ",'" + _
                                               form_ori + "','" + form_name + "')"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        fmm.load_mycustom()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As nPgSqlException
            MessageBox.Show(ex.Message)
        End Try

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                For Each ctrl_dp In dp.Controls
                    If TypeOf ctrl_dp Is DevExpress.XtraBars.Docking.ControlContainer Then
                        cc = CType(ctrl_dp, DevExpress.XtraBars.Docking.ControlContainer)
                        For Each ctrl_cc In cc.Controls
                            If TypeOf ctrl_cc Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_cc, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraEditors.SplitContainerControl Then
                                                spcd2 = CType(ctrl_xtp, DevExpress.XtraEditors.SplitContainerControl)
                                                For Each ctrl_spcd2 In spcd2.Controls
                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                gv_detail = gc_detail.MainView
                                                                Try
                                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                                        With objtable
                                                                            .Connection.Open()
                                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                                            Try
                                                                                .Command = .Connection.CreateCommand
                                                                                .Command.Transaction = sqlTran
                                                                                .Command.CommandType = CommandType.Text
                                                                                'gv = get_gv()
                                                                                For i = 0 To gv_detail.Columns.Count - 1
                                                                                    If gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                                                                                        fixed = True
                                                                                        fixed_alignment = "left"
                                                                                    ElseIf gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None Then
                                                                                        fixed = False
                                                                                        fixed_alignment = "none"
                                                                                    ElseIf gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right Then
                                                                                        fixed = True
                                                                                        fixed_alignment = "right"
                                                                                    End If
                                                                                    .Command.CommandText = "insert into tconf_columns values ('" + form_name.ToLower + "','" + _
                                                                                                           gv_detail.Name + "'," + ClsVar.sUserID.ToString + ",'" + _
                                                                                                           gv_detail.Columns(i).Name + "'," + gv_detail.Columns(i).Visible.ToString + "," + _
                                                                                                           gv_detail.Columns(i).VisibleIndex.ToString + "," + _
                                                                                                           gv_detail.Columns(i).Visible.ToString + "," + _
                                                                                                           gv_detail.Columns(i).VisibleIndex.ToString + "," + _
                                                                                                           fixed.ToString + ",'" + fixed_alignment + "')"
                                                                                    .Command.ExecuteNonQuery()
                                                                                    .Command.Parameters.Clear()
                                                                                Next

                                                                                '.Command.CommandText = "insert into tconfmycustom values ('" + form_alias + "'," + _
                                                                                '                       ClsVar.sUserID.ToString + ",'" + _
                                                                                '                       form_ori + "','" + form_name + "')"
                                                                                '.Command.ExecuteNonQuery()
                                                                                '.Command.Parameters.Clear()

                                                                                sqlTran.Commit()
                                                                                fmm.load_mycustom()
                                                                                Exit Sub
                                                                            Catch ex As nPgSqlException
                                                                                sqlTran.Rollback()
                                                                                MessageBox.Show(ex.Message)
                                                                            End Try
                                                                        End With
                                                                    End Using
                                                                Catch ex As nPgSqlException
                                                                    MessageBox.Show(ex.Message)
                                                                End Try
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            ElseIf TypeOf ctrl_xtp Is DevExpress.XtraTab.XtraTabControl Then
                                                xtcd = CType(ctrl_xtp, DevExpress.XtraTab.XtraTabControl)
                                                For Each ctrl_xtcd In xtcd.Controls
                                                    If TypeOf ctrl_xtcd Is DevExpress.XtraTab.XtraTabPage Then
                                                        xtpd = CType(ctrl_xtcd, DevExpress.XtraTab.XtraTabPage)
                                                        For Each ctrl_xtpd In xtpd.Controls
                                                            If TypeOf ctrl_xtpd Is DevExpress.XtraEditors.SplitContainerControl Then
                                                                spcd2 = CType(ctrl_xtpd, DevExpress.XtraEditors.SplitContainerControl)
                                                                For Each ctrl_spcd2 In spcd2.Controls
                                                                    If TypeOf ctrl_spcd2 Is DevExpress.XtraEditors.SplitGroupPanel Then
                                                                        sgpd2 = CType(ctrl_spcd2, DevExpress.XtraEditors.SplitGroupPanel)
                                                                        For Each ctrl_sgpd2 In sgpd2.Controls
                                                                            If TypeOf ctrl_sgpd2 Is DevExpress.XtraGrid.GridControl Then
                                                                                Dim gc_detail As New DevExpress.XtraGrid.GridControl
                                                                                gc_detail = CType(ctrl_sgpd2, DevExpress.XtraGrid.GridControl)
                                                                                Dim gv_detail As New DevExpress.XtraGrid.Views.Grid.GridView
                                                                                gv_detail = gc_detail.MainView
                                                                                Try
                                                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                                                        With objtable
                                                                                            .Connection.Open()
                                                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                                                            Try
                                                                                                .Command = .Connection.CreateCommand
                                                                                                .Command.Transaction = sqlTran
                                                                                                .Command.CommandType = CommandType.Text
                                                                                                'gv = get_gv()
                                                                                                For i = 0 To gv_detail.Columns.Count - 1
                                                                                                    If gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                                                                                                        fixed = True
                                                                                                        fixed_alignment = "left"
                                                                                                    ElseIf gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None Then
                                                                                                        fixed = False
                                                                                                        fixed_alignment = "none"
                                                                                                    ElseIf gv_detail.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right Then
                                                                                                        fixed = True
                                                                                                        fixed_alignment = "right"
                                                                                                    End If
                                                                                                    .Command.CommandText = "insert into tconf_columns values ('" + form_name.ToLower + "','" + _
                                                                                                                           gv_detail.Name + "'," + ClsVar.sUserID.ToString + ",'" + _
                                                                                                                           gv_detail.Columns(i).Name + "'," + gv_detail.Columns(i).Visible.ToString + "," + _
                                                                                                                           gv_detail.Columns(i).VisibleIndex.ToString + "," + _
                                                                                                                           gv_detail.Columns(i).Visible.ToString + "," + _
                                                                                                                           gv_detail.Columns(i).VisibleIndex.ToString + "," + _
                                                                                                                           fixed.ToString + ",'" + fixed_alignment + "')"
                                                                                                    .Command.ExecuteNonQuery()
                                                                                                    .Command.Parameters.Clear()
                                                                                                Next

                                                                                                '.Command.CommandText = "insert into tconfmycustom values ('" + form_alias + "'," + _
                                                                                                '                       ClsVar.sUserID.ToString + ",'" + _
                                                                                                '                       form_ori + "','" + form_name + "')"
                                                                                                '.Command.ExecuteNonQuery()
                                                                                                '.Command.Parameters.Clear()

                                                                                                sqlTran.Commit()
                                                                                                fmm.load_mycustom()
                                                                                                Exit Sub
                                                                                            Catch ex As nPgSqlException
                                                                                                sqlTran.Rollback()
                                                                                                MessageBox.Show(ex.Message)
                                                                                            End Try
                                                                                        End With
                                                                                    End Using
                                                                                Catch ex As nPgSqlException
                                                                                    MessageBox.Show(ex.Message)
                                                                                End Try
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                For Each ctrl_spc In spc.Controls
                    If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                        sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                        For Each ctrl_sgp In sgp.Controls
                            If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                For Each ctrl_xtc In xtc.Controls
                                    If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                        For Each ctrl_xtp In xtp.Controls
                                            If TypeOf ctrl_xtp Is DevExpress.XtraGrid.GridControl Then
                                                gc = CType(ctrl_xtp, DevExpress.XtraGrid.GridControl)
                                                gv = gc.MainView
                                                Try
                                                    Using objtable As New master_new.WDABasepgsql("", "")
                                                        With objtable
                                                            .Connection.Open()
                                                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                                                            Try
                                                                .Command = .Connection.CreateCommand
                                                                .Command.Transaction = sqlTran
                                                                .Command.CommandType = CommandType.Text
                                                                For i = 0 To gv.Columns.Count - 1
                                                                    If gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                                                                        fixed = True
                                                                        fixed_alignment = "left"
                                                                    ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None Then
                                                                        fixed = False
                                                                        fixed_alignment = "none"
                                                                    ElseIf gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right Then
                                                                        fixed = True
                                                                        fixed_alignment = "right"
                                                                    End If
                                                                    .Command.CommandText = "insert into tconf_columns values ('" + form_name.ToLower + "','" + _
                                                                                           gv.Name + "'," + ClsVar.sUserID.ToString + ",'" + _
                                                                                           gv.Columns(i).Name + "'," + gv.Columns(i).Visible.ToString + "," + _
                                                                                           gv.Columns(i).VisibleIndex.ToString + "," + _
                                                                                           gv.Columns(i).Visible.ToString + "," + _
                                                                                           gv.Columns(i).VisibleIndex.ToString + "," + _
                                                                                           fixed.ToString + ",'" + fixed_alignment + "')"
                                                                    .Command.ExecuteNonQuery()
                                                                    .Command.Parameters.Clear()
                                                                Next

                                                                '.Command.CommandText = "insert into tconfmycustom values ('" + form_alias + "'," + _
                                                                '                       ClsVar.sUserID.ToString + ",'" + _
                                                                '                       form_ori + "','" + form_name + "')"
                                                                '.Command.ExecuteNonQuery()
                                                                '.Command.Parameters.Clear()

                                                                sqlTran.Commit()
                                                                fmm.load_mycustom()
                                                                Exit Sub
                                                            Catch ex As nPgSqlException
                                                                sqlTran.Rollback()
                                                                MessageBox.Show(ex.Message)
                                                            End Try
                                                        End With
                                                    End Using
                                                Catch ex As nPgSqlException
                                                    MessageBox.Show(ex.Message)
                                                End Try
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
    End Sub

    Public Overrides Sub freeze()
        Dim i As Integer
        Dim col As Integer
        Dim status As Boolean = False

        gv = get_gv()
        For i = 0 To gv.Columns.Count - 1
            If gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left Then
                status = True
                Exit For
            End If
        Next

        save_setting_columns()
        If status = False Then
            col = gv.Columns.IndexOf(gv.FocusedColumn)
            For i = 0 To col
                gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
            Next
            Try
                Using objtable As New master_new.WDABasepgsql("", "")
                    With objtable
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            .Command.CommandType = CommandType.Text
                            For i = 0 To col
                                .Command.CommandText = "update tconf_columns set fixed = true, fixed_alignment = 'left' " + _
                                                       " where form_name ~~* '" + Me.Name.ToLower + "'" + _
                                                       " and gv_name ~~* '" + gv.Name + "'" + _
                                                       " and userid = " + ClsVar.sUserID.ToString + _
                                                       " and column_name ~~* '" + gv.Columns(i).Name + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As nPgSqlException
                MessageBox.Show(ex.Message)
            End Try
        Else
            For i = 0 To gv.Columns.Count - 1
                gv.Columns(i).Fixed = DevExpress.XtraGrid.Columns.FixedStyle.None
            Next
            Try
                Using objtable As New master_new.WDABasepgsql("", "")
                    With objtable
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            .Command.CommandType = CommandType.Text
                            For i = 0 To col
                                .Command.CommandText = "update tconf_columns set fixed = false, fixed_alignment = 'none' " + _
                                                       " where form_name ~~* '" + Me.Name.ToLower + "'" + _
                                                       " and gv_name ~~* '" + gv.Name + "'" + _
                                                       " and userid = " + ClsVar.sUserID.ToString
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next

                            sqlTran.Commit()
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As nPgSqlException
                MessageBox.Show(ex.Message)
            End Try
        End If

        load_Columns()
    End Sub

    Private Sub MasterWork_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Try
        '    If e.KeyCode = Keys.Escape Then
        '        Me.Close()
        '    End If
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try
    End Sub

    Public Overrides Sub smart_approve()

        Try

            '    'MyBase.smart_approve()
            '    Dim ssql As String = ""
            '    ssql = "select '' as form, '' as control, '' as text"
            '    dt_control = master_new.PGSqlConn.GetTableData(ssql)

            '    dt_control.Rows.Clear()

            '    For Each ctrl In Me.Controls
            '        If TypeOf ctrl Is DevExpress.XtraEditors.LabelControl Then
            '            Dim _row As DataRow
            '            _row = dt_control.NewRow
            '            _row("control") = ctrl.name
            '            _row("text") = ctrl.text
            '            dt_control.Rows.Add(_row)
            '        End If
            '        If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
            '            spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
            '            For Each ctrl_spc In spc.Controls
            '                If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
            '                    sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
            '                    For Each ctrl_sgp In sgp.Controls
            '                        If TypeOf ctrl_sgp Is DevExpress.XtraEditors.LabelControl Then
            '                            Dim _row As DataRow
            '                            _row = dt_control.NewRow
            '                            _row("control") = ctrl_sgp.name
            '                            _row("text") = ctrl_sgp.text
            '                            dt_control.Rows.Add(_row)
            '                        End If
            '                        If TypeOf ctrl_sgp Is DevExpress.XtraLayout.LayoutControlItem Then
            '                            Dim _row As DataRow
            '                            _row = dt_control.NewRow
            '                            _row("control") = ctrl_sgp.name
            '                            _row("text") = ctrl_sgp.text
            '                            dt_control.Rows.Add(_row)
            '                        End If
            '                        If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
            '                            xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
            '                            For Each ctrl_xtc In xtc.Controls
            '                                If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
            '                                    If ctrl_xtc.name = "xtp_edit" Then
            '                                        xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
            '                                        For Each ctrl_xtp In xtp.Controls
            '                                            If TypeOf ctrl_xtp Is Panel Then
            '                                                pnl = CType(ctrl_xtp, Panel)
            '                                                For Each ctrl_pnl In pnl.Controls
            '                                                    If TypeOf ctrl_pnl Is DevExpress.XtraLayout.LayoutControl Then

            '                                                        For Each obj_x As Object In ctrl_pnl.items
            '                                                            If TypeOf obj_x Is DevExpress.XtraLayout.TabbedControlGroup Then
            '                                                                For i As Integer = 0 To obj_x.TabPages.GroupCount - 1
            '                                                                    For Each obj As Object In obj_x.TabPages(i).Items

            '                                                                        If TypeOf obj Is DevExpress.XtraLayout.LayoutControlGroup Then
            '                                                                            For Each obj_2 As Object In obj.items
            '                                                                                If TypeOf obj_2 Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                                    If obj_2.text.ToString.Contains("Empty") Then
            '                                                                                    ElseIf obj_2.text.ToString.Contains("Layout") Then
            '                                                                                    Else
            '                                                                                        Dim _row As DataRow
            '                                                                                        _row = dt_control.NewRow
            '                                                                                        _row("form") = Me.Name
            '                                                                                        _row("control") = obj_2.name
            '                                                                                        _row("text") = obj_2.text
            '                                                                                        dt_control.Rows.Add(_row)
            '                                                                                    End If

            '                                                                                End If
            '                                                                                If TypeOf obj_2 Is DevExpress.XtraLayout.TabbedControlGroup Then
            '                                                                                    For n As Integer = 0 To obj_2.TabPages.GroupCount - 1
            '                                                                                        For Each obj_3 As Object In obj_2.TabPages(n).Items
            '                                                                                            If TypeOf obj_3 Is DevExpress.XtraLayout.TabbedControlGroup Then
            '                                                                                                For m As Integer = 0 To obj_3.TabPages.GroupCount - 1
            '                                                                                                    For Each obj_4 As Object In obj_3.TabPages(m).Items
            '                                                                                                        If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlGroup Then
            '                                                                                                            For Each obj_5 As Object In obj_4.items
            '                                                                                                                If TypeOf obj_5 Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                                                                    If obj_5.text.ToString.Contains("Empty") Then
            '                                                                                                                    ElseIf obj_5.text.ToString.Contains("Layout") Then
            '                                                                                                                    Else
            '                                                                                                                        Dim _row As DataRow
            '                                                                                                                        _row = dt_control.NewRow
            '                                                                                                                        _row("form") = Me.Name
            '                                                                                                                        _row("control") = obj_5.name
            '                                                                                                                        _row("text") = obj_5.text
            '                                                                                                                        dt_control.Rows.Add(_row)
            '                                                                                                                    End If
            '                                                                                                                End If
            '                                                                                                            Next
            '                                                                                                        End If
            '                                                                                                        If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                                                            If obj_4.text.ToString.Contains("Empty") Then
            '                                                                                                            ElseIf obj_4.text.ToString.Contains("Layout") Then
            '                                                                                                            Else
            '                                                                                                                Dim _row As DataRow
            '                                                                                                                _row = dt_control.NewRow
            '                                                                                                                _row("form") = Me.Name
            '                                                                                                                _row("control") = obj_4.name
            '                                                                                                                _row("text") = obj_4.text
            '                                                                                                                dt_control.Rows.Add(_row)
            '                                                                                                            End If
            '                                                                                                        End If

            '                                                                                                    Next
            '                                                                                                Next

            '                                                                                            End If

            '                                                                                            If TypeOf obj_3 Is DevExpress.XtraLayout.LayoutControlGroup Then
            '                                                                                                For Each obj_4 As Object In obj_3.items
            '                                                                                                    If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                                                        If obj_4.text.ToString.Contains("Empty") Then
            '                                                                                                        ElseIf obj_4.text.ToString.Contains("Layout") Then
            '                                                                                                        Else
            '                                                                                                            Dim _row As DataRow
            '                                                                                                            _row = dt_control.NewRow
            '                                                                                                            _row("form") = Me.Name
            '                                                                                                            _row("control") = obj_4.name
            '                                                                                                            _row("text") = obj_4.text
            '                                                                                                            dt_control.Rows.Add(_row)
            '                                                                                                        End If
            '                                                                                                    End If
            '                                                                                                Next
            '                                                                                            End If
            '                                                                                            If TypeOf obj_3 Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                                                If obj_3.text.ToString.Contains("Empty") Then
            '                                                                                                ElseIf obj_3.text.ToString.Contains("Layout") Then
            '                                                                                                Else
            '                                                                                                    Dim _row As DataRow
            '                                                                                                    _row = dt_control.NewRow
            '                                                                                                    _row("form") = Me.Name
            '                                                                                                    _row("control") = obj_3.name
            '                                                                                                    _row("text") = obj_3.text
            '                                                                                                    dt_control.Rows.Add(_row)
            '                                                                                                End If
            '                                                                                            End If
            '                                                                                        Next
            '                                                                                    Next
            '                                                                                End If

            '                                                                            Next
            '                                                                        End If
            '                                                                        If TypeOf obj Is DevExpress.XtraLayout.LayoutControlItem Then

            '                                                                            If obj.text.ToString.Contains("Empty") Then
            '                                                                            ElseIf obj.text.ToString.Contains("Layout") Then
            '                                                                            Else
            '                                                                                Dim _row As DataRow
            '                                                                                _row = dt_control.NewRow
            '                                                                                _row("form") = Me.Name
            '                                                                                _row("control") = obj.name
            '                                                                                _row("text") = obj.text
            '                                                                                dt_control.Rows.Add(_row)
            '                                                                            End If

            '                                                                        End If
            '                                                                    Next

            '                                                                Next


            '                                                            End If
            '                                                        Next
            '                                                        For Each ctrl_group As Object In ctrl_pnl.Controls

            '                                                            If TypeOf ctrl_group Is DevExpress.XtraLayout.LayoutControlItem Then
            '                                                                Dim _row As DataRow
            '                                                                _row = dt_control.NewRow
            '                                                                _row("control") = ctrl_group.name
            '                                                                _row("text") = ctrl_group.text
            '                                                                dt_control.Rows.Add(_row)
            '                                                            End If
            '                                                        Next

            '                                                    End If
            '                                                Next
            '                                            End If
            '                                        Next
            '                                    End If

            '                                End If
            '                            Next
            '                        End If
            '                    Next
            '                End If
            '            Next
            '        End If
            '    Next

            '    Dim gcCredentials As New GridControl
            '    Dim gvCredentials As New GridView

            '    gcCredentials.ViewCollection.Add(gvCredentials)
            '    gcCredentials.MainView = gvCredentials
            '    gcCredentials.BindingContext = New BindingContext()

            '    gcCredentials.DataSource = dt_control

            '    gvCredentials.PopulateColumns()
            '    gcCredentials.ForceInitialize()
            '    Dim _file As String = ""
            '    _file = AskSaveAsFile("Excel Files | *.xls")
            '    If _file = "" Then
            '        Exit Sub
            '    End If
            '    gcCredentials.ExportToXls(_file)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
