Imports npgsql
Imports master_new.ModFunction

Public Class FVoucherBalanceReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FDRCRMemo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        'pr_txttglawal.DateTime = _now
        'pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total Voucher", "voucher", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Closed Status", "closed_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total Payable", "payable_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Payment", "payment_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Outstanding", "outstanding_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")

        add_column(gv_detail_po, "ap_ptnr_id", False)
        add_column_copy(gv_detail_po, "Voucher Number", "ap_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_po, "Effective Date", "ap_eff_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_po, "Due Date", "ap_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_po, "Amount", "ap_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_po, "Pay Amount", "ap_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_po, "Outstanding Amount", "outstanding_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_po, "Remarks", "ap_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_po, "Status", "ap_status", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT DISTINCT  " _
                & "  public.ap_mstr.ap_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ap_mstr.ap_ptnr_id, " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  count(public.ap_mstr.ap_ptnr_id) AS voucher, " _
                & "  count(public.ap_mstr.ap_status) AS closed_status, " _
                & "  SUM(public.ap_mstr.ap_amount) AS payable_total, " _
                & "  SUM(public.ap_mstr.ap_pay_amount) AS payment_total, " _
                & "  SUM(public.ap_mstr.ap_amount) - SUM(public.ap_mstr.ap_pay_amount) AS outstanding_total " _
                & "FROM " _
                & "  public.ap_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                & "  INNER JOIN public.en_mstr ON (public.ap_mstr.ap_en_id = public.en_mstr.en_id) " _
                & "WHERE " _
                & " ap_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & "GROUP BY " _
                & "  public.ap_mstr.ap_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.ap_mstr.ap_ptnr_id, " _
                & "  public.ptnr_mstr.ptnr_name " _
                & "ORDER BY " _
                & "  public.ptnr_mstr.ptnr_name"
        Return get_sequel
    End Function

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                ds.Tables("detail_po").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  public.ptnr_mstr.ptnr_name, " _
                & "  public.ap_mstr.ap_code, " _
                & "  public.ap_mstr.ap_eff_date, " _
                & "  public.ap_mstr.ap_due_date, " _
                & "  public.ap_mstr.ap_amount, " _
                & "  public.ap_mstr.ap_pay_amount, " _
                & "  public.ap_mstr.ap_amount - public.ap_mstr.ap_pay_amount AS outstanding_amount, " _
                & "  public.ap_mstr.ap_remarks, " _
                & "  public.ap_mstr.ap_status, " _
                & "  public.ap_mstr.ap_invoice, " _
                & "  public.ap_mstr.ap_ac_prepaid, " _
                & "  public.ap_mstr.ap_pay_prepaid, " _
                & "  public.ap_mstr.ap_ptnr_id " _
                & "FROM " _
                & "  public.ap_mstr " _
                & "  INNER JOIN public.ptnr_mstr ON (public.ap_mstr.ap_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                & " where public.ap_mstr.ap_ptnr_id='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_ptnr_id").ToString & "'" _
                & "ORDER BY " _
                & "  public.ap_mstr.ap_code ASC"
            '& "  where ar_mstr.ar_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            '& "  and ar_mstr.ar_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

            load_data_detail(sql, gc_detail_po, "detail_po")
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
End Class
