Imports npgsql
Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FRelationAccToCostCnt
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
    Private Sub FRelationAccToCostCnt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        cca_en_id.Properties.DataSource = dt_bantu
        cca_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        cca_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        cca_en_id.ItemIndex = 0
    End Sub

    Private Sub bdgt_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cca_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("cc_mstr", cca_en_id.EditValue))
        cca_cc_id.Properties.DataSource = dt_bantu
        cca_cc_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        cca_cc_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        cca_cc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "cca_cc_id", False)
        add_column_copy(gv_master, "Cost Centre", "cc_desc", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail, "cca_cc_id", False)
        add_column(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_detail, "Status", "cca_status", DevExpress.Utils.HorzAlignment.Center)

        'add_column_edit(gv_edit, "Status", "cca_status", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT    " _
                    & "DISTINCT(cca_cc_id), " _
                    & "cc_desc,cca_en_id,en_desc " _
                    & "FROM    " _
                    & "public.cca_acount   " _
                    & "inner join en_mstr on en_id = cca_en_id   " _
                    & "inner join cc_mstr on cc_id = cca_cc_id " _
                    & "where cca_cc_id <> 0 " _
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
            & "  cca_oid, " _
            & "  cca_dom_id, " _
            & "  cca_en_id, " _
            & "  cca_add_by, " _
            & "  cca_add_date, " _
            & "  cca_upd_by, " _
            & "  cca_upd_date, " _
            & "  cca_cc_id, " _
            & "  cca_ac_id,ac_code,ac_name, " _
            & "  cca_remarks, " _
            & "  cca_dt,cca_status " _
            & " FROM  " _
            & "  public.cca_acount " _
            & " inner join ac_mstr on ac_id = cca_ac_id " _
            & " where cca_cc_id <> 0 and cca_status = True "
        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("cca_cc_id").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("cca_cc_id='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cca_cc_id").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.Columns("cca_cc_id").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cca_cc_id"))
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
        cca_en_id.Focus()
        cca_en_id.ItemIndex = 0
        cca_cc_id.ItemIndex = 0
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
                            & "  cca_oid, " _
                            & "  cca_dom_id, " _
                            & "  cca_en_id, " _
                            & "  cca_add_by, " _
                            & "  cca_add_date, " _
                            & "  cca_upd_by, " _
                            & "  cca_upd_date, " _
                            & "  cca_cc_id, " _
                            & "  cca_ac_id,ac_code,ac_name, " _
                            & "  cca_remarks, " _
                            & "  cca_dt,cca_status " _
                            & " FROM  " _
                            & "  public.cca_acount " _
                            & " inner join ac_mstr on ac_id = cca_ac_id " _
                            & " where cca_cc_id = -52642 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit, "edit")
                    gc_edit.DataSource = ds_edit.Tables(0)
                    gv_edit.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer
        Dim _ds_bantu As New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  ac_oid,false as ceklist, " _
                            & "  ac_dom_id, " _
                            & "  ac_add_by, " _
                            & "  ac_add_date, " _
                            & "  ac_upd_by, " _
                            & "  ac_upd_date, " _
                            & "  ac_id, " _
                            & "  ac_code, " _
                            & "  ac_name, " _
                            & "  ac_desc, " _
                            & "  ac_parent, " _
                            & "  ac_type, " _
                            & "  ac_is_sumlevel, " _
                            & "  ac_sign, " _
                            & "  ac_active, " _
                            & "  ac_dt, " _
                            & "  ac_subclass, " _
                            & "  ac_subclass_2, " _
                            & "  ac_subclass_3, " _
                            & "  ac_cu_id, " _
                            & "  ac_cash_flow " _
                            & "FROM  " _
                            & "  public.ac_mstr " _
                            & " where ac_active ~~* 'Y' and ac_id <> 0 and ac_is_sumlevel ~~* 'N'" _
                            & " order by ac_code asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_bantu, "bantu")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim _dtrow As DataRow
        For i = 0 To _ds_bantu.Tables(0).Rows.Count - 1
            _dtrow = ds_edit.Tables("edit").NewRow
            _dtrow("cca_ac_id") = _ds_bantu.Tables(0).Rows(i).Item("ac_id")
            _dtrow("ac_code") = _ds_bantu.Tables(0).Rows(i).Item("ac_code")
            _dtrow("ac_name") = _ds_bantu.Tables(0).Rows(i).Item("ac_name")
            _dtrow("cca_remarks") = ""
            _dtrow("cca_status") = False
            ds_edit.Tables("edit").Rows.Add(_dtrow)
        Next
        _ds_bantu.Tables(0).AcceptChanges()
        gv_edit.BestFitColumns()

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

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.cca_acount " _
                                                & "( " _
                                                & "  cca_oid, " _
                                                & "  cca_dom_id, " _
                                                & "  cca_en_id, " _
                                                & "  cca_add_by, " _
                                                & "  cca_add_date, " _
                                                & "  cca_cc_id, " _
                                                & "  cca_ac_id, " _
                                                & "  cca_remarks, " _
                                                & "  cca_dt, " _
                                                & "  cca_status " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(cca_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(cca_cc_id.EditValue) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("cca_ac_id")) & ",  " _
                                                & SetSetring(cca_remarks.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("cca_status") & "  " _
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
                cca_en_id.EditValue = .Item("cca_en_id")
                cca_cc_id.EditValue = .Item("cca_cc_id")
            End With
            cca_en_id.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  cca_oid, " _
                            & "  cca_dom_id, " _
                            & "  cca_en_id, " _
                            & "  cca_add_by, " _
                            & "  cca_add_date, " _
                            & "  cca_upd_by, " _
                            & "  cca_upd_date, " _
                            & "  cca_cc_id, " _
                            & "  cca_ac_id,ac_code,ac_name, " _
                            & "  cca_remarks, " _
                            & "  cca_dt,cca_status " _
                            & " FROM  " _
                            & "  public.cca_acount " _
                            & " inner join ac_mstr on ac_id = cca_ac_id " _
                            & " where cca_cc_id = " + SetInteger(cca_cc_id.EditValue)
                        .InitializeCommand()
                        .FillDataSet(ds_edit, "detail")
                        gc_edit.DataSource = ds_edit.Tables(0)
                        gv_edit.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            With ds_edit.Tables("detail").Rows(0)
                cca_remarks.Text = SetString(.Item("cca_remarks"))
            End With

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        Dim i As Integer
        Dim ds_bantu As New DataSet

        ds_bantu = func_data.load_aprv_mstr(cca_en_id.EditValue)

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cca_acount where cca_cc_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cca_cc_id"))
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.cca_acount " _
                                                & "( " _
                                                & "  cca_oid, " _
                                                & "  cca_dom_id, " _
                                                & "  cca_en_id, " _
                                                & "  cca_upd_by, " _
                                                & "  cca_upd_date, " _
                                                & "  cca_cc_id, " _
                                                & "  cca_ac_id, " _
                                                & "  cca_remarks, " _
                                                & "  cca_dt, " _
                                                & "  cca_status " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(cca_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(cca_cc_id.EditValue) & ",  " _
                                                & SetInteger(ds_edit.Tables(0).Rows(i).Item("cca_ac_id")) & ",  " _
                                                & SetSetring(cca_remarks.Text) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & ds_edit.Tables(0).Rows(i).Item("cca_status") & "  " _
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
                            .Command.CommandText = "delete from cca_acount where cca_cc_id = " + SetInteger(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cca_cc_id"))
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
