Imports npgsql
Imports master_new.ModFunction

Public Class FLocationGroupForecast
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _wc_oid_mstr As String
    Dim sSQLs As New ArrayList

    Private Sub FWc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        locgr_en_id.Properties.DataSource = dt_bantu
        locgr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        locgr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        locgr_en_id.ItemIndex = 0

    End Sub

    Private Sub wc_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles locgr_en_id.EditValueChanged

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(locgr_en_id.EditValue))
        locgr_loc_id.Properties.DataSource = dt_bantu
        locgr_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        locgr_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        locgr_loc_id.ItemIndex = 0

    End Sub

    Public Overrides Sub load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Part Number", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description 1", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "locgr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "locgr_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "locgr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "locgr_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.locgr_oid, " _
                    & "  a.locgr_en_id, " _
                    & "  a.locgr_loc_id, " _
                    & "  a.locgr_add_by, " _
                    & "  a.locgr_add_date, " _
                    & "  a.locgr_upd_by, " _
                    & "  a.locgr_upd_date, " _
                    & "  b.en_desc, " _
                    & "  c.loc_desc " _
                    & "FROM " _
                    & "  public.locgr_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.locgr_en_id = b.en_id) " _
                    & "  INNER JOIN public.loc_mstr c ON (a.locgr_loc_id = c.loc_id) " _
                    & "WHERE " _
                    & "  a.locgr_en_id IN (select user_en_id from tconfuserentity " _
                        & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & "ORDER BY " _
                    & "  c.loc_desc"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        locgr_en_id.ItemIndex = 0
        locgr_loc_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _wc_oid As Guid
        _wc_oid = Guid.NewGuid

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
                            & "  public.locgr_mstr " _
                            & "( " _
                            & "  locgr_oid, " _
                            & "  locgr_en_id, " _
                            & "  locgr_loc_id, " _
                            & "  locgr_add_by, " _
                            & "  locgr_add_date " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(_wc_oid.ToString) & ",  " _
                            & SetInteger(locgr_en_id.EditValue) & ",  " _
                            & SetInteger(locgr_loc_id.EditValue) & ",  " _
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
                        set_row(Trim(_wc_oid.ToString), "locgr_oid")
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
                _wc_oid_mstr = .Item("locgr_oid").ToString
                locgr_en_id.EditValue = .Item("locgr_en_id")
                locgr_loc_id.EditValue = .Item("locgr_loc_id")
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
                            & "  public.locgr_mstr   " _
                            & "SET  " _
                            & "  locgr_en_id = " & SetInteger(locgr_en_id.EditValue) & ",  " _
                            & "  locgr_loc_id = " & SetInteger(locgr_loc_id.EditValue) & ",  " _
                            & "  locgr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                            & "  locgr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                            & "WHERE  " _
                            & "  locgr_oid = " & SetSetring(_wc_oid_mstr.ToString) & " "

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
                        set_row(Trim(_wc_oid_mstr.ToString), "locgr_oid")
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
                            .Command.CommandText = "delete from locgr_mstr where locgr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("locgr_oid").ToString + "'"
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
