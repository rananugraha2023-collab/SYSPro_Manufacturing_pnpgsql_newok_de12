Imports npgsql

Public Class FMenuAuthorization

    Dim int_groupid, int_menuid As Integer

    Private Sub FMenuAuthorization_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        Try
            Dim ds_cb As New DataSet
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "enable_status")
                    sc_cbenable_status.DataSource = ds_cb.Tables("enable_status")
                    sc_cbenable_status.DisplayMember = "display"
                    sc_cbenable_status.ValueMember = "value"

                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "visible_status")
                    sc_cbvisible_status.DataSource = ds_cb.Tables("visible_status")
                    sc_cbvisible_status.DisplayMember = "display"
                    sc_cbvisible_status.ValueMember = "value"

                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "editable_status")
                    sc_cbeditable_status.DataSource = ds_cb.Tables("editable_status")
                    sc_cbeditable_status.DisplayMember = "display"
                    sc_cbeditable_status.ValueMember = "value"

                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "deleteable_status")
                    sc_cb_delete.DataSource = ds_cb.Tables("deleteable_status")
                    sc_cb_delete.DisplayMember = "display"
                    sc_cb_delete.ValueMember = "value"

                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "insert_status")
                    sc_cb_insert.DataSource = ds_cb.Tables("insert_status")
                    sc_cb_insert.DisplayMember = "display"
                    sc_cb_insert.ValueMember = "value"

                    .SQL = "help_status"
                    .InitializeCommand()
                    .FillDataSet(ds_cb, "cancelable_status")
                    cancelable_status.DataSource = ds_cb.Tables("cancelable_status")
                    cancelable_status.DisplayMember = "display"
                    cancelable_status.ValueMember = "value"
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "GroupID", "groupid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "MenuID", "menuid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Group", "groupnama", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu", "menudesc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Enable Status", "enablestatus", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Visible Status", "visiblestatus", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Insertable Status", "insertablestatus", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Editable Status", "editablestatus", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Deleteable Status", "deleteablestatus", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Cancelable Status", "cancelablestatus", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select m.groupid, m.menuid, groupnama, menuname,menudesc, enablestatus, visiblestatus, insertablestatus, editablestatus, deleteablestatus, cancelablestatus " + _
                     " from tconfmenu m " + _
                     " inner join tconfgroup g on g.groupid = m.groupid " + _
                     " inner join tconfmenucollection mc on mc.menuid = m.menuid " + _
                     " order by groupnama, menudesc "
        Return get_sequel
    End Function

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu")
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Insert Data Not Available For This Menu")
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                int_groupid = .Item("groupid")
                int_menuid = .Item("menuid")

                sc_txtgroup_1.Text = .Item("groupnama")
                sc_txtmenu.Text = .Item("menudesc")
                sc_cbenable_status.SelectedValue = .Item("enablestatus")
                sc_cbvisible_status.SelectedValue = .Item("visiblestatus")
                sc_cbeditable_status.SelectedValue = .Item("editablestatus")
                sc_cb_delete.SelectedValue = .Item("deleteablestatus")
                sc_cb_insert.SelectedValue = .Item("insertablestatus")
                cancelable_status.SelectedValue = .Item("cancelablestatus")
            End With
            sc_txtgroup_1.Properties.ReadOnly = True
            sc_txtmenu.Properties.ReadOnly = True
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
                                            & "  public.tconfmenu   " _
                                            & "SET  " _
                                            & "  enablestatus = " & sc_cbenable_status.SelectedValue & ",  " _
                                            & "  visiblestatus = " & sc_cbvisible_status.SelectedValue & ",  " _
                                            & "  deleteablestatus = " & sc_cb_delete.SelectedValue & ",  " _
                                            & "  cancelablestatus = " & cancelable_status.SelectedValue & ",  " _
                                            & "  insertablestatus = " & sc_cb_insert.SelectedValue & ",  " _
                                            & "  editablestatus = " & sc_cbeditable_status.SelectedValue & "  " _
                                            & "  " _
                                            & "WHERE groupid = " & int_groupid.ToString & "  " _
                                            & "and menuid = " & int_menuid.ToString
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
End Class
