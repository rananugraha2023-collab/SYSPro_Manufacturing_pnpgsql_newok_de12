Imports Npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports System.IO
Imports master_new.PGSqlConn
Imports System.Text

Public Class FRequisitionWFDetailReportNew
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

    Private Sub FRequisitionWFDetailReportNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        add_column_copy(gv_master, "Req Date", "req_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")


        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Process", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "WF Seq", "pr_wf_seq_count", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PR Approval Summary ", "approval_summary", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "PR Approval Time", "pr_process_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Req Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "PR Approval Lead Time (days)", "pr_process_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PR Cycle Time (days)", "pr_to_po_duration", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "PR Total Lead Time (days) ", "total_pr_po_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "PO Code", "po_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "WF Seq", "po_wf_seq_count", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description", "po_wfs_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "PO Approval Summary", "po_approval_summary", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO Total Lead Time (days) ", "pr_to_po_approval_duration", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (1st)", "po_wfs_desc_0", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (1st)", "po_wf_iscurrent_0", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (1st)", "po_wf_aprv_date_0", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (1st)", "po_wf_desc_0", DevExpress.Utils.HorzAlignment.Default)

        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (2nd)", "po_wf_aprv_user_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (2nd)", "po_wfs_desc_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (2nd)", "po_wf_iscurrent_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (2nd)", "po_wf_aprv_date_1", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (2nd)", "po_wf_desc_1", DevExpress.Utils.HorzAlignment.Default)



        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (3rd)", "po_wf_aprv_user_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (3rd)", "po_wfs_desc_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (3rd)", "po_wf_iscurrent_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (3nd)", "po_wf_aprv_date_2", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (3rd)", "po_wf_desc_2", DevExpress.Utils.HorzAlignment.Default)

        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (4th)", "po_wf_aprv_user_3", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (4th)", "po_wfs_desc_3", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (4th)", "po_wf_iscurrent_3", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (4th)", "po_wf_aprv_date_3", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (4th)", "po_wf_desc_3", DevExpress.Utils.HorzAlignment.Default)

        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (5th)", "po_wf_aprv_user_4", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (5th)", "po_wfs_desc_4", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (5th)", "po_wf_iscurrent_4", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (5th)", "po_wf_aprv_date_4", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (5th)", "po_wf_desc_4", DevExpress.Utils.HorzAlignment.Default)

        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (6th)", "po_wf_aprv_user_5", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (6th)", "po_wfs_desc_5", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (6th)", "po_wf_iscurrent_5", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (6th)", "po_wf_aprv_date_5", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (6th)", "po_wf_desc_5", DevExpress.Utils.HorzAlignment.Default)

        ''add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "User Approval (7th)", "po_wf_aprv_user_6", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Status (7th)", "po_wfs_desc_6", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Current (7th)", "po_wf_iscurrent_6", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Aprroval Date (7th)", "po_wf_aprv_date_6", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_master, "Remarks (7th)", "po_wf_desc_6", DevExpress.Utils.HorzAlignment.Default)

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
        'If ce_detail.EditValue = True Then
        'get_sequel = "SELECT DISTINCT  " _
        '        & "  public.req_mstr.req_en_id, " _
        '        & "  public.wf_mstr.wf_dom_id, " _
        '        & "  public.req_mstr.req_code, " _
        '        & "  public.wf_mstr.wf_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  public.wf_mstr.wf_tran_id, " _
        '        & "  public.wf_mstr.wf_ref_oid, " _
        '        & "  public.wf_mstr.wf_ref_code, " _
        '        & "  public.req_mstr.req_trans_id, " _
        '        & "  public.req_mstr.req_date, " _
        '        & "  COUNT(public.wf_mstr.wf_seq) AS wf_seq, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_0, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_0, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_0, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wfs_status.wfs_desc END) AS wfs_desc_0, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '0' THEN public.wf_mstr.wf_desc END) AS wf_desc_0, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_1, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_1, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_1, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wfs_status.wfs_desc END) AS wfs_desc_1, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '1' THEN public.wf_mstr.wf_desc END) AS wf_desc_1, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_2, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_2, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_2, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wfs_status.wfs_desc END) AS wfs_desc_2, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '2' THEN public.wf_mstr.wf_desc END) AS wf_desc_2, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_3, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_3, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_3, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wfs_status.wfs_desc END) AS wfs_desc_3, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '3' THEN public.wf_mstr.wf_desc END) AS wf_desc_3, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_4, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_4, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_4, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wfs_status.wfs_desc END) AS wfs_desc_4, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '4' THEN public.wf_mstr.wf_desc END) AS wf_desc_4, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_5, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_5, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_5, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wfs_status.wfs_desc END) AS wfs_desc_5, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '5' THEN public.wf_mstr.wf_desc END) AS wf_desc_5, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_6, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_6, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_6, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wfs_status.wfs_desc END) AS wfs_desc_6, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '6' THEN public.wf_mstr.wf_desc END) AS wf_desc_6, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_aprv_date END) AS wf_aprv_date_7, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_user_id END) AS wf_aprv_user_7, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_iscurrent END) AS wf_iscurrent_7, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wfs_status.wfs_desc END) AS wfs_desc_7, " _
        '        & "  MAX(CASE WHEN public.wf_mstr.wf_seq = '7' THEN public.wf_mstr.wf_desc END) AS wf_desc_7, " _
        '        & "  public.reqd_det.reqd_pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.pt_mstr.pt_desc2, " _
        '        & "  public.reqd_det.reqd_qty, " _
        '        & "  public.reqd_det.reqd_um, " _
        '        & "  public.code_mstr.code_name " _
        '        & "FROM " _
        '        & "  public.wf_mstr " _
        '        & "  INNER JOIN public.req_mstr ON (public.wf_mstr.wf_ref_oid = public.req_mstr.req_oid) " _
        '        & "  INNER JOIN public.en_mstr ON (public.wf_mstr.wf_en_id = public.en_mstr.en_id) " _
        '        & "  INNER JOIN public.reqd_det ON (public.req_mstr.req_oid = public.reqd_det.reqd_req_oid) " _
        '        & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
        '        & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
        '        & "  INNER JOIN public.wfs_status ON (public.wfs_status.wfs_id = public.wf_mstr.wf_wfs_id) " _
        '        & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '        & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '        & "  and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
        '        & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '        & "GROUP BY " _
        '        & "  public.req_mstr.req_en_id, " _
        '        & "  public.wf_mstr.wf_dom_id, " _
        '        & "  public.req_mstr.req_code, " _
        '        & "  public.wf_mstr.wf_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  public.wf_mstr.wf_tran_id, " _
        '        & "  public.wf_mstr.wf_ref_oid, " _
        '        & "  public.wf_mstr.wf_ref_code, " _
        '        & "  public.req_mstr.req_trans_id, " _
        '        & "  public.req_mstr.req_date, " _
        '        & "  public.reqd_det.reqd_pt_id, " _
        '        & "  public.pt_mstr.pt_code, " _
        '        & "  public.pt_mstr.pt_desc1, " _
        '        & "  public.pt_mstr.pt_desc2, " _
        '        & "  public.reqd_det.reqd_qty, " _
        '        & "  public.reqd_det.reqd_um, " _
        '        & "  public.code_mstr.code_name"

        'Else
        'get_sequel = "SELECT DISTINCT  " _
        '        & "  public.req_mstr.req_en_id, " _
        '        & "  public.req_mstr.req_en_id, " _
        '        & "  req_wf_mstr.wf_dom_id, " _
        '        & "  public.req_mstr.req_code, " _
        '        & "  req_wf_mstr.wf_en_id, " _
        '        & "  public.en_mstr.en_desc, " _
        '        & "  req_wf_mstr.wf_tran_id, " _
        '        & "  req_wf_mstr.wf_ref_oid, " _
        '        & "  req_wf_mstr.wf_ref_code, " _
        '        & "  public.req_mstr.req_trans_id, " _
        '        & "  public.req_mstr.req_date, " _
        '        & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '0' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_0, " _
        '        & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '0' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_0, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '0' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_0, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '0' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_0, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '0' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_0, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '1' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_1, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '1' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_1, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '1' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_1, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '1' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_1, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '1' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_1, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '2' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_2, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '2' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_2, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '2' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_2, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '2' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_2, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '2' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_2, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '3' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_3, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '3' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_3, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '3' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_3, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '3' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_3, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '3' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_3, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '4' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_4, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '4' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_4, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '4' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_4, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '4' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_4, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '4' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_4, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '5' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_5, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '5' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_5, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '5' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_5, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '5' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_5, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '5' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_5, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '6' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_6, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '6' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_6, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '6' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_6, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '6' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_6, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '6' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_6, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '7' THEN req_wf_mstr.wf_aprv_date END) AS req_wf_aprv_date_7, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '7' THEN req_wf_mstr.wf_user_id END) AS req_wf_aprv_user_7, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '7' THEN req_wf_mstr.wf_iscurrent END) AS req_wf_iscurrent_7, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '7' THEN req_wfs_status.wfs_desc END) AS req_wfs_desc_7, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = '7' THEN req_wf_mstr.wf_desc END) AS req_wf_desc_7, " _
        '                    & "  MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) AS wf_aprv_date_max, " _
        '                    & "  (MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date) AS pr_wf_duration, " _
        '                    & "  concat( " _
        '                    & "  extract(day from (MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date)), ' hari ', " _
        '                    & "  lpad(extract(hour from (MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date))::text, 2, '0'), ':'," _
        '                    & "  lpad(extract(minute from (MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date))::text, 2, '0') " _
        '                    & ") AS pr_wf_duration_fmt," _
        '                    & "  public.req_mstr.req_rmks, " _
        '                    & "  public.reqd_det.reqd_pt_id, " _
        '                    & "  public.pt_mstr.pt_code, " _
        '                    & "  public.pt_mstr.pt_desc1, " _
        '                    & "  public.pt_mstr.pt_desc2, " _
        '                    & "  public.pt_mstr.pt_um, " _
        '                    & "  public.code_mstr.code_name, " _
        '                    & "  public.reqd_det.reqd_qty, " _
        '                    & "  public.reqd_det.reqd_qty_processed, " _
        '                    & "  public.reqd_det.reqd_oid, " _
        '                    & "  public.pod_det.pod_reqd_oid, " _
        '                    & "  public.po_mstr.po_code, " _
        '                    & "  public.po_mstr.po_date, " _
        '                    & "  COUNT(po_wf_mstr.wf_seq) AS po_wf_seq, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_0, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_0, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_0, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_0, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '0' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_0, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_1, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_1, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_1, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_1, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '1' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_1, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_2, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_2, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_2, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_2, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '2' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_2, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_3, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_3, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_3, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_3, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '3' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_3, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_4, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_4, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_4, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_4, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '4' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_4, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_5, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_5, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_5, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_5, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '5' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_5, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_6, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_6, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_6, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_6, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '6' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_6, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_aprv_date END) AS po_wf_aprv_date_7, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_user_id END) AS po_wf_aprv_user_7, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_iscurrent END) AS po_wf_iscurrent_7, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wfs_status.wfs_desc END) AS po_wfs_desc_7, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = '7' THEN po_wf_mstr.wf_desc END) AS po_wf_desc_7, " _
        '                    & "  MAX(CASE WHEN po_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.po_mstr.po_oid) THEN po_wf_mstr.wf_aprv_date END) AS po_aprv_date_max, " _
        '                    & "  (MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date) AS pr_wf_duration, " _
        '                    & "  (MAX(CASE WHEN po_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.po_mstr.po_oid) THEN po_wf_mstr.wf_aprv_date END) - public.po_mstr.po_date) as po_wf_duration, " _
        '                    & "  (public.po_mstr.po_date -(MAX(CASE WHEN req_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.req_mstr.req_oid) THEN req_wf_mstr.wf_aprv_date END))) as pr_release_po, " _
        '                    & "  (MAX(CASE WHEN po_wf_mstr.wf_seq = (SELECT MAX(wf_seq) FROM public.wf_mstr WHERE wf_ref_oid = public.po_mstr.po_oid) THEN po_wf_mstr.wf_aprv_date END) - public.req_mstr.req_date) as pemenuhan " _
        '                    & "FROM " _
        '                    & "  public.wf_mstr req_wf_mstr " _
        '                    & "  INNER JOIN public.req_mstr ON (req_wf_mstr.wf_ref_oid = public.req_mstr.req_oid) " _
        '                    & "  INNER JOIN public.en_mstr ON (req_wf_mstr.wf_en_id = public.en_mstr.en_id) " _
        '                    & "  INNER JOIN public.wfs_status req_wfs_status ON (req_wfs_status.wfs_id = req_wf_mstr.wf_wfs_id) " _
        '                    & "  INNER JOIN public.reqd_det ON (public.req_mstr.req_oid = public.reqd_det.reqd_req_oid) " _
        '                    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
        '                    & "  LEFT OUTER JOIN public.pod_det ON (public.reqd_det.reqd_oid = public.pod_det.pod_reqd_oid) " _
        '                    & "  LEFT OUTER JOIN public.po_mstr ON (public.pod_det.pod_po_oid = public.po_mstr.po_oid) " _
        '                    & "  LEFT OUTER JOIN public.wf_mstr po_wf_mstr ON (public.po_mstr.po_oid = po_wf_mstr.wf_ref_oid) " _
        '                    & "  LEFT OUTER JOIN public.wfs_status po_wfs_status ON (po_wf_mstr.wf_wfs_id = po_wfs_status.wfs_id) " _
        '                    & "  INNER JOIN public.code_mstr ON (public.pt_mstr.pt_um = public.code_mstr.code_id) " _
        '                    & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '                    & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
        '                    & "  and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
        '                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '                    & "GROUP BY " _
        '                    & "  public.req_mstr.req_en_id, " _
        '                    & "  req_wf_mstr.wf_dom_id, " _
        '                    & "  public.req_mstr.req_code, " _
        '                    & "  req_wf_mstr.wf_en_id, " _
        '                    & "  public.en_mstr.en_desc, " _
        '                    & "  req_wf_mstr.wf_tran_id, " _
        '                    & "  req_wf_mstr.wf_ref_oid, " _
        '                    & "  req_wf_mstr.wf_ref_code, " _
        '                    & "  public.req_mstr.req_trans_id, " _
        '                    & "  public.req_mstr.req_date, " _
        '                    & "  public.req_mstr.req_rmks, " _
        '                    & "  public.reqd_det.reqd_pt_id, " _
        '                    & "  public.pt_mstr.pt_code, " _
        '                    & "  public.pt_mstr.pt_desc1, " _
        '                    & "  public.pt_mstr.pt_desc2, " _
        '                    & "  public.pt_mstr.pt_um, " _
        '                    & "  public.code_mstr.code_name, " _
        '                    & "  public.reqd_det.reqd_qty, " _
        '                    & "  public.reqd_det.reqd_qty_processed, " _
        '                    & "  public.reqd_det.reqd_oid, " _
        '                    & "  public.pod_det.pod_reqd_oid, " _
        '                    & "  public.po_mstr.po_code, " _
        '                    & "  public.po_mstr.po_date"

        'get_sequel = "SELECT r.wf_ref_oid, " _
        '    & " r.en_desc, " _
        '    & " r.req_code as wf_ref_code, " _
        '    & " r.req_date, " _
        '    & " r.req_add_date, " _
        '    & " r.req_trans_id, " _
        '    & " f.wf_start_dt, " _
        '    & " COUNT(r.wf_seq) AS wf_seq, " _
        '    & " wfs_desc, " _
        '    & " STRING_AGG( " _
        '    & "   CASE " _
        '    & "     WHEN r.wf_iscurrent = 'N' AND r.wf_aprv_date IS NOT NULL THEN 'Approved by ' || r.wf_user_id || ' ' || TO_CHAR(r.wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
        '    & "     WHEN r.wf_iscurrent = 'Y' AND r.wf_seq = w.waiting_seq THEN 'Waiting for Approve by ' || r.wf_user_id " _
        '    & "     WHEN r.wf_iscurrent = 'N' AND r.wf_seq > w.waiting_seq THEN 'Pending by ' || r.wf_user_id " _
        '    & "     ELSE NULL " _
        '    & "   END, ' - ' ORDER BY r.wf_seq) AS approval_summary " _
        '    & "FROM ( " _
        '    & " SELECT wf_mstr.wf_ref_oid, " _
        '    & "        public.req_mstr.req_en_id, " _
        '    & "        public.en_mstr.en_desc, " _
        '    & "        public.req_mstr.req_code, " _
        '    & "        public.req_mstr.req_add_date, " _
        '    & "        public.req_mstr.req_date, " _
        '    & "        public.req_mstr.req_trans_id, " _
        '    & "        wf_mstr.wf_seq, " _
        '    & "        wf_mstr.wf_user_id, " _
        '    & "        wf_mstr.wf_aprv_user, " _
        '    & "        wf_mstr.wf_aprv_date, " _
        '    & "        wf_mstr.wf_iscurrent, " _
        '    & "        wf_mstr.wf_dt, " _
        '    & "        wfs_status.wfs_desc, " _
        '    & "        ROW_NUMBER() OVER(PARTITION BY wf_mstr.wf_ref_oid ORDER BY wf_mstr.wf_seq) AS rn " _
        '    & " FROM wf_mstr " _
        '    & " INNER JOIN public.req_mstr ON wf_mstr.wf_ref_oid = public.req_mstr.req_oid " _
        '    & " INNER JOIN public.en_mstr ON public.req_mstr.req_en_id = public.en_mstr.en_id " _
        '    & " INNER JOIN public.wfs_status wfs_status ON (wf_mstr.wf_wfs_id = wfs_status.wfs_id) " _
        '    & ") r " _
        '    & "LEFT JOIN ( " _
        '    & " SELECT wf_ref_oid, MIN(wf_seq) AS waiting_seq " _
        '    & " FROM wf_mstr WHERE wf_iscurrent = 'Y' GROUP BY wf_ref_oid " _
        '    & ") w ON r.wf_ref_oid = w.wf_ref_oid " _
        '    & "LEFT JOIN ( " _
        '    & " SELECT wf_ref_oid, MIN(wf_dt) AS wf_start_dt " _
        '    & " FROM wf_mstr GROUP BY wf_ref_oid " _
        '    & ") f ON r.wf_ref_oid = f.wf_ref_oid " _
        '    & "WHERE r.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) + " " _
        '    & "AND r.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
        '    & "AND r.req_en_id IN (SELECT user_en_id FROM tconfuserentity WHERE userid = " + master_new.ClsVar.sUserID.ToString + ") " _
        '    & "GROUP BY r.wf_ref_oid, r.req_code, f.wf_start_dt, r.req_add_date,r.en_desc,r.req_date,r.req_trans_id,wf_iscurrent,wfs_desc"


        get_sequel = "SELECT " _
            & " r.wf_ref_oid, " _
            & " r.en_desc, " _
            & " r.req_code AS wf_ref_code, " _
            & " r.req_date, " _
            & " r.req_add_date, " _
            & " r.req_trans_id, " _
            & " f.wf_start_dt, " _
            & " l.wf_last_dt, " _
            & " COUNT(r.pr_wf_seq) AS pr_wf_seq_count, " _
              & " CASE " _
    & " WHEN last_step.final_status = 'Waiting' THEN 'Waiting' " _
    & " WHEN last_step.final_status = 'Pending' THEN 'Pending' " _
    & " ELSE MAX(r.pr_wfs_desc) " _
        & " END AS wfs_desc, " _
            & " STRING_AGG( " _
            & " CASE " _
            & " WHEN r.wf_iscurrent = 'N' AND r.wf_aprv_date IS NOT NULL THEN  " _
            & " 'Approved by ' || r.wf_user_id || ' ' || TO_CHAR(r.wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
            & " WHEN r.pr_wf_seq = w.waiting_seq THEN  " _
            & " 'Waiting for Approve by ' || r.wf_user_id " _
            & " WHEN r.pr_wf_seq > w.waiting_seq THEN  " _
            & " 'Pending by ' || r.wf_user_id " _
            & " ELSE NULL " _
            & " END, " _
            & " ' - ' " _
            & " ORDER BY r.pr_wf_seq " _
            & " ) AS approval_summary, " _
            & " r.pt_code, " _
            & " r.pt_desc1, " _
            & " r.pt_desc2, " _
            & " r.reqd_qty, " _
            & " r.reqd_qty_processed, " _
            & " r.pt_um, " _
            & " r.code_name, " _
            & " EXTRACT(DAY FROM AGE(l.wf_last_dt, r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(l.wf_last_dt, r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(l.wf_last_dt, r.req_add_date))::text, 2, '0') || ' menit' AS pr_process_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND a.max_aprv_date IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(p.po_add_date, a.max_aprv_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date, a.max_aprv_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date, a.max_aprv_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS pr_to_po_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(p.po_add_date, r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS total_pr_po_duration, " _
            & " r.po_code, " _
            & " r.po_add_date, " _
            & " r.po_date, " _
            & " COUNT(r.po_wf_seq) AS po_wf_seq_count, " _
            & " MAX(r.po_wfs_desc) AS po_wfs_desc, " _
            & " STRING_AGG( " _
            & " DISTINCT  CASE " _
            & " WHEN r.po_wf_iscurrent = 'N' AND r.po_wf_wf_aprv_date IS NOT NULL THEN  " _
            & " 'Approved by ' || r.po_wf_user_id || ' ' || TO_CHAR(r.po_wf_wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
            & " WHEN r.po_rn = 1 AND r.po_wf_iscurrent = 'Y' THEN  " _
            & " 'Waiting for Approve by ' || r.po_wf_user_id " _
            & " ELSE NULL " _
            & " END, " _
            & " ' - ' " _
            & " ) AS po_approval_summary, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS po_approval_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS pr_to_po_approval_duration " _
            & "  	FROM ( " _
            & "  	  SELECT " _
            & "  	    pr_wf.wf_ref_oid, " _
            & "  	    req.req_en_id, " _
            & "  	    en.en_desc, " _
            & "  	    req.req_code, " _
            & "  	    req.req_add_date, " _
            & "  	    req.req_date, " _
            & "  	    req.req_trans_id, " _
            & "  	    req.req_rmks, " _
            & "  	    reqd.reqd_pt_id, " _
            & "  	    pt.pt_code, " _
            & "  	    pt.pt_desc1, " _
            & "  	    pt.pt_desc2, " _
            & "  	    pt.pt_um, " _
            & "  	    code.code_name, " _
            & "  	    reqd.reqd_qty, " _
            & "  	    reqd.reqd_qty_processed, " _
            & "  	    reqd.reqd_oid, " _
            & "  	    pod.pod_reqd_oid, " _
            & "  	    po.po_code, " _
            & "  	    po.po_date, " _
            & "  	    po.po_add_date, " _
            & "  	    pr_wf.wf_seq AS pr_wf_seq, " _
            & "  	    pr_wf.wf_user_id, " _
            & "  	    pr_wf.wf_aprv_user, " _
            & "  	    pr_wf.wf_aprv_date, " _
            & "  	    pr_wf.wf_iscurrent, " _
            & "  	    pr_wf.wf_tran_id, " _
            & "  	    pr_wf.wf_ref_code, " _
            & "  	    pr_wf.wf_dt, " _
            & "  	    pr_wfs.wfs_desc AS pr_wfs_desc, " _
            & "  	    ROW_NUMBER() OVER (PARTITION BY pr_wf.wf_ref_oid ORDER BY pr_wf.wf_seq) AS pr_rn, " _
            & "  	    po_wf.wf_tran_id AS po_wf_tran_id, " _
            & "  	    po_wf.wf_ref_oid AS po_wf_ref_oid, " _
            & "  	    po_wf.wf_ref_code AS po_wf_ref_code, " _
            & "  	    po_wf.wf_seq AS po_wf_seq, " _
            & "  	    po_wf.wf_user_id AS po_wf_user_id, " _
            & "  	    po_wf.wf_iscurrent AS po_wf_iscurrent, " _
            & "  	    po_wf.wf_dt AS po_wf_wf_dt, " _
            & "  	    po_wf.wf_aprv_user AS po_wf_aprv_user, " _
            & "  	    po_wf.wf_aprv_date AS po_wf_wf_aprv_date, " _
            & "  	    po_wfs.wfs_desc AS po_wfs_desc, " _
            & "  	    ROW_NUMBER() OVER (PARTITION BY po_wf.wf_ref_oid ORDER BY po_wf.wf_seq) AS po_rn " _
            & "  	  FROM " _
            & "  	    wf_mstr pr_wf " _
            & "  	    INNER JOIN public.req_mstr req ON pr_wf.wf_ref_oid = req.req_oid " _
            & "  	    INNER JOIN public.en_mstr en ON req.req_en_id = en.en_id " _
            & "  	    INNER JOIN public.wfs_status pr_wfs ON pr_wf.wf_wfs_id = pr_wfs.wfs_id " _
            & "  	    INNER JOIN public.reqd_det reqd ON req.req_oid = reqd.reqd_req_oid " _
            & "  	    INNER JOIN public.pt_mstr pt ON reqd.reqd_pt_id = pt.pt_id " _
            & "  	    LEFT OUTER JOIN public.pod_det pod ON reqd.reqd_oid = pod.pod_reqd_oid " _
            & "  	    LEFT OUTER JOIN public.po_mstr po ON pod.pod_po_oid = po.po_oid " _
            & "  	    LEFT OUTER JOIN public.wf_mstr po_wf ON po.po_oid = po_wf.wf_ref_oid " _
            & "  	    LEFT OUTER JOIN public.wfs_status po_wfs ON po_wf.wf_wfs_id = po_wfs.wfs_id " _
            & "  	    INNER JOIN public.code_mstr code ON pt.pt_um = code.code_id " _
            & "  	) r " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MIN(wf_seq) AS waiting_seq " _
            & "  	  FROM wf_mstr " _
            & "  	  WHERE wf_iscurrent = 'Y' " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) w ON r.wf_ref_oid = w.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MIN(wf_dt) AS wf_start_dt " _
            & "  	  FROM wf_mstr " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) f ON r.wf_ref_oid = f.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MAX(wf_dt) AS wf_last_dt " _
            & "  	  FROM wf_mstr " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) l ON r.wf_ref_oid = l.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MAX(wf_aprv_date) AS max_aprv_date " _
            & "  	  FROM wf_mstr " _
            & "  	  WHERE wf_aprv_date IS NOT NULL " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) a ON r.wf_ref_oid = a.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT pod_reqd_oid, MIN(po_add_date) AS po_add_date " _
            & "  	  FROM pod_det " _
            & "  	  INNER JOIN po_mstr ON pod_det.pod_po_oid = po_mstr.po_oid " _
            & "  	  GROUP BY pod_reqd_oid " _
            & "  	) p ON r.reqd_oid = p.pod_reqd_oid " _
            & "  LEFT JOIN (" _
  & "  SELECT wf_ref_oid, wf_seq," _
         & "  CASE " _
           & "  WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'Y' THEN 'Waiting'" _
           & "  WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'N' THEN 'Pending'" _
           & "  ELSE NULL" _
         & "  END AS final_status" _
        & "  FROM wf_mstr" _
  & "  WHERE (wf_ref_oid, wf_seq) IN (" _
    & "  SELECT wf_ref_oid, MAX(wf_seq) " _
        & "  FROM wf_mstr " _
    & "  GROUP BY wf_ref_oid" _
  & "  ) " _
& "  ) last_step ON r.wf_ref_oid = last_step.wf_ref_oid AND r.pr_wf_seq = last_step.wf_seq " _
            & "WHERE r.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) + " " _
            & "AND r.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
            & "AND r.req_en_id IN (SELECT user_en_id FROM tconfuserentity WHERE userid = " + master_new.ClsVar.sUserID.ToString + ") " _
& "  	GROUP BY  " _
& "  	  r.wf_ref_oid, " _
& "  	  r.req_code, " _
& "  	  f.wf_start_dt, " _
& "  	  l.wf_last_dt, " _
& "  	  a.max_aprv_date, " _
& "  	  p.po_add_date, " _
& "  	  r.req_add_date, " _
& "  	  r.en_desc, " _
& "  	  r.req_date, " _
& "  	  r.req_trans_id, " _
& "  	  r.pt_code, " _
& "  	  r.pt_desc1, " _
& "  	  r.pt_desc2, " _
& "  	  r.reqd_qty, " _
& "  	  r.reqd_qty_processed, " _
& "  	  r.pt_um, " _
& "  	  r.code_name, " _
& "  	  r.po_add_date, " _
& "  	  r.po_date, " _
& "  	  r.po_code, " _
& "  	  r.po_wf_seq, " _
        & " Last_step.final_status "


        'End If
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

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT " _
            & " r.wf_ref_oid, " _
            & " r.en_desc, " _
            & " r.req_code AS wf_ref_code, " _
            & " r.req_date, " _
            & " r.req_add_date, " _
            & " r.req_trans_id, " _
            & " f.wf_start_dt, " _
            & " l.wf_last_dt, " _
            & " COUNT(r.pr_wf_seq) AS pr_wf_seq_count, " _
              & " CASE " _
    & " WHEN last_step.final_status = 'Waiting' THEN 'Waiting' " _
    & " WHEN last_step.final_status = 'Pending' THEN 'Pending' " _
    & " ELSE MAX(r.pr_wfs_desc) " _
        & " END AS wfs_desc, " _
            & " STRING_AGG( " _
            & " CASE " _
            & " WHEN r.wf_iscurrent = 'N' AND r.wf_aprv_date IS NOT NULL THEN  " _
            & " 'Approved by ' || r.wf_user_id || ' ' || TO_CHAR(r.wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
            & " WHEN r.pr_wf_seq = w.waiting_seq THEN  " _
            & " 'Waiting for Approve by ' || r.wf_user_id " _
            & " WHEN r.pr_wf_seq > w.waiting_seq THEN  " _
            & " 'Pending by ' || r.wf_user_id " _
            & " ELSE NULL " _
            & " END, " _
            & " ' - ' " _
            & " ORDER BY r.pr_wf_seq " _
            & " ) AS approval_summary, " _
            & " r.pt_code, " _
            & " r.pt_desc1, " _
            & " r.pt_desc2, " _
            & " r.reqd_qty, " _
            & " r.reqd_qty_processed, " _
            & " r.pt_um, " _
            & " r.code_name, " _
            & " EXTRACT(DAY FROM AGE(l.wf_last_dt, r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(l.wf_last_dt, r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(l.wf_last_dt, r.req_add_date))::text, 2, '0') || ' menit' AS pr_process_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND a.max_aprv_date IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(p.po_add_date, a.max_aprv_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date, a.max_aprv_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date, a.max_aprv_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS pr_to_po_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(p.po_add_date, r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS total_pr_po_duration, " _
            & " r.po_code, " _
            & " r.po_add_date, " _
            & " r.po_date, " _
            & " COUNT(r.po_wf_seq) AS po_wf_seq_count, " _
            & " MAX(r.po_wfs_desc) AS po_wfs_desc, " _
            & " STRING_AGG( " _
            & " DISTINCT  CASE " _
            & " WHEN r.po_wf_iscurrent = 'N' AND r.po_wf_wf_aprv_date IS NOT NULL THEN  " _
            & " 'Approved by ' || r.po_wf_user_id || ' ' || TO_CHAR(r.po_wf_wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
            & " WHEN r.po_rn = 1 AND r.po_wf_iscurrent = 'Y' THEN  " _
            & " 'Waiting for Approve by ' || r.po_wf_user_id " _
            & " ELSE NULL " _
            & " END, " _
            & " ' - ' " _
            & " ) AS po_approval_summary, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS po_approval_duration, " _
            & " CASE  " _
            & " WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN " _
            & " EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date)) || ' hari ' || " _
            & " LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' jam ' || " _
            & " LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' menit' " _
            & " ELSE NULL " _
            & " END AS pr_to_po_approval_duration " _
            & "  	FROM ( " _
            & "  	  SELECT " _
            & "  	    pr_wf.wf_ref_oid, " _
            & "  	    req.req_en_id, " _
            & "  	    en.en_desc, " _
            & "  	    req.req_code, " _
            & "  	    req.req_add_date, " _
            & "  	    req.req_date, " _
            & "  	    req.req_trans_id, " _
            & "  	    req.req_rmks, " _
            & "  	    reqd.reqd_pt_id, " _
            & "  	    pt.pt_code, " _
            & "  	    pt.pt_desc1, " _
            & "  	    pt.pt_desc2, " _
            & "  	    pt.pt_um, " _
            & "  	    code.code_name, " _
            & "  	    reqd.reqd_qty, " _
            & "  	    reqd.reqd_qty_processed, " _
            & "  	    reqd.reqd_oid, " _
            & "  	    pod.pod_reqd_oid, " _
            & "  	    po.po_code, " _
            & "  	    po.po_date, " _
            & "  	    po.po_add_date, " _
            & "  	    pr_wf.wf_seq AS pr_wf_seq, " _
            & "  	    pr_wf.wf_user_id, " _
            & "  	    pr_wf.wf_aprv_user, " _
            & "  	    pr_wf.wf_aprv_date, " _
            & "  	    pr_wf.wf_iscurrent, " _
            & "  	    pr_wf.wf_tran_id, " _
            & "  	    pr_wf.wf_ref_code, " _
            & "  	    pr_wf.wf_dt, " _
            & "  	    pr_wfs.wfs_desc AS pr_wfs_desc, " _
            & "  	    ROW_NUMBER() OVER (PARTITION BY pr_wf.wf_ref_oid ORDER BY pr_wf.wf_seq) AS pr_rn, " _
            & "  	    po_wf.wf_tran_id AS po_wf_tran_id, " _
            & "  	    po_wf.wf_ref_oid AS po_wf_ref_oid, " _
            & "  	    po_wf.wf_ref_code AS po_wf_ref_code, " _
            & "  	    po_wf.wf_seq AS po_wf_seq, " _
            & "  	    po_wf.wf_user_id AS po_wf_user_id, " _
            & "  	    po_wf.wf_iscurrent AS po_wf_iscurrent, " _
            & "  	    po_wf.wf_dt AS po_wf_wf_dt, " _
            & "  	    po_wf.wf_aprv_user AS po_wf_aprv_user, " _
            & "  	    po_wf.wf_aprv_date AS po_wf_wf_aprv_date, " _
            & "  	    po_wfs.wfs_desc AS po_wfs_desc, " _
            & "  	    ROW_NUMBER() OVER (PARTITION BY po_wf.wf_ref_oid ORDER BY po_wf.wf_seq) AS po_rn " _
            & "  	  FROM " _
            & "  	    wf_mstr pr_wf " _
            & "  	    INNER JOIN public.req_mstr req ON pr_wf.wf_ref_oid = req.req_oid " _
            & "  	    INNER JOIN public.en_mstr en ON req.req_en_id = en.en_id " _
            & "  	    INNER JOIN public.wfs_status pr_wfs ON pr_wf.wf_wfs_id = pr_wfs.wfs_id " _
            & "  	    INNER JOIN public.reqd_det reqd ON req.req_oid = reqd.reqd_req_oid " _
            & "  	    INNER JOIN public.pt_mstr pt ON reqd.reqd_pt_id = pt.pt_id " _
            & "  	    LEFT OUTER JOIN public.pod_det pod ON reqd.reqd_oid = pod.pod_reqd_oid " _
            & "  	    LEFT OUTER JOIN public.po_mstr po ON pod.pod_po_oid = po.po_oid " _
            & "  	    LEFT OUTER JOIN public.wf_mstr po_wf ON po.po_oid = po_wf.wf_ref_oid " _
            & "  	    LEFT OUTER JOIN public.wfs_status po_wfs ON po_wf.wf_wfs_id = po_wfs.wfs_id " _
            & "  	    INNER JOIN public.code_mstr code ON pt.pt_um = code.code_id " _
            & "  	) r " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MIN(wf_seq) AS waiting_seq " _
            & "  	  FROM wf_mstr " _
            & "  	  WHERE wf_iscurrent = 'Y' " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) w ON r.wf_ref_oid = w.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MIN(wf_dt) AS wf_start_dt " _
            & "  	  FROM wf_mstr " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) f ON r.wf_ref_oid = f.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MAX(wf_dt) AS wf_last_dt " _
            & "  	  FROM wf_mstr " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) l ON r.wf_ref_oid = l.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT wf_ref_oid, MAX(wf_aprv_date) AS max_aprv_date " _
            & "  	  FROM wf_mstr " _
            & "  	  WHERE wf_aprv_date IS NOT NULL " _
            & "  	  GROUP BY wf_ref_oid " _
            & "  	) a ON r.wf_ref_oid = a.wf_ref_oid " _
            & "  	LEFT JOIN ( " _
            & "  	  SELECT pod_reqd_oid, MIN(po_add_date) AS po_add_date " _
            & "  	  FROM pod_det " _
            & "  	  INNER JOIN po_mstr ON pod_det.pod_po_oid = po_mstr.po_oid " _
            & "  	  GROUP BY pod_reqd_oid " _
            & "  	) p ON r.reqd_oid = p.pod_reqd_oid " _
            & "  LEFT JOIN (" _
  & "  SELECT wf_ref_oid, wf_seq," _
         & "  CASE " _
           & "  WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'Y' THEN 'Waiting'" _
           & "  WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'N' THEN 'Pending'" _
           & "  ELSE NULL" _
         & "  END AS final_status" _
        & "  FROM wf_mstr" _
  & "  WHERE (wf_ref_oid, wf_seq) IN (" _
    & "  SELECT wf_ref_oid, MAX(wf_seq) " _
        & "  FROM wf_mstr " _
    & "  GROUP BY wf_ref_oid" _
  & "  ) " _
& "  ) last_step ON r.wf_ref_oid = last_step.wf_ref_oid AND r.pr_wf_seq = last_step.wf_seq " _
            & "WHERE r.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) + " " _
            & "AND r.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
            & "AND r.req_en_id IN (SELECT user_en_id FROM tconfuserentity WHERE userid = " + master_new.ClsVar.sUserID.ToString + ") " _
& "  	GROUP BY  " _
& "  	  r.wf_ref_oid, " _
& "  	  r.req_code, " _
& "  	  f.wf_start_dt, " _
& "  	  l.wf_last_dt, " _
& "  	  a.max_aprv_date, " _
& "  	  p.po_add_date, " _
& "  	  r.req_add_date, " _
& "  	  r.en_desc, " _
& "  	  r.req_date, " _
& "  	  r.req_trans_id, " _
& "  	  r.pt_code, " _
& "  	  r.pt_desc1, " _
& "  	  r.pt_desc2, " _
& "  	  r.reqd_qty, " _
& "  	  r.reqd_qty_processed, " _
& "  	  r.pt_um, " _
& "  	  r.code_name, " _
& "  	  r.po_add_date, " _
& "  	  r.po_date, " _
& "  	  r.po_code, " _
& "  	  r.po_wf_seq, " _
        & " Last_step.final_status "

            If gv_master.ActiveFilterString <> "" Then
                ssql = ssql & " and " & gv_master.ActiveFilterString.Replace("[", "").Replace("]", "").ToLower.Replace("like", "~~*")
            End If


            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

End Class
