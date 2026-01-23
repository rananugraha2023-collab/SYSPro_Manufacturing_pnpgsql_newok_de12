Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports System.IO
Imports master_new.PGSqlConn
Imports System.Text



Public Class FRequisition

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

    Private Sub FRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _conf_value = func_coll.get_conf_file("wf_requisition")
        _cost_center_pr_po = func_coll.get_conf_file("cost_center_pr_po")
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
        req_en_id.Properties.DataSource = dt_bantu
        req_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        req_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        req_en_id.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            req_tran_id.Properties.DataSource = dt_bantu
            req_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            req_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            req_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            req_tran_id.Properties.DataSource = dt_bantu
            req_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            req_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            req_tran_id.ItemIndex = 0
        End If

    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", req_en_id.EditValue))
        req_ptnr_id.Properties.DataSource = dt_bantu
        req_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        req_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        req_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cmaddr_mstr", req_en_id.EditValue))
        req_cmaddr_id.Properties.DataSource = dt_bantu
        req_cmaddr_id.Properties.DisplayMember = dt_bantu.Columns("cmaddr_name").ToString
        req_cmaddr_id.Properties.ValueMember = dt_bantu.Columns("cmaddr_id").ToString
        req_cmaddr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("sb_mstr", req_en_id.EditValue))
        req_sb_id.Properties.DataSource = dt_bantu
        req_sb_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        req_sb_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        req_sb_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_mstr", req_en_id.EditValue))
        req_cc_id.Properties.DataSource = dt_bantu
        req_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        req_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        req_cc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", req_en_id.EditValue))
        req_si_id.Properties.DataSource = dt_bantu
        req_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        req_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        req_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("pjc_mstr", req_en_id.EditValue))
        req_pjc_id.Properties.DataSource = dt_bantu
        req_pjc_id.Properties.DisplayMember = dt_bantu.Columns("pjc_desc").ToString
        req_pjc_id.Properties.ValueMember = dt_bantu.Columns("pjc_id").ToString
        req_pjc_id.ItemIndex = 0
    End Sub

    Private Sub req_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles req_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Need Date", "req_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Due Date", "req_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "req_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Date", "req_close_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "req_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqd_req_oid", False)
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
        add_column_copy(gv_detail, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
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

        add_column(gv_wf, "wf_ref_code", False)
        add_column_copy(gv_wf, "Seq.", "wf_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "User Approval", "wf_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Status", "wfs_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Hold To", "wf_date_to", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy")
        add_column_copy(gv_wf, "Remark", "wf_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Is Current", "wf_iscurrent", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_wf, "Date", "wf_aprv_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "MM/dd/yy H:mm")
        add_column_copy(gv_wf, "User", "wf_aprv_user", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_email, "req_oid", False)
        add_column_copy(gv_email, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Company Address", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date", "req_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Project", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Total", "req_total", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_email, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "User Create", "req_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_email, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column(gv_email, "reqd_req_oid", False)
        add_column_copy(gv_email, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description1", "reqd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Description2", "reqd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_email, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_email, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_email, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")

        add_column_copy(gv_smart, "Code", "req_code", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "reqd_en_id", False)
        add_column(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_related_oid", False)
        add_column(gv_edit, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_ptnr_id", False)
        add_column(gv_edit, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_si_id", False)
        add_column(gv_edit, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "reqd_qty_processed", False)
        add_column(gv_edit, "reqd_um", False)
        add_column(gv_edit, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_cost", False) 'cost
        add_column_edit(gv_edit, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "reqd_disc", False) 'Diskon
        'add_column_edit(gv_edit, "Discount", "reqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_edit(gv_edit, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column(gv_edit, "reqd_qty_cost", False) 'Qty * Cost

        add_column(gv_edit_doc, "File", "reqdd_file", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_doc, "File", "reqdd_file", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_doc, "Seq", "reqdd_seg", DevExpress.Utils.HorzAlignment.Default)

    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.req_mstr.req_oid, " _
                    & "  public.req_mstr.req_dom_id, " _
                    & "  public.req_mstr.req_en_id, " _
                    & "  public.req_mstr.req_upd_date, " _
                    & "  public.req_mstr.req_upd_by, " _
                    & "  public.req_mstr.req_add_date, " _
                    & "  public.req_mstr.req_add_by, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.req_mstr.req_ptnr_id, " _
                    & "  public.req_mstr.req_cmaddr_id, " _
                    & "  public.req_mstr.req_date, " _
                    & "  public.req_mstr.req_need_date, " _
                    & "  public.req_mstr.req_due_date, " _
                    & "  public.req_mstr.req_requested, " _
                    & "  public.req_mstr.req_end_user, " _
                    & "  public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_sb_id, " _
                    & "  public.req_mstr.req_cc_id, " _
                    & "  public.req_mstr.req_si_id, " _
                    & "  public.req_mstr.req_type, " _
                    & "  public.req_mstr.req_pjc_id, " _
                    & "  public.req_mstr.req_close_date, " _
                    & "  public.req_mstr.req_total, " _
                    & "  public.req_mstr.req_tran_id, " _
                    & "  public.req_mstr.req_trans_id, " _
                    & "  public.req_mstr.req_trans_rmks, " _
                    & "  public.req_mstr.req_current_route, " _
                    & "  public.req_mstr.req_next_route, " _
                    & "  public.req_mstr.req_dt, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  public.pjc_mstr.pjc_code, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.sb_mstr.sb_desc, " _
                    & "  public.cc_mstr.cc_desc " _
                    & "FROM " _
                    & "  public.req_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  left outer join public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and req_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If TxtPRDetail.Text.Length > 0 Then
            get_sequel = "SELECT distinct " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.req_mstr.req_oid, " _
                    & "  public.req_mstr.req_dom_id, " _
                    & "  public.req_mstr.req_en_id, " _
                    & "  public.req_mstr.req_upd_date, " _
                    & "  public.req_mstr.req_upd_by, " _
                    & "  public.req_mstr.req_add_date, " _
                    & "  public.req_mstr.req_add_by, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.req_mstr.req_ptnr_id, " _
                    & "  public.req_mstr.req_cmaddr_id, " _
                    & "  public.req_mstr.req_date, " _
                    & "  public.req_mstr.req_need_date, " _
                    & "  public.req_mstr.req_due_date, " _
                    & "  public.req_mstr.req_requested, " _
                    & "  public.req_mstr.req_end_user, " _
                    & "  public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_sb_id, " _
                    & "  public.req_mstr.req_cc_id, " _
                    & "  public.req_mstr.req_si_id, " _
                    & "  public.req_mstr.req_type, " _
                    & "  public.req_mstr.req_pjc_id, " _
                    & "  public.req_mstr.req_close_date, " _
                    & "  public.req_mstr.req_total, " _
                    & "  public.req_mstr.req_tran_id, " _
                    & "  public.req_mstr.req_trans_id, " _
                    & "  public.req_mstr.req_trans_rmks, " _
                    & "  public.req_mstr.req_current_route, " _
                    & "  public.req_mstr.req_next_route, " _
                    & "  public.req_mstr.req_dt, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  public.pjc_mstr.pjc_code, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.sb_mstr.sb_desc, " _
                    & "  public.cc_mstr.cc_desc " _
                    & "FROM " _
                    & "  public.req_mstr " _
                    & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                     & "  INNER JOIN public.reqd_det ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                    & "  left outer join public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                     & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid  " _
                     & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                    & " where  public.req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and public.req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and public.req_mstr.req_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and req_mstr_relation.req_code='" & TxtPRDetail.Text & "'"
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

        sql = "SELECT  " _
            & "  public.reqd_det.reqd_oid, " _
            & "  public.reqd_det.reqd_dom_id, " _
            & "  public.reqd_det.reqd_en_id, " _
            & "  public.en_mstr.en_desc, " _
            & "  public.reqd_det.reqd_add_by, " _
            & "  public.reqd_det.reqd_add_date, " _
            & "  public.reqd_det.reqd_upd_by, " _
            & "  public.reqd_det.reqd_upd_date, " _
            & "  public.reqd_det.reqd_req_oid, " _
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
            & "  where req_mstr.req_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and req_mstr.req_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""

        load_data_detail(sql, gc_detail, "detail")

        Dim row As Integer = BindingContext(ds.Tables(0)).Position
        Dim ssql As String
        ssql = "SELECT  " _
                            & "  reqdd_oid, " _
                            & "  reqdd_req_oid, " _
                            & "  reqdd_file, " _
                            & "  reqdd_file_old, " _
                            & "  reqdd_status, " _
                            & "  reqdd_seg " _
                            & "FROM  " _
                            & "  public.reqdd_doc where reqdd_req_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("req_oid").ToString) _
                            & " order by reqdd_seg"

        'dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
        gc_detail_doc.DataSource = master_new.PGSqlConn.GetTableData(ssql)
        gv_detail_doc.BestFitColumns()

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
                  " inner join req_mstr on req_code = wf_ref_code " + _
                  " inner join reqd_det dt on dt.reqd_req_oid = req_oid " _
                & " where req_date >= " + SetDate(pr_txttglawal.DateTime) _
                & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                & " and req_en_id in (select user_en_id from tconfuserentity " _
                                      & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by wf_ref_code, wf_seq "
            load_data_detail(sql, gc_wf, "wf")
            gv_wf.BestFitColumns()

            Try
                ds.Tables("email").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.req_mstr.req_oid, " _
                    & "  public.req_mstr.req_upd_date, " _
                    & "  public.req_mstr.req_upd_by, " _
                    & "  public.req_mstr.req_add_date, " _
                    & "  public.req_mstr.req_add_by, " _
                    & "  public.req_mstr.req_code, " _
                    & "  public.req_mstr.req_date, " _
                    & "  public.req_mstr.req_requested, " _
                    & "  public.req_mstr.req_rmks, " _
                    & "  public.req_mstr.req_pjc_id, " _
                    & "  public.req_mstr.req_total, " _
                    & "  public.req_mstr.req_trans_rmks, " _
                    & "  public.ptnr_mstr.ptnr_name, " _
                    & "  cmaddr_name, " _
                    & "  tran_name, " _
                    & "  public.pjc_mstr.pjc_code, " _
                    & "  public.si_mstr.si_desc, " _
                    & "  public.sb_mstr.sb_desc, " _
                    & "  public.cc_mstr.cc_desc, " _
                    & "  public.reqd_det.reqd_oid, " _
                    & "  public.reqd_det.reqd_req_oid, " _
                    & "  public.reqd_det.reqd_seq, " _
                    & "  public.reqd_det.reqd_pt_id, " _
                    & "  public.pt_mstr.pt_code, " _
                    & "  public.pt_mstr.pt_desc1, " _
                    & "  public.pt_mstr.pt_desc2, " _
                    & "  public.reqd_det.reqd_rmks, " _
                    & "  public.reqd_det.reqd_end_user, " _
                    & "  public.reqd_det.reqd_qty, " _
                    & "  public.reqd_det.reqd_um, " _
                    & "  public.code_mstr.code_name, " _
                    & "  public.reqd_det.reqd_due_date, " _
                    & "  public.reqd_det.reqd_um_conv, " _
                    & "  public.reqd_det.reqd_qty_real, " _
                    & "  public.reqd_det.reqd_pt_class, " _
                    & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                    & "FROM " _
                    & "  public.req_mstr " _
                    & "  INNER JOIN public.reqd_det ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
                    & "  INNER JOIN public.en_mstr ON (public.req_mstr.req_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.req_mstr.req_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                    & "  INNER JOIN public.cmaddr_mstr ON (public.req_mstr.req_cmaddr_id = public.cmaddr_mstr.cmaddr_id) " _
                    & "  INNER JOIN public.sb_mstr ON (public.req_mstr.req_sb_id = public.sb_mstr.sb_id) " _
                    & "  INNER JOIN public.cc_mstr ON (public.req_mstr.req_cc_id = public.cc_mstr.cc_id) " _
                    & "  INNER JOIN public.si_mstr ON (public.req_mstr.req_si_id = public.si_mstr.si_id) " _
                    & "  INNER JOIN public.pjc_mstr ON (public.req_mstr.req_pjc_id = public.pjc_mstr.pjc_id) " _
                    & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                    & " where req_date >= " + SetDate(pr_txttglawal.DateTime) _
                    & " and req_date <= " + SetDate(pr_txttglakhir.DateTime) _
                    & " and req_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            load_data_detail(sql, gc_email, "email")
            gv_email.BestFitColumns()

            Try
                ds.Tables("smart").Clear()
            Catch ex As Exception
            End Try

            sql = "select req_oid, req_code, req_trans_id, false as status from req_mstr " _
                & " where req_trans_id ~~* 'd' and req_add_by ~~* '" + master_new.ClsVar.sNama + "'"

            load_data_detail(sql, gc_smart, "smart")
        End If

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("reqd_req_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqd_req_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("reqd_req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid"))
            'gv_detail.BestFitColumns()

            If _conf_value = "1" Then
                gv_wf.Columns("wf_ref_code").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code").ToString & "'")
                gv_wf.BestFitColumns()

                gv_email.Columns("req_oid").FilterInfo = _
                New DevExpress.XtraGrid.Columns.ColumnFilterInfo("req_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString & "'")
                gv_email.BestFitColumns()

                'gv_wf.Columns("wf_ref_code").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
                'gv_wf.BestFitColumns()

                'gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid"))
                'gv_email.BestFitColumns()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region


#Region "DML"
    Public Overrides Sub insert_data_awal()
        req_en_id.Focus()
        req_en_id.ItemIndex = 0
        req_date.DateTime = _now
        req_due_date.DateTime = _now
        req_need_date.DateTime = _now
        req_requested.Text = ""
        req_end_user.Text = ""
        req_rmks.Text = ""
        req_tran_id.ItemIndex = 0
        req_type.SelectedIndex = 0

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
                            & "  public.reqd_det.reqd_oid, " _
                            & "  public.reqd_det.reqd_dom_id, " _
                            & "  public.reqd_det.reqd_en_id, " _
                            & "  public.en_mstr.en_desc, " _
                            & "  public.reqd_det.reqd_add_by, " _
                            & "  public.reqd_det.reqd_add_date, " _
                            & "  public.reqd_det.reqd_upd_by, " _
                            & "  public.reqd_det.reqd_upd_date, " _
                            & "  public.reqd_det.reqd_req_oid, " _
                            & "  public.reqd_det.reqd_seq, " _
                            & "  public.reqd_det.reqd_related_oid, " _
                            & "  public.reqd_det.reqd_related_type, " _
                            & "  req_mstr_relation.req_code as req_code_relation, " _
                            & "  public.reqd_det.reqd_ptnr_id, " _
                            & "  public.ptnr_mstr.ptnr_name, " _
                            & "  public.reqd_det.reqd_si_id, " _
                            & "  public.si_mstr.si_desc, " _
                            & "  public.reqd_det.reqd_pt_id, " _
                            & "  public.pt_mstr.pt_code, " _
                            & "  public.pt_mstr.pt_desc1, " _
                            & "  public.pt_mstr.pt_desc2, " _
                            & "  public.reqd_det.reqd_pt_desc1, " _
                            & "  public.reqd_det.reqd_pt_desc2, " _
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
                            & "  public.reqd_det.reqd_dt, 0.0 as reqd_qty_cost " _
                            & "FROM " _
                            & "  public.reqd_det " _
                            & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                            & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                            & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                            & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                            & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                            & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
                            & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
                            & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                            & " where public.reqd_det.reqd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "detail")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
            Dim ssql As String
            ssql = "SELECT  " _
                & "  reqdd_oid, " _
                & "  reqdd_req_oid, " _
                & "  reqdd_file, " _
                & "  reqdd_file_old, " _
                & "  reqdd_status, " _
                & "  reqdd_seg " _
                & "FROM  " _
                & "  public.reqdd_doc where reqdd_oid is null"

            dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
            gc_edit_doc.DataSource = dt_edit_doc
            gv_edit_doc.BestFitColumns()

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
            If ds_edit.Tables(0).Rows(i).Item("reqd_qty") = 0 Then
                MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        If func_coll.get_conf_file("restrict_pr_bp") = "1" Then
            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_type")) = True Then
                    MessageBox.Show("Relation Code Can't Empty...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            Next
        End If

        If _cost_center_pr_po = "1" Then
            If CInt(SetNumber(req_cc_id.EditValue)) = 0 Then
                MessageBox.Show("Cost center can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If _conf_value = "1" Then
            If req_tran_id.EditValue Is System.DBNull.Value Then
                MessageBox.Show("Transaction / routing approval can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            Else

                Dim ssql As String = "SELECT  " _
                    & "  aprv_oid, " _
                    & "  aprv_tran_id, " _
                    & "  aprv_user_id, " _
                    & "  aprv_dt, " _
                    & "  tran_oid, " _
                    & "  case when aprv_type = '0' then 'User' when aprv_type = '1' then 'Group' end as aprv_type, " _
                    & "  aprv_seq " _
                    & "FROM  " _
                    & "  public.aprv_mstr " _
                    & "  inner join tran_mstr on tran_id = aprv_tran_id" _
                    & "  inner join tranu_usr on tranu_tran_id = tran_id " _
                    & " where aprv_tran_id= " & SetInteger(req_tran_id.EditValue) _
                    & " order by aprv_seq"

                If master_new.PGSqlConn.CekRowSelect(ssql) = 0 Then
                    MessageBox.Show("Transaction / routing approval can't blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If

            End If
        End If



        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _req_oid As Guid
        _req_oid = Guid.NewGuid

        Dim _req_code As String
        Dim _req_total As Double
        Dim i, j As Integer
        Dim _req_trn_id As Integer
        Dim ssqls As New ArrayList
        Dim _req_trn_status As String
        _req_trn_status = "D" 'Lansung Default Ke Draft

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(req_tran_id.EditValue)
        End If
        _req_trn_id = req_tran_id.EditValue

        _req_code = func_coll.get_transaction_number("PR", req_en_id.GetColumnValue("en_code"), "req_mstr", "req_code")

        _req_total = 0
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _req_total = _req_total + (CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) * CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")))
        Next

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
                                            & "  public.req_mstr " _
                                            & "( " _
                                            & "  req_oid, " _
                                            & "  req_dom_id, " _
                                            & "  req_en_id, " _
                                            & "  req_add_by, " _
                                            & "  req_add_date, " _
                                            & "  req_code, " _
                                            & "  req_ptnr_id, " _
                                            & "  req_cmaddr_id, " _
                                            & "  req_date, " _
                                            & "  req_need_date, " _
                                            & "  req_due_date, " _
                                            & "  req_requested, " _
                                            & "  req_end_user, " _
                                            & "  req_rmks, " _
                                            & "  req_sb_id, " _
                                            & "  req_cc_id, " _
                                            & "  req_si_id, " _
                                            & "  req_type, " _
                                            & "  req_pjc_id, " _
                                            & "  req_total, " _
                                            & "  req_tran_id, " _
                                            & "  req_trans_id, " _
                                            & "  req_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_req_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & req_en_id.EditValue & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ", " _
                                            & SetSetring(_req_code) & ",  " _
                                            & SetInteger(req_ptnr_id.EditValue) & ",  " _
                                            & req_cmaddr_id.EditValue & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(req_need_date.DateTime) & ",  " _
                                            & SetDate(req_due_date.DateTime) & ",  " _
                                            & SetSetring(req_requested.Text) & ",  " _
                                            & SetSetring(req_end_user.Text) & ",  " _
                                            & SetSetring(req_rmks.Text) & ",  " _
                                            & SetInteger(req_sb_id.EditValue) & ",  " _
                                            & SetInteger(req_cc_id.EditValue) & ",  " _
                                            & SetInteger(req_si_id.EditValue) & ",  " _
                                            & SetSetring(req_type.Text.Substring(0, 1)) & ",  " _
                                            & SetInteger(req_pjc_id.EditValue) & ",  " _
                                            & SetDec(_req_total) & ",  " _
                                            & SetSetring(req_tran_id.EditValue) & ",  " _
                                            & SetSetring(_req_trn_status) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqd_det " _
                                                & "( " _
                                                & "  reqd_oid, " _
                                                & "  reqd_dom_id, " _
                                                & "  reqd_en_id, " _
                                                & "  reqd_add_by, " _
                                                & "  reqd_add_date, " _
                                                & "  reqd_req_oid, " _
                                                & "  reqd_seq, " _
                                                & "  reqd_related_oid, " _
                                                     & "  reqd_related_type, " _
                                                & "  reqd_ptnr_id, " _
                                                & "  reqd_si_id, " _
                                                & "  reqd_pt_id, " _
                                                & "  reqd_rmks, " _
                                                & "  reqd_end_user, " _
                                                & "  reqd_qty, " _
                                                & "  reqd_um, " _
                                                & "  reqd_cost, " _
                                                & "  reqd_disc, " _
                                                & "  reqd_need_date, " _
                                                & "  reqd_due_date, " _
                                                & "  reqd_um_conv, " _
                                                & "  reqd_qty_real, " _
                                                   & "  reqd_pt_desc1, " _
                                                & "  reqd_pt_desc2, " _
                                                & "  reqd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & master_new.ClsVar.sdom_id & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_en_id")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_req_oid.ToString) & ",  " _
                                                & i & ",  " _
                                                & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString)) & ",  " _
                                                   & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_type").ToString)) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_pt_id")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_rmks")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_end_user")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_um")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                  & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_pt_desc1")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_pt_desc2")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'Update relation PR apabila terdapat relasi pr
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = False Then
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString) _
                                '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"
                                'ssqls.Add(.Command.CommandText)
                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()

                                ''ant 13 maret 2011
                                'If ds_edit.Tables(0).Rows(i).Item("reqd_related_type") = "R" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString _
                                                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'ElseIf ds_edit.Tables(0).Rows(i).Item("reqd_related_type") = "B" Then
                                '    .Command.CommandType = CommandType.Text
                                '    .Command.CommandText = "update bpd_det set bpd_qty_consume = coalesce(bpd_qty_consume,0) + " + ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString _
                                '                         & " where bpd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"

                                '    .Command.ExecuteNonQuery()
                                '    .Command.Parameters.Clear()
                                'End If
                                ''-------


                                'Ini tidak dilakukan karena close pada saat user sudah terima barang dan ada menu khusus
                                '.Command.CommandType = CommandType.Text
                                '.Command.CommandText = "update reqd_det set reqd_status = 'c'" _
                                '                     & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'" _
                                '                     & " and reqd_qty_processed = reqd_qty "

                                '.Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            End If
                        Next

                        Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
                        Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
                        Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")


                        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                            _ip_server = func_coll.get_conf_file("ftp_doc_server_online")
                        Else
                            Dim ssql As String = ""
                            ssql = "select serv_code from tconfsetting"
                            Dim dr_serv As DataRow
                            dr_serv = GetRowInfo(ssql)

                            If SetString(dr_serv(0)) <> "SVR01" Then
                                _ip_server = func_coll.get_conf_file("ftp_doc_server_online")
                            End If

                        End If




                        Dim r As Integer = 0
                        For Each dr As DataRow In dt_edit_doc.Rows

                            If SetString(dr("reqdd_file_old")) = "" Then
                                If SetString(dr("reqdd_file")).Contains("\") Then

                                    Dim _nama_file As String = ""
                                    _nama_file = IO.Path.GetFileName(dr("reqdd_file")).Replace(" ", "_")
                                    _nama_file = _req_code & "_" & _nama_file


                                    Dim _sukses As Boolean = False

                                    If func_coll.get_conf_file("doc_upload_mode") = "http" Then
                                        Try
                                            Dim _url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & "upload.php?nama_file=" & _nama_file
                                            'Box(_url)
                                            HttpUploadFile(_url, dr("reqdd_file"), "files", "multipart/form-data")

                                            _sukses = True
                                        Catch ex As Exception
                                            MsgBox(ex.Message)
                                        End Try
                                    Else
                                        Dim _error As New ArrayList

                                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                                        If Upload(dr("reqdd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                            sqlTran.Rollback()
                                            'MessageBox.Show(ex.Message)
                                            Box("Upload error : " & _error.Item(0))
                                            insert = False
                                            '    'Return False
                                            '    'Exit Function
                                        End If
                                    End If

                                    


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqdd_doc " _
                                                & "( " _
                                                & "  reqdd_oid, " _
                                                & "  reqdd_req_oid, " _
                                                & "  reqdd_file, " _
                                                & "  reqdd_file_old, " _
                                                & "  reqdd_status, " _
                                                & "  reqdd_seg " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_req_oid.ToString) & ",  " _
                                                & SetSetring(_nama_file) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetInteger(r) & "  " _
                                                & ")"


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                Else

                                End If
                            End If


                            r = r + 1
                        Next


                        If _conf_value = "1" Then
                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(req_en_id.EditValue) & ",  " _
                                                        & SetSetring(req_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_req_oid.ToString) & ",  " _
                                                        & SetSetring(_req_code) & ",  " _
                                                        & SetSetring("Requisition") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, req_en_id.EditValue, 1, _req_oid.ToString, _req_code, req_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'insert = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Purchase Request " & _req_code)
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
                        set_row(_req_oid.ToString, "req_oid")
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
        Dim func_coll As New function_collection
        _conf_value = func_coll.get_conf_file("wf_requisition")

        If _conf_value = "0" Then
            If func_coll.get_conf_file("wf_requisition_editable") = "0" Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If

        relation_detail()
        Dim ssql As String
        ssql = "select reqd_qty_processed from reqd_det where reqd_req_oid= '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString & "'"
        Dim dt As New DataTable
        dt = master_new.PGSqlConn.GetTableData(ssql)
        For n As Integer = 0 To dt.Rows.Count - 1
            If SetNumber(dt.Rows(n).Item("reqd_qty_processed")) > 0 Then
                MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Function
            End If
        Next


        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _req_oid_mstr = .Item("req_oid").ToString
                req_en_id.EditValue = .Item("req_en_id")
                req_ptnr_id.EditValue = .Item("req_ptnr_id")
                req_cmaddr_id.EditValue = .Item("req_cmaddr_id")
                req_date.DateTime = .Item("req_date")
                req_need_date.DateTime = .Item("req_need_date")
                req_due_date.DateTime = .Item("req_due_date")
                req_requested.Text = SetString(.Item("req_requested"))
                req_end_user.Text = SetString(.Item("req_end_user"))
                req_rmks.Text = SetString(.Item("req_rmks"))
                req_sb_id.EditValue = .Item("req_sb_id")
                req_cc_id.EditValue = .Item("req_cc_id")
                req_si_id.EditValue = .Item("req_si_id")

                If .Item("req_type") = "B" Then
                    req_type.SelectedIndex = 0
                ElseIf .Item("req_type") = "N" Then
                    req_type.SelectedIndex = 1
                ElseIf .Item("req_type") = "R" Then
                    req_type.SelectedIndex = 2
                End If

                req_pjc_id.EditValue = .Item("req_pjc_id")
                req_tran_id.EditValue = .Item("req_tran_id")
            End With
            req_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            req_tran_id.Enabled = True

            ds_edit = New DataSet
            ds_update_related = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  public.reqd_det.reqd_oid, " _
                                & "  public.reqd_det.reqd_dom_id, " _
                                & "  public.reqd_det.reqd_en_id, " _
                                & "  public.en_mstr.en_desc, " _
                                & "  public.reqd_det.reqd_add_by, " _
                                & "  public.reqd_det.reqd_add_date, " _
                                & "  public.reqd_det.reqd_upd_by, " _
                                & "  public.reqd_det.reqd_upd_date, " _
                                & "  public.reqd_det.reqd_req_oid, " _
                                & "  public.reqd_det.reqd_seq, " _
                                & "  public.reqd_det.reqd_related_oid, " _
                             & "  public.reqd_det.reqd_related_type, " _
                                & "  CASE  coalesce(public.reqd_det.reqd_related_type,'X') " _
                                & "    WHEN 'R' THEN (SELECT req_code from req_mstr reqm inner join reqd_det reqd on reqd.reqd_req_oid=reqm.req_oid where reqd.reqd_oid = public.reqd_det.reqd_related_oid) " _
                                & "    WHEN 'B' THEN (SELECT bp_code from bp_mstr inner join bpd_det on bpd_bp_oid=bp_oid where bpd_oid = public.reqd_det.reqd_related_oid) " _
                                & "    ELSE  '-' " _
                                & "  END AS req_code_relation, " _
                                & "  public.reqd_det.reqd_ptnr_id, " _
                                & "  public.ptnr_mstr.ptnr_name, " _
                                & "  public.reqd_det.reqd_si_id, " _
                                & "  public.si_mstr.si_desc, " _
                                & "  public.reqd_det.reqd_pt_id, " _
                                & "  public.pt_mstr.pt_code, " _
                                & "  public.pt_mstr.pt_desc1, " _
                                & "  public.pt_mstr.pt_desc2, " _
                                & "  public.reqd_det.reqd_pt_desc1, " _
                                & "  public.reqd_det.reqd_pt_desc2, " _
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
                                & "  public.reqd_det.reqd_status, public.reqd_det.reqd_boqs_oid, " _
                                & "  public.reqd_det.reqd_dt, ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
                                & "FROM " _
                                & "  public.reqd_det " _
                                & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
                                & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
                                & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
                                & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
                                & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
                                & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
                                & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
                                & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
                                & " where public.reqd_det.reqd_req_oid = '" + ds.Tables(0).Rows(row).Item("req_oid").ToString + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
                        .FillDataSet(ds_update_related, "update_related")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()


                        'Dim row As Integer = BindingContext(ds.Tables(0)).Position
                        'Dim ssql As String
                        ssql = "SELECT  " _
                            & "  reqdd_oid, " _
                            & "  reqdd_req_oid, " _
                            & "  reqdd_file, " _
                            & "  reqdd_file_old, " _
                            & "  reqdd_status, " _
                            & "  reqdd_seg " _
                            & "FROM  " _
                            & "  public.reqdd_doc where reqdd_req_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("req_oid").ToString) _
                            & " order by reqdd_seg"

                        dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
                        gc_edit_doc.DataSource = dt_edit_doc
                        gv_edit_doc.BestFitColumns()


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
        Dim _req_total, _reqd_qty_processed As Double
        Dim i, j As Integer
        Dim ssqls As New ArrayList
        Dim _req_trn_status As String
        _req_trn_status = "D" 'set default langsung ke D
        Dim _req_trn_id As Integer

        _req_trn_id = req_tran_id.EditValue

        Dim ds_bantu As New DataSet
        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(req_tran_id.EditValue)
        End If

        _req_total = 0
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _req_total = _req_total + (CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) * CDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")))
        Next

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        '=====================================================
                        'har 20110615 
                        'pengecekan apakah nilai yg diupdate lebih besar dr qty stand

                        Dim sSQL As String
                        Dim _qty_os, _qty_delete, _qty_add As Double

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) = False Then
                                _qty_os = 0
                                _qty_delete = 0
                                _qty_add = 0

                                sSQL = "SELECT  " _
                                    & "  a.boqs_oid, " _
                                    & "  a.boqs_seq, " _
                                    & "  a.boqs_pt_id, " _
                                    & "  a.boqs_qty,coalesce(a.boqs_qty_pr,0) as boqs_qty_pr, a.boqs_qty + coalesce(a.boqs_qty_pr,0) as boqs_qty_os " _
                                    & "FROM " _
                                    & "  public.boqs_stand a " _
                                    & "  INNER JOIN public.boq_mstr b ON (a.boqs_boq_oid = b.boq_oid) " _
                                    & "WHERE " _
                                    & "  a.boqs_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid"))

                                Dim dt As New DataTable
                                dt = master_new.PGSqlConn.GetTableData(sSQL)

                                For Each dr As DataRow In dt.Rows
                                    _qty_os = dr("boqs_qty_os")
                                Next

                                For n As Integer = 0 To ds_update_related.Tables(0).Rows.Count - 1
                                    If SetString(ds_update_related.Tables(0).Rows(n).Item("reqd_boqs_oid")) <> "" Then
                                        If ds_update_related.Tables(0).Rows(n).Item("reqd_boqs_oid").ToString = ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid").ToString Then
                                            _qty_delete = ds_update_related.Tables(0).Rows(n).Item("reqd_qty")
                                        End If
                                    End If
                                Next

                                _qty_add = ds_edit.Tables(0).Rows(i).Item("reqd_qty")

                                If _qty_add > (_qty_os - _qty_delete) Then
                                    Box("Qty PR higher than Qty Bill of Quantity")
                                    Return False
                                    Exit Function
                                End If
                            End If

                        Next
                        'har 20110615 end
                        '=====================================================

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.req_mstr   " _
                                            & "SET  " _
                                            & "  req_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  req_en_id = " & req_en_id.EditValue & ",  " _
                                            & "  req_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  req_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  req_ptnr_id = " & req_ptnr_id.EditValue & ",  " _
                                            & "  req_cmaddr_id = " & req_cmaddr_id.EditValue & ",  " _
                                            & "  req_date = " & SetDate(req_date.DateTime) & ",  " _
                                            & "  req_need_date = " & SetDate(req_need_date.DateTime) & ",  " _
                                            & "  req_due_date = " & SetDate(req_due_date.DateTime) & ",  " _
                                            & "  req_requested = " & SetSetringDB(req_requested.Text) & ",  " _
                                            & "  req_end_user = " & SetSetringDB(req_end_user.Text) & ",  " _
                                            & "  req_rmks = " & SetSetringDB(req_rmks.Text) & ",  " _
                                            & "  req_sb_id = " & SetInteger(req_sb_id.EditValue) & ",  " _
                                            & "  req_cc_id = " & SetInteger(req_cc_id.EditValue) & ",  " _
                                            & "  req_si_id = " & SetInteger(req_si_id.EditValue) & ",  " _
                                            & "  req_type = " & SetSetring(req_type.Text.Substring(0, 1)) & ",  " _
                                            & "  req_pjc_id = " & SetInteger(req_pjc_id.EditValue) & ",  " _
                                            & "  req_total = " & SetDec(_req_total.ToString) & ",  " _
                                            & "  req_tran_id = " & req_tran_id.EditValue & ",  " _
                                            & "  req_trans_id = " & SetSetring(_req_trn_status) & ",  " _
                                            & "  req_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  req_oid = " & SetSetring(_req_oid_mstr.ToString) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_update_related.Tables(0).Rows.Count - 1
                            'update boqs_stand on edit================
                            'har 20110615 begin
                            If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " _
                                                    + SetDec(ds.Tables("detail").Rows(i).Item("reqd_qty").ToString) + ", " _
                                                    + " reqd_status = null" _
                                                    & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" _
                                                    + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            If SetString(ds_update_related.Tables(0).Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                            & SetDbl(ds_update_related.Tables(0).Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds_update_related.Tables(0).Rows(i).Item("reqd_boqs_oid").ToString & "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                            'har 20110615 end
                            '=========================================
                        Next

                        'Delete Data Detail Sebelum Insert tapi yang belum mempunyai relasi ke table po
                        'kalau sudah relasi ke table po jadi nya error...dan harusnya tidak didelete
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from reqd_det where coalesce(reqd_qty_processed,0) = 0 and reqd_req_oid = '" + _req_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from reqdd_doc where reqdd_req_oid = '" + _req_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Insert data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _reqd_qty_processed = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed")) = True, 0, ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed"))
                            If _reqd_qty_processed = 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.reqd_det " _
                                                    & "( " _
                                                    & "  reqd_oid, " _
                                                    & "  reqd_dom_id, " _
                                                    & "  reqd_en_id, " _
                                                    & "  reqd_upd_by, " _
                                                    & "  reqd_upd_date, " _
                                                    & "  reqd_req_oid, " _
                                                    & "  reqd_seq, " _
                                                    & "  reqd_related_oid, " _
                                                    & "  reqd_ptnr_id, " _
                                                    & "  reqd_si_id,  " _
                                                    & "  reqd_pt_id, " _
                                                    & "  reqd_rmks, " _
                                                    & "  reqd_end_user, " _
                                                    & "  reqd_qty, " _
                                                    & "  reqd_qty_processed, " _
                                                    & "  reqd_um, " _
                                                    & "  reqd_cost, " _
                                                    & "  reqd_disc, " _
                                                    & "  reqd_need_date, " _
                                                    & "  reqd_due_date, " _
                                                    & "  reqd_um_conv, " _
                                                    & "  reqd_qty_real, " _
                                                    & "  reqd_boqs_oid, " _
                                                    & "  reqd_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & master_new.ClsVar.sdom_id & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = True, "null", SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString)) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                    & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_pt_id")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_rmks").ToString) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_end_user").ToString) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                    & SetDblDB(ds_edit.Tables(0).Rows(i).Item("reqd_qty_processed")) & ",  " _
                                                    & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_um").ToString) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                    & SetDate(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                    & SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                    & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ");"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                'Update relation PR apabila terdapat relasi pr
                                If IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_related_oid")) = False Then
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update reqd_det set reqd_qty_processed = coalesce(reqd_qty_processed,0) + " + SetDec(ds_edit.Tables(0).Rows(i).Item("reqd_qty").ToString) _
                                                         & " where reqd_oid = '" + ds_edit.Tables(0).Rows(i).Item("reqd_related_oid").ToString + "'"
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                End If
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                    & "  public.reqd_det   " _
                                                    & "SET  " _
                                                    & "  reqd_ptnr_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_ptnr_id")) & ",  " _
                                                    & "  reqd_si_id = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_si_id")) & ",  " _
                                                    & "  reqd_rmks = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_rmks")) & ",  " _
                                                    & "  reqd_end_user = " & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("reqd_end_user")) & ",  " _
                                                    & "  reqd_qty = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & ",  " _
                                                    & "  reqd_um = " & SetInteger(ds_edit.Tables(0).Rows(i).Item("reqd_um")) & ",  " _
                                                    & "  reqd_cost = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_cost")) & ",  " _
                                                    & "  reqd_disc = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_disc")) & ",  " _
                                                    & "  reqd_need_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_need_date")) & ",  " _
                                                    & "  reqd_due_date = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_due_date")) & ",  " _
                                                    & "  reqd_um_conv = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_um_conv")) & ",  " _
                                                    & "  reqd_qty_real = " & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty_real")) & ",  " _
                                                    & "  reqd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                                    & "  " _
                                                    & "WHERE  " _
                                                    & "  reqd_oid = " & SetSetring(ds_edit.Tables(0).Rows(i).Item("reqd_oid")) & " "
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                            'update boqs_stand on edit================
                            'har 20110615 begin

                            If SetString(ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) + " _
                                            & SetDbl(ds_edit.Tables(0).Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds_edit.Tables(0).Rows(i).Item("reqd_boqs_oid").ToString & "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                            'har 20110615 end
                            '=========================================

                        Next


                        Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
                        Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
                        Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")

                        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                            _ip_server = func_coll.get_conf_file("ftp_doc_server_online")

                        End If

                        Dim r As Integer = 0
                        For Each dr As DataRow In dt_edit_doc.Rows

                            If SetString(dr("reqdd_file_old")) = "" Then
                                If SetString(dr("reqdd_file")).Contains("\") Then

                                    Dim _nama_file As String = ""
                                    _nama_file = IO.Path.GetFileName(dr("reqdd_file")).Replace(" ", "_")
                                    _nama_file = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") & "_" & _nama_file

                                    Dim _error As New ArrayList

                                    Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                                    If Upload(dr("reqdd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                                        sqlTran.Rollback()
                                        Box("Upload error : " & _error.Item(0))
                                        edit = False

                                    End If


                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqdd_doc " _
                                                & "( " _
                                                & "  reqdd_oid, " _
                                                & "  reqdd_req_oid, " _
                                                & "  reqdd_file, " _
                                                & "  reqdd_file_old, " _
                                                & "  reqdd_status, " _
                                                & "  reqdd_seg " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                & SetSetring(_nama_file) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetInteger(r) & "  " _
                                                & ")"


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                Else

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqdd_doc " _
                                                & "( " _
                                                & "  reqdd_oid, " _
                                                & "  reqdd_req_oid, " _
                                                & "  reqdd_file, " _
                                                & "  reqdd_file_old, " _
                                                & "  reqdd_status, " _
                                                & "  reqdd_seg " _
                                                & ") " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                & SetSetring(dr("reqdd_file")) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetInteger(r) & "  " _
                                                & ")"


                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If
                            End If


                            r = r + 1
                        Next




                        If _conf_value = "1" Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
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
                                                        & SetInteger(req_en_id.EditValue) & ",  " _
                                                        & SetSetring(req_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_req_oid_mstr.ToString) & ",  " _
                                                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) & ",  " _
                                                        & SetSetring("Requisition") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                        & ")"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        'If func_coll.insert_tranaprvd_det(ssqls, objinsert, req_en_id.EditValue, 1, _req_oid_mstr.ToString, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"), req_date.DateTime) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Purchase Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
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
                        set_row(_req_oid_mstr, "req_oid")
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
                    .SQL = "select coalesce(reqd_qty_processed,0) as reqd_qty_processed from reqd_det " + _
                           " where reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "reqd_det")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
            If ds_bantu.Tables(0).Rows(i).Item("reqd_qty_processed") > 0 Then
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

        '14 maret 2011
        If _conf_value = "0" Then
            'MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Exit Function
        ElseIf _conf_value = "1" Then
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_trans_id") <> "D" Then
                If func_coll.get_status_wf(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) > 0 Then
                    MessageBox.Show("Can't Edit Data That Has Been In The Process..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End If
            End If
        End If
        '----------------------------------------

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim i As Integer
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

                            'ant 14 maret 2011
                            Dim _is_proc_comp As Boolean

                            _is_proc_comp = False
                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqd_req_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString Then
                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_processed")) = False Then
                                        If ds.Tables("detail").Rows(i).Item("reqd_qty_processed") > 0 Then
                                            _is_proc_comp = True
                                        End If
                                    End If

                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_completed")) = False Then
                                        If ds.Tables("detail").Rows(i).Item("reqd_qty_completed") > 0 Then
                                            _is_proc_comp = True
                                        End If
                                    End If

                                End If
                            Next

                            If _is_proc_comp = False Then
                                For i = 0 To ds.Tables("detail").Rows.Count - 1
                                    If ds.Tables("detail").Rows(i).Item("reqd_req_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString Then
                                        If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + SetDec(ds.Tables("detail").Rows(i).Item("reqd_qty").ToString) + ", " + _
                                                                   " reqd_status = null" _
                                                                 & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()

                                            .Command.CommandType = CommandType.Text
                                            .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                            ssqls.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            .Command.Parameters.Clear()
                                        End If

                                        'update boqs_stand on delete###############
                                        'har 20110606
                                        Dim sSQL As String
                                        sSQL = "SELECT  " _
                                            & "  a.reqd_oid, " _
                                            & "  a.reqd_req_oid, " _
                                            & "  a.reqd_boqs_oid, " _
                                            & "  a.reqd_qty, " _
                                            & "  a.reqd_pt_id " _
                                            & "FROM " _
                                            & "  public.reqd_det a " _
                                            & "WHERE " _
                                            & "  a.reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"

                                        Dim dt As New DataTable
                                        dt = master_new.PGSqlConn.GetTableData(sSQL)

                                        For Each dr As DataRow In dt.Rows
                                            If SetString(dr("reqd_boqs_oid")) <> "" Then
                                                .Command.CommandType = CommandType.Text
                                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                                            & SetDbl(dr("reqd_qty")) & " where boqs_oid = '" & dr("reqd_boqs_oid").ToString & "'"
                                                ssqls.Add(.Command.CommandText)
                                                .Command.ExecuteNonQuery()
                                                .Command.Parameters.Clear()
                                            End If
                                        Next
                                        '###########################################
                                    End If
                                Next

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from req_mstr where req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = insert_log("Delete Purchase Request " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                MsgBox("Unable to Delete.., Part Already Processed or Completed!", MsgBoxStyle.Critical, "Delete Canceled")
                            End If
                            '----------------------------------

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
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
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
        Dim _reqd_en_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_en_id")
        Dim _pod_si_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_si_id")
        'Dim _pod_pt_id As Integer = gv_edit.GetRowCellValue(_row, "reqd_pt_id")

        If _col = "req_code_relation" Then
            Dim frm As New FRequisitionSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _reqd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "en_desc" Then
            Dim frm As New FEntitySearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "si_desc" Then
            Dim frm As New FSiteSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _reqd_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _reqd_en_id
            frm._si_id = _pod_si_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "code_name" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _reqd_en_id
            frm._pt_id = gv_edit.GetRowCellValue(_row, "reqd_pt_id")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _reqd_qty, _reqd_qty_real, _reqd_um_conv, _reqd_qty_cost, _reqd_cost, _reqd_disc, _reqd_qty_processed As Double
        _reqd_um_conv = 1
        _reqd_qty = 1
        _reqd_cost = 0
        _reqd_disc = 0

        If e.Column.Name = "reqd_qty" Then
            '********* Cek Qty Processed
            Try
                _reqd_qty_processed = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty_processed"))
            Catch ex As Exception
            End Try

            If e.Value < _reqd_qty_processed Then
                MessageBox.Show("Qty Requistion Can't Lower Than Qty Processed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit.CancelUpdateCurrentRow()
                Exit Sub
            End If
            '********************************

            Try
                _reqd_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_um_conv"))
            Catch ex As Exception
            End Try

            Try
                _reqd_cost = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost"))
            Catch ex As Exception
            End Try

            Try
                _reqd_disc = (gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc"))
            Catch ex As Exception
            End Try

            _reqd_qty_real = e.Value * _reqd_um_conv
            _reqd_qty_cost = (e.Value * _reqd_cost) - (e.Value * _reqd_cost * _reqd_disc)

            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)

        ElseIf e.Column.Name = "reqd_cost" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _reqd_disc = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_disc")))
            Catch ex As Exception
            End Try

            _reqd_qty_cost = (e.Value * _reqd_qty) - (e.Value * _reqd_qty * _reqd_disc)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)
        ElseIf e.Column.Name = "reqd_disc" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            Try
                _reqd_cost = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_cost")))
            Catch ex As Exception
            End Try

            _reqd_qty_cost = (_reqd_cost * _reqd_qty) - (_reqd_cost * _reqd_qty * e.Value)
            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_cost", _reqd_qty_cost)
        ElseIf e.Column.Name = "reqd_um_conv" Then
            Try
                _reqd_qty = ((gv_edit.GetRowCellValue(e.RowHandle, "reqd_qty")))
            Catch ex As Exception
            End Try

            _reqd_qty_real = e.Value * _reqd_qty

            gv_edit.SetRowCellValue(e.RowHandle, "reqd_qty_real", _reqd_qty_real)
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "reqd_en_id", req_en_id.EditValue)
            .SetRowCellValue(e.RowHandle, "en_desc", req_en_id.GetColumnValue("en_desc"))
            .SetRowCellValue(e.RowHandle, "reqd_ptnr_id", req_ptnr_id.GetColumnValue("ptnr_id"))
            .SetRowCellValue(e.RowHandle, "ptnr_name", req_ptnr_id.GetColumnValue("ptnr_name"))
            .SetRowCellValue(e.RowHandle, "reqd_si_id", req_si_id.EditValue)
            .SetRowCellValue(e.RowHandle, "si_desc", req_si_id.GetColumnValue("si_desc"))
            .SetRowCellValue(e.RowHandle, "reqd_end_user", Trim(req_end_user.Text))
            .SetRowCellValue(e.RowHandle, "reqd_qty", 0)
            .SetRowCellValue(e.RowHandle, "reqd_cost", 0)
            .SetRowCellValue(e.RowHandle, "reqd_disc", 0)
            .SetRowCellValue(e.RowHandle, "reqd_need_date", req_need_date.DateTime)
            .SetRowCellValue(e.RowHandle, "reqd_due_date", req_due_date.DateTime)
            .SetRowCellValue(e.RowHandle, "reqd_um_conv", 1)
            .SetRowCellValue(e.RowHandle, "reqd_qty_real", 0)
            .SetRowCellValue(e.RowHandle, "reqd_qty_cost", 0)
            .BestFitColumns()
        End With
    End Sub
#End Region

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Dim _reqd_qty_processed As Double = 0

        Try
            _reqd_qty_processed = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "reqd_qty_processed")))
        Catch ex As Exception
        End Try

        If _reqd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _reqd_qty_processed As Double = 0

        Try
            _reqd_qty_processed = ((gv_edit.GetRowCellValue(0, "reqd_qty_processed")))
        Catch ex As Exception
        End Try

        If _reqd_qty_processed <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_en_id")
        _type = 1
        _table = "req_mstr"
        _initial = "req"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  req_oid, " _
            & "  req_dom_id, " _
            & "  req_en_id, " _
            & "  req_upd_date, " _
            & "  req_upd_by, " _
            & "  req_add_date, " _
            & "  req_add_by, " _
            & "  req_code, " _
            & "  req_ptnr_id, " _
            & "  req_cmaddr_id, " _
            & "  req_date, " _
            & "  req_need_date, " _
            & "  req_due_date, " _
            & "  req_requested, " _
            & "  req_end_user, " _
            & "  req_rmks, " _
            & "  req_sb_id, " _
            & "  req_cc_id, " _
            & "  req_si_id, " _
            & "  req_type, " _
            & "  req_pjc_id, " _
            & "  req_close_date, " _
            & "  req_total, " _
            & "  req_tran_id, " _
            & "  req_trans_id, " _
            & "  req_trans_rmks, " _
            & "  req_current_route, " _
            & "  req_next_route, " _
            & "  req_dt, " _
            & "  reqd_ptnr_id, " _
            & "  reqd_pt_id, " _
            & "  reqd_rmks, " _
            & "  reqd_end_user, " _
            & "  reqd_qty, " _
            & "  reqd_um, " _
            & "  reqd_cost, " _
            & "  reqd_disc, " _
            & "  reqd_need_date, " _
            & "  reqd_due_date, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3, " _
            & "  ptnr_name, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  um_master.code_name as um_name , " _
            & "  coalesce(tranaprvd_name_1,'') as tranaprvd_name_1, coalesce(tranaprvd_name_2,'') as tranaprvd_name_2, coalesce(tranaprvd_name_3,'') as tranaprvd_name_3, coalesce(tranaprvd_name_4,'') as tranaprvd_name_4, " _
            & "  tranaprvd_pos_1, tranaprvd_pos_2, tranaprvd_pos_3, tranaprvd_pos_4 " _
            & "FROM  " _
            & "  req_mstr " _
            & "  inner join reqd_det on reqd_req_oid = req_oid " _
            & "  left outer join cmaddr_mstr on cmaddr_id = req_cmaddr_id " _
            & "  left outer join ptnr_mstr on ptnr_id = reqd_ptnr_id " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr um_master on um_master.code_id = reqd_um" _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = req_oid  " _
            & "  where req_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql


        'If GetCurrentColumnValue("tran_name") <> "*" Then

        'End If

        If func_coll.get_conf_file("wf_requisition") = "1" And SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_name")) <> "*" Then
            frm._report = "XRRequisitionApproval"
        Else
            frm._report = "XRRequisition"
        End If

        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        frm.ShowDialog()
    End Sub

    Public Overrides Sub approve_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type, _title As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString
        _colom = "req_trans_id"
        _table = "req_mstr"
        _criteria = "req_code"
        _initial = "req"
        _type = "pr"
        _title = "Requisition"

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


        Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu, user_id_telegram, user_id_telegram_create As String


        _pby_code = par_code
        _trn_status = "W"
        user_wf = mf.get_user_wf(par_code, 0)
        user_wf_email = mf.get_email_address(user_wf)
        user_id_telegram = mf.get_id_telegram(user_wf)
        user_id_telegram_create = mf.get_id_telegram(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_add_by"))

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update " + par_table + " set " + par_colom + " = '" + _trn_status + "'," + _
                                               " " + par_initial + "_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " " + par_initial + "_upd_date = current_timestamp " + _
                                               " where " + par_initial + "_oid = '" + par_oid + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

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


                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        'format_email_bantu = mf.format_email(user_wf, par_code, par_type)

                        'filename = "c:\syspro\" + par_code + ".xls"
                        'ExportTo(par_gv, New ExportXlsProvider(filename))

                        'If user_wf_email <> "" Then
                        '    mf.sent_email(user_wf_email, "", mf.title_email(par_title, par_code), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
                        'Else
                        '    MessageBox.Show("Email Address Not Available For User " + user_wf + Chr(13) + "Please Contact Your Admin Program ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        'End If

                        'MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Approved..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        If func_coll.get_conf_file("send_telegram") = "1" Then
                            'If func_coll.get_http_approval() = "" Then

                            'End If
                            Dim _alamat_api As String = "" & func_coll.get_http_approval() & "?pesan=approve@PR@" & func_coll.get_conf_file("syspro_approval_code") & "@" & par_code _
                            & "@" & user_id_telegram & "@" & user_wf & "@" & user_id_telegram_create

                            Dim result As String
                            result = mf.run_get_to_api(_alamat_api & "")

                            If SetString(result).Contains("success") Then
                                '_MakeReport("Data empty")

                            Else
                                Box("Gagal notif telegram")
                                Exit Sub
                            End If
                        End If

                        Box("Approve success")

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
    Public Overrides Sub cancel_line()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _code, _oid, _colom, _table, _criteria, _initial, _type As String
        _code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        _oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString
        _colom = "req_trans_id"
        _table = "req_mstr"
        _criteria = "req_code"
        _initial = "req"
        _type = "pr"
        cancel_approve(_colom, _table, _criteria, _oid, _code, _initial, _type)
    End Sub

    Public Overrides Sub cancel_approve(ByVal par_colom As String, ByVal par_table As String, ByVal par_criteria As String, ByVal par_oid As String, ByVal par_code As String, _
                                   ByVal par_initial As String, ByVal par_type As String)

        Dim _trans_id As String = ""
        Dim ssqls As New ArrayList

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select " + par_colom + " as trans_id from " + par_table + " where " + par_criteria + " = '" + par_code + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _trans_id = .DataReader("trans_id").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _trans_id.ToUpper = "D" Then
            MessageBox.Show("Can't Cancel For Draft Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        ElseIf _trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Cancel For Close Data..", "Conf", MessageBoxButtons.OK)
            Exit Sub
        Else
            If MessageBox.Show("Cancel This Data..", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'ant 14 maret 2011
                        Dim _is_proc_comp As Boolean

                        _is_proc_comp = False
                        For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                            If ds.Tables("detail").Rows(i).Item("reqd_req_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString Then
                                If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_processed")) = False Then
                                    If ds.Tables("detail").Rows(i).Item("reqd_qty_processed") > 0 Then
                                        _is_proc_comp = True
                                    End If
                                End If

                                If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_qty_completed")) = False Then
                                    If ds.Tables("detail").Rows(i).Item("reqd_qty_completed") > 0 Then
                                        _is_proc_comp = True
                                    End If
                                End If

                            End If
                        Next

                        If _is_proc_comp = False Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update " + par_table + " set " + par_colom + " = 'X'" + _
                                                   " where " + par_criteria + " = '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update wf_mstr set wf_iscurrent = 'N'" + _
                                                   " where wf_ref_code = '" + par_code + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqd_req_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString Then
                                    If IsDBNull(ds.Tables("detail").Rows(i).Item("reqd_related_oid")) = False Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update reqd_det set reqd_qty_processed = reqd_qty_processed - " + ds.Tables("detail").Rows(i).Item("reqd_qty").ToString + ", " + _
                                                               " reqd_status = null" _
                                                             & " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()

                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update req_mstr set req_close_date = null where req_oid = (select reqd_req_oid from reqd_det where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqd_related_oid").ToString + "')"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If

                                    'update boqs_stand on cancel approve ================
                                    'har 20110615 begin

                                    If SetString(ds.Tables("detail").Rows(i).Item("reqd_boqs_oid")) <> "" Then
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) - " _
                                                    & SetDbl(ds.Tables("detail").Rows(i).Item("reqd_qty")) & " where boqs_oid = '" & ds.Tables("detail").Rows(i).Item("reqd_boqs_oid").ToString & "'"
                                        ssqls.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                    'har 20110615 end
                                    '=========================================
                                End If
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
                            MessageBox.Show("Weldone " + master_new.ClsVar.sNama + ", Data Have Been Updated..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            row = BindingContext(ds.Tables(0)).Position
                            help_load_data(True)
                            set_row(Trim(par_oid), par_initial + "_oid")
                        Else
                            MsgBox("Unable to Canceled.., Part Already Processed or Completed!", MsgBoxStyle.Critical, "Delete Canceled")
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
    End Sub

    Public Overrides Sub reminder_mail()
        If _conf_value = "0" Then
            Exit Sub
        End If


        Try
            Dim selectedRowHandles As Integer() = gv_master.GetSelectedRows()
            If selectedRowHandles.Length > 0 Then
                For i As Integer = 0 To selectedRowHandles.Length - 1
                    If SetString(gv_master.GetRowCellValue(selectedRowHandles(i), "req_trans_id")) <> "W" Then
                        Box("Transaksi harus dalam status workflow (W)")
                        set_row(gv_master.GetRowCellValue(selectedRowHandles(i), "req_oid").ToString, "req_oid")
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

        'Dim _code, _type, _user, _title As String
        '_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")
        '_type = "pr"
        '_user = func_coll.get_wf_iscurrent(_code)
        '_title = "Requisition"

        'If _user = "" Then
        '    MessageBox.Show("Not Available Current User..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End If

        'reminder_by_mail(_code, _type, _user, gv_email, _title)

        Try
            Dim selectedRowHandles As Integer() = gv_master.GetSelectedRows()

            If selectedRowHandles.Length > 0 Then
                'Move focus to the first selected row.
                'View.FocusedRowHandle = selectedRowHandles(0)
                'Copy the selection to the clipboard
                'View.CopyToClipboard()
                'Copy the selected company names to a Memo editor.
                'MemoEdit1.Text = ""
                For i As Integer = 0 To selectedRowHandles.Length - 1
                    'MsgBox(gv_master.GetRowCellValue(selectedRowHandles(i), "po_code"))
                    'MemoEdit1.Text += View.GetRowCellDisplayText(selectedRowHandles(i), colCompany) & vbCrLf

                    Dim user_wf, user_wf_email, user_phone, filename, format_email_bantu, user_id_telegram As String
                    Dim par_code As String = gv_master.GetRowCellValue(selectedRowHandles(i), "req_code")

                    Dim _seq_id As Integer = 0
                    _seq_id = mf.get_user_wf_seq(par_code)

                    user_wf = mf.get_user_wf(par_code, _seq_id)
                    'user_wf_email = mf.get_email_address(user_wf)
                    'user_phone = mf.get_phone(user_wf)
                    user_id_telegram = mf.get_id_telegram(user_wf)

                    If func_coll.get_conf_file("send_telegram") = "1" Then

                        Dim _alamat_api As String = "" & func_coll.get_http_approval() & "?pesan=approve@PR@" & func_coll.get_conf_file("syspro_approval_code") & "@" & par_code _
                        & "@" & user_id_telegram & "@" & user_wf

                        Dim result As String
                        result = mf.run_get_to_api(_alamat_api & "")

                        If SetString(result).Contains("success") Then
                            '_MakeReport("Data empty")

                        Else
                            Box("Gagal notif telegram")
                            Exit Sub
                        End If
                    End If

                Next
            End If

            Box("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Overrides Sub smart_approve()
        If _conf_value = "0" Then
            Exit Sub
        End If

        Dim _trans_id, user_wf, user_wf_email, filename, format_email_bantu As String
        Dim i As Integer

        If MessageBox.Show("Approve All Data..?", "Approve", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        ds.Tables("smart").AcceptChanges()

        For i = 0 To ds.Tables("smart").Rows.Count - 1
            If ds.Tables("smart").Rows(i).Item("status") = "True" Then

                Try
                    gv_email.Columns("req_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables("smart").Rows(i).Item("req_oid").ToString)
                Catch ex As Exception
                End Try

                _trans_id = "W" 'default langsung ke W 
                user_wf = mf.get_user_wf(ds.Tables("smart").Rows(i).Item("req_code"), 0)
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
                                .Command.CommandText = "update req_mstr set req_trans_id = '" + _trans_id + "'," + _
                                               " req_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " req_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " + _
                                               " where req_oid = '" + ds.Tables("smart").Rows(i).Item("req_oid").ToString + "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update wf_mstr set wf_iscurrent = 'Y' " + _
                                                       " where wf_ref_code ~~* '" + ds.Tables("smart").Rows(i).Item("req_code") + "'" + _
                                                       " and wf_seq = 0"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                                format_email_bantu = mf.format_email(user_wf, ds.Tables("smart").Rows(i).Item("req_code"), "pr")

                                filename = "c:\syspro\" + ds.Tables("smart").Rows(i).Item("req_code") + ".xls"
                                ExportTo(gv_email, New ExportXlsProvider(filename))

                                If user_wf_email <> "" Then
                                    mf.sent_email(user_wf_email, "", mf.title_email("Requisition", ds.Tables("smart").Rows(i).Item("req_code")), format_email_bantu + vbCrLf + vbCrLf + master_new.ClsVar.sBody + vbCrLf + mf.petunjuk(), master_new.ClsVar.sEmailSyspro, filename)
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
    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  c.en_desc, " _
                & "  a.req_code, " _
                & "  d.ptnr_code, " _
                & "  d.ptnr_name, " _
                & "  a.req_date, " _
                & "  a.req_need_date, " _
                & "  a.req_due_date, " _
                & "  a.req_requested, " _
                & "  a.req_end_user, " _
                & "  a.req_rmks, " _
                & "  e.pt_code, " _
                & "  e.pt_desc1, " _
                & "  e.pt_desc2, " _
                & "  b.reqd_rmks, " _
                & "  b.reqd_cost, " _
                & "  b.reqd_qty, " _
                & "  f.code_code, " _
                & "  b.reqd_qty_processed, " _
                & "  b.reqd_qty_completed " _
                & "FROM " _
                & "  public.req_mstr a " _
                & "  INNER JOIN public.reqd_det b ON (a.req_oid = b.reqd_req_oid) " _
                & "  INNER JOIN public.en_mstr c ON (a.req_en_id = c.en_id) " _
                & "  INNER JOIN public.ptnr_mstr d ON (a.req_ptnr_id = d.ptnr_id) " _
                & "  INNER JOIN public.pt_mstr e ON (b.reqd_pt_id = e.pt_id) " _
                & "  INNER JOIN public.code_mstr f ON (b.reqd_um = f.code_id) " _
                & "WHERE " _
                & "  a.req_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
                & "  a.req_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

            Dim frm As New frmExport
            Dim _file As String = AskSaveAsFile("Excel Files | *.xls")

            With frm
                add_column_copy(.gv_export, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "PR Code", "req_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Entity", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Date", "req_date", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Duedate", "req_due_date", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Requested", "req_requested", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "End User", "req_end_user", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remarks", "req_rmks", DevExpress.Utils.HorzAlignment.Default)

                add_column_copy(.gv_export, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Remark Detail", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty Komplit", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "UM", "code_code", DevExpress.Utils.HorzAlignment.Default)
                add_column_copy(.gv_export, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
                add_column_copy(.gv_export, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
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



    Private Sub LabelControl4_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelControl4.DoubleClick
        Dim data As String
        data = func_coll.get_http_approval()
    End Sub

    Private Sub bt_tambah_dokumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_tambah_dokumen.Click
        Try

            If be_document.Text <> "" Then
                Dim _row As DataRow
                _row = dt_edit_doc.NewRow

                _row.Item("reqdd_file") = be_document.Text

                dt_edit_doc.Rows.Add(_row)
                dt_edit_doc.AcceptChanges()
                gv_edit_doc.BestFitColumns()
                be_document.Text = ""
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bt_hapus_dokumen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_hapus_dokumen.Click
        Try

            Dim row As Integer = BindingContext(dt_edit_doc).Position
            dt_edit_doc.Rows.RemoveAt(row)
            dt_edit_doc.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub bt_preview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_preview.Click
        Try
            Process.Start("")
        Catch ex As Exception

        End Try


    End Sub

    Private Sub bt_download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_download.Click
        Try
            Dim _nama_file As String = ""

            Dim _nomer As String = "PR4021030014500093"
            For Each dr As DataRow In dt_edit_doc.Rows
                If SetString(dr("reqdd_file_old")) = "" Then
                    'baru
                    If SetString(dr("reqdd_file")).Contains("\") Then
                        _nama_file = IO.Path.GetFileName(dr("reqdd_file"))
                        _nama_file = _nomer & "_" & _nama_file

                        Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
                        Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
                        Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")
                        Dim _error As New ArrayList

                        'ftp_doc_server
                        'ftp_doc_server_online
                        'ftp_doc_user
                        'ftp_doc_pass

                        If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                            _ip_server = func_coll.get_conf_file("ftp_doc_server_online")

                        End If


                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                        If Upload(dr("reqdd_file"), "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                            Box("Upload error : " & _error.Item(0))
                            '    'Return False
                            '    'Exit Function
                        End If

                    Else

                    End If

                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub be_document_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_document.ButtonClick

        Dim stream As FileStream
        Dim reader As BinaryReader
        Try
            Dim dlg As New OpenFileDialog
            dlg.Filter = "Image Files(*.jpeg;*.jpg;*.png)|*.JPEG;*.JPG;*.PNG|PDF files (*.pdf)|*.PDF"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                be_document.Text = dlg.FileName

                stream = New FileStream(be_document.Text, FileMode.Open)
                reader = New BinaryReader(stream)

                If stream.Length > 4000000 Then
                    Box("File max 4MB")
                    be_document.Text = ""
                    stream.Dispose()
                    Exit Sub
                End If
                stream.Dispose()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub be_document_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles be_document.EditValueChanged

    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            'If gv_master.RowCount = 0 Then
            '    Exit Sub
            'End If

            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim row As Integer = BindingContext(ds.Tables(0)).Position
            Dim ssql As String
            ssql = "SELECT  " _
                                & "  reqdd_oid, " _
                                & "  reqdd_req_oid, " _
                                & "  reqdd_file, " _
                                & "  reqdd_file_old, " _
                                & "  reqdd_status, " _
                                & "  reqdd_seg " _
                                & "FROM  " _
                                & "  public.reqdd_doc where reqdd_req_oid =" & SetSetring(ds.Tables(0).Rows(row).Item("req_oid").ToString) _
                                & " order by reqdd_seg"

            'dt_edit_doc = master_new.PGSqlConn.GetTableData(ssql)
            gc_detail_doc.DataSource = master_new.PGSqlConn.GetTableData(ssql)
            gv_detail_doc.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDocumentToolStripMenuItem.Click
        Dim stream As FileStream
        Dim reader As BinaryReader
        Try

            If master_new.ClsVar.sNama <> ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_add_by") Then
                Box("Can't proccess this menu, check permission..")
                Exit Sub
            End If


            Dim _user_ftp As String = func_coll.get_conf_file("ftp_doc_user")
            Dim _password_ftp As String = func_coll.get_conf_file("ftp_doc_pass")
            Dim _ip_server As String = func_coll.get_conf_file("ftp_doc_server")

            If konfigurasi(GetFileContents(appbase() & "\filekonfigurasi\pgserver.txt"), "server").ToString.ToLower.Contains("vpnsygma") = True Then
                _ip_server = func_coll.get_conf_file("ftp_doc_server_online")
            Else
                Dim ssql As String = ""
                ssql = "select serv_code from tconfsetting"
                Dim dr_serv As DataRow
                dr_serv = GetRowInfo(ssql)

                If SetString(dr_serv(0)) <> "SVR01" Then
                    _ip_server = func_coll.get_conf_file("ftp_doc_server_online")
                End If
            End If





            Dim _file_name As String = ""

            Dim dlg As New OpenFileDialog
            dlg.Filter = "Image Files(*.jpeg;*.jpg;*.png)|*.JPEG;*.JPG;*.PNG|PDF files (*.pdf)|*.PDF"
            If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
                _file_name = dlg.FileName

                stream = New FileStream(_file_name, FileMode.Open)
                reader = New BinaryReader(stream)


                If stream.Length > 4000000 Then
                    Box("File max 4MB")
                    be_document.Text = ""
                    stream.Dispose()
                    Exit Sub
                Else

                    stream.Dispose()
                    Dim _nama_file As String = ""
                    _nama_file = IO.Path.GetFileName(_file_name.Replace(" ", "_"))
                    _nama_file = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") & "_" & _nama_file

                    'Box(func_coll.get_conf_file("doc_upload_mode"))

                    Dim _sukses As Boolean = False
                    If func_coll.get_conf_file("doc_upload_mode") = "http" Then
                        Try
                            Dim _url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & "upload.php?nama_file=" & _nama_file
                            'Box(_url)
                            HttpUploadFile(_url, _file_name, "files", "multipart/form-data")

                            _sukses = True
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Else
                        Dim _error As New ArrayList
                        Dim credential As New Net.NetworkCredential(_user_ftp, _password_ftp)
                        If Upload(_file_name, "ftp://" & _ip_server & "/" & _nama_file, credential, _error) = False Then
                            'sqlTran.Rollback()
                            'MessageBox.Show(ex.Message)
                            Box("Upload error : " & _error.Item(0))
                            'insert = False
                            '    'Return False
                            '    'Exit Function
                        Else
                            _sukses = True
                        End If
                    End If


                    If _sukses = True Then
                        Dim sSQL As String
                        Dim ssqls As New ArrayList

                        sSQL = "INSERT INTO  " _
                                & "  public.reqdd_doc " _
                                & "( " _
                                & "  reqdd_oid, " _
                                & "  reqdd_req_oid, " _
                                & "  reqdd_file, " _
                                & "  reqdd_file_old, " _
                                & "  reqdd_status, " _
                                & "  reqdd_seg " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")) & ",  " _
                                & SetSetring(_nama_file) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & ",  " _
                                & SetInteger(1) & "  " _
                                & ")"

                        ssqls.Add(sSQL)

                        If master_new.PGSqlConn.status_sync = True Then
                            'Dim _data As String = arr_to_str(ssqls)
                            If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                                Exit Sub
                            End If
                            ssqls.Clear()
                        Else
                            If DbRunTran(ssqls, "") = False Then

                                Exit Sub
                            End If
                            ssqls.Clear()
                        End If

                        after_success()
                    End If


                    ' End If

                End If

            End If
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RemoveDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveDocumentToolStripMenuItem.Click
        Try

            Dim sSQL As String
            Dim ssqls As New ArrayList

            If master_new.ClsVar.sNama <> ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_add_by") Then
                Box("Can't proccess this menu, check permission..")
                Exit Sub
            End If

            sSQL = "DELETE from  " _
                    & "  public.reqdd_doc " _
                    & "where reqdd_oid='" & gv_detail_doc.GetRowCellValue(gv_detail_doc.FocusedRowHandle, "reqdd_oid").ToString & "'"

            ssqls.Add(sSQL)

            If master_new.PGSqlConn.status_sync = True Then
                'Dim _data As String = arr_to_str(ssqls)
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If
            after_success()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ShowFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowFileToolStripMenuItem.Click
        Try
            Dim Url As String = "http://" & func_coll.get_conf_file("ftp_doc_http_online") & gv_detail_doc.GetRowCellValue(gv_detail_doc.FocusedRowHandle, "reqdd_file")

            Process.Start(Url)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtPacking.Click
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

    Private Sub BtReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtReload.Click
        Try
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then
                'If SetString(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("po_trans_id")) = "I" Then
                '    MessageBox.Show("Can't Edit Data That Has Been Release", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Exit Sub
                'End If
                MessageBox.Show("No access to this menu..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If MessageBox.Show("Are you sure to reload ?", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssqls As New ArrayList

            Dim ssql As String

            'ssql = "UPDATE public.req_mstr set req_trans_id='W' where req_oid='" _
            '        & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "'"

            'ssql = "DELETE from public.wf_mstr  where wf_ref_oid='" _
            '        & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "'"

            'ssqls.Add(ssql)


            'ssql = "SELECT  " _
            '    & "  public.aprv_mstr.aprv_seq, " _
            '    & "  public.aprv_mstr.aprv_user_id " _
            '    & "FROM " _
            '    & "  public.aprv_mstr " _
            '    & "WHERE " _
            '    & "  public.wf_mstr.wf_ref_oid = '" _
            '    & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "'"

            Dim ds_bantu As New DataSet
            If _conf_value = "1" Then
                ds_bantu = func_data.load_aprv_mstr(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_tran_id"))
            End If



            If _conf_value = "1" Then
                ssql = "delete from wf_mstr where wf_ref_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
                ssqls.Add(ssql)

                Dim j As Integer
                For j = 0 To ds_bantu.Tables(0).Rows.Count - 1
                    ssql = "INSERT INTO  " _
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
                            & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_en_id")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_tran_id")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")) & ",  " _
                            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) & ",  " _
                            & SetSetring("Requisition") & ",  " _
                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                            & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                            & SetInteger(0) & ",  " _
                            & IIf(j = 0, SetSetring("Y"), SetSetring("N")) & ",  " _
                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                            & ")"
                    ssqls.Add(ssql)

                Next
            End If


            ssqls.Add(insert_log("Reload Purchase Requisition " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")))

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            Box("Reload success")
            after_success()
            set_row(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString, "req_oid")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtResetApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtResetApproval.Click
        Try
            If func_coll.get_menu_status(master_new.ClsVar.sUserID, Me.Name.Substring(1, Len(Me.Name) - 1)) = False Then

                MessageBox.Show("No access to this menu..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            If MessageBox.Show("Are you sure to reload ?", "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If

            Dim ssqls As New ArrayList

            Dim ssql As String



            ssql = "SELECT  " _
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
                & "  wf_date_to, " _
                & "  wf_desc, " _
                & "  wf_iscurrent, " _
                & "  wf_dt, " _
                & "  wf_aprv_user, " _
                & "  wf_aprv_date " _
                & " FROM  " _
                & "  public.wf_mstr  " _
                & "where wf_ref_code='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") & "' " _
                & "order by wf_seq"

            Dim dt_wf As New DataTable

            dt_wf = GetTableData(ssql)

            Dim _wf_oid_current As String = ""
            Dim _seq As Integer = 0

            For Each dr_wf As DataRow In dt_wf.Rows
                If dr_wf("wf_aprv_user").ToString = "" Then

                    _wf_oid_current = dr_wf("wf_oid").ToString
                    _seq = dr_wf("wf_seq")

                    ssql = "update wf_mstr set wf_iscurrent = 'Y' " & _
                             " where wf_oid = '" & _wf_oid_current & "'"
                    ssqls.Add(ssql)

                    ssql = "update wf_mstr set wf_iscurrent = 'N' " & _
                             " where  wf_ref_code ~~* '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code") & "'" & _
                             " and wf_seq < " & _seq

                    ssqls.Add(ssql)
                    Exit For
                End If

            Next



            'ssql = "UPDATE public.req_mstr set req_trans_id='W' where req_oid='" _
            '        & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "'"

            'ssqls.Add(ssql)


            'ssql = "SELECT  " _
            '    & "  public.aprv_mstr.aprv_seq, " _
            '    & "  public.aprv_mstr.aprv_user_id " _
            '    & "FROM " _
            '    & "  public.aprv_mstr " _
            '    & "WHERE " _
            '    & "  public.aprv_mstr.aprv_tran_id = " & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_tran_id")) & " and aprv_user_id not in (SELECT   public.wf_mstr.wf_user_id FROM  public.wf_mstr WHERE  public.wf_mstr.wf_ref_oid = '" _
            '    & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid") & "')"


            'Dim dt_wf As New DataTable
            'dt_wf = GetTableData(ssql)


            'For Each dr_wf As DataRow In dt_wf.Rows
            '    ssql = "INSERT INTO  " _
            '            & "  public.wf_mstr " _
            '            & "( " _
            '            & "  wf_oid, " _
            '            & "  wf_dom_id, " _
            '            & "  wf_en_id, " _
            '            & "  wf_tran_id, " _
            '            & "  wf_ref_oid, " _
            '            & "  wf_ref_code, " _
            '            & "  wf_ref_desc, " _
            '            & "  wf_seq, " _
            '            & "  wf_user_id, " _
            '            & "  wf_wfs_id, " _
            '            & "  wf_iscurrent, " _
            '            & "  wf_dt " _
            '            & ")  " _
            '            & "VALUES ( " _
            '            & SetSetring(Guid.NewGuid.ToString) & ",  " _
            '            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '            & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_en_id")) & ",  " _
            '            & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_tran_id")) & ",  " _
            '            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid")) & ",  " _
            '            & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")) & ",  " _
            '            & SetSetring("Requisition") & ",  " _
            '            & SetSetring(dr_wf("aprv_seq")) & ",  " _
            '            & SetSetring(dr_wf("aprv_user_id")) & ",  " _
            '            & SetInteger(0) & ",  " _
            '            & SetSetring("Y") & ",  " _
            '            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
            '            & ")"

            '    ssqls.Add(ssql)

            'Next

            ssqls.Add(insert_log("Reset Approval Purchase Requisition " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code")))

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            Box("Reload success")
            after_success()
            set_row(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString, "req_oid")
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    
End Class
