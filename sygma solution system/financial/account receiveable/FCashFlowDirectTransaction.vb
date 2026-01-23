Imports npgsql
Imports master_new.ModFunction

Public Class FCashFlowDirectTransaction
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
        init_le(le_gcal, "gcal_mstr")
    End Sub

    Public Overrides Sub format_grid()
        
        add_column_copy(gv_master, "GL Periode", "glt_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "GL Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "GL Code", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "GL Reff", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account ID", "glt_ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT   " _
                & "  a.glt_periode, " _
                & "  a.glt_code, " _
                & "  a.glt_date, " _
                & "  a.glt_ac_id, " _
                & "  b.ac_code_hirarki, " _
                & "  a.ac_code, " _
                & "  a.ac_name, " _
                & "  a.glt_desc, " _
                & "  a.glt_debit, " _
                & "  a.glt_credit,glt_ref_trans_code " _
                & "FROM " _
                & "  public.cf_save a " _
                & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
                & "WHERE " _
                & "  a.glt_periode = '" & le_gcal.EditValue.ToString & "' " _
                & "ORDER BY " _
                & "  a.glt_code, a.glt_date,glt_debit desc,glt_credit desc"

        If TxtAcCode.Text.Length > 0 Then
            get_sequel = "SELECT   " _
               & "  a.glt_periode, " _
               & "  a.glt_code, " _
               & "  a.glt_date, " _
               & "  a.glt_ac_id, " _
               & "  b.ac_code_hirarki, " _
               & "  a.ac_code, " _
               & "  a.ac_name, " _
               & "  a.glt_desc, " _
               & "  a.glt_debit, " _
               & "  a.glt_credit " _
               & "FROM " _
               & "  public.cf_save a " _
               & "  INNER JOIN public.ac_mstr b ON (a.glt_ac_id = b.ac_id) " _
               & "WHERE " _
               & "  a.glt_periode = '" & le_gcal.EditValue.ToString & "' and glt_code in (select distinct x.glt_code from cf_save x where x.glt_periode='" _
               & le_gcal.EditValue.ToString & "' and x.ac_code='" & TxtAcCode.Text & "') " _
               & "ORDER BY " _
               & "  a.glt_code, a.glt_date,glt_debit desc,glt_credit desc"
        End If

        Return get_sequel
    End Function

End Class
