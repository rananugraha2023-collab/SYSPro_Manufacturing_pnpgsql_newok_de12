Imports npgsql
Imports master_new.ModFunction

Public Class FPointMaster
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String
    Dim sSQLs As New ArrayList

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Min Point", "secomm_min", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Max Point", "secomm_max", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Multiplier", "secomm_multiplier", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Type", "secomm_type", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  " _
                & "  a.secomm_oid, " _
                & "  a.secomm_min, " _
                & "  a.secomm_max, " _
                & "  a.secomm_multiplier, " _
                & "  a.secomm_type " _
                & "FROM " _
                & "  public.secomm_mstr a " _
                & "ORDER BY " _
                & "  a.secomm_min"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        secomm_min.EditValue = 0.0
        secomm_max.EditValue = 0.0
        secomm_multiplier.EditValue = 0.0
        secomm_type.EditValue = "C"

    End Sub

    Public Overrides Function insert() As Boolean

        sSQLs.Clear()

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
                                & "  public.secomm_mstr " _
                                & "( " _
                                & "  secomm_oid, " _
                                & "  secomm_min, " _
                                & "  secomm_max, " _
                                & "  secomm_multiplier, " _
                                & "  secomm_type " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                & SetDec(secomm_min.EditValue) & ",  " _
                                & SetDec(secomm_max.EditValue) & ",  " _
                                & SetDec(secomm_multiplier.EditValue) & ",  " _
                                & SetSetring(secomm_type.EditValue) & "  " _
                                & ")"


                        sSQLs.Add(.Command.CommandText)
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
                        set_row(Trim(secomm_min.EditValue), "secomm_min")
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
            'wc_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _oid_mstr = .Item("secomm_oid").ToString
                secomm_min.EditValue = .Item("secomm_min")
                secomm_max.EditValue = .Item("secomm_max")
                secomm_multiplier.EditValue = .Item("secomm_multiplier")
                secomm_type.EditValue = .Item("secomm_type")

            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        sSQLs.Clear()
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
                            & "  public.secomm_mstr   " _
                            & "SET  " _
                            & "  secomm_min = " & SetDec(secomm_min.EditValue) & ",  " _
                            & "  secomm_max = " & SetDec(secomm_max.EditValue) & ",  " _
                            & "  secomm_multiplier = " & SetDec(secomm_multiplier.EditValue) & ",  " _
                            & "  secomm_type = " & SetSetring(secomm_type.EditValue) & "  " _
                            & "WHERE  " _
                            & "  secomm_oid = " & SetSetring(_oid_mstr) & " "


                        sSQLs.Add(.Command.CommandText)

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
                        set_row(Trim(secomm_min.EditValue), "secomm_min")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Akan Menghapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If
        sSQLs.Clear()

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
                            .Command.CommandText = "delete from secomm_mstr where secomm_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("secomm_oid").ToString + "'"
                            sSQLs.Add(.Command.CommandText)
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
