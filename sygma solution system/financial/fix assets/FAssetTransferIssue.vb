Imports npgsql
Imports master_new.ModFunction

Public Class FAssetTransferIssue
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit As DataSet
    Dim _now As DateTime
    Public _req_oid As String
    Dim ds_edit_detail As New DataSet

    Private Sub FAssetTransferIssue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        reqs_en_id.Properties.DataSource = dt_bantu
        reqs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        reqs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        reqs_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Requisition Number", "req_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Assignment Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Assignment Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "reqs_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "req_code", False)
        add_column(gv_detail, "reqds_reqs_oid", False)
        add_column(gv_detail, "reqds_reqd_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "reqds_ass_id", False)
        add_column(gv_detail, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location Code", "loc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Department", "department", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_asset, "reqds_oid", False)
        add_column(gv_detail_asset, "reqds_reqs_oid", False)
        add_column(gv_detail_asset, "reqds_reqd_oid", False)
        add_column(gv_detail_asset, "reqds_ass_id", False)
        add_column(gv_detail_asset, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail_asset, "Qty", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_edit, "reqd_oid", False)
        add_column(gv_edit, "reqd_req_oid", False)
        add_column(gv_edit, "reqd_pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Open", "reqd_qty_open", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Assignment", "reqd_qty_assign", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "reqd_um", False)
        add_column(gv_edit, "UM", "um", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_loc_id", False)
        add_column(gv_edit, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "reqd_emp_id", False)
        add_column(gv_edit, "End User", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "xemp_dept", False)
        add_column(gv_edit, "Department", "department", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "xemp_rg", False)
        add_column(gv_edit, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_detail, "reqds_oid", False)
        add_column(gv_edit_detail, "reqds_reqd_oid", False)
        add_column(gv_edit_detail, "reqds_ass_id", False)
        add_column(gv_edit_detail, "Serial Number", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit_detail, "pt_id", False)
        add_column(gv_edit_detail, "Qty", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit_detail, "reqds_um", False)
        add_column(gv_edit_detail, "reqds_loc_id", False)
        add_column(gv_edit_detail, "reqds_emp_id", False)
        add_column(gv_edit_detail, "reqds_dept_id", False)
        add_column(gv_edit_detail, "reqds_rg", False)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  reqs_oid, " _
                    & "  reqs_dom_id, " _
                    & "  reqs_add_by, " _
                    & "  reqs_add_date, " _
                    & "  reqs_upd_by, " _
                    & "  reqs_upd_date, " _
                    & "  reqs_req_oid, " _
                    & "  req_code, " _
                    & "  reqs_code, " _
                    & "  reqs_date, " _
                    & "  reqs_receive_date, " _
                    & "  reqs_en_id, " _
                    & "  en_mstr_from.en_desc as en_desc_from, " _
                    & "  reqs_si_id, " _
                    & "  si_mstr_from.si_desc as si_desc_from, " _
                    & "  reqs_remarks, " _
                    & "  reqs_trans_id, " _
                    & "  reqs_dt   " _
                    & "FROM  " _
                    & "  public.reqs_mstr " _
                    & "  LEFT OUTER join req_mstr on req_oid = reqs_req_oid " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = reqs_si_id " _
                    & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and reqs_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

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

        sql = "SELECT " _
            & "distinct(reqds_reqd_oid), " _
            & "reqds_reqs_oid, " _
            & "pt_id,pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "pt_cost,  " _
            & "reqds_ass_id,  " _
            & "sum(reqds_qty) as reqds_qty,  " _
            & "um.code_name as reqds_um_name, " _
            & "xemp_name, " _
            & "dept.code_name as department, " _
            & "rg.code_name as regional, " _
            & "reqds_loc_id, " _
            & "loc_code, " _
            & "loc_desc, " _
            & "req_code " _
            & "FROM   " _
            & "public.reqsd_det  " _
            & "LEFT OUTER join reqs_mstr on reqs_oid = reqds_reqs_oid  " _
            & "LEFT OUTER join req_mstr on req_oid = reqs_req_oid " _
            & "LEFT OUTER join reqd_det on reqd_oid = reqds_reqd_oid  " _
            & "inner join code_mstr um on um.code_id = reqds_um  " _
            & "inner join xemp_mstr on xemp_id = reqds_emp_id " _
            & "inner join ass_mstr on ass_id = reqds_ass_id  " _
            & "inner join pt_mstr on pt_id = ass_pt_id  " _
            & "inner join code_mstr dept on dept.code_id = reqds_dept_id  " _
            & "inner join code_mstr rg on rg.code_id = reqds_rg " _
            & "LEFT OUTER join loc_mstr on loc_id = reqds_loc_id" _
            & " where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & " and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
            & "group by reqds_reqd_oid, " _
            & "pt_id,pt_code,  " _
            & "pt_desc1,  " _
            & "pt_desc2,  " _
            & "reqds_ass_id,  " _
            & "pt_cost,  " _
            & "reqds_qty,  " _
            & "reqds_um_name, " _
            & "xemp_name, " _
            & "department, " _
            & "regional,req_code, " _
            & "reqds_reqs_oid, " _
            & "reqds_loc_id, " _
            & "loc_code, " _
            & "loc_desc "

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_det").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  reqds_oid, " _
            & "  reqds_add_by, " _
            & "  reqds_add_date, " _
            & "  reqds_upd_by, " _
            & "  reqds_upd_date, " _
            & "  reqds_reqs_oid, " _
            & "  reqds_reqd_oid, " _
            & "  reqds_seq, " _
            & "  pt_id, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  pt_cost, " _
            & "  reqds_qty, " _
            & "  reqds_um, " _
            & "  um.code_name as reqds_um_name, " _
            & "  reqds_qty_real, " _
            & "  0 as reqds_qty_open, " _
            & "  reqds_dt, " _
            & "  reqds_emp_id,xemp_name, " _
            & "  reqds_ass_id,ass_desc,ass_code, " _
            & "  reqds_dept_id,dept.code_name as department, " _
            & "  reqds_rg,rg.code_name as regional " _
            & "FROM  " _
            & "  public.reqsd_det " _
            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
            & "  LEFT OUTER join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join code_mstr um on um.code_id = reqds_um " _
            & "  inner join xemp_mstr on xemp_id = reqds_emp_id " _
            & "  inner join ass_mstr on ass_id = reqds_ass_id " _
            & "  inner join pt_mstr on pt_id = ass_pt_id  " _
            & "  inner join code_mstr dept on dept.code_id = reqds_dept_id " _
            & "  inner join code_mstr rg on rg.code_id = reqds_rg " _
            & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date)
        load_data_detail(sql, gc_detail_asset, "detail_det")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[reqds_reqs_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_detail_asset.Columns("reqds_reqs_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[reqds_reqs_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString & "'")
            gv_detail_asset.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        reqs_en_id.ItemIndex = 0
        reqs_remarks.Text = ""
        reqs_req_oid.Text = ""
        _req_oid = ""
        reqs_en_id.Focus()

        Try
            tcg_header.SelectedTabPageIndex = 0
            tcg_detail.SelectedTabPageIndex = 0
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
                            & "  reqd_oid, " _
                            & "  reqd_req_oid, " _
                            & "  reqd_seq, " _
                            & "  reqd_pt_id,pt_code,pt_desc1,pt_desc2, " _
                            & "  reqd_rmks, " _
                            & "  reqd_qty, " _
                            & "  reqd_qty_processed, " _
                            & "  reqd_qty_completed, " _
                            & "  reqd_um,um.code_name as um, " _
                            & "  reqd_cost, " _
                            & "  reqd_disc, " _
                            & "  reqd_um_conv, " _
                            & "  reqd_qty_real, " _
                            & "  reqd_qty,  " _
                            & "  reqd_qty_processed,  " _
                            & "  reqd_qty_completed,  " _
                            & "  (reqd_qty - coalesce(reqd_qty_completed,0)) as reqd_qty_open, " _
                            & "  0 as reqd_qty_assign, " _
                            & "  reqd_pt_class, " _
                            & "  reqd_status, " _
                            & "  -1 as reqd_loc_id, '' as loc_desc, " _
                            & "  reqd_emp_id,xemp_name, " _
                            & "  xemp_rg,rg.code_name as regional, " _
                            & "  xemp_dept,dept.code_name as department, " _
                            & "  xemp_div,div.code_name as divisi " _
                            & "FROM  " _
                            & "  public.reqd_det  " _
                            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = reqd_um " _
                            & "  inner join xemp_mstr on xemp_id = reqd_emp_id " _
                            & "  inner join code_mstr rg on rg.code_id = xemp_rg  " _
                            & "  inner join code_mstr dept on dept.code_id = xemp_dept  " _
                            & "  inner join code_mstr div on div.code_id = xemp_div  " _
                            & "  where reqd_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_detail = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  reqds_oid, " _
                            & "  reqds_add_by, " _
                            & "  reqds_add_date, " _
                            & "  reqds_upd_by, " _
                            & "  reqds_upd_date, " _
                            & "  reqds_reqs_oid, " _
                            & "  reqds_reqd_oid, " _
                            & "  reqds_seq, " _
                            & "  pt_id, " _
                            & "  pt_code, " _
                            & "  pt_desc1, " _
                            & "  pt_desc2, " _
                            & "  pt_cost, " _
                            & "  reqds_qty, " _
                            & "  reqds_um, " _
                            & "  um.code_name as reqds_um_name, " _
                            & "  reqds_qty_real, " _
                            & "  0 as reqds_qty_open, " _
                            & "  reqds_dt, " _
                            & "  reqds_loc_id,loc_code, loc_desc, " _
                            & "  reqds_emp_id,xemp_name, " _
                            & "  reqds_ass_id,ass_desc,ass_code, " _
                            & "  reqds_dept_id,dept.code_name as department, " _
                            & "  reqds_rg,rg.code_name as regional " _
                            & "FROM  " _
                            & "  public.reqsd_det " _
                            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
                            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                            & "  inner join code_mstr um on um.code_id = reqds_um " _
                            & "  inner join xemp_mstr on xemp_id = reqds_emp_id " _
                            & "  inner join ass_mstr on ass_id = reqds_ass_id " _
                            & "  inner join code_mstr dept on dept.code_id = reqds_dept_id " _
                            & "  inner join code_mstr rg on rg.code_id = reqds_rg " _
                            & "  left outer join loc_mstr on loc_id = reqds_loc_id " _
                            & "  where pt_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_detail, "insert_edit_det")
                    gc_edit_detail.DataSource = ds_edit_detail.Tables(0)
                    gv_edit_detail.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Sub gv_edit_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "reqd_oid", Guid.NewGuid.ToString())
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit.UpdateCurrentRow()
        gv_serial.UpdateCurrentRow()

        ds_edit.AcceptChanges()
        ds_edit_detail.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim i, j As Integer
        Dim _qty, _qty_ttl_serial As Double

        'Mencari apakah receive barang yang Serial mempunyai detail nya atau tidak
        'dan apakah jumlah detailnya sama dengan qty receive atau tidak..
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            _qty = IIf(IsDBNull(ds_edit.Tables(0).Rows(i).Item("reqd_qty_assign")) = True, 0, ds_edit.Tables(0).Rows(i).Item("reqd_qty_assign"))
            _qty_ttl_serial = 0
            For j = 0 To ds_edit_detail.Tables(0).Rows.Count - 1
                If ds_edit.Tables(0).Rows(i).Item("reqd_oid").ToString = ds_edit_detail.Tables(0).Rows(j).Item("reqds_reqd_oid").ToString Then
                    _qty_ttl_serial = _qty_ttl_serial + 1
                End If
            Next
            If _qty <> _qty_ttl_serial Then
                MessageBox.Show("Part Number : " + ds_edit.Tables(0).Rows(i).Item("pt_code") + " Have Wrong Serial Number Data.. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                BindingContext(ds_edit.Tables(0)).Position = i
                Return False
            End If
        Next
        '***********************************************************************
        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _reqs_oid As Guid = Guid.NewGuid

        Dim _reqs_code As String
        Dim _tran_id As Integer
        Dim i As Integer


        _reqs_code = func_coll.get_transaction_number("RI", reqs_en_id.GetColumnValue("en_code"), "reqs_mstr", "reqs_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-tr")
        If _tran_id = -1 Then
            MessageBox.Show("Transfer Issue In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
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
                        If reqs_req_oid.Text = "" Then
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqs_mstr " _
                                                & "( " _
                                                & "  reqs_oid, " _
                                                & "  reqs_dom_id, " _
                                                & "  reqs_en_id, " _
                                                & "  reqs_add_by, " _
                                                & "  reqs_add_date, " _
                                                & "  reqs_code, " _
                                                & "  reqs_date, " _
                                                & "  reqs_remarks, " _
                                                & "  reqs_dt, " _
                                                & "  reqs_si_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_reqs_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(reqs_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(_reqs_code) & ",  " _
                                                & " current_date " & ",  " _
                                                & SetSetring(reqs_remarks.Text) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetInteger(reqs_si_id.EditValue) & "  " _
                                                & ")"
                        Else
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.reqs_mstr " _
                                                & "( " _
                                                & "  reqs_oid, " _
                                                & "  reqs_dom_id, " _
                                                & "  reqs_en_id, " _
                                                & "  reqs_add_by, " _
                                                & "  reqs_add_date, " _
                                                & "  reqs_code, " _
                                                & "  reqs_date, " _
                                                & "  reqs_req_oid, " _
                                                & "  reqs_remarks, " _
                                                & "  reqs_dt, " _
                                                & "  reqs_si_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_reqs_oid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(reqs_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetSetring(_reqs_code) & ",  " _
                                                & " current_date " & ",  " _
                                                & SetSetringDB(_req_oid) & ",  " _
                                                & SetSetring(reqs_remarks.Text) & ",  " _
                                                & " current_timestamp " & ",  " _
                                                & SetInteger(reqs_si_id.EditValue) & "  " _
                                                & ")"
                        End If
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_detail.Tables(0).Rows.Count - 1
                            'MessageBox.Show(ds_edit_detail.Tables(0).Rows(i).Item("reqds_dept_id"))
                            If ds_edit_detail.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                .Command.CommandType = CommandType.Text
                                If reqs_req_oid.Text = "" Then
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.reqsd_det " _
                                                        & "( " _
                                                        & "  reqds_oid, " _
                                                        & "  reqds_add_by, " _
                                                        & "  reqds_add_date, " _
                                                        & "  reqds_reqs_oid, " _
                                                        & "  reqds_seq, " _
                                                        & "  reqds_qty, " _
                                                        & "  reqds_um, " _
                                                        & "  reqds_qty_real, " _
                                                        & "  reqds_dt, " _
                                                        & "  reqds_loc_id, " _
                                                        & "  reqds_emp_id, " _
                                                        & "  reqds_ass_id, " _
                                                        & "  reqds_dept_id, " _
                                                        & "  reqds_rg " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(ds_edit_detail.Tables(0).Rows(i).Item("reqds_oid")) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & " current_timestamp " & ",  " _
                                                        & SetSetring(_reqs_oid.ToString) & ",  " _
                                                        & SetInteger(i) & ",  " _
                                                        & SetDbl(ds_edit_detail.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_um")) & ",  " _
                                                        & SetDbl(ds_edit_detail.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                        & " current_timestamp " & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_loc_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_emp_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_ass_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_dept_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_rg")) & "  " _
                                                        & ")"
                                Else
                                    .Command.CommandText = "INSERT INTO  " _
                                                        & "  public.reqsd_det " _
                                                        & "( " _
                                                        & "  reqds_oid, " _
                                                        & "  reqds_add_by, " _
                                                        & "  reqds_add_date, " _
                                                        & "  reqds_reqs_oid, " _
                                                        & "  reqds_reqd_oid, " _
                                                        & "  reqds_seq, " _
                                                        & "  reqds_qty, " _
                                                        & "  reqds_um, " _
                                                        & "  reqds_qty_real, " _
                                                        & "  reqds_dt, " _
                                                        & "  reqds_loc_id, " _
                                                        & "  reqds_emp_id, " _
                                                        & "  reqds_ass_id, " _
                                                        & "  reqds_dept_id, " _
                                                        & "  reqds_rg " _
                                                        & ")  " _
                                                        & "VALUES ( " _
                                                        & SetSetring(ds_edit_detail.Tables(0).Rows(i).Item("reqds_oid")) & ",  " _
                                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                        & " current_timestamp " & ",  " _
                                                        & SetSetring(_reqs_oid.ToString) & ",  " _
                                                        & SetSetringDB(ds_edit_detail.Tables(0).Rows(i).Item("reqds_reqd_oid")) & ",  " _
                                                        & SetInteger(i) & ",  " _
                                                        & SetDbl(ds_edit_detail.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_um")) & ",  " _
                                                        & SetDbl(ds_edit_detail.Tables(0).Rows(i).Item("reqds_qty")) & ",  " _
                                                        & " current_timestamp " & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_loc_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_emp_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_ass_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_dept_id")) & ",  " _
                                                        & SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_rg")) & "  " _
                                                        & ")"
                                End If
                                'ini karena didatabase tidak ada um conversion..jadi disamin aja 
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = " update ass_mstr " + _
                                                       " set ass_qty_assgn = 1, " + _
                                                       " ass_loc_id = " + SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_loc_id")) + ", " + _
                                                       " ass_emp_id = " + SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_emp_id")) + ", " + _
                                                       " ass_emp_dept = " + SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_dept_id")) + ", " + _
                                                       " ass_emp_rg = " + SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_rg")) + " " + _
                                                       " where ass_id = " + SetInteger(ds_edit_detail.Tables(0).Rows(i).Item("reqds_ass_id"))
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            End If
                        Next

                        If reqs_req_oid.Text = "" Then

                        Else
                            For Each _dr As DataRow In ds_edit.Tables(0).Rows
                                'Update qty reqd_qty_completed di reqd_det
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = " update reqd_det set reqd_qty_completed = " + SetDbl(_dr("reqd_qty_assign")) + _
                                                       " where reqd_oid = '" + _dr("reqd_oid").ToString + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If
                        
                        sqlTran.Commit()
                        after_success()
                        set_row(_reqs_oid.ToString, "reqs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As nPgSqlException
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim _reqs_trans_id As String = ""
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(reqs_trans_id,'') as reqs_trans_id from reqs_mstr where reqs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _reqs_trans_id = .DataReader("reqs_trans_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _reqs_trans_id.ToUpper = "C" Then
            MessageBox.Show("Can't Delete Closed Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
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

        If before_delete() = True Then
            Dim _reqs_code, _reqs_oid As String
            Dim _row As Integer
            Dim i As Integer

            row = BindingContext(ds.Tables(0)).Position
            _row = row

            _reqs_oid = ds.Tables(0).Rows(_row).Item("reqs_oid").ToString
            _reqs_code = ds.Tables(0).Rows(_row).Item("reqs_code")

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

                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("reqds_reqs_oid").ToString = _reqs_oid Then
                                    If IsDBNull(ds.Tables(0).Rows(_row).Item("req_code")) Then
                                    Else
                                        .Command.CommandType = CommandType.Text
                                        .Command.CommandText = "update reqd_det set reqd_qty_completed = reqd_qty_completed - " + ds.Tables("detail").Rows(i).Item("reqds_qty").ToString + _
                                                               " where reqd_oid = '" + ds.Tables("detail").Rows(i).Item("reqds_reqd_oid").ToString + "'"
                                        .Command.ExecuteNonQuery()
                                        .Command.Parameters.Clear()
                                    End If
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update ass_mstr set ass_qty_assgn = 0, " + _
                                                           " ass_emp_id = 0, " + _
                                                           " ass_emp_dept = 0, " + _
                                                           " ass_emp_rg = 0 " + _
                                                           " where ass_id = " + SetString(ds.Tables("detail").Rows(i).Item("reqds_ass_id"))
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If
                            Next
                            '*****************************************************************************************

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from reqs_mstr where reqs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            'If func_coll.delete_glt_det_ap(objinsert, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_oid"), _
                            '                               ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ap_code")) = False Then
                            '    sqlTran.Rollback()
                            '    Exit Function
                            'End If

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

    Private Sub gv_edit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub gv_edit_detail_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit_detail.DoubleClick
        browse_data_det()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _pod_en_id As Integer = reqs_en_id.EditValue

        If _col = "ass_code" Then
            Dim frm As New FAssetSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "um" Then
            Dim frm As New FUMSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "loc_desc" Then
            Dim frm As New FLocationSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "xemp_name" Then
            Dim frm As New FEmpSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "department" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._type = "emp_dept"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "regional" Then
            Dim frm As New FDeptSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._type = "emp_region"
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "pt_code" Then
            If reqs_req_oid.Text = "" Then
                Dim frm As New FPartNumberSearch
                frm.set_win(Me)
                frm._row = _row
                frm._en_id = _pod_en_id
                frm.type_form = True
                frm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub browse_data_det()
        Dim _col As String = gv_edit_detail.FocusedColumn.Name
        Dim _row As Integer = gv_edit_detail.FocusedRowHandle
        Dim _pod_en_id As Integer = reqs_en_id.EditValue

        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position

        Dim _pt_id As Integer
        _pt_id = ds_edit.Tables(0).Rows(_row_edit).Item("reqd_pt_id")

        If _col = "ass_code" Then
            Dim frm As New FAssetSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _pod_en_id
            frm._pt_id = _pt_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_detail_InitNewRow(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_detail.InitNewRow
        Dim _row_edit As Integer
        _row_edit = BindingContext(ds_edit.Tables(0)).Position
        ds_edit.AcceptChanges()
        With gv_edit_detail
            If ds_edit.Tables(0).Rows(_row_edit).Item("reqd_loc_id") = -1 Then
                MessageBox.Show("Please Define Location First..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit_detail.CancelUpdateCurrentRow()
                Exit Sub
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("reqd_emp_id")) = True Then
                MessageBox.Show("Please Define Employee First..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit_detail.CancelUpdateCurrentRow()
                Exit Sub
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("xemp_dept")) = True Then
                MessageBox.Show("Please Define Departement First..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit_detail.CancelUpdateCurrentRow()
                Exit Sub
            ElseIf IsDBNull(ds_edit.Tables(0).Rows(_row_edit).Item("xemp_rg")) = True Then
                MessageBox.Show("Please Define Region First..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                gv_edit_detail.CancelUpdateCurrentRow()
                Exit Sub
            End If

            .SetRowCellValue(e.RowHandle, "reqds_oid", Guid.NewGuid.ToString())
            .SetRowCellValue(e.RowHandle, "reqds_reqd_oid", ds_edit.Tables(0).Rows(_row_edit).Item("reqd_oid").ToString)
            .SetRowCellValue(e.RowHandle, "reqds_qty", 1)
            .SetRowCellValue(e.RowHandle, "reqds_um", ds_edit.Tables(0).Rows(_row_edit).Item("reqd_um"))
            .SetRowCellValue(e.RowHandle, "reqds_qty_real", 1)
            .SetRowCellValue(e.RowHandle, "reqds_loc_id", ds_edit.Tables(0).Rows(_row_edit).Item("reqd_loc_id"))
            .SetRowCellValue(e.RowHandle, "reqds_emp_id", ds_edit.Tables(0).Rows(_row_edit).Item("reqd_emp_id"))
            .SetRowCellValue(e.RowHandle, "reqds_dept_id", ds_edit.Tables(0).Rows(_row_edit).Item("xemp_dept"))
            .SetRowCellValue(e.RowHandle, "reqds_rg", ds_edit.Tables(0).Rows(_row_edit).Item("xemp_rg"))
            .BestFitColumns()
        End With
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Try
            gv_edit_detail.Columns("reqds_reqd_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[reqds_reqd_oid] = '" & ds_edit.Tables(0).Rows(BindingContext(ds_edit.Tables(0)).Position).Item("reqd_oid").ToString & "'")
            gv_edit_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_detail_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_detail.FocusedRowChanged
        Try
            gv_detail_asset.Columns("reqds_reqd_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[reqds_reqd_oid] = '" & ds.Tables("detail").Rows(BindingContext(ds.Tables("detail")).Position).Item("reqds_reqd_oid").ToString & "'")
            gv_detail_asset.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub reqs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reqs_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(reqs_en_id.EditValue))
        reqs_si_id.Properties.DataSource = dt_bantu
        reqs_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        reqs_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        reqs_si_id.ItemIndex = 0
    End Sub

    Private Sub reqs_req_oid_ButtonClick1(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles reqs_req_oid.ButtonClick
        Dim frm As New FRequisitionSearch()
        frm.set_win(Me)
        frm._en_id = reqs_en_id.EditValue
        frm.type_form = True
        frm._obj = reqs_req_oid
        frm.ShowDialog()
    End Sub
  
    Private Sub tcg_header_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles tcg_header.SelectedPageChanged
        If tcg_header.SelectedTabPage.Name.ToString = "LayoutControlGroup6" Then
            If Trim(reqs_req_oid.Text) = "" Then
                gc_edit.EmbeddedNavigator.Buttons.Append.Visible = True
                gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
            Else
                gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
                gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
            End If

        End If
    End Sub
End Class
