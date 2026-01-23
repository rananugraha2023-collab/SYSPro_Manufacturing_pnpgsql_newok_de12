Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerBank
    Dim ssql As String
    Dim _ptnrb_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_ptnr_mstr())
            With ptnrb_ptnr_oid
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
            dt_bantu = (func_data.load_bk_mstr())
            With ptnrb_bk_id
                .Properties.DataSource = dt_bantu
                .Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
                .Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
                .ItemIndex = 0
            End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address1", "ptnrb_bank_addr_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address2", "ptnrb_bank_addr_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address3", "ptnrb_bank_addr_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "City", "ptnrb_bank_city", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Provice", "ptnrb_bank_provice", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Country", "ptnrb_bank_country", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code Bank", "ptnrb_bank_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Rekening Number", "ptnrb_rek_nbr", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Default", "ptnrb_vnd_address_def", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User Create", "ptnrb_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnrb_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnrb_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnrb_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptnrb_oid, " _
                & "  a.ptnrb_add_by, " _
                & "  a.ptnrb_add_date, " _
                & "  a.ptnrb_upd_by, " _
                & "  a.ptnrb_upd_date, " _
                & "  a.ptnrb_ptnr_oid, " _
                & "  a.ptnrb_seq, " _
                & "  a.ptnrb_bk_id, " _
                & "  a.ptnrb_bank_addr_1, " _
                & "  a.ptnrb_bank_addr_2, " _
                & "  a.ptnrb_bank_addr_3, " _
                & "  a.ptnrb_bank_city, " _
                & "  a.ptnrb_bank_provice, " _
                & "  a.ptnrb_bank_country, " _
                & "  a.ptnrb_bank_code, " _
                & "  a.ptnrb_rek_nbr, " _
                & "  a.ptnrb_vnd_address_def, " _
                & "  a.ptnrb_dt, " _
                & "  b.bk_name, " _
                & "  c.ptnr_name " _
                & "FROM " _
                & "  public.ptnrb_bank a " _
                & "  INNER JOIN public.bk_mstr b ON (a.ptnrb_bk_id = b.bk_id) " _
                & "  INNER JOIN public.ptnr_mstr c ON (a.ptnrb_ptnr_oid = c.ptnr_oid)"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ptnrb_ptnr_oid.ItemIndex = 0
        ptnrb_bk_id.ItemIndex = 0
        ptnrb_bank_addr_1.Text = ""
        ptnrb_bank_addr_2.Text = ""
        ptnrb_bank_addr_3.Text = ""
        ptnrb_bank_city.Text = ""
        ptnrb_bank_provice.Text = ""
        ptnrb_bank_code.Text = ""
        ptnrb_bank_country.Text = ""
        ptnrb_rek_nbr.Text = ""
        ptnrb_vnd_address_def.Text = ""
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _ptnrb_oid As Guid
        _ptnrb_oid = Guid.NewGuid
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
                                            & "  public.ptnrb_bank " _
                                            & "( " _
                                            & "  ptnrb_oid, " _
                                            & "  ptnrb_add_by, " _
                                            & "  ptnrb_add_date, " _
                                            & "  ptnrb_ptnr_oid, " _
                                            & "  ptnrb_seq, " _
                                            & "  ptnrb_bk_id, " _
                                            & "  ptnrb_bank_addr_1, " _
                                            & "  ptnrb_bank_addr_2, " _
                                            & "  ptnrb_bank_addr_3, " _
                                            & "  ptnrb_bank_city, " _
                                            & "  ptnrb_bank_provice, " _
                                            & "  ptnrb_bank_country, " _
                                            & "  ptnrb_bank_code, " _
                                            & "  ptnrb_rek_nbr, " _
                                            & "  ptnrb_vnd_address_def, " _
                                            & "  ptnrb_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnrb_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(ptnrb_ptnr_oid.EditValue) & ",  " _
                                            & SetInteger(func_coll.GetID("ptnrb_bank", "ptnrb_seq", "ptnrb_ptnr_oid", ptnrb_ptnr_oid.EditValue.ToString)) & ",  " _
                                            & SetInteger(ptnrb_bk_id.EditValue) & ",  " _
                                            & SetSetring(ptnrb_bank_addr_1.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_addr_2.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_addr_3.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_city.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_provice.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_country.Text) & ",  " _
                                            & SetSetring(ptnrb_bank_code.Text) & ",  " _
                                            & SetSetring(ptnrb_rek_nbr.Text) & ",  " _
                                            & SetSetring(ptnrb_vnd_address_def.Text) & ",  " _
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
                        set_row(Trim(ptnrb_bank_code.Text), "ptnrb_bank_code")
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
            ptnrb_ptnr_oid.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnrb_oid = .Item("ptnrb_oid").ToString
                ptnrb_ptnr_oid.EditValue = .Item("ptnrb_ptnr_oid").ToString
                ptnrb_bk_id.EditValue = .Item("ptnrb_bk_id")
                ptnrb_bank_addr_1.Text = SetString(.Item("ptnrb_bank_addr_1"))
                ptnrb_bank_addr_2.Text = SetString(.Item("ptnrb_bank_addr_2"))
                ptnrb_bank_addr_3.Text = SetString(.Item("ptnrb_bank_addr_3"))
                ptnrb_bank_city.Text = SetString(.Item("ptnrb_bank_city"))
                ptnrb_bank_provice.Text = SetString(.Item("ptnrb_bank_provice"))
                ptnrb_bank_code.Text = SetString(.Item("ptnrb_bank_country"))
                ptnrb_bank_country.Text = SetString(.Item("ptnrb_bank_code"))
                ptnrb_rek_nbr.Text = SetString(.Item("ptnrb_rek_nbr"))
                ptnrb_vnd_address_def.Text = SetString(.Item("ptnrb_vnd_address_def"))

            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _ptnrb_bank_code As String
        _ptnrb_bank_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrb_bank_code")
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
                                            & "  public.ptnrb_bank   " _
                                            & "SET  " _
                                            & "  ptnrb_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ptnrb_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & "  ptnrb_ptnr_oid = " & SetSetring(ptnrb_ptnr_oid.EditValue) & ",  " _
                                            & "  ptnrb_bk_id = " & SetSetring(ptnrb_bk_id.EditValue) & ",  " _
                                            & "  ptnrb_bank_addr_1 = " & SetSetring(ptnrb_bank_addr_1.Text) & ",  " _
                                            & "  ptnrb_bank_addr_2 = " & SetSetring(ptnrb_bank_addr_2.Text) & ",  " _
                                            & "  ptnrb_bank_addr_3 = " & SetSetring(ptnrb_bank_addr_3.Text) & ",  " _
                                            & "  ptnrb_bank_city = " & SetSetring(ptnrb_bank_city.Text) & ",  " _
                                            & "  ptnrb_bank_provice = " & SetSetring(ptnrb_bank_provice.Text) & ",  " _
                                            & "  ptnrb_bank_country = " & SetSetring(ptnrb_bank_country.Text) & ",  " _
                                            & "  ptnrb_bank_code = " & SetSetring(ptnrb_bank_code.Text) & ",  " _
                                            & "  ptnrb_rek_nbr = " & SetSetring(ptnrb_rek_nbr.Text) & ",  " _
                                            & "  ptnrb_vnd_address_def = " & SetSetring(ptnrb_vnd_address_def.Text) & ",  " _
                                            & "  ptnrb_dt = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ptnrb_oid = '" & _ptnrb_oid.ToString & "' "
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
                        set_row(_ptnrb_bank_code, "ptnrb_bank_code")
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
                            .Command.CommandText = "delete from ptnrb_bank where ptnrb_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnrb_oid").ToString + "'"
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
