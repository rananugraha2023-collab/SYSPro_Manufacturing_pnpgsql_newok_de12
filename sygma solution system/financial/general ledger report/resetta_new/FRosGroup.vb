Imports npgsql
Imports master_new.ModFunction

Public Class FRosGroup
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pts_oid_mstr As String

    Private Sub FItemSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "rosg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "rosg_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Default Sign", "rosg_default_sign", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.rosg_code, " _
                & "  a.rosg_desc, " _
                & "  a.rosg_default_sign " _
                & "FROM " _
                & "  public.rosg_group a " _
                & "ORDER BY " _
                & "  a.rosg_code"

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        rosg_code.Text = ""
        rosg_desc.Text = ""
        rosg_default_sign.EditValue = ""
        rosg_code.Focus()
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _pts_oid As Guid
        _pts_oid = Guid.NewGuid

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
                            & "  public.rosg_group " _
                            & "( " _
                            & "  rosg_code, " _
                            & "  rosg_desc, " _
                            & "  rosg_default_sign " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(rosg_code.EditValue) & ",  " _
                            & SetSetring(rosg_desc.EditValue) & ",  " _
                            & SetSetring(rosg_default_sign.EditValue) & "  " _
                            & ")"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(rosg_code.EditValue), "rosg_code")
                        insert = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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
            rosg_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pts_oid_mstr = .Item("rosg_code")
                rosg_code.EditValue = .Item("rosg_code")
                rosg_desc.EditValue = .Item("rosg_desc")
                rosg_default_sign.EditValue = .Item("rosg_default_sign")

            End With

            edit_data = True
        End If
    End Function

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
                                & "  public.rosg_group   " _
                                & "SET  " _
                                & "  rosg_desc = " & SetSetring(rosg_desc.EditValue) & ",  " _
                                 & "  rosg_code = " & SetSetring(rosg_code.EditValue) & ",  " _
                                & "  rosg_default_sign = " & SetSetring(rosg_default_sign.EditValue) & "  " _
                                & "WHERE  " _
                                & "  rosg_code = " & SetSetring(_pts_oid_mstr) & " "


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(_pts_oid_mstr.ToString), "rost_code")
                        edit = True
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " akan mengHapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
                            .Command.CommandText = "delete from rosg_group where rosg_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rosg_code") + "'"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Success..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
