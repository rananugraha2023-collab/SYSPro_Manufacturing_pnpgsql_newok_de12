Imports npgsql
Imports master_new.ModFunction

Public Class FDocumentApproval
    Dim _aprvd_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As Date

    Private Sub FDocumentApproval_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        aprvd_en_id.Properties.DataSource = dt_bantu
        aprvd_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        aprvd_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        aprvd_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_type_document())
        aprvd_type.Properties.DataSource = dt_bantu
        aprvd_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        aprvd_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        aprvd_type.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "aprvd_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "aprvd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name 1", "aprvd_name_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position 1", "aprvd_pos_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name 2", "aprvd_name_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position 2", "aprvd_pos_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name 3", "aprvd_name_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position 3", "aprvd_pos_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name 4", "aprvd_name_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Position 4", "aprvd_pos_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Eff. Date", "aprvd_start_eff", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "End Eff. Date", "aprvd_end_eff", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "aprvd_active", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  aprvd_oid, " _
                    & "  aprvd_dom_id, " _
                    & "  aprvd_en_id, " _
                    & "  aprvd_add_by, " _
                    & "  aprvd_add_date, " _
                    & "  aprvd_upd_by, " _
                    & "  aprvd_upd_date, " _
                    & "  aprvd_dt, " _
                    & "  aprvd_type, " _
                    & "  aprvd_desc, " _
                    & "  aprvd_name_1, " _
                    & "  aprvd_name_2, " _
                    & "  aprvd_name_3, " _
                    & "  aprvd_name_4, " _
                    & "  aprvd_pos_1, " _
                    & "  aprvd_pos_2, " _
                    & "  aprvd_pos_3, " _
                    & "  aprvd_pos_4, " _
                    & "  aprvd_start_eff, " _
                    & "  aprvd_end_eff, " _
                    & "  aprvd_active, " _
                    & "  en_desc " _
                    & "FROM  " _
                    & "  public.aprvd_dok " _
                    & "  inner join en_mstr on en_id = aprvd_en_id" _
                    & " where aprvd_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        aprvd_en_id.Focus()
        aprvd_en_id.ItemIndex = 0
        aprvd_type.ItemIndex = 0
        aprvd_name_1.Text = ""
        aprvd_pos_1.text  = ""
        aprvd_name_2.Text = ""
        aprvd_pos_2.text  = ""
        aprvd_name_3.Text = ""
        aprvd_pos_3.text  = ""
        aprvd_name_4.Text = ""
        aprvd_pos_4.text  = ""
        aprvd_start_eff.DateTime = _now
        aprvd_end_eff.DateTime = _now
        aprvd_active.EditValue = False

        aprvd_start_eff.enabled = true
        aprvd_end_eff.enabled = true 
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _aprvd_oid As Guid
        _aprvd_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        if before_save_conf = false then
            return false
            exit Function
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
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.aprvd_dok " _
                                            & "( " _
                                            & "  aprvd_oid, " _
                                            & "  aprvd_dom_id, " _
                                            & "  aprvd_en_id, " _
                                            & "  aprvd_add_by, " _
                                            & "  aprvd_add_date, " _
                                            & "  aprvd_dt, " _
                                            & "  aprvd_type, " _
                                            & "  aprvd_desc, " _
                                            & "  aprvd_name_1, " _
                                            & "  aprvd_pos_1, " _
                                            & "  aprvd_name_2, " _
                                            & "  aprvd_pos_2, " _
                                            & "  aprvd_name_3, " _
                                            & "  aprvd_pos_3, " _
                                            & "  aprvd_name_4, " _
                                            & "  aprvd_pos_4, " _
                                            & "  aprvd_start_eff, " _
                                            & "  aprvd_end_eff, " _
                                            & "  aprvd_active " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_aprvd_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(aprvd_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetInteger(aprvd_type.EditValue) & ",  " _
                                            & SetSetring(aprvd_type.GetColumnValue("display")) & ",  " _
                                            & SetSetring(aprvd_name_1.Text) & ",  " _
                                            & SetSetring(aprvd_pos_1.Text) & ",  " _
                                            & SetSetring(aprvd_name_2.Text) & ",  " _
                                            & SetSetring(aprvd_pos_2.Text) & ",  " _
                                            & SetSetring(aprvd_name_3.Text) & ",  " _
                                            & SetSetring(aprvd_pos_3.Text) & ",  " _
                                            & SetSetring(aprvd_name_4.Text) & ",  " _
                                            & SetSetring(aprvd_pos_4.Text) & ",  " _
                                            & SetDate(aprvd_start_eff.Text) & ",  " _
                                            & SetDate(aprvd_end_eff.Text) & ",  " _
                                            & SetBitYN(aprvd_active.EditValue) & "  " _
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
                        set_row(Trim(_aprvd_oid.ToString), "aprvd_oid")
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
                _aprvd_oid_mstr = .Item("aprvd_oid").ToString
                aprvd_active.EditValue = SetBitYNB(.Item("aprvd_active"))
                aprvd_en_id.EditValue = .Item("aprvd_en_id")
                aprvd_name_1.Text = SetString(.Item("aprvd_name_1"))
                aprvd_pos_1.Text = SetString(.Item("aprvd_pos_1"))
                aprvd_name_2.Text = SetString(.Item("aprvd_name_2"))
                aprvd_pos_2.Text = SetString(.Item("aprvd_pos_2"))
                aprvd_name_3.Text = SetString(.Item("aprvd_name_3"))
                aprvd_pos_3.Text = SetString(.Item("aprvd_pos_3"))
                aprvd_name_4.Text = SetString(.Item("aprvd_name_4"))
                aprvd_pos_4.Text = SetString(.Item("aprvd_pos_4"))
                aprvd_start_eff.DateTime = .Item("aprvd_start_eff")
                aprvd_end_eff.DateTime = .Item("aprvd_end_eff")
                aprvd_type.EditValue = .Item("aprvd_type")
            End With
            aprvd_en_id.Focus()
            aprvd_start_eff.Enabled = True
            aprvd_end_eff.Enabled = True
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
                                            & "  public.aprvd_dok   " _
                                            & "SET  " _
                                            & "  aprvd_dom_id = " & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  aprvd_en_id = " & SetSetring(aprvd_en_id.EditValue) & ",  " _
                                            & "  aprvd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  aprvd_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  aprvd_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  aprvd_type = " & SetInteger(aprvd_type.EditValue) & ",  " _
                                            & "  aprvd_name_1 = " & SetSetring(aprvd_name_1.Text) & ",  " _
                                            & "  aprvd_pos_1 = " & SetSetring(aprvd_pos_1.Text) & ",  " _
                                            & "  aprvd_name_2 = " & SetSetring(aprvd_name_2.Text) & ",  " _
                                            & "  aprvd_pos_2 = " & SetSetring(aprvd_pos_2.Text) & ",  " _
                                            & "  aprvd_name_3 = " & SetSetring(aprvd_name_3.Text) & ",  " _
                                            & "  aprvd_pos_3 = " & SetSetring(aprvd_pos_3.Text) & ",  " _
                                            & "  aprvd_name_4 = " & SetSetring(aprvd_name_4.Text) & ",  " _
                                            & "  aprvd_pos_4 = " & SetSetring(aprvd_pos_4.Text) & ",  " _
                                            & "  aprvd_start_eff = " & SetDate(aprvd_start_eff.Text) & ",  " _
                                            & "  aprvd_end_eff = " & SetDate(aprvd_end_eff.Text) & ",  " _
                                            & "  aprvd_active = " & SetBitYN(aprvd_active.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  aprvd_oid = " & SetSetring(_aprvd_oid_mstr.ToString) & " "

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
                        set_row(Trim(_aprvd_oid_mstr), "aprvd_oid")
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
                            .Command.CommandText = "delete from aprvd_dok where aprvd_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("aprvd_oid").ToString + "'"
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

    private Function before_save_conf() As Boolean
        before_save_conf = True

        Dim _end_date As Date = "01/01/1909"
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select aprvd_end_eff from aprvd_dok " + _
                                           " where aprvd_type = " + aprvd_type.EditValue.ToString + _
                                           " and aprvd_en_id = " + aprvd_en_id.EditValue.ToString + _
                                           " order by aprvd_end_eff desc limit 1"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _end_date = .DataReader("aprvd_end_eff").ToString
                    End While

                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        If _end_date > aprvd_start_eff.DateTime Then
            MessageBox.Show("Date Ranges May Not Overlap...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            aprvd_start_eff.Focus()
            Return False
        End If

        Return before_save
    End Function
End Class
