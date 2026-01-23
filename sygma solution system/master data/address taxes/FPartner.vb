Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartner
    Dim ssql As String
    Dim _ptnr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        sc_le_ptnr_en_id.Properties.DataSource = dt_bantu
        sc_le_ptnr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sc_le_ptnr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sc_le_ptnr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ptnr_cu_id.Properties.DataSource = dt_bantu
        ptnr_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ptnr_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ptnr_cu_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        ptnr_ac_ar_id.Properties.DataSource = dt_bantu
        ptnr_ac_ar_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ptnr_ac_ar_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ptnr_ac_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        ptnr_ac_ap_id.Properties.DataSource = dt_bantu
        ptnr_ac_ap_id.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        ptnr_ac_ap_id.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        ptnr_ac_ap_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Name", "ptnr_name_alt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "URL", "ptnr_url", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Email", "ptnr_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptnr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Customer", "ptnr_is_cust", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Vendor", "ptnr_is_vend", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Member", "ptnr_is_member", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Employee", "ptnr_is_emp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Writer", "ptnr_is_writer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Code", "ac_code_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Name", "ac_name_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Sub Account", "sb_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Cost Center", "cc_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Code", "ac_code_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Name", "ac_name_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Sub Account", "sb_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Cost Center", "cc_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Limit Credit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Prepayment Balance", "ptnr_prepaid_balance", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Is Active", "ptnr_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptnr_oid, " _
                & "  a.ptnr_dom_id, " _
                & "  a.ptnr_en_id, " _
                & "  a.ptnr_transaction_code_id, " _
                & "  code_code, " _
                & "  code_name, " _
                & "  a.ptnr_add_by, " _
                & "  a.ptnr_add_date, " _
                & "  a.ptnr_upd_by, " _
                & "  a.ptnr_upd_date, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name, " _
                & "  a.ptnr_name_alt, " _
                & "  a.ptnr_ptnrg_id, " _
                & "  ptnrg_name, " _
                & "  a.ptnr_url, " _
                & "  a.ptnr_email, " _
                & "  a.ptnr_remarks, " _
                & "  a.ptnr_parent, " _
                & "  a.ptnr_is_cust, " _
                & "  a.ptnr_is_vend, " _
                & "  a.ptnr_is_member, " _
                & "  a.ptnr_is_emp, " _
                & "  a.ptnr_is_writer, " _
                & "  a.ptnr_npwp, " _
                & "  a.ptnr_nppkp, " _
                & "  a.ptnr_active, " _
                & "  a.ptnr_dt, " _
                & "  c.en_desc, " _
                & "  b.dom_desc, " _
                & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                & "  sb_mstr_ar.sb_desc as sb_desc_ar, " _
                & "  cc_mstr_ar.cc_desc as cc_desc_ar, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  a.ptnr_ac_ar_id, " _
                & "  a.ptnr_sb_ar_id, " _
                & "  a.ptnr_cc_ar_id, " _
                & "  a.ptnr_ac_ap_id, " _
                & "  a.ptnr_sb_ap_id, " _
                & "  a.ptnr_cc_ap_id, " _
                & "  a.ptnr_cu_id, " _
                & "  cu_name, " _
                & "  ptnr_prepaid_balance, " _
                & "  a.ptnr_limit_credit " _
                & "FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.ptnr_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  INNER JOIN public.ptnrg_grp ON ptnrg_id = ptnr_ptnrg_id " _
                & "  INNER JOIN public.cu_mstr ON cu_id = ptnr_cu_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ap ON ptnr_ac_ap_id = ac_mstr_ap.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ap ON ptnr_sb_ap_id = sb_mstr_ap.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ap ON ptnr_cc_ap_id = cc_mstr_ap.cc_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ar ON ptnr_ac_ar_id = ac_mstr_ar.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ar ON ptnr_sb_ar_id = sb_mstr_ar.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ar ON ptnr_cc_ar_id = cc_mstr_ar.cc_id " _
                & "  left outer join public.code_mstr ON code_id = ptnr_transaction_code_id " _
                & " where ptnr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " order by ptnr_name"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_ptnr_en_id.ItemIndex = 0
        ptnr_transaction_code_id.ItemIndex = 0
        sc_te_ptnr_name.Text = ""
        ptnr_name_alt.Text = ""
        sc_te_ptnr_url.Text = ""
        ptnr_npwp.Text = ""
        ptnr_nppkp.Text = ""
        sc_te_ptnr_remarks.Text = ""
        sc_le_ptnr_ptnrg_id.ItemIndex = 0
        sc_ce_ptnr_active.EditValue = True
        sc_ce_ptnr_is_cust.EditValue = False
        sc_ce_ptnr_is_vend.EditValue = False
        ptnr_is_writer.EditValue = False
        ptnr_is_member.EditValue = False
        ptnr_is_emp.EditValue = False
        ptnr_cu_id.ItemIndex = 0
        ptnr_ac_ar_id.ItemIndex = 0
        ptnr_ac_ap_id.ItemIndex = 0
        ptnr_email.Text = ""
        sc_le_ptnr_en_id.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid

        Dim _ptnr_code As String = ""
        Dim _ptnr_id As Integer
        Dim ssqls As New ArrayList

        _ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sc_le_ptnr_en_id.EditValue.ToString))

        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        If Len(_ptnr_code) = 2 Then
            _ptnr_code = _ptnr_code + "00"
        End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(2, Len(_ptnr_id.ToString) - 2)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 6 Then
            _ptnr_id_s = "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 7 Then
            _ptnr_id_s = _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

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
                                            & "  public.ptnr_mstr " _
                                            & "( " _
                                            & "  ptnr_oid, " _
                                            & "  ptnr_dom_id, " _
                                            & "  ptnr_en_id, " _
                                            & "  ptnr_add_by, " _
                                            & "  ptnr_add_date, " _
                                            & "  ptnr_id, " _
                                            & "  ptnr_code, " _
                                            & "  ptnr_name, " _
                                            & "  ptnr_name_alt, " _
                                            & "  ptnr_ptnrg_id, " _
                                            & "  ptnr_url, " _
                                            & "  ptnr_email, " _
                                            & "  ptnr_npwp, " _
                                            & "  ptnr_nppkp, " _
                                            & "  ptnr_remarks, " _
                                            & "  ptnr_is_cust, " _
                                            & "  ptnr_is_vend, " _
                                            & "  ptnr_is_member, " _
                                            & "  ptnr_is_emp, " _
                                            & "  ptnr_is_writer, " _
                                            & "  ptnr_ac_ar_id, " _
                                            & "  ptnr_sb_ar_id, " _
                                            & "  ptnr_cc_ar_id, " _
                                            & "  ptnr_ac_ap_id, " _
                                            & "  ptnr_sb_ap_id, " _
                                            & "  ptnr_cc_ap_id, " _
                                            & "  ptnr_cu_id, " _
                                            & "  ptnr_limit_credit, " _
                                            & "  ptnr_active, " _
                                            & "  ptnr_transaction_code_id, " _
                                            & "  ptnr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnr_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(sc_le_ptnr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & _ptnr_id & ",  " _
                                            & SetSetring(_ptnr_code) & ",  " _
                                            & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                            & SetSetring(ptnr_name_alt.Text) & ",  " _
                                            & sc_le_ptnr_ptnrg_id.EditValue & ",  " _
                                            & SetSetring(sc_te_ptnr_url.Text) & ",  " _
                                            & SetSetring(ptnr_email.Text) & ",  " _
                                            & SetSetring(ptnr_npwp.Text) & ",  " _
                                            & SetSetring(ptnr_nppkp.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cu_id.EditValue) & ",  " _
                                            & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                            & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                            & ")"
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
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
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
        If MyBase.edit_data = True Then
            sc_le_ptnr_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnr_oid = .Item("ptnr_oid").ToString
                sc_le_ptnr_en_id.EditValue = .Item("ptnr_en_id")
                sc_le_ptnr_ptnrg_id.EditValue = .Item("ptnr_ptnrg_id")
                sc_te_ptnr_name.Text = SetString(.Item("ptnr_name"))
                ptnr_name_alt.Text = SetString(.Item("ptnr_name_alt"))
                sc_te_ptnr_url.Text = SetString(.Item("ptnr_url"))
                ptnr_email.Text = SetString(.Item("ptnr_email"))
                ptnr_npwp.Text = SetString(.Item("ptnr_npwp"))
                ptnr_nppkp.Text = SetString(.Item("ptnr_nppkp"))
                sc_te_ptnr_remarks.Text = SetString(.Item("ptnr_remarks"))
                sc_ce_ptnr_active.EditValue = SetBitYNB(.Item("ptnr_active"))
                sc_ce_ptnr_is_cust.EditValue = SetBitYNB(.Item("ptnr_is_cust"))
                sc_ce_ptnr_is_vend.EditValue = SetBitYNB(.Item("ptnr_is_vend"))
                ptnr_is_member.EditValue = SetBitYNB(.Item("ptnr_is_member"))
                ptnr_is_emp.EditValue = SetBitYNB(.Item("ptnr_is_emp"))
                ptnr_is_writer.EditValue = SetBitYNB(.Item("ptnr_is_writer"))
                ptnr_ac_ar_id.EditValue = .Item("ptnr_ac_ar_id")
                ptnr_sb_ar_id.EditValue = .Item("ptnr_sb_ar_id")
                ptnr_cc_ar_id.EditValue = .Item("ptnr_cc_ar_id")
                ptnr_ac_ap_id.EditValue = .Item("ptnr_ac_ap_id")
                ptnr_sb_ap_id.EditValue = .Item("ptnr_sb_ap_id")
                ptnr_cc_ap_id.EditValue = .Item("ptnr_cc_ap_id")
                ptnr_cu_id.EditValue = .Item("ptnr_cu_id")
                ptnr_limit_credit.EditValue = .Item("ptnr_limit_credit")

                If IsDBNull(.Item("ptnr_transaction_code_id")) = True Then
                    ptnr_transaction_code_id.ItemIndex = 0
                Else
                    ptnr_transaction_code_id.EditValue = .Item("ptnr_transaction_code_id")
                End If
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim _ptnr_code As String = ""
        Dim _ptnr_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id")
        Dim ssqls As New ArrayList

        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        If Len(_ptnr_code) = 2 Then
            _ptnr_code = _ptnr_code + "00"
        End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(2, Len(_ptnr_id.ToString) - 2)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 6 Then
            _ptnr_id_s = "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 7 Then
            _ptnr_id_s = _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.ptnr_mstr  " _
                                                & "SET  " _
                                                & "  ptnr_en_id = " & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                & "  ptnr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ptnr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & "  ptnr_code = " & SetSetring(_ptnr_code) & ",  " _
                                                & "  ptnr_name = " & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                                & "  ptnr_name_alt = " & SetSetring(ptnr_name_alt.Text) & ",  " _
                                                & "  ptnr_ptnrg_id = " & sc_le_ptnr_ptnrg_id.EditValue & ",  " _
                                                & "  ptnr_url = " & SetSetring(sc_te_ptnr_url.Text) & ",  " _
                                                & "  ptnr_email = " & SetSetring(ptnr_email.Text) & ",  " _
                                                & "  ptnr_npwp = " & SetSetring(ptnr_npwp.Text) & ",  " _
                                                & "  ptnr_nppkp = " & SetSetring(ptnr_nppkp.Text) & ",  " _
                                                & "  ptnr_transaction_code_id = " & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                                & "  ptnr_remarks = " & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                                & "  ptnr_is_cust = " & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                                & "  ptnr_is_vend = " & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                                & "  ptnr_is_member = " & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                                & "  ptnr_is_emp = " & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                                & "  ptnr_is_writer = " & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                                & "  ptnr_active = " & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                                & "  ptnr_ac_ar_id = " & ptnr_ac_ar_id.EditValue & ",  " _
                                                & "  ptnr_sb_ar_id = " & ptnr_sb_ar_id.EditValue & ",  " _
                                                & "  ptnr_cc_ar_id = " & ptnr_cc_ar_id.EditValue & ",  " _
                                                & "  ptnr_ac_ap_id = " & ptnr_ac_ap_id.EditValue & ",  " _
                                                & "  ptnr_sb_ap_id = " & ptnr_sb_ap_id.EditValue & ",  " _
                                                & "  ptnr_cc_ap_id = " & ptnr_cc_ap_id.EditValue & ",  " _
                                                & "  ptnr_cu_id = " & ptnr_cu_id.EditValue & ",  " _
                                                & "  ptnr_limit_credit = " & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                                & "  ptnr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ptnr_oid = '" & _ptnr_oid & "' "
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
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
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

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrg_grp", sc_le_ptnr_en_id.EditValue))
        sc_le_ptnr_ptnrg_id.Properties.DataSource = dt_bantu
        sc_le_ptnr_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        sc_le_ptnr_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        sc_le_ptnr_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ap_id.Properties.DataSource = dt_bantu
        ptnr_sb_ap_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ap_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ar_id.Properties.DataSource = dt_bantu
        ptnr_sb_ar_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ar_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ap_id.Properties.DataSource = dt_bantu
        ptnr_cc_ap_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ap_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ar_id.Properties.DataSource = dt_bantu
        ptnr_cc_ar_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ar_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sc_le_ptnr_en_id.EditValue, "fakturpajak_transactioncode"))
        ptnr_transaction_code_id.Properties.DataSource = dt_bantu
        ptnr_transaction_code_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnr_transaction_code_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnr_transaction_code_id.ItemIndex = 0
    End Sub

    Private Sub sc_le_ptnr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_ptnr_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

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

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ptnr_mstr where ptnr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString + "'"
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
End Class
