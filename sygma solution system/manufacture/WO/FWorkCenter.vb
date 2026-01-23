Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn


Public Class FWorkCenter

    Dim _wc_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _en_id_coll As String

    Private Sub FDomain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        _en_id_coll = func_data.entity_parent(func_data.entity_user())
    End Sub

    Public Overrides Sub load_cb_en()
     
        init_le(wc_dpt, "dpt_mstr", wc_en_id.EditValue)
    End Sub

    Private Sub sc_le_pt_en_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles wc_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub load_cb()

        init_le(wc_en_id, "en_mstr")
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "wc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "wc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Deskcription", "wc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Departement", "dpt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "wc_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "wc_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "wc_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "wc_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.wc_oid, " _
                    & "  a.wc_dom_id, " _
                    & "  a.wc_en_id, " _
                    & "  a.wc_add_by, " _
                    & "  a.wc_add_date, " _
                    & "  a.wc_upd_by, " _
                    & "  a.wc_upd_date, " _
                    & "  a.wc_id, " _
                    & "  a.wc_code, " _
                    & "  a.wc_dpt_id, " _
                    & "  a.wc_name, " _
                    & "  a.wc_desc, " _
                    & "  a.wc_dt, " _
                    & "  c.en_id, " _
                    & "  c.en_code, " _
                    & "  c.en_desc, " _
                    & "  b.dpt_id, " _
                    & "  b.dpt_code, " _
                    & "  b.dpt_desc " _
                    & "FROM " _
                    & " public.wc_mstr a " _
                    & " INNER JOIN public.dpt_mstr b ON (a.wc_dpt_id = b.dpt_id) " _
                    & " INNER JOIN public.en_mstr c ON (a.wc_en_id = c.en_id)" _
                    & " Where a.wc_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " ORDER BY wc_code "


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        wc_en_id.Focus()
        wc_en_id.ItemIndex = 0
        wc_dpt.ItemIndex = 0

        wc_name.Text = ""
        wc_desc.Text = ""

    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        If required(wc_en_id, "Entity") = False Then
            Return False
            Exit Function
        End If
        If required(wc_dpt, "Department") = False Then
            Return False
            Exit Function
        End If
        If required(wc_name, "Name") = False Then
            Return False
            Exit Function
        End If
        If required(wc_desc, "Description") = False Then
            Return False
            Exit Function
        End If

        Return before_save
    End Function
    Public Overrides Function insert() As Boolean
        Dim _wc_oid As Guid
        _wc_oid = Guid.NewGuid

        Dim ssqls As New ArrayList
        Dim _wc_code As String = GetNewNumberYM("wc_mstr", "wc_code", 5, "WC" & wc_en_id.GetColumnValue("en_code") _
                                      & CekTanggal.ToString("yyMM") & master_new.ClsVar.sServerCode, True)

        Dim _wc_id As Integer = GetNewIDSvrCode(wc_en_id.GetColumnValue("en_code"), "wc_mstr", "wc_id")
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
                                            & "  public.wc_mstr " _
                                            & "( " _
                                            & "  wc_oid, " _
                                            & "  wc_dom_id, " _
                                            & "  wc_en_id, " _
                                            & "  wc_add_by, " _
                                            & "  wc_add_date, " _
                                            & "  wc_id, " _
                                            & "  wc_code, " _
                                            & "  wc_dpt_id, " _
                                            & "  wc_name, " _
                                            & "  wc_desc, " _
                                            & "  wc_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_wc_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(wc_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetInteger(_wc_id) & ",  " _
                                            & SetSetring(_wc_code) & ",  " _
                                            & SetInteger(wc_dpt.EditValue) & ",  " _
                                            & SetSetring(wc_name.Text) & ",  " _
                                            & SetSetring(wc_desc.Text) & ",  " _
                                            & SetDate(master_new.PGSqlConn.CekTanggal) & ") "
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
                        set_row(Trim(_wc_oid.ToString), "wc_oid")
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
            wc_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _wc_oid = .Item("wc_oid").ToString
                wc_name.Text = SetString(.Item("wc_name"))
                wc_desc.Text = SetString(.Item("wc_desc"))
                wc_dpt.EditValue = .Item("wc_dpt_id")
                wc_en_id.EditValue = .Item("wc_en_id")

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
                                            & "  public.wc_mstr   " _
                                            & "SET  " _
                                            & "  wc_en_id = " & SetInteger(wc_en_id.EditValue) & ",  " _
                                            & "  wc_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  wc_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  wc_dpt_id = " & SetInteger(wc_dpt.EditValue) & ",  " _
                                            & "  wc_name = " & SetSetring(wc_name.Text) & ",  " _
                                            & "  wc_desc = " & SetSetring(wc_desc.Text) & ",  " _
                                            & "  wc_dt = " & SetDate(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "WHERE  " _
                                            & "  wc_oid = '" + _wc_oid + "'"
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
                            .Command.CommandText = "delete from wc_mstr where wc_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("wc_oid").ToString + "'"
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
                            delete_data = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function
End Class




