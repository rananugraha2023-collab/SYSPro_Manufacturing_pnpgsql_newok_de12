Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports master_new

Public Class FAccount
    Dim ssql As String
    Dim _ac_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_type())
        ac_type.Properties.DataSource = dt_bantu
        ac_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ac_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ac_type.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_sign())
        ac_sign.Properties.DataSource = dt_bantu
        ac_sign.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ac_sign.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ac_sign.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_ac_mstr())
        'ac_parent.Properties.DataSource = dt_bantu
        'ac_parent.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        'ac_parent.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        'ac_parent.ItemIndex = 0

        init_le(ac_parent, "account_all")

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_cu_mstr())
        'ac_cu_id.Properties.DataSource = dt_bantu
        'ac_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        'ac_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        'ac_cu_id.ItemIndex = 0

        init_le(ac_cu_id, "cu_mstr")

      
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "ID", "ac_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code Hirarki", "ac_code_hirarki", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Deskripsi", "ac_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Parent", "ac_parent_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Type", "ac_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Level", "ac_level", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Sum Level", "ac_is_sumlevel", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sign", "ac_sign", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsBudget", "ac_is_budget", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Is Cashflow", "ac_is_cf", DevExpress.Utils.HorzAlignment.Center)

        add_column_copy(gv_master, "IsActive", "ac_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ac_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ac_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ac_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ac_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  b.ac_oid," _
            & "  b.ac_id," _
            & "  a.dom_desc, " _
            & "  b.ac_add_by, " _
            & "  b.ac_add_date, " _
            & "  b.ac_upd_by, " _
            & "  b.ac_upd_date, " _
            & "  b.ac_code,b.ac_code_hirarki,b.ac_level, " _
            & "  b.ac_name, " _
            & "  b.ac_desc, " _
            & "  b.ac_parent, " _
            & "  b.ac_type, " _
            & "  b.ac_cu_id, " _
            & "  cu_name, " _
            & "  b.ac_is_sumlevel,coalesce(b.ac_is_cf,'N') as ac_is_cf, " _
            & "  b.ac_sign, " _
            & "  ac_mstr_parent.ac_name as ac_parent_name, " _
            & "  b.ac_active,b.ac_is_budget " _
            & "FROM " _
            & "  public.ac_mstr b " _
            & "  INNER JOIN public.dom_mstr a ON (b.ac_dom_id = a.dom_id)" _
            & "  LEFT OUTER JOIN public.ac_mstr ac_mstr_parent ON (b.ac_parent = ac_mstr_parent.ac_id)" _
            & "  LEFT OUTER JOIN public.cu_mstr c ON (b.ac_cu_id = c.cu_id)" _
            & " order by ac_code_hirarki"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ac_code.Focus()
        ac_code.Text = ""
        ac_name.Text = ""
        ac_desc.Text = ""
        ac_type.ItemIndex = 0
        ac_sign.ItemIndex = 0
        ac_parent.ItemIndex = 0
        ac_cu_id.ItemIndex = 0
        ac_code_hirarki.Text = ""
        ac_level.Text = ""
        ac_is_sumlevel.EditValue = False
        ac_active.EditValue = True
    End Sub

    Public Overrides Function before_save() As Boolean
        Dim _ac_code As String = ""
        before_save = True
        Try
            If required(ac_code_hirarki, "Account Code Hirarki") = False Then
                Return False
                Exit Function
            End If
            If required(ac_name, "Account Name") = False Then
                Return False
                Exit Function
            End If
            If required(ac_type, "Account Type") = False Then
                Return False
                Exit Function
            End If
            If required(ac_sign, "Account Sign") = False Then
                Return False
                Exit Function
            End If
            If required(ac_cu_id, "Account Currency") = False Then
                Return False
                Exit Function
            End If
            If required(ac_level, "Account Level") = False Then
                Return False
                Exit Function
            End If

            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select ac_mstr_parent.ac_code from ac_mstr " _
                                          & " inner join ac_mstr ac_mstr_parent on ac_mstr_parent.ac_id = ac_mstr.ac_parent" _
                                          & " where ac_mstr.ac_id = " + ac_parent.EditValue.ToString

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _ac_code = SetString(.DataReader("ac_code"))
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        If ac_code.EditValue <> "" Then
            If _ac_code = Trim(ac_code.Text) Then
                MessageBox.Show("Circular Link Not Allowed..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ac_parent.Focus()
                Return False
            End If
        End If

        If ac_code.Text = ac_parent.GetColumnValue("ac_name") Then
            MessageBox.Show("Account Code Can't Same With Account Parent..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ac_parent.Focus()
            Return False
        End If
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ac_oid As Guid = Guid.NewGuid
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
                                            & "  public.ac_mstr " _
                                            & "( " _
                                            & "  ac_oid, " _
                                            & "  ac_dom_id, " _
                                            & "  ac_add_by, " _
                                            & "  ac_add_date, " _
                                            & "  ac_id, " _
                                            & "  ac_code,ac_code_hirarki,ac_level, " _
                                            & "  ac_name, " _
                                            & "  ac_desc, " _
                                            & "  ac_parent, " _
                                            & "  ac_type, " _
                                            & "  ac_is_sumlevel, " _
                                            & "  ac_sign, " _
                                            & "  ac_cu_id, " _
                                            & "  ac_active,ac_is_cf, " _
                                            & "  ac_is_budget " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ac_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(func_coll.GetID("ac_mstr", "ac_id")) & ",  " _
                                            & SetSetring(ac_code.Text) & ",  " _
                                             & SetSetring(ac_code_hirarki.Text) & ",  " _
                                             & SetSetring(ac_level.Text) & ",  " _
                                            & SetSetring(ac_name.Text) & ",  " _
                                            & SetSetring(ac_desc.Text) & ",  " _
                                            & IIf(ac_parent.EditValue = 0, "null", (ac_parent.EditValue)) & ",  " _
                                            & SetSetring(ac_type.EditValue) & ",  " _
                                            & SetBitYN(ac_is_sumlevel.EditValue) & ",  " _
                                            & SetSetring(ac_sign.EditValue) & ",  " _
                                            & SetInteger(ac_cu_id.EditValue) & ",  " _
                                            & SetBitYN(ac_active.EditValue) & ",  " _
                                              & SetBitYN(ac_is_cf.EditValue) & ",  " _
                                            & SetBitYN(ac_is_budget.EditValue) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Account " & ac_code.Text)
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
                        set_row(Trim(ac_code_hirarki.Text), "ac_code_hirarki")
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
            ac_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ac_oid = SetString(.Item("ac_oid"))
                ac_code.Text = SetString(.Item("ac_code"))
                ac_name.Text = SetString(.Item("ac_name"))
                ac_desc.Text = SetString(.Item("ac_desc"))
                ac_parent.EditValue = IIf(IsDBNull(.Item("ac_parent")) = True, 0, .Item("ac_parent"))
                ac_type.EditValue = SetString(.Item("ac_type"))
                ac_is_sumlevel.EditValue = SetBitYNB(.Item("ac_is_sumlevel"))
                ac_sign.EditValue = .Item("ac_sign")
                ac_cu_id.EditValue = .Item("ac_cu_id")
                ac_active.EditValue = SetBitYNB(.Item("ac_active"))
                ac_is_budget.EditValue = SetBitYNB(.Item("ac_is_budget"))
                ac_is_cf.EditValue = SetBitYNB(.Item("ac_is_cf"))
                ac_level.EditValue = SetString(.Item("ac_level"))
                ac_code_hirarki.EditValue = SetString(.Item("ac_code_hirarki"))
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
                                            & "  public.ac_mstr   " _
                                            & "SET  " _
                                            & "  ac_upd_by = " & SetSetring(ClsVar.sNama) & ",  " _
                                            & "  ac_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ac_code = " & SetSetring(ac_code.Text) & ",  " _
                                            & "  ac_name = " & SetSetring(ac_name.Text) & ",  " _
                                            & "  ac_desc = " & SetSetring(ac_desc.Text) & ",  " _
                                            & "  ac_parent = " & IIf(ac_parent.EditValue = 0, "null", ac_parent.EditValue) & ",  " _
                                            & "  ac_level = " & SetInteger(ac_level.Text) & ",  " _
                                            & "  ac_code_hirarki = " & SetSetring(ac_code_hirarki.Text) & ",  " _
                                            & "  ac_type = " & SetSetring(ac_type.EditValue) & ",  " _
                                            & "  ac_is_sumlevel = " & SetBitYN(ac_is_sumlevel.EditValue) & ",  " _
                                            & "  ac_sign = " & SetSetring(ac_sign.EditValue) & ",  " _
                                            & "  ac_cu_id = " & SetInteger(ac_cu_id.EditValue) & ",  " _
                                            & "  ac_active = " & SetBitYN(ac_active.EditValue) & ",  " _
                                             & "  ac_is_cf = " & SetBitYN(ac_is_cf.EditValue) & ",  " _
                                            & "  ac_is_budget = " & SetBitYN(ac_is_budget.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ac_oid = '" & _ac_oid & "' "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()


                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Account " & ac_code.Text)
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
                        set_row(_ac_oid, "ac_oid")
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

    'Private Sub set_row(ByVal par_code As String)
    '    Dim i As Integer
    '    For i = 0 To ds.Tables(0).Rows.Count - 1
    '        If par_code = ds.Tables(0).Rows(i).Item("ac_code") Then
    '            BindingContext(ds.Tables(0)).Position = i
    '            Exit Sub
    '        End If
    '    Next
    'End Sub

    Public Overrides Function delete_data() As Boolean
        delete_data = True
        Dim ssqls As New ArrayList

        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show(String.Format("Yakin {0} Hapus Data Ini..?", master_new.ClsVar.sNama), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from ac_mstr where ac_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ac_oid").ToString + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Account " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ac_code"))
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
