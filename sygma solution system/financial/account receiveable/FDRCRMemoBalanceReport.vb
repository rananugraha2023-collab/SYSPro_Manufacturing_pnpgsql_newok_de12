Imports npgsql
Imports master_new.ModFunction

Public Class FDRCRMemoBalanceReport
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
        add_column_copy(gv_master, "Total Orders", "total_orders", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Closed Status", "closed_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Total Receivables", "total_receivables", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Total Payment", "total_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Outstanding Payment", "outstanding_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Credit Limit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_detail_memo, "ar_bill_to", False)
        add_column_copy(gv_detail_memo, "Invoice Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_memo, "SO Number", "arso_so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_memo, "SO Date", "arso_so_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_memo, "Effective Date", "ar_eff_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_memo, "Due Date", "ar_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail_memo, "Total", "ar_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_memo, "Total Payment", "ar_pay_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_memo, "Outstanding Payment", "ar_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail_memo, "Status", "ar_status", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT DISTINCT  " _
                    & "  public.ar_mstr.ar_bill_to, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  COUNT(public.ar_mstr.ar_code) AS total_orders, " _
                    & "  COUNT(public.ar_mstr.ar_status) AS closed_status, " _
                    & "  sum(public.ar_mstr.ar_amount) AS total_receivables, " _
                    & "  sum(public.ar_mstr.ar_pay_amount) AS total_payment, sum(public.ar_mstr.ar_amount) - sum(public.ar_mstr.ar_pay_amount) AS outstanding_payment, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.ptnr_mstr.ptnr_limit_credit " _
                    & "FROM " _
                    & "  public.ar_mstr " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.ar_mstr.ar_bill_to = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.ar_mstr.ar_en_id = public.en_mstr.en_id) " _
                    & "  AND (public.ptnr_mstr.ptnr_en_id = public.en_mstr.en_id) " _
                    & " where " _
                    & " ar_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "GROUP BY " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.ar_mstr.ar_bill_to, " _
                    & "  public.ptnr_mstr.ptnr_limit_credit " _
                    & " ORDER BY en_desc,ptnr_name"

        Return get_sequel
    End Function

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String

            Try
                ds.Tables("detail_memo").Clear()
            Catch ex As Exception
            End Try


            sql = "SELECT " _
                & "  b.ar_bill_to, " _
                & "  b.ar_code, " _
                & "  b.ar_en_id, " _
                & "  a.ptnr_name, " _
                & "  b.ar_date, " _
                & "  b.ar_eff_date, " _
                & "  b.ar_amount, " _
                & "  b.ar_pay_amount, " _
                & "  b.ar_amount - b.ar_pay_amount AS ar_outstanding, " _
                & "  b.ar_due_date, " _
                & "  b.ar_status,c.arso_so_code,c.arso_so_date " _
                & "  FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.ar_mstr b ON (a.ptnr_id = b.ar_bill_to) " _
                & " INNER JOIN arso_so c ON ( b.ar_oid=c.arso_ar_oid ) " _
                & "  where b.ar_bill_to='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ar_bill_to").ToString & "' " _
                & "ORDER BY " _
                & "  b.ar_code "

            load_data_detail(sql, gc_detail_memo, "detail_memo")
        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub
End Class