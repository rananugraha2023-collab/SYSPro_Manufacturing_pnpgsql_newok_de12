Imports npgsql
Imports master_new.ModFunction

Public Class FRosGenerateView
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim mf As New master_new.ModFunction
    Dim ds_glt As DataSet
    Dim ds_group_glt As DataSet
    Dim ds_rosetta As DataSet

    Private Sub FRosGenerateView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        dt_bantu = (func_data.load_en_mstr_tran())
        le_entity.Properties.DataSource = dt_bantu
        le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_entity.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_gcal_mstr())
        le_periode.Properties.DataSource = dt_bantu
        le_periode.Properties.DisplayMember = dt_bantu.Columns("gcal_start_date").ToString
        le_periode.Properties.ValueMember = dt_bantu.Columns("gcal_oid").ToString
        le_periode.ItemIndex = 0

    End Sub

    Public Overrides Sub help_load_data(ByVal par As Boolean)
        ds_rosetta = New DataSet
        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .SQL = "SELECT  " _
                    & "  rstbal_oid, " _
                    & "  rstbal_dom_id, " _
                    & "  rstbal_en_id, " _
                    & "  rstbal_add_by, " _
                    & "  rstbal_add_date, " _
                    & "  rstbal_upd_by, " _
                    & "  rstbal_upd_date, " _
                    & "  rstbal_rstrule_oid, " _
                    & "  rstbal_gcal_oid, " _
                    & "  coalesce(rstbal_openbal_amount,0) + coalesce(rstbal_amount,0) as rstbal_amount, " _
                    & "  rstbal_dt, " _
                    & "  gcal_year, " _
                    & "  gcal_periode, " _
                    & "  gcal_start_date, " _
                    & "  gcal_end_date, " _
                    & "  group_mstr.code_name as group_name, " _
                    & "  account_mstr.code_name as account_name, " _
                    & "  tranline_mstr.code_name as line_name, " _
                    & "  cashflow_mstr.code_name as cashflow_name " _
                    & " FROM  " _
                    & "  public.rstbal_mstr " _
                    & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                    & "  INNER JOIN public.rstrule_mstr ON (public.rstrule_mstr.rstrule_oid = public.rstbal_mstr.rstbal_rstrule_oid)" _
                    & "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                    & "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                    & "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                    & "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                    & " where rstbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                    & " ORDER BY group_mstr.code_seq,account_mstr.code_seq,tranline_mstr.code_seq,cashflow_mstr.code_seq "

                    ' .SQL = "SELECT  " _
                    '& "  rstbal_oid, " _
                    '& "  rstbal_dom_id, " _
                    '& "  rstbal_en_id, " _
                    '& "  rstbal_add_by, " _
                    '& "  rstbal_add_date, " _
                    '& "  rstbal_upd_by, " _
                    '& "  rstbal_upd_date, " _
                    '& "  rstbal_rstrule_oid, " _
                    '& "  rstbal_gcal_oid, " _
                    '& "  coalesce(rstbal_openbal_amount,0) + coalesce(rstbal_amount,0) as rstbal_amount, " _
                    '& "  rstbal_dt, " _
                    '& "  en_desc, " _
                    '& "  gcal_year, " _
                    '& "  gcal_periode, " _
                    '& "  gcal_start_date, " _
                    '& "  gcal_end_date, " _
                    '& "  group_mstr.code_name as group_name, " _
                    '& "  account_mstr.code_name as account_name, " _
                    '& "  tranline_mstr.code_name as line_name, " _
                    '& "  cashflow_mstr.code_name as cashflow_name " _
                    '& " FROM  " _
                    '& "  public.rstbal_mstr " _
                    '& "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
                    '& "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                    '& "  INNER JOIN public.rstrule_mstr ON (public.rstrule_mstr.rstrule_oid = public.rstbal_mstr.rstbal_rstrule_oid)" _
                    '& "  INNER JOIN code_mstr group_mstr on rstrule_group_id = group_mstr.code_id " _
                    '& "  INNER JOIN code_mstr account_mstr on rstrule_name_id = account_mstr.code_id " _
                    '& "  INNER JOIN code_mstr tranline_mstr on rstrule_line_id = tranline_mstr.code_id " _
                    '& "  INNER JOIN code_mstr cashflow_mstr on rstrule_cashflow_id = cashflow_mstr.code_id " _
                    '& " where rstbal_en_id = " + le_entity.EditValue.ToString _
                    '& " and rstbal_gcal_oid = '" + le_periode.EditValue.ToString + "'" _
                    '& " and rstbal_en_id in (select user_en_id from tconfuserentity " _
                    '                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    '& " ORDER BY group_mstr.code_seq,account_mstr.code_seq,tranline_mstr.code_seq,cashflow_mstr.code_seq "

                    .InitializeCommand()
                    .FillDataSet(ds_rosetta, "rosseta")
                    pgc_master.DataSource = ds_rosetta.Tables(0)
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub sb_generate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_generate.Click

        'cek opening balance
        Dim _gcal_available As Boolean = False
        Dim _gcal_start_date As Date
        Dim _gcal_end_date As Date

        Try
            Using objcb As New master_new.WDABasepgsql("", "")
                With objcb
                    .Connection.Open()
                    .Command = .Connection.CreateCommand

                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rstbal_oid, gcal_start_date, gcal_end_date  from rstbal_mstr inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " + _
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
                            & "  coalesce(ac_priority,99) as ac_priority , " _
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
                            & "  and glt_type<>'JE'  " _
                            & " order by glt_date, glt_code, ac_priority, glt_credit, glt_debit "

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
                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
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

            Else
                ds_group_glt.Tables(0).AcceptChanges()

                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
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

                _glt_code = ds_glt.Tables(0).Rows(i).Item("glt_code")
                ds_group_glt.Tables(0).Clear()
                dr = ds_group_glt.Tables(0).NewRow
                dr("glt_code") = ds_glt.Tables(0).Rows(i).Item("glt_code")
                dr("glt_date") = ds_glt.Tables(0).Rows(i).Item("glt_date")
                dr("glt_exc_rate") = ds_glt.Tables(0).Rows(i).Item("glt_exc_rate")
                dr("glt_ac_id") = ds_glt.Tables(0).Rows(i).Item("glt_ac_id")
                dr("ac_sign") = ds_glt.Tables(0).Rows(i).Item("ac_sign")
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

                    Try
                        Using objinsert As New master_new.WDABasepgsql("", "")
                            With objinsert
                                .Connection.Open()
                                Dim sqlTran As nPgSqlTransaction = .Connection.BeginTransaction()
                                Try
                                    .Command = .Connection.CreateCommand
                                    .Command.Transaction = sqlTran

                                    If ros_allocation(ssqls, objinsert, ds_group_glt) = False Then
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

        Next

        MsgBox("Generate Successful...", MsgBoxStyle.Information, "Data Generated")
    End Sub

    Private Function ros_allocation(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet) As Boolean
        Dim _sign_priority As String
        Dim _temp_amount As Double
        Dim _ac_id1, _ac_id2 As Integer
        Dim _sign1, _sign2 As String
        Dim _rst_amount, _rst_amount1, _rst_amount2 As Double
        Dim _negatif_jurnal As Boolean = False

        ros_allocation = True

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
                                'Exit Function
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

                                '**update rosetta-nya langsung 2 untuk debit credit
                                If par_ds.Tables(0).Rows(i).Item("ac_sign") = "C" Then
                                    _rst_amount1 = _rst_amount * -1
                                Else
                                    _rst_amount1 = _rst_amount
                                End If

                                _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                _sign1 = "D"
                                _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                _sign2 = "C"
                                'kirim untuk dialokasikan ke rosetta
                                'If par_ds.Tables(0).Rows(i).Item("glt_code") = "CO5112010000053" Then
                                '    If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                '        MessageBox.Show(par_ds.Tables(0).Rows(i).Item("glt_code"), "")
                                '        Return False
                                '        Exit Function
                                '    End If
                                'End If
                                If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                    Return False
                                    Exit Function
                                End If

                                If par_ds.Tables(0).Rows(j).Item("ac_sign") = "D" Then
                                    _rst_amount2 = _rst_amount * -1
                                Else
                                    _rst_amount2 = _rst_amount
                                End If

                                _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                _sign1 = "C"
                                _ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                _sign2 = "D"

                                'kirim untuk dialokasikan ke rosetta
                                'If par_ds.Tables(0).Rows(i).Item("glt_code") = "CO5112010000053" Then
                                '    If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                '        Return False
                                '        Exit Function
                                '    End If
                                'End If
                                If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                    Return False
                                    Exit Function
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

                                '**update rosetta-nya langsung 2 untuk debit credit
                                If par_ds.Tables(0).Rows(i).Item("ac_sign") = "D" Then
                                    _rst_amount1 = _rst_amount * -1
                                Else
                                    _rst_amount1 = _rst_amount
                                End If

                                _ac_id1 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                _sign1 = "C"
                                _ac_id2 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                _sign2 = "D"

                                'kirim untuk dialokasikan ke rosetta
                                If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount1, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                    Return False
                                    Exit Function
                                End If

                                If par_ds.Tables(0).Rows(j).Item("ac_sign") = "C" Then
                                    _rst_amount2 = _rst_amount * -1
                                Else
                                    _rst_amount2 = _rst_amount
                                End If

                                _ac_id1 = par_ds.Tables(0).Rows(j).Item("glt_ac_id")
                                _sign1 = "D"
                                _ac_id2 = par_ds.Tables(0).Rows(i).Item("glt_ac_id")
                                _sign2 = "C"

                                'kirim untuk dialokasikan ke rosetta
                                If ros_allocation_post(par_ssqls, par_obj, _ac_id1, _sign1, _ac_id2, _sign2, _rst_amount2, par_ds.Tables(0).Rows(i).Item("glt_code")) = False Then
                                    Return False
                                    Exit Function
                                End If

                                '******************************************************

                            End If
                        End If

                    Next
                End If
            Next
        End If

    End Function

    Private Function ros_allocation_post(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ac_id1 As Integer, ByVal par_sign1 As String, ByVal par_ac_id2 As Integer, ByVal par_sign2 As String, ByVal par_amount As Double, ByVal par_glt_code As String) As Boolean
        ros_allocation_post = True

        Dim _rstbal_oid As String
        Dim _rule_available As Boolean = False

        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select rstbal_oid from rstbal_mstr " _
                                        & "  INNER JOIN public.en_mstr ON (public.rstbal_mstr.rstbal_en_id = public.en_mstr.en_id)" _
                                        & "  inner join gcal_mstr on gcal_oid = rstbal_gcal_oid " _
                                        & "  INNER JOIN rstrule_mstr ON (rstrule_mstr.rstrule_oid = rstbal_mstr.rstbal_rstrule_oid)" _
                                        & "  INNER JOIN rstruled_det ON (rstruled_det.rstruled_rstrule_oid  = rstrule_mstr.rstrule_oid)" _
                                        & " where  rstbal_gcal_oid = " & SetSetring(le_periode.EditValue) _
                                        & " and rstruled_ac_id1 = " & par_ac_id1 _
                                        & " and rstruled_sign1 = " & SetSetring(par_sign1) _
                                        & " and ((rstruled_ac_id2 = " & par_ac_id2 & ") or (coalesce(rstruled_ac_id2,0)=0))" _
                                        & " and ((rstruled_sign2 = " & SetSetring(par_sign2) & ") or (coalesce(rstruled_sign2,'')=''))"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _rstbal_oid = .DataReader("rstbal_oid").ToString
                        _rule_available = True
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If _rule_available = False Then
            Dim dt_bantu1 As DataTable = get_account(par_ac_id1)
            Dim dt_bantu2 As DataTable = get_account(par_ac_id2)

            MsgBox("Rule Account : " & dt_bantu1.Rows(0).Item("ac_code") & ", " & dt_bantu1.Rows(0).Item("ac_name") & "(" & par_sign1 & ")" + Chr(13) + _
                   " for Account : " & dt_bantu2.Rows(0).Item("ac_code") & ", " & dt_bantu2.Rows(0).Item("ac_name") & "(" & par_sign2 & "), Unavailable", MsgBoxStyle.Critical, "Allocation Error")
            Return False
        End If


        With par_obj
            Try
                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.rstbal_mstr   " _
                                    & "SET  " _
                                    & "  rstbal_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  rstbal_upd_date = current_timestamp,  " _
                                    & "  rstbal_amount = coalesce(rstbal_amount,0) + " & SetDbl(par_amount) & ",  " _
                                    & "  rstbal_dt = current_timestamp  " _
                                    & "  " _
                                    & "WHERE  " _
                                    & "  rstbal_oid = " & SetSetring(_rstbal_oid) & " "
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                .Command.Parameters.Clear()

                .Command.CommandType = CommandType.Text
                .Command.CommandText = "UPDATE  " _
                                    & "  public.glt_det   " _
                                    & "SET  " _
                                    & "  glt_is_gen_ros = 'Y' " _
                                    & " WHERE  " _
                                    & "  glt_code = " & SetSetring(par_glt_code.ToString) & " "
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


    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            pgc_master.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function
End Class
