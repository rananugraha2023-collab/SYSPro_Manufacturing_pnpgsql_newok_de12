Imports npgsql
Imports master_new.ModFunction

Public Class FFixAssetBookMaster

    Dim _pt_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FFixAssetBookMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "fabk_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "fabk_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Posted", "fabk_posted", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Create", "fabk_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "fabk_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "fabk_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "fabk_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  fabk_oid, " _
                    & "  fabk_dom_id, " _
                    & "  fabk_add_by, " _
                    & "  fabk_add_date, " _
                    & "  fabk_upd_by, " _
                    & "  fabk_upd_date, " _
                    & "  fabk_id, " _
                    & "  fabk_code, " _
                    & "  fabk_desc, " _
                    & "  fabk_posted, " _
                    & "  fabk_dt " _
                    & "FROM  " _
                    & "  public.fabk_mstr "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        fabk_code.Text = ""
        fabk_desc.Text = ""
        fabk_posted.Checked = False
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        'If Trim(fabk_code.Text) = "" Then
        '    MessageBox.Show("Code Can't Empty..", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    fabk_code.Focus()
        '    before_save = False
        '    MessageBox.Show(pt_pl_id.Text)
        'End If
        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim _fabk_oid As Guid
        _fabk_oid = Guid.NewGuid

        Dim _fabk_id As Integer
        _fabk_id = SetNewID_OLD("fabk_mstr", "fabk_id")

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
                                            & "  public.fabk_mstr " _
                                            & "( " _
                                            & "  fabk_oid, " _
                                            & "  fabk_dom_id, " _
                                            & "  fabk_add_by, " _
                                            & "  fabk_add_date, " _
                                            & "  fabk_id, " _
                                            & "  fabk_code, " _
                                            & "  fabk_desc, " _
                                            & "  fabk_posted, " _
                                            & "  fabk_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_fabk_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_fabk_id) & ",  " _
                                            & SetSetring(fabk_code.Text) & ",  " _
                                            & SetSetring(fabk_desc.Text) & ",  " _
                                            & SetBitYN(fabk_posted.EditValue) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
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
                        set_row(Trim(fabk_code.Text), "fabk_code")
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
            fabk_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pt_oid = .Item("fabk_oid").ToString
                fabk_code.Text = .Item("fabk_code")
                fabk_desc.Text = SetString(.Item("fabk_desc"))
                fabk_posted.EditValue = SetBitYNB(.Item("fabk_posted"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        Dim ssqls As New ArrayList
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
                                            & "  public.fabk_mstr   " _
                                            & "SET  " _
                                            & "  fabk_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  fabk_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  fabk_upd_date = current_timestamp,  " _
                                            & "  fabk_code = " & SetSetring(fabk_code.Text) & ",  " _
                                            & "  fabk_desc = " & SetSetring(fabk_desc.Text) & ",  " _
                                            & "  fabk_posted = " & SetBitYN(fabk_posted.EditValue) & ",  " _
                                            & "  fabk_dt = current_timestamp  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  fabk_oid = " & SetSetring(_pt_oid.ToString) & " " _
                                            & ";"
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
                        set_row(Trim(fabk_code.Text), "fabk_code")
                        edit = True
                    Catch ex As nPgSqlException
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
        Dim ssqls As New ArrayList
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
                            .Command.CommandText = "delete from fabk_mstr where fabk_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("fabk_oid").ToString + "'"
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


