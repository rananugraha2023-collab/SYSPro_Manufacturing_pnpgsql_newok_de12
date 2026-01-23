Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDownTimeChartPar
    Dim _chart_uid As String
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim dt_bantu As DataTable

    Private Sub FDownTimeChartPar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        chart_en_id.Properties.DataSource = dt_bantu
        chart_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        chart_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        chart_en_id.ItemIndex = 0
    End Sub

    Private Sub chart_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chart_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(chart_en_id.EditValue, "down_reason"))
        With chart_code_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
            .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Down Time Reason", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "chart_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "chart_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "chart_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "chart_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  chart_uid, " _
                    & "  chart_name, " _
                    & "  chart_type, " _
                    & "  chart_en_id, " _
                    & "  en_desc, " _
                    & "  chart_code_id, " _
                    & "  code_name, " _
                    & "  chart_add_by, " _
                    & "  chart_add_date, " _
                    & "  chart_upd_by, " _
                    & "  chart_upd_date " _
                    & "FROM  " _
                    & "  public.tconfchart_setting " _
                    & "  inner join en_mstr on en_id = chart_en_id " _
                    & "  inner join code_mstr on code_id = chart_code_id " _
                    & " where coalesce(chart_user_id,'99') = '99'" _
                    & " and chart_type = 'LF' and chart_mode = 1 "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        chart_en_id.Focus()
        chart_en_id.ItemIndex = 0
        chart_code_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
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
                                               & "  public.tconfchart_setting " _
                                               & "( " _
                                               & "  chart_uid, " _
                                               & "  chart_code_id, " _
                                               & "  chart_type, " _
                                               & "  chart_mode, " _
                                               & "  chart_user_id, " _
                                               & "  chart_en_id, " _
                                               & "  chart_add_by, " _
                                               & "  chart_add_date " _
                                               & ")  " _
                                               & "VALUES ( " _
                                               & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                               & SetInteger(chart_code_id.EditValue) & ",  " _
                                               & SetSetring("LF") & ",  " _
                                               & SetInteger("1") & ",  " _
                                               & "null, " _
                                               & SetInteger(chart_en_id.EditValue) & ",  " _
                                               & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                               & " current_timestamp " _
                                               & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(chart_code_id.GetColumnValue("code_name")), "code_name")
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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
                _chart_uid = .Item("chart_uid").ToString
                chart_en_id.EditValue = .Item("chart_en_id")
                chart_code_id.EditValue = .Item("chart_code_id")
            End With
            chart_en_id.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
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
                                            & "  public.tconfchart_setting   " _
                                            & "SET  " _
                                            & "  chart_en_id = " & SetInteger(chart_en_id.EditValue) & ",  " _
                                            & "  chart_code_id = " & SetInteger(chart_code_id.EditValue) & ",  " _
                                            & "  chart_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  chart_upd_date = current_timestamp " & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  chart_uid = " & SetSetring(_chart_uid.ToString) & " "
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(Trim(chart_code_id.GetColumnValue("code_name")), "code_name")
                        edit = True
                    Catch ex As nPgSqlException
                        edit = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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
                            .Command.CommandText = "delete from tconfchart_setting where chart_uid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("chart_uid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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
