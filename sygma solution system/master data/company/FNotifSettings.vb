Imports npgsql
Imports master_new.ModFunction

Public Class FNotifSettings
    Dim _tran_oid_mstr As String
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim ds_edit As DataSet

    Private Sub FNotifSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Transaction ID", "tran_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Name", "tran_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "tran_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User", "usernama", DevExpress.Utils.HorzAlignment.Near)
        add_column_copy(gv_master, "Active", "tran_active", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail_flow, "trane_tran_oid", False)
        add_column_copy(gv_detail_flow, "Transaction Status", "trane_trans_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_flow, "Sequence", "trane_seq", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_user, "tran_oid", False)
        add_column(gv_detail_user, "aprv_tran_id", False)
        add_column_copy(gv_detail_user, "User Approval", "aprv_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_user, "Sequence", "aprv_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_user, "Type", "aprv_type", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "aprv_oid", False)
        add_column(gv_edit, "User Approval", "aprv_user_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Sequence", "aprv_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Type", "aprv_type", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  tran_oid, " _
                    & "  tran_id, " _
                    & "  tran_table, " _
                    & "  tran_name, " _
                    & "  tran_desc, " _
                    & "  tran_review_amount, " _
                    & "  tran_dt, " _
                    & "  tranu_user_id, " _
                    & "  tran_active,usernama " _
                    & "FROM  " _
                    & "  public.tran_mstr " _
                    & "  left outer join tranu_usr on tranu_tran_id = tran_id " _
                    & "  left outer join tconfuser on tranu_user_id = userid " _
                    & " order by tran_name,usernama"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("flow").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  trane_oid, " _
            & "  trane_tran_oid, " _
            & "  trane_seq, " _
            & "  trane_trans_id, " _
            & "  trane_edit, " _
            & "  trane_delete, " _
            & "  trane_dt " _
            & "FROM  " _
            & "  public.trane_entity " _
            & "  inner join public.tran_mstr on tran_oid = trane_tran_oid " _
            & "  inner join tranu_usr on tranu_tran_id = tran_id "

        load_data_detail(sql, gc_detail_flow, "flow")

        Try
            ds.Tables("user").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  aprv_oid, " _
            & "  aprv_tran_id, " _
            & "  aprv_user_id, " _
            & "  aprv_dt, " _
            & "  tran_oid, " _
            & "  case when aprv_type = '0' then 'User' when aprv_type = '1' then 'Group' end as aprv_type, " _
            & "  aprv_seq " _
            & "FROM  " _
            & "  public.aprv_mstr " _
            & "  inner join tran_mstr on tran_id = aprv_tran_id" _
            & "  inner join tranu_usr on tranu_tran_id = tran_id " _
            & " order by aprv_seq"

        load_data_detail(sql, gc_detail_user, "user")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_flow.Columns("trane_tran_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[trane_tran_oid]='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid").ToString & "'")
            gv_detail_flow.BestFitColumns()

            gv_detail_user.Columns("aprv_tran_id").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[aprv_tran_id]='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_id").ToString & "'")
            gv_detail_user.BestFitColumns()

            'gv_detail_flow.Columns("trane_tran_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid"))
            'gv_detail_flow.BestFitColumns()

            'gv_detail_user.Columns("tran_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid"))
            'gv_detail_user.BestFitColumns()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        tran_name.Focus()
        tran_name.Text = ""
        tran_desc.Text = ""
        tran_active.EditValue = False
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  aprv_oid, " _
                            & "  aprv_tran_id, " _
                            & "  aprv_user_id, " _
                            & "  aprv_dt, " _
                            & "  tran_oid, " _
                            & "  case when aprv_type = '0' then 'User' when aprv_type = '1' then 'Group' end as aprv_type, " _
                            & "  aprv_seq " _
                            & "FROM  " _
                            & "  public.aprv_mstr " _
                            & "  inner join tran_mstr on tran_id = aprv_tran_id" _
                            & "  inner join tranu_usr on tranu_tran_id = tran_id" _
                            & "  where tranu_user_id = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "insert_edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function insert() As Boolean
        Dim _tran_oid As Guid
        _tran_oid = Guid.NewGuid

        Dim ssqls As New ArrayList
        Dim i As Integer
        Dim _trans_id As String = ""

        Dim _tran_id As Integer
        _tran_id = func_coll.GetID("tran_mstr", "tran_id")

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
                                            & "  public.tran_mstr " _
                                            & "( " _
                                            & "  tran_oid, " _
                                            & "  tran_id, " _
                                            & "  tran_table, " _
                                            & "  tran_name, " _
                                            & "  tran_desc, " _
                                            & "  tran_review_amount, " _
                                            & "  tran_active, " _
                                            & "  tran_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_tran_oid.ToString) & ",  " _
                                            & SetInteger(_tran_id) & ",  " _
                                            & SetSetring("") & ",  " _
                                            & SetSetring(tran_name.Text) & ",  " _
                                            & SetSetring(tran_desc.Text) & ",  " _
                                            & SetBitYN(False) & ",  " _
                                            & SetBitYN(tran_active.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To 3

                            If i = 0 Then
                                _trans_id = "D"
                            ElseIf i = 1 Then
                                _trans_id = "W"
                            ElseIf i = 2 Then
                                _trans_id = "I"
                            ElseIf i = 3 Then
                                _trans_id = "C"
                            End If

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.trane_entity " _
                                                & "( " _
                                                & "  trane_oid, " _
                                                & "  trane_tran_oid, " _
                                                & "  trane_seq, " _
                                                & "  trane_trans_id, " _
                                                & "  trane_edit, " _
                                                & "  trane_delete, " _
                                                & "  trane_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_tran_oid.ToString) & ",  " _
                                                & SetInteger(i) & ",  " _
                                                & SetSetring(_trans_id) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetSetring("") & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.aprv_mstr " _
                                                & "( " _
                                                & "  aprv_oid, " _
                                                & "  aprv_tran_id, " _
                                                & "  aprv_user_id, " _
                                                & "  aprv_dt, " _
                                                & "  aprv_seq, " _
                                                & "  aprv_type " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(_tran_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                & SetSetring(IIf(ds_edit.Tables(0).Rows(i).Item("aprv_type") = "User", 0, 1)) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.tranu_usr " _
                                            & "( " _
                                            & "  tranu_oid, " _
                                            & "  tranu_tran_id, " _
                                            & "  tranu_user_id, " _
                                            & "  tranu_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(_tran_id) & ",  " _
                                            & SetInteger(tranu_user_id.Tag) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & "  " _
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
                        set_row(Trim(_tran_oid.ToString), "tran_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
                _tran_oid_mstr = .Item("tran_oid").ToString
                tran_name.Text = SetString(.Item("tran_name"))
                tran_name.Tag = .Item("tran_id")
                tran_desc.Text = SetString(.Item("tran_desc"))
                tran_active.EditValue = SetBitYNB(.Item("tran_active"))
                tranu_user_id.EditValue = .Item("usernama")
                tranu_user_id.Tag = .Item("tranu_user_id")
            End With
            tran_name.Focus()

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  aprv_oid, " _
                                & "  aprv_tran_id, " _
                                & "  aprv_user_id, " _
                                & "  aprv_dt, " _
                                & "  tran_oid, " _
                                & "  case when aprv_type = '0' then 'User' when aprv_type = '1' then 'Group' end as aprv_type, " _
                                & "  aprv_seq " _
                                & "FROM  " _
                                & "  public.aprv_mstr " _
                                & "  inner join tran_mstr on tran_id = aprv_tran_id" _
                                & "  where aprv_tran_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_id").ToString
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "insert_edit")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i As Integer
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
                                            & "  public.tran_mstr   " _
                                            & "SET  " _
                                            & "  tran_name = " & SetSetring(tran_name.Text) & ",  " _
                                            & "  tran_desc = " & SetSetring(tran_desc.Text) & ",  " _
                                            & "  tran_active = " & SetBitYN(tran_active.EditValue) & ",  " _
                                            & "  tran_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tran_oid = " & SetSetring(_tran_oid_mstr.ToString) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.tranu_usr   " _
                                            & "SET  " _
                                            & "  tranu_user_id = " & SetInteger(tranu_user_id.Tag) & ",  " _
                                            & "  tranu_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  tranu_tran_id = " & SetInteger(tran_name.Tag) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from aprv_mstr where aprv_tran_id = " + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_id").ToString
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.aprv_mstr " _
                                                & "( " _
                                                & "  aprv_oid, " _
                                                & "  aprv_tran_id, " _
                                                & "  aprv_user_id, " _
                                                & "  aprv_dt, " _
                                                & "  aprv_seq, " _
                                                & "  aprv_type " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_id")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("aprv_user_id")) & ",  " _
                                                & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("aprv_seq")) & ",  " _
                                                & SetSetring(IIf(ds_edit.Tables(0).Rows(i).Item("aprv_type") = "User", 0, 1)) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

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
                        set_row(Trim(_tran_oid_mstr.ToString), "tran_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
                            .Command.CommandText = "delete from tran_mstr where tran_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("tran_oid").ToString + "'"
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

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        Dim _seq As Integer
        _seq = ds_edit.Tables(0).Rows.Count
        gv_edit.SetRowCellValue(e.RowHandle, "aprv_seq", _seq)
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _pod_en_id As Integer = gv_edit.GetRowCellValue(_row, "pod_en_id")

        If _col = "aprv_user_id" Then
            Dim frm As New FUserApprovalSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm._obj = gv_edit
            frm.ShowDialog()
        End If
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Dim i As Integer
        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            If ds_edit.Tables(0).Rows(i).Item("aprv_seq") <> i Then
                MessageBox.Show("Not Valid Sequence...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub tranu_user_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tranu_user_id.ButtonClick
        Dim frm As New FUserApprovalSearch
        frm.set_win(Me)
        frm.type_form = True
        frm._obj = tranu_user_id
        frm.ShowDialog()
    End Sub

    'Private Sub tranu_group_id(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles tranu_group_id.ButtonClick
    '    Dim frm As New FUserApprovalSearch
    '    frm.set_win(Me)
    '    frm.type_form = True
    '    frm._obj = tranu_group_id
    '    frm.ShowDialog()
    'End Sub
End Class
