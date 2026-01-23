Imports npgsql
Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FPreOrder
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _ass_oid_mstr As String
    Dim ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime

#Region "Seting Awal"
    Private Sub FAssetBeginingBalance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        preorder_en_id.Properties.DataSource = dt_bantu
        preorder_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        preorder_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        preorder_en_id.ItemIndex = 0

     
    End Sub

    Private Sub ass_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles preorder_en_id.EditValueChanged
       
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Preorder Date", "preorder_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Preorder Number", "preorder_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "preorder_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty SQ", "preorder_qty_sq", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty SO", "preorder_qty_so", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Remarks", "preorder_remarks", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Active", "preorder_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "preorder_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "preorder_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "preorder_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "preorder_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

    End Sub

    Public Overrides Function get_sequel() As String


        get_sequel = "SELECT  " _
                & "  a.preorder_oid,preorder_code, " _
                & "  a.preorder_date, " _
                & "  a.preorder_en_id, " _
                & "  b.en_code, " _
                & "  b.en_desc, " _
                & "  a.preorder_dom_id, " _
                & "  a.preorder_pt_id, " _
                & "  c.pt_code, " _
                & "  c.pt_desc1, " _
                & "  c.pt_desc2, " _
                & "  a.preorder_qty, " _
                & "  a.preorder_qty_sq, " _
                & "  a.preorder_qty_so, " _
                & "  a.preorder_active, " _
                & "  a.preorder_remarks, " _
                & "  a.preorder_add_by, " _
                & "  a.preorder_add_date, " _
                & "  a.preorder_upd_by, " _
                & "  a.preorder_upd_date " _
                & "FROM " _
                & "  public.preorder_mstr a " _
                & "  INNER JOIN public.en_mstr b ON (a.preorder_en_id = b.en_id) " _
                & "  INNER JOIN public.pt_mstr c ON (a.preorder_pt_id = c.pt_id) " _
                & "WHERE " _
                & "  a.preorder_date BETWEEN " & SetDateNTime00(pr_txttglawal.DateTime) & " AND " & SetDateNTime00(pr_txttglakhir.DateTime) & " " _
                & "ORDER BY " _
                & "  a.preorder_date"


        Return get_sequel
    End Function

    'Public Overrides Sub load_data_grid_detail()
    '    Dim _qty_del As Integer
    '    If ce_retirement.Checked = True Then
    '        _qty_del = 1
    '    Else
    '        _qty_del = 0
    '    End If


    '    If ds.Tables(0).Rows.Count = 0 Then
    '        Exit Sub
    '    End If

    '    Dim sql As String
    '    Try
    '        ds.Tables("detail").Clear()
    '    Catch ex As Exception
    '    End Try

    '    sql = "SELECT  " _
    '        & "  assbk_oid, " _
    '        & "  assbk_ass_oid, " _
    '        & "  assbk_fabk_id,fabk_code, " _
    '        & "  assbk_famt_id,famt_oid,famt_code,famt_method, " _
    '        & "  assbk_exp_life, " _
    '        & "  assbk_cost, " _
    '        & "  assbk_depr_acum, " _
    '        & "  assbk_per_year, " _
    '        & "  assbk_per_month, " _
    '        & "  assbk_dt " _
    '        & "FROM  " _
    '        & "  public.assbk_mstr " _
    '        & "  inner join fabk_mstr on fabk_id = assbk_fabk_id " _
    '        & "  inner join famt_mstr on famt_id = assbk_famt_id " _
    '        & "  inner join ass_mstr on ass_oid = assbk_ass_oid " _
    '        & "  where ass_qty_del = " + SetInteger(_qty_del)
    '    load_data_detail(sql, gc_detail, "detail")
    'End Sub

    'Public Overrides Sub relation_detail()
    '    Try
    '        gv_detail.Columns("assbk_ass_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid"))
    '        gv_detail.BestFitColumns()
    '    Catch ex As Exception
    '    End Try
    'End Sub

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
        preorder_en_id.ItemIndex = 0
        preorder_en_id.Focus()
        preorder_pt_id.Tag = -1
        preorder_pt_id.EditValue = ""
        preorder_date.DateTime = CekTanggal()
        pt_desc.Text = ""
        preorder_qty.EditValue = 0.0
        preorder_remarks.Text = ""
        preorder_active.Checked = True

        'Try
        '    tcg_header.SelectedTabPageIndex = 0
        'Catch ex As Exception
        'End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        'ds_edit = New DataSet
        'Try
        '    Using objcb As New master_new.WDABasepgsql("", "")
        '        With objcb
        '            .SQL = "SELECT  " _
        '                    & "  assbk_oid, " _
        '                    & "  assbk_ass_oid, " _
        '                    & "  assbk_fabk_id,fabk_code, " _
        '                    & "  assbk_famt_id,famt_oid,famt_code,famt_method, " _
        '                    & "  assbk_exp_life, " _
        '                    & "  assbk_cost, " _
        '                    & "  assbk_depr_acum, " _
        '                    & "  assbk_per_year, " _
        '                    & "  assbk_per_month, " _
        '                    & "  assbk_dt " _
        '                    & "FROM  " _
        '                    & "  public.assbk_mstr " _
        '                    & "  inner join fabk_mstr on fabk_id = assbk_fabk_id " _
        '                    & "  inner join famt_mstr on famt_id = assbk_famt_id " _
        '                    & "  inner join ass_mstr on ass_oid = assbk_ass_oid " _
        '                    & "  where assbk_fabk_id = -427642 "
        '            .InitializeCommand()
        '            .FillDataSet(ds_edit, "insert_edit")
        '            gc_edit.DataSource = ds_edit.Tables(0)
        '            gv_edit.BestFitColumns()
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        'gv_edit.UpdateCurrentRow()
        'ds_edit.AcceptChanges()

        If preorder_pt_id.Tag = -1 Then
            Box("Partnumber can't blank")
            Return False
            Exit Function
        End If

        If preorder_qty.EditValue = 0 Then
            Box("Qty can't blank")
            Return False
            Exit Function
        End If


        Return before_save
    End Function

  

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList

        'Dim _ass_oid As Guid
        '_ass_oid = Guid.NewGuid
        _ass_oid_mstr = Guid.NewGuid.ToString
        Dim _ass_code As String
        _ass_code = master_new.PGSqlConn.GetNewNumberYM("preorder_mstr", "preorder_code", 3, "PRE-" & CekTanggal.ToString("yyMM"), True) 'func_coll.get_ass_code(preorder_pt_id.Text, "A", "ass_mstr", "ass_code", 1)

        ''Dim i As Integer
        'Dim _ass_id As Integer
        '_ass_id = SetNewID_OLD("ass_mstr", "ass_id")

        'Dim _line As Integer
        '_line = SetNewLine(preorder_pt_id.EditValue)

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
                                        & "  public.preorder_mstr " _
                                        & "( " _
                                        & "  preorder_oid,preorder_code, " _
                                        & "  preorder_date, " _
                                        & "  preorder_en_id, " _
                                        & "  preorder_dom_id, " _
                                        & "  preorder_pt_id, " _
                                        & "  preorder_qty, " _
                                        & "  preorder_qty_sq, " _
                                        & "  preorder_qty_so, " _
                                        & "  preorder_active, " _
                                        & "  preorder_remarks, " _
                                        & "  preorder_add_by, " _
                                        & "  preorder_add_date " _
                                        & ") " _
                                        & "VALUES ( " _
                                        & SetSetring(_ass_oid_mstr) & ",  " _
                                        & SetSetring(_ass_code) & "," _
                                        & SetDateNTime00(preorder_date.EditValue) & ",  " _
                                        & SetInteger(preorder_en_id.EditValue) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(preorder_pt_id.Tag) & ",  " _
                                        & SetDec(preorder_qty.EditValue) & ",  " _
                                        & SetDec(0) & ",  " _
                                        & SetDec(0) & ",  " _
                                        & SetBitYN(preorder_active.EditValue) & ",  " _
                                        & SetSetring(preorder_remarks.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDateNTime(CekTanggal) & "  " _
                                        & ")"


                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.assbk_mstr " _
                        '                        & "( " _
                        '                        & "  assbk_oid, " _
                        '                        & "  assbk_ass_oid, " _
                        '                        & "  assbk_fabk_id, " _
                        '                        & "  assbk_famt_id, " _
                        '                        & "  assbk_exp_life, " _
                        '                        & "  assbk_cost, " _
                        '                        & "  assbk_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(_ass_oid.ToString) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_fabk_id")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_famt_id")) & ",  " _
                        '                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("assbk_exp_life")) & ",  " _
                        '                        & SetDbl(ass_service_cost.EditValue) & ",  " _
                        '                        & "current_timestamp" & "  " _
                        '                        & ");"

                        'ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.PGSqlConn.FinsertSQL2Array(ssqls)
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                            Next
                        End If

                        sqlTran.Commit()

                        'pr_filter.Text = _ass_code
                        after_success()
                        set_row(_ass_oid_mstr.ToString, "preorder_oid")
                        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ass_oid_mstr = .Item("preorder_oid").ToString
                preorder_en_id.EditValue = .Item("preorder_en_id")
                preorder_pt_id.Tag = .Item("preorder_pt_id")
                preorder_pt_id.EditValue = .Item("pt_code")
                pt_desc.EditValue = SetString(.Item("pt_desc1"))
                preorder_date.EditValue = .Item("preorder_date")
                preorder_qty.EditValue = .Item("preorder_qty")
                preorder_remarks.EditValue = SetString(.Item("preorder_remarks"))
                preorder_active.EditValue = SetBitYNB(.Item("preorder_active"))

            End With
            preorder_en_id.Focus()

            'Try
            '    tcg_header.SelectedTabPageIndex = 0
            'Catch ex As Exception
            'End Try

            'ds_edit = New DataSet
            'Try
            '    Using objcb As New master_new.WDABasepgsql("", "")
            '        With objcb
            '            .SQL = "SELECT  " _
            '                & "  assbk_oid, " _
            '                & "  assbk_ass_oid, " _
            '                & "  assbk_fabk_id,fabk_code, " _
            '                & "  assbk_famt_id,famt_oid,famt_code,famt_method, " _
            '                & "  assbk_exp_life, " _
            '                & "  assbk_cost, " _
            '                & "  assbk_depr_acum, " _
            '                & "  assbk_per_year, " _
            '                & "  assbk_per_month, " _
            '                & "  assbk_dt " _
            '                & "FROM  " _
            '                & "  public.assbk_mstr " _
            '                & "  inner join fabk_mstr on fabk_id = assbk_fabk_id " _
            '                & "  inner join famt_mstr on famt_id = assbk_famt_id " _
            '                & "  inner join ass_mstr on ass_oid = assbk_ass_oid " _
            '                & "  where assbk_ass_oid = '" + ds.Tables(0).Rows(row).Item("ass_oid").ToString + "'"
            '            .InitializeCommand()
            '            .FillDataSet(ds_edit, "detail")
            '            gc_edit.DataSource = ds_edit.Tables(0)
            '            gv_edit.BestFitColumns()
            '        End With
            '    End Using
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message)
            'End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True
        'Dim i As Integer
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
                                    & "  public.preorder_mstr  " _
                                    & "SET  " _
                                    & "  preorder_date = " & SetDateNTime00(preorder_date.EditValue) & ",  " _
                                    & "  preorder_en_id = " & SetInteger(preorder_en_id.EditValue) & ",  " _
                                    & "  preorder_dom_id = " & SetInteger(1) & ",  " _
                                    & "  preorder_pt_id = " & SetInteger(preorder_pt_id.Tag) & ",  " _
                                    & "  preorder_qty = " & SetDec(preorder_qty.EditValue) & ",  " _
                                    & "  preorder_active = " & SetBitYN(preorder_active.EditValue) & ",  " _
                                    & "  preorder_remarks = " & SetSetring(preorder_remarks.EditValue) & ",  " _
                                    & "  preorder_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  preorder_upd_date = " & SetDateNTime(CekTanggal) & "  " _
                                    & "WHERE  " _
                                    & "  preorder_oid = " & SetSetring(_ass_oid_mstr) & " "

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "delete from assbk_mstr where assbk_ass_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid"))
                        'ssqls.Add(.Command.CommandText)
                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()
                        '******************************************************

                        ''Insert dan update data detail
                        'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                        '    .Command.CommandType = CommandType.Text
                        '    .Command.CommandText = "INSERT INTO  " _
                        '                        & "  public.assbk_mstr " _
                        '                        & "( " _
                        '                        & "  assbk_oid, " _
                        '                        & "  assbk_ass_oid, " _
                        '                        & "  assbk_fabk_id, " _
                        '                        & "  assbk_famt_id, " _
                        '                        & "  assbk_exp_life, " _
                        '                        & "  assbk_cost, " _
                        '                        & "  assbk_dt " _
                        '                        & ")  " _
                        '                        & "VALUES ( " _
                        '                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                        '                        & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ass_oid")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_fabk_id")) & ",  " _
                        '                        & SetInteger(ds_edit.Tables(0).Rows(i).Item("assbk_famt_id")) & ",  " _
                        '                        & SetDbl(ds_edit.Tables(0).Rows(i).Item("assbk_exp_life")) & ",  " _
                        '                        & SetDbl(ass_service_cost.EditValue) & ",  " _
                        '                        & "current_timestamp" & "  " _
                        '                        & ");"
                        'ssqls.Add(.Command.CommandText)
                        '    .Command.ExecuteNonQuery()
                        '    .Command.Parameters.Clear()
                        'Next

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
                        set_row(_ass_oid_mstr, "preorder_oid")
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

    Public Overrides Function before_delete() As Boolean
        before_delete = True

        Return before_delete
    End Function

    Public Overrides Function delete_data() As Boolean
        Dim ssqls As New ArrayList

        If SetNumber(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("preorder_qty_sq")) > 0 Then
            Box("Can't delete data")
            Exit Function
        End If

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

        'Dim i As Integer
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
                            .Command.CommandText = "delete from preorder_mstr where preorder_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("preorder_oid") + "'"
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
#End Region

#Region "gv_edit"
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        'Dim _pod_qty, _pod_qty_real, _pod_um_conv, _pod_qty_cost, _pod_cost, _pod_disc, _pod_qty_receive As Double
        '_pod_um_conv = 1
        '_pod_qty = 1
        '_pod_cost = 0
        '_pod_disc = 0

        'If e.Column.Name = "pod_qty" Then
        '    '********* Cek Qty Processed
        '    Try
        '        _pod_qty_receive = (gv_edit.GetRowCellValue(e.RowHandle, "pod_qty_receive"))
        '    Catch ex As Exception
        '    End Try

        '    If e.Value < _pod_qty_receive Then
        '        MessageBox.Show("Qty PO Can't Lower Than Qty Receive..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        gv_edit.CancelUpdateCurrentRow()
        '        Exit Sub
        '    End If
        '    '********************************

        '    Try
        '        _pod_um_conv = (gv_edit.GetRowCellValue(e.RowHandle, "pod_um_conv"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _pod_cost = (gv_edit.GetRowCellValue(e.RowHandle, "pod_cost"))
        '    Catch ex As Exception
        '    End Try

        '    Try
        '        _pod_disc = (gv_edit.GetRowCellValue(e.RowHandle, "pod_disc"))
        '    Catch ex As Exception
        '    End Try

        '    _pod_qty_real = e.Value * _pod_um_conv
        '    _pod_qty_cost = (e.Value * _pod_cost) - (e.Value * _pod_cost * _pod_disc)

        '    gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_real", _pod_qty_real)
        '    gv_edit.SetRowCellValue(e.RowHandle, "pod_qty_cost", _pod_qty_cost)
        'End If
    End Sub

    'Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    If e.Control And e.KeyCode = Keys.I Then
    '        gv_edit.AddNewRow()
    '    ElseIf e.Control And e.KeyCode = Keys.D Then
    '        gv_edit.DeleteSelectedRows()
    '    End If
    'End Sub

    'Private Sub gv_edit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
    '        browse_data()
    '    End If
    'End Sub

    'Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    browse_data()
    'End Sub

    'Private Sub browse_data()
    '    Dim _col As String = gv_edit.FocusedColumn.Name
    '    Dim _row As Integer = gv_edit.FocusedRowHandle

    '    If _col = "fabk_code" Then
    '        Dim frm As New FFixAssetBookSearch
    '        frm.set_win(Me)
    '        frm._row = _row
    '        frm.type_form = True
    '        frm.ShowDialog()
    '    ElseIf _col = "famt_code" Then
    '        Dim frm As New FFixAssetMethodeSearch
    '        frm.set_win(Me)
    '        frm._row = _row
    '        frm.type_form = True
    '        frm.ShowDialog()
    '    End If
    'End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs)
        'With gv_edit
        '    '.SetRowCellValue(e.RowHandle, "pod_en_id", asrtr_en_id.EditValue)
        '    .BestFitColumns()
        'End With
    End Sub


#End Region

    Private Sub preorder_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles preorder_pt_id.ButtonClick
        Try
            Dim frm As New FSearchPartNumberSearch
            frm.set_win(Me)
            frm._en_id = preorder_en_id.EditValue
            frm._obj = preorder_pt_id
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


   
End Class
