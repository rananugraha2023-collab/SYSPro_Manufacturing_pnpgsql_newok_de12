Imports npgsql
Imports master_new.ModFunction

Public Class FStockControlReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FStockControlReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Control Code", "opname_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Control Desc", "opname_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Control Date", "opname_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view1, "Desc", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "opnamed_qty_stock", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Qty Processed", "opnamed_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Closed", "opname_close", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Processed", "opname_process", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "User Create", "opnamed_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Create", "opnamed_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_view1, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_view1, "reqd_req_oid", False)
        'add_column_copy(gv_view1, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Remarks Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view1, "End User Detail", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view1, "Need Date Detail", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "Due Date Detail", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view1, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view1, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view2, "Entity Header", "en_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "PR Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Order Date Header", "req_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Need Date Header", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Due Date Header", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Remarks Header", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Sub Account Header", "sb_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Cost Center Header", "cc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Site Header", "si_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Project Header", "pjc_desc_header", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        'add_column_copy(gv_view2, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        'add_column_copy(gv_view2, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column(gv_view2, "reqd_req_oid", False)
        'add_column_copy(gv_view2, "Entity Detail", "en_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Site Detail", "si_desc_detail", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Remarks Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "End User Detail", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_view2, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Outstanding", "reqd_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)

        'add_column_copy(gv_view2, "Need Date Detail", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "Due Date Detail", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        'add_column_copy(gv_view2, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_view2, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)
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
                            '.SQL = "SELECT  " _
                            '        & "  en_mstr_header.en_desc as en_desc_header, " _
                            '        & "  a.req_oid, " _
                            '        & "  a.req_dom_id, " _
                            '        & "  a.req_en_id, " _
                            '        & "  a.req_upd_date, " _
                            '        & "  a.req_upd_by, " _
                            '        & "  a.req_add_date, " _
                            '        & "  a.req_add_by, " _
                            '        & "  a.req_code, " _
                            '        & "  a.req_ptnr_id, " _
                            '        & "  a.req_cmaddr_id, " _
                            '        & "  a.req_date, " _
                            '        & "  a.req_need_date, " _
                            '        & "  a.req_due_date, " _
                            '        & "  a.req_requested, " _
                            '        & "  a.req_end_user, " _
                            '        & "  a.req_rmks, " _
                            '        & "  a.req_sb_id, " _
                            '        & "  a.req_cc_id, " _
                            '        & "  a.req_si_id, " _
                            '        & "  a.req_type, " _
                            '        & "  a.req_pjc_id, " _
                            '        & "  a.req_close_date, " _
                            '        & "  a.req_total, " _
                            '        & "  a.req_tran_id, " _
                            '        & "  a.req_trans_id, " _
                            '        & "  a.req_trans_rmks, " _
                            '        & "  a.req_current_route, " _
                            '        & "  a.req_next_route, " _
                            '        & "  a.req_dt, " _
                            '        & "  c.ptnr_name, " _
                            '        & "  d.pjc_code, d.pjc_desc as pjc_desc_header, " _
                            '        & "  f.si_desc as si_desc_header, " _
                            '        & "  h.sb_desc as sb_desc_header, " _
                            '        & "  i.cc_desc as cc_desc_header, " _
                            '        & "  j.cmaddr_name, " _
                            '        & "  k.tran_name, " _
                            '        & "  g.reqd_oid, " _
                            '        & "  g.reqd_dom_id, " _
                            '        & "  g.reqd_en_id, " _
                            '        & "  g.reqd_add_by, " _
                            '        & "  g.reqd_add_date, " _
                            '        & "  g.reqd_upd_by, " _
                            '        & "  g.reqd_upd_date, " _
                            '        & "  g.reqd_req_oid, " _
                            '        & "  g.reqd_seq, " _
                            '        & "  g.reqd_related_oid, " _
                            '        & "  g.reqd_ptnr_id, " _
                            '        & "  g.reqd_si_id, " _
                            '        & "  g.reqd_pt_id, " _
                            '        & "  g.reqd_rmks, " _
                            '        & "  g.reqd_end_user, " _
                            '        & "  g.reqd_qty, " _
                            '        & "  g.reqd_qty_processed, " _
                            '        & "  g.reqd_qty_completed, " _
                            '        & "  g.reqd_um, " _
                            '        & "  g.reqd_cost, " _
                            '        & "  g.reqd_disc, " _
                            '        & "  g.reqd_need_date, " _
                            '        & "  g.reqd_due_date, " _
                            '        & "  g.reqd_um_conv, " _
                            '        & "  g.reqd_qty_real, " _
                            '        & "  g.reqd_pt_class, " _
                            '        & "  case when (reqd_qty - coalesce(reqd_qty_processed,0)) >0 then 'D' else 'C' end  as reqd_status, " _
                            '        & "  g.reqd_dt, " _
                            '        & "  g.reqd_related_type, " _
                            '        & "  g.reqd_pt_desc1, " _
                            '        & "  g.reqd_pt_desc2, " _
                            '        & "  g.reqd_loc_id, " _
                            '        & "  g.reqd_boqs_oid, o.code_name as um_name, " _
                            '        & " l.en_desc as en_desc_detail, " _
                            '        & " m.si_desc as si_desc_detail, " _
                            '        & " n.pt_code, n.pt_desc1, n.pt_desc2, po_date, po_code " _
                            '        & "FROM " _
                            '        & "  public.req_mstr a " _
                            '        & "  INNER JOIN public.en_mstr en_mstr_header ON (a.req_en_id = en_mstr_header.en_id) " _
                            '        & "  INNER JOIN public.ptnr_mstr c ON (a.req_ptnr_id = c.ptnr_id) " _
                            '        & "  INNER JOIN public.cmaddr_mstr j ON (a.req_cmaddr_id = j.cmaddr_id) " _
                            '        & "  INNER JOIN public.sb_mstr h ON (a.req_sb_id = h.sb_id) " _
                            '        & "  INNER JOIN public.cc_mstr i ON (a.req_cc_id = i.cc_id) " _
                            '        & "  INNER JOIN public.si_mstr f ON (a.req_si_id = f.si_id) " _
                            '        & "  INNER JOIN public.pjc_mstr d ON (a.req_pjc_id = d.pjc_id) " _
                            '        & "  LEFT OUTER JOIN public.tran_mstr k ON (a.req_tran_id = k.tran_id) " _
                            '        & "  INNER JOIN public.reqd_det g ON (a.req_oid = g.reqd_req_oid) " _
                            '        & "  INNER JOIN public.en_mstr l ON (g.reqd_en_id = l.en_id) " _
                            '        & "  INNER JOIN public.si_mstr m ON (g.reqd_si_id = m.si_id) " _
                            '        & "  INNER JOIN public.pt_mstr n ON (g.reqd_pt_id = n.pt_id) " _
                            '        & "  INNER JOIN public.code_mstr o ON (g.reqd_um = o.code_id) " _
                            '        & "  LEFT OUTER JOIN public.pod_det p ON (g.reqd_oid = p.pod_reqd_oid) " _
                            '        & "  LEFT OUTER JOIN public.po_mstr q ON (p.pod_po_oid = q.po_oid) " _
                            '        & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '        & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '        & " and req_en_id in (select user_en_id from tconfuserentity " _
                            '        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

                            .SQL = "SELECT  " _
                                    & "  public.opname_mstr.opname_en_id, " _
                                    & "  public.en_mstr.en_desc, " _
                                    & "  public.opname_mstr.opname_code, " _
                                    & "  public.opname_mstr.opname_desc, " _
                                    & "  public.opname_mstr.opname_date, " _
                                    & "  public.opnamed_det.opnamed_pt_id, " _
                                    & "  public.pt_mstr.pt_code, " _
                                    & "  public.pt_mstr.pt_desc1, " _
                                    & "  public.opnamed_det.opnamed_qty_stock, " _
                                    & "  public.opnamed_det.opnamed_qty, " _
                                    & "  public.opnamed_det.opnamed_loc_id, " _
                                    & "  public.loc_mstr.loc_desc, " _
                                    & "  public.opnamed_det.opnamed_add_by, " _
                                    & "  public.opnamed_det.opnamed_add_date, " _
                                    & "  public.opname_mstr.opname_close, " _
                                    & "  public.opname_mstr.opname_process " _
                                    & "FROM " _
                                    & "  public.opname_mstr " _
                                    & "  INNER JOIN public.opnamed_det ON (public.opname_mstr.opname_code = public.opnamed_det.opnamed_opname_code) " _
                                    & "  INNER JOIN public.en_mstr ON (public.opname_mstr.opname_en_id = public.en_mstr.en_id) " _
                                    & "  INNER JOIN public.pt_mstr ON (public.opnamed_det.opnamed_pt_id = public.pt_mstr.pt_id) " _
                                    & "  INNER JOIN public.loc_mstr ON (public.opnamed_det.opnamed_loc_id = public.loc_mstr.loc_id)"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
                        ElseIf xtc_master.SelectedTabPageIndex = 1 Then
                            '.SQL = "SELECT  " _
                            '        & "  en_mstr_header.en_desc as en_desc_header, " _
                            '        & "  a.req_oid, " _
                            '        & "  a.req_dom_id, " _
                            '        & "  a.req_en_id, " _
                            '        & "  a.req_upd_date, " _
                            '        & "  a.req_upd_by, " _
                            '        & "  a.req_add_date, " _
                            '        & "  a.req_add_by, " _
                            '        & "  a.req_code, " _
                            '        & "  a.req_ptnr_id, " _
                            '        & "  a.req_cmaddr_id, " _
                            '        & "  a.req_date, " _
                            '        & "  a.req_need_date, " _
                            '        & "  a.req_due_date, " _
                            '        & "  a.req_requested, " _
                            '        & "  a.req_end_user, " _
                            '        & "  a.req_rmks, " _
                            '        & "  a.req_sb_id, " _
                            '        & "  a.req_cc_id, " _
                            '        & "  a.req_si_id, " _
                            '        & "  a.req_type, " _
                            '        & "  a.req_pjc_id, " _
                            '        & "  a.req_close_date, " _
                            '        & "  a.req_total, " _
                            '        & "  a.req_tran_id, " _
                            '        & "  a.req_trans_id, " _
                            '        & "  a.req_trans_rmks, " _
                            '        & "  a.req_current_route, " _
                            '        & "  a.req_next_route, " _
                            '        & "  a.req_dt, " _
                            '        & "  c.ptnr_name, " _
                            '        & "  d.pjc_code, d.pjc_desc as pjc_desc_header, " _
                            '        & "  f.si_desc as si_desc_header, " _
                            '        & "  h.sb_desc as sb_desc_header, " _
                            '        & "  i.cc_desc as cc_desc_header, " _
                            '        & "  j.cmaddr_name, " _
                            '        & "  k.tran_name, " _
                            '        & "  g.reqd_oid, " _
                            '        & "  g.reqd_dom_id, " _
                            '        & "  g.reqd_en_id, " _
                            '        & "  g.reqd_add_by, " _
                            '        & "  g.reqd_add_date, " _
                            '        & "  g.reqd_upd_by, " _
                            '        & "  g.reqd_upd_date, " _
                            '        & "  g.reqd_req_oid, " _
                            '        & "  g.reqd_seq, " _
                            '        & "  g.reqd_related_oid, " _
                            '        & "  g.reqd_ptnr_id, " _
                            '        & "  g.reqd_si_id, " _
                            '        & "  g.reqd_pt_id, " _
                            '        & "  g.reqd_rmks, " _
                            '        & "  g.reqd_end_user, " _
                            '        & "  g.reqd_qty, " _
                            '        & "  g.reqd_qty_processed, " _
                            '        & "  g.reqd_qty_completed, " _
                            '        & "  g.reqd_um, " _
                            '        & "  g.reqd_cost, " _
                            '        & "  g.reqd_disc, " _
                            '        & "  g.reqd_need_date, " _
                            '        & "  g.reqd_due_date, " _
                            '        & "  g.reqd_um_conv, " _
                            '        & "  g.reqd_qty_real, " _
                            '        & "  g.reqd_pt_class, " _
                            '        & "  case when (reqd_qty - coalesce(reqd_qty_processed,0)) >0 then 'D' else 'C' end  as reqd_status, " _
                            '        & "  g.reqd_dt, " _
                            '        & "  g.reqd_related_type, " _
                            '        & "  g.reqd_pt_desc1, " _
                            '        & "  g.reqd_pt_desc2, " _
                            '        & "  g.reqd_loc_id, " _
                            '        & "  g.reqd_boqs_oid, o.code_name as um_name, " _
                            '        & " l.en_desc as en_desc_detail, " _
                            '        & " m.si_desc as si_desc_detail, " _
                            '        & " n.pt_code, n.pt_desc1, n.pt_desc2, " _
                            '        & " reqd_qty - coalesce(reqd_qty_processed,0) as reqd_qty_outstanding " _
                            '        & "FROM " _
                            '        & "  public.req_mstr a " _
                            '        & "  INNER JOIN public.en_mstr en_mstr_header ON (a.req_en_id = en_mstr_header.en_id) " _
                            '        & "  INNER JOIN public.ptnr_mstr c ON (a.req_ptnr_id = c.ptnr_id) " _
                            '        & "  INNER JOIN public.cmaddr_mstr j ON (a.req_cmaddr_id = j.cmaddr_id) " _
                            '        & "  INNER JOIN public.sb_mstr h ON (a.req_sb_id = h.sb_id) " _
                            '        & "  INNER JOIN public.cc_mstr i ON (a.req_cc_id = i.cc_id) " _
                            '        & "  INNER JOIN public.si_mstr f ON (a.req_si_id = f.si_id) " _
                            '        & "  INNER JOIN public.pjc_mstr d ON (a.req_pjc_id = d.pjc_id) " _
                            '        & "  LEFT OUTER JOIN public.tran_mstr k ON (a.req_tran_id = k.tran_id) " _
                            '        & "  INNER JOIN public.reqd_det g ON (a.req_oid = g.reqd_req_oid) " _
                            '        & "  INNER JOIN public.en_mstr l ON (g.reqd_en_id = l.en_id) " _
                            '        & "  INNER JOIN public.si_mstr m ON (g.reqd_si_id = m.si_id) " _
                            '        & "  INNER JOIN public.pt_mstr n ON (g.reqd_pt_id = n.pt_id) " _
                            '        & "  INNER JOIN public.code_mstr o ON (g.reqd_um = o.code_id) " _
                            '        & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                            '        & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                            '        & " AND coalesce(reqd_qty_processed,0) < reqd_qty " _
                            '        & " and req_en_id in (select user_en_id from tconfuserentity " _
                            '        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .SQL = "SELECT  " _
                                    & "  public.opname_mstr.opname_en_id, " _
                                    & "  public.en_mstr.en_desc, " _
                                    & "  public.opname_mstr.opname_code, " _
                                    & "  public.opname_mstr.opname_desc, " _
                                    & "  public.opname_mstr.opname_date, " _
                                    & "  public.opnamed_det.opnamed_pt_id, " _
                                    & "  public.pt_mstr.pt_code, " _
                                    & "  public.pt_mstr.pt_desc1, " _
                                    & "  public.opnamed_det.opnamed_qty_stock, " _
                                    & "  public.opnamed_det.opnamed_qty, " _
                                    & "  public.opnamed_det.opnamed_loc_id, " _
                                    & "  public.loc_mstr.loc_desc, " _
                                    & "  public.opnamed_det.opnamed_add_by, " _
                                    & "  public.opnamed_det.opnamed_add_date, " _
                                    & "  public.opname_mstr.opname_close, " _
                                    & "  public.opname_mstr.opname_process " _
                                    & "FROM " _
                                    & "  public.opname_mstr " _
                                    & "  INNER JOIN public.opnamed_det ON (public.opname_mstr.opname_code = public.opnamed_det.opnamed_opname_code) " _
                                    & "  INNER JOIN public.en_mstr ON (public.opname_mstr.opname_en_id = public.en_mstr.en_id) " _
                                    & "  INNER JOIN public.pt_mstr ON (public.opnamed_det.opnamed_pt_id = public.pt_mstr.pt_id) " _
                                    & "  INNER JOIN public.loc_mstr ON (public.opnamed_det.opnamed_loc_id = public.loc_mstr.loc_id)"
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
