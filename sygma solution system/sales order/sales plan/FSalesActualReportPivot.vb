Imports master_new.ModFunction

Public Class FSalesActualReportPivot
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSalesActualReportPivot_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'TODO: This line of code loads data into the 'Ds_general_ledger_print1.DataTable1' table. You can move, or remove it, as needed.
        'Me.DataTable1TableAdapter.Fill(Me.Ds_general_ledger_print1.DataTable1)
        form_first_load()
        format_pivot()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal.Date
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal.Date
    End Sub

    Public Overrides Sub load_cb()
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

        add_column_pivot(pgc_ar, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Year", "soshipd_dt", DevExpress.XtraPivotGrid.PivotArea.ColumnArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        add_column_pivot(pgc_ar, "Month", "soshipd_dt", DevExpress.XtraPivotGrid.PivotArea.ColumnArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        add_column_pivot(pgc_ar, "Week", "soship_dt", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateWeekOfMonth, "soship_date_week")
        'add_column_pivot(pgc_ar, "Periode", "plans_periode", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Shipment Date", "pb_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")

        'add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description2", "pt_desc2", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "End User", "pbd_end_user", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Stock", "plansptd_amount", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Actual Sales", "sales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Average Sales", "sales", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "Qty Process", "pbd_qty_processed", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Qty Outstanding", "pbd_qty_outstanding", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")


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

        'set_sql = "SELECT  " _
        '            & "  public.soship_mstr.soship_en_id, " _
        '            & "  public.en_mstr.en_desc, " _
        '            & "  public.plansptd_det.plansptd_pt_id, " _
        '            & "  public.pt_mstr.pt_code, " _
        '            & "  public.pt_mstr.pt_desc1, " _
        '            & "  sum(public.soshipd_det.soshipd_qty *-1) AS sales, " _
        '            & "  public.soshipd_det.soshipd_dt, " _
        '            & "  public.soship_mstr.soship_is_shipment " _
        '            & "FROM " _
        '            & "  public.plansptd_det " _
        '            & "  INNER JOIN public.sod_det ON (public.plansptd_det.plansptd_pt_id = public.sod_det.sod_pt_id) " _
        '            & "  INNER JOIN public.soshipd_det ON (public.sod_det.sod_oid = public.soshipd_det.soshipd_sod_oid) " _
        '            & "  INNER JOIN public.soship_mstr ON (public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid) " _
        '            & "  INNER JOIN public.pt_mstr ON (public.plansptd_det.plansptd_pt_id = public.pt_mstr.pt_id) " _
        '            & "  INNER JOIN public.en_mstr ON (public.soship_mstr.soship_en_id = public.en_mstr.en_id) " _
        '            & "WHERE " _
        '            & "  public.soship_mstr.soship_is_shipment = 'Y' " _
        '            & "  and public.soshipd_det.soshipd_dt >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
        '            & "  and public.soshipd_det.soshipd_dt <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
        '            & "  and soship_en_id in (select user_en_id from tconfuserentity " _
        '            & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '            & "GROUP BY " _
        '            & "  public.soship_mstr.soship_en_id, " _
        '            & "  public.en_mstr.en_desc, " _
        '            & "  public.plansptd_det.plansptd_pt_id, " _
        '            & "  public.pt_mstr.pt_code, " _
        '            & "  public.pt_mstr.pt_desc1, " _
        '            & "  public.soshipd_det.soshipd_dt, " _
        '            & "  public.soship_mstr.soship_is_shipment " _
        '            & "ORDER BY " _
        '            & "  public.soship_mstr.soship_en_id"

        set_sql = "SELECT  " _
                    & "  public.soship_mstr.soship_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  SUM(public.soshipd_det.soshipd_qty *-1) AS sales, " _
                    & "  public.soship_mstr.soship_is_shipment, " _
                    & "  public.soshipd_det.soshipd_dt " _
                    & "FROM " _
                    & "  public.soshipd_det " _
                    & "  INNER JOIN public.sod_det ON (public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid) " _
                    & "  INNER JOIN public.pt_mstr ON (public.sod_det.sod_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.soship_mstr ON (public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.soship_mstr.soship_en_id = public.en_mstr.en_id) " _
                    & "WHERE " _
                    & "  public.soship_mstr.soship_is_shipment = 'Y' " _
                    & "  and public.soshipd_det.soshipd_dt >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & "  and public.soshipd_det.soshipd_dt <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & "  and soship_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & "GROUP BY " _
                    & "  public.soship_mstr.soship_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pt_mstr.pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.soship_mstr.soship_is_shipment, " _
                    & "  public.soshipd_det.soshipd_dt "

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
