Imports npgsql
Imports master_new.ModFunction

Public Class FCostCenter

    Dim _cc_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("entity", ""))
        'sc_le_entity.Properties.DataSource = dt_bantu
        'sc_le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'sc_le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'sc_le_entity.ItemIndex = 0
        init_le(sc_le_entity, "en_mstr")

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "cc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "cc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "cc_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "cc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  cc_oid, " _
                    & "  cc_dom_id, " _
                    & "  cc_en_id, " _
                    & "  en_code,en_desc, " _
                    & "  cc_add_by, " _
                    & "  cc_add_date, " _
                    & "  cc_upd_by, " _
                    & "  cc_upd_date, " _
                    & "  cc_id, " _
                    & "  cc_code, " _
                    & "  cc_desc, " _
                    & "  cc_active, " _
                    & "  cc_dt " _
                    & " FROM  " _
                    & "  public.cc_mstr" _
                    & " inner join public.en_mstr on en_id = cc_en_id" _
                    & " where cc_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_entity.Focus()
        sc_le_entity.ItemIndex = 0
        sc_te_cc_code.Text = ""
        sc_te_cc_desc.Text = ""
        sc_ce_cc_active.EditValue = True

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _cc_oid As Guid
        _cc_oid = Guid.NewGuid
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
                                            & "  public.cc_mstr " _
                                            & "( " _
                                            & "  cc_oid, " _
                                            & "  cc_dom_id, " _
                                            & "  cc_en_id, " _
                                            & "  cc_add_by, " _
                                            & "  cc_add_date, " _
                                            & "  cc_id, " _
                                            & "  cc_code, " _
                                            & "  cc_desc, " _
                                            & "  cc_active, " _
                                            & "  cc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(sc_le_entity.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("cc_mstr", sc_le_entity.GetColumnValue("en_code"), "cc_id", "cc_en_id", sc_le_entity.EditValue.ToString)) & ",  " _
                                            & SetSetring(sc_te_cc_code.Text) & ",  " _
                                            & SetSetring(sc_te_cc_desc.Text) & ",  " _
                                            & SetBitYN(sc_ce_cc_active.EditValue) & ",  " _
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
            sc_le_entity.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cc_oid = .Item("cc_oid").ToString
                sc_te_cc_code.Text = .Item("cc_code")
                sc_te_cc_desc.Text = .Item("cc_desc")
                sc_le_entity.EditValue = .Item("cc_en_id")
                sc_ce_cc_active.EditValue = IIf(.Item("cc_active") = "Y", True, False)
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
                        .Command.CommandText = "update cc_mstr set cc_code = '" + Trim(sc_te_cc_code.Text) + "', " + _
                                               " cc_upd_by = '" + master_new.ClsVar.sNama + "', " + _
                                               " cc_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "), " + _
                                               " cc_en_id = " + Trim(sc_le_entity.EditValue) + ", " + _
                                               " cc_desc = '" + Trim(sc_te_cc_desc.Text) + "', " + _
                                               " cc_active = '" + IIf(sc_ce_cc_active.EditValue = True, "Y", "N") + "'," + _
                                               " cc_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" + _
                                               " where cc_oid = '" + _cc_oid + "'"
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
                            .Command.CommandText = "delete from cc_mstr where cc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cc_oid").ToString + "'"
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
