Imports Npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn
Imports DevExpress.XtraEditors.Controls

Public Class FWOLaborFeedback
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String
    Public dt_edit, dt_edit_reject, dt_edit_down As New DataTable
    Dim sSQL As String
    Public ds_edit, ds_edit_shipment, ds_edit_dist As New DataSet
    Dim sSQLs As New ArrayList

    Private Sub FItemSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        pr_entity.Properties.DataSource = dt_bantu
        pr_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        pr_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        pr_entity.ItemIndex = 0

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        lbrf_en_id.Properties.DataSource = dt_bantu
        lbrf_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        lbrf_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        lbrf_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        lbrf_en_id.Properties.DataSource = dt_bantu
        lbrf_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        lbrf_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        lbrf_en_id.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Number", "lbrf_code", DevExpress.Utils.HorzAlignment.Default)
        'lbrf_code
        add_column_copy(gv_master, "Work Order Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partnumber", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Work Center", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Machine", "mch_name", DevExpress.Utils.HorzAlignment.Default)

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
        add_column_copy(gv_master, "Down Start", "lbrf_down_start", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Down Stop", "lbrf_down_stop", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Down Elapsed", "lbrf_elapsed_down", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Down Time Reason", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "lbrf_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "lbrf_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "lbrf_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "lbrf_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "lbrf_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_downtime, "lbrfd_lbrf_oid", False)
        add_column(gv_downtime, "lbrfd_down_reason_id", False)
        add_column_copy(gv_downtime, "Reason Code", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_downtime, "Start", "lbrfd_down_start", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_downtime, "Stop", "lbrfd_down_stop", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_downtime, "Duration", "lbrfd_elapsed_down", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_downtime, "Remarks", "lbrfd_down_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_downtime_edit, "lbrfd_lbrf_oid", False)
        add_column(gv_downtime_edit, "lbrfd_down_reason_id", False)
        add_column(gv_downtime_edit, "Reason Code", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit_datetime(gv_downtime_edit, "Start", "lbrfd_down_start", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_edit_datetime(gv_downtime_edit, "Stop", "lbrfd_down_stop", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_downtime_edit, "Duration", "lbrfd_elapsed_down", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_downtime_edit, "Remarks", "lbrfd_down_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_reject, "lbrfd_lbrf_oid", False)
        add_column(gv_reject, "lbrfd_reason_id", "lbrfd_reason_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_reject, "Reason Code", "qc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_reject, "Qty Reject", "lbrfd_qty_reject", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_reject, "Type", "type_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_reject_edit, "lbrfd_lbrf_oid", False)
        add_column(gv_reject_edit, "lbrfd_reason_id", False)
        'add_column(gv_reject_edit, "Type", "lbrfd_reject_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_reject_edit, "Type", "lbrfd_reject_type", DevExpress.Utils.HorzAlignment.Default, init_le_repo("qc_type"))
        'add_column(gv_reject_edit, "Reason Code", "qc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_reject_edit, "Reason Code", "qc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_reject_edit, "Qty Reject", "lbrfd_qty_reject", DevExpress.Utils.HorzAlignment.Default)


        'add_column(gv_reject, "lbrfd_lbrf_oid", False)
        'add_column_copy(gv_reject, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_reject, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_reject_edit, "lbrfd_lbrfp_id", False)
        'add_column(gv_reject_edit, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_reject_edit, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_person_detail, "lbrfd_lbrfp_id", False)
        add_column_copy(gv_person_detail, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_person_detail, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_person_edit, "lbrfd_lbrfp_id", False)
        add_column(gv_person_edit, "Person Name", "lbrfp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_person_edit, "Group", "lbrfp_group", DevExpress.Utils.HorzAlignment.Default)

        ' === TAMBAHAN BARU: Mengaktifkan Fitur Add Line (Append) ===
        gv_person_edit.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        gv_downtime_edit.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        gv_reject_edit.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        ' ===========================================================

    End Sub

    Public Overrides Function get_sequel() As String
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        get_sequel = "SELECT  " _
                & "  a.lbrf_oid,lbrf_code, " _
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
                & "  a.lbrf_qc_out_reason_id,pt_desc1,lbrf_shift_id, " _
                & "  a.lbrf_qc_in_reason_id,mch_name, " _
                & "  a.lbrf_remarks,lbrf_add_by,lbrf_add_date,lbrf_upd_by,lbrf_upd_date,lbrf_activity_type_id,lbrf_mch_id,lbrfa_desc " _
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
                & "  LEFT outer JOIN public.mch_mstr l ON (a.lbrf_mch_id = l.mch_id) " _
                & " WHERE " _
                & "  a.lbrf_date BETWEEN " & SetDate(pr_txttglawal.DateTime) & " AND " & SetDate(pr_txttglakhir.DateTime) & " " _
                & " and lbrf_en_id=" & pr_entity.EditValue _
                & " ORDER BY " _
                & "  a.lbrf_date"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        lbrf_en_id.ItemIndex = 0
        lbrf_en_id.Focus()
        lbrf_wodr_uid.Tag = ""
        lbrf_wodr_uid.Text = ""
        wc_desc.EditValue = ""
        lbrf_date.DateTime = Now
        lbrf_qty_complete.EditValue = ""
        lbrf_qty_reject.EditValue = ""

        lbrf_start_setup.EditValue = Nothing
        lbrf_stop_setup.EditValue = Nothing
        lbrf_elapsed_setup.EditValue = ""

        lbrf_start_run.EditValue = Nothing
        lbrf_stop_run.EditValue = Nothing
        lbrf_elapsed_run.EditValue = ""

        lbrf_down_start.EditValue = Nothing
        lbrf_down_stop.EditValue = Nothing
        lbrf_elapsed_down.EditValue = ""

        lbrf_down_reason_id.ItemIndex = 0
        lbrf_remarks.EditValue = ""

        'lbrf_person.EditValue = ""
        lbrf_qc_in_reason_id.ItemIndex = 0
        lbrf_qc_out_reason_id.ItemIndex = 0
        lbrf_qty_conversion.EditValue = 0
        lbrf_mch_id.ItemIndex = 0

        ' --- A. GRID PERSON ---
        sSQL = "SELECT  " _
            & "  a.lbrfd_oid, " _
            & "  a.lbrfd_lbrf_oid, " _
            & "  a.lbrfd_lbrfp_id, " _
            & "  b.lbrfp_name, " _
            & "  b.lbrfp_group " _
            & "FROM " _
            & "  public.lbrfd_det_person a " _
            & "  INNER JOIN public.lbrfp_person b ON (a.lbrfd_lbrfp_id = b.lbrfp_id) " _
            & "WHERE " _
            & "  a.lbrfd_lbrf_oid is null"

        dt_edit = GetTableData(sSQL)
        gc_person_edit.DataSource = dt_edit

        ' === TAMBAHAN BARU: Inisialisasi Grid Downtime & Reject agar bisa di-isi ===

        ' --- B. GRID DOWNTIME ---
        sSQL = "SELECT lbrfd_oid, lbrfd_lbrf_oid, lbrfd_down_reason_id, " & _
               "'' as code_name, " & _
               "lbrfd_down_start, lbrfd_down_stop, lbrfd_elapsed_down " & _
               "FROM public.lbrfd_det_down WHERE lbrfd_lbrf_oid is null"
        Dim dt_down As DataTable = GetTableData(sSQL)
        gc_downtime_edit.DataSource = dt_down

        ' --- C. GRID REJECT ---

        sSQL = "SELECT  " _
            & "  a.lbrfd_oid, " _
            & "  a.lbrfd_lbrf_oid, " _
            & "  a.lbrfd_reason_id, " _
            & "  a.lbrfd_qty_reject, " _
            & "  a.lbrfd_reject_type, " _
            & "  b.qc_desc, " _
            & "  b.qc_type " _
            & "FROM " _
            & "  public.lbrfd_det_reject a " _
            & "  INNER JOIN public.qc_reason_mstr b ON (a.lbrfd_reason_id = b.qc_id)  " _
            & "  AND (a.lbrfd_reject_type = b.qc_type) " _
            & "WHERE " _
            & "  a.lbrfd_lbrf_oid is null"

        Dim dt_reject As DataTable = GetTableData(sSQL)
        gc_reject_edit.DataSource = dt_reject
        ' ========================================================================

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
    End Sub

    ' === TAMBAHAN BARU: Handle InitNewRow untuk Append ===
    Private Sub gv_person_edit_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_person_edit.InitNewRow
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        view.SetRowCellValue(e.RowHandle, "lbrfd_oid", Guid.NewGuid.ToString())
        view.SetRowCellValue(e.RowHandle, "lbrfd_lbrf_oid", _oid_mstr)
        view.SetRowCellValue(e.RowHandle, "lbrfp_name", "")
    End Sub

    Private Sub gv_downtime_edit_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_downtime_edit.InitNewRow
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        view.SetRowCellValue(e.RowHandle, "lbrfd_oid", Guid.NewGuid.ToString())
        view.SetRowCellValue(e.RowHandle, "lbrfd_lbrf_oid", _oid_mstr)
        view.SetRowCellValue(e.RowHandle, "lbrfd_down_start", Now)
        view.SetRowCellValue(e.RowHandle, "lbrfd_down_stop", Now)
        view.SetRowCellValue(e.RowHandle, "lbrfd_elapsed_down", 0)
    End Sub

    Private Sub gv_reject_edit_InitNewRow(sender As Object, e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_reject_edit.InitNewRow
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        view.SetRowCellValue(e.RowHandle, "lbrfd_oid", Guid.NewGuid.ToString())
        view.SetRowCellValue(e.RowHandle, "qc_desc", "")
        view.SetRowCellValue(e.RowHandle, "lbrfd_lbrf_oid", _oid_mstr)
        view.SetRowCellValue(e.RowHandle, "lbrfd_qty_reject", 0)
    End Sub
    ' ======================================================

    Public Overrides Function insert() As Boolean
        hitung()
        calc_elapsed()
        _oid_mstr = Guid.NewGuid.ToString
        Try
            sSQLs.Clear()
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        Dim _code As String
                        _code = master_new.PGSqlConn.GetNewNumberYM("lbrf_mstr", "lbrf_code", 5, "LB" _
                            & lbrf_en_id.GetColumnValue("en_code") & (master_new.PGSqlConn.CekTanggal.Date.ToString("yyMM")) & master_new.ClsVar.sServerCode)

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.lbrf_mstr " _
                                            & "( " _
                                            & "  lbrf_oid,lbrf_code, " _
                                            & "  lbrf_dom_id, " _
                                            & "  lbrf_en_id, " _
                                            & "  lbrf_wodr_uid, " _
                                            & "  lbrf_qty_complete, " _
                                            & "  lbrf_qty_reject, " _
                                            & "  lbrf_date, " _
                                            & "  lbrf_start_setup, " _
                                            & "  lbrf_stop_setup, " _
                                            & "  lbrf_elapsed_setup, " _
                                            & "  lbrf_start_run, " _
                                            & "  lbrf_stop_run, " _
                                            & "  lbrf_elapsed_run, " _
                                            & "  lbrf_down_start, " _
                                            & "  lbrf_down_stop, " _
                                            & "  lbrf_elapsed_down, " _
                                            & "  lbrf_down_reason_id,lbrf_mch_id, " _
                                            & "  lbrf_remarks,lbrf_qc_in_reason_id,lbrf_qc_out_reason_id,lbrf_person,lbrf_activity_type_id,lbrf_shift_id,lbrf_qty_conversion, " _
                                            & "  lbrf_add_by, " _
                                            & "  lbrf_add_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_oid_mstr.ToString) & ",  " _
                                            & SetSetring(_code) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(lbrf_en_id.EditValue) & ",  " _
                                            & SetSetring(lbrf_wodr_uid.Tag.ToString) & ",  " _
                                            & SetDec(lbrf_qty_complete.EditValue) & ",  " _
                                            & SetDec(lbrf_qty_reject.EditValue) & ",  " _
                                            & SetDate(lbrf_date.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_start_setup.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_stop_setup.EditValue) & ",  " _
                                            & SetDec(lbrf_elapsed_setup.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_start_run.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_stop_run.EditValue) & ",  " _
                                            & SetDec(lbrf_elapsed_run.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_down_start.EditValue) & ",  " _
                                            & SetDateNTime(lbrf_down_stop.EditValue) & ",  " _
                                            & SetDec(lbrf_elapsed_down.EditValue) & ",  " _
                                            & SetInteger(lbrf_down_reason_id.EditValue) & ",  " _
                                            & SetInteger(lbrf_mch_id.EditValue) & ",  " _
                                            & SetSetring(lbrf_remarks.EditValue) & ",  " _
                                            & SetInteger(lbrf_qc_in_reason_id.EditValue) & ",  " _
                                            & SetInteger(lbrf_qc_out_reason_id.EditValue) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetInteger(lbrf_activity_type_id.EditValue) & ",  " _
                                            & SetInteger(lbrf_shift_id.EditValue) & ",  " _
                                            & SetDec(lbrf_qty_conversion.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update wr_routing
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wodr_routing    " _
                                            & "SET  " _
                                            & "  wodr_qty_complete = coalesce(wodr_qty_complete,0) + " & SetDec(lbrf_qty_complete.EditValue) & ",  " _
                                            & "  wodr_qty_reject = coalesce(wodr_qty_reject,0) + " & SetDec(lbrf_qty_reject.EditValue) & ",  " _
                                            & "  wodr_run = coalesce(wodr_run,0) + " & SetDec(lbrf_elapsed_run.EditValue) & ",  " _
                                            & "  wodr_setup = coalesce(wodr_setup,0) + " & SetDec(lbrf_elapsed_setup.EditValue) & ",  " _
                                            & "  wodr_down = coalesce(wodr_down,0) + " & SetDec(lbrf_elapsed_down.EditValue) & "  " _
                                            & "WHERE  " _
                                            & "  wodr_uid = " & SetSetring(lbrf_wodr_uid.Tag.ToString) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert WO Labor Feedback " & _code & " " & lbrf_wodr_uid.Text)

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        ' --- 1. INSERT PERSON DETAIL ---
                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.lbrfd_det_person " _
                                & "( " _
                                & "  lbrfd_oid, " _
                                & "  lbrfd_lbrf_oid, " _
                                & "  lbrfd_lbrfp_id " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_oid_mstr.ToString) & ",  " _
                                & SetInteger(dr("lbrfd_lbrfp_id")) & " " _
                                & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        ' --- 2. INSERT DOWNTIME DETAIL (TAMBAHAN BARU) ---
                        Dim dt_downtime As DataTable = CType(gc_downtime_edit.DataSource, DataTable)
                        For Each dr As DataRow In dt_downtime.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO public.lbrfd_det_down " _
                                & "(lbrfd_oid, lbrfd_lbrf_oid, lbrfd_down_reason_id, lbrfd_down_start, lbrfd_down_stop, lbrfd_elapsed_down) " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ", " _
                                & SetSetring(_oid_mstr.ToString) & ", " _
                                & SetInteger(dr("lbrfd_down_reason_id")) & ", " _
                                & SetDateNTime(dr("lbrfd_down_start")) & ", " _
                                & SetDateNTime(dr("lbrfd_down_stop")) & ", " _
                                & SetDec(dr("lbrfd_elapsed_down")) & " " _
                                & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        ' --- 3. INSERT REJECT DETAIL (TAMBAHAN BARU) ---
                        Dim dt_reject As DataTable = CType(gc_reject_edit.DataSource, DataTable)
                        For Each dr As DataRow In dt_reject.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO public.lbrfd_det_reject " _
                                & "(lbrfd_oid, lbrfd_lbrf_oid, lbrfd_reason_id, lbrfd_qty_reject, lbrfd_reject_type) " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ", " _
                                & SetSetring(_oid_mstr.ToString) & ", " _
                                & SetInteger(dr("lbrfd_reason_id")) & ", " _
                                & SetDec(dr("lbrfd_qty_reject")) & ", " _
                                & SetSetring(dr("lbrfd_reject_type")) & " " _
                                & ")"
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If
                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_oid_mstr), "lbrf_oid")
                        insert = True
                    Catch ex As nPgSqlException
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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

    'Private Function get_bk_ac_id(ByVal par_bk_id As Integer) As Integer
    '    get_bk_ac_id = -1
    '    Try
    '        Using objcek As New master_new.CustomCommand
    '            With objcek
    '                '.Command.Open()
    '                '.Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select bk_ac_id from bk_mstr where bk_id = " + par_bk_id.ToString
    '                .InitializeCommand()
    '                .DataReader = .ExecuteReader
    '                While .DataReader.Read
    '                    get_bk_ac_id = .DataReader("bk_ac_id")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Exit Function
    '    End Try
    '    Return get_bk_ac_id
    'End Function

    Public Overrides Function cancel_data() As Boolean
        Return MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        'Box("This menu is not available")
        'Return False
        'Exit Function
        If MyBase.edit_data = True Then


            lbrf_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                _oid_mstr = .Item("lbrf_oid").ToString
                lbrf_en_id.EditValue = .Item("lbrf_en_id")
                lbrf_wodr_uid.Tag = .Item("lbrf_wodr_uid").ToString
                lbrf_wodr_uid.Text = .Item("wo_code")
                wc_desc.EditValue = .Item("wc_desc")
                lbrf_activity_type_id.EditValue = .Item("lbrf_activity_type_id")
                lbrf_mch_id.EditValue = .Item("lbrf_mch_id")

                lbrf_date.DateTime = .Item("lbrf_date")
                lbrf_qty_complete.EditValue = SetNumber(.Item("lbrf_qty_complete"))
                lbrf_qty_reject.EditValue = SetNumber(.Item("lbrf_qty_reject"))
                lbrf_shift_id.EditValue = .Item("lbrf_shift_id")
                lbrf_qc_in_reason_id.EditValue = .Item("lbrf_qc_in_reason_id")
                lbrf_qc_out_reason_id.EditValue = .Item("lbrf_qc_out_reason_id")

                lbrf_start_setup.EditValue = SetNothing(.Item("lbrf_start_setup"))
                lbrf_stop_setup.EditValue = SetNothing(.Item("lbrf_stop_setup"))
                lbrf_elapsed_setup.EditValue = SetNumber(.Item("lbrf_elapsed_setup"))

                lbrf_start_run.EditValue = SetNothing(.Item("lbrf_start_run"))
                lbrf_stop_run.EditValue = SetNothing(.Item("lbrf_stop_run"))
                lbrf_elapsed_run.EditValue = SetNumber(.Item("lbrf_elapsed_run"))

                lbrf_down_start.EditValue = SetNothing(.Item("lbrf_down_start"))
                lbrf_down_stop.EditValue = SetNothing(.Item("lbrf_down_stop"))
                lbrf_elapsed_down.EditValue = SetNumber(.Item("lbrf_elapsed_down"))

                lbrf_down_reason_id.EditValue = .Item("lbrf_down_reason_id")
                lbrf_remarks.EditValue = SetString(.Item("lbrf_remarks"))

                lbrf_qty_conversion.EditValue = .Item("lbrf_qty_conversion")

                sSQL = "SELECT  " _
                  & "  a.lbrfd_oid, " _
                  & "  a.lbrfd_lbrf_oid, " _
                  & "  a.lbrfd_lbrfp_id, " _
                  & "  b.lbrfp_name, " _
                  & "  b.lbrfp_group " _
                  & "FROM " _
                  & "  public.lbrfd_det_person a " _
                  & "  INNER JOIN public.lbrfp_person b ON (a.lbrfd_lbrfp_id = b.lbrfp_id) " _
                  & "WHERE " _
                  & "  a.lbrfd_lbrf_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "'"

                dt_edit = GetTableData(sSQL)
                gc_person_edit.DataSource = dt_edit
                gv_person_edit.BestFitColumns()

                sSQL = "SELECT  " _
                    & "  down.lbrfd_oid, " _
                    & "  down.lbrfd_lbrf_oid, " _
                    & "  down.lbrfd_down_reason_id, " _
                    & "  code.code_name, " _
                    & "  code.code_desc, " _
                    & "  down.lbrfd_down_start, " _
                    & "  down.lbrfd_down_stop, " _
                    & "  down.lbrfd_elapsed_down, " _
                    & "  down.lbrfd_down_remarks " _
                    & "FROM " _
                    & "  public.lbrfd_det_down down " _
                    & "  INNER JOIN public.code_mstr code ON (down.lbrfd_down_reason_id = code.code_id) " _
                    & "WHERE " _
                    & "  down.lbrfd_lbrf_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "' " _
                    & "ORDER BY " _
                    & "  down.lbrfd_down_start"

                dt_edit_down = GetTableData(sSQL)
                gc_downtime_edit.DataSource = dt_edit_down
                gv_downtime_edit.BestFitColumns()

              

                sSQL = "SELECT  " _
                    & "  reject.lbrfd_oid, " _
                    & "  reject.lbrfd_lbrf_oid, " _
                    & "  reject.lbrfd_reason_id, " _
                    & "  reject.lbrfd_qty_reject, " _
                    & "  qc_type as lbrfd_reject_type, case when qc_type='I' then 'QC IN' else 'QC OUT' end as type_desc, " _
                    & "  reason.qc_desc " _
                    & "FROM " _
                    & "  public.lbrfd_det_reject reject " _
                    & "  INNER JOIN public.qc_reason_mstr reason ON (reject.lbrfd_reason_id = reason.qc_id) " _
                    & "WHERE " _
                    & "  reject.lbrfd_lbrf_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "'"


                dt_edit_reject = GetTableData(sSQL)
                gc_reject_edit.DataSource = dt_edit_reject
                gv_reject_edit.BestFitColumns()
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        sSQLs.Clear()
        hitung()
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        'Delete lbrfd_det_person
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from lbrfd_det_person where lbrfd_lbrf_oid = " & SetSetring(_oid_mstr) & " "
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                           & "  public.lbrf_mstr   " _
                                           & "SET  " _
                                           & "  lbrf_en_id = " & SetInteger(lbrf_en_id.EditValue) & ",  " _
                                           & "  lbrf_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                           & "  lbrf_upd_date = " & "current_timestamp" & ",  " _
                                           & "  lbrf_wodr_uid = " & SetSetring(lbrf_wodr_uid.Tag) & ",  " _
                                           & "  lbrf_date = " & SetDate(lbrf_date.DateTime) & ",  " _
                                           & "  lbrf_qty_complete = " & SetDec(lbrf_qty_complete.EditValue) & ",  " _
                                           & "  lbrf_qty_reject = " & SetDec(lbrf_qty_reject.EditValue) & ",  " _
                                           & "  lbrf_qty_conversion = " & SetDec(lbrf_qty_conversion.EditValue) & ",  " _
                                           & "  lbrf_start_setup = " & SetDateNTime(lbrf_start_setup.EditValue) & ",  " _
                                           & "  lbrf_stop_setup = " & SetDateNTime(lbrf_stop_setup.EditValue) & ",  " _
                                           & "  lbrf_elapsed_setup = " & SetDec(lbrf_elapsed_setup.EditValue) & ",  " _
                                           & "  lbrf_start_run = " & SetDateNTime(lbrf_start_run.EditValue) & ",  " _
                                           & "  lbrf_stop_run = " & SetDateNTime(lbrf_stop_run.EditValue) & ",  " _
                                           & "  lbrf_elapsed_run = " & SetDec(lbrf_elapsed_run.EditValue) & ",  " _
                                           & "  lbrf_down_start = " & SetDateNTime(lbrf_down_start.EditValue) & ",  " _
                                           & "  lbrf_down_stop = " & SetDateNTime(lbrf_down_stop.EditValue) & ",  " _
                                           & "  lbrf_elapsed_down = " & SetDec(lbrf_elapsed_down.EditValue) & ",  " _
                                           & "  lbrf_down_reason_id = " & SetInteger(lbrf_down_reason_id.EditValue) & ",  " _
                                           & "  lbrf_qc_out_reason_id = " & SetInteger(lbrf_qc_out_reason_id.EditValue) & ",  " _
                                           & "  lbrf_activity_type_id = " & SetInteger(lbrf_activity_type_id.EditValue) & ",  " _
                                           & "  lbrf_shift_id = " & SetInteger(lbrf_shift_id.EditValue) & ",  " _
                                           & "  lbrf_remarks = " & SetSetring(lbrf_remarks.Text) & ",  " _
                                           & "  lbrf_mch_id = " & SetSetring(lbrf_mch_id.EditValue) & "  " _
                                           & "WHERE  " _
                                           & "  lbrf_oid = " & SetSetring(_oid_mstr) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        For Each dr As DataRow In dt_edit.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.lbrfd_det_person " _
                                & "( " _
                                & "  lbrfd_oid, " _
                                & "  lbrfd_lbrf_oid, " _
                                & "  lbrfd_lbrfp_id " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_oid_mstr.ToString) & ",  " _
                                & SetInteger(dr("lbrfd_lbrfp_id")) & " " _
                                & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from lbrfd_det_down where lbrfd_lbrf_oid = " & SetSetring(_oid_mstr) & " "
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit_down.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.lbrfd_det_down " _
                                & "( " _
                                & "  lbrfd_oid, " _
                                & "  lbrfd_lbrf_oid, " _
                                & "  lbrfd_down_reason_id, lbrfd_elapsed_down, lbrfd_down_start, lbrfd_down_stop, lbrfd_down_remarks " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_oid_mstr.ToString) & ",  " _
                                & SetInteger(dr("lbrfd_down_reason_id")) & ", " _
                                & SetDec(dr("lbrfd_elapsed_down")) & ", " _
                                & SetDateNTime(dr("lbrfd_down_start")) & ", " _
                                & SetDateNTime(dr("lbrfd_down_stop")) & ", " _
                                & SetSetring(dr("lbrfd_down_remarks")) & " " _
                                & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from lbrfd_det_reject where lbrfd_lbrf_oid = " & SetSetring(_oid_mstr) & " "
                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        For Each dr As DataRow In dt_edit_reject.Rows
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                & "  public.lbrfd_det_reject " _
                                & "( " _
                                & "  lbrfd_oid, " _
                                & "  lbrfd_lbrf_oid, " _
                                & "  lbrfd_reason_id, lbrfd_qty_reject, lbrfd_reject_type " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetSetring(_oid_mstr.ToString) & ",  " _
                                & SetInteger(dr("lbrfd_reason_id")) & ", " _
                                & SetDec(dr("lbrfd_qty_reject")) & ", " _
                                & SetSetring(dr("lbrfd_reject_type")) & " " _
                                & ")"

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                        Next


                        'update wr_routing penyesuaian awal
                        Dim _row As Integer = BindingContext(ds.Tables(0)).Position
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wodr_routing    " _
                                            & "SET  " _
                                            & "  wodr_qty_complete = coalesce(wodr_qty_complete,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_qty_complete")) & ",  " _
                                            & "  wodr_qty_reject = coalesce(wodr_qty_reject,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_qty_reject")) & ",  " _
                                            & "  wodr_run = coalesce(wodr_run,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_run")) & ",  " _
                                            & "  wodr_setup = coalesce(wodr_setup,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_setup")) & ",  " _
                                            & "  wodr_down = coalesce(wodr_down,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_down")) & "  " _
                                            & "WHERE  " _
                                            & "  wodr_uid = " & SetSetring(ds.Tables(0).Rows(_row).Item("lbrf_wodr_uid").ToString) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'update wr_routing
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.wodr_routing    " _
                                            & "SET  " _
                                            & "  wodr_qty_complete = coalesce(wodr_qty_complete,0) + " & SetDec(lbrf_qty_complete.EditValue) & ",  " _
                                            & "  wodr_qty_reject = coalesce(wodr_qty_reject,0) + " & SetDec(lbrf_qty_reject.EditValue) & ",  " _
                                            & "  wodr_run = coalesce(wodr_run,0) + " & SetDec(lbrf_elapsed_run.EditValue) & ",  " _
                                            & "  wodr_setup = coalesce(wodr_setup,0) + " & SetDec(lbrf_elapsed_setup.EditValue) & ",  " _
                                            & "  wodr_down = coalesce(wodr_down,0) + " & SetDec(lbrf_elapsed_down.EditValue) & "  " _
                                            & "WHERE  " _
                                            & "  wodr_uid = " & SetSetring(lbrf_wodr_uid.Tag) & " "

                        sSQLs.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_oid_mstr.ToString), "lbrf_oid")
                        edit = True
                    Catch ex As NpgsqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " akan menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            sSQLs.Clear()
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            'update wr_routing penyesuaian awal
                            Dim _row As Integer = BindingContext(ds.Tables(0)).Position
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                       & "  public.wodr_routing    " _
                                       & "SET  " _
                                       & "  wodr_qty_complete = coalesce(wodr_qty_complete,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_qty_complete")) & ",  " _
                                       & "  wodr_qty_reject = coalesce(wodr_qty_reject,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_qty_reject")) & ",  " _
                                       & "  wodr_run = coalesce(wodr_run,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_run")) & ",  " _
                                       & "  wodr_setup = coalesce(wodr_setup,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_setup")) & ",  " _
                                       & "  wodr_down = coalesce(wodr_down,0) - " & SetDec(ds.Tables(0).Rows(_row).Item("lbrf_elapsed_down")) & "  " _
                                       & "WHERE  " _
                                       & "  wodr_uid = " & SetSetring(ds.Tables(0).Rows(_row).Item("lbrf_wodr_uid")) & " "

                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from lbrf_mstr where lbrf_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            sSQLs.Add(.Command.CommandText)
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete WO Labor Feedback " & ds.Tables(0).Rows(_row).Item("lbrf_code") & " " & ds.Tables(0).Rows(_row).Item("wo_code"))
                            sSQLs.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(sSQLs)
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim sSQL As String
        Try
            If lbrf_wodr_uid.Tag = "" Then
                Box("WO Code can't blank")
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try

        Return before_save
    End Function


    Private Sub hitung()
        Try
            If Not lbrf_start_setup.EditValue Is Nothing And Not lbrf_stop_setup.EditValue Is Nothing Then
                Dim _detik As Integer
                Dim _jam As Double = 0

                _detik = DateDiff(DateInterval.Second, lbrf_start_setup.EditValue, lbrf_stop_setup.EditValue)
                _jam = (_detik / 3600)

                lbrf_elapsed_setup.EditValue = _jam
            End If

            If Not lbrf_start_run.EditValue Is Nothing And Not lbrf_stop_run.EditValue Is Nothing Then
                Dim _detik As Integer
                Dim _jam As Double = 0

                _detik = DateDiff(DateInterval.Second, lbrf_start_run.EditValue, lbrf_stop_run.EditValue)
                _jam = (_detik / 3600)

                lbrf_elapsed_run.EditValue = _jam

            End If

            If Not lbrf_down_start.EditValue Is Nothing And Not lbrf_down_stop.EditValue Is Nothing Then
                Dim _detik As Integer
                Dim _jam As Double = 0

                _detik = DateDiff(DateInterval.Second, lbrf_down_start.EditValue, lbrf_down_stop.EditValue)
                _jam = (_detik / 3600)

                lbrf_elapsed_down.EditValue = _jam
            End If

            dt_edit.AcceptChanges()
            dt_edit_down.AcceptChanges()
            dt_edit_reject.AcceptChanges()

            Dim _qty_reject As Double = 0
            For Each dr As DataRow In dt_edit_reject.Rows
                _qty_reject = _qty_reject + SetNumber(dr("lbrfd_qty_reject"))
            Next


            Dim _qty_down As Double = 0
            For Each dr As DataRow In dt_edit_down.Rows
                'dr("lbrfd_elapsed_down") = 0
                _qty_down = _qty_down + SetNumber(dr("lbrfd_elapsed_down"))
            Next

            lbrf_qty_reject.EditValue = _qty_reject
            lbrf_elapsed_down.EditValue = _qty_down
        Catch ex As Exception

            Pesan(Err)
        End Try
    End Sub


    Private Sub calc_elapsed()
        If Not lbrf_start_run.EditValue Is Nothing And Not lbrf_stop_run.EditValue Is Nothing Then
            Dim _detik As Integer
            Dim _jam As Double = 0

            _detik = DateDiff(DateInterval.Second, lbrf_start_run.EditValue, lbrf_stop_run.EditValue)
            _jam = (_detik / 3600)

            lbrf_elapsed_run.EditValue = _jam
        End If
        If Not lbrf_start_setup.EditValue Is Nothing And Not lbrf_stop_setup.EditValue Is Nothing Then
            Dim _detik As Integer
            Dim _jam As Double = 0

            _detik = DateDiff(DateInterval.Second, lbrf_start_setup.EditValue, lbrf_stop_setup.EditValue)
            _jam = (_detik / 3600)

            lbrf_elapsed_setup.EditValue = _jam
        End If
    End Sub

    Private Sub wrd_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbrf_en_id.EditValueChanged
        init_le(lbrf_down_reason_id, "down_reason", lbrf_en_id.EditValue)
        init_le(lbrf_qc_in_reason_id, "qc_in", lbrf_en_id.EditValue)
        init_le(lbrf_qc_out_reason_id, "qc_out", lbrf_en_id.EditValue)
        init_le(lbrf_activity_type_id, "activity", lbrf_en_id.EditValue)
        init_le(lbrf_shift_id, "shift_mstr", lbrf_en_id.EditValue)
        init_le(lbrf_mch_id, "mch_mstr", lbrf_en_id.EditValue)
    End Sub

    Private Sub wrd_wr_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles lbrf_wodr_uid.ButtonClick
        Dim frm As New FWORoutingSearch
        frm.set_win(Me)
        frm._en_id = lbrf_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub lbrf_start_setup_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbrf_start_setup.EditValueChanged, lbrf_stop_setup.EditValueChanged, lbrf_start_run.EditValueChanged, lbrf_stop_run.EditValueChanged, lbrf_down_start.EditValueChanged, lbrf_down_stop.EditValueChanged
        hitung()
    End Sub

    Private Sub gv_person_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_person_edit.DoubleClick
        Try
            Dim _col As String = gv_person_edit.FocusedColumn.Name
            Dim _row As Integer = gv_person_edit.FocusedRowHandle

            If _col = "lbrfp_name" Then
                Dim frm As New FPersonSearch
                frm.set_win(Me)
                frm._row = _row
                frm.type_form = True
                frm.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_downtime_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_downtime_edit.DoubleClick
        Try
            Dim _col As String = gv_downtime_edit.FocusedColumn.FieldName
            Dim _row As Integer = gv_downtime_edit.FocusedRowHandle

            If _col = "code_name" Then
                Dim frm As New FDownReaseonSearch
                frm.set_win(Me)
                frm._row = _row
                frm.type_form = True
                frm.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_reject_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_reject_edit.DoubleClick
        Try
            Dim _col As String = gv_reject_edit.FocusedColumn.Name
            Dim _row As Integer = gv_reject_edit.FocusedRowHandle
            If _col = "qc_desc" Then
                Dim frm As New FRejectReaseonSearch
                frm.set_win(Me)
                frm._row = _row
                frm._type = gv_reject_edit.GetRowCellValue(_row, "lbrfd_reject_type")
                frm.type_form = True
                frm.ShowDialog()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged

        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If
        Catch ex As Exception
            Exit Sub
        End Try

        sSQL = "SELECT  " _
            & "  a.lbrfd_oid, " _
            & "  a.lbrfd_lbrf_oid, " _
            & "  a.lbrfd_lbrfp_id, " _
            & "  b.lbrfp_name, " _
            & "  b.lbrfp_group " _
            & "FROM " _
            & "  public.lbrfd_det_person a " _
            & "  INNER JOIN public.lbrfp_person b ON (a.lbrfd_lbrfp_id = b.lbrfp_id) " _
            & "WHERE " _
            & "  a.lbrfd_lbrf_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "'"

        'dt_edit = GetTableData(sSQL)
        gc_person_detail.DataSource = GetTableData(sSQL)
        gv_person_detail.BestFitColumns()

       
        sSQL = "SELECT  " _
            & "  down.lbrfd_oid, " _
            & "  down.lbrfd_lbrf_oid, " _
            & "  down.lbrfd_down_reason_id, " _
            & "  code.code_name, " _
            & "  code.code_desc, " _
            & "  down.lbrfd_down_start, " _
            & "  down.lbrfd_down_stop, " _
            & "  down.lbrfd_elapsed_down, " _
            & "  down.lbrfd_down_remarks " _
            & "FROM " _
            & "  public.lbrfd_det_down down " _
            & "  INNER JOIN public.code_mstr code ON (down.lbrfd_down_reason_id = code.code_id) " _
            & "WHERE " _
            & "  down.lbrfd_lbrf_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "' " _
            & "ORDER BY " _
            & "  down.lbrfd_down_start"

        'dt_edit = GetTableData(sSQL)
        gc_downtime.DataSource = GetTableData(sSQL)
        gv_downtime.BestFitColumns()

        'sSQL = "SELECT  " _
        '   & "  a.lbrfd_oid, " _
        '   & "  a.lbrfd_lbrf_oid, " _
        '   & "  a.lbrfd_lbrfp_id, " _
        '   & "  b.lbrfp_name, " _
        '   & "  b.lbrfp_group " _
        '   & "FROM " _
        '   & "  public.lbrfd_det_person a " _
        '   & "  INNER JOIN public.lbrfp_person b ON (a.lbrfd_lbrfp_id = b.lbrfp_id) " _
        '   & "WHERE " _
        '   & "  a.lbrfd_lbrf_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "'"

        sSQL = "SELECT  " _
            & "  reject.lbrfd_oid, " _
            & "  reject.lbrfd_lbrf_oid, " _
            & "  reject.lbrfd_reason_id, " _
            & "  reject.lbrfd_qty_reject, " _
            & "  qc_type as lbrfd_reject_type, case when qc_type='I' then 'QC IN' else 'QC OUT' end as type_desc, " _
            & "  reason.qc_desc " _
            & "FROM " _
            & "  public.lbrfd_det_reject reject " _
            & "  INNER JOIN public.qc_reason_mstr reason ON (reject.lbrfd_reason_id = reason.qc_id) " _
            & "WHERE " _
            & "  reject.lbrfd_lbrf_oid ='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrf_oid").ToString & "'"


        'dt_edit = GetTableData(sSQL)
        gc_reject.DataSource = GetTableData(sSQL)
        gv_reject.BestFitColumns()

        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Private Sub gv_downtime_edit_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_downtime_edit.CellValueChanged
        ' Hanya proses jika kolom yang berubah adalah lbrfd_down_stop
        If (e.Column.FieldName = "lbrfd_down_stop" Or e.Column.FieldName = "lbrfd_down_start") AndAlso e.RowHandle >= 0 Then
            Try
                ' Ambil nilai start & stop dari row yang sama
                Dim startVal As Object = gv_downtime_edit.GetRowCellValue(e.RowHandle, "lbrfd_down_start")
                Dim stopVal As Object = gv_downtime_edit.GetRowCellValue(e.RowHandle, "lbrfd_down_stop")

                ' Validasi data
                If Not IsDBNull(startVal) AndAlso Not IsDBNull(stopVal) _
                   AndAlso TypeOf startVal Is DateTime AndAlso TypeOf stopVal Is DateTime Then

                    Dim startTime As DateTime = Convert.ToDateTime(startVal)
                    Dim stopTime As DateTime = Convert.ToDateTime(stopVal)

                    ' Hitung selisih dalam jam (desimal)
                    If stopTime > startTime Then
                        Dim elapsedHours As Double = (stopTime - startTime).TotalHours
                        gv_downtime_edit.SetRowCellValue(e.RowHandle, "lbrfd_elapsed_down", elapsedHours)
                    Else
                        gv_downtime_edit.SetRowCellValue(e.RowHandle, "lbrfd_elapsed_down", 0)
                    End If
                End If
            Catch ex As Exception
                ' Optional: Log error
                Console.WriteLine("Error hitung elapsed: " & ex.Message)
            End Try
        End If
    End Sub
End Class