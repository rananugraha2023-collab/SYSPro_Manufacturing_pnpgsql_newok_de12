Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports npgsql

Public Class frmShowExcelDataCrossSelling
    Public _dscs As DataSet
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Private Sub frmShowExcelData_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '_dscs.Tables(0).Columns.Add("qty_old", System.Type.GetType("System.Double"))
            'With _dscs.Tables(0)
            '    For i As Integer = 0 To _dscs.Tables(0).Rows.Count - 1
            '        .Rows(i).Item("qty_old") = get_qty_old(Transalte2ID("en_mstr", "en_id", "en_desc", .Rows(i).Item("entity")), _
            '                                               Transalte2ID("si_mstr", "si_id", "si_desc", _dscs.Tables(0).Rows(i).Item("Site")), _
            '                                               Transalte2ID("loc_mstr", "loc_id", "loc_desc", _dscs.Tables(0).Rows(i).Item("Location")), _
            '                                               Transalte2ID("pt_mstr", "pt_id", "pt_code", _dscs.Tables(0).Rows(i).Item("Part Number")))
            '    Next
            'End With
            '_dscs.Tables(0).AcceptChanges()

            gc_master.DataMember = "result"
            gc_master.DataSource = _dscs.Tables(0)

            gv_master.BestFitColumns()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Function get_qty_old(ByVal _en_id As String, ByVal _si_id As String, ByVal _loc_id As String, ByVal _pt_id As String) As Double
        get_qty_old = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(sum(invc_qty),0) as sum_qty from invc_mstr " _
                                         & " where invc_en_id = " + _en_id _
                                         & " and invc_si_id =  " + _si_id _
                                         & " and invc_loc_id =  " + _loc_id _
                                         & " and invc_pt_id =  " + _pt_id _
                                         & " and coalesce(invc_serial,'') = ''"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_qty_old = .DataReader("sum_qty")
                    End While
                End With
            End Using
        Catch ex As Exception
            Box(ex.Message, "Find QTY Old")
            Exit Function
        End Try

        Return get_qty_old
    End Function
    Private Function get_cost(ByVal _par_pt_id As String, ByVal _par_en_id As String, ByVal _par_si_id As String) As Double
        Dim ssql As String
        Try
            ssql = "SELECT  " _
           & "  coalesce(invct_cost,0) as invct_cost " _
           & "FROM  " _
           & " public.invct_table  " _
           & " where " _
           & " invct_pt_id=" & _par_pt_id _
                 & " and invct_si_id=" & _par_si_id

            Dim dt As New DataTable
            dt = GetTableData(ssql)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Pesan(Err)
            Return 0
        End Try
    End Function
    Private Sub btProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btProcess.Click
        Try
            If DevExpress.XtraEditors.XtraMessageBox.Show("Are you sure process this data?", "Confirm ...", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Dim _text As String = Me.Text
                Dim ssqls As New ArrayList

                Dim ssql As String
                ssql = "select * from invct_mstr where "
                Dim pt_temp As String = ""

                For i As Integer = 0 To _dscs.Tables(0).Rows.Count - 1
                    If SetString(_dscs.Tables(0).Rows(i).Item("Part Number")) <> "" Then
                        pt_temp = pt_temp & "'" & _dscs.Tables(0).Rows(i).Item("Part Number") & "',"
                    End If
                Next

                pt_temp = Microsoft.VisualBasic.Left(pt_temp, pt_temp.Length - 1)

                ssql = "SELECT  " _
                    & "  b.pt_code, " _
                    & "  b.pt_desc1,a.invct_cost " _
                    & "FROM " _
                    & "  public.invct_table a " _
                    & "  RIGHT OUTER JOIN public.pt_mstr b ON (a.invct_pt_id = b.pt_id) " _
                    & "WHERE " _
                    & "(a.invct_cost=0 or a.invct_cost is null) and  " _
                    & "  b.pt_id IN (select pt_id from pt_mstr where pt_code in (" _
                     & pt_temp & "))"

                Dim dt As New DataTable
                dt = GetTableData(ssql)

                pt_temp = ""
                For Each dr As DataRow In dt.Rows
                    pt_temp = pt_temp & dr(0) & ", " & dr(1) & ","
                Next
                'If pt_temp <> "" Then
                '    Box("Cost for partnumber " & pt_temp & " is empty")
                '    Clipboard.SetText(pt_temp)
                '    Exit Sub
                'End If

                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran
                            '.Command.CommandType = CommandType.Text

                            Dim jj As String = ""
                            Dim jk As String = ""
                            For i As Integer = 0 To _dscs.Tables(0).Rows.Count - 1
                                If jk <> _dscs.Tables(0).Rows(i).Item("Location") Then
                                    Try
                                        If Transalte2ID("loc_mstr", "loc_id", "loc_desc", _dscs.Tables(0).Rows(i).Item("Location")) Is Nothing Then
                                            Exit Sub
                                        End If
                                        jk = _dscs.Tables(0).Rows(i).Item("Location")
                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                        Exit Sub
                                    End Try
                                End If
                            Next

                            For i As Integer = 0 To _dscs.Tables(0).Rows.Count - 1
                                If jk <> _dscs.Tables(0).Rows(i).Item("Unit Measure") Then
                                    Try
                                        If Transalte2ID("code_mstr", "code_id", "code_name", _dscs.Tables(0).Rows(i).Item("Unit Measure")) Is Nothing Then
                                            Exit Sub
                                        End If
                                        ' jj = Transalte2ID("code_mstr", "code_id", "code_name", _dscs.Tables(0).Rows(i).Item("Unit Measure"))
                                        jk = _dscs.Tables(0).Rows(i).Item("Unit Measure")
                                    Catch ex As Exception
                                        MsgBox(ex.Message)
                                        Exit Sub
                                    End Try
                                End If
                            Next

                            For i As Integer = 0 To _dscs.Tables(0).Rows.Count - 1
                                LabelControl1.Text = i & " of " & _dscs.Tables(0).Rows.Count - 1 & " " & _dscs.Tables(0).Rows(i).Item("Part Number") & " " & _dscs.Tables(0).Rows(i).Item("Deskripsi1")
                                System.Windows.Forms.Application.DoEvents()

                                If insert(ssqls, objinsert, "R", Transalte2ID("en_mstr", "en_id", "en_desc", _dscs.Tables(0).Rows(i).Item("Entity")), _
                                          _dscs.Tables(0).Rows(i).Item("Date"), Transalte2ID("pt_mstr", "pt_id", "pt_code", _dscs.Tables(0).Rows(i).Item("Part Number")), _dscs.Tables(0).Rows(i).Item("Part Number"), _dscs.Tables(0).Rows(i).Item("Deskripsi1"), _
                                            Transalte2ID("si_mstr", "si_id", "si_desc", _dscs.Tables(0).Rows(i).Item("Site")), _
                                            Transalte2ID("loc_mstr", "loc_id", "loc_desc", _dscs.Tables(0).Rows(i).Item("Location")), _
                                           SetNumber(_dscs.Tables(0).Rows(i).Item("Qty Opname")), SetNumber(_dscs.Tables(0).Rows(i).Item("Qty On Hand")), _
                                            Transalte2ID("code_mstr", "code_id", "code_name", _dscs.Tables(0).Rows(i).Item("Unit Measure"))) = False Then
                                    sqlTran.Rollback()
                                    Box("Error")
                                    Exit Sub
                                End If
                            Next
                            LabelControl1.Text = "Execute Data"
                            System.Windows.Forms.Application.DoEvents()
                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                            sqlTran.Commit()

                            Box("Process complete")
                            btProcess.Enabled = False
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Public Function insert(ByVal _ssqls As ArrayList, ByVal par_obj As Object, ByVal _type As String, ByVal _en_id As String, ByVal _date As Date, _
                           ByVal _pt_id As String, ByVal _pt_code As String, ByVal _pt_desc As String, ByVal _si_ID As String, ByVal _loc_id As String, ByVal _qty_real As Double, _
                           ByVal _qty_old As Double, ByVal _um_id As String) As Boolean
        insert = True

        Dim _ccre_oid As Guid = Guid.NewGuid
        Dim ssql As String
        Dim _serial, _cost_methode As String
        Dim _tran_id As Integer
        Dim _cost, _cost_avg As Double
        Dim _qty As Double = 0

        If _type.ToUpper = "I" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-cnt")
        ElseIf _type.ToUpper = "R" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-rcnt")
        End If

        _cost_methode = func_coll.get_pt_cost_method(_pt_id)
        _cost = get_cost(_pt_id, _en_id, _si_ID)
        _serial = "''"


        'If _cost = 0 And _qty_real <> 0 Then
        '    MsgBox("Cost 0 for Part Number " & _pt_code)
        '    Return False
        'End If

        If _tran_id = -1 Then
            MessageBox.Show("Inventory Begining Balance In Transaction Master Doesn't Exist", "Error..", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

        Try
            _qty = _qty_real - _qty_old

            With par_obj

                ssql = "INSERT INTO  " _
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
                        & SetDate(_date) & ",  " _
                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                        & SetSetring(_type) & ",  " _
                        & SetInteger(_pt_id) & ",  " _
                        & SetInteger(_si_ID) & ",  " _
                        & SetInteger(_loc_id) & ",  " _
                        & SetSetring("") & ",  " _
                        & SetDbl(_qty) & ",  " _
                        & SetInteger(_um_id) & ",  " _
                        & SetDbl(1) & ",  " _
                        & SetDbl(_qty) & ",  " _
                        & SetDbl(_cost) & ",  " _
                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                        & SetDbl(_qty_old) & ",  " _
                        & SetInteger(_en_id) & "  " _
                        & ")"

                .Command.CommandType = CommandType.Text
                .Command.CommandText = ssql
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()
                _ssqls.Add(ssql)


                If _type.ToUpper = "R" Then
                    'Update Table Inventory Dan Cost Inventory Dan History Inventory
                    '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial


                    If _qty > 0 Then
                        '_en_id = ccre_en_id.EditValue
                        '_si_ID = ccre_si_id.EditValue
                        '_loc_id = ccre_loc_id.EditValue
                        '_pt_id = _pt_id_global


                        If func_coll.update_invc_mstr_plus(_ssqls, par_obj, _en_id, _si_ID, _loc_id, _pt_id, _serial, _qty) = False Then
                            'sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        'Update History Inventory                                    
                        _cost = _cost
                        _cost_avg = func_coll.get_cost_average("+", _en_id, _si_ID, _pt_id, _qty, _cost)
                        If func_coll.update_invh_mstr(_ssqls, par_obj, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "R", _si_ID, _loc_id, _pt_id, _qty, _cost, _cost_avg, "", _date) = False Then
                            'sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        ''2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        ''_cost_methode = func_coll.get_pt_cost_method(_pt_id)
                        ''_en_id = ccre_en_id.EditValue
                        ''_pt_id = _pt_id_global
                        ''_qty = ccre_qty_real.EditValue
                        ''_cost = ccre_cost.EditValue
                        'If _cost_methode = "F" Or _cost_methode = "L" Then
                        '    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '    Return False
                        '    'If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                        '    '    sqlTran.Rollback()
                        '    '    insert = False
                        '    '    Exit Function
                        '    'End If
                        'ElseIf _cost_methode = "A" Then
                        '    _cost_avg = func_coll.get_cost_average("+", _en_id, _si_ID, _pt_id, _qty_real, _cost)
                        '    If func_coll.update_item_cost_avg(_ssqls, par_obj, _si_ID, _pt_id, _cost_avg) = False Then
                        '        'sqlTran.Rollback()
                        '        insert = False
                        '        Exit Function
                        '    End If
                        'End If

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If insert_glt_det_inv_rcn_plus(_ssqls, par_obj, _ccre_oid.ToString, "", _date, _en_id, _pt_id, _qty, _cost, _pt_code, _pt_desc) = False Then
                                ' sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                    ElseIf _qty < 0 Then

                        _serial = "''"
                        '_qty = ccre_qty_real.EditValue * -1
                        If func_coll.update_invc_mstr_minus(_ssqls, par_obj, _en_id, _si_ID, _loc_id, _pt_id, _pt_code, _serial, _qty * -1) = False Then
                            'sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        'Update History Inventory                                    
                        '_cost = ccre_cost.EditValue
                        '_qty = ccre_qty_real.EditValue
                        _cost_avg = func_coll.get_cost_average("-", _en_id, _si_ID, _pt_id, _qty, _cost)
                        If func_coll.update_invh_mstr(_ssqls, par_obj, _tran_id, 1, _en_id, "", _
                                                      _ccre_oid.ToString, "Cycle Count Recount", "R", _si_ID, _loc_id, _pt_id, _
                                                      _qty, _cost, _cost_avg, "", _date) = False Then
                            'sqlTran.Rollback()
                            insert = False
                            Exit Function
                        End If

                        ''2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                        ''_cost_methode = func_coll.get_pt_cost_method(_pt_id)
                        ''_en_id = ccre_en_id.EditValue
                        ''_pt_id = _pt_id_global
                        ''_qty = ccre_qty_real.EditValue * -1
                        ''_cost = ccre_cost.EditValue
                        'If _cost_methode = "F" Or _cost_methode = "L" Then
                        '    MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
                        '    Return False
                        '    'If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                        '    '    sqlTran.Rollback()
                        '    '    insert = False
                        '    '    Exit Function
                        '    'End If
                        'ElseIf _cost_methode = "A" Then
                        '    _cost_avg = func_coll.get_cost_average("-", _en_id, _si_ID, _pt_id, _qty_real * -1, _cost)
                        '    If func_coll.update_item_cost_avg(_ssqls, par_obj, _si_ID, _pt_id, _cost_avg) = False Then
                        '        'sqlTran.Rollback()
                        '        insert = False
                        '        Exit Function
                        '    End If
                        'End If

                        'Insert ke table glt_det
                        'Di proses ini langsung ke prodline account tidak ke prodline location account...
                        'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                        If _create_jurnal = True Then
                            If insert_glt_det_inv_rcn_minus(_ssqls, par_obj, _ccre_oid.ToString, "", _date, _en_id, _pt_id, _qty * -1, _cost, _pt_code, _pt_desc) = False Then
                                'sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        End If
                    End If

                End If

                'If master_new.PGSqlConn.status_sync = True Then
                '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                '        .Command.CommandType = CommandType.Text
                '        .Command.CommandText = Data
                '        .Command.ExecuteNonQuery()
                '        .Command.Parameters.Clear()
                '    Next
                'End If

                'sqlTran.Commit()
                'after_success()
                'set_row(_ccre_oid.ToString, "ccre_oid")
                insert = True
                'Catch ex As nPgSqlException
                '    sqlTran.Rollback()
                '    MessageBox.Show(ex.Message)
                'End Try
                'End With
                'End Using
            End With
        Catch ex As Exception
            'row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
    Private Function insert_glt_det_inv_rcn_minus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_oid As String, ByVal par_trans_code As String, _
                                ByVal par_date As Date, ByVal par_en_id As String, ByVal par_pt_id As String, ByVal par_qty As Double, ByVal par_cost As Double, _
                                ByVal par_pt_code As String, ByVal par_pt_desc As String) As Boolean

        insert_glt_det_inv_rcn_minus = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number("IC", Transalte2ID("en_mstr", "en_code", "en_id", par_en_id), "glt_det", "glt_code")
        'Return False
        'Exit Function
        With par_obj
            Try
                If par_qty > 0 Then
                    'Debet nya dulu
                    dt_bantu = New DataTable
                    _pl_id = func_coll.get_prodline(par_pt_id)

                    _cost = par_qty * par_cost

                    'Insert Untuk debet nya....
                    dt_bantu = New DataTable
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_dscsCRP-")

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
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency " & par_pt_code & " " & Microsoft.VisualBasic.Left(par_pt_desc, 38)) & ",  " _
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
                                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                     1, _cost, "D") = False Then

                        Return False
                        Exit Function
                    End If

                    dt_bantu = New DataTable
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
                                        & SetInteger(par_en_id) & ",  " _
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
                                        & SetSetring("Inventory Discrepency " & par_pt_code & " " & Microsoft.VisualBasic.Left(par_pt_desc, 38)) & ",  " _
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
                                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
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
    Private Function insert_glt_det_inv_rcn_plus(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_oid As String, _
                                                 ByVal par_trans_code As String, ByVal par_date As String, _
                                                 ByVal par_en_id As String, ByVal par_pt_id As String, ByVal par_qty As Double, ByVal par_cost As Double, _
                                                    ByVal par_pt_code As String, ByVal par_pt_desc As String) As Boolean
        insert_glt_det_inv_rcn_plus = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim _cost As Double
        Dim dt_bantu As DataTable
        _glt_code = func_coll.get_transaction_number("IC", Transalte2ID("en_mstr", "en_code", "en_id", par_en_id), "glt_det", "glt_code")

        With par_obj
            Try
                If par_qty > 0 Then
                    'Debet nya dulu
                    dt_bantu = New DataTable
                    _pl_id = func_coll.get_prodline(par_pt_id)
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_ACCT")

                    _cost = par_qty * par_cost

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
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency " & par_pt_code & " " & Microsoft.VisualBasic.Left(par_pt_desc, 38)) & ",  " _
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
                                                     par_en_id, master_new.ClsVar.ibase_cur_id, _
                                                     1, _cost, "D") = False Then

                        Return False
                        Exit Function
                    End If
                    '********************** finish untuk yang debet

                    'Insert Untuk credit nya....
                    dt_bantu = New DataTable
                    dt_bantu = func_coll.get_prodline_account(_pl_id, "INV_dscsCRP+")

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
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetInteger(i) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("Inventory Discrepency " & par_pt_code & " " & Microsoft.VisualBasic.Left(par_pt_desc, 38)) & ",  " _
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
    End Function
    Private Function Transalte2ID(ByVal _table As String, ByVal _field_out As String, ByVal _field_in As String, ByVal _value As String) As String
        Try
            Dim _hasil As String = ""
            Dim ssql As String
            ssql = "select " & _field_out & " from " & _table & " where " & _field_in & "=" & SetSetring(_value) & ""
            Dim dt As New DataTable
            dt = GetTableData(ssql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    _hasil = dt.Rows(0).Item(0).ToString
                Else
                    ' _hasil = dt.Rows(0).Item(0).ToString
                    Box("Translate to ID value " & _value & " " & ssql & " found multiple data")
                    Return Nothing
                    Exit Function
                End If
            Else
                Box("Translate to ID value " & _value & " not found")
            End If
            Return _hasil
        Catch ex As Exception
            Pesan(Err)
            Return Nothing
        End Try
    End Function
End Class