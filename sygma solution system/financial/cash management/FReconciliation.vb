
Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FReconciliation
    Dim _oid_mstr As String
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
    Public Overrides Sub cek_page()
        If xtc_master.SelectedTabPageIndex = 0 Then
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        End If
    End Sub

    Public Overrides Sub load_cb()
        init_le(recm_en_id, "en_mstr")
        init_le(recm_cu_id, "cu_mstr")
    End Sub
    Private Sub casht_en_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles recm_en_id.EditValueChanged
        init_le(recm_bk_id, "bk_mstr", recm_en_id.EditValue)
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "recm_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ending Bank", "recm_ending_bank", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Beginning", "recm_beginning", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Change", "recm_changes", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ending", "recm_ending", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Start Date", "recm_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End date", "recm_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Beginning", "recm_is_beginning", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "recm_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "recm_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "recm_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "recm_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "recm_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


        add_column(gv_detail, "recd_oid", False)
        add_column_copy(gv_detail, "Date", "recd_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Type", "recd_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Reff", "recd_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Remark", "recd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Amount", "recd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_detail, "Amount Summ", "recd_amount_summ", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

        add_column(gv_rec, "recd_oid", False)
        add_column_copy(gv_rec, "Date", "recd_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_rec, "Type", "recd_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_rec, "Reff", "recd_reff", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_rec, "Remark", "recd_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_rec, "Amount", "recd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")


    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.recm_oid, " _
                & "  a.recm_code, " _
                & "  a.recm_date, " _
                & "  c.en_desc, " _
                & "  a.recm_bk_id, " _
                & "  b.bk_name, " _
                & "  a.recm_beginning, " _
                & "  a.recm_changes, " _
                & "  a.recm_ending, " _
                & "  a.recm_start_date, " _
                & "  a.recm_end_date, " _
                & "  a.recm_is_beginning, " _
                & "  a.recm_ending_bank, " _
                & "  a.recm_add_by, " _
                & "  a.recm_add_date, " _
                & "  a.recm_upd_by, " _
                & "  a.recm_upd_date, " _
                & "  a.recm_remarks " _
                & "FROM " _
                & "  public.reconciliation_mstr a " _
                & "  INNER JOIN public.bk_mstr b ON (a.recm_bk_id = b.bk_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.recm_en_id = c.en_id) " _
                & " where recm_en_id in (select user_en_id from tconfuserentity " _
                & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                & " and recm_date between " & SetDateNTime00(pr_txttglawal.DateTime) & " and " & SetDateNTime00(pr_txttglakhir.DateTime) _
                & "ORDER BY " _
                & "  a.recm_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        recm_en_id.Focus()
        recm_en_id.ItemIndex = 0
        recm_code.Text = ""
        recm_date.DateTime = CekTanggal()
        recm_start_date.DateTime = CekTanggal()
        recm_end_date.DateTime = CekTanggal()
        recm_remarks.Text = ""
        recm_bk_id.ItemIndex = 0
        recm_cu_id.ItemIndex = 0
        recm_exc_rate.EditValue = 1.0
        recm_changes.EditValue = 0.0
        recm_beginning.EditValue = 0.0
        recm_ending.EditValue = 0.0
        recm_ending_bank.EditValue = 0.0
        recm_is_beginning.EditValue = False
        gc_rec.DataSource = Nothing


    End Sub

    Public Overrides Function insert() As Boolean
        Dim sSQL As String
        Dim ssqls As New ArrayList

        recm_code.Text = GetNewNumberYM("reconciliation_mstr", "recm_code", 5, "REC" & recm_en_id.GetColumnValue("en_code") _
                                       & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)
        _oid_mstr = Guid.NewGuid.ToString

        Try


            If recm_is_beginning.EditValue = True Then
                sSQL = "INSERT INTO  " _
                    & "  public.reconciliation_mstr " _
                    & "( " _
                    & "  recm_oid, " _
                    & "  recm_code, " _
                    & "  recm_date, " _
                    & "  recm_add_by, " _
                    & "  recm_add_date, " _
                    & "  recm_ending, " _
                    & "  recm_start_date, " _
                    & "  recm_end_date, " _
                    & "  recm_is_beginning, " _
                    & "  recm_bk_id, " _
                    & "  recm_en_id, " _
                    & "  recm_dom_id, " _
                    & "  recm_remarks, " _
                    & "  recm_cu_id, " _
                    & "  recm_exc_rate " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_oid_mstr) & ",  " _
                    & SetSetring(recm_code.Text) & ",  " _
                    & SetDateNTime00(recm_date.DateTime) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & SetDateNTime(CekTanggal) & ",  " _
                    & SetDec(recm_ending.EditValue) & ",  " _
                    & SetDateNTime00(recm_start_date.DateTime) & ",  " _
                    & SetDateNTime00(recm_end_date.DateTime) & ",  " _
                    & SetBitYN(recm_is_beginning.EditValue) & ",  " _
                    & SetInteger(recm_bk_id.EditValue) & ",  " _
                    & SetInteger(recm_en_id.EditValue) & ",  " _
                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                    & SetSetring(recm_remarks.Text) & ",  " _
                    & SetInteger(recm_cu_id.EditValue) & ",  " _
                    & SetDec(recm_exc_rate.EditValue) & "  " _
                    & ")"

                ssqls.Add(sSQL)
            Else

                If gv_rec.RowCount = 0 Then
                    Box("Data Detail can't blank")
                    Return False
                    Exit Function
                End If

                If System.Math.Round(recm_ending.EditValue, 2) <> System.Math.Round(recm_ending_bank.EditValue, 2) Then
                    Box("Ending Transaction is not balance with ending bank")
                    Return False
                    Exit Function
                End If


                sSQL = "INSERT INTO  " _
                    & "  public.reconciliation_mstr " _
                    & "( " _
                    & "  recm_oid, " _
                    & "  recm_code, " _
                    & "  recm_date, " _
                    & "  recm_add_by, " _
                    & "  recm_add_date, " _
                    & "  recm_beginning, " _
                    & "  recm_changes, " _
                    & "  recm_ending, " _
                    & "  recm_start_date, " _
                    & "  recm_end_date, " _
                    & "  recm_is_beginning, " _
                    & "  recm_ending_bank, " _
                    & "  recm_bk_id, " _
                    & "  recm_en_id, " _
                    & "  recm_dom_id, " _
                    & "  recm_remarks, " _
                    & "  recm_cu_id, " _
                    & "  recm_exc_rate " _
                    & ")  " _
                    & "VALUES ( " _
                    & SetSetring(_oid_mstr) & ",  " _
                    & SetSetring(recm_code.Text) & ",  " _
                    & SetDate(recm_date.DateTime) & ",  " _
                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                    & SetDateNTime00(CekTanggal) & ",  " _
                    & SetDec(recm_beginning.EditValue) & ",  " _
                    & SetDec(recm_changes.EditValue) & ",  " _
                    & SetDec(recm_ending.EditValue) & ",  " _
                    & SetDateNTime00(recm_start_date.DateTime) & ",  " _
                    & SetDateNTime00(recm_end_date.DateTime) & ",  " _
                    & SetBitYN(recm_is_beginning.EditValue) & ",  " _
                    & SetDec(recm_ending_bank.EditValue) & ",  " _
                    & SetInteger(recm_bk_id.EditValue) & ",  " _
                    & SetInteger(recm_en_id.EditValue) & ",  " _
                    & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                    & SetSetring(recm_remarks.Text) & ",  " _
                    & SetInteger(recm_cu_id.EditValue) & ",  " _
                    & SetDec(recm_exc_rate.EditValue) & "  " _
                    & ")"

                ssqls.Add(sSQL)

                For i As Integer = 0 To gv_rec.RowCount - 1
                    sSQL = "update reconciliation_det set recd_code_mstr=" _
                        & SetSetring(recm_code.Text) & " where recd_oid='" _
                        & gv_rec.GetRowCellValue(i, "recd_oid").ToString & "'"

                    ssqls.Add(sSQL)
                Next


            End If

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
            If required(recm_en_id, "Entity") = False Then
                Return False
                Exit Function
            End If

            If required(recm_bk_id, "Bank From") = False Then
                Return False
                Exit Function
            End If

            If required(recm_exc_rate, "Exchange Rate") = False Then
                Return False
                Exit Function
            End If


            If required(recm_ending, "Amount Ending Reconciliation") = False Then
                Return False
                Exit Function
            End If

            If recm_is_beginning.EditValue = False Then

                If required(recm_beginning, "Beginning reconciliation ") = False Then
                    Return False
                    Exit Function
                End If

                If required(recm_changes, "Amount Changes reconciliation") = False Then
                    Return False
                    Exit Function
                End If



                If required(recm_ending_bank, "Amount Ending Bank") = False Then
                    Return False
                    Exit Function
                End If

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Overrides Function edit_data() As Boolean

        If MyBase.edit_data = True Then
            'row = BindingContext(ds.Tables(0)).Position

            'With ds.Tables(0).Rows(row)
            '    '_bom_oid_mstr = .Item("bom_oid")
            '    recm_en_id.EditValue = .Item("bom_en_id")
            '    recm_code.Text = .Item("bom_code")
            '    recm_remarks.Text = .Item("bom_desc")
            '    recm_cu_id.EditValue = .Item("bom_um_id")
            '    'bom_active.EditValue = SetBitYNB(.Item("bom_active"))
            '    recm_bk_id.EditValue = .Item("pt_desc1")
            '    '__bom_pt_id = .Item("bom_pt_id")

            'End With
            'recm_en_id.Focus()
            'edit_data = True

            Return False
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
                        & "  public.reconciliation_mstr  " _
                        & "WHERE  " _
                        & "  recm_oid ='" & .Item("recm_oid").ToString & "'"

                    ssqls.Add(sSQL)

                    For i As Integer = 0 To gv_detail.RowCount - 1
                        sSQL = "update reconciliation_det set recd_code_mstr = null where recd_oid='" _
                            & gv_detail.GetRowCellValue(i, "recd_oid").ToString & "'"

                        ssqls.Add(sSQL)
                    Next

                    ''update rekonsiliasi kas keluar
                    'If update_rec(ssqls, .Item("casht_en"), .Item("casht_bk_id_from"), .Item("casht_cu_id"), _
                    '              .Item("casht_exch_rate"), .Item("casht_amount"), .Item("casht_date"), .Item("casht_code"), _
                    '              "Delete transfer to " & .Item("bk_name_from") & ", " & .Item("casht_remarks"), "TRANSFER") = False Then
                    '    Return False
                    '    Exit Function
                    'End If

                    ''update rekonsiliasi kas masuk
                    'If update_rec(ssqls, .Item("casht_en"), .Item("casht_bk_id_to"), .Item("casht_cu_id"), _
                    '             .Item("casht_exch_rate"), .Item("casht_amount") * -1, .Item("casht_date"), .Item("casht_code"), _
                    '             "Delete transfer from " & .Item("bk_name_to") & ", " & .Item("casht_remarks"), "TRANSFER") = False Then
                    '    Return False
                    '    Exit Function
                    'End If


                    ''jurnal balik
                    'Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

                    'If _create_jurnal = True Then

                    '    'insert dulu debetnya
                    '    Dim _ac_id_kredit, _ac_id_debit As String

                    '    _ac_id_debit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_from"))
                    '    _ac_id_kredit = GetIDByName("bk_mstr", "bk_ac_id", "bk_id", .Item("casht_bk_id_to"))

                    '    Dim _glt_code As String
                    '    _glt_code = GetNewNumberYM("glt_det", "glt_code", 7, "CT" _
                    '           & .Item("casht_en") & (CekTanggal().ToString("yyMM")))

                    '    If insert_gl(ssqls, _glt_code, _ac_id_debit, .Item("casht_amount"), 0, _
                    '               master_new.ClsVar.sdom_id, recm_en_id.EditValue, 0, 0, _
                    '               .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                    '               "CT", 1, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                    '        Return False
                    '        Exit Function
                    '    End If

                    '    If update_glbal(ssqls, _ac_id_debit, .Item("casht_amount"), _
                    '                    master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, .Item("casht_date"), _
                    '                    .Item("casht_cu_id"), .Item("casht_exch_rate"), "D") = False Then
                    '        Return False
                    '        Exit Function
                    '    End If

                    '    'insert GL kredit
                    '    If insert_gl(ssqls, _glt_code, _ac_id_kredit, 0, .Item("casht_amount"), _
                    '                 master_new.ClsVar.sdom_id, .Item("casht_en"), 0, 0, _
                    '                 .Item("casht_date"), .Item("casht_cu_id"), .Item("casht_exch_rate"), _
                    '                 "CT", 2, "Delete " & .Item("casht_remarks"), "", .Item("casht_code"), "TRANSFER") = False Then
                    '        Return False
                    '        Exit Function
                    '    End If

                    '    If update_glbal(ssqls, _ac_id_kredit, .Item("casht_amount"), master_new.ClsVar.sdom_id, _
                    '                    .Item("casht_en"), 0, 0, .Item("casht_date"), .Item("casht_cu_id"), _
                    '                    .Item("casht_exch_rate"), "C") = False Then
                    '        Return False
                    '        Exit Function
                    '    End If

                    'End If

                End With

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

    Private Sub BTLoadData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTLoadData.Click
        Dim sSQL As String
        Try
            If recm_end_date.DateTime < recm_start_date.DateTime Then
                Box("End date can't lower than start date")
                gc_rec.DataSource = Nothing
                Exit Sub
            End If

            sSQL = "SELECT  " _
                & "  a.recd_oid, " _
                & "  a.recd_en_id, " _
                & "  d.en_desc, " _
                & "  a.recd_bk_id, " _
                & "  b.bk_name, " _
                & "  a.recd_cu_id, " _
                & "  c.cu_name, " _
                & "  a.recd_ex_rate, " _
                & "  a.recd_amount, " _
                & "  a.recd_date, " _
                & "  a.recd_reff, " _
                & "  a.recd_remarks, " _
                & "  a.recd_type, " _
                & "  a.recd_ex_rate_ext, " _
                & "  a.recd_add_by, " _
                & "  a.recd_add_date, " _
                & "  a.recd_upd_by, " _
                & "  a.recd_upd_date " _
                & "FROM " _
                & "  public.reconciliation_det a " _
                & "  INNER JOIN public.bk_mstr b ON (a.recd_bk_id = b.bk_id) " _
                & "  INNER JOIN public.cu_mstr c ON (a.recd_cu_id = c.cu_id) " _
                & "  INNER JOIN public.en_mstr d ON (a.recd_en_id = d.en_id) " _
                & " Where a.recd_bk_id=" & recm_bk_id.EditValue _
                & " and  recd_date between " & SetDateNTime00(recm_start_date.DateTime) & " and " & SetDateNTime00(recm_end_date.DateTime) _
                & "   ORDER BY " _
                & " a.recd_date "


            gc_rec.DataSource = GetTableData(sSQL)
            gv_rec.BestFitColumns()


            Dim _total As Double = 0
            For i As Integer = 0 To gv_rec.RowCount - 1
                _total += gv_rec.GetRowCellValue(i, "recd_amount")
            Next

            recm_changes.EditValue = _total
            recm_ending.EditValue = recm_beginning.EditValue + recm_changes.EditValue

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub recm_is_beginning_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles recm_is_beginning.EditValueChanged
        Try
            If recm_is_beginning.EditValue = False Then
                recm_ending.Enabled = False
                recm_ending.EditValue = 0.0
            Else
                recm_ending.Enabled = True
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub recm_bk_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles recm_bk_id.EditValueChanged
        Dim sSQL As String = ""
        Try
            If recm_is_beginning.EditValue = False Then
                sSQL = "select * from reconciliation_mstr where recm_code= (select max(recm_code) from reconciliation_mstr where recm_bk_id=" & recm_bk_id.EditValue & ")"
                Dim dr_recon As DataRow
                dr_recon = GetRowInfo(sSQL)
                If dr_recon Is Nothing Then
                    recm_start_date.DateTime = CekTanggal()
                    recm_start_date.Enabled = True
                    recm_beginning.EditValue = 0
                    recm_beginning.Enabled = True
                Else
                    recm_start_date.DateTime = DateAdd(DateInterval.Day, 1, dr_recon("recm_end_date"))
                    recm_start_date.Enabled = False
                    recm_beginning.EditValue = dr_recon("recm_ending")
                    recm_beginning.Enabled = False
                End If
              
            End If


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Dim sSQL As String
        Try
            sSQL = "SELECT  " _
              & "  a.recd_oid, " _
              & "  a.recd_en_id, " _
              & "  d.en_desc, " _
              & "  a.recd_bk_id, " _
              & "  b.bk_name, " _
              & "  a.recd_cu_id, " _
              & "  c.cu_name, " _
              & "  a.recd_ex_rate, " _
              & "  a.recd_amount, " _
              & "  a.recd_date, " _
              & "  a.recd_reff, " _
              & "  a.recd_remarks, " _
              & "  a.recd_type, " _
              & "  a.recd_ex_rate_ext, " _
              & "  a.recd_add_by, " _
              & "  a.recd_add_date, " _
              & "  a.recd_upd_by, " _
              & "  a.recd_upd_date,0 as recd_amount_summ " _
              & "FROM " _
              & "  public.reconciliation_det a " _
              & "  INNER JOIN public.bk_mstr b ON (a.recd_bk_id = b.bk_id) " _
              & "  INNER JOIN public.cu_mstr c ON (a.recd_cu_id = c.cu_id) " _
              & "  INNER JOIN public.en_mstr d ON (a.recd_en_id = d.en_id) " _
              & " Where a.recd_code_mstr='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("recm_code") _
              & "'   ORDER BY " _
              & " a.recd_date "


            gc_detail.DataSource = GetTableData(sSQL)
            gv_detail.BestFitColumns()

            running_summ()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Sub running_summ()
        Dim _nilai As Double
        For i As Integer = 0 To gv_detail.RowCount - 1
            If i = 0 Then
                _nilai = gv_detail.GetRowCellValue(i, "recd_amount") + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("recm_beginning")
                gv_detail.SetRowCellValue(i, "recd_amount_summ", _nilai)
            Else
                _nilai = _nilai + gv_detail.GetRowCellValue(i, "recd_amount")
                gv_detail.SetRowCellValue(i, "recd_amount_summ", _nilai)
            End If
        Next
    End Sub
End Class

