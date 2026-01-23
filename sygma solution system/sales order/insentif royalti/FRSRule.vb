Imports npgsql
Imports master_new.ModFunction

Public Class FRSRule
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim _brsr_oid_mstr As String

    Private Sub FRSRule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        brsr_en_id.Properties.DataSource = dt_bantu
        brsr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        brsr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        brsr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_area_mstr())
        brsr_area_id.Properties.DataSource = dt_bantu
        brsr_area_id.Properties.DisplayMember = dt_bantu.Columns("area_code").ToString
        brsr_area_id.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        brsr_area_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr("position"))
        brsr_position_id.Properties.DataSource = dt_bantu
        brsr_position_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        brsr_position_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        brsr_position_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "brsr_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Date", "brsr_end_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Amount Target", "brsr_target_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bobot Insentive", "brsr_insentive", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Bobot Selisih", "brsr_bobot", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "p")

        add_column_copy(gv_master, "User Create", "brsr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "brsr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "brsr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "brsr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  brsr_oid, " _
                    & "  brsr_dom_id, " _
                    & "  brsr_en_id, " _
                    & "  brsr_add_by, " _
                    & "  brsr_add_date, " _
                    & "  brsr_upd_by, " _
                    & "  brsr_upd_date, " _
                    & "  brsr_position_id, " _
                    & "  brsr_start_date, " _
                    & "  brsr_end_date, " _
                    & "  brsr_target_amount, " _
                    & "  brsr_insentive, " _
                    & "  brsr_bobot, " _
                    & "  brsr_dt, " _
                    & "  brsr_area_id, " _
                    & "  en_desc, " _
                    & "  code_name, " _
                    & "  area_name " _
                    & "FROM  " _
                    & "  public.brsr_rule " _
                    & "  inner join public.en_mstr on en_id = brsr_en_id " _
                    & "  inner join code_mstr on code_id = brsr_position_id " _
                    & "  inner join area_mstr on area_id = brsr_area_id"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        brsr_en_id.Focus()
        brsr_en_id.ItemIndex = 0
        brsr_area_id.ItemIndex = 0
        brsr_position_id.ItemIndex = 0
        brsr_target_amount.Text = ""
        brsr_insentive.Text = ""
        brsr_bobot.Text = ""
        brsr_start_date.DateTime = _now
        brsr_end_date.DateTime = _now
    End Sub

    Private Function before_save_local() As Boolean
        before_save_local = True

        If brsr_start_date.DateTime > brsr_end_date.DateTime Then
            MessageBox.Show("Start Date Can't Higher Than End Date..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            brsr_end_date.Focus()
            Return False
        End If

        If brsr_start_date.Text = "" Then
            MessageBox.Show("Start Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            brsr_start_date.Focus()
            Return False
        End If

        If brsr_end_date.Text = "" Then
            MessageBox.Show("End Date Can't Blank..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            brsr_end_date.Focus()
            Return False
        End If

        Dim _brsr_end_date As Date = "01/01/1909"
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select brsr_end_date from brsr_rule " + _
                                           " where brsr_en_id = " + brsr_en_id.EditValue.ToString + _
                                           " and brsr_area_id = " + brsr_area_id.EditValue.ToString + _
                                           " and brsr_position_id = " + brsr_position_id.EditValue.ToString + _
                                           " order by brsr_end_date desc limit 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _brsr_end_date = .DataReader("brsr_end_date").ToString
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _brsr_end_date > brsr_start_date.DateTime Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            brsr_start_date.Focus()
            Return False
        End If

        Return before_save_local
    End Function

    Public Overrides Function insert() As Boolean
        If before_save_local() = False Then
            Return False
        End If

        Dim _brsr_oid As Guid = Guid.NewGuid
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
                                            & "  public.brsr_rule " _
                                            & "( " _
                                            & "  brsr_oid, " _
                                            & "  brsr_dom_id, " _
                                            & "  brsr_en_id, " _
                                            & "  brsr_add_by, " _
                                            & "  brsr_add_date, " _
                                            & "  brsr_position_id, " _
                                            & "  brsr_start_date, " _
                                            & "  brsr_end_date, " _
                                            & "  brsr_target_amount, " _
                                            & "  brsr_insentive, " _
                                            & "  brsr_bobot, " _
                                            & "  brsr_dt, " _
                                            & "  brsr_area_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_brsr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(brsr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(brsr_position_id.EditValue) & ",  " _
                                            & SetDate(brsr_start_date.DateTime) & ",  " _
                                            & SetDate(brsr_end_date.DateTime) & ",  " _
                                            & SetDbl(brsr_target_amount.EditValue) & ",  " _
                                            & SetDbl(brsr_insentive.EditValue) & ",  " _
                                            & SetDbl(brsr_bobot.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(brsr_area_id.EditValue) & "  " _
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
                        set_row(Trim(_brsr_oid.ToString), "brsr_oid")
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
                _brsr_oid_mstr = .Item("brsr_oid").ToString
                brsr_en_id.EditValue = .Item("brsr_en_id")
                brsr_area_id.EditValue = .Item("brsr_area_id")
                brsr_position_id.EditValue = .Item("brsr_position_id")
                brsr_target_amount.EditValue = .Item("brsr_target_amount")
                brsr_insentive.EditValue = .Item("brsr_insentive")
                brsr_bobot.EditValue = .Item("brsr_bobot")
                brsr_start_date.DateTime = .Item("brsr_start_date")
                brsr_end_date.DateTime = .Item("brsr_end_date")
            End With
            brsr_en_id.Focus()
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
                                            & "  public.brsr_rule   " _
                                            & "SET  " _
                                            & "  brsr_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  brsr_en_id = " & SetInteger(brsr_en_id.EditValue) & ",  " _
                                            & "  brsr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  brsr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  brsr_position_id = " & SetInteger(brsr_position_id.EditValue) & ",  " _
                                            & "  brsr_start_date = " & SetDate(brsr_start_date.DateTime) & ",  " _
                                            & "  brsr_end_date = " & SetDate(brsr_end_date.DateTime) & ",  " _
                                            & "  brsr_target_amount = " & SetDbl(brsr_target_amount.EditValue) & ",  " _
                                            & "  brsr_insentive = " & SetDbl(brsr_insentive.EditValue) & ",  " _
                                            & "  brsr_bobot = " & SetDbl(brsr_bobot.EditValue) & ",  " _
                                            & "  brsr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  brsr_area_id = " & SetInteger(brsr_area_id.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  brsr_oid = " & SetSetring(_brsr_oid_mstr) & " "
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
                        set_row(Trim(_brsr_oid_mstr.ToString), "brsr_oid")
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
                            .Command.CommandText = "delete from brsr_mstr where brsr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("brsr_oid").ToString + "'"
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
