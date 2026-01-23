Imports npgsql
Imports master_new.ModFunction


Public Class FPSPeriode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wc_oid_mstr As String
    Dim sSQLs As New ArrayList

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
       
    End Sub

    Public Overrides Sub load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Periode Code", "periode_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "periode_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "periode_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Group Name", "periode_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "periode_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "periode_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "periode_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "periode_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.periode_oid, " _
                & "  a.periode_code, " _
                & "  a.periode_start_date, " _
                & "  a.periode_end_date, " _
                & "  a.periode_active, " _
                & "  a.periode_add_by, " _
                & "  a.periode_add_date, " _
                & "  a.periode_upd_by, " _
                & "  a.periode_upd_date " _
                & "FROM " _
                & "  public.psperiode_mstr a " _
                & "ORDER BY " _
                & "  a.periode_code"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        periode_active.EditValue = True
        periode_code.EditValue = ""
        periode_end_date.EditValue = master_new.PGSqlConn.CekTanggal
        periode_start_date.EditValue = master_new.PGSqlConn.CekTanggal
    End Sub

    Public Overrides Function insert() As Boolean

        If periode_code.EditValue = "" Then
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
                            & "  public.psperiode_mstr " _
                            & "( " _
                            & "  periode_oid, " _
                            & "  periode_code, " _
                            & "  periode_start_date, " _
                            & "  periode_end_date, " _
                            & "  periode_active, " _
                            & "  periode_add_by, " _
                            & "  periode_add_date " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                            & SetSetring(periode_code.EditValue) & ",  " _
                            & SetDateNTime00(periode_start_date.EditValue) & ",  " _
                            & SetDateNTime00(periode_end_date.EditValue) & ",  " _
                            & SetBitYN(periode_active.EditValue) & ",  " _
                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
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
                        set_row(Trim(periode_code.EditValue), "periode_code")
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
                _wc_oid_mstr = .Item("periode_oid").ToString
                periode_active.EditValue = SetBitYNB(.Item("periode_active"))
                periode_code.EditValue = .Item("periode_code")
                periode_start_date.EditValue = .Item("periode_start_date")
                periode_end_date.EditValue = .Item("periode_end_date")


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
                            & "  public.psperiode_mstr   " _
                            & "SET  " _
                            & "  periode_active = " & SetBitYN(periode_active.EditValue) & ",  " _
                            & "  periode_code = " & SetSetring(periode_code.EditValue) & ",  " _
                            & "  periode_start_date = " & SetDateNTime00(periode_start_date.EditValue) & ",  " _
                            & "  periode_end_date = " & SetDateNTime00(periode_end_date.EditValue) & ",  " _
                            & "  periode_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  periode_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                            & "WHERE  " _
                            & "  periode_oid = " & SetSetring(_wc_oid_mstr) & " "

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
                        set_row(Trim(_wc_oid_mstr.ToString), "periode_oid")
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
                            .Command.CommandText = "delete from psperiode_mstr where periode_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("periode_oid").ToString + "'"
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

    Private Sub ptgr_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Dim frm As New FPartNumberSearch()
        frm.set_win(Me)
        frm._en_id = 1
        frm._obj = ""
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
