Imports npgsql
Imports master_new.ModFunction

Public Class FSOARFPPerFADReport
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ds_detail As DataSet

    Private Sub FSOARFPPerFADReport(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        AddHandler gv_view1.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_view1.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "AY Date", "arpay_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "AY Number", "arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sold To", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales", "sales_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Price List", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Cost", "sod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "(%) Cost", "pct_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Total Sales", "sales_ttl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_view1, "Total Bruto ", "total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "(%) Gross Sales Contribution", "grss_sls_contr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Discount Value", "disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Price", "price_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Disc. Value", "disc_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Netto", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "(%) Netto Sales Contribution", "nett_sls_contr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Total Cost", "total_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "(%)Total Cost", "ttl_cost_nett", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Gross Profit", "gross_profit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "(%) Gross Profit", "netto_gross_profit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Reason Code", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Vat Paid", "ppn_bayar", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Vat Free", "ppn_bebas", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Sales Unit", "sod_sales_unit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Sales Unit Total", "sod_sales_unit_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            Dim _ptnr_id As Integer = 0
            Dim sSQL As String
            sSQL = "select coalesce(user_ptnr_id,0) as user_ptnr_id from tconfuser where userid=" & master_new.ClsVar.sUserID
            _ptnr_id = master_new.PGSqlConn.GetRowInfo(sSQL)(0)

            'If _ptnr_id = 0 Then
            '    Box("Sorry this account haven't Sales ID")
            '    Exit Sub
            'End If
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then

                            .SQL = "SELECT DISTINCT* " _
                                & "FROM ( " _
                                & "        SELECT si_desc, " _
                                & "         en_desc, " _
                                & "         so_date, " _
                                & "         so_code, " _
                                & "         soship_date, " _
                                & "         soship_code, " _
                                & "         ar_date, " _
                                & "         ar_code, " _
                                & "         ptnr_mstr.ptnr_code, " _
                                & "         ptnr_mstr.ptnr_name, " _
                                & "         sales_mstr.ptnr_code AS sales_code, " _
                                & "         sales_mstr.ptnr_name AS sales_name, " _
                                & "         pi_desc, " _
                                & "         pt_code, " _
                                & "         pt_desc1, " _
                                & "         pt_desc2, " _
                                & "         um_mstr.code_name AS um_name, " _
                                & "         loc_desc, " _
                                & "         soshipd_qty * - 1 AS soshipd_qty, " _
                                & "         sod_cost, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN (sod_cost / sod_price) " _
                                & "           ELSE 0 " _
                                & "         END AS pct_cost, " _
                                & "         soshipd_qty * - 1 * sod_price AS sales_ttl, " _
                                & "         soshipd_qty * - 1 * sod_price / NULLIF(SUM(soshipd_qty * - 1 * sod_price) OVER(), " _
                                & "           0) AS grss_sls_contr, " _
                                & "         sod_price, " _
                                & "         sod_disc, " _
                                & "         soshipd_qty * - 1 * sod_price * sod_disc AS disc_value, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price " _
                                & "           WHEN sod_tax_inc = 'Y' THEN soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "         END AS price_fp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) * sod_disc " _
                                & "         END AS disc_fp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "         END AS dpp, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "           WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "         END / NULLIF(SUM(CASE " _
                                & "                            WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "                            WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / " _
                                & "                              110.0 * sod_disc) " _
                                & "                          END) OVER(), " _
                                & "           0) AS nett_sls_contr, " _
                                & "         SUM(CASE " _
                                & "               WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc " _
                                & "               WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "             END) as JML, " _
                                & "         soshipd_qty * - 1 * sod_cost AS total_cost, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN soshipd_qty * - 1 * sod_cost / NULLIF(CASE " _
                                & "                                                                            WHEN sod_tax_inc = 'N' THEN soshipd_qty * - 1 * sod_price - soshipd_qty * " _
                                & "                                                                              - 1 * sod_price * sod_disc " _
                                & "                                                                            WHEN sod_tax_inc = 'Y' THEN (soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "                                                                              ) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc) " _
                                & "                                                                          END, " _
                                & "           0) " _
                                & "           ELSE 0 " _
                                & "         END AS ttl_cost_nett, " _
                                & "         CASE " _
                                & "           WHEN sod_tax_inc = 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) - soshipd_qty * - 1 * sod_cost " _
                                & "           WHEN sod_tax_inc = 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 / 110.0 * sod_disc)) - " _
                                & "             soshipd_qty * - 1 * sod_cost " _
                                & "         END AS gross_profit, " _
                                & "         CASE " _
                                & "           WHEN sod_price <> 0 THEN (CASE UPPER(sod_tax_inc) " _
                                & "                                       WHEN 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) - soshipd_qty * - 1 * " _
                                & "                                         sod_cost " _
                                & "                                       WHEN 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) - soshipd_qty * - 1 * sod_price * 100.0 / 110.0 " _
                                & "                                         * sod_disc) - soshipd_qty * - 1 * sod_cost " _
                                & "                                     END) / NULLIF(soshipd_qty * - 1 * sod_price, " _
                                & "           0) " _
                                & "           ELSE 0 " _
                                & "         END AS netto_gross_profit, " _
                                & "         sod_sales_unit, " _
                                & "         sod_sales_unit * - 1 * soshipd_qty AS sod_sales_unit_total, " _
                                & "         pl_desc, " _
                                & "         soship_is_shipment, " _
                                & "         soshipd_seq, " _
                                & "         cu_name, " _
                                & "         so_exc_rate, " _
                                & "         sod_taxable, " _
                                & "         sod_tax_inc, " _
                                & "         tax_mstr.code_name AS tax_name, " _
                                & "         CASE " _
                                & "           WHEN pl_code = '990000000001' THEN CASE sod_tax_inc " _
                                & "                                                WHEN 'N' THEN (soshipd_qty * - 1 * sod_price - soshipd_qty * - 1 * sod_price * sod_disc) * 0.1 " _
                                & "                                                WHEN 'Y' THEN ((soshipd_qty * - 1 * sod_price * 100.0 / 110.0) -(soshipd_qty * - 1 * sod_price * 100.0 " _
                                & "                                                  / 110.0 * sod_disc)) * 0.1 " _
                                & "                                              END " _
                                & "         END AS ppn_bayar, " _
                                & "         reason_mstr.code_name AS reason_name, " _
                                & "         ti_code, " _
                                & "         ptnrg_name, " _
                                & "         ti_date, " _
                                & "         pay_type.code_name AS pay_type_desc, " _
                                & "         CASE " _
                                & "           WHEN ptnr_mstr.ptnr_is_ps = 'Y' THEN 'PS' " _
                                & "           ELSE 'NON PS' " _
                                & "         END AS ps_status " _
                                & "       FROM public.soship_mstr " _
                                & "         INNER JOIN soshipd_det ON soshipd_soship_oid = soship_oid " _
                                & "         INNER JOIN so_mstr ON so_oid = soship_so_oid " _
                                & "         LEFT OUTER JOIN pi_mstr ON so_pi_id = pi_id " _
                                & "         INNER JOIN sod_det ON sod_oid = soshipd_sod_oid " _
                                & "         INNER JOIN ptnr_mstr sales_mstr ON sales_mstr.ptnr_id = so_sales_person " _
                                & "         INNER JOIN en_mstr ON en_id = soship_en_id " _
                                & "         INNER JOIN si_mstr ON si_id = soship_si_id " _
                                & "         INNER JOIN ptnr_mstr ON ptnr_mstr.ptnr_id = so_ptnr_id_sold " _
                                & "         INNER JOIN pt_mstr ON pt_id = sod_pt_id " _
                                & "         INNER JOIN code_mstr AS um_mstr ON um_mstr.code_id = soshipd_um " _
                                & "         INNER JOIN loc_mstr ON loc_id = soshipd_loc_id " _
                                & "         INNER JOIN cu_mstr ON cu_id = so_cu_id " _
                                & "         INNER JOIN code_mstr AS tax_mstr ON tax_mstr.code_id = sod_tax_class " _
                                & "         INNER JOIN code_mstr pay_type ON pay_type.code_id = so_pay_type " _
                                & "         LEFT OUTER JOIN code_mstr AS reason_mstr ON reason_mstr.code_id = soshipd_rea_code_id " _
                                & "         LEFT OUTER JOIN ars_ship ON ars_soshipd_oid = soshipd_oid " _
                                & "         LEFT OUTER JOIN ar_mstr ON ar_oid = ars_ar_oid " _
                                & "         LEFT OUTER JOIN tia_ar ON tia_ar_oid = ar_oid " _
                                & "         LEFT OUTER JOIN ti_mstr ON ti_oid = tia_ti_oid " _
                                & "         LEFT OUTER JOIN ptnrg_grp ON ptnrg_grp.ptnrg_id = ptnr_mstr.ptnr_ptnrg_id " _
                                & "         INNER JOIN pl_mstr ON pl_id = pt_pl_id " _
                                & "         LEFT OUTER JOIN public.arpayd_det ON (public.ars_ship.ars_ar_oid = public.arpayd_det.arpayd_ar_oid)  " _
                                & "         LEFT OUTER JOIN public.arpay_payment ON (public.arpayd_det.arpayd_arpay_oid = public.arpay_payment.arpay_oid)  " _
                                & "  where soship_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and soship_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and so_en_id in (select user_en_id from tconfuserentity " _
                                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                & "GROUP BY si_mstr.si_desc, " _
                                & "  en_desc, " _
                                & "  so_date, " _
                                & "  so_code, " _
                                & "  soship_date, " _
                                & "  soship_code, " _
                                & "  ar_date, " _
                                & "  ar_code, " _
                                & "  ptnr_mstr.ptnr_code, " _
                                & "  ptnr_mstr.ptnr_name, " _
                                & "  sales_mstr.ptnr_code, " _
                                & "  sales_mstr.ptnr_name, " _
                                & "  pi_desc, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  um_mstr.code_name, " _
                                & "  loc_desc, " _
                                & "  sod_cost, " _
                                & "  sod_price, " _
                                & "  sod_disc, " _
                                & "  sod_tax_inc, " _
                                & "  sod_sales_unit, " _
                                & "  pl_desc, " _
                                & "  soship_is_shipment, " _
                                & "  soshipd_seq, " _
                                & "  cu_name, " _
                                & "  so_exc_rate, " _
                                & "  sod_taxable, " _
                                & "  tax_mstr.code_name, " _
                                & "  reason_mstr.code_name, " _
                                & "  ti_code, " _
                                & "  ptnrg_name, " _
                                & "  ti_date, " _
                                & "  pay_type.code_name, " _
                                & "  ptnr_mstr.ptnr_is_ps, " _
                                & "  soshipd_qty, " _
                                & "  pl_code " _
                                & "  ) as shipment_data"

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")

                            gc_view1.DataSource = ds.Tables("view1")
                        End If

                        bestfit_column()
                        'load_data_grid_detail()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
