Imports npgsql
Imports master_new.ModFunction

Public Class FDSPeriode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _bds_oid_mstr As String

    Private Sub FDSPeriode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        bds_en_id.Properties.DataSource = dt_bantu
        bds_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bds_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bds_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Periode Code", "bds_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "bds_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Start Date", "bds_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "bds_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date Periode 2", "bds_end_date2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remark", "bds_remark", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Generate", "bds_generate", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "bds_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "bds_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "bds_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "bds_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  bds_oid, " _
                    & "  bds_dom_id, " _
                    & "  bds_en_id, " _
                    & "  bds_add_by, " _
                    & "  bds_add_date, " _
                    & "  bds_upd_by, " _
                    & "  bds_upd_date, " _
                    & "  bds_code, " _
                    & "  bds_date, " _
                    & "  bds_start_date, " _
                    & "  bds_end_date,bds_end_date2, " _
                    & "  bds_remark, " _
                    & "  bds_trans_id, " _
                    & "  bds_generate, " _
                    & "  bds_dt, " _
                    & "  en_desc " _
                    & "  FROM  " _
                    & "  public.bds_mstr " _
                    & "  inner join en_mstr on en_id = bds_en_id" _
                    & "  where bds_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and bds_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and bds_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        bds_en_id.Focus()
        bds_en_id.ItemIndex = 0
        bds_code.Text = ""
        bds_start_date.Text = ""
        bds_end_date.Text = ""
        bds_end_date2.Text = ""

        bds_remark.Text = ""
        bds_generate.EditValue = False

        bds_en_id.Enabled = True
        bds_code.Enabled = True
        bds_start_date.Enabled = True
        bds_end_date.Enabled = True
        bds_end_date2.Enabled = True

        bds_remark.Enabled = True
    End Sub

    Private Function before_save_local() As Boolean
        before_save_local = True

        If bds_start_date.DateTime > bds_end_date.DateTime Then
            MessageBox.Show("Start Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bds_end_date.Focus()
            Return False
        End If

        If bds_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bds_start_date.Focus()
            Return False
        End If

        If bds_end_date.Text = "" Then
            MessageBox.Show("End Date Periode 2 Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bds_end_date2.Focus()
            Return False
        End If


        If bds_end_date2.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bds_end_date.Focus()
            Return False
        End If

        Dim _bds_end_date As Date = "01/01/1909"
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bds_end_date from bds_mstr " + _
                                           " where bds_en_id = " + bds_en_id.EditValue.ToString + _
                                           " order by bds_end_date desc limit 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _bds_end_date = .DataReader("bds_end_date").ToString
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _bds_end_date > bds_start_date.DateTime Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            bds_start_date.Focus()
            Return False
        End If

        Return before_save_local
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_local() = False Then
            Exit Function
        End If

        Dim _bds_oid As Guid = Guid.NewGuid
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.bds_mstr " _
                                            & "( " _
                                            & "  bds_oid, " _
                                            & "  bds_dom_id, " _
                                            & "  bds_en_id, " _
                                            & "  bds_add_by, " _
                                            & "  bds_add_date, " _
                                            & "  bds_code, " _
                                            & "  bds_date, " _
                                            & "  bds_start_date, " _
                                            & "  bds_end_date,bds_end_date2, " _
                                            & "  bds_remark, " _
                                            & "  bds_generate, " _
                                            & "  bds_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_bds_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(bds_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(bds_code.Text) & ",  " _
                                            & "" & SetDateNTime00(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDate(bds_start_date.DateTime) & ",  " _
                                            & SetDate(bds_end_date.DateTime) & ",  " _
                                             & SetDate(bds_end_date2.DateTime) & ",  " _
                                            & SetSetring(bds_remark.Text) & ",  " _
                                            & SetBitYN(bds_generate.EditValue) & ",  " _
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
                        set_row(Trim(_bds_oid.ToString), "bds_oid")
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
                _bds_oid_mstr = .Item("bds_oid").ToString
                bds_en_id.EditValue = .Item("bds_en_id")
                bds_code.Text = SetString(.Item("bds_code"))
                bds_start_date.DateTime = .Item("bds_start_date")
                bds_end_date.DateTime = .Item("bds_end_date")
                bds_end_date2.EditValue = .Item("bds_end_date2")
                bds_remark.Text = SetString(.Item("bds_remark"))
                bds_generate.EditValue = SetBitYNB(.Item("bds_generate"))
            End With
            bds_en_id.Focus()
            bds_en_id.Enabled = False
            bds_code.Enabled = False
            bds_start_date.Enabled = False
            bds_end_date.Enabled = False
            bds_end_date2.Enabled = True
            bds_remark.Enabled = False
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
                                            & "  public.bds_mstr   " _
                                            & "SET  " _
                                            & "  bds_generate = " & SetBitYN(bds_generate.EditValue) & ",  " _
                                            & "  bds_end_date2 = " & SetDateNTime00(bds_end_date2.EditValue) & ",  " _
                                            & "  bds_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  bds_oid = " & SetSetring(_bds_oid_mstr) & " "
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
                        set_row(Trim(_bds_oid_mstr.ToString), "bds_oid")
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
                            .Command.CommandText = "delete from bds_mstr where bds_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bds_oid").ToString + "'"
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
