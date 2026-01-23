
Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FTransfer
    Dim _bom_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public __bank_from As String
    Public __bank_to As String

    Private Sub FWarehouse_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = CekTanggal()
        pr_txttglakhir.DateTime = CekTanggal()
    End Sub

    Public Overrides Sub load_cb()
        init_le(casht_en, "en_mstr")
        init_le(casht_cu_id, "cu_mstr")
        init_le(casht_en_to, "en_id")
    End Sub

    Private Sub casht_en_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles casht_en.EditValueChanged

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
            & "Where enacc_en_id=" & SetInteger(casht_en.EditValue) & " and enacc_code='transfer_bank_from'))"

        If limit_account(casht_en.EditValue) = True Then
            init_le(casht_bk_id_from, "bk_mstr", casht_en.EditValue, _filter)
        Else
            init_le(casht_bk_id_from, "bk_mstr", casht_en.EditValue)
        End If

        If SetString(casht_bk_id_to.EditValue) <> "" Then
            _filter = "and bk_id in (SELECT  " _
               & "  bk_id " _
               & "FROM  " _
               & "  public.bk_mstr  " _
               & "WHERE  " _
               & "bk_ac_id in (SELECT  " _
               & "  enacc_ac_id " _
               & "FROM  " _
               & "  public.enacc_mstr  " _
               & "Where enacc_en_id=" & SetInteger(casht_en_to.EditValue) & " and enacc_code='transfer_bank_to'))"

            If limit_account(casht_en.EditValue) = True Then
                init_le(casht_bk_id_to, "bk_mstr", casht_en_to.EditValue, _filter)
            Else
                init_le(casht_bk_id_to, "bk_mstr", casht_en_to.EditValue)
            End If
        End If
        
    End Sub

    Private Sub casht_en_to_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles casht_en_to.EditValueChanged
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
            & "Where enacc_en_id=" & SetInteger(casht_en_to.EditValue) & " and enacc_code='transfer_bank_to'))"

        If limit_account(casht_en.EditValue) = True Then
            init_le(casht_bk_id_to, "bk_mstr", casht_en_to.EditValue, _filter)
        Else
            init_le(casht_bk_id_to, "bk_mstr", casht_en_to.EditValue)
        End If

    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity From", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "casht_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "casht_date", DevExpress.Utils.HorzAlignment.Default)
        'casht_date
        add_column_copy(gv_master, "Bank from", "bk_name_from", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity To", "en_desc_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank to", "bk_name_to", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "casht_exch_rate", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "casht_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Reff", "casht_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "casht_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column_copy(gv_master, "User Create", "casht_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "casht_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "casht_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "casht_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.casht_oid, " _
                & "  a.casht_en,casht_en_to, " _
                & "  b.en_desc, " _
                & "  a.casht_bk_id_from, " _
                & "  c.bk_name AS bk_name_from, " _
                & "  a.casht_cu_id, " _
                & "  e.cu_name, " _
                & "  a.casht_exch_rate, " _
                & "  a.casht_remarks, " _
                & "  a.casht_bk_id_to, " _
                & "  d.bk_name AS bk_name_to,f.en_desc as en_desc_to, " _
                & "  a.casht_reff, " _
                & "  a.casht_amount, " _
                & "  a.casht_code, " _
                & "  a.casht_date, " _
                & "  a.casht_add_by, " _
                & "  a.casht_add_date, " _
                & "  a.casht_upd_by, " _
                & "  a.casht_upd_date " _
                & "FROM " _
                & "  public.casht_transfer a " _
                & "  INNER JOIN public.en_mstr b ON (a.casht_en = b.en_id) " _
                & "  INNER JOIN public.bk_mstr c ON (a.casht_bk_id_from = c.bk_id) " _
                & "  INNER JOIN public.bk_mstr d ON (a.casht_bk_id_to = d.bk_id) " _
                & "  INNER JOIN public.cu_mstr e ON (a.casht_cu_id = e.cu_id) " _
                & "  LEFT OUTER JOIN public.en_mstr f ON (a.casht_en_to = f.en_id) " _
                & " where casht_en in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " and casht_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & "  order by casht_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        casht_en.Focus()
        casht_en.ItemIndex = 0
        casht_code.Text = ""
        casht_remarks.Text = ""
        casht_bk_id_from.ItemIndex = 0
        casht_bk_id_to.ItemIndex = 0
        casht_date.DateTime = CekTanggal()
        casht_cu_id.ItemIndex = 0
        casht_exch_rate.EditValue = 1.0
        casht_reff.EditValue = ""
        casht_en_to.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim sSQL As String
        Dim ssqls As New ArrayList

        casht_code.Text = GetNewNumberYM("casht_transfer", "casht_code", 5, "TRF" & casht_en.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)



        Try
            sSQL = "INSERT INTO  " _
                & "  public.casht_transfer " _
                & "( " _
                & "  casht_oid, " _
                & "  casht_dom, " _
                & "  casht_en, " _
                & "  casht_add_by, " _
                & "  casht_add_date, " _
                & "  casht_bk_id_from, " _
                & "  casht_cu_id, " _
                & "  casht_exch_rate, " _
                & "  casht_remarks,casht_en_to, " _
                & "  casht_bk_id_to, " _
                & "  casht_reff, " _
                & "  casht_amount, " _
                & "  casht_code, " _
                & "  casht_date " _
                & ")  " _
                & "VALUES ( " _
                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                & SetInteger(casht_en.EditValue) & ",  " _
                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                & SetDateNTime00(CekTanggal) & ",  " _
                & SetInteger(casht_bk_id_from.EditValue) & ",  " _
                & SetInteger(casht_cu_id.EditValue) & ",  " _
                & SetDbl(casht_exch_rate.EditValue) & ",  " _
                & SetSetring(casht_remarks.Text) & ",  " _
                & SetInteger(casht_en_to.EditValue) & ",  " _
                & SetInteger(casht_bk_id_to.EditValue) & ",  " _
                & SetSetring(casht_reff.Text) & ",  " _
                & SetDbl(casht_amount.Text) & ",  " _
                & SetSetring(casht_code.Text) & ",  " _
                & SetDateNTime(casht_date.DateTime) & "  " _
                & ")"


            ssqls.Add(sSQL)

            'update rekonsiliasi kas keluar
            If update_rec(ssqls, casht_en.EditValue, casht_bk_id_from.EditValue, casht_cu_id.EditValue, _
                          casht_exch_rate.EditValue, casht_amount.EditValue * -1, casht_date.DateTime, casht_code.Text, _
                          "Transfer to " & casht_bk_id_to.GetColumnValue("bk_name") & ", " & casht_remarks.Text, "TRANSFER") = False Then
                Return False
                Exit Function
            End If

            'update rekonsiliasi kas masuk
            If update_rec(ssqls, casht_en.EditValue, casht_bk_id_to.EditValue, casht_cu_id.EditValue, _
                         casht_exch_rate.EditValue, casht_amount.EditValue, casht_date.DateTime, casht_code.Text, _
                         "Transfer from " & casht_bk_id_from.GetColumnValue("bk_name") & ", " & casht_remarks.Text, "TRANSFER") = False Then
                Return False
                Exit Function
            End If

            'jurnal
            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

            If _create_jurnal = True Then

               

                If casht_en.EditValue = casht_en_to.EditValue Then
                    'insert dulu debetnya
                    Dim _ac_id_kredit, _ac_id_debit As String

                    _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", casht_bk_id_to.EditValue)
                    _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", casht_bk_id_from.EditValue)


                    Dim _glt_code As String
                    '_glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                    '       & casht_en.GetColumnValue("en_code") & (CekTanggal().ToString("yyMM")))

                    _glt_code = func_coll.get_transaction_number("CT", casht_en.GetColumnValue("en_code"), "glt_det", "glt_code")


                    If insert_gl(ssqls, _glt_code, _ac_id_debit, casht_amount.EditValue, 0, _
                               master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, _
                               casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                               "CT", 1, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_debit, casht_amount.EditValue, _
                                    master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, casht_date.DateTime, _
                                    casht_cu_id.EditValue, casht_exch_rate.EditValue, "D") = False Then
                        Return False
                        Exit Function
                    End If

                    'insert GL kredit
                    If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, casht_amount.EditValue, _
                                 master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, _
                                 casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                                 "CT", 2, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_kredit, casht_amount.EditValue, master_new.ClsVar.sdom_id, _
                                casht_en.EditValue, 0, 0, casht_date.DateTime, casht_cu_id.EditValue, _
                                casht_exch_rate.EditValue, "C") = False Then
                        Return False
                        Exit Function
                    End If

                Else

                    Dim _ac_id_kredit, _ac_id_debit, _ac_id_ayat_silang As String

                    _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", casht_bk_id_to.EditValue)
                    _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", casht_bk_id_from.EditValue)
                    _ac_id_ayat_silang = GetIDByName("conf_file", "conf_value", "conf_name", "temp_transfer_ac_id")

                    Dim _glt_code As String
                    '_glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                    '       & casht_en.GetColumnValue("en_code") & (CekTanggal().ToString("yyMM")))

                    _glt_code = func_coll.get_transaction_number("CT", casht_en.GetColumnValue("en_code"), "glt_det", "glt_code")

                    If insert_gl(ssqls, _glt_code, _ac_id_debit, casht_amount.EditValue, 0, _
                               master_new.ClsVar.sdom_id, casht_en_to.EditValue, 0, 0, _
                               casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                               "CT", 1, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_debit, casht_amount.EditValue, _
                                    master_new.ClsVar.sdom_id, casht_en_to.EditValue, 0, 0, casht_date.DateTime, _
                                    casht_cu_id.EditValue, casht_exch_rate.EditValue, "D") = False Then
                        Return False
                        Exit Function
                    End If


                    'insert GL kredit
                    If insert_gl(ssqls, _glt_code, _ac_id_ayat_silang, 0, casht_amount.EditValue, _
                                 master_new.ClsVar.sdom_id, casht_en_to.EditValue, 0, 0, _
                                 casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                                 "CT", 2, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_ayat_silang, casht_amount.EditValue, master_new.ClsVar.sdom_id, _
                                casht_en_to.EditValue, 0, 0, casht_date.DateTime, casht_cu_id.EditValue, _
                                casht_exch_rate.EditValue, "C") = False Then
                        Return False
                        Exit Function
                    End If

                    'entity asal
                    If insert_gl(ssqls, _glt_code, _ac_id_ayat_silang, casht_amount.EditValue, 0, _
                              master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, _
                              casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                              "CT", 3, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_ayat_silang, casht_amount.EditValue, _
                                    master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, casht_date.DateTime, _
                                    casht_cu_id.EditValue, casht_exch_rate.EditValue, "D") = False Then
                        Return False
                        Exit Function
                    End If

                    'insert GL kredit
                    If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, casht_amount.EditValue, _
                                 master_new.ClsVar.sdom_id, casht_en.EditValue, 0, 0, _
                                 casht_date.DateTime, casht_cu_id.EditValue, casht_exch_rate.EditValue, _
                                 "CT", 4, casht_remarks.EditValue, "", casht_code.EditValue, "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    If update_glbal(ssqls, _ac_id_kredit, casht_amount.EditValue, master_new.ClsVar.sdom_id, _
                                casht_en.EditValue, 0, 0, casht_date.DateTime, casht_cu_id.EditValue, _
                                casht_exch_rate.EditValue, "C") = False Then
                        Return False
                        Exit Function
                    End If

                End If

                

            End If


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
            insert = True


        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function
    Public Overrides Function before_save() As Boolean
        Try
            Dim _gcald_det_status As String = func_data.get_gcald_det_status(casht_en.EditValue, "gcald_ap", casht_date.DateTime)

            If _gcald_det_status = "" Then
                MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + casht_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            ElseIf _gcald_det_status.ToUpper = "Y" Then
                MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + casht_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If required(casht_en, "Entity") = False Then
                Return False
                Exit Function
            End If

            If required(casht_bk_id_from, "Bank From") = False Then
                Return False
                Exit Function
            End If

            If required(casht_bk_id_to, "Bank To") = False Then
                Return False
                Exit Function
            End If

            If required(casht_exch_rate, "Exchange Rate") = False Then
                Return False
                Exit Function
            End If

            If required(casht_amount, "Amount") = False Then
                Return False
                Exit Function
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Overrides Function edit_data() As Boolean
        Return False
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _bom_oid_mstr = .Item("bom_oid").ToString
                casht_en.EditValue = .Item("bom_en_id")
                casht_code.Text = .Item("bom_code")
                casht_remarks.Text = .Item("bom_desc")
                casht_cu_id.EditValue = .Item("bom_um_id")
                'bom_active.EditValue = SetBitYNB(.Item("bom_active"))
                casht_bk_id_from.EditValue = .Item("pt_desc1")
                '__bom_pt_id = .Item("bom_pt_id")

            End With
            casht_en.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text

                        '.Command.CommandText = "UPDATE  " _
                        '                    & "  public.bom_mstr   " _
                        '                    & "SET  " _
                        '                    & "  bom_en_id = " & SetInteger(bom_en_id.EditValue) & ",  " _
                        '                    & "  bom_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                        '                    & "  bom_upd_date = " & SetDateNTime(CekTanggal) & ",  " _
                        '                    & "  bom_pt_id = " & SetInteger(__bom_pt_id) & ",  " _
                        '                    & "  bom_code = " & SetSetring(bom_code.Text) & ",  " _
                        '                    & "  bom_desc = " & SetSetring(bom_desc.Text) & ",  " _
                        '                    & "  bom_um_id = " & SetInteger(bom_um_id.EditValue) & ",  " _
                        '                    & "  bom_active = " & SetBitYN(bom_active.EditValue) & "  " _
                        '                    & "  " _
                        '                    & "WHERE  " _
                        '                    & "  bom_oid = " & SetSetring(_bom_oid_mstr) & " "

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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Dim _casht_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("casht_date")
        Dim _casht_en As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("casht_en")
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(_casht_en, "gcald_ap", _casht_date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _casht_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _casht_date.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
    End Function
    Public Overrides Function delete_data() As Boolean
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
                        & "  public.casht_transfer  " _
                        & "WHERE  " _
                        & "  casht_oid ='" & .Item("casht_oid").ToString & "'"

                    ssqls.Add(sSQL)

                    'update rekonsiliasi kas keluar
                    If update_rec(ssqls, .Item("casht_en"), .Item("casht_bk_id_from"), .Item("casht_cu_id"), _
                                  .Item("casht_exch_rate"), .Item("casht_amount"), .Item("casht_date"), .Item("casht_code"), _
                                  "Delete transfer to " & .Item("bk_name_from") & ", " & .Item("casht_remarks"), "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If

                    'update rekonsiliasi kas masuk
                    If update_rec(ssqls, .Item("casht_en"), .Item("casht_bk_id_to"), .Item("casht_cu_id"), _
                                 .Item("casht_exch_rate"), .Item("casht_amount") * -1, .Item("casht_date"), .Item("casht_code"), _
                                 "Delete transfer from " & .Item("bk_name_to") & ", " & .Item("casht_remarks"), "TRANSFER") = False Then
                        Return False
                        Exit Function
                    End If


                    'jurnal balik
                    Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

                    If _create_jurnal = True Then

                        If SetString(.Item("casht_en_to")) = "" Then
                            'insert dulu debetnya
                            Dim _ac_id_kredit, _ac_id_debit As String

                            _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_from"))
                            _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_to"))

                            Dim _glt_code As String
                            _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                                   & .Item("casht_en") & (CekTanggal().ToString("yyMM")))

                            If insert_gl(ssqls, _glt_code, _ac_id_debit, .Item("casht_amount"), 0, _
                                       master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                       .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                       "CT", 1, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                Return False
                                Exit Function
                            End If

                            If update_glbal(ssqls, _ac_id_debit, .Item("casht_amount"), _
                                            master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, .Item("casht_date"), _
                                            .Item("casht_cu_id"), .Item("casht_exch_rate"), "D") = False Then
                                Return False
                                Exit Function
                            End If

                            'insert GL kredit
                            If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, .Item("casht_amount"), _
                                         master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                         .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                         "CT", 2, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                Return False
                                Exit Function
                            End If

                            If update_glbal(ssqls, _ac_id_kredit, .Item("casht_amount"), master_new.ClsVar.sdom_id, _
                                            .Item("casht_en"), 0, 0, .Item("casht_date"), .Item("casht_cu_id"), _
                                            .Item("casht_exch_rate"), "C") = False Then
                                Return False
                                Exit Function
                            End If
                        Else
                            'baru transaksinya

                            If SetString(.Item("casht_en")) = SetString(.Item("casht_en_to")) Then
                                'insert dulu debetnya
                                Dim _ac_id_kredit, _ac_id_debit As String

                                _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_from"))
                                _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_to"))

                                Dim _glt_code As String
                                _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                                       & .Item("casht_en") & (CekTanggal().ToString("yyMM")))

                                If insert_gl(ssqls, _glt_code, _ac_id_debit, .Item("casht_amount"), 0, _
                                           master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                           .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                           "CT", 1, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_debit, .Item("casht_amount"), _
                                                master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, .Item("casht_date"), _
                                                .Item("casht_cu_id"), .Item("casht_exch_rate"), "D") = False Then
                                    Return False
                                    Exit Function
                                End If

                                'insert GL kredit
                                If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, .Item("casht_amount"), _
                                             master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                             .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                             "CT", 2, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_kredit, .Item("casht_amount"), master_new.ClsVar.sdom_id, _
                                                .Item("casht_en"), 0, 0, .Item("casht_date"), .Item("casht_cu_id"), _
                                                .Item("casht_exch_rate"), "C") = False Then
                                    Return False
                                    Exit Function
                                End If
                            Else
                                'insert dulu debetnya
                                Dim _ac_id_kredit, _ac_id_debit, _ac_id_ayat_silang As String

                                _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_from"))
                                _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_to"))
                                _ac_id_ayat_silang = GetIDByName("conf_file", "conf_value", "conf_name", "temp_transfer_ac_id")

                                Dim _glt_code As String
                                _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                                       & .Item("casht_en") & (CekTanggal().ToString("yyMM")))

                                If insert_gl(ssqls, _glt_code, _ac_id_debit, .Item("casht_amount"), 0, _
                                           master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                           .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                           "CT", 1, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_debit, .Item("casht_amount"), _
                                                master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, .Item("casht_date"), _
                                                .Item("casht_cu_id"), .Item("casht_exch_rate"), "D") = False Then
                                    Return False
                                    Exit Function
                                End If


                                'insert GL kredit
                                If insert_gl(ssqls, _glt_code, _ac_id_ayat_silang, 0, .Item("casht_amount"), _
                                             master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                                             .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                             "CT", 2, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_ayat_silang, .Item("casht_amount"), master_new.ClsVar.sdom_id, _
                                            .Item("casht_en"), 0, 0, .Item("casht_date"), .Item("casht_cu_id"), _
                                            .Item("casht_exch_rate"), "C") = False Then
                                    Return False
                                    Exit Function
                                End If

                                'entity asal
                                If insert_gl(ssqls, _glt_code, _ac_id_ayat_silang, .Item("casht_amount"), 0, _
                                          master_new.ClsVar.sdom_id, .Item("casht_en_to"), 0, 0, _
                                          .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                          "CT", 3, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_ayat_silang, .Item("casht_amount"), _
                                                master_new.ClsVar.sdom_id, .Item("casht_en_to"), 0, 0, .Item("casht_date"), _
                                                .Item("casht_cu_id"), .Item("casht_exch_rate"), "D") = False Then
                                    Return False
                                    Exit Function
                                End If


                                'insert GL kredit
                                If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, .Item("casht_amount"), _
                                             master_new.ClsVar.sdom_id, .Item("casht_en_to"), 0, 0, _
                                             .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                                             "CT", 4, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                                    Return False
                                    Exit Function
                                End If

                                If update_glbal(ssqls, _ac_id_kredit, .Item("casht_amount"), master_new.ClsVar.sdom_id, _
                                                .Item("casht_en_to"), 0, 0, .Item("casht_date"), .Item("casht_cu_id"), _
                                                .Item("casht_exch_rate"), "C") = False Then
                                    Return False
                                    Exit Function
                                End If
                            End If
                        End If


                    End If

                End With

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

                help_load_data(True)
                MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)


            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

End Class

