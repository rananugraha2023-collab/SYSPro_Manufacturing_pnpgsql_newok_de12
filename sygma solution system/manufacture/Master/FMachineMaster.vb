Imports npgsql
Imports master_new.ModFunction

Public Class FMachineMaster

    Dim _lbrfa_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        lbrfa_en_id.Properties.DataSource = dt_bantu
        lbrfa_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        lbrfa_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        lbrfa_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "lbrfa_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "lbrfa_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "lbrfa_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "lbrfa_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "lbrfa_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "lbrfa_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "lbrfa_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "lbrfa_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "Select  " _
            & "  a.lbrfa_oid, " _
            & "  a.lbrfa_en_id, " _
            & "  b.en_desc, " _
            & "  a.lbrfa_code, " _
            & "  a.lbrfa_name, " _
            & "  a.lbrfa_desc, " _
            & "  a.lbrfa_active, " _
            & "  a.lbrfa_add_by, " _
            & "  a.lbrfa_add_date, " _
            & "  a.lbrfa_upd_by, " _
            & "  a.lbrfa_upd_date " _
            & "FROM " _
            & "  Public.lbrfa_activity a " _
            & "  INNER JOIN Public.en_mstr b On (a.lbrfa_en_id = b.en_id)" _
            & " where lbrfa_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        lbrfa_en_id.Focus()
        lbrfa_en_id.ItemIndex = 0
        lbrfa_code.Text = ""
        lbrfa_name.Text = ""
        lbrfa_desc.Text = ""
        lbrfa_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _lbrfa_oid As Guid
        _lbrfa_oid = Guid.NewGuid
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
                                            & "  public.lbrfa_activity " _
                                            & "( " _
                                            & "  lbrfa_oid, " _
                                            & "  lbrfa_dom_id, " _
                                            & "  lbrfa_en_id, " _
                                            & "  lbrfa_add_by, " _
                                            & "  lbrfa_add_date,lbrfa_credit_limit, lbrfa_term_of_payment, " _
                                            & "  lbrfa_id, " _
                                            & "  lbrfa_code, " _
                                            & "  lbrfa_name, " _
                                            & "  lbrfa_desc, " _
                                            & "  lbrfa_active, " _
                                            & "  lbrfa_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_lbrfa_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(lbrfa_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("lbrfa_activity", lbrfa_en_id.GetColumnValue("en_code"), "lbrfa_id", "lbrfa_en_id", lbrfa_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(lbrfa_code.Text) & ",  " _
                                            & SetSetring(lbrfa_name.Text) & ",  " _
                                            & SetSetring(lbrfa_desc.Text) & ",  " _
                                            & SetBitYN(lbrfa_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ");"
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
                        set_row(Trim(lbrfa_code.Text), "lbrfa_code")
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
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _lbrfa_oid = .Item("lbrfa_oid").ToString
                lbrfa_en_id.EditValue = .Item("lbrfa_en_id")
                lbrfa_code.Text = .Item("lbrfa_code")
                lbrfa_name.Text = .Item("lbrfa_name")
                lbrfa_desc.Text = .Item("lbrfa_desc")
                lbrfa_active.EditValue = IIf(.Item("lbrfa_active") = "Y", True, False)
            End With
            lbrfa_en_id.Focus()
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
                                            & "  public.lbrfa_activity   " _
                                            & "SET  " _
                                            & "  lbrfa_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  lbrfa_en_id = " & SetInteger(lbrfa_en_id.EditValue) & ",  " _
                                            & "  lbrfa_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  lbrfa_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  lbrfa_code = " & SetSetring(lbrfa_code.Text) & ",  " _
                                            & "  lbrfa_name = " & SetSetring(lbrfa_name.Text) & ",  " _
                                            & "  lbrfa_desc = " & SetSetring(lbrfa_desc.Text) & ",  " _
                                            & "  lbrfa_active = " & SetBitYN(lbrfa_active.EditValue) & ",  " _
                                            & "  lbrfa_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  lbrfa_oid = " & SetSetring(_lbrfa_oid.ToString) & " "

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
                        set_row(Trim(lbrfa_code.Text), "lbrfa_code")
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
                            .Command.CommandText = "delete from lbrfa_activity where lbrfa_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("lbrfa_oid").ToString + "'"
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
