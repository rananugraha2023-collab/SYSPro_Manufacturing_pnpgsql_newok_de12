Imports npgsql
Imports master_new.ModFunction

Public Class FCompanyAddress

    Dim _cmaddr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FCompanyAddress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        cmaddr_en_id.Properties.DataSource = dt_bantu
        cmaddr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cmaddr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cmaddr_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "cmaddr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code Cabang", "cmaddr_code_cabang", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "cmaddr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "cmaddr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PKP Date", "cmaddr_pkp_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Address Line 1", "cmaddr_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Line 2", "cmaddr_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Line 3", "cmaddr_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone", "cmaddr_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Fax", "cmaddr_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Address Line 1", "cmaddr_tax_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Address Line 2", "cmaddr_tax_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Address Line 3", "cmaddr_tax_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Phone", "cmaddr_tax_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Tax Fax", "cmaddr_tax_phone_2", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  cmaddr_oid, " _
                & "  cmaddr_dom_id, " _
                & "  cmaddr_en_id, " _
                & "  en_code,en_desc, " _
                & "  cmaddr_id, " _
                & "  cmaddr_code, " _
                & "  cmaddr_name, " _
                & "  cmaddr_line_1, " _
                & "  cmaddr_line_2, " _
                & "  cmaddr_line_3, " _
                & "  cmaddr_phone_1, " _
                & "  cmaddr_phone_2, " _
                & "  cmaddr_tax_line_1, " _
                & "  cmaddr_tax_line_2, " _
                & "  cmaddr_tax_line_3, " _
                & "  cmaddr_tax_phone_1, " _
                & "  cmaddr_tax_phone_2, " _
                & "  cmaddr_npwp, " _
                & "  cmaddr_pkp_date, " _
                & "  cmaddr_code_cabang, " _
                & "  cmaddr_dt " _
                & "FROM  " _
                & "  public.cmaddr_mstr " _
                & " inner join public.en_mstr on en_id = cmaddr_en_id" _
                & " where cmaddr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        cmaddr_en_id.Focus()
        cmaddr_en_id.ItemIndex = 0
        cmaddr_code.Text = ""
        cmaddr_name.Text = ""
        cmaddr_line_1.Text = ""
        cmaddr_line_2.Text = ""
        cmaddr_line_3.Text = ""
        cmaddr_phone_1.Text = ""
        cmaddr_phone_2.Text = ""
        cmaddr_tax_line_1.Text = ""
        cmaddr_tax_line_2.Text = ""
        cmaddr_tax_line_3.Text = ""
        cmaddr_tax_phone_1.Text = ""
        cmaddr_tax_phone_2.Text = ""
        cmaddr_npwp.Text = ""
        cmaddr_pkp_date.Text = ""
        cmaddr_code_cabang.Text = ""
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _cmaddr_oid As Guid
        _cmaddr_oid = Guid.NewGuid
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
                                            & "  public.cmaddr_mstr " _
                                            & "( " _
                                            & "  cmaddr_oid, " _
                                            & "  cmaddr_dom_id, " _
                                            & "  cmaddr_en_id, " _
                                            & "  cmaddr_id, " _
                                            & "  cmaddr_code, " _
                                            & "  cmaddr_name, " _
                                            & "  cmaddr_line_1, " _
                                            & "  cmaddr_line_2, " _
                                            & "  cmaddr_line_3, " _
                                            & "  cmaddr_phone_1, " _
                                            & "  cmaddr_phone_2, " _
                                            & "  cmaddr_tax_line_1, " _
                                            & "  cmaddr_tax_line_2, " _
                                            & "  cmaddr_tax_line_3, " _
                                            & "  cmaddr_tax_phone_1, " _
                                            & "  cmaddr_tax_phone_2, " _
                                            & "  cmaddr_npwp, " _
                                            & "  cmaddr_pkp_date, " _
                                            & "  cmaddr_code_cabang, " _
                                            & "  cmaddr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_cmaddr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(cmaddr_en_id.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("cmaddr_mstr", cmaddr_en_id.GetColumnValue("en_code"), "cmaddr_id", "cmaddr_en_id", cmaddr_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(cmaddr_code.Text) & ",  " _
                                            & SetSetring(cmaddr_name.Text) & ",  " _
                                            & SetSetring(cmaddr_line_1.Text) & ",  " _
                                            & SetSetring(cmaddr_line_2.Text) & ",  " _
                                            & SetSetring(cmaddr_line_3.Text) & ",  " _
                                            & SetSetring(cmaddr_phone_1.Text) & ",  " _
                                            & SetSetring(cmaddr_phone_2.Text) & ",  " _
                                            & SetSetring(cmaddr_tax_line_1.Text) & ",  " _
                                            & SetSetring(cmaddr_tax_line_2.Text) & ",  " _
                                            & SetSetring(cmaddr_tax_line_3.Text) & ",  " _
                                            & SetSetring(cmaddr_tax_phone_1.Text) & ",  " _
                                            & SetSetring(cmaddr_tax_phone_2.Text) & ",  " _
                                            & SetSetring(cmaddr_npwp.Text) & ",  " _
                                            & SetDate(cmaddr_pkp_date.DateTime) & ",  " _
                                            & SetSetring(cmaddr_code_cabang.Text) & ",  " _
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
                        set_row(Trim(cmaddr_code.Text), "cmaddr_code")
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
                _cmaddr_oid = .Item("cmaddr_oid").ToString
                cmaddr_en_id.EditValue = .Item("cmaddr_en_id")
                cmaddr_code.Text = .Item("cmaddr_code")
                cmaddr_name.Text = .Item("cmaddr_name")
                cmaddr_line_1.Text = SetString(.Item("cmaddr_line_1"))
                cmaddr_line_2.Text = SetString(.Item("cmaddr_line_2"))
                cmaddr_line_3.Text = SetString(.Item("cmaddr_line_3"))
                cmaddr_phone_1.Text = SetString(.Item("cmaddr_phone_1"))
                cmaddr_phone_2.Text = SetString(.Item("cmaddr_phone_2"))
                cmaddr_tax_line_1.Text = SetString(.Item("cmaddr_tax_line_1"))
                cmaddr_tax_line_2.Text = SetString(.Item("cmaddr_tax_line_2"))
                cmaddr_tax_line_3.Text = SetString(.Item("cmaddr_tax_line_3"))
                cmaddr_tax_phone_1.Text = SetString(.Item("cmaddr_tax_phone_1"))
                cmaddr_tax_phone_2.Text = SetString(.Item("cmaddr_tax_phone_2"))
                cmaddr_npwp.Text = SetString(.Item("cmaddr_npwp"))
                cmaddr_pkp_date.Text = IIf(IsDBNull(.Item("cmaddr_pkp_date")) = True, "", (.Item("cmaddr_pkp_date")))
                cmaddr_code_cabang.Text = SetString(.Item("cmaddr_code_cabang"))
            End With
            cmaddr_en_id.Focus()
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
                                            & "  public.cmaddr_mstr   " _
                                            & "SET  " _
                                            & "  cmaddr_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  cmaddr_en_id = " & SetInteger(cmaddr_en_id.EditValue) & ",  " _
                                            & "  cmaddr_code = " & SetSetring(cmaddr_code.Text) & ",  " _
                                            & "  cmaddr_name = " & SetSetring(cmaddr_name.Text) & ",  " _
                                            & "  cmaddr_line_1 = " & SetSetring(cmaddr_line_1.Text) & ",  " _
                                            & "  cmaddr_line_2 = " & SetSetring(cmaddr_line_2.Text) & ",  " _
                                            & "  cmaddr_line_3 = " & SetSetring(cmaddr_line_3.Text) & ",  " _
                                            & "  cmaddr_phone_1 = " & SetSetring(cmaddr_phone_1.Text) & ",  " _
                                            & "  cmaddr_phone_2 = " & SetSetring(cmaddr_phone_2.Text) & ",  " _
                                            & "  cmaddr_tax_line_1 = " & SetSetring(cmaddr_tax_line_1.Text) & ",  " _
                                            & "  cmaddr_tax_line_2 = " & SetSetring(cmaddr_tax_line_2.Text) & ",  " _
                                            & "  cmaddr_tax_line_3 = " & SetSetring(cmaddr_tax_line_3.Text) & ",  " _
                                            & "  cmaddr_tax_phone_1 = " & SetSetring(cmaddr_tax_phone_1.Text) & ",  " _
                                            & "  cmaddr_tax_phone_2 = " & SetSetring(cmaddr_tax_phone_2.Text) & ",  " _
                                            & "  cmaddr_npwp = " & SetSetring(cmaddr_npwp.Text) & ",  " _
                                            & "  cmaddr_pkp_date = " & SetDate(cmaddr_pkp_date.DateTime) & ",  " _
                                            & "  cmaddr_code_cabang = " & SetSetring(cmaddr_code_cabang.Text) & ",  " _
                                            & "  cmaddr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cmaddr_oid = " & SetSetring(_cmaddr_oid.ToString) & " "

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
                        set_row(Trim(cmaddr_code.Text), "cmaddr_code")
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
                            .Command.CommandText = "delete from cmaddr_mstr where cmaddr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cmaddr_oid").ToString + "'"
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
