Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport
Imports master_new.PGSqlConn
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid

Public Class FInventoryRequestControl
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As New DataSet
    Dim ds_detail As New DataSet
    Dim mf As New master_new.ModFunction
    Dim _now As DateTime
    Public _pt_id As String
    Public _si_id As String
    Public _loc_id As String
    Public _pt_ac_id As String
    Public _ptd_ac_code As String
    Public _ps_oid As String
    Public _asmb_cost As Double
    Dim sSQL As String
    Dim _pt_code As String = ""
    Dim warna As Boolean = False
    Dim _load As Boolean = False
    Dim dt As New DataTable

    Private Sub FProductStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        irc_start_date.DateTime = _now
        irc_end_date.DateTime = _now
        irc_date.DateTime = _now
        sSQL = "select loc_id from loc_mstr where loc_desc='" & func_coll.get_conf_file("irc_stock_location") & "'"

        irc_loc_id.Tag = GetRowInfo(sSQL)(0)
        irc_loc_id.Text = func_coll.get_conf_file("irc_stock_location")

        sSQL = "select loc_id from loc_mstr where loc_desc='" & func_coll.get_conf_file("irc_lock_location") & "'"

        irc_loc_id_lock.Tag = GetRowInfo(sSQL)(0)
        irc_loc_id_lock.Text = func_coll.get_conf_file("irc_lock_location")

        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        TabbedControlGroup1.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())

        irc_en_id.Properties.DataSource = dt_bantu
        irc_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        irc_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        irc_en_id.ItemIndex = 0
    End Sub

    Private Sub asmb_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "irc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "irc_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Stock Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Save Location", "loc_desc_lock", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date Req", "irc_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date Req", "irc_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "irc_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "irc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "irc_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "irc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "irc_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "ircd_irc_oid", False)
        add_column(gv_detail, "ircd_flag", False)
        add_column_copy(gv_detail, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Partnumber Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Stock", "ircd_qty_stock", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Request", "ircd_qty_request", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Approve", "ircd_qty_approve", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Outstanding", "ircd_qty_outstanding", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Qty Transfer", "ircd_qty_use", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        'pb_code
        add_column_copy(gv_detail, "IR Number", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Requested", "ircd_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "End User", "ircd_end_user", DevExpress.Utils.HorzAlignment.Default)


        add_column(gv_edit, "pbd_pt_id", False)
        add_column(gv_edit, "warna", False)
        add_column_copy(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "PT Desc 1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Unit Measure", "um_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Qty Stock", "invc_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Qty Request", "pbd_qty", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Qty Approve", "pbd_qty_riud", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "Qty Stock Remaining", "qty_stock_remaining", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_edit, "IR Code", "pb_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Request Date", "pb_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_edit, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Requested", "pb_requested", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "End User", "pb_end_user", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.irc_oid, " _
                    & "  a.irc_code, " _
                    & "  a.irc_date, " _
                    & "  a.irc_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.irc_loc_id, " _
                    & "  c.loc_desc, " _
                    & "  a.irc_start_date, " _
                    & "  a.irc_end_date, " _
                    & "  a.irc_remarks, " _
                    & "  a.irc_add_by, " _
                    & "  a.irc_add_date, " _
                    & "  a.irc_upd_by, " _
                    & "  a.irc_upd_date, " _
                    & "  a.irc_loc_id_lock, " _
                    & "  d.loc_desc as loc_desc_lock " _
                    & "FROM " _
                    & "  public.irc_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.irc_en_id = b.en_id) " _
                    & "  INNER JOIN public.loc_mstr c ON (a.irc_loc_id = c.loc_id) " _
                    & "  INNER JOIN public.loc_mstr d ON (a.irc_loc_id_lock = d.loc_id) " _
                    & "WHERE " _
                    & "  a.irc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " " _
                    & " and  irc_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " & master_new.ClsVar.sUserID.ToString & ") " _
                    & " ORDER BY " _
                    & "  a.irc_code"



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
            & "  a.ircd_oid, " _
            & "  a.ircd_irc_oid, " _
            & "  a.ircd_pb_oid, " _
            & "  c.pb_code, " _
            & "  a.ircd_en_id, " _
            & "  d.en_desc, " _
            & "  a.ircd_requested, " _
            & "  a.ircd_end_user, " _
            & "  a.ircd_qty_stock, " _
            & "  a.ircd_qty_request, " _
            & "  a.ircd_qty_approve, " _
            & "  a.ircd_qty_outstanding, " _
            & "  a.ircd_qty_use, " _
            & "  a.ircd_flag, " _
            & "  a.ircd_pt_id, " _
            & "  b.pt_code, " _
            & "  b.pt_desc1, " _
            & "  b.pt_desc2, " _
            & "  e.code_name AS um_desc " _
            & "FROM " _
            & "  public.ircd_det a " _
            & "  INNER JOIN public.pt_mstr b ON (a.ircd_pt_id = b.pt_id) " _
            & "  INNER JOIN public.pb_mstr c ON (a.ircd_pb_oid = c.pb_oid) " _
            & "  INNER JOIN public.en_mstr d ON (a.ircd_en_id = d.en_id) " _
            & "  INNER JOIN public.code_mstr e ON (b.pt_um = e.code_id) " _
            & "  INNER JOIN public.irc_mstr f ON (a.ircd_irc_oid = f.irc_oid) " _
            & "WHERE " _
            & "  f.irc_date BETWEEN " & SetDate(pr_txttglawal.DateTime.Date) & " AND " & SetDate(pr_txttglakhir.DateTime.Date) & " AND  " _
            & "  f.irc_en_id IN (select user_en_id from tconfuserentity " _
            & " where userid = " & master_new.ClsVar.sUserID.ToString & ") " _
            & "ORDER BY " _
            & "  a.ircd_seq"

        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try

            gv_detail.Columns("ircd_irc_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[ircd_irc_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString & "'")
            gv_detail.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Sub insert_data_awal()
        irc_en_id.ItemIndex = 0
        irc_remarks.EditValue = ""
        irc_start_date.DateTime = CekTanggal.Date
        irc_end_date.DateTime = CekTanggal.Date

        'be_asmb_ps.EditValue = ""
        'asmb_date.DateTime = _now
        'be_asmb_ps.Properties.ReadOnly = True
        'asmb_loc.Text = ""
        'asmb_desc.Text = ""
        'asmb_remarks.Text = ""
        'asmb_qty.Text = 0
        'asmb_en_id.Focus()

        TabbedControlGroup1.SelectedTabPageIndex = 0
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        dt.Rows.Clear()
        dt.AcceptChanges()
        gc_edit.DataSource = dt
        gv_edit.BestFitColumns()

        'ds_edit = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb


        '            .InitializeCommand()
        '            .FillDataSet(ds_edit, "insert_edit")
        '            gc_edit.DataSource = ds_edit.Tables(0)
        '            gv_edit.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()


        'Dim _date As Date = asmb_date.DateTime
        'Dim _gcald_det_status As String = func_data.get_gcald_det_status(asmb_en_id.EditValue, "gcald_ic", _date)

        'If _gcald_det_status = "" Then
        '    MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'ElseIf _gcald_det_status.ToUpper = "Y" Then
        '    MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_detail.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim i As Integer


        ''*********************
        ''Cek part number, Location,account
        'For i = 0 To ds_detail.Tables(0).Rows.Count - 1
        '    If ds_detail.Tables(0).Rows(i).Item("loc_id").ToString.Trim = "" Then
        '        MessageBox.Show("Location Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_detail.Tables(0)).Position = i
        '        Return False
        '    ElseIf ds_detail.Tables(0).Rows(i).Item("pla_ac_id").ToString.Trim = "" Then
        '        MessageBox.Show("Account Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        BindingContext(ds_detail.Tables(0)).Position = i
        '        Return False
        '    ElseIf ds_detail.Tables(0).Rows(i).Item("psd_qty") = 0 Then
        '        MessageBox.Show("Can't Save For Qty 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        '    If ds_detail.Tables(0).Rows(i).Item("cost") = 0 Then
        '        MessageBox.Show("Can't Save For cost 0...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'Next
        ''*********************


        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function insert() As Boolean
        Dim _tran_id, _en_id As Integer
        Dim _serial, _pt_code_det As String
        Dim ssqls As New ArrayList
        Dim _qty, _cost, _cost_avg As Double

        Dim _oid As String
        _oid = Guid.NewGuid.ToString

        Dim ds_bantu As New DataSet
        Dim i, i_2 As Integer



        Dim _code As String
        _code = func_coll.get_transaction_number("IC", irc_en_id.GetColumnValue("en_code"), "irc_mstr", "irc_code")

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
                                            & "  public.irc_mstr " _
                                            & "( " _
                                            & "  irc_oid, " _
                                            & "  irc_code, " _
                                            & "  irc_date, " _
                                            & "  irc_en_id, " _
                                            & "  irc_loc_id, " _
                                            & "  irc_pt_id, " _
                                            & "  irc_start_date, " _
                                            & "  irc_end_date, " _
                                            & "  irc_qty, " _
                                            & "  irc_qty_process, " _
                                            & "  irc_remarks, " _
                                            & "  irc_add_by, " _
                                            & "  irc_add_date, " _
                                            & "  irc_loc_id_lock " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_oid) & ",  " _
                                            & SetSetring(_code) & ",  " _
                                            & SetDateNTime00(irc_date.EditValue) & ",  " _
                                            & SetInteger(irc_en_id.EditValue) & ",  " _
                                            & SetInteger(irc_loc_id.Tag) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetDateNTime00(irc_start_date.EditValue) & ",  " _
                                            & SetDateNTime00(irc_end_date.EditValue) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring(irc_remarks.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(CekTanggal) & ",  " _
                                            & SetInteger(irc_loc_id_lock.Tag) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To dt.Rows.Count - 1
                            '****************************************
                            If SetNumber(dt.Rows(i).Item("pbd_qty_riud")) > 0 Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                            & "  public.ircd_det " _
                                            & "( " _
                                            & "  ircd_oid, " _
                                            & "  ircd_irc_oid, " _
                                            & "  ircd_pb_oid, " _
                                            & "  ircd_pbd_oid,ircd_qty_stock,ircd_en_id,ircd_requested,ircd_end_user, " _
                                            & "  ircd_qty_request, " _
                                            & "  ircd_qty_approve, " _
                                            & "  ircd_qty_outstanding, " _
                                            & "  ircd_qty_use, " _
                                            & "  ircd_pt_id,ircd_flag, " _
                                            & "  ircd_seq " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_oid) & ",  " _
                                            & SetSetring(dt.Rows(i).Item("pb_oid")) & ",  " _
                                            & SetSetring(dt.Rows(i).Item("pbd_oid")) & ",  " _
                                            & SetDec(dt.Rows(i).Item("invc_qty")) & ",  " _
                                            & SetInteger(dt.Rows(i).Item("pb_en_id")) & ",  " _
                                            & SetSetring(dt.Rows(i).Item("pb_requested")) & ",  " _
                                            & SetSetring(dt.Rows(i).Item("pb_end_user")) & ",  " _
                                            & SetDec(dt.Rows(i).Item("pbd_qty")) & ",  " _
                                            & SetDec(dt.Rows(i).Item("pbd_qty_riud")) & ",  " _
                                            & SetDec(dt.Rows(i).Item("qty_stock_remaining")) & ",  " _
                                            & SetDec(0) & ",  " _
                                            & SetInteger(dt.Rows(i).Item("pbd_pt_id")) & ",  " _
                                            & SetInteger(dt.Rows(i).Item("warna")) & ",  " _
                                            & SetDec(i) & "  " _
                                            & ")"


                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update pbd_det set pbd_qty_riud=coalesce(pbd_qty_riud,0) + " & SetDec(dt.Rows(i).Item("pbd_qty_riud")) _
                                        & " where pbd_oid=" & SetSetring(dt.Rows(i).Item("pbd_oid"))

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()


                            End If
                        Next

                        Dim pt_code As String = ""
                        Dim qty As Double = 0
                        Dim drarray() As DataRow
                        For i = 0 To dt.Rows.Count - 1
                            qty = 0
                            If pt_code <> dt.Rows(i).Item("pt_code") Then
                                pt_code = dt.Rows(i).Item("pt_code")
                                drarray = dt.Select("pt_code='" & dt.Rows(i).Item("pt_code").ToString & "'", "pt_code", DataViewRowState.CurrentRows)
                                For x As Integer = 0 To (drarray.Length - 1)
                                    qty += drarray(x)("pbd_qty_riud")
                                Next

                                If qty > 0 Then
                                    _en_id = irc_en_id.EditValue
                                    _si_id = 992
                                    _loc_id = irc_loc_id.Tag
                                    _pt_id = dt.Rows(i).Item("pbd_pt_id")
                                    _pt_code = dt.Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = qty
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _
                                                                        _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If


                                    _loc_id = irc_loc_id_lock.Tag
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _
                                                                      _pt_id, _serial, _qty) = False Then
                                        MsgBox("Error at rows " & i + 1)
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    _qty = _qty
                                    _cost = 0
                                    _cost_avg = 0
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _code, _oid.ToString, "Inv Req Control", "", _
                                                                  _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", CekTanggal()) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = 0
                                    _cost_avg = 0
                                    _loc_id = irc_loc_id.Tag
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _code, _oid.ToString, "Inv Req Control", "", _
                                                                  _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", CekTanggal()) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If


                            End If
                        Next
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Inv Req Control " & _code)
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
                        set_row(Trim(_oid.ToString), "irc_oid")
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
    Private Sub browse_data()
        gv_edit.UpdateCurrentRow()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        'Dim _filter As String
        '_filter = " and b.ac_id in (SELECT  " _
        '        & "  enacc_ac_id " _
        '        & "FROM  " _
        '        & "  public.enacc_mstr  " _
        '        & "Where enacc_en_id=" & SetInteger(asmb_en_id.EditValue) & " and enacc_code='asmbl_account')" 'dbcr_account'asmbl_account


        'If _col = "loc_desc" Then
        '    Dim frm As New FLocationSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._pil = 2
        '    frm._en_id = asmb_en_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()

        'ElseIf _col = "ac_code" Then

        '    Dim frm As New FAccountSearch()
        '    frm.set_win(Me)
        '    If limit_account(asmb_en_id.EditValue) = True Then
        '        frm._obj = _filter
        '    End If
        '    frm._row = _row
        '    frm.type_form = True
        '    frm.ShowDialog()
        'ElseIf _col = "ac_name" Then
        '    Dim frm As New FAccountSearch()
        '    frm.set_win(Me)
        '    frm._row = _row
        '    If limit_account(asmb_en_id.EditValue) = True Then
        '        frm._obj = _filter
        '    End If
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If


    End Sub
    Private Function insert_glt_det_inv_rct(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_type As String, ByVal par_daybook As String) As Boolean
        insert_glt_det_inv_rct = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If par_ds.Tables(0).Rows(i).Item("psd_qty") > 0 Then
                        dt_bantu = New DataTable
                        _pl_id = func_coll.get_prodline(par_ds.Tables(0).Rows(i).Item("psd_pt_bom_id"))
                        dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                        _cost = par_ds.Tables(0).Rows(i).Item("psd_qty") * par_ds.Tables(0).Rows(i).Item("cost")

                        'Insert Untuk Yang debet Dulu
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetring("Inventory Assembly") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                        '********************** finish untuk yang debet

                        'Insert Untuk credit nya....
                        _seq = _seq + 1
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                            & SetDbl(1) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("Inventory Assembly") & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                         par_ds.Tables(0).Rows(i).Item("pla_ac_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("pla_sb_id"), _
                                                         par_ds.Tables(0).Rows(i).Item("pla_cc_id"), _
                                                         par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                         1, _cost, "C") = False Then

                            Return False
                            Exit Function
                        End If
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
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

        If MessageBox.Show("Sure " + master_new.ClsVar.sNama + " delete this data ..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If


        Dim ssqls As New ArrayList
        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status
        Dim i, i_2 As Integer

        Dim _tran_id, _en_id As Integer
        Dim _serial, _pt_code_det As String

        Dim _qty, _cost, _cost_avg As Double

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

                            Dim pt_code As String = ""
                            Dim qty As Double = 0
                            Dim drarray() As DataRow

                            For i = 0 To ds.Tables("detail").Rows.Count - 1
                                If ds.Tables("detail").Rows(i).Item("ircd_irc_oid").ToString = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString Then
                                    qty = 0

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update pbd_det set pbd_qty_riud=coalesce(pbd_qty_riud,0) - " & SetDec(ds.Tables("detail").Rows(i).Item("ircd_qty_approve")) _
                                        & " where pbd_oid=" & SetSetring(ds.Tables("detail").Rows(i).Item("ircd_pbd_oid"))

                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                    If pt_code <> ds.Tables("detail").Rows(i).Item("pt_code") Then
                                        pt_code = ds.Tables("detail").Rows(i).Item("pt_code")

                                        drarray = ds.Tables("detail").Select("pt_code='" & ds.Tables("detail").Rows(i).Item("pt_code").ToString & "' and ircd_irc_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString & "'", "pt_code", DataViewRowState.CurrentRows)
                                        For x As Integer = 0 To (drarray.Length - 1)
                                            qty += drarray(x)("ircd_qty_approve")
                                        Next

                                        If qty > 0 Then
                                            _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_en_id")
                                            _si_id = 992
                                            _loc_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_loc_id_lock")
                                            _pt_id = ds.Tables("detail").Rows(i).Item("ircd_pt_id")
                                            _pt_code = ds.Tables("detail").Rows(i).Item("pt_code")
                                            _serial = "''"
                                            _qty = qty
                                            If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _
                                                     _pt_id, _pt_code, _serial, _qty) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If


                                            _loc_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_loc_id") ' irc_loc_id_lock.Tag
                                            If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _
                                                      _pt_id, _serial, _qty) = False Then
                                                MsgBox("Error at rows " & i + 1)
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            _qty = _qty
                                            _cost = 0
                                            _cost_avg = 0
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_code"), _
                                                                          ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString, "Inv Req Control", "", _
                                                     _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", CekTanggal()) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If

                                            'Update History Inventory                                    
                                            _qty = _qty * -1
                                            _cost = 0
                                            _cost_avg = 0
                                            _loc_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_loc_id_lock")
                                            If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_code"), _
                                                                          ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString, "Inv Req Control", "", _
                                                     _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", CekTanggal()) = False Then
                                                sqlTran.Rollback()
                                                delete_data = False
                                                Exit Function
                                            End If
                                        End If

                                    End If

                                End If
                            Next


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from irc_mstr where irc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()



                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Inventory Request Control " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("irc_code"))
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

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Harus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function


    Private Sub be_asmb_ps_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        'Dim frm As New FPTBOMSrch
        'frm.set_win(Me)
        'frm._en_id = asmb_en_id.EditValue
        'frm._pil = 1
        'frm.type_form = True
        'frm.ShowDialog()
        'sb_generate.PerformClick()
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Try
            If e.Column.Name = "pbd_qty_riud" Then
                Dim _sisa As Double = 0
                Dim _stok As Double = SetNumber(gv_edit.GetRowCellValue(e.RowHandle, "invc_qty"))
                For Each dr As DataRow In dt.Rows
                    If dr("pt_code") = gv_edit.GetRowCellValue(e.RowHandle, "pt_code") Then
                        _sisa += SetNumber(dr("pbd_qty_riud"))
                    End If
                Next
                For Each dr As DataRow In dt.Rows
                    If dr("pt_code") = gv_edit.GetRowCellValue(e.RowHandle, "pt_code") Then
                        If (_stok - _sisa) < 0 Then
                            Box("Qty approve over than stock")
                            gv_edit.CancelUpdateCurrentRow()
                            Exit Sub
                        End If
                        dr("qty_stock_remaining") = _stok - _sisa
                    End If
                Next
                dt.AcceptChanges()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub gv_edit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.Click
        _load = False
    End Sub


    'Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
    '    'browse_data()
    '    Try
    '        Dim _col As String = gv_edit.FocusedColumn.Name
    '        Dim _row As Integer = gv_edit.FocusedRowHandle

    '        If _col = "pbd_qty_riud" Then
    '            gv_edit.SetFocusedRowCellValue("pbd_qty_riud", gv_edit.GetFocusedRowCellValue("pbd_qty"))
    '        End If



    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub be_asmb_ps_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub asmb_loc_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles irc_loc_id.ButtonClick
        'Dim frm As New FLocationSearch
        'frm.set_win(Me)
        'frm._en_id = asmb_en_id.EditValue
        'frm._pil = 1
        'frm.type_form = True
        'frm.ShowDialog()
        'sb_generate.PerformClick()
    End Sub



    Private Sub BtLoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtLoadData.Click
        Try
            Dim sSQL As String

            sSQL = "select * from (SELECT  " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  e.code_name AS um_desc, " _
                & "  a.pb_code, " _
                & "  c.en_desc, " _
                & "  a.pb_requested,'' as warna, " _
                & "  a.pb_end_user, " _
                & "  a.pb_date, " _
                & "  b.pbd_qty,coalesce((select invc_qty from invc_mstr where invc_pt_id=b.pbd_pt_id and invc_en_id=" & SetInteger(irc_en_id.EditValue) _
                & " and invc_loc_id=" & SetInteger(irc_loc_id.Tag) & " order by invc_qty desc  limit 1),0) as invc_qty, " _
                & "  coalesce(b.pbd_qty_riud, 0) AS pbd_qty_riud, 0.0 as qty_stock_remaining, " _
                & "  b.pbd_qty_processed, " _
                & "  d.pt_desc2, " _
                & "  a.pb_oid, " _
                & "  a.pb_en_id, " _
                & "  b.pbd_pt_id, " _
                & "  d.pt_um, " _
                & "  b.pbd_oid, " _
                & "  a.pb_status, " _
                & "  a.pb_close_date, " _
                & "  a.pb_trans_id " _
                & "FROM " _
                & "  public.pb_mstr a " _
                & "  INNER JOIN public.pbd_det b ON (a.pb_oid = b.pbd_pb_oid) " _
                & "  INNER JOIN public.en_mstr c ON (a.pb_en_id = c.en_id) " _
                & "  INNER JOIN public.pt_mstr d ON (b.pbd_pt_id = d.pt_id) " _
                & "  INNER JOIN public.code_mstr e ON (d.pt_um = e.code_id) " _
                & "WHERE " _
                & "  a.pb_date BETWEEN " & SetDate(irc_start_date.DateTime) & " AND " & SetDate(irc_end_date.DateTime) & " AND  " _
                & "  coalesce(b.pbd_qty_riud, 0) = 0 and pb_pbt_code='REQCBG' and coalesce(pb_status,'') <> 'C'  and coalesce(pbd_qty_processed,0)=0 ) as temp where  1=1  " & IIf(ce0.EditValue = True, "and invc_qty>0", "") _
                & " ORDER BY " _
                & "  pt_desc1,pb_date, " _
                & "  en_desc "


            dt = master_new.PGSqlConn.GetTableData(sSQL)
            Dim _status As Boolean = False

            For Each dr As DataRow In dt.Rows
                If _pt_code <> dr("pt_code") Then
                    _pt_code = dr("pt_code")
                    If _status = True Then
                        _status = False
                    Else
                        _status = True
                    End If

                End If
                dr("qty_stock_remaining") = dr("invc_qty")
                If _status = True Then
                    dr("warna") = "1"
                Else
                    dr("warna") = "0"
                End If
            Next
            dt.AcceptChanges()
            gc_edit.DataSource = dt
            gv_edit.BestFitColumns()

            'If ce0.EditValue = True Then
            '    gv_edit.ActiveFilterString = "[invc_qty]>0"
            'Else
            '    gv_edit.ActiveFilterString = ""
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub
    Private Sub VGridControl1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        DoRowDoubleClick(view, pt)

    End Sub

    Private inplaceEditor As BaseEdit

    Private Sub DoRowDoubleClick(ByVal view As GridView, ByVal pt As Point)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        If info.InRow OrElse info.InRowCell Then
            Dim colCaption As String
            If info.Column Is Nothing Then
                colCaption = "N/A"
            Else
                colCaption = info.Column.GetCaption()
            End If
            If colCaption = "Qty Approve" Then
                gv_edit.SetFocusedRowCellValue("pbd_qty_riud", gv_edit.GetFocusedRowCellValue("pbd_qty"))
            End If



            ' MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption))
        End If
    End Sub
    Private Sub inplaceEditor_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim editor As BaseEdit = CType(sender, BaseEdit)
        Dim grid As GridControl = CType(editor.Parent, GridControl)
        Dim pt As Point = grid.PointToClient(Control.MousePosition)
        Dim view As GridView = CType(grid.FocusedView, GridView)
        DoRowDoubleClick(view, pt)
    End Sub

    Private Sub gridView1_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gv_edit.ShownEditor
        inplaceEditor = (CType(sender, GridView)).ActiveEditor
        AddHandler inplaceEditor.DoubleClick, AddressOf inplaceEditor_DoubleClick
    End Sub

    Private Sub gridView1_HiddenEditor(ByVal sender As Object, ByVal e As EventArgs) Handles gv_edit.HiddenEditor
        If inplaceEditor IsNot Nothing Then
            RemoveHandler inplaceEditor.DoubleClick, AddressOf inplaceEditor_DoubleClick
            inplaceEditor = Nothing
        End If
    End Sub


    Private Sub GridView1_RowStyle(ByVal sender As Object, _
ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gv_edit.RowStyle

        Dim View As GridView = sender

        If (e.RowHandle >= 0) Then
            Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("warna"))

            If category = "1" Then
                e.Appearance.BackColor = Color.White
                e.Appearance.BackColor2 = Color.WhiteSmoke
            Else
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If


        End If


    End Sub
    Private Sub GridViewDetail_RowStyle(ByVal sender As Object, _
ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles gv_detail.RowStyle

        'Dim View As GridView = sender

        'If (e.RowHandle >= 0) Then
        '    Dim category As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("ircd_flag"))

        '    If category = "1" Then
        '        e.Appearance.BackColor = Color.White
        '        e.Appearance.BackColor2 = Color.WhiteSmoke
        '    Else
        '        e.Appearance.BackColor = Color.Salmon
        '        e.Appearance.BackColor2 = Color.SeaShell
        '    End If


        'End If


    End Sub
End Class
