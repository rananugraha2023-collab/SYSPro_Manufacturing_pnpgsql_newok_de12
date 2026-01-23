Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerContactPerson
    Dim ssql As String
    Dim _ptnrac_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_ptnra_addr())
            With addrc_ptnra_oid
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
                .Properties.ValueMember = dt_bantu.Columns("ptnra_oid").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_ptnrac_function())
            With ptnrac_function
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
                .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Partner Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Type", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Contact Person", "ptnrac_contact_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone 1", "ptnrac_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone 2", "ptnrac_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Email", "ptnrac_email", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "ptnrac_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnrac_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnrac_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnrac_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptnrac_oid, " _
                & "  a.addrc_ptnra_oid, " _
                & "  a.ptnrac_add_by, " _
                & "  a.ptnrac_add_date, " _
                & "  a.ptnrac_seq, " _
                & "  a.ptnrac_function, " _
                & "  a.ptnrac_contact_name, " _
                & "  a.ptnrac_phone_1, " _
                & "  a.ptnrac_phone_2, " _
                & "  a.ptnrac_email, " _
                & "  a.ptnrac_dt, " _
                & "  b.ptnra_line, " _
                & "  code_name, " _
                & "  ptnr_name " _
                & "FROM " _
                & "  public.ptnrac_cntc a " _
                & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                & "  Inner join public.code_mstr on code_id = ptnra_addr_type"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        addrc_ptnra_oid.ItemIndex = 0
        ptnrac_contact_name.Text = ""
        ptnrac_phone_1.Text = ""
        ptnrac_phone_2.Text = ""
        ptnrac_email.Text = ""
        ptnrac_function.ItemIndex = 0

    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnrac_oid As Guid
        _ptnrac_oid = Guid.NewGuid

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
                                            & "  public.ptnrac_cntc " _
                                            & "( " _
                                            & "  ptnrac_oid, " _
                                            & "  addrc_ptnra_oid, " _
                                            & "  ptnrac_add_by, " _
                                            & "  ptnrac_add_date, " _
                                            & "  ptnrac_seq, " _
                                            & "  ptnrac_function, " _
                                            & "  ptnrac_contact_name, " _
                                            & "  ptnrac_phone_1, " _
                                            & "  ptnrac_phone_2, " _
                                            & "  ptnrac_email, " _
                                            & "  ptnrac_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnrac_oid.ToString) & ",  " _
                                            & SetSetring(addrc_ptnra_oid.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(GetNewID("ptnrac_cntc", "ptnrac_seq", "addrc_ptnra_oid", addrc_ptnra_oid.EditValue)) & ",  " _
                                            & SetSetring(ptnrac_function.EditValue) & ",  " _
                                            & SetSetring(ptnrac_contact_name.Text) & ",  " _
                                            & SetSetring(ptnrac_phone_1.Text) & ",  " _
                                            & SetSetring(ptnrac_phone_2.Text) & ",  " _
                                            & SetSetring(ptnrac_email.Text) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
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
                        set_row(_ptnrac_oid.ToString, "ptnrac_oid")
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
            addrc_ptnra_oid.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnrac_oid = .Item("ptnrac_oid").ToString
                addrc_ptnra_oid.EditValue = .Item("addrc_ptnra_oid").ToString
                ptnrac_function.EditValue = .Item("ptnrac_function")
                ptnrac_contact_name.Text = SetString(.Item("ptnrac_contact_name"))
                ptnrac_phone_1.Text = SetString(.Item("ptnrac_phone_1"))
                ptnrac_phone_2.Text = SetString(.Item("ptnrac_phone_2"))
                ptnrac_email.Text = SetString(.Item("ptnrac_email"))

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
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.ptnrac_cntc   " _
                                                & "SET  " _
                                                & "  addrc_ptnra_oid = " & SetSetring(addrc_ptnra_oid.EditValue) & ",  " _
                                                & "  ptnrac_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ptnrac_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " ,  " _
                                                & "  ptnrac_function = " & SetSetring(ptnrac_function.EditValue) & ",  " _
                                                & "  ptnrac_contact_name = " & SetSetring(ptnrac_contact_name.Text) & ",  " _
                                                & "  ptnrac_phone_1 = " & SetSetring(ptnrac_phone_1.Text) & ",  " _
                                                & "  ptnrac_phone_2 = " & SetSetring(ptnrac_phone_2.Text) & ",  " _
                                                & "  ptnrac_email = " & SetSetring(ptnrac_email.Text) & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ptnrac_oid = '" & _ptnrac_oid.ToString & "' "
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
                        set_row(_ptnrac_oid.ToString, "ptnrac_oid")
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
                            .Command.CommandText = "delete from ptnrac_cntc where ptnrac_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrac_oid").ToString + "'"
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
