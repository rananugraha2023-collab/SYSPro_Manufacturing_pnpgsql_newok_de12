Imports npgsql
Imports master_new.ModFunction

Public Class FWOLaborfeedbackReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim _ar_oid_mstr As String
    Public ds_edit_so, ds_edit_shipment, ds_edit_dist, ds_edit_piutang, ds_sod_piutang As DataSet
    Dim _now As DateTime

    Private Sub FSalesOrderReturnReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "lbrf_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Order Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partnumber", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Conversion", "lbrf_qty_conversion", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Activity", "lbrfa_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Shift", "shift_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "lbrf_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Complete", "lbrf_qty_complete", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "lbrf_qty_reject", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Reason Reject Incoming", "qc_desc_in", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reason Reject Outgoing", "qc_desc_out", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Setup Start", "lbrf_start_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Setup Stop", "lbrf_stop_setup", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Setup Elapsed", "lbrf_elapsed_setup", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Run Start", "lbrf_start_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Run Stop", "lbrf_stop_run", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Run Elapsed", "lbrf_elapsed_run", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Run Start", "lbrf_down_start", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Run Stop", "lbrf_down_stop", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Run Elapsed", "lbrf_elapsed_down", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Down Time Reason", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "lbrf_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "lbrf_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "lbrf_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "lbrf_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "lbrf_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_master, "lbrfd_lbrfp_id", False)

        add_column_copy(gv_master, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Person Name (Old Data)", "lbrf_person", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.lbrf_oid,lbrf_code,lbrf_person, " _
                & "  a.lbrf_dom_id, " _
                & "  a.lbrf_en_id, " _
                & "  b.en_desc, " _
                & "  a.lbrf_wodr_uid, " _
                & "  d.wo_code,wodr_wc_id, " _
                & "  f.wc_desc, " _
                & "  e.code_name as reason_name, " _
                & "  j.code_name as shift_name, " _
                & "  a.lbrf_qty_complete, " _
                & "  a.lbrf_qty_reject, " _
                & "  a.lbrf_date, " _
                & "  a.lbrf_start_setup, " _
                & "  a.lbrf_stop_setup, " _
                & "  a.lbrf_elapsed_setup, " _
                & "  a.lbrf_start_run, " _
                & "  a.lbrf_stop_run, " _
                & "  a.lbrf_elapsed_run, " _
                & "  a.lbrf_down_start, " _
                & "  a.lbrf_down_stop, " _
                & "  a.lbrf_elapsed_down,lbrf_qty_conversion, " _
                & "  a.lbrf_down_reason_id, " _
                & "  h.qc_desc as qc_desc_in, " _
                & "  g.qc_desc as qc_desc_out, " _
                & "  a.lbrf_qc_out_reason_id,pt_desc1, " _
                & "  a.lbrf_qc_in_reason_id, " _
                & "  a.lbrf_remarks,lbrf_add_by,lbrf_add_date,lbrf_upd_by,lbrf_upd_date,lbrf_activity_type_id,lbrfa_desc , " _
                  & "  l.lbrfd_oid, " _
            & "  l.lbrfd_lbrf_oid, " _
            & "  l.lbrfd_lbrfp_id, " _
            & "  m.lbrfp_name, " _
            & "  m.lbrfp_group " _
                & " FROM " _
                & "  public.lbrf_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.lbrf_en_id = b.en_id) " _
                & "  INNER JOIN public.wodr_routing c ON (a.lbrf_wodr_uid = c.wodr_uid) " _
                & "  INNER JOIN public.wo_mstr d ON (c.wodr_wo_oid = d.wo_oid) " _
                 & "  INNER JOIN public.pt_mstr k ON (d.wo_pt_id = k.pt_id) " _
                & "  LEFT outer JOIN public.code_mstr e ON (a.lbrf_down_reason_id = e.code_id) " _
                & "  LEFT outer JOIN public.wc_mstr f ON (c.wodr_wc_id = f.wc_id) " _
                & "  LEFT outer JOIN public.qc_reason_mstr g ON  (a.lbrf_qc_out_reason_id = g.qc_id) " _
                & "  LEFT outer JOIN public.qc_reason_mstr h ON (a.lbrf_qc_in_reason_id = h.qc_id) " _
                & "  LEFT outer JOIN public.lbrfa_activity i ON (a.lbrf_activity_type_id = i.lbrfa_id) " _
                & "  LEFT outer JOIN public.code_mstr j ON (a.lbrf_shift_id = j.code_id) " _
                & "  LEFT outer JOIN  public.lbrfd_det_person l ON (a.lbrf_oid = l.lbrfd_lbrf_oid) " _
               & "  LEFT outer JOIN public.lbrfp_person m ON (l.lbrfd_lbrfp_id = m.lbrfp_id) " _
                & " WHERE " _
                & "  a.lbrf_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
                & " and lbrf_en_id IN (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " ORDER BY " _
                & "  a.lbrf_date"


        Return get_sequel
    End Function

End Class
