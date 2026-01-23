Imports npgsql
Imports master_new.ModFunction

Public Class FOrganization

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _org_oid_mstr As String

    Private Sub FOrganization_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        sc_le_org_en_id.Properties.DataSource = dt_bantu
        sc_le_org_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sc_le_org_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sc_le_org_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("user_login", ""))
        sc_le_org_approver_id.Properties.DataSource = dt_bantu
        sc_le_org_approver_id.Properties.DisplayMember = dt_bantu.Columns("usernama").ToString
        sc_le_org_approver_id.Properties.ValueMember = dt_bantu.Columns("userid").ToString
        sc_le_org_approver_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Organization Type", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "org_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "org_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Approver", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "org_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsDefault", "org_default", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "org_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "org_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "org_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "org_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "org_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.org_mstr.org_oid, " _
                    & "  public.org_mstr.org_dom_id, " _
                    & "  public.org_mstr.org_en_id, " _
                    & "  public.org_mstr.org_add_by, " _
                    & "  public.org_mstr.org_add_date, " _
                    & "  public.org_mstr.org_upd_by, " _
                    & "  public.org_mstr.org_upd_date, " _
                    & "  public.org_mstr.org_id, " _
                    & "  public.org_mstr.org_type_id, " _
                    & "  public.org_mstr.org_code, " _
                    & "  public.org_mstr.org_name, " _
                    & "  public.org_mstr.org_approver_id, " _
                    & "  public.org_mstr.org_remarks, " _
                    & "  public.org_mstr.org_default, " _
                    & "  public.org_mstr.org_active, " _
                    & "  public.org_mstr.org_dt, " _
                    & "  org_type.code_id, " _
                    & "  org_type.code_name, " _
                    & "  public.tconfuser.usernama, " _
                    & "  en_desc " _
                    & "FROM " _
                    & "  public.org_mstr " _
                    & "  INNER JOIN public.code_mstr org_type ON (public.org_mstr.org_type_id = org_type.code_id) " _
                    & "  INNER JOIN public.en_mstr on public.en_mstr.en_id = org_en_id " _
                    & "  INNER JOIN public.tconfuser ON (public.org_mstr.org_approver_id = public.tconfuser.usernama)" _
                    & " where org_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_org_en_id.Focus()
        sc_le_org_en_id.ItemIndex = 0
        sc_te_org_code.Text = ""
        sc_te_org_name.Text = ""
        sc_te_org_remarks.Text = ""
        sc_le_org_approver_id.ItemIndex = 0
        sc_ce_org_active.EditValue = False
        sc_ce_org_default.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _org_oid As Guid
        _org_oid = Guid.NewGuid
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
                                            & "  public.org_mstr " _
                                            & "( " _
                                            & "  org_oid, " _
                                            & "  org_dom_id, " _
                                            & "  org_en_id, " _
                                            & "  org_add_by, " _
                                            & "  org_add_date, " _
                                            & "  org_id, " _
                                            & "  org_type_id, " _
                                            & "  org_code, " _
                                            & "  org_name, " _
                                            & "  org_approver_id, " _
                                            & "  org_remarks, " _
                                            & "  org_active, " _
                                            & "  org_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_org_oid.ToString) & ",  " _
                                            & master_new.ClsVar.sdom_id & ",  " _
                                            & sc_le_org_en_id.EditValue & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(func_coll.GetID("org_mstr", sc_le_org_en_id.GetColumnValue("en_code"), "org_id", "org_en_id", sc_le_org_en_id.EditValue.ToString)) & ",  " _
                                            & sc_le_org_type_id.EditValue & ",  " _
                                            & SetSetring(Trim(sc_te_org_code.Text)) & ",  " _
                                            & SetSetring(Trim(sc_te_org_name.Text)) & ",  " _
                                            & SetSetring(sc_le_org_approver_id.GetColumnValue("usernama")) & ",  " _
                                            & SetSetring(Trim(sc_te_org_remarks.Text)) & ",  " _
                                            & SetBitYN(sc_ce_org_active.EditValue) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                            & ");"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If sc_ce_org_default.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'N'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'Y' where org_oid = '" + _org_oid.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'N' where org_oid = '" + _org_oid.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

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

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("org_type_id", sc_le_org_en_id.EditValue))
        sc_le_org_type_id.Properties.DataSource = dt_bantu
        sc_le_org_type_id.Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        sc_le_org_type_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        sc_le_org_type_id.ItemIndex = 0
    End Sub

    Private Sub sc_le_org_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_org_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _org_oid_mstr = .Item("org_oid").ToString
                sc_le_org_en_id.EditValue = .Item("org_en_id")
                sc_le_org_type_id.EditValue = .Item("org_type_id")
                sc_te_org_code.Text = .Item("org_code")
                sc_te_org_name.Text = .Item("org_name")
                sc_le_org_approver_id.EditValue = .Item("org_approver_id")
                sc_te_org_remarks.Text = SetString(.Item("org_remarks"))
                sc_ce_org_default.EditValue = IIf(.Item("org_default") = "Y", True, False)
                sc_ce_org_active.EditValue = IIf(.Item("org_active") = "Y", True, False)
            End With
            sc_le_org_en_id.Focus()
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
                                                & "  public.org_mstr   " _
                                                & "SET  " _
                                                & "  org_en_id = " & sc_le_org_en_id.EditValue & ",  " _
                                                & "  org_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  org_upd_date = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & "  org_type_id = " & sc_le_org_type_id.EditValue & ",  " _
                                                & "  org_code = " & SetSetring(Trim(sc_te_org_code.Text)) & ",  " _
                                                & "  org_name = " & SetSetring(Trim(sc_te_org_name.Text)) & ",  " _
                                                & "  org_approver_id = " & SetSetring(sc_le_org_approver_id.GetColumnValue("usernama")) & ",  " _
                                                & "  org_remarks = " & SetSetring(Trim(sc_te_org_remarks.Text)) & ",  " _
                                                & "  org_active = " & SetBitYN(sc_ce_org_active.EditValue) & ",  " _
                                                & "  org_dt = (" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  org_oid = " & SetSetring(_org_oid_mstr) & "  " _
                                                & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        If sc_ce_org_default.EditValue = True Then
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'N'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'Y' where org_oid = '" + _org_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Else
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update org_mstr set org_default = 'N' where org_oid = '" + _org_oid_mstr.ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        End If

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
        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

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
                            .Command.CommandText = "delete from org_mstr where org_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("org_oid").ToString + "'"
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
