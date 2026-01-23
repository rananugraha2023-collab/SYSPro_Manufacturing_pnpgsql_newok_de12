Imports npgsql
Imports master_new.ModFunction

Public Class FPointMasterNew

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pi_oid_mstr As String
    Dim _now As DateTime
    Public ds_edit_item, ds_edit_rule As DataSet

    Private Sub FPriceList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        AddHandler gv_edit_item.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_edit_item.ColumnFilterChanged, AddressOf relation_detail
    End Sub

    Public Overrides Sub load_cb()
       
    End Sub

    Public Overrides Sub load_cb_en()

    End Sub

    Private Sub pi_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Start Date", "secommp_start_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "End Date", "secommp_end_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "Active", "secommp_active", DevExpress.Utils.HorzAlignment.Default)
        
        add_column(gv_detail_item, "secomm_secommp_oid", False)
        add_column_copy(gv_detail_item, "Min Point", "secomm_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_item, "Max Point", "secomm_max", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_item, "Multiplier", "secomm_multiplier", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail_item, "Type", "secomm_type", DevExpress.Utils.HorzAlignment.Default)

        add_column_edit(gv_edit_item, "Min Point", "secomm_min", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_item, "Max Point", "secomm_max", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_item, "Multiplier", "secomm_multiplier", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit_item, "Type", "secomm_type", DevExpress.Utils.HorzAlignment.Default)


      
    End Sub

    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                    & "  a.secommp_oid, " _
                    & "  a.secommp_start_date, " _
                    & "  a.secommp_end_date, " _
                    & "  a.secommp_active " _
                    & "FROM " _
                    & "  public.secomm_periode a " _
                    & "ORDER BY " _
                    & "  a.secommp_start_date"

        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String = ""

        Try
            ds.Tables("detail_item").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  a.secomm_oid, " _
            & "  a.secomm_min, " _
            & "  a.secomm_max, " _
            & "  a.secomm_multiplier, " _
            & "  a.secomm_type, " _
            & "  a.secomm_secommp_oid " _
            & "FROM " _
            & "  public.secomm_mstr a " _
            & "ORDER BY " _
            & "  secomm_type,a.secomm_min"


        load_data_detail(sql, gc_detail_item, "detail_item")

       
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail_item.Columns("secomm_secommp_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("secomm_secommp_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("secommp_oid").ToString & "'")
            gv_detail_item.BestFitColumns()

        Catch ex As Exception
        End Try

        'Try
        '    gv_edit_rule.Columns("pidd_pid_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("pidd_pid_oid='" & ds_edit_item.Tables(0).Rows(BindingContext(ds_edit_item.Tables(0)).Position).Item("pid_oid").ToString & "'")
        '    gv_edit_rule.BestFitColumns()
        'Catch ex As Exception
        'End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

        secommp_start_date.DateTime = _now
        secommp_end_date.DateTime = _now
        secommp_active.EditValue = True

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_item = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  a.secomm_oid, " _
                            & "  a.secomm_min, " _
                            & "  a.secomm_max, " _
                            & "  a.secomm_multiplier, " _
                            & "  a.secomm_type, " _
                            & "  a.secomm_secommp_oid " _
                            & "FROM " _
                            & "  public.secomm_mstr a where secomm_oid is null " _
                            & "ORDER BY " _
                            & "  a.secomm_min"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_item, "item")
                    gc_edit_item.DataSource = ds_edit_item.Tables(0)
                    gv_edit_item.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Sub gv_edit_item_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_item.DoubleClick
        'Dim _col As String = gv_edit_item.FocusedColumn.Name
        'Dim _row As Integer = gv_edit_item.FocusedRowHandle

        'If _col = "pt_code" Then
        '    Dim frm As New FPartNumberSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = pi_en_id.EditValue
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_item_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_item.InitNewRow
        With gv_edit_item
            .SetRowCellValue(e.RowHandle, "pid_oid", Guid.NewGuid.ToString)
        End With
    End Sub

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_item.UpdateCurrentRow()
        'gv_edit_rule.UpdateCurrentRow()

        ds_edit_item.AcceptChanges()
        ' ds_edit_rule.AcceptChanges()

        'Ini Diperbolehkan kosong...,kalau kosong artinya berlaku untuk semua barang
        'If ds_edit_item.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'If ds_edit_rule.Tables(0).Rows.Count = 0 Then
        '    MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        'Dim i As Integer

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim _pi_oid As Guid = Guid.NewGuid
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
                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.secomm_periode " _
                                        & "( " _
                                        & "  secommp_oid, " _
                                        & "  secommp_start_date, " _
                                        & "  secommp_end_date, " _
                                        & "  secommp_active " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_pi_oid.ToString) & ",  " _
                                        & SetDate(secommp_start_date.EditValue) & ",  " _
                                        & SetDate(secommp_end_date.EditValue) & ",  " _
                                        & SetBitYN(secommp_active.EditValue) & "  " _
                                        & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.secomm_mstr " _
                                            & "( " _
                                            & "  secomm_oid, " _
                                            & "  secomm_min, " _
                                            & "  secomm_max, " _
                                            & "  secomm_multiplier, " _
                                            & "  secomm_type, " _
                                            & "  secomm_secommp_oid " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_min")) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_max")) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_multiplier")) & ",  " _
                                            & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("secomm_type")) & ",  " _
                                            & SetSetring(_pi_oid.ToString) & "  " _
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
                        set_row(_pi_oid.ToString, "secommp_oid")
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
                            .Command.CommandText = "delete from secomm_periode where secommp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("secommp_oid").ToString + "'"
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _pi_oid_mstr = .Item("secommp_oid").ToString
                secommp_start_date.DateTime = .Item("secommp_start_date")
                secommp_end_date.DateTime = .Item("secommp_end_date")
                secommp_active.EditValue = SetBitYNB(.Item("secommp_active"))
            End With

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit_item = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.secomm_oid, " _
                            & "  a.secomm_min, " _
                            & "  a.secomm_max, " _
                            & "  a.secomm_multiplier, " _
                            & "  a.secomm_type, " _
                            & "  a.secomm_secommp_oid " _
                            & "FROM " _
                            & "  public.secomm_mstr a " _
                             & "  where secomm_secommp_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("secommp_oid").ToString + "' " _
                            & "ORDER BY " _
                            & " secomm_type, a.secomm_min"
                        .InitializeCommand()
                        .FillDataSet(ds_edit_item, "pid_det")
                        gc_edit_item.DataSource = ds_edit_item.Tables(0)
                        gv_edit_item.BestFitColumns()
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
                                    & "  public.secomm_periode   " _
                                    & "SET  " _
                                    & "  secommp_start_date = " & SetDate(secommp_start_date.EditValue) & ",  " _
                                    & "  secommp_end_date = " & SetDate(secommp_end_date.EditValue) & ",  " _
                                    & "  secommp_active = " & SetBitYN(secommp_active.EditValue) & "  " _
                                    & "WHERE  " _
                                    & "  secommp_oid = " & SetSetring(_pi_oid_mstr.ToString) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from secomm_mstr where secomm_secommp_oid = '" + _pi_oid_mstr + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit_item.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                            & "  public.secomm_mstr " _
                                            & "( " _
                                            & "  secomm_oid, " _
                                            & "  secomm_min, " _
                                            & "  secomm_max, " _
                                            & "  secomm_multiplier, " _
                                            & "  secomm_type, " _
                                            & "  secomm_secommp_oid " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_min")) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_max")) & ",  " _
                                            & SetDec(ds_edit_item.Tables(0).Rows(i).Item("secomm_multiplier")) & ",  " _
                                            & SetSetring(ds_edit_item.Tables(0).Rows(i).Item("secomm_type")) & ",  " _
                                            & SetSetring(_pi_oid_mstr.ToString) & "  " _
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
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        set_row(_pi_oid_mstr, "secommp_oid")
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

