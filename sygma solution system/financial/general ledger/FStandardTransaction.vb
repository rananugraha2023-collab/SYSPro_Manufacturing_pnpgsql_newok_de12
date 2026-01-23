Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FStandardTransaction

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet
    Dim _glt_code As String

#Region "Setting Awal"
    Private Sub FStandardTransaction_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        pr_txttglawal.DateTime = Now
        pr_txttglakhir.DateTime = Now
        gv_edit.OptionsSelection.MultiSelect = True
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        glt_en_id.Properties.DataSource = dt_bantu
        glt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        glt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        glt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        glt_cu_id.Properties.DataSource = dt_bantu
        glt_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        glt_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        glt_cu_id.ItemIndex = 0
    End Sub

    Private Sub glt_cu_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles glt_cu_id.EditValueChanged
        Dim _glt_exc_rate As Double
        If glt_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            _glt_exc_rate = func_data.get_exchange_rate(glt_cu_id.EditValue)
            If _glt_exc_rate = 1 Then
                glt_exc_rate.EditValue = 0
            Else
                glt_exc_rate.EditValue = _glt_exc_rate
            End If

        Else
            glt_exc_rate.EditValue = 1
        End If
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "GL Number", "glt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Effective Date", "glt_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Type", "glt_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Exchange Rate", "glt_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Ext Debit", "glt_ext_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        add_column_copy(gv_master, "Ext Credit", "glt_ext_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n}")
        'add_column_copy(gv_master, "Transaction", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Ref. Number", "glt_ref_trans_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Daybook", "glt_daybook", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Posted", "glt_posted", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Reverse", "glt_is_reverse", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "glt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "glt_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "glt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "glt_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_edit, "glt_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glt_sb_id", False)
        add_column(gv_edit, "Sub Account", "sb_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "glt_cc_id", False)
        add_column(gv_edit, "Cost Center", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Description", "glt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Debit", "glt_debit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n2}")
        add_column_edit(gv_edit, "Credit", "glt_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n2", DevExpress.Data.SummaryItemType.Sum, "TOTAL={0:n2}")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.glt_det.glt_oid, " _
                    & "  public.glt_det.glt_dom_id, " _
                    & "  public.glt_det.glt_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.glt_det.glt_add_by, " _
                    & "  public.glt_det.glt_add_date, " _
                    & "  public.glt_det.glt_upd_by, " _
                    & "  public.glt_det.glt_upd_date, " _
                    & "  public.glt_det.glt_gl_oid, " _
                    & "  public.glt_det.glt_code, " _
                    & "  public.glt_det.glt_date, " _
                    & "  public.glt_det.glt_type, " _
                    & "  public.glt_det.glt_cu_id, " _
                    & "  public.glt_det.glt_exc_rate, " _
                    & "  public.glt_det.glt_seq, " _
                    & "  public.glt_det.glt_ac_id, " _
                    & "  public.glt_det.glt_sb_id, " _
                    & "  public.glt_det.glt_cc_id, " _
                    & "  public.glt_det.glt_desc, " _
                    & "  public.glt_det.glt_debit, " _
                    & "  public.glt_det.glt_credit, " _
                    & "  public.glt_det.glt_debit * public.glt_det.glt_exc_rate as glt_ext_debit, " _
                    & "  public.glt_det.glt_credit * public.glt_det.glt_exc_rate as glt_ext_credit, " _
                    & "  public.glt_det.glt_posted, " _
                    & "  public.glt_det.glt_dt, " _
                    & "  public.tran_mstr.tran_name, " _
                    & "  public.glt_det.glt_ref_trans_code, " _
                    & "  public.glt_det.glt_daybook, " _
                    & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                    & "  public.cu_mstr.cu_name, " _
                    & "  public.ac_mstr.ac_code, " _
                    & "  public.ac_mstr.ac_name, " _
                    & "  public.cc_mstr.cc_desc, " _
                    & "  public.sb_mstr.sb_desc " _
                    & "FROM " _
                    & "  public.glt_det " _
                    & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                    & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                    & "  LEFT OUTER JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id) " _
                    & "  left outer join public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                    & "  left outer join public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                    & " where glt_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and glt_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and glt_daybook ~~* 'system'" _
                    & " and glt_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        glt_en_id.Focus()
        glt_en_id.ItemIndex = 0
        glt_date.DateTime = Now
        glt_type.SelectedIndex = 0
        glt_cu_id.ItemIndex = 0
        glt_exc_rate.EditValue = 1
        glt_ref_trans_code.Text = ""
        'glt_desc.Text = ""
        glt_date.Enabled = True
        glt_is_reverse.EditValue = False
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.glt_det.glt_ac_id, " _
                        & "  public.glt_det.glt_sb_id, " _
                        & "  public.glt_det.glt_cc_id, " _
                        & "  public.glt_det.glt_debit, " _
                        & "  public.glt_det.glt_credit, " _
                        & "  public.glt_det.glt_desc, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.cc_mstr.cc_desc, " _
                        & "  public.sb_mstr.sb_desc " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                        & "  INNER JOIN public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                        & "  INNER JOIN public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                        & " where glt_ac_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        Dim i As Integer
        Dim _debit, _credit As Double
        Dim _gcald_det_status As String
        _debit = 0
        _credit = 0

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        _gcald_det_status = func_data.get_gcald_det_status(glt_en_id.EditValue, "gcald_gl", glt_date.DateTime)
        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + glt_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + glt_date.DateTime.ToString("dd/MM/yyyy"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim ssql As String
        Dim _cu_id As Integer = 0
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            ssql = "select  ac_cu_id from ac_mstr where ac_id =" & ds_edit.Tables(0).Rows(i).Item("glt_ac_id")
            _cu_id = GetRowInfo(ssql)(0)
            'Return False
            'If glt_cu_id.EditValue <> master_new.ClsVar.ibase_cur_id Then
            '    If _cu_id = glt_cu_id.EditValue Then

            _debit = _debit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_debit")) * glt_exc_rate.EditValue
            _credit = _credit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_credit")) * glt_exc_rate.EditValue

            '    Else
            '_debit = _debit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_debit"))
            '_credit = _credit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_credit"))
            '    End If
            'Else
            '_debit = _debit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_debit"))
            '_credit = _credit + CDbl(ds_edit.Tables(0).Rows(i).Item("glt_credit"))
            'End If


        Next

        If System.Math.Round(_debit, 3) <> System.Math.Round(_credit, 3) Then
            'MessageBox.Show("Not Balance Transaction....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Box("Not Balance Transaction, Debit = " & System.Math.Round(_debit, 3) & " And Credit = " & System.Math.Round(_credit, 3))
            Return False
        End If

        If Trim(glt_exc_rate.EditValue) = 0 Then
            MessageBox.Show("Exchange Rate Does'nt Exist..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return before_save
    End Function

    Public Overrides Function before_delete() As Boolean
        before_delete = True
        Dim _glt_posted As String = ""
        Dim _gcald_det_status As String = ""

        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_daybook").ToString.ToUpper <> "SYSTEM" Then
            MessageBox.Show("Can't Delete Automatic Transaction...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select glt_posted from glt_det " + _
        '                                   " where glt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_oid") + "'"
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                _glt_posted = .DataReader("glt_posted").ToString
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Return False
        'End Try

        'If _glt_posted = "Y" Then
        '    MessageBox.Show("Can't Delete Posted Transaction...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Dim ssql As String
        Dim ssqls As New ArrayList
        ssql = "SELECT  " _
                & "  a.gcal_pra_closing,gcal_closing " _
                & "FROM " _
                & "  public.gcal_mstr a " _
                & "WHERE gcal_oid in (SELECT  " _
                    & "  a.gcal_oid " _
                    & "FROM " _
                    & "  public.gcal_mstr a " _
                    & "WHERE " _
                    & "  a.gcal_start_date <= " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_date")) & " AND  " _
                    & "  a.gcal_end_date >= " & SetDateNTime00(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_date")) & " limit 1)"



        Dim dt As New DataTable

        dt = GetTableData(ssql)

        Dim _status As String = ""

        For Each dr As DataRow In dt.Rows
            _status = SetString(dr("gcal_closing"))
        Next

        If _status = "Y" Then
            Box("This periode have been close")
            Exit Function
        End If

        Dim _glt_date As Date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_date")
        Dim _glt_en_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_en_id")

        _gcald_det_status = func_data.get_gcald_det_status(_glt_en_id, "gcald_gl", _glt_date)
        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _glt_date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _glt_date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return before_delete
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If before_delete() = True Then
            'Dim _glt_value As Double
            'Dim i, _ac_id As Integer
            'Dim _sb_id, _cc_id As String ' diset setring agar bisa diberi nilai null
            'Dim _ac_sign, _glt_code As String

            _glt_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code")
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

                            If func_coll.delete_glt_det(ssqls, objinsert, _glt_code) = False Then
                                sqlTran.Rollback()
                                Exit Function
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from glt_det where glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'" + _
                                                   "  "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete GL " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code"))
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

    Public Overrides Function edit_data() As Boolean
        Dim _gcald_det_status As String
        Dim _glt_date As Date
        Dim _glt_en_id As Integer

        _glt_date = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_date")
        _glt_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_en_id")

        _gcald_det_status = func_data.get_gcald_det_status(_glt_en_id, "gcald_gl", _glt_date)
        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _glt_date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _glt_date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _glt_code = .Item("glt_code")
                glt_en_id.EditValue = .Item("glt_en_id")
                glt_date.DateTime = .Item("glt_date")
                glt_type.EditValue = .Item("glt_type")
                glt_ref_trans_code.Text = SetString(.Item("glt_ref_trans_code"))
                glt_cu_id.EditValue = .Item("glt_cu_id")
                glt_exc_rate.EditValue = .Item("glt_exc_rate")
                glt_is_reverse.EditValue = SetBitYNB(.Item("glt_is_reverse"))
                'glt_desc.Text = .Item("glt_desc")
            End With
            glt_en_id.Focus()
            glt_date.Enabled = False 'effective date tidak bisa dirubah.....titik..
            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                        & "  public.glt_det.glt_ac_id, " _
                        & "  public.glt_det.glt_sb_id, " _
                        & "  public.glt_det.glt_cc_id, " _
                        & "  public.glt_det.glt_desc, " _
                        & "  public.glt_det.glt_debit, " _
                        & "  public.glt_det.glt_credit, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.cc_mstr.cc_desc, " _
                        & "  public.sb_mstr.sb_desc,glt_date,glt_en_id,glt_cu_id,glt_cu_id,glt_exc_rate " _
                        & "FROM " _
                        & "  public.glt_det " _
                        & "  INNER JOIN public.en_mstr ON (public.glt_det.glt_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.cu_mstr ON (public.glt_det.glt_cu_id = public.cu_mstr.cu_id) " _
                        & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                        & "  LEFT OUTER JOIN public.tran_mstr ON (public.glt_det.glt_ref_tran_id = public.tran_mstr.tran_id) " _
                        & "  left outer join public.cc_mstr ON (public.glt_det.glt_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.sb_mstr ON (public.glt_det.glt_sb_id = public.sb_mstr.sb_id)" _
                        & " where glt_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"

                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        .FillDataSet(ds_edit, "old")
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
#End Region

#Region "gv_edit"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        If e.Column.Name = "glt_debit" Then
            If e.Value > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "glt_credit", 0)
            End If
        ElseIf e.Column.Name = "glt_credit" Then
            If e.Value > 0 Then
                gv_edit.SetRowCellValue(e.RowHandle, "glt_debit", 0)
            End If
        End If
        gv_edit.BestFitColumns()
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        With gv_edit
            .SetRowCellValue(e.RowHandle, "glt_debit", 0)
            .SetRowCellValue(e.RowHandle, "glt_credit", 0)
            .SetRowCellValue(e.RowHandle, "glt_sb_id", 0)
            .SetRowCellValue(e.RowHandle, "sb_desc", "-")
            .SetRowCellValue(e.RowHandle, "glt_cc_id", 0)
            .SetRowCellValue(e.RowHandle, "cc_desc", "-")
            .BestFitColumns()
        End With
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _glt_code As String
        Dim _glt_value As Double
        Dim i, _ac_id As Integer
        Dim _sb_id, _cc_id As String ' diset setring agar bisa diberi nilai null
        Dim _ac_sign As String = ""
        Dim ssqls As New ArrayList

        _glt_code = func_coll.get_transaction_number(glt_type.Text, glt_en_id.GetColumnValue("en_code"), "glt_det", "glt_code")

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
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
                                                & "  glt_ref_trans_code, " _
                                                & "  glt_daybook, " _
                                                & "  glt_posted, " _
                                                & "  glt_is_reverse, " _
                                                & "  glt_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(glt_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_glt_code) & ",  " _
                                                & SetDate(glt_date.DateTime) & ",  " _
                                                & SetSetring(glt_type.Text) & ",  " _
                                                & SetInteger(glt_cu_id.EditValue) & ",  " _
                                                & SetDbl(glt_exc_rate.EditValue) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_ac_id")) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("glt_desc")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("glt_debit")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("glt_credit")) & ",  " _
                                                & SetSetring(glt_ref_trans_code.Text) & ",  " _
                                                & SetSetring("SYSTEM") & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & SetBitYN(glt_is_reverse.EditValue) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"

                            'glt_sb_id dan glt_cc_id di set ke 0 karena belum dipakai...
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If ds_edit.Tables(0).Rows(i).Item("glt_debit") <> 0 Then
                                _glt_value = ds_edit.Tables(0).Rows(i).Item("glt_debit")
                                _ac_sign = "D"
                            ElseIf ds_edit.Tables(0).Rows(i).Item("glt_credit") <> 0 Then
                                _glt_value = ds_edit.Tables(0).Rows(i).Item("glt_credit")
                                _ac_sign = "C"
                            End If


                            _ac_id = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_ac_id"))
                            '_sb_id = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_sb_id"))
                            _cc_id = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_cc_id"))
                            _sb_id = 0 'ini belum dipake di program ini jadi di set ke 0 dulu aja
                            '_cc_id = 0 'ini belum dipake di program ini jadi di set ke 0 dulu aja

                            If func_coll.update_unposted_glbal_balance(ssqls, objinsert, glt_date.DateTime, _ac_id, _sb_id, _cc_id, glt_en_id.EditValue, glt_cu_id.EditValue, glt_exc_rate.EditValue, _glt_value, _ac_sign) = False Then
                                sqlTran.Rollback()
                                insert = False
                                Exit Function
                            End If
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert GL " & _glt_code)
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
                        set_row(_glt_code, "glt_code")
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

    Public Overrides Function edit()
        edit = True

        Dim _glt_value, _glt_exc_rate As Double
        Dim i, _ac_id As Integer
        Dim _sb_id, _cc_id As String ' diset setring agar bisa diberi nilai null
        Dim _ac_sign As String = ""
        Dim ssqls As New ArrayList
        Dim _glt_date As Date
        Dim _glt_en_id, _glt_cu_id As Integer

        _glt_code = _glt_code

        Try
            gv_edit.UpdateCurrentRow()
            ds_edit.AcceptChanges()
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        'Dilakukan adjustment terhadap glbal_balance karena kan sudah terupdate
                        For i = 0 To ds_edit.Tables("old").Rows.Count - 1
                            If ds_edit.Tables("old").Rows(i).Item("glt_debit") <> 0 Then
                                _glt_value = ds_edit.Tables("old").Rows(i).Item("glt_debit") * -1 'dikali -1 agar dibalik nilainya
                                _ac_sign = "D"
                            ElseIf ds_edit.Tables("old").Rows(i).Item("glt_credit") <> 0 Then
                                _glt_value = ds_edit.Tables("old").Rows(i).Item("glt_credit") * -1 'dikali -1 agar dibalik nilainya
                                _ac_sign = "C"
                            End If

                            _ac_id = SetIntegerDB(ds_edit.Tables("old").Rows(i).Item("glt_ac_id"))
                            _sb_id = 0 'SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_sb_id"))
                            _cc_id = 0 'SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_cc_id"))
                            _glt_date = ds_edit.Tables("old").Rows(i).Item("glt_date")
                            _glt_en_id = ds_edit.Tables("old").Rows(i).Item("glt_en_id")
                            _glt_cu_id = ds_edit.Tables("old").Rows(i).Item("glt_cu_id")
                            _glt_exc_rate = ds_edit.Tables("old").Rows(i).Item("glt_exc_rate")
                            If func_coll.update_unposted_glbal_balance(ssqls, objinsert, _glt_date, _ac_id, _sb_id, _cc_id, _glt_en_id, _glt_cu_id, _glt_exc_rate, _glt_value, _ac_sign) = False Then
                                sqlTran.Rollback()
                                edit = False
                                Exit Function
                            End If
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from glt_det where glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables("detail").Rows.Count - 1
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
                                                & "  glt_ref_trans_code, " _
                                                & "  glt_daybook, " _
                                                & "  glt_posted, " _
                                                & "  glt_is_reverse, " _
                                                & "  glt_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(glt_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(_glt_code) & ",  " _
                                                & SetDate(glt_date.DateTime) & ",  " _
                                                & SetSetring(glt_type.Text) & ",  " _
                                                & SetInteger(glt_cu_id.EditValue) & ",  " _
                                                & SetDbl(glt_exc_rate.EditValue) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetIntegerDB(ds_edit.Tables("detail").Rows(i).Item("glt_ac_id")) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetIntegerDB(0) & ",  " _
                                                & SetSetringDB(ds_edit.Tables("detail").Rows(i).Item("glt_desc")) & ",  " _
                                                & SetDbl(ds_edit.Tables("detail").Rows(i).Item("glt_debit")) & ",  " _
                                                & SetDbl(ds_edit.Tables("detail").Rows(i).Item("glt_credit")) & ",  " _
                                                & SetSetring(glt_ref_trans_code.Text) & ",  " _
                                                & SetSetring("SYSTEM") & ",  " _
                                                & SetSetring("N") & ",  " _
                                                & SetBitYN(glt_is_reverse.EditValue) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If ds_edit.Tables("detail").Rows(i).Item("glt_debit") <> 0 Then
                                _glt_value = ds_edit.Tables("detail").Rows(i).Item("glt_debit")
                                _ac_sign = "D"
                            ElseIf ds_edit.Tables("detail").Rows(i).Item("glt_credit") <> 0 Then
                                _glt_value = ds_edit.Tables("detail").Rows(i).Item("glt_credit")
                                _ac_sign = "C"
                            End If

                            _ac_id = SetIntegerDB(ds_edit.Tables("detail").Rows(i).Item("glt_ac_id"))
                            _sb_id = 0 'SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_sb_id"))
                            _cc_id = 0 'SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_cc_id"))

                            If func_coll.update_unposted_glbal_balance(ssqls, objinsert, glt_date.DateTime, _ac_id, _sb_id, _cc_id, glt_en_id.EditValue, glt_cu_id.EditValue, glt_exc_rate.EditValue, _glt_value, _ac_sign) = False Then
                                sqlTran.Rollback()
                                edit = False
                                Exit Function
                            End If
                        Next


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit GL " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code"))
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
                        set_row(_glt_code, "glt_code")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            edit = False
            MessageBox.Show(ex.Message)
        End Try

        Return edit
    End Function
#End Region

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _gl_en_id As Integer = glt_en_id.EditValue

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = glt_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._cu_id = glt_cu_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
            'ElseIf _col = "sb_desc" Then
            '    Dim frm As New FSubAccountSearch
            '    frm.set_win(Me)
            '    frm._row = _row
            '    frm._en_id = _gl_en_id
            '    frm.type_form = True
            '    frm.ShowDialog()
        ElseIf _col = "cc_desc" Then
            Dim frm As New FCostCenterSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = _gl_en_id
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub PasteFromExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data
            a = i.Split(vbNewLine)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_debit") = a(j).ToString
                End If
            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteFromExcelToCreditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToCreditToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data
            a = i.Split(vbNewLine)
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_credit") = a(j).ToString
                End If
            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteToRemarkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToRemarkToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer

        Try
            'Box(data)
            i = data.Replace(vbNewLine, "~")
            a = i.Split("~")
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ds_edit.Tables(0).Rows(j).Item("glt_desc") = a(j).ToString
                End If

            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub PasteFromExcelToAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteFromExcelToAccountToolStripMenuItem.Click
        Dim data As String = Clipboard.GetDataObject().GetData(DataFormats.Text)
        Dim i As String
        Dim a() As String
        Dim j As Integer
        Dim ssql As String
        Try
            'Box(data)
            i = data.Replace(vbNewLine, "~")
            a = i.Split("~")
            ds_edit.AcceptChanges()
            For j = 0 To a.GetUpperBound(0)
                'Box(a(j))
                If j <= ds_edit.Tables(0).Rows.Count - 1 Then
                    ssql = "Select ac_id,ac_code,ac_name from ac_mstr where ac_code='" & Trim(a(j).ToString) & "'"
                    Dim dr As DataRow = GetRowInfo(ssql)
                    ds_edit.Tables(0).Rows(j).Item("glt_ac_id") = dr("ac_id")
                    ds_edit.Tables(0).Rows(j).Item("ac_code") = a(j).ToString
                    ds_edit.Tables(0).Rows(j).Item("ac_name") = dr("ac_name")
                End If

            Next
            ds_edit.AcceptChanges()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub preview()
        'Dim _en_id As Integer
        'Dim _type, _table, _initial, _code_awal, _code_akhir As String

        '_en_id = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_en_id")
        '_type = 10
        '_table = "so_mstr"
        '_initial = "so"
        '_code_awal = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")
        '_code_akhir = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("so_code")

        'func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, _now)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
             & "glt_det.glt_oid, " _
             & "glt_det.glt_dom_id, " _
             & "glt_det.glt_en_id, " _
             & "en_mstr.en_desc, " _
             & "glt_det.glt_add_by, " _
             & "glt_det.glt_add_date, " _
             & "glt_det.glt_upd_by, " _
             & "glt_det.glt_upd_date, " _
             & "glt_det.glt_gl_oid, " _
             & "glt_det.glt_code, " _
             & "glt_det.glt_date, " _
             & "glt_det.glt_type, " _
             & "glt_det.glt_cu_id, " _
             & "glt_det.glt_exc_rate, " _
             & "glt_det.glt_seq, " _
             & "glt_det.glt_ac_id, " _
             & "glt_det.glt_sb_id, " _
             & "glt_det.glt_cc_id, " _
             & "glt_det.glt_desc, " _
             & "glt_det.glt_debit, " _
             & "glt_det.glt_credit, " _
             & "glt_det.glt_debit * glt_det.glt_exc_rate as glt_ext_debit, " _
             & "glt_det.glt_credit * glt_det.glt_exc_rate as glt_ext_credit, " _
             & "glt_det.glt_posted, " _
             & "glt_det.glt_dt, " _
             & "tran_mstr.tran_name, " _
             & "glt_det.glt_ref_trans_code, " _
             & "glt_det.glt_daybook, " _
             & "cu_mstr.cu_name, " _
             & "ac_mstr.ac_code, " _
             & "ac_mstr.ac_name, " _
             & "cc_mstr.cc_desc, " _
             & "sb_mstr.sb_desc, " _
             & "cmaddr_name, " _
             & "cmaddr_line_1, " _
             & "cmaddr_line_2, " _
             & "cmaddr_line_3 " _
             & "FROM " _
             & "glt_det " _
             & "INNER JOIN en_mstr ON (glt_det.glt_en_id = en_mstr.en_id) " _
             & "INNER JOIN cu_mstr ON (glt_det.glt_cu_id = cu_mstr.cu_id) " _
             & "INNER JOIN ac_mstr ON (glt_det.glt_ac_id = ac_mstr.ac_id) " _
             & "LEFT OUTER JOIN tran_mstr ON (glt_det.glt_ref_tran_id = tran_mstr.tran_id) " _
             & "left outer join cc_mstr ON (glt_det.glt_cc_id = cc_mstr.cc_id) " _
             & "left outer join sb_mstr ON (glt_det.glt_sb_id = sb_mstr.sb_id)  " _
             & "inner join cmaddr_mstr on cmaddr_en_id = glt_en_id " _
             & "WHERE " _
             & "glt_code ~~* '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code") + "'"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRGeneralLedgerPrint"
        frm._remarks = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("glt_code")
        frm.ShowDialog()
    End Sub

    Private Sub BtImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtImport.Click
        Try
            Dim ssqls As New ArrayList
            Dim ssql As String

            Dim ds_import As New DataSet
            Dim _file As String = AskOpenFile("Format import Standar Transaction Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            ds_import = master_new.excelconn.ImportExcel(_file)

            Dim _en_id As String = ""
            Dim _cu_id As String = ""
            Dim _ac_id As String = ""

            Dim _sb_id As String = ""
            Dim _cc_id As String = ""

            Dim dt_temp As New DataTable


            Dim _debet, _credit As Double
            _debet = 0.0
            _credit = 0.0

            Dim _number As String = ""
            ssql = "select 0 as en_id, '' as gl_number, current_date as effective_date, '' as type, 0 as cu_id, 0.0 as exc_rate, 0 as ac_id, 0 as sb_id, 0 as cc_id, '' as desc, 0.0 as debit, 0.0 as credit, '' as reff_number "


            Dim dt_trx As New DataTable
            dt_trx = GetTableData(ssql)

            Dim dt_trx_ok As New DataTable
            dt_trx_ok = GetTableData(ssql)

            dt_trx.Rows.Clear()
            dt_trx_ok.Rows.Clear()

            Dim _total As Integer = ds_import.Tables(0).Rows.Count
            Dim x As Integer = 1
            For Each dr As DataRow In ds_import.Tables(0).Rows

                If dr("GL Number").ToString.ToLower <> _number.ToLower Then

                    '_debet = 0.0
                    '_credit = 0.0

                    'For Each dr_trx As DataRow In dt_trx.Rows

                    '    _debet += dr_trx("debit") * dr_trx("exc_rate")
                    '    _credit += dr_trx("credit") * dr_trx("exc_rate")
                    'Next

                    If System.Math.Round(_debet, 3) <> System.Math.Round(_credit, 3) Then
                        Box(dr("GL Number").ToString & " Not Balance Transaction, Debit = " & System.Math.Round(_debet, 3) & " And Credit = " & System.Math.Round(_credit, 3))
                        Exit Sub
                    End If


                    _number = dr("GL Number").ToString
                    _debet = 0.0
                    _credit = 0.0
                    dt_trx.Rows.Clear()
                End If



                _debet = _debet + SetNumber(dr("Debit"))
                _credit = _credit + SetNumber(dr("Credit"))

                ssql = "select en_id from en_mstr where lower(en_desc)=" & SetSetring(dr("Entity").ToString.ToLower)

                dt_temp = GetTableData(ssql)

                _en_id = "0"
                For Each dr_temp As DataRow In dt_temp.Rows
                    _en_id = dr_temp(0).ToString
                Next

                If _en_id = "0" Then
                    Box(dr("GL Number").ToString & " entity 0")
                    Exit Sub
                End If

                ssql = "select cu_id from cu_mstr where lower(cu_name)=" & SetSetring(dr("Currency").ToString.ToLower)

                dt_temp = GetTableData(ssql)

                _cu_id = "0"
                For Each dr_temp As DataRow In dt_temp.Rows
                    _cu_id = dr_temp(0).ToString
                Next

                If _cu_id = "0" Then
                    Box(dr("GL Number").ToString & " Currency 0")
                    Exit Sub
                End If


                ssql = "select ac_id from ac_mstr where lower(ac_code)=" & SetSetring(dr("Account Code").ToString.ToLower)

                dt_temp = GetTableData(ssql)
                _ac_id = "0"
                For Each dr_temp As DataRow In dt_temp.Rows
                    _ac_id = dr_temp(0).ToString
                Next

                If _ac_id = "0" Then
                    Box(dr("GL Number").ToString & " Account ID 0")
                    Exit Sub
                End If

                ssql = "select sb_id from sb_mstr where lower(sb_desc)=" & SetSetring(dr("Sub Account").ToString.ToLower)

                dt_temp = GetTableData(ssql)
                For Each dr_temp As DataRow In dt_temp.Rows
                    _sb_id = dr_temp(0).ToString
                Next

                ssql = "select cc_id from cc_mstr where lower(cc_desc)=" & SetSetring(dr("Cost Center").ToString.ToLower)

                dt_temp = GetTableData(ssql)
                For Each dr_temp As DataRow In dt_temp.Rows
                    _cc_id = dr_temp(0).ToString
                Next


                Dim _row As DataRow
                Dim _row_ok As DataRow

                _row = dt_trx.NewRow
                _row_ok = dt_trx_ok.NewRow


                _row("en_id") = _en_id
                _row("gl_number") = _number
                _row("effective_date") = dr("Effective Date")
                _row("type") = dr("Type")
                _row("cu_id") = CInt(_cu_id)
                _row("exc_rate") = SetNumber(dr("Exchange Rate"))
                _row("ac_id") = CInt(_ac_id)
                _row("sb_id") = CInt(_sb_id)
                _row("cc_id") = CInt(_cc_id)
                _row("desc") = dr("Description")
                _row("debit") = SetNumber(dr("Debit"))
                _row("credit") = SetNumber(dr("Credit"))
                _row("reff_number") = dr("Ref Number")

                dt_trx.Rows.Add(_row)
                dt_trx.AcceptChanges()

                _row_ok("en_id") = _en_id
                _row_ok("gl_number") = _number
                _row_ok("effective_date") = dr("Effective Date")
                _row_ok("type") = dr("Type")
                _row_ok("cu_id") = CInt(_cu_id)
                _row_ok("exc_rate") = SetNumber(dr("Exchange Rate"))
                _row_ok("ac_id") = CInt(_ac_id)
                _row_ok("sb_id") = CInt(_sb_id)
                _row_ok("cc_id") = CInt(_cc_id)
                _row_ok("desc") = IIf(dr("Description").ToString.Length > 75, Microsoft.VisualBasic.Left(dr("Description"), 75), dr("Description"))
                _row_ok("debit") = SetNumber(dr("Debit"))
                _row_ok("credit") = SetNumber(dr("Credit"))
                _row_ok("reff_number") = dr("Ref Number")


                dt_trx_ok.Rows.Add(_row_ok)
                dt_trx_ok.AcceptChanges()

                LblStatus.Text = "Pengecekan " & x & " of " & _total
                Windows.Forms.Application.DoEvents()

                x = x + 1
            Next

            x = 1
            _number = ""
            Dim _glt_code As String = ""
            Dim _glt_value As Double
            Dim _ac_sign As String = ""
            Dim _nomor As Double = 0
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            For Each dr As DataRow In dt_trx_ok.Rows

                                _glt_code = dr("gl_number")
                                If dr("gl_number").ToString.ToLower <> _number.ToLower Then

                                    _number = dr("gl_number")

                                    '    ssql = "select en_code from en_mstr where en_id=" & SetInteger(dr("en_id"))

                                    '    dt_temp = GetTableData(ssql)

                                    '    For Each dr_temp As DataRow In dt_temp.Rows
                                    '        _en_id = dr_temp(0).ToString
                                    '    Next

                                    '    Dim tanggal As Date
                                    '    Dim tahun, bulan, no_urut_format As String
                                    '    tanggal = master_new.PGSqlConn.CekTanggal


                                    '    tahun = tanggal.Year.ToString.Substring(2, 2)
                                    '    bulan = tanggal.Month.ToString
                                    '    Dim _no_id As String = Format(master_new.ClsVar.sUserID, "000")
                                    '    If _nomor = 0 Then
                                    '        _nomor = get_transaction_number_local(dr("type"), _en_id, "glt_det", "glt_code")
                                    '    Else
                                    '        _nomor = _nomor + 1
                                    '    End If


                                    '    _glt_code = dr("type") + _en_id + tahun + bulan + master_new.ClsVar.sServerCode + _no_id + Format(_nomor, "00000")

                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = insert_log("Insert GL " & _glt_code)
                                    ssqls.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()

                                End If



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
                                                    & "  glt_ref_trans_code, " _
                                                    & "  glt_daybook, " _
                                                    & "  glt_posted, " _
                                                    & "  glt_is_reverse, " _
                                                    & "  glt_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(dr("en_id")) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetSetring(_glt_code) & ",  " _
                                                    & SetDate(dr("effective_date")) & ",  " _
                                                    & SetSetring(dr("type")) & ",  " _
                                                    & SetInteger(dr("cu_id")) & ",  " _
                                                    & SetDbl(dr("exc_rate")) & ",  " _
                                                    & SetInteger(x) & ",  " _
                                                    & SetIntegerDB(dr("ac_id")) & ",  " _
                                                    & SetIntegerDB(0) & ",  " _
                                                    & SetIntegerDB(0) & ",  " _
                                                    & SetSetringDB(dr("desc")) & ",  " _
                                                    & SetDbl(dr("debit")) & ",  " _
                                                    & SetDbl(dr("credit")) & ",  " _
                                                    & SetSetring(dr("reff_number")) & ",  " _
                                                    & SetSetring("SYSTEM") & ",  " _
                                                    & SetSetring("N") & ",  " _
                                                    & SetBitYN(False) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"

                                ''glt_sb_id dan glt_cc_id di set ke 0 karena belum dipakai...
                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                If dr("debit") <> 0 Then
                                    _glt_value = dr("debit")
                                    _ac_sign = "D"
                                ElseIf dr("credit") <> 0 Then
                                    _glt_value = dr("credit")
                                    _ac_sign = "C"
                                End If


                                _ac_id = SetIntegerDB(dr("ac_id"))
                                '_sb_id = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_sb_id"))
                                '_cc_id = SetIntegerDB(ds_edit.Tables(0).Rows(i).Item("glt_cc_id"))
                                _sb_id = 0 'ini belum dipake di program ini jadi di set ke 0 dulu aja
                                _cc_id = 0 'ini belum dipake di program ini jadi di set ke 0 dulu aja

                                If func_coll.update_unposted_glbal_balance(ssqls, objinsert, dr("effective_date"), _ac_id, _sb_id, _cc_id, dr("en_id"), dr("cu_id"), dr("exc_rate"), _glt_value, _ac_sign) = False Then
                                    sqlTran.Rollback()
                                    ' insert = False
                                    Exit Sub
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

                            after_success()
                            'set_row(_glt_code, "glt_code")

                            LblStatus.Text = "Process " & x & " of " & _total
                            Windows.Forms.Application.DoEvents()

                            x = x + 1

                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                row = 0

                MessageBox.Show(ex.Message)
            End Try


            LblStatus.Text = "Selesai"
            Box("Import success")
            refresh_data()


        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Public Function get_transaction_number_local(ByVal par_type As String, ByVal par_entity As String, _
                                                 ByVal par_table As String, ByVal par_colom As String) As Double
        get_transaction_number_local = 0

        Dim tahun, bulan, no_urut_format As String
        Dim _no_urut As Double

        Dim tanggal As Date
        tanggal = CekTanggal()

        tahun = tanggal.Year.ToString.Substring(2, 2)
        bulan = tanggal.Month.ToString
        no_urut_format = ""

        If Len(bulan) = 1 Then
            bulan = "0" + bulan
        End If

        Try
            Dim ds_bantu As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    .SQL = "select coalesce(max(cast(substring(" + par_colom + ",14,5) as integer)),0) + 1 as no_urut " + _
                           " from " + par_table + _
                           " where substring(" + par_colom + ",3,2) = '" + par_entity + "'" + _
                           " and substring(" + par_colom + ",5,2) = '" + tahun + "'" + _
                           " and substring(" + par_colom + ",7,2) = '" + bulan + "'" + _
                           " and length(" + par_colom + ")=18 limit 1"

                    .InitializeCommand()
                    .FillDataSet(ds_bantu, "transactionnumber")
                    _no_urut = ds_bantu.Tables(0).Rows(0).Item("no_urut")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        'Dim _no_id As String = Format(master_new.ClsVar.sUserID, "000")

        'no_urut_format = Format(_no_urut, "00000")

        get_transaction_number_local = _no_urut 'par_type + par_entity + tahun + bulan + master_new.ClsVar.sServerCode + _no_id + no_urut_format

        Return get_transaction_number_local

    End Function

    Private Sub BtDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtDel.Click
        Try
            Dim ssqls As New ArrayList
            Dim ssql As String

            Dim ds_import As New DataSet
            Dim _file As String = AskOpenFile("Format import Delete Standart Transaction Excel 2003 | *.xls")

            If _file = "" Then
                Exit Sub
            End If

            ds_import = master_new.excelconn.ImportExcel(_file)

            Dim _total As Integer = ds_import.Tables(0).Rows.Count
            Dim x As Integer = 1

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            For Each dr As DataRow In ds_import.Tables(0).Rows

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "delete from glt_det where glt_code='" & dr("GL Number") & "'"

                                ssqls.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                LblStatus.Text = "Process " & x & " of " & _total
                                Windows.Forms.Application.DoEvents()

                                x = x + 1
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
                            after_success()

                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)

                        End Try
                    End With
                End Using
            Catch ex As Exception
                row = 0

                MessageBox.Show(ex.Message)
            End Try

            LblStatus.Text = "Selesai"
            Box("Delete success")
            refresh_data()

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub
End Class
