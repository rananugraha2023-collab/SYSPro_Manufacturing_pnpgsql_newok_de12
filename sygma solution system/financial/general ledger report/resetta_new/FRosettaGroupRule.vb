Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FRosettaGroupRule
    Dim ssql As String
    Dim _rstrule_oid_edit As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Dim _conf_value As String
    Public ds_edit As DataSet
    Public dt_edit_detail As DataTable


    Private Sub FRosettaRule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

    End Sub

    Public Overrides Sub load_cb()
    End Sub

    Private Sub rstrule_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column_copy(gv_master, "Account Code Hirarki", "rosgr_ac_hirarki", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Group", "rosgr_rosg_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group Desc", "rosg_desc", DevExpress.Utils.HorzAlignment.Default)
       
        add_column(gv_edit, "rosgr_oid", False)
        add_column(gv_edit, "Account Code Hirarki Debit", "rosgr_ac_hirarki", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_edit, "Account Sign", "ac_sign_hirarki", DevExpress.Utils.HorzAlignment.Center)

        add_column_edit(gv_edit, "Group", "rosgr_rosg_code", DevExpress.Utils.HorzAlignment.Default, init_le_repo("rosg_desc"))
        'add_column_edit(gv_edit, "Transaction Line", "rosr_rost_code", DevExpress.Utils.HorzAlignment.Default, init_le_repo("rost_desc"))

       
    End Sub


    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
            & "  a.rosgr_oid, " _
            & "  a.rosgr_rosg_code, " _
            & "  b.rosg_desc, " _
            & "  b.rosg_default_sign, " _
            & "  a.rosgr_ac_hirarki, " _
            & "  c.ac_code, " _
            & "  c.ac_name, " _
            & "  c.ac_desc " _
            & "FROM " _
            & "  public.rosrg_rule_group a " _
            & "  INNER JOIN public.rosg_group b ON (a.rosgr_rosg_code = b.rosg_code) " _
            & "  INNER JOIN public.ac_mstr c ON (a.rosgr_ac_hirarki = c.ac_code_hirarki) " _
            & "ORDER BY " _
            & "  a.rosgr_rosg_code"


        Return get_sequel
    End Function


    Public Overrides Sub load_data_grid_detail()
        Dim _get_oid As String = ""
        Dim i As Integer

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

    End Sub

    Public Overrides Sub insert_data_awal()

        _rstrule_oid_edit = ""

        ssql = "SELECT  " _
            & "  a.rosgr_oid, " _
            & "  a.rosgr_rosg_code, " _
            & "  b.rosg_desc, " _
            & "  b.rosg_default_sign, " _
            & "  a.rosgr_ac_hirarki, " _
            & "  c.ac_code, " _
            & "  c.ac_name, " _
            & "  c.ac_desc " _
            & "FROM " _
            & "  public.rosrg_rule_group a " _
            & "  INNER JOIN public.rosg_group b ON (a.rosgr_rosg_code = b.rosg_code) " _
            & "  INNER JOIN public.ac_mstr c ON (a.rosgr_ac_hirarki = c.ac_code_hirarki) " _
            & "ORDER BY " _
            & "  a.rosgr_rosg_code"
        dt_edit_detail = GetTableData(ssql)
        gc_edit.DataSource = dt_edit_detail
        gv_edit.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            'DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Function insert() As Boolean
        Dim _rstrule_oid As String = Guid.NewGuid.ToString
        Dim ssqls As New ArrayList


        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        Dim _seq As Integer = 0
                        For Each dr As DataRow In dt_edit_detail.Rows

                            ssql = "INSERT INTO  " _
                                    & "  public.rosrg_rule_group " _
                                    & "( " _
                                    & "  rosgr_oid, " _
                                    & "  rosgr_rosg_code, " _
                                    & "  rosgr_ac_hirarki " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(dr("rosgr_rosg_code")) & ",  " _
                                    & SetSetring(dr("rosgr_ac_hirarki")) & "  " _
                                    & ")"


                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        insert = True
                        sqlTran.Commit()

                        after_success()
                        'set_row(_rstrule_oid, "rstrule_oid")
                        'dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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


    Public Overrides Sub relation_detail()
        Try
            'gv_detail.Columns("rstruled_rstrule_oid").FilterInfo = _
            'New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rstruled_rstrule_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rstrule_oid").ToString & "'")
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            'rstrule_en_id.Focus()

            'row = BindingContext(ds.Tables(0)).Position

            ssql = "SELECT  " _
                & "  a.rosgr_oid, " _
                & "  a.rosgr_rosg_code, " _
                & "  b.rosg_desc, " _
                & "  b.rosg_default_sign, " _
                & "  a.rosgr_ac_hirarki, " _
                & "  c.ac_code, " _
                & "  c.ac_name, " _
                & "  c.ac_desc " _
                & "FROM " _
                & "  public.rosrg_rule_group a " _
                & "  INNER JOIN public.rosg_group b ON (a.rosgr_rosg_code = b.rosg_code) " _
                & "  INNER JOIN public.ac_mstr c ON (a.rosgr_ac_hirarki = c.ac_code_hirarki) " _
                & "ORDER BY " _
                & "  a.rosgr_rosg_code"

            dt_edit_detail = GetTableData(ssql)
            gc_edit.DataSource = dt_edit_detail
            gv_edit.BestFitColumns()

            Try
                tcg_header.SelectedTabPageIndex = 0
                'DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit_detail.AcceptChanges()

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
                        .Command.CommandText = "delete from rosrg_rule_group "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0
                        For Each dr As DataRow In dt_edit_detail.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.rosrg_rule_group " _
                                    & "( " _
                                    & "  rosgr_oid, " _
                                    & "  rosgr_rosg_code, " _
                                    & "  rosgr_ac_hirarki " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(dr("rosgr_rosg_code")) & ",  " _
                                    & SetSetring(dr("rosgr_ac_hirarki")) & "  " _
                                    & ")"

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        'If master_new.PGSqlConn.status_sync = True Then
                        '    For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                        '        .Command.CommandType = CommandType.Text
                        '        .Command.CommandText = Data
                        '        .Command.ExecuteNonQuery()
                        '        .Command.Parameters.Clear()
                        '    Next
                        'End If

                        sqlTran.Commit()
                        after_success()
                        'set_row(Trim(_rstrule_oid_edit.ToString), "rstrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        Dim ssqls As New ArrayList

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

                            ssql = "delete from rosrg_rule_group where rosgr_oid = '" + ds.Tables(0).Rows(row).Item("rosgr_oid").ToString + "'"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            sqlTran.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As nPgSqlException
                            sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            delete_data = False
                        End Try
                    End With
                End Using

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                delete_data = False
            End Try
        End If

        Return delete_data
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        'DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit_detail.AcceptChanges()

        'cek sign
        For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
            
            If SetString(dt_edit_detail.Rows(i).Item("rosgr_rosg_code")) = "" Then
                MsgBox("Group is empty on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unable to Save")
                before_save = False
            End If

            If SetString(dt_edit_detail.Rows(i).Item("rosgr_ac_hirarki")) = "" Then
                MsgBox("Account is empty on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unable to Save")
                before_save = False
            End If
          
        Next


        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub



    '---------------------------------------------------------------

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "rosgr_ac_hirarki" Or _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub



    Private Sub BtClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            dt_edit_detail.Rows.Clear()
            dt_edit_detail.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub te_ac_from1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            frm._obj = sender
            frm._col_name = ""
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        Try
         
        Catch ex As Exception

        End Try
    End Sub
End Class
