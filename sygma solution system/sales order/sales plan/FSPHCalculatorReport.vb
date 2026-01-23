Imports npgsql
Imports master_new.ModFunction

Public Class FSPHCalculatorReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSPHCalculatorReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Sales Person", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Partner Name", "hppr_nama_costumer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Type", "hppr_type_hpp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Partnumber", "hppr_nama_produk", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Margin", "hppr_margin", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Insentif", "hppr_insentif_sales", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Reward", "hppr_reward_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Fee", "hppr_fee_mitra", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Price", "hppr_hasil_satuan", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Total Price", "hppr_hasil_total_harga", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Print Dat", "", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "SPH Code", "sph_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Plans Code", "plans_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Periode", "plans_periode", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column_copy(gv_view1, "Visite Date", "visite_startdate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "Type", "visite_type", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_view1, "Visite Date", "visite_enddate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "Start", "visited_check_in", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "Chekin", "visited_address_gps_check_in", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "End", "visited_check_out", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "Chek Out", "visited_address_gps_check_out", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Duration", "duration", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Output", "visite_output", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Result", "visited_Result", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view2, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Year", "plans_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "Y")
        'add_column_copy(gv_view2, "Month", "plans_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "M")
        'add_column_copy(gv_view2, "Periode", "plans_periode", DevExpress.Utils.HorzAlignment.Default)
        'add_column_pivot(pgc_ar, "Shipment Date", "pb_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")

        'add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_copy(gv_view2, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_pivot(pgc_ar, "End User", "pbd_end_user", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Stock", "plansptd_amount", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_pivot(pgc_ar, "Qty Process", "pbd_qty_processed", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_pivot(pgc_ar, "Qty Outstanding", "pbd_qty_outstanding", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_view2, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Visit Code", "visite_code", DevExpress.Utils.HorzAlignment.Default)


        'add_column_copy(gv_view2, "Sales Person", "ptnr_name_sales", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Partner Name", "ptnr_mstr_cust", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Visit is Planned", "visite_is_planned", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Plans Code", "plans_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Periode", "plans_periode", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column_copy(gv_view2, "Visite Date", "visite_startdate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "Type", "visite_type", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_view1, "Visite Date", "visite_enddate", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "Start", "visited_check_in", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "Chekin", "visited_address_gps_check_in", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "End", "visited_check_out", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "Chek Out", "visited_address_gps_check_out", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Duration", "duration", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Output", "visite_output", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Result", "visited_Result", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Planned", "visite_is_planned", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Planned", "plans_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Qty", "plansd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty Processed", "opnamed_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Closed", "opname_close", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Processed", "opname_process", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "User Create", "opnamed_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Date Create", "opnamed_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_view1, "reqd_req_oid", False)
        'add_column_copy(gv_view1, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Remarks Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "End User Detail", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Need Date Detail", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Due Date Detail", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view2, "Entity Header", "en_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "PR Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Order Date Header", "req_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Need Date Header", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Due Date Header", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Remarks Header", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Site Header", "si_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Project Header", "pjc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_view2, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_view2, "reqd_req_oid", False)
        'add_column_copy(gv_view2, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Remarks Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "End User Detail", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Outstanding", "reqd_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view2, "Need Date Detail", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Due Date Detail", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)
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
                            .SQL = "SELECT  " _
                                    & "  public.hppr_mstr.hppr_en_id, " _
                                    & "  public.en_mstr.en_desc, " _
                                    & "  public.hppr_mstr.hppr_sales_id, " _
                                    & "  public.ptnr_mstr.ptnr_name, " _
                                    & "  public.hppr_mstr.hppr_nama_costumer, " _
                                    & "  public.hppr_mstr.hppr_type_hpp, " _
                                    & "  public.hppr_mstr.hppr_nama_produk, " _
                                    & "  public.hppr_mstr.hppr_qty, " _
                                    & "  public.hppr_mstr.hppr_margin, " _
                                    & "  public.hppr_mstr.hppr_insentif_sales, " _
                                    & "  public.hppr_mstr.hppr_reward_mitra, " _
                                    & "  public.hppr_mstr.hppr_fee_mitra, " _
                                    & "  public.hppr_mstr.hppr_hasil_satuan, " _
                                    & "  public.hppr_mstr.hppr_hasil_total_harga " _
                                    & "FROM " _
                                    & "  public.hppr_mstr " _
                                    & "  INNER JOIN public.en_mstr ON (public.hppr_mstr.hppr_en_id = public.en_mstr.en_id) " _
                                    & "  INNER JOIN public.ptnr_mstr ON (public.hppr_mstr.hppr_sales_id = public.ptnr_mstr.ptnr_id)"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")

                            '    ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            '        .SQL = "SELECT  " _
                            '& "  public.plans_mstr.plans_en_id, " _
                            '& "  public.en_mstr.en_desc, " _
                            '& "  public.plans_mstr.plans_periode, " _
                            '& "  public.plansptd_det.plansptd_pt_id, " _
                            '& "  public.pt_mstr.pt_desc1, " _
                            '& "  public.pt_mstr.pt_desc2, " _
                            '& "  public.pt_mstr.pt_code, " _
                            '& "  public.plansptd_det.plansptd_amount, " _
                            '& "  public.plans_mstr.plans_date, " _
                            '& "  public.plans_mstr.plans_oid, " _
                            '& "  public.plans_mstr.plans_code, " _
                            '& "  public.plans_mstr.plans_sales_id, " _
                            '& "  public.plans_mstr.plans_add_by, " _
                            '& "  public.plans_mstr.plans_add_date, " _
                            '& "  public.plans_mstr.plans_upd_by, " _
                            '& "  public.plans_mstr.plans_upd_date, " _
                            '& "  public.plans_mstr.plans_amount_total, " _
                            '& "  public.plans_mstr.plans_dom_id, " _
                            '& "  public.plans_mstr.plans_remarks, " _
                            '& "  public.plansptd_det.plansptd_oid, " _
                            '& "  public.plansptd_det.plansptd_plans_oid, " _
                            '& "  public.plansptd_det.plansptd_qty " _
                            '& "FROM " _
                            '& "  public.plans_mstr " _
                            '& "  INNER JOIN public.plansptd_det ON (public.plans_mstr.plans_oid = public.plansptd_det.plansptd_plans_oid) " _
                            '& "  INNER JOIN public.en_mstr ON (public.plans_mstr.plans_en_id = public.en_mstr.en_id) " _
                            '& "  INNER JOIN public.pt_mstr ON (public.plansptd_det.plansptd_pt_id = public.pt_mstr.pt_id)" _
                            '& "  where  " _
                            '& "  plans_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                            '& " and plans_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                            '& " and plans_en_id in (select user_en_id from tconfuserentity " _
                            '& " where userid = " + master_new.ClsVar.sUserID.ToString + ") "
                            '        .InitializeCommand()
                            '        .FillDataSet(ds, "view2")
                            '        gc_view2.DataSource = ds.Tables("view2")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub xtc_master_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xtc_master.TabIndexChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        End If
    End Sub

    Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
                              ByVal par_area As DevExpress.XtraPivotGrid.PivotArea)
        Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

        pvg.Fields.Add(pvg_field)
        pvg_field.Area = par_area
        pvg_field.FieldName = par_fn
        pvg_field.Caption = par_caption

    End Sub

    'Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
    '                           ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal par_group_interval As DevExpress.XtraPivotGrid.PivotGroupInterval, ByVal par_column_name As String)
    '    Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

    '    pvg.Fields.Add(pvg_field)
    '    pvg_field.Name = par_column_name
    '    pvg_field.Area = par_area
    '    pvg_field.FieldName = par_fn
    '    pvg_field.Caption = par_caption
    '    pvg_field.GroupInterval = par_group_interval


    'End Sub
    'Public Sub add_column_pivot(ByVal pvg As DevExpress.XtraPivotGrid.PivotGridControl, ByVal par_caption As String, ByVal par_fn As String, _
    '                           ByVal par_area As DevExpress.XtraPivotGrid.PivotArea, ByVal formatType As DevExpress.Utils.FormatType, ByVal formatString As String)
    '    Dim pvg_field As New DevExpress.XtraPivotGrid.PivotGridField

    '    pvg.Fields.Add(pvg_field)
    '    pvg_field.Area = par_area
    '    pvg_field.FieldName = par_fn
    '    pvg_field.Caption = par_caption
    '    pvg_field.CellFormat.FormatType = formatType
    '    pvg_field.CellFormat.FormatString = formatString
    '    pvg_field.GrandTotalCellFormat.FormatType = formatType
    '    pvg_field.GrandTotalCellFormat.FormatString = formatString
    '    pvg_field.ValueFormat.FormatType = formatType
    '    pvg_field.ValueFormat.FormatString = formatString
    '    pvg_field.TotalCellFormat.FormatType = formatType
    '    pvg_field.TotalCellFormat.FormatString = formatString
    '    pvg_field.TotalValueFormat.FormatType = formatType
    '    pvg_field.TotalValueFormat.FormatString = formatString

    'End Sub
End Class