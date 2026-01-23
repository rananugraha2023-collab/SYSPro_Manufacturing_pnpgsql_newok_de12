Imports npgsql
Imports master_new.ModFunction

Public Class FLaborFeedbackUser

    Dim _trans_oid_mst As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Dim _user_id As String = ""

    Private Sub FTransactionStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Code", "usernama", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Description", "usernik", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Start Workflow", "useractive", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "User", "usernama", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PIN", "pin", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NIK", "usernik", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Active", "useractive", DevExpress.Utils.HorzAlignment.Center)


    End Sub

    Public Overrides Sub load_cb()
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb

                    
                    .SQL = "select en_id, en_code, en_desc from en_mstr where en_active ~~* 'Y'" + _
                               " and en_dom_id = " & master_new.ClsVar.sdom_id.ToString & " order by en_desc "
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "entity")
                   
                    '.FillDataSet(ds_cb, "userentity")
                    userentity.Properties.DataSource = ds_cb.Tables("entity")
                    userentity.Properties.DisplayMember = ds_cb.Tables("entity").Columns("en_desc").ToString
                    userentity.Properties.ValueMember = ds_cb.Tables("entity").Columns("en_id").ToString
                    userentity.EditValue = ds_cb.Tables("entity").Rows(0).Item("en_id")

                End With
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Overrides Function get_sequel() As String
        get_sequel = "select userid, u.groupid,last_access, userkode, usernama,userphone, " _
                     & "useremail, password, last_access, usernik, useractive, en_desc, u.en_id,userpidgin,user_ptnr_id,ptnr_name,pin " + _
                      " from  tconfuser u " + _
                      " left outer join en_mstr e on e.en_id = u.en_id " + _
                       " left outer join ptnr_mstr f on f.ptnr_id = u.user_ptnr_id " + _
                      " order by usernama"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        usernama.Focus()
        usernama.Text = ""
        pin.Text = ""
        useractive.EditValue = False
        usernik.Text = ""
        userentity.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _trans_oid As Guid
        _trans_oid = Guid.NewGuid
        Dim ssqls As New ArrayList
        Dim userid As Integer
        Try
            Using objbantu As New master_new.WDABasepgsql("", "")
                With objbantu
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(max(userid),0) +1 as max_id from tconfuser"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read()
                        userid = .DataReader.Item("max_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                       
                        .Command.CommandType = CommandType.Text
                        '.Command.CommandText = "INSERT INTO  " _
                        '                    & "  public.trans_status " _
                        '                    & "( " _
                        '                    & "  trans_oid, " _
                        '                    & "  trans_id, " _
                        '                    & "  trans_desc, " _
                        '                    & "  trans_wf_start, " _
                        '                    & "  trans_dt " _
                        '                    & ")  " _
                        '                    & "VALUES ( " _
                        '                    & SetSetring(_trans_oid.ToString) & ",  " _
                        '                    & SetSetring(usernama.Text) & ",  " _
                        '                    & SetSetring(pin.Text) & ",  " _
                        '                    & SetBitYN(useractive.EditValue) & ",  " _
                        '                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                        '                    & ");"

                       

                        .Command.CommandText = "INSERT INTO  " _
                                           & "  public.tconfuser " _
                                           & "( " _
                                           & "  userid, " _
                                           & "  userkode, " _
                                           & "  usernama, " _
                                           & "  usernik, " _
                                           & "  pin, en_id," _
                                           & "  useractive " _
                                           & ")  " _
                                           & "VALUES ( " _
                                           & SetInteger(userid) & ",  " _
                                           & SetSetring(usernama.Text) & ",  " _
                                           & SetSetring(usernama.Text) & ",  " _
                                           & SetSetring(usernik.Text) & ",  " _
                                             & SetSetring(pin.EditValue) & ",  " _
                                             & SetInteger(userentity.EditValue) & ",  " _
                                           & SetBitYN(useractive.EditValue) & "  " _
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
                        set_row(Trim(usernama.Text), "usernama")
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
                _user_id = .Item("userid").ToString
                usernama.EditValue = .Item("usernama")
                pin.EditValue = .Item("pin")
                usernik.EditValue = .Item("usernik")
                userentity.EditValue = .Item("en_id")
                useractive.EditValue = SetBitYNB(.Item("useractive"))

            End With
            usernama.Focus()
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
                                            & "  public.tconfuser   " _
                                            & "SET  " _
                                            & "  userkode = " & SetSetring(usernama.EditValue) & ",  " _
                                            & "  usernama = " & SetSetring(usernama.EditValue) & ",  " _
                                            & "  en_id = " & SetInteger(userentity.EditValue) & ",  " _
                                            & "  pin = " & SetSetring(pin.EditValue) & ",  " _
                                            & "  useractive = " & SetBitYN(useractive.EditValue) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  userid = " & SetInteger(_user_id) & " "

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
                        set_row(Trim(usernama.Text), "usernama")
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
                            .Command.CommandText = "delete from tconfuser where userid  = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("userid")) + ""
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
