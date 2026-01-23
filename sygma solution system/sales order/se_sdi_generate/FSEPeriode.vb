Imports npgsql
Imports master_new.ModFunction

Public Class FSEPeriode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim __seperiode_code As String
    Dim sSQLs As New ArrayList

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Public Overrides Sub load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Periode Code", "seperiode_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "seperiode_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "seperiode_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Generate Bonus", "seperiode_bonus_gen", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "seperiode_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
     

        get_sequel = "SELECT  " _
                & "  a.seperiode_code, " _
                & "  a.seperiode_start_date, " _
                & "  a.seperiode_end_date, " _
                & "  a.seperiode_bonus_gen, " _
                & "  a.seperiode_payment_date, " _
                & "  a.seperiode_remarks " _
                & "FROM " _
                & "  public.seperiode_mstr a " _
                & "ORDER BY " _
                & "  a.seperiode_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        seperiode_bonus_gen.EditValue = False
        seperiode_code.EditValue = ""
        seperiode_end_date.EditValue = master_new.PGSqlConn.CekTanggal
        seperiode_start_date.EditValue = master_new.PGSqlConn.CekTanggal
        seperiode_payment_date.EditValue = master_new.PGSqlConn.CekTanggal
        seperiode_remarks.EditValue = ""

    End Sub

    Public Overrides Function insert() As Boolean

        If seperiode_code.EditValue = "" Then
            Box("Periode Code can't empty")
            Exit Function
        End If

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
                                & "  public.seperiode_mstr " _
                                & "( " _
                                & "  seperiode_code, " _
                                & "  seperiode_start_date, " _
                                & "  seperiode_end_date, " _
                                & "  seperiode_bonus_gen, " _
                                & "  seperiode_payment_date, " _
                                & "  seperiode_remarks " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(seperiode_code.EditValue) & ",  " _
                                & SetDateNTime00(seperiode_start_date.EditValue) & ",  " _
                                & SetDateNTime00(seperiode_end_date.EditValue) & ",  " _
                                & SetBitYN(seperiode_bonus_gen.EditValue) & ",  " _
                                & SetDateNTime00(seperiode_payment_date.EditValue) & ",  " _
                                & SetSetring(seperiode_remarks.EditValue) & "  " _
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
                        set_row(Trim(seperiode_code.EditValue), "seperiode_code")
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
                __seperiode_code = .Item("seperiode_code")
                seperiode_bonus_gen.EditValue = SetBitYNB(.Item("seperiode_bonus_gen"))
                seperiode_code.EditValue = .Item("seperiode_code")
                seperiode_start_date.EditValue = .Item("seperiode_start_date")
                seperiode_end_date.EditValue = .Item("seperiode_end_date")
                seperiode_payment_date.EditValue = .Item("seperiode_payment_date")
                seperiode_remarks.EditValue = .Item("seperiode_remarks")
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
                            & "  public.seperiode_mstr   " _
                            & "SET  " _
                            & "  seperiode_bonus_gen = " & SetBitYN(seperiode_bonus_gen.EditValue) & ",  " _
                            & "  seperiode_code = " & SetSetring(seperiode_code.EditValue) & ",  " _
                            & "  seperiode_start_date = " & SetDateNTime00(seperiode_start_date.EditValue) & ",  " _
                            & "  seperiode_end_date = " & SetDateNTime00(seperiode_end_date.EditValue) & ",  " _
                            & "  seperiode_payment_date = " & SetDateNTime00(seperiode_payment_date.EditValue) & ",  " _
                            & "  seperiode_remarks = " & SetSetring(seperiode_remarks.EditValue) & "  " _
                            & "WHERE  " _
                            & "  seperiode_code = " & SetSetring(__seperiode_code) & " "

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
                        set_row(Trim(__seperiode_code.ToString), "seperiode_code")
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
                            .Command.CommandText = "delete from seperiode_mstr where seperiode_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("seperiode_code") + "'"
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
