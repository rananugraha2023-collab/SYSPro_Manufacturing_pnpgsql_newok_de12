Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FVirtualAccount
    Dim ssql As String
    Dim _va_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FAccount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        With va_en_id
            .Properties.DataSource = dt_bantu
            .Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
            .Properties.ValueMember = dt_bantu.Columns("en_id").ToString
            .ItemIndex = 0
        End With
       
    End Sub

    Public Overrides Sub load_cb_en()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("ptnr_mstr_cust", va_en_id.EditValue))
        va_ptnr_id.Properties.DataSource = dt_bantu
        va_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
        va_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
        va_ptnr_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = func_data.load_bank_mstr(va_en_id.EditValue)
        va_bk_id.Properties.DataSource = dt_bantu
        va_bk_id.Properties.DisplayMember = dt_bantu.Columns("bk_name").ToString
        va_bk_id.Properties.ValueMember = dt_bantu.Columns("bk_id").ToString
        va_bk_id.ItemIndex = 0
    End Sub

    Private Sub sc_le_bk_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles va_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Va Code", "va_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "va_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "bk_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partner", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "va_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "va_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "va_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "va_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "va_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  va_oid, " _
                & "  va_dom_id, " _
                & "  dom_desc, " _
                & "  va_en_id, " _
                & "  va_add_by, " _
                & "  va_add_date, " _
                & "  va_upd_by, " _
                & "  va_upd_date, " _
                & "  va_id, " _
                & "  va_code, " _
                & "  va_name, " _
                & "  va_bk_id, " _
                & "  bk_name, " _
                & "  va_ptnr_id, " _
                & "  ptnr_name, " _
                & "  va_active, " _
                & "  va_dt, " _
                & "  en_desc " _
                & "FROM " _
                & "  public.va_mstr " _
                & "  INNER JOIN public.dom_mstr ON (va_dom_id = dom_id) " _
                & "  INNER JOIN public.en_mstr ON (va_en_id = en_id) " _
                & "  INNER JOIN public.bk_mstr ON (bk_id = va_bk_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (ptnr_id = va_ptnr_id) " _
                & "  AND (dom_id = en_dom_id)" _
                & " and va_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()

        va_en_id.Focus()
        va_code.Text = ""
        Description.Text = ""
        va_en_id.ItemIndex = 0
        va_bk_id.ItemIndex = 0
        va_ptnr_id.ItemIndex = 0
        va_active.EditValue = False
        va_name.Text = ""


    End Sub

    Public Overrides Function insert() As Boolean
        Dim _va_oid As Guid
        _va_oid = Guid.NewGuid
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
                                            & "  public.va_mstr " _
                                            & "( " _
                                            & "  va_oid, " _
                                            & "  va_dom_id, " _
                                            & "  va_en_id, " _
                                            & "  va_add_by, " _
                                            & "  va_add_date, " _
                                            & "  va_id, " _
                                            & "  va_code, " _
                                            & "  va_name, " _
                                            & "  va_bk_id, " _
                                            & "  va_ptnr_id, " _
                                            & "  va_active, " _
                                            & "  va_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_va_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(va_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                            & SetInteger(func_coll.GetID("va_mstr", va_en_id.GetColumnValue("en_code"), "va_id", "va_en_id", va_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(va_code.Text) & ",  " _
                                            & SetSetring(va_name.Text) & ",  " _
                                            & SetInteger(va_bk_id.EditValue) & ",  " _
                                            & SetInteger(va_ptnr_id.EditValue) & ",  " _
                                            & SetBitYN(va_active.EditValue) & ",  " _
                                            & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
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
                        set_row(Trim(va_code.Text), "va_code")
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
            va_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _va_oid = .Item("va_oid").ToString
                va_code.Text = SetString(.Item("va_code"))
                va_name.Text = SetString(.Item("va_name"))
                va_en_id.EditValue = .Item("va_en_id")
                va_bk_id.EditValue = .Item("va_bk_id")
                va_ptnr_id.EditValue = .Item("va_ptnr_id")
                va_active.EditValue = SetBitYNB(.Item("va_active"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim _va_code As String
        Dim ssqls As New ArrayList

        _va_code = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("va_code")

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
                                                & "  public.va_mstr   " _
                                                & "SET  " _
                                                & "  va_en_id = " & SetSetring(va_en_id.EditValue) & ",  " _
                                                & "  va_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  va_upd_date = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & "  va_code = " & SetSetring(va_code.Text) & ",  " _
                                                & "  va_name = " & SetSetring(va_name.Text) & ",  " _
                                                & "  va_bk_id = " & SetSetring(va_bk_id.EditValue) & ",  " _
                                                & "  va_ptnr_id = " & SetSetring(va_ptnr_id.EditValue) & ",  " _
                                                & "  va_active = " & SetBitYN(va_active.EditValue) & ",  " _
                                                & "  va_dt = " & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  va_oid ='" & _va_oid & "' "
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
                        set_row(_va_code, "va_code")
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
                            .Command.CommandText = "delete from va_mstr where va_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("va_oid").ToString + "'"
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

