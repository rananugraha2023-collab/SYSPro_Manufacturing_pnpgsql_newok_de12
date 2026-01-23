Imports DevExpress.XtraGrid
Imports DevExpress.XtraCharts
Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDownTimeChart
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _status_trans As String
    Dim _uid As String
    Dim _now As DateTime

    Private Sub FDownTimeChart_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        read_only(False)
        clear()
        show_master()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        chart_en_id.Properties.DataSource = dt_bantu
        chart_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        chart_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        chart_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        par_en_id.Properties.DataSource = dt_bantu
        par_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_en_id.ItemIndex = 0
    End Sub

    'Public Overrides Sub page_change()
    '    If xtc_master.SelectedTabPageIndex = 0 Then
    '        dp_edit.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '    Else
    '        dp_edit.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
    '    End If
    'End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "chart_uid", False)
        add_column(gv_master, "chart_en_id", False)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Reject Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_data, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_data, "Work Order Code ", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_data, "Operation", "wr_op", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_data, "Purchase Order Code ", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_data, "Date", "wrd_eff_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_data, "Qty ", "wrd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_data, "Setup Start", "wrd_start_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_data, "Setup Stop", "wrd_stop_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_data, "Setup Elapsed", "wrd_elapsed_setup", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_data, "Run Start", "wrd_start_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_data, "Run Stop", "wrd_stop_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_data, "Run Elapsed", "wrd_elapsed_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_data, "Down time", "wrd_down_time", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_data, "Down Time Reason", "down_reason_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_data, "Remarks", "wrd_remark", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then

            If xtc_master.SelectedTabPageIndex = 0 Then
                show_master()
            ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                par_en_id.Focus()
                Try
                    ds = New DataSet
                    Using objload As New master_new.WDABasepgsql("", "")
                        With objload

                            .SQL = "SELECT  " _
                                & "  code_name, count(code_name) as jml_code " _
                                & "FROM " _
                                & "  wrd_det " _
                                & "  inner join code_mstr on code_id = wrd_down_reason_id " _
                                & "  inner join en_mstr on en_id = wrd_en_id " _
                                & "  where wrd_down_time <> 0  " _
                                & "  and wrd_en_id=" & par_en_id.EditValue.ToString & " and wrd_down_reason_id in (select chart_code_id from tconfchart_setting where " _
                                & "  chart_type = 'LF' AND chart_mode = 1 and chart_userid = " & master_new.ClsVar.sUserID.ToString _
                                & "  and chart_en_id = " & par_en_id.EditValue.ToString & " )" _
                                & "  and wrd_eff_date >= " + SetDate(pr_txttglawal.EditValue) + " and wrd_eff_date <= " + SetDate(pr_txttglakhir.EditValue) _
                                & "  group by code_name "
                            .InitializeCommand()
                            .FillDataSet(ds, "data")

                            bestfit_column()
                            ConditionsAdjustment()
                            chart_setting()
                        End With
                    End Using

                    chart_gr.Series(0).DataSource = ds.Tables("data")

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If

        Cursor = Cursors.Arrow
    End Sub

    Private Sub chart_gr_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chart_gr.DoubleClick
        Try
            Dim sSQL As String
            Dim test As ChartHitInfo = chart_gr.CalcHitInfo(chart_gr.PointToClient(MousePosition))
            If test.SeriesPoint IsNot Nothing Then
                sSQL = "SELECT  " _
                    & "  a.wrd_oid, " _
                    & "  a.wrd_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.wrd_wr_oid, " _
                    & "  c.wr_op, " _
                    & "  d.wo_code, " _
                    & "  a.wrd_eff_date, " _
                    & "  a.wrd_qty, " _
                    & "  a.wrd_start_setup, " _
                    & "  a.wrd_stop_setup, " _
                    & "  a.wrd_elapsed_setup, " _
                    & "  a.wrd_start_run, " _
                    & "  a.wrd_stop_run, " _
                    & "  a.wrd_elapsed_run, " _
                    & "  a.wrd_down_time, " _
                    & "  a.wrd_down_reason_id, " _
                    & "  e.code_name AS down_reason_desc, " _
                    & "  a.wrd_sub_cost, " _
                    & "  a.wrd_remark, " _
                    & "  a.wrd_add_by, " _
                    & "  a.wrd_add_date, " _
                    & "  a.wrd_upd_by, " _
                    & "  a.wrd_upd_date,f.po_code " _
                    & "FROM " _
                    & "  public.wrd_det a " _
                    & "  INNER JOIN public.en_mstr b ON (a.wrd_en_id = b.en_id) " _
                    & "  INNER JOIN public.wr_route c ON (a.wrd_wr_oid = c.wr_oid) " _
                    & "  INNER JOIN public.wo_mstr d ON (c.wr_wo_oid = d.wo_oid) " _
                    & "  LEFT OUTER JOIN public.code_mstr e ON (a.wrd_down_reason_id = e.code_id) " _
                    & "  LEFT OUTER JOIN public.po_mstr f ON (a.wrd_po_oid = f.po_oid) " _
                    & "WHERE " _
                    & "  a.wrd_eff_date BETWEEN " & SetDate(pr_txttglawal.EditValue) & " AND " & SetDate(pr_txttglakhir.EditValue) & " " _
                    & " and wrd_en_id=" & par_en_id.EditValue _
                    & " and wrd_down_time <> 0 " _
                    & " and e.code_name ~~* '" + test.SeriesPoint.Argument + "'" _
                    & " ORDER BY " _
                    & "  wo_code, wr_op "

                gc_data.DataSource = GetTableData(sSQL)
                gv_data.BestFitColumns()
                xtc_master.SelectedTabPageIndex = 2


            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gc_master_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gc_master.DoubleClick
        Try
            If gv_master.RowCount = 0 Then
                Exit Sub
            End If

            _uid = gv_master.GetFocusedRowCellValue("chart_uid").ToString
            chart_en_id.EditValue = gv_master.GetFocusedRowCellValue("chart_en_id")
            chart_code_id.EditValue = gv_master.GetFocusedRowCellValue("chart_code_id")
            _status_trans = "edit"

            read_only(True)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAdd.Click
        Try
            _status_trans = ""
            chart_en_id.ItemIndex = 0
            chart_code_id.ItemIndex = 0
            read_only(True)

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub show_master()
        Dim sSQL As String
        Dim _dt As New DataTable

        sSQL = "SELECT  " _
            & "  chart_uid, " _
            & "  chart_userid, " _
            & "  chart_en_id, " _
            & "  chart_code_id, " _
            & "  code_name, " _
            & "  en_desc " _
            & "FROM " _
            & "  public.tconfchart_setting  " _
            & " inner join en_mstr on en_id= chart_en_id " _
            & " inner join code_mstr on code_id= chart_code_id " _
            & " WHERE chart_userid = " & master_new.ClsVar.sUserID.ToString & " and chart_type = 'LF' and  chart_mode = 1 " _
            & "ORDER BY " _
            & " en_desc, code_name"

        _dt = GetTableData(sSQL)

        If _dt.Rows.Count = 0 Then
            _dt = copy_from_template()
        End If

        gc_master.DataSource = _dt
        gv_master.BestFitColumns()

        chart_en_id.ItemIndex = 0
        chart_code_id.ItemIndex = 0
        read_only(False)
    End Sub

    Private Function copy_from_template() As DataTable
        copy_from_template = Nothing

        Dim sSQL As String
        Dim _dt As New DataTable

        sSQL = "SELECT  " _
            & "  chart_uid, " _
            & "  chart_en_id, " _
            & "  chart_code_id, " _
            & "  code_name, " _
            & "  en_desc " _
            & "FROM " _
            & " public.tconfchart_setting " _
            & " inner join en_mstr on en_id= chart_en_id " _
            & " inner join code_mstr on code_id = chart_code_id " _
            & " WHERE coalesce(chart_userid,-99)= -99 and chart_type = 'LF' and chart_mode = 1 "

        _dt = GetTableData(sSQL)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each _dr As DataRow In _dt.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                   & "  public.tconfchart_setting " _
                                                   & "( " _
                                                   & "  chart_uid, " _
                                                   & "  chart_code_id, " _
                                                   & "  chart_type, " _
                                                   & "  chart_mode, " _
                                                   & "  chart_userid, " _
                                                   & "  chart_en_id " _
                                                   & ")  " _
                                                   & "VALUES ( " _
                                                   & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                   & SetInteger(_dr("chart_code_id")) & ",  " _
                                                   & SetSetring("LF") & ",  " _
                                                   & SetInteger("1") & ",  " _
                                                   & SetInteger(master_new.ClsVar.sUserID) & ",  " _
                                                   & SetInteger(_dr("chart_en_id")) _
                                                   & ")"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        Return Nothing
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try

        sSQL = "SELECT  " _
           & "  chart_uid, " _
           & "  chart_en_id, " _
           & "  chart_code_id, " _
           & "  code_name, " _
           & "  en_desc " _
           & "FROM " _
           & "  publitconfchart_setting " _
           & " inner join en_mstr on en_id = chart_en_id " _
           & " inner join code_mstr on code_id = chart_code_id " _
           & " WHERE chart_userid = " & master_new.ClsVar.sUserID.ToString & " and chart_type = 'LF' and  chart_mode = 1 " _
           & "ORDER BY " _
           & " en_desc, code_name"

        _dt = GetTableData(sSQL)

        Return _dt
    End Function

    Private Sub btDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btDel.Click
        Dim sSQL As String
        Try
            If _status_trans = "edit" Then
                sSQL = "delete from tconfchart_setting where chart_uid = '" & _uid & "'"
                DbRun(sSQL)

                show_master()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSave.Click
        Dim sSQL As String
        Try
            If _status_trans = "edit" Then
                sSQL = "UPDATE  " _
                    & "  public.tconfchart_setting   " _
                    & "SET  " _
                    & "  chart_code_id = " & SetInteger(chart_code_id.EditValue) & ",  " _
                    & "  chart_en_id=" & SetInteger(chart_en_id.EditValue) _
                    & "  " _
                    & "WHERE  " _
                    & "  chart_uid = " & SetSetring(_uid) & " "
                DbRun(sSQL)
            Else
                sSQL = "INSERT INTO  " _
                       & "  public.tconfchart_setting " _
                       & "( " _
                       & "  chart_uid, " _
                       & "  chart_code_id, " _
                       & "  chart_type, " _
                       & "  chart_mode, " _
                       & "  chart_userid, chart_en_id " _
                       & ")  " _
                       & "VALUES ( " _
                       & SetSetring(Guid.NewGuid.ToString) & ",  " _
                       & SetInteger(chart_code_id.EditValue) & ",  " _
                       & SetSetring("LF") & ",  " _
                       & SetInteger("1") & ",  " _
                       & SetInteger(master_new.ClsVar.sUserID) & ",  " _
                       & SetInteger(chart_en_id.EditValue) _
                       & ")"
                DbRun(sSQL)
            End If

            show_master()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub read_only(ByVal par_status As Boolean)
        chart_code_id.Enabled = par_status
        chart_en_id.Enabled = par_status
        If par_status = True Then
            chart_en_id.Focus()
        End If
    End Sub

    Private Sub clear()
        chart_en_id.ItemIndex = 0
        chart_code_id.ItemIndex = 0
    End Sub

    Private Sub chart_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chart_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(chart_en_id.EditValue, "down_reason"))
        With chart_code_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub reset()
        If MessageBox.Show("Reset This Configuration Setting...?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim FileToDelete As String
        FileToDelete = "c:\syspro\chart\" + Me.Name + "_" + master_new.ClsVar.sUserID.ToString + ".xml"

        If System.IO.File.Exists(FileToDelete) = True Then
            System.IO.File.Delete(FileToDelete)
        End If

        Dim sSQL As String
        sSQL = "delete from tconfchart_setting where chart_type = 'LF' and chart_mode = 1 and chart_userid = " + master_new.ClsVar.sUserID.ToString
        DbRun(sSQL)
        show_master()
    End Sub
End Class
