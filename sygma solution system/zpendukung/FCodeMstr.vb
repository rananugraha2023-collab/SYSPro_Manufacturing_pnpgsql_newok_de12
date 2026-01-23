Imports npgsql
Imports master_new.ModFunction

Public Class FCodeMstr

    Public _code_field As String
    Public _code_oid_mstr As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FCodeMstr_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'form_first_load()
        'set_code_field()
        _code_field = "code_mstr"
    End Sub

    'Public Overridable Sub set_code_field()
    '    _code_field = "code_mstr"
    'End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        sc_le_code_en_id.Properties.DataSource = dt_bantu
        sc_le_code_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sc_le_code_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sc_le_code_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsDefault", "code_default", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "code_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser1", "code_usr_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser2", "code_usr_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser3", "code_usr_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser4", "code_usr_4", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "CodeUser5", "code_usr_5", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "code_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "code_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "code_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "code_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select code_oid, code_dom_id, code_en_id, en_desc, code_add_by, code_add_date, " + _
                     " code_upd_by, code_upd_date, code_id, code_seq, code_field, " + _
                     " code_code, code_name, code_desc, code_default, " + _
                     " code_parent, code_usr_1, code_usr_2, code_usr_3, " + _
                     " code_usr_4, code_usr_5, code_active, code_dt from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where code_field ~~* '" + _code_field + "'" + _
                     " and code_en_id in (select user_en_id from tconfuserentity " + _
                                        " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_code_en_id.Focus()
        sc_le_code_en_id.ItemIndex = 0
        sc_te_code_code.Text = ""
        sc_te_code_desc.Text = ""
        sc_te_code_name.Text = ""
        sc_te_code_usr_1.Text = ""
        sc_te_code_usr_2.Text = ""
        sc_te_code_usr_3.Text = ""
        sc_te_code_usr_4.Text = ""
        sc_te_code_usr_5.Text = ""
        sc_ce_code_active.EditValue = False
        sc_ce_code_default.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _code_oid As Guid
        _code_oid = Guid.NewGuid
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
                                            & "  public.code_mstr " _
                                            & "( " _
                                            & "  code_oid, " _
                                            & "  code_dom_id, " _
                                            & "  code_en_id, " _
                                            & "  code_add_by, " _
                                            & "  code_add_date, " _
                                            & "  code_id, " _
                                            & "  code_seq, " _
                                            & "  code_field, " _
                                            & "  code_code, " _
                                            & "  code_name, " _
                                            & "  code_desc, " _
                                            & "  code_default, " _
                                            & "  code_usr_1, " _
                                            & "  code_usr_2, " _
                                            & "  code_usr_3, " _
                                            & "  code_usr_4, " _
                                            & "  code_usr_5, " _
                                            & "  code_active, " _
                                            & "  code_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_code_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sc_le_code_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("code_mstr", "code_id")) & ",  " _
                                            & SetInteger(func_coll.GetID("code_mstr", sc_le_code_en_id.GetColumnValue("en_code"), "code_seq", "code_field", _code_field)) & ",  " _
                                            & SetSetring(_code_field) & ",  " _
                                            & SetSetring(sc_te_code_code.Text) & ",  " _
                                            & SetSetring(sc_te_code_name.Text) & ",  " _
                                            & SetSetring(sc_te_code_desc.Text) & ",  " _
                                            & SetBitYN(sc_ce_code_default.EditValue) & ",  " _
                                            & SetSetring(sc_te_code_usr_1.Text) & ",  " _
                                            & SetSetring(sc_te_code_usr_2.Text) & ",  " _
                                            & SetSetring(sc_te_code_usr_3.Text) & ",  " _
                                            & SetSetring(sc_te_code_usr_4.Text) & ",  " _
                                            & SetSetring(sc_te_code_usr_5.Text) & ",  " _
                                            & SetBitYN(sc_ce_code_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If sc_ce_code_default.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'N'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'Y' where code_oid = '" + _code_oid.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'N' where code_oid = '" + _code_oid.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

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
            sc_le_code_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _code_oid_mstr = .Item("code_oid").ToString
                sc_le_code_en_id.EditValue = .Item("code_en_id")
                sc_te_code_code.EditValue = .Item("code_code")
                sc_te_code_name.EditValue = .Item("code_name")
                sc_te_code_desc.EditValue = .Item("code_desc")
                sc_te_code_usr_1.EditValue = SetString(.Item("code_usr_1"))
                sc_te_code_usr_2.EditValue = SetString(.Item("code_usr_2"))
                sc_te_code_usr_3.EditValue = SetString(.Item("code_usr_3"))
                sc_te_code_usr_4.EditValue = SetString(.Item("code_usr_4"))
                sc_te_code_usr_5.EditValue = SetString(.Item("code_usr_5"))
                sc_ce_code_default.EditValue = IIf(.Item("code_default") = "Y", True, False)
                sc_ce_code_active.EditValue = IIf(.Item("code_active") = "Y", True, False)
            End With

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
                        .Command.CommandText = "update code_mstr set code_en_id = " + sc_le_code_en_id.EditValue.ToString + "," + _
                                               " code_upd_by = '" + master_new.ClsVar.sNama + "'," + _
                                               " code_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")," + _
                                               " code_code = '" + Trim(sc_te_code_code.Text) + "'," + _
                                               " code_name = '" + Trim(sc_te_code_name.Text) + "'," + _
                                               " code_desc = '" + Trim(sc_te_code_desc.Text) + "'," + _
                                               " code_usr_1 = '" + Trim(sc_te_code_usr_1.Text) + "'," + _
                                               " code_usr_2 = '" + Trim(sc_te_code_usr_2.Text) + "'," + _
                                               " code_usr_3 = '" + Trim(sc_te_code_usr_3.Text) + "'," + _
                                               " code_usr_4 = '" + Trim(sc_te_code_usr_4.Text) + "'," + _
                                               " code_usr_5 = '" + Trim(sc_te_code_usr_5.Text) + "'," + _
                                               " code_active = " + IIf(sc_ce_code_active.EditValue = True, "'Y'", "'N'") + "," + _
                                               " code_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" + _
                                               " where code_oid = '" + _code_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If sc_ce_code_default.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'N'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'Y' where code_oid = '" + _code_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update code_mstr set code_default = 'N' where code_oid = '" + _code_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

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
                            .Command.CommandText = "delete from code_mstr where code_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_oid").ToString + "'"
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
