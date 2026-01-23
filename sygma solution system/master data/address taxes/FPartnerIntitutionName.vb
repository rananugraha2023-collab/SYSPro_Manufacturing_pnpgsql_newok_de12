Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerIntitutionName
    Dim ssql As String
    Dim _ptnru_oid As String
    Dim _ptnrc_ptnr_id As Integer
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerIntitutionName_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        With ptnru_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With

    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnru_ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Registered", "ptnru_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Category", "ptnru_ptnr_ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Updated Partner", "ptnru_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Updated Category", "ptnrc_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Comment", "ptnru_remarks", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Active", "ptnru_active", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "ptnru_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnru_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnru_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnru_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  public.ptnru_update.ptnru_oid, " _
                    & "  public.ptnru_update.ptnru_dom_id, " _
                    & "  public.dom_mstr.dom_desc, " _
                    & "  public.ptnru_update.ptnru_en_id, " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.ptnru_update.ptnru_add_by, " _
                    & "  public.ptnru_update.ptnru_add_date, " _
                    & "  public.ptnru_update.ptnru_upd_by, " _
                    & "  public.ptnru_update.ptnru_upd_date, " _
                    & "  public.ptnru_update.ptnru_ptnr_id, " _
                    & "  public.ptnru_update.ptnru_ptnr_name, " _
                    & "  public.ptnr_mstr.ptnr_upd_date, " _
                    & "  public.ptnru_update.ptnru_ptnr_ptnrc_id, " _
                    & "  public.ptnru_update.ptnru_ptnr_ptnrc_name, " _
                    & "  public.ptnru_update.ptnru_name, " _
                    & "  public.ptnru_update.ptnru_ptnrc_id, " _
                    & "  public.ptnru_update.ptnru_ptnrc_name, " _
                    & "  public.ptnrc_cat.ptnrc_name, " _
                    & "  public.ptnru_update.ptnru_remarks, " _
                    & "  public.ptnru_update.ptnru_dt " _
                    & "FROM " _
                    & "  public.ptnru_update " _
                    & "  INNER JOIN public.ptnrc_cat ON (public.ptnru_update.ptnru_ptnrc_id = public.ptnrc_cat.ptnrc_id) " _
                    & "  INNER JOIN public.en_mstr ON (public.ptnru_update.ptnru_en_id = public.en_mstr.en_id) " _
                    & "  INNER JOIN public.dom_mstr ON (public.ptnru_update.ptnru_dom_id = public.dom_mstr.dom_id) " _
                    & "  INNER JOIN public.ptnr_mstr ON (public.ptnru_update.ptnru_ptnr_id = public.ptnr_mstr.ptnr_id)" _
                    & " where ptnru_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        _ptnrc_ptnr_id = -1
        ptnru_en_id.ItemIndex = 0
        ptnru_ptnr_ptnrc_id.Text = ""
        ptnru_name.Text = ""
        'ptnra_line_3.Text = ""
        ptnru_gen_name.EditValue = True
        'ptnra_phone_1.Text = ""
        'ptnra_phone_2.Text = ""
        'ptnra_fax_1.Text = ""
        'ptnra_fax_2.Text = ""
        ptnru_ptnr_id.Text = ""
        'ptnra_zip.Text = ""
        ptnru_comment.Text = ""
        ptnru_ptnrc_id.ItemIndex = 0
    End Sub

    'Private Sub ptnru_gen_name_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ptnru_gen_name.CheckedChanged
    '    If ptnru_gen_name.EditValue = True Then
    '        ptnru_name.Enabled = False

    '    ElseIf ptnru_gen_name.EditValue = False Then
    '        ptnru_name.Enabled = True

    '    End If
    'End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnru_oid As Guid
        _ptnru_oid = Guid.NewGuid
        Dim ssqls As New ArrayList
        'Dim _ptnru_id, i As Long

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try

                        'Dim _ptnr_old_name, _ptnr_new_name As Long
                        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", so_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", so_en_id.EditValue.ToString))

                        '_ptnr_old_name = SetString(ptnru_ptnr_id.EditValue)
                        '_ptnr_new_name = SetSetring(ptnru_ptnr_ptnrc_id.EditValue)


                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.ptnru_update " _
                                            & "( " _
                                            & "  ptnru_oid, " _
                                            & "  ptnru_dom_id, " _
                                            & "  ptnru_en_id, " _
                                            & "  ptnru_add_by, " _
                                            & "  ptnru_add_date, " _
                                            & "  ptnru_ptnr_id, " _
                                            & "  ptnru_ptnr_name, " _
                                            & "  ptnru_ptnr_ptnrc_id, " _
                                            & "  ptnru_ptnr_ptnrc_name, " _
                                            & "  ptnru_name, " _
                                            & "  ptnru_ptnrc_id, " _
                                            & "  ptnru_remarks, " _
                                            & "  ptnru_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnru_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ptnru_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(ptnru_ptnr_id.Tag) & ",  " _
                                            & SetSetring(ptnru_ptnr_id.EditValue) & ",  " _
                                            & SetInteger(ptnru_ptnr_ptnrc_id.Tag) & ",  " _
                                            & SetSetring(ptnru_ptnr_ptnrc_id.EditValue) & ",  " _
                                            & SetSetring(ptnru_name.Text) & ",  " _
                                            & ptnru_ptnrc_id.EditValue & ",  " _
                                            & SetSetring(ptnru_comment.Text) & ",  " _
                                            & SetDate(ptnru_dt.DateTime) & "  " _
                                            & ")"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'If ptnru_gen_name.EditValue = True Then
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "insert into ptnru_update set ptnr_kor_name = " & SetSetring(ptnru_ptnr_id.EditValue) & " " & _
                        '                         " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
                        '    ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'End If

                        If .Command.CommandType = CommandType.Text Then
                            .Command.CommandText = "update ptnr_mstr set ptnr_ptnrc_id = " & SetSetring(ptnru_ptnrc_id.EditValue) & " " & _
                                                 " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_kor_name = " & SetSetring(ptnru_ptnr_id.EditValue) & " " & _
                                                 " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_name = " & SetSetring(ptnru_name.EditValue) & " " & _
                                                 " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & " " & _
                                                 " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "update ptnr_mstr set ptnr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & _
                                                 " where ptnr_id = " & SetInteger(ptnru_ptnr_id.Tag) & " "
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
                        set_row(Trim(_ptnru_oid.ToString), "ptnru_oid")
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
        If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnru_ptnr_id") > 0 Then
            MessageBox.Show("Data Canot be Edited...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        If MyBase.edit_data = True Then
            ptnru_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnru_oid = .Item("ptnru_oid").ToString
                ptnru_en_id.EditValue = .Item("ptnru_en_id")
                'ptnru_ptnr_ptnrc_id.Text = SetString(.Item("ptnru_line_1"))
                'ptnru_name.Text = SetString(.Item("ptnru_line_2"))
                'ptnru_line_3.Text = SetString(.Item("ptnru_line_3"))
                'ptnru_phone_1.Text = SetString(.Item("ptnru_phone_1"))
                'ptnru_phone_2.Text = SetString(.Item("ptnru_phone_2"))
                'ptnru_fax_1.Text = SetString(.Item("ptnru_fax_1"))
                'ptnru_fax_2.Text = SetString(.Item("ptnru_fax_2"))
                'ptnru_zip.Text = SetString(.Item("ptnru_zip"))
                'ptnru_comment.Text = SetString(.Item("ptnru_comment"))
                'ptnru_ptnr_oid.EditValue = .Item("ptnru_ptnr_oid")
                'ptnru_ptnrc_id.EditValue = .Item("ptnru_addr_type")
                'ptnru_active.EditValue = SetBitYNB(.Item("ptnru_active"))
                ptnru_ptnr_id.EditValue = SetString(.Item("ptnru_ptnr_name"))
                ptnru_ptnr_ptnrc_id.EditValue = SetString(.Item("ptnru_ptnr_ptnrc_name"))
                'ptnru_ptnrc_id.EditValue = .Item("ptnru_ptnrc_id")
                ptnru_ptnrc_id.EditValue = SetString(.Item("ptnru_ptnrc_name"))
                ptnru_ptnrc_id.EditValue = SetString(.Item("ptnru_name"))
                '& "  ptnru_ptnr_ptnrc_name, " _
                '                    & "  ptnru_name, " _
                '                    & "  ptnru_ptnrc_id, " _
                '                    & "  ptnru_remarks, " _
                '                    & "  ptnru_dt " _
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
                                            & "  public.ptnru_update   " _
                                            & "SET  " _
                                            & "  ptnru_en_id = " & SetSetring(ptnru_en_id.EditValue) & ",  " _
                                            & "  ptnru_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnru_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & "  ptnra_line_1 = " & SetSetring(ptnru_ptnr_ptnrc_id.Text) & ",  " _
                                            & "  ptnra_line_2 = " & SetSetring(ptnru_name.Text) & ",  " _
                                            & "  ptnra_line_3 = " & SetSetring(ptnra_line_3.Text) & ",  " _
                                            & "  ptnra_phone_1 = " & SetSetring(ptnra_phone_1.Text) & ",  " _
                                            & "  ptnra_phone_2 = " & SetSetring(ptnra_phone_2.Text) & ",  " _
                                            & "  ptnra_fax_1 = " & SetSetring(ptnra_fax_1.Text) & ",  " _
                                            & "  ptnra_fax_2 = " & SetSetring(ptnra_fax_2.Text) & ",  " _
                                            & "  ptnra_zip = " & SetSetring(ptnra_zip.Text) & ",  " _
                                            & "  ptnra_addr_type = " & SetInteger(ptnru_ptnrc_id.EditValue) & ",  " _
                                            & "  ptnra_comment = " & SetSetring(ptnru_comment.Text) & ",  " _
                                            & "  ptnra_dt = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnru_oid = '" & _ptnru_oid & "' "
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
                        set_row(Trim(_ptnru_oid.ToString), "ptnru_oid")
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
                            .Command.CommandText = "delete from ptnra_addr where ptnru_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnru_oid").ToString + "'"
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
        'Try
        '    dt_bantu = New DataTable
        '    dt_bantu = func_data.load_ptnr_mstr(ptnru_en_id.EditValue.ToString)
        '    With ptnra_ptnr_oid
        '        .Properties.DataSource = dt_bantu
        '        .Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        '        .Properties.ValueMember = dt_bantu.Columns("ptnr_oid").ToString
        '        .ItemIndex = 0
        '    End With
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

        'Try
        '    dt_bantu = New DataTable
        '    dt_bantu = func_data.load_ptnrc_mstr(ptnru_en_id.EditValue.ToString)
        '    With ptnru_ptnrc_id
        '        .Properties.DataSource = dt_bantu
        '        .Properties.DisplayMember = dt_bantu.Columns("ptnrc_name").ToString
        '        .Properties.ValueMember = dt_bantu.Columns("ptnrc_id").ToString
        '        .ItemIndex = 0
        '    End With
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try

        'Penambahan fungsi untuk categori customer

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnrc_cat", ptnru_en_id.EditValue))
        ptnru_ptnrc_id.Properties.DataSource = dt_bantu
        ptnru_ptnrc_id.Properties.DisplayMember = dt_bantu.Columns("ptnrc_name").ToString
        ptnru_ptnrc_id.Properties.ValueMember = dt_bantu.Columns("ptnrc_id").ToString
        ptnru_ptnrc_id.ItemIndex = 0


        'Try
        '    dt_bantu = New DataTable
        '    dt_bantu = (func_data.load_addr_type_mstr(ptnrc_en_id.EditValue))
        '    With ptnrc_cat_id
        '        .Properties.DataSource = dt_bantu
        '        .Properties.DisplayMember = dt_bantu.Columns("code_desc").ToString
        '        .Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        '        .ItemIndex = 0
        '    End With
        'Catch ex As Exception
        '    Pesan(Err)
        'End Try
    End Sub

    Private Sub ptnrc_ptnr_name_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptnru_ptnr_id.ButtonClick
        Dim frm As New FPartnerSearch
        frm.set_win(Me)
        frm._en_id = ptnru_en_id.EditValue
        frm.type_form = True
        frm.ShowDialog()
    End Sub
End Class
