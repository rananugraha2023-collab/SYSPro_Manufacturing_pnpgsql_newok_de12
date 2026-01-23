Imports npgsql
Imports master_new.ModFunction

Public Class FActivityMaster

    Dim _mch_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FPartnerGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        mch_en_id.Properties.DataSource = dt_bantu
        mch_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        mch_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        mch_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "mch_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "mch_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "mch_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "mch_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "mch_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "mch_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "mch_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "mch_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "Select  " _
            & "  a.mch_oid, " _
            & "  a.mch_en_id, " _
            & "  b.en_desc, " _
            & "  a.mch_code, " _
            & "  a.mch_name, " _
            & "  a.mch_desc, " _
            & "  a.mch_active, " _
            & "  a.mch_add_by, " _
            & "  a.mch_add_date, " _
            & "  a.mch_upd_by, " _
            & "  a.mch_upd_date " _
            & "FROM " _
            & "  Public.mch_mstr a " _
            & "  INNER JOIN Public.en_mstr b On (a.mch_en_id = b.en_id)" _
            & " where mch_en_id in (select user_en_id from tconfuserentity " _
            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        mch_en_id.Focus()
        mch_en_id.ItemIndex = 0
        mch_code.Text = ""
        mch_name.Text = ""
        mch_desc.Text = ""
        mch_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _mch_oid As Guid
        _mch_oid = Guid.NewGuid
        Dim ssqls As New ArrayList

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.mch_mstr " _
                                            & "( " _
                                            & "  mch_oid, " _
                                            & "  mch_dom_id, " _
                                            & "  mch_en_id, " _
                                            & "  mch_add_by, " _
                                            & "  mch_add_date,mch_credit_limit, mch_term_of_payment, " _
                                            & "  mch_id, " _
                                            & "  mch_code, " _
                                            & "  mch_name, " _
                                            & "  mch_desc, " _
                                            & "  mch_active, " _
                                            & "  mch_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_mch_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(mch_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("mch_mstr", mch_en_id.GetColumnValue("en_code"), "mch_id", "mch_en_id", mch_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(mch_code.Text) & ",  " _
                                            & SetSetring(mch_name.Text) & ",  " _
                                            & SetSetring(mch_desc.Text) & ",  " _
                                            & SetBitYN(mch_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
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
                        set_row(Trim(mch_code.Text), "mch_code")
                        insert = True
                    Catch ex As NpgsqlException
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
                _mch_oid = .Item("mch_oid").ToString
                mch_en_id.EditValue = .Item("mch_en_id")
                mch_code.Text = .Item("mch_code")
                mch_name.Text = .Item("mch_name")
                mch_desc.Text = .Item("mch_desc")
                mch_active.EditValue = IIf(.Item("mch_active") = "Y", True, False)
            End With
            mch_en_id.Focus()
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
                    Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.mch_mstr   " _
                                            & "SET  " _
                                            & "  mch_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  mch_en_id = " & SetInteger(mch_en_id.EditValue) & ",  " _
                                            & "  mch_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  mch_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  mch_code = " & SetSetring(mch_code.Text) & ",  " _
                                            & "  mch_name = " & SetSetring(mch_name.Text) & ",  " _
                                            & "  mch_desc = " & SetSetring(mch_desc.Text) & ",  " _
                                            & "  mch_active = " & SetBitYN(mch_active.EditValue) & ",  " _
                                            & "  mch_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  mch_oid = " & SetSetring(_mch_oid.ToString) & " "

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
                        set_row(Trim(mch_code.Text), "mch_code")
                        edit = True
                    Catch ex As NpgsqlException
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
                        Dim sqlTran As NpgsqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from mch_mstr where mch_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("mch_oid").ToString + "'"
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
                        Catch ex As NpgsqlException
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
