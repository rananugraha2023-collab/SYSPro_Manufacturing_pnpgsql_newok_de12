Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FWfDisbursementForm
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public __woci_wo_id As String

    Public __ptnr_id As String = ""
    Dim ds_check As New DataSet
    Public ds_edit As DataSet
    Dim _conf_budget As String

    Public _disburs_invest_code As String
    Public _disburs_investd_periode As String

    Private Sub FWfDisbursementForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(disburs_en_id, "en_mstr")
        'init_le(disburs_cu_id, "cu_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr())
        disburs_per_id.Properties.DataSource = dt_bantu
        disburs_per_id.Properties.DisplayMember = dt_bantu.Columns("periode_code").ToString
        disburs_per_id.Properties.ValueMember = dt_bantu.Columns("periode_code").ToString
        disburs_per_id.ItemIndex = 0

        init_le(disburs_type_id, "disburs_type")

    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "disburs_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Disburement Code", "disburs_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Periode", "disburs_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "disburs_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Realization", "disburs_Due_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "disburs_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Refference", "disburs_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "disburs_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Poinr", "disburs_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Reverse", "disburs_amount_reverse", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Reff", "disburs_reff_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "disburs_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "disburs_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "disburs_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "disburs_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "disbursd_oid", False)
        add_column(gv_detail, "disbursd_disburs_oid", False)
        'add_column(gv_detail, "disbursd_ac_id", False)
        add_column(gv_detail, "Partner Code", "disbursd_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Total Point", "disbursd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "Disb Point", "disburd_disb_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "Amount", "disbursd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "Remain Point", "disburd_remaind_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "Remarks", "disbursd_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "disbursd_cc_id", False)
        add_column(gv_detail, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "disbursd_oid", False)
        add_column(gv_edit, "disbursd_disburs_oid", False)
        'add_column(gv_detail, "disbursd_ac_id", False)
        add_column(gv_edit, "Partner Code", "disbursd_ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Total Point", "disbursd_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Disb Point", "disburd_disb_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Amount", "disbursd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Remain Point", "disburd_remaind_point", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Remarks", "disbursd_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "disbursd_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        'add_column(gv_edit, "disbursd_oid", False)
        'add_column(gv_edit, "disbursd_disburs_oid", False)
        'add_column(gv_edit, "disbursd_ac_id", False)
        'add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_edit(gv_edit, "Amount", "disbursd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        'add_column_edit(gv_edit, "Remarks", "disbursd_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_edit, "disbursd_cc_id", False)
        'add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  public.disburs_out.disburs_oid, " _
                & "  public.disburs_out.disburs_dom_id, " _
                & "  public.disburs_out.disburs_en_id, " _
                & "  public.disburs_out.disburs_add_by, " _
                & "  public.disburs_out.disburs_add_date, " _
                & "  public.disburs_out.disburs_upd_by, " _
                & "  public.disburs_out.disburs_upd_date, " _
                & "  public.disburs_out.disburs_code, " _
                & "  public.disburs_out.disburs_periode, " _
                & "  public.disburs_out.disburs_date, " _
                & "  public.disburs_out.disburs_duedate, " _
                & "  public.disburs_out.disburs_remarks, " _
                & "  public.disburs_out.disburs_type_id, " _
                & "  public.disburs_out.disburs_reff, " _
                & "  public.disburs_out.disburs_point, " _
                & "  public.psperiode_mstr.periode_code " _
                & "FROM " _
                & "  public.disburs_out " _
                & "  LEFT OUTER JOIN public.psperiode_mstr ON (public.disburs_out.disburs_periode = public.psperiode_mstr.periode_code)" _
                & "  Where public.disburs_out.disburs_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " and disburs_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY disburs_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        disburs_en_id.EditValue = ""
        disburs_code.EditValue = ""
        disburs_code.Enabled = False

        disburs_per_id.EditValue = ""

        disburs_date.DateTime = CekTanggal()
        disburs_duedate.DateTime = CekTanggal()

        disburs_point.EditValue = ""

        disburs_reff.EditValue = ""
        disburs_point.EditValue = 0.0
        disburs_remarks.EditValue = ""
        disburs_en_id.Focus()
        disburs_type_id.EditValue = ""
        '_disburs_invest_code = ""
        _disburs_investd_periode = 0

        Try
            tcg_header.SelectedTabPageIndex = 0
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
                        & "  a.disbursd_oid, " _
                        & "  a.disbursd_disburs_oid, " _
                        & "  a.disbursd_ac_id, " _
                        & "  b.ac_code, " _
                        & "  b.ac_name, " _
                        & "  a.disbursd_amount, " _
                        & "  a.disbursd_remarks,a.disbursd_cc_id, c.cc_desc " _
                        & "FROM " _
                        & "  public.disbursd_detail a " _
                        & "  INNER JOIN public.ac_mstr b ON (a.disbursd_ac_id = b.ac_id) " _
                        & "  INNER JOIN public.cc_mstr c ON (a.disbursd_cc_id = c.cc_id) " _
                        & " Where  a.disbursd_disburs_oid IS NULL "

                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Public Overrides Function insert() As Boolean
        '_conf_budget = func_coll.get_conf_file("budget_base")
        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList


        disburs_code.Text = GetNewNumberYM("disburs_out", "disburs_code", 5, "DSB" & disburs_en_id.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Dim _remains As Double = 0
        Dim _realization As Double = 0

        If disburs_type_id.EditValue = "TEMP" Then
            _remains = disburs_point.EditValue
            _realization = 0
        ElseIf disburs_type_id.EditValue = "REAL" Then
            _remains = 0
            _realization = 0
        Else
            _remains = 0
            _realization = 0
        End If



        Try
            'ssql = "INSERT INTO  " _
            '    & "  public.disburs_out " _
            '    & "( " _
            '    & "  disburs_oid, " _
            '    & "  disburs_dom_id, " _
            '    & "  disburs_en_id, " _
            '    & "  disburs_add_by, " _
            '    & "  disburs_add_date, " _
            '    & "  disburs_bk_id, " _
            '    & "  disburs_ptnr_id, " _
            '    & "  disburs_code, " _
            '    & "  disburs_date, " _
            '    & "  disburs_remarks,disburs_invest_code,disburs_investd_oid,disburs_investd_periode, " _
            '    & "  disburs_reff,disburs_type,disburs_reff_code,disburs_reff_oid,disburs_close,disburs_close_temp, " _
            '    & "  disburs_amount, " _
            '    & "  disburs_cu_id,disburs_amount_remains,disburs_amount_realization,disburs_amount_reverse, " _
            '    & "  disburs_exc_rate,disburs_is_reverse " _
            '    & ")  " _
            '    & "VALUES ( " _
            '    & SetSetring(_mstr_oid) & ",  " _
            '    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
            '    & SetInteger(disburs_en_id.EditValue) & ",  " _
            '    & SetSetring(master_new.ClsVar.sNama) & ",  " _
            '    & SetDateNTime(CekTanggal) & ",  " _
            '    & SetInteger(__ptnr_id) & ",  " _
            '    & SetSetring(disburs_code.Text) & ",  " _
            '    & SetDateNTime00(disburs_date.DateTime) & ",  " _
            '    & SetSetring(disburs_remarks.Text) & ",  " _
            '    & SetSetring(_disburs_invest_code) & ",  " _
            '    & SetSetring(disburs_reff.Text) & ",  " _
            '    & SetSetring(disburs_type.EditValue) & ",  " _
            '    & SetSetring("N") & ",  " _
            '    & SetBitYN(disburs_close_temp.Checked) & ",  " _
            '    & SetDec(disburs_amount.EditValue) & ",  " _
            '    & SetInteger(disburs_cu_id.EditValue) & ",  " _
            '    & SetDec(_remains) & ",  " _
            '    & SetDec(_realization) & ",0,  " _
            '    & SetDec(disburs_exc_rate.EditValue) & ",  " _
            '    & SetBitYN(disburs_is_reverse.EditValue) _
            '    & ")"

            ssqls.Add(ssql)

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.disbursd_detail " _
                        & "( " _
                        & "  disbursd_oid, " _
                        & "  disbursd_disburs_oid, " _
                        & "  disbursd_ac_id, " _
                        & "  disbursd_amount, " _
                        & "  disbursd_remarks, " _
                        & "  disbursd_seq,disbursd_cc_id " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("disbursd_ac_id")) & ",  " _
                        & SetDec(.Item("disbursd_amount")) & ",  " _
                        & SetSetring(.Item("disbursd_remarks")) & ",  " _
                        & SetInteger(i) & ",  " _
                        & SetInteger(.Item("disbursd_cc_id")) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next

            'If disburs_type_id.EditValue = "REAL" Then
            '    ssql = "update  " _
            '          & "  public.disburs_out " _
            '          & "set disburs_close=" & SetBitYN(disburs_close_temp.Checked) & ", disburs_amount_realization=coalesce(disburs_amount_realization,0) +  " & SetDec(disburs_amount.EditValue) _
            '          & ", disburs_amount_remains=coalesce(disburs_amount_remains,0) -  " & SetDec(disburs_amount.EditValue) _
            '          & " where disburs_oid=" & SetSetring(disburs_reff_oid.Tag)

            '    ssqls.Add(ssql)
            'End If

            'update rekonsiliasi kas masuk
            'If update_rec(ssqls, disburs_en_id.EditValue, disburs_bk_id.EditValue, disburs_cu_id.EditValue, _
            '             disburs_exc_rate.EditValue, disburs_amount.EditValue * -1, disburs_date.DateTime, disburs_code.Text, _
            '             disburs_remarks.Text, "CASH OUT") = False Then
            '    Return False
            '    Exit Function
            'End If

            'If disburs_investd_oid.Tag <> "" Then

            '    ssql = "update invest_mstr set " _
            '    & " invest_payment_out = coalesce(invest_payment_out,0) + " & SetDec(disburs_point.EditValue) _
            '    & " where invest_code=" & SetSetring(_disburs_invest_code)

            '    ssqls.Add(ssql)

            '    ssql = "update investd_detail set investd_payment_date_realization = " & SetDateNTime00(disburs_date.DateTime) _
            '       & ",investd_paid=coalesce(investd_paid,0) + " & SetDec(disburs_amount.EditValue) _
            '       & "  where investd_oid=" & SetSetring(disburs_investd_oid.Tag)

            '    ssqls.Add(ssql)

            'End If


            'jurnal
            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

            'If _create_jurnal = True Then

            '    'insert dulu debetnya
            '    Dim _ac_id_kredit As String
            '    _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", disburs_bk_id.EditValue)

            '    Dim _glt_code As String
            '_glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CO" _
            '       & disburs_en_id.GetColumnValue("en_code") & (CekTanggal().ToString("yyMM")))




            '_glt_code = func_coll.get_transaction_number("CO", disburs_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

            'Box("GL Code")

            'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            '    With ds_edit.Tables(0).Rows(i)
            '        'Box("Insert GL")
            '        If insert_gl(ssqls, _glt_code, .Item("disbursd_ac_id"), .Item("disbursd_amount"), 0, _
            '            master_new.ClsVar.sdom_id, disburs_en_id.EditValue, 0, SetNumber(.Item("disbursd_cc_id")), _
            '            disburs_date.DateTime, disburs_cu_id.EditValue, disburs_exc_rate.EditValue, _
            '            "CO", i + 2, "Cash Out " & disburs_reff.Text & " " & .Item("disbursd_remarks"), "", disburs_code.EditValue, "CASH OUT", SetBitYN(disburs_is_reverse.EditValue)) = False Then
            '            Return False
            '            Exit Function
            '        End If

            '        'Box("Update GLBAL")
            '        If update_glbal(ssqls, .Item("disbursd_ac_id"), .Item("disbursd_amount"), _
            '                        master_new.ClsVar.sdom_id, disburs_en_id.EditValue, 0, 0, disburs_date.DateTime, _
            '                        disburs_cu_id.EditValue, disburs_exc_rate.EditValue, "D") = False Then
            '            Return False
            '            Exit Function
            '        End If

            '        'Box("Update Budget")
            '        If _conf_budget = "1" Then
            '            If update_budget(ssqls, .Item("disbursd_ac_id"), .Item("disbursd_amount"), disburs_en_id.EditValue, disburs_date.EditValue, .Item("disbursd_cc_id")) = False Then
            '                Return False
            '                Exit Function
            '            End If
            '        End If

            '    End With

            'Next

            'insert GL
            'Box("Insert GL2")
            '    If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, disburs_amount.EditValue, _
            '                 master_new.ClsVar.sdom_id, disburs_en_id.EditValue, 0, 0, _
            '                 disburs_date.DateTime, disburs_cu_id.EditValue, disburs_exc_rate.EditValue, _
            '                 "CO", 1, "Cash Out " & disburs_reff.Text & " " & disburs_remarks.EditValue, "", disburs_code.EditValue, "CASH OUT", SetBitYN(disburs_is_reverse.EditValue)) = False Then
            '        Return False
            '        Exit Function
            '    End If

            '    'Box("Update GLBAL2")
            '    If update_glbal(ssqls, _ac_id_kredit, disburs_amount.EditValue, master_new.ClsVar.sdom_id, _
            '                    disburs_en_id.EditValue, 0, 0, disburs_date.DateTime, disburs_cu_id.EditValue, _
            '                    disburs_exc_rate.EditValue, "C") = False Then
            '        Return False
            '        Exit Function
            '    End If

            'End If


            '.Command.CommandType = CommandType.Text
            '.Command.CommandText = insert_log("Insert Debit Credit Memo " & _ar_code)
            ssqls.Add(insert_log("Insert Cash Out " & disburs_code.Text))
            '.Command.ExecuteNonQuery()
            '.Command.Parameters.Clear()


            'Dim _data As String = ""
            'For Each Str As String In ssqls
            '    _data += Str & vbNewLine

            'Next

            'For Each Str As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
            '    _data += Str & vbNewLine
            'Next

            'Box("Insert " & _data)

            If master_new.PGSqlConn.status_sync = True Then
                If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then
                    Return False
                    Exit Function
                End If
                ssqls.Clear()
            End If

            after_success()
            set_row(_mstr_oid, "disburs_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Function update_budget(ByVal par_ssqls As ArrayList, ByVal par_ac_id As String, ByVal par_amount As Double, _
                                  ByVal par_en_id As Integer, ByVal par_date As Date, ByVal par_cc_id As Integer) As Boolean
        update_budget = True

        Dim _bdgt_oid As String = ""
        _bdgt_oid = func_coll.get_bdgt_oid(par_cc_id)

        If func_coll.get_budget(_bdgt_oid, par_ac_id, par_date) = False Then
            MessageBox.Show("Budget Tidak Tersedia..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            update_budget = False
            Exit Function
        End If

        Dim _sisa_budget As Double = 0

        ssql = "select (bdgtd_budget - ((coalesce(bdgtd_alokasi,0)) + (coalesce(bdgtd_realisasi,0)))) as sisa_budget " _
             & " from bdgtd_det " _
             & " where bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
             & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
             & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
             & " where bdgtp_start_date <= " + SetDate(par_date) _
             & " and bdgtp_end_date >= " + SetDate(par_date) _
             & " ) "

        _sisa_budget = GetRowInfo(ssql)(0)


        '=========================================================
        Dim _acc_cek_budget As String
        _acc_cek_budget = func_coll.acc_cek_budget(par_ac_id)
        If _acc_cek_budget = "Y" Then
            If _sisa_budget < par_amount Then
                MessageBox.Show("Biaya Lebih Besar Dari Budget Yang Tersedia,,! Silahkan Lakukan Cross Budget Terlebih Dahulu,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Information)
                update_budget = False
                Exit Function
            End If
        Else
            Exit Function
        End If
        '=========================================================
        Try

            'Update bdgtd_det Set Alokasi nya
            ssql = "UPDATE  " _
                    & "  public.bdgtd_det   " _
                    & "SET  " _
                    & "  bdgtd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & "  bdgtd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ", " _
                    & "  bdgtd_alokasi = bdgtd_alokasi + " & SetDbl(par_amount) & ",  " _
                    & "  bdgtd_realisasi = bdgtd_realisasi + " & SetDbl(par_amount) & ",  " _
                    & "  bdgtd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" _
                    & "  " _
                    & "WHERE  " _
                    & "  bdgtd_bdgt_oid = " & SetSetring(_bdgt_oid.ToString()) & "  " _
                    & "  and bdgtd_ac_id = " & SetInteger(par_ac_id) & "  " _
                    & "  and bdgtd_bdgtp_id in (select bdgtp_id from bdgtp_periode " _
                    & " where bdgtp_start_date <= " + SetDate(par_date) _
                    & " and bdgtp_end_date >= " + SetDate(par_date) _
                    & " ) "

            par_ssqls.Add(ssql)


        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Public Overrides Function edit_data() As Boolean
        Return False
    End Function

    Public Overrides Function edit()
        edit = False
        Return edit
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _disburs_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_date")
        Dim _disburs_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_disburs_en_id, "gcald_ap", _disburs_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _disburs_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _disburs_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function

    Public Overrides Function delete_data() As Boolean
        delete_data = False

        gv_master_SelectionChanged(Nothing, Nothing)

        Dim sSQL As String
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


        Dim ssqls As New ArrayList

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                With ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position)

                    sSQL = "DELETE FROM  " _
                        & "  public.disburs_out  " _
                        & "WHERE  " _
                        & "  disburs_oid ='" & .Item("disburs_oid").ToString & "'"

                    ssqls.Add(sSQL)

                    'update rekonsiliasi kas masuk
                    If update_rec(ssqls, .Item("disburs_en_id"), .Item("disburs_bk_id"), .Item("disburs_cu_id"), _
                                 .Item("disburs_exc_rate"), .Item("disburs_amount"), .Item("disburs_date"), .Item("disburs_code"), _
                                 "Delete Cash Out " & .Item("disburs_remarks"), "CASH OUT") = False Then
                        Return False
                        Exit Function
                    End If


                    If SetString(.Item("disburs_type")) = "REAL" Then
                        sSQL = "update  " _
                              & "  public.disburs_out " _
                              & "set disburs_close='N', disburs_amount_realization=coalesce(disburs_amount_realization,0) -  " & SetDec(.Item("disburs_amount")) _
                              & ", disburs_amount_remains=coalesce(disburs_amount_remains,0) +  " & SetDec(.Item("disburs_amount")) _
                              & " where disburs_oid=" & SetSetring(.Item("disburs_reff_oid"))

                        ssqls.Add(sSQL)
                    End If


                    If SetString(.Item("disburs_investd_oid")) <> "" Then

                        sSQL = "update invest_mstr set " _
                        & " invest_payment_out = coalesce(invest_payment_out,0) - " & SetDec(.Item("disburs_amount")) _
                        & " where invest_code=" & SetSetring(.Item("disburs_invest_code"))

                        ssqls.Add(sSQL)

                        sSQL = "update investd_detail set investd_payment_date_realization =null " _
                           & ",investd_paid=coalesce(investd_paid,0) - " & SetDec(.Item("disburs_amount")) _
                           & "  where investd_oid=" & SetSetring(.Item("disburs_investd_oid"))

                        ssqls.Add(sSQL)

                    End If


                    'jurnal balik
                    Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

                    If _create_jurnal = True Then

                        Dim _gcald_det_status As String = func_data.get_gcald_det_status(.Item("disburs_en_id"), "gcald_ap", CekTanggal)

                        If _gcald_det_status = "" Then
                            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + CekTanggal.Date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                            Exit Function
                        ElseIf _gcald_det_status.ToUpper = "Y" Then
                            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + CekTanggal.Date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                            Exit Function
                        End If


                        sSQL = "select glt_code, glt_posted from glt_det " + _
                             " where glt_ref_trans_code = '" + .Item("disburs_code") + "'"


                        Dim dt_posted As New DataTable

                        dt_posted = GetTableData(sSQL)

                        Dim _glt_code_old As String = ""
                        Dim _glt_posted_old As String = ""

                        For Each dr_posted As DataRow In dt_posted.Rows
                            _glt_code_old = dr_posted("glt_code")
                            _glt_posted_old = SetString(dr_posted("glt_posted"))
                        Next

                        If _glt_posted_old.ToUpper = "N" Then

                            sSQL = "select * from glt_det where glt_code = '" + _glt_code_old + "'"

                            Dim dt As New DataTable

                            dt = GetTableData(sSQL)
                            Dim _glt_value As Double
                            Dim _ac_sign As String = ""

                            For i As Integer = 0 To dt.Rows.Count - 1
                                If dt.Rows(i).Item("glt_debit") <> 0 Then
                                    _glt_value = dt.Rows(i).Item("glt_debit") * -1 'dikali -1 agar dibalik nilainya
                                    _ac_sign = "D"
                                ElseIf dt.Rows(i).Item("glt_credit") <> 0 Then
                                    _glt_value = dt.Rows(i).Item("glt_credit") * -1 'dikali -1 agar dibalik nilainya
                                    _ac_sign = "C"
                                End If

                                If update_glbal(ssqls, dt.Rows(i).Item("glt_ac_id"), _glt_value, _
                                               master_new.ClsVar.sdom_id, dt.Rows(i).Item("glt_en_id"), 0, 0, dt.Rows(i).Item("glt_date"), _
                                               dt.Rows(i).Item("glt_cu_id"), dt.Rows(i).Item("glt_exc_rate"), _ac_sign) = False Then
                                    Return False
                                    Exit Function
                                End If

                            Next

                            sSQL = "delete from glt_det where glt_code = '" + _glt_code_old + "'" + _
                                " and glt_posted = 'N' "

                            ssqls.Add(sSQL)
                        Else


                            'insert dulu debetnya
                            Dim _ac_id_debit As String
                            _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("disburs_bk_id"))

                            Dim _glt_code As String
                            _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CO" _
                                   & .Item("en_code") & (CekTanggal().ToString("yyMM")))


                            'insert GL
                            If insert_gl(ssqls, _glt_code, _ac_id_debit, .Item("disburs_amount"), 0, _
                                         master_new.ClsVar.sdom_id, .Item("disburs_en_id"), 0, 0, _
                                          .Item("disburs_date"), .Item("disburs_cu_id"), .Item("disburs_exc_rate"), _
                                         "CO", 1, "Delete Cash Out " & .Item("disburs_remarks"), "", .Item("disburs_code"), "CASH OUT") = False Then
                                Return False
                                Exit Function
                            End If

                            If update_glbal(ssqls, _ac_id_debit, .Item("disburs_amount"), master_new.ClsVar.sdom_id, _
                                            .Item("disburs_en_id"), 0, 0, .Item("disburs_date"), .Item("disburs_cu_id"), _
                                            .Item("disburs_exc_rate"), "D") = False Then
                                Return False
                                Exit Function
                            End If


                            For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
                                'With ds.Tables("detail").Rows(i)

                                If insert_gl(ssqls, _glt_code, ds.Tables("detail").Rows(i).Item("disbursd_ac_id"), 0, ds.Tables("detail").Rows(i).Item("disbursd_amount"), _
                                    master_new.ClsVar.sdom_id, .Item("disburs_en_id"), 0, 0, _
                                    .Item("disburs_date"), .Item("disburs_cu_id"), .Item("disburs_exc_rate"), _
                                    "CO", i + 2, "Delete " & ds.Tables("detail").Rows(i).Item("disbursd_remarks"), "", .Item("disburs_code"), "CASH OUT") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, ds.Tables("detail").Rows(i).Item("disbursd_ac_id"), ds.Tables("detail").Rows(i).Item("disbursd_amount"), _
                                                master_new.ClsVar.sdom_id, .Item("disburs_en_id"), 0, 0, .Item("disburs_date"), _
                                                .Item("disburs_cu_id"), .Item("disburs_exc_rate"), "C") = False Then
                                    Return False
                                    Exit Function
                                End If
                                'End With


                                If _conf_budget = "1" Then
                                    If update_budget(ssqls, ds.Tables("detail").Rows(i).Item("disbursd_ac_id"), ds.Tables("detail").Rows(i).Item("disbursd_amount") * -1, .Item("disburs_en_id"), .Item("disburs_date"), ds.Tables("detail").Rows(i).Item("disbursd_cc_id")) = False Then
                                        Return False
                                        Exit Function
                                    End If
                                End If

                            Next
                        End If





                    End If

                End With

                ssqls.Add(insert_log("Delete Cash Out " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_code")))

                If master_new.PGSqlConn.status_sync = True Then
                    'DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "")
                    If DbRunTran(ssqls, "", master_new.PGSqlConn.FinsertSQL2Array(ssqls), "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                Else
                    If DbRunTran(ssqls, "") = False Then
                        Return False
                        Exit Function
                    End If
                    ssqls.Clear()
                End If

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        'If disburs_bk_id.ItemIndex = -1 Then
        '    MessageBox.Show("Bank can't empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If


        Dim _gcald_det_status As String = func_data.get_gcald_det_status(disburs_en_id.EditValue, "gcald_ap", disburs_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + disburs_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + disburs_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If required(disburs_en_id, "Entity") = False Then
            Return False
            Exit Function
        End If

        'If required(disburs_ptnr_id, "Partner") = False Then
        '    Return False
        '    Exit Function
        'End If

        'If __ptnr_id = "" Then
        '    Box("Vendor / Partner can't blank")
        '    disburs_ptnr_id.Focus()
        '    Return False
        '    Exit Function
        'End If

        If required(disburs_point, "Amount") = False Then
            Return False
            Exit Function
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If

        Dim _total As Double = 0.0
        Dim _header As Double = 0.0

        _header = SetNumber(disburs_point.EditValue)

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            With ds_edit.Tables(0).Rows(i)
                If .Item("disbursd_ac_id") Is System.DBNull.Value Then
                    Box("Account can't blank")
                    Return False
                    Exit Function
                End If
                If .Item("disbursd_amount") Is System.DBNull.Value Then
                    Box("Amount can't blank")
                    Return False
                    Exit Function
                End If

                _total = _total + System.Math.Round(.Item("disbursd_amount"), 2)

                If .Item("disbursd_cc_id") Is System.DBNull.Value Then
                    Box("Cost Center can't blank")
                    Return False
                    Exit Function
                End If
            End With
        Next

        If System.Math.Round(_total, 2) <> _header Then
            Box("Amount Header and Detail does not equal. Header : " & disburs_point.EditValue & ", Detail : " & _total)
            Return False
            Exit Function
        End If

        If func_coll.get_conf_file("cash_out_control") = "1" Then

            If disburs_type_id.EditValue = "TEMP" Then
                Dim _count As Integer = 0
                ssql = "SELECT  " _
                   & "  count(*) as jml " _
                   & "FROM " _
                   & "  public.disburs_out a " _
                   & "WHERE " _
                   & " disburs_type='TEMP' and coalesce(disburs_close,'N')='N' and  a.disburs_ptnr_id =  " & SetInteger(__ptnr_id) _
                   & " "


                Dim dt_count As New DataTable
                dt_count = GetTableData(ssql)
                For Each dr_count As DataRow In dt_count.Rows
                    _count = dr_count(0)
                Next
                If _count > 2 Then
                    Box("This partner has exceeded the transaction limit")
                    Return False
                    Exit Function
                End If
            End If
        End If



        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(sender, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Try
                ds.Tables("detail").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.disbursd_oid, " _
                & "  a.disbursd_disburs_oid, " _
                & "  a.disbursd_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name, " _
                & "  a.disbursd_amount, " _
                & "  a.disbursd_remarks,a.disbursd_cc_id, c.cc_desc " _
                & "FROM " _
                & "  public.disbursd_detail a " _
                & "  INNER JOIN public.ac_mstr b ON (a.disbursd_ac_id = b.ac_id) " _
                & "  INNER JOIN public.cc_mstr c ON (a.disbursd_cc_id = c.cc_id) " _
                & " Where  a.disbursd_disburs_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_oid").ToString & "' " _
                & " ORDER BY disbursd_seq "

            load_data_detail(sql, gc_detail, "detail")

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        Try
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
                browse_data()
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        Try
            browse_data()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _en_id As Integer = disburs_en_id.EditValue

        If _col = "ac_code" Or _col = "ac_name" Then
            Dim _filter As String

            _filter = " and ac_id in (SELECT  " _
              & "  enacc_ac_id " _
              & "FROM  " _
              & "  public.enacc_mstr  " _
              & "Where enacc_en_id=" & SetInteger(disburs_en_id.EditValue) & " and enacc_code='cash_out_detail') "

            Dim frm As New FAccountSearch
            frm.set_win(Me)

            If limit_account(disburs_en_id.EditValue) = True Then
                frm._obj = _filter
            Else
                frm._obj = ""
            End If

            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()

        ElseIf _col = "cc_desc" Then
            Dim frm As New FCostCenterSearch
            frm.set_win(Me)
            frm._en_id = disburs_en_id.EditValue
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    'Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Try

    '        Dim frm As New FPartnerSearch
    '        frm.set_win(Me)
    '        frm._obj = disburs_ptnr_id
    '        frm._en_id = disburs_en_id.EditValue
    '        frm.type_form = True
    '        frm.ShowDialog()

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        'Try
        '    gv_edit.UpdateCurrentRow()
        '    Dim _col As String = gv_edit.FocusedColumn.Name
        '    Dim _row As Integer = gv_edit.FocusedRowHandle

        '    If _col = "si_desc" Then

        '        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '            ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
        '            ds_edit.Tables(0).Rows(i).Item("wocid_si_id") = gv_edit.GetRowCellValue(_row, "wocid_si_id")
        '        Next

        '    ElseIf _col = "loc_desc" Then
        '        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '            ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
        '            ds_edit.Tables(0).Rows(i).Item("wocid_loc_id") = gv_edit.GetRowCellValue(_row, "wocid_loc_id")
        '        Next


        '    End If

        'Catch ex As Exception
        '    Pesan(Err)
        'End Try
    End Sub

    'Private Sub disburs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles disburs_en_id.EditValueChanged
    '    Try
    '        Dim _filter As String

    '        _filter = "and bk_id in (SELECT  " _
    '            & "  bk_id " _
    '            & "FROM  " _
    '            & "  public.bk_mstr  " _
    '            & "WHERE  " _
    '            & "bk_ac_id in (SELECT  " _
    '            & "  enacc_ac_id " _
    '            & "FROM  " _
    '            & "  public.enacc_mstr  " _
    '            & "Where enacc_en_id=" & SetInteger(disburs_en_id.EditValue) & " and enacc_code='cash_out_header'))"

    '        If limit_account(disburs_en_id.EditValue) = True Then
    '            init_le(disburs_bk_id, "bk_mstr", disburs_en_id.EditValue, _filter)
    '        Else
    '            init_le(disburs_bk_id, "bk_mstr", disburs_en_id.EditValue)
    '        End If

    '    Catch ex As Exception
    '        Pesan(Err)
    '    End Try
    'End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  disburs_oid, " _
            & "  disburs_dom_id, " _
            & "  disburs_en_id, " _
            & "  disburs_add_by, " _
            & "  disburs_add_date, " _
            & "  disburs_upd_by, " _
            & "  disburs_upd_date, " _
            & "  disburs_bk_id, " _
            & "  disburs_ptnr_id, " _
            & "  disburs_code, " _
            & "  disburs_date, " _
            & "  disburs_remarks, " _
            & "  disburs_reff, " _
            & "  disburs_cu_id, " _
            & "  disburs_exc_rate, " _
            & "  disburs_amount, " _
            & "  disburs_amount * disburs_exc_rate as disburs_amount_ext, " _
            & "  disburs_check_number, " _
            & "  disburs_post_dated_check, " _
            & "  disbursd_oid, " _
            & "  disbursd_disburs_oid, " _
            & "  disbursd_ac_id, " _
            & "  disbursd_amount, " _
            & "  disbursd_amount * disburs_exc_rate as disbursd_amount_ext, " _
            & "  disbursd_remarks, " _
            & "  disbursd_seq, " _
            & "  bk_name, " _
            & "  ptnr_name, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3 " _
            & "FROM  " _
            & "  disburs_out " _
            & "inner join disbursd_detail on disbursd_disburs_oid = disburs_oid " _
            & "inner join bk_mstr on bk_id = disburs_bk_id " _
            & "inner join ptnr_mstr on ptnr_id = disburs_ptnr_id " _
            & "inner join cu_mstr on cu_id = disburs_cu_id " _
            & "inner join ac_mstr on ac_id = disbursd_ac_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = disburs_en_id" _
            & "  where disburs_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRCashOutPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("disburs_code")
        frm.ShowDialog()

    End Sub

    'Private Sub disburs_type_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles disburs_type_id.EditValueChanged
    '    If SetString(disburs_type_id.EditValue) = "REAL" Then
    '        disburs_reff_oid.Enabled = True
    '    Else
    '        disburs_reff_oid.Enabled = False
    '    End If
    'End Sub

    'Private Sub disburs_reff_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
    '    Try
    '        If __ptnr_id = "" Then
    '            Box("Please fill partner field")
    '            Exit Sub
    '        End If
    '        Dim frm As New FCashOutSearch
    '        frm.set_win(Me)

    '        frm._ptnr_id = __ptnr_id
    '        frm._obj = disburs_reff_oid
    '        frm.type_form = True
    '        frm.ShowDialog()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub LblUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblUpdate.Click
        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " update Data Ini..?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If

        Try
            Dim x As Integer = 1
            Dim ssql As String = ""
            Dim dt_det As New DataTable
            For Each dr As DataRow In ds.Tables(0).Rows

                ssql = "SELECT  " _
                & "  a.disbursd_oid, " _
                & "  a.disbursd_disburs_oid, " _
                & "  a.disbursd_ac_id, " _
                & "  a.disbursd_amount, " _
                & "  a.disbursd_remarks,a.disbursd_cc_id " _
                & "FROM " _
                & "  public.disbursd_detail a " _
                & " Where  a.disbursd_disburs_oid='" & dr("disburs_oid").ToString & "' "

                dt_det = GetTableData(ssql)


                LblUpdate.Text = x & " of " & ds.Tables(0).Rows.Count
                x = x + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub disburs_investd_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            Dim frm As New FInvestmentSearch
            frm.set_win(Me)
            frm._en_id = disburs_en_id.EditValue
            frm._ptnr_id = __ptnr_id
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
