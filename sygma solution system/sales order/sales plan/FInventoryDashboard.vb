Imports master_new.ModFunction

Public Class FInventoryDashboard
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryDashboard_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        'add_column_pivot(pgc_ar, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        'add_column_pivot(pgc_ar, "Sold To", "ptnr_name", DevExpress.XtraPivotGrid.PivotArea.RowArea)

        'add_column_pivot(pgc_ar, "SO Number", "so_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Pay Type", "pay_type_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Effective Date", "so_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea)

        'add_column_pivot(pgc_ar, "Sales Program", "sls_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Sales ID", "ptnr_id", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Sales Code", "ptnr_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Sales", "sales_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "PS Status", "ps_status", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Customer Group", "ptnrg_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Price List", "pi_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Shipment / Return Number", "soship_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Shipment Year", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        'add_column_pivot(pgc_ar, "Shipment Month", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        'add_column_pivot(pgc_ar, "Shipment Date", "soship_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")

        'add_column_pivot(pgc_ar, "Is Shipment", "soship_is_shipment", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Currency", "cu_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Exchange Rate", "so_exc_rate", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description2", "pt_desc2", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "UM", "um_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Location", "loc_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Reason Code", "reason_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Taxable", "sod_taxable", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Tax Include", "sod_tax_inc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Tax Class", "tax_name", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Qty", "soshipd_qty", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Total Sales", "sales_ttl", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Discount Value", "disc_value", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "FP Price", "price_fp", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "FP Disc. Value", "disc_fp", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Netto", "dpp", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_pivot(pgc_ar, "Sales Unit", "sod_sales_unit", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")



        add_column_pivot(pgc_ar, "Entity", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Location", "loc_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Year", "pod_dt", DevExpress.XtraPivotGrid.PivotArea.ColumnArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        'add_column_pivot(pgc_ar, "Month", "pod_dt", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        add_column_pivot(pgc_ar, "Cost", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Brut", "en_desc", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Periode", "plans_periode", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Shipment Date", "pb_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")
        'add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "End User", "pbd_end_user", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Stock (a)", "invc_qty", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Purchase Order", "purchaseorder", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Receipt", "purchasereceipt", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Purchase Return", "purchasereturn", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Outstading (b)", "outstanding", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Total Stock", "totalstock", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Month Count", "monthcount", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Forecast", "forecast", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "AVGForecast", "avgforecast", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By SF", "stock_cover_by_sf_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By SF OS", "stock_cover_by_sf_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "abc", "abc", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "Actual Sales", "actualsales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "AVG Sales", "avg_sales", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By Sls", "stock_cover_by_sales_wo_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        add_column_pivot(pgc_ar, "SC By Sls OS", "stock_cover_by_sales_w_os", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "abcs", "abcs", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n0")
        'add_column_pivot(pgc_ar, "Actual Sales", "actualsales2", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
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
        '        & "  public.pod_det.pod_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  public.pt_mstr.pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.invld_table.invld_monitored, " _
        '        & "  public.invc_mstr.invc_loc_id, " _
        '        & "  public.loc_mstr.loc_desc, " _
        '        & "  public.invc_mstr.invc_qty, " _
        '        & "  SUM(public.pod_det.pod_qty) AS po_release, " _
        '        & "  SUM(public.pod_det.pod_qty_receive) AS po_receipt, " _
        '        & "  SUM(public.pod_det.pod_qty) - sum(public.pod_det.pod_qty_receive) AS outstanding, " _
        '        & "  public.invc_mstr.invc_qty + sum(public.pod_det.pod_qty) - sum(public.pod_det.pod_qty_receive) AS total, " _
        '        & "  SUM(public.plansptd_det.plansptd_amount) AS sf, " _
        '        & "  round(avg(public.plansptd_det.plansptd_amount)) AS rata, " _
        '        & "  round(public.invc_mstr.invc_qty / nullif(avg(public.plansptd_det.plansptd_amount), 0)) AS a, " _
        '        & "  round((public.invc_mstr.invc_qty + (SUM(public.pod_det.pod_qty) - sum(public.pod_det.pod_qty_receive))) / round(nullif(avg(public.plansptd_det.plansptd_amount), 0))) AS ab, " _
        '        & "  round((public.invc_mstr.invc_qty + sum(public.pod_det.pod_qty_receive) + (sum(public.pod_det.pod_qty) - sum(public.pod_det.pod_qty_receive))) / round(nullif(avg(public.plansptd_det.plansptd_amount), 0))) AS abc, " _
        '        & "  public.pod_det.pod_dt " _
        '        & "FROM " _
        '        & "  public.pt_mstr " _
        '        & "  INNER JOIN public.invld_table ON (public.pt_mstr.pt_id = public.invld_table.invld_pt_id) " _
        '        & "  INNER JOIN public.pod_det ON (public.invld_table.invld_pt_id = public.pod_det.pod_pt_id) " _
        '        & "  INNER JOIN public.invc_mstr ON (public.pt_mstr.pt_id = public.invc_mstr.invc_pt_id) " _
        '        & "  LEFT OUTER JOIN public.plansptd_det ON (public.pt_mstr.pt_id = public.plansptd_det.plansptd_pt_id) " _
        '        & "  INNER JOIN public.en_mstr ON (public.pod_det.pod_en_id = public.en_mstr.en_id) " _
        '        & "  INNER JOIN public.loc_mstr ON (public.invc_mstr.invc_loc_id = public.loc_mstr.loc_id) " _
        '        & "WHERE " _
        '        & "  public.invld_table.invld_monitored = 'Y' AND  " _
        '        & "  public.invc_mstr.invc_loc_id = '301' AND  " _
        '        & "  public.pod_det.pod_dt >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
        '        & " and public.pod_det.pod_dt <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
        '        & " and public.pod_det.pod_en_id in (select user_en_id from tconfuserentity " _
        '        & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '        & "GROUP BY " _
        '        & "  public.pod_det.pod_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  public.pt_mstr.pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.invld_table.invld_monitored, " _
        '        & "  public.invc_mstr.invc_loc_id, " _
        '        & "  public.invc_mstr.invc_qty, " _
        '        & "  public.loc_mstr.loc_desc, " _
        '        & "  public.pod_det.pod_dt"

        set_sql = "SELECT public.plans_mstr.plans_en_id, " _
                & " public.en_mstr.en_desc, " _
                & " plansptd_pt_id, " _
                & " public.pt_mstr.pt_code, " _
                & " public.pt_mstr.pt_desc1, " _
                & " tpod_total.total_pod AS purchaseorder, " _
                & " tpod_recieve.pod_recieve AS purchasereceipt, " _
                & " tpod_return.pod_return AS purchasereturn, " _
                & " (tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return) AS outstanding, " _
                & " tinvc_qty.invc_qty, " _
                & " (tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return + tinvc_qty.invc_qty) AS totalstock, " _
                & " sum(plansptd_amount) as forecast, " _
                & " round(avg(public.plansptd_det.plansptd_amount)) AS avgforecast, " _
                & " tsod_total.total_sod AS actualsales, " _
                & " round(tinvc_qty.invc_qty / (avg(nullif (public.plansptd_det.plansptd_amount,0)))) AS stock_cover_by_sf_wo_os, " _
                & "       round((tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return + tinvc_qty.invc_qty) / (avg(nullif (public.plansptd_det.plansptd_amount,0)))) AS stock_cover_by_sf_w_os," _
                & "   (select count(distinct (to_char(so_close_date, 'YYYYMM'))) monthcount " _
                & "         FROM so_mstr " _
                & "       WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                & "             public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & "             and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & "       ) as monthcount, " _
                & " round ((tsod_total.total_sod) / ( " _
                & "         select count(distinct (to_char(so_close_date, 'YYYYMM'))) monthcount " _
                & "         FROM so_mstr " _
                & "       WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                & "             public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & "             and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & "       )) as avg_sales, " _
                & " round ((tinvc_qty.invc_qty) /((tsod_total.total_sod) / ( " _
                & "         select count(distinct (to_char(so_close_date, 'YYYYMM'))) monthcount " _
                & "         FROM so_mstr " _
                & "       WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                & "             public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & "             and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & "       ))) as stock_cover_by_sales_wo_os, " _
                & " round ((tpod_total.total_pod - tpod_recieve.pod_recieve + tpod_return.pod_return " _
                & "         + tinvc_qty.invc_qty) /((tsod_total.total_sod) / ( " _
                & "         select count(distinct (to_char(so_close_date, 'YYYYMM'))) monthcount " _
                & "         FROM so_mstr " _
                & "       WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                & "             public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & "             and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & "       ))) as stock_cover_by_sales_w_os " _
                & " FROM plansptd_det " _
                & " INNER JOIN public.plans_mstr ON (public.plansptd_det.plansptd_plans_oid = public.plans_mstr.plans_oid) " _
                & " INNER JOIN public.en_mstr ON (public.plans_mstr.plans_en_id = public.en_mstr.en_id) " _
                & " INNER JOIN public.pt_mstr ON (public.plansptd_det.plansptd_pt_id =public.pt_mstr.pt_id) " _
                & " LEFT OUTER JOIN " _
                & "     (SELECT pod_pt_id,sum(COALESCE(pod_qty, 0)) as total_pod " _
                & "       FROM po_mstr " _
                & "       		INNER JOIN public.pod_det ON (public.po_mstr.po_oid =public.pod_det.pod_po_oid) " _
                & "       WHERE public.po_mstr.po_close_date IS NULL " _
                & "       GROUP BY pod_pt_id) tpod_total ON tpod_total.pod_pt_id = plansptd_pt_id " _
                & "     LEFT JOIN  " _
                & "     (SELECT pod_pt_id, sum(COALESCE(pod_qty_receive, 0)) as pod_recieve " _
                & "       FROM po_mstr " _
                & "            INNER JOIN public.pod_det ON (public.po_mstr.po_oid = public.pod_det.pod_po_oid) " _
                & "       WHERE public.po_mstr.po_close_date IS NULL " _
                & "       GROUP BY pod_pt_id) tpod_recieve ON tpod_recieve.pod_pt_id = plansptd_pt_id " _
                & "     LEFT JOIN  " _
                & "     (SELECT pod_pt_id, sum(COALESCE(pod_qty_return, 0)) as pod_return " _
                & "       FROM po_mstr " _
                & "            INNER JOIN public.pod_det ON (public.po_mstr.po_oid = public.pod_det.pod_po_oid) " _
                & "       WHERE public.po_mstr.po_close_date IS NULL " _
                & "       GROUP BY pod_pt_id) tpod_return ON tpod_return.pod_pt_id = plansptd_pt_id " _
                & "     LEFT JOIN  " _
                & "     (SELECT invc_pt_id, sum(COALESCE(invc_qty, 0)) as invc_qty " _
                & "       FROM invc_mstr " _
                & "       WHERE public.invc_mstr.invc_loc_id = '301' " _
                & "       GROUP BY invc_pt_id) tinvc_qty ON tinvc_qty.invc_pt_id = plansptd_pt_id " _
                & "     LEFT OUTER JOIN  " _
                & "     (SELECT sod_pt_id, sum(COALESCE(sod_qty_shipment, 0)) as total_sod " _
                & "       FROM so_mstr " _
                & "            INNER JOIN public.sod_det ON (public.so_mstr.so_oid = public.sod_det.sod_so_oid) " _
                & "       WHERE public.so_mstr.so_close_date IS NOT NULL AND " _
                & "             public.so_mstr.so_close_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & "             and public.so_mstr.so_close_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & "       GROUP BY sod_pt_id) tsod_total ON tsod_total.sod_pt_id = plansptd_pt_id " _
                & "WHERE public.plans_mstr.plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                & " and public.plans_mstr.plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                & " and public.plans_mstr.plans_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & "GROUP BY public.plans_mstr.plans_en_id, " _
                & "         public.en_mstr.en_desc, " _
                & "         plansptd_pt_id, " _
                & "         public.pt_mstr.pt_code, " _
                & "         public.pt_mstr.pt_desc1, " _
                & "         tpod_total.total_pod, " _
                & "         tpod_return.pod_return, " _
                & "         tinvc_qty.invc_qty, " _
                & "         tpod_recieve.pod_recieve, " _
                & "         tsod_total.total_sod"


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
