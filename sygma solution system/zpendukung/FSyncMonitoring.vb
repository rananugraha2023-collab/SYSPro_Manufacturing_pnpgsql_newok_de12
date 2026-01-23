Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FSyncMonitoring
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _sql As String
    Public _par_item As String

    Private Sub FInventoryReportDetailLog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        center_split(SplitContainerControl1, "width")
        center_split(SplitContainerControl2, "height")
        center_split(SplitContainerControl3, "height")

        add_column_copy(gv_synci_summ, "Serv Code", "synci_serv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_synci_summ, "Serv Name", "serv_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_synci_summ, "Time Req", "synci_time_request", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_synci_summ, "Download", "synci_time_download", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_synci_summ, "Count", "synci_records_count", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_synco_summ, "Serv Code", "synco_serv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_synco_summ, "Serv Name", "serv_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_synco_summ, "Time Req", "synco_time_request", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_synco_summ, "Download", "synco_time_download", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_synco_summ, "Count", "synco_records_count", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_jobo_summ, "Serv Code", "jobo_serv_code_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_jobo_summ, "Serv Name", "serv_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_jobo_summ, "Count", "jml", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_jobi_summ, "Serv Code", "jobi_serv_code_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_jobi_summ, "Serv Name", "serv_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_jobi_summ, "Count", "jml", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_cb()
       
    End Sub

    Public Overrides Sub format_grid()
      
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor

        'Dim _en_id_all As String

        If arg <> False Then
            '================================================================
            Try
                show_summ()
                'ds = New DataSet
                'Using objload As New master_new.WDABasepgsql("", "")
                '    With objload
                '        'If _sql <> "" Then
                '        '    .SQL = _sql
                '        'Else
                '        '    .SQL = "SELECT  " _
                '        '   & "  invc_oid, " _
                '        '   & "  invc_dom_id, " _
                '        '   & "  invc_en_id, " _
                '        '   & "  invc_si_id, " _
                '        '   & "  invc_loc_id, " _
                '        '   & "  invc_pt_id, " _
                '        '   & "  invc_serial, " _
                '        '   & "  sum(invc_qty) as invc_qty_sum,sum(invc_qty_old) as invc_qty_old, " _
                '        '   & "  en_desc, " _
                '        '   & "  si_desc, " _
                '        '   & "  loc_desc, " _
                '        '   & "  pt_code, " _
                '        '   & "  pt_desc1, " _
                '        '   & "  pt_desc2, " _
                '        '   & "  pl_desc, " _
                '        '   & "  pt_cost,um_mstr.code_name as um_name " _
                '        '   & "FROM  " _
                '        '   & "  invc_mstr " _
                '        '   & "  inner join en_mstr on en_id = invc_en_id " _
                '        '   & "  inner join si_mstr on si_id = invc_si_id " _
                '        '   & "  inner join loc_mstr on loc_id = invc_loc_id " _
                '        '   & "  inner join pt_mstr on pt_id = invc_pt_id " _
                '        '   & "  inner join code_mstr um_mstr on pt_um = um_mstr.code_id  " _
                '        '   & "  inner join pl_mstr on pt_pl_id = pl_id " _
                '        '   & "  where invc_en_id in (" + _en_id_all & ") "

                '        'End If

                '        'If BtEItem.Text <> "" Then
                '        '    .SQL = .SQL & " and pt_id in (" & _par_item & ") "
                '        'End If

                '        '.SQL = .SQL & "  group by invc_oid, " _
                '        '   & "  invc_dom_id, " _
                '        '   & "  invc_en_id, " _
                '        '   & "  invc_si_id, " _
                '        '   & "  invc_loc_id, " _
                '        '   & "  invc_pt_id, " _
                '        '   & "  invc_serial, " _
                '        '   & "  en_desc, " _
                '        '   & "  si_desc, " _
                '        '   & "  loc_desc, " _
                '        '   & "  pt_code, " _
                '        '   & "  pt_desc1, " _
                '        '   & "  pt_desc2, " _
                '        '   & "  pl_desc, " _
                '        '   & "  pt_cost, um_mstr.code_name "

                '        '.InitializeCommand()
                '        '.FillDataSet(ds, "inv_location")
                '        'gc_loc.DataSource = ds.Tables("inv_location")


                '        '.SQL = "SELECT  " _
                '        '    & "  invc_oid, " _
                '        '    & "  invc_dom_id, " _
                '        '    & "  invc_en_id, " _
                '        '    & "  invc_si_id, " _
                '        '    & "  invc_loc_id, " _
                '        '    & "  invc_pt_id, " _
                '        '    & "  invc_serial, " _
                '        '    & "  invc_qty, " _
                '        '    & "  en_desc, " _
                '        '    & "  si_desc, " _
                '        '    & "  loc_desc, " _
                '        '    & "  pt_code, " _
                '        '    & "  pt_desc1, " _
                '        '    & "  pt_desc2, " _
                '        '    & "  pl_desc, " _
                '        '    & "  pt_cost " _
                '        '    & "FROM  " _
                '        '    & "  invc_mstr " _
                '        '    & "  inner join en_mstr on en_id = invc_en_id " _
                '        '    & "  inner join si_mstr on si_id = invc_si_id " _
                '        '    & "  inner join loc_mstr on loc_id = invc_loc_id " _
                '        '    & "  inner join pt_mstr on pt_id = invc_pt_id " _
                '        '    & "  inner join pl_mstr on pt_pl_id = pl_id " _
                '        '    & " where invc_en_id = " + pr_entity.EditValue.ToString

                '        '.InitializeCommand()
                '        '.FillDataSet(ds, "serial")
                '        'gc_serial.DataSource = ds.Tables("serial")

                '        'Dim ds_detail As New DataSet
                '        '.SQL = "SELECT  " _
                '        '        & "  public.invc_mstr.invc_oid, " _
                '        '        & "  public.sod_det.sod_invc_oid, " _
                '        '        & "  public.sod_det.sod_qty_allocated, " _
                '        '        & "  public.sod_det.sod_qty_allocated - sum(coalesce(public.soshipd_det.soshipd_qty_allocated,0)) as qty_alloc_remain , " _
                '        '        & "  public.so_mstr.so_code " _
                '        '        & "FROM " _
                '        '        & "  public.sod_det " _
                '        '        & "  INNER JOIN public.so_mstr ON (public.sod_det.sod_so_oid = public.so_mstr.so_oid) " _
                '        '        & "  LEFT OUTER JOIN public.soship_mstr ON (public.so_mstr.so_oid = public.soship_mstr.soship_so_oid) " _
                '        '        & "  LEFT OUTER JOIN public.soshipd_det ON (public.soship_mstr.soship_oid = public.soshipd_det.soshipd_soship_oid) " _
                '        '        & "  INNER JOIN public.invc_mstr ON (public.sod_det.sod_invc_oid = public.invc_mstr.invc_oid)" _
                '        '        & " GROUP BY " _
                '        '        & "  public.invc_mstr.invc_oid, " _
                '        '        & "  public.sod_det.sod_invc_oid, " _
                '        '        & "  public.sod_det.sod_qty_allocated, " _
                '        '        & "  public.so_mstr.so_code " _
                '        '        & " HAVING public.sod_det.sod_qty_allocated - sum(coalesce(public.soshipd_det.soshipd_qty_allocated,0)) > 0 "


                '        '.InitializeCommand()
                '        '.FillDataSet(ds_detail, "invc_allocate")
                '        'gc_detail.DataSource = ds_detail.Tables("invc_allocate")
                '        'relation_detail()

                '        'bestfit_column()
                '        'ConditionsAdjustment()
                '    End With
                'End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub center_split(ByVal par_split_container As DevExpress.XtraEditors.SplitContainerControl, ByVal par_opsi As String)
        If par_opsi.ToUpper = "WIDTH" Then
            par_split_container.SplitterPosition = par_split_container.Width / 2
        Else
            par_split_container.SplitterPosition = par_split_container.Height / 2
        End If

    End Sub

    Private Sub FSyncMonitoring_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        center_split(SplitContainerControl1, "width")
        center_split(SplitContainerControl2, "height")
        center_split(SplitContainerControl3, "height")

    End Sub

    Private Sub show_summ()
        Dim sSQL As String
        Try
            'LblLastTime.Text = "Last Refresh : " & Now.ToString("dd/MM/yyyy HH:mm:ss")
            Dim dt As New DataTable

            sSQL = "SELECT  " _
                & "  a.synci_serv_code, " _
                & "  a.synci_time_request, " _
                & "  a.synci_time_download, " _
                & "  a.synci_records_count,serv_name " _
                & "FROM " _
                & "  public.synci_sync_in a " _
                & "INNER JOIN serv_server b on (a.synci_serv_code=b.serv_code) " _
                & "WHERE " _
                & "  a.synci_close = 'N' " _
                & "ORDER BY " _
                & "  a.synci_time_request"

            gc_synci_summ.DataSource = GetTableData(sSQL, "SYNC")
            gv_synci_summ.BestFitColumns()


            sSQL = "SELECT  " _
                & "  a.synco_serv_code, " _
                & "  a.synco_time_request, " _
                & "  a.synco_time_download, " _
                & "  a.synco_records_count ,serv_name " _
                & "FROM " _
                & "  public.synco_sync_out a " _
                  & "INNER JOIN serv_server b on (a.synco_serv_code=b.serv_code) " _
                & "WHERE " _
                & "  a.synco_close = 'N' " _
                & "ORDER BY " _
                & "  a.synco_time_request"


            dt = GetTableData(sSQL, "SYNC")
            gc_synco_summ.DataSource = dt
            gv_synco_summ.BestFitColumns()


            sSQL = "SELECT  " _
                & " jobo_serv_code_to, count(jobo_uid) as jml , serv_name " _
                & "FROM " _
                & "  public.jobo_job_out a " _
                 & "INNER JOIN serv_server b on (a.jobo_serv_code_to=b.serv_code) " _
                & "WHERE " _
                & "  a.jobo_status = 'N' " _
                & "  GROUP BY  jobo_serv_code_to, serv_name " _
                & "ORDER BY " _
                & "  a.jobo_serv_code_to"


            dt = GetTableData(sSQL, "SYNC")
            gc_jobo_summ.DataSource = dt
            gv_jobo_summ.BestFitColumns()

            sSQL = "SELECT  " _
                & " jobi_serv_code_from, count(jobi_uid) as jml, serv_name " _
                & "FROM " _
                & "  public.jobi_job_in a " _
                 & "INNER JOIN serv_server b on (a.jobi_serv_code_from=b.serv_code) " _
                & "WHERE " _
                & "  a.jobi_status = 'N' " _
                & "  GROUP BY  jobi_serv_code_from , serv_name " _
                & "ORDER BY " _
                & "  a.jobi_serv_code_from"


            gc_jobi_summ.DataSource = GetTableData(sSQL, "SYNC")
            gv_jobi_summ.BestFitColumns()

            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class



