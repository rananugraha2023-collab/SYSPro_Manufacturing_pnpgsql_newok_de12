Imports npgsql
Imports master_new.ModFunction

Public Class FProjectAccount

    Dim _pjc_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        sc_le_pjc.Properties.DataSource = dt_bantu
        sc_le_pjc.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sc_le_pjc.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sc_le_pjc.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "pjc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pjc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Project", "pjc_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Active", "pjc_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "pjc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "pjc_add_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Update", "pjc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "pjc_upd_date", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  pjc_oid, " _
                    & "  pjc_dom_id, " _
                    & "  pjc_en_id, " _
                    & "  pjc_add_by, " _
                    & "  pjc_add_date, " _
                    & "  pjc_upd_by, " _
                    & "  pjc_upd_date, " _
                    & "  pjc_id, " _
                    & "  pjc_code, " _
                    & "  pjc_date, " _
                    & "  pjc_desc, " _
                    & "  pjc_active, " _
                    & "  en_desc, " _
                    & "  pjc_dt " _
                    & "FROM  " _
                    & "  public.pjc_mstr" _
                    & "  inner join public.en_mstr on en_id = pjc_en_id " _
                    & " where pjc_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        sc_te_pjc_code.Focus()
        sc_te_pjc_code.Text = ""
        sc_te_pjc_desc.Text = ""
        sc_ce_pjc_active.EditValue = False
        sc_le_dt_pjc.Text = ""

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pjc_oid As Guid
        _pjc_oid = Guid.NewGuid
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
                                            & "  public.pjc_mstr " _
                                            & "( " _
                                            & "  pjc_oid, " _
                                            & "  pjc_dom_id, " _
                                            & "  pjc_en_id, " _
                                            & "  pjc_add_by, " _
                                            & "  pjc_add_date, " _
                                            & "  pjc_id, " _
                                            & "  pjc_code, " _
                                            & "  pjc_date, " _
                                            & "  pjc_desc, " _
                                            & "  pjc_active, " _
                                            & "  pjc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_pjc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sc_le_pjc.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("pjc_mstr", sc_le_pjc.GetColumnValue("en_code"), "pjc_id", "pjc_en_id", sc_le_pjc.EditValue.ToString)) & ",  " _
                                            & SetSetring(sc_te_pjc_code.Text) & ",  " _
                                            & SetDate(sc_le_dt_pjc.DateTime) & ",  " _
                                            & SetSetring(sc_te_pjc_desc.Text) & ",  " _
                                            & SetBitYN(sc_ce_pjc_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
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
                        set_row(Trim(sc_te_pjc_code.Text), "pjc_code")
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
            sc_te_pjc_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pjc_oid = .Item("pjc_oid").ToString
                sc_te_pjc_code.Text = .Item("pjc_code")
                sc_te_pjc_desc.Text = .Item("pjc_desc")
                sc_le_pjc.EditValue = .Item("pjc_en_id")
                sc_le_dt_pjc.Text = IIf(IsDBNull(.Item("pjc_date")) = True, "", .Item("pjc_date"))
                sc_ce_pjc_active.EditValue = IIf(.Item("pjc_active") = "Y", True, False)
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
                        .Command.CommandText = "update pjc_mstr set pjc_code = '" + Trim(sc_te_pjc_code.Text) + "', " + _
                                               " pjc_upd_by = '" + master_new.ClsVar.sNama + "', " + _
                                               " pjc_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "), " + _
                                               " pjc_en_id = " + Trim(sc_le_pjc.EditValue) + ", " + _
                                               " pjc_desc = '" + Trim(sc_te_pjc_desc.Text) + "', " + _
                                               " pjc_date = " + SetDate(sc_le_dt_pjc.EditValue) + ", " + _
                                               " pjc_active = '" + IIf(sc_ce_pjc_active.EditValue = True, "Y", "N") + "'," + _
                                               " pjc_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" + _
                                               " where pjc_oid = '" + _pjc_oid + "'"
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
                        set_row(Trim(sc_te_pjc_code.Text), "pjc_code")
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
                            .Command.CommandText = "delete from pjc_mstr where pjc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pjc_oid").ToString + "'"
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
