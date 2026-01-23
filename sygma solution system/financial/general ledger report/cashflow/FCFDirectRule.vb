Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FCFDirectRule
    Dim ssql As String
    Dim _cfdrule_oid_edit As String
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection

    Dim _conf_value As String
    Public ds_edit As DataSet
    Public dt_edit_detail As DataTable
    Public dt_edit_sum As DataTable

    Private Sub FCFDirectRule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(1, "cf_direct_group"))
        cfdrule_group_id.Properties.DataSource = dt_bantu
        cfdrule_group_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        cfdrule_group_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        cfdrule_group_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(1, "cf_direct_line"))
        cfdrule_line_id.Properties.DataSource = dt_bantu
        cfdrule_line_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        cfdrule_line_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        cfdrule_line_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "cfdrule_oid", False)
        'add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Sequence", "cfdrule_seq", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sum Group", "sum_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Sum of Rule", "cfdrule_is_sum", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "cfdrule_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "cfdrule_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "cfdrule_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "User Update", "cfdrule_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "cfdrule_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")

        add_column(gv_detail, "cfdruled_oid", False)
        add_column(gv_detail, "cfdruled_cfdrule_oid", False)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Sign", "cfdruled_sign", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_sum, "cfdrules_oid", False)
        add_column(gv_sum, "cfdrules_cfdrule_oid", False)
        add_column_copy(gv_sum, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Sum Group", "sum_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_sum, "Cashflow Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit, "cfdruled_oid", False)
        add_column(gv_edit, "cfdruled_ac_id", False)
        add_column(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Account Sign", "cfdruled_sign", DevExpress.Utils.HorzAlignment.Center)

        add_column(gv_sum_edit, "cfdrules_oid", False)
        add_column(gv_sum_edit, "cfdrules_cfdrule_oid", False)
        add_column(gv_sum_edit, "cfdrules_ref_oid", False)
        add_column(gv_sum_edit, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_sum_edit, "Cashflow Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
    End Sub


    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  cfdrule_oid, " _
                & "  cfdrule_dom_id, " _
                & "  cfdrule_en_id, " _
                & "  cfdrule_add_by, " _
                & "  cfdrule_add_date, " _
                & "  cfdrule_upd_by, " _
                & "  cfdrule_upd_date, " _
                & "  cfdrule_group_id, " _
                & "  coalesce(cfdrule_is_sum,'N') as cfdrule_is_sum, " _
                & "  cfdrule_line_id, " _
                & "  cfdrule_dt, " _
                & "  coalesce(cfdrule_seq,0) as cfdrule_seq, " _
                & "  cfdrule_remarks, " _
                & "  en_desc, " _
                & "  group_mstr.code_name as group_name, " _
                & "  tranline_mstr.code_name as tranline_name " _
                & "FROM  " _
                & "  public.cfdrule_mstr" _
                & "  INNER JOIN public.en_mstr ON (public.cfdrule_mstr.cfdrule_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
                & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
                & "  order by cfdrule_seq "
                

        Return get_sequel
    End Function


    Public Overrides Sub load_data_grid_detail()
        Dim _get_oid As String = ""
        'Dim i As Integer

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cfdruled_oid, " _
            & "  cfdruled_cfdrule_oid, " _
            & "  cfdruled_seq, " _
            & "  cfdruled_ac_id, " _
            & "  ac_mstr.ac_code as ac_code, " _
            & "  ac_mstr.ac_name as ac_name, " _
            & "  cfdruled_sign " _
            & "FROM  " _
            & "  public.cfdruled_det " _
            & "  INNER JOIN ac_mstr ON (cfdruled_ac_id = ac_mstr.ac_id) "

        load_data_detail(sql, gc_detail, "detail")

        Try
            ds.Tables("detail_sum").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  cfdrules_oid, " _
            & "  cfdrules_cfdrule_oid, " _
            & "  cfdrules_seq, " _
            & "  cfdrules_ref_oid, " _
            & "  group_mstr.code_name as group_name, " _
            & "  tranline_mstr.code_name as tranline_name " _
            & "FROM  " _
            & "  public.cfdrules_sum" _
            & "  INNER JOIN cfdrule_mstr ON (cfdrule_mstr.cfdrule_oid = cfdrules_sum.cfdrules_ref_oid)" _
            & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
            & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id "


        load_data_detail(sql, gc_sum, "detail_sum")

    End Sub

    Public Overrides Sub insert_data_awal()

        _cfdrule_oid_edit = ""
        'cfdrule_en_id.ItemIndex = 0
        cfdrule_seq.EditValue = 0
        cfdrule_group_id.ItemIndex = 0
        cfdrule_is_sum.Checked = False
        cfdrule_line_id.ItemIndex = 0
        cfdrule_remarks.Text = ""

        ssql = "SELECT  " _
            & "  cfdruled_oid, " _
            & "  cfdruled_cfdrule_oid, " _
            & "  cfdruled_seq, " _
            & "  cfdruled_ac_id, " _
            & "  ac_mstr.ac_code as ac_code, " _
            & "  ac_mstr.ac_name as ac_name, " _
            & "  cfdruled_sign " _
            & "FROM  " _
            & "  public.cfdruled_det " _
            & "  INNER JOIN ac_mstr ON (cfdruled_ac_id = ac_mstr.ac_id) " _
            & "WHERE " _
            & "  cfdruled_cfdrule_oid is null"

        dt_edit_detail = GetTableData(ssql)
        gc_edit.DataSource = dt_edit_detail
        gv_edit.BestFitColumns()

        ssql = "SELECT  " _
            & "  cfdrules_oid, " _
            & "  cfdrules_cfdrule_oid, " _
            & "  cfdrules_seq, " _
            & "  cfdrules_ref_oid, " _
            & "  group_mstr.code_name as group_name, " _
            & "  tranline_mstr.code_name as tranline_name " _
            & "FROM  " _
            & "  public.cfdrules_sum" _
            & "  INNER JOIN cfdrule_mstr ON (cfdrule_mstr.cfdrule_oid = cfdrules_sum.cfdrules_ref_oid)" _
            & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
            & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
            & "WHERE " _
            & "  cfdrules_cfdrule_oid is null"

        dt_edit_sum = GetTableData(ssql)
        gc_sum_edit.DataSource = dt_edit_sum
        gv_sum_edit.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            tcg_edit.SelectedTabPageIndex = 0
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
        Catch ex As Exception
        End Try
    End Sub


    Public Overrides Function insert() As Boolean
        Dim _cfdrule_oid As String = Guid.NewGuid.ToString
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
                                        & "  public.cfdrule_mstr " _
                                        & "( " _
                                        & "  cfdrule_oid, " _
                                        & "  cfdrule_dom_id, " _
                                        & "  cfdrule_en_id, " _
                                        & "  cfdrule_seq, " _
                                        & "  cfdrule_add_by, " _
                                        & "  cfdrule_add_date, " _
                                        & "  cfdrule_group_id, " _
                                        & "  cfdrule_is_sum, " _
                                        & "  cfdrule_line_id, " _
                                        & "  cfdrule_dt, " _
                                        & "  cfdrule_remarks " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_cfdrule_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(1) & ",  " _
                                        & SetInteger(cfdrule_seq.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetInteger(cfdrule_group_id.EditValue) & ",  " _
                                        & SetBitYN(cfdrule_is_sum.EditValue) & ",  " _
                                        & SetInteger(cfdrule_line_id.EditValue) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetSetring(cfdrule_remarks.Text) & "  " _
                                        & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0

                        If cfdrule_is_sum.Checked = True Then
                            For Each dr As DataRow In dt_edit_sum.Rows
                                ssql = "INSERT INTO  " _
                                        & "  public.cfdrules_sum " _
                                        & "( " _
                                        & "  cfdrules_oid, " _
                                        & "  cfdrules_cfdrule_oid, " _
                                        & "  cfdrules_seq, " _
                                        & "  cfdrules_ref_oid " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_cfdrule_oid) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetSetring(dr("cfdrules_ref_oid")) & "  " _
                                        & ");"
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = ssql
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = _seq + 1
                            Next
                        Else
                            For Each dr As DataRow In dt_edit_detail.Rows
                                ssql = "INSERT INTO  " _
                                        & "  public.cfdruled_det " _
                                        & "( " _
                                        & "  cfdruled_oid, " _
                                        & "  cfdruled_cfdrule_oid, " _
                                        & "  cfdruled_seq, " _
                                        & "  cfdruled_ac_id, " _
                                        & "  cfdruled_sign " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_cfdrule_oid) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(dr("cfdruled_ac_id")) & ",  " _
                                        & SetSetring(dr("cfdruled_sign")) & "  " _
                                        & ");"
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = ssql
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = _seq + 1
                            Next
                        End If
                        
                        insert = True
                        sqlTran.Commit()

                        after_success()
                        set_row(_cfdrule_oid, "cfdrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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


    Public Overrides Sub relation_detail()
        Try

            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfdrule_is_sum").ToString = "N" Then
                xtc_detail.SelectedTabPageIndex = 0
            ElseIf ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfdrule_is_sum").ToString = "Y" Then
                xtc_detail.SelectedTabPageIndex = 1
            End If

            gv_detail.Columns("cfdruled_cfdrule_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cfdruled_cfdrule_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfdrule_oid").ToString & "'")
            gv_detail.BestFitColumns()

            gv_sum.Columns("cfdrules_cfdrule_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[cfdrules_cfdrule_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cfdrule_oid").ToString & "'")
            gv_sum.BestFitColumns()

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            cfdrule_group_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _cfdrule_oid_edit = .Item("cfdrule_oid").ToString
                'cfdrule_en_id.EditValue = .Item("cfdrule_en_id")
                cfdrule_seq.EditValue = .Item("cfdrule_seq")
                cfdrule_group_id.EditValue = .Item("cfdrule_group_id")
                cfdrule_is_sum.EditValue = SetBitYNB(.Item("cfdrule_is_sum"))
                cfdrule_line_id.EditValue = .Item("cfdrule_line_id")
                cfdrule_remarks.Text = SetString(.Item("cfdrule_remarks"))

            End With

            If ds.Tables(0).Rows(row).Item("cfdrule_is_sum") = "N" Then
                ssql = "SELECT  " _
                    & "  cfdruled_oid, " _
                    & "  cfdruled_cfdrule_oid, " _
                    & "  cfdruled_seq, " _
                    & "  cfdruled_ac_id, " _
                    & "  ac_mstr.ac_code as ac_code, " _
                    & "  ac_mstr.ac_name as ac_name, " _
                    & "  cfdruled_sign " _
                    & "FROM  " _
                    & "  public.cfdruled_det " _
                    & "  INNER JOIN ac_mstr ON (cfdruled_ac_id = ac_mstr.ac_id) " _
                    & "WHERE " _
                    & "  cfdruled_cfdrule_oid =" & SetSetring(_cfdrule_oid_edit)

                dt_edit_detail = GetTableData(ssql)
                gc_edit.DataSource = dt_edit_detail
                gv_edit.BestFitColumns()
            Else
                ssql = "SELECT  " _
                    & "  cfdrules_oid, " _
                    & "  cfdrules_cfdrule_oid, " _
                    & "  cfdrules_seq, " _
                    & "  cfdrules_ref_oid, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  tranline_mstr.code_name as tranline_name " _
                    & "FROM  " _
                    & "  public.cfdrules_sum" _
                    & "  INNER JOIN cfdrule_mstr ON (cfdrule_mstr.cfdrule_oid = cfdrules_sum.cfdrules_ref_oid)" _
                    & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
                    & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
                    & "WHERE " _
                    & "  cfdrules_cfdrule_oid =" & SetSetring(_cfdrule_oid_edit)

                dt_edit_sum = GetTableData(ssql)
                gc_sum_edit.DataSource = dt_edit_sum
                gv_sum_edit.BestFitColumns()

            End If

            Try
                tcg_header.SelectedTabPageIndex = 0
                DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
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
                        .Command.CommandText = "UPDATE  " _
                                            & "  public.cfdrule_mstr   " _
                                            & "SET  " _
                                            & "  cfdrule_en_id = 1" & ",  " _
                                            & "  cfdrule_seq = " & SetInteger(cfdrule_seq.EditValue) & ",  " _
                                            & "  cfdrule_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  cfdrule_upd_date = " & SetDate(CekTanggal) & ",  " _
                                            & "  cfdrule_group_id = " & SetInteger(cfdrule_group_id.EditValue) & ",  " _
                                            & "  cfdrule_is_sum = " & SetBitYN(cfdrule_is_sum.EditValue) & ",  " _
                                            & "  cfdrule_line_id = " & SetInteger(cfdrule_line_id.EditValue) & ",  " _
                                            & "  cfdrule_dt = " & SetDate(CekTanggal) & ",  " _
                                            & "  cfdrule_remarks = " & SetSetring(cfdrule_remarks.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  cfdrule_oid = " & SetSetring(_cfdrule_oid_edit) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cfdruled_det where cfdruled_cfdrule_oid = " & SetSetring(_cfdrule_oid_edit)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from cfdrules_sum where cfdrules_cfdrule_oid = " & SetSetring(_cfdrule_oid_edit)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0
                        If cfdrule_is_sum.Checked = True Then
                            For Each dr As DataRow In dt_edit_sum.Rows
                                ssql = "INSERT INTO  " _
                                        & "  public.cfdrules_sum " _
                                        & "( " _
                                        & "  cfdrules_oid, " _
                                        & "  cfdrules_cfdrule_oid, " _
                                        & "  cfdrules_seq, " _
                                        & "  cfdrules_ref_oid " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_cfdrule_oid_edit) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetSetring(dr("cfdrules_ref_oid")) & "  " _
                                        & ");"
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = ssql
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = _seq + 1
                            Next
                            
                        Else
                            For Each dr As DataRow In dt_edit_detail.Rows
                                ssql = "INSERT INTO  " _
                                        & "  public.cfdruled_det " _
                                        & "( " _
                                        & "  cfdruled_oid, " _
                                        & "  cfdruled_cfdrule_oid, " _
                                        & "  cfdruled_seq, " _
                                        & "  cfdruled_ac_id, " _
                                        & "  cfdruled_sign " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(_cfdrule_oid_edit) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(dr("cfdruled_ac_id")) & ",  " _
                                        & SetSetring(dr("cfdruled_sign")) & "  " _
                                        & ");"
                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = ssql
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()
                                _seq = _seq + 1
                            Next
                        End If
                        

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
                        set_row(Trim(_cfdrule_oid_edit.ToString), "cfdrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then

            row = BindingContext(ds.Tables(0)).Position

            'If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
            '    row = row - 1
            'ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
            '    row = 0
            'End If

            Try
                Using objinsert As New master_new.WDABasepgsql("", "")
                    With objinsert
                        .Connection.Open()
                        Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            .Command = .Connection.CreateCommand
                            .Command.Transaction = sqlTran

                            ssql = "delete from cfdruled_det where cfdruled_cfdrule_oid = '" + ds.Tables(0).Rows(row).Item("cfdrule_oid").ToString + "'"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            ssql = "delete from cfdrules_sum where cfdrules_cfdrule_oid = '" + ds.Tables(0).Rows(row).Item("cfdrule_oid").ToString + "'"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            ssql = "delete from cfdrule_mstr where cfdrule_oid = '" + ds.Tables(0).Rows(row).Item("cfdrule_oid").ToString + "'"
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
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        If cfdrule_is_sum.Checked = False Then
            gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit_detail.AcceptChanges()
        Else
            gc_sum_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
            dt_edit_sum.AcceptChanges()
        End If

        If cfdrule_is_sum.Checked = False Then
            If (dt_edit_detail.Rows.Count < 1) Then
                MsgBox("Detail Can't Empty...", MsgBoxStyle.Critical, "Unable to Save")
                before_save = False
            End If
        Else
            If (dt_edit_sum.Rows.Count < 1) Then
                MsgBox("Detail Can't Empty...", MsgBoxStyle.Critical, "Unable to Save")
                before_save = False
            End If
        End If

        Return before_save
    End Function

    Private Sub gv_edit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Control And e.KeyCode = Keys.I Then
            gv_edit.AddNewRow()
        ElseIf e.Control And e.KeyCode = Keys.D Then
            gv_edit.DeleteSelectedRows()
        End If
    End Sub

    Private Sub btn_gen_rule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_gen_rule.Click

        Dim _dtrow As DataRow
        Dim _avail_code As Boolean


        For _code As Integer = te_ac_from.EditValue To te_ac_to.EditValue

            If cek_avail_list(_code, cbo_sign.EditValue) = True Then
                MsgBox("Rule " & _code & "(" & cbo_sign.EditValue & ") Already Available on List", MsgBoxStyle.Critical, "Duplicate")
                Exit Sub
            End If

            _avail_code = False
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand

                        .Command.CommandType = CommandType.Text
                        'ant 24 jan 2012 (revisi jika kode akun ada titik-nya)
                        .Command.CommandText = "select ac_id,ac_code,ac_name from ac_mstr where replace(ac_code,'.','') in (" & SetSetring(_code) & ")"
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader

                        _dtrow = dt_edit_detail.NewRow
                        _dtrow("cfdruled_oid") = Guid.NewGuid.ToString
                        While .DataReader.Read
                            If Replace(.DataReader("ac_code").ToString, ".", "") = _code.ToString Then
                                _avail_code = True
                                _dtrow("cfdruled_ac_id") = .DataReader("ac_id").ToString
                                _dtrow("ac_code") = .DataReader("ac_code").ToString
                                _dtrow("ac_name") = .DataReader("ac_name").ToString
                                _dtrow("cfdruled_sign") = cbo_sign.EditValue
                            End If
                        End While
                        '---------------------------------

                        'pak rana tidak mau pake
                        'If _avail_code = False Then
                        '    MsgBox("Account Code " & _code_from.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                        '    Exit Sub
                        'End If

                        'solusi pak rana yg atas
                        If _avail_code = True Then
                            dt_edit_detail.Rows.Add(_dtrow)
                        End If


                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


        Next
        dt_edit_detail.AcceptChanges()
        '--------------------------------------
    End Sub

    Private Function cek_avail_list(ByVal _par_code As String, ByVal _par_sign As String) As Boolean
        cek_avail_list = False
        Try
            For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
                If (_par_code = Replace(dt_edit_detail.Rows(i).Item("ac_code").ToString, ".", "")) _
                And (_par_sign = dt_edit_detail.Rows(i).Item("cfdruled_sign").ToString) Then
                    cek_avail_list = True
                End If
            Next
        Catch
        End Try
        Return cek_avail_list
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        'Dim _col As String = gv_edit.FocusedColumn.Name
        'Dim _row As Integer = gv_edit.FocusedRowHandle

        'If _col = "cfdruled_sign" Then
        '    If e.Value not in ("D","C") Then
        '        gv_edit.SetRowCellValue(e.RowHandle, "cfdruled_sign", "D")
        '    End If
        'End If
    End Sub

    '---------------------------------------------------------------

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "ac_code" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            'frm._en_id = cfdrule_en_id.EditValue
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub gv_sum_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_sum_edit.DoubleClick
        browse_data_sum()
    End Sub

    Private Sub browse_data_sum()
        Dim _col As String = gv_sum_edit.FocusedColumn.Name
        Dim _row As Integer = gv_sum_edit.FocusedRowHandle

        If (_col = "group_name") Or (_col = "tranline_name") Then
            Dim frm As New FCFDRuleSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = 1
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        End If

    End Sub

    Private Sub cfdrule_is_sum_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cfdrule_is_sum.CheckedChanged
        If cfdrule_is_sum.Checked = False Then
            tcg_edit.SelectedTabPageIndex = 0
        Else
            tcg_edit.SelectedTabPageIndex = 1
        End If
    End Sub
End Class
