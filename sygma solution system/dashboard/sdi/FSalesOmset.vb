Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FSalesOmset
    Dim ssql As String
    Dim _omz_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        With omz_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_cust", omz_en_id.EditValue))

    End Sub

    Private Sub sc_le_bk_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles omz_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Omset Code", "omz_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "omz_date", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.DateTime, "y")
        add_column_copy(gv_master, "Target", "omz_target", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "omz_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "omz_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "omz_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "omz_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.omz_oid, " _
                    & "  a.omz_date, " _
                    & "  a.omz_target, " _
                    & "  a.omz_add_by, " _
                    & "  a.omz_add_date, " _
                    & "  a.omz_upd_by, " _
                    & "  a.omz_upd_date, " _
                    & "  a.omz_en_id, " _
                    & "  a.omz_code, " _
                    & "  b.en_desc " _
                    & "FROM " _
                    & "  public.omz_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.omz_en_id = b.en_id) " _
                    & " WHERE omz_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        omz_en_id.Focus()
        omz_code.Text = ""
        omz_target.Text = ""
        omz_date.Text = Date.Now
        omz_en_id.ItemIndex = 0


    End Sub

    Public Overrides Function insert() As Boolean
        Dim _omz_oid As Guid
        _omz_oid = Guid.NewGuid
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
                                            & "  public.omz_mstr " _
                                            & "( " _
                                            & "  omz_oid, " _
                                            & "  omz_date, " _
                                            & "  omz_target, " _
                                            & "  omz_add_by, " _
                                            & "  omz_add_date, " _
                                            & "  omz_en_id, " _
                                            & "  omz_code " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_omz_oid.ToString) & ",  " _
                                            & SetDate(omz_date.EditValue) & ",  " _
                                            & SetSetring(omz_target.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetSetring(omz_en_id.EditValue) & ",  " _
                                            & SetSetring(omz_code.Text) & "  " _
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
                        set_row(Trim(omz_code.Text), "omz_code")
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
            omz_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _omz_oid = .Item("omz_oid").ToString
                omz_code.Text = SetString(.Item("omz_code"))
                omz_target.Text = SetString(.Item("omz_target"))
                omz_en_id.EditValue = .Item("omz_en_id")
                omz_date.Text = .Item("omz_date")
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _omz_code As String
        Dim ssqls As New ArrayList

        _omz_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("omz_code")

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
                                                & "  public.omz_mstr   " _
                                                & "SET  " _
                                                & "  omz_date = " & SetDate(omz_date.EditValue) & ",  " _
                                                & "  omz_target = " & SetSetring(omz_target.EditValue) & ",  " _
                                                & "  omz_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  omz_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & "  omz_en_id = " & SetSetring(omz_en_id.EditValue) & ",  " _
                                                & "  omz_code = " & SetSetring(omz_code.Text) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  omz_oid = '" & _omz_oid & "' "

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
                        set_row(_omz_oid, "omz_code")
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from akses.omz_mstr where omz_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("omz_oid").ToString + "'"
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

