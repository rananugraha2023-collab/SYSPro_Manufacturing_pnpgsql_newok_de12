Imports master_new.ModFunction

Public Class FInventoryDashboardAss
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryDashboardAss_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        format_pivot()
        pr_txttglawal.EditValue = master_new.PGSqlConn.CekTanggal.Date
        pr_txttglakhir.EditValue = master_new.PGSqlConn.CekTanggal.Date
    End Sub

    Public Overrides Sub load_cb()
        init_le(pr_entity, "en_mstr")
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor

        Dim _en_id_all As String

        If CBChild.EditValue = True Then
            _en_id_all = get_en_id_child(pr_entity.EditValue)
        Else

            _en_id_all = pr_entity.EditValue

        End If

        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        'If xtc_master.SelectedTabPageIndex = 0 Then
                        '    load_ar(objload)
                        'ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                        '    load_ars(objload)
                        'End If

                        If xtc_ida.SelectedTabPageIndex = 0 Then
                            load_ar(objload)
                        ElseIf xtc_ida.SelectedTabPageIndex = 1 Then
                            load_ars(objload)
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
        add_column_pivot(pgc_ar, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Stock", "invc_qty", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Purchase Order", "purchaseorder", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Receipt", "purchasereceipt", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Purchase Return", "purchasereturn", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Outstading", "outstanding", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Total Stock", "totalstock", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Month Count", "monthcount", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Forecast", "forecast", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "AVGForecast", "avgforecast", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By DS (SF)", "sc_by_fc_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By TS (SF)", "sc_by_fc_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "abc", "abc", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Actual Sales", "actualsales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "AVG Actual Sales", "avg_sales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By DS (AS)", "sc_by_sl_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By TS (AS)", "sc_by_sl_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "abcs", "abcs", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "Actual Sales", "actualsales2", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Qty Process", "pbd_qty_processed", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Qty Outstanding", "pbd_qty_outstanding", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_pivot(pgc_ars, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ars, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ars, "Description", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ars, "Stock", "invc_qty_valid", DevExpress.XtraPivotGrid.PivotArea.RowArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "BOM Part Number", "bom_detail", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ars, "BOM Description", "bom_desc", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        'add_column_pivot(pgc_ars, "Location", "loc_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ars, "BOM Stock", "psd_invc_qty", DevExpress.XtraPivotGrid.PivotArea.RowArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ars, "Year", "pod_dt", DevExpress.XtraPivotGrid.PivotArea.ColumnArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        'add_column_pivot(pgc_ars, "Month", "pod_dt", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        'add_column_pivot(pgc_ars, "Cost", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "Brut", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "Periode", "plans_periode", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "Shipment Date", "pb_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")
        'add_column_pivot(pgc_ars, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "End User", "pbd_end_user", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ars, "Stock", "invc_qty_valid", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Purchase Order", "purchaseorder", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Receipt", "purchasereceipt", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Purchase Return", "purchasereturn", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Outstading", "outstanding", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Total Stock", "totalstock", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Month Count", "monthcount", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Forecast", "tinvc_qty.invc_qty", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "AVGForecast", "avgforecast", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "SC By DS (SF)", "stock_cover_by_sf_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "SC By TS (SF)", "stock_cover_by_sf_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ars, "abc", "abc", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "Actual Sales", "actualsales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "AVG Actual Sales", "avg_sales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "SC By DS (AS)", "stock_cover_by_sales_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ars, "SC By TS (AS)", "stock_cover_by_sales_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ars, "abcs", "abcs", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ars, "Actual Sales", "actualsales2", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ars, "Qty Process", "pbd_qty_processed", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ars, "Qty Outstanding", "pbd_qty_outstanding", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Private Sub load_ar(ByVal par_obj As Object)
        pgc_ar.DataSource = Nothing
        pgc_ar.DataMember = Nothing

        With par_obj
            .SQL = set_sqlar()

            .InitializeCommand()
            .FillDataSet(ds, "data_ar")
            pgc_ar.DataSource = ds.Tables("data_ar")
            pgc_ar.BestFit()
        End With
    End Sub

    Private Sub load_ars(ByVal par_obj As Object)
        pgc_ars.DataSource = Nothing
        pgc_ars.DataMember = Nothing

        With par_obj
            .SQL = set_sqlars()

            .InitializeCommand()
            .FillDataSet(ds, "data_ars")
            pgc_ars.DataSource = ds.Tables("data_ars")
            pgc_ars.BestFit()
        End With
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_ar.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function

    Private Function set_sqlar() As String
        'Private Sub load_detail_issued()
        set_sqlar = "SELECT public.plans_mstr.plans_en_id, " _
                    & " public.en_mstr.en_desc, " _
                    & " public.plansptd_det.plansptd_pt_id, " _
                    & " public.pt_mstr.pt_id, " _
                    & " public.pt_mstr.pt_code, " _
                    & " public.pt_mstr.pt_desc1, " _
                    & " tinvc_qty.invc_qty, " _
                    & " COALESCE((tpod_total.total_pod), 0) AS purchaseorder, " _
                    & " COALESCE((tpod_recieve.pod_recieve), 0) AS purchasereceipt, " _
                    & " COALESCE((tpod_return.pod_return), 0) AS purchasereturn, " _
                    & " COALESCE((tpod_total.total_pod) - tpod_recieve.pod_recieve, 0) AS outstanding, " _
                    & " CASE WHEN tpod_total.total_pod > 0 THEN (COALESCE((tpod_total.total_pod - tpod_recieve.pod_recieve + tinvc_qty.invc_qty), 0)) ELSE tinvc_qty.invc_qty END AS totalstock, " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & ",WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " ) as monthcount, " _
                    & " round(SUM(COALESCE(public.plansptd_det.plansptd_qty, 0))) AS forecast, " _
                    & " round((SUM(COALESCE(public.plansptd_det.plansptd_qty, 0))) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " )) AS avgforecast, " _
                    & " round(tinvc_qty.invc_qty /(round((SUM(COALESCE(nullif( " _
                    & "   public.plansptd_det.plansptd_qty, 0),1))) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " )))) AS sc_by_fc_wo_os, " _
                    & " round(((tpod_total.total_pod - tpod_recieve.pod_recieve) + tinvc_qty.invc_qty) /(round((SUM(COALESCE(nullif(public.plansptd_det.plansptd_qty, 0),1))) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " )))) as sc_by_fc_w_os, " _
                    & " COALESCE((tsod_total.total_sod), 0) AS actualsales, " _
                    & " round((tsod_total.total_sod) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " )) as avg_sales, " _
                    & " round((tinvc_qty.invc_qty) /((tsod_total.total_sod) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " ))) as sc_by_sl_wo_os, " _
                    & " round(((tpod_total.total_pod - tpod_recieve.pod_recieve) + tinvc_qty.invc_qty) /((tsod_total.total_sod) / " _
                    & " ( " _
                    & "   select count(distinct (to_char(plans_date, 'YYYYMM'))) monthcount " _
                    & "   FROM plans_mstr " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " ))) as sc_by_sl_w_os " _
                    & "FROM public.plansptd_det " _
                    & "     INNER JOIN public.plans_mstr ON (public.plansptd_det.plansptd_plans_oid = public.plans_mstr.plans_oid) " _
                    & "     INNER JOIN public.en_mstr ON (public.plans_mstr.plans_en_id = public.en_mstr.en_id) " _
                    & "     INNER JOIN public.pt_mstr ON (public.plansptd_det.plansptd_pt_id = public.pt_mstr.pt_id) " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT public.invc_mstr.invc_pt_id, " _
                    & "        sum(public.invc_mstr.invc_qty) as invc_qty " _
                    & " FROM public.invc_mstr " _
                    & "      INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                    & " WHERE public.loc_mstr.loc_mon = 'Y' " _
                    & " GROUP BY public.invc_mstr.invc_pt_id " _
                    & "     ) tinvc_qty ON tinvc_qty.invc_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty, 0)) as total_pod " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = public.pod_det.pod_po_oid) " _
                    & " WHERE coalesce(public.pod_det.pod_qty_receive, 0) < pod_qty and " _
                    & "       coalesce(public.po_mstr.po_trans_id, 'I') <> 'X' and " _
                    & "       coalesce(lower(public.pod_det.pod_status), '') <> 'c'" _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_total ON tpod_total.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty_receive, 0)) as pod_recieve " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = public.pod_det.pod_po_oid) " _
                    & " WHERE coalesce(public.pod_det.pod_qty_receive, 0) < pod_qty and " _
                    & "       coalesce(public.po_mstr.po_trans_id, 'I') <> 'X' and " _
                    & "       coalesce(lower(public.pod_det.pod_status), '') <> 'c'" _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_recieve ON tpod_recieve.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty_return, 0)) as pod_return " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = public.pod_det.pod_po_oid) " _
                    & " WHERE coalesce(public.pod_det.pod_qty_receive, 0) < pod_qty and " _
                    & "       coalesce(public.po_mstr.po_trans_id, 'I') <> 'X' and " _
                    & "       coalesce(lower(public.pod_det.pod_status), '') <> 'c'" _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_return ON tpod_return.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT sod_pt_id, " _
                    & "        sum(COALESCE(sod_qty_shipment, 0)) as total_sod " _
                    & " FROM so_mstr " _
                    & "      INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                    & " WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                    & "       public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & "       and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " GROUP BY sod_pt_id) tsod_total ON tsod_total.sod_pt_id = plansptd_pt_id " _
                    & " WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & "GROUP BY public.plans_mstr.plans_en_id, " _
                    & "   public.en_mstr.en_desc, " _
                    & "   public.plansptd_det.plansptd_pt_id, " _
                    & "   public.pt_mstr.pt_id, " _
                    & "   public.pt_mstr.pt_code, " _
                    & "   public.pt_mstr.pt_desc1, " _
                    & "   tinvc_qty.invc_qty, " _
                    & "   tpod_total.total_pod, " _
                    & "   tpod_recieve.pod_recieve, " _
                    & "   tpod_return.pod_return, " _
                    & "   tsod_total.total_sod " _
                    & "ORDER BY public.plansptd_det.plansptd_pt_id"

        Return set_sqlar

    End Function

    Private Function set_sqlars() As String
        'Private Sub load_detail_issued()
        set_sqlars = "SELECT DISTINCT public.plans_mstr.plans_en_id, " _
                    & " public.en_mstr.en_desc, " _
                    & " public.plansptd_det.plansptd_pt_id, " _
                    & " public.pt_mstr.pt_code, " _
                    & " public.pt_mstr.pt_desc1, " _
                    & " (case when tinvc_qty.ps_invc_qty is null then tinvc_qtyb.psb_invc_qty else tinvc_qty.ps_invc_qty end) as invc_qty_valid, " _
                    & " tpod_total.total_pod AS purchaseorder, " _
                    & " tpod_recieve.pod_recieve AS purchasereceipt, " _
                    & " tpod_return.pod_return AS purchasereturn, " _
                    & " (tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return) AS outstanding, " _
                    & " public.ps_mstr.ps_oid, " _
                    & " public.ps_mstr.ps_pt_bom_id, " _
                    & " public.ps_mstr.ps_is_assembly, " _
                    & " public.ps_mstr.ps_active, " _
                    & " public.loc_mstr.loc_mon, " _
                    & " (case when tinvc_qty.ps_invc_qty is null then tinvc_qtyb.psb_invc_qty else tinvc_qty.ps_invc_qty end) as invc_qty_valid, " _
                    & " (tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return " _
                    & "   +(case " _
                    & "       when tinvc_qty.ps_invc_qty is null then tinvc_qtyb.psb_invc_qty " _
                    & "       else tinvc_qty.ps_invc_qty " _
                    & "     end)) AS totalstock, " _
                    & " plansptd_qty.plansptd_amount, " _
                    & " public.psd_det.psd_pt_bom_id, " _
                    & " ps_mstrs.pt_code as bom_detail, " _
                    & " ps_mstrs.pt_desc1 as bom_desc, " _
                    & " tinvc_qtyd.psd_invc_qty, " _
                    & " coalesce((plansptdet_qty.planpsd_amount), 0) as planpsd_amount, " _
                    & " public.psd_det.psd_seq " _
                    & " FROM public.plansptd_det " _
                    & "     INNER JOIN public.plans_mstr ON (public.plansptd_det.plansptd_plans_oid = public.plans_mstr.plans_oid) " _
                    & "     INNER JOIN public.en_mstr ON (public.plans_mstr.plans_en_id = public.en_mstr.en_id) " _
                    & "     LEFT OUTER JOIN public.ps_mstr ON (public.plansptd_det.plansptd_pt_id = public.ps_mstr.ps_pt_bom_id) " _
                    & "     LEFT JOIN public.invc_mstr ON (public.ps_mstr.ps_pt_bom_id = public.invc_mstr.invc_pt_id) " _
                    & "     LEFT JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
                    & "     INNER JOIN public.pt_mstr ON (public.plansptd_det.plansptd_pt_id = public.pt_mstr.pt_id) " _
                    & "     LEFT OUTER JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty, 0)) as total_pod " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = " _
                    & "        public.pod_det.pod_po_oid) " _
                    & " WHERE public.po_mstr.po_close_date IS NULL " _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_total ON tpod_total.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty_receive, 0)) as pod_recieve " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = " _
                    & "        public.pod_det.pod_po_oid) " _
                    & " WHERE public.po_mstr.po_close_date IS NULL " _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_recieve ON tpod_recieve.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pod_pt_id, " _
                    & "        sum(COALESCE(pod_qty_return, 0)) as pod_return " _
                    & " FROM po_mstr " _
                    & "      INNER JOIN public.pod_det ON (public.po_mstr.po_oid = " _
                    & "        public.pod_det.pod_po_oid) " _
                    & " WHERE public.po_mstr.po_close_date IS NULL " _
                    & " GROUP BY pod_pt_id " _
                    & "     ) tpod_return ON tpod_return.pod_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT DISTINCT invc_mstr.invc_pt_id, " _
                    & "        sum(invc_mstr.invc_qty) AS ps_invc_qty, " _
                    & "        public.loc_mstr.loc_mon " _
                    & " FROM invc_mstr " _
                    & "      LEFT OUTER JOIN public.ps_mstr ON (invc_mstr.invc_pt_id = " _
                    & "        public.ps_mstr.ps_pt_bom_id) " _
                    & "      INNER JOIN public.loc_mstr ON (invc_mstr.invc_loc_id = " _
                    & "        public.loc_mstr.loc_id) " _
                    & " WHERE public.loc_mstr.loc_mon = 'Y' AND " _
                    & "       public.ps_mstr.ps_active = 'Y' AND " _
                    & "       public.ps_mstr.ps_is_assembly = 'Y' " _
                    & " GROUP BY invc_mstr.invc_pt_id, " _
                    & "          public.loc_mstr.loc_mon " _
                    & "     ) tinvc_qty ON tinvc_qty.invc_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT DISTINCT invc_mstr.invc_pt_id, " _
                    & "        sum(invc_mstr.invc_qty) AS psb_invc_qty, " _
                    & "        public.loc_mstr.loc_mon " _
                    & " FROM invc_mstr " _
                    & "      LEFT OUTER JOIN public.ps_mstr ON (invc_mstr.invc_pt_id = " _
                    & "        public.ps_mstr.ps_pt_bom_id) " _
                    & "      INNER JOIN public.loc_mstr ON (invc_mstr.invc_loc_id = " _
                    & "        public.loc_mstr.loc_id) " _
                    & " WHERE public.loc_mstr.loc_mon = 'Y' " _
                    & " GROUP BY invc_mstr.invc_pt_id, " _
                    & "          public.loc_mstr.loc_mon " _
                    & "     ) tinvc_qtyb ON tinvc_qtyb.invc_pt_id = plansptd_pt_id " _
                    & "     LEFT JOIN public.psd_det ON (public.ps_mstr.ps_oid = " _
                    & " public.psd_det.psd_ps_oid) " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT pt_id, " _
                    & "        pt_code, " _
                    & "        pt_desc1 " _
                    & " FROM public.psd_det " _
                    & "      INNER JOIN public.pt_mstr ON (public.psd_det.psd_pt_bom_id = " _
                    & "        public.pt_mstr.pt_id) " _
                    & "     ) ps_mstrs ON ps_mstrs.pt_id = psd_pt_bom_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT DISTINCT invc_mstr.invc_pt_id, " _
                    & "        sum(invc_mstr.invc_qty) AS psd_invc_qty, " _
                    & "        public.loc_mstr.loc_mon " _
                    & " FROM invc_mstr " _
                    & "      INNER JOIN public.loc_mstr ON (invc_mstr.invc_loc_id = " _
                    & "        public.loc_mstr.loc_id) " _
                    & " WHERE public.loc_mstr.loc_mon = 'Y' " _
                    & " GROUP BY invc_mstr.invc_pt_id, " _
                    & "          public.loc_mstr.loc_mon " _
                    & "     ) tinvc_qtyd ON tinvc_qtyd.invc_pt_id = psd_pt_bom_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT DISTINCT public.plansptd_det.plansptd_pt_id, " _
                    & "        round(SUM(public.plansptd_det.plansptd_amount)) AS plansptd_amount " _
                    & " FROM public.plansptd_det " _
                    & " GROUP BY public.plansptd_det.plansptd_pt_id " _
                    & "     ) plansptd_qty ON plansptd_qty.plansptd_pt_id = " _
                    & " public.plansptd_det.plansptd_pt_id " _
                    & "     LEFT JOIN  " _
                    & "     ( " _
                    & " SELECT DISTINCT public.plansptd_det.plansptd_pt_id, " _
                    & "        round(SUM(public.plansptd_det.plansptd_amount)) AS planpsd_amount " _
                    & " FROM public.plansptd_det " _
                    & " GROUP BY public.plansptd_det.plansptd_pt_id " _
                    & "     ) plansptdet_qty ON plansptdet_qty.plansptd_pt_id = " _
                    & " public.psd_det.psd_pt_bom_id " _
                    & " WHERE public.loc_mstr.loc_mon = 'Y' AND " _
                    & "      public.ps_mstr.ps_active NOT LIKE 'N' AND " _
                    & "      public.ps_mstr.ps_is_assembly = 'Y' OR " _
                    & "      public.ps_mstr.ps_is_assembly IS NULL AND " _
                    & "      public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " and public.plans_mstr.plans_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " GROUP BY public.plansptd_det.plansptd_pt_id, " _
                    & "   public.pt_mstr.pt_code, " _
                    & "   public.pt_mstr.pt_desc1, " _
                    & "   public.ps_mstr.ps_oid, " _
                    & "   public.ps_mstr.ps_pt_bom_id, " _
                    & "   public.ps_mstr.ps_is_assembly, " _
                    & "   public.ps_mstr.ps_dt, " _
                    & "   public.ps_mstr.ps_active, " _
                    & "   public.loc_mstr.loc_mon, " _
                    & "   tinvc_qty.ps_invc_qty, " _
                    & "   tinvc_qtyb.psb_invc_qty, " _
                    & "   public.psd_det.psd_pt_bom_id, " _
                    & "   ps_mstrs.pt_code, " _
                    & "   tinvc_qtyd.psd_invc_qty, " _
                    & "   plansptd_qty.plansptd_amount, " _
                    & "   plansptdet_qty.planpsd_amount, " _
                    & "   public.psd_det.psd_seq, " _
                    & "   public.plans_mstr.plans_en_id, " _
                    & "   public.en_mstr.en_desc, " _
                    & "   public.plansptd_det.plansptd_pt_id, " _
                    & "   tpod_total.total_pod, " _
                    & "   tpod_return.pod_return, " _
                    & "   tpod_recieve.pod_recieve, " _
                    & "   ps_mstrs.pt_desc1 " _
                    & "ORDER BY public.pt_mstr.pt_code, " _
                    & "   public.ps_mstr.ps_oid, " _
                    & "   public.psd_det.psd_seq"

        Return set_sqlars

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
