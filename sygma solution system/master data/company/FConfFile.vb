Imports npgsql
Imports master_new.ModFunction

Public Class FConfFile
    Dim _conf_oid_mstr, _value As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ssqls As New ArrayList

    Private Sub FConfFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Configuration Name", "conf_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Configuration Value", "conf_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  conf_oid, conf_name, " _
                    & "  conf_value " _
                    & " FROM  " _
                    & "  public.conf_file"
        Return get_sequel
    End Function

    Public Overrides Function delete_data() As Boolean

        'MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Sub insert_data_awal()
        conf_name.Enabled = True
        conf_name.Properties.ReadOnly = False
        conf_name.Focus()
        conf_name.Text = ""
        conf_value.Text = ""
    End Sub

    'Public Overrides Function insert_data() As Boolean
    '    MessageBox.Show("Insert Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim _conf_oid As Guid
        _conf_oid = Guid.NewGuid

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.conf_file " _
                                            & "( " _
                                            & "  conf_oid, " _
                                            & "  conf_name, " _
                                            & "  conf_value " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_conf_oid.ToString) & ",  " _
                                            & SetSetring(conf_name.Text) & ",  " _
                                            & SetSetring(conf_value.Text) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Conf File " & conf_name.EditValue)
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
                        set_to_data_insert()
                        after_success()
                        set_row(_conf_oid.ToString, "conf_oid")
                        insert = True
                    Catch ex As Npgsql.NpgsqlException
                        row = 0
                        insert = False
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Npgsql.NpgsqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        'gc_mstr.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _conf_oid_mstr = .Item("conf_oid").ToString
                conf_name.Text = .Item("conf_name")
                conf_value.Text = .Item("conf_value")
            End With
            conf_name.Enabled = False
            conf_name.Properties.ReadOnly = True
            conf_value.Focus()
            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Try
            ' _conf_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("conf_oid")
            '_value = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("conf_value")
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.conf_file   " _
                                            & "SET  " _
                                            & "  conf_value = " & SetSetring(conf_value.EditValue) & " " _
                                            & "WHERE  " _
                                            & "  conf_oid = " & SetSetring(_conf_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Conf File " & conf_name.EditValue & " " & conf_value.EditValue)
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
                        set_row(Trim(_conf_oid_mstr), "conf_oid")
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

End Class
