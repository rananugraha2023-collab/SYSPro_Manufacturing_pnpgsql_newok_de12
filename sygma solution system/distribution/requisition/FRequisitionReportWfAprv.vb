Imports npgsql
Imports master_new.ModFunction

Public Class FRequisitionReportWfAprv
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FRequisitionReportWfAprv_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Code", "wf_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Req Date", "req_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty Process", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "WF Seq", "pr_wf_seq_count", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PR Approval Summary ", "approval_summary", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "PR Approval Time", "pr_process_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Req Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "PR Approval Lead Time (days)", "pr_process_duration", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "PR Cycle Time (days)", "pr_to_po_duration", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "PR Over Due (days)", "pr_to_po_duration", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view1, "PR to PO Lead Time (days) ", "total_pr_po_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view1, "PO Code", "po_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PO Approval Summary", "po_approval_summary", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "PO Total Lead Time (days) ", "pr_to_po_approval_duration", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Code", "wf_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Req Date", "req_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        add_column_copy(gv_view2, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Code", "wf_ref_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Req Date", "req_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view2, "Req Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "WF Seq", "pr_wf_seq_count", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (1st)", "wf_aprv_user_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (1st)", "wfs_desc_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (1st)", "wf_iscurrent_0", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (1st)", "wf_aprv_date_0", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (1st)", "due_1", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (2nd)", "wf_aprv_user_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (2nd)", "wfs_desc_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (2nd)", "wf_iscurrent_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (2nd)", "wf_aprv_date_1", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (2st)", "due_2", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (3rd)", "wf_aprv_user_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (3rd)", "wfs_desc_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (3rd)", "wf_iscurrent_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (3rd)", "wf_aprv_date_2", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (3rd)", "due_3", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (4th)", "wf_aprv_user_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (4th)", "wfs_desc_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (4th)", "wf_iscurrent_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (4th)", "wf_aprv_date_3", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (4th)", "due_4", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (5th)", "wf_aprv_user_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (5th)", "wfs_desc_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (5th)", "wf_iscurrent_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (5th)", "wf_aprv_date_4", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (5th)", "due_5", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (6th)", "wf_aprv_user_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (6th)", "wfs_desc_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (6th)", "wf_iscurrent_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (6th)", "wf_aprv_date_5", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (6th)", "due_6", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_master, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "User Approval (7th)", "wf_aprv_user_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Status (7th)", "wfs_desc_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Is Current (7th)", "wf_iscurrent_6", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Aprroval Date (7th)", "wf_aprv_date_6", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "Due (7th)", "due_7", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "PR Approval Lead Time (days)", "pr_process_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "PR Cycle Time (days)", "pr_to_po_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "PR Over Due (days)", "pr_to_po_duration", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_view2, "PR Total Lead Time (days) ", "total_pr_po_duration", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "PO Date", "po_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_view2, "PO Code", "po_code", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "WF Seq", "po_wf_seq_count", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Description", "po_wfs_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Workflow Date", "wf_dt", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view2, "PO Approval Summary", "po_approval_summary", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view2, "PO Total Lead Time (days) ", "pr_to_po_approval_duration", DevExpress.Utils.HorzAlignment.Default)

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
                            .SQL = "SELECT " _
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

                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")

                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            

                            .SQL = "SELECT " _
                                & "	r.wf_ref_oid, " _
                                & "	r.en_desc, " _
                                & "	r.req_code AS wf_ref_code, " _
                                & "	r.req_date, " _
                                & "	r.req_add_date, " _
                                & "	r.req_trans_id, " _
                                & "	f.wf_start_dt, " _
                                & "	l.wf_last_dt, " _
                                & "	COUNT(r.pr_wf_seq) AS pr_wf_seq_count, " _
                                & "	CASE " _
                                & "	WHEN last_step.final_status = 'Waiting' THEN 'Waiting' " _
                                & "	WHEN last_step.final_status = 'Pending' THEN 'Pending' " _
                                & "	ELSE MAX(r.pr_wfs_desc) " _
                                & "	END AS wfs_desc, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END) AS wf_aprv_date_0, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END), r.req_add_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END), r.req_add_date))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END), r.req_add_date))::text, 2, '0') || ' menit' AS due_0, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_user_id END) AS wf_aprv_user_0, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_iscurrent END) AS wf_iscurrent_0, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.pr_wfs_desc END) AS pr_wfs_desc_0, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END) AS wf_aprv_date_1, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '0' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_1, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_user_id END) AS wf_aprv_user_1, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_iscurrent END) AS wf_iscurrent_1, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.pr_wfs_desc END) AS pr_wfs_desc_1, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END) AS wf_aprv_date_2, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '1' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_2, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_user_id END) AS wf_aprv_user_2, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_iscurrent END) AS wf_iscurrent_2, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.pr_wfs_desc END) AS pr_wfs_desc_2, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END) AS wf_aprv_date_3, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '2' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_3, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_user_id END) AS wf_aprv_user_3, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_iscurrent END) AS wf_iscurrent_3, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.pr_wfs_desc END) AS pr_wfs_desc_3, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END) AS wf_aprv_date_4, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '3' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_4, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_user_id END) AS wf_aprv_user_4, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_iscurrent END) AS wf_iscurrent_4, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.pr_wfs_desc END) AS pr_wfs_desc_5, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END) AS wf_aprv_date_5, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '4' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_5, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_user_id END) AS wf_aprv_user_5, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_iscurrent END) AS wf_iscurrent_5, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.pr_wfs_desc END) AS pr_wfs_desc_5, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END) AS wf_aprv_date_6, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '5' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_6, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_user_id END) AS wf_aprv_user_6, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_iscurrent END) AS wf_iscurrent_6, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.pr_wfs_desc END) AS pr_wfs_desc_6, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_aprv_date END) AS wf_aprv_date_7, " _
                                & "	EXTRACT(DAY FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END))) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END)))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_aprv_date END), MAX(CASE WHEN r.pr_wf_seq = '6' THEN r.wf_aprv_date END)))::text, 2, '0') || ' menit' AS due_7, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_user_id END) AS wf_aprv_user_7, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.wf_iscurrent END) AS wf_iscurrent_7, " _
                                & "	MAX(CASE WHEN r.pr_wf_seq = '7' THEN r.pr_wfs_desc END) AS pr_wfs_desc_7, " _
                                & "	r.pt_code, " _
                                & "	r.pt_desc1, " _
                                & "	r.pt_desc2, " _
                                & "	r.reqd_qty, " _
                                & "	r.reqd_qty_processed, " _
                                & "	r.pt_um, " _
                                & "	r.code_name, " _
                                & "	EXTRACT(DAY FROM AGE(l.wf_last_dt, r.req_add_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(l.wf_last_dt, r.req_add_date))::text,2,'0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(l.wf_last_dt, r.req_add_date))::text, 2, '0') || ' menit' AS pr_process_duration, " _
                                & "	CASE " _
                                & "	WHEN p.po_add_date IS NOT NULL AND a.max_aprv_date IS NOT NULL THEN EXTRACT(DAY FROM AGE(p.po_add_date,a.max_aprv_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date,a.max_aprv_date))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date,a.max_aprv_date))::text, 2, '0') || ' menit' " _
                                & "	ELSE NULL " _
                                & "	END AS pr_to_po_duration, " _
                                & "	CASE " _
                                & "	WHEN p.po_add_date IS NOT NULL THEN EXTRACT(DAY FROM AGE(p.po_add_date, r.req_add_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(p.po_add_date, r.req_add_date))::text, 2, '0') || ' menit' " _
                                & "	ELSE NULL " _
                                & "	END AS total_pr_po_duration, " _
                                & "	r.po_code, " _
                                & "	r.po_add_date, " _
                                & "	r.po_date, " _
                                & "	COUNT(r.po_wf_seq) AS po_wf_seq_count, " _
                                & "	MAX(r.po_wfs_desc) AS po_wfs_desc, " _
                                & "	STRING_AGG(DISTINCT CASE " _
                                & "	WHEN r.po_wf_iscurrent = 'N' AND r.po_wf_wf_aprv_date IS NOT NULL THEN 'Approved by ' || r.po_wf_user_id || ' ' || TO_CHAR( r.po_wf_wf_aprv_date, 'DD/MM/YYYY HH24:MI:SS') " _
                                & "	WHEN r.po_rn = 1 AND r.po_wf_iscurrent = 'Y' THEN 'Waiting for Approve by ' || r.po_wf_user_id " _
                                & "	ELSE NULL " _
                                & "	END, " _
                                & "	' - ') AS po_approval_summary, " _
                                & "	CASE " _
                                & "	WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), p.po_add_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), " _
                                & "	p.po_add_date))::text, 2, '0') || ' jam ' || LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), " _
                                & "	p.po_add_date))::text, 2, '0') || ' menit' " _
                                & "	ELSE NULL " _
                                & "	END AS po_approval_duration, " _
                                & "	CASE " _
                                & "	WHEN p.po_add_date IS NOT NULL AND MAX(r.po_wf_wf_aprv_date) IS NOT NULL THEN EXTRACT(DAY FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date)) || ' hari ' || " _
                                & "	LPAD(EXTRACT(HOUR FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' jam ' || " _
                                & "	LPAD(EXTRACT(MINUTE FROM AGE(MAX(r.po_wf_wf_aprv_date), r.req_add_date))::text, 2, '0') || ' menit' " _
                                & "	ELSE NULL " _
                                & "	END AS pr_to_po_approval_duration " _
                                & "	FROM ( " _
                                & "	SELECT pr_wf.wf_ref_oid, " _
                                & "	req.req_en_id, " _
                                & "	en.en_desc, " _
                                & "	req.req_code, " _
                                & "	req.req_add_date, " _
                                & "	req.req_date, " _
                                & "	req.req_trans_id, " _
                                & "	req.req_rmks, " _
                                & "	reqd.reqd_pt_id, " _
                                & "	pt.pt_code, " _
                                & "	pt.pt_desc1, " _
                                & "	pt.pt_desc2, " _
                                & "	pt.pt_um, " _
                                & "	code.code_name, " _
                                & "	reqd.reqd_qty, " _
                                & "	reqd.reqd_qty_processed, " _
                                & "	reqd.reqd_oid, " _
                                & "	pod.pod_reqd_oid, " _
                                & "	po.po_code, " _
                                & "	po.po_date, " _
                                & "	po.po_add_date, " _
                                & "	pr_wf.wf_seq AS pr_wf_seq, " _
                                & "	pr_wf.wf_user_id, " _
                                & "	pr_wf.wf_aprv_user, " _
                                & "	pr_wf.wf_aprv_date, " _
                                & "	pr_wf.wf_iscurrent, " _
                                & "	pr_wf.wf_tran_id, " _
                                & "	pr_wf.wf_ref_code, " _
                                & "	pr_wf.wf_dt, " _
                                & "	pr_wfs.wfs_desc AS pr_wfs_desc, " _
                                & "	ROW_NUMBER() OVER(PARTITION BY pr_wf.wf_ref_oid ORDER BY pr_wf.wf_seq) AS pr_rn, " _
                                & "	po_wf.wf_tran_id AS po_wf_tran_id, " _
                                & "	po_wf.wf_ref_oid AS po_wf_ref_oid, " _
                                & "	po_wf.wf_ref_code AS po_wf_ref_code, " _
                                & "	po_wf.wf_seq AS po_wf_seq, " _
                                & "	po_wf.wf_user_id AS po_wf_user_id, " _
                                & "	po_wf.wf_iscurrent AS po_wf_iscurrent, " _
                                & "	po_wf.wf_dt AS po_wf_wf_dt, " _
                                & "	po_wf.wf_aprv_user AS po_wf_aprv_user, " _
                                & "	po_wf.wf_aprv_date AS po_wf_wf_aprv_date, " _
                                & "	po_wfs.wfs_desc AS po_wfs_desc, " _
                                & "	ROW_NUMBER() OVER(PARTITION BY po_wf.wf_ref_oid ORDER BY po_wf.wf_seq) AS po_rn " _
                                & "	FROM wf_mstr pr_wf " _
                                & "	INNER JOIN public.req_mstr req ON pr_wf.wf_ref_oid = req.req_oid " _
                                & "	INNER JOIN public.en_mstr en ON req.req_en_id = en.en_id " _
                                & "	INNER JOIN public.wfs_status pr_wfs ON pr_wf.wf_wfs_id = pr_wfs.wfs_id " _
                                & "	INNER JOIN public.reqd_det reqd ON req.req_oid = reqd.reqd_req_oid " _
                                & "	INNER JOIN public.pt_mstr pt ON reqd.reqd_pt_id = pt.pt_id " _
                                & "	LEFT OUTER JOIN public.pod_det pod ON reqd.reqd_oid = pod.pod_reqd_oid " _
                                & "	LEFT OUTER JOIN public.po_mstr po ON pod.pod_po_oid = po.po_oid " _
                                & "	LEFT OUTER JOIN public.wf_mstr po_wf ON po.po_oid = po_wf.wf_ref_oid " _
                                & "	LEFT OUTER JOIN public.wfs_status po_wfs ON po_wf.wf_wfs_id = po_wfs.wfs_id " _
                                & "	INNER JOIN public.code_mstr code ON pt.pt_um = code.code_id " _
                                & "	) r " _
                                & "	LEFT JOIN  " _
                                & "	( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	MIN(wf_seq) AS waiting_seq " _
                                & "	FROM wf_mstr " _
                                & "	WHERE wf_iscurrent = 'Y' " _
                                & "	GROUP BY wf_ref_oid " _
                                & "	) w ON r.wf_ref_oid = w.wf_ref_oid " _
                                & "	LEFT JOIN  " _
                                & "	( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	MIN(wf_dt) AS wf_start_dt " _
                                & "	FROM wf_mstr " _
                                & "	GROUP BY wf_ref_oid " _
                                & "	) f ON r.wf_ref_oid = f.wf_ref_oid " _
                                & "	LEFT JOIN " _
                                & "	( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	MAX(wf_dt) AS wf_last_dt " _
                                & "	FROM wf_mstr " _
                                & "	GROUP BY wf_ref_oid " _
                                & "	) l ON r.wf_ref_oid = l.wf_ref_oid " _
                                & "	LEFT JOIN " _
                                & "	( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	MAX(wf_aprv_date) AS max_aprv_date " _
                                & "	FROM wf_mstr " _
                                & "	WHERE wf_aprv_date IS NOT NULL " _
                                & "	GROUP BY wf_ref_oid " _
                                & "	) a ON r.wf_ref_oid = a.wf_ref_oid " _
                                & "	LEFT JOIN " _
                                & "	( " _
                                & "	SELECT pod_reqd_oid, " _
                                & "	MIN(po_add_date) AS po_add_date " _
                                & "	FROM pod_det " _
                                & "	INNER JOIN po_mstr ON pod_det.pod_po_oid = po_mstr.po_oid " _
                                & "	GROUP BY pod_reqd_oid " _
                                & "	) p ON r.reqd_oid = p.pod_reqd_oid " _
                                & "	LEFT JOIN " _
                                & "	( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	wf_seq, " _
                                & "	CASE " _
                                & "	WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'Y' THEN 'Waiting' " _
                                & "	WHEN wf_aprv_date IS NULL AND wf_iscurrent = 'N' THEN 'Pending' " _
                                & "	ELSE NULL " _
                                & "	END AS final_status " _
                                & "	FROM wf_mstr " _
                                & "	WHERE (wf_ref_oid, " _
                                & "	wf_seq) IN ( " _
                                & "	SELECT wf_ref_oid, " _
                                & "	MAX(wf_seq) " _
                                & "	FROM wf_mstr " _
                                & "	GROUP BY wf_ref_oid " _
                                & "  ) " _
                                & "  ) last_step ON r.wf_ref_oid = last_step.wf_ref_oid AND r.pr_wf_seq = last_step.wf_seq " _
                                & "WHERE r.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) + " " _
                                & "AND r.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + " " _
                                & "AND r.req_en_id IN (SELECT user_en_id FROM tconfuserentity WHERE userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                                & "	GROUP BY r.wf_ref_oid, " _
                                & "	r.req_code, " _
                                & "	f.wf_start_dt, " _
                                & "	l.wf_last_dt, " _
                                & "	a.max_aprv_date, " _
                                & "	p.po_add_date, " _
                                & "	r.req_add_date, " _
                                & "	r.en_desc, " _
                                & "	r.req_date, " _
                                & "	r.req_trans_id, " _
                                & "	r.pt_code, " _
                                & "	r.pt_desc1, " _
                                & "	r.pt_desc2, " _
                                & "	r.reqd_qty, " _
                                & "	r.reqd_qty_processed, " _
                                & "	r.pt_um, " _
                                & "	r.code_name, " _
                                & "	r.po_add_date, " _
                                & "	r.po_date, " _
                                & "	r.po_code, " _
                                & "	r.po_wf_seq, " _
                                & "	Last_step.final_status"


                            .InitializeCommand()
                            .FillDataSet(ds, "view2")
                            gc_view2.DataSource = ds.Tables("view2")
                        End If

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Private Sub xtc_master_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles xtc_master.TabIndexChanged
        If xtc_master.SelectedTabPageIndex = 0 Then
            pr_txttglawal.Enabled = True
            pr_txttglakhir.Enabled = True
        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
            pr_txttglawal.Enabled = False
            pr_txttglakhir.Enabled = False
        End If
    End Sub
End Class
