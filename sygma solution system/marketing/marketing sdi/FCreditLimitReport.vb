Imports npgsql
Imports master_new.ModFunction

Public Class FCreditLimitReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderShipment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Customer Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Credit Limit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "AR Outstanding", "ar_outstanding", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  b.en_desc, " _
            & "  a.ptnr_code, " _
            & "  a.ptnr_name, " _
            & "  a.ptnr_limit_credit, " _
            & "  coalesce((SELECT sum(ar_amount - coalesce(ar_pay_amount, 0)) AS jml FROM ar_mstr WHERE coalesce(ar_status, 'N') = 'N' AND ar_bill_to = ptnr_id),0) AS ar_outstanding " _
            & "FROM " _
            & "  public.ptnr_mstr a " _
            & "  INNER JOIN public.en_mstr b ON (a.ptnr_en_id = b.en_id) " _
            & "WHERE " _
            & "  a.ptnr_is_cust = 'Y' AND a.ptnr_en_id IN (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
            & " And " _
            & "  a.ptnr_active = 'Y' "

        Return get_sequel
    End Function

End Class
