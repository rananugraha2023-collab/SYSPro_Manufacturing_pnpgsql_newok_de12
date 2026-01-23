Imports master_new.ModFunction

Public Class FTransferReceiptPivot
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FARReportByAging_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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



        add_column_pivot(pgc_ar, "Entity", "en_desc_to", DevExpress.XtraPivotGrid.PivotArea.RowArea)
        add_column_pivot(pgc_ar, "Shipment Year", "ptsfr_receive_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear, "soship_date_year")
        add_column_pivot(pgc_ar, "Shipment Month", "ptsfr_receive_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth, "soship_date_month")
        add_column_pivot(pgc_ar, "Shipment Date", "ptsfr_receive_date", DevExpress.XtraPivotGrid.PivotArea.FilterArea, DevExpress.XtraPivotGrid.PivotGroupInterval.Date, "soship_date_date")

        add_column_pivot(pgc_ar, "Part Number", "pt_code", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Description1", "pt_desc1", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        add_column_pivot(pgc_ar, "Description2", "pt_desc2", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "End User", "pbd_end_user", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
        'add_column_pivot(pgc_ar, "Qty", "pbd_qty", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_pivot(pgc_ar, "Qty Receipt", "ptsfrd_qty_receive", DevExpress.XtraPivotGrid.PivotArea.DataArea, DevExpress.Utils.FormatType.Numeric, "n")
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

        set_sql = "SELECT  " _
                    & "  public.pb2_mstr.pb_upd_date, " _
                    & "  public.pb2_mstr.pb_upd_by, " _
                    & "  public.pb2_mstr.pb_add_date, " _
                    & "  public.pb2_mstr.pb_add_by, " _
                    & "  public.pb2_mstr.pb_code, " _
                    & "  public.pb2_mstr.pb_date, " _
                    & "  public.pb2_mstr.pb_due_date, " _
                    & "  public.pb2_mstr.pb_requested, " _
                    & "  public.pb2_mstr.pb_end_user, " _
                    & "  public.pb2_mstr.pb_rmks, " _
                    & "  public.pb2_mstr.pb_status, " _
                    & "  public.pb2_mstr.pb_tran_id, " _
                    & "  public.pb2_mstr.pb_trans_id, " _
                    & "  public.pb2_mstr.pb_close_date, " _
                    & "  public.pb2_mstr.pb_dt, " _
                    & "  public.pb2d_det.pbd_oid, " _
                    & "  public.pb2d_det.pbd_dom_id, " _
                    & "  public.pb2d_det.pbd_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pb2d_det.pbd_si_id, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.pb2d_det.pbd_add_by, " _
                    & "  public.pb2d_det.pbd_add_date, " _
                    & "  public.pb2d_det.pbd_upd_by, " _
                    & "  public.pb2d_det.pbd_upd_date, " _
                    & "  public.pb2d_det.pbd_pb_oid, " _
                    & "  public.pb2d_det.pbd_seq, " _
                    & "  public.pb2d_det.pbd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.pb2d_det.pbd_rmks, " _
                    & "  public.pb2d_det.pbd_end_user, " _
                    & "  public.pb2d_det.pbd_due_date, " _
                    & "  public.pb2d_det.pbd_qty, " _
                    & "  public.pb2d_det.pbd_qty_processed,public.pb2d_det.pbd_qty- coalesce(public.pb2d_det.pbd_qty_processed,0) as  pbd_qty_outstanding, " _
                    & "  public.pb2d_det.pbd_qty_completed, " _
                    & "  public.pb2d_det.pbd_qty_riud, " _
                    & "  public.pb2d_det.pbd_um, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.pb2d_det.pbd_status, " _
                    & "  public.pb2d_det.pbd_dt,pb_pbt_code,pbt_desc " _
                    & "  FROM " _
                    & "  public.pb2d_det " _
                    & "  LEFT OUTER JOIN public.si_mstr ON (public.pb2d_det.pbd_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.pb2d_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.pb2d_det.pbd_um = public.code_mstr.code_id) " _
                    & "  INNER JOIN public.pb2_mstr ON (public.pb2_mstr.pb_oid = public.pb2d_det.pbd_pb_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.pb2_mstr.pb_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.pbt_type ON (public.pb2_mstr.pb_pbt_code = public.pbt_type.pbt_code) " _
                    & "  where  " _
                    & "  pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " and pb_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") "

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
