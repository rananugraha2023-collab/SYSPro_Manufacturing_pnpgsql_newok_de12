Imports npgsql
Imports master_new.ModFunction

Public Class FVerificationValue

    Dim _sqr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ver_category())
        sqr_code_id.Properties.DataSource = dt_bantu
        sqr_code_id.Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        sqr_code_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sqr_code_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Verification Category", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Verification Category2", "code_field", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Value", "sqr_value", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Plus Value", "sqr_plus", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Verification Dt", "sqr_dt", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select sqr_oid, " _
                    & "sqr_id, " _
                    & "sqr_code_id,code_name,code_desc,code_field,sqr_value, " _
                    & "sqr_plus,sqr_dt  " _
                    & "from sqr_rate " _
                    & "inner join code_mstr on code_id=sqr_code_id"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sqr_code_id.Focus()
        sqr_plus.EditValue = True
        sqr_code_id.ItemIndex = 0
        sqr_value.EditValue = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList

        Dim _sqr_oid As Guid
        _sqr_oid = Guid.NewGuid
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
                                            & "  public.sqr_rate " _
                                            & "( " _
                                            & "  sqr_oid, " _
                                            & "  sqr_id, " _
                                            & "  sqr_code_id, " _
                                            & "  sqr_value, " _
                                            & "  sqr_plus, " _
                                            & "  sqr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sqr_oid.ToString) & ",  " _
                                            & SetInteger(func_coll.GetID("sqr_rate", "sqr_id")) & ",  " _
                                            & SetInteger(sqr_code_id.EditValue) & ",  " _
                                            & SetInteger(sqr_value.Text) & ",  " _
                                            & SetBitYN(sqr_plus.EditValue) & ",  " _
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
            sqr_code_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _sqr_oid = .Item("sqr_oid")
                sqr_code_id.EditValue = .Item("sqr_code_id")
                sqr_code_id.Text = .Item("code_desc")
                sqr_value.EditValue = .Item("sqr_value")
                sqr_plus.EditValue = SetBitYNB(.Item("sqr_plus"))
            End With
            sqr_code_id.ClosePopup()
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
                                            & "  public.sqr_rate   " _
                                            & "SET  " _
                                            & "  sqr_code_id = " & SetInteger(sqr_code_id.EditValue) & ",  " _
                                            & "  sqr_value = " & SetInteger(sqr_value.EditValue) & ",  " _
                                            & "  sqr_plus = " & SetBitYN(sqr_plus.EditValue) & ",  " _
                                            & "  sqr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sqr_oid = " & SetSetring(_sqr_oid.ToString) & " "
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
                            .Command.CommandText = "delete from sqr_rate where sqr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sqr_oid") + "'"
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


    Private Sub sqr_code_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sqr_code_id.EditValueChanged
        Try
            sqr_value.EditValue = SetNumber(sqr_code_id.GetColumnValue("code_usr_1"))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
