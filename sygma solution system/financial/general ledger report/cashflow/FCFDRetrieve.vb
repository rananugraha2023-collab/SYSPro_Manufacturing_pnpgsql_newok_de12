Imports npgsql
Imports master_new.ModFunction

Public Class FCFDRetrieve
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_glt As DataSet
    Dim ds_group_glt As DataSet
    Dim ds_cashflow As DataSet
    Dim _session As String
    Dim _is_setara_kas As Boolean = False

    Private Sub FCFDRetrieve_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub form_first_load()
        'create_table()
        'help_load_data(False)
        load_cb()
        on_load()
        format_grid()
        add_handler_numeric()
        'add_groupsummary()
        'AllowIncrementalSearch()
        set_component()
        'load_Columns()

        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        'master
        add_column(gv_master, "cfd_session", False)
        add_column(gv_master, "cfd_ref_oid", False)
        'add_column_copy(gv_master, "Sequence", "cfdrule_seq", DevExpress.Utils.HorzAlignment.Default)
        'add_column_copy(gv_master, "Is Sum of Rule", "cfdrule_is_sum", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Group", "group_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cashflow Line", "tranline_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Amount", "cfd_amount", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n2")
        add_column_copy(gv_master, "Remarks", "cfdrule_remarks", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        _session = Guid.NewGuid.ToString

        'set dulu temporary tablenya
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "insert into cfd_temp  " _
                                                & " select " _
                                                & SetSetring(_session.ToString) & " as _session, " _
                                                & " cfdrule_oid, " _
                                                & " 0.00 as amount, " _
                                                & SetSetring(le_periode.EditValue) & " " _
                                                & " from cfdrule_mstr "

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '---------

        'untuk generate cashflow
        generate_cashflow()
        generate_sum_cashflow()
        '------------------------------

        Dim _cfd_nett_text As String = func_coll.get_conf_file("text_cfd_nett")
        Dim _cfd_start_period As String = func_coll.get_conf_file("text_cfd_start_period")
        Dim _cfd_end_period As String = func_coll.get_conf_file("text_cfd_end_period")

        ds_cashflow = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  cfd_session, " _
                            & "  cfd_ref_oid, " _
                            & "  cfd_amount, " _
                            & "  cfdrule_group_id, " _
                            & "  coalesce(cfdrule_is_sum,'N') as cfdrule_is_sum, " _
                            & "  cfdrule_line_id, " _
                            & "  cfdrule_seq, " _
                            & "  cfdrule_remarks, " _
                            & "  group_mstr.code_name as group_name, " _
                            & "  tranline_mstr.code_name as tranline_name " _
                            & "FROM  " _
                            & "  public.cfd_temp" _
                            & "  INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid)" _
                            & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
                            & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
                            & " WHERE cfd_session = " & SetSetring(_session.ToString) _
                            & " UNION  " _
                            & "SELECT  " _
                            & SetSetring(_session.ToString) & " as cfd_session, " _
                            & "  null as cfd_ref_oid, " _
                            & "  (select sum(cfd_amount) from cfd_temp INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid) WHERE coalesce(cfdrule_is_sum,'N') = 'N' and cfd_session = " & SetSetring(_session.ToString) & ") as cfd_amount, " _
                            & "  null cfdrule_group_id, " _
                            & "  'Y' as cfdrule_is_sum, " _
                            & "  null as cfdrule_line_id, " _
                            & "  101 as cfdrule_seq, " _
                            & "  '' as cfdrule_remarks, " _
                            & " 'TOTAL' as group_name, " _
                            & SetSetring(_cfd_nett_text) & " as tranline_name " _
                            & " UNION  " _
                            & "SELECT  " _
                            & SetSetring(_session.ToString) & " as cfd_session, " _
                            & "  null as cfd_ref_oid, " _
                            & "  (select sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y') as cfd_amount, " _
                            & "  null cfdrule_group_id, " _
                            & "  'Y' as cfdrule_is_sum, " _
                            & "  null as cfdrule_line_id, " _
                            & "  102 as cfdrule_seq, " _
                            & "  '' as cfdrule_remarks, " _
                            & " 'TOTAL' as group_name, " _
                            & SetSetring(_cfd_start_period) & " as tranline_name " _
                            & " UNION  " _
                            & "SELECT  " _
                            & SetSetring(_session.ToString) & " as cfd_session, " _
                            & "  null as cfd_ref_oid, " _
                            & " ((select sum(cfd_amount) from cfd_temp INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid) WHERE coalesce(cfdrule_is_sum,'N') = 'N' and cfd_session = " & SetSetring(_session.ToString) & ") + (select sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y')) as cfd_amount, " _
                            & "  null cfdrule_group_id, " _
                            & "  'Y' as cfdrule_is_sum, " _
                            & "  null as cfdrule_line_id, " _
                            & "  103 as cfdrule_seq, " _
                            & "  '' as cfdrule_remarks, " _
                            & " 'TOTAL' as group_name, " _
                            & SetSetring(_cfd_end_period) & " as tranline_name " _
                            & " ORDER BY cfdrule_seq  "

                    '.SQL = "SELECT  " _
                    '        & "  cfd_session, " _
                    '        & "  cfd_ref_oid, " _
                    '        & "  cfd_amount, " _
                    '        & "  cfdrule_group_id, " _
                    '        & "  coalesce(cfdrule_is_sum,'N') as cfdrule_is_sum, " _
                    '        & "  cfdrule_line_id, " _
                    '        & "  cfdrule_seq, " _
                    '        & "  cfdrule_remarks, " _
                    '        & "  en_desc, " _
                    '        & "  group_mstr.code_name as group_name, " _
                    '        & "  tranline_mstr.code_name as tranline_name " _
                    '        & "FROM  " _
                    '        & "  public.cfd_temp" _
                    '        & "  INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid)" _
                    '        & "  INNER JOIN public.en_mstr ON (public.cfdrule_mstr.cfdrule_en_id = public.en_mstr.en_id)" _
                    '        & "  INNER JOIN code_mstr group_mstr on cfdrule_group_id = group_mstr.code_id " _
                    '        & "  INNER JOIN code_mstr tranline_mstr on cfdrule_line_id = tranline_mstr.code_id " _
                    '        & " WHERE cfd_session = " & SetSetring(_session.ToString) _
                    '        & " UNION  " _
                    '        & "SELECT  " _
                    '        & SetSetring(_session.ToString) & " as cfd_session, " _
                    '        & "  null as cfd_ref_oid, " _
                    '        & "  (select sum(cfd_amount) from cfd_temp INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid) WHERE coalesce(cfdrule_is_sum,'N') = 'N' and cfd_session = " & SetSetring(_session.ToString) & ") as cfd_amount, " _
                    '        & "  null cfdrule_group_id, " _
                    '        & "  'Y' as cfdrule_is_sum, " _
                    '        & "  null as cfdrule_line_id, " _
                    '        & "  101 as cfdrule_seq, " _
                    '        & "  '' as cfdrule_remarks, " _
                    '        & SetSetring(le_entity.Text) & " as en_desc, " _
                    '        & " 'TOTAL' as group_name, " _
                    '        & SetSetring(_cfd_nett_text) & " as tranline_name " _
                    '        & " UNION  " _
                    '        & "SELECT  " _
                    '        & SetSetring(_session.ToString) & " as cfd_session, " _
                    '        & "  null as cfd_ref_oid, " _
                    '        & "  (select sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y') as cfd_amount, " _
                    '        & "  null cfdrule_group_id, " _
                    '        & "  'Y' as cfdrule_is_sum, " _
                    '        & "  null as cfdrule_line_id, " _
                    '        & "  102 as cfdrule_seq, " _
                    '        & "  '' as cfdrule_remarks, " _
                    '        & SetSetring(le_entity.Text) & " as en_desc, " _
                    '        & " 'TOTAL' as group_name, " _
                    '        & SetSetring(_cfd_start_period) & " as tranline_name " _
                    '        & " UNION  " _
                    '        & "SELECT  " _
                    '        & SetSetring(_session.ToString) & " as cfd_session, " _
                    '        & "  null as cfd_ref_oid, " _
                    '        & " ((select sum(cfd_amount) from cfd_temp INNER JOIN public.cfdrule_mstr on (cfd_ref_oid = cfdrule_oid) WHERE coalesce(cfdrule_is_sum,'N') = 'N' and cfd_session = " & SetSetring(_session.ToString) & ") + (select sum(glbal_balance_open) from glbal_balance INNER JOIN ac_mstr on glbal_ac_id = ac_id WHERE glbal_gcal_oid = " & SetSetring(le_periode.EditValue) & " and ac_is_cf = 'Y')) as cfd_amount, " _
                    '        & "  null cfdrule_group_id, " _
                    '        & "  'Y' as cfdrule_is_sum, " _
                    '        & "  null as cfdrule_line_id, " _
                    '        & "  103 as cfdrule_seq, " _
                    '        & "  '' as cfdrule_remarks, " _
                    '        & SetSetring(le_entity.Text) & " as en_desc, " _
                    '        & " 'TOTAL' as group_name, " _
                    '        & SetSetring(_cfd_end_period) & " as tranline_name " _
                    '        & " ORDER BY cfdrule_seq  "


                    .InitializeCommand()
                    .FillDataSet(ds_cashflow, "direct_cashflow")
                    gc_master.DataSource = ds_cashflow.Tables(0)
                    gv_master.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'hapus lagi isi temporary tablenya
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM cfd_temp WHERE cfd_session = " & SetSetring(_session.ToString)
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()

                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '---------

    End Sub

    Private Sub generate_cashflow()

        Dim _gcal_available As Boolean = False
        Dim _gcal_start_date As Date
        Dim _gcal_end_date As Date

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select gcal_start_date, gcal_end_date  from gcal_mstr " + _
                                           " where gcal_start_date <= " + SetDate(le_periode.Text) + _
                                           " and gcal_end_date >= " + SetDate(le_periode.Text)

                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader

                    While .DataReader.Read
                        _gcal_available = True
                        _gcal_start_date = CDate(.DataReader("gcal_start_date"))
                        _gcal_end_date = CDate(.DataReader("gcal_end_date"))
                    End While

                    If _gcal_available = False Then
                        MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + le_periode.Text.ToString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '-------------------------------------------------------

        'collecting data dari glt_det

        ds_glt = New DataSet
        ds_group_glt = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                            & "  glt_oid, " _
                            & "  glt_dom_id, " _
                            & "  glt_en_id, " _
                            & "  glt_add_by, " _
                            & "  glt_add_date, " _
                            & "  glt_upd_by, " _
                            & "  glt_upd_date, " _
                            & "  glt_gl_oid, " _
                            & "  glt_code, " _
                            & "  glt_date, " _
                            & "  glt_type, " _
                            & "  glt_cu_id, " _
                            & "  glt_exc_rate, " _
                            & "  glt_seq, " _
                            & "  glt_ac_id, " _
                            & "  ac_sign, " _
                            & "  coalesce(ac_priority,99) as ac_priority, " _
                            & "  coalesce(ac_is_cf,'N') as ac_is_cf, " _
                            & "  glt_sb_id, " _
                            & "  glt_cc_id, " _
                            & "  glt_desc, " _
                            & "  glt_debit, " _
                            & "  glt_credit, " _
                            & "  glt_ref_tran_id, " _
                            & "  glt_ref_trans_code, " _
                            & "  glt_posted, " _
                            & "  glt_dt, " _
                            & "  glt_daybook, " _
                            & "  glt_ref_oid, " _
                            & "  coalesce(glt_is_reverse,'N') as glt_is_reverse, " _
                            & "  glt_desc_detail, " _
                            & "  glt_ref_detail_no, " _
                            & "  glt_is_gen_ros, " _
                            & "  'N' as _empty " _
                            & "FROM  " _
                            & "  public.glt_det " _
                            & "  INNER JOIN public.ac_mstr ON (public.glt_det.glt_ac_id = public.ac_mstr.ac_id) " _
                            & "  where glt_date >= " & SetDate(_gcal_start_date) _
                            & "  and glt_date <= " & SetDate(_gcal_end_date) _
                            & " and glt_type <> 'JE' order by glt_date, glt_code, ac_is_cf, ac_priority, glt_credit, glt_debit "

                    '& "  and glt_en_id = " & SetInteger(le_entity.EditValue) _
                    .InitializeCommand()
                    .FillDataSet(ds_glt, "list_glt")
                    .FillDataSet(ds_group_glt, "list_glt_group")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim dr As DataRow
        Dim _glt_code As String = ""
        Dim ssqls As New ArrayList

        If ds_glt.Tables(0).Rows.Count > 0 Then
            _glt_code = ds_glt.Tables(0).Rows(0).Item("glt_code")
        End If

        ds_group_glt.Tables(0).Clear()

        For i As Integer = 0 To ds_glt.Tables(0).Rows.Count - 1
            If _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code") Then
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_is_cf") = ds_glt.Tables(0).Rows(i).Item("ac_is_cf")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")

                'untuk yang reverse dibalik kredit debit dan dinegatifkan
                If ds_glt.Tables(0).Rows(i).Item("glt_is_reverse") = "Y" Then
                    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_credit") * -1
                    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_debit") * -1
                Else
                    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                End If

                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")

                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    'cek dulu ada kas setara kas atau tidak
                    _is_setara_kas = False
                    For a As Integer = 0 To ds_group_glt.Tables(0).Rows.Count - 1
                        If ds_group_glt.Tables(0).Rows(a).Item("ac_is_cf") = "Y" Then
                            _is_setara_kas = True
                        End If
                    Next

                    If _is_setara_kas = True Then
                        Try
                            Using objinsert As New master_new.WDABasepgsql("", "")
                                With objinsert
                                    .Connection.Open()
                                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                    Try
                                        .Command = .Connection.CreateCommand
                                        .Command.Transaction = sqlTran

                                        If cashflow_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                            sqlTran.Rollback()
                                            Exit Sub
                                        End If

                                        sqlTran.Commit()
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

                End If

            Else
                ds_group_glt.Tables(0).AcceptChanges()

                'cek dulu ada kas setara kas atau tidak
                _is_setara_kas = False
                For a As Integer = 0 To ds_group_glt.Tables(0).Rows.Count - 1
                    If ds_group_glt.Tables(0).Rows(a).Item("ac_is_cf") = "Y" Then
                        _is_setara_kas = True
                    End If
                Next

                If _is_setara_kas = True Then
                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If cashflow_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                        sqlTran.Rollback()
                                        Exit Sub
                                    End If

                                    sqlTran.Commit()
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

                _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code")
                ds_group_glt.Tables(0).Clear()
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
                dr("ac_is_cf") = ds_glt.Tables(0).Rows(i).Item("ac_is_cf")
                dr("ac_priority") = ds_glt.Tables(0).Rows(i).Item("ac_priority")

                'untuk yang reverse dibalik kredit debit dan dinegatifkan
                If ds_glt.Tables(0).Rows(i).Item("glt_is_reverse") = "Y" Then
                    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_credit") * -1
                    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_debit") * -1
                Else
                    dr("glt_debit") = ds_glt.Tables(0).Rows(i).Item("glt_debit")
                    dr("glt_credit") = ds_glt.Tables(0).Rows(i).Item("glt_credit")
                End If

                dr("glt_is_reverse") = ds_glt.Tables(0).Rows(i).Item("glt_is_reverse")
                dr("_empty") = ds_glt.Tables(0).Rows(i).Item("_empty")
                ds_group_glt.Tables(0).Rows.Add(dr)

                'jika pada looping terakhir 
                If i = ds_glt.Tables(0).Rows.Count - 1 Then
                    ds_group_glt.Tables(0).AcceptChanges()

                    'cek dulu ada kas setara kas atau tidak
                    _is_setara_kas = False
                    For a As Integer = 0 To ds_group_glt.Tables(0).Rows.Count - 1
                        If ds_group_glt.Tables(0).Rows(a).Item("ac_is_cf") = "Y" Then
                            _is_setara_kas = True
                        End If
                    Next

                    If _is_setara_kas = True Then
                        Try
                            Using objinsert As New master_new.WDABasepgsql("", "")
                                With objinsert
                                    .Connection.Open()
                                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                    Try
                                        .Command = .Connection.CreateCommand
                                        .Command.Transaction = sqlTran

                                        If cashflow_allocation(ssqls, objinsert, ds_group_glt) = False Then
                                            sqlTran.Rollback()
                                            Exit Sub
                                        End If

                                        sqlTran.Commit()
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

                End If

            End If

        Next


    End Sub

    Private Function cashflow_allocation(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet) As Boolean
        Dim _sign_priority As String
        Dim _temp_amount As Double
        Dim _ac_id1 As Integer
        Dim _sign1 As String
        Dim _rst_amount, _rst_amount1, _rst_amount2 As Double
        Dim _negatif_jurnal As Boolean = False
        
        cashflow_allocation = True

        If par_ds.Tables(0).Rows(0).Item("glt_debit") <> 0 Then
            _sign_priority = "D"

            If par_ds.Tables(0).Rows(0).Item("glt_debit") < 0 Then
                _negatif_jurnal = True
            End If
        Else
            _sign_priority = "C"

            If par_ds.Tables(0).Rows(0).Item("glt_credit") < 0 Then
                _negatif_jurnal = True
            End If
        End If

        If _negatif_jurnal = True Then 'positifkan dulu
            For x As Integer = 0 To par_ds.Tables(0).Rows.Count - 1
                par_ds.Tables(0).Rows(x).Item("glt_debit") = par_ds.Tables(0).Rows(x).Item("glt_debit") * -1
                par_ds.Tables(0).Rows(x).Item("glt_credit") = par_ds.Tables(0).Rows(x).Item("glt_credit") * -1
            Next
        End If

        If _sign_priority = "D" Then
            For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

                If par_ds.Tables(0).Rows(i).Item("glt_debit") <> 0 Then 'cari yang debit saja

                    For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

                        If par_ds.Tables(0).Rows(j).Item("glt_credit") <> 0 Then 'cari yang credit saja

                            If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

                                _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_debit") - par_ds.Tables(0).Rows(j).Item("glt_credit")

                                If _temp_amount = 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit")
                                    par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
                                    par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                    par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
                                    par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                                ElseIf _temp_amount < 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit")
                                    par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
                                    par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                    par_ds.Tables(0).Rows(j).Item("glt_credit") = par_ds.Tables(0).Rows(j).Item("glt_credit") - par_ds.Tables(0).Rows(i).Item("glt_debit")
                                ElseIf _temp_amount > 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_credit")
                                    par_ds.Tables(0).Rows(i).Item("glt_debit") = _temp_amount
                                    par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
                                    par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                                End If

                                If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
                                    _rst_amount = _rst_amount * -1
                                End If

                                '**update cashflow-nya langsung 2 untuk cek cashflownya ada debit/credit

                                If (par_ds.Tables(0).Rows(i).Item("ac_is_cf") = "Y") Or (par_ds.Tables(0).Rows(j).Item("ac_is_cf") = "Y") Then 'penyaringan bahwa salah satu adalah cashflow

                                    If (par_ds.Tables(0).Rows(i).Item("ac_is_cf") = "N") Then 'justru bukan cashflownya yang diupdate (perhatikan rule)

                                        'karena kas setara kas pasti lawannya, maka dikali -1
                                        _rst_amount = _rst_amount * -1
                                        '--------------------------------------

                                        If par_ds.Tables(0).Rows(i).Item("ac_sign") = "C" Then   'cek kesesuaian sign (- or + )
                                            _rst_amount1 = _rst_amount * -1
                                        Else
                                            _rst_amount1 = _rst_amount
                                        End If

                                        _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                        _sign1 = "D"
                                        '_ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                        '_sign2 = "C"
                                        'kirim untuk dialokasikan ke cashflow
                                        If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _rst_amount1) = False Then
                                            Return False
                                            Exit Function
                                        End If
                                    End If

                                    If (par_ds.Tables(0).Rows(j).Item("ac_is_cf") = "N") Then 'justru bukan cashflownya yang diupdate (perhatikan rule)

                                        'karena kas setara kas pasti lawannya, maka dikali -1
                                        _rst_amount = _rst_amount * -1
                                        '--------------------------------------

                                        If par_ds.Tables(0).Rows(j).Item("ac_sign") = "D" Then 'cek kesesuaian sign (- or + )
                                            _rst_amount2 = _rst_amount * -1
                                        Else
                                            _rst_amount2 = _rst_amount
                                        End If

                                        _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                        _sign1 = "C"
                                        '_ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                        '_sign2 = "D"

                                        'kirim untuk dialokasikan ke cashflow
                                        If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _rst_amount2) = False Then
                                            Return False
                                            Exit Function
                                        End If

                                    End If

                                End If
                                '******************************************************

                            End If
                        End If

                    Next
                End If
            Next
        ElseIf _sign_priority = "C" Then

            For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

                If par_ds.Tables(0).Rows(i).Item("glt_credit") <> 0 Then 'cari yang credit saja

                    For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

                        If par_ds.Tables(0).Rows(j).Item("glt_debit") <> 0 Then 'cari yang debit saja

                            If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

                                _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_credit") - par_ds.Tables(0).Rows(j).Item("glt_debit")

                                If _temp_amount = 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
                                    par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
                                    par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                    par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
                                    par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                                ElseIf _temp_amount < 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
                                    par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
                                    par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
                                    par_ds.Tables(0).Rows(j).Item("glt_debit") = par_ds.Tables(0).Rows(j).Item("glt_debit") - par_ds.Tables(0).Rows(i).Item("glt_credit")
                                ElseIf _temp_amount > 0 Then
                                    _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_debit")
                                    par_ds.Tables(0).Rows(i).Item("glt_credit") = _temp_amount
                                    par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
                                    par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
                                End If

                                If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
                                    _rst_amount = _rst_amount * -1
                                End If

                                '**update cashflow-nya langsung 2 untuk debit credit
                                If (par_ds.Tables(0).Rows(i).Item("ac_is_cf") = "Y") Or (par_ds.Tables(0).Rows(j).Item("ac_is_cf") = "Y") Then 'penyaringan bahwa salah satu adalah cashflow

                                    If (par_ds.Tables(0).Rows(i).Item("ac_is_cf") = "N") Then 'justru bukan cashflownya yang diupdate (perhatikan rule)

                                        'karena kas setara kas pasti lawannya, maka dikali -1
                                        _rst_amount = _rst_amount * -1
                                        '--------------------------------------

                                        If par_ds.Tables(0).Rows(i).Item("ac_sign") = "D" Then
                                            _rst_amount1 = _rst_amount * -1
                                        Else
                                            _rst_amount1 = _rst_amount
                                        End If

                                        _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                        _sign1 = "C"
                                        '_ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                        '_sign2 = "D"

                                        'kirim untuk dialokasikan ke cashflow
                                        If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _rst_amount1) = False Then
                                            Return False
                                            Exit Function
                                        End If

                                    End If

                                    If (par_ds.Tables(0).Rows(j).Item("ac_is_cf") = "N") Then 'justru bukan cashflownya yang diupdate (perhatikan rule)

                                        'karena kas setara kas pasti lawannya, maka dikali -1
                                        _rst_amount = _rst_amount * -1
                                        '--------------------------------------

                                        If par_ds.Tables(0).Rows(j).Item("ac_sign") = "C" Then
                                            _rst_amount2 = _rst_amount * -1
                                        Else
                                            _rst_amount2 = _rst_amount
                                        End If

                                        _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                        _sign1 = "D"
                                        '_ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                        '_sign2 = "C"

                                        'kirim untuk dialokasikan ke cashflow
                                        If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _rst_amount2) = False Then
                                            Return False
                                            Exit Function
                                        End If

                                    End If

                                End If
                                '******************************************************

                            End If
                        End If
                    Next
                End If
            Next
        End If

    End Function

    Private Function cashflow_allocation_post(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id As Integer, ByVal par_sign As String, ByVal par_amount As Double) As Boolean
        cashflow_allocation_post = True

        Dim _cfdrule_oid As String
        Dim _rule_available As Boolean = False

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select cfdrule_oid from cfdrule_mstr " _
                                        & "  INNER JOIN cfdruled_det ON (cfdruled_det.cfdruled_cfdrule_oid  = cfdrule_mstr.cfdrule_oid)" _
                                        & " where cfdruled_ac_id = " & par_ac_id _
                                        & " and cfdruled_sign = " & SetSetring(par_sign) 
                                        
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _cfdrule_oid = .DataReader("cfdrule_oid").ToString
                        _rule_available = True
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _rule_available = False Then
            Dim dt_bantu1 As DataTable = get_account(par_ac_id)

            MsgBox("Cashflow Rule Account : " & dt_bantu1.Rows(0).Item("ac_code") & ", " & dt_bantu1.Rows(0).Item("ac_name") & "(" & par_sign & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
            'MsgBox("Cashflow Rule Account Id " & par_ac_id & "(" & par_sign & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
            Return False
        End If

        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.cfd_temp   " _
                                    & "SET  " _
                                    & "  cfd_amount = coalesce(cfd_amount,0.00) + " & SetDblDB(par_amount) & "  " _
                                    & "  " _
                                    & " WHERE cfd_session = " & SetSetring(_session.ToString) & " " _
                                    & "  AND cfd_ref_oid = " & SetSetring(_cfdrule_oid.ToString) & " "
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Private Function get_account(ByVal _id_ac As Integer) As DataTable
        Using ds_bantu As New DataSet()
            Dim dt_bantu As New DataTable()
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "select ac_code, ac_name from ac_mstr where ac_id = " + _id_ac.ToString
                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "acct")
                        dt_bantu = ds_bantu.Tables(0)
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return dt_bantu
        End Using
    End Function

    'Private Function cashflow_allocation(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet) As Boolean
    '    Dim _sign_priority As String
    '    Dim _temp_amount As Double
    '    Dim _ac_id1, _ac_id2 As Integer
    '    Dim _sign1, _sign2 As String
    '    Dim _rst_amount, _rst_amount1, _rst_amount2 As Double
    '    Dim _negatif_jurnal As Boolean = False

    '    cashflow_allocation = True

    '    If par_ds.Tables(0).Rows(0).Item("glt_debit") <> 0 Then
    '        _sign_priority = "D"

    '        If par_ds.Tables(0).Rows(0).Item("glt_debit") < 0 Then
    '            _negatif_jurnal = True
    '        End If
    '    Else
    '        _sign_priority = "C"

    '        If par_ds.Tables(0).Rows(0).Item("glt_credit") < 0 Then
    '            _negatif_jurnal = True
    '        End If
    '    End If

    '    If _negatif_jurnal = True Then 'positifkan dulu
    '        For x As Integer = 0 To par_ds.Tables(0).Rows.Count - 1
    '            par_ds.Tables(0).Rows(x).Item("glt_debit") = par_ds.Tables(0).Rows(x).Item("glt_debit") * -1
    '            par_ds.Tables(0).Rows(x).Item("glt_credit") = par_ds.Tables(0).Rows(x).Item("glt_debit") * -1
    '        Next
    '    End If

    '    If _sign_priority = "D" Then
    '        For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

    '            If par_ds.Tables(0).Rows(i).Item("glt_debit") <> 0 Then 'cari yang debit saja

    '                For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

    '                    If par_ds.Tables(0).Rows(j).Item("glt_credit") <> 0 Then 'cari yang credit saja

    '                        If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

    '                            _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_debit") - par_ds.Tables(0).Rows(j).Item("glt_credit")

    '                            If _temp_amount = 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
    '                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
    '                                par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
    '                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
    '                            ElseIf _temp_amount < 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_debit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_debit") = 0
    '                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
    '                                par_ds.Tables(0).Rows(j).Item("glt_credit") = par_ds.Tables(0).Rows(j).Item("glt_credit") - par_ds.Tables(0).Rows(i).Item("glt_debit")
    '                            ElseIf _temp_amount > 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_credit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_debit") = _temp_amount
    '                                par_ds.Tables(0).Rows(j).Item("glt_credit") = 0
    '                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
    '                            End If

    '                            If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
    '                                _rst_amount = _rst_amount * -1
    '                            End If

    '                            '**update cashflow-nya langsung 2 untuk debit credit
    '                            If par_ds.Tables(0).Rows(i).Item("ac_sign") = "C" Then
    '                                _rst_amount1 = _rst_amount * -1
    '                            Else
    '                                _rst_amount1 = _rst_amount
    '                            End If

    '                            _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
    '                            _sign1 = "D"
    '                            _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
    '                            _sign2 = "C"
    '                            'kirim untuk dialokasikan ke cashflow
    '                            If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
    '                                Return False
    '                                Exit Function
    '                            End If

    '                            If par_ds.Tables(0).Rows(j).Item("ac_sign") = "D" Then
    '                                _rst_amount2 = _rst_amount * -1
    '                            Else
    '                                _rst_amount2 = _rst_amount
    '                            End If

    '                            _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
    '                            _sign1 = "C"
    '                            _ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
    '                            _sign2 = "D"

    '                            'kirim untuk dialokasikan ke cashflow
    '                            If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
    '                                Return False
    '                                Exit Function
    '                            End If

    '                            '******************************************************

    '                        End If
    '                    End If

    '                Next
    '            End If
    '        Next
    '    ElseIf _sign_priority = "C" Then

    '        For i As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

    '            If par_ds.Tables(0).Rows(i).Item("glt_credit") <> 0 Then 'cari yang credit saja

    '                For j As Integer = 0 To par_ds.Tables(0).Rows.Count - 1

    '                    If par_ds.Tables(0).Rows(j).Item("glt_debit") <> 0 Then 'cari yang debit saja

    '                        If par_ds.Tables(0).Rows(i).Item("_empty") <> "Y" Then 'jika belum kosong

    '                            _temp_amount = par_ds.Tables(0).Rows(i).Item("glt_credit") - par_ds.Tables(0).Rows(j).Item("glt_debit")

    '                            If _temp_amount = 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
    '                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
    '                                par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
    '                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
    '                            ElseIf _temp_amount < 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(i).Item("glt_credit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_credit") = 0
    '                                par_ds.Tables(0).Rows(i).Item("_empty") = "Y"
    '                                par_ds.Tables(0).Rows(j).Item("glt_debit") = par_ds.Tables(0).Rows(j).Item("glt_debit") - par_ds.Tables(0).Rows(i).Item("glt_credit")
    '                            ElseIf _temp_amount > 0 Then
    '                                _rst_amount = par_ds.Tables(0).Rows(j).Item("glt_debit")
    '                                par_ds.Tables(0).Rows(i).Item("glt_credit") = _temp_amount
    '                                par_ds.Tables(0).Rows(j).Item("glt_debit") = 0
    '                                par_ds.Tables(0).Rows(j).Item("_empty") = "Y"
    '                            End If

    '                            If _negatif_jurnal = True Then 'kembalikan ke negatif lagi
    '                                _rst_amount = _rst_amount * -1
    '                            End If

    '                            '**update cashflow-nya langsung 2 untuk debit credit
    '                            If par_ds.Tables(0).Rows(i).Item("ac_sign") = "D" Then
    '                                _rst_amount1 = _rst_amount * -1
    '                            Else
    '                                _rst_amount1 = _rst_amount
    '                            End If

    '                            _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
    '                            _sign1 = "C"
    '                            _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
    '                            _sign2 = "D"

    '                            'kirim untuk dialokasikan ke cashflow
    '                            If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
    '                                Return False
    '                                Exit Function
    '                            End If

    '                            If par_ds.Tables(0).Rows(j).Item("ac_sign") = "C" Then
    '                                _rst_amount2 = _rst_amount * -1
    '                            Else
    '                                _rst_amount2 = _rst_amount
    '                            End If

    '                            _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
    '                            _sign1 = "D"
    '                            _ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
    '                            _sign2 = "C"

    '                            'kirim untuk dialokasikan ke cashflow
    '                            If cashflow_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
    '                                Return False
    '                                Exit Function
    '                            End If

    '                            '******************************************************

    '                        End If
    '                    End If

    '                Next
    '            End If
    '        Next
    '    End If

    'End Function

    'Private Function cashflow_allocation_post(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id1 As Integer, ByVal par_sign1 As String, ByVal par_ac_id2 As Integer, ByVal par_sign2 As String, ByVal par_amount As Double, ByVal par_glt_code As String) As Boolean
    '    cashflow_allocation_post = True

    '    Dim _rstbal_oid As String
    '    Dim _rule_available As Boolean = False

    '    Try
    '        Using objcek As New master_new.WDABasepgsql("", "")
    '            With objcek
    '                .Connection.Open()
    '                .Command = .Connection.CreateCommand
    '                .Command.CommandType = CommandType.Text
    '                .Command.CommandText = "select rstbal_oid from rstbal_mstr " _
    '                                    & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
    '                                    & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
    '                                    & "  INNER JOIN rstrule_mstr ON (rstrule_mstr.rstrule_oid = rstbal_mstr.rstbal_rstrule_oid)" _
    '                                    & "  INNER JOIN rstruled_det ON (rstruled_det.rstruled_rstrule_oid  = rstrule_mstr.rstrule_oid)" _
    '                                    & " where rstbal_en_id = " & le_entity.EditValue _
    '                                    & " and rstbal_gcal_oid = " & SetSetring(le_periode.EditValue) _
    '                                    & " and rstruled_ac_id1 = " & par_ac_id1 _
    '                                    & " and rstruled_sign1 = " & SetSetring(par_sign1) _
    '                                    & " and ((rstruled_ac_id2 = " & par_ac_id2 & ") or (coalesce(rstruled_ac_id2,0)=0))" _
    '                                    & " and ((rstruled_sign2 = " & SetSetring(par_sign2) & ") or (coalesce(rstruled_sign2,'')=''))"
    '                .InitializeCommand()
    '                .DataReader = .Command.ExecuteReader
    '                While .DataReader.Read
    '                    _rstbal_oid = .DataReader("rstbal_oid")
    '                    _rule_available = True
    '                End While
    '            End With
    '        End Using
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    If _rule_available = False Then
    '        MsgBox("Rule Account Id " & par_ac_id1 & "(" & par_sign1 & ") for Account Id " & par_ac_id2 & "(" & par_sign2 & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
    '        Return False
    '    End If

    '    With par_obj
    '        Try
    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "UPDATE  " _
    '                                & "  public.rstbal_mstr   " _
    '                                & "SET  " _
    '                                & "  rstbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
    '                                & "  rstbal_upd_date = current_timestamp,  " _
    '                                & "  rstbal_amount = coalesce(rstbal_amount,0) + " & SetDbl(par_amount) & ",  " _
    '                                & "  rstbal_dt = current_timestamp  " _
    '                                & "  " _
    '                                & "WHERE  " _
    '                                & "  rstbal_oid = " & SetSetring(_rstbal_oid) & " "
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()

    '            .Command.CommandType = CommandType.Text
    '            .Command.CommandText = "UPDATE  " _
    '                                & "  public.glt_det   " _
    '                                & "SET  " _
    '                                & "  glt_is_gen_ros = 'Y' " _
    '                                & " WHERE  " _
    '                                & "  glt_code = " & SetSetring(par_glt_code.ToString) & " "
    '            par_ssqls.Add(.Command.CommandText)
    '            .Command.ExecuteNonQuery()
    '            .Command.Parameters.Clear()

    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '            Return False
    '        End Try
    '    End With

    'End Function

    Private Sub generate_sum_cashflow()
        Dim ds_cfd_sum As DataSet
        ds_cfd_sum = New DataSet

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT " _
                        & " cfdrule_oid, " _
                        & " sum(cfd_amount) as cfd_amount  " _
                        & " FROM cfdrule_mstr " _
                        & "  INNER JOIN cfdrules_sum ON (cfdrules_sum.cfdrules_cfdrule_oid  = cfdrule_mstr.cfdrule_oid)" _
                        & "  INNER JOIN cfd_temp ON (cfd_temp.cfd_ref_oid  = cfdrules_sum.cfdrules_ref_oid)" _
                        & " WHERE coalesce(cfdrule_is_sum,'N') = 'Y' " _
                        & " AND cfd_session = " & SetSetring(_session) _
                        & " GROUP BY  cfdrule_oid, cfdrule_seq" _
                        & " order by cfdrule_seq "
                    .InitializeCommand()
                    .FillDataSet(ds_cfd_sum, "list_sum_cashflow")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ssqls As New ArrayList
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran


                        For x As Integer = 0 To ds_cfd_sum.Tables(0).Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.cfd_temp   " _
                                                & " SET  " _
                                                & "  cfd_amount = coalesce(cfd_amount,0) + " & SetDblDB(ds_cfd_sum.Tables(0).Rows(x).Item("cfd_amount")) & "  " _
                                                & "  " _
                                                & " WHERE  cfd_session = " & SetSetring(_session.ToString) & " " _
                                                & " AND cfd_ref_oid = " & SetSetring(ds_cfd_sum.Tables(0).Rows(x).Item("cfdrule_oid")) & " "
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next
                        

                        
                        sqlTran.Commit()
               
                    Catch ex As nPgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            gc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
