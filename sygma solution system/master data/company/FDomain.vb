Imports npgsql
Imports master_new.ModFunction

Public Class FDomain

    Dim _dom_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        dom_base_cur_id.Properties.DataSource = dt_bantu
        dom_base_cur_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        dom_base_cur_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        dom_base_cur_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        dom_pl_ac.Properties.DataSource = dt_bantu
        dom_pl_ac.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        dom_pl_ac.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        dom_pl_ac.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ac_mstr())
        dom_re_ac.Properties.DataSource = dt_bantu
        dom_re_ac.Properties.DisplayMember = dt_bantu.Columns("ac_name").ToString
        dom_re_ac.Properties.ValueMember = dt_bantu.Columns("ac_id").ToString
        dom_re_ac.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "dom_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "dom_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Company Name", "dom_company", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Base Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Profit/Loss Account Code", "ac_pl_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Profit/Loss Account Name", "ac_pl_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Retained Earnings Account Code", "ac_re_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Retained Earnings Account Name", "ac_re_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IsActive", "dom_active", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select dom_oid, dom_id, dom_code, dom_desc, dom_active, dom_dt, " + _
                     " dom_base_cur_id, cu_name, " + _
                     " ac_pl.ac_code as ac_pl_code, ac_pl.ac_name as ac_pl_name, " + _
                     " ac_re.ac_code as ac_re_code, ac_re.ac_name as ac_re_name, " + _
                     " dom_base_cur_id, cu_name, dom_pl_ac, dom_re_ac , dom_company " + _
                     " from dom_mstr" + _
                     " left outer join cu_mstr on cu_id = dom_base_cur_id " + _
                     " left outer join ac_mstr ac_pl on ac_pl.ac_id = dom_pl_ac " + _
                     " left outer join ac_mstr ac_re on ac_re.ac_id = dom_re_ac "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        dom_code.Focus()
        dom_code.Text = ""
        dom_desc.Text = ""
        dom_active.EditValue = True
        dom_base_cur_id.ItemIndex = 0
        dom_pl_ac.ItemIndex = 0
        dom_pl_ac.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList

        Dim _dom_oid As Guid
        _dom_oid = Guid.NewGuid
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
                                            & "  public.dom_mstr " _
                                            & "( " _
                                            & "  dom_oid, " _
                                            & "  dom_id, " _
                                            & "  dom_code, " _
                                            & "  dom_desc, " _
                                            & "  dom_active, " _
                                            & "  dom_dt, " _
                                            & "  dom_base_cur_id, " _
                                            & "  dom_pl_ac,dom_company, " _
                                            & "  dom_re_ac " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_dom_oid.ToString) & ",  " _
                                            & SetInteger(func_coll.GetID("dom_mstr", "dom_id")) & ",  " _
                                            & SetSetring(dom_code.Text) & ",  " _
                                            & SetSetring(dom_desc.Text) & ",  " _
                                            & SetBitYN(dom_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(dom_base_cur_id.EditValue) & ",  " _
                                            & SetInteger(dom_pl_ac.EditValue) & ",  " _
                                            & SetSetring(dom_company.EditValue) & ",  " _
                                            & SetInteger(dom_re_ac.EditValue) & "  " _
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
            dom_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _dom_oid = .Item("dom_oid").ToString
                dom_code.Text = .Item("dom_code")
                dom_desc.Text = .Item("dom_desc")
                dom_base_cur_id.EditValue = .Item("dom_base_cur_id")
                dom_pl_ac.EditValue = .Item("dom_pl_ac")
                dom_re_ac.EditValue = .Item("dom_re_ac")
                dom_active.EditValue = SetBitYNB(.Item("dom_active"))
                dom_company.EditValue = .Item("dom_company")
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
                                            & "  public.dom_mstr   " _
                                            & "SET  " _
                                            & "  dom_code = " & SetSetring(dom_code.Text) & ",  " _
                                            & "  dom_desc = " & SetSetring(dom_desc.Text) & ",  " _
                                            & "  dom_company = " & SetSetring(dom_company.EditValue) & ",  " _
                                            & "  dom_active = " & SetBitYN(dom_active.EditValue) & ",  " _
                                            & "  dom_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  dom_base_cur_id = " & SetInteger(dom_base_cur_id.EditValue) & ",  " _
                                            & "  dom_pl_ac = " & SetInteger(dom_pl_ac.EditValue) & ",  " _
                                            & "  dom_re_ac = " & SetInteger(dom_re_ac.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  dom_oid = " & SetSetring(_dom_oid.ToString) & " "
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
                            .Command.CommandText = "delete from dom_mstr where dom_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("dom_oid").ToString + "'"
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
