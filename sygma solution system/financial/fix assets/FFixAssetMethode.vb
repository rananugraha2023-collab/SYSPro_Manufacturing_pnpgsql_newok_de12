Imports npgsql
Imports master_new.ModFunction

Public Class FFixAssetMethode
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _po_oid_mstr As String
    Dim ds_edit As DataSet
    'Dim ds_update_related As DataSet
    Dim status_insert As Boolean = True
    Public _pod_related_oid As String = ""
    Dim _now As DateTime

#Region "Seting Awal"
    Private Sub FFixAssetMethode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
    End Sub

    Public Overrides Sub load_cb()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'asrtr_en_id.Properties.DataSource = dt_bantu
        'asrtr_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'asrtr_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'asrtr_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_fix_asset_method())
        famt_method.Properties.DataSource = dt_bantu
        famt_method.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        famt_method.Properties.ValueMember = dt_bantu.Columns("code_code").ToString
        famt_method.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "famt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "famt_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Methode", "famt_method", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Salvage", "famt_salv", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Actual", "famt_actual", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Exp. Life", "famt_exp_life", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "famt_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "famt_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "famt_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "famt_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_detail, "famtd_oid", False)
        add_column(gv_detail, "famtd_famt_oid", False)
        add_column_copy(gv_detail, "Year", "famtd_year", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Percent 1", "famtd_per_1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 2", "famtd_per_2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 3", "famtd_per_3", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 4", "famtd_per_4", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 5", "famtd_per_5", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 6", "famtd_per_6", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 7", "famtd_per_7", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 8", "famtd_per_8", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 9", "famtd_per_9", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 10", "famtd_per_10", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 11", "famtd_per_11", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "Percent 12", "famtd_per_12", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_detail, "User Create", "famtd_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Date Create", "famtd_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "User Update", "famtd_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Date Update", "famtd_upd_date", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_edit, "famtd_oid", False)
        add_column(gv_edit, "famtd_famt_oid", False)
        add_column_edit(gv_edit, "Year", "famtd_year", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_edit(gv_edit, "Percent 1", "famtd_per_1", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 2", "famtd_per_2", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 3", "famtd_per_3", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 4", "famtd_per_4", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 5", "famtd_per_5", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 6", "famtd_per_6", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 7", "famtd_per_7", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 8", "famtd_per_8", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 9", "famtd_per_9", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 10", "famtd_per_10", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 11", "famtd_per_11", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_edit(gv_edit, "Percent 12", "famtd_per_12", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  famt_oid, " _
                    & "  famt_dom_id, " _
                    & "  famt_add_by, " _
                    & "  famt_add_date, " _
                    & "  famt_upd_by, " _
                    & "  famt_upd_date, " _
                    & "  famt_id, " _
                    & "  famt_code, " _
                    & "  famt_method, " _
                    & "  famt_conv, " _
                    & "  famt_desc, " _
                    & "  famt_salv, " _
                    & "  famt_actual, " _
                    & "  famt_exp_life, " _
                    & "  famt_dt " _
                    & "FROM  " _
                    & "  public.famt_mstr "

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
            & "  famtd_oid, " _
            & "  famtd_famt_oid, " _
            & "  famtd_add_by, " _
            & "  famtd_add_date, " _
            & "  famtd_upd_by, " _
            & "  famtd_upd_date, " _
            & "  famtd_year, " _
            & "  famtd_per_1, " _
            & "  famtd_per_2, " _
            & "  famtd_per_3, " _
            & "  famtd_per_4, " _
            & "  famtd_per_5, " _
            & "  famtd_per_6, " _
            & "  famtd_per_7, " _
            & "  famtd_per_8, " _
            & "  famtd_per_9, " _
            & "  famtd_per_10, " _
            & "  famtd_per_11, " _
            & "  famtd_per_12, " _
            & "  famtd_dt " _
            & "FROM  " _
            & "  public.famtd_det "
        load_data_detail(sql, gc_detail, "detail")
    End Sub

    Public Overrides Sub relation_detail()
        Try
            gv_detail.Columns("famtd_famt_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[famtd_famt_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid").ToString & "'")
            gv_detail.BestFitColumns()

            'gv_detail.BestFitColumns()
            'gv_detail.Columns("famtd_famt_oid").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid"))
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
        famt_code.Text = ""
        famt_desc.Text = ""
        famt_method.ItemIndex = 0
        famt_salv.Checked = True
        famt_actual.Checked = False
        famt_exp_life.Text = "0"

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
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & " where famtd_add_by = '@%%$$#*'"
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

    Public Overrides Function before_save() As Boolean
        before_save = True

        gv_edit.UpdateCurrentRow()
        ds_edit.AcceptChanges()

        If ds_edit.Tables(0).Rows.Count = 0 Then
            If famt_method.GetColumnValue("code_code") = "T" Then
                MessageBox.Show("Methode Is 'Custom Table',,! Data Detail Can't Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                before_save = False
            End If
        End If

        Dim _exp_life, _line As Integer
        _exp_life = SetInteger(famt_exp_life.Text)
        _line = 0
        'If famt_method.GetColumnValue("code_code") = "T" Then
        '    For Each _dr As DataRow In ds_edit.Tables(0).Rows
        '        _line = _line + 1
        '    Next

        '    If _exp_life <> _line Then
        '        MessageBox.Show("Exp. Life Is Not Mach,,,!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '        before_save = False
        '    End If
        'End If

        'Dim _tot_pcr As Double
        'Dim _ok As Boolean
        '_ok = True
        'For Each _dr As DataRow In ds_edit.Tables(0).Rows
        '    _tot_pcr = _dr("famtd_per_1") + _dr("famtd_per_2") + _dr("famtd_per_3") _
        '    + _dr("famtd_per_4") + _dr("famtd_per_5") + _dr("famtd_per_6") _
        '    + _dr("famtd_per_7") + _dr("famtd_per_8") + _dr("famtd_per_9") _
        '    + _dr("famtd_per_10") + _dr("famtd_per_11") + _dr("famtd_per_12")
        '    If _tot_pcr <> 10 Then
        '        '_ok = False
        '        Exit For
        '    End If
        'Next

        'If _ok = False Then
        '    MessageBox.Show("Persentase Depresiasi Harus 100%,,,,!!", "Conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    before_save = False
        'End If

        Return before_save
    End Function

    Public Overrides Function insert() As Boolean
        Dim ssqls As New ArrayList
        Dim _famt_oid As Guid
        _famt_oid = Guid.NewGuid

        Dim _famt_code As String
        Dim i As Integer

        _famt_code = func_coll.get_transaction_number("FM", "10", "famt_mstr", "famt_code")

        Dim _famt_id As Integer
        _famt_id = SetNewID_OLD("famt_mstr", "famt_id")

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
                                            & "  public.famt_mstr " _
                                            & "( " _
                                            & "  famt_oid, " _
                                            & "  famt_dom_id, " _
                                            & "  famt_add_by, " _
                                            & "  famt_add_date, " _
                                            & "  famt_id, " _
                                            & "  famt_code, " _
                                            & "  famt_method, " _
                                            & "  famt_desc, " _
                                            & "  famt_salv, " _
                                            & "  famt_actual, " _
                                            & "  famt_exp_life, " _
                                            & "  famt_dt " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_famt_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "current_timestamp" & ",  " _
                                            & SetInteger(_famt_id) & ",  " _
                                            & SetSetring(famt_code.Text) & ",  " _
                                            & SetSetring(famt_method.GetColumnValue("code_code")) & ",  " _
                                            & SetSetring(famt_desc.Text) & ",  " _
                                            & SetBitYN(famt_salv.EditValue) & ",  " _
                                            & SetBitYN(famt_actual.EditValue) & ",  " _
                                            & SetDbl(famt_exp_life.EditValue) & ",  " _
                                            & "current_timestamp" & "  " _
                                            & ");"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.famtd_det " _
                                                & "( " _
                                                & "  famtd_oid, " _
                                                & "  famtd_famt_oid, " _
                                                & "  famtd_add_by, " _
                                                & "  famtd_add_date, " _
                                                & "  famtd_year, " _
                                                & "  famtd_per_1, " _
                                                & "  famtd_per_2, " _
                                                & "  famtd_per_3, " _
                                                & "  famtd_per_4, " _
                                                & "  famtd_per_5, " _
                                                & "  famtd_per_6, " _
                                                & "  famtd_per_7, " _
                                                & "  famtd_per_8, " _
                                                & "  famtd_per_9, " _
                                                & "  famtd_per_10, " _
                                                & "  famtd_per_11, " _
                                                & "  famtd_per_12, " _
                                                & "  famtd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(_famt_oid.ToString) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_year")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_1")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_2")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_3")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_4")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_5")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_6")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_7")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_8")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_9")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_10")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_11")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_12")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            '.Command.CommandText = "UPDATE ass_mstr  " _
                            '                    & "  set ass_qty_del = 1, " _
                            '                    & "  ass_its_id =  2, " _
                            '                    & "  ass_emp_id = 0, " _
                            '                    & "  ass_emp_dept = 0, " _
                            '                    & "  ass_emp_rg = 0 " _
                            '                    & "  where ass_id = " + SetInteger(ds_edit.Tables(0).Rows(i).Item("asrtrd_ass_id"))
                            '.Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

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
                        set_row(_famt_oid.ToString, "famt_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _po_oid_mstr = .Item("famt_oid").ToString
                famt_code.Text = SetString(.Item("famt_code"))
                famt_desc.Text = SetString(.Item("famt_desc"))
                famt_method.EditValue = .Item("famt_method")
                famt_salv.EditValue = SetBitYNB(.Item("famt_salv"))
                famt_actual.EditValue = SetBitYNB(.Item("famt_actual"))
                famt_exp_life.Text = .Item("famt_exp_life")
            End With
            famt_code.Focus()

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            ds_edit = New DataSet
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                            & "  famtd_oid, " _
                            & "  famtd_famt_oid, " _
                            & "  famtd_add_by, " _
                            & "  famtd_add_date, " _
                            & "  famtd_upd_by, " _
                            & "  famtd_upd_date, " _
                            & "  famtd_year, " _
                            & "  famtd_per_1, " _
                            & "  famtd_per_2, " _
                            & "  famtd_per_3, " _
                            & "  famtd_per_4, " _
                            & "  famtd_per_5, " _
                            & "  famtd_per_6, " _
                            & "  famtd_per_7, " _
                            & "  famtd_per_8, " _
                            & "  famtd_per_9, " _
                            & "  famtd_per_10, " _
                            & "  famtd_per_11, " _
                            & "  famtd_per_12, " _
                            & "  famtd_dt " _
                            & "FROM  " _
                            & "  public.famtd_det " _
                            & " where famtd_famt_oid = '" + ds.Tables(0).Rows(row).Item("famt_oid").ToString + "'"

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
        Dim _pod_qty_receive As Double = 0
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
                                            & "  public.famt_mstr   " _
                                            & "SET  " _
                                            & "  famt_dom_id = " & master_new.ClsVar.sdom_id & ",  " _
                                            & "  famt_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  famt_upd_date = current_timestamp " & ",  " _
                                            & "  famt_code = " & SetSetring(famt_code.Text) & ",  " _
                                            & "  famt_method = " & SetSetring(famt_method.GetColumnValue("code_code")) & ",  " _
                                            & "  famt_conv = 'X'" & ",  " _
                                            & "  famt_desc = " & SetSetring(famt_desc.Text) & ",  " _
                                            & "  famt_salv = " & SetBitYN(famt_salv.EditValue) & ",  " _
                                            & "  famt_actual = " & SetBitYN(famt_actual.EditValue) & ",  " _
                                            & "  famt_exp_life = " & SetDbl(famt_exp_life.Text) & ",  " _
                                            & "  famt_dt = current_timestamp " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  famt_oid = " & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid")) & "  " _
                                            & ";"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from famtd_det where famtd_famt_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()
                        '******************************************************

                        'Insert dan update data detail
                        For i = 0 To ds_edit.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.famtd_det " _
                                                & "( " _
                                                & "  famtd_oid, " _
                                                & "  famtd_famt_oid, " _
                                                & "  famtd_add_by, " _
                                                & "  famtd_add_date, " _
                                                & "  famtd_year, " _
                                                & "  famtd_per_1, " _
                                                & "  famtd_per_2, " _
                                                & "  famtd_per_3, " _
                                                & "  famtd_per_4, " _
                                                & "  famtd_per_5, " _
                                                & "  famtd_per_6, " _
                                                & "  famtd_per_7, " _
                                                & "  famtd_per_8, " _
                                                & "  famtd_per_9, " _
                                                & "  famtd_per_10, " _
                                                & "  famtd_per_11, " _
                                                & "  famtd_per_12, " _
                                                & "  famtd_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_year")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_1")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_2")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_3")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_4")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_5")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_6")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_7")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_8")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_9")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_10")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_11")) & ",  " _
                                                & SetDbl(ds_edit.Tables(0).Rows(i).Item("famtd_per_12")) & ",  " _
                                                & "current_timestamp" & "  " _
                                                & ");"
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
                        set_row(_po_oid_mstr, "famt_oid")
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
        Dim _famt_oid As String
        _famt_oid = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid").ToString

        Dim ssqls As New ArrayList

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
                            .Command.CommandText = "delete from famt_mstr where famt_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("famt_oid").ToString + "'"
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
    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
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

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv_edit.KeyDown
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
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
        'Dim _col As String = gv_edit.FocusedColumn.Name
        'Dim _row As Integer = gv_edit.FocusedRowHandle
        'Dim _asrtr_en_id As Integer = asrtr_en_id.EditValue

        'If _col = "ass_code" Then
        '    Dim frm As New FAssetSearch
        '    frm.set_win(Me)
        '    frm._row = _row
        '    frm._en_id = _asrtr_en_id
        '    frm.type_form = True
        '    frm.ShowDialog()
        'End If
    End Sub

    Private Sub gv_edit_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gv_edit.FocusedRowChanged
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(e.FocusedRowHandle, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.GotFocus
        Dim _pod_qty_receive As Double = 0

        Try
            _pod_qty_receive = ((gv_edit.GetRowCellValue(0, "pod_qty_receive")))
        Catch ex As Exception
        End Try

        If _pod_qty_receive <> 0 Then
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
        Else
            gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
        End If
    End Sub

    Private Sub gv_edit_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit.InitNewRow
        'With gv_edit
        '    '.SetRowCellValue(e.RowHandle, "pod_en_id", asrtr_en_id.EditValue)
        '    .BestFitColumns()
        'End With
    End Sub
#End Region

End Class
