Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class FCashIn
    Dim ssql As String
    Dim _mstr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public __woci_wo_id As String
    Public _invest_top_bulan As Double

    Public __ptnr_id As String
    ' Dim ds_check As New DataSet
    Public ds_edit As DataSet

    Private Sub FPartnerAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ssql As String = "SELECT  " _
            & "  a.lang_oid, " _
            & "  a.lang_form, " _
            & "  a.lang_comp_name, " _
            & "  a.lang_id, " _
            & "  a.lang_en " _
            & "FROM " _
            & "  public.lang_mstr a " _
            & "WHERE " _
            & "  a.lang_form = '" & Me.Name & "' " _
            & "ORDER BY " _
            & "  a.lang_comp_name"

        _dt_lang = GetTableData(ssql)

        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(cashi_en_id, "en_mstr")
        init_le(cashi_cu_id, "cu_mstr")

    End Sub

    Public Overrides Sub change_lang()
        Try
            '    Dim ssql As String = "SELECT  " _
            '        & "  a.lang_oid, " _
            '        & "  a.lang_form, " _
            '        & "  a.lang_comp_name, " _
            '        & "  a.lang_id, " _
            '        & "  a.lang_en " _
            '        & "FROM " _
            '        & "  public.lang_mstr a " _
            '        & "WHERE " _
            '        & "  a.lang_form = '" & Me.Name & "' " _
            '        & "ORDER BY " _
            '        & "  a.lang_comp_name"

            '    _dt_lang = GetTableData(ssql)

            '    set_lang(scc_master.Panel1)
            '    set_lang(xtc_detail)
            '    set_lang(LayoutControlGroup2)
            '    set_lang(gv_master)
            '    set_lang(gv_detail)
            '    set_lang(gv_edit)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "cashi_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank Name", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Code", "cashi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "cashi_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Remarks", "cashi_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Refference", "cashi_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Investment Code", "cashi_invest_code", DevExpress.Utils.HorzAlignment.Default)


        add_column_copy(gv_master, "Reverse Status", "cashi_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Close Status", "cashi_close_temp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cash Out Number", "cashi_reff_code", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Amount", "cashi_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount Used", "cashi_amount_used", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Amount Remains", "cashi_amount_remains", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "AR Payment", "cashi_ar_payment", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Balance", "cashi_balance", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "cashi_exc_rate", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Reverse", "cashi_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cashi_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cashi_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cashi_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cashi_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "cashid_oid", False)
        add_column(gv_detail, "cashid_cashi_oid", False)
        add_column(gv_detail, "cashid_ac_id", False)
        add_column(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Amount", "cashid_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_detail, "Remarks", "cashid_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "cashid_oid", False)
        add_column(gv_edit, "cashid_cashi_oid", False)
        add_column(gv_edit, "cashid_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Amount", "cashid_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_edit(gv_edit, "Remarks", "cashid_remarks", DevExpress.Utils.HorzAlignment.Default)


    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.cashi_oid, " _
                & "  a.cashi_dom_id, " _
                & "  a.cashi_en_id, " _
                & "  b.en_desc,b.en_code,coalesce(cashi_close_temp,'N') as cashi_close_temp , " _
                & "  a.cashi_add_by, " _
                & "  a.cashi_add_date, " _
                & "  a.cashi_upd_by, " _
                & "  a.cashi_upd_date, " _
                & "  a.cashi_bk_id, " _
                & "  c.bk_name, " _
                & "  d.ptnr_name, " _
                & "  a.cashi_code, " _
                & "  a.cashi_date, " _
                & "  a.cashi_remarks, " _
                & "  a.cashi_reff, " _
                & "  a.cashi_amount, " _
                & "  a.cashi_cu_id,cashi_reverse, cashi_reff_oid, cashi_reff_code, " _
                & "  e.cu_name,cashi_so_oid, cashi_amount_used,so_code, cashi_amount_remains, " _
                & "  a.cashi_exc_rate,coalesce(cashi_is_reverse,'N') as cashi_is_reverse,cashi_invest_code " _
                & "FROM " _
                & "  public.cashi_in a " _
                & "  INNER JOIN public.en_mstr b ON (a.cashi_en_id = b.en_id) " _
                & "  INNER JOIN public.bk_mstr c ON (a.cashi_bk_id = c.bk_id) " _
                & "  LEFT OUTER JOIN public.ptnr_mstr d ON (a.cashi_ptnr_id = d.ptnr_id) " _
                & "  INNER JOIN public.cu_mstr e ON (a.cashi_cu_id = e.cu_id) " _
                 & "  LEFT OUTER JOIN public.so_mstr f ON (a.cashi_so_oid = f.so_oid) " _
                & "  Where a.cashi_en_id in (select user_en_id from tconfuserentity " _
                & "  where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                & " and cashi_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & " ORDER BY cashi_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        cashi_en_id.EditValue = ""
        cashi_code.EditValue = ""
        cashi_code.Enabled = False
        cashi_bk_id.ItemIndex = 0
        cashi_cu_id.ItemIndex = 0
        cashi_exc_rate.EditValue = 1.0
        cashi_ptnr_id.EditValue = ""
        cashi_date.DateTime = CekTanggal()
        cashi_reff.EditValue = ""
        cashi_amount.EditValue = 0.0
        cashi_remarks.EditValue = ""
        cashi_is_reverse.EditValue = False
        cashi_en_id.Focus()
        cashi_so_oid.Text = ""
        cashi_so_oid.Tag = ""
        cashi_reverse.Checked = False
        cashi_reff_oid.Enabled = False
        cashi_close_temp.Checked = False
        cashi_invest_code.Tag = ""
        cashi_invest_code.Text = ""
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
                        & "  a.cashid_oid, " _
                        & "  a.cashid_cashi_oid, " _
                        & "  a.cashid_ac_id, " _
                        & "  b.ac_code, " _
                        & "  b.ac_name, " _
                        & "  a.cashid_amount, " _
                        & "  a.cashid_remarks " _
                        & "FROM " _
                        & "  public.cashid_detail a " _
                        & "  INNER JOIN public.ac_mstr b ON (a.cashid_ac_id = b.ac_id) " _
                        & " Where  a.cashid_cashi_oid IS NULL "

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

        Dim _mstr_oid As String = Guid.NewGuid.ToString
        Dim i As Integer
        Dim ssqls As New ArrayList


        cashi_code.Text = GetNewNumberYM("cashi_in", "cashi_code", 5, "CSI" & cashi_en_id.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        ds_edit.Tables(0).AcceptChanges()

        Try
            ssql = "INSERT INTO  " _
                & "  public.cashi_in " _
                & "( " _
                & "  cashi_oid, " _
                & "  cashi_dom_id, " _
                & "  cashi_en_id, " _
                & "  cashi_add_by, " _
                & "  cashi_add_date, " _
                & "  cashi_bk_id, " _
                & "  cashi_ptnr_id, cashi_close_temp, " _
                & "  cashi_code, cashi_reverse, cashi_reff_oid, cashi_reff_code, " _
                & "  cashi_date,cashi_so_oid,cashi_amount_used,cashi_amount_remains, " _
                & "  cashi_remarks, " _
                & "  cashi_reff, " _
                & "  cashi_amount, " _
                & "  cashi_cu_id,cashi_invest_code, " _
                & "  cashi_exc_rate,cashi_is_reverse " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(_mstr_oid) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(cashi_en_id.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime(CekTanggal) & ",  " _
                & SetInteger(cashi_bk_id.EditValue) & ",  " _
                & SetInteger(__ptnr_id) & ",  " _
                & SetBitYN(cashi_close_temp.Checked) & ",  " _
                & SetSetring(cashi_code.Text) & ",  " _
                & SetBitYN(cashi_reverse.Checked) & ",  " _
                 & SetSetring(cashi_reff_oid.Tag) & ",  " _
                  & SetSetring(cashi_reff_oid.Text) & ",  " _
                & SetDateNTime00(cashi_date.DateTime) & ",  " _
                & SetSetring(cashi_so_oid.Tag) & ",  " _
                & SetDec(0) & ",  " _
                & SetDec(cashi_amount.EditValue) & ",  " _
                & SetSetring(cashi_remarks.Text) & ",  " _
                & SetSetring(cashi_reff.Text) & ",  " _
                & SetDec(cashi_amount.EditValue) & ",  " _
                & SetInteger(cashi_cu_id.EditValue) & ",  " _
                & SetSetring(cashi_invest_code.Tag) & "," _
                & SetDec(cashi_exc_rate.EditValue) & ",  " _
                & SetBitYN(cashi_is_reverse.EditValue) _
                & ")"


            ssqls.Add(ssql)

            If cashi_so_oid.Text <> "" Then
                ssql = "update so_mstr set so_indent='Y' where so_oid=" & SetSetring(cashi_so_oid.Tag)

                ssqls.Add(ssql)
            End If
            

            For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                With ds_edit.Tables(0).Rows(i)
                    ssql = "INSERT INTO  " _
                        & "  public.cashid_detail " _
                        & "( " _
                        & "  cashid_oid, " _
                        & "  cashid_cashi_oid, " _
                        & "  cashid_ac_id, " _
                        & "  cashid_amount, " _
                        & "  cashid_remarks, " _
                        & "  cashid_seq " _
                        & ")  " _
                        & "VALUES ( " _
                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        & SetSetring(_mstr_oid) & ",  " _
                        & SetInteger(.Item("cashid_ac_id")) & ",  " _
                        & SetDec(.Item("cashid_amount")) & ",  " _
                        & SetSetring(.Item("cashid_remarks")) & ",  " _
                        & SetInteger(i) & "  " _
                        & ")"

                    ssqls.Add(ssql)

                End With
            Next

            'update rekonsiliasi kas masuk
            If update_rec(ssqls, cashi_en_id.EditValue, cashi_bk_id.EditValue, cashi_cu_id.EditValue, _
                         cashi_exc_rate.EditValue, cashi_amount.EditValue, cashi_date.DateTime, cashi_code.Text, _
                         cashi_remarks.Text, "CASH IN") = False Then
                Return False
                Exit Function
            End If


            If cashi_reverse.Checked = True Then

                ssql = "update  " _
                      & "  public.casho_out " _
                      & "set  casho_amount_reverse=coalesce(casho_amount_reverse,0) +  " & SetDec(cashi_amount.EditValue) _
                      & " where casho_oid=" & SetSetring(cashi_reff_oid.Tag)

                ssqls.Add(ssql)
            End If

            If cashi_close_temp.Checked = True Then
                ssql = "update  " _
                     & "  public.casho_out " _
                     & "set casho_close=" & SetBitYN(cashi_close_temp.Checked) & " where casho_oid=" & SetSetring(cashi_reff_oid.Tag)

                ssqls.Add(ssql)
            End If

            If cashi_invest_code.Tag <> "" Then

                ssql = "update invest_mstr set " _
                & " invest_payment_in = coalesce(invest_payment_in,0) + " & SetDec(cashi_amount.EditValue) _
                & ", invest_tanggal_transfer=" & SetDateNTime00(cashi_date.DateTime) _
                & ", invest_awal_angsuran=" & SetDateNTime00(DateAdd(DateInterval.Month, 1, cashi_date.DateTime)) _
                & ", invest_akhir_angsuran=" & SetDateNTime00(DateAdd(DateInterval.Month, _invest_top_bulan, cashi_date.DateTime)) _
                & " where invest_code=" & SetSetring(cashi_invest_code.Tag)

                ssqls.Add(ssql)

                ssql = "SELECT  " _
                     & "  a.investd_oid, " _
                     & "  a.investd_invest_code, " _
                     & "  a.investd_periode, " _
                     & "  a.investd_payment_date_schedule, " _
                     & "  a.investd_payment_date_realization, " _
                     & "  a.investd_principal_installment, " _
                     & "  a.investd_profit_sharing, " _
                     & "  a.investd_remaining_principal, " _
                     & "  a.investd_payment_total,investd_paid, investd_payment_total - coalesce(investd_paid,0) as investd_outstanding " _
                     & "FROM " _
                     & "  public.investd_detail a " _
                     & "WHERE " _
                     & "  a.investd_invest_code='" & cashi_invest_code.Tag & "' " _
                     & "ORDER BY " _
                     & "  a.investd_periode"

                Dim dt_investd As New DataTable
                dt_investd = GetTableData(ssql)

                Dim x As Integer = 1
                For Each dr_investd As DataRow In dt_investd.Rows
                    ssql = "update investd_detail set investd_payment_date_schedule = " & SetDateNTime00(DateAdd(DateInterval.Month, x, cashi_date.DateTime)) _
                    & " where investd_oid=" & SetSetring(dr_investd("investd_oid"))

                    ssqls.Add(ssql)
                    x = x + 1
                Next

            End If


            'jurnal
            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

            If _create_jurnal = True Then

                'insert dulu debetnya
                Dim _ac_id_debet As String
                _ac_id_debet = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", cashi_bk_id.EditValue)

                Dim _glt_code As String
                '_glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CI" _
                '       & cashi_en_id.GetColumnValue("en_code") & (CekTanggal().ToString("yyMM")))


                _glt_code = func_coll.get_transaction_number("CI", cashi_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

                'insert GL
                If insert_gl(ssqls, _glt_code, _ac_id_debet, cashi_amount.EditValue, 0, _
                             master_new.ClsVar.sdom_id, cashi_en_id.EditValue, 0, 0, _
                             cashi_date.DateTime, cashi_cu_id.EditValue, cashi_exc_rate.EditValue, _
                             "CI", 1, "Cash IN " & cashi_remarks.EditValue, _mstr_oid, cashi_code.EditValue, "CASH IN", SetBitYN(cashi_is_reverse.EditValue)) = False Then
                    Return False
                    Exit Function
                End If

                If update_glbal(ssqls, _ac_id_debet, cashi_amount.EditValue, master_new.ClsVar.sdom_id, _
                                cashi_en_id.EditValue, 0, 0, cashi_date.DateTime, cashi_cu_id.EditValue, _
                                cashi_exc_rate.EditValue, "D") = False Then
                    Return False
                    Exit Function
                End If

                For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                    With ds_edit.Tables(0).Rows(i)

                        If insert_gl(ssqls, _glt_code, .Item("cashid_ac_id"), 0, .Item("cashid_amount"), _
                            master_new.ClsVar.sdom_id, cashi_en_id.EditValue, 0, 0, _
                            cashi_date.DateTime, cashi_cu_id.EditValue, cashi_exc_rate.EditValue, _
                            "CI", i + 2, SetString(.Item("cashid_remarks")), _mstr_oid, cashi_code.EditValue, "CASH IN", SetBitYN(cashi_is_reverse.EditValue)) = False Then
                            Return False
                            Exit Function
                        End If

                        If update_glbal(ssqls, .Item("cashid_ac_id"), .Item("cashid_amount"), _
                                        master_new.ClsVar.sdom_id, cashi_en_id.EditValue, 0, 0, cashi_date.DateTime, _
                                        cashi_cu_id.EditValue, cashi_exc_rate.EditValue, "C") = False Then
                            Return False
                            Exit Function
                        End If
                    End With
                Next

            End If

            ssqls.Add(insert_log("Insert Cash IN " & cashi_code.Text))

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
            set_row(_mstr_oid, "cashi_oid")
            dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
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

        Dim _cashi_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_date")
        Dim _cashi_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_en_id")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_cashi_en_id, "gcald_ap", _cashi_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _cashi_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _cashi_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        If SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_amount_used")) > 0 Then
            Box("This data have been used in AR Payments Detail")
            Return False
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
                        & "  public.cashi_in  " _
                        & "WHERE  " _
                        & "  cashi_oid ='" & .Item("cashi_oid").ToString & "'"

                    ssqls.Add(sSQL)



                    sSQL = "update so_mstr set so_indent=null where so_oid=" & SetSetring(.Item("cashi_so_oid").ToString)

                    ssqls.Add(sSQL)

                    If SetString(.Item("cashi_reverse")) = "Y" Then

                        sSQL = "update  " _
                              & "  public.casho_out " _
                              & "set  casho_amount_reverse=coalesce(casho_amount_reverse,0) -  " & SetDec(.Item("cashi_amount")) _
                              & " where casho_oid=" & SetSetring(.Item("cashi_reff_oid"))

                        ssqls.Add(sSQL)
                    End If



                    'update rekonsiliasi kas masuk
                    If update_rec(ssqls, .Item("cashi_en_id"), .Item("cashi_bk_id"), .Item("cashi_cu_id"), _
                                 .Item("cashi_exc_rate"), .Item("cashi_amount") * -1, .Item("cashi_date"), .Item("cashi_code"), _
                                 "Delete Cash In " & .Item("cashi_remarks"), "CASH IN") = False Then
                        Return False
                        Exit Function
                    End If


                    If SetString(.Item("cashi_invest_code")) <> "" Then
                        sSQL = "SELECT invest_payment_out from invest_mstr where invest_code=" & SetSetring(.Item("cashi_invest_code"))

                        If SetNumber(GetRowInfo(sSQL)(0)) > 0 Then
                            Box("This data have been Payout")
                            Return False
                            Exit Function
                        End If


                        sSQL = "update invest_mstr set " _
                                & " invest_payment_in = coalesce(invest_payment_in,0) - " & SetDec(cashi_amount.EditValue) _
                                & ", invest_tanggal_transfer=null " _
                                & ", invest_awal_angsuran=null" _
                                & ", invest_akhir_angsuran=null" _
                                & " where invest_code=" & SetSetring(.Item("cashi_invest_code"))

                        ssqls.Add(sSQL)

                        sSQL = "SELECT  " _
                             & "  a.investd_oid, " _
                             & "  a.investd_invest_code, " _
                             & "  a.investd_periode, " _
                             & "  a.investd_payment_date_schedule, " _
                             & "  a.investd_payment_date_realization, " _
                             & "  a.investd_principal_installment, " _
                             & "  a.investd_profit_sharing, " _
                             & "  a.investd_remaining_principal, " _
                             & "  a.investd_payment_total,investd_paid, investd_payment_total - coalesce(investd_paid,0) as investd_outstanding " _
                             & "FROM " _
                             & "  public.investd_detail a " _
                             & "WHERE " _
                             & "  a.investd_invest_code=" & SetSetring(.Item("cashi_invest_code")) & " " _
                             & "ORDER BY " _
                             & "  a.investd_periode"

                        Dim dt_investd As New DataTable
                        dt_investd = GetTableData(sSQL)

                        Dim x As Integer = 1
                        For Each dr_investd As DataRow In dt_investd.Rows
                            sSQL = "update investd_detail set investd_payment_date_schedule =null " _
                            & " where investd_oid=" & SetSetring(dr_investd("investd_oid"))

                            ssqls.Add(sSQL)
                            x = x + 1
                        Next

                    End If


                    'jurnal balik
                    Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

                    If _create_jurnal = True Then

                        Dim _gcald_det_status As String = func_data.get_gcald_det_status(.Item("cashi_en_id"), "gcald_ar", CekTanggal)

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
                               " where glt_ref_trans_code = '" + .Item("cashi_code") + "'"


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
                            Dim _ac_id_credit As String
                            _ac_id_credit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("cashi_bk_id"))

                            Dim _glt_code As String
                            _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CI" _
                                   & .Item("en_code") & (CekTanggal().ToString("yyMM")))

                            'insert gl debet dulu
                            For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1

                                If insert_gl(ssqls, _glt_code, ds.Tables("detail").Rows(i).Item("cashid_ac_id"), ds.Tables("detail").Rows(i).Item("cashid_amount"), 0, _
                                    master_new.ClsVar.sdom_id, .Item("cashi_en_id"), 0, 0, _
                                    .Item("cashi_date"), .Item("cashi_cu_id"), .Item("cashi_exc_rate"), _
                                    "CI", i + 2, "Delete " & ds.Tables("detail").Rows(i).Item("cashid_remarks"), "", .Item("cashi_code"), "CASH IN") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, ds.Tables("detail").Rows(i).Item("cashid_ac_id"), ds.Tables("detail").Rows(i).Item("cashid_amount"), _
                                                master_new.ClsVar.sdom_id, .Item("cashi_en_id"), 0, 0, .Item("cashi_date"), _
                                                .Item("cashi_cu_id"), .Item("cashi_exc_rate"), "D") = False Then
                                    Return False
                                    Exit Function
                                End If
                                'End With
                            Next

                            'insert GL
                            If insert_gl(ssqls, _glt_code, _ac_id_credit, 0, .Item("cashi_amount"), _
                                         master_new.ClsVar.sdom_id, .Item("cashi_en_id"), 0, 0, _
                                          .Item("cashi_date"), .Item("cashi_cu_id"), .Item("cashi_exc_rate"), _
                                         "CI", 1, "Delete Cash Out " & .Item("cashi_remarks"), "", .Item("cashi_code"), "CASH OUT") = False Then
                                Return False
                                Exit Function
                            End If

                            If update_glbal(ssqls, _ac_id_credit, .Item("cashi_amount"), master_new.ClsVar.sdom_id, _
                                            .Item("cashi_en_id"), 0, 0, .Item("cashi_date"), .Item("cashi_cu_id"), _
                                            .Item("cashi_exc_rate"), "C") = False Then
                                Return False
                                Exit Function
                            End If
                        End If

                    End If

                End With

                ssqls.Add(insert_log("Delete Cash IN " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code")))

                If master_new.PGSqlConn.status_sync = True Then
                    'Dim _data As String = arr_to_str(ssqls)
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

        Dim _gcald_det_status As String = func_data.get_gcald_det_status(cashi_en_id.EditValue, "gcald_ap", cashi_date.DateTime)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + cashi_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + cashi_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If


        If required(cashi_en_id, "Entity") = False Then
            Return False
            Exit Function
        End If

        If __ptnr_id = "" Then
            Box("Partner can't empty")
            Return False
            Exit Function
        End If


        If required(cashi_amount, "Amount") = False Then
            Return False
            Exit Function
        End If

        If ds_edit.Tables(0).Rows.Count = 0 Then
            Box("Detail can't blank")
            Return False
            Exit Function
        End If

        Dim _total As Double = 0

        For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            With ds_edit.Tables(0).Rows(i)
                If .Item("cashid_ac_id") Is System.DBNull.Value Then
                    Box("Account can't blank")
                    Return False
                    Exit Function
                End If
                If .Item("cashid_amount") Is System.DBNull.Value Then
                    Box("Amount can't blank")
                    Return False
                    Exit Function
                End If
                _total += .Item("cashid_amount")

            End With
        Next

        If System.Math.Round(_total, 2) <> SetNumber(cashi_amount.EditValue) Then
            Box("Amount Header and Detail does not equal")
            Return False
            Exit Function
        End If

        'For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
        '    With ds_edit.Tables(0).Rows(i)
        '        Dim _stock As Double = get_stock(.Item("wocid_pt_id"), .Item("wocid_si_id"), .Item("wocid_loc_id"), casho_en_id.EditValue)

        '        If _stock < SetNumber(.Item("wocid_qty")) Then

        '            MessageBox.Show("Inventory " + .Item("pt_code") + " " + .Item("pt_desc1") + " in location " _
        '                            + .Item("loc_desc") + " = " & _stock & ", Lower Than Qty Process... (" _
        '                            & SetNumber(.Item("wocid_qty")) & ")", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            Return False
        '            Exit Function
        '        End If
        '    End With
        'Next

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
                & "  a.cashid_oid, " _
                & "  a.cashid_cashi_oid, " _
                & "  a.cashid_ac_id, " _
                & "  b.ac_code, " _
                & "  b.ac_name, " _
                & "  a.cashid_amount, " _
                & "  a.cashid_remarks " _
                & "FROM " _
                & "  public.cashid_detail a " _
                & "  INNER JOIN public.ac_mstr b ON (a.cashid_ac_id = b.ac_id) " _
                & " Where  a.cashid_cashi_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_oid").ToString & "' " _
                & " ORDER BY cashid_seq "

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
        'Dim _en_id As Integer = casho_en_id.EditValue

        If _col = "ac_code" Or _col = "ac_name" Then

            Dim _filter As String

            _filter = " and ac_id in (SELECT  " _
              & "  enacc_ac_id " _
              & "FROM  " _
              & "  public.enacc_mstr  " _
              & "Where enacc_en_id=" & SetInteger(cashi_en_id.EditValue) & " and enacc_code='cash_in_detail') "

            Dim frm As New FAccountSearch
            frm.set_win(Me)

            If limit_account(cashi_en_id.EditValue) = True Then
                frm._obj = _filter
            Else
                frm._obj = ""
            End If

            'Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            ' frm._en_id = _en_id
            frm.type_form = True
            frm.ShowDialog()

        End If
    End Sub

    Private Sub ps_bom_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cashi_ptnr_id.ButtonClick
        Try

            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = cashi_ptnr_id
            frm._en_id = cashi_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub SetToAllRowsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetToAllRowsToolStripMenuItem.Click
        Try
            'gv_edit.UpdateCurrentRow()
            'Dim _col As String = gv_edit.FocusedColumn.Name
            'Dim _row As Integer = gv_edit.FocusedRowHandle

            'If _col = "si_desc" Then

            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("si_desc") = gv_edit.GetRowCellValue(_row, "si_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_si_id") = gv_edit.GetRowCellValue(_row, "wocid_si_id")
            '    Next

            'ElseIf _col = "loc_desc" Then
            '    For i As Integer = 0 To ds_edit.Tables(0).Rows.Count - 1
            '        ds_edit.Tables(0).Rows(i).Item("loc_desc") = gv_edit.GetRowCellValue(_row, "loc_desc")
            '        ds_edit.Tables(0).Rows(i).Item("wocid_loc_id") = gv_edit.GetRowCellValue(_row, "wocid_loc_id")
            '    Next


            'End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub casho_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cashi_en_id.EditValueChanged
        Try
            Dim _filter As String

            _filter = "and bk_id in (SELECT  " _
                & "  bk_id " _
                & "FROM  " _
                & "  public.bk_mstr  " _
                & "WHERE  " _
                & "bk_ac_id in (SELECT  " _
                & "  enacc_ac_id " _
                & "FROM  " _
                & "  public.enacc_mstr  " _
                & "Where enacc_en_id=" & SetInteger(cashi_en_id.EditValue) & " and enacc_code='cash_in_header'))"

            If limit_account(cashi_en_id.EditValue) = True Then
                init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue, _filter)
            Else
                init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
            End If

            ' init_le(cashi_bk_id, "bk_mstr", cashi_en_id.EditValue)
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  cashi_oid, " _
            & "  cashi_dom_id, " _
            & "  cashi_en_id, " _
            & "  cashi_add_by, " _
            & "  cashi_add_date, " _
            & "  cashi_upd_by, " _
            & "  cashi_upd_date, " _
            & "  cashi_bk_id, " _
            & "  cashi_ptnr_id, " _
            & "  cashi_code, " _
            & "  cashi_date, " _
            & "  cashi_remarks, " _
            & "  cashi_reff, " _
            & "  cashi_cu_id, " _
            & "  cashi_exc_rate, " _
            & "  cashi_amount, " _
            & "  cashi_amount * cashi_exc_rate as cashi_amount_ext, " _
            & "  cashi_check_number, " _
            & "  cashi_post_dated_check, " _
            & "  cashid_oid, " _
            & "  cashid_cashi_oid, " _
            & "  cashid_ac_id, " _
            & "  cashid_amount, " _
            & "  cashid_amount * cashi_exc_rate as cashid_amount_ext, " _
            & "  cashid_remarks, " _
            & "  cashid_seq, " _
            & "  bk_name, " _
            & "  ptnr_name, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3 " _
            & "FROM  " _
            & "  cashi_in " _
            & "inner join cashid_detail on cashid_cashi_oid = cashi_oid " _
            & "inner join bk_mstr on bk_id = cashi_bk_id " _
            & "inner join ptnr_mstr on ptnr_id = cashi_ptnr_id " _
            & "inner join cu_mstr on cu_id = cashi_cu_id " _
            & "inner join ac_mstr on ac_id = cashid_ac_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = cashi_en_id" _
            & "  where cashi_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRCashInPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cashi_code")
        frm.ShowDialog()
    End Sub

    Public Overrides Sub smart_approve()
        Try


            '    set_lang(scc_master.Panel1)
            '    set_lang(xtc_detail)
            '    set_lang(LayoutControlGroup2)
            '    set_lang(gv_master)
            '    set_lang(gv_detail)
            '    set_lang(gv_edit)

            'MyBase.smart_approve()
            Dim ssql As String = ""
            ssql = "select '' as form, '' as control, '' as text"
            dt_control = master_new.PGSqlConn.GetTableData(ssql)

            dt_control.Rows.Clear()

            For Each ctrl In Me.Controls
                For Each obj_10 As Object In ctrl.controls
                    If TypeOf obj_10 Is DevExpress.XtraBars.Docking.ControlContainer Then
                        For Each obj_11 As Object In obj_10.controls
                            If TypeOf obj_11 Is DevExpress.XtraTab.XtraTabControl Then
                                For Each obj_12 As Object In obj_11.controls
                                    If TypeOf obj_12 Is DevExpress.XtraTab.XtraTabPage Then
                                        For Each obj_13 As Object In obj_12.controls
                                            If TypeOf obj_13 Is DevExpress.XtraEditors.SplitContainerControl Then
                                                For Each obj_14 As Object In obj_13.controls
                                                    For Each obj_15 As Object In obj_14.controls
                                                        For Each obj_16 As Object In obj_15.Views
                                                            If TypeOf obj_16 Is DevExpress.XtraGrid.Views.Grid.GridView Then
                                                                set_lang(CType(obj_16, DevExpress.XtraGrid.Views.Grid.GridView))
                                                                'set gv_detail

                                                            End If
                                                        Next
                                                    Next
                                                Next
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    End If
                    

                Next
                If TypeOf ctrl Is DevExpress.XtraEditors.SplitContainerControl Then
                    spc = CType(ctrl, DevExpress.XtraEditors.SplitContainerControl)
                    For Each ctrl_spc In spc.Controls
                        For Each obj_10 As Object In ctrl_spc.controls
                            set_lang(obj_10)
                            'set panel atas

                            'ada xtc master

                        Next
                        If TypeOf ctrl_spc Is DevExpress.XtraEditors.SplitGroupPanel Then
                            sgp = CType(ctrl_spc, DevExpress.XtraEditors.SplitGroupPanel)
                            For Each ctrl_sgp In sgp.Controls

                                If TypeOf ctrl_sgp Is DevExpress.XtraTab.XtraTabControl Then
                                    xtc = CType(ctrl_sgp, DevExpress.XtraTab.XtraTabControl)
                                    For Each ctrl_xtc In xtc.Controls

                                        For Each obj_10 As Object In ctrl_xtc.controls
                                            If TypeOf obj_10 Is DevExpress.XtraGrid.GridControl Then
                                                For Each obj_11 As Object In obj_10.Views
                                                    set_lang(CType(obj_11, DevExpress.XtraGrid.Views.Grid.GridView))
                                                    'set gv_master
                                                Next
                                            End If

                                        Next

                                        'gc_master
                                        'panel1
                                        'PanelControl2
                                        'PictureBox1
                                        'PanelControl1


                                        If TypeOf ctrl_xtc Is DevExpress.XtraTab.XtraTabPage Then
                                            If ctrl_xtc.name = "xtp_edit" Then
                                                xtp = CType(ctrl_xtc, DevExpress.XtraTab.XtraTabPage)
                                                For Each ctrl_xtp In xtp.Controls
                                                    If TypeOf ctrl_xtp Is Panel Then
                                                        pnl = CType(ctrl_xtp, Panel)
                                                        For Each ctrl_pnl In pnl.Controls
                                                            If TypeOf ctrl_pnl Is DevExpress.XtraLayout.LayoutControl Then

                                                                For Each obj_x As Object In ctrl_pnl.items
                                                                    If TypeOf obj_x Is DevExpress.XtraLayout.TabbedControlGroup Then
                                                                        For i As Integer = 0 To obj_x.TabPages.GroupCount - 1
                                                                            For Each obj As Object In obj_x.TabPages(i).Items

                                                                                If TypeOf obj Is DevExpress.XtraLayout.LayoutControlGroup Then
                                                                                    For Each obj_2 As Object In obj.items
                                                                                        If TypeOf obj_2 Is DevExpress.XtraLayout.LayoutControlItem Then


                                                                                        End If
                                                                                        If TypeOf obj_2 Is DevExpress.XtraLayout.TabbedControlGroup Then
                                                                                            For n As Integer = 0 To obj_2.TabPages.GroupCount - 1
                                                                                                For Each obj_3 As Object In obj_2.TabPages(n).Items
                                                                                                    If TypeOf obj_3 Is DevExpress.XtraLayout.TabbedControlGroup Then
                                                                                                        For m As Integer = 0 To obj_3.TabPages.GroupCount - 1
                                                                                                            For Each obj_4 As Object In obj_3.TabPages(m).Items
                                                                                                                If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlGroup Then
                                                                                                                    For Each obj_5 As Object In obj_4.items
                                                                                                                        If TypeOf obj_5 Is DevExpress.XtraLayout.LayoutControlItem Then

                                                                                                                        End If
                                                                                                                    Next
                                                                                                                End If
                                                                                                                If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlItem Then

                                                                                                                End If

                                                                                                            Next
                                                                                                        Next

                                                                                                    End If

                                                                                                    If TypeOf obj_3 Is DevExpress.XtraLayout.LayoutControlGroup Then
                                                                                                        For Each obj_4 As Object In obj_3.items
                                                                                                            If TypeOf obj_4 Is DevExpress.XtraLayout.LayoutControlItem Then

                                                                                                            End If
                                                                                                        Next
                                                                                                    End If
                                                                                                    If TypeOf obj_3 Is DevExpress.XtraLayout.LayoutControlItem Then

                                                                                                    End If
                                                                                                Next
                                                                                            Next
                                                                                        End If

                                                                                    Next
                                                                                End If
                                                                                If TypeOf obj Is DevExpress.XtraLayout.LayoutControlItem Then



                                                                                End If
                                                                            Next

                                                                        Next


                                                                    End If
                                                                Next
                                                                For Each ctrl_group As Object In ctrl_pnl.Controls


                                                                Next

                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If

                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next

            'Dim gcCredentials As New GridControl
            'Dim gvCredentials As New GridView

            'gcCredentials.ViewCollection.Add(gvCredentials)
            'gcCredentials.MainView = gvCredentials
            'gcCredentials.BindingContext = New BindingContext()

            'gcCredentials.DataSource = dt_control

            'gvCredentials.PopulateColumns()
            'gcCredentials.ForceInitialize()
            'Dim _file As String = ""
            '_file = AskSaveAsFile("Excel Files | *.xls")
            'If _file = "" Then
            '    Exit Sub
            'End If
            'gcCredentials.ExportToXls(_file)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        

    End Sub
   
    Private Sub cashi_so_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cashi_so_oid.ButtonClick
        Try
            Dim frm As New FSalesOrderSearch
            frm.set_win(Me)

            frm._en_id = cashi_en_id.EditValue
            frm._ptnr_id = __ptnr_id
            frm._obj = cashi_so_oid
            frm._cu_id = cashi_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cashi_reff_oid_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cashi_reff_oid.ButtonClick
        Try
            If __ptnr_id = "" Then
                Box("Please fill partner field")
                Exit Sub
            End If
            Dim frm As New FCashOutSearch
            frm.set_win(Me)

            frm._ptnr_id = __ptnr_id
            frm._obj = cashi_reff_oid
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cashi_reverse_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cashi_reverse.CheckedChanged
        If SetString(cashi_reverse.Checked) = True Then
            cashi_reff_oid.Enabled = True
        Else
            cashi_reff_oid.Enabled = False
        End If
    End Sub


    Private Sub cashi_invest_code_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles cashi_invest_code.ButtonClick
        Try
            Dim frm As New FInvestmentSearch
            frm.set_win(Me)
            frm._en_id = cashi_en_id.EditValue
            frm._ptnr_id = __ptnr_id
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub
End Class
