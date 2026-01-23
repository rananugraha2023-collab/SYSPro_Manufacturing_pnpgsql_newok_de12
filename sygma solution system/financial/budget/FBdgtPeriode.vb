Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FBdgtPeriode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr As String
    Dim ds_edit As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime
    Dim mf As New master_new.ModFunction
    Dim _cc_id_mstr As Integer

#Region "Seting Awal"
    Private Sub FBdgtPeriode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        bdgtp_en_id.Properties.DataSource = dt_bantu
        bdgtp_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        bdgtp_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        bdgtp_en_id.ItemIndex = 0
    End Sub

    Private Sub bdgt_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bdgtp_en_id.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("cc_mstr", bdgtp_en_id.EditValue))
        'cca_cc_id.Properties.DataSource = dt_bantu
        'cca_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        'cca_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        'cca_cc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Budget Year", "bdgtp_year", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "bdgtp_year", False)
        add_column(gv_detail, "Code", "bdgtp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Remarks", "bdgtp_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Start Date", "bdgtp_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_detail, "End Date", "bdgtp_end_date", DevExpress.Utils.HorzAlignment.Center)

        add_column_edit(gv_edit, "Code", "bdgtp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Remarks", "bdgtp_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Start Date", "bdgtp_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_edit(gv_edit, "End Date", "bdgtp_end_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  distinct(bdgtp_year), " _
                    & "  bdgtp_en_id,en_desc " _
                    & "FROM  " _
                    & "  public.bdgtp_periode  " _
                    & "  inner join en_mstr on en_id = bdgtp_en_id " _
                    & ""
        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  bdgtp_oid, " _
            & "  bdgtp_dom_id, " _
            & "  bdgtp_en_id, " _
            & "  bdgtp_add_by, " _
            & "  bdgtp_add_date, " _
            & "  bdgtp_upd_by, " _
            & "  bdgtp_upd_date, " _
            & "  bdgtp_id, " _
            & "  bdgtp_code, " _
            & "  bdgtp_remarks, " _
            & "  bdgtp_start_date, " _
            & "  bdgtp_end_date, " _
            & "  bdgtp_dt, " _
            & "  bdgtp_year " _
            & "FROM  " _
            & "  public.bdgtp_periode " _
            & " where bdgtp_id <> 0 " _
            & " order by bdgtp_id asc "
        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("bdgtp_year").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("bdgtp_year='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_year").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("bdgtp_year").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_year"))
            'gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "valuechanged"
    'Public Overrides Sub load_cb_en()
    '    dt_bantu = New DataTable
    '    dt_bantu = (func_data.load_data("ptnr_mstr_vend", asrtr_en_id.EditValue))
    '    po_ptnr_id.Properties.DataSource = dt_bantu
    '    po_ptnr_id.Properties.DisplayMember = dt_bantu.Columns("ptnr_name").ToString
    '    po_ptnr_id.Properties.ValueMember = dt_bantu.Columns("ptnr_id").ToString
    '    po_ptnr_id.ItemIndex = 0
    'End Sub
#End Region

#Region "DML"
    Public Overrides Sub insert_data_awal()
        bdgtp_en_id.Focus()
        bdgtp_en_id.ItemIndex = 0
        bdgtp_year.Text = ""
        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try

    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        ds_edit = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  bdgtp_oid, " _
                            & "  bdgtp_dom_id, " _
                            & "  bdgtp_en_id, " _
                            & "  bdgtp_add_by, " _
                            & "  bdgtp_add_date, " _
                            & "  bdgtp_upd_by, " _
                            & "  bdgtp_upd_date, " _
                            & "  bdgtp_id, " _
                            & "  bdgtp_code, " _
                            & "  bdgtp_remarks, " _
                            & "  bdgtp_start_date, " _
                            & "  bdgtp_end_date, " _
                            & "  bdgtp_dt, " _
                            & "  bdgtp_year " _
                            & "FROM  " _
                            & "  public.bdgtp_periode " _
                            & " where bdgtp_id = -52642 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            before_save = False
        End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim ds_bantu As New DataSet
        Dim i As Integer
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        Dim _bdgtp_id As Integer
                        _bdgtp_id = SetNewID_OLD("bdgtp_periode", "bdgtp_id")
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _bdgtp_id = _bdgtp_id + 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.bdgtp_periode " _
                                                & "( " _
                                                & "  bdgtp_oid, " _
                                                & "  bdgtp_dom_id, " _
                                                & "  bdgtp_en_id, " _
                                                & "  bdgtp_add_by, " _
                                                & "  bdgtp_add_date, " _
                                                & "  bdgtp_id, " _
                                                & "  bdgtp_code, " _
                                                & "  bdgtp_remarks, " _
                                                & "  bdgtp_start_date, " _
                                                & "  bdgtp_end_date, " _
                                                & "  bdgtp_dt, " _
                                                & "  bdgtp_year " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(bdgtp_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(_bdgtp_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("bdgtp_code")) & ",  " _
                                                & SetSetringDB(ds_edit.Tables(0).Rows(i).Item("bdgtp_remarks")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("bdgtp_start_date")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("bdgtp_end_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(bdgtp_year.Text) & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        after_success()
                        'set_row(_bdgt_oid.ToString, "bdgt_oid")
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If ds.Tables.Count = 0 Then
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            Exit Function
        End If

        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                bdgtp_en_id.EditValue = .Item("bdgtp_en_id")
                bdgtp_year.Text = .Item("bdgtp_year")
            End With
            bdgtp_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                                & "  bdgtp_oid, " _
                                & "  bdgtp_dom_id, " _
                                & "  bdgtp_en_id, " _
                                & "  bdgtp_add_by, " _
                                & "  bdgtp_add_date, " _
                                & "  bdgtp_upd_by, " _
                                & "  bdgtp_upd_date, " _
                                & "  bdgtp_id, " _
                                & "  bdgtp_code, " _
                                & "  bdgtp_remarks, " _
                                & "  bdgtp_start_date, " _
                                & "  bdgtp_end_date, " _
                                & "  bdgtp_dt, " _
                                & "  bdgtp_year " _
                                & "FROM  " _
                                & "  public.bdgtp_periode " _
                                & " where bdgtp_year = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_year")) _
                                & " and bdgtp_id <> 0 "
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
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
        Dim ds_bantu As New DataSet

        ds_bantu = func_data.load_aprv_mstr(bdgtp_en_id.EditValue)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from bdgtp_periode where bdgtp_year = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_year"))
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _bdgtp_id As Integer
                        _bdgtp_id = SetNewID_OLD("bdgtp_periode", "bdgtp_id")
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            _bdgtp_id = _bdgtp_id + i
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.bdgtp_periode " _
                                                & "( " _
                                                & "  bdgtp_oid, " _
                                                & "  bdgtp_dom_id, " _
                                                & "  bdgtp_en_id, " _
                                                & "  bdgtp_upd_by, " _
                                                & "  bdgtp_upd_date, " _
                                                & "  bdgtp_id, " _
                                                & "  bdgtp_code, " _
                                                & "  bdgtp_remarks, " _
                                                & "  bdgtp_start_date, " _
                                                & "  bdgtp_end_date, " _
                                                & "  bdgtp_dt, " _
                                                & "  bdgtp_year " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(bdgtp_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(_bdgtp_id) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("bdgtp_code")) & ",  " _
                                                & SetSetring(ds_edit.Tables(0).Rows(i).Item("bdgtp_remarks")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("bdgtp_start_date")) & ",  " _
                                                & SetDate(ds_edit.Tables(0).Rows(i).Item("bdgtp_end_date")) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetSetring(bdgtp_year.Text) & "  " _
                                                & ");"
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        after_success()
                        'set_row(_bdgt_oid_mstr, "bdgt_oid")
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Return before_delete
    End Function

    Public Overrides Function delete_data() As Boolean
        row = BindingContext(ds.Tables(0)).Position

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
                            .Command.CommandText = "delete from bdgtp_periode where bdgtp_year = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("bdgtp_year"))
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
#End Region

End Class
