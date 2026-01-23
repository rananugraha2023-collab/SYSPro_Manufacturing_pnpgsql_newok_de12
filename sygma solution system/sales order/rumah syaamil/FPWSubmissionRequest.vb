Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FPWSubmissionRequest
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pb_oid_mstr As String
    Dim mf As New master_new.ModFunction
    Public ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pbd_related_oid As String = ""
    'Public _pwsr_ptnr_id As Integer
    Dim _conf_value As String
    Dim _now As Date

    Private Sub FPWSubmissionRequest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_wsr_request")
        form_first_load()
        _now = func_coll.get_tanggal_sistem
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

        If _conf_value = "0" Then
            xtc_detail.TabPages(1).PageVisible = False
            xtc_detail.TabPages(3).PageVisible = False
        ElseIf _conf_value = "1" Then
            xtc_detail.TabPages(1).PageVisible = True
            xtc_detail.TabPages(3).PageVisible = True
        End If
        xtc_detail.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        pwsr_en_id.Properties.DataSource = dt_bantu
        pwsr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pwsr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pwsr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_all())
        pwsr_en_id_shipment.Properties.DataSource = dt_bantu
        pwsr_en_id_shipment.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pwsr_en_id_shipment.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pwsr_en_id_shipment.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pb_type())
        pwsr_rwd_code.Properties.DataSource = dt_bantu
        pwsr_rwd_code.Properties.DisplayMember = dt_bantu.Columns("pbt_desc").ToString
        pwsr_rwd_code.Properties.ValueMember = dt_bantu.Columns("pbt_code").ToString
        pwsr_rwd_code.ItemIndex = 0


        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            pwsr_tran_id.Properties.DataSource = dt_bantu
            pwsr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            pwsr_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            pwsr_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            pwsr_tran_id.Properties.DataSource = dt_bantu
            pwsr_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            pwsr_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            pwsr_tran_id.ItemIndex = 0
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "Cancel", "pilih", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity Shipment", "en_desc_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "pb_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "pbt_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Status Reject Web", "pb_status_reject_web", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Request Reject Reff", "pb_reff_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Status Inventory Issue", "pb_status_inv_issue", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Issue", "pb_inv_issue_code", DevExpress.Utils.HorzAlignment.Default)

        '
        add_column_copy(gv_master, "Status Inventory Receipt", "pb_status_inv_receipt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Inventory Receipt", "pb_inv_receipt_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Packing Status", "pb_status_packing", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Packing By", "pb_packing_by", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Status Work Flow", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "pbd_pb_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "pbd_pb2_oid", False)
        add_column(gv_detail, "pbd_pb2det_oid", False)
        add_column_copy(gv_detail, "Product Req Number", "pbd_pb2_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Trans Process", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'complete teh buat kalau sudah receipt
        add_column_copy(gv_detail, "Qty Trans Complete", "pbd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Issue Process", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "pbd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pbd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "pbd_pb2_oid", False)
        add_column(gv_edit, "pbd_pb2det_oid", False)
        add_column(gv_edit, "Product Req Number", "pbd_pb2_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "pbd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "pbd_qty_processed", False)
        add_column(gv_edit, "pbd_um", False)
        add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "pb_oid", False)
        add_column(gv_email, "pbd_pb_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_smart, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)

    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  false as pilih," _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.pb_mstr.pb_oid, " _
                    & "  public.pb_mstr.pb_dom_id, " _
                    & "  public.pb_mstr.pb_en_id, " _
                    & "  public.pb_mstr.pb_upd_date, " _
                    & "  public.pb_mstr.pb_upd_by, " _
                    & "  public.pb_mstr.pb_add_date, " _
                    & "  public.pb_mstr.pb_add_by, " _
                    & "  public.pb_mstr.pb_code, " _
                    & "  public.pb_mstr.pb_date, " _
                    & "  public.pb_mstr.pb_due_date, " _
                    & "  public.pb_mstr.pb_requested, " _
                    & "  public.pb_mstr.pb_end_user, " _
                    & "  public.pb_mstr.pb_rmks, " _
                    & "  public.pb_mstr.pb_status, " _
                    & "  public.pb_mstr.pb_tran_id, " _
                    & "  public.pb_mstr.pb_trans_id,pb_status_packing,pb_packing_by, " _
                    & "  public.pb_mstr.pb_close_date,pb_inv_issue_code,pb_inv_receipt_code,pb_reff_code,pb_status_inv_issue,pb_status_inv_receipt,pb_status_reject_web, " _
                    & "  public.pb_mstr.pb_dt,pb_pbt_code,pbt_desc,pb_en_id_shipment,en_mstr_shipment.en_desc as en_desc_shipment " _
                    & "FROM " _
                    & "  public.pb_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.pb_mstr.pb_en_id = public.en_mstr.en_id) " _
                    & "  LEFT OUTER JOIN public.pbt_type ON (public.pb_mstr.pb_pbt_code = public.pbt_type.pbt_code) " _
                    & "  LEFT OUTER JOIN public.en_mstr en_mstr_shipment ON (pb_en_id_shipment = en_mstr_shipment.en_id) " _
                    & " where pb_wo_oid is  null and pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
                    & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + "" _
                    & " and pb_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        load_detail()
        'If ds.Tables(0).Rows.Count = 0 Then
        '    Exit Sub
        'End If

        'Dim sql As String

        'Try
        '    ds.Tables("detail").Clear()
        'Catch ex As Exception
        'End Try

        'sql = "SELECT  " _
        '    & "  public.pbd_det.pbd_oid, " _
        '    & "  public.pbd_det.pbd_dom_id, " _
        '    & "  public.pbd_det.pbd_en_id, " _
        '    & "  public.en_mstr.en_desc, " _
        '    & "  public.pbd_det.pbd_si_id, " _
        '    & "  public.si_mstr.si_desc, " _
        '    & "  public.pbd_det.pbd_add_by, " _
        '    & "  public.pbd_det.pbd_add_date, " _
        '    & "  public.pbd_det.pbd_upd_by, " _
        '    & "  public.pbd_det.pbd_upd_date, " _
        '    & "  public.pbd_det.pbd_pb_oid, " _
        '    & "  public.pbd_det.pbd_seq, " _
        '    & "  public.pbd_det.pbd_pt_id, " _
        '    & "  public.pt_mstr.pt_code, " _
        '    & "  public.pt_mstr.pt_desc1, " _
        '    & "  public.pt_mstr.pt_desc2, " _
        '    & "  public.pbd_det.pbd_rmks, " _
        '    & "  public.pbd_det.pbd_end_user, " _
        '    & "  public.pbd_det.pbd_due_date, " _
        '    & "  public.pbd_det.pbd_qty, " _
        '    & "  public.pbd_det.pbd_qty_processed, " _
        '    & "  public.pbd_det.pbd_qty_completed, " _
        '    & "  public.pbd_det.pbd_qty_riud, " _
        '    & "  public.pbd_det.pbd_um, " _
        '    & "  public.code_mstr.code_name, " _
        '    & "  public.pbd_det.pbd_status, " _
        '    & "  public.pbd_det.pbd_dt " _
        '    & "  FROM " _
        '    & "  public.pbd_det " _
        '    & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
        '    & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
        '    & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
        '    & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
        '    & "  INNER JOIN public.pb_mstr ON (public.pbd_det.pbd_pb_oid = public.pb_mstr.pb_oid) " _
        '    & "  where pb_mstr.pb_date >= " + SetDate(pr_txttglawal.DateTime.Date) + "" _
        '    & "  and pb_mstr.pb_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        'load_data_detail(sql, gc_detail, "detail")

        'If _conf_value = "1" Then
        '    Try
        '        ds.Tables("wf").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
        '          " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
        '          " wf_iscurrent, wf_seq " + _
        '          " from wf_mstr w " + _
        '          " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
        '          " inner join pb_mstr on pb_code = wf_ref_code " + _
        '          " inner join pbd_det dt on dt.pbd_pb_oid = pb_oid " _
        '        & " where pb_date >= " + SetDate(pr_txttglawal.DateTime) _
        '        & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
        '        & " and pb_en_id in (select user_en_id from tconfuserentity " _
        '                              & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
        '        & " order by wf_ref_code, wf_seq "
        '    load_data_detail(sql, gc_wf, "wf")
        '    gv_wf.BestFitColumns()

        '    Try
        '        ds.Tables("email").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "SELECT  " _
        '               & "  public.en_mstr.en_desc, " _
        '                & "  public.pb_mstr.pb_oid, " _
        '                & "  public.pb_mstr.pb_dom_id, " _
        '                & "  public.pb_mstr.pb_en_id, " _
        '                & "  public.pb_mstr.pb_upd_date, " _
        '                & "  public.pb_mstr.pb_upd_by, " _
        '                & "  public.pb_mstr.pb_add_date, " _
        '                & "  public.pb_mstr.pb_add_by, " _
        '                & "  public.pb_mstr.pb_code, " _
        '                & "  public.pb_mstr.pb_date, " _
        '                & "  public.pb_mstr.pb_due_date, " _
        '                & "  public.pb_mstr.pb_requested, " _
        '                & "  public.pb_mstr.pb_end_user, " _
        '                & "  public.pb_mstr.pb_rmks, " _
        '                & "  public.pb_mstr.pb_status, " _
        '                & "  public.pb_mstr.pb_close_date, " _
        '                & "  public.pb_mstr.pb_dt, " _
        '                & "  public.pbd_det.pbd_oid, " _
        '                & "  public.pbd_det.pbd_dom_id, " _
        '                & "  public.pbd_det.pbd_en_id, " _
        '                & "  public.en_mstr.en_desc, " _
        '                & "  public.pbd_det.pbd_add_by, " _
        '                & "  public.pbd_det.pbd_add_date, " _
        '                & "  public.pbd_det.pbd_upd_by, " _
        '                & "  public.pbd_det.pbd_upd_date, " _
        '                & "  public.pbd_det.pbd_pb_oid, " _
        '                & "  public.pbd_det.pbd_seq, " _
        '                & "  public.pbd_det.pbd_pt_id, " _
        '                & "  public.pt_mstr.pt_code, " _
        '                & "  public.pt_mstr.pt_desc1, " _
        '                & "  public.pt_mstr.pt_desc2, " _
        '                & "  public.pbd_det.pbd_rmks, " _
        '                & "  public.pbd_det.pbd_end_user, " _
        '                & "  public.pbd_det.pbd_due_date, " _
        '                & "  public.pbd_det.pbd_qty, " _
        '                & "  public.pbd_det.pbd_qty_processed, " _
        '                & "  public.pbd_det.pbd_qty_completed, " _
        '                & "  public.pbd_det.pbd_um, " _
        '                & "  public.code_mstr.code_name, " _
        '                & "  public.pbd_det.pbd_status, " _
        '                & "  public.pbd_det.pbd_dt " _
        '                & "FROM " _
        '                & "  public.pb_mstr " _
        '                & " inner join pbd_det on pbd_pb_oid = pb_oid " _
        '                & " inner join en_mstr on en_id = pb_en_id  " _
        '                & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
        '                & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
        '                & " where pb_date >= " + SetDate(pr_txttglawal.DateTime) _
        '                & " and pb_date <= " + SetDate(pr_txttglakhir.DateTime) _
        '                & " and pb_en_id in (select user_en_id from tconfuserentity " _
        '                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        '    load_data_detail(sql, gc_email, "email")
        '    gv_email.BestFitColumns()

        '    Try
        '        ds.Tables("smart").Clear()
        '    Catch ex As Exception
        '    End Try

        '    sql = "select pb_oid, pb_code, pb_trans_id, false as status from pb_mstr " _
        '        & " where pb_trans_id ~~* 'd' "

        '    load_data_detail(sql, gc_smart, "smart")
        'End If
    End Sub

    Public Overrides Sub relation_detail()
        'Try
        '    gv_detail.Columns("pbd_pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pbd_pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '    gv_detail.BestFitColumns()

        '    If _conf_value = "1" Then
        '        gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code").ToString & "'")
        '        gv_wf.BestFitColumns()

        '        gv_email.Columns("pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'")
        '        gv_email.BestFitColumns()
        '    End If

        'Catch ex As Exception
        'End Try
    End Sub
    Public Sub load_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  public.pbd_det.pbd_oid, " _
            & "  public.pbd_det.pbd_dom_id, " _
            & "  public.pbd_det.pbd_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.pbd_det.pbd_si_id,pbd_pb2_code,pbd_pb2_oid,pbd_pb2det_oid, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.pbd_det.pbd_add_by, " _
            & "  public.pbd_det.pbd_add_date, " _
            & "  public.pbd_det.pbd_upd_by, " _
            & "  public.pbd_det.pbd_upd_date, " _
            & "  public.pbd_det.pbd_pb_oid, " _
            & "  public.pbd_det.pbd_seq, " _
            & "  public.pbd_det.pbd_pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  public.pbd_det.pbd_rmks, " _
            & "  public.pbd_det.pbd_end_user, " _
            & "  public.pbd_det.pbd_due_date, " _
            & "  public.pbd_det.pbd_qty, " _
            & "  public.pbd_det.pbd_qty_processed, " _
            & "  public.pbd_det.pbd_qty_completed, " _
            & "  public.pbd_det.pbd_qty_riud, " _
            & "  public.pbd_det.pbd_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.pbd_det.pbd_status, " _
            & "  public.pbd_det.pbd_dt " _
            & "  FROM " _
            & "  public.pbd_det " _
            & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
            & "  where pbd_det.pbd_pb_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"


        load_data_detail(sql, gc_detail, "detail")

        If _conf_value = "1" Then
            Try
                ds.Tables("wf").Clear()
            Catch ex As Exception
            End Try

            sql = "select distinct wf_oid, wf_ref_oid, wf_ref_code, " + _
                  " wf_user_id, wf_wfs_id, wfs_desc, wf_date_to, wf_aprv_date, wf_desc, wf_aprv_user, " + _
                  " wf_iscurrent, wf_seq " + _
                  " from wf_mstr w " + _
                  " inner join wfs_status s on s.wfs_id = w.wf_wfs_id " + _
                  " inner join pb_mstr on pb_code = wf_ref_code " + _
                  " inner join pbd_det dt on dt.pbd_pb_oid = pb_oid " _
                & " where pb_mstr.pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
               & "  public.en_mstr.en_desc, " _
                & "  public.pb_mstr.pb_oid, " _
                & "  public.pb_mstr.pb_dom_id, " _
                & "  public.pb_mstr.pb_en_id, " _
                & "  public.pb_mstr.pb_upd_date, " _
                & "  public.pb_mstr.pb_upd_by, " _
                & "  public.pb_mstr.pb_add_date, " _
                & "  public.pb_mstr.pb_add_by, " _
                & "  public.pb_mstr.pb_code, " _
                & "  public.pb_mstr.pb_date, " _
                & "  public.pb_mstr.pb_due_date, " _
                & "  public.pb_mstr.pb_requested, " _
                & "  public.pb_mstr.pb_end_user, " _
                & "  public.pb_mstr.pb_rmks, " _
                & "  public.pb_mstr.pb_status, " _
                & "  public.pb_mstr.pb_close_date, " _
                & "  public.pb_mstr.pb_dt, " _
                & "  public.pbd_det.pbd_oid, " _
                & "  public.pbd_det.pbd_dom_id, " _
                & "  public.pbd_det.pbd_en_id, " _
                & "  public.en_mstr.en_desc, " _
                & "  public.pbd_det.pbd_add_by, " _
                & "  public.pbd_det.pbd_add_date, " _
                & "  public.pbd_det.pbd_upd_by, " _
                & "  public.pbd_det.pbd_upd_date, " _
                & "  public.pbd_det.pbd_pb_oid, " _
                & "  public.pbd_det.pbd_seq, " _
                & "  public.pbd_det.pbd_pt_id, " _
                & "  public.pt_mstr.pt_code, " _
                & "  public.pt_mstr.pt_desc1, " _
                & "  public.pt_mstr.pt_desc2, " _
                & "  public.pbd_det.pbd_rmks, " _
                & "  public.pbd_det.pbd_end_user, " _
                & "  public.pbd_det.pbd_due_date, " _
                & "  public.pbd_det.pbd_qty, " _
                & "  public.pbd_det.pbd_qty_processed, " _
                & "  public.pbd_det.pbd_qty_completed, " _
                & "  public.pbd_det.pbd_um, " _
                & "  public.code_mstr.code_name, " _
                & "  public.pbd_det.pbd_status, " _
                & "  public.pbd_det.pbd_dt " _
                & "FROM " _
                & "  public.pb_mstr " _
                & " inner join pbd_det on pbd_pb_oid = pb_oid " _
                & " inner join en_mstr on en_id = pb_en_id  " _
                & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                & " where pb_mstr.pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select pb_oid, pb_code, pb_trans_id, false as status from pb_mstr " _
                & " where pb_trans_id ~~* 'd' "

            load_data_detail(sql, gc_smart, "smart")
        End If
    End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        pwsr_en_id.Focus()
        pwsr_en_id.ItemIndex = 0
        pwsr_date.DateTime = Now
        pwsr_due_date.DateTime = Now
        pwsr_requested.Text = ""
        pwsr_end_user.Text = ""
        pwsr_rmks.Text = ""
        pwsr_tran_id.ItemIndex = 0
        pwsr_rwd_code.ItemIndex = 0
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.pbd_det.pbd_oid, " _
                        & "  public.pbd_det.pbd_dom_id, " _
                        & "  public.pbd_det.pbd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.pbd_det.pbd_si_id, " _
                        & "  public.si_mstr.si_desc, " _
                        & "  public.pbd_det.pbd_add_by, " _
                        & "  public.pbd_det.pbd_add_date, " _
                        & "  public.pbd_det.pbd_upd_by, " _
                        & "  public.pbd_det.pbd_upd_date, " _
                        & "  public.pbd_det.pbd_pb_oid, " _
                        & "  public.pbd_det.pbd_seq, " _
                        & "  public.pbd_det.pbd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  public.pbd_det.pbd_rmks, " _
                        & "  public.pbd_det.pbd_end_user, " _
                        & "  public.pbd_det.pbd_qty, " _
                        & "  public.pbd_det.pbd_qty_processed, " _
                        & "  public.pbd_det.pbd_qty_completed, " _
                        & "  public.pbd_det.pbd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  public.pbd_det.pbd_due_date, " _
                        & "  public.pbd_det.pbd_status,pbd_pb2_oid,pbd_pb2det_oid,pbd_pb2_code, " _
                        & "  public.pbd_det.pbd_dt " _
                        & "FROM " _
                        & "  public.pbd_det " _
                        & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                        & " where public.pbd_det.pbd_oid is null"

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()
        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("pbd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _pb_oid As Guid
        _pb_oid = Guid.NewGuid


        Dim _pb_code As String

        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _pb_trn_id As Integer
        Dim _pb_trn_satatus As String
        Dim ds_bantu As New DataSet



        If cek_duplikat_pt_id(gv_edit, "pbd_pt_id") = False Then
            Return False
        End If

        _pb_code = func_coll.get_transaction_number("IR", pwsr_en_id.GetColumnValue("en_code"), "pb_mstr", "pb_code")

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(pwsr_tran_id.EditValue)
        End If

        '=============================================================================

        _pb_trn_id = pwsr_tran_id.EditValue
        _pb_trn_satatus = "D" 'Lansung Default Ke Draft

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.pb_mstr " _
                                            & "( " _
                                            & "  pb_oid, " _
                                            & "  pb_dom_id, " _
                                            & "  pb_en_id, " _
                                            & "  pb_add_by, " _
                                            & "  pb_add_date, " _
                                            & "  pb_code, " _
                                            & "  pb_date, " _
                                            & "  pb_due_date, " _
                                            & "  pb_requested, " _
                                            & "  pb_end_user, " _
                                            & "  pb_rmks, " _
                                            & "  pb_tran_id, " _
                                            & "  pb_trans_id,pb_pbt_code,pb_en_id_shipment, " _
                                            & "  pb_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pb_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(pwsr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ", " _
                                            & SetSetring(_pb_code) & ",  " _
                                            & SetDate(pwsr_date.DateTime) & ",  " _
                                            & SetDate(pwsr_due_date.DateTime) & ",  " _
                                            & SetSetring(pwsr_requested.Text) & ",  " _
                                            & SetSetring(pwsr_end_user.Text) & ",  " _
                                            & SetSetring(pwsr_rmks.Text) & ",  " _
                                            & SetInteger(pwsr_tran_id.EditValue) & ",  " _
                                            & SetSetring(_pb_trn_satatus) & ",  " _
                                            & SetSetring(pwsr_rwd_code.EditValue) & ",  " _
                                            & SetInteger(pwsr_en_id_shipment.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.pbd_det " _
                                                & "( " _
                                                & "  pbd_oid, " _
                                                & "  pbd_dom_id, " _
                                                & "  pbd_en_id, " _
                                                & "  pbd_si_id, " _
                                                & "  pbd_add_by, " _
                                                & "  pbd_add_date, " _
                                                & "  pbd_pb_oid, " _
                                                & "  pbd_seq, " _
                                                & "  pbd_pt_id, " _
                                                & "  pbd_rmks, " _
                                                & "  pbd_end_user,pbd_pb2_oid,pbd_pb2det_oid,pbd_pb2_code, " _
                                                & "  pbd_qty, " _
                                                & "  pbd_um, " _
                                                & "  pbd_due_date, " _
                                                & "  pbd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_en_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_si_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_pb_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_rmks")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_end_user")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2_oid")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2det_oid")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2_code")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("pbd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_um")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("pbd_due_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE pb2d_det set pbd_qty_processed= coalesce(pbd_qty_processed,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pbd_qty")) _
                                            & " where pbd_oid =" & SetSetring(ds_edit.Tables(0).Rows(i).Item("pbd_pb2det_oid"))

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                        Next

                        If _conf_value = "1" Then
                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(pwsr_en_id.EditValue) & ",  " _
                                                        & SetSetring(pwsr_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_pb_oid.ToString) & ",  " _
                                                        & SetSetring(_pb_code) & ",  " _
                                                        & SetSetring("Inventory Request") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, pb_en_id.EditValue, 7, _pb_oid.ToString, _pb_code, pb_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Inv Request " & _pb_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        after_success()
                        set_row(_pb_oid.ToString, "pb_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean


        If _conf_value = "1" Then
            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_trans_id")) = "X" Then
                MessageBox.Show("Can't Edit Data That Has Been cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
            If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_trans_id")) = "W" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_trans_id")) = "I" Then
                    MessageBox.Show("Can't Edit Data That Has Been Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        Else
            'If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
            '    MessageBox.Show("Disable Authorization Edit PO...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Function
            'End If
            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_trans_id") <> "D" Then
            '    If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")) > 0 Then
            '        MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Function
            '    End If
            'End If

        End If

        For i As Integer = 0 To gv_detail.RowCount - 1
            If Not gv_detail.GetRowCellValue(i, "pbd_qty_processed") Is System.DBNull.Value Then
                If gv_detail.GetRowCellValue(i, "pbd_qty_processed") > 0 Then
                    MessageBox.Show("Data already processed..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                    Exit Function
                End If
            End If
            If Not gv_detail.GetRowCellValue(i, "pbd_qty_riud") Is System.DBNull.Value Then
                If gv_detail.GetRowCellValue(i, "pbd_qty_riud") > 0 Then
                    MessageBox.Show("Data already processed..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                    Exit Function
                End If
            End If
            '
        Next

        If ds.Tables.Count = 0 Then
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            Exit Function
        End If

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pb_oid_mstr = .Item("pb_oid").ToString
                pwsr_en_id.EditValue = .Item("pb_en_id")
                pwsr_rwd_code.EditValue = .Item("pb_pbt_code")
                pwsr_date.DateTime = .Item("pb_date")
                pwsr_due_date.DateTime = .Item("pb_due_date")
                pwsr_requested.Text = SetString(.Item("pb_requested"))
                pwsr_end_user.Text = SetString(.Item("pb_end_user"))
                pwsr_rmks.Text = SetString(.Item("pb_rmks"))
                pwsr_tran_id.EditValue = .Item("pb_tran_id")
                pwsr_en_id_shipment.EditValue = .Item("pb_en_id_shipment")
            End With
            pwsr_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            'ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  public.pbd_det.pbd_oid, " _
                                & "  public.pbd_det.pbd_dom_id, " _
                                & "  public.pbd_det.pbd_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.pbd_det.pbd_si_id, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.pbd_det.pbd_add_by, " _
                                & "  public.pbd_det.pbd_add_date, " _
                                & "  public.pbd_det.pbd_upd_by, " _
                                & "  public.pbd_det.pbd_upd_date, " _
                                & "  public.pbd_det.pbd_pb_oid, " _
                                & "  public.pbd_det.pbd_seq, " _
                                & "  public.pbd_det.pbd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.pbd_det.pbd_rmks, " _
                                & "  public.pbd_det.pbd_end_user, " _
                                & "  public.pbd_det.pbd_qty, " _
                                & "  public.pbd_det.pbd_qty_processed,pbd_pb2_code,pbd_pb2_oid,pbd_pb2det_oid, " _
                                & "  public.pbd_det.pbd_qty_completed, " _
                                & "  public.pbd_det.pbd_um, " _
                                & "  public.code_mstr.code_name, " _
                                & "  public.pbd_det.pbd_due_date, " _
                                & "  public.pbd_det.pbd_status, " _
                                & "  public.pbd_det.pbd_dt " _
                                & "FROM " _
                                & "  public.pbd_det " _
                                & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.pb_mstr ON (public.pbd_det.pbd_pb_oid = public.pb_mstr.pb_oid)" _
                                & " where public.pbd_det.pbd_pb_oid = '" + ds.Tables(0).Rows(row).Item("pb_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _pbd_qty_processed As Double
        Dim i As Integer
        Dim ssqls As New ArrayList
        Dim _pb_trn_id As Integer
        Dim _pb_trn_satatus As String
        Dim ds_bantu As New DataSet

        _pb_trn_id = pwsr_tran_id.EditValue
        _pb_trn_satatus = "D" 'set default langsung ke D

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(pwsr_tran_id.EditValue)
        End If

        Dim ssql As String = ""
        ssql = "SELECT  " _
            & "  public.pbd_det.pbd_oid, " _
            & "  public.pbd_det.pbd_dom_id, " _
            & "  public.pbd_det.pbd_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.pbd_det.pbd_si_id,pbd_pb2_code,pbd_pb2_oid,pbd_pb2det_oid, " _
            & "  public.si_mstr.si_desc, " _
            & "  public.pbd_det.pbd_add_by, " _
            & "  public.pbd_det.pbd_add_date, " _
            & "  public.pbd_det.pbd_upd_by, " _
            & "  public.pbd_det.pbd_upd_date, " _
            & "  public.pbd_det.pbd_pb_oid, " _
            & "  public.pbd_det.pbd_seq, " _
            & "  public.pbd_det.pbd_pt_id, " _
            & "  public.pt_mstr.pt_code, " _
            & "  public.pt_mstr.pt_desc1, " _
            & "  public.pt_mstr.pt_desc2, " _
            & "  public.pbd_det.pbd_rmks, " _
            & "  public.pbd_det.pbd_end_user, " _
            & "  public.pbd_det.pbd_due_date, " _
            & "  public.pbd_det.pbd_qty, " _
            & "  public.pbd_det.pbd_qty_processed, " _
            & "  public.pbd_det.pbd_qty_completed, " _
            & "  public.pbd_det.pbd_qty_riud, " _
            & "  public.pbd_det.pbd_um, " _
            & "  public.code_mstr.code_name, " _
            & "  public.pbd_det.pbd_status, " _
            & "  public.pbd_det.pbd_dt " _
            & "  FROM " _
            & "  public.pbd_det " _
            & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
            & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
            & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
            & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
            & "  where pbd_det.pbd_pb_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"

        Dim dt_detail As New DataTable
        dt_detail = GetTableData(ssql)



        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.pb_mstr   " _
                                            & "SET  " _
                                            & "  pb_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  pb_en_id = " & SetInteger(pwsr_en_id.EditValue) & ",  " _
                                            & "  pb_en_id_shipment = " & SetInteger(pwsr_en_id_shipment.EditValue) & ",  " _
                                            & "  pb_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  pb_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  pb_date = " & SetDate(pwsr_date.DateTime) & ",  " _
                                            & "  pb_due_date = " & SetDate(pwsr_due_date.DateTime) & ",  " _
                                            & "  pb_requested = " & SetSetringDB(pwsr_requested.Text) & ",  " _
                                            & "  pb_end_user = " & SetSetringDB(pwsr_end_user.Text) & ",  " _
                                            & "  pb_rmks = " & SetSetringDB(pwsr_rmks.Text) & ",  " _
                                            & "  pb_tran_id = " & SetInteger(pwsr_tran_id.EditValue) & ",  " _
                                            & "  pb_trans_id = " & SetSetring(_pb_trn_satatus) & ",  " _
                                            & "  pb_pbt_code = " & SetSetringDB(pwsr_rwd_code.EditValue) & ",  " _
                                            & "  pb_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  pb_oid = " & SetSetring(_pb_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table transfer
                        'kalau sudah relasi ke table transfer jadi nya error...dan harusnya tidak didelete
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from pbd_det where coalesce(pbd_qty_processed,0) = 0 and pbd_pb_oid = '" + _pb_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        For Each dr_det As DataRow In dt_detail.Rows

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE pb2d_det set pbd_qty_processed= coalesce(pbd_qty_processed,0) - " & SetDbl(dr_det("pbd_qty")) _
                                            & " where pbd_oid =" & SetSetring(dr_det("pbd_pb2det_oid"))

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        'Insert data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _pbd_qty_processed = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("pbd_qty_processed")) = True, 0, ds_edit.Tables(0).Rows(i).Item("pbd_qty_processed"))
                            If _pbd_qty_processed = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.pbd_det " _
                                                    & "( " _
                                                    & "  pbd_oid, " _
                                                    & "  pbd_dom_id, " _
                                                    & "  pbd_en_id, " _
                                                    & "  pbd_si_id, " _
                                                    & "  pbd_add_by, " _
                                                    & "  pbd_add_date, " _
                                                    & "  pbd_pb_oid, " _
                                                    & "  pbd_seq, " _
                                                    & "  pbd_pt_id, " _
                                                    & "  pbd_rmks, " _
                                                    & "  pbd_end_user,pbd_pb2_oid,pbd_pb2det_oid,pbd_pb2_code, " _
                                                    & "  pbd_qty, " _
                                                    & "  pbd_qty_processed, " _
                                                    & "  pbd_um, " _
                                                    & "  pbd_due_date, " _
                                                    & "  pbd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_en_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("pbd_si_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_pb_oid_mstr.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & ds_edit.Tables(0).Rows(i).Item("pbd_pt_id") & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_rmks").ToString) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_end_user").ToString) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2_oid")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2det_oid")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_pb2_code").ToString) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("pbd_qty")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("pbd_qty_processed")) & ",  " _
                                                    & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("pbd_um").ToString) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("pbd_due_date")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.pbd_det   " _
                                                    & "SET  " _
                                                    & "  pbd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_rmks")) & ",  " _
                                                    & "  pbd_end_user = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("pbd_end_user")) & ",  " _
                                                    & "  pbd_qty = " & SetDblDB(ds_edit.Tables(0).Rows(i).Item("pbd_qty")) & ",  " _
                                                    & "  pbd_um = " & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("pbd_um")) & ",  " _
                                                    & "  pbd_due_date = " & SetDate(ds_edit.Tables(0).Rows(i).Item("pbd_due_date")) & ",  " _
                                                    & "  pbd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  pbd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("pbd_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE pb2d_det set pbd_qty_processed= coalesce(pbd_qty_processed,0) + " & SetDbl(ds_edit.Tables(0).Rows(i).Item("pbd_qty")) _
                                            & " where pbd_oid =" & SetSetring(ds_edit.Tables(0).Rows(i).Item("pbd_pb2det_oid"))

                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        '================================================================
                        If _conf_value = "1" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + _pb_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.wf_mstr " _
                                                        & "( " _
                                                        & "  wf_oid, " _
                                                        & "  wf_dom_id, " _
                                                        & "  wf_en_id, " _
                                                        & "  wf_tran_id, " _
                                                        & "  wf_ref_oid, " _
                                                        & "  wf_ref_code, " _
                                                        & "  wf_ref_desc, " _
                                                        & "  wf_seq, " _
                                                        & "  wf_user_id, " _
                                                        & "  wf_wfs_id, " _
                                                        & "  wf_iscurrent, " _
                                                        & "  wf_dt " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                        & SetInteger(pwsr_en_id.EditValue) & ",  " _
                                                        & SetSetring(pwsr_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_pb_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")) & ",  " _
                                                        & SetSetring("Inventory Request") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, pb_en_id.EditValue, 7, _pb_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code"), pb_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Inv Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pb_oid_mstr, "pb_oid")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select coalesce(pbd_qty_processed,0) as pbd_qty_processed from pbd_det " + _
                           " where pbd_pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "pbd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("pbd_qty_processed") > 0 Then
                MessageBox.Show("Can't Delete Processed Requisition...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        'Dim i As Integer
        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from pb_mstr where pb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Inv Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            Dim ssql As String = ""
                            ssql = "SELECT  " _
                                & "  public.pbd_det.pbd_oid, " _
                                & "  public.pbd_det.pbd_dom_id, " _
                                & "  public.pbd_det.pbd_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.pbd_det.pbd_si_id,pbd_pb2_code,pbd_pb2_oid,pbd_pb2det_oid, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.pbd_det.pbd_add_by, " _
                                & "  public.pbd_det.pbd_add_date, " _
                                & "  public.pbd_det.pbd_upd_by, " _
                                & "  public.pbd_det.pbd_upd_date, " _
                                & "  public.pbd_det.pbd_pb_oid, " _
                                & "  public.pbd_det.pbd_seq, " _
                                & "  public.pbd_det.pbd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.pbd_det.pbd_rmks, " _
                                & "  public.pbd_det.pbd_end_user, " _
                                & "  public.pbd_det.pbd_due_date, " _
                                & "  public.pbd_det.pbd_qty, " _
                                & "  public.pbd_det.pbd_qty_processed, " _
                                & "  public.pbd_det.pbd_qty_completed, " _
                                & "  public.pbd_det.pbd_qty_riud, " _
                                & "  public.pbd_det.pbd_um, " _
                                & "  public.code_mstr.code_name, " _
                                & "  public.pbd_det.pbd_status, " _
                                & "  public.pbd_det.pbd_dt " _
                                & "  FROM " _
                                & "  public.pbd_det " _
                                & "  LEFT OUTER JOIN public.si_mstr ON (public.pbd_det.pbd_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.pbd_det.pbd_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.pt_mstr ON (public.pbd_det.pbd_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.pbd_det.pbd_um = public.code_mstr.code_id) " _
                                & "  where pbd_det.pbd_pb_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString & "'"

                            Dim dt_detail As New DataTable
                            dt_detail = GetTableData(ssql)

                            For Each dr_det As DataRow In dt_detail.Rows
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE pb2d_det set pbd_qty_processed= coalesce(pbd_qty_processed,0) - " & SetDbl(dr_det("pbd_qty")) _
                                                & " where pbd_oid =" & SetSetring(dr_det("pbd_pb2det_oid"))

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                            Next



                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region

#Region "gv_edit"

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "pbd_en_id")

        If _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._si_id = gv_edit.GetRowCellValue(_row, "pbd_si_id")
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pbd_pb2_code" Then
            Dim frm As New FProductRequestSearch
            frm.set_win(Me)
            frm._row = _row
            'frm._obj = riu_ref_pb_code
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _pbd_qty, _pbd_um_conv, _pbd_cost, _pbd_disc, _pbd_qty_processed As Double
        _pbd_um_conv = 1
        _pbd_qty = 1
        _pbd_cost = 0
        _pbd_disc = 0

        If e.Column.Name = "pbd_qty" Then
            '********* Cek Qty Processed
            Try
                _pbd_qty_processed = (gv_edit.GetRowCellValue(e.RowHandle, "pbd_qty_processed"))
            Catch ex As Exception
            End Try

            If e.Value < _pbd_qty_processed Then
                MessageBox.Show("Qty Request Can't Lower Than Qty Processed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            '*******************************            
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "pbd_en_id", pwsr_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", pwsr_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "pbd_end_user", Trim(pwsr_end_user.Text))
            .SetRowCellValue(e.RowHandle, "pbd_qty", 0)
            .SetRowCellValue(e.RowHandle, "pbd_due_date", pwsr_due_date.DateTime)
            .BestFitColumns()
        End With
    End Sub
#End Region

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Dim _pbd_qty_processed As Double = 0

        Try
            _pbd_qty_processed = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pbd_qty_processed")))
        Catch ex As Exception
        End Try

        If _pbd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub pwsr_ptnr_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pwsr_ptnr_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = pwsr_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _pbd_qty_processed As Double = 0

        Try
            _pbd_qty_processed = ((gv_edit.GetRowCellValue(0, "pbd_qty_processed")))
        Catch ex As Exception
        End Try

        If _pbd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Public Overrides Sub approve_line()
        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString
        _colom = "pb_trans_id"
        _table = "pb_mstr"
        _criteria = "pb_code"
        _initial = "pb"
        _type = "ir"
        _title = "Inventory Request"
        approve(_colom, _table, _criteria, _oid, _code, _initial, _type, gv_email, _title)
    End Sub

    Public Overrides Sub approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                  ByVal par_initial As String, ByVal par_type As String, ByVal par_gv As Object, ByVal par_title As String)
        Dim _trn_status, _pby_code, _sql As String


        If mf.get_transaction_status(par_colom, par_table, par_criteria, par_code) <> "D" Then
            MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Approve This Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If
        Dim ssqls As New ArrayList
        _pby_code = par_code
        _trn_status = "W"

        Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu As String

        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)
        user_phone = mf.get_phone(user_wf)

        'Dim _ds_pod_det As New DataSet
        '_ds_pod_det = get_pod_det(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_oid"))

        Try
            Using objappove As New master_new.WDABasepgsql("", "")
                With objappove
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If get_status_wf(par_code.ToString()) = 0 Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                                   " and wf_seq = 0"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Karena bisa saja datanya di rollback sehingga pada saat approve lagi...datanya harus dikosongkan lagi....
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        ElseIf get_status_wf(par_code.ToString()) > 0 Then

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_wfs_id = 0, wf_date_to = null, wf_desc = '', wf_aprv_user = '', wf_aprv_date = null " + _
                                                   " where wf_ref_code ~~* '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            _sql = "update wf_mstr set " + _
                                   " wf_iscurrent = 'Y', " + _
                                   " wf_wfs_id = '0', " + _
                                   " wf_desc = '', " + _
                                   " wf_date_to = null, " + _
                                   " wf_aprv_user = '', " + _
                                   " wf_aprv_date = null " + _
                                   " where wf_ref_code ~~* '" + par_code + "'" + _
                                   " and wf_seq = 0 "
                            ssqls.Add(.Command.CommandText)
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = _sql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        End If

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If


                        sqlTran.Commit()
                        format_email_bantu = mf.format_email(user_wf, par_code, par_type)
                        filename = appbase() & "\export\" + par_code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"
                        If preview_export(filename) = True Then

                            If user_wf_email <> "" Then
                                If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then
                                    If sent_sms(user_phone, "Mohon di approve IR : " & par_code & " entity : " & master_new.PGSqlConn.GetRowInfo("select en_desc from en_mstr where en_id = (select pb_en_id from pb_mstr where pb_oid='" & par_oid & "') ")(0).ToString & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
                                        Box("Gagal mengirim pesan")
                                        'Exit Sub
                                    End If
                                    Box("Email success")
                                Else
                                    Box("Email Failed, Use remainder mail to send email again")
                                End If

                                Try
                                    IO.File.Delete(filename)
                                Catch ex As Exception
                                End Try
                            Else
                                MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End If


                        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        help_load_data(True)
                        set_row(Trim(par_oid), par_initial + "_oid")
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function preview_export(ByVal par_filename As String) As Boolean
        Try
            Dim _sql As String

            Dim _en_id As Integer
            Dim _type, _table, _initial, _code_awal, _code_akhir As String

            _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_en_id")
            _type = 7
            _table = "pb_mstr"
            _initial = "pb"
            _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
            _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

            func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

            'Dim ds_bantu As New DataSet

            _sql = "SELECT " _
                & "pb_mstr.pb_oid, " _
                & "pb_mstr.pb_dom_id,  " _
                & "pb_mstr.pb_en_id,  " _
                & "pb_mstr.pb_add_by,  " _
                & "pb_mstr.pb_add_date, " _
                & "pb_mstr.pb_upd_by,  " _
                & "pb_mstr.pb_upd_date,  " _
                & "pb_mstr.pb_date,  " _
                & "pb_mstr.pb_due_date, " _
                & "pb_mstr.pb_requested,  " _
                & "pb_mstr.pb_end_user,  " _
                & "pb_mstr.pb_rmks, " _
                & "pb_mstr.pb_status,  " _
                & "pb_mstr.pb_close_date,  " _
                & "pb_mstr.pb_dt,  " _
                & "pb_mstr.pb_code, " _
                & "pbd_det.pbd_oid,  " _
                & "pbd_det.pbd_dom_id,  " _
                & "pbd_det.pbd_en_id,  " _
                & "pbd_det.pbd_add_by,  " _
                & "pbd_det.pbd_add_date,  " _
                & "pbd_det.pbd_upd_by,  " _
                & "pbd_det.pbd_upd_date,  " _
                & "pbd_det.pbd_pb_oid,  " _
                & "pbd_det.pbd_seq,  " _
                & "pbd_det.pbd_pt_id,  " _
                & "pbd_det.pbd_rmks, " _
                & "pbd_det.pbd_end_user,  " _
                & "pbd_det.pbd_qty,  " _
                & "pbd_det.pbd_qty_processed, " _
                & "pbd_det.pbd_qty_completed,  " _
                & "pbd_det.pbd_um,  " _
                & "pbd_det.pbd_due_date,  " _
                & "pbd_det.pbd_status,  " _
                & "pbd_det.pbd_dt,  " _
                & "pt_mstr.pt_code,  " _
                & "pt_mstr.pt_desc1,  " _
                & "pt_mstr.pt_desc2,  " _
                & "en_mstr.en_id,  " _
                & "en_mstr.en_desc, " _
                & "cmaddr_en_id, " _
                & "cmaddr_mstr.cmaddr_name,  " _
                & "cmaddr_mstr.cmaddr_line_1, " _
                & "cmaddr_mstr.cmaddr_line_2,  " _
                & "cmaddr_mstr.cmaddr_line_3, " _
                & "code_mstr.code_id,  " _
                & "code_mstr.code_code, " _
                & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM pb_mstr " _
                & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
                & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
                & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
                & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
                & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
                & "WHERE " _
                & "pb_mstr.pb_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code") + "' " _
                & "order by pb_mstr.pb_code"

            Dim rpt As New XRInventoryReqPrint
            With rpt
                Dim ds As New DataSet
                ds = master_new.PGSqlConn.ReportDataset(_sql)
                If ds.Tables(0).Rows.Count < 1 Then
                    Box("Maaf data kosong. Gagal email silahkan perbaiki data kemudian klik remainder email")
                    'Exit Function
                    Return False
                    Exit Function
                End If

                .DataSource = ds
                .DataMember = "Table"

                If IO.Directory.Exists(appbase() & "\export") = False Then
                    IO.Directory.CreateDirectory(appbase() & "\export")
                End If

                'export image
                Dim imageOptions As DevExpress.XtraPrinting.ImageExportOptions = rpt.ExportOptions.Image

                ' Set Image-specific export options.
                imageOptions.Resolution = 72
                imageOptions.ExportMode = DevExpress.XtraPrinting.ImageExportMode.SingleFile
                imageOptions.Format = System.Drawing.Imaging.ImageFormat.Png


                .ExportToImage(par_filename, imageOptions)

            End With
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function


    Public Overrides Sub cancel_line()
        Try
            Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
            _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
            _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString
            _colom = "pb_trans_id"
            _table = "pb_mstr"
            _criteria = "pb_code"
            _initial = "pb"
            _type = "ir"
            _title = "Inventory Request"
            cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub reminder_mail()
        'Dim _code, _type, _user, _title As String

        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        '_type = "ir"
        '_user = func_coll.get_wf_iscurrent(_code)
        '_title = "Inventory Request"

        'If _user = "" Then
        '    MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'reminder_by_mail(_code, _type, _user, gv_email, _title)

        Dim user_wf, user_wf_email, filename, format_email_bantu As String

        _conf_value = func_coll.get_conf_file("wf_purchase_order")

        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title, user_phone As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_oid").ToString
        _colom = "pb_trans_id"
        _table = "pb_mstr"
        _criteria = "pb_code"
        _initial = "pb"
        _type = "ir"
        _title = "Inventory Request"


        user_wf = mf.get_user_wf(_code, 0)
        user_wf_email = mf.get_email_address(user_wf)

        format_email_bantu = mf.format_email(user_wf, _code, _type)
        user_phone = mf.get_phone(user_wf)

        'filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".xls"
        filename = appbase() & "\export\" + _code + Now.ToString("_yyyyMMdd_HHmmss") + ".png"
        'ExportTo(par_gv, New ExportXlsProvider(filename))

        If preview_export(filename) = True Then

            If user_wf_email <> "" Then

                If mf.sent_email(user_wf_email, master_new.ClsVar.email_cc, mf.title_email(_title, _code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.email_user_name, filename) = True Then
                    If sent_sms(user_phone, "Mohon di approve IR : " & _code & " entity : " & master_new.PGSqlConn.GetRowInfo("select en_desc from en_mstr where en_id = (select pb_en_id from pb_mstr where pb_oid='" & _oid & "') ")(0).ToString & " melalui email atau syspro, replay email jika setuju. SMS Center") = False Then
                        Box("Gagal mengirim pesan")
                        'Exit Sub
                    End If
                    Box("Email success")

                Else
                    Box("Email Failed")
                End If

                Try
                    IO.File.Delete(filename)
                Catch ex As Exception

                End Try

            Else
                MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Public Overrides Sub smart_approve()
        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu, _title As String
        Dim i As Integer

        _title = "Inventory Request"

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = True Then

                Try
                    gv_email.Columns("pb_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pb_oid='" & ds.Tables("smart").Rows(i).Item("pb_oid").ToString & "'")
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("pb_code"), 0)
                user_wf_email = mf.get_email_address(user_wf)

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pb_mstr set pb_trans_id = '" + _trans_id + "'," + _
                                               " pb_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " pb_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where pb_oid = '" + ds.Tables("smart").Rows(i).Item("pb_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("pb_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("pb_code"), "dr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("pb_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email(_title, ds.Tables("smart").Rows(i).Item("pb_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                                Else
                                    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                End If

                            Catch ex As nPgSqlException
                                sqlTran.Rollback()
                                MessageBox.Show(ex.Message)
                            End Try
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        Next

        help_load_data(True)
        MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_en_id")
        _type = 7
        _table = "pb_mstr"
        _initial = "pb"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT " _
        & "pb_mstr.pb_oid, " _
        & "pb_mstr.pb_dom_id,  " _
        & "pb_mstr.pb_en_id,  " _
        & "pb_mstr.pb_add_by,  " _
        & "pb_mstr.pb_add_date, " _
        & "pb_mstr.pb_upd_by,  " _
        & "pb_mstr.pb_upd_date,  " _
        & "pb_mstr.pb_date,  " _
        & "pb_mstr.pb_due_date, " _
        & "pb_mstr.pb_requested,  " _
        & "pb_mstr.pb_end_user,  " _
        & "pb_mstr.pb_rmks, " _
        & "pb_mstr.pb_status,  " _
        & "pb_mstr.pb_close_date,  " _
        & "pb_mstr.pb_dt,  " _
        & "pb_mstr.pb_code, " _
        & "pbd_det.pbd_oid,  " _
        & "pbd_det.pbd_dom_id,  " _
        & "pbd_det.pbd_en_id,  " _
        & "pbd_det.pbd_add_by,  " _
        & "pbd_det.pbd_add_date,  " _
        & "pbd_det.pbd_upd_by,  " _
        & "pbd_det.pbd_upd_date,  " _
        & "pbd_det.pbd_pb_oid,  " _
        & "pbd_det.pbd_seq,  " _
        & "pbd_det.pbd_pt_id,  " _
        & "pbd_det.pbd_rmks, " _
        & "pbd_det.pbd_end_user,  " _
        & "pbd_det.pbd_qty,  " _
        & "pbd_det.pbd_qty_processed, " _
        & "pbd_det.pbd_qty_completed,  " _
        & "pbd_det.pbd_um,  " _
        & "pbd_det.pbd_due_date,  " _
        & "pbd_det.pbd_status,  " _
        & "pbd_det.pbd_dt,  " _
        & "pt_mstr.pt_code,  " _
        & "pt_mstr.pt_desc1,  " _
        & "pt_mstr.pt_desc2,  " _
        & "en_mstr.en_id,  " _
        & "en_mstr.en_desc, " _
        & "cmaddr_en_id, " _
        & "cmaddr_mstr.cmaddr_name,  " _
        & "cmaddr_mstr.cmaddr_line_1, " _
        & "cmaddr_mstr.cmaddr_line_2,  " _
        & "cmaddr_mstr.cmaddr_line_3, " _
        & "code_mstr.code_id,  " _
        & "code_mstr.code_code, " _
        & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
        & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
        & "FROM pb_mstr " _
        & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
        & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
        & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
        & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
        & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
        & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
        & "WHERE " _
        & "pb_mstr.pb_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code") + "' " _
        & "order by pb_mstr.pb_code"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRInventoryReqPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pb_code")
        frm.ShowDialog()

    End Sub
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT " _
                & "pb_mstr.pb_oid, " _
                & "pb_mstr.pb_dom_id,  " _
                & "pb_mstr.pb_en_id,  " _
                & "pb_mstr.pb_add_by,  " _
                & "pb_mstr.pb_add_date, " _
                & "pb_mstr.pb_upd_by,  " _
                & "pb_mstr.pb_upd_date,  " _
                & "pb_mstr.pb_date,  " _
                & "pb_mstr.pb_due_date, " _
                & "pb_mstr.pb_requested,  " _
                & "pb_mstr.pb_end_user,  " _
                & "pb_mstr.pb_rmks, " _
                & "pb_mstr.pb_status,pbt_desc,  " _
                & "pb_mstr.pb_close_date,  " _
                & "pb_mstr.pb_dt,  " _
                & "pb_mstr.pb_code, " _
                & "pbd_det.pbd_oid,  " _
                & "pbd_det.pbd_dom_id,  " _
                & "pbd_det.pbd_en_id,  " _
                & "pbd_det.pbd_add_by,  " _
                & "pbd_det.pbd_add_date,  " _
                & "pbd_det.pbd_upd_by,  " _
                & "pbd_det.pbd_upd_date,  " _
                & "pbd_det.pbd_pb_oid,  " _
                & "pbd_det.pbd_seq,  " _
                & "pbd_det.pbd_pt_id,  " _
                & "pbd_det.pbd_rmks, " _
                & "pbd_det.pbd_end_user,  " _
                & "pbd_det.pbd_qty,  " _
                & "pbd_det.pbd_qty_processed, " _
                & "pbd_det.pbd_qty_completed,  " _
                & "pbd_det.pbd_um,  " _
                & "pbd_det.pbd_due_date,  " _
                & "pbd_det.pbd_status,  " _
                & "pbd_det.pbd_dt,  " _
                & "pt_mstr.pt_code,  " _
                & "pt_mstr.pt_desc1,  " _
                & "pt_mstr.pt_desc2,  " _
                & "en_mstr.en_id,  " _
                & "en_mstr.en_desc, " _
                & "cmaddr_en_id, " _
                & "cmaddr_mstr.cmaddr_name,  " _
                & "cmaddr_mstr.cmaddr_line_1, " _
                & "cmaddr_mstr.cmaddr_line_2,  " _
                & "cmaddr_mstr.cmaddr_line_3, " _
                & "code_mstr.code_id,  " _
                & "code_mstr.code_code, " _
                & "coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
                & "tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
                & "FROM pb_mstr " _
                & "inner join pbd_det on pb_mstr.pb_oid = pbd_det.pbd_pb_oid " _
                & "inner join en_mstr on pb_mstr.pb_en_id = en_mstr.en_id " _
                & "inner join pt_mstr on pbd_det.pbd_pt_id = pt_mstr.pt_id " _
                & "inner join code_mstr on pbd_det.pbd_um = code_mstr.code_id  " _
                & "inner join cmaddr_mstr on pb_mstr.pb_en_id = cmaddr_mstr.cmaddr_en_id " _
                & "left outer join tranaprvd_dok on tranaprvd_tran_oid = pb_oid " _
                & "  LEFT OUTER JOIN public.pbt_type ON (public.pb_mstr.pb_pbt_code = public.pbt_type.pbt_code) " _
                & "WHERE " _
                & "  pb_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
                & "  pb_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " order by pb_mstr.pb_code"


            Dim frm As New frmExport
            Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

            With frm
                'add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "PR Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Entity", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Duedate", "req_due_date", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)

                'add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Remark Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
                'add_column_copy(.gv_export, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "Qty Komplit", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                'add_column_copy(.gv_export, "UM", "code_code", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date", "pb_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Due Date", "pb_due_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "pb_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Status", "pb_trans_id", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Type", "pbt_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Close Date", "pb_close_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "User Create", "pb_add_by", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date Create", "pb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
                add_column_copy(.gv_export, "User Update", "pb_upd_by", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date Update", "pb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

                add_column_copy(.gv_export, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "pbd_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "pbd_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Qty", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Trans Process", "pbd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Trans Complete", "pbd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Issue Process", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Due Date", "pbd_due_date", DevExpress.Utils.HorzAlignment.Center)
                add_column_copy(.gv_export, "Status", "pbd_status", DevExpress.Utils.HorzAlignment.Default)


                .gc_export.DataSource = master_new.PGSqlConn.GetTableData(ssql)
                .gv_export.BestFitColumns()
                .gv_export.ExportToXls(_file)
            End With

            frm.Dispose()
            Box("Export data sucess")
            OpenFile(_file)


        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function


    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_detail()
    End Sub

    Private Sub BeCopySQ_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BeCopySQ.ButtonClick
        Try
            Dim frm As New FSalesQuotationSearch
            frm.set_win(Me)
            frm._en_id = pwsr_en_id.EditValue
            frm._obj = BeCopySQ
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UpdateEntityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each dr As DataRow In ds_edit.Tables(0).Rows
                dr("pbd_en_id") = pwsr_en_id.EditValue
                dr("en_desc") = pwsr_en_id.Text
            Next
            ds_edit.AcceptChanges()
            Box("Success")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        Try
            Dim sSQL As String = ""
            Dim sSQLs As New ArrayList

            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                MessageBox.Show("Disable Authorization Cancel IR ..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ds.AcceptChanges()
            For Each dr As DataRow In ds.Tables(0).Rows
                If dr("pilih") = True Then
                    sSQL = "update pb_mstr set pb_status='X' where pb_oid='" & dr("pb_oid").ToString & "' and coalesce(pb_status,'')<>'C'"

                    sSQLs.Add(sSQL)
                End If
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then
                    Exit Sub
                End If
                sSQLs.Clear()
            End If

            Box("Success")
            help_load_data(True)

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPacking.Click
        Try
            'If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pay_interval") = 0 Then

            If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Proses Data Ini..?", "Packing", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssql As String

            Dim sSQLs As New ArrayList

            For Each dr_master As DataRow In ds.Tables(0).Rows
                If dr_master("pilih") = True Then
                    ssql = "update pb_mstr set pb_status_packing='D' where pb_oid='" & dr_master("pb_oid").ToString & "'"

                    sSQLs.Add(ssql)

                End If
            Next

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(sSQLs, "", master_new.PGSqlConn.FinsertSQL2Array(sSQLs), "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            Else
                If DbRunTran(sSQLs, "") = False Then

                    Exit Sub
                End If
                sSQLs.Clear()
            End If
            after_success()
            Box("Update success")


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
