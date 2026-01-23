Imports npgsql
Imports master_new.ModFunction

Public Class FArea
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _area_oid_mstr As String

    Private Sub FArea_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_area_mstr())
        area_parent.Properties.DataSource = dt_bantu
        area_parent.Properties.DisplayMember = dt_bantu.Columns("area_code").ToString
        area_parent.Properties.ValueMember = dt_bantu.Columns("area_id").ToString
        area_parent.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "area_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "area_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "area_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent", "emp_parent_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "area_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "area_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "area_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "area_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  area_mstr.area_oid, " _
                    & "  area_mstr.area_dom_id, " _
                    & "  area_mstr.area_add_by, " _
                    & "  area_mstr.area_add_date, " _
                    & "  area_mstr.area_upd_by, " _
                    & "  area_mstr.area_upd_date, " _
                    & "  area_mstr.area_id, " _
                    & "  area_mstr.area_code, " _
                    & "  area_mstr.area_name, " _
                    & "  area_mstr.area_desc, " _
                    & "  area_mstr.area_parent, " _
                    & "  area_mstr.area_dt, " _
                    & "  area_mstr_parent.area_name as emp_parent_name " _
                    & "FROM  " _
                    & "  public.area_mstr " _
                    & "  left outer join public.area_mstr area_mstr_parent on area_mstr_parent.area_id = area_mstr.area_parent" 
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        load_cb()
        area_code.Focus()
        area_code.Text = ""
        area_name.Text = ""
        area_desc.Text = ""
        area_parent.ItemIndex = 0
    End Sub

    'Public Overrides Function before_save() As Boolean
    '    before_save = True

    '    Dim _ac_code As String = ""
    '    before_save = True
    '    Try
    '        Using objcek As New master_new.WDABasepgsql("", "")
    '            With objcek
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select area_parent from area_mstr " + _
    '                                       " where area_id " + area_parent.GetColumnValue("area_id")

    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    _ac_code = .DataReader("ac_code")
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try

    '    If _ac_code = Trim(ac_code.Text) Then
    '        MessageBox.Show("Circular Link Not Allowed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ac_parent.Focus()
    '        Return False
    '    End If

    '    If ac_code.Text = ac_parent.GetColumnValue("ac_name") Then
    '        MessageBox.Show("Account Code Can't Same With Account Parent..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        ac_parent.Focus()
    '        Return False
    '    End If

    '    Return before_save
    'End Function

    Public Overrides Function insert() As Boolean
        Dim _area_oid As Guid = Guid.NewGuid
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
                                            & "  public.area_mstr " _
                                            & "( " _
                                            & "  area_oid, " _
                                            & "  area_dom_id, " _
                                            & "  area_add_by, " _
                                            & "  area_add_date, " _
                                            & "  area_id, " _
                                            & "  area_code, " _
                                            & "  area_name, " _
                                            & "  area_desc, " _
                                            & "  area_parent, " _
                                            & "  area_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_area_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetInteger(func_coll.GetID("area_mstr", "area_id")) & ",  " _
                                            & SetSetring(area_code.Text) & ",  " _
                                            & SetSetring(area_name.Text) & ",  " _
                                            & SetSetring(area_desc.Text) & ",  " _
                                            & IIf(area_parent.ItemIndex = 0, "null", SetInteger(area_parent.EditValue)) & ",  " _
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
                        set_row(Trim(_area_oid.ToString), "area_oid")
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
                _area_oid_mstr = .Item("area_oid").ToString
                area_code.Text = SetString(.Item("area_code"))
                area_name.Text = SetString(.Item("area_name"))
                area_desc.Text = SetString(.Item("area_desc"))

                If IsDBNull(.Item("area_parent")) = True Then
                    area_parent.ItemIndex = 0
                Else
                    area_parent.EditValue = .Item("area_parent")
                End If
            End With
            area_code.Focus()
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
                                            & "  public.area_mstr   " _
                                            & "SET  " _
                                            & "  area_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  area_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  area_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  area_code = " & SetSetring(area_code.Text) & ",  " _
                                            & "  area_name = " & SetSetring(area_name.Text) & ",  " _
                                            & "  area_desc = " & SetSetring(area_desc.Text) & ",  " _
                                            & "  area_parent = " & IIf(area_parent.ItemIndex = 0, "null", SetInteger(area_parent.EditValue)) & ",  " _
                                            & "  area_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  area_oid = " & SetSetring(_area_oid_mstr) & " "
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
                        set_row(Trim(_area_oid_mstr), "area_oid")
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
                            .Command.CommandText = "delete from area_mstr where area_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("area_oid").ToString + "'"
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
