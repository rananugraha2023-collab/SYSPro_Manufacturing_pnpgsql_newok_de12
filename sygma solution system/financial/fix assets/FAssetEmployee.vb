Imports npgsql
Imports master_new.ModFunction

Public Class FAssetEmployee
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FEmployeeNew_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        xemp_en_id.Properties.DataSource = dt_bantu
        xemp_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        xemp_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        xemp_en_id.ItemIndex = 0
    End Sub

    Private Sub xemp_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles xemp_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(xemp_en_id.EditValue, "emp_region"))
        xemp_rg.Properties.DataSource = dt_bantu
        xemp_rg.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        xemp_rg.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        xemp_rg.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(xemp_en_id.EditValue, "emp_dept"))
        xemp_dept.Properties.DataSource = dt_bantu
        xemp_dept.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        xemp_dept.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        xemp_dept.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(xemp_en_id.EditValue, "emp_div"))
        xemp_div.Properties.DataSource = dt_bantu
        xemp_div.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        xemp_div.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        xemp_div.ItemIndex = 0

    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "xemp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "xemp_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Regional", "regional", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Department", "department", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Divisi", "divisi", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "xemp_is_active", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  xemp_oid, " _
                    & "  xemp_dom_id, " _
                    & "  xemp_en_id,en_desc, " _
                    & "  xemp_id, " _
                    & "  xemp_code, " _
                    & "  xemp_name, " _
                    & "  xemp_rg,rg.code_name as regional, " _
                    & "  xemp_dept,dept.code_name as department, " _
                    & "  xemp_div, div.code_name as divisi, " _
                    & "  xemp_is_active, " _
                    & "  xemp_dt " _
                    & "FROM  " _
                    & "  public.xemp_mstr " _
                    & "  inner join en_mstr on en_id = xemp_en_id " _
                    & "  inner join code_mstr rg on rg.code_id = xemp_rg " _
                    & "  inner join code_mstr dept on dept.code_id = xemp_dept " _
                    & "  inner join code_mstr div on div.code_id = xemp_div "

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        xemp_code.Focus()
        xemp_code.Text = ""
        xemp_name.Text = ""
        xemp_rg.ItemIndex = 0
        xemp_dept.ItemIndex = 0
        xemp_div.ItemIndex = 0
        xemp_is_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim _emp_oid As String
        _emp_oid = Guid.NewGuid.ToString
        Dim _emp_id As Integer
        _emp_id = SetNewID_OLD("xemp_mstr", "xemp_id")

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
                                            & "  public.xemp_mstr " _
                                            & "( " _
                                            & "  xemp_oid, " _
                                            & "  xemp_dom_id, " _
                                            & "  xemp_en_id, " _
                                            & "  xemp_id, " _
                                            & "  xemp_code, " _
                                            & "  xemp_name, " _
                                            & "  xemp_rg, " _
                                            & "  xemp_dept, " _
                                            & "  xemp_div, " _
                                            & "  xemp_is_active, " _
                                            & "  xemp_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_emp_oid) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(xemp_en_id.EditValue) & ",  " _
                                            & SetInteger(_emp_id) & ",  " _
                                            & SetSetring(xemp_code.Text) & ",  " _
                                            & SetSetring(xemp_name.Text) & ",  " _
                                            & SetInteger(xemp_rg.EditValue) & ",  " _
                                            & SetInteger(xemp_dept.EditValue) & ",  " _
                                            & SetInteger(xemp_div.EditValue) & ",  " _
                                            & SetBitYN(xemp_is_active.EditValue) & ",  " _
                                            & "current_timestamp" & "  " _
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
                        set_row(Trim(xemp_code.Text), "xemp_code")
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
                xemp_en_id.EditValue = .Item("xemp_en_id")
                xemp_code.Text = SetString(.Item("xemp_code"))
                xemp_name.Text = SetString(.Item("xemp_name"))
                xemp_rg.EditValue = .Item("xemp_rg")
                xemp_dept.EditValue = .Item("xemp_dept")
                xemp_div.EditValue = .Item("xemp_div")
                xemp_is_active.EditValue = SetBitYNB(.Item("xemp_is_active"))

            End With
            xemp_code.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        Dim ssqls As New ArrayList

        edit = True
        'Dim _emp_oid As String

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
                                            & "  public.xemp_mstr   " _
                                            & "SET  " _
                                            & "  xemp_en_id = " & SetInteger(xemp_en_id.EditValue) & ",  " _
                                            & "  xemp_code = " & SetSetring(xemp_code.Text) & ",  " _
                                            & "  xemp_name = " & SetSetring(xemp_name.Text) & ",  " _
                                            & "  xemp_rg = " & SetInteger(xemp_rg.EditValue) & ",  " _
                                            & "  xemp_dept = " & SetInteger(xemp_dept.EditValue) & ",  " _
                                            & "  xemp_div = " & SetInteger(xemp_div.EditValue) & ",  " _
                                            & "  xemp_is_active = " & SetBitYN(xemp_is_active.EditValue) & ",  " _
                                            & "  xemp_dt = current_timestamp " & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  xemp_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("xemp_oid")) & " " _
                                            & ";"
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
                        set_row(Trim(xemp_code.Text), "xemp_code")
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
                            .Command.CommandText = "delete from xemp_mstr where xemp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("xemp_oid").ToString + "'"
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
