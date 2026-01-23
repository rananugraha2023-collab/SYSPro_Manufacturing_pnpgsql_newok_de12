Imports npgsql
Imports master_new.ModFunction

Public Class FRosTransLine
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pts_oid_mstr As String

    Private Sub FItemSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Code", "rost_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "rost_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Code", "rost_cf", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
            & "  a.rost_code, " _
            & "  a.rost_desc, " _
            & "  a.rost_cf " _
            & "FROM " _
            & "  public.rost_trans_line a " _
            & "ORDER BY " _
            & "  a.rost_code"


        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        rost_code.Text = ""
        rost_desc.Text = ""
        rost_cf.EditValue = ""
        rost_code.Focus()
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
                            & "  public.rost_trans_line " _
                            & "( " _
                            & "  rost_code, " _
                            & "  rost_desc, " _
                            & "  rost_cf " _
                            & ")  " _
                            & "VALUES ( " _
                            & SetSetring(rost_code.EditValue) & ",  " _
                            & SetSetring(rost_desc.EditValue) & ",  " _
                            & SetSetring(rost_cf.EditValue) & "  " _
                            & ")"


                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        set_row(Trim(rost_code.EditValue), "rost_code")
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
            rost_code.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pts_oid_mstr = .Item("rost_code")
                rost_code.EditValue = .Item("rost_code")
                rost_desc.EditValue = .Item("rost_desc")
                rost_cf.EditValue = .Item("rost_cf")
              
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
                                & "  public.rost_trans_line   " _
                                & "SET  " _
                                & "  rost_desc = " & SetSetring(rost_desc.EditValue) & ",  " _
                                 & "  rost_code = " & SetSetring(rost_code.EditValue) & ",  " _
                                & "  rost_cf = " & SetSetring(rost_cf.EditValue) & "  " _
                                & "WHERE  " _
                                & "  rost_code = " & SetSetring(_pts_oid_mstr) & " "


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
                            .Command.CommandText = "delete from rost_trans_line where rost_code = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rost_code") + "'"
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
