Imports npgsql
Imports master_new.ModFunction

Public Class FOperation

    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
     
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "op_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "op_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "op_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "op_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "op_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "op_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "op_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "op_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
       
        get_sequel = "SELECT  " _
                & "  a.op_code, " _
                & "  a.op_name, " _
                & "  a.op_desc, " _
                & "  a.op_active, " _
                & "  a.op_add_by, " _
                & "  a.op_add_date, " _
                & "  a.op_upd_by, " _
                & "  a.op_upd_date " _
                & "FROM " _
                & "  public.op_mstr a " _
                & "ORDER BY " _
                & "  a.op_name"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        op_code.Text = 0
        op_name.Text = ""
        op_active.EditValue = True
        op_desc.Text = ""
        op_code.Enabled = True
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _si_oid As Guid
        _si_oid = Guid.NewGuid
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
                                            & "  public.op_mstr " _
                                            & "( " _
                                            & "  op_code, " _
                                            & "  op_name, " _
                                            & "  op_desc, " _
                                            & "  op_active, " _
                                            & "  op_add_by, " _
                                            & "  op_add_date " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetDec(op_code.EditValue) & ",  " _
                                            & SetSetring(op_name.EditValue) & ",  " _
                                            & SetSetring(op_desc.EditValue) & ",  " _
                                            & SetBitYN(op_active.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetSetring(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Operation " & op_code.EditValue)
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
                        set_row(Trim(op_code.Text), "op_code")
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
                op_code.EditValue = .Item("op_code")
                op_code.Enabled = False
                op_name.EditValue = .Item("op_name")
                op_active.EditValue = SetBitYNB(.Item("op_active"))
                op_desc.EditValue = .Item("op_desc")
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
                        .Command.CommandText = "UPDATE  " _
                            & "  public.op_mstr   " _
                            & "SET  " _
                            & "  op_name = " & SetSetring(op_name.EditValue) & ",  " _
                            & "  op_desc = " & SetSetring(op_desc.EditValue) & ",  " _
                            & "  op_active = " & SetBitYN(op_active.EditValue) & ",  " _
                            & "  op_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  op_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                            & "WHERE  " _
                            & "  op_code = " & SetSetring(op_code.EditValue) & "  "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Operation " & op_name.EditValue)
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
                        set_row(Trim(op_code.Text), "op_code")
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

            Dim sSQLs As New ArrayList
            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from op_mstr where op_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("op_code") + "'"
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
