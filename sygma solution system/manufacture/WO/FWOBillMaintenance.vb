Imports npgsql
Imports master_new.ModFunction

Public Class FWOBillMaintenance

    Public dt_bantu As DataTable
    Dim dr As DataRow
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public _wo_oid_mstr As String
    Dim ds_edit As DataSet
    Public dt_edit As New DataTable
    Dim status_insert As Boolean = True
    Public _wod_related_oid As String = ""

    Private Sub FWorkOrderRelease_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'par_entity.Properties.DataSource = dt_bantu
        'par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "wo_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wo_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Order", "wo_qty_ord", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Comp.", "wo_qty_comp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Reject", "wo_qty_rjc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Order Date", "wo_ord_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Release Date", "wo_rel_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Due Date", "wo_due_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Yield Percent", "wo_yield_pct", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column(gv_master, "wo_ro_id", False)
        add_column_copy(gv_master, "Routing Desc.", "ro_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "wo_status", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remark", "wo_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "wod_oid", False)
        add_column(gv_detail, "wod_wo_oid", False)
        ' add_column_copy(gv_detail, "Use BOM", "wod_use_bom", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_detail, "wod_pt_bom_id", False)

        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_detail, "Qty Per", "wod_qty_per", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        ' add_column_copy(gv_detail, "Qty Alloc", "wod_qty_alloc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        ' add_column_copy(gv_detail, "Qty Picked", "wod_qty_picked", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Issued", "wod_qty_issued", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Cost", "wod_cost", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "wod_oid", False)
        add_column(gv_edit, "wod_pt_bom_id", False)
        add_column(gv_edit, "Part Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Part Desc", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Operation", "wod_op", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Qty Per", "wod_qty_per", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Req", "wod_qty_req", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

#Region "SQL"
    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                     & "  wo_oid, " _
                     & "  wo_dom_id, " _
                     & "  wo_en_id, " _
                     & "  wo_si_id, " _
                     & "  wo_id, " _
                     & "  wo_code, " _
                     & "  wo_type, " _
                     & "  wo_pt_id, " _
                     & "  wo_qty_ord, " _
                     & "  wo_qty_comp, " _
                     & "  wo_qty_rjc, " _
                     & "  wo_ord_date, " _
                     & "  wo_rel_date, " _
                     & "  wo_due_date, " _
                     & "  wo_yield_pct, " _
                     & "   " _
                     & "  wo_ro_id, " _
                     & "  wo_status, " _
                     & "  wo_remarks, " _
                     & "  si_desc, " _
                     & "  pt_code, " _
                     & "  pt_desc1, " _
                     & "  pt_desc2, " _
                     & "  ro_desc, " _
                     & "  en_desc, " _
                     & "  wo_dt " _
                     & "FROM  " _
                     & "  public.wo_mstr " _
                     & "  INNER JOIN public.en_mstr ON (public.wo_mstr.wo_en_id = public.en_mstr.en_id) " _
                     & "  INNER JOIN public.si_mstr ON (public.wo_mstr.wo_si_id = public.si_mstr.si_id) " _
                     & "  INNER JOIN public.pt_mstr ON (public.wo_mstr.wo_pt_id = public.pt_mstr.pt_id) " _
                     & "  INNER JOIN public.ro_mstr ON (public.wo_mstr.wo_ro_id = public.ro_mstr.ro_id) " _
                     & "  where wo_mstr.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                     & "  and wo_mstr.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                     & "  and wo_en_id in (select user_en_id from tconfuserentity " _
                                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function
    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu")
        Return False
        Exit Function
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
                & "  a.wod_oid, " _
                & "  a.wod_wo_oid, " _
                & "  a.wod_pt_bom_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.wod_op, " _
                & "  a.wod_qty_per, " _
                & "  a.wod_qty_req, " _
                & "  a.wod_qty_alloc, " _
                & "  a.wod_qty_picked, " _
                & "  a.wod_qty_issued, " _
                & "  a.wod_cost " _
                & "FROM " _
                & "  public.wod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                & "  INNER JOIN public.wo_mstr c ON (a.wod_wo_oid = c.wo_oid) " _
                & "  where c.wo_ord_date >= " & SetDate(pr_txttglawal.DateTime.Date) _
                & "  and c.wo_ord_date <= " & SetDate(pr_txttglakhir.DateTime.Date) _
                & " ORDER BY " _
                & "  b.pt_code"

        'sql = "SELECT  " _
        '    & "  wod_oid, " _
        '    & "  wod_wo_oid, " _
        '    & "  wod_use_bom, " _
        '    & "  wod_pt_bom_id, " _
        '    & "  wod_comp, " _
        '    & "  wod_op, " _
        '    & "  wod_qty_per, " _
        '    & "  wod_qty_req, " _
        '    & "  wod_qty_alloc, " _
        '    & "  wod_qty_picked, " _
        '    & "  wod_qty_issued, " _
        '    & "  wod_cost, " _
        '    & "   CASE WHEN wod_use_bom = 'Y' " _
        '    & "   THEN (SELECT bom_mstr.bom_desc from bom_mstr where bom_id=wod_pt_bom_id) " _
        '    & "   ELSE (SELECT pt_mstr.pt_desc1 from pt_mstr where pt_id=wod_pt_bom_id) " _
        '    & "   END AS ptbomdesc, " _
        '    & "  wod_dt " _
        '    & "FROM  " _
        '    & "  public.wod_det " _
        '    & "  inner join wo_mstr on wo_mstr.wo_oid = wod_det.wod_wo_oid " _
        '    & "  where wo_mstr.wo_ord_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
        '    & "  and wo_mstr.wo_ord_date <= " + SetDate(pr_txttglakhir.DateTime.Date)

        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("wod_wo_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[wod_wo_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"

#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        cancel_data()
        MessageBox.Show("Insert Data Not Available For This Menu")
        Exit Sub
    End Sub


    Public Overrides Function before_save() As Boolean
        before_save = True
        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()
        'If ds_edit.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    before_save = False
        'End If

        Dim i As Integer
        For i = 0 To dt_edit.Rows.Count - 1
            If cek_qty_lebih(SetString(dt_edit.Rows(i).Item("wod_oid")), dt_edit.Rows(i).Item("wod_qty_req")) = False Then
                MessageBox.Show(dt_edit.Rows(i).Item("pt_code") & " Qty edited less than qty old", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        'If IsDBNull(part_desc.EditValue) Then
        '    MessageBox.Show("Data cannot null...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Return before_save
    End Function
    Private Function cek_qty_lebih(ByVal par_oid As String, ByVal par_value As Double) As Boolean
        Try
            Dim sSQL As String

            sSQL = "select wod_qty_req from wod_det where wod_oid='" & par_oid & "'"

            Dim dt As New DataTable

            dt = master_new.PGSqlConn.GetTableData(sSQL)

            For Each dr As DataRow In dt.Rows
                If dr(0) > par_value Then
                    Return False
                    Exit Function
                End If
            Next
            Return True
        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try
    End Function

    'Public Overrides Function insert() As Boolean
    '    Dim sql As String
    '    Dim _wo_oid As Guid
    '    _wo_oid = Guid.NewGuid

    '    Dim _wo_code As String

    '    Dim i As Integer

    '    _wo_code = func_coll.get_transaction_number("WO", wo_en_id.GetColumnValue("en_code"), "wo_mstr", "wo_code")



    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "INSERT INTO  " _
    '                                        & "  public.wo_mstr " _
    '                                        & "( " _
    '                                        & "  wo_oid, " _
    '                                        & "  wo_dom_id, " _
    '                                        & "  wo_en_id, " _
    '                                        & "  wo_si_id, " _
    '                                        & "  wo_id, " _
    '                                        & "  wo_code, " _
    '                                        & "  wo_pt_id, " _
    '                                        & "  wo_qty_ord, " _
    '                                        & "  wo_qty_comp, " _
    '                                        & "  wo_qty_rjc, " _
    '                                        & "  wo_ord_date, " _
    '                                        & "  wo_due_date, " _
    '                                        & "  wo_yield_pct, " _
    '                                        & "  wo_bom_id, " _
    '                                        & "  wo_ro_id, " _
    '                                        & "  wo_status, " _
    '                                        & "  wo_remarks, " _
    '                                        & "  wo_dt " _
    '                                        & ")  " _
    '                                        & "VALUES ( " _
    '                                        & SetSetring(_wo_oid.ToString) & ",  " _
    '                                        & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
    '                                        & SetIntegerDB(wo_en_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_si_id.EditValue) & ",  " _
    '                                        & SetInteger(func_coll.GetID("wo_mstr", wo_en_id.GetColumnValue("en_code"), "wo_id", "wo_en_id", wo_en_id.EditValue.ToString)) & ",  " _
    '                                        & SetSetring(_wo_code.ToString) & ",  " _
    '                                        & SetIntegerDB(wo_pt_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
    '                                        & "0,  " _
    '                                        & "0,  " _
    '                                        & SetDate(wo_ord_date.EditValue) & ",  " _
    '                                        & SetDate(wo_due_date.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_bom_id.EditValue) & ",  " _
    '                                        & SetIntegerDB(wo_ro_id.EditValue) & ",  " _
    '                                        & "'F' ,  " _
    '                                        & SetSetring(wo_remarks.Text) & ",  " _
    '                                        & "current_timestamp " _
    '                                        & ");"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    'Untuk generate WO (release)
    '                    '========================================================================
    '                    'ds_edit = New DataSet
    '                    'Try
    '                    '    Using objcb As New master_new.WDABasepgsql("", "")
    '                    '        With objcb
    '                    '            sql = "SELECT  " _
    '                    '                & "  psd_det.psd_oid, " _
    '                    '                & "  psd_det.psd_ps_oid, " _
    '                    '                & "  psd_det.psd_use_bom, " _
    '                    '                & "  psd_det.psd_desc as ptbomdesc, " _
    '                    '                & "  psd_det.psd_pt_bom_id, " _
    '                    '                & "  psd_det.psd_comp, " _
    '                    '                & "  psd_det.psd_ref, " _
    '                    '                & "  psd_det.psd_desc, " _
    '                    '                & "  psd_det.psd_start_date, " _
    '                    '                & "  psd_det.psd_end_date, " _
    '                    '                & "  psd_det.psd_qty, " _
    '                    '                & "  psd_det.psd_str_type, " _
    '                    '                & "  psd_det.psd_scrp_pct, " _
    '                    '                & "  psd_det.psd_lt_off, " _
    '                    '                & "  psd_det.psd_op, " _
    '                    '                & "  psd_det.psd_seq, " _
    '                    '                & "  psd_det.psd_fcst_pct, " _
    '                    '                & "  psd_det.psd_group, " _
    '                    '                & "  psd_det.psd_process, " _
    '                    '                & "   CASE WHEN psd_use_bom = 'Y' " _
    '                    '                & "   THEN 0 " _
    '                    '                & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
    '                    '                & "   END AS ptbomcost " _
    '                    '                & "FROM " _
    '                    '                & "  psd_det " _
    '                    '                & "  INNER JOIN ps_mstr ON (psd_det.psd_ps_oid = ps_mstr.ps_oid) "

    '                    '            If wo_bom_id.Text = "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_pt_id.EditValue
    '                    '            ElseIf wo_bom_id.Text <> "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_bom_id.EditValue
    '                    '            End If

    '                    '            .SQL = sql
    '                    '            .InitializeCommand()
    '                    '            .FillDataSet(ds_edit, "insert_edit")
    '                    '        End With
    '                    '    End Using
    '                    'Catch ex As Exception
    '                    '    MessageBox.Show(ex.Message)
    '                    'End Try

    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
    '                    '    If SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) = "Y" Then




    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & "(SELECT sum(pt_cost) as totbom " _
    '                    '                               & " from ""public"".""GetAllProductStructureRaw""(" _
    '                    '                               & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ", " _
    '                    '                               & " 1 " & ")),  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    Else

    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    End If
    '                    '=======================================================================================================================================================
    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1

    '                    '    .Command.CommandType = CommandType.Text
    '                    '    .Command.CommandText = "INSERT INTO  " _
    '                    '                        & "  public.wod_det " _
    '                    '                        & "( " _
    '                    '                        & "  wod_oid, " _
    '                    '                        & "  wod_wo_oid, " _
    '                    '                        & "  wod_use_bom, " _
    '                    '                        & "  wod_pt_bom_id, " _
    '                    '                        & "  wod_comp, " _
    '                    '                        & "  wod_op, " _
    '                    '                        & "  wod_qty_per, " _
    '                    '                        & "  wod_qty_req, " _
    '                    '                        & "  wod_cost, " _
    '                    '                        & "  wod_dt " _
    '                    '                        & ")  " _
    '                    '                        & "VALUES ( " _
    '                    '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                        & SetSetring(_wo_oid.ToString) & ",  " _
    '                    '                        & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                        & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                        & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                        & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                        & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                        & " current_timestamp " _
    '                    '                        & ");"


    '                    '    .Command.ExecuteNonQuery()
    '                    '    .Command.Parameters.Clear()


    '                    'Next

    '                    sqlTran.Commit()

    '                    after_success()
    '                    set_row(_wo_oid.ToString, "wo_oid")
    '                    'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '                    insert = True
    '                Catch ex As nPgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        row = 0
    '        insert = False
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return insert
    'End Function
    '==========================================================================
    'Public Overrides Function edit_data() As Boolean
    '    If MyBase.edit_data = True Then

    '        row = BindingContext(ds.Tables(0)).Position

    '        With ds.Tables(0).Rows(row)
    '            _wo_oid_mstr = .Item("wo_oid")
    '            wo_en_id.EditValue = .Item("wo_en_id")
    '            wo_si_id.EditValue = .Item("wo_si_id")
    '            wo_pt_id.EditValue = .Item("wo_pt_id")
    '            wo_qty_ord.EditValue = .Item("wo_qty_ord")
    '            wo_ord_date.DateTime = .Item("wo_ord_date")
    '            wo_due_date.DateTime = .Item("wo_due_date")
    '            wo_yield_pct.EditValue = .Item("wo_yield_pct")
    '            wo_bom_id.EditValue = .Item("wo_bom_id")
    '            wo_ro_id.EditValue = .Item("wo_ro_id")
    '            wo_remarks.Text = SetString(.Item("wo_remarks"))


    '        End With
    '        wo_en_id.Focus()

    '        Try
    '            tcg_header.SelectedTabPageIndex = 0
    '        Catch ex As Exception
    '        End Try

    '        'ds_edit = New DataSet
    '        ''ds_update_related = New DataSet
    '        'Try
    '        '    Using objcb As New master_new.WDABasepgsql("", "")
    '        '        With objcb
    '        '            .SQL = "SELECT  " _
    '        '                    & "  public.reqd_det.reqd_oid, " _
    '        '                    & "  public.reqd_det.reqd_dom_id, " _
    '        '                    & "  public.reqd_det.reqd_en_id, " _
    '        '                    & "  public.en_mstr.en_desc, " _
    '        '                    & "  public.reqd_det.reqd_add_by, " _
    '        '                    & "  public.reqd_det.reqd_add_date, " _
    '        '                    & "  public.reqd_det.reqd_upd_by, " _
    '        '                    & "  public.reqd_det.reqd_upd_date, " _
    '        '                    & "  public.reqd_det.reqd_req_oid, " _
    '        '                    & "  public.reqd_det.reqd_seq, " _
    '        '                    & "  public.reqd_det.reqd_related_oid, " _
    '        '                    & "  req_mstr_relation.req_code as req_code_relation, " _
    '        '                    & "  public.reqd_det.reqd_ptnr_id, " _
    '        '                    & "  public.ptnr_mstr.ptnr_name, " _
    '        '                    & "  public.reqd_det.reqd_si_id, " _
    '        '                    & "  public.si_mstr.si_desc, " _
    '        '                    & "  public.reqd_det.reqd_pt_id, " _
    '        '                    & "  public.pt_mstr.pt_code, " _
    '        '                    & "  public.pt_mstr.pt_desc1, " _
    '        '                    & "  public.pt_mstr.pt_desc2, " _
    '        '                    & "  public.reqd_det.reqd_rmks, " _
    '        '                    & "  public.reqd_det.reqd_end_user, " _
    '        '                    & "  public.reqd_det.reqd_qty, " _
    '        '                    & "  public.reqd_det.reqd_qty_processed, " _
    '        '                    & "  public.reqd_det.reqd_qty_completed, " _
    '        '                    & "  public.reqd_det.reqd_um, " _
    '        '                    & "  public.code_mstr.code_name, " _
    '        '                    & "  public.reqd_det.reqd_cost, " _
    '        '                    & "  public.reqd_det.reqd_disc, " _
    '        '                    & "  public.reqd_det.reqd_need_date, " _
    '        '                    & "  public.reqd_det.reqd_due_date, " _
    '        '                    & "  public.reqd_det.reqd_um_conv, " _
    '        '                    & "  public.reqd_det.reqd_qty_real, " _
    '        '                    & "  public.reqd_det.reqd_pt_class, " _
    '        '                    & "  public.reqd_det.reqd_status, " _
    '        '                    & "  public.reqd_det.reqd_dt, ((reqd_det.reqd_qty * reqd_det.reqd_cost) - (reqd_det.reqd_qty * reqd_det.reqd_cost * reqd_det.reqd_disc)) as reqd_qty_cost " _
    '        '                    & "FROM " _
    '        '                    & "  public.reqd_det " _
    '        '                    & "  INNER JOIN public.ptnr_mstr ON (public.reqd_det.reqd_ptnr_id = public.ptnr_mstr.ptnr_id) " _
    '        '                    & "  INNER JOIN public.si_mstr ON (public.reqd_det.reqd_si_id = public.si_mstr.si_id) " _
    '        '                    & "  INNER JOIN public.pt_mstr ON (public.reqd_det.reqd_pt_id = public.pt_mstr.pt_id) " _
    '        '                    & "  INNER JOIN public.code_mstr ON (public.reqd_det.reqd_um = public.code_mstr.code_id) " _
    '        '                    & "  INNER JOIN public.en_mstr ON (public.reqd_det.reqd_en_id = public.en_mstr.en_id) " _
    '        '                    & "  INNER JOIN public.req_mstr ON (public.reqd_det.reqd_req_oid = public.req_mstr.req_oid)" _
    '        '                    & "  left outer join public.reqd_det as reqd_det_relation ON reqd_det_relation.reqd_oid =  public.reqd_det.reqd_related_oid " _
    '        '                    & "  left outer join req_mstr as req_mstr_relation on req_mstr_relation.req_oid = reqd_det_relation.reqd_req_oid " _
    '        '                    & " where public.reqd_det.reqd_req_oid = '" + ds.Tables(0).Rows(row).Item("req_oid").ToString + "'"

    '        '            .InitializeCommand()
    '        '            .FillDataSet(ds_edit, "detail")
    '        '            'ds_update_related adalah dataset untuk membantu update reqd_qty_processed kembali ke posisi semula dulu...
    '        '            '.FillDataSet(ds_update_related, "update_related")
    '        '            gc_edit.DataSource = ds_edit.Tables(0)
    '        '            gv_edit.BestFitColumns()
    '        '        End With
    '        '    End Using
    '        'Catch ex As Exception
    '        '    MessageBox.Show(ex.Message)
    '        'End Try

    '        edit_data = True
    '    End If
    'End Function
    '================================================================================
    'Public Overrides Function edit()
    '    edit = True
    '    Dim sql As String
    '    Dim i As Integer

    '    Try
    '        Using objinsert As New master_new.WDABasepgsql("", "")
    '            With objinsert
    '                .Connection.Open()
    '                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                Try
    '                    .Command = .Connection.CreateCommand
    '                    .Command.Transaction = sqlTran

    '                    .Command.CommandType = CommandType.Text
    '                    .Command.CommandText = "UPDATE  " _
    '                                        & "  public.wo_mstr   " _
    '                                        & "SET  " _
    '                                        & "  wo_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
    '                                        & "  wo_en_id = " & wo_en_id.EditValue & ",  " _
    '                                        & "  wo_si_id = " & wo_si_id.EditValue & ",  " _
    '                                        & "  wo_pt_id = " & wo_pt_id.EditValue & ",  " _
    '                                        & "  wo_qty_ord = " & SetIntegerDB(wo_qty_ord.EditValue) & ",  " _
    '                                        & "  wo_ord_date = " & SetDate(wo_due_date.DateTime) & ",  " _
    '                                        & "  wo_due_date = " & SetDate(wo_due_date.DateTime) & ",  " _
    '                                        & "  wo_ord_date = current_timestamp ,  " _
    '                                        & "  wo_yield_pct = " & SetIntegerDB(wo_yield_pct.EditValue) & ",  " _
    '                                        & "  wo_bom_id = " & SetSetringDB(wo_bom_id.EditValue) & ",  " _
    '                                        & "  wo_ro_id = " & SetIntegerDB(wo_ro_id.EditValue) & ",  " _
    '                                        & "  wo_remarks = " & SetSetringDB(wo_remarks.Text) & ",  " _
    '                                        & "  wo_dt = current_timestamp  " _
    '                                        & "  " _
    '                                        & "WHERE  " _
    '                                        & "  wo_oid = " & SetSetring(_wo_oid_mstr.ToString) & "  " _
    '                                        & ";"

    '                    .Command.ExecuteNonQuery()
    '                    .Command.Parameters.Clear()

    '                    'untuk generate WO (release)
    '                    '.Command.CommandText = "delete from wod_det where wod_wo_oid = '" + _wo_oid_mstr + "'"
    '                    '.Command.ExecuteNonQuery()
    '                    '.Command.Parameters.Clear()
    '                    ''******************************************************

    '                    ''Insert data detail
    '                    'ds_edit = New DataSet
    '                    'Try
    '                    '    Using objcb As New master_new.WDABasepgsql("", "")
    '                    '        With objcb
    '                    '            sql = "SELECT  " _
    '                    '                & "  psd_det.psd_oid, " _
    '                    '                & "  psd_det.psd_ps_oid, " _
    '                    '                & "  psd_det.psd_use_bom, " _
    '                    '                & "  psd_det.psd_desc as ptbomdesc, " _
    '                    '                & "  psd_det.psd_pt_bom_id, " _
    '                    '                & "  psd_det.psd_comp, " _
    '                    '                & "  psd_det.psd_ref, " _
    '                    '                & "  psd_det.psd_desc, " _
    '                    '                & "  psd_det.psd_start_date, " _
    '                    '                & "  psd_det.psd_end_date, " _
    '                    '                & "  psd_det.psd_qty, " _
    '                    '                & "  psd_det.psd_str_type, " _
    '                    '                & "  psd_det.psd_scrp_pct, " _
    '                    '                & "  psd_det.psd_lt_off, " _
    '                    '                & "  psd_det.psd_op, " _
    '                    '                & "  psd_det.psd_seq, " _
    '                    '                & "  psd_det.psd_fcst_pct, " _
    '                    '                & "  psd_det.psd_group, " _
    '                    '                & "  psd_det.psd_process, " _
    '                    '                & "   CASE WHEN psd_use_bom = 'Y' " _
    '                    '                & "   THEN 0 " _
    '                    '                & "   ELSE (SELECT pt_mstr.pt_cost from pt_mstr where pt_id=psd_pt_bom_id) " _
    '                    '                & "   END AS ptbomcost " _
    '                    '                & "FROM " _
    '                    '                & "  psd_det " _
    '                    '                & "  INNER JOIN ps_mstr ON (psd_det.psd_ps_oid = ps_mstr.ps_oid) "

    '                    '            If wo_bom_id.Text = "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_pt_id.EditValue
    '                    '            ElseIf wo_bom_id.Text <> "-" Then
    '                    '                sql = sql & " where ps_mstr.ps_pt_bom_id = " & wo_bom_id.EditValue
    '                    '            End If

    '                    '            .SQL = sql
    '                    '            .InitializeCommand()
    '                    '            .FillDataSet(ds_edit, "insert_edit")
    '                    '        End With
    '                    '    End Using
    '                    'Catch ex As Exception
    '                    '    MessageBox.Show(ex.Message)
    '                    'End Try

    '                    'For i = 0 To ds_edit.Tables("insert_edit").Rows.Count - 1
    '                    '    If SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) = "Y" Then
    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid_mstr.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & "(SELECT sum(pt_cost) as totbom " _
    '                    '                               & " from ""public"".""GetAllProductStructureRaw""(" _
    '                    '                               & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ", " _
    '                    '                               & " 1 " & ")),  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    Else

    '                    '        .Command.CommandType = CommandType.Text
    '                    '        .Command.CommandText = "INSERT INTO  " _
    '                    '                            & "  public.wod_det " _
    '                    '                            & "( " _
    '                    '                            & "  wod_oid, " _
    '                    '                            & "  wod_wo_oid, " _
    '                    '                            & "  wod_use_bom, " _
    '                    '                            & "  wod_pt_bom_id, " _
    '                    '                            & "  wod_comp, " _
    '                    '                            & "  wod_op, " _
    '                    '                            & "  wod_qty_per, " _
    '                    '                            & "  wod_qty_req, " _
    '                    '                            & "  wod_cost, " _
    '                    '                            & "  wod_dt " _
    '                    '                            & ")  " _
    '                    '                            & "VALUES ( " _
    '                    '                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
    '                    '                            & SetSetring(_wo_oid_mstr.ToString) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_use_bom")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_pt_bom_id")) & ",  " _
    '                    '                            & SetSetringDB(ds_edit.Tables("insert_edit").Rows(i).Item("psd_comp")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_op")) & ",  " _
    '                    '                            & SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) & ",  " _
    '                    '                            & (SetInteger(ds_edit.Tables("insert_edit").Rows(i).Item("psd_qty")) * SetInteger(wo_qty_ord.EditValue)) & ",  " _
    '                    '                            & SetIntegerDB(ds_edit.Tables("insert_edit").Rows(i).Item("ptbomcost")) & ",  " _
    '                    '                            & " current_timestamp " _
    '                    '                            & ");"

    '                    '        .Command.ExecuteNonQuery()
    '                    '        .Command.Parameters.Clear()
    '                    '    End If

    '                    'Next

    '                    sqlTran.Commit()
    '                    'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    '                    after_success()
    '                    set_row(_wo_oid_mstr, "wo_oid")
    '                    edit = True
    '                Catch ex As nPgSqlException
    '                    sqlTran.Rollback()
    '                    MessageBox.Show(ex.Message)
    '                End Try
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        edit = False
    '        MessageBox.Show(ex.Message)
    '    End Try
    '    Return edit
    'End Function
    '==============================================================================
    'Public Overrides Function before_delete() As Boolean
    '    before_delete = True
    '    'Dim ds_bantu As New DataSet
    '    'Dim i As Integer
    '    'Try
    '    '    Using objcb As New master_new.WDABasepgsql("", "")
    '    '        With objcb
    '    '            .SQL = "select coalesce(reqd_qty_processed,0) as reqd_qty_processed from reqd_det " + _
    '    '                   " where reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_oid").ToString + "'"
    '    '            .InitializeCommand()
    '    '            .FillDataSet(ds_bantu, "reqd_det")
    '    '        End With
    '    '    End Using
    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    '    'For i = 0 To ds_bantu.Tables(0).Rows.Count - 1
    '    '    If ds_bantu.Tables(0).Rows(i).Item("reqd_qty_processed") > 0 Then
    '    '        MessageBox.Show("Can't Delete Processed WorkOrder...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    '        Return False
    '    '    End If
    '    'Next
    'End Function
    '=====================================================================
    'Public Overrides Function delete_data() As Boolean
    '    delete_data = True
    '    If ds.Tables.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    ElseIf ds.Tables(0).Rows.Count = 0 Then
    '        delete_data = False
    '        Exit Function
    '    End If

    '    If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
    '        Exit Function
    '    End If

    '    Dim i As Integer

    '    If before_delete() = True Then
    '        row = BindingContext(ds.Tables(0)).Position

    '        If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
    '            row = row - 1
    '        ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
    '            row = 0
    '        End If

    '        Try
    '            Using objinsert As New master_new.WDABasepgsql("", "")
    '                With objinsert
    '                    .Connection.Open()
    '                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
    '                    Try
    '                        .Command = .Connection.CreateCommand
    '                        .Command.Transaction = sqlTran

    '                        .Command.CommandType = CommandType.Text
    '                        .Command.CommandText = "delete from wo_mstr where wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") + "'"
    '                        .Command.ExecuteNonQuery()
    '                        .Command.Parameters.Clear()

    '                        '.Command.CommandType = CommandType.Text
    '                        '.Command.CommandText = "delete from wod_mstr where wod_wo_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wo_oid") + "'"
    '                        '.Command.ExecuteNonQuery()
    '                        '.Command.Parameters.Clear()

    '                        sqlTran.Commit()

    '                        help_load_data(True)
    '                        MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Catch ex As nPgSqlException
    '                        sqlTran.Rollback()
    '                        MessageBox.Show(ex.Message)
    '                    End Try
    '                End With
    '            End Using
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End If

    '    Return delete_data
    'End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function
#End Region
    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _wo_oid_mstr = .Item("wo_oid").ToString
                wo_en_id.Text = .Item("en_desc")
                wo_si_id.Text = .Item("si_desc")
                wo_qty_ord.EditValue = .Item("wo_qty_ord")
                wo_due_date.EditValue = Format(CDate(.Item("wo_due_date")), "d")
                wo_remarks.Text = SetString(.Item("wo_remarks"))
                wo_ro_id.Text = SetString(.Item("ro_desc"))
                wo_yield_pct.EditValue = SetNumber(.Item("wo_yield_pct"))
                pt_code.Text = .Item("pt_code")
                part_desc.Text = .Item("pt_desc1")
                wo_ord_date.EditValue = Format(CDate(.Item("wo_ord_date")), "d") ' .Item("wo_ord_date").ToString("d")
            End With

            Dim sSQL As String

            sSQL = "SELECT  " _
                & "  a.wod_oid, " _
                & "  a.wod_wo_oid, " _
                & "  a.wod_pt_bom_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  a.wod_op, " _
                & "  a.wod_qty_per, " _
                & "  a.wod_qty_req, " _
                & "  a.wod_qty_alloc, " _
                & "  a.wod_qty_picked, " _
                & "  a.wod_qty_issued, " _
                & "  a.wod_cost " _
                & "FROM " _
                & "  public.wod_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.wod_pt_bom_id = b.pt_id) " _
                & " WHERE wod_wo_oid='" & ds.Tables(0).Rows(row).Item("wo_oid").ToString & "' " _
                & "ORDER BY " _
                & "  b.pt_code"


            dt_edit = master_new.PGSqlConn.GetTableData(sSQL)
            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()

            edit_data = True
        End If
    End Function
    Private Sub gv_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.Click
        gv_edit_SelectionChanged(Nothing, Nothing)
    End Sub
    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
      

        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _row = -2147483647 Then
            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit.AcceptChanges()
            ' _row = dt_edit.Rows.Count - 1
            _row = gv_edit.FocusedRowHandle
        End If

        If gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False Then

            Box("Only add operation permited")
            Exit Sub
        End If

        'Exit Sub
        If _col = "pt_code" Or _col = "pt_desc1" Then
            '            Dim frm As New FProdStrucSearch
            Dim frm As New FPartNumberSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = ds.Tables(0).Rows(row).Item("wo_en_id")
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        gv_edit_SelectionChanged(Nothing, Nothing)
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
    End Sub

    Private Sub gv_edit_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyUp
        If e.KeyCode = Keys.Enter Then
            browse_data()
        End If
    End Sub

    'Dim _qty_before As Double
    'Dim _status_edit As Boolean
    'Dim _line As Integer
    Private Sub gv_edit_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_edit.SelectionChanged
        Try
            'Dim _row As Integer = gv_edit.FocusedRowHandle

            'If _row = -2147483647 Then
            '    gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            '    dt_edit.AcceptChanges()
            '    _row = dt_edit.Rows.Count - 1
            'End If

            'If Not dt_edit.Rows(_row).Item("wod_oid") Is System.DBNull.Value Then
            '    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
            '    '_status_edit = True
            '    '_line = _row
            '    '_qty_before = gv_edit.GetFocusedRowCellValue("wod_qty_req")
            'Else
            '    gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
            '    '_status_edit = False
            'End If

            Dim _nilai As Object
            _nilai = gv_edit.GetFocusedRowCellValue("wod_oid").ToString
            If _nilai Is System.DBNull.Value Then
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = True
            Else
                gc_edit.EmbeddedNavigator.Buttons.Remove.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Overrides Function edit() As Object
        edit = True
        Dim i As Integer
        Try
            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit.AcceptChanges()

            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        For i = 0 To dt_edit.Rows.Count - 1
                            If SetString(dt_edit.Rows(i).Item("wod_oid")) = "" Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wod_det " _
                                                & "( " _
                                                & "  wod_oid, " _
                                                & "  wod_wo_oid, " _
                                                & "  wod_use_bom, " _
                                                & "  wod_pt_bom_id, " _
                                                & "  wod_comp, " _
                                                & "  wod_op, " _
                                                & "  wod_qty_per, " _
                                                & "  wod_qty_req, " _
                                                & "  wod_cost, " _
                                                & "  wod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_wo_oid_mstr) & ",  " _
                                                & SetSetringDB("N") & ",  " _
                                                & SetInteger(dt_edit.Rows(i).Item("wod_pt_bom_id")) & ",  " _
                                                & SetSetringDB(dt_edit.Rows(i).Item("pt_code")) & ",  " _
                                                & SetInteger(dt_edit.Rows(i).Item("wod_op")) & ",  " _
                                                & SetDecDB(dt_edit.Rows(i).Item("wod_qty_per")) & ",  " _
                                                & SetDecDB(dt_edit.Rows(i).Item("wod_qty_req")) & ",  " _
                                                & "0," _
                                                & " current_timestamp " _
                                                & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Else
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE   " _
                                                & "  public.wod_det " _
                                                & " set wod_qty_req =" & SetDecDB(dt_edit.Rows(i).Item("wod_qty_req")) _
                                                & " Where wod_oid='" & dt_edit.Rows(i).Item("wod_oid").ToString & "'"


                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If

                        Next

                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_wo_oid_mstr.ToString), "wo_oid")
                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As nPgSqlException
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
    Public Overrides Function insert() As Boolean
        insert = True
        Dim i As Integer

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "UPDATE  " _
                        '                    & "  public.wo_mstr   " _
                        '                    & "SET  " _
                        '                    & "  wo_status = 'R' ," _
                        '                    & "  wo_rel_date = current_timestamp " _
                        '                    & " WHERE  " _
                        '                    & "  wo_code = '" & wo_code.EditValue.ToString & "'  " _
                        '                    & ";"

                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To dt_edit.Rows.Count - 1
                            If dt_edit.Rows(i).Item("wod_oid") Is System.DBNull.Value Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                & "  public.wod_det " _
                                                & "( " _
                                                & "  wod_oid, " _
                                                & "  wod_wo_oid, " _
                                                & "  wod_use_bom, " _
                                                & "  wod_pt_bom_id, " _
                                                & "  wod_comp, " _
                                                & "  wod_op, " _
                                                & "  wod_qty_per, " _
                                                & "  wod_qty_req, " _
                                                & "  wod_cost, " _
                                                & "  wod_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(ds.Tables(0).Rows(row).Item("wo_oid")) & ",  " _
                                                & SetSetringDB("N") & ",  " _
                                                & SetInteger(dt_edit.Rows(i).Item("wod_pt_bom_id")) & ",  " _
                                                & SetSetringDB(dt_edit.Rows(i).Item("pt_code")) & ",  " _
                                                & SetInteger(dt_edit.Rows(i).Item("wod_op")) & ",  " _
                                                & SetDec(dt_edit.Rows(i).Item("wod_qty_per")) & ",  " _
                                                & SetDec(dt_edit.Rows(i).Item("wod_qty_req")) & ",  " _
                                                & "0," _
                                                & " current_timestamp " _
                                                & ")"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                           
                        Next

                        sqlTran.Commit()
                        after_success()
                        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using

        Catch ex As Exception
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

End Class