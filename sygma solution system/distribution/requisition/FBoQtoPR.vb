Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FBoQtoPR
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim _user_approval As String
    Dim ds_detail As DataSet
    Dim ds_jurnal As DataSet
    Dim _conf_value As String
    Public _boq_oid As String

    Private Sub FVoucherApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        gv_outstanding.Columns("status").VisibleIndex = 0

        _conf_value = func_coll.get_conf_file("wf_requisition")

    End Sub

    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        par_entity.Properties.DataSource = dt_bantu
        par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_entity.ItemIndex = 0

        If _conf_value = "0" Then
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_tran_mstr())
            par_tran_id.Properties.DataSource = dt_bantu
            par_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            par_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
            par_tran_id.ItemIndex = 0
        Else
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_transaction())
            par_tran_id.Properties.DataSource = dt_bantu
            par_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
            par_tran_id.Properties.ValueMember = dt_bantu.Columns("tranu_tran_id").ToString
            par_tran_id.ItemIndex = 0
        End If

    End Sub

    Public Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_vend", par_entity.EditValue))
        par_ptnr_id.Properties.DataSource = dt_bantu
        par_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        par_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        par_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cmaddr_mstr", par_entity.EditValue))
        par_cmaddr_id.Properties.DataSource = dt_bantu
        par_cmaddr_id.Properties.DisplayMember = dt_bantu.Columns("cmaddr_name").ToString
        par_cmaddr_id.Properties.ValueMember = dt_bantu.Columns("cmaddr_id").ToString
        par_cmaddr_id.ItemIndex = 0



        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("si_mstr", par_entity.EditValue))
        par_si_id.Properties.DataSource = dt_bantu
        par_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        par_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        par_si_id.ItemIndex = 0

        par_type.Text = par_type.Properties.Items(0)
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_outstanding, "boqs_oid", False)
        add_column(gv_outstanding, "boqs_boq_oid", False)
        add_column(gv_outstanding, "boqs_pt_id", False)
        add_column(gv_outstanding, "pt_um", False)
        add_column(gv_outstanding, "Pt Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_outstanding, "Pt Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_outstanding, "Pt Desc 2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_outstanding, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_outstanding, "Qty Generate", "qty_generate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_outstanding, "Unit Measure", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_outstanding, "Is Manual", "boqs_is_manual", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_generate, "reqd_req_oid", False)
        add_column_copy(gv_generate, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Description1", "reqd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Description2", "reqd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_generate, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "Discount", "reqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_generate, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_generate, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_generate, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_generate, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_generate, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)


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
        add_column_copy(gv_master, "Date Create", "req_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "req_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "req_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "reqd_req_oid", False)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Code Relation", "req_code_relation", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Supplier", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "reqd_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "reqd_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remarks", "reqd_rmks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "reqd_end_user", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty", "reqd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Processed", "reqd_qty_processed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Completed", "reqd_qty_completed", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost", "reqd_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Discount", "reqd_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Need Date", "reqd_need_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Due Date", "reqd_due_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "UM Conversion", "reqd_um_conv", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Real", "reqd_qty_real", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty * Cost", "reqd_qty_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_detail, "Status", "reqd_status", DevExpress.Utils.HorzAlignment.Default)

    End Sub


    Private Sub par_project_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles par_project.ButtonClick
        Dim frm As New FProjectSearch
        frm.set_win(Me)
        frm._en_id = par_entity.EditValue
        frm.ShowDialog()
        frm.type_form = True
    End Sub

    Private Sub par_select_all_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles par_select_all.EditValueChanged
        Try
            For i As Integer = 0 To gv_outstanding.RowCount - 1
                gv_outstanding.SetRowCellValue(i, "status", par_select_all.EditValue)
            Next
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try
            Dim _count As Integer = 0

            For i As Integer = 0 To gv_outstanding.RowCount - 1
                If gv_outstanding.GetRowCellValue(i, "status") = True Then
                    _count = _count + 1
                End If
            Next
            If _count = 0 Then
                Box("Nothing data selected")
                Exit Sub
            End If
            If insert() = False Then
                Exit Sub
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Function insert() As Boolean
        Dim _req_oid As Guid
        _req_oid = Guid.NewGuid

        Dim _req_code As String
        Dim _req_total As Double
        Dim i, j As Integer
        Dim _req_trn_id As Integer
        Dim _req_trn_status As String
        Dim ds_bantu As New DataSet

     

        If _conf_value = "1" Then
            ds_bantu = func_data.load_aprv_mstr(par_tran_id.EditValue)
        End If
        ' Exit Function
        '=============================================================================

        _req_trn_id = par_tran_id.EditValue
        _req_trn_status = "D" 'Lansung Default Ke Draft

        _req_code = func_coll.get_transaction_number("PR", par_entity.GetColumnValue("en_code"), "req_mstr", "req_code")

        _req_total = 0
        For i = 0 To gv_outstanding.RowCount - 1
            If gv_outstanding.GetRowCellValue(i, "status") = True Then
                Dim _cost As DataRow = master_new.PGSqlConn.GetRowInfo("select coalesce(invct_cost,0) as invct_cost from invct_table where invct_pt_id=" _
                                                                 & gv_outstanding.GetRowCellValue(i, "boqs_pt_id") & " and invct_en_id=" & par_entity.EditValue _
                                                                 & " and invct_si_id=" & par_si_id.EditValue)
                Dim _cost_value As Double

                If _cost Is Nothing Then
                    _cost_value = 0
                Else
                    _cost_value = _cost(0)
                End If

                _req_total = _req_total + (CDbl(gv_outstanding.GetRowCellValue(i, "reqd_qty_real")) * CDbl(_cost_value))
            End If
           
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
                                            & par_entity.EditValue & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ", " _
                                            & " current_timestamp " & ", " _
                                            & SetSetring(_req_code) & ",  " _
                                            & par_ptnr_id.EditValue & ",  " _
                                            & par_cmaddr_id.EditValue & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetDate(_now) & ",  " _
                                            & SetDate(_now) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring("") & ",  " _
                                            & 0 & ",  " _
                                            & 0 & ",  " _
                                            & par_si_id.EditValue & ",  " _
                                            & SetSetring(par_type.Text.Substring(0, 1)) & ",  " _
                                            & par_project.Tag & ",  " _
                                            & _req_total & ",  " _
                                            & SetSetring(par_tran_id.EditValue) & ",  " _
                                            & SetSetring(_req_trn_status) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To gv_outstanding.RowCount - 1
                            If gv_outstanding.GetRowCellValue(i, "status") = True Then
                                Dim _cost As DataRow = master_new.PGSqlConn.GetRowInfo("select coalesce(invct_cost,0) as invct_cost from invct_table where invct_pt_id=" _
                                                          & gv_outstanding.GetRowCellValue(i, "boqs_pt_id") & " and invct_en_id=" & par_entity.EditValue _
                                                          & " and invct_si_id=" & par_si_id.EditValue)
                                Dim _cost_value As Double

                                If _cost Is Nothing Then
                                    _cost_value = 0
                                Else
                                    _cost_value = _cost(0)
                                End If

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
                                                    & "  reqd_dt,reqd_boqs_oid " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & master_new.ClsVar.sdom_id & ",  " _
                                                    & SetInteger(par_entity.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring(_req_oid.ToString) & ",  " _
                                                    & i & ",  " _
                                                    & "null" & ",  " _
                                                    & "null" & ",  " _
                                                    & SetInteger(par_ptnr_id.EditValue) & ",  " _
                                                    & SetInteger(par_si_id.EditValue) & ",  " _
                                                    & SetInteger(gv_outstanding.GetRowCellValue(i, "boqs_pt_id")) & ",  " _
                                                    & SetSetringDB("") & ",  " _
                                                    & SetSetringDB("") & ",  " _
                                                    & SetDbl(gv_outstanding.GetRowCellValue(i, "qty_generate")) & ",  " _
                                                    & SetInteger(gv_outstanding.GetRowCellValue(i, "pt_um")) & ",  " _
                                                    & SetDbl(_cost_value) & ",  " _
                                                    & SetDbl(0) & ",  " _
                                                    & SetDate(_now) & ",  " _
                                                    & SetDate(_now) & ",  " _
                                                    & SetDbl(gv_outstanding.GetRowCellValue(i, "pt_um")) & ",  " _
                                                    & SetDbl(gv_outstanding.GetRowCellValue(i, "qty_generate")) & ",  " _
                                                    & SetSetringDB(gv_outstanding.GetRowCellValue(i, "pt_desc1")) & ",  " _
                                                    & SetSetringDB(gv_outstanding.GetRowCellValue(i, "pt_desc2")) & ",  " _
                                                    & "current_timestamp" & ",  " _
                                                    & SetSetring(gv_outstanding.GetRowCellValue(i, "boqs_oid")) _
                                                    & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update boqs_stand set boqs_qty_pr=coalesce(boqs_qty_pr,0) + " _
                                                & SetDbl(gv_outstanding.GetRowCellValue(i, "qty_generate")) & " where boqs_oid='" _
                                                & gv_outstanding.GetRowCellValue(i, "boqs_oid").ToString & "'"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                           
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
                                                        & SetInteger(par_entity.EditValue) & ",  " _
                                                        & SetSetring(par_tran_id.EditValue) & ",  " _
                                                        & SetSetring(_req_oid.ToString) & ",  " _
                                                        & SetSetring(_req_code) & ",  " _
                                                        & SetSetring("Requisition") & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_seq")) & ",  " _
                                                        & SetSetring(ds_bantu.Tables(0).Rows(j).Item("aprv_user_id")) & ",  " _
                                                        & SetInteger(0) & ",  " _
                                                        & SetSetring("N") & ",  " _
                                                        & " current_timestamp " & "  " _
                                                        & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        Box("Generate done")
                       

                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
            'Refresh detail generate
            Dim sSql As String
            sSql = "SELECT  " _
               & "  false as status,a.boqs_oid, " _
               & "  a.boqs_boq_oid, " _
               & "  a.boqs_pt_id, " _
               & "  b.pt_code, " _
               & "  b.pt_desc1, " _
               & "  b.pt_desc2, " _
               & "  b.pt_um, " _
               & "  c.code_code, " _
               & "  a.boqs_qty_plan, " _
               & "  coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_open , coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0) as qty_generate, " _
               & "  a.boqs_qty_pr, " _
               & "  a.boqs_qty_po, " _
               & "  a.boqs_qty_receipt, " _
               & "  a.boqs_qty_wo, " _
               & "  a.boqs_is_manual " _
               & "FROM " _
               & "  public.boqs_stand a " _
               & "  INNER JOIN public.boq_mstr d ON (a.boqs_boq_oid = d.boq_oid) " _
               & "  INNER JOIN public.pt_mstr b ON (a.boqs_pt_id = b.pt_id) " _
               & "  INNER JOIN public.code_mstr c ON (b.pt_um = c.code_id) " _
               & "WHERE " _
               & "  a.boqs_boq_oid ='" & _boq_oid & "' " _
               & " and d.boq_en_id in (select user_en_id from tconfuserentity " _
               & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
               & " and coalesce(a.boqs_qty,0) - coalesce(a.boqs_qty_pr,0)>0 " _
               & "ORDER BY " _
               & "  a.boqs_seq"

            gc_outstanding.DataSource = master_new.PGSqlConn.GetTableData(sSql)
            gv_outstanding.BestFitColumns()

            'refresh tab 2
            pr_code.Text = _req_code
            pr_entity.Text = par_entity.Text
            pr_site.Text = par_si_id.Text
            pr_comp_address.Text = par_cmaddr_id.Text


            sSql = "SELECT  " _
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
               & "  CASE  coalesce(reqd_related_type,'X') " _
               & "    WHEN 'R' THEN (SELECT req_code from req_mstr reqm inner join reqd_det reqd on reqd.reqd_req_oid=reqm.req_oid where reqd.reqd_oid = public.reqd_det.reqd_related_oid) " _
               & "    WHEN 'B' THEN (SELECT bp_code  from bp_mstr  inner join bpd_det  on bpd_bp_oid  =bp_oid  where bpd_oid  = public.reqd_det.reqd_related_oid) " _
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
               & "  public.reqd_det.reqd_dt, " _
               & "  public.reqd_det.reqd_pt_desc1, " _
               & "  public.reqd_det.reqd_pt_desc2, " _
               & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
               & "  FROM " _
               & "  public.reqd_det " _
               & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
               & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
               & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
               & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
               & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
               & "  where req_mstr.req_code='" & _req_code & "' " _
               & " Order by reqd_seq"

            gc_generate.DataSource = master_new.PGSqlConn.GetTableData(sSql)
            gv_generate.BestFitColumns()

            'refresh tab 3
            sSql = "SELECT  " _
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
                & "  INNER JOIN public.tran_mstr ON (public.req_mstr.req_tran_id = public.tran_mstr.tran_id) " _
                & " where req_pjc_id= " & par_project.Tag _
                & " and req_en_id in (select user_en_id from tconfuserentity " _
                       & " where userid = " & master_new.ClsVar.sUserID.ToString & ") " _
                & " Order by req_code"

            gc_master.DataSource = master_new.PGSqlConn.GetTableData(sSql)
            gv_master.BestFitColumns()

        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
    Private Sub gv_outstanding_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_outstanding.CellValueChanged
        Try
            If e.Column.Name = "qty_generate" Then
                If e.Value > SetNumber(gv_outstanding.GetRowCellValue(e.RowHandle, "qty_open")) Then
                    MessageBox.Show("Qty Generate Can't Higher Than Qty Open..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    gv_outstanding.CancelUpdateCurrentRow()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub par_entity_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles par_entity.EditValueChanged
        load_cb_en()
    End Sub

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_FocusedRowChanged(Nothing, Nothing)
    End Sub


    Private Sub gv_master_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_master.FocusedRowChanged
        Try
            Dim sSql As String
            sSql = "SELECT  " _
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
               & "  CASE  coalesce(reqd_related_type,'X') " _
               & "    WHEN 'R' THEN (SELECT req_code from req_mstr reqm inner join reqd_det reqd on reqd.reqd_req_oid=reqm.req_oid where reqd.reqd_oid = public.reqd_det.reqd_related_oid) " _
               & "    WHEN 'B' THEN (SELECT bp_code  from bp_mstr  inner join bpd_det  on bpd_bp_oid  =bp_oid  where bpd_oid  = public.reqd_det.reqd_related_oid) " _
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
               & "  public.reqd_det.reqd_dt, " _
               & "  public.reqd_det.reqd_pt_desc1, " _
               & "  public.reqd_det.reqd_pt_desc2, " _
               & "  ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
               & "  FROM " _
               & "  public.reqd_det " _
               & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
               & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
               & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id)               " _
               & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
               & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
               & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid) " _
               & "  where req_mstr.req_code='" & gv_master.GetFocusedRowCellValue("req_code") & "' " _
               & " Order by reqd_seq"

            gc_detail.DataSource = master_new.PGSqlConn.GetTableData(sSql)
            gv_detail.BestFitColumns()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
