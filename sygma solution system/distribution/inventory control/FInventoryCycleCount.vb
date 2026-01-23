Imports npgsql
Imports master_new.ModFunction

Public Class FInventoryCycleCount
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _pt_ls As String = ""
    Public _pt_id_global As Integer

    Private Sub FInventoryCycleCount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ccre_en_id.Properties.DataSource = dt_bantu
        ccre_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ccre_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ccre_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccre_type())
        ccre_type.Properties.DataSource = dt_bantu
        ccre_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ccre_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ccre_type.ItemIndex = 0
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ccre_en_id.EditValue))
        ccre_si_id.Properties.DataSource = dt_bantu
        ccre_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ccre_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ccre_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(ccre_en_id.EditValue))
        ccre_loc_id.Properties.DataSource = dt_bantu
        ccre_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        ccre_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ccre_loc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ccre_en_id.EditValue, "unitmeasure"))
        ccre_um_id.Properties.DataSource = dt_bantu
        ccre_um_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ccre_um_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ccre_um_id.ItemIndex = 0
    End Sub

    Private Sub ccre_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ccre_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "ccre_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Type", "ccre_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Lot/Serial", "ccre_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty Opname", "ccre_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Old", "ccre_qty_old", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM Conversion", "ccre_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Real", "ccre_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost", "ccre_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "ccre_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ccre_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ccre_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ccre_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ccre_oid, " _
                    & "  ccre_date, " _
                    & "  ccre_add_by, " _
                    & "  ccre_add_date, " _
                    & "  ccre_upd_by, " _
                    & "  ccre_upd_date, " _
                    & "  ccre_type, " _
                    & "  ccre_pt_id, " _
                    & "  ccre_si_id, " _
                    & "  ccre_loc_id, " _
                    & "  ccre_lot_serial, " _
                    & "  ccre_qty, " _
                    & "  ccre_qty_old, " _
                    & "  ccre_um_id, " _
                    & "  ccre_um_conv, " _
                    & "  ccre_qty_real, " _
                    & "  ccre_cost, " _
                    & "  ccre_dt, " _
                    & "  ccre_en_id, " _
                    & "  en_desc, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  si_desc, " _
                    & "  loc_desc, " _
                    & "  um_master.code_name as um_name " _
                    & "FROM  " _
                    & "  public.ccre_mstr " _
                    & "  inner join en_mstr on en_id = ccre_en_id " _
                    & "  inner join pt_mstr on pt_id = ccre_pt_id " _
                    & "  inner join si_mstr on si_id = ccre_si_id " _
                    & "  inner join loc_mstr on loc_id = ccre_loc_id " _
                    & "  inner join code_mstr um_master on um_master.code_id = ccre_um_id" _
                    & " where ccre_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and ccre_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and ccre_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ccre_en_id.Focus()
        ccre_en_id.ItemIndex = 0
        ccre_date.DateTime = _now
        ccre_pt_id.Text = ""
        ccre_loc_id.ItemIndex = 0
        ccre_type.ItemIndex = 0
        ccre_lot_serial.Text = ""
        ccre_qty.EditValue = 0
        ccre_um_conv.EditValue = 1
        ccre_cost.EditValue = 0
        ccre_qty_real.EditValue = 0
        ccre_qty_real.Properties.ReadOnly = True
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim _date As Date = ccre_date.DateTime
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ccre_en_id.EditValue, "gcald_ic", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _pt_ls.ToUpper = "S" Then
            If ccre_qty.EditValue > 1 Then
                MessageBox.Show("Qty Can't Higher Than 1 For Serial Item..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If _pt_ls.ToUpper = "S" Then
            If Trim(ccre_lot_serial.Text) = "" Then
                MessageBox.Show("Serial Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If _pt_ls.ToUpper = "L" Then
            If Trim(ccre_lot_serial.Text) = "" Then
                MessageBox.Show("Lot Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        End If

        If ccre_type.EditValue.ToString.ToUpper = "I" Then
            If ccre_qty_real.EditValue < 1 Then
                MessageBox.Show("Qty Can't Lower Than 1..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ccre_qty.Focus()
                Return False
            End If
        End If

        Return before_save
    End Function

    Private Sub ccre_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ccre_qty.EditValueChanged
        ccre_qty_real.EditValue = ccre_qty.EditValue * ccre_um_conv.EditValue
    End Sub

    Private Sub ccre_um_conv_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ccre_um_conv.EditValueChanged
        ccre_qty_real.EditValue = ccre_qty.EditValue * ccre_um_conv.EditValue
    End Sub

    Private Sub ccre_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ccre_pt_id.ButtonClick
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = ccre_en_id.EditValue
        frm._si_id = ccre_si_id.EditValue
        frm._obj = ccre_pt_id
        frm.type_form = True
        frm.ShowDialog()
    End Sub

#Region "DML"
    Public Function get_qty_old() As Double
        get_qty_old = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(sum(invc_qty),0) as sum_qty from invc_mstr " _
                                         & " where invc_en_id = " + ccre_en_id.EditValue.ToString _
                                         & " and invc_si_id =  " + ccre_si_id.EditValue.ToString _
                                         & " and invc_loc_id =  " + ccre_loc_id.EditValue.ToString _
                                         & " and invc_pt_id =  " + _pt_id_global.ToString _
                                         & " and coalesce(invc_serial,'') = '" + Trim(ccre_lot_serial.Text) + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_qty_old = .DataReader("sum_qty")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try

        Return get_qty_old
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ccre_oid As Guid = Guid.NewGuid

        Dim _serial, _cost_methode, _pt_code As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim ssqls As New ArrayList

        If ccre_type.EditValue.ToString.ToUpper = "I" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-cnt")
        ElseIf ccre_type.EditValue.ToString.ToUpper = "R" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-rcnt")
        End If

        If _tran_id = -1 Then
            MessageBox.Show("Inventory Begining Balance In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

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
                                            & "  public.ccre_mstr " _
                                            & "( " _
                                            & "  ccre_oid, " _
                                            & "  ccre_date, " _
                                            & "  ccre_add_by, " _
                                            & "  ccre_add_date, " _
                                            & "  ccre_type, " _
                                            & "  ccre_pt_id, " _
                                            & "  ccre_si_id, " _
                                            & "  ccre_loc_id, " _
                                            & "  ccre_lot_serial, " _
                                            & "  ccre_qty, " _
                                            & "  ccre_um_id, " _
                                            & "  ccre_um_conv, " _
                                            & "  ccre_qty_real, " _
                                            & "  ccre_cost, " _
                                            & "  ccre_dt, " _
                                            & "  ccre_qty_old, " _
                                            & "  ccre_en_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ccre_oid.ToString) & ",  " _
                                            & SetDate(ccre_date.DateTime) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetSetring(ccre_type.EditValue) & ",  " _
                                            & SetInteger(_pt_id_global) & ",  " _
                                            & SetInteger(ccre_si_id.EditValue) & ",  " _
                                            & SetInteger(ccre_loc_id.EditValue) & ",  " _
                                            & SetSetring(ccre_lot_serial.Text) & ",  " _
                                            & SetDbl(ccre_qty.EditValue) & ",  " _
                                            & SetInteger(ccre_um_id.EditValue) & ",  " _
                                            & SetDbl(ccre_um_conv.EditValue) & ",  " _
                                            & SetDbl(ccre_qty_real.EditValue) & ",  " _
                                            & SetDbl(ccre_cost.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDbl(get_qty_old()) & ",  " _
                                            & SetInteger(ccre_en_id.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If ccre_type.EditValue = "R" Then
                            'Update Table Inventory Dan Cost Inventory Dan History Inventory
                            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial

                            If ccre_qty_real.EditValue > 0 Then
                                _en_id = ccre_en_id.EditValue
                                _si_id = ccre_si_id.EditValue
                                _loc_id = ccre_loc_id.EditValue
                                _pt_id = _pt_id_global
                                _serial = IIf(Trim(ccre_lot_serial.Text) = "", "''", Trim(ccre_lot_serial.Text))
                                _qty = ccre_qty_real.EditValue
                                If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory                                    
                                _cost = ccre_cost.EditValue
                                _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "R", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ccre_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                '2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                '_cost_methode = func_coll.get_pt_cost_method(_pt_id_global)
                                '_en_id = ccre_en_id.EditValue
                                '_pt_id = _pt_id_global
                                '_qty = ccre_qty_real.EditValue
                                '_cost = ccre_cost.EditValue
                                'If _cost_methode = "F" Or _cost_methode = "L" Then
                                '    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                                '    Return False
                                '    'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                                '    '    sqlTran.Rollback()
                                '    '    insert = False
                                '    '    Exit Function
                                '    'End If
                                'ElseIf _cost_methode = "A" Then
                                '    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_id, _pt_id, _qty, _cost)
                                '    If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                                '        sqlTran.Rollback()
                                '        insert = False
                                '        Exit Function
                                '    End If
                                'End If

                                'Insert ke table glt_det
                                'Di proses ini langsung ke prodline account tidak ke prodline location account...
                                'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                                If _create_jurnal = True Then
                                    If insert_glt_det_inv_rcn_plus(ssqls, objinsert, _ccre_oid.ToString, "", ccre_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            ElseIf ccre_qty_real.EditValue < 0 Then
                                _en_id = ccre_en_id.EditValue
                                _si_id = ccre_si_id.EditValue
                                _loc_id = ccre_loc_id.EditValue
                                _pt_id = _pt_id_global
                                _pt_code = ccre_pt_id.Text
                                _serial = IIf(Trim(ccre_lot_serial.Text) = "", "''", Trim(ccre_lot_serial.Text))
                                _qty = ccre_qty_real.EditValue * -1
                                If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory                                    
                                _cost = ccre_cost.EditValue
                                _qty = ccre_qty_real.EditValue
                                _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "R", _si_id, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", ccre_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                ''2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                '_cost_methode = func_coll.get_pt_cost_method(_pt_id_global)
                                '_en_id = ccre_en_id.EditValue
                                '_pt_id = _pt_id_global
                                '_qty = ccre_qty_real.EditValue * -1
                                '_cost = ccre_cost.EditValue
                                'If _cost_methode = "F" Or _cost_methode = "L" Then
                                '    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                                '    Return False
                                '    'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                                '    '    sqlTran.Rollback()
                                '    '    insert = False
                                '    '    Exit Function
                                '    'End If
                                'ElseIf _cost_methode = "A" Then
                                '    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                                '    If func_coll.update_item_cost_avg(ssqls, objinsert, _si_id, _pt_id, _cost_avg) = False Then
                                '        sqlTran.Rollback()
                                '        insert = False
                                '        Exit Function
                                '    End If
                                'End If

                                'Insert ke table glt_det
                                'Di proses ini langsung ke prodline account tidak ke prodline location account...
                                'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                                If _create_jurnal = True Then
                                    If insert_glt_det_inv_rcn_minus(ssqls, objinsert, _ccre_oid.ToString, "", ccre_date.DateTime) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If
                                End If
                            End If
                        ElseIf ccre_type.EditValue = "I" Then
                            insert = False
                            Box("Can't use Initial transaction")
                            Exit Function
                            If ccre_qty_real.EditValue > 0 Then
                                _en_id = ccre_en_id.EditValue
                                _si_id = ccre_si_id.EditValue
                                _loc_id = ccre_loc_id.EditValue
                                _pt_id = _pt_id_global
                                _serial = IIf(Trim(ccre_lot_serial.Text) = "", "''", Trim(ccre_lot_serial.Text))
                                _qty = ccre_qty_real.EditValue
                                If func_coll.update_invc_mstr_adj(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                'Update History Inventory                                    
                                _cost = ccre_cost.EditValue
                                If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Initial", "I", _si_id, _loc_id, _pt_id, _qty, _cost, _cost, "", ccre_date.DateTime) = False Then
                                    sqlTran.Rollback()
                                    insert = False
                                    Exit Function
                                End If

                                '2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                _cost_methode = func_coll.get_pt_cost_method(_pt_id_global)
                                _en_id = ccre_en_id.EditValue
                                _pt_id = _pt_id_global
                                _qty = ccre_qty_real.EditValue
                                _cost = ccre_cost.EditValue
                                If _cost_methode = "F" Or _cost_methode = "L" Then
                                    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                                    Return False
                                    'If func_coll.update_invct_table_plus(objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                ElseIf _cost_methode = "A" Then
                                    'Untuk Initial tidak dilakukan perhitungan average
                                    'If func_coll.update_item_cost_begining_balance(ssqls, objinsert, _en_id, _pt_id, _cost) = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                End If

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
                            End If
                        End If

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Cycle Count " & ccre_pt_id.EditValue)
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
                        set_row(_ccre_oid.ToString, "ccre_oid")
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

    Private Function insert_glt_det_inv_rcn_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_oid As String, ByVal par_trans_code As String, ByVal par_date As Date) As Boolean
        insert_glt_det_inv_rcn_minus = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number("IC", ccre_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

        With par_obj
            Try
                If ccre_qty.EditValue < 0 Then
                    'Debet nya dulu
                   
                    _cost = ccre_qty_real.EditValue * ccre_cost.EditValue * -1
                    _pl_id = func_coll.get_prodline(_pt_id_global)
                    'Insert Untuk debet nya....
                    dt_bantu = New DataTable
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_DSCRP-")

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
                                        & SetInteger(ccre_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency") & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("CYC-RCNT") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     ccre_en_id.EditValue, master_new.ClsVar.ibase_cur_id, _
                                                     1, _cost, "D") = False Then

                        Return False
                        Exit Function
                    End If

                    dt_bantu = New DataTable
                    _pl_id = func_coll.get_prodline(_pt_id_global)
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                    'Insert Untuk Yang credit
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
                                        & SetInteger(ccre_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency") & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("CYC-RCNT") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     ccre_en_id.EditValue, master_new.ClsVar.ibase_cur_id, _
                                                     1, _cost, "C") = False Then

                        Return False
                        Exit Function
                    End If
                    '********************** finish untuk yang debet


                End If
                Return True
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Private Function insert_glt_det_inv_rcn_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_oid As String, ByVal par_trans_code As String, ByVal par_date As String) As Boolean
        insert_glt_det_inv_rcn_plus = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number("IC", ccre_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

        With par_obj
            Try
                If ccre_qty.EditValue > 0 Then
                    'Debet nya dulu
                    dt_bantu = New DataTable
                    _pl_id = func_coll.get_prodline(_pt_id_global)
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                    _cost = ccre_qty_real.EditValue * ccre_cost.EditValue

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
                                        & SetInteger(ccre_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency") & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("CYC-RCNT") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     ccre_en_id.EditValue, master_new.ClsVar.ibase_cur_id, _
                                                     1, _cost, "D") = False Then

                        Return False
                        Exit Function
                    End If
                    '********************** finish untuk yang debet

                    'Insert Untuk credit nya....
                    dt_bantu = New DataTable
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_DSCRP+")

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
                                        & SetInteger(ccre_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(par_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency") & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetSetring(par_oid) & ",  " _
                                        & SetSetring(par_trans_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("CYC-RCNT") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, par_date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     ccre_en_id.EditValue, master_new.ClsVar.ibase_cur_id, _
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
    End Function
#End Region

    Private Sub btImportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btImportExcel.Click
        Try
            Dim ds As New DataSet
            ds = master_new.excelconn.ImportExcel(AskOpenFile("Excel Files | *.xls"))

            Dim frm As New frmShowExcelData
            frm._ds = ds
            frm.Show()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
