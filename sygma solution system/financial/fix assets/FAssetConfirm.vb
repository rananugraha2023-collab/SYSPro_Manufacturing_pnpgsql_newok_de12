Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FAssetConfirm
    Dim ssql As String
    Dim _ptnr_oid As String
    Public dt_bantu As DataTable
    Dim ds_edit As DataSet
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAssetConfirm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        ass_en_id.Properties.DataSource = dt_bantu
        ass_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ass_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ass_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_pt_class("A','E")
        ass_class.Properties.DataSource = dt_bantu
        ass_class.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_class.Properties.ValueMember = dt_bantu.Columns("code_code").ToString
        ass_class.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ptnr_mstr())
        ass_ptnr_id.Properties.DataSource = dt_bantu
        ass_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        ass_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        ass_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0
    End Sub

    Private Sub sc_le_ptnr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ass_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = func_data.load_pt_mstr(ass_en_id.EditValue)
        ass_pt_id.Properties.DataSource = dt_bantu
        ass_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        ass_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        ass_pt_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_end_user(ass_en_id.EditValue)
        ass_emp_id.Properties.DataSource = dt_bantu
        ass_emp_id.Properties.DisplayMember = dt_bantu.Columns("xemp_name").ToString
        ass_emp_id.Properties.ValueMember = dt_bantu.Columns("xemp_id").ToString
        ass_emp_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "emp_dept"))
        ass_emp_dept.Properties.DataSource = dt_bantu
        ass_emp_dept.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_emp_dept.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_emp_dept.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "emp_region"))
        ass_emp_rg.Properties.DataSource = dt_bantu
        ass_emp_rg.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_emp_rg.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_emp_rg.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "ass_st_purc"))
        ass_st_purc.Properties.DataSource = dt_bantu
        ass_st_purc.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_st_purc.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_st_purc.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(ass_en_id.EditValue, "ass_lic_type"))
        ass_lic_type.Properties.DataSource = dt_bantu
        ass_lic_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ass_lic_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ass_lic_type.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "ass_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Line", "ass_line", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "group", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Asset Code", "ass_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Serial Number", "ass_sn", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ass_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Barcode", "ass_barcode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Class", "ass_class", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Status Purc", "status_purc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Licence Type", "licence", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Model", "ass_model", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "ass_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Assign", "ass_qty_assgn", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Del", "ass_qty_del", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Status", "its_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Service Date", "ass_service_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Garansi Date", "ass_gar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost", "ass_service_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Salvage Cost", "ass_salvage_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Basis Cost", "ass_basis_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresaisi Akumulasi", "ass_depr_acum", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost After Depresiasi", "cost_sisa", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Depresiasi Date", "ass_depr_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Dispose Cost", "ass_disp_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Dispose Date", "ass_disp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Confirm", "ass_confirm", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End User", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Department", "department", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Req No", "ass_ref_req", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PO No", "ass_ref_po", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Receipt No", "ass_ref_rcpt", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        ce_select.EditValue = False

        get_sequel = "SELECT  " _
                & "  ass_oid,False as ceklist, " _
                & "  ass_dom_id, " _
                & "  ass_en_id, en_code, en_desc, " _
                & "  ass_add_by, " _
                & "  ass_add_date, " _
                & "  ass_upd_by, " _
                & "  ass_upd_date, " _
                & "  ass_id, " _
                & "  ass_pt_id,pt_code,pt_desc1,pt_desc2, pt_pl_id, " _
                & "  ass_code, " _
                & "  ass_barcode, " _
                & "  ass_desc, " _
                & "  ass_class, " _
                & "  ass_ref_req, " _
                & "  ass_ref_po, " _
                & "  ass_ref_rcpt, " _
                & "  ass_ref_rcpt_oid, " _
                & "  ass_ref_inv, " _
                & "  ass_model, " _
                & "  ass_qty, " _
                & "  ass_qty_assgn, " _
                & "  ass_qty_del, " _
                & "  ass_sn, " _
                & "  ass_service_date, " _
                & "  ass_gar_date, " _
                & "  ass_line, " _
                & "  ass_manual, " _
                & "  ass_ptnr_id,ptnr_name, " _
                & "  ass_st_purc, st_purc.code_name as status_purc, " _
                & "  ass_lic_type,lic.code_name as licence, " _
                & "  ass_service_cost, ass_service_cost - ass_depr_acum as cost_sisa, " _
                & "  ass_emp_id,xemp_name, " _
                & "  ass_emp_dept,dept.code_name as department, " _
                & "  ass_emp_rg,rg.code_name as regional, " _
                & "  ass_confirm, " _
                & "  ass_its_id,its_desc, " _
                & "  ass_dt, " _
                & "  ass_salvage_cost, " _
                & "  ass_basis_cost, " _
                & "  ass_depr_acum, " _
                & "  ass_depr_date, " _
                & "  ass_disp_amount, " _
                & "  ass_disp_date,ass_remarks, " _
                & "  grp.code_name as group " _
                & "FROM  " _
                & "  public.ass_mstr " _
                & "  inner join its_mstr on its_id = ass_its_id " _
                & "  inner join pt_mstr on pt_id = ass_pt_id " _
                & "  inner join ptnr_mstr on ptnr_id = ass_ptnr_id " _
                & "  left outer join xemp_mstr on xemp_id = ass_emp_id " _
                & "  inner join en_mstr on en_id = ass_en_id " _
                & "  left outer join code_mstr st_purc on st_purc.code_id = ass_st_purc " _
                & "  left outer join code_mstr lic on lic.code_id = ass_lic_type " _
                & "  left outer join code_mstr dept on dept.code_id = ass_emp_dept " _
                & "  left outer join code_mstr rg on rg.code_id = ass_emp_rg " _
                & "  inner join code_mstr grp on grp.code_id = pt_group " _
                & "  where ass_confirm ~~* 'n' " _
                & " order by ass_code "
        Return get_sequel
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("This Form Can't Insert Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        Return before_save
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("This Form Can't Edit Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("This Form Can't To Delete Data,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Function
    End Function

    Private Sub ce_select_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ce_select.EditValueChanged
        For Each _dr As DataRow In ds.Tables(0).Rows
            If ce_select.Checked = True Then
                _dr("ceklist") = True
            Else
                _dr("ceklist") = False
            End If
        Next
    End Sub

    Private Function get_code(ByVal _code As String, ByVal _counter As Integer) As String
        get_code = ""
        Dim no_urut_format As String = ""

        If Len(_counter.ToString) = 1 Then
            no_urut_format = "000000" + _counter.ToString
        ElseIf Len(_counter.ToString) = 2 Then
            no_urut_format = "00000" + _counter.ToString
        ElseIf Len(_counter.ToString) = 3 Then
            no_urut_format = "0000" + _counter.ToString
        ElseIf Len(_counter.ToString) = 4 Then
            no_urut_format = "000" + _counter.ToString
        ElseIf Len(_counter.ToString) = 5 Then
            no_urut_format = "00" + _counter.ToString
        ElseIf Len(_counter.ToString) = 6 Then
            no_urut_format = "0" + _counter.ToString
        ElseIf Len(_counter.ToString) = 7 Then
            no_urut_format = _counter.ToString
        End If

        get_code = _code + no_urut_format

        Return get_code
    End Function

    Private Sub btn_confirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_confirm.Click
        If MessageBox.Show("Confirm Selected Data..?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Dim _status As Boolean = False
        For Each _dr As DataRow In ds.Tables(0).Rows
            If _dr("ceklist") = True Then
                _status = True
                Exit For
            End If
        Next

        If _status = False Then
            MessageBox.Show("Please Select Data First..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim ssqls As New ArrayList
        Dim _glt_code As String = ""
        Dim _glt_code_awal As String = func_coll.get_transaction_number("GA", le_entity.GetColumnValue("en_code"), "glt_det", "glt_code")
        Dim _code As String = _glt_code_awal.Substring(0, 8)
        Dim _no_urut As Integer = CInt(_glt_code_awal.Substring(8, 7)) '- 1

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each _dr As DataRow In ds.Tables(0).Rows
                            If _dr("ceklist") = True Then
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "UPDATE  " _
                                                        & "  public.ass_mstr   " _
                                                        & "SET  " _
                                                        & "  ass_confirm = " & SetSetring("Y") & "  " _
                                                        & "  " _
                                                        & "WHERE  " _
                                                        & "  ass_oid = " & SetSetring(_dr("ass_oid")) & "  " _
                                                        & ";"
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If UCase(_dr("ass_manual")) = "N" Then
                                    If func_coll.get_conf_file("automatic_jurnal_asset_confirm") = "1" Then
                                        _glt_code = get_code(_code, _no_urut)
                                        _no_urut = _no_urut + 1

                                        If insert_glt_det_asset_confirm(ssqls, objinsert, _dr, _glt_code) = False Then
                                            sqlTran.Rollback()
                                            Exit Sub
                                        End If
                                    End If
                                End If
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
                        MessageBox.Show("Confirm Succesfull...!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        help_load_data(True)
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

    Private Function insert_glt_det_asset_confirm(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_dr As DataRow, ByVal par_glt_code As String) As Boolean
        insert_glt_det_asset_confirm = True

        Dim _glt_code As String = par_glt_code 'func_coll.get_transaction_number("GA", par_dr("en_code"), "glt_det", "glt_code", func_coll.get_tanggal_sistem)

        Dim dt_bantu As DataTable


        With par_obj
            Try
                'Insert Untuk Yang Debet Dulu
                dt_bantu = New DataTable
                dt_bantu = (func_coll.get_prodline_account(par_dr("pt_pl_id"), "ASS_ASSET"))

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
                                    & SetInteger(par_dr("ass_en_id")) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & " current_date " & ",  " _
                                    & SetSetring("GA") & ",  " _
                                    & SetInteger(master_new.ClsVar.ibase_cur_id) & ",  " _
                                    & SetDbl(1) & ",  " _
                                    & SetInteger(0) & ",  " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                    & SetSetring("Asset Confirm") & ",  " _
                                    & SetDblDB(par_dr("ass_service_cost")) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetSetring(par_dr("ass_oid")) & ",  " _
                                    & SetSetring(par_dr("ass_code")) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & "current_timestamp" & ",  " _
                                    & SetSetring("AS-CNF") & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, func_coll.get_tanggal_sistem, _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")), _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")), _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")), _
                                                 par_dr("ass_en_id"), master_new.ClsVar.ibase_cur_id, _
                                                 1, par_dr("ass_service_cost"), "D") = False Then

                    Return False
                    Exit Function
                End If
                '********************** finish untuk yang debet

                'credit 
                dt_bantu = New DataTable
                dt_bantu = (func_coll.get_prodline_account(par_dr("pt_pl_id"), "PRC_PACC"))
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
                                    & SetSetring(Guid.NewGuid.ToString) & ", " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ", " _
                                    & SetInteger(par_dr("ass_en_id")) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ", " _
                                    & "current_timestamp" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & " current_date " & ",  " _
                                    & SetSetring("GA") & ",  " _
                                    & SetInteger(master_new.ClsVar.ibase_cur_id) & ", " _
                                    & SetDbl(1) & ",  " _
                                    & SetInteger(1) & ",  " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ", " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ", " _
                                    & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ", " _
                                    & SetSetringDB("Asset Confirm") & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetDblDB(par_dr("ass_service_cost")) & ", " _
                                    & SetSetring(par_dr("ass_oid")) & ", " _
                                    & SetSetring(par_dr("ass_code")) & ", " _
                                    & SetSetring("N") & ",  " _
                                    & "current_timestamp" & ", " _
                                    & SetSetring("AS-CNF") & " " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, func_coll.get_tanggal_sistem, _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")), _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")), _
                                                 SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")), _
                                                 par_dr("ass_en_id"), master_new.ClsVar.ibase_cur_id, _
                                                 1, par_dr("ass_service_cost"), "C") = False Then

                    Return False
                    Exit Function
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

    End Function
End Class
