Imports npgsql
Imports master_new.ModFunction

Public Class FCashReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        format_grid()

    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "SO Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Referensi Po No.", "so_ref_po_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sold To", "ptnr_name_sold", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Shipment Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Shipment Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Qty", "sod_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Qty Shipment", "sod_qty_shipment", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_master, "Qty Return", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_master, "Balance", "public.sod_det.sod_qty_shipment - public.soshipd_det.soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "UM", "soshipd_um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "UM Conversion", "soshipd_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        ''add_column_copy(gv_master, "Qty Real", "soshipd_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_master, "Serial Number", "soshipds_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        ''add_column_copy(gv_master, "Reason Code", "rea_code_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Partner ID", "casho_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "casho_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "casho_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "casho_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Refference", "casho_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "casho_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Remains", "casho_amount_remains", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Realization", "casho_amount_realization", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Reverse", "casho_amount_reverse", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "casho_exc_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Reverse", "casho_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "casho_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "casho_exc_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "casho_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reff", "casho_reff_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Close", "casho_close", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Temp", "casho_close_temp", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Date Create", "casho_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "casho_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "casho_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_master, "cashod_oid", False)
        add_column(gv_master, "cashod_casho_oid", False)
        add_column(gv_master, "cashod_ac_id", False)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "cashod_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Remarks", "cashod_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "cashod_cc_id", False)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master_ci, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Bank Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Trans Code", "cashi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Date", "cashi_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master_ci, "Remarks", "cashi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Refference", "cashi_reff", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master_ci, "Reverse Status", "cashi_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Close Status", "cashi_close_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Cash Out Number", "cashi_reff_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master_ci, "Amount", "cashi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master_ci, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Amount Used", "cashi_amount_used", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master_ci, "Amount Remains", "cashi_amount_remains", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master_ci, "AR Payment", "cashi_ar_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master_ci, "Balance", "cashi_balance", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master_ci, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Exchange Rate", "cashi_exc_rate", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Is Reverse", "cashi_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "User Create", "cashi_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Date Create", "cashi_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master_ci, "User Update", "cashi_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Date Update", "cashi_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_master_ci, "cashid_oid", False)
        add_column(gv_master_ci, "cashid_cashi_oid", False)
        add_column(gv_master_ci, "cashid_ac_id", False)
        add_column_copy(gv_master_ci, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ci, "Amount", "cashid_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master_ci, "Remarks", "cashid_remarks", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master_ct, "Entity From", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Code", "casht_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Date", "casht_date", DevExpress.Utils.HorzAlignment.Default)
        'casht_date
        add_column_copy(gv_master_ct, "Bank from", "bk_name_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Bank to", "bk_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Exchange Rate", "casht_exch_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master_ct, "Remarks", "casht_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Reff", "casht_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Amount", "casht_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column_copy(gv_master_ct, "User Create", "casht_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Date Create", "casht_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master_ct, "User Update", "casht_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master_ct, "Date Update", "casht_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            get_sequel = "SELECT  " _
               & "  a.casho_oid, " _
               & "  a.casho_dom_id, " _
               & "  a.casho_en_id,b.en_code, " _
               & "  b.en_desc, " _
               & "  a.casho_add_by, " _
               & "  a.casho_add_date, " _
               & "  a.casho_upd_by, " _
               & "  a.casho_upd_date, " _
               & "  a.casho_bk_id,casho_ptnr_id, " _
               & "  c.bk_name, " _
               & "  d.ptnr_name, " _
               & "  a.casho_code, " _
               & "  a.casho_date, " _
               & "  a.casho_remarks, " _
               & "  a.casho_reff, " _
               & "  a.casho_amount, " _
               & "  a.casho_cu_id, " _
               & "  e.cu_name,casho_type,casho_reff_code,casho_reff_oid,casho_amount_remains,casho_amount_realization,casho_amount_reverse,casho_close,casho_close_temp, " _
               & "  a.casho_exc_rate, coalesce(casho_is_reverse,'N') as casho_is_reverse ," _
                & "  f.cashod_oid, " _
                & "  f.cashod_casho_oid, " _
                & "  f.cashod_ac_id, " _
                & "  g.ac_code, " _
                & "  g.ac_name, " _
                & "  f.cashod_amount, " _
                & "  f.cashod_remarks,f.cashod_cc_id, h.cc_desc " _
               & "FROM " _
               & "  public.casho_out a " _
               & "  INNER JOIN public.en_mstr b ON (a.casho_en_id = b.en_id) " _
               & "  INNER JOIN public.bk_mstr c ON (a.casho_bk_id = c.bk_id) " _
               & "  INNER JOIN public.ptnr_mstr d ON (a.casho_ptnr_id = d.ptnr_id) " _
               & "  INNER JOIN public.cu_mstr e ON (a.casho_cu_id = e.cu_id) " _
               & "  INNER JOIN public.cashod_detail f ON (a.casho_oid = f.cashod_casho_oid) " _
                & "  INNER JOIN public.ac_mstr g ON (f.cashod_ac_id = g.ac_id) " _
                & "  left outer JOIN public.cc_mstr h ON (f.cashod_cc_id = h.cc_id) " _
               & "  Where a.casho_en_id in (select user_en_id from tconfuserentity " _
               & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
               & " and casho_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
               & " ORDER BY casho_code"

        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            get_sequel = "SELECT  " _
               & "  a.cashi_oid, " _
               & "  a.cashi_dom_id, " _
               & "  a.cashi_en_id, " _
               & "  b.en_desc,b.en_code,coalesce(cashi_close_temp,'N') as cashi_close_temp , " _
               & "  a.cashi_add_by, " _
               & "  a.cashi_add_date, " _
               & "  a.cashi_upd_by, " _
               & "  a.cashi_upd_date, " _
               & "  a.cashi_bk_id, " _
               & "  c.bk_name, " _
               & "  d.ptnr_name, " _
               & "  a.cashi_code, " _
               & "  a.cashi_date, " _
               & "  a.cashi_remarks, " _
               & "  a.cashi_reff, " _
               & "  a.cashi_amount, " _
               & "  a.cashi_cu_id,cashi_reverse, cashi_reff_oid, cashi_reff_code, " _
               & "  e.cu_name,cashi_so_oid, cashi_amount_used,so_code, cashi_amount_remains, " _
               & "  a.cashi_exc_rate,coalesce(cashi_is_reverse,'N') as cashi_is_reverse , " _
                & "  g.cashid_oid, " _
                & "  g.cashid_cashi_oid, " _
                & "  g.cashid_ac_id, " _
                & "  h.ac_code, " _
                & "  h.ac_name, " _
                & "  g.cashid_amount, " _
                & "  g.cashid_remarks " _
               & "FROM " _
               & "  public.cashi_in a " _
               & "  INNER JOIN public.en_mstr b ON (a.cashi_en_id = b.en_id) " _
               & "  INNER JOIN public.bk_mstr c ON (a.cashi_bk_id = c.bk_id) " _
               & "  LEFT OUTER JOIN public.ptnr_mstr d ON (a.cashi_ptnr_id = d.ptnr_id) " _
               & "  INNER JOIN public.cu_mstr e ON (a.cashi_cu_id = e.cu_id) " _
                & "  LEFT OUTER JOIN public.so_mstr f ON (a.cashi_so_oid = f.so_oid) " _
                  & " INNER JOIN public.cashid_detail g on (a.cashi_oid = g.cashid_cashi_oid)" _
                & "  INNER JOIN public.ac_mstr h ON (g.cashid_ac_id = h.ac_id) " _
               & "  Where a.cashi_en_id in (select user_en_id from tconfuserentity " _
               & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
               & " and cashi_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
               & " ORDER BY cashi_code"
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then

            get_sequel = "SELECT  " _
               & "  a.casht_oid, " _
               & "  a.casht_en,casht_en_to, " _
               & "  b.en_desc, " _
               & "  a.casht_bk_id_from, " _
               & "  c.bk_name AS bk_name_from, " _
               & "  a.casht_cu_id, " _
               & "  e.cu_name, " _
               & "  a.casht_exch_rate, " _
               & "  a.casht_remarks, " _
               & "  a.casht_bk_id_to, " _
               & "  d.bk_name AS bk_name_to,f.en_desc as en_desc_to, " _
               & "  a.casht_reff, " _
               & "  a.casht_amount, " _
               & "  a.casht_code, " _
               & "  a.casht_date, " _
               & "  a.casht_add_by, " _
               & "  a.casht_add_date, " _
               & "  a.casht_upd_by, " _
               & "  a.casht_upd_date " _
               & "FROM " _
               & "  public.casht_transfer a " _
               & "  INNER JOIN public.en_mstr b ON (a.casht_en = b.en_id) " _
               & "  INNER JOIN public.bk_mstr c ON (a.casht_bk_id_from = c.bk_id) " _
               & "  INNER JOIN public.bk_mstr d ON (a.casht_bk_id_to = d.bk_id) " _
               & "  INNER JOIN public.cu_mstr e ON (a.casht_cu_id = e.cu_id) " _
               & "  LEFT OUTER JOIN public.en_mstr f ON (a.casht_en_to = f.en_id) " _
               & " where casht_en in (select user_en_id from tconfuserentity " _
               & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
               & " and casht_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
               & "  order by casht_code"
        End If


      

        Return get_sequel
    End Function

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        load_data_many(par)
    End Sub
    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If XtraTabControl1.SelectedTabPageIndex = 0 Then

                            .SQL = get_sequel()

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")

                            gc_master.DataSource = ds.Tables("view1")
                            gv_master.BestFitColumns()
                        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
                            .SQL = get_sequel()

                            .InitializeCommand()
                            .FillDataSet(ds, "view2")

                            gc_master_ci.DataSource = ds.Tables("view2")
                            gv_master_ci.BestFitColumns()
                        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
                            .SQL = get_sequel()

                            .InitializeCommand()
                            .FillDataSet(ds, "view3")

                            gc_master_ct.DataSource = ds.Tables("view3")
                            gv_master_ct.BestFitColumns()
                        End If

                        'bestfit_column()
                        'load_data_grid_detail()
                        'ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

End Class
