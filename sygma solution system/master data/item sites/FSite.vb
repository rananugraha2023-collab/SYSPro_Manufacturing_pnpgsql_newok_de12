Imports npgsql
Imports master_new.ModFunction

Public Class FSite
    Dim _si_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FSite_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        si_en_id.Properties.DataSource = dt_bantu
        si_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        si_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        si_en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "si_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "si_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "si_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "si_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "si_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "si_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  si_oid, " _
                    & "  si_dom_id, " _
                    & "  si_en_id, " _
                    & "  en_code, " _
                    & "  si_add_by, " _
                    & "  si_add_date, " _
                    & "  si_upd_by, " _
                    & "  si_upd_date, " _
                    & "  si_id, " _
                    & "  si_code, " _
                    & "  si_desc, " _
                    & "  si_active, " _
                    & "  si_dt " _
                    & " FROM  " _
                    & "  public.si_mstr" _
                    & " inner join public.en_mstr on en_id = si_en_id" _
                    & " where si_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        si_en_id.Focus()
        si_en_id.ItemIndex = 0
        si_code.Text = ""
        si_desc.Text = ""
        si_active.EditValue = False
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _si_oid As Guid
        _si_oid = Guid.NewGuid
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
                                            & "  public.si_mstr " _
                                            & "( " _
                                            & "  si_oid, " _
                                            & "  si_dom_id, " _
                                            & "  si_en_id, " _
                                            & "  si_add_by, " _
                                            & "  si_add_date, " _
                                            & "  si_id, " _
                                            & "  si_code, " _
                                            & "  si_desc, " _
                                            & "  si_active, " _
                                            & "  si_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_si_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(si_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetInteger(func_coll.GetID("si_mstr", si_en_id.GetColumnValue("en_code"), "si_id", "si_en_id", si_en_id.EditValue.ToString)) & ",  " _
                                            & SetSetring(si_code.Text) & ",  " _
                                            & SetSetring(si_desc.Text) & ",  " _
                                            & SetBitYN(si_active.EditValue) & ",  " _
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
                        set_row(Trim(si_code.Text), "si_code")
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
                _si_oid = .Item("si_oid").ToString
                si_en_id.EditValue = .Item("si_en_id")
                si_code.Text = .Item("si_code")
                si_desc.Text = .Item("si_desc")
                si_active.EditValue = IIf(.Item("si_active") = "Y", True, False)
            End With
            si_en_id.Focus()
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
                                            & "  public.si_mstr   " _
                                            & "SET  " _
                                            & "  si_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & "  si_en_id = " & SetInteger(si_en_id.EditValue) & ",  " _
                                            & "  si_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  si_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  si_code = " & SetSetring(si_code.Text) & ",  " _
                                            & "  si_desc = " & SetSetring(si_desc.Text) & ",  " _
                                            & "  si_active = " & SetBitYN(si_active.EditValue) & ",  " _
                                            & "  si_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  si_oid = " & SetSetring(_si_oid.ToString) & " "
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
                        set_row(Trim(si_code.Text), "si_code")
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
                            .Command.CommandText = "delete from si_mstr where si_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("si_oid").ToString + "'"
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

    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim _hasil As Double

            _hasil = DateDiff(DateInterval.Minute, CDate("19/09/2015"), Now)

            MsgBox(_hasil)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
