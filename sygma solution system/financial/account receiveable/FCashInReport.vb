Imports master_new.ModFunction

Public Class FCashInReport
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FCashInReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        grid_detail()
    End Sub

    Private Sub grid_detail()
        add_column_copy(gv_detail_ar, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Effective Date", "arpay_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_ar, "Bank Code", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Bank Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Payment Code", "arpay_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "AR Code", "arpayd_ar_ref", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Remarks", "arpayd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Amount", "arpayd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_ar, "Exchange Rate", "arpayd_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_ar, "Total IDR", "arpayd_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_ar, "User Create", "arpay_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Date Create", "arpay_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail_ar, "User Update", "arpay_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_ar, "Date Update", "arpay_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_detail_so_cash, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_so_cash, "Bank Code", "bk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Bank Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Payment Code", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Amount", "so_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_so_cash, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_so_cash, "Total IDR", "so_amount_idr", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_so_cash, "User Create", "so_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Date Create", "so_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_detail_so_cash, "User Update", "so_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_so_cash, "Date Update", "so_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
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
                            load_detail_ar(objload)
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            load_detail_so_cash(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 2 Then
                            '    load_sales_person(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 3 Then
                            '    load_sales_by_so(objload)
                            'ElseIf xtc_master.SelectedTabPageIndex = 4 Then
                            '    load_cash_credit(objload)
                        End If
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub load_detail_ar(ByVal par_obj As Object)
        With par_obj
            .SQL = "SELECT  " _
                    & "  public.arpay_payment.arpay_oid, " _
                    & "  public.arpay_payment.arpay_dom_id, " _
                    & "  public.arpay_payment.arpay_en_id, " _
                    & "  public.arpay_payment.arpay_add_by, " _
                    & "  public.arpay_payment.arpay_add_date, " _
                    & "  public.arpay_payment.arpay_upd_by, " _
                    & "  public.arpay_payment.arpay_upd_date, " _
                    & "  public.arpay_payment.arpay_code, " _
                    & "  public.arpay_payment.arpay_bill_to, " _
                    & "  public.arpay_payment.arpay_cu_id, " _
                    & "  public.arpay_payment.arpay_bk_id, " _
                    & "  public.arpay_payment.arpay_date, " _
                    & "  public.arpay_payment.arpay_eff_date, " _
                    & "  public.arpay_payment.arpay_total_amount, " _
                    & "  public.arpay_payment.arpay_remarks, " _
                    & "  public.arpay_payment.arpay_dt, " _
                    & "  public.arpayd_det.arpayd_oid, " _
                    & "  public.arpayd_det.arpayd_arpay_oid, " _
                    & "  public.arpayd_det.arpayd_ar_oid, " _
                    & "  public.arpayd_det.arpayd_ar_ref, " _
                    & "  public.arpayd_det.arpayd_type, " _
                    & "  public.arpayd_det.arpayd_amount, " _
                    & "  public.arpayd_det.arpayd_remarks, " _
                    & "  public.arpayd_det.arpayd_exc_rate, " _
                    & "  public.arpayd_det.arpayd_sokp_oid, " _
                    & "  public.arpayd_det.arpayd_exc_rate * public.arpayd_det.arpayd_amount as arpayd_amount_idr, " _
                    & "  en_desc, " _
                    & "  ptnr_name, " _
                    & "  cu_name, " _
                    & "  bk_code, bk_name " _
                    & "FROM " _
                    & "  public.arpay_payment " _
                    & "  INNER JOIN public.arpayd_det ON (public.arpay_payment.arpay_oid = public.arpayd_det.arpayd_arpay_oid) " _
                    & "  inner join en_mstr on en_id = arpay_en_id " _
                    & "  inner join ptnr_mstr on ptnr_id = arpay_bill_to " _
                    & "  inner join cu_mstr on cu_id = arpay_cu_id " _
                    & "  inner join bk_mstr on bk_id = arpay_bk_id" _
                    & " where arpay_eff_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and arpay_eff_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and arpay_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            .InitializeCommand()
            .FillDataSet(ds, "detail_ar")
            gc_detail_ar.DataSource = ds.Tables("detail_ar")
            gv_detail_ar.BestFitColumns()
        End With
    End Sub

    Private Sub load_detail_so_cash(ByVal par_obj As Object)
        With par_obj
            .SQL = "SELECT  " _
                    & "  public.so_mstr.so_oid, " _
                    & "  public.so_mstr.so_dom_id, " _
                    & "  public.so_mstr.so_en_id, " _
                    & "  public.so_mstr.so_add_by, " _
                    & "  public.so_mstr.so_add_date, " _
                    & "  public.so_mstr.so_upd_by, " _
                    & "  public.so_mstr.so_upd_date, " _
                    & "  public.so_mstr.so_code, " _
                    & "  public.so_mstr.so_ptnr_id_sold, " _
                    & "  public.so_mstr.so_ptnr_id_bill, " _
                    & "  public.so_mstr.so_date, " _
                    & "  public.so_mstr.so_credit_term, " _
                    & "  public.so_mstr.so_type, " _
                    & "  public.so_mstr.so_total, " _
                    & "  public.so_mstr.so_exc_rate, " _
                    & "  public.so_mstr.so_cu_id, " _
                    & "  public.so_mstr.so_total_ppn, " _
                    & "  public.so_mstr.so_total_pph, " _
                    & "  public.so_mstr.so_bk_id, " _
                    & "  so_total + so_total_ppn + so_total_pph as so_amount, " _
                    & "  (so_total + so_total_ppn + so_total_pph) * so_exc_rate as so_amount_idr, " _
                    & "  en_desc, " _
                    & "  cu_name, " _
                    & "  ptnr_name, " _
                    & "  bk_code, " _
                    & "  bk_name " _
                    & "FROM " _
                    & "  public.so_mstr " _
                    & "  inner join en_mstr on en_id = so_en_id " _
                    & "  inner join code_mstr pay_type on pay_type.code_id = so_pay_type " _
                    & "  inner join cu_mstr on cu_id = so_cu_id " _
                    & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                    & "  left outer join bk_mstr on bk_id = so_bk_id " _
                    & "  where pay_type.code_usr_1 = '0' " _
                    & " and so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and so_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            .InitializeCommand()
            .FillDataSet(ds, "detail_so_cash")
            gc_detail_so_cash.DataSource = ds.Tables("detail_so_cash")
            gv_detail_so_cash.BestFitColumns()
        End With
    End Sub
End Class
