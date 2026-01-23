Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports System.IO
Imports master_new.PGSqlConn
Imports System.Text

Public Class FRequisitionWFDetailReport
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _req_oid_mstr As String
    Dim ds_edit As DataSet
    Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _reqd_related_oid As String = ""
    Dim _cost_center_pr_po As String = ""
    Dim _conf_value As String
    Dim _now As Date
    Dim mf As New master_new.ModFunction
    Dim dt_edit_doc As New DataTable

    Private Sub FRequisitionWFDetailReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        form_first_load()
        _now = func_coll.get_tanggal_sistem
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        ce_detail.EditValue = False
        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wf_ref_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description", "wf_ref_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Req Date", "req_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Req Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WF Seq", "wf_seq", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (1st)", "wf_aprv_user_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (1st)", "wfs_desc_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (1st)", "wf_iscurrent_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (1st)", "wf_aprv_date_0", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (1st)", "wf_desc_0", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (2nd)", "wf_aprv_user_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (2nd)", "wfs_desc_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (2nd)", "wf_iscurrent_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (2nd)", "wf_aprv_date_1", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column_copy(gv_master, "Remarks (2nd)", "wf_desc_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (3rd)", "wf_aprv_user_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (3rd)", "wfs_desc_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (3rd)", "wf_iscurrent_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (3rd)", "wf_aprv_date_2", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (3rd)", "wf_desc_2", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (4th)", "wf_aprv_user_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (4th)", "wfs_desc_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (4th)", "wf_iscurrent_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (4th)", "wf_aprv_date_3", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (4th)", "wf_desc_3", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (5th)", "wf_aprv_user_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (5th)", "wfs_desc_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (5th)", "wf_iscurrent_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (5th)", "wf_aprv_date_4", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (5th)", "wf_desc_4", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (6th)", "wf_aprv_user_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (6th)", "wfs_desc_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (6th)", "wf_iscurrent_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (6th)", "wf_aprv_date_5", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (6th)", "wf_desc_5", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Approval (7th)", "wf_aprv_user_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status (7th)", "wfs_desc_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Current (7th)", "wf_iscurrent_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Aprroval Date (7th)", "wf_aprv_date_6", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Remarks (7th)", "wf_desc_6", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Due Date", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Type", "req_type", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_master, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqd_req_oid", False)
        add_column(gv_detail, "wf_ref_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_detail, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "reqd_cost", False) 'cost
        add_column(gv_detail, "reqd_disc", False) 'diskon
        add_column_copy(gv_detail, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "reqd_qty_cost", False) 'qty * cost
        add_column(gv_detail, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_wf, "wf_ref_code", False)
        'add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        'add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        'add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)


    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        If ce_detail.EditValue = True Then
            get_sequel = "SELECT DISTINCT  " _
                    & "  public.req_mstr.req_en_id, " _
                    & "  public.wf_mstr.wf_dom_id, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.wf_mstr.wf_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.wf_mstr.wf_tran_id, " _
                    & "  public.wf_mstr.wf_ref_oid, " _
                    & "  public.wf_mstr.wf_ref_code, " _
                    & "  public.req_mstr.req_trans_id, " _
                    & "  public.req_mstr.req_date, " _
                    & "  COUNT(public.wf_mstr.wf_seq) AS wf_seq, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_0, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_0, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_0, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wfs_status.wfs_desc END) AS wfs_desc_0, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_desc END) AS wf_desc_0, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_1, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_1, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_1, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wfs_status.wfs_desc END) AS wfs_desc_1, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_desc END) AS wf_desc_1, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_2, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_2, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_2, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wfs_status.wfs_desc END) AS wfs_desc_2, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_desc END) AS wf_desc_2, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_3, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_3, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_3, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wfs_status.wfs_desc END) AS wfs_desc_3, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_desc END) AS wf_desc_3, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_4, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_4, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_4, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wfs_status.wfs_desc END) AS wfs_desc_4, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_desc END) AS wf_desc_4, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_5, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_5, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_5, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wfs_status.wfs_desc END) AS wfs_desc_5, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_desc END) AS wf_desc_5, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_6, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_6, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_6, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wfs_status.wfs_desc END) AS wfs_desc_6, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_desc END) AS wf_desc_6, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_7, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_7, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_7, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wfs_status.wfs_desc END) AS wfs_desc_7, " _
                    & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_desc END) AS wf_desc_7, " _
                    & "  public.reqd_det.reqd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.reqd_det.reqd_qty, " _
                    & "  public.reqd_det.reqd_um, " _
                    & "  public.code_mstr.code_name " _
                    & "FROM " _
                    & "  public.wf_mstr " _
                    & "  INNER JOIN public.req_mstr ON (public.wf_mstr.wf_ref_oid = public.req_mstr.req_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.wf_mstr.wf_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.reqd_det ON (public.req_mstr.req_oid = public.reqd_det.reqd_req_oid) " _
                    & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.wfs_status ON (public.wfs_status.wfs_id = public.wf_mstr.wf_wfs_id) " _
                    & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & "GROUP BY " _
                    & "  public.req_mstr.req_en_id, " _
                    & "  public.wf_mstr.wf_dom_id, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.wf_mstr.wf_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.wf_mstr.wf_tran_id, " _
                    & "  public.wf_mstr.wf_ref_oid, " _
                    & "  public.wf_mstr.wf_ref_code, " _
                    & "  public.req_mstr.req_trans_id, " _
                    & "  public.req_mstr.req_date, " _
                    & "  public.reqd_det.reqd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.reqd_det.reqd_qty, " _
                    & "  public.reqd_det.reqd_um, " _
                    & "  public.code_mstr.code_name"

        Else
            get_sequel = "SELECT DISTINCT  " _
                                & "  public.req_mstr.req_en_id, " _
                                & "  public.wf_mstr.wf_dom_id, " _
                                & "  public.req_mstr.req_code, " _
                                & "  public.wf_mstr.wf_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.wf_mstr.wf_tran_id, " _
                                & "  public.wf_mstr.wf_ref_oid, " _
                                & "  public.wf_mstr.wf_ref_code, " _
                                & "  public.req_mstr.req_trans_id, " _
                                & "  public.req_mstr.req_date, " _
                                & "  COUNT(public.wf_mstr.wf_seq) AS wf_seq, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_0, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_0, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_0, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wfs_status.wfs_desc END) AS wfs_desc_0, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_desc END) AS wf_desc_0, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_1, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_1, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_1, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wfs_status.wfs_desc END) AS wfs_desc_1, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_desc END) AS wf_desc_1, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_2, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_2, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_2, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wfs_status.wfs_desc END) AS wfs_desc_2, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_desc END) AS wf_desc_2, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_3, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_3, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_3, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wfs_status.wfs_desc END) AS wfs_desc_3, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_desc END) AS wf_desc_3, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_4, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_4, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_4, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wfs_status.wfs_desc END) AS wfs_desc_4, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_desc END) AS wf_desc_4, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_5, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_5, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_5, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wfs_status.wfs_desc END) AS wfs_desc_5, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_desc END) AS wf_desc_5, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_6, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_6, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_6, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wfs_status.wfs_desc END) AS wfs_desc_6, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_desc END) AS wf_desc_6, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_7, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_7, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_7, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wfs_status.wfs_desc END) AS wfs_desc_7, " _
                                & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_desc END) AS wf_desc_7, " _
                                & "  public.req_mstr.req_rmks, " _
                                & "  public.reqd_det.reqd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.reqd_det.reqd_qty, " _
                                & "  public.reqd_det.reqd_qty_processed, " _
                                & "  public.reqd_det.reqd_oid, " _
                                & "  public.pod_det.pod_reqd_oid, " _
                                & "  public.po_mstr.po_code, " _
                                & "  public.po_mstr.po_date, " _
                                & "  COUNT(po_wf_mstr.wf_seq) AS po_wf_seq, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_0, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_0, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_0, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_0, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_0, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_1, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_1, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_1, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_1, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_1, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_2, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_2, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_2, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_2, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_2, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_3, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_3, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_3, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_3, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_3, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_4, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_4, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_4, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_4, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_4, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_5, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_5, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_5, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_5, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_5, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_6, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_6, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_6, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_6, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_6, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_7, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_7, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_7, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_7, " _
                                & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_7 " _
                                & "FROM " _
                                & "  public.wf_mstr " _
                                & "  INNER JOIN public.req_mstr ON (public.wf_mstr.wf_ref_oid = public.req_mstr.req_oid) " _
                                & "  INNER JOIN public.en_mstr ON (public.wf_mstr.wf_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.wfs_status ON (public.wfs_status.wfs_id = public.wf_mstr.wf_wfs_id) " _
                                & "  INNER JOIN public.reqd_det ON (public.req_mstr.req_oid = public.reqd_det.reqd_req_oid) " _
                                & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                                & "  LEFT OUTER JOIN public.pod_det ON (public.reqd_det.reqd_oid = public.pod_det.pod_reqd_oid) " _
                                & "  LEFT OUTER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
                                & "  LEFT OUTER JOIN public.wf_mstr po_wf_mstr ON (public.po_mstr.po_oid = po_wf_mstr.wf_ref_oid) " _
                                & "  LEFT OUTER JOIN public.wfs_status po_wfs_status ON (po_wf_mstr.wf_wfs_id = po_wfs_status.wfs_id) " _
                                & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
                                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                & "GROUP BY " _
                                & "  public.req_mstr.req_en_id, " _
                                & "  public.wf_mstr.wf_dom_id, " _
                                & "  public.req_mstr.req_code, " _
                                & "  public.wf_mstr.wf_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.wf_mstr.wf_tran_id, " _
                                & "  public.wf_mstr.wf_ref_oid, " _
                                & "  public.wf_mstr.wf_ref_code, " _
                                & "  public.req_mstr.req_trans_id, " _
                                & "  public.req_mstr.req_date, " _
                                & "  public.req_mstr.req_rmks, " _
                                & "  public.reqd_det.reqd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.reqd_det.reqd_qty, " _
                                & "  public.reqd_det.reqd_qty_processed, " _
                                & "  public.reqd_det.reqd_oid, " _
                                & "  public.pod_det.pod_reqd_oid, " _
                                & "  public.po_mstr.po_code, " _
                                & "  public.po_mstr.po_date"
        End If

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT DISTINCT" _
            & "  public.reqd_det.reqd_oid, " _
            & "  public.reqd_det.reqd_dom_id, " _
            & "  public.reqd_det.reqd_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.reqd_det.reqd_add_by, " _
            & "  public.reqd_det.reqd_add_date, " _
            & "  public.reqd_det.reqd_upd_by, " _
            & "  public.reqd_det.reqd_upd_date, " _
            & "  public.reqd_det.reqd_req_oid, " _
            & "  public.wf_mstr.wf_ref_oid, " _
            & "  public.reqd_det.reqd_seq, " _
            & "  public.reqd_det.reqd_related_oid, " _
            & "  req_mstr_relation.req_code as req_code_relation, " _
            & "  public.reqd_det.reqd_ptnr_id, " _
            & "  public.ptnr_mstr.ptnr_name, " _
            & "  public.reqd_det.reqd_si_id, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.reqd_det.reqd_pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  public.reqd_det.reqd_rmks, " _
            & "  public.reqd_det.reqd_end_user, " _
            & "  public.reqd_det.reqd_qty, " _
            & "  public.reqd_det.reqd_qty_processed, " _
            & "  public.reqd_det.reqd_qty_completed, " _
            & "  public.reqd_det.reqd_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.reqd_det.reqd_cost, " _
            & "  public.reqd_det.reqd_disc, " _
            & "  public.reqd_det.reqd_need_date, " _
            & "  public.reqd_det.reqd_due_date, " _
            & "  public.reqd_det.reqd_um_conv, " _
            & "  public.reqd_det.reqd_qty_real, " _
            & "  public.reqd_det.reqd_pt_class, " _
            & "  public.reqd_det.reqd_status, " _
            & "  public.reqd_det.reqd_dt,  " _
            & "  public.reqd_det.reqd_dt, " _
            & "  public.reqd_det.reqd_pt_desc1, " _
            & "  public.reqd_det.reqd_pt_desc2,  public.reqd_det.reqd_boqs_oid, " _
            & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
            & "  FROM " _
            & "  public.reqd_det " _
            & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
            & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid               " _
            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
            & "  INNER JOIN public.wf_mstr ON (public.reqd_det.reqd_req_oid = public.wf_mstr.wf_ref_oid) " _
            & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail, "detail")


    End Sub

    Public Overrides Sub relation_detail()
        Try

            gv_detail.Columns("reqd_req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqd_req_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wf_ref_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub
#End Region

End Class
