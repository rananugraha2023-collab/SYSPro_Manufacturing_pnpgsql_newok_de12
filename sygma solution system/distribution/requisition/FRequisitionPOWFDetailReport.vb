Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports System.IO
Imports master_new.PGSqlConn
Imports System.Text

Public Class FRequisitionPOWFDetailReport
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

    Private Sub FRequisitionPOWFDetailReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        get_sequel = "WITH latest_pr_wf AS ( " _
                    & "  SELECT DISTINCT ON (wf_ref_code) * " _
                    & "  FROM public.wf_mstr " _
                    & "  WHERE wf_ref_code LIKE 'PR%' " _
                    & "  ORDER BY wf_ref_code, wf_seq DESC " _
                    & "), " _
                    & "latest_po_wf AS ( " _
                    & "  SELECT DISTINCT ON (wf_ref_code) * " _
                    & "  FROM public.wf_mstr " _
                    & "  WHERE wf_ref_code LIKE 'PO%' " _
                    & "  ORDER BY wf_ref_code, wf_seq DESC " _
                    & "), " _
                    & "approval_sequence_detailed AS ( " _
                    & "  SELECT " _
                    & "    wf_ref_code, " _
                    & "    wf_aprv_user, " _
                    & "    wf_aprv_date, " _
                    & "    wf_seq, " _
                    & "    LAG(wf_aprv_date) OVER (PARTITION BY wf_ref_code ORDER BY wf_seq) AS previous_approval_date, " _
                    & "    (wf_aprv_date - LAG(wf_aprv_date) OVER (PARTITION BY wf_ref_code ORDER BY wf_seq)) AS approval_time " _
                    & "  FROM public.wf_mstr " _
                    & "  WHERE wf_aprv_user IS NOT NULL AND wf_aprv_date IS NOT NULL " _
                    & "), " _
                    & "approval_sequence_final AS ( " _
                    & "  SELECT " _
                    & "    wf_ref_code, " _
                    & "    string_agg( " _
                    & "      wf_aprv_user || ' (' || to_char(wf_aprv_date, 'YYYY-MM-DD') || ') ' || approval_time, " _
                    & "      ' || ' ORDER BY wf_seq " _
                    & "    ) AS sequence_str " _
                    & "  FROM approval_sequence_detailed " _
                    & "  GROUP BY wf_ref_code " _
                    & ") " _
                    & " " _
                    & "SELECT  " _
                    & "  pr.wf_en_id, " _
                    & "  en.en_desc, " _
                    & "  pr.wf_ref_oid, " _
                    & "  pr.wf_ref_desc, " _
                    & "  pr.wf_ref_code, " _
                    & "  req.req_add_date, " _
                    & "  req.req_date, " _
                    & "  pr.wf_oid AS pr_wf_oid, " _
                    & "  pr.wf_seq AS pr_wf_seq, " _
                    & "  pr.wf_desc AS pr_wf_desc, " _
                    & "  pr.wf_aprv_user AS pr_aprv_user, " _
                    & "  pr.wf_aprv_date AS pr_aprv_date, " _
                    & "  pr.wf_aprv_date - req.req_add_date AS pr_duration, " _
                    & "  rd.reqd_seq, " _
                    & "  rd.reqd_pt_id, " _
                    & "  pt.pt_code, " _
                    & "  pt.pt_desc1, " _
                    & "  rd.reqd_oid, " _
                    & "  pod.pod_reqd_oid, " _
                    & "  po.po_code, " _
                    & "  po.po_add_date, " _
                    & "  powf.wf_oid AS po_wf_oid, " _
                    & "  powf.wf_seq AS po_wf_seq, " _
                    & "  powf.wf_desc AS po_wf_desc, " _
                    & "  powf.wf_aprv_user, " _
                    & "  powf.wf_aprv_date AS po_aprv_date, " _
                    & "  powf.wf_aprv_date - po.po_add_date AS po_duration, " _
                    & "  (pr.wf_aprv_date - req.req_add_date) +  " _
                    & "  (powf.wf_aprv_date - po.po_add_date) AS pr_po_duration, " _
                    & "  seq_pr.sequence_str AS approval_sequence_pr, " _
                    & "  seq_po.sequence_str AS approval_sequence_po, " _
                    & "  CASE  " _
                    & "    WHEN pr.wf_oid IS NOT NULL AND powf.wf_oid IS NOT NULL THEN 'PR-PO' " _
                    & "    WHEN pr.wf_oid IS NOT NULL AND powf.wf_oid IS NULL THEN 'PR' " _
                    & "    WHEN pr.wf_oid IS NULL AND powf.wf_oid IS NOT NULL THEN 'PO-MANUAL' " _
                    & "    ELSE 'UNKNOWN' " _
                    & "  END AS jenis_transaksi " _
                    & "FROM latest_pr_wf pr " _
                    & "  INNER JOIN public.en_mstr en ON pr.wf_en_id = en.en_id " _
                    & "  INNER JOIN public.req_mstr req ON pr.wf_ref_code = req.req_code " _
                    & "  INNER JOIN public.reqd_det rd ON req.req_oid = rd.reqd_req_oid " _
                    & "  INNER JOIN public.pt_mstr pt ON rd.reqd_pt_id = pt.pt_id " _
                    & "  LEFT JOIN public.pod_det pod ON rd.reqd_oid = pod.pod_reqd_oid " _
                    & "  LEFT JOIN public.po_mstr po ON pod.pod_po_oid = po.po_oid " _
                    & "  LEFT JOIN latest_po_wf powf ON po.po_code = powf.wf_ref_code " _
                    & "  LEFT JOIN approval_sequence_final seq_pr ON pr.wf_ref_code = seq_pr.wf_ref_code " _
                    & "  LEFT JOIN approval_sequence_final seq_po ON po.po_code = seq_po.wf_ref_code " _
                    & "UNION ALL " _
                    & "SELECT  " _
                    & "  po_only.wf_en_id, " _
                    & "  en.en_desc, " _
                    & "  po_only.wf_ref_oid, " _
                    & "  po_only.wf_ref_desc, " _
                    & "  po_only.wf_ref_code, " _
                    & "  NULL, NULL, " _
                    & "  NULL, NULL, NULL, " _
                    & "  NULL, NULL, NULL, " _
                    & "  NULL, NULL, NULL, NULL, NULL, NULL, " _
                    & "  po.po_code, " _
                    & "  po.po_add_date, " _
                    & "  po_only.wf_oid, " _
                    & "  po_only.wf_seq, " _
                    & "  po_only.wf_desc, " _
                    & "  po_only.wf_aprv_user, " _
                    & "  po_only.wf_aprv_date, " _
                    & "  po_only.wf_aprv_date - po.po_add_date, " _
                    & "  NULL, " _
                    & "  NULL, " _
                    & "  seq_po.sequence_str AS approval_sequence_po, " _
                    & "  'PO-MANUAL' AS jenis_transaksi " _
                    & "FROM latest_po_wf po_only " _
                    & "  INNER JOIN public.en_mstr en ON po_only.wf_en_id = en.en_id " _
                    & "  INNER JOIN public.po_mstr po ON po_only.wf_ref_code = po.po_code " _
                    & "  LEFT JOIN public.pod_det pod ON pod.pod_po_oid = po.po_oid " _
                    & "  LEFT JOIN approval_sequence_final seq_po ON po.po_code = seq_po.wf_ref_code " _
                    & "WHERE NOT EXISTS ( " _
                    & "  SELECT 1 FROM public.pod_det pd WHERE pd.pod_po_oid = po.po_oid AND pd.pod_reqd_oid IS NOT NULL " _
                    & ") " _
                    & "ORDER BY wf_ref_code, reqd_seq"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT DISTINCT" _
        '    & "  public.reqd_det.reqd_oid, " _
        '    & "  public.reqd_det.reqd_dom_id, " _
        '    & "  public.reqd_det.reqd_en_id, " _
        '    & "  public.en_mstr.en_desc, " _
        '    & "  public.reqd_det.reqd_add_by, " _
        '    & "  public.reqd_det.reqd_add_date, " _
        '    & "  public.reqd_det.reqd_upd_by, " _
        '    & "  public.reqd_det.reqd_upd_date, " _
        '    & "  public.reqd_det.reqd_req_oid, " _
        '    & "  public.wf_mstr.wf_ref_oid, " _
        '    & "  public.reqd_det.reqd_seq, " _
        '    & "  public.reqd_det.reqd_related_oid, " _
        '    & "  req_mstr_relation.req_code as req_code_relation, " _
        '    & "  public.reqd_det.reqd_ptnr_id, " _
        '    & "  public.ptnr_mstr.ptnr_name, " _
        '    & "  public.reqd_det.reqd_si_id, " _
        '    & "  public.si_mstr.si_desc, " _
        '    & "  public.reqd_det.reqd_pt_id, " _
        '    & "  public.pt_mstr.pt_code, " _
        '    & "  public.pt_mstr.pt_desc1, " _
        '    & "  public.pt_mstr.pt_desc2, " _
        '    & "  public.reqd_det.reqd_rmks, " _
        '    & "  public.reqd_det.reqd_end_user, " _
        '    & "  public.reqd_det.reqd_qty, " _
        '    & "  public.reqd_det.reqd_qty_processed, " _
        '    & "  public.reqd_det.reqd_qty_completed, " _
        '    & "  public.reqd_det.reqd_um, " _
        '    & "  public.code_mstr.code_name, " _
        '    & "  public.reqd_det.reqd_cost, " _
        '    & "  public.reqd_det.reqd_disc, " _
        '    & "  public.reqd_det.reqd_need_date, " _
        '    & "  public.reqd_det.reqd_due_date, " _
        '    & "  public.reqd_det.reqd_um_conv, " _
        '    & "  public.reqd_det.reqd_qty_real, " _
        '    & "  public.reqd_det.reqd_pt_class, " _
        '    & "  public.reqd_det.reqd_status, " _
        '    & "  public.reqd_det.reqd_dt,  " _
        '    & "  public.reqd_det.reqd_dt, " _
        '    & "  public.reqd_det.reqd_pt_desc1, " _
        '    & "  public.reqd_det.reqd_pt_desc2,  public.reqd_det.reqd_boqs_oid, " _
        '    & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
        '    & "  FROM " _
        '    & "  public.reqd_det " _
        '    & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
        '    & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
        '    & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
        '    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
        '    & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
        '    & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
        '    & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid               " _
        '    & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
        '    & "  INNER JOIN public.wf_mstr ON (public.reqd_det.reqd_req_oid = public.wf_mstr.wf_ref_oid) " _
        '    & "  where public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        'load_data_detail(sql, gc_detail, "detail")


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
