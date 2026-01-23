Imports npgsql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FRosettaRule
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
        DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        rstrule_en_id.Properties.DataSource = dt_bantu
        rstrule_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        rstrule_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        rstrule_en_id.ItemIndex = 0

    End Sub

    Private Sub rstrule_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rstrule_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(rstrule_en_id.EditValue, "ros_acc_group"))
        rstrule_group_id.Properties.DataSource = dt_bantu
        rstrule_group_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        rstrule_group_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        rstrule_group_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(rstrule_en_id.EditValue.ToString, "ros_acc_name"))
        rstrule_name_id.Properties.DataSource = dt_bantu
        rstrule_name_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        rstrule_name_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        rstrule_name_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(rstrule_en_id.EditValue, "ros_line_trans"))
        rstrule_line_id.Properties.DataSource = dt_bantu
        rstrule_line_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        rstrule_line_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        rstrule_line_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(rstrule_en_id.EditValue, "ros_cash_flow"))
        rstrule_cashflow_id.Properties.DataSource = dt_bantu
        rstrule_cashflow_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        rstrule_cashflow_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        rstrule_cashflow_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "rstrule_oid", False)
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "account_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Transaction Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Code", "cashflow_name", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Remarks", "rstrule_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "rstrule_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "rstrule_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")
        add_column_copy(gv_master, "User Update", "rstrule_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "rstrule_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "d")


        add_column(gv_detail, "rstruled_oid", False)
        add_column(gv_detail, "rstruled_rstrule_oid", False)
        add_column_copy(gv_detail, "Account Code", "ac_code1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Account Name", "ac_name1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Sign", "rstruled_sign1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "For Account Code", "ac_code2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "For Account Name", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "For Account Sign", "rstruled_sign2", DevExpress.Utils.HorzAlignment.Center)


        add_column(gv_edit, "rstruled_oid", False)
        add_column(gv_edit, "rstruled_ac_id1", False)
        add_column(gv_edit, "Account Code", "ac_code1", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "Account Name", "ac_name1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "Account Sign", "rstruled_sign1", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_edit, "rstruled_ac_id2", False)
        add_column(gv_edit, "For Account Code", "ac_code2", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_edit, "For Account Name", "ac_name2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "For Account Sign", "rstruled_sign2", DevExpress.Utils.HorzAlignment.Center)

    End Sub


    Public Overrides Function get_sequel() As String

        get_sequel = "SELECT  " _
                & "  rstrule_oid, " _
                & "  rstrule_dom_id, " _
                & "  rstrule_en_id, " _
                & "  rstrule_add_by, " _
                & "  rstrule_add_date, " _
                & "  rstrule_upd_by, " _
                & "  rstrule_upd_date, " _
                & "  rstrule_group_id, " _
                & "  rstrule_name_id, " _
                & "  rstrule_line_id, " _
                & "  rstrule_cashflow_id, " _
                & "  rstrule_dt, " _
                & "  rstrule_remarks, " _
                & "  en_desc, " _
                & "  group_mstr.code_name as group_name, " _
                & "  account_mstr.code_name as account_name, " _
                & "  tranline_mstr.code_name as tranline_name, " _
                & "  cashflow_mstr.code_name as cashflow_name " _
                & "FROM  " _
                & "  public.rstrule_mstr" _
                & "  INNER JOIN public.en_mstr ON (public.rstrule_mstr.rstrule_en_id = public.en_mstr.en_id)" _
                & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id "


        Return get_sequel
    End Function


    Public Overrides Sub load_data_grid_detail()
        Dim _get_oid As String = ""
        Dim i As Integer

        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        End If

        Dim sql As String

        Try
            ds.Tables("detail").Clear()
        Catch ex As Exception
        End Try

        sql = "SELECT  " _
            & "  rstruled_oid, " _
            & "  rstruled_rstrule_oid, " _
            & "  rstruled_seq, " _
            & "  rstruled_ac_id1, " _
            & "  ac_mstr1.ac_code as ac_code1, " _
            & "  ac_mstr1.ac_name as ac_name1, " _
            & "  rstruled_sign1, " _
            & "  rstruled_ac_id2, " _
            & "  ac_mstr2.ac_code as ac_code2, " _
            & "  ac_mstr2.ac_name as ac_name2, " _
            & "  rstruled_sign2 " _
            & "FROM  " _
            & "  public.rstruled_det " _
            & "  INNER JOIN ac_mstr ac_mstr1 ON (rstruled_ac_id1 = ac_mstr1.ac_id) " _
            & "  LEFT OUTER JOIN ac_mstr ac_mstr2 ON (rstruled_ac_id2 = ac_mstr2.ac_id) "

        load_data_detail(sql, gc_detail, "detail")

    End Sub

    Public Overrides Sub insert_data_awal()

        _rstrule_oid_edit = ""
        rstrule_en_id.ItemIndex = 0
        rstrule_group_id.ItemIndex = 0
        rstrule_name_id.ItemIndex = 0
        rstrule_line_id.ItemIndex = 0
        rstrule_cashflow_id.ItemIndex = 0
        rstrule_remarks.Text = ""

        ssql = "SELECT  " _
            & "  rstruled_oid, " _
            & "  rstruled_rstrule_oid, " _
            & "  rstruled_seq, " _
            & "  rstruled_ac_id1, " _
            & "  ac_mstr1.ac_code as ac_code1, " _
            & "  ac_mstr1.ac_name as ac_name1, " _
            & "  rstruled_sign1, " _
            & "  rstruled_ac_id2, " _
            & "  ac_mstr2.ac_code as ac_code2, " _
            & "  ac_mstr2.ac_name as ac_name2, " _
            & "  rstruled_sign2 " _
            & "FROM  " _
            & "  public.rstruled_det " _
            & "  INNER JOIN ac_mstr ac_mstr1 ON (rstruled_ac_id1 = ac_mstr1.ac_id) " _
            & "  INNER JOIN ac_mstr ac_mstr2 ON (rstruled_ac_id2 = ac_mstr2.ac_id) " _
            & "WHERE " _
            & "  rstruled_rstrule_oid is null"

        dt_edit_detail = GetTableData(ssql)
        gc_edit.DataSource = dt_edit_detail
        gv_edit.BestFitColumns()

        Try

            tcg_header.SelectedTabPageIndex = 0
            DockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden
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

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                        & "  public.rstrule_mstr " _
                                        & "( " _
                                        & "  rstrule_oid, " _
                                        & "  rstrule_dom_id, " _
                                        & "  rstrule_en_id, " _
                                        & "  rstrule_add_by, " _
                                        & "  rstrule_add_date, " _
                                        & "  rstrule_group_id, " _
                                        & "  rstrule_name_id, " _
                                        & "  rstrule_line_id, " _
                                        & "  rstrule_cashflow_id, " _
                                        & "  rstrule_dt, " _
                                        & "  rstrule_remarks " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_rstrule_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(rstrule_en_id.EditValue) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetInteger(rstrule_group_id.EditValue) & ",  " _
                                        & SetInteger(rstrule_name_id.EditValue) & ",  " _
                                        & SetInteger(rstrule_line_id.EditValue) & ",  " _
                                        & SetInteger(rstrule_cashflow_id.EditValue) & ",  " _
                                        & SetDate(CekTanggal) & ",  " _
                                        & SetSetring(rstrule_remarks.Text) & "  " _
                                        & ");"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0
                        For Each dr As DataRow In dt_edit_detail.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.rstruled_det " _
                                    & "( " _
                                    & "  rstruled_oid, " _
                                    & "  rstruled_rstrule_oid, " _
                                    & "  rstruled_seq, " _
                                    & "  rstruled_ac_id1, " _
                                    & "  rstruled_sign1, " _
                                    & "  rstruled_ac_id2, " _
                                    & "  rstruled_sign2 " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_rstrule_oid) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(dr("rstruled_ac_id1")) & ",  " _
                                    & SetSetring(dr("rstruled_sign1")) & ",  " _
                                    & SetInteger(dr("rstruled_ac_id2")) & ",  " _
                                    & SetSetring(dr("rstruled_sign2")) & "  " _
                                    & ");"
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = ssql
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        insert = True
                        sqlTran.Commit()

                        after_success()
                        set_row(_rstrule_oid, "rstrule_oid")
                        dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
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
            gv_detail.Columns("rstruled_rstrule_oid").FilterInfo = _
            New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[rstruled_rstrule_oid] = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rstrule_oid").ToString & "'")
            gv_detail.BestFitColumns()
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            rstrule_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _rstrule_oid_edit = .Item("rstrule_oid").ToString
                rstrule_en_id.EditValue = .Item("rstrule_en_id")
                rstrule_group_id.EditValue = .Item("rstrule_group_id")
                rstrule_name_id.EditValue = .Item("rstrule_name_id")
                rstrule_line_id.EditValue = .Item("rstrule_line_id")
                rstrule_cashflow_id.EditValue = .Item("rstrule_cashflow_id")
                rstrule_remarks.Text = SetString(.Item("rstrule_remarks"))

            End With


            ssql = "SELECT  " _
                & "  rstruled_oid, " _
                & "  rstruled_rstrule_oid, " _
                & "  rstruled_seq, " _
                & "  rstruled_ac_id1, " _
                & "  ac_mstr1.ac_code as ac_code1, " _
                & "  ac_mstr1.ac_name as ac_name1, " _
                & "  rstruled_sign1, " _
                & "  rstruled_ac_id2, " _
                & "  ac_mstr2.ac_code as ac_code2, " _
                & "  ac_mstr2.ac_name as ac_name2, " _
                & "  rstruled_sign2 " _
                & "FROM  " _
                & "  public.rstruled_det " _
                & "  INNER JOIN ac_mstr ac_mstr1 ON (rstruled_ac_id1 = ac_mstr1.ac_id) " _
                & "  LEFT OUTER JOIN ac_mstr ac_mstr2 ON (rstruled_ac_id2 = ac_mstr2.ac_id) " _
                & "WHERE " _
                & "  rstruled_rstrule_oid =" & SetSetring(_rstrule_oid_edit)

            dt_edit_detail = GetTableData(ssql)
            gc_edit.DataSource = dt_edit_detail
            gv_edit.BestFitColumns()

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
                                            & "  public.rstrule_mstr   " _
                                            & "SET  " _
                                            & "  rstrule_en_id = " & SetInteger(rstrule_en_id.EditValue) & ",  " _
                                            & "  rstrule_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "  rstrule_upd_date = " & SetDate(CekTanggal) & ",  " _
                                            & "  rstrule_group_id = " & SetInteger(rstrule_group_id.EditValue) & ",  " _
                                            & "  rstrule_name_id = " & SetInteger(rstrule_name_id.EditValue) & ",  " _
                                            & "  rstrule_line_id = " & SetInteger(rstrule_line_id.EditValue) & ",  " _
                                            & "  rstrule_cashflow_id = " & SetInteger(rstrule_cashflow_id.EditValue) & ",  " _
                                            & "  rstrule_dt = " & SetDate(CekTanggal) & ",  " _
                                            & "  rstrule_remarks = " & SetSetring(rstrule_remarks.Text) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  rstrule_oid = " & SetSetring(_rstrule_oid_edit) & " "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from rstruled_det where rstruled_rstrule_oid = " & SetSetring(_rstrule_oid_edit)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        Dim _seq As Integer = 0
                        For Each dr As DataRow In dt_edit_detail.Rows
                            ssql = "INSERT INTO  " _
                                    & "  public.rstruled_det " _
                                    & "( " _
                                    & "  rstruled_oid, " _
                                    & "  rstruled_rstrule_oid, " _
                                    & "  rstruled_seq, " _
                                    & "  rstruled_ac_id1, " _
                                    & "  rstruled_sign1, " _
                                    & "  rstruled_ac_id2, " _
                                    & "  rstruled_sign2 " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_rstrule_oid_edit) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(dr("rstruled_ac_id1")) & ",  " _
                                    & SetSetring(dr("rstruled_sign1")) & ",  " _
                                    & SetInteger(dr("rstruled_ac_id2")) & ",  " _
                                    & SetSetring(dr("rstruled_sign2")) & "  " _
                                    & ");"
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
                        set_row(Trim(_rstrule_oid_edit.ToString), "rstrule_oid")
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

                            ssql = "delete from rstrule_mstr where rstrule_oid = '" + ds.Tables(0).Rows(row).Item("rstrule_oid").ToString + "'"
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

        gc_edit.EmbeddedNavigator.Buttons.DoClick(gc_edit.EmbeddedNavigator.Buttons.EndEdit)
        dt_edit_detail.AcceptChanges()

        'cek sign
        For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
            If dt_edit_detail.Rows(i).Item("rstruled_sign1").ToString = dt_edit_detail.Rows(i).Item("rstruled_sign2").ToString Then
                MsgBox("Sign is Double on Row " & (i + 1).ToString, MsgBoxStyle.Critical, "Unable to Save")
                before_save = False
            End If
        Next

        'untuk cek wildcard
        For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .Connection.Open()
                        .Command = .Connection.CreateCommand

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select rstrule_oid, ac_name  from rstrule_mstr " & _
                                               " inner join rstruled_det on rstruled_rstrule_oid = rstrule_oid " & _
                                               " inner join ac_mstr on ac_id = rstruled_ac_id1  " & _
                                               " where rstruled_ac_id1 = " & SetInteger(dt_edit_detail.Rows(i).Item("rstruled_ac_id1")) & _
                                               " and rstruled_sign1 = " & SetSetring(dt_edit_detail.Rows(i).Item("rstruled_sign1")) & _
                                               " and coalesce(rstruled_ac_id2,-1) = -1" 

                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader

                        While .DataReader.Read
                            If _rstrule_oid_edit.ToString <> .DataReader("rstrule_oid") Then
                                MsgBox("Rule for Account " & .DataReader("ac_name") & " Is Wildcard On Another Rule", MsgBoxStyle.Critical, "Double Data")
                                Return False
                            End If
                        End While

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                before_save = False
            End Try
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

    Private Sub btn_gen_rule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If cbo_sign1.EditValue = cbo_sign2.EditValue Then
            MsgBox("Unable Generate Rule, Please Check Sign", MsgBoxStyle.Critical, "Sign Error")
            Exit Sub
        End If

        Dim _dtrow As DataRow
        Dim _avail_code_from, _avail_code_to As Boolean

        'ant 31 jan 2012 revisi agar yg wildcard bisa generate rule
        For _code_from As Integer = te_ac_from1.EditValue To te_ac_from2.EditValue
            If Trim(te_ac_to1.EditValue) = "" Then
                _avail_code_from = False
                '_avail_code_to = False
                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .Connection.Open()
                            .Command = .Connection.CreateCommand

                            .Command.CommandType = CommandType.Text
                            'ant 24 jan 2012 (revisi jika kode akun ada titik-nya)
                            .Command.CommandText = "select ac_id,ac_code,ac_name from ac_mstr where replace(ac_code,'.','') in (" & SetSetring(_code_from) & ")"
                            .InitializeCommand()
                            .DataReader = .Command.ExecuteReader

                            _dtrow = dt_edit_detail.NewRow
                            _dtrow("rstruled_oid") = Guid.NewGuid.ToString
                            While .DataReader.Read
                                If Replace(.DataReader("ac_code").ToString, ".", "") = _code_from.ToString Then
                                    _avail_code_from = True
                                    _dtrow("rstruled_ac_id1") = .DataReader("ac_id").ToString
                                    _dtrow("ac_code1") = .DataReader("ac_code").ToString
                                    _dtrow("ac_name1") = .DataReader("ac_name").ToString
                                    _dtrow("rstruled_sign1") = cbo_sign1.EditValue
                                    'ElseIf Replace(.DataReader("ac_code").ToString, ".", "") = _code_to.ToString Then
                                    '_avail_code_to = True
                                    '_dtrow("rstruled_ac_id2") = DBNull
                                    '_dtrow("ac_code2") = .DataReader("ac_code").ToString
                                    '_dtrow("ac_name2") = .DataReader("ac_name").ToString
                                    '_dtrow("rstruled_sign2") = cbo_sign2.EditValue
                                End If
                            End While
                            '---------------------------------

                            'pak rana tidak mau pake
                            'If _avail_code_from = False Then
                            '    MsgBox("Account Code " & _code_from.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                            '    Exit Sub
                            'ElseIf _avail_code_to = False Then
                            '    MsgBox("Account Code " & _code_to.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                            '    Exit Sub
                            'End If

                            'solusi pak rana yg atas
                            If _avail_code_from = True Then
                                dt_edit_detail.Rows.Add(_dtrow)
                            End If


                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Else
                For _code_to As Integer = te_ac_to1.EditValue To te_ac_to2.EditValue
                    If cek_avail_list(_code_from, cbo_sign1.EditValue, _code_to, cbo_sign2.EditValue) = True Then
                        MsgBox("Rule " & _code_from & "(" & cbo_sign1.EditValue & ") for " & _code_to & "(" & cbo_sign2.EditValue & ") Already Available on List", MsgBoxStyle.Critical, "Duplicate")
                        Exit Sub
                    End If

                    _avail_code_from = False
                    _avail_code_to = False
                    Try
                        Using objcb As New master_new.WDABasepgsql("", "")
                            With objcb
                                .Connection.Open()
                                .Command = .Connection.CreateCommand

                                .Command.CommandType = CommandType.Text
                                'ant 24 jan 2012 (revisi jika kode akun ada titik-nya)
                                .Command.CommandText = "select ac_id,ac_code,ac_name from ac_mstr where replace(ac_code,'.','') in (" & SetSetring(_code_from) & "," & SetSetring(_code_to) & ")"
                                .InitializeCommand()
                                .DataReader = .Command.ExecuteReader

                                _dtrow = dt_edit_detail.NewRow
                                _dtrow("rstruled_oid") = Guid.NewGuid.ToString
                                While .DataReader.Read
                                    If Replace(.DataReader("ac_code").ToString, ".", "") = _code_from.ToString Then
                                        _avail_code_from = True
                                        _dtrow("rstruled_ac_id1") = .DataReader("ac_id").ToString
                                        _dtrow("ac_code1") = .DataReader("ac_code").ToString
                                        _dtrow("ac_name1") = .DataReader("ac_name").ToString
                                        _dtrow("rstruled_sign1") = cbo_sign1.EditValue
                                    ElseIf Replace(.DataReader("ac_code").ToString, ".", "") = _code_to.ToString Then
                                        _avail_code_to = True
                                        _dtrow("rstruled_ac_id2") = .DataReader("ac_id").ToString
                                        _dtrow("ac_code2") = .DataReader("ac_code").ToString
                                        _dtrow("ac_name2") = .DataReader("ac_name").ToString
                                        _dtrow("rstruled_sign2") = cbo_sign2.EditValue
                                    End If
                                End While
                                '---------------------------------

                                'pak rana tidak mau pake
                                'If _avail_code_from = False Then
                                '    MsgBox("Account Code " & _code_from.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                                '    Exit Sub
                                'ElseIf _avail_code_to = False Then
                                '    MsgBox("Account Code " & _code_to.ToString & " Is Unknown", MsgBoxStyle.Critical, "Account Unavailable")
                                '    Exit Sub
                                'End If

                                'solusi pak rana yg atas
                                If _avail_code_from = True And _avail_code_to = True Then
                                    dt_edit_detail.Rows.Add(_dtrow)
                                End If


                            End With
                        End Using
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                Next
            End If
        Next
        dt_edit_detail.AcceptChanges()
        '--------------------------------------
    End Sub

    Private Function cek_avail_list(ByVal _par_code1 As String, ByVal _par_sign1 As String, ByVal _par_code2 As String, ByVal _par_sign2 As String) As Boolean
        cek_avail_list = False
        Try
            For i As Integer = 0 To dt_edit_detail.Rows.Count - 1
                If (_par_code1 = dt_edit_detail.Rows(i).Item("ac_code1").ToString) _
                And (_par_sign1 = dt_edit_detail.Rows(i).Item("rstruled_sign1").ToString) _
                And (_par_code2 = dt_edit_detail.Rows(i).Item("ac_code2").ToString) _
                And (_par_sign2 = dt_edit_detail.Rows(i).Item("rstruled_sign2").ToString) Then

                    cek_avail_list = True

                End If
            Next
        Catch
        End Try
        Return cek_avail_list
    End Function

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "rstruled_sign1" Then
            If (e.Value <> "D") And (e.Value <> "C") Then
                If gv_edit.GetRowCellValue(_row, "rstruled_sign2").ToString = "C" Then
                    gv_edit.SetRowCellValue(e.RowHandle, "rstruled_sign1", "D")
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "rstruled_sign1", "C")
                End If
            End If
        ElseIf _col = "rstruled_sign2" Then
            If (e.Value <> "D") And (e.Value <> "C") Then
                If gv_edit.GetRowCellValue(_row, "rstruled_sign1").ToString = "C" Then
                    gv_edit.SetRowCellValue(e.RowHandle, "rstruled_sign2", "D")
                Else
                    gv_edit.SetRowCellValue(e.RowHandle, "rstruled_sign2", "C")
                End If
            End If
        End If
    End Sub

    '---------------------------------------------------------------

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle

        If _col = "ac_code1" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = rstrule_en_id.EditValue
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        ElseIf _col = "ac_code2" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = rstrule_en_id.EditValue
            frm._col_name = _col
            frm.type_form = True
            frm.ShowDialog()
        End If
        
    End Sub

    Private Sub btn_gen_rule_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_gen_rule.Click
        Try
            Dim dt_kiri, dt_kanan As New DataTable

            ssql = "select ac_id,ac_code,ac_name from ac_mstr where ac_code_hirarki between " _
                & SetSetring(te_ac_from1.Text) & " and " & SetSetring(te_ac_from2.Text) & " and ac_is_sumlevel='N'"

            dt_kiri = GetTableData(ssql)

            ssql = "select ac_id,ac_code,ac_name from ac_mstr where ac_code_hirarki between " _
               & SetSetring(te_ac_to1.Text) & " and " & SetSetring(te_ac_to2.Text) & " and ac_is_sumlevel='N'"

            dt_kanan = GetTableData(ssql)


            Dim _dtrow As DataRow

            For Each dr_kiri As DataRow In dt_kiri.Rows
                For Each dr_kanan As DataRow In dt_kanan.Rows
                    _dtrow = dt_edit_detail.NewRow
                    _dtrow("rstruled_ac_id1") = dr_kiri("ac_id").ToString
                    _dtrow("ac_code1") = dr_kiri("ac_code").ToString
                    _dtrow("ac_name1") = dr_kiri("ac_name").ToString
                    _dtrow("rstruled_sign1") = cbo_sign1.EditValue
                    _dtrow("rstruled_ac_id2") = dr_kanan("ac_id").ToString
                    _dtrow("ac_code2") = dr_kanan("ac_code").ToString
                    _dtrow("ac_name2") = dr_kanan("ac_name").ToString
                    _dtrow("rstruled_sign2") = cbo_sign2.EditValue
                    dt_edit_detail.Rows.Add(_dtrow)
                Next
            Next
            dt_edit_detail.AcceptChanges()
            gv_edit.BestFitColumns()
            Box("Generate success")


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtClear.Click
        Try
            dt_edit_detail.Rows.Clear()
            dt_edit_detail.AcceptChanges()
            gv_edit.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub te_ac_from1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles te_ac_from1.DoubleClick, te_ac_from2.DoubleClick, te_ac_to1.DoubleClick, te_ac_to2.DoubleClick
        Try
            Dim frm As New FAccountSearch()
            frm.set_win(Me)
            frm._obj2 = sender
            frm._col_name = ""
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
