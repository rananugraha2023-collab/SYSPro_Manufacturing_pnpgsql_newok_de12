Imports Npgsql
Imports master_new.ModFunction

Public Class FRSQuarterPeriode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _qrtr_oid_mstr As String

    Private Sub FRSQuarterPeriode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        qrtr_en_id.Properties.DataSource = dt_bantu
        qrtr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        qrtr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        qrtr_en_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_period_type_mstr())
        'qrtr_qrtr_code_id.Properties.DataSource = dt_bantu
        'qrtr_qrtr_code_id.Properties.DisplayMember = dt_bantu.Columns("period_type_name").ToString
        'qrtr_qrtr_code_id.Properties.ValueMember = dt_bantu.Columns("period_type_id").ToString
        'qrtr_qrtr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Code", "qrtr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Type", "qrtr_code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Name", "qrtr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Year", "qrtr_year", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Period Interval", "qrtr_period_count", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Start Date", "qrtr_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "qrtr_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remark", "qrtr_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Generate", "qrtr_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Trans Status", "qrtr_trans_id", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "qrtr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "qrtr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "qrtr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "qrtr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  qrtr_oid, " _
                    & "  qrtr_en_id, " _
                    & "  qrtr_add_by, " _
                    & "  qrtr_add_date, " _
                    & "  qrtr_upd_by, " _
                    & "  qrtr_upd_date, " _
                    & "  qrtr_code_id, " _
                    & "  qrtr_code_name, " _
                    & "  qrtr_code, " _
                    & "  qrtr_name, " _
                    & "  qrtr_year, " _
                    & "  qrtr_date, " _
                    & "  qrtr_start_date, " _
                    & "  qrtr_end_date, " _
                    & "  qrtr_remark, " _
                    & "  qrtr_trans_id, " _
                    & "  qrtr_active, " _
                    & "  qrtr_dt, " _
                    & "  en_desc, " _
                    & "  qrtr_period_count " _
                    & "  FROM  " _
                    & "  public.qrtr_mstr " _
                    & "  inner join en_mstr on en_id = qrtr_en_id" _
                    & "  where qrtr_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and qrtr_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and qrtr_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        qrtr_en_id.Focus()
        qrtr_en_id.ItemIndex = 0
        qrtr_code.Text = ""
        qrtr_start_date.EditValue = _now
        qrtr_end_date.Text = ""
        qrtr_remark.Text = ""
        qrtr_active.EditValue = False

        qrtr_en_id.Enabled = True
        qrtr_code.Enabled = True
        qrtr_start_date.Enabled = True
        qrtr_end_date.Enabled = True
        qrtr_remark.Enabled = True
    End Sub

    Private Function before_save_local() As Boolean
        before_save_local = True

        If qrtr_start_date.DateTime > qrtr_end_date.DateTime Then
            MessageBox.Show("End Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            qrtr_end_date.Focus()
            Return False
        End If

        If qrtr_start_date.DateTime > qrtr_end_date.DateTime Then
            MessageBox.Show("Start Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            qrtr_end_date.Focus()
            Return False
        End If

        If qrtr_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            qrtr_start_date.Focus()
            Return False
        End If

        If qrtr_end_date.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            qrtr_end_date.Focus()
            Return False
        End If

        Dim _qrtr_end_date As Date = "01/01/1909"
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select qrtr_end_date from qrtr_mstr " + _
                                           " where qrtr_en_id = " + qrtr_en_id.EditValue.ToString + _
                                           " order by qrtr_end_date desc limit 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _qrtr_end_date = .DataReader("qrtr_end_date").ToString
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'If _qrtr_end_date > qrtr_start_date.DateTime Then
        '    MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    qrtr_start_date.Focus()
        '    Return False
        'End If

        Return before_save_local
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_local() = False Then
            Exit Function
        End If

        Dim _qrtr_oid As Guid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Dim _qrtr_id As Long = SetInteger(func_coll.GetID("qrtr_mstr", qrtr_en_id.GetColumnValue("en_code"), "qrtr_id", "qrtr_en_id", qrtr_en_id.EditValue.ToString))
        Dim _qrtr_code As String

        _qrtr_code = func_coll.get_transaction_number("QR", qrtr_en_id.GetColumnValue("en_code"), "qrtr_mstr", "qrtr_code")
        ssqls.Clear()

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
                                            & "  public.qrtr_mstr " _
                                            & "( " _
                                            & "  qrtr_oid, " _
                                            & "  qrtr_id, " _
                                            & "  qrtr_en_id, " _
                                            & "  qrtr_code, " _
                                            & "  qrtr_name, " _
                                            & "  qrtr_year, " _
                                            & "  qrtr_code_id, " _
                                            & "  qrtr_code_name, " _
                                            & "  qrtr_period_count, " _
                                            & "  qrtr_add_by, " _
                                            & "  qrtr_add_date, " _
                                            & "  qrtr_date, " _
                                            & "  qrtr_start_date, " _
                                            & "  qrtr_end_date, " _
                                            & "  qrtr_remark, " _
                                            & "  qrtr_active, " _
                                            & "  qrtr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_qrtr_oid.ToString) & ",  " _
                                            & _qrtr_id & ",  " _
                                            & SetInteger(qrtr_en_id.EditValue) & ",  " _
                                            & SetSetring(IIf(SetString(qrtr_code.EditValue) = "", _qrtr_code, qrtr_code.EditValue)) & ",  " _
                                            & SetSetring(qrtr_name.Text) & ",  " _
                                            & SetSetring(qrtr_year.Text) & ",  " _
                                            & SetSetring(qrtr_period_type_id.Tag) & ",  " _
                                            & SetSetring(qrtr_period_type_id.Text) & ",  " _
                                            & SetSetring(qrtr_period_count.Text) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & "" & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(qrtr_start_date.DateTime) & ",  " _
                                            & SetDate(qrtr_end_date.DateTime) & ",  " _
                                            & SetSetring(qrtr_remark.Text) & ",  " _
                                            & SetBitYN(qrtr_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
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
                        set_row(Trim(_qrtr_oid.ToString), "qrtr_oid")
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
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _qrtr_oid_mstr = .Item("qrtr_oid").ToString
                qrtr_en_id.EditValue = .Item("qrtr_en_id")
                qrtr_code.Text = SetString(.Item("qrtr_code"))
                qrtr_start_date.DateTime = .Item("qrtr_start_date")
                qrtr_end_date.DateTime = .Item("qrtr_end_date")
                qrtr_remark.Text = SetString(.Item("qrtr_remark"))
                qrtr_active.EditValue = SetBitYNB(.Item("qrtr_generate"))
            End With
            qrtr_en_id.Focus()
            qrtr_en_id.Enabled = False
            qrtr_code.Enabled = False
            qrtr_start_date.Enabled = False
            qrtr_end_date.Enabled = False
            qrtr_remark.Enabled = False
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.qrtr_mstr   " _
                                            & "SET  " _
                                            & "  qrtr_generate = " & SetBitYN(qrtr_active.EditValue) & ",  " _
                                            & "  qrtr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  qrtr_oid = " & SetSetring(_qrtr_oid_mstr) & " "
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
                        set_row(Trim(_qrtr_oid_mstr.ToString), "qrtr_oid")
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

    'Public Overrides Function edit_data() As Boolean
    '    MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Function

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
                            .Command.CommandText = "delete from qrtr_mstr where qrtr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("qrtr_oid").ToString + "'"
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

    Private Sub qrtr_period_type_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles qrtr_period_type_id.ButtonClick
        Dim frm As New FPeriodeTypeSearch
        frm.set_win(Me)
        frm._obj = qrtr_period_type_id
        'frm._code = qrtr_period_type_id.EditValue
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

    Private Sub qrtr_retrieve_Click(sender As Object, e As EventArgs) Handles qrtr_retrieve.Click
        ' Pastikan Start Date dan Interval sudah diisi
        If qrtr_start_date.Text = "" OrElse qrtr_period_count.Text = "" Then
            MessageBox.Show("Harap masukkan Start Date dan Interval.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim startDate As DateTime = qrtr_start_date.DateTime
        Dim interval As Integer

        ' Validasi interval sebagai angka
        If Not Integer.TryParse(qrtr_period_count.Text, interval) Then
            MessageBox.Show("Interval harus berupa angka.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Set Year dari Start Date
        qrtr_year.Text = startDate.Year.ToString()

        ' Tentukan awal bulan dari start date
        Dim adjustedStartDate As DateTime = New DateTime(startDate.Year, startDate.Month, 1)

        ' Hitung End Date dengan menambahkan bulan sesuai interval
        Dim tempEndDate As DateTime = adjustedStartDate.AddMonths(interval - 1)
        Dim endDate As DateTime = New DateTime(tempEndDate.Year, tempEndDate.Month, DateTime.DaysInMonth(tempEndDate.Year, tempEndDate.Month))

        ' Tampilkan hasil ke kontrol End Date
        qrtr_end_date.DateTime = endDate
    End Sub


End Class
