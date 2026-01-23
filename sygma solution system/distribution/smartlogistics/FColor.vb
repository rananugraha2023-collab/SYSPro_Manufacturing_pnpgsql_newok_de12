Imports npgsql
Imports master_new.ModFunction

Public Class FColor

    Dim _dom_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "color_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "color_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "color_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "color_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "color_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "color_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "color_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")


    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  color_code, " _
                & "  color_name, " _
                & "  color_active, " _
                & "  color_add_by, " _
                & "  color_add_date, " _
                & "  color_upd_by, " _
                & "  color_upd_date " _
                & "FROM  " _
                & "  public.color_mstr " _
                & " order by color_name"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        color_code.Focus()
        color_code.Text = ""
        color_name.Text = ""
        color_active.EditValue = True
        
    End Sub

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList

        Dim _dom_oid As Guid
        _dom_oid = Guid.NewGuid
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
                                        & "  public.color_mstr " _
                                        & "( " _
                                        & "  color_code, " _
                                        & "  color_name, " _
                                        & "  color_active, " _
                                        & "  color_add_by, " _
                                        & "  color_add_date " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(color_code.EditValue) & ",  " _
                                        & SetSetring(color_name.EditValue) & ",  " _
                                        & SetBitYN(color_active.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
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
            color_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _dom_oid = .Item("color_code")
                color_code.Text = .Item("color_code")
                color_name.Text = .Item("color_name")
                color_active.EditValue = SetBitYNB(.Item("color_active"))
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
                                        & "  public.color_mstr  " _
                                        & "SET  " _
                                        & "  color_code = " & SetSetring(color_code.EditValue) & " , " _
                                        & "  color_name = " & SetSetring(color_name.EditValue) & ",  " _
                                        & "  color_active = " & SetBitYN(color_active.EditValue) & ",  " _
                                        & "  color_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  color_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                        & "WHERE  " _
                                        & "  color_code = " & SetSetring(_dom_oid) & " "


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
                            .Command.CommandText = "delete from color_mstr where color_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("color_code") + "'"
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
