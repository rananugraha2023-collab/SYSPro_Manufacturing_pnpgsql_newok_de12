Imports master_new.ModFunction
Imports DevExpress.XtraEditors.Controls

Public Class FWorkOrderReportEntityPivot
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FWorkOrderReportEntityPivot_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        format_pivot()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal.Date
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal.Date
    End Sub

    Public Overrides Sub load_cb()
        Dim sSQL As String

        sSQL = "select wog_code , wog_name  from wog_group " _
         & "order by wog_name "
        Dim dt As New DataTable

        dt = master_new.PGSqlConn.GetTableData(sSQL)
        With wog_code
            If .Properties.Columns.VisibleCount = 0 Then
                .Properties.Columns.Add(New LookUpColumnInfo("wog_code", "Code", 10))
                .Properties.Columns.Add(New LookUpColumnInfo("wog_name", "Name", 20))
            End If

            .Properties.DataSource = dt
            .Properties.DisplayMember = dt.Columns("wog_name").ToString
            .Properties.ValueMember = dt.Columns("wog_code").ToString
            .ItemIndex = 0
            .Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit
            .Properties.BestFit()
            .Properties.DropDownRows = 6
        End With

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            load_ar(objload)
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
    Private Sub format_pivot()

        add_column_pivot_no_total(pgc_ar, "SO", "so_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "SO Date", "so_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "so_date_date")
        add_column_pivot(pgc_ar, "Sold To", "ptnr_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "WO", "wo_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "WO Date", "wo_ord_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "wo_date_date")

        add_column_pivot(pgc_ar, "PT Project", "pt_code_prj", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "PT Project Desc", "pt_desc1_prj", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "PT WO", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "PT WO Desc", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.RowArea)

        add_column_pivot(pgc_ar, "Work Center", "wc_desc", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)
        add_column_pivot(pgc_ar, "Start Date", "lbrf_date", DevExpress.XtraPivotGrid.PivotArea.ColumnArea)

        add_column_pivot(pgc_ar, "Routing Number", "wodr_seq", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Qty In", "wodr_qty_in", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Comp", "wodr_qty_complete", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Rjc", "wodr_qty_reject", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Ostd", "wodr_qty_outstanding", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Out", "wodr_qty_out", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Conv", "wodr_qty_conversion", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        pgc_ar.OptionsView.ShowColumnGrandTotals = False
        pgc_ar.OptionsView.ShowRowGrandTotals = False
        CE_Filter_Group.EditValue = False
    End Sub
    Private Sub load_ar(ByVal par_obj As Object)
        pgc_ar.DataSource = Nothing
        pgc_ar.DataMember = Nothing

        With par_obj
            .SQL = set_sql()

            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_ar.DataSource = ds.Tables("data_ar")
            pgc_ar.BestFit()
        End With
    End Sub
    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_ar.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
    Private Function set_sql() As String
        If wc_group.Checked = False Then
            set_sql = "SELECT  " _
                & "wo_mstr.wo_code,so_date, " _
                & "wo_mstr.wo_ord_date, " _
                & "so_code, " _
                & "ptnr_mstr.ptnr_name, " _
                & "pt_prj.pt_code as pt_code_prj, " _
                & "pt_prj.pt_desc1 as pt_desc1_prj, " _
                & "pt_wo.pt_code as pt_code, " _
                & "pt_wo.pt_desc1 as pt_desc1,wodr_qty_conversion, " _
                & "to_char(wodr_routing.wodr_seq,'09 ') as wodr_seq, wc_mstr.wc_desc ,coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0) as wodr_qty_outstanding," _
                & "coalesce(wodr_routing.wodr_qty_in,0) as wodr_qty_in,coalesce(wodr_qty_complete,0) as wodr_qty_complete,coalesce(wodr_qty_reject,0) as wodr_qty_reject,coalesce(wodr_qty_out,0) as wodr_qty_out " _
                & "FROM " _
                & "  public.wo_mstr  " _
                & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
                & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
                & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
                & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
                & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
                & " Where  wo_status = 'R' and wo_ord_date between " & SetDate(pr_txttglawal.DateTime.Date) _
                & " and " & SetDate(pr_txttglakhir.DateTime.Date)


            If CE_Filter_Group.EditValue = True Then
                set_sql = set_sql & " and wo_wog_code=" & SetSetring(wog_code.EditValue)
            End If
        Else

            set_sql = "SELECT  " _
              & "wo_mstr.wo_code,so_date, " _
              & "wo_mstr.wo_ord_date, " _
              & "so_code, " _
              & "ptnr_mstr.ptnr_name, " _
              & "pt_prj.pt_code as pt_code_prj, " _
              & "pt_prj.pt_desc1 as pt_desc1_prj, " _
              & "pt_wo.pt_code as pt_code, " _
              & "pt_wo.pt_desc1 as pt_desc1,wodr_qty_conversion, " _
              & "to_char(wodr_routing.wodr_seq,'09 ') as wodr_seq, dpt_code || '_' || dpt_desc as wc_desc ,coalesce(wodr_routing.wodr_qty_in,0)- coalesce(wodr_qty_complete,0) -coalesce(wodr_qty_reject,0) as wodr_qty_outstanding," _
              & "coalesce(wodr_routing.wodr_qty_in,0) as wodr_qty_in,coalesce(wodr_qty_complete,0) as wodr_qty_complete,coalesce(wodr_qty_reject,0) as wodr_qty_reject,coalesce(wodr_qty_out,0) as wodr_qty_out, " _
              & "lbrf_date " _
              & "FROM " _
              & "  public.wo_mstr  " _
              & "  INNER JOIN public.wodr_routing ON (public.wo_mstr.wo_oid = public.wodr_routing.wodr_wo_oid) " _
              & "  INNER JOIN public.wc_mstr ON (public.wodr_routing.wodr_wc_id = public.wc_mstr.wc_id) " _
              & "  INNER JOIN public.lbrf_mstr ON public.wodr_routing.wodr_uid = public.lbrf_mstr.lbrf_wodr_uid " _
              & "  INNER JOIN public.so_mstr ON (public.wo_mstr.wo_so_oid = public.so_mstr.so_oid) " _
              & "  INNER JOIN public.ptnr_mstr ON (public.so_mstr.so_ptnr_id_bill = public.ptnr_mstr.ptnr_id) " _
              & "  INNER JOIN public.pt_mstr pt_wo ON (public.wo_mstr.wo_pt_id = pt_wo.pt_id) " _
              & "  INNER JOIN public.pt_mstr pt_prj ON  (public.wo_mstr.wo_pt_id_prj = pt_prj.pt_id) " _
               & "   LEFT outer JOIN public.dpt_mstr ON (wc_dpt_id = public.dpt_mstr.dpt_id) " _
              & " Where wo_status = 'R' and wo_ord_date between " & SetDate(pr_txttglawal.DateTime.Date) _
              & " and " & SetDate(pr_txttglakhir.DateTime.Date)


            If CE_Filter_Group.EditValue = True Then
                set_sql = set_sql & " and wo_wog_code=" & SetSetring(wog_code.EditValue)
            End If

        End If


        Return set_sql
    End Function



    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption

    End Sub

    Public Sub add_column_pivot_no_total(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                              ByVal par_area As DevExpress.XtraPivotGrid.PivotArea)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
        pvg_field.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None


    End Sub

    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal par_group_interval As DevExpress.XtraPivotGrid.PivotGroupInterval, ByVal par_column_name As String)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Name = par_column_name
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
        pvg_field.GroupInterval = par_group_interval


    End Sub
    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                               ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption
        pvg_field.CellFormat.FormatType = formatType
        pvg_field.CellFormat.FormatString = formatString
        pvg_field.GrandTotalCellFormat.FormatType = formatType
        pvg_field.GrandTotalCellFormat.FormatString = formatString
        pvg_field.ValueFormat.FormatType = formatType
        pvg_field.ValueFormat.FormatString = formatString
        pvg_field.TotalCellFormat.FormatType = formatType
        pvg_field.TotalCellFormat.FormatString = formatString
        pvg_field.TotalValueFormat.FormatType = formatType
        pvg_field.TotalValueFormat.FormatString = formatString

    End Sub
End Class
