Imports npgsql
Imports master_new.ModFunction

Public Class FSalesStructure
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _sls_oid_mstr As String
    Dim _now As DateTime

    Private Sub FSalesStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_employee())
        sls_emp_id.Properties.DataSource = dt_bantu
        sls_emp_id.Properties.DisplayMember = dt_bantu.Columns("emp_fname").ToString
        sls_emp_id.Properties.ValueMember = dt_bantu.Columns("emp_id").ToString
        sls_emp_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_employee())
        sls_parent_id.Properties.DataSource = dt_bantu
        sls_parent_id.Properties.DisplayMember = dt_bantu.Columns("emp_fname").ToString
        sls_parent_id.Properties.ValueMember = dt_bantu.Columns("emp_id").ToString
        sls_parent_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Employee", "sls_emp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "sls_parent_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Date", "sls_start_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "End Date", "sls_end_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Active", "sls_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "sls_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sls_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sls_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sls_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  sls_oid, " _
                    & "  sls_add_by, " _
                    & "  sls_add_date, " _
                    & "  sls_upd_by, " _
                    & "  sls_upd_date, " _
                    & "  sls_emp_id, " _
                    & "  sls_parent_id, " _
                    & "  sls_start_date, " _
                    & "  sls_end_date, " _
                    & "  sls_active, " _
                    & "  emp_mstr_emp.emp_fname as sls_emp_name, " _
                    & "  emp_mstr_parent.emp_fname as sls_parent_name " _
                    & "FROM  " _
                    & "  public.sls_strct " _
                    & "  inner join emp_mstr emp_mstr_emp on emp_mstr_emp.emp_id = sls_emp_id " _
                    & "  left outer join emp_mstr emp_mstr_parent on emp_mstr_parent.emp_id = sls_parent_id"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        load_cb()
        sls_emp_id.Focus()
        sls_emp_id.ItemIndex = 0
        sls_parent_id.ItemIndex = 0
        sls_start_date.DateTime = _now
        sls_end_date.DateTime = _now
        sls_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _sls_oid As Guid = Guid.NewGuid
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
                                            & "  public.sls_strct " _
                                            & "( " _
                                            & "  sls_oid, " _
                                            & "  sls_add_by, " _
                                            & "  sls_add_date, " _
                                            & "  sls_emp_id, " _
                                            & "  sls_parent_id, " _
                                            & "  sls_start_date, " _
                                            & "  sls_end_date, " _
                                            & "  sls_active " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_sls_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(sls_emp_id.EditValue) & ",  " _
                                            & IIf(sls_parent_id.ItemIndex = 0, "null", SetInteger(sls_parent_id.EditValue)) & ",  " _
                                            & SetDate(sls_start_date.DateTime) & ",  " _
                                            & SetDate(sls_end_date.DateTime) & ",  " _
                                            & SetBitYN(sls_active.EditValue) & "  " _
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
                        set_row(Trim(_sls_oid.ToString), "sls_oid")
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
                _sls_oid_mstr = .Item("sls_oid").ToString
                sls_emp_id.EditValue = .Item("sls_emp_id")
                sls_parent_id.EditValue = .Item("sls_parent_id")
                sls_start_date.DateTime = .Item("sls_start_date")
                sls_end_date.DateTime = .Item("sls_end_date")
                sls_active.EditValue = SetBitYNB(.Item("sls_active"))
            End With
            sls_emp_id.Focus()
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
                                            & "  public.sls_strct   " _
                                            & "SET  " _
                                            & "  sls_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  sls_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  sls_emp_id = " & SetInteger(sls_emp_id.EditValue) & ",  " _
                                            & "  sls_parent_id = " & SetInteger(sls_parent_id.EditValue) & ",  " _
                                            & "  sls_start_date = " & SetDate(sls_start_date.DateTime) & ",  " _
                                            & "  sls_end_date = " & SetDate(sls_end_date.DateTime) & ",  " _
                                            & "  sls_active = " & SetBitYN(sls_active.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  sls_oid = " & SetSetring(_sls_oid_mstr) & " "
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
                        set_row(Trim(_sls_oid_mstr), "sls_oid")
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
                            .Command.CommandText = "delete from sls_strct where sls_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sls_oid").ToString + "'"
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
