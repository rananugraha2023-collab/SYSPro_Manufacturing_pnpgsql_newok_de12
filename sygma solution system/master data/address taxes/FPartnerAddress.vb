Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerAddress
    Dim ssql As String
    Dim _ptnra_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        With ptnra_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Type", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Comment", "ptnra_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Active", "ptnra_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "ptnra_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnra_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnra_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnra_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.ptnra_oid, " _
            & "  a.ptnra_id, " _
            & "  a.ptnra_dom_id, " _
            & "  a.ptnra_en_id, " _
            & "  a.ptnra_add_by, " _
            & "  a.ptnra_add_date, " _
            & "  a.ptnra_upd_by, " _
            & "  a.ptnra_upd_date, " _
            & "  a.ptnra_line, " _
            & "  a.ptnra_line_1, " _
            & "  a.ptnra_line_2, " _
            & "  a.ptnra_line_3, " _
            & "  a.ptnra_phone_1, " _
            & "  a.ptnra_phone_2, " _
            & "  a.ptnra_fax_1, " _
            & "  a.ptnra_fax_2, " _
            & "  a.ptnra_zip, " _
            & "  a.ptnra_ptnr_oid, " _
            & "  a.ptnra_addr_type, " _
            & "  a.ptnra_comment, " _
            & "  a.ptnra_active, " _
            & "  a.ptnra_dt, " _
            & "  b.dom_desc, " _
            & "  c.en_desc, " _
            & "  code_name, " _
            & "  public.ptnr_mstr.ptnr_name " _
            & "FROM " _
            & "  public.ptnra_addr a " _
            & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
            & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
            & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
            & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
            & " where ptnra_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
            & " order by ptnr_name, ptnra_line "
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptnra_en_id.ItemIndex = 0
        ptnra_line_1.Text = ""
        ptnra_line_2.Text = ""
        ptnra_line_3.Text = ""
        ptnra_phone_1.Text = ""
        ptnra_phone_2.Text = ""
        ptnra_fax_1.Text = ""
        ptnra_fax_2.Text = ""
        ptnra_zip.Text = ""
        ptnra_comment.Text = ""
        ptnra_ptnr_oid.ItemIndex = 0
        ptnra_addr_type.ItemIndex = 0
        ptnra_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnra_oid As Guid
        _ptnra_oid = Guid.NewGuid
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
                                            & "  public.ptnra_addr " _
                                            & "( " _
                                            & "  ptnra_oid, " _
                                            & "  ptnra_id, " _
                                            & "  ptnra_dom_id, " _
                                            & "  ptnra_en_id, " _
                                            & "  ptnra_add_by, " _
                                            & "  ptnra_add_date, " _
                                            & "  ptnra_line, " _
                                            & "  ptnra_line_1, " _
                                            & "  ptnra_line_2, " _
                                            & "  ptnra_line_3, " _
                                            & "  ptnra_phone_1, " _
                                            & "  ptnra_phone_2, " _
                                            & "  ptnra_fax_1, " _
                                            & "  ptnra_fax_2, " _
                                            & "  ptnra_zip, " _
                                            & "  ptnra_ptnr_oid, " _
                                            & "  ptnra_addr_type, " _
                                            & "  ptnra_comment, " _
                                            & "  ptnra_active, " _
                                            & "  ptnra_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnra_oid.ToString) & ",  " _
                                            & SetInteger(func_coll.GetID("ptnra_addr", ptnra_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", ptnra_en_id.EditValue.ToString)) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptnra_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("ptnra_addr", ptnra_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", ptnra_ptnr_oid.EditValue.ToString)) & ",  " _
                                            & SetSetring(ptnra_line_1.Text) & ",  " _
                                            & SetSetring(ptnra_line_2.Text) & ",  " _
                                            & SetSetring(ptnra_line_3.Text) & ",  " _
                                            & SetSetring(ptnra_phone_1.Text) & ",  " _
                                            & SetSetring(ptnra_phone_2.Text) & ",  " _
                                            & SetSetring(ptnra_fax_1.Text) & ",  " _
                                            & SetSetring(ptnra_fax_2.Text) & ",  " _
                                            & SetSetring(ptnra_zip.Text) & ",  " _
                                            & SetSetring(ptnra_ptnr_oid.EditValue) & ",  " _
                                            & SetInteger(ptnra_addr_type.EditValue) & ",  " _
                                            & SetSetring(ptnra_comment.Text) & ",  " _
                                            & SetBitYN(ptnra_active.EditValue) & ",  " _
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
                        set_row(Trim(_ptnra_oid.ToString), "ptnra_oid")
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
            ptnra_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnra_oid = .Item("ptnra_oid").ToString
                ptnra_en_id.EditValue = .Item("ptnra_en_id")
                ptnra_line_1.Text = SetString(.Item("ptnra_line_1"))
                ptnra_line_2.Text = SetString(.Item("ptnra_line_2"))
                ptnra_line_3.Text = SetString(.Item("ptnra_line_3"))
                ptnra_phone_1.Text = SetString(.Item("ptnra_phone_1"))
                ptnra_phone_2.Text = SetString(.Item("ptnra_phone_2"))
                ptnra_fax_1.Text = SetString(.Item("ptnra_fax_1"))
                ptnra_fax_2.Text = SetString(.Item("ptnra_fax_2"))
                ptnra_zip.Text = SetString(.Item("ptnra_zip"))
                ptnra_comment.Text = SetString(.Item("ptnra_comment"))
                ptnra_ptnr_oid.EditValue = .Item("ptnra_ptnr_oid").ToString
                ptnra_addr_type.EditValue = .Item("ptnra_addr_type")
                ptnra_active.EditValue = SetBitYNB(.Item("ptnra_active"))
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
                                            & "  public.ptnra_addr   " _
                                            & "SET  " _
                                            & "  ptnra_en_id = " & SetSetring(ptnra_en_id.EditValue) & ",  " _
                                            & "  ptnra_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnra_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & "  ptnra_line_1 = " & SetSetring(ptnra_line_1.Text) & ",  " _
                                            & "  ptnra_line_2 = " & SetSetring(ptnra_line_2.Text) & ",  " _
                                            & "  ptnra_line_3 = " & SetSetring(ptnra_line_3.Text) & ",  " _
                                            & "  ptnra_phone_1 = " & SetSetring(ptnra_phone_1.Text) & ",  " _
                                            & "  ptnra_phone_2 = " & SetSetring(ptnra_phone_2.Text) & ",  " _
                                            & "  ptnra_fax_1 = " & SetSetring(ptnra_fax_1.Text) & ",  " _
                                            & "  ptnra_fax_2 = " & SetSetring(ptnra_fax_2.Text) & ",  " _
                                            & "  ptnra_zip = " & SetSetring(ptnra_zip.Text) & ",  " _
                                            & "  ptnra_ptnr_oid = " & SetSetring(ptnra_ptnr_oid.EditValue) & ",  " _
                                            & "  ptnra_addr_type = " & SetInteger(ptnra_addr_type.EditValue) & ",  " _
                                            & "  ptnra_comment = " & SetSetring(ptnra_comment.Text) & ",  " _
                                            & "  ptnra_active = " & SetBitYN(ptnra_active.EditValue) & ",  " _
                                            & "  ptnra_dt = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnra_oid = '" & _ptnra_oid & "' "
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
                        set_row(Trim(_ptnra_oid.ToString), "ptnra_oid")
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
                            .Command.CommandText = "delete from ptnra_addr where ptnra_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnra_oid").ToString + "'"
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

    Public Overrides Sub load_cb_en()
        Try
            dt_bantu = New DataTable
            dt_bantu = func_data.load_ptnr_mstr(ptnra_en_id.EditValue.ToString)
            With ptnra_ptnr_oid
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
                .Properties.ValueMember = dt_bantu.Columns("ptnr_oid").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_addr_type_mstr(ptnra_en_id.EditValue))
            With ptnra_addr_type
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
                .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sc_le_ptnra_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ptnra_en_id.EditValueChanged
        load_cb_en()
    End Sub
End Class
