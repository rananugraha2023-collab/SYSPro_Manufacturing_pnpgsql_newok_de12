Imports npgsql
Imports master_new.ModFunction

Public Class FCostCenterUser
    Dim ds_help As DataSet
    Dim _status As Boolean
    Dim _ccr_oid As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Private Sub FCcr_restrc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _status = True
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        ccr_en_id.Properties.DataSource = dt_bantu
        ccr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ccr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ccr_en_id.ItemIndex = 0

        'user id
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "select userid as value, usernama as display from tconfuser " + _
                           " order by value asc "
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "userid")
                    ccr_user_id.Properties.DataSource = ds_cb.Tables(0)
                    ccr_user_id.Properties.DisplayMember = "display"
                    ccr_user_id.Properties.ValueMember = "value"
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select ccr_oid," + _
                     " ccr_dom_id,dom_code,dom_desc, " + _
                     " ccr_en_id,en_code,en_desc, " + _
                     " ccr_add_by," + _
                     " ccr_add_date," + _
                     " ccr_upd_by," + _
                     " ccr_upd_date," + _
                     " ccr_cc_id,cs.cc_desc as cost_centre," + _
                     " ccr_user_id,usr.usernama as user, " + _
                     " ccr_remarks," + _
                     " ccr_dt, en_desc " + _
                     " from ccr_restrc " + _
                     " inner join dom_mstr on dom_id = ccr_dom_id " + _
                     " inner join en_mstr on en_id = ccr_en_id " + _
                     " inner join cc_mstr cs on cs.cc_id = ccr_cc_id " + _
                     " inner join tconfuser usr on usr.userid = ccr_user_id " + _
                     " order by ccr_oid asc "
        Return get_sequel
    End Function

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "User ID", "user", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cost Centre", "cost_centre", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Remarks", "ccr_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        kosong()
        _status = True
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ccr_oid As Guid
        _ccr_oid = Guid.NewGuid
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
                                            & "  public.ccr_restrc " _
                                            & "( " _
                                            & "  ccr_oid, " _
                                            & "  ccr_dom_id, " _
                                            & "  ccr_en_id, " _
                                            & "  ccr_add_by, " _
                                            & "  ccr_add_date, " _
                                            & "  ccr_cc_id, " _
                                            & "  ccr_user_id, " _
                                            & "  ccr_remarks, " _
                                            & "  ccr_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ccr_oid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(ccr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & SetInteger(ccr_cc_id.EditValue) & ",  " _
                                            & SetInteger(ccr_user_id.EditValue) & ",  " _
                                            & SetSetring(ccr_remarks.Text) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                            & ")"

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                        after_success()
                        set_row(_ccr_oid.ToString, "ccr_oid")
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
            ccr_user_id.Focus()
            With ds.Tables(0).Rows(row)
                _ccr_oid = .Item("ccr_oid").ToString
                ccr_en_id.EditValue = .Item("ccr_en_id")
                ccr_cc_id.EditValue = .Item("ccr_cc_id")
                ccr_user_id.EditValue = .Item("ccr_user_id")
                ccr_remarks.Text = SetString(.Item("ccr_remarks"))
            End With
            edit_data = True
            _status = False
        End If
    End Function

    Public Overrides Sub kosong()
        MyBase.kosong()
        ccr_en_id.ItemIndex = 0
        ccr_en_id.Focus()
        ccr_user_id.ItemIndex = 0
        ccr_cc_id.ItemIndex = 0
        ccr_remarks.Text = ""
    End Sub

    Public Overrides Function edit()
        edit = True
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
                                            & "  public.ccr_restrc   " _
                                            & "SET  " _
                                            & "  ccr_en_id = " & SetInteger(ccr_en_id.EditValue) & ",  " _
                                            & "  ccr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  ccr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & "  ccr_user_id = " & SetInteger(ccr_user_id.EditValue) & ",  " _
                                            & "  ccr_cc_id = " & SetInteger(ccr_cc_id.EditValue) & ",  " _
                                            & "  ccr_remarks = " & SetSetring(ccr_remarks.Text) & ",  " _
                                            & "  ccr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  ccr_oid = " & SetSetring(_ccr_oid.ToString) & " "
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(_ccr_oid.ToString, "ccr_oid")

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
                            .Command.CommandText = "delete from ccr_restrc where ccr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ccr_oid").ToString + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

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

    Private Sub ccr_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ccr_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(ccr_en_id.EditValue))
        ccr_cc_id.Properties.DataSource = dt_bantu
        ccr_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ccr_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ccr_cc_id.ItemIndex = 0
    End Sub
End Class
