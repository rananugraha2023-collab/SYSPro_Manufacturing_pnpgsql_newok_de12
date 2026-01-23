Imports npgsql
Imports master_new.ModFunction

Public Class FReqTransferReceipt
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public ds_edit, ds_serial As DataSet
    Dim _now As DateTime
    Dim _reqs_oid_mstr As String
    Dim _reqs_code_mstr As String
    Dim _conf_value As String

    Private Sub FReqTransferReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        _conf_value = func_coll.get_conf_file("wf_req_transfer_issue")
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        reqs_en_id.Properties.DataSource = dt_bantu
        reqs_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        reqs_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        reqs_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        reqs_en_id_to.Properties.DataSource = dt_bantu
        reqs_en_id_to.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        reqs_en_id_to.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        reqs_en_id_to.ItemIndex = 0
    End Sub

    Private Sub reqs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reqs_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(reqs_en_id.EditValue))
        reqs_si_id.Properties.DataSource = dt_bantu
        reqs_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        reqs_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        reqs_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id.EditValue))
        reqs_loc_id_from.Properties.DataSource = dt_bantu
        reqs_loc_id_from.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_from.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_from.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id.EditValue))
        reqs_loc_id_git.Properties.DataSource = dt_bantu
        reqs_loc_id_git.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_git.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_git.ItemIndex = 0
    End Sub

    Private Sub reqs_en_to_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles reqs_en_id_to.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(reqs_en_id_to.EditValue))
        reqs_si_to_id.Properties.DataSource = dt_bantu
        reqs_si_to_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        reqs_si_to_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        reqs_si_to_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(reqs_en_id_to.EditValue))
        reqs_loc_id_to.Properties.DataSource = dt_bantu
        reqs_loc_id_to.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        reqs_loc_id_to.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        reqs_loc_id_to.EditValue = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transfer Issue Number", "reqs_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transfer Issue Date", "reqs_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Transfer Receipt Date", "reqs_receive_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Entity From", "en_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site From", "si_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location From", "loc_desc_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location GIT", "loc_desc_git", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site To", "si_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location To", "loc_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "reqs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status", "reqs_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "reqs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "reqs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "reqs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "reqs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "reqds_reqs_oid", False)
        add_column_copy(gv_detail, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Qty Kirim", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "reqds_oid", False)
        add_column(gv_edit, "pt_id", False)
        add_column(gv_edit, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Qty Issue", "reqds_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "reqds_um", False)
        add_column(gv_edit, "UM", "reqds_um_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  reqs_oid, " _
                    & "  reqs_dom_id, " _
                    & "  reqs_add_by, " _
                    & "  reqs_add_date, " _
                    & "  reqs_upd_by, " _
                    & "  reqs_upd_date, " _
                    & "  reqs_code, " _
                    & "  reqs_req_oid, " _
                    & "  reqs_date, " _
                    & "  reqs_receive_date, " _
                    & "  reqs_en_id, " _
                    & "  en_mstr_from.en_desc as en_desc_from, " _
                    & "  reqs_si_id, " _
                    & "  si_mstr_from.si_desc as si_desc_from, " _
                    & "  reqs_loc_id_from, " _
                    & "  loc_mstr_from.loc_desc as loc_desc_from, " _
                    & "  reqs_loc_id_git, " _
                    & "  loc_mstr_git.loc_desc as loc_desc_git, " _
                    & "  reqs_en_id_to, " _
                    & "  en_mstr_to.en_desc as en_desc_to, " _
                    & "  reqs_si_to_id, " _
                    & "  si_mstr_to.si_desc as si_desc_to, " _
                    & "  reqs_loc_id_to, " _
                    & "  loc_mstr_to.loc_desc as loc_desc_to, " _
                    & "  reqs_remarks, " _
                    & "  reqs_trans_id, " _
                    & "  reqs_dt   " _
                    & "FROM  " _
                    & "  public.reqs_mstr " _
                    & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
                    & "  inner join si_mstr si_mstr_from on si_mstr_from.si_id = reqs_si_id " _
                    & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
                    & "  inner join loc_mstr loc_mstr_git on loc_mstr_git.loc_id = reqs_loc_id_git " _
                    & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
                    & "  inner join si_mstr si_mstr_to on si_mstr_to.si_id = reqs_si_to_id " _
                    & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
                    & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and reqs_en_id_to in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        If _conf_value = "1" Then
            get_sequel = get_sequel + " and reqs_trans_id in ('I','C','X') "
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
            & "  pt_type, " _
            & "  pt_cost, " _
            & "  reqds_qty, " _
            & "  reqds_um, " _
            & "  code_name as reqds_um_name, " _
            & "  reqds_qty_real, reqds_cost " _
            & "  reqds_dt " _
            & "FROM  " _
            & "  public.reqsd_det " _
            & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr on code_id = reqds_um " _
            & "  where reqs_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
            & "  and reqs_date <= " + SetDate(pr_txttglakhir.DateTime.Date) + ""
        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("reqds_reqs_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("reqds_reqs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("reqds_reqs_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If before_edit() = False Then
            Return False
        End If

        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _reqs_oid_mstr = .Item("reqs_oid").ToString
                _reqs_code_mstr = .Item("reqs_code")
                reqs_receive_date.DateTime = _now
                reqs_code.Text = .Item("reqs_code")
                reqs_en_id.EditValue = .Item("reqs_en_id")
                reqs_si_id.EditValue = .Item("reqs_si_id")
                reqs_loc_id_from.EditValue = .Item("reqs_loc_id_from")
                reqs_loc_id_git.EditValue = .Item("reqs_loc_id_git")
                reqs_en_id_to.EditValue = .Item("reqs_en_id_to")
                reqs_si_to_id.EditValue = .Item("reqs_si_to_id")
                reqs_loc_id_to.EditValue = .Item("reqs_loc_id_to")
                reqs_remarks.Text = SetString(.Item("reqs_remarks"))
            End With
            reqs_code.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
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
                                & "  reqs_req_oid, " _
                                & "  reqds_seq, " _
                                & "  pt_id, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  pt_cost, " _
                                & "  pt_type, " _
                                & "  reqds_qty, " _
                                & "  reqds_um, " _
                                & "  code_name as reqds_um_name, " _
                                & "  reqds_qty_real, reqds_cost, " _
                                & "  reqds_dt " _
                                & "FROM  " _
                                & "  public.reqsd_det " _
                                & "  inner join reqs_mstr on reqs_oid = reqds_reqs_oid " _
                                & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
                                & "  inner join pt_mstr on pt_id = reqd_pt_id " _
                                & "  inner join code_mstr on code_id = reqds_um " _
                                & "  where reqsd_det.reqds_reqs_oid = '" + ds.Tables(0).Rows(row).Item("reqs_oid").ToString + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
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

    Public Overrides Function before_edit() As Boolean
        before_edit = True
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_trans_id").ToString.ToUpper = "C" Then
            MessageBox.Show("Can't Edit Closed Transaction..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim _serial, _pt_code As String
        'Dim  _cost_methode As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ssqls As New ArrayList

        _tran_id = func_coll.get_id_tran_mstr("rct-tr")
        If _tran_id = -1 Then
            MessageBox.Show("Transfer Receipt In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            edit = False
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
                        .Command.CommandText = "update reqs_mstr set reqs_receive_date = " + SetDate(reqs_receive_date.DateTime) + ", reqs_trans_id = 'C' " _
                                             & " where reqs_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'Untuk update po_close_date apabile semua line sudah terpenuhi qty_receivenya...
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "update req_mstr set req_trans_id = 'C' " + _
                                               " where coalesce((select count(reqd_req_oid) as jml From reqd_det " + _
                                               " where reqd_qty <> coalesce(reqd_qty_completed,0) " + _
                                               " and reqd_req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_req_oid").ToString + "'" + _
                                               " group by reqd_req_oid),0) = 0 " + _
                                               " and req_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_req_oid").ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '*********************************************************************************
                        'Proses Pengurangan untuk lokasi git
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                    'If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = reqs_en_id.EditValue
                                    _si_id = reqs_si_id.EditValue
                                    _loc_id = reqs_loc_id_git.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _qty = _qty * -1
                                    _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                                    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code_mstr, _reqs_oid_mstr.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_receive_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If
                                    'End If
                                End If
                            End If
                        Next

                        ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        '' tidak aada serial di databasenya.....
                        ''For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        ''    If ds_serial.Tables(0).Rows(i).Item("reqdss_qty_receipt") > 0 Then
                        ''        i_2 += 1

                        ''        _en_id = reqs_en_id.EditValue
                        ''        _si_id = reqs_si_id.EditValue
                        ''        _loc_id = reqs_loc_git.EditValue
                        ''        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        ''        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        ''        _serial = ds_serial.Tables(0).Rows(i).Item("reqdss_lot_serial")
                        ''        _qty = ds_serial.Tables(0).Rows(i).Item("reqdss_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        ''        'If func_coll.update_invc_mstr_plus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        ''        If func_coll.update_invc_mstr_minus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                        ''            sqlTran.Rollback()
                        ''            edit = False
                        ''            Exit Function
                        ''        End If

                        ''        'Update History Inventory
                        ''        _cost = ds_serial.Tables(0).Rows(i).Item("reqdss_cost")
                        ''        _cost = _cost * -1
                        ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _reqs_code_mstr, _reqs_oid_mstr.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                        ''            sqlTran.Rollback()
                        ''            edit = False
                        ''            Exit Function
                        ''        End If
                        ''    End If
                        ''Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '        _en_id = reqs_en_id.EditValue
                        '        _si_id = reqs_si_id.EditValue
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty") 'karena ini GIT maka harus dikurangi semua qty nya...
                        '        _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                        '        If _cost_methode = "F" Or _cost_methode = "L" Then
                        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '            Return False
                        '            'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    edit = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                edit = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        '*********************************************************************************
                        'Proses penambahan (+) untuk lokasi tujuan
                        'Update Table Inventory Dan Cost Inventory Dan History Inventory
                        '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
                        i_2 = 0
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                                If ds_edit.Tables(0).Rows(i).Item("reqds_qty") > 0 Then
                                    'If ds_edit.Tables(0).Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                                    i_2 += 1

                                    _en_id = reqs_en_id_to.EditValue
                                    _si_id = reqs_si_to_id.EditValue
                                    _loc_id = reqs_loc_id_to.EditValue
                                    _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                                    _pt_code = ds_edit.Tables(0).Rows(i).Item("pt_code")
                                    _serial = "''"
                                    _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                                    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, i_2, _en_id, _reqs_code_mstr, _reqs_oid_mstr.ToString, "Transfer Receipt", "", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", reqs_receive_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        edit = False
                                        Exit Function
                                    End If
                                    'End If
                                End If
                            End If
                        Next

                        ''2. Update invc_mstr dan invh_mstr untuk barang yang lot / serial
                        '' tidak ada table serial di databasenya...
                        ''For i = 0 To ds_serial.Tables(0).Rows.Count - 1
                        ''    If ds_serial.Tables(0).Rows(i).Item("reqdss_qty_receipt") > 0 Then
                        ''        i_2 += 1

                        ''        _en_id = reqs_en_to_id.EditValue
                        ''        _si_id = reqs_si_to_id.EditValue
                        ''        _loc_id = reqs_loc_to_id.EditValue
                        ''        _pt_id = ds_serial.Tables(0).Rows(i).Item("pt_id")
                        ''        _pt_code = ds_serial.Tables(0).Rows(i).Item("pt_code")
                        ''        _serial = ds_serial.Tables(0).Rows(i).Item("reqdss_lot_serial")
                        ''        _qty = ds_serial.Tables(0).Rows(i).Item("reqdss_qty_receipt")
                        ''        If func_coll.update_invc_mstr_plus(objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                        ''            sqlTran.Rollback()
                        ''            edit = False
                        ''            Exit Function
                        ''        End If

                        ''        'Update History Inventory
                        ''        _cost = ds_serial.Tables(0).Rows(i).Item("reqdss_cost")
                        ''        If func_coll.update_invh_mstr(objinsert, _tran_id, i_2, _en_id, _reqs_code_mstr, _reqs_oid_mstr.ToString, "Transfer Issue", "", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                        ''            sqlTran.Rollback()
                        ''            edit = False
                        ''            Exit Function
                        ''        End If
                        ''    End If
                        ''Next

                        ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    If ds_edit.Tables(0).Rows(i).Item("pt_type") = "I" Then
                        '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("pt_id").ToString.ToUpper)
                        '        _en_id = reqs_en_id_to.EditValue
                        '        _si_id = reqs_si_to_id.EditValue
                        '        _pt_id = ds_edit.Tables(0).Rows(i).Item("pt_id")
                        '        _qty = ds_edit.Tables(0).Rows(i).Item("reqds_qty")
                        '        _cost = ds_edit.Tables(0).Rows(i).Item("reqds_cost")
                        '        If _cost_methode = "F" Or _cost_methode = "L" Then
                        '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '            Return False
                        '            'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '            '    sqlTran.Rollback()
                        '            '    edit = False
                        '            '    Exit Function
                        '            'End If
                        '        ElseIf _cost_methode = "A" Then
                        '            _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                        '            If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                        '                sqlTran.Rollback()
                        '                edit = False
                        '                Exit Function
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        '*****************************************************************************************

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                        '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                        '                         _rcv_oid.ToString, _rcv_code, _
                        '                         func_coll.get_tanggal_sistem, _
                        '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                        '                         "IC", "IC-RPO") = False Then
                        '    sqlTran.Rollback()
                        '    insert = False
                        '    Exit Function
                        'End If

                        'If func_coll.update_tranaprvd_det(ssqls, objinsert, reqs_en_id_to.EditValue, 4, _reqs_oid_mstr.ToString, _reqs_code_mstr, _now.Date) = False Then
                        '    'sqlTran.Rollback()
                        '    'edit = False
                        '    'Exit Function
                        'End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Inv Transfer Receipt " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("req_code"))
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
                        set_row(Trim(_reqs_oid_mstr), "reqs_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String

        _en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_en_id")
        _type = 4
        _table = "reqs_mstr"
        _initial = "reqs"
        _code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        _code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")

        func_coll.update_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  reqs_oid, " _
            & "  reqs_dom_id, " _
            & "  reqs_en_id, " _
            & "  reqs_add_by, " _
            & "  reqs_add_date, " _
            & "  reqs_upd_by, " _
            & "  reqs_upd_date, " _
            & "  reqs_code, " _
            & "  reqs_date, " _
            & "  reqs_req_oid, " _
            & "  reqs_en_id_to, " _
            & "  reqs_loc_id_from, " _
            & "  reqs_loc_id_git, " _
            & "  reqs_loc_id_to, " _
            & "  reqs_trans_id, " _
            & "  reqs_receive_date, " _
            & "  reqs_remarks, " _
            & "  reqs_dt, " _
            & "  reqs_si_id, " _
            & "  reqs_si_to_id, " _
            & "  req_code, " _
            & "  en_mstr_from.en_desc as en_desc_from, " _
            & "  en_mstr_to.en_desc as en_desc_to, " _
            & "  loc_mstr_from.loc_desc as loc_desc_from, " _
            & "  loc_mstr_to.loc_desc as loc_desc_to, " _
            & "  cmaddr_mstr_from.cmaddr_name as cmaddr_name_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_1 as cmaddr_line_1_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_2 as cmaddr_line_2_from, " _
            & "  cmaddr_mstr_from.cmaddr_line_3 as cmaddr_line_3_from, " _
            & "  cmaddr_mstr_to.cmaddr_name as cmaddr_name_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_1 as cmaddr_line_1_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_2 as cmaddr_line_2_to, " _
            & "  cmaddr_mstr_to.cmaddr_line_3 as cmaddr_line_3_to, " _
            & "  pt_code, " _
            & "  pt_desc1, " _
            & "  pt_desc2, " _
            & "  reqds_qty, " _
            & "  code_name,  " _
            & "  coalesce(tranaprvd_name_5,'') as tranaprvd_name_5, coalesce(tranaprvd_name_6,'') as tranaprvd_name_6, coalesce(tranaprvd_name_7,'') as tranaprvd_name_7, coalesce(tranaprvd_name_8,'') as tranaprvd_name_8, " _
            & "  tranaprvd_pos_5, tranaprvd_pos_6, tranaprvd_pos_7, tranaprvd_pos_8 " _
            & "FROM  " _
            & "  public.reqs_mstr " _
            & "  inner join req_mstr on req_oid = reqs_req_oid " _
            & "  inner join en_mstr en_mstr_from on en_mstr_from.en_id = reqs_en_id " _
            & "  inner join en_mstr en_mstr_to on en_mstr_to.en_id = reqs_en_id_to " _
            & "  inner join loc_mstr loc_mstr_from on loc_mstr_from.loc_id = reqs_loc_id_from " _
            & "  inner join loc_mstr loc_mstr_to on loc_mstr_to.loc_id = reqs_loc_id_to " _
            & "  inner join cmaddr_mstr cmaddr_mstr_from on cmaddr_mstr_from.cmaddr_en_id = reqs_en_id " _
            & "  inner join cmaddr_mstr cmaddr_mstr_to on cmaddr_mstr_to.cmaddr_en_id = reqs_en_id_to " _
            & "  inner join reqsd_det on reqsd_det.reqds_reqs_oid = reqs_oid " _
            & "  inner join reqd_det on reqd_oid = reqds_reqd_oid " _
            & "  inner join pt_mstr on pt_id = reqd_pt_id " _
            & "  inner join code_mstr on code_id = reqds_um " _
            & "  left outer join tranaprvd_dok on tranaprvd_tran_oid = reqs_oid " _
            & "  where reqs_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code") + "'"


        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRReqTransferReceipt"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("reqs_code")
        frm.ShowDialog()


    End Sub
End Class
