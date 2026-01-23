Imports npgsql
Imports master_new.ModFunction

Public Class FSalesProgramNew
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
        add_column_copy(gv_master, "Code", "sls_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "sls_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Desc", "sls_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Active", "sls_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Add By", "sls_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sls_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Upd By", "sls_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sls_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        'add_column_copy(gv_master, "Min Point", "secomm_min", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Max Point", "secomm_max", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Multiplier", "secomm_multiplier", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        'add_column_copy(gv_master, "Type", "secomm_type", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  " _
                & "  a.sls_code, " _
                & "  a.sls_name, " _
                & "  a.sls_desc, " _
                & "  a.sls_active, " _
                & "  a.sls_add_by, " _
                & "  a.sls_add_date, " _
                & "  a.sls_upd_by, " _
                & "  a.sls_upd_date " _
                & "FROM " _
                & "  public.sls_program a " _
                & "ORDER BY " _
                & "  a.sls_name"



        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sls_code.EditValue = ""
        sls_name.EditValue = ""
        sls_desc.EditValue = ""
        sls_active.Checked = True
        sls_code.Enabled = True
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
                                & "  public.sls_program " _
                                & "( " _
                                & "  sls_code, " _
                                & "  sls_name, " _
                                & "  sls_desc, " _
                                & "  sls_active, " _
                                & "  sls_add_by, " _
                                & "  sls_add_date, " _
                                & "  sls_upd_by, " _
                                & "  sls_upd_date " _
                                & ") " _
                                & "VALUES ( " _
                                & SetSetring(sls_code.EditValue) & ",  " _
                                & SetSetring(sls_name.EditValue) & ",  " _
                                & SetSetring(sls_desc.EditValue) & ",  " _
                                & SetBitYN(sls_active.EditValue) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                & SetSetring("") & ",  " _
                                & SetSetring("") & "  " _
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
                        set_row(Trim(sls_code.EditValue), "sls_code")
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
                _oid_mstr = .Item("sls_code")
                sls_code.EditValue = .Item("sls_code")
                sls_name.EditValue = .Item("sls_name")
                sls_desc.EditValue = .Item("sls_desc")
                sls_active.Checked = SetBitYNB(.Item("sls_active"))
                sls_code.Enabled = False
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

                        '.Command.CommandText = "UPDATE  " _
                        '    & "  public.secomm_mstr   " _
                        '    & "SET  " _
                        '    & "  secomm_min = " & SetDec(secomm_min.EditValue) & ",  " _
                        '    & "  secomm_max = " & SetDec(secomm_max.EditValue) & ",  " _
                        '    & "  secomm_multiplier = " & SetDec(secomm_multiplier.EditValue) & ",  " _
                        '    & "  secomm_type = " & SetSetring(secomm_type.EditValue) & "  " _
                        '    & "WHERE  " _
                        '    & "  secomm_oid = " & SetSetring(_oid_mstr) & " "

                        .Command.CommandText = "UPDATE  " _
                                & "  public.sls_program  " _
                                & "SET  " _
                                & "  sls_name = " & SetSetring(sls_name.EditValue) & ",  " _
                                & "  sls_desc = " & SetSetring(sls_desc.EditValue) & ",  " _
                                & "  sls_active = " & SetBitYN(sls_active.EditValue) & ",  " _
                                & "  sls_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  sls_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                & "WHERE  " _
                                & "  sls_code = " & SetSetring(_oid_mstr) & " "


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
                        set_row(Trim(sls_code.EditValue), "sls_code")
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
                            .Command.CommandText = "delete from sls_program where sls_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_code") + "'"
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

    Private Sub sls_payout_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sls_payout_id.ButtonClick
        Dim frm As New FPayOutSearch
        frm.set_win(Me)
        'frm._en_id = geninstf_en_id.EditValue
        'frm._obj = geninstf_fsn_oid
        'frm._code = geninstf_periode_id
        'frm._pil = 1
        frm.type_form = True
        frm.ShowDialog()
        'BtGen.PerformClick()
    End Sub

End Class
